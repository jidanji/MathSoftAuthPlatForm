using DataAccess;
using MathSoftModelLib;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BLL_Math_Work_History: BaseMathRoleAuthorEntities
    {
        private DbSet<Math_Work_History> contextItem = null;
        public BLL_Math_Work_History():base()
        {
            context = new MathRoleAuthorEntities();
            contextItem = context.Math_Work_History;
        }

        public UIModelData<Math_Work_History> Add(Guid WorkId, string ShenPiRen, string ShenPiRenTel, string ShenPiJianYi
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
            IQueryable<Math_Work_History> lazyList = contextItem.Where(exp1).OrderBy(i => i.WorkHisSeq);
            return (PageIndex >= 0) ? lazyList.Skip(PageIndex).Take(PageSize).ToList() : lazyList.ToList();
        }
        public int GetTotal(Expression<Func<Math_Work_History, bool>> where = null)
        {
            Expression<Func<Math_Work_History, bool>> exp1 = where == null ? item => true : where;
            var total = contextItem.Count(exp1);
            return total;
        }
    }
}
