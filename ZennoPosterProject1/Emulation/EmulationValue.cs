using ZennoLab.InterfacesLibrary.ProjectModel;

namespace ZennoPosterEmulation
{    
    public class EmulationValue
    {
        readonly IZennoPosterProjectModel Project;

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
        public static string LatencyKey { get; set; }
        public static int ElementPosition { get; set; }
        public static int InstanceHeight { get; set; }

        public EmulationValue(IZennoPosterProjectModel project)
        {
            this.Project = project;

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
    }
}
