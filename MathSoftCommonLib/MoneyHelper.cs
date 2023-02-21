using System;
using System.Globalization;

namespace MathSoftCommonLib
{
    #region 货币操作类
    /// <summary>
    /// 货币操作类
    /// </summary>
    public class MoneyHelper
    {
        #region 输入Float格式数字，将其转换为货币表达方式
        /// <summary> 
        /// 输入Float格式数字，将其转换为货币表达方式 
        /// </summary> 
        /// <param name="ftype">货币表达类型：0=人民币；1=港币；2=美钞；3=英镑；4=不带货币;其它=不带货币表达方式</param> 
        /// <param name="fmoney">传入的int数字</param> 
        /// <param name="point">小数位数</param>
        /// <returns>返回转换的货币表达形式</returns> 
        public static string ConvertCurrency(decimal fmoney, int ftype, int point = 0)
        {
            if (point < 0)
            {
                throw new Exception("小数位不能为负数");
            }
            CultureInfo cul = null;
            string _rmoney = string.Empty;

            switch (ftype)
            {
                case 0:
                    cul = new CultureInfo("zh-CN");//中国大陆 
                    _rmoney = fmoney.ToString("c", cul);
                    break;
                case 1:
                    cul = new CultureInfo("zh-HK");//香港 
                    _rmoney = fmoney.ToString("c", cul);
                    break;
                case 2:
                    cul = new CultureInfo("en-US");//美国 
                    _rmoney = fmoney.ToString("c", cul);
                    break;
                case 3:
                    cul = new CultureInfo("en-GB");//英国 
                    _rmoney = fmoney.ToString("c", cul);
                    break;
                case 4:
                    _rmoney = string.Format("{0:N" + point + "}", fmoney);//没有货币符号 
                    break;
                default:
                    _rmoney = string.Format("{0:N" + point + "}", fmoney);
                    break;
            }
            return _rmoney;
        }
        #endregion

        #region 将数据转化成万元  并保留n位小数
        /// <summary>
        /// 将数据转化成万元  并保留n位小数
        /// </summary>
        /// <param name="o">钱数</param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static string ConvertYuanToWanyuan(object o, int point)
        {
            decimal yuan = 0;
            if (o.GetType() == typeof(decimal))
            {
                yuan = Convert.ToDecimal(o);
            }
            else
            {
                yuan = decimal.Parse(o.ToString());
            }
            decimal wanyuan = yuan / 10000;
            return string.Format("{0:N" + point.ToString() + "}", wanyuan);
        }
        #endregion
    }
    #endregion
}

