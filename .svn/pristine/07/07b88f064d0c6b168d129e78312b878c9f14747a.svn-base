using BLL;
using BLL.FunctionClasses.Account;
using BLL.FunctionClasses.Master;
using BLL.PropertyClasses.Account;
using DERP.Class;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace DERP.Account
{
    public partial class FrmPartyPurchasePayable : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        FormEvents objBOFormEvents = new FormEvents();
        BLL.BeginTranConnection Conn;
        Validation Val = new Validation();
        BLL.FormPer ObjPer = new BLL.FormPer();
        PartyPayable objPartyPay = new PartyPayable();
        CurrencyMaster objCurr = new CurrencyMaster();
        DataTable m_dtbPaymenttype;
        DataTable m_DTab;
        DataTable m_DTabList;
        DataTable m_dtbCurrency;
        double OS_Amount = 0;
        decimal Exchange_Rate = 0;
        int IntRes = 0;
        int m_numForm_id = 0;
        IncomeEntryMaster objIncomeEntry = new IncomeEntryMaster();
        #endregion

        #region Constructor
        public FrmPartyPurchasePayable()
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

        private void FrmBrokeragePayable_Load(object sender, EventArgs e)
        {

            m_dtbPaymenttype = new DataTable();

            m_dtbPaymenttype.Columns.Add("payment_type");
            m_dtbPaymenttype.Rows.Add("Cash");
            m_dtbPaymenttype.Rows.Add("Bank");

            RepPaymentType.DataSource = m_dtbPaymenttype;
            RepPaymentType.ValueMember = "payment_type";
            RepPaymentType.DisplayMember = "payment_type";

            m_dtbCurrency = objCurr.GetData(1);
            RepCurrency.DataSource = m_dtbCurrency;
            RepCurrency.ValueMember = "currency_id";
            RepCurrency.DisplayMember = "currency";
            //RepRecDate.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            //RepRecDate.Mask.EditMask = "dd/MMM/yyyy";
            //RepRecDate.Mask.UseMaskAsDisplayFormat = true;
            //RepRecDate.CharacterCasing = CharacterCasing.Upper;


            GetData();
        }

        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (grdPartyPurchasePayable.DataSource != null)
                {
                    m_DTab = (DataTable)grdPartyPurchasePayable.DataSource;

                    foreach (DataRow drw in m_DTab.Rows)
                    {
                        if (Val.ToDecimal(drw["payable"]) > 0 || Val.ToDecimal(drw["return_amount"]) > 0)
                        {
                            if (Val.ToString(drw["payment_type"]) == "")
                            {
                                lstError.Add(new ListError(13, "Payment Type"));
                                if (!blnFocus)
                                {
                                    blnFocus = true;
                                }
                            }
                            if (Val.ToString(drw["Receive_date"]) == "")
                            {
                                lstError.Add(new ListError(13, "Receive Date"));
                                if (!blnFocus)
                                {
                                    blnFocus = true;
                                }
                            }
                            if (Val.ToInt(drw["currency_id"]) == 0)
                            {
                                lstError.Add(new ListError(13, "Currency"));
                                if (!blnFocus)
                                {
                                    blnFocus = true;
                                }
                            }
                            if (Val.ToDecimal(drw["exchange_rate"]) == 0)
                            {
                                lstError.Add(new ListError(12, "Exchange Rate"));
                                if (!blnFocus)
                                {
                                    blnFocus = true;
                                }
                            }
                            var result = DateTime.Compare(Convert.ToDateTime(drw["Receive_date"]), DateTime.Today);
                            if (result > 0)
                            {
                                lstError.Add(new ListError(5, "Receive Date Not Be Greater Than Today Date"));
                                if (!blnFocus)
                                {
                                    blnFocus = true;
                                }
                            }
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
            ObjPer.SetFormPer();
            if (ObjPer.AllowUpdate == false || ObjPer.AllowInsert == false)
            {
                Global.Message(BLL.GlobalDec.gStrPermissionInsUpdMsg);
                return;
            }

            btnSave.Enabled = false;

            if (!ValidateDetails())
            {
                btnSave.Enabled = true;
                return;
            }

            DialogResult result = MessageBox.Show("Do you want to save data?", "Confirmation", MessageBoxButtons.YesNoCancel);
            if (result != DialogResult.Yes)
            {
                btnSave.Enabled = true;
                return;
            }

            DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
            backgroundWorker_PartyPayable.RunWorkerAsync();

            btnSave.Enabled = true;

            //if (!ValidateDetails())
            //{
            //    blnReturn = false;
            //    return;
            //}
        }

        #endregion
        private void GetData()
        {
            try
            {
                m_DTab = objPartyPay.PartyPurchase_GetData();
                grdPartyPurchasePayable.DataSource = m_DTab;
                dgvPartyPurchasePayable.BestFitColumns();
                decimal numBalance = 0;
                m_DTabList = objPartyPay.PartyPurchase_GetDataList();

                foreach (DataRow Drw in m_DTabList.Rows)
                {
                    numBalance = numBalance + Val.ToDecimal(Drw["CR"]) - Val.ToDecimal(Drw["DR"]);
                    Drw["Balance"] = numBalance;
                }

                grdPaymentList.DataSource = m_DTabList;
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            grdPartyPurchasePayable.DataSource = null;
        }

        private void CalculateGridAmount(int rowindex)
        {
            try
            {
                OS_Amount = Math.Round(Val.ToDouble(dgvPartyPurchasePayable.GetRowCellValue(rowindex, "os_amount")), 3);
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
            }
        }

        private void dgvPartyPayable_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            CalculateGridAmount(dgvPartyPurchasePayable.FocusedRowHandle);
            GridView view = sender as GridView;

            if (view.FocusedColumn.FieldName == "payable")
            {
                string brd = e.Value as string;
                if (Val.ToDouble(brd) > Val.ToDouble(OS_Amount))
                {
                    e.Valid = false;
                    e.ErrorText = "Payable amount not more than OS amount.";
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            Global.Export("xlsx", dgvPartyPurchasePayable);
        }

        private void dgvPaymentList_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;
            if (view == null) return;
            if (e.Column.Caption == "Entry" || e.Column.Caption == "JKK" || e.Column.Caption == "Sale Remark" || e.Column.Caption == "Account Remark")
            {
                view.SetRowCellValue(e.RowHandle, view.Columns["flag"], 1);
            }
            else
            {
                return;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Party_PayableProperty PartyPayableProperty = new Party_PayableProperty();
            //BrokeragePayable objParty = new PartyMaster();
            int IntRes = 0;
            try
            {
                if (grdPaymentList.DataSource != null)
                {
                    m_DTabList = (DataTable)grdPaymentList.DataSource;

                    foreach (DataRow drw in m_DTabList.Rows)
                    {
                        if (Val.ToInt(drw["flag"]) == 1)
                        {
                            PartyPayableProperty.remarks = Val.ToString(drw["remarks"]);
                            PartyPayableProperty.special_remarks = Val.ToString(drw["special_remarks"]);
                            PartyPayableProperty.client_remarks = Val.ToString(drw["client_remarks"]);
                            PartyPayableProperty.payment_remarks = Val.ToString(drw["payment_remarks"]);
                            if (Val.ToDecimal(drw["Cr"]) > 0)
                            {
                                PartyPayableProperty.income_id = Val.ToInt(drw["expense_id"]);
                                PartyPayableProperty.expense_id = Val.ToInt(0);
                            }
                            if (Val.ToDecimal(drw["Dr"]) > 0)
                            {
                                PartyPayableProperty.expense_id = Val.ToInt(drw["expense_id"]);
                                PartyPayableProperty.income_id = Val.ToInt(0);
                            }
                            IntRes = objPartyPay.SaveRemark(PartyPayableProperty);
                        }
                    }

                    //IntRes = objBrokeragePay.Save(BrokeragePayableProperty);

                    if (IntRes == -1)
                    {
                        Global.Confirm("Error In Save Payment Remark");
                    }
                    else
                    {
                        Global.Confirm("Payment Remark Save Succesfully");
                        grdPartyPurchasePayable.DataSource = null;
                        GetData();
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

        private void btnListExport_Click(object sender, EventArgs e)
        {
            Global.Export("xlsx", dgvPaymentList);
        }
        private void CalculatePayAmount(int rowindex)
        {
            try
            {
                CurrencyMaster objCurrency = new CurrencyMaster();
                DataTable DTab_Rate = objCurrency.GetCurrencyRate(Val.ToInt(GlobalDec.gEmployeeProperty.currency_id), Val.ToInt(dgvPartyPurchasePayable.GetRowCellValue(rowindex, "currency_id")));

                if (DTab_Rate.Rows.Count > 0)
                {
                    Exchange_Rate = Val.ToDecimal(DTab_Rate.Rows[0]["rate"].ToString());
                    dgvPartyPurchasePayable.SetRowCellValue(rowindex, "exchange_rate", Exchange_Rate);
                }
                else
                {
                    dgvPartyPurchasePayable.SetRowCellValue(rowindex, "exchange_rate", 0);
                }
            }
            catch (Exception)
            {
            }
        }

        private void dgvPartyPayable_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            if (e.PrevFocusedColumn != null && e.PrevFocusedColumn.FieldName == "currency_id")
                CalculatePayAmount(dgvPartyPurchasePayable.FocusedRowHandle);
        }

        private void backgroundWorker_PartyPayable_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.Default;
                Conn = new BeginTranConnection(true, false);
                Party_PayableProperty PartyPayableProperty = new Party_PayableProperty();
                IntRes = 0;
                try
                {
                    Int64 Against_Ledger_Id_Cash = objIncomeEntry.ISLadgerName_GetData("CASH BALANCE");
                    Int64 Against_Ledger_Id_Bank = objIncomeEntry.ISLadgerName_GetData("BANK BALANCE");

                    if (Against_Ledger_Id_Cash == 0 || Against_Ledger_Id_Bank == 0)
                    {
                        Global.Message("Cash Balance Or Bank Balance Leger Not Set ");
                        return;
                    }

                    foreach (DataRow drw in m_DTab.Rows)
                    {
                        if (Val.ToDecimal(drw["payable"]) > 0)
                        {
                            PartyPayableProperty.ledger_id = Val.ToInt(drw["ledger_id"]);
                            PartyPayableProperty.purchase_id = Val.ToInt(drw["purchase_id"]);
                            PartyPayableProperty.purchase_detail_id = Val.ToInt(drw["purchase_detail_id"]);
                            PartyPayableProperty.payable = Val.ToDecimal(drw["payable"]);
                            PartyPayableProperty.payment_type = Val.ToString(drw["payment_type"]);
                            PartyPayableProperty.currency_id = Val.ToInt(drw["currency_id"]);
                            PartyPayableProperty.remarks = Val.ToString(drw["remarks"]);
                            PartyPayableProperty.special_remarks = Val.ToString(drw["special_remarks"]);
                            PartyPayableProperty.client_remarks = Val.ToString(drw["client_remarks"]);
                            PartyPayableProperty.payment_remarks = Val.ToString(drw["payment_remarks"]);
                            PartyPayableProperty.receive_date = Val.ToString(drw["Receive_date"]);
                            PartyPayableProperty.exchange_rate = Val.ToDecimal(drw["exchange_rate"]);
                            PartyPayableProperty.form_id = m_numForm_id;

                            if (Val.ToString(drw["payment_type"]) == "Cash")
                            {
                                PartyPayableProperty.against_ledger_id = Val.ToInt64(Against_Ledger_Id_Cash);
                            }
                            else if (Val.ToString(drw["payment_type"]) == "Bank")
                            {
                                PartyPayableProperty.against_ledger_id = Val.ToInt64(Against_Ledger_Id_Bank);
                            }

                            IntRes = objPartyPay.PurchaseParty_Save(PartyPayableProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                        }

                        if (Val.ToDecimal(drw["return_amount"]) > 0)
                        {
                            PartyPayableProperty.ledger_id = Val.ToInt(drw["ledger_id"]);
                            PartyPayableProperty.purchase_id = Val.ToInt(drw["purchase_id"]);
                            PartyPayableProperty.purchase_detail_id = Val.ToInt(drw["purchase_detail_id"]);
                            PartyPayableProperty.payable = Val.ToDecimal(drw["return_amount"]);
                            PartyPayableProperty.payment_type = Val.ToString(drw["payment_type"]);
                            PartyPayableProperty.currency_id = Val.ToInt(drw["currency_id"]);
                            PartyPayableProperty.remarks = Val.ToString(drw["remarks"]) + "Purchase Return";
                            PartyPayableProperty.special_remarks = Val.ToString(drw["special_remarks"]);
                            PartyPayableProperty.client_remarks = Val.ToString(drw["client_remarks"]);
                            PartyPayableProperty.payment_remarks = Val.ToString(drw["payment_remarks"]);
                            PartyPayableProperty.receive_date = Val.ToString(drw["Receive_date"]);
                            PartyPayableProperty.exchange_rate = Val.ToDecimal(drw["exchange_rate"]);
                            PartyPayableProperty.form_id = m_numForm_id;
                            IntRes = objPartyPay.Save(PartyPayableProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                        }
                    }
                    Conn.Inter1.Commit();
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
                    PartyPayableProperty = null;
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
        }

        private void backgroundWorker_PartyPayable_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (IntRes > 0)
                {
                    Global.Confirm("Party Purchase Payment Save Succesfully");
                    grdPartyPurchasePayable.DataSource = null;
                    GetData();
                }
                else
                {
                    Global.Confirm("Error In Save Party Purchase Payment");
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

                PrinterSettingsUsing pst = new PrinterSettingsUsing();

                PrintSystem.PageSettings.AssignDefaultPrinterSettings(pst);


                PrintableComponentLink link = new PrintableComponentLink(PrintSystem);

                link.Component = grdPartyPurchasePayable;

                foreach (System.Drawing.Printing.PaperKind foo in Enum.GetValues(typeof(System.Drawing.Printing.PaperKind)))
                {
                    if (Val.ToString("Custom") == foo.ToString())
                    {
                        link.PaperKind = foo;
                        link.PaperName = foo.ToString();

                    }
                }

                if (Val.ToString("Portrait") == "Landscape")
                {
                    link.Landscape = true;
                }
                dgvPartyPurchasePayable.OptionsPrint.ExpandAllGroups = false;
                dgvPartyPurchasePayable.OptionsPrint.AutoWidth = true;

                link.Margins.Left = 40;
                link.Margins.Right = 40;
                link.Margins.Bottom = 40;
                link.Margins.Top = 130;
                link.CreateMarginalHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);
                link.CreateMarginalFooterArea += new CreateAreaEventHandler(Link_CreateMarginalFooterArea);
                link.CreateDocument();
                link.ShowPreview();
                link.PrintDlg();
            }
            catch (Exception EX)
            {
                Global.Message(EX.Message);
            }
        }

        public void Link_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {
            // ' For Report Title
            TextBrick BrickTitle = e.Graph.DrawString("Party Payment", System.Drawing.Color.Navy, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 20), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitle.Font = new Font("Tahoma", 12, FontStyle.Bold);
            BrickTitle.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitle.VertAlignment = DevExpress.Utils.VertAlignment.Center;

            int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 400, 0));
            TextBrick BrickTitledate = e.Graph.DrawString("Print Date :- " + DateTime.Now.ToShortDateString(), System.Drawing.Color.Navy, new RectangleF(IntX, 25, 400, 18), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitledate.Font = new Font("Tahoma", 8, FontStyle.Bold);
            BrickTitledate.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            BrickTitledate.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitledate.ForeColor = Color.Black;
        }
        public void Link_CreateMarginalFooterArea(object sender, CreateAreaEventArgs e)
        {
            int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 100, 0));

            PageInfoBrick BrickPageNo = e.Graph.DrawPageInfo(PageInfo.NumberOfTotal, "Page {0} of {1}", System.Drawing.Color.Navy, new RectangleF(IntX, 0, 100, 15), DevExpress.XtraPrinting.BorderSide.None);
            BrickPageNo.LineAlignment = BrickAlignment.Far;
            BrickPageNo.Alignment = BrickAlignment.Far;
            BrickPageNo.Font = new Font("Tahoma", 8, FontStyle.Bold);
            BrickPageNo.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            BrickPageNo.VertAlignment = DevExpress.Utils.VertAlignment.Center;
        }
    }
}
