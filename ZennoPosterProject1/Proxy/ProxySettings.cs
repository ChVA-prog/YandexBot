using System;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoPosterEmulation;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace ZennoPosterProxy
{
    class ProxySettings
    {
        readonly Instance instance;
        readonly IZennoPosterProjectModel Project;

        public ProxySettings(Instance instance, IZennoPosterProjectModel project)
        {
            this.instance = instance;
            this.Project = project;            
        }

        public static List<string> MyProxyList = new List<string>();
        public static List<string> MyProxyChangeIpUrlList = new List<string>();
        public void ReadProxyListAndProxyChangeIpUrlList()
        {
            MyProxyList = ProxyValue.ProxyList.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
        }//Добавление прокси и ссылок для смены ip из входных настроек в список     
    }
}
