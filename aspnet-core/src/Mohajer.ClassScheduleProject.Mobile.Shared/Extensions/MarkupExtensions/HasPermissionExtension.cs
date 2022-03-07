using System;
using Mohajer.ClassScheduleProject.Core;
using Mohajer.ClassScheduleProject.Core.Dependency;
using Mohajer.ClassScheduleProject.Services.Permission;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mohajer.ClassScheduleProject.Extensions.MarkupExtensions
{
    [ContentProperty("Text")]
    public class HasPermissionExtension : IMarkupExtension
    {
        public string Text { get; set; }
        
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (ApplicationBootstrapper.AbpBootstrapper == null || Text == null)
            {
                return false;
            }

            var permissionService = DependencyResolver.Resolve<IPermissionService>();
            return permissionService.HasPermission(Text);
        }
    }
}