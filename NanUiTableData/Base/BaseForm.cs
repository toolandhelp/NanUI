using NetDimension.NanUI;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Linq;

namespace NanUiTableData.Base
{
    /// <summary>
    /// 窗体父类
    /// </summary>
    public class BaseForm : Formium
    {
        #region 窗体效果属性
        [DllImport("user32")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);
        private const int AW_HOR_POSITIVE = 0x0001;
        private const int AW_HOR_NEGATIVE = 0x0002;
        private const int AW_VER_POSITIVE = 0x0004;
        private const int AW_VER_NEGATIVE = 0x0008;
        private const int AW_CENTER = 0x0010;
        private const int AW_HIDE = 0x10000;
        private const int AW_ACTIVATE = 0x20000;
        private const int AW_SLIDE = 0x40000;
        private const int AW_BLEND = 0x80000;
        #endregion

        /// <summary>
        /// 窗体显示时候的动画类型, 默认为   从中间向四周展开效果
        /// </summary>
        private readonly AnimationType Animation = AnimationType.Center;

        /// <summary>
        /// 获取登录用户的信息
        /// </summary>
        public UserInfo LoginUser { get; private set; }

        /// <summary>
        /// 窗体父类无参构造函数
        /// </summary>
        public BaseForm()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        /// <summary>
        /// 窗体父类构造函数
        /// </summary>
        /// <param name="animationType">显示的动画效果, 默认为   从中间向四周展开效果</param>
        public BaseForm(AnimationType animationType)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Animation = animationType;
        }

        /// <summary>
        /// 窗体父类构造函数
        /// </summary>
        /// <param name="animationType">显示的动画效果, 默认为   从中间向四周展开效果</param>
        /// <param name="initialUrl">要加载的HTML地址</param>
        /// <param name="isNetwork">是不是网络地址, 默认不是, 比如initialUrl = https://www.baidu.com, 则需要把该参数设置为true, 否则页面无法加载</param>
        public BaseForm(AnimationType animationType, string initialUrl, bool isNetwork = false)
            : base(isNetwork ? initialUrl : $"http://my.resource.local/{initialUrl}")
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Animation = animationType;
            base.GlobalObject.AddFunction("saveLoginInfo").Execute += (_, args) =>
            {
                var result = this.SaveLoginInfo(args.Arguments.FirstOrDefault(f => f.IsString)?.StringValue);
                args.SetReturnValue(result);
            };
        }

        /// <summary>
        /// 窗体父类构造函数
        /// </summary>
        /// <param name="animationType">显示的动画效果, 默认为   从中间向四周展开效果</param>
        /// <param name="initialUrl">要加载的HTML地址</param>
        /// <param name="isNetwork">是不是网络地址, 默认不是, 比如initialUrl = https://www.baidu.com, 则需要把该参数设置为true, 否则页面无法加载</param>
        /// <param name="enableModernForm">是否强制使用winform渲染, 默认不是,如果传false, 则会显示winform边框</param>
        public BaseForm(AnimationType animationType, string initialUrl, bool isNetwork = false, bool enableModernForm = true)
            : base(isNetwork ? initialUrl : $"http://my.resource.local/{initialUrl}", enableModernForm)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Animation = animationType;
            base.GlobalObject.AddFunction("saveLoginInfo").Execute += (_, args) =>
            {
                var result = this.SaveLoginInfo(args.Arguments.FirstOrDefault(f => f.IsString)?.StringValue);
                args.SetReturnValue(result);
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.SetAnimation();
        }

        private void SetAnimation()
        {
            switch (this.Animation)
            {
                case AnimationType.Ordinary:
                    AnimateWindow(Handle, 300, AW_ACTIVATE);
                    break;
                case AnimationType.LeftToRight:
                    AnimateWindow(Handle, 300, AW_HOR_POSITIVE);
                    break;
                case AnimationType.RightToLeft:
                    AnimateWindow(Handle, 300, AW_HOR_NEGATIVE);
                    break;
                case AnimationType.TopToBottom:
                    AnimateWindow(Handle, 300, AW_VER_POSITIVE);
                    break;
                case AnimationType.BottomToTop:
                    AnimateWindow(Handle, 300, AW_VER_NEGATIVE);
                    break;
                case AnimationType.Gradient:
                    AnimateWindow(Handle, 300, AW_BLEND);
                    break;
                case AnimationType.Center:
                    AnimateWindow(Handle, 300, AW_CENTER);
                    break;
                case AnimationType.Slide_LeftTop:
                    AnimateWindow(Handle, 300, AW_SLIDE | AW_HOR_POSITIVE | AW_VER_POSITIVE);
                    break;
                case AnimationType.Slide_LeftBottom:
                    AnimateWindow(Handle, 300, AW_SLIDE | AW_HOR_POSITIVE | AW_VER_NEGATIVE);
                    break;
                case AnimationType.Slide_RightTop:
                    AnimateWindow(Handle, 300, AW_SLIDE | AW_HOR_NEGATIVE | AW_VER_POSITIVE);
                    break;
                case AnimationType.Slide_RightBottom:
                    AnimateWindow(Handle, 300, AW_SLIDE | AW_HOR_NEGATIVE | AW_VER_NEGATIVE);
                    break;
                default:
                    AnimateWindow(Handle, 300, AW_CENTER);
                    break;
            }
        }

        /// <summary>
        /// 保存登录用户的信息
        /// </summary>
        /// <param name="userInfoJson">用户信息JSON字符串</param>
        /// <returns></returns>
        public bool SaveLoginInfo(string userInfoJson)
        {
            userInfoJson = (userInfoJson ?? string.Empty).Trim();
            if (string.IsNullOrEmpty(userInfoJson))
            {
                return false;
            }
            try
            {
                this.LoginUser = JsonConvert.DeserializeObject<UserInfo>(userInfoJson);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}