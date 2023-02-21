using System;

namespace DTOModel
{
    public class Dto_View_AnalySISS
    {
        public string name_Check_Content { get; set; }
        public int name_Check { get; set; }
       public string zhanzhangdaqu { get; set; }
      public string school { get; set; }
        public string zhanzhang { get; set; }
        public Dto_View_AnalySISS()
        { }

        #region Model
        private Guid _jyt_id;
        private string _jyt_studentname;
        private string _jyt_idcard;
        private string _jyt_studentsex;
        private string _jyt_grandschool;
        private DateTime? _jyt_inserttime;
        private DateTime _jyt_lastupdatetime;
        private string _jyt_datafrom;
        private string _xsc_idcard;
        private string _zs_idcard;
        private string _syjd_grandschool;
        private string _syjd_who;
        private string _cwc_idcard;
        private string _zs_who;
        private string _ZSLB;


        public string tuijianren { get; set; }
        public int tuijianrentype { get; set; }

        public string XSC_StudentName { get; set; }

        public int  FeeItems{ get; set; }

         

       


        /// <summary>
        /// 
        /// </summary>
        public Guid JYT_Id
        {
            set { _jyt_id = value; }
            get { return _jyt_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JYT_StudentName
        {
            set { _jyt_studentname = value; }
            get { return _jyt_studentname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JYT_IdCard
        {
            set { _jyt_idcard = value; }
            get { return _jyt_idcard; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JYT_StudentSex
        {
            set { _jyt_studentsex = value; }
            get { return _jyt_studentsex; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JYT_GrandSchool
        {
            set { _jyt_grandschool = value; }
            get { return _jyt_grandschool; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? JYT_InsertTime
        {
            set { _jyt_inserttime = value; }
            get { return _jyt_inserttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime JYT_LastUpdateTime
        {
            set { _jyt_lastupdatetime = value; }
            get { return _jyt_lastupdatetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JYT_DataFrom
        {
            set { _jyt_datafrom = value; }
            get { return _jyt_datafrom; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string XSC_IdCard
        {
            set { _xsc_idcard = value; }
            get { return _xsc_idcard; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZS_IdCard
        {
            set { _zs_idcard = value; }
            get { return _zs_idcard; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SYJD_GrandSchool
        {
            set { _syjd_grandschool = value; }
            get { return _syjd_grandschool; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SYJD_Who
        {
            set { _syjd_who = value; }
            get { return _syjd_who; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CWC_IdCard
        {
            set { _cwc_idcard = value; }
            get { return _cwc_idcard; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZS_Who
        {
            set { _zs_who = value; }
            get { return _zs_who; }
        }

        #region 是否报道
        /// <summary>
        /// 是否报道
        /// </summary>
        public int Is_baodao
        {
            get
            {
                return string.IsNullOrEmpty(_xsc_idcard) ? 0 : 1;
            }
        }
        #endregion

        #region 是否缴费
        /// <summary>
        /// 是否缴费
        /// </summary>
        public int Is_jiaofei
        {
            get
            {
                return string.IsNullOrEmpty(_cwc_idcard) ? 0 : 1;
            }

        }
        #endregion

        #region 是否生源基地
        /// <summary>
        /// 是否生源基地
        /// </summary>
        public int Is_shengyuanjidi
        {
            get
            {
                return string.IsNullOrEmpty(_syjd_grandschool) ? 0 : 1;
            }

        }
        #endregion

        #region 推荐人
        /// <summary>
        /// 推荐人
        /// </summary>
        public string Resault_Who
        {
            get
            {
                //string who = string.IsNullOrEmpty(_syjd_grandschool) ? _zs_who : _syjd_who;
                //return string.IsNullOrWhiteSpace(who) ? "自然生源" : who;

                string who = string.IsNullOrEmpty(_syjd_grandschool) ? _zs_who : _syjd_grandschool;
                return string.IsNullOrWhiteSpace(who) ? "自然生源" : who;
            }
        }
        #endregion

        #region  是否有警告
        /// <summary>
        /// 是否有警告
        /// </summary>
        public int Is_Warning
        {
            get
            {
                return (this.Is_jiaofei == 1 && this.Is_baodao == 1) ? 0 : 1;
            }
        }

        public string ZSLB { get => _ZSLB; set => _ZSLB = value; }
        #endregion

        #endregion Model
    }
}
