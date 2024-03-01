using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Models
{
	public class ERs
	{
		[Key]
		public int ResId { get; set; }
		public string? HumanName { get; set; }
		public int? EngageType { get; set; }
		public string? HumanAddress { get; set; }
		public string? HumanPostcode { get; set; }
		public string? HumanMajorKindId { get; set; }
		public string? HumanMajorKindName { get; set; }
		public string? HumanMajorId { get; set; }
		public string? HunmaMajorName { get; set; }
		public string? HumanTelephone { get; set; }
		public string? HumanHomephone { get; set; }
		public string? HumanMobilephone { get; set; }
		public string? HumanEmail { get; set; }
		public string? HumanHobby { get; set; }
		public string? HumanSpeciality { get; set; }
		public string? HumanSex { get; set; }
		public string? HumanReligion { get; set; }
		public string? HumanParty { get; set; }
		public string? HumanNationality { get; set; }
		public string? HumanRace { get; set; }
		public string? HumanBirthday { get; set; }
		public int? HumanAge { get; set; }
		public string? HumanEducatedDegree { get; set; }
		public string? HumanEducatedYears { get; set; }
		public string? HumanEducatedMajor { get; set; }
		public string? HumanCollege { get; set; }
		public string? HumanIdcard { get; set; }
		public string? HumanBirthplace { get; set; }
		public string? DemandSalaryStandard { get; set; }
		public string? HumanHistoryRecords { get; set; }
		public string? Remark { get; set; }
		public string? Recomandation { get; set; }
		public string? HumanPicture { get; set; }
		public string? AttachmentName { get; set; }
		public string? CheckStatus { get; set; }
		public string? Register { get; set; }
		public string? RegistTime { get; set; }
		public string? Checker { get; set; }
		public string? CheckTime { get; set; }
		public string? InterviewStatus { get; set; }
		public string? TotalPoints { get; set; }
		public string? TestAmount { get; set; }
		public string? TestChecker { get; set; }
		public string? TestCheckTime { get; set; }
		public string? PassRegister { get; set; }
		public string? PassRegistTime { get; set; }
		public string? PassChecker { get; set; }
		public string? PassCheckTime { get; set; }
		public string? PassCheckStatus { get; set; }
		public string? PassCheckComment { get; set; }
		public string? PassPassComment { get; set; }
	}
}
