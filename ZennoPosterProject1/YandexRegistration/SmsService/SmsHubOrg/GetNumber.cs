using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ZennoLab.CommandCenter;
using System.Threading;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoPosterEmulation;
using ZennoPosterSiteWalk;
using ZennoPosterProject1;
using ZennoPosterYandexWalk;
using ZennoPosterYandexRegistrationSmsServiceSmsHubOrg;

namespace ZennoPosterYandexRegistrationSmsServiceSmsHubOrg
{
    class GetNumber
    {
        readonly IZennoPosterProjectModel project;
        readonly Instance instance;

        public GetNumber(Instance instance, IZennoPosterProjectModel project)
        {
            this.instance = instance;
            this.project = project;
        }

        private bool GetCountNumber()
        {
            project.SendInfoToLog("Получаем количество свободных номеров", true);
            string ApiGetResponce = String.Format("https://smshub.org/stubs/handler_api.php?api_key={0}&action=getNumbersStatus&country=0&operator={1}", SmshubValue.ApiKeySmshub, SmshubValue.SmshubOperator);

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
        }
        private int Balance()
        {
            project.SendInfoToLog("Узнаем баланс аккаунта", true);
            string ApiGetResponce = "https://smshub.org/stubs/handler_api.php?api_key=101937U9065b39e17557f2ce72e71392e5eb7be&action=getBalance";

            var resultHttpGet = ZennoPoster.HttpGet(ApiGetResponce, "", "UTF-8",    //ya
                ZennoLab.InterfacesLibrary.Enums.Http.ResponceType.BodyOnly);

            project.SendInfoToLog("Баланс: " + resultHttpGet.Split(':', '.')[1], true);
            return Convert.ToInt32(resultHttpGet.Split(':', '.')[1]);
        }
        public void GetNumberAndId()
        {
            project.SendInfoToLog("Получаем номер для смс", true);
            bool CountNumber = GetCountNumber();
            int CountBalance = Balance();

            if (CountNumber && CountBalance > 3)
            {
                string ApiGetResponce = String.Format("https://smshub.org/stubs/handler_api.php?api_key={0}&action=getNumber&service=ya&operator={1}&country=0", SmshubValue.ApiKeySmshub, SmshubValue.SmshubOperator);
                var resultHttpGet = ZennoPoster.HttpGet(ApiGetResponce, "", "UTF-8",
                    ZennoLab.InterfacesLibrary.Enums.Http.ResponceType.BodyOnly);

                SmshubValue.IdActivation = resultHttpGet.Split(':')[1];
                SmshubValue.PhoneNumber = resultHttpGet.Split(':')[2];
                project.SendInfoToLog("Получили номер: " + SmshubValue.PhoneNumber, true);
            }
            else
            {
                throw new Exception("Не удалось получить номер: Нет свободных номеров либо недостаточный баланс");
            }
        }

        int CounterGetNumber;
        public void GetSmsCode()
        {
            project.SendInfoToLog("Ждем смс с кодом.", true);
            string ApiGetResponce = String.Format("https://smshub.org/stubs/handler_api.php?api_key={0}&action=getStatus&id={1}", SmshubValue.ApiKeySmshub, SmshubValue.IdActivation);
            var resultHttpGet = ZennoPoster.HttpGet(ApiGetResponce, "", "UTF-8",
                ZennoLab.InterfacesLibrary.Enums.Http.ResponceType.BodyOnly);

            if(resultHttpGet.Contains("STATUS_WAIT_CODE"))
            {
                if(CounterGetNumber == 30)
                {
                  RefuseGetNumber();

                  throw new Exception("Смс с кодом не пришла после 75 секунд ожидания, отменили активацию.");
                }
                Thread.Sleep(5000);
                CounterGetNumber++;
                GetSmsCode();             
            }
            if (resultHttpGet.Contains("STATUS_CANCEL"))
            {
                throw new Exception("Активация отменена");
            }

            if (resultHttpGet.Contains("STATUS_OK"))
            {
                SmshubValue.CodeActivation = resultHttpGet.Split(':')[1];
                project.SendInfoToLog("Получили код: " + SmshubValue.CodeActivation, true);
                NeedMoreSms();
            }
        }
        public void RefuseGetNumber()
        {
            string RefuseGetNumber = String.Format("https://smshub.org/stubs/handler_api.php?api_key={0}&action=setStatus&status=8&id={1}", SmshubValue.ApiKeySmshub, SmshubValue.IdActivation);
            ZennoPoster.HttpGet(RefuseGetNumber, "", "UTF-8",ZennoLab.InterfacesLibrary.Enums.Http.ResponceType.BodyOnly);
            project.SendInfoToLog("Отменили взятый номер.", true);
        }
        public void EndUseNumber()
        {
            string EndUseNumber = String.Format("https://smshub.org/stubs/handler_api.php?api_key={0}&action=setStatus&status=6&id={1}", SmshubValue.ApiKeySmshub, SmshubValue.IdActivation);
            ZennoPoster.HttpGet(EndUseNumber, "", "UTF-8", ZennoLab.InterfacesLibrary.Enums.Http.ResponceType.BodyOnly);
            project.SendInfoToLog("Завершили работу с номером", true);
        }
        public void NeedMoreSms()
        { 
                string EndUseNumber = String.Format("https://smshub.org/stubs/handler_api.php?api_key={0}&action=setStatus&status=3&id={1}", SmshubValue.ApiKeySmshub, SmshubValue.IdActivation);
                ZennoPoster.HttpGet(EndUseNumber, "", "UTF-8", ZennoLab.InterfacesLibrary.Enums.Http.ResponceType.BodyOnly);
                project.SendInfoToLog("Подготовили номер к еще одной смс", true);
        }
    }
}
