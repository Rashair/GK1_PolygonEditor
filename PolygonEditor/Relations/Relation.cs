using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEditor
{
    abstract class Relation
    {
        private event Action DeleteVertices;

        protected Vertex v1;
        protected Vertex v2;
        protected double angle1;

        protected Vertex v3;
        protected Vertex v4;
        protected double angle2;

        public Relation(Vertex v1, Vertex v2, Vertex v3, Vertex v4)
        {
            (this.v1, this.v2) = (v1, v2);
            this.angle1 = Geometry.Angle(v1.Point, v2.Point);

            (this.v3, this.v4) = (v3, v4);
            this.angle2 = Geometry.Angle(v3.Point, v4.Point);


            v1.ParentRelation = this;
            v2.ChildRelation = this;
            v3.ParentRelation = this;
            v4.ChildRelation = this;
        }

        public bool BelongsToRelation(Vertex v)
        {
            return v1 == v || v2 == v ||
                v3 == v || v4 == v;
        }

        public void RegisterToRelation(Action deleteFromRelation)
        {
            this.DeleteVertices += deleteFromRelation;
        }

        public void OnRemoveRelation()
        {
            this.DeleteVertices.Invoke();
        }

        public (Vertex v1, Vertex v2, Vertex v3, Vertex v4) GetMembersOfRelation()
        {
            return (v1, v2, v3, v4);
        }

        public abstract void ImposeRelation();

        public abstract void PreserveRelation(Vertex movedVertex);

        protected (Vertex v1, Vertex v2) GetEdgeofVertex(Vertex v)
        {
            if (v == v1 || v == v2)
            {
                return (v1, v2);
            }
            else
            {
                return (v3, v4);
            }
        }

        public abstract void PreserveRelation((Vertex v1, Vertex v2) movedEdge);
    }
}
