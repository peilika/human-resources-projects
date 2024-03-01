using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace EFCore.Models
{
    /// <summary>
    /// 薪酬标准基本信息表
    /// </summary>
    public class SSs
    {
        [Key]
        public int SsdId {  get; set; } // 主键
        public string StandardId {  get; set; }    // 薪酬标准单编号
        public string StandardName {  get; set; }   // 薪酬标准单名称
        public string Designer { get; set; }    // 制定者名字
        public int Register {  get; set; }  // 登记人
        public int? Checker { get;set; } // 复核人
        public int? Changer {  get; set; }   // 变更人
        public DateTime RegistTime {  get; set; }   // 登记时间
        public DateTime? CheckTime {  get; set; }    // 复核时间
        public DateTime? ChangeTime {  get; set; }   // 变更时间
        public double? SalarySum {  get; set; }  // 薪酬总额
        public int CheckStatus { get; set; } = 0; // 是否经过复核
        public int ChangeStatus { get; set; } = 0; // 更改状态
        public string? CheckComment {  get; set; }   // 复核意见
        public string? Remark {  get; set; } // 备注

    }
}
