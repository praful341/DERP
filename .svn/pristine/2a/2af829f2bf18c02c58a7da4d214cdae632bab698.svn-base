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
    public partial class FrmInspectionIssue : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member
        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        Control _NextEnteredControl;
        private List<Control> _tabControls;
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        InspectionIssue objInspectionIssue;
        UserAuthentication objUserAuthentication;
        AssortMaster objAssort;
        SieveMaster objSieve;
        RateMaster objRate;
        SaleInvoice objSaleInvoice;
        DataTable DtControlSettings;
        DataTable m_dtbAssorts;
        DataTable m_dtbSieve;
        DataTable m_dtbInspectionIssueDetail;
        DataTable m_dtbMemoGetIssueDetail;
        DataTable m_dtbCurrency;
        DataTable m_dtbDetails;
        DataTable m_opDate;
        DataTable m_dtbStockCarat;
        DataSet m_dtbDS;
        DataTable m_dtbCurrencyType;
        DataTable m_dtbSeller;
        DataTable m_dtbInpectionData;

        int m_numForm_id;
        int m_inspection_id;
        int m_srno;
        int m_update_srno;
        int IntRes;
        int m_numCurrency_id;

        string NewInspectionNo;

        decimal m_numcarat;
        decimal m_numTotalCarats;
        decimal m_numTotalAmount;
        decimal m_numCurrentRate;
        decimal m_numSummRate;
        decimal numStockCarat;
        bool m_blnadd = new bool();
        bool m_blnsave = new bool();
        bool m_blncheck = new bool();
        string strInspectionMessage = string.Empty;
        #endregion

        #region Constructor
        public FrmInspectionIssue()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            _NextEnteredControl = new Control();
            _tabControls = new List<Control>();

            objInspectionIssue = new InspectionIssue();
            objUserAuthentication = new UserAuthentication();
            objAssort = new AssortMaster();
            objSieve = new SieveMaster();
            objRate = new RateMaster();
            objSaleInvoice = new SaleInvoice();
            DtControlSettings = new DataTable();
            m_dtbAssorts = new DataTable();
            m_dtbSieve = new DataTable();
            m_dtbInspectionIssueDetail = new DataTable();
            m_dtbMemoGetIssueDetail = new DataTable();
            m_dtbCurrency = new DataTable();
            m_dtbDetails = new DataTable();
            m_opDate = new DataTable();
            m_dtbStockCarat = new DataTable();
            m_dtbDS = new DataSet();
            m_dtbCurrencyType = new DataTable();
            m_dtbSeller = new DataTable();

            m_numForm_id = 0;
            m_inspection_id = 0;
            m_srno = 0;
            m_update_srno = 0;
            IntRes = 0;

            NewInspectionNo = "";

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
            objBOFormEvents.ObjToDispose.Add(objInspectionIssue);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }

        public void ShowForm_New(DataTable InpectionDt)
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

            m_dtbInpectionData = InpectionDt;

            this.Show();

            FillInpectionIssue();
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
                    ttlbInspectionIssue.SelectedTabPage = tblMemoIssuedetail;
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

                //Int64 Return_MemoId = objInspectionIssue.FindReturnMemoID(Val.ToString(txtInspectionNo.Text));

                //if (Return_MemoId > 0)
                //{
                //    Global.Message("Already Receive This Inspection No...");
                //    btnSave.Enabled = true;
                //    return;
                //}

                DialogResult result = MessageBox.Show("Do you want to save Inspection?", "Confirmation", MessageBoxButtons.YesNoCancel);
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
                if (txtInspectionNo.Text != "")
                {
                    objMemoIssue_Property.memo_no = Val.ToString(txtInspectionNo.Text);
                }
                else
                {
                    objMemoIssue_Property.memo_no = Val.ToString(NewInspectionNo);
                }

                m_dtbDetails = objInspectionIssue.GetPrintData(objMemoIssue_Property);

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

                int total_cnt = m_dtbDetails.Rows.Count;

                if (total_cnt == 1)
                {
                    m_dtbDetails.Rows.Add();
                    m_dtbDetails.Rows.Add();
                    m_dtbDetails.Rows.Add();
                    m_dtbDetails.Rows.Add();
                    m_dtbDetails.Rows.Add();
                    m_dtbDetails.Rows.Add();
                    m_dtbDetails.Rows.Add();
                }
                else if (total_cnt == 2)
                {
                    m_dtbDetails.Rows.Add();
                    m_dtbDetails.Rows.Add();
                    m_dtbDetails.Rows.Add();
                    m_dtbDetails.Rows.Add();
                    m_dtbDetails.Rows.Add();
                    m_dtbDetails.Rows.Add();
                }
                else if (total_cnt == 3)
                {
                    m_dtbDetails.Rows.Add();
                    m_dtbDetails.Rows.Add();
                    m_dtbDetails.Rows.Add();
                    m_dtbDetails.Rows.Add();
                    m_dtbDetails.Rows.Add();
                }
                else if (total_cnt == 4)
                {
                    m_dtbDetails.Rows.Add();
                    m_dtbDetails.Rows.Add();
                    m_dtbDetails.Rows.Add();
                    m_dtbDetails.Rows.Add();
                }
                else if (total_cnt == 5)
                {
                    m_dtbDetails.Rows.Add();
                    m_dtbDetails.Rows.Add();
                    m_dtbDetails.Rows.Add();
                }
                else if (total_cnt == 6)
                {
                    m_dtbDetails.Rows.Add();
                    m_dtbDetails.Rows.Add();
                }
                else if (total_cnt == 7)
                {
                    m_dtbDetails.Rows.Add();
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
                    DataTable DTab_PartyMap = objInspectionIssue.GetData_Party_To_Broker_Map(Party_Id);

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

                    //txtRate.Text = Val.ToString(p_numNewStockRate + ((p_numNewStockRate / 100) * 20));

                    txtRate.Text = Val.ToString(p_numNewStockRate);
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
                    GetCarat();
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
                Inspection_IssueProperty objInspectionIssue_Property = new Inspection_IssueProperty();
                InspectionIssue objInspectionIssue = new InspectionIssue();
                try
                {
                    IntRes = 0;
                    int IntCounter = 0;
                    int Count = 0;
                    int TotalCount = m_dtbInspectionIssueDetail.Rows.Count;
                    int IntMemoMasterID = 0;
                    NewInspectionNo = "";


                    IntMemoMasterID = Val.ToInt(objInspectionIssue.FindMaxMemoMasterID());
                    objInspectionIssue_Property.inspection_master_id = IntMemoMasterID;


                    foreach (DataRow drw in m_dtbInspectionIssueDetail.Rows)
                    {
                        objInspectionIssue_Property.inspection_id = Val.ToInt(drw["inspection_id"]);
                        objInspectionIssue_Property.inspection_no = Val.ToString(txtInspectionNo.Text);
                        objInspectionIssue_Property.demand_no = Val.ToString(txtDemandNo.Text);

                        //if (txtMemoNo.Text != "")
                        //{
                        //    objMemoIssue_Property.memo_no = Val.ToString(txtMemoNo.Text);
                        //}
                        //else
                        //{
                        //    objMemoIssue_Property.memo_no = Val.ToString(NewMemoNo);
                        //}
                        //objMemoIssue_Property.memo_no = Val.ToString(NewMemoNo);

                        objInspectionIssue_Property.company_id = Val.ToInt(GlobalDec.gEmployeeProperty.company_id);
                        objInspectionIssue_Property.branch_id = Val.ToInt(GlobalDec.gEmployeeProperty.branch_id);
                        objInspectionIssue_Property.location_id = Val.ToInt(GlobalDec.gEmployeeProperty.location_id);
                        objInspectionIssue_Property.department_id = Val.ToInt(GlobalDec.gEmployeeProperty.department_id);

                        objInspectionIssue_Property.inspection_date = Val.DBDate(dtpInspectionDate.Text);
                        objInspectionIssue_Property.delivery_type_id = Val.ToInt(lueDeliveryType.EditValue);
                        objInspectionIssue_Property.remarks = Val.ToString(txtRemark.Text);

                        objInspectionIssue_Property.form_id = m_numForm_id;

                        objInspectionIssue_Property.term_days = Val.ToInt(txtTermDays.Text);
                        objInspectionIssue_Property.due_date = Val.DBDate(dtpDueDate.Text);
                        objInspectionIssue_Property.final_days = Val.ToInt(txtFinalTermDays.Text);
                        objInspectionIssue_Property.final_due_date = Val.DBDate(dtpFinalDueDate.Text);

                        objInspectionIssue_Property.Party_Id = Val.ToInt(lueParty.EditValue);
                        objInspectionIssue_Property.Broker_Id = Val.ToInt(lueBroker.EditValue);

                        objInspectionIssue_Property.Special_Remark = Val.ToString(txtSpecialRemark.Text);
                        objInspectionIssue_Property.Client_Remark = Val.ToString(txtClientRemark.Text);
                        objInspectionIssue_Property.Payment_Remark = Val.ToString(txtPaymentRemark.Text);

                        objInspectionIssue_Property.Brokerage_Per = Val.ToDecimal(txtBrokeragePer.Text);
                        objInspectionIssue_Property.Brokerage_Amt = Val.ToDecimal(txtBrokerageAmt.Text);

                        objInspectionIssue_Property.assort_id = Val.ToInt(drw["assort_id"]);
                        objInspectionIssue_Property.sieve_id = Val.ToInt(drw["sieve_id"]);
                        objInspectionIssue_Property.sub_sieve_id = Val.ToInt(drw["sub_sieve_id"]);
                        objInspectionIssue_Property.Pcs = Val.ToInt(drw["pcs"]);
                        objInspectionIssue_Property.carat = Val.ToDecimal(drw["carat"]);
                        objInspectionIssue_Property.rate = Val.ToDecimal(drw["rate"]);
                        objInspectionIssue_Property.amount = Val.ToDecimal(drw["amount"]);
                        objInspectionIssue_Property.discount_per = Val.ToDecimal(drw["discount_per"]);
                        objInspectionIssue_Property.discount_amount = Val.ToDecimal(drw["discount_amt"]);
                        objInspectionIssue_Property.Net_Amt = Val.ToDecimal(drw["net_amount"]);

                        objInspectionIssue_Property.diff_carat = Val.ToDecimal(drw["diff_carat"]);
                        objInspectionIssue_Property.diff_pcs = Val.ToInt(drw["diff_pcs"]);
                        objInspectionIssue_Property.flag = Val.ToInt(drw["flag"]);
                        objInspectionIssue_Property.old_assort_id = Val.ToInt(drw["old_assort_id"]);
                        objInspectionIssue_Property.old_sieve_id = Val.ToInt(drw["old_sieve_id"]);
                        objInspectionIssue_Property.old_sub_sieve_id = Val.ToInt(drw["old_sub_sieve_id"]);
                        objInspectionIssue_Property.current_rate = Val.ToDecimal(drw["current_rate"]);
                        objInspectionIssue_Property.current_amount = Val.ToDecimal(drw["current_amount"]);

                        objInspectionIssue_Property.exchange_rate = Val.ToDecimal(txtExchangeRate.Text);
                        objInspectionIssue_Property.currency_type = Val.ToString(lueCurrency.Text);
                        objInspectionIssue_Property.seller_id = Val.ToInt(lueSeller.EditValue);
                        objInspectionIssue_Property.purchase_rate = Val.ToDecimal(drw["purchase_rate"]);
                        objInspectionIssue_Property.purchase_amount = Val.ToDecimal(drw["purchase_amount"]);

                        objInspectionIssue_Property.rej_pcs = Val.ToInt(drw["rej_pcs"]);
                        objInspectionIssue_Property.rej_per = Val.ToDecimal(drw["rej_per"]);
                        objInspectionIssue_Property.rej_carat = Val.ToDecimal(drw["rej_carat"]);

                        objInspectionIssue_Property.demand_id = Val.ToInt64(drw["demand_id"]);
                        objInspectionIssue_Property.check_issue_flag = Val.ToBoolean(drw["flag_issue_check"]);

                        //IntRes = objMemoIssue.Save(objMemoIssue_Property, m_dtbMemoIssueDetail);
                        //IntRes = objMemoIssue.Save(objMemoIssue_Property);
                        objInspectionIssue_Property = objInspectionIssue.Save(objInspectionIssue_Property, DLL.GlobalDec.EnumTran.Continue, Conn);

                        NewInspectionNo = Val.ToString(objInspectionIssue_Property.inspection_no);

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
                    objInspectionIssue_Property = null;
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
                        Global.Message("Inspection Data Save Successfully.... Your Inspection No is : " + NewInspectionNo);
                        btnPrint_Click(null, null);

                    }
                    else
                    {
                        Global.Confirm("Inspection Data Save Successfully.... Your Inspection No is : " + NewInspectionNo);
                        btnPrint_Click(null, null);
                    }
                }
                else
                {
                    Global.Confirm("Error In Inspection");
                    txtInspectionNo.Focus();
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
                objInspectionIssue = new InspectionIssue();
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvInspectionView.GetDataRow(e.RowHandle);
                        txtInspectionNo.Text = Val.ToString(Drow["inspection_no"]);
                        ttlbInspectionIssue.SelectedTabPage = tblMemoIssuedetail;
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
                objInspectionIssue = new InspectionIssue();
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvInspectionDetails.GetDataRow(e.RowHandle);
                        btnAdd.Text = "&Update";
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt32(Drow["inspection_id"]);
                        lueSieveName.Text = Val.ToString(Drow["sieve_name"]);
                        lueSieveName.Tag = Val.ToInt64(Drow["sieve_id"]);
                        lueSubSieveName.Tag = Val.ToInt64(Drow["sub_sieve_id"]);
                        lueSubSieveName.Text = Val.ToString(Drow["sub_sieve_name"]);
                        lueAssortName.Text = Val.ToString(Drow["assort_name"]);
                        lueAssortName.Tag = Val.ToInt64(Drow["assort_id"]);
                        txtPcs.Text = Val.ToString(Drow["pcs"]);
                        txtCarat.Text = Val.ToString(Drow["carat"]);
                        txtDiscPer.Text = Val.ToString(Drow["discount_per"]);
                        txtDiscAmt.Text = Val.ToString(Drow["discount_amt"]);
                        txtRate.Text = Val.ToString(Drow["rate"]);
                        txtAmount.Text = Val.ToString(Drow["amount"]);
                        m_numcarat = Val.ToDecimal(Drow["carat"]);
                        m_inspection_id = Val.ToInt(Drow["inspection_id"]);
                        m_update_srno = Val.ToInt(Drow["sr_no"]);
                        txtPurchaseRate.Text = Val.ToString(Drow["purchase_rate"]);
                        txtPurchaseAmount.Text = Val.ToString(Drow["purchase_amount"]);

                        lueAssortName.Focus();

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
                m_numTotalCarats = Math.Round(Val.ToDecimal(clmICarat.SummaryItem.SummaryValue), 3, MidpointRounding.AwayFromZero);

                if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == "rate")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                        m_numTotalAmount = 0;
                    else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                        m_numTotalAmount += (Val.ToDecimal(e.GetValue("issue_carat")) * Val.ToDecimal(e.GetValue("rate")));
                    else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    {
                        if (m_numTotalAmount > 0 && m_numTotalCarats > 0)
                            e.TotalValue = Math.Round((m_numTotalAmount / m_numTotalCarats), 0, MidpointRounding.AwayFromZero);
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

                //m_opDate = Global.GetDate();
                dtpFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
                //dtpFromDate.EditValue = Val.DBDate(m_opDate.Rows[0]["opening_date"].ToString());
                dtpFromDate.EditValue = DateTime.Now;

                dtpToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpToDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpToDate.EditValue = DateTime.Now;

                dtpInspectionDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpInspectionDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpInspectionDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpInspectionDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpInspectionDate.EditValue = DateTime.Now;
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

                    //if (numStockCarat < (Val.ToDecimal(txtCarat.Text)))
                    //{
                    //    Global.Message("Please check enter carat more then stock carat", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    txtCarat.Focus();
                    //    blnReturn = false;
                    //    return blnReturn;
                    //}
                    m_srno = dgvInspectionDetails.RowCount;
                    DataRow[] dr = m_dtbInspectionIssueDetail.Select("sieve_id = " + Val.ToInt(lueSieveName.EditValue) + " AND assort_id = " + Val.ToInt(lueAssortName.EditValue));
                    if (dr.Count() == 1)
                    {
                        Global.Message("Record already exists in grid", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lueAssortName.Focus();
                        blnReturn = false;
                        return blnReturn;
                    }

                    DataRow drwNew = m_dtbInspectionIssueDetail.NewRow();
                    decimal numCarat = Val.ToDecimal(txtCarat.Text);
                    decimal numRate = Val.ToDecimal(txtRate.Text);
                    decimal numAmount = Val.ToDecimal(txtAmount.Text);
                    decimal numDiscPer = Val.ToDecimal(txtDiscPer.Text);
                    decimal numDiscAmount = Val.ToDecimal(txtDiscAmt.Text);
                    decimal numNetAmount = Val.ToDecimal(txtNetAmount.Text);

                    decimal numpurRate = Val.ToDecimal(txtPurchaseRate.Text);
                    decimal numpurAmount = Val.ToDecimal(txtPurchaseAmount.Text);

                    int numPcs = Val.ToInt(txtPcs.Text);

                    drwNew["inspection_no"] = Val.ToString(txtInspectionNo.Text);
                    drwNew["inspection_id"] = Val.ToInt(0);
                    drwNew["inspection_date"] = Val.ToString(dtpInspectionDate.Text);
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
                    m_dtbInspectionIssueDetail.Rows.Add(drwNew);

                    txtBrokeragePer_EditValueChanged_1(null, null);

                    dgvInspectionDetails.MoveLast();

                    //DataView dv = m_dtbMemoIssueDetail.DefaultView;
                    //dv.Sort = "sr_no desc";
                    //DataTable sortedDT = dv.ToTable();
                }
                else if (btnAdd.Text == "&Update")
                {
                    //DataRow[] dr = m_dtbMemoIssueDetail.Select("sieve_id = " + Val.ToInt(lueSieveName.EditValue) + " AND assort_id = " + Val.ToInt(lueAssortName.EditValue));
                    //if (dr.Count() == 1)
                    //{
                    //    Global.Message("Record already exists in grid", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    lueAssortName.Focus();
                    //    blnReturn = false;
                    //    return blnReturn;
                    //}

                    if (m_dtbInspectionIssueDetail.Select("assort_id ='" + Val.ToInt(lueAssortName.EditValue) + "' AND sieve_id ='" + Val.ToInt(lueSieveName.EditValue) + "'").Length > 0)
                    {
                        for (int i = 0; i < m_dtbInspectionIssueDetail.Rows.Count; i++)
                        {
                            if (m_dtbInspectionIssueDetail.Select("inspection_id ='" + m_inspection_id + "'AND sr_no = '" + m_update_srno + "'").Length > 0)
                            {
                                if (m_dtbInspectionIssueDetail.Rows[m_update_srno - 1]["inspection_id"].ToString() == m_inspection_id.ToString())
                                {
                                    m_dtbInspectionIssueDetail.Rows[m_update_srno - 1]["diff_carat"] = m_dtbInspectionIssueDetail.Rows[i]["carat"];
                                    m_dtbInspectionIssueDetail.Rows[m_update_srno - 1]["diff_pcs"] = m_dtbInspectionIssueDetail.Rows[i]["pcs"];
                                    m_dtbInspectionIssueDetail.Rows[m_update_srno - 1]["carat"] = Val.ToDecimal(txtCarat.Text).ToString();
                                    m_dtbInspectionIssueDetail.Rows[m_update_srno - 1]["pcs"] = Val.ToInt(txtPcs.Text);
                                    m_dtbInspectionIssueDetail.Rows[m_update_srno - 1]["rate"] = Val.ToDecimal(txtRate.Text);
                                    m_dtbInspectionIssueDetail.Rows[m_update_srno - 1]["flag"] = 1;
                                    // Add By Praful On 13082020
                                    m_dtbInspectionIssueDetail.Rows[m_update_srno - 1]["sieve_id"] = Val.ToInt(lueSieveName.EditValue);
                                    m_dtbInspectionIssueDetail.Rows[m_update_srno - 1]["sub_sieve_id"] = Val.ToInt(lueSubSieveName.EditValue);
                                    m_dtbInspectionIssueDetail.Rows[m_update_srno - 1]["sieve_name"] = Val.ToString(lueSieveName.Text);
                                    m_dtbInspectionIssueDetail.Rows[m_update_srno - 1]["sub_sieve_name"] = Val.ToString(lueSubSieveName.Text);
                                    // End
                                    m_dtbInspectionIssueDetail.Rows[m_update_srno - 1]["discount_per"] = Val.ToDecimal(txtDiscPer.Text);
                                    m_dtbInspectionIssueDetail.Rows[m_update_srno - 1]["discount_amt"] = Val.ToDecimal(txtDiscAmt.Text);
                                    m_dtbInspectionIssueDetail.Rows[m_update_srno - 1]["net_amount"] = Val.ToDecimal(txtNetAmount.Text);
                                    m_dtbInspectionIssueDetail.Rows[m_update_srno - 1]["amount"] = Math.Round(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text), 3);

                                    m_dtbInspectionIssueDetail.Rows[m_update_srno - 1]["purchase_rate"] = Val.ToDecimal(txtPurchaseRate.Text);
                                    m_dtbInspectionIssueDetail.Rows[m_update_srno - 1]["purchase_amount"] = Val.ToDecimal(txtPurchaseAmount.Text);

                                    m_dtbInspectionIssueDetail.Rows[m_update_srno - 1]["rej_pcs"] = Val.ToInt(txtRejPcs.Text);
                                    m_dtbInspectionIssueDetail.Rows[m_update_srno - 1]["rej_per"] = Val.ToDecimal(txtRejectionPer.Text);
                                    m_dtbInspectionIssueDetail.Rows[m_update_srno - 1]["rej_carat"] = Val.ToDecimal(txtRejCarat.Text);

                                    break;
                                }
                            }
                        }
                        btnAdd.Text = "&Add";
                    }
                    else
                    {
                        for (int i = 0; i < m_dtbInspectionIssueDetail.Rows.Count; i++)
                        {
                            if (m_dtbInspectionIssueDetail.Select("inspection_id ='" + m_inspection_id + "' AND sr_no = '" + m_update_srno + "'").Length > 0)
                            {
                                if (m_dtbInspectionIssueDetail.Rows[m_update_srno - 1]["inspection_id"].ToString() == m_inspection_id.ToString())
                                {
                                    m_dtbInspectionIssueDetail.Rows[m_update_srno - 1]["diff_carat"] = m_dtbInspectionIssueDetail.Rows[i]["carat"];
                                    m_dtbInspectionIssueDetail.Rows[m_update_srno - 1]["diff_pcs"] = m_dtbInspectionIssueDetail.Rows[i]["pcs"];
                                    m_dtbInspectionIssueDetail.Rows[m_update_srno - 1]["carat"] = Val.ToDecimal(txtCarat.Text).ToString();
                                    m_dtbInspectionIssueDetail.Rows[m_update_srno - 1]["pcs"] = Val.ToInt(txtPcs.Text);
                                    m_dtbInspectionIssueDetail.Rows[m_update_srno - 1]["rate"] = Val.ToDecimal(txtRate.Text);
                                    m_dtbInspectionIssueDetail.Rows[m_update_srno - 1]["flag"] = 1;
                                    m_dtbInspectionIssueDetail.Rows[m_update_srno - 1]["assort_id"] = Val.ToInt(lueAssortName.EditValue);
                                    m_dtbInspectionIssueDetail.Rows[m_update_srno - 1]["sieve_id"] = Val.ToInt(lueSieveName.EditValue);
                                    m_dtbInspectionIssueDetail.Rows[m_update_srno - 1]["sub_sieve_id"] = Val.ToInt(lueSubSieveName.EditValue);
                                    m_dtbInspectionIssueDetail.Rows[m_update_srno - 1]["assort_name"] = Val.ToString(lueAssortName.Text);
                                    m_dtbInspectionIssueDetail.Rows[m_update_srno - 1]["sieve_name"] = Val.ToString(lueSieveName.Text);
                                    m_dtbInspectionIssueDetail.Rows[m_update_srno - 1]["sub_sieve_name"] = Val.ToString(lueSubSieveName.Text);

                                    m_dtbInspectionIssueDetail.Rows[m_update_srno - 1]["discount_per"] = Val.ToDecimal(txtDiscPer.Text);
                                    m_dtbInspectionIssueDetail.Rows[m_update_srno - 1]["discount_amt"] = Val.ToDecimal(txtDiscAmt.Text);
                                    m_dtbInspectionIssueDetail.Rows[m_update_srno - 1]["net_amount"] = Val.ToDecimal(txtNetAmount.Text);

                                    m_dtbInspectionIssueDetail.Rows[m_update_srno - 1]["amount"] = Math.Round(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text), 3);

                                    m_dtbInspectionIssueDetail.Rows[m_update_srno - 1]["purchase_rate"] = Val.ToDecimal(txtPurchaseRate.Text);
                                    m_dtbInspectionIssueDetail.Rows[m_update_srno - 1]["purchase_amount"] = Val.ToDecimal(txtPurchaseAmount.Text);

                                    m_dtbInspectionIssueDetail.Rows[m_update_srno - 1]["rej_pcs"] = Val.ToInt(txtRejPcs.Text);
                                    m_dtbInspectionIssueDetail.Rows[m_update_srno - 1]["rej_per"] = Val.ToDecimal(txtRejectionPer.Text);
                                    m_dtbInspectionIssueDetail.Rows[m_update_srno - 1]["rej_carat"] = Val.ToDecimal(txtRejCarat.Text);

                                }
                            }
                        }
                        btnAdd.Text = "&Add";
                    }
                    dgvInspectionDetails.MoveLast();
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
                    if (txtInspectionNo.Text == string.Empty)
                    {
                        lstError.Add(new ListError(12, "Inspection No"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtInspectionNo.Focus();
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
                    if (m_dtbInspectionIssueDetail.Rows.Count == 0)
                    {
                        lstError.Add(new ListError(22, "Record"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                        }
                    }
                    var result = DateTime.Compare(Convert.ToDateTime(dtpInspectionDate.Text), DateTime.Today);
                    if (result > 0)
                    {
                        lstError.Add(new ListError(5, "Inspection Date Not Be Greater Than Today Date"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            dtpInspectionDate.Focus();
                        }
                    }

                    foreach (DataRow Drw in m_dtbInspectionIssueDetail.Rows)
                    {
                        DataTable m_dtbStockCarat = new DataTable();

                        m_dtbStockCarat = objInspectionIssue.GetStockCarat(GlobalDec.gEmployeeProperty.company_id, GlobalDec.gEmployeeProperty.branch_id, GlobalDec.gEmployeeProperty.location_id, GlobalDec.gEmployeeProperty.department_id, Val.ToInt(Drw["assort_id"]), Val.ToInt(Drw["sieve_id"]));

                        if (m_dtbStockCarat.Rows.Count > 0)
                        {
                            numStockCarat = Val.ToDecimal(m_dtbStockCarat.Rows[0]["cl_carat"]);
                        }
                        else
                        {
                            numStockCarat = 0;
                        }

                        //if (numStockCarat < Val.ToDecimal(Drw["carat"]))
                        //{
                        //    Global.Message("Please check enter carat more then stock carat  (Assorts : " + Val.ToString(Drw["assort_name"]) + ") (Sieve : " + Val.ToString(Drw["sieve_name"]) + " ) ", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    txtCarat.Focus();
                        //    this.Cursor = Cursors.Default;
                        //    return false;
                        //}

                        if (Val.ToDouble(Drw["carat"]) <= 0 && Val.ToBoolean(Drw["flag_issue_check"]).ToString() == "True")
                        {
                            Global.Message(" Please check enter carat ", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCarat.Focus();
                            this.Cursor = Cursors.Default;
                            return false;
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
                    //if (lueSubSieveName.Text == "")
                    //{
                    //    lstError.Add(new ListError(13, "Sub Sieve"));
                    //    if (!blnFocus)
                    //    {
                    //        blnFocus = true;
                    //        lueSubSieveName.Focus();
                    //    }
                    //}

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

                    //if (Val.ToDouble(txtPurchaseRate.Text) == 0)
                    //{
                    //    lstError.Add(new ListError(12, "Purchase Rate"));
                    //    if (!blnFocus)
                    //    {
                    //        blnFocus = true;
                    //        txtPurchaseRate.Focus();
                    //    }
                    //}

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
                lueParty.EditValue = System.DBNull.Value;
                lueDeliveryType.EditValue = System.DBNull.Value;
                txtInspectionNo.Text = string.Empty;
                lueBroker.EditValue = System.DBNull.Value;
                lueDeliveryType.EditValue = Val.ToInt32(GlobalDec.gEmployeeProperty.delivery_type_id);

                //m_opDate = Global.GetDate();
                dtpFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
                //dtpFromDate.EditValue = Val.DBDate(m_opDate.Rows[0]["opening_date"].ToString());
                dtpFromDate.EditValue = DateTime.Now;

                dtpInspectionDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpInspectionDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpInspectionDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpInspectionDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpInspectionDate.EditValue = DateTime.Now;

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

                txtRemark.Text = string.Empty;
                txtSpecialRemark.Text = string.Empty;
                txtPaymentRemark.Text = string.Empty;
                txtClientRemark.Text = string.Empty;
                txtInspectionNo.Focus();
                lblMemoNo.Tag = null;
                lblBalCrt.Text = string.Empty;
                lueCurrency.EditValue = Val.ToString(GlobalDec.gEmployeeProperty.currency_id);
                lueCurrency.ReadOnly = false;

                txtDemandNo.Text = string.Empty;

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
                if (m_dtbInspectionIssueDetail.Rows.Count > 0)
                    m_dtbInspectionIssueDetail.Rows.Clear();

                m_dtbInspectionIssueDetail = new DataTable();
                m_dtbInspectionIssueDetail.Columns.Add("sr_no", typeof(int));
                m_dtbInspectionIssueDetail.Columns.Add("inspection_id", typeof(int));
                m_dtbInspectionIssueDetail.Columns.Add("inspection_no", typeof(string));
                m_dtbInspectionIssueDetail.Columns.Add("inspection_date", typeof(string));
                m_dtbInspectionIssueDetail.Columns.Add("assort_id", typeof(int));
                m_dtbInspectionIssueDetail.Columns.Add("assort_name", typeof(string));
                m_dtbInspectionIssueDetail.Columns.Add("sieve_id", typeof(int));
                m_dtbInspectionIssueDetail.Columns.Add("sieve_name", typeof(string));
                m_dtbInspectionIssueDetail.Columns.Add("sub_sieve_id", typeof(int));
                m_dtbInspectionIssueDetail.Columns.Add("sub_sieve_name", typeof(string));
                m_dtbInspectionIssueDetail.Columns.Add("pcs", typeof(int)).DefaultValue = 0;
                m_dtbInspectionIssueDetail.Columns.Add("carat", typeof(decimal)).DefaultValue = 0;
                m_dtbInspectionIssueDetail.Columns.Add("rate", typeof(decimal)).DefaultValue = 0;
                m_dtbInspectionIssueDetail.Columns.Add("amount", typeof(decimal)).DefaultValue = 0;
                m_dtbInspectionIssueDetail.Columns.Add("discount_per", typeof(decimal)).DefaultValue = 0;
                m_dtbInspectionIssueDetail.Columns.Add("discount_amt", typeof(decimal)).DefaultValue = 0;
                m_dtbInspectionIssueDetail.Columns.Add("net_amount", typeof(decimal)).DefaultValue = 0;
                m_dtbInspectionIssueDetail.Columns.Add("discount", typeof(decimal)).DefaultValue = 0;
                m_dtbInspectionIssueDetail.Columns.Add("remarks", typeof(string));
                m_dtbInspectionIssueDetail.Columns.Add("diff_pcs", typeof(int)).DefaultValue = 0;
                m_dtbInspectionIssueDetail.Columns.Add("diff_carat", typeof(decimal));
                m_dtbInspectionIssueDetail.Columns.Add("flag", typeof(int)).DefaultValue = 0;
                m_dtbInspectionIssueDetail.Columns.Add("old_assort_id", typeof(int));
                m_dtbInspectionIssueDetail.Columns.Add("old_sieve_id", typeof(int));
                m_dtbInspectionIssueDetail.Columns.Add("old_sub_sieve_id", typeof(int));
                m_dtbInspectionIssueDetail.Columns.Add("old_assort_name", typeof(string));
                m_dtbInspectionIssueDetail.Columns.Add("old_sieve_name", typeof(string));
                m_dtbInspectionIssueDetail.Columns.Add("old_sub_sieve_name", typeof(string));

                m_dtbInspectionIssueDetail.Columns.Add("current_rate", typeof(decimal));
                m_dtbInspectionIssueDetail.Columns.Add("current_amount", typeof(decimal));

                m_dtbInspectionIssueDetail.Columns.Add("purchase_rate", typeof(decimal)).DefaultValue = 0;
                m_dtbInspectionIssueDetail.Columns.Add("purchase_amount", typeof(decimal)).DefaultValue = 0;

                m_dtbInspectionIssueDetail.Columns.Add("rej_pcs", typeof(int)).DefaultValue = 0;
                m_dtbInspectionIssueDetail.Columns.Add("rej_per", typeof(decimal)).DefaultValue = 0;
                m_dtbInspectionIssueDetail.Columns.Add("rej_carat", typeof(decimal)).DefaultValue = 0;

                grdInspectionDetails.DataSource = m_dtbInspectionIssueDetail;
                grdInspectionDetails.Refresh();
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
            objInspectionIssue = new InspectionIssue();
            bool blnReturn = true;
            DateTime datFromDate = DateTime.MinValue;
            DateTime datToDate = DateTime.MinValue;
            try
            {
                m_dtbDetails = objInspectionIssue.GetData(Val.DBDate(dtpFromDate.Text), Val.DBDate(dtpToDate.Text), Val.ToString(txtSearchMemoNo.Text), Val.ToInt(lueListParty.EditValue));

                if (m_dtbDetails.Rows.Count == 0)
                {
                    Global.Message("Data Not Found");
                    blnReturn = false;
                }

                grdInspectionView.DataSource = m_dtbDetails;
                dgvInspectionView.BestFitColumns();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            finally
            {
                objInspectionIssue = null;
            }

            return blnReturn;
        }
        public void GetData(string MemoNo)
        {
            try
            {
                objInspectionIssue = new InspectionIssue();

                m_dtbInspectionIssueDetail = objInspectionIssue.GetMemoData(GlobalDec.gEmployeeProperty.company_id, GlobalDec.gEmployeeProperty.branch_id, GlobalDec.gEmployeeProperty.location_id, GlobalDec.gEmployeeProperty.department_id, MemoNo);

                if (m_dtbInspectionIssueDetail.Rows.Count > 0)
                {
                    //DialogResult result = MessageBox.Show("This Memo already issue Do You want to update data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                    //if (result == DialogResult.Yes)
                    //{
                    grdInspectionDetails.DataSource = m_dtbInspectionIssueDetail;
                    txtInspectionNo.Text = Val.ToString(m_dtbInspectionIssueDetail.Rows[0]["inspection_no"]);
                    dtpInspectionDate.Text = Val.DBDate(Val.ToString(m_dtbInspectionIssueDetail.Rows[0]["inspection_date"]));
                    lueParty.EditValue = Val.ToInt(m_dtbInspectionIssueDetail.Rows[0]["party_id"]);
                    lueBroker.EditValue = Val.ToInt(m_dtbInspectionIssueDetail.Rows[0]["broker_id"]);
                    lueDeliveryType.EditValue = Val.ToInt(m_dtbInspectionIssueDetail.Rows[0]["delivery_type_id"]);
                    txtBrokerageAmt.Text = Val.ToDecimal(m_dtbInspectionIssueDetail.Rows[0]["broker_amt"]).ToString();
                    txtBrokeragePer.Text = Val.ToDecimal(m_dtbInspectionIssueDetail.Rows[0]["broker_per"]).ToString();
                    txtRemark.Text = Val.ToString(m_dtbInspectionIssueDetail.Rows[0]["remarks"]);
                    txtSpecialRemark.Text = Val.ToString(m_dtbInspectionIssueDetail.Rows[0]["special_remarks"]);
                    txtClientRemark.Text = Val.ToString(m_dtbInspectionIssueDetail.Rows[0]["client_remarks"]);
                    txtPaymentRemark.Text = Val.ToString(m_dtbInspectionIssueDetail.Rows[0]["payment_remarks"]);
                    lblMemoNo.Tag = Val.ToInt(m_dtbInspectionIssueDetail.Rows[0]["inspection_master_id"]);

                    txtTermDays.Text = Val.ToString(m_dtbInspectionIssueDetail.Rows[0]["term_days"]);
                    dtpDueDate.Text = Val.DBDate(Val.ToString(m_dtbInspectionIssueDetail.Rows[0]["due_date"]));

                    txtFinalTermDays.Text = Val.ToString(m_dtbInspectionIssueDetail.Rows[0]["final_term_days"]);
                    dtpFinalDueDate.Text = Val.DBDate(Val.ToString(m_dtbInspectionIssueDetail.Rows[0]["final_due_date"]));

                    txtExchangeRate.Text = Val.ToString(m_dtbInspectionIssueDetail.Rows[0]["exchange_rate"]);
                    lueCurrency.Text = Val.ToString(m_dtbInspectionIssueDetail.Rows[0]["currency_type"]);
                    lueSeller.EditValue = Val.ToInt(m_dtbInspectionIssueDetail.Rows[0]["seller_id"]);

                    txtDemandNo.Text = Val.ToString(m_dtbInspectionIssueDetail.Rows[0]["demand_no"]);
                }
                else
                {
                    if (!GenerateMemoIssueDetails())
                    {
                    }
                }

                dgvInspectionDetails.BestFitColumns();

            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        private void GetCarat()
        {
            try
            {
                DataTable m_dtbStockCarat = new DataTable();
                m_dtbStockCarat = objInspectionIssue.GetStockCarat(GlobalDec.gEmployeeProperty.company_id, GlobalDec.gEmployeeProperty.branch_id, GlobalDec.gEmployeeProperty.location_id, GlobalDec.gEmployeeProperty.department_id, Val.ToInt(lueAssortName.EditValue), Val.ToInt(lueSieveName.EditValue));

                if (m_dtbStockCarat.Rows.Count > 0)
                {
                    numStockCarat = Val.ToDecimal(m_dtbStockCarat.Rows[0]["cl_carat"]);
                    lblBalCrt.Text = Val.ToString(numStockCarat);
                }
                else
                {
                    numStockCarat = 0;
                    lblBalCrt.Text = "0";
                }

                //if (numStockCarat < Val.ToDecimal(txtCarat.Text))
                //{
                //    Global.Message("Please check enter carat more then stock carat  (Assorts : " + Val.ToString(lueAssortName.Text) + ") (Sieve : " + Val.ToString(lueSieveName.Text) + " ) ", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    txtCarat.Focus();
                //    this.Cursor = Cursors.Default;
                //    return;
                //}
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
                            dgvInspectionView.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvInspectionView.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvInspectionView.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvInspectionView.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvInspectionView.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvInspectionView.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvInspectionView.ExportToCsv(Filepath);
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

        private void FillInpectionIssue()
        {
            try
            {
                objInspectionIssue = new InspectionIssue();

                if (m_dtbInpectionData.Rows.Count > 0)
                {
                    DataTable InpectionDet = new DataTable();
                    InpectionDet.Columns.Add("sr_no");
                    InpectionDet.Columns.Add("inspection_id");
                    InpectionDet.Columns.Add("inspection_no");
                    InpectionDet.Columns.Add("inspection_date");
                    InpectionDet.Columns.Add("assort_id");
                    InpectionDet.Columns.Add("assort_name");
                    InpectionDet.Columns.Add("sieve_id");
                    InpectionDet.Columns.Add("sieve_name");
                    InpectionDet.Columns.Add("sub_sieve_id");
                    InpectionDet.Columns.Add("sub_sieve_name");
                    InpectionDet.Columns.Add("pcs");
                    InpectionDet.Columns.Add("carat");
                    InpectionDet.Columns.Add("rate");
                    InpectionDet.Columns.Add("amount");
                    InpectionDet.Columns.Add("discount_per");
                    InpectionDet.Columns.Add("discount_amt");
                    InpectionDet.Columns.Add("net_amount");
                    InpectionDet.Columns.Add("discount");
                    InpectionDet.Columns.Add("remarks");
                    InpectionDet.Columns.Add("diff_pcs");
                    InpectionDet.Columns.Add("diff_carat");
                    InpectionDet.Columns.Add("flag");
                    InpectionDet.Columns.Add("old_assort_id");
                    InpectionDet.Columns.Add("old_sieve_id");
                    InpectionDet.Columns.Add("old_sub_sieve_id");
                    InpectionDet.Columns.Add("old_assort_name");
                    InpectionDet.Columns.Add("old_sieve_name");
                    InpectionDet.Columns.Add("old_sub_sieve_name");
                    InpectionDet.Columns.Add("current_rate");
                    InpectionDet.Columns.Add("current_amount");
                    InpectionDet.Columns.Add("purchase_rate");
                    InpectionDet.Columns.Add("purchase_amount");

                    InpectionDet.Columns.Add("rej_pcs");
                    InpectionDet.Columns.Add("rej_per");
                    InpectionDet.Columns.Add("rej_carat");
                    InpectionDet.Columns.Add("demand_id");
                    InpectionDet.Columns.Add("flag_issue_check", typeof(Boolean));

                    lueParty.EditValue = Convert.ToInt32(m_dtbInpectionData.Rows[0]["Party_Id"]);
                    lueBroker.EditValue = Convert.ToInt32(m_dtbInpectionData.Rows[0]["Broker_Id"]);
                    txtDemandNo.Text = Convert.ToString(m_dtbInpectionData.Rows[0]["demand_no"]);

                    txtTermDays.Text = Convert.ToString(m_dtbInpectionData.Rows[0]["term_days"]);
                    //dtpDueDate.Text = Val.DBDate(Val.ToString(m_dtbInpectionData.Rows[0]["due_date"]));
                    //txtFinalTermDays.Text = Convert.ToString(m_dtbInpectionData.Rows[0]["final_term_days"]);
                    //dtpFinalDueDate.Text = Val.DBDate(Val.ToString(m_dtbInpectionData.Rows[0]["final_due_date"]));

                    txtExchangeRate.Text = Val.ToString(m_dtbInpectionData.Rows[0]["exchange_rate"]);
                    lueCurrency.EditValue = Val.ToInt32(m_dtbInpectionData.Rows[0]["currency_id"]);
                    //lueSeller.EditValue = Val.ToInt(m_dtbInpectionData.Rows[0]["seller_id"]);
                    lueDeliveryType.EditValue = Val.ToInt(m_dtbInpectionData.Rows[0]["delivery_type_id"]);

                    int i = 0;

                    foreach (DataRow DRow in m_dtbInpectionData.Rows)
                    {

                        InpectionDet.Rows.Add();

                        InpectionDet.Rows[i]["sr_no"] = Val.ToInt(DRow["sr_no"]);
                        InpectionDet.Rows[i]["inspection_id"] = Val.ToInt(0);

                        InpectionDet.Rows[i]["assort_id"] = Val.ToInt(DRow["assort_id"]);
                        InpectionDet.Rows[i]["assort_name"] = Val.ToString(DRow["assort_name"]);

                        InpectionDet.Rows[i]["sieve_id"] = Val.ToInt(DRow["sieve_id"]);
                        InpectionDet.Rows[i]["sieve_name"] = Val.ToString(DRow["sieve_name"]);

                        InpectionDet.Rows[i]["sub_sieve_id"] = Val.ToInt(DRow["sub_sieve_id"]);
                        InpectionDet.Rows[i]["sub_sieve_name"] = Val.ToString(DRow["sub_sieve_name"]);

                        InpectionDet.Rows[i]["pcs"] = Val.ToInt(DRow["pcs"]);
                        InpectionDet.Rows[i]["carat"] = Val.ToDecimal(DRow["carat"]);

                        InpectionDet.Rows[i]["rate"] = Val.ToDecimal(DRow["rate"]);
                        InpectionDet.Rows[i]["amount"] = Val.ToDecimal(DRow["amount"]);
                        InpectionDet.Rows[i]["net_amount"] = Val.ToDecimal(DRow["amount"]);

                        InpectionDet.Rows[i]["rej_pcs"] = 0;
                        InpectionDet.Rows[i]["rej_carat"] = 0;
                        InpectionDet.Rows[i]["discount"] = 0;

                        InpectionDet.Rows[i]["purchase_rate"] = 0;
                        InpectionDet.Rows[i]["purchase_amount"] = 0;

                        InpectionDet.Rows[i]["diff_pcs"] = 0;
                        InpectionDet.Rows[i]["diff_carat"] = 0;

                        InpectionDet.Rows[i]["old_assort_id"] = Val.ToInt(0);
                        InpectionDet.Rows[i]["old_assort_name"] = "";

                        InpectionDet.Rows[i]["old_sieve_id"] = Val.ToInt(0);
                        InpectionDet.Rows[i]["old_sieve_name"] = "";

                        InpectionDet.Rows[i]["old_sub_sieve_id"] = Val.ToInt(0);
                        InpectionDet.Rows[i]["old_sub_sieve_name"] = "";

                        InpectionDet.Rows[i]["demand_id"] = Val.ToInt64(DRow["demand_id"]);
                        InpectionDet.Rows[i]["flag_issue_check"] = Val.ToInt(DRow["flag_issue_check"]);

                        i++;

                    }

                    grdInspectionDetails.DataSource = InpectionDet;

                    m_dtbInspectionIssueDetail = InpectionDet;
                    txtTermDays_TextChanged(null, null);
                    //ttlbSaleInvoice.SelectedTabPage = tblSaledetail;
                    //txtInvoiceNo.Focus();

                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
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
                if (dtpInspectionDate.Text.Length <= 0 || dtpInspectionDate.Text == "")
                {
                    txtFinalTermDays.Text = "";
                    dtpFinalDueDate.EditValue = null;
                }
                else
                {
                    Double Days = Val.ToDouble(txtFinalTermDays.Text);
                    DateTime Date = Convert.ToDateTime(dtpInspectionDate.EditValue).AddDays(Val.ToDouble(Days));
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
                if (dtpInspectionDate.Text.Length <= 0 || dtpInspectionDate.Text == "")
                {
                    txtTermDays.Text = "";
                    dtpDueDate.EditValue = null;
                }
                else
                {
                    Double Days = Val.ToDouble(txtTermDays.Text) + Val.ToDouble(dtpInspectionDate.Text);
                    DateTime Date = Convert.ToDateTime(dtpInspectionDate.EditValue).AddDays(Val.ToDouble(Days));
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

        private void lblRejPer_Click(object sender, EventArgs e)
        {

        }

        private void dgvInspectionView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (Val.ToInt32(dgvInspectionView.GetRowCellValue(e.RowHandle, "receive_pending")) == 1)
                {
                    e.Appearance.BackColor = Color.FromArgb(248, 210, 210);
                }
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            ObjPer.SetFormPer();
            if (ObjPer.AllowDelete == false)
            {
                Global.Message(BLL.GlobalDec.gStrPermissionDelMsg);
                return;
            }
            BtnDelete.Enabled = false;


            DialogResult result = MessageBox.Show("Do you want to Delete data?", "Confirmation", MessageBoxButtons.YesNoCancel);
            if (result != DialogResult.Yes)
            {
                BtnDelete.Enabled = true;
                return;
            }

            DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
            panelProgress.Visible = true;
            backgroundWorkerInspectionIssueDelete.RunWorkerAsync();

            BtnDelete.Enabled = true;
        }

        private void backgroundWorkerInspectionIssueDelete_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Inspection_IssueProperty objInspection_IssueProperty = new Inspection_IssueProperty();
            InspectionIssue objInspectionIssue = new InspectionIssue();
            try
            {
                if (txtInspectionNo.Text != "")
                {
                    objInspection_IssueProperty.inspection_no = Val.ToString(txtInspectionNo.Text);
                    strInspectionMessage = objInspectionIssue.Delete(objInspection_IssueProperty);

                    if (strInspectionMessage == "1")
                    {
                        Global.Message("This Inspection Number :" + objInspection_IssueProperty.inspection_no + " Receive Data Are Not Deleted");
                        return;
                    }
                }
                else
                {
                    Global.Message("Inspection No Not Found");
                    return;
                }
            }
            catch (Exception ex)
            {
                IntRes = -1;
                General.ShowErrors(ex.ToString());
                if (ex.InnerException != null)
                {
                    Global.Message(ex.InnerException.ToString());
                }
            }
            finally
            {
                objInspection_IssueProperty = null;
            }
        }

        private void backgroundWorkerInspectionIssueDelete_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                panelProgress.Visible = false;
                if (strInspectionMessage == "0")
                {
                    Global.Confirm("Inspection Issue Data Delete Successfully");
                    ClearDetails();
                }
                else
                {
                    Global.Confirm("Error In Inspection Issue Delete");
                    txtInspectionNo.Focus();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }

        private void dtpInspectionDate_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtpInspectionDate.Text.Length <= 0 || dtpInspectionDate.Text == "")
                {
                    txtTermDays.Text = "";
                    dtpDueDate.EditValue = null;
                    txtFinalTermDays.Text = "";
                    dtpFinalDueDate.EditValue = null;
                }
                else
                {
                    Double Days = Val.ToDouble(txtTermDays.Text) + Val.ToDouble(dtpInspectionDate.Text);
                    DateTime Date = Convert.ToDateTime(dtpInspectionDate.EditValue).AddDays(Val.ToDouble(Days));
                    dtpDueDate.EditValue = Val.DBDate(Date.ToShortDateString());

                    Double Days_Terms = Val.ToDouble(txtFinalTermDays.Text);
                    DateTime Date_Terms = Convert.ToDateTime(dtpInspectionDate.EditValue).AddDays(Val.ToDouble(Days_Terms));
                    dtpFinalDueDate.EditValue = Val.DBDate(Date_Terms.ToShortDateString());
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }

        private void txtInspectionNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    m_blncheck = true;
                    string MemoNo = Val.ToString(txtInspectionNo.Text);
                    if (m_blncheck)
                        GetData(MemoNo);
                    m_blncheck = false;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
    }
}