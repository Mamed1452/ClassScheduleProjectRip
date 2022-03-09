using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Mohajer.ClassScheduleProject.CentralUnit.ListOfMainDomains.Dtos;
using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.ListOfMainDomains
{
    public interface IListOfMainDomainsAppService : IApplicationService
    {
        Task<PagedResultDto<GetListOfMainDomainForViewDto>> GetAll(GetAllListOfMainDomainsInput input);

        Task<GetListOfMainDomainForViewDto> GetListOfMainDomainForView(long id);

        Task<GetListOfMainDomainForEditOutput> GetListOfMainDomainForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditListOfMainDomainDto input);

        Task Delete(EntityDto<long> input);

        Task<FileDto> GetListOfMainDomainsToExcel(GetAllListOfMainDomainsForExcelInput input);

    }
}