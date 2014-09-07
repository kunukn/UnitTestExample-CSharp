﻿using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using MyApp.Services;

namespace MyApp.Controllers
{
    public class DataController : Controller
    {              
        public ActionResult GetData(string subject)
        {
            IList<string> data = new DataService().GetData(subject);
            
            if (subject == "foo")
            {
                new ReportService().ReportAbuseUsage(subject);
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
