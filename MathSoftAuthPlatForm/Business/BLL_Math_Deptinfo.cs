using DataAccess;
using MathSoftModelLib;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Business
{
    public class BLL_Math_Deptinfo: BaseMathRoleAuthorEntities
    {
        private DbSet<Math_Deptinfo> contextItem = null;
        public BLL_Math_Deptinfo():base()
        {
            contextItem = context.Math_Deptinfo;
        }
        public UIModelData<Math_Deptinfo> Add(string Math_Dept_Name, string Math_Dept_Remark, Expression<Func<Math_Deptinfo, bool>> perWhere)
        {
            Math_Deptinfo model = new Math_Deptinfo()
            {
                Math_Dept_Id = Guid.NewGuid(),
                Math_Dept_Name = Math_Dept_Name,
                Math_Dept_Remark = Math_Dept_Remark,
                Math_Dept_InsertTime = DateTime.Now,
                Math_Dept_UpdateTime = DateTime.Now
            };
            UIModelData<Math_Deptinfo> uIModelData = new UIModelData<Math_Deptinfo> { };
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
        public UIModelData<Math_Deptinfo> Update(Guid Math_Dept_Id,string Math_Dept_Name, string Math_Dept_Remark, Expression<Func<Math_Deptinfo, bool>> perWhere)
        {
            UIModelData<Math_Deptinfo> uIModelData = new UIModelData<Math_Deptinfo> { };
            int total = GetTotal(perWhere);
            if (total > 0)
            {
                uIModelData.status = 7;
                uIModelData.suc = false;
                uIModelData.remark = string.Format("部门名称重复");
            }
            else
            {
                MathRoleAuthorEntities context = new MathRoleAuthorEntities();
                DbSet<Math_Deptinfo> contextItem = context.Math_Deptinfo;

                Math_Deptinfo model = contextItem.FirstOrDefault(item => item.Math_Dept_Id == Math_Dept_Id);
                model.Math_Dept_Name = Math_Dept_Name;
                model.Math_Dept_Remark = Math_Dept_Remark;
                model.Math_Dept_UpdateTime = DateTime.Now;

                context.SaveChanges();
                uIModelData.suc = true;
                uIModelData.Data = model;
            }
            return uIModelData;
        }

        public void Delete(Guid id)
        {
            var model = contextItem.FirstOrDefault(item => item.Math_Dept_Id == id);
            if (model != null)
            {
                contextItem.Remove(model);
                context.SaveChanges();
            }
        }

        public List<Math_Deptinfo> Search(int PageIndex, int PageSize, out int total, Expression<Func<Math_Deptinfo, bool>> where = null)
        {
            Expression<Func<Math_Deptinfo, bool>> exp1 = where == null ? item => true : where;
            total = contextItem.Count(exp1);
            IQueryable<Math_Deptinfo> lazyList = contextItem.Where(exp1).OrderByDescending(item => item.Math_SeqNo);
            return PageIndex >= 0 ? lazyList.Skip(PageIndex).Take(PageSize).ToList() : lazyList.ToList();
        }

        public int GetTotal(Expression<Func<Math_Deptinfo, bool>> where = null)
        {
            Expression<Func<Math_Deptinfo, bool>> exp1 = where == null ? item => true : where;
            var total = contextItem.Count(exp1);
            return total;
        }

        public Math_Deptinfo GetSingle(Guid id)
        {
            return contextItem.FirstOrDefault(item => item.Math_Dept_Id == id);
        }
    }
}
