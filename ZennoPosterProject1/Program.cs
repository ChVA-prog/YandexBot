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
using ZennoPosterProject1.YandexWalk;

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
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//input[@name='text']", 0);
            //swipeAndClick.SwipeAndClickToElement(he);

            YandexWalk.YandexWalk yandexWalkSettings = new YandexWalk.YandexWalk(instance, project);

            yandexWalkSettings.GoToSearchQuery();



            // HtmlElement HtmlElementSearchButton = instance.ActiveTab.FindElementByXPath("//button[@class='mini-suggest__button']", 0);
            //swipeAndClick.ClickToElement(HtmlElementSearchButton);

            return executionResult;
        }
    }
}