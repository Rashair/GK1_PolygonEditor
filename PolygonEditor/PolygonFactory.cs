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

        public static List<Vertex> GetTriangle(Point leftVertexPosition)
        {
            var v1 = new Vertex(leftVertexPosition);
            var v2 = new Vertex(leftVertexPosition.X + defaultSideLength, leftVertexPosition.Y);
            int height = (int)Math.Round((defaultSideLength * Math.Sqrt(3) / 2.0));
            var v3 = new Vertex(leftVertexPosition.X + defaultSideLength / 2,
                leftVertexPosition.Y + height);

            v1.SetPrevAndNext(v3, v2);
            v2.SetPrevAndNext(v1, v3);
            v3.SetPrevAndNext(v2, v1);

            return new List<Vertex> { v1, v2, v3 };
        }

        public static List<Vertex> GetRectangle(Point leftVertexPosition)
        {
            var v1 = new Vertex(leftVertexPosition);
            var v2 = new Vertex(leftVertexPosition.X + defaultSideLength, leftVertexPosition.Y);
            var v3 = new Vertex(leftVertexPosition.X + defaultSideLength, leftVertexPosition.Y + defaultSideLength);
            var v4 = new Vertex(leftVertexPosition.X, leftVertexPosition.Y + defaultSideLength);

            v1.SetPrevAndNext(v4, v2);
            v2.SetPrevAndNext(v1, v3);
            v3.SetPrevAndNext(v2, v4);
            v4.SetPrevAndNext(v3, v1);

            return new List<Vertex> { v1, v2, v3, v4 };
        }

    }
}
