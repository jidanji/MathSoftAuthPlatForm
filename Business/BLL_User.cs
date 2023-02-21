using System;
using System.Collections.Generic;
using System.Linq;
using MathSoftCommonLib;
using System.Linq.Expressions;
using DataAccess;
using System.Data.Entity;
using MathSoftModelLib;
using DTOModel;

namespace Business
{
    public class BLL_User
    {
        private MathRoleAuthorEntities context = null;
        private DbSet<Math_UserInfo> contextItem = null;
        public bool ValidUser(String UserName, String UserPWD)
        {
            int total = contextItem.Count(item => item.UserAccount == UserName && item.UserPwd == UserPWD);
            return total > 0;
        }

        public BLL_User()
        {
            context = new MathRoleAuthorEntities();
            contextItem = context.Math_UserInfo;
        }


        /// <summary>
        /// 新建用户
        /// </summary>
        /// <param name="model"></param>
        public UIModelData<Math_UserInfo> Add(string UserName, string UserAccount, string UserPhone, string UserTel, string UserPwd, Guid User_Dept_Id, string User_Sex, int User_Area, string RoleIds, Expression<Func<Math_UserInfo, bool>> perWhere)
        {
            List<UIRoleModel> roleList = JsonHelper.DeserializeObject<List<UIRoleModel>>(RoleIds);

            Math_UserInfo model = new Math_UserInfo()
            {
                UserAccount = UserAccount,
                UserInsertTime = DateTime.Now,
                UserId = Guid.NewGuid(),
                UserTel = UserTel,
                UserName = UserName,
                User_Sex = User_Sex,
                User_Dept_Id = User_Dept_Id,
                UserPhone = UserPhone,
                UserPwd = UserPwd,
                UserUpdateTime = DateTime.Now,
                User_Area = User_Area
            };
            UIModelData<Math_UserInfo> uIModelData = new UIModelData<Math_UserInfo> { };
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

                foreach (UIRoleModel uiitem in roleList)
                {
                    context.Math_User_Role_Select.Add(new Math_User_Role_Select
                    {
                        User_Role_Select_Id = Guid.NewGuid(),
                        RoleId = uiitem.RoleId,
                        UserId = model.UserId
                    });
                }
                context.SaveChanges();

                uIModelData.status = 6;
                uIModelData.suc = true;
                uIModelData.Data = model;
            }
            return uIModelData;
        }

        public UIModelData<Math_UserInfo> Update(Guid UserId, string UserName, string UserAccount, string UserPhone, string UserTel, string UserPwd, Guid User_Dept_Id, string User_Sex, int User_Area ,string RoleIds, Expression<Func<Math_UserInfo, bool>> perWhere)
        {
            List<UIRoleModel> roleList = JsonHelper.DeserializeObject<List<UIRoleModel>>(RoleIds);

           
            IQueryable<Math_User_Role_Select> query = context.Math_User_Role_Select.Where(item => item.UserId == UserId);
            context.Math_User_Role_Select.RemoveRange(query);
            context.SaveChanges();

            UIModelData<Math_UserInfo> uIModelData = new UIModelData<Math_UserInfo> { };
            int total = GetTotal(perWhere);
            if (total > 0)
            {
                uIModelData.status = 7;
                uIModelData.suc = false;
                uIModelData.remark = string.Format("登录名称重复");
            }
            else
            {
                MathRoleAuthorEntities context = new MathRoleAuthorEntities();
                DbSet<Math_UserInfo> contextItem = context.Math_UserInfo;
                Math_UserInfo model = contextItem.FirstOrDefault(item => item.UserId == UserId);
                model.UserAccount = UserAccount;
                model.UserTel = UserTel;
                model.UserName = UserName;
                model.User_Sex = User_Sex;
                model.User_Dept_Id = User_Dept_Id;
                model.UserPhone = UserPhone;
                model.UserPwd = UserPwd;
                model.UserUpdateTime = DateTime.Now;
                model.User_Area=User_Area;
                context.SaveChanges();

                foreach (UIRoleModel uiitem in roleList)
                {
                    context.Math_User_Role_Select.Add(new Math_User_Role_Select
                    {
                        User_Role_Select_Id = Guid.NewGuid(),
                        RoleId = uiitem.RoleId,
                        UserId = model.UserId
                    });
                }
                context.SaveChanges();

                uIModelData.suc = true;
                uIModelData.Data = model;
            }
            return uIModelData;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        public void Delete(Guid id)
        {
            var model = contextItem.FirstOrDefault(item => item.UserId == id);
            if (model != null)
            {
                contextItem.Remove(model);
                context.SaveChanges();
            }
        }
        /// <summary>
        /// 分页或者不分页查询数据
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="total"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<Math_UserInfo> Search(int PageIndex, int PageSize, out int total, Expression<Func<Math_UserInfo, bool>> where = null)
        {
            Expression<Func<Math_UserInfo, bool>> exp1 = null;
            if (where == null)
            {
                exp1 = item => true;
            }
            else
            {
                exp1 = where;
            }
            total = contextItem.Count(exp1);
            if (PageIndex >= 0)
            {
                return
                    contextItem
                    .Include("Math_User_Role_Select")
                    .Include("Math_User_Role_Select.Math_UserInfo")
                    .Where(exp1)
                    .OrderByDescending(item => item.UserOrderId)
                    .Skip( PageIndex)
                    .Take(PageSize).ToList();
            }
            else
            {
                return contextItem.Where(exp1).OrderByDescending(item => item.UserOrderId).ToList();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public int GetTotal(Expression<Func<Math_UserInfo, bool>> where = null)
        {
            Expression<Func<Math_UserInfo, bool>> exp1 = where == null ? item => true : where;
            var total = contextItem.Count(exp1);
            return total;
        }
        public Math_UserInfo GetSingle(Guid id)
        {
            return contextItem.FirstOrDefault(item => item.UserId == id);
        }

        public Math_UserInfo GetSingleByUserAccount(string UserAccount)
        {
            return contextItem.FirstOrDefault(item => item.UserAccount == UserAccount);
        }


        public Math_UserInfo GetSingle(String UserAccount)
        {
            return contextItem.FirstOrDefault(item => item.UserAccount == UserAccount);
        }
   
    }
}
