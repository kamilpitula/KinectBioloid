using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace KinectBioloid
{
    public static class ActionPoints
    {
        private const double _radius = 3;

        public static List<ActionPoint> GetActionPoints()
        {
            var actionPoints = new List<ActionPoint>();

            actionPoints.Add(new ActionPoint() {Radius = 3, X = 20, Y = 40 ,PositionName = ActionPointsEnum.LeftUp});
            actionPoints.Add(new ActionPoint() {Radius = 3, X = 200, Y = 40, PositionName = ActionPointsEnum.LeftMid });
            actionPoints.Add(new ActionPoint() {Radius = 3, X = 360, Y = 40, PositionName = ActionPointsEnum.LeftDown });
            actionPoints.Add(new ActionPoint() {Radius = 3, X = 20, Y = 620, PositionName = ActionPointsEnum.RightUp });
            actionPoints.Add(new ActionPoint() {Radius = 3, X = 200, Y = 620, PositionName = ActionPointsEnum.RightMid });
            actionPoints.Add(new ActionPoint() {Radius = 3, X = 360, Y = 620, PositionName = ActionPointsEnum.RightDown });

            return actionPoints;
        }
    }
}
