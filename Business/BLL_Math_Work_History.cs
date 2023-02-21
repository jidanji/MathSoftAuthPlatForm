using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using DataAccess;
using MathSoftModelLib;

namespace Business
{
    public class BLL_Math_Work_History
    {
        private MathRoleAuthorEntities context = null;
        private DbSet<Math_Work_History> contextItem = null;

        public BLL_Math_Work_History()
        {
            context = new MathRoleAuthorEntities();
            contextItem = context.Math_Work_History;
        }

        public UIModelData<Math_Work_History> Add(
        Guid WorkId
      , string ShenPiRen
      , string ShenPiRenTel
      , string ShenPiJianYi
      , string ShenPiJianRemark, Expression<Func<Math_Work_History, bool>> perWhere)
        {
            Math_Work_History model = new Math_Work_History()
            {
                ShenPiJianRemark = ShenPiJianRemark,
                ShenPiJianYi = ShenPiJianYi,
                ShenPiRen = ShenPiRen,
                ShenPiRenTel = ShenPiRenTel,
                WorkId = WorkId,
                WorkHisId = Guid.NewGuid(),
                ShenPiDateTime = DateTime.Now
            };
            UIModelData<Math_Work_History> uIModelData = new UIModelData<Math_Work_History> { };
            int total = GetTotal(perWhere);
            if (total > 0)
            {
                uIModelData.status = 7;
                uIModelData.suc = false;
                uIModelData.remark = string.Format("角色名称重复");
            }
            else
            {
                contextItem.Add(model);
                context.SaveChanges();
                uIModelData.status = 6;
                uIModelData.suc = true;
                uIModelData.Data = model;
            }
            return uIModelData;
        }
        public List<Math_Work_History> Search(int PageIndex, int PageSize, out int total, Expression<Func<Math_Work_History, bool>> where = null)
        {
            Expression<Func<Math_Work_History, bool>> exp1 = where == null ? item => true : where;
            total = contextItem.Count(exp1);
            if (PageIndex >= 0)
            {
                return contextItem.Where(exp1).OrderBy(i => i.WorkHisSeq).Skip(PageIndex).Take(PageSize).ToList();
            }
            else
            {
                return contextItem.Where(exp1).OrderBy(i => i.WorkHisSeq).ToList();
            }
        }
        public int GetTotal(Expression<Func<Math_Work_History, bool>> where = null)
        {
            Expression<Func<Math_Work_History, bool>> exp1 = where == null ? item => true : where;
            var total = contextItem.Count(exp1);
            return total;
        }
    }
}
