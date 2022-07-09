﻿using System;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using System.Threading;
using System.Data.SQLite;
using System.Net;

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
            AddProxyInDB();
            GetProxyFromDB();
            if (CheckProxy())
            {
                instance.ClearProxy();
                instance.SetProxy(ProxyLine, false, true, true, true);
                project.SendInfoToLog("Назначили прокси: " + ProxyLine.Split('@')[1], true);
                CheckIp();
            }
            else
            {
                ChangeStatusProxyInDB("DEATH");

                project.SendErrorToLog("Прокси " + ProxyLine.Split('@')[1] + "  мертвый, пробуем взять другой", true);

                ProxyLine = null;
                SetProxyInInstance();
            }
        }//Назначить прокси из БД в инстанс
        public void AddProxyInDB()
        {         
            foreach (string proxy in DataBaseProfileAndProxyValue.MyProxyList)
            {
                SQLiteConnection sqliteConnection = new DB().OpenConnectDb();

                string ProfileStringRequest = String.Format("INSERT INTO Proxy (ProxyLine, Status, ProxyChangeIpUrl) VALUES('{0}', 'Free', '{1}')", proxy.Split('|')[0], proxy.Split('|')[1]);

                SQLiteCommand sQLiteCommand = new SQLiteCommand(ProfileStringRequest, sqliteConnection);

                sQLiteCommand.ExecuteReader();
                sqliteConnection.Close();
            }
                
            project.SendInfoToLog("Добавили прокси из входных настроек в БД", true);
        }//Добавление прокси в БД
        public void GetProxyFromDB()
        {
            lock (LockList)
            {
                SQLiteConnection sqliteConnection = new DB().OpenConnectDb();

                string ProfileStringRequest = "SELECT ProxyLine, ProxyChangeIpUrl FROM Proxy WHERE Status = 'Free' ORDER BY ProxyLine ASC LIMIT 1";
                SQLiteCommand sQLiteCommand = new SQLiteCommand(ProfileStringRequest, sqliteConnection);
                try
                {
                    SQLiteDataReader reader = sQLiteCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        ProxyLine = reader.GetValue(0).ToString();
                        ProxyChangeIpUrl = reader.GetValue(1).ToString();
                    }

                    if (String.IsNullOrEmpty(ProxyLine))
                    {
                        sqliteConnection.Close();
                        throw new Exception("Нету свободной прокси");
                    }
                }

                catch (Exception ex)
                {
                    sqliteConnection.Close();
                    throw new Exception("Ошибка при попытке получить прокси из БД: " + ex.Message);
                }

                sqliteConnection.Close();
                project.SendInfoToLog("Взяли прокси из базы данных: " + ProxyLine, true);
                ChangeStatusProxyInDB("Busy");
            }
        }//Взять прокси из БД
        public bool CheckProxy()
        {
            var resultHttpGet = ZennoPoster.HttpGet("http://www.google.com", ProxyLine, "UTF-8",
                ZennoLab.InterfacesLibrary.Enums.Http.ResponceType.HeaderOnly);
            if (resultHttpGet.ToString().Length == 0 || (resultHttpGet.ToString().Substring(8, 3) == "502"))
            {
                CounterCheckProxy++;
                if (CounterCheckProxy != 10)
                {
                    project.SendWarningToLog("Прокси не прошел проверку на живучесть, ждем 5 секунд и проверяем еще раз", true);
                    Thread.Sleep(5000);
                    CheckProxy();
                }
                return false;
            }
            else
            {
                return true;
            }
        }//Проверка прокси
        public void CheckIp()
        {
            string ipv6 = "https://ipv6-internet.yandex.net/api/v0/ip";
            string ipv4 = "https://ipv4-internet.yandex.net/api/v0/ip";

            var resultHttpGetipv6 = ZennoPoster.HttpGet(ipv6, ProxyLine, "UTF-8",
                ZennoLab.InterfacesLibrary.Enums.Http.ResponceType.BodyOnly);

            if (String.IsNullOrEmpty(resultHttpGetipv6))
            {
                var resultHttpGetipv4 = ZennoPoster.HttpGet(ipv4, ProxyLine, "UTF-8",
                 ZennoLab.InterfacesLibrary.Enums.Http.ResponceType.BodyOnly);

                project.SendInfoToLog("Текущий IpV4: " + resultHttpGetipv4.Split('"')[1], true);
                if (String.IsNullOrEmpty(resultHttpGetipv4))
                {
                    project.SendWarningToLog("Не удалось определить Ip", true);
                    return;
                }
                return;
            }
            project.SendInfoToLog("Текущий IpV6: " + resultHttpGetipv6.Split('"')[1], true);
        }//Проверка Ip
        public void ChangeStatusProxyInDB(string Status)
        {           
            SQLiteConnection sqliteConnection = new DB().OpenConnectDb();

            string ProfileStringRequest = String.Format("UPDATE Proxy SET Status = '{1}' WHERE ProxyLine = '{0}'", ProxyLine, Status);

            SQLiteCommand sQLiteCommand = new SQLiteCommand(ProfileStringRequest, sqliteConnection);

            sQLiteCommand.ExecuteReader();
            sqliteConnection.Close();
            project.SendInfoToLog("Сменили статус прокси на: " + Status, true);
        }//Смена статуса прокси (Free или Busy)
        public void ChangeIp()
        {
            try
            {
                WebRequest wrGETURL = WebRequest.Create(ProxyChangeIpUrl);
                string otvet = wrGETURL.GetResponse().ToString();
                project.SendInfoToLog("Сделали запрос на смену IP", true);
                Thread.Sleep(10000);
            }
            catch(Exception ex)
            {
                throw new Exception("Не удалось сменить IP: " + ex.Message);
            }
        }//Смена Ip
    }
}