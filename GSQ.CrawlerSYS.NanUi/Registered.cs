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
    using GSQ.CrawlerSYS.NanUi.Base;
    using NetDimension.NanUI;
    using Newtonsoft.Json.Linq;
    public partial class Registered : BaseForm
    {
        public Registered()
             : base(AnimationType.Center, "wwwroot/Pages/Registered.html", false)
        {
            base.ContextMenuHandler.OnBeforeContextMenu += (sender, e_) => e_.Model.Clear();  //禁用右键
            this.MinimumSize = new Size(475, 700);
            this.MaximumSize = new Size(475, 700);

            InitializeComponent();



            //注册关闭事件到JS
            base.GlobalObject.AddFunction("closeForm").Execute += (_, args) =>
            {
                this.RequireUIThread(() =>
                {
                    //Application.Run(new Main());
                    MessageBox.Show("确定取消注册?");

                    this.DialogResult = DialogResult.OK;
                    {
                        this.Close();
                        this.Dispose();
                    }
                    //this.Close();
                });
            };

            //注册最小化事件到JS
            //base.GlobalObject.AddFunction("minForm").Execute += (_, args) =>
            //this.RequireUIThread(() =>
            //this.WindowState = FormWindowState.Minimized);

        }
    }
}
