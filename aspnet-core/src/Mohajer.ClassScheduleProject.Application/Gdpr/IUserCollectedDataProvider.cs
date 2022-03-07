using System.Collections.Generic;
using System.Threading.Tasks;
using Abp;
using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.Gdpr
{
    public interface IUserCollectedDataProvider
    {
        Task<List<FileDto>> GetFiles(UserIdentifier user);
    }
}
