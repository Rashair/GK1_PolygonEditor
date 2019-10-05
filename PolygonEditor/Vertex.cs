using System;
using System.Drawing;

namespace GraphEditor
{
    class Vertex : IComparable<Vertex>
    {
        public const double eps = 1e-6;
        public const int radius = 15;

        public Point point;
        public Vertex prev;
        public Vertex next;


        public Vertex(Point point, Vertex prev = null, Vertex next = null)
        {
            this.point = point;
            SetPrevAndNext(prev, next);
        }

        public Vertex(int x, int y, Vertex prev = null, Vertex next = null)  : 
            this(new Point(x, y), prev, next)
        {
        }

        public void SetPrevAndNext(Vertex prev, Vertex next)
        {
            this.prev = prev;
            this.next = next;
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
