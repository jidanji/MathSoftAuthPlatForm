using AutoMapper;
using Business;
using DataAccess;
using DTOModel;
using MathSoftModelLib;
using MAZIKONG.App_Start;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MAZIKONG.Areas.Admin.Controllers
{
    public class EventController : AdminBaseController
    {
        [PreLoadMenuList]
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult GetData()
        {
            var data = new BLL_Event().GetData();
            datatablesModel<UIEventInfo> model = new datatablesModel<UIEventInfo>() { data = data, draw = int.Parse(Request["draw"]), recordsFiltered = 100, recordsTotal = 100 };
            return new JsonResult() { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}