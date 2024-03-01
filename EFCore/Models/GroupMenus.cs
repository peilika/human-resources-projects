using System.ComponentModel.DataAnnotations;

namespace EFCore.Models
{
    /// <summary>
    /// 分组表
    /// </summary>
    public class GroupMenus
    {
        [Key]public int GroupID { get; set; }//主键
        public string GroupName { get; set; }//分组名称
    }
}
