using System;
using System.Collections.Generic;
using System.Text;

namespace Mohajer.ClassScheduleProject.Dto
{
    public class ExtensionPagedResultDto<T> : AdditionPagedResultDto<T>
    {
        public List<string> TableFields { get; set; }

        public ExtensionPagedResultDto(int totalCount, IReadOnlyList<T> items, string customerName, List<string> tableFields) :
            base(totalCount, items, customerName)
            => TableFields = tableFields;
    }
}
