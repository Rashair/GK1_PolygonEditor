using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEditor.Relations
{
    class PerpendicularityRelation : Relation
    {
        const double rightAngle = 90.0;

        public PerpendicularityRelation(Vertex v1, Vertex v3) : base(v1, v3)
        {
            ImposeRelation();
        }

        private (double ang1, double ang2) CalculateAngles(Vertex w1, Vertex w2, Vertex w3, Vertex w4)
        {
            return (Geometry.Angle(w1, w2), Geometry.Angle(w3, w4));
        }

        public override void ImposeRelation()
        {
            double angle = Geometry.AngleBetweenVectors(v1, v2, v3, v4);
            if (Math.Abs(angle - rightAngle) > Geometry.Eps)
            {
                var distance = Geometry.Distance(v3, v4);
                var M = new Point((v1.Point.X + v2.Point.X) / 2, (v1.Point.Y + v2.Point.Y) / 2);
                var p = v1.Point.Difference(v2.Point);
                var n = new Point(-p.Y, p.X);
                int norm_length = (int)n.Magnitude();
                n.X /= norm_length;
                n.Y /= norm_length;

                var result = new Point((int)(M.X + (distance * n.X)), (int)(M.Y + distance * n.Y));
                var diff = v2.Point.Difference(v3);
                result.Offset(diff.X, diff.Y);
                v4.Point = result;
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
