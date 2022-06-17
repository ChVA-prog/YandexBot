using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Resources;
using System.Text;
using ZennoLab.CommandCenter;
using ZennoLab.Emulation;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoLab.InterfacesLibrary.ProjectModel.Enums;
using ZennoLab.InterfacesLibrary;
using ZennoLab.InterfacesLibrary.ProjectModel.Collections;
using ZennoLab.Macros;
using Global.ZennoExtensions;
using ZennoLab.CommandCenter.TouchEvents;
using ZennoLab.CommandCenter.FullEmulation;
using ZennoLab.InterfacesLibrary.Enums;
using ZennoPosterSiteWalk;
using ZennoPosterEmulation;
using ZennoPosterDataBaseAndProfile;
using System.Data.SQLite;
using ZennoPosterProxy;
using ZennoPosterYandexWalk;
using System.Threading;

namespace ZennoPosterProject1
{
    /// <summary>
    /// Класс для запуска выполнения скрипта
    /// </summary>
    public class Program : IZennoExternalCode
    {
        
        /// <summary>
        /// Метод для запуска выполнения скрипта
        /// </summary>
        /// <param name="instance">Объект инстанса выделеный для данного скрипта</param>
        /// <param name="project">Объект проекта выделеный для данного скрипта</param>
        /// <returns>Код выполнения скрипта</returns>		
        public int Execute(Instance instance, IZennoPosterProjectModel project)
        {
            int executionResult = 0;
            project.SendInfoToLog("Это сборка из ветки FeedCookies", true);
            project.SendInfoToLog("Считываем входные настройки", true);
            new InputSettings(instance, project).InitializationInputValue();

            project.SendInfoToLog("Запускаем нагуливание кук.", true);
            new StartMethod(instance, project).FeedingCookies();
            project.SendInfoToLog("Закончили.");

            return executionResult;
        }
    }
}





            /* 
             * НАГУЛИВАНИЕ КУКИСОВ  
             * 
            project.SendInfoToLog("Считываем входные настройки", true);
            new InputSettings(instance, project).InitializationInputValue();

            project.SendInfoToLog("Запускаем нагуливание кук.", true);
            new StartMethod(instance, project).FeedingCookies();
            project.SendInfoToLog("Закончили.");
            */