using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoLab.CommandCenter;
using DataBaseProfileAndProxy;
using ZennoPosterYandexWalk;
using System;


namespace ZennoPosterProject1
{
    class StartMethod
    {
        readonly IZennoPosterProjectModel project;
        readonly Instance instance;
        public StartMethod(Instance instance, IZennoPosterProjectModel project)
        {
            this.instance = instance;
            this.project = project;
        }
        public void FeedingCookies()
        {
            project.SendInfoToLog("Начинаем нагуливать куки.");
            Program.logger.Info("Начинаем нагуливать куки.");
            ProfileDB profile = new ProfileDB(project);
            ProxyDB proxyDB = new ProxyDB(instance, project);

            try
            {
                profile.DownloadProfileInZennoposter();
            }

            catch (Exception ex)
            {
                project.SendErrorToLog(ex.Message, true);
                Program.logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }//Загрузка профиля в проект.
            try
            {
                proxyDB.SetProxyInInstance();
            }

            catch (Exception ex)
            {                
                project.SendErrorToLog(ex.Message, true);
                Program.logger.Error(ex.Message);
                profile.UpdateStatusProfile("Free");
                proxyDB.ChangeStatusProxyInDB("Free");
                new AdditionalMethods(instance, project).InstanceScreen();
                throw new Exception(ex.Message);
            }//Установка прокси в проект.
            try
            {                              
                new YandexWalk(instance, project).GoYandexWalk();
            }

            catch(Exception ex)
            {
                Program.logger.Error(ex.Message);
                Program.logger.Error("Количество открытых вкладок: {0}. Url текущей вкладки: {1}." ,instance.AllTabs.Length, instance.ActiveTab.URL);
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
            profile.UpdateStatusProfile("Free", 1, 1);
        }//Нагуливание кук      
    }
}
