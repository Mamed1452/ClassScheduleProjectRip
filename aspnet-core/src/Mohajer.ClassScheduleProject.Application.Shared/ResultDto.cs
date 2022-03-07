using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Mohajer.ClassScheduleProject.Shared;

namespace Mohajer.ClassScheduleProject
{
   public class ApiResult
   {
        public bool IsSuccess { get; set; }

        public ApiResultStatusCode StatusCode { get; set; }

        /// <summary>
        /// به هر دلیلی نال بود توی خروجی نمایش داده نشود
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

   }
    public class ApiResult<T>
    {
        public bool IsSuccess { get; set; }

        public ApiResultStatusCode StatusCode { get; set; }

        /// <summary>
        /// به هر دلیلی نال بود توی خروجی نمایش داده نشود
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        public T Data { get; set; }
    }
}
