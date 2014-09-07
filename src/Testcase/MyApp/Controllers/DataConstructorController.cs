using MyApp.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

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
        
        public ActionResult GetData(string subject)
        {
            IList<string> data = dataService.GetData(subject);
            
            if (subject == "foo")
            {
                reportService.ReportAbuseUsage(subject);
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
