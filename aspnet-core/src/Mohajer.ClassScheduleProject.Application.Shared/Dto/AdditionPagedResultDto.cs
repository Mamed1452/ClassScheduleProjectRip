using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace Mohajer.ClassScheduleProject.Dto
{
    public class AdditionPagedResultDto<T> : PagedResultDto<T>
    {
        public string CustomerName { get; set; }

        public AdditionPagedResultDto(int totalCount, IReadOnlyList<T> items, string customerName) :
            base(totalCount, items)
            => CustomerName = customerName;
    }
}
