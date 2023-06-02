using BLL;
using BLL.FunctionClasses.Account;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Transaction;
using BLL.PropertyClasses.Account;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using DERP.Master;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DREP.Account
{
    public partial class FrmExpenseEntry : DevExpress.XtraEditors.XtraForm
    {
        BLL.FormPer ObjPer = new BLL.FormPer();
        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents = new BLL.FormEvents();
        BLL.Validation Val = new BLL.Validation();
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        Control _NextEnteredControl;
        private List<Control> _tabControls;

        ExpenseEntryMaster objExpenseEntry = new ExpenseEntryMaster();
        PurchaseInward objPurchase = new PurchaseInward();
        IncomeEntryMaster objIncomeEntry = new IncomeEntryMaster();
        DataTable DtControlSettings;
        SaleInvoice objSaleInvoice = new SaleInvoice();
        DataTable m_dtbCurrencyType = new DataTable();
        int m_numForm_id = 0;
        public FrmExpenseEntry()
        {
            InitializeComponent();

            _NextEnteredControl = new Control();
            _tabControls = new List<Control>();

            DtControlSettings = new DataTable();
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
            Val.frmGenSet(this);
            AttachFormEvents();

            if (Global.HideFormControls(Val.ToInt(ObjPer.form_id), this) != "")
            {
                Global.Message("Select First User Setting...Please Contact to Administrator...");
                return;
            }

            ControlSettingDT(Val.ToInt(ObjPer.form_id), this);
            AddGotFocusListener(this);
            this.KeyPreview = true;

            TabControlsToList(this.Controls);
            _tabControls = _tabControls.OrderBy(x => x.TabIndex).ToList();

            this.Show();
        }
        private void AttachFormEvents()
        {
            objBOFormEvents.CurForm = this;
            objBOFormEvents.FormKeyPress = true;
            objBOFormEvents.FormKeyDown = true;
            objBOFormEvents.FormResize = true;
            objBOFormEvents.FormClosing = true;
            objBOFormEvents.ObjToDispose.Add(objExpenseEntry);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lblMode.Tag = 0;
            lblMode.Text = "Add Mode";
            luePurchaseNo.EditValue = null;
            LookupLedgerId.EditValue = null;
            lueHead.EditValue = null;
            lueBank.EditValue = null;
            lueInvoiceNo.EditValue = null;
            txtRemark.Text = "";
            txtSpecialRemark.Text = "";
            txtAmount.Text = "";
            lueCurrency.EditValue = null;
            txtExchangeRate.Text = "";
            CmbTransactionType.SelectedIndex = 0;
            DTPEntryDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPEntryDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPEntryDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPEntryDate.Properties.CharacterCasing = CharacterCasing.Upper;
            DTPEntryDate.EditValue = DateTime.Now;
            lueCurrency.EditValue = GlobalDec.gEmployeeProperty.currency_id;
            LookupLedgerId.Focus();
        }
        private void MNExportExcel_Click(object sender, EventArgs e)
        {
            Global.Export("xlsx", dgvIncomeEntryMaster);
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            GetData();
        }
        #region Validation

        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (LookupLedgerId.ItemIndex < 0)
                {
                    lstError.Add(new ListError(12, "Ledger"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        LookupLedgerId.Focus();
                    }
                }

                if (CmbTransactionType.Text == "Select" || CmbTransactionType.Text == "")
                {
                    lstError.Add(new ListError(23, "Cash / Bank"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        CmbTransactionType.Focus();
                    }
                }
                if (txtAmount.Text.Length == 0 || txtAmount.Text == "")
                {
                    lstError.Add(new ListError(23, "Amount"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtAmount.Focus();
                    }
                }
                if (DTPEntryDate.Text == string.Empty)
                {
                    lstError.Add(new ListError(13, "Expense Date"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        DTPEntryDate.Focus();
                    }
                }
                if (lueCurrency.ItemIndex < 0)
                {
                    lstError.Add(new ListError(12, "Currency"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueCurrency.Focus();
                    }
                }
                if (txtExchangeRate.Text.Length == 0 || txtExchangeRate.Text == "")
                {
                    lstError.Add(new ListError(23, "Ex. Rate"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtExchangeRate.Focus();
                    }
                }
            }

            catch (Exception ex)
            {
                lstError.Add(new ListError(ex));
            }
            return (!(BLL.General.ShowErrors(lstError)));
        }

        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            ExpenseEntry_MasterProperty ExpenseEntryMasterProperty = new ExpenseEntry_MasterProperty();
            int IntRes = 0;
            try
            {
                if (!ValidateDetails())
                {
                    return;
                }
                Conn = new BeginTranConnection(true, false);

                ExpenseEntryMasterProperty.expense_id = Val.ToInt64(lblMode.Tag);
                ExpenseEntryMasterProperty.expense_date = Val.DBDate(DTPEntryDate.Text);
                ExpenseEntryMasterProperty.ledger_id = Val.ToInt64(LookupLedgerId.EditValue);
                ExpenseEntryMasterProperty.transaction_type = Val.ToString(CmbTransactionType.EditValue);
                ExpenseEntryMasterProperty.bank_id = Val.ToInt32(lueBank.EditValue);
                ExpenseEntryMasterProperty.head_id = Val.ToInt32(lueHead.EditValue);
                ExpenseEntryMasterProperty.amount = Val.ToDecimal(txtAmount.Text);
                ExpenseEntryMasterProperty.remarks = Val.ToString(txtRemark.Text);
                ExpenseEntryMasterProperty.special_remarks = Val.ToString(txtSpecialRemark.Text);
                ExpenseEntryMasterProperty.invoice_id = Val.ToInt(lueInvoiceNo.EditValue);
                ExpenseEntryMasterProperty.currency_id = Val.ToInt(lueCurrency.EditValue);
                ExpenseEntryMasterProperty.exchange_rate = Val.ToDecimal(txtExchangeRate.Text);
                ExpenseEntryMasterProperty.purchase_id = Val.ToInt(luePurchaseNo.EditValue);

                Int64 Against_Ledger_Id_Cash = objIncomeEntry.ISLadgerName_GetData("CASH BALANCE");
                Int64 Against_Ledger_Id_Bank = objIncomeEntry.ISLadgerName_GetData("BANK BALANCE");
                Int64 Against_Ledger_Id_Patty_Cash = objIncomeEntry.ISLadgerName_GetData("PATTY CASH");

                if (Val.ToString(CmbTransactionType.EditValue) == "Cash")
                {
                    ExpenseEntryMasterProperty.against_ledger_id = Val.ToInt64(Against_Ledger_Id_Cash);
                }
                else if (Val.ToString(CmbTransactionType.EditValue) == "Bank")
                {
                    ExpenseEntryMasterProperty.against_ledger_id = Val.ToInt64(Against_Ledger_Id_Bank);
                }
                else if (Val.ToString(CmbTransactionType.EditValue) == "Patty Cash")
                {
                    ExpenseEntryMasterProperty.against_ledger_id = Val.ToInt64(Against_Ledger_Id_Patty_Cash);
                }

                if (ExpenseEntryMasterProperty.against_ledger_id == 0)
                {
                    Global.Message("Ledger Setting Not Proper...");
                    return;
                }

                CurrencyMaster objCurrency = new CurrencyMaster();
                DataTable DTab_Rate = objCurrency.GetCurrencyRate(Val.ToInt(GlobalDec.gEmployeeProperty.currency_id), Val.ToInt(GlobalDec.gEmployeeProperty.currency_id));

                if (DTab_Rate.Rows.Count > 0)
                {
                    //ExpenseEntryMasterProperty.exchange_rate = Val.ToDecimal(DTab_Rate.Rows[0]["rate"].ToString());
                }
                else
                {
                    Global.Message("Currency Rate Not Found..");
                    return;
                }

                ExpenseEntryMasterProperty.form_id = m_numForm_id;

                IntRes = objExpenseEntry.ExpenseEntry_Save(ExpenseEntryMasterProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                Conn.Inter1.Commit();
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Save Expense Entry Details");
                    LookupLedgerId.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Expense Entry Master Details Data Save Successfully");
                    }
                    else
                    {
                        Global.Confirm("Expense Entry Master Details Data Update Successfully");
                    }
                    GetData();
                    btnClear_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                IntRes = -1;
                Conn.Inter1.Rollback();
                Conn = null;
                General.ShowErrors(ex.ToString());
                return;
            }
            finally
            {
                ExpenseEntryMasterProperty = null;
            }
        }

        public void GetData()
        {
            DataTable DTab = objExpenseEntry.Expense_Entry_GetData_Search();
            grdIncomeEntryMaster.DataSource = DTab;
            dgvIncomeEntryMaster.BestFitColumns();
        }

        private void FrmCountryMaster_Load(object sender, EventArgs e)
        {
            Global.LOOKUPLedger(LookupLedgerId, GlobalDec.gEmployeeProperty.location_id);
            Global.LOOKUPBank(lueBank);
            Global.LOOKUPHead(lueHead);
            m_dtbCurrencyType = Global.CurrencyType();
            lueCurrency.Properties.DataSource = m_dtbCurrencyType;
            lueCurrency.Properties.ValueMember = "currency_id";
            lueCurrency.Properties.DisplayMember = "currency";
            lueCurrency.EditValue = GlobalDec.gEmployeeProperty.currency_id;
            DataTable dtbDetail = new DataTable();
            dtbDetail = objPurchase.GetPurchaseNo();

            if (dtbDetail.Rows.Count > 0)
            {
                luePurchaseNo.Properties.DataSource = dtbDetail;
                luePurchaseNo.Properties.ValueMember = "purchase_id";
                luePurchaseNo.Properties.DisplayMember = "purchase_no";

            }
            DataTable dtbInvoice = new DataTable();
            dtbDetail = objSaleInvoice.GetInvoiceNo();

            if (dtbDetail.Rows.Count > 0)
            {
                lueInvoiceNo.Properties.DataSource = dtbDetail;
                lueInvoiceNo.Properties.ValueMember = "invoice_id";
                lueInvoiceNo.Properties.DisplayMember = "invoice_no";
            }
            DTPEntryDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPEntryDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPEntryDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPEntryDate.Properties.CharacterCasing = CharacterCasing.Upper;
            DTPEntryDate.EditValue = DateTime.Now;

            GetData();
            btnClear_Click(btnClear, null);
        }

        private void dgvCountryMaster_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (e.Clicks == 2)
                {
                    DataRow Drow = dgvIncomeEntryMaster.GetDataRow(e.RowHandle);
                    lblMode.Text = "Edit Mode";
                    lblMode.Tag = Val.ToInt64(Drow["expense_id"]);
                    lueBank.EditValue = Val.ToInt32(Drow["bank_id"]);
                    lueHead.EditValue = Val.ToInt32(Drow["head_id"]);
                    LookupLedgerId.EditValue = Val.ToInt32(Drow["ledger_id"]);
                    luePurchaseNo.EditValue = Val.ToInt32(Drow["purchase_id"]);
                    lueInvoiceNo.EditValue = Val.ToInt32(Drow["invoice_id"]);
                    CmbTransactionType.EditValue = Convert.ToString(Drow["transaction_type"]);
                    txtRemark.Text = Val.ToString(Drow["remarks"]);
                    lueCurrency.EditValue = Val.ToInt32(Drow["currency_id"]);
                    txtExchangeRate.Text = Val.ToString(Drow["exchange_rate"]);
                    txtSpecialRemark.Text = Val.ToString(Drow["special_remarks"]);
                    DTPEntryDate.EditValue = Val.DBDate(Drow["expense_date"].ToString());
                    txtAmount.Text = Val.ToDecimal(Drow["amount"]).ToString();
                }
            }
        }

        private void LookupPartyName_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmLedgerMaster frmCnt = new FrmLedgerMaster();
                frmCnt.ShowDialog();
                Global.LOOKUPLedger(LookupLedgerId, GlobalDec.gEmployeeProperty.location_id);
            }
        }

        private void lueBank_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmBankMaster frmBank = new FrmBankMaster();
                frmBank.ShowDialog();
                Global.LOOKUPBank(lueBank);
            }
        }

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
    }
}
