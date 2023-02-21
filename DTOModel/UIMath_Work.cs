using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOModel
{
    public class UIMath_Work
    {
        public Guid WorkId { get; set; }
        public string WorkName { get; set; }
        public string JiaotongFangshi { get; set; }
        public string Target { get; set; }
        public string ShoolName { get; set; }
        public string PointWhoNumber { get; set; }
        public string PointWho { get; set; }
        public int Fee { get; set; }
        public DateTime BegTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Remark { get; set; }
        public int Status { get; set; }


        public Guid CreateDeptId { get; set; }
        public string UserAccount { get; set; }
        public string UserName { get; set; }
        public string UserTelNumber { get; set; }
        public string UserDeptName { get; set; }
        public int? InfactFee { get; set; }

        public DateTime CreateTime { get; set; }
        public DateTime ShenPiRiQi { get; set; }

        public List<UIMath_Work_History> HisList = new List<UIMath_Work_History>();

        public List<UI_AttachedMent> AttachedMentList = new List<UI_AttachedMent>();
    }
}
