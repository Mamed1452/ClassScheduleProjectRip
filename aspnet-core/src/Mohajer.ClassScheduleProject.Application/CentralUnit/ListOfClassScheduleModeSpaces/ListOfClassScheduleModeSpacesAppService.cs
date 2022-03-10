using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Mohajer.ClassScheduleProject.CentralUnit.ListOfClassScheduleModeSpaces.Exporting;
using Mohajer.ClassScheduleProject.CentralUnit.ListOfClassScheduleModeSpaces.Dtos;
using Mohajer.ClassScheduleProject.Dto;
using Abp.Application.Services.Dto;
using Mohajer.ClassScheduleProject.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Mohajer.ClassScheduleProject.Storage;

namespace Mohajer.ClassScheduleProject.CentralUnit.ListOfClassScheduleModeSpaces
{
    [AbpAuthorize(AppPermissions.Pages_ListOfClassScheduleModeSpaces)]
    public class ListOfClassScheduleModeSpacesAppService : ClassScheduleProjectAppServiceBase, IListOfClassScheduleModeSpacesAppService
    {
        private readonly IRepository<ListOfClassScheduleModeSpace, long> _listOfClassScheduleModeSpaceRepository;
        private readonly IListOfClassScheduleModeSpacesExcelExporter _listOfClassScheduleModeSpacesExcelExporter;

        public ListOfClassScheduleModeSpacesAppService(IRepository<ListOfClassScheduleModeSpace, long> listOfClassScheduleModeSpaceRepository, IListOfClassScheduleModeSpacesExcelExporter listOfClassScheduleModeSpacesExcelExporter)
        {
            _listOfClassScheduleModeSpaceRepository = listOfClassScheduleModeSpaceRepository;
            _listOfClassScheduleModeSpacesExcelExporter = listOfClassScheduleModeSpacesExcelExporter;

        }

        public async Task<PagedResultDto<GetListOfClassScheduleModeSpaceForViewDto>> GetAll(GetAllListOfClassScheduleModeSpacesInput input)
        {

            var filteredListOfClassScheduleModeSpaces = _listOfClassScheduleModeSpaceRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.ListOfClassScheduleModeSpaceName.Contains(input.Filter));

            var pagedAndFilteredListOfClassScheduleModeSpaces = filteredListOfClassScheduleModeSpaces
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var listOfClassScheduleModeSpaces = from o in pagedAndFilteredListOfClassScheduleModeSpaces
                                                select new
                                                {

                                                    Id = o.Id
                                                };

            var totalCount = await filteredListOfClassScheduleModeSpaces.CountAsync();

            var dbList = await listOfClassScheduleModeSpaces.ToListAsync();
            var results = new List<GetListOfClassScheduleModeSpaceForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetListOfClassScheduleModeSpaceForViewDto()
                {
                    ListOfClassScheduleModeSpace = new ListOfClassScheduleModeSpaceDto
                    {

                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetListOfClassScheduleModeSpaceForViewDto>(
                totalCount,
                results
            );

        }

        [AbpAuthorize(AppPermissions.Pages_ListOfClassScheduleModeSpaces_Edit)]
        public async Task<GetListOfClassScheduleModeSpaceForEditOutput> GetListOfClassScheduleModeSpaceForEdit(EntityDto<long> input)
        {
            var listOfClassScheduleModeSpace = await _listOfClassScheduleModeSpaceRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetListOfClassScheduleModeSpaceForEditOutput { ListOfClassScheduleModeSpace = ObjectMapper.Map<CreateOrEditListOfClassScheduleModeSpaceDto>(listOfClassScheduleModeSpace) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditListOfClassScheduleModeSpaceDto input)
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

        [AbpAuthorize(AppPermissions.Pages_ListOfClassScheduleModeSpaces_Create)]
        protected virtual async Task Create(CreateOrEditListOfClassScheduleModeSpaceDto input)
        {
            var listOfClassScheduleModeSpace = ObjectMapper.Map<ListOfClassScheduleModeSpace>(input);
            if (AbpSession.TenantId != null)
            {
                listOfClassScheduleModeSpace.TenantId = (int)AbpSession.TenantId;
            }

            await _listOfClassScheduleModeSpaceRepository.InsertAsync(listOfClassScheduleModeSpace);

        }

        [AbpAuthorize(AppPermissions.Pages_ListOfClassScheduleModeSpaces_Edit)]
        protected virtual async Task Update(CreateOrEditListOfClassScheduleModeSpaceDto input)
        {
            var listOfClassScheduleModeSpace = await _listOfClassScheduleModeSpaceRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, listOfClassScheduleModeSpace);

        }

        [AbpAuthorize(AppPermissions.Pages_ListOfClassScheduleModeSpaces_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _listOfClassScheduleModeSpaceRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetListOfClassScheduleModeSpacesToExcel(GetAllListOfClassScheduleModeSpacesForExcelInput input)
        {

            var filteredListOfClassScheduleModeSpaces = _listOfClassScheduleModeSpaceRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.ListOfClassScheduleModeSpaceName.Contains(input.Filter));

            var query = (from o in filteredListOfClassScheduleModeSpaces
                         select new GetListOfClassScheduleModeSpaceForViewDto()
                         {
                             ListOfClassScheduleModeSpace = new ListOfClassScheduleModeSpaceDto
                             {
                                 Id = o.Id
                             }
                         });

            var listOfClassScheduleModeSpaceListDtos = await query.ToListAsync();

            return _listOfClassScheduleModeSpacesExcelExporter.ExportToFile(listOfClassScheduleModeSpaceListDtos);
        }

    }
}