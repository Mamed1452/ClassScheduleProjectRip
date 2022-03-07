using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace Mohajer.ClassScheduleProject.Web.Public.Views
{
    public abstract class ClassScheduleProjectRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected ClassScheduleProjectRazorPage()
        {
            LocalizationSourceName = ClassScheduleProjectConsts.LocalizationSourceName;
        }
    }
}
