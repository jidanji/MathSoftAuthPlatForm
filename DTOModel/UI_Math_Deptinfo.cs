using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOModel
{
    public class UI_Math_Deptinfo
    {
        public Guid Math_Dept_Id { get; set; }
        public string Math_Dept_Name { get; set; }
        public string Math_Dept_Remark { get; set; }
        public DateTime Math_Dept_InsertTime { get; set; }
        public DateTime Math_Dept_UpdateTime { get; set; }
    }
}
