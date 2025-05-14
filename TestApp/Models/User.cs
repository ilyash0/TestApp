using System;
using System.Collections.Generic;

namespace TestApp.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Fullname { get; set; } = null!;

    public int? RoleId { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual Role? Role { get; set; }

    public virtual ICollection<Test> Tests { get; set; } = new List<Test>();
}
