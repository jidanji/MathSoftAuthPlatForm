using AutoMapper;

using MathSoftModelLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

using MathSoftCommonLib;
using DataAccess;
using Business;
using DTOModel;

namespace MAZIKONG.Areas.Admin.Controllers
{
    public class MathDictController : AdminBaseController
    {
        [Authorize]
        public ActionResult Index()
        {
            int tsthh = 0;
            List<View_User_Menu> View_User_Menul = new BLL_View_User_Menu().Search(
                -1,
                -1,
                out tsthh,
                i => i.UserAccount == System.Web.HttpContext.Current.User.Identity.Name);
            List<UI_Math_Menuinfo> lllll = View_User_Menul.Select(item => Mapper.Map<UI_Math_Menuinfo>(item)).ToList();


            ViewBag.MenuList = lllll;

            BLL_Math_DictType bll = new BLL_Math_DictType();
            int total = 0;
            List<Math_DictType> list = bll.Search(-1, -1, out total, i => true);
            List<UIMath_DictType> list1 = list.Select(item => Mapper.Map<UIMath_DictType>(item)).ToList();
            ViewBag.list = new SelectList(list1, "DictTypeId", "DictTypeValue", "");
            return View();
        }

        [Authorize]
        public ActionResult GetData(int draw, int start, int length, string DictValue, Guid? DictTypeId,string DictRemark)
        {
            Expression<Func<Math_View_DictAndType, bool>> where1 = i => true;
            if (!string.IsNullOrWhiteSpace(DictValue))
            {
                where1 = i => i.DictValue.Contains(DictValue);
            }

            Expression<Func<Math_View_DictAndType, bool>> where2 = i => true;

            if (DictTypeId != null)
            {
                where2 = i => i.DictTypeId == DictTypeId;
            }

            Expression<Func<Math_View_DictAndType, bool>> where3 = i => true;

            if (!string.IsNullOrEmpty(DictRemark))
            {
                where3 = i => i.DictRemark.Contains(DictRemark);
            }


            int total = 0;
            List<Math_View_DictAndType> list = new BLL_Math_View_DictAndType().Search(start, length, out total, where1.And(where2).And(where3));

            List<UIMath_View_DictAndType> listUIRoleModel = new List<UIMath_View_DictAndType>();
            foreach (Math_View_DictAndType item in list)
            {
                listUIRoleModel.Add(Mapper.Map<UIMath_View_DictAndType>(item));
            }
            datatablesModel<UIMath_View_DictAndType> datatablesModel = new datatablesModel<UIMath_View_DictAndType>()
            {
                data = listUIRoleModel,
                draw = draw,
                recordsFiltered = total,
                recordsTotal = total
            };
            return new JsonResult() { Data = datatablesModel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Authorize]
        public ActionResult AddMathDict(string DictValue, Guid DictTypeId, string DictRemark)
        {
            BLL_Math_Dict bll = new BLL_Math_Dict();
            UIModelData<Math_Dict> model = bll.Add(new Math_Dict()
            {
                DictRemark = DictRemark,
                DictTypeId = DictTypeId,
                DictValue = DictValue,
                DictId = Guid.NewGuid()
            }, i =>

i.DictValue == DictValue
&& i.DictTypeId == DictTypeId
            );
            UIModelData<UIMath_Dict> model1 = new UIModelData<UIMath_Dict>()
            {
                remark = model.remark,
                status = model.status,
                suc = model.suc,
                Data = Mapper.Map<UIMath_Dict>(model.Data)
            };

            return new JsonResult() { Data = model1, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Authorize]
        public ActionResult UpdateMathDict(Guid DictId, string DictValue, Guid DictTypeId, string DictRemark)
        {
            BLL_Math_Dict bll = new BLL_Math_Dict();
            UIModelData<Math_Dict> model = bll.Update(DictId, DictValue, DictTypeId, DictRemark,
                i => i.DictValue == DictValue
                && i.DictId != DictId
                && i.DictTypeId == DictTypeId
                );

            UIModelData<UIMath_Dict> model1 = new UIModelData<UIMath_Dict>()
            {
                remark = model.remark,
                status = model.status,
                suc = model.suc,
                Data = Mapper.Map<UIMath_Dict>(model.Data)
            };

            return new JsonResult() { Data = model1, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Authorize]
        public ActionResult DeleteMathDict(Guid DictId)
        {
            UIModelData<string> model = null;
            try
            {
                BLL_Math_Dict bll = new BLL_Math_Dict();
                bll.Delete(DictId);

                model = new UIModelData<string>()
                {
                    Data = string.Empty,
                    remark = OpCommonString.DeleteSuccess,
                    status = 1,
                    suc = true
                };
            }
            catch (Exception ex)
            {
                model = new UIModelData<string>()
                {
                    Data = string.Empty,
                    remark = ex.Message,
                    status = 0,
                    suc = false
                };
            }
            finally
            {
            }

            return new JsonResult() { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Authorize]
        public ActionResult DeleteMathDictBatch(string DictIds)
        {
            List<Guid> ids = JsonHelper.DeserializeObject<List<Guid>>(DictIds);
            foreach (Guid item in ids)
            {
                BLL_Math_Dict bll = new BLL_Math_Dict();
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