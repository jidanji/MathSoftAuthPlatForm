using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathSoftModelLib
{
    public class UIModelData1<T>
    {
        public  List<T> data { get; set; }
        public string remark { get; set; }
        public bool suc { get; set; }
    }
}
