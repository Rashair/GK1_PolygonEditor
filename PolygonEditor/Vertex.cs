using System;
using System.Drawing;

namespace GraphEditor
{
    class Vertex : IComparable<Vertex>
    {
        public const double eps = 1e-6;

        public Point point;
        public Vertex next;
        public Vertex prev;

        public Vertex(Point point)
        {
            this.point = point;
        }

        public Vertex(int x, int y)
        {
            this.point = new Point(x, y);
        }

        public int CompareTo(Vertex other)
        {
            return point.X - other.point.X < eps ?
                point.Y.CompareTo(other.point.Y) :
                point.X.CompareTo(other.point.X);
        }

        public override bool Equals(object obj)
        {
            return obj is Vertex vertex &&
                   point.Equals(vertex.point);
        }

        public override int GetHashCode()
        {
            var hashCode = -1667306863;
            hashCode = hashCode * -1521134295 + point.GetHashCode();
            return hashCode;
        }
    };

}
