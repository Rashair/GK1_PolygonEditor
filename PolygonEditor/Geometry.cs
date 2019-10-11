using System;
using System.Collections.Generic;
using System.Drawing;
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
                if(A.X > 0 && A.Y > 0 && A.X < canvas.Width && A.Y < canvas.Height)
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
