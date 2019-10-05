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
        private static readonly Pen edgePen = new Pen(Color.Black, 2);

        private enum Action { None, Move }
        private Action current;

        private Size previousLocation;
        private Vertex selectedVertex;
        private Rectangle rectangle;

        private Bitmap canvas;

        private LinkedList<Vertex> vertices;

        public Form1()
        {
            InitializeComponent();

            var bitmapSize = bitMap.Size;
            canvas = new Bitmap(bitmapSize.Width, bitmapSize.Height);
            rectangle = new Rectangle() { Size = new Size(Vertex.radius, Vertex.radius) };
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
                    canvasGraphics.DrawLine(edgePen, v.point, v.next.point);
                }

                if (vertices.Count > 0)
                {
                    canvasGraphics.FillPolygon(Brushes.Green, vertices.Select(v => v.point).ToArray());
                }

                foreach (Vertex v in vertices)
                {
                    rectangle.Location = v.point + locationAdjustment;
                    canvasGraphics.FillEllipse(v == selectedVertex ?
                        Brushes.Red : Brushes.Black,
                        rectangle);
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
                    
                }
                else
                {
                    if (selectedVertex != null)
                    {
                        bitMap.Invalidate();
                    }

                    selectedVertex = ClosestOrDefault(lastPosition);
                    if (selectedVertex != null)
                    {
                        bitMap.Invalidate();
                    }
                }
            }
        }

        private void BitMap_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var lastPosition = new Point(e.X, e.Y);
                Vertex vertex = ClosestOrDefault(lastPosition);
                if (vertex == null)
                {
                    LinkedListNode<Vertex> prevVertex = GetPreviousVertex(lastPosition);
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

        private Vertex ClosestOrDefault(Point position)
        {
            Vertex result = null;
            double minDist = double.PositiveInfinity;
            foreach (Vertex v in vertices)
            {
                if (Geometry.VerticesIntersect(v.point, position, Vertex.radius) || 
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


        private LinkedListNode<Vertex> GetPreviousVertex(Point position)
        {
            for(var currentNode = vertices.First; currentNode != null; currentNode = currentNode.Next)
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
            if (e.Button == MouseButtons.XButton1)
            {
                current = Action.Move;
                previousLocation = (Size)e.Location;
            }
        }

        private void BitMap_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.XButton1)
            {
                current = Action.None;
                if (selectedVertex != null)
                {
                    var area = bitMap.ClientRectangle;
                    if (selectedVertex.point.Y < area.Y)
                        selectedVertex.point.Y = area.Y;
                    else if (selectedVertex.point.Y > area.Y + area.Height)
                        selectedVertex.point.Y = area.Y + area.Height;

                    if (selectedVertex.point.X < area.X)
                        selectedVertex.point.X = area.X;
                    else if (selectedVertex.point.X > area.X + area.Width)
                        selectedVertex.point.X = area.X + area.Width;

                    bitMap.Invalidate();
                }
            }
        }

        private void BitMap_MouseMove(object sender, MouseEventArgs e)
        {
            if (current == Action.Move && selectedVertex != null)
            {
                selectedVertex.point += (Size)e.Location - previousLocation;
                previousLocation = (Size)e.Location;
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
