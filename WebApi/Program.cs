using Com.Ctrip.Framework.Apollo;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace WebApi
{
    public class Program
    {
        /// <summary>
        /// ��ں���
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// ����CreateHostBuilder
        /// </summary>
        /// <param name="args">����</param>
        /// <returns>IHostBuilder</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // ���Apollo����
                    webBuilder.ConfigureAppConfiguration(
                        builder => builder.AddApollo(builder.Build().GetSection("apollo")).AddDefault())
                   .UseStartup<Startup>();
                });
    }
}
