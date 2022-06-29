using System;
using System.Collections.Generic;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoPosterEmulation;
using System.Threading;

namespace ZennoPosterYandexWalk
{   
    class YandexWalkSettings
    {
        readonly IZennoPosterProjectModel Project;
        readonly Instance instance;

        Random Random = new Random();

        public YandexWalkSettings(Instance _instance, IZennoPosterProjectModel _project)
        {
            this.instance = _instance;
            this.Project = _project;
        }                            
        public bool CheckMyUrl(string url)
        {
            try
            {                
                foreach (string site in YandexWalkValue.MyUrlList)
                {
                    if (site.Contains(Convert.ToString(url)))
                    {
                        return true;
                    }
                    if(url.ToLower().Contains("yandex") || url.ToLower().Contains("яндекс"))
                    {
                        Project.SendWarningToLog("Сайт яндекса, пропускаем его", true);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Project.SendErrorToLog(ex.Message);
                return false;
            }
            return false;
        }//Проверяем не содержит ли карточка для перехода мой URL      
        public void GoOrLearnCard(List<int> SearchCardList)
        {
            YandexNavigate yandexNavigate = new YandexNavigate(instance, Project);
            SwipeAndClick swipeAndClick = new SwipeAndClick(instance, Project);

            foreach (int i in SearchCardList)
            {
                HtmlElement LearnElement = instance.ActiveTab.FindElementByXPath(YandexWalkValue.HtmlElementSearchResultsCard, i);

                if (Random.Next(0, 100) < YandexWalkValue.CountGetCard.ParseRangeValueInt().ValueRandom)
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
                }
                yandexNavigate.CloseUnnecessaryWindows();
                Thread.Sleep(Random.Next(2000, 4000));
            }
        }//Решаем переходим в карточку или просто изучаем
        public List<int> CountLearnCard(int CountCard)
        {           
            List<int> SearchCardList = new List<int>();
            for (int i = 0; i < CountCard; i++)
            {
                int rnd = Random.Next(0, CountCard);
                if (SearchCardList.Contains(rnd))
                {
                    i--;
                    continue;
                }
                SearchCardList.Add(rnd);
            }
            return SearchCardList;
        } //Количество карточек с которыми будем работать
        public string GetRandomSearchQueries()
        {
            string[] YandexSearchQueries = Project.Variables["set_SearchQueries"].Value.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            return YandexSearchQueries[Random.Next(0, YandexSearchQueries.Length)];
        }//Получение рандомного поискового запроса
        public int GetRandomPageCountSearch()
        {
            int CountPage = ZennoPosterEmulation.Extension.ParseRangeValueInt(YandexWalkValue.PageCountSearch).ValueRandom;

            return CountPage;
        }//Получение рандомного количества изучаемых страниц
        public string GetRandomYandexHost()
        {
            string[] YandexHosts = Project.Variables["set_YandexHosts"].Value.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            return YandexHosts[Random.Next(0, YandexHosts.Length)];
        }//Получение рандомного хоста яндекса
        public bool CheckMySiteInSearchList()
        {


            return true;
        } //TODO
    }
}
