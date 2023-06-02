using BLL;
using BLL.FunctionClasses.Report;
using BLL.FunctionClasses.Utility;
using BLL.PropertyClasses.Report;
using DERP.Class;
using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Windows.Forms;

namespace DERP.Report
{
    public partial class FrmMFGStockReport : DevExpress.XtraEditors.XtraForm
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
        Control _NextEnteredControl = new Control();
        ReportParams_Property ReportParams_Property = new BLL.PropertyClasses.Report.ReportParams_Property();
        string StrType = string.Empty;
        string StrTransactionType = string.Empty;
        DataTable DTForm = new DataTable();
        UserAuthentication objUser = new UserAuthentication();
        #endregion

        #region Counstructor

        public FrmMFGStockReport()
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

            DTPToDate.EditValue = DateTime.Now;
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
            ReportParams_Property.company_id = Val.Trim(ListCompany.Properties.GetCheckedItems());
            ReportParams_Property.branch_id = Val.Trim(ListBranch.Properties.GetCheckedItems());
            ReportParams_Property.location_id = Val.Trim(ListLocation.Properties.GetCheckedItems());
            ReportParams_Property.department_id = Val.Trim(ListFromDepartment.Properties.GetCheckedItems());
            ReportParams_Property.type = StrType;
            ReportParams_Property.transaction_type = StrTransactionType;
            ReportParams_Property.IsCurrent = Val.ToBoolean(chkIsCurrent.Checked);

            ReportParams_Property.process_id = Val.Trim(ListProcess.Properties.GetCheckedItems());
            ReportParams_Property.sub_process_id = Val.Trim(ListSubProcess.Properties.GetCheckedItems());
            ReportParams_Property.to_department_id = Val.Trim(ListToDepartment.Properties.GetCheckedItems());
            ReportParams_Property.kapan_id = Val.Trim(ListKapan.Properties.GetCheckedItems());
            ReportParams_Property.cut_id = Val.Trim(ListRoughCut.Properties.GetCheckedItems());
            ReportParams_Property.quality_id = Val.Trim(ListQuality.Properties.GetCheckedItems());
            ReportParams_Property.rough_clarity_id = Val.Trim(ListClarity.Properties.GetCheckedItems());
            ReportParams_Property.rough_sieve_id = Val.Trim(ListSieve.Properties.GetCheckedItems());
            ReportParams_Property.purity_id = Val.Trim(ListPurity.Properties.GetCheckedItems());
            ReportParams_Property.Department_Type = Val.ToString(CmbDeptType.Text);

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
                int count = 0;

                DTab = ObjReportParams.GetMFGLiveStock(ReportParams_Property, ObjReportDetailProperty.Procedure_Name);

                for (int i = 0; i < DTab.Rows.Count; i++)
                {
                    DataTable details = DTab.Select("mixsplit_srno = " + DTab.Rows[i]["mixsplit_srno"]).CopyToDataTable();

                    count = count + 1;

                    from_amount += Val.ToDecimal(DTab.Rows[i]["from_amount"]);
                    to_amount += Val.ToDecimal(DTab.Rows[i]["to_amount"]);

                    if (count == details.Rows.Count)
                    {
                        DTab.Rows[i]["diff_amount"] = Val.ToDecimal(to_amount - from_amount);
                        count = 0;
                        from_amount = 0;
                        to_amount = 0;
                    }
                }
            }
            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "MFG_JANGED_PENDING_SUMMARY")
            {
                DTab = ObjReportParams.GetMFGLiveStock(ReportParams_Property, ObjReportDetailProperty.Procedure_Name);
            }
            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "MFG_JANGED_RECIEVE_SUMMARY")
            {
                DTab = ObjReportParams.GetMFGLiveStock(ReportParams_Property, ObjReportDetailProperty.Procedure_Name);
            }
            else
            {
                DTab = ObjReportParams.GetMFGLiveStock(ReportParams_Property, ObjReportDetailProperty.Procedure_Name);
            }
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

                    //DTForm = objUser.Get_MENU_FormGetDetail(Val.ToInt(GlobalDec.gEmployeeProperty.role_id));
                    //Boolean Flag = false;

                    //try
                    //{
                    //    string ref_form_name = "";
                    //    Form k = Application.OpenForms["FRMGREPORTVIEWERBAND"];

                    //    if (k == null)
                    //    {
                    //        Assembly frmAssembly = Assembly.LoadFile(Application.ExecutablePath);
                    //        foreach (Type type in frmAssembly.GetTypes())
                    //        {
                    //            if (Flag)
                    //            {
                    //                break;
                    //            }

                    //            if (type.BaseType == typeof(DevExpress.XtraEditors.XtraForm))
                    //            {
                    //                if (type.Name.ToString().ToUpper() == "FRMGREPORTVIEWERBAND")
                    //                {
                    //                    XtraForm frmShow = (XtraForm)frmAssembly.CreateInstance(type.ToString());
                    //                    DERP.MDI.MDIMain main = new MDI.MDIMain();
                    //                    main.IsMdiContainer = true;

                    //                    FrmGReportViewer.MdiParent = this.MdiParent;

                    //                    //frmShow.IsMdiContainer = true;
                    //                    //frmShow.MdiParent = this

                    //                    // MDI.MDIMain main = md MDI.MDIMain();
                    //                    //frmShow.MdiParent = this;
                    //                    if (ref_form_name.Length > 0)
                    //                        FrmGReportViewer.Name = ref_form_name.ToUpper();
                    //                    if (DTForm.AsEnumerable().Where(m => m.Field<string>("form_name") == "FRMGREPORTVIEWERBAND" && m.Field<string>("caption") == "Report Viewer").CopyToDataTable().Rows[0]["param"].ToString().Length > 0)
                    //                    {
                    //                        string param = DTForm.AsEnumerable().Where(m => m.Field<string>("form_name") == "FRMSALESDASHBOARD" && m.Field<string>("caption") == "Report Viewer").CopyToDataTable().Rows[0]["param"].ToString();
                    //                        object[] obj1 = param.Split(',');
                    //                        FrmGReportViewer.GetType().GetMethod("ShowForm").Invoke(FrmGReportViewer, obj1);
                    //                        Flag = true;
                    //                    }
                    //                    else
                    //                    {
                    //                        FrmGReportViewer.GetType().GetMethod("ShowForm").Invoke(FrmGReportViewer, null);
                    //                        Flag = true;
                    //                    }
                    //                }
                    //            }
                    //        }
                    //    }
                    //    else
                    //    {
                    //        k.Activate();
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    MessageBox.Show(ex.ToString());
                    //}
                    //this.Cursor = Cursors.Default;
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
                ListGroupBy.DTab = mDTDetail;
                ListGroupBy.Default = ObjReportDetailProperty.Default_Group_By;
            }
            else
            {
                ListGroupBy.DTab = mDTDetail;
                ListGroupBy.Default = ObjReportDetailProperty.Default_Group_By;
            }

            if (ObjReportDetailProperty.Remark == "MIX_SPLIT_REPORT_FORMAT1" || ObjReportDetailProperty.Remark == "MIX_SPLIT_REPORT_DETAIL" || ObjReportDetailProperty.Remark == "FACTORY_MIX_SPLIT_REPORT")
            {
                groupControl4.Visible = true;
                groupControl5.Visible = true;
            }
            else
            {
                if (ObjReportDetailProperty.Remark == "MAKABLE_STOCK_TRACE" || ObjReportDetailProperty.Remark == "MAKABLE_STOCK_TRACE_DETAIL")
                {
                    groupControl17.Visible = true;
                    groupControl4.Visible = false;
                    groupControl5.Visible = false;
                    CmbDeptType.Text = "";
                }
                else
                {
                    CmbDeptType.Text = "";
                    groupControl4.Visible = false;
                    groupControl5.Visible = false;
                    groupControl17.Visible = false;
                }
            }
        }
        private void ListFromDepartment_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable DeTemp = new DataTable();
                var temp = ListFromDepartment.Properties.GetCheckedItems().ToString().Replace(" ", "").Replace("  ", "").Trim();
                if (temp != "")
                    DeTemp = (DataTable)ListFromDepartment.Properties.DataSource;

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
            for (int i = 0; i < ListFromDepartment.Properties.Items.Count; i++)
                ListFromDepartment.Properties.Items[i].CheckState = CheckState.Unchecked;
            for (int i = 0; i < ListToDepartment.Properties.Items.Count; i++)
                ListToDepartment.Properties.Items[i].CheckState = CheckState.Unchecked;
            for (int i = 0; i < ListKapan.Properties.Items.Count; i++)
                ListKapan.Properties.Items[i].CheckState = CheckState.Unchecked;
            for (int i = 0; i < ListRoughCut.Properties.Items.Count; i++)
                ListRoughCut.Properties.Items[i].CheckState = CheckState.Unchecked;
            for (int i = 0; i < ListType.Properties.Items.Count; i++)
                ListType.Properties.Items[i].CheckState = CheckState.Unchecked;
            for (int i = 0; i < ListTransactionType.Properties.Items.Count; i++)
                ListTransactionType.Properties.Items[i].CheckState = CheckState.Unchecked;
            for (int i = 0; i < ListProcess.Properties.Items.Count; i++)
                ListProcess.Properties.Items[i].CheckState = CheckState.Unchecked;
            for (int i = 0; i < ListSubProcess.Properties.Items.Count; i++)
                ListSubProcess.Properties.Items[i].CheckState = CheckState.Unchecked;
            CmbDeptType.Text = "";
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
        private void ListToDepartment_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable DeTemp = new DataTable();
                var temp = ListToDepartment.Properties.GetCheckedItems().ToString().Replace(" ", "").Replace("  ", "").Trim();
                if (temp != "")
                    DeTemp = (DataTable)ListToDepartment.Properties.DataSource;

                if (temp.Length > 0)
                    DeTemp = DeTemp.Select("department_id in (" + temp + ")").CopyToDataTable();

                StrColumnName = "To Department ";
                string StrFieldName = "department_name";
                TreeView_List(DeTemp, StrColumnName, StrFieldName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

            ListFromDepartment.Properties.DataSource = DTabDepartment;
            ListFromDepartment.Properties.DisplayMember = "Department_Name";
            ListFromDepartment.Properties.ValueMember = "department_id";

            ListBranch.SetEditValue(BLL.GlobalDec.gEmployeeProperty.branch_id.ToString());
            ListFromDepartment.SetEditValue(BLL.GlobalDec.gEmployeeProperty.department_id);
            r = dtAllvalues.NewRow();
            r[dc_Unique] = BLL.GlobalDec.gEmployeeProperty.branch_name;
            r[dc_Parent] = "Branch ";
            r[dc_Data] = BLL.GlobalDec.gEmployeeProperty.branch_name;
            dtAllvalues.Rows.Add(r);

            ListToDepartment.Properties.DataSource = DTabDepartment;
            ListToDepartment.Properties.DisplayMember = "Department_Name";
            ListToDepartment.Properties.ValueMember = "department_id";

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

            if (ListCompany.Text.Length > 0)
            {
                Str += ", Company : " + ListCompany.Text.ToString();
            }
            if (ListBranch.Text.Length > 0)
            {
                Str += ", Branch : " + ListBranch.Text.ToString();
            }
            if (ListLocation.Text.Length > 0)
            {
                Str += ", Location : " + ListLocation.Text.ToString();
            }
            if (ListFromDepartment.Text.Length > 0)
            {
                Str += ", From Dept : " + ListFromDepartment.Text.ToString();
            }
            if (ListToDepartment.Text.Length > 0)
            {
                Str += ", To Dept : " + ListToDepartment.Text.ToString();
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
    }
}
