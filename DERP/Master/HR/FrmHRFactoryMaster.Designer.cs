namespace DREP.Master.HR
{
    partial class FrmHRFactoryMaster
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelControl5 = new DevExpress.XtraEditors.PanelControl();
            this.TabRegisterDetail = new DevExpress.XtraTab.XtraTabControl();
            this.tblGeneralDetail = new DevExpress.XtraTab.XtraTabPage();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.txtFactoryShortName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.chkActive = new DevExpress.XtraEditors.CheckEdit();
            this.txtFactoryName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl6 = new DevExpress.XtraEditors.PanelControl();
            this.lblMode = new DevExpress.XtraEditors.LabelControl();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.dgvFactoryMaster = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.clmFactoryID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmFactoryName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmActive = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmFactoryShortName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdFactoryMaster = new DevExpress.XtraGrid.GridControl();
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
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).BeginInit();
            this.panelControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TabRegisterDetail)).BeginInit();
            this.TabRegisterDetail.SuspendLayout();
            this.tblGeneralDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFactoryShortName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkActive.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFactoryName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).BeginInit();
            this.panelControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFactoryMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFactoryMaster)).BeginInit();
            this.ContextMNExport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl5
            // 
            this.panelControl5.Controls.Add(this.TabRegisterDetail);
            this.panelControl5.Controls.Add(this.panelControl6);
            this.panelControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl5.Location = new System.Drawing.Point(381, 22);
            this.panelControl5.Name = "panelControl5";
            this.panelControl5.Size = new System.Drawing.Size(442, 481);
            this.panelControl5.TabIndex = 13;
            // 
            // TabRegisterDetail
            // 
            this.TabRegisterDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabRegisterDetail.Location = new System.Drawing.Point(2, 2);
            this.TabRegisterDetail.Name = "TabRegisterDetail";
            this.TabRegisterDetail.SelectedTabPage = this.tblGeneralDetail;
            this.TabRegisterDetail.Size = new System.Drawing.Size(438, 429);
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
            this.tblGeneralDetail.Controls.Add(this.labelControl11);
            this.tblGeneralDetail.Controls.Add(this.txtFactoryShortName);
            this.tblGeneralDetail.Controls.Add(this.labelControl12);
            this.tblGeneralDetail.Controls.Add(this.labelControl15);
            this.tblGeneralDetail.Controls.Add(this.chkActive);
            this.tblGeneralDetail.Controls.Add(this.txtFactoryName);
            this.tblGeneralDetail.Controls.Add(this.labelControl2);
            this.tblGeneralDetail.Name = "tblGeneralDetail";
            this.tblGeneralDetail.Size = new System.Drawing.Size(432, 401);
            this.tblGeneralDetail.Text = "GENERAL DETAIL";
            // 
            // labelControl11
            // 
            this.labelControl11.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl11.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl11.Appearance.Options.UseFont = true;
            this.labelControl11.Appearance.Options.UseForeColor = true;
            this.labelControl11.Location = new System.Drawing.Point(159, 40);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(6, 13);
            this.labelControl11.TabIndex = 464;
            this.labelControl11.Text = "*";
            // 
            // txtFactoryShortName
            // 
            this.txtFactoryShortName.EnterMoveNextControl = true;
            this.txtFactoryShortName.Location = new System.Drawing.Point(170, 40);
            this.txtFactoryShortName.Name = "txtFactoryShortName";
            this.txtFactoryShortName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFactoryShortName.Properties.Appearance.Options.UseFont = true;
            this.txtFactoryShortName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFactoryShortName.Size = new System.Drawing.Size(256, 22);
            this.txtFactoryShortName.TabIndex = 1;
            // 
            // labelControl12
            // 
            this.labelControl12.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl12.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl12.Appearance.Options.UseFont = true;
            this.labelControl12.Appearance.Options.UseForeColor = true;
            this.labelControl12.Location = new System.Drawing.Point(17, 43);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(124, 16);
            this.labelControl12.TabIndex = 463;
            this.labelControl12.Text = "Factory ShortName";
            // 
            // labelControl15
            // 
            this.labelControl15.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl15.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl15.Appearance.Options.UseFont = true;
            this.labelControl15.Appearance.Options.UseForeColor = true;
            this.labelControl15.Location = new System.Drawing.Point(159, 12);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(6, 13);
            this.labelControl15.TabIndex = 436;
            this.labelControl15.Text = "*";
            // 
            // chkActive
            // 
            this.chkActive.EditValue = true;
            this.chkActive.EnterMoveNextControl = true;
            this.chkActive.Location = new System.Drawing.Point(170, 68);
            this.chkActive.Name = "chkActive";
            this.chkActive.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkActive.Properties.Appearance.Options.UseFont = true;
            this.chkActive.Properties.Caption = "Active";
            this.chkActive.Size = new System.Drawing.Size(67, 20);
            this.chkActive.TabIndex = 2;
            // 
            // txtFactoryName
            // 
            this.txtFactoryName.EnterMoveNextControl = true;
            this.txtFactoryName.Location = new System.Drawing.Point(170, 12);
            this.txtFactoryName.Name = "txtFactoryName";
            this.txtFactoryName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFactoryName.Properties.Appearance.Options.UseFont = true;
            this.txtFactoryName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFactoryName.Size = new System.Drawing.Size(256, 22);
            this.txtFactoryName.TabIndex = 0;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Location = new System.Drawing.Point(17, 12);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(88, 16);
            this.labelControl2.TabIndex = 55;
            this.labelControl2.Text = "Factory Name";
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
            this.panelControl6.Size = new System.Drawing.Size(438, 48);
            this.panelControl6.TabIndex = 0;
            // 
            // lblMode
            // 
            this.lblMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMode.Appearance.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMode.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblMode.Appearance.Options.UseFont = true;
            this.lblMode.Appearance.Options.UseForeColor = true;
            this.lblMode.Location = new System.Drawing.Point(36, 15);
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
            this.btnExit.Location = new System.Drawing.Point(329, 8);
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
            this.btnClear.Location = new System.Drawing.Point(221, 8);
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
            this.btnSave.Location = new System.Drawing.Point(113, 8);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(102, 32);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panelControl4
            // 
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl4.Location = new System.Drawing.Point(381, 503);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(442, 11);
            this.panelControl4.TabIndex = 12;
            // 
            // panelControl3
            // 
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl3.Location = new System.Drawing.Point(823, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(11, 514);
            this.panelControl3.TabIndex = 11;
            // 
            // panelControl2
            // 
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl2.Location = new System.Drawing.Point(370, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(11, 514);
            this.panelControl2.TabIndex = 10;
            // 
            // dgvFactoryMaster
            // 
            this.dgvFactoryMaster.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.dgvFactoryMaster.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.dgvFactoryMaster.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            this.dgvFactoryMaster.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            this.dgvFactoryMaster.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.dgvFactoryMaster.Appearance.FooterPanel.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.dgvFactoryMaster.Appearance.FooterPanel.Options.UseFont = true;
            this.dgvFactoryMaster.Appearance.HeaderPanel.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.dgvFactoryMaster.Appearance.HeaderPanel.Options.UseFont = true;
            this.dgvFactoryMaster.Appearance.Row.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold);
            this.dgvFactoryMaster.Appearance.Row.Options.UseFont = true;
            this.dgvFactoryMaster.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.clmFactoryID,
            this.clmFactoryName,
            this.clmActive,
            this.clmFactoryShortName});
            this.dgvFactoryMaster.GridControl = this.grdFactoryMaster;
            this.dgvFactoryMaster.Name = "dgvFactoryMaster";
            this.dgvFactoryMaster.OptionsBehavior.Editable = false;
            this.dgvFactoryMaster.OptionsBehavior.ReadOnly = true;
            this.dgvFactoryMaster.OptionsCustomization.AllowQuickHideColumns = false;
            this.dgvFactoryMaster.OptionsView.ColumnAutoWidth = false;
            this.dgvFactoryMaster.OptionsView.ShowAutoFilterRow = true;
            this.dgvFactoryMaster.OptionsView.ShowFooter = true;
            this.dgvFactoryMaster.OptionsView.ShowGroupPanel = false;
            this.dgvFactoryMaster.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.dgvFactoryMaster_RowClick);
            this.dgvFactoryMaster.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.dgvEmployeeMaster_RowStyle);
            // 
            // clmFactoryID
            // 
            this.clmFactoryID.Caption = "Factory Id";
            this.clmFactoryID.FieldName = "factory_id";
            this.clmFactoryID.Name = "clmFactoryID";
            this.clmFactoryID.OptionsColumn.AllowEdit = false;
            this.clmFactoryID.Width = 77;
            // 
            // clmFactoryName
            // 
            this.clmFactoryName.Caption = "Factory";
            this.clmFactoryName.FieldName = "factory_name";
            this.clmFactoryName.Name = "clmFactoryName";
            this.clmFactoryName.OptionsColumn.AllowEdit = false;
            this.clmFactoryName.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.clmFactoryName.Visible = true;
            this.clmFactoryName.VisibleIndex = 0;
            this.clmFactoryName.Width = 133;
            // 
            // clmActive
            // 
            this.clmActive.Caption = "Active";
            this.clmActive.FieldName = "active";
            this.clmActive.Name = "clmActive";
            this.clmActive.OptionsColumn.AllowEdit = false;
            this.clmActive.Visible = true;
            this.clmActive.VisibleIndex = 2;
            // 
            // clmFactoryShortName
            // 
            this.clmFactoryShortName.Caption = "Factory Short Name";
            this.clmFactoryShortName.FieldName = "factory_short_name";
            this.clmFactoryShortName.Name = "clmFactoryShortName";
            this.clmFactoryShortName.OptionsColumn.AllowEdit = false;
            this.clmFactoryShortName.Visible = true;
            this.clmFactoryShortName.VisibleIndex = 1;
            this.clmFactoryShortName.Width = 132;
            // 
            // grdFactoryMaster
            // 
            this.grdFactoryMaster.ContextMenuStrip = this.ContextMNExport;
            this.grdFactoryMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdFactoryMaster.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdFactoryMaster.Location = new System.Drawing.Point(0, 0);
            this.grdFactoryMaster.MainView = this.dgvFactoryMaster;
            this.grdFactoryMaster.Name = "grdFactoryMaster";
            this.grdFactoryMaster.Size = new System.Drawing.Size(361, 487);
            this.grdFactoryMaster.TabIndex = 0;
            this.grdFactoryMaster.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvFactoryMaster});
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.SHERE_PER.DefaultCellStyle = dataGridViewCellStyle1;
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
            this.dockPanel1.OriginalSize = new System.Drawing.Size(370, 200);
            this.dockPanel1.Size = new System.Drawing.Size(370, 514);
            this.dockPanel1.Text = "HR Factory Master";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.grdFactoryMaster);
            this.dockPanel1_Container.Location = new System.Drawing.Point(4, 23);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(361, 487);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // panelControl1
            // 
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(381, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(442, 22);
            this.panelControl1.TabIndex = 13;
            // 
            // FrmHRFactoryMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 514);
            this.Controls.Add(this.panelControl5);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panelControl4);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.dockPanel1);
            this.KeyPreview = true;
            this.Name = "FrmHRFactoryMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HR Factory Master";
            this.Load += new System.EventHandler(this.FrmHRFactoryMaster_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).EndInit();
            this.panelControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TabRegisterDetail)).EndInit();
            this.TabRegisterDetail.ResumeLayout(false);
            this.tblGeneralDetail.ResumeLayout(false);
            this.tblGeneralDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFactoryShortName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkActive.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFactoryName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).EndInit();
            this.panelControl6.ResumeLayout(false);
            this.panelControl6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFactoryMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFactoryMaster)).EndInit();
            this.ContextMNExport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
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
        private DevExpress.XtraGrid.Views.Grid.GridView dgvFactoryMaster;
        private DevExpress.XtraGrid.Columns.GridColumn clmFactoryID;
        private DevExpress.XtraGrid.Columns.GridColumn clmFactoryName;
        private DevExpress.XtraGrid.GridControl grdFactoryMaster;
        private DevExpress.XtraTab.XtraTabControl TabRegisterDetail;
        private DevExpress.XtraTab.XtraTabPage tblGeneralDetail;
        private System.Windows.Forms.DataGridViewTextBoxColumn LEDGER_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn LEDGER_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn SHERE_PER;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtFactoryName;
        private DevExpress.XtraGrid.Columns.GridColumn clmActive;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraEditors.LabelControl labelControl15;
        private DevExpress.XtraEditors.CheckEdit chkActive;
        private DevExpress.XtraEditors.LabelControl lblMode;
        private DevExpress.XtraGrid.Columns.GridColumn clmFactoryShortName;
        private System.Windows.Forms.ContextMenuStrip ContextMNExport;
        private System.Windows.Forms.ToolStripMenuItem MNExportExcel;
        private System.Windows.Forms.ToolStripMenuItem MNExportPDF;
        private System.Windows.Forms.ToolStripMenuItem MNExportTEXT;
        private System.Windows.Forms.ToolStripMenuItem MNExportHTML;
        private System.Windows.Forms.ToolStripMenuItem MNExportRTF;
        private System.Windows.Forms.ToolStripMenuItem MNExportCSV;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.TextEdit txtFactoryShortName;
        private DevExpress.XtraEditors.LabelControl labelControl12;
    }
}