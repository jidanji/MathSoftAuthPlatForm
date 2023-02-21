using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOModel
{
    public class UIStatic
    {
        public String DateMonth { get; set; }
        public Guid CreateDeptId { get; set; }
        public String UserDeptName { get; set; }
        public int InfactFee { get; set; }

        public int PersonCount { get; set; }

        public string StaticType { get; set; }
    }
}
