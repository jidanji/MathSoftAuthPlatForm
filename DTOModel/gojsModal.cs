using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOModel
{
    public class gojsModal
    {
        public string key { get; set; }
        public string text { get; set; }
        public bool isGroup { get; set; }
        public string category { get; set; }

        public string group { get; set; }
    }

    public class gojsDiaModal
    {
       public List<gojsModal> nodeDataArray { get; set; }
    }
}
