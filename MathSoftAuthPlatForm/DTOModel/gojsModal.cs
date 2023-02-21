using System.Collections.Generic;

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
