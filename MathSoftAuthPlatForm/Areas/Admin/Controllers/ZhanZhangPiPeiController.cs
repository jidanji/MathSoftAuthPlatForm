using AutoMapper;
using Business;
using DataAccess;
using DTOModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using MathSoftCommonLib;
using MathSoftModelLib;

namespace MAZIKONG.Areas.Admin.Controllers
{
    public class ZhanZhangPiPeiController : Controller
    {
        [Authorize]
        // GET: Admin/ZhanZhangPiPei
        public ActionResult Index()
        {
            string userName = System.Web.HttpContext.Current.User.Identity.Name;
            int tsthh = 0;
            BLL_View_User_Menu bLL_View_User_Menu = new BLL_View_User_Menu();
            List<View_User_Menu> View_User_Menul = bLL_View_User_Menu.Search(-1, -1, out tsthh, i => i.UserAccount == userName);
            List<UI_Math_Menuinfo> lllll = View_User_Menul.Select(item => Mapper.Map<UI_Math_Menuinfo>(item)).ToList();
            ViewBag.MenuList = lllll;



            int total = 0;

            //用户相关的信息
            List<Math_UserInfo> list = new BLL_User().Search(-1, -1, out total, i => true);
            List<UIMathUserInfo> list1 = list.Select(item => Mapper.Map<UIMathUserInfo>(item)).ToList();

            ViewBag.UserList = list1;

            //功能相关的信息
            List<Math_MenuInfo> menulist = new BLL_Math_MenuInfo().Search(-1, -1, out total, i => true);
            List<UI_Math_Menuinfo> uimenulist = menulist.Select(item => Mapper.Map<UI_Math_Menuinfo>(item)).ToList();

            ViewBag.MenuList1 = uimenulist;

            return View();
        }

        [Authorize]
        public ActionResult GetData()
        {
            BLL_Math_ZZ bLL_Math_ZZ = new BLL_Math_ZZ();
            List<gojsModal> list = bLL_Math_ZZ.GetGojsList();
            string ssss = string.Empty;
            if (System.IO.File.Exists(Server.MapPath("~/upload/10000.txt")))
            {
                ssss= System.IO.File.ReadAllText(Server.MapPath("~/upload/10000.txt"));
            }
            else
            {
                System.IO.File.WriteAllText(Server.MapPath("~/upload/10000.txt"), JsonHelper.SerializeObject(new { nodeDataArray = list }));
                ssss = System.IO.File.ReadAllText(Server.MapPath("~/upload/10000.txt"));
                BLL_Math_ZZTARGET bLL_Math_ZZTARGET = new BLL_Math_ZZTARGET();
                gojsDiaModal cmd = new gojsDiaModal { nodeDataArray = list };
                bLL_Math_ZZTARGET.trans(cmd);
            }


            return new JsonResult() { Data = ssss, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Authorize]
        public ActionResult SaveData(string flowstr)
        {
            System.IO.File.WriteAllText(Server.MapPath("~/upload/10000.txt"), flowstr);
            gojsDiaModal cmd = JsonHelper.DeserializeObject<gojsDiaModal>(flowstr);
            BLL_Math_ZZTARGET bLL_Math_ZZTARGET = new BLL_Math_ZZTARGET();
            bLL_Math_ZZTARGET.trans(cmd);
            return new JsonResult() { Data = 1, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}