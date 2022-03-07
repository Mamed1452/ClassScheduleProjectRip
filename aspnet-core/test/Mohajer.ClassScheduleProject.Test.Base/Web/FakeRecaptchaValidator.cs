using System.Threading.Tasks;
using Mohajer.ClassScheduleProject.Security.Recaptcha;

namespace Mohajer.ClassScheduleProject.Test.Base.Web
{
    public class FakeRecaptchaValidator : IRecaptchaValidator
    {
        public Task ValidateAsync(string captchaResponse)
        {
            return Task.CompletedTask;
        }
    }
}
