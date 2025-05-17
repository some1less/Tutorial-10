using System;
using System.Collections.Generic;

namespace Tutorial10.RestAPI;

public partial class Employee
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int JobId { get; set; }

    public int? ManagerId { get; set; }

    public DateTime HireDate { get; set; }

    public decimal Salary { get; set; }

    public decimal? Commission { get; set; }

    public int DepartmentId { get; set; }

    public virtual Departemnt Department { get; set; } = null!;

    public virtual ICollection<Employee> InverseManager { get; set; } = new List<Employee>();

    public virtual Job Job { get; set; } = null!;

    public virtual Employee? Manager { get; set; }
}
