using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ProjectManagement.Models;
using ProjectManagementTracker.Business.Manager.Contract;
using ProjectManagementTracker.Business.Manager.Implementation;
using ProjectManagementTracker.DAL.Repository.Contract;
using ProjectManagementTracker.DAL.Repository.Implementation;

namespace ProjectManagementTracker
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;           
        }

        public IConfiguration Configuration { get; }      

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("Cors", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            services.AddScoped<IProjectManagementManager, ProjectManagementManager>();
            services.AddScoped<IProjectManagementRepository, ProjectManagementRepository>();
            services.AddScoped<ISkillSetManager, SkillSetManager>();
            services.AddScoped<ISkillSetRepository, SkillSetRepository>();
            services.AddScoped<IProjectMemberManager, ProjectMemberManager>();
            services.AddScoped<IProjectMemberRepository, ProjectMemberRepository>();
            services.AddScoped<IProjectManager, ProjectManager>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Project Management Tracker", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseCors("Cors");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Project Management Tracker v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseMiddleware<CustomeErrorHandler>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
