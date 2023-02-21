using System.Collections.Generic;

namespace MathSoftModelLib
{
    public class UIRowsData<T>
    {
        public int total { get; set; }
        public List<T> rows { get; set; }
        public string remark { get; set; }
        public bool suc { get; set; }
        public int status { get; set; }

        /// <summary>
        /// 教育厅
        /// </summary>
        public int A1 { get; set; }
        /// <summary>
        /// 财务处
        /// </summary>
        public int A2 { get; set; }
        /// <summary>
        /// 学生处
        /// </summary>
        public int A3 { get; set; }
        /// <summary>
        /// 生源基地
        /// </summary>
        public int A4 { get; set; }

        /// <summary>
        /// 生源基地
        /// </summary>
        public int A5 { get; set; }
    }
}
