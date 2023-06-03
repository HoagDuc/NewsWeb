using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace NewsWeb.Models
{
  public partial class Category
  {
    public Category()
    {
      Posts = new HashSet<Post>();
    }

    [Key]
    public int CategoryId { get; set; }
    [Required]
    public string CategoryName { get; set; }
    [Required]
    public string Title { get; set; }
    public string Alias { get; set; }
    [Required]
    public string MetaDesc { get; set; }
    [Required]
    public string MetaKey { get; set; }
    public string Thumb { get; set; }
    public bool? Published { get; set; }
    public int? Ordering { get; set; }
    public int? Parents { get; set; }
    public int? Levels { get; set; }
    public string Icon { get; set; }
    public string Cover { get; set; }
    [Required]
    public string Description { get; set; }

    public virtual ICollection<Post> Posts { get; set; }
  }
}
