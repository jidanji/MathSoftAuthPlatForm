using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOModel
{
    public class UIStudent
    {
        public string StudentAddress { get; set; }
        public string StudentName { get; set; }
        public Guid StudentId { get; set; }
        public int StudentOrderId { get; set; }
        public string StudentIDCard { get; set; }
        public string StudentPhone { get; set; }
        public string StudentType { get; set; }
        public string StudentSchool { get; set; }
        public Guid UserId { get; set; }
        public string UserPhone { get; set; }
        public string UserName { get; set; }
        public Guid UserDeptId { get; set; }
        public string UserDeptName { get; set; }
        public Guid StudentZhuanYeId { get; set; }
        public string StudentZhuanYeValue { get; set; }
        public DateTime InsertTime { get; set; }
        public DateTime UpdateTime { get; set; }

        public string Remark { get; set; }

        public string Area { get; set; }
        public string Sex { get; set; }

        public string DingZhengName { get; set; }
        public string DingZhengNumber { get; set; }
        public DateTime? DingZhengUpdateTime { get; set; }
    }
}
