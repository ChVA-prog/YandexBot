using System;
using ZennoLab.InterfacesLibrary.ProjectModel;
using System.Linq;
using System.Collections.Generic;

namespace DataBaseProfileAndProxy
{
     class DataBaseProfileAndProxyValue
     {
        readonly IZennoPosterProjectModel Project;

        protected int CountFreeProfileInDB { set; get; }       
        protected string PathToFolderProfile { get; set; }
        protected int CountSessionLimit { get; set; }
        protected int CountSession { get; set; }
        protected int CountSessionDay { get; set; }
        protected string PathToProfile { get; set; }
        private static string ProxyListInput { get; set; }
        public static List<string> MyProxyList { get; set; }
        public DataBaseProfileAndProxyValue(IZennoPosterProjectModel project)
        {
            this.Project = project;

            CountFreeProfileInDB = Convert.ToInt32(Project.Variables["set_CountFreeProfileInDB"].Value);
            DB.PathToDB = Project.Variables["set_PathToDB"].Value;
            PathToFolderProfile = Project.Variables["set_PathToFolderProfile"].Value;
            CountSessionLimit = Convert.ToInt32(Project.Variables["set_CountSessionDayLimit"].Value);
            ProxyListInput = Project.Variables["set_Proxy"].Value;
            MyProxyList = ProxyListInput.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
        }
     }
}
