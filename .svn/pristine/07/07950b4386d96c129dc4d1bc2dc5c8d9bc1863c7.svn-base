using BLL;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Rejection;
using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.FunctionClasses.Utility;
using BLL.PropertyClasses.Transaction;
using BLL.PropertyClasses.Transaction.MFG;
using DERP.Class;
using DERP.Master.MFG;
using DERP.Rejection;
using DevExpress.XtraEditors;
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
    public partial class FrmMFGRejectionJangedIssue : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        Control _NextEnteredControl;
        private List<Control> _tabControls;
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        MFGRejectionJangedIssue objMFGRejectionJangedIssue;
        FinancialYearMaster objFinYear;
        UserAuthentication objUserAuthentication;
        RateMaster objRate;
        OpeningStock opstk;

        DataTable DtControlSettings;
        DataTable m_dtbRejJangedDetails;
        DataTable m_dtbDetails;
        DataTable m_opDate;
        DataTable m_dtbMemoNo;
        DataTable m_dtbDemandNo;
        DataTable m_dtbKapanNo;
        DataTable m_dtbType;
        DataTable RejPurity;
        DataTable m_dtbCurrencyType;
        DataTable m_dtbParty;
        DataTable m_dtbRejectionSale;
        MfgRejectionPurityMaster objRejPurity = new MfgRejectionPurityMaster();

        string m_broker_name;
        int m_invoice_id;
        int m_srno;
        int m_update_srno;
        Int64 IntRes;
        Int64 IntResDet;
        int m_numForm_id;
        int m_numCurrency_id;

        decimal m_numTotalCarats;
        decimal m_numTotalAmount;
        decimal m_numcarat;
        decimal m_current_rate;
        decimal m_current_amount;
        decimal m_numSummRate;

        bool m_blnadd;
        bool m_blnsave;

        #endregion

        #region Constructor
        public FrmMFGRejectionJangedIssue()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            _NextEnteredControl = new Control();
            _tabControls = new List<Control>();

            objMFGRejectionJangedIssue = new MFGRejectionJangedIssue();
            objFinYear = new FinancialYearMaster();
            objUserAuthentication = new UserAuthentication();
            objRate = new RateMaster();
            opstk = new OpeningStock();

            DtControlSettings = new DataTable();
            m_dtbRejJangedDetails = new DataTable();
            m_dtbDetails = new DataTable();
            m_opDate = new DataTable();
            m_dtbMemoNo = new DataTable();
            m_dtbDemandNo = new DataTable();
            m_dtbKapanNo = new DataTable();
            m_dtbType = new DataTable();
            RejPurity = new DataTable();
            m_dtbCurrencyType = new DataTable();
            m_dtbParty = new DataTable();
            m_dtbRejectionSale = new DataTable();

            m_broker_name = string.Empty;
            m_invoice_id = 0;
            m_srno = 0;
            m_update_srno = 0;
            IntRes = 0;
            IntResDet = 0;
            m_numForm_id = 0;
            m_numTotalCarats = 0;
            m_numTotalAmount = 0;
            m_numcarat = 0;
            m_current_rate = 0;
            m_current_amount = 0;
            m_numCurrency_id = 0;

            m_blnadd = new bool();
            m_blnsave = new bool();
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
            AddKeyPressListener(this);
            this.KeyPreview = true;

            TabControlsToList(this.Controls);
            _tabControls = _tabControls.OrderBy(x => x.TabIndex).ToList();

            // End for Dynamic Setting By Praful On 01022020

            this.Show();
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

        private void AddKeyPressListener(Control ctrl)
        {
            foreach (Control c in ctrl.Controls)
            {
                c.KeyPress += new KeyPressEventHandler(Control_KeyPress);
                if (c.Controls.Count > 0)
                {
                    AddKeyPressListener(c);
                }
            }
        }

        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((Control)sender).Name.ToString().Trim().Equals(string.Empty))
            {
                _NextEnteredControl = (Control)sender;
                if ((Control)sender is LookUpEdit)
                {
                    if (e.KeyChar == Convert.ToChar(Keys.Enter))
                    {
                        SendKeys.Send("{TAB}");
                    }
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
        private void ControllSettings(Control item2, DataTable DTab)
        {
            BLL.Validation Val = new BLL.Validation();

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

            if (item2.Controls.Count > 0)
            {
                foreach (Control item1 in item2.Controls)
                {
                    ControllSettings(item1, DTab);
                }
            }
        }
        #endregion
        private void AttachFormEvents()
        {
            objBOFormEvents.CurForm = this;
            objBOFormEvents.FormKeyPress = true;
            objBOFormEvents.FormKeyDown = true;
            objBOFormEvents.FormResize = true;
            objBOFormEvents.FormClosing = true;
            objBOFormEvents.ObjToDispose.Add(objMFGRejectionJangedIssue);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);

        }

        #endregion

        #region Events
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (AddInGrid())
            {
                txtPurityGroup.Text = "";
                txtPcs.Text = string.Empty;
                txtCarat.Text = string.Empty;
                txtRate.Text = string.Empty;
                txtAmount.Text = string.Empty;

                DialogResult result = MessageBox.Show("Add More Entry?", "Confirmation", MessageBoxButtons.YesNo);
                if (result != DialogResult.Yes)
                {
                    txtAddPer.Focus();
                }
                else
                {
                    lueRejPurity.Focus();
                }
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
            backgroundWorker_MFGRejJangedIssue.RunWorkerAsync();

            btnSave.Enabled = true;
        }
        private void btnClear_Click_1(object sender, EventArgs e)
        {

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
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (GlobalDec.gEmployeeProperty.user_name != "JAYESH")
            {
                Global.Message("Don't have Permission...So Please Contact to Administrator");
                return;
            }
            ObjPer.SetFormPer();
            if (ObjPer.AllowDelete == false)
            {
                Global.Message(BLL.GlobalDec.gStrPermissionDelMsg);
                return;
            }
            btnDelete.Enabled = false;
            MFGRejectionJangedIssueProperty objMFGRejectionJangedIssueProperty = new MFGRejectionJangedIssueProperty();
            MFGRejectionJangedIssue objMFGRejectionJangedIssue = new MFGRejectionJangedIssue();
            try
            {
                if (Val.ToInt(lblMode.Tag) != 0)
                {
                    DialogResult result = MessageBox.Show("Do you want to Delete data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                    if (result != DialogResult.Yes)
                    {
                        btnDelete.Enabled = true;
                        return;
                    }

                    objMFGRejectionJangedIssueProperty.janged_id = Val.ToInt(lblMode.Tag);

                    int IntRes = objMFGRejectionJangedIssue.GetDeleteJanged_ID(objMFGRejectionJangedIssueProperty);

                    if (IntRes == -1)
                    {
                        Global.Confirm("Error In Rejection Janged Issue Invoice");
                    }
                    else
                    {
                        if (Val.ToInt(lblMode.Tag) == 0)
                        {
                            Global.Confirm("Rejection Janged Issue Data Delete Successfully");
                            ClearDetails();
                            PopulateDetails();
                        }
                        else
                        {
                            Global.Confirm("Rejection Janged Issue Data Delete Successfully");
                            ClearDetails();
                            PopulateDetails();
                        }
                    }
                }
                else
                {
                    Global.Message("Rejection Janged Data not found");
                    return;
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
            }
            finally
            {
                objMFGRejectionJangedIssueProperty = null;
            }
            btnDelete.Enabled = true;
        }
        private void FrmMFGRejectionJangedIssue_Load(object sender, EventArgs e)
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
                    ttlbMFGRejJangedIssue.SelectedTabPage = tblRejJangeddetail;
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
            e.Graphics.DrawLine(pen, 0, 80, 1500, 80);
        }
        private void txtCarat_EditValueChanged(object sender, EventArgs e)
        {
            txtAmount.Text = string.Format("{0:0.00}", Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text));
            m_current_amount = Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(m_current_rate);
        }
        private void txtRate_EditValueChanged(object sender, EventArgs e)
        {
            txtAmount.Text = string.Format("{0:0.00}", Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text));
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
                    Double Days = Val.ToDouble(txtTermDays.Text);
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
        private void lueBroker_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //if (e.Button.Index == 1)
            //{
            //    FrmBrokerMaster frmBroker = new FrmBrokerMaster();
            //    frmBroker.ShowDialog();
            //    Global.LOOKUPBroker(lueBroker);
            //}
        }
        private void dtpPurchaseDate_EditValueChanged(object sender, EventArgs e)
        {

            //DataTable dtFinYear = new DataTable();
            //dtFinYear = objFinYear.GetData();
            //foreach (DataRow drw in dtFinYear.Rows)
            //{
            //    var result = DateTime.Compare(Convert.ToDateTime(dtpInvoiceDate.Text), Convert.ToDateTime(drw["start_date"]));
            //    var result2 = DateTime.Compare(Convert.ToDateTime(dtpInvoiceDate.Text), Convert.ToDateTime(drw["end_date"]));
            //    if (result > 0 && result2 < 0)
            //    {
            //        m_FinYear = Val.ToInt(drw["fin_year_id"]);
            //    }

            //}
        }
        private void lueRejPurity_EditValueChanged(object sender, EventArgs e)
        {
            DataTable DTabRejStock = new DataTable();
            objMFGRejectionJangedIssue = new MFGRejectionJangedIssue();
            if (lueRejPurity.Text != "")
            {
                DataTable DTabRejPurity = RejPurity.Select("purity_id =" + Val.ToInt64(lueRejPurity.EditValue)).CopyToDataTable();
                DTabRejStock = objMFGRejectionJangedIssue.GetStockCarat(Val.ToInt64(lueRejPurity.EditValue));
                if (DTabRejPurity.Rows.Count > 0)
                {
                    txtPurityGroup.Text = Val.ToString(DTabRejPurity.Rows[0]["group_name"]);
                }
                if (DTabRejStock.Rows.Count > 0)
                {
                    lblStockCarat.Text = Val.ToString(DTabRejStock.Rows[0]["cl_carat"]);
                    lblDiffCarat.Text = Val.ToString(DTabRejStock.Rows[0]["cl_carat"]);
                }
                else
                {
                    lblStockCarat.Text = "0.00";
                    lblDiffCarat.Text = "0.00";
                }
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearDetails();
        }
        private void txtAddPer_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                decimal Add_Amt = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue)) * Val.ToDecimal(txtAddPer.Text) / 100, 0);
                txtAddAmt.Text = Add_Amt.ToString();
                decimal Net_Amount = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue)) + Val.ToDecimal(txtAddAmt.Text) - Val.ToDecimal(txtLessAmt.Text), 0);
                txtNetAmount.Text = Net_Amount.ToString();
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void txtLessPer_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                decimal Less_Amt = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue)) * Val.ToDecimal(txtLessPer.Text) / 100, 0);
                txtLessAmt.Text = Less_Amt.ToString();
                decimal Net_Amount = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue)) + Val.ToDecimal(txtAddAmt.Text) - Val.ToDecimal(txtLessAmt.Text), 0);
                txtNetAmount.Text = Net_Amount.ToString();
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void backgroundWorker_MFGRejJangedIssue_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.Default;
                Conn = new BeginTranConnection(true, false);
                //double dueDays;
                //dueDays = (Convert.ToDateTime(dtpDueDate.Text) - Convert.ToDateTime(dtpPurchaseDate.Text)).TotalDays;

                MFGRejectionJangedIssueProperty objMFGRejectionJangedIssueProperty = new MFGRejectionJangedIssueProperty();
                MFGRejectionJangedIssue objMFGRejectionJangedIssue = new MFGRejectionJangedIssue();
                //IntRes = 0;
                try
                {
                    objMFGRejectionJangedIssueProperty.janged_id = Val.ToInt(lblMode.Tag);
                    objMFGRejectionJangedIssueProperty.invoice_no = Val.ToString(txtInvoiceNo.Text);
                    objMFGRejectionJangedIssueProperty.company_id = Val.ToInt(GlobalDec.gEmployeeProperty.company_id);
                    objMFGRejectionJangedIssueProperty.branch_id = Val.ToInt(GlobalDec.gEmployeeProperty.branch_id);
                    objMFGRejectionJangedIssueProperty.location_id = Val.ToInt(GlobalDec.gEmployeeProperty.location_id);
                    objMFGRejectionJangedIssueProperty.department_id = Val.ToInt(GlobalDec.gEmployeeProperty.department_id);
                    objMFGRejectionJangedIssueProperty.invoice_date = Val.DBDate(dtpInvoiceDate.Text);
                    objMFGRejectionJangedIssueProperty.type = Val.ToString(lueType.Text);
                    objMFGRejectionJangedIssueProperty.remarks = Val.ToString(txtRemarks.Text);

                    objMFGRejectionJangedIssueProperty.rejection_party_id = Val.ToInt64(lueRejectionParty.EditValue);
                    objMFGRejectionJangedIssueProperty.rejection_broker_name = Val.ToString(CmbRejectionBroker.Text.ToUpper());
                    objMFGRejectionJangedIssueProperty.due_days = Val.ToInt(txtTermDays.EditValue);
                    objMFGRejectionJangedIssueProperty.due_date = Val.DBDate(dtpDueDate.Text);
                    objMFGRejectionJangedIssueProperty.total_pcs = Val.ToDecimal(clmDetPcs.SummaryItem.SummaryValue);
                    objMFGRejectionJangedIssueProperty.total_carat = Math.Round(Val.ToDecimal(clmDetCarat.SummaryItem.SummaryValue), 3);
                    objMFGRejectionJangedIssueProperty.gross_amount = Math.Round(Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue), 3);
                    objMFGRejectionJangedIssueProperty.gross_rate = Math.Round(Val.ToDecimal(clmRSrate.SummaryItem.SummaryValue), 3);
                    objMFGRejectionJangedIssueProperty.add_per = Val.ToDecimal(txtAddPer.Text);
                    objMFGRejectionJangedIssueProperty.add_amount = Val.ToDecimal(txtAddAmt.Text);
                    objMFGRejectionJangedIssueProperty.less_per = Val.ToDecimal(txtLessPer.Text);
                    objMFGRejectionJangedIssueProperty.less_amount = Val.ToDecimal(txtLessAmt.Text);
                    objMFGRejectionJangedIssueProperty.net_amount = Val.ToDecimal(txtNetAmount.Text);
                    objMFGRejectionJangedIssueProperty.union_id = IntRes;
                    objMFGRejectionJangedIssueProperty.form_id = m_numForm_id;
                    objMFGRejectionJangedIssueProperty.currency = Val.ToString(lueCurrency.Text);
                    objMFGRejectionJangedIssueProperty.currency_type_id = Val.ToInt(m_numCurrency_id);
                    objMFGRejectionJangedIssueProperty.exchange_rate = Val.ToDecimal(txtExchangeRate.Text);

                    objMFGRejectionJangedIssueProperty = objMFGRejectionJangedIssue.Save(objMFGRejectionJangedIssueProperty, DLL.GlobalDec.EnumTran.Start, Conn);

                    Int64 NewmJangedid = Val.ToInt64(objMFGRejectionJangedIssueProperty.janged_id);
                    IntRes = Val.ToInt64(objMFGRejectionJangedIssueProperty.union_id);

                    int IntCounter = 0;
                    int Count = 0;
                    int TotalCount = m_dtbRejJangedDetails.Rows.Count;
                    foreach (DataRow drw in m_dtbRejJangedDetails.Rows)
                    {
                        objMFGRejectionJangedIssueProperty.invoice_date = Val.ToString(dtpInvoiceDate.Text);
                        objMFGRejectionJangedIssueProperty.janged_id = Val.ToInt64(NewmJangedid);
                        objMFGRejectionJangedIssueProperty.janged_detail_id = Val.ToInt64(drw["janged_det_id"]);
                        objMFGRejectionJangedIssueProperty.union_id = Val.ToInt64(IntRes);
                        objMFGRejectionJangedIssueProperty.type = Val.ToString(drw["type"]);
                        objMFGRejectionJangedIssueProperty.rej_purity_id = Val.ToInt64(drw["purity_id"]);
                        objMFGRejectionJangedIssueProperty.group_name = Val.ToString(drw["group_name"]);
                        objMFGRejectionJangedIssueProperty.pcs = Val.ToDecimal(drw["pcs"]);
                        objMFGRejectionJangedIssueProperty.carat = Val.ToDecimal(drw["carat"]);
                        objMFGRejectionJangedIssueProperty.rate = Val.ToDecimal(drw["rate"]);
                        objMFGRejectionJangedIssueProperty.amount = Val.ToDecimal(drw["amount"]);
                        objMFGRejectionJangedIssueProperty.form_id = m_numForm_id;
                        IntResDet = objMFGRejectionJangedIssue.Save_Detail(objMFGRejectionJangedIssueProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                        Count++;
                        IntCounter++;
                        SetControlPropertyValue(lblProgressCount, "Text", Count.ToString() + "" + "/" + "" + TotalCount.ToString() + " Completed....");
                    }
                    Conn.Inter1.Commit();
                }
                catch (Exception ex)
                {
                    IntResDet = -1;
                    Conn.Inter1.Rollback();
                    Conn = null;
                    General.ShowErrors(ex.ToString());
                    return;
                }
                finally
                {
                    objMFGRejectionJangedIssueProperty = null;
                }
            }
            catch (Exception ex)
            {
                IntResDet = -1;
                Conn.Inter1.Rollback();
                Conn = null;
                General.ShowErrors(ex.ToString());
                if (ex.InnerException != null)
                {
                    Global.Message(ex.InnerException.ToString());
                }
            }
        }
        private void backgroundWorker_MFGRejJangedIssue_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                panelProgress.Visible = false;
                if (IntResDet > 0)
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Rough Rejection Janged Issue Entry Data Save Successfully");
                        objMFGRejectionJangedIssue = new MFGRejectionJangedIssue();
                    }
                    else
                    {
                        Global.Confirm("Rough Rejection Janged Issue Entry Data Update Successfully");
                    }

                    m_dtbRejectionSale = m_dtbRejJangedDetails.Copy();

                    DialogResult result = MessageBox.Show("Do you want to Rejection sale data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                    if (result == DialogResult.Yes)
                    {
                        FillRejectionSaleInvoice(m_dtbRejectionSale);
                    }

                    ClearDetails();
                    PopulateDetails();
                }
                else
                {
                    Global.Confirm("Error In Rough Rejection Janged Issue Invoice");
                    dtpInvoiceDate.Focus();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }

        public void FillRejectionSaleInvoice(DataTable RejectionSaleDT)
        {
            FrmMFGRejectionSale frmRejectionSale = new FrmMFGRejectionSale();
            Assembly frmAssembly = Assembly.LoadFile(Application.ExecutablePath);

            foreach (Type type in frmAssembly.GetTypes())
            {
                string type1 = type.Name.ToString().ToUpper();
                if (type.BaseType == typeof(DevExpress.XtraEditors.XtraForm))
                {
                    if (type.Name.ToString().ToUpper() == "FRMMFGREJECTIONSALE")
                    {
                        XtraForm frmShow = (XtraForm)frmAssembly.CreateInstance(type.ToString());
                        frmShow.MdiParent = Global.gMainFormRef;
                        frmShow.GetType().GetMethod("ShowForm_New").Invoke(frmShow, new object[] { RejectionSaleDT });
                        break;
                    }
                }
            }
        }

        #region "Grid Events" 

        private void dgvMFGRejJanged_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvMFGRejJanged.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt32(Drow["janged_id"]);
                        btnAdd.Text = "&Update";
                        lueType.Text = Val.ToString(Drow["type"]);
                        txtPurityGroup.Text = Val.ToString(Drow["group_name"]);
                        lueRejPurity.EditValue = Val.ToInt64(Drow["purity_id"]);
                        txtPcs.Text = Val.ToString(Drow["pcs"]);
                        txtCarat.Text = Val.ToString(Drow["carat"]);
                        txtRate.Text = Val.ToString(Drow["rate"]);
                        txtAmount.Text = Val.ToString(Drow["amount"]);
                        m_numcarat = Val.ToDecimal(Drow["carat"]);
                        m_invoice_id = Val.ToInt(Drow["janged_id"]);
                        m_update_srno = Val.ToInt(Drow["sr_no"]);
                        decimal Diff_Carat = Val.ToDecimal(Drow["carat"]) + Val.ToDecimal(lblStockCarat.Text);
                        lblDiffCarat.Text = Diff_Carat.ToString();
                        //IntRes = Val.ToInt64(Drow["union_id"]);
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void dgvMFGRejJanged_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                m_numTotalCarats = Math.Round(Val.ToDecimal(clmDetCarat.SummaryItem.SummaryValue), 2, MidpointRounding.AwayFromZero);

                if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == "rate")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                        m_numTotalAmount = 0;
                    else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                        m_numTotalAmount += (Val.ToDecimal(e.GetValue("carat")) * Val.ToDecimal(e.GetValue("rate")));
                    else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    {
                        if (m_numTotalAmount > 0 && m_numTotalCarats > 0)
                            e.TotalValue = Math.Round((m_numTotalAmount / m_numTotalCarats), 2, MidpointRounding.AwayFromZero);
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
        private void dgvMFGRejJangedList_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                if (Val.ToDecimal(clmLAmount.SummaryItem.SummaryValue) > 0 && Val.ToDecimal(clmLCarat.SummaryItem.SummaryValue) > 0)
                {
                    m_numSummRate = Math.Round((Val.ToDecimal(clmLAmount.SummaryItem.SummaryValue) / Val.ToDecimal(clmLCarat.SummaryItem.SummaryValue)), 2, MidpointRounding.AwayFromZero);
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
        private void dgvMFGRejJangedList_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                objMFGRejectionJangedIssue = new MFGRejectionJangedIssue();
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        //m_blncheckevents = true;

                        DataRow Drow = dgvMFGRejJangedList.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt32(Drow["janged_id"]);
                        txtInvoiceNo.Text = Val.ToString(Drow["invoice_no"]);
                        dtpInvoiceDate.Text = Val.DBDate(Val.ToString(Drow["invoice_date"]));
                        lueRejectionParty.EditValue = Val.ToInt(Drow["rejection_party_id"]);
                        CmbRejectionBroker.Text = Val.ToString(Drow["broker_name"]);
                        //lueBroker.EditValue = Val.ToInt(Drow["broker_id"]);
                        txtTermDays.Text = Val.ToString(Drow["due_days"]);
                        txtRemarks.Text = Val.ToString(Drow["remarks"]);
                        txtAddPer.Text = Val.ToString(Drow["add_per"]);
                        txtAddAmt.Text = Val.ToString(Drow["add_amount"]);
                        txtLessPer.Text = Val.ToString(Drow["discount_per"]);
                        txtLessAmt.Text = Val.ToString(Drow["discount_amount"]);
                        txtGrossAmount.Text = Val.ToString(Drow["amount"]);
                        txtNetAmount.Text = Val.ToString(Drow["net_amount"]);
                        lueCurrency.EditValue = Val.ToInt(Drow["currency_id"]);
                        txtExchangeRate.Text = Val.ToString(Drow["exchange_rate"]);
                        IntRes = Val.ToInt64(Drow["union_id"]);
                        m_dtbRejJangedDetails = objMFGRejectionJangedIssue.GetDataDetails(Val.ToInt64(lblMode.Tag));

                        grdMFGRejJanged.DataSource = m_dtbRejJangedDetails;

                        ttlbMFGRejJangedIssue.SelectedTabPage = tblRejJangeddetail;
                        dtpInvoiceDate.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
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
        private void lueCurrency_EditValueChanged(object sender, EventArgs e)
        {
            CurrencyMaster objCurrency = new CurrencyMaster();

            m_dtbCurrencyType = objCurrency.GetCurrencyID(Val.ToString(lueCurrency.EditValue));

            if (m_dtbCurrencyType.Rows.Count > 0)
            {
                m_numCurrency_id = Val.ToInt(m_dtbCurrencyType.Rows[0]["currency_id"]);
            }
        }
        private void dgvMFGRejJangedList_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (Val.ToInt(dgvMFGRejJangedList.GetRowCellValue(e.RowHandle, "is_transfer")) == 1)
                {
                    e.Appearance.BackColor = Color.FromArgb(248, 210, 210);
                }

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
                Global.LOOKUPRejectionParty(lueRejectionParty);
                //Global.LOOKUPBroker(lueBroker);
                Global.LOOKUPRejectionParty(lueSearchRejectionParty);
                Global.LOOKUPRejectionContactPerson(CmbRejectionBroker);

                RejPurity = objRejPurity.GetData(1);
                lueRejPurity.Properties.DataSource = RejPurity;
                lueRejPurity.Properties.ValueMember = "purity_id";
                lueRejPurity.Properties.DisplayMember = "purity_name";

                dtpFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpFromDate.EditValue = DateTime.Now;

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

                m_dtbCurrencyType = Global.CurrencyType();
                lueCurrency.Properties.DataSource = m_dtbCurrencyType;
                lueCurrency.Properties.ValueMember = "currency_id";
                lueCurrency.Properties.DisplayMember = "currency";

                lueCurrency.EditValue = Val.ToInt64(GlobalDec.gEmployeeProperty.currency_id);

                m_dtbType = new DataTable();
                m_dtbType.Columns.Add("type");
                m_dtbType.Rows.Add("ROUGH");
                m_dtbType.Rows.Add("REJECTION");

                lueType.Properties.DataSource = m_dtbType;
                lueType.Properties.ValueMember = "type";
                lueType.Properties.DisplayMember = "type";
                lueType.EditValue = "ROUGH";
                txtExchangeRate.Text = "1";

                txtInvoiceNo.Focus();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            finally
            {
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
                    DataRow drwNew = m_dtbRejJangedDetails.NewRow();
                    decimal numPcs = Val.ToDecimal(txtPcs.Text);
                    decimal numCarat = Val.ToDecimal(txtCarat.Text);
                    decimal numRate = Val.ToDecimal(txtRate.Text);
                    decimal numAmount = Val.ToDecimal(txtAmount.Text);

                    drwNew["janged_id"] = Val.ToInt(0);
                    drwNew["pcs"] = numPcs;
                    drwNew["carat"] = numCarat;
                    drwNew["rate"] = Val.ToDecimal(txtRate.Text);
                    drwNew["amount"] = Math.Round(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text), 2);
                    //drwNew["old_carat"] = Val.ToDecimal(0);
                    drwNew["flag"] = Val.ToInt(0);
                    m_srno = m_srno + 1;
                    drwNew["sr_no"] = Val.ToInt(m_srno);

                    drwNew["purity_id"] = Val.ToInt64(lueRejPurity.EditValue);
                    drwNew["purity_name"] = Val.ToString(lueRejPurity.Text);
                    drwNew["group_name"] = Val.ToString(txtPurityGroup.Text);
                    drwNew["type"] = Val.ToString(lueType.Text);

                    drwNew["rejection_party_id"] = Val.ToInt64(lueRejectionParty.EditValue);
                    drwNew["rejection_party_name"] = Val.ToString(lueRejectionParty.Text);
                    drwNew["due_days"] = Val.ToInt32(txtTermDays.Text);
                    drwNew["due_date"] = Val.ToString(dtpDueDate.Text);
                    drwNew["slip_no"] = Val.ToString(txtInvoiceNo.Text);
                    drwNew["broker_name"] = Val.ToString(CmbRejectionBroker.Text.ToUpper());

                    m_dtbRejJangedDetails.Rows.Add(drwNew);
                    dgvMFGRejJanged.MoveLast();

                    decimal Gross_Amount = Math.Round(Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue), 0);
                    txtGrossAmount.Text = Gross_Amount.ToString();

                    decimal Net_Amount = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) + Val.ToDecimal(txtAddAmt.Text)) - Val.ToDecimal(txtLessAmt.Text), 0);
                    txtNetAmount.Text = Net_Amount.ToString();
                }
                else if (btnAdd.Text == "&Update")
                {
                    for (int i = 0; i < m_dtbRejJangedDetails.Rows.Count; i++)
                    {
                        if (m_dtbRejJangedDetails.Select("janged_id ='" + m_invoice_id + "' AND sr_no = '" + m_update_srno + "'").Length > 0)
                        {
                            if (m_dtbRejJangedDetails.Rows[m_update_srno - 1]["janged_id"].ToString() == m_invoice_id.ToString())
                            {
                                m_dtbRejJangedDetails.Rows[m_update_srno - 1]["pcs"] = Val.ToDecimal(txtPcs.Text).ToString();
                                m_dtbRejJangedDetails.Rows[m_update_srno - 1]["carat"] = Val.ToDecimal(txtCarat.Text).ToString();
                                m_dtbRejJangedDetails.Rows[m_update_srno - 1]["rate"] = Val.ToDecimal(txtRate.Text);
                                m_dtbRejJangedDetails.Rows[m_update_srno - 1]["flag"] = 1;
                                m_dtbRejJangedDetails.Rows[m_update_srno - 1]["amount"] = Math.Round(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text), 2);

                                m_dtbRejJangedDetails.Rows[m_update_srno - 1]["type"] = Val.ToString(lueType.Text);
                                m_dtbRejJangedDetails.Rows[m_update_srno - 1]["group_name"] = Val.ToString(txtPurityGroup.Text);
                                m_dtbRejJangedDetails.Rows[m_update_srno - 1]["purity_id"] = Val.ToInt64(lueRejPurity.EditValue);
                                m_dtbRejJangedDetails.Rows[m_update_srno - 1]["purity_name"] = Val.ToString(lueRejPurity.Text);

                                decimal Gross_Amount = Math.Round(Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue), 0);
                                txtGrossAmount.Text = Gross_Amount.ToString();
                                decimal Net_Amount = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) + Val.ToDecimal(txtAddAmt.Text)) - Val.ToDecimal(txtLessAmt.Text), 0);
                                txtNetAmount.Text = Net_Amount.ToString();
                                break;
                            }
                        }
                    }
                    btnAdd.Text = "&Add";
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
                if (!objMFGRejectionJangedIssue.ISExists(txtInvoiceNo.Text, Val.ToInt64(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
                {
                    lstError.Add(new ListError(5, "Slip No Already Exists..Please Choose Another Slip No"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtInvoiceNo.Focus();
                    }
                }
                if (m_blnsave)
                {
                    if (m_dtbRejJangedDetails.Rows.Count == 0)
                    {
                        lstError.Add(new ListError(22, "Record"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                        }
                    }
                    if (dgvMFGRejJanged == null)
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
                    if (lueRejectionParty.Text == "")
                    {
                        lstError.Add(new ListError(13, "Party"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueRejectionParty.Focus();
                        }
                    }
                    if (Val.ToDouble(txtExchangeRate.Text) == 0)
                    {
                        lstError.Add(new ListError(12, "Exchange Rate"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtExchangeRate.Focus();
                        }
                    }
                }
                if (m_blnadd)
                {
                    //if (Val.ToDouble(txtPcs.Text) == 0)
                    //{
                    //    lstError.Add(new ListError(12, "Pcs"));
                    //    if (!blnFocus)
                    //    {
                    //        blnFocus = true;
                    //        txtPcs.Focus();
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

                    if (Val.ToDouble(txtAmount.Text) == 0)
                    {
                        lstError.Add(new ListError(12, "Amount"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtAmount.Focus();
                        }
                    }

                    if (lueType.Text == "")
                    {
                        lstError.Add(new ListError(13, "Type"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueType.Focus();
                        }
                    }
                    if (lueRejPurity.Text == "")
                    {
                        lstError.Add(new ListError(13, "Rej Purity"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueRejPurity.Focus();
                        }
                    }
                    if (txtPurityGroup.Text == "")
                    {
                        lstError.Add(new ListError(13, "Purity Group"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtPurityGroup.Focus();
                        }
                    }
                    if (Val.ToDecimal(txtCarat.Text) > Val.ToDecimal(lblDiffCarat.Text))
                    {
                        lstError.Add(new ListError(5, "Carat is not more than Stock Carat "));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtCarat.Focus();
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

                lblMode.Tag = 0;
                lblMode.Text = "Add Mode";
                lueRejectionParty.EditValue = System.DBNull.Value;
                CmbRejectionBroker.EditValue = System.DBNull.Value;
                lueRejPurity.EditValue = System.DBNull.Value;
                txtTermDays.Text = string.Empty;

                txtSearchInvoice.Text = string.Empty;
                lueSearchRejectionParty.EditValue = System.DBNull.Value;
                dtpInvoiceDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpInvoiceDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpInvoiceDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpInvoiceDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpInvoiceDate.EditValue = DateTime.Now;

                txtInvoiceNo.Text = string.Empty;
                txtPcs.Text = string.Empty;
                txtCarat.Text = string.Empty;
                txtRate.Text = string.Empty;
                txtAmount.Text = string.Empty;
                lueCurrency.EditValue = Val.ToString(GlobalDec.gEmployeeProperty.currency_id);
                txtExchangeRate.Text = "1";
                txtRemarks.Text = string.Empty;

                txtAddPer.Text = string.Empty;
                txtAddAmt.Text = string.Empty;
                txtLessPer.Text = string.Empty;
                txtLessAmt.Text = string.Empty;
                txtGrossAmount.Text = string.Empty;
                txtNetAmount.Text = string.Empty;
                CmbRejectionBroker.SelectedIndex = -1;
                lblStockCarat.Text = "0";
                lblDiffCarat.Text = "0";
                m_srno = 0;
                btnAdd.Text = "&Add";
                txtInvoiceNo.Focus();
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
            objMFGRejectionJangedIssue = new MFGRejectionJangedIssue();
            bool blnReturn = true;
            DateTime datFromDate = DateTime.MinValue;
            DateTime datToDate = DateTime.MinValue;
            try
            {
                m_dtbDetails = objMFGRejectionJangedIssue.GetData(Val.DBDate(dtpFromDate.Text), Val.DBDate(dtpToDate.Text), Val.ToString(txtSearchInvoice.Text), Val.ToInt32(lueSearchRejectionParty.EditValue));

                if (m_dtbDetails.Rows.Count == 0)
                {
                    Global.Message("Data Not Found");
                    blnReturn = false;
                }
                grdMFGRejJangedList.DataSource = m_dtbDetails;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            finally
            {
                objMFGRejectionJangedIssue = null;
            }

            return blnReturn;
        }

        private bool GeneratePurchaseDetails()
        {
            bool blnReturn = true;
            try
            {
                if (m_dtbRejJangedDetails.Rows.Count > 0)
                    m_dtbRejJangedDetails.Rows.Clear();

                m_dtbRejJangedDetails = new DataTable();

                m_dtbRejJangedDetails.Columns.Add("sr_no", typeof(int));
                m_dtbRejJangedDetails.Columns.Add("janged_det_id", typeof(int));
                m_dtbRejJangedDetails.Columns.Add("janged_id", typeof(int));
                m_dtbRejJangedDetails.Columns.Add("purity_id", typeof(Int64));
                m_dtbRejJangedDetails.Columns.Add("purity_name", typeof(string));
                m_dtbRejJangedDetails.Columns.Add("pcs", typeof(decimal)).DefaultValue = 0;
                m_dtbRejJangedDetails.Columns.Add("carat", typeof(decimal)).DefaultValue = 0;
                m_dtbRejJangedDetails.Columns.Add("rate", typeof(decimal)).DefaultValue = 0;
                m_dtbRejJangedDetails.Columns.Add("amount", typeof(decimal)).DefaultValue = 0;
                m_dtbRejJangedDetails.Columns.Add("old_carat", typeof(decimal));
                m_dtbRejJangedDetails.Columns.Add("flag", typeof(int)).DefaultValue = 0;
                m_dtbRejJangedDetails.Columns.Add("current_rate", typeof(decimal)).DefaultValue = 0;
                m_dtbRejJangedDetails.Columns.Add("current_amount", typeof(decimal)).DefaultValue = 0;
                m_dtbRejJangedDetails.Columns.Add("group_name", typeof(string));
                m_dtbRejJangedDetails.Columns.Add("type", typeof(string));
                m_dtbRejJangedDetails.Columns.Add("rejection_party_id", typeof(Int64));
                m_dtbRejJangedDetails.Columns.Add("rejection_party_name", typeof(string));
                m_dtbRejJangedDetails.Columns.Add("broker_name", typeof(string));
                m_dtbRejJangedDetails.Columns.Add("due_days", typeof(Int32));
                m_dtbRejJangedDetails.Columns.Add("due_date", typeof(string));
                m_dtbRejJangedDetails.Columns.Add("slip_no", typeof(string));

                grdMFGRejJanged.DataSource = m_dtbRejJangedDetails;
                grdMFGRejJanged.Refresh();
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
                            dgvMFGRejJangedList.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvMFGRejJangedList.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvMFGRejJangedList.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvMFGRejJangedList.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvMFGRejJangedList.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvMFGRejJangedList.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvMFGRejJangedList.ExportToCsv(Filepath);
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

        private void txtPcs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}