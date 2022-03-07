using Mohajer.ClassScheduleProject.Models.Tenants;
using Mohajer.ClassScheduleProject.ViewModels;
using Xamarin.Forms;

namespace Mohajer.ClassScheduleProject.Views
{
    public partial class TenantsView : ContentPage, IXamarinView
    {
        public TenantsView()
        {
            InitializeComponent();
        }

        private async void ListView_OnItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            await ((TenantsViewModel)BindingContext).LoadMoreTenantsIfNeedsAsync(e.Item as TenantListModel);
        }
    }
}