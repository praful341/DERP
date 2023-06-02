namespace DERP.Transaction.MFG
{
    partial class FrmMFGProcessWeightLossRecieve
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMFGProcessWeightLossRecieve));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.backgroundWorker_ProcRecWeightLoss = new System.ComponentModel.BackgroundWorker();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.ttlbWeightLossRecieve = new DevExpress.XtraTab.XtraTabControl();
            this.tblWeightLossdetail = new DevExpress.XtraTab.XtraTabPage();
            this.panelControl7 = new DevExpress.XtraEditors.PanelControl();
            this.panelProgress = new DevExpress.XtraEditors.PanelControl();
            this.lblProgressCount = new System.Windows.Forms.Label();
            this.SaveProgressBar = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            this.grdProcessWeightLossRecieve = new DevExpress.XtraGrid.GridControl();
            this.dgvProcessWeightLossRecieve = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.clmRecieveID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmIssueDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.RepRecDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.clmLotID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmCutNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmManager = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmManagerID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmEmployee = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmEmployeeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmProcess = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmProcessID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmSubProcess = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmSubProcessID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmReturnPcs = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmReturnCarat = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmLossPcs = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmLossCarat = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmLostPcs = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmLostCarat = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmSrno = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmIssueId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmRejectionPcs = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmRejectionCarat = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ClmRoughCutNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ClmKapanID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ClmDelete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.RepDelete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.bandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnSearchRec = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.chkWithoutLoss = new DevExpress.XtraEditors.CheckEdit();
            this.btnPopUpStock = new DevExpress.XtraEditors.SimpleButton();
            this.lblOsPcs = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txtRejPcs = new DevExpress.XtraEditors.TextEdit();
            this.label15 = new System.Windows.Forms.Label();
            this.txtRejCarat = new DevExpress.XtraEditors.TextEdit();
            this.label17 = new System.Windows.Forms.Label();
            this.lueKapan = new DevExpress.XtraEditors.LookUpEdit();
            this.label14 = new System.Windows.Forms.Label();
            this.txtReturnPcs = new DevExpress.XtraEditors.TextEdit();
            this.label10 = new System.Windows.Forms.Label();
            this.txtReturnCarat = new DevExpress.XtraEditors.TextEdit();
            this.label11 = new System.Windows.Forms.Label();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.lblOsCarat = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtLostPcs = new DevExpress.XtraEditors.TextEdit();
            this.label7 = new System.Windows.Forms.Label();
            this.txtLostCarat = new DevExpress.XtraEditors.TextEdit();
            this.label8 = new System.Windows.Forms.Label();
            this.txtLotID = new DevExpress.XtraEditors.TextEdit();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLossPcs = new DevExpress.XtraEditors.TextEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.txtLossCarat = new DevExpress.XtraEditors.TextEdit();
            this.label16 = new System.Windows.Forms.Label();
            this.lueProcess = new DevExpress.XtraEditors.LookUpEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.lueCutNo = new DevExpress.XtraEditors.LookUpEdit();
            this.lblKapanNo = new System.Windows.Forms.Label();
            this.lueSubProcess = new DevExpress.XtraEditors.LookUpEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.dtpReceiveDate = new DevExpress.XtraEditors.DateEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.tblWeightLossList = new DevExpress.XtraTab.XtraTabPage();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.grdWeightLossRec = new DevExpress.XtraGrid.GridControl();
            this.dgvWeightLossRec = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.clmRecieve_Id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmRecDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmIssue_Id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmRoughCutId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmRoughCut = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmLot_Id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmCompany = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmBranch = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmLocation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmDepartment = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmProcess_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmLProcess = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmSubProcess_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmLSubProcess = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmRecievePcs = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmRecieveCarat = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmLoss_Pcs = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmLoss_Carat = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmLost_Pcs = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmLost_Carat = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmEntryBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmEntryDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmEntryTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmIP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmEmployee_Id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmManager_Id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl5 = new DevExpress.XtraEditors.PanelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.dtpToDate = new DevExpress.XtraEditors.DateEdit();
            this.dtpFromDate = new DevExpress.XtraEditors.DateEdit();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.ContextMNExport = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MNExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportPDF = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportTEXT = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportHTML = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportRTF = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportCSV = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ttlbWeightLossRecieve)).BeginInit();
            this.ttlbWeightLossRecieve.SuspendLayout();
            this.tblWeightLossdetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl7)).BeginInit();
            this.panelControl7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelProgress)).BeginInit();
            this.panelProgress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SaveProgressBar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProcessWeightLossRecieve)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProcessWeightLossRecieve)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepRecDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepRecDate.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkWithoutLoss.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRejPcs.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRejCarat.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueKapan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReturnPcs.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReturnCarat.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLostPcs.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLostCarat.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLossPcs.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLossCarat.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueProcess.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueCutNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueSubProcess.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpReceiveDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpReceiveDate.Properties)).BeginInit();
            this.tblWeightLossList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdWeightLossRec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWeightLossRec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).BeginInit();
            this.panelControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpToDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpToDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate.Properties)).BeginInit();
            this.ContextMNExport.SuspendLayout();
            this.SuspendLayout();
            // 
            // backgroundWorker_ProcRecWeightLoss
            // 
            this.backgroundWorker_ProcRecWeightLoss.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_ProcRecWeightLoss_DoWork);
            this.backgroundWorker_ProcRecWeightLoss.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_ProcRecWeightLoss_RunWorkerCompleted);
            // 
            // gridView1
            // 
            this.gridView1.Name = "gridView1";
            // 
            // gridView2
            // 
            this.gridView2.Name = "gridView2";
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.ttlbWeightLossRecieve);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(0, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(1276, 495);
            this.panelControl3.TabIndex = 99;
            // 
            // ttlbWeightLossRecieve
            // 
            this.ttlbWeightLossRecieve.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ttlbWeightLossRecieve.Location = new System.Drawing.Point(2, 2);
            this.ttlbWeightLossRecieve.Name = "ttlbWeightLossRecieve";
            this.ttlbWeightLossRecieve.SelectedTabPage = this.tblWeightLossdetail;
            this.ttlbWeightLossRecieve.Size = new System.Drawing.Size(1272, 491);
            this.ttlbWeightLossRecieve.TabIndex = 99;
            this.ttlbWeightLossRecieve.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tblWeightLossList,
            this.tblWeightLossdetail});
            // 
            // tblWeightLossdetail
            // 
            this.tblWeightLossdetail.Controls.Add(this.panelControl7);
            this.tblWeightLossdetail.Name = "tblWeightLossdetail";
            this.tblWeightLossdetail.Size = new System.Drawing.Size(1266, 463);
            this.tblWeightLossdetail.Text = "Entry Details";
            // 
            // panelControl7
            // 
            this.panelControl7.Controls.Add(this.panelProgress);
            this.panelControl7.Controls.Add(this.grdProcessWeightLossRecieve);
            this.panelControl7.Controls.Add(this.panelControl2);
            this.panelControl7.Controls.Add(this.panelControl1);
            this.panelControl7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl7.Location = new System.Drawing.Point(0, 0);
            this.panelControl7.Name = "panelControl7";
            this.panelControl7.Size = new System.Drawing.Size(1266, 463);
            this.panelControl7.TabIndex = 4;
            // 
            // panelProgress
            // 
            this.panelProgress.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelProgress.Controls.Add(this.lblProgressCount);
            this.panelProgress.Controls.Add(this.SaveProgressBar);
            this.panelProgress.Location = new System.Drawing.Point(492, 202);
            this.panelProgress.Name = "panelProgress";
            this.panelProgress.Size = new System.Drawing.Size(283, 58);
            this.panelProgress.TabIndex = 102;
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
            // grdProcessWeightLossRecieve
            // 
            this.grdProcessWeightLossRecieve.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdProcessWeightLossRecieve.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdProcessWeightLossRecieve.Location = new System.Drawing.Point(2, 75);
            this.grdProcessWeightLossRecieve.MainView = this.dgvProcessWeightLossRecieve;
            this.grdProcessWeightLossRecieve.Name = "grdProcessWeightLossRecieve";
            this.grdProcessWeightLossRecieve.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.RepDelete});
            this.grdProcessWeightLossRecieve.Size = new System.Drawing.Size(1262, 342);
            this.grdProcessWeightLossRecieve.TabIndex = 100;
            this.grdProcessWeightLossRecieve.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvProcessWeightLossRecieve,
            this.bandedGridView1});
            // 
            // dgvProcessWeightLossRecieve
            // 
            this.dgvProcessWeightLossRecieve.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.dgvProcessWeightLossRecieve.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.dgvProcessWeightLossRecieve.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            this.dgvProcessWeightLossRecieve.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            this.dgvProcessWeightLossRecieve.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.dgvProcessWeightLossRecieve.Appearance.FooterPanel.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.dgvProcessWeightLossRecieve.Appearance.FooterPanel.Options.UseFont = true;
            this.dgvProcessWeightLossRecieve.Appearance.HeaderPanel.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.dgvProcessWeightLossRecieve.Appearance.HeaderPanel.Options.UseFont = true;
            this.dgvProcessWeightLossRecieve.Appearance.Row.Font = new System.Drawing.Font("Cambria", 9F);
            this.dgvProcessWeightLossRecieve.Appearance.Row.Options.UseFont = true;
            this.dgvProcessWeightLossRecieve.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.clmRecieveID,
            this.clmIssueDate,
            this.clmLotID,
            this.clmCutNo,
            this.clmManager,
            this.clmManagerID,
            this.clmEmployee,
            this.clmEmployeeID,
            this.clmProcess,
            this.clmProcessID,
            this.clmSubProcess,
            this.clmSubProcessID,
            this.clmReturnPcs,
            this.clmReturnCarat,
            this.clmLossPcs,
            this.clmLossCarat,
            this.clmRate,
            this.clmAmount,
            this.clmLostPcs,
            this.clmLostCarat,
            this.clmSrno,
            this.clmIssueId,
            this.clmRejectionPcs,
            this.clmRejectionCarat,
            this.ClmRoughCutNo,
            this.ClmKapanID,
            this.ClmDelete});
            this.dgvProcessWeightLossRecieve.GridControl = this.grdProcessWeightLossRecieve;
            this.dgvProcessWeightLossRecieve.Name = "dgvProcessWeightLossRecieve";
            this.dgvProcessWeightLossRecieve.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dgvProcessWeightLossRecieve.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.dgvProcessWeightLossRecieve.OptionsCustomization.AllowColumnMoving = false;
            this.dgvProcessWeightLossRecieve.OptionsCustomization.AllowQuickHideColumns = false;
            this.dgvProcessWeightLossRecieve.OptionsNavigation.EnterMoveNextColumn = true;
            this.dgvProcessWeightLossRecieve.OptionsView.ColumnAutoWidth = false;
            this.dgvProcessWeightLossRecieve.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.dgvProcessWeightLossRecieve.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.dgvProcessWeightLossRecieve.OptionsView.ShowAutoFilterRow = true;
            this.dgvProcessWeightLossRecieve.OptionsView.ShowFooter = true;
            this.dgvProcessWeightLossRecieve.OptionsView.ShowGroupPanel = false;
            this.dgvProcessWeightLossRecieve.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.dgvProcessWeightLossRecieve_RowClick);
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
            // clmLotID
            // 
            this.clmLotID.Caption = "Lot ID";
            this.clmLotID.FieldName = "lot_id";
            this.clmLotID.Name = "clmLotID";
            this.clmLotID.OptionsColumn.AllowEdit = false;
            this.clmLotID.Visible = true;
            this.clmLotID.VisibleIndex = 3;
            // 
            // clmCutNo
            // 
            this.clmCutNo.Caption = "Cut No";
            this.clmCutNo.FieldName = "rough_cut_no";
            this.clmCutNo.Name = "clmCutNo";
            this.clmCutNo.OptionsColumn.AllowEdit = false;
            this.clmCutNo.Visible = true;
            this.clmCutNo.VisibleIndex = 2;
            this.clmCutNo.Width = 49;
            // 
            // clmManager
            // 
            this.clmManager.Caption = "Manager";
            this.clmManager.FieldName = "manager";
            this.clmManager.Name = "clmManager";
            this.clmManager.OptionsColumn.AllowEdit = false;
            this.clmManager.Width = 125;
            // 
            // clmManagerID
            // 
            this.clmManagerID.Caption = "Manager ID";
            this.clmManagerID.FieldName = "manager_id";
            this.clmManagerID.Name = "clmManagerID";
            this.clmManagerID.OptionsColumn.AllowEdit = false;
            // 
            // clmEmployee
            // 
            this.clmEmployee.Caption = "Employee";
            this.clmEmployee.FieldName = "employee";
            this.clmEmployee.Name = "clmEmployee";
            this.clmEmployee.OptionsColumn.AllowEdit = false;
            this.clmEmployee.Width = 125;
            // 
            // clmEmployeeID
            // 
            this.clmEmployeeID.Caption = "Employee ID";
            this.clmEmployeeID.FieldName = "employee_id";
            this.clmEmployeeID.Name = "clmEmployeeID";
            this.clmEmployeeID.OptionsColumn.AllowEdit = false;
            // 
            // clmProcess
            // 
            this.clmProcess.Caption = "Process";
            this.clmProcess.FieldName = "process";
            this.clmProcess.Name = "clmProcess";
            this.clmProcess.OptionsColumn.AllowEdit = false;
            this.clmProcess.Width = 125;
            // 
            // clmProcessID
            // 
            this.clmProcessID.Caption = "Process ID";
            this.clmProcessID.FieldName = "process_id";
            this.clmProcessID.Name = "clmProcessID";
            this.clmProcessID.OptionsColumn.AllowEdit = false;
            // 
            // clmSubProcess
            // 
            this.clmSubProcess.Caption = "Sub Process";
            this.clmSubProcess.FieldName = "subprocess";
            this.clmSubProcess.Name = "clmSubProcess";
            this.clmSubProcess.OptionsColumn.AllowEdit = false;
            this.clmSubProcess.Width = 125;
            // 
            // clmSubProcessID
            // 
            this.clmSubProcessID.Caption = "SubProcess ID";
            this.clmSubProcessID.FieldName = "sub_process_id";
            this.clmSubProcessID.Name = "clmSubProcessID";
            this.clmSubProcessID.OptionsColumn.AllowEdit = false;
            // 
            // clmReturnPcs
            // 
            this.clmReturnPcs.Caption = "Return Pcs";
            this.clmReturnPcs.FieldName = "return_pcs";
            this.clmReturnPcs.Name = "clmReturnPcs";
            this.clmReturnPcs.OptionsColumn.AllowEdit = false;
            this.clmReturnPcs.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.clmReturnPcs.Visible = true;
            this.clmReturnPcs.VisibleIndex = 8;
            this.clmReturnPcs.Width = 48;
            // 
            // clmReturnCarat
            // 
            this.clmReturnCarat.Caption = "Return Carat";
            this.clmReturnCarat.FieldName = "return_carat";
            this.clmReturnCarat.Name = "clmReturnCarat";
            this.clmReturnCarat.OptionsColumn.AllowEdit = false;
            this.clmReturnCarat.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.clmReturnCarat.Visible = true;
            this.clmReturnCarat.VisibleIndex = 9;
            // 
            // clmLossPcs
            // 
            this.clmLossPcs.Caption = "Loss Pcs";
            this.clmLossPcs.FieldName = "loss_pcs";
            this.clmLossPcs.Name = "clmLossPcs";
            this.clmLossPcs.OptionsColumn.AllowEdit = false;
            this.clmLossPcs.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.clmLossPcs.Visible = true;
            this.clmLossPcs.VisibleIndex = 4;
            this.clmLossPcs.Width = 61;
            // 
            // clmLossCarat
            // 
            this.clmLossCarat.Caption = "Loss Carat";
            this.clmLossCarat.FieldName = "loss_carat";
            this.clmLossCarat.Name = "clmLossCarat";
            this.clmLossCarat.OptionsColumn.AllowEdit = false;
            this.clmLossCarat.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.clmLossCarat.Visible = true;
            this.clmLossCarat.VisibleIndex = 5;
            // 
            // clmRate
            // 
            this.clmRate.Caption = "Rate";
            this.clmRate.FieldName = "rate";
            this.clmRate.Name = "clmRate";
            this.clmRate.OptionsColumn.AllowEdit = false;
            // 
            // clmAmount
            // 
            this.clmAmount.Caption = "Amount";
            this.clmAmount.FieldName = "amount";
            this.clmAmount.Name = "clmAmount";
            this.clmAmount.OptionsColumn.AllowEdit = false;
            // 
            // clmLostPcs
            // 
            this.clmLostPcs.Caption = "Lost Pcs";
            this.clmLostPcs.FieldName = "lost_pcs";
            this.clmLostPcs.Name = "clmLostPcs";
            this.clmLostPcs.OptionsColumn.AllowEdit = false;
            this.clmLostPcs.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.clmLostPcs.Visible = true;
            this.clmLostPcs.VisibleIndex = 6;
            this.clmLostPcs.Width = 42;
            // 
            // clmLostCarat
            // 
            this.clmLostCarat.Caption = "Lost Carat";
            this.clmLostCarat.FieldName = "lost_carat";
            this.clmLostCarat.Name = "clmLostCarat";
            this.clmLostCarat.OptionsColumn.AllowEdit = false;
            this.clmLostCarat.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.clmLostCarat.Visible = true;
            this.clmLostCarat.VisibleIndex = 7;
            // 
            // clmSrno
            // 
            this.clmSrno.Caption = "SrNo";
            this.clmSrno.FieldName = "sr_no";
            this.clmSrno.Name = "clmSrno";
            this.clmSrno.OptionsColumn.AllowEdit = false;
            // 
            // clmIssueId
            // 
            this.clmIssueId.Caption = "Issue Id";
            this.clmIssueId.FieldName = "issue_id";
            this.clmIssueId.Name = "clmIssueId";
            this.clmIssueId.OptionsColumn.AllowEdit = false;
            // 
            // clmRejectionPcs
            // 
            this.clmRejectionPcs.Caption = "Rejection Pcs";
            this.clmRejectionPcs.FieldName = "rejection_pcs";
            this.clmRejectionPcs.Name = "clmRejectionPcs";
            this.clmRejectionPcs.OptionsColumn.AllowEdit = false;
            this.clmRejectionPcs.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.clmRejectionPcs.Visible = true;
            this.clmRejectionPcs.VisibleIndex = 10;
            // 
            // clmRejectionCarat
            // 
            this.clmRejectionCarat.Caption = "Rejection Carat";
            this.clmRejectionCarat.FieldName = "rejection_carat";
            this.clmRejectionCarat.Name = "clmRejectionCarat";
            this.clmRejectionCarat.OptionsColumn.AllowEdit = false;
            this.clmRejectionCarat.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.clmRejectionCarat.Visible = true;
            this.clmRejectionCarat.VisibleIndex = 11;
            // 
            // ClmRoughCutNo
            // 
            this.ClmRoughCutNo.Caption = "Rough Cut ID";
            this.ClmRoughCutNo.FieldName = "rough_cut_id";
            this.ClmRoughCutNo.Name = "ClmRoughCutNo";
            this.ClmRoughCutNo.OptionsColumn.AllowEdit = false;
            // 
            // ClmKapanID
            // 
            this.ClmKapanID.Caption = "Kapan ID";
            this.ClmKapanID.FieldName = "kapan_id";
            this.ClmKapanID.Name = "ClmKapanID";
            // 
            // ClmDelete
            // 
            this.ClmDelete.Caption = "Del";
            this.ClmDelete.ColumnEdit = this.RepDelete;
            this.ClmDelete.FieldName = "delete";
            this.ClmDelete.Name = "ClmDelete";
            this.ClmDelete.Visible = true;
            this.ClmDelete.VisibleIndex = 0;
            this.ClmDelete.Width = 32;
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
            // bandedGridView1
            // 
            this.bandedGridView1.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1});
            this.bandedGridView1.GridControl = this.grdProcessWeightLossRecieve;
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
            this.panelControl2.Controls.Add(this.btnSearchRec);
            this.panelControl2.Controls.Add(this.btnDelete);
            this.panelControl2.Controls.Add(this.btnExport);
            this.panelControl2.Controls.Add(this.btnExit);
            this.panelControl2.Controls.Add(this.btnClear);
            this.panelControl2.Controls.Add(this.btnSave);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(2, 417);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1262, 44);
            this.panelControl2.TabIndex = 101;
            // 
            // btnSearchRec
            // 
            this.btnSearchRec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchRec.Appearance.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearchRec.Appearance.Options.UseFont = true;
            this.btnSearchRec.ImageOptions.Image = global::DERP.Properties.Resources.Search;
            this.btnSearchRec.Location = new System.Drawing.Point(700, 6);
            this.btnSearchRec.Name = "btnSearchRec";
            this.btnSearchRec.Size = new System.Drawing.Size(112, 32);
            this.btnSearchRec.TabIndex = 554;
            this.btnSearchRec.Text = "Search";
            this.btnSearchRec.Click += new System.EventHandler(this.btnSearchRec_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Appearance.Options.UseFont = true;
            this.btnDelete.ImageOptions.Image = global::DERP.Properties.Resources.Close;
            this.btnDelete.Location = new System.Drawing.Point(546, 5);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(102, 32);
            this.btnDelete.TabIndex = 35;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.Visible = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Appearance.Options.UseFont = true;
            this.btnExport.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.ImageOptions.Image")));
            this.btnExport.Location = new System.Drawing.Point(928, 6);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(102, 32);
            this.btnExport.TabIndex = 1;
            this.btnExport.Text = "&Export";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Appearance.Options.UseFont = true;
            this.btnExit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.ImageOptions.Image")));
            this.btnExit.Location = new System.Drawing.Point(1144, 6);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(102, 32);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "E&xit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Appearance.Options.UseFont = true;
            this.btnClear.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.ImageOptions.Image")));
            this.btnClear.Location = new System.Drawing.Point(1036, 6);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(102, 32);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "&Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.ImageOptions.Image")));
            this.btnSave.Location = new System.Drawing.Point(821, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(102, 32);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.chkWithoutLoss);
            this.panelControl1.Controls.Add(this.btnPopUpStock);
            this.panelControl1.Controls.Add(this.lblOsPcs);
            this.panelControl1.Controls.Add(this.label19);
            this.panelControl1.Controls.Add(this.txtRejPcs);
            this.panelControl1.Controls.Add(this.label15);
            this.panelControl1.Controls.Add(this.txtRejCarat);
            this.panelControl1.Controls.Add(this.label17);
            this.panelControl1.Controls.Add(this.lueKapan);
            this.panelControl1.Controls.Add(this.label14);
            this.panelControl1.Controls.Add(this.txtReturnPcs);
            this.panelControl1.Controls.Add(this.label10);
            this.panelControl1.Controls.Add(this.txtReturnCarat);
            this.panelControl1.Controls.Add(this.label11);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.lblOsCarat);
            this.panelControl1.Controls.Add(this.label9);
            this.panelControl1.Controls.Add(this.txtLostPcs);
            this.panelControl1.Controls.Add(this.label7);
            this.panelControl1.Controls.Add(this.txtLostCarat);
            this.panelControl1.Controls.Add(this.label8);
            this.panelControl1.Controls.Add(this.txtLotID);
            this.panelControl1.Controls.Add(this.label6);
            this.panelControl1.Controls.Add(this.txtLossPcs);
            this.panelControl1.Controls.Add(this.label5);
            this.panelControl1.Controls.Add(this.txtLossCarat);
            this.panelControl1.Controls.Add(this.label16);
            this.panelControl1.Controls.Add(this.lueProcess);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.lueCutNo);
            this.panelControl1.Controls.Add(this.lblKapanNo);
            this.panelControl1.Controls.Add(this.lueSubProcess);
            this.panelControl1.Controls.Add(this.label3);
            this.panelControl1.Controls.Add(this.btnAdd);
            this.panelControl1.Controls.Add(this.dtpReceiveDate);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(2, 2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1262, 73);
            this.panelControl1.TabIndex = 99;
            // 
            // chkWithoutLoss
            // 
            this.chkWithoutLoss.EnterMoveNextControl = true;
            this.chkWithoutLoss.Location = new System.Drawing.Point(1163, 44);
            this.chkWithoutLoss.Name = "chkWithoutLoss";
            this.chkWithoutLoss.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkWithoutLoss.Properties.Appearance.Options.UseFont = true;
            this.chkWithoutLoss.Properties.Caption = "Without Loss";
            this.chkWithoutLoss.Size = new System.Drawing.Size(103, 19);
            this.chkWithoutLoss.TabIndex = 589;
            // 
            // btnPopUpStock
            // 
            this.btnPopUpStock.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPopUpStock.Appearance.Options.UseFont = true;
            this.btnPopUpStock.Enabled = false;
            this.btnPopUpStock.Location = new System.Drawing.Point(629, 12);
            this.btnPopUpStock.Name = "btnPopUpStock";
            this.btnPopUpStock.Size = new System.Drawing.Size(22, 19);
            this.btnPopUpStock.TabIndex = 588;
            this.btnPopUpStock.Text = "...";
            this.btnPopUpStock.Click += new System.EventHandler(this.btnPopUpStock_Click);
            // 
            // lblOsPcs
            // 
            this.lblOsPcs.AutoSize = true;
            this.lblOsPcs.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblOsPcs.ForeColor = System.Drawing.Color.Red;
            this.lblOsPcs.Location = new System.Drawing.Point(1118, 16);
            this.lblOsPcs.Name = "lblOsPcs";
            this.lblOsPcs.Size = new System.Drawing.Size(15, 15);
            this.lblOsPcs.TabIndex = 578;
            this.lblOsPcs.Text = "0";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.label19.Location = new System.Drawing.Point(1061, 16);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(57, 15);
            this.label19.TabIndex = 577;
            this.label19.Text = "O/S Pcs:";
            // 
            // txtRejPcs
            // 
            this.txtRejPcs.EnterMoveNextControl = true;
            this.txtRejPcs.Location = new System.Drawing.Point(847, 45);
            this.txtRejPcs.Name = "txtRejPcs";
            this.txtRejPcs.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRejPcs.Properties.Appearance.Options.UseFont = true;
            this.txtRejPcs.Size = new System.Drawing.Size(54, 22);
            this.txtRejPcs.TabIndex = 12;
            this.txtRejPcs.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRejPcs_KeyPress);
            this.txtRejPcs.Validated += new System.EventHandler(this.txtRejPcs_Validated);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.label15.Location = new System.Drawing.Point(762, 47);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(87, 15);
            this.label15.TabIndex = 575;
            this.label15.Text = "Rejection Pcs";
            // 
            // txtRejCarat
            // 
            this.txtRejCarat.EnterMoveNextControl = true;
            this.txtRejCarat.Location = new System.Drawing.Point(1003, 45);
            this.txtRejCarat.Name = "txtRejCarat";
            this.txtRejCarat.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRejCarat.Properties.Appearance.Options.UseFont = true;
            this.txtRejCarat.Size = new System.Drawing.Size(54, 22);
            this.txtRejCarat.TabIndex = 13;
            this.txtRejCarat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRejCarat_KeyPress);
            this.txtRejCarat.Validated += new System.EventHandler(this.txtRejCarat_Validated);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.label17.Location = new System.Drawing.Point(903, 47);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(99, 15);
            this.label17.TabIndex = 576;
            this.label17.Text = "Rejection Carat";
            // 
            // lueKapan
            // 
            this.lueKapan.EnterMoveNextControl = true;
            this.lueKapan.Location = new System.Drawing.Point(243, 12);
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
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.label14.Location = new System.Drawing.Point(175, 14);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 15);
            this.label14.TabIndex = 571;
            this.label14.Text = "Kapan No";
            // 
            // txtReturnPcs
            // 
            this.txtReturnPcs.EnterMoveNextControl = true;
            this.txtReturnPcs.Location = new System.Drawing.Point(80, 45);
            this.txtReturnPcs.Name = "txtReturnPcs";
            this.txtReturnPcs.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReturnPcs.Properties.Appearance.Options.UseFont = true;
            this.txtReturnPcs.Size = new System.Drawing.Size(54, 22);
            this.txtReturnPcs.TabIndex = 6;
            this.txtReturnPcs.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtReturnPcs_KeyPress);
            this.txtReturnPcs.Validated += new System.EventHandler(this.txtReturnPcs_Validated);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(5, 47);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 15);
            this.label10.TabIndex = 568;
            this.label10.Text = "Return  Pcs";
            // 
            // txtReturnCarat
            // 
            this.txtReturnCarat.EnterMoveNextControl = true;
            this.txtReturnCarat.Location = new System.Drawing.Point(228, 45);
            this.txtReturnCarat.Name = "txtReturnCarat";
            this.txtReturnCarat.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReturnCarat.Properties.Appearance.Options.UseFont = true;
            this.txtReturnCarat.Size = new System.Drawing.Size(54, 22);
            this.txtReturnCarat.TabIndex = 7;
            this.txtReturnCarat.EditValueChanged += new System.EventHandler(this.txtReturnCarat_EditValueChanged);
            this.txtReturnCarat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtReturnCarat_KeyPress);
            this.txtReturnCarat.Validated += new System.EventHandler(this.txtReturnCarat_Validated);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(140, 47);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(88, 15);
            this.label11.TabIndex = 570;
            this.label11.Text = "Return  Carat";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl2.Appearance.Options.UseBackColor = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Location = new System.Drawing.Point(530, 12);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(9, 13);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "* ";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl1.Appearance.Options.UseBackColor = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(390, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(9, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "* ";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl3.Appearance.Options.UseBackColor = true;
            this.labelControl3.Appearance.Options.UseForeColor = true;
            this.labelControl3.Location = new System.Drawing.Point(42, 12);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(9, 13);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "* ";
            // 
            // lblOsCarat
            // 
            this.lblOsCarat.AutoSize = true;
            this.lblOsCarat.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblOsCarat.ForeColor = System.Drawing.Color.Red;
            this.lblOsCarat.Location = new System.Drawing.Point(1213, 16);
            this.lblOsCarat.Name = "lblOsCarat";
            this.lblOsCarat.Size = new System.Drawing.Size(15, 15);
            this.lblOsCarat.TabIndex = 563;
            this.lblOsCarat.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(1156, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 15);
            this.label9.TabIndex = 562;
            this.label9.Text = "O/S Crt:";
            // 
            // txtLostPcs
            // 
            this.txtLostPcs.EnterMoveNextControl = true;
            this.txtLostPcs.Location = new System.Drawing.Point(579, 45);
            this.txtLostPcs.Name = "txtLostPcs";
            this.txtLostPcs.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLostPcs.Properties.Appearance.Options.UseFont = true;
            this.txtLostPcs.Size = new System.Drawing.Size(54, 22);
            this.txtLostPcs.TabIndex = 10;
            this.txtLostPcs.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLostPcs_KeyPress);
            this.txtLostPcs.Validated += new System.EventHandler(this.txtLostPcs_Validated);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(525, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 15);
            this.label7.TabIndex = 559;
            this.label7.Text = "Lost Pcs";
            // 
            // txtLostCarat
            // 
            this.txtLostCarat.EnterMoveNextControl = true;
            this.txtLostCarat.Location = new System.Drawing.Point(705, 45);
            this.txtLostCarat.Name = "txtLostCarat";
            this.txtLostCarat.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLostCarat.Properties.Appearance.Options.UseFont = true;
            this.txtLostCarat.Size = new System.Drawing.Size(54, 22);
            this.txtLostCarat.TabIndex = 11;
            this.txtLostCarat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLostCarat_KeyPress);
            this.txtLostCarat.Validated += new System.EventHandler(this.txtLostCarat_Validated);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(639, 47);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 15);
            this.label8.TabIndex = 561;
            this.label8.Text = "Lost Carat";
            // 
            // txtLotID
            // 
            this.txtLotID.EnterMoveNextControl = true;
            this.txtLotID.Location = new System.Drawing.Point(542, 12);
            this.txtLotID.Name = "txtLotID";
            this.txtLotID.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLotID.Properties.Appearance.Options.UseFont = true;
            this.txtLotID.Size = new System.Drawing.Size(81, 22);
            this.txtLotID.TabIndex = 3;
            this.txtLotID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLotID_KeyPress);
            this.txtLotID.Validated += new System.EventHandler(this.txtLotID_Validated);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(503, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 15);
            this.label6.TabIndex = 556;
            this.label6.Text = "Lot";
            // 
            // txtLossPcs
            // 
            this.txtLossPcs.EnterMoveNextControl = true;
            this.txtLossPcs.Location = new System.Drawing.Point(339, 45);
            this.txtLossPcs.Name = "txtLossPcs";
            this.txtLossPcs.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLossPcs.Properties.Appearance.Options.UseFont = true;
            this.txtLossPcs.Size = new System.Drawing.Size(54, 22);
            this.txtLossPcs.TabIndex = 8;
            this.txtLossPcs.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLossPcs_KeyPress);
            this.txtLossPcs.Validated += new System.EventHandler(this.txtLossPcs_Validated);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(283, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 15);
            this.label5.TabIndex = 551;
            this.label5.Text = "Loss Pcs";
            // 
            // txtLossCarat
            // 
            this.txtLossCarat.EnterMoveNextControl = true;
            this.txtLossCarat.Location = new System.Drawing.Point(465, 45);
            this.txtLossCarat.Name = "txtLossCarat";
            this.txtLossCarat.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLossCarat.Properties.Appearance.Options.UseFont = true;
            this.txtLossCarat.Size = new System.Drawing.Size(54, 22);
            this.txtLossCarat.TabIndex = 9;
            this.txtLossCarat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLossCarat_KeyPress);
            this.txtLossCarat.Validated += new System.EventHandler(this.txtLossCarat_Validated);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.label16.Location = new System.Drawing.Point(398, 47);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(68, 15);
            this.label16.TabIndex = 555;
            this.label16.Text = "Loss Carat";
            // 
            // lueProcess
            // 
            this.lueProcess.EnterMoveNextControl = true;
            this.lueProcess.Location = new System.Drawing.Point(711, 12);
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
            this.lueProcess.Size = new System.Drawing.Size(128, 22);
            this.lueProcess.TabIndex = 4;
            this.lueProcess.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.lueProcess_ButtonClick);
            this.lueProcess.EditValueChanged += new System.EventHandler(this.lueProcess_EditValueChanged_2);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(658, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 15);
            this.label2.TabIndex = 548;
            this.label2.Text = "Process";
            // 
            // lueCutNo
            // 
            this.lueCutNo.EnterMoveNextControl = true;
            this.lueCutNo.Location = new System.Drawing.Point(401, 12);
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
            this.lueCutNo.Size = new System.Drawing.Size(100, 22);
            this.lueCutNo.TabIndex = 2;
            this.lueCutNo.EditValueChanged += new System.EventHandler(this.lueCutNo_EditValueChanged_1);
            // 
            // lblKapanNo
            // 
            this.lblKapanNo.AutoSize = true;
            this.lblKapanNo.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblKapanNo.Location = new System.Drawing.Point(345, 16);
            this.lblKapanNo.Name = "lblKapanNo";
            this.lblKapanNo.Size = new System.Drawing.Size(46, 15);
            this.lblKapanNo.TabIndex = 545;
            this.lblKapanNo.Text = "Cut No";
            // 
            // lueSubProcess
            // 
            this.lueSubProcess.EnterMoveNextControl = true;
            this.lueSubProcess.Location = new System.Drawing.Point(923, 12);
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
            this.lueSubProcess.Size = new System.Drawing.Size(134, 22);
            this.lueSubProcess.TabIndex = 5;
            this.lueSubProcess.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.lueSubProcess_ButtonClick);
            this.lueSubProcess.EditValueChanged += new System.EventHandler(this.lueSubProcess_EditValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(838, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 15);
            this.label3.TabIndex = 30;
            this.label3.Text = "Sub Process";
            // 
            // btnAdd
            // 
            this.btnAdd.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Appearance.Options.UseFont = true;
            this.btnAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.ImageOptions.Image")));
            this.btnAdd.Location = new System.Drawing.Point(1060, 36);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 32);
            this.btnAdd.TabIndex = 14;
            this.btnAdd.Text = "&Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // dtpReceiveDate
            // 
            this.dtpReceiveDate.EditValue = null;
            this.dtpReceiveDate.EnterMoveNextControl = true;
            this.dtpReceiveDate.Location = new System.Drawing.Point(54, 12);
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
            this.dtpReceiveDate.Size = new System.Drawing.Size(121, 22);
            this.dtpReceiveDate.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(5, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Date";
            // 
            // tblWeightLossList
            // 
            this.tblWeightLossList.Controls.Add(this.panelControl4);
            this.tblWeightLossList.Controls.Add(this.panelControl5);
            this.tblWeightLossList.Name = "tblWeightLossList";
            this.tblWeightLossList.PageVisible = false;
            this.tblWeightLossList.Size = new System.Drawing.Size(1266, 463);
            this.tblWeightLossList.Text = "List";
            // 
            // panelControl4
            // 
            this.panelControl4.Controls.Add(this.grdWeightLossRec);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl4.Location = new System.Drawing.Point(0, 43);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(1266, 420);
            this.panelControl4.TabIndex = 3;
            // 
            // grdWeightLossRec
            // 
            this.grdWeightLossRec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdWeightLossRec.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdWeightLossRec.Location = new System.Drawing.Point(2, 2);
            this.grdWeightLossRec.MainView = this.dgvWeightLossRec;
            this.grdWeightLossRec.Name = "grdWeightLossRec";
            this.grdWeightLossRec.Size = new System.Drawing.Size(1262, 416);
            this.grdWeightLossRec.TabIndex = 18;
            this.grdWeightLossRec.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvWeightLossRec});
            // 
            // dgvWeightLossRec
            // 
            this.dgvWeightLossRec.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.dgvWeightLossRec.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.dgvWeightLossRec.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            this.dgvWeightLossRec.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            this.dgvWeightLossRec.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.dgvWeightLossRec.Appearance.FooterPanel.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.dgvWeightLossRec.Appearance.FooterPanel.Options.UseFont = true;
            this.dgvWeightLossRec.Appearance.HeaderPanel.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.dgvWeightLossRec.Appearance.HeaderPanel.Options.UseFont = true;
            this.dgvWeightLossRec.Appearance.Row.Font = new System.Drawing.Font("Cambria", 9F);
            this.dgvWeightLossRec.Appearance.Row.Options.UseFont = true;
            this.dgvWeightLossRec.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.clmRecieve_Id,
            this.clmRecDate,
            this.clmIssue_Id,
            this.clmRoughCutId,
            this.clmRoughCut,
            this.clmLot_Id,
            this.clmCompany,
            this.clmBranch,
            this.clmLocation,
            this.clmDepartment,
            this.clmProcess_ID,
            this.clmLProcess,
            this.clmSubProcess_ID,
            this.clmLSubProcess,
            this.clmRecievePcs,
            this.clmRecieveCarat,
            this.clmLoss_Pcs,
            this.clmLoss_Carat,
            this.clmLost_Pcs,
            this.clmLost_Carat,
            this.clmEntryBy,
            this.clmEntryDate,
            this.clmEntryTime,
            this.clmIP,
            this.clmEmployee_Id,
            this.clmManager_Id});
            this.dgvWeightLossRec.GridControl = this.grdWeightLossRec;
            this.dgvWeightLossRec.Name = "dgvWeightLossRec";
            this.dgvWeightLossRec.OptionsBehavior.Editable = false;
            this.dgvWeightLossRec.OptionsBehavior.ReadOnly = true;
            this.dgvWeightLossRec.OptionsCustomization.AllowQuickHideColumns = false;
            this.dgvWeightLossRec.OptionsView.ColumnAutoWidth = false;
            this.dgvWeightLossRec.OptionsView.RowAutoHeight = true;
            this.dgvWeightLossRec.OptionsView.ShowAutoFilterRow = true;
            this.dgvWeightLossRec.OptionsView.ShowFooter = true;
            this.dgvWeightLossRec.OptionsView.ShowGroupPanel = false;
            this.dgvWeightLossRec.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.dgvWeightLossRec_RowClick);
            // 
            // clmRecieve_Id
            // 
            this.clmRecieve_Id.Caption = "Recieve Id";
            this.clmRecieve_Id.FieldName = "receive_id";
            this.clmRecieve_Id.Name = "clmRecieve_Id";
            this.clmRecieve_Id.OptionsColumn.AllowEdit = false;
            // 
            // clmRecDate
            // 
            this.clmRecDate.Caption = "Receive Date";
            this.clmRecDate.FieldName = "receive_date";
            this.clmRecDate.Name = "clmRecDate";
            this.clmRecDate.OptionsColumn.AllowEdit = false;
            this.clmRecDate.Visible = true;
            this.clmRecDate.VisibleIndex = 0;
            this.clmRecDate.Width = 83;
            // 
            // clmIssue_Id
            // 
            this.clmIssue_Id.Caption = "Issue Id";
            this.clmIssue_Id.FieldName = "issue_id";
            this.clmIssue_Id.Name = "clmIssue_Id";
            this.clmIssue_Id.OptionsColumn.AllowEdit = false;
            // 
            // clmRoughCutId
            // 
            this.clmRoughCutId.Caption = "Rough Cut ID";
            this.clmRoughCutId.FieldName = "rough_cut_id";
            this.clmRoughCutId.Name = "clmRoughCutId";
            this.clmRoughCutId.OptionsColumn.AllowEdit = false;
            // 
            // clmRoughCut
            // 
            this.clmRoughCut.Caption = "Rough Cut";
            this.clmRoughCut.FieldName = "rough_cut_no";
            this.clmRoughCut.Name = "clmRoughCut";
            this.clmRoughCut.OptionsColumn.AllowEdit = false;
            this.clmRoughCut.Visible = true;
            this.clmRoughCut.VisibleIndex = 1;
            // 
            // clmLot_Id
            // 
            this.clmLot_Id.Caption = "Lot Id";
            this.clmLot_Id.FieldName = "lot_id";
            this.clmLot_Id.Name = "clmLot_Id";
            this.clmLot_Id.OptionsColumn.AllowEdit = false;
            this.clmLot_Id.Visible = true;
            this.clmLot_Id.VisibleIndex = 2;
            // 
            // clmCompany
            // 
            this.clmCompany.Caption = "Company";
            this.clmCompany.FieldName = "company_name";
            this.clmCompany.Name = "clmCompany";
            this.clmCompany.OptionsColumn.AllowEdit = false;
            // 
            // clmBranch
            // 
            this.clmBranch.Caption = "Branch";
            this.clmBranch.FieldName = "branch_name";
            this.clmBranch.Name = "clmBranch";
            this.clmBranch.OptionsColumn.AllowEdit = false;
            // 
            // clmLocation
            // 
            this.clmLocation.Caption = "Location";
            this.clmLocation.FieldName = "location_name";
            this.clmLocation.Name = "clmLocation";
            this.clmLocation.OptionsColumn.AllowEdit = false;
            // 
            // clmDepartment
            // 
            this.clmDepartment.Caption = "Department";
            this.clmDepartment.FieldName = "department_name";
            this.clmDepartment.Name = "clmDepartment";
            this.clmDepartment.OptionsColumn.AllowEdit = false;
            // 
            // clmProcess_ID
            // 
            this.clmProcess_ID.Caption = "Process ID";
            this.clmProcess_ID.FieldName = "process_id";
            this.clmProcess_ID.Name = "clmProcess_ID";
            this.clmProcess_ID.OptionsColumn.AllowEdit = false;
            // 
            // clmLProcess
            // 
            this.clmLProcess.Caption = "Process Name";
            this.clmLProcess.FieldName = "process_name";
            this.clmLProcess.Name = "clmLProcess";
            this.clmLProcess.Visible = true;
            this.clmLProcess.VisibleIndex = 3;
            // 
            // clmSubProcess_ID
            // 
            this.clmSubProcess_ID.Caption = "SubProcessID";
            this.clmSubProcess_ID.FieldName = "sub_process_id";
            this.clmSubProcess_ID.Name = "clmSubProcess_ID";
            this.clmSubProcess_ID.OptionsColumn.AllowEdit = false;
            // 
            // clmLSubProcess
            // 
            this.clmLSubProcess.Caption = "Sub Process";
            this.clmLSubProcess.FieldName = "sub_process_name";
            this.clmLSubProcess.Name = "clmLSubProcess";
            this.clmLSubProcess.Visible = true;
            this.clmLSubProcess.VisibleIndex = 4;
            // 
            // clmRecievePcs
            // 
            this.clmRecievePcs.Caption = "Recieve Pcs";
            this.clmRecievePcs.FieldName = "pcs";
            this.clmRecievePcs.Name = "clmRecievePcs";
            this.clmRecievePcs.OptionsColumn.AllowEdit = false;
            this.clmRecievePcs.Visible = true;
            this.clmRecievePcs.VisibleIndex = 5;
            // 
            // clmRecieveCarat
            // 
            this.clmRecieveCarat.Caption = "Recieve Carat";
            this.clmRecieveCarat.FieldName = "carat";
            this.clmRecieveCarat.Name = "clmRecieveCarat";
            this.clmRecieveCarat.OptionsColumn.AllowEdit = false;
            this.clmRecieveCarat.Visible = true;
            this.clmRecieveCarat.VisibleIndex = 6;
            // 
            // clmLoss_Pcs
            // 
            this.clmLoss_Pcs.Caption = "Loss Pcs";
            this.clmLoss_Pcs.FieldName = "loss_pcs";
            this.clmLoss_Pcs.Name = "clmLoss_Pcs";
            this.clmLoss_Pcs.OptionsColumn.AllowEdit = false;
            this.clmLoss_Pcs.Visible = true;
            this.clmLoss_Pcs.VisibleIndex = 7;
            // 
            // clmLoss_Carat
            // 
            this.clmLoss_Carat.Caption = "Loss Carat";
            this.clmLoss_Carat.FieldName = "loss_carat";
            this.clmLoss_Carat.Name = "clmLoss_Carat";
            this.clmLoss_Carat.OptionsColumn.AllowEdit = false;
            this.clmLoss_Carat.Visible = true;
            this.clmLoss_Carat.VisibleIndex = 8;
            // 
            // clmLost_Pcs
            // 
            this.clmLost_Pcs.Caption = "Lost Pcs";
            this.clmLost_Pcs.FieldName = "lost_pcs";
            this.clmLost_Pcs.Name = "clmLost_Pcs";
            this.clmLost_Pcs.OptionsColumn.AllowEdit = false;
            this.clmLost_Pcs.Visible = true;
            this.clmLost_Pcs.VisibleIndex = 9;
            // 
            // clmLost_Carat
            // 
            this.clmLost_Carat.Caption = "Lost Carat";
            this.clmLost_Carat.FieldName = "lost_carat";
            this.clmLost_Carat.Name = "clmLost_Carat";
            this.clmLost_Carat.OptionsColumn.AllowEdit = false;
            this.clmLost_Carat.Visible = true;
            this.clmLost_Carat.VisibleIndex = 10;
            // 
            // clmEntryBy
            // 
            this.clmEntryBy.Caption = "Entry By";
            this.clmEntryBy.FieldName = "entry_by";
            this.clmEntryBy.Name = "clmEntryBy";
            this.clmEntryBy.Width = 56;
            // 
            // clmEntryDate
            // 
            this.clmEntryDate.Caption = "Entry Date";
            this.clmEntryDate.FieldName = "entry_date";
            this.clmEntryDate.Name = "clmEntryDate";
            // 
            // clmEntryTime
            // 
            this.clmEntryTime.Caption = "Entry Time";
            this.clmEntryTime.FieldName = "entry_time";
            this.clmEntryTime.Name = "clmEntryTime";
            // 
            // clmIP
            // 
            this.clmIP.Caption = "Entry IP";
            this.clmIP.FieldName = "ip_address";
            this.clmIP.Name = "clmIP";
            this.clmIP.Width = 85;
            // 
            // clmEmployee_Id
            // 
            this.clmEmployee_Id.Caption = "Employee_id";
            this.clmEmployee_Id.FieldName = "employee_id";
            this.clmEmployee_Id.Name = "clmEmployee_Id";
            // 
            // clmManager_Id
            // 
            this.clmManager_Id.Caption = "Manager Id";
            this.clmManager_Id.FieldName = "manager_id";
            this.clmManager_Id.Name = "clmManager_Id";
            // 
            // panelControl5
            // 
            this.panelControl5.Controls.Add(this.btnSearch);
            this.panelControl5.Controls.Add(this.dtpToDate);
            this.panelControl5.Controls.Add(this.dtpFromDate);
            this.panelControl5.Controls.Add(this.label12);
            this.panelControl5.Controls.Add(this.label13);
            this.panelControl5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl5.Location = new System.Drawing.Point(0, 0);
            this.panelControl5.Name = "panelControl5";
            this.panelControl5.Size = new System.Drawing.Size(1266, 43);
            this.panelControl5.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Appearance.Options.UseFont = true;
            this.btnSearch.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.ImageOptions.Image")));
            this.btnSearch.Location = new System.Drawing.Point(360, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(102, 32);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "S&earch";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click_1);
            // 
            // dtpToDate
            // 
            this.dtpToDate.EditValue = null;
            this.dtpToDate.EnterMoveNextControl = true;
            this.dtpToDate.Location = new System.Drawing.Point(245, 11);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpToDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpToDate.Size = new System.Drawing.Size(100, 20);
            this.dtpToDate.TabIndex = 1;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.EditValue = null;
            this.dtpFromDate.EnterMoveNextControl = true;
            this.dtpFromDate.Location = new System.Drawing.Point(78, 11);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpFromDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpFromDate.Size = new System.Drawing.Size(100, 20);
            this.dtpFromDate.TabIndex = 0;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(184, 13);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(55, 13);
            this.label12.TabIndex = 1;
            this.label12.Text = "To Date : ";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(5, 14);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(67, 13);
            this.label13.TabIndex = 0;
            this.label13.Text = "From Date : ";
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
            // 
            // MNExportPDF
            // 
            this.MNExportPDF.Name = "MNExportPDF";
            this.MNExportPDF.Size = new System.Drawing.Size(129, 22);
            this.MNExportPDF.Text = "To PDF";
            // 
            // MNExportTEXT
            // 
            this.MNExportTEXT.Name = "MNExportTEXT";
            this.MNExportTEXT.Size = new System.Drawing.Size(129, 22);
            this.MNExportTEXT.Text = "To TEXT";
            // 
            // MNExportHTML
            // 
            this.MNExportHTML.Name = "MNExportHTML";
            this.MNExportHTML.Size = new System.Drawing.Size(129, 22);
            this.MNExportHTML.Text = "To HTML";
            // 
            // MNExportRTF
            // 
            this.MNExportRTF.Name = "MNExportRTF";
            this.MNExportRTF.Size = new System.Drawing.Size(129, 22);
            this.MNExportRTF.Text = "To RTF";
            // 
            // MNExportCSV
            // 
            this.MNExportCSV.Name = "MNExportCSV";
            this.MNExportCSV.Size = new System.Drawing.Size(129, 22);
            this.MNExportCSV.Text = "To CSV";
            // 
            // FrmMFGProcessWeightLossRecieve
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1276, 495);
            this.Controls.Add(this.panelControl3);
            this.Name = "FrmMFGProcessWeightLossRecieve";
            this.Text = "Process Recieve (2P / FARSI)";
            this.Load += new System.EventHandler(this.FrmMFGProcessWeightLossRecieve_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ttlbWeightLossRecieve)).EndInit();
            this.ttlbWeightLossRecieve.ResumeLayout(false);
            this.tblWeightLossdetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl7)).EndInit();
            this.panelControl7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelProgress)).EndInit();
            this.panelProgress.ResumeLayout(false);
            this.panelProgress.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SaveProgressBar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProcessWeightLossRecieve)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProcessWeightLossRecieve)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepRecDate.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepRecDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkWithoutLoss.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRejPcs.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRejCarat.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueKapan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReturnPcs.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReturnCarat.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLostPcs.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLostCarat.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLossPcs.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLossCarat.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueProcess.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueCutNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueSubProcess.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpReceiveDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpReceiveDate.Properties)).EndInit();
            this.tblWeightLossList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdWeightLossRec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWeightLossRec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).EndInit();
            this.panelControl5.ResumeLayout(false);
            this.panelControl5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpToDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpToDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate.Properties)).EndInit();
            this.ContextMNExport.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker_ProcRecWeightLoss;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraTab.XtraTabControl ttlbWeightLossRecieve;
        private DevExpress.XtraTab.XtraTabPage tblWeightLossList;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraGrid.GridControl grdWeightLossRec;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvWeightLossRec;
        private DevExpress.XtraGrid.Columns.GridColumn clmRecieve_Id;
        private DevExpress.XtraGrid.Columns.GridColumn clmIssue_Id;
        private DevExpress.XtraGrid.Columns.GridColumn clmRoughCutId;
        private DevExpress.XtraGrid.Columns.GridColumn clmRoughCut;
        private DevExpress.XtraGrid.Columns.GridColumn clmLot_Id;
        private DevExpress.XtraGrid.Columns.GridColumn clmCompany;
        private DevExpress.XtraGrid.Columns.GridColumn clmBranch;
        private DevExpress.XtraGrid.Columns.GridColumn clmLocation;
        private DevExpress.XtraGrid.Columns.GridColumn clmDepartment;
        private DevExpress.XtraGrid.Columns.GridColumn clmRecDate;
        private DevExpress.XtraGrid.Columns.GridColumn clmProcess_ID;
        private DevExpress.XtraGrid.Columns.GridColumn clmSubProcess_ID;
        private DevExpress.XtraGrid.Columns.GridColumn clmLoss_Pcs;
        private DevExpress.XtraGrid.Columns.GridColumn clmLoss_Carat;
        private DevExpress.XtraGrid.Columns.GridColumn clmLost_Pcs;
        private DevExpress.XtraGrid.Columns.GridColumn clmLost_Carat;
        private DevExpress.XtraGrid.Columns.GridColumn clmRecievePcs;
        private DevExpress.XtraGrid.Columns.GridColumn clmRecieveCarat;
        private DevExpress.XtraGrid.Columns.GridColumn clmEntryBy;
        private DevExpress.XtraGrid.Columns.GridColumn clmEntryDate;
        private DevExpress.XtraGrid.Columns.GridColumn clmEntryTime;
        private DevExpress.XtraGrid.Columns.GridColumn clmIP;
        private DevExpress.XtraEditors.PanelControl panelControl5;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.DateEdit dtpToDate;
        private DevExpress.XtraEditors.DateEdit dtpFromDate;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private DevExpress.XtraTab.XtraTabPage tblWeightLossdetail;
        private DevExpress.XtraEditors.PanelControl panelControl7;
        private DevExpress.XtraGrid.GridControl grdProcessWeightLossRecieve;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvProcessWeightLossRecieve;
        private DevExpress.XtraGrid.Columns.GridColumn clmRecieveID;
        private DevExpress.XtraGrid.Columns.GridColumn clmIssueDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit RepRecDate;
        private DevExpress.XtraGrid.Columns.GridColumn clmLotID;
        private DevExpress.XtraGrid.Columns.GridColumn clmCutNo;
        private DevExpress.XtraGrid.Columns.GridColumn clmManager;
        private DevExpress.XtraGrid.Columns.GridColumn clmManagerID;
        private DevExpress.XtraGrid.Columns.GridColumn clmEmployee;
        private DevExpress.XtraGrid.Columns.GridColumn clmEmployeeID;
        private DevExpress.XtraGrid.Columns.GridColumn clmProcess;
        private DevExpress.XtraGrid.Columns.GridColumn clmProcessID;
        private DevExpress.XtraGrid.Columns.GridColumn clmSubProcess;
        private DevExpress.XtraGrid.Columns.GridColumn clmSubProcessID;
        private DevExpress.XtraGrid.Columns.GridColumn clmReturnPcs;
        private DevExpress.XtraGrid.Columns.GridColumn clmReturnCarat;
        private DevExpress.XtraGrid.Columns.GridColumn clmLossPcs;
        private DevExpress.XtraGrid.Columns.GridColumn clmLossCarat;
        private DevExpress.XtraGrid.Columns.GridColumn clmRate;
        private DevExpress.XtraGrid.Columns.GridColumn clmAmount;
        private DevExpress.XtraGrid.Columns.GridColumn clmLostPcs;
        private DevExpress.XtraGrid.Columns.GridColumn clmLostCarat;
        private DevExpress.XtraGrid.Columns.GridColumn clmSrno;
        private DevExpress.XtraGrid.Columns.GridColumn clmIssueId;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnExport;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.TextEdit txtReturnPcs;
        private System.Windows.Forms.Label label10;
        private DevExpress.XtraEditors.TextEdit txtReturnCarat;
        private System.Windows.Forms.Label label11;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private System.Windows.Forms.Label lblOsCarat;
        private System.Windows.Forms.Label label9;
        private DevExpress.XtraEditors.TextEdit txtLostPcs;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraEditors.TextEdit txtLostCarat;
        private System.Windows.Forms.Label label8;
        private DevExpress.XtraEditors.TextEdit txtLotID;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.TextEdit txtLossPcs;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.TextEdit txtLossCarat;
        private System.Windows.Forms.Label label16;
        private DevExpress.XtraEditors.LookUpEdit lueProcess;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.LookUpEdit lueCutNo;
        private System.Windows.Forms.Label lblKapanNo;
        private DevExpress.XtraEditors.LookUpEdit lueSubProcess;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.DateEdit dtpReceiveDate;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.Columns.GridColumn clmLProcess;
        private DevExpress.XtraGrid.Columns.GridColumn clmLSubProcess;
        private DevExpress.XtraGrid.Columns.GridColumn clmEmployee_Id;
        private DevExpress.XtraGrid.Columns.GridColumn clmManager_Id;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.LookUpEdit lueKapan;
        private System.Windows.Forms.Label label14;
        private DevExpress.XtraEditors.TextEdit txtRejPcs;
        private System.Windows.Forms.Label label15;
        private DevExpress.XtraEditors.TextEdit txtRejCarat;
        private System.Windows.Forms.Label label17;
        private DevExpress.XtraGrid.Columns.GridColumn clmRejectionPcs;
        private DevExpress.XtraGrid.Columns.GridColumn clmRejectionCarat;
        private System.Windows.Forms.ContextMenuStrip ContextMNExport;
        private System.Windows.Forms.ToolStripMenuItem MNExportExcel;
        private System.Windows.Forms.ToolStripMenuItem MNExportPDF;
        private System.Windows.Forms.ToolStripMenuItem MNExportTEXT;
        private System.Windows.Forms.ToolStripMenuItem MNExportHTML;
        private System.Windows.Forms.ToolStripMenuItem MNExportRTF;
        private System.Windows.Forms.ToolStripMenuItem MNExportCSV;
        private System.Windows.Forms.Label lblOsPcs;
        private System.Windows.Forms.Label label19;
        private DevExpress.XtraGrid.Columns.GridColumn ClmRoughCutNo;
        private DevExpress.XtraGrid.Columns.GridColumn ClmKapanID;
        private DevExpress.XtraGrid.Columns.GridColumn ClmDelete;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit RepDelete;
        private DevExpress.XtraEditors.SimpleButton btnPopUpStock;
        private DevExpress.XtraEditors.PanelControl panelProgress;
        private System.Windows.Forms.Label lblProgressCount;
        private DevExpress.XtraEditors.MarqueeProgressBarControl SaveProgressBar;
        private DevExpress.XtraEditors.SimpleButton btnSearchRec;
        private DevExpress.XtraEditors.CheckEdit chkWithoutLoss;
    }
}