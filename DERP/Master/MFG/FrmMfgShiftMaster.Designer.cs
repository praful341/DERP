namespace DERP.Master.MFG
{
    partial class FrmMfgShiftMaster
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
            this.grdShiftMaster = new DevExpress.XtraGrid.GridControl();
            this.ContextMNExport = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MNExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportPDF = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportTEXT = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportHTML = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportRTF = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportCSV = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvShiftMaster = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.clmShiftId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmShiftName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmSftStartTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmSftEndTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmPnchStartTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmPnchEndTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmLnchStartTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmLunchEndTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmHfDayAfter = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmHfDayBefore = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmGraceTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmIntvStartTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmIntvEndTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmNextShift = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmTtlShiftHrs = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmShiftType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmNxtShiftId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmActive = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl5 = new DevExpress.XtraEditors.PanelControl();
            this.lueNextShift = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl24 = new DevExpress.XtraEditors.LabelControl();
            this.txtShiftType = new DevExpress.XtraEditors.TextEdit();
            this.labelControl23 = new DevExpress.XtraEditors.LabelControl();
            this.txtTotalShiftHrs = new DevExpress.XtraEditors.TextEdit();
            this.labelControl22 = new DevExpress.XtraEditors.LabelControl();
            this.txtGraceTime = new DevExpress.XtraEditors.TextEdit();
            this.labelControl21 = new DevExpress.XtraEditors.LabelControl();
            this.txtIntervalEndTime = new DevExpress.XtraEditors.TextEdit();
            this.labelControl18 = new DevExpress.XtraEditors.LabelControl();
            this.txtIntervalStartTime = new DevExpress.XtraEditors.TextEdit();
            this.labelControl20 = new DevExpress.XtraEditors.LabelControl();
            this.txtHalfDayBefore = new DevExpress.XtraEditors.TextEdit();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.txtHalfDayAfter = new DevExpress.XtraEditors.TextEdit();
            this.labelControl17 = new DevExpress.XtraEditors.LabelControl();
            this.txtLunchEndTime = new DevExpress.XtraEditors.TextEdit();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.txtLunchStartTime = new DevExpress.XtraEditors.TextEdit();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.txtPunchEndTime = new DevExpress.XtraEditors.TextEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.txtPunchStartTime = new DevExpress.XtraEditors.TextEdit();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.txtShiftEndTime = new DevExpress.XtraEditors.TextEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtShiftStartTime = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtRemark = new DevExpress.XtraEditors.MemoEdit();
            this.chkActive = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.txtShiftName = new DevExpress.XtraEditors.TextEdit();
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
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdShiftMaster)).BeginInit();
            this.ContextMNExport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShiftMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).BeginInit();
            this.panelControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueNextShift.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShiftType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalShiftHrs.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGraceTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIntervalEndTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIntervalStartTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHalfDayBefore.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHalfDayAfter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLunchEndTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLunchStartTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPunchEndTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPunchStartTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShiftEndTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShiftStartTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkActive.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShiftName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).BeginInit();
            this.panelControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
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
            this.dockPanel1.Size = new System.Drawing.Size(239, 472);
            this.dockPanel1.Text = "Shift Master";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.grdShiftMaster);
            this.dockPanel1_Container.Location = new System.Drawing.Point(4, 23);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(230, 445);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // grdShiftMaster
            // 
            this.grdShiftMaster.ContextMenuStrip = this.ContextMNExport;
            this.grdShiftMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdShiftMaster.Location = new System.Drawing.Point(0, 0);
            this.grdShiftMaster.MainView = this.dgvShiftMaster;
            this.grdShiftMaster.Name = "grdShiftMaster";
            this.grdShiftMaster.Size = new System.Drawing.Size(230, 445);
            this.grdShiftMaster.TabIndex = 15;
            this.grdShiftMaster.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvShiftMaster});
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
            // dgvShiftMaster
            // 
            this.dgvShiftMaster.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.dgvShiftMaster.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.dgvShiftMaster.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            this.dgvShiftMaster.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            this.dgvShiftMaster.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.dgvShiftMaster.Appearance.FooterPanel.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvShiftMaster.Appearance.FooterPanel.Options.UseFont = true;
            this.dgvShiftMaster.Appearance.HeaderPanel.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvShiftMaster.Appearance.HeaderPanel.Options.UseFont = true;
            this.dgvShiftMaster.Appearance.Row.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvShiftMaster.Appearance.Row.Options.UseFont = true;
            this.dgvShiftMaster.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.clmShiftId,
            this.clmShiftName,
            this.clmSftStartTime,
            this.clmSftEndTime,
            this.clmPnchStartTime,
            this.clmPnchEndTime,
            this.clmLnchStartTime,
            this.clmLunchEndTime,
            this.clmHfDayAfter,
            this.clmHfDayBefore,
            this.clmGraceTime,
            this.clmIntvStartTime,
            this.clmIntvEndTime,
            this.clmNextShift,
            this.clmTtlShiftHrs,
            this.clmShiftType,
            this.clmNxtShiftId,
            this.clmRemark,
            this.clmActive});
            this.dgvShiftMaster.GridControl = this.grdShiftMaster;
            this.dgvShiftMaster.Name = "dgvShiftMaster";
            this.dgvShiftMaster.OptionsBehavior.Editable = false;
            this.dgvShiftMaster.OptionsBehavior.ReadOnly = true;
            this.dgvShiftMaster.OptionsCustomization.AllowQuickHideColumns = false;
            this.dgvShiftMaster.OptionsView.ColumnAutoWidth = false;
            this.dgvShiftMaster.OptionsView.ShowAutoFilterRow = true;
            this.dgvShiftMaster.OptionsView.ShowFooter = true;
            this.dgvShiftMaster.OptionsView.ShowGroupPanel = false;
            this.dgvShiftMaster.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.dgvMfgShiftMaster_RowClick);
            // 
            // clmShiftId
            // 
            this.clmShiftId.Caption = "Tension Type Id";
            this.clmShiftId.FieldName = "shift_id";
            this.clmShiftId.Name = "clmShiftId";
            this.clmShiftId.OptionsColumn.AllowEdit = false;
            // 
            // clmShiftName
            // 
            this.clmShiftName.Caption = "Shift Name";
            this.clmShiftName.FieldName = "shift_name";
            this.clmShiftName.Name = "clmShiftName";
            this.clmShiftName.OptionsColumn.AllowEdit = false;
            this.clmShiftName.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.clmShiftName.Visible = true;
            this.clmShiftName.VisibleIndex = 0;
            // 
            // clmSftStartTime
            // 
            this.clmSftStartTime.Caption = "Shift Start Time";
            this.clmSftStartTime.FieldName = "start_time";
            this.clmSftStartTime.Name = "clmSftStartTime";
            this.clmSftStartTime.Visible = true;
            this.clmSftStartTime.VisibleIndex = 1;
            // 
            // clmSftEndTime
            // 
            this.clmSftEndTime.Caption = "Shift End Time";
            this.clmSftEndTime.FieldName = "end_time";
            this.clmSftEndTime.Name = "clmSftEndTime";
            this.clmSftEndTime.Visible = true;
            this.clmSftEndTime.VisibleIndex = 2;
            // 
            // clmPnchStartTime
            // 
            this.clmPnchStartTime.Caption = "Punch Start Time";
            this.clmPnchStartTime.FieldName = "punch_start_time";
            this.clmPnchStartTime.Name = "clmPnchStartTime";
            this.clmPnchStartTime.Visible = true;
            this.clmPnchStartTime.VisibleIndex = 3;
            // 
            // clmPnchEndTime
            // 
            this.clmPnchEndTime.Caption = "Punch End Time";
            this.clmPnchEndTime.FieldName = "punch_end_time";
            this.clmPnchEndTime.Name = "clmPnchEndTime";
            this.clmPnchEndTime.Visible = true;
            this.clmPnchEndTime.VisibleIndex = 4;
            // 
            // clmLnchStartTime
            // 
            this.clmLnchStartTime.Caption = "Lunch Start Time";
            this.clmLnchStartTime.FieldName = "lunch_start_time";
            this.clmLnchStartTime.Name = "clmLnchStartTime";
            this.clmLnchStartTime.Visible = true;
            this.clmLnchStartTime.VisibleIndex = 5;
            // 
            // clmLunchEndTime
            // 
            this.clmLunchEndTime.Caption = "Lunch End Time";
            this.clmLunchEndTime.FieldName = "lunch_end_time";
            this.clmLunchEndTime.Name = "clmLunchEndTime";
            this.clmLunchEndTime.Visible = true;
            this.clmLunchEndTime.VisibleIndex = 6;
            // 
            // clmHfDayAfter
            // 
            this.clmHfDayAfter.Caption = "Half Day After";
            this.clmHfDayAfter.FieldName = "half_day_after";
            this.clmHfDayAfter.Name = "clmHfDayAfter";
            this.clmHfDayAfter.Visible = true;
            this.clmHfDayAfter.VisibleIndex = 7;
            // 
            // clmHfDayBefore
            // 
            this.clmHfDayBefore.Caption = "Half Day Before";
            this.clmHfDayBefore.FieldName = "half_day_before";
            this.clmHfDayBefore.Name = "clmHfDayBefore";
            this.clmHfDayBefore.Visible = true;
            this.clmHfDayBefore.VisibleIndex = 8;
            // 
            // clmGraceTime
            // 
            this.clmGraceTime.Caption = "Grace Time";
            this.clmGraceTime.FieldName = "grace_time";
            this.clmGraceTime.Name = "clmGraceTime";
            this.clmGraceTime.Visible = true;
            this.clmGraceTime.VisibleIndex = 9;
            // 
            // clmIntvStartTime
            // 
            this.clmIntvStartTime.Caption = "Interval Start Time";
            this.clmIntvStartTime.FieldName = "start_time_interval";
            this.clmIntvStartTime.Name = "clmIntvStartTime";
            this.clmIntvStartTime.Visible = true;
            this.clmIntvStartTime.VisibleIndex = 10;
            // 
            // clmIntvEndTime
            // 
            this.clmIntvEndTime.Caption = "Interval End Time";
            this.clmIntvEndTime.FieldName = "end_time_interval";
            this.clmIntvEndTime.Name = "clmIntvEndTime";
            this.clmIntvEndTime.Visible = true;
            this.clmIntvEndTime.VisibleIndex = 11;
            // 
            // clmNextShift
            // 
            this.clmNextShift.Caption = "Next Shift";
            this.clmNextShift.FieldName = "next_shift";
            this.clmNextShift.Name = "clmNextShift";
            this.clmNextShift.Visible = true;
            this.clmNextShift.VisibleIndex = 12;
            // 
            // clmTtlShiftHrs
            // 
            this.clmTtlShiftHrs.Caption = "Shift Hrs";
            this.clmTtlShiftHrs.FieldName = "total_shift_hrs";
            this.clmTtlShiftHrs.Name = "clmTtlShiftHrs";
            this.clmTtlShiftHrs.Visible = true;
            this.clmTtlShiftHrs.VisibleIndex = 13;
            // 
            // clmShiftType
            // 
            this.clmShiftType.Caption = "Shift Type";
            this.clmShiftType.FieldName = "shift_type";
            this.clmShiftType.Name = "clmShiftType";
            this.clmShiftType.Visible = true;
            this.clmShiftType.VisibleIndex = 14;
            // 
            // clmNxtShiftId
            // 
            this.clmNxtShiftId.Caption = "Next Shift Id";
            this.clmNxtShiftId.FieldName = "next_shift_id";
            this.clmNxtShiftId.Name = "clmNxtShiftId";
            // 
            // clmRemark
            // 
            this.clmRemark.Caption = "Remark";
            this.clmRemark.FieldName = "remarks";
            this.clmRemark.Name = "clmRemark";
            this.clmRemark.OptionsColumn.AllowEdit = false;
            this.clmRemark.Visible = true;
            this.clmRemark.VisibleIndex = 15;
            // 
            // clmActive
            // 
            this.clmActive.Caption = "Active";
            this.clmActive.FieldName = "active";
            this.clmActive.Name = "clmActive";
            this.clmActive.OptionsColumn.AllowEdit = false;
            this.clmActive.Visible = true;
            this.clmActive.VisibleIndex = 16;
            // 
            // panelControl5
            // 
            this.panelControl5.Controls.Add(this.lueNextShift);
            this.panelControl5.Controls.Add(this.labelControl24);
            this.panelControl5.Controls.Add(this.txtShiftType);
            this.panelControl5.Controls.Add(this.labelControl23);
            this.panelControl5.Controls.Add(this.txtTotalShiftHrs);
            this.panelControl5.Controls.Add(this.labelControl22);
            this.panelControl5.Controls.Add(this.txtGraceTime);
            this.panelControl5.Controls.Add(this.labelControl21);
            this.panelControl5.Controls.Add(this.txtIntervalEndTime);
            this.panelControl5.Controls.Add(this.labelControl18);
            this.panelControl5.Controls.Add(this.txtIntervalStartTime);
            this.panelControl5.Controls.Add(this.labelControl20);
            this.panelControl5.Controls.Add(this.txtHalfDayBefore);
            this.panelControl5.Controls.Add(this.labelControl15);
            this.panelControl5.Controls.Add(this.labelControl16);
            this.panelControl5.Controls.Add(this.txtHalfDayAfter);
            this.panelControl5.Controls.Add(this.labelControl17);
            this.panelControl5.Controls.Add(this.txtLunchEndTime);
            this.panelControl5.Controls.Add(this.labelControl12);
            this.panelControl5.Controls.Add(this.txtLunchStartTime);
            this.panelControl5.Controls.Add(this.labelControl14);
            this.panelControl5.Controls.Add(this.txtPunchEndTime);
            this.panelControl5.Controls.Add(this.labelControl9);
            this.panelControl5.Controls.Add(this.txtPunchStartTime);
            this.panelControl5.Controls.Add(this.labelControl11);
            this.panelControl5.Controls.Add(this.txtShiftEndTime);
            this.panelControl5.Controls.Add(this.labelControl7);
            this.panelControl5.Controls.Add(this.labelControl5);
            this.panelControl5.Controls.Add(this.txtShiftStartTime);
            this.panelControl5.Controls.Add(this.labelControl6);
            this.panelControl5.Controls.Add(this.txtRemark);
            this.panelControl5.Controls.Add(this.chkActive);
            this.panelControl5.Controls.Add(this.labelControl8);
            this.panelControl5.Controls.Add(this.txtShiftName);
            this.panelControl5.Controls.Add(this.labelControl1);
            this.panelControl5.Controls.Add(this.panelControl6);
            this.panelControl5.Controls.Add(this.labelControl2);
            this.panelControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl5.Location = new System.Drawing.Point(250, 22);
            this.panelControl5.Name = "panelControl5";
            this.panelControl5.Size = new System.Drawing.Size(652, 439);
            this.panelControl5.TabIndex = 2;
            // 
            // lueNextShift
            // 
            this.lueNextShift.EnterMoveNextControl = true;
            this.lueNextShift.Location = new System.Drawing.Point(107, 188);
            this.lueNextShift.Name = "lueNextShift";
            this.lueNextShift.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueNextShift.Properties.Appearance.Options.UseFont = true;
            this.lueNextShift.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold);
            this.lueNextShift.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.lueNextShift.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.lueNextShift.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueNextShift.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("shift_name", "Shift Name"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("shift_id", "Shift Id", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lueNextShift.Properties.NullText = "";
            this.lueNextShift.Properties.ShowHeader = false;
            this.lueNextShift.Size = new System.Drawing.Size(187, 20);
            this.lueNextShift.TabIndex = 12;
            // 
            // labelControl24
            // 
            this.labelControl24.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl24.Appearance.Options.UseFont = true;
            this.labelControl24.Location = new System.Drawing.Point(6, 191);
            this.labelControl24.Name = "labelControl24";
            this.labelControl24.Size = new System.Drawing.Size(48, 13);
            this.labelControl24.TabIndex = 462;
            this.labelControl24.Text = "Next Shift";
            // 
            // txtShiftType
            // 
            this.txtShiftType.EnterMoveNextControl = true;
            this.txtShiftType.Location = new System.Drawing.Point(107, 240);
            this.txtShiftType.Name = "txtShiftType";
            this.txtShiftType.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtShiftType.Properties.Appearance.Options.UseFont = true;
            this.txtShiftType.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtShiftType.Size = new System.Drawing.Size(187, 20);
            this.txtShiftType.TabIndex = 14;
            // 
            // labelControl23
            // 
            this.labelControl23.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl23.Appearance.Options.UseFont = true;
            this.labelControl23.Location = new System.Drawing.Point(6, 243);
            this.labelControl23.Name = "labelControl23";
            this.labelControl23.Size = new System.Drawing.Size(49, 13);
            this.labelControl23.TabIndex = 460;
            this.labelControl23.Text = "Shift Type";
            // 
            // txtTotalShiftHrs
            // 
            this.txtTotalShiftHrs.EnterMoveNextControl = true;
            this.txtTotalShiftHrs.Location = new System.Drawing.Point(107, 214);
            this.txtTotalShiftHrs.Name = "txtTotalShiftHrs";
            this.txtTotalShiftHrs.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalShiftHrs.Properties.Appearance.Options.UseFont = true;
            this.txtTotalShiftHrs.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTotalShiftHrs.Size = new System.Drawing.Size(187, 20);
            this.txtTotalShiftHrs.TabIndex = 13;
            // 
            // labelControl22
            // 
            this.labelControl22.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl22.Appearance.Options.UseFont = true;
            this.labelControl22.Location = new System.Drawing.Point(6, 217);
            this.labelControl22.Name = "labelControl22";
            this.labelControl22.Size = new System.Drawing.Size(68, 13);
            this.labelControl22.TabIndex = 458;
            this.labelControl22.Text = "Total Shift Hrs";
            // 
            // txtGraceTime
            // 
            this.txtGraceTime.EnterMoveNextControl = true;
            this.txtGraceTime.Location = new System.Drawing.Point(107, 136);
            this.txtGraceTime.Name = "txtGraceTime";
            this.txtGraceTime.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGraceTime.Properties.Appearance.Options.UseFont = true;
            this.txtGraceTime.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtGraceTime.Size = new System.Drawing.Size(187, 20);
            this.txtGraceTime.TabIndex = 9;
            // 
            // labelControl21
            // 
            this.labelControl21.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl21.Appearance.Options.UseFont = true;
            this.labelControl21.Location = new System.Drawing.Point(6, 139);
            this.labelControl21.Name = "labelControl21";
            this.labelControl21.Size = new System.Drawing.Size(53, 13);
            this.labelControl21.TabIndex = 456;
            this.labelControl21.Text = "Grace Time";
            // 
            // txtIntervalEndTime
            // 
            this.txtIntervalEndTime.EnterMoveNextControl = true;
            this.txtIntervalEndTime.Location = new System.Drawing.Point(229, 162);
            this.txtIntervalEndTime.Name = "txtIntervalEndTime";
            this.txtIntervalEndTime.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIntervalEndTime.Properties.Appearance.Options.UseFont = true;
            this.txtIntervalEndTime.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtIntervalEndTime.Size = new System.Drawing.Size(65, 20);
            this.txtIntervalEndTime.TabIndex = 11;
            // 
            // labelControl18
            // 
            this.labelControl18.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl18.Appearance.Options.UseFont = true;
            this.labelControl18.Location = new System.Drawing.Point(175, 165);
            this.labelControl18.Name = "labelControl18";
            this.labelControl18.Size = new System.Drawing.Size(43, 13);
            this.labelControl18.TabIndex = 453;
            this.labelControl18.Text = "End Time";
            // 
            // txtIntervalStartTime
            // 
            this.txtIntervalStartTime.EnterMoveNextControl = true;
            this.txtIntervalStartTime.Location = new System.Drawing.Point(107, 162);
            this.txtIntervalStartTime.Name = "txtIntervalStartTime";
            this.txtIntervalStartTime.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIntervalStartTime.Properties.Appearance.Options.UseFont = true;
            this.txtIntervalStartTime.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtIntervalStartTime.Size = new System.Drawing.Size(65, 20);
            this.txtIntervalStartTime.TabIndex = 10;
            // 
            // labelControl20
            // 
            this.labelControl20.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl20.Appearance.Options.UseFont = true;
            this.labelControl20.Location = new System.Drawing.Point(6, 165);
            this.labelControl20.Name = "labelControl20";
            this.labelControl20.Size = new System.Drawing.Size(90, 13);
            this.labelControl20.TabIndex = 451;
            this.labelControl20.Text = "Interval Start Time";
            // 
            // txtHalfDayBefore
            // 
            this.txtHalfDayBefore.EnterMoveNextControl = true;
            this.txtHalfDayBefore.Location = new System.Drawing.Point(229, 110);
            this.txtHalfDayBefore.Name = "txtHalfDayBefore";
            this.txtHalfDayBefore.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHalfDayBefore.Properties.Appearance.Options.UseFont = true;
            this.txtHalfDayBefore.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtHalfDayBefore.Size = new System.Drawing.Size(65, 20);
            this.txtHalfDayBefore.TabIndex = 8;
            // 
            // labelControl15
            // 
            this.labelControl15.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl15.Appearance.Options.UseFont = true;
            this.labelControl15.Location = new System.Drawing.Point(175, 113);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(32, 13);
            this.labelControl15.TabIndex = 448;
            this.labelControl15.Text = "Before";
            // 
            // labelControl16
            // 
            this.labelControl16.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl16.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl16.Appearance.Options.UseFont = true;
            this.labelControl16.Appearance.Options.UseForeColor = true;
            this.labelControl16.Location = new System.Drawing.Point(220, 34);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(6, 13);
            this.labelControl16.TabIndex = 447;
            this.labelControl16.Text = "*";
            // 
            // txtHalfDayAfter
            // 
            this.txtHalfDayAfter.EnterMoveNextControl = true;
            this.txtHalfDayAfter.Location = new System.Drawing.Point(107, 110);
            this.txtHalfDayAfter.Name = "txtHalfDayAfter";
            this.txtHalfDayAfter.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHalfDayAfter.Properties.Appearance.Options.UseFont = true;
            this.txtHalfDayAfter.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtHalfDayAfter.Size = new System.Drawing.Size(65, 20);
            this.txtHalfDayAfter.TabIndex = 7;
            // 
            // labelControl17
            // 
            this.labelControl17.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl17.Appearance.Options.UseFont = true;
            this.labelControl17.Location = new System.Drawing.Point(6, 113);
            this.labelControl17.Name = "labelControl17";
            this.labelControl17.Size = new System.Drawing.Size(69, 13);
            this.labelControl17.TabIndex = 446;
            this.labelControl17.Text = "Half Day After";
            // 
            // txtLunchEndTime
            // 
            this.txtLunchEndTime.EnterMoveNextControl = true;
            this.txtLunchEndTime.Location = new System.Drawing.Point(229, 84);
            this.txtLunchEndTime.Name = "txtLunchEndTime";
            this.txtLunchEndTime.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLunchEndTime.Properties.Appearance.Options.UseFont = true;
            this.txtLunchEndTime.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLunchEndTime.Size = new System.Drawing.Size(65, 20);
            this.txtLunchEndTime.TabIndex = 6;
            // 
            // labelControl12
            // 
            this.labelControl12.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl12.Appearance.Options.UseFont = true;
            this.labelControl12.Location = new System.Drawing.Point(175, 87);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(43, 13);
            this.labelControl12.TabIndex = 443;
            this.labelControl12.Text = "End Time";
            // 
            // txtLunchStartTime
            // 
            this.txtLunchStartTime.EnterMoveNextControl = true;
            this.txtLunchStartTime.Location = new System.Drawing.Point(107, 84);
            this.txtLunchStartTime.Name = "txtLunchStartTime";
            this.txtLunchStartTime.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLunchStartTime.Properties.Appearance.Options.UseFont = true;
            this.txtLunchStartTime.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLunchStartTime.Size = new System.Drawing.Size(65, 20);
            this.txtLunchStartTime.TabIndex = 5;
            // 
            // labelControl14
            // 
            this.labelControl14.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl14.Appearance.Options.UseFont = true;
            this.labelControl14.Location = new System.Drawing.Point(6, 87);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(80, 13);
            this.labelControl14.TabIndex = 441;
            this.labelControl14.Text = "Lunch Start Time";
            // 
            // txtPunchEndTime
            // 
            this.txtPunchEndTime.EnterMoveNextControl = true;
            this.txtPunchEndTime.Location = new System.Drawing.Point(229, 58);
            this.txtPunchEndTime.Name = "txtPunchEndTime";
            this.txtPunchEndTime.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPunchEndTime.Properties.Appearance.Options.UseFont = true;
            this.txtPunchEndTime.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPunchEndTime.Size = new System.Drawing.Size(65, 20);
            this.txtPunchEndTime.TabIndex = 4;
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl9.Appearance.Options.UseFont = true;
            this.labelControl9.Location = new System.Drawing.Point(175, 61);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(43, 13);
            this.labelControl9.TabIndex = 438;
            this.labelControl9.Text = "End Time";
            // 
            // txtPunchStartTime
            // 
            this.txtPunchStartTime.EnterMoveNextControl = true;
            this.txtPunchStartTime.Location = new System.Drawing.Point(107, 58);
            this.txtPunchStartTime.Name = "txtPunchStartTime";
            this.txtPunchStartTime.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPunchStartTime.Properties.Appearance.Options.UseFont = true;
            this.txtPunchStartTime.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPunchStartTime.Size = new System.Drawing.Size(65, 20);
            this.txtPunchStartTime.TabIndex = 3;
            // 
            // labelControl11
            // 
            this.labelControl11.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl11.Appearance.Options.UseFont = true;
            this.labelControl11.Location = new System.Drawing.Point(6, 61);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(81, 13);
            this.labelControl11.TabIndex = 436;
            this.labelControl11.Text = "Punch Start Time";
            // 
            // txtShiftEndTime
            // 
            this.txtShiftEndTime.EnterMoveNextControl = true;
            this.txtShiftEndTime.Location = new System.Drawing.Point(229, 32);
            this.txtShiftEndTime.Name = "txtShiftEndTime";
            this.txtShiftEndTime.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtShiftEndTime.Properties.Appearance.Options.UseFont = true;
            this.txtShiftEndTime.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtShiftEndTime.Size = new System.Drawing.Size(65, 20);
            this.txtShiftEndTime.TabIndex = 2;
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.Location = new System.Drawing.Point(175, 35);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(43, 13);
            this.labelControl7.TabIndex = 433;
            this.labelControl7.Text = "End Time";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Appearance.Options.UseForeColor = true;
            this.labelControl5.Location = new System.Drawing.Point(98, 34);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(6, 13);
            this.labelControl5.TabIndex = 432;
            this.labelControl5.Text = "*";
            // 
            // txtShiftStartTime
            // 
            this.txtShiftStartTime.EnterMoveNextControl = true;
            this.txtShiftStartTime.Location = new System.Drawing.Point(107, 32);
            this.txtShiftStartTime.Name = "txtShiftStartTime";
            this.txtShiftStartTime.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtShiftStartTime.Properties.Appearance.Options.UseFont = true;
            this.txtShiftStartTime.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtShiftStartTime.Size = new System.Drawing.Size(65, 20);
            this.txtShiftStartTime.TabIndex = 1;
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Location = new System.Drawing.Point(6, 35);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(74, 13);
            this.labelControl6.TabIndex = 431;
            this.labelControl6.Text = "Shift Start Time";
            // 
            // txtRemark
            // 
            this.txtRemark.EditValue = "";
            this.txtRemark.EnterMoveNextControl = true;
            this.txtRemark.Location = new System.Drawing.Point(107, 266);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemark.Properties.Appearance.Options.UseFont = true;
            this.txtRemark.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRemark.Size = new System.Drawing.Size(187, 49);
            this.txtRemark.TabIndex = 15;
            // 
            // chkActive
            // 
            this.chkActive.EditValue = true;
            this.chkActive.EnterMoveNextControl = true;
            this.chkActive.Location = new System.Drawing.Point(310, 6);
            this.chkActive.Name = "chkActive";
            this.chkActive.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkActive.Properties.Appearance.Options.UseFont = true;
            this.chkActive.Properties.Caption = "Active";
            this.chkActive.Size = new System.Drawing.Size(58, 19);
            this.chkActive.TabIndex = 16;
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl8.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl8.Appearance.Options.UseFont = true;
            this.labelControl8.Appearance.Options.UseForeColor = true;
            this.labelControl8.Location = new System.Drawing.Point(98, 8);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(6, 13);
            this.labelControl8.TabIndex = 426;
            this.labelControl8.Text = "*";
            // 
            // txtShiftName
            // 
            this.txtShiftName.EnterMoveNextControl = true;
            this.txtShiftName.Location = new System.Drawing.Point(107, 6);
            this.txtShiftName.Name = "txtShiftName";
            this.txtShiftName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtShiftName.Properties.Appearance.Options.UseFont = true;
            this.txtShiftName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtShiftName.Size = new System.Drawing.Size(187, 20);
            this.txtShiftName.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(6, 9);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(52, 13);
            this.labelControl1.TabIndex = 13;
            this.labelControl1.Text = "Shift Name";
            // 
            // panelControl6
            // 
            this.panelControl6.Controls.Add(this.lblMode);
            this.panelControl6.Controls.Add(this.btnExit);
            this.panelControl6.Controls.Add(this.btnClear);
            this.panelControl6.Controls.Add(this.btnSave);
            this.panelControl6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl6.Location = new System.Drawing.Point(2, 389);
            this.panelControl6.Name = "panelControl6";
            this.panelControl6.Size = new System.Drawing.Size(648, 48);
            this.panelControl6.TabIndex = 17;
            // 
            // lblMode
            // 
            this.lblMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMode.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMode.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblMode.Appearance.Options.UseFont = true;
            this.lblMode.Appearance.Options.UseForeColor = true;
            this.lblMode.Location = new System.Drawing.Point(240, 18);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(63, 13);
            this.lblMode.TabIndex = 430;
            this.lblMode.Text = "Add Mode";
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Appearance.Options.UseFont = true;
            this.btnExit.ImageOptions.Image = global::DERP.Properties.Resources.Exit;
            this.btnExit.Location = new System.Drawing.Point(535, 9);
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
            this.btnClear.Location = new System.Drawing.Point(427, 9);
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
            this.btnSave.Location = new System.Drawing.Point(319, 9);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(102, 32);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(6, 268);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(36, 13);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Remark";
            // 
            // panelControl1
            // 
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(250, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(652, 22);
            this.panelControl1.TabIndex = 18;
            // 
            // panelControl4
            // 
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl4.Location = new System.Drawing.Point(250, 461);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(652, 11);
            this.panelControl4.TabIndex = 16;
            // 
            // panelControl3
            // 
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl3.Location = new System.Drawing.Point(902, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(11, 472);
            this.panelControl3.TabIndex = 15;
            // 
            // panelControl2
            // 
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl2.Location = new System.Drawing.Point(239, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(11, 472);
            this.panelControl2.TabIndex = 14;
            // 
            // FrmMfgShiftMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 472);
            this.Controls.Add(this.panelControl5);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panelControl4);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.dockPanel1);
            this.Name = "FrmMfgShiftMaster";
            this.Text = "Shift Master";
            this.Load += new System.EventHandler(this.FrmMfgShiftMaster_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdShiftMaster)).EndInit();
            this.ContextMNExport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvShiftMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).EndInit();
            this.panelControl5.ResumeLayout(false);
            this.panelControl5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueNextShift.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShiftType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalShiftHrs.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGraceTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIntervalEndTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIntervalStartTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHalfDayBefore.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHalfDayAfter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLunchEndTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLunchStartTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPunchEndTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPunchStartTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShiftEndTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShiftStartTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkActive.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShiftName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).EndInit();
            this.panelControl6.ResumeLayout(false);
            this.panelControl6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraGrid.GridControl grdShiftMaster;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvShiftMaster;
        private DevExpress.XtraGrid.Columns.GridColumn clmShiftId;
        private DevExpress.XtraGrid.Columns.GridColumn clmShiftName;
        private DevExpress.XtraGrid.Columns.GridColumn clmActive;
        private DevExpress.XtraGrid.Columns.GridColumn clmRemark;
        private DevExpress.XtraEditors.PanelControl panelControl5;
        private DevExpress.XtraEditors.CheckEdit chkActive;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.TextEdit txtShiftName;
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
        private DevExpress.XtraEditors.TextEdit txtShiftEndTime;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtShiftStartTime;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtPunchEndTime;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.TextEdit txtPunchStartTime;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.TextEdit txtShiftType;
        private DevExpress.XtraEditors.LabelControl labelControl23;
        private DevExpress.XtraEditors.TextEdit txtTotalShiftHrs;
        private DevExpress.XtraEditors.LabelControl labelControl22;
        private DevExpress.XtraEditors.TextEdit txtGraceTime;
        private DevExpress.XtraEditors.LabelControl labelControl21;
        private DevExpress.XtraEditors.TextEdit txtIntervalEndTime;
        private DevExpress.XtraEditors.LabelControl labelControl18;
        private DevExpress.XtraEditors.TextEdit txtIntervalStartTime;
        private DevExpress.XtraEditors.LabelControl labelControl20;
        private DevExpress.XtraEditors.TextEdit txtHalfDayBefore;
        private DevExpress.XtraEditors.LabelControl labelControl15;
        private DevExpress.XtraEditors.LabelControl labelControl16;
        private DevExpress.XtraEditors.TextEdit txtHalfDayAfter;
        private DevExpress.XtraEditors.LabelControl labelControl17;
        private DevExpress.XtraEditors.TextEdit txtLunchEndTime;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.TextEdit txtLunchStartTime;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraEditors.LookUpEdit lueNextShift;
        private DevExpress.XtraEditors.LabelControl labelControl24;
        private DevExpress.XtraGrid.Columns.GridColumn clmSftStartTime;
        private DevExpress.XtraGrid.Columns.GridColumn clmSftEndTime;
        private DevExpress.XtraGrid.Columns.GridColumn clmPnchStartTime;
        private DevExpress.XtraGrid.Columns.GridColumn clmPnchEndTime;
        private DevExpress.XtraGrid.Columns.GridColumn clmLnchStartTime;
        private DevExpress.XtraGrid.Columns.GridColumn clmLunchEndTime;
        private DevExpress.XtraGrid.Columns.GridColumn clmHfDayAfter;
        private DevExpress.XtraGrid.Columns.GridColumn clmHfDayBefore;
        private DevExpress.XtraGrid.Columns.GridColumn clmGraceTime;
        private DevExpress.XtraGrid.Columns.GridColumn clmIntvStartTime;
        private DevExpress.XtraGrid.Columns.GridColumn clmIntvEndTime;
        private DevExpress.XtraGrid.Columns.GridColumn clmNextShift;
        private DevExpress.XtraGrid.Columns.GridColumn clmTtlShiftHrs;
        private DevExpress.XtraGrid.Columns.GridColumn clmShiftType;
        private DevExpress.XtraGrid.Columns.GridColumn clmNxtShiftId;
        private System.Windows.Forms.ContextMenuStrip ContextMNExport;
        private System.Windows.Forms.ToolStripMenuItem MNExportExcel;
        private System.Windows.Forms.ToolStripMenuItem MNExportPDF;
        private System.Windows.Forms.ToolStripMenuItem MNExportTEXT;
        private System.Windows.Forms.ToolStripMenuItem MNExportHTML;
        private System.Windows.Forms.ToolStripMenuItem MNExportRTF;
        private System.Windows.Forms.ToolStripMenuItem MNExportCSV;
    }
}