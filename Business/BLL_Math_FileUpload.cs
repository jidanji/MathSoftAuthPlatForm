using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using DataAccess;
using MathSoftModelLib;
namespace Business
{
    public class BLL_Math_FileUpload
    {
        private MathRoleAuthorEntities context = null;
        private DbSet<Math_FileUpload> contextItem = null;
        public BLL_Math_FileUpload()
        {
            context = new MathRoleAuthorEntities();
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
            Expression<Func<Math_FileUpload, bool>> exp1 = where == null? item => true: where;
            total = contextItem.Count(exp1);
            if (PageIndex > 0)
            {
                return contextItem.Where(exp1).OrderByDescending(i=>i.UploadTime).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
            }
            else
            {
                return contextItem.Where(exp1).OrderByDescending(i => i.UploadTime).ToList();
            }
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
