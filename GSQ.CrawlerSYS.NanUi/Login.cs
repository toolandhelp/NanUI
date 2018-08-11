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
    using GSQ.CrawlerSYS.BLL;
    using GSQ.CrawlerSYS.CommonLib;
    using GSQ.CrawlerSYS.Model;
    using GSQ.CrawlerSYS.NanUi.Base;
    using NetDimension.NanUI;
    using Newtonsoft.Json.Linq;

    //学习地址：https://www.cnblogs.com/MVC1013/p/8860990.html

    public partial class Login : BaseForm
    {
        private static Registered registered;
        private static  string Vcode = "";
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
                args.Retval = CfrV8Value.CreateString("MyLogin");
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

                    ReturnMessageModel returnMessage = LoginFun(model);


                    var resultStr = CfrV8Value.CreateString(Newtonsoft.Json.JsonConvert.SerializeObject(returnMessage));
                    args.SetReturnValue(resultStr);
                }
            };



            //注册验证码到JS
            base.GlobalObject.AddFunction("ClickValidateCode").Execute += (_, args) =>
            {
                string base64 = BaseImgCode();
                ExecuteJavascript("ImgCode('data:image/jpg;base64," + base64 + "')");
            };

            //加载获取验证码
            LoadHandler.OnLoadEnd += LoadValidaeCode;


            //google调试器
            base.LoadHandler.OnLoadStart += (sender, e) =>
            {
                base.Chromium.ShowDevTools();
            };

        }

        private ReturnMessageModel LoginFun(JObject model)
        {
            var name = model["Name"].ToString().ToLower();
            var pass = model["Pass"].ToString().ToLower();
            var code = model["Code"].ToString().ToLower();

            ReturnMessageModel returnMessage = new ReturnMessageModel();

            if (string.IsNullOrEmpty(code))
            {
                returnMessage.ErrorType = 3;
                returnMessage.MessageContent = "验证码不能为空";
                return returnMessage;
            }

            if (code != Vcode)
            {
                returnMessage.ErrorType = 2;
                returnMessage.MessageContent = "验证码错误";
                return returnMessage;
            }
            pass = CommonLib.HashEncrypt.BgPassWord(pass);
            BLL_User userBll = new BLL_User();
            var UserModel = userBll.LoginUsers(name,pass);

            if (UserModel!=null)
            {
                //设置个人信息
                //个人信息类  拓展

                returnMessage.IsSuccess = true;
                returnMessage.ErrorType = 1;
                returnMessage.MessageContent = "登录成功";
                return returnMessage;
            }
            else
            {
                returnMessage.ErrorType = 0; //可有可无 构造方法已赋值
                returnMessage.MessageContent = "账号密码错误";
                return returnMessage;
            }
        }

        private void LoadValidaeCode(object sender, Chromium.Event.CfxOnLoadEndEventArgs e)
        {
            string base64 = BaseImgCode();
            ExecuteJavascript("ImgCode('data:image/jpg;base64," + base64 + "')");
            // return File(bytes, @"image/jpg");
        }

        private static string BaseImgCode()
        {
            var itemValidateCode = new CommonLib.ValidateCode();
            string code = itemValidateCode.CreateValidateCode(4);
            //  Session["CheckCode"] = code;
            Vcode = code;
            byte[] bytes = itemValidateCode.CreateValidateGraphic(code);
            string base64 = Convert.ToBase64String(bytes);
            // return File(bytes, @"image/jpg");
            return base64;
        }

    }
}
