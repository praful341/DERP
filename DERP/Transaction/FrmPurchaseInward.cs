using BLL;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Utility;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using DERP.Master;
using DevExpress.XtraEditors;
using DREP.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DREP.Transaction
{
    public partial class FrmPurchaseInward : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member
        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        Control _NextEnteredControl;
        private List<Control> _tabControls;
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        PurchaseInward objPurchase;
        UserAuthentication objUserAuthentication;
        AssortMaster objAssort;
        SieveMaster objSieve;
        RateMaster objRate;

        DataTable DtControlSettings;
        DataTable m_dtbAssorts;
        DataTable m_dtbSieve;
        DataTable m_dtbPurchaseDetails;
        DataTable m_dtbCurrency;
        DataTable m_dtbDetails;
        DataTable m_opDate;
        DataTable m_dtbCurrencyType;
        int m_purchase_detail_id;
        int m_srno;
        int m_update_srno;
        int m_numForm_id;
        int IntRes;
        int m_numCurrency_id;
        decimal m_numcarat;
        decimal m_current_rate;
        decimal m_current_amount;
        decimal m_numSummRate;

        bool m_blnadd;
        bool m_blnsave;
        bool m_blncheckevents;

        #endregion

        #region Constructor
        public FrmPurchaseInward()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            _NextEnteredControl = new Control();
            _tabControls = new List<Control>();

            objPurchase = new PurchaseInward();
            objUserAuthentication = new UserAuthentication();
            objAssort = new AssortMaster();
            objSieve = new SieveMaster();
            objRate = new RateMaster();

            DtControlSettings = new DataTable();
            m_dtbAssorts = new DataTable();
            m_dtbSieve = new DataTable();
            m_dtbPurchaseDetails = new DataTable();
            m_dtbCurrency = new DataTable();
            m_dtbDetails = new DataTable();
            m_opDate = new DataTable();
            m_dtbCurrencyType = new DataTable();
            m_purchase_detail_id = 0;
            m_srno = 0;
            m_update_srno = 0;
            m_numForm_id = 0;
            IntRes = 0;

            m_numcarat = 0;
            m_current_rate = 0;
            m_current_amount = 0;
            m_numCurrency_id = 0;

            m_blnadd = new bool();
            m_blnsave = new bool();
            m_blncheckevents = new bool();
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
            objBOFormEvents.ObjToDispose.Add(objPurchase);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }

        #endregion

        #region Events
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
        private void FrmPurchaseInward_Load(object sender, EventArgs e)
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
                    ttlbPurchase.SelectedTabPage = tblBranchdetail;
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
                lueAssortName.EditValue = DBNull.Value;
                lueSieveName.EditValue = DBNull.Value;
                lueSubSieveName.EditValue = DBNull.Value;
                txtPcs.Text = string.Empty;
                txtCarat.Text = string.Empty;
                txtRate.Text = string.Empty;
                txtAmount.Text = string.Empty;
                lueAssortName.Focus();
                lueAssortName.ShowPopup();
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            ObjPer.SetFormPer();
            if (ObjPer.AllowUpdate == false || ObjPer.AllowInsert == false)
            {
                Global.Message(BLL.GlobalDec.gStrPermissionInsUpdMsg);
                return;
            }

            List<ListError> lstError = new List<ListError>();
            Dictionary<Control, string> rtnCtrls = new Dictionary<Control, string>();
            rtnCtrls = Global.CheckCompulsoryControls(Val.ToInt(ObjPer.form_id), this);
            if (rtnCtrls.Count > 0)
            {
                foreach (KeyValuePair<Control, string> entry in rtnCtrls)
                {
                    if (entry.Key is DevExpress.XtraEditors.LookUpEdit || entry.Key is DevExpress.XtraEditors.DateEdit || entry.Key is DevExpress.XtraEditors.TextEdit)
                    {
                        lstError.Add(new ListError(13, entry.Value));
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

            DialogResult result = MessageBox.Show("Do you want to save data?", "Confirmation", MessageBoxButtons.YesNoCancel);
            if (result != DialogResult.Yes)
            {
                btnSave.Enabled = true;
                return;
            }

            DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;

            panelProgress.Visible = true;
            backgroundWorker_PurchaseInward.RunWorkerAsync();

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
                Global.Message(ex.ToString());
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
                Global.Message(ex.ToString());
                return;
            }
        }
        private void lueAssortName_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (lueAssortName.ItemIndex != -1 && lueSieveName.ItemIndex != -1)
                {
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
                if (dtpPurchaseDate.Text.Length <= 0 || dtpPurchaseDate.Text == "")
                {
                    txtTermDays.Text = "";
                    dtpDueDate.EditValue = null;
                }
                else
                {
                    Double Days = Val.ToDouble(txtTermDays.Text) + Val.ToDouble(txtAddOnDays.Text);
                    DateTime Date = Convert.ToDateTime(dtpPurchaseDate.EditValue).AddDays(Val.ToDouble(Days));
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
                if (dtpPurchaseDate.Text.Length <= 0 || dtpPurchaseDate.Text == "")
                {
                    txtAddOnDays.Text = "";
                    dtpDueDate.EditValue = null;
                }
                else
                {
                    Double Days = Val.ToDouble(txtTermDays.Text) + Val.ToDouble(txtAddOnDays.Text);
                    DateTime Date = Convert.ToDateTime(dtpPurchaseDate.EditValue).AddDays(Val.ToDouble(Days));
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
            try
            {
                Global.LOOKUPSubSieve(lueSubSieveName, Val.ToInt(lueSieveName.EditValue));
                lueAssortName_EditValueChanged(null, null);
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void txtDiscountPer_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!m_blncheckevents)
                {
                    decimal Dis_amt = Math.Round(Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) * Val.ToDecimal(txtDiscountPer.Text) / 100, 0);
                    txtDiscountAmt.Text = Dis_amt.ToString();
                    decimal Net_Amount = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) - Val.ToDecimal(txtDiscountAmt.Text)) + Val.ToDecimal(txtInterestAmt.Text) + Val.ToDecimal(txtShippingCharge.Text), 0);
                    txtNetAmount.Text = Net_Amount.ToString();
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
            try
            {
                if (Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) != 0)
                {
                    if (!m_blncheckevents)
                    {
                        decimal Dis_Per = Math.Round(Val.ToDecimal(txtDiscountAmt.Text) / Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) * 100, 3);
                        txtDiscountPer.Text = Dis_Per.ToString();
                        decimal Net_Amount = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) - Val.ToDecimal(txtDiscountAmt.Text)) + Val.ToDecimal(txtInterestAmt.Text) + Val.ToDecimal(txtShippingCharge.Text), 0);
                        txtNetAmount.Text = Net_Amount.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void txtBrokeragePer_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!m_blncheckevents)
                {
                    decimal Brokerage_amt = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) - Val.ToDecimal(txtDiscountAmt.Text)) * Val.ToDecimal(txtBrokeragePer.Text) / 100, 0);
                    txtBrokerageAmt.Text = Brokerage_amt.ToString();
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
            try
            {
                if (Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) != 0)
                {
                    if (!m_blncheckevents)
                    {
                        decimal Brokarage_Per = Math.Round(Val.ToDecimal(txtBrokerageAmt.Text) / (Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) - Val.ToDecimal(txtDiscountAmt.Text)) * 100, 3);
                        txtBrokeragePer.Text = Brokarage_Per.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void txtInterestPer_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!m_blncheckevents)
                {
                    decimal Interest_amt = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) - Val.ToDecimal(txtDiscountAmt.Text)) * Val.ToDecimal(txtInterestPer.Text) / 100, 0);
                    txtInterestAmt.Text = Interest_amt.ToString();
                    decimal Net_Amount = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) - Val.ToDecimal(txtDiscountAmt.Text)) + Val.ToDecimal(txtInterestAmt.Text) + Val.ToDecimal(txtShippingCharge.Text), 0);
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
            try
            {
                if (Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) != 0)
                {
                    if (!m_blncheckevents)
                    {
                        decimal interest_Per = Math.Round(Val.ToDecimal(txtInterestAmt.Text) / (Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) - Val.ToDecimal(txtDiscountAmt.Text)) * 100, 3);
                        txtInterestPer.Text = interest_Per.ToString();
                        decimal Net_Amount = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) - Val.ToDecimal(txtDiscountAmt.Text)) + Val.ToDecimal(txtInterestAmt.Text) + Val.ToDecimal(txtShippingCharge.Text), 0);
                        txtNetAmount.Text = Net_Amount.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void txtShippingCharge_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) != 0)
                {
                    decimal Shipping_Charge = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) + Val.ToDecimal(txtInterestAmt.Text)) - (Val.ToDecimal(txtDiscountAmt.Text)) + Val.ToDecimal(txtShippingCharge.Text), 0);
                    txtNetAmount.Text = Shipping_Charge.ToString();
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
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
        private void lueParty_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmPartyMaster frmParty = new FrmPartyMaster();
                frmParty.ShowDialog();
                Global.LOOKUPParty(lueParty);
            }
        }
        private void lueBroker_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmBrokerMaster frmBroker = new FrmBrokerMaster();
                frmBroker.ShowDialog();
                Global.LOOKUPBroker(lueBroker);
            }
        }
        private void panelControl4_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(255, 191, 219, 255), 2);
            e.Graphics.DrawLine(pen, 0, 57, 1500, 57);
        }
        private void backgroundWorker_PurchaseInward_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.Default;
                if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                {
                    Conn = new BeginTranConnection(true, false);
                }
                else
                {
                    Conn = new BeginTranConnection(false, true);
                }
                Purchase_InwardProperty objPurchase_Property = new Purchase_InwardProperty();
                PurchaseInward objPurchase = new PurchaseInward();
                try
                {
                    IntRes = 0;
                    objPurchase_Property.purchase_id = Val.ToInt(lblMode.Tag);
                    objPurchase_Property.purchase_No = Val.ToString(txtPurchaseNo.Text);
                    objPurchase_Property.company_id = Val.ToInt(GlobalDec.gEmployeeProperty.company_id);
                    objPurchase_Property.branch_id = Val.ToInt(GlobalDec.gEmployeeProperty.branch_id);
                    objPurchase_Property.location_id = Val.ToInt(GlobalDec.gEmployeeProperty.location_id);
                    objPurchase_Property.department_id = Val.ToInt(GlobalDec.gEmployeeProperty.department_id);

                    objPurchase_Property.purchase_date = Val.DBDate(dtpPurchaseDate.Text);
                    objPurchase_Property.delivery_type_id = Val.ToInt(lueDeliveryType.EditValue);
                    objPurchase_Property.remarks = Val.ToString(txtEntry.Text);
                    objPurchase_Property.delivery_type_id = Val.ToInt(lueDeliveryType.EditValue);

                    objPurchase_Property.form_id = m_numForm_id;

                    objPurchase_Property.Party_Id = Val.ToInt(lueParty.EditValue);
                    objPurchase_Property.Broker_Id = Val.ToInt(lueBroker.EditValue);
                    objPurchase_Property.Term_Days = Val.ToInt(txtTermDays.EditValue);
                    objPurchase_Property.Add_On_Days = Val.ToInt(txtAddOnDays.EditValue);

                    objPurchase_Property.Special_Remark = Val.ToString(txtJKK.Text);
                    objPurchase_Property.Client_Remark = Val.ToString(txtSaleRemark.Text);
                    objPurchase_Property.Payment_Remark = Val.ToString(txtAccountRemark.Text);

                    objPurchase_Property.total_pcs = Math.Round(Val.ToDecimal(clmPcs.SummaryItem.SummaryValue), 3);
                    objPurchase_Property.total_carat = Math.Round(Val.ToDecimal(clmDetCarat.SummaryItem.SummaryValue), 3);

                    objPurchase_Property.Gross_Amount = Math.Round(Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue), 3);

                    objPurchase_Property.Brokerage_Per = Val.ToDecimal(txtBrokeragePer.Text);
                    objPurchase_Property.Brokerage_Amt = Val.ToDecimal(txtBrokerageAmt.Text);
                    objPurchase_Property.Discount_Per = Val.ToDecimal(txtDiscountPer.Text);
                    objPurchase_Property.Discount_Amt = Val.ToDecimal(txtDiscountAmt.Text);
                    objPurchase_Property.Interest_Per = Val.ToDecimal(txtInterestPer.Text);
                    objPurchase_Property.Interest_Amt = Val.ToDecimal(txtInterestAmt.Text);
                    objPurchase_Property.Shipping_Charge = Val.ToDecimal(txtShippingCharge.Text);
                    objPurchase_Property.currency_type = lueCurrency.Text;
                    objPurchase_Property.currency_id = Val.ToInt(m_numCurrency_id);
                    objPurchase_Property.exchange_rate = Val.ToDecimal(txtExchangeRate.Text);
                    objPurchase_Property.net_amount = Val.ToDecimal(txtNetAmount.Text);

                    objPurchase_Property = objPurchase.Save(objPurchase_Property, DLL.GlobalDec.EnumTran.Start, Conn);

                    Int64 NewmPurchaseid = Val.ToInt64(objPurchase_Property.purchase_id);

                    int IntCounter = 0;
                    int Count = 0;
                    int TotalCount = m_dtbPurchaseDetails.Rows.Count;

                    foreach (DataRow drw in m_dtbPurchaseDetails.Rows)
                    {
                        objPurchase_Property = new Purchase_InwardProperty();
                        objPurchase_Property.purchase_id = Val.ToInt32(NewmPurchaseid);
                        objPurchase_Property.purchase_Detail_id = Val.ToInt(drw["purchase_detail_id"]);
                        objPurchase_Property.assort_id = Val.ToInt(drw["assort_id"]);
                        objPurchase_Property.sieve_id = Val.ToInt(drw["sieve_id"]);
                        objPurchase_Property.sub_sieve_id = Val.ToInt(drw["sub_sieve_id"]);
                        objPurchase_Property.pcs = Val.ToInt(drw["pcs"]);
                        objPurchase_Property.carat = Val.ToDecimal(drw["carat"]);
                        objPurchase_Property.rate = Val.ToDecimal(drw["rate"]);
                        objPurchase_Property.amount = Val.ToDecimal(drw["amount"]);
                        objPurchase_Property.discount = Val.ToDecimal(drw["discount"]);

                        objPurchase_Property.old_carat = Val.ToDecimal(drw["old_carat"]);
                        objPurchase_Property.old_pcs = Val.ToInt(drw["old_pcs"]);
                        objPurchase_Property.flag = Val.ToInt(drw["flag"]);
                        objPurchase_Property.old_assort_id = Val.ToInt(drw["old_assort_id"]);
                        objPurchase_Property.old_sieve_id = Val.ToInt(drw["old_sieve_id"]);
                        objPurchase_Property.old_sub_sieve_id = Val.ToInt(drw["old_sub_sieve_id"]);
                        objPurchase_Property.current_rate = Val.ToDecimal(drw["current_rate"]);
                        objPurchase_Property.current_amount = Val.ToDecimal(drw["current_amount"]);

                        IntRes = objPurchase.Save_Detail(objPurchase_Property, DLL.GlobalDec.EnumTran.Continue, Conn);

                        Count++;
                        IntCounter++;
                        IntRes++;
                        SetControlPropertyValue(lblProgressCount, "Text", Count.ToString() + "" + "/" + "" + TotalCount.ToString() + " Completed....");
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
                    objPurchase_Property = null;
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
        private void backgroundWorker_PurchaseInward_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                panelProgress.Visible = false;
                if (IntRes > 0)
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Purchase Data Save Successfully");
                        ClearDetails();
                        PopulateDetails();
                    }
                    else
                    {
                        Global.Confirm("Purchase Data Update Successfully");
                        ClearDetails();
                        PopulateDetails();
                    }
                }
                else
                {
                    Global.Confirm("Error In Purchase");
                    txtPurchaseNo.Focus();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }

        #endregion

        #region "Grid Events" 
        private void dgvPurchaseDetails_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                if (Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) > 0 && Val.ToDecimal(clmDetCarat.SummaryItem.SummaryValue) > 0)
                {
                    m_numSummRate = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) / Val.ToDecimal(clmDetCarat.SummaryItem.SummaryValue)), 2, MidpointRounding.AwayFromZero);

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
        private void dgvPurchase_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                objPurchase = new PurchaseInward();
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        m_blncheckevents = true;

                        DataRow Drow = dgvPurchase.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt32(Drow["purchase_id"]);

                        dtpPurchaseDate.Text = Val.DBDate(Val.ToString(Drow["purchase_date"]));
                        lueDeliveryType.EditValue = Val.ToInt(Drow["delivery_type_id"]);
                        txtPurchaseNo.Text = Val.ToString(Drow["purchase_no"]);
                        lueParty.EditValue = Val.ToInt(Drow["party_id"]);
                        lueBroker.EditValue = Val.ToInt(Drow["broker_id"]);
                        txtShippingCharge.Text = Val.ToString(Drow["shipping"]);
                        txtTermDays.Text = Val.ToString(Drow["term_days"]);
                        txtAddOnDays.Text = Val.ToString(Drow["add_on_days"]);
                        txtEntry.Text = Val.ToString(Drow["remarks"]);
                        txtJKK.Text = Val.ToString(Drow["special_remarks"]);
                        txtAccountRemark.Text = Val.ToString(Drow["client_remarks"]);
                        txtSaleRemark.Text = Val.ToString(Drow["payment_remarks"]);
                        txtBrokeragePer.Text = Val.ToString(Drow["brokerage_per"]);
                        txtBrokerageAmt.Text = Val.ToString(Drow["brokerage_amount"]);
                        txtDiscountPer.Text = Val.ToString(Drow["discount_per"]);
                        txtDiscountAmt.Text = Val.ToString(Drow["discount_amount"]);
                        txtInterestPer.Text = Val.ToString(Drow["interest_per"]);
                        txtInterestAmt.Text = Val.ToString(Drow["interest_amount"]);
                        txtNetAmount.Text = Val.ToString(Drow["net_amount"]);

                        lueCurrency.Text = Val.ToString(Drow["currency"]);
                        txtExchangeRate.EditValue = Val.ToDecimal(Drow["exchange_rate"]);

                        m_dtbPurchaseDetails = objPurchase.GetDataDetails(Val.ToInt(lblMode.Tag));

                        grdPurchaseDetails.DataSource = m_dtbPurchaseDetails;

                        ttlbPurchase.SelectedTabPage = tblBranchdetail;
                        txtPurchaseNo.Focus();

                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void dgvPurchaseDetails_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvPurchaseDetails.GetDataRow(e.RowHandle);
                        btnAdd.Text = "&Update";
                        lueSieveName.Text = Val.ToString(Drow["sieve_name"]);
                        lueSieveName.Tag = Val.ToInt64(Drow["sieve_id"]);
                        lueSubSieveName.Text = Val.ToString(Drow["sub_sieve_name"]);
                        lueSubSieveName.Tag = Val.ToInt64(Drow["sub_sieve_id"]);
                        lueAssortName.Text = Val.ToString(Drow["assort_name"]);
                        lueAssortName.Tag = Val.ToInt64(Drow["assort_id"]);
                        txtPcs.Text = Val.ToString(Drow["pcs"]);
                        txtCarat.Text = Val.ToString(Drow["carat"]);
                        txtRate.Text = Val.ToString(Drow["rate"]);
                        txtAmount.Text = Val.ToString(Drow["amount"]);
                        m_numcarat = Val.ToDecimal(Drow["carat"]);
                        m_purchase_detail_id = Val.ToInt(Drow["purchase_detail_id"]);
                        m_update_srno = Val.ToInt(Drow["sr_no"]);
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        #endregion

        #region Functions
        private bool LoadDefaults()
        {
            bool blnReturn = true;
            try
            {
                Global.LOOKUPParty(lueParty);
                Global.LOOKUPBroker(lueBroker);
                Global.LOOKUPDeliveryType(lueDeliveryType);

                m_dtbCurrencyType = Global.CurrencyType();
                lueCurrency.Properties.DataSource = m_dtbCurrencyType;
                lueCurrency.Properties.ValueMember = "currency_id";
                lueCurrency.Properties.DisplayMember = "currency";

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

                dtpPurchaseDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpPurchaseDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpPurchaseDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpPurchaseDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpPurchaseDate.EditValue = DateTime.Now;
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

                if (btnAdd.Text == "&Add")
                {

                    //DataTable p_dtbStockCarat = new DataTable();
                    //p_dtbStockCarat = objPurchase.GetStockCarat(GlobalDec.gEmployeeProperty.company_id, GlobalDec.gEmployeeProperty.branch_id, GlobalDec.gEmployeeProperty.location_id, GlobalDec.gEmployeeProperty.department_id, Val.ToInt(lueAssortName.EditValue), Val.ToInt(lueSieveName.EditValue));

                    //if (p_dtbStockCarat.Rows.Count > 0)
                    //{
                    //    numStockCarat = Val.ToDecimal(p_dtbStockCarat.Rows[0]["stock_carat"]);
                    //}

                    //if (numStockCarat < Val.ToDecimal(txtCarat.Text))
                    //{
                    //    Global.Message("Please check enter carat more then stock carat", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    txtCarat.Focus();
                    //    blnReturn = false;
                    //    return blnReturn;ss
                    //}

                    DataRow[] dr = m_dtbPurchaseDetails.Select("sieve_id = " + Val.ToInt(lueSieveName.EditValue) + " AND assort_id = " + Val.ToInt(lueAssortName.EditValue) + " AND sub_sieve_id = " + Val.ToInt(lueSubSieveName.EditValue));
                    if (dr.Count() == 1)
                    {
                        Global.Message("Record already exists in grid", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lueAssortName.Focus();
                        blnReturn = false;
                        return blnReturn;
                    }

                    DataRow drwNew = m_dtbPurchaseDetails.NewRow();
                    decimal numCarat = Val.ToDecimal(txtCarat.Text);
                    decimal numRate = Val.ToDecimal(txtRate.Text);
                    decimal numAmount = Val.ToDecimal(txtAmount.Text);
                    int numPcs = Val.ToInt(txtPcs.Text);

                    drwNew["purchase_id"] = Val.ToInt(0);
                    drwNew["purchase_detail_id"] = Val.ToInt(0);

                    drwNew["assort_id"] = Val.ToInt(lueAssortName.EditValue);
                    drwNew["assort_name"] = Val.ToString(lueAssortName.Text);

                    drwNew["sieve_id"] = Val.ToInt(lueSieveName.EditValue);
                    drwNew["sieve_name"] = Val.ToString(lueSieveName.Text);

                    drwNew["sub_sieve_id"] = Val.ToInt(lueSubSieveName.EditValue);
                    drwNew["sub_sieve_name"] = Val.ToString(lueSubSieveName.Text);

                    drwNew["pcs"] = numPcs;
                    drwNew["carat"] = numCarat;
                    drwNew["rate"] = Val.ToDecimal(txtRate.Text);
                    drwNew["amount"] = Math.Round(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text), 3);
                    drwNew["old_carat"] = Val.ToDecimal(0);
                    drwNew["old_pcs"] = Val.ToDecimal(0);
                    drwNew["flag"] = Val.ToInt(0);
                    m_srno = m_srno + 1;
                    drwNew["sr_no"] = Val.ToInt(m_srno);

                    drwNew["current_rate"] = m_current_rate;
                    drwNew["current_amount"] = m_current_amount;

                    m_dtbPurchaseDetails.Rows.Add(drwNew);

                    dgvPurchaseDetails.MoveLast();

                    //DataView dv = m_dtbPurchaseDetails.DefaultView;
                    //dv.Sort = "sr_no desc";
                    //DataTable sortedDT = dv.ToTable();

                    decimal Shipping_Charge = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) + Val.ToDecimal(txtInterestAmt.Text)) - (Val.ToDecimal(txtDiscountAmt.Text)) + Val.ToDecimal(txtShippingCharge.Text), 0);
                    txtNetAmount.Text = Shipping_Charge.ToString();
                }
                else if (btnAdd.Text == "&Update")
                {
                    objPurchase = new PurchaseInward();
                    //if (Val.ToDecimal(txtCarat.Text) > m_numcarat)
                    //{
                    //    if (m_purchase_detail_id == 0)
                    //    {
                    //        DataTable p_dtbStockCarat = new DataTable();
                    //        p_dtbStockCarat = objPurchase.GetStockCarat(GlobalDec.gEmployeeProperty.company_id, GlobalDec.gEmployeeProperty.branch_id, GlobalDec.gEmployeeProperty.location_id, GlobalDec.gEmployeeProperty.department_id, Val.ToInt(lueAssortName.EditValue), Val.ToInt(lueSieveName.EditValue));

                    //        if (p_dtbStockCarat.Rows.Count > 0)
                    //        {
                    //            numStockCarat = Val.ToDecimal(p_dtbStockCarat.Rows[0]["stock_carat"]);
                    //        }

                    //        if (numStockCarat < Val.ToDecimal(txtCarat.Text))
                    //        {
                    //            Global.Message("Please check enter carat more then stock carat", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //            txtCarat.Focus();
                    //            blnReturn = false;
                    //            return blnReturn;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        DataTable p_dtbStockCarat = new DataTable();
                    //        p_dtbStockCarat = objPurchase.GetStockCarat(GlobalDec.gEmployeeProperty.company_id, GlobalDec.gEmployeeProperty.branch_id, GlobalDec.gEmployeeProperty.location_id, GlobalDec.gEmployeeProperty.department_id, Val.ToInt(lueAssortName.EditValue), Val.ToInt(lueSieveName.EditValue));

                    //        decimal p_numdiff_Carat = Val.ToDecimal(txtCarat.Text) - m_numcarat;

                    //        if (p_dtbStockCarat.Rows.Count > 0)
                    //        {
                    //            numStockCarat = Val.ToDecimal(p_dtbStockCarat.Rows[0]["stock_carat"]);
                    //        }

                    //        if (numStockCarat < Val.ToDecimal(p_numdiff_Carat))
                    //        {
                    //            Global.Message("Please check enter carat more then stock carat", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //            txtCarat.Focus();
                    //            blnReturn = false;
                    //            return blnReturn;
                    //        }
                    //    }
                    //}

                    if (m_dtbPurchaseDetails.Select("assort_id ='" + Val.ToInt(lueAssortName.EditValue) + "' AND sieve_id ='" + Val.ToInt(lueSieveName.EditValue) + "'").Length > 0)
                    {
                        for (int i = 0; i < m_dtbPurchaseDetails.Rows.Count; i++)
                        {
                            if (m_dtbPurchaseDetails.Select("purchase_detail_id ='" + m_purchase_detail_id + "' AND sr_no = '" + m_update_srno + "'").Length > 0)
                            {
                                if (m_dtbPurchaseDetails.Rows[m_update_srno - 1]["purchase_detail_id"].ToString() == m_purchase_detail_id.ToString())
                                {
                                    m_dtbPurchaseDetails.Rows[m_update_srno - 1]["carat"] = Val.ToDecimal(txtCarat.Text).ToString();
                                    m_dtbPurchaseDetails.Rows[m_update_srno - 1]["pcs"] = Val.ToInt(txtPcs.Text);
                                    m_dtbPurchaseDetails.Rows[m_update_srno - 1]["rate"] = Val.ToDecimal(txtRate.Text);
                                    m_dtbPurchaseDetails.Rows[m_update_srno - 1]["flag"] = 1;
                                    m_dtbPurchaseDetails.Rows[m_update_srno - 1]["amount"] = Math.Round(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text), 3);
                                    m_dtbPurchaseDetails.Rows[m_update_srno - 1]["current_rate"] = m_current_rate;
                                    m_dtbPurchaseDetails.Rows[m_update_srno - 1]["current_amount"] = m_current_amount;
                                    decimal Shipping_Charge = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) + Val.ToDecimal(txtInterestAmt.Text)) - (Val.ToDecimal(txtDiscountAmt.Text)) + Val.ToDecimal(txtShippingCharge.Text), 0);
                                    txtNetAmount.Text = Shipping_Charge.ToString();
                                    break;
                                }
                            }
                        }
                        btnAdd.Text = "&Add";
                    }
                    else
                    {
                        for (int i = 0; i < m_dtbPurchaseDetails.Rows.Count; i++)
                        {
                            if (m_dtbPurchaseDetails.Select("purchase_detail_id ='" + m_purchase_detail_id + "' AND sr_no = '" + m_update_srno + "'").Length > 0)
                            {
                                if (m_dtbPurchaseDetails.Rows[m_update_srno - 1]["purchase_detail_id"].ToString() == m_purchase_detail_id.ToString())
                                {
                                    m_dtbPurchaseDetails.Rows[m_update_srno - 1]["carat"] = Val.ToDecimal(txtCarat.Text).ToString();
                                    m_dtbPurchaseDetails.Rows[m_update_srno - 1]["pcs"] = Val.ToInt(txtPcs.Text);
                                    m_dtbPurchaseDetails.Rows[m_update_srno - 1]["rate"] = Val.ToDecimal(txtRate.Text);
                                    m_dtbPurchaseDetails.Rows[m_update_srno - 1]["flag"] = 1;
                                    m_dtbPurchaseDetails.Rows[m_update_srno - 1]["assort_id"] = Val.ToInt(lueAssortName.EditValue);
                                    m_dtbPurchaseDetails.Rows[m_update_srno - 1]["sieve_id"] = Val.ToInt(lueSieveName.EditValue);
                                    m_dtbPurchaseDetails.Rows[m_update_srno - 1]["sub_sieve_id"] = Val.ToInt(lueSubSieveName.EditValue);
                                    m_dtbPurchaseDetails.Rows[m_update_srno - 1]["assort_name"] = Val.ToString(lueAssortName.Text);
                                    m_dtbPurchaseDetails.Rows[m_update_srno - 1]["sieve_name"] = Val.ToString(lueSieveName.Text);
                                    m_dtbPurchaseDetails.Rows[m_update_srno - 1]["sub_sieve_name"] = Val.ToString(lueSubSieveName.Text);

                                    m_dtbPurchaseDetails.Rows[m_update_srno - 1]["amount"] = Math.Round(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text), 3);
                                    m_dtbPurchaseDetails.Rows[m_update_srno - 1]["current_rate"] = m_current_rate;
                                    m_dtbPurchaseDetails.Rows[m_update_srno - 1]["current_amount"] = m_current_amount;
                                    decimal Shipping_Charge = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) + Val.ToDecimal(txtInterestAmt.Text)) - (Val.ToDecimal(txtDiscountAmt.Text)) + Val.ToDecimal(txtShippingCharge.Text), 0);
                                    txtNetAmount.Text = Shipping_Charge.ToString();
                                }
                            }
                        }
                        btnAdd.Text = "&Add";
                    }
                    dgvPurchaseDetails.MoveLast();
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
                    //if (txtPurchaseNo.Text == string.Empty)
                    //{
                    //    lstError.Add(new ListError(13, "Purchase No"));
                    //    if (!blnFocus)
                    //    {
                    //        blnFocus = true;
                    //        txtPurchaseNo.Focus();
                    //    }
                    //}

                    //if (dtpPurchaseDate.Text == string.Empty)
                    //{
                    //    lstError.Add(new ListError(13, "Purchase Date"));
                    //    if (!blnFocus)
                    //    {
                    //        blnFocus = true;
                    //        dtpPurchaseDate.Focus();
                    //    }
                    //}

                    //if (lueParty.ItemIndex < 0)
                    //{
                    //    lstError.Add(new ListError(13, "Party"));
                    //    if (!blnFocus)
                    //    {
                    //        blnFocus = true;
                    //        lueParty.Focus();
                    //    }
                    //}

                    //if (lueDeliveryType.ItemIndex < 0)
                    //{
                    //    lstError.Add(new ListError(13, "Delivery Type"));
                    //    if (!blnFocus)
                    //    {
                    //        blnFocus = true;
                    //        lueDeliveryType.Focus();
                    //    }
                    //}

                    //if (lueBroker.ItemIndex < 0)
                    //{
                    //    lstError.Add(new ListError(13, "Broker Name"));
                    //    if (!blnFocus)
                    //    {
                    //        blnFocus = true;
                    //        lueBroker.Focus();
                    //    }
                    //}
                    var result = DateTime.Compare(Convert.ToDateTime(dtpPurchaseDate.Text), DateTime.Today);
                    if (result > 0)
                    {
                        lstError.Add(new ListError(5, "Purchase Date Not Be Greater Than Today Date"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            dtpPurchaseDate.Focus();
                        }
                    }

                    if (m_dtbPurchaseDetails.Rows.Count == 0)
                    {
                        lstError.Add(new ListError(22, "Record"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
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
                if (!GeneratePurchaseDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }

                lblMode.Tag = null;
                lueParty.EditValue = System.DBNull.Value;
                lueBroker.EditValue = System.DBNull.Value;
                lueDeliveryType.EditValue = System.DBNull.Value;
                lueCurrency.EditValue = Val.ToString(GlobalDec.gEmployeeProperty.currency_id);
                lueDeliveryType.EditValue = Val.ToInt32(GlobalDec.gEmployeeProperty.delivery_type_id);
                txtPurchaseNo.Text = string.Empty;
                dtpDueDate.EditValue = string.Empty;
                txtTermDays.Text = string.Empty;
                txtAddOnDays.Text = string.Empty;

                dtpPurchaseDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpPurchaseDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpPurchaseDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpPurchaseDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpPurchaseDate.EditValue = DateTime.Now;

                m_opDate = Global.GetDate();
                dtpFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpFromDate.EditValue = Val.DBDate(m_opDate.Rows[0]["opening_date"].ToString());

                lueAssortName.EditValue = System.DBNull.Value;
                lueSieveName.EditValue = System.DBNull.Value;
                lueSubSieveName.EditValue = System.DBNull.Value;
                txtPcs.Text = string.Empty;
                txtCarat.Text = string.Empty;
                txtRate.Text = string.Empty;
                txtAmount.Text = string.Empty;
                txtBrokeragePer.Text = string.Empty;
                txtBrokerageAmt.Text = string.Empty;
                txtDiscountPer.Text = string.Empty;
                txtDiscountAmt.Text = string.Empty;
                txtInterestPer.Text = string.Empty;
                txtInterestAmt.Text = string.Empty;
                txtEntry.Text = string.Empty;
                txtJKK.Text = string.Empty;
                txtAccountRemark.Text = string.Empty;
                txtSaleRemark.Text = string.Empty;
                txtShippingCharge.Text = string.Empty;
                txtNetAmount.Text = string.Empty;
                btnAdd.Text = "&Add";
                txtPurchaseNo.Focus();
                btnSave.Enabled = true;
                m_srno = 0;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
        }
        private bool GeneratePurchaseDetails()
        {
            bool blnReturn = true;
            try
            {
                if (m_dtbPurchaseDetails.Rows.Count > 0)
                    m_dtbPurchaseDetails.Rows.Clear();

                m_dtbPurchaseDetails = new DataTable();

                m_dtbPurchaseDetails.Columns.Add("sr_no", typeof(int));
                m_dtbPurchaseDetails.Columns.Add("purchase_detail_id", typeof(int));
                m_dtbPurchaseDetails.Columns.Add("purchase_id", typeof(int));
                m_dtbPurchaseDetails.Columns.Add("assort_id", typeof(int));
                m_dtbPurchaseDetails.Columns.Add("assort_name", typeof(string));
                m_dtbPurchaseDetails.Columns.Add("sieve_id", typeof(int));
                m_dtbPurchaseDetails.Columns.Add("sieve_name", typeof(string));
                m_dtbPurchaseDetails.Columns.Add("sub_sieve_id", typeof(int));
                m_dtbPurchaseDetails.Columns.Add("sub_sieve_name", typeof(string));
                m_dtbPurchaseDetails.Columns.Add("pcs", typeof(int)).DefaultValue = 0;
                m_dtbPurchaseDetails.Columns.Add("carat", typeof(decimal)).DefaultValue = 0;
                m_dtbPurchaseDetails.Columns.Add("rate", typeof(decimal)).DefaultValue = 0;
                m_dtbPurchaseDetails.Columns.Add("amount", typeof(decimal)).DefaultValue = 0;
                m_dtbPurchaseDetails.Columns.Add("discount", typeof(decimal)).DefaultValue = 0;
                m_dtbPurchaseDetails.Columns.Add("remarks", typeof(string));
                m_dtbPurchaseDetails.Columns.Add("old_pcs", typeof(int)).DefaultValue = 0;
                m_dtbPurchaseDetails.Columns.Add("old_carat", typeof(decimal));
                m_dtbPurchaseDetails.Columns.Add("flag", typeof(int)).DefaultValue = 0;
                m_dtbPurchaseDetails.Columns.Add("old_assort_id", typeof(int));
                m_dtbPurchaseDetails.Columns.Add("old_sieve_id", typeof(int));
                m_dtbPurchaseDetails.Columns.Add("old_sub_sieve_id", typeof(int));
                m_dtbPurchaseDetails.Columns.Add("old_assort_name", typeof(string));
                m_dtbPurchaseDetails.Columns.Add("old_sieve_name", typeof(string));
                m_dtbPurchaseDetails.Columns.Add("old_sub_sieve_name", typeof(string));
                m_dtbPurchaseDetails.Columns.Add("current_rate", typeof(decimal)).DefaultValue = 0;
                m_dtbPurchaseDetails.Columns.Add("current_amount", typeof(decimal)).DefaultValue = 0;

                grdPurchaseDetails.DataSource = m_dtbPurchaseDetails;
                grdPurchaseDetails.Refresh();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
        }
        private bool PopulateDetails()
        {
            objPurchase = new PurchaseInward();
            bool blnReturn = true;
            DateTime datFromDate = DateTime.MinValue;
            DateTime datToDate = DateTime.MinValue;
            try
            {
                m_dtbDetails = objPurchase.GetData(Val.DBDate(dtpFromDate.Text), Val.DBDate(dtpToDate.Text));

                if (m_dtbDetails.Rows.Count == 0)
                {
                    Global.Message("Data Not Found");
                    blnReturn = false;
                }

                grdPurchase.DataSource = m_dtbDetails;

                //GridGroupSummaryItem item = new GridGroupSummaryItem();
                //item.FieldName = "purchase_date";
                //item.SummaryType = DevExpress.Data.SummaryItemType.Count;
                //item.ShowInGroupColumnFooter = dgvPurchase.Columns["purchase_date"];
                //dgvPurchase.GroupSummary.Add(item);
                //dgvPurchase.Columns["purchase_date"].Group();

                //GridGroupSummaryItem item1 = new GridGroupSummaryItem();
                //item1.FieldName = "party_name";
                //item1.SummaryType = DevExpress.Data.SummaryItemType.Count;
                //item1.ShowInGroupColumnFooter = dgvPurchase.Columns["party_name"];
                //dgvPurchase.GroupSummary.Add(item1);
                //dgvPurchase.Columns["party_name"].Group();

                //dgvPurchase.ExpandAllGroups();
                dgvPurchase.BestFitColumns();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            finally
            {
                objPurchase = null;
            }

            return blnReturn;
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
                            dgvPurchase.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvPurchase.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvPurchase.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvPurchase.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvPurchase.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvPurchase.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvPurchase.ExportToCsv(Filepath);
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

        private void dtpPurchaseDate_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtpPurchaseDate.Text.Length <= 0 || dtpPurchaseDate.Text == "")
                {
                    txtTermDays.Text = "";
                    txtAddOnDays.Text = "";
                    dtpDueDate.EditValue = null;
                }
                else
                {
                    Double Days = Val.ToDouble(txtTermDays.Text) + Val.ToDouble(txtAddOnDays.Text);
                    DateTime Date = Convert.ToDateTime(dtpPurchaseDate.EditValue).AddDays(Val.ToDouble(Days));
                    dtpDueDate.EditValue = Val.DBDate(Date.ToShortDateString());
                }
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
            backgroundWorker_PurchaseDelete.RunWorkerAsync();

            btnDelete.Enabled = true;
        }

        private void backgroundWorker_PurchaseDelete_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                Conn = new BeginTranConnection(true, false);
            }
            else
            {
                Conn = new BeginTranConnection(false, true);
            }

            Purchase_InwardProperty objPurchaseInwardProperty = new Purchase_InwardProperty();
            PurchaseInward objPurchaseInward = new PurchaseInward();
            try
            {
                if (Val.ToInt(lblMode.Tag) != 0)
                {
                    IntRes = 0;
                    objPurchaseInwardProperty.purchase_id = Val.ToInt(lblMode.Tag);

                    int IntCounter = 0;
                    int Count = 0;
                    int FlagCount = 1;
                    int TotalCount = m_dtbPurchaseDetails.Rows.Count;
                    Int32 Flag = 0;
                    foreach (DataRow drw in m_dtbPurchaseDetails.Rows)
                    {
                        objPurchaseInwardProperty.purchase_Detail_id = Val.ToInt(drw["purchase_detail_id"]);
                        objPurchaseInwardProperty.assort_id = Val.ToInt(drw["assort_id"]);
                        objPurchaseInwardProperty.sieve_id = Val.ToInt(drw["sieve_id"]);
                        objPurchaseInwardProperty.pcs = Val.ToInt(drw["pcs"]);
                        objPurchaseInwardProperty.carat = Val.ToDecimal(drw["carat"]);

                        if (FlagCount == TotalCount)
                        {
                            Flag = 1;
                        }

                        IntRes = objPurchaseInward.Delete(objPurchaseInwardProperty, Flag, DLL.GlobalDec.EnumTran.Continue, Conn);

                        FlagCount++;
                        Count++;
                        IntCounter++;
                        IntRes++;
                        SetControlPropertyValue(lblProgressCount, "Text", Count.ToString() + "" + "/" + "" + TotalCount.ToString() + " Completed....");
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
                else
                {
                    Global.Message("Purchase No not found");
                    if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                    {
                        Conn.Inter1.Rollback();
                    }
                    else
                    {
                        Conn.Inter2.Rollback();
                    }
                    Conn = null;
                    return;
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
                if (ex.InnerException != null)
                {
                    Global.Message(ex.InnerException.ToString());
                }
            }
            finally
            {
                objPurchaseInwardProperty = null;
            }
        }

        private void backgroundWorker_PurchaseDelete_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                panelProgress.Visible = false;
                if (IntRes > 0)
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Purchase Data Delete Successfully");
                        ClearDetails();
                    }
                    else
                    {
                        Global.Confirm("Purchase Data Delete Successfully");
                        ClearDetails();
                    }
                }
                else
                {
                    Global.Confirm("Error In Sale Invoice Delete");
                    txtPurchaseNo.Focus();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }
    }
}