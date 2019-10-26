using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GraphEditor
{
    public partial class Form1 : Form
    {
        private const double eps = 0.5;

        private static readonly Size locationAdjustment = new Size(-Vertex.radius / 2, -Vertex.radius / 2);
        private Size previousMouseDownLocation;
        private Rectangle vertexRectangle;

        private Vertex selectedVertex;
        private readonly Brush selectedVertexBrush = Brushes.Red;
        private Vertex selectedEdgeVertex;
        private bool isPolygonSelected;

        private readonly List<LinkedList<Vertex>> polygons;
        private bool inAddPolygonMode;
        private LinkedList<Vertex> currentPolygon;

        private bool mouseDownAndUpDetached;

        private void DrawNormalPolygon(Graphics canvasGraphics, LinkedList<Vertex> polygon)
        {
            canvasGraphics.FillPolygon(Brushes.White, polygon.Select(v => v.point).ToArray());
            foreach (var v in polygon)
            {
                canvas.DrawLine(v.point, v.next.point, Color.Black);
                vertexRectangle.Location = v.point + locationAdjustment;
                canvasGraphics.FillEllipse(Brushes.Black, vertexRectangle);
            }
        }

        private void AddNewPolygon(Point firstVertexLocation)
        {
            LinkedList<Vertex> newPolygon = new LinkedList<Vertex>();
            newPolygon.AddLast(new Vertex(firstVertexLocation));

            polygons.Add(newPolygon);
            currentPolygon = newPolygon;

            TurnOnAddPolygonMode();
        }

        private void TurnOnAddPolygonMode()
        {
            inAddPolygonMode = true;
            ClearAllSelection();
            generatePolygonButton.Enabled = false;

            bitMap.MouseDown -= BitMap_MouseDown;
            bitMap.MouseUp -= BitMap_MouseUp;
        }
        private void ClearAllSelection()
        {
            isPolygonSelected = false;
            selectedEdgeVertex = null;
            selectedVertex = null;
            RemoveVertexButton.Enabled = false;
        }

        private void TurnOffAddPolygonMode()
        {
            inAddPolygonMode = false;
            generatePolygonButton.Enabled = true;
            bitMap.MouseDown += BitMap_MouseDown;
            bitMap.MouseUp += BitMap_MouseUp;
            mouseDownAndUpDetached = false;
        }

        private Vertex GetVertexAtPosition(Point position)
        {
            Vertex result = null;
            double minDist = double.PositiveInfinity;
            foreach (Vertex v in currentPolygon)
            {
                if (Geometry.AreVerticesIntersecting(v.point, position, Vertex.radius) ||
                    v.point.Equals(position))
                {
                    double currDist = Geometry.Distance(v.point, position);
                    if (minDist > currDist)
                    {
                        minDist = currDist;
                        result = v;
                    }
                }
            }

            return result;
        }

        private LinkedListNode<Vertex> GetEdgeVertex(Point position)
        {
            for (var currentNode = currentPolygon.First; currentNode != null; currentNode = currentNode.Next)
            {
                var vertex = currentNode.Value;
                double d1 = Geometry.Distance(vertex.point, position);
                double d2 = Geometry.Distance(position, vertex.next.point);
                double d = Geometry.Distance(vertex.point, vertex.next.point);

                if (Math.Abs(d1 + d2 - d) < eps)
                    return (currentNode);
            }

            return (null);
        }

        private static (int xAdjustment, int yAdjustment) GetAdjustment(ref Rectangle area, Point pos)
        {
            int xAdjustment = pos.X < area.X ? area.X - pos.X :
                pos.X > area.X + area.Width ? area.X + area.Width - pos.X : 0;
            int yAdjustment = pos.Y < area.Y ? area.Y - pos.Y :
                pos.Y > area.Y + area.Height ? area.Y + area.Height - pos.Y : 0;

            return (xAdjustment, yAdjustment);
        }

        private void RemoveVertex()
        {
            if (currentPolygon.Count > 3)
            {
                currentPolygon.Remove(selectedVertex);
                selectedVertex.next.prev = selectedVertex.prev;
                selectedVertex.prev.next = selectedVertex.next;

                selectedVertex = null;
                RemoveVertexButton.Enabled = false;
                bitMap.Invalidate();
            }
        }

        private void DeletePolygon()
        {
            ClearAllSelection();
            polygons.Remove(currentPolygon);
            GetNewCurrentPolygon();
            if (inAddPolygonMode)
            {
                TurnOffAddPolygonMode();
            }
        }

        private void GetNewCurrentPolygon()
        {
            if (polygons.Count > 0)
            {
                currentPolygon = polygons.Last();
                if (mouseDownAndUpDetached)
                {
                    bitMap.MouseDown += BitMap_MouseDown;
                    bitMap.MouseUp += BitMap_MouseUp;
                    mouseDownAndUpDetached = false;
                }
            }
            else
            {
                currentPolygon = null;
                bitMap.MouseDown -= BitMap_MouseDown;
                bitMap.MouseUp -= BitMap_MouseUp;
                mouseDownAndUpDetached = true;
            }
        }
    }
}
