﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOModel
{
    public class UIZHaoShengBaobiao
    {
        public Guid UserDeptId { get; set; }
        public string UserDeptName { get; set; }
        public int TotalNumber { get; set; }
        public string InsertData { get; set; }
        public string UserName { get; set; }
        public Guid UserId { get; set; }

        public Guid ZhuanYeId { get; set; }
        public string ZhuanYeName { get; set; }

        public string Sex { get; set; }
        public string Area { get; set; }

        public string StudentType { get; set; }


    }
}
