using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using DataAccess;
using MathSoftModelLib;

namespace Business
{
    public class BLL_Math_SYJD
    {
        private MathRoleAuthorEntities context = null;
        private DbSet<Math_SYJD> contextItem = null;
        public BLL_Math_SYJD()
        {
            context = new MathRoleAuthorEntities();
            contextItem = context.Math_SYJD;
        }

        public void clearTable()
        {
            context.Database.ExecuteSqlCommand("truncate table [dbo].[Math_SYJD]", new object[] { });
        }

        public UIModelData<Math_SYJD> Add(string GrandSchool
      , string Who
      , DateTime InsertTime
      , DateTime LastUpdateTime
      , string DataFrom)
        {
            Math_SYJD model = new Math_SYJD()
            {
                SYJDId = Guid.NewGuid(),
                GrandSchool = GrandSchool,
                Who = Who,
                InsertTime = DateTime.Now,
                LastUpdateTime = DateTime.Now,
                DataFrom = DataFrom
            };
            UIModelData<Math_SYJD> uIModelData = new UIModelData<Math_SYJD> { };

            contextItem.Add(model);
            context.SaveChanges();
            uIModelData.status = 6;
            uIModelData.suc = true;
            uIModelData.Data = model;

            return uIModelData;
        }


        public List<Math_SYJD> Search(int PageIndex, int PageSize, out int total, Expression<Func<Math_SYJD, bool>> where = null)
        {
            Expression<Func<Math_SYJD, bool>> exp1 = where == null ? item => true : where;

            total = contextItem.Count(exp1);
            if (PageIndex >= 0)
            {
                return contextItem.Where(exp1).OrderByDescending(item => item.InsertTime).Skip(PageIndex).Take(PageSize).ToList();
            }
            else
            {
                return contextItem.Where(exp1).OrderByDescending(item => item.InsertTime).ToList();
            }
        }

        public int GetTotal(Expression<Func<Math_SYJD, bool>> where = null)
        {
            Expression<Func<Math_SYJD, bool>> exp1 = where == null ? item => true : where;

            var total = contextItem.Count(exp1);

            return total;
        }

        public Math_SYJD GetSingle(Guid id)
        {
            return contextItem.FirstOrDefault(item => item.SYJDId == id);
        }
    }
}

