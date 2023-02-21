using System.ComponentModel;

namespace MathSoftModelLib
{
    public class FieldModel : INotifyPropertyChanged
    {
        private System.String _表名;

        public System.String 表名
        {
            get { return _表名; }
            set
            {
                _表名 = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("表名"));
                }
            }
        }


        private System.String _表说明;

        public System.String 表说明
        {
            get { return _表说明; }
            set
            {
                _表说明 = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("表说明"));
                }
            }
        }

        private System.Int16? _字段序号;

        public System.Int16? 字段序号
        {
            get { return _字段序号; }
            set
            {
                _字段序号 = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("字段序号"));
                }
            }
        }

        private System.String _字段名;

        public System.String 字段名
        {
            get { return _字段名; }
            set
            {
                _字段名 = value;

                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("字段名"));
                }
            }
        }

        private System.String _标识;

        public System.String 标识
        {
            get { return _标识; }
            set
            {
                _标识 = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("标识"));
                }
            }
        }
        private System.Int32? _主键;

        public System.Int32? 主键
        {
            get { return _主键; }
            set
            {
                _主键 = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("主键"));
                }
            }
        }
        private System.String _类型;

        public System.String 类型
        {
            get { return _类型; }
            set
            {
                _类型 = value;

                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("类型"));
                }
            }
        }
        private System.Int16? _占用字节数;

        public System.Int16? 占用字节数
        {
            get { return _占用字节数; }
            set
            {
                _占用字节数 = value;


                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("占用字节数"));
                }
            }
        }
        private System.Int32? _长度;

        public System.Int32? 长度
        {
            get { return _长度; }
            set
            {
                _长度 = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("长度"));
                }
            }
        }

        private System.Int32? _小数位数;

        public System.Int32? 小数位数
        {
            get { return _小数位数; }
            set
            {
                _小数位数 = value;


                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("小数位数"));
                }
            }
        }
        private System.String _允许空;

        public System.String 允许空
        {
            get { return _允许空; }
            set
            {
                _允许空 = value;

                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("允许空"));
                }
            }
        }
        private System.String _默认值;

        public System.String 默认值
        {
            get { return _默认值; }
            set
            {
                _默认值 = value;


                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("默认值"));
                }
            }
        }
        private System.String _字段说明;

        public System.String 字段说明
        {
            get { return _字段说明; }
            set
            {
                _字段说明 = value;

                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("字段说明"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
