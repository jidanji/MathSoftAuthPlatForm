using AutoMapper;
using Business;
using DataAccess;
using DTOModel;
using MathSoftCommonLib;
using MathSoftModelLib;
using MAZIKONG.App_Start;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MAZIKONG.Areas.Admin.Controllers
{
    public class SysSettingController : AdminBaseController
    {
        [PreLoadMenuList]
        [Authorize]
        public ActionResult Index()
        {
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
            string whoOp = System.Web.HttpContext.Current.User.Identity.Name;

            UIModelData<SysSetting> model1 = new BLL_SysSetting().Update(whoOp);

            UI_SysSetting uI_SysSetting = Mapper.Map<UI_SysSetting>(model1.Data);
            return new JsonResult()
            {
                Data = uI_SysSetting,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}