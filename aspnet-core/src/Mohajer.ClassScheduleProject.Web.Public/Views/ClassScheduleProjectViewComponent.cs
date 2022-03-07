using Abp.AspNetCore.Mvc.ViewComponents;

namespace Mohajer.ClassScheduleProject.Web.Public.Views
{
    public abstract class ClassScheduleProjectViewComponent : AbpViewComponent
    {
        protected ClassScheduleProjectViewComponent()
        {
            LocalizationSourceName = ClassScheduleProjectConsts.LocalizationSourceName;
        }
    }
}