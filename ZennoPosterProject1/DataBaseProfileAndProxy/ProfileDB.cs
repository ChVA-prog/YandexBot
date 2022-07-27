using ZennoLab.InterfacesLibrary.ProjectModel;
using System.Data.SQLite;
using System;
using System.Threading;
using ZennoPosterProject1;

namespace DataBaseProfileAndProxy
{
     class ProfileDB : DataBaseProfileAndProxyValue
     {
        public static object LockList = new object();

        readonly IZennoPosterProjectModel project;

        public ProfileDB(IZennoPosterProjectModel project) : base(project)
        {
            this.project = project;
        }
        public void DownloadProfileInZennoposter()
        {
            lock (LockList)
            {
                Program.logger.Debug("Начинаем процесс установки профиля в инстанс");
                CheckAndCreateNewProfile();
                GetProfileFromDB();
                project.Profile.Load(PathToProfile);
                project.SendInfoToLog("Назначили профиль " + PathToProfile + " в проект", true);
                Program.logger.Info("Назначили профиль " + PathToProfile + " в проект");
            }
        }//Загрузка профиля в зенопостер
        private void CheckAndCreateNewProfile()
        {
            Program.logger.Debug("Проверяем соответсвует ли трубуемое кол-во профилей в БД ({0}) фактическому.", CountFreeProfileInDB);

            if (GetCountProfileInDB() < CountFreeProfileInDB)
            {
                project.SendInfoToLog("Количество свободных профилей в базе данных меньше " + project.Variables["set_CountFreeProfileInDB"].Value + ", создаем профиль и сохраняем его",true);
                Program.logger.Info("Количество свободных профилей в базе данных меньше " + project.Variables["set_CountFreeProfileInDB"].Value + ", создаем профиль и сохраняем его");

                if (!project.Profile.UserAgent.ToLower().Contains("android"))
                {
                    Program.logger.Error("Юзерагент не подходит. Прекращаем работу");
                    throw new Exception("Юзерагент не подходит. Прекращаем работу");
                }

                else
                {                    
                    string PathToSaveProfile = PathToFolderProfile + @"\" + project.Profile.NickName + ".zpprofile";
                    project.Profile.Save(PathToSaveProfile, true, true, true, true, true, true, true, true, true, null);
                    SaveProfileDataToDB(PathToSaveProfile);
                    Thread.Sleep(2000);
                }                             
            }
        }//Проверка количества профилей и создание новых если их не достаточно
        private int GetCountProfileInDB()
        {
            Program.logger.Debug("Получаем количество профилей в БД");
            SQLiteConnection sqliteConnection = new DB().OpenConnectDb();
            SQLiteCommand sQLiteCommand = new SQLiteCommand(sqliteConnection)
            {CommandText = "SELECT COUNT(*) FROM Profiles WHERE Status = 'Free' OR Status = 'Busy'"};
            Program.logger.Debug("SELECT COUNT(*) FROM Profiles WHERE Status = 'Free' OR Status = 'Busy'");
            object CountProfile = sQLiteCommand.ExecuteScalar();
            sqliteConnection.Close();
            project.SendInfoToLog("Количество профилей в базе данных: " + CountProfile.ToString(),true);
            Program.logger.Info("Количество профилей в базе данных: " + CountProfile.ToString());          
            ClearSessionDayAllProfile();
            return Convert.ToInt32(CountProfile);
        }//Получение количества профилей для работы из БД
        private void GetProfileFromDB()
        {
               Program.logger.Debug("Начинаем процесс получения данных профиля для работы из БД.");
               SQLiteConnection sqliteConnection = new DB().OpenConnectDb();
               string ProfileStringRequest = String.Format("SELECT PathToProfile, CountSession, CountSessionDay, DateLastEnterYandex FROM Profiles WHERE Status = 'Free' AND TimeToGetYandex > '{0}' AND TimeToNextGetYandex < '{1}' AND CountSession < '{2}' ORDER BY CountSessionDay ASC LIMIT 1",
                    DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"), DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"), CountSessionLimit);
               Program.logger.Debug(ProfileStringRequest);
               SQLiteCommand sQLiteCommand = new SQLiteCommand(ProfileStringRequest, sqliteConnection);

                try
                {
                    SQLiteDataReader reader = sQLiteCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        Program.logger.Debug("Получаем данные профиля");
                        PathToProfile = reader.GetValue(0).ToString();
                        Program.logger.Debug("Получили путь: " + PathToProfile);
                        CountSession = Convert.ToInt32(reader.GetValue(1).ToString());
                        CountSessionDay = Convert.ToInt32(reader.GetValue(2).ToString());

                        //if (CountSessionDay != 0 && (Convert.ToDateTime(reader.GetString(3)) < DateTime.Now.Date))
                        //{
                        //    Program.logger.Debug("Начался новый день, обнуляем количество дневных сессий профиля.");
                        //    UpdateCountSessionDay(sqliteConnection);
                        //}
                    }
                    if (String.IsNullOrEmpty(PathToProfile))
                    {
                        Program.logger.Error("В БД нету подходящих профилей.");
                        throw new Exception("В БД нету подходящих профилей.");
                    }
                }
                catch (Exception ex)
                {
                    sqliteConnection.Close();
                    Program.logger.Error("Ошибка при попытке получить профиль из БД: " + ex.Message);
                    throw new Exception("Ошибка при попытке получить профиль из БД: " + ex.Message);
                }
                sqliteConnection.Close();
                UpdateStatusProfile("Busy");                    
                project.SendInfoToLog("Взяли профиль из БД.", true);
                Program.logger.Info("Взяли профиль {0} из БД.", PathToProfile);          
        }//Получение данных профиля из БД
        public void UpdateStatusProfile(string Status)
        {
            lock (LockList)
            {
                Program.logger.Debug("Меняем статус профиля {0} на: {1}.", PathToProfile, Status);
                SQLiteConnection sqliteConnection = new DB().OpenConnectDb();
                string ProfileStringRequest = String.Format("UPDATE Profiles SET Status = '{1}', TimeToNextGetYandex = '{2}' WHERE PathToProfile = '{0}'",
                    PathToProfile, Status, DateTime.Now.AddHours(3).ToString("yyyy-MM-dd HH-mm-ss"));
                Program.logger.Debug(ProfileStringRequest);
                SQLiteCommand sQLiteCommand = new SQLiteCommand(ProfileStringRequest, sqliteConnection);
                sQLiteCommand.ExecuteReader();
                sqliteConnection.Close();
                Program.logger.Debug("Успешно сменили статус профиля {0} на: {1}", PathToProfile, Status);
                project.SendInfoToLog("Изменили статус профиля на: " + Status, true);
            }
        }//Изменение статуса профиля (Free или Busy) 
        public void UpdateStatusProfile(string Status, int Session, int SessionDay)
        {
            lock (LockList)
            {
                Program.logger.Debug("Меняем статус профиля {0} на: {1} , кол-во сессий на {2}, количество сессий за день на {3}. ", PathToProfile, Status, Session, SessionDay);
                CountSession = CountSession + Session;
                CountSessionDay = CountSessionDay + SessionDay;
                SQLiteConnection sqliteConnection = new DB().OpenConnectDb();
                string ProfileStringRequest = String.Format("UPDATE Profiles SET Status = '{1}', CountSession = '{2}', CountSessionDay = '{3}', TimeToNextGetYandex = '{4}', DateLastEnterYandex = '{5}' " +
                    "WHERE PathToProfile = '{0}'", PathToProfile, Status, CountSession, CountSessionDay, DateTime.Now.AddHours(3).ToString("yyyy-MM-dd HH-mm"), DateTime.Now);
                Program.logger.Debug(ProfileStringRequest);
                SQLiteCommand sQLiteCommand = new SQLiteCommand(ProfileStringRequest, sqliteConnection);
                sQLiteCommand.ExecuteReader();
                sqliteConnection.Close();
                project.SendInfoToLog("Изменили статус профиля на: " + Status + " Количество сессий на: " + CountSession + " Количество сессий в день на: " + CountSessionDay, true);
                Program.logger.Debug("Успешно сменили статус профиля {0} на: {1} , кол-во сессий на {2}, количество сессий за день на {3}. ", PathToProfile, Status, Session, SessionDay);
            }
        }//Изменение статуса профиля (Free или Busy), кол-во сессий и кол-во сессий в день
        private void UpdateCountSessionDay(SQLiteConnection sqliteConnection,string path)
        {
            CountSessionDay = 0;
            string ProfileStringRequest = String.Format("UPDATE Profiles SET CountSessionDay = '0' WHERE PathToProfile = '{0}'",
                path);           
            SQLiteCommand sQLiteCommand = new SQLiteCommand(ProfileStringRequest, sqliteConnection);
            sQLiteCommand.ExecuteReader();
        }//Обнуление дневных сессий у всех профилей
        private void SaveProfileDataToDB(string PathTosave)
        {
            Program.logger.Debug("Начинаем процесс сохранения в БД профиля: " + PathTosave);
            SQLiteConnection sqliteConnection = new DB().OpenConnectDb();
            string ProfileStringRequest = String.Format("INSERT INTO Profiles(PathToProfile, TimeToGetYandex, CountSession, CountSessionDay, TimeToNextGetYandex, Status, DateLastEnterYandex, YandexRegistration, SettingsAccount) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                PathTosave, DateTime.UtcNow.AddDays(3).ToString("yyy-MM-dd HH-mm-ss"), 0, 0, DateTime.UtcNow.ToString("yyyy-MM-dd HH-mm-ss"), "Free", DateTime.MinValue.ToString("yyyy-MM-dd"), "NO", "NO");
            Program.logger.Debug(ProfileStringRequest);
            SQLiteCommand sQLiteCommand = new SQLiteCommand(ProfileStringRequest, sqliteConnection);
            sQLiteCommand.ExecuteReader();
            sqliteConnection.Close();
            project.SendInfoToLog("Сохранили профиль в БД. " + project.Profile.NickName);
            Program.logger.Info("Профиль успешно сохранен в БД: " + PathTosave);
        }//Сохранение профиля в БД
        public void SaveProfile()
        {
            Program.logger.Debug("Начинаем процесс сохранения профиля на ПК");
            string PathToSaveProfile = PathToFolderProfile + @"\" + project.Profile.NickName + ".zpprofile";
            project.Profile.Save(PathToSaveProfile, false, true, true, true, true, true, true, true, true, null);
            project.SendInfoToLog("Сохранили профиль на ПК: " + PathToSaveProfile, true);
            Program.logger.Info("Успешно сохранили профиль на ПК: " + PathToSaveProfile);
        }//Сохранение профиля на пк
        public void ClearSessionDayAllProfile()
        {
            Program.logger.Info("Обнуляем дневные сессии у всех профилей");
            SQLiteConnection sqliteConnection = new DB().OpenConnectDb();
            string ProfileStringRequest = "SELECT PathToProfile, CountSessionDay, DateLastEnterYandex FROM Profiles WHERE Status = 'Free' ORDER BY CountSessionDay ASC";
            Program.logger.Debug(ProfileStringRequest);
            SQLiteCommand sQLiteCommand = new SQLiteCommand(ProfileStringRequest, sqliteConnection);

            try
            {
                SQLiteDataReader reader = sQLiteCommand.ExecuteReader();

                while (reader.Read())
                {
                    var path = reader.GetValue(0).ToString();
                    var CountSessionDay = Convert.ToInt32(reader.GetValue(1).ToString());

                    if (CountSessionDay != 0 && (Convert.ToDateTime(reader.GetString(2)) < DateTime.Now.Date))
                    {                       
                        UpdateCountSessionDay(sqliteConnection, path);
                    }
                }
            }            
            catch (Exception ex)
            {
                sqliteConnection.Close();
                Program.logger.Error("Ошибка при попытке получить профили для обнуления количества сессий: " + ex.Message);
                throw new Exception("Ошибка при попытке получить профили для обнуления количества сессий: " + ex.Message);
            }
            sqliteConnection.Close();
            Program.logger.Info("Обнулили дневные сессии у всех профилей");
            project.SendInfoToLog("Обнулили дневные сессии у всех профилей",true);
        }
     }
}
