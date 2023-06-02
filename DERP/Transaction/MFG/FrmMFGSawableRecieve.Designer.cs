namespace DERP.Transaction.MFG
{
    partial class FrmMFGSawableRecieve
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMFGSawableRecieve));
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions4 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject13 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject14 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject15 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject16 = new DevExpress.Utils.SerializableAppearanceObject();
            this.RepRecDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.grdSawableRecieve = new DevExpress.XtraGrid.GridControl();
            this.ContextMNExport = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MNExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportPDF = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportTEXT = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportHTML = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportRTF = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportCSV = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvSawableRecieve = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.clmRecieveID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmIssueDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmLotID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmCutNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmPurityGroup = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmPurityGroupID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmPurity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmPurityID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmPcs = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmCarat = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmRRPcs = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmRRCarat = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmSrno = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmCaratPlus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmCaratLoss = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ClmDelete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.RepDelete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.ClmRoughCutID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ClmKapanID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ClmOSPcs = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ClmOSCarat = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblOsPcs = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lblOsCarat = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.lueProcess = new DevExpress.XtraEditors.LookUpEdit();
            this.label17 = new System.Windows.Forms.Label();
            this.lueSubProcess = new DevExpress.XtraEditors.LookUpEdit();
            this.label18 = new System.Windows.Forms.Label();
            this.txtWeightPlus = new DevExpress.XtraEditors.TextEdit();
            this.label14 = new System.Windows.Forms.Label();
            this.txtWeightLoss = new DevExpress.XtraEditors.TextEdit();
            this.label15 = new System.Windows.Forms.Label();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.lueKapan = new DevExpress.XtraEditors.LookUpEdit();
            this.label13 = new System.Windows.Forms.Label();
            this.txtIssProcess = new DevExpress.XtraEditors.TextEdit();
            this.label12 = new System.Windows.Forms.Label();
            this.txtRRPcs = new DevExpress.XtraEditors.TextEdit();
            this.label10 = new System.Windows.Forms.Label();
            this.txtRRCarat = new DevExpress.XtraEditors.TextEdit();
            this.label11 = new System.Windows.Forms.Label();
            this.txtBalancePcs = new DevExpress.XtraEditors.TextEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAmount = new DevExpress.XtraEditors.TextEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRate = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.label9 = new System.Windows.Forms.Label();
            this.txtPcs = new DevExpress.XtraEditors.TextEdit();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCarat = new DevExpress.XtraEditors.TextEdit();
            this.label8 = new System.Windows.Forms.Label();
            this.txtLotID = new DevExpress.XtraEditors.TextEdit();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBalanceCarat = new DevExpress.XtraEditors.TextEdit();
            this.label16 = new System.Windows.Forms.Label();
            this.luePurityGroup = new DevExpress.XtraEditors.LookUpEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.lueCutNo = new DevExpress.XtraEditors.LookUpEdit();
            this.lblKapanNo = new System.Windows.Forms.Label();
            this.luePurity = new DevExpress.XtraEditors.LookUpEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpReceiveDate = new DevExpress.XtraEditors.DateEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.backgroundWorker_SoyebleReceive = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.RepRecDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepRecDate.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSawableRecieve)).BeginInit();
            this.ContextMNExport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSawableRecieve)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueProcess.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueSubProcess.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWeightPlus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWeightLoss.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueKapan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIssProcess.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRRPcs.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRRCarat.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBalancePcs.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPcs.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCarat.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBalanceCarat.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.luePurityGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueCutNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.luePurity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpReceiveDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpReceiveDate.Properties)).BeginInit();
            this.SuspendLayout();
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
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.ImageOptions.Image")));
            this.btnSave.Location = new System.Drawing.Point(949, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(102, 32);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grdSawableRecieve
            // 
            this.grdSawableRecieve.ContextMenuStrip = this.ContextMNExport;
            this.grdSawableRecieve.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSawableRecieve.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdSawableRecieve.Location = new System.Drawing.Point(2, 78);
            this.grdSawableRecieve.MainView = this.dgvSawableRecieve;
            this.grdSawableRecieve.Name = "grdSawableRecieve";
            this.grdSawableRecieve.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.RepDelete});
            this.grdSawableRecieve.Size = new System.Drawing.Size(1280, 371);
            this.grdSawableRecieve.TabIndex = 97;
            this.grdSawableRecieve.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvSawableRecieve,
            this.bandedGridView1});
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
            // dgvSawableRecieve
            // 
            this.dgvSawableRecieve.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.dgvSawableRecieve.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.dgvSawableRecieve.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            this.dgvSawableRecieve.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            this.dgvSawableRecieve.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.dgvSawableRecieve.Appearance.FooterPanel.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.dgvSawableRecieve.Appearance.FooterPanel.Options.UseFont = true;
            this.dgvSawableRecieve.Appearance.HeaderPanel.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.dgvSawableRecieve.Appearance.HeaderPanel.Options.UseFont = true;
            this.dgvSawableRecieve.Appearance.Row.Font = new System.Drawing.Font("Cambria", 9F);
            this.dgvSawableRecieve.Appearance.Row.Options.UseFont = true;
            this.dgvSawableRecieve.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.clmRecieveID,
            this.clmIssueDate,
            this.clmLotID,
            this.clmCutNo,
            this.clmPurityGroup,
            this.clmPurityGroupID,
            this.clmPurity,
            this.clmPurityID,
            this.clmPcs,
            this.clmCarat,
            this.clmRRPcs,
            this.clmRRCarat,
            this.clmRate,
            this.clmAmount,
            this.clmSrno,
            this.clmCaratPlus,
            this.clmCaratLoss,
            this.ClmDelete,
            this.ClmRoughCutID,
            this.ClmKapanID,
            this.ClmOSPcs,
            this.ClmOSCarat});
            this.dgvSawableRecieve.GridControl = this.grdSawableRecieve;
            this.dgvSawableRecieve.Name = "dgvSawableRecieve";
            this.dgvSawableRecieve.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dgvSawableRecieve.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.dgvSawableRecieve.OptionsCustomization.AllowColumnMoving = false;
            this.dgvSawableRecieve.OptionsCustomization.AllowQuickHideColumns = false;
            this.dgvSawableRecieve.OptionsNavigation.EnterMoveNextColumn = true;
            this.dgvSawableRecieve.OptionsView.ColumnAutoWidth = false;
            this.dgvSawableRecieve.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.dgvSawableRecieve.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.dgvSawableRecieve.OptionsView.ShowAutoFilterRow = true;
            this.dgvSawableRecieve.OptionsView.ShowFooter = true;
            this.dgvSawableRecieve.OptionsView.ShowGroupPanel = false;
            this.dgvSawableRecieve.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.dgvSawableRecieve_RowClick);
            this.dgvSawableRecieve.CustomSummaryCalculate += new DevExpress.Data.CustomSummaryEventHandler(this.dgvSawableRecieve_CustomSummaryCalculate);
            // 
            // clmRecieveID
            // 
            this.clmRecieveID.Caption = "Recieve ID";
            this.clmRecieveID.FieldName = "recieve_id";
            this.clmRecieveID.Name = "clmRecieveID";
            this.clmRecieveID.OptionsColumn.AllowEdit = false;
            // 
            // clmIssueDate
            // 
            this.clmIssueDate.Caption = "Rec. Date";
            this.clmIssueDate.ColumnEdit = this.RepRecDate;
            this.clmIssueDate.DisplayFormat.FormatString = "d";
            this.clmIssueDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.clmIssueDate.FieldName = "recieve_date";
            this.clmIssueDate.Name = "clmIssueDate";
            this.clmIssueDate.OptionsColumn.AllowEdit = false;
            this.clmIssueDate.Visible = true;
            this.clmIssueDate.VisibleIndex = 1;
            this.clmIssueDate.Width = 80;
            // 
            // clmLotID
            // 
            this.clmLotID.Caption = "Lot ID";
            this.clmLotID.FieldName = "lot_id";
            this.clmLotID.Name = "clmLotID";
            this.clmLotID.OptionsColumn.AllowEdit = false;
            this.clmLotID.Visible = true;
            this.clmLotID.VisibleIndex = 2;
            // 
            // clmCutNo
            // 
            this.clmCutNo.Caption = "Cut No";
            this.clmCutNo.FieldName = "cut_no";
            this.clmCutNo.Name = "clmCutNo";
            this.clmCutNo.OptionsColumn.AllowEdit = false;
            this.clmCutNo.Visible = true;
            this.clmCutNo.VisibleIndex = 3;
            this.clmCutNo.Width = 49;
            // 
            // clmPurityGroup
            // 
            this.clmPurityGroup.Caption = "Purity Group";
            this.clmPurityGroup.FieldName = "purity_group";
            this.clmPurityGroup.Name = "clmPurityGroup";
            this.clmPurityGroup.OptionsColumn.AllowEdit = false;
            this.clmPurityGroup.Visible = true;
            this.clmPurityGroup.VisibleIndex = 4;
            this.clmPurityGroup.Width = 125;
            // 
            // clmPurityGroupID
            // 
            this.clmPurityGroupID.Caption = "Purity Group ID";
            this.clmPurityGroupID.FieldName = "purity_group_id";
            this.clmPurityGroupID.Name = "clmPurityGroupID";
            this.clmPurityGroupID.OptionsColumn.AllowEdit = false;
            // 
            // clmPurity
            // 
            this.clmPurity.Caption = "Purity";
            this.clmPurity.FieldName = "purity_name";
            this.clmPurity.Name = "clmPurity";
            this.clmPurity.OptionsColumn.AllowEdit = false;
            this.clmPurity.Visible = true;
            this.clmPurity.VisibleIndex = 5;
            this.clmPurity.Width = 125;
            // 
            // clmPurityID
            // 
            this.clmPurityID.Caption = "Purity ID";
            this.clmPurityID.FieldName = "purity_id";
            this.clmPurityID.Name = "clmPurityID";
            this.clmPurityID.OptionsColumn.AllowEdit = false;
            // 
            // clmPcs
            // 
            this.clmPcs.Caption = "Pcs";
            this.clmPcs.FieldName = "pcs";
            this.clmPcs.Name = "clmPcs";
            this.clmPcs.OptionsColumn.AllowEdit = false;
            this.clmPcs.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.clmPcs.Visible = true;
            this.clmPcs.VisibleIndex = 6;
            this.clmPcs.Width = 45;
            // 
            // clmCarat
            // 
            this.clmCarat.Caption = "Carat";
            this.clmCarat.FieldName = "carat";
            this.clmCarat.Name = "clmCarat";
            this.clmCarat.OptionsColumn.AllowEdit = false;
            this.clmCarat.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.clmCarat.Visible = true;
            this.clmCarat.VisibleIndex = 7;
            // 
            // clmRRPcs
            // 
            this.clmRRPcs.Caption = "RR Pcs";
            this.clmRRPcs.FieldName = "rr_pcs";
            this.clmRRPcs.Name = "clmRRPcs";
            this.clmRRPcs.OptionsColumn.AllowEdit = false;
            this.clmRRPcs.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            // 
            // clmRRCarat
            // 
            this.clmRRCarat.Caption = "RR Carat";
            this.clmRRCarat.FieldName = "rr_carat";
            this.clmRRCarat.Name = "clmRRCarat";
            this.clmRRCarat.OptionsColumn.AllowEdit = false;
            this.clmRRCarat.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            // 
            // clmRate
            // 
            this.clmRate.Caption = "Rate";
            this.clmRate.FieldName = "rate";
            this.clmRate.Name = "clmRate";
            this.clmRate.OptionsColumn.AllowEdit = false;
            this.clmRate.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom)});
            this.clmRate.Visible = true;
            this.clmRate.VisibleIndex = 10;
            // 
            // clmAmount
            // 
            this.clmAmount.Caption = "Amount";
            this.clmAmount.FieldName = "amount";
            this.clmAmount.Name = "clmAmount";
            this.clmAmount.OptionsColumn.AllowEdit = false;
            this.clmAmount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.clmAmount.Visible = true;
            this.clmAmount.VisibleIndex = 11;
            // 
            // clmSrno
            // 
            this.clmSrno.Caption = "SrNo";
            this.clmSrno.FieldName = "sr_no";
            this.clmSrno.Name = "clmSrno";
            this.clmSrno.OptionsColumn.AllowEdit = false;
            // 
            // clmCaratPlus
            // 
            this.clmCaratPlus.Caption = "Carat Plus";
            this.clmCaratPlus.FieldName = "plus_carat";
            this.clmCaratPlus.Name = "clmCaratPlus";
            this.clmCaratPlus.OptionsColumn.AllowEdit = false;
            this.clmCaratPlus.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.clmCaratPlus.Visible = true;
            this.clmCaratPlus.VisibleIndex = 8;
            // 
            // clmCaratLoss
            // 
            this.clmCaratLoss.Caption = "Carat Loss";
            this.clmCaratLoss.FieldName = "loss_carat";
            this.clmCaratLoss.Name = "clmCaratLoss";
            this.clmCaratLoss.OptionsColumn.AllowEdit = false;
            this.clmCaratLoss.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.clmCaratLoss.Visible = true;
            this.clmCaratLoss.VisibleIndex = 9;
            // 
            // ClmDelete
            // 
            this.ClmDelete.Caption = "Del";
            this.ClmDelete.ColumnEdit = this.RepDelete;
            this.ClmDelete.FieldName = "ClmDelete";
            this.ClmDelete.Name = "ClmDelete";
            this.ClmDelete.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            this.ClmDelete.Visible = true;
            this.ClmDelete.VisibleIndex = 0;
            this.ClmDelete.Width = 32;
            // 
            // RepDelete
            // 
            this.RepDelete.AutoHeight = false;
            editorButtonImageOptions4.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions4.Image")));
            this.RepDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions4, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject13, serializableAppearanceObject14, serializableAppearanceObject15, serializableAppearanceObject16, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.RepDelete.Name = "RepDelete";
            this.RepDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.RepDelete.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.RepDelete_ButtonClick);
            // 
            // ClmRoughCutID
            // 
            this.ClmRoughCutID.Caption = "Rough Cut ID";
            this.ClmRoughCutID.FieldName = "rough_cut_id";
            this.ClmRoughCutID.Name = "ClmRoughCutID";
            // 
            // ClmKapanID
            // 
            this.ClmKapanID.Caption = "Kapan_ID";
            this.ClmKapanID.FieldName = "kapan_id";
            this.ClmKapanID.Name = "ClmKapanID";
            // 
            // ClmOSPcs
            // 
            this.ClmOSPcs.Caption = "OS Pcs";
            this.ClmOSPcs.FieldName = "os_pcs";
            this.ClmOSPcs.Name = "ClmOSPcs";
            this.ClmOSPcs.Visible = true;
            this.ClmOSPcs.VisibleIndex = 12;
            // 
            // ClmOSCarat
            // 
            this.ClmOSCarat.Caption = "OS Carat";
            this.ClmOSCarat.FieldName = "os_carat";
            this.ClmOSCarat.Name = "ClmOSCarat";
            this.ClmOSCarat.Visible = true;
            this.ClmOSCarat.VisibleIndex = 13;
            // 
            // bandedGridView1
            // 
            this.bandedGridView1.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1});
            this.bandedGridView1.GridControl = this.grdSawableRecieve;
            this.bandedGridView1.Name = "bandedGridView1";
            // 
            // gridBand1
            // 
            this.gridBand1.Caption = "gridBand1";
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.VisibleIndex = 0;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnSearch);
            this.panelControl2.Controls.Add(this.btnExport);
            this.panelControl2.Controls.Add(this.btnExit);
            this.panelControl2.Controls.Add(this.btnClear);
            this.panelControl2.Controls.Add(this.btnSave);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 451);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1284, 44);
            this.panelControl2.TabIndex = 98;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Appearance.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Appearance.Options.UseFont = true;
            this.btnSearch.ImageOptions.Image = global::DERP.Properties.Resources.Search;
            this.btnSearch.Location = new System.Drawing.Point(831, 6);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(112, 32);
            this.btnSearch.TabIndex = 555;
            this.btnSearch.Text = "Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Appearance.Options.UseFont = true;
            this.btnExport.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.ImageOptions.Image")));
            this.btnExport.Location = new System.Drawing.Point(682, 6);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(102, 32);
            this.btnExport.TabIndex = 0;
            this.btnExport.Text = "&Export";
            this.btnExport.Visible = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Appearance.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Appearance.Options.UseFont = true;
            this.btnExit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.ImageOptions.Image")));
            this.btnExit.Location = new System.Drawing.Point(1166, 6);
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
            this.btnClear.Location = new System.Drawing.Point(1058, 6);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(102, 32);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "&Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.grdSawableRecieve);
            this.panelControl3.Controls.Add(this.panelControl1);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(0, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(1284, 451);
            this.panelControl3.TabIndex = 99;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lblOsPcs);
            this.panelControl1.Controls.Add(this.label19);
            this.panelControl1.Controls.Add(this.lblOsCarat);
            this.panelControl1.Controls.Add(this.label20);
            this.panelControl1.Controls.Add(this.lueProcess);
            this.panelControl1.Controls.Add(this.label17);
            this.panelControl1.Controls.Add(this.lueSubProcess);
            this.panelControl1.Controls.Add(this.label18);
            this.panelControl1.Controls.Add(this.txtWeightPlus);
            this.panelControl1.Controls.Add(this.label14);
            this.panelControl1.Controls.Add(this.txtWeightLoss);
            this.panelControl1.Controls.Add(this.label15);
            this.panelControl1.Controls.Add(this.btnAdd);
            this.panelControl1.Controls.Add(this.lueKapan);
            this.panelControl1.Controls.Add(this.label13);
            this.panelControl1.Controls.Add(this.txtIssProcess);
            this.panelControl1.Controls.Add(this.label12);
            this.panelControl1.Controls.Add(this.txtRRPcs);
            this.panelControl1.Controls.Add(this.label10);
            this.panelControl1.Controls.Add(this.txtRRCarat);
            this.panelControl1.Controls.Add(this.label11);
            this.panelControl1.Controls.Add(this.txtBalancePcs);
            this.panelControl1.Controls.Add(this.label4);
            this.panelControl1.Controls.Add(this.txtAmount);
            this.panelControl1.Controls.Add(this.label5);
            this.panelControl1.Controls.Add(this.txtRate);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.label9);
            this.panelControl1.Controls.Add(this.txtPcs);
            this.panelControl1.Controls.Add(this.label7);
            this.panelControl1.Controls.Add(this.txtCarat);
            this.panelControl1.Controls.Add(this.label8);
            this.panelControl1.Controls.Add(this.txtLotID);
            this.panelControl1.Controls.Add(this.label6);
            this.panelControl1.Controls.Add(this.txtBalanceCarat);
            this.panelControl1.Controls.Add(this.label16);
            this.panelControl1.Controls.Add(this.luePurityGroup);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.lueCutNo);
            this.panelControl1.Controls.Add(this.lblKapanNo);
            this.panelControl1.Controls.Add(this.luePurity);
            this.panelControl1.Controls.Add(this.label3);
            this.panelControl1.Controls.Add(this.dtpReceiveDate);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(2, 2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1280, 76);
            this.panelControl1.TabIndex = 98;
            // 
            // lblOsPcs
            // 
            this.lblOsPcs.AutoSize = true;
            this.lblOsPcs.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblOsPcs.ForeColor = System.Drawing.Color.Red;
            this.lblOsPcs.Location = new System.Drawing.Point(1139, 11);
            this.lblOsPcs.Name = "lblOsPcs";
            this.lblOsPcs.Size = new System.Drawing.Size(15, 15);
            this.lblOsPcs.TabIndex = 603;
            this.lblOsPcs.Text = "0";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.label19.Location = new System.Drawing.Point(1082, 11);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(57, 15);
            this.label19.TabIndex = 602;
            this.label19.Text = "O/S Pcs:";
            // 
            // lblOsCarat
            // 
            this.lblOsCarat.AutoSize = true;
            this.lblOsCarat.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblOsCarat.ForeColor = System.Drawing.Color.Red;
            this.lblOsCarat.Location = new System.Drawing.Point(1225, 11);
            this.lblOsCarat.Name = "lblOsCarat";
            this.lblOsCarat.Size = new System.Drawing.Size(15, 15);
            this.lblOsCarat.TabIndex = 601;
            this.lblOsCarat.Text = "0";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.label20.Location = new System.Drawing.Point(1168, 11);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(55, 15);
            this.label20.TabIndex = 600;
            this.label20.Text = "O/S Crt:";
            // 
            // lueProcess
            // 
            this.lueProcess.EnterMoveNextControl = true;
            this.lueProcess.Location = new System.Drawing.Point(734, 7);
            this.lueProcess.Name = "lueProcess";
            this.lueProcess.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueProcess.Properties.Appearance.Options.UseFont = true;
            this.lueProcess.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
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
            this.lueProcess.Size = new System.Drawing.Size(122, 22);
            this.lueProcess.TabIndex = 583;
            this.lueProcess.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.lueProcess_ButtonClick);
            this.lueProcess.EditValueChanged += new System.EventHandler(this.lueProcess_EditValueChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.label17.Location = new System.Drawing.Point(680, 11);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(53, 15);
            this.label17.TabIndex = 586;
            this.label17.Text = "Process";
            // 
            // lueSubProcess
            // 
            this.lueSubProcess.EnterMoveNextControl = true;
            this.lueSubProcess.Location = new System.Drawing.Point(939, 7);
            this.lueSubProcess.Name = "lueSubProcess";
            this.lueSubProcess.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueSubProcess.Properties.Appearance.Options.UseFont = true;
            this.lueSubProcess.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.lueSubProcess.Properties.AppearanceDropDown.Options.UseFont = true;
            this.lueSubProcess.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold);
            this.lueSubProcess.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.lueSubProcess.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.lueSubProcess.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.lueSubProcess.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("sub_process_name", "Sub Process Name"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("sub_process_id", "Sub Process ID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lueSubProcess.Properties.NullText = "";
            this.lueSubProcess.Properties.ShowHeader = false;
            this.lueSubProcess.Size = new System.Drawing.Size(140, 22);
            this.lueSubProcess.TabIndex = 584;
            this.lueSubProcess.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.lueSubProcess_ButtonClick);
            this.lueSubProcess.EditValueChanged += new System.EventHandler(this.lueSubProcess_EditValueChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.label18.Location = new System.Drawing.Point(860, 11);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(79, 15);
            this.label18.TabIndex = 585;
            this.label18.Text = "Sub Process";
            // 
            // txtWeightPlus
            // 
            this.txtWeightPlus.EnterMoveNextControl = true;
            this.txtWeightPlus.Location = new System.Drawing.Point(791, 40);
            this.txtWeightPlus.Name = "txtWeightPlus";
            this.txtWeightPlus.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWeightPlus.Properties.Appearance.Options.UseFont = true;
            this.txtWeightPlus.Size = new System.Drawing.Size(63, 22);
            this.txtWeightPlus.TabIndex = 12;
            this.txtWeightPlus.EditValueChanged += new System.EventHandler(this.txtWeightPlus_EditValueChanged);
            this.txtWeightPlus.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtWeightPlus_KeyPress);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.label14.Location = new System.Drawing.Point(721, 44);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(70, 15);
            this.label14.TabIndex = 582;
            this.label14.Text = "Weight (+)";
            // 
            // txtWeightLoss
            // 
            this.txtWeightLoss.EnterMoveNextControl = true;
            this.txtWeightLoss.Location = new System.Drawing.Point(658, 40);
            this.txtWeightLoss.Name = "txtWeightLoss";
            this.txtWeightLoss.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWeightLoss.Properties.Appearance.Options.UseFont = true;
            this.txtWeightLoss.Size = new System.Drawing.Size(59, 22);
            this.txtWeightLoss.TabIndex = 11;
            this.txtWeightLoss.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtWeightLoss_KeyPress);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.label15.Location = new System.Drawing.Point(591, 44);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(66, 15);
            this.label15.TabIndex = 581;
            this.label15.Text = "Weight (-)";
            // 
            // btnAdd
            // 
            this.btnAdd.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Appearance.Options.UseFont = true;
            this.btnAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.ImageOptions.Image")));
            this.btnAdd.Location = new System.Drawing.Point(1089, 34);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 32);
            this.btnAdd.TabIndex = 15;
            this.btnAdd.Text = "&Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lueKapan
            // 
            this.lueKapan.EnterMoveNextControl = true;
            this.lueKapan.Location = new System.Drawing.Point(278, 8);
            this.lueKapan.Name = "lueKapan";
            this.lueKapan.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueKapan.Properties.Appearance.Options.UseFont = true;
            this.lueKapan.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
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
            this.lueKapan.Size = new System.Drawing.Size(100, 22);
            this.lueKapan.TabIndex = 1;
            this.lueKapan.EditValueChanged += new System.EventHandler(this.lueKapan_EditValueChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.label13.Location = new System.Drawing.Point(212, 12);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 15);
            this.label13.TabIndex = 578;
            this.label13.Text = "Kapan No";
            // 
            // txtIssProcess
            // 
            this.txtIssProcess.Enabled = false;
            this.txtIssProcess.EnterMoveNextControl = true;
            this.txtIssProcess.Location = new System.Drawing.Point(1299, 49);
            this.txtIssProcess.Name = "txtIssProcess";
            this.txtIssProcess.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIssProcess.Properties.Appearance.Options.UseFont = true;
            this.txtIssProcess.Properties.ReadOnly = true;
            this.txtIssProcess.Size = new System.Drawing.Size(37, 20);
            this.txtIssProcess.TabIndex = 6;
            this.txtIssProcess.Visible = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(1209, 43);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(73, 13);
            this.label12.TabIndex = 577;
            this.label12.Text = "Issue Process";
            this.label12.Visible = false;
            // 
            // txtRRPcs
            // 
            this.txtRRPcs.EnterMoveNextControl = true;
            this.txtRRPcs.Location = new System.Drawing.Point(1252, 57);
            this.txtRRPcs.Name = "txtRRPcs";
            this.txtRRPcs.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRRPcs.Properties.Appearance.Options.UseFont = true;
            this.txtRRPcs.Size = new System.Drawing.Size(30, 20);
            this.txtRRPcs.TabIndex = 572;
            this.txtRRPcs.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(1207, 60);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 13);
            this.label10.TabIndex = 573;
            this.label10.Text = "RR Pcs";
            this.label10.Visible = false;
            // 
            // txtRRCarat
            // 
            this.txtRRCarat.EnterMoveNextControl = true;
            this.txtRRCarat.Location = new System.Drawing.Point(1252, 38);
            this.txtRRCarat.Name = "txtRRCarat";
            this.txtRRCarat.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRRCarat.Properties.Appearance.Options.UseFont = true;
            this.txtRRCarat.Size = new System.Drawing.Size(28, 20);
            this.txtRRCarat.TabIndex = 574;
            this.txtRRCarat.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(1210, 42);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(51, 13);
            this.label11.TabIndex = 575;
            this.label11.Text = "RR Carat";
            this.label11.Visible = false;
            // 
            // txtBalancePcs
            // 
            this.txtBalancePcs.Enabled = false;
            this.txtBalancePcs.EnterMoveNextControl = true;
            this.txtBalancePcs.Location = new System.Drawing.Point(1289, 54);
            this.txtBalancePcs.Name = "txtBalancePcs";
            this.txtBalancePcs.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBalancePcs.Properties.Appearance.Options.UseFont = true;
            this.txtBalancePcs.Properties.ReadOnly = true;
            this.txtBalancePcs.Size = new System.Drawing.Size(61, 20);
            this.txtBalancePcs.TabIndex = 4;
            this.txtBalancePcs.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1246, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 571;
            this.label4.Text = "Bal Pcs";
            this.label4.Visible = false;
            // 
            // txtAmount
            // 
            this.txtAmount.Enabled = false;
            this.txtAmount.EnterMoveNextControl = true;
            this.txtAmount.Location = new System.Drawing.Point(1014, 40);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.Properties.Appearance.Options.UseFont = true;
            this.txtAmount.Properties.ReadOnly = true;
            this.txtAmount.Size = new System.Drawing.Size(72, 22);
            this.txtAmount.TabIndex = 14;
            this.txtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmount_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(958, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 15);
            this.label5.TabIndex = 568;
            this.label5.Text = "Amount";
            // 
            // txtRate
            // 
            this.txtRate.EnterMoveNextControl = true;
            this.txtRate.Location = new System.Drawing.Point(892, 40);
            this.txtRate.Name = "txtRate";
            this.txtRate.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRate.Properties.Appearance.Options.UseFont = true;
            this.txtRate.Size = new System.Drawing.Size(61, 22);
            this.txtRate.TabIndex = 13;
            this.txtRate.EditValueChanged += new System.EventHandler(this.txtRate_EditValueChanged);
            this.txtRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRate_KeyPress);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl2.Appearance.Options.UseBackColor = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Location = new System.Drawing.Point(590, 8);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(9, 13);
            this.labelControl2.TabIndex = 566;
            this.labelControl2.Text = "* ";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl1.Appearance.Options.UseBackColor = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(427, 8);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(9, 13);
            this.labelControl1.TabIndex = 565;
            this.labelControl1.Text = "* ";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl3.Appearance.Options.UseBackColor = true;
            this.labelControl3.Appearance.Options.UseForeColor = true;
            this.labelControl3.Location = new System.Drawing.Point(73, 8);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(9, 13);
            this.labelControl3.TabIndex = 564;
            this.labelControl3.Text = "* ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(856, 44);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 15);
            this.label9.TabIndex = 562;
            this.label9.Text = "Rate";
            // 
            // txtPcs
            // 
            this.txtPcs.EnterMoveNextControl = true;
            this.txtPcs.Location = new System.Drawing.Point(398, 40);
            this.txtPcs.Name = "txtPcs";
            this.txtPcs.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPcs.Properties.Appearance.Options.UseFont = true;
            this.txtPcs.Size = new System.Drawing.Size(78, 22);
            this.txtPcs.TabIndex = 9;
            this.txtPcs.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPcs_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(350, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(27, 15);
            this.label7.TabIndex = 559;
            this.label7.Text = "Pcs";
            // 
            // txtCarat
            // 
            this.txtCarat.EnterMoveNextControl = true;
            this.txtCarat.Location = new System.Drawing.Point(519, 40);
            this.txtCarat.Name = "txtCarat";
            this.txtCarat.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCarat.Properties.Appearance.Options.UseFont = true;
            this.txtCarat.Size = new System.Drawing.Size(61, 22);
            this.txtCarat.TabIndex = 10;
            this.txtCarat.EditValueChanged += new System.EventHandler(this.txtCarat_EditValueChanged);
            this.txtCarat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCarat_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(482, 44);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 15);
            this.label8.TabIndex = 561;
            this.label8.Text = "Carat";
            // 
            // txtLotID
            // 
            this.txtLotID.EnterMoveNextControl = true;
            this.txtLotID.Location = new System.Drawing.Point(601, 8);
            this.txtLotID.Name = "txtLotID";
            this.txtLotID.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLotID.Properties.Appearance.Options.UseFont = true;
            this.txtLotID.Size = new System.Drawing.Size(77, 22);
            this.txtLotID.TabIndex = 3;
            this.txtLotID.EditValueChanged += new System.EventHandler(this.txtLotID_EditValueChanged);
            this.txtLotID.Validated += new System.EventHandler(this.txtLotID_Validated);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(564, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 15);
            this.label6.TabIndex = 556;
            this.label6.Text = "Lot";
            // 
            // txtBalanceCarat
            // 
            this.txtBalanceCarat.Enabled = false;
            this.txtBalanceCarat.EnterMoveNextControl = true;
            this.txtBalanceCarat.Location = new System.Drawing.Point(1288, 48);
            this.txtBalanceCarat.Name = "txtBalanceCarat";
            this.txtBalanceCarat.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBalanceCarat.Properties.Appearance.Options.UseFont = true;
            this.txtBalanceCarat.Properties.ReadOnly = true;
            this.txtBalanceCarat.Size = new System.Drawing.Size(61, 20);
            this.txtBalanceCarat.TabIndex = 5;
            this.txtBalanceCarat.Visible = false;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(1231, 55);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(51, 13);
            this.label16.TabIndex = 555;
            this.label16.Text = "Bal Carat";
            this.label16.Visible = false;
            // 
            // luePurityGroup
            // 
            this.luePurityGroup.EnterMoveNextControl = true;
            this.luePurityGroup.Location = new System.Drawing.Point(85, 40);
            this.luePurityGroup.Name = "luePurityGroup";
            this.luePurityGroup.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.luePurityGroup.Properties.Appearance.Options.UseFont = true;
            this.luePurityGroup.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.luePurityGroup.Properties.AppearanceDropDown.Options.UseFont = true;
            this.luePurityGroup.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold);
            this.luePurityGroup.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.luePurityGroup.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.luePurityGroup.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.luePurityGroup.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("purity_group", "Purity Group"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("purity_group_id", "Purity Group ID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.luePurityGroup.Properties.NullText = "";
            this.luePurityGroup.Properties.ShowHeader = false;
            this.luePurityGroup.Size = new System.Drawing.Size(100, 22);
            this.luePurityGroup.TabIndex = 7;
            this.luePurityGroup.Visible = false;
            this.luePurityGroup.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.luePurityGroup_ButtonClick);
            this.luePurityGroup.EditValueChanged += new System.EventHandler(this.luePurityGroup_EditValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(2, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 15);
            this.label2.TabIndex = 548;
            this.label2.Text = "Purity Group";
            // 
            // lueCutNo
            // 
            this.lueCutNo.EnterMoveNextControl = true;
            this.lueCutNo.Location = new System.Drawing.Point(438, 8);
            this.lueCutNo.Name = "lueCutNo";
            this.lueCutNo.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueCutNo.Properties.Appearance.Options.UseFont = true;
            this.lueCutNo.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.lueCutNo.Properties.AppearanceDropDown.Options.UseFont = true;
            this.lueCutNo.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold);
            this.lueCutNo.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.lueCutNo.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.lueCutNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueCutNo.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("rough_cut_no", "Rough Cut No"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("rough_cut_id", "Rough Cut ID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lueCutNo.Properties.NullText = "";
            this.lueCutNo.Properties.ShowHeader = false;
            this.lueCutNo.Size = new System.Drawing.Size(120, 22);
            this.lueCutNo.TabIndex = 2;
            this.lueCutNo.Validated += new System.EventHandler(this.lueCutNo_Validated);
            // 
            // lblKapanNo
            // 
            this.lblKapanNo.AutoSize = true;
            this.lblKapanNo.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblKapanNo.Location = new System.Drawing.Point(382, 12);
            this.lblKapanNo.Name = "lblKapanNo";
            this.lblKapanNo.Size = new System.Drawing.Size(46, 15);
            this.lblKapanNo.TabIndex = 545;
            this.lblKapanNo.Text = "Cut No";
            // 
            // luePurity
            // 
            this.luePurity.EnterMoveNextControl = true;
            this.luePurity.Location = new System.Drawing.Point(246, 40);
            this.luePurity.Name = "luePurity";
            this.luePurity.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.luePurity.Properties.Appearance.Options.UseFont = true;
            this.luePurity.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.luePurity.Properties.AppearanceDropDown.Options.UseFont = true;
            this.luePurity.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold);
            this.luePurity.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.luePurity.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.luePurity.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.luePurity.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("purity_name", "Purity Name"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("purity_id", "Purity ID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.luePurity.Properties.NullText = "";
            this.luePurity.Properties.ShowHeader = false;
            this.luePurity.Size = new System.Drawing.Size(100, 22);
            this.luePurity.TabIndex = 8;
            this.luePurity.Visible = false;
            this.luePurity.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.luePurity_ButtonClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(188, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 15);
            this.label3.TabIndex = 30;
            this.label3.Text = "Purity";
            // 
            // dtpReceiveDate
            // 
            this.dtpReceiveDate.EditValue = null;
            this.dtpReceiveDate.EnterMoveNextControl = true;
            this.dtpReceiveDate.Location = new System.Drawing.Point(84, 8);
            this.dtpReceiveDate.Name = "dtpReceiveDate";
            this.dtpReceiveDate.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtpReceiveDate.Properties.Appearance.Options.UseFont = true;
            this.dtpReceiveDate.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtpReceiveDate.Properties.AppearanceDropDown.Options.UseFont = true;
            this.dtpReceiveDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpReceiveDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpReceiveDate.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dtpReceiveDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpReceiveDate.Size = new System.Drawing.Size(122, 22);
            this.dtpReceiveDate.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(2, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Entry Date";
            // 
            // backgroundWorker_SoyebleReceive
            // 
            this.backgroundWorker_SoyebleReceive.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_SoyebleReceive_DoWork);
            this.backgroundWorker_SoyebleReceive.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_SoyebleReceive_RunWorkerCompleted);
            // 
            // FrmMFGSawableRecieve
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 495);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.panelControl2);
            this.Name = "FrmMFGSawableRecieve";
            this.Text = "Soyeble Recieve";
            this.Load += new System.EventHandler(this.FrmMFGSawableRecieve_Load);
            ((System.ComponentModel.ISupportInitialize)(this.RepRecDate.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepRecDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSawableRecieve)).EndInit();
            this.ContextMNExport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSawableRecieve)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueProcess.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueSubProcess.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWeightPlus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWeightLoss.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueKapan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIssProcess.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRRPcs.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRRCarat.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBalancePcs.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPcs.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCarat.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBalanceCarat.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.luePurityGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueCutNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.luePurity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpReceiveDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpReceiveDate.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.GridControl grdSawableRecieve;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.LookUpEdit luePurity;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraEditors.DateEdit dtpReceiveDate;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.LookUpEdit lueCutNo;
        private System.Windows.Forms.Label lblKapanNo;
        private DevExpress.XtraEditors.LookUpEdit luePurityGroup;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvSawableRecieve;
        private DevExpress.XtraGrid.Columns.GridColumn clmRecieveID;
        private DevExpress.XtraGrid.Columns.GridColumn clmIssueDate;
        private DevExpress.XtraGrid.Columns.GridColumn clmLotID;
        private DevExpress.XtraGrid.Columns.GridColumn clmCutNo;
        private DevExpress.XtraGrid.Columns.GridColumn clmPurityGroup;
        private DevExpress.XtraGrid.Columns.GridColumn clmPurityGroupID;
        private DevExpress.XtraGrid.Columns.GridColumn clmPurity;
        private DevExpress.XtraGrid.Columns.GridColumn clmPurityID;
        private DevExpress.XtraGrid.Columns.GridColumn clmPcs;
        private DevExpress.XtraGrid.Columns.GridColumn clmCarat;
        private DevExpress.XtraGrid.Columns.GridColumn clmRate;
        private DevExpress.XtraGrid.Columns.GridColumn clmAmount;
        private DevExpress.XtraEditors.TextEdit txtBalanceCarat;
        private System.Windows.Forms.Label label16;
        private DevExpress.XtraEditors.TextEdit txtLotID;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.TextEdit txtPcs;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraEditors.TextEdit txtCarat;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private DevExpress.XtraGrid.Columns.GridColumn clmSrno;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtAmount;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.TextEdit txtRate;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit RepRecDate;
        private DevExpress.XtraEditors.SimpleButton btnExport;
        private DevExpress.XtraEditors.TextEdit txtBalancePcs;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.TextEdit txtRRPcs;
        private System.Windows.Forms.Label label10;
        private DevExpress.XtraEditors.TextEdit txtRRCarat;
        private System.Windows.Forms.Label label11;
        private DevExpress.XtraGrid.Columns.GridColumn clmRRPcs;
        private DevExpress.XtraGrid.Columns.GridColumn clmRRCarat;
        private DevExpress.XtraEditors.TextEdit txtIssProcess;
        private System.Windows.Forms.Label label12;
        private System.ComponentModel.BackgroundWorker backgroundWorker_SoyebleReceive;
        private DevExpress.XtraEditors.LookUpEdit lueKapan;
        private System.Windows.Forms.Label label13;
        private DevExpress.XtraGrid.Columns.GridColumn clmCaratPlus;
        private DevExpress.XtraGrid.Columns.GridColumn clmCaratLoss;
        private DevExpress.XtraEditors.TextEdit txtWeightPlus;
        private System.Windows.Forms.Label label14;
        private DevExpress.XtraEditors.TextEdit txtWeightLoss;
        private System.Windows.Forms.Label label15;
        private DevExpress.XtraEditors.LookUpEdit lueProcess;
        private System.Windows.Forms.Label label17;
        private DevExpress.XtraEditors.LookUpEdit lueSubProcess;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ContextMenuStrip ContextMNExport;
        private System.Windows.Forms.ToolStripMenuItem MNExportExcel;
        private System.Windows.Forms.ToolStripMenuItem MNExportPDF;
        private System.Windows.Forms.ToolStripMenuItem MNExportTEXT;
        private System.Windows.Forms.ToolStripMenuItem MNExportHTML;
        private System.Windows.Forms.ToolStripMenuItem MNExportRTF;
        private System.Windows.Forms.ToolStripMenuItem MNExportCSV;
        private System.Windows.Forms.Label lblOsPcs;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lblOsCarat;
        private System.Windows.Forms.Label label20;
        private DevExpress.XtraGrid.Columns.GridColumn ClmDelete;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit RepDelete;
        private DevExpress.XtraGrid.Columns.GridColumn ClmRoughCutID;
        private DevExpress.XtraGrid.Columns.GridColumn ClmKapanID;
        private DevExpress.XtraGrid.Columns.GridColumn ClmOSPcs;
        private DevExpress.XtraGrid.Columns.GridColumn ClmOSCarat;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
    }
}