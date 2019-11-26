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
            //Apollo配置
            //JsonSerializer .net core 3.0自带序列化的类库
            var config = JsonSerializer.Deserialize<Config>(Configuration.GetSection("AppSettings").Value);
            services.ConfigureJsonValue<Config>(Configuration.GetSection("AppSettings"));
            // 添加泛亚日志平台
            services.AddFairhrLogs(options =>
            {
                options.Key = config.LogsAppkey;
                options.ServerUrl = config.LogsService;
            });

            //添加接口版本管理 http://doc.fairhr.com/2019/10/30/%E8%A7%84%E8%8C%83/RESTful-API-%E8%A7%84%E8%8C%83/
            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;// API在响应标头中返回版本
                o.AssumeDefaultVersionWhenUnspecified = true; // API在响应标头中返回版本迁移
                o.DefaultApiVersion = new ApiVersion(1, 0); // 默认版本（设置了默认版本请求头可以不添加x-api-version）
                o.ApiVersionReader = new HeaderApiVersionReader("x-api-version");// 设置请求头的Key;请求是带上x-api-version:1.0
            });

            // 如果项目需要调用其它阿里云接口，添加这个
            //services.AddFarihrGetWay(options =>
            //{
            //    options.AppKey = "AppKey";
            //    options.AppSecret = "AppSecret";
            //    // api网关地址
            //    options.Host = "api网关地址";
            //    // 配置环境变量
            //    options.Environment = "TEST
            //});

            //数据库DbContext
            services.AddDbContext<TestDbContext>(options =>
            {
                options.UseMySql(config.ConnectionStrings, b => b.MigrationsAssembly(typeof(Startup).Assembly.GetName().Name));
            });

            ///依赖注入
            services.AddDependency();
            services.AddControllers();

            // Swagger配置 https://docs.microsoft.com/zh-cn/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-3.0&tabs=visual-studio
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "接口文档", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
            }
            //添加swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "公用平台");
                c.RoutePrefix = string.Empty;
            });
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            // 添加日志平台
            app.UseFairhrLogs();
        }
    }
}
