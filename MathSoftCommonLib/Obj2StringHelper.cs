using System;

namespace MathSoftCommonLib
{
    /// <summary>
    /// 由任意对象生成string类型
    /// 然后基本的思想就是model理念写常量去
    /// </summary>
    public class Obj2StringHelper<T>
    {
        public string Convert(T t1, Func<T, string> converter)
        {
            return converter(t1);
        }
    }
}
