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
           
            profile.DownloadProfileInZennoposter();
            proxyDB.SetProxyInInstance();
            try
            {
                new YandexWalk(instance, project).GoYandexWalk();
            }
            catch(Exception ex)
            {
                project.SendErrorToLog("Вышли из GoYandexWalk по ошибке: " + ex.Message);
                new AdditionalMethods(instance, project).ErrorExit();
            }
            proxyDB.ChangeIp();
            proxyDB.ChangeStatusProxyInDB("Free");
            profile.UpdateStatusProfile("Free", DataBaseAndProfileValue.CountSession + 1, DataBaseAndProfileValue.CountSessionDay +1);

        }//Запуск нагуливания кукисов
    }
}
