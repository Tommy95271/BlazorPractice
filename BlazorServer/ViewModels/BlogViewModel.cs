using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServer.ViewModels
{
    public class BlogViewModel
    {
        public int BlogId { get; set; }
        [Required(ErrorMessage = "部落格名稱為必填")]
        [MaxLength(10, ErrorMessage = "部落格名稱太長")]
        public string BlogName { get; set; }
        public List<PostViewModel> Posts { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}
