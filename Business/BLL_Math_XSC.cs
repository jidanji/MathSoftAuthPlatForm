using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using DataAccess;
using MathSoftModelLib;

namespace Business
{
    public class BLL_Math_XSC
    {
        private MathRoleAuthorEntities context = null;
        private DbSet<Math_XSC> contextItem = null;
        public BLL_Math_XSC()
        {
            context = new MathRoleAuthorEntities();
            contextItem = context.Math_XSC;
        }

        public void clearTable()
        {
            context.Database.ExecuteSqlCommand("truncate table [dbo].[Math_XSC]", new object[] { });
        }

        public UIModelData<Math_XSC> Add(string StudentName
      , string IdCard
      , DateTime InsertTime
      , DateTime LastUpdateTime
      , string DataFrom,
        string StudentNumber,
            Expression<Func<Math_XSC, bool>> perWhere)
        {
            UIModelData<Math_XSC> uIModelData = new UIModelData<Math_XSC> { };
            if (string.IsNullOrEmpty(IdCard.Trim()))
            {
                uIModelData.status = 7;
                uIModelData.suc = false;
                uIModelData.remark = string.Format("身份证号为空");
                return uIModelData;
            }
            Math_XSC model = new Math_XSC()
            {
                XSCId = Guid.NewGuid(),
                IdCard = IdCard,
                InsertTime = DateTime.Now,
                LastUpdateTime = DateTime.Now,
                StudentName = StudentName,
                DataFrom = DataFrom,
                StudentNumber = StudentNumber
            };
         
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


        public List<Math_XSC> Search(int PageIndex, int PageSize, out int total, Expression<Func<Math_XSC, bool>> where = null)
        {
            Expression<Func<Math_XSC, bool>> exp1 = where == null ? item => true : where;

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

        public int GetTotal(Expression<Func<Math_XSC, bool>> where = null)
        {
            Expression<Func<Math_XSC, bool>> exp1 = where == null ? item => true : where;
            var total = contextItem.Count(exp1);
            return total;
        }

        public Math_XSC GetSingle(Guid id)
        {
            return contextItem.FirstOrDefault(item => item.XSCId == id);
        }
    }
}
