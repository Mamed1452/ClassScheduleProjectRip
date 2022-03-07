using System.Collections.Generic;
using Mohajer.ClassScheduleProject.Authorization.Users.Importing.Dto;
using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.Authorization.Users.Importing
{
    public interface IInvalidUserExporter
    {
        FileDto ExportToFile(List<ImportUserDto> userListDtos);
    }
}
