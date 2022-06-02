using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoLab.CommandCenter;

namespace ZennoPosterSiteWalk
{
    class SiteWalkValue
    {
        readonly IZennoPosterProjectModel Project;
        readonly Instance instance;

        public static string UrlListToVisit { get; set; }
        public static string CountSitesToVisit { get; set; }


        public SiteWalkValue(Instance instance, IZennoPosterProjectModel project)
        {
            this.instance = instance;
            this.Project = project;

            UrlListToVisit = Project.Variables["set_UrlListToVisit"].Value;
            CountSitesToVisit = Project.Variables["set_CountSitesToVisit"].Value;
        }
    }

}
