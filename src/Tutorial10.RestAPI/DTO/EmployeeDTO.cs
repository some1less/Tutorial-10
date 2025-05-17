namespace Tutorial10.RestAPI.DTO;

public class EmployeeDTO
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string JobName { get; set; } = null!;

    public string? ManagerName { get; set; } = null!;
    public DateTime HireDate { get; set; }

    public decimal Salary { get; set; }

    public decimal? Commission { get; set; }

    public string? DepartmentName { get; set; }

}