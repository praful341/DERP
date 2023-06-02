namespace DERP.Master
{
    partial class FrmConfigFormDetails
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.grdRolePermissionMaster = new DevExpress.XtraGrid.GridControl();
            this.dgvRolePermissionMaster = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.clmfield_no = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmColumnName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmActive = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmcaption = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmis_visible = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmis_compulsary = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmiseditable = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmtab_index = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmgrid_name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmcolumn_type = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmis_control = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmis_newrow = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lueRole = new DevExpress.XtraEditors.LookUpEdit();
            this.lueFormName = new DevExpress.XtraEditors.LookUpEdit();
            this.btnShow = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.ContextMNExport = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MNExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportPDF = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportTEXT = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportHTML = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportRTF = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportCSV = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdRolePermissionMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRolePermissionMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueRole.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueFormName.Properties)).BeginInit();
            this.ContextMNExport.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnExit);
            this.panelControl2.Controls.Add(this.btnClear);
            this.panelControl2.Controls.Add(this.btnSave);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 537);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(911, 50);
            this.panelControl2.TabIndex = 19;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Appearance.Options.UseFont = true;
            this.btnExit.ImageOptions.Image = global::DERP.Properties.Resources.Exit;
            this.btnExit.Location = new System.Drawing.Point(801, 9);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(102, 32);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "E&xit";
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Appearance.Options.UseFont = true;
            this.btnClear.ImageOptions.Image = global::DERP.Properties.Resources.Clear;
            this.btnClear.Location = new System.Drawing.Point(693, 9);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(102, 32);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "&Clear";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.ImageOptions.Image = global::DERP.Properties.Resources.Save;
            this.btnSave.Location = new System.Drawing.Point(585, 9);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(102, 32);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "&Save";
            // 
            // grdRolePermissionMaster
            // 
            this.grdRolePermissionMaster.ContextMenuStrip = this.ContextMNExport;
            this.grdRolePermissionMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdRolePermissionMaster.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridLevelNode1.RelationName = "Level1";
            this.grdRolePermissionMaster.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.grdRolePermissionMaster.Location = new System.Drawing.Point(0, 44);
            this.grdRolePermissionMaster.MainView = this.dgvRolePermissionMaster;
            this.grdRolePermissionMaster.Name = "grdRolePermissionMaster";
            this.grdRolePermissionMaster.Size = new System.Drawing.Size(911, 543);
            this.grdRolePermissionMaster.TabIndex = 20;
            this.grdRolePermissionMaster.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvRolePermissionMaster});
            // 
            // dgvRolePermissionMaster
            // 
            this.dgvRolePermissionMaster.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.dgvRolePermissionMaster.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.dgvRolePermissionMaster.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            this.dgvRolePermissionMaster.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            this.dgvRolePermissionMaster.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.dgvRolePermissionMaster.Appearance.FooterPanel.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.dgvRolePermissionMaster.Appearance.FooterPanel.Options.UseFont = true;
            this.dgvRolePermissionMaster.Appearance.HeaderPanel.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.dgvRolePermissionMaster.Appearance.HeaderPanel.Options.UseFont = true;
            this.dgvRolePermissionMaster.Appearance.Row.Font = new System.Drawing.Font("Cambria", 9F);
            this.dgvRolePermissionMaster.Appearance.Row.Options.UseFont = true;
            this.dgvRolePermissionMaster.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.clmfield_no,
            this.clmColumnName,
            this.clmActive,
            this.clmcaption,
            this.clmis_visible,
            this.clmis_compulsary,
            this.clmiseditable,
            this.clmtab_index,
            this.clmgrid_name,
            this.clmcolumn_type,
            this.clmis_control,
            this.clmis_newrow});
            this.dgvRolePermissionMaster.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.dgvRolePermissionMaster.GridControl = this.grdRolePermissionMaster;
            this.dgvRolePermissionMaster.Name = "dgvRolePermissionMaster";
            this.dgvRolePermissionMaster.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.dgvRolePermissionMaster.OptionsCustomization.AllowColumnMoving = false;
            this.dgvRolePermissionMaster.OptionsCustomization.AllowFilter = false;
            this.dgvRolePermissionMaster.OptionsCustomization.AllowGroup = false;
            this.dgvRolePermissionMaster.OptionsCustomization.AllowSort = false;
            this.dgvRolePermissionMaster.OptionsNavigation.EnterMoveNextColumn = true;
            this.dgvRolePermissionMaster.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.dgvRolePermissionMaster.OptionsView.ShowAutoFilterRow = true;
            this.dgvRolePermissionMaster.OptionsView.ShowFooter = true;
            this.dgvRolePermissionMaster.OptionsView.ShowGroupPanel = false;
            // 
            // clmfield_no
            // 
            this.clmfield_no.Caption = "Field No";
            this.clmfield_no.FieldName = "fiel_no";
            this.clmfield_no.Name = "clmfield_no";
            this.clmfield_no.OptionsColumn.AllowEdit = false;
            this.clmfield_no.Visible = true;
            this.clmfield_no.VisibleIndex = 0;
            // 
            // clmColumnName
            // 
            this.clmColumnName.AppearanceHeader.Options.UseTextOptions = true;
            this.clmColumnName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clmColumnName.Caption = "Column Name";
            this.clmColumnName.FieldName = "column_name";
            this.clmColumnName.Name = "clmColumnName";
            this.clmColumnName.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.clmColumnName.Visible = true;
            this.clmColumnName.VisibleIndex = 1;
            this.clmColumnName.Width = 260;
            // 
            // clmActive
            // 
            this.clmActive.Caption = "Active";
            this.clmActive.FieldName = "isdeleted";
            this.clmActive.Name = "clmActive";
            // 
            // clmcaption
            // 
            this.clmcaption.Caption = "Caption";
            this.clmcaption.FieldName = "caption";
            this.clmcaption.Name = "clmcaption";
            this.clmcaption.Visible = true;
            this.clmcaption.VisibleIndex = 2;
            // 
            // clmis_visible
            // 
            this.clmis_visible.Caption = "Visible";
            this.clmis_visible.FieldName = "is_visible";
            this.clmis_visible.Name = "clmis_visible";
            this.clmis_visible.Visible = true;
            this.clmis_visible.VisibleIndex = 3;
            // 
            // clmis_compulsary
            // 
            this.clmis_compulsary.Caption = "Compulsary";
            this.clmis_compulsary.FieldName = "is_compulsary";
            this.clmis_compulsary.Name = "clmis_compulsary";
            this.clmis_compulsary.Visible = true;
            this.clmis_compulsary.VisibleIndex = 4;
            // 
            // clmiseditable
            // 
            this.clmiseditable.Caption = "Editable";
            this.clmiseditable.FieldName = "is_editable";
            this.clmiseditable.Name = "clmiseditable";
            this.clmiseditable.Visible = true;
            this.clmiseditable.VisibleIndex = 5;
            // 
            // clmtab_index
            // 
            this.clmtab_index.Caption = "Tab Index";
            this.clmtab_index.FieldName = "tab_index";
            this.clmtab_index.Name = "clmtab_index";
            this.clmtab_index.Visible = true;
            this.clmtab_index.VisibleIndex = 6;
            // 
            // clmgrid_name
            // 
            this.clmgrid_name.Caption = "Grid Name";
            this.clmgrid_name.FieldName = "grid_name";
            this.clmgrid_name.Name = "clmgrid_name";
            this.clmgrid_name.Visible = true;
            this.clmgrid_name.VisibleIndex = 7;
            // 
            // clmcolumn_type
            // 
            this.clmcolumn_type.Caption = "Column Type";
            this.clmcolumn_type.FieldName = "column_type";
            this.clmcolumn_type.Name = "clmcolumn_type";
            this.clmcolumn_type.Visible = true;
            this.clmcolumn_type.VisibleIndex = 8;
            // 
            // clmis_control
            // 
            this.clmis_control.Caption = "Control";
            this.clmis_control.FieldName = "is_control";
            this.clmis_control.Name = "clmis_control";
            this.clmis_control.Visible = true;
            this.clmis_control.VisibleIndex = 9;
            // 
            // clmis_newrow
            // 
            this.clmis_newrow.Caption = "Newrow";
            this.clmis_newrow.FieldName = "is_newrow";
            this.clmis_newrow.Name = "clmis_newrow";
            this.clmis_newrow.Visible = true;
            this.clmis_newrow.VisibleIndex = 10;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lueRole);
            this.panelControl1.Controls.Add(this.lueFormName);
            this.panelControl1.Controls.Add(this.btnShow);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(911, 44);
            this.panelControl1.TabIndex = 18;
            // 
            // lueRole
            // 
            this.lueRole.EnterMoveNextControl = true;
            this.lueRole.Location = new System.Drawing.Point(280, 10);
            this.lueRole.Name = "lueRole";
            this.lueRole.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold);
            this.lueRole.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.lueRole.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.lueRole.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueRole.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("role_name", "Role Name"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("role_id", "Role Id", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lueRole.Properties.NullText = "";
            this.lueRole.Size = new System.Drawing.Size(145, 20);
            this.lueRole.TabIndex = 426;
            // 
            // lueFormName
            // 
            this.lueFormName.EnterMoveNextControl = true;
            this.lueFormName.Location = new System.Drawing.Point(78, 10);
            this.lueFormName.Name = "lueFormName";
            this.lueFormName.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold);
            this.lueFormName.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.lueFormName.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.lueFormName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueFormName.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("form_name", "Form Name"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("form_id", "Form Id", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lueFormName.Properties.NullText = "";
            this.lueFormName.Size = new System.Drawing.Size(157, 20);
            this.lueFormName.TabIndex = 425;
            // 
            // btnShow
            // 
            this.btnShow.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShow.Appearance.Options.UseFont = true;
            this.btnShow.ImageOptions.Image = global::DERP.Properties.Resources.Show;
            this.btnShow.Location = new System.Drawing.Point(447, 4);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(102, 32);
            this.btnShow.TabIndex = 0;
            this.btnShow.Text = "&Show";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(246, 14);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(21, 13);
            this.labelControl1.TabIndex = 421;
            this.labelControl1.Text = "Role";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(11, 14);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(54, 13);
            this.labelControl2.TabIndex = 420;
            this.labelControl2.Text = "Form Name";
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
            // FrmConfigFormDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(911, 587);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.grdRolePermissionMaster);
            this.Controls.Add(this.panelControl1);
            this.Name = "FrmConfigFormDetails";
            this.Text = "Config Form Details";
            this.Load += new System.EventHandler(this.FrmConfigFormDetails_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdRolePermissionMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRolePermissionMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueRole.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueFormName.Properties)).EndInit();
            this.ContextMNExport.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraGrid.GridControl grdRolePermissionMaster;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvRolePermissionMaster;
        private DevExpress.XtraGrid.Columns.GridColumn clmColumnName;
        private DevExpress.XtraGrid.Columns.GridColumn clmActive;
        private DevExpress.XtraGrid.Columns.GridColumn clmcaption;
        private DevExpress.XtraGrid.Columns.GridColumn clmis_visible;
        private DevExpress.XtraGrid.Columns.GridColumn clmis_compulsary;
        private DevExpress.XtraGrid.Columns.GridColumn clmiseditable;
        private DevExpress.XtraGrid.Columns.GridColumn clmtab_index;
        private DevExpress.XtraGrid.Columns.GridColumn clmgrid_name;
        private DevExpress.XtraGrid.Columns.GridColumn clmcolumn_type;
        private DevExpress.XtraGrid.Columns.GridColumn clmis_control;
        private DevExpress.XtraGrid.Columns.GridColumn clmis_newrow;
        private DevExpress.XtraGrid.Columns.GridColumn clmfield_no;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LookUpEdit lueRole;
        private DevExpress.XtraEditors.LookUpEdit lueFormName;
        private DevExpress.XtraEditors.SimpleButton btnShow;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.ContextMenuStrip ContextMNExport;
        private System.Windows.Forms.ToolStripMenuItem MNExportExcel;
        private System.Windows.Forms.ToolStripMenuItem MNExportPDF;
        private System.Windows.Forms.ToolStripMenuItem MNExportTEXT;
        private System.Windows.Forms.ToolStripMenuItem MNExportHTML;
        private System.Windows.Forms.ToolStripMenuItem MNExportRTF;
        private System.Windows.Forms.ToolStripMenuItem MNExportCSV;
    }
}