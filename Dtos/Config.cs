using System;
using System.Collections.Generic;
using System.Text;

namespace Dtos
{
    /// <summary>
    /// 配置文件
    /// </summary>
    public class Config
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionStrings { get; set; }

        /// <summary>
        /// 日志系统Key
        /// </summary>
        public string LogsAppkey { get; set; }

        /// <summary>
        /// 日志Service
        /// </summary>
        public string LogsService { get; set; }

        /// <summary>
        /// Ftp地址
        /// </summary>
        public string FtpAddress { get; set; }

        /// <summary>
        /// Ftp用户名
        /// </summary>
        public string FtpUser { get; set; }

        /// <summary>
        /// Ftp密码
        /// </summary>
        public string FtpPwd { get; set; }

        /// <summary>
        /// Ftp端口
        /// </summary>
        public int FtpPort { get; set; }
    }
}
