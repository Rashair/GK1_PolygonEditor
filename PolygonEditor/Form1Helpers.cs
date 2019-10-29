using GraphEditor.Relations;
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
        private const int relationIconSize = 12;

        private static readonly Size locationAdjustment = new Size(-Vertex.radius / 2, -Vertex.radius / 2);
        private Size previousMouseDownLocation;
        private Rectangle vertexRectangle;

        private Vertex selectedVertex;
        private Vertex selectedMoveEdgeVertex;
        private Vertex selectedRelationEdgeVertex;
        private Type selectedRelationType;
        private bool isPolygonSelected;

        private readonly List<LinkedList<Vertex>> polygons;
        private bool inAddPolygonMode;
        private LinkedList<Vertex> currentPolygon;

        private Font lockedFont = new Font("Arial", 18, FontStyle.Bold);
        private int lockedCount = 0;
        private bool CurrentPolygonLocked
        {
            get
            {
                return lockedCount > 0;
            }
        }

        private bool mouseDownAndUpDetached;


        /// Drawing polygons ---------------------------------------------------------------------------------
        private void DrawNormalPolygon(Graphics canvasGraphics, LinkedList<Vertex> polygon)
        {
            canvasGraphics.FillPolygon(Brushes.White, polygon.Select(v => v.Point).ToArray());
            foreach (var v in polygon)
            {
                canvas.DrawLine(v, v.next, Color.Black);
                if (v.IsInParentRelation())
                {
                    HandleDrawingRelationIcon(canvasGraphics, v);
                }
                vertexRectangle.Location = v.Point + locationAdjustment;
                canvasGraphics.FillEllipse(Brushes.Black, vertexRectangle);
            }
        }

        private void DrawSelectedPolygon(Graphics canvasGraphics)
        {
            canvasGraphics.FillPolygon(Brushes.LightGreen, currentPolygon.Select(v => v.Point).ToArray());
            foreach (Vertex v in currentPolygon)
            {
                canvas.DrawLine(v.Point, v.next.Point, GetEdgeColor(v));
                if (v.IsInParentRelation())
                {
                    HandleDrawingRelationIcon(canvasGraphics, v);
                }

                vertexRectangle.Location = v.Point + locationAdjustment;
                canvasGraphics.FillEllipse(v == selectedVertex ? Brushes.Red : Brushes.Black,
                    vertexRectangle);
                if (v.Locked)
                {
                    canvasGraphics.DrawString("X", lockedFont, Brushes.Blue, vertexRectangle.Location);
                }
            }
        }

        private void DrawUnfinishedPolygon(Graphics canvasGraphics)
        {
            var mousePos = bitMap.PointToClient(MousePosition);
            foreach (Vertex v in currentPolygon)
            {
                canvas.DrawLine(v.Point, v.next?.Point ?? mousePos, Color.Black);
                vertexRectangle.Location = v.Point + locationAdjustment;
                canvasGraphics.FillEllipse(Brushes.Black, vertexRectangle);
            }
        }

        private void HandleDrawingRelationIcon(Graphics graphics, Vertex v)
        {
            var p1 = v.Point;
            var p2 = v.next.Point;
            Point p = new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
            Rectangle rect = new Rectangle(p.X - relationIconSize / 2, p.Y - relationIconSize / 2, relationIconSize, relationIconSize);
            if (v.ParentRelation.GetType() == typeof(EqualLengthRelation))
            {
                graphics.DrawIcon(Properties.Resources.Equality481, rect);
            }
            else
            {
                graphics.DrawIcon(Properties.Resources.Perpendicularity481, rect);
            }
            graphics.DrawString(v.ParentRelation.RelationNumber.ToString(), this.Font, Brushes.DarkBlue,
                rect.X + rect.Width, rect.Y + rect.Height);
        }

        private Color GetEdgeColor(Vertex v)
        {
            return v == selectedRelationEdgeVertex ? Color.Blue : Color.Black;
        }

        /// End drawing polygons ---------------------------------------------------------------------------------


        /// Polygon data -------------------------------------------------------------------------------------
        private void AddNewPolygon(Point firstVertexLocation)
        {
            LinkedList<Vertex> newPolygon = new LinkedList<Vertex>();
            newPolygon.AddLast(new Vertex(firstVertexLocation));

            polygons.Add(newPolygon);
            currentPolygon = newPolygon;

            TurnOnAddPolygonMode();
        }

        private void ReverseIfNotClockwise()
        {
            var v1 = currentPolygon.First.Value;
            var det = Geometry.CrossProduct(v1, v1.next);
            if (det < 0)
            {
                currentPolygon.Reverse();
                foreach (Vertex v in currentPolygon)
                {
                    (v.next, v.prev) = (v.prev, v.next);
                }
            }
        }

        private void TurnOnAddPolygonMode()
        {
            inAddPolygonMode = true;
            ClearAllSelection();
            generatePolygonButton.Enabled = false;

            bitMap.MouseDown -= BitMap_MouseDown;
            bitMap.MouseUp -= BitMap_MouseUp;
        }

        private void TurnOffAddPolygonMode()
        {
            inAddPolygonMode = false;
            generatePolygonButton.Enabled = true;
            bitMap.MouseDown += BitMap_MouseDown;
            bitMap.MouseUp += BitMap_MouseUp;
            mouseDownAndUpDetached = false;
        }

        private void ClearAllSelection()
        {
            isPolygonSelected = false;
            selectedMoveEdgeVertex = null;
            selectedRelationEdgeVertex = null;
            selectedVertex = null;
            DeleteVertexButton.Enabled = false;
        }

        // ---------------------------------------------------
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
                    mouseDownUpAttach(true);
                }
            }
            else
            {
                currentPolygon = null;
                mouseDownUpAttach(false);
            }
        }

        private void mouseDownUpAttach(bool value)
        {
            if (value)
            {
                bitMap.MouseDown += BitMap_MouseDown;
                bitMap.MouseUp += BitMap_MouseUp;
                mouseDownAndUpDetached = false;
            }
            else
            {
                bitMap.MouseDown -= BitMap_MouseDown;
                bitMap.MouseUp -= BitMap_MouseUp;
                mouseDownAndUpDetached = true;
            }
        }

        // Generate polygon
        private bool TryParseAllParameters(out Point centre, out int numberOfPoints, out int sideLength)
        {
            centre = new Point();
            numberOfPoints = 0;
            sideLength = 0;

            if (!int.TryParse(centreXTextBox.Text, out int x))
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

        /// End polygon data ---------------------------------------------------------------------------------


        private Vertex GetVertexAtPosition(Point position)
        {
            Vertex result = null;
            double minDist = double.PositiveInfinity;
            foreach (Vertex v in currentPolygon)
            {
                if (Geometry.AreVerticesIntersecting(v.Point, position, Vertex.radius) ||
                    v.Point.Equals(position))
                {
                    double currDist = Geometry.Distance(v.Point, position);
                    if (minDist > currDist)
                    {
                        minDist = currDist;
                        result = v;
                    }
                }
            }

            return result;
        }

        private LinkedListNode<Vertex> GetEdgeVertex(Point position, double edgeEps = eps)
        {
            for (var currentNode = currentPolygon.First; currentNode != null; currentNode = currentNode.Next)
            {
                var vertex = currentNode.Value;
                double d1 = Geometry.Distance(vertex.Point, position);
                double d2 = Geometry.Distance(position, vertex.next.Point);
                double d = Geometry.Distance(vertex.Point, vertex.next.Point);
                if (Math.Abs(d1 + d2 - d) < edgeEps)
                {
                    return (currentNode);
                }
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


        // -------------------------------------------------------------------
        private void AddVertex(MouseEventArgs e)
        {
            LinkedListNode<Vertex> firstEdgeVertex = GetEdgeVertex(e.Location);
            if (firstEdgeVertex != null)
            {
                var v1 = firstEdgeVertex.Value;
                var v2 = v1.next;
                if (v1.IsInParentRelation())
                {
                    v1.InvokeOnRemoveRelation();
                }

                var newVertex = new Vertex(e.Location, v1, v2);
                v1.next = newVertex;
                v2.prev = newVertex;

                currentPolygon.AddAfter(firstEdgeVertex, newVertex);
                bitMap.Invalidate();
            }
        }

        private void RemoveVertex()
        {
            if (currentPolygon.Count > 3)
            {
                currentPolygon.Remove(selectedVertex);
                if (selectedVertex.IsInParentRelation())
                {
                    selectedVertex.InvokeOnRemoveRelation();
                }
                if (selectedVertex.prev.IsInParentRelation())
                {
                    selectedVertex.prev.InvokeOnRemoveRelation();
                }

                selectedVertex.next.prev = selectedVertex.prev;
                selectedVertex.prev.next = selectedVertex.next;

                selectedVertex = null;
                DeleteVertexButton.Enabled = false;
                bitMap.Invalidate();
            }
        }


        // Relations ---------------------------------------------------------------------------------

        private void ChooseRelation(Type chosenRelationType)
        {
            selectedRelationType = chosenRelationType;
            OnRelationBoxShow();
            if (selectedRelationEdgeVertex.IsInParentRelation())
            {
                if (contextMenuStrip1.Items[0].Enabled)
                {
                    NegateContextMenuItemsEnabled();
                }
                deleteRelationButton.Enabled = true;
                if (selectedRelationEdgeVertex.ParentRelation.GetType() != selectedRelationType)
                {
                    SwapRelationsForSelected();
                }
            }
            else
            {
                chooseRelationEdgeLabel.Visible = true;
            }

            RelationGroupBox.Invalidate();
            bitMap.Invalidate();
        }

        private void SwapRelationsForSelected()
        {
            var (v1, _, v3, _) = selectedRelationEdgeVertex.ParentRelation.GetMembersOfRelation();
            selectedRelationEdgeVertex.InvokeOnRemoveRelation();
            AddRelationForSelected(v1, v3);
        }

        private void AddRelationForSelected(Vertex v1, Vertex v3)
        {
            if(v1.Locked || v1.next.Locked || v3.Locked || v3.next.Locked)
            {
                MessageBox.Show("Nie można dodać relacji dla krawędzi z zablokowanym wierzchołkiem.", "Błąd", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else if (selectedRelationType == typeof(EqualLengthRelation))
            {
                _ = new EqualLengthRelation(v1, v3);
            }
            else
            {
                _ = new PerpendicularityRelation(v1, v3);
            }
        }

        private void OnRelationBoxShow()
        {
            var chooseColor = SystemColors.GradientActiveCaption;
            if (selectedRelationType == typeof(EqualLengthRelation))
            {
                equalityPictureBox.BackColor = chooseColor;
                perpendicularityPictureBox.BackColor = SystemColors.ButtonHighlight;
            }
            else
            {
                perpendicularityPictureBox.BackColor = chooseColor;
                equalityPictureBox.BackColor = SystemColors.ButtonHighlight;
            }

            deleteRelationButton.Enabled = false;
            chooseRelationEdgeLabel.Visible = false;

        }

        private void RelationBoxHide()
        {
            selectedRelationEdgeVertex = null;
            RelationGroupBox.Visible = false;
            OnChangingToEmptyRelation();
            RelationGroupBox.Invalidate();
        }

        private void OnChangingToEmptyRelation()
        {
            deleteRelationButton.Enabled = false;
            chooseRelationEdgeLabel.Visible = false;
            if (!contextMenuStrip1.Items[0].Enabled)
            {
                NegateContextMenuItemsEnabled();
            }

            perpendicularityPictureBox.BackColor = SystemColors.ButtonHighlight;
            equalityPictureBox.BackColor = SystemColors.ButtonHighlight;
        }

        private void NegateContextMenuItemsEnabled()
        {
            contextMenuStrip1.Items[0].Enabled = !contextMenuStrip1.Items[0].Enabled;
            contextMenuStrip1.Items[1].Enabled = !contextMenuStrip1.Items[1].Enabled;
            contextMenuStrip1.Items[2].Enabled = !contextMenuStrip1.Items[2].Enabled;
        }
    }
}
