using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Models
{
    /// <summary>
    /// 试题二级分类设置表
    /// </summary>
    public class CQSKs
    {
        [Key]
        public int QskId { get; set; }//主键
        public int FirstKindId { get; set; }//试题一级分类编号
        public string FirstKindName { get; set; }//试题一级分类名称
        public int SecondKindId { get;set; }//试题二级分类编号
        public string SecondKindName { get;set; }//试题一级分类名称

    }
}
