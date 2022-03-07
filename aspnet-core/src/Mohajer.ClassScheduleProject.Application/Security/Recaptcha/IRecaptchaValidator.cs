using System.Threading.Tasks;

namespace Mohajer.ClassScheduleProject.Security.Recaptcha
{
    public interface IRecaptchaValidator
    {
        Task ValidateAsync(string captchaResponse);
    }
}