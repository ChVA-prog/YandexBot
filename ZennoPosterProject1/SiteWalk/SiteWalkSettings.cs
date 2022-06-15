using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoLab.CommandCenter;
using System.Collections.Generic;
using System;
using System.Linq;

namespace ZennoPosterSiteWalk
{
    class SiteWalkSettings
    {
        readonly IZennoPosterProjectModel Project;
        readonly Instance instance;

        public SiteWalkSettings(Instance instance, IZennoPosterProjectModel project)
        {
            this.instance = instance;
            this.Project = project;
            
        }

        public static List<string> SiteVisitUrl = new List<string>();
       
        public void ReadSiteVisitUrl()
        {
            SiteVisitUrl = SiteWalkValue.UrlListToVisit.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();           
        }//Получение списка сайтов для посещения

        public void ShuffleListSitesForVisit()
        {
            new SiteWalkValue(instance, Project);
            ReadSiteVisitUrl();      
            SiteVisitUrl.ShuffleList();
        }//Перемешивание списка сайтов для посещения
    }
}
