using System;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoPosterEmulation;
using System.IO;
using NLog;
using NLog.Config;
using NLog.Targets;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.Threading;
using System.Net;

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
            project.SendInfoToLog("Это сборка из ветки YandexRegistration!", true);
            project.SendInfoToLog("Считываем входные настройки", true);
            new InputSettings(instance, project).InitializationInputValue();
            StartMethod startMethod = new StartMethod(instance, project);

            try
            {
                startMethod.YandexRegistration();
            }
            catch (Exception ex)
            {
                project.SendErrorToLog("Не смогли зарегестрировать аккаунт: " + ex.Message);
            }

            return executionResult;
        }
    }
}