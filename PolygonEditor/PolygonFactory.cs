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
        public static List<Vertex> GetRegularPolygon(Point centre, int numOfPoints, int sideLength = 150)
        {
            if (numOfPoints < 3)
            {
                throw new ArgumentException("Number of points can't be fewer than 3");
            }

            double radius = sideLength / (2 * Math.Sin(Math.PI / numOfPoints));
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
    }
}
