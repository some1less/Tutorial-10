using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Tutorial10.RestAPI;
using Tutorial10.RestAPI.DTO;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<Tutorial10Context>(options => options.UseSqlServer(connectionString));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/jobs", (Tutorial10Context context) => {
    try
    {
        return Results.Ok(context.Jobs);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapGet("/api/departments", (Tutorial10Context context) => {
    try
    {
        return Results.Ok(context.Departemnts);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapGet("/api/employees", (async (Tutorial10Context context, CancellationToken cancellationToken) =>
{
    try
    {
        var employees = await context.Employees.ToListAsync(cancellationToken);
        var empDtos = new List<EmployeeDTO>();

        foreach (var emp in employees)
        {
            empDtos.Add(new EmployeeDTO()
            {
                Id = emp.Id,
                Name = emp.Name,
                Commission = emp.Commission,
                DepartmentId = emp.DepartmentId,
                HireDate = emp.HireDate,
                Salary = emp.Salary,
                JobId = emp.JobId,
                ManagerId = emp.ManagerId,
            });
        }
        
        return Results.Ok(empDtos);
        
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
}));

app.MapGet("/api/employees/{id}", (int id, CancellationToken cancellationToken, Tutorial10Context context) =>
{
    try
    {
        var emp = context.Employees.
            Include(e => e.Department).
            FirstOrDefault(e => e.Id == id);
        var employeeDto = new EmployeeDTO()
        {
            Id = emp.Id,
            Name = emp.Name,
            Commission = emp.Commission,
            DepartmentId = emp.DepartmentId,
            HireDate = emp.HireDate,
            Salary = emp.Salary,
            JobId = emp.JobId,
            ManagerId = emp.ManagerId,
        };

    return Results.Ok(employeeDto);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
    
});

app.MapPost("/api/employees", async (Tutorial10Context context, CancellationToken cancellationToken, Employee empDto) =>
{
    try
    {
        await context.Employees.AddAsync(empDto, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return Results.Created($"/api/employees/{empDto.Id}", empDto);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapPut("/api/employees/{id}", async (int id, Tutorial10Context context, CancellationToken cancellationToken, EmployeeDTO empDto) =>
{
    try
    {
        var emp = context.Employees.
            Include(e => e.Department).
            FirstOrDefault(e => e.Id == id);
        if (emp == null) throw new KeyNotFoundException($"Employee with id {id} not found");
        
        emp.Name = empDto.Name;
        emp.Commission = empDto.Commission;
        emp.DepartmentId = empDto.DepartmentId;
        emp.HireDate = empDto.HireDate;
        emp.Salary = empDto.Salary;
        emp.JobId = empDto.JobId;
        emp.ManagerId = empDto.ManagerId;
        
        context.Entry(emp).State = EntityState.Modified;
        await context.SaveChangesAsync(cancellationToken);
        
        return Results.NoContent();
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapDelete("/api/employees/{id}", async (int id, Tutorial10Context context, CancellationToken cancellationToken) =>
{
    try
    {
        var emp = await context.Employees.FindAsync(id);
        if (emp != null)
        {
            context.Employees.Remove(emp);
            await context.SaveChangesAsync(cancellationToken);
        }
        
        return Results.NoContent();
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
    
});

app.Run();
