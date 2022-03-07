using System.Reflection;
using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace Mohajer.ClassScheduleProject.Localization
{
    public static class ClassScheduleProjectLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    ClassScheduleProjectConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(ClassScheduleProjectLocalizationConfigurer).GetAssembly(),
                        "Mohajer.ClassScheduleProject.Localization.ClassScheduleProject"
                    )
                )
            );
        }
    }
}