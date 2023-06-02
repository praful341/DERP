using BLL;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Utility;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using DERP.DRPT;
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
    public partial class FrmMemoIssue : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member
        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        Control _NextEnteredControl;
        private List<Control> _tabControls;
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        MemoInvoice objMemoIssue;
        UserAuthentication objUserAuthentication;
        AssortMaster objAssort;
        SieveMaster objSieve;
        RateMaster objRate;
        SaleInvoice objSaleInvoice;
        DataTable DtControlSettings;
        DataTable m_dtbAssorts;
        DataTable m_dtbSieve;
        DataTable m_dtbMemoIssueDetail;
        DataTable m_dtbMemoGetIssueDetail;
        DataTable m_dtbCurrency;
        DataTable m_dtbDetails;
        DataTable m_opDate;
        DataTable m_dtbStockCarat;
        DataSet m_dtbDS;
        DataTable m_dtbCurrencyType;
        DataTable m_dtbSeller;
        DataTable m_dtbInspectionData;
        DataTable m_dtbInspNo = new DataTable();

        int m_numForm_id;
        int m_memo_id;
        int m_srno;
        int m_update_srno;
        int IntRes;
        int m_numCurrency_id;

        string NewMemoNo;
        decimal m_numcarat;
        decimal m_numTotalCarats;
        decimal m_numTotalAmount;
        decimal m_numCurrentRate;
        decimal m_numSummRate;
        decimal numStockCarat;
        bool m_blnadd = new bool();
        bool m_blnsave = new bool();
        bool m_blncheck = new bool();
        #endregion

        #region Constructor
        public FrmMemoIssue()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            _NextEnteredControl = new Control();
            _tabControls = new List<Control>();

            objMemoIssue = new MemoInvoice();
            objUserAuthentication = new UserAuthentication();
            objAssort = new AssortMaster();
            objSieve = new SieveMaster();
            objRate = new RateMaster();
            objSaleInvoice = new SaleInvoice();
            DtControlSettings = new DataTable();
            m_dtbAssorts = new DataTable();
            m_dtbSieve = new DataTable();
            m_dtbMemoIssueDetail = new DataTable();
            m_dtbMemoGetIssueDetail = new DataTable();
            m_dtbCurrency = new DataTable();
            m_dtbDetails = new DataTable();
            m_opDate = new DataTable();
            m_dtbStockCarat = new DataTable();
            m_dtbDS = new DataSet();
            m_dtbCurrencyType = new DataTable();
            m_dtbSeller = new DataTable();
            m_dtbInspectionData = new DataTable();

            m_numForm_id = 0;
            m_memo_id = 0;
            m_srno = 0;
            m_update_srno = 0;
            IntRes = 0;

            NewMemoNo = "";
            m_numcarat = 0;
            m_numTotalCarats = 0;
            m_numTotalAmount = 0;
            m_numCurrentRate = 0;
            numStockCarat = 0;

            m_blnadd = new bool();
            m_blnsave = new bool();
            m_blncheck = true;
            m_numCurrency_id = 0;
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

            m_dtbInspectionData = MemoDt;

            this.Show();
            FillInspToMemo();
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
            objBOFormEvents.ObjToDispose.Add(objMemoIssue);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);

        }

        #endregion        

        #region Events
        private void FrmMemoIssue_Load(object sender, EventArgs e)
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
                    ttlbMemoIssue.SelectedTabPage = tblMemoIssuedetail;
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
                txtDiscPer.Text = string.Empty;
                txtDiscAmt.Text = string.Empty;
                txtRate.Text = string.Empty;
                txtAmount.Text = string.Empty;
                txtNetAmount.Text = string.Empty;
                txtPurchaseRate.Text = string.Empty;
                txtPurchaseAmount.Text = string.Empty;
                txtRejCarat.Text = string.Empty;
                txtRejectionPer.Text = string.Empty;
                txtRejPcs.Text = string.Empty;
                lueAssortName.Focus();
                lueAssortName.ShowPopup();
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

                //Int64 Return_MemoId = objMemoIssue.FindReturnMemoID(Val.ToString(txtMemoNo.Text));

                //if (Return_MemoId > 0)
                //{
                //    Global.Message("Already Receive This Memo No...");
                //    btnSave.Enabled = true;
                //    return;
                //}

                DialogResult result = MessageBox.Show("Do you want to save Memo?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result != DialogResult.Yes)
                {
                    btnSave.Enabled = true;
                    return;
                }

                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;

                panelProgress.Visible = true;
                backgroundWorker_MemoIssue.RunWorkerAsync();

                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                this.Cursor = Cursors.Default;
                return;
            }
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
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                ObjPer.SetFormPer();
                if (ObjPer.AllowPrint == false)
                {
                    Global.Message(BLL.GlobalDec.gStrPermissionPrintMsg);
                    return;
                }
                Memo_InvoiceProperty objMemoIssue_Property = new Memo_InvoiceProperty();
                //objMemoIssue_Property.memo_date = Val.DBDate(dtpMemoDate.Text);
                if (txtMemoNo.Text != "")
                {
                    objMemoIssue_Property.memo_no = Val.ToString(txtMemoNo.Text);
                }
                else
                {
                    objMemoIssue_Property.memo_no = Val.ToString(NewMemoNo);
                }

                m_dtbDetails = objMemoIssue.GetPrintData(objMemoIssue_Property);

                m_dtbDetails.Columns.Add("srno", typeof(int));
                int srno = 0;

                if (m_dtbDetails.Rows.Count > 0)
                {
                    foreach (DataRow DRW in m_dtbDetails.Rows)
                    {
                        srno = srno + 1;
                        DRW["srno"] = srno;
                    }
                }

                ClearDetails();

                XtraReportViewer frmRepViewer = new XtraReportViewer("Memo Issue", "Memo Issue Report", m_dtbDetails, m_dtbDS);
                frmRepViewer.MdiParent = this.MdiParent;
                frmRepViewer.Show();
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void txtCarat_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtAmount.Text = string.Format("{0:0.00}", Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text));
                txtNetAmount.Text = Math.Round(Val.ToDecimal(txtAmount.Text) - Val.ToDecimal(txtDiscAmt.Text), 3).ToString();
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
                txtNetAmount.Text = Math.Round(Val.ToDecimal(txtAmount.Text) - Val.ToDecimal(txtDiscAmt.Text), 3).ToString();
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void lueParty_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToInt(lueParty.EditValue) > 0)
                {
                    Int32 Party_Id = Val.ToInt(lueParty.EditValue);
                    DataTable DTab_PartyMap = objMemoIssue.GetData_Party_To_Broker_Map(Party_Id);

                    if (DTab_PartyMap.Rows.Count > 0)
                    {
                        lueBroker.Properties.DataSource = DTab_PartyMap;
                        lueBroker.Properties.ValueMember = "broker_id";
                        lueBroker.Properties.DisplayMember = "broker_name";
                        lueBroker.EditValue = lueBroker.Properties.GetDataSourceValue("broker_id", 0);
                    }
                    else
                    {
                        lueBroker.Properties.DataSource = null;
                        Global.LOOKUPBroker(lueBroker);
                        lueBroker.EditValue = null;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void FrmMemoIssue_Leave(object sender, EventArgs e)
        {
            m_blncheck = false;
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
                    m_numCurrentRate = Val.ToDecimal(p_numStockRate);

                    decimal p_numNewStockRate = Val.ToDecimal(p_numStockRate);

                    txtRate.Text = Val.ToString(p_numNewStockRate + ((p_numNewStockRate / 100) * 20));
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
                if (lueAssortName.ItemIndex != -1 && lueSieveName.ItemIndex != -1)
                {
                    objSaleInvoice = new SaleInvoice();
                    if (lblMode.Text == "Add Mode")
                    {
                        GetCarat();
                    }
                    Global.LOOKUPSubSieve(lueSubSieveName, Val.ToInt(lueSieveName.EditValue));
                    lueAssortName_EditValueChanged(null, null);
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void txtBrokeragePer_EditValueChanged_1(object sender, EventArgs e)
        {
            try
            {
                decimal Brokerage_amt = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) - Val.ToDecimal(txtBrokerageAmt.Text)) * Val.ToDecimal(txtBrokeragePer.Text) / 100, 0);
                txtBrokerageAmt.Text = Brokerage_amt.ToString();
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void txtMemoNo_EditValueChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    m_blncheck = true;
            //    string MemoNo = Val.ToString(txtMemoNo.Text);
            //    if (m_blncheck)
            //        GetData(MemoNo);
            //    m_blncheck = false;
            //}
            //catch (Exception ex)
            //{
            //    Global.Message(ex.ToString());
            //    return;
            //}
        }
        private void txtDiscPer_EditValueChanged(object sender, EventArgs e)
        {

            try
            {
                decimal Disc_amt = Math.Round((Val.ToDecimal(txtAmount.Text)) * Val.ToDecimal(txtDiscPer.Text) / 100, 0);
                txtDiscAmt.Text = Disc_amt.ToString();
                txtNetAmount.Text = Math.Round(Val.ToDecimal(txtAmount.Text) - Val.ToDecimal(Disc_amt), 3).ToString();
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void lueParty_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (e.Button.Index == 1)
                {
                    FrmPartyMaster frmParty = new FrmPartyMaster();
                    frmParty.ShowDialog();
                    Global.LOOKUPParty(lueParty);
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
        private void panelControl4_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(255, 191, 219, 255), 2);
            e.Graphics.DrawLine(pen, 0, 55, 1500, 55);
        }
        private void backgroundWorker_MemoIssue_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                    Conn = new BeginTranConnection(true, false);                
                Memo_InvoiceProperty objMemoIssue_Property = new Memo_InvoiceProperty();
                MemoInvoice objMemoInvoice = new MemoInvoice();
                try
                {
                    IntRes = 0;
                    int IntCounter = 0;
                    int Count = 0;
                    int TotalCount = m_dtbMemoIssueDetail.Rows.Count;
                    int IntMemoMasterID = 0;
                    NewMemoNo = "";

                    IntMemoMasterID = Val.ToInt(objMemoInvoice.FindMaxMemoMasterID());
                    objMemoIssue_Property.memo_master_id = IntMemoMasterID;

                    foreach (DataRow drw in m_dtbMemoIssueDetail.Rows)
                    {
                        objMemoIssue_Property.memo_id = Val.ToInt(drw["memo_id"]);
                        objMemoIssue_Property.memo_no = Val.ToString(txtMemoNo.Text);

                        objMemoIssue_Property.company_id = Val.ToInt(GlobalDec.gEmployeeProperty.company_id);
                        objMemoIssue_Property.branch_id = Val.ToInt(GlobalDec.gEmployeeProperty.branch_id);
                        objMemoIssue_Property.location_id = Val.ToInt(GlobalDec.gEmployeeProperty.location_id);
                        objMemoIssue_Property.department_id = Val.ToInt(GlobalDec.gEmployeeProperty.department_id);

                        objMemoIssue_Property.memo_date = Val.DBDate(dtpMemoDate.Text);
                        objMemoIssue_Property.delivery_type_id = Val.ToInt(lueDeliveryType.EditValue);
                        objMemoIssue_Property.remarks = Val.ToString(txtRemark.Text);
                        objMemoIssue_Property.inspection_master_id = Val.ToInt(lueInspectionNo.EditValue);
                        objMemoIssue_Property.form_id = m_numForm_id;

                        objMemoIssue_Property.term_days = Val.ToInt(txtTermDays.Text);
                        objMemoIssue_Property.due_date = Val.DBDate(dtpDueDate.Text);
                        objMemoIssue_Property.final_days = Val.ToInt(txtFinalTermDays.Text);
                        objMemoIssue_Property.final_due_date = Val.DBDate(dtpFinalDueDate.Text);

                        objMemoIssue_Property.Party_Id = Val.ToInt(lueParty.EditValue);
                        objMemoIssue_Property.Broker_Id = Val.ToInt(lueBroker.EditValue);

                        objMemoIssue_Property.Special_Remark = Val.ToString(txtSpecialRemark.Text);
                        objMemoIssue_Property.Client_Remark = Val.ToString(txtClientRemark.Text);
                        objMemoIssue_Property.Payment_Remark = Val.ToString(txtPaymentRemark.Text);

                        objMemoIssue_Property.Brokerage_Per = Val.ToDecimal(txtBrokeragePer.Text);
                        objMemoIssue_Property.Brokerage_Amt = Val.ToDecimal(txtBrokerageAmt.Text);

                        objMemoIssue_Property.assort_id = Val.ToInt(drw["assort_id"]);
                        objMemoIssue_Property.sieve_id = Val.ToInt(drw["sieve_id"]);
                        objMemoIssue_Property.sub_sieve_id = Val.ToInt(drw["sub_sieve_id"]);
                        objMemoIssue_Property.Pcs = Val.ToInt(drw["pcs"]);
                        objMemoIssue_Property.carat = Val.ToDecimal(drw["carat"]);
                        objMemoIssue_Property.rate = Val.ToDecimal(drw["rate"]);
                        objMemoIssue_Property.amount = Val.ToDecimal(drw["amount"]);
                        objMemoIssue_Property.discount_per = Val.ToDecimal(drw["discount_per"]);
                        objMemoIssue_Property.discount_amount = Val.ToDecimal(drw["discount_amt"]);
                        objMemoIssue_Property.Net_Amt = Val.ToDecimal(drw["net_amount"]);

                        objMemoIssue_Property.diff_carat = Val.ToDecimal(drw["diff_carat"]);
                        objMemoIssue_Property.diff_pcs = Val.ToInt(drw["diff_pcs"]);
                        objMemoIssue_Property.flag = Val.ToInt(drw["flag"]);
                        objMemoIssue_Property.old_assort_id = Val.ToInt(drw["old_assort_id"]);
                        objMemoIssue_Property.old_sieve_id = Val.ToInt(drw["old_sieve_id"]);
                        objMemoIssue_Property.old_sub_sieve_id = Val.ToInt(drw["old_sub_sieve_id"]);
                        objMemoIssue_Property.current_rate = Val.ToDecimal(drw["current_rate"]);
                        objMemoIssue_Property.current_amount = Val.ToDecimal(drw["current_amount"]);

                        objMemoIssue_Property.exchange_rate = Val.ToDecimal(txtExchangeRate.Text);
                        objMemoIssue_Property.currency_type = Val.ToString(lueCurrency.Text);
                        objMemoIssue_Property.seller_id = Val.ToInt(lueSeller.EditValue);
                        objMemoIssue_Property.purchase_rate = Val.ToDecimal(drw["purchase_rate"]);
                        objMemoIssue_Property.purchase_amount = Val.ToDecimal(drw["purchase_amount"]);

                        objMemoIssue_Property.rej_pcs = Val.ToInt(drw["rej_pcs"]);
                        objMemoIssue_Property.rej_per = Val.ToDecimal(drw["rej_per"]);
                        objMemoIssue_Property.rej_carat = Val.ToDecimal(drw["rej_carat"]);
                        objMemoIssue_Property.status_days = Val.ToInt(txtStatusDays.Text);

                        objMemoIssue_Property = objMemoIssue.Save(objMemoIssue_Property, DLL.GlobalDec.EnumTran.Continue, Conn);

                        NewMemoNo = Val.ToString(objMemoIssue_Property.memo_no);

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
                    objMemoIssue_Property = null;
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
        private void backgroundWorker_MemoIssue_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                panelProgress.Visible = false;
                if (IntRes > 0)
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        if (Global.Confirm("Memo Data Save Successfully.... Your Memo No is : " + NewMemoNo + "\n Are You Sure To Print Memo Janged ?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            btnPrint_Click(null, null);
                        }
                        else
                        {
                            ClearDetails();
                        }
                    }
                    else
                    {
                        if (Global.Confirm("Memo Data Save Successfully.... Your Memo No is : " + NewMemoNo + "\n Are You Sure To Print Memo Janged ?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            btnPrint_Click(null, null);
                        }
                        else
                        {
                            ClearDetails();
                        }
                    }
                }
                else
                {
                    Global.Confirm("Error In Memo");
                    txtMemoNo.Focus();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }

        #region "Grid Events" 
        private void dgvMemoDetails_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                if (Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) > 0 && Val.ToDecimal(clmDetCarat.SummaryItem.SummaryValue) > 0)
                {
                    m_numSummRate = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) / Val.ToDecimal(clmDetCarat.SummaryItem.SummaryValue)), 3, MidpointRounding.AwayFromZero);

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
        private void dgvMemoIssue_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                objMemoIssue = new MemoInvoice();
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvMemoView.GetDataRow(e.RowHandle);
                        txtMemoNo.Text = Val.ToString(Drow["memo_no"]);
                        ttlbMemoIssue.SelectedTabPage = tblMemoIssuedetail;
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void dgvMemoIssueDetails_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                objMemoIssue = new MemoInvoice();
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvMemoDetails.GetDataRow(e.RowHandle);
                        btnAdd.Text = "&Update";
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt32(Drow["memo_id"]);

                        //lueSieveName.Text = Val.ToString(Drow["sieve_name"]);
                        lueSieveName.EditValue = Val.ToInt(Drow["sieve_id"]);

                        //lueSubSieveName.Tag = Val.ToInt64(Drow["sub_sieve_id"]);
                        lueSubSieveName.EditValue = Val.ToInt(Drow["sub_sieve_id"]);

                        // lueAssortName.Text = Val.ToString(Drow["assort_name"]);
                        lueAssortName.EditValue = Val.ToInt(Drow["assort_id"]);

                        txtPcs.Text = Val.ToString(Drow["pcs"]);
                        txtCarat.Text = Val.ToString(Drow["carat"]);
                        txtDiscPer.Text = Val.ToString(Drow["discount_per"]);
                        txtDiscAmt.Text = Val.ToString(Drow["discount_amt"]);
                        txtRate.Text = Val.ToString(Drow["rate"]);
                        txtAmount.Text = Val.ToString(Drow["amount"]);

                        txtRejPcs.Text = Val.ToString(Drow["rej_pcs"]);
                        txtRejectionPer.Text = Val.ToString(Drow["rej_per"]);
                        txtRejCarat.Text = Val.ToString(Drow["rej_carat"]);

                        m_numcarat = Val.ToDecimal(Drow["carat"]);
                        m_memo_id = Val.ToInt(Drow["memo_id"]);
                        m_update_srno = Val.ToInt(Drow["sr_no"]);
                        txtPurchaseRate.Text = Val.ToString(Drow["purchase_rate"]);
                        txtPurchaseAmount.Text = Val.ToString(Drow["purchase_amount"]);

                        lueAssortName.Focus();

                        // GetCarat();

                        txtPcs.Enabled = false;
                        txtCarat.Enabled = false;

                        txtRejPcs.Enabled = false;
                        txtRejectionPer.Enabled = false;
                        txtRejCarat.Enabled = false;

                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void dgvMemoView_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                m_numTotalCarats = Math.Round(Val.ToDecimal(clmCarats.SummaryItem.SummaryValue), 3, MidpointRounding.AwayFromZero);

                if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == "rate")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                        m_numTotalAmount = 0;
                    else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                        m_numTotalAmount += (Val.ToDecimal(e.GetValue("carat")) * Val.ToDecimal(e.GetValue("rate")));
                    else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    {
                        if (m_numTotalAmount > 0 && m_numTotalCarats > 0)
                            e.TotalValue = Math.Round((m_numTotalAmount / m_numTotalCarats), 3, MidpointRounding.AwayFromZero);
                        else
                            e.TotalValue = 0;
                    }
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
                Global.LOOKUPParty(lueListParty);
                Global.LOOKUPParty(lueParty);
                Global.LOOKUPBroker(lueBroker);
                Global.LOOKUPDeliveryType(lueDeliveryType);

                m_dtbSeller = objSaleInvoice.GetSellerName("Seller");

                lueSeller.Properties.DataSource = m_dtbSeller;
                lueSeller.Properties.ValueMember = "employee_id";
                lueSeller.Properties.DisplayMember = "employee_name";

                m_dtbAssorts = objAssort.GetData(1);
                lueAssortName.Properties.DataSource = m_dtbAssorts;
                lueAssortName.Properties.ValueMember = "assort_id";
                lueAssortName.Properties.DisplayMember = "assort_name";

                m_dtbSieve = objSieve.GetData(1);
                lueSieveName.Properties.DataSource = m_dtbSieve;
                lueSieveName.Properties.ValueMember = "sieve_id";
                lueSieveName.Properties.DisplayMember = "sieve_name";

                m_dtbCurrencyType = Global.CurrencyType();
                lueCurrency.Properties.DataSource = m_dtbCurrencyType;
                lueCurrency.Properties.ValueMember = "currency_id";
                lueCurrency.Properties.DisplayMember = "currency";
                lueCurrency.EditValue = GlobalDec.gEmployeeProperty.currency_id;

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

                dtpMemoDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpMemoDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpMemoDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpMemoDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpMemoDate.EditValue = DateTime.Now;

                m_dtbInspNo = objMemoIssue.GetInspectionNo();

                lueInspectionNo.Properties.DataSource = m_dtbInspNo;
                lueInspectionNo.Properties.ValueMember = "inspection_master_id";
                lueInspectionNo.Properties.DisplayMember = "inspection_no";
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
                    if (m_dtbStockCarat.Rows.Count > 0)
                    {
                        numStockCarat = Val.ToDecimal(m_dtbStockCarat.Rows[0]["stock_carat"]);
                    }

                    if (numStockCarat < (Val.ToDecimal(txtCarat.Text)))
                    {
                        Global.Message("Please check enter carat more then stock carat", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCarat.Focus();
                        blnReturn = false;
                        return blnReturn;
                    }
                    m_srno = dgvMemoDetails.RowCount;
                    DataRow[] dr = m_dtbMemoIssueDetail.Select("sieve_id = " + Val.ToInt(lueSieveName.EditValue) + " AND assort_id = " + Val.ToInt(lueAssortName.EditValue) + " AND sub_sieve_id = " + Val.ToInt(lueSubSieveName.EditValue));
                    if (dr.Count() == 1)
                    {
                        Global.Message("Record already exists in grid", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lueAssortName.Focus();
                        blnReturn = false;
                        return blnReturn;
                    }

                    DataRow drwNew = m_dtbMemoIssueDetail.NewRow();
                    decimal numCarat = Val.ToDecimal(txtCarat.Text);
                    decimal numRate = Val.ToDecimal(txtRate.Text);
                    decimal numAmount = Val.ToDecimal(txtAmount.Text);
                    decimal numDiscPer = Val.ToDecimal(txtDiscPer.Text);
                    decimal numDiscAmount = Val.ToDecimal(txtDiscAmt.Text);
                    decimal numNetAmount = Val.ToDecimal(txtNetAmount.Text);

                    decimal numpurRate = Val.ToDecimal(txtPurchaseRate.Text);
                    decimal numpurAmount = Val.ToDecimal(txtPurchaseAmount.Text);

                    int numPcs = Val.ToInt(txtPcs.Text);

                    drwNew["memo_no"] = Val.ToString(txtMemoNo.Text);
                    drwNew["memo_id"] = Val.ToInt(0);
                    drwNew["memo_date"] = Val.ToString(dtpMemoDate.Text);
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
                    drwNew["discount_per"] = numDiscPer;
                    drwNew["discount_amt"] = numDiscAmount;
                    drwNew["net_amount"] = numNetAmount;

                    drwNew["current_rate"] = Val.ToDecimal(m_numCurrentRate);
                    drwNew["current_amount"] = Val.ToDecimal(Val.ToDecimal(txtCarat.Text) * m_numCurrentRate);

                    drwNew["purchase_rate"] = Val.ToDecimal(numpurRate);
                    drwNew["purchase_amount"] = Val.ToDecimal(numpurAmount);


                    drwNew["rej_pcs"] = Val.ToInt(txtRejPcs.Text);
                    drwNew["rej_per"] = Val.ToDecimal(txtRejectionPer.Text);
                    drwNew["rej_carat"] = Val.ToDecimal(txtRejCarat.Text);

                    m_srno = m_srno + 1;

                    drwNew["sr_no"] = m_srno;
                    m_dtbMemoIssueDetail.Rows.Add(drwNew);

                    txtBrokeragePer_EditValueChanged_1(null, null);

                    dgvMemoDetails.MoveLast();

                    //DataView dv = m_dtbMemoIssueDetail.DefaultView;
                    //dv.Sort = "sr_no desc";
                    //DataTable sortedDT = dv.ToTable();
                }
                else if (btnAdd.Text == "&Update")
                {
                    DataRow[] dr = m_dtbMemoIssueDetail.Select("sieve_id = " + Val.ToInt(lueSieveName.EditValue) + " AND assort_id = " + Val.ToInt(lueAssortName.EditValue) + " AND sub_sieve_id = " + Val.ToInt(lueSubSieveName.EditValue) + " AND sr_no <> " + Val.ToInt(m_update_srno));
                    if (dr.Count() == 1)
                    {
                        Global.Message("Record already exists in grid", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lueAssortName.Focus();
                        blnReturn = false;
                        return blnReturn;
                    }

                    if (m_dtbMemoIssueDetail.Select("assort_id ='" + Val.ToInt(lueAssortName.EditValue) + "' AND sieve_id ='" + Val.ToInt(lueSieveName.EditValue) + "'AND sub_sieve_id ='" + Val.ToInt(lueSubSieveName.EditValue) + "'").Length > 0)
                    {
                        for (int i = 0; i < m_dtbMemoIssueDetail.Rows.Count; i++)
                        {
                            if (m_dtbMemoIssueDetail.Select("memo_id ='" + m_memo_id + "'AND sr_no = '" + m_update_srno + "'").Length > 0)
                            {
                                if (m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["memo_id"].ToString() == m_memo_id.ToString())
                                {
                                    m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["diff_carat"] = m_dtbMemoIssueDetail.Rows[i]["carat"];
                                    m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["diff_pcs"] = m_dtbMemoIssueDetail.Rows[i]["pcs"];
                                    m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["carat"] = Val.ToDecimal(txtCarat.Text).ToString();
                                    m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["pcs"] = Val.ToInt(txtPcs.Text);
                                    m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["rate"] = Val.ToDecimal(txtRate.Text);
                                    m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["flag"] = 1;
                                    // Add By Praful On 13082020
                                    m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["sieve_id"] = Val.ToInt(lueSieveName.EditValue);
                                    m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["sub_sieve_id"] = Val.ToInt(lueSubSieveName.EditValue);
                                    m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["sieve_name"] = Val.ToString(lueSieveName.Text);
                                    m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["sub_sieve_name"] = Val.ToString(lueSubSieveName.Text);
                                    // End
                                    m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["discount_per"] = Val.ToDecimal(txtDiscPer.Text);
                                    m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["discount_amt"] = Val.ToDecimal(txtDiscAmt.Text);
                                    m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["net_amount"] = Val.ToDecimal(txtNetAmount.Text);
                                    m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["amount"] = Math.Round(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text), 3);

                                    m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["purchase_rate"] = Val.ToDecimal(txtPurchaseRate.Text);
                                    m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["purchase_amount"] = Val.ToDecimal(txtPurchaseAmount.Text);

                                    m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["rej_pcs"] = Val.ToInt(txtRejPcs.Text);
                                    m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["rej_per"] = Val.ToDecimal(txtRejectionPer.Text);
                                    m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["rej_carat"] = Val.ToDecimal(txtRejCarat.Text);

                                    break;
                                }
                            }
                        }
                        btnAdd.Text = "&Add";
                    }
                    else
                    {
                        for (int i = 0; i < m_dtbMemoIssueDetail.Rows.Count; i++)
                        {
                            if (m_dtbMemoIssueDetail.Select("memo_id ='" + m_memo_id + "' AND sr_no = '" + m_update_srno + "'").Length > 0)
                            {
                                if (m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["memo_id"].ToString() == m_memo_id.ToString())
                                {
                                    m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["diff_carat"] = m_dtbMemoIssueDetail.Rows[i]["carat"];
                                    m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["diff_pcs"] = m_dtbMemoIssueDetail.Rows[i]["pcs"];
                                    m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["carat"] = Val.ToDecimal(txtCarat.Text).ToString();
                                    m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["pcs"] = Val.ToInt(txtPcs.Text);
                                    m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["rate"] = Val.ToDecimal(txtRate.Text);
                                    m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["flag"] = 1;
                                    m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["assort_id"] = Val.ToInt(lueAssortName.EditValue);
                                    m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["sieve_id"] = Val.ToInt(lueSieveName.EditValue);
                                    m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["sub_sieve_id"] = Val.ToInt(lueSubSieveName.EditValue);
                                    m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["assort_name"] = Val.ToString(lueAssortName.Text);
                                    m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["sieve_name"] = Val.ToString(lueSieveName.Text);
                                    m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["sub_sieve_name"] = Val.ToString(lueSubSieveName.Text);

                                    m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["discount_per"] = Val.ToDecimal(txtDiscPer.Text);
                                    m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["discount_amt"] = Val.ToDecimal(txtDiscAmt.Text);
                                    m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["net_amount"] = Val.ToDecimal(txtNetAmount.Text);

                                    m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["amount"] = Math.Round(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text), 3);

                                    m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["purchase_rate"] = Val.ToDecimal(txtPurchaseRate.Text);
                                    m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["purchase_amount"] = Val.ToDecimal(txtPurchaseAmount.Text);

                                    m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["rej_pcs"] = Val.ToInt(txtRejPcs.Text);
                                    m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["rej_per"] = Val.ToDecimal(txtRejectionPer.Text);
                                    m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["rej_carat"] = Val.ToDecimal(txtRejCarat.Text);

                                }
                            }
                        }
                        btnAdd.Text = "&Add";
                    }
                    dgvMemoDetails.MoveLast();
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
                if (m_blnsave)
                {
                    if (txtMemoNo.Text == string.Empty)
                    {
                        lstError.Add(new ListError(12, "Memo No"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtMemoNo.Focus();
                        }
                    }

                    if (lueParty.ItemIndex < 0 && lueBroker.ItemIndex < 0)
                    {
                        lstError.Add(new ListError(13, "Party / Broker"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueParty.Focus();
                        }
                    }
                    if (m_dtbMemoIssueDetail.Rows.Count == 0)
                    {
                        lstError.Add(new ListError(22, "Record"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                        }
                    }
                    var result = DateTime.Compare(Convert.ToDateTime(dtpMemoDate.Text), DateTime.Today);
                    if (result > 0)
                    {
                        lstError.Add(new ListError(5, "Memo Date Not Be Greater Than Today Date"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            dtpMemoDate.Focus();
                        }
                    }
                    if (chkActive.Checked == false)
                    {
                        if (!objMemoIssue.ISExists(txtMemoNo.Text).ToString().Trim().Equals(string.Empty))
                        {
                            lstError.Add(new ListError(5, "Memo No. Already Exist"));
                            if (!blnFocus)
                            {
                                blnFocus = true;
                                txtMemoNo.Focus();
                            }
                        }
                    }

                    DataSet DTab_Party = objSaleInvoice.GetDataPartyOutStandingLimit(Val.ToInt64(lueParty.EditValue));

                    if (DTab_Party.Tables[0].Rows.Count > 0)
                    {
                        if (DTab_Party.Tables[0].Rows[0]["is_party_lock_open"].ToString() == "False")
                        {
                            if (DTab_Party.Tables[1].Rows.Count > 0)
                            {
                                lstError.Add(new ListError(5, "Party Invoice Due Date Limit Over...Please Contact To Administrator"));
                                if (!blnFocus)
                                {
                                    blnFocus = true;
                                    btnSave.Focus();
                                }
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

                    if (Val.ToDouble(txtPurchaseRate.Text) == 0)
                    {
                        lstError.Add(new ListError(12, "Purchase Rate"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtPurchaseRate.Focus();
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

                    if (Val.ToDouble(txtNetAmount.Text) == 0)
                    {
                        lstError.Add(new ListError(12, "Net Amount"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtNetAmount.Focus();
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
                if (!GenerateMemoIssueDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }

                lblMode.Tag = null;
                lblMode.Text = "Add Mode";
                lueParty.EditValue = System.DBNull.Value;
                lueDeliveryType.EditValue = System.DBNull.Value;
                txtMemoNo.Text = string.Empty;
                lueBroker.EditValue = System.DBNull.Value;
                lueDeliveryType.EditValue = Val.ToInt32(GlobalDec.gEmployeeProperty.delivery_type_id);

                m_opDate = Global.GetDate();
                dtpFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpFromDate.EditValue = Val.DBDate(m_opDate.Rows[0]["opening_date"].ToString());

                dtpMemoDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpMemoDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpMemoDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpMemoDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpMemoDate.EditValue = DateTime.Now;

                lueAssortName.EditValue = System.DBNull.Value;
                lueSieveName.EditValue = System.DBNull.Value;
                lueSubSieveName.EditValue = System.DBNull.Value;
                lueSeller.EditValue = System.DBNull.Value;

                txtFinalTermDays.Text = string.Empty;
                txtTermDays.Text = string.Empty;
                txtPcs.Text = string.Empty;
                txtCarat.Text = string.Empty;
                txtDiscPer.Text = string.Empty;
                txtDiscAmt.Text = string.Empty;
                txtNetAmount.Text = string.Empty;
                txtRate.Text = string.Empty;
                txtAmount.Text = string.Empty;
                //txtBrokeragePer.Text = string.Empty;
                txtBrokerageAmt.Text = string.Empty;

                txtRejCarat.Text = string.Empty;
                txtRejectionPer.Text = string.Empty;
                txtRejPcs.Text = string.Empty;
                txtStatusDays.Text = string.Empty;

                txtRemark.Text = string.Empty;
                txtSpecialRemark.Text = string.Empty;
                txtPaymentRemark.Text = string.Empty;
                txtClientRemark.Text = string.Empty;
                txtMemoNo.Focus();
                lblMemoNo.Tag = null;
                lblBalCrt.Text = string.Empty;
                lueCurrency.EditValue = Val.ToString(GlobalDec.gEmployeeProperty.currency_id);
                lueCurrency.ReadOnly = false;

                m_srno = 0;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
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
        private bool GenerateMemoIssueDetails()
        {
            bool blnReturn = true;
            try
            {
                if (m_dtbMemoIssueDetail.Rows.Count > 0)
                    m_dtbMemoIssueDetail.Rows.Clear();

                m_dtbMemoIssueDetail = new DataTable();
                m_dtbMemoIssueDetail.Columns.Add("sr_no", typeof(int));
                m_dtbMemoIssueDetail.Columns.Add("memo_id", typeof(int));
                m_dtbMemoIssueDetail.Columns.Add("memo_no", typeof(string));
                m_dtbMemoIssueDetail.Columns.Add("memo_date", typeof(string));
                m_dtbMemoIssueDetail.Columns.Add("assort_id", typeof(int));
                m_dtbMemoIssueDetail.Columns.Add("assort_name", typeof(string));
                m_dtbMemoIssueDetail.Columns.Add("sieve_id", typeof(int));
                m_dtbMemoIssueDetail.Columns.Add("sieve_name", typeof(string));
                m_dtbMemoIssueDetail.Columns.Add("sub_sieve_id", typeof(int));
                m_dtbMemoIssueDetail.Columns.Add("sub_sieve_name", typeof(string));
                m_dtbMemoIssueDetail.Columns.Add("pcs", typeof(int)).DefaultValue = 0;
                m_dtbMemoIssueDetail.Columns.Add("carat", typeof(decimal)).DefaultValue = 0;
                m_dtbMemoIssueDetail.Columns.Add("rate", typeof(decimal)).DefaultValue = 0;
                m_dtbMemoIssueDetail.Columns.Add("amount", typeof(decimal)).DefaultValue = 0;
                m_dtbMemoIssueDetail.Columns.Add("discount_per", typeof(decimal)).DefaultValue = 0;
                m_dtbMemoIssueDetail.Columns.Add("discount_amt", typeof(decimal)).DefaultValue = 0;
                m_dtbMemoIssueDetail.Columns.Add("net_amount", typeof(decimal)).DefaultValue = 0;
                m_dtbMemoIssueDetail.Columns.Add("discount", typeof(decimal)).DefaultValue = 0;
                m_dtbMemoIssueDetail.Columns.Add("remarks", typeof(string));
                m_dtbMemoIssueDetail.Columns.Add("diff_pcs", typeof(int)).DefaultValue = 0;
                m_dtbMemoIssueDetail.Columns.Add("diff_carat", typeof(decimal));
                m_dtbMemoIssueDetail.Columns.Add("flag", typeof(int)).DefaultValue = 0;
                m_dtbMemoIssueDetail.Columns.Add("old_assort_id", typeof(int));
                m_dtbMemoIssueDetail.Columns.Add("old_sieve_id", typeof(int));
                m_dtbMemoIssueDetail.Columns.Add("old_sub_sieve_id", typeof(int));
                m_dtbMemoIssueDetail.Columns.Add("old_assort_name", typeof(string));
                m_dtbMemoIssueDetail.Columns.Add("old_sieve_name", typeof(string));
                m_dtbMemoIssueDetail.Columns.Add("old_sub_sieve_name", typeof(string));

                m_dtbMemoIssueDetail.Columns.Add("current_rate", typeof(decimal));
                m_dtbMemoIssueDetail.Columns.Add("current_amount", typeof(decimal));

                m_dtbMemoIssueDetail.Columns.Add("purchase_rate", typeof(decimal)).DefaultValue = 0;
                m_dtbMemoIssueDetail.Columns.Add("purchase_amount", typeof(decimal)).DefaultValue = 0;

                m_dtbMemoIssueDetail.Columns.Add("rej_pcs", typeof(int)).DefaultValue = 0;
                m_dtbMemoIssueDetail.Columns.Add("rej_per", typeof(decimal)).DefaultValue = 0;
                m_dtbMemoIssueDetail.Columns.Add("rej_carat", typeof(decimal)).DefaultValue = 0;

                grdMemoDetails.DataSource = m_dtbMemoIssueDetail;
                grdMemoDetails.Refresh();
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
            objMemoIssue = new MemoInvoice();
            bool blnReturn = true;
            DateTime datFromDate = DateTime.MinValue;
            DateTime datToDate = DateTime.MinValue;
            try
            {
                m_dtbDetails = objMemoIssue.GetData(Val.DBDate(dtpFromDate.Text), Val.DBDate(dtpToDate.Text), Val.ToString(txtSearchMemoNo.Text), Val.ToInt(lueListParty.EditValue));

                if (m_dtbDetails.Rows.Count == 0)
                {
                    Global.Message("Data Not Found");
                    blnReturn = false;
                }

                grdMemoView.DataSource = m_dtbDetails;
                dgvMemoView.BestFitColumns();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            finally
            {
                objMemoIssue = null;
            }

            return blnReturn;
        }
        public void GetData(string MemoNo)
        {
            try
            {
                objMemoIssue = new MemoInvoice();

                m_dtbMemoIssueDetail = objMemoIssue.GetMemoData(GlobalDec.gEmployeeProperty.company_id, GlobalDec.gEmployeeProperty.branch_id, GlobalDec.gEmployeeProperty.location_id, GlobalDec.gEmployeeProperty.department_id, MemoNo);

                if (m_dtbMemoIssueDetail.Rows.Count > 0)
                {
                    //DialogResult result = MessageBox.Show("This Memo already issue Do You want to update data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                    //if (result == DialogResult.Yes)
                    //{
                    grdMemoDetails.DataSource = m_dtbMemoIssueDetail;
                    txtMemoNo.Text = Val.ToString(m_dtbMemoIssueDetail.Rows[0]["memo_no"]);
                    dtpMemoDate.Text = Val.DBDate(Val.ToString(m_dtbMemoIssueDetail.Rows[0]["memo_date"]));
                    lueParty.EditValue = Val.ToInt(m_dtbMemoIssueDetail.Rows[0]["party_id"]);
                    lueBroker.EditValue = Val.ToInt(m_dtbMemoIssueDetail.Rows[0]["broker_id"]);
                    lueDeliveryType.EditValue = Val.ToInt(m_dtbMemoIssueDetail.Rows[0]["delivery_type_id"]);
                    txtBrokerageAmt.Text = Val.ToDecimal(m_dtbMemoIssueDetail.Rows[0]["broker_amt"]).ToString();
                    txtBrokeragePer.Text = Val.ToDecimal(m_dtbMemoIssueDetail.Rows[0]["broker_per"]).ToString();
                    txtRemark.Text = Val.ToString(m_dtbMemoIssueDetail.Rows[0]["remarks"]);
                    txtSpecialRemark.Text = Val.ToString(m_dtbMemoIssueDetail.Rows[0]["special_remarks"]);
                    txtClientRemark.Text = Val.ToString(m_dtbMemoIssueDetail.Rows[0]["client_remarks"]);
                    txtPaymentRemark.Text = Val.ToString(m_dtbMemoIssueDetail.Rows[0]["payment_remarks"]);
                    lblMemoNo.Tag = Val.ToInt(m_dtbMemoIssueDetail.Rows[0]["memo_master_id"]);

                    txtTermDays.Text = Val.ToString(m_dtbMemoIssueDetail.Rows[0]["term_days"]);
                    dtpDueDate.Text = Val.DBDate(Val.ToString(m_dtbMemoIssueDetail.Rows[0]["due_date"]));

                    txtFinalTermDays.Text = Val.ToString(m_dtbMemoIssueDetail.Rows[0]["final_term_days"]);
                    dtpFinalDueDate.Text = Val.DBDate(Val.ToString(m_dtbMemoIssueDetail.Rows[0]["final_due_date"]));

                    txtExchangeRate.Text = Val.ToString(m_dtbMemoIssueDetail.Rows[0]["exchange_rate"]);
                    lueCurrency.Text = Val.ToString(m_dtbMemoIssueDetail.Rows[0]["currency_type"]);
                    lueSeller.EditValue = Val.ToInt(m_dtbMemoIssueDetail.Rows[0]["seller_id"]);
                    txtStatusDays.Text = Val.ToString(m_dtbMemoIssueDetail.Rows[0]["status_days"]);
                }
                else
                {
                    if (!GenerateMemoIssueDetails())
                    {
                    }
                }

                dgvMemoDetails.BestFitColumns();

            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        private void FillInspToMemo()
        {
            try
            {
                objSaleInvoice = new SaleInvoice();
                if (m_dtbInspectionData.Rows.Count > 0)
                {
                    DataTable InspDet = new DataTable();
                    InspDet.Columns.Add("memo_id");
                    InspDet.Columns.Add("memo_no");
                    InspDet.Columns.Add("memo_date");
                    InspDet.Columns.Add("assort_id");
                    InspDet.Columns.Add("assort_name");
                    InspDet.Columns.Add("sieve_id");
                    InspDet.Columns.Add("sieve_name");
                    InspDet.Columns.Add("sub_sieve_id");
                    InspDet.Columns.Add("sub_sieve_name");
                    InspDet.Columns.Add("pcs");
                    InspDet.Columns.Add("carat");
                    InspDet.Columns.Add("rate");
                    InspDet.Columns.Add("amount");
                    InspDet.Columns.Add("discount_per");
                    InspDet.Columns.Add("discount_amt");
                    InspDet.Columns.Add("net_amount");
                    InspDet.Columns.Add("discount");
                    InspDet.Columns.Add("remarks");
                    InspDet.Columns.Add("diff_carat");
                    InspDet.Columns.Add("diff_pcs");
                    InspDet.Columns.Add("flag");
                    InspDet.Columns.Add("sr_no");
                    InspDet.Columns.Add("old_assort_id");
                    InspDet.Columns.Add("old_sieve_id");
                    InspDet.Columns.Add("old_sub_sieve_id");
                    InspDet.Columns.Add("old_assort_name");
                    InspDet.Columns.Add("old_sieve_name");
                    InspDet.Columns.Add("old_sub_sieve_name");
                    InspDet.Columns.Add("current_rate");
                    InspDet.Columns.Add("current_amount");

                    InspDet.Columns.Add("rej_pcs");
                    InspDet.Columns.Add("rej_carat");
                    InspDet.Columns.Add("rej_per");

                    InspDet.Columns.Add("purchase_rate");
                    InspDet.Columns.Add("purchase_amount");

                    //SaleDet.Columns.Add("demand_master_id");
                    //SaleDet.Columns.Add("memo_master_id");

                    lueParty.EditValue = Convert.ToInt32(m_dtbInspectionData.Rows[0]["party_id"]);
                    lueBroker.EditValue = Convert.ToInt32(m_dtbInspectionData.Rows[0]["broker_id"]);
                    lueInspectionNo.Text = Convert.ToString(m_dtbInspectionData.Rows[0]["inspection_no"]);
                    //txtInspectionNo.Text = Convert.ToString(m_dtbInspectionData.Rows[0]["inspection_no"]);
                    //txtDiscountPer.Text = Convert.ToString(m_dtbInspectionData.Rows[0]["discount_per"]);
                    //txtDiscountAmt.Text = Convert.ToString(m_dtbInspectionData.Rows[0]["discount_amt"]);
                    txtTermDays.Text = Convert.ToString(m_dtbInspectionData.Rows[0]["term_days"]);
                    dtpDueDate.Text = Val.DBDate(Val.ToString(m_dtbInspectionData.Rows[0]["due_date"]));

                    txtFinalTermDays.Text = Convert.ToString(m_dtbInspectionData.Rows[0]["final_term_days"]);
                    dtpFinalDueDate.Text = Val.DBDate(Val.ToString(m_dtbInspectionData.Rows[0]["final_due_date"]));

                    txtExchangeRate.Text = Val.ToString(m_dtbInspectionData.Rows[0]["exchange_rate"]);
                    lueCurrency.Text = Val.ToString(m_dtbInspectionData.Rows[0]["currency_type"]);
                    lueSeller.EditValue = Val.ToInt(m_dtbInspectionData.Rows[0]["seller_id"]);
                    lueDeliveryType.EditValue = Val.ToInt(m_dtbInspectionData.Rows[0]["delivery_type_id"]);

                    //lblMemoNo.Tag = Convert.ToInt32(m_dtbMemoData.Rows[0]["memo_master_id"]);
                    lueInspectionNo.EditValue = Convert.ToInt32(m_dtbInspectionData.Rows[0]["inspection_master_id"]);

                    //txtBrokeragePer.Text = Val.ToDecimal(m_dtbMemoData.Rows[0]["broker_per"]).ToString();
                    //txtBrokerageAmt.Text = Val.ToDecimal(m_dtbMemoData.Rows[0]["broker_amt"]).ToString();

                    int i = 0;
                    foreach (DataRow DRow in m_dtbInspectionData.Rows)
                    {
                        if (Convert.ToDecimal(DRow["return_carat"]) > 0)
                        {
                            InspDet.Rows.Add();
                            InspDet.Rows[i]["memo_id"] = Val.ToInt(0);
                            InspDet.Rows[i]["assort_id"] = Val.ToInt(DRow["assort_id"]);
                            InspDet.Rows[i]["sieve_id"] = Val.ToInt(DRow["sieve_id"]);
                            InspDet.Rows[i]["sub_sieve_id"] = Val.ToInt(DRow["sub_sieve_id"]);
                            InspDet.Rows[i]["assort_name"] = Val.ToString(DRow["assort_name"]);
                            InspDet.Rows[i]["sieve_name"] = Val.ToString(DRow["sieve_name"]);
                            InspDet.Rows[i]["sub_sieve_name"] = Val.ToString(DRow["sub_sieve_name"]);
                            InspDet.Rows[i]["pcs"] = Val.ToInt(DRow["return_pcs"]);
                            InspDet.Rows[i]["carat"] = Val.ToDecimal(DRow["return_carat"]);
                            InspDet.Rows[i]["rate"] = Val.ToDecimal(DRow["rate"]);
                            InspDet.Rows[i]["amount"] = Math.Round(Val.ToDecimal(DRow["return_carat"]) * Val.ToDecimal(DRow["rate"]), 2);
                            InspDet.Rows[i]["diff_carat"] = 0;
                            InspDet.Rows[i]["diff_pcs"] = 0;
                            InspDet.Rows[i]["flag"] = 0;
                            InspDet.Rows[i]["sr_no"] = i + 1;
                            InspDet.Rows[i]["old_assort_id"] = Val.ToInt(0);
                            InspDet.Rows[i]["old_sieve_id"] = Val.ToInt(0);
                            InspDet.Rows[i]["old_sub_sieve_id"] = Val.ToInt(0);
                            InspDet.Rows[i]["old_assort_name"] = Val.ToInt(0);
                            InspDet.Rows[i]["old_sieve_name"] = Val.ToInt(0);
                            InspDet.Rows[i]["old_sub_sieve_name"] = Val.ToInt(0);
                            InspDet.Rows[i]["current_rate"] = Val.ToDecimal(DRow["current_rate"]);
                            InspDet.Rows[i]["current_amount"] = Math.Round(Val.ToDecimal(DRow["return_carat"]) * Val.ToDecimal(DRow["current_rate"]), 2);
                            InspDet.Rows[i]["discount"] = 0;

                            InspDet.Rows[i]["rej_pcs"] = 0;
                            InspDet.Rows[i]["rej_carat"] = 0;
                            InspDet.Rows[i]["rej_per"] = 0;

                            //InspDet.Rows[i]["loss_carat"] = Val.ToDecimal(DRow["loss_carat"]);
                            //InspDet.Rows[i]["old_loss_carat"] = Val.ToDecimal(0);
                            //InspDet.Rows[i]["is_Insp"] = Val.ToInt(1);

                            InspDet.Rows[i]["purchase_rate"] = Val.ToDecimal(DRow["purchase_rate"]);
                            InspDet.Rows[i]["purchase_amount"] = Val.ToDecimal(DRow["purchase_amount"]);
                            i++;
                        }
                    }

                    grdMemoDetails.DataSource = InspDet;

                    ttlbMemoIssue.SelectedTabPage = tblMemoIssuedetail;
                    m_dtbMemoIssueDetail = InspDet;
                    txtMemoNo.Focus();

                    txtRejCarat.Enabled = false;

                    // m_IsValid = true;
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void GetCarat()
        {
            try
            {
                DataTable m_dtbStockCarat = new DataTable();
                m_dtbStockCarat = objSaleInvoice.GetStockCarat(GlobalDec.gEmployeeProperty.company_id, GlobalDec.gEmployeeProperty.branch_id, GlobalDec.gEmployeeProperty.location_id, GlobalDec.gEmployeeProperty.department_id, Val.ToInt(lueAssortName.EditValue), Val.ToInt(lueSieveName.EditValue));

                if (m_dtbStockCarat.Rows.Count > 0)
                {
                    numStockCarat = Val.ToDecimal(m_dtbStockCarat.Rows[0]["stock_carat"]);
                    lblBalCrt.Text = Val.ToString(Val.ToDecimal(numStockCarat));
                }
                else
                {
                    numStockCarat = 0;
                    lblBalCrt.Text = "0";
                }

                if (numStockCarat < Val.ToDecimal(txtCarat.Text))
                {
                    Global.Message("Please check enter carat more then stock carat  (Assorts : " + Val.ToString(lueAssortName.Text) + ") (Sieve : " + Val.ToString(lueSieveName.Text) + " ) ", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCarat.Focus();
                    this.Cursor = Cursors.Default;
                    return;
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                //blnReturn = false;
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
                            dgvMemoView.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvMemoView.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvMemoView.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvMemoView.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvMemoView.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvMemoView.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvMemoView.ExportToCsv(Filepath);
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

        private void txtFinalTermDays_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtpMemoDate.Text.Length <= 0 || dtpMemoDate.Text == "")
                {
                    txtFinalTermDays.Text = "";
                    dtpFinalDueDate.EditValue = null;
                }
                else
                {
                    Double Days = Val.ToDouble(txtFinalTermDays.Text);
                    DateTime Date = Convert.ToDateTime(dtpMemoDate.EditValue).AddDays(Val.ToDouble(Days));
                    dtpFinalDueDate.EditValue = Val.DBDate(Date.ToShortDateString());
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }

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

        private void lueEmployee_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void txtTermDays_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtpMemoDate.Text.Length <= 0 || dtpMemoDate.Text == "")
                {
                    txtTermDays.Text = "";
                    dtpDueDate.EditValue = null;
                }
                else
                {
                    Double Days = Val.ToDouble(txtTermDays.Text) + Val.ToDouble(dtpMemoDate.Text);
                    DateTime Date = Convert.ToDateTime(dtpMemoDate.EditValue).AddDays(Val.ToDouble(Days));
                    dtpDueDate.EditValue = Val.DBDate(Date.ToShortDateString());
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }

        private void txtMemoNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (chkActive.Checked == true)
                {
                    if (e.KeyChar == Convert.ToChar(Keys.Enter))
                    {
                        m_blncheck = true;
                        string MemoNo = Val.ToString(txtMemoNo.Text);
                        if (m_blncheck)
                            GetData(MemoNo);
                        m_blncheck = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }

        private void txtRejectionPer_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtRejectionPer_Validated(object sender, EventArgs e)
        {
            if (txtRejectionPer.Text != "")
            {
                txtRejCarat.Text = Val.ToString(Math.Round((Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRejectionPer.Text) / 100), 3));
            }
        }

        private void btnserch_Click(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToString(lueInspectionNo.Text) != "")
                {
                    objSaleInvoice = new SaleInvoice();
                    m_dtbInspectionData = new DataTable();
                    m_dtbInspectionData = objMemoIssue.SearchInspData(Val.ToString(lueInspectionNo.Text));

                    while (objMemoIssue.ISAlredySaveInspData(Val.ToInt(lueInspectionNo.EditValue)) == true)
                    {
                        Global.Message("this Inspection No already Saved please check Inspection No");
                        btnSave.Enabled = true;
                        return;
                    }

                    FillInspToMemo();

                }

            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }

        private void dtpMemoDate_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtpMemoDate.Text.Length <= 0 || dtpMemoDate.Text == "")
                {
                    txtTermDays.Text = "";
                    dtpDueDate.EditValue = null;
                    txtFinalTermDays.Text = "";
                    dtpFinalDueDate.EditValue = null;
                }
                else
                {
                    Double Days = Val.ToDouble(txtTermDays.Text) + Val.ToDouble(dtpMemoDate.Text);
                    DateTime Date = Convert.ToDateTime(dtpMemoDate.EditValue).AddDays(Val.ToDouble(Days));
                    dtpDueDate.EditValue = Val.DBDate(Date.ToShortDateString());

                    Double Days_Terms = Val.ToDouble(txtFinalTermDays.Text);
                    DateTime Date_Terms = Convert.ToDateTime(dtpMemoDate.EditValue).AddDays(Val.ToDouble(Days_Terms));
                    dtpFinalDueDate.EditValue = Val.DBDate(Date_Terms.ToShortDateString());
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }

        private void dgvMemoView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (Val.ToInt(dgvMemoView.GetRowCellValue(e.RowHandle, "flag_color")) == 1)
                {
                    e.Appearance.BackColor = Color.FromArgb(248, 210, 210);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to Delete data?", "Confirmation", MessageBoxButtons.YesNoCancel);
            if (result != DialogResult.Yes)
            {
                btnDelete.Enabled = true;
                return;
            }
            Memo_InvoiceProperty objMemoIssue_Property = new Memo_InvoiceProperty();
            MemoInvoice objMemoInvoice = new MemoInvoice();

            objMemoIssue_Property.memo_no = Val.ToString(txtMemoNo.Text);

            objMemoIssue_Property = objMemoIssue.Memo_Issue_Delete(objMemoIssue_Property);

            NewMemoNo = Val.ToString(objMemoIssue_Property.memo_no);

            if (NewMemoNo == "0")
            {
                Global.Message("Already Receive in This Memo No...So Don't have Delete in this Memo No.");
                return;
            }
            else
            {
                Global.Message("Memo No Deleted Successfully");
                ClearDetails();
            }
        }
    }
}