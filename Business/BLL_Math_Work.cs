using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using DataAccess;
using MathSoftCommonLib;
using MathSoftModelLib;

namespace Business
{
    public class BLL_Math_Work
    {
        private MathRoleAuthorEntities context = null;
        private DbSet<Math_Work> contextItem = null;

        public BLL_Math_Work()
        {
            context = new MathRoleAuthorEntities();
            contextItem = context.Math_Work;
        }

        public UIModelData<Math_Work> Add(Math_Work model, Expression<Func<Math_Work, bool>> perWhere)
        {
            UIModelData<Math_Work> uIModelData = new UIModelData<Math_Work> { };
            contextItem.Add(model);
            context.SaveChanges();
            uIModelData.status = 6;
            uIModelData.suc = true;
            uIModelData.Data = model;
            return uIModelData;
        }


        public UIModelData<Math_Work> Update(
Guid WorkId,
string WorkName,
string JiaotongFangshi,
string Target,
string ShoolName,
string PointWhoNumber,
string PointWho,
int Fee,
DateTime BegTime,
DateTime EndTime,
string Remark,
Expression<Func<Math_Work, bool>> perWhere)
        {
            UIModelData<Math_Work> uIModelData = new UIModelData<Math_Work> { };
            int total = GetTotal(perWhere);

            MathRoleAuthorEntities context = new MathRoleAuthorEntities();
            DbSet<Math_Work> contextItem = context.Math_Work;
            Math_Work model = contextItem.FirstOrDefault(item => item.WorkId == WorkId);
            model.WorkName = WorkName;
            model.JiaotongFangshi = JiaotongFangshi;
            model.Target = Target;
            model.ShoolName = ShoolName;
            model.PointWhoNumber = PointWhoNumber;
            model.PointWho = PointWho;
            model.Fee = Fee;
            model.BegTime = BegTime;
            model.EndTime = EndTime;
            model.Remark = Remark;
            context.SaveChanges();
            uIModelData.suc = true;
            uIModelData.Data = model;

            return uIModelData;
        }

        public void Delete(Guid id)
        {
            var model = contextItem.FirstOrDefault(item => item.WorkId == id);
            if (model != null)
            {
                contextItem.Remove(model);
                context.SaveChanges();
            }
        }

        public List<Math_Work> Search(int PageIndex, int PageSize, out int total, Expression<Func<Math_Work, bool>> where = null)
        {
            Expression<Func<Math_Work, bool>> exp1 = where == null ? item => true : where;

            total = contextItem.Count(exp1);
            if (PageIndex >= 0)
            {
                return contextItem.Where(exp1).OrderByDescending(i => i.WorkSeq).Skip(PageIndex).Take(PageSize).ToList();
            }
            else
            {
                return contextItem.Where(exp1).OrderByDescending(i => i.WorkSeq).ToList();
            }
        }

        public int GetTotal(Expression<Func<Math_Work, bool>> where = null)
        {
            Expression<Func<Math_Work, bool>> exp1 = where == null ? item => true : where;
            var total = contextItem.Count(exp1);
            return total;
        }

        public Math_Work GetSingle(Guid id)
        {
            return contextItem.FirstOrDefault(item => item.WorkId == id);
        }

        public void Send2SHengPi(Guid WorkId)
        {
            MathRoleAuthorEntities context = new MathRoleAuthorEntities();
            DbSet<Math_Work> contextItem = context.Math_Work;
            Math_Work model = contextItem.FirstOrDefault(item => item.WorkId == WorkId);
            model.Status = -2;
            model.ShenPiRiQi = DateTime.Now;
            context.SaveChanges();
        }

        public void ShenPi2SHengPi(Guid WorkId, int status)
        {
            MathRoleAuthorEntities context = new MathRoleAuthorEntities();
            DbSet<Math_Work> contextItem = context.Math_Work;
            Math_Work model = contextItem.FirstOrDefault(item => item.WorkId == WorkId);
            model.Status = status;
            model.ShenPiRiQi = DateTime.Now;
            context.SaveChanges();
        }

        public void Send3SHengPi(Guid WorkId, int infactFee)
        {
            MathRoleAuthorEntities context = new MathRoleAuthorEntities();
            DbSet<Math_Work> contextItem = context.Math_Work;
            Math_Work model = contextItem.FirstOrDefault(item => item.WorkId == WorkId);
            model.Status = -3;
            model.InfactFee = infactFee;
            model.ShenPiRiQi = DateTime.Now;
            context.SaveChanges();
        }
    }
}
