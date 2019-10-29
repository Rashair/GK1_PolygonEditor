using System;

namespace GraphEditor.Relations
{
    class PerpendicularityRelation : Relation
    {
        const double rightAngle = 90.0;

        public PerpendicularityRelation(Vertex v1, Vertex v3) : base(v1, v3)
        {
            ImposeRelation();
        }

        public override void ImposeRelation()
        {
            // Does not work for over 180 degrees
            double angle = Geometry.AngleBetweenVectors(v1, v2, v3, v4);
            if (Math.Abs(angle - rightAngle) > Geometry.Eps)
            {
                //v2.Offset(-v1.Point.X, 0);
                //v4.Offset(0, -v3.Point.Y);
            }
        }

        public override void PreserveRelation(Vertex movedVertex)
        {
            var (w1, w2, w3, w4) = GetMovedEdgeFirst(movedVertex);
            double angle = Geometry.AngleBetweenVectors(w1, w2, w3, w4);
            if (Math.Abs(angle - rightAngle) > Geometry.Eps)
            {
                //v2.Offset(0, -v1.Point.Y);
                //v4.Offset(-v3.Point.X, 0);
            }
        }

        public override void PreserveRelation((Vertex v1, Vertex v2) movedEdge)
        {
            //throw new NotImplementedException();
        }
    }
}
