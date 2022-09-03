using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoPosterEmulation;
using ZennoPosterYandexWalk;

namespace ZennoPosterYandexParseImage
{
    class ParseImageSettings : YandexParseImageValue
    {
        readonly IZennoPosterProjectModel project;
        readonly Instance instance;
        public static object LockList = new object();

        public ParseImageSettings(Instance instance, IZennoPosterProjectModel project) : base(project)
        {
            this.instance = instance;
            this.project = project;
        }

        public string ReadParseKeyword()
        {
            lock (LockList)
            {
                string ParseKeyword = File.ReadLines(KeyWordFilePath).Skip(0).First();
                File.WriteAllLines(ParseKeyword, File.ReadAllLines(ParseKeyword).Skip(1));
                return ParseKeyword;
            }
        }//Получение ключа для парсинга

        public void GoToYandexEndEnterParseKeyword(string ParseKeyword)
        {

        }
    }
}
