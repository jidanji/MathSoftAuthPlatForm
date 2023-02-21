using AutoMapper;
using Business;
using DataAccess;
using DTOModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MAZIKONG.Areas.Admin.Controllers
{
    public class MainController : AdminBaseController
    {
        [Authorize]
        public ActionResult Index()
        {
            string userName = System.Web.HttpContext.Current.User.Identity.Name;
            int tsthh = 0;
            BLL_View_User_Menu bLL_View_User_Menu = new BLL_View_User_Menu();
            List<View_User_Menu> View_User_Menul = bLL_View_User_Menu.Search(-1, -1, out tsthh, i => i.UserAccount == userName);
            List<UI_Math_Menuinfo> lllll = View_User_Menul.Select(item => Mapper.Map<UI_Math_Menuinfo>(item)).ToList();


            ViewBag.MenuList = lllll;
            return View();
        }
    }
}