using System;
using ZennoLab.CommandCenter.TouchEvents;
using ZennoLab.InterfacesLibrary.ProjectModel;


namespace ZennoPosterEmulation
{   
    public class RangeValueInt
    {
        public int ValueMin { get; set; }
        public int ValueMax { get; set; }
        public int ValueRandom {get{return new Random().Next(ValueMin, (ValueMax + 1));}}
    }//Генерация рандомного числа без остатка из заданного диапазона
    public class RangeValueFloat
    {
        public float ValueMin { get; set; }
        public float ValueMax { get; set; }
        public float ValueRandom {get{return(float) new Random().NextDouble() * (ValueMax - ValueMin) + ValueMin;}}
    }//Генерация рандомного числа с остатком из заданного диапазона


    public class CreateTouchAndSwipeParametr : EmulationValue
    {
        public CreateTouchAndSwipeParametr(IZennoPosterProjectModel project) : base(project) { }

        public TouchEmulationParameters CreateTouchParametrs()
        {
            TouchEmulationParameters touchEmulationParameters = new TouchEmulationParameters();
            
            touchEmulationParameters.Acceleration = Extension.ParseRangeValueFloat(EmulationValue.Acceleration).ValueRandom;
            touchEmulationParameters.LongTouchLengthMs = Extension.ParseRangeValueInt(EmulationValue.LongTouchLengthMs).ValueRandom;
            touchEmulationParameters.MaxCurvature = Extension.ParseRangeValueFloat(EmulationValue.MaxCurvature).ValueRandom;
            touchEmulationParameters.MaxCurvePeakShift = Extension.ParseRangeValueFloat(EmulationValue.MaxCurvePeakShift).ValueRandom;
            touchEmulationParameters.MaxStep = Extension.ParseRangeValueFloat(EmulationValue.MaxStep).ValueRandom;
            touchEmulationParameters.MaxSwipeShiftTowardsThumb = Extension.ParseRangeValueFloat(EmulationValue.MaxSwipeShiftTowardsThumb).ValueRandom;
            touchEmulationParameters.MinCurvature = Extension.ParseRangeValueFloat(EmulationValue.MinCurvature).ValueRandom;
            touchEmulationParameters.MinCurvePeakShift = Extension.ParseRangeValueFloat(EmulationValue.MinCurvePeakShift).ValueRandom;
            touchEmulationParameters.MinStep = Extension.ParseRangeValueFloat(EmulationValue.MinStep).ValueRandom;
            touchEmulationParameters.MinSwipeShiftTowardsThumb = Extension.ParseRangeValueFloat(EmulationValue.MinSwipeShiftTowardsThumb).ValueRandom;
            touchEmulationParameters.PauseAfterTouchMs = Extension.ParseRangeValueInt(EmulationValue.PauseAfterTouchMs).ValueRandom;
            touchEmulationParameters.PauseBetweenStepsMs = Extension.ParseRangeValueInt(EmulationValue.PauseBetweenStepsMs).ValueRandom;
            touchEmulationParameters.PauseBetweenSwipesMs = Extension.ParseRangeValueInt(EmulationValue.PauseBetweenSwipesMs).ValueRandom;
            touchEmulationParameters.RectangleBasePointPartH = Extension.ParseRangeValueFloat(EmulationValue.RectangleBasePointPartH).ValueRandom;
            touchEmulationParameters.RectangleBasePointPartW = Extension.ParseRangeValueFloat(EmulationValue.RectangleBasePointPartW).ValueRandom;
            touchEmulationParameters.RightThumbProbability = Extension.ParseRangeValueFloat(EmulationValue.RightThumbProbability).ValueRandom;
            touchEmulationParameters.SwipeDeviationX = Extension.ParseRangeValueFloat(EmulationValue.SwipeDeviationX).ValueRandom;
            touchEmulationParameters.SwipeDeviationY = Extension.ParseRangeValueFloat(EmulationValue.SwipeDeviationY).ValueRandom;
            touchEmulationParameters.SwipeFractionX = Extension.ParseRangeValueFloat(EmulationValue.SwipeFractionX).ValueRandom;
            touchEmulationParameters.SwipeFractionY = Extension.ParseRangeValueFloat(EmulationValue.SwipeFractionY).ValueRandom;
            touchEmulationParameters.TouchLengthMs = Extension.ParseRangeValueInt(EmulationValue.TouchLengthMs).ValueRandom;

            return touchEmulationParameters;
        }//Генерация рандомных параметров эмуляции свайпа и тача
    }
}
