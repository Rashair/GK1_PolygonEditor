using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace GraphEditor
{
    public partial class Form1 : Form
    {
        private const int vertexRadius = 15;
        private static readonly StringFormat format = new StringFormat()
        {
            LineAlignment = StringAlignment.Center,
            Alignment = StringAlignment.Center
        };
        private static readonly Size locationAdjustment = new Size(-vertexRadius / 2, -vertexRadius / 2);
        private static readonly Pen pen = new Pen(Color.Black, 2);
        private static readonly Pen edgePen = new Pen(Color.Black, 2);
        private readonly ComponentResourceManager resources;

        private enum Action { None, Move }
        private Action current;

        private Point lastPosition;
        private Size previousLocation;
        private Vertex selectedVertex;
        private int selectedVertexIndex;
        private Rectangle rectangle;

        private Bitmap canvas;

        private class Vertex
        {
            public Point point;

            public Vertex(Point point)
            {
                this.point = point;
            }

            public override bool Equals(object obj)
            {
                return obj is Vertex vertex &&
                       point.Equals(vertex.point);
            }

            public override int GetHashCode()
            {
                var hashCode = -1667306863;
                hashCode = hashCode * -1521134295 + point.GetHashCode();
                return hashCode;
            }
        };
        private List<Vertex> Vertices;
        private List<HashSet<Vertex>> Edges;

        public Form1()
        {
            InitializeComponent();

            canvas = new Bitmap(bitMap.Size.Width, bitMap.Size.Height);
            rectangle = new Rectangle() { Size = new Size(vertexRadius, vertexRadius) };
            Vertices = new List<Vertex>();
            Edges = new List<HashSet<Vertex>>();
            resources = new ComponentResourceManager(typeof(Form1));
        }

        private void BitMap_Paint(object sender, PaintEventArgs e)
        {
            // Bitmap setpixel
            using (Graphics canvasGraphics = Graphics.FromImage(canvas))
            {
                canvasGraphics.Clear(Color.White);
                for (int i = 0; i < Vertices.Count; ++i)
                {
                    foreach (Vertex vertex in Edges[i])
                    {
                        canvasGraphics.DrawLine(edgePen, Vertices[i].point, vertex.point);
                    }
                }

                for (int i = 0; i < Vertices.Count; ++i)
                {
                    rectangle.Location = Vertices[i].point + locationAdjustment;
                    canvasGraphics.FillEllipse(Brushes.White, rectangle);
                    if (Vertices[i] == selectedVertex)
                    {
                        var prevColor = pen.Color;
                        pen.Color = Color.Red;
                        pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                        canvasGraphics.FillEllipse(pen.Brush, rectangle);
                        pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                        pen.Color = prevColor;
                    }
                    else
                    {
                        canvasGraphics.FillEllipse(pen.Brush, rectangle);
                    }
                }
            }
            e.Graphics.DrawImage(canvas, 0, 0);

            pen.Color = colorDialog1.Color;

            RemoveVertexButton.Enabled = selectedVertex != null;
        }


        private void BitMap_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                lastPosition = new Point(e.X, e.Y);
                (Vertex vertex, int ind) = ClosestOrDefault();
                if (vertex == null)
                {
                    Vertices.Add(new Vertex(lastPosition));
                    Edges.Add(new HashSet<Vertex>());
                    bitMap.Invalidate();
                }
                else if (selectedVertex != null && vertex != selectedVertex)
                {
                    if (Edges[selectedVertexIndex].Contains(vertex))
                    {
                        Edges[selectedVertexIndex].Remove(vertex);
                        Edges[ind].Remove(selectedVertex);
                    }
                    else
                    {
                        Edges[selectedVertexIndex].Add(vertex);
                        Edges[ind].Add(selectedVertex);
                    }
                    bitMap.Invalidate();
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                lastPosition = new Point(e.X, e.Y);
                if (selectedVertex != null)
                {
                    bitMap.Invalidate();
                }

                (selectedVertex, selectedVertexIndex) = ClosestOrDefault();
                if (selectedVertex != null)
                {
                    bitMap.Invalidate();
                }
            }
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

        private (Vertex, int) ClosestOrDefault()
        {
            Vertex result = null;
            int index = -1;
            double minDist = double.PositiveInfinity;
            for (int i = 0; i < Vertices.Count; ++i)
            {
                if (VerticesIntersect(Vertices[i].point, lastPosition) || Vertices[i].point.Equals(lastPosition))
                {
                    double currDist = Distance(Vertices[i].point, lastPosition);
                    if (minDist > currDist)
                    {
                        minDist = currDist;
                        result = Vertices[i];
                        index = i;
                    }
                }
            }

            return (result, index);
        }

        private bool VerticesIntersect(Point p1, Point p2)
        {
            return (p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y)
                <= vertexRadius * vertexRadius;
        }

        private double Distance(Point p1, Point p2)
        {
            return (p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && selectedVertex != null)
            {
                RemoveVertex();
            }
        }

        // Right section

        private void ColorButton_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pen.Color = colorDialog1.Color;
                ColorBox.Invalidate();

                if (selectedVertex != null)
                {
                    bitMap.Invalidate();
                }
            }
        }

        private void ColorBox_Paint(object sender, PaintEventArgs e)
        {
            ColorBox.BackColor = pen.Color;
        }

        private void RemoveVertexButton_Click(object sender, EventArgs e)
        {
            RemoveVertex();
        }

        private void RemoveVertex()
        {
            Vertices.RemoveAt(selectedVertexIndex);
            for (int i = 0; i < Edges.Count; ++i)
            {
                Edges[i].Remove(selectedVertex);

            }
            Edges.RemoveAt(selectedVertexIndex);

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
            Vertices.Clear();
            Edges.Clear();
            current = Action.None;
            selectedVertex = null;
            RemoveVertexButton.Enabled = false;
        }
    }
}
