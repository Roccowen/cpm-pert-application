
namespace WinFormsQSBMOD
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.addWorkToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.delWorkToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.optimiseToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toBackToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripButton,
            this.openToolStripButton,
            this.toolStripSeparator1,
            this.addWorkToolStripButton,
            this.delWorkToolStripButton,
            this.toolStripSeparator2,
            this.optimiseToolStripButton,
            this.toBackToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(883, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = global::QSBMODWinForms.Properties.Resources._003_diskette;
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveToolStripButton.Text = "toolStripButton2";
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = global::QSBMODWinForms.Properties.Resources._004_open;
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.openToolStripButton.Text = "toolStripButton3";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // addWorkToolStripButton
            // 
            this.addWorkToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addWorkToolStripButton.Image = global::QSBMODWinForms.Properties.Resources._002_add;
            this.addWorkToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addWorkToolStripButton.Name = "addWorkToolStripButton";
            this.addWorkToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.addWorkToolStripButton.Text = "toolStripButton4";
            // 
            // delWorkToolStripButton
            // 
            this.delWorkToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.delWorkToolStripButton.Image = global::QSBMODWinForms.Properties.Resources._007_delete;
            this.delWorkToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.delWorkToolStripButton.Name = "delWorkToolStripButton";
            this.delWorkToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.delWorkToolStripButton.Text = "toolStripButton5";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // optimiseToolStripButton
            // 
            this.optimiseToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.optimiseToolStripButton.Image = global::QSBMODWinForms.Properties.Resources._005_bar_chart;
            this.optimiseToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.optimiseToolStripButton.Name = "optimiseToolStripButton";
            this.optimiseToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.optimiseToolStripButton.Text = "toolStripButton6";
            // 
            // toBackToolStripButton
            // 
            this.toBackToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toBackToolStripButton.Image = global::QSBMODWinForms.Properties.Resources._001_previous;
            this.toBackToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toBackToolStripButton.Name = "toBackToolStripButton";
            this.toBackToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.toBackToolStripButton.Text = "toolStripButton7";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(400, 0);
            this.ClientSize = new System.Drawing.Size(883, 470);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton addWorkToolStripButton;
        private System.Windows.Forms.ToolStripButton delWorkToolStripButton;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripButton toolStripButton7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton optimiseToolStripButton;
        private System.Windows.Forms.ToolStripButton toBackToolStripButton;
    }
}

