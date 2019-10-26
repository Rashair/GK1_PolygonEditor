using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEditor
{
    static class PolygonFactory
    {
        public const int defaultSideLength = 200;

        public static List<Vertex> GetRegularPolygon(Point centre, int numOfPoints, int radius = defaultSideLength / 2)
        {
            if (numOfPoints < 3)
            {
                throw new ArgumentException("Number of points can't be fewer than 3");
            }

            var result = new List<Vertex>(numOfPoints);
            double angle = (2 * Math.PI) / numOfPoints;
            for (int i = 0; i < numOfPoints; i++)
            {
                int x = (int)Math.Round(centre.X + radius * Math.Sin(i * angle));
                int y = (int)Math.Round(centre.Y + radius * Math.Cos(i * angle));
                result.Add(new Vertex(x, y));
            }

            int lastInd = result.Count - 1;
            result[0].SetPrevAndNext(result[lastInd], result[1]);
            result[lastInd].SetPrevAndNext(result[lastInd - 2], result[0]);
            for (int i = 1; i < lastInd; ++i)
            {
                result[i].SetPrevAndNext(result[i - 1], result[i + 1]);
            }

            return result;
        }


        public static List<Vertex> GetTriangle(Point centerPosition)
        {
            return GetRegularPolygon(centerPosition,  3);
        }

        public static List<Vertex> GetRectangle(Point centerPosition)
        {
            return GetRegularPolygon(centerPosition, 4);
        }

        public static List<Vertex> GetPentagon(Point centerPosition)
        {
            return GetRegularPolygon(centerPosition, 5);
        }
    }
}
