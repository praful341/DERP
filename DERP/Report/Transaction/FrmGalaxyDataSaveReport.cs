using BLL;
using BLL.FunctionClasses.Report;
using BLL.PropertyClasses.Report;
using DERP.Class;
using System;
using System.Data;
using System.Windows.Forms;

namespace DERP.Report
{
    public partial class FrmGalaxyDataSaveReport : DevExpress.XtraEditors.XtraForm
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

        public FrmGalaxyDataSaveReport()
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

            FillControls();
            FillListControls();
            ListKapan.Focus();
        }
        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            DTab = ObjReportParams.GetGalaxyKapanData_SaveReport(ReportParams_Property, "GALAXY_Kapan_Data_Report");
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {

            PanelLoading.Visible = false;
            //if (mIntPivot == 0)
            //{
            FrmGReportViewerBand FrmGReportViewer = new Report.FrmGReportViewerBand();

            if (chkPivot.Checked == true)
            {
                FrmGReportViewer.IsPivot = true;
            }
            else
            {
                FrmGReportViewer.IsPivot = false;
            }

            FrmGReportViewer.ObjReportDetailProperty = ObjReportDetailProperty;
            FrmGReportViewer.mDTDetail = mDTDetail;
            FrmGReportViewer.Report_Type = Val.ToString(RbtReportType.EditValue);
            FrmGReportViewer.Report_Code = ObjReportDetailProperty.Report_code;

            FrmGReportViewer.Remark = ObjReportDetailProperty.Remark;
            FrmGReportViewer.ReportParams_Property = ReportParams_Property;
            FrmGReportViewer.Procedure_Name = ObjReportDetailProperty.Procedure_Name;
            FrmGReportViewer.FilterByFormName = this.Name;


            FrmGReportViewer.ReportHeaderName = ObjReportDetailProperty.Report_Header_Name;

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
            //}
        }
        private string GetFilterByValue()
        {
            string Str = string.Empty;

            Str += "Filter By : ";

            if (ListKapan.Text.Length > 0)
            {
                Str += ", Kapan : " + ListKapan.Text.ToString();
            }
            return Str;
        }
        private void BtnReset_Click(object sender, EventArgs e)
        {
            chkPivot.Checked = false;
            for (int i = 0; i < ListKapan.Properties.Items.Count; i++)
                ListKapan.Properties.Items[i].CheckState = CheckState.Unchecked;

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
            DataTable DTabKapan_No = ObjFillCombo.FillCmb(FillCombo.TABLE.Galaxy_Distinct_Kapan);
            DTabKapan_No.DefaultView.Sort = "galaxy";
            DTabKapan_No = DTabKapan_No.DefaultView.ToTable();

            ListKapan.Properties.DataSource = DTabKapan_No;
            ListKapan.Properties.DisplayMember = "galaxy";
            ListKapan.Properties.ValueMember = "union_id";
        }
        #endregion

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BtnReport_Click(object sender, EventArgs e)
        {
            PanelLoading.Visible = true;
            ReportParams_Property = new ReportParams_Property();

            StrReportType = "Lot Wise Report";
            ReportParams_Property.kapan_no = StrKapan;


            if (this.backgroundWorker1.IsBusy)
            {
            }
            else
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void RbtReportType_EditValueChanged(object sender, EventArgs e)
        {
            ObjReportDetailProperty = ObjReportMaster.GetReportDetailProperty(mIntReportCode, Val.ToString(RbtReportType.EditValue));
            mDTDetail = ObjReportMaster.GetDataForSearchSettings(mIntReportCode, Val.ToString(RbtReportType.EditValue));
        }

        private void RepBtnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //if (Global.Confirm("Are you sure delete selected row?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            //{
            //    int IntRes = 0;
            //    ReportParams_Property = new ReportParams_Property();

            //    ReportParams_Property.lot_no = Val.ToInt64(dgvKapanReport.GetFocusedRowCellValue("summary").ToString());
            //    ReportParams_Property.kapan_no = Val.ToString(dgvKapanReport.GetFocusedRowCellValue("galaxy").ToString());

            //    IntRes = ObjReportParams.DeleteGalaxyLot_Detail(ReportParams_Property);
            //    dgvKapanReport.DeleteRow(dgvKapanReport.GetRowHandle(dgvKapanReport.FocusedRowHandle));

            //    if (IntRes == -1)
            //    {
            //        Global.Confirm("Error in Galaxy Lot No Deleted Data.");
            //        ReportParams_Property = null;
            //    }
            //    else
            //    {
            //        Global.Confirm("Galaxy Lot No Deleted successfully...");
            //        ReportParams_Property = null;
            //    }
            //}
        }
    }
}
