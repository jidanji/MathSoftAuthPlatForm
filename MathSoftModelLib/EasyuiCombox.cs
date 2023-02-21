using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathSoftModelLib
{
    /// <summary>
    /// Easyui的combox返回的数据结构
    /// http://www.jeasyui.com/index.php
    /// 马良
    /// 2017年4月26日
    /// </summary>
    public class EasyuiCombox
    {
        public int id { get; set; }
        public string text { get; set; }
        public bool Selected { get => _selected; set => _selected = value; }

        private bool _selected = false;

    }
}
