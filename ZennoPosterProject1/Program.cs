﻿using System;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;


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

            new InputSettings(instance, project).InitializationInputValue();

            SwipeAndClick swipeAndClick = new SwipeAndClick(instance,project);
            StartMethod startMethod = new StartMethod(instance, project);

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
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//span[contains(@class,'PageNavigation-linkTitle')]", 0); //Возврат в аккаунт
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//a[contains(@class,'personal-info__add-avatar d-link')]", 0); //Добавить фото
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//a[contains(@class,'personal-info__add-avatar d-link')]", 0); //Ввод ссылки на фото
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//button[contains(@class,'edit-avatar_upload-url')]", 0); //Загрузить картинку
            startMethod.YandexRegistration();

            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//div[contains(@class,'Section Section_isHidden Addresses Addresses_v2')]", 0); //Адреса
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//div[contains(@class,'Addresses-link')]", 0); //Добавить домашний и рабочий адрес
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//input[contains(@class,'addressLine')]", 0); //Ввод адреса 
            //swipeAndClick.SetText(he, project.Profile.Country + "," + project.Profile.CurrentRegion + "," + "Киевская улица" + "," + "5k6");
            HtmlElement he = instance.ActiveTab.FindElementByXPath("//span[contains(@class,'p-control-saveblock-button')]", 0); //Сохранить адрес


            //swipeAndClick.SetText(he, "");

            swipeAndClick.ClickToElement(he);

            //var resultHttpGet = ZennoPoster.HttpGet(ApiGetResponce, "", "UTF-8",
            //    ZennoLab.InterfacesLibrary.Enums.Http.ResponceType.BodyOnly);         ПОЛУЧЕНИЕ СПИСКА НОМЕРОВ




            return executionResult;
        }
    }
}