using MyApp.Interfaces;
using MyApp.Services;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace MyApp.Controllers
{
    public class DataControllerProperty : Controller
    {
        private IDataService _dataService;
        public IDataService DataService
        {
            get { return _dataService ?? new DataService(); }
            set { _dataService = _dataService ?? value; }
        }

        private IReportService _reportService;
        public IReportService ReportService
        {
            get { return _reportService ?? new ReportService(); }
            set { _reportService = _reportService ?? value; }
        }
        
        public ActionResult GetData(string subject)
        {
            IList<string> data = DataService.GetData(subject);
            
            if (subject == "foo")
            {
                ReportService.ReportAbuseUsage(subject);
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
