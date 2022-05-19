using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;
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
    public class Emulation
    {
        public TouchEmulationParameters CreateTouchParametrs()
        {
            TouchEmulationParameters touchEmulationParameters = new TouchEmulationParameters();

            touchEmulationParameters.Acceleration = 0;
            touchEmulationParameters.LongTouchLengthMs = 0;
            touchEmulationParameters.MaxCurvature = 0;
            touchEmulationParameters.MaxCurvePeakShift = 0;
            touchEmulationParameters.MaxStep = 0;
            touchEmulationParameters.MaxSwipeShiftTowardsThumb = 0;
            touchEmulationParameters.MinCurvature = 0;
            touchEmulationParameters.MinCurvePeakShift = 0;
            touchEmulationParameters.MinStep = 0;
            touchEmulationParameters.MinSwipeShiftTowardsThumb = 0;
            touchEmulationParameters.PauseAfterTouchMs = 0;
            touchEmulationParameters.PauseBetweenStepsMs = 0;
            touchEmulationParameters.PauseBetweenSwipesMs = 0;
            touchEmulationParameters.RectangleBasePointPartH = 0;
            touchEmulationParameters.RectangleBasePointPartW = 0;
            touchEmulationParameters.RightThumbProbability = 0;
            touchEmulationParameters.SwipeDeviationX = 0;
            touchEmulationParameters.SwipeDeviationY = 0;
            touchEmulationParameters.SwipeFractionX = 0;
            touchEmulationParameters.SwipeFractionY = 0;
            touchEmulationParameters.TouchLengthMs = 0;




            return touchEmulationParameters;
        }




    }
}
