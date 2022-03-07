using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace Mohajer.ClassScheduleProject.Dto
{
    public class AdditionListDto <T>
    {
        public bool IsEmpty { get;}

        public ListResultDto<T> List { get; }

        public AdditionListDto(ListResultDto<T> list, bool isEmpty)
        {
            this.List = list;
            this.IsEmpty = isEmpty;
        }
    }
}
