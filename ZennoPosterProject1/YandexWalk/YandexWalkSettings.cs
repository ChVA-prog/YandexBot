using System;
using System.Collections.Generic;
using System.Linq;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoPosterEmulation;
using System.Threading;
using ZennoPosterSiteWalk;
using ZennoPosterProject1;

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
                     
        public void CloseUnnecessaryWindows()
        {
            if (instance.AllTabs.Length > 1)
            {
                instance.ActiveTab.Close();
                Project.SendErrorToLog("Закрыли лишнюю вкладку.", true);
            }
            else
            {
                if (!instance.ActiveTab.URL.Contains("/search/") || instance.ActiveTab.FindElementByXPath(YandexWalkValue.HtmlElementSearchResultsCard, 0).IsVoid)
                {
                    instance.ActiveTab.MainDocument.EvaluateScript("javascript:history.back()");
                }
            }
        }//Закрываем лишнюю вкладку

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


        public bool GoSearchCard(HtmlElement LearnElement)
        {
            SiteWalk siteWalk = new SiteWalk(instance, Project);
            YandexWalkSettings yandexWalkSettings = new YandexWalkSettings(instance, Project);
            SwipeAndClick swipeAndClick = new SwipeAndClick(instance, Project);

            HtmlElement GetSearchCard = LearnElement.FindChildByXPath(YandexWalkValue.HtmlElementUrlSearchCard, 0);

            string CurenSite = GetSearchCard.InnerHtml;
            string ClearCurenSite = CurenSite.GetUrlToDomain();

            if (String.IsNullOrEmpty(ClearCurenSite) || String.IsNullOrWhiteSpace(ClearCurenSite))
            {
                return true;
            }
            else
            {
                Project.SendInfoToLog("Делаем проверку на запрещенные для перехода сайты", true);
                if (yandexWalkSettings.CheckMyUrl(CurenSite))
                {
                    return true;
                }
            }

            Project.SendInfoToLog("Переходим в карточку " + ClearCurenSite, true);
            swipeAndClick.SwipeAndClickToElement(LearnElement);

            new AdditionalMethods(instance, Project).WaitDownloading();

            Project.SendInfoToLog("Изучаем сайт: " + ClearCurenSite, true);
            siteWalk.SiteRandomWalk();
            Thread.Sleep(Random.Next(4000, 8000));

            return false;
        }//Переходим в карточку

        public void GoOrLearnCard(List<int> SearchCardList)
        {
            YandexWalkSettings yandexWalkSettings = new YandexWalkSettings(instance, Project);
            SwipeAndClick swipeAndClick = new SwipeAndClick(instance, Project);

            foreach (int i in SearchCardList)
            {
                HtmlElement LearnElement = instance.ActiveTab.FindElementByXPath(YandexWalkValue.HtmlElementSearchResultsCard, i);

                if (Random.Next(0, 100) < YandexWalkValue.CountGetCard.ParseRangeValueInt().ValueRandom)
                {
                    if (yandexWalkSettings.GoSearchCard(LearnElement))
                    {
                        continue;
                    }
                }
                else
                {
                    Project.SendInfoToLog("Просто изучаем карточку ", true);

                    swipeAndClick.SwipeToElement(LearnElement);
                }
                yandexWalkSettings.CloseUnnecessaryWindows();
                Thread.Sleep(Random.Next(4000, 8000));
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
