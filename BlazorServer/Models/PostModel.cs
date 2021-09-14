using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServer.Models
{
    public class PostModel
    {
        [Key]
        public int PostId { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "標題太長")]
        public string Title { get; set; }
        [Required]
        [MinLength(100, ErrorMessage = "內容太短")]
        public string Content { get; set; }
        public int BlogId { get; set; }
        public BlogModel Blog { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}
