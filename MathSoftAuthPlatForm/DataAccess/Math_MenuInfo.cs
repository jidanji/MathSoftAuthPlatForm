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
    
    public partial class Math_MenuInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Math_MenuInfo()
        {
            this.Math_Role_Menu_Selcet = new HashSet<Math_Role_Menu_Selcet>();
        }
    
        public System.Guid MenuId { get; set; }
        public int MenuOrderId { get; set; }
        public string MenuTitle { get; set; }
        public string MenuURL { get; set; }
        public string MenuRemark { get; set; }
        public string MenuIcon { get; set; }
        public Nullable<System.DateTime> MenuInsertTime { get; set; }
        public Nullable<System.DateTime> MenuUpdateTime { get; set; }
        public Nullable<int> MenuOrderBy { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Math_Role_Menu_Selcet> Math_Role_Menu_Selcet { get; set; }
    }
}
