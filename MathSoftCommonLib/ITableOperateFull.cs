using System.Collections.Generic;

namespace MathSoftCommonLib
{
    /// <summary>
    ///作者马良 
    ///日期：2016年11月9日
    ///表操作接口 用于统一表的操作。高级类库，覆盖全面
    /// </summary>
    /// <typeparam name="ModelType"></typeparam>
    /// <typeparam name="IDType"></typeparam>
    public interface ITableOperateFull<ModelType, IDType>
    {
        /// <summary>
        /// 得到最大ID
        /// </summary>
        IDType GetMaxId();

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(IDType modelID);

        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool AddModel(ModelType model);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool UpdateModel(ModelType model);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool DeleteModel(IDType UserID);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ModelType GetModel(IDType UserID);

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
