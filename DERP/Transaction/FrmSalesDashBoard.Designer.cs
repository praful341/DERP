namespace DERP.Transaction
{
    partial class FrmSalesDashBoard
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
            DevExpress.XtraCharts.XYDiagram xyDiagram4 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series5 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.Series series6 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.XYDiagram xyDiagram5 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series7 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.XYDiagram xyDiagram6 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series8 = new DevExpress.XtraCharts.Series();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnGenerateReport = new DevExpress.XtraEditors.SimpleButton();
            this.dtpToDate = new DevExpress.XtraEditors.DateEdit();
            this.dtpFromDate = new DevExpress.XtraEditors.DateEdit();
            this.label11 = new System.Windows.Forms.Label();
            this.XRChartBTSales = new DevExpress.XtraCharts.ChartControl();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.XRChartParty = new DevExpress.XtraCharts.ChartControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.XRChartColorSieveWise = new DevExpress.XtraCharts.ChartControl();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.XRChartPieInOut = new DevExpress.XtraCharts.ChartControl();
            this.grdDueDays = new DevExpress.XtraGrid.GridControl();
            this.dgvDueDays = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.clmPartyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmFinalDays = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmFinalDueDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmDiffDays = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmDiffAmt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ClmInvNo = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpToDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpToDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.XRChartBTSales)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.XRChartParty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series7)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.XRChartColorSieveWise)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.XRChartPieInOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDueDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDueDays)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnExit);
            this.panelControl1.Controls.Add(this.BtnGenerateReport);
            this.panelControl1.Controls.Add(this.dtpToDate);
            this.panelControl1.Controls.Add(this.dtpFromDate);
            this.panelControl1.Controls.Add(this.label11);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1337, 51);
            this.panelControl1.TabIndex = 99;
            // 
            // btnExit
            // 
            this.btnExit.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Appearance.Options.UseFont = true;
            this.btnExit.ImageOptions.Image = global::DERP.Properties.Resources.Close;
            this.btnExit.Location = new System.Drawing.Point(404, 8);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(40, 36);
            this.btnExit.TabIndex = 183;
            this.btnExit.TabStop = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // BtnGenerateReport
            // 
            this.BtnGenerateReport.Appearance.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnGenerateReport.Appearance.Options.UseFont = true;
            this.BtnGenerateReport.ImageOptions.Image = global::DERP.Properties.Resources.Report_final;
            this.BtnGenerateReport.Location = new System.Drawing.Point(358, 8);
            this.BtnGenerateReport.Name = "BtnGenerateReport";
            this.BtnGenerateReport.Size = new System.Drawing.Size(40, 36);
            this.BtnGenerateReport.TabIndex = 181;
            this.BtnGenerateReport.TabStop = false;
            this.BtnGenerateReport.Click += new System.EventHandler(this.BtnGenerateReport_Click);
            // 
            // dtpToDate
            // 
            this.dtpToDate.EditValue = null;
            this.dtpToDate.EnterMoveNextControl = true;
            this.dtpToDate.Location = new System.Drawing.Point(224, 14);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtpToDate.Properties.Appearance.Options.UseFont = true;
            this.dtpToDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpToDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpToDate.Size = new System.Drawing.Size(128, 22);
            this.dtpToDate.TabIndex = 582;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.EditValue = null;
            this.dtpFromDate.EnterMoveNextControl = true;
            this.dtpFromDate.Location = new System.Drawing.Point(90, 14);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtpFromDate.Properties.Appearance.Options.UseFont = true;
            this.dtpFromDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpFromDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpFromDate.Size = new System.Drawing.Size(128, 22);
            this.dtpFromDate.TabIndex = 0;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(10, 17);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(74, 16);
            this.label11.TabIndex = 101;
            this.label11.Text = "From Date";
            // 
            // XRChartBTSales
            // 
            xyDiagram4.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram4.AxisY.VisibleInPanesSerializable = "-1";
            this.XRChartBTSales.Diagram = xyDiagram4;
            this.XRChartBTSales.Legend.Name = "Default Legend";
            this.XRChartBTSales.Location = new System.Drawing.Point(426, 23);
            this.XRChartBTSales.Name = "XRChartBTSales";
            series5.Name = "Series 1";
            series6.Name = "Series 2";
            this.XRChartBTSales.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series5,
        series6};
            this.XRChartBTSales.Size = new System.Drawing.Size(889, 262);
            this.XRChartBTSales.TabIndex = 100;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Maroon;
            this.label2.Location = new System.Drawing.Point(704, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(352, 16);
            this.label2.TabIndex = 102;
            this.label2.Text = "Branch Transfer V/s Sale Invoice Net Amount (In Rs)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Maroon;
            this.label1.Location = new System.Drawing.Point(486, 287);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(260, 16);
            this.label1.TabIndex = 103;
            this.label1.Text = "TOP 20 Party wise Sale Amount (In Rs)";
            // 
            // XRChartParty
            // 
            xyDiagram5.AxisX.Label.Angle = -50;
            xyDiagram5.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram5.AxisY.VisibleInPanesSerializable = "-1";
            this.XRChartParty.Diagram = xyDiagram5;
            this.XRChartParty.Legend.Name = "Default Legend";
            this.XRChartParty.Location = new System.Drawing.Point(0, 306);
            this.XRChartParty.Name = "XRChartParty";
            series7.Name = "Series 1";
            this.XRChartParty.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series7};
            this.XRChartParty.Size = new System.Drawing.Size(1315, 377);
            this.XRChartParty.TabIndex = 104;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.grdDueDays);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.XRChartColorSieveWise);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.XRChartPieInOut);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.XRChartParty);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.XRChartBTSales);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 51);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1337, 699);
            this.panel1.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.Maroon;
            this.label6.Location = new System.Drawing.Point(888, 686);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(325, 16);
            this.label6.TabIndex = 589;
            this.label6.Text = "TOP Sale Amount With Color + Sieve wise (In Rs)";
            this.label6.Visible = false;
            // 
            // XRChartColorSieveWise
            // 
            xyDiagram6.AxisX.Label.Angle = -50;
            xyDiagram6.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram6.AxisY.VisibleInPanesSerializable = "-1";
            this.XRChartColorSieveWise.Diagram = xyDiagram6;
            this.XRChartColorSieveWise.Legend.Name = "Default Legend";
            this.XRChartColorSieveWise.Location = new System.Drawing.Point(703, 705);
            this.XRChartColorSieveWise.Name = "XRChartColorSieveWise";
            series8.Name = "Series 1";
            this.XRChartColorSieveWise.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series8};
            this.XRChartColorSieveWise.Size = new System.Drawing.Size(697, 344);
            this.XRChartColorSieveWise.TabIndex = 588;
            this.XRChartColorSieveWise.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.Maroon;
            this.label4.Location = new System.Drawing.Point(212, 686);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(136, 16);
            this.label4.TabIndex = 585;
            this.label4.Text = "Final Due Days Wise";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.Maroon;
            this.label3.Location = new System.Drawing.Point(116, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(172, 16);
            this.label3.TabIndex = 583;
            this.label3.Text = "Branch IN / Sales / Stock";
            // 
            // XRChartPieInOut
            // 
            this.XRChartPieInOut.Legend.Name = "Default Legend";
            this.XRChartPieInOut.Location = new System.Drawing.Point(3, 25);
            this.XRChartPieInOut.Name = "XRChartPieInOut";
            this.XRChartPieInOut.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.XRChartPieInOut.Size = new System.Drawing.Size(416, 261);
            this.XRChartPieInOut.TabIndex = 105;
            // 
            // grdDueDays
            // 
            this.grdDueDays.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdDueDays.Location = new System.Drawing.Point(3, 705);
            this.grdDueDays.MainView = this.dgvDueDays;
            this.grdDueDays.Name = "grdDueDays";
            this.grdDueDays.Size = new System.Drawing.Size(694, 341);
            this.grdDueDays.TabIndex = 590;
            this.grdDueDays.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvDueDays});
            // 
            // dgvDueDays
            // 
            this.dgvDueDays.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.dgvDueDays.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.dgvDueDays.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            this.dgvDueDays.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            this.dgvDueDays.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.dgvDueDays.Appearance.FooterPanel.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvDueDays.Appearance.FooterPanel.Options.UseFont = true;
            this.dgvDueDays.Appearance.HeaderPanel.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvDueDays.Appearance.HeaderPanel.Options.UseFont = true;
            this.dgvDueDays.Appearance.Row.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvDueDays.Appearance.Row.Options.UseFont = true;
            this.dgvDueDays.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ClmInvNo,
            this.clmPartyName,
            this.clmFinalDays,
            this.clmFinalDueDate,
            this.clmDiffDays,
            this.clmDiffAmt});
            this.dgvDueDays.GridControl = this.grdDueDays;
            this.dgvDueDays.Name = "dgvDueDays";
            this.dgvDueDays.OptionsBehavior.Editable = false;
            this.dgvDueDays.OptionsBehavior.ReadOnly = true;
            this.dgvDueDays.OptionsCustomization.AllowQuickHideColumns = false;
            this.dgvDueDays.OptionsView.ColumnAutoWidth = false;
            this.dgvDueDays.OptionsView.ShowAutoFilterRow = true;
            this.dgvDueDays.OptionsView.ShowFooter = true;
            this.dgvDueDays.OptionsView.ShowGroupPanel = false;
            // 
            // clmPartyName
            // 
            this.clmPartyName.Caption = "Party Name";
            this.clmPartyName.FieldName = "party_name";
            this.clmPartyName.Name = "clmPartyName";
            this.clmPartyName.OptionsColumn.AllowEdit = false;
            this.clmPartyName.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.clmPartyName.Visible = true;
            this.clmPartyName.VisibleIndex = 1;
            this.clmPartyName.Width = 169;
            // 
            // clmFinalDays
            // 
            this.clmFinalDays.Caption = "Final Days";
            this.clmFinalDays.FieldName = "final_days";
            this.clmFinalDays.Name = "clmFinalDays";
            this.clmFinalDays.OptionsColumn.AllowEdit = false;
            this.clmFinalDays.Visible = true;
            this.clmFinalDays.VisibleIndex = 2;
            this.clmFinalDays.Width = 100;
            // 
            // clmFinalDueDate
            // 
            this.clmFinalDueDate.Caption = "Final Due Date";
            this.clmFinalDueDate.FieldName = "final_due_date";
            this.clmFinalDueDate.Name = "clmFinalDueDate";
            this.clmFinalDueDate.OptionsColumn.AllowEdit = false;
            this.clmFinalDueDate.Visible = true;
            this.clmFinalDueDate.VisibleIndex = 3;
            this.clmFinalDueDate.Width = 99;
            // 
            // clmDiffDays
            // 
            this.clmDiffDays.Caption = "Diff. Days";
            this.clmDiffDays.FieldName = "diff_days";
            this.clmDiffDays.Name = "clmDiffDays";
            this.clmDiffDays.OptionsColumn.AllowEdit = false;
            this.clmDiffDays.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.clmDiffDays.Visible = true;
            this.clmDiffDays.VisibleIndex = 4;
            this.clmDiffDays.Width = 95;
            // 
            // clmDiffAmt
            // 
            this.clmDiffAmt.Caption = "Diff. Amt";
            this.clmDiffAmt.FieldName = "diff_amt";
            this.clmDiffAmt.Name = "clmDiffAmt";
            this.clmDiffAmt.OptionsColumn.AllowEdit = false;
            this.clmDiffAmt.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.clmDiffAmt.Visible = true;
            this.clmDiffAmt.VisibleIndex = 5;
            // 
            // ClmInvNo
            // 
            this.ClmInvNo.Caption = "Inv. No";
            this.ClmInvNo.FieldName = "invoice_no";
            this.ClmInvNo.Name = "ClmInvNo";
            this.ClmInvNo.Visible = true;
            this.ClmInvNo.VisibleIndex = 0;
            this.ClmInvNo.Width = 64;
            // 
            // FrmSalesDashBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1337, 750);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelControl1);
            this.Name = "FrmSalesDashBoard";
            this.Text = "Sales DashBoard";
            this.Load += new System.EventHandler(this.FrmSalesDashBoard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpToDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpToDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.XRChartBTSales)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.XRChartParty)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.XRChartColorSieveWise)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.XRChartPieInOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDueDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDueDays)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraEditors.SimpleButton BtnGenerateReport;
        private DevExpress.XtraEditors.DateEdit dtpToDate;
        private DevExpress.XtraEditors.DateEdit dtpFromDate;
        private System.Windows.Forms.Label label11;
        private DevExpress.XtraCharts.ChartControl XRChartBTSales;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraCharts.ChartControl XRChartParty;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraCharts.ChartControl XRChartPieInOut;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraCharts.ChartControl XRChartColorSieveWise;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraGrid.GridControl grdDueDays;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvDueDays;
        private DevExpress.XtraGrid.Columns.GridColumn clmPartyName;
        private DevExpress.XtraGrid.Columns.GridColumn clmFinalDays;
        private DevExpress.XtraGrid.Columns.GridColumn clmFinalDueDate;
        private DevExpress.XtraGrid.Columns.GridColumn clmDiffDays;
        private DevExpress.XtraGrid.Columns.GridColumn clmDiffAmt;
        private DevExpress.XtraGrid.Columns.GridColumn ClmInvNo;
    }
}