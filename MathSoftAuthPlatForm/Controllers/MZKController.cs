using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MAZIKONG.Controllers
{
    public class MZKController : Controller
    {
        // GET: MZK
        public ActionResult Index()
        {
            return new RedirectResult("Admin/SysSetting/Index");
        }
    }
}