using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoLab.CommandCenter;


namespace ZennoPosterSiteWalk
{
    class SiteWalk
    {
        readonly IZennoPosterProjectModel Project;
        readonly Instance instance;

        public SiteWalk(Instance instance, IZennoPosterProjectModel project)
        {
            this.instance = instance;
            this.Project = project;           
        }


    }
}
