using System;
using System.Linq;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoPosterEmulation;
using System.Threading;
using ZennoPosterProject1;
using ZennoPosterYandexWalk;
using ZennoPosterYandexRegistrationSmsServiceSmsHubOrg;
using System.IO;
using System.Collections.Generic;

namespace ZennoPosterYandexRegistration
{
    class RegistrationAndSettingsAccount : YandexRegistrationValue
    {
        readonly IZennoPosterProjectModel project;
        readonly Instance instance;
        public static object LockList = new object();

        string YandexLogin { get; set; }
        string YandexPassword { get; set; }
        string Mail { get; set; }
        string MailPassword { get; set; }
        string MailPasswordIMAP { get; set; }
        GetNumber getNumber { get; set; }       

        public RegistrationAndSettingsAccount(Instance instance, IZennoPosterProjectModel project) : base(project)
        {
            this.instance = instance;
            this.project = project;
            getNumber = new GetNumber(project);
        }

        public void RegisterAccountAndSetPassword()
        {         
            AdditionalMethods additionalMethods = new AdditionalMethods(instance, project);
            YandexNavigate yandexNavigate = new YandexNavigate(instance, project);
            SwipeAndClick swipeAndClick = new SwipeAndClick(instance, project);
            Random random = new Random();
            
            yandexNavigate.GoToYandex();
            additionalMethods.FuckCapcha();

            try
            {
                swipeAndClick.ClickToElement(additionalMethods.WaitHtmlElement(HtmlElementEnterId, 0));
                Thread.Sleep(random.Next(1500,2000));
                do
                {
                    swipeAndClick.ClickToElement(additionalMethods.WaitHtmlElement(HtmlElementCreatId, 0));
                    Thread.Sleep(random.Next(1500, 2000));
                }
                while (!instance.ActiveTab.URL.ToLower().Contains("reg?origin"));              
escho:               
                getNumber.GetNumberAndId();

                swipeAndClick.SetText(additionalMethods.WaitHtmlElement(HtmlElementSetPhoneNumber, 0), getNumber.PhoneNumber.Substring(1), false);
                swipeAndClick.ClickToElement(instance.ActiveTab.FindElementByXPath(HtmlElementConfirmPhoneNumber, 0));                
                try
                {
                    getNumber.GetSmsCode(true);
                }
                catch (Exception)
                {
                    swipeAndClick.ClickToElement(additionalMethods.WaitHtmlElement(HtmlElementButtonBack, 0));
                    swipeAndClick.ClickToElement(additionalMethods.WaitHtmlElement(HtmlElementButtonBack, 0));
                    Thread.Sleep(random.Next(1500, 2000));
                    if (!instance.ActiveTab.FindElementByXPath(HtmlElementEnterId, 0).IsVoid)
                    {
                        swipeAndClick.ClickToElement(additionalMethods.WaitHtmlElement(HtmlElementEnterId, 0));
                    }
                    swipeAndClick.ClickToElement(additionalMethods.WaitHtmlElement(HtmlElementCreatId, 0));
goto escho;
                }//Получаем смс

                swipeAndClick.SetText(additionalMethods.WaitHtmlElement(HtmlElementInputCodeActivation, 0), getNumber.CodeActivation, false);

                Thread.Sleep(random.Next(3000, 5000));
                if (!instance.ActiveTab.FindElementByXPath(HtmlElementCreateNewAccount, 0).IsVoid)
                {
                    swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement(HtmlElementCreateNewAccount, 0));
                }
                Thread.Sleep(random.Next(3000, 5000));
                if (!instance.ActiveTab.FindElementByXPath(HtmlElementCheckBox, 0).IsVoid)
                {
                    swipeAndClick.ClickToElement(additionalMethods.WaitHtmlElement(HtmlElementCheckBox, 0));
                    swipeAndClick.ClickToElement(additionalMethods.WaitHtmlElement(HtmlElementConfirmUserAgreement, 0));
                }
                
                
                additionalMethods.WaitDownloading();
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось зарегестрировать аккаунт: " + ex.Message);
            }
        }//Регистрация аккаунта
        public void SetLoginAndPasswordAndRemovePhoneNumber()
        {
            AdditionalMethods additionalMethods = new AdditionalMethods(instance, project);
            SwipeAndClick swipeAndClick = new SwipeAndClick(instance, project);           
            Random random = new Random();
            additionalMethods.WaitDownloading();
            try
            {
                swipeAndClick.ClickToElement(additionalMethods.WaitHtmlElement(HtmlElementAccountMenu, 0));
                swipeAndClick.ClickToElement(additionalMethods.WaitHtmlElement(HtmlElementSettings, 0));
                swipeAndClick.ClickToElement(additionalMethods.WaitHtmlElement(HtmlElementAccountSettings, 0));

                additionalMethods.WaitHtmlElement(HtmlElementCheckInterfaceVersion, 0);
                if (!instance.ActiveTab.FindElementByXPath(HtmlElementNewInterface, 0).IsVoid)
                {
                    swipeAndClick.ClickToElement(additionalMethods.WaitHtmlElement(HtmlElementNewInterfacePublicData, 0));
                    swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement(HtmlElementNewInterfaceMainPhone, 0));
                    swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement(HtmlElementHumberSettings, 0));
                    swipeAndClick.ClickToElement(additionalMethods.WaitHtmlElement(HtmlElementWhyDeletePhoneNumber, 0));
                    swipeAndClick.ClickToElement(additionalMethods.WaitHtmlElement(HtmlElementNextPageDeleteNumber, 0));
                    swipeAndClick.SetText(additionalMethods.WaitHtmlElement(HtmlElementSetLogin, 0), project.Profile.NickName, true);
                    swipeAndClick.SetText(additionalMethods.WaitHtmlElement(HtmlElementSetPassword, 0), project.Profile.Password, true);
                    swipeAndClick.ClickToElement(additionalMethods.WaitHtmlElement(HtmlElementNextPageDeleteNumber, 0));
                    swipeAndClick.ClickToElement(additionalMethods.WaitHtmlElement(HtmlElementNewInterfaceConfirm, 0));
                    swipeAndClick.ClickToElement(additionalMethods.WaitHtmlElement(HtmlElementButtonIdForSettingsAccount, 0));
                    swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement(HtmlElementMailAndPhone, 0));
                    swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement(HtmlElementChangeMailAndPhoneList, 0));
                    swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement(HtmlElementOffInputSms, 0));
                    return;
                }//Обработка если вывалился новый интерфейс

                swipeAndClick.ClickToElement(additionalMethods.WaitHtmlElement(HtmlElementCreateLogin, 0));
                if (String.IsNullOrEmpty(additionalMethods.WaitHtmlElement(HtmlElementSetLogin, 0).GetAttribute("value")))
                {
                    swipeAndClick.SetText(additionalMethods.WaitHtmlElement(HtmlElementSetLogin, 0), project.Profile.NickName, false);
                    YandexLogin = project.Profile.NickName;
                }
                else
                {
                    YandexLogin = instance.ActiveTab.FindElementByXPath(HtmlElementSetLogin, 0).GetAttribute("value");
                }
                Thread.Sleep(random.Next(1000, 2000));
                HtmlElement LoginIsBusy = instance.ActiveTab.FindElementByXPath("//div[contains(text(),'логин занят')]", 0);
                if (!LoginIsBusy.IsVoid)
                {
                    swipeAndClick.SetText(additionalMethods.WaitHtmlElement(HtmlElementSetLogin, 0), new Random().Next(1000, 5000).ToString(), false);
                    YandexLogin = instance.ActiveTab.FindElementByXPath(HtmlElementSetLogin, 0).GetAttribute("value");
                }

                swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement(HtmlElementApprovedLogin, 0));
                swipeAndClick.SetText(additionalMethods.WaitHtmlElement(HtmlElementSetPassword, 0), project.Profile.Password, false);
                swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement(HtmlElementApprovedPassword, 0));
                YandexPassword = project.Profile.Password;
                swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement(HtmlElementMailAndPhone, 0));
                swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement(HtmlElementChangeMailAndPhoneList, 0));
                swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement(HtmlElementOffInputSms, 0));
                additionalMethods.WaitDownloading();
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось установить логин или отключить вход по смс: " + ex.Message);
            }

        }//Указание логина и пароля для аккаунта
        public void DeletePhoneNumberFromAccount()
        {
            SwipeAndClick swipeAndClick = new SwipeAndClick(instance, project);
            AdditionalMethods additionalMethods = new AdditionalMethods(instance, project);
            Random random = new Random();
            try
            {
                swipeAndClick.ClickToElement(additionalMethods.WaitHtmlElement(HtmlElementHumberSettings, 0));
                swipeAndClick.ClickToElement(additionalMethods.WaitHtmlElement(HtmlElementWhyDeletePhoneNumber,0));
                swipeAndClick.ClickToElement(additionalMethods.WaitHtmlElement(HtmlElementNextPageDeleteNumber, 0));
                swipeAndClick.ClickToElement(additionalMethods.WaitHtmlElement(HtmlElementSecurityQuestionMenu, 0));

                int top = Convert.ToInt32(instance.ActiveTab.FindElementByXPath(HtmlElementSecurityQuestionMenu, 0).GetAttribute("topInTab"));
                int left = Convert.ToInt32(instance.ActiveTab.FindElementByXPath(HtmlElementSecurityQuestionMenu, 0).GetAttribute("leftInTab"));               
                Thread.Sleep(random.Next(2000, 4000));
                instance.ActiveTab.Touch.Touch(left + random.Next(30, 100), top + random.Next(100, 200));

                swipeAndClick.SetText(additionalMethods.WaitHtmlElement(HtmlElementResponceSecurityQuestion, 0), project.Profile.Password, false);
                swipeAndClick.ClickToElement(additionalMethods.WaitHtmlElement(HtmlElementSaveSecurityQuestion, 0));
                additionalMethods.WaitDownloading();

                if (!instance.ActiveTab.FindElementByXPath(HtmlElementCheckNeedWritePassword, 0).IsVoid)//Введите пароль еще раз (проверрка)
                {
                    swipeAndClick.SetText(additionalMethods.WaitHtmlElement(HtmlElementInputPasswordSecurityQuestion, 0), project.Profile.Password, true);
                    swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement(HtmlElementConfirmPasswordSecurityQuestion, 0));
                }

                swipeAndClick.ClickToElement(additionalMethods.WaitHtmlElement(HtmlElementHumberSettings, 0));
                swipeAndClick.ClickToElement(additionalMethods.WaitHtmlElement(HtmlElementDeletePhoneNumber, 0));
                swipeAndClick.ClickToElement(additionalMethods.WaitHtmlElement(HtmlElementSendSmsForDeletePhoneNumber, 0));

                try
                {
                    getNumber.GetSmsCode(false);
                }
                catch (Exception)
                {
                    swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement(HtmlElementNewInterfaceButtonBack, 0));
                    swipeAndClick.ClickToElement(additionalMethods.WaitHtmlElement(HtmlElementHumberSettings, 0));
                    swipeAndClick.ClickToElement(additionalMethods.WaitHtmlElement(HtmlElementDeletePhoneNumber, 0));
                    swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement(HtmlElementNewInterfaceCheckBox, 0));
                    swipeAndClick.ClickToElement(additionalMethods.WaitHtmlElement(HtmlElementNextPageDeleteNumber, 0));
                    swipeAndClick.SetText(additionalMethods.WaitHtmlElement(HtmlElementInputPasswordForDeletePhoneNumber, 0), project.Profile.Password, true);
                    swipeAndClick.ClickToElement(additionalMethods.WaitHtmlElement(HtmlElementNextPageDeleteNumber, 0));
                }              
                swipeAndClick.SetText(additionalMethods.WaitHtmlElement(HtmlElementInputSmsCodeDeletePhoneNumber, 0), getNumber.CodeActivation, false);
                swipeAndClick.SetText(additionalMethods.WaitHtmlElement(HtmlElementInputPasswordForDeletePhoneNumber, 0), project.Profile.Password, false);
                swipeAndClick.ClickToElement(additionalMethods.WaitHtmlElement(HtmlElementConfirmDeletePhoneNumber, 0));
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось удалить номер из аккаунта: " + ex.Message);
            }
        }//Отвязка номера и установка контрольного вопроса
        public string LinkEmail()
        {
            AdditionalMethods additionalMethods = new AdditionalMethods(instance, project);
            SwipeAndClick swipeAndClick = new SwipeAndClick(instance, project);

            lock (LockList)
            {

                string MailLine = File.ReadLines(EmailListPath).Skip(0).First();
                if (string.IsNullOrEmpty(MailLine) || string.IsNullOrWhiteSpace(MailLine))
                {
                    project.SendErrorToLog("Закончились Доп.Емейлы.", true);
                    return "NULL" + ":" + "NULL" + ":" + "NULL";
                }
                Mail = MailLine.Split(':')[0];
                MailPassword = MailLine.Split(':')[1];
                MailPasswordIMAP = MailLine.Split(':')[2];
                File.WriteAllLines(EmailListPath, File.ReadAllLines(EmailListPath).Skip(1));
            }

            swipeAndClick.ClickToElement(additionalMethods.WaitHtmlElement(HtmlElementButtonIdForSettingsAccount, 0));// кнопка id, для входа в настройки акка
            swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement(HtmlElementMailAndPhone, 0));
            swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement("//a[starts-with(text(),'Добавить адрес')]", 0));
            swipeAndClick.SetText(additionalMethods.WaitHtmlElement("//input[contains(@name, 'email')]", 0), Mail,false);
            swipeAndClick.ClickToElement(additionalMethods.WaitHtmlElement("//div[contains(@class, 'p-control-saveblock-cell-right p-control-saveblock-button')]", 0));
            Thread.Sleep(2000);
            string EmailCode;
            do
            {
                EmailCode = additionalMethods.AcceptMail(Mail, MailPasswordIMAP);
            } while (string.IsNullOrEmpty(EmailCode));            

            swipeAndClick.SetText(additionalMethods.WaitHtmlElement("//input[contains(@name, 'code')]", 0), EmailCode, false);
            swipeAndClick.ClickToElement(additionalMethods.WaitHtmlElement("//div[contains(@class, 'p-control-saveblock-cell-right p-control-saveblock-button')]", 0));

            
            return Mail+":"+MailPassword+":"+MailPasswordIMAP;

        }//Привязка почты
        public void SettingsAccount()
        {
            
            AdditionalMethods additionalMethods = new AdditionalMethods(instance, project);
            SwipeAndClick swipeAndClick = new SwipeAndClick(instance, project);
            Random random = new Random();

            swipeAndClick.ClickToElement(additionalMethods.WaitHtmlElement(HtmlElementButtonIdForSettingsAccount, 0));
            swipeAndClick.ClickToElement(additionalMethods.WaitHtmlElement(HtmlElementAddAccountPhoto, 0));


            List<string> AccountFolder = (from a in Directory.GetFiles(AccountAvatarFolder) select Path.GetFileName(a)).ToList();
            string PathhToPhoto = AccountAvatarFolder + @"\" + AccountFolder[random.Next(0, AccountFolder.Count)];
            instance.SetFilesForUpload(PathhToPhoto);
            swipeAndClick.ClickToElement(additionalMethods.WaitHtmlElement(HtmlElementDownloadAccountPhoto, 0));
            swipeAndClick.ClickToElement(additionalMethods.WaitHtmlElement(HtmlElementSaveAccountPhoto, 0));
            File.Delete(PathhToPhoto);

            swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement(HtmlElementAdditionalPersonalInfo, 0));
            swipeAndClick.SetText(additionalMethods.WaitHtmlElement(HtmlElementInputName, 0), project.Profile.Name, false);
            swipeAndClick.SetText(additionalMethods.WaitHtmlElement(HtmlElementInputLastName, 0), project.Profile.Surname, false);
            swipeAndClick.SetText(additionalMethods.WaitHtmlElement(HtmlElementInputBirthdayDay, 0), project.Profile.BornDay.ToString(), false);

            HtmlElement he = instance.GetTabByAddress("page").GetDocumentByAddress("0").FindElementByTag("form", 0).FindChildByName("month");
            swipeAndClick.ClickToElement(additionalMethods.WaitHtmlElement(HtmlElementInputBirthdayMonth, 0));
            Thread.Sleep(random.Next(1500,2500));
            he.SetValue(project.Profile.BornMonth.ToString(), instance.EmulationLevel, false);
            Thread.Sleep(random.Next(1500, 2500));
            instance.SendText("{ENTER}", 15);

            swipeAndClick.SetText(additionalMethods.WaitHtmlElement(HtmlElementInputBirthdayYear, 0), project.Profile.BornYear.ToString(), false);

            if (project.Profile.Sex.ToString().Contains("Female"))
            {
                HtmlElement Gender = instance.ActiveTab.FindElementByXPath(HtmlElementChangeGender, 0);
                swipeAndClick.SwipeAndClickToElement(Gender);
            }
            else
            {
                HtmlElement Gender = instance.ActiveTab.FindElementByXPath("//div[starts-with(text(),'Мужской')]", 0);
                swipeAndClick.SwipeAndClickToElement(Gender);
            }

            swipeAndClick.SetText(additionalMethods.WaitHtmlElement(HtmlElementInputMySity, 0), project.Profile.CurrentRegion, true);
            swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement(HtmlElementSavePersonalSettings, 0));
            swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement(HtmlElementAdditionalPersonalAdress, 0));
            swipeAndClick.ClickToElement(additionalMethods.WaitHtmlElement(HtmlElementChangePersonalAdress, 0));
            swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement(HtmlElementSavePersonalAdress, 0));
            swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement(HtmlElementAdditionalHomeAdress, 0));

            swipeAndClick.SetText(additionalMethods.WaitHtmlElement(HtmlElementInputHomeAndWorkAdress, 0), AdressList[random.Next(0, AdressList.Count)], true);
            var SetHomeAdressTop = Convert.ToInt32(additionalMethods.WaitHtmlElement(HtmlElementInputHomeAndWorkAdress, 0).GetAttribute("topInTab"));
            var SetHomeAdressLeft = Convert.ToInt32(additionalMethods.WaitHtmlElement(HtmlElementInputHomeAndWorkAdress, 0).GetAttribute("leftInTab"));
            instance.ActiveTab.Touch.Touch(SetHomeAdressLeft + random.Next(30, 100), SetHomeAdressTop + random.Next(50, 60));

            HtmlElement SetWorkAdress = instance.ActiveTab.FindElementByXPath(HtmlElementInputHomeAndWorkAdress, 1);
            swipeAndClick.SetText(SetWorkAdress, AdressList[random.Next(0, AdressList.Count)], true);
            Thread.Sleep(random.Next(1000,2000));
            var SetWorkAdressTop = Convert.ToInt32(SetWorkAdress.GetAttribute("topInTab"));
            var SetWorkAdressLeft = Convert.ToInt32(SetWorkAdress.GetAttribute("leftInTab"));
            instance.ActiveTab.Touch.Touch(SetWorkAdressLeft + random.Next(30, 100), SetWorkAdressTop + random.Next(50, 60));

            swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement(HtmlElementSaveHomeAndWorkAdress, 0));
        }//Настрока личных данных аккаунта
        public void EndUseAccount()//Выход из настроек аккаунта
        {
            AdditionalMethods additionalMethods = new AdditionalMethods(instance, project);
            SwipeAndClick swipeAndClick = new SwipeAndClick(instance, project);
            Random random = new Random();

            swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement("//div[contains(@class, 'user-pic user-pic_has-plus_ user-account__pic')]", 0)); //Иконка профиля
            swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement("//span[starts-with(text(),'Почта')]", 0));//Почта

            additionalMethods.WaitHtmlElement("//div[contains(@class, 'messagesMessage-firstline')]", 0);
            HtmlElementCollection htmlElements = instance.ActiveTab.FindElementsByXPath("//div[contains(@class, 'messagesMessage-firstline')]");//Письма

            for (int i = 0; i < htmlElements.Count; i++)
            {
                swipeAndClick.LongTuch(additionalMethods.WaitHtmlElement("//span[contains(@class, 'messagesMessage-firstline')]", i));//Письмо

                Thread.Sleep(random.Next(1000, 2000));
            }

            Thread.Sleep(random.Next(1000, 2000));
            swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement("//div[starts-with(text(),'Прочитано')]", 0));//Назад
            instance.ActiveTab.Navigate("vk.com", instance.ActiveTab.URL);
            Thread.Sleep(random.Next(1000, 2000));
        }
    }
}
