﻿using System;
using Abp.Application.Services.Dto;

namespace Mohajer.ClassScheduleProject.Authorization.Users.Delegation.Dto
{
    public class UserDelegationDto : EntityDto<long>
    {
        public string Username { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}