using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoLab.CommandCenter.TouchEvents;
using ZennoLab.CommandCenter;

namespace ZennoPosterEmulation
{    
    public class EmulationValue
    {
        readonly Instance instance;
        readonly IZennoPosterProjectModel Project;

        private static string Acceleration { get; set; }
        private static string LongTouchLengthMs { get; set; }
        private static string MaxCurvature { get; set; }
        private static string MaxCurvePeakShift { get; set; }
        private static string MaxStep { get; set; }
        private static string MaxSwipeShiftTowardsThumb { get; set; }
        private static string MinCurvature { get; set; }
        private static string MinCurvePeakShift { get; set; }
        private static string MinStep { get; set; }
        private static string MinSwipeShiftTowardsThumb { get; set; }
        private static string PauseAfterTouchMs { get; set; }
        private static string PauseBetweenStepsMs { get; set; }
        private static string PauseBetweenSwipesMs { get; set; }
        private static string RectangleBasePointPartH { get; set; }
        private static string RectangleBasePointPartW { get; set; }
        private static string RightThumbProbability { get; set; }
        private static string SwipeDeviationX { get; set; }
        private static string SwipeDeviationY { get; set; }
        private static string SwipeFractionX { get; set; }
        private static string SwipeFractionY { get; set; }
        private static string TouchLengthMs { get; set; }
        public static string LatencyKey { get; set; }

        public EmulationValue(Instance _instance, IZennoPosterProjectModel _project)
        {
            this.instance = _instance;
            this.Project = _project;

            Acceleration = Project.Variables["set_Acceleration"].Value;
            LongTouchLengthMs = Project.Variables["set_LongTouchLengthMs"].Value;
            MaxCurvature = Project.Variables["set_MaxCurvature"].Value;
            MaxCurvePeakShift = Project.Variables["set_MaxCurvePeakShift"].Value;
            MaxStep = Project.Variables["set_MaxStep"].Value;
            MaxSwipeShiftTowardsThumb = Project.Variables["set_MaxSwipeShiftTowardsThumb"].Value;
            MinCurvature = Project.Variables["set_MinCurvature"].Value;
            MinCurvePeakShift = Project.Variables["set_MinCurvePeakShift"].Value;
            MinStep = Project.Variables["set_MinStep"].Value;
            MinSwipeShiftTowardsThumb = Project.Variables["set_MinSwipeShiftTowardsThumb"].Value;
            PauseAfterTouchMs = Project.Variables["set_PauseAfterTouchMs"].Value;
            PauseBetweenStepsMs = Project.Variables["set_PauseBetweenStepsMs"].Value;
            PauseBetweenSwipesMs = Project.Variables["set_PauseBetweenSwipesMs"].Value;
            RectangleBasePointPartH = Project.Variables["set_RectangleBasePointPartH"].Value;
            RectangleBasePointPartW = Project.Variables["set_RectangleBasePointPartW"].Value;
            RightThumbProbability = Project.Variables["set_RightThumbProbability"].Value;
            SwipeDeviationX = Project.Variables["set_SwipeDeviationX"].Value;
            SwipeDeviationY = Project.Variables["set_SwipeDeviationY"].Value;
            SwipeFractionX = Project.Variables["set_SwipeFractionX"].Value;
            SwipeFractionY = Project.Variables["set_SwipeFractionY"].Value;
            TouchLengthMs = Project.Variables["set_TouchLengthMs"].Value;
            LatencyKey = Project.Variables["set_LatencyKey"].Value;
        }
       
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
