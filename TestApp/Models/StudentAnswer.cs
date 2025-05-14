using System;
using System.Collections.Generic;

namespace TestApp.Models;

public partial class StudentAnswer
{
    public int QuestionQuestionId { get; set; }

    public int StudentUserId { get; set; }

    public int AnswerId { get; set; }

    public virtual Answer Answer { get; set; } = null!;
}
