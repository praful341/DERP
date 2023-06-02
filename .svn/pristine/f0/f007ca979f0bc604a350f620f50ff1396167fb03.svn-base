using BLL;
using BLL.FunctionClasses.Account;
using BLL.FunctionClasses.Master;
using BLL.PropertyClasses.Account;
using DERP.Class;
using System;
using System.Collections.Generic;
using System.Data;


namespace DERP.Account
{
    public partial class FrmBrokeragePayable : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        FormEvents objBOFormEvents = new FormEvents();
        Validation Val = new Validation();
        BLL.FormPer ObjPer = new BLL.FormPer();
        BrokeragePayable objBrokeragePay = new BrokeragePayable();
        DataTable m_dtbPaymenttype;
        DataTable m_DTab;
        int m_numForm_id = 0;
        IncomeEntryMaster objIncomeEntry = new IncomeEntryMaster();

        #endregion

        #region Constructor
        public FrmBrokeragePayable()
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
            objBOFormEvents.ObjToDispose.Add("");
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion

        #region Events
        private void btnShow_Click(object sender, EventArgs e)
        {
            GetData();
        }
        private void FrmBrokeragePayable_Load(object sender, EventArgs e)
        {
            m_dtbPaymenttype = new DataTable();

            m_dtbPaymenttype.Columns.Add("payment_type");
            m_dtbPaymenttype.Rows.Add("Cash");
            m_dtbPaymenttype.Rows.Add("Bank");

            repositoryItemLuePaymentType.DataSource = m_dtbPaymenttype;
            repositoryItemLuePaymentType.ValueMember = "payment_type";
            repositoryItemLuePaymentType.DisplayMember = "payment_type";

        }

        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (grdBrokeragePayable.DataSource != null)
                {
                    m_DTab = (DataTable)grdBrokeragePayable.DataSource;

                    foreach (DataRow drw in m_DTab.Rows)
                    {
                        if (Val.ToDecimal(drw["payable"]) > 0)
                        {
                            if (Val.ToString(drw["payment_type"]) == "")
                            {
                                lstError.Add(new ListError(13, "Payment Type"));
                                if (!blnFocus)
                                {
                                    blnFocus = true;
                                }
                            }
                            //if (Val.ToString(drw["Receive_date"]) == "")
                            //{
                            //    lstError.Add(new ListError(13, "Receive Date"));
                            //    if (!blnFocus)
                            //    {
                            //        blnFocus = true;
                            //    }
                            //}                            

                            //var result = DateTime.Compare(Convert.ToDateTime(drw["Receive_date"]), DateTime.Today);
                            //if (result > 0)
                            //{
                            //    lstError.Add(new ListError(5, "Receive Date Not Be Greater Than Today Date"));
                            //    if (!blnFocus)
                            //    {
                            //        blnFocus = true;
                            //    }
                            //}
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateDetails())
            {
                return;
            }

            Brokerage_PayableProperty BrokeragePayableProperty = new Brokerage_PayableProperty();
            //BrokeragePayable objParty = new PartyMaster();
            int IntRes = 0;
            try
            {
                if (grdBrokeragePayable.DataSource != null)
                {
                    m_DTab = (DataTable)grdBrokeragePayable.DataSource;

                    Int64 Against_Ledger_Id_Cash = objIncomeEntry.ISLadgerName_GetData("CASH BALANCE");
                    Int64 Against_Ledger_Id_Bank = objIncomeEntry.ISLadgerName_GetData("BANK BALANCE");

                    if (Against_Ledger_Id_Cash == 0 || Against_Ledger_Id_Bank == 0)
                    {
                        Global.Message("Cash Balance Or Bank Balance Leger Not Set ");
                        return;
                    }
                    CurrencyMaster objCurrency = new CurrencyMaster();
                    DataTable DTab_Rate = objCurrency.GetCurrencyRate(Val.ToInt(GlobalDec.gEmployeeProperty.currency_id), Val.ToInt(GlobalDec.gEmployeeProperty.currency_id));

                    if (DTab_Rate.Rows.Count > 0)
                    {
                        BrokeragePayableProperty.exchange_rate = Val.ToDecimal(DTab_Rate.Rows[0]["rate"].ToString());
                    }
                    else
                    {
                        BrokeragePayableProperty.exchange_rate = 0;
                    }
                    foreach (DataRow drw in m_DTab.Rows)
                    {
                        if (Val.ToDecimal(drw["payable"]) > 0)
                        {
                            BrokeragePayableProperty.ledger_id = Val.ToInt(drw["ledger_id"]);
                            BrokeragePayableProperty.payable = Val.ToDecimal(drw["payable"]);
                            BrokeragePayableProperty.payment_type = Val.ToString(drw["payment_type"]);
                            BrokeragePayableProperty.remarks = Val.ToString(drw["remark"]);
                            BrokeragePayableProperty.expense_date = Val.ToString(drw["Receive_date"]);
                            if (Val.ToString(drw["payment_type"]) == "Cash")
                            {
                                BrokeragePayableProperty.against_ledger_id = Val.ToInt64(Against_Ledger_Id_Cash);
                            }
                            else if (Val.ToString(drw["payment_type"]) == "Bank")
                            {
                                BrokeragePayableProperty.against_ledger_id = Val.ToInt64(Against_Ledger_Id_Bank);
                            }
                            BrokeragePayableProperty.form_id = m_numForm_id;

                            IntRes = objBrokeragePay.Save(BrokeragePayableProperty);
                        }
                    }

                    //IntRes = objBrokeragePay.Save(BrokeragePayableProperty);

                    if (IntRes == -1)
                    {
                        Global.Confirm("Error In Save Brokerage Payment");
                    }
                    else
                    {
                        Global.Confirm("Brokerage Payment Save Succesfully");
                        grdBrokeragePayable.DataSource = null;
                    }
                }
                else
                {
                    Global.Confirm("No data found");
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
            }
        }
        private void dgvBrokeragePayable_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (Val.ToDecimal(dgvBrokeragePayable.GetFocusedRowCellValue("amount")) < Val.ToDecimal(dgvBrokeragePayable.GetFocusedRowCellValue("payable")))
            {
                Global.Confirm("Payable amount not more than amount");
            }
        }
        #endregion

        #region Functions
        public void GetData()
        {
            m_DTab = objBrokeragePay.GetData();
            grdBrokeragePayable.DataSource = m_DTab;
        }

        #endregion
    }
}
