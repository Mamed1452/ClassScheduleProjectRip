using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Abp.Dependency;

namespace Mohajer.ClassScheduleProject.ClassScheduleProjectAddition.Localization
{
    public interface IToGeorgianCultureInfoConverter : ISingletonDependency
    {
        CultureInfo GetCultureWithGeorgianCalenderOrTarget(CultureInfo sourceCultureInfo = null);

        void SetTargetCalenderOnCurrentThread();
    }
}
