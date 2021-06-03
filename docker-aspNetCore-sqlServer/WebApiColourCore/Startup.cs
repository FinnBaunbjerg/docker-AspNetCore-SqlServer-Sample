using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApiColourData;

namespace WebApiColourCore
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
            var sqlCnnBuilder = SqlConnectionStringBuilder();

            services.AddDbContext<ColourContext>( options => options.UseSqlServer( sqlCnnBuilder.ConnectionString));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApiColourCore", Version = "v1" });
            });
        }

        private SqlConnectionStringBuilder SqlConnectionStringBuilder()
        {
            // Environment variable injected from docker in order not to rebuild every image depending on environment
            var env = Configuration["ASPNETCORE_ENVIRONMENT"];
            System.Console.WriteLine($"Environment is {env}");

            var server = Configuration["DBSERVER"] ?? "localhost";
            System.Console.WriteLine($"Server name is {server}");

            var port = Configuration["DBPORT"] ?? "1433";
            System.Console.WriteLine($"Port is {port}");

            // Don't do this in production, create an sql image with another system user only here to save time
            var user = Configuration["DBSUSER"] ?? "SA";
            var password = Configuration["DBPASSWORD"] ?? "yourStrong(!)Password";

            var database = Configuration["Database"] ?? "Colour";

            var connectionString = $"Server={server},{port};Initial Catalog={database}";
            var sqlCnnBuilder = new SqlConnectionStringBuilder(connectionString);
            sqlCnnBuilder.UserID = $"{user}";
            sqlCnnBuilder.Password = $"{password}";
            return sqlCnnBuilder;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                SeedDatabase(app);
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiColourCore v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void SeedDatabase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<ColourContext>();

                PrepareDatabase.InitDatabase(dbContext);
            }
                
        }
    }
}
