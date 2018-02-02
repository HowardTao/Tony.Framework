using Tony.Application.Entity.SystemManage;
using System.Data.Entity.ModelConfiguration;

namespace Tony.Application.Mapping.SystemManage
{
    /// <summary>
    /// 版 本
    /// 创 建：陶海华
    /// 日 期：2017-10-24 08:57
    /// 描 述：数据字典明细表
    /// </summary>
    public class DataItemDetailMap : EntityTypeConfiguration<DataItemDetailEntity>
    {
        public DataItemDetailMap()
        {
            #region 表、主键
            //表
            this.ToTable("Base_DataItemDetail");
            //主键
            this.HasKey(t => t.ItemDetailId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
