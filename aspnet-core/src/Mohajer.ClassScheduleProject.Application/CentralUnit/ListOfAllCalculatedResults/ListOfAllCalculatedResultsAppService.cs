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
using Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleResults;
using Mohajer.ClassScheduleProject.CentralUnit.ListOfClassScheduleModeSpaces;
using Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleModeSpaces;

namespace Mohajer.ClassScheduleProject.CentralUnit.ListOfAllCalculatedResults
{
    [AbpAuthorize(AppPermissions.Pages_ListOfAllCalculatedResults)]
    public class ListOfAllCalculatedResultsAppService : ClassScheduleProjectAppServiceBase, IListOfAllCalculatedResultsAppService
    {
        private readonly IRepository<ListOfAllCalculatedResult, long> _listOfAllCalculatedResultRepository;
        private readonly IRepository<ListOfClassScheduleModeSpace, long> _listOfClassScheduleModeSpaceRepository;
        private readonly IRepository<ClassScheduleModeSpace, long> _classScheduleModeSpaceRepository;
        private readonly IRepository<ClassScheduleResult, long> _classScheduleResultRepository;

        private readonly IListOfAllCalculatedResultsExcelExporter _listOfAllCalculatedResultsExcelExporter;

        public ListOfAllCalculatedResultsAppService(IRepository<ListOfAllCalculatedResult, long> listOfAllCalculatedResultRepository, IListOfAllCalculatedResultsExcelExporter listOfAllCalculatedResultsExcelExporter,
            IRepository<ClassScheduleResult, long> classScheduleResultRepository,
            IRepository<ListOfClassScheduleModeSpace, long> listOfClassScheduleModeSpaceRepository,
             IRepository<ClassScheduleModeSpace, long> classScheduleModeSpaceRepository
            )
        {
            _listOfAllCalculatedResultRepository = listOfAllCalculatedResultRepository;
            _listOfAllCalculatedResultsExcelExporter = listOfAllCalculatedResultsExcelExporter;
            _classScheduleResultRepository = classScheduleResultRepository;
            _listOfClassScheduleModeSpaceRepository = listOfClassScheduleModeSpaceRepository;
            _classScheduleModeSpaceRepository = classScheduleModeSpaceRepository;

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

        public async Task<CreateOrEditListOfAllCalculatedResultOutputDto> CreateOrEdit(CreateOrEditListOfAllCalculatedResultDto input)
        {
            if (input.Id == null)
            {
                return await Create(input);
            }
            else
            {
                return await Update(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_ListOfAllCalculatedResults_Create)]
        protected virtual async Task<CreateOrEditListOfAllCalculatedResultOutputDto> Create(CreateOrEditListOfAllCalculatedResultDto input)
        {
            var listOfAllCalculatedResult = ObjectMapper.Map<ListOfAllCalculatedResult>(input);
            listOfAllCalculatedResult.Price = 1;
            if (AbpSession.TenantId != null)
            {
                listOfAllCalculatedResult.TenantId = (int)AbpSession.TenantId;
            }

            long resultId = await _listOfAllCalculatedResultRepository.InsertAndGetIdAsync(listOfAllCalculatedResult);
            return new CreateOrEditListOfAllCalculatedResultOutputDto() { Id = resultId, IsCreated = true };
        }

        [AbpAuthorize(AppPermissions.Pages_ListOfAllCalculatedResults_Edit)]
        protected virtual async Task<CreateOrEditListOfAllCalculatedResultOutputDto> Update(CreateOrEditListOfAllCalculatedResultDto input)
        {
            var listOfAllCalculatedResult = await _listOfAllCalculatedResultRepository.GetAsync((long)input.Id);
            ObjectMapper.Map(input, listOfAllCalculatedResult);
            await _listOfAllCalculatedResultRepository.UpdateAsync(listOfAllCalculatedResult);
            return new CreateOrEditListOfAllCalculatedResultOutputDto() { Id = input.Id, IsCreated = false };
        }

        [AbpAuthorize(AppPermissions.Pages_ListOfAllCalculatedResults_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            var listModSpace = await _listOfClassScheduleModeSpaceRepository.FirstOrDefaultAsync(record => record.ListOfAllCalculatedResultId == input.Id);
            await _classScheduleModeSpaceRepository.DeleteAsync(record => record.ListOfClassScheduleModeSpaceId == listModSpace.Id);
            await _listOfClassScheduleModeSpaceRepository.DeleteAsync(listModSpace.Id);
            await _classScheduleResultRepository.DeleteAsync(record => record.ListOfAllCalculatedResultId == input.Id);
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