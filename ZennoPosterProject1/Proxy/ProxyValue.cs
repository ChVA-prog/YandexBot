using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;

namespace ZennoPosterProxy
{
    class ProxyValue
    {
        readonly Instance instance;
        readonly IZennoPosterProjectModel Project;

        protected string ProxyList { get; set; }
        protected string ProxyLine { get; set; }
        protected string ProxyChangeIpUrl { get; set; }
        public ProxyValue(Instance instance, IZennoPosterProjectModel project)
        {           
            this.instance = instance;
            this.Project = project;

            ProxyList = Project.Variables["set_Proxy"].Value;          
        }
    }    
}
