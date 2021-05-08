
namespace QSBMODWinForms
{
    partial class ResultForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResultForm));
            this.dependencyPlot = new ZedGraph.ZedGraphControl();
            this.propabilityPlot = new ZedGraph.ZedGraphControl();
            this.resoltTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dependencyPlot
            // 
            this.dependencyPlot.Location = new System.Drawing.Point(19, 12);
            this.dependencyPlot.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dependencyPlot.Name = "dependencyPlot";
            this.dependencyPlot.ScrollGrace = 0D;
            this.dependencyPlot.ScrollMaxX = 0D;
            this.dependencyPlot.ScrollMaxY = 0D;
            this.dependencyPlot.ScrollMaxY2 = 0D;
            this.dependencyPlot.ScrollMinX = 0D;
            this.dependencyPlot.ScrollMinY = 0D;
            this.dependencyPlot.ScrollMinY2 = 0D;
            this.dependencyPlot.Size = new System.Drawing.Size(317, 317);
            this.dependencyPlot.TabIndex = 0;
            this.dependencyPlot.UseExtendedPrintDialog = true;
            // 
            // propabilityPlot
            // 
            this.propabilityPlot.Location = new System.Drawing.Point(394, 12);
            this.propabilityPlot.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.propabilityPlot.Name = "propabilityPlot";
            this.propabilityPlot.ScrollGrace = 0D;
            this.propabilityPlot.ScrollMaxX = 0D;
            this.propabilityPlot.ScrollMaxY = 0D;
            this.propabilityPlot.ScrollMaxY2 = 0D;
            this.propabilityPlot.ScrollMinX = 0D;
            this.propabilityPlot.ScrollMinY = 0D;
            this.propabilityPlot.ScrollMinY2 = 0D;
            this.propabilityPlot.Size = new System.Drawing.Size(317, 317);
            this.propabilityPlot.TabIndex = 1;
            this.propabilityPlot.UseExtendedPrintDialog = true;
            // 
            // resoltTextBox
            // 
            this.resoltTextBox.Location = new System.Drawing.Point(6, 22);
            this.resoltTextBox.Multiline = true;
            this.resoltTextBox.Name = "resoltTextBox";
            this.resoltTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.resoltTextBox.Size = new System.Drawing.Size(692, 311);
            this.resoltTextBox.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.resoltTextBox);
            this.groupBox1.Location = new System.Drawing.Point(13, 335);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(704, 339);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Отчет";
            // 
            // ResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 686);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.propabilityPlot);
            this.Controls.Add(this.dependencyPlot);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ResultForm";
            this.Text = "QSBMOD14 Отчет";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ResultForm_FormClosed);
            this.Load += new System.EventHandler(this.ResultForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ZedGraph.ZedGraphControl dependencyPlot;
        private ZedGraph.ZedGraphControl propabilityPlot;
        private System.Windows.Forms.TextBox resoltTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}