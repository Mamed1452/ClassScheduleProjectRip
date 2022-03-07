using Abp.Dependency;
using GraphQL.Types;
using GraphQL.Utilities;
using Mohajer.ClassScheduleProject.Queries.Container;
using System;

namespace Mohajer.ClassScheduleProject.Schemas
{
    public class MainSchema : Schema, ITransientDependency
    {
        public MainSchema(IServiceProvider provider) :
            base(provider)
        {
            Query = provider.GetRequiredService<QueryContainer>();
        }
    }
}