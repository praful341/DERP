namespace DREP.Master.HR
{
    partial class FrmHREmployeeMaster
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelControl5 = new DevExpress.XtraEditors.PanelControl();
            this.TabRegisterDetail = new DevExpress.XtraTab.XtraTabControl();
            this.tblGeneralDetail = new DevExpress.XtraTab.XtraTabPage();
            this.txtEmployeeCode = new DevExpress.XtraEditors.TextEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtRefAdharNo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtSrNo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl17 = new DevExpress.XtraEditors.LabelControl();
            this.dtpLeaveDate = new DevExpress.XtraEditors.DateEdit();
            this.labelControl25 = new DevExpress.XtraEditors.LabelControl();
            this.dtpJoiningDate = new DevExpress.XtraEditors.DateEdit();
            this.txtAadharNo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl21 = new DevExpress.XtraEditors.LabelControl();
            this.txtRefBy = new DevExpress.XtraEditors.TextEdit();
            this.labelControl19 = new DevExpress.XtraEditors.LabelControl();
            this.txtMobile = new DevExpress.XtraEditors.TextEdit();
            this.labelControl18 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.chkActive = new DevExpress.XtraEditors.CheckEdit();
            this.lueFactory = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl30 = new DevExpress.XtraEditors.LabelControl();
            this.lueManager = new DevExpress.XtraEditors.LookUpEdit();
            this.lueFactDepartment = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl22 = new DevExpress.XtraEditors.LabelControl();
            this.txtEmpName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl6 = new DevExpress.XtraEditors.PanelControl();
            this.lblMode = new DevExpress.XtraEditors.LabelControl();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.dgvEmployeeMaster = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.clmEmployeeId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmempname = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmcompany_name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmdepartment_name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmbranch_name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmlocation_name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmActive = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmsrrno = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmmanagername = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmfactoryname = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmcompany_id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmbranch_id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmlocation_id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmdepartment_id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmmanager_id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmfactoryid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmfactorydeptid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmfactorydeptname = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmAadhar = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmRefAdharCardNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmRefBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmJoiningDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmLeaveDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmMobile = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmEmployeeCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdEmployeeMaster = new DevExpress.XtraGrid.GridControl();
            this.ContextMNExport = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MNExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportPDF = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportTEXT = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportHTML = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportRTF = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportCSV = new System.Windows.Forms.ToolStripMenuItem();
            this.LEDGER_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LEDGER_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SHERE_PER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.dtpDOB = new DevExpress.XtraEditors.DateEdit();
            this.labelControl27 = new DevExpress.XtraEditors.LabelControl();
            this.clmDOB = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).BeginInit();
            this.panelControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TabRegisterDetail)).BeginInit();
            this.TabRegisterDetail.SuspendLayout();
            this.tblGeneralDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmployeeCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRefAdharNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSrNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpLeaveDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpLeaveDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpJoiningDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpJoiningDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAadharNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRefBy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMobile.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkActive.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueFactory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueManager.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueFactDepartment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmpName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).BeginInit();
            this.panelControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployeeMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdEmployeeMaster)).BeginInit();
            this.ContextMNExport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDOB.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDOB.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl5
            // 
            this.panelControl5.Controls.Add(this.TabRegisterDetail);
            this.panelControl5.Controls.Add(this.panelControl6);
            this.panelControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl5.Location = new System.Drawing.Point(540, 22);
            this.panelControl5.Name = "panelControl5";
            this.panelControl5.Size = new System.Drawing.Size(477, 481);
            this.panelControl5.TabIndex = 13;
            // 
            // TabRegisterDetail
            // 
            this.TabRegisterDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabRegisterDetail.Location = new System.Drawing.Point(2, 2);
            this.TabRegisterDetail.Name = "TabRegisterDetail";
            this.TabRegisterDetail.SelectedTabPage = this.tblGeneralDetail;
            this.TabRegisterDetail.Size = new System.Drawing.Size(473, 429);
            this.TabRegisterDetail.TabIndex = 0;
            this.TabRegisterDetail.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tblGeneralDetail});
            // 
            // tblGeneralDetail
            // 
            this.tblGeneralDetail.Appearance.Header.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblGeneralDetail.Appearance.Header.Options.UseFont = true;
            this.tblGeneralDetail.Appearance.PageClient.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.tblGeneralDetail.Appearance.PageClient.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.tblGeneralDetail.Appearance.PageClient.Options.UseBorderColor = true;
            this.tblGeneralDetail.Controls.Add(this.dtpDOB);
            this.tblGeneralDetail.Controls.Add(this.labelControl27);
            this.tblGeneralDetail.Controls.Add(this.txtEmployeeCode);
            this.tblGeneralDetail.Controls.Add(this.labelControl8);
            this.tblGeneralDetail.Controls.Add(this.labelControl7);
            this.tblGeneralDetail.Controls.Add(this.txtRefAdharNo);
            this.tblGeneralDetail.Controls.Add(this.labelControl4);
            this.tblGeneralDetail.Controls.Add(this.txtSrNo);
            this.tblGeneralDetail.Controls.Add(this.labelControl17);
            this.tblGeneralDetail.Controls.Add(this.dtpLeaveDate);
            this.tblGeneralDetail.Controls.Add(this.labelControl25);
            this.tblGeneralDetail.Controls.Add(this.dtpJoiningDate);
            this.tblGeneralDetail.Controls.Add(this.txtAadharNo);
            this.tblGeneralDetail.Controls.Add(this.labelControl6);
            this.tblGeneralDetail.Controls.Add(this.labelControl21);
            this.tblGeneralDetail.Controls.Add(this.txtRefBy);
            this.tblGeneralDetail.Controls.Add(this.labelControl19);
            this.tblGeneralDetail.Controls.Add(this.txtMobile);
            this.tblGeneralDetail.Controls.Add(this.labelControl18);
            this.tblGeneralDetail.Controls.Add(this.labelControl15);
            this.tblGeneralDetail.Controls.Add(this.labelControl10);
            this.tblGeneralDetail.Controls.Add(this.labelControl9);
            this.tblGeneralDetail.Controls.Add(this.labelControl1);
            this.tblGeneralDetail.Controls.Add(this.chkActive);
            this.tblGeneralDetail.Controls.Add(this.lueFactory);
            this.tblGeneralDetail.Controls.Add(this.labelControl30);
            this.tblGeneralDetail.Controls.Add(this.lueManager);
            this.tblGeneralDetail.Controls.Add(this.lueFactDepartment);
            this.tblGeneralDetail.Controls.Add(this.labelControl3);
            this.tblGeneralDetail.Controls.Add(this.labelControl22);
            this.tblGeneralDetail.Controls.Add(this.txtEmpName);
            this.tblGeneralDetail.Controls.Add(this.labelControl2);
            this.tblGeneralDetail.Name = "tblGeneralDetail";
            this.tblGeneralDetail.Size = new System.Drawing.Size(467, 401);
            this.tblGeneralDetail.Text = "GENERAL DETAIL";
            // 
            // txtEmployeeCode
            // 
            this.txtEmployeeCode.EnterMoveNextControl = true;
            this.txtEmployeeCode.Location = new System.Drawing.Point(130, 111);
            this.txtEmployeeCode.Name = "txtEmployeeCode";
            this.txtEmployeeCode.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmployeeCode.Properties.Appearance.Options.UseFont = true;
            this.txtEmployeeCode.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEmployeeCode.Size = new System.Drawing.Size(256, 22);
            this.txtEmployeeCode.TabIndex = 4;
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl8.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl8.Appearance.Options.UseFont = true;
            this.labelControl8.Appearance.Options.UseForeColor = true;
            this.labelControl8.Location = new System.Drawing.Point(5, 114);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(97, 16);
            this.labelControl8.TabIndex = 463;
            this.labelControl8.Text = "Employee Code";
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl7.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.Appearance.Options.UseForeColor = true;
            this.labelControl7.Location = new System.Drawing.Point(119, 83);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(6, 13);
            this.labelControl7.TabIndex = 462;
            this.labelControl7.Text = "*";
            // 
            // txtRefAdharNo
            // 
            this.txtRefAdharNo.EnterMoveNextControl = true;
            this.txtRefAdharNo.Location = new System.Drawing.Point(130, 250);
            this.txtRefAdharNo.Name = "txtRefAdharNo";
            this.txtRefAdharNo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRefAdharNo.Properties.Appearance.Options.UseFont = true;
            this.txtRefAdharNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRefAdharNo.Size = new System.Drawing.Size(256, 22);
            this.txtRefAdharNo.TabIndex = 9;
            this.txtRefAdharNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRefAdharNo_KeyPress);
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Appearance.Options.UseForeColor = true;
            this.labelControl4.Location = new System.Drawing.Point(5, 253);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(98, 16);
            this.labelControl4.TabIndex = 460;
            this.labelControl4.Text = "Ref. Aadhar No";
            // 
            // txtSrNo
            // 
            this.txtSrNo.EnterMoveNextControl = true;
            this.txtSrNo.Location = new System.Drawing.Point(130, 83);
            this.txtSrNo.Name = "txtSrNo";
            this.txtSrNo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSrNo.Properties.Appearance.Options.UseFont = true;
            this.txtSrNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSrNo.Size = new System.Drawing.Size(256, 22);
            this.txtSrNo.TabIndex = 3;
            // 
            // labelControl17
            // 
            this.labelControl17.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl17.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl17.Appearance.Options.UseFont = true;
            this.labelControl17.Appearance.Options.UseForeColor = true;
            this.labelControl17.Location = new System.Drawing.Point(5, 86);
            this.labelControl17.Name = "labelControl17";
            this.labelControl17.Size = new System.Drawing.Size(34, 16);
            this.labelControl17.TabIndex = 458;
            this.labelControl17.Text = "Sr No";
            // 
            // dtpLeaveDate
            // 
            this.dtpLeaveDate.EditValue = null;
            this.dtpLeaveDate.EnterMoveNextControl = true;
            this.dtpLeaveDate.Location = new System.Drawing.Point(130, 334);
            this.dtpLeaveDate.Name = "dtpLeaveDate";
            this.dtpLeaveDate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtpLeaveDate.Properties.Appearance.Options.UseFont = true;
            this.dtpLeaveDate.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtpLeaveDate.Properties.AppearanceDropDown.Options.UseFont = true;
            this.dtpLeaveDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpLeaveDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpLeaveDate.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dtpLeaveDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpLeaveDate.Size = new System.Drawing.Size(129, 22);
            this.dtpLeaveDate.TabIndex = 12;
            // 
            // labelControl25
            // 
            this.labelControl25.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl25.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl25.Appearance.Options.UseFont = true;
            this.labelControl25.Appearance.Options.UseForeColor = true;
            this.labelControl25.Location = new System.Drawing.Point(5, 337);
            this.labelControl25.Name = "labelControl25";
            this.labelControl25.Size = new System.Drawing.Size(74, 16);
            this.labelControl25.TabIndex = 456;
            this.labelControl25.Text = "Leave Date";
            // 
            // dtpJoiningDate
            // 
            this.dtpJoiningDate.EditValue = null;
            this.dtpJoiningDate.EnterMoveNextControl = true;
            this.dtpJoiningDate.Location = new System.Drawing.Point(130, 306);
            this.dtpJoiningDate.Name = "dtpJoiningDate";
            this.dtpJoiningDate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtpJoiningDate.Properties.Appearance.Options.UseFont = true;
            this.dtpJoiningDate.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtpJoiningDate.Properties.AppearanceDropDown.Options.UseFont = true;
            this.dtpJoiningDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpJoiningDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpJoiningDate.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dtpJoiningDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpJoiningDate.Size = new System.Drawing.Size(129, 22);
            this.dtpJoiningDate.TabIndex = 11;
            // 
            // txtAadharNo
            // 
            this.txtAadharNo.EnterMoveNextControl = true;
            this.txtAadharNo.Location = new System.Drawing.Point(130, 166);
            this.txtAadharNo.Name = "txtAadharNo";
            this.txtAadharNo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAadharNo.Properties.Appearance.Options.UseFont = true;
            this.txtAadharNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAadharNo.Size = new System.Drawing.Size(256, 22);
            this.txtAadharNo.TabIndex = 6;
            this.txtAadharNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAadharNo_KeyPress);
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Appearance.Options.UseForeColor = true;
            this.labelControl6.Location = new System.Drawing.Point(5, 169);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(68, 16);
            this.labelControl6.TabIndex = 451;
            this.labelControl6.Text = "Aadhar No";
            // 
            // labelControl21
            // 
            this.labelControl21.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl21.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl21.Appearance.Options.UseFont = true;
            this.labelControl21.Appearance.Options.UseForeColor = true;
            this.labelControl21.Location = new System.Drawing.Point(5, 309);
            this.labelControl21.Name = "labelControl21";
            this.labelControl21.Size = new System.Drawing.Size(79, 16);
            this.labelControl21.TabIndex = 449;
            this.labelControl21.Text = "Joining Date";
            // 
            // txtRefBy
            // 
            this.txtRefBy.EnterMoveNextControl = true;
            this.txtRefBy.Location = new System.Drawing.Point(130, 222);
            this.txtRefBy.Name = "txtRefBy";
            this.txtRefBy.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRefBy.Properties.Appearance.Options.UseFont = true;
            this.txtRefBy.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRefBy.Size = new System.Drawing.Size(256, 22);
            this.txtRefBy.TabIndex = 8;
            // 
            // labelControl19
            // 
            this.labelControl19.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl19.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl19.Appearance.Options.UseFont = true;
            this.labelControl19.Appearance.Options.UseForeColor = true;
            this.labelControl19.Location = new System.Drawing.Point(5, 225);
            this.labelControl19.Name = "labelControl19";
            this.labelControl19.Size = new System.Drawing.Size(87, 16);
            this.labelControl19.TabIndex = 445;
            this.labelControl19.Text = "Reference By";
            // 
            // txtMobile
            // 
            this.txtMobile.EnterMoveNextControl = true;
            this.txtMobile.Location = new System.Drawing.Point(130, 194);
            this.txtMobile.Name = "txtMobile";
            this.txtMobile.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMobile.Properties.Appearance.Options.UseFont = true;
            this.txtMobile.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMobile.Properties.MaxLength = 10;
            this.txtMobile.Size = new System.Drawing.Size(256, 22);
            this.txtMobile.TabIndex = 7;
            this.txtMobile.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMobile_KeyPress);
            // 
            // labelControl18
            // 
            this.labelControl18.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl18.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl18.Appearance.Options.UseFont = true;
            this.labelControl18.Appearance.Options.UseForeColor = true;
            this.labelControl18.Location = new System.Drawing.Point(5, 197);
            this.labelControl18.Name = "labelControl18";
            this.labelControl18.Size = new System.Drawing.Size(61, 16);
            this.labelControl18.TabIndex = 443;
            this.labelControl18.Text = "Mobile No";
            // 
            // labelControl15
            // 
            this.labelControl15.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl15.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl15.Appearance.Options.UseFont = true;
            this.labelControl15.Appearance.Options.UseForeColor = true;
            this.labelControl15.Location = new System.Drawing.Point(119, 138);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(6, 13);
            this.labelControl15.TabIndex = 436;
            this.labelControl15.Text = "*";
            // 
            // labelControl10
            // 
            this.labelControl10.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl10.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl10.Appearance.Options.UseFont = true;
            this.labelControl10.Appearance.Options.UseForeColor = true;
            this.labelControl10.Location = new System.Drawing.Point(119, 57);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(6, 13);
            this.labelControl10.TabIndex = 432;
            this.labelControl10.Text = "*";
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl9.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl9.Appearance.Options.UseFont = true;
            this.labelControl9.Appearance.Options.UseForeColor = true;
            this.labelControl9.Location = new System.Drawing.Point(119, 5);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(6, 13);
            this.labelControl9.TabIndex = 431;
            this.labelControl9.Text = "*";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(119, 31);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(6, 13);
            this.labelControl1.TabIndex = 430;
            this.labelControl1.Text = "*";
            // 
            // chkActive
            // 
            this.chkActive.EditValue = true;
            this.chkActive.EnterMoveNextControl = true;
            this.chkActive.Location = new System.Drawing.Point(397, 8);
            this.chkActive.Name = "chkActive";
            this.chkActive.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkActive.Properties.Appearance.Options.UseFont = true;
            this.chkActive.Properties.Caption = "Active";
            this.chkActive.Size = new System.Drawing.Size(67, 20);
            this.chkActive.TabIndex = 13;
            // 
            // lueFactory
            // 
            this.lueFactory.EnterMoveNextControl = true;
            this.lueFactory.Location = new System.Drawing.Point(130, 5);
            this.lueFactory.Name = "lueFactory";
            this.lueFactory.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueFactory.Properties.Appearance.Options.UseFont = true;
            this.lueFactory.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.lueFactory.Properties.AppearanceDropDown.Options.UseFont = true;
            this.lueFactory.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold);
            this.lueFactory.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.lueFactory.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.lueFactory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.lueFactory.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("factory_name", "Factory Name"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("factory_id", "Factory Id", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lueFactory.Properties.NullText = "";
            this.lueFactory.Properties.ShowHeader = false;
            this.lueFactory.Size = new System.Drawing.Size(256, 22);
            this.lueFactory.TabIndex = 0;
            this.lueFactory.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.lueFactory_ButtonClick);
            this.lueFactory.EditValueChanged += new System.EventHandler(this.lueFactory_EditValueChanged);
            // 
            // labelControl30
            // 
            this.labelControl30.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl30.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl30.Appearance.Options.UseFont = true;
            this.labelControl30.Appearance.Options.UseForeColor = true;
            this.labelControl30.Location = new System.Drawing.Point(5, 9);
            this.labelControl30.Name = "labelControl30";
            this.labelControl30.Size = new System.Drawing.Size(49, 16);
            this.labelControl30.TabIndex = 45;
            this.labelControl30.Text = "Factory";
            // 
            // lueManager
            // 
            this.lueManager.EnterMoveNextControl = true;
            this.lueManager.Location = new System.Drawing.Point(130, 57);
            this.lueManager.Name = "lueManager";
            this.lueManager.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueManager.Properties.Appearance.Options.UseFont = true;
            this.lueManager.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.lueManager.Properties.AppearanceDropDown.Options.UseFont = true;
            this.lueManager.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold);
            this.lueManager.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.lueManager.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.lueManager.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.lueManager.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("manager_name", "Manager Name"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("manager_id", "Manager Id", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lueManager.Properties.NullText = "";
            this.lueManager.Properties.ShowHeader = false;
            this.lueManager.Size = new System.Drawing.Size(256, 22);
            this.lueManager.TabIndex = 2;
            this.lueManager.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.lueManager_ButtonClick);
            // 
            // lueFactDepartment
            // 
            this.lueFactDepartment.EnterMoveNextControl = true;
            this.lueFactDepartment.Location = new System.Drawing.Point(130, 31);
            this.lueFactDepartment.Name = "lueFactDepartment";
            this.lueFactDepartment.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueFactDepartment.Properties.Appearance.Options.UseFont = true;
            this.lueFactDepartment.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.lueFactDepartment.Properties.AppearanceDropDown.Options.UseFont = true;
            this.lueFactDepartment.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold);
            this.lueFactDepartment.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.lueFactDepartment.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.lueFactDepartment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueFactDepartment.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("fact_dept_name", "Fact Dept Name"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("fact_department_id", "Fact Dept ID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lueFactDepartment.Properties.NullText = "";
            this.lueFactDepartment.Properties.ShowHeader = false;
            this.lueFactDepartment.Size = new System.Drawing.Size(256, 22);
            this.lueFactDepartment.TabIndex = 1;
            this.lueFactDepartment.EditValueChanged += new System.EventHandler(this.lueFactDepartment_EditValueChanged);
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Appearance.Options.UseForeColor = true;
            this.labelControl3.Location = new System.Drawing.Point(5, 60);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(57, 16);
            this.labelControl3.TabIndex = 43;
            this.labelControl3.Text = "Manager";
            // 
            // labelControl22
            // 
            this.labelControl22.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl22.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl22.Appearance.Options.UseFont = true;
            this.labelControl22.Appearance.Options.UseForeColor = true;
            this.labelControl22.Location = new System.Drawing.Point(5, 35);
            this.labelControl22.Name = "labelControl22";
            this.labelControl22.Size = new System.Drawing.Size(109, 16);
            this.labelControl22.TabIndex = 42;
            this.labelControl22.Text = "Fact Department";
            // 
            // txtEmpName
            // 
            this.txtEmpName.EnterMoveNextControl = true;
            this.txtEmpName.Location = new System.Drawing.Point(130, 138);
            this.txtEmpName.Name = "txtEmpName";
            this.txtEmpName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmpName.Properties.Appearance.Options.UseFont = true;
            this.txtEmpName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEmpName.Size = new System.Drawing.Size(256, 22);
            this.txtEmpName.TabIndex = 5;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Location = new System.Drawing.Point(5, 141);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(65, 16);
            this.labelControl2.TabIndex = 55;
            this.labelControl2.Text = "Emp Name";
            // 
            // panelControl6
            // 
            this.panelControl6.Controls.Add(this.lblMode);
            this.panelControl6.Controls.Add(this.btnExit);
            this.panelControl6.Controls.Add(this.btnClear);
            this.panelControl6.Controls.Add(this.btnSave);
            this.panelControl6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl6.Location = new System.Drawing.Point(2, 431);
            this.panelControl6.Name = "panelControl6";
            this.panelControl6.Size = new System.Drawing.Size(473, 48);
            this.panelControl6.TabIndex = 0;
            // 
            // lblMode
            // 
            this.lblMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMode.Appearance.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMode.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblMode.Appearance.Options.UseFont = true;
            this.lblMode.Appearance.Options.UseForeColor = true;
            this.lblMode.Location = new System.Drawing.Point(50, 15);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(71, 16);
            this.lblMode.TabIndex = 0;
            this.lblMode.Text = "Add Mode";
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Appearance.Options.UseFont = true;
            this.btnExit.ImageOptions.Image = global::DERP.Properties.Resources.Exit;
            this.btnExit.Location = new System.Drawing.Point(364, 8);
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
            this.btnClear.Location = new System.Drawing.Point(256, 8);
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
            this.btnSave.Location = new System.Drawing.Point(148, 8);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(102, 32);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panelControl4
            // 
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl4.Location = new System.Drawing.Point(540, 503);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(477, 11);
            this.panelControl4.TabIndex = 12;
            // 
            // panelControl3
            // 
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl3.Location = new System.Drawing.Point(1017, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(11, 514);
            this.panelControl3.TabIndex = 11;
            // 
            // panelControl2
            // 
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl2.Location = new System.Drawing.Point(529, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(11, 514);
            this.panelControl2.TabIndex = 10;
            // 
            // dgvEmployeeMaster
            // 
            this.dgvEmployeeMaster.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.dgvEmployeeMaster.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.dgvEmployeeMaster.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            this.dgvEmployeeMaster.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            this.dgvEmployeeMaster.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.dgvEmployeeMaster.Appearance.FooterPanel.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.dgvEmployeeMaster.Appearance.FooterPanel.Options.UseFont = true;
            this.dgvEmployeeMaster.Appearance.HeaderPanel.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.dgvEmployeeMaster.Appearance.HeaderPanel.Options.UseFont = true;
            this.dgvEmployeeMaster.Appearance.Row.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold);
            this.dgvEmployeeMaster.Appearance.Row.Options.UseFont = true;
            this.dgvEmployeeMaster.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.clmEmployeeId,
            this.clmempname,
            this.clmcompany_name,
            this.clmdepartment_name,
            this.clmbranch_name,
            this.clmlocation_name,
            this.clmActive,
            this.clmsrrno,
            this.clmmanagername,
            this.clmfactoryname,
            this.clmcompany_id,
            this.clmbranch_id,
            this.clmlocation_id,
            this.clmdepartment_id,
            this.clmmanager_id,
            this.clmfactoryid,
            this.clmfactorydeptid,
            this.clmfactorydeptname,
            this.clmAadhar,
            this.clmRefAdharCardNo,
            this.clmRefBy,
            this.clmJoiningDate,
            this.clmLeaveDate,
            this.clmMobile,
            this.clmEmployeeCode,
            this.clmDOB});
            this.dgvEmployeeMaster.GridControl = this.grdEmployeeMaster;
            this.dgvEmployeeMaster.Name = "dgvEmployeeMaster";
            this.dgvEmployeeMaster.OptionsBehavior.Editable = false;
            this.dgvEmployeeMaster.OptionsBehavior.ReadOnly = true;
            this.dgvEmployeeMaster.OptionsCustomization.AllowQuickHideColumns = false;
            this.dgvEmployeeMaster.OptionsView.ColumnAutoWidth = false;
            this.dgvEmployeeMaster.OptionsView.ShowAutoFilterRow = true;
            this.dgvEmployeeMaster.OptionsView.ShowFooter = true;
            this.dgvEmployeeMaster.OptionsView.ShowGroupPanel = false;
            this.dgvEmployeeMaster.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.dgvEmployeeMaster_RowClick);
            this.dgvEmployeeMaster.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.dgvEmployeeMaster_RowStyle);
            // 
            // clmEmployeeId
            // 
            this.clmEmployeeId.Caption = "Employee Id";
            this.clmEmployeeId.FieldName = "employee_id";
            this.clmEmployeeId.Name = "clmEmployeeId";
            this.clmEmployeeId.OptionsColumn.AllowEdit = false;
            this.clmEmployeeId.Width = 77;
            // 
            // clmempname
            // 
            this.clmempname.Caption = "Emp Name";
            this.clmempname.FieldName = "employee_name";
            this.clmempname.Name = "clmempname";
            this.clmempname.OptionsColumn.AllowEdit = false;
            this.clmempname.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.clmempname.Visible = true;
            this.clmempname.VisibleIndex = 2;
            this.clmempname.Width = 139;
            // 
            // clmcompany_name
            // 
            this.clmcompany_name.Caption = "Comp Name";
            this.clmcompany_name.FieldName = "company_name";
            this.clmcompany_name.Name = "clmcompany_name";
            this.clmcompany_name.OptionsColumn.AllowEdit = false;
            this.clmcompany_name.Width = 86;
            // 
            // clmdepartment_name
            // 
            this.clmdepartment_name.Caption = "Dept Name";
            this.clmdepartment_name.FieldName = "department_name";
            this.clmdepartment_name.Name = "clmdepartment_name";
            this.clmdepartment_name.OptionsColumn.AllowEdit = false;
            this.clmdepartment_name.Width = 71;
            // 
            // clmbranch_name
            // 
            this.clmbranch_name.Caption = "Branch Name";
            this.clmbranch_name.FieldName = "branch_name";
            this.clmbranch_name.Name = "clmbranch_name";
            this.clmbranch_name.OptionsColumn.AllowEdit = false;
            this.clmbranch_name.Width = 62;
            // 
            // clmlocation_name
            // 
            this.clmlocation_name.Caption = "Location Name";
            this.clmlocation_name.FieldName = "location_name";
            this.clmlocation_name.Name = "clmlocation_name";
            this.clmlocation_name.OptionsColumn.AllowEdit = false;
            this.clmlocation_name.Width = 76;
            // 
            // clmActive
            // 
            this.clmActive.Caption = "Active";
            this.clmActive.FieldName = "active";
            this.clmActive.Name = "clmActive";
            this.clmActive.OptionsColumn.AllowEdit = false;
            this.clmActive.Visible = true;
            this.clmActive.VisibleIndex = 13;
            // 
            // clmsrrno
            // 
            this.clmsrrno.Caption = "Sr No";
            this.clmsrrno.FieldName = "sr_no";
            this.clmsrrno.Name = "clmsrrno";
            this.clmsrrno.OptionsColumn.AllowEdit = false;
            this.clmsrrno.Visible = true;
            this.clmsrrno.VisibleIndex = 0;
            // 
            // clmmanagername
            // 
            this.clmmanagername.Caption = "Manager";
            this.clmmanagername.FieldName = "manager_name";
            this.clmmanagername.Name = "clmmanagername";
            this.clmmanagername.OptionsColumn.AllowEdit = false;
            this.clmmanagername.Visible = true;
            this.clmmanagername.VisibleIndex = 3;
            // 
            // clmfactoryname
            // 
            this.clmfactoryname.Caption = "Factory";
            this.clmfactoryname.FieldName = "factory_name";
            this.clmfactoryname.Name = "clmfactoryname";
            this.clmfactoryname.OptionsColumn.AllowEdit = false;
            this.clmfactoryname.Visible = true;
            this.clmfactoryname.VisibleIndex = 4;
            // 
            // clmcompany_id
            // 
            this.clmcompany_id.Caption = "Company Id";
            this.clmcompany_id.FieldName = "company_id";
            this.clmcompany_id.Name = "clmcompany_id";
            this.clmcompany_id.OptionsColumn.AllowEdit = false;
            // 
            // clmbranch_id
            // 
            this.clmbranch_id.Caption = "Branch Id";
            this.clmbranch_id.FieldName = "branch_id";
            this.clmbranch_id.Name = "clmbranch_id";
            this.clmbranch_id.OptionsColumn.AllowEdit = false;
            // 
            // clmlocation_id
            // 
            this.clmlocation_id.Caption = "Loc Id";
            this.clmlocation_id.FieldName = "location_id";
            this.clmlocation_id.Name = "clmlocation_id";
            this.clmlocation_id.OptionsColumn.AllowEdit = false;
            // 
            // clmdepartment_id
            // 
            this.clmdepartment_id.Caption = "Dept Id";
            this.clmdepartment_id.FieldName = "department_id";
            this.clmdepartment_id.Name = "clmdepartment_id";
            this.clmdepartment_id.OptionsColumn.AllowEdit = false;
            // 
            // clmmanager_id
            // 
            this.clmmanager_id.Caption = "Manager ID";
            this.clmmanager_id.FieldName = "manager_id";
            this.clmmanager_id.Name = "clmmanager_id";
            this.clmmanager_id.OptionsColumn.AllowEdit = false;
            // 
            // clmfactoryid
            // 
            this.clmfactoryid.Caption = "Factory ID";
            this.clmfactoryid.FieldName = "factory_id";
            this.clmfactoryid.Name = "clmfactoryid";
            this.clmfactoryid.OptionsColumn.AllowEdit = false;
            // 
            // clmfactorydeptid
            // 
            this.clmfactorydeptid.Caption = "Factory Dept ID";
            this.clmfactorydeptid.FieldName = "fact_department_id";
            this.clmfactorydeptid.Name = "clmfactorydeptid";
            this.clmfactorydeptid.OptionsColumn.AllowEdit = false;
            // 
            // clmfactorydeptname
            // 
            this.clmfactorydeptname.Caption = "Factory Department";
            this.clmfactorydeptname.FieldName = "fact_dept_name";
            this.clmfactorydeptname.Name = "clmfactorydeptname";
            this.clmfactorydeptname.OptionsColumn.AllowEdit = false;
            this.clmfactorydeptname.Visible = true;
            this.clmfactorydeptname.VisibleIndex = 5;
            this.clmfactorydeptname.Width = 134;
            // 
            // clmAadhar
            // 
            this.clmAadhar.Caption = "Aadhar No";
            this.clmAadhar.FieldName = "adharcard_no";
            this.clmAadhar.Name = "clmAadhar";
            this.clmAadhar.Visible = true;
            this.clmAadhar.VisibleIndex = 6;
            // 
            // clmRefAdharCardNo
            // 
            this.clmRefAdharCardNo.Caption = "Ref AdharCard No";
            this.clmRefAdharCardNo.FieldName = "refrence_adharcard_no";
            this.clmRefAdharCardNo.Name = "clmRefAdharCardNo";
            this.clmRefAdharCardNo.Visible = true;
            this.clmRefAdharCardNo.VisibleIndex = 9;
            this.clmRefAdharCardNo.Width = 118;
            // 
            // clmRefBy
            // 
            this.clmRefBy.Caption = "Reference By";
            this.clmRefBy.FieldName = "refrence_emp_name";
            this.clmRefBy.Name = "clmRefBy";
            this.clmRefBy.Visible = true;
            this.clmRefBy.VisibleIndex = 8;
            this.clmRefBy.Width = 89;
            // 
            // clmJoiningDate
            // 
            this.clmJoiningDate.Caption = "Joining Date";
            this.clmJoiningDate.FieldName = "joining_date";
            this.clmJoiningDate.Name = "clmJoiningDate";
            this.clmJoiningDate.Visible = true;
            this.clmJoiningDate.VisibleIndex = 11;
            this.clmJoiningDate.Width = 90;
            // 
            // clmLeaveDate
            // 
            this.clmLeaveDate.Caption = "Leave Date";
            this.clmLeaveDate.FieldName = "leave_date";
            this.clmLeaveDate.Name = "clmLeaveDate";
            this.clmLeaveDate.Visible = true;
            this.clmLeaveDate.VisibleIndex = 12;
            this.clmLeaveDate.Width = 85;
            // 
            // clmMobile
            // 
            this.clmMobile.Caption = "Mobile No";
            this.clmMobile.FieldName = "mobile_no";
            this.clmMobile.Name = "clmMobile";
            this.clmMobile.Visible = true;
            this.clmMobile.VisibleIndex = 7;
            // 
            // clmEmployeeCode
            // 
            this.clmEmployeeCode.Caption = "Emp Code";
            this.clmEmployeeCode.FieldName = "employee_code";
            this.clmEmployeeCode.Name = "clmEmployeeCode";
            this.clmEmployeeCode.Visible = true;
            this.clmEmployeeCode.VisibleIndex = 1;
            this.clmEmployeeCode.Width = 78;
            // 
            // grdEmployeeMaster
            // 
            this.grdEmployeeMaster.ContextMenuStrip = this.ContextMNExport;
            this.grdEmployeeMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdEmployeeMaster.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdEmployeeMaster.Location = new System.Drawing.Point(0, 0);
            this.grdEmployeeMaster.MainView = this.dgvEmployeeMaster;
            this.grdEmployeeMaster.Name = "grdEmployeeMaster";
            this.grdEmployeeMaster.Size = new System.Drawing.Size(520, 487);
            this.grdEmployeeMaster.TabIndex = 0;
            this.grdEmployeeMaster.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvEmployeeMaster});
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
            // LEDGER_NAME
            // 
            this.LEDGER_NAME.HeaderText = "Ledger Name";
            this.LEDGER_NAME.Name = "LEDGER_NAME";
            this.LEDGER_NAME.Width = 106;
            // 
            // LEDGER_CODE
            // 
            this.LEDGER_CODE.HeaderText = "Ledger Code";
            this.LEDGER_CODE.Name = "LEDGER_CODE";
            this.LEDGER_CODE.Visible = false;
            this.LEDGER_CODE.Width = 102;
            // 
            // SHERE_PER
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.SHERE_PER.DefaultCellStyle = dataGridViewCellStyle2;
            this.SHERE_PER.HeaderText = "Shere(%)";
            this.SHERE_PER.Name = "SHERE_PER";
            this.SHERE_PER.Width = 88;
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
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // dockPanel1
            // 
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanel1.ID = new System.Guid("d6e3ca33-077e-4857-9c9f-bad2282ff8f5");
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.Options.ShowCloseButton = false;
            this.dockPanel1.OriginalSize = new System.Drawing.Size(529, 200);
            this.dockPanel1.Size = new System.Drawing.Size(529, 514);
            this.dockPanel1.Text = "HR Employee Master";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.grdEmployeeMaster);
            this.dockPanel1_Container.Location = new System.Drawing.Point(4, 23);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(520, 487);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // panelControl1
            // 
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(540, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(477, 22);
            this.panelControl1.TabIndex = 13;
            // 
            // dtpDOB
            // 
            this.dtpDOB.EditValue = null;
            this.dtpDOB.EnterMoveNextControl = true;
            this.dtpDOB.Location = new System.Drawing.Point(130, 278);
            this.dtpDOB.Name = "dtpDOB";
            this.dtpDOB.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtpDOB.Properties.Appearance.Options.UseFont = true;
            this.dtpDOB.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtpDOB.Properties.AppearanceDropDown.Options.UseFont = true;
            this.dtpDOB.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDOB.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDOB.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dtpDOB.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpDOB.Size = new System.Drawing.Size(129, 22);
            this.dtpDOB.TabIndex = 10;
            // 
            // labelControl27
            // 
            this.labelControl27.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl27.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl27.Appearance.Options.UseFont = true;
            this.labelControl27.Appearance.Options.UseForeColor = true;
            this.labelControl27.Location = new System.Drawing.Point(5, 281);
            this.labelControl27.Name = "labelControl27";
            this.labelControl27.Size = new System.Drawing.Size(26, 16);
            this.labelControl27.TabIndex = 466;
            this.labelControl27.Text = "DOB";
            // 
            // clmDOB
            // 
            this.clmDOB.Caption = "DOB";
            this.clmDOB.FieldName = "dob";
            this.clmDOB.Name = "clmDOB";
            this.clmDOB.Visible = true;
            this.clmDOB.VisibleIndex = 10;
            this.clmDOB.Width = 100;
            // 
            // FrmHREmployeeMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 514);
            this.Controls.Add(this.panelControl5);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panelControl4);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.dockPanel1);
            this.KeyPreview = true;
            this.Name = "FrmHREmployeeMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HR Employee Master";
            this.Load += new System.EventHandler(this.FrmEmployeeMaster_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).EndInit();
            this.panelControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TabRegisterDetail)).EndInit();
            this.TabRegisterDetail.ResumeLayout(false);
            this.tblGeneralDetail.ResumeLayout(false);
            this.tblGeneralDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmployeeCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRefAdharNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSrNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpLeaveDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpLeaveDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpJoiningDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpJoiningDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAadharNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRefBy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMobile.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkActive.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueFactory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueManager.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueFactDepartment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmpName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).EndInit();
            this.panelControl6.ResumeLayout(false);
            this.panelControl6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployeeMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdEmployeeMaster)).EndInit();
            this.ContextMNExport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDOB.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDOB.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl5;
        private DevExpress.XtraEditors.PanelControl panelControl6;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvEmployeeMaster;
        private DevExpress.XtraGrid.Columns.GridColumn clmEmployeeId;
        private DevExpress.XtraGrid.Columns.GridColumn clmempname;
        private DevExpress.XtraGrid.Columns.GridColumn clmcompany_name;
        private DevExpress.XtraGrid.Columns.GridColumn clmdepartment_name;
        private DevExpress.XtraGrid.GridControl grdEmployeeMaster;
        private DevExpress.XtraTab.XtraTabControl TabRegisterDetail;
        private DevExpress.XtraTab.XtraTabPage tblGeneralDetail;
        private System.Windows.Forms.DataGridViewTextBoxColumn LEDGER_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn LEDGER_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn SHERE_PER;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtEmpName;
        private DevExpress.XtraGrid.Columns.GridColumn clmbranch_name;
        private DevExpress.XtraGrid.Columns.GridColumn clmlocation_name;
        private DevExpress.XtraEditors.LookUpEdit lueManager;
        private DevExpress.XtraEditors.LookUpEdit lueFactDepartment;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl22;
        private DevExpress.XtraEditors.LookUpEdit lueFactory;
        private DevExpress.XtraEditors.LabelControl labelControl30;
        private DevExpress.XtraGrid.Columns.GridColumn clmActive;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraEditors.LabelControl labelControl15;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.CheckEdit chkActive;
        private DevExpress.XtraEditors.LabelControl lblMode;
        private DevExpress.XtraGrid.Columns.GridColumn clmsrrno;
        private DevExpress.XtraGrid.Columns.GridColumn clmmanagername;
        private DevExpress.XtraGrid.Columns.GridColumn clmfactoryname;
        private DevExpress.XtraGrid.Columns.GridColumn clmcompany_id;
        private DevExpress.XtraGrid.Columns.GridColumn clmbranch_id;
        private DevExpress.XtraGrid.Columns.GridColumn clmlocation_id;
        private DevExpress.XtraGrid.Columns.GridColumn clmdepartment_id;
        private DevExpress.XtraGrid.Columns.GridColumn clmmanager_id;
        private DevExpress.XtraGrid.Columns.GridColumn clmfactoryid;
        private DevExpress.XtraGrid.Columns.GridColumn clmfactorydeptid;
        private DevExpress.XtraGrid.Columns.GridColumn clmfactorydeptname;
        private System.Windows.Forms.ContextMenuStrip ContextMNExport;
        private System.Windows.Forms.ToolStripMenuItem MNExportExcel;
        private System.Windows.Forms.ToolStripMenuItem MNExportPDF;
        private System.Windows.Forms.ToolStripMenuItem MNExportTEXT;
        private System.Windows.Forms.ToolStripMenuItem MNExportHTML;
        private System.Windows.Forms.ToolStripMenuItem MNExportRTF;
        private System.Windows.Forms.ToolStripMenuItem MNExportCSV;
        private DevExpress.XtraGrid.Columns.GridColumn clmAadhar;
        private DevExpress.XtraEditors.TextEdit txtAadharNo;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl21;
        private DevExpress.XtraEditors.TextEdit txtRefBy;
        private DevExpress.XtraEditors.LabelControl labelControl19;
        private DevExpress.XtraEditors.TextEdit txtMobile;
        private DevExpress.XtraEditors.LabelControl labelControl18;
        private DevExpress.XtraEditors.DateEdit dtpJoiningDate;
        private DevExpress.XtraGrid.Columns.GridColumn clmRefAdharCardNo;
        private DevExpress.XtraGrid.Columns.GridColumn clmRefBy;
        private DevExpress.XtraGrid.Columns.GridColumn clmJoiningDate;
        private DevExpress.XtraGrid.Columns.GridColumn clmMobile;
        private DevExpress.XtraEditors.DateEdit dtpLeaveDate;
        private DevExpress.XtraEditors.LabelControl labelControl25;
        private DevExpress.XtraGrid.Columns.GridColumn clmLeaveDate;
        private DevExpress.XtraEditors.TextEdit txtSrNo;
        private DevExpress.XtraEditors.LabelControl labelControl17;
        private DevExpress.XtraEditors.TextEdit txtRefAdharNo;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.TextEdit txtEmployeeCode;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraGrid.Columns.GridColumn clmEmployeeCode;
        private DevExpress.XtraEditors.DateEdit dtpDOB;
        private DevExpress.XtraEditors.LabelControl labelControl27;
        private DevExpress.XtraGrid.Columns.GridColumn clmDOB;
    }
}