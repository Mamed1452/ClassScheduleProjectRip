using System.Globalization;

namespace Mohajer.ClassScheduleProject.Localization
{
    public interface IApplicationCulturesProvider
    {
        CultureInfo[] GetAllCultures();
    }
}