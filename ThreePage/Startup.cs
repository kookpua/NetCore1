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
using Three;
using Three.Services;

namespace ThreePage
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
            //mvc
            //services.AddControllersWithViews();

            //razor page
            services.AddRazorPages();

            services.AddSingleton<IClock, ChinaClock>();
            services.AddSingleton<IDepartmentService, DepartmentService>();
            services.AddSingleton<IEmployeeService, EmployeeService>();
            services.Configure<ThreeOptions>(_configuration.GetSection("Three"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //��̬�ļ�
            app.UseStaticFiles();

            //ǿ���û�ʹ��https
            app.UseHttpsRedirection();

            //�����֤
            app.UseAuthentication();

            //·���м��
            app.UseRouting();


            //�˵��м��
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            }); 
        }
    }
}
