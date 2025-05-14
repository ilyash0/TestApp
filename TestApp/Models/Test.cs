using System;
using System.Collections.Generic;

namespace TestApp.Models;

public partial class Test
{
    public int TestId { get; set; }

    public string TestName { get; set; } = null!;

    public int CreatedBy { get; set; }

    public int SubjectId { get; set; }

    public string ImageSource { get; set; }

    public string? Description { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual Subject Subject { get; set; } = null!;
}
