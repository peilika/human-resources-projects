using System.ComponentModel.DataAnnotations;

namespace EFCore.Models
{
    /// <summary>
    /// 用户角色表
    /// </summary>
    public class UserRoles
    {
        [Key]
        public int UserRolesID {  get; set; }   // 用户角色编号
        public int UserID {  get; set; }    // 用户编号
        public int RolesID {  get; set; }   // 角色编号
    }
}
