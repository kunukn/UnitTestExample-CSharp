using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyApp.Controllers;
using MyApp.Interfaces;
using Rhino.Mocks;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace UnitTestMyApp
{
    /// <summary>
    /// Naming standard: MethodName_StateUnderTest_ExpectedBehavior
    /// </summary>
    [TestClass]
    public class DataPropertyControllerTest
    {
        // Arrange
        private IDataService dataService;
        private IReportService reportService;

        [TestInitialize]
        public void Setup()
        {
            dataService = MockRepository.GenerateStub<IDataService>();
            reportService = MockRepository.GenerateStub<IReportService>();

            dataService
                .Stub(s => s.GetData(Arg<string>.Is.Anything))
                .Return(new List<string>());
        }

        [TestMethod]
        public void GetData_WhenCalledWithAnythingButFoo_ReturnsJsonResult()
        {
            // Arrange            
            var controller = new DataPropertyController
            {
                DataService = dataService,
                ReportService = reportService
            };

            // act
            ActionResult news = controller.GetData(subject: "news");
            ActionResult fooish = controller.GetData(subject: "fooish");

            // Assert            
            Assert.IsNotNull(news as JsonResult);
            Assert.IsNotNull(fooish as JsonResult);
            
            dataService
                .AssertWasCalled(s => s.GetData(Arg<string>.Is.Anything));
            
            reportService
                .AssertWasNotCalled(s => s.ReportAbuseUsage(Arg<string>.Is.Anything));
        }


        [TestMethod]
        public void GetData_WhenCalledWithFoo_ReturnsHttpDenied()
        {
            // Arrange            
            var controller = new DataPropertyController()
            {
                DataService = dataService,
                ReportService = reportService
            };

            var forbidden = new HttpStatusCodeResult(HttpStatusCode.Forbidden);

            // act
            ActionResult foo = controller.GetData(subject: "foo");
            var fooHttpStatusCodeResult = foo as HttpStatusCodeResult;

            // Assert            
            Assert.IsNotNull(fooHttpStatusCodeResult);
            Assert.AreEqual(fooHttpStatusCodeResult.StatusCode, forbidden.StatusCode);
            
            dataService
                .AssertWasCalled(s => s.GetData(Arg<string>.Is.Anything));
            
            reportService
                .AssertWasCalled(s => s.ReportAbuseUsage(Arg<string>.Is.Anything));
        }
    }
}
