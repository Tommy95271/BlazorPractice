using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServer.ViewModels
{
    public class PostViewModel
    {
        public int PostId { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "標題太長")]
        public string Title { get; set; }
        [Required]
        [MinLength(50, ErrorMessage = "內容太短")]
        public string Content { get; set; }
        public int BlogId { get; set; }
        public BlogViewModel Blog { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
