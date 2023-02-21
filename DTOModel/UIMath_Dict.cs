using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTOModel
{
    public class UIMath_Dict
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public Guid DictId { get; set; }

        /// <summary>
        /// 排序id
        /// </summary>
        public int DictOrderId { get; set; }

        /// <summary>
        /// 字典值
        /// </summary>
        public string DictValue { get; set; }

        /// <summary>
        /// 字典类型
        /// </summary>
        public Guid DictTypeId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string DictRemark { get; set; }
    }
}