using System.ComponentModel.DataAnnotations;

namespace EFCore.Models
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class Users
    {
        [Key]
        public int UId { get; set; }    // 用户编号
        public string UName {  get; set; }  // 用户名
        public string UTrueName {  get; set; }  // 真实姓名
        public string UPassWord {  get; set; }  // 密码
    }
}
