using System.ComponentModel.DataAnnotations;

namespace EFCore.Models
{
    /// <summary>
    /// 一级机构设置表
    /// </summary>
    public class CFFKs
    {
        [Key]
        public int FfkId { get; set; }//主键
        public string FirstKindId { get; set; }//一级机构编号
        public string FirstKindName { get; set; }//一级机构名称
        public string FirstKindSalaryId { get; set; }//一级机构薪酬发放责任人编号
        public string FirstKindSaleId { get; set; }//一级机构销售责任人编号
    }
}
