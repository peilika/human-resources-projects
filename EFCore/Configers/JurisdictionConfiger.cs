using EFCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Configers
{
	public class JurisdictionConfiger : IEntityTypeConfiguration<Jurisdictions>
	{
		public void Configure(EntityTypeBuilder<Jurisdictions> builder)
		{
			//建表  
			builder.ToTable(nameof(Jurisdictions));
			//初始化数据
			builder.HasData(
				new Jurisdictions { JuriID = 1 , JurName = "招聘管理", GroupID = 1, JurAddress = "position" },
				new Jurisdictions { JuriID = 2 , JurName = "职位发布管理", GroupID = 2, JurAddress = "position" },
				new Jurisdictions { JuriID = 3 , JurName = "职位发布登记", GroupID = 3, JurAddress = "position-register" },
				new Jurisdictions { JuriID = 4 , JurName = "职位发布变更", GroupID = 3, JurAddress = "position-change-update" },
				new Jurisdictions { JuriID = 5 , JurName = "职位发布查询", GroupID = 3, JurAddress = "position-release" },
				new Jurisdictions { JuriID = 6 , JurName = "简历管理", GroupID = 2, JurAddress = "register-html" },
				new Jurisdictions { JuriID = 7 , JurName = "简历登记", GroupID = 3, JurAddress = "register" },
				new Jurisdictions { JuriID = 8 , JurName = "简历筛选", GroupID = 3, JurAddress = "register-choose" },
				new Jurisdictions { JuriID = 9 , JurName = "有效简历查询", GroupID = 3, JurAddress = "valid-resume" },
				new Jurisdictions { JuriID = 10 , JurName = "面试管理", GroupID = 2, JurAddress = "interview-html" },
				new Jurisdictions { JuriID = 11 , JurName = "面试结构登记", GroupID = 3, JurAddress = "interview" },
				new Jurisdictions { JuriID = 12 , JurName = "面试筛选", GroupID = 3, JurAddress = "sift-list" },
				new Jurisdictions { JuriID = 13 , JurName = "录用管理", GroupID = 2, JurAddress = "register-two" },
				new Jurisdictions { JuriID = 14 , JurName = "录用申请", GroupID = 3, JurAddress = "register-list" },
				new Jurisdictions { JuriID = 15 , JurName = "录用审批", GroupID = 3, JurAddress = "check-list" },
				new Jurisdictions { JuriID = 16 , JurName = "录用查询", GroupID = 3, JurAddress = "list" },
				new Jurisdictions { JuriID = 17 , JurName = "人力资源档案管理", GroupID = 1, JurAddress = "human" },
				new Jurisdictions { JuriID = 18 , JurName = "人力资源档案登记", GroupID = 3, JurAddress = "human-register" },
				new Jurisdictions { JuriID = 19 , JurName = "人力资源档案登记复核", GroupID = 3, JurAddress = "check-list" },
				new Jurisdictions { JuriID = 20 , JurName = "人力资源档案查询", GroupID = 3, JurAddress = "query-locate" },
				new Jurisdictions { JuriID = 21 , JurName = "人力资源档案变更", GroupID = 3, JurAddress = "change-locate" },
				new Jurisdictions { JuriID = 22 , JurName = "人力资源档案删除变更", GroupID = 2, JurAddress = "delete-locate" },
				new Jurisdictions { JuriID = 23 , JurName = "人力资源档案删除", GroupID = 3, JurAddress = "delete-locate" },
				new Jurisdictions { JuriID = 24 , JurName = "档案删除恢复", GroupID = 3, JurAddress = "recovery-locate" },
				new Jurisdictions { JuriID = 25 , JurName = "人力资源档案永久删除", GroupID = 3, JurAddress = "delete-forever-list" },
				new Jurisdictions { JuriID = 26 , JurName = "薪酬标准管理", GroupID = 1, JurAddress = "salarytandard" },
				new Jurisdictions { JuriID = 27 , JurName = "薪酬标准登记", GroupID = 3, JurAddress = "salarystandard-register" },
				new Jurisdictions { JuriID = 28 , JurName = "薪酬标准登记复核", GroupID = 3, JurAddress = "salarystandard-check-list" },
				new Jurisdictions { JuriID = 29 , JurName = "薪酬标准查询", GroupID = 3, JurAddress = "salarystandard-query-locate" },
				new Jurisdictions { JuriID = 30 , JurName = "薪酬标准变更", GroupID = 3, JurAddress = "salarystandard-change-locate" },
				new Jurisdictions { JuriID = 31 , JurName = "薪酬发放管理", GroupID = 1, JurAddress = "register-locate" },
				new Jurisdictions { JuriID = 32 , JurName = "薪酬发放登记", GroupID = 3, JurAddress = "register-locate" },
				new Jurisdictions { JuriID = 33 , JurName = "薪酬发放登记复核", GroupID = 3, JurAddress = "check-list" },
				new Jurisdictions { JuriID = 34 , JurName = "薪酬发放查询", GroupID = 3, JurAddress = "query-locate" },
				new Jurisdictions { JuriID = 35 , JurName = "调动管理", GroupID = 1, JurAddress = "register" },
				new Jurisdictions { JuriID = 36 , JurName = "调动登记", GroupID = 3, JurAddress = "register-list" },
				new Jurisdictions { JuriID = 37 , JurName = "调动审核", GroupID = 3, JurAddress = "chec-list" },
				new Jurisdictions { JuriID = 38 , JurName = "调动查询", GroupID = 3, JurAddress = "locate" },
				new Jurisdictions { JuriID = 39 , JurName = "客户化设置", GroupID = 1, JurAddress = "management" },
				new Jurisdictions { JuriID = 40 , JurName = "人力资源档案管理设置", GroupID = 2, JurAddress = "management" },
				new Jurisdictions { JuriID = 41 , JurName = "I级机构设置", GroupID = 3, JurAddress = "first-kind" },
				new Jurisdictions { JuriID = 42 , JurName = "II级机构设置", GroupID = 3, JurAddress = "second-kind" },
				new Jurisdictions { JuriID = 43 , JurName = "III级机构设置", GroupID = 3, JurAddress = "third-kind" },
				new Jurisdictions { JuriID = 44 , JurName = "职称设置", GroupID = 3, JurAddress = "profession-design" },
				new Jurisdictions { JuriID = 45 , JurName = "职位分类设置", GroupID = 3, JurAddress = "major-kind" },
				new Jurisdictions { JuriID = 46 , JurName = "职位设置", GroupID = 3, JurAddress = "major" },
				new Jurisdictions { JuriID = 47 , JurName = "公共属性设置", GroupID = 3, JurAddress = "public-char" },
				new Jurisdictions { JuriID = 48 , JurName = "薪酬管理设置", GroupID = 2, JurAddress = "薪酬项目设置" },
				new Jurisdictions { JuriID = 49 , JurName = "薪酬管理设置", GroupID = 3, JurAddress = "salary-item" },
				new Jurisdictions { JuriID = 50 , JurName = "薪酬发放方式设置", GroupID = 3, JurAddress = "salary-grant-mode" },
				new Jurisdictions { JuriID = 51 , JurName = "其他设置", GroupID = 2, JurAddress = "其他设置" },
				new Jurisdictions { JuriID = 52 , JurName = "关键字查询设置", GroupID = 3, JurAddress = "primary-key" },
				new Jurisdictions { JuriID = 53 , JurName = "标准数据报表", GroupID = 1, JurAddress = "标准数据报表" },
				new Jurisdictions { JuriID = 54 , JurName = "Excel标准数据报表", GroupID = 3, JurAddress = "excal-locate" },
				new Jurisdictions { JuriID = 55 , JurName = "Pdf标准数据报表", GroupID = 3, JurAddress = "pdf-locate" },
				new Jurisdictions { JuriID = 56 , JurName = "Xml标准数据报表", GroupID = 3, JurAddress = "xml-locate" },
				new Jurisdictions { JuriID = 57 , JurName = "权限管理", GroupID = 1, JurAddress = "roles" },
				new Jurisdictions { JuriID = 58 , JurName = "用户管理", GroupID = 3, JurAddress = "user-list" },
				new Jurisdictions { JuriID = 59 , JurName = "角色管理", GroupID = 3, JurAddress = "right-list" }
			);
		}
	}
}
