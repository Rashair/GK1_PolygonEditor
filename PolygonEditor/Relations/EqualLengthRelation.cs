﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEditor.Relations
{
    class EqualLengthRelation : Relation
    {

        public EqualLengthRelation(Vertex v1, Vertex v2, Vertex v3, Vertex v4) : base(v1, v2, v3, v4)
        {
            ImposeRelation();
        }


        public override void ImposeRelation()
        {
            double oldLength = Geometry.Distance(v1, v2);
            double newLength = (oldLength + Geometry.Distance(v3, v4)) / 2;
            if (newLength - oldLength < Double.Epsilon)
            {
                return;
            }
            v2.Point = Geometry.GetNewPointFromAngleAndDistance(v1, angle1, newLength);
            v4.Point = Geometry.GetNewPointFromAngleAndDistance(v3, angle2, newLength);
        }

        public override void PreserveRelation(Vertex movedVertex)
        {
            if (v2 == v3 && movedVertex == v2)
            {
                // TODO
            }

            var (w1, w2) = GetEdgeofVertex(movedVertex);

        }



        public override void PreserveRelation((Vertex v1, Vertex v2) movedEdge)
        {
            throw new NotImplementedException();
        }
    }
}
