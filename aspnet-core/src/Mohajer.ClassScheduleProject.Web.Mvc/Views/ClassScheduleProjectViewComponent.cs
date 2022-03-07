using Abp.AspNetCore.Mvc.ViewComponents;

namespace Mohajer.ClassScheduleProject.Web.Views
{
    public abstract class ClassScheduleProjectViewComponent : AbpViewComponent
    {
        protected ClassScheduleProjectViewComponent()
        {
            LocalizationSourceName = ClassScheduleProjectConsts.LocalizationSourceName;
        }
    }
}