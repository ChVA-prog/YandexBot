using System;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoPosterEmulation;
using NLog;
using NLog.Config;

namespace ZennoPosterProject1
{
    /// <summary>
    /// Класс для запуска выполнения скрипта
    /// </summary>
    public class Program : IZennoExternalCode
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Метод для запуска выполнения скрипта
        /// </summary>
        /// <param name="instance">Объект инстанса выделеный для данного скрипта</param>
        /// <param name="project">Объект проекта выделеный для данного скрипта</param>
        /// <returns>Код выполнения скрипта</returns>		
        public int Execute(Instance instance, IZennoPosterProjectModel project)
        {
            int executionResult = 0;

            logger.Trace("trace message");
            logger.Debug("debug message");
            logger.Info("info message");
            logger.Warn("warn message");
            logger.Error("error message");
            logger.Fatal("fatal message");


            //project.SendInfoToLog("Это сборка из ветки FeedCookies!", true);
            //project.SendInfoToLog("Считываем входные настройки", true);
            //new InputSettings(instance, project).InitializationInputValue();

            //StartMethod startMethod = new StartMethod(instance, project);

            //try
            //{
            //    startMethod.FeedingCookies();
            //}
            //catch (Exception ex)
            //{
            //    project.SendErrorToLog("Не смогли нагулять куки: " + ex.Message);
            //}

            return executionResult;           
        }
    }
}