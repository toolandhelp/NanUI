﻿using System;

namespace GSQ.CrawlerSYS.CommonLib
{
    public class Config
    {
        /// <summary>
        /// 系统角色(学生)
        /// </summary>
        public static Guid StudentRoleID
        {
            get
            {
                //string temp = System.Configuration.ConfigurationManager.AppSettings["StudentRoleID"];
                //if (RegexValidate.IsGuid(temp))
                //    return new Guid(temp);
                return new Guid("214d3173-6b0b-43cc-a622-497accdfaeeb");
            }
        }
        /// <summary>
        /// 系统角色(老师)
        /// </summary>
        public static Guid TeacherRoleID
        {
            get
            {
                //string temp = System.Configuration.ConfigurationManager.AppSettings["TeacherRoleID"];
                //if (RegexValidate.IsGuid(temp))
                //    return new Guid(temp);
                return new Guid("bdbbf50a-7f93-4c86-b8d1-6ba9686bb275");
            }
        }
        /// <summary>
        /// AdminID
        /// </summary>
        public static Guid AdminRole
        {
            get
            {
                string temp = System.Configuration.ConfigurationManager.AppSettings["AdminRole"];
                if (RegexValidate.IsGuid(temp))
                    return new Guid(temp);
                return Guid.Empty;
            }
        }

        public static string UserID
        {
            get
            {
                string temp = System.Configuration.ConfigurationManager.AppSettings["UserID"];
                if (temp != null)
                    return temp;
                return temp = "";
            }
        }
        /// <summary>
        /// 版本号
        /// </summary>
        public static readonly string Version = System.Configuration.ConfigurationManager.AppSettings["Version"];
        private static readonly string EntityName = System.Configuration.ConfigurationManager.AppSettings["EntityName"];
        private static readonly string DatabaseServer = System.Configuration.ConfigurationManager.AppSettings["DatabaseServer"];
        private static readonly string DatabaseName = System.Configuration.ConfigurationManager.AppSettings["DatabaseName"];
        private static readonly string DatabaseUid = System.Configuration.ConfigurationManager.AppSettings["DatabaseUid"];
        private static readonly string DatabasePwd = System.Configuration.ConfigurationManager.AppSettings["DatabasePwd"];
        /// <summary>
        /// 默认Ef
        /// </summary>
        /// <param name="isEf"></param>
        /// <returns></returns>
        public static string PlatformConnectionString(bool isEf = true)
        {
            if (isEf)
            {
                return string.Concat("metadata=res://*/",
                    EntityName, ".csdl|res://*/",
                    EntityName, ".ssdl|res://*/",
                    EntityName, ".msl;provider=System.Data.SqlClient;provider connection string='Data Source=",
                    DatabaseServer, ";Initial Catalog=",
                    DatabaseName, ";Persist Security Info=True;User ID=",
                    DatabaseUid, ";Password=",
                    DatabasePwd, ";MultipleActiveResultSets=True'");
                //return System.Configuration.ConfigurationManager.ConnectionStrings["PlatformConnectionEFMSSQL"].ConnectionString;
            }
            else
            {
                return string.Concat("server=", DatabaseServer, ";database=", DatabaseName, ";uid=", DatabaseUid, ";pwd=", DatabasePwd, ";");
                //return System.Configuration.ConfigurationManager.ConnectionStrings["PlatformConnectionMSSQL"].ConnectionString;
            }
        }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public static string DataBaseType
        {
            get
            {
                string dbType = System.Configuration.ConfigurationManager.AppSettings["DatabaseType"];
                return string.IsNullOrEmpty(dbType) ? "MSSQL" : dbType.ToUpper();
            }
        }

        /// <summary>
        /// 公司电话
        /// </summary>
        public static string CsTel
        {
            get
            {
                string sTel = System.Configuration.ConfigurationManager.AppSettings["InitsTel"];
                return string.IsNullOrEmpty(sTel) ? "+86 182 1111 111" : sTel.Trim();
            }
        }

        /// <summary>
        /// 公司地址 
        /// </summary>
        public static string sAdder
        {
            get
            {
                string sAdder = System.Configuration.ConfigurationManager.AppSettings["InitsAdder"];
                return string.IsNullOrEmpty(sAdder) ? "上海市 ， 南京东路 ， 开心花园123弄123号" : sAdder.Trim();
            }
        }

        public static string CInfoD_T
        {
            get
            {
                string CInfoD_T = System.Configuration.ConfigurationManager.AppSettings["InitCInfoD_T"];
                return string.IsNullOrEmpty(CInfoD_T) ? "无" : CInfoD_T.Trim();
            }
        }

        public static string CInfoD
        {
            get
            {
                string CInfoD = System.Configuration.ConfigurationManager.AppSettings["InitCInfoD"];
                return string.IsNullOrEmpty(CInfoD) ? "无" : CInfoD.Trim();
            }
        }

        public static string CompanyName
        {
            get
            {
                string CompanyName = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
                return string.IsNullOrEmpty(CompanyName) ? " " : CompanyName.Trim();
            }
        }

        /// <summary>
        /// 系统初始密码
        /// </summary>
        public static string SystemInitPassword
        {
            get
            {
                string pass = System.Configuration.ConfigurationManager.AppSettings["InitPassword"];
                return string.IsNullOrEmpty(pass) ? "111111" : pass.Trim();
            }
        }

    
        /// <summary>
        /// 得到网站域名
        /// </summary>
        public static string WebDomian
        {
            get
            {
                string webDomian = System.Configuration.ConfigurationManager.AppSettings["WebDomian"];
                return string.IsNullOrEmpty(webDomian) ? "" : webDomian.Trim();
            }
        }
        /// <summary>
        /// 得到系统开放时间配置ID（默认为1）
        /// </summary>
        public static int ResourceDirId
        {
            get
            {
                string configID = System.Configuration.ConfigurationManager.AppSettings["ResourceDirId"];
                return RegexValidate.IsInt(configID) ? int.Parse(configID) : 1;
            }
        }

    }
}
