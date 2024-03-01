using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HREF.Model
{
    public class Salary_standard_details
    {
        [Key]
        public short sdt_id { get; set; }
        [DisplayName("薪酬项目名称：")]
        public string item_name { get; set; }
    }
}
