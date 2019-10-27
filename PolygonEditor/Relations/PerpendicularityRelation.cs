using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEditor.Relations
{
    class PerpendicularityRelation : Relation
    {
        public const double Eps = 1e-6;
        const double rightAngle = 90.0;

        public PerpendicularityRelation(Vertex v1, Vertex v2, Vertex v3, Vertex v4) : base(v1, v2, v3, v4)
        {
            ImposeRelation();
        }

        private (double ang1, double ang2) CalculateAngles(Vertex w1, Vertex w2, Vertex w3, Vertex w4)
        {
            return (Geometry.Angle(w1, w2), Geometry.Angle(w3, w4));
        }

        public override void ImposeRelation()
        {
            var (ang1, ang2) = CalculateAngles(v1, v2, v3, v4);
            var angleDifference = Math.Abs(ang1 - ang2);
            if(Math.Abs(angleDifference - rightAngle) >= Eps)
            {
                var x = 1;
            }
        }

        public override void PreserveRelation(Vertex movedVertex)
        {
            throw new NotImplementedException();
        }

        public override void PreserveRelation((Vertex v1, Vertex v2) movedEdge)
        {
            throw new NotImplementedException();
        }
    }
}
