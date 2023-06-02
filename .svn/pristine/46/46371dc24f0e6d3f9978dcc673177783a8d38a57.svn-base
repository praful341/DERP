using BLL;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Master.MFG;
using BLL.FunctionClasses.Report;
using BLL.PropertyClasses.Master;
using BLL.PropertyClasses.Report;
using DERP.Class;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace DERP.Report
{
    public partial class FrmMFGAssortmentReport : DevExpress.XtraEditors.XtraForm
    {
        #region "Data Member"
        BLL.FormEvents objBOFormEvents = new BLL.FormEvents();
        BLL.Validation Val = new BLL.Validation();
        string mStrReportGroup = string.Empty;
        int mIntReportCode = 0;
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
        MfgQualityMaster ObjQuality = new MfgQualityMaster();
        string StrColumnName = string.Empty;
        RateTypeMaster ObjRateType = new RateTypeMaster();
        DataTable DTab = new DataTable();
        DataSet DTab_IssueJanged = new DataSet();
        DataSet DSetSemi2 = new DataSet();
        DataTable DTab_Semi1 = new DataTable();
        DataTable DTab_IssueJangedSemi1 = new DataTable();
        DataSet dtList = new DataSet();
        DataTable m_opDate = new DataTable();
        Control _NextEnteredControl = new Control();
        ReportParams_Property ReportParams_Property = new BLL.PropertyClasses.Report.ReportParams_Property();
        string StrType = string.Empty;
        string StrTransactionType = string.Empty;
        string StrTempQuality = string.Empty;
        DataTable DTabTempQuality = new DataTable();

        #endregion

        #region Counstructor

        public FrmMFGAssortmentReport()
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

            AddGotFocusListener(this);
            AddKeyPressListener(this);

            this.Show();

            mIntReportCode = Val.ToInt(this.Tag);

            FillControls();
            FillListControls();
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
            DTPFromDate.Focus();

            dtpRateDate.Properties.Items.Clear();

            RateType_MasterProperty RateTypeProperty = new RateType_MasterProperty();
            RateTypeProperty.ratetype_id = Val.ToInt(1);
            DataTable DTab = ObjRateType.GetDate(RateTypeProperty);

            if (DTab.Rows.Count > 0)
            {
                foreach (DataRow DRow in DTab.Rows)
                {
                    dtpRateDate.Properties.Items.Add(Convert.ToDateTime(DRow[0]).ToString("dd/MMM/yyyy"));
                }
                if (dtpRateDate.Properties.Items.Count >= 1)
                {
                    dtpRateDate.SelectedIndex = 0;
                }
            }
            else
            {
                dtpRateDate.Properties.Items.Clear();
                dtpRateDate.EditValue = null;
            }
            RateTypeProperty = null;
        }
        private void AddGotFocusListener(Control ctrl)
        {
            foreach (Control c in ctrl.Controls)
            {
                c.GotFocus += new EventHandler(Control_GotFocus);
                if (c.Controls.Count > 0)
                {
                    AddGotFocusListener(c);
                }
            }
        }
        private void Control_GotFocus(object sender, EventArgs e)
        {
            if (!((Control)sender).Name.ToString().Trim().Equals(string.Empty))
            {
                _NextEnteredControl = (Control)sender;
                if ((Control)sender is LookUpEdit)
                {
                    ((LookUpEdit)(Control)sender).ShowPopup();
                }
                else if ((Control)sender is CheckedComboBoxEdit)
                {
                    ((CheckedComboBoxEdit)(Control)sender).ShowPopup();
                }
            }
        }
        private void AddKeyPressListener(Control ctrl)
        {
            foreach (Control c in ctrl.Controls)
            {
                c.KeyPress += new KeyPressEventHandler(Control_KeyPress);
                if (c.Controls.Count > 0)
                {
                    AddKeyPressListener(c);
                }
            }
        }
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((Control)sender).Name.ToString().Trim().Equals(string.Empty))
            {
                _NextEnteredControl = (Control)sender;
                if ((Control)sender is LookUpEdit)
                {
                    if (e.KeyChar == Convert.ToChar(Keys.Enter))
                    {
                        SendKeys.Send("{TAB}");
                    }
                }
                if ((Control)sender is CheckedComboBoxEdit)
                {
                    if (e.KeyChar == Convert.ToChar(Keys.Enter))
                    {
                        SendKeys.Send("{TAB}");
                    }
                }
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            DTPFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
            DTPFromDate.EditValue = Val.DBDate(m_opDate.Rows[0]["opening_date"].ToString());

            DTPToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPToDate.Properties.CharacterCasing = CharacterCasing.Upper;


            DTPToDate.EditValue = DateTime.Now;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void BtnGenerateReport_Click(object sender, EventArgs e)
        {
            var result = DateTime.Compare(Convert.ToDateTime(DTPFromDate.Text), Convert.ToDateTime(DTPToDate.Text.ToString()));
            if (result > 0)
            {
                Global.Message(" Transaction From Date Not Be Greater Than Transaction To Date");
                return;
            }

            if (Val.ToString(ObjReportDetailProperty.Remark.ToUpper()) == "MFG_PROCESS_ASSORTMENT_CUT_COMPARISION")
            {

            }

            PanelLoading.Visible = true;
            ReportParams_Property.From_Date = Val.DBDate(DTPFromDate.Text);
            ReportParams_Property.To_Date = Val.DBDate(DTPToDate.Text);
            ReportParams_Property.kapan_id = Val.Trim(ListKapan.Properties.GetCheckedItems());
            ReportParams_Property.cut_id = Val.Trim(ListRoughCut.Properties.GetCheckedItems());
            //ReportParams_Property.rough_sieve_id = Val.Trim(ListSieve.Properties.GetCheckedItems());
            ReportParams_Property.rough_cut_no = Val.ToString(ListRoughCut.Text);
            ReportParams_Property.temp_quality_name = Val.Trim(ListQuality.Properties.GetCheckedItems());
            ReportParams_Property.kapan_no = Val.ToString(ListKapan.Text);
            ReportParams_Property.rate_date = Val.DBDate(dtpRateDate.Text);

            if (RBtnLocationType.EditValue.ToString() == "1")
            {
                ReportParams_Property.location_id = Val.ToString(1);
            }
            else
            {
                ReportParams_Property.location_id = Val.ToString(2);
            }

            if (ReportParams_Property.temp_quality_name.ToString() != "")
            {
                DataTable Dtab = DTabTempQuality.Select("lot_srno in(" + ReportParams_Property.temp_quality_name.ToString() + ")").CopyToDataTable();
                string StrLotList = "";
                for (int i = 0; i < Dtab.Rows.Count; i++)
                {
                    if (StrLotList.Length > 0)
                    {
                        StrLotList = StrLotList + ",(" + Dtab.Rows[i]["temp_quality_name_old"].ToString() + ")";
                    }
                    else
                    {
                        StrLotList = "(" + Dtab.Rows[i]["temp_quality_name_old"].ToString() + ")";
                    }
                }
                ReportParams_Property.temp_quality_name_Trim = Val.ToString(StrLotList);
            }
            else
            {
                ReportParams_Property.temp_quality_name_Trim = Val.ToString("");
            }

            if (this.backgroundWorker1.IsBusy)
            {
            }
            else
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }
        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            int IntRes = 0;
            IntRes = ObjReportParams.All_In_One_AssortReport_Save(ReportParams_Property, "MFG_TRN_All_In_One_Report_History_Save");

            if (Val.ToString(ObjReportDetailProperty.Remark.ToUpper()) == "MFG_PROCESS_ASSORTMENT_SUMMARY")
            {
                // DTab_IssueJanged = ObjReportParams.GetMFGAssortment_AllINOne_Data(ReportParams_Property, ObjReportDetailProperty.Procedure_Name);
                if (RBtnLocationType.EditValue.ToString() == "1")
                {
                    if (RBtnType.SelectedIndex == 0)
                    {
                        DTab_IssueJanged = ObjReportParams.GetMFGAssortment_AllINOne_Data(ReportParams_Property, ObjReportDetailProperty.Procedure_Name);
                    }
                    else if (RBtnType.SelectedIndex == 1)
                    {
                        DTab_IssueJanged = ObjReportParams.Print_Assort_Final_Print2_ALL(ReportParams_Property, "RPT_MFG_TRN_Assortment_Final_Print_2_ALL");
                    }
                    else if (RBtnType.SelectedIndex == 2)
                    {
                        DTab_IssueJanged = ObjReportParams.Print_Assort_Final_Print2_ALL(ReportParams_Property, "RPT_MFG_TRN_Assortment_Final_Print4_ALL");

                        for (int i = 0; i <= DTab_IssueJanged.Tables[1].Rows.Count - 1; i++)
                        {
                            if (DTab_IssueJanged.Tables[1].Rows[i]["purity_name"].ToString() == "FW X-1")
                            {
                                DataTable DTab_New = DTab_IssueJanged.Tables[1].Select("purity_name = 'WT X-1'").CopyToDataTable();
                                DTab_IssueJanged.Tables[1].Rows[i]["-2WT"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["-2WT"]) + Val.ToDecimal(DTab_New.Rows[0]["-2WT"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["-2_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["-2_per"]) + Val.ToDecimal(DTab_New.Rows[0]["-2_per"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["+2WT"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["+2WT"]) + Val.ToDecimal(DTab_New.Rows[0]["+2WT"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["+2_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["+2_per"]) + Val.ToDecimal(DTab_New.Rows[0]["+2_per"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["total_carat"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["total_carat"]) + Val.ToDecimal(DTab_New.Rows[0]["total_carat"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["total_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["total_per"]) + Val.ToDecimal(DTab_New.Rows[0]["total_per"]);
                            }
                            else if (DTab_IssueJanged.Tables[1].Rows[i]["purity_name"].ToString() == "FW X-2")
                            {
                                DataTable DTab_New = DTab_IssueJanged.Tables[1].Select("purity_name = 'WT X-2'").CopyToDataTable();
                                DTab_IssueJanged.Tables[1].Rows[i]["-2WT"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["-2WT"]) + Val.ToDecimal(DTab_New.Rows[0]["-2WT"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["-2_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["-2_per"]) + Val.ToDecimal(DTab_New.Rows[0]["-2_per"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["+2WT"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["+2WT"]) + Val.ToDecimal(DTab_New.Rows[0]["+2WT"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["+2_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["+2_per"]) + Val.ToDecimal(DTab_New.Rows[0]["+2_per"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["total_carat"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["total_carat"]) + Val.ToDecimal(DTab_New.Rows[0]["total_carat"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["total_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["total_per"]) + Val.ToDecimal(DTab_New.Rows[0]["total_per"]);
                            }
                            else if (DTab_IssueJanged.Tables[1].Rows[i]["purity_name"].ToString() == "FW Y-1")
                            {
                                DataTable DTab_New = DTab_IssueJanged.Tables[1].Select("purity_name = 'WT Y-1'").CopyToDataTable();
                                DTab_IssueJanged.Tables[1].Rows[i]["-2WT"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["-2WT"]) + Val.ToDecimal(DTab_New.Rows[0]["-2WT"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["-2_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["-2_per"]) + Val.ToDecimal(DTab_New.Rows[0]["-2_per"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["+2WT"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["+2WT"]) + Val.ToDecimal(DTab_New.Rows[0]["+2WT"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["+2_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["+2_per"]) + Val.ToDecimal(DTab_New.Rows[0]["+2_per"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["total_carat"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["total_carat"]) + Val.ToDecimal(DTab_New.Rows[0]["total_carat"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["total_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["total_per"]) + Val.ToDecimal(DTab_New.Rows[0]["total_per"]);
                            }
                            else if (DTab_IssueJanged.Tables[1].Rows[i]["purity_name"].ToString() == "FW Y-2")
                            {
                                DataTable DTab_New = DTab_IssueJanged.Tables[1].Select("purity_name = 'WT Y-2'").CopyToDataTable();
                                DTab_IssueJanged.Tables[1].Rows[i]["-2WT"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["-2WT"]) + Val.ToDecimal(DTab_New.Rows[0]["-2WT"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["-2_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["-2_per"]) + Val.ToDecimal(DTab_New.Rows[0]["-2_per"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["+2WT"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["+2WT"]) + Val.ToDecimal(DTab_New.Rows[0]["+2WT"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["+2_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["+2_per"]) + Val.ToDecimal(DTab_New.Rows[0]["+2_per"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["total_carat"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["total_carat"]) + Val.ToDecimal(DTab_New.Rows[0]["total_carat"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["total_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["total_per"]) + Val.ToDecimal(DTab_New.Rows[0]["total_per"]);
                            }
                            else if (DTab_IssueJanged.Tables[1].Rows[i]["purity_name"].ToString() == "FW Z-1")
                            {
                                DataTable DTab_New = DTab_IssueJanged.Tables[1].Select("purity_name = 'WT Z-1'").CopyToDataTable();
                                DTab_IssueJanged.Tables[1].Rows[i]["-2WT"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["-2WT"]) + Val.ToDecimal(DTab_New.Rows[0]["-2WT"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["-2_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["-2_per"]) + Val.ToDecimal(DTab_New.Rows[0]["-2_per"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["+2WT"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["+2WT"]) + Val.ToDecimal(DTab_New.Rows[0]["+2WT"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["+2_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["+2_per"]) + Val.ToDecimal(DTab_New.Rows[0]["+2_per"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["total_carat"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["total_carat"]) + Val.ToDecimal(DTab_New.Rows[0]["total_carat"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["total_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["total_per"]) + Val.ToDecimal(DTab_New.Rows[0]["total_per"]);
                            }
                            else if (DTab_IssueJanged.Tables[1].Rows[i]["purity_name"].ToString() == "FW Z-2")
                            {
                                DataTable DTab_New = DTab_IssueJanged.Tables[1].Select("purity_name = 'WT Z-2'").CopyToDataTable();
                                DTab_IssueJanged.Tables[1].Rows[i]["-2WT"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["-2WT"]) + Val.ToDecimal(DTab_New.Rows[0]["-2WT"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["-2_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["-2_per"]) + Val.ToDecimal(DTab_New.Rows[0]["-2_per"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["+2WT"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["+2WT"]) + Val.ToDecimal(DTab_New.Rows[0]["+2WT"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["+2_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["+2_per"]) + Val.ToDecimal(DTab_New.Rows[0]["+2_per"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["total_carat"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["total_carat"]) + Val.ToDecimal(DTab_New.Rows[0]["total_carat"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["total_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["total_per"]) + Val.ToDecimal(DTab_New.Rows[0]["total_per"]);
                            }
                            else if (DTab_IssueJanged.Tables[1].Rows[i]["purity_name"].ToString() == "FW 1-1")
                            {
                                DataTable DTab_New = DTab_IssueJanged.Tables[1].Select("purity_name = 'WT 1-1'").CopyToDataTable();
                                DTab_IssueJanged.Tables[1].Rows[i]["-2WT"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["-2WT"]) + Val.ToDecimal(DTab_New.Rows[0]["-2WT"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["-2_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["-2_per"]) + Val.ToDecimal(DTab_New.Rows[0]["-2_per"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["+2WT"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["+2WT"]) + Val.ToDecimal(DTab_New.Rows[0]["+2WT"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["+2_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["+2_per"]) + Val.ToDecimal(DTab_New.Rows[0]["+2_per"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["total_carat"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["total_carat"]) + Val.ToDecimal(DTab_New.Rows[0]["total_carat"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["total_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["total_per"]) + Val.ToDecimal(DTab_New.Rows[0]["total_per"]);
                            }
                            else if (DTab_IssueJanged.Tables[1].Rows[i]["purity_name"].ToString() == "FW 1-2")
                            {
                                DataTable DTab_New = DTab_IssueJanged.Tables[1].Select("purity_name = 'WT 1-2'").CopyToDataTable();
                                DTab_IssueJanged.Tables[1].Rows[i]["-2WT"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["-2WT"]) + Val.ToDecimal(DTab_New.Rows[0]["-2WT"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["-2_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["-2_per"]) + Val.ToDecimal(DTab_New.Rows[0]["-2_per"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["+2WT"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["+2WT"]) + Val.ToDecimal(DTab_New.Rows[0]["+2WT"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["+2_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["+2_per"]) + Val.ToDecimal(DTab_New.Rows[0]["+2_per"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["total_carat"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["total_carat"]) + Val.ToDecimal(DTab_New.Rows[0]["total_carat"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["total_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["total_per"]) + Val.ToDecimal(DTab_New.Rows[0]["total_per"]);
                            }
                            else if (DTab_IssueJanged.Tables[1].Rows[i]["purity_name"].ToString() == "FW 2-3")
                            {
                                DataTable DTab_New = DTab_IssueJanged.Tables[1].Select("purity_name = 'WT 2'").CopyToDataTable();
                                DTab_IssueJanged.Tables[1].Rows[i]["-2WT"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["-2WT"]) + Val.ToDecimal(DTab_New.Rows[0]["-2WT"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["-2_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["-2_per"]) + Val.ToDecimal(DTab_New.Rows[0]["-2_per"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["+2WT"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["+2WT"]) + Val.ToDecimal(DTab_New.Rows[0]["+2WT"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["+2_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["+2_per"]) + Val.ToDecimal(DTab_New.Rows[0]["+2_per"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["total_carat"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["total_carat"]) + Val.ToDecimal(DTab_New.Rows[0]["total_carat"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["total_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["total_per"]) + Val.ToDecimal(DTab_New.Rows[0]["total_per"]);
                            }
                            else if (DTab_IssueJanged.Tables[1].Rows[i]["purity_name"].ToString() == "FW 4-5")
                            {
                                DataTable DTab_New = DTab_IssueJanged.Tables[1].Select("purity_name = 'WT 3-4'").CopyToDataTable();
                                DTab_IssueJanged.Tables[1].Rows[i]["-2WT"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["-2WT"]) + Val.ToDecimal(DTab_New.Rows[0]["-2WT"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["-2_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["-2_per"]) + Val.ToDecimal(DTab_New.Rows[0]["-2_per"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["+2WT"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["+2WT"]) + Val.ToDecimal(DTab_New.Rows[0]["+2WT"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["+2_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["+2_per"]) + Val.ToDecimal(DTab_New.Rows[0]["+2_per"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["total_carat"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["total_carat"]) + Val.ToDecimal(DTab_New.Rows[0]["total_carat"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["total_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["total_per"]) + Val.ToDecimal(DTab_New.Rows[0]["total_per"]);
                            }
                            else if (DTab_IssueJanged.Tables[1].Rows[i]["purity_name"].ToString() == "FW 6-7")
                            {
                                DataTable DTab_New = DTab_IssueJanged.Tables[1].Select("purity_name = 'WT 5-6'").CopyToDataTable();
                                DTab_IssueJanged.Tables[1].Rows[i]["-2WT"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["-2WT"]) + Val.ToDecimal(DTab_New.Rows[0]["-2WT"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["-2_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["-2_per"]) + Val.ToDecimal(DTab_New.Rows[0]["-2_per"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["+2WT"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["+2WT"]) + Val.ToDecimal(DTab_New.Rows[0]["+2WT"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["+2_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["+2_per"]) + Val.ToDecimal(DTab_New.Rows[0]["+2_per"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["total_carat"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["total_carat"]) + Val.ToDecimal(DTab_New.Rows[0]["total_carat"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["total_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["total_per"]) + Val.ToDecimal(DTab_New.Rows[0]["total_per"]);
                            }
                            else if (DTab_IssueJanged.Tables[1].Rows[i]["purity_name"].ToString() == "FW 8-9")
                            {
                                DataTable DTab_New = DTab_IssueJanged.Tables[1].Select("purity_name = 'WT 7-8'").CopyToDataTable();
                                DTab_IssueJanged.Tables[1].Rows[i]["-2WT"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["-2WT"]) + Val.ToDecimal(DTab_New.Rows[0]["-2WT"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["-2_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["-2_per"]) + Val.ToDecimal(DTab_New.Rows[0]["-2_per"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["+2WT"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["+2WT"]) + Val.ToDecimal(DTab_New.Rows[0]["+2WT"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["+2_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["+2_per"]) + Val.ToDecimal(DTab_New.Rows[0]["+2_per"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["total_carat"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["total_carat"]) + Val.ToDecimal(DTab_New.Rows[0]["total_carat"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["total_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["total_per"]) + Val.ToDecimal(DTab_New.Rows[0]["total_per"]);
                            }
                            else if (DTab_IssueJanged.Tables[1].Rows[i]["purity_name"].ToString() == "FW 10-11-12")
                            {
                                DataTable DTab_New = DTab_IssueJanged.Tables[1].Select("purity_name = 'WT 9-10-11-12'").CopyToDataTable();
                                DTab_IssueJanged.Tables[1].Rows[i]["-2WT"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["-2WT"]) + Val.ToDecimal(DTab_New.Rows[0]["-2WT"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["-2_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["-2_per"]) + Val.ToDecimal(DTab_New.Rows[0]["-2_per"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["+2WT"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["+2WT"]) + Val.ToDecimal(DTab_New.Rows[0]["+2WT"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["+2_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["+2_per"]) + Val.ToDecimal(DTab_New.Rows[0]["+2_per"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["total_carat"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["total_carat"]) + Val.ToDecimal(DTab_New.Rows[0]["total_carat"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["total_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["total_per"]) + Val.ToDecimal(DTab_New.Rows[0]["total_per"]);
                            }
                            else if (DTab_IssueJanged.Tables[1].Rows[i]["purity_name"].ToString() == "FW LSTR")
                            {
                                DataTable DTab_New = DTab_IssueJanged.Tables[1].Select("purity_name = 'WT WK'").CopyToDataTable();
                                DTab_IssueJanged.Tables[1].Rows[i]["-2WT"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["-2WT"]) + Val.ToDecimal(DTab_New.Rows[0]["-2WT"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["-2_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["-2_per"]) + Val.ToDecimal(DTab_New.Rows[0]["-2_per"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["+2WT"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["+2WT"]) + Val.ToDecimal(DTab_New.Rows[0]["+2WT"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["+2_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["+2_per"]) + Val.ToDecimal(DTab_New.Rows[0]["+2_per"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["total_carat"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["total_carat"]) + Val.ToDecimal(DTab_New.Rows[0]["total_carat"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["total_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["total_per"]) + Val.ToDecimal(DTab_New.Rows[0]["total_per"]);
                            }
                            else if (DTab_IssueJanged.Tables[1].Rows[i]["purity_name"].ToString() == "FW A-1")
                            {
                                DataTable DTab_New = DTab_IssueJanged.Tables[1].Select("purity_name = 'WT A-1'").CopyToDataTable();
                                DTab_IssueJanged.Tables[1].Rows[i]["-2WT"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["-2WT"]) + Val.ToDecimal(DTab_New.Rows[0]["-2WT"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["-2_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["-2_per"]) + Val.ToDecimal(DTab_New.Rows[0]["-2_per"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["+2WT"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["+2WT"]) + Val.ToDecimal(DTab_New.Rows[0]["+2WT"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["+2_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["+2_per"]) + Val.ToDecimal(DTab_New.Rows[0]["+2_per"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["total_carat"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["total_carat"]) + Val.ToDecimal(DTab_New.Rows[0]["total_carat"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["total_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["total_per"]) + Val.ToDecimal(DTab_New.Rows[0]["total_per"]);
                            }
                            else if (DTab_IssueJanged.Tables[1].Rows[i]["purity_name"].ToString() == "FW A-2")
                            {
                                DataTable DTab_New = DTab_IssueJanged.Tables[1].Select("purity_name = 'WT A-2'").CopyToDataTable();
                                DTab_IssueJanged.Tables[1].Rows[i]["-2WT"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["-2WT"]) + Val.ToDecimal(DTab_New.Rows[0]["-2WT"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["-2_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["-2_per"]) + Val.ToDecimal(DTab_New.Rows[0]["-2_per"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["+2WT"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["+2WT"]) + Val.ToDecimal(DTab_New.Rows[0]["+2WT"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["+2_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["+2_per"]) + Val.ToDecimal(DTab_New.Rows[0]["+2_per"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["total_carat"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["total_carat"]) + Val.ToDecimal(DTab_New.Rows[0]["total_carat"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["total_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["total_per"]) + Val.ToDecimal(DTab_New.Rows[0]["total_per"]);
                            }
                            else if (DTab_IssueJanged.Tables[1].Rows[i]["purity_name"].ToString() == "FW B-1")
                            {
                                DataTable DTab_New = DTab_IssueJanged.Tables[1].Select("purity_name = 'WT B-1'").CopyToDataTable();
                                DTab_IssueJanged.Tables[1].Rows[i]["-2WT"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["-2WT"]) + Val.ToDecimal(DTab_New.Rows[0]["-2WT"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["-2_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["-2_per"]) + Val.ToDecimal(DTab_New.Rows[0]["-2_per"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["+2WT"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["+2WT"]) + Val.ToDecimal(DTab_New.Rows[0]["+2WT"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["+2_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["+2_per"]) + Val.ToDecimal(DTab_New.Rows[0]["+2_per"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["total_carat"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["total_carat"]) + Val.ToDecimal(DTab_New.Rows[0]["total_carat"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["total_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["total_per"]) + Val.ToDecimal(DTab_New.Rows[0]["total_per"]);
                            }
                            else if (DTab_IssueJanged.Tables[1].Rows[i]["purity_name"].ToString() == "FW B-2")
                            {
                                DataTable DTab_New = DTab_IssueJanged.Tables[1].Select("purity_name = 'WT B-2'").CopyToDataTable();
                                DTab_IssueJanged.Tables[1].Rows[i]["-2WT"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["-2WT"]) + Val.ToDecimal(DTab_New.Rows[0]["-2WT"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["-2_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["-2_per"]) + Val.ToDecimal(DTab_New.Rows[0]["-2_per"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["+2WT"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["+2WT"]) + Val.ToDecimal(DTab_New.Rows[0]["+2WT"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["+2_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["+2_per"]) + Val.ToDecimal(DTab_New.Rows[0]["+2_per"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["total_carat"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["total_carat"]) + Val.ToDecimal(DTab_New.Rows[0]["total_carat"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["total_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["total_per"]) + Val.ToDecimal(DTab_New.Rows[0]["total_per"]);
                            }
                            else if (DTab_IssueJanged.Tables[1].Rows[i]["purity_name"].ToString() == "FW C-1")
                            {
                                DataTable DTab_New = DTab_IssueJanged.Tables[1].Select("purity_name = 'WT C-1'").CopyToDataTable();
                                DTab_IssueJanged.Tables[1].Rows[i]["-2WT"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["-2WT"]) + Val.ToDecimal(DTab_New.Rows[0]["-2WT"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["-2_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["-2_per"]) + Val.ToDecimal(DTab_New.Rows[0]["-2_per"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["+2WT"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["+2WT"]) + Val.ToDecimal(DTab_New.Rows[0]["+2WT"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["+2_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["+2_per"]) + Val.ToDecimal(DTab_New.Rows[0]["+2_per"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["total_carat"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["total_carat"]) + Val.ToDecimal(DTab_New.Rows[0]["total_carat"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["total_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["total_per"]) + Val.ToDecimal(DTab_New.Rows[0]["total_per"]);
                            }
                            else if (DTab_IssueJanged.Tables[1].Rows[i]["purity_name"].ToString() == "FW C-2")
                            {
                                DataTable DTab_New = DTab_IssueJanged.Tables[1].Select("purity_name = 'WT C-2'").CopyToDataTable();
                                DTab_IssueJanged.Tables[1].Rows[i]["-2WT"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["-2WT"]) + Val.ToDecimal(DTab_New.Rows[0]["-2WT"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["-2_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["-2_per"]) + Val.ToDecimal(DTab_New.Rows[0]["-2_per"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["+2WT"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["+2WT"]) + Val.ToDecimal(DTab_New.Rows[0]["+2WT"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["+2_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["+2_per"]) + Val.ToDecimal(DTab_New.Rows[0]["+2_per"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["total_carat"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["total_carat"]) + Val.ToDecimal(DTab_New.Rows[0]["total_carat"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["total_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["total_per"]) + Val.ToDecimal(DTab_New.Rows[0]["total_per"]);
                            }
                            else if (DTab_IssueJanged.Tables[1].Rows[i]["purity_name"].ToString() == "FW D-1")
                            {
                                DataTable DTab_New = DTab_IssueJanged.Tables[1].Select("purity_name = 'WT D-1'").CopyToDataTable();
                                DTab_IssueJanged.Tables[1].Rows[i]["-2WT"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["-2WT"]) + Val.ToDecimal(DTab_New.Rows[0]["-2WT"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["-2_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["-2_per"]) + Val.ToDecimal(DTab_New.Rows[0]["-2_per"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["+2WT"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["+2WT"]) + Val.ToDecimal(DTab_New.Rows[0]["+2WT"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["+2_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["+2_per"]) + Val.ToDecimal(DTab_New.Rows[0]["+2_per"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["total_carat"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["total_carat"]) + Val.ToDecimal(DTab_New.Rows[0]["total_carat"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["total_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["total_per"]) + Val.ToDecimal(DTab_New.Rows[0]["total_per"]);
                            }
                            else if (DTab_IssueJanged.Tables[1].Rows[i]["purity_name"].ToString() == "FW D-2")
                            {
                                DataTable DTab_New = DTab_IssueJanged.Tables[1].Select("purity_name = 'WT D-2'").CopyToDataTable();
                                DTab_IssueJanged.Tables[1].Rows[i]["-2WT"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["-2WT"]) + Val.ToDecimal(DTab_New.Rows[0]["-2WT"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["-2_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["-2_per"]) + Val.ToDecimal(DTab_New.Rows[0]["-2_per"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["+2WT"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["+2WT"]) + Val.ToDecimal(DTab_New.Rows[0]["+2WT"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["+2_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["+2_per"]) + Val.ToDecimal(DTab_New.Rows[0]["+2_per"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["total_carat"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["total_carat"]) + Val.ToDecimal(DTab_New.Rows[0]["total_carat"]);
                                DTab_IssueJanged.Tables[1].Rows[i]["total_per"] = Val.ToDecimal(DTab_IssueJanged.Tables[1].Rows[i]["total_per"]) + Val.ToDecimal(DTab_New.Rows[0]["total_per"]);
                            }

                            if (DTab_IssueJanged.Tables[1].Rows[i]["purity_name"].ToString() == "WT X-1" || DTab_IssueJanged.Tables[1].Rows[i]["purity_name"].ToString() == "WT X-2" || DTab_IssueJanged.Tables[1].Rows[i]["purity_name"].ToString() == "WT Y-1" || DTab_IssueJanged.Tables[1].Rows[i]["purity_name"].ToString() == "WT Y-2" ||
                                DTab_IssueJanged.Tables[1].Rows[i]["purity_name"].ToString() == "WT Z-1" || DTab_IssueJanged.Tables[1].Rows[i]["purity_name"].ToString() == "WT Z-2" || DTab_IssueJanged.Tables[1].Rows[i]["purity_name"].ToString() == "WT 1-1" || DTab_IssueJanged.Tables[1].Rows[i]["purity_name"].ToString() == "WT 1-2" ||
                                DTab_IssueJanged.Tables[1].Rows[i]["purity_name"].ToString() == "WT 2" || DTab_IssueJanged.Tables[1].Rows[i]["purity_name"].ToString() == "WT 3-4" || DTab_IssueJanged.Tables[1].Rows[i]["purity_name"].ToString() == "WT 5-6" || DTab_IssueJanged.Tables[1].Rows[i]["purity_name"].ToString() == "WT 7-8" ||
                                DTab_IssueJanged.Tables[1].Rows[i]["purity_name"].ToString() == "WT 9-10-11-12" || DTab_IssueJanged.Tables[1].Rows[i]["purity_name"].ToString() == "WT WK")
                            {
                                DTab_IssueJanged.Tables[1].Rows[i]["-2WT"] = "0.000";
                                DTab_IssueJanged.Tables[1].Rows[i]["-2_per"] = "0";
                                DTab_IssueJanged.Tables[1].Rows[i]["+2WT"] = "0.000";
                                DTab_IssueJanged.Tables[1].Rows[i]["+2_per"] = "0";
                                DTab_IssueJanged.Tables[1].Rows[i]["total_carat"] = "0.000";
                                DTab_IssueJanged.Tables[1].Rows[i]["total_per"] = "0";
                            }
                            else if (DTab_IssueJanged.Tables[1].Rows[i]["purity_name"].ToString() == "WT A-1" || DTab_IssueJanged.Tables[1].Rows[i]["purity_name"].ToString() == "WT A-2" || DTab_IssueJanged.Tables[1].Rows[i]["purity_name"].ToString() == "WT B-1" || DTab_IssueJanged.Tables[1].Rows[i]["purity_name"].ToString() == "WT B-2" ||
                               DTab_IssueJanged.Tables[1].Rows[i]["purity_name"].ToString() == "WT C-1" || DTab_IssueJanged.Tables[1].Rows[i]["purity_name"].ToString() == "WT C-2" || DTab_IssueJanged.Tables[1].Rows[i]["purity_name"].ToString() == "WT D-1" || DTab_IssueJanged.Tables[1].Rows[i]["purity_name"].ToString() == "WT D-2")
                            {
                                DTab_IssueJanged.Tables[1].Rows[i]["-2WT"] = "0.000";
                                DTab_IssueJanged.Tables[1].Rows[i]["-2_per"] = "0";
                                DTab_IssueJanged.Tables[1].Rows[i]["+2WT"] = "0.000";
                                DTab_IssueJanged.Tables[1].Rows[i]["+2_per"] = "0";
                                DTab_IssueJanged.Tables[1].Rows[i]["total_carat"] = "0.000";
                                DTab_IssueJanged.Tables[1].Rows[i]["total_per"] = "0";
                            }
                        }
                    }
                }
                else if (RBtnLocationType.EditValue.ToString() == "2")
                {
                    DTab_IssueJanged = ObjReportParams.Print_Assort_Final_Mumbai(ReportParams_Property, "RPT_MFG_TRN_Assortment_Final_Mumbai_All");
                }
            }
            else if (Val.ToString(ObjReportDetailProperty.Remark.ToUpper()) == "MFG_PROCESS_ASSORTMENT_CUT_COMPARISION")
            {
                if (RBtnLocationType.EditValue.ToString() == "1")
                {
                    if (RBtnType.SelectedIndex == 0)
                    {
                        DTab_IssueJanged = ObjReportParams.GetMFGAssortment_AllINOne_Data(ReportParams_Property, ObjReportDetailProperty.Procedure_Name);
                    }
                    else if (RBtnType.SelectedIndex == 1)
                    {
                        DTab_IssueJanged = ObjReportParams.Print_Assort_Final_Print2_ALL(ReportParams_Property, "RPT_MFG_TRN_Assortment_Final_Print_2_ALL");
                    }
                }
            }
            else if (Val.ToString(ObjReportDetailProperty.Remark.ToUpper()) == "MFG_PROCESS_ASSORTMENT_OK_SUMMARY")
            {
                // DTab_IssueJanged = ObjReportParams.GetMFGAssortment_AllINOne_Data(ReportParams_Property, ObjReportDetailProperty.Procedure_Name);
                if (RBtnLocationType.EditValue.ToString() == "1")
                {
                    if (RBtnType.SelectedIndex == 0)
                    {
                        DTab_IssueJanged = ObjReportParams.GetMFGAssortment_AllINOne_Data(ReportParams_Property, ObjReportDetailProperty.Procedure_Name);
                    }
                    //else if (RBtnType.SelectedIndex == 1)
                    //{
                    //    DTab_IssueJanged = ObjReportParams.Print_Assort_Final_Print2_ALL(ReportParams_Property, "RPT_MFG_TRN_Assortment_Final_Print_2_ALL");
                    //}
                }
                else if (RBtnLocationType.EditValue.ToString() == "2")
                {
                    DTab_IssueJanged = ObjReportParams.Print_Assort_Final_Mumbai(ReportParams_Property, "RPT_MFG_TRN_Assortment_Final_Mumbai_All");
                }
            }
            else if (Val.ToString(ObjReportDetailProperty.Remark.ToUpper()) == "MFG_PROCESS_SEMI2_SUMMARY")
            {
                DSetSemi2 = ObjReportParams.Print_Semi_2(ReportParams_Property, ObjReportDetailProperty.Procedure_Name);

                DTab_Semi1 = ObjReportParams.Print_Semi_1_Sub(ReportParams_Property, "RPT_MFG_TRN_Assortment_Semi_1_Sub_ALL");
                DTab_Semi1.TableName = "Semi1";
            }
            else if (Val.ToString(ObjReportDetailProperty.Remark.ToUpper()) == "MFG_PROCESS_SEMI1_SUMMARY")
            {
                DTab_IssueJangedSemi1 = ObjReportParams.Print_Semi_1(ReportParams_Property, ObjReportDetailProperty.Procedure_Name);
            }
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (Val.ToString(ObjReportDetailProperty.Remark.ToUpper()) == "MFG_PROCESS_ASSORTMENT_SUMMARY")
            {
                PanelLoading.Visible = false;

                FrmReportViewer FrmReportViewer = new FrmReportViewer();
                foreach (DataTable DTab in DTab_IssueJanged.Tables)
                    FrmReportViewer.DS.Tables.Add(DTab.Copy());
                FrmReportViewer.GroupBy = "";
                FrmReportViewer.RepName = "";
                FrmReportViewer.RepPara = "";
                this.Cursor = Cursors.Default;
                FrmReportViewer.AllowSetFormula = true;

                if (RBtnLocationType.EditValue.ToString() == "1")
                {
                    if (RBtnType.SelectedIndex == 0)
                    {
                        //FrmReportViewer.ShowForm_SubReport("CrtPolishGrading_Final_Main", 120, FrmReportViewer.ReportFolder.ACCOUNT);
                        FrmReportViewer.ShowForm_SubReport("CrtPolishGrading_Final_Main_New", 120, FrmReportViewer.ReportFolder.ACCOUNT);
                    }
                    else if (RBtnType.SelectedIndex == 1)
                    {
                        FrmReportViewer.ShowForm_SubReport("CrtPolishGrading_Final_Main_Print2", 120, FrmReportViewer.ReportFolder.ACCOUNT);
                    }
                    else if (RBtnType.SelectedIndex == 2)
                    {
                        FrmReportViewer.ShowForm_SubReport("CrtPolishGrading_Final_Main_New", 120, FrmReportViewer.ReportFolder.ACCOUNT);
                    }
                }
                else if (RBtnLocationType.EditValue.ToString() == "2")
                {
                    //FrmReportViewer.ShowForm_SubReport("CrtPolishGrading_Final_Main_Mumbai_New", 120, FrmReportViewer.ReportFolder.ACCOUNT);
                    FrmReportViewer.ShowForm_SubReport("CrtPolishGrading_Final_Main_Mumbai_New", 120, FrmReportViewer.ReportFolder.FINAL_MAIN_ALL_MUMBAI);
                }

                DTab_IssueJanged = null;
                FrmReportViewer.DS.Tables.Clear();
                FrmReportViewer.DS.Clear();
                FrmReportViewer = null;
            }
            else if (Val.ToString(ObjReportDetailProperty.Remark.ToUpper()) == "MFG_PROCESS_ASSORTMENT_CUT_COMPARISION")
            {
                PanelLoading.Visible = false;

                FrmReportViewer FrmReportViewer = new FrmReportViewer();
                foreach (DataTable DTab in DTab_IssueJanged.Tables)
                    FrmReportViewer.DS.Tables.Add(DTab.Copy());
                FrmReportViewer.GroupBy = "";
                FrmReportViewer.RepName = "";
                FrmReportViewer.RepPara = "";
                this.Cursor = Cursors.Default;
                FrmReportViewer.AllowSetFormula = true;

                if (RBtnLocationType.EditValue.ToString() == "1")
                {
                    if (RBtnType.SelectedIndex == 0)
                    {
                        FrmReportViewer.ShowForm_SubReport("CrtPolishGrading_Final_Main", 120, FrmReportViewer.ReportFolder.ACCOUNT);
                    }
                    else
                    {
                        FrmReportViewer.ShowForm_SubReport("CrtPolishGrading_Final_Main_Print2", 120, FrmReportViewer.ReportFolder.ACCOUNT);
                    }
                }

                DTab_IssueJanged = null;
                FrmReportViewer.DS.Tables.Clear();
                FrmReportViewer.DS.Clear();
                FrmReportViewer = null;
            }
            else if (Val.ToString(ObjReportDetailProperty.Remark.ToUpper()) == "MFG_PROCESS_ASSORTMENT_OK_SUMMARY")
            {
                PanelLoading.Visible = false;

                FrmReportViewer FrmReportViewer = new FrmReportViewer();
                foreach (DataTable DTab in DTab_IssueJanged.Tables)
                    FrmReportViewer.DS.Tables.Add(DTab.Copy());
                FrmReportViewer.GroupBy = "";
                FrmReportViewer.RepName = "";
                FrmReportViewer.RepPara = "";
                this.Cursor = Cursors.Default;
                FrmReportViewer.AllowSetFormula = true;

                if (RBtnLocationType.EditValue.ToString() == "1")
                {
                    if (RBtnType.SelectedIndex == 0)
                    {
                        FrmReportViewer.ShowForm_SubReport("CrtPolishGrading_Final_OK_Main", 120, FrmReportViewer.ReportFolder.ACCOUNT);
                    }
                    else
                    {
                        FrmReportViewer.ShowForm_SubReport("CrtPolishGrading_Final_Main_Print2", 120, FrmReportViewer.ReportFolder.ACCOUNT);
                    }
                }
                else if (RBtnLocationType.EditValue.ToString() == "2")
                {
                    FrmReportViewer.ShowForm_SubReport("CrtPolishGrading_Final_Main_Mumbai", 120, FrmReportViewer.ReportFolder.ACCOUNT);
                }

                DTab_IssueJanged = null;
                FrmReportViewer.DS.Tables.Clear();
                FrmReportViewer.DS.Clear();
                FrmReportViewer = null;
            }
            else if (Val.ToString(ObjReportDetailProperty.Remark.ToUpper()) == "MFG_PROCESS_SEMI2_SUMMARY")
            {
                PanelLoading.Visible = false;

                FrmReportViewer FrmReportViewer = new FrmReportViewer();
                FrmReportViewer.DS.Tables.Add(DTab_Semi1);

                foreach (DataTable DTab in DSetSemi2.Tables)
                    FrmReportViewer.DS.Tables.Add(DTab.Copy());

                FrmReportViewer.GroupBy = "";
                FrmReportViewer.RepName = "";
                FrmReportViewer.RepPara = "";
                this.Cursor = Cursors.Default;
                FrmReportViewer.AllowSetFormula = true;

                //FrmReportViewer.ShowForm_SubReport("CrtPolishGrading_Semi2_Main", 120, FrmReportViewer.ReportFolder.ACCOUNT);

                if (RBtnLocationType.EditValue.ToString() == "1")
                {
                    //FrmReportViewer.ShowForm_SubReport("CrtPolishGrading_Semi2_Main", 120, FrmReportViewer.ReportFolder.ACCOUNT);
                    FrmReportViewer.ShowForm_SubReport("CrtPolishGrading_Semi2_Surat_Main", 120, FrmReportViewer.ReportFolder.SEMI2_SURAT);
                }
                else
                {
                    FrmReportViewer.ShowForm_SubReport("CrtPolishGrading_Semi2_Main_Mumbai", 120, FrmReportViewer.ReportFolder.ACCOUNT);
                }

                DTab_Semi1 = null;
                FrmReportViewer.DS.Tables.Clear();
                FrmReportViewer.DS.Clear();
                FrmReportViewer = null;
            }
            else if (Val.ToString(ObjReportDetailProperty.Remark.ToUpper()) == "MFG_PROCESS_SEMI1_SUMMARY")
            {
                PanelLoading.Visible = false;

                if (DTab_IssueJangedSemi1.Rows.Count == 0)
                {
                    Global.Message("Data not Found");
                    return;
                }
                else
                {
                    FrmReportViewer FrmReportViewer = new FrmReportViewer();
                    FrmReportViewer.DS.Tables.Add(DTab_IssueJangedSemi1);
                    FrmReportViewer.GroupBy = "";
                    FrmReportViewer.RepName = "";
                    FrmReportViewer.RepPara = "";
                    this.Cursor = Cursors.Default;
                    FrmReportViewer.AllowSetFormula = true;

                    //FrmReportViewer.ShowForm("CrtPolishGrading_Semi1", 120, FrmReportViewer.ReportFolder.ACCOUNT);

                    if (RBtnLocationType.EditValue.ToString() == "1")
                    {
                        FrmReportViewer.ShowForm("CrtPolishGrading_Semi1", 120, FrmReportViewer.ReportFolder.ACCOUNT);
                    }
                    else
                    {
                        FrmReportViewer.ShowForm("CrtPolishGrading_Semi1_Mumbai", 120, FrmReportViewer.ReportFolder.ACCOUNT);
                    }

                    DTab_IssueJanged = null;
                    FrmReportViewer.DS.Tables.Clear();
                    FrmReportViewer.DS.Clear();
                    FrmReportViewer = null;
                }
            }
        }
        private void RbtReportType_EditValueChanged(object sender, EventArgs e)
        {
            ObjReportDetailProperty = ObjReportMaster.GetReportDetailProperty(mIntReportCode, Val.ToString(RbtReportType.EditValue));
            mDTDetail = ObjReportMaster.GetDataForSearchSettings(mIntReportCode, Val.ToString(RbtReportType.EditValue));

            if (Val.ToString(RbtReportType.EditValue) == "FINAL")
            {
                RBtnType.Visible = true;
            }
            else if (Val.ToString(RbtReportType.EditValue) == "FINAL OK")
            {
                RBtnType.SelectedIndex = 0;
                RBtnType.Visible = false;
            }
            else
            {
                RBtnType.Visible = false;
            }

            //if(Val.ToString(RbtReportType.EditValue) == "FINAL OK")
            //{
            //    RBtnType.Visible = false;
            //}
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void BtnReset_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ListKapan.Properties.Items.Count; i++)
                ListKapan.Properties.Items[i].CheckState = CheckState.Unchecked;
            for (int i = 0; i < ListRoughCut.Properties.Items.Count; i++)
                ListRoughCut.Properties.Items[i].CheckState = CheckState.Unchecked;
            for (int i = 0; i < ListQuality.Properties.Items.Count; i++)
                ListQuality.Properties.Items[i].CheckState = CheckState.Unchecked;
            for (int i = 0; i < ListProcess.Properties.Items.Count; i++)
                ListProcess.Properties.Items[i].CheckState = CheckState.Unchecked;
            for (int i = 0; i < ListSubProcess.Properties.Items.Count; i++)
                ListSubProcess.Properties.Items[i].CheckState = CheckState.Unchecked;

            m_opDate = Global.GetDate();
            DTPFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPFromDate.Properties.CharacterCasing = CharacterCasing.Upper;

            if (m_opDate.Rows[0]["opening_date"].ToString() == "")
            {
                DTPFromDate.EditValue = DateTime.Now;
            }
            else
            {
                DTPFromDate.EditValue = Val.DBDate(m_opDate.Rows[0]["opening_date"].ToString());
            }

            DTPToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPToDate.Properties.CharacterCasing = CharacterCasing.Upper;
            DTPToDate.EditValue = DateTime.Now;
            DTPFromDate.Focus();
        }
        private void ListKapan_EditValueChanged(object sender, EventArgs e)
        {
            DataTable DTabCut = Global.GetReportKapanWise(Val.ToString(ListKapan.EditValue));
            DTabCut.DefaultView.Sort = "rough_cut_id";
            DTabCut = DTabCut.DefaultView.ToTable();

            ListRoughCut.Properties.DataSource = DTabCut;
            ListRoughCut.Properties.DisplayMember = "rough_cut_no";
            ListRoughCut.Properties.ValueMember = "rough_cut_id";

            try
            {
                DataTable DeTemp = new DataTable();
                var temp = ListKapan.Properties.GetCheckedItems().ToString().Replace(" ", "").Replace("  ", "").Trim();
                if (temp != "")
                    DeTemp = (DataTable)ListKapan.Properties.DataSource;

                if (temp.Length > 0)
                    DeTemp = DeTemp.Select("kapan_id in (" + temp + ")").CopyToDataTable();

                StrColumnName = "Kapan ";
                string StrFieldName = "kapan_no";
                TreeView_List(DeTemp, StrColumnName, StrFieldName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ListRoughCut_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable DeTemp = new DataTable();
                var temp = ListRoughCut.Properties.GetCheckedItems().ToString().Replace(" ", "").Replace("  ", "").Trim();
                if (temp != "")
                    DeTemp = (DataTable)ListRoughCut.Properties.DataSource;

                if (temp.Length > 0)
                    DeTemp = DeTemp.Select("rough_cut_id in (" + temp + ")").CopyToDataTable();

                StrColumnName = "Rough Cut ";
                string StrFieldName = "rough_cut_no";
                TreeView_List(DeTemp, StrColumnName, StrFieldName);

                if (ListRoughCut.Text.ToString() != "" && ListSubProcess.Text.ToString() != "")
                {
                    DTabTempQuality = ObjQuality.Temp_Quality_GetData(Val.Trim(ListRoughCut.Properties.GetCheckedItems()), Val.Trim(ListSubProcess.Properties.GetCheckedItems()), Val.ToInt32(RBtnLocationType.EditValue.ToString()));

                    ListQuality.Properties.DataSource = DTabTempQuality;
                    ListQuality.Properties.DisplayMember = "temp_quality_name";
                    ListQuality.Properties.ValueMember = "lot_srno";
                }
                else
                {
                    for (int i = 0; i < ListQuality.Properties.Items.Count; i++)
                        ListQuality.Properties.Items[i].CheckState = CheckState.Unchecked;
                    ListQuality.Properties.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ListSubProcess_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable DeTemp = new DataTable();
                var temp = ListSubProcess.Properties.GetCheckedItems().ToString().Replace(" ", "").Replace("  ", "").Trim();
                if (temp != "")
                    DeTemp = (DataTable)ListSubProcess.Properties.DataSource;

                if (temp.Length > 0)
                    DeTemp = DeTemp.Select("sub_process_id in (" + temp + ")").CopyToDataTable();

                StrColumnName = "SubProcess ";
                string StrFieldName = "sub_process_name";
                TreeView_List(DeTemp, StrColumnName, StrFieldName);

                if (ListRoughCut.Text.ToString() != "" && ListSubProcess.Text.ToString() != "")
                {
                    DTabTempQuality = ObjQuality.Temp_Quality_GetData(Val.Trim(ListRoughCut.Properties.GetCheckedItems()), Val.Trim(ListSubProcess.Properties.GetCheckedItems()), Val.ToInt32(RBtnLocationType.EditValue.ToString()));

                    ListQuality.Properties.DataSource = DTabTempQuality;
                    ListQuality.Properties.DisplayMember = "temp_quality_name";
                    ListQuality.Properties.ValueMember = "lot_srno";
                }
                else
                {
                    for (int i = 0; i < ListQuality.Properties.Items.Count; i++)
                        ListQuality.Properties.Items[i].CheckState = CheckState.Unchecked;
                    ListQuality.Properties.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ListProcess_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable DTabSubProcess = Global.GetReportProcessWise(Val.ToString(ListProcess.EditValue));
                DTabSubProcess.DefaultView.Sort = "sub_process_id";
                DTabSubProcess = DTabSubProcess.DefaultView.ToTable();

                ListSubProcess.Properties.DataSource = DTabSubProcess;
                ListSubProcess.Properties.DisplayMember = "sub_process_name";
                ListSubProcess.Properties.ValueMember = "sub_process_id";

                DataTable DeTemp = new DataTable();
                var temp = ListProcess.Properties.GetCheckedItems().ToString().Replace(" ", "").Replace("  ", "").Trim();
                if (temp != "")
                    DeTemp = (DataTable)ListProcess.Properties.DataSource;

                if (temp.Length > 0)
                    DeTemp = DeTemp.Select("process_id in (" + temp + ")").CopyToDataTable();

                StrColumnName = "Process ";
                string StrFieldName = "process_name";
                TreeView_List(DeTemp, StrColumnName, StrFieldName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void RBtnLocationType_EditValueChanged(object sender, EventArgs e)
        {
            if (ListRoughCut.Text.ToString() != "" && ListSubProcess.Text.ToString() != "")
            {
                DTabTempQuality = ObjQuality.Temp_Quality_GetData(Val.Trim(ListRoughCut.Properties.GetCheckedItems()), Val.Trim(ListSubProcess.Properties.GetCheckedItems()), Val.ToInt32(RBtnLocationType.EditValue.ToString()));

                ListQuality.Properties.DataSource = DTabTempQuality;
                ListQuality.Properties.DisplayMember = "temp_quality_name";
                ListQuality.Properties.ValueMember = "lot_srno";
            }
            else
            {
                for (int i = 0; i < ListQuality.Properties.Items.Count; i++)
                    ListQuality.Properties.Items[i].CheckState = CheckState.Unchecked;
                ListQuality.Properties.DataSource = null;
            }
        }

        #endregion

        #region "Functions"
        private void FillListControls()
        {
            DataTable DTabKapan = ObjFillCombo.FillCmb(FillCombo.TABLE.Kapan_Master);
            DTabKapan.DefaultView.Sort = "kapan_no";
            DTabKapan = DTabKapan.DefaultView.ToTable();

            ListKapan.Properties.DataSource = DTabKapan;
            ListKapan.Properties.DisplayMember = "kapan_no";
            ListKapan.Properties.ValueMember = "kapan_id";

            DataTable DTabCut = ObjFillCombo.FillCmb(FillCombo.TABLE.Cut_Master);
            DTabCut.DefaultView.Sort = "rough_cut_no";
            DTabCut = DTabCut.DefaultView.ToTable();

            ListRoughCut.Properties.DataSource = DTabCut;
            ListRoughCut.Properties.DisplayMember = "rough_cut_no";
            ListRoughCut.Properties.ValueMember = "rough_cut_id";

            DataTable DTabProcess = ObjFillCombo.FillCmb(FillCombo.TABLE.Process_Master_New);
            DTabProcess.DefaultView.Sort = "process_name";
            DTabProcess = DTabProcess.DefaultView.ToTable();

            ListProcess.Properties.DataSource = DTabProcess;
            ListProcess.Properties.DisplayMember = "process_name";
            ListProcess.Properties.ValueMember = "process_id";

            ListProcess.SetEditValue(3012);

            DataTable DTabSubProcess = ObjFillCombo.FillCmb(FillCombo.TABLE.Sub_Process_Master);
            DTabSubProcess.DefaultView.Sort = "sub_process_name";
            DTabSubProcess = DTabSubProcess.DefaultView.ToTable();

            ListSubProcess.Properties.DataSource = DTabSubProcess;
            ListSubProcess.Properties.DisplayMember = "sub_process_name";
            ListSubProcess.Properties.ValueMember = "sub_process_id";

            DataTable DTabFromCut = ObjFillCombo.FillCmb(FillCombo.TABLE.Cut_Master);
            DTabFromCut.DefaultView.Sort = "rough_cut_id";
            DTabFromCut = DTabFromCut.DefaultView.ToTable();

            lueFromCutNo.Properties.DataSource = DTabFromCut;
            lueFromCutNo.Properties.DisplayMember = "rough_cut_no";
            lueFromCutNo.Properties.ValueMember = "rough_cut_id";

            DataTable DTabToCut = ObjFillCombo.FillCmb(FillCombo.TABLE.Cut_Master);
            DTabToCut.DefaultView.Sort = "rough_cut_id";
            DTabToCut = DTabToCut.DefaultView.ToTable();

            lueToCutNo.Properties.DataSource = DTabToCut;
            lueToCutNo.Properties.DisplayMember = "rough_cut_no";
            lueToCutNo.Properties.ValueMember = "rough_cut_id";

            ListProcess_EditValueChanged(null, null);

            if (GlobalDec.gEmployeeProperty.user_name == "VIRAJ")
            {
                panelControl6.Visible = false;
            }
        }
        private string GetFilterByValue()
        {
            string Str = "Filter By : ";

            if (DTPFromDate.Text != "")
            {
                Str += "From Issue Date : " + DTPFromDate.Text;
            }
            if (DTPToDate.Text != "")
            {
                Str += " & As On Date : " + DTPToDate.Text;
            }
            if (ListKapan.Text.Length > 0)
            {
                Str += ", Kapan : " + ListKapan.Text.ToString();
            }
            if (ListRoughCut.Text.Length > 0)
            {
                Str += ", Cut No : " + ListRoughCut.Text.ToString();
            }
            return Str;
        }
        private void TreeView_List(DataTable DeTemp, string StrColumnName, string StrFieldName)
        {
            try
            {
                for (int i = dtAllvalues.Rows.Count - 1; i >= 0; i--)
                {

                    if (dtAllvalues.Rows[i][dc_Parent].ToString() == StrColumnName)
                    {
                        dtAllvalues.Rows.Remove(dtAllvalues.Rows[i]);
                    }
                }

                if (dtAllvalues.Rows.Count == 0)
                {
                    DataRow r = dtAllvalues.NewRow();
                    r[dc_Unique] = StrColumnName;
                    r[dc_Parent] = "";
                    r[dc_Data] = StrColumnName;
                    dtAllvalues.Rows.Add(r);
                }
                else
                {
                    int chk = 0;
                    for (int i = 0; i < dtAllvalues.Rows.Count; i++)
                    {
                        if (dtAllvalues.Rows[i][dc_Unique].ToString() == StrColumnName)
                        {
                            chk = 1;
                        }
                    }
                    if (chk == 0)
                    {
                        DataRow r = dtAllvalues.NewRow();
                        r[dc_Unique] = StrColumnName;
                        r[dc_Parent] = "";
                        r[dc_Data] = StrColumnName;
                        dtAllvalues.Rows.Add(r);
                    }
                }

                for (int j = 0; j < DeTemp.Rows.Count; j++)
                {

                    int chk = 0;
                    for (int i = 0; i < dtAllvalues.Rows.Count; i++)
                    {
                        if (dtAllvalues.Rows[i][dc_Unique].ToString() == DeTemp.Rows[j][StrFieldName].ToString())
                        {
                            chk = 1;
                        }
                    }
                    if (chk == 0)
                    {
                        DataRow r = dtAllvalues.NewRow();
                        r[dc_Unique] = DeTemp.Rows[j][StrFieldName].ToString();
                        r[dc_Parent] = StrColumnName;
                        r[dc_Data] = DeTemp.Rows[j][StrFieldName].ToString();
                        dtAllvalues.Rows.Add(r);
                    }
                }
                treeView_Condition.ParentFieldName = "Parent";
                treeView_Condition.KeyFieldName = "Unique";
                treeView_Condition.RootValue = string.Empty;
                treeView_Condition.DataSource = dtAllvalues;
                treeView_Condition.ExpandAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void FillControls()
        {
            DataTable DTab = ObjReportMaster.GetDataForSearchDetailReport(mIntReportCode);

            foreach (DataRow DRow in DTab.Rows)
            {
                //if (Val.ToInt(DRow["is_pivot"].ToString()) == 1)
                //{
                //    mIntPivot = 1;
                //}
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

        private void BtnEraser_Click(object sender, EventArgs e)
        {
            lueFromCutNo.EditValue = null;
            lueToCutNo.EditValue = null;
        }

        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (lueFromCutNo.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "From Cut No"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueFromCutNo.Focus();
                    }
                }
                if (lueToCutNo.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "To Cut No"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueToCutNo.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                lstError.Add(new ListError(ex));
            }
            return (!(BLL.General.ShowErrors(lstError)));
        }

        private void BtnCutGenerateReport_Click(object sender, EventArgs e)
        {
            if (!ValidateDetails())
            {
                return;
            }

            PanelLoading.Visible = true;
            ReportParams_Property.from_rough_cut_id = Val.ToInt64(lueFromCutNo.EditValue);
            ReportParams_Property.to_rough_cut_id = Val.ToInt64(lueToCutNo.EditValue);
            ReportParams_Property.rate_date = Val.DBDate(dtpRateDate.Text);
            ReportParams_Property.from_rough_cut_no = Val.ToString(lueFromCutNo.Text);
            ReportParams_Property.to_rough_cut_no = Val.ToString(lueToCutNo.Text);
            ReportParams_Property.From_Date = Val.DBDate(DTPFromDate.Text);

            if (this.backgroundWorker2.IsBusy)
            {
            }
            else
            {
                backgroundWorker2.RunWorkerAsync();
            }
        }

        private void backgroundWorker2_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (RBtnCutComparision.SelectedIndex == 0)
            {
                dtList = ObjReportParams.GetMFGAssortment_Cut_Comparision_Data(ReportParams_Property, "RPT_MFG_TRN_Assortment_Cut_Commparission");

                int cnt1 = 0;
                decimal Totalfrom_MinusFisrt_Color = 0;
                decimal Totalfrom_PlusFisrt_Color = 0;
                decimal TotalTo_MinusFisrt_Color = 0;
                decimal TotalTo_PlusFisrt_Color = 0;
                decimal Totalfrom_MinusSecond_Color = 0;
                decimal Totalfrom_PlusSecond_Color = 0;
                decimal TotalTo_MinusSecond_Color = 0;
                decimal TotalTo_PlusSecond_Color = 0;
                decimal Totalfrom_MinusThird_Color = 0;
                decimal Totalfrom_PlusThird_Color = 0;
                decimal TotalTo_MinusThird_Color = 0;
                decimal TotalTo_PlusThird_Color = 0;
                decimal Totalfrom_MinusFourth_Color = 0;
                decimal Totalfrom_PlusFourth_Color = 0;
                decimal TotalTo_MinusFourth_Color = 0;
                decimal TotalTo_PlusFourth_Color = 0;
                decimal Totalfrom_MinusFifth_Color = 0;
                decimal Totalfrom_PlusFifth_Color = 0;
                decimal TotalTo_MinusFifth_Color = 0;
                decimal TotalTo_PlusFifth_Color = 0;
                decimal Totalfrom_MinusSix_Color = 0;
                decimal Totalfrom_PlusSix_Color = 0;
                decimal TotalTo_MinusSix_Color = 0;
                decimal TotalTo_PlusSix_Color = 0;
                decimal Totalfrom_MinusSeven_Color = 0;
                decimal Totalfrom_PlusSeven_Color = 0;
                decimal TotalTo_MinusSeven_Color = 0;
                decimal TotalTo_PlusSeven_Color = 0;
                decimal Totalfrom_MinusEight_Color = 0;
                decimal Totalfrom_PlusEight_Color = 0;
                decimal TotalTo_MinusEight_Color = 0;
                decimal TotalTo_PlusEight_Color = 0;

                decimal Totalfrom_MinusFisrt_ColorWise = 0;
                decimal Totalfrom_PlusFisrt_ColorWise = 0;
                decimal TotalTo_MinusFisrt_ColorWise = 0;
                decimal TotalTo_PlusFisrt_ColorWise = 0;
                decimal Totalfrom_MinusSecond_ColorWise = 0;
                decimal Totalfrom_PlusSecond_ColorWise = 0;
                decimal TotalTo_MinusSecond_ColorWise = 0;
                decimal TotalTo_PlusSecond_ColorWise = 0;
                decimal Totalfrom_MinusThird_ColorWise = 0;
                decimal Totalfrom_PlusThird_ColorWise = 0;
                decimal TotalTo_MinusThird_ColorWise = 0;
                decimal TotalTo_PlusThird_ColorWise = 0;
                decimal Totalfrom_MinusFourth_ColorWise = 0;
                decimal Totalfrom_PlusFourth_ColorWise = 0;
                decimal TotalTo_MinusFourth_ColorWise = 0;
                decimal TotalTo_PlusFourth_ColorWise = 0;

                decimal Totalfrom_MinusFisrt_PurityWise = 0;
                decimal Totalfrom_PlusFisrt_PurityWise = 0;
                decimal TotalTo_MinusFisrt_PurityWise = 0;
                decimal TotalTo_PlusFisrt_PurityWise = 0;
                decimal Totalfrom_MinusSecond_PurityWise = 0;
                decimal Totalfrom_PlusSecond_PurityWise = 0;
                decimal TotalTo_MinusSecond_PurityWise = 0;
                decimal TotalTo_PlusSecond_PurityWise = 0;
                decimal Totalfrom_MinusThird_PurityWise = 0;
                decimal Totalfrom_PlusThird_PurityWise = 0;
                decimal TotalTo_MinusThird_PurityWise = 0;
                decimal TotalTo_PlusThird_PurityWise = 0;
                decimal Totalfrom_MinusFourth_PurityWise = 0;
                decimal Totalfrom_PlusFourth_PurityWise = 0;
                decimal TotalTo_MinusFourth_PurityWise = 0;
                decimal TotalTo_PlusFourth_PurityWise = 0;
                decimal Totalfrom_MinusFifth_PurityWise = 0;
                decimal Totalfrom_PlusFifth_PurityWise = 0;
                decimal TotalTo_MinusFifth_PurityWise = 0;
                decimal TotalTo_PlusFifth_PurityWise = 0;

                #region Diffrence
                foreach (DataRow Drw in dtList.Tables[0].Rows)
                {
                    cnt1++;

                    int cnt = 1;

                    foreach (DataRow Drw1 in dtList.Tables[1].Rows)
                    {
                        if (cnt1 == cnt)
                        {
                            Drw1["minus_two_per_diff"] = Val.ToDecimal(Drw["minus_two_per"]) - Val.ToDecimal(Drw1["minus_two_per"]);
                            Drw1["pluse_two_per_diff"] = Val.ToDecimal(Drw["pluse_two_per"]) - Val.ToDecimal(Drw1["pluse_two_per"]);
                        }
                        cnt++;
                    }
                }

                int cnt2 = 0;

                foreach (DataRow Drw in dtList.Tables[2].Rows)
                {
                    cnt2++;

                    int cnt = 1;

                    foreach (DataRow Drw1 in dtList.Tables[3].Rows)
                    {
                        if (cnt2 == cnt)
                        {
                            Drw1["minus_two_per_diff"] = Val.ToDecimal(Drw["minus_two_per"]) - Val.ToDecimal(Drw1["minus_two_per"]);
                            Drw1["pluse_two_per_diff"] = Val.ToDecimal(Drw["pluse_two_per"]) - Val.ToDecimal(Drw1["pluse_two_per"]);
                        }
                        cnt++;
                    }
                }

                int cnt3 = 0;

                foreach (DataRow Drw in dtList.Tables[4].Rows)
                {
                    cnt3++;

                    int cnt = 1;

                    foreach (DataRow Drw1 in dtList.Tables[5].Rows)
                    {
                        if (cnt3 == cnt)
                        {
                            Drw1["minus_two_per_diff"] = Val.ToDecimal(Drw["minus_two_per"]) - Val.ToDecimal(Drw1["minus_two_per"]);
                            Drw1["pluse_two_per_diff"] = Val.ToDecimal(Drw["pluse_two_per"]) - Val.ToDecimal(Drw1["pluse_two_per"]);
                        }
                        cnt++;
                    }
                }

                #endregion Diffrence

                #region Purity Wise Diffrence

                foreach (DataRow Drw in dtList.Tables[0].Rows)
                {
                    ///////////////////////////////////////// First Purity //////////////////////////////////////////

                    DataTable DTab_From_first = dtList.Tables[0].Select("purity_name in('VVS')").CopyToDataTable();
                    foreach (DataRow dRow in DTab_From_first.Rows)
                    {
                        Totalfrom_MinusFisrt_PurityWise = Totalfrom_MinusFisrt_PurityWise + Convert.ToDecimal(dRow["-2WT"]);
                        Totalfrom_PlusFisrt_PurityWise = Totalfrom_PlusFisrt_PurityWise + Convert.ToDecimal(dRow["+2WT"]);
                    }
                    Totalfrom_MinusFisrt_PurityWise = Math.Round((Totalfrom_MinusFisrt_PurityWise / Val.ToDecimal(dtList.Tables[0].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                    Totalfrom_PlusFisrt_PurityWise = Math.Round((Totalfrom_PlusFisrt_PurityWise / Val.ToDecimal(dtList.Tables[0].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                    DataTable DTab_To_First = dtList.Tables[1].Select("purity_name in('VVS')").CopyToDataTable();
                    foreach (DataRow dRow in DTab_To_First.Rows)
                    {
                        TotalTo_MinusFisrt_PurityWise = TotalTo_MinusFisrt_PurityWise + Convert.ToDecimal(dRow["-2WT"]);
                        TotalTo_PlusFisrt_PurityWise = TotalTo_PlusFisrt_PurityWise + Convert.ToDecimal(dRow["+2WT"]);
                    }
                    TotalTo_MinusFisrt_PurityWise = Math.Round((TotalTo_MinusFisrt_PurityWise / Val.ToDecimal(dtList.Tables[1].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                    TotalTo_PlusFisrt_PurityWise = Math.Round((TotalTo_PlusFisrt_PurityWise / Val.ToDecimal(dtList.Tables[1].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                    ///////////////////////////////////////// End First Purity //////////////////////////////////////////

                    ///////////////////////////////////////// Second Purity //////////////////////////////////////////

                    DataTable DTab_From_Second = dtList.Tables[0].Select("purity_name in('VS')").CopyToDataTable();
                    foreach (DataRow dRow in DTab_From_Second.Rows)
                    {
                        Totalfrom_MinusSecond_PurityWise = Totalfrom_MinusSecond_PurityWise + Convert.ToDecimal(dRow["-2WT"]);
                        Totalfrom_PlusSecond_PurityWise = Totalfrom_PlusSecond_PurityWise + Convert.ToDecimal(dRow["+2WT"]);
                    }
                    Totalfrom_MinusSecond_PurityWise = Math.Round((Totalfrom_MinusSecond_PurityWise / Val.ToDecimal(dtList.Tables[0].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                    Totalfrom_PlusSecond_PurityWise = Math.Round((Totalfrom_PlusSecond_PurityWise / Val.ToDecimal(dtList.Tables[0].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                    DataTable DTab_To_Second = dtList.Tables[1].Select("purity_name in('VS')").CopyToDataTable();
                    foreach (DataRow dRow in DTab_To_Second.Rows)
                    {
                        TotalTo_MinusSecond_PurityWise = TotalTo_MinusSecond_PurityWise + Convert.ToDecimal(dRow["-2WT"]);
                        TotalTo_PlusSecond_PurityWise = TotalTo_PlusSecond_PurityWise + Convert.ToDecimal(dRow["+2WT"]);
                    }
                    TotalTo_MinusSecond_PurityWise = Math.Round((TotalTo_MinusSecond_PurityWise / Val.ToDecimal(dtList.Tables[1].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                    TotalTo_PlusSecond_PurityWise = Math.Round((TotalTo_PlusSecond_PurityWise / Val.ToDecimal(dtList.Tables[1].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                    ///////////////////////////////////////// End Second Purity //////////////////////////////////////////

                    ///////////////////////////////////////// Third Purity //////////////////////////////////////////

                    DataTable DTab_From_Third = dtList.Tables[0].Select("purity_name in('SI')").CopyToDataTable();
                    foreach (DataRow dRow in DTab_From_Third.Rows)
                    {
                        Totalfrom_MinusThird_PurityWise = Totalfrom_MinusThird_PurityWise + Convert.ToDecimal(dRow["-2WT"]);
                        Totalfrom_PlusThird_PurityWise = Totalfrom_PlusThird_PurityWise + Convert.ToDecimal(dRow["+2WT"]);
                    }
                    Totalfrom_MinusThird_PurityWise = Math.Round((Totalfrom_MinusThird_PurityWise / Val.ToDecimal(dtList.Tables[0].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                    Totalfrom_PlusThird_PurityWise = Math.Round((Totalfrom_PlusThird_PurityWise / Val.ToDecimal(dtList.Tables[0].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                    DataTable DTab_To_Third = dtList.Tables[1].Select("purity_name in('SI')").CopyToDataTable();
                    foreach (DataRow dRow in DTab_To_Third.Rows)
                    {
                        TotalTo_MinusThird_PurityWise = TotalTo_MinusThird_PurityWise + Convert.ToDecimal(dRow["-2WT"]);
                        TotalTo_PlusThird_PurityWise = TotalTo_PlusThird_PurityWise + Convert.ToDecimal(dRow["+2WT"]);
                    }
                    TotalTo_MinusThird_PurityWise = Math.Round((TotalTo_MinusThird_PurityWise / Val.ToDecimal(dtList.Tables[1].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                    TotalTo_PlusThird_PurityWise = Math.Round((TotalTo_PlusThird_PurityWise / Val.ToDecimal(dtList.Tables[1].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                    ///////////////////////////////////////// End Third Purity //////////////////////////////////////////

                    ///////////////////////////////////////// Fourth Purity //////////////////////////////////////////

                    DataTable DTab_From_Fourth = dtList.Tables[0].Select("purity_name in('I')").CopyToDataTable();
                    foreach (DataRow dRow in DTab_From_Fourth.Rows)
                    {
                        Totalfrom_MinusFourth_PurityWise = Totalfrom_MinusFourth_PurityWise + Convert.ToDecimal(dRow["-2WT"]);
                        Totalfrom_PlusFourth_PurityWise = Totalfrom_PlusFourth_PurityWise + Convert.ToDecimal(dRow["+2WT"]);
                    }
                    Totalfrom_MinusFourth_PurityWise = Math.Round((Totalfrom_MinusFourth_PurityWise / Val.ToDecimal(dtList.Tables[0].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                    Totalfrom_PlusFourth_PurityWise = Math.Round((Totalfrom_PlusFourth_PurityWise / Val.ToDecimal(dtList.Tables[0].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                    DataTable DTab_To_Fourth = dtList.Tables[1].Select("purity_name in('I')").CopyToDataTable();
                    foreach (DataRow dRow in DTab_To_Fourth.Rows)
                    {
                        TotalTo_MinusFourth_PurityWise = TotalTo_MinusFourth_PurityWise + Convert.ToDecimal(dRow["-2WT"]);
                        TotalTo_PlusFourth_PurityWise = TotalTo_PlusFourth_PurityWise + Convert.ToDecimal(dRow["+2WT"]);
                    }
                    TotalTo_MinusFourth_PurityWise = Math.Round((TotalTo_MinusFourth_PurityWise / Val.ToDecimal(dtList.Tables[1].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                    TotalTo_PlusFourth_PurityWise = Math.Round((TotalTo_PlusFourth_PurityWise / Val.ToDecimal(dtList.Tables[1].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                    ///////////////////////////////////////// End Fourth Purity //////////////////////////////////////////

                    ///////////////////////////////////////// Fifth Purity //////////////////////////////////////////

                    DataTable DTab_From_Fifth = dtList.Tables[0].Select("purity_name in('PK')").CopyToDataTable();
                    foreach (DataRow dRow in DTab_From_Fifth.Rows)
                    {
                        Totalfrom_MinusFifth_PurityWise = Totalfrom_MinusFifth_PurityWise + Convert.ToDecimal(dRow["-2WT"]);
                        Totalfrom_PlusFifth_PurityWise = Totalfrom_PlusFifth_PurityWise + Convert.ToDecimal(dRow["+2WT"]);
                    }
                    Totalfrom_MinusFifth_PurityWise = Math.Round((Totalfrom_MinusFifth_PurityWise / Val.ToDecimal(dtList.Tables[0].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                    Totalfrom_PlusFifth_PurityWise = Math.Round((Totalfrom_PlusFifth_PurityWise / Val.ToDecimal(dtList.Tables[0].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                    DataTable DTab_To_Fifth = dtList.Tables[1].Select("purity_name in('PK')").CopyToDataTable();
                    foreach (DataRow dRow in DTab_To_Fifth.Rows)
                    {
                        TotalTo_MinusFifth_PurityWise = TotalTo_MinusFifth_PurityWise + Convert.ToDecimal(dRow["-2WT"]);
                        TotalTo_PlusFifth_PurityWise = TotalTo_PlusFifth_PurityWise + Convert.ToDecimal(dRow["+2WT"]);
                    }
                    TotalTo_MinusFifth_PurityWise = Math.Round((TotalTo_MinusFifth_PurityWise / Val.ToDecimal(dtList.Tables[1].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                    TotalTo_PlusFifth_PurityWise = Math.Round((TotalTo_PlusFifth_PurityWise / Val.ToDecimal(dtList.Tables[1].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                    ///////////////////////////////////////// End Fifth Purity //////////////////////////////////////////

                    TotalTo_MinusFisrt_PurityWise = Totalfrom_MinusFisrt_PurityWise - TotalTo_MinusFisrt_PurityWise;
                    TotalTo_PlusFisrt_PurityWise = Totalfrom_PlusFisrt_PurityWise - TotalTo_PlusFisrt_PurityWise;
                    TotalTo_MinusSecond_PurityWise = Totalfrom_MinusSecond_PurityWise - TotalTo_MinusSecond_PurityWise;
                    TotalTo_PlusSecond_PurityWise = Totalfrom_PlusSecond_PurityWise - TotalTo_PlusSecond_PurityWise;
                    TotalTo_MinusThird_PurityWise = Totalfrom_MinusThird_PurityWise - TotalTo_MinusThird_PurityWise;
                    TotalTo_PlusThird_PurityWise = Totalfrom_PlusThird_PurityWise - TotalTo_PlusThird_PurityWise;
                    TotalTo_MinusFourth_PurityWise = Totalfrom_MinusFourth_PurityWise - TotalTo_MinusFourth_PurityWise;
                    TotalTo_PlusFourth_PurityWise = Totalfrom_PlusFourth_PurityWise - TotalTo_PlusFourth_PurityWise;
                    TotalTo_MinusFifth_PurityWise = Totalfrom_MinusFifth_PurityWise - TotalTo_MinusFifth_PurityWise;
                    TotalTo_PlusFifth_PurityWise = Totalfrom_PlusFifth_PurityWise - TotalTo_PlusFifth_PurityWise;

                    for (int i = 0; i < dtList.Tables[1].Rows.Count; i++)
                    {
                        if (dtList.Tables[1].Rows[i]["purity_name"].ToString() == "VVS")
                        {
                            dtList.Tables[1].Rows[i]["first_to_minusdiff"] = TotalTo_MinusFisrt_PurityWise;
                            dtList.Tables[1].Rows[i]["first_to_plusdiff"] = TotalTo_PlusFisrt_PurityWise;
                        }
                        if (dtList.Tables[1].Rows[i]["purity_name"].ToString() == "VS")
                        {
                            dtList.Tables[1].Rows[i]["first_to_minusdiff"] = TotalTo_MinusSecond_PurityWise;
                            dtList.Tables[1].Rows[i]["first_to_plusdiff"] = TotalTo_PlusSecond_PurityWise;
                        }
                        if (dtList.Tables[1].Rows[i]["purity_name"].ToString() == "SI")
                        {
                            dtList.Tables[1].Rows[i]["first_to_minusdiff"] = TotalTo_MinusThird_PurityWise;
                            dtList.Tables[1].Rows[i]["first_to_plusdiff"] = TotalTo_PlusThird_PurityWise;
                        }
                        if (dtList.Tables[1].Rows[i]["purity_name"].ToString() == "I")
                        {
                            dtList.Tables[1].Rows[i]["first_to_minusdiff"] = TotalTo_MinusFourth_PurityWise;
                            dtList.Tables[1].Rows[i]["first_to_plusdiff"] = TotalTo_PlusFourth_PurityWise;
                        }
                        if (dtList.Tables[1].Rows[i]["purity_name"].ToString() == "Pk")
                        {
                            dtList.Tables[1].Rows[i]["first_to_minusdiff"] = TotalTo_MinusFifth_PurityWise;
                            dtList.Tables[1].Rows[i]["first_to_plusdiff"] = TotalTo_PlusFifth_PurityWise;
                        }
                    }
                    break;
                }

                #endregion Purity Wise Diffrence

                #region Color Wise Diffrence

                foreach (DataRow Drw in dtList.Tables[2].Rows)
                {
                    ///////////////////////////////////////// First Color //////////////////////////////////////////

                    DataTable DTab_From_first = dtList.Tables[2].Select("color_name in('FW','WT')").CopyToDataTable();
                    foreach (DataRow dRow in DTab_From_first.Rows)
                    {
                        Totalfrom_MinusFisrt_ColorWise = Totalfrom_MinusFisrt_ColorWise + Convert.ToDecimal(dRow["-2WT"]);
                        Totalfrom_PlusFisrt_ColorWise = Totalfrom_PlusFisrt_ColorWise + Convert.ToDecimal(dRow["+2WT"]);
                    }
                    Totalfrom_MinusFisrt_ColorWise = Math.Round((Totalfrom_MinusFisrt_ColorWise / Val.ToDecimal(dtList.Tables[2].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                    Totalfrom_PlusFisrt_ColorWise = Math.Round((Totalfrom_PlusFisrt_ColorWise / Val.ToDecimal(dtList.Tables[2].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                    DataTable DTab_To_First = dtList.Tables[3].Select("color_name in('FW','WT')").CopyToDataTable();
                    foreach (DataRow dRow in DTab_To_First.Rows)
                    {
                        TotalTo_MinusFisrt_ColorWise = TotalTo_MinusFisrt_ColorWise + Convert.ToDecimal(dRow["-2WT"]);
                        TotalTo_PlusFisrt_ColorWise = TotalTo_PlusFisrt_ColorWise + Convert.ToDecimal(dRow["+2WT"]);
                    }
                    TotalTo_MinusFisrt_ColorWise = Math.Round((TotalTo_MinusFisrt_ColorWise / Val.ToDecimal(dtList.Tables[3].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                    TotalTo_PlusFisrt_ColorWise = Math.Round((TotalTo_PlusFisrt_ColorWise / Val.ToDecimal(dtList.Tables[3].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                    ///////////////////////////////////////// End First Color //////////////////////////////////////////

                    ///////////////////////////////////////// Second Color //////////////////////////////////////////

                    DataTable DTab_From_Second = dtList.Tables[2].Select("color_name in('N')").CopyToDataTable();
                    foreach (DataRow dRow in DTab_From_Second.Rows)
                    {
                        Totalfrom_MinusSecond_ColorWise = Totalfrom_MinusSecond_ColorWise + Convert.ToDecimal(dRow["-2WT"]);
                        Totalfrom_PlusSecond_ColorWise = Totalfrom_PlusSecond_ColorWise + Convert.ToDecimal(dRow["+2WT"]);
                    }
                    Totalfrom_MinusSecond_ColorWise = Math.Round((Totalfrom_MinusSecond_ColorWise / Val.ToDecimal(dtList.Tables[2].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                    Totalfrom_PlusSecond_ColorWise = Math.Round((Totalfrom_PlusSecond_ColorWise / Val.ToDecimal(dtList.Tables[2].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                    DataTable DTab_To_Second = dtList.Tables[3].Select("color_name in('N')").CopyToDataTable();
                    foreach (DataRow dRow in DTab_To_Second.Rows)
                    {
                        TotalTo_MinusSecond_ColorWise = TotalTo_MinusSecond_ColorWise + Convert.ToDecimal(dRow["-2WT"]);
                        TotalTo_PlusSecond_ColorWise = TotalTo_PlusSecond_ColorWise + Convert.ToDecimal(dRow["+2WT"]);
                    }
                    TotalTo_MinusSecond_ColorWise = Math.Round((TotalTo_MinusSecond_ColorWise / Val.ToDecimal(dtList.Tables[3].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                    TotalTo_PlusSecond_ColorWise = Math.Round((TotalTo_PlusSecond_ColorWise / Val.ToDecimal(dtList.Tables[3].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                    ///////////////////////////////////////// End Second Color //////////////////////////////////////////

                    ///////////////////////////////////////// Third Color //////////////////////////////////////////

                    DataTable DTab_From_Third = dtList.Tables[2].Select("color_name in('AirLB','TTLB','NWLB','OWLB','LB')").CopyToDataTable();
                    foreach (DataRow dRow in DTab_From_Third.Rows)
                    {
                        Totalfrom_MinusThird_ColorWise = Totalfrom_MinusThird_ColorWise + Convert.ToDecimal(dRow["-2WT"]);
                        Totalfrom_PlusThird_ColorWise = Totalfrom_PlusThird_ColorWise + Convert.ToDecimal(dRow["+2WT"]);
                    }
                    Totalfrom_MinusThird_ColorWise = Math.Round((Totalfrom_MinusThird_ColorWise / Val.ToDecimal(dtList.Tables[2].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                    Totalfrom_PlusThird_ColorWise = Math.Round((Totalfrom_PlusThird_ColorWise / Val.ToDecimal(dtList.Tables[2].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                    DataTable DTab_To_Third = dtList.Tables[3].Select("color_name in('AirLB','TTLB','NWLB','OWLB','LB')").CopyToDataTable();
                    foreach (DataRow dRow in DTab_To_Third.Rows)
                    {
                        TotalTo_MinusThird_ColorWise = TotalTo_MinusThird_ColorWise + Convert.ToDecimal(dRow["-2WT"]);
                        TotalTo_PlusThird_ColorWise = TotalTo_PlusThird_ColorWise + Convert.ToDecimal(dRow["+2WT"]);
                    }
                    TotalTo_MinusThird_ColorWise = Math.Round((TotalTo_MinusThird_ColorWise / Val.ToDecimal(dtList.Tables[3].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                    TotalTo_PlusThird_ColorWise = Math.Round((TotalTo_PlusThird_ColorWise / Val.ToDecimal(dtList.Tables[3].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                    ///////////////////////////////////////// End Third Color //////////////////////////////////////////

                    ///////////////////////////////////////// Fourth Color //////////////////////////////////////////

                    DataTable DTab_From_Fourth = dtList.Tables[2].Select("color_name in('AirLC','TTLC','NWLC','LC','PINK 1','PINK 2')").CopyToDataTable();
                    foreach (DataRow dRow in DTab_From_Fourth.Rows)
                    {
                        Totalfrom_MinusFourth_ColorWise = Totalfrom_MinusFourth_ColorWise + Convert.ToDecimal(dRow["-2WT"]);
                        Totalfrom_PlusFourth_ColorWise = Totalfrom_PlusFourth_ColorWise + Convert.ToDecimal(dRow["+2WT"]);
                    }
                    Totalfrom_MinusFourth_ColorWise = Math.Round((Totalfrom_MinusFourth_ColorWise / Val.ToDecimal(dtList.Tables[2].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                    Totalfrom_PlusFourth_ColorWise = Math.Round((Totalfrom_PlusFourth_ColorWise / Val.ToDecimal(dtList.Tables[2].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                    DataTable DTab_To_Fourth = dtList.Tables[3].Select("color_name in('AirLC','TTLC','NWLC','LC','PINK 1','PINK 2')").CopyToDataTable();
                    foreach (DataRow dRow in DTab_To_Fourth.Rows)
                    {
                        TotalTo_MinusFourth_ColorWise = TotalTo_MinusFourth_ColorWise + Convert.ToDecimal(dRow["-2WT"]);
                        TotalTo_PlusFourth_ColorWise = TotalTo_PlusFourth_ColorWise + Convert.ToDecimal(dRow["+2WT"]);
                    }
                    TotalTo_MinusFourth_ColorWise = Math.Round((TotalTo_MinusFourth_ColorWise / Val.ToDecimal(dtList.Tables[3].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                    TotalTo_PlusFourth_ColorWise = Math.Round((TotalTo_PlusFourth_ColorWise / Val.ToDecimal(dtList.Tables[3].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                    ///////////////////////////////////////// End Fourth Color //////////////////////////////////////////

                    TotalTo_MinusFisrt_ColorWise = Totalfrom_MinusFisrt_ColorWise - TotalTo_MinusFisrt_ColorWise;
                    TotalTo_PlusFisrt_ColorWise = Totalfrom_PlusFisrt_ColorWise - TotalTo_PlusFisrt_ColorWise;
                    TotalTo_MinusSecond_ColorWise = Totalfrom_MinusSecond_ColorWise - TotalTo_MinusSecond_ColorWise;
                    TotalTo_PlusSecond_ColorWise = Totalfrom_PlusSecond_ColorWise - TotalTo_PlusSecond_ColorWise;
                    TotalTo_MinusThird_ColorWise = Totalfrom_MinusThird_ColorWise - TotalTo_MinusThird_ColorWise;
                    TotalTo_PlusThird_ColorWise = Totalfrom_PlusThird_ColorWise - TotalTo_PlusThird_ColorWise;
                    TotalTo_MinusFourth_ColorWise = Totalfrom_MinusFourth_ColorWise - TotalTo_MinusFourth_ColorWise;
                    TotalTo_PlusFourth_ColorWise = Totalfrom_PlusFourth_ColorWise - TotalTo_PlusFourth_ColorWise;

                    for (int i = 0; i < dtList.Tables[3].Rows.Count; i++)
                    {
                        if (dtList.Tables[3].Rows[i]["color_name"].ToString() == "FW" || dtList.Tables[3].Rows[i]["color_name"].ToString() == "WT")
                        {
                            dtList.Tables[3].Rows[i]["first_to_minusdiff"] = TotalTo_MinusFisrt_ColorWise;
                            dtList.Tables[3].Rows[i]["first_to_plusdiff"] = TotalTo_PlusFisrt_ColorWise;
                        }
                        if (dtList.Tables[3].Rows[i]["color_name"].ToString() == "N")
                        {
                            dtList.Tables[3].Rows[i]["first_to_minusdiff"] = TotalTo_MinusSecond_ColorWise;
                            dtList.Tables[3].Rows[i]["first_to_plusdiff"] = TotalTo_PlusSecond_ColorWise;
                        }
                        if (dtList.Tables[3].Rows[i]["color_name"].ToString() == "AirLB" || dtList.Tables[3].Rows[i]["color_name"].ToString() == "TTLB" ||
                            dtList.Tables[3].Rows[i]["color_name"].ToString() == "NWLB" || dtList.Tables[3].Rows[i]["color_name"].ToString() == "OWLB" || dtList.Tables[3].Rows[i]["color_name"].ToString() == "LB")
                        {
                            dtList.Tables[3].Rows[i]["first_to_minusdiff"] = TotalTo_MinusThird_ColorWise;
                            dtList.Tables[3].Rows[i]["first_to_plusdiff"] = TotalTo_PlusThird_ColorWise;
                        }
                        if (dtList.Tables[3].Rows[i]["color_name"].ToString() == "AirLC" || dtList.Tables[3].Rows[i]["color_name"].ToString() == "TTLC" ||
                            dtList.Tables[3].Rows[i]["color_name"].ToString() == "NWLC" || dtList.Tables[3].Rows[i]["color_name"].ToString() == "LC" ||
                            dtList.Tables[3].Rows[i]["color_name"].ToString() == "PINK 1" || dtList.Tables[3].Rows[i]["color_name"].ToString() == "PINK 2")
                        {
                            dtList.Tables[3].Rows[i]["first_to_minusdiff"] = TotalTo_MinusFourth_ColorWise;
                            dtList.Tables[3].Rows[i]["first_to_plusdiff"] = TotalTo_PlusFourth_ColorWise;
                        }
                    }
                    break;
                }

                #endregion Color Wise Diffrence

                #region Cutting Wise Diffrence

                foreach (DataRow Drw in dtList.Tables[4].Rows)
                {
                    ///////////////////////////////////////// First Color //////////////////////////////////////////

                    DataTable DTab_From_first = dtList.Tables[4].Select("color_name in('FW XXX','ORN XXX','JWE YYY','COLL ZZZ')").CopyToDataTable();
                    foreach (DataRow dRow in DTab_From_first.Rows)
                    {
                        Totalfrom_MinusFisrt_Color = Totalfrom_MinusFisrt_Color + Convert.ToDecimal(dRow["-2WT"]);
                        Totalfrom_PlusFisrt_Color = Totalfrom_PlusFisrt_Color + Convert.ToDecimal(dRow["+2WT"]);
                    }
                    Totalfrom_MinusFisrt_Color = Math.Round((Totalfrom_MinusFisrt_Color / Val.ToDecimal(dtList.Tables[4].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                    Totalfrom_PlusFisrt_Color = Math.Round((Totalfrom_PlusFisrt_Color / Val.ToDecimal(dtList.Tables[4].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                    DataTable DTab_To_First = dtList.Tables[5].Select("color_name in('FW XXX','ORN XXX','JWE YYY','COLL ZZZ')").CopyToDataTable();
                    foreach (DataRow dRow in DTab_To_First.Rows)
                    {
                        TotalTo_MinusFisrt_Color = TotalTo_MinusFisrt_Color + Convert.ToDecimal(dRow["-2WT"]);
                        TotalTo_PlusFisrt_Color = TotalTo_PlusFisrt_Color + Convert.ToDecimal(dRow["+2WT"]);
                    }
                    TotalTo_MinusFisrt_Color = Math.Round((TotalTo_MinusFisrt_Color / Val.ToDecimal(dtList.Tables[5].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                    TotalTo_PlusFisrt_Color = Math.Round((TotalTo_PlusFisrt_Color / Val.ToDecimal(dtList.Tables[5].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                    ///////////////////////////////////////// End First Color //////////////////////////////////////////

                    ///////////////////////////////////////// Second Color //////////////////////////////////////////

                    DataTable DTab_From_Second = dtList.Tables[4].Select("color_name in('ORN AAA','FW AAA','JWE BBB','COLL CCC')").CopyToDataTable();
                    foreach (DataRow dRow in DTab_From_Second.Rows)
                    {
                        Totalfrom_MinusSecond_Color = Totalfrom_MinusSecond_Color + Convert.ToDecimal(dRow["-2WT"]);
                        Totalfrom_PlusSecond_Color = Totalfrom_PlusSecond_Color + Convert.ToDecimal(dRow["+2WT"]);
                    }
                    Totalfrom_MinusSecond_Color = Math.Round((Totalfrom_MinusSecond_Color / Val.ToDecimal(dtList.Tables[4].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                    Totalfrom_PlusSecond_Color = Math.Round((Totalfrom_PlusSecond_Color / Val.ToDecimal(dtList.Tables[4].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                    DataTable DTab_To_Second = dtList.Tables[5].Select("color_name in('ORN AAA','FW AAA','JWE BBB','COLL CCC')").CopyToDataTable();
                    foreach (DataRow dRow in DTab_To_Second.Rows)
                    {
                        TotalTo_MinusSecond_Color = TotalTo_MinusSecond_Color + Convert.ToDecimal(dRow["-2WT"]);
                        TotalTo_PlusSecond_Color = TotalTo_PlusSecond_Color + Convert.ToDecimal(dRow["+2WT"]);
                    }
                    TotalTo_MinusSecond_Color = Math.Round((TotalTo_MinusSecond_Color / Val.ToDecimal(dtList.Tables[5].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                    TotalTo_PlusSecond_Color = Math.Round((TotalTo_PlusSecond_Color / Val.ToDecimal(dtList.Tables[5].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                    ///////////////////////////////////////// End Second Color //////////////////////////////////////////

                    ///////////////////////////////////////// Third Color //////////////////////////////////////////

                    DataTable DTab_From_Third = dtList.Tables[4].Select("color_name in('NXY','N1-2')").CopyToDataTable();
                    foreach (DataRow dRow in DTab_From_Third.Rows)
                    {
                        Totalfrom_MinusThird_Color = Totalfrom_MinusThird_Color + Convert.ToDecimal(dRow["-2WT"]);
                        Totalfrom_PlusThird_Color = Totalfrom_PlusThird_Color + Convert.ToDecimal(dRow["+2WT"]);
                    }
                    Totalfrom_MinusThird_Color = Math.Round((Totalfrom_MinusThird_Color / Val.ToDecimal(dtList.Tables[4].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                    Totalfrom_PlusThird_Color = Math.Round((Totalfrom_PlusThird_Color / Val.ToDecimal(dtList.Tables[4].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                    DataTable DTab_To_Third = dtList.Tables[5].Select("color_name in('NXY','N1-2')").CopyToDataTable();
                    foreach (DataRow dRow in DTab_To_Third.Rows)
                    {
                        TotalTo_MinusThird_Color = TotalTo_MinusThird_Color + Convert.ToDecimal(dRow["-2WT"]);
                        TotalTo_PlusThird_Color = TotalTo_PlusThird_Color + Convert.ToDecimal(dRow["+2WT"]);
                    }
                    TotalTo_MinusThird_Color = Math.Round((TotalTo_MinusThird_Color / Val.ToDecimal(dtList.Tables[5].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                    TotalTo_PlusThird_Color = Math.Round((TotalTo_PlusThird_Color / Val.ToDecimal(dtList.Tables[5].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                    ///////////////////////////////////////// End Third Color //////////////////////////////////////////

                    ///////////////////////////////////////// Fourth Color //////////////////////////////////////////

                    DataTable DTab_From_Fourth = dtList.Tables[4].Select("color_name in('NA','NB')").CopyToDataTable();
                    foreach (DataRow dRow in DTab_From_Fourth.Rows)
                    {
                        Totalfrom_MinusFourth_Color = Totalfrom_MinusFourth_Color + Convert.ToDecimal(dRow["-2WT"]);
                        Totalfrom_PlusFourth_Color = Totalfrom_PlusFourth_Color + Convert.ToDecimal(dRow["+2WT"]);
                    }
                    Totalfrom_MinusFourth_Color = Math.Round((Totalfrom_MinusFourth_Color / Val.ToDecimal(dtList.Tables[4].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                    Totalfrom_PlusFourth_Color = Math.Round((Totalfrom_PlusFourth_Color / Val.ToDecimal(dtList.Tables[4].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                    DataTable DTab_To_Fourth = dtList.Tables[5].Select("color_name in('NA','NB')").CopyToDataTable();
                    foreach (DataRow dRow in DTab_To_Fourth.Rows)
                    {
                        TotalTo_MinusFourth_Color = TotalTo_MinusFourth_Color + Convert.ToDecimal(dRow["-2WT"]);
                        TotalTo_PlusFourth_Color = TotalTo_PlusFourth_Color + Convert.ToDecimal(dRow["+2WT"]);
                    }
                    TotalTo_MinusFourth_Color = Math.Round((TotalTo_MinusFourth_Color / Val.ToDecimal(dtList.Tables[5].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                    TotalTo_PlusFourth_Color = Math.Round((TotalTo_PlusFourth_Color / Val.ToDecimal(dtList.Tables[5].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                    ///////////////////////////////////////// End Fourth Color //////////////////////////////////////////

                    ///////////////////////////////////////// Fifth Color //////////////////////////////////////////

                    DataTable DTab_From_Fifth = dtList.Tables[4].Select("color_name in('LB XY','TTLB XY')").CopyToDataTable();
                    foreach (DataRow dRow in DTab_From_Fifth.Rows)
                    {
                        Totalfrom_MinusFifth_Color = Totalfrom_MinusFifth_Color + Convert.ToDecimal(dRow["-2WT"]);
                        Totalfrom_PlusFifth_Color = Totalfrom_PlusFifth_Color + Convert.ToDecimal(dRow["+2WT"]);
                    }
                    Totalfrom_MinusFifth_Color = Math.Round((Totalfrom_MinusFifth_Color / Val.ToDecimal(dtList.Tables[4].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                    Totalfrom_PlusFifth_Color = Math.Round((Totalfrom_PlusFifth_Color / Val.ToDecimal(dtList.Tables[4].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                    DataTable DTab_To_Fifth = dtList.Tables[5].Select("color_name in('LB XY','TTLB XY')").CopyToDataTable();
                    foreach (DataRow dRow in DTab_To_Fifth.Rows)
                    {
                        TotalTo_MinusFifth_Color = TotalTo_MinusFifth_Color + Convert.ToDecimal(dRow["-2WT"]);
                        TotalTo_PlusFifth_Color = TotalTo_PlusFifth_Color + Convert.ToDecimal(dRow["+2WT"]);
                    }
                    TotalTo_MinusFifth_Color = Math.Round((TotalTo_MinusFifth_Color / Val.ToDecimal(dtList.Tables[5].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                    TotalTo_PlusFifth_Color = Math.Round((TotalTo_PlusFifth_Color / Val.ToDecimal(dtList.Tables[5].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                    ///////////////////////////////////////// End Fifth Color //////////////////////////////////////////

                    ///////////////////////////////////////// Six Color //////////////////////////////////////////

                    DataTable DTab_From_Six = dtList.Tables[4].Select("color_name in('LC XY','TTLC XY')").CopyToDataTable();
                    foreach (DataRow dRow in DTab_From_Six.Rows)
                    {
                        Totalfrom_MinusSix_Color = Totalfrom_MinusSix_Color + Convert.ToDecimal(dRow["-2WT"]);
                        Totalfrom_PlusSix_Color = Totalfrom_PlusSix_Color + Convert.ToDecimal(dRow["+2WT"]);
                    }
                    Totalfrom_MinusSix_Color = Math.Round((Totalfrom_MinusSix_Color / Val.ToDecimal(dtList.Tables[4].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                    Totalfrom_PlusSix_Color = Math.Round((Totalfrom_PlusSix_Color / Val.ToDecimal(dtList.Tables[4].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                    DataTable DTab_To_Six = dtList.Tables[5].Select("color_name in('LC XY','TTLC XY')").CopyToDataTable();
                    foreach (DataRow dRow in DTab_To_Six.Rows)
                    {
                        TotalTo_MinusSix_Color = TotalTo_MinusSix_Color + Convert.ToDecimal(dRow["-2WT"]);
                        TotalTo_PlusSix_Color = TotalTo_PlusSix_Color + Convert.ToDecimal(dRow["+2WT"]);
                    }
                    TotalTo_MinusSix_Color = Math.Round((TotalTo_MinusSix_Color / Val.ToDecimal(dtList.Tables[5].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                    TotalTo_PlusSix_Color = Math.Round((TotalTo_PlusSix_Color / Val.ToDecimal(dtList.Tables[5].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                    ///////////////////////////////////////// End Six Color //////////////////////////////////////////

                    ///////////////////////////////////////// Seven Color //////////////////////////////////////////

                    DataTable DTab_From_Seven = dtList.Tables[4].Select("color_name in('LB AB','TTLB AB')").CopyToDataTable();
                    foreach (DataRow dRow in DTab_From_Seven.Rows)
                    {
                        Totalfrom_MinusSeven_Color = Totalfrom_MinusSeven_Color + Convert.ToDecimal(dRow["-2WT"]);
                        Totalfrom_PlusSeven_Color = Totalfrom_PlusSeven_Color + Convert.ToDecimal(dRow["+2WT"]);
                    }
                    Totalfrom_MinusSeven_Color = Math.Round((Totalfrom_MinusSeven_Color / Val.ToDecimal(dtList.Tables[4].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                    Totalfrom_PlusSeven_Color = Math.Round((Totalfrom_PlusSeven_Color / Val.ToDecimal(dtList.Tables[4].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                    DataTable DTab_To_Seven = dtList.Tables[5].Select("color_name in('LB AB','TTLB AB')").CopyToDataTable();
                    foreach (DataRow dRow in DTab_To_Seven.Rows)
                    {
                        TotalTo_MinusSeven_Color = TotalTo_MinusSeven_Color + Convert.ToDecimal(dRow["-2WT"]);
                        TotalTo_PlusSeven_Color = TotalTo_PlusSeven_Color + Convert.ToDecimal(dRow["+2WT"]);
                    }
                    TotalTo_MinusSeven_Color = Math.Round((TotalTo_MinusSeven_Color / Val.ToDecimal(dtList.Tables[5].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                    TotalTo_PlusSeven_Color = Math.Round((TotalTo_PlusSeven_Color / Val.ToDecimal(dtList.Tables[5].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                    ///////////////////////////////////////// End Seven Color //////////////////////////////////////////

                    ///////////////////////////////////////// Eight Color //////////////////////////////////////////

                    DataTable DTab_From_Eight = dtList.Tables[4].Select("color_name in('LC AB','TTLC AB')").CopyToDataTable();
                    foreach (DataRow dRow in DTab_From_Eight.Rows)
                    {
                        Totalfrom_MinusEight_Color = Totalfrom_MinusEight_Color + Convert.ToDecimal(dRow["-2WT"]);
                        Totalfrom_PlusEight_Color = Totalfrom_PlusEight_Color + Convert.ToDecimal(dRow["+2WT"]);
                    }
                    Totalfrom_MinusEight_Color = Math.Round((Totalfrom_MinusEight_Color / Val.ToDecimal(dtList.Tables[4].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                    Totalfrom_PlusEight_Color = Math.Round((Totalfrom_PlusEight_Color / Val.ToDecimal(dtList.Tables[4].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                    DataTable DTab_To_Eight = dtList.Tables[5].Select("color_name in('LC AB','TTLC AB')").CopyToDataTable();
                    foreach (DataRow dRow in DTab_To_Eight.Rows)
                    {
                        TotalTo_MinusEight_Color = TotalTo_MinusEight_Color + Convert.ToDecimal(dRow["-2WT"]);
                        TotalTo_PlusEight_Color = TotalTo_PlusEight_Color + Convert.ToDecimal(dRow["+2WT"]);
                    }
                    TotalTo_MinusEight_Color = Math.Round((TotalTo_MinusEight_Color / Val.ToDecimal(dtList.Tables[5].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                    TotalTo_PlusEight_Color = Math.Round((TotalTo_PlusEight_Color / Val.ToDecimal(dtList.Tables[5].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                    ///////////////////////////////////////// End Eight Color //////////////////////////////////////////

                    TotalTo_MinusFisrt_Color = Totalfrom_MinusFisrt_Color - TotalTo_MinusFisrt_Color;
                    TotalTo_PlusFisrt_Color = Totalfrom_PlusFisrt_Color - TotalTo_PlusFisrt_Color;
                    TotalTo_MinusSecond_Color = Totalfrom_MinusSecond_Color - TotalTo_MinusSecond_Color;
                    TotalTo_PlusSecond_Color = Totalfrom_PlusSecond_Color - TotalTo_PlusSecond_Color;
                    TotalTo_MinusThird_Color = Totalfrom_MinusThird_Color - TotalTo_MinusThird_Color;
                    TotalTo_PlusThird_Color = Totalfrom_PlusThird_Color - TotalTo_PlusThird_Color;
                    TotalTo_MinusFourth_Color = Totalfrom_MinusFourth_Color - TotalTo_MinusFourth_Color;
                    TotalTo_PlusFourth_Color = Totalfrom_PlusFourth_Color - TotalTo_PlusFourth_Color;
                    TotalTo_MinusFifth_Color = Totalfrom_MinusFifth_Color - TotalTo_MinusFifth_Color;
                    TotalTo_PlusFifth_Color = Totalfrom_PlusFifth_Color - TotalTo_PlusFifth_Color;
                    TotalTo_MinusSix_Color = Totalfrom_MinusSix_Color - TotalTo_MinusSix_Color;
                    TotalTo_PlusSix_Color = Totalfrom_PlusSix_Color - TotalTo_PlusSix_Color;
                    TotalTo_MinusSeven_Color = Totalfrom_MinusSeven_Color - TotalTo_MinusSeven_Color;
                    TotalTo_PlusSeven_Color = Totalfrom_PlusSeven_Color - TotalTo_PlusSeven_Color;
                    TotalTo_MinusEight_Color = Totalfrom_MinusEight_Color - TotalTo_MinusEight_Color;
                    TotalTo_PlusEight_Color = Totalfrom_PlusEight_Color - TotalTo_PlusEight_Color;

                    for (int i = 0; i < dtList.Tables[5].Rows.Count; i++)
                    {
                        if (dtList.Tables[5].Rows[i]["color_name"].ToString() == "FW XXX" || dtList.Tables[5].Rows[i]["color_name"].ToString() == "ORN XXX" ||
                            dtList.Tables[5].Rows[i]["color_name"].ToString() == "JWE YYY" || dtList.Tables[5].Rows[i]["color_name"].ToString() == "COLL ZZZ")
                        {
                            dtList.Tables[5].Rows[i]["first_to_minusdiff"] = TotalTo_MinusFisrt_Color;
                            dtList.Tables[5].Rows[i]["first_to_plusdiff"] = TotalTo_PlusFisrt_Color;
                        }
                        if (dtList.Tables[5].Rows[i]["color_name"].ToString() == "ORN AAA" || dtList.Tables[5].Rows[i]["color_name"].ToString() == "FW AAA" ||
                            dtList.Tables[5].Rows[i]["color_name"].ToString() == "JWE BBB" || dtList.Tables[5].Rows[i]["color_name"].ToString() == "COLL CCC")
                        {
                            dtList.Tables[5].Rows[i]["first_to_minusdiff"] = TotalTo_MinusSecond_Color;
                            dtList.Tables[5].Rows[i]["first_to_plusdiff"] = TotalTo_PlusSecond_Color;
                        }
                        if (dtList.Tables[5].Rows[i]["color_name"].ToString() == "NXY" || dtList.Tables[5].Rows[i]["color_name"].ToString() == "N1-2")
                        {
                            dtList.Tables[5].Rows[i]["first_to_minusdiff"] = TotalTo_MinusThird_Color;
                            dtList.Tables[5].Rows[i]["first_to_plusdiff"] = TotalTo_PlusThird_Color;
                        }
                        if (dtList.Tables[5].Rows[i]["color_name"].ToString() == "NA" || dtList.Tables[5].Rows[i]["color_name"].ToString() == "NB")
                        {
                            dtList.Tables[5].Rows[i]["first_to_minusdiff"] = TotalTo_MinusFourth_Color;
                            dtList.Tables[5].Rows[i]["first_to_plusdiff"] = TotalTo_PlusFourth_Color;
                        }
                        if (dtList.Tables[5].Rows[i]["color_name"].ToString() == "LB XY" || dtList.Tables[5].Rows[i]["color_name"].ToString() == "TTLB XY")
                        {
                            dtList.Tables[5].Rows[i]["first_to_minusdiff"] = TotalTo_MinusFourth_Color;
                            dtList.Tables[5].Rows[i]["first_to_plusdiff"] = TotalTo_PlusFourth_Color;
                        }
                        if (dtList.Tables[5].Rows[i]["color_name"].ToString() == "LC XY" || dtList.Tables[5].Rows[i]["color_name"].ToString() == "TTLC XY")
                        {
                            dtList.Tables[5].Rows[i]["first_to_minusdiff"] = TotalTo_MinusSix_Color;
                            dtList.Tables[5].Rows[i]["first_to_plusdiff"] = TotalTo_PlusSix_Color;
                        }
                        if (dtList.Tables[5].Rows[i]["color_name"].ToString() == "LB AB" || dtList.Tables[5].Rows[i]["color_name"].ToString() == "TTLB AB")
                        {
                            dtList.Tables[5].Rows[i]["first_to_minusdiff"] = TotalTo_MinusSeven_Color;
                            dtList.Tables[5].Rows[i]["first_to_plusdiff"] = TotalTo_PlusSeven_Color;
                        }
                        if (dtList.Tables[5].Rows[i]["color_name"].ToString() == "LC AB" || dtList.Tables[5].Rows[i]["color_name"].ToString() == "TTLC AB")
                        {
                            dtList.Tables[5].Rows[i]["first_to_minusdiff"] = TotalTo_MinusEight_Color;
                            dtList.Tables[5].Rows[i]["first_to_plusdiff"] = TotalTo_PlusEight_Color;
                        }
                    }


                    break;
                }

                #endregion Cutting Wise Diffrence
            }
            else if (RBtnCutComparision.SelectedIndex == 1)
            {
                dtList = ObjReportParams.GetMFGAssortment_Cut_Comparision_Data(ReportParams_Property, "RPT_MFG_TRN_Cut_Commparission_Avg");
            }
            else if (RBtnCutComparision.SelectedIndex == 2)
            {
                dtList = ObjReportParams.Print_Assort_Final_Print3(ReportParams_Property, "RPT_MFG_TRN_Assortment_Final_Print_3");
            }
            else if (RBtnCutComparision.SelectedIndex == 3)
            {
                dtList = ObjReportParams.GetMFGAssortment_Cut_Comparision_Data(ReportParams_Property, "RPT_MFG_Assortment_Cut_Commparission_Summ");
            }
            else if (RBtnCutComparision.SelectedIndex == 4)
            {
                if (RBtnLocationType.EditValue.ToString() == "1")
                {
                    dtList = ObjReportParams.GetMFGAssortment_Cut_Comparision_Data(ReportParams_Property, "RPT_MFG_Assortment_Cut_Commparission_New");
                }
                else if (RBtnLocationType.EditValue.ToString() == "2")
                {
                    dtList = ObjReportParams.GetMFGAssortment_Cut_Comparision_Data(ReportParams_Property, "RPT_MFG_Assortment_Cut_Commparission_Mum");
                }

                #region Diffrence               

                //int cnt2 = 0;

                //foreach (DataRow Drw in dtList.Tables[1].Rows)
                //{
                //    cnt2++;

                //    int cnt = 1;

                //    foreach (DataRow Drw1 in dtList.Tables[2].Rows)
                //    {
                //        if (cnt2 == cnt)
                //        {
                //            Drw1["minus_two_per_diff"] = Val.ToDecimal(Drw["minus_two_per"]) - Val.ToDecimal(Drw1["minus_two_per"]);
                //            Drw1["pluse_two_per_diff"] = Val.ToDecimal(Drw["pluse_two_per"]) - Val.ToDecimal(Drw1["pluse_two_per"]);
                //        }
                //        cnt++;
                //    }
                //}

                //int cnt3 = 0;

                //foreach (DataRow Drw in dtList.Tables[3].Rows)
                //{
                //    cnt3++;

                //    int cnt = 1;

                //    foreach (DataRow Drw1 in dtList.Tables[4].Rows)
                //    {
                //        if (cnt3 == cnt)
                //        {
                //            Drw1["minus_two_per_diff"] = Val.ToDecimal(Drw["minus_two_per"]) - Val.ToDecimal(Drw1["minus_two_per"]);
                //            Drw1["pluse_two_per_diff"] = Val.ToDecimal(Drw["pluse_two_per"]) - Val.ToDecimal(Drw1["pluse_two_per"]);
                //        }
                //        cnt++;
                //    }
                //}

                #endregion Diffrence

                #region Color Wise Diffrence

                //foreach (DataRow Drw in dtList.Tables[1].Rows)
                //{
                //    ///////////////////////////////////////// First Color //////////////////////////////////////////

                //    DataTable DTab_From_first = dtList.Tables[1].Select("color_name in('FW','WT')").CopyToDataTable();
                //    foreach (DataRow dRow in DTab_From_first.Rows)
                //    {
                //        Totalfrom_MinusFisrt_ColorWise = Totalfrom_MinusFisrt_ColorWise + Convert.ToDecimal(dRow["-2WT"]);
                //        Totalfrom_PlusFisrt_ColorWise = Totalfrom_PlusFisrt_ColorWise + Convert.ToDecimal(dRow["+2WT"]);
                //    }
                //    Totalfrom_MinusFisrt_ColorWise = Math.Round((Totalfrom_MinusFisrt_ColorWise / Val.ToDecimal(dtList.Tables[1].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                //    Totalfrom_PlusFisrt_ColorWise = Math.Round((Totalfrom_PlusFisrt_ColorWise / Val.ToDecimal(dtList.Tables[1].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                //    DataTable DTab_To_First = dtList.Tables[2].Select("color_name in('FW','WT')").CopyToDataTable();
                //    foreach (DataRow dRow in DTab_To_First.Rows)
                //    {
                //        TotalTo_MinusFisrt_ColorWise = TotalTo_MinusFisrt_ColorWise + Convert.ToDecimal(dRow["-2WT"]);
                //        TotalTo_PlusFisrt_ColorWise = TotalTo_PlusFisrt_ColorWise + Convert.ToDecimal(dRow["+2WT"]);
                //    }
                //    TotalTo_MinusFisrt_ColorWise = Math.Round((TotalTo_MinusFisrt_ColorWise / Val.ToDecimal(dtList.Tables[2].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                //    TotalTo_PlusFisrt_ColorWise = Math.Round((TotalTo_PlusFisrt_ColorWise / Val.ToDecimal(dtList.Tables[2].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                //    ///////////////////////////////////////// End First Color //////////////////////////////////////////

                //    ///////////////////////////////////////// Second Color //////////////////////////////////////////

                //    DataTable DTab_From_Second = dtList.Tables[1].Select("color_name in('N')").CopyToDataTable();
                //    foreach (DataRow dRow in DTab_From_Second.Rows)
                //    {
                //        Totalfrom_MinusSecond_ColorWise = Totalfrom_MinusSecond_ColorWise + Convert.ToDecimal(dRow["-2WT"]);
                //        Totalfrom_PlusSecond_ColorWise = Totalfrom_PlusSecond_ColorWise + Convert.ToDecimal(dRow["+2WT"]);
                //    }
                //    Totalfrom_MinusSecond_ColorWise = Math.Round((Totalfrom_MinusSecond_ColorWise / Val.ToDecimal(dtList.Tables[1].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                //    Totalfrom_PlusSecond_ColorWise = Math.Round((Totalfrom_PlusSecond_ColorWise / Val.ToDecimal(dtList.Tables[1].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                //    DataTable DTab_To_Second = dtList.Tables[2].Select("color_name in('N')").CopyToDataTable();
                //    foreach (DataRow dRow in DTab_To_Second.Rows)
                //    {
                //        TotalTo_MinusSecond_ColorWise = TotalTo_MinusSecond_ColorWise + Convert.ToDecimal(dRow["-2WT"]);
                //        TotalTo_PlusSecond_ColorWise = TotalTo_PlusSecond_ColorWise + Convert.ToDecimal(dRow["+2WT"]);
                //    }
                //    TotalTo_MinusSecond_ColorWise = Math.Round((TotalTo_MinusSecond_ColorWise / Val.ToDecimal(dtList.Tables[2].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                //    TotalTo_PlusSecond_ColorWise = Math.Round((TotalTo_PlusSecond_ColorWise / Val.ToDecimal(dtList.Tables[2].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                //    ///////////////////////////////////////// End Second Color //////////////////////////////////////////

                //    ///////////////////////////////////////// Third Color //////////////////////////////////////////

                //    DataTable DTab_From_Third = dtList.Tables[1].Select("color_name in('AirLB','TTLB','NWLB','OWLB','LB')").CopyToDataTable();
                //    foreach (DataRow dRow in DTab_From_Third.Rows)
                //    {
                //        Totalfrom_MinusThird_ColorWise = Totalfrom_MinusThird_ColorWise + Convert.ToDecimal(dRow["-2WT"]);
                //        Totalfrom_PlusThird_ColorWise = Totalfrom_PlusThird_ColorWise + Convert.ToDecimal(dRow["+2WT"]);
                //    }
                //    Totalfrom_MinusThird_ColorWise = Math.Round((Totalfrom_MinusThird_ColorWise / Val.ToDecimal(dtList.Tables[1].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                //    Totalfrom_PlusThird_ColorWise = Math.Round((Totalfrom_PlusThird_ColorWise / Val.ToDecimal(dtList.Tables[1].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                //    DataTable DTab_To_Third = dtList.Tables[2].Select("color_name in('AirLB','TTLB','NWLB','OWLB','LB')").CopyToDataTable();
                //    foreach (DataRow dRow in DTab_To_Third.Rows)
                //    {
                //        TotalTo_MinusThird_ColorWise = TotalTo_MinusThird_ColorWise + Convert.ToDecimal(dRow["-2WT"]);
                //        TotalTo_PlusThird_ColorWise = TotalTo_PlusThird_ColorWise + Convert.ToDecimal(dRow["+2WT"]);
                //    }
                //    TotalTo_MinusThird_ColorWise = Math.Round((TotalTo_MinusThird_ColorWise / Val.ToDecimal(dtList.Tables[2].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                //    TotalTo_PlusThird_ColorWise = Math.Round((TotalTo_PlusThird_ColorWise / Val.ToDecimal(dtList.Tables[2].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                //    ///////////////////////////////////////// End Third Color //////////////////////////////////////////

                //    ///////////////////////////////////////// Fourth Color //////////////////////////////////////////

                //    DataTable DTab_From_Fourth = dtList.Tables[1].Select("color_name in('AirLC','TTLC','NWLC','LC','PINK 1','PINK 2')").CopyToDataTable();
                //    foreach (DataRow dRow in DTab_From_Fourth.Rows)
                //    {
                //        Totalfrom_MinusFourth_ColorWise = Totalfrom_MinusFourth_ColorWise + Convert.ToDecimal(dRow["-2WT"]);
                //        Totalfrom_PlusFourth_ColorWise = Totalfrom_PlusFourth_ColorWise + Convert.ToDecimal(dRow["+2WT"]);
                //    }
                //    Totalfrom_MinusFourth_ColorWise = Math.Round((Totalfrom_MinusFourth_ColorWise / Val.ToDecimal(dtList.Tables[1].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                //    Totalfrom_PlusFourth_ColorWise = Math.Round((Totalfrom_PlusFourth_ColorWise / Val.ToDecimal(dtList.Tables[1].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                //    DataTable DTab_To_Fourth = dtList.Tables[2].Select("color_name in('AirLC','TTLC','NWLC','LC','PINK 1','PINK 2')").CopyToDataTable();
                //    foreach (DataRow dRow in DTab_To_Fourth.Rows)
                //    {
                //        TotalTo_MinusFourth_ColorWise = TotalTo_MinusFourth_ColorWise + Convert.ToDecimal(dRow["-2WT"]);
                //        TotalTo_PlusFourth_ColorWise = TotalTo_PlusFourth_ColorWise + Convert.ToDecimal(dRow["+2WT"]);
                //    }
                //    TotalTo_MinusFourth_ColorWise = Math.Round((TotalTo_MinusFourth_ColorWise / Val.ToDecimal(dtList.Tables[2].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                //    TotalTo_PlusFourth_ColorWise = Math.Round((TotalTo_PlusFourth_ColorWise / Val.ToDecimal(dtList.Tables[2].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                //    ///////////////////////////////////////// End Fourth Color //////////////////////////////////////////

                //    TotalTo_MinusFisrt_ColorWise = Totalfrom_MinusFisrt_ColorWise - TotalTo_MinusFisrt_ColorWise;
                //    TotalTo_PlusFisrt_ColorWise = Totalfrom_PlusFisrt_ColorWise - TotalTo_PlusFisrt_ColorWise;
                //    TotalTo_MinusSecond_ColorWise = Totalfrom_MinusSecond_ColorWise - TotalTo_MinusSecond_ColorWise;
                //    TotalTo_PlusSecond_ColorWise = Totalfrom_PlusSecond_ColorWise - TotalTo_PlusSecond_ColorWise;
                //    TotalTo_MinusThird_ColorWise = Totalfrom_MinusThird_ColorWise - TotalTo_MinusThird_ColorWise;
                //    TotalTo_PlusThird_ColorWise = Totalfrom_PlusThird_ColorWise - TotalTo_PlusThird_ColorWise;
                //    TotalTo_MinusFourth_ColorWise = Totalfrom_MinusFourth_ColorWise - TotalTo_MinusFourth_ColorWise;
                //    TotalTo_PlusFourth_ColorWise = Totalfrom_PlusFourth_ColorWise - TotalTo_PlusFourth_ColorWise;

                //    for (int i = 0; i < dtList.Tables[2].Rows.Count; i++)
                //    {
                //        if (dtList.Tables[2].Rows[i]["color_name"].ToString() == "FW" || dtList.Tables[2].Rows[i]["color_name"].ToString() == "WT")
                //        {
                //            dtList.Tables[2].Rows[i]["first_to_minusdiff"] = TotalTo_MinusFisrt_ColorWise;
                //            dtList.Tables[2].Rows[i]["first_to_plusdiff"] = TotalTo_PlusFisrt_ColorWise;
                //        }
                //        if (dtList.Tables[2].Rows[i]["color_name"].ToString() == "N")
                //        {
                //            dtList.Tables[2].Rows[i]["first_to_minusdiff"] = TotalTo_MinusSecond_ColorWise;
                //            dtList.Tables[2].Rows[i]["first_to_plusdiff"] = TotalTo_PlusSecond_ColorWise;
                //        }
                //        if (dtList.Tables[2].Rows[i]["color_name"].ToString() == "AirLB" || dtList.Tables[2].Rows[i]["color_name"].ToString() == "TTLB" ||
                //            dtList.Tables[2].Rows[i]["color_name"].ToString() == "NWLB" || dtList.Tables[2].Rows[i]["color_name"].ToString() == "OWLB" || dtList.Tables[2].Rows[i]["color_name"].ToString() == "LB")
                //        {
                //            dtList.Tables[2].Rows[i]["first_to_minusdiff"] = TotalTo_MinusThird_ColorWise;
                //            dtList.Tables[2].Rows[i]["first_to_plusdiff"] = TotalTo_PlusThird_ColorWise;
                //        }
                //        if (dtList.Tables[2].Rows[i]["color_name"].ToString() == "AirLC" || dtList.Tables[2].Rows[i]["color_name"].ToString() == "TTLC" ||
                //            dtList.Tables[2].Rows[i]["color_name"].ToString() == "NWLC" || dtList.Tables[2].Rows[i]["color_name"].ToString() == "LC" ||
                //            dtList.Tables[2].Rows[i]["color_name"].ToString() == "PINK 1" || dtList.Tables[2].Rows[i]["color_name"].ToString() == "PINK 2")
                //        {
                //            dtList.Tables[2].Rows[i]["first_to_minusdiff"] = TotalTo_MinusFourth_ColorWise;
                //            dtList.Tables[2].Rows[i]["first_to_plusdiff"] = TotalTo_PlusFourth_ColorWise;
                //        }
                //    }
                //    break;
                //}

                #endregion Color Wise Diffrence

                #region Cutting Wise Diffrence

                //foreach (DataRow Drw in dtList.Tables[3].Rows)
                //{
                //    ///////////////////////////////////////// First Color //////////////////////////////////////////

                //    DataTable DTab_From_first = dtList.Tables[3].Select("color_name in('FW XXX','ORN XXX','JWE YYY','COLL ZZZ')").CopyToDataTable();
                //    foreach (DataRow dRow in DTab_From_first.Rows)
                //    {
                //        Totalfrom_MinusFisrt_Color = Totalfrom_MinusFisrt_Color + Convert.ToDecimal(dRow["-2WT"]);
                //        Totalfrom_PlusFisrt_Color = Totalfrom_PlusFisrt_Color + Convert.ToDecimal(dRow["+2WT"]);
                //    }
                //    Totalfrom_MinusFisrt_Color = Math.Round((Totalfrom_MinusFisrt_Color / Val.ToDecimal(dtList.Tables[3].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                //    Totalfrom_PlusFisrt_Color = Math.Round((Totalfrom_PlusFisrt_Color / Val.ToDecimal(dtList.Tables[3].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                //    DataTable DTab_To_First = dtList.Tables[4].Select("color_name in('FW XXX','ORN XXX','JWE YYY','COLL ZZZ')").CopyToDataTable();
                //    foreach (DataRow dRow in DTab_To_First.Rows)
                //    {
                //        TotalTo_MinusFisrt_Color = TotalTo_MinusFisrt_Color + Convert.ToDecimal(dRow["-2WT"]);
                //        TotalTo_PlusFisrt_Color = TotalTo_PlusFisrt_Color + Convert.ToDecimal(dRow["+2WT"]);
                //    }
                //    TotalTo_MinusFisrt_Color = Math.Round((TotalTo_MinusFisrt_Color / Val.ToDecimal(dtList.Tables[4].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                //    TotalTo_PlusFisrt_Color = Math.Round((TotalTo_PlusFisrt_Color / Val.ToDecimal(dtList.Tables[4].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                //    ///////////////////////////////////////// End First Color //////////////////////////////////////////

                //    ///////////////////////////////////////// Second Color //////////////////////////////////////////

                //    DataTable DTab_From_Second = dtList.Tables[3].Select("color_name in('ORN AAA','FW AAA','JWE BBB','COLL CCC')").CopyToDataTable();
                //    foreach (DataRow dRow in DTab_From_Second.Rows)
                //    {
                //        Totalfrom_MinusSecond_Color = Totalfrom_MinusSecond_Color + Convert.ToDecimal(dRow["-2WT"]);
                //        Totalfrom_PlusSecond_Color = Totalfrom_PlusSecond_Color + Convert.ToDecimal(dRow["+2WT"]);
                //    }
                //    Totalfrom_MinusSecond_Color = Math.Round((Totalfrom_MinusSecond_Color / Val.ToDecimal(dtList.Tables[3].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                //    Totalfrom_PlusSecond_Color = Math.Round((Totalfrom_PlusSecond_Color / Val.ToDecimal(dtList.Tables[3].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                //    DataTable DTab_To_Second = dtList.Tables[4].Select("color_name in('ORN AAA','FW AAA','JWE BBB','COLL CCC')").CopyToDataTable();
                //    foreach (DataRow dRow in DTab_To_Second.Rows)
                //    {
                //        TotalTo_MinusSecond_Color = TotalTo_MinusSecond_Color + Convert.ToDecimal(dRow["-2WT"]);
                //        TotalTo_PlusSecond_Color = TotalTo_PlusSecond_Color + Convert.ToDecimal(dRow["+2WT"]);
                //    }
                //    TotalTo_MinusSecond_Color = Math.Round((TotalTo_MinusSecond_Color / Val.ToDecimal(dtList.Tables[4].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                //    TotalTo_PlusSecond_Color = Math.Round((TotalTo_PlusSecond_Color / Val.ToDecimal(dtList.Tables[4].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                //    ///////////////////////////////////////// End Second Color //////////////////////////////////////////

                //    ///////////////////////////////////////// Third Color //////////////////////////////////////////

                //    DataTable DTab_From_Third = dtList.Tables[3].Select("color_name in('NXY','N1-2')").CopyToDataTable();
                //    foreach (DataRow dRow in DTab_From_Third.Rows)
                //    {
                //        Totalfrom_MinusThird_Color = Totalfrom_MinusThird_Color + Convert.ToDecimal(dRow["-2WT"]);
                //        Totalfrom_PlusThird_Color = Totalfrom_PlusThird_Color + Convert.ToDecimal(dRow["+2WT"]);
                //    }
                //    Totalfrom_MinusThird_Color = Math.Round((Totalfrom_MinusThird_Color / Val.ToDecimal(dtList.Tables[3].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                //    Totalfrom_PlusThird_Color = Math.Round((Totalfrom_PlusThird_Color / Val.ToDecimal(dtList.Tables[3].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                //    DataTable DTab_To_Third = dtList.Tables[4].Select("color_name in('NXY','N1-2')").CopyToDataTable();
                //    foreach (DataRow dRow in DTab_To_Third.Rows)
                //    {
                //        TotalTo_MinusThird_Color = TotalTo_MinusThird_Color + Convert.ToDecimal(dRow["-2WT"]);
                //        TotalTo_PlusThird_Color = TotalTo_PlusThird_Color + Convert.ToDecimal(dRow["+2WT"]);
                //    }
                //    TotalTo_MinusThird_Color = Math.Round((TotalTo_MinusThird_Color / Val.ToDecimal(dtList.Tables[4].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                //    TotalTo_PlusThird_Color = Math.Round((TotalTo_PlusThird_Color / Val.ToDecimal(dtList.Tables[4].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                //    ///////////////////////////////////////// End Third Color //////////////////////////////////////////

                //    ///////////////////////////////////////// Fourth Color //////////////////////////////////////////

                //    DataTable DTab_From_Fourth = dtList.Tables[3].Select("color_name in('NA','NB')").CopyToDataTable();
                //    foreach (DataRow dRow in DTab_From_Fourth.Rows)
                //    {
                //        Totalfrom_MinusFourth_Color = Totalfrom_MinusFourth_Color + Convert.ToDecimal(dRow["-2WT"]);
                //        Totalfrom_PlusFourth_Color = Totalfrom_PlusFourth_Color + Convert.ToDecimal(dRow["+2WT"]);
                //    }
                //    Totalfrom_MinusFourth_Color = Math.Round((Totalfrom_MinusFourth_Color / Val.ToDecimal(dtList.Tables[3].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                //    Totalfrom_PlusFourth_Color = Math.Round((Totalfrom_PlusFourth_Color / Val.ToDecimal(dtList.Tables[3].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                //    DataTable DTab_To_Fourth = dtList.Tables[4].Select("color_name in('NA','NB')").CopyToDataTable();
                //    foreach (DataRow dRow in DTab_To_Fourth.Rows)
                //    {
                //        TotalTo_MinusFourth_Color = TotalTo_MinusFourth_Color + Convert.ToDecimal(dRow["-2WT"]);
                //        TotalTo_PlusFourth_Color = TotalTo_PlusFourth_Color + Convert.ToDecimal(dRow["+2WT"]);
                //    }
                //    TotalTo_MinusFourth_Color = Math.Round((TotalTo_MinusFourth_Color / Val.ToDecimal(dtList.Tables[4].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                //    TotalTo_PlusFourth_Color = Math.Round((TotalTo_PlusFourth_Color / Val.ToDecimal(dtList.Tables[4].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                //    ///////////////////////////////////////// End Fourth Color //////////////////////////////////////////

                //    ///////////////////////////////////////// Fifth Color //////////////////////////////////////////

                //    DataTable DTab_From_Fifth = dtList.Tables[3].Select("color_name in('LB XY','TTLB XY')").CopyToDataTable();
                //    foreach (DataRow dRow in DTab_From_Fifth.Rows)
                //    {
                //        Totalfrom_MinusFifth_Color = Totalfrom_MinusFifth_Color + Convert.ToDecimal(dRow["-2WT"]);
                //        Totalfrom_PlusFifth_Color = Totalfrom_PlusFifth_Color + Convert.ToDecimal(dRow["+2WT"]);
                //    }
                //    Totalfrom_MinusFifth_Color = Math.Round((Totalfrom_MinusFifth_Color / Val.ToDecimal(dtList.Tables[3].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                //    Totalfrom_PlusFifth_Color = Math.Round((Totalfrom_PlusFifth_Color / Val.ToDecimal(dtList.Tables[3].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                //    DataTable DTab_To_Fifth = dtList.Tables[4].Select("color_name in('LB XY','TTLB XY')").CopyToDataTable();
                //    foreach (DataRow dRow in DTab_To_Fifth.Rows)
                //    {
                //        TotalTo_MinusFifth_Color = TotalTo_MinusFifth_Color + Convert.ToDecimal(dRow["-2WT"]);
                //        TotalTo_PlusFifth_Color = TotalTo_PlusFifth_Color + Convert.ToDecimal(dRow["+2WT"]);
                //    }
                //    TotalTo_MinusFifth_Color = Math.Round((TotalTo_MinusFifth_Color / Val.ToDecimal(dtList.Tables[4].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                //    TotalTo_PlusFifth_Color = Math.Round((TotalTo_PlusFifth_Color / Val.ToDecimal(dtList.Tables[4].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                //    ///////////////////////////////////////// End Fifth Color //////////////////////////////////////////

                //    ///////////////////////////////////////// Six Color //////////////////////////////////////////

                //    DataTable DTab_From_Six = dtList.Tables[3].Select("color_name in('LC XY','TTLC XY')").CopyToDataTable();
                //    foreach (DataRow dRow in DTab_From_Six.Rows)
                //    {
                //        Totalfrom_MinusSix_Color = Totalfrom_MinusSix_Color + Convert.ToDecimal(dRow["-2WT"]);
                //        Totalfrom_PlusSix_Color = Totalfrom_PlusSix_Color + Convert.ToDecimal(dRow["+2WT"]);
                //    }
                //    Totalfrom_MinusSix_Color = Math.Round((Totalfrom_MinusSix_Color / Val.ToDecimal(dtList.Tables[3].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                //    Totalfrom_PlusSix_Color = Math.Round((Totalfrom_PlusSix_Color / Val.ToDecimal(dtList.Tables[3].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                //    DataTable DTab_To_Six = dtList.Tables[4].Select("color_name in('LC XY','TTLC XY')").CopyToDataTable();
                //    foreach (DataRow dRow in DTab_To_Six.Rows)
                //    {
                //        TotalTo_MinusSix_Color = TotalTo_MinusSix_Color + Convert.ToDecimal(dRow["-2WT"]);
                //        TotalTo_PlusSix_Color = TotalTo_PlusSix_Color + Convert.ToDecimal(dRow["+2WT"]);
                //    }
                //    TotalTo_MinusSix_Color = Math.Round((TotalTo_MinusSix_Color / Val.ToDecimal(dtList.Tables[4].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                //    TotalTo_PlusSix_Color = Math.Round((TotalTo_PlusSix_Color / Val.ToDecimal(dtList.Tables[4].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                //    ///////////////////////////////////////// End Six Color //////////////////////////////////////////

                //    ///////////////////////////////////////// Seven Color //////////////////////////////////////////

                //    DataTable DTab_From_Seven = dtList.Tables[3].Select("color_name in('LB AB','TTLB AB')").CopyToDataTable();
                //    foreach (DataRow dRow in DTab_From_Seven.Rows)
                //    {
                //        Totalfrom_MinusSeven_Color = Totalfrom_MinusSeven_Color + Convert.ToDecimal(dRow["-2WT"]);
                //        Totalfrom_PlusSeven_Color = Totalfrom_PlusSeven_Color + Convert.ToDecimal(dRow["+2WT"]);
                //    }
                //    Totalfrom_MinusSeven_Color = Math.Round((Totalfrom_MinusSeven_Color / Val.ToDecimal(dtList.Tables[3].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                //    Totalfrom_PlusSeven_Color = Math.Round((Totalfrom_PlusSeven_Color / Val.ToDecimal(dtList.Tables[3].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                //    DataTable DTab_To_Seven = dtList.Tables[4].Select("color_name in('LB AB','TTLB AB')").CopyToDataTable();
                //    foreach (DataRow dRow in DTab_To_Seven.Rows)
                //    {
                //        TotalTo_MinusSeven_Color = TotalTo_MinusSeven_Color + Convert.ToDecimal(dRow["-2WT"]);
                //        TotalTo_PlusSeven_Color = TotalTo_PlusSeven_Color + Convert.ToDecimal(dRow["+2WT"]);
                //    }
                //    TotalTo_MinusSeven_Color = Math.Round((TotalTo_MinusSeven_Color / Val.ToDecimal(dtList.Tables[4].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                //    TotalTo_PlusSeven_Color = Math.Round((TotalTo_PlusSeven_Color / Val.ToDecimal(dtList.Tables[4].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                //    ///////////////////////////////////////// End Seven Color //////////////////////////////////////////

                //    ///////////////////////////////////////// Eight Color //////////////////////////////////////////

                //    DataTable DTab_From_Eight = dtList.Tables[3].Select("color_name in('LC AB','TTLC AB')").CopyToDataTable();
                //    foreach (DataRow dRow in DTab_From_Eight.Rows)
                //    {
                //        Totalfrom_MinusEight_Color = Totalfrom_MinusEight_Color + Convert.ToDecimal(dRow["-2WT"]);
                //        Totalfrom_PlusEight_Color = Totalfrom_PlusEight_Color + Convert.ToDecimal(dRow["+2WT"]);
                //    }
                //    Totalfrom_MinusEight_Color = Math.Round((Totalfrom_MinusEight_Color / Val.ToDecimal(dtList.Tables[3].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                //    Totalfrom_PlusEight_Color = Math.Round((Totalfrom_PlusEight_Color / Val.ToDecimal(dtList.Tables[3].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                //    DataTable DTab_To_Eight = dtList.Tables[4].Select("color_name in('LC AB','TTLC AB')").CopyToDataTable();
                //    foreach (DataRow dRow in DTab_To_Eight.Rows)
                //    {
                //        TotalTo_MinusEight_Color = TotalTo_MinusEight_Color + Convert.ToDecimal(dRow["-2WT"]);
                //        TotalTo_PlusEight_Color = TotalTo_PlusEight_Color + Convert.ToDecimal(dRow["+2WT"]);
                //    }
                //    TotalTo_MinusEight_Color = Math.Round((TotalTo_MinusEight_Color / Val.ToDecimal(dtList.Tables[4].Rows[0]["grn_carat_total_minus2"])) * 100, 2);
                //    TotalTo_PlusEight_Color = Math.Round((TotalTo_PlusEight_Color / Val.ToDecimal(dtList.Tables[4].Rows[0]["grn_carat_total_pls2"])) * 100, 2);

                //    ///////////////////////////////////////// End Eight Color //////////////////////////////////////////

                //    TotalTo_MinusFisrt_Color = Totalfrom_MinusFisrt_Color - TotalTo_MinusFisrt_Color;
                //    TotalTo_PlusFisrt_Color = Totalfrom_PlusFisrt_Color - TotalTo_PlusFisrt_Color;
                //    TotalTo_MinusSecond_Color = Totalfrom_MinusSecond_Color - TotalTo_MinusSecond_Color;
                //    TotalTo_PlusSecond_Color = Totalfrom_PlusSecond_Color - TotalTo_PlusSecond_Color;
                //    TotalTo_MinusThird_Color = Totalfrom_MinusThird_Color - TotalTo_MinusThird_Color;
                //    TotalTo_PlusThird_Color = Totalfrom_PlusThird_Color - TotalTo_PlusThird_Color;
                //    TotalTo_MinusFourth_Color = Totalfrom_MinusFourth_Color - TotalTo_MinusFourth_Color;
                //    TotalTo_PlusFourth_Color = Totalfrom_PlusFourth_Color - TotalTo_PlusFourth_Color;
                //    TotalTo_MinusFifth_Color = Totalfrom_MinusFifth_Color - TotalTo_MinusFifth_Color;
                //    TotalTo_PlusFifth_Color = Totalfrom_PlusFifth_Color - TotalTo_PlusFifth_Color;
                //    TotalTo_MinusSix_Color = Totalfrom_MinusSix_Color - TotalTo_MinusSix_Color;
                //    TotalTo_PlusSix_Color = Totalfrom_PlusSix_Color - TotalTo_PlusSix_Color;
                //    TotalTo_MinusSeven_Color = Totalfrom_MinusSeven_Color - TotalTo_MinusSeven_Color;
                //    TotalTo_PlusSeven_Color = Totalfrom_PlusSeven_Color - TotalTo_PlusSeven_Color;
                //    TotalTo_MinusEight_Color = Totalfrom_MinusEight_Color - TotalTo_MinusEight_Color;
                //    TotalTo_PlusEight_Color = Totalfrom_PlusEight_Color - TotalTo_PlusEight_Color;

                //    for (int i = 0; i < dtList.Tables[4].Rows.Count; i++)
                //    {
                //        if (dtList.Tables[4].Rows[i]["color_name"].ToString() == "FW XXX" || dtList.Tables[4].Rows[i]["color_name"].ToString() == "ORN XXX" ||
                //            dtList.Tables[4].Rows[i]["color_name"].ToString() == "JWE YYY" || dtList.Tables[4].Rows[i]["color_name"].ToString() == "COLL ZZZ")
                //        {
                //            dtList.Tables[4].Rows[i]["first_to_minusdiff"] = TotalTo_MinusFisrt_Color;
                //            dtList.Tables[4].Rows[i]["first_to_plusdiff"] = TotalTo_PlusFisrt_Color;
                //        }
                //        if (dtList.Tables[4].Rows[i]["color_name"].ToString() == "ORN AAA" || dtList.Tables[4].Rows[i]["color_name"].ToString() == "FW AAA" ||
                //            dtList.Tables[4].Rows[i]["color_name"].ToString() == "JWE BBB" || dtList.Tables[4].Rows[i]["color_name"].ToString() == "COLL CCC")
                //        {
                //            dtList.Tables[4].Rows[i]["first_to_minusdiff"] = TotalTo_MinusSecond_Color;
                //            dtList.Tables[4].Rows[i]["first_to_plusdiff"] = TotalTo_PlusSecond_Color;
                //        }
                //        if (dtList.Tables[4].Rows[i]["color_name"].ToString() == "NXY" || dtList.Tables[4].Rows[i]["color_name"].ToString() == "N1-2")
                //        {
                //            dtList.Tables[4].Rows[i]["first_to_minusdiff"] = TotalTo_MinusThird_Color;
                //            dtList.Tables[4].Rows[i]["first_to_plusdiff"] = TotalTo_PlusThird_Color;
                //        }
                //        if (dtList.Tables[4].Rows[i]["color_name"].ToString() == "NA" || dtList.Tables[4].Rows[i]["color_name"].ToString() == "NB")
                //        {
                //            dtList.Tables[4].Rows[i]["first_to_minusdiff"] = TotalTo_MinusFourth_Color;
                //            dtList.Tables[4].Rows[i]["first_to_plusdiff"] = TotalTo_PlusFourth_Color;
                //        }
                //        if (dtList.Tables[4].Rows[i]["color_name"].ToString() == "LB XY" || dtList.Tables[4].Rows[i]["color_name"].ToString() == "TTLB XY")
                //        {
                //            dtList.Tables[4].Rows[i]["first_to_minusdiff"] = TotalTo_MinusFourth_Color;
                //            dtList.Tables[4].Rows[i]["first_to_plusdiff"] = TotalTo_PlusFourth_Color;
                //        }
                //        if (dtList.Tables[4].Rows[i]["color_name"].ToString() == "LC XY" || dtList.Tables[4].Rows[i]["color_name"].ToString() == "TTLC XY")
                //        {
                //            dtList.Tables[4].Rows[i]["first_to_minusdiff"] = TotalTo_MinusSix_Color;
                //            dtList.Tables[4].Rows[i]["first_to_plusdiff"] = TotalTo_PlusSix_Color;
                //        }
                //        if (dtList.Tables[4].Rows[i]["color_name"].ToString() == "LB AB" || dtList.Tables[4].Rows[i]["color_name"].ToString() == "TTLB AB")
                //        {
                //            dtList.Tables[4].Rows[i]["first_to_minusdiff"] = TotalTo_MinusSeven_Color;
                //            dtList.Tables[4].Rows[i]["first_to_plusdiff"] = TotalTo_PlusSeven_Color;
                //        }
                //        if (dtList.Tables[4].Rows[i]["color_name"].ToString() == "LC AB" || dtList.Tables[4].Rows[i]["color_name"].ToString() == "TTLC AB")
                //        {
                //            dtList.Tables[4].Rows[i]["first_to_minusdiff"] = TotalTo_MinusEight_Color;
                //            dtList.Tables[4].Rows[i]["first_to_plusdiff"] = TotalTo_PlusEight_Color;
                //        }
                //    }
                //    break;
                //}

                #endregion Cutting Wise Diffrence
            }
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (RBtnCutComparision.SelectedIndex == 0)
            {
                PanelLoading.Visible = false;

                FrmReportViewer FrmReportViewer = new FrmReportViewer();
                foreach (DataTable DTab in dtList.Tables)
                    FrmReportViewer.DS.Tables.Add(DTab.Copy());
                FrmReportViewer.GroupBy = "";
                FrmReportViewer.GroupBy = "";
                FrmReportViewer.RepName = "";
                FrmReportViewer.RepPara = "";
                this.Cursor = Cursors.Default;
                FrmReportViewer.AllowSetFormula = true;

                FrmReportViewer.ShowForm_SubReport("Cut_Comparison", 120, FrmReportViewer.ReportFolder.CUT_COMPARISON);

                DTab_IssueJanged = null;
                FrmReportViewer.DS.Tables.Clear();
                FrmReportViewer.DS.Clear();
                FrmReportViewer = null;

                PanelLoading.Visible = false;
            }
            else if (RBtnCutComparision.SelectedIndex == 1)
            {
                PanelLoading.Visible = false;

                FrmReportViewer FrmReportViewer = new FrmReportViewer();
                foreach (DataTable DTab in dtList.Tables)
                    FrmReportViewer.DS.Tables.Add(DTab.Copy());
                FrmReportViewer.GroupBy = "";
                FrmReportViewer.GroupBy = "";
                FrmReportViewer.RepName = "";
                FrmReportViewer.RepPara = "";
                this.Cursor = Cursors.Default;
                FrmReportViewer.AllowSetFormula = true;

                FrmReportViewer.ShowForm_SubReport("Cut_Comparison_Average", 120, FrmReportViewer.ReportFolder.CUT_COMPARISON_AVG);

                DTab_IssueJanged = null;
                FrmReportViewer.DS.Tables.Clear();
                FrmReportViewer.DS.Clear();
                FrmReportViewer = null;

                PanelLoading.Visible = false;
            }
            else if (RBtnCutComparision.SelectedIndex == 2)
            {
                PanelLoading.Visible = false;

                FrmReportViewer FrmReportViewer = new FrmReportViewer();
                foreach (DataTable DTab in dtList.Tables)
                    FrmReportViewer.DS.Tables.Add(DTab.Copy());
                FrmReportViewer.GroupBy = "";
                FrmReportViewer.RepName = "";
                FrmReportViewer.RepPara = "";
                this.Cursor = Cursors.Default;
                FrmReportViewer.AllowSetFormula = true;

                //FrmReportViewer.ShowForm_SubReport("CrtPolishGrading_Final_Main_Print2", 120, FrmReportViewer.ReportFolder.ACCOUNT);
                FrmReportViewer.ShowForm_SubReport("CrtPolishGrading_Final_Difference_Main_Print3", 120, FrmReportViewer.ReportFolder.FINAL_DIFFERENCE_CUT);

                DTab_IssueJanged = null;
                FrmReportViewer.DS.Tables.Clear();
                FrmReportViewer.DS.Clear();
                FrmReportViewer = null;

                PanelLoading.Visible = false;
            }
            else if (RBtnCutComparision.SelectedIndex == 3)
            {
                PanelLoading.Visible = false;

                FrmReportViewer FrmReportViewer = new FrmReportViewer();
                foreach (DataTable DTab in dtList.Tables)
                    FrmReportViewer.DS.Tables.Add(DTab.Copy());
                FrmReportViewer.GroupBy = "";
                FrmReportViewer.GroupBy = "";
                FrmReportViewer.RepName = "";
                FrmReportViewer.RepPara = "";
                this.Cursor = Cursors.Default;
                FrmReportViewer.AllowSetFormula = true;

                FrmReportViewer.ShowForm_SubReport("Cut_Comparison_New", 120, FrmReportViewer.ReportFolder.CUT_COMPARISON_DIFF_SUMMARY);

                DTab_IssueJanged = null;
                FrmReportViewer.DS.Tables.Clear();
                FrmReportViewer.DS.Clear();
                FrmReportViewer = null;

                PanelLoading.Visible = false;
            }
            else if (RBtnCutComparision.SelectedIndex == 4)
            {
                if (RBtnLocationType.EditValue.ToString() == "1")
                {
                    PanelLoading.Visible = false;

                    FrmReportViewer FrmReportViewer = new FrmReportViewer();
                    foreach (DataTable DTab in dtList.Tables)
                        FrmReportViewer.DS.Tables.Add(DTab.Copy());
                    FrmReportViewer.GroupBy = "";
                    FrmReportViewer.GroupBy = "";
                    FrmReportViewer.RepName = "";
                    FrmReportViewer.RepPara = "";
                    this.Cursor = Cursors.Default;
                    FrmReportViewer.AllowSetFormula = true;

                    FrmReportViewer.ShowForm_SubReport("Cut_Comparison_New", 120, FrmReportViewer.ReportFolder.CUT_COMPARISON_DIFF);

                    DTab_IssueJanged = null;
                    FrmReportViewer.DS.Tables.Clear();
                    FrmReportViewer.DS.Clear();
                    FrmReportViewer = null;

                    PanelLoading.Visible = false;
                }
                else if (RBtnLocationType.EditValue.ToString() == "2")
                {
                    PanelLoading.Visible = false;

                    FrmReportViewer FrmReportViewer = new FrmReportViewer();
                    foreach (DataTable DTab in dtList.Tables)
                        FrmReportViewer.DS.Tables.Add(DTab.Copy());
                    FrmReportViewer.GroupBy = "";
                    FrmReportViewer.GroupBy = "";
                    FrmReportViewer.RepName = "";
                    FrmReportViewer.RepPara = "";
                    this.Cursor = Cursors.Default;
                    FrmReportViewer.AllowSetFormula = true;

                    FrmReportViewer.ShowForm_SubReport("Cut_Comparison_New", 120, FrmReportViewer.ReportFolder.MUMBAI_CUT_COMPARISON_DIFF);

                    DTab_IssueJanged = null;
                    FrmReportViewer.DS.Tables.Clear();
                    FrmReportViewer.DS.Clear();
                    FrmReportViewer = null;

                    PanelLoading.Visible = false;
                }
            }
        }
    }
}
