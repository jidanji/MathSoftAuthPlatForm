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
    public class BLL_AttachedMent
    {
        private MathRoleAuthorEntities context = null;
        private DbSet<AttachedMent> contextItem = null;
        public BLL_AttachedMent()
        {
            context = new MathRoleAuthorEntities();
            contextItem = context.AttachedMents;
        }

        public UIModelData<AttachedMent> Add(Guid WorkId, string AttachedUrl, string AttchedType, string AttchedRemark
       , Expression<Func<AttachedMent, bool>> perWhere)
        {
            AttachedMent model = new AttachedMent()
            {
                AttachedId = Guid.NewGuid(),
                AttachedUrl = AttachedUrl,
                AttchedRemark = AttchedRemark,
                AttchedInserTime = DateTime.Now,
                AttchedType = AttchedType,
                WorkId = WorkId
            };
            UIModelData<AttachedMent> uIModelData = new UIModelData<AttachedMent> { };
            int total = GetTotal(perWhere);
            if (total > 0)
            {
                uIModelData.status = 7;
                uIModelData.suc = false;
                uIModelData.remark = string.Format("字典值重复");
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
        public void Delete(Guid id)
        {
            var model = contextItem.FirstOrDefault(item => item.AttachedId == id);
            if (model != null)
            {
                contextItem.Remove(model);
                context.SaveChanges();
            }
        }

        public List<AttachedMent> Search(int PageIndex, int PageSize, out int total, Expression<Func<AttachedMent, bool>> where = null)
        {
            Expression<Func<AttachedMent, bool>> exp1 = where == null ? item => true : where;

            total = contextItem.Count(exp1);
            if (PageIndex >= 0)
            {
                return contextItem.Where(exp1).OrderByDescending(item => item.AttchSeq).Skip(PageIndex).Take(PageSize).ToList();
            }
            else
            {
                return contextItem.Where(exp1).OrderByDescending(item => item.AttchSeq).ToList();
            }
        }

        public int GetTotal(Expression<Func<AttachedMent, bool>> where = null)
        {
            Expression<Func<AttachedMent, bool>> exp1 = where == null ? item => true : where;
            var total = contextItem.Count(exp1);
            return total;
        }

        public AttachedMent GetSingle(Guid id)
        {
            return contextItem.FirstOrDefault(item => item.AttachedId == id);
        }
    }
}
