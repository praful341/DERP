namespace DERP.Transaction
{
    partial class FrmMFGChangeJangedPartyUtilty
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMFGChangeJangedPartyUtilty));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.panelProgress = new DevExpress.XtraEditors.PanelControl();
            this.lblProgressCount = new System.Windows.Forms.Label();
            this.SaveProgressBar = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            this.grdChangedJangedPartyUtility = new DevExpress.XtraGrid.GridControl();
            this.ContextMNExport = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MNExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportPDF = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportTEXT = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportHTML = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportRTF = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportCSV = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvChangedJangedPartyUtility = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.clmCut = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmKapan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmJanged_No = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ClmPacketTypeWages = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ClmManagerID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ClmRoughSieveID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ClmManager = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ClmFacWagesID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ClmWagesSieve = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ClmManagerID2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ClmManager1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.txtLotID = new DevExpress.XtraEditors.TextEdit();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.btnShow = new DevExpress.XtraEditors.SimpleButton();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelProgress)).BeginInit();
            this.panelProgress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SaveProgressBar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdChangedJangedPartyUtility)).BeginInit();
            this.ContextMNExport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChangedJangedPartyUtility)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotID.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.panelControl4);
            this.panelControl1.Controls.Add(this.panelControl3);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1296, 674);
            this.panelControl1.TabIndex = 0;
            // 
            // panelControl4
            // 
            this.panelControl4.Controls.Add(this.panelProgress);
            this.panelControl4.Controls.Add(this.grdChangedJangedPartyUtility);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl4.Location = new System.Drawing.Point(2, 40);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(1292, 632);
            this.panelControl4.TabIndex = 28;
            // 
            // panelProgress
            // 
            this.panelProgress.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelProgress.Controls.Add(this.lblProgressCount);
            this.panelProgress.Controls.Add(this.SaveProgressBar);
            this.panelProgress.Location = new System.Drawing.Point(503, 261);
            this.panelProgress.Name = "panelProgress";
            this.panelProgress.Size = new System.Drawing.Size(283, 58);
            this.panelProgress.TabIndex = 25;
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
            // grdChangedJangedPartyUtility
            // 
            this.grdChangedJangedPartyUtility.ContextMenuStrip = this.ContextMNExport;
            this.grdChangedJangedPartyUtility.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdChangedJangedPartyUtility.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdChangedJangedPartyUtility.Location = new System.Drawing.Point(2, 2);
            this.grdChangedJangedPartyUtility.MainView = this.dgvChangedJangedPartyUtility;
            this.grdChangedJangedPartyUtility.Name = "grdChangedJangedPartyUtility";
            this.grdChangedJangedPartyUtility.Size = new System.Drawing.Size(1288, 628);
            this.grdChangedJangedPartyUtility.TabIndex = 21;
            this.grdChangedJangedPartyUtility.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvChangedJangedPartyUtility});
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
            // dgvChangedJangedPartyUtility
            // 
            this.dgvChangedJangedPartyUtility.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.dgvChangedJangedPartyUtility.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.dgvChangedJangedPartyUtility.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            this.dgvChangedJangedPartyUtility.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            this.dgvChangedJangedPartyUtility.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.dgvChangedJangedPartyUtility.Appearance.FooterPanel.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.dgvChangedJangedPartyUtility.Appearance.FooterPanel.Options.UseFont = true;
            this.dgvChangedJangedPartyUtility.Appearance.HeaderPanel.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.dgvChangedJangedPartyUtility.Appearance.HeaderPanel.Options.UseFont = true;
            this.dgvChangedJangedPartyUtility.Appearance.Row.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold);
            this.dgvChangedJangedPartyUtility.Appearance.Row.Options.UseFont = true;
            this.dgvChangedJangedPartyUtility.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.clmCut,
            this.clmKapan,
            this.clmJanged_No,
            this.ClmPacketTypeWages,
            this.ClmManagerID,
            this.ClmRoughSieveID,
            this.ClmManager,
            this.ClmFacWagesID,
            this.ClmWagesSieve,
            this.ClmManagerID2,
            this.ClmManager1});
            this.dgvChangedJangedPartyUtility.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.dgvChangedJangedPartyUtility.GridControl = this.grdChangedJangedPartyUtility;
            this.dgvChangedJangedPartyUtility.Name = "dgvChangedJangedPartyUtility";
            this.dgvChangedJangedPartyUtility.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.dgvChangedJangedPartyUtility.OptionsBehavior.Editable = false;
            this.dgvChangedJangedPartyUtility.OptionsCustomization.AllowGroup = false;
            this.dgvChangedJangedPartyUtility.OptionsCustomization.AllowSort = false;
            this.dgvChangedJangedPartyUtility.OptionsNavigation.EnterMoveNextColumn = true;
            this.dgvChangedJangedPartyUtility.OptionsView.ColumnAutoWidth = false;
            this.dgvChangedJangedPartyUtility.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.dgvChangedJangedPartyUtility.OptionsView.ShowAutoFilterRow = true;
            this.dgvChangedJangedPartyUtility.OptionsView.ShowFooter = true;
            this.dgvChangedJangedPartyUtility.OptionsView.ShowGroupPanel = false;
            this.dgvChangedJangedPartyUtility.ColumnFilterChanged += new System.EventHandler(this.dgvDepartmentTransferConfirm_ColumnFilterChanged);
            // 
            // clmCut
            // 
            this.clmCut.Caption = "Cut No";
            this.clmCut.FieldName = "rough_cut_no";
            this.clmCut.Name = "clmCut";
            this.clmCut.OptionsColumn.AllowEdit = false;
            this.clmCut.OptionsColumn.AllowFocus = false;
            this.clmCut.OptionsColumn.AllowMove = false;
            this.clmCut.Visible = true;
            this.clmCut.VisibleIndex = 4;
            this.clmCut.Width = 131;
            // 
            // clmKapan
            // 
            this.clmKapan.Caption = "Kapan No";
            this.clmKapan.FieldName = "kapan_no";
            this.clmKapan.Name = "clmKapan";
            this.clmKapan.OptionsColumn.AllowEdit = false;
            this.clmKapan.OptionsColumn.AllowFocus = false;
            this.clmKapan.OptionsColumn.AllowMove = false;
            this.clmKapan.Visible = true;
            this.clmKapan.VisibleIndex = 3;
            this.clmKapan.Width = 93;
            // 
            // clmJanged_No
            // 
            this.clmJanged_No.Caption = "Janged ID";
            this.clmJanged_No.FieldName = "janged_id";
            this.clmJanged_No.Name = "clmJanged_No";
            this.clmJanged_No.OptionsColumn.AllowEdit = false;
            this.clmJanged_No.OptionsColumn.AllowFocus = false;
            this.clmJanged_No.OptionsColumn.AllowMove = false;
            this.clmJanged_No.Visible = true;
            this.clmJanged_No.VisibleIndex = 1;
            // 
            // ClmPacketTypeWages
            // 
            this.ClmPacketTypeWages.Caption = "Lot SrNo";
            this.ClmPacketTypeWages.FieldName = "lot_srno";
            this.ClmPacketTypeWages.Name = "ClmPacketTypeWages";
            this.ClmPacketTypeWages.Visible = true;
            this.ClmPacketTypeWages.VisibleIndex = 0;
            this.ClmPacketTypeWages.Width = 83;
            // 
            // ClmManagerID
            // 
            this.ClmManagerID.Caption = "Lot ID";
            this.ClmManagerID.FieldName = "lot_id";
            this.ClmManagerID.Name = "ClmManagerID";
            this.ClmManagerID.OptionsColumn.AllowEdit = false;
            this.ClmManagerID.OptionsColumn.AllowFocus = false;
            this.ClmManagerID.OptionsColumn.AllowMove = false;
            this.ClmManagerID.Visible = true;
            this.ClmManagerID.VisibleIndex = 2;
            // 
            // ClmRoughSieveID
            // 
            this.ClmRoughSieveID.Caption = "Previous Party";
            this.ClmRoughSieveID.FieldName = "previous_party_name";
            this.ClmRoughSieveID.Name = "ClmRoughSieveID";
            this.ClmRoughSieveID.OptionsColumn.AllowEdit = false;
            this.ClmRoughSieveID.OptionsColumn.AllowFocus = false;
            this.ClmRoughSieveID.OptionsColumn.AllowMove = false;
            this.ClmRoughSieveID.Visible = true;
            this.ClmRoughSieveID.VisibleIndex = 5;
            this.ClmRoughSieveID.Width = 137;
            // 
            // ClmManager
            // 
            this.ClmManager.Caption = "Upd Party";
            this.ClmManager.FieldName = "party_name";
            this.ClmManager.Name = "ClmManager";
            this.ClmManager.OptionsColumn.AllowEdit = false;
            this.ClmManager.OptionsColumn.AllowFocus = false;
            this.ClmManager.OptionsColumn.AllowMove = false;
            this.ClmManager.Visible = true;
            this.ClmManager.VisibleIndex = 6;
            this.ClmManager.Width = 111;
            // 
            // ClmFacWagesID
            // 
            this.ClmFacWagesID.Caption = "Updated User";
            this.ClmFacWagesID.FieldName = "user_name";
            this.ClmFacWagesID.Name = "ClmFacWagesID";
            this.ClmFacWagesID.Visible = true;
            this.ClmFacWagesID.VisibleIndex = 7;
            this.ClmFacWagesID.Width = 131;
            // 
            // ClmWagesSieve
            // 
            this.ClmWagesSieve.Caption = "Entry Date";
            this.ClmWagesSieve.FieldName = "entry_date";
            this.ClmWagesSieve.Name = "ClmWagesSieve";
            this.ClmWagesSieve.Visible = true;
            this.ClmWagesSieve.VisibleIndex = 8;
            this.ClmWagesSieve.Width = 106;
            // 
            // ClmManagerID2
            // 
            this.ClmManagerID2.Caption = "Entry Time";
            this.ClmManagerID2.FieldName = "entry_time";
            this.ClmManagerID2.Name = "ClmManagerID2";
            this.ClmManagerID2.Visible = true;
            this.ClmManagerID2.VisibleIndex = 9;
            this.ClmManagerID2.Width = 114;
            // 
            // ClmManager1
            // 
            this.ClmManager1.Caption = "IP Address";
            this.ClmManager1.FieldName = "ip_address";
            this.ClmManager1.Name = "ClmManager1";
            this.ClmManager1.Visible = true;
            this.ClmManager1.VisibleIndex = 10;
            this.ClmManager1.Width = 144;
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.btnClear);
            this.panelControl3.Controls.Add(this.txtLotID);
            this.panelControl3.Controls.Add(this.btnExit);
            this.panelControl3.Controls.Add(this.labelControl4);
            this.panelControl3.Controls.Add(this.btnShow);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl3.Location = new System.Drawing.Point(2, 2);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(1292, 38);
            this.panelControl3.TabIndex = 27;
            // 
            // txtLotID
            // 
            this.txtLotID.EditValue = "";
            this.txtLotID.EnterMoveNextControl = true;
            this.txtLotID.Location = new System.Drawing.Point(58, 7);
            this.txtLotID.Name = "txtLotID";
            this.txtLotID.Properties.AccessibleName = "EMPLOYEE";
            this.txtLotID.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLotID.Properties.Appearance.Options.UseFont = true;
            this.txtLotID.Size = new System.Drawing.Size(129, 22);
            this.txtLotID.TabIndex = 0;
            this.txtLotID.ToolTip = "Enter Employee Name";
            this.txtLotID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLotID_KeyPress);
            this.txtLotID.Validated += new System.EventHandler(this.txtLotID_Validated);
            // 
            // btnExit
            // 
            this.btnExit.Appearance.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Appearance.Options.UseFont = true;
            this.btnExit.ImageOptions.Image = global::DERP.Properties.Resources.Exit;
            this.btnExit.Location = new System.Drawing.Point(412, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(102, 32);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "E&xit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(10, 10);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(35, 15);
            this.labelControl4.TabIndex = 590;
            this.labelControl4.Text = "Lot Id";
            // 
            // btnShow
            // 
            this.btnShow.Appearance.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShow.Appearance.Options.UseFont = true;
            this.btnShow.ImageOptions.Image = global::DERP.Properties.Resources.Show;
            this.btnShow.Location = new System.Drawing.Point(197, 3);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(101, 32);
            this.btnShow.TabIndex = 1;
            this.btnShow.Text = "Show";
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // btnClear
            // 
            this.btnClear.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Appearance.Options.UseFont = true;
            this.btnClear.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.ImageOptions.Image")));
            this.btnClear.Location = new System.Drawing.Point(304, 3);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(102, 32);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "&Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // FrmMFGChangeJangedPartyUtilty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1296, 674);
            this.Controls.Add(this.panelControl1);
            this.Name = "FrmMFGChangeJangedPartyUtilty";
            this.Text = "Changed Janged Party Master";
            this.Load += new System.EventHandler(this.FrmMFGChangeJangedPartyUtilty_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelProgress)).EndInit();
            this.panelProgress.ResumeLayout(false);
            this.panelProgress.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SaveProgressBar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdChangedJangedPartyUtility)).EndInit();
            this.ContextMNExport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChangedJangedPartyUtility)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotID.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.GridControl grdChangedJangedPartyUtility;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvChangedJangedPartyUtility;
        private DevExpress.XtraEditors.PanelControl panelProgress;
        private System.Windows.Forms.Label lblProgressCount;
        private DevExpress.XtraEditors.MarqueeProgressBarControl SaveProgressBar;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraGrid.Columns.GridColumn clmCut;
        private System.Windows.Forms.ContextMenuStrip ContextMNExport;
        private System.Windows.Forms.ToolStripMenuItem MNExportExcel;
        private System.Windows.Forms.ToolStripMenuItem MNExportPDF;
        private System.Windows.Forms.ToolStripMenuItem MNExportTEXT;
        private System.Windows.Forms.ToolStripMenuItem MNExportHTML;
        private System.Windows.Forms.ToolStripMenuItem MNExportRTF;
        private System.Windows.Forms.ToolStripMenuItem MNExportCSV;
        private DevExpress.XtraGrid.Columns.GridColumn clmKapan;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraGrid.Columns.GridColumn clmJanged_No;
        private DevExpress.XtraGrid.Columns.GridColumn ClmPacketTypeWages;
        private DevExpress.XtraGrid.Columns.GridColumn ClmManagerID;
        private DevExpress.XtraGrid.Columns.GridColumn ClmRoughSieveID;
        private DevExpress.XtraGrid.Columns.GridColumn ClmManager;
        private DevExpress.XtraGrid.Columns.GridColumn ClmFacWagesID;
        private DevExpress.XtraGrid.Columns.GridColumn ClmWagesSieve;
        private DevExpress.XtraGrid.Columns.GridColumn ClmManagerID2;
        private DevExpress.XtraGrid.Columns.GridColumn ClmManager1;
        private DevExpress.XtraEditors.SimpleButton btnShow;
        private DevExpress.XtraEditors.TextEdit txtLotID;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SimpleButton btnClear;
    }
}