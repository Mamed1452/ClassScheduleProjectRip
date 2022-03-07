using Mohajer.ClassScheduleProject.Models.Users;
using Mohajer.ClassScheduleProject.ViewModels;
using Xamarin.Forms;

namespace Mohajer.ClassScheduleProject.Views
{
    public partial class UsersView : ContentPage, IXamarinView
    {
        public UsersView()
        {
            InitializeComponent();
        }

        public async void ListView_OnItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            await ((UsersViewModel) BindingContext).LoadMoreUserIfNeedsAsync(e.Item as UserListModel);
        }
    }
}