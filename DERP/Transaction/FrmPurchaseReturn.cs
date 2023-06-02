using BLL;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Utility;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using DERP.Master;
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
    public partial class FrmPurchaseReturn : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member
        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        Control _NextEnteredControl;
        private List<Control> _tabControls;
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        PurchaseReturn objPurchaseReturn = new PurchaseReturn();
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
        DataTable m_dtbAssorts = new DataTable();
        DataTable m_dtbSieve = new DataTable();
        DataTable m_dtbPurchaseReturnDetails = new DataTable();
        DataTable m_dtbCurrencyType = new DataTable();
        DataTable m_dtbDetails = new DataTable();
        DataTable m_opDate = new DataTable();
        DataTable m_dtbMemoNo = new DataTable();
        DataTable m_dtbDemandNo = new DataTable();
        DataTable m_dtbStockCarat = new DataTable();

        int m_return_detail_id;
        int m_srno;
        int m_update_srno;
        int m_numCurrency_id;
        int m_numForm_id;
        int IntRes;

        decimal m_numcarat;
        decimal m_current_rate;
        decimal m_current_amount;
        decimal m_numSummRate;
        decimal m_numSummLRate;
        bool m_blnadd;
        bool m_blnsave;
        bool m_blncheckevents;

        #endregion

        #region Constructor
        public FrmPurchaseReturn()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            _NextEnteredControl = new Control();
            _tabControls = new List<Control>();

            objPurchaseReturn = new PurchaseReturn();
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
            m_dtbAssorts = new DataTable();
            m_dtbSieve = new DataTable();
            m_dtbPurchaseReturnDetails = new DataTable();
            m_dtbCurrencyType = new DataTable();
            m_dtbDetails = new DataTable();
            m_opDate = new DataTable();
            m_dtbMemoNo = new DataTable();
            m_dtbDemandNo = new DataTable();
            m_dtbStockCarat = new DataTable();

            m_return_detail_id = 0;
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
            objBOFormEvents.ObjToDispose.Add(objPurchaseReturn);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);

        }

        #endregion

        #region Events
        private void FrmPurchaseReturn_Load(object sender, EventArgs e)
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
                    ttlbPurchaseReturn.SelectedTabPage = tblPurchaseReturndetail;
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
                txtRate.Text = string.Empty;
                txtAmount.Text = string.Empty;
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

            DialogResult result = MessageBox.Show("Do you want to save data?", "Confirmation", MessageBoxButtons.YesNoCancel);
            if (result != DialogResult.Yes)
            {
                btnSave.Enabled = true;
                return;
            }

            DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
            panelProgress.Visible = true;
            backgroundWorker_PurchaseReturn.RunWorkerAsync();

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
            try
            {
                objPurchaseReturn = new PurchaseReturn();
                GetCarat();
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
                decimal CGST_amt = Math.Round(Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) * Val.ToDecimal(txtCGSTPer.Text) / 100, 0);
                txtCGSTAmt.Text = CGST_amt.ToString();
                decimal Net_Amount = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) + Val.ToDecimal(txtCGSTAmt.Text) + Val.ToDecimal(txtSGSTAmt.Text) + Val.ToDecimal(txtIGSTAmt.Text) - Val.ToDecimal(txtDiscountAmt.Text)) + Val.ToDecimal(txtInterestAmt.Text) + Val.ToDecimal(txtShippingCharge.Text), 0);
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
                decimal SGST_amt = Math.Round(Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) * Val.ToDecimal(txtSGSTPer.Text) / 100, 0);
                txtSGSTAmt.Text = SGST_amt.ToString();
                decimal Net_Amount = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) + Val.ToDecimal(txtCGSTAmt.Text) + Val.ToDecimal(txtSGSTAmt.Text) + Val.ToDecimal(txtIGSTAmt.Text) - Val.ToDecimal(txtDiscountAmt.Text)) + Val.ToDecimal(txtInterestAmt.Text) + Val.ToDecimal(txtShippingCharge.Text), 0);
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
                decimal IGST_amt = Math.Round(Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) * Val.ToDecimal(txtIGSTPer.Text) / 100, 0);
                txtIGSTAmt.Text = IGST_amt.ToString();
                decimal Net_Amount = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) + Val.ToDecimal(txtCGSTAmt.Text) + Val.ToDecimal(txtSGSTAmt.Text) + Val.ToDecimal(txtIGSTAmt.Text) - Val.ToDecimal(txtDiscountAmt.Text)) + Val.ToDecimal(txtInterestAmt.Text) + Val.ToDecimal(txtShippingCharge.Text), 0);
                txtNetAmount.Text = Net_Amount.ToString();
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
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
                grdPurchaseReturnDetails.DataSource = null;


                if (txtFileName.Text.Length != 0)
                {
                    using (var pck = new ExcelPackage(new FileInfo(txtFileName.Text)))
                    {
                        ExcelWorksheet ws = pck.Workbook.Worksheets[1];
                        m_dtbPurchaseReturnDetails = WorksheetToDataTable(ws, true);
                    }
                }

                m_dtbSievecheck = new SieveMaster().GetData();
                m_dtbAssortscheck = new AssortMaster().GetData();
                m_dtbSubSievecheck = new SubSieveMaster().GetData();

                m_dtbPurchaseReturnDetails.Columns.Add("return_detail_id", typeof(int));
                m_dtbPurchaseReturnDetails.Columns.Add("purchase_return_id", typeof(int));
                m_dtbPurchaseReturnDetails.Columns.Add("assort_id", typeof(int));
                m_dtbPurchaseReturnDetails.Columns.Add("sieve_id", typeof(int));
                m_dtbPurchaseReturnDetails.Columns.Add("discount", typeof(decimal)).DefaultValue = 0;
                m_dtbPurchaseReturnDetails.Columns.Add("sub_sieve_id", typeof(int));
                m_dtbPurchaseReturnDetails.Columns.Add("current_rate", typeof(decimal)).DefaultValue = 0;
                m_dtbPurchaseReturnDetails.Columns.Add("current_amount", typeof(decimal)).DefaultValue = 0;
                m_dtbPurchaseReturnDetails.Columns.Add("old_carat", typeof(decimal)).DefaultValue = 0;
                m_dtbPurchaseReturnDetails.Columns.Add("old_pcs", typeof(int));
                m_dtbPurchaseReturnDetails.Columns.Add("flag", typeof(int));

                m_dtbPurchaseReturnDetails.Columns.Add("old_assort_id", typeof(int));
                m_dtbPurchaseReturnDetails.Columns.Add("old_sieve_id", typeof(int));
                m_dtbPurchaseReturnDetails.Columns.Add("old_sub_sieve_id", typeof(int));
                m_dtbPurchaseReturnDetails.Columns.Add("sr_no", typeof(int));
                m_srno = 0;

                foreach (DataRow DRow in m_dtbPurchaseReturnDetails.Rows)
                {
                    BranchTransfer objBranch = new BranchTransfer();

                    if (m_dtbPurchaseReturnDetails.Select("sieve_name ='" + Val.ToString(DRow["sieve_name"]) + "' And assort_name = '" + Val.ToString(DRow["assort_name"]) + "'").Length > 1)
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
                    m_dtbStockCarat = objPurchaseReturn.GetStockCarat(GlobalDec.gEmployeeProperty.company_id, GlobalDec.gEmployeeProperty.branch_id, GlobalDec.gEmployeeProperty.location_id, GlobalDec.gEmployeeProperty.department_id, Val.ToInt(lueAssortName.EditValue), Val.ToInt(lueSieveName.EditValue));

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

                grdPurchaseReturnDetails.DataSource = m_dtbPurchaseReturnDetails;
                dgvPurchaseReturnDetails.MoveLast();
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
                Global.Message(ex.ToString());
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
                Global.Message(ex.ToString());
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
                Global.Message(ex.ToString());
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
                Global.Message(ex.ToString());
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
        private void backgroundWorker_PurchaseReturn_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
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
                PurchaseReturn_Property objPurchaseReturnProperty = new PurchaseReturn_Property();
                PurchaseReturn objPurchaseReturn = new PurchaseReturn();
                try
                {
                    IntRes = 0;

                    objPurchaseReturnProperty.purchase_return_id = Val.ToInt(lblMode.Tag);
                    objPurchaseReturnProperty.invoice_No = Val.ToString(txtInvoiceNo.Text);
                    objPurchaseReturnProperty.company_id = Val.ToInt(GlobalDec.gEmployeeProperty.company_id);
                    objPurchaseReturnProperty.branch_id = Val.ToInt(GlobalDec.gEmployeeProperty.branch_id);
                    objPurchaseReturnProperty.location_id = Val.ToInt(GlobalDec.gEmployeeProperty.location_id);
                    objPurchaseReturnProperty.department_id = Val.ToInt(GlobalDec.gEmployeeProperty.department_id);

                    objPurchaseReturnProperty.invoice_date = Val.DBDate(dtpInvoiceDate.Text);
                    objPurchaseReturnProperty.delivery_type_id = Val.ToInt(lueDeliveryType.EditValue);
                    objPurchaseReturnProperty.remarks = Val.ToString(txtEntry.Text);

                    objPurchaseReturnProperty.form_id = m_numForm_id;

                    objPurchaseReturnProperty.Bill_To_Party_Id = Val.ToInt(lueBilledToParty.EditValue);
                    objPurchaseReturnProperty.Shipped_To_Party_Id = Val.ToInt(lueShippedToParty.EditValue);
                    objPurchaseReturnProperty.Broker_Id = Val.ToInt(lueBroker.EditValue);
                    objPurchaseReturnProperty.Term_Days = Val.ToInt(txtTermDays.EditValue);
                    objPurchaseReturnProperty.Add_On_Days = Val.ToInt(txtAddOnDays.EditValue);
                    objPurchaseReturnProperty.due_date = Val.DBDate(dtpDueDate.Text);

                    objPurchaseReturnProperty.Special_Remark = Val.ToString(txtJKK.Text);
                    objPurchaseReturnProperty.cod = Val.ToString(txtCod.Text);
                    objPurchaseReturnProperty.Client_Remark = Val.ToString(txtSaleRemark.Text);
                    objPurchaseReturnProperty.Payment_Remark = Val.ToString(txtAccountRemark.Text);

                    objPurchaseReturnProperty.total_pcs = Math.Round(Val.ToDecimal(clmPcs.SummaryItem.SummaryValue), 3);
                    objPurchaseReturnProperty.total_carat = Math.Round(Val.ToDecimal(clmDetCarat.SummaryItem.SummaryValue), 3);

                    objPurchaseReturnProperty.Gross_Amount = Math.Round(Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue), 3);

                    objPurchaseReturnProperty.cgst_rate = Val.ToDecimal(txtCGSTPer.Text);
                    objPurchaseReturnProperty.cgst_amount = Val.ToDecimal(txtCGSTAmt.Text);
                    objPurchaseReturnProperty.sgst_rate = Val.ToDecimal(txtSGSTPer.Text);
                    objPurchaseReturnProperty.sgst_amount = Val.ToDecimal(txtSGSTAmt.Text);
                    objPurchaseReturnProperty.igst_rate = Val.ToDecimal(txtIGSTPer.Text);
                    objPurchaseReturnProperty.igst_amount = Val.ToDecimal(txtIGSTAmt.Text);

                    objPurchaseReturnProperty.Brokerage_Per = Val.ToDecimal(txtBrokeragePer.Text);
                    objPurchaseReturnProperty.Brokerage_Amt = Val.ToDecimal(txtBrokerageAmt.Text);
                    objPurchaseReturnProperty.Discount_Per = Val.ToDecimal(txtDiscountPer.Text);
                    objPurchaseReturnProperty.Discount_Amt = Val.ToDecimal(txtDiscountAmt.Text);
                    objPurchaseReturnProperty.Interest_Per = Val.ToDecimal(txtInterestPer.Text);
                    objPurchaseReturnProperty.Interest_Amt = Val.ToDecimal(txtInterestAmt.Text);
                    objPurchaseReturnProperty.Shipping_Charge = Val.ToDecimal(txtShippingCharge.Text);

                    objPurchaseReturnProperty.net_amount = Val.ToDecimal(txtNetAmount.Text);
                    objPurchaseReturnProperty.Currency_Type = lueCurrency.Text;
                    objPurchaseReturnProperty.Currency_ID = Val.ToInt(m_numCurrency_id);
                    objPurchaseReturnProperty.exchange_rate = Val.ToDecimal(txtExchangeRate.Text);
                    //int IntRes = objSaleInvoice.Save(objSaleInvoiceProperty, m_dtbSaleDetails);
                    objPurchaseReturnProperty = objPurchaseReturn.Save(objPurchaseReturnProperty, DLL.GlobalDec.EnumTran.Start, Conn);

                    Int64 NewmInvoiceid = Val.ToInt64(objPurchaseReturnProperty.purchase_return_id);

                    int IntCounter = 0;
                    int Count = 0;
                    int TotalCount = m_dtbPurchaseReturnDetails.Rows.Count;

                    foreach (DataRow drw in m_dtbPurchaseReturnDetails.Rows)
                    {
                        objPurchaseReturnProperty = new PurchaseReturn_Property();
                        objPurchaseReturnProperty.purchase_return_id = Val.ToInt32(NewmInvoiceid);
                        objPurchaseReturnProperty.return_detail_id = Val.ToInt(drw["return_detail_id"]);
                        objPurchaseReturnProperty.assort_id = Val.ToInt(drw["assort_id"]);
                        objPurchaseReturnProperty.sieve_id = Val.ToInt(drw["sieve_id"]);
                        objPurchaseReturnProperty.sub_sieve_id = Val.ToInt(drw["sub_sieve_id"]);
                        objPurchaseReturnProperty.pcs = Val.ToInt(drw["pcs"]);
                        objPurchaseReturnProperty.carat = Val.ToDecimal(drw["carat"]);
                        objPurchaseReturnProperty.rate = Val.ToDecimal(drw["rate"]);
                        objPurchaseReturnProperty.amount = Val.ToDecimal(drw["amount"]);
                        objPurchaseReturnProperty.discount = Val.ToDecimal(drw["discount"]);

                        objPurchaseReturnProperty.old_carat = Val.ToDecimal(drw["old_carat"]);
                        objPurchaseReturnProperty.old_pcs = Val.ToInt(drw["old_pcs"]);
                        objPurchaseReturnProperty.flag = Val.ToInt(drw["flag"]);
                        objPurchaseReturnProperty.old_assort_id = Val.ToInt(drw["old_assort_id"]);
                        objPurchaseReturnProperty.old_sieve_id = Val.ToInt(drw["old_sieve_id"]);
                        objPurchaseReturnProperty.old_sub_sieve_id = Val.ToInt(drw["old_sub_sieve_id"]);
                        objPurchaseReturnProperty.current_rate = Val.ToDecimal(drw["current_rate"]);
                        objPurchaseReturnProperty.current_amount = Val.ToDecimal(drw["current_amount"]);
                        objPurchaseReturnProperty.old_sieve_id = Val.ToInt(drw["old_sieve_id"]);
                        objPurchaseReturnProperty.Currency_ID = Val.ToInt(m_numCurrency_id);


                        IntRes = objPurchaseReturn.Save_Detail(objPurchaseReturnProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

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
                    objPurchaseReturnProperty = null;
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
        private void backgroundWorker_PurchaseReturn_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                panelProgress.Visible = false;
                if (IntRes > 0)
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Purchase Return Data Save Successfully");
                        ClearDetails();
                        PopulateDetails();
                    }
                    else
                    {
                        Global.Confirm("Purchase Return Data Update Successfully");
                        ClearDetails();
                        PopulateDetails();
                    }
                }
                else
                {
                    Global.Confirm("Error In Purchase Return");
                    txtInvoiceNo.Focus();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }

        #region "Grid Events" 
        private void dgvPurchaseReturnDetails_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvPurchaseReturnDetails.GetDataRow(e.RowHandle);
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
                        m_return_detail_id = Val.ToInt(Drow["return_detail_id"]);
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
        private void dgvPurchaseReturnDetails_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        private void dgvPurchaseReturn_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                if (Val.ToDecimal(clmLAmount.SummaryItem.SummaryValue) > 0 && Val.ToDecimal(clmTotalCarat.SummaryItem.SummaryValue) > 0)
                {
                    m_numSummLRate = Math.Round((Val.ToDecimal(clmLAmount.SummaryItem.SummaryValue) / Val.ToDecimal(clmTotalCarat.SummaryItem.SummaryValue)), 2, MidpointRounding.AwayFromZero);

                }
                else
                {
                    m_numSummLRate = 0;
                }
                if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == "rate")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                        e.TotalValue = m_numSummLRate;
                }

            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        private void dgvPurchaseReturn_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                objPurchaseReturn = new PurchaseReturn();
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        m_blncheckevents = true;

                        DataRow Drow = dgvPurchaseReturn.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt32(Drow["purchase_return_id"]);

                        dtpInvoiceDate.Text = Val.DBDate(Val.ToString(Drow["invoice_date"]));
                        lueDeliveryType.EditValue = Val.ToInt(Drow["delivery_type_id"]);
                        txtInvoiceNo.Text = Val.ToString(Drow["invoice_no"]);
                        lueBilledToParty.EditValue = Val.ToInt(Drow["billed_to_party_id"]);
                        lueShippedToParty.EditValue = Val.ToInt(Drow["shipped_to_party_id"]);
                        lueBroker.EditValue = Val.ToInt(Drow["broker_id"]);
                        txtShippingCharge.Text = Val.ToString(Drow["shipping"]);
                        txtTermDays.Text = Val.ToString(Drow["term_days"]);
                        txtAddOnDays.Text = Val.ToString(Drow["add_on_days"]);
                        dtpDueDate.Text = Val.DBDate(Val.ToString(Drow["due_date"]));
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
                        lueCurrency.EditValue = Val.ToString(Drow["currency"]);
                        txtExchangeRate.EditValue = Val.ToDecimal(Drow["exchange_rate"]);

                        m_dtbPurchaseReturnDetails = objPurchaseReturn.GetDataDetails(Val.ToInt(lblMode.Tag));
                        grdPurchaseReturnDetails.DataSource = m_dtbPurchaseReturnDetails;

                        ttlbPurchaseReturn.SelectedTabPage = tblPurchaseReturndetail;
                        txtInvoiceNo.Focus();
                        btnBrowse.Enabled = false;
                        lueCurrency.ReadOnly = true;
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

        #endregion

        #region Functions
        private bool LoadDefaults()
        {
            bool blnReturn = true;
            try
            {
                Global.LOOKUPParty(lueBilledToParty);
                Global.LOOKUPParty(lueShippedToParty);
                Global.LOOKUPBroker(lueBroker);
                Global.LOOKUPDeliveryType(lueDeliveryType);
                Global.LOOKUPParty(lueBillToParty);

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
                lueCurrency.EditValue = GlobalDec.gEmployeeProperty.currency_id;


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

                btnSearch_Click(null, null);
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
                objPurchaseReturn = new PurchaseReturn();
                DataTable p_dtbDetail = new DataTable();

                p_dtbDetail = objPurchaseReturn.GetCheckPriceList(m_numCurrency_id, Val.ToInt(GlobalDec.gEmployeeProperty.rate_type_id));

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
                    objPurchaseReturn = new PurchaseReturn();
                    //m_dtbStockCarat = objSaleInvoice.GetStockCarat(GlobalDec.gEmployeeProperty.company_id, GlobalDec.gEmployeeProperty.branch_id, GlobalDec.gEmployeeProperty.location_id, GlobalDec.gEmployeeProperty.department_id, Val.ToInt(lueAssortName.EditValue), Val.ToInt(lueSieveName.EditValue));                    
                    if (m_dtbStockCarat.Rows.Count > 0)
                    {
                        numStockCarat = Val.ToDecimal(m_dtbStockCarat.Rows[0]["stock_carat"]);
                    }

                    if (numStockCarat < Val.ToDecimal(txtCarat.Text))
                    {
                        Global.Message("Please check enter carat more then stock carat", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCarat.Focus();
                        blnReturn = false;
                        return blnReturn;
                    }

                    DataRow[] dr = m_dtbPurchaseReturnDetails.Select("sieve_id = " + Val.ToInt(lueSieveName.EditValue) + " AND assort_id = " + Val.ToInt(lueAssortName.EditValue) + " AND sub_sieve_id = " + Val.ToInt(lueSubSieveName.EditValue));

                    if (dr.Count() == 1)
                    {
                        Global.Message("Record already exists in grid", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lueAssortName.Focus();
                        blnReturn = false;
                        return blnReturn;
                    }
                    DataRow drwNew = m_dtbPurchaseReturnDetails.NewRow();
                    decimal numCarat = Val.ToDecimal(txtCarat.Text);
                    decimal numRate = Val.ToDecimal(txtRate.Text);
                    decimal numAmount = Val.ToDecimal(txtAmount.Text);
                    int numPcs = Val.ToInt(txtPcs.Text);

                    drwNew["purchase_return_id"] = Val.ToInt(0);
                    drwNew["return_detail_id"] = Val.ToInt(0);

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

                    m_dtbPurchaseReturnDetails.Rows.Add(drwNew);

                    dgvPurchaseReturnDetails.MoveLast();

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
                    objPurchaseReturn = new PurchaseReturn();
                    if (Val.ToDecimal(txtCarat.Text) > m_numcarat)
                    {
                        if (m_return_detail_id == 0)
                        {
                            DataTable m_dtbStockCarat = new DataTable();
                            m_dtbStockCarat = objPurchaseReturn.GetStockCarat(GlobalDec.gEmployeeProperty.company_id, GlobalDec.gEmployeeProperty.branch_id, GlobalDec.gEmployeeProperty.location_id, GlobalDec.gEmployeeProperty.department_id, Val.ToInt(lueAssortName.EditValue), Val.ToInt(lueSieveName.EditValue));

                            if (m_dtbStockCarat.Rows.Count > 0)
                            {
                                numStockCarat = Val.ToDecimal(m_dtbStockCarat.Rows[0]["stock_carat"]);
                            }

                            if (numStockCarat < Val.ToDecimal(txtCarat.Text))
                            {
                                Global.Message("Please check enter carat more then stock carat", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtCarat.Focus();
                                blnReturn = false;
                                return blnReturn;
                            }
                        }
                        else
                        {
                            DataTable m_dtbStockCarat = new DataTable();
                            m_dtbStockCarat = objPurchaseReturn.GetStockCarat(GlobalDec.gEmployeeProperty.company_id, GlobalDec.gEmployeeProperty.branch_id, GlobalDec.gEmployeeProperty.location_id, GlobalDec.gEmployeeProperty.department_id, Val.ToInt(lueAssortName.EditValue), Val.ToInt(lueSieveName.EditValue));

                            decimal p_numdiff_Carat = Val.ToDecimal(txtCarat.Text) - m_numcarat;

                            if (m_dtbStockCarat.Rows.Count > 0)
                            {
                                numStockCarat = Val.ToDecimal(m_dtbStockCarat.Rows[0]["stock_carat"]);
                            }

                            if (numStockCarat < Val.ToDecimal(p_numdiff_Carat))
                            {
                                Global.Message("Please check enter carat more then stock carat", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtCarat.Focus();
                                blnReturn = false;
                                return blnReturn;
                            }
                        }

                    }

                    if (m_dtbPurchaseReturnDetails.Select("assort_id ='" + Val.ToInt(lueAssortName.EditValue) + "' AND sieve_id ='" + Val.ToInt(lueSieveName.EditValue) + "'").Length > 0)
                    {
                        for (int i = 0; i < m_dtbPurchaseReturnDetails.Rows.Count; i++)
                        {
                            if (m_dtbPurchaseReturnDetails.Select("return_detail_id ='" + m_return_detail_id + "' AND sr_no = '" + m_update_srno + "'").Length > 0)
                            {
                                if (m_dtbPurchaseReturnDetails.Rows[m_update_srno - 1]["return_detail_id"].ToString() == m_return_detail_id.ToString())
                                {
                                    m_dtbPurchaseReturnDetails.Rows[m_update_srno - 1]["carat"] = Val.ToDecimal(txtCarat.Text).ToString();
                                    m_dtbPurchaseReturnDetails.Rows[m_update_srno - 1]["pcs"] = Val.ToInt(txtPcs.Text);
                                    m_dtbPurchaseReturnDetails.Rows[m_update_srno - 1]["rate"] = Val.ToDecimal(txtRate.Text);
                                    m_dtbPurchaseReturnDetails.Rows[m_update_srno - 1]["flag"] = 1;
                                    m_dtbPurchaseReturnDetails.Rows[m_update_srno - 1]["amount"] = Math.Round(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text), 3);
                                    m_dtbPurchaseReturnDetails.Rows[m_update_srno - 1]["current_rate"] = m_current_rate;
                                    m_dtbPurchaseReturnDetails.Rows[m_update_srno - 1]["current_amount"] = m_current_amount;
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
                        for (int i = 0; i < m_dtbPurchaseReturnDetails.Rows.Count; i++)
                        {
                            if (m_dtbPurchaseReturnDetails.Select("return_detail_id ='" + m_return_detail_id + "' AND sr_no = '" + m_update_srno + "'").Length > 0)
                            {
                                if (m_dtbPurchaseReturnDetails.Rows[m_update_srno - 1]["return_detail_id"].ToString() == m_return_detail_id.ToString())
                                {
                                    m_dtbPurchaseReturnDetails.Rows[m_update_srno - 1]["carat"] = Val.ToDecimal(txtCarat.Text).ToString();
                                    m_dtbPurchaseReturnDetails.Rows[m_update_srno - 1]["pcs"] = Val.ToInt(txtPcs.Text);
                                    m_dtbPurchaseReturnDetails.Rows[m_update_srno - 1]["rate"] = Val.ToDecimal(txtRate.Text);
                                    m_dtbPurchaseReturnDetails.Rows[m_update_srno - 1]["flag"] = 1;
                                    m_dtbPurchaseReturnDetails.Rows[m_update_srno - 1]["assort_id"] = Val.ToInt(lueAssortName.EditValue);
                                    m_dtbPurchaseReturnDetails.Rows[m_update_srno - 1]["sieve_id"] = Val.ToInt(lueSieveName.EditValue);
                                    m_dtbPurchaseReturnDetails.Rows[m_update_srno - 1]["sub_sieve_id"] = Val.ToInt(lueSubSieveName.EditValue);
                                    m_dtbPurchaseReturnDetails.Rows[m_update_srno - 1]["assort_name"] = Val.ToString(lueAssortName.Text);
                                    m_dtbPurchaseReturnDetails.Rows[m_update_srno - 1]["sieve_name"] = Val.ToString(lueSieveName.Text);
                                    m_dtbPurchaseReturnDetails.Rows[m_update_srno - 1]["sub_sieve_name"] = Val.ToString(lueSubSieveName.Text);

                                    m_dtbPurchaseReturnDetails.Rows[m_update_srno - 1]["amount"] = Math.Round(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text), 3);
                                    m_dtbPurchaseReturnDetails.Rows[m_update_srno - 1]["current_rate"] = m_current_rate;
                                    m_dtbPurchaseReturnDetails.Rows[m_update_srno - 1]["current_amount"] = m_current_amount;
                                    decimal Shipping_Charge = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) + Val.ToDecimal(txtInterestAmt.Text) + Val.ToDecimal(txtCGSTAmt.Text) + Val.ToDecimal(txtSGSTAmt.Text) + Val.ToDecimal(txtIGSTAmt.Text)) - (Val.ToDecimal(txtDiscountAmt.Text)) + Val.ToDecimal(txtShippingCharge.Text), 0);
                                    txtNetAmount.Text = Shipping_Charge.ToString();
                                }
                            }
                        }
                        btnAdd.Text = "&Add";
                    }
                    dgvPurchaseReturnDetails.MoveLast();
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
                    if (m_dtbPurchaseReturnDetails.Rows.Count == 0)
                    {
                        lstError.Add(new ListError(22, "Record"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                        }
                    }
                    if (dgvPurchaseReturnDetails == null)
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
                if (!GeneratePurchaseReturnDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }

                lblMode.Tag = null;
                lueBilledToParty.EditValue = System.DBNull.Value;
                lueShippedToParty.EditValue = System.DBNull.Value;
                lueBroker.EditValue = System.DBNull.Value;
                txtInvoiceNo.Text = string.Empty;
                txtTermDays.Text = string.Empty;
                txtAddOnDays.Text = string.Empty;
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
                lueDeliveryType.EditValue = System.DBNull.Value;
                lueDeliveryType.EditValue = Val.ToInt32(GlobalDec.gEmployeeProperty.delivery_type_id);
                lueCurrency.EditValue = Val.ToString(GlobalDec.gEmployeeProperty.currency_id);
                btnAdd.Text = "&Add";
                txtInvoiceNo.Focus();
                btnBrowse.Enabled = true;
                lueCurrency.ReadOnly = false;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
        }
        private bool GeneratePurchaseReturnDetails()
        {
            bool blnReturn = true;
            try
            {
                if (m_dtbPurchaseReturnDetails.Rows.Count > 0)
                    m_dtbPurchaseReturnDetails.Rows.Clear();

                m_dtbPurchaseReturnDetails = new DataTable();

                m_dtbPurchaseReturnDetails.Columns.Add("sr_no", typeof(int));
                m_dtbPurchaseReturnDetails.Columns.Add("return_detail_id", typeof(int));
                m_dtbPurchaseReturnDetails.Columns.Add("purchase_return_id", typeof(int));
                m_dtbPurchaseReturnDetails.Columns.Add("assort_id", typeof(int));
                m_dtbPurchaseReturnDetails.Columns.Add("assort_name", typeof(string));
                m_dtbPurchaseReturnDetails.Columns.Add("sieve_id", typeof(int));
                m_dtbPurchaseReturnDetails.Columns.Add("sieve_name", typeof(string));
                m_dtbPurchaseReturnDetails.Columns.Add("sub_sieve_id", typeof(int));
                m_dtbPurchaseReturnDetails.Columns.Add("sub_sieve_name", typeof(string));
                m_dtbPurchaseReturnDetails.Columns.Add("pcs", typeof(int)).DefaultValue = 0;
                m_dtbPurchaseReturnDetails.Columns.Add("carat", typeof(decimal)).DefaultValue = 0;
                m_dtbPurchaseReturnDetails.Columns.Add("rate", typeof(decimal)).DefaultValue = 0;
                m_dtbPurchaseReturnDetails.Columns.Add("amount", typeof(decimal)).DefaultValue = 0;
                m_dtbPurchaseReturnDetails.Columns.Add("discount", typeof(decimal)).DefaultValue = 0;
                m_dtbPurchaseReturnDetails.Columns.Add("remarks", typeof(string));
                m_dtbPurchaseReturnDetails.Columns.Add("old_pcs", typeof(int)).DefaultValue = 0;
                m_dtbPurchaseReturnDetails.Columns.Add("old_carat", typeof(decimal));
                m_dtbPurchaseReturnDetails.Columns.Add("flag", typeof(int)).DefaultValue = 0;
                m_dtbPurchaseReturnDetails.Columns.Add("old_assort_id", typeof(int));
                m_dtbPurchaseReturnDetails.Columns.Add("old_sieve_id", typeof(int));
                m_dtbPurchaseReturnDetails.Columns.Add("old_sub_sieve_id", typeof(int));
                m_dtbPurchaseReturnDetails.Columns.Add("old_assort_name", typeof(string));
                m_dtbPurchaseReturnDetails.Columns.Add("old_sieve_name", typeof(string));
                m_dtbPurchaseReturnDetails.Columns.Add("old_sub_sieve_name", typeof(string));
                m_dtbPurchaseReturnDetails.Columns.Add("current_rate", typeof(decimal)).DefaultValue = 0;
                m_dtbPurchaseReturnDetails.Columns.Add("current_amount", typeof(decimal)).DefaultValue = 0;

                grdPurchaseReturnDetails.DataSource = m_dtbPurchaseReturnDetails;
                grdPurchaseReturnDetails.Refresh();
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
            objPurchaseReturn = new PurchaseReturn();
            bool blnReturn = true;
            DateTime datFromDate = DateTime.MinValue;
            DateTime datToDate = DateTime.MinValue;
            try
            {
                m_dtbDetails = objPurchaseReturn.GetData(Val.DBDate(dtpFromDate.Text), Val.DBDate(dtpToDate.Text), Val.ToString(txtSearchInvoice.Text), Val.ToInt32(lueBillToParty.EditValue));

                if (m_dtbDetails.Rows.Count == 0)
                {
                    Global.Message("Data Not Found");
                    blnReturn = false;
                }

                grdPurchaseReturn.DataSource = m_dtbDetails;
                dgvPurchaseReturn.BestFitColumns();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            finally
            {
                objPurchaseReturn = null;
            }

            return blnReturn;
        }
        private void GetCarat()
        {
            try
            {
                if (Val.ToString(lueAssortName.EditValue) != "" && Val.ToString(lueSieveName.EditValue) != "")
                {
                    m_dtbStockCarat = objPurchaseReturn.GetStockCarat(GlobalDec.gEmployeeProperty.company_id, GlobalDec.gEmployeeProperty.branch_id, GlobalDec.gEmployeeProperty.location_id, GlobalDec.gEmployeeProperty.department_id, Val.ToInt(lueAssortName.EditValue), Val.ToInt(lueSieveName.EditValue));
                }
                else
                {
                    m_dtbStockCarat = null;
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
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
                            dgvPurchaseReturn.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvPurchaseReturn.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvPurchaseReturn.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvPurchaseReturn.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvPurchaseReturn.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvPurchaseReturn.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvPurchaseReturn.ExportToCsv(Filepath);
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

        private void dtpInvoiceDate_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtpInvoiceDate.Text.Length <= 0 || dtpInvoiceDate.Text == "")
                {
                    txtTermDays.Text = "";
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
    }
}