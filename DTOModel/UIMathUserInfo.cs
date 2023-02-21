using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace DTOModel
{
    public class UIMathUserInfo
    {
        public Guid UserId { get; set; }
        public int UserOrderId { get; set; }
        public String UserName { get; set; }
        public String UserAccount { get; set; }
        public String UserAddress { get; set; }
        public String UserPhone { get; set; }
        public String UserTel { get; set; }
        public String UserPwd { get; set; }
        public int UserStatus { get; set; }
        public DateTime UserInsertTime { get; set; }
        public DateTime UserUpdateTime { get; set; }
        public Guid User_Dept_Id { get; set; }
        public string User_Sex { get; set; }
        public List<UIRoleModel> ListRole { get; set; }

        private string roleNames;
        public string RoleNames
        {
            get
            {
                if (ListRole == null || ListRole.Count == 0)
                {
                    return string.Empty;
                }
                return string.Join("<br>", ListRole.Select(item => item.RoleName).ToList());
            }
            set
            {
                roleNames = value;
            }
        }

        public int User_Area { get; set; }

        public string Math_Dept_Name { get; set; }
    }
}