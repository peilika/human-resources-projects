namespace HRUI.Models
{
	public class HFDsDAO
	{
		public int HfdId { get; set; }
		public string HumanId { get; set; }
		public string? FirstKindId { get; set; }//一级机构编号
		public string? FirstKindName { get; set; }//一级机构名称
		public string? SecondKindId { get; set; }//二级机构编号
		public string? SecondKindName { get; set; }//二级机构名称
		public string? ThirdKindId { get; set; }//三级机构编号
		public string? ThirdKindName { get; set; }//三级机构名称
		public string imageUrl { get; set; }    // 头像的相对路径
		public string HumanProDesignation { get; set; }// 选中的职称(id)
		public string HumanName { get; set; }   // 姓名
		public string HumanSex { get; set; }    // 文字的男女
		public string HumanEmail { get; set; }  // 邮箱
		public string HumanTelephone { get; set; } // 电话
		public string HumanQQ { get; set; } // QQ
		public string HumanMobilephone { get; set; }    // 手机
		public string HumanAddress { get; set; }    // 住址
		public string HumanPostcode { get; set; }   // 邮编
		public string HumanNationality { get; set; }   // 国籍（id）
		public string HumanBirthplace { get; set; } // 出生地
		public DateTime HumanBirthday { get; set; } // 生日
		public string HumanRace { get; set; }  // 民族（id）
		public string HumanReligion { get; set; }  // 宗教（id）
		public string HumanParty { get; set; } // 政治面貌
		public string HumanIdCard { get; set; } // 身份证号
		public string HumanSocietySecurityId { get; set; }  // 社会保障号码
		public int HumanAge { get; set; }   // 年龄
		public string HumanEducatedDegree { get; set; }    // 学历（id）
		public string HumanEducatedYears { get; set; } // 教育年限（id）
		public string HumanEducatedMajor { get; set; } // 学历专业
		public string SalaryStandardId { get; set; }   // 薪酬标准单编号
		public string HumanBank { get; set; }   // 开户行
		public string HumanAccount { get; set; }    // 银行账户
		public string HumanSpeciality { get; set; }    // 特长（id）
		public string HumanHobby { get; set; } // 爱好（id）
		public string HumanHistroyRecords { get; set; }    // 个人履历
		public string HumanFamilyMembership { get; set; }  // 家庭关系
		public string Remark { get; set; }  // 备注
		public string Changer { get; set; }//变更人
		public DateTime ChangeTime { get; set; }//变更时间
		public DateTime LastlyChangeTime { get; set; }//最近更新的时间
		public string? HumanMajorKindId { get; set; }//职位分类编号
		public string? HumanMajorKindName { get; set; }//职位分类名称
		public string? HumanMajorId { get; set; }//职位编号
		public string? HunmaMajorName { get; set; }//职位名称
		public string? SalaryStandardName { get; set; }//薪酬标准名称
		public double? SalarySum { get; set; }//基本薪酬总额
		public double? DemandSalaraySum { get; set; }//应发薪酬总额
		public double? PaidSalarySum { get; set; }//实发薪酬总额
		public int? MajorChangeAmount { get; set; }//调动次数
		public int? BonusAmount { get; set; }//激励累计次数
		public int? TrainingAmount { get; set; }//培训累计次数
		public int? FileChangAmount { get; set; }//档案变更累计次数
		public string? HumanPicture { get; set; }//相片
		public string? AttachmentName { get; set; }//附件名称
		public int? CheckStatus { get; set; }//复核状态
		public string? Register { get; set; }//档案登记人
		public string? Checker { get; set; }//档案复核人
		public DateTime? RegistTime { get; set; }//档案登记时间
		public DateTime? CheckTime { get; set; }//档案复核时间
		public DateTime? DeleteTime { get; set; }//档案删除时间
		public DateTime? RecoveryTime { get; set; }//档案恢复时间
		public int? HumanFileStatus { get; set; }//档案状态
	}
}
