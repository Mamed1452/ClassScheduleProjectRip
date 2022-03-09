using System.Collections.Generic;
using Mohajer.ClassScheduleProject.CentralUnit.ListOfMainDomains.Dtos;
using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.ListOfMainDomains.Exporting
{
    public interface IListOfMainDomainsExcelExporter
    {
        FileDto ExportToFile(List<GetListOfMainDomainForViewDto> listOfMainDomains);
    }
}