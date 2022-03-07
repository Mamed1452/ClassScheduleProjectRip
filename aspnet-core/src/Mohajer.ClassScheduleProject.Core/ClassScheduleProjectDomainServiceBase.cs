using Abp.Domain.Services;

namespace Mohajer.ClassScheduleProject
{
    public abstract class ClassScheduleProjectDomainServiceBase : DomainService
    {
        /* Add your common members for all your domain services. */

        protected ClassScheduleProjectDomainServiceBase()
        {
            LocalizationSourceName = ClassScheduleProjectConsts.LocalizationSourceName;
        }
    }
}
