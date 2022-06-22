using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoLab.CommandCenter;
using ZennoPosterDataBaseAndProfile;
using ZennoPosterEmulation;
using ZennoPosterProxy;
using ZennoPosterYandexWalk;
using ZennoPosterSiteWalk;
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
            Profile profile = new Profile(instance,project);
            ProxyDB proxyDB = new ProxyDB(instance, project);
           
            
            try
            {
                profile.DownloadProfileInZennoposter();
                proxyDB.SetProxyInInstance();
                new YandexWalk(instance, project).GoYandexWalk();
            }
            catch(Exception ex)
            {                
                project.SendErrorToLog("Вышли из GoYandexWalk по ошибке: " + ex.Message,true);
            }
            instance.CloseAllTabs();
            profile.SaveProfile();            
            proxyDB.ChangeIp();
            proxyDB.ChangeStatusProxyInDB("Free");
            profile.UpdateStatusProfile("Free", DataBaseAndProfileValue.CountSession + 1, DataBaseAndProfileValue.CountSessionDay +1);

        }//Запуск нагуливания кукисов
    }
}
