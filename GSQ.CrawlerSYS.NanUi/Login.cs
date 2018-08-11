using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GSQ.CrawlerSYS.NanUi
{
    using Chromium;
    using Chromium.Remote;
    using GSQ.CrawlerSYS.CommonLib;
    using GSQ.CrawlerSYS.NanUi.Base;
    using NetDimension.NanUI;
    using Newtonsoft.Json.Linq;



    public partial class Login : BaseForm
    {
        private static Registered registered;

        public Login()
            : base(AnimationType.Center, "wwwroot/Pages/Login.html", false)
        {

            InitializeComponent();
            base.ContextMenuHandler.OnBeforeContextMenu += (sender, e_) => e_.Model.Clear();  //禁用右键
            this.MinimumSize = new Size(475, 335);
            this.MaximumSize = new Size(475, 335);
            //this.FormBorderStyle = FormBorderStyle.FixedToolWindow;

            //注册显示注册界面事件到JS
            base.GlobalObject.AddFunction("showRegistered").Execute += (_, args) =>
            {
                this.RequireUIThread(() =>
                {
                    if (registered == null || registered.IsDisposed)
                    {
                        registered = new Registered();
                        registered.Show();
                    }
                    else
                    {
                        //if (registered.WindowState == FormWindowState.Minimized)
                        //    registered.WindowState = FormWindowState.Normal;
                        registered.Activate();
                    }
                    //Mian main = new Mian();
                    //main.Show();
                });
            };

            //注册关闭事件到JS
            base.GlobalObject.AddFunction("closeForm").Execute += (_, args) =>
            {
                this.RequireUIThread(() =>
                {
                    this.Close();
                    this.Dispose();
                    Application.Exit();
                });
            };

            //注册最小化事件到JS
            base.GlobalObject.AddFunction("minForm").Execute += (_, args) =>
            this.RequireUIThread(() =>
            this.WindowState = FormWindowState.Minimized);

            //注册显示主界面窗体到JS
            base.GlobalObject.AddFunction("viewMain").Execute += (_, args) =>
            {
                this.RequireUIThread(() =>
                {
                    //Application.Run(new Main());
                    this.Close();
                    this.Dispose();
                    //this.DialogResult = DialogResult.OK;
                    Mian main = new Mian();
                    main.Show();

                    //this.Close();
                });
            };
            var myObject = GlobalObject.AddObject("my");
            var nameProp = myObject.AddDynamicProperty("name");
            nameProp.PropertyGet += (prop, args) =>
            {
                args.Retval = CfrV8Value.CreateString("NanUI");
                args.SetReturnValue(true);
            };
            nameProp.PropertySet += (prop, args) =>
            {
                var value = args.Value;
                args.SetReturnValue(true);
            };


            /*登录*/
            var FuncLogin = myObject.AddFunction("Func_login");
            FuncLogin.Execute += (func, args) =>
            {
                var stringArgument = args.Arguments.FirstOrDefault(p => p.IsString);
                if (stringArgument != null)
                {
                    var str = stringArgument.StringValue;
                    JObject model = JObject.Parse(str);
                    var name = model["Name"].ToString().ToLower();
                    var pass = model["Pass"].ToString().ToLower();
                    object result = null;
                    if (name == "admin" && pass == "111111")
                        result = new { isSuccess = true, msg = "登录成功" };
                    else
                        result = new { isSuccess = false, msg = "账号密码错误" };

                    var resultStr = CfrV8Value.CreateString(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                    args.SetReturnValue(resultStr);
                }
            };

            //google调试器
            base.LoadHandler.OnLoadStart += (sender, e) =>
            {
                base.Chromium.ShowDevTools();
            };


            //注册验证码到JS
            base.GlobalObject.AddFunction("ClickValidateCode").Execute += (_, args) =>
            {

                this.RequireUIThread(() =>
                {
                    var itemValidateCode = new CommonLib.ValidateCode();
                    string code = itemValidateCode.CreateValidateCode(4);
                  //  Session["CheckCode"] = code;
                    byte[] bytes = itemValidateCode.CreateValidateGraphic(code);

                   // return File(bytes, @"image/jpg");
                });
            };


        }



    }
}
