
using PersonnelDepartment.ServicesConfigurator;

namespace PersonnelDepartment;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Initialize(services =>
        {
            builder.Services.AddControllersWithViews();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.Initialize();
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();

        app.UseStaticFiles();
        app.UseHttpsRedirection();
        app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());

        app.Run();
    }
}
