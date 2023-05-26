using System;
using System.Collections.Generic;

namespace WDA_Exam.Entities;

public partial class Classroom
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();
}
