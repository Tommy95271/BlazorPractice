using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorServer.ViewModels
{
    public class CustomRoleViewModel
    {
        public CustomRoleViewModel()
        {
            Users = new();
        }
        public string RoleId { get; set; }

        [Required(ErrorMessage = "角色名稱為必填")]
        public string RoleName { get; set; }
        public List<string> Users { get; set; }
    }
}
