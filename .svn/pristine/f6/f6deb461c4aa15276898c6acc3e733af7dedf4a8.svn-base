using BLL;
using BLL.FunctionClasses.Report;
using BLL.PropertyClasses.Report;
using DERP.Class;
using DevExpress.XtraPrinting;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Report
{
    public partial class FrmGalaxyReport : DevExpress.XtraEditors.XtraForm
    {
        #region "Data Member"
        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents = new BLL.FormEvents();
        BLL.Validation Val = new BLL.Validation();
        string mStrReportGroup = string.Empty;
        int mIntReportCode = 0;
        int mIntPivot = 0;
        ReportParams ObjReportParams = new ReportParams();
        New_Report_MasterProperty ObjReportMasterProperty = new New_Report_MasterProperty();
        New_Report_DetailProperty ObjReportDetailProperty = new New_Report_DetailProperty();
        NewReportMaster ObjReportMaster = new NewReportMaster();
        DataTable mDTDetail = new DataTable(BLL.TPV.Table.New_Report_Detail);
        DataTable dtAllvalues = new DataTable();
        DataColumn dc_Unique = new DataColumn("Unique", typeof(string));
        DataColumn dc_Parent = new DataColumn("Parent", typeof(string));
        DataColumn dc_Data = new DataColumn("Data", typeof(string));
        FillCombo ObjFillCombo = new FillCombo();
        string StrColumnName = string.Empty;
        DataTable DTab = new DataTable();
        DataTable m_opDate = new DataTable();
        ReportParams_Property ReportParams_Property = new BLL.PropertyClasses.Report.ReportParams_Property();
        string StrType = string.Empty;
        string StrTransactionType = string.Empty;
        string StrKapan = string.Empty;
        string StrShape = string.Empty;
        string StrColor = string.Empty;
        string StrClarity = string.Empty;
        string StrCutNo = string.Empty;
        string StrSieve = string.Empty;
        string StrReportType = string.Empty;
        string Type = string.Empty;
        #endregion

        #region Counstructor

        public FrmGalaxyReport()
        {
            InitializeComponent();
        }
        private void AttachFormEvents()
        {
            objBOFormEvents.CurForm = this;
            objBOFormEvents.FormKeyDown = true;
            objBOFormEvents.FormKeyPress = true;
            objBOFormEvents.FormResize = true;
            objBOFormEvents.FormClosing = true;
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }

        #endregion

        #region "Events" 
        private void FrmReportList_Load(object sender, EventArgs e)
        {
            if (dtAllvalues.Columns.Count == 0)
            {
                dtAllvalues.Columns.Add(dc_Unique);
                dtAllvalues.Columns.Add(dc_Parent);
                dtAllvalues.Columns.Add(dc_Data);
            }

            AttachFormEvents();

            this.Show();

            mIntReportCode = Val.ToInt(this.Tag);
            DTPFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
            DTPFromDate.EditValue = DateTime.Now;

            DTPToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPToDate.Properties.CharacterCasing = CharacterCasing.Upper;
            DTPToDate.EditValue = DateTime.Now;

            FillControls();
            FillListControls();
            ListKapan.Focus();
        }
        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (StrReportType.ToString() == "Lot Wise Report" ||
                StrReportType.ToString() == "Machine Wise Summary Report" ||
                StrReportType.ToString() == "Purity Wise Summary Report" ||
                StrReportType.ToString() == "Kapan wise Summary Report" ||
                StrReportType.ToString() == "Diameter wise Summary Report" ||
                StrReportType.ToString() == "Diameter wise 4P Report" ||
                StrReportType.ToString() == "Hight wise Summary Report" ||
                StrReportType.ToString() == "Charni wise Summary Report" ||
                StrReportType.ToString() == "TOP Wise Summary Report" ||
                StrReportType.ToString() == "Cent Wise Summary Report" ||
                StrReportType.ToString() == "Diameter + Purity wise Summary Report" ||
                StrReportType.ToString() == "Kapan Wise Result" ||
                StrReportType.ToString() == "Kapan Wise Barcode Print"
                )
            {
                DTab = ObjReportParams.GetGalaxyKapanReport(ReportParams_Property, "GALAXY_Kapan_Detail_Report");

                if ((GlobalDec.gEmployeeProperty.user_name == "YOGESH" && GlobalDec.gEmployeeProperty.role_name == "GALAXY ADMIN") || GlobalDec.gEmployeeProperty.role_name == "SURAT ADMIN")
                {
                }
                else
                {
                    if (DTab.Columns.Contains("per_day_avg"))
                    {
                        DTab.Columns.Remove("per_day_avg");
                    }
                    if (DTab.Columns.Contains("total_days"))
                    {
                        DTab.Columns.Remove("total_days");
                    }
                }
            }
            else if (StrReportType.ToString() == "Lot Wise Summary Report" && Type == "Summary")
            {
                DTab = ObjReportParams.GetGalaxyKapanReport(ReportParams_Property, "GALAXY_Kapan_Report");
            }
            //else if (StrReportType.ToString() == "Kapan Wise Result" && Type == "Summary")
            //{
            //    DTab = ObjReportParams.GetGalaxyKapanReport(ReportParams_Property, "GALAXY_Kapan_Wise_Result");
            //}
            else if (StrReportType.ToString() == "Lot Wise Summary Report" && Type == "Pending")
            {
                DTab = ObjReportParams.GetGalaxyKapanReport(ReportParams_Property, "GALAXY_Kapan_Pending_Report");
            }
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (StrReportType.ToString() == "Lot Wise Report" || StrReportType.ToString() == "Machine Wise Summary Report" || StrReportType.ToString() == "Purity Wise Summary Report" || StrReportType.ToString() == "Kapan wise Summary Report" || StrReportType.ToString() == "Diameter wise Summary Report" || StrReportType.ToString() == "Diameter wise 4P Report" || StrReportType.ToString() == "Hight wise Summary Report")
            {
                PanelLoading.Visible = false;
                if (mIntPivot == 0)
                {
                    FrmGReportViewerBand FrmGReportViewer = new Report.FrmGReportViewerBand();

                    if (chkPivot.Checked == true)
                    {
                        FrmGReportViewer.IsPivot = true;
                    }
                    else
                    {
                        FrmGReportViewer.IsPivot = false;
                    }

                    // FrmGReportViewer.Group_By_Tag = ListGroupBy.GetTagValue;
                    //  FrmGReportViewer.Group_By_Text = ListGroupBy.GetTextValue;
                    FrmGReportViewer.ObjReportDetailProperty = ObjReportDetailProperty;
                    FrmGReportViewer.mDTDetail = mDTDetail;
                    FrmGReportViewer.Report_Type = Val.ToString(RbtReportType.EditValue);
                    FrmGReportViewer.Report_Code = ObjReportDetailProperty.Report_code;

                    FrmGReportViewer.Remark = ObjReportDetailProperty.Remark;
                    FrmGReportViewer.ReportParams_Property = ReportParams_Property;
                    FrmGReportViewer.Procedure_Name = ObjReportDetailProperty.Procedure_Name;
                    FrmGReportViewer.FilterByFormName = this.Name;


                    //string Location_Name = Val.ToString(ListLocation.Text);
                    FrmGReportViewer.ReportHeaderName = Val.ToString(ListReportType.Text); // ObjReportDetailProperty.Report_Header_Name;

                    FrmGReportViewer.FilterBy = GetFilterByValue();
                    FrmGReportViewer.DTab = DTab;
                    if (FrmGReportViewer.DTab == null || FrmGReportViewer.DTab.Rows.Count == 0)
                    {
                        this.Cursor = Cursors.Default;
                        FrmGReportViewer.Dispose();
                        FrmGReportViewer = null;
                        Global.Message("Data Not Found");
                        return;
                    }
                    this.Cursor = Cursors.Default;
                    FrmGReportViewer.ShowForm();
                }
            }
            else if (StrReportType.ToString() == "Lot Wise Summary Report")
            {
                PanelLoading.Visible = false;
                this.Cursor = Cursors.Default;
                if (Type == "Pending")
                {
                    dgvKapanReport.Columns["delete"].Visible = true;
                }
                else if (Type == "Summary")
                {
                    dgvKapanReport.Columns["delete"].Visible = false;
                }

                if (DTab.Rows.Count > 0)
                {
                    grdKapanReport.DataSource = DTab;
                }
                else
                {
                    Global.Message("Data Not Found");
                    grdKapanReport.DataSource = null;
                    return;
                }
            }
        }
        private string GetFilterByValue()
        {
            string Str = string.Empty;

            Str += "Filter By : ";

            if (ChkDateWise.Checked == true)
            {
                Str += "From Date : " + DTPFromDate.Text;
            }
            if (ChkDateWise.Checked == true)
            {
                Str += " & As On Date : " + DTPToDate.Text;
            }
            if (ListKapan.Text.Length > 0)
            {
                Str += ", Kapan : " + ListKapan.Text.ToString();
            }
            if (ListShape.Text.Length > 0)
            {
                Str += ", Shape : " + ListShape.Text.ToString();
            }
            if (ListClarity.Text.Length > 0)
            {
                Str += ", Clarity : " + ListClarity.Text.ToString();
            }
            if (ListCut.Text.Length > 0)
            {
                Str += ", Cut No : " + ListCut.Text.ToString();
            }
            if (ListSieve.Text.Length > 0)
            {
                Str += ", Sieve : " + ListSieve.Text.ToString();
            }
            if (ListLotNo.Text.Length > 0)
            {
                Str += ", Lot No : " + ListLotNo.Text.ToString();
            }
            return Str;
        }
        private void BtnReset_Click(object sender, EventArgs e)
        {
            chkPivot.Checked = false;
            for (int i = 0; i < ListKapan.Properties.Items.Count; i++)
                ListKapan.Properties.Items[i].CheckState = CheckState.Unchecked;

            for (int i = 0; i < ListShape.Properties.Items.Count; i++)
                ListShape.Properties.Items[i].CheckState = CheckState.Unchecked;

            for (int i = 0; i < ListColor.Properties.Items.Count; i++)
                ListColor.Properties.Items[i].CheckState = CheckState.Unchecked;

            for (int i = 0; i < ListClarity.Properties.Items.Count; i++)
                ListClarity.Properties.Items[i].CheckState = CheckState.Unchecked;
            for (int i = 0; i < ListCut.Properties.Items.Count; i++)
                ListCut.Properties.Items[i].CheckState = CheckState.Unchecked;
            for (int i = 0; i < ListSieve.Properties.Items.Count; i++)
                ListSieve.Properties.Items[i].CheckState = CheckState.Unchecked;
            for (int i = 0; i < ListLotNo.Properties.Items.Count; i++)
                ListLotNo.Properties.Items[i].CheckState = CheckState.Unchecked;

            if (GlobalDec.gEmployeeProperty.role_name == "SURAT GALAXY")
            {
                txtMachineNo.Enabled = false;
                txtMachineNo.Text = Val.ToString(GlobalDec.gEmployeeProperty.user_name);
            }
            else
            {
                txtMachineNo.Enabled = true;
            }
            ChkDateWise.Checked = false;

            grdKapanReport.DataSource = null;

            DTPFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
            DTPFromDate.EditValue = DateTime.Now;

            DTPToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPToDate.Properties.CharacterCasing = CharacterCasing.Upper;
            DTPToDate.EditValue = DateTime.Now;
            ListKapan.Focus();
        }

        #endregion

        #region "Functions"
        private void FillControls()
        {
            DataTable DTab = ObjReportMaster.GetDataForSearchDetailReport(mIntReportCode);

            foreach (DataRow DRow in DTab.Rows)
            {
                if (Val.ToInt(DRow["is_pivot"].ToString()) == 1)
                {
                    mIntPivot = 1;
                }

                DevExpress.XtraEditors.Controls.RadioGroupItem rd = new DevExpress.XtraEditors.Controls.RadioGroupItem();
                rd.Tag = Val.ToString(DRow["report_type"]);
                rd.Value = Val.ToString(DRow["report_type"]);
                rd.Description = Val.ToString(DRow["report_type"]);
                RbtReportType.Properties.Items.Add(rd);

            }
            RbtReportType.SelectedIndex = 0;
            ObjReportMasterProperty = ObjReportMaster.GetReportMasterProperty(mIntReportCode);
            lblTitle.Text = ObjReportMasterProperty.Report_Name;
        }
        private void FillListControls()
        {
            DataTable DTabShape = ObjFillCombo.FillCmb(FillCombo.TABLE.Galaxy_Shape);
            DTabShape.DefaultView.Sort = "shape_name";
            DTabShape = DTabShape.DefaultView.ToTable();

            ListShape.Properties.DataSource = DTabShape;
            ListShape.Properties.DisplayMember = "shape_name";
            ListShape.Properties.ValueMember = "shape_name";

            DataTable DTabColor = ObjFillCombo.FillCmb(FillCombo.TABLE.Galaxy_Color);
            DTabColor.DefaultView.Sort = "color_name";
            DTabColor = DTabColor.DefaultView.ToTable();

            ListColor.Properties.DataSource = DTabColor;
            ListColor.Properties.DisplayMember = "color_name";
            ListColor.Properties.ValueMember = "color_name";

            DataTable DTabClarity = ObjFillCombo.FillCmb(FillCombo.TABLE.Galaxy_Clarity);
            DTabClarity.DefaultView.Sort = "clarity_name";
            DTabClarity = DTabClarity.DefaultView.ToTable();

            ListClarity.Properties.DataSource = DTabClarity;
            ListClarity.Properties.DisplayMember = "clarity_name";
            ListClarity.Properties.ValueMember = "clarity_name";

            DataTable DTabCut = ObjFillCombo.FillCmb(FillCombo.TABLE.Galaxy_Cut);
            DTabCut.DefaultView.Sort = "rough_cut_no";
            DTabCut = DTabCut.DefaultView.ToTable();

            ListCut.Properties.DataSource = DTabCut;
            ListCut.Properties.DisplayMember = "rough_cut_no";
            ListCut.Properties.ValueMember = "rough_cut_no";

            DataTable DTabSieve = ObjFillCombo.FillCmb(FillCombo.TABLE.Galaxy_Sieve);
            DTabSieve.DefaultView.Sort = "sieve_name";
            DTabSieve = DTabSieve.DefaultView.ToTable();

            ListSieve.Properties.DataSource = DTabSieve;
            ListSieve.Properties.DisplayMember = "sieve_name";
            ListSieve.Properties.ValueMember = "sieve_name";

            DataTable DTabKapan = ObjFillCombo.FillCmb(FillCombo.TABLE.Galaxy_Kapan);
            DTabKapan.DefaultView.Sort = "kapan_no";
            DTabKapan = DTabKapan.DefaultView.ToTable();

            ListKapan.Properties.DataSource = DTabKapan;
            ListKapan.Properties.DisplayMember = "kapan_no";
            ListKapan.Properties.ValueMember = "kapan_no";

            if (GlobalDec.gEmployeeProperty.role_name == "SURAT GALAXY")
            {
                txtMachineNo.Enabled = false;
                txtMachineNo.Text = Val.ToString(GlobalDec.gEmployeeProperty.user_name);
            }
            else
            {
                txtMachineNo.Enabled = true;
            }
        }
        #endregion

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnSummary_Click(object sender, EventArgs e)
        {
            PanelLoading.Visible = true;
            ReportParams_Property = new ReportParams_Property();
            if (ChkDateWise.Checked == true)
            {
                var result = DateTime.Compare(Convert.ToDateTime(DTPFromDate.Text), Convert.ToDateTime(DTPToDate.Text.ToString()));
                if (result > 0)
                {
                    Global.Message(" Transaction From Date Not Be Greater Than Transaction To Date");
                    return;
                }

                ReportParams_Property.From_Date = Val.DBDate(DTPFromDate.Text);
                ReportParams_Property.To_Date = Val.DBDate(DTPToDate.Text);
            }
            else
            {
                ReportParams_Property.From_Date = null;
                ReportParams_Property.To_Date = null;
            }

            StrReportType = "Lot Wise Summary Report";
            ReportParams_Property.report_type = Val.ToString(ListReportType.Text);
            ReportParams_Property.kapan_no = StrKapan;
            ReportParams_Property.galaxy_shape_id = StrShape; //Val.Trim(ListShape.Properties.GetCheckedItems());
            ReportParams_Property.galaxy_color_id = StrColor; //Val.Trim(ListColor.Properties.GetCheckedItems());
            ReportParams_Property.galaxy_clarity_id = StrClarity; //Val.Trim(ListClarity.Properties.GetCheckedItems());
            ReportParams_Property.galaxy_cut_id = StrCutNo; //Val.Trim(ListCut.Properties.GetCheckedItems());
            ReportParams_Property.galaxy_sieve_name = Val.Trim(ListSieve.Properties.GetCheckedItems());
            ReportParams_Property.galaxy_lot_no = Val.Trim(ListLotNo.Properties.GetCheckedItems());
            ReportParams_Property.galaxy_machine_no = Val.ToString(txtMachineNo.Text);
            Type = Val.ToString("Summary");

            if (this.backgroundWorker1.IsBusy)
            {
            }
            else
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void ListKapan_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable DeTemp = new DataTable();
                var temp1 = "";

                temp1 = ListKapan.Properties.GetCheckedItems().ToString().Replace(" ", "").Replace("  ", "").Trim();

                StrKapan = "";
                StrType = "";
                string[] array = temp1.Split(',');
                if (!string.IsNullOrEmpty(temp1))
                {
                    foreach (var item in array)
                    {
                        StrType += "'" + item + "',";
                    }
                    StrKapan = StrType.Remove(StrType.Length - 1);
                }

                DataTable DTabLotNo = Global.GetReportKapanWise_LotNo(Val.ToString(StrKapan));
                DTabLotNo.DefaultView.Sort = "lot_no";
                DTabLotNo = DTabLotNo.DefaultView.ToTable();

                ListLotNo.Properties.DataSource = DTabLotNo;
                ListLotNo.Properties.DisplayMember = "lot_no";
                ListLotNo.Properties.ValueMember = "lot_no";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BtnReport_Click(object sender, EventArgs e)
        {
            PanelLoading.Visible = true;
            //if (StrKapan.Length > 0)
            //{
            //    if (StrKapan.ToString().Contains(","))
            //    {
            //        Global.Message("Please Select Atlest One Kapan No");
            //        PanelLoading.Visible = false;
            //        return;
            //    }
            //}
            //else
            //{
            //    Global.Message("Please Select Atlest One Kapan No");
            //    PanelLoading.Visible = false;
            //    return;
            //}
            ReportParams_Property = new ReportParams_Property();
            if (ChkDateWise.Checked == true)
            {
                var result = DateTime.Compare(Convert.ToDateTime(DTPFromDate.Text), Convert.ToDateTime(DTPToDate.Text.ToString()));
                if (result > 0)
                {
                    Global.Message(" Transaction From Date Not Be Greater Than Transaction To Date");
                    PanelLoading.Visible = true;
                    return;
                }

                ReportParams_Property.From_Date = Val.DBDate(DTPFromDate.Text);
                ReportParams_Property.To_Date = Val.DBDate(DTPToDate.Text);
            }
            else
            {
                ReportParams_Property.From_Date = null;
                ReportParams_Property.To_Date = null;
            }

            StrReportType = "Lot Wise Report";
            ReportParams_Property.report_type = Val.ToString(ListReportType.Text);
            ReportParams_Property.kapan_no = StrKapan;
            ReportParams_Property.galaxy_shape_id = StrShape; //Val.Trim(ListShape.Properties.GetCheckedItems());
            ReportParams_Property.galaxy_color_id = StrColor; //Val.Trim(ListColor.Properties.GetCheckedItems());
            ReportParams_Property.galaxy_clarity_id = StrClarity; //Val.Trim(ListClarity.Properties.GetCheckedItems());
            ReportParams_Property.galaxy_cut_id = StrCutNo; //Val.Trim(ListCut.Properties.GetCheckedItems());
            ReportParams_Property.galaxy_sieve_name = Val.Trim(ListSieve.Properties.GetCheckedItems());
            ReportParams_Property.galaxy_lot_no = Val.Trim(ListLotNo.Properties.GetCheckedItems());
            ReportParams_Property.galaxy_machine_no = Val.ToString(txtMachineNo.Text);

            if (this.backgroundWorker1.IsBusy)
            {
            }
            else
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        #region Export Grid

        private void Export(string format, string dlgHeader, string dlgFilter)
        {
            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = format;
                svDialog.Title = dlgHeader;
                svDialog.FileName = "Report";
                svDialog.Filter = dlgFilter;
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    string Filepath = svDialog.FileName;

                    switch (format)
                    {
                        case "pdf":
                            dgvKapanReport.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvKapanReport.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvKapanReport.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvKapanReport.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvKapanReport.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvKapanReport.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvKapanReport.ExportToCsv(Filepath);
                            break;
                    }

                    if (format.Equals(Exports.xlsx.ToString()))
                    {
                        if (Global.Confirm("Export Done\n\nYou Want To Open Excel File ?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(Filepath);
                        }
                    }
                    else if (format.Equals(Exports.pdf.ToString()))
                    {
                        if (Global.Confirm("Export Done\n\nYou Want To Open PDF File ?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(Filepath);
                        }
                    }
                    else
                    {
                        if (Global.Confirm("Export Done\n\nYou Want To Open File ?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(Filepath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString(), "Error in Export");
            }
        }
        private void MNExportExcel_Click(object sender, EventArgs e)
        {
            Export("xlsx", "Export to Excel", "Excel files 97-2003 (Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*");
        }
        private void MNPrint_Click(object sender, EventArgs e)
        {
            try
            {
                DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

                PrinterSettingsUsing pst = new PrinterSettingsUsing();

                PrintSystem.PageSettings.AssignDefaultPrinterSettings(pst);

                PrintableComponentLink link = new PrintableComponentLink(PrintSystem);

                link.Component = grdKapanReport;

                dgvKapanReport.OptionsPrint.AutoWidth = true;

                link.Margins.Left = 40;
                link.Margins.Right = 40;
                link.Margins.Bottom = 40;
                link.Margins.Top = 130;
                link.CreateMarginalHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);
                link.CreateMarginalFooterArea += new CreateAreaEventHandler(Link_CreateMarginalFooterArea);
                link.CreateDocument();
                link.ShowPreview();
                link.PrintDlg();
            }
            catch (Exception EX)
            {
                Class.Global.Message(EX.Message);
            }
        }
        public void Link_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {
            // ' For Report Title
            //TextBrick BrickTitle = e.Graph.DrawString(lblReportHeader.Text, System.Drawing.Color.Navy, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 20), DevExpress.XtraPrinting.BorderSide.None);
            //BrickTitle.Font = new Font("Tahoma", 12, FontStyle.Bold);
            //BrickTitle.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            //BrickTitle.VertAlignment = DevExpress.Utils.VertAlignment.Center;

            //// ' For Group 
            //TextBrick BrickTitleseller = e.Graph.DrawString("Group :- " + lblGroupBy.Text, System.Drawing.Color.Navy, new RectangleF(0, 25, e.Graph.ClientPageSize.Width, 20), DevExpress.XtraPrinting.BorderSide.None);
            //BrickTitleseller.Font = new Font("Tahoma", 8, FontStyle.Bold);
            //BrickTitleseller.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            //BrickTitleseller.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            //BrickTitleseller.ForeColor = Color.Black;

            //// ' For Filter 
            //TextBrick BrickTitlesParam = e.Graph.DrawString("Parameters :- " + lblFilter.Text, System.Drawing.Color.Navy, new RectangleF(0, 40, e.Graph.ClientPageSize.Width, 60), DevExpress.XtraPrinting.BorderSide.None);
            //BrickTitlesParam.Font = new Font("Tahoma", 8, FontStyle.Bold);
            //BrickTitlesParam.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            //BrickTitlesParam.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            //BrickTitlesParam.ForeColor = Color.Black;

            //int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 400, 0));
            //TextBrick BrickTitledate = e.Graph.DrawString("Print Date :- " + lblDateTime.Text, System.Drawing.Color.Navy, new RectangleF(IntX, 25, 400, 18), DevExpress.XtraPrinting.BorderSide.None);
            //BrickTitledate.Font = new Font("Tahoma", 8, FontStyle.Bold);
            //BrickTitledate.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            //BrickTitledate.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            //BrickTitledate.ForeColor = Color.Black;
        }
        public void Link_CreateMarginalFooterArea(object sender, CreateAreaEventArgs e)
        {
            int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 100, 0));

            PageInfoBrick BrickPageNo = e.Graph.DrawPageInfo(PageInfo.NumberOfTotal, "Page {0} of {1}", System.Drawing.Color.Navy, new RectangleF(IntX, 0, 100, 15), DevExpress.XtraPrinting.BorderSide.None);
            BrickPageNo.LineAlignment = BrickAlignment.Far;
            BrickPageNo.Alignment = BrickAlignment.Far;
            //BrickPageNo.Font = new Font("Tahoma", 8, FontStyle.Bold);
            BrickPageNo.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            BrickPageNo.VertAlignment = DevExpress.Utils.VertAlignment.Center;
        }

        #endregion

        private void RbtReportType_EditValueChanged(object sender, EventArgs e)
        {
            ObjReportDetailProperty = ObjReportMaster.GetReportDetailProperty(mIntReportCode, Val.ToString(RbtReportType.EditValue));
            mDTDetail = ObjReportMaster.GetDataForSearchSettings(mIntReportCode, Val.ToString(RbtReportType.EditValue));
        }

        private void ListShape_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable DeTemp = new DataTable();
                var temp1 = "";

                temp1 = ListShape.Properties.GetCheckedItems().ToString().Replace(" ", "").Replace("  ", "").Trim();

                StrShape = "";
                StrType = "";
                string[] array = temp1.Split(',');
                if (!string.IsNullOrEmpty(temp1))
                {
                    foreach (var item in array)
                    {
                        StrType += "'" + item + "',";
                    }
                    StrShape = StrType.Remove(StrType.Length - 1);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ListColor_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable DeTemp = new DataTable();
                var temp1 = "";

                temp1 = ListColor.Properties.GetCheckedItems().ToString().Replace(" ", "").Replace("  ", "").Trim();

                StrColor = "";
                StrType = "";
                string[] array = temp1.Split(',');
                if (!string.IsNullOrEmpty(temp1))
                {
                    foreach (var item in array)
                    {
                        StrType += "'" + item + "',";
                    }
                    StrColor = StrType.Remove(StrType.Length - 1);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ListClarity_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable DeTemp = new DataTable();
                var temp1 = "";

                temp1 = ListClarity.Properties.GetCheckedItems().ToString().Replace(" ", "").Replace("  ", "").Trim();

                StrClarity = "";
                StrType = "";
                string[] array = temp1.Split(',');
                if (!string.IsNullOrEmpty(temp1))
                {
                    foreach (var item in array)
                    {
                        StrType += "'" + item + "',";
                    }
                    StrClarity = StrType.Remove(StrType.Length - 1);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ListCut_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable DeTemp = new DataTable();
                var temp1 = "";

                temp1 = ListCut.Properties.GetCheckedItems().ToString().Replace(" ", "").Replace("  ", "").Trim();

                StrCutNo = "";
                StrType = "";
                string[] array = temp1.Split(',');
                if (!string.IsNullOrEmpty(temp1))
                {
                    foreach (var item in array)
                    {
                        StrType += "'" + item + "',";
                    }
                    StrCutNo = StrType.Remove(StrType.Length - 1);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ListSieve_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable DeTemp = new DataTable();
                var temp1 = "";

                temp1 = ListSieve.Properties.GetCheckedItems().ToString().Replace(" ", "").Replace("  ", "").Trim();

                StrSieve = "";
                StrType = "";
                string[] array = temp1.Split(',');
                if (!string.IsNullOrEmpty(temp1))
                {
                    foreach (var item in array)
                    {
                        StrType += "'" + item + "',";
                    }
                    StrSieve = StrType.Remove(StrType.Length - 1);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BtnPending_Click(object sender, EventArgs e)
        {
            PanelLoading.Visible = true;

            if (StrKapan.Length > 0)
            {
                if (StrKapan.ToString().Contains(","))
                {
                    Global.Message("Please Select Atlest One Kapan No");
                    grdKapanReport.DataSource = null;
                    PanelLoading.Visible = false;
                    return;
                }
            }
            else
            {
                Global.Message("Please Select Atlest One Kapan No");
                grdKapanReport.DataSource = null;
                PanelLoading.Visible = false;
                return;
            }
            ReportParams_Property = new ReportParams_Property();
            if (ChkDateWise.Checked == true)
            {
                var result = DateTime.Compare(Convert.ToDateTime(DTPFromDate.Text), Convert.ToDateTime(DTPToDate.Text.ToString()));
                if (result > 0)
                {
                    Global.Message(" Transaction From Date Not Be Greater Than Transaction To Date");
                    return;
                }

                ReportParams_Property.From_Date = Val.DBDate(DTPFromDate.Text);
                ReportParams_Property.To_Date = Val.DBDate(DTPToDate.Text);
            }
            else
            {
                ReportParams_Property.From_Date = null;
                ReportParams_Property.To_Date = null;
            }

            StrReportType = "Lot Wise Summary Report";
            ReportParams_Property.report_type = Val.ToString(ListReportType.Text);
            ReportParams_Property.kapan_no = StrKapan;
            ReportParams_Property.galaxy_shape_id = StrShape; //Val.Trim(ListShape.Properties.GetCheckedItems());
            ReportParams_Property.galaxy_color_id = StrColor; //Val.Trim(ListColor.Properties.GetCheckedItems());
            ReportParams_Property.galaxy_clarity_id = StrClarity; //Val.Trim(ListClarity.Properties.GetCheckedItems());
            ReportParams_Property.galaxy_cut_id = StrCutNo; //Val.Trim(ListCut.Properties.GetCheckedItems());
            ReportParams_Property.galaxy_sieve_name = Val.Trim(ListSieve.Properties.GetCheckedItems());
            ReportParams_Property.galaxy_lot_no = Val.Trim(ListLotNo.Properties.GetCheckedItems());
            ReportParams_Property.galaxy_machine_no = Val.ToString(txtMachineNo.Text);
            Type = "Pending";

            if (this.backgroundWorker1.IsBusy)
            {
            }
            else
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void RepBtnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (Global.Confirm("Are you sure delete selected row?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                int IntRes = 0;
                ReportParams_Property = new ReportParams_Property();

                ReportParams_Property.lot_no = Val.ToInt64(dgvKapanReport.GetFocusedRowCellValue("summary").ToString());
                ReportParams_Property.kapan_no = Val.ToString(dgvKapanReport.GetFocusedRowCellValue("galaxy").ToString());

                IntRes = ObjReportParams.DeleteGalaxyLot_Detail(ReportParams_Property);
                dgvKapanReport.DeleteRow(dgvKapanReport.GetRowHandle(dgvKapanReport.FocusedRowHandle));

                if (IntRes == -1)
                {
                    Global.Confirm("Error in Galaxy Lot No Deleted Data.");
                    ReportParams_Property = null;
                }
                else
                {
                    Global.Confirm("Galaxy Lot No Deleted successfully...");
                    ReportParams_Property = null;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable DTab = (DataTable)grdKapanReport.DataSource;
                Int64 Union_ID = 0;
                Conn = new BeginTranConnection(true, false);
                ObjReportParams = new ReportParams();

                if (DTab.Rows.Count > 0)
                {
                    foreach (DataRow DRaw in DTab.Rows)
                    {
                        ReportParams_Property = new ReportParams_Property();
                        ReportParams_Property.Union_ID = Union_ID;
                        ReportParams_Property.Summary = Val.ToString(DRaw["summary"]);
                        ReportParams_Property.Galaxy = Val.ToString(DRaw["galaxy"]);
                        ReportParams_Property.Summary_Gujarati = Val.ToString(DRaw["summary_gujarati"]);
                        ReportParams_Property.seq_no = Val.ToInt(DRaw["seq_no"]);

                        ReportParams_Property = ObjReportParams.Save_Galaxy_Report(ReportParams_Property, DLL.GlobalDec.EnumTran.Continue, Conn);
                        Union_ID = Val.ToInt64(ReportParams_Property.Union_ID);
                    }
                    Conn.Inter1.Commit();
                    if (Union_ID > 0)
                    {
                        Global.Message("Galaxy Report Data Save Succesfully");
                        BtnReset_Click(null, null);
                    }
                    else
                    {
                        Conn.Inter1.Rollback();
                        Conn = null;
                        Global.Confirm("Error In Galaxy Report Data Save");
                    }
                }
            }
            catch (Exception Ex)
            {
                Conn.Inter1.Rollback();
                Conn = null;
                MessageBox.Show(Ex.ToString());
            }
        }
    }
}
