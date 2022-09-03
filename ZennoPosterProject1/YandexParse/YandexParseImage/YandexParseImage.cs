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

namespace ZennoPosterYandexParseImage
{
    class YandexParseImage 
    {
        readonly IZennoPosterProjectModel project;
        readonly Instance instance;

        public YandexParseImage(Instance instance, IZennoPosterProjectModel project)
        {
            this.instance = instance;
            this.project = project;
        }
        public void parse()
        {
            SwipeAndClick swipeAndClick = new SwipeAndClick(instance, project);
            WebClient Client = new WebClient();
            ParseImageSettings parseImageSettings = new ParseImageSettings(instance,project);

            string ParseKeyword = parseImageSettings.ReadParseKeyword();
            string DirPath = project.Directory + @"\ResultImage\" + ParseKeyword + @"\";

            if (!Directory.Exists(DirPath)) Directory.CreateDirectory(DirPath);




            AdditionalMethods additionalMethods = new AdditionalMethods(instance, project);
            int counter = 100;
            int i = 0;
            while (true)
            {
                HtmlElement hhe = instance.ActiveTab.FindElementByXPath("//a[contains(@class, 'link link_theme')]", i);
                swipeAndClick.ClickToElement(hhe);
                Thread.Sleep(500);
            eshc:
                HtmlElement aa = instance.ActiveTab.FindElementByXPath("//button[contains(text(),'Поделиться')]", 0);
                swipeAndClick.ClickToElement(aa);

                Thread.Sleep(1000);
                if (instance.AllTabs.Length > 1)
                {
                    instance.GetTabByAddress("popup").Close();
                    goto eshc;
                }
                HtmlElement bb = instance.ActiveTab.FindElementByXPath("//span[contains(@class, 'share-copy__icon share-copy__copy-text icon icon_type_share2')]", 0);

                string path = DirPath + i + ".jpg";

                try
                {
                    Client.DownloadFileAsync(new Uri(bb.InnerHtml), path);
                }
                catch (Exception)
                {

                }


                instance.ActiveTab.MainDocument.EvaluateScript("javascript:history.back()");

                additionalMethods.WaitHtmlElement("//a[contains(@class, 'link link_theme')]", i + 1);

                foreach (var items in System.IO.Directory.GetFiles(DirPath))
                {
                    System.IO.FileInfo file = new System.IO.FileInfo(items);
                    if (System.IO.Path.GetExtension(items) == ".jpg" && file.Length / 1024 <= 0)
                    {
                        try
                        {
                            file.Delete();
                        }
                        catch (Exception) { }
                    }
                }
                if (new DirectoryInfo(DirPath).GetFiles().Length < counter)
                {

                }
                else
                {
                    break;
                }
                i++;
            }
        }

    }
}
