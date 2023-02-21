using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BLL_Dcit
    {
        public Dictionary<int, string> Dict = new Dictionary<int, string>();

        public BLL_Dcit()
        {
            Dict.Add(1, "新建");
            Dict.Add(2, "同意出差");
            Dict.Add(-2, "提交部门审批");
            Dict.Add(-3, "提交招办审批");
            Dict.Add(3, "审批成功");
            Dict.Add(-1, "不同意出差");
            Dict.Add(-4, "不同意报销");
        }

    }
}
