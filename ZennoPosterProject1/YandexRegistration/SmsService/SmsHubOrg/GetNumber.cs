using System;
using Newtonsoft.Json;
using ZennoLab.CommandCenter;
using System.Threading;
using ZennoLab.InterfacesLibrary.ProjectModel;

namespace ZennoPosterYandexRegistrationSmsServiceSmsHubOrg
{
    class GetNumber : SmshubValue
    {
        public string PhoneNumber { get; set; }
        public string IdActivation { get; set; }
        public string CodeActivation { get; set; }

        readonly IZennoPosterProjectModel project;

        public GetNumber(IZennoPosterProjectModel project) : base (project)
        {           
            this.project = project;
        }

        private bool GetCountNumber()
        {
            project.SendInfoToLog("Получаем количество свободных номеров.", true);
            string ApiGetResponce = String.Format("https://smshub.org/stubs/handler_api.php?api_key={0}&action=getNumbersStatus&country=0&operator={1}",
                SmshubValue.ApiKeySmshub, SmshubValue.SmshubOperator);

            var resultHttpGet = ZennoPoster.HttpGet(ApiGetResponce, "", "UTF-8",
                ZennoLab.InterfacesLibrary.Enums.Http.ResponceType.BodyOnly);

            ApiResponceJsonToCSharp.CountFreeNumber myDeserializedClass = JsonConvert.DeserializeObject<ApiResponceJsonToCSharp.CountFreeNumber>(resultHttpGet);

            if(Convert.ToInt32(myDeserializedClass.ya_0) <= 0)
            {
                project.SendInfoToLog("Количество свободных номеров: " + myDeserializedClass.ya_0, true);
                return false;
            }
            else
            {
                project.SendInfoToLog("Количество свободных номеров: " + myDeserializedClass.ya_0, true);
                return true;                
            }
        }//Получение свободных номеров
        private int Balance()
        {
            string ApiGetResponce = "https://smshub.org/stubs/handler_api.php?api_key=101937U9065b39e17557f2ce72e71392e5eb7be&action=getBalance";

            var resultHttpGet = ZennoPoster.HttpGet(ApiGetResponce, "", "UTF-8",
                ZennoLab.InterfacesLibrary.Enums.Http.ResponceType.BodyOnly);

            project.SendInfoToLog("Баланс: " + resultHttpGet.Split(':', '.')[1], true);
            return Convert.ToInt32(resultHttpGet.Split(':', '.')[1]);
        }//Получение баланса
        public void GetNumberAndId()
        {
            project.SendInfoToLog("Получаем номер для смс.", true);
            bool CountNumber = GetCountNumber();
            int CountBalance = Balance();

            if (CountNumber && CountBalance > 3)
            {
                nomernepodhodit:
                string ApiGetResponce = String.Format("https://smshub.org/stubs/handler_api.php?api_key={0}&action=getNumber&service=ya&operator={1}&country=0", SmshubValue.ApiKeySmshub, SmshubValue.SmshubOperator);
                var resultHttpGet = ZennoPoster.HttpGet(ApiGetResponce, "", "UTF-8",
                    ZennoLab.InterfacesLibrary.Enums.Http.ResponceType.BodyOnly);

                IdActivation = resultHttpGet.Split(':')[1];
                PhoneNumber = resultHttpGet.Split(':')[2];
                if (!PhoneNumber.Contains(Prefix))
                {
                    project.SendWarningToLog("Взятый номер не подходит: " + PhoneNumber);
                    RefuseGetNumber();
                    Thread.Sleep(1500);
                    goto nomernepodhodit;
                }
                project.SendInfoToLog("Получили номер: " + PhoneNumber, true);
            }
            else
            {
                throw new Exception("Не удалось получить номер: Нет свободных номеров либо недостаточный баланс");
            }
        }//Получаем номер и id активации
        private int CounterOfReceiveToSms;
        public void GetSmsCode(bool GetMoreSms)
        {
            project.SendInfoToLog("Ждем смс с кодом.", true);
            string ApiGetResponce = String.Format("https://smshub.org/stubs/handler_api.php?api_key={0}&action=getStatus&id={1}",
                SmshubValue.ApiKeySmshub, IdActivation);

            var resultHttpGet = ZennoPoster.HttpGet(ApiGetResponce, "", "UTF-8",
                ZennoLab.InterfacesLibrary.Enums.Http.ResponceType.BodyOnly);

            if(resultHttpGet.Contains("STATUS_WAIT_CODE"))
            {
                if(CounterOfReceiveToSms == 30)
                {
                  RefuseGetNumber();
                  throw new Exception("Смс с кодом не пришла после 150 секунд ожидания, отменили активацию.");
                }
                Thread.Sleep(5000);
                CounterOfReceiveToSms++;
                GetSmsCode(GetMoreSms);             
            }
            if (resultHttpGet.Contains("STATUS_CANCEL"))
            {
                throw new Exception("Активация отменена.");
            }
            if (resultHttpGet.Contains("STATUS_OK"))
            {
                CodeActivation = resultHttpGet.Split(':')[1];
                project.SendInfoToLog("Получили код: " + CodeActivation, true);

                if (GetMoreSms)
                {
                    NeedMoreSms();
                }
                else
                {
                    EndUseNumber();
                }
            }
        }//Получаем смс код
        private void RefuseGetNumber()
        {
            string RefuseGetNumber = String.Format("https://smshub.org/stubs/handler_api.php?api_key={0}&action=setStatus&status=8&id={1}", SmshubValue.ApiKeySmshub, IdActivation);
            ZennoPoster.HttpGet(RefuseGetNumber, "", "UTF-8",ZennoLab.InterfacesLibrary.Enums.Http.ResponceType.BodyOnly);
            project.SendInfoToLog("Отменили взятый номер.", true);
        }//Отмена номера
        private void EndUseNumber()
        {
            string EndUseNumber = String.Format("https://smshub.org/stubs/handler_api.php?api_key={0}&action=setStatus&status=6&id={1}", SmshubValue.ApiKeySmshub, IdActivation);
            ZennoPoster.HttpGet(EndUseNumber, "", "UTF-8", ZennoLab.InterfacesLibrary.Enums.Http.ResponceType.BodyOnly);
            project.SendInfoToLog("Завершили работу с номером", true);
        }//Закончить работу с номером после получения смс
        private void NeedMoreSms()
        { 
                string EndUseNumber = String.Format("https://smshub.org/stubs/handler_api.php?api_key={0}&action=setStatus&status=3&id={1}",
                    SmshubValue.ApiKeySmshub, IdActivation);

                ZennoPoster.HttpGet(EndUseNumber, "", "UTF-8", ZennoLab.InterfacesLibrary.Enums.Http.ResponceType.BodyOnly);
                project.SendInfoToLog("Подготовили номер к еще одной смс", true);
        }//Получить еще одну смс
    }
}
