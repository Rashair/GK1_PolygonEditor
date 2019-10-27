using System;
using System.Collections.Generic;
using System.Drawing;
namespace GraphEditor
{
    static class Geometry
    {
        public const double Eps = 1e-6;

        public static bool AreVerticesIntersecting(Point p1, Point p2, int radius)
        {
            return (p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y)
                <= radius * radius;
        }

        public static double Magnitude(this Point p)
        {
            return Math.Sqrt(p.X * p.X + p.Y * p.Y);
        }

        public static Point Difference(this Point p, Point d)
        {
            return new Point(p.X - d.X, p.Y - d.Y);
        }

        public static double Distance(Point p1, Point p2)
        {
            return Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
        }

        public static double Angle(Point p1, Point p2)
        {
            float xDiff = p2.X - p1.X;
            float yDiff = p2.Y - p1.Y;

            return Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI;
        }

        public static int CrossProduct(Point p1, Point p2)
        {
            return p1.X * p2.Y - p1.Y * p2.X;
        }

        public static int DotProduct(Point p1, Point p2)
        {
            return p1.X * p2.X + p1.Y * p2.Y;
        }


        public static double AngleBetweenVectors(Point w1, Point w2, Point v1, Point v2)
        {
            // Swap vectors if in wrong order
            var det = CrossProduct(w2, v1);
            if (det > 0)
            {
                (w1, w2) = (v1, v2);
            }

            // Shift second vector to have origin in end of first
            int xDiff = v1.X - w2.X;
            int yDiff = v1.Y - w2.Y;
            v1.Offset(xDiff, yDiff);
            v2.Offset(xDiff, yDiff);
            // Now w2 = v1


            // Algorithm
            Point a = v1.Difference(w1);
            Point b = v1.Difference(v2);

            double angle = Math.Acos(DotProduct(a, b) / (a.Magnitude() * b.Magnitude())) * (180 / Math.PI);

            /// ----
            double theta1 = Math.Atan2(a.Y, a.X) * 180 / Math.PI;
            double theta2 = Math.Atan2(b.Y, b.X) * 180 / Math.PI;
            angle = (180 + theta1 - theta2 + 360);
            while (angle > 360) angle -= 360;

            return angle;
        }


        // https://math.stackexchange.com/questions/175896/finding-a-point-along-a-line-https://stackoverflow.com/questions/13302396/given-two-points-find-a-third-point-on-the-line?rq=1a-certain-distance-away-from-another-point/175906
        public static Point SameLinePoint(Point p1, double distanceRatio, Point p2)
        {
            return new Point(
               (int)((1 - distanceRatio) * p1.X + distanceRatio * p2.X),
               (int)((1 - distanceRatio) * p1.Y + distanceRatio * p2.Y)
            );
        }


        // https://stackoverflow.com/a/14998816/6841224
        // The function counts the number of sides of the polygon that:
        //  - intersect the Y coordinate of the point (the first if() condition) 
        //  - are to the left of it (the second if() condition).
        // If the number of such sides is odd, then the point is inside the polygon
        public static bool IsPointInsidePolygon(Point p, List<Point> polygon)
        {
            bool result = false;
            int j = polygon.Count - 1;
            for (int i = 0; i < polygon.Count; ++i)
            {
                if ((polygon[j].Y < p.Y && polygon[i].Y >= p.Y) ||
                    (polygon[i].Y < p.Y && polygon[j].Y >= p.Y))
                {
                    if (polygon[i].X + (p.Y - polygon[i].Y) * (polygon[j].X - polygon[i].X)
                        / (double)(polygon[j].Y - polygon[i].Y) < p.X)
                    {
                        result = !result;
                    }
                }
                j = i;
            }

            return result;
        }

        // http://tech-algorithm.com/articles/drawing-line-using-bresenham-algorithm/
        public static void DrawLine(this Bitmap canvas, Point A, Point B, Color color)
        {
            int width = B.X - A.X;
            int height = B.Y - A.Y;
            Point d1 = new Point(Math.Sign(width), Math.Sign(height));

            Point d2;
            int longerDim = Math.Abs(width);
            int shorterDim = Math.Abs(height);
            if (longerDim < shorterDim)
            {
                (longerDim, shorterDim) = (shorterDim, longerDim);
                d2 = new Point(0, d1.Y);
            }
            else
                d2 = new Point(d1.X, 0);

            int numerator = longerDim >> 1;

            for (int i = 0; i <= longerDim; ++i)
            {
                if (A.X > 0 && A.Y > 0 && A.X < canvas.Width && A.Y < canvas.Height)
                    canvas.SetPixel(A.X, A.Y, color);

                numerator += shorterDim;
                if (numerator >= longerDim)
                {
                    numerator -= longerDim;
                    A.Offset(d1);
                }
                else
                {
                    A.Offset(d2);
                }
            }
        }
    }
}
