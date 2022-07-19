using System;
using ZennoLab.CommandCenter.TouchEvents;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoPosterProject1;


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
            Program.logger.Debug("Генерируем параметры эмуляции тача и свайпа.");
            TouchEmulationParameters touchEmulationParameters = new TouchEmulationParameters();            
            touchEmulationParameters.Acceleration = Extension.ParseRangeValueFloat(Acceleration).ValueRandom;
            Program.logger.Debug("Присвоили значение {0} параметру: Acceleration",touchEmulationParameters.Acceleration);
            touchEmulationParameters.LongTouchLengthMs = Extension.ParseRangeValueInt(LongTouchLengthMs).ValueRandom;
            Program.logger.Debug("Присвоили значение {0} параметру: LongTouchLengthMs", touchEmulationParameters.LongTouchLengthMs);
            touchEmulationParameters.MaxCurvature = Extension.ParseRangeValueFloat(MaxCurvature).ValueRandom;
            Program.logger.Debug("Присвоили значение {0} параметру: MaxCurvature", touchEmulationParameters.MaxCurvature);
            touchEmulationParameters.MaxCurvePeakShift = Extension.ParseRangeValueFloat(MaxCurvePeakShift).ValueRandom;
            Program.logger.Debug("Присвоили значение {0} параметру: MaxCurvePeakShift", touchEmulationParameters.MaxCurvePeakShift);
            touchEmulationParameters.MaxStep = Extension.ParseRangeValueFloat(MaxStep).ValueRandom;
            Program.logger.Debug("Присвоили значение {0} параметру: MaxStep", touchEmulationParameters.MaxStep);
            touchEmulationParameters.MaxSwipeShiftTowardsThumb = Extension.ParseRangeValueFloat(MaxSwipeShiftTowardsThumb).ValueRandom;
            Program.logger.Debug("Присвоили значение {0} параметру: MaxSwipeShiftTowardsThumb", touchEmulationParameters.MaxSwipeShiftTowardsThumb);
            touchEmulationParameters.MinCurvature = Extension.ParseRangeValueFloat(MinCurvature).ValueRandom;
            Program.logger.Debug("Присвоили значение {0} параметру: MinCurvature", touchEmulationParameters.MinCurvature);
            touchEmulationParameters.MinCurvePeakShift = Extension.ParseRangeValueFloat(MinCurvePeakShift).ValueRandom;
            Program.logger.Debug("Присвоили значение {0} параметру: MinCurvePeakShift", touchEmulationParameters.MinCurvePeakShift);
            touchEmulationParameters.MinStep = Extension.ParseRangeValueFloat(MinStep).ValueRandom;
            Program.logger.Debug("Присвоили значение {0} параметру: MinStep", touchEmulationParameters.MinStep);
            touchEmulationParameters.MinSwipeShiftTowardsThumb = Extension.ParseRangeValueFloat(MinSwipeShiftTowardsThumb).ValueRandom;
            Program.logger.Debug("Присвоили значение {0} параметру: MinSwipeShiftTowardsThumb", touchEmulationParameters.MinSwipeShiftTowardsThumb);
            touchEmulationParameters.PauseAfterTouchMs = Extension.ParseRangeValueInt(PauseAfterTouchMs).ValueRandom;
            Program.logger.Debug("Присвоили значение {0} параметру: PauseAfterTouchMs", touchEmulationParameters.PauseAfterTouchMs);
            touchEmulationParameters.PauseBetweenStepsMs = Extension.ParseRangeValueInt(PauseBetweenStepsMs).ValueRandom;
            Program.logger.Debug("Присвоили значение {0} параметру: PauseBetweenStepsMs", touchEmulationParameters.PauseBetweenStepsMs);
            touchEmulationParameters.PauseBetweenSwipesMs = Extension.ParseRangeValueInt(PauseBetweenSwipesMs).ValueRandom;
            Program.logger.Debug("Присвоили значение {0} параметру: PauseBetweenSwipesMs", touchEmulationParameters.PauseBetweenSwipesMs);
            touchEmulationParameters.RectangleBasePointPartH = Extension.ParseRangeValueFloat(RectangleBasePointPartH).ValueRandom;
            Program.logger.Debug("Присвоили значение {0} параметру: RectangleBasePointPartH", touchEmulationParameters.RectangleBasePointPartH);
            touchEmulationParameters.RectangleBasePointPartW = Extension.ParseRangeValueFloat(RectangleBasePointPartW).ValueRandom;
            Program.logger.Debug("Присвоили значение {0} параметру: RectangleBasePointPartW", touchEmulationParameters.RectangleBasePointPartW);
            touchEmulationParameters.RightThumbProbability = Extension.ParseRangeValueFloat(RightThumbProbability).ValueRandom;
            Program.logger.Debug("Присвоили значение {0} параметру: RightThumbProbability", touchEmulationParameters.RightThumbProbability);
            touchEmulationParameters.SwipeDeviationX = Extension.ParseRangeValueFloat(SwipeDeviationX).ValueRandom;
            Program.logger.Debug("Присвоили значение {0} параметру: SwipeDeviationX", touchEmulationParameters.SwipeDeviationX);
            touchEmulationParameters.SwipeDeviationY = Extension.ParseRangeValueFloat(SwipeDeviationY).ValueRandom;
            Program.logger.Debug("Присвоили значение {0} параметру: SwipeDeviationY", touchEmulationParameters.SwipeDeviationY);
            touchEmulationParameters.SwipeFractionX = Extension.ParseRangeValueFloat(SwipeFractionX).ValueRandom;
            Program.logger.Debug("Присвоили значение {0} параметру: SwipeFractionX", touchEmulationParameters.SwipeFractionX);
            touchEmulationParameters.SwipeFractionY = Extension.ParseRangeValueFloat(SwipeFractionY).ValueRandom;
            Program.logger.Debug("Присвоили значение {0} параметру: SwipeFractionY", touchEmulationParameters.SwipeFractionY);
            touchEmulationParameters.TouchLengthMs = Extension.ParseRangeValueInt(TouchLengthMs).ValueRandom;
            Program.logger.Debug("Присвоили значение {0} параметру: TouchLengthMs", touchEmulationParameters.TouchLengthMs);
            Program.logger.Debug("Генерация значений эмуляции закончена.");
            return touchEmulationParameters;
        }//Генерация рандомных параметров эмуляции свайпа и тача
    }
}
