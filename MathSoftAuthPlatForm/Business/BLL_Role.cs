using DataAccess;
using MathSoftCommonLib;
using MathSoftModelLib;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Business
{
    /// <summary>
    /// 角色业务类
    /// </summary>
    public class BLL_Role: BaseMathRoleAuthorEntities
    {
        private DbSet<Math_RoleInfo> contextItem = null;

        public BLL_Role():base()
        {
            contextItem = context.Math_RoleInfo;
        }

        public UIModelData<Math_RoleInfo> Add(Math_RoleInfo model, Expression<Func<Math_RoleInfo, bool>> perWhere)
        {
            UIModelData<Math_RoleInfo> uIModelData = new UIModelData<Math_RoleInfo> { };
            int total = GetTotal(perWhere);
            if (total > 0)
            {
                uIModelData.status = 7;
                uIModelData.suc = false;
                uIModelData.remark = string.Format("角色名称重复");
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


        public UIModelData<Math_RoleInfo> Update(Guid RoleId, string roleName, string roleRemark, Expression<Func<Math_RoleInfo, bool>> perWhere)
        {
            UIModelData<Math_RoleInfo> uIModelData = new UIModelData<Math_RoleInfo> { };
            int total = GetTotal(perWhere);
            if (total > 0)
            {
                uIModelData.status = 7;
                uIModelData.suc = false;
                uIModelData.remark = string.Format("角色名称重复");
            }
            else
            {
                MathRoleAuthorEntities context = new MathRoleAuthorEntities();
                DbSet<Math_RoleInfo> contextItem = context.Math_RoleInfo;
                Math_RoleInfo model = contextItem.FirstOrDefault(item => item.RoleId == RoleId);

                model.RoleName = roleName;
                model.RoleRemark = roleRemark;
                context.SaveChanges();
                uIModelData.suc = true;
                uIModelData.Data = model;
            }
            return uIModelData;
        }

        public void Delete(Guid id)
        {
            var model = contextItem.FirstOrDefault(item => item.RoleId == id);
            if (model != null)
            {
                contextItem.Remove(model);
                context.SaveChanges();
            }
        }

        public List<Math_RoleInfo> Search(int PageIndex, int PageSize, out int total, Expression<Func<Math_RoleInfo, bool>> where = null)
        {
            Expression<Func<Math_RoleInfo, bool>> exp1 = where == null ? item => true : where;

            total = contextItem.Count(exp1);
            IQueryable<Math_RoleInfo> lazyList = contextItem.Where(exp1).OrderByDescending(i => i.RoleOrderId);
            return (PageIndex >= 0) ? lazyList.Skip(PageIndex).Take(PageSize).ToList() : lazyList.ToList();
        }

        public int GetTotal(Expression<Func<Math_RoleInfo, bool>> where = null)
        {
            Expression<Func<Math_RoleInfo, bool>> exp1 = where == null ? item => true : where;
            return contextItem.Count(exp1);
        }

        public Math_RoleInfo GetSingle(Guid id)
        {
            return contextItem.FirstOrDefault(item => item.RoleId == id);
        }

        public void SendRole(Guid RoleId, string stringUserInfo)
        {
            MathRoleAuthorEntities entity = new MathRoleAuthorEntities();
            IQueryable<Math_User_Role_Select> query = entity.Math_User_Role_Select.Where(item => item.RoleId == RoleId);
            entity.Math_User_Role_Select.RemoveRange(query);

            List<Math_UserInfo> list = JsonHelper.DeserializeObject<List<Math_UserInfo>>(stringUserInfo);
            foreach (Math_UserInfo item in list)
            {
                entity.Math_User_Role_Select.Add(new Math_User_Role_Select()
                {
                    RoleId = RoleId,
                    UserId = item.UserId,
                    User_Role_Select_Id = Guid.NewGuid()
                });
            }
            entity.SaveChanges();
        }

        public void SendMenu(Guid RoleId, string stringMenuInfo)
        {
            MathRoleAuthorEntities entity = new MathRoleAuthorEntities();
            IQueryable<Math_Role_Menu_Selcet> query = entity.Math_Role_Menu_Selcet.Where(item => item.RoleId == RoleId);
            entity.Math_Role_Menu_Selcet.RemoveRange(query);

            List<Math_MenuInfo> list = JsonHelper.DeserializeObject<List<Math_MenuInfo>>(stringMenuInfo);
            list.ForEach(item => { entity.Math_Role_Menu_Selcet.Add(new Math_Role_Menu_Selcet() { RoleId = RoleId, MenuId = item.MenuId, Role_Menu_Selet_Id = Guid.NewGuid() }); });
            entity.SaveChanges();
        }
    }
}
