using System;
using System.Collections.Generic;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoPosterEmulation;
using System.Threading;

namespace ZennoPosterYandexWalk
{   
    class YandexWalkSettings : YandexWalkValue
    {
        readonly IZennoPosterProjectModel Project;
        readonly Instance instance;

        Random random = new Random();

        public YandexWalkSettings(Instance _instance, IZennoPosterProjectModel _project) : base(_project)
        {
            this.instance = _instance;
            this.Project = _project;
        }
        public void GoOrLearnCard(List<int> SearchCardList, string NextPageHtmlElementSearchResultsCard)
        {
            YandexNavigate yandexNavigate = new YandexNavigate(instance, Project);
            SwipeAndClick swipeAndClick = new SwipeAndClick(instance, Project);
            SearchCardList.Sort();

            foreach (int i in SearchCardList)
            {
                HtmlElement LearnElement = instance.ActiveTab.FindElementByXPath(NextPageHtmlElementSearchResultsCard, i);

                if (random.Next(0, 100) < CountGetCard.ParseRangeValueInt().ValueRandom)
                {
                    if (yandexNavigate.GoSearchCard(LearnElement))
                    {
                        continue;
                    }
                }
                else
                {
                    Project.SendInfoToLog("Просто изучаем карточку ", true);

                    swipeAndClick.SwipeToElement(LearnElement);
                    Thread.Sleep(random.Next(2000, 4000));
                }
                yandexNavigate.CloseUnnecessaryWindows();               
            }
        }//Решаем переходим в карточку или просто изучаем
        public bool CheckMyUrl(string url)
        {           
            try
            {                
                foreach (string site in MyUrlList)
                {
                    if (site.Contains(Convert.ToString(url)))
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Project.SendWarningToLog("Не удалось сделать проверку URL",true);
                return false;
            }
            return false;
        }//Проверяем не содержит ли карточка для перехода мой URL      
        public string GetRandomYandexHost()
        {
            string[] YandexHosts = Project.Variables["set_YandexHosts"].Value.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            return YandexHosts[random.Next(0, YandexHosts.Length)];
        }//Получение рандомного хоста яндекса
        public string GetRandomSearchQueries()
        {
            string[] YandexSearchQueries = Project.Variables["set_SearchQueries"].Value.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            return YandexSearchQueries[random.Next(0, YandexSearchQueries.Length)];
        }//Получение рандомного поискового запроса
        public int GetRandomPageCountSearch()
        {
            int CountPage = ZennoPosterEmulation.Extension.ParseRangeValueInt(PageCountSearch).ValueRandom;

            return CountPage;
        }//Получение рандомного количества изучаемых страниц
        public List<int> CountLearnCard(int CountCard)
        {
            List<int> SearchCardList = new List<int>();
            for (int i = 0; i < CountCard; i++)
            {
                int rnd = random.Next(0, CountCard);
                if (SearchCardList.Contains(rnd))
                {
                    i--;
                    continue;
                }
                SearchCardList.Add(rnd);
            }
            return SearchCardList;
        } //Получение рандомного количиства карточек с которыми будем работать
    }
}
