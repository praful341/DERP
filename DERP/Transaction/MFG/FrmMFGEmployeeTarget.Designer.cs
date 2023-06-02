namespace DERP.Transaction.MFG
{
    partial class FrmMFGEmployeeTarget
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
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.grdEmployeeTarget = new DevExpress.XtraGrid.GridControl();
            this.bandedGridView2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand6 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.clmDate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.RepRecDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.clmPerformanceID = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.clmA1Cut = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.CHKA1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.G1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.clmG1Cut = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.CHKG1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.gridBand3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.clmA5Cut = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.CHKA5 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.gridBand4 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.clmG5Cut = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.CHKG5 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.gridBand5 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.clmOtherCut = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.CHKOTHER = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.RepPaymentType = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.bandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lueManager = new DevExpress.XtraEditors.LookUpEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.lueSubProcess = new DevExpress.XtraEditors.LookUpEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.lueDepartment = new DevExpress.XtraEditors.LookUpEdit();
            this.label33 = new System.Windows.Forms.Label();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.dtpToDate = new DevExpress.XtraEditors.DateEdit();
            this.dtpFromDate = new DevExpress.XtraEditors.DateEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.backgroundWorker_EmployeeTarget = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.grdEmployeeTarget)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepRecDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepRecDate.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CHKA1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CHKG1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CHKA5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CHKG5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CHKOTHER)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepPaymentType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueManager.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueSubProcess.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueDepartment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpToDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpToDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.ImageOptions.Image = global::DERP.Properties.Resources.Save;
            this.btnSave.Location = new System.Drawing.Point(566, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(102, 32);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grdEmployeeTarget
            // 
            this.grdEmployeeTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdEmployeeTarget.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdEmployeeTarget.Location = new System.Drawing.Point(2, 69);
            this.grdEmployeeTarget.MainView = this.bandedGridView2;
            this.grdEmployeeTarget.Name = "grdEmployeeTarget";
            this.grdEmployeeTarget.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.RepPaymentType,
            this.RepRecDate,
            this.CHKA1,
            this.CHKG1,
            this.CHKA5,
            this.CHKG5,
            this.CHKOTHER});
            this.grdEmployeeTarget.Size = new System.Drawing.Size(1004, 380);
            this.grdEmployeeTarget.TabIndex = 97;
            this.grdEmployeeTarget.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.bandedGridView2,
            this.bandedGridView1});
            // 
            // bandedGridView2
            // 
            this.bandedGridView2.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.bandedGridView2.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.bandedGridView2.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            this.bandedGridView2.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            this.bandedGridView2.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.bandedGridView2.Appearance.FooterPanel.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.bandedGridView2.Appearance.FooterPanel.Options.UseFont = true;
            this.bandedGridView2.Appearance.HeaderPanel.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.bandedGridView2.Appearance.HeaderPanel.Options.UseFont = true;
            this.bandedGridView2.Appearance.Row.Font = new System.Drawing.Font("Cambria", 9F);
            this.bandedGridView2.Appearance.Row.Options.UseFont = true;
            this.bandedGridView2.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand6,
            this.gridBand2,
            this.G1,
            this.gridBand3,
            this.gridBand4,
            this.gridBand5});
            this.bandedGridView2.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.clmPerformanceID,
            this.clmDate,
            this.clmA1Cut,
            this.clmG1Cut,
            this.clmA5Cut,
            this.clmG5Cut,
            this.clmOtherCut});
            this.bandedGridView2.GridControl = this.grdEmployeeTarget;
            this.bandedGridView2.Name = "bandedGridView2";
            this.bandedGridView2.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.bandedGridView2.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.bandedGridView2.OptionsCustomization.AllowColumnMoving = false;
            this.bandedGridView2.OptionsCustomization.AllowQuickHideColumns = false;
            this.bandedGridView2.OptionsNavigation.EnterMoveNextColumn = true;
            this.bandedGridView2.OptionsView.ColumnAutoWidth = false;
            this.bandedGridView2.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.bandedGridView2.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.bandedGridView2.OptionsView.ShowAutoFilterRow = true;
            this.bandedGridView2.OptionsView.ShowFooter = true;
            this.bandedGridView2.OptionsView.ShowGroupPanel = false;
            // 
            // gridBand6
            // 
            this.gridBand6.Caption = " ";
            this.gridBand6.Columns.Add(this.clmDate);
            this.gridBand6.Name = "gridBand6";
            this.gridBand6.VisibleIndex = 0;
            this.gridBand6.Width = 87;
            // 
            // clmDate
            // 
            this.clmDate.Caption = "Date";
            this.clmDate.ColumnEdit = this.RepRecDate;
            this.clmDate.DisplayFormat.FormatString = "d";
            this.clmDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.clmDate.FieldName = "performance_date";
            this.clmDate.Name = "clmDate";
            this.clmDate.Visible = true;
            this.clmDate.Width = 87;
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
            // gridBand2
            // 
            this.gridBand2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridBand2.AppearanceHeader.Options.UseFont = true;
            this.gridBand2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand2.Caption = "A1";
            this.gridBand2.Columns.Add(this.clmPerformanceID);
            this.gridBand2.Columns.Add(this.clmA1Cut);
            this.gridBand2.Name = "gridBand2";
            this.gridBand2.VisibleIndex = 1;
            this.gridBand2.Width = 125;
            // 
            // clmPerformanceID
            // 
            this.clmPerformanceID.Caption = "Performance ID";
            this.clmPerformanceID.FieldName = "emp_performance_id";
            this.clmPerformanceID.Name = "clmPerformanceID";
            this.clmPerformanceID.OptionsColumn.AllowEdit = false;
            // 
            // clmA1Cut
            // 
            this.clmA1Cut.Caption = "Target";
            this.clmA1Cut.ColumnEdit = this.CHKA1;
            this.clmA1Cut.FieldName = "A1";
            this.clmA1Cut.Name = "clmA1Cut";
            this.clmA1Cut.Visible = true;
            this.clmA1Cut.Width = 125;
            // 
            // CHKA1
            // 
            this.CHKA1.AutoHeight = false;
            this.CHKA1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CHKA1.Name = "CHKA1";
            // 
            // G1
            // 
            this.G1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.G1.AppearanceHeader.Options.UseFont = true;
            this.G1.AppearanceHeader.Options.UseTextOptions = true;
            this.G1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.G1.Caption = "G1";
            this.G1.Columns.Add(this.clmG1Cut);
            this.G1.Name = "G1";
            this.G1.VisibleIndex = 2;
            this.G1.Width = 125;
            // 
            // clmG1Cut
            // 
            this.clmG1Cut.Caption = "Target";
            this.clmG1Cut.ColumnEdit = this.CHKG1;
            this.clmG1Cut.FieldName = "G1";
            this.clmG1Cut.Name = "clmG1Cut";
            this.clmG1Cut.Visible = true;
            this.clmG1Cut.Width = 125;
            // 
            // CHKG1
            // 
            this.CHKG1.AutoHeight = false;
            this.CHKG1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CHKG1.Name = "CHKG1";
            // 
            // gridBand3
            // 
            this.gridBand3.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridBand3.AppearanceHeader.Options.UseFont = true;
            this.gridBand3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand3.Caption = "A5";
            this.gridBand3.Columns.Add(this.clmA5Cut);
            this.gridBand3.Name = "gridBand3";
            this.gridBand3.VisibleIndex = 3;
            this.gridBand3.Width = 125;
            // 
            // clmA5Cut
            // 
            this.clmA5Cut.Caption = "Target";
            this.clmA5Cut.ColumnEdit = this.CHKA5;
            this.clmA5Cut.FieldName = "A5";
            this.clmA5Cut.Name = "clmA5Cut";
            this.clmA5Cut.Visible = true;
            this.clmA5Cut.Width = 125;
            // 
            // CHKA5
            // 
            this.CHKA5.AutoHeight = false;
            this.CHKA5.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CHKA5.Name = "CHKA5";
            // 
            // gridBand4
            // 
            this.gridBand4.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridBand4.AppearanceHeader.Options.UseFont = true;
            this.gridBand4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand4.Caption = "G5";
            this.gridBand4.Columns.Add(this.clmG5Cut);
            this.gridBand4.Name = "gridBand4";
            this.gridBand4.VisibleIndex = 4;
            this.gridBand4.Width = 125;
            // 
            // clmG5Cut
            // 
            this.clmG5Cut.Caption = "Target";
            this.clmG5Cut.ColumnEdit = this.CHKG5;
            this.clmG5Cut.FieldName = "G5";
            this.clmG5Cut.Name = "clmG5Cut";
            this.clmG5Cut.Visible = true;
            this.clmG5Cut.Width = 125;
            // 
            // CHKG5
            // 
            this.CHKG5.AutoHeight = false;
            this.CHKG5.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CHKG5.Name = "CHKG5";
            // 
            // gridBand5
            // 
            this.gridBand5.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridBand5.AppearanceHeader.Options.UseFont = true;
            this.gridBand5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand5.Caption = "OTHER";
            this.gridBand5.Columns.Add(this.clmOtherCut);
            this.gridBand5.Name = "gridBand5";
            this.gridBand5.VisibleIndex = 5;
            this.gridBand5.Width = 125;
            // 
            // clmOtherCut
            // 
            this.clmOtherCut.Caption = "Target";
            this.clmOtherCut.ColumnEdit = this.CHKOTHER;
            this.clmOtherCut.FieldName = "OTHER";
            this.clmOtherCut.Name = "clmOtherCut";
            this.clmOtherCut.Visible = true;
            this.clmOtherCut.Width = 125;
            // 
            // CHKOTHER
            // 
            this.CHKOTHER.AutoHeight = false;
            this.CHKOTHER.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CHKOTHER.Name = "CHKOTHER";
            // 
            // RepPaymentType
            // 
            this.RepPaymentType.AutoHeight = false;
            this.RepPaymentType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.RepPaymentType.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("payment_type", "Payment Type")});
            this.RepPaymentType.Name = "RepPaymentType";
            this.RepPaymentType.NullText = "";
            this.RepPaymentType.ShowHeader = false;
            // 
            // bandedGridView1
            // 
            this.bandedGridView1.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1});
            this.bandedGridView1.GridControl = this.grdEmployeeTarget;
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
            this.panelControl2.Controls.Add(this.btnExport);
            this.panelControl2.Controls.Add(this.btnExit);
            this.panelControl2.Controls.Add(this.btnClear);
            this.panelControl2.Controls.Add(this.btnSave);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 451);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1008, 44);
            this.panelControl2.TabIndex = 98;
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Appearance.Options.UseFont = true;
            this.btnExport.ImageOptions.Image = global::DERP.Properties.Resources.Upload_final;
            this.btnExport.Location = new System.Drawing.Point(674, 6);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(102, 32);
            this.btnExport.TabIndex = 1;
            this.btnExport.Text = "&Export";
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Appearance.Options.UseFont = true;
            this.btnExit.ImageOptions.Image = global::DERP.Properties.Resources.Exit;
            this.btnExit.Location = new System.Drawing.Point(890, 6);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(102, 32);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "E&xit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Appearance.Options.UseFont = true;
            this.btnClear.ImageOptions.Image = global::DERP.Properties.Resources.Clear;
            this.btnClear.Location = new System.Drawing.Point(782, 6);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(102, 32);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "&Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.panelControl1);
            this.panelControl3.Controls.Add(this.grdEmployeeTarget);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(0, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(1008, 451);
            this.panelControl3.TabIndex = 99;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lueManager);
            this.panelControl1.Controls.Add(this.label4);
            this.panelControl1.Controls.Add(this.lueSubProcess);
            this.panelControl1.Controls.Add(this.label3);
            this.panelControl1.Controls.Add(this.lueDepartment);
            this.panelControl1.Controls.Add(this.label33);
            this.panelControl1.Controls.Add(this.btnSearch);
            this.panelControl1.Controls.Add(this.dtpToDate);
            this.panelControl1.Controls.Add(this.dtpFromDate);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(2, 2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1004, 43);
            this.panelControl1.TabIndex = 98;
            // 
            // lueManager
            // 
            this.lueManager.EnterMoveNextControl = true;
            this.lueManager.Location = new System.Drawing.Point(777, 11);
            this.lueManager.Name = "lueManager";
            this.lueManager.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueManager.Properties.Appearance.Options.UseFont = true;
            this.lueManager.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold);
            this.lueManager.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.lueManager.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.lueManager.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.lueManager.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("short_name", "Short Name"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("employee_name", "Employee Name"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("employee_id", "Employee ID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lueManager.Properties.NullText = "";
            this.lueManager.Size = new System.Drawing.Size(119, 20);
            this.lueManager.TabIndex = 4;
            this.lueManager.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.lueManager_ButtonClick);
            this.lueManager.EditValueChanged += new System.EventHandler(this.lueManager_EditValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(722, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 32;
            this.label4.Text = "Manager";
            // 
            // lueSubProcess
            // 
            this.lueSubProcess.EnterMoveNextControl = true;
            this.lueSubProcess.Location = new System.Drawing.Point(597, 11);
            this.lueSubProcess.Name = "lueSubProcess";
            this.lueSubProcess.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueSubProcess.Properties.Appearance.Options.UseFont = true;
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
            this.lueSubProcess.Size = new System.Drawing.Size(119, 20);
            this.lueSubProcess.TabIndex = 3;
            this.lueSubProcess.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.lueSubProcess_ButtonClick);
            this.lueSubProcess.EditValueChanged += new System.EventHandler(this.lueSubProcess_EditValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(526, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "Sub Process";
            // 
            // lueDepartment
            // 
            this.lueDepartment.EnterMoveNextControl = true;
            this.lueDepartment.Location = new System.Drawing.Point(401, 11);
            this.lueDepartment.Name = "lueDepartment";
            this.lueDepartment.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueDepartment.Properties.Appearance.Options.UseFont = true;
            this.lueDepartment.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold);
            this.lueDepartment.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.lueDepartment.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.lueDepartment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.lueDepartment.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("department_name", "Department Name"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("department_id", "Department ID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lueDepartment.Properties.NullText = "";
            this.lueDepartment.Properties.ShowHeader = false;
            this.lueDepartment.Size = new System.Drawing.Size(119, 20);
            this.lueDepartment.TabIndex = 2;
            this.lueDepartment.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.lueDepartment_ButtonClick);
            this.lueDepartment.EditValueChanged += new System.EventHandler(this.lueDepartment_EditValueChanged);
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(331, 14);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(64, 13);
            this.label33.TabIndex = 28;
            this.label33.Text = "Department";
            // 
            // btnSearch
            // 
            this.btnSearch.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Appearance.Options.UseFont = true;
            this.btnSearch.ImageOptions.Image = global::DERP.Properties.Resources.Search;
            this.btnSearch.Location = new System.Drawing.Point(898, 6);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(102, 32);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "&Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dtpToDate
            // 
            this.dtpToDate.EditValue = null;
            this.dtpToDate.EnterMoveNextControl = true;
            this.dtpToDate.Location = new System.Drawing.Point(225, 11);
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
            this.dtpFromDate.Location = new System.Drawing.Point(68, 11);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpFromDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpFromDate.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dtpFromDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpFromDate.Size = new System.Drawing.Size(100, 20);
            this.dtpFromDate.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(174, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "To Date";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "From Date";
            // 
            // backgroundWorker_EmployeeTarget
            // 
            this.backgroundWorker_EmployeeTarget.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_EmployeeTarget_DoWork);
            this.backgroundWorker_EmployeeTarget.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_EmployeeTarget_RunWorkerCompleted);
            // 
            // FrmMFGEmployeeTarget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 495);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.panelControl2);
            this.Name = "FrmMFGEmployeeTarget";
            this.Text = "Employee Target";
            this.Load += new System.EventHandler(this.FrmBrokeragePayable_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdEmployeeTarget)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepRecDate.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepRecDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CHKA1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CHKG1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CHKA5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CHKG5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CHKOTHER)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepPaymentType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueManager.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueSubProcess.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueDepartment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpToDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpToDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.GridControl grdEmployeeTarget;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit RepPaymentType;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit RepRecDate;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnExport;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LookUpEdit lueDepartment;
        private System.Windows.Forms.Label label33;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.DateEdit dtpToDate;
        private DevExpress.XtraEditors.DateEdit dtpFromDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.LookUpEdit lueSubProcess;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.LookUpEdit lueManager;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit CHKA1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn clmPerformanceID;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn clmDate;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn clmA1Cut;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn clmG1Cut;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit CHKG1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn clmA5Cut;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit CHKA5;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn clmG5Cut;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit CHKG5;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn clmOtherCut;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit CHKOTHER;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand6;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand G1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand3;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand4;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand5;
        private System.ComponentModel.BackgroundWorker backgroundWorker_EmployeeTarget;
    }
}