namespace DERP.Transaction
{
    partial class FrmStorePartyOpening
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
            this.grdPartyOpening = new DevExpress.XtraGrid.GridControl();
            this.ContextMNExport = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MNExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportPDF = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportTEXT = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportHTML = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportRTF = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportCSV = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvPartyOpening = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ClmParty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmItem = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmQty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmPartyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmItemName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmSubItem = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmSubItemName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.RepLueAssort = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.RepLueSieve = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.btnBrowse = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnShow = new DevExpress.XtraEditors.SimpleButton();
            this.lblFormatSample = new DevExpress.XtraEditors.LabelControl();
            this.txtFileName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.dtpDate = new DevExpress.XtraEditors.DateEdit();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.panelProgress = new DevExpress.XtraEditors.PanelControl();
            this.lblProgressCount = new System.Windows.Forms.Label();
            this.SaveProgressBar = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            this.backgroundWorker_PriceImport = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.PanelSave)).BeginInit();
            this.PanelSave.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPartyOpening)).BeginInit();
            this.ContextMNExport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPartyOpening)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepLueAssort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepLueSieve)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
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
            this.PanelSave.Location = new System.Drawing.Point(0, 463);
            this.PanelSave.Name = "PanelSave";
            this.PanelSave.Size = new System.Drawing.Size(909, 50);
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
            // grdPartyOpening
            // 
            this.grdPartyOpening.ContextMenuStrip = this.ContextMNExport;
            this.grdPartyOpening.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPartyOpening.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdPartyOpening.Location = new System.Drawing.Point(2, 2);
            this.grdPartyOpening.MainView = this.dgvPartyOpening;
            this.grdPartyOpening.Name = "grdPartyOpening";
            this.grdPartyOpening.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.RepLueAssort,
            this.RepLueSieve});
            this.grdPartyOpening.Size = new System.Drawing.Size(905, 413);
            this.grdPartyOpening.TabIndex = 20;
            this.grdPartyOpening.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvPartyOpening});
            // 
            // ContextMNExport
            // 
            this.ContextMNExport.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.ContextMNExport.ImageScalingSize = new System.Drawing.Size(24, 24);
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
            // dgvPartyOpening
            // 
            this.dgvPartyOpening.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.dgvPartyOpening.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.dgvPartyOpening.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            this.dgvPartyOpening.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            this.dgvPartyOpening.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.dgvPartyOpening.Appearance.FooterPanel.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold);
            this.dgvPartyOpening.Appearance.FooterPanel.Options.UseFont = true;
            this.dgvPartyOpening.Appearance.HeaderPanel.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.dgvPartyOpening.Appearance.HeaderPanel.Options.UseFont = true;
            this.dgvPartyOpening.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.dgvPartyOpening.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dgvPartyOpening.Appearance.Row.Font = new System.Drawing.Font("Cambria", 10.75F, System.Drawing.FontStyle.Bold);
            this.dgvPartyOpening.Appearance.Row.Options.UseFont = true;
            this.dgvPartyOpening.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ClmParty,
            this.clmItem,
            this.clmQty,
            this.clmPartyName,
            this.clmItemName,
            this.clmRate,
            this.clmAmount,
            this.clmSubItem,
            this.clmSubItemName});
            this.dgvPartyOpening.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.dgvPartyOpening.GridControl = this.grdPartyOpening;
            this.dgvPartyOpening.Name = "dgvPartyOpening";
            this.dgvPartyOpening.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.dgvPartyOpening.OptionsCustomization.AllowColumnMoving = false;
            this.dgvPartyOpening.OptionsCustomization.AllowFilter = false;
            this.dgvPartyOpening.OptionsCustomization.AllowGroup = false;
            this.dgvPartyOpening.OptionsCustomization.AllowSort = false;
            this.dgvPartyOpening.OptionsNavigation.EnterMoveNextColumn = true;
            this.dgvPartyOpening.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.dgvPartyOpening.OptionsView.ShowAutoFilterRow = true;
            this.dgvPartyOpening.OptionsView.ShowFooter = true;
            this.dgvPartyOpening.OptionsView.ShowGroupPanel = false;
            this.dgvPartyOpening.CustomSummaryCalculate += new DevExpress.Data.CustomSummaryEventHandler(this.dgvPartyOpening_CustomSummaryCalculate);
            this.dgvPartyOpening.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.dgvPartyOpening_FocusedRowChanged);
            this.dgvPartyOpening.FocusedColumnChanged += new DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventHandler(this.dgvPartyOpening_FocusedColumnChanged);
            this.dgvPartyOpening.ValidatingEditor += new DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventHandler(this.dgvPartyOpening_ValidatingEditor);
            // 
            // ClmParty
            // 
            this.ClmParty.Caption = "Party";
            this.ClmParty.FieldName = "party_id";
            this.ClmParty.Name = "ClmParty";
            this.ClmParty.OptionsColumn.AllowEdit = false;
            this.ClmParty.OptionsColumn.AllowFocus = false;
            this.ClmParty.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "party", "{0}")});
            this.ClmParty.Width = 216;
            // 
            // clmItem
            // 
            this.clmItem.Caption = "Item";
            this.clmItem.FieldName = "item_id";
            this.clmItem.Name = "clmItem";
            this.clmItem.OptionsColumn.AllowEdit = false;
            this.clmItem.OptionsColumn.AllowFocus = false;
            this.clmItem.Width = 207;
            // 
            // clmQty
            // 
            this.clmQty.Caption = "Qty";
            this.clmQty.FieldName = "qty";
            this.clmQty.Name = "clmQty";
            this.clmQty.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.clmQty.Visible = true;
            this.clmQty.VisibleIndex = 3;
            this.clmQty.Width = 84;
            // 
            // clmPartyName
            // 
            this.clmPartyName.Caption = "Party";
            this.clmPartyName.FieldName = "party";
            this.clmPartyName.Name = "clmPartyName";
            this.clmPartyName.OptionsColumn.AllowEdit = false;
            this.clmPartyName.OptionsColumn.AllowFocus = false;
            this.clmPartyName.OptionsColumn.AllowMove = false;
            this.clmPartyName.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.clmPartyName.Visible = true;
            this.clmPartyName.VisibleIndex = 0;
            this.clmPartyName.Width = 181;
            // 
            // clmItemName
            // 
            this.clmItemName.Caption = "Item";
            this.clmItemName.FieldName = "item";
            this.clmItemName.Name = "clmItemName";
            this.clmItemName.OptionsColumn.AllowEdit = false;
            this.clmItemName.OptionsColumn.AllowFocus = false;
            this.clmItemName.OptionsColumn.AllowMove = false;
            this.clmItemName.Visible = true;
            this.clmItemName.VisibleIndex = 1;
            this.clmItemName.Width = 217;
            // 
            // clmRate
            // 
            this.clmRate.Caption = "Rate";
            this.clmRate.FieldName = "rate";
            this.clmRate.Name = "clmRate";
            this.clmRate.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom)});
            this.clmRate.Visible = true;
            this.clmRate.VisibleIndex = 4;
            this.clmRate.Width = 90;
            // 
            // clmAmount
            // 
            this.clmAmount.Caption = "Amount";
            this.clmAmount.FieldName = "amount";
            this.clmAmount.Name = "clmAmount";
            this.clmAmount.OptionsColumn.AllowEdit = false;
            this.clmAmount.OptionsColumn.AllowFocus = false;
            this.clmAmount.OptionsColumn.AllowMove = false;
            this.clmAmount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.clmAmount.Visible = true;
            this.clmAmount.VisibleIndex = 5;
            this.clmAmount.Width = 267;
            // 
            // clmSubItem
            // 
            this.clmSubItem.Caption = "Sub Item";
            this.clmSubItem.FieldName = "sub_item_id";
            this.clmSubItem.Name = "clmSubItem";
            this.clmSubItem.OptionsColumn.AllowEdit = false;
            this.clmSubItem.OptionsColumn.AllowFocus = false;
            this.clmSubItem.Width = 216;
            // 
            // clmSubItemName
            // 
            this.clmSubItemName.Caption = "Sub Item";
            this.clmSubItemName.FieldName = "sub_item";
            this.clmSubItemName.Name = "clmSubItemName";
            this.clmSubItemName.OptionsColumn.AllowEdit = false;
            this.clmSubItemName.OptionsColumn.AllowFocus = false;
            this.clmSubItemName.OptionsColumn.AllowMove = false;
            this.clmSubItemName.Visible = true;
            this.clmSubItemName.VisibleIndex = 2;
            this.clmSubItemName.Width = 223;
            // 
            // RepLueAssort
            // 
            this.RepLueAssort.AutoHeight = false;
            this.RepLueAssort.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.RepLueAssort.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("assort_id", "Assort Id", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("assort_name", "Assort")});
            this.RepLueAssort.Name = "RepLueAssort";
            this.RepLueAssort.NullText = "";
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
            // btnBrowse
            // 
            this.btnBrowse.Appearance.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.Appearance.Options.UseFont = true;
            this.btnBrowse.ImageOptions.Image = global::DERP.Properties.Resources.Upload_final;
            this.btnBrowse.Location = new System.Drawing.Point(179, 5);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(102, 32);
            this.btnBrowse.TabIndex = 453;
            this.btnBrowse.Text = "&Browse";
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnShow);
            this.panelControl1.Controls.Add(this.lblFormatSample);
            this.panelControl1.Controls.Add(this.txtFileName);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.dtpDate);
            this.panelControl1.Controls.Add(this.btnBrowse);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(909, 46);
            this.panelControl1.TabIndex = 18;
            // 
            // btnShow
            // 
            this.btnShow.Appearance.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShow.Appearance.Options.UseFont = true;
            this.btnShow.ImageOptions.Image = global::DERP.Properties.Resources.Show;
            this.btnShow.Location = new System.Drawing.Point(287, 5);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(102, 32);
            this.btnShow.TabIndex = 461;
            this.btnShow.Text = "Show";
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // lblFormatSample
            // 
            this.lblFormatSample.Appearance.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormatSample.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblFormatSample.Appearance.Options.UseFont = true;
            this.lblFormatSample.Appearance.Options.UseForeColor = true;
            this.lblFormatSample.Location = new System.Drawing.Point(411, 13);
            this.lblFormatSample.Name = "lblFormatSample";
            this.lblFormatSample.Size = new System.Drawing.Size(220, 16);
            this.lblFormatSample.TabIndex = 460;
            this.lblFormatSample.Text = "Click For Excel Format Sample";
            this.lblFormatSample.Click += new System.EventHandler(this.lblFormatSample_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.EnterMoveNextControl = true;
            this.txtFileName.Location = new System.Drawing.Point(992, 10);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFileName.Properties.Appearance.Options.UseFont = true;
            this.txtFileName.Size = new System.Drawing.Size(56, 20);
            this.txtFileName.TabIndex = 455;
            this.txtFileName.Visible = false;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(12, 12);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(34, 16);
            this.labelControl2.TabIndex = 421;
            this.labelControl2.Text = "Date";
            // 
            // dtpDate
            // 
            this.dtpDate.EditValue = null;
            this.dtpDate.EnterMoveNextControl = true;
            this.dtpDate.Location = new System.Drawing.Point(52, 11);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtpDate.Properties.Appearance.Options.UseFont = true;
            this.dtpDate.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtpDate.Properties.AppearanceDropDown.Options.UseFont = true;
            this.dtpDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDate.Size = new System.Drawing.Size(121, 22);
            this.dtpDate.TabIndex = 454;
            this.dtpDate.EditValueChanged += new System.EventHandler(this.dtpDate_EditValueChanged);
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.panelProgress);
            this.panelControl3.Controls.Add(this.grdPartyOpening);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(0, 46);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(909, 417);
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
            // backgroundWorker_PriceImport
            // 
            this.backgroundWorker_PriceImport.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_PriceImport_DoWork);
            this.backgroundWorker_PriceImport.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_PriceImport_RunWorkerCompleted);
            // 
            // FrmStorePartyOpening
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 513);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.PanelSave);
            this.Controls.Add(this.panelControl1);
            this.Name = "FrmStorePartyOpening";
            this.Text = "Store Party Opening";
            this.Load += new System.EventHandler(this.FrmStorePartyOpening_Load);
            this.Shown += new System.EventHandler(this.FrmPriceImport_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.PanelSave)).EndInit();
            this.PanelSave.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdPartyOpening)).EndInit();
            this.ContextMNExport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPartyOpening)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepLueAssort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepLueSieve)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
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
        private DevExpress.XtraGrid.GridControl grdPartyOpening;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvPartyOpening;
        private DevExpress.XtraGrid.Columns.GridColumn clmItem;
        private DevExpress.XtraGrid.Columns.GridColumn ClmParty;
        private DevExpress.XtraGrid.Columns.GridColumn clmQty;
        private DevExpress.XtraEditors.SimpleButton btnBrowse;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.DateEdit dtpDate;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtFileName;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.LabelControl lblFormatSample;
        private DevExpress.XtraEditors.PanelControl panelProgress;
        private System.Windows.Forms.Label lblProgressCount;
        private DevExpress.XtraEditors.MarqueeProgressBarControl SaveProgressBar;
        private System.ComponentModel.BackgroundWorker backgroundWorker_PriceImport;
        private System.Windows.Forms.ContextMenuStrip ContextMNExport;
        private System.Windows.Forms.ToolStripMenuItem MNExportExcel;
        private System.Windows.Forms.ToolStripMenuItem MNExportPDF;
        private System.Windows.Forms.ToolStripMenuItem MNExportTEXT;
        private System.Windows.Forms.ToolStripMenuItem MNExportHTML;
        private System.Windows.Forms.ToolStripMenuItem MNExportRTF;
        private System.Windows.Forms.ToolStripMenuItem MNExportCSV;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit RepLueAssort;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit RepLueSieve;
        private DevExpress.XtraGrid.Columns.GridColumn clmPartyName;
        private DevExpress.XtraGrid.Columns.GridColumn clmItemName;
        private DevExpress.XtraEditors.SimpleButton btnShow;
        private DevExpress.XtraGrid.Columns.GridColumn clmRate;
        private DevExpress.XtraGrid.Columns.GridColumn clmAmount;
        private DevExpress.XtraGrid.Columns.GridColumn clmSubItem;
        private DevExpress.XtraGrid.Columns.GridColumn clmSubItemName;
    }
}