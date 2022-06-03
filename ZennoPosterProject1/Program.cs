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
using Humaniation;
using ZennoPosterSiteWalk;
using ZennoPosterEmulation;
using ZennoPosterDataBaseAndProfile;
using System.Data.SQLite;

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
            SwipeAndClick swipeAndClick = new SwipeAndClick(instance, project);
            new SiteWalkValue(instance, project);
            Random random = new Random();

            DB dB = new DB(instance,project);

            Profile profile = new Profile(instance, project);
       
            profile.DownloadProfileInZennoposter();
            profile.UpdateStatusProfile("Free",4,4);

            return executionResult;
        }
    }
}