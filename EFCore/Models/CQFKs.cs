using System.ComponentModel.DataAnnotations;

namespace EFCore.Models
{
    /// <summary>
    /// 试题一级分类设置表
    /// </summary>
    public class CQFKs
    {
        [Key]public int QskId { get; set; }//主键
        public int FirstKindId { get; set; }//试题一级分类编号
        public string FirstKindName { get; set; }//试题一级分类名称
    }
}
