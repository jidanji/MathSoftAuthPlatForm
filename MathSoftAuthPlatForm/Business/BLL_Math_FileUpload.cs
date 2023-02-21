using DataAccess;
using MathSoftModelLib;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Business
{
    public class BLL_Math_FileUpload: BaseMathRoleAuthorEntities
    {
        private DbSet<Math_FileUpload> contextItem = null;
        public BLL_Math_FileUpload():base()
        {
            contextItem = context.Math_FileUpload;
        }

        public UIModelData<Math_FileUpload> Add(Math_FileUpload model)
        {
            UIModelData<Math_FileUpload> uIModelData = new UIModelData<Math_FileUpload> { };
            contextItem.Add(model);
            context.SaveChanges();
            uIModelData.status = 6;
            uIModelData.suc = true;
            uIModelData.Data = model;
            return uIModelData;
        }

        public List<Math_FileUpload> Search(int PageIndex, int PageSize, out int total, Expression<Func<Math_FileUpload, bool>> where = null)
        {
            Expression<Func<Math_FileUpload, bool>> exp1 = where == null ? item => true : where;
            total = contextItem.Count(exp1);
            IQueryable<Math_FileUpload> lazyList = contextItem.Where(exp1).OrderByDescending(i => i.UploadTime);
            return PageIndex > 0 ? lazyList.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList() : lazyList.ToList();
        }

        public int GetTotal(Expression<Func<Math_FileUpload, bool>> where = null)
        {
            Expression<Func<Math_FileUpload, bool>> exp1 = where == null ? item => true : where;
            var total = contextItem.Count(exp1);
            return total;
        }

        public Math_FileUpload GetSingle(Guid id)
        {
            return contextItem.FirstOrDefault(item => item.UploadId == id);
        }
    }
}
