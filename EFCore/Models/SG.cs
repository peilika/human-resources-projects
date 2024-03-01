using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Models
{
    public class SG
    {
        [Key]
        public  int  SgrId { get; set; }//主键，自动增长列
        public string ? SalaryGrantId { get; set; }
        public string ? SalaryStandardId { get; set; }
        public string? FirstKindId { get; set; }//一级机构编号
        public string? FirstKindName { get; set; }//一级机构名称
        public string? SecondKindId { get; set; }//二级机构编号
        public string? SecondKindName { get; set; }//二级机构名称
        public string? ThirdKindId { get; set; }//三级机构编号
        public string? ThirdKindName { get; set; }//三级机构名称
        public int? HumanAmount { get; set;}
        public int? SalaryStandardSum { get; set;}
        public int? SalaryPaidSum { get; set; }
        public string? Register { get; set; }
        public string? RegistTime { get; set; }
        public string? Checker { get; set;}
        public string? CheckTime { get;set; }
        public bool? CheckStatus { get; set; }
    }
}
