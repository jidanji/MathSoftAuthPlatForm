using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTOModel
{
    public class UIRoleModel
    {
        public UIRoleModel()
        {
            UserList = new List<UIMathUserInfo>();
        }

        public Guid RoleId { get; set; }
        public int RoleOrderId { get; set; }
        public string RoleName { get; set; }
        public int RoleStatus { get; set; }
        public DateTime RoleInsertTime { get; set; }
        public DateTime RoleUpdateTime { get; set; }
        public string RoleRemark { get; set; }

        public String StringUserInfo { get; set; }
        public List<UIMathUserInfo> UserList = new List<UIMathUserInfo>();

        public List<UI_Math_Menuinfo> MenuList = new List<UI_Math_Menuinfo>();

    }
}