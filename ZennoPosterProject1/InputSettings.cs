using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoLab.CommandCenter;
using ZennoPosterDataBaseAndProfile;
using ZennoPosterEmulation;
using ZennoPosterProxy;
using ZennoPosterYandexWalk;
using ZennoPosterSiteWalk;

namespace ZennoPosterProject1
{
    class InputSettings
    {
        readonly IZennoPosterProjectModel project;
        readonly Instance instance;
        public InputSettings(Instance instance, IZennoPosterProjectModel project)
        {
            this.instance = instance;
            this.project = project;
        }

        public void InitializationInputValue()
        {
            new DataBaseAndProfileValue(instance, project);
            new EmulationValue(instance, project);
            new ProxyValue(instance, project);
            new YandexWalkValue(instance, project);
            new SiteWalkValue(instance, project);
        }//Считывание входных настроек
    }
}
