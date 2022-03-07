using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Mohajer.ClassScheduleProject.CentralUnit.Grades.Exporting;
using Mohajer.ClassScheduleProject.CentralUnit.Grades.Dtos;
using Mohajer.ClassScheduleProject.Dto;
using Abp.Application.Services.Dto;
using Mohajer.ClassScheduleProject.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Mohajer.ClassScheduleProject.Storage;

namespace Mohajer.ClassScheduleProject.CentralUnit.Grades
{
    [AbpAuthorize(AppPermissions.Pages_Grades)]
    public class GradesAppService : ClassScheduleProjectAppServiceBase, IGradesAppService
    {
        private readonly IRepository<Grade> _gradeRepository;
        private readonly IGradesExcelExporter _gradesExcelExporter;

        public GradesAppService(IRepository<Grade> gradeRepository, IGradesExcelExporter gradesExcelExporter)
        {
            _gradeRepository = gradeRepository;
            _gradesExcelExporter = gradesExcelExporter;

        }

        public async Task<PagedResultDto<GetGradeForViewDto>> GetAll(GetAllGradesInput input)
        {

            var filteredGrades = _gradeRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.GradeName.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GradeNameFilter), e => e.GradeName == input.GradeNameFilter);

            var pagedAndFilteredGrades = filteredGrades
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var grades = from o in pagedAndFilteredGrades
                         select new
                         {

                             o.GradeName,
                             Id = o.Id
                         };

            var totalCount = await filteredGrades.CountAsync();

            var dbList = await grades.ToListAsync();
            var results = new List<GetGradeForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetGradeForViewDto()
                {
                    Grade = new GradeDto
                    {

                        GradeName = o.GradeName,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetGradeForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetGradeForViewDto> GetGradeForView(int id)
        {
            var grade = await _gradeRepository.GetAsync(id);

            var output = new GetGradeForViewDto { Grade = ObjectMapper.Map<GradeDto>(grade) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Grades_Edit)]
        public async Task<GetGradeForEditOutput> GetGradeForEdit(EntityDto input)
        {
            var grade = await _gradeRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetGradeForEditOutput { Grade = ObjectMapper.Map<CreateOrEditGradeDto>(grade) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditGradeDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Grades_Create)]
        protected virtual async Task Create(CreateOrEditGradeDto input)
        {
            var grade = ObjectMapper.Map<Grade>(input);

            if (AbpSession.TenantId != null)
            {
                grade.TenantId = (int)AbpSession.TenantId;
            }

            await _gradeRepository.InsertAsync(grade);

        }

        [AbpAuthorize(AppPermissions.Pages_Grades_Edit)]
        protected virtual async Task Update(CreateOrEditGradeDto input)
        {
            var grade = await _gradeRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, grade);

        }

        [AbpAuthorize(AppPermissions.Pages_Grades_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _gradeRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetGradesToExcel(GetAllGradesForExcelInput input)
        {

            var filteredGrades = _gradeRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.GradeName.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GradeNameFilter), e => e.GradeName == input.GradeNameFilter);

            var query = (from o in filteredGrades
                         select new GetGradeForViewDto()
                         {
                             Grade = new GradeDto
                             {
                                 GradeName = o.GradeName,
                                 Id = o.Id
                             }
                         });

            var gradeListDtos = await query.ToListAsync();

            return _gradesExcelExporter.ExportToFile(gradeListDtos);
        }

    }
}