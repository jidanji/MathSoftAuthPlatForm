using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using MathSoftCommonLib;
using MathSoftModelLib;
using Business;
using DTOModel;
using DataAccess;
using AutoMapper;

namespace MAZIKONG.Areas.Admin.Controllers
{
    public class SysSettingController : AdminBaseController
    {
        [Authorize]
        public ActionResult Index()
        {
            string userName = System.Web.HttpContext.Current.User.Identity.Name;
            int tsthh = 0;

            List<View_User_Menu> View_User_Menul = new BLL_View_User_Menu().Search(-1, -1, out tsthh, i => i.UserAccount == userName);
            List<UI_Math_Menuinfo> lllll = View_User_Menul.Select(item => Mapper.Map<UI_Math_Menuinfo>(item)).ToList();
            ViewBag.MenuList = lllll;


            int total = 0;
            List<Math_DictType> list = new BLL_Math_DictType().Search(-1, -1, out total, i => true);
            List<UIMath_DictType> list1 = list.Select(item => Mapper.Map<UIMath_DictType>(item)).ToList();


            ViewBag.list = new SelectList(list1, "DictTypeId", "DictTypeValue", "");

            return View();
        }

        [Authorize]
        public ActionResult GetData()
        {
             
            UI_SysSetting uI_SysSetting = new BLL_SysSetting().GetStauts();
            return new JsonResult()
            {
                Data = uI_SysSetting,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [Authorize]
        public ActionResult Update()
        {

            UIModelData<SysSetting> model1 = new BLL_SysSetting().Update();

            UI_SysSetting uI_SysSetting = Mapper.Map<UI_SysSetting>(model1.Data);
            return new JsonResult()
            {
                Data = uI_SysSetting,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}