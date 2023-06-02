namespace DERP.Transaction.MFG
{
    partial class FrmMFGMakablePCsUpdate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMFGMakablePCsUpdate));
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.ContextMNExport = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MNExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportPDF = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportTEXT = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportHTML = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportRTF = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportCSV = new System.Windows.Forms.ToolStripMenuItem();
            this.bandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.PanelIssueReceive = new DevExpress.XtraEditors.PanelControl();
            this.panelProgress = new DevExpress.XtraEditors.PanelControl();
            this.lblProgressCount = new System.Windows.Forms.Label();
            this.SaveProgressBar = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            this.grdMakablePcs = new DevExpress.XtraGrid.GridControl();
            this.dgvMakablePcs = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ClmProcessID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ClmProcess = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repChkSel = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repLueRejectionPurity = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.grvProcessIssue = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.clmIssueID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmPrdId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmRoughSieve = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmPurity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmQuality = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmCarat = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmClarity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmFlag = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ClmRoughCutID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ClmRoughCutNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.RepRecDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.RepBtnDelete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.bandedGridView2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.lblType = new System.Windows.Forms.Label();
            this.lueType = new DevExpress.XtraEditors.LookUpEdit();
            this.lueProcess = new DevExpress.XtraEditors.LookUpEdit();
            this.lblProcess = new System.Windows.Forms.Label();
            this.lueKapan = new DevExpress.XtraEditors.LookUpEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpEntryDate = new DevExpress.XtraEditors.DateEdit();
            this.label11 = new System.Windows.Forms.Label();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.backgroundWorker_MakablePcsUpdate = new System.ComponentModel.BackgroundWorker();
            this.panelIssueReceiveSearch = new DevExpress.XtraEditors.PanelControl();
            this.ContextMNExport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PanelIssueReceive)).BeginInit();
            this.PanelIssueReceive.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelProgress)).BeginInit();
            this.panelProgress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SaveProgressBar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMakablePcs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMakablePcs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repChkSel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repLueRejectionPurity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvProcessIssue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepRecDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepRecDate.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepBtnDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueProcess.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueKapan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEntryDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEntryDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelIssueReceiveSearch)).BeginInit();
            this.panelIssueReceiveSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.ImageOptions.Image")));
            this.btnSave.Location = new System.Drawing.Point(907, 7);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(102, 32);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
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
            // bandedGridView1
            // 
            this.bandedGridView1.Name = "bandedGridView1";
            // 
            // gridBand1
            // 
            this.gridBand1.Caption = "gridBand1";
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.VisibleIndex = -1;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnExit);
            this.panelControl2.Controls.Add(this.btnClear);
            this.panelControl2.Controls.Add(this.btnSave);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 451);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1241, 44);
            this.panelControl2.TabIndex = 0;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Appearance.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Appearance.Options.UseFont = true;
            this.btnExit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.ImageOptions.Image")));
            this.btnExit.Location = new System.Drawing.Point(1123, 6);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(102, 32);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "E&xit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Appearance.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Appearance.Options.UseFont = true;
            this.btnClear.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.ImageOptions.Image")));
            this.btnClear.Location = new System.Drawing.Point(1015, 6);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(102, 32);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "&Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // PanelIssueReceive
            // 
            this.PanelIssueReceive.Controls.Add(this.panelProgress);
            this.PanelIssueReceive.Controls.Add(this.grdMakablePcs);
            this.PanelIssueReceive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelIssueReceive.Location = new System.Drawing.Point(0, 50);
            this.PanelIssueReceive.Name = "PanelIssueReceive";
            this.PanelIssueReceive.Size = new System.Drawing.Size(1241, 401);
            this.PanelIssueReceive.TabIndex = 99;
            // 
            // panelProgress
            // 
            this.panelProgress.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelProgress.Controls.Add(this.lblProgressCount);
            this.panelProgress.Controls.Add(this.SaveProgressBar);
            this.panelProgress.Location = new System.Drawing.Point(447, 134);
            this.panelProgress.Name = "panelProgress";
            this.panelProgress.Size = new System.Drawing.Size(282, 35);
            this.panelProgress.TabIndex = 99;
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
            // grdMakablePcs
            // 
            this.grdMakablePcs.ContextMenuStrip = this.ContextMNExport;
            this.grdMakablePcs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMakablePcs.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdMakablePcs.Location = new System.Drawing.Point(2, 2);
            this.grdMakablePcs.MainView = this.dgvMakablePcs;
            this.grdMakablePcs.Name = "grdMakablePcs";
            this.grdMakablePcs.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repChkSel,
            this.repLueRejectionPurity});
            this.grdMakablePcs.Size = new System.Drawing.Size(1237, 397);
            this.grdMakablePcs.TabIndex = 99;
            this.grdMakablePcs.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvMakablePcs});
            // 
            // dgvMakablePcs
            // 
            this.dgvMakablePcs.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.dgvMakablePcs.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.dgvMakablePcs.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            this.dgvMakablePcs.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            this.dgvMakablePcs.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.dgvMakablePcs.Appearance.FooterPanel.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.dgvMakablePcs.Appearance.FooterPanel.Options.UseFont = true;
            this.dgvMakablePcs.Appearance.HeaderPanel.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.dgvMakablePcs.Appearance.HeaderPanel.Options.UseFont = true;
            this.dgvMakablePcs.Appearance.Row.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold);
            this.dgvMakablePcs.Appearance.Row.Options.UseFont = true;
            this.dgvMakablePcs.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ClmProcessID,
            this.ClmProcess,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn1});
            this.dgvMakablePcs.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.dgvMakablePcs.GridControl = this.grdMakablePcs;
            this.dgvMakablePcs.Name = "dgvMakablePcs";
            this.dgvMakablePcs.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.dgvMakablePcs.OptionsCustomization.AllowGroup = false;
            this.dgvMakablePcs.OptionsCustomization.AllowSort = false;
            this.dgvMakablePcs.OptionsNavigation.EnterMoveNextColumn = true;
            this.dgvMakablePcs.OptionsSelection.MultiSelect = true;
            this.dgvMakablePcs.OptionsView.ColumnAutoWidth = false;
            this.dgvMakablePcs.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.dgvMakablePcs.OptionsView.ShowAutoFilterRow = true;
            this.dgvMakablePcs.OptionsView.ShowFooter = true;
            this.dgvMakablePcs.OptionsView.ShowGroupPanel = false;
            this.dgvMakablePcs.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.dgvRejectionRate_FocusedRowChanged);
            this.dgvMakablePcs.FocusedColumnChanged += new DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventHandler(this.dgvRejectionRate_FocusedColumnChanged);
            this.dgvMakablePcs.ValidatingEditor += new DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventHandler(this.dgvRejectionRate_ValidatingEditor);
            // 
            // ClmProcessID
            // 
            this.ClmProcessID.Caption = "Process ID";
            this.ClmProcessID.FieldName = "process_id";
            this.ClmProcessID.Name = "ClmProcessID";
            this.ClmProcessID.OptionsColumn.AllowEdit = false;
            this.ClmProcessID.OptionsColumn.AllowFocus = false;
            this.ClmProcessID.OptionsColumn.AllowMove = false;
            // 
            // ClmProcess
            // 
            this.ClmProcess.Caption = "Process";
            this.ClmProcess.FieldName = "process_name";
            this.ClmProcess.Name = "ClmProcess";
            this.ClmProcess.OptionsColumn.AllowEdit = false;
            this.ClmProcess.OptionsColumn.AllowFocus = false;
            this.ClmProcess.OptionsColumn.AllowMove = false;
            this.ClmProcess.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "process_name", "{0}")});
            this.ClmProcess.Visible = true;
            this.ClmProcess.VisibleIndex = 1;
            this.ClmProcess.Width = 137;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Kapan";
            this.gridColumn5.FieldName = "kapan_no";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.AllowFocus = false;
            this.gridColumn5.OptionsColumn.AllowMove = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 0;
            this.gridColumn5.Width = 173;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "MFG Pcs";
            this.gridColumn6.FieldName = "mfg_pcs";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 2;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Makable ID";
            this.gridColumn7.FieldName = "makable_id";
            this.gridColumn7.Name = "gridColumn7";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Kapan ID";
            this.gridColumn1.FieldName = "kapan_id";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // repChkSel
            // 
            this.repChkSel.AutoHeight = false;
            this.repChkSel.Caption = "Check";
            this.repChkSel.Name = "repChkSel";
            this.repChkSel.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.repChkSel.ValueGrayed = false;
            // 
            // repLueRejectionPurity
            // 
            this.repLueRejectionPurity.AutoHeight = false;
            this.repLueRejectionPurity.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repLueRejectionPurity.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("purity_id", "Purity Id", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("purity_name", "Purity Name")});
            this.repLueRejectionPurity.Name = "repLueRejectionPurity";
            this.repLueRejectionPurity.NullText = "";
            this.repLueRejectionPurity.ShowHeader = false;
            // 
            // grvProcessIssue
            // 
            this.grvProcessIssue.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.grvProcessIssue.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.grvProcessIssue.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            this.grvProcessIssue.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            this.grvProcessIssue.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.grvProcessIssue.Appearance.FooterPanel.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.grvProcessIssue.Appearance.FooterPanel.Options.UseFont = true;
            this.grvProcessIssue.Appearance.HeaderPanel.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.grvProcessIssue.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvProcessIssue.Appearance.Row.Font = new System.Drawing.Font("Cambria", 9F);
            this.grvProcessIssue.Appearance.Row.Options.UseFont = true;
            this.grvProcessIssue.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.clmIssueID,
            this.clmPrdId,
            this.clmRoughSieve,
            this.clmPurity,
            this.clmQuality,
            this.clmCarat,
            this.clmRate,
            this.clmAmount,
            this.clmClarity,
            this.clmFlag,
            this.ClmRoughCutID,
            this.ClmRoughCutNo});
            this.grvProcessIssue.Name = "grvProcessIssue";
            this.grvProcessIssue.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.grvProcessIssue.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.grvProcessIssue.OptionsCustomization.AllowColumnMoving = false;
            this.grvProcessIssue.OptionsCustomization.AllowQuickHideColumns = false;
            this.grvProcessIssue.OptionsNavigation.EnterMoveNextColumn = true;
            this.grvProcessIssue.OptionsView.ColumnAutoWidth = false;
            this.grvProcessIssue.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.grvProcessIssue.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.grvProcessIssue.OptionsView.ShowAutoFilterRow = true;
            this.grvProcessIssue.OptionsView.ShowFooter = true;
            this.grvProcessIssue.OptionsView.ShowGroupPanel = false;
            this.grvProcessIssue.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.grvProcessIssue_FocusedRowChanged);
            this.grvProcessIssue.FocusedColumnChanged += new DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventHandler(this.grvProcessIssue_FocusedColumnChanged);
            this.grvProcessIssue.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvProcessIssue_CellValueChanged);
            this.grvProcessIssue.ValidatingEditor += new DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventHandler(this.grvProcessIssue_ValidatingEditor);
            // 
            // clmIssueID
            // 
            this.clmIssueID.Caption = "Issue ID";
            this.clmIssueID.FieldName = "issue_id";
            this.clmIssueID.Name = "clmIssueID";
            this.clmIssueID.OptionsColumn.AllowEdit = false;
            this.clmIssueID.OptionsColumn.AllowFocus = false;
            this.clmIssueID.OptionsColumn.AllowMove = false;
            this.clmIssueID.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.clmIssueID.Visible = true;
            this.clmIssueID.VisibleIndex = 0;
            // 
            // clmPrdId
            // 
            this.clmPrdId.Caption = "Prd Id";
            this.clmPrdId.FieldName = "prediction_id";
            this.clmPrdId.Name = "clmPrdId";
            this.clmPrdId.OptionsColumn.AllowFocus = false;
            this.clmPrdId.OptionsColumn.AllowMove = false;
            // 
            // clmRoughSieve
            // 
            this.clmRoughSieve.Caption = "Sieve";
            this.clmRoughSieve.FieldName = "sieve_name";
            this.clmRoughSieve.Name = "clmRoughSieve";
            this.clmRoughSieve.OptionsColumn.AllowEdit = false;
            this.clmRoughSieve.OptionsColumn.AllowFocus = false;
            this.clmRoughSieve.OptionsColumn.AllowMove = false;
            this.clmRoughSieve.Visible = true;
            this.clmRoughSieve.VisibleIndex = 2;
            // 
            // clmPurity
            // 
            this.clmPurity.Caption = "Purity";
            this.clmPurity.FieldName = "purity_name";
            this.clmPurity.Name = "clmPurity";
            this.clmPurity.OptionsColumn.AllowEdit = false;
            this.clmPurity.OptionsColumn.AllowFocus = false;
            this.clmPurity.OptionsColumn.AllowMove = false;
            this.clmPurity.Visible = true;
            this.clmPurity.VisibleIndex = 3;
            // 
            // clmQuality
            // 
            this.clmQuality.Caption = "Quality";
            this.clmQuality.FieldName = "quality_name";
            this.clmQuality.Name = "clmQuality";
            this.clmQuality.OptionsColumn.AllowEdit = false;
            this.clmQuality.OptionsColumn.AllowFocus = false;
            this.clmQuality.OptionsColumn.AllowMove = false;
            this.clmQuality.Visible = true;
            this.clmQuality.VisibleIndex = 4;
            // 
            // clmCarat
            // 
            this.clmCarat.Caption = "Carat";
            this.clmCarat.FieldName = "carat";
            this.clmCarat.Name = "clmCarat";
            this.clmCarat.OptionsColumn.AllowEdit = false;
            this.clmCarat.OptionsColumn.AllowFocus = false;
            this.clmCarat.OptionsColumn.AllowMove = false;
            this.clmCarat.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.clmCarat.Visible = true;
            this.clmCarat.VisibleIndex = 6;
            // 
            // clmRate
            // 
            this.clmRate.Caption = "Rate";
            this.clmRate.FieldName = "rate";
            this.clmRate.Name = "clmRate";
            this.clmRate.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.clmRate.Visible = true;
            this.clmRate.VisibleIndex = 7;
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
            this.clmAmount.VisibleIndex = 8;
            // 
            // clmClarity
            // 
            this.clmClarity.Caption = "Clarity";
            this.clmClarity.FieldName = "rough_clarity_name";
            this.clmClarity.Name = "clmClarity";
            this.clmClarity.OptionsColumn.AllowEdit = false;
            this.clmClarity.OptionsColumn.AllowFocus = false;
            this.clmClarity.OptionsColumn.AllowMove = false;
            this.clmClarity.Visible = true;
            this.clmClarity.VisibleIndex = 5;
            this.clmClarity.Width = 94;
            // 
            // clmFlag
            // 
            this.clmFlag.Caption = "Flag";
            this.clmFlag.FieldName = "flag";
            this.clmFlag.Name = "clmFlag";
            // 
            // ClmRoughCutID
            // 
            this.ClmRoughCutID.Caption = "Rough Cut ID";
            this.ClmRoughCutID.FieldName = "rough_cut_id";
            this.ClmRoughCutID.Name = "ClmRoughCutID";
            // 
            // ClmRoughCutNo
            // 
            this.ClmRoughCutNo.Caption = "Cut No";
            this.ClmRoughCutNo.FieldName = "rough_cut_no";
            this.ClmRoughCutNo.Name = "ClmRoughCutNo";
            this.ClmRoughCutNo.OptionsColumn.AllowEdit = false;
            this.ClmRoughCutNo.OptionsColumn.AllowFocus = false;
            this.ClmRoughCutNo.OptionsColumn.AllowMove = false;
            this.ClmRoughCutNo.Visible = true;
            this.ClmRoughCutNo.VisibleIndex = 1;
            this.ClmRoughCutNo.Width = 156;
            // 
            // RepRecDate
            // 
            this.RepRecDate.AutoHeight = false;
            this.RepRecDate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.RepRecDate.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.RepRecDate.CalendarTimeProperties.TouchUIMaxValue = new System.DateTime(9999, 12, 31, 0, 0, 0, 0);
            this.RepRecDate.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.RepRecDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.RepRecDate.EditFormat.FormatString = "dd/MM/yyyy";
            this.RepRecDate.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.RepRecDate.Mask.EditMask = "dd-MMM-yyyy";
            this.RepRecDate.Mask.UseMaskAsDisplayFormat = true;
            this.RepRecDate.MaxValue = new System.DateTime(9999, 12, 31, 0, 0, 0, 0);
            this.RepRecDate.Name = "RepRecDate";
            // 
            // RepBtnDelete
            // 
            this.RepBtnDelete.AutoHeight = false;
            this.RepBtnDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Delete", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "Delete This Row", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.RepBtnDelete.Name = "RepBtnDelete";
            this.RepBtnDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // bandedGridView2
            // 
            this.bandedGridView2.Name = "bandedGridView2";
            // 
            // gridBand2
            // 
            this.gridBand2.Caption = "gridBand1";
            this.gridBand2.Name = "gridBand2";
            this.gridBand2.VisibleIndex = -1;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblType.Location = new System.Drawing.Point(1088, 18);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(35, 13);
            this.lblType.TabIndex = 613;
            this.lblType.Text = "Type";
            this.lblType.Visible = false;
            // 
            // lueType
            // 
            this.lueType.EnterMoveNextControl = true;
            this.lueType.Location = new System.Drawing.Point(1126, 15);
            this.lueType.Name = "lueType";
            this.lueType.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueType.Properties.Appearance.Options.UseFont = true;
            this.lueType.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.lueType.Properties.AppearanceDropDown.Options.UseFont = true;
            this.lueType.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold);
            this.lueType.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.lueType.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.lueType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueType.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("type", "Type")});
            this.lueType.Properties.NullText = "";
            this.lueType.Properties.ShowHeader = false;
            this.lueType.Size = new System.Drawing.Size(130, 22);
            this.lueType.TabIndex = 612;
            this.lueType.Visible = false;
            // 
            // lueProcess
            // 
            this.lueProcess.EnterMoveNextControl = true;
            this.lueProcess.Location = new System.Drawing.Point(940, 15);
            this.lueProcess.Name = "lueProcess";
            this.lueProcess.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueProcess.Properties.Appearance.Options.UseFont = true;
            this.lueProcess.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.lueProcess.Properties.AppearanceDropDown.Options.UseFont = true;
            this.lueProcess.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold);
            this.lueProcess.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.lueProcess.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.lueProcess.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.lueProcess.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("process_name", "Process Name"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("process_id", "Process ID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lueProcess.Properties.NullText = "";
            this.lueProcess.Properties.ShowHeader = false;
            this.lueProcess.Size = new System.Drawing.Size(146, 22);
            this.lueProcess.TabIndex = 3;
            this.lueProcess.Visible = false;
            this.lueProcess.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.lueProcess_ButtonClick);
            // 
            // lblProcess
            // 
            this.lblProcess.AutoSize = true;
            this.lblProcess.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblProcess.Location = new System.Drawing.Point(885, 18);
            this.lblProcess.Name = "lblProcess";
            this.lblProcess.Size = new System.Drawing.Size(51, 13);
            this.lblProcess.TabIndex = 0;
            this.lblProcess.Text = "Process";
            this.lblProcess.Visible = false;
            // 
            // lueKapan
            // 
            this.lueKapan.EnterMoveNextControl = true;
            this.lueKapan.Location = new System.Drawing.Point(267, 13);
            this.lueKapan.Name = "lueKapan";
            this.lueKapan.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueKapan.Properties.Appearance.Options.UseFont = true;
            this.lueKapan.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.lueKapan.Properties.AppearanceDropDown.Options.UseFont = true;
            this.lueKapan.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold);
            this.lueKapan.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.lueKapan.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.lueKapan.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueKapan.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("kapan_no", "Kapan No"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("kapan_id", "Kapan Id", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lueKapan.Properties.NullText = "";
            this.lueKapan.Properties.ShowHeader = false;
            this.lueKapan.Size = new System.Drawing.Size(196, 22);
            this.lueKapan.TabIndex = 1;
            this.lueKapan.EditValueChanged += new System.EventHandler(this.lueKapan_EditValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(203, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 551;
            this.label5.Text = "Kapan No";
            // 
            // dtpEntryDate
            // 
            this.dtpEntryDate.EditValue = null;
            this.dtpEntryDate.EnterMoveNextControl = true;
            this.dtpEntryDate.Location = new System.Drawing.Point(85, 12);
            this.dtpEntryDate.Name = "dtpEntryDate";
            this.dtpEntryDate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtpEntryDate.Properties.Appearance.Options.UseFont = true;
            this.dtpEntryDate.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtpEntryDate.Properties.AppearanceDropDown.Options.UseFont = true;
            this.dtpEntryDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpEntryDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpEntryDate.Size = new System.Drawing.Size(115, 22);
            this.dtpEntryDate.TabIndex = 0;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(5, 15);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(67, 13);
            this.label11.TabIndex = 101;
            this.label11.Text = "Entry Date";
            // 
            // btnSearch
            // 
            this.btnSearch.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Appearance.Options.UseFont = true;
            this.btnSearch.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.ImageOptions.Image")));
            this.btnSearch.Location = new System.Drawing.Point(469, 7);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(102, 32);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "S&earch";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // backgroundWorker_MakablePcsUpdate
            // 
            this.backgroundWorker_MakablePcsUpdate.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_MakablePcsUpdate_DoWork);
            this.backgroundWorker_MakablePcsUpdate.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_MakablePcsUpdate_RunWorkerCompleted);
            // 
            // panelIssueReceiveSearch
            // 
            this.panelIssueReceiveSearch.Controls.Add(this.label11);
            this.panelIssueReceiveSearch.Controls.Add(this.lblType);
            this.panelIssueReceiveSearch.Controls.Add(this.btnSearch);
            this.panelIssueReceiveSearch.Controls.Add(this.lueType);
            this.panelIssueReceiveSearch.Controls.Add(this.dtpEntryDate);
            this.panelIssueReceiveSearch.Controls.Add(this.label5);
            this.panelIssueReceiveSearch.Controls.Add(this.lueKapan);
            this.panelIssueReceiveSearch.Controls.Add(this.lblProcess);
            this.panelIssueReceiveSearch.Controls.Add(this.lueProcess);
            this.panelIssueReceiveSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelIssueReceiveSearch.Location = new System.Drawing.Point(0, 0);
            this.panelIssueReceiveSearch.Name = "panelIssueReceiveSearch";
            this.panelIssueReceiveSearch.Size = new System.Drawing.Size(1241, 50);
            this.panelIssueReceiveSearch.TabIndex = 101;
            // 
            // FrmMFGMakablePCsUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1241, 495);
            this.Controls.Add(this.PanelIssueReceive);
            this.Controls.Add(this.panelIssueReceiveSearch);
            this.Controls.Add(this.panelControl2);
            this.Name = "FrmMFGMakablePCsUpdate";
            this.Text = "Makable Pcs Update";
            this.Load += new System.EventHandler(this.FrmMFGProcessChipiyoRecieve_Load);
            this.ContextMNExport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PanelIssueReceive)).EndInit();
            this.PanelIssueReceive.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelProgress)).EndInit();
            this.panelProgress.ResumeLayout(false);
            this.panelProgress.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SaveProgressBar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMakablePcs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMakablePcs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repChkSel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repLueRejectionPurity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvProcessIssue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepRecDate.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepRecDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepBtnDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueProcess.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueKapan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEntryDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEntryDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelIssueReceiveSearch)).EndInit();
            this.panelIssueReceiveSearch.ResumeLayout(false);
            this.panelIssueReceiveSearch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl PanelIssueReceive;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraEditors.DateEdit dtpEntryDate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ContextMenuStrip ContextMNExport;
        private System.Windows.Forms.ToolStripMenuItem MNExportExcel;
        private System.Windows.Forms.ToolStripMenuItem MNExportPDF;
        private System.Windows.Forms.ToolStripMenuItem MNExportTEXT;
        private System.Windows.Forms.ToolStripMenuItem MNExportHTML;
        private System.Windows.Forms.ToolStripMenuItem MNExportRTF;
        private System.Windows.Forms.ToolStripMenuItem MNExportCSV;
        private System.ComponentModel.BackgroundWorker backgroundWorker_MakablePcsUpdate;
        private DevExpress.XtraEditors.LookUpEdit lueKapan;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.LookUpEdit lueProcess;
        private System.Windows.Forms.Label lblProcess;
        private DevExpress.XtraGrid.Views.Grid.GridView grvProcessIssue;
        private DevExpress.XtraGrid.Columns.GridColumn clmIssueID;
        private DevExpress.XtraGrid.Columns.GridColumn clmRoughSieve;
        private DevExpress.XtraGrid.Columns.GridColumn clmPurity;
        private DevExpress.XtraGrid.Columns.GridColumn clmQuality;
        private DevExpress.XtraGrid.Columns.GridColumn clmCarat;
        private DevExpress.XtraGrid.Columns.GridColumn clmRate;
        private DevExpress.XtraGrid.Columns.GridColumn clmAmount;
        private DevExpress.XtraGrid.Columns.GridColumn clmPrdId;
        private DevExpress.XtraGrid.Columns.GridColumn clmClarity;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit RepRecDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit RepBtnDelete;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView2;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
        private DevExpress.XtraGrid.Columns.GridColumn clmFlag;
        private DevExpress.XtraGrid.Columns.GridColumn ClmRoughCutID;
        private DevExpress.XtraGrid.Columns.GridColumn ClmRoughCutNo;
        private DevExpress.XtraGrid.GridControl grdMakablePcs;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvMakablePcs;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repChkSel;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repLueRejectionPurity;
        private DevExpress.XtraGrid.Columns.GridColumn ClmProcessID;
        private DevExpress.XtraGrid.Columns.GridColumn ClmProcess;
        private System.Windows.Forms.Label lblType;
        private DevExpress.XtraEditors.LookUpEdit lueType;
        private DevExpress.XtraEditors.PanelControl panelIssueReceiveSearch;
        private DevExpress.XtraEditors.PanelControl panelProgress;
        private System.Windows.Forms.Label lblProgressCount;
        private DevExpress.XtraEditors.MarqueeProgressBarControl SaveProgressBar;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
    }
}