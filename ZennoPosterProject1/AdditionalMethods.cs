using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoLab.CommandCenter;
using ZennoPosterDataBaseAndProfile;
using ZennoPosterProxy;
using System;
using System.Threading;

namespace ZennoPosterProject1
{
    public class AdditionalMethods       
    {
        readonly IZennoPosterProjectModel project;
        readonly Instance instance;
        public AdditionalMethods(Instance instance, IZennoPosterProjectModel project)
        {
            this.instance = instance;
            this.project = project;
        }

        public void ErrorExit()
        {
            ProxyDB proxyDB = new ProxyDB(instance, project);

            try
            {
                proxyDB.ChangeIp();
                
                proxyDB.ChangeStatusProxyInDB("Free");
            }
            catch(Exception ex)
            {
                project.SendErrorToLog(ex.Message);
            }

            new Profile(instance,project).UpdateStatusProfile("Free");
            throw new Exception("Выходим по ошибке!");
        }//Завершение программы по ошибке с изменением статусов профиля и прокси

        public void WaitDownloading()
        {
            Random random = new Random();
            instance.ActiveTab.WaitDownloading();
            Thread.Sleep(random.Next(10000,15000));
            instance.ActiveTab.WaitDownloading();
        }//Ожидание загрузки страницы
    }
}
