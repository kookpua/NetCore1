using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Three.Services;

namespace Three
{
    public class Startup
    {
        public IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc();
            //services.AddControllers();
            services.AddControllersWithViews();
            //services.AddRazorPages();
            services.AddSingleton<IClock, ChinaClock>();
            //services.AddSingleton<IClock, UtcClock>();

            services.AddSingleton<IDepartmentService, DepartmentService>();
            services.AddSingleton<IEmployeeService, EmployeeService>();


            //var three = _configuration["Three:BoldDepartmentEmployeeCountThreshold"];

            services.Configure<ThreeOptions>(_configuration.GetSection("Three"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //静态文件
            app.UseStaticFiles();

            //强制用户使用https
            app.UseHttpsRedirection();

            //身份认证
            app.UseAuthentication();

            //路由中间件
            app.UseRouting();


            //端点中间件
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
               
                
                //加属性的用这个
               // endpoints.MapControllers();

                //这个路由表
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Department}/{action=Index}/{id?}");
            });
        }
    }
}
