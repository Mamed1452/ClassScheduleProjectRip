using Mohajer.ClassScheduleProject.Auditing;
using Mohajer.ClassScheduleProject.Test.Base;
using Shouldly;
using Xunit;

namespace Mohajer.ClassScheduleProject.Tests.Auditing
{
    // ReSharper disable once InconsistentNaming
    public class NamespaceStripper_Tests: AppTestBase
    {
        private readonly INamespaceStripper _namespaceStripper;

        public NamespaceStripper_Tests()
        {
            _namespaceStripper = Resolve<INamespaceStripper>();
        }

        [Fact]
        public void Should_Stripe_Namespace()
        {
            var controllerName = _namespaceStripper.StripNameSpace("Mohajer.ClassScheduleProject.Web.Controllers.HomeController");
            controllerName.ShouldBe("HomeController");
        }

        [Theory]
        [InlineData("Mohajer.ClassScheduleProject.Auditing.GenericEntityService`1[[Mohajer.ClassScheduleProject.Storage.BinaryObject, Mohajer.ClassScheduleProject.Core, Version=1.10.1.0, Culture=neutral, PublicKeyToken=null]]", "GenericEntityService<BinaryObject>")]
        [InlineData("CompanyName.ProductName.Services.Base.EntityService`6[[CompanyName.ProductName.Entity.Book, CompanyName.ProductName.Core, Version=1.10.1.0, Culture=neutral, PublicKeyToken=null],[CompanyName.ProductName.Services.Dto.Book.CreateInput, N...", "EntityService<Book, CreateInput>")]
        [InlineData("Mohajer.ClassScheduleProject.Auditing.XEntityService`1[Mohajer.ClassScheduleProject.Auditing.AService`5[[Mohajer.ClassScheduleProject.Storage.BinaryObject, Mohajer.ClassScheduleProject.Core, Version=1.10.1.0, Culture=neutral, PublicKeyToken=null],[Mohajer.ClassScheduleProject.Storage.TestObject, Mohajer.ClassScheduleProject.Core, Version=1.10.1.0, Culture=neutral, PublicKeyToken=null],]]", "XEntityService<AService<BinaryObject, TestObject>>")]
        public void Should_Stripe_Generic_Namespace(string serviceName, string result)
        {
            var genericServiceName = _namespaceStripper.StripNameSpace(serviceName);
            genericServiceName.ShouldBe(result);
        }
    }
}
