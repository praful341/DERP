using BLL;
using BLL.FunctionClasses.Account;
using BLL.FunctionClasses.Rejection;
using BLL.FunctionClasses.Transaction;
using BLL.PropertyClasses.Rejection;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using DERP.Master.MFG;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DREP.Rejection
{
    public partial class FrmRoughSalePaymentEntry : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        Control _NextEnteredControl;
        private List<Control> _tabControls;
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        BLL.FormPer ObjPer = new BLL.FormPer();
        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents = new BLL.FormEvents();
        BLL.Validation Val = new BLL.Validation();
        ExpenseEntryMaster objExpenseEntry = new ExpenseEntryMaster();
        PurchaseInward objPurchase = new PurchaseInward();
        MFGRoughSalePaymentEntry objSaleEntry = new MFGRoughSalePaymentEntry();
        DataTable m_dtbSale = new DataTable();
        int m_numForm_id = 0;
        int IntRes = 0;
        DataTable m_dtbSaleType;
        DataTable DtControlSettings;
        #endregion

        #region Constructor
        public FrmRoughSalePaymentEntry()
        {
            InitializeComponent();

            DtControlSettings = new DataTable();
            m_dtbSaleType = new DataTable();
            _NextEnteredControl = new Control();
            _tabControls = new List<Control>();
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
            objBOFormEvents.ObjToDispose.Add(objExpenseEntry);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }

        #endregion

        #region Events
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            lblMode.Tag = 0;
            lblMode.Text = "Add Mode";
            txtPay.Text = null;
            lueRejectionParty.EditValue = null;
            lueSaleNo.EditValue = null;
            lblDiff.Text =
            txtRemark.Text = "";
            txtPayment.Text = "";
            txtPaymentId.Text = "0";
            DTPEntryDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPEntryDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPEntryDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPEntryDate.Properties.CharacterCasing = CharacterCasing.Upper;
            DTPEntryDate.EditValue = DateTime.Now;
            dtpSaleDate.EditValue = null;
            //m_dtbSale = objSaleEntry.GetSaleData();
            lueRejectionParty.Focus();
            //luePaymentType.EditValue = null;
            GetData();
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

            backgroundWorker_RoughSalePaymentEntry.RunWorkerAsync();

            btnSave.Enabled = true;
        }
        private void FrmRoughSalePaymentEntry_Load(object sender, EventArgs e)
        {
            Global.LOOKUPRejectionParty(lueRejectionParty);

            DataTable dtbDetail = new DataTable();

            DTPEntryDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPEntryDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPEntryDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPEntryDate.Properties.CharacterCasing = CharacterCasing.Upper;
            DTPEntryDate.EditValue = DateTime.Now;

            m_dtbSaleType = new DataTable();
            m_dtbSaleType.Columns.Add("payment_type");
            m_dtbSaleType.Rows.Add("BANK");
            m_dtbSaleType.Rows.Add("CASH");

            luePaymentType.Properties.DataSource = m_dtbSaleType;
            luePaymentType.Properties.ValueMember = "payment_type";
            luePaymentType.Properties.DisplayMember = "payment_type";
            luePaymentType.EditValue = "BANK";

            GetData();
            btnClear_Click(btnClear, null);
        }
        private void txtPending_EditValueChanged(object sender, EventArgs e)
        {
            lblDiff.Text = Val.ToString(Val.ToDecimal(txtPending.Text) + Val.ToDecimal(txtPaid.Text));

        }
        private void txtPay_EditValueChanged(object sender, EventArgs e)
        {
            lblDiff.Text = Val.ToString(Val.ToDecimal(txtPending.Text) + Val.ToDecimal(txtPaid.Text));
        }
        private void lueSaleNo_EditValueChanged(object sender, EventArgs e)
        {
            DataTable dtbFillSale = new DataTable();
            if (lueSaleNo.Text.ToString() != "" && m_dtbSale.Rows.Count > 0)
            {
                dtbFillSale = m_dtbSale.Select("sale_id =" + Val.ToInt(lueSaleNo.EditValue)).CopyToDataTable();
            }
            if (dtbFillSale.Rows.Count > 0)
            {
                dtpSaleDate.EditValue = Val.ToString(dtbFillSale.Rows[0]["sale_date"]);
                txtPayment.Text = Val.ToString(Val.ToDecimal(dtbFillSale.Rows[0]["sale_amount"]));
                txtPending.Text = Val.ToString(dtbFillSale.Rows[0]["pending_amount"]);
                txtPaid.Text = Val.ToString(dtbFillSale.Rows[0]["paid_amount"]);
            }
            else
            {
                dtpSaleDate.EditValue = null;
                txtPayment.Text = Val.ToString(Val.ToDecimal(0));
                txtPending.Text = Val.ToString(0);
                txtPaid.Text = Val.ToString(0.00);
            }
        }
        private void backgroundWorker_RoughSalePaymentEntry_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

            MFGRejectionSale_PaymentProperty RoughSalePaymentProperty = new MFGRejectionSale_PaymentProperty();
            try
            {
                if (!ValidateDetails())
                {
                    return;
                }

                Conn = new BeginTranConnection(true, false);

                RoughSalePaymentProperty.sr_no = objSaleEntry.FindNewSrNo();
                RoughSalePaymentProperty.payment_id = Val.ToInt64(txtPaymentId.Text);
                RoughSalePaymentProperty.date = Val.DBDate(DTPEntryDate.Text);
                RoughSalePaymentProperty.rejection_party_id = Val.ToInt64(lueRejectionParty.EditValue);
                RoughSalePaymentProperty.sale_id = Val.ToInt64(lueSaleNo.EditValue);
                RoughSalePaymentProperty.slip_no = Val.ToString(lueSaleNo.Text);
                RoughSalePaymentProperty.sale_date = Val.DBDate(dtpSaleDate.Text);
                RoughSalePaymentProperty.total_payment = Val.ToDecimal(txtPayment.Text);
                RoughSalePaymentProperty.total_receive = Val.ToDecimal(txtPaid.Text);
                RoughSalePaymentProperty.pending_amount = Val.ToDecimal(txtPending.Text);
                RoughSalePaymentProperty.pay_amount = Val.ToDecimal(txtPay.Text);
                RoughSalePaymentProperty.remarks = Val.ToString(txtRemark.Text);
                RoughSalePaymentProperty.payment_type = Val.ToString(luePaymentType.Text);

                IntRes = objSaleEntry.Save(RoughSalePaymentProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

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
                RoughSalePaymentProperty = null;
            }
        }
        private void backgroundWorker_RoughSalePaymentEntry_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Rough Sale Payment Entry");
                    lueRejectionParty.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Rough Sale Payment Entry Save Successfully");
                    }
                    else
                    {
                        Global.Confirm("Rough Sale Payment Entry Updated Successfully");
                    }
                    GetData();
                    btnClear_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }
        private void lueRejectionParty_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmMfgRejectionPartyMaster frmRejectionParty = new FrmMfgRejectionPartyMaster();
                frmRejectionParty.ShowDialog();
                Global.LOOKUPRejectionParty(lueRejectionParty);
            }
        }
        private void lueRejectionParty_EditValueChanged(object sender, EventArgs e)
        {
            m_dtbSale = objSaleEntry.GetSaleData();

            DataTable dtbFillData = new DataTable();
            dtbFillData = objSaleEntry.GetSaleDataEntry(Val.ToInt64(lueRejectionParty.EditValue));

            lueSaleNo.Properties.DataSource = dtbFillData;
            lueSaleNo.Properties.ValueMember = "sale_id";
            lueSaleNo.Properties.DisplayMember = "invoice_no";
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to Delete Rough Sale Payment Entry data?", "Confirmation", MessageBoxButtons.YesNoCancel);
            if (result != DialogResult.Yes)
            {
                return;
            }
            int IntRes = objSaleEntry.Delete(Val.ToInt32(lblMode.Tag));

            if (IntRes == -1)
            {
                Global.Confirm("Error In Delete Rough Sale Payment Entry Data");
                DTPEntryDate.Focus();
            }
            else
            {
                if (Val.ToInt(lblMode.Tag) == 0)
                {
                    Global.Confirm("Rough Sale Payment Entry Data Delete Successfully");
                    GetData();
                    btnClear_Click(null, null);
                }
                else
                {
                    Global.Confirm("Rough Sale Payment Entry Data Delete Successfully");
                    GetData();
                    btnClear_Click(null, null);
                }
            }
        }
        private void txtPay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as DevExpress.XtraEditors.TextEdit).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }
        private void txtPaid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as DevExpress.XtraEditors.TextEdit).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        #region Grid Events
        private void dgvoughSalePaymentEntry_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (e.Clicks == 2)
                {
                    DataRow Drow = grvRoughSalePaymentEntry.GetDataRow(e.RowHandle);
                    lblMode.Text = "Edit Mode";
                    lblMode.Tag = Val.ToInt64(Drow["payment_id"]);
                    lueRejectionParty.EditValue = Val.ToInt32(Drow["rejection_party_id"]);
                    lueSaleNo.EditValue = Val.ToInt64(Drow["sale_id"]);
                    txtPaymentId.Text = Val.ToString(Drow["payment_id"]);
                    txtRemark.Text = Val.ToString(Drow["remarks"]);
                    DTPEntryDate.EditValue = Val.DBDate(Drow["payment_date"].ToString());
                    dtpSaleDate.EditValue = Val.DBDate(Drow["sale_date"].ToString());
                    txtPay.Text = Val.ToDecimal(Drow["pay_amount"]).ToString();
                    luePaymentType.Text = Val.ToString(Drow["payment_type"]);
                }
            }
        }
        #endregion

        #endregion

        #region Validation

        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (Val.ToInt(lueRejectionParty.EditValue) <= 0)
                {
                    lstError.Add(new ListError(12, "Party"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueRejectionParty.Focus();
                    }
                }
                if (Val.ToInt(lueSaleNo.EditValue) < 0)
                {
                    lstError.Add(new ListError(12, "Sale No"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueSaleNo.Focus();
                    }
                }

                if (txtPay.Text.Length == 0 || txtPay.Text == "")
                {
                    lstError.Add(new ListError(12, "Pay"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtPayment.Focus();
                    }
                }
                if (lblMode.Text == "Add Mode")
                {
                    if (Val.ToDecimal(txtPay.Text) > Val.ToDecimal(txtPending.Text))
                    {
                        lstError.Add(new ListError(5, "Transfer Amount not greater than Pending Amount."));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtPayment.Focus();
                        }
                    }
                }
                if (DTPEntryDate.Text == string.Empty)
                {
                    lstError.Add(new ListError(13, "Payment Date"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        DTPEntryDate.Focus();
                    }
                }
                if (luePaymentType.Text.Length == 0 || luePaymentType.Text == "")
                {
                    lstError.Add(new ListError(12, "Payment Type"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        luePaymentType.Focus();
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

        #region Functions
        public void GetData()
        {
            DataTable DTab = objSaleEntry.GetPaymentList();
            grdRoughSalePaymentEntry.DataSource = DTab;
            grvRoughSalePaymentEntry.BestFitColumns();
        }

        #endregion

        #region Export Grid
        private void MNExportExcel_Click(object sender, EventArgs e)
        {
            //Global.Export("xlsx", dgvIncomeEntryMaster);
        }
        #endregion        
    }
}
