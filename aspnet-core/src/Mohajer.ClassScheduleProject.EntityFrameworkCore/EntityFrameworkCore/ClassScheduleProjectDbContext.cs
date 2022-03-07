using Mohajer.ClassScheduleProject.CentralUnit.LessonsOfUniversityProfessors;
using Mohajer.ClassScheduleProject.CentralUnit.LessonsOfSemesters;
using Mohajer.ClassScheduleProject.CentralUnit.Lessons;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessorWorkingTimes;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessors;
using Mohajer.ClassScheduleProject.CentralUnit.WorkTimeInDays;
using Mohajer.ClassScheduleProject.CentralUnit.AssigningGradeToUniversityMajors;
using Mohajer.ClassScheduleProject.CentralUnit.Semesters;
using Mohajer.ClassScheduleProject.CentralUnit.Grades;
using Mohajer.ClassScheduleProject.CentralUnit.AssigningUniversityMajorToClassroomBuildings;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityDepartments;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityMajors;
using Mohajer.ClassScheduleProject.CentralUnit.ClassroomBuildings;
using Abp.IdentityServer4vNext;
using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mohajer.ClassScheduleProject.Authorization.Delegation;
using Mohajer.ClassScheduleProject.Authorization.Roles;
using Mohajer.ClassScheduleProject.Authorization.Users;
using Mohajer.ClassScheduleProject.Chat;
using Mohajer.ClassScheduleProject.Editions;
using Mohajer.ClassScheduleProject.Friendships;
using Mohajer.ClassScheduleProject.MultiTenancy;
using Mohajer.ClassScheduleProject.MultiTenancy.Accounting;
using Mohajer.ClassScheduleProject.MultiTenancy.Payments;
using Mohajer.ClassScheduleProject.Storage;
using Mohajer.ClassScheduleProject.Notifications;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace Mohajer.ClassScheduleProject.EntityFrameworkCore
{
    public class ClassScheduleProjectDbContext : AbpZeroDbContext<Tenant, Role, User, ClassScheduleProjectDbContext>, IAbpPersistedGrantDbContext
    {
        public virtual DbSet<LessonsOfUniversityProfessor> LessonsOfUniversityProfessors { get; set; }

        public virtual DbSet<LessonsOfSemester> LessonsOfSemesters { get; set; }

        public virtual DbSet<Lesson> Lessons { get; set; }

        public virtual DbSet<UniversityProfessorWorkingTime> UniversityProfessorWorkingTimes { get; set; }

        public virtual DbSet<UniversityProfessor> UniversityProfessors { get; set; }

        public virtual DbSet<WorkTimeInDay> WorkTimeInDays { get; set; }

        public virtual DbSet<AssigningGradeToUniversityMajor> AssigningGradeToUniversityMajors { get; set; }

        public virtual DbSet<Semester> Semesters { get; set; }

        public virtual DbSet<Grade> Grades { get; set; }

        public virtual DbSet<AssigningUniversityMajorToClassroomBuilding> AssigningUniversityMajorToClassroomBuildings { get; set; }

        public virtual DbSet<UniversityDepartment> UniversityDepartments { get; set; }

        public virtual DbSet<UniversityMajor> UniversityMajors { get; set; }

        public virtual DbSet<ClassroomBuilding> ClassroomBuildings { get; set; }

        /* Define an IDbSet for each entity of the application */

        public virtual DbSet<BinaryObject> BinaryObjects { get; set; }

        public virtual DbSet<Friendship> Friendships { get; set; }

        public virtual DbSet<ChatMessage> ChatMessages { get; set; }

        public virtual DbSet<SubscribableEdition> SubscribableEditions { get; set; }

        public virtual DbSet<SubscriptionPayment> SubscriptionPayments { get; set; }

        public virtual DbSet<Invoice> Invoices { get; set; }

        public virtual DbSet<PersistedGrantEntity> PersistedGrants { get; set; }

        public virtual DbSet<SubscriptionPaymentExtensionData> SubscriptionPaymentExtensionDatas { get; set; }

        public virtual DbSet<UserDelegation> UserDelegations { get; set; }

        public ClassScheduleProjectDbContext(DbContextOptions<ClassScheduleProjectDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LessonsOfUniversityProfessor>(l =>
            {
                l.HasIndex(e => new { e.TenantId });
            });
            modelBuilder.Entity<LessonsOfSemester>(l =>
                       {
                           l.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<Lesson>(l =>
                       {
                           l.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<UniversityProfessorWorkingTime>(u =>
                       {
                           u.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<UniversityProfessor>(u =>
                       {
                           u.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<WorkTimeInDay>(w =>
                       {
                           w.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<Semester>(s =>
                       {
                           s.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<AssigningGradeToUniversityMajor>(a =>
                       {
                           a.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<Semester>(s =>
                       {
                           s.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<Grade>(g =>
                       {
                           g.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<AssigningUniversityMajorToClassroomBuilding>(a =>
                       {
                           a.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<UniversityMajor>(u =>
                       {
                           u.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<UniversityDepartment>(u =>
                       {
                           u.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<ClassroomBuilding>(c =>
                       {
                           c.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<UniversityMajor>(u =>
                       {
                           u.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<UniversityDepartment>(u =>
                       {
                           u.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<ClassroomBuilding>(c =>
                       {
                           c.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<UniversityMajor>(u =>
                       {
                           u.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<UniversityDepartment>(u =>
                       {
                           u.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<UniversityMajor>(u =>
                       {
                           u.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<ClassroomBuilding>(c =>
                       {
                           c.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<BinaryObject>(b =>
                       {
                           b.HasIndex(e => new { e.TenantId });
                       });

            modelBuilder.Entity<ChatMessage>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId, e.ReadState });
                b.HasIndex(e => new { e.TenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.UserId, e.ReadState });
            });

            modelBuilder.Entity<Friendship>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId });
                b.HasIndex(e => new { e.TenantId, e.FriendUserId });
                b.HasIndex(e => new { e.FriendTenantId, e.UserId });
                b.HasIndex(e => new { e.FriendTenantId, e.FriendUserId });
            });

            modelBuilder.Entity<Tenant>(b =>
            {
                b.HasIndex(e => new { e.SubscriptionEndDateUtc });
                b.HasIndex(e => new { e.CreationTime });
            });

            modelBuilder.Entity<SubscriptionPayment>(b =>
            {
                b.HasIndex(e => new { e.Status, e.CreationTime });
                b.HasIndex(e => new { PaymentId = e.ExternalPaymentId, e.Gateway });
            });

            modelBuilder.Entity<SubscriptionPaymentExtensionData>(b =>
            {
                b.HasQueryFilter(m => !m.IsDeleted)
                    .HasIndex(e => new { e.SubscriptionPaymentId, e.Key, e.IsDeleted })
                    .IsUnique();
            });

            modelBuilder.Entity<UserDelegation>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.SourceUserId });
                b.HasIndex(e => new { e.TenantId, e.TargetUserId });
            });

            modelBuilder.ConfigurePersistedGrantEntity();
        }
    }
}