namespace DERP.Transaction.MFG
{
    partial class FrmPriceImportPurityWise
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
            this.PanelSave = new DevExpress.XtraEditors.PanelControl();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.grdPriceImportPurityWise = new DevExpress.XtraGrid.GridControl();
            this.ContextMNExport = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MNExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportPDF = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportTEXT = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportHTML = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportRTF = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportCSV = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvPriceImportPurityWise = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ClmPurity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.RepLuePurity = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.clmSieve = new DevExpress.XtraGrid.Columns.GridColumn();
            this.RepLueSieve = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.clmCarat = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmPurityName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmSievename = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ClmPerPcs = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnBrowse = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblFormatSample = new DevExpress.XtraEditors.LabelControl();
            this.lueCurrency = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.lueRateType = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtFileName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.dtpDate = new DevExpress.XtraEditors.DateEdit();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.panelProgress = new DevExpress.XtraEditors.PanelControl();
            this.lblProgressCount = new System.Windows.Forms.Label();
            this.SaveProgressBar = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            this.backgroundWorker_PriceImportPurityWise = new System.ComponentModel.BackgroundWorker();
            this.clmPerJanged = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.PanelSave)).BeginInit();
            this.PanelSave.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPriceImportPurityWise)).BeginInit();
            this.ContextMNExport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPriceImportPurityWise)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepLuePurity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepLueSieve)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueCurrency.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueRateType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDate.Properties)).BeginInit();
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
            this.btnExit.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Appearance.Options.UseFont = true;
            this.btnExit.ImageOptions.Image = global::DERP.Properties.Resources.Exit;
            this.btnExit.Location = new System.Drawing.Point(264, 8);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(120, 32);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "E&xit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnClear
            // 
            this.btnClear.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Appearance.Options.UseFont = true;
            this.btnClear.ImageOptions.Image = global::DERP.Properties.Resources.Clear;
            this.btnClear.Location = new System.Drawing.Point(138, 8);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(120, 32);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "&Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.ImageOptions.Image = global::DERP.Properties.Resources.Save;
            this.btnSave.Location = new System.Drawing.Point(12, 8);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 32);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "&Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grdPriceImportPurityWise
            // 
            this.grdPriceImportPurityWise.ContextMenuStrip = this.ContextMNExport;
            this.grdPriceImportPurityWise.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPriceImportPurityWise.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdPriceImportPurityWise.Location = new System.Drawing.Point(2, 2);
            this.grdPriceImportPurityWise.MainView = this.dgvPriceImportPurityWise;
            this.grdPriceImportPurityWise.Name = "grdPriceImportPurityWise";
            this.grdPriceImportPurityWise.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.RepLuePurity,
            this.RepLueSieve});
            this.grdPriceImportPurityWise.Size = new System.Drawing.Size(1065, 642);
            this.grdPriceImportPurityWise.TabIndex = 20;
            this.grdPriceImportPurityWise.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvPriceImportPurityWise});
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
            this.MNExportExcel.Size = new System.Drawing.Size(129, 22);
            this.MNExportExcel.Text = "To Excel";
            this.MNExportExcel.Click += new System.EventHandler(this.MNExportExcel_Click);
            // 
            // MNExportPDF
            // 
            this.MNExportPDF.Name = "MNExportPDF";
            this.MNExportPDF.Size = new System.Drawing.Size(129, 22);
            this.MNExportPDF.Text = "To PDF";
            this.MNExportPDF.Click += new System.EventHandler(this.MNExportPDF_Click);
            // 
            // MNExportTEXT
            // 
            this.MNExportTEXT.Name = "MNExportTEXT";
            this.MNExportTEXT.Size = new System.Drawing.Size(129, 22);
            this.MNExportTEXT.Text = "To TEXT";
            this.MNExportTEXT.Click += new System.EventHandler(this.MNExportTEXT_Click);
            // 
            // MNExportHTML
            // 
            this.MNExportHTML.Name = "MNExportHTML";
            this.MNExportHTML.Size = new System.Drawing.Size(129, 22);
            this.MNExportHTML.Text = "To HTML";
            this.MNExportHTML.Click += new System.EventHandler(this.MNExportHTML_Click);
            // 
            // MNExportRTF
            // 
            this.MNExportRTF.Name = "MNExportRTF";
            this.MNExportRTF.Size = new System.Drawing.Size(129, 22);
            this.MNExportRTF.Text = "To RTF";
            this.MNExportRTF.Click += new System.EventHandler(this.MNExportRTF_Click);
            // 
            // MNExportCSV
            // 
            this.MNExportCSV.Name = "MNExportCSV";
            this.MNExportCSV.Size = new System.Drawing.Size(129, 22);
            this.MNExportCSV.Text = "To CSV";
            this.MNExportCSV.Click += new System.EventHandler(this.MNExportCSV_Click);
            // 
            // dgvPriceImportPurityWise
            // 
            this.dgvPriceImportPurityWise.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.dgvPriceImportPurityWise.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.dgvPriceImportPurityWise.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            this.dgvPriceImportPurityWise.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            this.dgvPriceImportPurityWise.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.dgvPriceImportPurityWise.Appearance.FooterPanel.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.dgvPriceImportPurityWise.Appearance.FooterPanel.Options.UseFont = true;
            this.dgvPriceImportPurityWise.Appearance.HeaderPanel.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.dgvPriceImportPurityWise.Appearance.HeaderPanel.Options.UseFont = true;
            this.dgvPriceImportPurityWise.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.dgvPriceImportPurityWise.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dgvPriceImportPurityWise.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.dgvPriceImportPurityWise.Appearance.Row.Options.UseFont = true;
            this.dgvPriceImportPurityWise.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ClmPurity,
            this.clmSieve,
            this.clmCarat,
            this.clmPurityName,
            this.clmSievename,
            this.ClmPerPcs,
            this.clmPerJanged});
            this.dgvPriceImportPurityWise.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.dgvPriceImportPurityWise.GridControl = this.grdPriceImportPurityWise;
            this.dgvPriceImportPurityWise.Name = "dgvPriceImportPurityWise";
            this.dgvPriceImportPurityWise.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.dgvPriceImportPurityWise.OptionsCustomization.AllowColumnMoving = false;
            this.dgvPriceImportPurityWise.OptionsCustomization.AllowFilter = false;
            this.dgvPriceImportPurityWise.OptionsCustomization.AllowGroup = false;
            this.dgvPriceImportPurityWise.OptionsCustomization.AllowSort = false;
            this.dgvPriceImportPurityWise.OptionsNavigation.EnterMoveNextColumn = true;
            this.dgvPriceImportPurityWise.OptionsView.ColumnAutoWidth = false;
            this.dgvPriceImportPurityWise.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.dgvPriceImportPurityWise.OptionsView.ShowAutoFilterRow = true;
            this.dgvPriceImportPurityWise.OptionsView.ShowFooter = true;
            this.dgvPriceImportPurityWise.OptionsView.ShowGroupPanel = false;
            // 
            // ClmPurity
            // 
            this.ClmPurity.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.ClmPurity.AppearanceCell.Options.UseFont = true;
            this.ClmPurity.Caption = "Purity Id";
            this.ClmPurity.ColumnEdit = this.RepLuePurity;
            this.ClmPurity.FieldName = "purity_id";
            this.ClmPurity.Name = "ClmPurity";
            this.ClmPurity.OptionsColumn.AllowEdit = false;
            this.ClmPurity.OptionsColumn.AllowFocus = false;
            this.ClmPurity.OptionsColumn.AllowMove = false;
            this.ClmPurity.OptionsColumn.AllowSize = false;
            this.ClmPurity.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "assort", "{0}")});
            this.ClmPurity.Visible = true;
            this.ClmPurity.VisibleIndex = 0;
            // 
            // RepLuePurity
            // 
            this.RepLuePurity.AutoHeight = false;
            this.RepLuePurity.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.RepLuePurity.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("assort_id", "Assort Id", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("assort_name", "Assort")});
            this.RepLuePurity.Name = "RepLuePurity";
            this.RepLuePurity.NullText = "";
            // 
            // clmSieve
            // 
            this.clmSieve.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.clmSieve.AppearanceCell.Options.UseFont = true;
            this.clmSieve.Caption = "Sieve Id";
            this.clmSieve.ColumnEdit = this.RepLueSieve;
            this.clmSieve.FieldName = "sieve_id";
            this.clmSieve.Name = "clmSieve";
            this.clmSieve.OptionsColumn.AllowEdit = false;
            this.clmSieve.OptionsColumn.AllowFocus = false;
            this.clmSieve.OptionsColumn.AllowMove = false;
            this.clmSieve.OptionsColumn.AllowSize = false;
            this.clmSieve.Visible = true;
            this.clmSieve.VisibleIndex = 1;
            // 
            // RepLueSieve
            // 
            this.RepLueSieve.AutoHeight = false;
            this.RepLueSieve.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.RepLueSieve.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("sieve_id", "Sieve Id", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("sieve_name", "Sieve")});
            this.RepLueSieve.Name = "RepLueSieve";
            this.RepLueSieve.NullText = "";
            // 
            // clmCarat
            // 
            this.clmCarat.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.clmCarat.AppearanceCell.Options.UseFont = true;
            this.clmCarat.Caption = "Per Carat";
            this.clmCarat.FieldName = "per_carat";
            this.clmCarat.Name = "clmCarat";
            this.clmCarat.OptionsColumn.AllowEdit = false;
            this.clmCarat.OptionsColumn.AllowFocus = false;
            this.clmCarat.OptionsColumn.AllowMove = false;
            this.clmCarat.OptionsColumn.AllowSize = false;
            this.clmCarat.Visible = true;
            this.clmCarat.VisibleIndex = 3;
            // 
            // clmPurityName
            // 
            this.clmPurityName.Caption = "Purity";
            this.clmPurityName.FieldName = "purity_name";
            this.clmPurityName.Name = "clmPurityName";
            this.clmPurityName.OptionsColumn.AllowEdit = false;
            this.clmPurityName.OptionsColumn.AllowFocus = false;
            this.clmPurityName.OptionsColumn.AllowMove = false;
            this.clmPurityName.OptionsColumn.AllowSize = false;
            // 
            // clmSievename
            // 
            this.clmSievename.Caption = "Sieve";
            this.clmSievename.FieldName = "sieve_name";
            this.clmSievename.Name = "clmSievename";
            this.clmSievename.OptionsColumn.AllowEdit = false;
            this.clmSievename.OptionsColumn.AllowFocus = false;
            this.clmSievename.OptionsColumn.AllowMove = false;
            this.clmSievename.OptionsColumn.AllowSize = false;
            // 
            // ClmPerPcs
            // 
            this.ClmPerPcs.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.ClmPerPcs.AppearanceCell.Options.UseFont = true;
            this.ClmPerPcs.Caption = "Per Pcs";
            this.ClmPerPcs.FieldName = "per_pcs";
            this.ClmPerPcs.Name = "ClmPerPcs";
            this.ClmPerPcs.OptionsColumn.AllowEdit = false;
            this.ClmPerPcs.OptionsColumn.AllowFocus = false;
            this.ClmPerPcs.OptionsColumn.AllowMove = false;
            this.ClmPerPcs.OptionsColumn.AllowSize = false;
            this.ClmPerPcs.Visible = true;
            this.ClmPerPcs.VisibleIndex = 2;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Appearance.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.Appearance.Options.UseFont = true;
            this.btnBrowse.ImageOptions.Image = global::DERP.Properties.Resources.Upload_final;
            this.btnBrowse.Location = new System.Drawing.Point(542, 3);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(118, 32);
            this.btnBrowse.TabIndex = 453;
            this.btnBrowse.Text = "&Browse";
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lblFormatSample);
            this.panelControl1.Controls.Add(this.lueCurrency);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.lueRateType);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.txtFileName);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.dtpDate);
            this.panelControl1.Controls.Add(this.btnBrowse);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1069, 46);
            this.panelControl1.TabIndex = 18;
            // 
            // lblFormatSample
            // 
            this.lblFormatSample.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormatSample.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblFormatSample.Appearance.Options.UseFont = true;
            this.lblFormatSample.Appearance.Options.UseForeColor = true;
            this.lblFormatSample.Location = new System.Drawing.Point(666, 11);
            this.lblFormatSample.Name = "lblFormatSample";
            this.lblFormatSample.Size = new System.Drawing.Size(187, 16);
            this.lblFormatSample.TabIndex = 460;
            this.lblFormatSample.Text = "Click For Excel Format Sample";
            this.lblFormatSample.Click += new System.EventHandler(this.lblFormatSample_Click);
            // 
            // lueCurrency
            // 
            this.lueCurrency.EnterMoveNextControl = true;
            this.lueCurrency.Location = new System.Drawing.Point(414, 10);
            this.lueCurrency.Name = "lueCurrency";
            this.lueCurrency.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueCurrency.Properties.Appearance.Options.UseFont = true;
            this.lueCurrency.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.lueCurrency.Properties.AppearanceDropDown.Options.UseFont = true;
            this.lueCurrency.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold);
            this.lueCurrency.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.lueCurrency.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.lueCurrency.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueCurrency.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("currency_id", "Currency Id", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("currency", "Currency")});
            this.lueCurrency.Properties.NullText = "";
            this.lueCurrency.Properties.ShowHeader = false;
            this.lueCurrency.Size = new System.Drawing.Size(103, 22);
            this.lueCurrency.TabIndex = 459;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(354, 12);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(54, 14);
            this.labelControl3.TabIndex = 458;
            this.labelControl3.Text = "Currency";
            // 
            // lueRateType
            // 
            this.lueRateType.EnterMoveNextControl = true;
            this.lueRateType.Location = new System.Drawing.Point(245, 10);
            this.lueRateType.Name = "lueRateType";
            this.lueRateType.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueRateType.Properties.Appearance.Options.UseFont = true;
            this.lueRateType.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.lueRateType.Properties.AppearanceDropDown.Options.UseFont = true;
            this.lueRateType.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold);
            this.lueRateType.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.lueRateType.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.lueRateType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueRateType.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ratetype_id", "Rate Type Id", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("rate_type", "Rate Type")});
            this.lueRateType.Properties.NullText = "";
            this.lueRateType.Properties.ShowHeader = false;
            this.lueRateType.Size = new System.Drawing.Size(103, 22);
            this.lueRateType.TabIndex = 457;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(177, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(62, 14);
            this.labelControl1.TabIndex = 456;
            this.labelControl1.Text = "Rate Type";
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
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(12, 12);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(29, 14);
            this.labelControl2.TabIndex = 421;
            this.labelControl2.Text = "Date";
            // 
            // dtpDate
            // 
            this.dtpDate.EditValue = null;
            this.dtpDate.EnterMoveNextControl = true;
            this.dtpDate.Location = new System.Drawing.Point(47, 10);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDate.Properties.Appearance.Options.UseFont = true;
            this.dtpDate.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtpDate.Properties.AppearanceDropDown.Options.UseFont = true;
            this.dtpDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDate.Size = new System.Drawing.Size(121, 22);
            this.dtpDate.TabIndex = 454;
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.panelProgress);
            this.panelControl3.Controls.Add(this.grdPriceImportPurityWise);
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
            // backgroundWorker_PriceImportPurityWise
            // 
            this.backgroundWorker_PriceImportPurityWise.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_PriceImportPurityWise_DoWork);
            this.backgroundWorker_PriceImportPurityWise.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_PriceImportPurityWise_RunWorkerCompleted);
            // 
            // clmPerJanged
            // 
            this.clmPerJanged.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.clmPerJanged.AppearanceCell.Options.UseFont = true;
            this.clmPerJanged.Caption = "Janged Per Carat";
            this.clmPerJanged.FieldName = "janged_per_carat";
            this.clmPerJanged.Name = "clmPerJanged";
            this.clmPerJanged.OptionsColumn.AllowEdit = false;
            this.clmPerJanged.Visible = true;
            this.clmPerJanged.VisibleIndex = 4;
            // 
            // FrmPriceImportPurityWise
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1069, 742);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.PanelSave);
            this.Controls.Add(this.panelControl1);
            this.Name = "FrmPriceImportPurityWise";
            this.Text = "Price Import Purity Wise";
            this.Load += new System.EventHandler(this.FrmPriceImport_Load);
            this.Shown += new System.EventHandler(this.FrmPriceImport_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.PanelSave)).EndInit();
            this.PanelSave.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdPriceImportPurityWise)).EndInit();
            this.ContextMNExport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPriceImportPurityWise)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepLuePurity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepLueSieve)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueCurrency.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueRateType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDate.Properties)).EndInit();
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
        private DevExpress.XtraGrid.GridControl grdPriceImportPurityWise;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvPriceImportPurityWise;
        private DevExpress.XtraGrid.Columns.GridColumn clmSieve;
        private DevExpress.XtraGrid.Columns.GridColumn ClmPurity;
        private DevExpress.XtraGrid.Columns.GridColumn clmCarat;
        private DevExpress.XtraEditors.SimpleButton btnBrowse;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.DateEdit dtpDate;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtFileName;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit lueCurrency;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LookUpEdit lueRateType;
        private DevExpress.XtraEditors.LabelControl lblFormatSample;
        private DevExpress.XtraEditors.PanelControl panelProgress;
        private System.Windows.Forms.Label lblProgressCount;
        private DevExpress.XtraEditors.MarqueeProgressBarControl SaveProgressBar;
        private System.ComponentModel.BackgroundWorker backgroundWorker_PriceImportPurityWise;
        private System.Windows.Forms.ContextMenuStrip ContextMNExport;
        private System.Windows.Forms.ToolStripMenuItem MNExportExcel;
        private System.Windows.Forms.ToolStripMenuItem MNExportPDF;
        private System.Windows.Forms.ToolStripMenuItem MNExportTEXT;
        private System.Windows.Forms.ToolStripMenuItem MNExportHTML;
        private System.Windows.Forms.ToolStripMenuItem MNExportRTF;
        private System.Windows.Forms.ToolStripMenuItem MNExportCSV;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit RepLuePurity;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit RepLueSieve;
        private DevExpress.XtraGrid.Columns.GridColumn clmPurityName;
        private DevExpress.XtraGrid.Columns.GridColumn clmSievename;
        private DevExpress.XtraGrid.Columns.GridColumn ClmPerPcs;
        private DevExpress.XtraGrid.Columns.GridColumn clmPerJanged;
    }
}