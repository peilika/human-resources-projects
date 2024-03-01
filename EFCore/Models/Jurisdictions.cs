using System.ComponentModel.DataAnnotations;

namespace EFCore.Models
{
    /// <summary>
    /// 权限表
    /// </summary>
    public class Jurisdictions
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int JuriID{ get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        public string JurName { get; set; }
        /// <summary>
        /// 分组ID
        /// </summary>
        public int GroupID { get; set; }
        /// <summary>
        /// 权限地址
        /// </summary>
        public string JurAddress { get; set; }
    }
}
