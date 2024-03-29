﻿using System;

namespace GraphEditor.Relations
{
    class EqualLengthRelation : Relation
    {
        public EqualLengthRelation(Vertex v1, Vertex v3) : base(v1, v3)
        {

            ImposeRelation();
        }

        private (double len1, double len2) CalculateLenghts(Vertex w1, Vertex w2, Vertex w3, Vertex w4)
        {
            return (Geometry.Distance(w1, w2), Geometry.Distance(w3, w4));
        }

        public override void ImposeRelation()
        {
            var (len1, len2) = CalculateLenghts(v1, v2, v3, v4);

            double newLength = (len1 + len2) / 2;
            if (Math.Abs(newLength - len1) > Geometry.Eps)
            {
                v2.Point = Geometry.SameLinePoint(v1, distanceRatio: newLength / len1, v2);
                v4.Point = Geometry.SameLinePoint(v3, distanceRatio: newLength / len2, v4);
            }
        }

        public override void PreserveRelation(Vertex movedVertex)
        {
            var (w1, w2, w3, w4) = GetMovedEdgeFirst(movedVertex);
            var (len1, len2) = CalculateLenghts(w1, w2, w3, w4);
            if (w4.Locked)
            {
                if (!w3.Locked)
                {
                    w3.Point = Geometry.SameLinePoint(w4, distanceRatio: len1 / len2, w3);
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
            else
            {
                w4.Point = Geometry.SameLinePoint(w3, distanceRatio: len1 / len2, w4);
            }
        }

        public override void PreserveRelation((Vertex v1, Vertex v2) movedEdge)
        {
        }
    }
}
