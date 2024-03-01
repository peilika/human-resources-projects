using System.ComponentModel.DataAnnotations;

namespace EFCore.Models
{
    /// <summary>
    /// 角色表
    /// </summary>
    public class Roles
    {
        [Key]
        public int RolesID { get; set; }
        public string RolesName { get; set; }
        public string RolesInstruction { get; set; }
        public int RolesIf { get; set; }

    }
}
