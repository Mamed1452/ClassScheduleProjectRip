using Mohajer.ClassScheduleProject.CentralUnit.ListOfAllCalculatedResults;
using Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleModeSpaces;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleResults.Exporting;
using Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleResults.Dtos;
using Mohajer.ClassScheduleProject.Dto;
using Abp.Application.Services.Dto;
using Mohajer.ClassScheduleProject.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Mohajer.ClassScheduleProject.Storage;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityDepartments;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessors;
using Mohajer.ClassScheduleProject.CentralUnit.LessonsOfSemesters;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessorWorkingTimes;
using Mohajer.ClassScheduleProject.CentralUnit.Lessons;
using Mohajer.ClassScheduleProject.CentralUnit.Semesters;
using Mohajer.ClassScheduleProject.CentralUnit.Grades;
using Mohajer.ClassScheduleProject.CentralUnit.AssigningGradeToUniversityMajors;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityMajors;
using Mohajer.ClassScheduleProject.CentralUnit.ClassroomBuildings;
using Mohajer.ClassScheduleProject.CentralUnit.AssigningUniversityMajorToClassroomBuildings;
using Mohajer.ClassScheduleProject.CentralUnit.LessonsOfUniversityProfessors;
using Mohajer.ClassScheduleProject.CentralUnit.WorkTimeInDays;
using Mohajer.ClassScheduleProject.CentralUnit.ListOfClassScheduleModeSpaces;
using Mohajer.ClassScheduleProject.CentralUnit.ListOfMainDomains;
using Mohajer.ClassScheduleProject.CentralUnit.MainDomains;

namespace Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleResults
{
    [AbpAuthorize(AppPermissions.Pages_ClassScheduleResults)]
    public class ClassScheduleResultsAppService : ClassScheduleProjectAppServiceBase, IClassScheduleResultsAppService
    {
        private readonly IRepository<ClassScheduleResult, long> _classScheduleResultRepository;
        private readonly IClassScheduleResultsExcelExporter _classScheduleResultsExcelExporter;
        private readonly IRepository<ListOfAllCalculatedResult, long> _lookup_listOfAllCalculatedResultRepository;
        private readonly IRepository<ClassScheduleModeSpace, long> _lookup_classScheduleModeSpaceRepository;

        private readonly IRepository<UniversityProfessorWorkingTime, long> _universityProfessorWorkingTimeRepository;
        private readonly IRepository<Lesson, long> _lessonRepository;
        private readonly IRepository<LessonsOfSemester, long> _lessonsOfSemesterRepository;
        private readonly IRepository<UniversityProfessor> _universityProfessorRepository;
        private readonly IRepository<Semester, long> _semesterRepository;
        private readonly IRepository<Grade> _gradeRepository;
        private readonly IRepository<AssigningGradeToUniversityMajor, long> _assigningGradeToUniversityMajorRepository;
        private readonly IRepository<UniversityMajor> _universityMajorRepository;
        private readonly IRepository<UniversityDepartment> _universityDepartmentRepository;
        private readonly IRepository<ClassroomBuilding> _classroomBuildingRepository;
        private readonly IRepository<AssigningUniversityMajorToClassroomBuilding, long> _assigningUniversityMajorToClassroomBuildingRepository;
        private readonly IRepository<LessonsOfUniversityProfessor, long> _lessonsOfUniversityProfessorRepository;
        private readonly IRepository<ClassScheduleModeSpace, long> _classScheduleModeSpaceRepository;
        private readonly IRepository<WorkTimeInDay, long> _workTimeInDayRepository;

        private readonly IRepository<ListOfClassScheduleModeSpace, long> _listOfClassScheduleModeSpaceRepository;
        private readonly IRepository<ListOfMainDomain, long> _listOfMainDomainRepository;
        private readonly IRepository<MainDomain, long> _mainDomainRepository;


        private List<UniversityProfessorWorkingTime> _universityProfessorWorkingTime;
        private List<Lesson> _lesson;
        private List<LessonsOfSemester> _lessonsOfSemester;
        private List<UniversityProfessor> _universityProfessors;
        private List<WorkTimeInDay> _workTimesInDay;
        private List<Semester> _semester;
        private List<Grade> _grades;
        private List<AssigningGradeToUniversityMajor> _assigningGradeToUniversityMajor;
        private List<UniversityMajor> _universityMajor;
        private List<UniversityDepartment> _universityDepartment;
        private List<ClassroomBuilding> _classroomBuilding;
        private List<AssigningUniversityMajorToClassroomBuilding> _assigningUniversityMajorToClassroomBuilding;
        private List<LessonsOfUniversityProfessor> _lessonsOfUniversityProfessor;
        private List<ClassScheduleModeSpace> _classScheduleModeSpace;

        private Random random;


        public ClassScheduleResultsAppService(IRepository<ClassScheduleResult, long> classScheduleResultRepository, IClassScheduleResultsExcelExporter classScheduleResultsExcelExporter, IRepository<ListOfAllCalculatedResult, long> lookup_listOfAllCalculatedResultRepository, IRepository<ClassScheduleModeSpace, long> lookup_classScheduleModeSpaceRepository,
            IRepository<UniversityProfessorWorkingTime, long> universityProfessorWorkingTimeRepository,
            IRepository<Lesson, long> lessonRepository,
            IRepository<LessonsOfSemester, long> lessonsOfSemesterRepository,
            IRepository<UniversityProfessor> universityProfessorRepository,
           IRepository<Semester, long> semesterRepository,
             IRepository<Grade> gradeRepository,
            IRepository<AssigningGradeToUniversityMajor, long> assigningGradeToUniversityMajorRepository,
            IRepository<UniversityMajor> universityMajorRepository,
            IRepository<UniversityDepartment> universityDepartmentRepository,
             IRepository<ClassroomBuilding> classroomBuildingRepository,
           IRepository<AssigningUniversityMajorToClassroomBuilding, long> assigningUniversityMajorToClassroomBuildingRepository,
           IRepository<LessonsOfUniversityProfessor, long> lessonsOfUniversityProfessorRepository,
            IRepository<ClassScheduleModeSpace, long> classScheduleModeSpaceRepository,
            IRepository<WorkTimeInDay, long> workTimeInDayRepository,
            IRepository<ListOfClassScheduleModeSpace, long> listOfClassScheduleModeSpaceRepository,
            IRepository<ListOfMainDomain, long> listOfMainDomainRepository,
            IRepository<MainDomain, long> mainDomainRepository)
        {
            _classScheduleResultRepository = classScheduleResultRepository;
            _classScheduleResultsExcelExporter = classScheduleResultsExcelExporter;
            _lookup_listOfAllCalculatedResultRepository = lookup_listOfAllCalculatedResultRepository;
            _lookup_classScheduleModeSpaceRepository = lookup_classScheduleModeSpaceRepository;

            _universityProfessorWorkingTimeRepository = universityProfessorWorkingTimeRepository;
            _lessonRepository = lessonRepository;
            _lessonsOfSemesterRepository = lessonsOfSemesterRepository;
            _universityProfessorRepository = universityProfessorRepository;
            _semesterRepository = semesterRepository;
            _gradeRepository = gradeRepository;
            _assigningGradeToUniversityMajorRepository = assigningGradeToUniversityMajorRepository;
            _universityMajorRepository = universityMajorRepository;
            _universityDepartmentRepository = universityDepartmentRepository;
            _classroomBuildingRepository = classroomBuildingRepository;
            _assigningUniversityMajorToClassroomBuildingRepository = assigningUniversityMajorToClassroomBuildingRepository;
            _lessonsOfUniversityProfessorRepository = lessonsOfUniversityProfessorRepository;
            _classScheduleModeSpaceRepository = classScheduleModeSpaceRepository;
            _workTimeInDayRepository = workTimeInDayRepository;
            _listOfClassScheduleModeSpaceRepository = listOfClassScheduleModeSpaceRepository;
            _listOfMainDomainRepository = listOfMainDomainRepository;
            _mainDomainRepository = mainDomainRepository;
        }


        public async Task<PagedResultDto<GetClassScheduleResultForViewDto>> GetAll(GetAllClassScheduleResultsInput input)
        {

            var filteredClassScheduleResults = _classScheduleResultRepository.GetAll()
                        .Include(e => e.ListOfAllCalculatedResultFk)
                        .Include(e => e.ClassScheduleModeSpaceFk)
                        .WhereIf(!string.IsNullOrEmpty(input.ListOfAllCalculatedResultNameCalculatedResultFilter), e => e.ListOfAllCalculatedResultFk != null && e.ListOfAllCalculatedResultFk.NameCalculatedResult == input.ListOfAllCalculatedResultNameCalculatedResultFilter.Trim())
                        .WhereIf(!string.IsNullOrEmpty(input.Filter), e => e.ListOfAllCalculatedResultFk != null && e.ListOfAllCalculatedResultFk.NameCalculatedResult == input.Filter.Trim())
                        .WhereIf(!string.IsNullOrEmpty(input.ClassScheduleModeSpaceNameClassScheduleModeSpacesFilter), e => e.ClassScheduleModeSpaceFk != null && e.ClassScheduleModeSpaceFk.NameClassScheduleModeSpaces == input.ClassScheduleModeSpaceNameClassScheduleModeSpacesFilter.Trim())
                        .WhereIf(!string.IsNullOrEmpty(input.Filter), e => e.ClassScheduleModeSpaceFk != null && e.ClassScheduleModeSpaceFk.NameClassScheduleModeSpaces == input.Filter.Trim())
                        .WhereIf(input.ListOfAllCalculatedResultNameCalculatedResultIdFilter.HasValue, e => e.ListOfAllCalculatedResultId == input.ListOfAllCalculatedResultNameCalculatedResultIdFilter)
                        .WhereIf(input.ClassScheduleModeSpaceNameClassScheduleModeSpacesIdFilter.HasValue, e => e.ListOfAllCalculatedResultId == input.ClassScheduleModeSpaceNameClassScheduleModeSpacesIdFilter);

            var pagedAndFilteredClassScheduleResults = filteredClassScheduleResults
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var classScheduleResults = (from o in pagedAndFilteredClassScheduleResults
                                        join o1 in _lookup_listOfAllCalculatedResultRepository.GetAll() on o.ListOfAllCalculatedResultId equals o1.Id into j1
                                        from s1 in j1.DefaultIfEmpty()

                                        join o2 in _lookup_classScheduleModeSpaceRepository.GetAll() on o.ClassScheduleModeSpaceId equals o2.Id into j2
                                        from s2 in j2.DefaultIfEmpty()

                                        join o3 in _classroomBuildingRepository.GetAll()
                                        on o.ClassroomBuildingId equals o3.Id into j3
                                        from s3 in j3.DefaultIfEmpty()

                                        join o4 in _universityProfessorRepository.GetAll()
                                        on o.UniversityProfessorId equals o4.Id into j4
                                        from s4 in j4.DefaultIfEmpty()

                                        join o5 in _universityDepartmentRepository.GetAll()
                                        on o.UniversityDepartmentId equals o5.Id into j5
                                        from s5 in j5.DefaultIfEmpty()

                                        join o6 in _gradeRepository.GetAll()
                                        on o.GradeId equals o6.Id into j6
                                        from s6 in j6.DefaultIfEmpty()

                                        join o7 in _semesterRepository.GetAll()
                                        on o.SemesterId equals o7.Id into j7
                                        from s7 in j7.DefaultIfEmpty()

                                        join o8 in _universityMajorRepository.GetAll()
                                        on o.UniversityMajorId equals o8.Id into j8
                                        from s8 in j8.DefaultIfEmpty()


                                        join o9 in _lessonRepository.GetAll()

                                        on o.LessonId equals o9.Id into j9
                                        from s9 in j9.DefaultIfEmpty()

                                        join o10 in _workTimeInDayRepository.GetAll()
                                        on o.WorkTimeInDayId equals o10.Id into j10
                                        from s10 in j10.DefaultIfEmpty()

                                        select new GetClassScheduleResultForViewDto()
                                        {
                                            ClassScheduleResult = new ClassScheduleResultDto
                                            {
                                                WorkTimeInDayId = s10.NameWorkTimeInDay,
                                                LessonId = s9.NameLesson,
                                                UniversityProfessorId = s4.UniversityProfessorName,
                                                SemesterId = s7.SemesterName,
                                                GradeId = s6.GradeName,
                                                UniversityMajorId = s8.UniversityMajorName,
                                                UniversityDepartmentId = s5.UniversityDepartmentName,
                                                ClassroomBuildingId = s3.ClassroomBuildingName,
                                                Id = o.Id,
                                                ClassScheduleModeSpaceId = o.ClassScheduleModeSpaceId,
                                                ListOfAllCalculatedResultId = o.ListOfAllCalculatedResultId
                                            },
                                            ListOfAllCalculatedResultNameCalculatedResult = s1 == null || s1.NameCalculatedResult == null ? "" : s1.NameCalculatedResult.ToString(),
                                            ClassScheduleModeSpaceNameClassScheduleModeSpaces = s2 == null || s2.NameClassScheduleModeSpaces == null ? "" : s2.NameClassScheduleModeSpaces.ToString()
                                        })
                                        .WhereIf(!string.IsNullOrEmpty(input.ClassroomBuildingFilter), e => e.ClassScheduleResult.ClassroomBuildingId == input.ClassroomBuildingFilter.Trim())
                                        .WhereIf(!string.IsNullOrEmpty(input.Filter), e => e.ClassScheduleResult.ClassroomBuildingId == input.Filter.Trim())
                                        .WhereIf(!string.IsNullOrEmpty(input.UniversityProfessorFilter), e => e.ClassScheduleResult.UniversityProfessorId == input.UniversityProfessorFilter.Trim())
                                        .WhereIf(!string.IsNullOrEmpty(input.Filter), e => e.ClassScheduleResult.UniversityProfessorId == input.Filter.Trim())
                                        .WhereIf(!string.IsNullOrEmpty(input.UniversityDepartmentFilter), e => e.ClassScheduleResult.UniversityDepartmentId == input.UniversityDepartmentFilter.Trim())
                                        .WhereIf(!string.IsNullOrEmpty(input.Filter), e => e.ClassScheduleResult.UniversityDepartmentId == input.Filter.Trim())
                                        .WhereIf(!string.IsNullOrEmpty(input.GradeFilter), e => e.ClassScheduleResult.GradeId == input.GradeFilter.Trim())
                                        .WhereIf(!string.IsNullOrEmpty(input.Filter), e => e.ClassScheduleResult.GradeId == input.Filter.Trim())
                                        .WhereIf(!string.IsNullOrEmpty(input.SemesterFilter), e => e.ClassScheduleResult.SemesterId == input.SemesterFilter.Trim())
                                        .WhereIf(!string.IsNullOrEmpty(input.Filter), e => e.ClassScheduleResult.SemesterId == input.Filter.Trim())
                                        .WhereIf(!string.IsNullOrEmpty(input.UniversityMajorFilter), e => e.ClassScheduleResult.UniversityMajorId == input.UniversityMajorFilter.Trim())
                                        .WhereIf(!string.IsNullOrEmpty(input.Filter), e => e.ClassScheduleResult.UniversityMajorId == input.Filter.Trim())
                                        .WhereIf(!string.IsNullOrEmpty(input.LessonFilter), e => e.ClassScheduleResult.LessonId == input.LessonFilter.Trim())
                                        .WhereIf(!string.IsNullOrEmpty(input.Filter), e => e.ClassScheduleResult.LessonId == input.Filter.Trim())
                                        .WhereIf(!string.IsNullOrEmpty(input.WorkTimeInDayFilter), e => e.ClassScheduleResult.WorkTimeInDayId == input.WorkTimeInDayFilter.Trim())
                                        .WhereIf(!string.IsNullOrEmpty(input.Filter), e => e.ClassScheduleResult.WorkTimeInDayId == input.Filter.Trim());

            var totalCount = await filteredClassScheduleResults.CountAsync();

            var dbList = await classScheduleResults.ToListAsync();

            return new PagedResultDto<GetClassScheduleResultForViewDto>(
                totalCount,
                dbList
            );

        }

        public async Task<GetClassScheduleResultForViewDto> GetClassScheduleResultForView(long id)
        {
            var classScheduleResult = await _classScheduleResultRepository.GetAsync(id);

            var output = new GetClassScheduleResultForViewDto { ClassScheduleResult = ObjectMapper.Map<ClassScheduleResultDto>(classScheduleResult) };

            if (output.ClassScheduleResult.ListOfAllCalculatedResultId != null)
            {
                var _lookupListOfAllCalculatedResult = await _lookup_listOfAllCalculatedResultRepository.FirstOrDefaultAsync((long)output.ClassScheduleResult.ListOfAllCalculatedResultId);
                output.ListOfAllCalculatedResultNameCalculatedResult = _lookupListOfAllCalculatedResult?.NameCalculatedResult?.ToString();
            }

            if (output.ClassScheduleResult.ClassScheduleModeSpaceId != null)
            {
                var _lookupClassScheduleModeSpace = await _lookup_classScheduleModeSpaceRepository.FirstOrDefaultAsync((long)output.ClassScheduleResult.ClassScheduleModeSpaceId);
                output.ClassScheduleModeSpaceNameClassScheduleModeSpaces = _lookupClassScheduleModeSpace?.NameClassScheduleModeSpaces?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_ClassScheduleResults_Edit)]
        public async Task<GetClassScheduleResultForEditOutput> GetClassScheduleResultForEdit(EntityDto<long> input)
        {
            var classScheduleResult = await _classScheduleResultRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetClassScheduleResultForEditOutput { ClassScheduleResult = ObjectMapper.Map<CreateOrEditClassScheduleResultDto>(classScheduleResult) };

            if (output.ClassScheduleResult.ListOfAllCalculatedResultId != null)
            {
                var _lookupListOfAllCalculatedResult = await _lookup_listOfAllCalculatedResultRepository.FirstOrDefaultAsync((long)output.ClassScheduleResult.ListOfAllCalculatedResultId);
                output.ListOfAllCalculatedResultNameCalculatedResult = _lookupListOfAllCalculatedResult?.NameCalculatedResult?.ToString();
            }

            if (output.ClassScheduleResult.ClassScheduleModeSpaceId != null)
            {
                var _lookupClassScheduleModeSpace = await _lookup_classScheduleModeSpaceRepository.FirstOrDefaultAsync((long)output.ClassScheduleResult.ClassScheduleModeSpaceId);
                output.ClassScheduleModeSpaceNameClassScheduleModeSpaces = _lookupClassScheduleModeSpace?.NameClassScheduleModeSpaces?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditClassScheduleResultDto input)
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

        [AbpAuthorize(AppPermissions.Pages_ClassScheduleResults_Create)]
        protected virtual async Task Create(CreateOrEditClassScheduleResultDto input)
        {
            var classScheduleResult = ObjectMapper.Map<ClassScheduleResult>(input);

            if (AbpSession.TenantId != null)
            {
                classScheduleResult.TenantId = (int)AbpSession.TenantId;
            }

            await _classScheduleResultRepository.InsertAsync(classScheduleResult);

        }

        [AbpAuthorize(AppPermissions.Pages_ClassScheduleResults_Edit)]
        protected virtual async Task Update(CreateOrEditClassScheduleResultDto input)
        {
            var classScheduleResult = await _classScheduleResultRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, classScheduleResult);

        }

        [AbpAuthorize(AppPermissions.Pages_ClassScheduleResults_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _classScheduleResultRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetClassScheduleResultsToExcel(GetAllClassScheduleResultsForExcelInput input)
        {

            var filteredClassScheduleResults = _classScheduleResultRepository.GetAll()
                        .Include(e => e.ListOfAllCalculatedResultFk)
                        .Include(e => e.ClassScheduleModeSpaceFk)
                        .WhereIf(!string.IsNullOrEmpty(input.ListOfAllCalculatedResultNameCalculatedResultFilter), e => e.ListOfAllCalculatedResultFk != null && e.ListOfAllCalculatedResultFk.NameCalculatedResult == input.ListOfAllCalculatedResultNameCalculatedResultFilter.Trim())
                        .WhereIf(!string.IsNullOrEmpty(input.Filter), e => e.ListOfAllCalculatedResultFk != null && e.ListOfAllCalculatedResultFk.NameCalculatedResult == input.Filter.Trim())
                        .WhereIf(!string.IsNullOrEmpty(input.ClassScheduleModeSpaceNameClassScheduleModeSpacesFilter), e => e.ClassScheduleModeSpaceFk != null && e.ClassScheduleModeSpaceFk.NameClassScheduleModeSpaces == input.ClassScheduleModeSpaceNameClassScheduleModeSpacesFilter.Trim())
                        .WhereIf(!string.IsNullOrEmpty(input.Filter), e => e.ClassScheduleModeSpaceFk != null && e.ClassScheduleModeSpaceFk.NameClassScheduleModeSpaces == input.Filter.Trim())
                        .WhereIf(input.ListOfAllCalculatedResultNameCalculatedResultIdFilter.HasValue, e => e.ListOfAllCalculatedResultId == input.ListOfAllCalculatedResultNameCalculatedResultIdFilter)
                        .WhereIf(input.ClassScheduleModeSpaceNameClassScheduleModeSpacesIdFilter.HasValue, e => e.ListOfAllCalculatedResultId == input.ClassScheduleModeSpaceNameClassScheduleModeSpacesIdFilter);

            var query = (from o in filteredClassScheduleResults
                         join o1 in _lookup_listOfAllCalculatedResultRepository.GetAll() on o.ListOfAllCalculatedResultId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         join o2 in _lookup_classScheduleModeSpaceRepository.GetAll() on o.ClassScheduleModeSpaceId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()

                         join o3 in _classroomBuildingRepository.GetAll()
                         on o.ClassroomBuildingId equals o3.Id into j3
                         from s3 in j3.DefaultIfEmpty()

                         join o4 in _universityProfessorRepository.GetAll()
                         on o.UniversityProfessorId equals o4.Id into j4
                         from s4 in j4.DefaultIfEmpty()

                         join o5 in _universityDepartmentRepository.GetAll()
                         on o.UniversityDepartmentId equals o5.Id into j5
                         from s5 in j5.DefaultIfEmpty()

                         join o6 in _gradeRepository.GetAll()
                         on o.GradeId equals o6.Id into j6
                         from s6 in j6.DefaultIfEmpty()

                         join o7 in _semesterRepository.GetAll()
                         on o.SemesterId equals o7.Id into j7
                         from s7 in j7.DefaultIfEmpty()

                         join o8 in _universityMajorRepository.GetAll()
                         on o.UniversityMajorId equals o8.Id into j8
                         from s8 in j8.DefaultIfEmpty()


                         join o9 in _lessonRepository.GetAll()

                         on o.LessonId equals o9.Id into j9
                         from s9 in j9.DefaultIfEmpty()

                         join o10 in _workTimeInDayRepository.GetAll()
                         on o.WorkTimeInDayId equals o10.Id into j10
                         from s10 in j10.DefaultIfEmpty()

                         select new GetClassScheduleResultForViewDto()
                         {
                             ClassScheduleResult = new ClassScheduleResultDto
                             {
                                 WorkTimeInDayId = s10.NameWorkTimeInDay,
                                 LessonId = s9.NameLesson,
                                 UniversityProfessorId = s4.UniversityProfessorName,
                                 SemesterId = s7.SemesterName,
                                 GradeId = s6.GradeName,
                                 UniversityMajorId = s8.UniversityMajorName,
                                 UniversityDepartmentId = s5.UniversityDepartmentName,
                                 ClassroomBuildingId = s3.ClassroomBuildingName,
                                 Id = o.Id,
                                 ClassScheduleModeSpaceId = o.ClassScheduleModeSpaceId,
                                 ListOfAllCalculatedResultId = o.ListOfAllCalculatedResultId
                             },
                             ListOfAllCalculatedResultNameCalculatedResult = s1 == null || s1.NameCalculatedResult == null ? "" : s1.NameCalculatedResult.ToString(),
                             ClassScheduleModeSpaceNameClassScheduleModeSpaces = s2 == null || s2.NameClassScheduleModeSpaces == null ? "" : s2.NameClassScheduleModeSpaces.ToString()
                         })
                                        .WhereIf(!string.IsNullOrEmpty(input.ClassroomBuildingFilter), e => e.ClassScheduleResult.ClassroomBuildingId == input.ClassroomBuildingFilter.Trim())
                                        .WhereIf(!string.IsNullOrEmpty(input.Filter), e => e.ClassScheduleResult.ClassroomBuildingId == input.Filter.Trim())
                                        .WhereIf(!string.IsNullOrEmpty(input.UniversityProfessorFilter), e => e.ClassScheduleResult.UniversityProfessorId == input.UniversityProfessorFilter.Trim())
                                        .WhereIf(!string.IsNullOrEmpty(input.Filter), e => e.ClassScheduleResult.UniversityProfessorId == input.Filter.Trim())
                                        .WhereIf(!string.IsNullOrEmpty(input.UniversityDepartmentFilter), e => e.ClassScheduleResult.UniversityDepartmentId == input.UniversityDepartmentFilter.Trim())
                                        .WhereIf(!string.IsNullOrEmpty(input.Filter), e => e.ClassScheduleResult.UniversityDepartmentId == input.Filter.Trim())
                                        .WhereIf(!string.IsNullOrEmpty(input.GradeFilter), e => e.ClassScheduleResult.GradeId == input.GradeFilter.Trim())
                                        .WhereIf(!string.IsNullOrEmpty(input.Filter), e => e.ClassScheduleResult.GradeId == input.Filter.Trim())
                                        .WhereIf(!string.IsNullOrEmpty(input.SemesterFilter), e => e.ClassScheduleResult.SemesterId == input.SemesterFilter.Trim())
                                        .WhereIf(!string.IsNullOrEmpty(input.Filter), e => e.ClassScheduleResult.SemesterId == input.Filter.Trim())
                                        .WhereIf(!string.IsNullOrEmpty(input.UniversityMajorFilter), e => e.ClassScheduleResult.UniversityMajorId == input.UniversityMajorFilter.Trim())
                                        .WhereIf(!string.IsNullOrEmpty(input.Filter), e => e.ClassScheduleResult.UniversityMajorId == input.Filter.Trim())
                                        .WhereIf(!string.IsNullOrEmpty(input.LessonFilter), e => e.ClassScheduleResult.LessonId == input.LessonFilter.Trim())
                                        .WhereIf(!string.IsNullOrEmpty(input.Filter), e => e.ClassScheduleResult.LessonId == input.Filter.Trim())
                                        .WhereIf(!string.IsNullOrEmpty(input.WorkTimeInDayFilter), e => e.ClassScheduleResult.WorkTimeInDayId == input.WorkTimeInDayFilter.Trim())
                                        .WhereIf(!string.IsNullOrEmpty(input.Filter), e => e.ClassScheduleResult.WorkTimeInDayId == input.Filter.Trim());

            var classScheduleResultListDtos = await query.ToListAsync();

            return _classScheduleResultsExcelExporter.ExportToFile(classScheduleResultListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_ClassScheduleResults)]
        public async Task<PagedResultDto<ClassScheduleResultListOfAllCalculatedResultLookupTableDto>> GetAllListOfAllCalculatedResultForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_listOfAllCalculatedResultRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.NameCalculatedResult != null && e.NameCalculatedResult.Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var listOfAllCalculatedResultList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<ClassScheduleResultListOfAllCalculatedResultLookupTableDto>();
            foreach (var listOfAllCalculatedResult in listOfAllCalculatedResultList)
            {
                lookupTableDtoList.Add(new ClassScheduleResultListOfAllCalculatedResultLookupTableDto
                {
                    Id = listOfAllCalculatedResult.Id,
                    DisplayName = listOfAllCalculatedResult.NameCalculatedResult?.ToString()
                });
            }

            return new PagedResultDto<ClassScheduleResultListOfAllCalculatedResultLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

        [AbpAuthorize(AppPermissions.Pages_ClassScheduleResults)]
        public async Task<PagedResultDto<ClassScheduleResultClassScheduleModeSpaceLookupTableDto>> GetAllClassScheduleModeSpaceForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_classScheduleModeSpaceRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.NameClassScheduleModeSpaces != null && e.NameClassScheduleModeSpaces.Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var classScheduleModeSpaceList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<ClassScheduleResultClassScheduleModeSpaceLookupTableDto>();
            foreach (var classScheduleModeSpace in classScheduleModeSpaceList)
            {
                lookupTableDtoList.Add(new ClassScheduleResultClassScheduleModeSpaceLookupTableDto
                {
                    Id = classScheduleModeSpace.Id,
                    DisplayName = classScheduleModeSpace.NameClassScheduleModeSpaces?.ToString()
                });
            }

            return new PagedResultDto<ClassScheduleResultClassScheduleModeSpaceLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }
        public async Task<StartClassScheduleOutputDto> StartClassSchedule(StartClassScheduleInputDto input)
        {

            _classroomBuilding = await _classroomBuildingRepository.GetAll().ToListAsync();
            _universityDepartment = await _universityDepartmentRepository.GetAll().ToListAsync();

            _universityMajor = await _universityMajorRepository.GetAll()
                .Include(s => s.UniversityDepartmentFk)
                .ToListAsync();

            _assigningUniversityMajorToClassroomBuilding = await _assigningUniversityMajorToClassroomBuildingRepository
                .GetAll().
                Include(s => s.ClassroomBuildingFk)
                .Include(s => s.UniversityMajorFk)
                .ToListAsync();
            _grades = await _gradeRepository.GetAll().ToListAsync();

            _assigningGradeToUniversityMajor = await _assigningGradeToUniversityMajorRepository.GetAll()
                .Include(s => s.GradeFk)
                .Include(s => s.UniversityMajorFk)
                .ToListAsync();

            _semester = await _semesterRepository.GetAll()
                .Where(record => record.IsActive == true)
                 .Include(s => s.AssigningGradeToUniversityMajorFk.UniversityMajorFk.UniversityDepartmentFk)
                 .Include(s => s.AssigningGradeToUniversityMajorFk.GradeFk)
                 .ToListAsync();

            _workTimesInDay = await _workTimeInDayRepository.GetAll().ToListAsync();

            _universityProfessors = await _universityProfessorRepository.GetAll()
                .Where(record => record.IsActive == true)
                .ToListAsync();

            _universityProfessorWorkingTime = await _universityProfessorWorkingTimeRepository.GetAll()
                .Include(s => s.UniversityProfessorFk)
                .Include(s => s.WorkTimeInDayFk)
                .ToListAsync();
            _lesson = await _lessonRepository.GetAll().
                Include(s => s.ClassroomBuildingFk)
                .ToListAsync();
            _lessonsOfSemester = await _lessonsOfSemesterRepository.GetAll()
                .Include(s => s.LessonFk)
                .Include(s => s.SemesterFk.AssigningGradeToUniversityMajorFk.UniversityMajorFk.UniversityDepartmentFk)
                .Include(s => s.SemesterFk.AssigningGradeToUniversityMajorFk.GradeFk)
                .Where(record => record.IsActive == true && record.SemesterFk.IsActive == true)
                .ToListAsync();
            _lessonsOfUniversityProfessor = await _lessonsOfUniversityProfessorRepository.GetAll()
                .Include(s => s.LessonFk)
                .Include(s => s.UniversityProfessorFk)
                .Where(record => record.UniversityProfessorFk.IsActive == true && record.LessonFk.IsActive == true)
                .ToListAsync();

            ListOfAllCalculatedResult listOfAllCalculatedResult = await _lookup_listOfAllCalculatedResultRepository.GetAsync(input.ListOfAllCalculatedResultId);
            long listOfClassScheduleModeSpaceId = await _listOfClassScheduleModeSpaceRepository.InsertAndGetIdAsync(new ListOfClassScheduleModeSpace()
            {
                TenantId = AbpSession.TenantId.Value,
                ListOfClassScheduleModeSpaceName = listOfAllCalculatedResult.NameCalculatedResult,
                ListOfAllCalculatedResultId= input.ListOfAllCalculatedResultId
            });
           
            _classScheduleModeSpace = new List<ClassScheduleModeSpace>();

            foreach (var universityProfessors in _universityProfessors)
            {
                foreach (var lessonOfUniversityProfessor in _lessonsOfUniversityProfessor.Where(record => record.UniversityProfessorId == universityProfessors.Id).ToList())
                {
                    foreach (var universityProfessorModeSpace in _universityProfessorWorkingTime.Where(record => record.UniversityProfessorId == universityProfessors.Id))
                    {
                        ClassScheduleModeSpace classScheduleModeSpace = new ClassScheduleModeSpace()
                        {
                            UniversityProfessorFk = universityProfessors,
                            IsLock = false,
                            LessonId = lessonOfUniversityProfessor.LessonId,
                            NameClassScheduleModeSpaces = $"{universityProfessors.UniversityProfessorName} {lessonOfUniversityProfessor.LessonFk.NameLesson} {universityProfessorModeSpace.WorkTimeInDayFk.NameWorkTimeInDay}",
                            UniversityProfessorId = universityProfessors.Id,
                            WorkTimeInDayId = universityProfessorModeSpace.WorkTimeInDayId,
                            TenantId = AbpSession.TenantId.Value,
                            ListOfClassScheduleModeSpaceId = listOfClassScheduleModeSpaceId,

                        };
                        long classScheduleModeSpaceId = await _classScheduleModeSpaceRepository.InsertAndGetIdAsync(classScheduleModeSpace);
                        _classScheduleModeSpace.Add(
                            new ClassScheduleModeSpace()
                            {
                                LessonFk = lessonOfUniversityProfessor.LessonFk,
                                UniversityProfessorFk = universityProfessors,
                                WorkTimeInDayFk = universityProfessorModeSpace.WorkTimeInDayFk,
                                IsLock = false,
                                Id = classScheduleModeSpaceId,
                                LessonId = lessonOfUniversityProfessor.LessonId,
                                NameClassScheduleModeSpaces = universityProfessorModeSpace.WorkTimeInDayFk.NameWorkTimeInDay,
                                UniversityProfessorId = universityProfessors.Id,
                                WorkTimeInDayId = universityProfessorModeSpace.WorkTimeInDayId,
                                TenantId = AbpSession.TenantId.Value,
                                ListOfClassScheduleModeSpaceId = listOfClassScheduleModeSpaceId
                            });
                    }
                }
            }
            long listOfMainDomainId = await _listOfMainDomainRepository.InsertAndGetIdAsync(new ListOfMainDomain()
            {
                TenantId = AbpSession.TenantId.Value,
                ListOfMainDomainName = listOfAllCalculatedResult.NameCalculatedResult
            });
            List<MainDomain> mainDomain = new List<MainDomain>();
            foreach (var semester in _semester)
            {
                var lessonsOfSemester = _lessonsOfSemester.Where(record => record.SemesterId == semester.Id).ToList();
                foreach (var Lesson in lessonsOfSemester)
                {
                    for (int numberOfClassesToBeFormedIndex = 0; numberOfClassesToBeFormedIndex < Lesson.NumberOfClassesToBeFormed; numberOfClassesToBeFormedIndex++)
                    {
                        for (int courseNumberOfGroupsIndex = 0; courseNumberOfGroupsIndex < Lesson.LessonFk.NumberOfGroups; courseNumberOfGroupsIndex++)
                        {
                            long mainDomainId = await _mainDomainRepository.InsertAndGetIdAsync(new MainDomain()
                            {
                                TenantId = AbpSession.TenantId.Value,
                                LessonsOfSemesterId = Lesson.Id,
                                ListOfMainDomainId = listOfMainDomainId,
                                MainDomainName = Lesson.LessonsOfSemesterName
                            });
                            mainDomain.Add(new MainDomain()
                            {
                                LessonsOfSemesterFk = Lesson,
                                TenantId = AbpSession.TenantId.Value,
                                Id = mainDomainId,
                                LessonsOfSemesterId = Lesson.Id,
                                ListOfMainDomainId = listOfMainDomainId,
                                MainDomainName = Lesson.LessonsOfSemesterName
                            });
                        }
                    }
                }
            }

            List<ClassScheduleResult> classScheduleResults = new List<ClassScheduleResult>();
            random = new Random(Guid.NewGuid().GetHashCode());
            Parallel.ForEach(_semester, new ParallelOptions() { MaxDegreeOfParallelism = _semester.Count }, async (semester) => classScheduleResults.AddRange(SetCoursesToSemester(semester).Result));
            listOfAllCalculatedResult.Price = Convert.ToInt32(Math.Floor(CalculatorH(classScheduleResults, mainDomain)));
            await _lookup_listOfAllCalculatedResultRepository.UpdateAsync(listOfAllCalculatedResult);
            foreach (ClassScheduleResult classScheduleResult in classScheduleResults)
            {
                await _classScheduleResultRepository.InsertAsync(new ClassScheduleResult()
                {
                    SemesterId = classScheduleResult.SemesterId,
                    LessonId = classScheduleResult.LessonId,
                    ClassroomBuildingId = classScheduleResult.ClassroomBuildingId,
                    ClassScheduleModeSpaceId = classScheduleResult.ClassScheduleModeSpaceId,
                    GradeId = classScheduleResult.GradeId,
                    ListOfAllCalculatedResultId = listOfAllCalculatedResult.Id,
                    TenantId = AbpSession.TenantId.Value,
                    UniversityDepartmentId = classScheduleResult.UniversityDepartmentId,
                    UniversityMajorId = classScheduleResult.UniversityMajorId,
                    UniversityProfessorId = classScheduleResult.UniversityProfessorId,
                    WorkTimeInDayId = classScheduleResult.WorkTimeInDayId
                });
            }
            return new StartClassScheduleOutputDto() { IsSuccsses = true };
        }
        private async Task<ClassScheduleResult> AllocationCourseToSemester(Lesson lesson, WorkTimeInDay workTimeInDay, UniversityProfessor universityProfessor, Semester semester, Grade grade, UniversityMajor universityMajor, UniversityDepartment universityDepartment, List<ClassScheduleResult> remain)
        {
            lock (_classScheduleModeSpace)
            {
                var universityMajorToClassroomBuilding = _assigningUniversityMajorToClassroomBuilding.Where(record => record.UniversityMajorId == universityMajor.Id).ToList();
                var classroomBuildingForLesson = universityMajorToClassroomBuilding.FirstOrDefault(record => record.ClassroomBuildingId == lesson.ClassroomBuildingId);

                if (classroomBuildingForLesson != null)
                {
                    int countClassroomBuildingUsed = remain.Count(record => record.ClassroomBuildingId == classroomBuildingForLesson.ClassroomBuildingId && record.WorkTimeInDayId == workTimeInDay.Id);

                    var universityProfessorCanAllocationLessonList = ParallelEnumerable
                      .Where(
                            ParallelEnumerable.AsParallel(_classScheduleModeSpace),
                            record => record.IsLock == false && record.UniversityProfessorId == universityProfessor.Id && record.WorkTimeInDayId == workTimeInDay.Id)
                           .WithMergeOptions(ParallelMergeOptions.FullyBuffered).WithExecutionMode(ParallelExecutionMode.ForceParallelism).OrderBy(record => _universityProfessorWorkingTime.Count(record => record.UniversityProfessorId == record.UniversityProfessorId)).ThenBy(record => record.WorkTimeInDayFk.DayOfWeek).ThenBy(record => record.WorkTimeInDayFk.WhatTimeOfDayIndex).ToList();
                    if (universityProfessorCanAllocationLessonList != null && universityProfessorCanAllocationLessonList.Any())
                    {
                        var universityProfessorCanAllocationLesson = universityProfessorCanAllocationLessonList.FirstOrDefault(record => record.LessonId == lesson.Id);
                        if (universityProfessorCanAllocationLesson != null)
                        {
                            if ((countClassroomBuildingUsed + 1) > classroomBuildingForLesson.MaximumRestrictionsOnUsingClassroomsAtTheSameTime)
                            {
                                return null;
                            }

                            universityProfessorCanAllocationLessonList.ForEach((rows) =>
                            {
                                rows.IsLock = true;
                            });
                            universityProfessorCanAllocationLesson.IsLock = true;
                            return new ClassScheduleResult()
                            {
                                LessonId = Convert.ToInt32(universityProfessorCanAllocationLesson.LessonId),
                                GradeId = grade.Id,
                                UniversityMajorId = universityMajor.Id,
                                UniversityDepartmentId = universityDepartment.Id,
                                UniversityProfessorId = universityProfessor.Id,
                                WorkTimeInDayId = workTimeInDay.Id,
                                SemesterId = semester.Id,
                                ClassScheduleModeSpaceId = universityProfessorCanAllocationLesson.Id,
                                ClassroomBuildingId = lesson.ClassroomBuildingId,
                                TenantId = AbpSession.TenantId.Value
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    throw new Exception("the faculty Degree Not have course bilding!");
                }

            }
        }
        private async Task<bool> RelaseCourseToSemester(List<ClassScheduleResult> remain)
        {
            lock (_classScheduleModeSpace)
            {
                var universityProfessorCanAllocatedCourseList = ParallelEnumerable
                    .Where(
                          ParallelEnumerable.AsParallel(_classScheduleModeSpace),
                          record => remain.Any(row => row.ClassScheduleModeSpaceId == record.Id)) //
                         .WithMergeOptions(ParallelMergeOptions.FullyBuffered).WithExecutionMode(ParallelExecutionMode.ForceParallelism).ToList();
                if (universityProfessorCanAllocatedCourseList != null && universityProfessorCanAllocatedCourseList.Any())
                {
                    universityProfessorCanAllocatedCourseList.ForEach((rows) =>
                    {
                        rows.IsLock = false;
                    });
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        private async Task<List<ClassScheduleModeSpace>> GetWorkTimeInDayOFUniversityProfessor(Lesson lesson)
        {
            lock (_classScheduleModeSpace)
            {
                var universityProfessorCanAllocationLessonList = ParallelEnumerable
               .Where(
                     ParallelEnumerable.AsParallel(_classScheduleModeSpace),
                     record => lesson.Id == record.LessonId && record.IsLock == false)
                    .WithMergeOptions(ParallelMergeOptions.FullyBuffered).WithExecutionMode(ParallelExecutionMode.ForceParallelism).OrderBy(record => _universityProfessorWorkingTime.Count(record => record.UniversityProfessorId == record.UniversityProfessorId)).ThenBy(record => record.WorkTimeInDayFk.DayOfWeek).ThenBy(record => record.WorkTimeInDayFk.WhatTimeOfDayIndex).ToList();
                if (universityProfessorCanAllocationLessonList != null && universityProfessorCanAllocationLessonList.Any())
                {
                    var randomeWorkTimeInDayOFUniversityProfessor = universityProfessorCanAllocationLessonList.Take(lesson.HoursPerWeek).ToList();
                    if (randomeWorkTimeInDayOFUniversityProfessor != null && randomeWorkTimeInDayOFUniversityProfessor.Any())
                    {
                        List<ClassScheduleModeSpace> result = new List<ClassScheduleModeSpace>();
                        foreach (var rows in randomeWorkTimeInDayOFUniversityProfessor)
                        {
                            result.Add(new ClassScheduleModeSpace()
                            {
                                LessonId = Convert.ToInt32(rows.LessonId),
                                UniversityProfessorId = rows.UniversityProfessorId,
                                UniversityProfessorFk = rows.UniversityProfessorFk,
                                LessonFk = rows.LessonFk,
                                WorkTimeInDayFk = rows.WorkTimeInDayFk,
                                TenantId = rows.TenantId,
                                NameClassScheduleModeSpaces = rows.NameClassScheduleModeSpaces,
                                WorkTimeInDayId = rows.WorkTimeInDayId,
                                ListOfClassScheduleModeSpaceId = rows.ListOfClassScheduleModeSpaceId,
                                ListOfClassScheduleModeSpaceFk = rows.ListOfClassScheduleModeSpaceFk,
                                IsLock = rows.IsLock,
                                Id = rows.Id
                            });
                        }
                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }
        private async Task<List<ClassScheduleResult>> SetCoursesToSemester(Semester semester, int trushHoldeTry = 200, List<ClassScheduleResult> remain = null, List<MainDomain> semesterDomain = null)
        {
            if (trushHoldeTry <= 0)
            {
                return remain;
            }

            if (semesterDomain == null)
            {
                semesterDomain = new List<MainDomain>();
                var lessonsOfSemester = _lessonsOfSemester.Where(record => record.SemesterId == semester.Id).ToList();
                foreach (var Lesson in lessonsOfSemester)
                {
                    for (int numberOfClassesToBeFormedIndex = 0; numberOfClassesToBeFormedIndex < Lesson.NumberOfClassesToBeFormed; numberOfClassesToBeFormedIndex++)
                    {
                        for (int courseNumberOfGroupsIndex = 0; courseNumberOfGroupsIndex < Lesson.LessonFk.NumberOfGroups; courseNumberOfGroupsIndex++)
                        {
                            semesterDomain.Add(new MainDomain()
                            {
                                LessonsOfSemesterFk = Lesson,
                                TenantId = AbpSession.TenantId.Value,
                                LessonsOfSemesterId = Lesson.Id,
                                MainDomainName = Lesson.LessonsOfSemesterName
                            });
                        }
                    }
                }

            }
            if (remain == null)
            {
                remain = new List<ClassScheduleResult>();
                if (semesterDomain.Count <= 0)
                {
                    return remain;
                }
            }

            var mainLessonInSemester = semesterDomain.Where(record => record.LessonsOfSemesterFk.LessonOfSemesterType == LessonOfSemesterTypeEnum.Main).ToList();
            foreach (var lesson in mainLessonInSemester)
            {
                lock (_classScheduleModeSpace)
                {
                    var universityProfessorTask = GetWorkTimeInDayOFUniversityProfessor(lesson.LessonsOfSemesterFk.LessonFk);
                    universityProfessorTask.Wait();
                    var universityProfessor = universityProfessorTask.Result;
                    if (universityProfessor != null && universityProfessor.Any())
                    {
                        foreach (var rows in universityProfessor)
                        {
                            var lessonsDomainTask = AllocationCourseToSemester(lesson.LessonsOfSemesterFk.LessonFk, rows.WorkTimeInDayFk, rows.UniversityProfessorFk, semester, semester.AssigningGradeToUniversityMajorFk.GradeFk, semester.AssigningGradeToUniversityMajorFk.UniversityMajorFk, semester.AssigningGradeToUniversityMajorFk.UniversityMajorFk.UniversityDepartmentFk, remain);
                            lessonsDomainTask.Wait();
                            var coursesDomain = lessonsDomainTask.Result;
                            if (coursesDomain != null)
                            {
                                remain.Add(coursesDomain);
                            }
                        }
                    }
                }

            }
            var compensatoryLessonInSemester = semesterDomain.Where(record => record.LessonsOfSemesterFk.LessonOfSemesterType == LessonOfSemesterTypeEnum.Compensatory).ToList();
            foreach (var lesson in compensatoryLessonInSemester)
            {
                lock (_classScheduleModeSpace)
                {
                    var universityProfessorTask = GetWorkTimeInDayOFUniversityProfessor(lesson.LessonsOfSemesterFk.LessonFk);
                    universityProfessorTask.Wait();
                    var universityProfessor = universityProfessorTask.Result;
                    if (universityProfessor != null && universityProfessor.Any())
                    {
                        foreach (var rows in universityProfessor)
                        {
                            var coursesDomainTask = AllocationCourseToSemester(lesson.LessonsOfSemesterFk.LessonFk, rows.WorkTimeInDayFk, rows.UniversityProfessorFk, semester, semester.AssigningGradeToUniversityMajorFk.GradeFk, semester.AssigningGradeToUniversityMajorFk.UniversityMajorFk, semester.AssigningGradeToUniversityMajorFk.UniversityMajorFk.UniversityDepartmentFk, remain);
                            coursesDomainTask.Wait();
                            var coursesDomain = coursesDomainTask.Result;
                            if (coursesDomain != null)
                            {
                                remain.Add(coursesDomain);
                            }
                        }
                    }
                }
            }
            if (CalculatorH(remain, semesterDomain) < 90)
            {

                if (trushHoldeTry != 1)
                {
                    await RelaseCourseToSemester(remain);
                    remain.Clear();
                }
                return await SetCoursesToSemester(semester, (trushHoldeTry - 1), remain, semesterDomain);
            }
            else
            {
                return remain;
            }

        }
        private double CalculatorH(List<ClassScheduleResult> classScheduleResults, List<MainDomain> semesterDomain)
        {
            double price = 0;
            List<MainDomain> calculatoedLesson = new List<MainDomain>();
            if (classScheduleResults != null && semesterDomain != null)
            {
                foreach (var semesterLesson in semesterDomain)
                {
                    if (!calculatoedLesson.Any(record => record.LessonsOfSemesterFk.LessonId == semesterLesson.LessonsOfSemesterFk.LessonId && record.LessonsOfSemesterFk.SemesterId == semesterLesson.LessonsOfSemesterFk.SemesterId && record.LessonsOfSemesterFk.LessonOfSemesterType == semesterLesson.LessonsOfSemesterFk.LessonOfSemesterType) && (classScheduleResults.Count(record => record.LessonId == semesterLesson.LessonsOfSemesterFk.LessonId) == (semesterLesson.LessonsOfSemesterFk.NumberOfClassesToBeFormed * (semesterLesson.LessonsOfSemesterFk.LessonFk.NumberOfGroups * semesterLesson.LessonsOfSemesterFk.LessonFk.HoursPerWeek))))
                    {
                        price = price + (((semesterLesson.LessonsOfSemesterFk.NumberOfClassesToBeFormed * (semesterLesson.LessonsOfSemesterFk.LessonFk.NumberOfGroups)) * 100d) / semesterDomain.Count);
                        calculatoedLesson.Add(semesterLesson);
                    }
                }
            }

            return price;
        }

    }
}