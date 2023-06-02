namespace DERP.Rejection
{
    partial class FrmMFGRoughStockEntry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMFGRoughStockEntry));
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.ContextMNExport = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MNExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportPDF = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportTEXT = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportHTML = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportRTF = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportCSV = new System.Windows.Forms.ToolStripMenuItem();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl5 = new DevExpress.XtraEditors.PanelControl();
            this.lueType = new DevExpress.XtraEditors.LookUpEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.txtKapanNo = new DevExpress.XtraEditors.TextEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.txtAmount = new DevExpress.XtraEditors.TextEdit();
            this.label18 = new System.Windows.Forms.Label();
            this.txtWt = new DevExpress.XtraEditors.TextEdit();
            this.label19 = new System.Windows.Forms.Label();
            this.txtRate = new DevExpress.XtraEditors.TextEdit();
            this.label20 = new System.Windows.Forms.Label();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.dtpDate = new DevExpress.XtraEditors.DateEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.lblMode = new DevExpress.XtraEditors.LabelControl();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.grdRoughStockEntry = new DevExpress.XtraGrid.GridControl();
            this.dgvRoughStockEntry = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.clmDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmKapanId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmKapanNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmCarat = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl6 = new DevExpress.XtraEditors.PanelControl();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.dtpToDate = new DevExpress.XtraEditors.DateEdit();
            this.dtpFromDate = new DevExpress.XtraEditors.DateEdit();
            this.txtPcs = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.clmPcs = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.ContextMNExport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).BeginInit();
            this.panelControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKapanNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdRoughStockEntry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoughStockEntry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).BeginInit();
            this.panelControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpToDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpToDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPcs.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
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
            // panelControl1
            // 
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1119, 11);
            this.panelControl1.TabIndex = 18;
            // 
            // panelControl5
            // 
            this.panelControl5.Controls.Add(this.label3);
            this.panelControl5.Controls.Add(this.txtPcs);
            this.panelControl5.Controls.Add(this.lueType);
            this.panelControl5.Controls.Add(this.label2);
            this.panelControl5.Controls.Add(this.txtKapanNo);
            this.panelControl5.Controls.Add(this.label4);
            this.panelControl5.Controls.Add(this.btnDelete);
            this.panelControl5.Controls.Add(this.txtAmount);
            this.panelControl5.Controls.Add(this.label18);
            this.panelControl5.Controls.Add(this.txtWt);
            this.panelControl5.Controls.Add(this.label19);
            this.panelControl5.Controls.Add(this.txtRate);
            this.panelControl5.Controls.Add(this.label20);
            this.panelControl5.Controls.Add(this.labelControl3);
            this.panelControl5.Controls.Add(this.dtpDate);
            this.panelControl5.Controls.Add(this.label1);
            this.panelControl5.Controls.Add(this.btnExit);
            this.panelControl5.Controls.Add(this.lblMode);
            this.panelControl5.Controls.Add(this.btnClear);
            this.panelControl5.Controls.Add(this.btnSave);
            this.panelControl5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl5.Location = new System.Drawing.Point(0, 11);
            this.panelControl5.Name = "panelControl5";
            this.panelControl5.Size = new System.Drawing.Size(1119, 206);
            this.panelControl5.TabIndex = 1;
            // 
            // lueType
            // 
            this.lueType.EnterMoveNextControl = true;
            this.lueType.Location = new System.Drawing.Point(92, 173);
            this.lueType.Name = "lueType";
            this.lueType.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueType.Properties.Appearance.Options.UseFont = true;
            this.lueType.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.lueType.Properties.AppearanceDropDown.Options.UseFont = true;
            this.lueType.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold);
            this.lueType.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.lueType.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.lueType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueType.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("type", "Type")});
            this.lueType.Properties.NullText = "";
            this.lueType.Properties.ShowHeader = false;
            this.lueType.Size = new System.Drawing.Size(113, 22);
            this.lueType.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(9, 176);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 638;
            this.label2.Text = "Type";
            // 
            // txtKapanNo
            // 
            this.txtKapanNo.EnterMoveNextControl = true;
            this.txtKapanNo.Location = new System.Drawing.Point(92, 32);
            this.txtKapanNo.Name = "txtKapanNo";
            this.txtKapanNo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKapanNo.Properties.Appearance.Options.UseFont = true;
            this.txtKapanNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtKapanNo.Size = new System.Drawing.Size(157, 22);
            this.txtKapanNo.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(9, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 16);
            this.label4.TabIndex = 635;
            this.label4.Text = "Kapan No";
            // 
            // btnDelete
            // 
            this.btnDelete.Appearance.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Appearance.Options.UseFont = true;
            this.btnDelete.ImageOptions.Image = global::DERP.Properties.Resources.Close;
            this.btnDelete.Location = new System.Drawing.Point(537, 168);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(102, 32);
            this.btnDelete.TabIndex = 10;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // txtAmount
            // 
            this.txtAmount.Enabled = false;
            this.txtAmount.EnterMoveNextControl = true;
            this.txtAmount.Location = new System.Drawing.Point(92, 145);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
            this.txtAmount.Properties.Appearance.Options.UseFont = true;
            this.txtAmount.Properties.Appearance.Options.UseForeColor = true;
            this.txtAmount.Properties.Mask.EditMask = "f2";
            this.txtAmount.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtAmount.Properties.ReadOnly = true;
            this.txtAmount.Size = new System.Drawing.Size(113, 22);
            this.txtAmount.TabIndex = 5;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label18.Location = new System.Drawing.Point(9, 148);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(59, 16);
            this.label18.TabIndex = 626;
            this.label18.Text = "Amount";
            // 
            // txtWt
            // 
            this.txtWt.EnterMoveNextControl = true;
            this.txtWt.Location = new System.Drawing.Point(92, 89);
            this.txtWt.Name = "txtWt";
            this.txtWt.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWt.Properties.Appearance.Options.UseFont = true;
            this.txtWt.Size = new System.Drawing.Size(113, 22);
            this.txtWt.TabIndex = 3;
            this.txtWt.EditValueChanged += new System.EventHandler(this.txtWt_EditValueChanged);
            this.txtWt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtWt_KeyPress);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label19.Location = new System.Drawing.Point(9, 92);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(27, 16);
            this.label19.TabIndex = 623;
            this.label19.Text = "Wt";
            // 
            // txtRate
            // 
            this.txtRate.EnterMoveNextControl = true;
            this.txtRate.Location = new System.Drawing.Point(92, 117);
            this.txtRate.Name = "txtRate";
            this.txtRate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRate.Properties.Appearance.Options.UseFont = true;
            this.txtRate.Size = new System.Drawing.Size(113, 22);
            this.txtRate.TabIndex = 4;
            this.txtRate.EditValueChanged += new System.EventHandler(this.txtRate_EditValueChanged);
            this.txtRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRate_KeyPress);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label20.Location = new System.Drawing.Point(9, 120);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(39, 16);
            this.label20.TabIndex = 624;
            this.label20.Text = "Rate";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl3.Appearance.Options.UseBackColor = true;
            this.labelControl3.Appearance.Options.UseForeColor = true;
            this.labelControl3.Location = new System.Drawing.Point(78, 32);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(9, 13);
            this.labelControl3.TabIndex = 588;
            this.labelControl3.Text = "* ";
            // 
            // dtpDate
            // 
            this.dtpDate.EditValue = null;
            this.dtpDate.EnterMoveNextControl = true;
            this.dtpDate.Location = new System.Drawing.Point(92, 4);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtpDate.Properties.Appearance.Options.UseFont = true;
            this.dtpDate.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtpDate.Properties.AppearanceDropDown.Options.UseFont = true;
            this.dtpDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDate.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dtpDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpDate.Size = new System.Drawing.Size(157, 22);
            this.dtpDate.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 16);
            this.label1.TabIndex = 586;
            this.label1.Text = "Date";
            // 
            // btnExit
            // 
            this.btnExit.Appearance.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Appearance.Options.UseFont = true;
            this.btnExit.ImageOptions.Image = global::DERP.Properties.Resources.Exit;
            this.btnExit.Location = new System.Drawing.Point(429, 168);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(102, 32);
            this.btnExit.TabIndex = 9;
            this.btnExit.Text = "E&xit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblMode
            // 
            this.lblMode.Appearance.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMode.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblMode.Appearance.Options.UseFont = true;
            this.lblMode.Appearance.Options.UseForeColor = true;
            this.lblMode.Location = new System.Drawing.Point(659, 176);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(71, 16);
            this.lblMode.TabIndex = 31;
            this.lblMode.Text = "Add Mode";
            // 
            // btnClear
            // 
            this.btnClear.Appearance.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Appearance.Options.UseFont = true;
            this.btnClear.ImageOptions.Image = global::DERP.Properties.Resources.Clear;
            this.btnClear.Location = new System.Drawing.Point(321, 168);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(102, 32);
            this.btnClear.TabIndex = 8;
            this.btnClear.Text = "&Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.Appearance.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.ImageOptions.Image = global::DERP.Properties.Resources.Save;
            this.btnSave.Location = new System.Drawing.Point(213, 168);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(102, 32);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "&Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.grdRoughStockEntry);
            this.panelControl2.Controls.Add(this.panelControl6);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 217);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1119, 489);
            this.panelControl2.TabIndex = 19;
            // 
            // grdRoughStockEntry
            // 
            this.grdRoughStockEntry.ContextMenuStrip = this.ContextMNExport;
            this.grdRoughStockEntry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdRoughStockEntry.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdRoughStockEntry.Location = new System.Drawing.Point(2, 43);
            this.grdRoughStockEntry.MainView = this.dgvRoughStockEntry;
            this.grdRoughStockEntry.Name = "grdRoughStockEntry";
            this.grdRoughStockEntry.Size = new System.Drawing.Size(1115, 444);
            this.grdRoughStockEntry.TabIndex = 17;
            this.grdRoughStockEntry.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvRoughStockEntry});
            // 
            // dgvRoughStockEntry
            // 
            this.dgvRoughStockEntry.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.dgvRoughStockEntry.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.dgvRoughStockEntry.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            this.dgvRoughStockEntry.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            this.dgvRoughStockEntry.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.dgvRoughStockEntry.Appearance.FooterPanel.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.dgvRoughStockEntry.Appearance.FooterPanel.Options.UseFont = true;
            this.dgvRoughStockEntry.Appearance.HeaderPanel.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.dgvRoughStockEntry.Appearance.HeaderPanel.Options.UseFont = true;
            this.dgvRoughStockEntry.Appearance.Row.Font = new System.Drawing.Font("Cambria", 9.75F);
            this.dgvRoughStockEntry.Appearance.Row.Options.UseFont = true;
            this.dgvRoughStockEntry.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.clmDate,
            this.clmKapanId,
            this.clmKapanNo,
            this.clmCarat,
            this.clmRate,
            this.clmAmount,
            this.clmType,
            this.clmPcs});
            this.dgvRoughStockEntry.GridControl = this.grdRoughStockEntry;
            this.dgvRoughStockEntry.Name = "dgvRoughStockEntry";
            this.dgvRoughStockEntry.OptionsBehavior.Editable = false;
            this.dgvRoughStockEntry.OptionsBehavior.ReadOnly = true;
            this.dgvRoughStockEntry.OptionsCustomization.AllowQuickHideColumns = false;
            this.dgvRoughStockEntry.OptionsView.ColumnAutoWidth = false;
            this.dgvRoughStockEntry.OptionsView.ShowAutoFilterRow = true;
            this.dgvRoughStockEntry.OptionsView.ShowFooter = true;
            this.dgvRoughStockEntry.OptionsView.ShowGroupPanel = false;
            this.dgvRoughStockEntry.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.dgvRoughStockEntry_RowClick);
            this.dgvRoughStockEntry.CustomSummaryCalculate += new DevExpress.Data.CustomSummaryEventHandler(this.dgvRoughStockEntry_CustomSummaryCalculate);
            // 
            // clmDate
            // 
            this.clmDate.Caption = "Date";
            this.clmDate.FieldName = "kapan_date";
            this.clmDate.Name = "clmDate";
            this.clmDate.Visible = true;
            this.clmDate.VisibleIndex = 0;
            this.clmDate.Width = 108;
            // 
            // clmKapanId
            // 
            this.clmKapanId.Caption = "Kapan ID";
            this.clmKapanId.FieldName = "kapan_id";
            this.clmKapanId.Name = "clmKapanId";
            // 
            // clmKapanNo
            // 
            this.clmKapanNo.Caption = "Kapan No";
            this.clmKapanNo.FieldName = "kapan_no";
            this.clmKapanNo.Name = "clmKapanNo";
            this.clmKapanNo.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.clmKapanNo.Visible = true;
            this.clmKapanNo.VisibleIndex = 1;
            this.clmKapanNo.Width = 118;
            // 
            // clmCarat
            // 
            this.clmCarat.Caption = "Carat";
            this.clmCarat.FieldName = "carat";
            this.clmCarat.Name = "clmCarat";
            this.clmCarat.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.clmCarat.Visible = true;
            this.clmCarat.VisibleIndex = 4;
            // 
            // clmRate
            // 
            this.clmRate.Caption = "Rate";
            this.clmRate.FieldName = "rate";
            this.clmRate.Name = "clmRate";
            this.clmRate.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom)});
            this.clmRate.Visible = true;
            this.clmRate.VisibleIndex = 5;
            // 
            // clmAmount
            // 
            this.clmAmount.Caption = "Amount";
            this.clmAmount.FieldName = "amount";
            this.clmAmount.Name = "clmAmount";
            this.clmAmount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.clmAmount.Visible = true;
            this.clmAmount.VisibleIndex = 6;
            // 
            // clmType
            // 
            this.clmType.Caption = "Type";
            this.clmType.FieldName = "type";
            this.clmType.Name = "clmType";
            this.clmType.Visible = true;
            this.clmType.VisibleIndex = 2;
            // 
            // panelControl6
            // 
            this.panelControl6.Controls.Add(this.label23);
            this.panelControl6.Controls.Add(this.label22);
            this.panelControl6.Controls.Add(this.btnSearch);
            this.panelControl6.Controls.Add(this.dtpToDate);
            this.panelControl6.Controls.Add(this.dtpFromDate);
            this.panelControl6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl6.Location = new System.Drawing.Point(2, 2);
            this.panelControl6.Name = "panelControl6";
            this.panelControl6.Size = new System.Drawing.Size(1115, 41);
            this.panelControl6.TabIndex = 0;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label23.Location = new System.Drawing.Point(218, 11);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(58, 16);
            this.label23.TabIndex = 588;
            this.label23.Text = "To Date";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label22.Location = new System.Drawing.Point(5, 11);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(74, 16);
            this.label22.TabIndex = 587;
            this.label22.Text = "From Date";
            // 
            // btnSearch
            // 
            this.btnSearch.Appearance.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Appearance.Options.UseFont = true;
            this.btnSearch.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.ImageOptions.Image")));
            this.btnSearch.Location = new System.Drawing.Point(419, 5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(102, 32);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "S&earch";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dtpToDate
            // 
            this.dtpToDate.EditValue = null;
            this.dtpToDate.EnterMoveNextControl = true;
            this.dtpToDate.Location = new System.Drawing.Point(279, 9);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtpToDate.Properties.Appearance.Options.UseFont = true;
            this.dtpToDate.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtpToDate.Properties.AppearanceDropDown.Options.UseFont = true;
            this.dtpToDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpToDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpToDate.Size = new System.Drawing.Size(123, 22);
            this.dtpToDate.TabIndex = 1;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.EditValue = null;
            this.dtpFromDate.EnterMoveNextControl = true;
            this.dtpFromDate.Location = new System.Drawing.Point(86, 9);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtpFromDate.Properties.Appearance.Options.UseFont = true;
            this.dtpFromDate.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtpFromDate.Properties.AppearanceDropDown.Options.UseFont = true;
            this.dtpFromDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpFromDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpFromDate.Size = new System.Drawing.Size(126, 22);
            this.dtpFromDate.TabIndex = 0;
            // 
            // txtPcs
            // 
            this.txtPcs.EnterMoveNextControl = true;
            this.txtPcs.Location = new System.Drawing.Point(92, 60);
            this.txtPcs.Name = "txtPcs";
            this.txtPcs.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPcs.Properties.Appearance.Options.UseFont = true;
            this.txtPcs.Size = new System.Drawing.Size(113, 22);
            this.txtPcs.TabIndex = 2;
            this.txtPcs.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPcs_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(9, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 16);
            this.label3.TabIndex = 640;
            this.label3.Text = "Pcs";
            // 
            // clmPcs
            // 
            this.clmPcs.Caption = "Pcs";
            this.clmPcs.FieldName = "pcs";
            this.clmPcs.Name = "clmPcs";
            this.clmPcs.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.clmPcs.Visible = true;
            this.clmPcs.VisibleIndex = 3;
            // 
            // FrmMFGRoughStockEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1119, 706);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl5);
            this.Controls.Add(this.panelControl1);
            this.Name = "FrmMFGRoughStockEntry";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rough Stock Entry";
            this.Load += new System.EventHandler(this.FrmMFGRoughStockEntry_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.ContextMNExport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).EndInit();
            this.panelControl5.ResumeLayout(false);
            this.panelControl5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKapanNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdRoughStockEntry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoughStockEntry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).EndInit();
            this.panelControl6.ResumeLayout(false);
            this.panelControl6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpToDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpToDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPcs.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl5;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.LabelControl lblMode;
        private System.Windows.Forms.ContextMenuStrip ContextMNExport;
        private System.Windows.Forms.ToolStripMenuItem MNExportExcel;
        private System.Windows.Forms.ToolStripMenuItem MNExportPDF;
        private System.Windows.Forms.ToolStripMenuItem MNExportTEXT;
        private System.Windows.Forms.ToolStripMenuItem MNExportHTML;
        private System.Windows.Forms.ToolStripMenuItem MNExportRTF;
        private System.Windows.Forms.ToolStripMenuItem MNExportCSV;
        private DevExpress.XtraEditors.DateEdit dtpDate;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit txtAmount;
        private System.Windows.Forms.Label label18;
        private DevExpress.XtraEditors.TextEdit txtWt;
        private System.Windows.Forms.Label label19;
        private DevExpress.XtraEditors.TextEdit txtRate;
        private System.Windows.Forms.Label label20;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.GridControl grdRoughStockEntry;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvRoughStockEntry;
        private DevExpress.XtraGrid.Columns.GridColumn clmDate;
        private DevExpress.XtraGrid.Columns.GridColumn clmKapanId;
        private DevExpress.XtraGrid.Columns.GridColumn clmKapanNo;
        private DevExpress.XtraGrid.Columns.GridColumn clmCarat;
        private DevExpress.XtraGrid.Columns.GridColumn clmRate;
        private DevExpress.XtraGrid.Columns.GridColumn clmAmount;
        private DevExpress.XtraGrid.Columns.GridColumn clmType;
        private DevExpress.XtraEditors.PanelControl panelControl6;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.DateEdit dtpToDate;
        private DevExpress.XtraEditors.DateEdit dtpFromDate;
        private DevExpress.XtraEditors.TextEdit txtKapanNo;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.LookUpEdit lueType;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.TextEdit txtPcs;
        private DevExpress.XtraGrid.Columns.GridColumn clmPcs;
    }
}