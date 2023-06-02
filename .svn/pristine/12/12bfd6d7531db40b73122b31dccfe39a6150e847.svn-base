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
    public partial class FrmMFGProcessReport : DevExpress.XtraEditors.XtraForm
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

        public FrmMFGProcessReport()
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
            ReportParams_Property.From_Time = Val.ToString(lueFromTime.EditValue);
            ReportParams_Property.To_Time = Val.ToString(lueToTime.EditValue);
            ReportParams_Property.department_id = Val.Trim(ListDepartment.Properties.GetCheckedItems());

            ReportParams_Property.process_id = Val.Trim(ListProcess.Properties.GetCheckedItems());
            ReportParams_Property.sub_process_id = Val.Trim(ListSubProcess.Properties.GetCheckedItems());
            ReportParams_Property.kapan_id = Val.Trim(ListKapan.Properties.GetCheckedItems());
            ReportParams_Property.cut_id = Val.Trim(ListRoughCut.Properties.GetCheckedItems());

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
            DTab = ObjReportParams.GetMFGProcessData(ReportParams_Property, ObjReportDetailProperty.Procedure_Name);
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
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void BtnReset_Click(object sender, EventArgs e)
        {

            chkPivot.Checked = false;

            for (int i = 0; i < ListDepartment.Properties.Items.Count; i++)
                ListDepartment.Properties.Items[i].CheckState = CheckState.Unchecked;
            for (int i = 0; i < ListKapan.Properties.Items.Count; i++)
                ListKapan.Properties.Items[i].CheckState = CheckState.Unchecked;
            for (int i = 0; i < ListRoughCut.Properties.Items.Count; i++)
                ListRoughCut.Properties.Items[i].CheckState = CheckState.Unchecked;
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

            DataTable DTabDepartment = ObjFillCombo.FillCmb(FillCombo.TABLE.Department_Master);
            DTabDepartment.DefaultView.Sort = "Department_Name";
            DTabDepartment = DTabDepartment.DefaultView.ToTable();

            ListDepartment.Properties.DataSource = DTabDepartment;
            ListDepartment.Properties.DisplayMember = "Department_Name";
            ListDepartment.Properties.ValueMember = "department_id";

            ListDepartment.SetEditValue(BLL.GlobalDec.gEmployeeProperty.department_id);

            DataTable DTabKapan = ObjFillCombo.FillCmb(FillCombo.TABLE.Kapan_Master);
            DTabKapan.DefaultView.Sort = "kapan_id";
            DTabKapan = DTabKapan.DefaultView.ToTable();

            ListKapan.Properties.DataSource = DTabKapan;
            ListKapan.Properties.DisplayMember = "kapan_no";
            ListKapan.Properties.ValueMember = "kapan_id";

            DataTable DTabCut = ObjFillCombo.FillCmb(FillCombo.TABLE.Cut_Master);
            DTabCut.DefaultView.Sort = "rough_cut_id";
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

            DataTable DTabSubProcess = ObjFillCombo.FillCmb(FillCombo.TABLE.Sub_Process_Master);
            DTabSubProcess.DefaultView.Sort = "sub_process_name";
            DTabSubProcess = DTabSubProcess.DefaultView.ToTable();

            ListSubProcess.Properties.DataSource = DTabSubProcess;
            ListSubProcess.Properties.DisplayMember = "sub_process_name";
            ListSubProcess.Properties.ValueMember = "sub_process_id";

            treeView_Condition.ParentFieldName = "Parent";
            treeView_Condition.KeyFieldName = "Unique";
            treeView_Condition.RootValue = string.Empty;
            treeView_Condition.DataSource = dtAllvalues;
            treeView_Condition.ExpandAll();
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
            if (ListDepartment.Text.Length > 0)
            {
                Str += ", Department : " + ListDepartment.Text.ToString();
            }
            if (ListKapan.Text.Length > 0)
            {
                Str += ", Kapan : " + ListKapan.Text.ToString();
            }
            if (ListRoughCut.Text.Length > 0)
            {
                Str += ", Cut No : " + ListRoughCut.Text.ToString();
            }
            if (ListProcess.Text.Length > 0)
            {
                Str += ", Process : " + ListProcess.Text.ToString();
            }
            if (ListSubProcess.Text.Length > 0)
            {
                Str += ", Sub Process : " + ListSubProcess.Text.ToString();
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
                DataTable DeTemp = new DataTable();
                var temp = ListRoughCut.Properties.GetCheckedItems().ToString().Replace(" ", "").Replace("  ", "").Trim();
                if (temp != "")
                    DeTemp = (DataTable)ListRoughCut.Properties.DataSource;

                if (temp.Length > 0)
                    DeTemp = DeTemp.Select("rough_cut_id in (" + temp + ")").CopyToDataTable();

                StrColumnName = "Rough Cut ";
                string StrFieldName = "rough_cut_no";
                TreeView_List(DeTemp, StrColumnName, StrFieldName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
