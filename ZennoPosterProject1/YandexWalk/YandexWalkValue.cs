using ZennoLab.InterfacesLibrary.ProjectModel;
using System.Collections.Generic;
using System.Linq;
using System;

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
            MyUrlList = Project.Variables["set_MyUrl"].Value.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
        }
    }
}
