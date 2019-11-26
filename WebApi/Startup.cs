using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Dtos;
using Entity;
using Fairhr.Logs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace WebApi
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
            //Apollo����
            //JsonSerializer .net core 3.0�Դ����л������
            var config = JsonSerializer.Deserialize<Config>(Configuration.GetSection("AppSettings").Value);
            services.ConfigureJsonValue<Config>(Configuration.GetSection("AppSettings"));
            // ��ӷ�����־ƽ̨
            services.AddFairhrLogs(options =>
            {
                options.Key = config.LogsAppkey;
                options.ServerUrl = config.LogsService;
            });

            //��ӽӿڰ汾���� http://doc.fairhr.com/2019/10/30/%E8%A7%84%E8%8C%83/RESTful-API-%E8%A7%84%E8%8C%83/
            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;// API����Ӧ��ͷ�з��ذ汾
                o.AssumeDefaultVersionWhenUnspecified = true; // API����Ӧ��ͷ�з��ذ汾Ǩ��
                o.DefaultApiVersion = new ApiVersion(1, 0); // Ĭ�ϰ汾��������Ĭ�ϰ汾����ͷ���Բ����x-api-version��
                o.ApiVersionReader = new HeaderApiVersionReader("x-api-version");// ��������ͷ��Key;�����Ǵ���x-api-version:1.0
            });

            // �����Ŀ��Ҫ�������������ƽӿڣ�������
            //services.AddFarihrGetWay(options =>
            //{
            //    options.AppKey = "AppKey";
            //    options.AppSecret = "AppSecret";
            //    // api���ص�ַ
            //    options.Host = "api���ص�ַ";
            //    // ���û�������
            //    options.Environment = "TEST
            //});

            //���ݿ�DbContext
            services.AddDbContext<TestDbContext>(options =>
            {
                options.UseMySql(config.ConnectionStrings, b => b.MigrationsAssembly(typeof(Startup).Assembly.GetName().Name));
            });

            ///����ע��
            services.AddDependency();
            services.AddControllers();

            // Swagger���� https://docs.microsoft.com/zh-cn/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-3.0&tabs=visual-studio
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "�ӿ��ĵ�", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
            }
            //���swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "����ƽ̨");
                c.RoutePrefix = string.Empty;
            });
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            // �����־ƽ̨
            app.UseFairhrLogs();
        }
    }
}
