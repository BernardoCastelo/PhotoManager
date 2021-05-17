using BusinessLayer;
using DataLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace WebApi
{
    public class Startup
    {
        public const string CookieScheme = "SameSite=Strict";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Configuration 
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();
            services.AddSingleton<IConfiguration>(configuration);
            #endregion

            #region Database
            string connectionString = Configuration.GetConnectionString("PhotoManager");
            services.AddDbContext<IDbContainer, DbContainer>(options => options.UseSqlServer(connectionString));
            #endregion

            #region DataLayer
            services.AddTransient(typeof(IDbContainer), typeof(DbContainer));
            services.AddTransient(typeof(ICameraRepository), typeof(CameraRepository));
            services.AddTransient(typeof(IFileRepository), typeof(FileRepository));
            services.AddTransient(typeof(IPhotoRepository), typeof(PhotoRepository));
            services.AddTransient(typeof(ICategoryRepository), typeof(CategoryRepository));
            services.AddTransient(typeof(IUserRepository), typeof(UserRepository));
            #endregion

            #region BusinessLayer
            services.AddTransient(typeof(IPhotos), typeof(Photos));
            services.AddTransient(typeof(ICategories), typeof(Categories));
            services.AddTransient(typeof(IUsers), typeof(Users));
            #endregion

            #region Authentication
            services.AddAuthentication(CookieScheme) // Sets the default scheme to cookies
                .AddCookie(CookieScheme, options =>
                {
                    options.AccessDeniedPath = "/account/denied";
                    options.LoginPath = "/account/login";
                });
            #endregion

            services.AddControllers().AddNewtonsoftJson();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
