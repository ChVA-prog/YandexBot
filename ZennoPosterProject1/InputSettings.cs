using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoLab.CommandCenter;
using DataBaseProfileAndProxy;
using ZennoPosterEmulation;
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
            new DataBaseProfileAndProxyValue(project);
            new EmulationValue(project);
            new YandexWalkValue(project);
        }//Считывание входных настроек
    }
}