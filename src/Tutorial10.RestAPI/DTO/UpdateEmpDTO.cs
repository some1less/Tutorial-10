namespace Tutorial10.RestAPI.DTO;

public class UpdateEmpDTO
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int JobId { get; set; }

    public int ManagerId { get; set; }
    
    public DateTime HireDate { get; set; }

    public decimal Salary { get; set; }

    public decimal? Commission { get; set; }

    public int DepartmentId { get; set; }
}