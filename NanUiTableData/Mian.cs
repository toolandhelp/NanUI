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
    using Chromium.Remote;
  //  using Crp.Tools.FileHelper;
    using NanUiTableData.Base;
    using NetDimension.NanUI;
    using System.IO;

    public partial class Mian : BaseForm
    {
        public Mian()
            :base(AnimationType.Center, "wwwroot/Pages/Main.html", false)
        {
            InitializeComponent();
            base.ContextMenuHandler.OnBeforeContextMenu += (sender, e_) => e_.Model.Clear();  //禁用右键
            this.MinimumSize = new Size(1000, 700);
            this.MaximumSize = new Size(1000, 700);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;

            //注册显示注册界面事件到JS
            base.GlobalObject.AddFunction("showRegistered").Execute += (_, args) =>
            {
                this.RequireUIThread(() =>
                {
                });
            };

            //注册显示主界    面窗体到JS
            base.GlobalObject.AddFunction("viewMain").Execute += (_, args) =>
            {
                this.RequireUIThread(() =>
                {
                    //Application.Run(new Main());
                    //this.Close();
                    //this.Dispose();
                    //this.DialogResult = DialogResult.OK;
                    //new Main(this).Show();
                    //this.Close();
                });
            };
            var FuncExport = GlobalObject.AddFunction("Func_Export");
            FuncExport.Execute += (func, args) =>
            {
                var stringArgument = args.Arguments.FirstOrDefault(p => p.IsString);
                if (stringArgument != null)
                {
                    var str = stringArgument.StringValue;
                    str = str.Replace("ind","序号");
                    str = str.Replace("one","数字");
                    str = str.Replace("two","排序");
                    str = str.Replace("three","正负");
                    ExportList tables = Newtonsoft.Json.JsonConvert.DeserializeObject<ExportList>(str);
                   
                    //导出五个表
                    var saveDir = System.Environment.CurrentDirectory;

                    string currDate = DateTime.Now.ToString("yyyy-MM-dd");
                    string currTime = DateTime.Now.ToString("HH-mm");
                    if (!Directory.Exists(saveDir + "\\数据表格")) { Directory.CreateDirectory(saveDir + "\\数据表格"); }
                    if (!Directory.Exists(saveDir + "\\数据表格\\"+ currDate)) { Directory.CreateDirectory(saveDir + "\\数据表格\\"+ currDate); }
                    try
                    {
                        var path = saveDir + "\\数据表格\\" + currDate;
                        //if (tables.table1 != null && tables.table1.Rows.Count > 0) FileHelper.ExportExcel(path + "\\千位" + currTime + "正比率" + GetPrecent(tables.table1) + ".xls", tables.table1, "");
                        //if (tables.table2 != null && tables.table2.Rows.Count > 0) FileHelper.ExportExcel(path + "\\百位" + currTime + "正比率" + GetPrecent(tables.table2) + ".xls", tables.table2, "");
                        //if (tables.table3 != null && tables.table3.Rows.Count > 0) FileHelper.ExportExcel(path + "\\十位" + currTime + "正比率" + GetPrecent(tables.table3) + ".xls", tables.table3, "");
                        //if (tables.table4 != null && tables.table4.Rows.Count > 0) FileHelper.ExportExcel(path + "\\个位" + currTime + "正比率" + GetPrecent(tables.table4) + ".xls", tables.table4, "");
                        //if (tables.table5 != null && tables.table5.Rows.Count > 0) FileHelper.ExportExcel(path + "\\球五" + currTime + "正比率" + GetPrecent(tables.table5) + ".xls", tables.table5, "");
                        var resultStr = CfrV8Value.CreateString(Newtonsoft.Json.JsonConvert.SerializeObject(true));
                        args.SetReturnValue(resultStr);
                    }
                    catch {
                        var resultStr = CfrV8Value.CreateString(Newtonsoft.Json.JsonConvert.SerializeObject(false));
                        args.SetReturnValue(resultStr);
                    }
                }
            };

            //注册最小化事件到JS
            base.GlobalObject.AddFunction("minForm").Execute += (_, args) => this.RequireUIThread(() => this.WindowState = FormWindowState.Minimized);

            //注册最大化事件到JS
            base.GlobalObject.AddFunction("maxForm").Execute += (_, args) => this.RequireUIThread(() => this.WindowState = FormWindowState.Maximized);

            //注册默认大小事件到JS
            base.GlobalObject.AddFunction("normalForm").Execute += (_, args) => this.RequireUIThread(() => this.WindowState = FormWindowState.Normal);

            //注册关闭窗体事件到JS
            base.GlobalObject.AddFunction("closeForm").Execute += (_, args) =>
            {
                this.RequireUIThread(() =>
                {
                    this.Close();
                    this.Dispose();
                    Application.Exit();
                });
            };
            this.Visible = true;

            //#if DEBUG
            //            base.LoadHandler.OnLoadStart += (sender, e) =>
            //            {
            //                base.Chromium.ShowDevTools();
            //            };
            //#endif
        }

        public class ExportList{

            public DataTable table1 { get; set; }
            public DataTable table2 { get; set; }
            public DataTable table3 { get; set; }
            public DataTable table4 { get; set; }
            public DataTable table5 { get; set; }
        }


        public string GetPrecent(DataTable dt) {
            if (dt != null && dt.Rows.Count > 0)
            {
                var zCount = 0;
                foreach (DataRow row in dt.Rows)
                {
                    if (row["正负"].ToString() == "1")
                    {
                        zCount++;
                    }
                }
                var res = Convert.ToDouble(zCount) / Convert.ToDouble(dt.Rows.Count);
                string result = string.Format("{0:0.00%}", res);//得到5.88%
                return result;
            }
            else {
                return "";
            }
        }
    }
}
