namespace DREP.Transaction
{
    partial class FrmMFGLock
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.COMPANY_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COMPANY_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BRANCH_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BRANCH_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BRANCH_COMBINE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LOCATION_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LOCATION_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DEPARTMENT_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DEPARTMENT_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FROM_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TO_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MINUTES = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ACTIVE = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.REMARK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TRANSACTION_TYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GrpDepartment = new DevExpress.XtraEditors.GroupControl();
            this.MainGridDepartment = new DevExpress.XtraGrid.GridControl();
            this.GrdDetDepartment = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl6 = new DevExpress.XtraEditors.PanelControl();
            this.lblMode = new DevExpress.XtraEditors.LabelControl();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.txtRemark = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.chkActive = new DevExpress.XtraEditors.CheckEdit();
            this.txtMinutes = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.lueCompany = new DevExpress.XtraEditors.LookUpEdit();
            this.lueBranch = new DevExpress.XtraEditors.LookUpEdit();
            this.lueLocation = new DevExpress.XtraEditors.LookUpEdit();
            this.lueDepartment = new DevExpress.XtraEditors.LookUpEdit();
            this.DTPToDate = new DevExpress.XtraEditors.DateEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.DTPFromDate = new DevExpress.XtraEditors.DateEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl5 = new DevExpress.XtraEditors.PanelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.lueSDepartment = new DevExpress.XtraEditors.LookUpEdit();
            this.DTPSToDate = new DevExpress.XtraEditors.DateEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.label5 = new System.Windows.Forms.Label();
            this.DTPSFromDate = new DevExpress.XtraEditors.DateEdit();
            this.panelControl7 = new DevExpress.XtraEditors.PanelControl();
            this.grdMFGLock = new DevExpress.XtraGrid.GridControl();
            this.dgvMFGLock = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.clmCompany = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmBranch = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmLocation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmDepartment = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmTransactionType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmFromDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmToDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmMinutes = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmCompanyId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmBranchId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmLocationId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmDepartmentId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmActive = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ClmBranchCombine = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.GrpDepartment)).BeginInit();
            this.GrpDepartment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGridDepartment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDetDepartment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).BeginInit();
            this.panelControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkActive.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMinutes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueBranch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueLocation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueDepartment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DTPToDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DTPToDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DTPFromDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DTPFromDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).BeginInit();
            this.panelControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueSDepartment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DTPSToDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DTPSToDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DTPSFromDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DTPSFromDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl7)).BeginInit();
            this.panelControl7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMFGLock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMFGLock)).BeginInit();
            this.SuspendLayout();
            // 
            // COMPANY_CODE
            // 
            this.COMPANY_CODE.HeaderText = "CompanyCode";
            this.COMPANY_CODE.Name = "COMPANY_CODE";
            this.COMPANY_CODE.ReadOnly = true;
            this.COMPANY_CODE.Visible = false;
            this.COMPANY_CODE.Width = 124;
            // 
            // COMPANY_NAME
            // 
            this.COMPANY_NAME.HeaderText = "Company";
            this.COMPANY_NAME.Name = "COMPANY_NAME";
            this.COMPANY_NAME.ReadOnly = true;
            this.COMPANY_NAME.Width = 92;
            // 
            // BRANCH_CODE
            // 
            this.BRANCH_CODE.HeaderText = "BranchCode";
            this.BRANCH_CODE.Name = "BRANCH_CODE";
            this.BRANCH_CODE.ReadOnly = true;
            this.BRANCH_CODE.Visible = false;
            this.BRANCH_CODE.Width = 109;
            // 
            // BRANCH_NAME
            // 
            this.BRANCH_NAME.HeaderText = "Branch Name";
            this.BRANCH_NAME.Name = "BRANCH_NAME";
            this.BRANCH_NAME.ReadOnly = true;
            this.BRANCH_NAME.Visible = false;
            this.BRANCH_NAME.Width = 118;
            // 
            // BRANCH_COMBINE
            // 
            this.BRANCH_COMBINE.HeaderText = "Branch";
            this.BRANCH_COMBINE.Name = "BRANCH_COMBINE";
            this.BRANCH_COMBINE.ReadOnly = true;
            this.BRANCH_COMBINE.Width = 77;
            // 
            // LOCATION_CODE
            // 
            this.LOCATION_CODE.HeaderText = "LocationCode";
            this.LOCATION_CODE.Name = "LOCATION_CODE";
            this.LOCATION_CODE.ReadOnly = true;
            this.LOCATION_CODE.Visible = false;
            this.LOCATION_CODE.Width = 119;
            // 
            // LOCATION_NAME
            // 
            this.LOCATION_NAME.HeaderText = "Location";
            this.LOCATION_NAME.Name = "LOCATION_NAME";
            this.LOCATION_NAME.ReadOnly = true;
            this.LOCATION_NAME.Width = 87;
            // 
            // DEPARTMENT_CODE
            // 
            this.DEPARTMENT_CODE.HeaderText = "Dept Code";
            this.DEPARTMENT_CODE.Name = "DEPARTMENT_CODE";
            this.DEPARTMENT_CODE.ReadOnly = true;
            this.DEPARTMENT_CODE.Visible = false;
            this.DEPARTMENT_CODE.Width = 98;
            // 
            // DEPARTMENT_NAME
            // 
            this.DEPARTMENT_NAME.HeaderText = "Department";
            this.DEPARTMENT_NAME.Name = "DEPARTMENT_NAME";
            this.DEPARTMENT_NAME.ReadOnly = true;
            this.DEPARTMENT_NAME.Width = 109;
            // 
            // FROM_DATE
            // 
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FROM_DATE.DefaultCellStyle = dataGridViewCellStyle2;
            this.FROM_DATE.HeaderText = "From Date";
            this.FROM_DATE.Name = "FROM_DATE";
            this.FROM_DATE.ReadOnly = true;
            this.FROM_DATE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.FROM_DATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // TO_DATE
            // 
            this.TO_DATE.HeaderText = "To Date";
            this.TO_DATE.Name = "TO_DATE";
            this.TO_DATE.ReadOnly = true;
            this.TO_DATE.Width = 82;
            // 
            // MINUTES
            // 
            this.MINUTES.HeaderText = "Mins";
            this.MINUTES.Name = "MINUTES";
            this.MINUTES.ReadOnly = true;
            this.MINUTES.Width = 61;
            // 
            // ACTIVE
            // 
            this.ACTIVE.HeaderText = "Status";
            this.ACTIVE.Name = "ACTIVE";
            this.ACTIVE.ReadOnly = true;
            this.ACTIVE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ACTIVE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ACTIVE.Width = 73;
            // 
            // REMARK
            // 
            this.REMARK.HeaderText = "Remark";
            this.REMARK.Name = "REMARK";
            this.REMARK.ReadOnly = true;
            this.REMARK.Width = 82;
            // 
            // TRANSACTION_TYPE
            // 
            this.TRANSACTION_TYPE.HeaderText = "Transaction Type";
            this.TRANSACTION_TYPE.Name = "TRANSACTION_TYPE";
            this.TRANSACTION_TYPE.ReadOnly = true;
            this.TRANSACTION_TYPE.Width = 145;
            // 
            // GrpDepartment
            // 
            this.GrpDepartment.AppearanceCaption.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.GrpDepartment.AppearanceCaption.ForeColor = System.Drawing.Color.Black;
            this.GrpDepartment.AppearanceCaption.Options.UseFont = true;
            this.GrpDepartment.AppearanceCaption.Options.UseForeColor = true;
            this.GrpDepartment.AppearanceCaption.Options.UseTextOptions = true;
            this.GrpDepartment.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrpDepartment.Controls.Add(this.MainGridDepartment);
            this.GrpDepartment.Dock = System.Windows.Forms.DockStyle.Right;
            this.GrpDepartment.Location = new System.Drawing.Point(549, 0);
            this.GrpDepartment.LookAndFeel.SkinName = "Blue";
            this.GrpDepartment.LookAndFeel.UseDefaultLookAndFeel = false;
            this.GrpDepartment.Name = "GrpDepartment";
            this.GrpDepartment.Size = new System.Drawing.Size(427, 234);
            this.GrpDepartment.TabIndex = 15;
            this.GrpDepartment.Text = "Department";
            // 
            // MainGridDepartment
            // 
            this.MainGridDepartment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGridDepartment.Location = new System.Drawing.Point(2, 22);
            this.MainGridDepartment.LookAndFeel.SkinName = "Blue";
            this.MainGridDepartment.LookAndFeel.UseDefaultLookAndFeel = false;
            this.MainGridDepartment.MainView = this.GrdDetDepartment;
            this.MainGridDepartment.Name = "MainGridDepartment";
            this.MainGridDepartment.Size = new System.Drawing.Size(423, 210);
            this.MainGridDepartment.TabIndex = 0;
            this.MainGridDepartment.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDetDepartment});
            // 
            // GrdDetDepartment
            // 
            this.GrdDetDepartment.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.GrdDetDepartment.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.GrdDetDepartment.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.White;
            this.GrdDetDepartment.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            this.GrdDetDepartment.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            this.GrdDetDepartment.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.GrdDetDepartment.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(216)))), ((int)(((byte)(254)))));
            this.GrdDetDepartment.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(216)))), ((int)(((byte)(254)))));
            this.GrdDetDepartment.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black;
            this.GrdDetDepartment.Appearance.ColumnFilterButtonActive.Options.UseBackColor = true;
            this.GrdDetDepartment.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = true;
            this.GrdDetDepartment.Appearance.ColumnFilterButtonActive.Options.UseForeColor = true;
            this.GrdDetDepartment.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.GrdDetDepartment.Appearance.Empty.BackColor2 = System.Drawing.Color.White;
            this.GrdDetDepartment.Appearance.Empty.Options.UseBackColor = true;
            this.GrdDetDepartment.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.GrdDetDepartment.Appearance.EvenRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.GrdDetDepartment.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black;
            this.GrdDetDepartment.Appearance.EvenRow.Options.UseBackColor = true;
            this.GrdDetDepartment.Appearance.EvenRow.Options.UseBorderColor = true;
            this.GrdDetDepartment.Appearance.EvenRow.Options.UseForeColor = true;
            this.GrdDetDepartment.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.GrdDetDepartment.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.GrdDetDepartment.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.White;
            this.GrdDetDepartment.Appearance.FilterCloseButton.Options.UseBackColor = true;
            this.GrdDetDepartment.Appearance.FilterCloseButton.Options.UseBorderColor = true;
            this.GrdDetDepartment.Appearance.FilterCloseButton.Options.UseForeColor = true;
            this.GrdDetDepartment.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.GrdDetDepartment.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.White;
            this.GrdDetDepartment.Appearance.FilterPanel.ForeColor = System.Drawing.Color.Black;
            this.GrdDetDepartment.Appearance.FilterPanel.Options.UseBackColor = true;
            this.GrdDetDepartment.Appearance.FilterPanel.Options.UseForeColor = true;
            this.GrdDetDepartment.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(133)))), ((int)(((byte)(195)))));
            this.GrdDetDepartment.Appearance.FixedLine.Options.UseBackColor = true;
            this.GrdDetDepartment.Appearance.FocusedCell.BackColor = System.Drawing.Color.White;
            this.GrdDetDepartment.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.GrdDetDepartment.Appearance.FocusedCell.Options.UseBackColor = true;
            this.GrdDetDepartment.Appearance.FocusedCell.Options.UseForeColor = true;
            this.GrdDetDepartment.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(109)))), ((int)(((byte)(189)))));
            this.GrdDetDepartment.Appearance.FocusedRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(139)))), ((int)(((byte)(206)))));
            this.GrdDetDepartment.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White;
            this.GrdDetDepartment.Appearance.FocusedRow.Options.UseBackColor = true;
            this.GrdDetDepartment.Appearance.FocusedRow.Options.UseBorderColor = true;
            this.GrdDetDepartment.Appearance.FocusedRow.Options.UseForeColor = true;
            this.GrdDetDepartment.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(114)))), ((int)(((byte)(182)))));
            this.GrdDetDepartment.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.GrdDetDepartment.Appearance.FooterPanel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.GrdDetDepartment.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black;
            this.GrdDetDepartment.Appearance.FooterPanel.Options.UseBackColor = true;
            this.GrdDetDepartment.Appearance.FooterPanel.Options.UseBorderColor = true;
            this.GrdDetDepartment.Appearance.FooterPanel.Options.UseFont = true;
            this.GrdDetDepartment.Appearance.FooterPanel.Options.UseForeColor = true;
            this.GrdDetDepartment.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.GrdDetDepartment.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.GrdDetDepartment.Appearance.GroupButton.Options.UseBackColor = true;
            this.GrdDetDepartment.Appearance.GroupButton.Options.UseBorderColor = true;
            this.GrdDetDepartment.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(216)))), ((int)(((byte)(254)))));
            this.GrdDetDepartment.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(216)))), ((int)(((byte)(254)))));
            this.GrdDetDepartment.Appearance.GroupFooter.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.GrdDetDepartment.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black;
            this.GrdDetDepartment.Appearance.GroupFooter.Options.UseBackColor = true;
            this.GrdDetDepartment.Appearance.GroupFooter.Options.UseBorderColor = true;
            this.GrdDetDepartment.Appearance.GroupFooter.Options.UseFont = true;
            this.GrdDetDepartment.Appearance.GroupFooter.Options.UseForeColor = true;
            this.GrdDetDepartment.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White;
            this.GrdDetDepartment.Appearance.GroupPanel.ForeColor = System.Drawing.Color.White;
            this.GrdDetDepartment.Appearance.GroupPanel.Options.UseForeColor = true;
            this.GrdDetDepartment.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(114)))), ((int)(((byte)(182)))));
            this.GrdDetDepartment.Appearance.GroupRow.BorderColor = System.Drawing.Color.White;
            this.GrdDetDepartment.Appearance.GroupRow.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.GrdDetDepartment.Appearance.GroupRow.ForeColor = System.Drawing.Color.White;
            this.GrdDetDepartment.Appearance.GroupRow.Options.UseBackColor = true;
            this.GrdDetDepartment.Appearance.GroupRow.Options.UseBorderColor = true;
            this.GrdDetDepartment.Appearance.GroupRow.Options.UseFont = true;
            this.GrdDetDepartment.Appearance.GroupRow.Options.UseForeColor = true;
            this.GrdDetDepartment.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(114)))), ((int)(((byte)(182)))));
            this.GrdDetDepartment.Appearance.HeaderPanel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.GrdDetDepartment.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.GrdDetDepartment.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.GrdDetDepartment.Appearance.HeaderPanel.Options.UseFont = true;
            this.GrdDetDepartment.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.GrdDetDepartment.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(109)))), ((int)(((byte)(189)))));
            this.GrdDetDepartment.Appearance.HideSelectionRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(109)))), ((int)(((byte)(189)))));
            this.GrdDetDepartment.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.White;
            this.GrdDetDepartment.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.GrdDetDepartment.Appearance.HideSelectionRow.Options.UseBorderColor = true;
            this.GrdDetDepartment.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.GrdDetDepartment.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(157)))), ((int)(((byte)(185)))));
            this.GrdDetDepartment.Appearance.HorzLine.Options.UseBackColor = true;
            this.GrdDetDepartment.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.GrdDetDepartment.Appearance.OddRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.GrdDetDepartment.Appearance.OddRow.ForeColor = System.Drawing.Color.Black;
            this.GrdDetDepartment.Appearance.OddRow.Options.UseBackColor = true;
            this.GrdDetDepartment.Appearance.OddRow.Options.UseBorderColor = true;
            this.GrdDetDepartment.Appearance.OddRow.Options.UseForeColor = true;
            this.GrdDetDepartment.Appearance.Preview.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.GrdDetDepartment.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(215)))));
            this.GrdDetDepartment.Appearance.Preview.Options.UseFont = true;
            this.GrdDetDepartment.Appearance.Preview.Options.UseForeColor = true;
            this.GrdDetDepartment.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.GrdDetDepartment.Appearance.Row.BorderColor = System.Drawing.Color.Black;
            this.GrdDetDepartment.Appearance.Row.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.GrdDetDepartment.Appearance.Row.Options.UseBackColor = true;
            this.GrdDetDepartment.Appearance.Row.Options.UseBorderColor = true;
            this.GrdDetDepartment.Appearance.Row.Options.UseFont = true;
            this.GrdDetDepartment.Appearance.RowSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.GrdDetDepartment.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.White;
            this.GrdDetDepartment.Appearance.RowSeparator.Options.UseBackColor = true;
            this.GrdDetDepartment.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(109)))), ((int)(((byte)(189)))));
            this.GrdDetDepartment.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White;
            this.GrdDetDepartment.Appearance.SelectedRow.Options.UseBackColor = true;
            this.GrdDetDepartment.Appearance.SelectedRow.Options.UseForeColor = true;
            this.GrdDetDepartment.Appearance.TopNewRow.BackColor = System.Drawing.Color.White;
            this.GrdDetDepartment.Appearance.TopNewRow.Options.UseBackColor = true;
            this.GrdDetDepartment.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(157)))), ((int)(((byte)(185)))));
            this.GrdDetDepartment.Appearance.VertLine.Options.UseBackColor = true;
            this.GrdDetDepartment.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.GrdDetDepartment.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.GrdDetDepartment.GridControl = this.MainGridDepartment;
            this.GrdDetDepartment.Name = "GrdDetDepartment";
            this.GrdDetDepartment.OptionsBehavior.Editable = false;
            this.GrdDetDepartment.OptionsFilter.UseNewCustomFilterDialog = true;
            this.GrdDetDepartment.OptionsPrint.ExpandAllGroups = false;
            this.GrdDetDepartment.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.GrdDetDepartment.OptionsView.ColumnAutoWidth = false;
            this.GrdDetDepartment.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.GrdDetDepartment.OptionsView.ShowAutoFilterRow = true;
            this.GrdDetDepartment.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn1.AppearanceCell.Options.UseFont = true;
            this.gridColumn1.Caption = "Code";
            this.gridColumn1.FieldName = "DEPARTMENT_CODE";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn2.AppearanceCell.Options.UseFont = true;
            this.gridColumn2.Caption = "Department";
            this.gridColumn2.FieldName = "DEPARTMENT_NAME";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            // 
            // panelControl6
            // 
            this.panelControl6.Controls.Add(this.lblMode);
            this.panelControl6.Controls.Add(this.lblTitle);
            this.panelControl6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl6.Location = new System.Drawing.Point(0, 0);
            this.panelControl6.Name = "panelControl6";
            this.panelControl6.Size = new System.Drawing.Size(1008, 34);
            this.panelControl6.TabIndex = 8;
            // 
            // lblMode
            // 
            this.lblMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMode.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMode.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblMode.Appearance.Options.UseFont = true;
            this.lblMode.Appearance.Options.UseForeColor = true;
            this.lblMode.Location = new System.Drawing.Point(924, 9);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(63, 13);
            this.lblMode.TabIndex = 544;
            this.lblMode.Text = "Add Mode";
            this.lblMode.Visible = false;
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Verdana", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Appearance.Options.UseForeColor = true;
            this.lblTitle.Location = new System.Drawing.Point(12, 5);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(101, 23);
            this.lblTitle.TabIndex = 522;
            this.lblTitle.Text = "MFG Lock";
            // 
            // panelControl1
            // 
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl1.Location = new System.Drawing.Point(0, 34);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(21, 515);
            this.panelControl1.TabIndex = 9;
            // 
            // panelControl2
            // 
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl2.Location = new System.Drawing.Point(987, 34);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(21, 515);
            this.panelControl2.TabIndex = 10;
            // 
            // panelControl3
            // 
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl3.Location = new System.Drawing.Point(0, 549);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(1008, 17);
            this.panelControl3.TabIndex = 9;
            // 
            // panelControl4
            // 
            this.panelControl4.Controls.Add(this.btnExit);
            this.panelControl4.Controls.Add(this.btnSave);
            this.panelControl4.Controls.Add(this.btnClear);
            this.panelControl4.Controls.Add(this.btnDelete);
            this.panelControl4.Controls.Add(this.txtRemark);
            this.panelControl4.Controls.Add(this.labelControl5);
            this.panelControl4.Controls.Add(this.chkActive);
            this.panelControl4.Controls.Add(this.txtMinutes);
            this.panelControl4.Controls.Add(this.label3);
            this.panelControl4.Controls.Add(this.lueCompany);
            this.panelControl4.Controls.Add(this.lueBranch);
            this.panelControl4.Controls.Add(this.lueLocation);
            this.panelControl4.Controls.Add(this.lueDepartment);
            this.panelControl4.Controls.Add(this.DTPToDate);
            this.panelControl4.Controls.Add(this.label2);
            this.panelControl4.Controls.Add(this.DTPFromDate);
            this.panelControl4.Controls.Add(this.label1);
            this.panelControl4.Controls.Add(this.labelControl4);
            this.panelControl4.Controls.Add(this.labelControl3);
            this.panelControl4.Controls.Add(this.labelControl2);
            this.panelControl4.Controls.Add(this.labelControl1);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl4.Location = new System.Drawing.Point(21, 34);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(966, 133);
            this.panelControl4.TabIndex = 9;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Appearance.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Appearance.Options.UseFont = true;
            this.btnExit.ImageOptions.Image = global::DERP.Properties.Resources.Exit;
            this.btnExit.Location = new System.Drawing.Point(616, 79);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(102, 32);
            this.btnExit.TabIndex = 542;
            this.btnExit.Text = "E&xit";
            this.btnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Appearance.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.ImageOptions.Image = global::DERP.Properties.Resources.Save;
            this.btnSave.Location = new System.Drawing.Point(292, 79);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(102, 32);
            this.btnSave.TabIndex = 540;
            this.btnSave.Text = "&Save";
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Appearance.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Appearance.Options.UseFont = true;
            this.btnClear.ImageOptions.Image = global::DERP.Properties.Resources.Clear;
            this.btnClear.Location = new System.Drawing.Point(508, 79);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(102, 32);
            this.btnClear.TabIndex = 541;
            this.btnClear.Text = "&Clear";
            this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Appearance.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Appearance.Options.UseFont = true;
            this.btnDelete.ImageOptions.Image = global::DERP.Properties.Resources.Close;
            this.btnDelete.Location = new System.Drawing.Point(400, 79);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(102, 32);
            this.btnDelete.TabIndex = 543;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // txtRemark
            // 
            this.txtRemark.EditValue = "";
            this.txtRemark.EnterMoveNextControl = true;
            this.txtRemark.Location = new System.Drawing.Point(83, 74);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemark.Properties.Appearance.Options.UseFont = true;
            this.txtRemark.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRemark.Size = new System.Drawing.Size(190, 49);
            this.txtRemark.TabIndex = 538;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(13, 88);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(50, 15);
            this.labelControl5.TabIndex = 539;
            this.labelControl5.Text = "Remark";
            // 
            // chkActive
            // 
            this.chkActive.EnterMoveNextControl = true;
            this.chkActive.Location = new System.Drawing.Point(801, 47);
            this.chkActive.Name = "chkActive";
            this.chkActive.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkActive.Properties.Appearance.Options.UseFont = true;
            this.chkActive.Properties.Caption = "Active";
            this.chkActive.Size = new System.Drawing.Size(67, 19);
            this.chkActive.TabIndex = 537;
            // 
            // txtMinutes
            // 
            this.txtMinutes.EnterMoveNextControl = true;
            this.txtMinutes.Location = new System.Drawing.Point(670, 47);
            this.txtMinutes.Name = "txtMinutes";
            this.txtMinutes.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMinutes.Properties.Appearance.Options.UseFont = true;
            this.txtMinutes.Size = new System.Drawing.Size(115, 22);
            this.txtMinutes.TabIndex = 536;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(619, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 15);
            this.label3.TabIndex = 535;
            this.label3.Text = "Minute";
            // 
            // lueCompany
            // 
            this.lueCompany.EnterMoveNextControl = true;
            this.lueCompany.Location = new System.Drawing.Point(83, 16);
            this.lueCompany.Name = "lueCompany";
            this.lueCompany.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueCompany.Properties.Appearance.Options.UseFont = true;
            this.lueCompany.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.lueCompany.Properties.AppearanceDropDown.Options.UseFont = true;
            this.lueCompany.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold);
            this.lueCompany.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.lueCompany.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.lueCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueCompany.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("company_name", "Company Name"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("company_id", "Company ID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lueCompany.Properties.NullText = "";
            this.lueCompany.Properties.ShowHeader = false;
            this.lueCompany.Size = new System.Drawing.Size(190, 22);
            this.lueCompany.TabIndex = 534;
            // 
            // lueBranch
            // 
            this.lueBranch.EnterMoveNextControl = true;
            this.lueBranch.Location = new System.Drawing.Point(324, 16);
            this.lueBranch.Name = "lueBranch";
            this.lueBranch.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueBranch.Properties.Appearance.Options.UseFont = true;
            this.lueBranch.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.lueBranch.Properties.AppearanceDropDown.Options.UseFont = true;
            this.lueBranch.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold);
            this.lueBranch.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.lueBranch.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.lueBranch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueBranch.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("branch_name", "Branch Name"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("branch_id", "Branch ID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lueBranch.Properties.NullText = "";
            this.lueBranch.Properties.ShowHeader = false;
            this.lueBranch.Size = new System.Drawing.Size(190, 22);
            this.lueBranch.TabIndex = 533;
            // 
            // lueLocation
            // 
            this.lueLocation.EnterMoveNextControl = true;
            this.lueLocation.Location = new System.Drawing.Point(574, 16);
            this.lueLocation.Name = "lueLocation";
            this.lueLocation.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueLocation.Properties.Appearance.Options.UseFont = true;
            this.lueLocation.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.lueLocation.Properties.AppearanceDropDown.Options.UseFont = true;
            this.lueLocation.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold);
            this.lueLocation.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.lueLocation.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.lueLocation.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueLocation.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("location_name", "Location Name"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("location_id", "Location ID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lueLocation.Properties.NullText = "";
            this.lueLocation.Properties.ShowHeader = false;
            this.lueLocation.Size = new System.Drawing.Size(190, 22);
            this.lueLocation.TabIndex = 532;
            // 
            // lueDepartment
            // 
            this.lueDepartment.EnterMoveNextControl = true;
            this.lueDepartment.Location = new System.Drawing.Point(83, 48);
            this.lueDepartment.Name = "lueDepartment";
            this.lueDepartment.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueDepartment.Properties.Appearance.Options.UseFont = true;
            this.lueDepartment.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.lueDepartment.Properties.AppearanceDropDown.Options.UseFont = true;
            this.lueDepartment.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold);
            this.lueDepartment.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.lueDepartment.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.lueDepartment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueDepartment.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("department_name", "Department Name"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("department_id", "Department ID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lueDepartment.Properties.NullText = "";
            this.lueDepartment.Properties.ShowHeader = false;
            this.lueDepartment.Size = new System.Drawing.Size(190, 22);
            this.lueDepartment.TabIndex = 531;
            // 
            // DTPToDate
            // 
            this.DTPToDate.EditValue = null;
            this.DTPToDate.EnterMoveNextControl = true;
            this.DTPToDate.Location = new System.Drawing.Point(498, 47);
            this.DTPToDate.Name = "DTPToDate";
            this.DTPToDate.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.DTPToDate.Properties.Appearance.Options.UseFont = true;
            this.DTPToDate.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.DTPToDate.Properties.AppearanceDropDown.Options.UseFont = true;
            this.DTPToDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DTPToDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DTPToDate.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.DTPToDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.DTPToDate.Size = new System.Drawing.Size(115, 22);
            this.DTPToDate.TabIndex = 529;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(473, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 15);
            this.label2.TabIndex = 530;
            this.label2.Text = "To";
            // 
            // DTPFromDate
            // 
            this.DTPFromDate.EditValue = null;
            this.DTPFromDate.EnterMoveNextControl = true;
            this.DTPFromDate.Location = new System.Drawing.Point(352, 48);
            this.DTPFromDate.Name = "DTPFromDate";
            this.DTPFromDate.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.DTPFromDate.Properties.Appearance.Options.UseFont = true;
            this.DTPFromDate.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.DTPFromDate.Properties.AppearanceDropDown.Options.UseFont = true;
            this.DTPFromDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DTPFromDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DTPFromDate.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.DTPFromDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.DTPFromDate.Size = new System.Drawing.Size(115, 22);
            this.DTPFromDate.TabIndex = 527;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(279, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 15);
            this.label1.TabIndex = 528;
            this.label1.Text = "From Date";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Appearance.Options.UseForeColor = true;
            this.labelControl4.Location = new System.Drawing.Point(13, 51);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(68, 15);
            this.labelControl4.TabIndex = 526;
            this.labelControl4.Text = "Department";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Appearance.Options.UseForeColor = true;
            this.labelControl3.Location = new System.Drawing.Point(279, 19);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(37, 15);
            this.labelControl3.TabIndex = 525;
            this.labelControl3.Text = "Branch";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Location = new System.Drawing.Point(520, 19);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(46, 15);
            this.labelControl2.TabIndex = 524;
            this.labelControl2.Text = "Location";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(13, 19);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(51, 15);
            this.labelControl1.TabIndex = 523;
            this.labelControl1.Text = "Company";
            // 
            // panelControl5
            // 
            this.panelControl5.Controls.Add(this.btnSearch);
            this.panelControl5.Controls.Add(this.lueSDepartment);
            this.panelControl5.Controls.Add(this.DTPSToDate);
            this.panelControl5.Controls.Add(this.labelControl6);
            this.panelControl5.Controls.Add(this.label5);
            this.panelControl5.Controls.Add(this.DTPSFromDate);
            this.panelControl5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl5.Location = new System.Drawing.Point(21, 167);
            this.panelControl5.Name = "panelControl5";
            this.panelControl5.Size = new System.Drawing.Size(966, 49);
            this.panelControl5.TabIndex = 523;
            // 
            // btnSearch
            // 
            this.btnSearch.Appearance.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Appearance.Options.UseFont = true;
            this.btnSearch.ImageOptions.Image = global::DERP.Properties.Resources.Search;
            this.btnSearch.Location = new System.Drawing.Point(648, 10);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(102, 32);
            this.btnSearch.TabIndex = 544;
            this.btnSearch.Text = "S&earch";
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // lueSDepartment
            // 
            this.lueSDepartment.EnterMoveNextControl = true;
            this.lueSDepartment.Location = new System.Drawing.Point(435, 16);
            this.lueSDepartment.Name = "lueSDepartment";
            this.lueSDepartment.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueSDepartment.Properties.Appearance.Options.UseFont = true;
            this.lueSDepartment.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.lueSDepartment.Properties.AppearanceDropDown.Options.UseFont = true;
            this.lueSDepartment.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold);
            this.lueSDepartment.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.lueSDepartment.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.lueSDepartment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueSDepartment.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("department_name", "Department Name"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("department_id", "Department ID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lueSDepartment.Properties.NullText = "";
            this.lueSDepartment.Properties.ShowHeader = false;
            this.lueSDepartment.Size = new System.Drawing.Size(190, 22);
            this.lueSDepartment.TabIndex = 545;
            // 
            // DTPSToDate
            // 
            this.DTPSToDate.EditValue = null;
            this.DTPSToDate.EnterMoveNextControl = true;
            this.DTPSToDate.Location = new System.Drawing.Point(216, 16);
            this.DTPSToDate.Name = "DTPSToDate";
            this.DTPSToDate.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.DTPSToDate.Properties.Appearance.Options.UseFont = true;
            this.DTPSToDate.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.DTPSToDate.Properties.AppearanceDropDown.Options.UseFont = true;
            this.DTPSToDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DTPSToDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DTPSToDate.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.DTPSToDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.DTPSToDate.Size = new System.Drawing.Size(133, 22);
            this.DTPSToDate.TabIndex = 546;
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Appearance.Options.UseForeColor = true;
            this.labelControl6.Location = new System.Drawing.Point(357, 19);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(68, 15);
            this.labelControl6.TabIndex = 544;
            this.labelControl6.Text = "Department";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(13, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 15);
            this.label5.TabIndex = 545;
            this.label5.Text = "From Date";
            // 
            // DTPSFromDate
            // 
            this.DTPSFromDate.EditValue = null;
            this.DTPSFromDate.EnterMoveNextControl = true;
            this.DTPSFromDate.Location = new System.Drawing.Point(83, 16);
            this.DTPSFromDate.Name = "DTPSFromDate";
            this.DTPSFromDate.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.DTPSFromDate.Properties.Appearance.Options.UseFont = true;
            this.DTPSFromDate.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.DTPSFromDate.Properties.AppearanceDropDown.Options.UseFont = true;
            this.DTPSFromDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DTPSFromDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DTPSFromDate.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.DTPSFromDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.DTPSFromDate.Size = new System.Drawing.Size(127, 22);
            this.DTPSFromDate.TabIndex = 544;
            // 
            // panelControl7
            // 
            this.panelControl7.Controls.Add(this.grdMFGLock);
            this.panelControl7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl7.Location = new System.Drawing.Point(21, 216);
            this.panelControl7.Name = "panelControl7";
            this.panelControl7.Size = new System.Drawing.Size(966, 333);
            this.panelControl7.TabIndex = 524;
            // 
            // grdMFGLock
            // 
            this.grdMFGLock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMFGLock.Location = new System.Drawing.Point(2, 2);
            this.grdMFGLock.MainView = this.dgvMFGLock;
            this.grdMFGLock.Name = "grdMFGLock";
            this.grdMFGLock.Size = new System.Drawing.Size(962, 329);
            this.grdMFGLock.TabIndex = 16;
            this.grdMFGLock.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvMFGLock});
            // 
            // dgvMFGLock
            // 
            this.dgvMFGLock.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.dgvMFGLock.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.dgvMFGLock.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            this.dgvMFGLock.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            this.dgvMFGLock.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.dgvMFGLock.Appearance.FooterPanel.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvMFGLock.Appearance.FooterPanel.Options.UseFont = true;
            this.dgvMFGLock.Appearance.HeaderPanel.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvMFGLock.Appearance.HeaderPanel.Options.UseFont = true;
            this.dgvMFGLock.Appearance.Row.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvMFGLock.Appearance.Row.Options.UseFont = true;
            this.dgvMFGLock.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.clmCompany,
            this.clmBranch,
            this.clmLocation,
            this.clmDepartment,
            this.clmTransactionType,
            this.clmFromDate,
            this.clmToDate,
            this.clmMinutes,
            this.clmCompanyId,
            this.clmBranchId,
            this.clmLocationId,
            this.clmDepartmentId,
            this.clmRemark,
            this.clmActive,
            this.ClmBranchCombine});
            this.dgvMFGLock.GridControl = this.grdMFGLock;
            this.dgvMFGLock.Name = "dgvMFGLock";
            this.dgvMFGLock.OptionsBehavior.Editable = false;
            this.dgvMFGLock.OptionsBehavior.ReadOnly = true;
            this.dgvMFGLock.OptionsCustomization.AllowQuickHideColumns = false;
            this.dgvMFGLock.OptionsView.ColumnAutoWidth = false;
            this.dgvMFGLock.OptionsView.ShowAutoFilterRow = true;
            this.dgvMFGLock.OptionsView.ShowFooter = true;
            this.dgvMFGLock.OptionsView.ShowGroupPanel = false;
            this.dgvMFGLock.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.dgvMFGLock_RowClick);
            // 
            // clmCompany
            // 
            this.clmCompany.Caption = "Company";
            this.clmCompany.FieldName = "company_name";
            this.clmCompany.Name = "clmCompany";
            this.clmCompany.Visible = true;
            this.clmCompany.VisibleIndex = 0;
            // 
            // clmBranch
            // 
            this.clmBranch.Caption = "Branch";
            this.clmBranch.FieldName = "branch_name";
            this.clmBranch.Name = "clmBranch";
            this.clmBranch.Visible = true;
            this.clmBranch.VisibleIndex = 1;
            // 
            // clmLocation
            // 
            this.clmLocation.Caption = "Location";
            this.clmLocation.FieldName = "location_name";
            this.clmLocation.Name = "clmLocation";
            this.clmLocation.Visible = true;
            this.clmLocation.VisibleIndex = 2;
            // 
            // clmDepartment
            // 
            this.clmDepartment.Caption = "Department";
            this.clmDepartment.FieldName = "department_name";
            this.clmDepartment.Name = "clmDepartment";
            this.clmDepartment.Visible = true;
            this.clmDepartment.VisibleIndex = 3;
            this.clmDepartment.Width = 94;
            // 
            // clmTransactionType
            // 
            this.clmTransactionType.Caption = "Transaction Type";
            this.clmTransactionType.FieldName = "transaction_type";
            this.clmTransactionType.Name = "clmTransactionType";
            this.clmTransactionType.Visible = true;
            this.clmTransactionType.VisibleIndex = 9;
            this.clmTransactionType.Width = 120;
            // 
            // clmFromDate
            // 
            this.clmFromDate.Caption = "From Date";
            this.clmFromDate.FieldName = "from_date";
            this.clmFromDate.Name = "clmFromDate";
            this.clmFromDate.Visible = true;
            this.clmFromDate.VisibleIndex = 4;
            // 
            // clmToDate
            // 
            this.clmToDate.Caption = "To Date";
            this.clmToDate.FieldName = "to_date";
            this.clmToDate.Name = "clmToDate";
            this.clmToDate.Visible = true;
            this.clmToDate.VisibleIndex = 5;
            // 
            // clmMinutes
            // 
            this.clmMinutes.Caption = "Minutes";
            this.clmMinutes.FieldName = "Minutes";
            this.clmMinutes.Name = "clmMinutes";
            this.clmMinutes.Visible = true;
            this.clmMinutes.VisibleIndex = 6;
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
            // clmRemark
            // 
            this.clmRemark.Caption = "Remark";
            this.clmRemark.FieldName = "remarks";
            this.clmRemark.Name = "clmRemark";
            this.clmRemark.OptionsColumn.AllowEdit = false;
            this.clmRemark.Visible = true;
            this.clmRemark.VisibleIndex = 8;
            // 
            // clmActive
            // 
            this.clmActive.Caption = "Status";
            this.clmActive.FieldName = "active";
            this.clmActive.Name = "clmActive";
            this.clmActive.OptionsColumn.AllowEdit = false;
            this.clmActive.Visible = true;
            this.clmActive.VisibleIndex = 7;
            // 
            // ClmBranchCombine
            // 
            this.ClmBranchCombine.Caption = "Branch Combine";
            this.ClmBranchCombine.FieldName = "branch_combine";
            this.ClmBranchCombine.Name = "ClmBranchCombine";
            // 
            // FrmMFGLock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 566);
            this.Controls.Add(this.panelControl7);
            this.Controls.Add(this.panelControl5);
            this.Controls.Add(this.panelControl4);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panelControl6);
            this.Controls.Add(this.panelControl3);
            this.Name = "FrmMFGLock";
            this.Text = "MFG LOCK";
            this.Load += new System.EventHandler(this.FrmMFGLock_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GrpDepartment)).EndInit();
            this.GrpDepartment.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainGridDepartment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDetDepartment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).EndInit();
            this.panelControl6.ResumeLayout(false);
            this.panelControl6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            this.panelControl4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkActive.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMinutes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueBranch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueLocation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueDepartment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DTPToDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DTPToDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DTPFromDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DTPFromDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).EndInit();
            this.panelControl5.ResumeLayout(false);
            this.panelControl5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueSDepartment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DTPSToDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DTPSToDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DTPSFromDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DTPSFromDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl7)).EndInit();
            this.panelControl7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdMFGLock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMFGLock)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.GroupControl GrpDepartment;
        private DevExpress.XtraGrid.GridControl MainGridDepartment;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdDetDepartment;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        //private DNKControlLib.dLable lblTotal;
        //private DNKControlLib.dTextBox txtRemark;
        //private DNKControlLib.dLable dLable5;
        //private DNKControlLib.dLable lblMode;
        //private DNKControlLib.InfraRadioButton RbtStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn COMPANY_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn COMPANY_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn BRANCH_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn BRANCH_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn BRANCH_COMBINE;
        private System.Windows.Forms.DataGridViewTextBoxColumn LOCATION_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn LOCATION_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn DEPARTMENT_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn DEPARTMENT_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn FROM_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn TO_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn MINUTES;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ACTIVE;
        private System.Windows.Forms.DataGridViewTextBoxColumn REMARK;
        private System.Windows.Forms.DataGridViewTextBoxColumn TRANSACTION_TYPE;
        private DevExpress.XtraEditors.PanelControl panelControl6;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit DTPToDate;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.DateEdit DTPFromDate;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.LookUpEdit lueDepartment;
        private DevExpress.XtraEditors.LookUpEdit lueCompany;
        private DevExpress.XtraEditors.LookUpEdit lueBranch;
        private DevExpress.XtraEditors.LookUpEdit lueLocation;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.TextEdit txtMinutes;
        private DevExpress.XtraEditors.CheckEdit chkActive;
        private DevExpress.XtraEditors.MemoEdit txtRemark;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.PanelControl panelControl5;
        private DevExpress.XtraEditors.PanelControl panelControl7;
        private DevExpress.XtraEditors.LookUpEdit lueSDepartment;
        private DevExpress.XtraEditors.DateEdit DTPSToDate;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.DateEdit DTPSFromDate;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.LabelControl lblMode;
        private DevExpress.XtraGrid.GridControl grdMFGLock;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvMFGLock;
        private DevExpress.XtraGrid.Columns.GridColumn clmCompany;
        private DevExpress.XtraGrid.Columns.GridColumn clmBranch;
        private DevExpress.XtraGrid.Columns.GridColumn clmLocation;
        private DevExpress.XtraGrid.Columns.GridColumn clmDepartment;
        private DevExpress.XtraGrid.Columns.GridColumn clmTransactionType;
        private DevExpress.XtraGrid.Columns.GridColumn clmFromDate;
        private DevExpress.XtraGrid.Columns.GridColumn clmToDate;
        private DevExpress.XtraGrid.Columns.GridColumn clmMinutes;
        private DevExpress.XtraGrid.Columns.GridColumn clmCompanyId;
        private DevExpress.XtraGrid.Columns.GridColumn clmBranchId;
        private DevExpress.XtraGrid.Columns.GridColumn clmLocationId;
        private DevExpress.XtraGrid.Columns.GridColumn clmDepartmentId;
        private DevExpress.XtraGrid.Columns.GridColumn clmRemark;
        private DevExpress.XtraGrid.Columns.GridColumn clmActive;
        private DevExpress.XtraGrid.Columns.GridColumn ClmBranchCombine;
    }
}