
namespace QSBMODWinFormsNF
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
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
            this.errorStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.tableRowsFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.errorStripLabel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = global::QSBMODWinFormsNF.Properties.Resources._004_open;
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.openToolStripButton.Text = "Открыть модель";
            this.openToolStripButton.Click += new System.EventHandler(this.OpenToolStripButton_Click);
            // 
            // addWorkToolStripButton
            // 
            this.addWorkToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addWorkToolStripButton.Image = global::QSBMODWinFormsNF.Properties.Resources._002_add;
            this.addWorkToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addWorkToolStripButton.Name = "addWorkToolStripButton";
            this.addWorkToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.addWorkToolStripButton.Text = "Добавить работу";
            this.addWorkToolStripButton.Click += new System.EventHandler(this.AddWorkToolStripButton_Click);
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
            this.delWorkToolStripButton.Image = global::QSBMODWinFormsNF.Properties.Resources._007_delete;
            this.delWorkToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.delWorkToolStripButton.Name = "delWorkToolStripButton";
            this.delWorkToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.delWorkToolStripButton.Text = "Удалить работу";
            this.delWorkToolStripButton.Click += new System.EventHandler(this.DelWorkToolStripButton_Click);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Enabled = false;
            this.saveToolStripButton.Image = global::QSBMODWinFormsNF.Properties.Resources._003_diskette;
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveToolStripButton.Text = "Сохранить модель";
            this.saveToolStripButton.Click += new System.EventHandler(this.SaveToolStripButton_Click);
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
            this.optimiseToolStripButton.Image = global::QSBMODWinFormsNF.Properties.Resources._005_bar_chart;
            this.optimiseToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.optimiseToolStripButton.Name = "optimiseToolStripButton";
            this.optimiseToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.optimiseToolStripButton.Text = "Оптимизация на 1 день";
            this.optimiseToolStripButton.Click += new System.EventHandler(this.OptimiseToolStripButton_Click);
            // 
            // fullOptimisationToolStripButton
            // 
            this.fullOptimisationToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.fullOptimisationToolStripButton.Enabled = false;
            this.fullOptimisationToolStripButton.Image = global::QSBMODWinFormsNF.Properties.Resources._008_bar_chart_red;
            this.fullOptimisationToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fullOptimisationToolStripButton.Name = "fullOptimisationToolStripButton";
            this.fullOptimisationToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.fullOptimisationToolStripButton.Text = "Полная оптимизация";
            this.fullOptimisationToolStripButton.Click += new System.EventHandler(this.FullOptimisationToolStripButton_Click);
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
            this.resultToolStripButton.Image = global::QSBMODWinFormsNF.Properties.Resources._009_research;
            this.resultToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.resultToolStripButton.Name = "resultToolStripButton";
            this.resultToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.resultToolStripButton.Text = "Отчет";
            this.resultToolStripButton.Click += new System.EventHandler(this.ResultToolStripButton_Click);
            // 
            // errorStripLabel
            // 
            this.errorStripLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.errorStripLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.errorStripLabel.Name = "errorStripLabel";
            this.errorStripLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.errorStripLabel.Size = new System.Drawing.Size(0, 22);
            // 
            // tableRowsFlowPanel
            // 
            this.tableRowsFlowPanel.AutoScroll = true;
            this.tableRowsFlowPanel.Location = new System.Drawing.Point(12, 56);
            this.tableRowsFlowPanel.Name = "tableRowsFlowPanel";
            this.tableRowsFlowPanel.Size = new System.Drawing.Size(776, 382);
            this.tableRowsFlowPanel.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableRowsFlowPanel);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "QSBMOD6";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton addWorkToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton delWorkToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton optimiseToolStripButton;
        private System.Windows.Forms.ToolStripButton fullOptimisationToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton resultToolStripButton;
        private System.Windows.Forms.ToolStripLabel errorStripLabel;
        private System.Windows.Forms.FlowLayoutPanel tableRowsFlowPanel;
    }
}

