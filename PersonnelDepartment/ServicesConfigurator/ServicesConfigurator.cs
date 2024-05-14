using PersonnelDepartment.Services.Contracts;
using PersonnelDepartment.Services.Contracts.Repository;
using PersonnelDepartment.Services.Departments;
using PersonnelDepartment.Services.Departments.Repository;
using PersonnelDepartment.Services.DismissalOrders;
using PersonnelDepartment.Services.DismissalOrders.Repository;
using PersonnelDepartment.Services.Employees;
using PersonnelDepartment.Services.Employees.Repository;
using PersonnelDepartment.Tools.Database;

namespace PersonnelDepartment.ServicesConfigurator;

public static class ServicesConfigurator
{
    public static void Initialize(this IServiceCollection services)
    {
        #region Services

        String connectionString = "Server=localhost;Port=5432;Database=personneldepartment;User Id=postgres;Password=root";

        services.AddSingleton<IMainConnector>(new MainConnector(connectionString));

        services.AddSingleton<IEmployeeService, EmployeeService>();
        services.AddSingleton<IDepartmentService, DepartmentService>();
        services.AddSingleton<IContractsService, ContractsService>();
        services.AddSingleton<IDismissalOrdersService, DismissalOrdersService>();

        #endregion Services

        #region Repositories

        services.AddSingleton<IEmployeeRepository, EmployeeRepository>();
        services.AddSingleton<IDepartmentsRepository, DepartmentsRepository>();
        services.AddSingleton<IContractsRepository, ContractsRepository>();
        services.AddSingleton<IDismissalOrdersRepository, DismissalOrdersRepository>();

        #endregion Repositories
    }
}
