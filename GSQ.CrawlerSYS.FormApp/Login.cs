using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GSQ.CrawlerSYS.FormApp
{
    using Chromium;
    using Chromium.Remote;
    using NanUiTableData.Base;
    using NetDimension.NanUI;
    using Newtonsoft.Json.Linq;
    public partial class Login : BaseForm
    {
        public Login() : base(AnimationType.Center, "wwwroot/Pages/Login.html", false)
        {
            InitializeComponent();
            base.ContextMenuHandler.OnBeforeContextMenu += (sender, e_) => e_.Model.Clear();  //禁用右键
            this.MinimumSize = new Size(475, 335);
            this.MaximumSize = new Size(475, 335);


        }
    }
}
