using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathSoftModelLib
{
    /// <summary>
    /// 用于datatable插件的数据返回类型
    /// https://datatables.net/
    /// 马良
    /// 日期
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class datatablesModel<T>
    {
        public int? draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<T> data { get; set; }

        public string remark { get; set; }
    }
}
