using System;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoPosterEmulation;
using System.Threading;
using ZennoPosterProject1;
using ZennoPosterSiteWalk;

namespace ZennoPosterYandexWalk
{
    class YandexNavigate : YandexWalkValue
    {
        readonly Instance instance;
        readonly IZennoPosterProjectModel Project;

        public YandexNavigate(Instance instance, IZennoPosterProjectModel project) : base(project)
        {
            this.instance = instance;
            this.Project = project;
        }

        public void GoToSearchQuery()
        { 
            SwipeAndClick swipeAndClick = new SwipeAndClick(instance, Project);           
            string RandomSearchQueries = new YandexWalkSettings(instance, Project).GetRandomSearchQueries();

            GoToYandex();

            HtmlElement HtmlElementInputSearch = instance.ActiveTab.FindElementByXPath(HtmlElementInputSearchIn, 0);
            HtmlElement HtmlElementSearchButton = instance.ActiveTab.FindElementByXPath(HtmlElementSearchButtonIn, 0);

            Project.SendInfoToLog("Вводим поисковый запрос в строку",true);
            swipeAndClick.SetText(HtmlElementInputSearch, RandomSearchQueries);

            if (String.IsNullOrEmpty(HtmlElementInputSearch.GetAttribute("value")))
            {
                throw new Exception("Поисковый запрос не ввелся в строку");
            }

            Project.SendInfoToLog("Кликаем по кнопке найти",true);

            //Сделать проверку нажатия кнопки найти 

            swipeAndClick.ClickToElement(HtmlElementSearchButton);
            new AdditionalMethods(instance,Project).WaitDownloading();
            CloseYandexTrash();
        }//Ввод поискового запроса и переход по нему



        public void GoToYandex()
        {           
            Project.SendInfoToLog("Переходим на страницу яндекса", true);
            instance.ActiveTab.Navigate(new YandexWalkSettings(instance,Project).GetRandomYandexHost());
            new AdditionalMethods(instance, Project).WaitDownloading();
        }//Переход в яндекс
        public void CloseUnnecessaryWindows()
        {
            if (instance.AllTabs.Length > 1)
            {
                instance.ActiveTab.Close();
                Project.SendInfoToLog("Закрыли лишнюю вкладку.", true);
            }
            else
            {
                if (!instance.ActiveTab.URL.Contains("/search/") || instance.ActiveTab.FindElementByXPath(HtmlElementSearchResultsCard, 0).IsVoid)
                {
                    instance.ActiveTab.MainDocument.EvaluateScript("javascript:history.back()");
                }
            }
        }//Закрываем лишнюю вкладку
        public bool GoSearchCard(HtmlElement LearnElement)
        {
            SiteWalk siteWalk = new SiteWalk(instance, Project);
            YandexWalkSettings yandexWalkSettings = new YandexWalkSettings(instance, Project);
            SwipeAndClick swipeAndClick = new SwipeAndClick(instance, Project);
            Random random = new Random();

            HtmlElement GetSearchCard = LearnElement.FindChildByXPath(HtmlElementUrlSearchCard, 0);

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
            CloseYandexTrash();
            Project.SendInfoToLog("Изучаем сайт: " + ClearCurenSite, true);
            siteWalk.SiteRandomWalk();
            Thread.Sleep(random.Next(4000, 8000));

            return false;
        }//Переходим в карточку
        public void CloseYandexTrash()
        {
            Random random = new Random();
            SwipeAndClick swipeAndClick = new SwipeAndClick(instance,Project);
            instance.ActiveTab.WaitDownloading();
            Thread.Sleep(random.Next(2000, 5000));
            instance.ActiveTab.WaitDownloading();

            HtmlElement alisa = instance.ActiveTab.FindElementByXPath("//span[starts-with(text(),'Закрыть')]", 0); //Алиса
            HtmlElement kinopoisk = instance.ActiveTab.FindElementByXPath("//a[starts-with(text(),'Остаться')]", 0); //Кинопоиск
            HtmlElement dzen = instance.ActiveTab.FindElementByXPath("//div[starts-with(text(),'В приложении удобнее')] | //div[starts-with(text(),'Пора переходить')]", 0); //Дзен
            HtmlElement Yamerket = instance.ActiveTab.FindElementByXPath("//span[starts-with(text(),'Продолжить на сайте')]", 0); //Яндекс маркет
            HtmlElement YandexBrowser = instance.ActiveTab.FindElementByXPath("//span[starts-with(text(),'Позже')] | //span[starts-with(text(),'Не сейчас')]", 0); //Яндекс браузер
            HtmlElement Yamerket2 = instance.ActiveTab.FindElementByXPath("//span[starts-with(text(),'Скрыть')]", 0); //Яндекс маркет2
            //КАПЧА
            HtmlElement IAmNotRobot = instance.ActiveTab.FindElementByXPath("//span[contains(text(),'похожи на автоматические')]", 0);
            HtmlElement InpuCaptcha = instance.ActiveTab.FindElementByXPath("//label[contains(text(),'Введите текст с картинки')]", 0);
            HtmlElement InputTextCaptcha = instance.ActiveTab.FindElementByXPath("//input[contains(@class,'Textinput-Control')]", 0);
            HtmlElement Captcha = instance.ActiveTab.FindElementByXPath("//img[contains(@class,'AdvancedCaptcha')]", 0);
            HtmlElement SendCaptcha = instance.ActiveTab.FindElementByXPath("//span[contains(text(),'Отправить')]", 0);


            if (!IAmNotRobot.IsVoid)
            {
                swipeAndClick.ClickToElement(instance.ActiveTab.FindElementByXPath("//div[contains(@class,'CheckboxCaptcha')]", 0));
            }    
            
            if (!InpuCaptcha.IsVoid)
            {              
                string recognition = ZennoPoster.CaptchaRecognition("RuCaptcha.dll", Captcha.DrawToBitmap(false), "");
                swipeAndClick.SetText(InputTextCaptcha, recognition.Split('-')[0]);
                swipeAndClick.ClickToElement(SendCaptcha);
            }

            if (!alisa.IsVoid)
            {
                swipeAndClick.SwipeAndClickToElement(alisa);
            }
            if (!kinopoisk.IsVoid)
            {
                swipeAndClick.SwipeAndClickToElement(kinopoisk);
            }
            if (!dzen.IsVoid)
            {
                var top = Convert.ToInt32(instance.ActiveTab.MainDocument.EvaluateScript("return window.innerHeight"));
                var left = Convert.ToInt32(instance.ActiveTab.MainDocument.EvaluateScript("return window.innerWidth"));                
                instance.ActiveTab.Touch.Touch(left - random.Next(10, 20), (top - top) + random.Next(5, 20));  // Выбор контрольного вопроса
            }
            if (!Yamerket.IsVoid)
            {
                swipeAndClick.SwipeAndClickToElement(Yamerket);
            }
            if (!YandexBrowser.IsVoid)
            {
                swipeAndClick.SwipeAndClickToElement(YandexBrowser);
            }
            if (!Yamerket2.IsVoid)
            {
                swipeAndClick.SwipeAndClickToElement(Yamerket2);
            }
        }
    }
}
