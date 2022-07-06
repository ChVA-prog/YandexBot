using ZennoLab.InterfacesLibrary.ProjectModel;
using System.Data.SQLite;
using System;
using System.Threading;

namespace ZennoPosterDataBaseAndProfile
{
     class Profile : DataBaseAndProfileValue
    {
        private int CountSession { get; set; }
        private int CountSessionDay { get; set; }
        private string PathToProfile { get; set; }

        readonly IZennoPosterProjectModel project;

        public Profile(IZennoPosterProjectModel project) : base(project)
        {
            this.project = project;
        }

        private void CheckAndCreateNewProfile()
        {
            if(GetCountProfileInDB() < CountFreeProfileInDB)
            {
                project.SendInfoToLog("Количество свободных профилей в базе данных меньше "+ project.Variables["set_CountFreeProfileInDB"].Value
                    + System.Environment.NewLine + ", создаем профиль и сохраняем его",true);

                if(!project.Profile.UserAgent.ToLower().Contains("android"))
                {
                    throw new Exception("Юзерагент не подходит. Прекращаем работу");
                }
                else
                {                    
                    string PathToSaveProfile = PathToFolderProfile + @"\" + project.Profile.NickName + ".zpprofile";
                    project.Profile.Save(PathToSaveProfile, true, true, true, true, true, true, true, true, true, null);
                    SaveProfileDataToDB(PathToSaveProfile);
                    project.SendInfoToLog("Сохранили профиль : " + project.Profile.NickName + " в БД",true);
                    Thread.Sleep(2000);
                }                             
            }
        }//Проверка количества профилей и создание новых если их не достаточно
        private int GetCountProfileInDB()
        {
            SQLiteConnection sqliteConnection = new DB(project).OpenConnectDb();
            SQLiteCommand sQLiteCommand = new SQLiteCommand(sqliteConnection)
            {CommandText = "SELECT COUNT(*) FROM Profiles WHERE Status = 'Free' OR Status = 'Busy'"};
           
            object CountProfile = sQLiteCommand.ExecuteScalar();

            sqliteConnection.Close();

            project.SendInfoToLog("Количество профилей в базе данный: " + CountProfile.ToString(),true);
           
            return Convert.ToInt32(CountProfile);
        }//Получение количества профилей для работы из БД
        private void SaveProfileDataToDB(string PathTosave)
        {
            SQLiteConnection sqliteConnection = new DB(project).OpenConnectDb();

            string ProfileStringRequest = String.Format("INSERT INTO Profiles(PathToProfile, TimeToGetYandex, CountSession, CountSessionDay, TimeToNextGetYandex, Status, DateLastEnterYandex, YandexRegistration) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                PathTosave, DateTime.UtcNow.AddDays(3).ToString("yyy-MM-dd HH-mm-ss"), 0, 0, DateTime.UtcNow.ToString("yyyy-MM-dd HH-mm-ss"), "Free", DateTime.MinValue.ToString("yyyy-MM-dd"), "NO");

            SQLiteCommand sQLiteCommand = new SQLiteCommand(ProfileStringRequest, sqliteConnection);

            sQLiteCommand.ExecuteReader();

            sqliteConnection.Close();
        }//Сохранение профиля в БД
        private void GetProfileFromDB()
        {
            SQLiteConnection sqliteConnection = new DB(project).OpenConnectDb();

            string ProfileStringRequest = String.Format("SELECT PathToProfile, CountSession, CountSessionDay, DateLastEnterYandex FROM Profiles WHERE Status = 'Free' AND TimeToGetYandex > '{0}' AND TimeToNextGetYandex < '{1}' AND CountSessionDay < {2} AND CountSession < 4 ORDER BY CountSessionDay ASC LIMIT 1",
                DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"), DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"), CountSessionDayLimit);

            SQLiteCommand sQLiteCommand = new SQLiteCommand(ProfileStringRequest, sqliteConnection);

            try
            {
                SQLiteDataReader reader = sQLiteCommand.ExecuteReader();

                while (reader.Read())
                {
                    PathToProfile = reader.GetValue(0).ToString();
                    CountSession = Convert.ToInt32(reader.GetValue(1).ToString());
                    CountSessionDay = Convert.ToInt32(reader.GetValue(2).ToString());

                    if (CountSessionDay != 0 && (Convert.ToDateTime(reader.GetString(3)) < DateTime.Now.Date))
                    {
                        UpdateCountSessionDay(sqliteConnection);
                    }
                }
                if (String.IsNullOrEmpty(PathToProfile))
                {
                    throw new Exception("В БД нету подходящих профилей");
                }
            }
            catch(Exception ex)
            {
                sqliteConnection.Close();
                throw new Exception("Ошибка при попытке получить профиль из БД: " + ex.Message);
            }
            UpdateStatusProfile("Busy", sqliteConnection);
            sqliteConnection.Close();
            project.SendInfoToLog("Взяли профиль из БД", true);
        }//Получение данных профиля из БД       
        public void UpdateStatusProfile(string Status)
        {
            SQLiteConnection sqliteConnection = new DB(project).OpenConnectDb();
            string ProfileStringRequest = String.Format("UPDATE Profiles SET Status = '{1}', TimeToNextGetYandex = '{2}' WHERE PathToProfile = '{0}'",
                PathToProfile, Status, DateTime.Now.AddHours(3).ToString("yyyy-MM-dd HH-mm-ss"));

            SQLiteCommand sQLiteCommand = new SQLiteCommand(ProfileStringRequest, sqliteConnection);

            sQLiteCommand.ExecuteReader();
            sqliteConnection.Close();
            project.SendInfoToLog("Изменили статус профиля на: " + Status, true);
        }//Изменение статуса профиля (Free или Busy) 
        private void UpdateStatusProfile(string Status, SQLiteConnection sqliteConnection)
        {
            string ProfileStringRequest = String.Format("UPDATE Profiles SET Status = '{1}' WHERE PathToProfile = '{0}'",
                PathToProfile, Status);

            SQLiteCommand sQLiteCommand = new SQLiteCommand(ProfileStringRequest, sqliteConnection);

            sQLiteCommand.ExecuteReader();
            project.SendInfoToLog("Изменили статус профиля на: " + Status, true);
        }//Изменение статуса профиля (Free или Busy) 
        public void UpdateStatusProfile(string Status, int Session, int SessionDay)
        {
            CountSession = CountSession + Session;
            CountSessionDay = CountSessionDay + SessionDay;

            SQLiteConnection sqliteConnection = new DB(project).OpenConnectDb();

            string ProfileStringRequest = String.Format("UPDATE Profiles SET Status = '{1}', CountSession = '{2}', CountSessionDay = '{3}', TimeToNextGetYandex = '{4}', DateLastEnterYandex = '{5}' " +
                "WHERE PathToProfile = '{0}'", PathToProfile, Status, CountSession, CountSessionDay,DateTime.Now.AddHours(3).ToString("yyyy-MM-dd HH-mm"), DateTime.Now);

            SQLiteCommand sQLiteCommand = new SQLiteCommand(ProfileStringRequest, sqliteConnection);

            sQLiteCommand.ExecuteReader();

            sqliteConnection.Close();
            project.SendInfoToLog("Изменили статус профиля на: " + Status + " Количество сессий на: " + CountSession
                + " Количество сессий в день на: " + CountSessionDay, true);
        }//Изменение статуса профиля (Free или Busy), кол-во сессий и кол-во сессий в день
        public void DownloadProfileInZennoposter()
        {
            CheckAndCreateNewProfile();
            GetProfileFromDB();           

            project.Profile.Load(PathToProfile);
            project.SendInfoToLog("Назначили профиль " + PathToProfile + " в проект", true);
        }//Загрузка профиля в зенопостер
        private void UpdateCountSessionDay(SQLiteConnection sqliteConnection)
        {
            CountSessionDay = 0;
            string ProfileStringRequest = String.Format("UPDATE Profiles SET CountSessionDay = '0' WHERE PathToProfile = '{0}'",
                PathToProfile);

            SQLiteCommand sQLiteCommand = new SQLiteCommand(ProfileStringRequest, sqliteConnection);

            sQLiteCommand.ExecuteReader();

            project.SendWarningToLog("Обнулили количество дневных сессий", true);
        }//Обнуление дневных сессий профиля
        public void SaveProfile()
        {
            string PathToSaveProfile = PathToFolderProfile + @"\" + project.Profile.NickName + ".zpprofile";
            project.Profile.Save(PathToSaveProfile, false, true, true, true, true, true, true, true, true, null);
            project.SendInfoToLog("Сохранили профиль: " + PathToSaveProfile, true);
        }//Сохранение профиля       
     }
}
