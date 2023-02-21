using System.Collections.Generic;

namespace MathSoftCommonLib
{
    /// <summary>
    ///作者马良 
    ///日期：2016年11月9日
    ///视图操作接口 用于统一视图操作。
    /// </summary>
    /// <typeparam name="ModelType"></typeparam>
    public interface IViewSearch<ModelType>
    {
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ModelType GetModel(string strWhere);

        /// <summary>
        /// 获取记录总数
        /// </summary>
        int GetRecordCount(string strWhere);

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        List<ModelType> GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);

        /// <summary>
        /// 获取数据列表
        /// </summary>
        List<ModelType> GetListByPage(string strWhere, string orderby);
    }
}
