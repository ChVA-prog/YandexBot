using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZennoPosterYandexWalk
{
    static class Extansion
    {
        public static int CalcPercentLearnCard(this int Count, int Percent)
        {
            return (int)Math.Round((double)(Count * Percent) / 100, MidpointRounding.AwayFromZero);
        }

        public static string GetUrlToDomain(this string url)
        {
            try
            {
                if (!url.Substring(0, 4).ToLower().Contains("http"))
                    url = "http://" + url;
                string host = new Uri(url).Host;

                return host.Substring(host.LastIndexOf('.', host.LastIndexOf('.') - 1) + 1);
            }
            catch
            {
                return "pustayastroka.loh";
            }
        }
    }
}
