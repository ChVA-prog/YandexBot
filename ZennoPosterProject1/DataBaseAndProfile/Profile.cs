using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using System.Data.SQLite;
using System;

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
            new DataBaseAndProfileValue(instance, project);
        }

        public void CheckAndCreateNewProfile()
        {           
            if(GetCountProfileInDB() < DataBaseAndProfileValue.CountFreeProfileInDB)
            {
                project.SendInfoToLog("Количество профилей в базе данных меньше заданного значения, создаем профиль и сохраняем его");
                if(!project.Profile.UserAgent.ToLower().Contains("android"))
                {
                    project.SendInfoToLog("Юзерагент не подходит.");
                    return;
                }
                else
                {
                    

                    string PathToSaveProfile = DataBaseAndProfileValue.PathToFolderProfile + @"\" + project.Profile.NickName + ".zpprofile";
                    project.Profile.Save(PathToSaveProfile, true, true, true, true, true, true, true, true, true, null);

                    SaveProfileDataToDB(PathToSaveProfile);

                    project.SendInfoToLog("Создали профиль");
                }
                
                
            }
        }//Проверка количества профилей и создание новых если их не достаточно

        public int GetCountProfileInDB()
        {
            DB dB = new DB(instance, project);
            SQLiteConnection sqliteConnection = dB.OpenConnectDb();
            SQLiteCommand sQLiteCommand = new SQLiteCommand(sqliteConnection);

            sQLiteCommand.CommandText = "SELECT COUNT(*) FROM Profiles WHERE Status = 'Free' OR Status = 'Busy'";

            object CountProfile = sQLiteCommand.ExecuteScalar();

            sqliteConnection.Close();

            project.SendErrorToLog(CountProfile.ToString());

            return Convert.ToInt32(CountProfile);
        }//Получение количества профилей для работы из БД

        public void SaveProfileDataToDB(string PathTosave)
        {
            DB dB = new DB(instance, project);
            SQLiteConnection sqliteConnection = dB.OpenConnectDb();

            string ProfileStringRequest = String.Format("INSERT INTO Profiles(PathToProfile, TimeToGetYandex, CountSession, CountSessionDay, TimeToNextGetYandex, Status) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}')",
                PathTosave, DateTime.UtcNow.AddDays(3).ToString("dd-MM-yyyy HH-mm"), 0, 0, DateTime.UtcNow.ToString("dd-MM-yyyy HH-mm"), "Free");

            SQLiteCommand sQLiteCommand = new SQLiteCommand(ProfileStringRequest, sqliteConnection);

            sQLiteCommand.ExecuteReader();

            sqliteConnection.Close();
        }//Сохранение профиля в БД

        public bool GetProfileFromDB()
        {
            DB dB = new DB(instance, project);
            SQLiteConnection sqliteConnection = dB.OpenConnectDb();

            string ProfileStringRequest = String.Format("SELECT PathToProfile, CountSession, CountSessionDay FROM Profiles WHERE Status = 'Free' AND TimeToGetYandex < '{0}' ORDER BY CountSessionDay ASC LIMIT 1", DateTime.Now.ToString("dd-MM-yyyy HH-mm"));
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
            catch(Exception e)
            {
                project.SendErrorToLog("Ошибка при попытке получить профиль из БД");
                project.SendErrorToLog(e.Message);
                return false;
            }

            sqliteConnection.Close();
            UpdateStatusProfile("Busy");
            return true;
        }//Получение данных профиля из БД

        public void UpdateStatusProfile(string Status)
        {
            DB dB = new DB(instance, project);
            SQLiteConnection sqliteConnection = dB.OpenConnectDb();

            string ProfileStringRequest = String.Format("UPDATE Profiles SET Status = '{1}' WHERE PathToProfile = '{0}'",DataBaseAndProfileValue.PathToProfile,Status);

            SQLiteCommand sQLiteCommand = new SQLiteCommand(ProfileStringRequest, sqliteConnection);

            sQLiteCommand.ExecuteReader();

            sqliteConnection.Close();
        }//Изменение статуса профиля (Free или Busy) 

        public void UpdateStatusProfile(string Status, int CountSession, int CountSessionDay)
        {
            DB dB = new DB(instance, project);
            SQLiteConnection sqliteConnection = dB.OpenConnectDb();

            string ProfileStringRequest = String.Format("UPDATE Profiles SET Status = '{1}', CountSession = '{2}', CountSessionDay = '{3}', TimeToNextGetYandex = '{4}' " +
                "WHERE PathToProfile = '{0}'", DataBaseAndProfileValue.PathToProfile, Status, CountSession, CountSessionDay,DateTime.Now.AddHours(3).ToString("dd-MM-yyyy HH-mm"));

            SQLiteCommand sQLiteCommand = new SQLiteCommand(ProfileStringRequest, sqliteConnection);

            sQLiteCommand.ExecuteReader();

            sqliteConnection.Close();
        }//Изменение статуса профиля (Free или Busy), кол-во сессий и кол-во сессий в день

        public void DownloadProfileInZennoposter()
        {
            if(!GetProfileFromDB())
            {
                project.SendErrorToLog("В БД не нашелся нужный профиль");
                return;
            }

            project.Profile.Load(DataBaseAndProfileValue.PathToProfile);

        }//Загрузка профиля в зенопостер        
      
    }
}
