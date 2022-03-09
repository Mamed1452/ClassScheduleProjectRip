using System.Collections.Generic;
using Mohajer.ClassScheduleProject.CentralUnit.MainDomains.Dtos;
using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.MainDomains.Exporting
{
    public interface IMainDomainsExcelExporter
    {
        FileDto ExportToFile(List<GetMainDomainForViewDto> mainDomains);
    }
}