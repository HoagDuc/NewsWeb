using System;
using System.Collections.Generic;

#nullable disable

namespace NewsWeb.Models
{
    public partial class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string SContent { get; set; }
        public string Contents { get; set; }
        public string Thumb { get; set; }
        public bool? Published { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDesc { get; set; }
        public string MetaKey { get; set; }
        public string Alias { get; set; }
        public DateTime? CreateAt { get; set; }
        public string Author { get; set; }
        public string Tag { get; set; }
        public int? UserId { get; set; }
        public int? CategoryId { get; set; }
        public bool? IsHot { get; set; }
        public bool? IsNewfeed { get; set; }

        public virtual Category Category { get; set; }
        public virtual User User { get; set; }
    }
}
