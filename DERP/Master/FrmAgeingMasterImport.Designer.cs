namespace DERP.Transaction
{
    partial class FrmAgeingMasterImport
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
            this.PanelSave = new DevExpress.XtraEditors.PanelControl();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.grdAgeingImport = new DevExpress.XtraGrid.GridControl();
            this.dgvAgeingImport = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ClmAssort = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmSieve = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnBrowse = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblFormatSample = new DevExpress.XtraEditors.LabelControl();
            this.txtFileName = new DevExpress.XtraEditors.TextEdit();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.panelProgress = new DevExpress.XtraEditors.PanelControl();
            this.lblProgressCount = new System.Windows.Forms.Label();
            this.SaveProgressBar = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            this.backgroundWorker_AgeingImport = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.PanelSave)).BeginInit();
            this.PanelSave.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAgeingImport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAgeingImport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelProgress)).BeginInit();
            this.panelProgress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SaveProgressBar.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelSave
            // 
            this.PanelSave.Controls.Add(this.btnExit);
            this.PanelSave.Controls.Add(this.btnClear);
            this.PanelSave.Controls.Add(this.btnSave);
            this.PanelSave.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelSave.Location = new System.Drawing.Point(0, 692);
            this.PanelSave.Name = "PanelSave";
            this.PanelSave.Size = new System.Drawing.Size(1069, 50);
            this.PanelSave.TabIndex = 19;
            // 
            // btnExit
            // 
            this.btnExit.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Appearance.Options.UseFont = true;
            this.btnExit.ImageOptions.Image = global::DERP.Properties.Resources.Exit;
            this.btnExit.Location = new System.Drawing.Point(228, 8);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(102, 32);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "E&xit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnClear
            // 
            this.btnClear.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Appearance.Options.UseFont = true;
            this.btnClear.ImageOptions.Image = global::DERP.Properties.Resources.Clear;
            this.btnClear.Location = new System.Drawing.Point(120, 8);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(102, 32);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "&Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.ImageOptions.Image = global::DERP.Properties.Resources.Save;
            this.btnSave.Location = new System.Drawing.Point(12, 8);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(102, 32);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "&Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grdAgeingImport
            // 
            this.grdAgeingImport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdAgeingImport.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdAgeingImport.Location = new System.Drawing.Point(2, 2);
            this.grdAgeingImport.MainView = this.dgvAgeingImport;
            this.grdAgeingImport.Name = "grdAgeingImport";
            this.grdAgeingImport.Size = new System.Drawing.Size(1065, 642);
            this.grdAgeingImport.TabIndex = 20;
            this.grdAgeingImport.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvAgeingImport});
            // 
            // dgvAgeingImport
            // 
            this.dgvAgeingImport.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.dgvAgeingImport.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.dgvAgeingImport.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            this.dgvAgeingImport.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            this.dgvAgeingImport.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.dgvAgeingImport.Appearance.FooterPanel.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.dgvAgeingImport.Appearance.FooterPanel.Options.UseFont = true;
            this.dgvAgeingImport.Appearance.HeaderPanel.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.dgvAgeingImport.Appearance.HeaderPanel.Options.UseFont = true;
            this.dgvAgeingImport.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.dgvAgeingImport.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dgvAgeingImport.Appearance.Row.Font = new System.Drawing.Font("Cambria", 9F);
            this.dgvAgeingImport.Appearance.Row.Options.UseFont = true;
            this.dgvAgeingImport.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ClmAssort,
            this.clmSieve,
            this.clmRate});
            this.dgvAgeingImport.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.dgvAgeingImport.GridControl = this.grdAgeingImport;
            this.dgvAgeingImport.Name = "dgvAgeingImport";
            this.dgvAgeingImport.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.dgvAgeingImport.OptionsCustomization.AllowColumnMoving = false;
            this.dgvAgeingImport.OptionsCustomization.AllowFilter = false;
            this.dgvAgeingImport.OptionsCustomization.AllowGroup = false;
            this.dgvAgeingImport.OptionsCustomization.AllowSort = false;
            this.dgvAgeingImport.OptionsNavigation.EnterMoveNextColumn = true;
            this.dgvAgeingImport.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.dgvAgeingImport.OptionsView.ShowAutoFilterRow = true;
            this.dgvAgeingImport.OptionsView.ShowFooter = true;
            this.dgvAgeingImport.OptionsView.ShowGroupPanel = false;
            // 
            // ClmAssort
            // 
            this.ClmAssort.Caption = "Assort";
            this.ClmAssort.FieldName = "assort";
            this.ClmAssort.Name = "ClmAssort";
            this.ClmAssort.OptionsColumn.AllowEdit = false;
            this.ClmAssort.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "assort", "{0}")});
            this.ClmAssort.Visible = true;
            this.ClmAssort.VisibleIndex = 0;
            // 
            // clmSieve
            // 
            this.clmSieve.Caption = "Sieve";
            this.clmSieve.FieldName = "sieve";
            this.clmSieve.Name = "clmSieve";
            this.clmSieve.OptionsColumn.AllowEdit = false;
            this.clmSieve.Visible = true;
            this.clmSieve.VisibleIndex = 1;
            // 
            // clmRate
            // 
            this.clmRate.Caption = "Carat";
            this.clmRate.FieldName = "carat";
            this.clmRate.Name = "clmRate";
            this.clmRate.OptionsColumn.AllowEdit = false;
            this.clmRate.Visible = true;
            this.clmRate.VisibleIndex = 2;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.Appearance.Options.UseFont = true;
            this.btnBrowse.ImageOptions.Image = global::DERP.Properties.Resources.Upload_final;
            this.btnBrowse.Location = new System.Drawing.Point(26, 8);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(102, 32);
            this.btnBrowse.TabIndex = 453;
            this.btnBrowse.Text = "&Browse";
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lblFormatSample);
            this.panelControl1.Controls.Add(this.txtFileName);
            this.panelControl1.Controls.Add(this.btnBrowse);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1069, 46);
            this.panelControl1.TabIndex = 18;
            // 
            // lblFormatSample
            // 
            this.lblFormatSample.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormatSample.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblFormatSample.Appearance.Options.UseFont = true;
            this.lblFormatSample.Appearance.Options.UseForeColor = true;
            this.lblFormatSample.Location = new System.Drawing.Point(654, 11);
            this.lblFormatSample.Name = "lblFormatSample";
            this.lblFormatSample.Size = new System.Drawing.Size(168, 13);
            this.lblFormatSample.TabIndex = 460;
            this.lblFormatSample.Text = "Click For Excel Format Sample";
            this.lblFormatSample.Click += new System.EventHandler(this.lblFormatSample_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.EnterMoveNextControl = true;
            this.txtFileName.Location = new System.Drawing.Point(866, 10);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFileName.Properties.Appearance.Options.UseFont = true;
            this.txtFileName.Size = new System.Drawing.Size(187, 20);
            this.txtFileName.TabIndex = 455;
            this.txtFileName.Visible = false;
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.panelProgress);
            this.panelControl3.Controls.Add(this.grdAgeingImport);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(0, 46);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(1069, 646);
            this.panelControl3.TabIndex = 21;
            // 
            // panelProgress
            // 
            this.panelProgress.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelProgress.Controls.Add(this.lblProgressCount);
            this.panelProgress.Controls.Add(this.SaveProgressBar);
            this.panelProgress.Location = new System.Drawing.Point(332, 230);
            this.panelProgress.Name = "panelProgress";
            this.panelProgress.Size = new System.Drawing.Size(283, 58);
            this.panelProgress.TabIndex = 22;
            this.panelProgress.Visible = false;
            // 
            // lblProgressCount
            // 
            this.lblProgressCount.AutoSize = true;
            this.lblProgressCount.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblProgressCount.Location = new System.Drawing.Point(76, 37);
            this.lblProgressCount.Name = "lblProgressCount";
            this.lblProgressCount.Size = new System.Drawing.Size(0, 13);
            this.lblProgressCount.TabIndex = 1;
            // 
            // SaveProgressBar
            // 
            this.SaveProgressBar.EditValue = 0;
            this.SaveProgressBar.Location = new System.Drawing.Point(5, 5);
            this.SaveProgressBar.Name = "SaveProgressBar";
            this.SaveProgressBar.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.SaveProgressBar.Properties.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.SaveProgressBar.Properties.Appearance.BorderColor = System.Drawing.Color.Navy;
            this.SaveProgressBar.Properties.Appearance.ForeColor = System.Drawing.Color.Fuchsia;
            this.SaveProgressBar.Properties.LookAndFeel.SkinMaskColor = System.Drawing.Color.Lime;
            this.SaveProgressBar.Properties.LookAndFeel.SkinMaskColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.SaveProgressBar.Properties.LookAndFeel.SkinName = "Office 2013 Dark Gray";
            this.SaveProgressBar.Properties.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D;
            this.SaveProgressBar.Size = new System.Drawing.Size(273, 25);
            this.SaveProgressBar.TabIndex = 0;
            // 
            // backgroundWorker_AgeingImport
            // 
            this.backgroundWorker_AgeingImport.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_PriceImport_DoWork);
            this.backgroundWorker_AgeingImport.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_PriceImport_RunWorkerCompleted);
            // 
            // FrmAgeingMasterImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1069, 742);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.PanelSave);
            this.Controls.Add(this.panelControl1);
            this.Name = "FrmAgeingMasterImport";
            this.Text = "Ageing Import";
            this.Load += new System.EventHandler(this.FrmPriceImport_Load);
            this.Shown += new System.EventHandler(this.FrmPriceImport_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.PanelSave)).EndInit();
            this.PanelSave.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdAgeingImport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAgeingImport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelProgress)).EndInit();
            this.panelProgress.ResumeLayout(false);
            this.panelProgress.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SaveProgressBar.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl PanelSave;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraGrid.GridControl grdAgeingImport;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvAgeingImport;
        private DevExpress.XtraGrid.Columns.GridColumn clmSieve;
        private DevExpress.XtraGrid.Columns.GridColumn ClmAssort;
        private DevExpress.XtraGrid.Columns.GridColumn clmRate;
        private DevExpress.XtraEditors.SimpleButton btnBrowse;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.TextEdit txtFileName;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.LabelControl lblFormatSample;
        private DevExpress.XtraEditors.PanelControl panelProgress;
        private System.Windows.Forms.Label lblProgressCount;
        private DevExpress.XtraEditors.MarqueeProgressBarControl SaveProgressBar;
        private System.ComponentModel.BackgroundWorker backgroundWorker_AgeingImport;
    }
}