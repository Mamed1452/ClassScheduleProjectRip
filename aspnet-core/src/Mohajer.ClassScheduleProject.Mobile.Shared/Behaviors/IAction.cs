using Xamarin.Forms.Internals;

namespace Mohajer.ClassScheduleProject.Behaviors
{
    [Preserve(AllMembers = true)]
    public interface IAction
    {
        bool Execute(object sender, object parameter);
    }
}