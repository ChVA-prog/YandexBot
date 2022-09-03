using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    class ParseImageSettings : YandexParseImageValue
    {
        readonly IZennoPosterProjectModel project;
        readonly Instance instance;
        public static object LockList = new object();

        public ParseImageSettings(Instance instance, IZennoPosterProjectModel project) : base(project)
        {
            this.instance = instance;
            this.project = project;
        }

        public string ReadParseKeyword()//Получение ключа для парсинга
        {
            lock (LockList)
            {
                string ParseKeyword = null;
                try
                {
                    ParseKeyword = File.ReadLines(KeyWordFilePath).Skip(0).First();
                    File.WriteAllLines(KeyWordFilePath, File.ReadAllLines(KeyWordFilePath).Skip(1));
                    
                }
                catch (Exception ex)
                {
                    project.SendErrorToLog("Файл с ключами для поиска отсутствует либо пустой");
                    throw new Exception("Файл с ключами для поиска отсутствует либо пустой " + ex.Message);
                }
                return ParseKeyword;
            }
        }
        public void SetSearchImageFilter()//Установка фильтров поиска картинок
        {
            SwipeAndClick swipeAndClick = new SwipeAndClick(instance,project);
            AdditionalMethods additionalMethods = new AdditionalMethods(instance, project);
            HtmlElement Image = instance.ActiveTab.FindElementByXPath("//a[starts-with(text(),'Картинки')]", 0);
            swipeAndClick.ClickToElement(Image);
            if (ParseFilter.Contains("Обои"))
            {
                swipeAndClick.ClickToElement(additionalMethods.WaitHtmlElement("//span[starts-with(text(),'Обои')]", 0));
            }
            if (ParseFilter.Contains("Лица"))
            {
                swipeAndClick.ClickToElement(additionalMethods.WaitHtmlElement("//span[starts-with(text(),'Лица')]", 0));
            }
            Thread.Sleep(2000);
            if (!instance.ActiveTab.FindElementByXPath("//a[starts-with(text(),'Закрыть')]", 0).IsVoid)
            {
                swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath("//a[starts-with(text(),'Закрыть')]", 0));
            }
        }

    }
}
