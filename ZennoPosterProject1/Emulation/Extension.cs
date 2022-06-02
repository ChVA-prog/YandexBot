using System;

namespace ZennoPosterEmulation
{ 
    static class Extension
    {
        public static RangeValueInt ParseRangeValueInt(this string Line)
        {
            string[] ArrayValue = Line.Split(new[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
            RangeValueInt rangeValueInt = new RangeValueInt();
            rangeValueInt.ValueMin = Convert.ToInt32(ArrayValue[0]);
            rangeValueInt.ValueMax = Convert.ToInt32(ArrayValue[1]);

            return rangeValueInt;
        }//Получение рандомного числа без остатка из заданного диапазона
        public static RangeValueFloat ParseRangeValueFloat(this string Line)
        {
            string[] ArrayValue = Line.Split(new[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
            RangeValueFloat rangeValueFloat = new RangeValueFloat();
            rangeValueFloat.ValueMin = float.Parse(ArrayValue[0]);
            rangeValueFloat.ValueMax = float.Parse(ArrayValue[1]);

            return rangeValueFloat;
        }//Получение рандомного числа без остатка из заданного диапазона
    }
}
