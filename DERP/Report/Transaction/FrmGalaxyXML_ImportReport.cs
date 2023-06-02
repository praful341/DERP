using BLL;
using BLL.FunctionClasses.Report;
using BLL.PropertyClasses.Report;
using DERP.Class;
using System;
using System.Data;
using System.Windows.Forms;

namespace DERP.Report
{
    public partial class FrmGalaxyXML_ImportReport : DevExpress.XtraEditors.XtraForm
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
        string Type = string.Empty;
        #endregion

        #region Counstructor

        public FrmGalaxyXML_ImportReport()
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
            DTPFromDate.Focus();
        }
        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            DTab = ObjReportParams.GetGalaxy_XMLImport_Report(ReportParams_Property, "GALAXY_XML_Import_Data_Report");
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {

            PanelLoading.Visible = false;

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
        }
        private string GetFilterByValue()
        {
            string Str = string.Empty;

            Str += "Filter By : ";

            if (DTPFromDate.Text != "")
            {
                Str += "From Date : " + DTPFromDate.Text;
            }
            if (DTPToDate.Text != "")
            {
                Str += " & As On Date : " + DTPToDate.Text;
            }
            return Str;
        }
        private void BtnReset_Click(object sender, EventArgs e)
        {
            chkPivot.Checked = false;

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

            DTPFromDate.Focus();
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
        #endregion

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void BtnReport_Click(object sender, EventArgs e)
        {
            PanelLoading.Visible = true;
            ReportParams_Property = new ReportParams_Property();

            ReportParams_Property.From_Date = Val.DBDate(DTPFromDate.Text);
            ReportParams_Property.To_Date = Val.DBDate(DTPToDate.Text);

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
    }
}
