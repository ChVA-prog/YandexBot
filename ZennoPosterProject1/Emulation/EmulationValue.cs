using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoPosterProject1;

namespace ZennoPosterEmulation
{    
    public class EmulationValue
    {
        readonly IZennoPosterProjectModel Project;
        protected static string Acceleration { get; set; }
        protected static string LongTouchLengthMs { get; set; }
        protected static string MaxCurvature { get; set; }
        protected static string MaxCurvePeakShift { get; set; }
        protected static string MaxStep { get; set; }
        protected static string MaxSwipeShiftTowardsThumb { get; set; }
        protected static string MinCurvature { get; set; }
        protected static string MinCurvePeakShift { get; set; }
        protected static string MinStep { get; set; }
        protected static string MinSwipeShiftTowardsThumb { get; set; }
        protected static string PauseAfterTouchMs { get; set; }
        protected static string PauseBetweenStepsMs { get; set; }
        protected static string PauseBetweenSwipesMs { get; set; }
        protected static string RectangleBasePointPartH { get; set; }
        protected static string RectangleBasePointPartW { get; set; }
        protected static string RightThumbProbability { get; set; }
        protected static string SwipeDeviationX { get; set; }
        protected static string SwipeDeviationY { get; set; }
        protected static string SwipeFractionX { get; set; }
        protected static string SwipeFractionY { get; set; }
        protected static string TouchLengthMs { get; set; }
        public static string LatencyKey { get; set; }

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
