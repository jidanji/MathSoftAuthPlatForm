using System.Collections.Generic;
using System.Web.Mvc;

namespace MAZIKONG.Areas.Admin.Controllers
{
    public class TempController : AdminBaseController
    {
        // GET: Admin/Temp
        public ActionResult Index()
        {
            List<SelectListItem> listItem = new List<SelectListItem>{
                new SelectListItem{Text="是",Value="1"},
                new SelectListItem{Text="否",Value="0"}
            };
            ViewBag.List = new SelectList(listItem, "Value", "Text", "");
            return View();
        }
    }
}