using System;
using ZennoLab.CommandCenter.TouchEvents;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoPosterProject1;


namespace ZennoPosterEmulation
{   
    
    public class RangeValueInt
    {
        Random random = new Random();
        public int ValueMin { get; set; }
        public int ValueMax { get; set; }
        public int ValueRandom {get{return random.Next(ValueMin, (ValueMax + 1));}}
    }//Генерация рандомного числа без остатка из заданного диапазона
    public class RangeValueFloat
    {
        Random random = new Random();
        public float ValueMin { get; set; }
        public float ValueMax { get; set; }
        public float ValueRandom {get{return(float) random.NextDouble() * (ValueMax - ValueMin) + ValueMin;}}
    }//Генерация рандомного числа с остатком из заданного диапазона
    public class CreateTouchAndSwipeParametr : EmulationValue
    {
        public CreateTouchAndSwipeParametr(IZennoPosterProjectModel project) : base(project) { }

        public TouchEmulationParameters CreateTouchParametrs()
        {
            TouchEmulationParameters touchEmulationParameters = new TouchEmulationParameters();            
            touchEmulationParameters.Acceleration = Extension.ParseRangeValueFloat(Acceleration).ValueRandom;
            touchEmulationParameters.LongTouchLengthMs = Extension.ParseRangeValueInt(LongTouchLengthMs).ValueRandom;
            touchEmulationParameters.MaxCurvature = Extension.ParseRangeValueFloat(MaxCurvature).ValueRandom;
            touchEmulationParameters.MaxCurvePeakShift = Extension.ParseRangeValueFloat(MaxCurvePeakShift).ValueRandom;
            touchEmulationParameters.MaxStep = Extension.ParseRangeValueFloat(MaxStep).ValueRandom;
            touchEmulationParameters.MaxSwipeShiftTowardsThumb = Extension.ParseRangeValueFloat(MaxSwipeShiftTowardsThumb).ValueRandom;
            touchEmulationParameters.MinCurvature = Extension.ParseRangeValueFloat(MinCurvature).ValueRandom;
            touchEmulationParameters.MinCurvePeakShift = Extension.ParseRangeValueFloat(MinCurvePeakShift).ValueRandom;
            touchEmulationParameters.MinStep = Extension.ParseRangeValueFloat(MinStep).ValueRandom;
            touchEmulationParameters.MinSwipeShiftTowardsThumb = Extension.ParseRangeValueFloat(MinSwipeShiftTowardsThumb).ValueRandom;
            touchEmulationParameters.PauseAfterTouchMs = Extension.ParseRangeValueInt(PauseAfterTouchMs).ValueRandom;
            touchEmulationParameters.PauseBetweenStepsMs = Extension.ParseRangeValueInt(PauseBetweenStepsMs).ValueRandom;
            touchEmulationParameters.PauseBetweenSwipesMs = Extension.ParseRangeValueInt(PauseBetweenSwipesMs).ValueRandom;
            touchEmulationParameters.RectangleBasePointPartH = Extension.ParseRangeValueFloat(RectangleBasePointPartH).ValueRandom;
            touchEmulationParameters.RectangleBasePointPartW = Extension.ParseRangeValueFloat(RectangleBasePointPartW).ValueRandom;
            touchEmulationParameters.RightThumbProbability = Extension.ParseRangeValueFloat(RightThumbProbability).ValueRandom;
            touchEmulationParameters.SwipeDeviationX = Extension.ParseRangeValueFloat(SwipeDeviationX).ValueRandom;
            touchEmulationParameters.SwipeDeviationY = Extension.ParseRangeValueFloat(SwipeDeviationY).ValueRandom;
            touchEmulationParameters.SwipeFractionX = Extension.ParseRangeValueFloat(SwipeFractionX).ValueRandom;
            touchEmulationParameters.SwipeFractionY = Extension.ParseRangeValueFloat(SwipeFractionY).ValueRandom;
            touchEmulationParameters.TouchLengthMs = Extension.ParseRangeValueInt(TouchLengthMs).ValueRandom;
            return touchEmulationParameters;
        }//Генерация рандомных параметров эмуляции свайпа и тача
    }
}
