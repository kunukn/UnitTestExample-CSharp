using MyApp.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using MyApp.Services;

namespace MyApp.Controllers
{
    public class DataConstructorController : Controller
    {
        private readonly IDataService dataService;
        private readonly IReportService reportService;

        public DataConstructorController(IDataService dataService, IReportService reportService)
        {
            this.dataService = dataService;
            this.reportService = reportService;
        }

        public DataConstructorController()
            : this(new DataService(), new ReportService())
        {
        }

        public ActionResult GetData(string subject)
        {
            if (subject == "foo")
            {
                reportService.ReportAbuseUsage(subject);
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            IList<string> data = dataService.GetData(subject);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
