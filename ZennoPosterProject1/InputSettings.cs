using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoLab.CommandCenter;
using DataBaseProfileAndProxy;
using ZennoPosterEmulation;
using ZennoPosterYandexWalk;
using ZennoPosterYandexRegistration;
using ZennoPosterYandexRegistrationSmsServiceSmsHubOrg;
using ZennoPosterYandexParseImage;

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
            project.SendInfoToLog("Инициализируем входные настройки");
            Program.logger.Debug("Начинаем инициализацию входных настроек проекта.");
            new DataBaseProfileAndProxyValue(project);
            new EmulationValue(project);
            new YandexWalkValue(project);
            new YandexRegistrationValue(project);
            new SmshubValue(project);
            new YandexParseImageValue(project);
            Program.logger.Debug("Закончили инициализацию входных настроек проекта.");
        }//Считывание входных настроек
    }
}