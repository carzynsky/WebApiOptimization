using AspNetCoreRateLimit;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.IO.Compression;
using System.Reflection;
using WebApiOptimization.Application.Handlers.CommandHandlers.EmployeeHandlers;
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
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
            });

            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });

            services.AddDistributedSqlServerCache(options =>
            {
                options.ConnectionString = @"Data Source=DESKTOP-4P5I0TV;Initial Catalog=NORTHWND; Integrated Security=True;";
                options.SchemaName = "dbo";
                options.TableName = "CacheStore";
            });

            services.Configure<IpRateLimitOptions>(options =>
            {
                options.EnableEndpointRateLimiting = false;
                options.StackBlockedRequests = false;
                options.HttpStatusCode = 429;
                options.RealIpHeader = "X-Real-IP";
                options.ClientIdHeader = "X-ClientId";
                options.GeneralRules = new List<RateLimitRule> 
                { 
                    new RateLimitRule 
                    { 
                        Endpoint = "*", 
                        Period = "1m", 
                        Limit = 25 
                    },
                    new RateLimitRule
                    {
                        Endpoint = "*",
                        Period = "1h",
                        Limit = 1000
                    }
                };
            });
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
            services.AddInMemoryRateLimiting();

            services.AddMemoryCache();
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

            services.AddMediatR(typeof(CreateEmployeeHandler).GetTypeInfo().Assembly);

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

            app.UseResponseCompression();
            app.UseIpRateLimiting();
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
