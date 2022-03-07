using System.Threading.Tasks;

namespace Mohajer.ClassScheduleProject.Net.Sms
{
    public interface ISmsSender
    {
        Task SendAsync(string number, string message);
    }
}