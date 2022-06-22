using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Resources;
using System.Text;
using ZennoLab.CommandCenter;
using ZennoLab.Emulation;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoLab.InterfacesLibrary.ProjectModel.Enums;
using ZennoLab.InterfacesLibrary;
using ZennoLab.InterfacesLibrary.ProjectModel.Collections;
using ZennoLab.Macros;
using Global.ZennoExtensions;
using ZennoLab.CommandCenter.TouchEvents;
using ZennoLab.CommandCenter.FullEmulation;
using ZennoLab.InterfacesLibrary.Enums;
using ZennoPosterSiteWalk;
using ZennoPosterEmulation;
using ZennoPosterDataBaseAndProfile;
using System.Data.SQLite;
using ZennoPosterProxy;
using ZennoPosterYandexWalk;
using System.Threading;
using Newtonsoft.Json;
using ZennoPosterYandexRegistrationSmsServiceSmsHubOrg;
using ZennoPosterYandexRegistration;


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

            SwipeAndClick swipeAndClick = new SwipeAndClick(instance, project);
            StartMethod startMethod = new StartMethod(instance, project);
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//div[contains(@class, 'AdditionalPersonalInfo-birthday')]", 0); //изменить дату рождения
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//input[contains(@name, 'birthday-day')]", 0); //указать день рождения
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//select[contains(@name, 'month')]", 0); //окно выбора месяца рождения
            //СДЕЛАТЬ ВЫБОР МЕСЯЦА РОЖДЕНИЯ
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//input[contains(@name, 'birthday-year')]", 0); //указать год рождения
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//div[starts-with(text(),'Мужской')]", 0); //Выбор мужского пола
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//div[starts-with(text(),'Женский')]", 0); //Выбор женского пола
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//input[contains(@name, 'city')]", 0); //указать город 
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//span[contains(@class, 'p-control-saveblock-button')]", 0); //сохранить настройки
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//span[contains(@class, 'AdditionalPersonalInfo-link')]", 0); //изменить публичный адрес
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//span[contains(@class, 'PublicId-suggestValue')]", random.Next(0,3)); //выбор никнейма
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//div[contains(@class, 'PublicId-formButtons')]", 0); //Подтвердить никнейм

            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//span[contains(@class,'PageNavigation-linkTitle')]", 0); //Возврат в аккаунт
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//a[contains(@class,'personal-info__add-avatar d-link')]", 0); //Добавить фото
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//a[contains(@class,'personal-info__add-avatar d-link')]", 0); //Ввод ссылки на фото
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//button[contains(@class,'edit-avatar_upload-url')]", 0); //Загрузить картинку
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//span[contains(@class,'Button2-Text')]", 0); //Сохранить
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//div[contains(@class,'Section Section_isHidden Addresses Addresses_v2')]", 0); //Адреса
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//div[contains(@class,'Addresses-link')]", 0); //Добавить домашний и рабочий адрес
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//input[contains(@class,'addressLine')]", 0); //Ввод адреса 
            //swipeAndClick.SetText(he, project.Profile.Country + "," + project.Profile.CurrentRegion + "," + "Киевская улица" + "," + "5k6");
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//span[contains(@class,'p-control-saveblock-button')]", 0); //Сохранить адрес


            //swipeAndClick.SetText(he, "");
            // HtmlElement htmlElement = instance.ActiveTab.FindElementByXPath("//a[@class='home-link2 theader__link  home-link2_color_inherit']", 0);
            //swipeAndClick.ClickToElement(htmlElement);


            //string ApiGetResponce = "https://smshub.org/stubs/handler_api.php?api_key=101937U9065b39e17557f2ce72e71392e5eb7be&action=getNumbersStatus&country=0&operator=megafon";

            //var resultHttpGet = ZennoPoster.HttpGet(ApiGetResponce, "", "UTF-8",
            //    ZennoLab.InterfacesLibrary.Enums.Http.ResponceType.BodyOnly);         ПОЛУЧЕНИЕ СПИСКА НОМЕРОВ

            startMethod.YandexRegistration();
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//div[contains(@class,'Section Section_isHidden Addresses Addresses_v2')]", 0); //Адреса
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//div[contains(@class,'Addresses-link')]", 0); //Добавить домашний и рабочий адрес
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//input[contains(@class,'addressLine')]", 0); //Ввод адреса 
            //swipeAndClick.SetText(he, project.Profile.Country + "," + project.Profile.CurrentRegion + "," + "Киевская улица" + "," + "5k6");

            

            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//div[starts-with(text(),'Создать ID')]", 0);
            //swipeAndClick.SwipeAndClickToElement(he);

            return executionResult;
        
        }
    }
}





            /* 
             * НАГУЛИВАНИЕ КУКИСОВ  
             * 
            project.SendInfoToLog("Считываем входные настройки", true);
            new InputSettings(instance, project).InitializationInputValue();

            project.SendInfoToLog("Запускаем нагуливание кук.", true);
            new StartMethod(instance, project).FeedingCookies();
            project.SendInfoToLog("Закончили.");
            */