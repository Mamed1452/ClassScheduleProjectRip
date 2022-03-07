using System.Threading.Tasks;
using Mohajer.ClassScheduleProject.Views;
using Xamarin.Forms;

namespace Mohajer.ClassScheduleProject.Services.Modal
{
    public interface IModalService
    {
        Task ShowModalAsync(Page page);

        Task ShowModalAsync<TView>(object navigationParameter) where TView : IXamarinView;

        Task<Page> CloseModalAsync();
    }
}
