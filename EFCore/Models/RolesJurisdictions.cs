using System.ComponentModel.DataAnnotations;

namespace EFCore.Models
{
    /// <summary>
    /// 角色权限表
    /// </summary>
    public class RolesJurisdictions
    {
        [Key]
        public int RolesJID { get; set; }
        public int RolesID {  get; set; }
        public int JuriID {  get; set; }

    }
}
