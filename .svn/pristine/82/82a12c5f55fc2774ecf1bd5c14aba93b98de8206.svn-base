using BLL;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Report;
using BLL.PropertyClasses.Report;
using DERP.Class;
using System;
using System.Data;
using System.Windows.Forms;
namespace DERP.Report
{
    public partial class FrmLoanAdvanceReport : DevExpress.XtraEditors.XtraForm
    {
        BLL.FormEvents objBOFormEvents = new BLL.FormEvents();
        BLL.Validation Val = new BLL.Validation();
        string mStrReportGroup = string.Empty;
        string RptName = "";
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
        LedgerMaster objLedger = new LedgerMaster();
        DataTable m_opDate = new DataTable();
        string StrColumnName = string.Empty;
        string StrTransaction_Type = "";
        ReportParams_Property ReportParams_Property = new BLL.PropertyClasses.Report.ReportParams_Property();
        DataTable DTab;

        #region Counstructor

        public FrmLoanAdvanceReport()
        {
            InitializeComponent();
        }

        public void ShowForm(string pStrReportGroup)
        {
            mStrReportGroup = pStrReportGroup;
            Val.frmGenSet(this);
            AttachFormEvents();
            RptName = pStrReportGroup;

            this.Show();
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

        #region Functions

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

            DataTable DTabLedger = objLedger.GetData();
            DTabLedger.DefaultView.Sort = "ledger_name";
            DTabLedger = DTabLedger.DefaultView.ToTable();

            ListLedger.Properties.DataSource = DTabLedger;
            ListLedger.Properties.DisplayMember = "ledger_name";
            ListLedger.Properties.ValueMember = "ledger_id";

            DataTable DTabTransactionType = ObjFillCombo.DTab_Transaction_Type();
            DTabTransactionType.DefaultView.Sort = "Transaction_Type";
            DTabTransactionType = DTabTransactionType.DefaultView.ToTable();

            ListTransactionType.Properties.DataSource = DTabTransactionType;
            ListTransactionType.Properties.DisplayMember = "Transaction_Type";
            ListTransactionType.Properties.ValueMember = "Transaction_Type";
        }
        private string GetFilterByValue()
        {
            string Str = "Filter By : ";

            if (DTPTranFromDate.Text != "")
            {
                Str += "From Issue Date : " + DTPTranFromDate.Text;
            }
            if (DTPTranToDate.Text != "")
            {
                Str += " & As On Date : " + DTPTranToDate.Text;
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

        #endregion

        #region Events

        private void btnClear_Click(object sender, EventArgs e)
        {
            DTPTranFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPTranFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPTranFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPTranFromDate.Properties.CharacterCasing = CharacterCasing.Upper;

            DTPTranToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPTranToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPTranToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPTranToDate.Properties.CharacterCasing = CharacterCasing.Upper;

            DTPTranFromDate.EditValue = DateTime.Now;
            DTPTranToDate.EditValue = DateTime.Now;

            for (int i = 0; i < ListLedger.Properties.Items.Count; i++)
                ListLedger.Properties.Items[i].CheckState = CheckState.Unchecked;
            for (int i = 0; i < ListTransactionType.Properties.Items.Count; i++)
                ListTransactionType.Properties.Items[i].CheckState = CheckState.Unchecked;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void BtnGenerateReport_Click(object sender, EventArgs e)
        {
            var result = DateTime.Compare(Convert.ToDateTime(DTPTranFromDate.Text), Convert.ToDateTime(DTPTranToDate.Text.ToString()));
            if (result > 0)
            {
                Global.Message(" Transaction From Date Not Be Greater Than Transaction To Date");
                return;
            }
            PanelLoading.Visible = true;

            ReportParams_Property.Group_By_Tag = ListGroupBy.GetTagValue;
            ReportParams_Property.From_Date = Val.DBDate(DTPTranFromDate.Text);
            ReportParams_Property.To_Date = Val.DBDate(DTPTranToDate.Text);
            ReportParams_Property.ledger_id = Val.Trim(ListLedger.Properties.GetCheckedItems());
            ReportParams_Property.Cash_Type = StrTransaction_Type;
            ReportParams_Property.company_id = Val.Trim(ListCompany.Properties.GetCheckedItems());
            ReportParams_Property.branch_id = Val.Trim(ListBranch.Properties.GetCheckedItems());
            ReportParams_Property.location_id = Val.Trim(ListLocation.Properties.GetCheckedItems());
            ReportParams_Property.department_id = Val.Trim(ListDepartment.Properties.GetCheckedItems());

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
            decimal numBalance = 0;
            DTab = ObjReportParams.Get_Transaction_View_Report(ReportParams_Property, ObjReportDetailProperty.Procedure_Name);

            try
            {
                if (Val.ToString(ObjReportDetailProperty.Remark) == "Daily Ledger Book")
                {
                    foreach (DataRow Drw in DTab.Rows)
                    {
                        numBalance = numBalance + Val.ToDecimal(Drw["Credit_Amt"]) - Val.ToDecimal(Drw["Debit_Amt"]);
                        Drw["Balance"] = numBalance;
                    }
                }

                if (Val.Trim(ListLedger.Properties.GetCheckedItems()) != "")
                {
                    if (ObjReportDetailProperty.Remark == "Credit_Debit_Ledger_Book")
                    {
                        int count = DTab.Rows.Count;
                        foreach (DataRow Dr in DTab.Rows)
                        {
                            if (Val.ToDecimal(Dr["credit_amount"]) > 0 || Val.ToDecimal(Dr["debit_amount"]) > 0)
                            {
                            }
                            else
                            {
                                count--;
                                if (count > 0)
                                {
                                    Dr[0] = string.Empty;
                                    Dr.Delete();
                                }
                            }
                        }
                    }
                }
                if (Val.ToString(ObjReportDetailProperty.Remark) == "Payment_Pending_Remark")
                {
                    foreach (DataRow Drw in DTab.Rows)
                    {
                        numBalance = numBalance + Val.ToDecimal(Drw["opening_amount"]) + Val.ToDecimal(Drw["sale_amount"]) - Val.ToDecimal(Drw["receive_amount"]);
                        Drw["closing_amount"] = numBalance;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
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
        private void FrmReportList_Shown(object sender, EventArgs e)
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

            m_opDate = Global.GetDate();
            DTPTranFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPTranFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPTranFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPTranFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
            string From_Date = Val.DBDate(m_opDate.Rows[0]["opening_date"].ToString());
            if (From_Date != "")
            {
                DTPTranFromDate.EditValue = Val.DBDate(m_opDate.Rows[0]["opening_date"].ToString());
            }
            else
            {
                DTPTranFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                DTPTranFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                DTPTranFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                DTPTranFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
                DTPTranFromDate.EditValue = DateTime.Now;
            }

            DTPTranToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPTranToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPTranToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPTranToDate.Properties.CharacterCasing = CharacterCasing.Upper;

            DTPTranToDate.EditValue = DateTime.Now;
            chkPivot.Checked = true;
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
        private void ListLedger_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable DeTemp = new DataTable();
                var temp = ListLedger.Properties.GetCheckedItems().ToString().Replace(" ", "").Replace("  ", "").Trim();
                if (temp != "")
                    DeTemp = (DataTable)ListLedger.Properties.DataSource;

                if (temp.Length > 0)
                    DeTemp = DeTemp.Select("ledger_id in (" + temp + ")").CopyToDataTable();

                StrColumnName = "Ledger ";
                string StrFieldName = "ledger_name";
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
                StrTransaction_Type = "";
                string[] array = temp1.Split(',');
                if (!string.IsNullOrEmpty(temp1))
                {
                    foreach (var item in array)
                    {
                        StrTransaction_Type += "'" + item + "',";
                    }
                    StrTransaction_Type = StrTransaction_Type.Remove(StrTransaction_Type.Length - 1);
                }

                if (StrTransaction_Type != "")
                    DeTemp = (DataTable)ListTransactionType.Properties.DataSource;

                if (StrTransaction_Type.Length > 0)
                    DeTemp = DeTemp.Select("Transaction_Type in (" + StrTransaction_Type + ")").CopyToDataTable();

                StrColumnName = "Transaction Type ";
                string StrFieldName = "Transaction_Type";
                TreeView_List(DeTemp, StrColumnName, StrFieldName);
            }
            catch (Exception ex)
            {
                throw ex;
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
        private void ListDepartment_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable DeTemp = new DataTable();
                var temp = ListDepartment.Properties.GetCheckedItems().ToString().Replace(" ", "").Replace("  ", "").Trim();
                if (temp != "")
                    DeTemp = (DataTable)ListDepartment.Properties.DataSource;

                if (temp.Length > 0)
                    DeTemp = DeTemp.Select("department_id in (" + temp + ")").CopyToDataTable();

                StrColumnName = "Department ";
                string StrFieldName = "department_name";
                TreeView_List(DeTemp, StrColumnName, StrFieldName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
