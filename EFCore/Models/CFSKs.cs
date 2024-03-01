using System.ComponentModel.DataAnnotations;

namespace EFCore.Models
{
    /// <summary>
    /// 二级机构设置表
    /// </summary>
    public class CFSKs
    {
        [Key]
        public int FskId { get; set; }//主键
        public string FirstKindId { get; set; }//一级机构编号
        public string FirstKindName { get; set; }//一级机构名称
        public string SecondKindId { get; set; }//二级机构编号
        public string SecondKindName { get; set; }//二级机构名称
        public string  SecondSalaryId { get; set; }//二级机构薪酬发放责任人编号
        public string SecondSaleId { get; set; }//二级机构销售责任人编号

    }
}
