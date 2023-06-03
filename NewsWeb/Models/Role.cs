using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace NewsWeb.Models
{
  public partial class Role
  {
    public Role()
    {
      Users = new HashSet<User>();
    }

    [Key]
    public int RoleId { get; set; }
    [DisplayName("RoleName")]
    [Required]
    //[Remote(action: "VerifyRole", controller: "Roles", ErrorMessage = "Role is already available")]
    public string RoleName { get; set; }

    [DisplayName("RoleDescription")]
    [Required]
    public string RoleDescription { get; set; }

    public virtual ICollection<User> Users { get; set; }
  }
}
