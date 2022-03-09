using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Mohajer.ClassScheduleProject.CentralUnit.ListOfMainDomains.Exporting;
using Mohajer.ClassScheduleProject.CentralUnit.ListOfMainDomains.Dtos;
using Mohajer.ClassScheduleProject.Dto;
using Abp.Application.Services.Dto;
using Mohajer.ClassScheduleProject.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Mohajer.ClassScheduleProject.Storage;

namespace Mohajer.ClassScheduleProject.CentralUnit.ListOfMainDomains
{
    [AbpAuthorize(AppPermissions.Pages_ListOfMainDomains)]
    public class ListOfMainDomainsAppService : ClassScheduleProjectAppServiceBase, IListOfMainDomainsAppService
    {
        private readonly IRepository<ListOfMainDomain, long> _listOfMainDomainRepository;
        private readonly IListOfMainDomainsExcelExporter _listOfMainDomainsExcelExporter;

        public ListOfMainDomainsAppService(IRepository<ListOfMainDomain, long> listOfMainDomainRepository, IListOfMainDomainsExcelExporter listOfMainDomainsExcelExporter)
        {
            _listOfMainDomainRepository = listOfMainDomainRepository;
            _listOfMainDomainsExcelExporter = listOfMainDomainsExcelExporter;

        }

        public async Task<PagedResultDto<GetListOfMainDomainForViewDto>> GetAll(GetAllListOfMainDomainsInput input)
        {

            var filteredListOfMainDomains = _listOfMainDomainRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.ListOfMainDomainName.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ListOfMainDomainNameFilter), e => e.ListOfMainDomainName == input.ListOfMainDomainNameFilter);

            var pagedAndFilteredListOfMainDomains = filteredListOfMainDomains
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var listOfMainDomains = from o in pagedAndFilteredListOfMainDomains
                                    select new
                                    {

                                        o.ListOfMainDomainName,
                                        Id = o.Id
                                    };

            var totalCount = await filteredListOfMainDomains.CountAsync();

            var dbList = await listOfMainDomains.ToListAsync();
            var results = new List<GetListOfMainDomainForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetListOfMainDomainForViewDto()
                {
                    ListOfMainDomain = new ListOfMainDomainDto
                    {

                        ListOfMainDomainName = o.ListOfMainDomainName,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetListOfMainDomainForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetListOfMainDomainForViewDto> GetListOfMainDomainForView(long id)
        {
            var listOfMainDomain = await _listOfMainDomainRepository.GetAsync(id);

            var output = new GetListOfMainDomainForViewDto { ListOfMainDomain = ObjectMapper.Map<ListOfMainDomainDto>(listOfMainDomain) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_ListOfMainDomains_Edit)]
        public async Task<GetListOfMainDomainForEditOutput> GetListOfMainDomainForEdit(EntityDto<long> input)
        {
            var listOfMainDomain = await _listOfMainDomainRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetListOfMainDomainForEditOutput { ListOfMainDomain = ObjectMapper.Map<CreateOrEditListOfMainDomainDto>(listOfMainDomain) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditListOfMainDomainDto input)
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

        [AbpAuthorize(AppPermissions.Pages_ListOfMainDomains_Create)]
        protected virtual async Task Create(CreateOrEditListOfMainDomainDto input)
        {
            var listOfMainDomain = ObjectMapper.Map<ListOfMainDomain>(input);

            if (AbpSession.TenantId != null)
            {
                listOfMainDomain.TenantId = (int)AbpSession.TenantId;
            }

            await _listOfMainDomainRepository.InsertAsync(listOfMainDomain);

        }

        [AbpAuthorize(AppPermissions.Pages_ListOfMainDomains_Edit)]
        protected virtual async Task Update(CreateOrEditListOfMainDomainDto input)
        {
            var listOfMainDomain = await _listOfMainDomainRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, listOfMainDomain);

        }

        [AbpAuthorize(AppPermissions.Pages_ListOfMainDomains_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _listOfMainDomainRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetListOfMainDomainsToExcel(GetAllListOfMainDomainsForExcelInput input)
        {

            var filteredListOfMainDomains = _listOfMainDomainRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.ListOfMainDomainName.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ListOfMainDomainNameFilter), e => e.ListOfMainDomainName == input.ListOfMainDomainNameFilter);

            var query = (from o in filteredListOfMainDomains
                         select new GetListOfMainDomainForViewDto()
                         {
                             ListOfMainDomain = new ListOfMainDomainDto
                             {
                                 ListOfMainDomainName = o.ListOfMainDomainName,
                                 Id = o.Id
                             }
                         });

            var listOfMainDomainListDtos = await query.ToListAsync();

            return _listOfMainDomainsExcelExporter.ExportToFile(listOfMainDomainListDtos);
        }

    }
}