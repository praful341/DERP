using BLL;
using BLL.FunctionClasses.Account;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.PropertyClasses.Account;
using BLL.PropertyClasses.Transaction;
using BLL.PropertyClasses.Transaction.MFG;
using DERP.Class;
using DERP.Search;
using DevExpress.XtraEditors;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Transaction
{
    public partial class FrmMFGLoanEntry : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member
        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        Control _NextEnteredControl;
        private List<Control> _tabControls;
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);


        DataTable DtControlSettings;
        DataTable m_dtbPaymenttype;
        DataTable DTabLoan;

        CompanyMaster objCompany = new CompanyMaster();
        BranchMaster objBranch = new BranchMaster();
        LocationMaster objLocation = new LocationMaster();
        DepartmentMaster objDepartment = new DepartmentMaster();
        MFGLoanEntry ObjLoan = new MFGLoanEntry();
        LedgerMaster objLedger = new LedgerMaster();
        ExpenseEntryMaster objExpenseEntry = new ExpenseEntryMaster();
        IncomeEntryMaster objIncomeEntry = new IncomeEntryMaster();
        bool m_blnsave;
        int m_numForm_id;
        int IntRes;
        FrmSearchNew FrmSearchNew;
        decimal m_exchange_rate = 0;
        #endregion

        #region Constructor
        public FrmMFGLoanEntry()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();
            _NextEnteredControl = new Control();
            _tabControls = new List<Control>();
            DtControlSettings = new DataTable();
            DTabLoan = new DataTable();
            m_numForm_id = 0;
            IntRes = 0;
        }
        public void ShowForm()
        {
            ObjPer.FormName = this.Name.ToUpper();
            m_numForm_id = ObjPer.form_id;
            if (ObjPer.CheckPermission() == false)
            {
                Global.Message(BLL.GlobalDec.gStrPermissionViwMsg);
                return;
            }
            AttachFormEvents();

            // for Dynamic Setting By Praful On 01022020

            //if (Global.HideFormControls(Val.ToInt(ObjPer.form_id), this) != "")
            //{
            //    Global.Message("Select First User Setting...Please Contact to Administrator...");
            //    return;
            //}

            //ControlSettingDT(Val.ToInt(ObjPer.form_id), this);
            AddGotFocusListener(this);
            this.KeyPreview = true;

            TabControlsToList(this.Controls);
            _tabControls = _tabControls.OrderBy(x => x.TabIndex).ToList();

            // End for Dynamic Setting By Praful On 01022020

            this.Show();
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

        #region Dynamic Tab Setting
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
            }
        }

        private void TabControlsToList(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control.TabStop)
                    _tabControls.Add(control);
                if (control.HasChildren)
                    TabControlsToList(control.Controls);
            }
        }
        private void ControlSettingDT(int FormCode, Form pForm)
        {
            BLL.Validation Val = new BLL.Validation();
            Single_Setting ObjSingleSettings = new Single_Setting();
            Single_SettingProperty Property = new Single_SettingProperty();

            Property.role_id = Val.ToInt(BLL.GlobalDec.gEmployeeProperty.role_id);
            Property.form_id = Val.ToInt(FormCode);
            DataTable DtColSetting = ObjSingleSettings.GetData(Property);

            DataTable DtFilterColSetting = (from DataRow dr in DtColSetting.Rows
                                            where Val.ToBooleanToInt(dr["is_control"]) == 1
                                            && dr["column_type"].ToString() != "LABEL"
                                            select dr).CopyToDataTable();
            DevExpress.XtraLayout.LayoutControl l = new DevExpress.XtraLayout.LayoutControl();
            l.OptionsFocus.EnableAutoTabOrder = false;

            if (DtFilterColSetting.Rows.Count > 0)
            {
                DtControlSettings = DtFilterColSetting;
                foreach (Control item1 in pForm.Controls)
                {
                    ControllSettings(item1, DtFilterColSetting);
                }
            }
        }

        private static void ControllSettings(Control item2, DataTable DTab)
        {
            BLL.Validation Val = new BLL.Validation();

            //else
            {
                var VarControlSetting = (from DataRow dr in DTab.Rows
                                         where dr["column_name"].ToString() == item2.Name.ToString()
                                         select dr);

                if (VarControlSetting.Count() > 0)
                {
                    DataRow DRow = VarControlSetting.CopyToDataTable().Rows[0];
                    if (item2.Name.ToString() == Val.ToString(DRow["column_name"]))
                    {
                        if (!(item2 is TextEdit))
                        {
                            if (!(item2 is DateTimePicker))
                            {
                                if (!(item2 is DevExpress.XtraEditors.TextEdit))
                                {
                                    item2.Text = (Val.ToBooleanToInt(DRow["is_compulsory"]).Equals(0) ? Val.ToString(DRow["caption"]) : "* " + Val.ToString(DRow["caption"]));
                                }
                            }
                        }
                        if (Val.ToInt(DRow["tab_index"]) >= 0)
                        {
                            if (item2.CanSelect)
                                item2.TabStop = true;
                        }
                        else
                            item2.TabStop = false;
                        if (Val.ToBooleanToInt(DRow["is_visible"]).Equals(1))
                        {
                            item2.Visible = true;
                            item2.TabStop = true;
                        }
                        else
                        {
                            item2.Visible = false;
                            item2.TabStop = false;
                        }

                        item2.TabIndex = Val.ToInt(DRow["tab_index"]);
                        if (item2.TabIndex == 1)
                        {
                            item2.Select();
                            item2.Focus();
                        }
                        if (Val.ToBooleanToInt(DRow["is_editable"]).Equals(1))
                        {
                            item2.Enabled = true;
                        }
                        else
                        {
                            item2.Enabled = false;
                        }
                    }
                }
                else
                {
                    item2.TabStop = false;
                }
            }
            if (item2.Controls.Count > 0)
            {
                foreach (Control item1 in item2.Controls)
                {
                    ControllSettings(item1, DTab);
                }
            }
        }

        #endregion

        #region "Events" 
        private void FrmPriceImport_Shown(object sender, System.EventArgs e)
        {
            DTPFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPFromDate.Properties.CharacterCasing = CharacterCasing.Upper;

            DTPToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPToDate.Properties.CharacterCasing = CharacterCasing.Upper;

            DTPFromDate.EditValue = DateTime.Now;
            DTPToDate.EditValue = DateTime.Now;

            m_dtbPaymenttype = new DataTable();

            m_dtbPaymenttype.Columns.Add("payment_type");
            m_dtbPaymenttype.Rows.Add("Cash");
            m_dtbPaymenttype.Rows.Add("Bank");

            RepPaymentType.DataSource = m_dtbPaymenttype;
            RepPaymentType.ValueMember = "payment_type";
            RepPaymentType.DisplayMember = "payment_type";

            DTabLoan.Columns.Add(new DataColumn("delete", typeof(string)));
            DTabLoan.Columns.Add(new DataColumn("loan_id", typeof(string)));
            DTabLoan.Columns.Add(new DataColumn("ledger_id", typeof(int)));
            DTabLoan.Columns.Add(new DataColumn("ledger_name", typeof(string)));
            DTabLoan.Columns.Add(new DataColumn("company_id", typeof(int)));
            DTabLoan.Columns.Add(new DataColumn("company_name", typeof(string)));
            DTabLoan.Columns.Add(new DataColumn("branch_id", typeof(int)));
            DTabLoan.Columns.Add(new DataColumn("branch_name", typeof(string)));
            DTabLoan.Columns.Add(new DataColumn("location_id", typeof(int)));
            DTabLoan.Columns.Add(new DataColumn("location_name", typeof(string)));
            DTabLoan.Columns.Add(new DataColumn("department_id", typeof(int)));
            DTabLoan.Columns.Add(new DataColumn("department_name", typeof(string)));
            DTabLoan.Columns.Add(new DataColumn("fyear", typeof(int)));
            DTabLoan.Columns.Add(new DataColumn("fmonth", typeof(int)));
            DTabLoan.Columns.Add(new DataColumn("entry_date", typeof(DateTime)));
            DTabLoan.Columns.Add(new DataColumn("opening_amount", typeof(double)));
            DTabLoan.Columns.Add(new DataColumn("new_given", typeof(double)));
            DTabLoan.Columns.Add(new DataColumn("new_given_date", typeof(DateTime)));
            DTabLoan.Columns.Add(new DataColumn("payment_type", typeof(string)));
            DTabLoan.Columns.Add(new DataColumn("recovery_amount", typeof(double)));
            DTabLoan.Columns.Add(new DataColumn("recovery_date", typeof(DateTime)));
            DTabLoan.Columns.Add(new DataColumn("loss_amount", typeof(double)));
            DTabLoan.Columns.Add(new DataColumn("loss_date", typeof(DateTime)));
            DTabLoan.Columns.Add(new DataColumn("deduct_from_salary", typeof(double)));
            DTabLoan.Columns.Add(new DataColumn("with_emi_amount", typeof(double)));
            DTabLoan.Columns.Add(new DataColumn("interest_per", typeof(double)));
            DTabLoan.Columns.Add(new DataColumn("interest_amount", typeof(double)));
            DTabLoan.Columns.Add(new DataColumn("closing_amount", typeof(double)));
            DTabLoan.Columns.Add(new DataColumn("remark", typeof(string)));
            DTabLoan.Columns.Add(new DataColumn("is_paid", typeof(Int32)));
            DTabLoan.Columns.Add(new DataColumn("paid_date", typeof(string)));
            DTabLoan.Columns.Add(new DataColumn("pending_loan_amt", typeof(double)));
            DTabLoan.Columns.Add(new DataColumn("advance_amount", typeof(double)));
            DTabLoan.Columns.Add(new DataColumn("advance_date", typeof(DateTime)));
            DTabLoan.Columns.Add(new DataColumn("advance_rec_amount", typeof(double)));
            DTabLoan.Columns.Add(new DataColumn("advance_rec_date", typeof(DateTime)));
            DTabLoan.Columns.Add(new DataColumn("pending_advance_amt", typeof(double)));

            DgvMainLoan.DataSource = DTabLoan;
            DgvMainLoan.Refresh();

            ChkInterest_CheckedChanged(null, null);
            ChkLoss_CheckedChanged(null, null);

            CurrencyMaster objCurrency = new CurrencyMaster();
            DataTable DTab_Rate = objCurrency.GetCurrencyRate(Val.ToInt(GlobalDec.gEmployeeProperty.currency_id), Val.ToInt(GlobalDec.gEmployeeProperty.currency_id));

            if (DTab_Rate.Rows.Count > 0)
            {
                m_exchange_rate = Val.ToDecimal(DTab_Rate.Rows[0]["rate"].ToString());
            }
            else
            {
                Global.Message("Currency Rate Not Found..");
                return;
            }
        }
        private void FrmPriceImport_Load(object sender, EventArgs e)
        {
            try
            {
                ChkCmbCompanyName.Properties.DataSource = objCompany.GetData();
                ChkCmbCompanyName.Properties.DisplayMember = "company_name";
                ChkCmbCompanyName.Properties.ValueMember = "company_id";
                ChkCmbCompanyName.SetEditValue(GlobalDec.gEmployeeProperty.company_id);

                ChkCmbBranchName.Properties.DataSource = objBranch.GetData();
                ChkCmbBranchName.Properties.DisplayMember = "branch_name";
                ChkCmbBranchName.Properties.ValueMember = "branch_id";
                ChkCmbBranchName.SetEditValue(GlobalDec.gEmployeeProperty.branch_id);

                ChkCmbDepartmentName.Properties.DataSource = objDepartment.GetData();
                ChkCmbDepartmentName.Properties.DisplayMember = "department_name";
                ChkCmbDepartmentName.Properties.ValueMember = "department_id";

                ChkCmbLocationName.Properties.DataSource = objLocation.GetData();
                ChkCmbLocationName.Properties.DisplayMember = "location_name";
                ChkCmbLocationName.Properties.ValueMember = "location_id";
                ChkCmbLocationName.SetEditValue(GlobalDec.gEmployeeProperty.location_id);

                ChkCmbLedger.Properties.DataSource = objLedger.GetData(1, 0);
                ChkCmbLedger.Properties.DisplayMember = "ledger_name";
                ChkCmbLedger.Properties.ValueMember = "ledger_id";
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ObjPer.SetFormPer();
                if (ObjPer.AllowUpdate == false || ObjPer.AllowInsert == false)
                {
                    Global.Message(BLL.GlobalDec.gStrPermissionInsUpdMsg);
                    return;
                }
                btnSave.Enabled = false;
                m_blnsave = true;
                if (!ValidateDetails())
                {
                    m_blnsave = false;
                    btnSave.Enabled = true;
                    return;
                }
                DialogResult result = MessageBox.Show("Are You Sure to Save/Update Loan ?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result != DialogResult.Yes)
                {
                    btnSave.Enabled = true;
                    return;
                }

                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                backgroundWorker_LoanEntry.RunWorkerAsync();

                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                this.Cursor = Cursors.Default;
                return;
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
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

                ChkCmbCompanyName.SetEditValue(GlobalDec.gEmployeeProperty.company_id);
                ChkCmbBranchName.SetEditValue(GlobalDec.gEmployeeProperty.branch_id);
                ChkCmbLocationName.SetEditValue(GlobalDec.gEmployeeProperty.location_id);

                ChkCmbDepartmentName.Reset();
                ChkCmbLedger.Reset();
                ChkCmbDepartmentName.SetEditValue("");
                ChkCmbLedger.SetEditValue("");
                txtFromYearMonth.Text = "";
                txtToYearMonth.Text = "";
                ChkInterest.Checked = false;
                ChkLoss.Checked = false;
                ChkAutoWidth.Checked = true;
                DTabLoan.Rows.Clear();
                DgvMainLoan.DataSource = null;
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ChkAutoWidth_CheckedChanged(object sender, EventArgs e)
        {
            GrdDetLoan.OptionsView.ColumnAutoWidth = ChkAutoWidth.Checked;
        }
        private void ChkLoss_CheckedChanged(object sender, EventArgs e)
        {
            GrdDetLoan.Bands["BANDLOSS"].Visible = ChkLoss.Checked;
        }
        private void ChkInterest_CheckedChanged(object sender, EventArgs e)
        {
            GrdDetLoan.Bands["BANDINTEREST"].Visible = ChkInterest.Checked;
        }
        private void btnAddNewEmi_Click(object sender, EventArgs e)
        {
            if (DTabLoan.Rows.Count == 0)
            {
                Global.Message("No Any Loan Data Found");
                return;
            }
            DataRow DRow = DTabLoan.Rows[DTabLoan.Rows.Count - 1];
            AddNewEMI(DRow);
        }
        private void GrdDetLoan_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            switch (e.Column.FieldName)
            {
                case "with_emi_amount":
                    DataRow DRow = GrdDetLoan.GetDataRow(e.RowHandle);
                    double DouEMIAmount = Val.Val(DRow["with_emi_amount"]);
                    break;

                case "entry_date":
                    DRow = GrdDetLoan.GetDataRow(e.RowHandle);
                    if (Val.ISDate(DRow["entry_date"]) == true)
                    {
                        GrdDetLoan.SetRowCellValue(e.RowHandle, "fyear", DateTime.Parse(Val.ToString(DRow["entry_date"])).Year);
                        GrdDetLoan.SetRowCellValue(e.RowHandle, "fmonth", DateTime.Parse(Val.ToString(DRow["entry_date"])).Month);
                    }
                    break;

                case "new_given":
                case "recovery_amount":
                case "loss_amount":
                case "interest_amount":
                case "opening_amount":
                case "advance_amount":
                case "advance_rec_amount":
                    DRow = GrdDetLoan.GetDataRow(e.RowHandle);
                    double DouOpeningAmount = Val.Val(DRow["opening_amount"]);
                    double DouNewGiven = Val.Val(DRow["New_given"]);
                    double DouLoss = Val.Val(DRow["loss_amount"]);
                    double DouRecoveryAmount = Val.Val(DRow["recovery_amount"]);
                    double DouInterestAmount = Val.Val(DRow["interest_amount"]);

                    double DouClosing = Math.Round(DouOpeningAmount + DouNewGiven - DouRecoveryAmount - DouLoss + DouInterestAmount, 0);

                    double DouPending_Amt = Math.Round(DouNewGiven - DouRecoveryAmount - DouLoss + DouInterestAmount, 0);

                    GrdDetLoan.SetRowCellValue(e.RowHandle, "pending_loan_amt", DouPending_Amt);

                    double DouPendingAdvanceAmount = Val.Val(DRow["advance_amount"]) - Val.Val(DRow["advance_rec_amount"]);
                    GrdDetLoan.SetRowCellValue(e.RowHandle, "pending_advance_amt", DouPendingAdvanceAmount);

                    string ID = GrdDetLoan.GetRowCellValue(e.RowHandle, "loan_id").ToString();

                    DataRow[] dr = DTabLoan.Select("loan_id = '" + ID + "'");
                    int Index = 0;

                    if (dr.Length > 0)
                    {
                        Index = DTabLoan.Rows.IndexOf(dr[0]);
                    }

                    GrdDetLoan.SetRowCellValue(e.RowHandle, "closing_amount", DouClosing);

                    if (e.Column.FieldName == "recovery_amount")
                    {
                        GrdDetLoan.SetRowCellValue(e.RowHandle, "recovery_date", DateTime.Now.ToString("dd/MMM/yyyy"));
                    }
                    if (e.Column.FieldName == "loss_amount")
                    {
                        GrdDetLoan.SetRowCellValue(e.RowHandle, "loss_date", DateTime.Now.ToString("dd/MMM/yyyy"));
                    }
                    break;
                default:
                    break;
            }
            GrdDetLoan.BestFitColumns();
        }

        private void txtEmployeeID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Global.OnKeyPressToOpenPopup(e))
            {
                FrmSearchNew = new FrmSearchNew();
                FrmSearchNew.SearchText = e.KeyChar.ToString();

                FrmSearchNew.DTab = new LedgerMaster().GetData(1, 0);
                FrmSearchNew.ColumnsToHide = "ledger_id";
                FrmSearchNew.SearchField = "ledger_id,ledger_name";

                FrmSearchNew.ShowDialog();
                e.Handled = true;
                if (FrmSearchNew.DRow != null)
                {
                    GrdDetLoan.SetFocusedRowCellValue("ledger_id", Val.ToString(FrmSearchNew.DRow["ledger_id"]));
                    GrdDetLoan.SetFocusedRowCellValue("ledger_name", Val.ToString(FrmSearchNew.DRow["ledger_name"]));
                    GrdDetLoan.SetFocusedRowCellValue("company_id", Val.ToString(FrmSearchNew.DRow["company_id"]));
                    GrdDetLoan.SetFocusedRowCellValue("company_name", Val.ToString(FrmSearchNew.DRow["company_name"]));
                    GrdDetLoan.SetFocusedRowCellValue("branch_id", Val.ToString(FrmSearchNew.DRow["branch_id"]));
                    GrdDetLoan.SetFocusedRowCellValue("branch_name", Val.ToString(FrmSearchNew.DRow["branch_name"]));
                    GrdDetLoan.SetFocusedRowCellValue("location_id", Val.ToString(FrmSearchNew.DRow["location_id"]));
                    GrdDetLoan.SetFocusedRowCellValue("location_name", Val.ToString(FrmSearchNew.DRow["location_name"]));
                    GrdDetLoan.SetFocusedRowCellValue("department_id", Val.ToString(GlobalDec.gEmployeeProperty.department_id));
                    GrdDetLoan.SetFocusedRowCellValue("department_name", Val.ToString(GlobalDec.gEmployeeProperty.department_name));
                    GrdDetLoan.BestFitColumns();
                }

                FrmSearchNew.Hide();
                FrmSearchNew.Dispose();
                FrmSearchNew = null;
            }
        }

        private void txRemark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                DataRow DRow = GrdDetLoan.GetDataRow(GrdDetLoan.FocusedRowHandle);

                string Type = DRow["payment_type"].ToString();

                if (Type == "")
                {
                    Global.Message("payment Type is Required..");
                    return;
                }

                AddNewEMI(DRow);
            }
        }
        public void SetControlPropertyValue(Control oControl, string propName, object propValue)
        {
            if (oControl.InvokeRequired)
            {
                SetControlValueCallback d = new SetControlValueCallback(SetControlPropertyValue);
                oControl.Invoke(d, new object[]
                        {
                            oControl,
                            propName,
                            propValue
                        });
            }
            else
            {
                Type t = oControl.GetType();
                PropertyInfo[] props = t.GetProperties();
                foreach (PropertyInfo p in props)
                {
                    if ((p.Name.ToUpper() == propName.ToUpper()))
                    {
                        p.SetValue(oControl, propValue, null);
                    }
                }
            }
        }
        private void btnAddNewLoan_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are You Want To Add New Loan", "Confirmation", MessageBoxButtons.YesNoCancel);
            if (result != DialogResult.Yes)
            {
                btnSave.Enabled = true;
                return;
            }

            DTabLoan.Rows.Clear();

            DataRow DRow = DTabLoan.NewRow();
            DTabLoan.Rows.Add(DRow);
        }
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (Val.Trim(ChkCmbCompanyName.Properties.GetCheckedItems()) == "")
            {
                Global.Message("Please Select At Least One Company To Search Loan Detail");
                ChkCmbCompanyName.Focus();
                return;
            }
            if (Val.Trim(ChkCmbBranchName.Properties.GetCheckedItems()) == "")
            {
                Global.Message("Please Select At Least One Branch To Search Loan Detail");
                ChkCmbBranchName.Focus();
                return;
            }
            if (Val.Trim(ChkCmbLocationName.Properties.GetCheckedItems()) == "")
            {
                Global.Message("Please Select At Least One Location To Search Loan Detail");
                ChkCmbLocationName.Focus();
                return;
            }

            MFGLoanEntry_Property Property = new MFGLoanEntry_Property();

            Property.Company_Multi = Val.Trim(ChkCmbCompanyName.Properties.GetCheckedItems());
            Property.Branch_Multi = Val.Trim(ChkCmbBranchName.Properties.GetCheckedItems());
            Property.Department_Multi = Val.Trim(ChkCmbDepartmentName.Properties.GetCheckedItems());
            Property.Location_Multi = Val.Trim(ChkCmbLocationName.Properties.GetCheckedItems());

            Property.Ledger_ID_Multi = Val.Trim(ChkCmbLedger.Properties.GetCheckedItems());


            Property.FromYearMonth = Val.ToInt(txtFromYearMonth.Text);
            Property.ToYearMonth = Val.ToInt(txtToYearMonth.Text);

            if (DTPFromDate.Text != "" && DTPToDate.Text != "")
            {
                Property.FromDate = Val.DBDate(DTPFromDate.Text);
                Property.ToDate = Val.DBDate(DTPToDate.Text);
            }
            else
            {
                Property.FromDate = null;
                Property.ToDate = null;
            }

            DTabLoan = ObjLoan.GetDataForSearchNew(Property);
            DgvMainLoan.DataSource = DTabLoan;
            DgvMainLoan.RefreshDataSource();
            GrdDetLoan.BestFitColumns();
        }
        private void backgroundWorker_LoanEntry_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                {
                    Conn = new BeginTranConnection(true, false);
                }
                else
                {
                    Conn = new BeginTranConnection(false, true);
                }
                MFGLoanEntry_Property Property = new MFGLoanEntry_Property();
                IntRes = 0;
                try
                {
                    System.Data.DataTable DTab = (System.Data.DataTable)DTabLoan.GetChanges();

                    if (DTab == null)
                    {
                        Global.Message("No Row Found For Update");
                        return;
                    }
                    else
                    {
                        DTab.AcceptChanges();
                    }

                    if (DTab.Rows.Count == 0)
                    {
                        Global.Message("No Row Found For Update");
                        return;
                    }

                    Int64 Against_Ledger_Id_Cash = objIncomeEntry.ISLadgerName_GetData("CASH BALANCE");
                    Int64 Against_Ledger_Id_Bank = objIncomeEntry.ISLadgerName_GetData("BANK BALANCE");

                    if (Against_Ledger_Id_Cash == 0 || Against_Ledger_Id_Bank == 0)
                    {
                        Global.Message("Cash Balance Or Bank Balance Leger Not Set ");
                        return;
                    }

                    foreach (DataRow DRow in DTab.Rows)
                    {
                        if (Val.Val(DRow["company_id"]) == 0)
                        {
                            Global.Message("Company Id Not Found");
                            return;
                        }
                        if (Val.Val(DRow["branch_id"]) == 0)
                        {
                            Global.Message("Branch Id Not Found");
                            return;
                        }
                        if (Val.Val(DRow["department_id"]) == 0)
                        {
                            Global.Message("Department Id Not Found");
                            return;
                        }
                        if (Val.Val(DRow["location_id"]) == 0)
                        {
                            Global.Message("Location Id Not Found");
                            return;
                        }
                        if (Val.Val(DRow["new_given"]) > 0)
                        {
                            if (Val.ISDate(DRow["entry_date"]) == false)
                            {
                                Global.Message("Entry Valid Format Of Loan Date");
                                return;
                            }
                        }
                        if (Val.Val(DRow["recovery_amount"]) > 0)
                        {
                            if (Val.ISDate(DRow["recovery_date"]) == false)
                            {
                                Global.Message("Entry Valid Format Of Recovery Date");
                                return;
                            }
                        }
                        if (Val.Val(DRow["advance_amount"]) > 0)
                        {
                            if (Val.ISDate(DRow["advance_date"]) == false)
                            {
                                Global.Message("Entry Valid Format Of Advance Date");
                                return;
                            }
                        }
                        if (Val.Val(DRow["advance_rec_amount"]) > 0)
                        {
                            if (Val.ISDate(DRow["advance_rec_date"]) == false)
                            {
                                Global.Message("Entry Valid Format Of Advance Recovery Date");
                                return;
                            }
                        }
                        if (Val.ToString(DRow["payment_type"]) == "")
                        {
                            Global.Message("Payment Type Is Require");
                            return;
                        }
                    }
                    foreach (DataRow DRow in DTab.Rows)
                    {
                        if (Val.ToString(DRow["loan_id"]) == "" || Val.ToInt64(DRow["loan_id"]) == 0)
                        {
                            Property.Loan_ID = 0;
                        }
                        else
                        {
                            Property.Loan_ID = Val.ToInt64(DRow["loan_id"]);
                        }

                        Property.Ledger_id = Val.ToInt(DRow["ledger_id"]);
                        Property.Company_id = Val.ToInt(DRow["company_id"]);
                        Property.Branch_id = Val.ToInt(DRow["branch_id"]);
                        Property.Location_id = Val.ToInt(DRow["location_id"]);
                        Property.Department_id = Val.ToInt(DRow["department_id"]);
                        Property.Opening_Amount = Val.Val(DRow["opening_amount"]);
                        Property.New_Given = Val.Val(DRow["new_given"]);

                        if (Val.Val(DRow["new_given"]) > 0)
                        {
                            Property.New_Given_Date = Val.DBDate(System.DateTime.Now.ToShortDateString());
                            Property.FYear = DateTime.Parse(Val.ToString(DRow["entry_date"])).Year;
                            Property.FMonth = DateTime.Parse(Val.ToString(DRow["entry_date"])).Month;
                            Property.Entry_Date = Val.DBDate(DRow["entry_date"].ToString());
                            Property.Entry_Time = Val.GetFullTime12();
                        }
                        else
                        {
                            Property.New_Given_Date = null;
                        }

                        Property.Recovery_Amount = Val.Val(DRow["recovery_amount"]);

                        if (Val.Val(DRow["recovery_amount"]) > 0)
                        {
                            Property.Recovery_Date = Val.DBDate(Val.ToString(DRow["recovery_date"]));
                            Property.FYear = DateTime.Parse(Val.ToString(DRow["recovery_date"])).Year;
                            Property.FMonth = DateTime.Parse(Val.ToString(DRow["recovery_date"])).Month;
                            Property.Entry_Date = Val.DBDate(DRow["recovery_date"].ToString());
                            Property.Entry_Time = Val.GetFullTime12();
                        }
                        else
                        {
                            Property.Recovery_Date = null;
                        }

                        Property.Loss_Amount = Val.Val(DRow["loss_amount"]);
                        if (Val.ISDate(DRow["loss_date"]) == true)
                        {
                            Property.Loss_Date = Val.DBDate(Val.ToString(DRow["loss_date"]));
                            Property.Entry_Date = Val.DBDate(DRow["loss_date"].ToString());
                            Property.Entry_Time = Val.GetFullTime12();
                        }

                        Property.With_EMI_Amount = Val.Val(DRow["with_emi_amount"]);
                        Property.Deduct_From_Salary = Val.Val(0);

                        Property.Interest_Per = Val.Val(DRow["interest_per"]);
                        Property.Interest_Amount = Val.Val(0);
                        Property.Closing_Amount = Val.Val(DRow["closing_amount"]);
                        Property.Remark = Val.ToString(DRow["remark"]);
                        Property.IS_Paid = Val.ToInt(0);
                        Property.Paid_Date = Val.DBDate(Val.ToString((DRow["paid_date"])));
                        Property.transaction_type = Val.ToString(DRow["payment_type"]);

                        Property.YearMonth = Property.FYear.ToString() + (Property.FMonth < 10 ? "0" + Property.FMonth.ToString() : Property.FMonth.ToString());

                        Property.Advance_Amount = Val.Val(DRow["advance_amount"]);

                        if (Val.Val(DRow["advance_amount"]) > 0)
                        {
                            Property.Advance_Date = Val.DBDate(Val.ToString(DRow["advance_date"]));
                            Property.FYear = DateTime.Parse(Val.ToString(DRow["advance_date"])).Year;
                            Property.FMonth = DateTime.Parse(Val.ToString(DRow["advance_date"])).Month;
                            Property.Entry_Date = Val.DBDate(DRow["advance_date"].ToString());
                            Property.Entry_Time = Val.GetFullTime12();
                        }
                        else
                        {
                            Property.Advance_Date = null;
                        }

                        Property.Advance_Recovery_Amount = Val.Val(DRow["advance_rec_amount"]);

                        if (Val.Val(DRow["advance_rec_amount"]) > 0)
                        {
                            Property.Advance_Recovery_Date = Val.DBDate(Val.ToString(DRow["advance_rec_date"]));
                            Property.FYear = DateTime.Parse(Val.ToString(DRow["advance_rec_date"])).Year;
                            Property.FMonth = DateTime.Parse(Val.ToString(DRow["advance_rec_date"])).Month;
                            Property.Entry_Date = Val.DBDate(DRow["advance_rec_date"].ToString());
                            Property.Entry_Time = Val.GetFullTime12();
                        }
                        else
                        {
                            Property.Advance_Recovery_Date = null;
                        }

                        Property.exchange_rate = Val.ToDecimal(m_exchange_rate);

                        Property = ObjLoan.Save(Property, DLL.GlobalDec.EnumTran.Continue, Conn);

                        Int64 Loan_ID = Val.ToInt64(Property.Loan_ID);

                        if (Val.Val(DRow["recovery_amount"]) > 0 || Val.Val(DRow["advance_amount"]) > 0)
                        {
                            IncomeEntry_MasterProperty IncomeEntryMasterProperty = new IncomeEntry_MasterProperty();
                            IncomeEntryMasterProperty.income_id = Val.ToInt64(0);

                            IncomeEntryMasterProperty.ledger_id = Val.ToInt(DRow["ledger_id"]);
                            IncomeEntryMasterProperty.bank_id = Val.ToInt32(0);
                            IncomeEntryMasterProperty.transaction_type = Val.ToString(DRow["payment_type"]);

                            IncomeEntryMasterProperty.special_remarks = Val.ToString("");
                            IncomeEntryMasterProperty.client_remarks = Val.ToString("");
                            IncomeEntryMasterProperty.payment_remarks = Val.ToString("");
                            IncomeEntryMasterProperty.invoice_id = Val.ToInt(0);
                            IncomeEntryMasterProperty.exchange_rate = Val.ToDecimal(m_exchange_rate);
                            IncomeEntryMasterProperty.currency_id = Val.ToInt32(GlobalDec.gEmployeeProperty.currency_id);

                            if (Val.ToString(DRow["payment_type"]) == "Cash")
                            {
                                IncomeEntryMasterProperty.against_ledger_id = Val.ToInt64(Against_Ledger_Id_Cash);
                            }
                            else if (Val.ToString(DRow["payment_type"]) == "Bank")
                            {
                                IncomeEntryMasterProperty.against_ledger_id = Val.ToInt64(Against_Ledger_Id_Bank);
                            }

                            IncomeEntryMasterProperty.loan_id = Loan_ID;

                            if (Val.Val(DRow["advance_amount"]) > 0)
                            {
                                IncomeEntryMasterProperty.income_date = Val.DBDate(DRow["advance_date"].ToString());
                                IncomeEntryMasterProperty.amount = Val.ToDecimal(DRow["advance_amount"]);
                                IncomeEntryMasterProperty.remarks = Val.ToString("Loan Advance");

                                IntRes = objIncomeEntry.IncomeEntry_Save(IncomeEntryMasterProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                            }
                            else if (Val.Val(DRow["recovery_amount"]) > 0)
                            {
                                IncomeEntryMasterProperty.income_date = Val.DBDate(DRow["recovery_date"].ToString());
                                IncomeEntryMasterProperty.amount = Val.ToDecimal(DRow["recovery_amount"]);
                                IncomeEntryMasterProperty.remarks = Val.ToString("Loan Recovery");
                                IntRes = objIncomeEntry.IncomeEntry_Save(IncomeEntryMasterProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                            }
                        }
                        if (Val.Val(DRow["new_given"]) > 0 || Val.Val(DRow["advance_rec_amount"]) > 0)
                        {
                            ExpenseEntry_MasterProperty ExpenseEntryMasterProperty = new ExpenseEntry_MasterProperty();
                            ExpenseEntryMasterProperty.expense_id = Val.ToInt64(0);
                            ExpenseEntryMasterProperty.ledger_id = Val.ToInt(DRow["ledger_id"]);
                            ExpenseEntryMasterProperty.transaction_type = Val.ToString(DRow["payment_type"]);
                            ExpenseEntryMasterProperty.bank_id = Val.ToInt32(0);
                            ExpenseEntryMasterProperty.special_remarks = Val.ToString("");
                            ExpenseEntryMasterProperty.purchase_id = Val.ToInt(0);
                            ExpenseEntryMasterProperty.exchange_rate = Val.ToDecimal(m_exchange_rate);
                            ExpenseEntryMasterProperty.currency_id = Val.ToInt32(GlobalDec.gEmployeeProperty.currency_id);

                            if (Val.ToString(DRow["payment_type"]) == "Cash")
                            {
                                ExpenseEntryMasterProperty.against_ledger_id = Val.ToInt64(Against_Ledger_Id_Cash);
                            }
                            else if (Val.ToString(DRow["payment_type"]) == "Bank")
                            {
                                ExpenseEntryMasterProperty.against_ledger_id = Val.ToInt64(Against_Ledger_Id_Bank);
                            }

                            ExpenseEntryMasterProperty.loan_id = Loan_ID;

                            if (Val.Val(DRow["new_given"]) > 0)
                            {
                                ExpenseEntryMasterProperty.expense_date = Val.DBDate(DRow["entry_date"].ToString());
                                ExpenseEntryMasterProperty.amount = Val.ToDecimal(DRow["new_given"]);
                                ExpenseEntryMasterProperty.remarks = Val.ToString("Loan Given");
                                IntRes = objExpenseEntry.ExpenseEntry_Save(ExpenseEntryMasterProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                            }
                            else if (Val.Val(DRow["advance_rec_amount"]) > 0)
                            {
                                ExpenseEntryMasterProperty.expense_date = Val.DBDate(DRow["advance_rec_date"].ToString());
                                ExpenseEntryMasterProperty.amount = Val.ToDecimal(DRow["advance_rec_amount"]);
                                ExpenseEntryMasterProperty.remarks = Val.ToString("Advance Recovery");
                                IntRes = objExpenseEntry.ExpenseEntry_Save(ExpenseEntryMasterProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                            }
                        }
                    }
                    if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                    {
                        Conn.Inter1.Commit();
                    }
                    else
                    {
                        Conn.Inter2.Commit();
                    }
                }
                catch (Exception ex)
                {
                    IntRes = -1;
                    if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                    {
                        Conn.Inter1.Rollback();
                    }
                    else
                    {
                        Conn.Inter2.Rollback();
                    }
                    Conn = null;
                    General.ShowErrors(ex.ToString());
                    return;
                }
                finally
                {
                    Property = null;
                }
            }
            catch (Exception ex)
            {
                IntRes = -1;
                if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                {
                    Conn.Inter1.Rollback();
                }
                else
                {
                    Conn.Inter2.Rollback();
                }
                Conn = null;
                Global.Message(ex.ToString());
                if (ex.InnerException != null)
                {
                    Global.Message(ex.InnerException.ToString());
                }
            }
        }
        private void backgroundWorker_LoanEntry_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (IntRes == -1 || IntRes == 0)
                {
                    Global.Message("ERROR IN SAVE, please check");
                }
                else
                {
                    Global.Message("Record Saved Successfully");
                    btnClear_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }
        #endregion

        #region "Function"
        public void AddNewEMI(DataRow DRow)
        {
            if (GrdDetLoan.IsLastRow)
            {
                DataRow DRNew = DTabLoan.NewRow();

                int IntYear = Val.ToInt(DRow["fyear"]);
                int IntMonth = Val.ToInt(DRow["fmonth"]);
                IntMonth = IntMonth + 1;
                if (IntMonth > 12)
                {
                    IntMonth = 1;
                    IntYear = IntYear + 1;
                }
                double DouOpening = Val.Val(DRow["closing_amount"]);
                double DouEMIAmount = Val.Val(DRow["with_emi_amount"]);
                double DouClosing = 0;
                if (DouOpening < DouEMIAmount)
                {
                    DouEMIAmount = DouOpening;
                }

                DouClosing = Math.Round(DouOpening - DouEMIAmount, 0);

                DRNew["loan_id"] = 0;
                DRNew["fyear"] = IntYear.ToString();
                DRNew["fmonth"] = IntMonth.ToString();
                DRNew["opening_amount"] = DouOpening;
                DRNew["entry_date"] = DateTime.Now.ToShortDateString();
                DRNew["ledger_id"] = Val.ToString(DRow["ledger_id"]);
                DRNew["ledger_name"] = Val.ToString(DRow["ledger_name"]);
                DRNew["department_id"] = Val.ToInt(GlobalDec.gEmployeeProperty.department_id);
                DRNew["department_name"] = Val.ToString(GlobalDec.gEmployeeProperty.department_name);
                DRNew["company_id"] = Val.ToString(DRow["company_id"]);
                DRNew["company_name"] = Val.ToString(DRow["company_name"]);
                DRNew["branch_id"] = Val.ToString(DRow["branch_id"]);
                DRNew["branch_name"] = Val.ToString(DRow["branch_name"]);
                DRNew["location_id"] = Val.ToString(DRow["location_id"]);
                DRNew["location_name"] = Val.ToString(DRow["location_name"]);
                DRNew["with_emi_amount"] = Val.Val(DRow["with_emi_amount"]);
                DRNew["closing_amount"] = DouClosing;
                DRNew["is_paid"] = 0;

                DTabLoan.Rows.Add(DRNew);

                GrdDetLoan.PostEditor();
                GrdDetLoan.RefreshData();
                GrdDetLoan.FocusedRowHandle = GrdDetLoan.RowCount - 1;
                GrdDetLoan.FocusedColumn = GrdDetLoan.VisibleColumns[0];
            }
        }
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (m_blnsave)
                {

                    if (Val.Trim(ChkCmbCompanyName.Properties.GetCheckedItems()) == "")
                    {
                        lstError.Add(new ListError(5, "Please Select At Least One Company To Save Loan Detail"));
                        if (!blnFocus)
                        {
                            blnFocus = true;

                        }
                    }
                    if (Val.Trim(ChkCmbBranchName.Properties.GetCheckedItems()) == "")
                    {
                        lstError.Add(new ListError(5, "Please Select At Least One Branch To Save Loan Detail"));
                        if (!blnFocus)
                        {
                            blnFocus = true;

                        }
                    }
                    if (Val.Trim(ChkCmbLocationName.Properties.GetCheckedItems()) == "")
                    {
                        lstError.Add(new ListError(5, "Please Select At Least One Location To Save Loan Detail"));
                        if (!blnFocus)
                        {
                            blnFocus = true;

                        }
                    }
                }
            }

            catch (Exception ex)
            {
                lstError.Add(new ListError(ex));
            }
            return (!(BLL.General.ShowErrors(lstError)));
        }
        public System.Data.DataTable WorksheetToDataTable(ExcelWorksheet ws, bool hasHeader = true)
        {
            System.Data.DataTable dt = new System.Data.DataTable(ws.Name);
            int totalCols = ws.Dimension.End.Column;
            int totalRows = ws.Dimension.End.Row;
            int startRow = hasHeader ? 2 : 1;
            ExcelRange wsRow;
            DataRow dr;
            foreach (var firstRowCell in ws.Cells[1, 1, 1, totalCols])
            {
                dt.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
            }

            for (int rowNum = startRow; rowNum <= totalRows; rowNum++)
            {
                wsRow = ws.Cells[rowNum, 1, rowNum, totalCols];
                dr = dt.NewRow();
                foreach (var cell in wsRow)
                {
                    dr[cell.Start.Column - 1] = cell.Text;
                }

                dt.Rows.Add(dr);
            }

            return dt;
        }
        private void Export(string format, string dlgHeader, string dlgFilter)
        {
            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = format;
                svDialog.Title = dlgHeader;
                svDialog.FileName = "Report";
                svDialog.Filter = dlgFilter;
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    string Filepath = svDialog.FileName;

                    switch (format)
                    {
                        case "pdf":
                            GrdDetLoan.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            GrdDetLoan.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            GrdDetLoan.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            GrdDetLoan.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            GrdDetLoan.ExportToText(Filepath);
                            break;
                        case "html":
                            GrdDetLoan.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            GrdDetLoan.ExportToCsv(Filepath);
                            break;
                    }

                    if (format.Equals(Exports.xlsx.ToString()))
                    {
                        if (Global.Confirm("Export Done\n\nYou Want To Open Excel File ?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(Filepath);
                        }
                    }
                    else if (format.Equals(Exports.pdf.ToString()))
                    {
                        if (Global.Confirm("Export Done\n\nYou Want To Open PDF File ?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(Filepath);
                        }
                    }
                    else
                    {
                        if (Global.Confirm("Export Done\n\nYou Want To Open File ?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(Filepath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString(), "Error in Export");
            }
        }
        #endregion

        #region Export Grid
        private void MNExportExcel_Click(object sender, EventArgs e)
        {
            Export("xlsx", "Export to Excel", "Excel files 97-2003 (Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*");
        }
        private void MNExportPDF_Click(object sender, EventArgs e)
        {
            Export("pdf", "Export Report to PDF", "PDF (*.PDF)|*.PDF");
        }
        private void MNExportTEXT_Click(object sender, EventArgs e)
        {
            Export("txt", "Export to Text", "Text files (*.txt)|*.txt|All files (*.*)|*.*");
        }

        private void MNExportHTML_Click(object sender, EventArgs e)
        {
            Export("html", "Export to HTML", "Html files (*.html)|*.html|Htm files (*.htm)|*.htm");
        }

        private void MNExportRTF_Click(object sender, EventArgs e)
        {
            Export("rtf", "Export to RTF", "Word (*.doc) |*.doc;*.rtf|(*.txt) |*.txt|(*.*) |*.*");
        }

        private void MNExportCSV_Click(object sender, EventArgs e)
        {
            Export("csv", "Export Report to CSVB", "csv (*.csv)|*.csv");
        }
        #endregion
    }
}
