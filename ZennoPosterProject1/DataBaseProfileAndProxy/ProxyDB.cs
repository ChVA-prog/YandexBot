using System;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using System.Threading;
using System.Data.SQLite;
using System.Net;
using ZennoPosterProject1;

namespace DataBaseProfileAndProxy
{
    class ProxyDB
    {
        private string ProxyLine { get; set; }
        private string ProxyChangeIpUrl { get; set; }
        private int CounterCheckProxy { get; set; }

        public static object LockList = new object();

        readonly Instance instance;
        readonly IZennoPosterProjectModel project;

        public ProxyDB(Instance instance, IZennoPosterProjectModel project)
        {           
            this.instance = instance;
            this.project = project;
        }
        public void SetProxyInInstance()
        {
            Program.logger.Debug("Начинаем установку прокси в инстанс.");
            AddProxyInDB();
            GetProxyFromDB();

            if (CheckProxy())
            {
                Program.logger.Debug("Устанавливаем прокси в инстанс.");
                instance.ClearProxy();
                instance.SetProxy(ProxyLine, false, true, true, true);
                project.SendInfoToLog("Назначили прокси: " + ProxyLine.Split('@')[1], true);
                Program.logger.Info("Установили прокси: " + ProxyLine.Split('@')[1] + " в инстанс.");
                CheckIp();
            }
            else
            {
                ChangeStatusProxyInDB("DEATH");
                project.SendErrorToLog("Прокси " + ProxyLine.Split('@')[1] + "  мертвый, пробуем взять другой.", true);
                Program.logger.Warn("Прокси " + ProxyLine.Split('@')[1] + "  мертвый, пробуем взять другой.");
                ProxyLine = null;
                ProxyChangeIpUrl = null;
                Program.logger.Debug("Обнулили строку с прокси и строку с URL для смены IP.");
                SetProxyInInstance();
            }
        }//Назначить прокси из БД в инстанс
        public void AddProxyInDB()
        {
            Program.logger.Debug("Добавляем прокси из входных настроек в БД.");

            foreach (string proxy in DataBaseProfileAndProxyValue.MyProxyList)
            {
                SQLiteConnection sqliteConnection = new DB().OpenConnectDb();
                string ProfileStringRequest = String.Format("INSERT INTO Proxy (ProxyLine, Status, ProxyChangeIpUrl) VALUES('{0}', 'Free', '{1}')", proxy.Split('|')[0], proxy.Split('|')[1]);
                SQLiteCommand sQLiteCommand = new SQLiteCommand(ProfileStringRequest, sqliteConnection);
                sQLiteCommand.ExecuteReader();
                sqliteConnection.Close();
            }

            Program.logger.Info("Успешно добавили прокси из входных настроек в БД.");
            project.SendInfoToLog("Добавили прокси из входных настроек в БД.", true);
        }//Добавление прокси в БД
        public void GetProxyFromDB()
        {
            lock (LockList)
            {
                Program.logger.Debug("Получаем строку с прокси из БД.");
                SQLiteConnection sqliteConnection = new DB().OpenConnectDb();
                string ProfileStringRequest = "SELECT ProxyLine, ProxyChangeIpUrl FROM Proxy WHERE Status = 'Free' ORDER BY ProxyLine ASC LIMIT 1";
                SQLiteCommand sQLiteCommand = new SQLiteCommand(ProfileStringRequest, sqliteConnection);

                try
                {
                    SQLiteDataReader reader = sQLiteCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        ProxyLine = reader.GetValue(0).ToString();
                        Program.logger.Debug("Строка с прокси: " + ProxyLine);
                        ProxyChangeIpUrl = reader.GetValue(1).ToString();
                        Program.logger.Debug("Строка с URL для смены IP: " + ProxyChangeIpUrl);
                    }

                    if (String.IsNullOrEmpty(ProxyLine))
                    {
                        sqliteConnection.Close();
                        Program.logger.Error("Нету свободной прокси.");
                        throw new Exception("Нету свободной прокси.");
                    }
                }
                catch (Exception ex)
                {
                    sqliteConnection.Close();
                    Program.logger.Error("Ошибка при попытке получить прокси из БД: " + ex.Message);
                    throw new Exception("Ошибка при попытке получить прокси из БД: " + ex.Message);
                }

                sqliteConnection.Close();
                project.SendInfoToLog("Получили прокси из БД: " + ProxyLine, true);
                Program.logger.Info("Получили прокси из БД: " + ProxyLine);
                ChangeStatusProxyInDB("Busy");
            }
        }//Взять прокси из БД
        public bool CheckProxy()
        {
            Program.logger.Debug("Начинаем проверку прокси: " + ProxyLine);
            var resultHttpGet = ZennoPoster.HttpGet("http://www.google.com", ProxyLine, "UTF-8",
                ZennoLab.InterfacesLibrary.Enums.Http.ResponceType.HeaderOnly);

            if (resultHttpGet.ToString().Length == 0 || (resultHttpGet.ToString().Substring(8, 3) == "502"))
            {
                CounterCheckProxy++;

                if (CounterCheckProxy != 10)
                {
                    project.SendWarningToLog("Прокси не прошел проверку, ждем 5 секунд и проверяем еще раз.", true);
                    Program.logger.Debug("Прокси: " + ProxyLine + " не прошел проверку, ждем 5 секунд и проверяем еще раз.");
                    Thread.Sleep(5000);
                    CheckProxy();
                }

                Program.logger.Warn("Прокси: " + ProxyLine + " не прошел проверку.");
                return false;
            }
            else
            {
                Program.logger.Info("Прокси: " + ProxyLine + " прошел проверку.");
                return true;
            }
        }//Проверка прокси
        public void CheckIp()
        {
            Program.logger.Debug("Начинаем проверку IP у прокси: " + ProxyLine);
            string ipv6 = "https://ipv6-internet.yandex.net/api/v0/ip";
            string ipv4 = "https://ipv4-internet.yandex.net/api/v0/ip";
            var resultHttpGetipv6 = ZennoPoster.HttpGet(ipv6, ProxyLine, "UTF-8",
                ZennoLab.InterfacesLibrary.Enums.Http.ResponceType.BodyOnly);

            if (String.IsNullOrEmpty(resultHttpGetipv6))
            {
                var resultHttpGetipv4 = ZennoPoster.HttpGet(ipv4, ProxyLine, "UTF-8",
                 ZennoLab.InterfacesLibrary.Enums.Http.ResponceType.BodyOnly);
                project.SendInfoToLog("Текущий IpV4: " + resultHttpGetipv4.Split('"')[1], true);
                Program.logger.Info("Текущий IpV4 у прокси: {0} - {1}", ProxyLine, resultHttpGetipv4.Split('"')[1]);

                if (String.IsNullOrEmpty(resultHttpGetipv4))
                {
                    project.SendWarningToLog("Не удалось определить Ip", true);
                    Program.logger.Warn("Не удалось определить IP у прокси: {0}", ProxyLine);
                    return;
                }
                return;
            }

            project.SendInfoToLog("Текущий IpV6: " + resultHttpGetipv6.Split('"')[1], true);
            Program.logger.Info("Текущий IpV6 у прокси: {0} - {1}", ProxyLine, resultHttpGetipv6.Split('"')[1]);
        }//Проверка Ip
        public void ChangeStatusProxyInDB(string Status)
        {
            Program.logger.Debug("Меняем статус прокси {0} на: {1}",ProxyLine.Split('@')[1], Status);
            SQLiteConnection sqliteConnection = new DB().OpenConnectDb();
            string ProfileStringRequest = String.Format("UPDATE Proxy SET Status = '{1}' WHERE ProxyLine = '{0}'", ProxyLine.Split('@')[1], Status);
            SQLiteCommand sQLiteCommand = new SQLiteCommand(ProfileStringRequest, sqliteConnection);
            sQLiteCommand.ExecuteReader();
            sqliteConnection.Close();
            project.SendInfoToLog("Сменили статус прокси на: " + Status, true);
            Program.logger.Info("Успешно сменили статус прокси {0} на: {1}", ProxyLine.Split('@')[1], Status);
        }//Смена статуса прокси (Free или Busy)
        public void ChangeIp()
        {
            try
            {
                Program.logger.Debug("Начинаем процесс смены IP у прокси: {0} с помощью URL: {1}", ProxyLine.Split('@')[1], ProxyChangeIpUrl);
                WebRequest wrGETURL = WebRequest.Create(ProxyChangeIpUrl);
                string otvet = wrGETURL.GetResponse().ToString();
                project.SendInfoToLog("Сделали запрос на смену IP", true);
                Thread.Sleep(10000);
                Program.logger.Info("Успешная смена IP у прокси: {0} с помощью URL: {1}", ProxyLine.Split('@')[1], ProxyChangeIpUrl);
            }

            catch(Exception ex)
            {
                project.SendErrorToLog("Не удалось сменить IP: " + ex.Message);
                Program.logger.Error("Не удалось сменить IP: " + ex.Message);
                throw new Exception("Не удалось сменить IP: " + ex.Message);
            }
        }//Смена Ip
    }
}
