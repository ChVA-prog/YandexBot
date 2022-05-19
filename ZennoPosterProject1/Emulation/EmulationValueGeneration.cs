using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZennoPosterProject1
{
    
    public static class EmulationValueGeneration
    {       
       
        public static RangeValueInt ParseRangeValueInt(this string Line)
        {
            string[] ArrayValue = Line.Split(new[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
            RangeValueInt rangeValueInt = new RangeValueInt();
            rangeValueInt.ValueMin = Convert.ToInt32(ArrayValue[0]);
            rangeValueInt.ValueMax = Convert.ToInt32(ArrayValue[1]);

            return rangeValueInt;
        }
        public static RangeValueFloat ParseRangeValueFloat(this string Line)
        {
            string[] ArrayValue = Line.Split(new[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
            RangeValueFloat rangeValueFloat = new RangeValueFloat();
            rangeValueFloat.ValueMin = float.Parse(ArrayValue[0]);
            rangeValueFloat.ValueMax = float.Parse(ArrayValue[1]);

            return rangeValueFloat;
        }

    }
    public class RangeValueInt
    {
        public int ValueMin { get; set; }
        public int ValueMax { get; set; }
        public int ValueRandom
        {
            get
            {
                return new Random().Next(ValueMin, (ValueMax + 1));
            }
        }

    }

    public class RangeValueFloat
    {
        public float ValueMin { get; set; }
        public float ValueMax { get; set; }
        public float ValueRandom
        {
            get
            {
                return (float)new Random().NextDouble() * (ValueMax - ValueMin) + ValueMin;
            }
        }
    }
}
