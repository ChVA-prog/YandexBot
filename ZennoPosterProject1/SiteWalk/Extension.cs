using System;
using System.Collections.Generic;
using System.Linq;
using ZennoPosterProject1;

namespace ZennoPosterSiteWalk
{
    public static class Extension
    {
        public static void ShuffleList<T>(this List<T> List, int Take = -1)
        {
            Program.logger.Debug("Перемешиваем список: " + List);
            Random random = new Random();
            var temp = List.OrderBy(x => random.Next()).ToList();
            if (Take > 0)
                temp = temp.Take(Take).ToList();

            List.Clear();
            List.AddRange(temp);
            Program.logger.Debug("Перемешали список: " + List);
        }//Перемешивание списка
    }
}
