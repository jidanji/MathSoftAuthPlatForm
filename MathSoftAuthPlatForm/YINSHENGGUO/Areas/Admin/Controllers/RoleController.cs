using AutoMapper;
using Business;
using DataAccess;
using DTOModel;
using MathSoftCommonLib;
using MathSoftModelLib;
using MAZIKONG.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace MAZIKONG.Areas.Admin.Controllers
{
    public class RoleController : AdminBaseController
    {
        [PreLoadMenuList]
        [Authorize]
        public ActionResult Index()
        {
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
        public ActionResult GetData(int draw, int start, int length, string RoleName)
        {
            Expression<Func<Math_RoleInfo, bool>> where = i => true;
            if (!string.IsNullOrWhiteSpace(RoleName))
            {
                where = i => i.RoleName.Contains(RoleName);
            }


           
            int total = 0;
            List<Math_RoleInfo> list = new BLL_Role().Search(start, length, out total, where);

            List<UIRoleModel> listUIRoleModel = new List<UIRoleModel>();
            foreach (Math_RoleInfo item in list)
            {
                UIRoleModel uIRoleModel = Mapper.Map<UIRoleModel>(item);

                //用户
                listUIRoleModel.Add(uIRoleModel);
                foreach (Math_User_Role_Select subitem in item.Math_User_Role_Select)
                {
                    uIRoleModel.UserList.Add(Mapper.Map<UIMathUserInfo>(subitem.Math_UserInfo));
                }

                //功能菜单
                foreach (Math_Role_Menu_Selcet subitem in item.Math_Role_Menu_Selcet)
                {
                    uIRoleModel.MenuList.Add(Mapper.Map<UI_Math_Menuinfo>(subitem.Math_MenuInfo));
                }
            }
            datatablesModel<UIRoleModel> datatablesModel = new datatablesModel<UIRoleModel>()
            {
                data = listUIRoleModel,
                draw = draw,
                recordsFiltered = total,
                recordsTotal = total
            };

            return new JsonResult() { Data = datatablesModel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Authorize]
        public ActionResult AddRole(string RoleName, string RoleRemark)
        {
            Math_RoleInfo role = new Math_RoleInfo()
            {
                RoleId = Guid.NewGuid(),
                RoleName = RoleName,
                RoleInsertTime = DateTime.Now,
                RoleUpdateTime = DateTime.Now,
                RoleRemark = RoleRemark
            };
            MathRoleAuthorEntities entities = new MathRoleAuthorEntities();
          
            UIModelData<Math_RoleInfo> uIModelData = new BLL_Role().Add(role, i => i.RoleName == RoleName);


            UIModelData<UIRoleModel> model = new UIModelData<UIRoleModel>()
            {
                Data = Mapper.Map<UIRoleModel>(uIModelData.Data),
                remark = uIModelData.remark,
                status = uIModelData.status,
                suc = uIModelData.suc

            };
            return new JsonResult() { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Authorize]
        public ActionResult UpdateRole(Guid RoleId, string roleName, string roleRemark)
        {
            Math_RoleInfo role = new Math_RoleInfo()
            {
                RoleId = Guid.NewGuid(),
                RoleName = roleName,
                RoleUpdateTime = DateTime.Now,
            };
            MathRoleAuthorEntities entities = new MathRoleAuthorEntities();
           
            UIModelData<Math_RoleInfo> uIModelData = new BLL_Role().Update(
                RoleId, 
                roleName, 
                roleRemark, 
                i => i.RoleId != RoleId && i.RoleName == roleName
                );

            UIModelData<UIRoleModel> model = new UIModelData<UIRoleModel>()
            {
                Data = Mapper.Map<UIRoleModel>(uIModelData.Data),
                remark = uIModelData.remark,
                status = uIModelData.status,
                suc = uIModelData.suc

            };


            return new JsonResult() { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Authorize]
        public ActionResult DeleteRole(Guid RoleId)
        {
            UIModelData<string> model = null;
            try
            {
                new BLL_Role().Delete(RoleId);
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
        public ActionResult DeleteRoleBatch(string RoleIds)
        {
            List<Guid> ids = JsonHelper.DeserializeObject<List<Guid>>(RoleIds);

            ids.ForEach(item => { new BLL_Role().Delete(item); });

            UIModelData<string> model1 = new UIModelData<string>()
            {
                remark = "成功删除" + ids.Count() + "条记录",
                suc = true,
                Data = string.Empty
            };
            return new JsonResult() { Data = model1, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Authorize]
        public ActionResult SendRole(Guid RoleId, string StringUserInfo)
        {
            UIModelData<string> model1 = null;
            try
            {
                new BLL_Role().SendRole(RoleId, StringUserInfo);
                model1 = new UIModelData<string>()
                {
                    remark = "福泉成功",
                    suc = true,
                    Data = string.Empty
                };
            }
            catch (Exception ex)
            {
                model1 = new UIModelData<string>()
                {
                    remark = ex.Message,
                    suc = false,
                    Data = ex.Message
                };
            }

            return new JsonResult() { Data = model1, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Authorize]
        public ActionResult SendMenu(Guid RoleId, string StringMenuInfo)
        {
            UIModelData<string> model1 = null;

            try
            {
                new BLL_Role().SendMenu(RoleId, StringMenuInfo);

                model1 = new UIModelData<string>()
                {
                    remark = "功能授予成功",
                    suc = true,
                    Data = string.Empty
                };
            }
            catch (Exception ex)
            {
                model1 = new UIModelData<string>()
                {
                    remark = ex.Message,
                    suc = false,
                    Data = ex.Message
                };
            }

            return new JsonResult() { Data = model1, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}