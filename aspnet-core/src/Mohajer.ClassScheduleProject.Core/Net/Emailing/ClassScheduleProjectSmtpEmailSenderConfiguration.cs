using Abp.Configuration;
using Abp.Net.Mail;
using Abp.Net.Mail.Smtp;
using Abp.Runtime.Security;

namespace Mohajer.ClassScheduleProject.Net.Emailing
{
    public class ClassScheduleProjectSmtpEmailSenderConfiguration : SmtpEmailSenderConfiguration
    {
        public ClassScheduleProjectSmtpEmailSenderConfiguration(ISettingManager settingManager) : base(settingManager)
        {

        }

        public override string Password => SimpleStringCipher.Instance.Decrypt(GetNotEmptySettingValue(EmailSettingNames.Smtp.Password));
    }
}