using EFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HREF.Model
{
    public class SalaryPayViewModel
    {
        public List<salary_grant_details> GDet { get; set; }

        public salary_grant GDet1 { get; set; }
        public List<salary_standard_detail> SDet { get; set; }
		public HFs humanInsert { get; set; }
	}
}
