using ZennoLab.InterfacesLibrary.ProjectModel;
using System.Collections.Generic;
using System.Linq;
using System;
using ZennoPosterProject1;

namespace ZennoPosterYandexWalk
{
    public class YandexWalkValue
    {
        readonly IZennoPosterProjectModel Project;

        public static string HtmlElementInputSearchIn { get; set; }
        public static string HtmlElementSearchButtonIn { get; set; }
        protected string HtmlElementCheckingLoading { get; set; }
        protected string HtmlElementNextPageButton { get; set; }
        protected string HtmlElementSearchResultsCard { get; set; }
        protected string HtmlElementUrlSearchCard { get; set; }
        protected string HtmlElementPageNumber { get; set; }
        protected string PageCountSearch { get; set; }
        protected string CountLearnCardIn { get; set; }
        protected string CountGetCard { get; set; }
        protected string HtmlElementYandexTrashAlisa { get; set; }
        protected string HtmlElementYandexTrashKinopoisk { get; set; }
        protected string HtmlElementYandexTrashDzen { get; set; }
        protected string HtmlElementYandexTrashYandexMarket { get; set; }
        protected string HtmlElementYandexTrashYandexMarket2 { get; set; }
        protected string HtmlElementYandexTrashYandexBrowser { get; set; }
        protected string HtmlElementPageNumberDie { get; set; }
        public string HtmlElementPageIamNotRobot { get; set; }
        public string HtmlElementIAmNotRobot { get; set; }
        public string HtmlElementInputCapcha { get; set; }
        public string HtmlElementPageCapcha { get; set; }
        public string HtmlElementSendCapcha { get; set; }
        public string HtmlElementCapchaImage { get; set; }
        public string HtmlElementCapchaError { get; set; }
        public string HtmlElementNewYandexPageDzen { get; set; }


        protected List<string> MyUrlList { get; set; }
        public YandexWalkValue(IZennoPosterProjectModel _project)
        {
            this.Project = _project;
            HtmlElementInputSearchIn = Project.Variables["set_HtmlElementInputSearch"].Value;
            HtmlElementSearchButtonIn = Project.Variables["set_HtmlElementSearchButton"].Value;
            HtmlElementCheckingLoading = Project.Variables["set_HtmlElementCheckingLoading"].Value;
            HtmlElementNextPageButton = Project.Variables["set_HtmlElementNextPageButton"].Value;
            HtmlElementSearchResultsCard = Project.Variables["set_HtmlElementSearchResultsCard"].Value;
            HtmlElementUrlSearchCard = Project.Variables["set_HtmlElementUrlSearchCard"].Value;
            HtmlElementPageNumber = Project.Variables["set_HtmlElementPageNumber"].Value;
            PageCountSearch = Project.Variables["set_PageCountSearch"].Value;
            CountLearnCardIn = Project.Variables["set_CountLearnCard"].Value;
            CountGetCard = Project.Variables["set_CountGetCard"].Value;
            HtmlElementYandexTrashAlisa = Project.Variables["set_HtmlElementYandexTrashAlisa"].Value;
            HtmlElementYandexTrashKinopoisk = Project.Variables["set_HtmlElementYandexTrashKinopoisk"].Value;
            HtmlElementYandexTrashDzen = Project.Variables["set_HtmlElementYandexTrashDzen"].Value;
            HtmlElementYandexTrashYandexMarket = Project.Variables["set_HtmlElementYandexTrashYandexMarket"].Value;
            HtmlElementYandexTrashYandexMarket2 = Project.Variables["set_HtmlElementYandexTrashYandexMarket2"].Value;
            HtmlElementYandexTrashYandexBrowser = Project.Variables["set_HtmlElementYandexTrashYandexBrowser"].Value;
            HtmlElementPageNumberDie = Project.Variables["set_HtmlElementPageNumberDie"].Value;
            HtmlElementPageIamNotRobot = Project.Variables["set_HtmlElementPageIamNotRobot"].Value;
            HtmlElementIAmNotRobot = Project.Variables["set_HtmlElementIAmNotRobot"].Value;
            HtmlElementInputCapcha = Project.Variables["set_HtmlElementInputCapcha"].Value;
            HtmlElementPageCapcha = Project.Variables["set_HtmlElementPageCapcha"].Value;
            HtmlElementSendCapcha = Project.Variables["set_HtmlElementSendCapcha"].Value;
            HtmlElementCapchaImage = Project.Variables["set_HtmlElementCapchaImage"].Value;
            HtmlElementCapchaError = Project.Variables["set_HtmlElementCapchaError"].Value;
            HtmlElementNewYandexPageDzen = Project.Variables["set_HtmlElementNewYandexPageDzen"].Value;
            MyUrlList = Project.Variables["set_MyUrl"].Value.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
        }
    }
}
