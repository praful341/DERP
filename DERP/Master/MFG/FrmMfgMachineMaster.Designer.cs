namespace DERP.Master.MFG
{
    partial class FrmMfgMachineMaster
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
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.grdMachineMaster = new DevExpress.XtraGrid.GridControl();
            this.ContextMNExport = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MNExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportPDF = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportTEXT = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportHTML = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportRTF = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportCSV = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvMachineMaster = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.clmMachineId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmMachineName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmManualCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmModelNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmSerialNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmCompany = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmBranch = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmLocation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmDepartment = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmMachineType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmTeam = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmGroup = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmProcess = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmVendorName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmCategory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmAcquiredDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmInstalldate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmdepriciableLife = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmElectricityPerHrs = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmMachineActivity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmCompanyId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmBranchId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmLocationId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmDepartmentId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmTeamId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmGroupId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmProcessId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmMachineTypeId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmActive = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmPurchaseDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmRobertKitDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmPurchaseRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl5 = new DevExpress.XtraEditors.PanelControl();
            this.txtPurchaseRate = new DevExpress.XtraEditors.TextEdit();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.dtpRobertKitDate = new DevExpress.XtraEditors.DateEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.dtpPurchaseDate = new DevExpress.XtraEditors.DateEdit();
            this.txtMachineActivity = new DevExpress.XtraEditors.TextEdit();
            this.labelControl18 = new DevExpress.XtraEditors.LabelControl();
            this.txtElectricityPerHrs = new DevExpress.XtraEditors.TextEdit();
            this.labelControl20 = new DevExpress.XtraEditors.LabelControl();
            this.txtDepriciable = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl33 = new DevExpress.XtraEditors.LabelControl();
            this.dtpInstallationDate = new DevExpress.XtraEditors.DateEdit();
            this.labelControl31 = new DevExpress.XtraEditors.LabelControl();
            this.dtpAcquiredDate = new DevExpress.XtraEditors.DateEdit();
            this.lueMachineType = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl29 = new DevExpress.XtraEditors.LabelControl();
            this.txtCategory = new DevExpress.XtraEditors.TextEdit();
            this.labelControl25 = new DevExpress.XtraEditors.LabelControl();
            this.txtVendorName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl27 = new DevExpress.XtraEditors.LabelControl();
            this.lueGroup = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl23 = new DevExpress.XtraEditors.LabelControl();
            this.lueTeam = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl21 = new DevExpress.XtraEditors.LabelControl();
            this.lueProcess = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl19 = new DevExpress.XtraEditors.LabelControl();
            this.lueDepartment = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.lueLocation = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.lueBranch = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.lueCompany = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl17 = new DevExpress.XtraEditors.LabelControl();
            this.txtSerialNo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtModelNo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtManualCode = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtRemark = new DevExpress.XtraEditors.MemoEdit();
            this.chkActive = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.txtMachineName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl6 = new DevExpress.XtraEditors.PanelControl();
            this.lblMode = new DevExpress.XtraEditors.LabelControl();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.chkIsMachine = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMachineMaster)).BeginInit();
            this.ContextMNExport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMachineMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).BeginInit();
            this.panelControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPurchaseRate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpRobertKitDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpRobertKitDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpPurchaseDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpPurchaseDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMachineActivity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtElectricityPerHrs.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDepriciable.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpInstallationDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpInstallationDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpAcquiredDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpAcquiredDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueMachineType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCategory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVendorName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueTeam.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueProcess.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueDepartment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueLocation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueBranch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSerialNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtModelNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtManualCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkActive.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMachineName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).BeginInit();
            this.panelControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsMachine.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel1});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevExpress.XtraBars.Navigation.OfficeNavigationBar",
            "DevExpress.XtraBars.Navigation.TileNavPane",
            "DevExpress.XtraBars.TabFormControl"});
            // 
            // dockPanel1
            // 
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanel1.ID = new System.Guid("31d52c2d-f66d-4052-a32f-f65b92637cc5");
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.OriginalSize = new System.Drawing.Size(239, 200);
            this.dockPanel1.Size = new System.Drawing.Size(239, 517);
            this.dockPanel1.Text = "Machine Master";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.grdMachineMaster);
            this.dockPanel1_Container.Location = new System.Drawing.Point(4, 23);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(230, 490);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // grdMachineMaster
            // 
            this.grdMachineMaster.ContextMenuStrip = this.ContextMNExport;
            this.grdMachineMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMachineMaster.Location = new System.Drawing.Point(0, 0);
            this.grdMachineMaster.MainView = this.dgvMachineMaster;
            this.grdMachineMaster.Name = "grdMachineMaster";
            this.grdMachineMaster.Size = new System.Drawing.Size(230, 490);
            this.grdMachineMaster.TabIndex = 15;
            this.grdMachineMaster.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvMachineMaster});
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
            // dgvMachineMaster
            // 
            this.dgvMachineMaster.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.dgvMachineMaster.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.dgvMachineMaster.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            this.dgvMachineMaster.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            this.dgvMachineMaster.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.dgvMachineMaster.Appearance.FooterPanel.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.dgvMachineMaster.Appearance.FooterPanel.Options.UseFont = true;
            this.dgvMachineMaster.Appearance.HeaderPanel.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.dgvMachineMaster.Appearance.HeaderPanel.Options.UseFont = true;
            this.dgvMachineMaster.Appearance.Row.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold);
            this.dgvMachineMaster.Appearance.Row.Options.UseFont = true;
            this.dgvMachineMaster.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.clmMachineId,
            this.clmMachineName,
            this.clmManualCode,
            this.clmModelNo,
            this.clmSerialNo,
            this.clmCompany,
            this.clmBranch,
            this.clmLocation,
            this.clmDepartment,
            this.clmMachineType,
            this.clmTeam,
            this.clmGroup,
            this.clmProcess,
            this.clmVendorName,
            this.clmCategory,
            this.clmAcquiredDate,
            this.clmInstalldate,
            this.clmdepriciableLife,
            this.clmElectricityPerHrs,
            this.clmMachineActivity,
            this.clmCompanyId,
            this.clmBranchId,
            this.clmLocationId,
            this.clmDepartmentId,
            this.clmTeamId,
            this.clmGroupId,
            this.clmProcessId,
            this.clmMachineTypeId,
            this.clmRemark,
            this.clmActive,
            this.clmPurchaseDate,
            this.clmRobertKitDate,
            this.clmPurchaseRate});
            this.dgvMachineMaster.GridControl = this.grdMachineMaster;
            this.dgvMachineMaster.Name = "dgvMachineMaster";
            this.dgvMachineMaster.OptionsBehavior.Editable = false;
            this.dgvMachineMaster.OptionsBehavior.ReadOnly = true;
            this.dgvMachineMaster.OptionsCustomization.AllowQuickHideColumns = false;
            this.dgvMachineMaster.OptionsView.ColumnAutoWidth = false;
            this.dgvMachineMaster.OptionsView.ShowAutoFilterRow = true;
            this.dgvMachineMaster.OptionsView.ShowFooter = true;
            this.dgvMachineMaster.OptionsView.ShowGroupPanel = false;
            this.dgvMachineMaster.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.dgvMfgTensionTypeMaster_RowClick);
            // 
            // clmMachineId
            // 
            this.clmMachineId.Caption = "Machine Id";
            this.clmMachineId.FieldName = "machine_id";
            this.clmMachineId.Name = "clmMachineId";
            this.clmMachineId.OptionsColumn.AllowEdit = false;
            // 
            // clmMachineName
            // 
            this.clmMachineName.Caption = "Machine Name";
            this.clmMachineName.FieldName = "machine_name";
            this.clmMachineName.Name = "clmMachineName";
            this.clmMachineName.OptionsColumn.AllowEdit = false;
            this.clmMachineName.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.clmMachineName.Visible = true;
            this.clmMachineName.VisibleIndex = 0;
            // 
            // clmManualCode
            // 
            this.clmManualCode.Caption = "Manual Code";
            this.clmManualCode.FieldName = "manual_code";
            this.clmManualCode.Name = "clmManualCode";
            this.clmManualCode.Visible = true;
            this.clmManualCode.VisibleIndex = 1;
            // 
            // clmModelNo
            // 
            this.clmModelNo.Caption = "Model No";
            this.clmModelNo.FieldName = "model_no";
            this.clmModelNo.Name = "clmModelNo";
            this.clmModelNo.Visible = true;
            this.clmModelNo.VisibleIndex = 2;
            // 
            // clmSerialNo
            // 
            this.clmSerialNo.Caption = "Serial No";
            this.clmSerialNo.FieldName = "serial_no";
            this.clmSerialNo.Name = "clmSerialNo";
            this.clmSerialNo.Visible = true;
            this.clmSerialNo.VisibleIndex = 3;
            // 
            // clmCompany
            // 
            this.clmCompany.Caption = "Company";
            this.clmCompany.FieldName = "company_name";
            this.clmCompany.Name = "clmCompany";
            this.clmCompany.Visible = true;
            this.clmCompany.VisibleIndex = 4;
            // 
            // clmBranch
            // 
            this.clmBranch.Caption = "Branch";
            this.clmBranch.FieldName = "branch_name";
            this.clmBranch.Name = "clmBranch";
            this.clmBranch.Visible = true;
            this.clmBranch.VisibleIndex = 5;
            // 
            // clmLocation
            // 
            this.clmLocation.Caption = "Location";
            this.clmLocation.FieldName = "location_name";
            this.clmLocation.Name = "clmLocation";
            this.clmLocation.Visible = true;
            this.clmLocation.VisibleIndex = 6;
            // 
            // clmDepartment
            // 
            this.clmDepartment.Caption = "Department";
            this.clmDepartment.FieldName = "department_name";
            this.clmDepartment.Name = "clmDepartment";
            this.clmDepartment.Visible = true;
            this.clmDepartment.VisibleIndex = 7;
            // 
            // clmMachineType
            // 
            this.clmMachineType.Caption = "Machine Type";
            this.clmMachineType.FieldName = "machine_type";
            this.clmMachineType.Name = "clmMachineType";
            this.clmMachineType.Visible = true;
            this.clmMachineType.VisibleIndex = 8;
            // 
            // clmTeam
            // 
            this.clmTeam.Caption = "Team";
            this.clmTeam.FieldName = "team_name";
            this.clmTeam.Name = "clmTeam";
            this.clmTeam.Visible = true;
            this.clmTeam.VisibleIndex = 9;
            // 
            // clmGroup
            // 
            this.clmGroup.Caption = "Group";
            this.clmGroup.FieldName = "group_name";
            this.clmGroup.Name = "clmGroup";
            this.clmGroup.Visible = true;
            this.clmGroup.VisibleIndex = 10;
            // 
            // clmProcess
            // 
            this.clmProcess.Caption = "Process";
            this.clmProcess.FieldName = "process_name";
            this.clmProcess.Name = "clmProcess";
            this.clmProcess.Visible = true;
            this.clmProcess.VisibleIndex = 11;
            // 
            // clmVendorName
            // 
            this.clmVendorName.Caption = "Vendor Name";
            this.clmVendorName.FieldName = "vendor_name";
            this.clmVendorName.Name = "clmVendorName";
            this.clmVendorName.Visible = true;
            this.clmVendorName.VisibleIndex = 12;
            // 
            // clmCategory
            // 
            this.clmCategory.Caption = "Category";
            this.clmCategory.FieldName = "category_name";
            this.clmCategory.Name = "clmCategory";
            this.clmCategory.Visible = true;
            this.clmCategory.VisibleIndex = 13;
            // 
            // clmAcquiredDate
            // 
            this.clmAcquiredDate.Caption = "Acquired Date";
            this.clmAcquiredDate.FieldName = "date_acquired";
            this.clmAcquiredDate.Name = "clmAcquiredDate";
            this.clmAcquiredDate.Visible = true;
            this.clmAcquiredDate.VisibleIndex = 14;
            // 
            // clmInstalldate
            // 
            this.clmInstalldate.Caption = "Installation Date";
            this.clmInstalldate.FieldName = "installation_date";
            this.clmInstalldate.Name = "clmInstalldate";
            this.clmInstalldate.Visible = true;
            this.clmInstalldate.VisibleIndex = 15;
            // 
            // clmdepriciableLife
            // 
            this.clmdepriciableLife.Caption = "Depriciable Life";
            this.clmdepriciableLife.FieldName = "depriciable_life";
            this.clmdepriciableLife.Name = "clmdepriciableLife";
            this.clmdepriciableLife.Visible = true;
            this.clmdepriciableLife.VisibleIndex = 17;
            // 
            // clmElectricityPerHrs
            // 
            this.clmElectricityPerHrs.Caption = "Electricity Per Hrs";
            this.clmElectricityPerHrs.FieldName = "electricity_per_hrs";
            this.clmElectricityPerHrs.Name = "clmElectricityPerHrs";
            this.clmElectricityPerHrs.Visible = true;
            this.clmElectricityPerHrs.VisibleIndex = 18;
            // 
            // clmMachineActivity
            // 
            this.clmMachineActivity.Caption = "Machine Activity";
            this.clmMachineActivity.FieldName = "machine_activity";
            this.clmMachineActivity.Name = "clmMachineActivity";
            this.clmMachineActivity.Visible = true;
            this.clmMachineActivity.VisibleIndex = 19;
            // 
            // clmCompanyId
            // 
            this.clmCompanyId.Caption = "Company Id";
            this.clmCompanyId.FieldName = "company_id";
            this.clmCompanyId.Name = "clmCompanyId";
            // 
            // clmBranchId
            // 
            this.clmBranchId.Caption = "Branch Id";
            this.clmBranchId.FieldName = "branch_id";
            this.clmBranchId.Name = "clmBranchId";
            // 
            // clmLocationId
            // 
            this.clmLocationId.Caption = "Location Id";
            this.clmLocationId.FieldName = "location_id";
            this.clmLocationId.Name = "clmLocationId";
            // 
            // clmDepartmentId
            // 
            this.clmDepartmentId.Caption = "Department Id";
            this.clmDepartmentId.FieldName = "department_id";
            this.clmDepartmentId.Name = "clmDepartmentId";
            // 
            // clmTeamId
            // 
            this.clmTeamId.Caption = "Team Id";
            this.clmTeamId.FieldName = "team_id";
            this.clmTeamId.Name = "clmTeamId";
            this.clmTeamId.Visible = true;
            this.clmTeamId.VisibleIndex = 22;
            // 
            // clmGroupId
            // 
            this.clmGroupId.Caption = "Group Id";
            this.clmGroupId.FieldName = "group_id";
            this.clmGroupId.Name = "clmGroupId";
            // 
            // clmProcessId
            // 
            this.clmProcessId.Caption = "Process Id";
            this.clmProcessId.FieldName = "process_id";
            this.clmProcessId.Name = "clmProcessId";
            // 
            // clmMachineTypeId
            // 
            this.clmMachineTypeId.Caption = "Machine Type Id";
            this.clmMachineTypeId.FieldName = "machine_type_id";
            this.clmMachineTypeId.Name = "clmMachineTypeId";
            // 
            // clmRemark
            // 
            this.clmRemark.Caption = "Remark";
            this.clmRemark.FieldName = "remarks";
            this.clmRemark.Name = "clmRemark";
            this.clmRemark.OptionsColumn.AllowEdit = false;
            this.clmRemark.Visible = true;
            this.clmRemark.VisibleIndex = 20;
            // 
            // clmActive
            // 
            this.clmActive.Caption = "Active";
            this.clmActive.FieldName = "active";
            this.clmActive.Name = "clmActive";
            this.clmActive.OptionsColumn.AllowEdit = false;
            this.clmActive.Visible = true;
            this.clmActive.VisibleIndex = 21;
            // 
            // clmPurchaseDate
            // 
            this.clmPurchaseDate.Caption = "Purchase Date";
            this.clmPurchaseDate.FieldName = "purchase_date";
            this.clmPurchaseDate.Name = "clmPurchaseDate";
            // 
            // clmRobertKitDate
            // 
            this.clmRobertKitDate.Caption = "Robert Kit Date";
            this.clmRobertKitDate.FieldName = "robert_kit_date";
            this.clmRobertKitDate.Name = "clmRobertKitDate";
            this.clmRobertKitDate.Visible = true;
            this.clmRobertKitDate.VisibleIndex = 16;
            // 
            // clmPurchaseRate
            // 
            this.clmPurchaseRate.Caption = "Purchase Rate";
            this.clmPurchaseRate.FieldName = "purchase_rate";
            this.clmPurchaseRate.Name = "clmPurchaseRate";
            this.clmPurchaseRate.Visible = true;
            this.clmPurchaseRate.VisibleIndex = 23;
            // 
            // panelControl5
            // 
            this.panelControl5.Controls.Add(this.chkIsMachine);
            this.panelControl5.Controls.Add(this.txtPurchaseRate);
            this.panelControl5.Controls.Add(this.labelControl11);
            this.panelControl5.Controls.Add(this.labelControl10);
            this.panelControl5.Controls.Add(this.dtpRobertKitDate);
            this.panelControl5.Controls.Add(this.labelControl7);
            this.panelControl5.Controls.Add(this.dtpPurchaseDate);
            this.panelControl5.Controls.Add(this.txtMachineActivity);
            this.panelControl5.Controls.Add(this.labelControl18);
            this.panelControl5.Controls.Add(this.txtElectricityPerHrs);
            this.panelControl5.Controls.Add(this.labelControl20);
            this.panelControl5.Controls.Add(this.txtDepriciable);
            this.panelControl5.Controls.Add(this.labelControl5);
            this.panelControl5.Controls.Add(this.labelControl33);
            this.panelControl5.Controls.Add(this.dtpInstallationDate);
            this.panelControl5.Controls.Add(this.labelControl31);
            this.panelControl5.Controls.Add(this.dtpAcquiredDate);
            this.panelControl5.Controls.Add(this.lueMachineType);
            this.panelControl5.Controls.Add(this.labelControl29);
            this.panelControl5.Controls.Add(this.txtCategory);
            this.panelControl5.Controls.Add(this.labelControl25);
            this.panelControl5.Controls.Add(this.txtVendorName);
            this.panelControl5.Controls.Add(this.labelControl27);
            this.panelControl5.Controls.Add(this.lueGroup);
            this.panelControl5.Controls.Add(this.labelControl23);
            this.panelControl5.Controls.Add(this.lueTeam);
            this.panelControl5.Controls.Add(this.labelControl21);
            this.panelControl5.Controls.Add(this.lueProcess);
            this.panelControl5.Controls.Add(this.labelControl19);
            this.panelControl5.Controls.Add(this.lueDepartment);
            this.panelControl5.Controls.Add(this.labelControl14);
            this.panelControl5.Controls.Add(this.lueLocation);
            this.panelControl5.Controls.Add(this.labelControl12);
            this.panelControl5.Controls.Add(this.lueBranch);
            this.panelControl5.Controls.Add(this.labelControl16);
            this.panelControl5.Controls.Add(this.lueCompany);
            this.panelControl5.Controls.Add(this.labelControl17);
            this.panelControl5.Controls.Add(this.txtSerialNo);
            this.panelControl5.Controls.Add(this.labelControl6);
            this.panelControl5.Controls.Add(this.txtModelNo);
            this.panelControl5.Controls.Add(this.labelControl9);
            this.panelControl5.Controls.Add(this.labelControl3);
            this.panelControl5.Controls.Add(this.txtManualCode);
            this.panelControl5.Controls.Add(this.labelControl4);
            this.panelControl5.Controls.Add(this.txtRemark);
            this.panelControl5.Controls.Add(this.chkActive);
            this.panelControl5.Controls.Add(this.labelControl8);
            this.panelControl5.Controls.Add(this.txtMachineName);
            this.panelControl5.Controls.Add(this.labelControl1);
            this.panelControl5.Controls.Add(this.panelControl6);
            this.panelControl5.Controls.Add(this.labelControl2);
            this.panelControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl5.Location = new System.Drawing.Point(250, 22);
            this.panelControl5.Name = "panelControl5";
            this.panelControl5.Size = new System.Drawing.Size(807, 484);
            this.panelControl5.TabIndex = 0;
            // 
            // txtPurchaseRate
            // 
            this.txtPurchaseRate.EnterMoveNextControl = true;
            this.txtPurchaseRate.Location = new System.Drawing.Point(456, 6);
            this.txtPurchaseRate.Name = "txtPurchaseRate";
            this.txtPurchaseRate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPurchaseRate.Properties.Appearance.Options.UseFont = true;
            this.txtPurchaseRate.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPurchaseRate.Size = new System.Drawing.Size(187, 22);
            this.txtPurchaseRate.TabIndex = 12;
            // 
            // labelControl11
            // 
            this.labelControl11.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl11.Appearance.Options.UseFont = true;
            this.labelControl11.Location = new System.Drawing.Point(336, 9);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(95, 16);
            this.labelControl11.TabIndex = 490;
            this.labelControl11.Text = "Purchase Rate";
            // 
            // labelControl10
            // 
            this.labelControl10.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl10.Appearance.Options.UseFont = true;
            this.labelControl10.Location = new System.Drawing.Point(336, 35);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(101, 16);
            this.labelControl10.TabIndex = 488;
            this.labelControl10.Text = "Robert Kit Date";
            // 
            // dtpRobertKitDate
            // 
            this.dtpRobertKitDate.EditValue = null;
            this.dtpRobertKitDate.EnterMoveNextControl = true;
            this.dtpRobertKitDate.Location = new System.Drawing.Point(456, 32);
            this.dtpRobertKitDate.Name = "dtpRobertKitDate";
            this.dtpRobertKitDate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtpRobertKitDate.Properties.Appearance.Options.UseFont = true;
            this.dtpRobertKitDate.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtpRobertKitDate.Properties.AppearanceDropDown.Options.UseFont = true;
            this.dtpRobertKitDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpRobertKitDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpRobertKitDate.Size = new System.Drawing.Size(187, 22);
            this.dtpRobertKitDate.TabIndex = 13;
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.Location = new System.Drawing.Point(5, 395);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(95, 16);
            this.labelControl7.TabIndex = 486;
            this.labelControl7.Text = "Purchase Date";
            this.labelControl7.Visible = false;
            // 
            // dtpPurchaseDate
            // 
            this.dtpPurchaseDate.EditValue = null;
            this.dtpPurchaseDate.EnterMoveNextControl = true;
            this.dtpPurchaseDate.Location = new System.Drawing.Point(99, 392);
            this.dtpPurchaseDate.Name = "dtpPurchaseDate";
            this.dtpPurchaseDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpPurchaseDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpPurchaseDate.Size = new System.Drawing.Size(187, 20);
            this.dtpPurchaseDate.TabIndex = 16;
            this.dtpPurchaseDate.Visible = false;
            // 
            // txtMachineActivity
            // 
            this.txtMachineActivity.EnterMoveNextControl = true;
            this.txtMachineActivity.Location = new System.Drawing.Point(456, 110);
            this.txtMachineActivity.Name = "txtMachineActivity";
            this.txtMachineActivity.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMachineActivity.Properties.Appearance.Options.UseFont = true;
            this.txtMachineActivity.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMachineActivity.Size = new System.Drawing.Size(187, 22);
            this.txtMachineActivity.TabIndex = 16;
            // 
            // labelControl18
            // 
            this.labelControl18.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl18.Appearance.Options.UseFont = true;
            this.labelControl18.Location = new System.Drawing.Point(336, 113);
            this.labelControl18.Name = "labelControl18";
            this.labelControl18.Size = new System.Drawing.Size(108, 16);
            this.labelControl18.TabIndex = 484;
            this.labelControl18.Text = "Machine Activity";
            // 
            // txtElectricityPerHrs
            // 
            this.txtElectricityPerHrs.EditValue = "    ";
            this.txtElectricityPerHrs.EnterMoveNextControl = true;
            this.txtElectricityPerHrs.Location = new System.Drawing.Point(456, 84);
            this.txtElectricityPerHrs.Name = "txtElectricityPerHrs";
            this.txtElectricityPerHrs.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtElectricityPerHrs.Properties.Appearance.Options.UseFont = true;
            this.txtElectricityPerHrs.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtElectricityPerHrs.Size = new System.Drawing.Size(187, 22);
            this.txtElectricityPerHrs.TabIndex = 15;
            // 
            // labelControl20
            // 
            this.labelControl20.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl20.Appearance.Options.UseFont = true;
            this.labelControl20.Location = new System.Drawing.Point(336, 87);
            this.labelControl20.Name = "labelControl20";
            this.labelControl20.Size = new System.Drawing.Size(116, 16);
            this.labelControl20.TabIndex = 483;
            this.labelControl20.Text = "Electricity Per Hrs";
            // 
            // txtDepriciable
            // 
            this.txtDepriciable.EnterMoveNextControl = true;
            this.txtDepriciable.Location = new System.Drawing.Point(456, 58);
            this.txtDepriciable.Name = "txtDepriciable";
            this.txtDepriciable.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDepriciable.Properties.Appearance.Options.UseFont = true;
            this.txtDepriciable.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDepriciable.Size = new System.Drawing.Size(187, 22);
            this.txtDepriciable.TabIndex = 14;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(336, 61);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(98, 16);
            this.labelControl5.TabIndex = 480;
            this.labelControl5.Text = "Depriciable Life";
            // 
            // labelControl33
            // 
            this.labelControl33.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl33.Appearance.Options.UseFont = true;
            this.labelControl33.Location = new System.Drawing.Point(4, 295);
            this.labelControl33.Name = "labelControl33";
            this.labelControl33.Size = new System.Drawing.Size(108, 16);
            this.labelControl33.TabIndex = 478;
            this.labelControl33.Text = "Installation Date";
            // 
            // dtpInstallationDate
            // 
            this.dtpInstallationDate.EditValue = null;
            this.dtpInstallationDate.EnterMoveNextControl = true;
            this.dtpInstallationDate.Location = new System.Drawing.Point(137, 292);
            this.dtpInstallationDate.Name = "dtpInstallationDate";
            this.dtpInstallationDate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtpInstallationDate.Properties.Appearance.Options.UseFont = true;
            this.dtpInstallationDate.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtpInstallationDate.Properties.AppearanceDropDown.Options.UseFont = true;
            this.dtpInstallationDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpInstallationDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpInstallationDate.Size = new System.Drawing.Size(187, 22);
            this.dtpInstallationDate.TabIndex = 11;
            // 
            // labelControl31
            // 
            this.labelControl31.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl31.Appearance.Options.UseFont = true;
            this.labelControl31.Location = new System.Drawing.Point(4, 269);
            this.labelControl31.Name = "labelControl31";
            this.labelControl31.Size = new System.Drawing.Size(93, 16);
            this.labelControl31.TabIndex = 475;
            this.labelControl31.Text = "Acquired Date";
            // 
            // dtpAcquiredDate
            // 
            this.dtpAcquiredDate.EditValue = null;
            this.dtpAcquiredDate.EnterMoveNextControl = true;
            this.dtpAcquiredDate.Location = new System.Drawing.Point(137, 266);
            this.dtpAcquiredDate.Name = "dtpAcquiredDate";
            this.dtpAcquiredDate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtpAcquiredDate.Properties.Appearance.Options.UseFont = true;
            this.dtpAcquiredDate.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtpAcquiredDate.Properties.AppearanceDropDown.Options.UseFont = true;
            this.dtpAcquiredDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpAcquiredDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpAcquiredDate.Size = new System.Drawing.Size(187, 22);
            this.dtpAcquiredDate.TabIndex = 10;
            // 
            // lueMachineType
            // 
            this.lueMachineType.EnterMoveNextControl = true;
            this.lueMachineType.Location = new System.Drawing.Point(137, 240);
            this.lueMachineType.Name = "lueMachineType";
            this.lueMachineType.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueMachineType.Properties.Appearance.Options.UseFont = true;
            this.lueMachineType.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.lueMachineType.Properties.AppearanceDropDown.Options.UseFont = true;
            this.lueMachineType.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold);
            this.lueMachineType.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.lueMachineType.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.lueMachineType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueMachineType.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("machine_type_name", "Machine Type"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("machine_type_id", "Machine Type Id", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lueMachineType.Properties.NullText = "";
            this.lueMachineType.Properties.ShowHeader = false;
            this.lueMachineType.Size = new System.Drawing.Size(187, 22);
            this.lueMachineType.TabIndex = 9;
            // 
            // labelControl29
            // 
            this.labelControl29.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl29.Appearance.Options.UseFont = true;
            this.labelControl29.Location = new System.Drawing.Point(4, 243);
            this.labelControl29.Name = "labelControl29";
            this.labelControl29.Size = new System.Drawing.Size(88, 16);
            this.labelControl29.TabIndex = 472;
            this.labelControl29.Text = "Machine Type";
            // 
            // txtCategory
            // 
            this.txtCategory.EnterMoveNextControl = true;
            this.txtCategory.Location = new System.Drawing.Point(137, 213);
            this.txtCategory.Name = "txtCategory";
            this.txtCategory.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCategory.Properties.Appearance.Options.UseFont = true;
            this.txtCategory.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCategory.Size = new System.Drawing.Size(187, 22);
            this.txtCategory.TabIndex = 8;
            // 
            // labelControl25
            // 
            this.labelControl25.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl25.Appearance.Options.UseFont = true;
            this.labelControl25.Location = new System.Drawing.Point(4, 216);
            this.labelControl25.Name = "labelControl25";
            this.labelControl25.Size = new System.Drawing.Size(99, 16);
            this.labelControl25.TabIndex = 469;
            this.labelControl25.Text = "Category Name";
            // 
            // txtVendorName
            // 
            this.txtVendorName.EditValue = "    ";
            this.txtVendorName.EnterMoveNextControl = true;
            this.txtVendorName.Location = new System.Drawing.Point(137, 187);
            this.txtVendorName.Name = "txtVendorName";
            this.txtVendorName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVendorName.Properties.Appearance.Options.UseFont = true;
            this.txtVendorName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtVendorName.Size = new System.Drawing.Size(187, 22);
            this.txtVendorName.TabIndex = 7;
            // 
            // labelControl27
            // 
            this.labelControl27.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl27.Appearance.Options.UseFont = true;
            this.labelControl27.Location = new System.Drawing.Point(4, 190);
            this.labelControl27.Name = "labelControl27";
            this.labelControl27.Size = new System.Drawing.Size(128, 16);
            this.labelControl27.TabIndex = 467;
            this.labelControl27.Text = "Pur. Company Name";
            // 
            // lueGroup
            // 
            this.lueGroup.EnterMoveNextControl = true;
            this.lueGroup.Location = new System.Drawing.Point(137, 136);
            this.lueGroup.Name = "lueGroup";
            this.lueGroup.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueGroup.Properties.Appearance.Options.UseFont = true;
            this.lueGroup.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.lueGroup.Properties.AppearanceDropDown.Options.UseFont = true;
            this.lueGroup.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold);
            this.lueGroup.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.lueGroup.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.lueGroup.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueGroup.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("group_name", "Group Name"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("group_id", "Group Id", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lueGroup.Properties.NullText = "";
            this.lueGroup.Properties.ShowHeader = false;
            this.lueGroup.Size = new System.Drawing.Size(187, 22);
            this.lueGroup.TabIndex = 5;
            // 
            // labelControl23
            // 
            this.labelControl23.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl23.Appearance.Options.UseFont = true;
            this.labelControl23.Location = new System.Drawing.Point(4, 139);
            this.labelControl23.Name = "labelControl23";
            this.labelControl23.Size = new System.Drawing.Size(38, 16);
            this.labelControl23.TabIndex = 463;
            this.labelControl23.Text = "Group";
            // 
            // lueTeam
            // 
            this.lueTeam.EnterMoveNextControl = true;
            this.lueTeam.Location = new System.Drawing.Point(137, 110);
            this.lueTeam.Name = "lueTeam";
            this.lueTeam.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueTeam.Properties.Appearance.Options.UseFont = true;
            this.lueTeam.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.lueTeam.Properties.AppearanceDropDown.Options.UseFont = true;
            this.lueTeam.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold);
            this.lueTeam.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.lueTeam.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.lueTeam.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueTeam.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("team_name", "Team Name"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("team_id", "Team Id", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lueTeam.Properties.NullText = "";
            this.lueTeam.Properties.ShowHeader = false;
            this.lueTeam.Size = new System.Drawing.Size(187, 22);
            this.lueTeam.TabIndex = 4;
            // 
            // labelControl21
            // 
            this.labelControl21.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl21.Appearance.Options.UseFont = true;
            this.labelControl21.Location = new System.Drawing.Point(5, 113);
            this.labelControl21.Name = "labelControl21";
            this.labelControl21.Size = new System.Drawing.Size(34, 16);
            this.labelControl21.TabIndex = 460;
            this.labelControl21.Text = "Team";
            // 
            // lueProcess
            // 
            this.lueProcess.EnterMoveNextControl = true;
            this.lueProcess.Location = new System.Drawing.Point(137, 162);
            this.lueProcess.Name = "lueProcess";
            this.lueProcess.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueProcess.Properties.Appearance.Options.UseFont = true;
            this.lueProcess.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.lueProcess.Properties.AppearanceDropDown.Options.UseFont = true;
            this.lueProcess.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold);
            this.lueProcess.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.lueProcess.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.lueProcess.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueProcess.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("process_name", "Process Name"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("process_id", "Process Id", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lueProcess.Properties.NullText = "";
            this.lueProcess.Properties.ShowHeader = false;
            this.lueProcess.Size = new System.Drawing.Size(187, 22);
            this.lueProcess.TabIndex = 6;
            // 
            // labelControl19
            // 
            this.labelControl19.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl19.Appearance.Options.UseFont = true;
            this.labelControl19.Location = new System.Drawing.Point(4, 165);
            this.labelControl19.Name = "labelControl19";
            this.labelControl19.Size = new System.Drawing.Size(51, 16);
            this.labelControl19.TabIndex = 457;
            this.labelControl19.Text = "Process";
            // 
            // lueDepartment
            // 
            this.lueDepartment.EnterMoveNextControl = true;
            this.lueDepartment.Location = new System.Drawing.Point(456, 214);
            this.lueDepartment.Name = "lueDepartment";
            this.lueDepartment.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueDepartment.Properties.Appearance.Options.UseFont = true;
            this.lueDepartment.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.lueDepartment.Properties.AppearanceDropDown.Options.UseFont = true;
            this.lueDepartment.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold);
            this.lueDepartment.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.lueDepartment.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.lueDepartment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueDepartment.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("department_name", "Department Name"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("department_id", "Department Id", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lueDepartment.Properties.NullText = "";
            this.lueDepartment.Properties.ShowHeader = false;
            this.lueDepartment.Size = new System.Drawing.Size(187, 22);
            this.lueDepartment.TabIndex = 20;
            // 
            // labelControl14
            // 
            this.labelControl14.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl14.Appearance.Options.UseFont = true;
            this.labelControl14.Location = new System.Drawing.Point(336, 217);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(78, 16);
            this.labelControl14.TabIndex = 454;
            this.labelControl14.Text = "Department";
            // 
            // lueLocation
            // 
            this.lueLocation.EnterMoveNextControl = true;
            this.lueLocation.Location = new System.Drawing.Point(456, 188);
            this.lueLocation.Name = "lueLocation";
            this.lueLocation.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueLocation.Properties.Appearance.Options.UseFont = true;
            this.lueLocation.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.lueLocation.Properties.AppearanceDropDown.Options.UseFont = true;
            this.lueLocation.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold);
            this.lueLocation.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.lueLocation.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.lueLocation.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueLocation.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("location_name", "Location Name"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("location_id", "Location Id", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lueLocation.Properties.NullText = "";
            this.lueLocation.Properties.ShowHeader = false;
            this.lueLocation.Size = new System.Drawing.Size(187, 22);
            this.lueLocation.TabIndex = 19;
            // 
            // labelControl12
            // 
            this.labelControl12.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl12.Appearance.Options.UseFont = true;
            this.labelControl12.Location = new System.Drawing.Point(336, 191);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(55, 16);
            this.labelControl12.TabIndex = 452;
            this.labelControl12.Text = "Location";
            // 
            // lueBranch
            // 
            this.lueBranch.EnterMoveNextControl = true;
            this.lueBranch.Location = new System.Drawing.Point(456, 162);
            this.lueBranch.Name = "lueBranch";
            this.lueBranch.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueBranch.Properties.Appearance.Options.UseFont = true;
            this.lueBranch.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.lueBranch.Properties.AppearanceDropDown.Options.UseFont = true;
            this.lueBranch.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold);
            this.lueBranch.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.lueBranch.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.lueBranch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueBranch.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("branch_name", "Branch Name"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("branch_id", "Branch Id", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lueBranch.Properties.NullText = "";
            this.lueBranch.Properties.ShowHeader = false;
            this.lueBranch.Size = new System.Drawing.Size(187, 22);
            this.lueBranch.TabIndex = 18;
            // 
            // labelControl16
            // 
            this.labelControl16.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl16.Appearance.Options.UseFont = true;
            this.labelControl16.Location = new System.Drawing.Point(336, 165);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(45, 16);
            this.labelControl16.TabIndex = 449;
            this.labelControl16.Text = "Branch";
            // 
            // lueCompany
            // 
            this.lueCompany.EnterMoveNextControl = true;
            this.lueCompany.Location = new System.Drawing.Point(456, 136);
            this.lueCompany.Name = "lueCompany";
            this.lueCompany.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueCompany.Properties.Appearance.Options.UseFont = true;
            this.lueCompany.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.lueCompany.Properties.AppearanceDropDown.Options.UseFont = true;
            this.lueCompany.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold);
            this.lueCompany.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.lueCompany.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.lueCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueCompany.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("company_name", "Company Name"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("company_id", "Company Id", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lueCompany.Properties.NullText = "";
            this.lueCompany.Properties.ShowHeader = false;
            this.lueCompany.Size = new System.Drawing.Size(187, 22);
            this.lueCompany.TabIndex = 17;
            // 
            // labelControl17
            // 
            this.labelControl17.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl17.Appearance.Options.UseFont = true;
            this.labelControl17.Location = new System.Drawing.Point(336, 139);
            this.labelControl17.Name = "labelControl17";
            this.labelControl17.Size = new System.Drawing.Size(59, 16);
            this.labelControl17.TabIndex = 448;
            this.labelControl17.Text = "Company";
            // 
            // txtSerialNo
            // 
            this.txtSerialNo.EnterMoveNextControl = true;
            this.txtSerialNo.Location = new System.Drawing.Point(137, 84);
            this.txtSerialNo.Name = "txtSerialNo";
            this.txtSerialNo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSerialNo.Properties.Appearance.Options.UseFont = true;
            this.txtSerialNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSerialNo.Size = new System.Drawing.Size(187, 22);
            this.txtSerialNo.TabIndex = 3;
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Location = new System.Drawing.Point(4, 87);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(56, 16);
            this.labelControl6.TabIndex = 434;
            this.labelControl6.Text = "Serial No";
            // 
            // txtModelNo
            // 
            this.txtModelNo.EditValue = "    ";
            this.txtModelNo.EnterMoveNextControl = true;
            this.txtModelNo.Location = new System.Drawing.Point(137, 58);
            this.txtModelNo.Name = "txtModelNo";
            this.txtModelNo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtModelNo.Properties.Appearance.Options.UseFont = true;
            this.txtModelNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtModelNo.Size = new System.Drawing.Size(187, 22);
            this.txtModelNo.TabIndex = 2;
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl9.Appearance.Options.UseFont = true;
            this.labelControl9.Location = new System.Drawing.Point(4, 61);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(58, 16);
            this.labelControl9.TabIndex = 432;
            this.labelControl9.Text = "Model No";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Appearance.Options.UseForeColor = true;
            this.labelControl3.Location = new System.Drawing.Point(128, 34);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(6, 13);
            this.labelControl3.TabIndex = 429;
            this.labelControl3.Text = "*";
            // 
            // txtManualCode
            // 
            this.txtManualCode.EnterMoveNextControl = true;
            this.txtManualCode.Location = new System.Drawing.Point(137, 32);
            this.txtManualCode.Name = "txtManualCode";
            this.txtManualCode.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtManualCode.Properties.Appearance.Options.UseFont = true;
            this.txtManualCode.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtManualCode.Size = new System.Drawing.Size(187, 22);
            this.txtManualCode.TabIndex = 1;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(4, 35);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(82, 16);
            this.labelControl4.TabIndex = 428;
            this.labelControl4.Text = "Manual Code";
            // 
            // txtRemark
            // 
            this.txtRemark.EditValue = "";
            this.txtRemark.EnterMoveNextControl = true;
            this.txtRemark.Location = new System.Drawing.Point(456, 240);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemark.Properties.Appearance.Options.UseFont = true;
            this.txtRemark.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRemark.Size = new System.Drawing.Size(187, 49);
            this.txtRemark.TabIndex = 21;
            // 
            // chkActive
            // 
            this.chkActive.EditValue = true;
            this.chkActive.EnterMoveNextControl = true;
            this.chkActive.Location = new System.Drawing.Point(649, 7);
            this.chkActive.Name = "chkActive";
            this.chkActive.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkActive.Properties.Appearance.Options.UseFont = true;
            this.chkActive.Properties.Caption = "Active";
            this.chkActive.Size = new System.Drawing.Size(68, 20);
            this.chkActive.TabIndex = 22;
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl8.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl8.Appearance.Options.UseFont = true;
            this.labelControl8.Appearance.Options.UseForeColor = true;
            this.labelControl8.Location = new System.Drawing.Point(128, 8);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(6, 13);
            this.labelControl8.TabIndex = 426;
            this.labelControl8.Text = "*";
            // 
            // txtMachineName
            // 
            this.txtMachineName.EditValue = "    ";
            this.txtMachineName.EnterMoveNextControl = true;
            this.txtMachineName.Location = new System.Drawing.Point(137, 6);
            this.txtMachineName.Name = "txtMachineName";
            this.txtMachineName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMachineName.Properties.Appearance.Options.UseFont = true;
            this.txtMachineName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMachineName.Size = new System.Drawing.Size(187, 22);
            this.txtMachineName.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(4, 9);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(92, 16);
            this.labelControl1.TabIndex = 13;
            this.labelControl1.Text = "Machine Name";
            // 
            // panelControl6
            // 
            this.panelControl6.Controls.Add(this.lblMode);
            this.panelControl6.Controls.Add(this.btnExit);
            this.panelControl6.Controls.Add(this.btnClear);
            this.panelControl6.Controls.Add(this.btnSave);
            this.panelControl6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl6.Location = new System.Drawing.Point(2, 434);
            this.panelControl6.Name = "panelControl6";
            this.panelControl6.Size = new System.Drawing.Size(803, 48);
            this.panelControl6.TabIndex = 23;
            // 
            // lblMode
            // 
            this.lblMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMode.Appearance.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMode.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblMode.Appearance.Options.UseFont = true;
            this.lblMode.Appearance.Options.UseForeColor = true;
            this.lblMode.Location = new System.Drawing.Point(377, 16);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(71, 16);
            this.lblMode.TabIndex = 430;
            this.lblMode.Text = "Add Mode";
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Appearance.Options.UseFont = true;
            this.btnExit.ImageOptions.Image = global::DERP.Properties.Resources.Exit;
            this.btnExit.Location = new System.Drawing.Point(690, 9);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(102, 32);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "E&xit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Appearance.Options.UseFont = true;
            this.btnClear.ImageOptions.Image = global::DERP.Properties.Resources.Clear;
            this.btnClear.Location = new System.Drawing.Point(582, 9);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(102, 32);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "&Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.ImageOptions.Image = global::DERP.Properties.Resources.Save;
            this.btnSave.Location = new System.Drawing.Point(474, 9);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(102, 32);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(336, 243);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(50, 16);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Remark";
            // 
            // panelControl1
            // 
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(250, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(807, 22);
            this.panelControl1.TabIndex = 18;
            // 
            // panelControl4
            // 
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl4.Location = new System.Drawing.Point(250, 506);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(807, 11);
            this.panelControl4.TabIndex = 16;
            // 
            // panelControl3
            // 
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl3.Location = new System.Drawing.Point(1057, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(11, 517);
            this.panelControl3.TabIndex = 15;
            // 
            // panelControl2
            // 
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl2.Location = new System.Drawing.Point(239, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(11, 517);
            this.panelControl2.TabIndex = 14;
            // 
            // chkIsMachine
            // 
            this.chkIsMachine.EditValue = true;
            this.chkIsMachine.EnterMoveNextControl = true;
            this.chkIsMachine.Location = new System.Drawing.Point(649, 31);
            this.chkIsMachine.Name = "chkIsMachine";
            this.chkIsMachine.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIsMachine.Properties.Appearance.Options.UseFont = true;
            this.chkIsMachine.Properties.Caption = "Is Machine";
            this.chkIsMachine.Size = new System.Drawing.Size(99, 20);
            this.chkIsMachine.TabIndex = 491;
            // 
            // FrmMfgMachineMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1068, 517);
            this.Controls.Add(this.panelControl5);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panelControl4);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.dockPanel1);
            this.Name = "FrmMfgMachineMaster";
            this.Text = "Machine Master";
            this.Load += new System.EventHandler(this.FrmMfgTensionTypeMaster_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdMachineMaster)).EndInit();
            this.ContextMNExport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMachineMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).EndInit();
            this.panelControl5.ResumeLayout(false);
            this.panelControl5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPurchaseRate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpRobertKitDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpRobertKitDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpPurchaseDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpPurchaseDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMachineActivity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtElectricityPerHrs.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDepriciable.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpInstallationDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpInstallationDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpAcquiredDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpAcquiredDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueMachineType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCategory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVendorName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueTeam.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueProcess.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueDepartment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueLocation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueBranch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSerialNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtModelNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtManualCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkActive.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMachineName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).EndInit();
            this.panelControl6.ResumeLayout(false);
            this.panelControl6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsMachine.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraGrid.GridControl grdMachineMaster;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvMachineMaster;
        private DevExpress.XtraGrid.Columns.GridColumn clmMachineId;
        private DevExpress.XtraGrid.Columns.GridColumn clmMachineName;
        private DevExpress.XtraGrid.Columns.GridColumn clmActive;
        private DevExpress.XtraGrid.Columns.GridColumn clmRemark;
        private DevExpress.XtraEditors.PanelControl panelControl5;
        private DevExpress.XtraEditors.CheckEdit chkActive;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.TextEdit txtMachineName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl6;
        private DevExpress.XtraEditors.LabelControl lblMode;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.MemoEdit txtRemark;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtManualCode;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraGrid.Columns.GridColumn clmManualCode;
        private DevExpress.XtraEditors.TextEdit txtSerialNo;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtModelNo;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LookUpEdit lueDepartment;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraEditors.LookUpEdit lueLocation;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.LookUpEdit lueBranch;
        private DevExpress.XtraEditors.LabelControl labelControl16;
        private DevExpress.XtraEditors.LookUpEdit lueCompany;
        private DevExpress.XtraEditors.LabelControl labelControl17;
        private DevExpress.XtraEditors.TextEdit txtCategory;
        private DevExpress.XtraEditors.LabelControl labelControl25;
        private DevExpress.XtraEditors.TextEdit txtVendorName;
        private DevExpress.XtraEditors.LabelControl labelControl27;
        private DevExpress.XtraEditors.LookUpEdit lueGroup;
        private DevExpress.XtraEditors.LabelControl labelControl23;
        private DevExpress.XtraEditors.LookUpEdit lueTeam;
        private DevExpress.XtraEditors.LabelControl labelControl21;
        private DevExpress.XtraEditors.LookUpEdit lueProcess;
        private DevExpress.XtraEditors.LabelControl labelControl19;
        private DevExpress.XtraEditors.LookUpEdit lueMachineType;
        private DevExpress.XtraEditors.LabelControl labelControl29;
        private DevExpress.XtraEditors.LabelControl labelControl33;
        private DevExpress.XtraEditors.DateEdit dtpInstallationDate;
        private DevExpress.XtraEditors.LabelControl labelControl31;
        private DevExpress.XtraEditors.DateEdit dtpAcquiredDate;
        private DevExpress.XtraEditors.TextEdit txtMachineActivity;
        private DevExpress.XtraEditors.LabelControl labelControl18;
        private DevExpress.XtraEditors.TextEdit txtElectricityPerHrs;
        private DevExpress.XtraEditors.LabelControl labelControl20;
        private DevExpress.XtraEditors.TextEdit txtDepriciable;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraGrid.Columns.GridColumn clmModelNo;
        private DevExpress.XtraGrid.Columns.GridColumn clmSerialNo;
        private DevExpress.XtraGrid.Columns.GridColumn clmCompany;
        private DevExpress.XtraGrid.Columns.GridColumn clmBranch;
        private DevExpress.XtraGrid.Columns.GridColumn clmLocation;
        private DevExpress.XtraGrid.Columns.GridColumn clmDepartment;
        private DevExpress.XtraGrid.Columns.GridColumn clmMachineType;
        private DevExpress.XtraGrid.Columns.GridColumn clmTeam;
        private DevExpress.XtraGrid.Columns.GridColumn clmGroup;
        private DevExpress.XtraGrid.Columns.GridColumn clmProcess;
        private DevExpress.XtraGrid.Columns.GridColumn clmVendorName;
        private DevExpress.XtraGrid.Columns.GridColumn clmCategory;
        private DevExpress.XtraGrid.Columns.GridColumn clmAcquiredDate;
        private DevExpress.XtraGrid.Columns.GridColumn clmInstalldate;
        private DevExpress.XtraGrid.Columns.GridColumn clmdepriciableLife;
        private DevExpress.XtraGrid.Columns.GridColumn clmElectricityPerHrs;
        private DevExpress.XtraGrid.Columns.GridColumn clmMachineActivity;
        private DevExpress.XtraGrid.Columns.GridColumn clmCompanyId;
        private DevExpress.XtraGrid.Columns.GridColumn clmBranchId;
        private DevExpress.XtraGrid.Columns.GridColumn clmLocationId;
        private DevExpress.XtraGrid.Columns.GridColumn clmDepartmentId;
        private DevExpress.XtraGrid.Columns.GridColumn clmTeamId;
        private DevExpress.XtraGrid.Columns.GridColumn clmGroupId;
        private DevExpress.XtraGrid.Columns.GridColumn clmProcessId;
        private DevExpress.XtraGrid.Columns.GridColumn clmMachineTypeId;
        private System.Windows.Forms.ContextMenuStrip ContextMNExport;
        private System.Windows.Forms.ToolStripMenuItem MNExportExcel;
        private System.Windows.Forms.ToolStripMenuItem MNExportPDF;
        private System.Windows.Forms.ToolStripMenuItem MNExportTEXT;
        private System.Windows.Forms.ToolStripMenuItem MNExportHTML;
        private System.Windows.Forms.ToolStripMenuItem MNExportRTF;
        private System.Windows.Forms.ToolStripMenuItem MNExportCSV;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.DateEdit dtpRobertKitDate;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.DateEdit dtpPurchaseDate;
        private DevExpress.XtraGrid.Columns.GridColumn clmPurchaseDate;
        private DevExpress.XtraGrid.Columns.GridColumn clmRobertKitDate;
        private DevExpress.XtraEditors.TextEdit txtPurchaseRate;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraGrid.Columns.GridColumn clmPurchaseRate;
        private DevExpress.XtraEditors.CheckEdit chkIsMachine;
    }
}