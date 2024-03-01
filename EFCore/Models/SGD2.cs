using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Models
{
	public class SGD2
	{
		[Key]
public int GrdId  {get;set;}
 public string  SalaryGrantId {get;set;}
public string HumanId {get;set;}
public string HumanName {get;set;}
public int BounsSum {get;set;}
public int SaleSum {get;set;}
public int DeductSum {get;set;}
public int SalaryStandardSum {get;set;}
public int SalaryPaidSum {get;set;}

	}
}
