using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOModel
{
    public class UIMath_Work_History
    {
        public string ShenPiJianRemark { get; set; }
        public string ShenPiJianYi { get; set; }
        public string ShenPiRen { get; set; }
        public string ShenPiRenTel { get; set; }
        public Guid WorkHisId { get; set; }
        public int WorkHisSeq { get; set; }
        public Guid WorkId { get; set; }
        public DateTime ShenPiDateTime { get; set; }
    }
}
