using System;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoPosterEmulation;


namespace ZennoPosterProject1
{
    /// <summary>
    /// Класс для запуска выполнения скрипта
    /// </summary>
    public class Program : IZennoExternalCode
    {
        
        /// <summary>
        /// Метод для запуска выполнения скрипта
        /// </summary>
        /// <param name="instance">Объект инстанса выделеный для данного скрипта</param>
        /// <param name="project">Объект проекта выделеный для данного скрипта</param>
        /// <returns>Код выполнения скрипта</returns>		
        public int Execute(Instance instance, IZennoPosterProjectModel project)
        {
            int executionResult = 0;
            project.SendInfoToLog("Это сборка из ветки FeedCookies!", true);
            project.SendInfoToLog("Считываем входные настройки", true);
            new InputSettings(instance, project).InitializationInputValue();


            //SwipeAndClick swipeAndClick = new SwipeAndClick(instance,project);

            //HtmlElement IAmNotRobot = instance.ActiveTab.FindElementByXPath("//span[contains(text(),'похожи на автоматические')]", 0);
            //if (!IAmNotRobot.IsVoid)
            //{
            //    swipeAndClick.ClickToElement(instance.ActiveTab.FindElementByXPath("//div[contains(@class,'CheckboxCaptcha')]", 0));
            //}

            //HtmlElement InpuCaptcha = instance.ActiveTab.FindElementByXPath("//label[contains(text(),'Введите текст с картинки')]", 0);
            //if (!InpuCaptcha.IsVoid)
            //{
            //    HtmlElement InputTextCaptcha = instance.ActiveTab.FindElementByXPath("//input[contains(@class,'Textinput-Control')]", 0);
            //    HtmlElement Captcha = instance.ActiveTab.FindElementByXPath("//img[contains(@class,'AdvancedCaptcha')]", 0);
            //    string CaptchaImgUrl = Captcha.GetAttribute("src");
            //    project.SendInfoToLog(CaptchaImgUrl);




            //}

            //string url = "https://ext.captcha.yandex.net/image?key=00AfT1S8cXo2ulreugGAtM00qkxwgHHt";
            //// Отправить на распознавание
            //var goga = ZennoPoster.CaptchaRecognition("RuCaptcha.dll", url, "");

            //project.SendInfoToLog(goga);




            StartMethod startMethod = new StartMethod(instance, project);

            try
            {
                startMethod.FeedingCookies();
            }
            catch (Exception ex)
            {
                project.SendErrorToLog("Не смогли нагулять куки: " + ex.Message);
            }

            return executionResult;           
        }
    }
}