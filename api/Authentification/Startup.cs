using Authentification.Models;
using Authentification.Repositories.Entities;
using Authentification.Repositories.ProjectRepository;
using Authentification.Repositories.UserRepository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authentification
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<UserContext>(options => options.UseNpgsql(connection));
            services.AddScoped<LogRepository>(s => new LogRepository(connection));
            services.AddScoped<SLogRepository>(s => new SLogRepository(connection));
            services.AddScoped<ProjectRepository>(s => new ProjectRepository(connection));
            services.AddScoped<UserRepository>(s => new UserRepository(connection));
            services.AddScoped<RoleRepository>(s => new RoleRepository(connection));
            services.AddScoped<TasksRepository>(s => new TasksRepository(connection));
            services.AddScoped<TrackerRepository>(s => new TrackerRepository(connection));
            services.AddScoped<SpringRepository>(s => new SpringRepository(connection));
            services.AddScoped<CategoryRepository>(s => new CategoryRepository(connection));
            services.AddScoped<PeopleRepository>(s => new PeopleRepository(connection));
            services.AddScoped<WikiRepository>(s => new WikiRepository(connection));

            // установка конфигурации подключения
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => //CookieAuthenticationOptions
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/api/user");
                });

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseCors(builder => builder
.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader()
.AllowCredentials());
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
