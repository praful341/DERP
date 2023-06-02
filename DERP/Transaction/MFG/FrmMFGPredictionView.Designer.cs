namespace DERP.Transaction.MFG
{
    partial class FrmMFGPredictionView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMFGPredictionView));
            this.RepRecDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.grdPredictionView = new DevExpress.XtraGrid.GridControl();
            this.ContextMNExport = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MNExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportPDF = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportTEXT = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportHTML = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportRTF = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportCSV = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvPredictionView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.clmPredictionID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmEntryDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmCutNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmLotID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmEmployee = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmProcess = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmPurity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmSubProcess = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmPcs = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmCarat = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmManager = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmUnionId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmCompany = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmBranch = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmLocation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmDepartment = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmRoughClarity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmRoughSieve = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmIsNet = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmNetRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmNetAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmEntryBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmEntryTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmIp = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lueKapan = new DevExpress.XtraEditors.LookUpEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLotID = new DevExpress.XtraEditors.TextEdit();
            this.label6 = new System.Windows.Forms.Label();
            this.lueCutNo = new DevExpress.XtraEditors.LookUpEdit();
            this.lblKapanNo = new System.Windows.Forms.Label();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.RepRecDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepRecDate.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPredictionView)).BeginInit();
            this.ContextMNExport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPredictionView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueKapan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueCutNo.Properties)).BeginInit();
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
            // grdPredictionView
            // 
            this.grdPredictionView.ContextMenuStrip = this.ContextMNExport;
            this.grdPredictionView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPredictionView.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdPredictionView.Location = new System.Drawing.Point(2, 48);
            this.grdPredictionView.MainView = this.dgvPredictionView;
            this.grdPredictionView.Name = "grdPredictionView";
            this.grdPredictionView.Size = new System.Drawing.Size(1098, 401);
            this.grdPredictionView.TabIndex = 97;
            this.grdPredictionView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvPredictionView,
            this.bandedGridView1});
            this.grdPredictionView.Click += new System.EventHandler(this.grdPredictionView_Click);
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
            this.MNExportPDF.Visible = false;
            this.MNExportPDF.Click += new System.EventHandler(this.MNExportPDF_Click);
            // 
            // MNExportTEXT
            // 
            this.MNExportTEXT.Name = "MNExportTEXT";
            this.MNExportTEXT.Size = new System.Drawing.Size(129, 22);
            this.MNExportTEXT.Text = "To TEXT";
            this.MNExportTEXT.Visible = false;
            this.MNExportTEXT.Click += new System.EventHandler(this.MNExportTEXT_Click);
            // 
            // MNExportHTML
            // 
            this.MNExportHTML.Name = "MNExportHTML";
            this.MNExportHTML.Size = new System.Drawing.Size(129, 22);
            this.MNExportHTML.Text = "To HTML";
            this.MNExportHTML.Visible = false;
            this.MNExportHTML.Click += new System.EventHandler(this.MNExportHTML_Click);
            // 
            // MNExportRTF
            // 
            this.MNExportRTF.Name = "MNExportRTF";
            this.MNExportRTF.Size = new System.Drawing.Size(129, 22);
            this.MNExportRTF.Text = "To RTF";
            this.MNExportRTF.Visible = false;
            this.MNExportRTF.Click += new System.EventHandler(this.MNExportRTF_Click);
            // 
            // MNExportCSV
            // 
            this.MNExportCSV.Name = "MNExportCSV";
            this.MNExportCSV.Size = new System.Drawing.Size(129, 22);
            this.MNExportCSV.Text = "To CSV";
            this.MNExportCSV.Visible = false;
            this.MNExportCSV.Click += new System.EventHandler(this.MNExportCSV_Click);
            // 
            // dgvPredictionView
            // 
            this.dgvPredictionView.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.dgvPredictionView.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.dgvPredictionView.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            this.dgvPredictionView.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            this.dgvPredictionView.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.dgvPredictionView.Appearance.FooterPanel.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.dgvPredictionView.Appearance.FooterPanel.Options.UseFont = true;
            this.dgvPredictionView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.dgvPredictionView.Appearance.HeaderPanel.Options.UseFont = true;
            this.dgvPredictionView.Appearance.Row.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold);
            this.dgvPredictionView.Appearance.Row.Options.UseFont = true;
            this.dgvPredictionView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.clmPredictionID,
            this.clmEntryDate,
            this.clmCutNo,
            this.clmLotID,
            this.clmEmployee,
            this.clmProcess,
            this.clmPurity,
            this.clmSubProcess,
            this.clmPcs,
            this.clmCarat,
            this.clmRate,
            this.clmAmount,
            this.clmManager,
            this.clmUnionId,
            this.clmCompany,
            this.clmBranch,
            this.clmLocation,
            this.clmDepartment,
            this.clmRoughClarity,
            this.clmRoughSieve,
            this.clmIsNet,
            this.clmNetRate,
            this.clmNetAmount,
            this.clmEntryBy,
            this.clmEntryTime,
            this.clmIp});
            this.dgvPredictionView.GridControl = this.grdPredictionView;
            this.dgvPredictionView.Name = "dgvPredictionView";
            this.dgvPredictionView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dgvPredictionView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.dgvPredictionView.OptionsCustomization.AllowColumnMoving = false;
            this.dgvPredictionView.OptionsCustomization.AllowQuickHideColumns = false;
            this.dgvPredictionView.OptionsNavigation.EnterMoveNextColumn = true;
            this.dgvPredictionView.OptionsView.ColumnAutoWidth = false;
            this.dgvPredictionView.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.dgvPredictionView.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.dgvPredictionView.OptionsView.ShowAutoFilterRow = true;
            this.dgvPredictionView.OptionsView.ShowFooter = true;
            this.dgvPredictionView.OptionsView.ShowGroupPanel = false;
            this.dgvPredictionView.CustomSummaryCalculate += new DevExpress.Data.CustomSummaryEventHandler(this.dgvPredictionView_CustomSummaryCalculate);
            // 
            // clmPredictionID
            // 
            this.clmPredictionID.Caption = "Prediction ID";
            this.clmPredictionID.FieldName = "prediction_id";
            this.clmPredictionID.Name = "clmPredictionID";
            this.clmPredictionID.OptionsColumn.AllowEdit = false;
            // 
            // clmEntryDate
            // 
            this.clmEntryDate.Caption = "Date";
            this.clmEntryDate.ColumnEdit = this.RepRecDate;
            this.clmEntryDate.DisplayFormat.FormatString = "d";
            this.clmEntryDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.clmEntryDate.FieldName = "entry_date";
            this.clmEntryDate.Name = "clmEntryDate";
            this.clmEntryDate.OptionsColumn.AllowEdit = false;
            this.clmEntryDate.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.clmEntryDate.Visible = true;
            this.clmEntryDate.VisibleIndex = 0;
            this.clmEntryDate.Width = 76;
            // 
            // clmCutNo
            // 
            this.clmCutNo.Caption = "Cut No";
            this.clmCutNo.FieldName = "rough_cut_no";
            this.clmCutNo.Name = "clmCutNo";
            this.clmCutNo.OptionsColumn.AllowEdit = false;
            this.clmCutNo.Visible = true;
            this.clmCutNo.VisibleIndex = 1;
            this.clmCutNo.Width = 38;
            // 
            // clmLotID
            // 
            this.clmLotID.Caption = "Lot ID";
            this.clmLotID.FieldName = "lot_id";
            this.clmLotID.Name = "clmLotID";
            this.clmLotID.OptionsColumn.AllowEdit = false;
            this.clmLotID.Visible = true;
            this.clmLotID.VisibleIndex = 2;
            this.clmLotID.Width = 36;
            // 
            // clmEmployee
            // 
            this.clmEmployee.Caption = "Employee";
            this.clmEmployee.FieldName = "employee";
            this.clmEmployee.Name = "clmEmployee";
            this.clmEmployee.OptionsColumn.AllowEdit = false;
            this.clmEmployee.Visible = true;
            this.clmEmployee.VisibleIndex = 4;
            this.clmEmployee.Width = 68;
            // 
            // clmProcess
            // 
            this.clmProcess.Caption = "Process";
            this.clmProcess.FieldName = "process_name";
            this.clmProcess.Name = "clmProcess";
            this.clmProcess.OptionsColumn.AllowEdit = false;
            this.clmProcess.Visible = true;
            this.clmProcess.VisibleIndex = 8;
            this.clmProcess.Width = 65;
            // 
            // clmPurity
            // 
            this.clmPurity.Caption = "Purity";
            this.clmPurity.FieldName = "purity_name";
            this.clmPurity.Name = "clmPurity";
            this.clmPurity.OptionsColumn.AllowEdit = false;
            this.clmPurity.Visible = true;
            this.clmPurity.VisibleIndex = 6;
            this.clmPurity.Width = 46;
            // 
            // clmSubProcess
            // 
            this.clmSubProcess.Caption = "Sub Process";
            this.clmSubProcess.FieldName = "sub_process_name";
            this.clmSubProcess.Name = "clmSubProcess";
            this.clmSubProcess.OptionsColumn.AllowEdit = false;
            this.clmSubProcess.Visible = true;
            this.clmSubProcess.VisibleIndex = 9;
            this.clmSubProcess.Width = 66;
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
            this.clmPcs.VisibleIndex = 10;
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
            this.clmCarat.VisibleIndex = 11;
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
            this.clmRate.VisibleIndex = 12;
            this.clmRate.Width = 60;
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
            this.clmAmount.VisibleIndex = 13;
            this.clmAmount.Width = 60;
            // 
            // clmManager
            // 
            this.clmManager.Caption = "Manager";
            this.clmManager.FieldName = "manager";
            this.clmManager.Name = "clmManager";
            this.clmManager.OptionsColumn.AllowEdit = false;
            this.clmManager.Visible = true;
            this.clmManager.VisibleIndex = 3;
            this.clmManager.Width = 65;
            // 
            // clmUnionId
            // 
            this.clmUnionId.Caption = "UnionId";
            this.clmUnionId.FieldName = "union_id";
            this.clmUnionId.Name = "clmUnionId";
            this.clmUnionId.OptionsColumn.AllowEdit = false;
            // 
            // clmCompany
            // 
            this.clmCompany.Caption = "Company";
            this.clmCompany.FieldName = "company_name";
            this.clmCompany.Name = "clmCompany";
            this.clmCompany.OptionsColumn.AllowEdit = false;
            this.clmCompany.Visible = true;
            this.clmCompany.VisibleIndex = 17;
            // 
            // clmBranch
            // 
            this.clmBranch.Caption = "Branch";
            this.clmBranch.FieldName = "branch_name";
            this.clmBranch.Name = "clmBranch";
            this.clmBranch.OptionsColumn.AllowEdit = false;
            this.clmBranch.Visible = true;
            this.clmBranch.VisibleIndex = 18;
            // 
            // clmLocation
            // 
            this.clmLocation.Caption = "Location";
            this.clmLocation.FieldName = "location_name";
            this.clmLocation.Name = "clmLocation";
            this.clmLocation.OptionsColumn.AllowEdit = false;
            this.clmLocation.Visible = true;
            this.clmLocation.VisibleIndex = 19;
            this.clmLocation.Width = 61;
            // 
            // clmDepartment
            // 
            this.clmDepartment.Caption = "Department";
            this.clmDepartment.FieldName = "department_name";
            this.clmDepartment.Name = "clmDepartment";
            this.clmDepartment.OptionsColumn.AllowEdit = false;
            this.clmDepartment.Visible = true;
            this.clmDepartment.VisibleIndex = 20;
            // 
            // clmRoughClarity
            // 
            this.clmRoughClarity.Caption = "Rough Clarity";
            this.clmRoughClarity.FieldName = "rough_clarity_name";
            this.clmRoughClarity.Name = "clmRoughClarity";
            this.clmRoughClarity.OptionsColumn.AllowEdit = false;
            this.clmRoughClarity.Visible = true;
            this.clmRoughClarity.VisibleIndex = 5;
            this.clmRoughClarity.Width = 51;
            // 
            // clmRoughSieve
            // 
            this.clmRoughSieve.Caption = "Rough Sieve";
            this.clmRoughSieve.FieldName = "sieve_name";
            this.clmRoughSieve.Name = "clmRoughSieve";
            this.clmRoughSieve.OptionsColumn.AllowEdit = false;
            this.clmRoughSieve.Visible = true;
            this.clmRoughSieve.VisibleIndex = 7;
            this.clmRoughSieve.Width = 39;
            // 
            // clmIsNet
            // 
            this.clmIsNet.Caption = "Is Parakh";
            this.clmIsNet.FieldName = "is_net_rate";
            this.clmIsNet.Name = "clmIsNet";
            this.clmIsNet.OptionsColumn.AllowEdit = false;
            this.clmIsNet.Visible = true;
            this.clmIsNet.VisibleIndex = 14;
            this.clmIsNet.Width = 36;
            // 
            // clmNetRate
            // 
            this.clmNetRate.Caption = "Net Rate";
            this.clmNetRate.FieldName = "net_rate";
            this.clmNetRate.Name = "clmNetRate";
            this.clmNetRate.OptionsColumn.AllowEdit = false;
            this.clmNetRate.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom)});
            this.clmNetRate.Visible = true;
            this.clmNetRate.VisibleIndex = 15;
            this.clmNetRate.Width = 65;
            // 
            // clmNetAmount
            // 
            this.clmNetAmount.Caption = "Net Amount";
            this.clmNetAmount.FieldName = "net_amount";
            this.clmNetAmount.Name = "clmNetAmount";
            this.clmNetAmount.OptionsColumn.AllowEdit = false;
            this.clmNetAmount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.clmNetAmount.Visible = true;
            this.clmNetAmount.VisibleIndex = 16;
            this.clmNetAmount.Width = 59;
            // 
            // clmEntryBy
            // 
            this.clmEntryBy.Caption = "Entry By";
            this.clmEntryBy.FieldName = "entry_by";
            this.clmEntryBy.Name = "clmEntryBy";
            this.clmEntryBy.OptionsColumn.AllowEdit = false;
            this.clmEntryBy.Visible = true;
            this.clmEntryBy.VisibleIndex = 21;
            this.clmEntryBy.Width = 61;
            // 
            // clmEntryTime
            // 
            this.clmEntryTime.Caption = "Entry Time";
            this.clmEntryTime.FieldName = "entry_time";
            this.clmEntryTime.Name = "clmEntryTime";
            this.clmEntryTime.OptionsColumn.AllowEdit = false;
            this.clmEntryTime.Visible = true;
            this.clmEntryTime.VisibleIndex = 22;
            this.clmEntryTime.Width = 67;
            // 
            // clmIp
            // 
            this.clmIp.Caption = "Entry IP";
            this.clmIp.FieldName = "ip_address";
            this.clmIp.Name = "clmIp";
            this.clmIp.OptionsColumn.AllowEdit = false;
            this.clmIp.Visible = true;
            this.clmIp.VisibleIndex = 23;
            // 
            // bandedGridView1
            // 
            this.bandedGridView1.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1});
            this.bandedGridView1.GridControl = this.grdPredictionView;
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
            this.panelControl2.Controls.Add(this.btnExit);
            this.panelControl2.Controls.Add(this.btnClear);
            this.panelControl2.Controls.Add(this.btnSave);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 451);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1102, 44);
            this.panelControl2.TabIndex = 98;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Appearance.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Appearance.Options.UseFont = true;
            this.btnExit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.ImageOptions.Image")));
            this.btnExit.Location = new System.Drawing.Point(984, 6);
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
            this.btnClear.Location = new System.Drawing.Point(876, 6);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(102, 32);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "&Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Appearance.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.ImageOptions.Image")));
            this.btnSave.Location = new System.Drawing.Point(767, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(102, 32);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.Visible = false;
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Appearance.Options.UseFont = true;
            this.btnExport.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.ImageOptions.Image")));
            this.btnExport.Location = new System.Drawing.Point(670, 5);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(102, 32);
            this.btnExport.TabIndex = 4;
            this.btnExport.Text = "&Export";
            this.btnExport.Visible = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.grdPredictionView);
            this.panelControl3.Controls.Add(this.panelControl1);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(0, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(1102, 451);
            this.panelControl3.TabIndex = 99;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lueKapan);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Controls.Add(this.btnExport);
            this.panelControl1.Controls.Add(this.txtLotID);
            this.panelControl1.Controls.Add(this.label6);
            this.panelControl1.Controls.Add(this.lueCutNo);
            this.panelControl1.Controls.Add(this.lblKapanNo);
            this.panelControl1.Controls.Add(this.btnAdd);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(2, 2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1098, 46);
            this.panelControl1.TabIndex = 98;
            // 
            // lueKapan
            // 
            this.lueKapan.EnterMoveNextControl = true;
            this.lueKapan.Location = new System.Drawing.Point(80, 11);
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
            this.lueKapan.Size = new System.Drawing.Size(121, 22);
            this.lueKapan.TabIndex = 0;
            this.lueKapan.EditValueChanged += new System.EventHandler(this.lueKapan_EditValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(6, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 16);
            this.label1.TabIndex = 558;
            this.label1.Text = "Kapan No";
            // 
            // txtLotID
            // 
            this.txtLotID.EnterMoveNextControl = true;
            this.txtLotID.Location = new System.Drawing.Point(417, 10);
            this.txtLotID.Name = "txtLotID";
            this.txtLotID.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLotID.Properties.Appearance.Options.UseFont = true;
            this.txtLotID.Size = new System.Drawing.Size(113, 22);
            this.txtLotID.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(385, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 16);
            this.label6.TabIndex = 556;
            this.label6.Text = "Lot";
            // 
            // lueCutNo
            // 
            this.lueCutNo.EnterMoveNextControl = true;
            this.lueCutNo.Location = new System.Drawing.Point(258, 10);
            this.lueCutNo.Name = "lueCutNo";
            this.lueCutNo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueCutNo.Properties.Appearance.Options.UseFont = true;
            this.lueCutNo.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
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
            this.lueCutNo.Size = new System.Drawing.Size(121, 22);
            this.lueCutNo.TabIndex = 1;
            // 
            // lblKapanNo
            // 
            this.lblKapanNo.AutoSize = true;
            this.lblKapanNo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblKapanNo.Location = new System.Drawing.Point(202, 14);
            this.lblKapanNo.Name = "lblKapanNo";
            this.lblKapanNo.Size = new System.Drawing.Size(50, 16);
            this.lblKapanNo.TabIndex = 545;
            this.lblKapanNo.Text = "Cut No";
            // 
            // btnAdd
            // 
            this.btnAdd.Appearance.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Appearance.Options.UseFont = true;
            this.btnAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.ImageOptions.Image")));
            this.btnAdd.Location = new System.Drawing.Point(555, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 32);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Show";
            this.btnAdd.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // FrmMFGPredictionView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1102, 495);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.panelControl2);
            this.Name = "FrmMFGPredictionView";
            this.Text = "Prediction View";
            this.Load += new System.EventHandler(this.FrmMFGProcessIssue_Load);
            ((System.ComponentModel.ISupportInitialize)(this.RepRecDate.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepRecDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPredictionView)).EndInit();
            this.ContextMNExport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPredictionView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueKapan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueCutNo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.GridControl grdPredictionView;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvPredictionView;
        private DevExpress.XtraGrid.Columns.GridColumn clmPredictionID;
        private DevExpress.XtraGrid.Columns.GridColumn clmEntryDate;
        private DevExpress.XtraGrid.Columns.GridColumn clmLotID;
        private DevExpress.XtraGrid.Columns.GridColumn clmCutNo;
        private DevExpress.XtraGrid.Columns.GridColumn clmEmployee;
        private DevExpress.XtraGrid.Columns.GridColumn clmProcess;
        private DevExpress.XtraGrid.Columns.GridColumn clmPurity;
        private DevExpress.XtraGrid.Columns.GridColumn clmSubProcess;
        private DevExpress.XtraGrid.Columns.GridColumn clmPcs;
        private DevExpress.XtraGrid.Columns.GridColumn clmCarat;
        private DevExpress.XtraGrid.Columns.GridColumn clmRate;
        private DevExpress.XtraGrid.Columns.GridColumn clmAmount;
        private DevExpress.XtraGrid.Columns.GridColumn clmManager;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit RepRecDate;
        private DevExpress.XtraEditors.SimpleButton btnExport;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.TextEdit txtLotID;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.LookUpEdit lueCutNo;
        private System.Windows.Forms.Label lblKapanNo;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraGrid.Columns.GridColumn clmUnionId;
        private DevExpress.XtraGrid.Columns.GridColumn clmCompany;
        private DevExpress.XtraGrid.Columns.GridColumn clmBranch;
        private DevExpress.XtraGrid.Columns.GridColumn clmLocation;
        private DevExpress.XtraGrid.Columns.GridColumn clmDepartment;
        private DevExpress.XtraGrid.Columns.GridColumn clmRoughClarity;
        private DevExpress.XtraGrid.Columns.GridColumn clmRoughSieve;
        private DevExpress.XtraGrid.Columns.GridColumn clmIsNet;
        private DevExpress.XtraGrid.Columns.GridColumn clmNetRate;
        private DevExpress.XtraGrid.Columns.GridColumn clmNetAmount;
        private DevExpress.XtraGrid.Columns.GridColumn clmEntryBy;
        private DevExpress.XtraGrid.Columns.GridColumn clmEntryTime;
        private DevExpress.XtraGrid.Columns.GridColumn clmIp;
        private System.Windows.Forms.ContextMenuStrip ContextMNExport;
        private System.Windows.Forms.ToolStripMenuItem MNExportExcel;
        private System.Windows.Forms.ToolStripMenuItem MNExportPDF;
        private System.Windows.Forms.ToolStripMenuItem MNExportTEXT;
        private System.Windows.Forms.ToolStripMenuItem MNExportHTML;
        private System.Windows.Forms.ToolStripMenuItem MNExportRTF;
        private System.Windows.Forms.ToolStripMenuItem MNExportCSV;
        private DevExpress.XtraEditors.LookUpEdit lueKapan;
        private System.Windows.Forms.Label label1;
    }
}