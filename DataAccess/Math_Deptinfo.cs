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
    
    public partial class Math_Deptinfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Math_Deptinfo()
        {
            this.Math_UserInfo = new HashSet<Math_UserInfo>();
        }
    
        public System.Guid Math_Dept_Id { get; set; }
        public int Math_SeqNo { get; set; }
        public string Math_Dept_Name { get; set; }
        public string Math_Dept_Remark { get; set; }
        public Nullable<System.DateTime> Math_Dept_InsertTime { get; set; }
        public Nullable<System.DateTime> Math_Dept_UpdateTime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Math_UserInfo> Math_UserInfo { get; set; }
    }
}
