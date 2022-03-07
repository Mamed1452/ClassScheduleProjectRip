﻿using Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleCommonEnums;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Mohajer.ClassScheduleProject.CentralUnit.WorkTimeInDays.Exporting;
using Mohajer.ClassScheduleProject.CentralUnit.WorkTimeInDays.Dtos;
using Mohajer.ClassScheduleProject.Dto;
using Abp.Application.Services.Dto;
using Mohajer.ClassScheduleProject.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Mohajer.ClassScheduleProject.Storage;

namespace Mohajer.ClassScheduleProject.CentralUnit.WorkTimeInDays
{
    [AbpAuthorize(AppPermissions.Pages_WorkTimeInDays)]
    public class WorkTimeInDaysAppService : ClassScheduleProjectAppServiceBase, IWorkTimeInDaysAppService
    {
        private readonly IRepository<WorkTimeInDay, long> _workTimeInDayRepository;
        private readonly IWorkTimeInDaysExcelExporter _workTimeInDaysExcelExporter;

        public WorkTimeInDaysAppService(IRepository<WorkTimeInDay, long> workTimeInDayRepository, IWorkTimeInDaysExcelExporter workTimeInDaysExcelExporter)
        {
            _workTimeInDayRepository = workTimeInDayRepository;
            _workTimeInDaysExcelExporter = workTimeInDaysExcelExporter;

        }

        public async Task<PagedResultDto<GetWorkTimeInDayForViewDto>> GetAll(GetAllWorkTimeInDaysInput input)
        {
            var dayOfWeekFilter = input.DayOfWeekFilter.HasValue
                        ? (DayOfWeekEnum)input.DayOfWeekFilter
                        : default;

            var filteredWorkTimeInDays = _workTimeInDayRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.NameWorkTimeInDay.Contains(input.Filter) || e.startTime.Contains(input.Filter) || e.EndTime.Contains(input.Filter) || e.WhatTimeOfDay.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameWorkTimeInDayFilter), e => e.NameWorkTimeInDay == input.NameWorkTimeInDayFilter)
                        .WhereIf(input.DayOfWeekFilter.HasValue && input.DayOfWeekFilter > -1, e => e.DayOfWeek == dayOfWeekFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.startTimeFilter), e => e.startTime == input.startTimeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EndTimeFilter), e => e.EndTime == input.EndTimeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.WhatTimeOfDayFilter), e => e.WhatTimeOfDay == input.WhatTimeOfDayFilter);

            var pagedAndFilteredWorkTimeInDays = filteredWorkTimeInDays
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var workTimeInDays = from o in pagedAndFilteredWorkTimeInDays
                                 select new
                                 {

                                     o.NameWorkTimeInDay,
                                     o.DayOfWeek,
                                     o.startTime,
                                     o.EndTime,
                                     o.WhatTimeOfDay,
                                     Id = o.Id
                                 };

            var totalCount = await filteredWorkTimeInDays.CountAsync();

            var dbList = await workTimeInDays.ToListAsync();
            var results = new List<GetWorkTimeInDayForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetWorkTimeInDayForViewDto()
                {
                    WorkTimeInDay = new WorkTimeInDayDto
                    {

                        NameWorkTimeInDay = o.NameWorkTimeInDay,
                        DayOfWeek = o.DayOfWeek,
                        startTime = o.startTime,
                        EndTime = o.EndTime,
                        WhatTimeOfDay = o.WhatTimeOfDay,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetWorkTimeInDayForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetWorkTimeInDayForViewDto> GetWorkTimeInDayForView(long id)
        {
            var workTimeInDay = await _workTimeInDayRepository.GetAsync(id);

            var output = new GetWorkTimeInDayForViewDto { WorkTimeInDay = ObjectMapper.Map<WorkTimeInDayDto>(workTimeInDay) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_WorkTimeInDays_Edit)]
        public async Task<GetWorkTimeInDayForEditOutput> GetWorkTimeInDayForEdit(EntityDto<long> input)
        {
            var workTimeInDay = await _workTimeInDayRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetWorkTimeInDayForEditOutput { WorkTimeInDay = ObjectMapper.Map<CreateOrEditWorkTimeInDayDto>(workTimeInDay) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditWorkTimeInDayDto input)
        {
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_WorkTimeInDays_Create)]
        protected virtual async Task Create(CreateOrEditWorkTimeInDayDto input)
        {
            var workTimeInDay = ObjectMapper.Map<WorkTimeInDay>(input);

            if (AbpSession.TenantId != null)
            {
                workTimeInDay.TenantId = (int)AbpSession.TenantId;
            }

            await _workTimeInDayRepository.InsertAsync(workTimeInDay);

        }

        [AbpAuthorize(AppPermissions.Pages_WorkTimeInDays_Edit)]
        protected virtual async Task Update(CreateOrEditWorkTimeInDayDto input)
        {
            var workTimeInDay = await _workTimeInDayRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, workTimeInDay);

        }

        [AbpAuthorize(AppPermissions.Pages_WorkTimeInDays_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _workTimeInDayRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetWorkTimeInDaysToExcel(GetAllWorkTimeInDaysForExcelInput input)
        {
            var dayOfWeekFilter = input.DayOfWeekFilter.HasValue
                        ? (DayOfWeekEnum)input.DayOfWeekFilter
                        : default;

            var filteredWorkTimeInDays = _workTimeInDayRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.NameWorkTimeInDay.Contains(input.Filter) || e.startTime.Contains(input.Filter) || e.EndTime.Contains(input.Filter) || e.WhatTimeOfDay.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameWorkTimeInDayFilter), e => e.NameWorkTimeInDay == input.NameWorkTimeInDayFilter)
                        .WhereIf(input.DayOfWeekFilter.HasValue && input.DayOfWeekFilter > -1, e => e.DayOfWeek == dayOfWeekFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.startTimeFilter), e => e.startTime == input.startTimeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EndTimeFilter), e => e.EndTime == input.EndTimeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.WhatTimeOfDayFilter), e => e.WhatTimeOfDay == input.WhatTimeOfDayFilter);

            var query = (from o in filteredWorkTimeInDays
                         select new GetWorkTimeInDayForViewDto()
                         {
                             WorkTimeInDay = new WorkTimeInDayDto
                             {
                                 NameWorkTimeInDay = o.NameWorkTimeInDay,
                                 DayOfWeek = o.DayOfWeek,
                                 startTime = o.startTime,
                                 EndTime = o.EndTime,
                                 WhatTimeOfDay = o.WhatTimeOfDay,
                                 Id = o.Id
                             }
                         });

            var workTimeInDayListDtos = await query.ToListAsync();

            return _workTimeInDaysExcelExporter.ExportToFile(workTimeInDayListDtos);
        }

    }
}