﻿using System;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using System.Data.SQLite;

namespace ZennoPosterYandexRegistration
{
    class DBMethods
    {
        readonly IZennoPosterProjectModel project;
        readonly Instance instance;
        public DBMethods(Instance instance, IZennoPosterProjectModel project)
        {
            this.instance = instance;
            this.project = project;
        }

        public void GetCountProfileInDB()
        {
            SQLiteConnection sqliteConnection = new ZennoPosterDataBaseAndProfile.DB().OpenConnectDb();
            SQLiteCommand sQLiteCommand = new SQLiteCommand(sqliteConnection) { CommandText = String.Format("SELECT COUNT(*) FROM Profiles WHERE Status = 'Free' AND YandexRegistration = 'NO' AND TimeToGetYandex < '{0}'",DateTime.Now) };

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
            SQLiteConnection sqliteConnection = new ZennoPosterDataBaseAndProfile.DB().OpenConnectDb();

            string ProfileStringRequest = String.Format("SELECT PathToProfile FROM Profiles WHERE Status = 'Free' AND TimeToGetYandex < '{0}' AND YandexRegistration = 'NO' ORDER BY Status ASC LIMIT 1",
                DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss"));

            SQLiteCommand sQLiteCommand = new SQLiteCommand(ProfileStringRequest, sqliteConnection);

            try
            {
                SQLiteDataReader reader = sQLiteCommand.ExecuteReader();
                while (reader.Read())
                {
                    ZennoPosterDataBaseAndProfile.DataBaseAndProfileValue.PathToProfile = reader.GetValue(0).ToString();
                }
                
            }
            catch (Exception ex)
            {               
                throw new Exception("Ошибка при попытке получить профиль из БД: " + ex.Message);
            }

            sqliteConnection.Close();
            project.SendInfoToLog("Взяли профиль из БД", true);
            new ZennoPosterDataBaseAndProfile.Profile(project).UpdateStatusProfile("Busy");
        }//Получение данных профиля из БД
        public void DownloadProfileInZennoposter()
        {
            GetCountProfileInDB();
            GetProfileFromDB();

            project.Profile.Load(ZennoPosterDataBaseAndProfile.DataBaseAndProfileValue.PathToProfile);
            project.SendInfoToLog("Назначили профиль " + ZennoPosterDataBaseAndProfile.DataBaseAndProfileValue.PathToProfile + " в проект", true);
        }//Загрузка профиля в зенопостер
        public void UpdateStatusProfile(string Status, string YandexRegistration)
        {
            SQLiteConnection sqliteConnection = new ZennoPosterDataBaseAndProfile.DB().OpenConnectDb();

            string ProfileStringRequest = String.Format("UPDATE Profiles SET Status = '{1}', YandexRegistration = '{2}', DateYandexRegistration = '{3}', YandexLogin = '{4}', YandexPassword = '{5}', TimeToGetSettingAccount = '{6}' " +
                "WHERE PathToProfile = '{0}'", ZennoPosterDataBaseAndProfile.DataBaseAndProfileValue.PathToProfile, Status, YandexRegistration, DateTime.Now.ToString("dd.MM.yyyy"), YandexRegistrationValue.YandexLogin, YandexRegistrationValue.YandexPassword, DateTime.Now.AddDays(3).ToString("dd.MM.yyyy"));

            SQLiteCommand sQLiteCommand = new SQLiteCommand(ProfileStringRequest, sqliteConnection);

            sQLiteCommand.ExecuteReader();

            sqliteConnection.Close();
            project.SendInfoToLog("Изменили статус профиля на: " + Status, true);
        }//Изменение статуса регистрации в яндексе
        public void UpdateStatusProfile(string Status)
        {
            SQLiteConnection sqliteConnection = new ZennoPosterDataBaseAndProfile.DB().OpenConnectDb();
            string ProfileStringRequest = String.Format("UPDATE Profiles SET Status = '{1}' WHERE PathToProfile = '{0}'",
                ZennoPosterDataBaseAndProfile.DataBaseAndProfileValue.PathToProfile, Status);

            SQLiteCommand sQLiteCommand = new SQLiteCommand(ProfileStringRequest, sqliteConnection);

            sQLiteCommand.ExecuteReader();
            sqliteConnection.Close();
            project.SendInfoToLog("Изменили статус профиля на: " + Status, true);
        }//Изменение статуса профиля (Free или Busy) 
    }
}
