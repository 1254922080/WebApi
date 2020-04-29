using Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Renci.SshNet;
using System.IO;
using System.Linq;
using System.Text.Json;
namespace Utils
{
    public class FtpHelper
    {
        private static IConfiguration configuration;
        private static string ftpAddress;
        private static string ftpUser;
        private static string ftpPwd;
        private static int ftpPort;

        public FtpHelper(IConfiguration theconfiguration)
        {
            configuration = theconfiguration;
            var config = JsonSerializer.Deserialize<Config>(configuration.GetSection("AppSetting").Value);
            ftpAddress = config.FtpAddress;
            ftpUser = config.FtpUser;
            ftpPwd = config.FtpPwd;
            ftpPort = config.FtpPort;
        }

        /// <summary>
        /// 上传文件到ftp服务器上
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="dateStr">文件时间戳</param>
        /// <param name="customFileName">追加的文件名</param>
        /// <returns>bool</returns>
        public static bool UploadFile(IFormFile file, long dateStr, string customFileName = null)
        {
            bool result = false;
            using (var client = new SftpClient(ftpAddress, ftpPort, ftpUser, ftpPwd)) //创建连接对象，ftpAddress是ip地址如： 47.100.11.12  第二个参数是端口号
            {
                client.Connect(); //连接

                MemoryStream fs = new MemoryStream();
                file.CopyTo(fs);

                client.BufferSize = 4 * 1024 * 1024;
                fs.Seek(0, SeekOrigin.Begin);

                var filename = dateStr.ToString() + "_" + file.FileName;
                if (!string.IsNullOrEmpty(customFileName))
                {
                    //获取文件后缀
                    var suffix = file.FileName.Split('.').Last();
                    filename = $"{customFileName}-{dateStr.ToString()}.{suffix}";
                }

                client.UploadFile(fs, "/liuxixi/" + filename);
                fs.Dispose();
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 下载附件
        /// </summary>
        /// <param name="fileName">附件名</param>
        /// <returns>byte[]</returns>
        public static byte[] DownloadFile(string fileName)
        {
            byte[] buffer = new byte[2 * 1024 * 1024];
            using (var client = new SftpClient(ftpAddress, ftpPort, ftpUser, ftpPwd)) //创建连接对象
            {
                client.Connect(); //连接

                buffer = client.ReadAllBytes("/liuxixi/" + fileName);
            }

            return buffer;
        }
    }
}
