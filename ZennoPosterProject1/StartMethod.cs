using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoLab.CommandCenter;
using ZennoPosterDataBaseAndProfile;
using ZennoPosterProxy;
using ZennoPosterYandexWalk;
using System;
using ZennoPosterYandexRegistration;


namespace ZennoPosterProject1
{
    class StartMethod : DataBaseAndProfileValue
    {
        readonly IZennoPosterProjectModel project;
        readonly Instance instance;
        public StartMethod(Instance instance, IZennoPosterProjectModel project) : base (project)
        {
            this.instance = instance;
            this.project = project;
        }

        public void FeedingCookies()
        {
            Profile profile = new Profile(project);
            ProxyDB proxyDB = new ProxyDB(instance, project);

            try
            {
                profile.DownloadProfileInZennoposter();
            }
            catch (Exception ex)
            {
                project.SendErrorToLog(ex.Message, true);
                throw new Exception(ex.Message);
            }//Загрузка профиля в проект.
            try
            {
                proxyDB.SetProxyInInstance();
            }
            catch (Exception ex)
            {                
                project.SendErrorToLog(ex.Message, true);
                profile.UpdateStatusProfile("Free");
                proxyDB.ChangeStatusProxyInDB("Free");
                throw new Exception(ex.Message);
            }//Установка прокси в проект.
            try
            {                              
                new YandexWalk(instance, project).GoYandexWalk();
            }
            catch(Exception ex)
            {
                project.SendErrorToLog(ex.Message, true);
                profile.SaveProfile();
                profile.UpdateStatusProfile("Free");
                proxyDB.ChangeIp();
                proxyDB.ChangeStatusProxyInDB("Free");                
                throw new Exception(ex.Message);
            }//Запуск нагуливания кук
            instance.CloseAllTabs();
            profile.SaveProfile();            
            proxyDB.ChangeIp();
            proxyDB.ChangeStatusProxyInDB("Free");
            profile.UpdateStatusProfile("Free", CountSession + 1, CountSessionDay +1);
        }//Нагуливание кук
        public void YandexRegistration()
        {
            Profile profile = new Profile(project);
            DBMethods dBMethods = new DBMethods(instance, project);
            ProxyDB proxyDB = new ProxyDB(instance, project);

            try
            {
                dBMethods.DownloadProfileInZennoposter();
            }
            catch (Exception ex)
            {               
                project.SendErrorToLog(ex.Message,true);
                throw new Exception(ex.Message);
            }//Загрузка профиля в проект.                      
            try
            {
                proxyDB.SetProxyInInstance();
            }
            catch (Exception ex)
            {
                project.SendErrorToLog(ex.Message, true);
                dBMethods.UpdateStatusProfile("Free");
                throw new Exception(ex.Message);
            }//Установка прокси в инстанс.
            try
            {
                new RegistrationAndSettingsAccount(instance, project).RegisterAccountAndSetPassword();
                new RegistrationAndSettingsAccount(instance, project).SetLoginAndPasswordAndRemovePhoneNumber();
                new RegistrationAndSettingsAccount(instance, project).DeletePhoneNumberFromAccount();
            }
            catch (Exception ex)
            {
                project.SendErrorToLog(ex.Message);
                profile.SaveProfile();
                proxyDB.ChangeIp();
                proxyDB.ChangeStatusProxyInDB("Free");
                dBMethods.UpdateStatusProfile("Free");
                throw new Exception(ex.Message);
            }//Регистрируем аккаунт, устанавливаем логин с паролем, отвязываем номер.
            profile.SaveProfile();
            proxyDB.ChangeIp();
            proxyDB.ChangeStatusProxyInDB("Free");
            dBMethods.UpdateStatusProfile("Free", "YES");
        }//Регистрация в яндексе и отвязка номера
    }
}
