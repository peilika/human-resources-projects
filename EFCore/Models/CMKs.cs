using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Models
{
    /// <summary>
    /// 职位分类设置表
    /// </summary>
    public class CMKs
    {
        [Key]
        public int MfkId { get; set; }//主键
        public string MajorKindId { get; set; }//职位分类编号
        public string MajorKindName { get; set; }//职位分类名称

    }
}
