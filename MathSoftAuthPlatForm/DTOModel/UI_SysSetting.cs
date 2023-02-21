using System;

namespace DTOModel
{
    public class UI_SysSetting
    {
        #region 系统设置ID
        /// <summary>
        /// 系统设置ID
        /// </summary>
        public Guid SysSettingId { get; set; }
        #endregion

        #region 系统状态
        /// <summary>
        /// 系统状态
        /// </summary>
        public int? SysBaoMingStatus { get; set; }
        #endregion


        /// <summary>
        /// 单招1
        /// </summary>
        public bool danzhaoStatus { get; set; }

        /// <summary>
        /// 统招2
        /// </summary>
        public bool tongzhaoStatus { get; set; }

     

        /// <summary>
        /// 单招4
        /// </summary>
        public bool zhongzhuanStatus { get; set; }

        /// <summary>
        /// 五年一贯制5
        /// </summary>
        public bool wunianStatus { get; set; }
    }
}
