﻿using System;


namespace ZennoPosterEmulation
{
    

    public class RangeValueInt
    {
        public int ValueMin { get; set; }
        public int ValueMax { get; set; }
        public int ValueRandom {get{return new Random().Next(ValueMin, (ValueMax + 1));}}

    }

    public class RangeValueFloat
    {
        public float ValueMin { get; set; }
        public float ValueMax { get; set; }
        public float ValueRandom {get{return(float) new Random().NextDouble() * (ValueMax - ValueMin) + ValueMin;}}
    }
}
