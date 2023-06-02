using BLL;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Report;
using BLL.PropertyClasses.Master;
using BLL.PropertyClasses.Report;
using DERP.Class;
using DERP.DRPT;
using System;
using System.Data;
using System.Windows.Forms;

namespace DERP.Report
{
    public partial class FrmStockReport : DevExpress.XtraEditors.XtraForm
    {
        #region "Data Member"
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
        DataSet DSetProfitLoss = new DataSet();
        DataSet DSMontlySale = new DataSet();
        RateTypeMaster ObjRateType = new RateTypeMaster();
        DataSet DTab_DemandNotingRangeWise = new DataSet();
        #endregion

        #region Counstructor

        public FrmStockReport()
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
            //RbtReportType_EditValueChanged(null, null);
            FillListControls();
            m_opDate = Global.GetDate();
            DTPFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPFromDate.Properties.CharacterCasing = CharacterCasing.Upper;

            if (ObjReportDetailProperty.Remark.ToUpper() == "DEMAND_NOTING_REPORT")
            {
                DTPFromDate.EditValue = DateTime.Now;
            }
            else
            {
                string From_Date = Val.DBDate(m_opDate.Rows[0]["opening_date"].ToString());
                if (From_Date != "")
                {
                    DTPFromDate.EditValue = Val.DBDate(m_opDate.Rows[0]["opening_date"].ToString());
                }
                else
                {
                    DTPFromDate.EditValue = DateTime.Now;
                }
            }

            DTPToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPToDate.Properties.CharacterCasing = CharacterCasing.Upper;

            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "DEMAND_NOTING_REPORT")
            {
                chkPivot.Checked = false;
            }

            DTPToDate.EditValue = DateTime.Now;

            dtpPriceDate.Properties.Items.Clear();

            RateType_MasterProperty RateTypeProperty = new RateType_MasterProperty();
            RateTypeProperty.ratetype_id = Val.ToInt(2);
            DataTable DTab = ObjRateType.GetDate(RateTypeProperty);

            if (DTab.Rows.Count > 0)
            {
                foreach (DataRow DRow in DTab.Rows)
                {
                    dtpPriceDate.Properties.Items.Add(Convert.ToDateTime(DRow[0]).ToString("dd/MMM/yyyy"));
                }
                if (dtpPriceDate.Properties.Items.Count >= 1)
                {
                    dtpPriceDate.SelectedIndex = 0;
                }
            }
            else
            {
                dtpPriceDate.Properties.Items.Clear();
                dtpPriceDate.EditValue = null;
            }

            DTPFromDate.Focus();
        }
        private void BtnGenerateReport_Click(object sender, EventArgs e)
        {
            var result = DateTime.Compare(Convert.ToDateTime(DTPFromDate.Text), Convert.ToDateTime(DTPToDate.Text.ToString()));
            if (result > 0)
            {
                Global.Message(" Transaction From Date Not Be Greater Than Transaction To Date");
                return;
            }

            PanelLoading.Visible = true;
            ReportParams_Property.Group_By_Tag = ListGroupBy.GetTagValue;
            ReportParams_Property.From_Date = Val.DBDate(DTPFromDate.Text);
            ReportParams_Property.To_Date = Val.DBDate(DTPToDate.Text);
            ReportParams_Property.company_id = Val.Trim(ListCompany.Properties.GetCheckedItems());
            ReportParams_Property.branch_id = Val.Trim(ListBranch.Properties.GetCheckedItems());
            ReportParams_Property.location_id = Val.Trim(ListLocation.Properties.GetCheckedItems());
            ReportParams_Property.department_id = Val.Trim(ListDepartment.Properties.GetCheckedItems());
            ReportParams_Property.type = StrType;
            ReportParams_Property.transaction_type = StrTransactionType;
            ReportParams_Property.IsCurrent = Val.ToBoolean(chkIsCurrent.Checked);
            ReportParams_Property.price_date = Val.DBDate(dtpPriceDate.Text);

            //ReportParams_Property.process_id = Val.Trim(ListProcess.Properties.GetCheckedItems());
            //ReportParams_Property.sub_process_id = Val.Trim(ListSubProcess.Properties.GetCheckedItems());
            //ReportParams_Property.to_department_id = Val.Trim(ListToDepartment.Properties.GetCheckedItems());
            //ReportParams_Property.kapan_id = Val.Trim(ListKapan.Properties.GetCheckedItems());
            //ReportParams_Property.cut_id = Val.Trim(ListRoughCut.Properties.GetCheckedItems());
            //ReportParams_Property.quality_id = Val.Trim(ListQuality.Properties.GetCheckedItems());
            //ReportParams_Property.rough_clarity_id = Val.Trim(ListClarity.Properties.GetCheckedItems());
            //ReportParams_Property.rough_sieve_id = Val.Trim(ListSieve.Properties.GetCheckedItems());
            //ReportParams_Property.purity_id = Val.Trim(ListPurity.Properties.GetCheckedItems());

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
            if (Val.ToString(ObjReportDetailProperty.Remark) == "MIX_SPLIT_REPORT_DETAIL")
            {
                decimal from_amount = 0;
                decimal to_amount = 0;
                decimal to_carat = 0;

                int count = 0;

                DTab = ObjReportParams.GetLiveStock(ReportParams_Property, ObjReportDetailProperty.Procedure_Name);

                for (int i = 0; i < DTab.Rows.Count; i++)
                {
                    DataTable details = DTab.Select("mixsplit_srno = " + DTab.Rows[i]["mixsplit_srno"]).CopyToDataTable();

                    count = count + 1;

                    //if (Val.ToString(DTab.Rows[i]["transaction_type"]) == "Rejection")
                    //{
                    //    from_amount += Val.ToDecimal(DTab.Rows[i]["from_amount"]);
                    //    to_amount += Val.ToDecimal(DTab.Rows[i]["to_amount"]);
                    //}
                    //else if (Val.ToString(DTab.Rows[i]["transaction_type"]) == "")
                    //{
                    //    from_amount += Val.ToDecimal(DTab.Rows[i]["from_amount"]);
                    //    to_amount += Val.ToDecimal(DTab.Rows[i]["to_amount"]);
                    //}
                    //else
                    //{
                    //    from_amount = Val.ToDecimal(DTab.Rows[i]["from_amount"]);
                    //    to_amount += Val.ToDecimal(DTab.Rows[i]["to_amount"]);
                    //}

                    from_amount += Val.ToDecimal(DTab.Rows[i]["from_amount"]);
                    to_amount += Val.ToDecimal(DTab.Rows[i]["to_amount"]);
                    to_carat += Val.ToDecimal(DTab.Rows[i]["to_carat"]);


                    if (count == details.Rows.Count)
                    {
                        DTab.Rows[i]["diff_amount"] = Val.ToDecimal(to_amount - from_amount);
                        DTab.Rows[i]["diff_rate"] = Math.Round(Val.ToDecimal(to_amount - from_amount) / to_carat, 0);
                        DTab.Rows[i]["diff_per"] = Math.Round((Val.ToDecimal(to_amount - from_amount) / from_amount) * 100, 2);

                        count = 0;
                        from_amount = 0;
                        to_amount = 0;
                        to_carat = 0;
                    }
                }
            }
            else if (Val.ToString(ObjReportDetailProperty.Remark.ToUpper()) == "PROFIT_LOSS_REPORT")
            {
                DSetProfitLoss = ObjReportParams.GetProfitLossReport(ReportParams_Property, ObjReportDetailProperty.Procedure_Name);

                DataTable dtbSales = DSetProfitLoss.Tables[2];

                if (dtbSales.Rows.Count > 0)
                {
                    decimal numSalesAmount = Val.ToDecimal(dtbSales.Rows[0]["sales_amount"]);
                    string numExpAmount = "";
                    string numExpPer = "";

                    foreach (DataRow Drw in DSetProfitLoss.Tables[4].Rows)
                    {
                        numExpAmount = Val.ToString(Drw["amount"]);
                        numExpPer = Val.ToString(Math.Round((Val.ToDecimal(Drw["amount"]) / numSalesAmount) * 100, 3));

                        Drw["expper"] = Val.ToString(Val.ToDecimal(Drw["amount"]) + " (" + Val.ToString(numExpPer) + ")");
                    }
                }
            }
            else if (Val.ToString(ObjReportDetailProperty.Remark.ToUpper()) == "MONTH_WISE_SALE_DASHBOARD")
            {
                DSMontlySale = ObjReportParams.GetProfitLossReport(ReportParams_Property, ObjReportDetailProperty.Procedure_Name);
            }
            else if (Val.ToString(ObjReportDetailProperty.Remark.ToUpper()) == "DEMAND_NOTING_REPORT")
            {
                DTab = ObjReportParams.GetLiveStock(ReportParams_Property, ObjReportDetailProperty.Procedure_Name);

                //int total_count = DTab.Rows.Count;

                //int count = 0;

                //if (DTab.Rows.Count > 0)
                //{
                //    for (int i = 0; i < DTab.Rows.Count; i++)
                //    {
                //        string str = "mapColor = '" + DTab.Rows[i]["mapColor"] + "'" + " AND " + " sieve_name = '" + DTab.Rows[i]["sieve_name"] + "'" + " AND " + " size_group = '" + DTab.Rows[i]["size_group"] + "'";

                //        DataTable details = DTab.Select(str).CopyToDataTable();

                //        count = count + 1;

                //        if (count == details.Rows.Count)
                //        {
                //            double final_per = Math.Round(Val.ToDouble(Val.ToDouble(count) / Val.ToDouble(total_count)) * 100.00, 2);

                //            DTab.Rows[i]["final_per"] = final_per;

                //            count = 0;
                //        }
                //    }
                //}

                if (DTab.Rows.Count > 0)
                {

                }
            }
            else if (Val.ToString(ObjReportDetailProperty.Remark.ToUpper()) == "DEMAND_NOTING_RANGEWISE" || Val.ToString(ObjReportDetailProperty.Remark.ToUpper()) == "DEMAND_NOTING_DAHISAR_RANGEWISE")
            {
                DTab_DemandNotingRangeWise = new DataSet();
                DTab_DemandNotingRangeWise = ObjReportParams.GetDemandNotingData_RangeWise(ReportParams_Property, ObjReportDetailProperty.Procedure_Name);

                //DataView view_Plus2 = new DataView(DTab_DemandNotingRangeWise.Tables[0]);
                //DataTable distinctValuesPlus2 = view_Plus2.ToTable(true, "color_name");

                ////DataView view_Minus2 = new DataView(DTab_DemandNotingRangeWise.Tables[1]);
                ////DataTable distinctValuesMinus2 = view_Minus2.ToTable(true, "color_name");

                //for (int i = 0; i <= distinctValuesPlus2.Rows.Count - 1; i++)
                //{
                //    string Color_Plus2 = DTab_DemandNotingRangeWise.Tables[0].Rows[i]["color_name"].ToString();
                //    string Color_Minus2 = DTab_DemandNotingRangeWise.Tables[1].Rows[i]["color_name"].ToString();

                //    DataTable DTab_New_Plus2 = DTab_DemandNotingRangeWise.Tables[0].Select("color_name= '" + DTab_DemandNotingRangeWise.Tables[0].Rows[i]["color_name"].ToString() + "'").CopyToDataTable();
                //    DataTable DTab_New_Minus = DTab_DemandNotingRangeWise.Tables[1].Select("color_name= '" + DTab_DemandNotingRangeWise.Tables[1].Rows[i]["color_name"].ToString() + "'").CopyToDataTable();

                //}


                //if (DTab_DemandNotingRangeWise.Tables[0].Rows.Count > 0 && DTab_DemandNotingRangeWise.Tables[1].Rows.Count > 0)
                //{
                //    for (int i = 0; i < DTab_DemandNotingRangeWise.Tables[1].Rows.Count; i++)
                //    {
                //        string Color = DTab_DemandNotingRangeWise.Tables[1].Rows[i]["color_name"].ToString();

                //        DataTable DTab_New = DTab_DemandNotingRangeWise.Tables[1].Select("color_name= '" + DTab_DemandNotingRangeWise.Tables[1].Rows[i]["color_name"].ToString() + "'").CopyToDataTable();

                //        if (Color == DTab_New.Rows[0]["color_name"].ToString())
                //        {
                //            DTab_New = DTab_New.Select("sum(cnt)").CopyToDataTable();
                //            DTab_DemandNotingRangeWise.Tables[1].Rows[i]["cnt"] = DTab_New.Rows[0]["cnt"];
                //        }
                //    }
                //}

                //if (DTab_DemandNotingRangeWise.Tables[0].Rows.Count > 0 && DTab_DemandNotingRangeWise.Tables[1].Rows.Count > 0)
                //{
                //    for (int i = 0; i < DTab_DemandNotingRangeWise.Tables[0].Rows.Count; i++)
                //    {
                //        for (int j = 0; j < DTab_DemandNotingRangeWise.Tables[1].Rows.Count; j++)
                //        {

                //        }
                //    }
                //}
            }
            else
            {
                DTab = ObjReportParams.GetLiveStock(ReportParams_Property, ObjReportDetailProperty.Procedure_Name);
            }
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (Val.ToString(ObjReportDetailProperty.Remark.ToUpper()) == "PROFIT_LOSS_REPORT")
            {
                PanelLoading.Visible = false;

                FrmReportViewer FrmReportViewer = new FrmReportViewer();

                foreach (DataTable DTab in DSetProfitLoss.Tables)
                    FrmReportViewer.DS.Tables.Add(DTab.Copy());

                FrmReportViewer.GroupBy = "";
                FrmReportViewer.RepName = "";
                FrmReportViewer.RepPara = "";
                this.Cursor = Cursors.Default;
                FrmReportViewer.AllowSetFormula = true;

                FrmReportViewer.ShowForm_SubReport("Sales_Profit_Loss_Amount", 120, FrmReportViewer.ReportFolder.ACCOUNT);

                DTab = null;
                FrmReportViewer.DS.Tables.Clear();
                FrmReportViewer.DS.Clear();
                FrmReportViewer = null;
            }
            else if (Val.ToString(ObjReportDetailProperty.Remark.ToUpper()) == "DEMAND_NOTING_RANGEWISE")
            {
                PanelLoading.Visible = false;

                FrmReportViewer FrmReportViewer = new FrmReportViewer();
                foreach (DataTable DTab in DTab_DemandNotingRangeWise.Tables)
                    FrmReportViewer.DS.Tables.Add(DTab.Copy());
                //FrmReportViewer.DS.Tables.Add(DTab);
                FrmReportViewer.GroupBy = "";
                FrmReportViewer.RepName = "";
                FrmReportViewer.RepPara = "";
                this.Cursor = Cursors.Default;
                FrmReportViewer.AllowSetFormula = true;

                FrmReportViewer.ShowForm_SubReport("Demand_Noting_RangeWise", 120, FrmReportViewer.ReportFolder.ACCOUNT);

                DTab = null;
                FrmReportViewer.DS.Tables.Clear();
                FrmReportViewer.DS.Clear();
                FrmReportViewer = null;
            }
            else if (Val.ToString(ObjReportDetailProperty.Remark.ToUpper()) == "DEMAND_NOTING_DAHISAR_RANGEWISE")
            {
                PanelLoading.Visible = false;

                FrmReportViewer FrmReportViewer = new FrmReportViewer();
                foreach (DataTable DTab in DTab_DemandNotingRangeWise.Tables)
                    FrmReportViewer.DS.Tables.Add(DTab.Copy());
                //FrmReportViewer.DS.Tables.Add(DTab);
                FrmReportViewer.GroupBy = "";
                FrmReportViewer.RepName = "";
                FrmReportViewer.RepPara = "";
                this.Cursor = Cursors.Default;
                FrmReportViewer.AllowSetFormula = true;

                FrmReportViewer.ShowForm_SubReport("Demand_Noting_Dahisar_RangeWise", 120, FrmReportViewer.ReportFolder.ACCOUNT);

                DTab = null;
                FrmReportViewer.DS.Tables.Clear();
                FrmReportViewer.DS.Clear();
                FrmReportViewer = null;
            }
            else if (Val.ToString(ObjReportDetailProperty.Remark.ToUpper()) == "MONTH_WISE_SALE_DASHBOARD")
            {
                try
                {
                    XtraReportViewer frmRepViewer = new XtraReportViewer("Monthly Sale", "Monthly Sale Report", null, DSMontlySale);
                    frmRepViewer.MdiParent = this.MdiParent;
                    frmRepViewer.Show();
                }
                catch (Exception ex)
                {
                    Global.Message(ex.ToString());
                    return;
                }
                finally
                {
                    PanelLoading.Visible = false;
                    this.Cursor = Cursors.Default;
                    DSMontlySale = null;
                }
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

                    FrmGReportViewer.Group_By_Tag = ListGroupBy.GetTagValue;
                    FrmGReportViewer.Group_By_Text = ListGroupBy.GetTextValue;
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
                    FrmPReportViewer.Group_By_Tag = ListGroupBy.GetTagValue;
                    FrmPReportViewer.Group_By_Text = ListGroupBy.GetTextValue;
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
        private void ListCompany_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable DeTemp = new DataTable();
                var temp = ListCompany.Properties.GetCheckedItems().ToString().Replace(" ", "").Replace("  ", "").Trim();
                if (temp != "")
                    DeTemp = (DataTable)ListCompany.Properties.DataSource;

                if (temp.Length > 0)
                    DeTemp = DeTemp.Select("company_id in (" + temp + ")").CopyToDataTable();

                StrColumnName = "Company ";
                string StrFieldName = "company_name";
                TreeView_List(DeTemp, StrColumnName, StrFieldName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void RbtReportType_EditValueChanged(object sender, EventArgs e)
        {
            ObjReportDetailProperty = ObjReportMaster.GetReportDetailProperty(mIntReportCode, Val.ToString(RbtReportType.EditValue));
            mDTDetail = ObjReportMaster.GetDataForSearchSettings(mIntReportCode, Val.ToString(RbtReportType.EditValue));

            if (ListGroupBy.GetTextValue != "")
            {
                if (Val.ToString(RbtReportType.EditValue) == "ONLY SPLIT WITH LETEST")
                {
                    ListGroupBy.DTab = mDTDetail;
                    ListGroupBy.Default = ObjReportDetailProperty.Default_Group_By;
                    ObjReportDetailProperty.Default_Group_By = ListGroupBy.GetTextValue;
                    chkPivot.Checked = false;
                }
                else if (Val.ToString(RbtReportType.EditValue) == "SUMMARY" && ObjReportDetailProperty.Remark == "MIX_SPLIT_REPORT_DETAIL")
                {
                    ListGroupBy.DTab = mDTDetail;
                    ListGroupBy.Default = ObjReportDetailProperty.Default_Group_By;
                    chkPivot.Checked = true;
                }
                else
                {
                    ListGroupBy.DTab = mDTDetail;
                    ListGroupBy.Default = ObjReportDetailProperty.Default_Group_By;
                }
                // ObjReportDetailProperty.Default_Group_By = ListGroupBy.GetTextValue;
            }

            ListGroupBy.DTab = mDTDetail;
            ListGroupBy.Default = ObjReportDetailProperty.Default_Group_By;

            if (ObjReportDetailProperty.Remark == "MIX_SPLIT_REPORT_FORMAT1" || ObjReportDetailProperty.Remark == "MIX_SPLIT_REPORT_DETAIL" || ObjReportDetailProperty.Remark == "FACTORY_MIX_SPLIT_REPORT")
            {
                groupControl4.Visible = true;
                groupControl5.Visible = true;
            }
            else
            {
                groupControl4.Visible = false;
                groupControl5.Visible = false;
            }
        }
        private void ListFromDepartment_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable DeTemp = new DataTable();
                var temp = ListDepartment.Properties.GetCheckedItems().ToString().Replace(" ", "").Replace("  ", "").Trim();
                if (temp != "")
                    DeTemp = (DataTable)ListDepartment.Properties.DataSource;

                if (temp.Length > 0)
                    DeTemp = DeTemp.Select("department_id in (" + temp + ")").CopyToDataTable();

                StrColumnName = "From Department ";
                string StrFieldName = "department_name";
                TreeView_List(DeTemp, StrColumnName, StrFieldName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ListBranch_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable DeTemp = new DataTable();
                var temp = ListBranch.Properties.GetCheckedItems().ToString().Replace(" ", "").Replace("  ", "").Trim();
                if (temp != "")
                    DeTemp = (DataTable)ListBranch.Properties.DataSource;

                if (temp.Length > 0)
                    DeTemp = DeTemp.Select("branch_id in (" + temp + ")").CopyToDataTable();

                StrColumnName = "Branch ";
                string StrFieldName = "branch_name";
                TreeView_List(DeTemp, StrColumnName, StrFieldName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ListLocation_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable DeTemp = new DataTable();
                var temp = ListLocation.Properties.GetCheckedItems().ToString().Replace(" ", "").Replace("  ", "").Trim();
                if (temp != "")
                    DeTemp = (DataTable)ListLocation.Properties.DataSource;

                if (temp.Length > 0)
                    DeTemp = DeTemp.Select("Location_id in (" + temp + ")").CopyToDataTable();

                StrColumnName = "Location ";
                string StrFieldName = "location_name";
                TreeView_List(DeTemp, StrColumnName, StrFieldName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void BtnReset_Click(object sender, EventArgs e)
        {

            chkPivot.Checked = false;

            ListCompany.SetEditValue(BLL.GlobalDec.gEmployeeProperty.company_id.ToString());
            ListBranch.SetEditValue(BLL.GlobalDec.gEmployeeProperty.branch_id.ToString());
            ListLocation.SetEditValue(BLL.GlobalDec.gEmployeeProperty.location_id.ToString());
            for (int i = 0; i < ListDepartment.Properties.Items.Count; i++)
                ListDepartment.Properties.Items[i].CheckState = CheckState.Unchecked;
            for (int i = 0; i < ListType.Properties.Items.Count; i++)
                ListType.Properties.Items[i].CheckState = CheckState.Unchecked;
            for (int i = 0; i < ListTransactionType.Properties.Items.Count; i++)
                ListTransactionType.Properties.Items[i].CheckState = CheckState.Unchecked;

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
        private void ListType_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable DeTemp = new DataTable();
                var temp1 = "";
                if (ObjReportDetailProperty.Remark == "FACTORY_MIX_SPLIT_REPORT")
                {
                    temp1 = ListType.Properties.GetCheckedItems().ToString();
                }
                else
                {
                    temp1 = ListType.Properties.GetCheckedItems().ToString().Replace(" ", "").Replace("  ", "").Trim();
                }

                StrType = "";
                string[] array = temp1.Split(',');
                if (!string.IsNullOrEmpty(temp1))
                {
                    foreach (var item in array)
                    {
                        StrType += "'" + item + "',";
                    }
                    StrType = StrType.Remove(StrType.Length - 1);
                }
                if (StrType != "")
                    DeTemp = (DataTable)ListType.Properties.DataSource;

                if (StrType.Length > 0)
                    DeTemp = DeTemp.Select("Type1 in (" + StrType + ")").CopyToDataTable();

                StrColumnName = "Type ";
                string StrFieldName = "Type";
                TreeView_List(DeTemp, StrColumnName, StrFieldName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ListTransactionType_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable DeTemp = new DataTable();
                var temp1 = ListTransactionType.Properties.GetCheckedItems().ToString().Replace(" ", "").Replace("  ", "").Trim();
                StrTransactionType = "";
                string[] array = temp1.Split(',');
                if (!string.IsNullOrEmpty(temp1))
                {
                    foreach (var item in array)
                    {
                        StrTransactionType += "'" + item + "',";
                    }
                    StrTransactionType = StrTransactionType.Remove(StrTransactionType.Length - 1);
                }
                if (StrTransactionType != "")
                    DeTemp = (DataTable)ListTransactionType.Properties.DataSource;

                if (StrTransactionType.Length > 0)
                    DeTemp = DeTemp.Select("Transaction_Type1 in (" + StrTransactionType + ")").CopyToDataTable();

                StrColumnName = "Transaction Type ";
                string StrFieldName = "Transaction_Type";
                TreeView_List(DeTemp, StrColumnName, StrFieldName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region "Functions"
        private void FillListControls()
        {
            ObjFillCombo.user_id = GlobalDec.gEmployeeProperty.user_id;

            DataTable DTabCompany = ObjFillCombo.FillCmb(FillCombo.TABLE.Company_Master);
            DTabCompany.DefaultView.Sort = "Company_Name";
            DTabCompany = DTabCompany.DefaultView.ToTable();

            ListCompany.Properties.DataSource = DTabCompany;
            ListCompany.Properties.DisplayMember = "Company_Name";
            ListCompany.Properties.ValueMember = "company_id";

            ListCompany.SetEditValue(BLL.GlobalDec.gEmployeeProperty.company_id.ToString());

            DataRow r = dtAllvalues.NewRow();
            r[dc_Unique] = BLL.GlobalDec.gEmployeeProperty.company_name;
            r[dc_Parent] = "Company ";
            r[dc_Data] = BLL.GlobalDec.gEmployeeProperty.company_name;
            dtAllvalues.Rows.Add(r);

            DataTable DTabBranch = ObjFillCombo.FillCmb(FillCombo.TABLE.Branch_Master);
            DTabBranch.DefaultView.Sort = "Branch_Name";
            DTabBranch = DTabBranch.DefaultView.ToTable();

            ListBranch.Properties.DataSource = DTabBranch;
            ListBranch.Properties.DisplayMember = "Branch_Name";
            ListBranch.Properties.ValueMember = "branch_id";

            DataTable DTabLocation = ObjFillCombo.FillCmb(FillCombo.TABLE.Location_Master);
            DTabLocation.DefaultView.Sort = "Location_Name";
            DTabLocation = DTabLocation.DefaultView.ToTable();

            ListLocation.Properties.DataSource = DTabLocation;
            ListLocation.Properties.DisplayMember = "Location_Name";
            ListLocation.Properties.ValueMember = "location_id";

            DataTable DTabDepartment = ObjFillCombo.FillCmb(FillCombo.TABLE.Department_Master);
            DTabDepartment.DefaultView.Sort = "Department_Name";
            DTabDepartment = DTabDepartment.DefaultView.ToTable();

            ListDepartment.Properties.DataSource = DTabDepartment;
            ListDepartment.Properties.DisplayMember = "Department_Name";
            ListDepartment.Properties.ValueMember = "department_id";

            ListBranch.SetEditValue(BLL.GlobalDec.gEmployeeProperty.branch_id.ToString());
            ListDepartment.SetEditValue(BLL.GlobalDec.gEmployeeProperty.department_id);
            r = dtAllvalues.NewRow();
            r[dc_Unique] = BLL.GlobalDec.gEmployeeProperty.branch_name;
            r[dc_Parent] = "Branch ";
            r[dc_Data] = BLL.GlobalDec.gEmployeeProperty.branch_name;
            dtAllvalues.Rows.Add(r);

            ListLocation.SetEditValue(BLL.GlobalDec.gEmployeeProperty.location_id.ToString());

            r = dtAllvalues.NewRow();
            r[dc_Unique] = BLL.GlobalDec.gEmployeeProperty.location_name;
            r[dc_Parent] = "Location ";
            r[dc_Data] = BLL.GlobalDec.gEmployeeProperty.location_name;
            dtAllvalues.Rows.Add(r);

            treeView_Condition.ParentFieldName = "Parent";
            treeView_Condition.KeyFieldName = "Unique";
            treeView_Condition.RootValue = string.Empty;
            treeView_Condition.DataSource = dtAllvalues;
            treeView_Condition.ExpandAll();

            DataTable DTabType = Global.DTabType(ObjReportDetailProperty.Remark);
            DTabType.DefaultView.Sort = "Type";
            DTabType = DTabType.DefaultView.ToTable();

            ListType.Properties.DataSource = DTabType;
            ListType.Properties.DisplayMember = "Type";
            ListType.Properties.ValueMember = "Type1";

            DataTable DTabTransactionType = Global.DTabTransactionType();
            DTabTransactionType.DefaultView.Sort = "Transaction_Type";
            DTabTransactionType = DTabTransactionType.DefaultView.ToTable();

            ListTransactionType.Properties.DataSource = DTabTransactionType;
            ListTransactionType.Properties.DisplayMember = "Transaction_Type";
            ListTransactionType.Properties.ValueMember = "Transaction_Type1";
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

            if (ObjReportMasterProperty.Remark == "RPT_Live_Stock_With_Rejection")
            {
                chkPivot.Checked = false;
            }

        }

        #endregion      
    }
}
