using ZennoLab.InterfacesLibrary.ProjectModel;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ZennoPosterYandexWalk
{
    class YandexWalkValue
    {
        readonly IZennoPosterProjectModel Project;

        public static string HtmlElementInputSearchIn { get; set; }
        public static string HtmlElementSearchButtonIn { get; set; }
        public static string HtmlElementCheckingLoading { get; set; }
        public static string HtmlElementNextPageButton { get; set; }
        public static string HtmlElementSearchResultsCard { get; set; }
        public static string HtmlElementUrlSearchCard { get; set; }
        public static string HtmlElementDescriptionSerchCard { get; set; }
        public static string HtmlElementPageNumber { get; set; }
        public static string HtmlElementCloseAdvertisement { get; set; }
        public string PageCountSearch { get; set; }
        public string CountLearnCard { get; set; }
        public string CountGetCard { get; set; }
        public List<string> MyUrlList { get; set; }

        public YandexWalkValue(IZennoPosterProjectModel _project)
        {
            this.Project = _project;

            HtmlElementInputSearchIn = Project.Variables["set_HtmlElementInputSearch"].Value;
            HtmlElementSearchButtonIn = Project.Variables["set_HtmlElementSearchButton"].Value;
            HtmlElementCheckingLoading = Project.Variables["set_HtmlElementCheckingLoading"].Value;
            HtmlElementNextPageButton = Project.Variables["set_HtmlElementNextPageButton"].Value;
            HtmlElementSearchResultsCard = Project.Variables["set_HtmlElementSearchResultsCard"].Value;
            HtmlElementUrlSearchCard = Project.Variables["set_HtmlElementUrlSearchCard"].Value;
            HtmlElementDescriptionSerchCard = Project.Variables["set_HtmlElementDescriptionSerchCard"].Value;
            HtmlElementPageNumber = Project.Variables["set_HtmlElementPageNumber"].Value;
            HtmlElementCloseAdvertisement = Project.Variables["set_HtmlElementCloseAdvertisement"].Value;
            PageCountSearch = Project.Variables["set_PageCountSearch"].Value;
            CountLearnCard = Project.Variables["set_CountLearnCard"].Value;
            CountGetCard = Project.Variables["set_CountGetCard"].Value;
            MyUrlList = Project.Variables["set_MyUrl"].Value.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
        }      
    }
}
