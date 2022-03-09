using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Mohajer.ClassScheduleProject.CentralUnit.MainDomains.Dtos;
using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.MainDomains
{
    public interface IMainDomainsAppService : IApplicationService
    {
        Task<PagedResultDto<GetMainDomainForViewDto>> GetAll(GetAllMainDomainsInput input);

        Task<GetMainDomainForViewDto> GetMainDomainForView(long id);

        Task<GetMainDomainForEditOutput> GetMainDomainForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditMainDomainDto input);

        Task Delete(EntityDto<long> input);

        Task<FileDto> GetMainDomainsToExcel(GetAllMainDomainsForExcelInput input);

        Task<PagedResultDto<MainDomainListOfMainDomainLookupTableDto>> GetAllListOfMainDomainForLookupTable(GetAllForLookupTableInput input);

        Task<PagedResultDto<MainDomainLessonsOfSemesterLookupTableDto>> GetAllLessonsOfSemesterForLookupTable(GetAllForLookupTableInput input);

    }
}