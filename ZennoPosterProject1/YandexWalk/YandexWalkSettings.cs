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

namespace ZennoPosterProject1.YandexWalk
{
    


    class YandexWalkSettings
    {
        public string HtmlElementInputSearch { get; set; }
        public string HtmlElementSearchButton { get; set; }

        readonly Instance instance;
        readonly IZennoPosterProjectModel zennoPosterProjectModel;

        public YandexWalkSettings(Instance _instance, IZennoPosterProjectModel _zennoPosterProjectModel)
        {
            this.instance = _instance;
            this.zennoPosterProjectModel = _zennoPosterProjectModel;

            HtmlElementInputSearch = zennoPosterProjectModel.Variables["set_HtmlElementInputSearch"].Value;
            HtmlElementSearchButton = zennoPosterProjectModel.Variables["set_HtmlElementSearchButton"].Value;
        }

       public string GetRandomYandexHost()
       {
            string[] YandexHosts = zennoPosterProjectModel.Variables["set_YandexHosts"].Value.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            return YandexHosts[new Random().Next(0,YandexHosts.Length)];
       }

        public string GetRandomSearchQueries()
        {
            string[] YandexSearchQueries = zennoPosterProjectModel.Variables["set_SearchQueries"].Value.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            return YandexSearchQueries[new Random().Next(0, YandexSearchQueries.Length)];
        }

    }
}
