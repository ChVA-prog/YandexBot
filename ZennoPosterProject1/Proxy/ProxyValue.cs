using System;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoPosterEmulation;
using System.Threading;

namespace ZennoPosterProxy
{
    class ProxyValue
    {
        readonly Instance instance;
        readonly IZennoPosterProjectModel Project;

        public static string ProxyList { get; set; }
        public static string ProxyLine { get; set; }       
        public static string ProxyChangeIpUrl { get; set; }
        public ProxyValue(Instance instance, IZennoPosterProjectModel project)
        {           
            this.instance = instance;
            this.Project = project;

            ProxyList = Project.Variables["set_Proxy"].Value;          
        }
    }    
}
