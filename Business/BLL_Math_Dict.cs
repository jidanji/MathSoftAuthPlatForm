
using DataAccess;
using MathSoftModelLib;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Business
{
    public class BLL_Math_Dict
    {
        private MathRoleAuthorEntities context = null;
        private DbSet<Math_Dict> contextItem = null;
        public BLL_Math_Dict()
        {
            context = new MathRoleAuthorEntities();
            contextItem = context.Math_Dict;
        }

        public UIModelData<Math_Dict> Add(Math_Dict model, Expression<Func<Math_Dict, bool>> perWhere)
        {
            UIModelData<Math_Dict> uIModelData = new UIModelData<Math_Dict> { };
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

        public UIModelData<Math_Dict> Update(Guid DictId, string DictValue, Guid DictTypeId, string DictRemark, Expression<Func<Math_Dict, bool>> perWhere)
        {
            UIModelData<Math_Dict> uIModelData = new UIModelData<Math_Dict> { };
            int total = GetTotal(perWhere);
            if (total > 0)
            {
                uIModelData.status = 7;
                uIModelData.suc = false;
                uIModelData.remark = string.Format("字典值重复");
            }
            else
            {
                MathRoleAuthorEntities context = new MathRoleAuthorEntities();
                DbSet<Math_Dict> contextItem = context.Math_Dict;
                Math_Dict model = contextItem.FirstOrDefault(item => item.DictId == DictId);

                model.DictValue = DictValue;
                model.DictTypeId = DictTypeId;
                model.DictRemark = DictRemark;
                context.SaveChanges();
                uIModelData.suc = true;
                uIModelData.Data = model;
            }
            return uIModelData;
        }

        public void Delete(Guid id)
        {
            var model = contextItem.FirstOrDefault(item => item.DictId == id);
            if (model != null)
            {
                contextItem.Remove(model);
                context.SaveChanges();
            }
        }

        public List<Math_Dict> Search(int PageIndex, int PageSize, out int total, Expression<Func<Math_Dict, bool>> where = null)
        {
            Expression<Func<Math_Dict, bool>> exp1 = where == null ? item => true : where;
            total = contextItem.Count(exp1);
            if (PageIndex > 0)
            {
                return contextItem.Where(exp1).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
            }
            else
            {
                return contextItem.Where(exp1).ToList();
            }
        }

        public int GetTotal(Expression<Func<Math_Dict, bool>> where = null)
        {
            Expression<Func<Math_Dict, bool>> exp1 = where == null ? item => true : where;
            var total = contextItem.Count(exp1);
            return total;
        }

        public Math_Dict GetSingle(Guid id)
        {
            return contextItem.FirstOrDefault(item => item.DictId == id);
        }
    }
}
