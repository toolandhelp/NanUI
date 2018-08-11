using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirstNanUIApplication
{
    using Chromium;
    using GSQ.CrawlerSYS.NanUi;
    using NetDimension.NanUI;
    using System.Reflection;

    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Bootstrap.BeforeCefInitialize = (CefInitArgs) =>
            {
                //禁用日志
                CefInitArgs.Settings.LogSeverity = Chromium.CfxLogSeverity.Disable;
                //指定中文为当前CEF环境的默认语言
                CefInitArgs.Settings.AcceptLanguageList = "zh-CN";
                CefInitArgs.Settings.Locale = "zh-CN";
            };
            Bootstrap.BeforeCefCommandLineProcessing = (CefCmdArgs) =>
            {
                //在启动参数中添加disable-web-security开关，禁用跨域安全检测
                CefCmdArgs.CommandLine.AppendSwitch("disable-web-security");
            };
            //指定CEF架构和文件目录结构，并初始化CEF
            if (Bootstrap.Load(PlatformArch.Auto, System.IO.Path.Combine(Application.StartupPath, "fx"), System.IO.Path.Combine(Application.StartupPath, "fx\\Resources"), System.IO.Path.Combine(Application.StartupPath, "fx\\Resources\\locales")))
            {
                //注册嵌入资源，并为指定资源指定一个假的域名my.resource.local
                Bootstrap.RegisterAssemblyResources(Assembly.GetExecutingAssembly(), "my.resource.local");
                Login a = new Login();
                a.Show();
                Application.Run();
            }
            else
            {
                MessageBox.Show("打开失败, 请下载CEF");
            }
        }
    }
}
