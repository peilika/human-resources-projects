using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Models
{
    /// <summary>
    /// 职位设置表
    /// </summary>
    public class CMs
    {
        [Key]
        public int MakId { get; set; }//主键
        public string MajorKindId { get; set; }//职位分类编号
        public string MajorKindName { get; set; }//职位分类名称
        public string MajorId { get; set; }//职位编号
        public string MajorName { get; set; }//职位名称
        public int? TestAmount { get; set; }//题套数量

    }
}
