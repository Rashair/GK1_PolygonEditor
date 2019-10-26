using System;
using System.Drawing;

namespace GraphEditor
{
    class Vertex : IComparable<Vertex>
    {
        private Relation childRelation;
        private Relation parentRelation;
        private Point point;

        public const double eps = 1e-6;
        public const int radius = 15;


        public Vertex prev;
        public Vertex next;

        public Vertex(Point point, Vertex prev = null, Vertex next = null)
        {
            this.point = point;
            SetPrevAndNext(prev, next);
        }

        public Vertex(int x, int y, Vertex prev = null, Vertex next = null) :
            this(new Point(x, y), prev, next)
        {
        }

        public void SetPrevAndNext(Vertex prev, Vertex next)
        {
            this.prev = prev;
            this.next = next;
        }

        public static implicit operator Point(Vertex v)
        {
            return v.Point;
        }

        public void Offset(int x, int y)
        {
            this.point.Offset(x, y);
        }

        public void Offset(Point p)
        {
            Offset(p.X, p.Y);
        }


        public Point Point
        {
            get { return this.point; }
            set { this.point = value; }
        }


        public Relation ChildRelation
        {
            get { return childRelation; }
            set { this.childRelation = value; }
        }

        public Relation ParentRelation
        {
            get { return parentRelation; }
            set
            {
                this.parentRelation = value;
                value.RegisterToRelation(RemoveRelation);
            }
        }

        public void PreserveRelation()
        {
            if (IsInRelation())
            {
                parentRelation.PreserveRelation(this);
            }
        }

        private void RemoveRelation()
        {
            this.parentRelation = null;
            next.childRelation = null;
        }

        public void InvokeOnRemoveRelation()
        {
            if (IsInRelation())
            {
                this.parentRelation.OnRemoveRelation();
            }
        }

        public bool IsInRelation()
        {
            return this.parentRelation != null;
        }

        public int CompareTo(Vertex other)
        {
            return point.X - other.Point.X < eps ?
               point.Y.CompareTo(other.Point.Y) :
                point.X.CompareTo(other.Point.X);
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
