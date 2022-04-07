using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;
using WebApiOptimization.Application.Handlers.CommandHandlers.CategoryHandlers;
using WebApiOptimization.Application.Handlers.CommandHandlers.EmployeeHandlers;
using WebApiOptimization.Application.Handlers.QueryHandlers.Category;
using WebApiOptimization.Application.Handlers.QueryHandlers.Employee;
using WebApiOptimization.Core.Repositories;
using WebApiOptimization.Core.Repositories.Base;
using WebApiOptimization.Infrastructure.Data;
using WebApiOptimization.Infrastructure.Repositories;
using WebApiOptimization.Infrastructure.Repositories.Base;

namespace WebApiOptimization.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddDbContext<NorthwndContext>(x => x.UseSqlServer(Configuration.GetConnectionString("NorthwndDB")), ServiceLifetime.Transient);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApiOptimization.API", Version = "v1" });
            });
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            #region AddTransient repos

            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ICustomerCustomerDemoRepository, CustomerCustomerDemoRepository>();
            services.AddTransient<ICustomerDemographicRepository, CustomerDemographicRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IEmployeeTerritoryRepository, EmployeeTerritoryRepository>();
            services.AddTransient<IOrderDetailRepository, OrderDetailRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IRegionRepository, RegionRepository>();
            services.AddTransient<IShipperRepository, ShipperRepository>();
            services.AddTransient<ISupplierRepository, SupplierRepository>();
            services.AddTransient<ITerritoryRepository, TerritoryRepository>();

            #endregion

            #region AddMediatr

            // one is enough
            services.AddMediatR(typeof(CreateEmployeeHandler).GetTypeInfo().Assembly);
            /*
            services.AddMediatR(typeof(GetAllEmployeesHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(GetEmployeeByIdHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(DeleteEmployeeHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(UpdateEmployeeHandler).GetTypeInfo().Assembly);

            services.AddMediatR(typeof(CreateCategoryHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(GetAllCategoriesHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(GetCategoryByIdHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(DeleteCategoryHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(UpdateCategoryHandler).GetTypeInfo().Assembly);
            */
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiOptimization.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
