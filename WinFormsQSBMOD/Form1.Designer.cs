
namespace QSBMODWinForms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.mainFormToolStrip = new System.Windows.Forms.ToolStrip();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.addWorkToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.delWorkToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.optimiseToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.fullOptimisationToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.resultToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toBackToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.errorStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.mainFormToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainFormToolStrip
            // 
            this.mainFormToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripButton,
            this.addWorkToolStripButton,
            this.toolStripSeparator1,
            this.delWorkToolStripButton,
            this.saveToolStripButton,
            this.toolStripSeparator2,
            this.optimiseToolStripButton,
            this.fullOptimisationToolStripButton,
            this.toolStripSeparator3,
            this.resultToolStripButton,
            this.toBackToolStripButton,
            this.errorStripLabel});
            this.mainFormToolStrip.Location = new System.Drawing.Point(0, 0);
            this.mainFormToolStrip.Name = "mainFormToolStrip";
            this.mainFormToolStrip.Size = new System.Drawing.Size(883, 25);
            this.mainFormToolStrip.TabIndex = 0;
            this.mainFormToolStrip.Text = "toolStrip1";
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = global::QSBMODWinForms.Properties.Resources._004_open;
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.openToolStripButton.Text = "Открыть модель";
            // 
            // addWorkToolStripButton
            // 
            this.addWorkToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addWorkToolStripButton.Image = global::QSBMODWinForms.Properties.Resources._002_add;
            this.addWorkToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addWorkToolStripButton.Name = "addWorkToolStripButton";
            this.addWorkToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.addWorkToolStripButton.Text = "Добавить работу";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // delWorkToolStripButton
            // 
            this.delWorkToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.delWorkToolStripButton.Enabled = false;
            this.delWorkToolStripButton.Image = global::QSBMODWinForms.Properties.Resources._007_delete;
            this.delWorkToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.delWorkToolStripButton.Name = "delWorkToolStripButton";
            this.delWorkToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.delWorkToolStripButton.Text = "Удалить работу";
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Enabled = false;
            this.saveToolStripButton.Image = global::QSBMODWinForms.Properties.Resources._003_diskette;
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveToolStripButton.Text = "Сохранить модель";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // optimiseToolStripButton
            // 
            this.optimiseToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.optimiseToolStripButton.Enabled = false;
            this.optimiseToolStripButton.Image = global::QSBMODWinForms.Properties.Resources._005_bar_chart;
            this.optimiseToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.optimiseToolStripButton.Name = "optimiseToolStripButton";
            this.optimiseToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.optimiseToolStripButton.Text = "Оптимизация на 1";
            // 
            // fullOptimisationToolStripButton
            // 
            this.fullOptimisationToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.fullOptimisationToolStripButton.Enabled = false;
            this.fullOptimisationToolStripButton.Image = global::QSBMODWinForms.Properties.Resources._008_bar_chart_red;
            this.fullOptimisationToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fullOptimisationToolStripButton.Name = "fullOptimisationToolStripButton";
            this.fullOptimisationToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.fullOptimisationToolStripButton.Text = "Полная оптимизация";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // resultToolStripButton
            // 
            this.resultToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.resultToolStripButton.Enabled = false;
            this.resultToolStripButton.Image = global::QSBMODWinForms.Properties.Resources._009_research;
            this.resultToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.resultToolStripButton.Name = "resultToolStripButton";
            this.resultToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.resultToolStripButton.Text = "Отчет по оптимизации";
            // 
            // toBackToolStripButton
            // 
            this.toBackToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toBackToolStripButton.Enabled = false;
            this.toBackToolStripButton.Image = global::QSBMODWinForms.Properties.Resources._001_previous;
            this.toBackToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toBackToolStripButton.Name = "toBackToolStripButton";
            this.toBackToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.toBackToolStripButton.Text = "Отменить последнее изменение";
            // 
            // errorStripLabel
            // 
            this.errorStripLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.errorStripLabel.Font = new System.Drawing.Font("Leelawadee UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.errorStripLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.errorStripLabel.Name = "errorStripLabel";
            this.errorStripLabel.Size = new System.Drawing.Size(0, 22);

            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(400, 0);
            this.ClientSize = new System.Drawing.Size(883, 470);
            this.Controls.Add(this.mainFormToolStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "QSBMOD14";
            this.mainFormToolStrip.ResumeLayout(false);
            this.mainFormToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton addWorkToolStripButton;
        private System.Windows.Forms.ToolStripButton delWorkToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton optimiseToolStripButton;
        private System.Windows.Forms.ToolStripButton toBackToolStripButton;
        private System.Windows.Forms.ToolStripButton fullOptimisationToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton resultToolStripButton;
        private System.Windows.Forms.ToolStrip mainFormToolStrip;
        private System.Windows.Forms.ToolStripLabel errorStripLabel;
    }
}

