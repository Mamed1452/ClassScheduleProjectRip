namespace Mohajer.ClassScheduleProject.Configuration
{
    public interface IAppConfigurationWriter
    {
        void Write(string key, string value);
    }
}
