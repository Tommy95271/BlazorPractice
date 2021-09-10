using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorServer.Models
{
    public class BlogModel
    {
        [Key]
        public int BlogId { get; set; }
        [MaxLength(10, ErrorMessage = "部落格名稱太長")]
        public string BlogName { get; set; }
        public List<PostModel> Posts { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}
