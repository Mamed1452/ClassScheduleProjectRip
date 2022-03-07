using Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessors;
using Mohajer.ClassScheduleProject.CentralUnit.WorkTimeInDays;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessorWorkingTimes.Exporting;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessorWorkingTimes.Dtos;
using Mohajer.ClassScheduleProject.Dto;
using Abp.Application.Services.Dto;
using Mohajer.ClassScheduleProject.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Mohajer.ClassScheduleProject.Storage;

namespace Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessorWorkingTimes
{
    [AbpAuthorize(AppPermissions.Pages_UniversityProfessorWorkingTimes)]
    public class UniversityProfessorWorkingTimesAppService : ClassScheduleProjectAppServiceBase, IUniversityProfessorWorkingTimesAppService
    {
        private readonly IRepository<UniversityProfessorWorkingTime, long> _universityProfessorWorkingTimeRepository;
        private readonly IUniversityProfessorWorkingTimesExcelExporter _universityProfessorWorkingTimesExcelExporter;
        private readonly IRepository<UniversityProfessor, int> _lookup_universityProfessorRepository;
        private readonly IRepository<WorkTimeInDay, long> _lookup_workTimeInDayRepository;

        public UniversityProfessorWorkingTimesAppService(IRepository<UniversityProfessorWorkingTime, long> universityProfessorWorkingTimeRepository, IUniversityProfessorWorkingTimesExcelExporter universityProfessorWorkingTimesExcelExporter, IRepository<UniversityProfessor, int> lookup_universityProfessorRepository, IRepository<WorkTimeInDay, long> lookup_workTimeInDayRepository)
        {
            _universityProfessorWorkingTimeRepository = universityProfessorWorkingTimeRepository;
            _universityProfessorWorkingTimesExcelExporter = universityProfessorWorkingTimesExcelExporter;
            _lookup_universityProfessorRepository = lookup_universityProfessorRepository;
            _lookup_workTimeInDayRepository = lookup_workTimeInDayRepository;

        }

        public async Task<PagedResultDto<GetUniversityProfessorWorkingTimeForViewDto>> GetAll(GetAllUniversityProfessorWorkingTimesInput input)
        {

            var filteredUniversityProfessorWorkingTimes = _universityProfessorWorkingTimeRepository.GetAll()
                        .Include(e => e.UniversityProfessorFk)
                        .Include(e => e.WorkTimeInDayFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UniversityProfessorUniversityProfessorNameFilter), e => e.UniversityProfessorFk != null && e.UniversityProfessorFk.UniversityProfessorName == input.UniversityProfessorUniversityProfessorNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.WorkTimeInDayNameWorkTimeInDayFilter), e => e.WorkTimeInDayFk != null && e.WorkTimeInDayFk.NameWorkTimeInDay == input.WorkTimeInDayNameWorkTimeInDayFilter);

            var pagedAndFilteredUniversityProfessorWorkingTimes = filteredUniversityProfessorWorkingTimes
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var universityProfessorWorkingTimes = from o in pagedAndFilteredUniversityProfessorWorkingTimes
                                                  join o1 in _lookup_universityProfessorRepository.GetAll() on o.UniversityProfessorId equals o1.Id into j1
                                                  from s1 in j1.DefaultIfEmpty()

                                                  join o2 in _lookup_workTimeInDayRepository.GetAll() on o.WorkTimeInDayId equals o2.Id into j2
                                                  from s2 in j2.DefaultIfEmpty()

                                                  select new
                                                  {

                                                      Id = o.Id,
                                                      UniversityProfessorUniversityProfessorName = s1 == null || s1.UniversityProfessorName == null ? "" : s1.UniversityProfessorName.ToString(),
                                                      WorkTimeInDayNameWorkTimeInDay = s2 == null || s2.NameWorkTimeInDay == null ? "" : s2.NameWorkTimeInDay.ToString()
                                                  };

            var totalCount = await filteredUniversityProfessorWorkingTimes.CountAsync();

            var dbList = await universityProfessorWorkingTimes.ToListAsync();
            var results = new List<GetUniversityProfessorWorkingTimeForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetUniversityProfessorWorkingTimeForViewDto()
                {
                    UniversityProfessorWorkingTime = new UniversityProfessorWorkingTimeDto
                    {

                        Id = o.Id,
                    },
                    UniversityProfessorUniversityProfessorName = o.UniversityProfessorUniversityProfessorName,
                    WorkTimeInDayNameWorkTimeInDay = o.WorkTimeInDayNameWorkTimeInDay
                };

                results.Add(res);
            }

            return new PagedResultDto<GetUniversityProfessorWorkingTimeForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetUniversityProfessorWorkingTimeForViewDto> GetUniversityProfessorWorkingTimeForView(long id)
        {
            var universityProfessorWorkingTime = await _universityProfessorWorkingTimeRepository.GetAsync(id);

            var output = new GetUniversityProfessorWorkingTimeForViewDto { UniversityProfessorWorkingTime = ObjectMapper.Map<UniversityProfessorWorkingTimeDto>(universityProfessorWorkingTime) };

            if (output.UniversityProfessorWorkingTime.UniversityProfessorId != null)
            {
                var _lookupUniversityProfessor = await _lookup_universityProfessorRepository.FirstOrDefaultAsync((int)output.UniversityProfessorWorkingTime.UniversityProfessorId);
                output.UniversityProfessorUniversityProfessorName = _lookupUniversityProfessor?.UniversityProfessorName?.ToString();
            }

            if (output.UniversityProfessorWorkingTime.WorkTimeInDayId != null)
            {
                var _lookupWorkTimeInDay = await _lookup_workTimeInDayRepository.FirstOrDefaultAsync((long)output.UniversityProfessorWorkingTime.WorkTimeInDayId);
                output.WorkTimeInDayNameWorkTimeInDay = _lookupWorkTimeInDay?.NameWorkTimeInDay?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_UniversityProfessorWorkingTimes_Edit)]
        public async Task<GetUniversityProfessorWorkingTimeForEditOutput> GetUniversityProfessorWorkingTimeForEdit(EntityDto<long> input)
        {
            var universityProfessorWorkingTime = await _universityProfessorWorkingTimeRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetUniversityProfessorWorkingTimeForEditOutput { UniversityProfessorWorkingTime = ObjectMapper.Map<CreateOrEditUniversityProfessorWorkingTimeDto>(universityProfessorWorkingTime) };

            if (output.UniversityProfessorWorkingTime.UniversityProfessorId != null)
            {
                var _lookupUniversityProfessor = await _lookup_universityProfessorRepository.FirstOrDefaultAsync((int)output.UniversityProfessorWorkingTime.UniversityProfessorId);
                output.UniversityProfessorUniversityProfessorName = _lookupUniversityProfessor?.UniversityProfessorName?.ToString();
            }

            if (output.UniversityProfessorWorkingTime.WorkTimeInDayId != null)
            {
                var _lookupWorkTimeInDay = await _lookup_workTimeInDayRepository.FirstOrDefaultAsync((long)output.UniversityProfessorWorkingTime.WorkTimeInDayId);
                output.WorkTimeInDayNameWorkTimeInDay = _lookupWorkTimeInDay?.NameWorkTimeInDay?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditUniversityProfessorWorkingTimeDto input)
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

        [AbpAuthorize(AppPermissions.Pages_UniversityProfessorWorkingTimes_Create)]
        protected virtual async Task Create(CreateOrEditUniversityProfessorWorkingTimeDto input)
        {
            var universityProfessorWorkingTime = ObjectMapper.Map<UniversityProfessorWorkingTime>(input);

            if (AbpSession.TenantId != null)
            {
                universityProfessorWorkingTime.TenantId = (int)AbpSession.TenantId;
            }

            await _universityProfessorWorkingTimeRepository.InsertAsync(universityProfessorWorkingTime);

        }

        [AbpAuthorize(AppPermissions.Pages_UniversityProfessorWorkingTimes_Edit)]
        protected virtual async Task Update(CreateOrEditUniversityProfessorWorkingTimeDto input)
        {
            var universityProfessorWorkingTime = await _universityProfessorWorkingTimeRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, universityProfessorWorkingTime);

        }

        [AbpAuthorize(AppPermissions.Pages_UniversityProfessorWorkingTimes_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _universityProfessorWorkingTimeRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetUniversityProfessorWorkingTimesToExcel(GetAllUniversityProfessorWorkingTimesForExcelInput input)
        {

            var filteredUniversityProfessorWorkingTimes = _universityProfessorWorkingTimeRepository.GetAll()
                        .Include(e => e.UniversityProfessorFk)
                        .Include(e => e.WorkTimeInDayFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UniversityProfessorUniversityProfessorNameFilter), e => e.UniversityProfessorFk != null && e.UniversityProfessorFk.UniversityProfessorName == input.UniversityProfessorUniversityProfessorNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.WorkTimeInDayNameWorkTimeInDayFilter), e => e.WorkTimeInDayFk != null && e.WorkTimeInDayFk.NameWorkTimeInDay == input.WorkTimeInDayNameWorkTimeInDayFilter);

            var query = (from o in filteredUniversityProfessorWorkingTimes
                         join o1 in _lookup_universityProfessorRepository.GetAll() on o.UniversityProfessorId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         join o2 in _lookup_workTimeInDayRepository.GetAll() on o.WorkTimeInDayId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()

                         select new GetUniversityProfessorWorkingTimeForViewDto()
                         {
                             UniversityProfessorWorkingTime = new UniversityProfessorWorkingTimeDto
                             {
                                 Id = o.Id
                             },
                             UniversityProfessorUniversityProfessorName = s1 == null || s1.UniversityProfessorName == null ? "" : s1.UniversityProfessorName.ToString(),
                             WorkTimeInDayNameWorkTimeInDay = s2 == null || s2.NameWorkTimeInDay == null ? "" : s2.NameWorkTimeInDay.ToString()
                         });

            var universityProfessorWorkingTimeListDtos = await query.ToListAsync();

            return _universityProfessorWorkingTimesExcelExporter.ExportToFile(universityProfessorWorkingTimeListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_UniversityProfessorWorkingTimes)]
        public async Task<PagedResultDto<UniversityProfessorWorkingTimeUniversityProfessorLookupTableDto>> GetAllUniversityProfessorForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_universityProfessorRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.UniversityProfessorName != null && e.UniversityProfessorName.Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var universityProfessorList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<UniversityProfessorWorkingTimeUniversityProfessorLookupTableDto>();
            foreach (var universityProfessor in universityProfessorList)
            {
                lookupTableDtoList.Add(new UniversityProfessorWorkingTimeUniversityProfessorLookupTableDto
                {
                    Id = universityProfessor.Id,
                    DisplayName = universityProfessor.UniversityProfessorName?.ToString()
                });
            }

            return new PagedResultDto<UniversityProfessorWorkingTimeUniversityProfessorLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

        [AbpAuthorize(AppPermissions.Pages_UniversityProfessorWorkingTimes)]
        public async Task<PagedResultDto<UniversityProfessorWorkingTimeWorkTimeInDayLookupTableDto>> GetAllWorkTimeInDayForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_workTimeInDayRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.NameWorkTimeInDay != null && e.NameWorkTimeInDay.Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var workTimeInDayList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<UniversityProfessorWorkingTimeWorkTimeInDayLookupTableDto>();
            foreach (var workTimeInDay in workTimeInDayList)
            {
                lookupTableDtoList.Add(new UniversityProfessorWorkingTimeWorkTimeInDayLookupTableDto
                {
                    Id = workTimeInDay.Id,
                    DisplayName = workTimeInDay.NameWorkTimeInDay?.ToString()
                });
            }

            return new PagedResultDto<UniversityProfessorWorkingTimeWorkTimeInDayLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

    }
}