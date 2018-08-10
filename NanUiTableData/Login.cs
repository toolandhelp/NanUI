using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NanUiTableData
{
    using Chromium;
    using Chromium.Remote;
    using NanUiTableData.Base;
    using NetDimension.NanUI;
    using Newtonsoft.Json.Linq;

    public partial class Login : BaseForm
    {
        public Login()
            : base(AnimationType.Center, "wwwroot/Pages/Login.html", false)
        {
            InitializeComponent();
          /*  base.ContextMenuHandler.OnBeforeContextMenu += (sender, e_) => e_.Model.Clear(); */ //禁用右键
            this.MinimumSize = new Size(475, 335);
            this.MaximumSize = new Size(475, 335);
            //this.FormBorderStyle = FormBorderStyle.FixedToolWindow;

            //注册显示注册界面事件到JS
            base.GlobalObject.AddFunction("showRegistered").Execute += (_, args) =>
            {
                this.RequireUIThread(() =>
                {
                    Mian main = new Mian();
                    main.Show();
                });
            };



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
                    var name = model["Name"].ToString();
                    var pass = model["Pass"].ToString();
                    object result = null;
                    if (name == "Admin" && pass == "Admin123+.")
                        result = new{isSuccess = true,msg = "登录成功" };
                    else 
                        result = new { isSuccess = false, msg = "账号密码错误" };
                    
                    var resultStr =  CfrV8Value.CreateString(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                    args.SetReturnValue(resultStr);
                }
            };


            base.LoadHandler.OnLoadStart += (sender, e) =>
            {
                base.Chromium.ShowDevTools();
            };
        }



    }
}
