using System.ComponentModel.DataAnnotations;

namespace EFCore.Models
{
    /// <summary>
    /// 三级机构设置表
    /// </summary>
    public class CFTKs
    {
        [Key]
        public int FtkId { get; set; }//主键
        public string FirstKindId { get; set; }//一级机构编号
        public string FirstKindName { get; set; }//一级机构名称
        public string SecondKindId { get; set; }//二级机构编号
        public string SecondKindName { get; set; }//二级机构名称
        public string ThirdKindId { get; set; }//三级机构编号
        public int ThirdKindIsRetail { get; set; }//三级机构是否为零售店
        public string ThirdKindName {  get; set; }//三级机构名称
        public string ThirdKindSaleId {  get; set; }//三级机构销售责任人编号

	}
}
