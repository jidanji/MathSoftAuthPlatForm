using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using DataAccess;
using MathSoftModelLib;

namespace Business
{
    public class BLL_Math_JYT
    {
        private MathRoleAuthorEntities context = null;
        private DbSet<Math_JYT> contextItem = null;
        public BLL_Math_JYT()
        {
            context = new MathRoleAuthorEntities();
            contextItem = context.Math_JYT;
        }

        public void clearTable()
        {
            context.Database.ExecuteSqlCommand("truncate table [dbo].[Math_JYT]", new object[] { });
        }

        public UIModelData<Math_JYT> Add(string StudentName
      , string IdCard
      , string StudentSex
      , string GrandSchool
      , DateTime InsertTime
      , DateTime LastUpdateTime
      , string DataFrom
            ,string ZSLB,
            Expression<Func<Math_JYT, bool>> perWhere)
        {
            Math_JYT model = new Math_JYT()
            {
                JYTId = Guid.NewGuid(),
                GrandSchool = GrandSchool,
                IdCard = IdCard,
                InsertTime = DateTime.Now,
                LastUpdateTime = DateTime.Now,
                StudentName = StudentName,
                StudentSex = StudentSex,
                DataFrom = DataFrom,
                 ZSLB= ZSLB
            };
            UIModelData<Math_JYT> uIModelData = new UIModelData<Math_JYT> { };
            int total = GetTotal(perWhere);
            if (total > 0)
            {
                uIModelData.status = 7;
                uIModelData.suc = false;
                uIModelData.remark = string.Format("身份证号号码重复");
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


        public List<Math_JYT> Search(int PageIndex, int PageSize, out int total, Expression<Func<Math_JYT, bool>> where = null)
        {
            Expression<Func<Math_JYT, bool>> exp1 = where == null ? item => true : where;
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

        public int GetTotal(Expression<Func<Math_JYT, bool>> where = null)
        {
            Expression<Func<Math_JYT, bool>> exp1 = where == null ? item => true : where;
            return contextItem.Count(exp1);
        }

        public Math_JYT GetSingle(Guid id)
        {
            return contextItem.FirstOrDefault(item => item.JYTId == id);
        }
    }
}
