using BLL;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Report;
using BLL.PropertyClasses.Master;
using BLL.PropertyClasses.Report;
using DERP.Class;
using System;
using System.Data;
using System.Windows.Forms;

namespace DERP.Report
{
    public partial class FrmPriceList : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member
        BLL.FormEvents objBOFormEvents = new BLL.FormEvents();
        BLL.Validation Val = new BLL.Validation();
        BLL.FormPer ObjPer = new BLL.FormPer();
        int mIntReportCode = 0;
        int mIntPivot = 0;
        ReportParams ObjReportParams = new ReportParams();
        New_Report_MasterProperty ObjReportMasterProperty = new New_Report_MasterProperty();
        New_Report_DetailProperty ObjReportDetailProperty = new New_Report_DetailProperty();
        NewReportMaster ObjReportMaster = new NewReportMaster();
        ReportParams_Property ReportParams_Property = new BLL.PropertyClasses.Report.ReportParams_Property();
        DataTable DTab = new DataTable();
        DataSet DTab_PriceList = new DataSet();
        DataTable m_opDate = new DataTable();
        DataTable mDTDetail = new DataTable(BLL.TPV.Table.New_Report_Detail);
        RateTypeMaster ObjRateType = new RateTypeMaster();
        #endregion

        #region Constructor
        public FrmPriceList()
        {
            InitializeComponent();
        }

        private void AttachFormEvents()
        {
            objBOFormEvents.CurForm = this;
            objBOFormEvents.FormKeyPress = true;
            objBOFormEvents.FormKeyDown = true;
            objBOFormEvents.FormResize = true;
            objBOFormEvents.FormClosing = true;
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion

        #region "Events" 

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            DTPFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
            string From_Date = Val.DBDate(m_opDate.Rows[0]["opening_date"].ToString());
            if (From_Date != "")
            {
                DTPFromDate.EditValue = Val.DBDate(m_opDate.Rows[0]["opening_date"].ToString());
            }
            else
            {
                DTPFromDate.EditValue = DateTime.Now;
            }

            DTPToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPToDate.Properties.CharacterCasing = CharacterCasing.Upper;
            DTPToDate.EditValue = DateTime.Now;

            dtpDate.Properties.Items.Clear();
            dtpDate.EditValue = null;
            DtpPriceDate1.Properties.Items.Clear();
            DtpPriceDate1.EditValue = null;
            DtpPriceDate2.Properties.Items.Clear();
            DtpPriceDate2.EditValue = null;
            lueCurrency.EditValue = null;
            lueRateType.EditValue = null;
        }
        private void BtnGenerateReport_Click(object sender, EventArgs e)
        {
            PanelLoading.Visible = true;
            ReportParams_Property.rate_date = Val.DBDate(dtpDate.Text);
            ReportParams_Property.rate_type_id = Val.ToInt32(lueRateType.EditValue);
            ReportParams_Property.currency_id = Val.ToInt32(lueCurrency.EditValue);
            ReportParams_Property.From_Date = Val.DBDate(DTPFromDate.Text);
            ReportParams_Property.To_Date = Val.DBDate(DTPToDate.Text);
            ReportParams_Property.price_date_1 = Val.DBDate(DtpPriceDate1.Text);
            ReportParams_Property.price_date_2 = Val.DBDate(DtpPriceDate2.Text);
            ReportParams_Property.last_sale = Val.ToInt(CmbLastRate.Text);

            if (this.backgroundWorker_PriceList.IsBusy)
            {
            }
            else
            {
                backgroundWorker_PriceList.RunWorkerAsync();
            }
        }
        private void FrmPriceList_Load(object sender, EventArgs e)
        {
            AttachFormEvents();

            this.Show();

            mIntReportCode = Val.ToInt(this.Tag);
            FillControls();
            FillListControls();
        }
        private void backgroundWorker_PriceList_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (Val.ToString(ObjReportDetailProperty.Remark.ToUpper()) == "PRICE_LIST_RPT")
            {
                if (GlobalDec.gEmployeeProperty.location_id == 2)
                {
                    DTab_PriceList = ObjReportParams.GetPriceListRPT(ReportParams_Property, ObjReportDetailProperty.Procedure_Name);
                }
                else
                {
                    if (lueRateType.Text == "MUMBAI")
                    {
                        DTab_PriceList = ObjReportParams.GetPriceListRPT(ReportParams_Property, "MST_Price_List_RPT_MUMBAI");
                    }
                    else
                    {
                        if (ChkOldPrice.Checked == true)
                        {
                            DTab_PriceList = ObjReportParams.GetPriceListRPT(ReportParams_Property, "MST_Old_Price_List_RPT_SURAT");
                        }
                        else
                        {
                            DTab_PriceList = ObjReportParams.GetPriceListRPT(ReportParams_Property, "MST_Price_List_RPT_SURAT");
                        }
                    }
                }
            }
            else if (Val.ToString(ObjReportDetailProperty.Remark.ToUpper()) == "PRICE_LIST_RPT_DIFF")
            {
                if (GlobalDec.gEmployeeProperty.location_id == 2)
                {
                    DTab_PriceList = ObjReportParams.GetPriceListRPT_Diff(ReportParams_Property, ObjReportDetailProperty.Procedure_Name);
                }
            }
            else if (Val.ToString(ObjReportDetailProperty.Remark.ToUpper()) == "WEEKLY_PRICE_ANALYSIS")
            {
                if (GlobalDec.gEmployeeProperty.location_id == 2)
                {
                    DTab_PriceList = ObjReportParams.GetPriceListRPT(ReportParams_Property, ObjReportDetailProperty.Procedure_Name);
                }
            }
            else
            {
                DTab = ObjReportParams.GetPriceList(ReportParams_Property, ObjReportDetailProperty.Procedure_Name);
            }
        }

        private void backgroundWorker_PriceList_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (Val.ToString(ObjReportDetailProperty.Remark.ToUpper()) == "PRICE_LIST_RPT")
            {
                PanelLoading.Visible = false;

                FrmReportViewer FrmReportViewer = new FrmReportViewer();
                foreach (DataTable DTab in DTab_PriceList.Tables)
                    FrmReportViewer.DS.Tables.Add(DTab.Copy());
                FrmReportViewer.GroupBy = "";
                FrmReportViewer.RepName = "";
                FrmReportViewer.RepPara = "";
                this.Cursor = Cursors.Default;
                FrmReportViewer.AllowSetFormula = true;

                if (GlobalDec.gEmployeeProperty.location_id == 2)
                {
                    FrmReportViewer.ShowForm_SubReport("CrtPolishGrading_Price_List_RPT", 120, FrmReportViewer.ReportFolder.PRICE_LIST);
                }
                else
                {
                    if (lueRateType.Text == "MUMBAI")
                    {
                        FrmReportViewer.ShowForm_SubReport("CrtPolishGrading_Price_List_Mumbai_RPT", 120, FrmReportViewer.ReportFolder.MUMBAI_PRICE_LIST);
                    }
                    else
                    {
                        if (ChkOldPrice.Checked == true)
                        {
                            FrmReportViewer.ShowForm_SubReport("CrtPolishGrading_Price_List_RPT", 120, FrmReportViewer.ReportFolder.SURAT_OLD_PRICE_LIST);
                        }
                        else
                        {
                            FrmReportViewer.ShowForm_SubReport("CrtPolishGrading_Price_List_RPT", 120, FrmReportViewer.ReportFolder.SURAT_PRICE_LIST);
                        }
                    }
                }

                DTab_PriceList = null;
                FrmReportViewer.DS.Tables.Clear();
                FrmReportViewer.DS.Clear();
                FrmReportViewer = null;
            }
            else if (Val.ToString(ObjReportDetailProperty.Remark.ToUpper()) == "WEEKLY_PRICE_ANALYSIS")
            {
                PanelLoading.Visible = false;

                FrmReportViewer FrmReportViewer = new FrmReportViewer();
                foreach (DataTable DTab in DTab_PriceList.Tables)
                    FrmReportViewer.DS.Tables.Add(DTab.Copy());
                FrmReportViewer.GroupBy = "";
                FrmReportViewer.RepName = "";
                FrmReportViewer.RepPara = "";
                this.Cursor = Cursors.Default;
                FrmReportViewer.AllowSetFormula = true;

                if (GlobalDec.gEmployeeProperty.location_id == 2)
                {
                    FrmReportViewer.ShowForm_SubReport("CrtPolishGrading_Weekly_Price_List_RPT", 120, FrmReportViewer.ReportFolder.WEEKLY_PRICE_LIST);
                }

                DTab_PriceList = null;
                FrmReportViewer.DS.Tables.Clear();
                FrmReportViewer.DS.Clear();
                FrmReportViewer = null;
            }
            else if (Val.ToString(ObjReportDetailProperty.Remark.ToUpper()) == "PRICE_LIST_RPT_DIFF")
            {
                PanelLoading.Visible = false;

                FrmReportViewer FrmReportViewer = new FrmReportViewer();
                foreach (DataTable DTab in DTab_PriceList.Tables)
                    FrmReportViewer.DS.Tables.Add(DTab.Copy());
                FrmReportViewer.GroupBy = "";
                FrmReportViewer.RepName = "";
                FrmReportViewer.RepPara = "";
                this.Cursor = Cursors.Default;
                FrmReportViewer.AllowSetFormula = true;

                if (GlobalDec.gEmployeeProperty.location_id == 2)
                {
                    FrmReportViewer.ShowForm_SubReport("CrtPolishGrading_Price_List_RPT_Diff", 120, FrmReportViewer.ReportFolder.PRICE_LIST);
                }

                DTab_PriceList = null;
                FrmReportViewer.DS.Tables.Clear();
                FrmReportViewer.DS.Clear();
                FrmReportViewer = null;
            }
            else
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

                    FrmGReportViewer.Group_By_Tag = "";
                    FrmGReportViewer.Group_By_Text = "";
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
                else
                {
                    FrmPReportViewer FrmPReportViewer = new Report.FrmPReportViewer();
                    FrmPReportViewer.Group_By_Tag = "";
                    FrmPReportViewer.Group_By_Text = "";
                    FrmPReportViewer.ObjReportDetailProperty = ObjReportDetailProperty;
                    FrmPReportViewer.mDTDetail = mDTDetail;
                    FrmPReportViewer.Report_Type = Val.ToString(RbtReportType.EditValue);
                    FrmPReportViewer.Report_Code = ObjReportDetailProperty.Report_code;

                    FrmPReportViewer.ReportHeaderName = ObjReportDetailProperty.Report_Header_Name;
                    FrmPReportViewer.FilterBy = GetFilterByValue();
                    FrmPReportViewer.DTab = DTab;
                    if (FrmPReportViewer.DTab == null || FrmPReportViewer.DTab.Rows.Count == 0)
                    {
                        this.Cursor = Cursors.Default;
                        FrmPReportViewer.Dispose();
                        FrmPReportViewer = null;
                        Global.Message("Data Not Found");
                        this.Enabled = true;
                        return;
                    }
                    this.Cursor = Cursors.Default;
                    FrmPReportViewer.ShowForm();
                }
            }
        }
        private void RbtReportType_EditValueChanged(object sender, EventArgs e)
        {
            ObjReportDetailProperty = ObjReportMaster.GetReportDetailProperty(mIntReportCode, Val.ToString(RbtReportType.EditValue));
            mDTDetail = ObjReportMaster.GetDataForSearchSettings(mIntReportCode, Val.ToString(RbtReportType.EditValue));

            if (ListGroupBy.GetTextValue != "")
            {
                ObjReportDetailProperty.Default_Group_By = ListGroupBy.GetTextValue;
            }
            ListGroupBy.DTab = mDTDetail;
            ListGroupBy.Default = ObjReportDetailProperty.Default_Group_By;
        }
        private void lueRateType_EditValueChanged(object sender, EventArgs e)
        {
            if (lueRateType.Text != "")
            {
                dtpDate.Properties.Items.Clear();

                RateType_MasterProperty RateTypeProperty = new RateType_MasterProperty();
                RateTypeProperty.ratetype_id = Val.ToInt(lueRateType.EditValue);
                DataTable DTab = ObjRateType.GetDate(RateTypeProperty);

                if (DTab.Rows.Count > 0)
                {
                    foreach (DataRow DRow in DTab.Rows)
                    {
                        dtpDate.Properties.Items.Add(Convert.ToDateTime(DRow[0]).ToString("dd/MMM/yyyy"));
                    }
                    if (dtpDate.Properties.Items.Count >= 1)
                    {
                        dtpDate.SelectedIndex = 0;
                    }
                }
                else
                {
                    dtpDate.Properties.Items.Clear();
                    dtpDate.EditValue = null;
                }
                RateTypeProperty = null;
            }
        }

        #endregion

        #region "Function" 
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

                if (Val.ToInt(DRow["active"].ToString()) == 0)
                {
                    rd.Enabled = false;
                }

                if (GlobalDec.gEmployeeProperty.user_name == "RIKITA" || GlobalDec.gEmployeeProperty.user_name == "TUSHAR" || GlobalDec.gEmployeeProperty.user_name == "VIRAJ")
                {
                    if (rd.Value.ToString() == "NEW PRICE RPT" || rd.Value.ToString() == "FINAL PRICE DIFF.(CURRENT RATE - NEW RATE)" || rd.Value.ToString() == "NEW PRICE DIFF.(FINAL RATE - CURRENT RATE)" || rd.Value.ToString() == "DAYS WISE PRICE")
                    {
                        rd.Enabled = false;
                    }
                    else
                    {
                        RbtReportType.Properties.Items.Add(rd);
                    }
                }
                else
                {
                    RbtReportType.Properties.Items.Add(rd);
                }
            }
            RbtReportType.SelectedIndex = 0;
            ObjReportMasterProperty = ObjReportMaster.GetReportMasterProperty(mIntReportCode);
            lblTitle.Text = ObjReportMasterProperty.Report_Name;
        }
        private void FillListControls()
        {
            Global.LOOKUPRate(lueRateType);
            Global.LOOKUPCurrency(lueCurrency);

            if (GlobalDec.gEmployeeProperty.location_id == 2)
            {
                lueRateType.EditValue = 2;
                lueCurrency.EditValue = 6;
            }
            else if (GlobalDec.gEmployeeProperty.location_id == 1)
            {
                lueRateType.EditValue = 1;
                lueCurrency.EditValue = 6;
            }
            m_opDate = Global.GetDate();
            DTPFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
            string From_Date = Val.DBDate(m_opDate.Rows[0]["opening_date"].ToString());
            if (From_Date != "")
            {
                DTPFromDate.EditValue = Val.DBDate(m_opDate.Rows[0]["opening_date"].ToString());
            }
            else
            {
                DTPFromDate.EditValue = DateTime.Now;
            }

            DTPToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPToDate.Properties.CharacterCasing = CharacterCasing.Upper;
            DTPToDate.EditValue = DateTime.Now;

            dtpDate.Properties.Items.Clear();
            DtpPriceDate1.Properties.Items.Clear();
            DtpPriceDate2.Properties.Items.Clear();

            RateType_MasterProperty RateTypeProperty = new RateType_MasterProperty();
            RateTypeProperty.ratetype_id = Val.ToInt(lueRateType.EditValue);
            DataTable DTab = ObjRateType.GetDate(RateTypeProperty);

            if (DTab.Rows.Count > 0)
            {
                foreach (DataRow DRow in DTab.Rows)
                {
                    dtpDate.Properties.Items.Add(Convert.ToDateTime(DRow[0]).ToString("dd/MMM/yyyy"));
                    DtpPriceDate1.Properties.Items.Add(Convert.ToDateTime(DRow[0]).ToString("dd/MMM/yyyy"));
                    DtpPriceDate2.Properties.Items.Add(Convert.ToDateTime(DRow[0]).ToString("dd/MMM/yyyy"));
                }
                if (dtpDate.Properties.Items.Count >= 1)
                {
                    dtpDate.SelectedIndex = 0;
                    DtpPriceDate1.SelectedIndex = 0;
                    DtpPriceDate2.SelectedIndex = 0;
                }
            }
            else
            {
                dtpDate.Properties.Items.Clear();
                dtpDate.EditValue = null;
                DtpPriceDate1.Properties.Items.Clear();
                DtpPriceDate1.EditValue = null;
                DtpPriceDate2.Properties.Items.Clear();
                DtpPriceDate2.EditValue = null;
            }
            RateTypeProperty = null;

            if (GlobalDec.gEmployeeProperty.user_name == "RIKITA")
            {
                labelControl4.Visible = false;
                labelControl5.Visible = false;
                DtpPriceDate1.Visible = false;
                DtpPriceDate2.Visible = false;
                CmbLastRate.Visible = false;
                labelControl6.Visible = false;
            }

            if (GlobalDec.gEmployeeProperty.role_name == "SURAT ASSORT" || GlobalDec.gEmployeeProperty.role_name == "SURAT ADMIN")
            {
                ChkOldPrice.Visible = true;
                ChkOldPrice.Checked = false;
            }
            else
            {
                ChkOldPrice.Visible = false;
                ChkOldPrice.Checked = false;
            }
        }
        private string GetFilterByValue()
        {
            string Str = "Filter By : ";

            if (dtpDate.Text != "")
            {
                Str += "Date : " + dtpDate.Text;
            }
            return Str;
        }

        #endregion       
    }
}
