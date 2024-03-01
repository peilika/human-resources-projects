using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Models
{
    /// <summary>
    /// 公共字段设置表
    /// </summary>
    public class CPCs
    {
        [Key]
        public int PbcId { get; set; }//主键
        public string AttributeKind { get; set; }//属性的种类
        public string AttributeName { get; set; }//属性的名称

    }
}
