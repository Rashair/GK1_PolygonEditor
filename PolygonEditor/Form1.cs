using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Windows.Forms;

namespace GraphEditor
{
    public partial class Form1 : Form
    {
        private readonly Bitmap canvas;

        public Form1()
        {
            InitializeComponent();

            canvas = new Bitmap(1920, 1024);
            Point center = new Point(bitMap.Size.Width / 2, bitMap.Size.Height / 2);
            vertexRectangle = new Rectangle() { Size = new Size(Vertex.radius, Vertex.radius) };
            currentPolygon = new LinkedList<Vertex>(PolygonFactory.GetRegularPolygon(center, 4));
            polygons = new List<LinkedList<Vertex>> { currentPolygon };

            centreXTextBox.Text = center.X.ToString();
            centreYTextBox.Text = center.Y.ToString();
        }

        private void BitMap_Paint(object sender, PaintEventArgs e)
        {
            if (currentPolygon == null)
            {
                e.Graphics.Clear(Color.White);
                return;
            }

            // Bitmap setpixel
            using (Graphics canvasGraphics = Graphics.FromImage(canvas))
            {
                canvasGraphics.Clear(Color.White);

                if (inAddPolygonMode)
                {
                    foreach (LinkedList<Vertex> polygon in polygons)
                    {
                        if (polygon != currentPolygon)
                            DrawNormalPolygon(canvasGraphics, polygon);
                    }

                    var mousePos = bitMap.PointToClient(MousePosition);
                    foreach (Vertex v in currentPolygon)
                    {
                        canvas.DrawLine(v.point, v.next?.point ?? mousePos, Color.Black);

                        vertexRectangle.Location = v.point + locationAdjustment;
                        canvasGraphics.FillEllipse(Brushes.Black, vertexRectangle);
                    }
                }
                else
                {
                    foreach (LinkedList<Vertex> polygon in polygons)
                    {
                        if (polygon != currentPolygon)
                            DrawNormalPolygon(canvasGraphics, polygon);
                    }

                    canvasGraphics.FillPolygon(Brushes.LightGreen, currentPolygon.Select(v => v.point).ToArray());
                    foreach (Vertex v in currentPolygon)
                    {
                        canvas.DrawLine(v.point, v.next.point, Color.Black);

                        vertexRectangle.Location = v.point + locationAdjustment;
                        canvasGraphics.FillEllipse(v == selectedVertex ? selectedVertexBrush : Brushes.Black, 
                        vertexRectangle);
                    }
                }
            }
            e.Graphics.DrawImage(canvas, 0, 0);

            RemoveVertexButton.Enabled = selectedVertex != null && currentPolygon.Count > 3;
        }

        private void BitMap_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && !inAddPolygonMode)
            {
                if (GetVertexAtPosition(e.Location) == null)
                {
                    if (!Geometry.IsPointInsidePolygon(e.Location,
                        currentPolygon.Select(v => v.point).ToList()))
                    {
                        AddNewPolygon(e.Location);
                        bitMap.Invalidate();
                    }
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (inAddPolygonMode)
                {
                    DeletePolygon();
                    bitMap.Invalidate();
                }
                else if (GetVertexAtPosition(e.Location) == null)
                {
                    LinkedListNode<Vertex> firstEdgeVertex = GetEdgeVertex(e.Location);
                    if (firstEdgeVertex != null)
                    {
                        var v1 = firstEdgeVertex.Value;
                        var v2 = v1.next;
                        var newVertex = new Vertex(e.Location, v1, v2);
                        v1.next = newVertex;
                        v2.prev = newVertex;

                        currentPolygon.AddAfter(firstEdgeVertex, newVertex);
                        bitMap.Invalidate();
                    }
                }
                // TODO : relations here
            }
        }

        private void BitMap_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                centreXTextBox.Text = e.X.ToString();
                centreYTextBox.Text = e.Y.ToString();

                if (currentPolygon == null)
                {
                    AddNewPolygon(e.Location);
                    bitMap.Invalidate();
                }
                else if (inAddPolygonMode)
                {
                    var vertex = GetVertexAtPosition(e.Location);
                    if (vertex == null)
                    {
                        var newVertex = new Vertex(e.Location, currentPolygon.Last.Value);
                        currentPolygon.Last.Value.next = newVertex;
                        currentPolygon.AddLast(newVertex);
                        bitMap.Invalidate();
                    }
                    else if (currentPolygon.Count >= 3 && vertex.prev == null)
                    {
                        vertex.prev = currentPolygon.Last.Value;
                        currentPolygon.Last.Value.next = vertex;
                        TurnOffAddPolygonMode();
                        bitMap.Invalidate();
                    }
                }
            }
        }

        private void BitMap_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                previousMouseDownLocation = (Size)e.Location;
                selectedVertex = GetVertexAtPosition(e.Location);
                if (selectedVertex == null)
                {
                    selectedEdgeVertex = GetEdgeVertex(e.Location)?.Value;
                    if (selectedEdgeVertex == null)
                    {
                        isPolygonSelected = !isPolygonSelected && Geometry.IsPointInsidePolygon(e.Location,
                            currentPolygon.Select(v => v.point).ToList());
                        if (!isPolygonSelected)
                        {
                            foreach (LinkedList<Vertex> polygon in polygons)
                            {
                                if (Geometry.IsPointInsidePolygon(e.Location,
                                    polygon.Select(v => v.point).ToList()))
                                {
                                    currentPolygon = polygon;
                                    isPolygonSelected = true;
                                    bitMap.Invalidate();
                                    break;
                                }
                            }
                        }
                        else bitMap.Invalidate();
                    }
                    else bitMap.Invalidate();
                }
                else bitMap.Invalidate();

            }
        }

        private void BitMap_MouseUp(object sender, MouseEventArgs e)
        {
            // Prevents from going out of bitmap
            if (e.Button == MouseButtons.Right)
            {
                if (isPolygonSelected)
                {
                    var area = bitMap.ClientRectangle;
                    int maxXAdjustment = 0;
                    int maxYAdjustment = 0;
                    foreach (Vertex v in currentPolygon)
                    {
                        var (xValue, yValue) = GetAdjustment(ref area, v.point);
                        if (Math.Abs(maxXAdjustment) < Math.Abs(xValue))
                            maxXAdjustment = xValue;
                        if (Math.Abs(maxYAdjustment) < Math.Abs(yValue))
                            maxYAdjustment = yValue;
                    }

                    if (maxXAdjustment != 0 || maxYAdjustment != 0)
                    {
                        foreach (Vertex v in currentPolygon)
                        {
                            v.point.Offset(maxXAdjustment, maxYAdjustment);
                        }
                    }

                    isPolygonSelected = false;
                    bitMap.Invalidate();
                }
                else if (selectedEdgeVertex != null)
                {
                    var area = bitMap.ClientRectangle;
                    var (xValue1, yValue1) = GetAdjustment(ref area, selectedEdgeVertex.point);
                    var (xValue2, yValue2) = GetAdjustment(ref area, selectedEdgeVertex.next.point);

                    int maxXAdjustment = Math.Abs(xValue1) > Math.Abs(xValue2) ? xValue1 : xValue2;
                    int maxYAdjustment = Math.Abs(yValue1) > Math.Abs(yValue2) ? yValue1 : yValue2;

                    if (maxXAdjustment != 0 || maxYAdjustment != 0)
                    {
                        selectedEdgeVertex.point.Offset(maxXAdjustment, maxYAdjustment);
                        selectedEdgeVertex.next.point.Offset(maxXAdjustment, maxYAdjustment);
                    }

                    selectedEdgeVertex = null;
                    bitMap.Invalidate();
                }
                else if (selectedVertex != null)
                {
                    var area = bitMap.ClientRectangle;
                    var (xValue, yValue) = GetAdjustment(ref area, selectedVertex.point);
                    selectedVertex.point.Offset(xValue, yValue);
                    bitMap.Invalidate();
                }
            }
        }

        private void BitMap_MouseMove(object sender, MouseEventArgs e)
        {
            if (inAddPolygonMode)
            {
                bitMap.Invalidate();
            }
            else
            {
                if (selectedVertex != null && Control.MouseButtons == MouseButtons.Right)
                {
                    selectedVertex.point += (Size)e.Location - previousMouseDownLocation;
                    previousMouseDownLocation = (Size)e.Location;
                    bitMap.Invalidate();

                }
                else if (selectedEdgeVertex != null)
                {
                    var adjustment = (Size)e.Location - previousMouseDownLocation;
                    previousMouseDownLocation = (Size)e.Location;
                    selectedEdgeVertex.point += adjustment;
                    selectedEdgeVertex.next.point += adjustment;
                    bitMap.Invalidate();
                }
                else if (isPolygonSelected)
                {
                    var adjustment = (Size)e.Location - previousMouseDownLocation;
                    previousMouseDownLocation = (Size)e.Location;
                    foreach (Vertex v in currentPolygon)
                    {
                        v.point += adjustment;
                    }
                    bitMap.Invalidate();
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                
                if (selectedVertex != null)
                {
                    RemoveVertexButton_Click(null, null);
                }
                else
                {
                    DeletePolygonButton_Click(null, null);
                }
            }
        }

        // Right section
        /***************************************************************************************/
        private void RemoveVertexButton_Click(object sender, EventArgs e)
        {
            RemoveVertex();
        }

        private void DeletePolygonButton_Click(object sender, EventArgs e)
        {
            DeletePolygon();
            bitMap.Invalidate();
        }

        private void GeneratePolygonButton_Click(object sender, EventArgs e)
        {
            if (!TryParseAllParameters(out Point centre, out int numOfPoints, out int sideLength))
            {
                MessageBox.Show("Błąd", "Niewłaściwa wartość", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            polygons.Add(new LinkedList<Vertex> ( PolygonFactory.GetRegularPolygon(centre, numOfPoints, sideLength)));
            ClearAllSelection();
            GetNewCurrentPolygon();
            if (inAddPolygonMode)
            {
                TurnOffAddPolygonMode();
            }
            bitMap.Invalidate();
        }

        private bool TryParseAllParameters(out Point centre, out int numberOfPoints, out int sideLength)
        {
            centre = new Point();
            numberOfPoints = 0;
            sideLength = 0;

            if(!int.TryParse(centreXTextBox.Text, out int x))
            {
                return false;
            }
            if (!int.TryParse(centreYTextBox.Text, out int y))
            {
                return false;
            }
            centre.X = x;
            centre.Y = y;

            if (!int.TryParse(verticesNumberTextBox.Text, out numberOfPoints))
            {
                return false;
            }

            if (!int.TryParse(sideLengthTextBox.Text, out sideLength))
            {
                return false;
            }

            return true;
        }
    }
}
