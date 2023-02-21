using AutoMapper;
using Business;
using DataAccess;
using DTOModel;
using MathSoftModelLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MAZIKONG.Areas.Admin.Controllers
{
    public class EventController : AdminBaseController
    {
        [Authorize]
        // GET: Admin/Event
        public ActionResult Index()
        {
           
            int tsthh = 0;
            
            List<View_User_Menu> View_User_Menul = new BLL_View_User_Menu().Search(
                -1,
                -1,
                out tsthh,
                i => i.UserAccount == System.Web.HttpContext.Current.User.Identity.Name
                );
            List<UI_Math_Menuinfo> lllll = View_User_Menul.Select(item => Mapper.Map<UI_Math_Menuinfo>(item)).ToList();



            ViewBag.MenuList = lllll;


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