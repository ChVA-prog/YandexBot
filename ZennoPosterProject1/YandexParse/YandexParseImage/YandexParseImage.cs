using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoPosterEmulation;
using ZennoPosterProject1;
using ZennoPosterYandexWalk;

namespace ZennoPosterYandexParseImage
{
    class YandexParseImage : YandexParseImageValue
    {
        readonly IZennoPosterProjectModel project;
        readonly Instance instance;

        public YandexParseImage(Instance instance, IZennoPosterProjectModel project) : base(project)
        {
            this.instance = instance;
            this.project = project;
        }
        public void StartParse()
        {
            SwipeAndClick swipeAndClick = new SwipeAndClick(instance, project);
            WebClient Client = new WebClient();
            ParseImageSettings parseImageSettings = new ParseImageSettings(instance,project);
            YandexNavigate yandexNavigate = new YandexNavigate(instance, project);
            AdditionalMethods additionalMethods = new AdditionalMethods(instance, project);
            Random random = new Random();

            string ParseKeyword = parseImageSettings.ReadParseKeyword();
            yandexNavigate.GoToSearchQuery(ParseKeyword);
            parseImageSettings.SetSearchImageFilter();

            string DirPath = project.Directory + @"\ResultImage\" + ParseKeyword + @"\";
            if (!Directory.Exists(DirPath)) Directory.CreateDirectory(DirPath);

            int Counter = 0;
            while (true)
            {
                HtmlElement hhe = instance.ActiveTab.FindElementByXPath("//a[contains(@class, 'link link_theme')]", Counter+1);
                swipeAndClick.ClickToElement(hhe);
                Thread.Sleep(500);
                instance.ActiveTab.Touch.SwipeBetween(random.Next(50, 100), random.Next(400, 500), random.Next(50, 100), random.Next(200, 300));
                HtmlElement aa = instance.ActiveTab.FindElementByXPath("//button[contains(text(),'Поделиться')]", 0);
                swipeAndClick.ClickToElement(aa);
                aa = instance.ActiveTab.FindElementByXPath("//button[contains(text(),'Поделиться')]", 1);
                swipeAndClick.ClickToElement(aa);
               
                Thread.Sleep(1000);
                if (instance.AllTabs.Length > 1)
                {
                    instance.GetTabByAddress("popup").Close();
                    Counter++;
                    continue;
                }
                HtmlElement bb = instance.ActiveTab.FindElementByXPath("//span[contains(@class, 'share-copy__icon share-copy__copy-text icon icon_type_share2')]", 0);
                string ImagePath = DirPath + bb.InnerHtml.Split('/').Last().Split('.')[0] + ".jpg";

                try
                {
                    Client.DownloadFileAsync(new Uri(bb.InnerHtml), ImagePath);
                }
                catch (Exception)
                {

                }


                instance.ActiveTab.MainDocument.EvaluateScript("javascript:history.back()");

                additionalMethods.WaitHtmlElement("//a[contains(@class, 'link link_theme')]", Counter + 1);

                parseImageSettings.DeleteBrokenFile(DirPath);

                if (new DirectoryInfo(DirPath).GetFiles().Length == CountParseImage)
                {
                    break;
                }
                instance.ActiveTab.Touch.SwipeBetween(random.Next(50, 100), random.Next(400, 500), random.Next(150, 200), random.Next(200, 300));
                Counter++;
            }
        }

    }
}
