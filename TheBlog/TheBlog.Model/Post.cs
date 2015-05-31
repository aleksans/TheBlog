using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheBlog.Model
{
    public class Post
    {
        public Post()
        {
            Tags = new HashSet<Tag>();
        }

        [Required(ErrorMessage = "Post Id is mandatory")]
        public int PostId { get; set; }

        [Required(ErrorMessage = "Title is mandatory")]
        [StringLength(200, ErrorMessage = "Title should not be longer than 300 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is mandatory")]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "FullDesctipntion is mandatory")]
        public string FullDescription { get; set; }

        [Required(ErrorMessage = "AddedOn date is mandatory")]
        public DateTime AddedOn { get; set; }

        [Required(ErrorMessage = "IsPublishedField is mandatory")]
        public bool IsPublished { get; set; }

        public int UserId { get; set; }

        public int CategoryId { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
     
        public virtual User User { get; set; }

        public virtual Category Category { get; set; }
    }
}
