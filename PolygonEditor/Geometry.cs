using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEditor
{
    static class Geometry
    {
        public static bool AreVerticesIntersecting(Point p1, Point p2, int radius)
        {
            return (p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y)
                <= radius * radius;
        }

        public static double Distance(Point p1, Point p2)
        {
            return Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
        }

        public static bool IsPointInsidePolygon(Point p, List<Point> polygon)
        {
            if (p.Y < 200)
                return false;

            return true;
        }

        
    }
}
