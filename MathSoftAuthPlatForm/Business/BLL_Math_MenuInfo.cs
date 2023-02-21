using DataAccess;
using MathSoftModelLib;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Business
{
    public class BLL_Math_MenuInfo: BaseMathRoleAuthorEntities
    {
        private DbSet<Math_MenuInfo> contextItem = null;
        public BLL_Math_MenuInfo():base()
        {
            contextItem = context.Math_MenuInfo;
        }

        public UIModelData<Math_MenuInfo> Add(string MenuTitle, string MenuURL, string MenuRemark, string MenuIcon,int MenuOrderBy, Expression<Func<Math_MenuInfo, bool>> perWhere)
        {
            Math_MenuInfo model = new Math_MenuInfo()
            {
                MenuId = Guid.NewGuid(),
                MenuInsertTime = DateTime.Now,
                MenuUpdateTime = DateTime.Now,
                MenuRemark = MenuRemark,
                MenuIcon = MenuIcon,
                MenuTitle = MenuTitle,
                MenuURL = MenuURL,
                MenuOrderBy = MenuOrderBy
            };
            UIModelData<Math_MenuInfo> uIModelData = new UIModelData<Math_MenuInfo> { };
            int total = GetTotal(perWhere);
            if (total > 0)
            {
                uIModelData.status = 7;
                uIModelData.suc = false;
                uIModelData.remark = string.Format("菜单值重复");
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

        public UIModelData<Math_MenuInfo> Update(Guid Menuid, string MenuTitle, string MenuURL, string MenuRemark, string MenuIcon,int  MenuOrderBy, Expression<Func<Math_MenuInfo, bool>> perWhere)
        {
            UIModelData<Math_MenuInfo> uIModelData = new UIModelData<Math_MenuInfo> { };
            int total = GetTotal(perWhere);
            if (total > 0)
            {
                uIModelData.status = 7;
                uIModelData.suc = false;
                uIModelData.remark = string.Format("菜单名称重复");
            }
            else
            {
                MathRoleAuthorEntities context = new MathRoleAuthorEntities();
                DbSet<Math_MenuInfo> contextItem = context.Math_MenuInfo;
                Math_MenuInfo model = contextItem.FirstOrDefault(item => item.MenuId == Menuid);
                model.MenuUpdateTime = DateTime.Now;
                model.MenuRemark = MenuRemark;
                model.MenuIcon = MenuIcon;
                model.MenuTitle = MenuTitle;
                model.MenuURL = MenuURL;
                model.MenuOrderBy = MenuOrderBy;
                context.SaveChanges();
                uIModelData.suc = true;
                uIModelData.Data = model;
            }
            return uIModelData;
        }

        public void Delete(Guid id)
        {
            var model = contextItem.FirstOrDefault(item => item.MenuId == id);
            if (model != null)
            {
                contextItem.Remove(model);
                context.SaveChanges();
            }
        }

        public List<Math_MenuInfo> Search(int PageIndex, int PageSize, out int total, Expression<Func<Math_MenuInfo, bool>> where = null)
        {
            Expression<Func<Math_MenuInfo, bool>> exp1 = where == null ? item => true : where;
            total = contextItem.Count(exp1);
            IQueryable<Math_MenuInfo> lazyList = contextItem.Where(exp1).OrderByDescending(item => item.MenuOrderBy);
            return (PageIndex >= 0) ? lazyList.Skip(PageIndex).Take(PageSize).ToList() : lazyList.ToList();
        }

        public int GetTotal(Expression<Func<Math_MenuInfo, bool>> where = null)
        {
            Expression<Func<Math_MenuInfo, bool>> exp1 = where == null ? item => true : where;
            return contextItem.Count(exp1);
        }

        public Math_MenuInfo GetSingle(Guid id)
        {
            return contextItem.FirstOrDefault(item => item.MenuId == id);
        }
    }
}
