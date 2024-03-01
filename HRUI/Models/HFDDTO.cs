namespace HRUI.Models
{
	/// <summary>
	/// 接受人力资源档案登记请求传入的数据的类
	/// </summary>
	public class HFDDTO
	{
		public string JsonSelected {  get; set; }	// 选中的机构级联
		public string ImageUrl { get; set; }	// 头像的相对路径
		public string JsonSelectedCMKs {  get; set; }	// 职位选择的级联
		public int SelectedCpc {  get; set; }   // 选中的职称(id)
		public string HumanName { get; set; }	// 姓名
		public string HumanSex {  get; set; }	// 文字的男女
		public string HumanEmail {  get; set; }	// 邮箱
		public string HumanTelephone {  get; set; } // 电话
		public string HumanQQ {  get; set; }	// QQ
		public string HumanMobilephone {  get; set; }	// 手机
		public string HumanAddress {  get; set; }	// 住址
		public string HumanPostcode {  get; set; }	// 邮编
		public int HumanNationality {  get; set; }	// 国籍（id）
		public string HumanBirthplace {  get; set; }	// 出生地
		public DateTime HumanBirthday {  get; set; }	// 生日
		public int HumanRace {  get; set; }	// 民族（id）
		public int HumanReligion {  get; set; }	// 宗教（id）
		public int HumanParty {  get; set; }	// 政治面貌
		public string HumanIdCard {  get; set; }	// 身份证号
		public string HumanSocietySecurityId {  get; set; }	// 社会保障号码
		public int HumanAge {  get; set; }	// 年龄
		public int HumanEducatedDegree {  get; set; }	// 学历（id）
		public int HumanEducatedYears {  get; set; }	// 教育年限（id）
		public int HumanEducatedMajor {  get; set; }	// 学历专业
		public int SalaryStandardId {  get; set; }   // 薪酬标准单编号
		public string HumanBank {  get; set; }	// 开户行
		public string HumanAccount {  get; set; }	// 银行账户
		public string Register {  get; set; }	// 登记人（id）
		public DateTime RegistTime {  get; set; }	// 建档时间
		public int HumanSpeciality {  get; set; }	// 特长（id）
		public int HumanHobby {  get; set; }	// 爱好（id）
		public string HumanHistroyRecords {  get; set; }    // 个人履历
		public string HumanFamilyMembership {  get; set; }  // 家庭关系
		public string Remark {  get; set; }	// 备注

	}
}
