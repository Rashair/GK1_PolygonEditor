using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEditor.Relations
{
    class PerpendicularityRelation : Relation
    {
        public PerpendicularityRelation(Vertex v1, Vertex v2, Vertex v3, Vertex v4) : base(v1, v2, v3, v4)
        {
        }

        public override void ImposeRelation()
        {
            throw new NotImplementedException();
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
