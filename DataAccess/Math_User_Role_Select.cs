//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Math_User_Role_Select
    {
        public System.Guid User_Role_Select_Id { get; set; }
        public Nullable<System.Guid> UserId { get; set; }
        public Nullable<System.Guid> RoleId { get; set; }
    
        public virtual Math_RoleInfo Math_RoleInfo { get; set; }
        public virtual Math_UserInfo Math_UserInfo { get; set; }
    }
}