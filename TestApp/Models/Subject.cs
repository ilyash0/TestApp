using System;
using System.Collections.Generic;

namespace TestApp.Models;

public partial class Subject
{
    public int SubjectId { get; set; }

    public string SubjectName { get; set; } = null!;

    public virtual ICollection<Test> Tests { get; set; } = new List<Test>();
}
