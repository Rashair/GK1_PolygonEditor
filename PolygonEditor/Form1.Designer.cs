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
                selectedVertexBrush.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.EditionGroupBox = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.generatePolygonButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.sideLengthTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.verticesNumberTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.centreYTextBox = new System.Windows.Forms.TextBox();
            this.centreXTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RemoveVertexButton = new System.Windows.Forms.Button();
            this.DeletePolygonButton = new System.Windows.Forms.Button();
            this.bitMap = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.EditionGroupBox.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bitMap)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.bitMap, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.EditionGroupBox, 0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // EditionGroupBox
            // 
            this.EditionGroupBox.Controls.Add(this.groupBox2);
            this.EditionGroupBox.Controls.Add(this.groupBox1);
            resources.ApplyResources(this.EditionGroupBox, "EditionGroupBox");
            this.EditionGroupBox.Name = "EditionGroupBox";
            this.EditionGroupBox.TabStop = false;
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.generatePolygonButton);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.sideLengthTextBox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.verticesNumberTextBox);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.centreYTextBox);
            this.groupBox2.Controls.Add(this.centreXTextBox);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RemoveVertexButton);
            this.groupBox1.Controls.Add(this.DeletePolygonButton);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // RemoveVertexButton
            // 
            resources.ApplyResources(this.RemoveVertexButton, "RemoveVertexButton");
            this.RemoveVertexButton.Name = "RemoveVertexButton";
            this.RemoveVertexButton.UseVisualStyleBackColor = true;
            this.RemoveVertexButton.Click += new System.EventHandler(this.RemoveVertexButton_Click);
            // 
            // DeletePolygonButton
            // 
            resources.ApplyResources(this.DeletePolygonButton, "DeletePolygonButton");
            this.DeletePolygonButton.Name = "DeletePolygonButton";
            this.DeletePolygonButton.UseVisualStyleBackColor = true;
            this.DeletePolygonButton.Click += new System.EventHandler(this.DeletePolygonButton_Click);
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
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.EditionGroupBox.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bitMap)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox bitMap;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.GroupBox EditionGroupBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox verticesNumberTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox centreYTextBox;
        private System.Windows.Forms.TextBox centreXTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button RemoveVertexButton;
        private System.Windows.Forms.Button DeletePolygonButton;
        private System.Windows.Forms.TextBox sideLengthTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button generatePolygonButton;
    }
}

