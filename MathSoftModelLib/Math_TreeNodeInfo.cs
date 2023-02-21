using System.Collections.Generic;

namespace MathSoftModelLib
{
    public class Math_TreeNodeInfo : NameValueKeyPair
    {
        public string  Id { get; set; }
        public string id { get; set; }

        public string PId { get; set; }
        public string pid { get; set; }
        public List<Math_TreeNodeInfo> Children { get => _Children; set => _Children = value; }

        public List<Math_TreeNodeInfo> children { get => _Children; set => _Children = value; }

        private List<Math_TreeNodeInfo> _Children = new List<Math_TreeNodeInfo>();

    }
}
