using System;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoPosterEmulation;
using System.IO;
using NLog;
using NLog.Config;
using NLog.Targets;
using System.Text;

namespace ZennoPosterProject1
{

    /// <summary>
    /// Класс для запуска выполнения скрипта
    /// </summary>
    public class Program : IZennoExternalCode
    {
        public static Logger logger = LogManager.GetLogger(DateTime.Now.ToString("dd.MM.yyyy"));
        /// Метод для запуска выполнения скрипта
        /// </summary>
        /// <param name="instance">Объект инстанса выделеный для данного скрипта</param>
        /// <param name="project">Объект проекта выделеный для данного скрипта</param>
        /// <returns>Код выполнения скрипта</returns>		
        public int Execute(Instance instance, IZennoPosterProjectModel project)
        {
            int executionResult = 0;
            new AdditionalMethods(instance,project).NLogCofig();         
            project.SendInfoToLog("Это сборка из ветки FeedCookies!", true);
            new InputSettings(instance, project).InitializationInputValue();
            try
            {
                new StartMethod(instance, project).FeedingCookies();
            }
            catch (Exception ex)
            {
                project.SendErrorToLog("Не смогли нагулять куки: " + ex.Message);
            }
            Program.logger.Info("Закончили выполнение проекта.");
            project.SendInfoToLog("Закончили выполнение проекта.");
            return executionResult;           
        }
    }
}