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
            // Does not work
            double angle = Geometry.AngleBetweenVectors(v1, v2, v3, v4);
            if (Math.Abs(angle - rightAngle) > Geometry.Eps)
            {

            }
        }

        public override void PreserveRelation(Vertex movedVertex)
        {
            //throw new NotImplementedException();
        }

        public override void PreserveRelation((Vertex v1, Vertex v2) movedEdge)
        {
            //throw new NotImplementedException();
        }
    }
}
