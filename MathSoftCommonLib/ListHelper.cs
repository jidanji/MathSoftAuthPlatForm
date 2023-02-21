using System.Collections.Generic;

namespace MathSoftCommonLib
{
    /// <summary>
    /// 泛型列表操作
    /// </summary>
    public class ListHelper
    {
        #region 将泛型列表进行分页化操作
        /// <summary>
        /// 将泛型列表进行分页化操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputList"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static List<IEnumerable<T>> ListPager<T>(List<T> inputList, int pageSize = 999)
        {
            List<IEnumerable<T>> list = new List<IEnumerable<T>>();

            int pageCount;

            int count = inputList.Count;
            var yushu = count % pageSize;
            if (yushu == 0)
            {
                pageCount = count / pageSize;
            }
            else
            {
                pageCount = (count / pageSize) + 1;
            }
            for (int i = 0; i < pageCount; i++)
            {

                List<T> subList = new List<T>();
                int begIndex = i * pageSize;
                int endIndex = (i + 1) * pageSize;
                if (endIndex > inputList.Count)
                {
                    endIndex = inputList.Count;
                }

                for (int j = begIndex; i < endIndex; j++)
                {
                    subList.Add(inputList[j]);
                }
                list.Add(subList);
            }
            return list;
        }
        #endregion

        #region 将泛型列表进行分块
        /// <summary>
        /// 将泛型列表进行分块
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputList"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static List<IEnumerable<T>> Chuck<T>(List<T> inputList, int pageSize = 999)
        {
            List<IEnumerable<T>> list = new List<IEnumerable<T>>();

            int pageCount;

            int count = inputList.Count;
            var yushu = count % pageSize;
            if (yushu == 0)
            {
                pageCount = count / pageSize;
            }
            else
            {
                pageCount = (count / pageSize) + 1;
            }
            for (int i = 0; i < pageCount; i++)
            {

                List<T> subList = new List<T>();
                int begIndex = i * pageSize;
                int endIndex = (i + 1) * pageSize;
                if (endIndex > inputList.Count)
                {
                    endIndex = inputList.Count;
                }

                for (int j = begIndex; i < endIndex; j++)
                {
                    subList.Add(inputList[j]);
                }
                list.Add(subList);
            }
            return list;
        }
        #endregion
    }
}

