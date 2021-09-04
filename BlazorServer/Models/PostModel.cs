using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServer.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "標題太長")]
        public string Title { get; set; }
        [Required]
        [MinLength(100, ErrorMessage = "內容太短")]
        public string Content { get; set; }
    }
}
