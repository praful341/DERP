namespace DERP.Master
{
    partial class FrmRangeMaster
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRangeMaster));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.PanelSave = new DevExpress.XtraEditors.PanelControl();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.grdRange = new DevExpress.XtraGrid.GridControl();
            this.ContextMNExport = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MNExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportPDF = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportTEXT = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportHTML = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportRTF = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportCSV = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvRange = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ClmColorID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.RepLueColor = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.clmSieveID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.RepLueSieve = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.clmColorName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmSizeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmDelete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.RepDelete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.clmFlag = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmFromOldRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmRangeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmFromRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.RepRate = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.clmToRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ClmRange = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmToOldRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnShow = new DevExpress.XtraEditors.SimpleButton();
            this.panelProgress = new DevExpress.XtraEditors.PanelControl();
            this.lblProgressCount = new System.Windows.Forms.Label();
            this.SaveProgressBar = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            this.backgroundWorker_PriceImport = new System.ComponentModel.BackgroundWorker();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl5 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.PanelSave)).BeginInit();
            this.PanelSave.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdRange)).BeginInit();
            this.ContextMNExport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepLueColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepLueSieve)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelProgress)).BeginInit();
            this.panelProgress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SaveProgressBar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).BeginInit();
            this.panelControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelSave
            // 
            this.PanelSave.Controls.Add(this.btnExit);
            this.PanelSave.Controls.Add(this.btnClear);
            this.PanelSave.Controls.Add(this.btnSave);
            this.PanelSave.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelSave.Location = new System.Drawing.Point(0, 624);
            this.PanelSave.Name = "PanelSave";
            this.PanelSave.Size = new System.Drawing.Size(1084, 50);
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
            // grdRange
            // 
            this.grdRange.ContextMenuStrip = this.ContextMNExport;
            this.grdRange.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdRange.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdRange.Location = new System.Drawing.Point(2, 2);
            this.grdRange.MainView = this.dgvRange;
            this.grdRange.Name = "grdRange";
            this.grdRange.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.RepLueColor,
            this.RepLueSieve,
            this.RepDelete,
            this.RepRate});
            this.grdRange.Size = new System.Drawing.Size(513, 570);
            this.grdRange.TabIndex = 20;
            this.grdRange.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvRange});
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
            // dgvRange
            // 
            this.dgvRange.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.dgvRange.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.dgvRange.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            this.dgvRange.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            this.dgvRange.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.dgvRange.Appearance.FooterPanel.Font = new System.Drawing.Font("Cambria", 10.75F, System.Drawing.FontStyle.Bold);
            this.dgvRange.Appearance.FooterPanel.Options.UseFont = true;
            this.dgvRange.Appearance.HeaderPanel.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.dgvRange.Appearance.HeaderPanel.Options.UseFont = true;
            this.dgvRange.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.dgvRange.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dgvRange.Appearance.Row.Font = new System.Drawing.Font("Cambria", 10.75F, System.Drawing.FontStyle.Bold);
            this.dgvRange.Appearance.Row.Options.UseFont = true;
            this.dgvRange.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ClmColorID,
            this.clmSieveID,
            this.clmColorName,
            this.clmSizeName,
            this.clmDelete,
            this.clmFlag,
            this.clmFromOldRate,
            this.clmRangeID,
            this.clmFromRate,
            this.clmToRate,
            this.ClmRange,
            this.clmToOldRate});
            this.dgvRange.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.dgvRange.GridControl = this.grdRange;
            this.dgvRange.Name = "dgvRange";
            this.dgvRange.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.dgvRange.OptionsCustomization.AllowColumnMoving = false;
            this.dgvRange.OptionsCustomization.AllowGroup = false;
            this.dgvRange.OptionsCustomization.AllowSort = false;
            this.dgvRange.OptionsNavigation.EnterMoveNextColumn = true;
            this.dgvRange.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.dgvRange.OptionsView.ShowFooter = true;
            this.dgvRange.OptionsView.ShowGroupPanel = false;
            // 
            // ClmColorID
            // 
            this.ClmColorID.Caption = "Color";
            this.ClmColorID.ColumnEdit = this.RepLueColor;
            this.ClmColorID.FieldName = "color_id";
            this.ClmColorID.Name = "ClmColorID";
            this.ClmColorID.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "color_id", "{0:#,##,##,##0}")});
            this.ClmColorID.Visible = true;
            this.ClmColorID.VisibleIndex = 1;
            this.ClmColorID.Width = 91;
            // 
            // RepLueColor
            // 
            this.RepLueColor.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.RepLueColor.Appearance.Options.UseFont = true;
            this.RepLueColor.AppearanceDropDown.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.RepLueColor.AppearanceDropDown.Options.UseFont = true;
            this.RepLueColor.AutoHeight = false;
            this.RepLueColor.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.RepLueColor.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("color_id", "Color Id", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("color_name", "Color")});
            this.RepLueColor.Name = "RepLueColor";
            this.RepLueColor.NullText = "";
            // 
            // clmSieveID
            // 
            this.clmSieveID.Caption = "Sieve";
            this.clmSieveID.ColumnEdit = this.RepLueSieve;
            this.clmSieveID.FieldName = "sieve_id";
            this.clmSieveID.Name = "clmSieveID";
            this.clmSieveID.Visible = true;
            this.clmSieveID.VisibleIndex = 2;
            this.clmSieveID.Width = 63;
            // 
            // RepLueSieve
            // 
            this.RepLueSieve.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.RepLueSieve.Appearance.Options.UseFont = true;
            this.RepLueSieve.AppearanceDropDown.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.RepLueSieve.AppearanceDropDown.Options.UseFont = true;
            this.RepLueSieve.AutoHeight = false;
            this.RepLueSieve.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.RepLueSieve.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("sieve_id", "Sieve Id", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("sieve_name", "Sieve")});
            this.RepLueSieve.Name = "RepLueSieve";
            this.RepLueSieve.NullText = "";
            // 
            // clmColorName
            // 
            this.clmColorName.Caption = "Color";
            this.clmColorName.FieldName = "color_name";
            this.clmColorName.Name = "clmColorName";
            this.clmColorName.Width = 131;
            // 
            // clmSizeName
            // 
            this.clmSizeName.Caption = "Size";
            this.clmSizeName.FieldName = "sieve_name";
            this.clmSizeName.Name = "clmSizeName";
            this.clmSizeName.Width = 67;
            // 
            // clmDelete
            // 
            this.clmDelete.Caption = "Del";
            this.clmDelete.ColumnEdit = this.RepDelete;
            this.clmDelete.FieldName = "delete";
            this.clmDelete.Name = "clmDelete";
            this.clmDelete.OptionsColumn.AllowMove = false;
            this.clmDelete.Visible = true;
            this.clmDelete.VisibleIndex = 0;
            this.clmDelete.Width = 31;
            // 
            // RepDelete
            // 
            this.RepDelete.AutoHeight = false;
            editorButtonImageOptions1.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions1.Image")));
            this.RepDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Delete", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "Delete This Row", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.RepDelete.Name = "RepDelete";
            this.RepDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.RepDelete.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.RepDelete_ButtonClick);
            // 
            // clmFlag
            // 
            this.clmFlag.Caption = "Flag";
            this.clmFlag.FieldName = "flag";
            this.clmFlag.Name = "clmFlag";
            this.clmFlag.OptionsColumn.AllowEdit = false;
            this.clmFlag.OptionsColumn.AllowFocus = false;
            this.clmFlag.OptionsColumn.AllowMove = false;
            this.clmFlag.Width = 73;
            // 
            // clmFromOldRate
            // 
            this.clmFromOldRate.Caption = "From Old Rate";
            this.clmFromOldRate.FieldName = "from_old_rate";
            this.clmFromOldRate.Name = "clmFromOldRate";
            // 
            // clmRangeID
            // 
            this.clmRangeID.Caption = "Range ID";
            this.clmRangeID.FieldName = "range_id";
            this.clmRangeID.Name = "clmRangeID";
            // 
            // clmFromRate
            // 
            this.clmFromRate.Caption = "From Rate";
            this.clmFromRate.ColumnEdit = this.RepRate;
            this.clmFromRate.DisplayFormat.FormatString = "F0";
            this.clmFromRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.clmFromRate.FieldName = "from_rate";
            this.clmFromRate.Name = "clmFromRate";
            this.clmFromRate.Visible = true;
            this.clmFromRate.VisibleIndex = 3;
            this.clmFromRate.Width = 91;
            // 
            // RepRate
            // 
            this.RepRate.AutoHeight = false;
            this.RepRate.Name = "RepRate";
            // 
            // clmToRate
            // 
            this.clmToRate.Caption = "To Rate";
            this.clmToRate.DisplayFormat.FormatString = "{0:#,##,##,##0}";
            this.clmToRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.clmToRate.FieldName = "to_rate";
            this.clmToRate.Name = "clmToRate";
            this.clmToRate.Visible = true;
            this.clmToRate.VisibleIndex = 4;
            this.clmToRate.Width = 81;
            // 
            // ClmRange
            // 
            this.ClmRange.Caption = "Range";
            this.ClmRange.FieldName = "range";
            this.ClmRange.Name = "ClmRange";
            this.ClmRange.Visible = true;
            this.ClmRange.VisibleIndex = 5;
            this.ClmRange.Width = 138;
            // 
            // clmToOldRate
            // 
            this.clmToOldRate.Caption = "To Old Rate";
            this.clmToOldRate.FieldName = "to_old_rate";
            this.clmToOldRate.Name = "clmToOldRate";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnShow);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1084, 46);
            this.panelControl1.TabIndex = 18;
            // 
            // btnShow
            // 
            this.btnShow.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShow.Appearance.Options.UseFont = true;
            this.btnShow.ImageOptions.Image = global::DERP.Properties.Resources.Show;
            this.btnShow.Location = new System.Drawing.Point(502, 10);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(102, 32);
            this.btnShow.TabIndex = 461;
            this.btnShow.Text = "Show";
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // panelProgress
            // 
            this.panelProgress.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelProgress.Controls.Add(this.lblProgressCount);
            this.panelProgress.Controls.Add(this.SaveProgressBar);
            this.panelProgress.Location = new System.Drawing.Point(107, 242);
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
            // panelControl4
            // 
            this.panelControl4.Controls.Add(this.panelControl5);
            this.panelControl4.Controls.Add(this.panelControl3);
            this.panelControl4.Controls.Add(this.panelControl2);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl4.Location = new System.Drawing.Point(0, 46);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(1084, 578);
            this.panelControl4.TabIndex = 619;
            // 
            // panelControl5
            // 
            this.panelControl5.Controls.Add(this.panelProgress);
            this.panelControl5.Controls.Add(this.grdRange);
            this.panelControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl5.Location = new System.Drawing.Point(283, 2);
            this.panelControl5.Name = "panelControl5";
            this.panelControl5.Size = new System.Drawing.Size(517, 574);
            this.panelControl5.TabIndex = 25;
            // 
            // panelControl3
            // 
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl3.Location = new System.Drawing.Point(800, 2);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(282, 574);
            this.panelControl3.TabIndex = 24;
            // 
            // panelControl2
            // 
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl2.Location = new System.Drawing.Point(2, 2);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(281, 574);
            this.panelControl2.TabIndex = 23;
            // 
            // FrmRangeMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 674);
            this.Controls.Add(this.panelControl4);
            this.Controls.Add(this.PanelSave);
            this.Controls.Add(this.panelControl1);
            this.Name = "FrmRangeMaster";
            this.Text = "Range Master";
            this.Load += new System.EventHandler(this.FrmRangeMaster_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PanelSave)).EndInit();
            this.PanelSave.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdRange)).EndInit();
            this.ContextMNExport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepLueColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepLueSieve)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelProgress)).EndInit();
            this.panelProgress.ResumeLayout(false);
            this.panelProgress.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SaveProgressBar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).EndInit();
            this.panelControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl PanelSave;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraGrid.GridControl grdRange;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvRange;
        private DevExpress.XtraGrid.Columns.GridColumn clmSieveID;
        private DevExpress.XtraGrid.Columns.GridColumn ClmColorID;
        private DevExpress.XtraGrid.Columns.GridColumn clmFromRate;
        private DevExpress.XtraEditors.PanelControl panelControl1;
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
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit RepLueColor;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit RepLueSieve;
        private DevExpress.XtraGrid.Columns.GridColumn clmColorName;
        private DevExpress.XtraGrid.Columns.GridColumn clmSizeName;
        private DevExpress.XtraEditors.SimpleButton btnShow;
        private DevExpress.XtraGrid.Columns.GridColumn clmDelete;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit RepDelete;
        private DevExpress.XtraGrid.Columns.GridColumn clmFlag;
        private DevExpress.XtraGrid.Columns.GridColumn clmFromOldRate;
        private DevExpress.XtraGrid.Columns.GridColumn clmRangeID;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit RepRate;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraGrid.Columns.GridColumn clmToRate;
        private DevExpress.XtraGrid.Columns.GridColumn ClmRange;
        private DevExpress.XtraEditors.PanelControl panelControl5;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.Columns.GridColumn clmToOldRate;
    }
}