using System;
using ZennoPosterProject1;

namespace ZennoPosterYandexWalk
{
    static class Extansion
    {
        public static int CalcPercentLearnCard(this int Count, int Percent)
        {
            Program.logger.Debug("Высчитываем рандомный процент из заданного диапазона.");
            return (int)Math.Round((double)(Count * Percent) / 100, MidpointRounding.AwayFromZero);
        }//Высчитываем рандомный процент из заданного диапазона
        public static string GetUrlToDomain(this string url)
        {
            Program.logger.Debug("Вытаскиваем домен из ссылки: " + url);
            try
            {
                if (!url.Substring(0, 4).ToLower().Contains("http"))
                    url = "http://" + url;
                string host = new Uri(url).Host;
                string domen = host.Substring(host.LastIndexOf('.', host.LastIndexOf('.') - 1) + 1);
                Program.logger.Debug("Успешно вытащили домен: " + domen);
                return domen;
            }
            catch
            {
                Program.logger.Warn("Не удалось получить домен из ссылки: " + url);
                return "pustayastroka.loh";
            }
        }//Вытаскиваем домен из ссылки
    }
}
