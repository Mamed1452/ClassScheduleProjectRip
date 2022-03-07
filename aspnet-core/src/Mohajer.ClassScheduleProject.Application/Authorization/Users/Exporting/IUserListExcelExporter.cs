using System.Collections.Generic;
using Mohajer.ClassScheduleProject.Authorization.Users.Dto;
using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.Authorization.Users.Exporting
{
    public interface IUserListExcelExporter
    {
        FileDto ExportToFile(List<UserListDto> userListDtos);
    }
}