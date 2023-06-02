using BLL;
using BLL.FunctionClasses.Report;
using BLL.PropertyClasses.Report;
using DERP.Class;
using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Windows.Forms;

namespace DERP.Report
{
    public partial class FrmMFGMachineItemReport : DevExpress.XtraEditors.XtraForm
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
        DataTable m_dtbTime = new DataTable();
        Control _NextEnteredControl = new Control();
        ReportParams_Property ReportParams_Property = new BLL.PropertyClasses.Report.ReportParams_Property();
        string StrType = string.Empty;
        string StrTransactionType = string.Empty;
        #endregion

        #region Counstructor

        public FrmMFGMachineItemReport()
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
            //RbtReportType_EditValueChanged(null, null);
            FillListControls();
            m_opDate = Global.GetDate();
            DTPFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
            //string From_Date = Val.DBDate(m_opDate.Rows[0]["opening_date"].ToString());
            var today = DateTime.Now;
            var From_Date = new DateTime(today.Year, today.Month, 1);
            if (From_Date != null)
            {
                DTPFromDate.EditValue = From_Date;
            }
            else
            {
                DTPFromDate.EditValue = DateTime.Now;
            }

            DTPToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPToDate.Properties.CharacterCasing = CharacterCasing.Upper;

            DTPFromInsatallDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPFromInsatallDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPFromInsatallDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPFromInsatallDate.Properties.CharacterCasing = CharacterCasing.Upper;
            DTPFromInsatallDate.EditValue = From_Date;

            DTPToInstallDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPToInstallDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPToInstallDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPToInstallDate.Properties.CharacterCasing = CharacterCasing.Upper;
            DTPToInstallDate.EditValue = DateTime.Now;
            m_dtbTime.Columns.Add("time");
            m_dtbTime.Columns.Add("value");
            m_dtbTime.Rows.Add("01:00:00", "01:00 AM");
            m_dtbTime.Rows.Add("02:00:00", "02:00 AM");
            m_dtbTime.Rows.Add("03:00:00", "03:00 AM");
            m_dtbTime.Rows.Add("04:00:00", "04:00 AM");
            m_dtbTime.Rows.Add("05:00:00", "05:00 AM");
            m_dtbTime.Rows.Add("06:00:00", "06:00 AM");
            m_dtbTime.Rows.Add("07:00:00", "07:00 AM");
            m_dtbTime.Rows.Add("08:00:00", "08:00 AM");
            m_dtbTime.Rows.Add("09:00:00", "09:00 AM");
            m_dtbTime.Rows.Add("10:00:00", "10:00 AM");
            m_dtbTime.Rows.Add("11:00:00", "11:00 AM");
            m_dtbTime.Rows.Add("12:00:00", "12:00 PM");
            m_dtbTime.Rows.Add("13:00:00", "01:00 PM");
            m_dtbTime.Rows.Add("14:00:00", "02:00 PM");
            m_dtbTime.Rows.Add("15:00:00", "03:00 PM");
            m_dtbTime.Rows.Add("16:00:00", "04:00 PM");
            m_dtbTime.Rows.Add("17:00:00", "05:00 PM");
            m_dtbTime.Rows.Add("18:00:00", "06:00 PM");
            m_dtbTime.Rows.Add("19:00:00", "07:00 PM");
            m_dtbTime.Rows.Add("20:00:00", "08:00 PM");
            m_dtbTime.Rows.Add("21:00:00", "09:00 PM");
            m_dtbTime.Rows.Add("22:00:00", "10:00 PM");
            m_dtbTime.Rows.Add("23:59:59", "11:00 PM");
            m_dtbTime.Rows.Add("00:00:00", "12:00 AM");
            m_dtbTime.Rows.Add("", "");

            lueFromTime.Properties.DataSource = m_dtbTime;
            lueFromTime.Properties.ValueMember = "time";
            lueFromTime.Properties.DisplayMember = "value";

            lueToTime.Properties.DataSource = m_dtbTime;
            lueToTime.Properties.ValueMember = "time";
            lueToTime.Properties.DisplayMember = "value";
            DTPToDate.EditValue = DateTime.Now;
            timeEdit1.EditValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 00, 00, 00);
            timeEdit2.EditValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 00);
            DTPFromDate.Focus();
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

            PanelLoading.Visible = true;
            ReportParams_Property.Group_By_Tag = ListGroupBy.GetTagValue;
            ReportParams_Property.From_Date = Val.DBDate(DTPFromDate.Text);
            ReportParams_Property.To_Date = Val.DBDate(DTPToDate.Text);
            ReportParams_Property.From_Install_Date = Val.DBDate(DTPFromDate.Text);
            ReportParams_Property.To_Install_Date = Val.DBDate(DTPToDate.Text);
            ReportParams_Property.From_Time = Val.ToString(lueFromTime.EditValue);
            ReportParams_Property.To_Time = Val.ToString(lueToTime.EditValue);
            ReportParams_Property.company_id = Val.Trim(ListCompany.Properties.GetCheckedItems());
            ReportParams_Property.branch_id = Val.Trim(ListBranch.Properties.GetCheckedItems());
            ReportParams_Property.location_id = Val.Trim(ListLocation.Properties.GetCheckedItems());
            //ReportParams_Property.department_id = Val.Trim(ListDepartment.Properties.GetCheckedItems());
            //ReportParams_Property.type = StrType;
            //ReportParams_Property.transaction_type = StrTransactionType;
            //ReportParams_Property.IsCurrent = Val.ToBoolean(chkIsCurrent.Checked);

            //ReportParams_Property.process_id = Val.Trim(ListProcess.Properties.GetCheckedItems());
            //ReportParams_Property.sub_process_id = Val.Trim(ListSubProcess.Properties.GetCheckedItems());
            //ReportParams_Property.to_department_id = Val.Trim(ListToDepartment.Properties.GetCheckedItems());
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
            DTab = ObjReportParams.GetMFGMachineItemData(ReportParams_Property, ObjReportDetailProperty.Procedure_Name);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (Val.ToString(ObjReportDetailProperty.Remark.ToUpper()) == "MFG_JANGED_PENDING_SUMMARY")
            {
                PanelLoading.Visible = false;

                FrmReportViewer FrmReportViewer = new FrmReportViewer();
                FrmReportViewer.DS.Tables.Add(DTab);
                FrmReportViewer.GroupBy = "";
                FrmReportViewer.RepName = "";
                FrmReportViewer.RepPara = "";
                this.Cursor = Cursors.Default;
                FrmReportViewer.AllowSetFormula = true;

                FrmReportViewer.ShowForm("Janged_Issue_Pending_Summary", 120, FrmReportViewer.ReportFolder.ACCOUNT);

                DTab = null;
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
                    DeTemp = DeTemp.Select("id in (" + temp + ")").CopyToDataTable();

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
                //ObjReportDetailProperty.Default_Group_By = ListGroupBy.GetTextValue;
                ListGroupBy.DTab = mDTDetail;
                ListGroupBy.Default = ObjReportDetailProperty.Default_Group_By;
            }
            else
            {
                ListGroupBy.DTab = mDTDetail;
                ListGroupBy.Default = ObjReportDetailProperty.Default_Group_By;
            }

            //if (ObjReportDetailProperty.Remark == "MIX_SPLIT_REPORT_FORMAT1" || ObjReportDetailProperty.Remark == "MIX_SPLIT_REPORT_DETAIL" || ObjReportDetailProperty.Remark == "FACTORY_MIX_SPLIT_REPORT")
            //{
            //    groupControl4.Visible = true;
            //    groupControl5.Visible = true;
            //}
            //else
            //{
            //    groupControl4.Visible = false;
            //    groupControl5.Visible = false;
            //}
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

                StrColumnName = "Department ";
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
                    DeTemp = DeTemp.Select("id in (" + temp + ")").CopyToDataTable();

                StrColumnName = "Party ";
                string StrFieldName = "Party_Name";
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
                    DeTemp = DeTemp.Select("id in (" + temp + ")").CopyToDataTable();

                StrColumnName = "Item ";
                string StrFieldName = "Item_Name";
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

            //ListCompany.SetEditValue(BLL.GlobalDec.gEmployeeProperty.company_id.ToString());
            //ListBranch.SetEditValue(BLL.GlobalDec.gEmployeeProperty.branch_id.ToString());
            //ListLocation.SetEditValue(BLL.GlobalDec.gEmployeeProperty.location_id.ToString());
            for (int i = 0; i < ListCompany.Properties.Items.Count; i++)
                ListCompany.Properties.Items[i].CheckState = CheckState.Unchecked;
            for (int i = 0; i < ListBranch.Properties.Items.Count; i++)
                ListBranch.Properties.Items[i].CheckState = CheckState.Unchecked;
            for (int i = 0; i < ListLocation.Properties.Items.Count; i++)
                ListLocation.Properties.Items[i].CheckState = CheckState.Unchecked;
            for (int i = 0; i < ListDepartment.Properties.Items.Count; i++)
                ListDepartment.Properties.Items[i].CheckState = CheckState.Unchecked;
            //for (int i = 0; i < ListToDepartment.Properties.Items.Count; i++)
            //    ListToDepartment.Properties.Items[i].CheckState = CheckState.Unchecked;
            //for (int i = 0; i < ListKapan.Properties.Items.Count; i++)
            //    ListKapan.Properties.Items[i].CheckState = CheckState.Unchecked;
            //for (int i = 0; i < ListRoughCut.Properties.Items.Count; i++)
            //    ListRoughCut.Properties.Items[i].CheckState = CheckState.Unchecked;
            //for (int i = 0; i < ListQuality.Properties.Items.Count; i++)
            //    ListQuality.Properties.Items[i].CheckState = CheckState.Unchecked;
            //for (int i = 0; i < ListClarity.Properties.Items.Count; i++)
            //    ListClarity.Properties.Items[i].CheckState = CheckState.Unchecked;
            //for (int i = 0; i < ListPurity.Properties.Items.Count; i++)
            //    ListPurity.Properties.Items[i].CheckState = CheckState.Unchecked;
            //for (int i = 0; i < ListSieve.Properties.Items.Count; i++)
            //    ListSieve.Properties.Items[i].CheckState = CheckState.Unchecked;
            //for (int i = 0; i < ListType.Properties.Items.Count; i++)
            //    ListType.Properties.Items[i].CheckState = CheckState.Unchecked;
            //for (int i = 0; i < ListTransactionType.Properties.Items.Count; i++)
            //    ListTransactionType.Properties.Items[i].CheckState = CheckState.Unchecked;
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

        #endregion

        #region "Functions"
        private void FillListControls()
        {
            ObjFillCombo.user_id = GlobalDec.gEmployeeProperty.user_id;

            DataTable DTabCompany = ObjFillCombo.FillCmb(FillCombo.TABLE.Item_Company);
            DTabCompany.DefaultView.Sort = "Company_Name";
            DTabCompany = DTabCompany.DefaultView.ToTable();

            ListCompany.Properties.DataSource = DTabCompany;
            ListCompany.Properties.DisplayMember = "Company_Name";
            ListCompany.Properties.ValueMember = "id";

            //ListCompany.SetEditValue(BLL.GlobalDec.gEmployeeProperty.company_id.ToString());

            //DataRow r = dtAllvalues.NewRow();
            //r[dc_Unique] = BLL.GlobalDec.gEmployeeProperty.company_name;
            //r[dc_Parent] = "Company ";
            //r[dc_Data] = BLL.GlobalDec.gEmployeeProperty.company_name;
            //dtAllvalues.Rows.Add(r);

            DataTable DTabBranch = ObjFillCombo.FillCmb(FillCombo.TABLE.Item_Party);
            DTabBranch.DefaultView.Sort = "Party_Name";
            DTabBranch = DTabBranch.DefaultView.ToTable();

            ListBranch.Properties.DataSource = DTabBranch;
            ListBranch.Properties.DisplayMember = "Party_Name";
            ListBranch.Properties.ValueMember = "id";

            DataTable DTabLocation = ObjFillCombo.FillCmb(FillCombo.TABLE.Machine_Item);
            DTabLocation.DefaultView.Sort = "Item_Name";
            DTabLocation = DTabLocation.DefaultView.ToTable();

            ListLocation.Properties.DataSource = DTabLocation;
            ListLocation.Properties.DisplayMember = "Item_Name";
            ListLocation.Properties.ValueMember = "id";

            //r = dtAllvalues.NewRow();
            //r[dc_Unique] = BLL.GlobalDec.gEmployeeProperty.branch_name;
            //r[dc_Parent] = "Branch ";
            //r[dc_Data] = BLL.GlobalDec.gEmployeeProperty.branch_name;
            //dtAllvalues.Rows.Add(r);

            //ListLocation.SetEditValue(BLL.GlobalDec.gEmployeeProperty.location_id.ToString());

            //r = dtAllvalues.NewRow();
            //r[dc_Unique] = BLL.GlobalDec.gEmployeeProperty.location_name;
            //r[dc_Parent] = "Location ";
            //r[dc_Data] = BLL.GlobalDec.gEmployeeProperty.location_name;
            //dtAllvalues.Rows.Add(r);

            //treeView_Condition.ParentFieldName = "Parent";
            //treeView_Condition.KeyFieldName = "Unique";
            //treeView_Condition.RootValue = string.Empty;
            //treeView_Condition.DataSource = dtAllvalues;
            //treeView_Condition.ExpandAll();

            DataTable DTabProcess = ObjFillCombo.FillCmb(FillCombo.TABLE.Process_Master_New);
            DTabProcess.DefaultView.Sort = "process_name";
            DTabProcess = DTabProcess.DefaultView.ToTable();

            ListProcess.Properties.DataSource = DTabProcess;
            ListProcess.Properties.DisplayMember = "process_name";
            ListProcess.Properties.ValueMember = "process_id";

            DataTable DTabSubProcess = ObjFillCombo.FillCmb(FillCombo.TABLE.Sub_Process_Master);
            DTabSubProcess.DefaultView.Sort = "sub_process_name";
            DTabSubProcess = DTabSubProcess.DefaultView.ToTable();

            ListSubProcess.Properties.DataSource = DTabSubProcess;
            ListSubProcess.Properties.DisplayMember = "sub_process_name";
            ListSubProcess.Properties.ValueMember = "sub_process_id";
            
            
        }

        private string GetFilterByValue()
        {
            string Str = "Filter By : ";

            if (DTPFromDate.Text != "")
            {
                Str += "From Pur. Date : " + DTPFromDate.Text;
            }
            if (DTPToDate.Text != "")
            {
                Str += " & To Pur. Date : " + DTPToDate.Text;
            }

            if (DTPFromInsatallDate.Text != "")
            {
                Str += "From Inst. Date : " + DTPFromInsatallDate.Text;
            }
            if (DTPToInstallDate.Text != "")
            {
                Str += " & To Inst. Date : " + DTPToInstallDate.Text;
            }

            if (ListCompany.Text.Length > 0)
            {
                Str += ", Company : " + ListCompany.Text.ToString();
            }
            if (ListBranch.Text.Length > 0)
            {
                Str += ", Party : " + ListBranch.Text.ToString();
            }
            if (ListLocation.Text.Length > 0)
            {
                Str += ", Item : " + ListLocation.Text.ToString();
            }
            
            //if (ListToDepartment.Text.Length > 0)
            //{
            //    Str += ", To Dept : " + ListToDepartment.Text.ToString();
            //}
            //if (ListKapan.Text.Length > 0)
            //{
            //    Str += ", Kapan : " + ListKapan.Text.ToString();
            //}
            //if (ListRoughCut.Text.Length > 0)
            //{
            //    Str += ", Cut No : " + ListRoughCut.Text.ToString();
            //}
            //if (ListProcess.Text.Length > 0)
            //{
            //    Str += ", Process : " + ListProcess.Text.ToString();
            //}
            //if (ListSubProcess.Text.Length > 0)
            //{
            //    Str += ", Sub Process : " + ListSubProcess.Text.ToString();
            //}
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

            //if (ObjReportMasterProperty.Remark == "RPT_Live_Stock_With_Rejection")
            //{
            //    chkPivot.Checked = false;
            //}
        }

        #endregion

        private void ListType_EditValueChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    DataTable DeTemp = new DataTable();
            //    var temp1 = "";
            //    if (ObjReportDetailProperty.Remark == "FACTORY_MIX_SPLIT_REPORT")
            //    {
            //        temp1 = ListType.Properties.GetCheckedItems().ToString();
            //    }
            //    else
            //    {
            //        temp1 = ListType.Properties.GetCheckedItems().ToString().Replace(" ", "").Replace("  ", "").Trim();
            //    }

            //    StrType = "";
            //    string[] array = temp1.Split(',');
            //    if (!string.IsNullOrEmpty(temp1))
            //    {
            //        foreach (var item in array)
            //        {
            //            StrType += "'" + item + "',";
            //        }
            //        StrType = StrType.Remove(StrType.Length - 1);
            //    }
            //    if (StrType != "")
            //        DeTemp = (DataTable)ListType.Properties.DataSource;

            //    if (StrType.Length > 0)
            //        DeTemp = DeTemp.Select("Type1 in (" + StrType + ")").CopyToDataTable();

            //    StrColumnName = "Type ";
            //    string StrFieldName = "Type";
            //    TreeView_List(DeTemp, StrColumnName, StrFieldName);
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }
        private void ListTransactionType_EditValueChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    DataTable DeTemp = new DataTable();
            //    var temp1 = ListTransactionType.Properties.GetCheckedItems().ToString().Replace(" ", "").Replace("  ", "").Trim();
            //    StrTransactionType = "";
            //    string[] array = temp1.Split(',');
            //    if (!string.IsNullOrEmpty(temp1))
            //    {
            //        foreach (var item in array)
            //        {
            //            StrTransactionType += "'" + item + "',";
            //        }
            //        StrTransactionType = StrTransactionType.Remove(StrTransactionType.Length - 1);
            //    }
            //    if (StrTransactionType != "")
            //        DeTemp = (DataTable)ListTransactionType.Properties.DataSource;

            //    if (StrTransactionType.Length > 0)
            //        DeTemp = DeTemp.Select("Transaction_Type1 in (" + StrTransactionType + ")").CopyToDataTable();

            //    StrColumnName = "Transaction Type ";
            //    string StrFieldName = "Transaction_Type";
            //    TreeView_List(DeTemp, StrColumnName, StrFieldName);
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        private void ListToDepartment_EditValueChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    DataTable DeTemp = new DataTable();
            //    var temp = ListToDepartment.Properties.GetCheckedItems().ToString().Replace(" ", "").Replace("  ", "").Trim();
            //    if (temp != "")
            //        DeTemp = (DataTable)ListToDepartment.Properties.DataSource;

            //    if (temp.Length > 0)
            //        DeTemp = DeTemp.Select("department_id in (" + temp + ")").CopyToDataTable();

            //    StrColumnName = "To Department ";
            //    string StrFieldName = "department_name";
            //    TreeView_List(DeTemp, StrColumnName, StrFieldName);
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        private void ListKapan_EditValueChanged(object sender, EventArgs e)
        {
            //DataTable DTabCut = Global.GetReportKapanWise(Val.ToString(ListKapan.EditValue));
            //DTabCut.DefaultView.Sort = "rough_cut_id";
            //DTabCut = DTabCut.DefaultView.ToTable();

            //ListRoughCut.Properties.DataSource = DTabCut;
            //ListRoughCut.Properties.DisplayMember = "rough_cut_no";
            //ListRoughCut.Properties.ValueMember = "rough_cut_id";

            //try
            //{
            //    DataTable DeTemp = new DataTable();
            //    var temp = ListKapan.Properties.GetCheckedItems().ToString().Replace(" ", "").Replace("  ", "").Trim();
            //    if (temp != "")
            //        DeTemp = (DataTable)ListKapan.Properties.DataSource;

            //    if (temp.Length > 0)
            //        DeTemp = DeTemp.Select("kapan_id in (" + temp + ")").CopyToDataTable();

            //    StrColumnName = "Kapan ";
            //    string StrFieldName = "kapan_no";
            //    TreeView_List(DeTemp, StrColumnName, StrFieldName);
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
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

        private void ListRoughCut_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                //DataTable DeTemp = new DataTable();
                //var temp = ListRoughCut.Properties.GetCheckedItems().ToString().Replace(" ", "").Replace("  ", "").Trim();
                //if (temp != "")
                //    DeTemp = (DataTable)ListRoughCut.Properties.DataSource;

                //if (temp.Length > 0)
                //    DeTemp = DeTemp.Select("rough_cut_id in (" + temp + ")").CopyToDataTable();

                //StrColumnName = "Rough Cut ";
                //string StrFieldName = "rough_cut_no";
                //TreeView_List(DeTemp, StrColumnName, StrFieldName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ListQuality_EditValueChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    DataTable DeTemp = new DataTable();
            //    var temp = ListQuality.Properties.GetCheckedItems().ToString().Replace(" ", "").Replace("  ", "").Trim();
            //    if (temp != "")
            //        DeTemp = (DataTable)ListQuality.Properties.DataSource;

            //    if (temp.Length > 0)
            //        DeTemp = DeTemp.Select("quality_id in (" + temp + ")").CopyToDataTable();

            //    StrColumnName = "Quality ";
            //    string StrFieldName = "quality_name";
            //    TreeView_List(DeTemp, StrColumnName, StrFieldName);
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        private void ListSieve_EditValueChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    DataTable DeTemp = new DataTable();
            //    var temp = ListSieve.Properties.GetCheckedItems().ToString().Replace(" ", "").Replace("  ", "").Trim();
            //    if (temp != "")
            //        DeTemp = (DataTable)ListSieve.Properties.DataSource;

            //    if (temp.Length > 0)
            //        DeTemp = DeTemp.Select("rough_sieve_id in (" + temp + ")").CopyToDataTable();

            //    StrColumnName = "Rough Sieve ";
            //    string StrFieldName = "sieve_name";
            //    TreeView_List(DeTemp, StrColumnName, StrFieldName);
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        private void ListClarity_EditValueChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    DataTable DeTemp = new DataTable();
            //    var temp = ListClarity.Properties.GetCheckedItems().ToString().Replace(" ", "").Replace("  ", "").Trim();
            //    if (temp != "")
            //        DeTemp = (DataTable)ListClarity.Properties.DataSource;

            //    if (temp.Length > 0)
            //        DeTemp = DeTemp.Select("rough_clarity_id in (" + temp + ")").CopyToDataTable();

            //    StrColumnName = "Rough Clarity ";
            //    string StrFieldName = "rough_clarity_name";
            //    TreeView_List(DeTemp, StrColumnName, StrFieldName);
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        private void ListPurity_EditValueChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    DataTable DeTemp = new DataTable();
            //    var temp = ListPurity.Properties.GetCheckedItems().ToString().Replace(" ", "").Replace("  ", "").Trim();
            //    if (temp != "")
            //        DeTemp = (DataTable)ListPurity.Properties.DataSource;

            //    if (temp.Length > 0)
            //        DeTemp = DeTemp.Select("purity_id in (" + temp + ")").CopyToDataTable();

            //    StrColumnName = "Purity ";
            //    string StrFieldName = "purity_name";
            //    TreeView_List(DeTemp, StrColumnName, StrFieldName);
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }
    }
}
