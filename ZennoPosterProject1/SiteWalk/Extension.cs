using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZennoPosterSiteWalk
{
    public static class Extension
    {
        public static void ShuffleList<T>(this List<T> List, int Take = -1)
        {
            Random random = new Random();
            var temp = List.OrderBy(x => random.Next()).ToList();
            if (Take > 0)
                temp = temp.Take(Take).ToList();

            List.Clear();
            List.AddRange(temp);
        }//Перемешивание списка
    }
}
