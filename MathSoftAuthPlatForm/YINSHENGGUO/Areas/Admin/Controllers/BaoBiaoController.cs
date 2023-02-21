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
    public class BaoBiaoController : AdminBaseController
    {
        [PreLoadMenuList]
        [Authorize]
        public ActionResult Index()
        {
            //这个是部门信息的

            Math_UserInfo userinfo = new BLL_User().GetSingleByUserAccount(System.Web.HttpContext.Current.User.Identity.Name);

            List<UI_Math_Deptinfo> list1 = new List<UI_Math_Deptinfo>();
            if (userinfo.User_Area != 2)
            {
                //本部门
                list1.Add(Mapper.Map<UI_Math_Deptinfo>(userinfo.Math_Deptinfo));
            }
            else
            {
                int total = 0;
                List<Math_Deptinfo> list = new BLL_Math_Deptinfo().Search(-1, -1, out total, i => true);
                list.ForEach(item => { list1.Add(Mapper.Map<UI_Math_Deptinfo>(item)); });
            }
            ViewBag.DeptList = list1;
            return View();
        }

        [Authorize]
        public ActionResult GetData(string searchBegDate, string searchEndDate, string searchDept, string searchStaticType)
        {
            string loginName = System.Web.HttpContext.Current.User.Identity.Name;
            List<UIStatic> list = new BLL_BaoBiao().Search(loginName,searchBegDate, searchEndDate, searchDept, searchStaticType);
            datatablesModel<UIStatic> retModel = new datatablesModel<UIStatic>()
            {
                data = list,
                draw = int.Parse(Request["draw"]),
                recordsFiltered = list.Count,
                recordsTotal = list.Count
            };

            return new JsonResult() { Data = retModel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Authorize]
        public ActionResult GetZhaoShengBaoBiao()
        {
            List<UIZHaoShengBaobiao> list = new BLL_BaoBiao().GetZhaoShengBaoBiao();
            datatablesModel<UIZHaoShengBaobiao> retModel = new datatablesModel<UIZHaoShengBaobiao>()
            {
                data = list,
                draw = int.Parse(Request["draw"]),
                recordsFiltered = list.Count,
                recordsTotal = list.Count
            };
            return new JsonResult() { Data = retModel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Authorize]
        public ActionResult GetZhaoShengBaoBiaoByMonth()
        {
            List<UIZHaoShengBaobiao> list = new BLL_BaoBiao().GetZhaoShengBaoBiaoByMonth();
            datatablesModel<UIZHaoShengBaobiao> retModel = new datatablesModel<UIZHaoShengBaobiao>()
            {
                data = list,
                draw = int.Parse(Request["draw"]),
                recordsFiltered = list.Count,
                recordsTotal = list.Count
            };
            return new JsonResult() { Data = retModel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Authorize]
        public ActionResult GetZhaoShengBaoBiaoByTuiJianRen()
        {
            List<UIZHaoShengBaobiao> list = new BLL_BaoBiao().GetZhaoShengBaoBiaoByTuiJianRen();
            datatablesModel<UIZHaoShengBaobiao> retModel = new datatablesModel<UIZHaoShengBaobiao>()
            {
                data = list,
                draw = int.Parse(Request["draw"]),
                recordsFiltered = list.Count,
                recordsTotal = list.Count
            };
            return new JsonResult() { Data = retModel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Authorize]
        public ActionResult GetZhaoShengBaoBiaoByZhuanYe()
        {
            List<UIZHaoShengBaobiao> list = new BLL_BaoBiao().GetZhaoShengBaoBiaoByZhuanYe();
            datatablesModel<UIZHaoShengBaobiao> retModel = new datatablesModel<UIZHaoShengBaobiao>()
            {
                data = list,
                draw = int.Parse(Request["draw"]),
                recordsFiltered = list.Count,
                recordsTotal = list.Count
            };
            return new JsonResult() { Data = retModel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Authorize]
        public ActionResult GetZhaoShengBaoBiaoBySex()
        {
            List<UIZHaoShengBaobiao> list = new BLL_BaoBiao().GetZhaoShengBaoBiaoBySex();
            datatablesModel<UIZHaoShengBaobiao> retModel = new datatablesModel<UIZHaoShengBaobiao>()
            {
                data = list,
                draw = int.Parse(Request["draw"]),
                recordsFiltered = list.Count,
                recordsTotal = list.Count
            };
            return new JsonResult() { Data = retModel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Authorize]
        public ActionResult GetZhaoShengBaoBiaoByArea()
        {
            List<UIZHaoShengBaobiao> list = new BLL_BaoBiao().GetZhaoShengBaoBiaoByArea();
            datatablesModel<UIZHaoShengBaobiao> retModel = new datatablesModel<UIZHaoShengBaobiao>()
            {
                data = list,
                draw = int.Parse(Request["draw"]),
                recordsFiltered = list.Count,
                recordsTotal = list.Count
            };
            return new JsonResult() { Data = retModel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Authorize]
        public ActionResult GetZhaoShengBaoBiaoByStudentType()
        {
            List<UIZHaoShengBaobiao> list = new BLL_BaoBiao().GetZhaoShengBaoBiaoByStudentType();
            datatablesModel<UIZHaoShengBaobiao> retModel = new datatablesModel<UIZHaoShengBaobiao>()
            {
                data = list,
                draw = int.Parse(Request["draw"]),
                recordsFiltered = list.Count,
                recordsTotal = list.Count
            };
            return new JsonResult() { Data = retModel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}