namespace GraphEditor
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();

                canvas.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.bitMap = new System.Windows.Forms.PictureBox();
            this.menuControl = new System.Windows.Forms.TabControl();
            this.tab1 = new System.Windows.Forms.TabPage();
            this.RelationGroupBox = new System.Windows.Forms.GroupBox();
            this.perpendicularityPictureBox = new System.Windows.Forms.PictureBox();
            this.equalityPictureBox = new System.Windows.Forms.PictureBox();
            this.chooseRelationEdgeLabel = new System.Windows.Forms.Label();
            this.deleteRelationButton = new System.Windows.Forms.Button();
            this.deleteGroupBox = new System.Windows.Forms.GroupBox();
            this.DeleteVertexButton = new System.Windows.Forms.Button();
            this.DeletePolygonButton = new System.Windows.Forms.Button();
            this.tab2 = new System.Windows.Forms.TabPage();
            this.newPolygonGroupBox = new System.Windows.Forms.GroupBox();
            this.generatePolygonButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.sideLengthTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.verticesNumberTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.centreYTextBox = new System.Windows.Forms.TextBox();
            this.centreXTextBox = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.equalityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.perpendicularityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteRelationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bitMap)).BeginInit();
            this.menuControl.SuspendLayout();
            this.tab1.SuspendLayout();
            this.RelationGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.perpendicularityPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.equalityPictureBox)).BeginInit();
            this.deleteGroupBox.SuspendLayout();
            this.tab2.SuspendLayout();
            this.newPolygonGroupBox.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.bitMap, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.menuControl, 1, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // bitMap
            // 
            this.bitMap.BackColor = System.Drawing.Color.White;
            this.bitMap.Cursor = System.Windows.Forms.Cursors.Cross;
            resources.ApplyResources(this.bitMap, "bitMap");
            this.bitMap.Name = "bitMap";
            this.bitMap.TabStop = false;
            this.bitMap.Paint += new System.Windows.Forms.PaintEventHandler(this.BitMap_Paint);
            this.bitMap.MouseClick += new System.Windows.Forms.MouseEventHandler(this.BitMap_MouseClick);
            this.bitMap.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.BitMap_MouseDoubleClick);
            this.bitMap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BitMap_MouseDown);
            this.bitMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BitMap_MouseMove);
            this.bitMap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BitMap_MouseUp);
            // 
            // menuControl
            // 
            this.menuControl.Controls.Add(this.tab1);
            this.menuControl.Controls.Add(this.tab2);
            resources.ApplyResources(this.menuControl, "menuControl");
            this.menuControl.Name = "menuControl";
            this.menuControl.SelectedIndex = 0;
            // 
            // tab1
            // 
            this.tab1.BackColor = System.Drawing.SystemColors.Control;
            this.tab1.Controls.Add(this.RelationGroupBox);
            this.tab1.Controls.Add(this.deleteGroupBox);
            resources.ApplyResources(this.tab1, "tab1");
            this.tab1.Name = "tab1";
            // 
            // RelationGroupBox
            // 
            this.RelationGroupBox.BackColor = System.Drawing.SystemColors.Control;
            this.RelationGroupBox.Controls.Add(this.perpendicularityPictureBox);
            this.RelationGroupBox.Controls.Add(this.equalityPictureBox);
            this.RelationGroupBox.Controls.Add(this.chooseRelationEdgeLabel);
            this.RelationGroupBox.Controls.Add(this.deleteRelationButton);
            resources.ApplyResources(this.RelationGroupBox, "RelationGroupBox");
            this.RelationGroupBox.Name = "RelationGroupBox";
            this.RelationGroupBox.TabStop = false;
            // 
            // perpendicularityPictureBox
            // 
            this.perpendicularityPictureBox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.perpendicularityPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.perpendicularityPictureBox.Image = global::GraphEditor.Properties.Resources.Perpendicularity48;
            resources.ApplyResources(this.perpendicularityPictureBox, "perpendicularityPictureBox");
            this.perpendicularityPictureBox.Name = "perpendicularityPictureBox";
            this.perpendicularityPictureBox.TabStop = false;
            this.perpendicularityPictureBox.Click += new System.EventHandler(this.PerpendicularityPictureBox_Click);
            // 
            // equalityPictureBox
            // 
            this.equalityPictureBox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.equalityPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.equalityPictureBox.Image = global::GraphEditor.Properties.Resources.Equality48;
            resources.ApplyResources(this.equalityPictureBox, "equalityPictureBox");
            this.equalityPictureBox.Name = "equalityPictureBox";
            this.equalityPictureBox.TabStop = false;
            this.equalityPictureBox.Click += new System.EventHandler(this.EqualityPictureBox_Click);
            // 
            // chooseRelationEdgeLabel
            // 
            resources.ApplyResources(this.chooseRelationEdgeLabel, "chooseRelationEdgeLabel");
            this.chooseRelationEdgeLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chooseRelationEdgeLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chooseRelationEdgeLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.chooseRelationEdgeLabel.Name = "chooseRelationEdgeLabel";
            this.chooseRelationEdgeLabel.UseWaitCursor = true;
            // 
            // deleteRelationButton
            // 
            resources.ApplyResources(this.deleteRelationButton, "deleteRelationButton");
            this.deleteRelationButton.Name = "deleteRelationButton";
            this.deleteRelationButton.UseVisualStyleBackColor = true;
            this.deleteRelationButton.Click += new System.EventHandler(this.DeleteRelationButton_Click);
            // 
            // deleteGroupBox
            // 
            this.deleteGroupBox.BackColor = System.Drawing.SystemColors.Control;
            this.deleteGroupBox.Controls.Add(this.DeleteVertexButton);
            this.deleteGroupBox.Controls.Add(this.DeletePolygonButton);
            resources.ApplyResources(this.deleteGroupBox, "deleteGroupBox");
            this.deleteGroupBox.Name = "deleteGroupBox";
            this.deleteGroupBox.TabStop = false;
            // 
            // DeleteVertexButton
            // 
            resources.ApplyResources(this.DeleteVertexButton, "DeleteVertexButton");
            this.DeleteVertexButton.Name = "DeleteVertexButton";
            this.DeleteVertexButton.UseVisualStyleBackColor = true;
            this.DeleteVertexButton.Click += new System.EventHandler(this.DeleteVertexButton_Click);
            // 
            // DeletePolygonButton
            // 
            resources.ApplyResources(this.DeletePolygonButton, "DeletePolygonButton");
            this.DeletePolygonButton.Name = "DeletePolygonButton";
            this.DeletePolygonButton.UseVisualStyleBackColor = true;
            this.DeletePolygonButton.Click += new System.EventHandler(this.DeletePolygonButton_Click);
            // 
            // tab2
            // 
            this.tab2.BackColor = System.Drawing.SystemColors.Control;
            this.tab2.Controls.Add(this.newPolygonGroupBox);
            resources.ApplyResources(this.tab2, "tab2");
            this.tab2.Name = "tab2";
            // 
            // newPolygonGroupBox
            // 
            this.newPolygonGroupBox.Controls.Add(this.generatePolygonButton);
            this.newPolygonGroupBox.Controls.Add(this.label3);
            this.newPolygonGroupBox.Controls.Add(this.sideLengthTextBox);
            this.newPolygonGroupBox.Controls.Add(this.label2);
            this.newPolygonGroupBox.Controls.Add(this.verticesNumberTextBox);
            this.newPolygonGroupBox.Controls.Add(this.label1);
            this.newPolygonGroupBox.Controls.Add(this.centreYTextBox);
            this.newPolygonGroupBox.Controls.Add(this.centreXTextBox);
            resources.ApplyResources(this.newPolygonGroupBox, "newPolygonGroupBox");
            this.newPolygonGroupBox.Name = "newPolygonGroupBox";
            this.newPolygonGroupBox.TabStop = false;
            // 
            // generatePolygonButton
            // 
            resources.ApplyResources(this.generatePolygonButton, "generatePolygonButton");
            this.generatePolygonButton.Name = "generatePolygonButton";
            this.generatePolygonButton.UseVisualStyleBackColor = true;
            this.generatePolygonButton.Click += new System.EventHandler(this.GeneratePolygonButton_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // sideLengthTextBox
            // 
            resources.ApplyResources(this.sideLengthTextBox, "sideLengthTextBox");
            this.sideLengthTextBox.Name = "sideLengthTextBox";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // verticesNumberTextBox
            // 
            resources.ApplyResources(this.verticesNumberTextBox, "verticesNumberTextBox");
            this.verticesNumberTextBox.Name = "verticesNumberTextBox";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // centreYTextBox
            // 
            resources.ApplyResources(this.centreYTextBox, "centreYTextBox");
            this.centreYTextBox.Name = "centreYTextBox";
            // 
            // centreXTextBox
            // 
            resources.ApplyResources(this.centreXTextBox, "centreXTextBox");
            this.centreXTextBox.Name = "centreXTextBox";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.equalityToolStripMenuItem,
            this.perpendicularityToolStripMenuItem,
            this.deleteRelationToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            // 
            // equalityToolStripMenuItem
            // 
            this.equalityToolStripMenuItem.Name = "equalityToolStripMenuItem";
            resources.ApplyResources(this.equalityToolStripMenuItem, "equalityToolStripMenuItem");
            this.equalityToolStripMenuItem.Click += new System.EventHandler(this.EqualityToolStripMenuItem_Click);
            // 
            // perpendicularityToolStripMenuItem
            // 
            this.perpendicularityToolStripMenuItem.Name = "perpendicularityToolStripMenuItem";
            resources.ApplyResources(this.perpendicularityToolStripMenuItem, "perpendicularityToolStripMenuItem");
            this.perpendicularityToolStripMenuItem.Click += new System.EventHandler(this.PerpendicularityToolStripMenuItem_Click);
            // 
            // deleteRelationToolStripMenuItem
            // 
            resources.ApplyResources(this.deleteRelationToolStripMenuItem, "deleteRelationToolStripMenuItem");
            this.deleteRelationToolStripMenuItem.Name = "deleteRelationToolStripMenuItem";
            this.deleteRelationToolStripMenuItem.Click += new System.EventHandler(this.DeleteRelationToolStripMenuItem_Click);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bitMap)).EndInit();
            this.menuControl.ResumeLayout(false);
            this.tab1.ResumeLayout(false);
            this.RelationGroupBox.ResumeLayout(false);
            this.RelationGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.perpendicularityPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.equalityPictureBox)).EndInit();
            this.deleteGroupBox.ResumeLayout(false);
            this.tab2.ResumeLayout(false);
            this.newPolygonGroupBox.ResumeLayout(false);
            this.newPolygonGroupBox.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox bitMap;
        private System.Windows.Forms.TabControl menuControl;
        private System.Windows.Forms.TabPage tab1;
        private System.Windows.Forms.GroupBox RelationGroupBox;
        private System.Windows.Forms.Label chooseRelationEdgeLabel;
        private System.Windows.Forms.PictureBox perpendicularityPictureBox;
        private System.Windows.Forms.PictureBox equalityPictureBox;
        private System.Windows.Forms.GroupBox deleteGroupBox;
        private System.Windows.Forms.Button DeleteVertexButton;
        private System.Windows.Forms.Button DeletePolygonButton;
        private System.Windows.Forms.TabPage tab2;
        private System.Windows.Forms.GroupBox newPolygonGroupBox;
        private System.Windows.Forms.Button generatePolygonButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox sideLengthTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox verticesNumberTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox centreYTextBox;
        private System.Windows.Forms.TextBox centreXTextBox;
        private System.Windows.Forms.Button deleteRelationButton;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem equalityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem perpendicularityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteRelationToolStripMenuItem;
    }
}

