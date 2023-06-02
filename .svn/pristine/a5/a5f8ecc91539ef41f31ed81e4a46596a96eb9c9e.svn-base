using BLL;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Utility;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using DERP.Master;
using DERP.Report;
using DevExpress.XtraEditors;
using DREP.Master;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using static DERP.Class.Global;


namespace DREP.Transaction
{
    public partial class FrmSaleInvoice : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member
        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        Control _NextEnteredControl;
        private List<Control> _tabControls;
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        SaleInvoice objSaleInvoice = new SaleInvoice();
        UserAuthentication objUserAuthentication = new UserAuthentication();
        AssortMaster objAssort = new AssortMaster();
        SieveMaster objSieve = new SieveMaster();
        RateMaster objRate = new RateMaster();
        OpeningStock opstk = new OpeningStock();

        DataTable DtControlSettings = new DataTable();
        DataTable m_dtbSievecheck = new DataTable();
        DataTable m_dtbSubSievecheck = new DataTable();
        DataTable m_dtbAssortscheck = new DataTable();
        DataTable m_dtbSievedtl = new DataTable();
        DataTable m_dtbAssortsdtl = new DataTable();
        DataTable m_dtbSubSievedtl = new DataTable();
        DataTable m_dtbMemoData = new DataTable();
        DataTable m_opDate = new DataTable();
        DataTable m_dtbMemoNo = new DataTable();
        DataTable m_dtbDemandNo = new DataTable();
        DataTable m_dtbStockCarat = new DataTable();
        DataTable m_dtbAssorts = new DataTable();
        DataTable m_dtbSieve = new DataTable();
        DataTable m_dtbSaleDetails = new DataTable();
        DataTable m_dtbCurrencyType = new DataTable();
        DataTable m_dtbDetails = new DataTable();
        DataTable m_dtbSeller = new DataTable();


        int m_invoice_detail_id;
        int m_srno;
        int m_update_srno;
        int m_numCurrency_id;
        int m_numForm_id;
        int IntRes;

        decimal m_numcarat;
        decimal m_current_rate;
        decimal m_current_amount;
        decimal m_numSummRate;
        decimal m_numSummDetRate;

        bool m_blnadd;
        bool m_blnsave;
        bool m_blncheckevents;

        bool m_IsValid;
        bool m_IsUpdate;

        #endregion

        #region Constructor
        public FrmSaleInvoice()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            _NextEnteredControl = new Control();
            _tabControls = new List<Control>();

            objSaleInvoice = new SaleInvoice();
            objUserAuthentication = new UserAuthentication();
            objAssort = new AssortMaster();
            objSieve = new SieveMaster();
            objRate = new RateMaster();
            opstk = new OpeningStock();

            DtControlSettings = new DataTable();
            m_dtbSievecheck = new DataTable();
            m_dtbSubSievecheck = new DataTable();
            m_dtbAssortscheck = new DataTable();
            m_dtbSievedtl = new DataTable();
            m_dtbAssortsdtl = new DataTable();
            m_dtbSubSievedtl = new DataTable();
            m_dtbMemoData = new DataTable();
            m_opDate = new DataTable();
            m_dtbMemoNo = new DataTable();
            m_dtbDemandNo = new DataTable();
            m_dtbStockCarat = new DataTable();
            m_dtbAssorts = new DataTable();
            m_dtbSieve = new DataTable();
            m_dtbSaleDetails = new DataTable();
            m_dtbCurrencyType = new DataTable();
            m_dtbDetails = new DataTable();

            m_invoice_detail_id = 0;
            m_srno = 0;
            m_update_srno = 0;
            m_numCurrency_id = 0;
            m_numForm_id = 0;
            IntRes = 0;

            m_numcarat = 0;
            m_current_rate = 0;
            m_current_amount = 0;

            m_blnadd = new bool();
            m_blnsave = new bool();
            m_blncheckevents = new bool();
            m_IsValid = new bool();
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
            if (Global.CheckDefault() == 0)
            {
                Global.Message("Please Check User Default Setting");
                this.Close();
                return;
            }
            Val.frmGenSet(this);
            AttachFormEvents();

            // for Dynamic Setting By Praful On 01022020

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

            // End for Dynamic Setting By Praful On 01022020

            this.Show();
        }
        public void ShowForm_New(DataTable MemoDt)
        {
            ObjPer.FormName = this.Name.ToUpper();
            m_numForm_id = ObjPer.form_id;
            if (ObjPer.CheckPermission() == false)
            {
                Global.Message(BLL.GlobalDec.gStrPermissionViwMsg);
                return;
            }
            if (Global.CheckDefault() == 0)
            {
                Global.Message("Please Check User Default Setting");
                this.Close();
                return;
            }
            Val.frmGenSet(this);
            AttachFormEvents();


            //Global.HideFormControls(Val.ToInt(ObjPer.form_id), this);

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

            // End for Dynamic Setting By Praful On 01022020

            m_dtbMemoData = MemoDt;

            this.Show();
            FillMemoToSale();


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
        private void AttachFormEvents()
        {
            objBOFormEvents.CurForm = this;
            objBOFormEvents.FormKeyPress = true;
            objBOFormEvents.FormKeyDown = true;
            objBOFormEvents.FormResize = true;
            objBOFormEvents.FormClosing = true;
            objBOFormEvents.ObjToDispose.Add(objSaleInvoice);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);

        }

        #endregion

        #region Events
        private void FrmSaleInvoice_Load(object sender, EventArgs e)
        {
            try
            {
                if (!LoadDefaults())
                {
                    btnAdd.Enabled = false;
                    btnClear.Enabled = false;
                    btnSave.Enabled = false;
                }
                else
                {
                    ClearDetails();
                    ttlbSaleInvoice.SelectedTabPage = tblSaledetail;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (AddInGrid())
            {
                lueAssortName.Focus();
                lueAssortName.ShowPopup();
                lueAssortName.EditValue = DBNull.Value;
                lueSieveName.EditValue = DBNull.Value;
                lueSubSieveName.EditValue = DBNull.Value;
                txtPcs.Text = string.Empty;
                txtCarat.Text = string.Empty;
                txtRejCarat.Text = string.Empty;
                txtRejPer.Text = string.Empty;
                txtRate.Text = string.Empty;
                txtAmount.Text = string.Empty;
                txtPurchaseRate.Text = string.Empty;
                txtPurchaseAmount.Text = string.Empty;

                txtLossCarat.Text = string.Empty;
                m_IsValid = true;
                m_blncheckevents = false;
                txtDiscountPer_EditValueChanged(null, null);
                txtBrokeragePer_EditValueChanged(null, null);
                lblOldCarat.Text = string.Empty;
                m_blncheckevents = true;
                //lueAssortName_KeyUp(null, null);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            ObjPer.SetFormPer();

            if (lblMode.Text == "Add Mode")
            {
                if (ObjPer.AllowInsert == false)
                {
                    Global.Message(BLL.GlobalDec.gStrPermissionInsUpdMsg);
                    return;
                }
            }
            else if (lblMode.Text == "Edit Mode")
            {
                if (ObjPer.AllowUpdate == false)
                {
                    Global.Message(BLL.GlobalDec.gStrPermissionInsUpdMsg);
                    return;
                }
            }

            //DataTable DTab_Party = objSaleInvoice.GetDataPartyOutStandingLimit(Val.ToInt64(lueBilledToParty.EditValue));
            //DataTable DTab_PartyPayment_OS = objSaleInvoice.Get_PartyPayment_OS(Val.ToInt64(DTab_Party.Rows[0]["ledger_id"]));

            //if (Val.ToInt64(DTab_Party.Rows[0]["outstanding_limit"]) < Val.ToInt64(DTab_PartyPayment_OS.Rows[0]["cl_amount"]))
            //{
            //    Global.Message("Party OutStanding Limit Over....Contact to Administrator");
            //    return;
            //}

            List<ListError> lstError = new List<ListError>();
            Dictionary<Control, string> rtnCtrls = new Dictionary<Control, string>();
            rtnCtrls = Global.CheckCompulsoryControls(Val.ToInt(ObjPer.form_id), this);
            if (rtnCtrls.Count > 0)
            {
                foreach (KeyValuePair<Control, string> entry in rtnCtrls)
                {
                    if (entry.Key is DevExpress.XtraEditors.LookUpEdit || entry.Key is DevExpress.XtraEditors.DateEdit)
                    {
                        lstError.Add(new ListError(13, entry.Value));
                    }
                    else if (entry.Key is DevExpress.XtraEditors.TextEdit)
                    {
                        lstError.Add(new ListError(12, entry.Value));
                    }
                }
                rtnCtrls.First().Key.Focus();
                BLL.General.ShowErrors(lstError);
                Cursor.Current = Cursors.Arrow;
                return;
            }

            btnSave.Enabled = false;

            m_blnsave = true;
            m_blnadd = false;
            if (!ValidateDetails())
            {
                m_blnsave = false;
                btnSave.Enabled = true;
                return;
            }

            while (objSaleInvoice.ISExistsInvoiceNo(Val.ToString(txtInvoiceNo.Text)) == true && lblMode.Text == "Add Mode" && Val.ToInt(lblMode.Tag) == 0)
            {
                Global.Message("this Invoice No already Created please check invoice No");
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
            panelProgress.Visible = true;
            backgroundWorker_SaleInvoice.RunWorkerAsync();

            btnSave.Enabled = true;
        }
        private void btnClear_Click_1(object sender, EventArgs e)
        {
            ClearDetails();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!PopulateDetails())
                return;
        }
        private void txtCarat_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtAmount.Text = string.Format("{0:0.00}", Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text));
                m_current_amount = Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(m_current_rate);
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void txtRate_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtAmount.Text = string.Format("{0:0.00}", Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text));
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            ObjPer.SetFormPer();
            if (ObjPer.AllowDelete == false)
            {
                Global.Message(BLL.GlobalDec.gStrPermissionDelMsg);
                return;
            }
            btnDelete.Enabled = false;


            DialogResult result = MessageBox.Show("Do you want to Delete data?", "Confirmation", MessageBoxButtons.YesNoCancel);
            if (result != DialogResult.Yes)
            {
                btnDelete.Enabled = true;
                return;
            }

            DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
            panelProgress.Visible = true;
            backgroundWorker_SaleDelete.RunWorkerAsync();

            btnDelete.Enabled = true;
        }
        private void lueAssortName_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (lueAssortName.ItemIndex != -1 && lueSieveName.ItemIndex != -1)
                {
                    GetCarat();
                    BranchTransfer objBranch = new BranchTransfer();
                    string p_numStockRate = string.Empty;
                    p_numStockRate = objBranch.GetLetestPrice(Val.ToInt(lueAssortName.EditValue), Val.ToInt(lueSieveName.EditValue));

                    txtRate.Text = Val.ToString(p_numStockRate);
                    m_current_rate = Val.ToDecimal(p_numStockRate);
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void txtTermDays_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtpInvoiceDate.Text.Length <= 0 || dtpInvoiceDate.Text == "")
                {
                    txtTermDays.Text = "";
                    dtpDueDate.EditValue = null;
                }
                else
                {
                    Double Days = Val.ToDouble(txtTermDays.Text) + Val.ToDouble(txtAddOnDays.Text);
                    DateTime Date = Convert.ToDateTime(dtpInvoiceDate.EditValue).AddDays(Val.ToDouble(Days));
                    dtpDueDate.EditValue = Val.DBDate(Date.ToShortDateString());
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void txtAddOnDays_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtpInvoiceDate.Text.Length <= 0 || dtpInvoiceDate.Text == "")
                {
                    txtAddOnDays.Text = "";
                    dtpDueDate.EditValue = null;
                }
                else
                {
                    Double Days = Val.ToDouble(txtTermDays.Text) + Val.ToDouble(txtAddOnDays.Text);
                    DateTime Date = Convert.ToDateTime(dtpInvoiceDate.EditValue).AddDays(Val.ToDouble(Days));
                    dtpDueDate.EditValue = Val.DBDate(Date.ToShortDateString());
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void lueSieveName_EditValueChanged(object sender, EventArgs e)
        {
            objSaleInvoice = new SaleInvoice();
            GetCarat();
            Global.LOOKUPSubSieve(lueSubSieveName, Val.ToInt(lueSieveName.EditValue));
            lueAssortName_EditValueChanged(null, null);
        }
        private void txtDiscountPer_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!m_blncheckevents)
                {
                    decimal Dis_amt = Math.Round(Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) * Val.ToDecimal(txtDiscountPer.Text) / 100, 0);
                    txtDiscountAmt.Text = Dis_amt.ToString();
                    decimal Net_Amount = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) + Val.ToDecimal(txtCGSTAmt.Text) + Val.ToDecimal(txtSGSTAmt.Text) + Val.ToDecimal(txtIGSTAmt.Text) - Val.ToDecimal(txtDiscountAmt.Text)) + Val.ToDecimal(txtInterestAmt.Text) + Val.ToDecimal(txtShippingCharge.Text), 0);
                    txtNetAmount.Text = Net_Amount.ToString();

                    txtBrokeragePer_EditValueChanged(null, null);
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void txtDiscountAmt_EditValueChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (!m_blncheckevents)
            //    {
            //        if (Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) != 0)
            //        {
            //            decimal Dis_Per = Math.Round(Val.ToDecimal(txtDiscountAmt.Text) / Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) * 100, 3);
            //            txtDiscountPer.Text = Dis_Per.ToString();
            //            decimal Net_Amount = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) + Val.ToDecimal(txtCGSTAmt.Text) + Val.ToDecimal(txtSGSTAmt.Text) + Val.ToDecimal(txtIGSTAmt.Text) - Val.ToDecimal(txtDiscountAmt.Text)) + Val.ToDecimal(txtInterestAmt.Text) + Val.ToDecimal(txtShippingCharge.Text), 0);
            //            txtNetAmount.Text = Net_Amount.ToString();
            //        }
            //    }

            //}
            //catch (Exception ex)
            //{
            //    General.ShowErrors(ex.ToString());
            //    return;
            //}
        }
        private void txtBrokeragePer_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!m_blncheckevents)
                {
                    decimal Brokerage_amt = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) - Val.ToDecimal(txtDiscountAmt.Text)) * Val.ToDecimal(txtBrokeragePer.Text) / 100, 0);
                    txtBrokerageAmt.Text = Brokerage_amt.ToString();
                    //decimal Net_Amount = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) - Val.ToDecimal(txtDiscountAmt.Text)) + Val.ToDecimal(txtInterestAmt.Text) - Val.ToDecimal(txtBrokerageAmt.Text) + Val.ToDecimal(txtShippingCharge.Text), 0);
                    //txtNetAmount.Text = Net_Amount.ToString();
                }

            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void txtBrokerageAmt_EditValueChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) != 0)
            //    {
            //        if (!m_blncheckevents)
            //        {
            //            decimal Brokarage_Per = Math.Round(Val.ToDecimal(txtBrokerageAmt.Text) / (Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) - Val.ToDecimal(txtDiscountAmt.Text)) * 100, 3);
            //            txtBrokeragePer.Text = Brokarage_Per.ToString();
            //            //decimal Net_Amount = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) - Val.ToDecimal(txtDiscountAmt.Text)) + Val.ToDecimal(txtInterestAmt.Text) - Val.ToDecimal(txtBrokerageAmt.Text) + Val.ToDecimal(txtShippingCharge.Text), 0);
            //            //txtNetAmount.Text = Net_Amount.ToString();
            //        }
            //    }

            //}
            //catch (Exception ex)
            //{
            //    General.ShowErrors(ex.ToString());
            //    return;
            //}
        }
        private void txtInterestPer_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!m_blncheckevents)
                {
                    decimal Interest_amt = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) - Val.ToDecimal(txtDiscountAmt.Text)) * Val.ToDecimal(txtInterestPer.Text) / 100, 0);
                    txtInterestAmt.Text = Interest_amt.ToString();
                    decimal Net_Amount = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) + Val.ToDecimal(txtCGSTAmt.Text) + Val.ToDecimal(txtSGSTAmt.Text) + Val.ToDecimal(txtIGSTAmt.Text) - Val.ToDecimal(txtDiscountAmt.Text)) + Val.ToDecimal(txtInterestAmt.Text) + Val.ToDecimal(txtShippingCharge.Text), 0);
                    txtNetAmount.Text = Net_Amount.ToString();
                }

            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void txtInterestAmt_EditValueChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) != 0)
            //    {
            //        if (!m_blncheckevents)
            //        {
            //            decimal interest_Per = Math.Round(Val.ToDecimal(txtInterestAmt.Text) / (Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) - Val.ToDecimal(txtDiscountAmt.Text)) * 100, 2);
            //            txtInterestPer.Text = interest_Per.ToString();
            //            decimal Net_Amount = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) + Val.ToDecimal(txtCGSTAmt.Text) + Val.ToDecimal(txtSGSTAmt.Text) + Val.ToDecimal(txtIGSTAmt.Text) - Val.ToDecimal(txtDiscountAmt.Text)) + Val.ToDecimal(txtInterestAmt.Text) + Val.ToDecimal(txtShippingCharge.Text), 0);
            //            txtNetAmount.Text = Net_Amount.ToString();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    General.ShowErrors(ex.ToString());
            //    return;
            //}
        }
        private void txtShippingCharge_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) != 0)
                {
                    decimal Shipping_Charge = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) + Val.ToDecimal(txtInterestAmt.Text) + Val.ToDecimal(txtCGSTAmt.Text) + Val.ToDecimal(txtSGSTAmt.Text) + Val.ToDecimal(txtIGSTAmt.Text)) - (Val.ToDecimal(txtDiscountAmt.Text)) + Val.ToDecimal(txtShippingCharge.Text), 0);
                    txtNetAmount.Text = Shipping_Charge.ToString();
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void txtCGSTPer_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                decimal GrossAmt = Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue);
                decimal DiscountAmt = Val.ToDecimal(txtDiscountAmt.Text);

                decimal CGST_amt = Math.Round(Val.ToDecimal(GrossAmt - DiscountAmt) * Val.ToDecimal(txtCGSTPer.Text) / 100, 0);
                txtCGSTAmt.Text = CGST_amt.ToString();

                decimal Net_Amount = Math.Round((Val.ToDecimal(GrossAmt) + Val.ToDecimal(txtCGSTAmt.Text) + Val.ToDecimal(txtSGSTAmt.Text) + Val.ToDecimal(txtIGSTAmt.Text) - Val.ToDecimal(txtDiscountAmt.Text)) + Val.ToDecimal(txtInterestAmt.Text) + Val.ToDecimal(txtShippingCharge.Text), 0);
                txtNetAmount.Text = Net_Amount.ToString();
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void txtSGSTPer_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                decimal GrossAmt = Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue);
                decimal DiscountAmt = Val.ToDecimal(txtDiscountAmt.Text);

                decimal SGST_amt = Math.Round(Val.ToDecimal(GrossAmt - DiscountAmt) * Val.ToDecimal(txtSGSTPer.Text) / 100, 0);
                txtSGSTAmt.Text = SGST_amt.ToString();
                decimal Net_Amount = Math.Round((Val.ToDecimal(GrossAmt) + Val.ToDecimal(txtCGSTAmt.Text) + Val.ToDecimal(txtSGSTAmt.Text) + Val.ToDecimal(txtIGSTAmt.Text) - Val.ToDecimal(txtDiscountAmt.Text)) + Val.ToDecimal(txtInterestAmt.Text) + Val.ToDecimal(txtShippingCharge.Text), 0);
                txtNetAmount.Text = Net_Amount.ToString();
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void txtIGSTPer_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                decimal GrossAmt = Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue);
                decimal DiscountAmt = Val.ToDecimal(txtDiscountAmt.Text);

                decimal IGST_amt = Math.Round(Val.ToDecimal(GrossAmt - DiscountAmt) * Val.ToDecimal(txtIGSTPer.Text) / 100, 0);
                txtIGSTAmt.Text = IGST_amt.ToString();
                decimal Net_Amount = Math.Round((Val.ToDecimal(GrossAmt) + Val.ToDecimal(txtCGSTAmt.Text) + Val.ToDecimal(txtSGSTAmt.Text) + Val.ToDecimal(txtIGSTAmt.Text) - Val.ToDecimal(txtDiscountAmt.Text)) + Val.ToDecimal(txtInterestAmt.Text) + Val.ToDecimal(txtShippingCharge.Text), 0);
                txtNetAmount.Text = Net_Amount.ToString();
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            ObjPer.SetFormPer();
            if (ObjPer.AllowPrint == false)
            {
                Global.Message(BLL.GlobalDec.gStrPermissionPrintMsg);
                return;
            }
            Sales_InvoiceProperty Sales_InvoiceProperty = new Sales_InvoiceProperty();
            Sales_InvoiceProperty.invoice_date = Val.DBDate(dtpInvoiceDate.Text);
            Sales_InvoiceProperty.invoice_No = Val.ToString(txtInvoiceNo.Text);
            Sales_InvoiceProperty.invoice_id = Val.ToInt32(lblMode.Tag);

            DataTable dtpur = new DataTable();
            dtpur = objSaleInvoice.GetPrintData(Sales_InvoiceProperty);

            FrmReportViewer FrmReportViewer = new FrmReportViewer();
            FrmReportViewer.DS.Tables.Add(dtpur);
            FrmReportViewer.GroupBy = "";
            FrmReportViewer.RepName = "";
            FrmReportViewer.RepPara = "";
            this.Cursor = Cursors.Default;
            FrmReportViewer.AllowSetFormula = true;

            FrmReportViewer.ShowForm("Sale_Invoice_Sum", 120, FrmReportViewer.ReportFolder.ACCOUNT);
            Sales_InvoiceProperty = null;
            dtpur = null;
            FrmReportViewer.DS.Tables.Clear();
            FrmReportViewer.DS.Clear();
            FrmReportViewer = null;
        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog OpenDialog = new OpenFileDialog();
                if (OpenDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }
                txtFileName.Text = OpenDialog.FileName;
                OpenDialog.Dispose();
                OpenDialog = null;

                if (File.Exists(txtFileName.Text) == false)
                {
                    Global.Message("File Is Not Exists To The Path");
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                grdSalesDetails.DataSource = null;


                if (txtFileName.Text.Length != 0)
                {
                    using (var pck = new ExcelPackage(new FileInfo(txtFileName.Text)))
                    {
                        ExcelWorksheet ws = pck.Workbook.Worksheets[1];
                        m_dtbSaleDetails = WorksheetToDataTable(ws, true);
                    }
                }

                m_dtbSievecheck = new SieveMaster().GetData();
                m_dtbAssortscheck = new AssortMaster().GetData();
                m_dtbSubSievecheck = new SubSieveMaster().GetData();

                m_dtbSaleDetails.Columns.Add("invoice_detail_id", typeof(int));
                m_dtbSaleDetails.Columns.Add("invoice_id", typeof(int));
                m_dtbSaleDetails.Columns.Add("assort_id", typeof(int));
                m_dtbSaleDetails.Columns.Add("sieve_id", typeof(int));
                m_dtbSaleDetails.Columns.Add("discount", typeof(decimal)).DefaultValue = 0;
                m_dtbSaleDetails.Columns.Add("sub_sieve_id", typeof(int));
                m_dtbSaleDetails.Columns.Add("current_rate", typeof(decimal)).DefaultValue = 0;
                m_dtbSaleDetails.Columns.Add("current_amount", typeof(decimal)).DefaultValue = 0;
                m_dtbSaleDetails.Columns.Add("old_carat", typeof(decimal)).DefaultValue = 0;
                m_dtbSaleDetails.Columns.Add("old_pcs", typeof(int));
                m_dtbSaleDetails.Columns.Add("old_rej_carat", typeof(decimal)).DefaultValue = 0;
                m_dtbSaleDetails.Columns.Add("old_rej_percentage", typeof(decimal)).DefaultValue = 0;
                m_dtbSaleDetails.Columns.Add("flag", typeof(int));

                m_dtbSaleDetails.Columns.Add("old_assort_id", typeof(int));
                m_dtbSaleDetails.Columns.Add("old_sieve_id", typeof(int));
                m_dtbSaleDetails.Columns.Add("old_sub_sieve_id", typeof(int));
                m_dtbSaleDetails.Columns.Add("sr_no", typeof(int));
                m_dtbSaleDetails.Columns.Add("loss_carat", typeof(decimal)).DefaultValue = 0;
                m_dtbSaleDetails.Columns.Add("old_loss_carat", typeof(decimal)).DefaultValue = 0;
                m_srno = 0;

                foreach (DataRow DRow in m_dtbSaleDetails.Rows)
                {
                    BranchTransfer objBranch = new BranchTransfer();

                    if (m_dtbSaleDetails.Select("sieve_name ='" + Val.ToString(DRow["sieve_name"]) + "' And assort_name = '" + Val.ToString(DRow["assort_name"]) + "'").Length > 1)
                    {
                        Global.Message("Duplicate Value found in Sieve : " + Val.ToString(DRow["sieve_name"]) + " AND Assort: " + Val.ToString(DRow["assort_name"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (DRow["assort_name"] != null)
                    {
                        if (Val.ToString(DRow["assort_name"]) != "")
                        {
                            if (m_dtbAssortscheck.Select("assort_name ='" + Val.ToString(DRow["assort_name"]) + "'").Length > 0)
                            {
                                m_dtbAssortsdtl = m_dtbAssortscheck.Select("assort_name ='" + Val.ToString(DRow["assort_name"]) + "'").CopyToDataTable();
                            }
                            else
                            {
                                Global.Message("Assort Not found in Master : " + Val.ToString(DRow["assort_name"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                    }
                    else
                    {
                        Global.Message("Assort Name are not found :" + Val.ToString(DRow["assort_name"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Cursor = Cursors.Default;
                        return;
                    }


                    if (DRow["sieve_name"] != null)
                    {
                        if (Val.ToString(DRow["sieve_name"]) != "")
                        {
                            if (m_dtbSievecheck.Select("sieve_name ='" + Val.ToString(DRow["sieve_name"]) + "'").Length > 0)
                            {
                                m_dtbSievedtl = m_dtbSievecheck.Select("sieve_name ='" + Val.ToString(DRow["sieve_name"]) + "'").CopyToDataTable();
                            }
                            else
                            {
                                Global.Message("Sieve Not found in Master : " + Val.ToString(DRow["sieve_name"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Cursor = Cursors.Default;
                                return;
                            }
                        }
                    }
                    else
                    {
                        Global.Message("Sieve Name are not found : " + Val.ToString(DRow["sieve_name"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Cursor = Cursors.Default;
                        return;
                    }


                    if (DRow["sub_sieve_name"] != null)
                    {
                        if (Val.ToString(DRow["sub_sieve_name"]) != "")
                        {
                            if (m_dtbSubSievecheck.Select("sub_sieve_name ='" + Val.ToString(DRow["sub_sieve_name"]) + "'").Length > 0)
                            {
                                m_dtbSubSievedtl = m_dtbSubSievecheck.Select("sub_sieve_name ='" + Val.ToString(DRow["sub_sieve_name"]) + "'").CopyToDataTable();
                            }
                            else
                            {
                                Global.Message("Sub Sieve Not found in Master : " + Val.ToString(DRow["sub_sieve_name"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Cursor = Cursors.Default;
                                return;
                            }
                        }
                    }
                    else
                    {
                        Global.Message("Sub Sieve Name are not found : " + Val.ToString(DRow["sub_sieve_name"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Cursor = Cursors.Default;
                        return;
                    }


                    decimal numStockCarat = 0;

                    DataTable m_dtbStockCarat = new DataTable();
                    m_dtbStockCarat = objSaleInvoice.GetStockCarat(GlobalDec.gEmployeeProperty.company_id, GlobalDec.gEmployeeProperty.branch_id, GlobalDec.gEmployeeProperty.location_id, GlobalDec.gEmployeeProperty.department_id, Val.ToInt(lueAssortName.EditValue), Val.ToInt(lueSieveName.EditValue));

                    if (m_dtbStockCarat.Rows.Count > 0)
                    {
                        numStockCarat = Val.ToDecimal(m_dtbStockCarat.Rows[0]["stock_carat"]);
                    }

                    if (numStockCarat < Val.ToDecimal(DRow["carat"]))
                    {
                        Global.Message("Please check enter carat more then stock carat  (Assorts : " + Val.ToString(DRow["assort_name"]) + ") (Sieve : " + Val.ToString(DRow["sieve_name"]) + " ) ", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCarat.Focus();
                        this.Cursor = Cursors.Default;
                        return;
                    }


                    DRow["assort_id"] = Val.ToInt(m_dtbAssortsdtl.Rows[0]["assort_id"]);
                    DRow["sieve_id"] = Val.ToInt(m_dtbSievedtl.Rows[0]["sieve_id"]);
                    DRow["sub_sieve_id"] = Val.ToInt(m_dtbSubSievedtl.Rows[0]["sub_sieve_id"]);

                    string p_numStockRate = string.Empty;
                    p_numStockRate = objBranch.GetLetestPrice(Val.ToInt(m_dtbAssortsdtl.Rows[0]["assort_id"]), Val.ToInt(m_dtbSievedtl.Rows[0]["sieve_id"]));
                    m_current_rate = Val.ToDecimal(p_numStockRate);
                    DRow["current_rate"] = m_current_rate;
                    DRow["current_amount"] = Val.ToDecimal(m_current_rate) * Val.ToDecimal(DRow["carat"]);
                    m_srno = m_srno + 1;
                    DRow["sr_no"] = Val.ToInt(m_srno);
                }

                grdSalesDetails.DataSource = m_dtbSaleDetails;
                dgvSalesDetails.MoveLast();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                this.Cursor = Cursors.Default;
                return;
            }
        }
        private void lblFormatSample_Click(object sender, EventArgs e)
        {
            Global.CopyFormat(System.Windows.Forms.Application.StartupPath + @"\FORMAT\Sale_Invoice.xlsx", "Sale_Invoice.xlsx", "xlsx");
        }
        private void lueBilledToParty_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (e.Button.Index == 1)
                {
                    FrmPartyMaster frmParty = new FrmPartyMaster();
                    frmParty.ShowDialog();
                    Global.LOOKUPParty(lueBilledToParty);
                    Global.LOOKUPParty(lueShippedToParty);
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void lueShippedToParty_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (e.Button.Index == 1)
                {
                    FrmPartyMaster frmParty = new FrmPartyMaster();
                    frmParty.ShowDialog();
                    Global.LOOKUPParty(lueShippedToParty);
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void lueBroker_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (e.Button.Index == 1)
                {
                    FrmBrokerMaster frmBroker = new FrmBrokerMaster();
                    frmBroker.ShowDialog();
                    Global.LOOKUPBroker(lueBroker);
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void txtCGSTPer_KeyUp(object sender, KeyEventArgs e)
        {
            txtCGSTPer.Select();
        }
        private void lueBilledToParty_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToInt(lueBilledToParty.EditValue) > 0)
                {
                    lueShippedToParty.EditValue = Val.ToInt(lueBilledToParty.EditValue);
                    LookUpEdit type = sender as LookUpEdit;
                    Int32 State_id = Val.ToInt32(type.GetColumnValue("state_id"));

                    if (State_id == GlobalDec.gEmployeeProperty.state_id)
                    {
                        txtCGSTPer.Text = Val.ToDecimal(GlobalDec.gEmployeeProperty.cgst_per).ToString();
                        txtSGSTPer.Text = Val.ToDecimal(GlobalDec.gEmployeeProperty.sgst_per).ToString();
                        txtIGSTPer.Text = "";
                        txtIGSTAmt.Text = "";
                        decimal CGST_amt = Math.Round(Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) * Val.ToDecimal(txtCGSTPer.Text) / 100, 0);
                        txtCGSTAmt.Text = CGST_amt.ToString();
                        decimal SGST_amt = Math.Round(Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) * Val.ToDecimal(txtSGSTPer.Text) / 100, 0);
                        txtSGSTAmt.Text = SGST_amt.ToString();
                        decimal Shipping_Charge = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) + Val.ToDecimal(txtInterestAmt.Text) + Val.ToDecimal(txtCGSTAmt.Text) + Val.ToDecimal(txtSGSTAmt.Text) + Val.ToDecimal(txtIGSTAmt.Text)) - (Val.ToDecimal(txtDiscountAmt.Text)) + Val.ToDecimal(txtShippingCharge.Text), 0);
                        txtNetAmount.Text = Shipping_Charge.ToString();
                    }
                    else
                    {
                        txtIGSTPer.Text = Val.ToDecimal(GlobalDec.gEmployeeProperty.igst_per).ToString();
                        txtCGSTPer.Text = "";
                        txtCGSTAmt.Text = "";
                        txtSGSTPer.Text = "";
                        txtSGSTAmt.Text = "";
                        decimal IGST_amt = Math.Round(Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) * Val.ToDecimal(txtIGSTPer.Text) / 100, 0);
                        txtIGSTAmt.Text = IGST_amt.ToString();
                        decimal Shipping_Charge = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) + Val.ToDecimal(txtInterestAmt.Text) + Val.ToDecimal(txtCGSTAmt.Text) + Val.ToDecimal(txtSGSTAmt.Text) + Val.ToDecimal(txtIGSTAmt.Text)) - (Val.ToDecimal(txtDiscountAmt.Text)) + Val.ToDecimal(txtShippingCharge.Text), 0);
                        txtNetAmount.Text = Shipping_Charge.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void lueCurrency_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                CurrencyMaster objCurrency = new CurrencyMaster();

                m_dtbCurrencyType = objCurrency.GetCurrencyID(Val.ToString(lueCurrency.EditValue));

                if (m_dtbCurrencyType.Rows.Count > 0)
                {
                    m_numCurrency_id = Val.ToInt(m_dtbCurrencyType.Rows[0]["currency_id"]);
                }

                DataTable DTab_Rate = objCurrency.GetCurrencyRate(Val.ToInt(GlobalDec.gEmployeeProperty.currency_id), Val.ToInt(m_dtbCurrencyType.Rows[0]["currency_id"]));

                if (DTab_Rate.Rows.Count > 0)
                {
                    txtExchangeRate.EditValue = DTab_Rate.Rows[0]["rate"].ToString();
                }
                else
                {
                    txtExchangeRate.EditValue = 0;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }
        private void txtDiscountPer_KeyDown(object sender, KeyEventArgs e)
        {
            m_blncheckevents = false;
        }
        private void txtDiscountAmt_KeyDown(object sender, KeyEventArgs e)
        {
            m_blncheckevents = false;
        }
        private void txtBrokeragePer_KeyDown(object sender, KeyEventArgs e)
        {
            m_blncheckevents = false;
        }
        private void txtBrokerageAmt_KeyDown(object sender, KeyEventArgs e)
        {

            m_blncheckevents = false;
        }
        private void txtInterestPer_KeyDown(object sender, KeyEventArgs e)
        {
            m_blncheckevents = false;
        }
        private void txtInterestAmt_KeyDown(object sender, KeyEventArgs e)
        {
            m_blncheckevents = false;
        }
        private void panelControl4_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(255, 191, 219, 255), 2);
            e.Graphics.DrawLine(pen, 0, 85, 1500, 85);
        }
        private void btnserch_Click(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToString(lueMemoNo.Text) != "")
                {
                    objSaleInvoice = new SaleInvoice();
                    m_dtbMemoData = new DataTable();
                    m_dtbMemoData = objSaleInvoice.SearchMemoData(Val.ToString(lueMemoNo.Text));

                    while (objSaleInvoice.ISAlredySaveMemoData(Val.ToInt(lueMemoNo.EditValue)) == true)
                    {
                        Global.Message("this Memo No already Saved please check Memo No");
                        btnSave.Enabled = true;
                        return;
                    }

                    FillMemoToSale();

                }

            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void lueBroker_EditValueChanged(object sender, EventArgs e)
        {
            //ConfigPartyBrokerMaster objConfigPartyBroker = new ConfigPartyBrokerMaster();

            //if (Val.ToInt(lueBroker.EditValue) > 0)
            //{
            //    DataTable dtb = (((DataTable)lueBroker.Properties.DataSource).Copy());

            //    if (dtb.Rows.Count > 0)
            //    {
            //        dtb = dtb.Select("broker_id ='" + Val.ToString(Val.ToInt(lueBroker.EditValue)) + "'").CopyToDataTable();

            //        if (dtb.Rows.Count > 0)
            //        {
            //            txtBrokeragePer.Text = Val.ToString(dtb.Rows[0]["brokerage"]);
            //        }
            //    }
            //}
        }
        private void backgroundWorker_SaleDelete_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Conn = new BeginTranConnection(true, false);

            Sales_InvoiceProperty objSaleInvoiceProperty = new Sales_InvoiceProperty();
            SaleInvoice objSaleInvoice = new SaleInvoice();
            DataTable dtbMemoRecDelete = new DataTable();
            try
            {
                if (Val.ToInt(lblMode.Tag) != 0)
                {
                    IntRes = 0;
                    objSaleInvoiceProperty.invoice_id = Val.ToInt(lblMode.Tag);
                    dtbMemoRecDelete = objSaleInvoice.GetMemoReceiveData(Val.ToInt(lblMode.Tag));


                    int IntCounter = 0;
                    int Count = 0;
                    int FlagCount = 1;
                    int TotalCount = m_dtbSaleDetails.Rows.Count;
                    Int32 Flag = 0;
                    foreach (DataRow drw in m_dtbSaleDetails.Rows)
                    {
                        objSaleInvoiceProperty.invoice_detail_id = Val.ToInt(drw["invoice_detail_id"]);
                        objSaleInvoiceProperty.assort_id = Val.ToInt(drw["assort_id"]);
                        objSaleInvoiceProperty.sieve_id = Val.ToInt(drw["sieve_id"]);
                        objSaleInvoiceProperty.pcs = Val.ToInt(drw["pcs"]);
                        objSaleInvoiceProperty.carat = Val.ToDecimal(drw["carat"]);

                        if (FlagCount == TotalCount)
                        {
                            Flag = 1;
                        }

                        IntRes = objSaleInvoice.Delete(objSaleInvoiceProperty, Flag, DLL.GlobalDec.EnumTran.Continue, Conn);

                        FlagCount++;
                        Count++;
                        IntCounter++;
                        IntRes++;
                        SetControlPropertyValue(lblProgressCount, "Text", Count.ToString() + "" + "/" + "" + TotalCount.ToString() + " Completed....");
                    }

                    if (IntRes > 0)
                    {
                        objSaleInvoice = new SaleInvoice();
                        objSaleInvoiceProperty = new Sales_InvoiceProperty();

                        foreach (DataRow drw in dtbMemoRecDelete.Rows)
                        {
                            objSaleInvoiceProperty.memo_id = Val.ToInt(drw["memo_id"]);
                            objSaleInvoiceProperty.rej_carat = Val.ToDecimal(drw["rejection_carat"]);
                            objSaleInvoiceProperty.carat = Val.ToDecimal(drw["return_carat"]);
                            objSaleInvoiceProperty.assort_id = Val.ToInt(drw["assort_id"]);
                            objSaleInvoiceProperty.sieve_id = Val.ToInt(drw["sieve_id"]);

                            IntRes = objSaleInvoice.Delete_MemoRecive_Details(objSaleInvoiceProperty, Flag, DLL.GlobalDec.EnumTran.Continue, Conn);
                        }
                    }

                    Conn.Inter1.Commit();
                }
                else
                {
                    Global.Message("Invoice ID not found");
                    Conn.Inter1.Rollback();
                    Conn = null;
                    return;
                }
            }
            catch (Exception ex)
            {
                IntRes = -1;
                Conn.Inter1.Rollback();
                Conn = null;
                General.ShowErrors(ex.ToString());
                if (ex.InnerException != null)
                {
                    Global.Message(ex.InnerException.ToString());
                }
            }
            finally
            {
                objSaleInvoiceProperty = null;
            }
        }
        private void backgroundWorker_SaleDelete_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                panelProgress.Visible = false;
                if (IntRes > 0)
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Sale Invoice Data Delete Successfully");
                        ClearDetails();
                    }
                    else
                    {
                        Global.Confirm("Sale Invoice Data Delete Successfully");
                        ClearDetails();
                    }
                }
                else
                {
                    Global.Confirm("Error In Sale Invoice Delete");
                    txtInvoiceNo.Focus();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }
        private void backgroundWorker_SaleInvoice_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.Default;
                Conn = new BeginTranConnection(true, false);

                Sales_InvoiceProperty objSaleInvoiceProperty = new Sales_InvoiceProperty();
                SaleInvoice objSaleInvoice = new SaleInvoice();
                try
                {
                    IntRes = 0;

                    objSaleInvoiceProperty.invoice_id = Val.ToInt(lblMode.Tag);
                    objSaleInvoiceProperty.invoice_No = Val.ToString(txtInvoiceNo.Text);
                    objSaleInvoiceProperty.company_id = Val.ToInt(GlobalDec.gEmployeeProperty.company_id);
                    objSaleInvoiceProperty.branch_id = Val.ToInt(GlobalDec.gEmployeeProperty.branch_id);
                    objSaleInvoiceProperty.location_id = Val.ToInt(GlobalDec.gEmployeeProperty.location_id);
                    objSaleInvoiceProperty.department_id = Val.ToInt(GlobalDec.gEmployeeProperty.department_id);

                    objSaleInvoiceProperty.invoice_date = Val.DBDate(dtpInvoiceDate.Text);
                    objSaleInvoiceProperty.delivery_type_id = Val.ToInt(lueDeliveryType.EditValue);
                    objSaleInvoiceProperty.remarks = Val.ToString(txtEntry.Text);

                    objSaleInvoiceProperty.form_id = m_numForm_id;

                    objSaleInvoiceProperty.Bill_To_Party_Id = Val.ToInt(lueBilledToParty.EditValue);
                    objSaleInvoiceProperty.Shipped_To_Party_Id = Val.ToInt(lueShippedToParty.EditValue);
                    objSaleInvoiceProperty.Refrenace_Id = Val.ToInt(lueReferance.EditValue);

                    objSaleInvoiceProperty.Broker_Id = Val.ToInt(lueBroker.EditValue);
                    objSaleInvoiceProperty.Term_Days = Val.ToInt(txtTermDays.EditValue);
                    objSaleInvoiceProperty.Add_On_Days = Val.ToInt(txtAddOnDays.EditValue);
                    objSaleInvoiceProperty.due_date = Val.DBDate(dtpDueDate.Text);
                    objSaleInvoiceProperty.demand_master_id = Val.ToInt(lblDemandNo.Tag);
                    objSaleInvoiceProperty.memo_master_id = Val.ToInt(lueMemoNo.EditValue);

                    objSaleInvoiceProperty.final_Term_Days = Val.ToInt(txtFinalTermDays.EditValue);
                    objSaleInvoiceProperty.final_due_date = Val.DBDate(dtpFinalDueDate.Text);

                    objSaleInvoiceProperty.Special_Remark = Val.ToString(txtJKK.Text);
                    objSaleInvoiceProperty.cod = Val.ToString(txtCod.Text);
                    objSaleInvoiceProperty.Client_Remark = Val.ToString(txtSaleRemark.Text);
                    objSaleInvoiceProperty.Payment_Remark = Val.ToString(txtAccountRemark.Text);

                    objSaleInvoiceProperty.total_pcs = Math.Round(Val.ToDecimal(clmPcs.SummaryItem.SummaryValue), 3);
                    objSaleInvoiceProperty.total_carat = Math.Round(Val.ToDecimal(clmDetCarat.SummaryItem.SummaryValue), 3);

                    objSaleInvoiceProperty.Gross_Amount = Math.Round(Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue), 3);

                    objSaleInvoiceProperty.cgst_rate = Val.ToDecimal(txtCGSTPer.Text);
                    objSaleInvoiceProperty.cgst_amount = Val.ToDecimal(txtCGSTAmt.Text);
                    objSaleInvoiceProperty.sgst_rate = Val.ToDecimal(txtSGSTPer.Text);
                    objSaleInvoiceProperty.sgst_amount = Val.ToDecimal(txtSGSTAmt.Text);
                    objSaleInvoiceProperty.igst_rate = Val.ToDecimal(txtIGSTPer.Text);
                    objSaleInvoiceProperty.igst_amount = Val.ToDecimal(txtIGSTAmt.Text);

                    objSaleInvoiceProperty.Brokerage_Per = Val.ToDecimal(txtBrokeragePer.Text);
                    objSaleInvoiceProperty.Brokerage_Amt = Val.ToDecimal(txtBrokerageAmt.Text);
                    objSaleInvoiceProperty.Discount_Per = Val.ToDecimal(txtDiscountPer.Text);
                    objSaleInvoiceProperty.Discount_Amt = Val.ToDecimal(txtDiscountAmt.Text);
                    objSaleInvoiceProperty.Interest_Per = Val.ToDecimal(txtInterestPer.Text);
                    objSaleInvoiceProperty.Interest_Amt = Val.ToDecimal(txtInterestAmt.Text);
                    objSaleInvoiceProperty.Shipping_Charge = Val.ToDecimal(txtShippingCharge.Text);

                    objSaleInvoiceProperty.net_amount = Val.ToDecimal(txtNetAmount.Text);
                    objSaleInvoiceProperty.Currency_Type = lueCurrency.Text;
                    objSaleInvoiceProperty.Currency_ID = Val.ToInt(m_numCurrency_id);
                    objSaleInvoiceProperty.exchange_rate = Val.ToDecimal(txtExchangeRate.Text);
                    objSaleInvoiceProperty.Seller_ID = Val.ToInt(lueSeller.EditValue);

                    //int IntRes = objSaleInvoice.Save(objSaleInvoiceProperty, m_dtbSaleDetails);
                    objSaleInvoiceProperty = objSaleInvoice.Save(objSaleInvoiceProperty, DLL.GlobalDec.EnumTran.Start, Conn);

                    Int64 NewmInvoiceid = Val.ToInt64(objSaleInvoiceProperty.invoice_id);

                    int IntCounter = 0;
                    int Count = 0;
                    int TotalCount = m_dtbSaleDetails.Rows.Count;

                    foreach (DataRow drw in m_dtbSaleDetails.Rows)
                    {
                        objSaleInvoiceProperty = new Sales_InvoiceProperty();
                        objSaleInvoiceProperty.invoice_id = Val.ToInt32(NewmInvoiceid);
                        objSaleInvoiceProperty.invoice_detail_id = Val.ToInt(drw["invoice_detail_id"]);
                        objSaleInvoiceProperty.assort_id = Val.ToInt(drw["assort_id"]);
                        objSaleInvoiceProperty.sieve_id = Val.ToInt(drw["sieve_id"]);
                        objSaleInvoiceProperty.sub_sieve_id = Val.ToInt(drw["sub_sieve_id"]);
                        objSaleInvoiceProperty.pcs = Val.ToInt(drw["pcs"]);
                        objSaleInvoiceProperty.carat = Val.ToDecimal(drw["carat"]);
                        objSaleInvoiceProperty.rate = Val.ToDecimal(drw["rate"]);
                        objSaleInvoiceProperty.amount = Val.ToDecimal(drw["amount"]);
                        objSaleInvoiceProperty.discount = Val.ToDecimal(drw["discount"]);

                        objSaleInvoiceProperty.rej_carat = Val.ToDecimal(drw["rej_carat"]);
                        objSaleInvoiceProperty.rej_percentage = Val.ToDecimal(drw["rej_percentage"]);

                        objSaleInvoiceProperty.old_carat = Val.ToDecimal(drw["old_carat"]);
                        objSaleInvoiceProperty.old_pcs = Val.ToInt(drw["old_pcs"]);
                        objSaleInvoiceProperty.old_rej_carat = Val.ToDecimal(drw["old_rej_carat"]);
                        objSaleInvoiceProperty.old_rej_percentage = Val.ToInt(drw["old_rej_percentage"]);
                        objSaleInvoiceProperty.flag = Val.ToInt(drw["flag"]);
                        objSaleInvoiceProperty.old_assort_id = Val.ToInt(drw["old_assort_id"]);
                        objSaleInvoiceProperty.old_sieve_id = Val.ToInt(drw["old_sieve_id"]);
                        objSaleInvoiceProperty.old_sub_sieve_id = Val.ToInt(drw["old_sub_sieve_id"]);
                        objSaleInvoiceProperty.current_rate = Val.ToDecimal(drw["current_rate"]);
                        objSaleInvoiceProperty.current_amount = Val.ToDecimal(drw["current_amount"]);
                        objSaleInvoiceProperty.old_sieve_id = Val.ToInt(drw["old_sieve_id"]);
                        objSaleInvoiceProperty.Currency_ID = Val.ToInt(m_numCurrency_id);
                        objSaleInvoiceProperty.loss_carat = Val.ToDecimal(drw["loss_carat"]);
                        objSaleInvoiceProperty.old_loss_carat = Val.ToDecimal(drw["old_loss_carat"]);
                        objSaleInvoiceProperty.is_memo = Val.ToInt(drw["is_memo"]);

                        objSaleInvoiceProperty.purchase_rate = Val.ToDecimal(drw["purchase_rate"]);
                        objSaleInvoiceProperty.purchase_amount = Val.ToDecimal(drw["purchase_amount"]);


                        IntRes = objSaleInvoice.Save_Detail(objSaleInvoiceProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                        Count++;
                        IntCounter++;
                        IntRes++;
                        SetControlPropertyValue(lblProgressCount, "Text", Count.ToString() + "" + "/" + "" + TotalCount.ToString() + " Completed....");
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
                    objSaleInvoiceProperty = null;
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
        private void backgroundWorker_SaleInvoice_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                panelProgress.Visible = false;
                if (IntRes > 0)
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Sale Invoice Data Save Successfully");
                        ClearDetails();
                        PopulateDetails();
                    }
                    else
                    {
                        Global.Confirm("Sale Invoice Data Update Successfully");
                        ClearDetails();
                        PopulateDetails();
                    }
                }
                else
                {
                    Global.Confirm("Error In Sale Invoice");
                    txtInvoiceNo.Focus();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }
        private void txtRejPer_Validated(object sender, EventArgs e)
        {
            if (txtRejPer.Text != "")
            {
                txtRejCarat.Text = Val.ToString(Math.Round(((Val.ToDecimal(txtCarat.Text) + Val.ToDecimal(txtRejCarat.Text)) * Val.ToDecimal(txtRejPer.Text) / 100), 3));
            }
        }
        private void txtRejCarat_Validated(object sender, EventArgs e)
        {
            if (txtRejCarat.Text != "")
            {
                txtRejPer.Text = Val.ToString(Math.Round((Val.ToDecimal(txtRejCarat.Text) / (Val.ToDecimal(txtCarat.Text) + Val.ToDecimal(txtRejCarat.Text)) * 100), 3));
            }
        }

        #region "Grid Events" 
        private void dgvSalesDetails_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                if (Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) > 0 && Val.ToDecimal(clmDetCarat.SummaryItem.SummaryValue) > 0)
                {
                    m_numSummDetRate = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) / Val.ToDecimal(clmDetCarat.SummaryItem.SummaryValue)), 2, MidpointRounding.AwayFromZero);

                }
                else
                {
                    m_numSummDetRate = 0;
                }
                if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == "rate")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                        e.TotalValue = m_numSummDetRate;
                }

            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        private void dgvSaleInvoice_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                objSaleInvoice = new SaleInvoice();
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        m_blncheckevents = true;

                        DataRow Drow = dgvSaleInvoice.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt32(Drow["invoice_id"]);

                        dtpInvoiceDate.Text = Val.DBDate(Val.ToString(Drow["invoice_date"]));
                        lueDeliveryType.EditValue = Val.ToInt(Drow["delivery_type_id"]);
                        txtInvoiceNo.Text = Val.ToString(Drow["invoice_no"]);
                        lueBilledToParty.EditValue = Val.ToInt(Drow["billed_to_party_id"]);
                        lueShippedToParty.EditValue = Val.ToInt(Drow["shipped_to_party_id"]);
                        lueReferance.EditValue = Val.ToInt(Drow["refrenace_id"]);

                        lueBroker.EditValue = Val.ToInt(Drow["broker_id"]);
                        lueSeller.EditValue = Val.ToInt(Drow["seller_id"]);
                        txtShippingCharge.Text = Val.ToString(Drow["shipping"]);
                        txtTermDays.Text = Val.ToString(Drow["term_days"]);
                        txtFinalTermDays.Text = Val.ToString(Drow["final_term_days"]);
                        dtpFinalDueDate.Text = Val.DBDate(Val.ToString(Drow["final_due_date"]));
                        txtAddOnDays.Text = Val.ToString(Drow["add_on_days"]);
                        dtpDueDate.Text = Val.DBDate(Val.ToString(Drow["due_date"]));
                        lueDemandNo.EditValue = Val.ToInt(Drow["demand_master_id"]);
                        lueMemoNo.EditValue = Val.ToInt(Drow["memo_master_id"]);
                        txtEntry.Text = Val.ToString(Drow["remarks"]);
                        txtJKK.Text = Val.ToString(Drow["special_remarks"]);
                        txtCod.Text = Val.ToString(Drow["cod"]);
                        txtAccountRemark.Text = Val.ToString(Drow["payment_remarks"]);
                        txtSaleRemark.Text = Val.ToString(Drow["client_remarks"]);
                        txtBrokeragePer.Text = Val.ToString(Drow["brokerage_per"]);
                        txtBrokerageAmt.Text = Val.ToString(Drow["brokerage_amount"]);
                        txtDiscountPer.Text = Val.ToString(Drow["discount_per"]);
                        txtDiscountAmt.Text = Val.ToString(Drow["discount_amount"]);
                        txtInterestPer.Text = Val.ToString(Drow["interest_per"]);
                        txtInterestAmt.Text = Val.ToString(Drow["interest_amount"]);
                        txtCGSTPer.Text = Val.ToString(Drow["cgst_per"]);
                        txtCGSTAmt.Text = Val.ToString(Drow["cgst_amount"]);
                        txtSGSTPer.Text = Val.ToString(Drow["sgst_per"]);
                        txtSGSTAmt.Text = Val.ToString(Drow["sgst_amount"]);
                        txtIGSTPer.Text = Val.ToString(Drow["igst_per"]);
                        txtIGSTAmt.Text = Val.ToString(Drow["igst_amount"]);
                        txtNetAmount.Text = Val.ToString(Drow["net_amount"]);
                        lueCurrency.Text = Val.ToString(Drow["currency"]);
                        txtExchangeRate.EditValue = Val.ToDecimal(Drow["exchange_rate"]);

                        m_dtbSaleDetails = objSaleInvoice.GetDataDetails(Val.ToInt(lblMode.Tag));
                        grdSalesDetails.DataSource = m_dtbSaleDetails;

                        ttlbSaleInvoice.SelectedTabPage = tblSaledetail;
                        txtInvoiceNo.Focus();
                        btnBrowse.Enabled = false;
                        //lueCurrency.ReadOnly = true;
                        m_IsValid = false;
                        m_IsUpdate = true;
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void dgvSalesDetails_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvSalesDetails.GetDataRow(e.RowHandle);
                        btnAdd.Text = "&Update";
                        lueSieveName.Text = Val.ToString(Drow["sieve_name"]);
                        lueSieveName.Tag = Val.ToInt64(Drow["sieve_id"]);
                        lueSubSieveName.Text = Val.ToString(Drow["sub_sieve_name"]);
                        lueSubSieveName.Tag = Val.ToInt64(Drow["sub_sieve_id"]);
                        lueAssortName.Text = Val.ToString(Drow["assort_name"]);
                        lueAssortName.Tag = Val.ToInt64(Drow["assort_id"]);
                        txtPcs.Text = Val.ToString(Drow["pcs"]);
                        txtCarat.Text = Val.ToString(Drow["carat"]);
                        txtRejCarat.Text = Val.ToString(Drow["rej_carat"]);
                        txtRejPer.Text = Val.ToString(Drow["rej_percentage"]);
                        txtRate.Text = Val.ToString(Drow["rate"]);
                        txtAmount.Text = Val.ToString(Drow["amount"]);

                        txtPurchaseRate.Text = Val.ToString(Drow["purchase_rate"]);
                        txtPurchaseAmount.Text = Val.ToString(Drow["purchase_amount"]);

                        m_numcarat = Val.ToDecimal(Drow["carat"]);
                        m_invoice_detail_id = Val.ToInt(Drow["invoice_detail_id"]);
                        m_update_srno = Val.ToInt(Drow["sr_no"]);
                        txtLossCarat.Text = Val.ToString(Drow["loss_carat"]);
                        lblOldCarat.Text = Val.ToString(Drow["carat"]);
                        GetCarat();
                        m_IsValid = true;
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void dgvSaleInvoice_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                if (Val.ToDecimal(clmTotalAmount.SummaryItem.SummaryValue) > 0 && Val.ToDecimal(clmTotalCarat.SummaryItem.SummaryValue) > 0)
                {
                    m_numSummRate = Math.Round((Val.ToDecimal(clmTotalAmount.SummaryItem.SummaryValue) / Val.ToDecimal(clmTotalCarat.SummaryItem.SummaryValue)), 2, MidpointRounding.AwayFromZero);

                }
                else
                {
                    m_numSummRate = 0;
                }
                if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == "rate")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                        e.TotalValue = m_numSummRate;
                }

            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        #endregion

        #endregion

        #region Functions
        private bool LoadDefaults()
        {
            bool blnReturn = true;
            try
            {
                Global.LOOKUPParty(lueBilledToParty);
                Global.LOOKUPParty(lueShippedToParty);
                Global.LOOKUPParty(lueReferance);

                Global.LOOKUPBroker(lueBroker);
                Global.LOOKUPDeliveryType(lueDeliveryType);
                Global.LOOKUPParty(lueBillToParty);


                m_dtbMemoNo = objSaleInvoice.GetMemoNo();

                lueMemoNo.Properties.DataSource = m_dtbMemoNo;
                lueMemoNo.Properties.ValueMember = "memo_master_id";
                lueMemoNo.Properties.DisplayMember = "memo_no";

                m_dtbDemandNo = objSaleInvoice.GetDemandNo();
                lueDemandNo.Properties.DataSource = m_dtbDemandNo;
                lueDemandNo.Properties.ValueMember = "demand_master_id";
                lueDemandNo.Properties.DisplayMember = "demand_no";

                m_dtbCurrencyType = Global.CurrencyType();
                lueCurrency.Properties.DataSource = m_dtbCurrencyType;
                lueCurrency.Properties.ValueMember = "currency_id";
                lueCurrency.Properties.DisplayMember = "currency";
                //lueCurrency.EditValue = "INR";

                //m_dtbCurrencyType.Columns.Add("Currency_Type");
                //m_dtbCurrencyType.Rows.Add("INR");
                //m_dtbCurrencyType.Rows.Add("USD");
                //m_dtbCurrencyType.Rows.Add("HKD");
                //lueCurrency.Properties.DataSource = m_dtbCurrencyType;
                //lueCurrency.Properties.ValueMember = "Currency_Type";
                //lueCurrency.Properties.DisplayMember = "Currency_Type";
                lueCurrency.EditValue = Val.ToInt64(GlobalDec.gEmployeeProperty.currency_id);


                m_dtbAssorts = objAssort.GetData(1);
                lueAssortName.Properties.DataSource = m_dtbAssorts;
                lueAssortName.Properties.ValueMember = "assort_id";
                lueAssortName.Properties.DisplayMember = "assort_name";

                m_dtbSieve = objSieve.GetData(1);
                lueSieveName.Properties.DataSource = m_dtbSieve;
                lueSieveName.Properties.ValueMember = "sieve_id";
                lueSieveName.Properties.DisplayMember = "sieve_name";

                m_opDate = Global.GetDate();
                dtpFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpFromDate.EditValue = Val.DBDate(m_opDate.Rows[0]["opening_date"].ToString());

                dtpToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpToDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpToDate.EditValue = DateTime.Now;

                dtpInvoiceDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpInvoiceDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpInvoiceDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpInvoiceDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpInvoiceDate.EditValue = DateTime.Now;

                m_dtbSeller = objSaleInvoice.GetSellerName("Seller");

                lueSeller.Properties.DataSource = m_dtbSeller;
                lueSeller.Properties.ValueMember = "employee_id";
                lueSeller.Properties.DisplayMember = "employee_name";

                btnSearch_Click(null, null);

                if (GlobalDec.gEmployeeProperty.location_id == 2)
                {
                    txtBrokeragePer.EditValue = 1;
                }
                else
                {
                    txtBrokeragePer.EditValue = null;
                }

            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            finally
            {
                objAssort = null;
                objSieve = null;
            }

            return blnReturn;
        }
        private bool AddInGrid()
        {
            bool blnReturn = true;

            try
            {
                m_blnadd = true;
                m_blnsave = false;


                if (!ValidateDetails())
                {
                    m_blnadd = false;
                    blnReturn = false;
                    return blnReturn;
                }
                objSaleInvoice = new SaleInvoice();
                DataTable p_dtbDetail = new DataTable();

                //p_dtbDetail = objSaleInvoice.GetCheckPriceList(m_numCurrency_id, Val.ToInt(GlobalDec.gEmployeeProperty.rate_type_id));
                p_dtbDetail = objSaleInvoice.GetCheckPriceList(Val.ToInt(GlobalDec.gEmployeeProperty.currency_id), Val.ToInt(GlobalDec.gEmployeeProperty.rate_type_id));

                if (p_dtbDetail.Rows.Count <= 0)
                {
                    Global.Message("Selected currency type price not found in master please check", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    blnReturn = false;
                    return blnReturn;
                }

                decimal numStockCarat = 0;
                if (btnAdd.Text == "&Add")
                {
                    //DataTable m_dtbStockCarat = new DataTable();
                    objSaleInvoice = new SaleInvoice();
                    //m_dtbStockCarat = objSaleInvoice.GetStockCarat(GlobalDec.gEmployeeProperty.company_id, GlobalDec.gEmployeeProperty.branch_id, GlobalDec.gEmployeeProperty.location_id, GlobalDec.gEmployeeProperty.department_id, Val.ToInt(lueAssortName.EditValue), Val.ToInt(lueSieveName.EditValue));                    
                    if (m_dtbStockCarat.Rows.Count > 0)
                    {
                        numStockCarat = Val.ToDecimal(m_dtbStockCarat.Rows[0]["stock_carat"]);
                    }

                    if (numStockCarat < (Val.ToDecimal(txtCarat.Text) + Val.ToDecimal(txtLossCarat.Text)))
                    {
                        Global.Message("Please check enter carat more then stock carat", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCarat.Focus();
                        blnReturn = false;
                        return blnReturn;
                    }

                    DataRow[] dr = m_dtbSaleDetails.Select("sieve_id = " + Val.ToInt(lueSieveName.EditValue) + " AND assort_id = " + Val.ToInt(lueAssortName.EditValue) + " AND sub_sieve_id = " + Val.ToInt(lueSubSieveName.EditValue));

                    if (dr.Count() == 1)
                    {
                        Global.Message("Record already exists in grid", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lueAssortName.Focus();
                        blnReturn = false;
                        return blnReturn;
                    }
                    DataRow drwNew = m_dtbSaleDetails.NewRow();
                    decimal numCarat = Val.ToDecimal(txtCarat.Text);
                    decimal numRate = Val.ToDecimal(txtRate.Text);
                    decimal numAmount = Val.ToDecimal(txtAmount.Text);
                    int numPcs = Val.ToInt(txtPcs.Text);
                    decimal numLossCarat = Val.ToDecimal(txtLossCarat.Text);

                    drwNew["invoice_id"] = Val.ToInt(0);
                    drwNew["invoice_detail_id"] = Val.ToInt(0);

                    drwNew["assort_id"] = Val.ToInt(lueAssortName.EditValue);
                    drwNew["assort_name"] = Val.ToString(lueAssortName.Text);

                    drwNew["sieve_id"] = Val.ToInt(lueSieveName.EditValue);
                    drwNew["sieve_name"] = Val.ToString(lueSieveName.Text);

                    drwNew["sub_sieve_id"] = Val.ToInt(lueSubSieveName.EditValue);
                    drwNew["sub_sieve_name"] = Val.ToString(lueSubSieveName.Text);

                    drwNew["pcs"] = numPcs;
                    drwNew["carat"] = numCarat;
                    drwNew["rej_carat"] = Val.ToDecimal(txtRejCarat.Text);
                    drwNew["rej_percentage"] = Val.ToDecimal(txtRejPer.Text);
                    drwNew["rate"] = Val.ToDecimal(txtRate.Text);
                    drwNew["amount"] = Math.Round(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text), 3);
                    drwNew["old_carat"] = Val.ToDecimal(0);
                    drwNew["old_pcs"] = Val.ToDecimal(0);
                    drwNew["old_rej_carat"] = Val.ToDecimal(0);
                    drwNew["old_rej_percentage"] = Val.ToDecimal(0);
                    drwNew["flag"] = Val.ToInt(0);
                    m_srno = m_srno + 1;
                    drwNew["sr_no"] = Val.ToInt(m_srno);

                    drwNew["current_rate"] = m_current_rate;
                    drwNew["current_amount"] = m_current_amount;
                    drwNew["loss_carat"] = numLossCarat;
                    drwNew["old_loss_carat"] = Val.ToDecimal(0);

                    drwNew["purchase_rate"] = Val.ToDecimal(txtPurchaseRate.Text);
                    drwNew["purchase_amount"] = Math.Round(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtPurchaseRate.Text), 3);

                    m_dtbSaleDetails.Rows.Add(drwNew);

                    dgvSalesDetails.MoveLast();

                    //DataView dv = m_dtbSaleDetails.DefaultView;
                    //dv.Sort = "sr_no desc";
                    //DataTable sortedDT = dv.ToTable();

                    decimal CGST_amt = Math.Round(Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) * Val.ToDecimal(txtCGSTPer.Text) / 100, 0);
                    txtCGSTAmt.Text = CGST_amt.ToString();
                    decimal SGST_amt = Math.Round(Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) * Val.ToDecimal(txtSGSTPer.Text) / 100, 0);
                    txtSGSTAmt.Text = SGST_amt.ToString();
                    decimal IGST_amt = Math.Round(Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) * Val.ToDecimal(txtIGSTPer.Text) / 100, 0);
                    txtIGSTAmt.Text = IGST_amt.ToString();

                    decimal Shipping_Charge = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) + Val.ToDecimal(txtInterestAmt.Text) + Val.ToDecimal(txtCGSTAmt.Text) + Val.ToDecimal(txtSGSTAmt.Text) + Val.ToDecimal(txtIGSTAmt.Text)) - (Val.ToDecimal(txtDiscountAmt.Text)) + Val.ToDecimal(txtShippingCharge.Text), 0);
                    txtNetAmount.Text = Shipping_Charge.ToString();
                }
                else if (btnAdd.Text == "&Update")
                {
                    if (!m_IsUpdate)
                    {
                        Global.Message("You can't update this record", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCarat.Focus();
                        blnReturn = false;
                        return blnReturn;
                    }

                    objSaleInvoice = new SaleInvoice();
                    if (Val.ToDecimal(txtCarat.Text) > (Val.ToDecimal(lblBalCrt.Text) + Val.ToDecimal(lblOldCarat.Text)))
                    {
                        Global.Message("Please check enter carat more then stock carat", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCarat.Focus();
                        blnReturn = false;
                        return blnReturn;
                    }
                    if (Val.ToDecimal(txtCarat.Text) > m_numcarat)
                    {
                        if (m_invoice_detail_id == 0)
                        {
                            //DataTable m_dtbStockCarat = new DataTable();
                            //m_dtbStockCarat = objSaleInvoice.GetStockCarat(GlobalDec.gEmployeeProperty.company_id, GlobalDec.gEmployeeProperty.branch_id, GlobalDec.gEmployeeProperty.location_id, GlobalDec.gEmployeeProperty.department_id, Val.ToInt(lueAssortName.EditValue), Val.ToInt(lueSieveName.EditValue));

                            //if (m_dtbStockCarat.Rows.Count > 0)
                            //{
                            //    numStockCarat = Val.ToDecimal(m_dtbStockCarat.Rows[0]["stock_carat"]);
                            //}

                            //if (numStockCarat < Val.ToDecimal(txtCarat.Text))
                            //{
                            //    Global.Message("Please check enter carat more then stock carat", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    txtCarat.Focus();
                            //    blnReturn = false;
                            //    return blnReturn;
                            //}
                        }
                        else
                        {
                            //DataTable m_dtbStockCarat = new DataTable();
                            //m_dtbStockCarat = objSaleInvoice.GetStockCarat(GlobalDec.gEmployeeProperty.company_id, GlobalDec.gEmployeeProperty.branch_id, GlobalDec.gEmployeeProperty.location_id, GlobalDec.gEmployeeProperty.department_id, Val.ToInt(lueAssortName.EditValue), Val.ToInt(lueSieveName.EditValue));

                            //decimal p_numdiff_Carat = Val.ToDecimal(txtCarat.Text) - m_numcarat;

                            //if (m_dtbStockCarat.Rows.Count > 0)
                            //{
                            //    numStockCarat = Val.ToDecimal(m_dtbStockCarat.Rows[0]["stock_carat"]);
                            //}

                            //if (numStockCarat < Val.ToDecimal(p_numdiff_Carat))
                            //{
                            //    Global.Message("Please check enter carat more then stock carat", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    txtCarat.Focus();
                            //    blnReturn = false;
                            //    return blnReturn;
                            //}
                        }

                    }

                    if (m_dtbSaleDetails.Select("assort_id ='" + Val.ToInt(lueAssortName.EditValue) + "' AND sieve_id ='" + Val.ToInt(lueSieveName.EditValue) + "'").Length > 0)
                    {
                        for (int i = 0; i < m_dtbSaleDetails.Rows.Count; i++)
                        {
                            if (m_dtbSaleDetails.Select("invoice_detail_id ='" + m_invoice_detail_id + "' AND sr_no = '" + m_update_srno + "'").Length > 0)
                            {
                                if (m_dtbSaleDetails.Rows[m_update_srno - 1]["invoice_detail_id"].ToString() == m_invoice_detail_id.ToString())
                                {
                                    m_dtbSaleDetails.Rows[m_update_srno - 1]["carat"] = Val.ToDecimal(txtCarat.Text).ToString();
                                    m_dtbSaleDetails.Rows[m_update_srno - 1]["pcs"] = Val.ToInt(txtPcs.Text);
                                    m_dtbSaleDetails.Rows[m_update_srno - 1]["rej_carat"] = Val.ToDecimal(txtRejCarat.Text);
                                    m_dtbSaleDetails.Rows[m_update_srno - 1]["rej_percentage"] = Val.ToDecimal(txtRejPer.Text);
                                    m_dtbSaleDetails.Rows[m_update_srno - 1]["rate"] = Val.ToDecimal(txtRate.Text);
                                    m_dtbSaleDetails.Rows[m_update_srno - 1]["flag"] = 1;
                                    m_dtbSaleDetails.Rows[m_update_srno - 1]["amount"] = Math.Round(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text), 3);
                                    m_dtbSaleDetails.Rows[m_update_srno - 1]["current_rate"] = m_current_rate;
                                    m_dtbSaleDetails.Rows[m_update_srno - 1]["current_amount"] = m_current_amount;
                                    m_dtbSaleDetails.Rows[m_update_srno - 1]["loss_carat"] = Val.ToDecimal(txtLossCarat.Text);

                                    m_dtbSaleDetails.Rows[m_update_srno - 1]["purchase_rate"] = Val.ToDecimal(txtPurchaseRate.Text);
                                    m_dtbSaleDetails.Rows[m_update_srno - 1]["purchase_amount"] = Math.Round(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtPurchaseRate.Text), 3);

                                    decimal Shipping_Charge = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) + Val.ToDecimal(txtInterestAmt.Text) + Val.ToDecimal(txtCGSTAmt.Text) + Val.ToDecimal(txtSGSTAmt.Text) + Val.ToDecimal(txtIGSTAmt.Text)) - (Val.ToDecimal(txtDiscountAmt.Text)) + Val.ToDecimal(txtShippingCharge.Text), 0);
                                    txtNetAmount.Text = Shipping_Charge.ToString();
                                    break;
                                }
                            }
                        }
                        btnAdd.Text = "&Add";
                    }
                    else
                    {
                        for (int i = 0; i < m_dtbSaleDetails.Rows.Count; i++)
                        {
                            if (m_dtbSaleDetails.Select("invoice_detail_id ='" + m_invoice_detail_id + "' AND sr_no = '" + m_update_srno + "'").Length > 0)
                            {
                                if (m_dtbSaleDetails.Rows[m_update_srno - 1]["invoice_detail_id"].ToString() == m_invoice_detail_id.ToString())
                                {
                                    m_dtbSaleDetails.Rows[m_update_srno - 1]["carat"] = Val.ToDecimal(txtCarat.Text).ToString();
                                    m_dtbSaleDetails.Rows[m_update_srno - 1]["pcs"] = Val.ToInt(txtPcs.Text);
                                    m_dtbSaleDetails.Rows[m_update_srno - 1]["rej_carat"] = Val.ToDecimal(txtRejCarat.Text);
                                    m_dtbSaleDetails.Rows[m_update_srno - 1]["rej_percentage"] = Val.ToDecimal(txtRejPer.Text);
                                    m_dtbSaleDetails.Rows[m_update_srno - 1]["rate"] = Val.ToDecimal(txtRate.Text);
                                    m_dtbSaleDetails.Rows[m_update_srno - 1]["flag"] = 1;
                                    m_dtbSaleDetails.Rows[m_update_srno - 1]["assort_id"] = Val.ToInt(lueAssortName.EditValue);
                                    m_dtbSaleDetails.Rows[m_update_srno - 1]["sieve_id"] = Val.ToInt(lueSieveName.EditValue);
                                    m_dtbSaleDetails.Rows[m_update_srno - 1]["sub_sieve_id"] = Val.ToInt(lueSubSieveName.EditValue);
                                    m_dtbSaleDetails.Rows[m_update_srno - 1]["assort_name"] = Val.ToString(lueAssortName.Text);
                                    m_dtbSaleDetails.Rows[m_update_srno - 1]["sieve_name"] = Val.ToString(lueSieveName.Text);
                                    m_dtbSaleDetails.Rows[m_update_srno - 1]["sub_sieve_name"] = Val.ToString(lueSubSieveName.Text);

                                    m_dtbSaleDetails.Rows[m_update_srno - 1]["amount"] = Math.Round(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text), 3);
                                    m_dtbSaleDetails.Rows[m_update_srno - 1]["current_rate"] = m_current_rate;
                                    m_dtbSaleDetails.Rows[m_update_srno - 1]["current_amount"] = m_current_amount;
                                    m_dtbSaleDetails.Rows[m_update_srno - 1]["loss_carat"] = Val.ToDecimal(txtLossCarat.Text);

                                    m_dtbSaleDetails.Rows[m_update_srno - 1]["purchase_rate"] = Val.ToDecimal(txtRate.Text);
                                    m_dtbSaleDetails.Rows[m_update_srno - 1]["purchase_amount"] = Math.Round(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text), 3);

                                    decimal Shipping_Charge = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) + Val.ToDecimal(txtInterestAmt.Text) + Val.ToDecimal(txtCGSTAmt.Text) + Val.ToDecimal(txtSGSTAmt.Text) + Val.ToDecimal(txtIGSTAmt.Text)) - (Val.ToDecimal(txtDiscountAmt.Text)) + Val.ToDecimal(txtShippingCharge.Text), 0);
                                    txtNetAmount.Text = Shipping_Charge.ToString();
                                }
                            }
                        }
                        btnAdd.Text = "&Add";
                    }
                    dgvSalesDetails.MoveLast();
                    m_IsUpdate = false;
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
        }
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();

            try
            {
                if (txtExchangeRate.Text.ToString() == "0")
                {
                    lstError.Add(new ListError(15, "Exchange Rate"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtExchangeRate.Focus();
                    }
                }
                if (m_blnsave)
                {
                    if (m_dtbSaleDetails.Rows.Count == 0)
                    {
                        lstError.Add(new ListError(22, "Record"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                        }
                    }
                    if (dgvSalesDetails == null)
                    {
                        lstError.Add(new ListError(22, "Record"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                        }
                    }
                    var result = DateTime.Compare(Convert.ToDateTime(dtpInvoiceDate.Text), DateTime.Today);
                    if (result > 0)
                    {
                        lstError.Add(new ListError(5, " Invoice Date Not Be Greater Than Today Date"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            dtpInvoiceDate.Focus();
                        }
                    }

                    if (m_IsValid)
                    {
                        foreach (DataRow drw in m_dtbSaleDetails.Rows)
                        {
                            DataTable m_dtbStockCarat = new DataTable();
                            decimal p_numStockCarat = 0;
                            decimal p_numOldCarat = 0;
                            decimal p_numOldRejCarat = 0;

                            p_numOldCarat = Val.ToDecimal(drw["carat"]);
                            p_numOldRejCarat = Val.ToDecimal(drw["rej_carat"]);

                            m_dtbStockCarat = objSaleInvoice.GetStockCarat(GlobalDec.gEmployeeProperty.company_id, GlobalDec.gEmployeeProperty.branch_id, GlobalDec.gEmployeeProperty.location_id, GlobalDec.gEmployeeProperty.department_id, Val.ToInt(drw["assort_id"]), Val.ToInt(drw["sieve_id"]));

                            if (m_dtbStockCarat.Rows.Count > 0)
                            {
                                p_numStockCarat = Val.ToDecimal(m_dtbStockCarat.Rows[0]["stock_carat"]);
                            }

                            if ((p_numStockCarat + p_numOldCarat + p_numOldRejCarat) < (Val.ToDecimal(drw["carat"]) + Val.ToDecimal(drw["rej_carat"])))
                            {
                                Global.Message("(Asssort No : " + drw["assort_name"] + " " + "Sieve :  " + drw["sieve_name"] + ") Please check enter carat more then stock carat", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return false;
                            }
                        }
                    }
                }

                if (m_blnadd)
                {
                    if (lueAssortName.Text == "")
                    {
                        lstError.Add(new ListError(13, "Assort"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueAssortName.Focus();
                        }
                    }
                    if (lueSieveName.Text == "")
                    {
                        lstError.Add(new ListError(13, "Sieve"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueSieveName.Focus();
                        }
                    }
                    if (lueSubSieveName.Text == "")
                    {
                        lstError.Add(new ListError(13, "Sub Sieve"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueSubSieveName.Focus();
                        }
                    }

                    if (Val.ToDouble(txtCarat.Text) == 0)
                    {
                        lstError.Add(new ListError(12, "Carat"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtCarat.Focus();
                        }
                    }

                    if (Val.ToDouble(txtRate.Text) == 0)
                    {
                        lstError.Add(new ListError(12, "Rate"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtRate.Focus();
                        }
                    }

                    if (Val.ToDouble(txtAmount.Text) == 0)
                    {
                        lstError.Add(new ListError(12, "Amount"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtAmount.Focus();
                        }
                    }

                    //DataTable m_dtbStockCarat = new DataTable();
                    //decimal p_numStockCarat = 0;

                    //m_dtbStockCarat = objSaleInvoice.GetStockCarat(GlobalDec.gEmployeeProperty.company_id, GlobalDec.gEmployeeProperty.branch_id, GlobalDec.gEmployeeProperty.location_id, GlobalDec.gEmployeeProperty.department_id, Val.ToInt(lueAssortName.EditValue), Val.ToInt(lueSieveName.EditValue));

                    //if (m_dtbStockCarat.Rows.Count > 0)
                    //{
                    //    p_numStockCarat = Val.ToDecimal(m_dtbStockCarat.Rows[0]["stock_carat"]);
                    //}

                    //if (p_numStockCarat < (Val.ToDecimal(txtCarat.Text) + Val.ToDecimal(txtRejCarat.Text)))
                    //{
                    //    Global.Message("(Asssort No : " + lueAssortName.Text + " " + "Sieve :  " + lueSieveName.Text + ") Please check enter carat more then stock carat", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    return false;
                    //}
                }
            }
            catch (Exception ex)
            {
                lstError.Add(new ListError(ex));
            }
            return (!(BLL.General.ShowErrors(lstError)));
        }
        private bool ClearDetails()
        {
            bool blnReturn = true;
            try
            {
                if (!GenerateSaleInvoiceDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }

                lblMode.Tag = null;
                lueBilledToParty.EditValue = System.DBNull.Value;
                lueShippedToParty.EditValue = System.DBNull.Value;
                lueReferance.EditValue = System.DBNull.Value;
                lueBroker.EditValue = System.DBNull.Value;
                lueSeller.EditValue = System.DBNull.Value;
                txtInvoiceNo.Text = string.Empty;
                txtTermDays.Text = string.Empty;
                txtFinalTermDays.Text = string.Empty;
                txtAddOnDays.Text = string.Empty;
                lueDemandNo.EditValue = System.DBNull.Value;
                lueMemoNo.EditValue = System.DBNull.Value;
                txtSearchInvoice.Text = string.Empty;
                lueBillToParty.EditValue = System.DBNull.Value;
                dtpInvoiceDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpInvoiceDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpInvoiceDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpInvoiceDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpInvoiceDate.EditValue = DateTime.Now;

                dtpDueDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpDueDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpDueDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpDueDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpDueDate.EditValue = DateTime.Now;

                dtpFinalDueDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpFinalDueDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpFinalDueDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpFinalDueDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpFinalDueDate.EditValue = DateTime.Now;

                lueAssortName.EditValue = System.DBNull.Value;
                lueSieveName.EditValue = System.DBNull.Value;
                lueSubSieveName.EditValue = System.DBNull.Value;
                txtPcs.Text = string.Empty;
                txtCarat.Text = string.Empty;
                txtRate.Text = string.Empty;
                txtAmount.Text = string.Empty;
                txtBrokerageAmt.Text = string.Empty;
                txtDiscountPer.Text = string.Empty;
                txtDiscountAmt.Text = string.Empty;
                txtInterestPer.Text = string.Empty;
                txtInterestAmt.Text = string.Empty;
                txtEntry.Text = string.Empty;
                txtJKK.Text = string.Empty;
                txtCod.Text = string.Empty;
                txtAccountRemark.Text = string.Empty;
                txtSaleRemark.Text = string.Empty;
                txtShippingCharge.Text = string.Empty;
                txtNetAmount.Text = string.Empty;
                txtCGSTPer.Text = string.Empty;
                txtCGSTAmt.Text = string.Empty;
                txtSGSTPer.Text = string.Empty;
                txtSGSTAmt.Text = string.Empty;
                txtIGSTPer.Text = string.Empty;
                txtIGSTAmt.Text = string.Empty;
                lblStkCrt.Text = "";
                lblMemoCrt.Text = "";
                lblBalCrt.Text = "";
                lueDeliveryType.EditValue = System.DBNull.Value;
                lueDeliveryType.EditValue = Val.ToInt32(GlobalDec.gEmployeeProperty.delivery_type_id);
                lueCurrency.EditValue = Val.ToString(GlobalDec.gEmployeeProperty.currency_id);
                btnAdd.Text = "&Add";
                txtInvoiceNo.Focus();
                btnBrowse.Enabled = true;
                lueCurrency.ReadOnly = false;
                txtRejCarat.Text = string.Empty;
                txtRejPer.Text = string.Empty;
                m_srno = 0;
                m_IsValid = false;

                m_dtbMemoNo = objSaleInvoice.GetMemoNo();

                lueMemoNo.Properties.DataSource = m_dtbMemoNo;
                lueMemoNo.Properties.ValueMember = "memo_master_id";
                lueMemoNo.Properties.DisplayMember = "memo_no";
                lblOldCarat.Text = string.Empty;

                txtPurchaseRate.Text = string.Empty;
                txtPurchaseAmount.Text = string.Empty;
                m_IsUpdate = true;
                lblMode.Text = "Add Mode";
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
        }
        private bool GenerateSaleInvoiceDetails()
        {
            bool blnReturn = true;
            try
            {
                if (m_dtbSaleDetails.Rows.Count > 0)
                    m_dtbSaleDetails.Rows.Clear();

                m_dtbSaleDetails = new DataTable();

                m_dtbSaleDetails.Columns.Add("sr_no", typeof(int));
                m_dtbSaleDetails.Columns.Add("invoice_detail_id", typeof(int));
                m_dtbSaleDetails.Columns.Add("invoice_id", typeof(int));
                m_dtbSaleDetails.Columns.Add("assort_id", typeof(int));
                m_dtbSaleDetails.Columns.Add("assort_name", typeof(string));
                m_dtbSaleDetails.Columns.Add("sieve_id", typeof(int));
                m_dtbSaleDetails.Columns.Add("sieve_name", typeof(string));
                m_dtbSaleDetails.Columns.Add("sub_sieve_id", typeof(int));
                m_dtbSaleDetails.Columns.Add("sub_sieve_name", typeof(string));
                m_dtbSaleDetails.Columns.Add("pcs", typeof(int)).DefaultValue = 0;
                m_dtbSaleDetails.Columns.Add("carat", typeof(decimal)).DefaultValue = 0;
                m_dtbSaleDetails.Columns.Add("rate", typeof(decimal)).DefaultValue = 0;
                m_dtbSaleDetails.Columns.Add("amount", typeof(decimal)).DefaultValue = 0;
                m_dtbSaleDetails.Columns.Add("discount", typeof(decimal)).DefaultValue = 0;
                m_dtbSaleDetails.Columns.Add("remarks", typeof(string));
                m_dtbSaleDetails.Columns.Add("old_pcs", typeof(int)).DefaultValue = 0;
                m_dtbSaleDetails.Columns.Add("old_carat", typeof(decimal));
                m_dtbSaleDetails.Columns.Add("flag", typeof(int)).DefaultValue = 0;
                m_dtbSaleDetails.Columns.Add("old_assort_id", typeof(int));
                m_dtbSaleDetails.Columns.Add("old_sieve_id", typeof(int));
                m_dtbSaleDetails.Columns.Add("old_sub_sieve_id", typeof(int));
                m_dtbSaleDetails.Columns.Add("old_assort_name", typeof(string));
                m_dtbSaleDetails.Columns.Add("old_sieve_name", typeof(string));
                m_dtbSaleDetails.Columns.Add("old_sub_sieve_name", typeof(string));
                m_dtbSaleDetails.Columns.Add("current_rate", typeof(decimal)).DefaultValue = 0;
                m_dtbSaleDetails.Columns.Add("current_amount", typeof(decimal)).DefaultValue = 0;

                m_dtbSaleDetails.Columns.Add("rej_carat", typeof(decimal)).DefaultValue = 0;
                m_dtbSaleDetails.Columns.Add("rej_percentage", typeof(decimal)).DefaultValue = 0;
                m_dtbSaleDetails.Columns.Add("old_rej_carat", typeof(decimal)).DefaultValue = 0;
                m_dtbSaleDetails.Columns.Add("old_rej_percentage", typeof(decimal)).DefaultValue = 0;
                m_dtbSaleDetails.Columns.Add("loss_carat", typeof(decimal)).DefaultValue = 0;
                m_dtbSaleDetails.Columns.Add("old_loss_carat", typeof(decimal)).DefaultValue = 0;
                m_dtbSaleDetails.Columns.Add("is_memo", typeof(int)).DefaultValue = 0;

                m_dtbSaleDetails.Columns.Add("purchase_rate", typeof(decimal)).DefaultValue = 0;
                m_dtbSaleDetails.Columns.Add("purchase_amount", typeof(decimal)).DefaultValue = 0;

                grdSalesDetails.DataSource = m_dtbSaleDetails;
                grdSalesDetails.Refresh();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
        }
        private void GetCarat()
        {
            try
            {
                if (Val.ToString(lueAssortName.EditValue) != "" && Val.ToString(lueSieveName.EditValue) != "")
                {
                    m_dtbStockCarat = objSaleInvoice.GetStockCarat(GlobalDec.gEmployeeProperty.company_id, GlobalDec.gEmployeeProperty.branch_id, GlobalDec.gEmployeeProperty.location_id, GlobalDec.gEmployeeProperty.department_id, Val.ToInt(lueAssortName.EditValue), Val.ToInt(lueSieveName.EditValue));
                    if (m_dtbStockCarat.Rows.Count > 0)
                    {
                        lblStkCrt.Text = Val.ToString(m_dtbStockCarat.Rows[0]["stock_carat"]);
                        lblMemoCrt.Text = Val.ToString(m_dtbStockCarat.Rows[0]["memo_carat"]);
                        lblBalCrt.Text = Val.ToString(m_dtbStockCarat.Rows[0]["stock_carat"]); //Val.ToString(Val.ToDecimal(m_dtbStockCarat.Rows[0]["stock_carat"]) - Val.ToDecimal(m_dtbStockCarat.Rows[0]["memo_carat"]));
                    }
                    else
                    {
                        //m_dtbStockCarat = null;
                        lblStkCrt.Text = "";
                        lblMemoCrt.Text = "";
                        lblBalCrt.Text = "";
                    }
                }
                else
                {
                    m_dtbStockCarat = null;
                    lblStkCrt.Text = "";
                    lblMemoCrt.Text = "";
                    lblBalCrt.Text = "";

                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                //blnReturn = false;
            }
        }
        private bool PopulateDetails()
        {
            objSaleInvoice = new SaleInvoice();
            bool blnReturn = true;
            DateTime datFromDate = DateTime.MinValue;
            DateTime datToDate = DateTime.MinValue;
            try
            {
                m_dtbDetails = objSaleInvoice.GetData(Val.DBDate(dtpFromDate.Text), Val.DBDate(dtpToDate.Text), Val.ToString(txtSearchInvoice.Text), Val.ToInt32(lueBillToParty.EditValue));

                if (m_dtbDetails.Rows.Count == 0)
                {
                    Global.Message("Data Not Found");
                    blnReturn = false;
                }

                grdSaleInvoice.DataSource = m_dtbDetails;
                dgvSaleInvoice.BestFitColumns();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            finally
            {
                objSaleInvoice = null;
            }

            return blnReturn;
        }
        private void FillMemoToSale()
        {
            try
            {
                objSaleInvoice = new SaleInvoice();
                if (m_dtbMemoData.Rows.Count > 0)
                {
                    DataTable SaleDet = new DataTable();

                    SaleDet.Columns.Add("invoice_detail_id");
                    SaleDet.Columns.Add("assort_id");
                    SaleDet.Columns.Add("assort_name");
                    SaleDet.Columns.Add("sieve_id");
                    SaleDet.Columns.Add("sieve_name");
                    SaleDet.Columns.Add("sub_sieve_id");
                    SaleDet.Columns.Add("sub_sieve_name");
                    SaleDet.Columns.Add("pcs");
                    SaleDet.Columns.Add("carat");
                    SaleDet.Columns.Add("rate");
                    SaleDet.Columns.Add("amount");
                    SaleDet.Columns.Add("old_carat");
                    SaleDet.Columns.Add("old_pcs");
                    SaleDet.Columns.Add("flag");
                    SaleDet.Columns.Add("sr_no");
                    SaleDet.Columns.Add("old_assort_id");
                    SaleDet.Columns.Add("old_sieve_id");
                    SaleDet.Columns.Add("old_sub_sieve_id");
                    SaleDet.Columns.Add("current_rate");
                    SaleDet.Columns.Add("current_amount");
                    SaleDet.Columns.Add("discount");

                    SaleDet.Columns.Add("rej_pcs");
                    SaleDet.Columns.Add("rej_carat");

                    SaleDet.Columns.Add("old_rej_carat");
                    SaleDet.Columns.Add("rej_percentage");
                    SaleDet.Columns.Add("old_rej_percentage");

                    SaleDet.Columns.Add("loss_carat");
                    SaleDet.Columns.Add("old_loss_carat");
                    SaleDet.Columns.Add("is_memo");

                    SaleDet.Columns.Add("purchase_rate");
                    SaleDet.Columns.Add("purchase_amount");

                    SaleDet.Columns.Add("broker_per");
                    SaleDet.Columns.Add("broker_amt");


                    //SaleDet.Columns.Add("demand_master_id");
                    //SaleDet.Columns.Add("memo_master_id");

                    lueBilledToParty.EditValue = Convert.ToInt32(m_dtbMemoData.Rows[0]["party_id"]);
                    lueShippedToParty.EditValue = Convert.ToInt32(m_dtbMemoData.Rows[0]["party_id"]);
                    lueBroker.EditValue = Convert.ToInt32(m_dtbMemoData.Rows[0]["broker_id"]);
                    lueMemoNo.Text = Convert.ToString(m_dtbMemoData.Rows[0]["memo_no"]);
                    txtInvoiceNo.Text = Convert.ToString(m_dtbMemoData.Rows[0]["memo_no"]);
                    txtDiscountPer.Text = Convert.ToString(m_dtbMemoData.Rows[0]["discount_per"]);
                    txtDiscountAmt.Text = Convert.ToString(m_dtbMemoData.Rows[0]["discount_amt"]);
                    txtTermDays.Text = Convert.ToString(m_dtbMemoData.Rows[0]["term_days"]);
                    dtpDueDate.Text = Val.DBDate(Val.ToString(m_dtbMemoData.Rows[0]["due_date"]));

                    txtFinalTermDays.Text = Convert.ToString(m_dtbMemoData.Rows[0]["final_term_days"]);
                    dtpFinalDueDate.Text = Val.DBDate(Val.ToString(m_dtbMemoData.Rows[0]["final_due_date"]));

                    txtExchangeRate.Text = Val.ToString(m_dtbMemoData.Rows[0]["exchange_rate"]);
                    lueCurrency.Text = Val.ToString(m_dtbMemoData.Rows[0]["currency_type"]);
                    lueSeller.EditValue = Val.ToInt(m_dtbMemoData.Rows[0]["seller_id"]);
                    lueDeliveryType.EditValue = Val.ToInt(m_dtbMemoData.Rows[0]["delivery_type_id"]);

                    //lblMemoNo.Tag = Convert.ToInt32(m_dtbMemoData.Rows[0]["memo_master_id"]);
                    lueMemoNo.EditValue = Convert.ToInt32(m_dtbMemoData.Rows[0]["memo_master_id"]);
                    lblMode.Tag = Convert.ToInt32(m_dtbMemoData.Rows[0]["invoice_id"]);
                    //txtBrokeragePer.Text = Val.ToDecimal(m_dtbMemoData.Rows[0]["broker_per"]).ToString();
                    //txtBrokerageAmt.Text = Val.ToDecimal(m_dtbMemoData.Rows[0]["broker_amt"]).ToString();

                    int i = 0;
                    foreach (DataRow DRow in m_dtbMemoData.Rows)
                    {
                        if (Convert.ToDecimal(DRow["rec_carat"]) > 0)
                        {
                            SaleDet.Rows.Add();
                            SaleDet.Rows[i]["invoice_detail_id"] = Val.ToInt(DRow["invoice_detail_id"]);
                            SaleDet.Rows[i]["assort_id"] = Val.ToInt(DRow["assort_id"]);
                            SaleDet.Rows[i]["sieve_id"] = Val.ToInt(DRow["sieve_id"]);
                            SaleDet.Rows[i]["sub_sieve_id"] = Val.ToInt(DRow["sub_sieve_id"]);
                            SaleDet.Rows[i]["assort_name"] = Val.ToString(DRow["assort_name"]);
                            SaleDet.Rows[i]["sieve_name"] = Val.ToString(DRow["sieve_name"]);
                            SaleDet.Rows[i]["sub_sieve_name"] = Val.ToString(DRow["sub_sieve_name"]);
                            SaleDet.Rows[i]["pcs"] = Val.ToInt(DRow["rec_pcs"]);
                            SaleDet.Rows[i]["carat"] = Val.ToDecimal(DRow["rec_carat"]);
                            SaleDet.Rows[i]["rate"] = Val.ToDecimal(DRow["rate"]);
                            SaleDet.Rows[i]["amount"] = Val.ToDecimal(DRow["sale_amount"]);
                            SaleDet.Rows[i]["old_carat"] = 0;
                            SaleDet.Rows[i]["old_pcs"] = 0;
                            SaleDet.Rows[i]["old_rej_carat"] = 0;
                            SaleDet.Rows[i]["old_rej_percentage"] = 0;
                            SaleDet.Rows[i]["flag"] = Convert.ToInt32(DRow["invoice_id"]) == 0 ? 0 : 1;
                            SaleDet.Rows[i]["sr_no"] = i + 1;
                            SaleDet.Rows[i]["old_assort_id"] = Val.ToInt(0);
                            SaleDet.Rows[i]["old_sieve_id"] = Val.ToInt(0);
                            SaleDet.Rows[i]["old_sub_sieve_id"] = Val.ToInt(0);
                            SaleDet.Rows[i]["current_rate"] = Val.ToDecimal(DRow["current_rate"]);
                            SaleDet.Rows[i]["current_amount"] = Math.Round(Val.ToDecimal(DRow["rec_carat"]) * Val.ToDecimal(DRow["current_rate"]), 3);
                            SaleDet.Rows[i]["discount"] = 0;

                            SaleDet.Rows[i]["rej_pcs"] = 0;
                            SaleDet.Rows[i]["rej_carat"] = 0;
                            SaleDet.Rows[i]["rej_percentage"] = 0;

                            SaleDet.Rows[i]["loss_carat"] = Val.ToDecimal(DRow["loss_carat"]);
                            SaleDet.Rows[i]["old_loss_carat"] = Val.ToDecimal(0);
                            SaleDet.Rows[i]["is_memo"] = Val.ToInt(1);

                            SaleDet.Rows[i]["purchase_rate"] = Val.ToDecimal(DRow["purchase_rate"]);
                            SaleDet.Rows[i]["purchase_amount"] = Val.ToDecimal(DRow["purchase_amount"]);
                            if (Convert.ToInt32(DRow["invoice_id"]) > 0)
                            {
                                lblMode.Tag = lblMode.Tag = Convert.ToInt32(DRow["invoice_id"]);
                            }

                            i++;
                        }
                    }

                    grdSalesDetails.DataSource = SaleDet;

                    ttlbSaleInvoice.SelectedTabPage = tblSaledetail;
                    m_dtbSaleDetails = SaleDet;
                    txtInvoiceNo.Focus();

                    txtCGSTPer_EditValueChanged(null, null);
                    txtSGSTPer_EditValueChanged(null, null);
                    txtDiscountPer_EditValueChanged(null, null);
                    txtBrokeragePer_EditValueChanged(null, null);

                    txtRejCarat.Enabled = false;
                    txtRejPer.Enabled = false;

                    m_IsValid = true;
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
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
                            dgvSaleInvoice.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvSaleInvoice.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvSaleInvoice.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvSaleInvoice.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvSaleInvoice.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvSaleInvoice.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvSaleInvoice.ExportToCsv(Filepath);
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
            //Global.Export("xlsx", dgvRoughClarityMaster);
            Export("xlsx", "Export to Excel", "Excel files 97-2003 (Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*");
        }
        private void MNExportPDF_Click(object sender, EventArgs e)
        {
            // Global.Export("pdf", dgvRoughClarityMaster);
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
        private void txtPurchaseRate_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtPurchaseAmount.Text = string.Format("{0:0.00}", Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtPurchaseRate.Text));
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void txtFinalTermDays_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtpInvoiceDate.Text.Length <= 0 || dtpInvoiceDate.Text == "")
                {
                    txtFinalTermDays.Text = "";
                    dtpFinalDueDate.EditValue = null;
                }
                else
                {
                    Double Days = Val.ToDouble(txtFinalTermDays.Text);
                    DateTime Date = Convert.ToDateTime(dtpInvoiceDate.EditValue).AddDays(Val.ToDouble(Days));
                    dtpFinalDueDate.EditValue = Val.DBDate(Date.ToShortDateString());
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }

        private void dtpInvoiceDate_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtpInvoiceDate.Text.Length <= 0 || dtpInvoiceDate.Text == "")
                {
                    txtTermDays.Text = "";
                    txtFinalTermDays.Text = "";
                    dtpDueDate.EditValue = null;
                    dtpFinalDueDate.EditValue = null;
                }
                else
                {
                    Double Days = Val.ToDouble(txtTermDays.Text) + Val.ToDouble(txtAddOnDays.Text);
                    DateTime Date = Convert.ToDateTime(dtpInvoiceDate.EditValue).AddDays(Val.ToDouble(Days));
                    dtpDueDate.EditValue = Val.DBDate(Date.ToShortDateString());

                    Double Days_Terms = Val.ToDouble(txtFinalTermDays.Text);
                    DateTime Date_Terms = Convert.ToDateTime(dtpInvoiceDate.EditValue).AddDays(Val.ToDouble(Days_Terms));
                    dtpFinalDueDate.EditValue = Val.DBDate(Date_Terms.ToShortDateString());
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }

        private void dgvSaleInvoice_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (Val.ToInt(dgvSaleInvoice.GetRowCellValue(e.RowHandle, "flag_color")) == 1)
                {
                    e.Appearance.BackColor = Color.FromArgb(248, 210, 210);
                }
            }
        }
    }
}