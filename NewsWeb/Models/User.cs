using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace NewsWeb.Models
{
  public partial class User
  {
    public User()
    {
      Posts = new HashSet<Post>();
    }
    [Key]
    public int UserId { get; set; }

    [Required(ErrorMessage = "UserName is required")]
    public string UserName { get; set; }

    [Display(Name = "Email address")]
    [Required(ErrorMessage = "The email address is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; }

    [Required(ErrorMessage = "You must provide a phone number")]
    [Display(Name = "Home Phone")]
    [DataType(DataType.PhoneNumber)]
    [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
    public string PhoneNumber { get; set; }

    [Required]
    public string Password { get; set; }
    public int? RoleId { get; set; }
    public bool? Active { get; set; }
    public string LastLogin { get; set; }

    public virtual Role Role { get; set; }
    public virtual ICollection<Post> Posts { get; set; }
  }
}
