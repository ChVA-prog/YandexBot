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
            Random random = new Random();

            int executionResult = 0;

            new InputSettings(instance, project).InitializationInputValue();

            SwipeAndClick swipeAndClick = new SwipeAndClick(instance,project);
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//div[@class='theader__login']", 0);  //войти
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//div[@class='passp-button passp-exp-register-button']", 0); //Создать id
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//input[@class='Textinput-Control']", 0); //Номер мобильного телефона
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//div[@class='Phone-controls']", 0); //Утвердить номер телефона
            // HtmlElement he = instance.ActiveTab.FindElementByXPath("//span[@class='Textinput-Box']", 0); //Поле ввода кода смс
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//input[@name='firstname']", 0); //Поле ввода имени
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//input[@name='lastname']", 0); //Поле ввода фамилии
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//span[@class='Button2-Text']", 0); //Поле ввода фамилии
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//span[@class='Checkbox-Tick']", 0); //чек бокс не получать рекламу
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//div[@class='EulaSignUp-controls']", 0); //Принять пользовательское соглашение
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//span[@class='avatar__image-wrapper']", 0); //Аватарка аккаунта
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//div[@class='user-profile__item-title']", 3); //Настройки
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//div[contains(@class, 'link__inner')]", 5); //Настройки аккаунта
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//span[contains(@class, 'personal-info-login__link')]", 0); //Создать логин
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//span[contains(@class, 'Button2-Text')]", 1); //Подтвердить логин
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//input[contains(@name, 'password')]", 0); //Указание пароля
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//div[contains(@class, 'PasswordSignUpInDaHouse-controls')]", 0); //Подтверждение пароля
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
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//div[contains(@class, 'Section Section_isHidden p-mails')]", 0); //Почта и телефоны
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//div[contains(@class, 's-block__mt')]", 0); //Изменить список
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//div[contains(@class,'UniversalTile UniversalTile_isTouch')]", 1); //Отключить вход по смс
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//span[contains(@class,'PageNavigation-linkTitle')]", 0); //Возврат в аккаунт
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//a[contains(@class,'personal-info__add-avatar d-link')]", 0); //Добавить фото
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//a[contains(@class,'personal-info__add-avatar d-link')]", 0); //Ввод ссылки на фото
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//button[contains(@class,'edit-avatar_upload-url')]", 0); //Загрузить картинку
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//span[contains(@class,'Button2-Text')]", 0); //Сохранить
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//div[contains(@class,'Section Section_isHidden Addresses Addresses_v2')]", 0); //Адреса
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//div[contains(@class,'Addresses-link')]", 0); //Добавить домашний и рабочий адрес
            //HtmlElement he = instance.ActiveTab.FindElementByXPath("//input[contains(@class,'addressLine')]", 0); //Ввод адреса 
            //swipeAndClick.SetText(he, project.Profile.Country + "," + project.Profile.CurrentRegion + "," + "Киевская улица" + "," + "5k6");
            HtmlElement he = instance.ActiveTab.FindElementByXPath("//span[contains(@class,'p-control-saveblock-button')]", 0); //Сохранить адрес


            //swipeAndClick.SetText(he, "");

            swipeAndClick.ClickToElement(he);



            return executionResult;
        }
    }
}