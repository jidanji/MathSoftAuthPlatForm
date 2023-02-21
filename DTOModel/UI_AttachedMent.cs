using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOModel
{
    public class UI_AttachedMent
    {
        public Guid AttachedId { get; set; }
        public Guid WorkId { get; set; }
        public string AttachedUrl { get; set; }
        public string AttchedType { get; set; }
        public DateTime AttchedInserTime { get; set; }
        public string AttchedRemark { get; set; }
        public int AttchSeq { get; set; }
    }
}
