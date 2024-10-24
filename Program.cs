using EmployeeManagement.Data;
using EmployeeManagement.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;

namespace EmployeeManagement
{
	public class Program
	{
		public static void Main(string[] args)
		{

			string corsPolicyName = "MyCors";

			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddDbContext<AppDbContext>(
				options => options.UseInMemoryDatabase("EmployeeDb")
			);

			builder.Services.AddCors(options =>
			{
				options.AddPolicy(corsPolicyName, builder =>
				{
					builder.WithOrigins("http://localhost:4200")
					.AllowAnyMethod()
					.AllowAnyHeader();
				});
			});

			// Add the employee repository to the Dependency injection 
			builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

			builder.Services.AddControllers();

			// Setup Swagger API tool
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI(c => {
					c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
					c.RoutePrefix = string.Empty;
				});
			}

			app.UseCors(corsPolicyName);

			app.MapControllers();

			app.Run();
		}
	}
}