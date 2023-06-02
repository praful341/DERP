namespace DERP.Transaction.MFG
{
    partial class FrmMFGPataLotEntryStock
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
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.txtSelCarat = new DevExpress.XtraEditors.TextEdit();
            this.txtSelLot = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnOk = new DevExpress.XtraEditors.SimpleButton();
            this.MainGrid = new DevExpress.XtraGrid.GridControl();
            this.ContextMNExport = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MNExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportPDF = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportTEXT = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportHTML = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportRTF = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportCSV = new System.Windows.Forms.ToolStripMenuItem();
            this.GrdDet = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repSelect = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.ClmKapanNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ClmKapanID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ClmTotalPcs = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ClmCarat = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ClmRoughCutNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ClmRoughCutID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ClmLotSrNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ClmReceiveDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ClmEntryBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ClmComputerIP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ChkAll = new DevExpress.XtraEditors.CheckEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.BtnExit1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.lueKapan = new DevExpress.XtraEditors.LookUpEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.lueCutNo = new DevExpress.XtraEditors.LookUpEdit();
            this.lblKapanNo = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpToDate = new DevExpress.XtraEditors.DateEdit();
            this.dtpFromDate = new DevExpress.XtraEditors.DateEdit();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.backgroundWorker_AssortFinalStock = new System.ComponentModel.BackgroundWorker();
            this.ClmEntryDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ClmEntryTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSelCarat.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSelLot.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).BeginInit();
            this.ContextMNExport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChkAll.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueKapan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueCutNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpToDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpToDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel5
            // 
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(10, 562);
            this.panel5.TabIndex = 11;
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(1074, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(10, 562);
            this.panel3.TabIndex = 12;
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(10, 552);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1064, 10);
            this.panel4.TabIndex = 13;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BtnPrint);
            this.panel1.Controls.Add(this.txtSelCarat);
            this.panel1.Controls.Add(this.txtSelLot);
            this.panel1.Controls.Add(this.labelControl2);
            this.panel1.Controls.Add(this.labelControl1);
            this.panel1.Controls.Add(this.BtnExit);
            this.panel1.Controls.Add(this.BtnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(10, 511);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1064, 41);
            this.panel1.TabIndex = 0;
            // 
            // BtnPrint
            // 
            this.BtnPrint.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.BtnPrint.Appearance.Options.UseFont = true;
            this.BtnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnPrint.ImageOptions.Image = global::DERP.Properties.Resources.Print;
            this.BtnPrint.Location = new System.Drawing.Point(733, 6);
            this.BtnPrint.Name = "BtnPrint";
            this.BtnPrint.Size = new System.Drawing.Size(102, 31);
            this.BtnPrint.TabIndex = 23;
            this.BtnPrint.Text = "&Print";
            this.BtnPrint.ToolTip = "Click TO Calculate EMI As Per Month Bases & Suggested List Also";
            this.BtnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // txtSelCarat
            // 
            this.txtSelCarat.EditValue = "";
            this.txtSelCarat.Location = new System.Drawing.Point(212, 6);
            this.txtSelCarat.Name = "txtSelCarat";
            this.txtSelCarat.Properties.AccessibleName = "EMPLOYEE";
            this.txtSelCarat.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSelCarat.Properties.Appearance.Options.UseFont = true;
            this.txtSelCarat.Size = new System.Drawing.Size(68, 22);
            this.txtSelCarat.TabIndex = 22;
            this.txtSelCarat.ToolTip = "Enter Employee Name";
            // 
            // txtSelLot
            // 
            this.txtSelLot.EditValue = "";
            this.txtSelLot.Location = new System.Drawing.Point(96, 6);
            this.txtSelLot.Name = "txtSelLot";
            this.txtSelLot.Properties.AccessibleName = "EMPLOYEE";
            this.txtSelLot.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSelLot.Properties.Appearance.Options.UseFont = true;
            this.txtSelLot.Size = new System.Drawing.Size(68, 22);
            this.txtSelLot.TabIndex = 20;
            this.txtSelLot.ToolTip = "Enter Employee Name";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(170, 9);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(36, 14);
            this.labelControl2.TabIndex = 18;
            this.labelControl2.Text = "Carat";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(9, 9);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(82, 14);
            this.labelControl1.TabIndex = 17;
            this.labelControl1.Text = "Selected Lot";
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnExit.ImageOptions.Image = global::DERP.Properties.Resources.Exit;
            this.BtnExit.Location = new System.Drawing.Point(949, 6);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(102, 31);
            this.BtnExit.TabIndex = 14;
            this.BtnExit.Text = "E&xit";
            this.BtnExit.ToolTip = "Click TO Calculate EMI As Per Month Bases & Suggested List Also";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnOk
            // 
            this.BtnOk.Appearance.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.BtnOk.Appearance.Options.UseFont = true;
            this.BtnOk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnOk.ImageOptions.Image = global::DERP.Properties.Resources.Show;
            this.BtnOk.Location = new System.Drawing.Point(841, 6);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(102, 31);
            this.BtnOk.TabIndex = 14;
            this.BtnOk.Text = "&Ok";
            this.BtnOk.ToolTip = "Click TO Calculate EMI As Per Month Bases & Suggested List Also";
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // MainGrid
            // 
            this.MainGrid.ContextMenuStrip = this.ContextMNExport;
            this.MainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGrid.Location = new System.Drawing.Point(2, 2);
            this.MainGrid.MainView = this.GrdDet;
            this.MainGrid.Name = "MainGrid";
            this.MainGrid.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repSelect});
            this.MainGrid.Size = new System.Drawing.Size(1060, 456);
            this.MainGrid.TabIndex = 1;
            this.MainGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDet});
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
            // GrdDet
            // 
            this.GrdDet.Appearance.FocusedCell.Font = new System.Drawing.Font("Verdana", 9F);
            this.GrdDet.Appearance.FocusedCell.Options.UseFont = true;
            this.GrdDet.Appearance.FocusedRow.Font = new System.Drawing.Font("Verdana", 9F);
            this.GrdDet.Appearance.FocusedRow.Options.UseFont = true;
            this.GrdDet.Appearance.FooterPanel.Font = new System.Drawing.Font("Cambria", 10F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.FooterPanel.Options.UseFont = true;
            this.GrdDet.Appearance.HeaderPanel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.HeaderPanel.Options.UseFont = true;
            this.GrdDet.Appearance.Row.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold);
            this.GrdDet.Appearance.Row.Options.UseFont = true;
            this.GrdDet.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.GrdDet.Appearance.SelectedRow.Font = new System.Drawing.Font("Tahoma", 9F);
            this.GrdDet.Appearance.SelectedRow.Options.UseBackColor = true;
            this.GrdDet.Appearance.SelectedRow.Options.UseFont = true;
            this.GrdDet.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.ClmKapanNo,
            this.ClmKapanID,
            this.ClmTotalPcs,
            this.ClmCarat,
            this.ClmRoughCutNo,
            this.ClmRoughCutID,
            this.ClmLotSrNo,
            this.ClmReceiveDate,
            this.ClmEntryBy,
            this.ClmComputerIP,
            this.ClmEntryTime,
            this.ClmEntryDate});
            this.GrdDet.GridControl = this.MainGrid;
            this.GrdDet.Name = "GrdDet";
            this.GrdDet.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.GrdDet.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            this.GrdDet.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.GrdDet.OptionsBehavior.FocusLeaveOnTab = true;
            this.GrdDet.OptionsFilter.ShowAllTableValuesInFilterPopup = true;
            this.GrdDet.OptionsFilter.UseNewCustomFilterDialog = true;
            this.GrdDet.OptionsFind.FindDelay = 100;
            this.GrdDet.OptionsFind.FindFilterColumns = "";
            this.GrdDet.OptionsFind.HighlightFindResults = false;
            this.GrdDet.OptionsFind.SearchInPreview = true;
            this.GrdDet.OptionsFind.ShowCloseButton = false;
            this.GrdDet.OptionsSelection.MultiSelect = true;
            this.GrdDet.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.GrdDet.OptionsView.ColumnAutoWidth = false;
            this.GrdDet.OptionsView.ShowAutoFilterRow = true;
            this.GrdDet.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways;
            this.GrdDet.OptionsView.ShowFooter = true;
            this.GrdDet.OptionsView.ShowGroupPanel = false;
            this.GrdDet.OptionsView.ShowPreviewRowLines = DevExpress.Utils.DefaultBoolean.True;
            this.GrdDet.CustomSummaryCalculate += new DevExpress.Data.CustomSummaryEventHandler(this.GrdDet_CustomSummaryCalculate);
            this.GrdDet.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.GrdDet_CellValueChanging);
            this.GrdDet.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GrdDet_KeyUp);
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Font = new System.Drawing.Font("Verdana", 9F);
            this.gridColumn1.AppearanceCell.Options.UseFont = true;
            this.gridColumn1.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn1.AppearanceHeader.Options.UseFont = true;
            this.gridColumn1.Caption = " --";
            this.gridColumn1.ColumnEdit = this.repSelect;
            this.gridColumn1.FieldName = "SEL";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn1.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 31;
            // 
            // repSelect
            // 
            this.repSelect.AutoHeight = false;
            this.repSelect.Caption = "Check";
            this.repSelect.Name = "repSelect";
            this.repSelect.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.repSelect.ValueGrayed = false;
            this.repSelect.QueryValueByCheckState += new DevExpress.XtraEditors.Controls.QueryValueByCheckStateEventHandler(this.repSelect_QueryValueByCheckState);
            this.repSelect.CheckedChanged += new System.EventHandler(this.repSelect_CheckedChanged);
            this.repSelect.MouseUp += new System.Windows.Forms.MouseEventHandler(this.repSelect_MouseUp);
            // 
            // ClmKapanNo
            // 
            this.ClmKapanNo.AppearanceCell.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold);
            this.ClmKapanNo.AppearanceCell.Options.UseFont = true;
            this.ClmKapanNo.AppearanceCell.Options.UseTextOptions = true;
            this.ClmKapanNo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ClmKapanNo.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.ClmKapanNo.AppearanceHeader.Options.UseFont = true;
            this.ClmKapanNo.Caption = "Kapan No";
            this.ClmKapanNo.FieldName = "kapan_no";
            this.ClmKapanNo.Name = "ClmKapanNo";
            this.ClmKapanNo.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.ClmKapanNo.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.ClmKapanNo.Visible = true;
            this.ClmKapanNo.VisibleIndex = 3;
            this.ClmKapanNo.Width = 100;
            // 
            // ClmKapanID
            // 
            this.ClmKapanID.AppearanceCell.Font = new System.Drawing.Font("Verdana", 9F);
            this.ClmKapanID.AppearanceCell.Options.UseFont = true;
            this.ClmKapanID.AppearanceCell.Options.UseTextOptions = true;
            this.ClmKapanID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ClmKapanID.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.ClmKapanID.AppearanceHeader.Options.UseFont = true;
            this.ClmKapanID.Caption = "Kapan_Id";
            this.ClmKapanID.FieldName = "kapan_id";
            this.ClmKapanID.Name = "ClmKapanID";
            this.ClmKapanID.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.ClmKapanID.Width = 99;
            // 
            // ClmTotalPcs
            // 
            this.ClmTotalPcs.Caption = "Total Pcs";
            this.ClmTotalPcs.FieldName = "total_pcs";
            this.ClmTotalPcs.Name = "ClmTotalPcs";
            this.ClmTotalPcs.Visible = true;
            this.ClmTotalPcs.VisibleIndex = 5;
            // 
            // ClmCarat
            // 
            this.ClmCarat.AppearanceCell.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold);
            this.ClmCarat.AppearanceCell.Options.UseFont = true;
            this.ClmCarat.AppearanceCell.Options.UseTextOptions = true;
            this.ClmCarat.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.ClmCarat.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.ClmCarat.AppearanceHeader.Options.UseFont = true;
            this.ClmCarat.Caption = "Carat";
            this.ClmCarat.FieldName = "total_carat";
            this.ClmCarat.Name = "ClmCarat";
            this.ClmCarat.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.ClmCarat.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "carat", "{0:N3}")});
            this.ClmCarat.Visible = true;
            this.ClmCarat.VisibleIndex = 6;
            this.ClmCarat.Width = 66;
            // 
            // ClmRoughCutNo
            // 
            this.ClmRoughCutNo.AppearanceCell.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold);
            this.ClmRoughCutNo.AppearanceCell.Options.UseFont = true;
            this.ClmRoughCutNo.AppearanceCell.Options.UseTextOptions = true;
            this.ClmRoughCutNo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ClmRoughCutNo.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.ClmRoughCutNo.AppearanceHeader.Options.UseFont = true;
            this.ClmRoughCutNo.Caption = "Cut No";
            this.ClmRoughCutNo.FieldName = "rough_cut_no";
            this.ClmRoughCutNo.Name = "ClmRoughCutNo";
            this.ClmRoughCutNo.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.ClmRoughCutNo.Visible = true;
            this.ClmRoughCutNo.VisibleIndex = 4;
            this.ClmRoughCutNo.Width = 129;
            // 
            // ClmRoughCutID
            // 
            this.ClmRoughCutID.AppearanceCell.Font = new System.Drawing.Font("Verdana", 9F);
            this.ClmRoughCutID.AppearanceCell.Options.UseFont = true;
            this.ClmRoughCutID.AppearanceCell.Options.UseTextOptions = true;
            this.ClmRoughCutID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ClmRoughCutID.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.ClmRoughCutID.AppearanceHeader.Options.UseFont = true;
            this.ClmRoughCutID.Caption = "Cut Id";
            this.ClmRoughCutID.FieldName = "rough_cut_id";
            this.ClmRoughCutID.Name = "ClmRoughCutID";
            this.ClmRoughCutID.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.ClmRoughCutID.Width = 113;
            // 
            // ClmLotSrNo
            // 
            this.ClmLotSrNo.Caption = "Lot SrNo";
            this.ClmLotSrNo.FieldName = "lot_srno";
            this.ClmLotSrNo.Name = "ClmLotSrNo";
            this.ClmLotSrNo.Visible = true;
            this.ClmLotSrNo.VisibleIndex = 2;
            this.ClmLotSrNo.Width = 79;
            // 
            // ClmReceiveDate
            // 
            this.ClmReceiveDate.Caption = "Date";
            this.ClmReceiveDate.FieldName = "mix_date";
            this.ClmReceiveDate.Name = "ClmReceiveDate";
            this.ClmReceiveDate.Visible = true;
            this.ClmReceiveDate.VisibleIndex = 1;
            this.ClmReceiveDate.Width = 97;
            // 
            // ClmEntryBy
            // 
            this.ClmEntryBy.Caption = "Entry By";
            this.ClmEntryBy.FieldName = "entry_by";
            this.ClmEntryBy.Name = "ClmEntryBy";
            this.ClmEntryBy.Visible = true;
            this.ClmEntryBy.VisibleIndex = 7;
            // 
            // ClmComputerIP
            // 
            this.ClmComputerIP.Caption = "Computer IP";
            this.ClmComputerIP.FieldName = "ip_address";
            this.ClmComputerIP.Name = "ClmComputerIP";
            this.ClmComputerIP.Visible = true;
            this.ClmComputerIP.VisibleIndex = 8;
            this.ClmComputerIP.Width = 103;
            // 
            // ChkAll
            // 
            this.ChkAll.Location = new System.Drawing.Point(28, 3);
            this.ChkAll.Name = "ChkAll";
            this.ChkAll.Properties.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.ChkAll.Properties.Appearance.Options.UseFont = true;
            this.ChkAll.Properties.Caption = "";
            this.ChkAll.Size = new System.Drawing.Size(16, 19);
            this.ChkAll.TabIndex = 14;
            this.ChkAll.CheckedChanged += new System.EventHandler(this.ChkAll_CheckedChanged);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.BtnExit1);
            this.panelControl1.Controls.Add(this.btnSearch);
            this.panelControl1.Controls.Add(this.lueKapan);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Controls.Add(this.lueCutNo);
            this.panelControl1.Controls.Add(this.lblKapanNo);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.label3);
            this.panelControl1.Controls.Add(this.dtpToDate);
            this.panelControl1.Controls.Add(this.dtpFromDate);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(10, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1064, 51);
            this.panelControl1.TabIndex = 15;
            // 
            // BtnExit1
            // 
            this.BtnExit1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnExit1.Appearance.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit1.Appearance.Options.UseFont = true;
            this.BtnExit1.ImageOptions.Image = global::DERP.Properties.Resources.Exit;
            this.BtnExit1.Location = new System.Drawing.Point(952, 12);
            this.BtnExit1.Name = "BtnExit1";
            this.BtnExit1.Size = new System.Drawing.Size(102, 32);
            this.BtnExit1.TabIndex = 574;
            this.BtnExit1.Text = "E&xit";
            this.BtnExit1.Click += new System.EventHandler(this.BtnExit1_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Appearance.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Appearance.Options.UseFont = true;
            this.btnSearch.ImageOptions.Image = global::DERP.Properties.Resources.Search;
            this.btnSearch.Location = new System.Drawing.Point(831, 12);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(115, 32);
            this.btnSearch.TabIndex = 573;
            this.btnSearch.Text = "Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lueKapan
            // 
            this.lueKapan.EnterMoveNextControl = true;
            this.lueKapan.Location = new System.Drawing.Point(470, 17);
            this.lueKapan.Name = "lueKapan";
            this.lueKapan.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueKapan.Properties.Appearance.Options.UseFont = true;
            this.lueKapan.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.lueKapan.Properties.AppearanceDropDown.Options.UseFont = true;
            this.lueKapan.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.lueKapan.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.lueKapan.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.lueKapan.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueKapan.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("kapan_no", "Kapan No"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("kapan_id", "Kapan Id", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lueKapan.Properties.NullText = "";
            this.lueKapan.Properties.ShowHeader = false;
            this.lueKapan.Size = new System.Drawing.Size(125, 22);
            this.lueKapan.TabIndex = 565;
            this.lueKapan.EditValueChanged += new System.EventHandler(this.lueKapan_EditValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(400, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 14);
            this.label1.TabIndex = 568;
            this.label1.Text = "Kapan No";
            // 
            // lueCutNo
            // 
            this.lueCutNo.EnterMoveNextControl = true;
            this.lueCutNo.Location = new System.Drawing.Point(663, 17);
            this.lueCutNo.Name = "lueCutNo";
            this.lueCutNo.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueCutNo.Properties.Appearance.Options.UseFont = true;
            this.lueCutNo.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.lueCutNo.Properties.AppearanceDropDown.Options.UseFont = true;
            this.lueCutNo.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.lueCutNo.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.lueCutNo.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.lueCutNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueCutNo.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("rough_cut_no", "Rough Cut No"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("rough_cut_id", "Rough Cut ID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lueCutNo.Properties.NullText = "";
            this.lueCutNo.Properties.ShowHeader = false;
            this.lueCutNo.Size = new System.Drawing.Size(157, 22);
            this.lueCutNo.TabIndex = 566;
            // 
            // lblKapanNo
            // 
            this.lblKapanNo.AutoSize = true;
            this.lblKapanNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblKapanNo.Location = new System.Drawing.Point(611, 19);
            this.lblKapanNo.Name = "lblKapanNo";
            this.lblKapanNo.Size = new System.Drawing.Size(49, 14);
            this.lblKapanNo.TabIndex = 567;
            this.lblKapanNo.Text = "Cut No";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(6, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 14);
            this.label2.TabIndex = 564;
            this.label2.Text = "From Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(207, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 14);
            this.label3.TabIndex = 563;
            this.label3.Text = "To Date";
            // 
            // dtpToDate
            // 
            this.dtpToDate.EditValue = null;
            this.dtpToDate.EnterMoveNextControl = true;
            this.dtpToDate.Location = new System.Drawing.Point(268, 17);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtpToDate.Properties.Appearance.Options.UseFont = true;
            this.dtpToDate.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtpToDate.Properties.AppearanceDropDown.Options.UseFont = true;
            this.dtpToDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpToDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpToDate.Size = new System.Drawing.Size(126, 22);
            this.dtpToDate.TabIndex = 562;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.EditValue = null;
            this.dtpFromDate.EnterMoveNextControl = true;
            this.dtpFromDate.Location = new System.Drawing.Point(76, 17);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtpFromDate.Properties.Appearance.Options.UseFont = true;
            this.dtpFromDate.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtpFromDate.Properties.AppearanceDropDown.Options.UseFont = true;
            this.dtpFromDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpFromDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpFromDate.Size = new System.Drawing.Size(130, 22);
            this.dtpFromDate.TabIndex = 561;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.ChkAll);
            this.panelControl2.Controls.Add(this.MainGrid);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(10, 51);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1064, 460);
            this.panelControl2.TabIndex = 16;
            // 
            // ClmEntryDate
            // 
            this.ClmEntryDate.Caption = "Entry Time";
            this.ClmEntryDate.FieldName = "entry_time";
            this.ClmEntryDate.Name = "ClmEntryDate";
            this.ClmEntryDate.Visible = true;
            this.ClmEntryDate.VisibleIndex = 10;
            this.ClmEntryDate.Width = 92;
            // 
            // ClmEntryTime
            // 
            this.ClmEntryTime.Caption = "Entry Date";
            this.ClmEntryTime.FieldName = "entry_date";
            this.ClmEntryTime.Name = "ClmEntryTime";
            this.ClmEntryTime.Visible = true;
            this.ClmEntryTime.VisibleIndex = 9;
            this.ClmEntryTime.Width = 86;
            // 
            // FrmMFGPataLotEntryStock
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 562);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel5);
            this.KeyPreview = true;
            this.Name = "FrmMFGPataLotEntryStock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PATA LOT ENTRY STOCK";
            this.Load += new System.EventHandler(this.FrmMFGPataLotEntryStock_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMFGPataLotEntryStock_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSelCarat.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSelLot.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).EndInit();
            this.ContextMNExport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GrdDet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChkAll.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueKapan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueCutNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpToDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpToDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraGrid.GridControl MainGrid;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdDet;
        private DevExpress.XtraEditors.CheckEdit ChkAll;
        private DevExpress.XtraEditors.SimpleButton BtnOk;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn ClmKapanNo;
        private DevExpress.XtraGrid.Columns.GridColumn ClmCarat;
        private DevExpress.XtraGrid.Columns.GridColumn ClmRoughCutNo;
        private DevExpress.XtraGrid.Columns.GridColumn ClmRoughCutID;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private DevExpress.XtraGrid.Columns.GridColumn ClmKapanID;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repSelect;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtSelLot;
        private DevExpress.XtraEditors.TextEdit txtSelCarat;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private System.Windows.Forms.ContextMenuStrip ContextMNExport;
        private System.Windows.Forms.ToolStripMenuItem MNExportExcel;
        private System.Windows.Forms.ToolStripMenuItem MNExportPDF;
        private System.Windows.Forms.ToolStripMenuItem MNExportTEXT;
        private System.Windows.Forms.ToolStripMenuItem MNExportHTML;
        private System.Windows.Forms.ToolStripMenuItem MNExportRTF;
        private System.Windows.Forms.ToolStripMenuItem MNExportCSV;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.DateEdit dtpToDate;
        private DevExpress.XtraEditors.DateEdit dtpFromDate;
        private DevExpress.XtraEditors.LookUpEdit lueKapan;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.LookUpEdit lueCutNo;
        private System.Windows.Forms.Label lblKapanNo;
        private DevExpress.XtraEditors.SimpleButton BtnExit1;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraGrid.Columns.GridColumn ClmLotSrNo;
        private DevExpress.XtraGrid.Columns.GridColumn ClmReceiveDate;
        private DevExpress.XtraGrid.Columns.GridColumn ClmEntryBy;
        private DevExpress.XtraGrid.Columns.GridColumn ClmComputerIP;
        private System.ComponentModel.BackgroundWorker backgroundWorker_AssortFinalStock;
        private DevExpress.XtraGrid.Columns.GridColumn ClmTotalPcs;
        private DevExpress.XtraEditors.SimpleButton BtnPrint;
        private DevExpress.XtraGrid.Columns.GridColumn ClmEntryTime;
        private DevExpress.XtraGrid.Columns.GridColumn ClmEntryDate;
    }
}