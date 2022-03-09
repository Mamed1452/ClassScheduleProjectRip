using Mohajer.ClassScheduleProject.CentralUnit.ListOfMainDomains;
using Mohajer.ClassScheduleProject.CentralUnit.LessonsOfSemesters;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Mohajer.ClassScheduleProject.CentralUnit.MainDomains.Exporting;
using Mohajer.ClassScheduleProject.CentralUnit.MainDomains.Dtos;
using Mohajer.ClassScheduleProject.Dto;
using Abp.Application.Services.Dto;
using Mohajer.ClassScheduleProject.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Mohajer.ClassScheduleProject.Storage;

namespace Mohajer.ClassScheduleProject.CentralUnit.MainDomains
{
    [AbpAuthorize(AppPermissions.Pages_MainDomains)]
    public class MainDomainsAppService : ClassScheduleProjectAppServiceBase, IMainDomainsAppService
    {
        private readonly IRepository<MainDomain, long> _mainDomainRepository;
        private readonly IMainDomainsExcelExporter _mainDomainsExcelExporter;
        private readonly IRepository<ListOfMainDomain, long> _lookup_listOfMainDomainRepository;
        private readonly IRepository<LessonsOfSemester, long> _lookup_lessonsOfSemesterRepository;

        public MainDomainsAppService(IRepository<MainDomain, long> mainDomainRepository, IMainDomainsExcelExporter mainDomainsExcelExporter, IRepository<ListOfMainDomain, long> lookup_listOfMainDomainRepository, IRepository<LessonsOfSemester, long> lookup_lessonsOfSemesterRepository)
        {
            _mainDomainRepository = mainDomainRepository;
            _mainDomainsExcelExporter = mainDomainsExcelExporter;
            _lookup_listOfMainDomainRepository = lookup_listOfMainDomainRepository;
            _lookup_lessonsOfSemesterRepository = lookup_lessonsOfSemesterRepository;

        }

        public async Task<PagedResultDto<GetMainDomainForViewDto>> GetAll(GetAllMainDomainsInput input)
        {

            var filteredMainDomains = _mainDomainRepository.GetAll()
                        .Include(e => e.ListOfMainDomainFk)
                        .Include(e => e.LessonsOfSemesterFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.MainDomainName.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MainDomainNameFilter), e => e.MainDomainName == input.MainDomainNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ListOfMainDomainListOfMainDomainNameFilter), e => e.ListOfMainDomainFk != null && e.ListOfMainDomainFk.ListOfMainDomainName == input.ListOfMainDomainListOfMainDomainNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LessonsOfSemesterLessonsOfSemesterNameFilter), e => e.LessonsOfSemesterFk != null && e.LessonsOfSemesterFk.LessonsOfSemesterName == input.LessonsOfSemesterLessonsOfSemesterNameFilter)
                        .WhereIf(input.ListOfMainDomainIdFilter.HasValue, e => false || e.ListOfMainDomainId == input.ListOfMainDomainIdFilter.Value);

            var pagedAndFilteredMainDomains = filteredMainDomains
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var mainDomains = from o in pagedAndFilteredMainDomains
                              join o1 in _lookup_listOfMainDomainRepository.GetAll() on o.ListOfMainDomainId equals o1.Id into j1
                              from s1 in j1.DefaultIfEmpty()

                              join o2 in _lookup_lessonsOfSemesterRepository.GetAll() on o.LessonsOfSemesterId equals o2.Id into j2
                              from s2 in j2.DefaultIfEmpty()

                              select new
                              {

                                  o.MainDomainName,
                                  Id = o.Id,
                                  ListOfMainDomainListOfMainDomainName = s1 == null || s1.ListOfMainDomainName == null ? "" : s1.ListOfMainDomainName.ToString(),
                                  LessonsOfSemesterLessonsOfSemesterName = s2 == null || s2.LessonsOfSemesterName == null ? "" : s2.LessonsOfSemesterName.ToString()
                              };

            var totalCount = await filteredMainDomains.CountAsync();

            var dbList = await mainDomains.ToListAsync();
            var results = new List<GetMainDomainForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetMainDomainForViewDto()
                {
                    MainDomain = new MainDomainDto
                    {

                        MainDomainName = o.MainDomainName,
                        Id = o.Id,
                    },
                    ListOfMainDomainListOfMainDomainName = o.ListOfMainDomainListOfMainDomainName,
                    LessonsOfSemesterLessonsOfSemesterName = o.LessonsOfSemesterLessonsOfSemesterName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetMainDomainForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetMainDomainForViewDto> GetMainDomainForView(long id)
        {
            var mainDomain = await _mainDomainRepository.GetAsync(id);

            var output = new GetMainDomainForViewDto { MainDomain = ObjectMapper.Map<MainDomainDto>(mainDomain) };

            if (output.MainDomain.ListOfMainDomainId != null)
            {
                var _lookupListOfMainDomain = await _lookup_listOfMainDomainRepository.FirstOrDefaultAsync((long)output.MainDomain.ListOfMainDomainId);
                output.ListOfMainDomainListOfMainDomainName = _lookupListOfMainDomain?.ListOfMainDomainName?.ToString();
            }

            if (output.MainDomain.LessonsOfSemesterId != null)
            {
                var _lookupLessonsOfSemester = await _lookup_lessonsOfSemesterRepository.FirstOrDefaultAsync((long)output.MainDomain.LessonsOfSemesterId);
                output.LessonsOfSemesterLessonsOfSemesterName = _lookupLessonsOfSemester?.LessonsOfSemesterName?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_MainDomains_Edit)]
        public async Task<GetMainDomainForEditOutput> GetMainDomainForEdit(EntityDto<long> input)
        {
            var mainDomain = await _mainDomainRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetMainDomainForEditOutput { MainDomain = ObjectMapper.Map<CreateOrEditMainDomainDto>(mainDomain) };

            if (output.MainDomain.ListOfMainDomainId != null)
            {
                var _lookupListOfMainDomain = await _lookup_listOfMainDomainRepository.FirstOrDefaultAsync((long)output.MainDomain.ListOfMainDomainId);
                output.ListOfMainDomainListOfMainDomainName = _lookupListOfMainDomain?.ListOfMainDomainName?.ToString();
            }

            if (output.MainDomain.LessonsOfSemesterId != null)
            {
                var _lookupLessonsOfSemester = await _lookup_lessonsOfSemesterRepository.FirstOrDefaultAsync((long)output.MainDomain.LessonsOfSemesterId);
                output.LessonsOfSemesterLessonsOfSemesterName = _lookupLessonsOfSemester?.LessonsOfSemesterName?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditMainDomainDto input)
        {
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_MainDomains_Create)]
        protected virtual async Task Create(CreateOrEditMainDomainDto input)
        {
            var mainDomain = ObjectMapper.Map<MainDomain>(input);

            if (AbpSession.TenantId != null)
            {
                mainDomain.TenantId = (int)AbpSession.TenantId;
            }

            await _mainDomainRepository.InsertAsync(mainDomain);

        }

        [AbpAuthorize(AppPermissions.Pages_MainDomains_Edit)]
        protected virtual async Task Update(CreateOrEditMainDomainDto input)
        {
            var mainDomain = await _mainDomainRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, mainDomain);

        }

        [AbpAuthorize(AppPermissions.Pages_MainDomains_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _mainDomainRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetMainDomainsToExcel(GetAllMainDomainsForExcelInput input)
        {

            var filteredMainDomains = _mainDomainRepository.GetAll()
                        .Include(e => e.ListOfMainDomainFk)
                        .Include(e => e.LessonsOfSemesterFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.MainDomainName.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MainDomainNameFilter), e => e.MainDomainName == input.MainDomainNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ListOfMainDomainListOfMainDomainNameFilter), e => e.ListOfMainDomainFk != null && e.ListOfMainDomainFk.ListOfMainDomainName == input.ListOfMainDomainListOfMainDomainNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LessonsOfSemesterLessonsOfSemesterNameFilter), e => e.LessonsOfSemesterFk != null && e.LessonsOfSemesterFk.LessonsOfSemesterName == input.LessonsOfSemesterLessonsOfSemesterNameFilter);

            var query = (from o in filteredMainDomains
                         join o1 in _lookup_listOfMainDomainRepository.GetAll() on o.ListOfMainDomainId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         join o2 in _lookup_lessonsOfSemesterRepository.GetAll() on o.LessonsOfSemesterId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()

                         select new GetMainDomainForViewDto()
                         {
                             MainDomain = new MainDomainDto
                             {
                                 MainDomainName = o.MainDomainName,
                                 Id = o.Id
                             },
                             ListOfMainDomainListOfMainDomainName = s1 == null || s1.ListOfMainDomainName == null ? "" : s1.ListOfMainDomainName.ToString(),
                             LessonsOfSemesterLessonsOfSemesterName = s2 == null || s2.LessonsOfSemesterName == null ? "" : s2.LessonsOfSemesterName.ToString()
                         });

            var mainDomainListDtos = await query.ToListAsync();

            return _mainDomainsExcelExporter.ExportToFile(mainDomainListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_MainDomains)]
        public async Task<PagedResultDto<MainDomainListOfMainDomainLookupTableDto>> GetAllListOfMainDomainForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_listOfMainDomainRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.ListOfMainDomainName != null && e.ListOfMainDomainName.Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var listOfMainDomainList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<MainDomainListOfMainDomainLookupTableDto>();
            foreach (var listOfMainDomain in listOfMainDomainList)
            {
                lookupTableDtoList.Add(new MainDomainListOfMainDomainLookupTableDto
                {
                    Id = listOfMainDomain.Id,
                    DisplayName = listOfMainDomain.ListOfMainDomainName?.ToString()
                });
            }

            return new PagedResultDto<MainDomainListOfMainDomainLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

        [AbpAuthorize(AppPermissions.Pages_MainDomains)]
        public async Task<PagedResultDto<MainDomainLessonsOfSemesterLookupTableDto>> GetAllLessonsOfSemesterForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_lessonsOfSemesterRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.LessonsOfSemesterName != null && e.LessonsOfSemesterName.Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var lessonsOfSemesterList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<MainDomainLessonsOfSemesterLookupTableDto>();
            foreach (var lessonsOfSemester in lessonsOfSemesterList)
            {
                lookupTableDtoList.Add(new MainDomainLessonsOfSemesterLookupTableDto
                {
                    Id = lessonsOfSemester.Id,
                    DisplayName = lessonsOfSemester.LessonsOfSemesterName?.ToString()
                });
            }

            return new PagedResultDto<MainDomainLessonsOfSemesterLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

    }
}