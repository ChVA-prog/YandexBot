﻿using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using System.Data.SQLite;
using System;
using System.Threading;
using ZennoPosterProject1;

namespace ZennoPosterDataBaseAndProfile
{
     class Profile
     {
        readonly Instance instance;
        readonly IZennoPosterProjectModel project;

        public Profile(Instance instance, IZennoPosterProjectModel project)
        {
            this.instance = instance;
            this.project = project;
        }

        public void CheckAndCreateNewProfile()
        {           
            if(GetCountProfileInDB() < DataBaseAndProfileValue.CountFreeProfileInDB)
            {
                project.SendInfoToLog("Количество свободных профилей в базе данных меньше "+ project.Variables["set_CountFreeProfileInDB"].Value
                    + System.Environment.NewLine + ", создаем профиль и сохраняем его",true);

                if(!project.Profile.UserAgent.ToLower().Contains("android"))
                {
                    project.SendErrorToLog("Юзерагент не подходит. Прекращаем работу",true);
                    Environment.Exit(0);
                }
                else
                {                    
                    string PathToSaveProfile = DataBaseAndProfileValue.PathToFolderProfile + @"\" + project.Profile.NickName + ".zpprofile";
                    project.Profile.Save(PathToSaveProfile, true, true, true, true, true, true, true, true, true, null);

                    SaveProfileDataToDB(PathToSaveProfile);

                    project.SendInfoToLog("Сохранили профиль : " + project.Profile.NickName + " в БД",true);
                    Thread.Sleep(2000);
                }                             
            }
        }//Проверка количества профилей и создание новых если их не достаточно

        public int GetCountProfileInDB()
        {
            SQLiteConnection sqliteConnection = new DB().OpenConnectDb();
            SQLiteCommand sQLiteCommand = new SQLiteCommand(sqliteConnection) {CommandText = "SELECT COUNT(*) FROM Profiles WHERE Status = 'Free' OR Status = 'Busy'" };
           
            object CountProfile = sQLiteCommand.ExecuteScalar();

            sqliteConnection.Close();

            project.SendInfoToLog("Количество профилей в базе данный: " + CountProfile.ToString());
            return Convert.ToInt32(CountProfile);
        }//Получение количества профилей для работы из БД

        public void SaveProfileDataToDB(string PathTosave)
        {
            SQLiteConnection sqliteConnection = new DB().OpenConnectDb();

            string ProfileStringRequest = String.Format("INSERT INTO Profiles(PathToProfile, TimeToGetYandex, CountSession, CountSessionDay, TimeToNextGetYandex, Status) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}')",
                PathTosave, DateTime.UtcNow.AddDays(3).ToString("dd-MM-yyyy HH-mm-ss"), 0, 0, DateTime.UtcNow.ToString("dd-MM-yyyy HH-mm-ss"), "Free");

            SQLiteCommand sQLiteCommand = new SQLiteCommand(ProfileStringRequest, sqliteConnection);

            sQLiteCommand.ExecuteReader();

            sqliteConnection.Close();            
        }//Сохранение профиля в БД

        public bool GetProfileFromDB()
        {
            SQLiteConnection sqliteConnection = new DB().OpenConnectDb();

            string ProfileStringRequest = String.Format("SELECT PathToProfile, CountSession, CountSessionDay FROM Profiles WHERE Status = 'Free' AND TimeToGetYandex > '{0}' AND TimeToNextGetYandex < '{1}' ORDER BY CountSessionDay ASC LIMIT 1",
                DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss"), DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss"));

            SQLiteCommand sQLiteCommand = new SQLiteCommand(ProfileStringRequest, sqliteConnection);

            try
            {
                SQLiteDataReader reader = sQLiteCommand.ExecuteReader();
                while (reader.Read())
                {
                    DataBaseAndProfileValue.PathToProfile = reader.GetValue(0).ToString();
                    DataBaseAndProfileValue.CountSession = Convert.ToInt32(reader.GetValue(1).ToString());
                    DataBaseAndProfileValue.CountSessionDay = Convert.ToInt32(reader.GetValue(2).ToString());
                }
                if(String.IsNullOrEmpty(DataBaseAndProfileValue.PathToProfile))
                {
                    sqliteConnection.Close();
                    return false;
                }
            }
            catch(Exception ex)
            {
                project.SendErrorToLog("Ошибка при попытке получить профиль из БД: " + ex.Message, true);
                new AdditionalMethods(instance, project).ErrorExit();
            }

            sqliteConnection.Close();
            project.SendInfoToLog("Взяли профиль из БД", true);
            UpdateStatusProfile("Busy");

            return true;
        }//Получение данных профиля из БД

        public void UpdateStatusProfile(string Status)
        {
            SQLiteConnection sqliteConnection = new DB().OpenConnectDb();

            string ProfileStringRequest = String.Format("UPDATE Profiles SET Status = '{1}' WHERE PathToProfile = '{0}'",
                DataBaseAndProfileValue.PathToProfile,Status);

            SQLiteCommand sQLiteCommand = new SQLiteCommand(ProfileStringRequest, sqliteConnection);

            sQLiteCommand.ExecuteReader();

            sqliteConnection.Close();
            project.SendErrorToLog("Изменили статус профиля на: " + Status, true);
        }//Изменение статуса профиля (Free или Busy) 

        public void UpdateStatusProfile(string Status, int CountSession, int CountSessionDay)
        {
            SQLiteConnection sqliteConnection = new DB().OpenConnectDb();

            string ProfileStringRequest = String.Format("UPDATE Profiles SET Status = '{1}', CountSession = '{2}', CountSessionDay = '{3}', TimeToNextGetYandex = '{4}' " +
                "WHERE PathToProfile = '{0}'", DataBaseAndProfileValue.PathToProfile, Status, CountSession, CountSessionDay,DateTime.Now.AddHours(3).ToString("dd-MM-yyyy HH-mm"));

            SQLiteCommand sQLiteCommand = new SQLiteCommand(ProfileStringRequest, sqliteConnection);

            sQLiteCommand.ExecuteReader();

            sqliteConnection.Close();
            project.SendErrorToLog("Изменили статус профиля на: " + Status + " Количество сессий на: " + CountSession
                + Environment.NewLine + " Количество сессий в день на: " + CountSessionDay, true);
        }//Изменение статуса профиля (Free или Busy), кол-во сессий и кол-во сессий в день

        public void DownloadProfileInZennoposter()
        {
            CheckAndCreateNewProfile();
            if (!GetProfileFromDB())
            {
                project.SendErrorToLog("В БД не нашелся нужный профиль",true);
                new AdditionalMethods(instance, project).ErrorExit();
            }

            project.Profile.Load(DataBaseAndProfileValue.PathToProfile);
            project.SendInfoToLog("Назначили профиль " + project.Profile.NickName + " в проект", true);
        }//Загрузка профиля в зенопостер
    }
}