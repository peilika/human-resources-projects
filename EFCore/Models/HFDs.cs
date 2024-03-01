using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace EFCore.Models
{
    /// <summary>
    /// 记录人力资源档案所做的任何更改表
    /// </summary>
    public class HFDs
    {
        [Key]
        public int HfdId { get; set; }//主键
        public string? HumanId { get; set; }//档案编号
        public string? FirstKindId { get; set; }//一级机构编号
        public string? FirstKindName { get; set; }//一级机构名称
        public string? SecondKindId { get; set; }//二级机构编号
        public string? SecondKindName { get; set; }//二级机构名称
        public string? ThirdKindId { get; set; }//三级机构编号
        public string? ThirdKindName { get; set; }//三级机构名称
        public string? HumanName { get; set; }//姓名
        public string? HumanAddress { get; set; }//地址
        public string? HumanPostcode { get; set; }//邮政编码
        public string? HumanProDesignation { get; set; }//职称
        public string? HumanMajorKindId { get; set; }//职位分类编号
        public string? HumanMajorKindName { get;set; }//职位分类名称
        public string? HumanMajorId { get; set; }//职位编号
        public string? HunmaMajorName { get; set; }//职位名称
        public string? HumanTelephone { get;set; }//电话
        public string? HumanMobilephone { get; set; }//手机编号
        public string? HumanBank { get; set; }//开户银行
        public string? HumanAccount { get; set; }//银行帐户
        public string? HumanQQ { get; set; }//QQ号码
        public string? HumanEmail { get; set; }//电子邮件
        public string? HumanHobby { get; set; }//爱好
        public string? HumanSpeciality { get; set; }//特长
        public string? HumanSex { get; set; }//性别
        public string? HumanReligion { get; set; }//宗教信仰
        public string? HumanParty { get; set; }//政治面貌
        public string? HumanNationality { get; set; }//国籍
        public string? HumanRace { get; set; }//民族
        public DateTime? HumanBirthday { get; set; }//出生日期
        public string? HumanBirthplace { get; set; }//出生地
        public int? HumanAge { get; set; }//年龄
        public string? HumanEducatedDegree { get; set; }//学历
        public string? HumanEducatedYears { get; set; }//教育年限
        public string? HumanEducatedMajor { get; set; }//学历专业
        public string? HumanSocietySecurityId { get; set; }//社会保障号
        public string? HumanIdCard { get; set; }//身份证号
        public string? Remark { get; set; }//备注
        public string? SalaryStandardId { get; set; }//薪酬标准编号
        public string? SalaryStandardName { get; set; }//薪酬标准名称
        public double? SalarySum { get; set; }//基本薪酬总额
        public double? DemandSalaraySum { get; set; }//应发薪酬总额
        public double? PaidSalarySum { get; set; }//实发薪酬总额
        public int? MajorChangeAmount { get; set; }//调动次数
        public int? BonusAmount { get; set; }//激励累计次数
        public int? TrainingAmount { get; set; }//培训累计次数
        public int? FileChangAmount { get; set; }//档案变更累计次数
        public string? HumanHistroyRecords { get; set; }//简历
        public string? HumanFamilyMembership { get; set; }//家庭关系
        public string? HumanPicture { get; set; }//相片
        public string? AttachmentName { get; set; }//附件名称
        public int? CheckStatus { get; set; }//复核状态
        public string? Register { get; set; }//档案登记人
        public string? Checker { get; set; }//档案复核人
        public string? Changer { get; set; }//档案变更人
        public DateTime? RegistTime { get; set; }//档案登记时间
        public DateTime? CheckTime { get; set; }//档案复核时间
        public DateTime? ChangeTime { get; set; }//档案变更时间
        public DateTime? LastlyChangeTime { get; set; }//档案最近更改时间
        public DateTime? DeleteTime { get; set; }//档案删除时间
        public DateTime? RecoveryTime { get; set; }//档案恢复时间
        public int? HumanFileStatus { get; set; }//档案状态
    }
}
