using AutoMapper;
using Business;
using DataAccess;
using DTOModel;
using MathSoftCommonLib;
using MathSoftModelLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace MAZIKONG.Areas.Admin.Controllers
{
    public class DepartmentController : AdminBaseController
    {
        [Authorize]
        public ActionResult Index()
        {
            int tsthh = 0;
            List<View_User_Menu> View_User_Menul = new BLL_View_User_Menu().Search(-1,
                -1,
                out tsthh,
                i => i.UserAccount == System.Web.HttpContext.Current.User.Identity.Name);
            List<UI_Math_Menuinfo> lllll = View_User_Menul.Select(item => Mapper.Map<UI_Math_Menuinfo>(item)).ToList();

            ViewBag.MenuList = lllll;
            return View();
        }

        [Authorize]
        public ActionResult GetData(int draw, int start, int length, string Dept_Name)
        {
            Expression<Func<Math_Deptinfo, bool>> where = i => true;
            if (!string.IsNullOrWhiteSpace(Dept_Name))
            {
                where = i => i.Math_Dept_Name.Contains(Dept_Name);
            }


           
            int total = 0;

            List<Math_Deptinfo> list = new BLL_Math_Deptinfo().Search(start, length, out total, where);

            List<UI_Math_Deptinfo> listUIRoleModel= list.Select(item => Mapper.Map<UI_Math_Deptinfo>(item)).ToList();

            datatablesModel<UI_Math_Deptinfo> datatablesModel = new datatablesModel<UI_Math_Deptinfo>()
            {
                data = listUIRoleModel,
                draw = draw,
                recordsFiltered = total,
                recordsTotal = total
            };

            return new JsonResult() { Data = datatablesModel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Authorize]
        public ActionResult AddDept(string Math_Dept_Name, string Math_Dept_Remark)
        {

            MathRoleAuthorEntities entities = new MathRoleAuthorEntities();
            BLL_Math_Deptinfo bLL_Role = new BLL_Math_Deptinfo();
            UIModelData<Math_Deptinfo> uIModelData = bLL_Role.Add(Math_Dept_Name, Math_Dept_Remark, i => i.Math_Dept_Name == Math_Dept_Name);
            UIModelData<UI_Math_Deptinfo> model = new UIModelData<UI_Math_Deptinfo>
            {
                Data = Mapper.Map<UI_Math_Deptinfo>(uIModelData.Data),
                remark = uIModelData.remark,
                status = uIModelData.status,
                suc = uIModelData.suc
            };
            return new JsonResult() { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Authorize]
        public ActionResult UpdateDept(Guid Math_Dept_Id, string Math_Dept_Name, string Math_Dept_Remark)
        {

            MathRoleAuthorEntities entities = new MathRoleAuthorEntities();
            BLL_Math_Deptinfo bLL_Role = new BLL_Math_Deptinfo();
            UIModelData<Math_Deptinfo> uIModelData = bLL_Role.Update(Math_Dept_Id, Math_Dept_Name, Math_Dept_Remark,
                i => i.Math_Dept_Id != Math_Dept_Id && i.Math_Dept_Name == Math_Dept_Name);

            UIModelData<UI_Math_Deptinfo> model = new UIModelData<UI_Math_Deptinfo>
            {
                Data = Mapper.Map<UI_Math_Deptinfo>(uIModelData.Data),
                remark = uIModelData.remark,
                status = uIModelData.status,
                suc = uIModelData.suc

            };
            return new JsonResult() { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Authorize]
        public ActionResult DeleteDept(Guid Math_Dept_Id)
        {
            UIModelData<string> model = null;
            try
            {
                BLL_Math_Deptinfo bll = new BLL_Math_Deptinfo();
                bll.Delete(Math_Dept_Id);
                model = new UIModelData<string>()
                {
                    Data = string.Empty,
                    remark = OpCommonString.DeleteSuccess,
                    suc = true
                };
            }

            catch (Exception ex)
            {
                model = new UIModelData<string>()
                {
                    Data = string.Empty,
                    remark = "删除失败" + ex.Message,
                    suc = false
                };
            }
            return new JsonResult() { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Authorize]
        public ActionResult DeleteDeptBatch(string RoleIds)
        {
            List<Guid> ids = JsonHelper.DeserializeObject<List<Guid>>(RoleIds);
            foreach (Guid item in ids)
            {
                BLL_Math_Deptinfo  bll = new  BLL_Math_Deptinfo ();
                bll.Delete(item);
            }

            UIModelData<string> model1 = new UIModelData<string>()
            {
                remark = "成功删除" + ids.Count() + "条记录",
                suc = true,
                Data = string.Empty
            };
            return new JsonResult() { Data = model1, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}