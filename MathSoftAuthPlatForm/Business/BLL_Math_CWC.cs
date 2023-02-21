using DataAccess;
using MathSoftModelLib;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Business
{
    public class BLL_Math_CWC: BaseMathRoleAuthorEntities
    {
        
        private DbSet<Math_CWC> contextItem = null;
        public BLL_Math_CWC():base()
        {
            contextItem = context.Math_CWC;
        }
        public void clearTable()
        {
            context.Database.ExecuteSqlCommand("truncate table [dbo].[Math_CWC]", new object[] { });
        }
        public UIModelData<Math_CWC> Add(string StudentName, string IdCard, DateTime InsertTime, DateTime LastUpdateTime, string DataFrom, int FeeItems, Expression<Func<Math_CWC, bool>> perWhere)
        {
            Math_CWC model = new Math_CWC()
            {
                CWCId = Guid.NewGuid(),
                IdCard = IdCard,
                InsertTime = DateTime.Now,
                LastUpdateTime = DateTime.Now,
                StudentName = StudentName,
                DataFrom = DataFrom,
                FeeItems = FeeItems

            };
            UIModelData<Math_CWC> uIModelData = new UIModelData<Math_CWC> { };
            int total = GetTotal(perWhere);
            if (total > 0)
            {
                uIModelData.status = 7;
                uIModelData.suc = false;
                uIModelData.remark = string.Format("身份证号重复");
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
        public List<Math_CWC> Search(int PageIndex, int PageSize, out int total, Expression<Func<Math_CWC, bool>> where = null)
        {
            Expression<Func<Math_CWC, bool>> exp1 = where == null ? item => true : where;
            total = contextItem.Count(exp1);
            IQueryable<Math_CWC> lazyList = contextItem.Where(exp1).OrderByDescending(item => item.InsertTime);
            return PageIndex >= 0 ? lazyList.Skip(PageIndex).Take(PageSize).ToList() : lazyList.ToList();
        }
        public int GetTotal(Expression<Func<Math_CWC, bool>> where = null)
        {
            Expression<Func<Math_CWC, bool>> exp1 = where == null ? item => true : where;
            var total = contextItem.Count(exp1);
            return total;
        }
        public Math_CWC GetSingle(Guid id)
        {
            return contextItem.FirstOrDefault(item => item.CWCId == id);
        }
        public void update(string  idcard)
        {
            Math_CWC model= contextItem.FirstOrDefault(item => item.IdCard == idcard&&item.FeeItems==2);
            if (model != null)
            {
                model.FeeItems = -1;
                context.SaveChanges();
            }
        }
    }
}
