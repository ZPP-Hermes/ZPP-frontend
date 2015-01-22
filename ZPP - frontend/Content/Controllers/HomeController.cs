using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;

namespace ZPP___frontend.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            string json = @"{'kwejk':null, 'polska':{'1':null}}";
            JObject res = JObject.Parse(json);
            JToken j = res["kwejk"];
            return View();
        }
    }
}