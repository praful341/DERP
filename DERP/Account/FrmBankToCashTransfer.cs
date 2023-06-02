using BLL;
using BLL.FunctionClasses.Account;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Transaction;
using BLL.PropertyClasses.Account;
using DERP.Class;
using DERP.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace DREP.Account
{
    public partial class FrmBankToCashTransfer : DevExpress.XtraEditors.XtraForm
    {
        BLL.FormPer ObjPer = new BLL.FormPer();
        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents = new BLL.FormEvents();
        BLL.Validation Val = new BLL.Validation();
        ExpenseEntryMaster objExpenseEntry = new ExpenseEntryMaster();
        PurchaseInward objPurchase = new PurchaseInward();
        IncomeEntryMaster objIncomeEntry = new IncomeEntryMaster();
        int m_numForm_id = 0;
        decimal F_balance = 0;
        decimal T_balance = 0;
        public FrmBankToCashTransfer()
        {
            InitializeComponent();
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
            //luePurchaseNo.EditValue = null;
            lueFromLedger.EditValue = null;
            lueToLedger.EditValue = null;
            lueBank.EditValue = null;
            txtRemark.Text = "";
            //txtSpecialRemark.Text = "";
            txtAmount.Text = "";
            cmbFromType.SelectedIndex = 0;
            cmbToType.SelectedIndex = 0;
            DTPEntryDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPEntryDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPEntryDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPEntryDate.Properties.CharacterCasing = CharacterCasing.Upper;
            DTPEntryDate.EditValue = DateTime.Now;
            lueFromLedger.Focus();
        }
        private void MNExportExcel_Click(object sender, EventArgs e)
        {
            //Global.Export("xlsx", dgvIncomeEntryMaster);
        }

        #region Validation

        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (Val.ToInt(lueFromLedger.EditValue) <= 0)
                {
                    lstError.Add(new ListError(12, "From Ledger"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueFromLedger.Focus();
                    }
                }
                if (Val.ToInt(lueToLedger.EditValue) < 0)
                {
                    lstError.Add(new ListError(12, "To Ledger"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueFromLedger.Focus();
                    }
                }
                if (Val.ToInt(lueFromLedger.EditValue) == Val.ToInt(lueToLedger.EditValue))
                {
                    lstError.Add(new ListError(5, "From And To Ledger Not Same."));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueFromLedger.Focus();
                    }
                }
                if (cmbFromType.Text == "Select" || cmbFromType.Text == "")
                {
                    lstError.Add(new ListError(5, "From Type are Require"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        cmbFromType.Focus();
                    }
                }
                if (cmbToType.Text == "Select" || cmbToType.Text == "")
                {
                    lstError.Add(new ListError(5, "To Type are Require"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        cmbToType.Focus();
                    }
                }
                if (txtAmount.Text.Length == 0 || txtAmount.Text == "")
                {
                    lstError.Add(new ListError(12, "Amount"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtAmount.Focus();
                    }
                }
                if (Val.ToDecimal(txtAmount.Text) > Val.ToDecimal(lblFBalance.Text))
                {
                    lstError.Add(new ListError(5, "Transfer Amount not greater than Ledger Balance."));
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

                Int64 Against_Ledger_Id_Cash = objIncomeEntry.ISLadgerName_GetData("CASH BALANCE");
                Int64 Against_Ledger_Id_Bank = objIncomeEntry.ISLadgerName_GetData("BANK BALANCE");
                Int64 Against_Ledger_Id_Patty_Cash = objIncomeEntry.ISLadgerName_GetData("PATTY CASH");

                if (Against_Ledger_Id_Cash == 0 || Against_Ledger_Id_Bank == 0 || Against_Ledger_Id_Patty_Cash == 0)
                {
                    Global.Message("Ledger Setting not Proper");
                    return;
                }


                //ExpenseEntryMasterProperty.expense_id = Val.ToInt64(lblMode.Tag);
                ExpenseEntryMasterProperty.expense_date = Val.DBDate(DTPEntryDate.Text);

                ExpenseEntryMasterProperty.ledger_id = Val.ToInt64(lueFromLedger.EditValue);
                ExpenseEntryMasterProperty.to_ledger_id = Val.ToInt64(lueToLedger.EditValue);
                ExpenseEntryMasterProperty.from_against_ledger_id = Val.ToInt64(lueFromLedger.EditValue);
                ExpenseEntryMasterProperty.to_against_ledger_id = Val.ToInt64(lueToLedger.EditValue);

                ExpenseEntryMasterProperty.transaction_type = Val.ToString(cmbFromType.EditValue);
                ExpenseEntryMasterProperty.to_transaction_type = Val.ToString(cmbToType.EditValue);
                ExpenseEntryMasterProperty.bank_id = Val.ToInt32(lueBank.EditValue);
                ExpenseEntryMasterProperty.amount = Val.ToDecimal(txtAmount.Text);
                ExpenseEntryMasterProperty.remarks = Val.ToString(txtRemark.Text) + " " + lueFromLedger.Text + "_to_" + lueToLedger.Text + " Transfer";


                CurrencyMaster objCurrency = new CurrencyMaster();
                DataTable DTab_Rate = objCurrency.GetCurrencyRate(Val.ToInt(GlobalDec.gEmployeeProperty.currency_id), Val.ToInt(GlobalDec.gEmployeeProperty.currency_id));

                if (DTab_Rate.Rows.Count > 0)
                {
                    ExpenseEntryMasterProperty.exchange_rate = Val.ToDecimal(DTab_Rate.Rows[0]["rate"].ToString());
                }
                else
                {
                    Global.Message("Currency Rate Not Found..");
                    return;
                }

                //if (Val.ToString(lueFromLedger.Text) == "CASH BALANCE")
                //{
                //    ExpenseEntryMasterProperty.from_against_ledger_id = Val.ToInt64(Against_Ledger_Id_Bank);
                //}
                //if (Val.ToString(lueFromLedger.Text) == "BANK BALANCE")
                //{
                //    ExpenseEntryMasterProperty.from_against_ledger_id = Val.ToInt64(Against_Ledger_Id_Cash);
                //}
                //if (Val.ToString(lueToLedger.Text) == "CASH BALANCE")
                //{
                //    ExpenseEntryMasterProperty.to_against_ledger_id = Val.ToInt64(Against_Ledger_Id_Bank);
                //}
                //if (Val.ToString(lueToLedger.Text) == "BANK BALANCE")
                //{
                //    ExpenseEntryMasterProperty.to_against_ledger_id = Val.ToInt64(Against_Ledger_Id_Cash);
                //}

                //if (Val.ToString(lueToLedger.Text) == "PATTY CASH")
                //{
                //    ExpenseEntryMasterProperty.to_against_ledger_id = Val.ToInt64(Against_Ledger_Id_Cash);
                //    ExpenseEntryMasterProperty.from_against_ledger_id = Val.ToInt64(Against_Ledger_Id_Patty_Cash);
                //}

                ExpenseEntryMasterProperty.form_id = m_numForm_id;

                IntRes = objExpenseEntry.BankToCash_Save(ExpenseEntryMasterProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                Conn.Inter1.Commit();

                if (IntRes == -1)
                {
                    Global.Confirm("Error In Save Bank To Cash Entry Details");
                    lueFromLedger.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Bank To Cash Transfer Data Save Successfully");
                    }
                    else
                    {
                        Global.Confirm("Bank To Cash Transfer Data Update Successfully");
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
                Global.Message(ex.ToString());
                if (ex.InnerException != null)
                {
                    Global.Message(ex.InnerException.ToString());
                }
            }
            finally
            {
                ExpenseEntryMasterProperty = null;
            }
        }

        public void GetData()
        {
            DataTable DTab = objExpenseEntry.Expense_Entry_GetData_Search();
            //grdIncomeEntryMaster.DataSource = DTab;
            //dgvIncomeEntryMaster.BestFitColumns();
        }
        private void FrmBankToCash_Load(object sender, EventArgs e)
        {
            Global.LOOKUP_Bank_CashLedger(lueFromLedger, GlobalDec.gEmployeeProperty.location_id);
            Global.LOOKUP_Bank_CashLedger(lueToLedger, GlobalDec.gEmployeeProperty.location_id);
            Global.LOOKUPBank(lueBank);

            DataTable dtbDetail = new DataTable();
            dtbDetail = objPurchase.GetPurchaseNo();

            if (dtbDetail.Rows.Count > 0)
            {


            }

            DTPEntryDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPEntryDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPEntryDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPEntryDate.Properties.CharacterCasing = CharacterCasing.Upper;
            DTPEntryDate.EditValue = DateTime.Now;

            GetData();
            btnClear_Click(btnClear, null);
        }

        //private void dgvCountryMaster_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        //{
        //    if (e.RowHandle >= 0)
        //    {
        //        if (e.Clicks == 2)
        //        {
        //            DataRow Drow = dgvIncomeEntryMaster.GetDataRow(e.RowHandle);
        //            lblMode.Text = "Edit Mode";
        //            //lblMode.Tag = Val.ToInt64(Drow["expense_id"]);
        //            lueBank.EditValue = Val.ToInt32(Drow["bank_id"]);
        //            lueFromLedger.EditValue = Val.ToInt32(Drow["ledger_id"]);
        //            cmbFromType.EditValue = Convert.ToString(Drow["transaction_type"]);
        //            txtRemark.Text = Val.ToString(Drow["remarks"]);
        //            DTPEntryDate.EditValue = Val.DBDate(Drow["expense_date"].ToString());
        //            txtAmount.Text = Val.ToDecimal(Drow["amount"]).ToString();
        //        }
        //    }
        //}

        private void LookupPartyName_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmLedgerMaster frmCnt = new FrmLedgerMaster();
                frmCnt.ShowDialog();
                Global.LOOKUPLedger(lueFromLedger, GlobalDec.gEmployeeProperty.location_id);
            }
        }
        private void lueToLedger_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmLedgerMaster frmCnt = new FrmLedgerMaster();
                frmCnt.ShowDialog();
                Global.LOOKUPLedger(lueToLedger, GlobalDec.gEmployeeProperty.location_id);
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

        private void LookupLedgerId_EditValueChanged(object sender, EventArgs e)
        {
            DataTable DtFbalance = new DataTable();
            if (Val.ToInt(lueFromLedger.EditValue) != 0 && Val.ToInt(lueFromLedger.EditValue).ToString() != null)
            {
                DtFbalance = objExpenseEntry.LedgerBalanceGetData(Val.ToInt(lueFromLedger.EditValue));
                if (DtFbalance.Rows.Count > 0)
                {
                    F_balance = Val.ToDecimal(DtFbalance.Rows[0]["balance"]);
                }
                else
                {
                    F_balance = Val.ToDecimal(0);
                }


            }
            else
            {
                F_balance = 0;
            }
            lblFBalance.Text = Val.ToString(F_balance);
        }

        private void lueToLedger_EditValueChanged(object sender, EventArgs e)
        {
            DataTable DtTbalance = new DataTable();
            if (Val.ToInt(lueToLedger.EditValue) != 0 && Val.ToInt(lueToLedger.EditValue).ToString() != null)
            {
                DtTbalance = objExpenseEntry.LedgerBalanceGetData(Val.ToInt(lueToLedger.EditValue));
                if (DtTbalance.Rows.Count > 0)
                {
                    T_balance = Val.ToDecimal(DtTbalance.Rows[0]["balance"]);
                }
                else
                {
                    T_balance = Val.ToDecimal(0);
                }


            }
            else
            {
                T_balance = 0;
            }
            lblTBalance.Text = Val.ToString(T_balance);
        }
    }
}
