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

namespace ZennoPosterProject1.YandexWalk
{
    public class YandexWalk
    {
        readonly Instance instance;
        readonly IZennoPosterProjectModel zennoPosterProjectModel;
        
        public YandexWalk(Instance _instance, IZennoPosterProjectModel _zennoPosterProjectModel)
        {
            this.instance = _instance;
            this.zennoPosterProjectModel = _zennoPosterProjectModel;
                       
        }

        public void GoToYandex()
        {
            instance.ActiveTab.Navigate(new YandexWalkSettings(instance, zennoPosterProjectModel).GetRandomYandexHost());
            instance.ActiveTab.WaitDownloading();
        }

        public void GoToSearchQuery()
        {
            SwipeAndClick swipeAndClick = new SwipeAndClick(instance, zennoPosterProjectModel);
            YandexWalkSettings yandexWalkSettings = new YandexWalkSettings(instance, zennoPosterProjectModel);

            GoToYandex();

            HtmlElement HtmlElementInputSearch = instance.ActiveTab.FindElementByXPath(yandexWalkSettings.HtmlElementInputSearch, 0);
            HtmlElement HtmlElementSearchButton = instance.ActiveTab.FindElementByXPath(yandexWalkSettings.HtmlElementSearchButton, 0);

            

            swipeAndClick.SetText(HtmlElementInputSearch, new YandexWalkSettings(instance, zennoPosterProjectModel).GetRandomSearchQueries());            
            swipeAndClick.SwipeAndClickToElement(HtmlElementSearchButton);
        }



    }
}
