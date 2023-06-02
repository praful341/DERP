namespace DERP.Transaction
{
    partial class FrmSalesStock
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
            this.grdSalesStock = new DevExpress.XtraGrid.GridControl();
            this.dgvSalesStock = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.clmId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmSrNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.assort = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryLueAssort = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.Sieve = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryLueSieve = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.Carat = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Rate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Amount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmEntry = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmJkk = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmSales = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmAccount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmSieveID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmAssortId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmFlag = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelProgress = new DevExpress.XtraEditors.PanelControl();
            this.lblProgressCount = new System.Windows.Forms.Label();
            this.SaveProgressBar = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            this.backgroundWorker_SalesStock = new System.ComponentModel.BackgroundWorker();
            this.ContextMNExport = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MNExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportPDF = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportTEXT = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportHTML = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportRTF = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportCSV = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.grdSalesStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryLueAssort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryLueSieve)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelProgress)).BeginInit();
            this.panelProgress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SaveProgressBar.Properties)).BeginInit();
            this.ContextMNExport.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdSalesStock
            // 
            this.grdSalesStock.ContextMenuStrip = this.ContextMNExport;
            this.grdSalesStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSalesStock.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdSalesStock.Location = new System.Drawing.Point(0, 0);
            this.grdSalesStock.MainView = this.dgvSalesStock;
            this.grdSalesStock.Name = "grdSalesStock";
            this.grdSalesStock.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryLueAssort,
            this.repositoryLueSieve});
            this.grdSalesStock.Size = new System.Drawing.Size(1349, 742);
            this.grdSalesStock.TabIndex = 21;
            this.grdSalesStock.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvSalesStock});
            // 
            // dgvSalesStock
            // 
            this.dgvSalesStock.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.dgvSalesStock.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.dgvSalesStock.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            this.dgvSalesStock.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            this.dgvSalesStock.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.dgvSalesStock.Appearance.FooterPanel.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.dgvSalesStock.Appearance.FooterPanel.Options.UseFont = true;
            this.dgvSalesStock.Appearance.HeaderPanel.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.dgvSalesStock.Appearance.HeaderPanel.Options.UseFont = true;
            this.dgvSalesStock.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.dgvSalesStock.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dgvSalesStock.Appearance.Row.Font = new System.Drawing.Font("Cambria", 9F);
            this.dgvSalesStock.Appearance.Row.Options.UseFont = true;
            this.dgvSalesStock.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.clmId,
            this.clmSrNo,
            this.assort,
            this.Sieve,
            this.Carat,
            this.Rate,
            this.Amount,
            this.clmEntry,
            this.clmJkk,
            this.clmSales,
            this.clmAccount,
            this.clmSieveID,
            this.clmAssortId,
            this.clmFlag});
            this.dgvSalesStock.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.dgvSalesStock.GridControl = this.grdSalesStock;
            this.dgvSalesStock.Name = "dgvSalesStock";
            this.dgvSalesStock.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.dgvSalesStock.OptionsCustomization.AllowColumnMoving = false;
            this.dgvSalesStock.OptionsCustomization.AllowFilter = false;
            this.dgvSalesStock.OptionsCustomization.AllowGroup = false;
            this.dgvSalesStock.OptionsCustomization.AllowSort = false;
            this.dgvSalesStock.OptionsNavigation.EnterMoveNextColumn = true;
            this.dgvSalesStock.OptionsView.ColumnAutoWidth = false;
            this.dgvSalesStock.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.dgvSalesStock.OptionsView.ShowAutoFilterRow = true;
            this.dgvSalesStock.OptionsView.ShowFooter = true;
            this.dgvSalesStock.OptionsView.ShowGroupPanel = false;
            this.dgvSalesStock.CustomSummaryCalculate += new DevExpress.Data.CustomSummaryEventHandler(this.dgvSalesStock_CustomSummaryCalculate);
            this.dgvSalesStock.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.dgvSalesStock_InitNewRow);
            this.dgvSalesStock.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.dgvSalesStock_CellValueChanged);
            // 
            // clmId
            // 
            this.clmId.Caption = "ID";
            this.clmId.FieldName = "id";
            this.clmId.Name = "clmId";
            // 
            // clmSrNo
            // 
            this.clmSrNo.Caption = "Sr No";
            this.clmSrNo.FieldName = "sr_no";
            this.clmSrNo.Name = "clmSrNo";
            this.clmSrNo.OptionsColumn.AllowEdit = false;
            this.clmSrNo.OptionsColumn.AllowFocus = false;
            this.clmSrNo.OptionsColumn.AllowMove = false;
            this.clmSrNo.OptionsColumn.AllowSize = false;
            this.clmSrNo.Visible = true;
            this.clmSrNo.VisibleIndex = 0;
            // 
            // assort
            // 
            this.assort.Caption = "Assort";
            this.assort.ColumnEdit = this.repositoryLueAssort;
            this.assort.FieldName = "assort_id";
            this.assort.Name = "assort";
            this.assort.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.assort.Visible = true;
            this.assort.VisibleIndex = 1;
            this.assort.Width = 55;
            // 
            // repositoryLueAssort
            // 
            this.repositoryLueAssort.AutoHeight = false;
            this.repositoryLueAssort.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryLueAssort.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("assort_id", "Assort Id", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("assort_name", "Assort")});
            this.repositoryLueAssort.Name = "repositoryLueAssort";
            this.repositoryLueAssort.NullText = "";
            this.repositoryLueAssort.ShowHeader = false;
            this.repositoryLueAssort.Tag = "";
            this.repositoryLueAssort.UseCtrlScroll = false;
            // 
            // Sieve
            // 
            this.Sieve.Caption = "Sieve";
            this.Sieve.ColumnEdit = this.repositoryLueSieve;
            this.Sieve.FieldName = "sieve_id";
            this.Sieve.Name = "Sieve";
            this.Sieve.Visible = true;
            this.Sieve.VisibleIndex = 2;
            this.Sieve.Width = 45;
            // 
            // repositoryLueSieve
            // 
            this.repositoryLueSieve.AutoHeight = false;
            this.repositoryLueSieve.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryLueSieve.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("sieve_id", "Sieve Id", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("sieve_name", "Sieve")});
            this.repositoryLueSieve.Name = "repositoryLueSieve";
            this.repositoryLueSieve.NullText = "";
            this.repositoryLueSieve.ShowHeader = false;
            // 
            // Carat
            // 
            this.Carat.Caption = "Carat";
            this.Carat.FieldName = "carat";
            this.Carat.Name = "Carat";
            this.Carat.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.Carat.Visible = true;
            this.Carat.VisibleIndex = 3;
            this.Carat.Width = 48;
            // 
            // Rate
            // 
            this.Rate.Caption = "Rate";
            this.Rate.FieldName = "rate";
            this.Rate.Name = "Rate";
            this.Rate.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom)});
            this.Rate.Visible = true;
            this.Rate.VisibleIndex = 4;
            this.Rate.Width = 66;
            // 
            // Amount
            // 
            this.Amount.Caption = "Amount";
            this.Amount.FieldName = "amount";
            this.Amount.Name = "Amount";
            this.Amount.OptionsColumn.AllowEdit = false;
            this.Amount.OptionsColumn.AllowFocus = false;
            this.Amount.OptionsColumn.AllowMove = false;
            this.Amount.OptionsColumn.AllowSize = false;
            this.Amount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.Amount.Visible = true;
            this.Amount.VisibleIndex = 5;
            this.Amount.Width = 85;
            // 
            // clmEntry
            // 
            this.clmEntry.Caption = "Entry";
            this.clmEntry.FieldName = "remarks";
            this.clmEntry.Name = "clmEntry";
            this.clmEntry.Visible = true;
            this.clmEntry.VisibleIndex = 6;
            // 
            // clmJkk
            // 
            this.clmJkk.Caption = "JKK";
            this.clmJkk.FieldName = "special_remarks";
            this.clmJkk.Name = "clmJkk";
            this.clmJkk.Visible = true;
            this.clmJkk.VisibleIndex = 7;
            this.clmJkk.Width = 128;
            // 
            // clmSales
            // 
            this.clmSales.Caption = "Sales";
            this.clmSales.FieldName = "client_remarks";
            this.clmSales.Name = "clmSales";
            this.clmSales.Visible = true;
            this.clmSales.VisibleIndex = 8;
            this.clmSales.Width = 128;
            // 
            // clmAccount
            // 
            this.clmAccount.Caption = "Account Remark";
            this.clmAccount.FieldName = "payment_remarks";
            this.clmAccount.Name = "clmAccount";
            this.clmAccount.Visible = true;
            this.clmAccount.VisibleIndex = 9;
            this.clmAccount.Width = 117;
            // 
            // clmSieveID
            // 
            this.clmSieveID.Caption = "Sieve Id";
            this.clmSieveID.FieldName = "sieve_id";
            this.clmSieveID.Name = "clmSieveID";
            // 
            // clmAssortId
            // 
            this.clmAssortId.Caption = "Assort Id";
            this.clmAssortId.FieldName = "assort_id";
            this.clmAssortId.Name = "clmAssortId";
            // 
            // clmFlag
            // 
            this.clmFlag.Caption = "flag";
            this.clmFlag.FieldName = "flag";
            this.clmFlag.Name = "clmFlag";
            // 
            // panelProgress
            // 
            this.panelProgress.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelProgress.Controls.Add(this.lblProgressCount);
            this.panelProgress.Controls.Add(this.SaveProgressBar);
            this.panelProgress.Location = new System.Drawing.Point(402, 221);
            this.panelProgress.Name = "panelProgress";
            this.panelProgress.Size = new System.Drawing.Size(283, 58);
            this.panelProgress.TabIndex = 32;
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
            // backgroundWorker_SalesStock
            // 
            this.backgroundWorker_SalesStock.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_SalesStock_DoWork);
            this.backgroundWorker_SalesStock.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_SalesStock_RunWorkerCompleted);
            // 
            // ContextMNExport
            // 
            this.ContextMNExport.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.ContextMNExport.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MNExportExcel,
            this.MNExportPDF,
            this.MNExportTEXT,
            this.MNExportHTML,
            this.MNExportRTF,
            this.MNExportCSV});
            this.ContextMNExport.Name = "ContextExport";
            this.ContextMNExport.Size = new System.Drawing.Size(130, 136);
            // 
            // MNExportExcel
            // 
            this.MNExportExcel.Name = "MNExportExcel";
            this.MNExportExcel.Size = new System.Drawing.Size(152, 22);
            this.MNExportExcel.Text = "To Excel";
            this.MNExportExcel.Click += new System.EventHandler(this.MNExportExcel_Click);
            // 
            // MNExportPDF
            // 
            this.MNExportPDF.Name = "MNExportPDF";
            this.MNExportPDF.Size = new System.Drawing.Size(152, 22);
            this.MNExportPDF.Text = "To PDF";
            this.MNExportPDF.Click += new System.EventHandler(this.MNExportPDF_Click);
            // 
            // MNExportTEXT
            // 
            this.MNExportTEXT.Name = "MNExportTEXT";
            this.MNExportTEXT.Size = new System.Drawing.Size(152, 22);
            this.MNExportTEXT.Text = "To TEXT";
            this.MNExportTEXT.Click += new System.EventHandler(this.MNExportTEXT_Click);
            // 
            // MNExportHTML
            // 
            this.MNExportHTML.Name = "MNExportHTML";
            this.MNExportHTML.Size = new System.Drawing.Size(152, 22);
            this.MNExportHTML.Text = "To HTML";
            this.MNExportHTML.Click += new System.EventHandler(this.MNExportHTML_Click);
            // 
            // MNExportRTF
            // 
            this.MNExportRTF.Name = "MNExportRTF";
            this.MNExportRTF.Size = new System.Drawing.Size(152, 22);
            this.MNExportRTF.Text = "To RTF";
            this.MNExportRTF.Click += new System.EventHandler(this.MNExportRTF_Click);
            // 
            // MNExportCSV
            // 
            this.MNExportCSV.Name = "MNExportCSV";
            this.MNExportCSV.Size = new System.Drawing.Size(152, 22);
            this.MNExportCSV.Text = "To CSV";
            this.MNExportCSV.Click += new System.EventHandler(this.MNExportCSV_Click);
            // 
            // FrmSalesStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1349, 742);
            this.Controls.Add(this.panelProgress);
            this.Controls.Add(this.grdSalesStock);
            this.Name = "FrmSalesStock";
            this.Text = "Sales Stock";
            this.Load += new System.EventHandler(this.FrmSalesStock_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmSalesStock_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.grdSalesStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryLueAssort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryLueSieve)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelProgress)).EndInit();
            this.panelProgress.ResumeLayout(false);
            this.panelProgress.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SaveProgressBar.Properties)).EndInit();
            this.ContextMNExport.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdSalesStock;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvSalesStock;
        private DevExpress.XtraGrid.Columns.GridColumn assort;
        private DevExpress.XtraGrid.Columns.GridColumn Sieve;
        private DevExpress.XtraGrid.Columns.GridColumn Carat;
        private DevExpress.XtraGrid.Columns.GridColumn Rate;
        private DevExpress.XtraGrid.Columns.GridColumn Amount;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryLueAssort;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryLueSieve;
        private DevExpress.XtraGrid.Columns.GridColumn clmJkk;
        private DevExpress.XtraGrid.Columns.GridColumn clmSales;
        private DevExpress.XtraGrid.Columns.GridColumn clmAccount;
        private DevExpress.XtraGrid.Columns.GridColumn clmId;
        private DevExpress.XtraGrid.Columns.GridColumn clmSrNo;
        private DevExpress.XtraGrid.Columns.GridColumn clmSieveID;
        private DevExpress.XtraGrid.Columns.GridColumn clmAssortId;
        private DevExpress.XtraGrid.Columns.GridColumn clmEntry;
        private DevExpress.XtraEditors.PanelControl panelProgress;
        private System.Windows.Forms.Label lblProgressCount;
        private DevExpress.XtraEditors.MarqueeProgressBarControl SaveProgressBar;
        private System.ComponentModel.BackgroundWorker backgroundWorker_SalesStock;
        private DevExpress.XtraGrid.Columns.GridColumn clmFlag;
        private System.Windows.Forms.ContextMenuStrip ContextMNExport;
        private System.Windows.Forms.ToolStripMenuItem MNExportExcel;
        private System.Windows.Forms.ToolStripMenuItem MNExportPDF;
        private System.Windows.Forms.ToolStripMenuItem MNExportTEXT;
        private System.Windows.Forms.ToolStripMenuItem MNExportHTML;
        private System.Windows.Forms.ToolStripMenuItem MNExportRTF;
        private System.Windows.Forms.ToolStripMenuItem MNExportCSV;
    }
}