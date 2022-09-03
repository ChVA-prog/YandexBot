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
using ZennoPosterYandexParseImage;

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
            project.SendInfoToLog("Это сборка из ветки YandexParse!", true);
            project.SendInfoToLog("Считываем входные настройки", true);
            new InputSettings(instance, project).InitializationInputValue();
            StartMethod startMethod = new StartMethod(instance, project);
            YandexParseImage yandexParseImage = new YandexParseImage(instance, project);
            ParseImageSettings parseImageSettings = new ParseImageSettings(instance, project);
            yandexParseImage.StartParse();

            //try
            //{

            //    yandexParseImage.StartParse();
            //}
            //catch (Exception ex)
            //{

            //}

            return executionResult;
        }
    }
}