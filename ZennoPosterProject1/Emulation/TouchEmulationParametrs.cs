using System;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoLab.InterfacesLibrary.ProjectModel.Collections;
using ZennoLab.InterfacesLibrary.ProjectModel.Enums;
using ZennoLab.Macros;
using Global.ZennoExtensions;
using ZennoLab.Emulation;
using ZennoLab.CommandCenter.TouchEvents;
using ZennoLab.CommandCenter.FullEmulation;
using ZennoLab.InterfacesLibrary.Enums;


namespace ZennoPosterProject1
{
    public static class IntoEmulationVariables
    {

    }
    public class Emulation
    {
        readonly Instance instance;
        readonly IZennoPosterProjectModel zennoPosterProjectModel;
        public Emulation(Instance _instance, IZennoPosterProjectModel _zennoPosterProjectModel)
        {
            this.instance = _instance;
            this.zennoPosterProjectModel = _zennoPosterProjectModel;
        }

        public static string Acceleration { get; set; }
        public static string LongTouchLengthMs { get; set; }
        public static string MaxCurvature { get; set; }
        public static string MaxCurvePeakShift { get; set; }
        public static string MaxStep { get; set; }
        public static string MaxSwipeShiftTowardsThumb { get; set; }
        public static string MinCurvature { get; set; }
        public static string MinCurvePeakShift { get; set; }
        public static string MinStep { get; set; }
        public static string MinSwipeShiftTowardsThumb { get; set; }
        public static string PauseAfterTouchMs { get; set; }
        public static string PauseBetweenStepsMs { get; set; }
        public static string PauseBetweenSwipesMs { get; set; }
        public static string RectangleBasePointPartH { get; set; }
        public static string RectangleBasePointPartW { get; set; }
        public static string RightThumbProbability { get; set; }
        public static string SwipeDeviationX { get; set; }
        public static string SwipeDeviationY { get; set; }
        public static string SwipeFractionX { get; set; }
        public static string SwipeFractionY { get; set; }
        public static string TouchLengthMs { get; set; }

        public TouchEmulationParameters CreateTouchParametrs()
        {                      

            Acceleration = zennoPosterProjectModel.Variables["set_Acceleration"].Value;
            LongTouchLengthMs = zennoPosterProjectModel.Variables["set_LongTouchLengthMs"].Value;
            MaxCurvature = zennoPosterProjectModel.Variables["set_MaxCurvature"].Value;
            MaxCurvePeakShift = zennoPosterProjectModel.Variables["set_MaxCurvePeakShift"].Value;
            MaxStep = zennoPosterProjectModel.Variables["set_MaxStep"].Value;
            MaxSwipeShiftTowardsThumb = zennoPosterProjectModel.Variables["set_MaxSwipeShiftTowardsThumb"].Value;
            MinCurvature = zennoPosterProjectModel.Variables["set_MinCurvature"].Value;
            MinCurvePeakShift = zennoPosterProjectModel.Variables["set_MinCurvePeakShift"].Value;
            MinStep = zennoPosterProjectModel.Variables["set_MinStep"].Value;
            MinSwipeShiftTowardsThumb = zennoPosterProjectModel.Variables["set_MinSwipeShiftTowardsThumb"].Value;
            PauseAfterTouchMs = zennoPosterProjectModel.Variables["set_PauseAfterTouchMs"].Value;
            PauseBetweenStepsMs = zennoPosterProjectModel.Variables["set_PauseBetweenStepsMs"].Value;
            PauseBetweenSwipesMs = zennoPosterProjectModel.Variables["set_PauseBetweenSwipesMs"].Value;
            RectangleBasePointPartH = zennoPosterProjectModel.Variables["set_RectangleBasePointPartH"].Value;
            RectangleBasePointPartW = zennoPosterProjectModel.Variables["set_RectangleBasePointPartW"].Value;
            RightThumbProbability = zennoPosterProjectModel.Variables["set_RightThumbProbability"].Value;
            SwipeDeviationX = zennoPosterProjectModel.Variables["set_SwipeDeviationX"].Value;
            SwipeDeviationY = zennoPosterProjectModel.Variables["set_SwipeDeviationY"].Value;
            SwipeFractionX = zennoPosterProjectModel.Variables["set_SwipeFractionX"].Value;
            SwipeFractionY = zennoPosterProjectModel.Variables["set_SwipeFractionY"].Value;
            TouchLengthMs = zennoPosterProjectModel.Variables["set_TouchLengthMs"].Value;

            TouchEmulationParameters touchEmulationParameters = new TouchEmulationParameters();

            touchEmulationParameters.Acceleration = EmulationValueGeneration.ParseRangeValueFloat(Acceleration).ValueRandom;
            touchEmulationParameters.LongTouchLengthMs = EmulationValueGeneration.ParseRangeValueInt(LongTouchLengthMs).ValueRandom;
            touchEmulationParameters.MaxCurvature = EmulationValueGeneration.ParseRangeValueFloat(MaxCurvature).ValueRandom;
            touchEmulationParameters.MaxCurvePeakShift = EmulationValueGeneration.ParseRangeValueFloat(MaxCurvePeakShift).ValueRandom;
            touchEmulationParameters.MaxStep = EmulationValueGeneration.ParseRangeValueFloat(MaxStep).ValueRandom;
            touchEmulationParameters.MaxSwipeShiftTowardsThumb = EmulationValueGeneration.ParseRangeValueFloat(MaxSwipeShiftTowardsThumb).ValueRandom;
            touchEmulationParameters.MinCurvature = EmulationValueGeneration.ParseRangeValueFloat(MinCurvature).ValueRandom;
            touchEmulationParameters.MinCurvePeakShift = EmulationValueGeneration.ParseRangeValueFloat(MinCurvePeakShift).ValueRandom;
            touchEmulationParameters.MinStep = EmulationValueGeneration.ParseRangeValueFloat(MinStep).ValueRandom;
            touchEmulationParameters.MinSwipeShiftTowardsThumb = EmulationValueGeneration.ParseRangeValueFloat(MinSwipeShiftTowardsThumb).ValueRandom;
            touchEmulationParameters.PauseAfterTouchMs = EmulationValueGeneration.ParseRangeValueInt(PauseAfterTouchMs).ValueRandom;
            touchEmulationParameters.PauseBetweenStepsMs = EmulationValueGeneration.ParseRangeValueInt(PauseBetweenStepsMs).ValueRandom;
            touchEmulationParameters.PauseBetweenSwipesMs = EmulationValueGeneration.ParseRangeValueInt(PauseBetweenSwipesMs).ValueRandom;
            touchEmulationParameters.RectangleBasePointPartH = EmulationValueGeneration.ParseRangeValueFloat(RectangleBasePointPartH).ValueRandom;
            touchEmulationParameters.RectangleBasePointPartW = EmulationValueGeneration.ParseRangeValueFloat(RectangleBasePointPartW).ValueRandom;
            touchEmulationParameters.RightThumbProbability = EmulationValueGeneration.ParseRangeValueFloat(RightThumbProbability).ValueRandom;
            touchEmulationParameters.SwipeDeviationX = EmulationValueGeneration.ParseRangeValueFloat(SwipeDeviationX).ValueRandom;
            touchEmulationParameters.SwipeDeviationY = EmulationValueGeneration.ParseRangeValueFloat(SwipeDeviationY).ValueRandom;
            touchEmulationParameters.SwipeFractionX = EmulationValueGeneration.ParseRangeValueFloat(SwipeFractionX).ValueRandom;
            touchEmulationParameters.SwipeFractionY = EmulationValueGeneration.ParseRangeValueFloat(SwipeFractionY).ValueRandom;
            touchEmulationParameters.TouchLengthMs = EmulationValueGeneration.ParseRangeValueInt(TouchLengthMs).ValueRandom;

            return touchEmulationParameters;
        }


       

    }
}
