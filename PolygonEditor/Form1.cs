using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using Input = System.Windows.Input;

namespace GraphEditor
{
    public partial class Form1 : Form
    {
        private const double eps = 0.5;

        private static readonly Size locationAdjustment =
            new Size(-Vertex.radius / 2, -Vertex.radius / 2);
        private static readonly Pen edgePen = new Pen(Color.Black, 6);
        private Size previousLocation;
        private Rectangle vertexRectangle;

        private enum Action { None, MoveVertex, MoveEdge, MovePolygon }
        private Action current;
        private Vertex selectedVertex;
        private readonly Brush selectedVertexBrush = Brushes.Red;
        private Vertex selectedEdgeVertex;
        private readonly Brush selectedEdgeBrush = Brushes.BlueViolet;
        private bool isPolygonSelected;

        private readonly LinkedList<Vertex> vertices;
        private readonly Bitmap canvas;

        public Form1()
        {
            InitializeComponent();

            canvas = new Bitmap(1920, 1024);
            vertexRectangle = new Rectangle() { Size = new Size(Vertex.radius, Vertex.radius) };
            var bitmapSize = bitMap.Size;
            vertices = new LinkedList<Vertex>(PolygonFactory.GetRectangle(
                new Point(bitmapSize.Width / 3, bitmapSize.Height / 3)));
        }

        private void BitMap_Paint(object sender, PaintEventArgs e)
        {
            // Bitmap setpixel
            using (Graphics canvasGraphics = Graphics.FromImage(canvas))
            {
                canvasGraphics.Clear(Color.White);
                foreach (Vertex v in vertices)
                {
                    if (v == selectedEdgeVertex)
                    {
                        edgePen.Color = ((SolidBrush)selectedEdgeBrush).Color;
                        canvasGraphics.DrawLine(edgePen, v.point, v.next.point);
                        edgePen.Color = Color.Black;
                    }
                    else
                        canvasGraphics.DrawLine(edgePen, v.point, v.next.point);
                }

                if (vertices.Count > 0)
                {
                    var polygonBrush = isPolygonSelected ? Brushes.DarkBlue : Brushes.LightGreen;
                    canvasGraphics.FillPolygon(polygonBrush, vertices.Select(v => v.point).ToArray());
                }

                foreach (Vertex v in vertices)
                {
                    vertexRectangle.Location = v.point + locationAdjustment;
                    canvasGraphics.FillEllipse(
                        v == selectedVertex ? selectedVertexBrush :
                        v == selectedEdgeVertex || v.prev == selectedEdgeVertex ? selectedEdgeBrush :
                        Brushes.Black,
                        vertexRectangle);
                }
            }
            e.Graphics.DrawImage(canvas, 0, 0);

            RemoveVertexButton.Enabled = selectedVertex != null;
        }

        private void BitMap_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var lastPosition = new Point(e.X, e.Y);
                if (Input.Keyboard.IsKeyDown(Input.Key.LeftCtrl))
                {
                    isPolygonSelected = !isPolygonSelected && Geometry.IsPointInsidePolygon(lastPosition,
                        vertices.Select(v => v.point).ToList());
                }
                else if (Input.Keyboard.IsKeyDown(Input.Key.LeftShift))
                {
                    selectedEdgeVertex = GetEdgeVertex(lastPosition)?.Value;
                }
                else
                {
                    selectedVertex = GetVertexAtPosition(lastPosition);
                }
                bitMap.Invalidate();
            }
        }

        private void BitMap_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var lastPosition = new Point(e.X, e.Y);
                Vertex vertex = GetVertexAtPosition(lastPosition);
                if (vertex == null)
                {
                    LinkedListNode<Vertex> prevVertex = GetEdgeVertex(lastPosition);
                    if (prevVertex != null)
                    {
                        var v1 = prevVertex.Value;
                        var v2 = v1.next;
                        var newVertex = new Vertex(lastPosition, v1, v2);
                        v1.next = newVertex;
                        v2.prev = newVertex;

                        vertices.AddAfter(prevVertex, newVertex);
                        bitMap.Invalidate();
                    }
                }
            }
        }

        private Vertex GetVertexAtPosition(Point position)
        {
            Vertex result = null;
            double minDist = double.PositiveInfinity;
            foreach (Vertex v in vertices)
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
            for (var currentNode = vertices.First; currentNode != null; currentNode = currentNode.Next)
            {
                var vertex = currentNode.Value;
                double d1 = Geometry.Distance(vertex.point, position);
                double d2 = Geometry.Distance(position, vertex.next.point);
                double d = Geometry.Distance(vertex.point, vertex.next.point);

                if (Math.Abs(d1 + d2 - d) < eps)
                {
                    return (currentNode);
                }
            }

            return (null);
        }

        private void BitMap_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                current = Input.Keyboard.IsKeyDown(Input.Key.LeftCtrl) ? Action.MovePolygon :
                    Input.Keyboard.IsKeyDown(Input.Key.LeftShift) ? Action.MoveEdge :
                    Action.MoveVertex;
                previousLocation = (Size)e.Location;
            }
        }

        private void BitMap_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                current = Action.None;
                if (isPolygonSelected)
                {
                    var area = bitMap.ClientRectangle;
                    int maxXAdjustment = 0;
                    int maxYAdjustment = 0;
                    foreach (Vertex v in vertices)
                    {
                        var (xValue, yValue) = GetAdjustment(ref area, v.point);
                        if (Math.Abs(maxXAdjustment) < Math.Abs(xValue))
                            maxXAdjustment = xValue;
                        if (Math.Abs(maxYAdjustment) < Math.Abs(yValue))
                            maxYAdjustment = yValue;
                    }

                    if (maxXAdjustment != 0 || maxYAdjustment != 0)
                    {
                        foreach (Vertex v in vertices)
                        {
                            v.point.Offset(maxXAdjustment, maxYAdjustment);
                        }

                        bitMap.Invalidate();
                    }
                }
                else if (selectedEdgeVertex != null)
                {
                    var area = bitMap.ClientRectangle;
                    var (xValue1, yValue1) = GetAdjustment(ref area, selectedEdgeVertex.point);
                    var (xValue2, yValue2) = GetAdjustment(ref area, selectedEdgeVertex.next.point);

                    int maxXAdjustment = Math.Abs(xValue1) > Math.Abs(xValue2) ? xValue1 : xValue2;
                    int maxYAdjustment = Math.Abs(yValue1) > Math.Abs(yValue2) ? yValue1 : yValue2;

                    if(maxXAdjustment != 0 || maxYAdjustment != 0)
                    {
                        selectedEdgeVertex.point.Offset(maxXAdjustment, maxYAdjustment);
                        selectedEdgeVertex.next.point.Offset(maxXAdjustment, maxYAdjustment); 
                        bitMap.Invalidate();
                    }
                }
                else if (selectedVertex != null)
                {
                    var area = bitMap.ClientRectangle;
                    var (xValue, yValue) = GetAdjustment(ref area, selectedVertex.point);

                    if (xValue != 0 || yValue != 0)
                    {
                        selectedVertex.point.Offset(xValue, yValue);
                        bitMap.Invalidate();
                    }
                }
            }
        }

        private static (int xAdjustment, int yAdjustment) GetAdjustment(ref Rectangle area, Point pos)
        {
            int xAdjustment = pos.X < area.X ? area.X - pos.X :
                pos.X > area.X + area.Width ? area.X + area.Width - pos.X : 0;
            int yAdjustment = pos.Y < area.Y ? area.Y - pos.Y :
                pos.Y > area.Y + area.Height ? area.Y + area.Height - pos.Y : 0;

            return (xAdjustment, yAdjustment);
        }

        private void BitMap_MouseMove(object sender, MouseEventArgs e)
        {
            if (current == Action.MoveVertex && selectedVertex != null)
            {
                selectedVertex.point += (Size)e.Location - previousLocation;
                previousLocation = (Size)e.Location;
                bitMap.Invalidate();
            }
            else if (current == Action.MoveEdge && selectedEdgeVertex != null)
            {
                var adjustment = (Size)e.Location - previousLocation;
                selectedEdgeVertex.point += adjustment;
                selectedEdgeVertex.next.point += adjustment;
                previousLocation = (Size)e.Location;
                bitMap.Invalidate();
            }
            else if (current == Action.MovePolygon && isPolygonSelected)
            {
                var adjustment = (Size)e.Location - previousLocation;
                previousLocation = (Size)e.Location;
                foreach (Vertex v in vertices)
                {
                    v.point += adjustment;
                }
                bitMap.Invalidate();
            }
        }



        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && selectedVertex != null)
            {
                RemoveVertex();
            }
        }

        // Right section
        /***************************************************************************************/
        private void RemoveVertexButton_Click(object sender, EventArgs e)
        {
            RemoveVertex();
        }

        private void RemoveVertex()
        {
            vertices.Remove(selectedVertex);
            selectedVertex.next.prev = selectedVertex.prev;
            selectedVertex.prev.next = selectedVertex.next;

            selectedVertex = null;
            current = Action.None;
            RemoveVertexButton.Enabled = false;
            bitMap.Invalidate();
        }

        private void ClearGraphButton_Click(object sender, EventArgs e)
        {
            ClearGraph();
            bitMap.Invalidate();
        }

        private void ClearGraph()
        {
            vertices.Clear();
            vertices.AppendRange(PolygonFactory.GetTriangle(
                new Point(bitMap.Width / 3, bitMap.Height / 3)));

            current = Action.None;
            selectedVertex = null;
            RemoveVertexButton.Enabled = false;
        }
    }
}
