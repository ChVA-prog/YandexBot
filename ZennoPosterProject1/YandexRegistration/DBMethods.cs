﻿using System;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using System.Data.SQLite;

namespace ZennoPosterYandexRegistration
{
    class DBMethods : YandexRegistrationValue
    {
        private string PathToProfile { get; set; }
        public static object LockList = new object();

        readonly IZennoPosterProjectModel project;
        readonly Instance instance;
        public DBMethods(Instance instance, IZennoPosterProjectModel project) : base(project)
        {
            this.instance = instance;
            this.project = project;
        }

        public void GetCountProfileInDB()
        {
            SQLiteConnection sqliteConnection = new DataBaseProfileAndProxy.DB().OpenConnectDb();
            SQLiteCommand sQLiteCommand = new SQLiteCommand(sqliteConnection) { CommandText = String.Format("SELECT COUNT(*) FROM Profiles WHERE Status = 'Free' AND YandexRegistration = 'NO' AND TimeToGetYandex < '{0}'",DateTime.Now.ToString("yyyy-MM-dd")) };

            object CountProfile = sQLiteCommand.ExecuteScalar();

            if (Convert.ToInt32(CountProfile) < 1)
            {
                sqliteConnection.Close();
                throw new Exception("В базе данных нету подходящих профилей");
            }

            sqliteConnection.Close();
            project.SendInfoToLog("Количество профилей в базе данных для регистрации: " + CountProfile.ToString());
        }//Получение количества профилей для работы из БД
        public void GetProfileFromDB()
        {
            SQLiteConnection sqliteConnection = new DataBaseProfileAndProxy.DB().OpenConnectDb();

            string ProfileStringRequest = String.Format("SELECT PathToProfile FROM Profiles WHERE Status = 'Free' AND TimeToGetYandex < '{0}' AND YandexRegistration = 'NO' ORDER BY Status ASC LIMIT 1",
                DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"));

            SQLiteCommand sQLiteCommand = new SQLiteCommand(ProfileStringRequest, sqliteConnection);

            try
            {
                SQLiteDataReader reader = sQLiteCommand.ExecuteReader();
                while (reader.Read())
                {
                    PathToProfile = reader.GetValue(0).ToString();
                }
                
            }
            catch (Exception ex)
            {               
                throw new Exception("Ошибка при попытке получить профиль из БД: " + ex.Message);
            }

            sqliteConnection.Close();
            project.SendInfoToLog("Взяли профиль из БД", true);
            UpdateStatusProfile("Busy");
        }//Получение данных профиля из БД
        public void DownloadProfileInZennoposter()
        {
            lock (LockList)
            {
                GetCountProfileInDB();
                GetProfileFromDB();

                project.Profile.Load(PathToProfile);
                project.SendInfoToLog("Назначили профиль " + PathToProfile + " в проект", true);
            }
        }//Загрузка профиля в зенопостер
        public void UpdateStatusProfile(string MailStatus, string Mail, string MailPassword, string MailPasswordIMAP)
        {
            SQLiteConnection sqliteConnection = new DataBaseProfileAndProxy.DB().OpenConnectDb();

            string ProfileStringRequest = String.Format(
                "UPDATE Profiles " +
                "SET MailStatus = '{1}'," +
                " Mail = '{2}'," +
                " MailPassword = '{3}'," +
                " MailPassowrdIMAP = '{4}'," +
                " DateLastEnterYandex = '{5}'" +
                " WHERE PathToProfile = '{0}'",
                PathToProfile,
                MailStatus,
                Mail,
                MailPassword,
                MailPasswordIMAP,
                DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"));

            SQLiteCommand sQLiteCommand = new SQLiteCommand(ProfileStringRequest, sqliteConnection);

            sQLiteCommand.ExecuteReader();
            project.SendInfoToLog("Сохранили привязанную почту аккаунта в БД");
            sqliteConnection.Close();
        }//Изменение статуса регистрации в яндексе
        public void UpdateStatusProfile(string YandexRegistration, string YandexLogin, string YandexPassword)
        {
            SQLiteConnection sqliteConnection = new DataBaseProfileAndProxy.DB().OpenConnectDb();

            string ProfileStringRequest = String.Format(
                "UPDATE Profiles " +
                "SET YandexRegistration = '{1}'," +
                " DateYandexRegistration = '{2}'," +
                " YandexLogin = '{3}'," +
                " YandexPassword = '{4}'," +
                " DateLastEnterYandex = '{5}'" +
                " WHERE PathToProfile = '{0}'",
                PathToProfile,
                YandexRegistration,
                DateTime.Now.ToString("yyyy-MM-dd"),
                YandexLogin,
                YandexPassword,
                DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"));

            SQLiteCommand sQLiteCommand = new SQLiteCommand(ProfileStringRequest, sqliteConnection);

            sQLiteCommand.ExecuteReader();
            project.SendInfoToLog("Сохранили регистрацию аккаунта в БД");
            sqliteConnection.Close();
        }//Изменение статуса регистрации в яндексе
        public void UpdateStatusProfile(string SettingsAccount, string Status)
        {
            SQLiteConnection sqliteConnection = new DataBaseProfileAndProxy.DB().OpenConnectDb();

            string ProfileStringRequest = String.Format(
                "UPDATE Profiles" +
                " SET SettingsAccount = '{1}'," +
                " DateLastEnterYandex = '{2}'," +
                " Status = '{3}' " +
                "WHERE PathToProfile = '{0}'",
                PathToProfile,
                SettingsAccount,
                DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"),
                Status);

            SQLiteCommand sQLiteCommand = new SQLiteCommand(ProfileStringRequest, sqliteConnection);

            sQLiteCommand.ExecuteReader();
            project.SendInfoToLog("Сменили статус профиля на Free, поставили отметку о том что акк заполнен.");
            sqliteConnection.Close();
        }//Изменение статуса настроек акка в яндексе
        public void UpdateStatusProfile(string Status)
        {
            SQLiteConnection sqliteConnection = new DataBaseProfileAndProxy.DB().OpenConnectDb();
            string ProfileStringRequest = String.Format(
                "UPDATE Profiles" +
                " SET Status = '{1}'" +
                " WHERE PathToProfile = '{0}'",
                PathToProfile,
                Status);

            SQLiteCommand sQLiteCommand = new SQLiteCommand(ProfileStringRequest, sqliteConnection);

            sQLiteCommand.ExecuteReader();
            sqliteConnection.Close();
            project.SendInfoToLog("Изменили статус профиля на: " + Status, true);
        }//Изменение статуса профиля (Free или Busy) 

        
    }
}
