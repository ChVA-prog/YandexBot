using System;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoPosterEmulation;
using System.Threading;

namespace ZennoPosterYandexWalk
{
    class RandomGeneration
    {
        readonly IZennoPosterProjectModel Project;
        readonly Instance instance;

        Random Random = new Random();

        public RandomGeneration(Instance instance, IZennoPosterProjectModel project)
        {
            this.instance = instance;
            this.Project = project;
        }

        public string GetRandomSearchQueries()
        {
            string[] YandexSearchQueries = Project.Variables["set_SearchQueries"].Value.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            return YandexSearchQueries[Random.Next(0, YandexSearchQueries.Length)];
        }//Получение рандомного поискового запроса

        public int GetRandomPageCountSearch()
        {
            int CountPage = Extension.ParseRangeValueInt(YandexWalkValue.PageCountSearch).ValueRandom;

            return CountPage;
        }//Получение рандомного количества изучаемых страниц

        public string GetRandomYandexHost()
        {
            string[] YandexHosts = Project.Variables["set_YandexHosts"].Value.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            return YandexHosts[Random.Next(0, YandexHosts.Length)];
        }//Получение рандомного хоста яндекса
    }
}
