using System;
using System.Reflection;
using System.Web;


namespace MathSoftCommonLib
{
    public class RequestHelper
    {
        public T Request2Object<T>(HttpRequest Requst) where T : new()
        {
            T t = new T();
            Type Ts = t.GetType();
            PropertyInfo[] properts = Ts.GetProperties();
            foreach (PropertyInfo item in properts)
            {
            }
            return t;
        }
    }
}
