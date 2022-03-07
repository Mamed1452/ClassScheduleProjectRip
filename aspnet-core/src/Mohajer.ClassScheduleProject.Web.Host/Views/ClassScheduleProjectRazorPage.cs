using Abp.AspNetCore.Mvc.Views;

namespace Mohajer.ClassScheduleProject.Web.Views
{
    public abstract class ClassScheduleProjectRazorPage<TModel> : AbpRazorPage<TModel>
    {
        protected ClassScheduleProjectRazorPage()
        {
            LocalizationSourceName = ClassScheduleProjectConsts.LocalizationSourceName;
        }
    }
}
