using Mohajer.ClassScheduleProject.ClassScheduleProjectAddition.Localization;
using System.Collections.Concurrent;
using System.Globalization;
using System.Threading;

namespace Mohajer.ClassScheduleProject
{
    public class ToGeorgianCultureInfoConverter : IToGeorgianCultureInfoConverter
    {
        private const string TargetLanguageName = "";

        private static readonly object _targetCultureCreateLock = new object();
        private static CultureInfo _targetCultureInfo;

        private static CultureInfo TargetCultureInfo
        {
            get
            {
                if (_targetCultureInfo != null)
                    return _targetCultureInfo;

                lock (_targetCultureCreateLock)
                {
                    return _targetCultureInfo ??= CultureInfo.InvariantCulture;
                }
            }
        }


        public ToGeorgianCultureInfoConverter()
        {

        }

        public void SetTargetCalenderOnCurrentThread()
        {
            var newCultureInfo = GetCultureWithGeorgianCalenderOrTarget(Thread.CurrentThread.CurrentCulture);

            Thread.CurrentThread.CurrentCulture = newCultureInfo;
            Thread.CurrentThread.CurrentUICulture = newCultureInfo;
        }

        private ConcurrentDictionary<string, CultureInfo> previousCultureInfos;

        public CultureInfo GetCultureWithGeorgianCalenderOrTarget(CultureInfo sourceCultureInfo = null)
        {
            if (sourceCultureInfo == null || sourceCultureInfo.Name == TargetLanguageName)
                return TargetCultureInfo;

            previousCultureInfos ??= new ConcurrentDictionary<string, CultureInfo>();//init if necessary

            return previousCultureInfos.GetOrAdd(sourceCultureInfo.Name,
                (sourceCultureInfoName) =>
                {

                    CultureInfo mainCultureClone =
                        CultureInfo.GetCultureInfo(sourceCultureInfoName).Clone() as CultureInfo;

                    DateTimeFormatInfo originalDateTimeFormat = mainCultureClone.DateTimeFormat;

                    mainCultureClone.DateTimeFormat =
                        TargetCultureInfo.DateTimeFormat.Clone() as DateTimeFormatInfo;

                    //Restore original day names and week start day
                    mainCultureClone.DateTimeFormat.FirstDayOfWeek = originalDateTimeFormat.FirstDayOfWeek;
                    mainCultureClone.DateTimeFormat.AbbreviatedDayNames = originalDateTimeFormat.AbbreviatedDayNames;
                    mainCultureClone.DateTimeFormat.DayNames = originalDateTimeFormat.DayNames;

                    return mainCultureClone;
                });

        }
    }
}

