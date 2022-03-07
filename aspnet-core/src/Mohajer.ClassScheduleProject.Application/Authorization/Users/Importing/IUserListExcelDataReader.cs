using System.Collections.Generic;
using Mohajer.ClassScheduleProject.Authorization.Users.Importing.Dto;
using Abp.Dependency;

namespace Mohajer.ClassScheduleProject.Authorization.Users.Importing
{
    public interface IUserListExcelDataReader: ITransientDependency
    {
        List<ImportUserDto> GetUsersFromExcel(byte[] fileBytes);
    }
}
