using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace api.Dtos.Comment
{
    public class UpdateCommentDto
    {

        [MinLength(5, ErrorMessage = "Title need a leats 5 characters.")]
        [MaxLength(15, ErrorMessage = "Title can have a max of 15 characters.")]
        public string Title { get; set; } = string.Empty;

        [MinLength(4, ErrorMessage = "Comment need a leats 4 characters.")]
        [MaxLength(150, ErrorMessage = "Comment can have a max of 150 characters.")]
        public string Content { get; set; } = string.Empty;
    }
}