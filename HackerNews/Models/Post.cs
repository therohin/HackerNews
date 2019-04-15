namespace HackerNews.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Post")]
    public partial class Post
    {
        public int PostId { get; set; }

        [Required]
        [StringLength(500)]
        public string URL { get; set; }

        [Required]
        [StringLength(500)]
        public string Title { get; set; }

        public int RelatedUserId { get; set; }

        public int Vote { get; set; }

        public virtual User User { get; set; }
    }
}
