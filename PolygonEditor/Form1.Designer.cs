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
                selectedEdgeBrush.Dispose();
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
            this.ClearGraphButton = new System.Windows.Forms.Button();
            this.RemoveVertexButton = new System.Windows.Forms.Button();
            this.bitMap = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.EditionGroupBox.SuspendLayout();
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
            resources.ApplyResources(this.EditionGroupBox, "EditionGroupBox");
            this.EditionGroupBox.Controls.Add(this.ClearGraphButton);
            this.EditionGroupBox.Controls.Add(this.RemoveVertexButton);
            this.EditionGroupBox.Name = "EditionGroupBox";
            this.EditionGroupBox.TabStop = false;
            // 
            // ClearGraphButton
            // 
            resources.ApplyResources(this.ClearGraphButton, "ClearGraphButton");
            this.ClearGraphButton.Name = "ClearGraphButton";
            this.ClearGraphButton.UseVisualStyleBackColor = true;
            this.ClearGraphButton.Click += new System.EventHandler(this.ClearGraphButton_Click);
            // 
            // RemoveVertexButton
            // 
            resources.ApplyResources(this.RemoveVertexButton, "RemoveVertexButton");
            this.RemoveVertexButton.Name = "RemoveVertexButton";
            this.RemoveVertexButton.UseVisualStyleBackColor = true;
            this.RemoveVertexButton.Click += new System.EventHandler(this.RemoveVertexButton_Click);
            // 
            // bitMap
            // 
            resources.ApplyResources(this.bitMap, "bitMap");
            this.bitMap.BackColor = System.Drawing.Color.White;
            this.bitMap.Cursor = System.Windows.Forms.Cursors.Cross;
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
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.EditionGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bitMap)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.GroupBox EditionGroupBox;
        private System.Windows.Forms.PictureBox bitMap;
        private System.Windows.Forms.Button RemoveVertexButton;
        private System.Windows.Forms.Button ClearGraphButton;
    }
}

