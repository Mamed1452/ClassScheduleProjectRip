using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Mohajer.ClassScheduleProject.CentralUnit.ListOfAllCalculatedResults.Exporting;
using Mohajer.ClassScheduleProject.CentralUnit.ListOfAllCalculatedResults.Dtos;
using Mohajer.ClassScheduleProject.Dto;
using Abp.Application.Services.Dto;
using Mohajer.ClassScheduleProject.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Mohajer.ClassScheduleProject.Storage;

namespace Mohajer.ClassScheduleProject.CentralUnit.ListOfAllCalculatedResults
{
    [AbpAuthorize(AppPermissions.Pages_ListOfAllCalculatedResults)]
    public class ListOfAllCalculatedResultsAppService : ClassScheduleProjectAppServiceBase, IListOfAllCalculatedResultsAppService
    {
        private readonly IRepository<ListOfAllCalculatedResult, long> _listOfAllCalculatedResultRepository;
        private readonly IListOfAllCalculatedResultsExcelExporter _listOfAllCalculatedResultsExcelExporter;

        public ListOfAllCalculatedResultsAppService(IRepository<ListOfAllCalculatedResult, long> listOfAllCalculatedResultRepository, IListOfAllCalculatedResultsExcelExporter listOfAllCalculatedResultsExcelExporter)
        {
            _listOfAllCalculatedResultRepository = listOfAllCalculatedResultRepository;
            _listOfAllCalculatedResultsExcelExporter = listOfAllCalculatedResultsExcelExporter;

        }

        public async Task<PagedResultDto<GetListOfAllCalculatedResultForViewDto>> GetAll(GetAllListOfAllCalculatedResultsInput input)
        {

            var filteredListOfAllCalculatedResults = _listOfAllCalculatedResultRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.NameCalculatedResult.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameCalculatedResultFilter), e => e.NameCalculatedResult == input.NameCalculatedResultFilter)
                        .WhereIf(input.MinPriceFilter != null, e => e.Price >= input.MinPriceFilter)
                        .WhereIf(input.MaxPriceFilter != null, e => e.Price <= input.MaxPriceFilter);

            var pagedAndFilteredListOfAllCalculatedResults = filteredListOfAllCalculatedResults
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var listOfAllCalculatedResults = from o in pagedAndFilteredListOfAllCalculatedResults
                                             select new
                                             {

                                                 o.NameCalculatedResult,
                                                 o.Price,
                                                 Id = o.Id
                                             };

            var totalCount = await filteredListOfAllCalculatedResults.CountAsync();

            var dbList = await listOfAllCalculatedResults.ToListAsync();
            var results = new List<GetListOfAllCalculatedResultForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetListOfAllCalculatedResultForViewDto()
                {
                    ListOfAllCalculatedResult = new ListOfAllCalculatedResultDto
                    {

                        NameCalculatedResult = o.NameCalculatedResult,
                        Price = o.Price,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetListOfAllCalculatedResultForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetListOfAllCalculatedResultForViewDto> GetListOfAllCalculatedResultForView(long id)
        {
            var listOfAllCalculatedResult = await _listOfAllCalculatedResultRepository.GetAsync(id);

            var output = new GetListOfAllCalculatedResultForViewDto { ListOfAllCalculatedResult = ObjectMapper.Map<ListOfAllCalculatedResultDto>(listOfAllCalculatedResult) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_ListOfAllCalculatedResults_Edit)]
        public async Task<GetListOfAllCalculatedResultForEditOutput> GetListOfAllCalculatedResultForEdit(EntityDto<long> input)
        {
            var listOfAllCalculatedResult = await _listOfAllCalculatedResultRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetListOfAllCalculatedResultForEditOutput { ListOfAllCalculatedResult = ObjectMapper.Map<CreateOrEditListOfAllCalculatedResultDto>(listOfAllCalculatedResult) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditListOfAllCalculatedResultDto input)
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

        [AbpAuthorize(AppPermissions.Pages_ListOfAllCalculatedResults_Create)]
        protected virtual async Task Create(CreateOrEditListOfAllCalculatedResultDto input)
        {
            var listOfAllCalculatedResult = ObjectMapper.Map<ListOfAllCalculatedResult>(input);

            if (AbpSession.TenantId != null)
            {
                listOfAllCalculatedResult.TenantId = (int)AbpSession.TenantId;
            }

            await _listOfAllCalculatedResultRepository.InsertAsync(listOfAllCalculatedResult);

        }

        [AbpAuthorize(AppPermissions.Pages_ListOfAllCalculatedResults_Edit)]
        protected virtual async Task Update(CreateOrEditListOfAllCalculatedResultDto input)
        {
            var listOfAllCalculatedResult = await _listOfAllCalculatedResultRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, listOfAllCalculatedResult);

        }

        [AbpAuthorize(AppPermissions.Pages_ListOfAllCalculatedResults_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _listOfAllCalculatedResultRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetListOfAllCalculatedResultsToExcel(GetAllListOfAllCalculatedResultsForExcelInput input)
        {

            var filteredListOfAllCalculatedResults = _listOfAllCalculatedResultRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.NameCalculatedResult.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameCalculatedResultFilter), e => e.NameCalculatedResult == input.NameCalculatedResultFilter)
                        .WhereIf(input.MinPriceFilter != null, e => e.Price >= input.MinPriceFilter)
                        .WhereIf(input.MaxPriceFilter != null, e => e.Price <= input.MaxPriceFilter);

            var query = (from o in filteredListOfAllCalculatedResults
                         select new GetListOfAllCalculatedResultForViewDto()
                         {
                             ListOfAllCalculatedResult = new ListOfAllCalculatedResultDto
                             {
                                 NameCalculatedResult = o.NameCalculatedResult,
                                 Price = o.Price,
                                 Id = o.Id
                             }
                         });

            var listOfAllCalculatedResultListDtos = await query.ToListAsync();

            return _listOfAllCalculatedResultsExcelExporter.ExportToFile(listOfAllCalculatedResultListDtos);
        }

    }
}