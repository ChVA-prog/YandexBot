using System;
using System.Collections.Generic;
using System.Linq;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoPosterEmulation;

namespace ZennoPosterYandexWalk
{
    


    class YandexWalkSettings
    {
        readonly IZennoPosterProjectModel Project;
        readonly Instance instance;
        public YandexWalkSettings(Instance _instance, IZennoPosterProjectModel _project)
        {
            this.instance = _instance;
            this.Project = _project;
        }

        Random Random = new Random();

        public string GetRandomSearchQueries()
        {
            string[] YandexSearchQueries = Project.Variables["set_SearchQueries"].Value.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            return YandexSearchQueries[Random.Next(0, YandexSearchQueries.Length)];
        }
        public int GetRandomPageCountSearch()
        {
            int CountPage = Extension.ParseRangeValueInt(YandexWalkValue.PageCountSearch).ValueRandom;

            return CountPage;
        }
        public string GetRandomYandexHost()
        {
            string[] YandexHosts = Project.Variables["set_YandexHosts"].Value.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            
            return YandexHosts[Random.Next(0,YandexHosts.Length)];
        }




        public List<string> MyUrlList = new List<string>();
        public void ReadMyUrl()
        {

            MyUrlList = YandexWalkValue.MyUrl.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
        }

        public bool CheckMyUrl(string url)
        {
            string Url = url.GetUrlToDomain();
            foreach(string site in MyUrlList)
            {
                if (site.Contains(Url))
                    return true;
            }
            return false;
        }
        

        

    }

}
