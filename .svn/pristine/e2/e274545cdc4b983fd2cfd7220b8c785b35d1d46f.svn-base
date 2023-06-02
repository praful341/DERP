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
    public partial class FrmMFGRoughStockReport : DevExpress.XtraEditors.XtraForm
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
        string StrEntryType = string.Empty;
        string StrTransactionType = string.Empty;
        #endregion

        #region Counstructor

        public FrmMFGRoughStockReport()
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
            RBtnRejectionType.Visible = true;
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


            ReportParams_Property.transaction_type = StrTransactionType;

            ReportParams_Property.kapan_id = Val.Trim(ListKapan.Properties.GetCheckedItems());
            ReportParams_Property.party_id = Val.Trim(ListParty.Properties.GetCheckedItems());
            ReportParams_Property.purity_id = Val.Trim(ListRejPurity.Properties.GetCheckedItems());

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
            //DTab.Rows.Clear();
            if (ListTransactionType.Text == "REJECTION")
            {
                if (RBtnRejectionType.Text == "REJECTION DETAIL")
                {
                    ReportParams_Property.entry_type = Val.ToString(RBtnRejectionType.Text);
                    DTab = ObjReportParams.GetMFGRoughStock(ReportParams_Property, "RPT_Rejection_Detail");
                }
                else if (RBtnRejectionType.Text == "REJECTION KAPAN WISE PURITY SUMMARY")
                {
                    ReportParams_Property.entry_type = Val.ToString(RBtnRejectionType.Text);
                    DTab = ObjReportParams.GetMFGRoughStock(ReportParams_Property, "RPT_Rejection_Detail");
                }
                else if (RBtnRejectionType.Text == "REJECTION KAPAN WISE PURITY DETAIL")
                {
                    ReportParams_Property.entry_type = Val.ToString(RBtnRejectionType.Text);
                    DTab = ObjReportParams.GetMFGRoughStock(ReportParams_Property, "RPT_Rejection_Detail");
                }
                else if (RBtnRejectionType.Text == "REJECTION PURITY WISE SUMMARY")
                {
                    ReportParams_Property.entry_type = Val.ToString(RBtnRejectionType.Text);
                    DTab = ObjReportParams.GetMFGRoughStock(ReportParams_Property, "RPT_Rejection_Detail");
                }
            }
            else if (ListTransactionType.Text == "SALE")
            {
                if (RBtnSaleType.Text == "ROUGH SALE DETAIL")
                {
                    ReportParams_Property.entry_type = Val.ToString(RBtnSaleType.Text);
                    DTab = ObjReportParams.GetMFGRoughStock(ReportParams_Property, "RPT_Sale_Detail");
                }
                else if (RBtnSaleType.Text == "ROUGH SALE SUMMARY")
                {
                    ReportParams_Property.entry_type = Val.ToString(RBtnSaleType.Text);
                    DTab = ObjReportParams.GetMFGRoughStock(ReportParams_Property, "RPT_Sale_Detail");
                }
            }
            else if (ListTransactionType.Text == "STOCK")
            {
                if (RBtnStockType.Text == "ROUGH SALE STOCK WITH CL RATE JANGED")
                {
                    ReportParams_Property.entry_type = Val.ToString(RBtnStockType.Text);
                    DTab = ObjReportParams.GetMFGRoughStock(ReportParams_Property, ObjReportDetailProperty.Procedure_Name);
                }
                else if (RBtnStockType.Text == "ROUGH SALE STOCK WITH CL RATE SALE")
                {
                    ReportParams_Property.entry_type = Val.ToString(RBtnStockType.Text);
                    DTab = ObjReportParams.GetMFGRoughStock(ReportParams_Property, ObjReportDetailProperty.Procedure_Name);
                }
                else if (RBtnStockType.Text == "ROUGH STOCK")
                {
                    ReportParams_Property.entry_type = Val.ToString(RBtnStockType.Text);
                    DTab = ObjReportParams.GetMFGRoughStock(ReportParams_Property, ObjReportDetailProperty.Procedure_Name);
                }
            }
            else if (ListTransactionType.Text == "REJECTION TRANSFER")
            {
                if (RBtnRejTransferType.Text == "REJECTION TRANSFER FROM PURITY WISE DETAIL")
                {
                    DTab = ObjReportParams.GetMFGRoughStock(ReportParams_Property, "RPT_MFG_TRN_Rejection_InternalTrf");
                }
            }
            else if (ListTransactionType.Text == "MANUFACTURE")
            {
                if (RBtnManufactureType.Text == "MANUFACTURE LOTTING DETAIL")
                {
                    DTab = ObjReportParams.GetMFGRoughStock(ReportParams_Property, "RPT_MFG_TRN_RoughToMakable_Trf");
                }
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

                ObjReportDetailProperty.Remark = "";
                FrmGReportViewer.Remark = "";

                if (ListTransactionType.Text == "REJECTION")
                {
                    if (RBtnRejectionType.Text == "REJECTION DETAIL")
                    {
                        ObjReportDetailProperty.Remark = "REJECTION DETAIL";
                        FrmGReportViewer.Remark = "REJECTION DETAIL";
                        FrmGReportViewer.ReportHeaderName = "Rejection Detail";
                    }
                    else if (RBtnRejectionType.Text == "REJECTION KAPAN WISE PURITY SUMMARY")
                    {
                        ObjReportDetailProperty.Remark = "REJECTION KAPAN WISE PURITY SUMMARY";
                        FrmGReportViewer.Remark = "REJECTION KAPAN WISE PURITY SUMMARY";
                        FrmGReportViewer.ReportHeaderName = "Rejection Kapan Wise Purity Summary";
                    }
                    else if (RBtnRejectionType.Text == "REJECTION KAPAN WISE PURITY DETAIL")
                    {
                        ObjReportDetailProperty.Remark = "REJECTION KAPAN WISE PURITY DETAIL";
                        FrmGReportViewer.Remark = "REJECTION KAPAN WISE PURITY DETAIL";
                        FrmGReportViewer.ReportHeaderName = "Rejection Kapan Wise Purity Detail";
                    }
                    else if (RBtnRejectionType.Text == "REJECTION PURITY WISE SUMMARY")
                    {
                        ObjReportDetailProperty.Remark = "REJECTION PURITY WISE SUMMARY";
                        FrmGReportViewer.Remark = "REJECTION PURITY WISE SUMMARY";
                        FrmGReportViewer.ReportHeaderName = "Rejection Purity Wise Summary";
                    }
                }
                else if (ListTransactionType.Text == "SALE")
                {
                    if (RBtnSaleType.Text == "ROUGH SALE DETAIL")
                    {
                        ObjReportDetailProperty.Remark = "ROUGH SALE DETAIL";
                        FrmGReportViewer.Remark = "ROUGH SALE DETAIL";
                        FrmGReportViewer.ReportHeaderName = "Rough Sale Detail";
                    }
                    else if (RBtnSaleType.Text == "ROUGH SALE SUMMARY")
                    {
                        ObjReportDetailProperty.Remark = "ROUGH SALE SUMMARY";
                        FrmGReportViewer.Remark = "ROUGH SALE SUMMARY";
                        FrmGReportViewer.ReportHeaderName = "Rough Sale Summary";
                    }
                }
                else if (ListTransactionType.Text == "STOCK")
                {
                    if (RBtnStockType.Text == "ROUGH SALE STOCK WITH CL RATE JANGED")
                    {
                        ObjReportDetailProperty.Remark = "MFG_ROUGH_SALE_STOCK_CLOSING_RATE";
                        FrmGReportViewer.Remark = "MFG_ROUGH_SALE_STOCK_CLOSING_RATE";
                        FrmGReportViewer.ReportHeaderName = "Rough Sale Stock With CL Rate Janged";
                    }
                    else if (RBtnStockType.Text == "ROUGH SALE STOCK WITH CL RATE SALE")
                    {
                        ObjReportDetailProperty.Remark = "MFG_ROUGH_SALE_STOCK_CLOSING_RATE";
                        FrmGReportViewer.Remark = "MFG_ROUGH_SALE_STOCK_CLOSING_RATE";
                        FrmGReportViewer.ReportHeaderName = "Rough Sale Stock With CL Rate Sale";
                    }
                    else if (RBtnStockType.Text == "ROUGH STOCK")
                    {
                        ObjReportDetailProperty.Remark = "ROUGH_STOCK";
                        FrmGReportViewer.Remark = "ROUGH_STOCK";
                        FrmGReportViewer.ReportHeaderName = "Rough Stock";
                    }
                }
                else if (ListTransactionType.Text == "REJECTION TRANSFER")
                {
                    if (RBtnRejTransferType.Text == "REJECTION TRANSFER FROM PURITY WISE DETAIL")
                    {
                        ObjReportDetailProperty.Remark = "REJECTION TRANSFER FROM PURITY WISE DETAIL";
                        FrmGReportViewer.Remark = "REJECTION TRANSFER FROM PURITY WISE DETAIL";
                        FrmGReportViewer.ReportHeaderName = "Rejection Transfer From / To Purity Wise";
                    }
                    else if (RBtnStockType.Text == "REJECTION TO MAKABLE TRANSFER")
                    {
                        ObjReportDetailProperty.Remark = "REJECTION TO MAKABLE TRANSFER";
                        FrmGReportViewer.Remark = "REJECTION TO MAKABLE TRANSFER";
                        FrmGReportViewer.ReportHeaderName = "Rejection To Makable Transfer";
                    }
                }
                else if (ListTransactionType.Text == "MANUFACTURE")
                {
                    if (RBtnManufactureType.Text == "MANUFACTURE LOTTING DETAIL")
                    {
                        ObjReportDetailProperty.Remark = "MANUFACTURE LOTTING DETAIL";
                        FrmGReportViewer.Remark = "MANUFACTURE LOTTING DETAIL";
                        FrmGReportViewer.ReportHeaderName = "Manufacture Lotting Detail";
                    }
                }

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
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void BtnReset_Click(object sender, EventArgs e)
        {

            chkPivot.Checked = false;

            for (int i = 0; i < ListKapan.Properties.Items.Count; i++)
                ListKapan.Properties.Items[i].CheckState = CheckState.Unchecked;
            for (int i = 0; i < ListParty.Properties.Items.Count; i++)
                ListParty.Properties.Items[i].CheckState = CheckState.Unchecked;
            for (int i = 0; i < ListRejPurity.Properties.Items.Count; i++)
                ListRejPurity.Properties.Items[i].CheckState = CheckState.Unchecked;

            for (int i = 0; i < ListEntryType.Properties.Items.Count; i++)
                ListEntryType.Properties.Items[i].CheckState = CheckState.Unchecked;
            for (int i = 0; i < ListTransactionType1.Properties.Items.Count; i++)
                ListTransactionType1.Properties.Items[i].CheckState = CheckState.Unchecked;

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

            DataTable DTabKapan = ObjFillCombo.FillCmb(FillCombo.TABLE.Kapan_Master);
            DTabKapan.DefaultView.Sort = "kapan_id";
            DTabKapan = DTabKapan.DefaultView.ToTable();

            ListKapan.Properties.DataSource = DTabKapan;
            ListKapan.Properties.DisplayMember = "kapan_no";
            ListKapan.Properties.ValueMember = "kapan_id";

            DataTable DTabCut = ObjFillCombo.FillCmb(FillCombo.TABLE.Cut_Master);
            DTabCut.DefaultView.Sort = "rough_cut_id";
            DTabCut = DTabCut.DefaultView.ToTable();

            ListParty.Properties.DataSource = DTabCut;
            ListParty.Properties.DisplayMember = "rough_cut_no";
            ListParty.Properties.ValueMember = "rough_cut_id";

            treeView_Condition.ParentFieldName = "Parent";
            treeView_Condition.KeyFieldName = "Unique";
            treeView_Condition.RootValue = string.Empty;
            treeView_Condition.DataSource = dtAllvalues;
            treeView_Condition.ExpandAll();

            //DataTable DTabType = Global.DTabEntryType();
            //DTabType.DefaultView.Sort = "Entry_Type";
            //DTabType = DTabType.DefaultView.ToTable();

            //ListEntryType.Properties.DataSource = DTabType;
            //ListEntryType.Properties.DisplayMember = "Entry_Type";
            //ListEntryType.Properties.ValueMember = "Entry_Type1";

            //DataTable DTabTransactionType = Global.Rough_Type();
            //DTabTransactionType.DefaultView.Sort = "Type";
            //DTabTransactionType = DTabTransactionType.DefaultView.ToTable();

            //ListTransactionType.Properties.DataSource = DTabTransactionType;
            //ListTransactionType.Properties.DisplayMember = "Type";
            //ListTransactionType.Properties.ValueMember = "Type1";

            DataTable DTabPurity = ObjFillCombo.FillCmb(FillCombo.TABLE.Purity_Master);
            DTabPurity.DefaultView.Sort = "purity_id";
            DTabPurity = DTabPurity.DefaultView.ToTable();

            ListRejPurity.Properties.DataSource = DTabPurity;
            ListRejPurity.Properties.DisplayMember = "purity_name";
            ListRejPurity.Properties.ValueMember = "purity_id";

            Global.LOOKUPRoughCheckClarity(ListRejPurity);
            Global.LOOKUPRoughParty(ListParty);
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
            if (ListParty.Text.Length > 0)
            {
                Str += ", Cut No : " + ListParty.Text.ToString();
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

        private void ListTransactionType_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (ListTransactionType.Text == "REJECTION")
                {
                    RBtnRejectionType.Visible = true;
                    RBtnSaleType.Visible = false;
                    RBtnManufactureType.Visible = false;
                    RBtnRejTransferType.Visible = false;
                    RBtnStockType.Visible = false;
                    RBtnRejectionType.SelectedIndex = 0;
                }
                else if (ListTransactionType.Text == "SALE")
                {
                    RBtnRejectionType.Visible = false;
                    RBtnSaleType.Visible = true;
                    RBtnManufactureType.Visible = false;
                    RBtnRejTransferType.Visible = false;
                    RBtnStockType.Visible = false;
                    RBtnSaleType.SelectedIndex = 0;
                }
                else if (ListTransactionType.Text == "STOCK")
                {
                    RBtnRejectionType.Visible = false;
                    RBtnSaleType.Visible = false;
                    RBtnManufactureType.Visible = false;
                    RBtnRejTransferType.Visible = false;
                    RBtnStockType.Visible = true;
                    RBtnStockType.SelectedIndex = 0;
                }
                else if (ListTransactionType.Text == "MANUFACTURE")
                {
                    RBtnRejectionType.Visible = false;
                    RBtnSaleType.Visible = false;
                    RBtnManufactureType.Visible = true;
                    RBtnRejTransferType.Visible = false;
                    RBtnStockType.Visible = false;
                    RBtnManufactureType.SelectedIndex = 0;
                }
                else if (ListTransactionType.Text == "REJECTION TRANSFER")
                {
                    RBtnRejectionType.Visible = false;
                    RBtnSaleType.Visible = false;
                    RBtnManufactureType.Visible = false;
                    RBtnRejTransferType.Visible = true;
                    RBtnStockType.Visible = false;
                    RBtnRejTransferType.SelectedIndex = 0;
                }
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

            ListParty.Properties.DataSource = DTabCut;
            ListParty.Properties.DisplayMember = "rough_cut_no";
            ListParty.Properties.ValueMember = "rough_cut_id";

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
        private void ListPurity_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable DeTemp = new DataTable();
                var temp = ListRejPurity.Properties.GetCheckedItems().ToString().Replace(" ", "").Replace("  ", "").Trim();
                if (temp != "")
                    DeTemp = (DataTable)ListRejPurity.Properties.DataSource;

                if (temp.Length > 0)
                    DeTemp = DeTemp.Select("purity_id in (" + temp + ")").CopyToDataTable();

                StrColumnName = "Purity ";
                string StrFieldName = "purity_name";
                TreeView_List(DeTemp, StrColumnName, StrFieldName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ListParty_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable DeTemp = new DataTable();
                var temp = ListParty.Properties.GetCheckedItems().ToString().Replace(" ", "").Replace("  ", "").Trim();
                if (temp != "")
                    DeTemp = (DataTable)ListParty.Properties.DataSource;

                if (temp.Length > 0)
                    DeTemp = DeTemp.Select("party_id in (" + temp + ")").CopyToDataTable();

                StrColumnName = "Party ";
                string StrFieldName = "party_name";
                TreeView_List(DeTemp, StrColumnName, StrFieldName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
