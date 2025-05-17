using System;
using System.Collections.Generic;

namespace Tutorial10.RestAPI;

public partial class Departemnt
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Location { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
