using System;
using System.Collections.Generic;
using System.Linq;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoPosterEmulation;
using System.Threading;
using ZennoPosterSiteWalk;
using ZennoPosterProject1;
using ZennoPosterYandexWalk;
using ZennoPosterYandexRegistrationSmsServiceSmsHubOrg;
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

        public void GetProfileFromDb()
        {

        }

        public int GetCountProfileInDB()
        {
            SQLiteConnection sqliteConnection = new ZennoPosterDataBaseAndProfile.DB().OpenConnectDb();
            SQLiteCommand sQLiteCommand = new SQLiteCommand(sqliteConnection) { CommandText = "SELECT COUNT(*) FROM Profiles WHERE Status = 'Free' AND YandexRegistration = 'NO'" };

            object CountProfile = sQLiteCommand.ExecuteScalar();

            sqliteConnection.Close();

            project.SendInfoToLog("Количество профилей в базе данных для регистрации: " + CountProfile.ToString());
            return Convert.ToInt32(CountProfile);
        }//Получение количества профилей для работы из БД

        public bool GetProfileFromDB()
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
                project.SendErrorToLog("Ошибка при попытке получить профиль из БД: " + ex.Message, true);
                new AdditionalMethods(instance, project).ErrorExit();
            }

            sqliteConnection.Close();
            project.SendInfoToLog("Взяли профиль из БД", true);
            new ZennoPosterDataBaseAndProfile.Profile(instance,project).UpdateStatusProfile("Busy");
            return true;
        }//Получение данных профиля из БД
        public void DownloadProfileInZennoposter()
        {
            GetCountProfileInDB();
            if (!GetProfileFromDB())
            {
                project.SendErrorToLog("В БД не нашелся нужный профиль", true);
                new AdditionalMethods(instance, project).ErrorExit();
            }
            project.Profile.Load(ZennoPosterDataBaseAndProfile.DataBaseAndProfileValue.PathToProfile);
            project.SendInfoToLog("Назначили профиль " + project.Profile.NickName + " в проект", true);
        }//Загрузка профиля в зенопостер

        public void UpdateStatusProfile(string Status, string YandexRegistration)
        {
            SQLiteConnection sqliteConnection = new ZennoPosterDataBaseAndProfile.DB().OpenConnectDb();

            string ProfileStringRequest = String.Format("UPDATE Profiles SET Status = '{1}', YandexRegistration = '{2}', DateYandexRegistration = '{3}', YandexLogin = '{4}', YandexPassword = '{4}' " +
                "WHERE PathToProfile = '{0}'", ZennoPosterDataBaseAndProfile.DataBaseAndProfileValue.PathToProfile, Status, YandexRegistration, DateTime.Now.ToString("dd.MM.yyyy"), YandexRegistrationValue.YandexLogin, YandexRegistrationValue.YandexPassword);

            SQLiteCommand sQLiteCommand = new SQLiteCommand(ProfileStringRequest, sqliteConnection);

            sQLiteCommand.ExecuteReader();

            sqliteConnection.Close();
            project.SendInfoToLog("Изменили статус профиля на: " + Status, true);
        }//Изменение статуса профиля (Free или Busy), кол-во сессий и кол-во сессий в день



    }
}
