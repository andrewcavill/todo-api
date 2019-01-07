using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoApi.Models;
using TodoApi.Services;
using TodoApi.IServices;
using AutoMapper;

namespace TodoApi
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
            //services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("TodoList"));
            services.AddDbContext<TodoContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("TodoContext")));
            
            services.AddAutoMapper();
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddCors();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITodoListService, TodoListService>();
            services.AddScoped<ITodoItemService, TodoItemService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(builder => builder.AllowAnyOrigin());

            app.UseMvc();
        }
    }
}
