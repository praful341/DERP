using BLL;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Utility;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
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
    public partial class FrmInspectionRecieve : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        Control _NextEnteredControl;
        private List<Control> _tabControls;

        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        InspectionRecieve objInspectionRecieve;
        UserAuthentication objUserAuthentication;
        AssortMaster objAssort;
        SieveMaster objSieve;
        RateMaster objRate;
        SaleInvoice objSaleInvoice;

        DataTable DtControlSettings;
        DataTable m_dtbAssorts;
        DataTable m_dtbSieve;
        DataTable m_dtbInscriptionDetails;
        DataTable m_dtbCurrency;
        DataTable m_dtbDetails;
        DataTable m_dtbMemo;
        DataTable m_dtbSeller;
        DataTable m_dtbCurrencyType;

        int m_inspection_id;
        int m_numForm_id;
        int IntRes;
        decimal m_numCurrentRate;
        decimal m_numSummRate;
        decimal m_numSummLossRate;
        decimal m_numIssueCarat;

        bool m_blnadd;
        bool m_blnsave;

        #endregion

        #region Constructor
        public FrmInspectionRecieve()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            _NextEnteredControl = new Control();
            _tabControls = new List<Control>();

            objInspectionRecieve = new InspectionRecieve();
            objUserAuthentication = new UserAuthentication();
            objAssort = new AssortMaster();
            objSieve = new SieveMaster();
            objRate = new RateMaster();
            objSaleInvoice = new SaleInvoice();

            DtControlSettings = new DataTable();
            m_dtbAssorts = new DataTable();
            m_dtbSieve = new DataTable();
            m_dtbInscriptionDetails = new DataTable();
            m_dtbCurrency = new DataTable();
            m_dtbDetails = new DataTable();
            m_dtbMemo = new DataTable();
            m_dtbSeller = new DataTable();
            m_dtbCurrencyType = new DataTable();

            m_inspection_id = 0;
            m_numForm_id = 0;
            IntRes = 0;

            m_numCurrentRate = 0;
            m_numIssueCarat = 0;

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
            objBOFormEvents.ObjToDispose.Add(objInspectionRecieve);
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
                    lblpcs.Visible = false;
                    txtOsPcs.Visible = false;
                    lblcrt.Visible = false;
                    txtOsCrt.Visible = false;
                }
                else
                {
                    ClearDetails();
                    ttlbInspectionRecieve.SelectedTabPage = tblMemoRecieveList;
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
                txtRejPcs.Text = string.Empty;
                txtRejCarat.Text = string.Empty;
                txtRejectionPer.Text = string.Empty;
                txtRate.Text = string.Empty;
                txtLossCarat.Text = string.Empty;
                txtLossPcs.Text = string.Empty;
                txtLossRate.Text = string.Empty;
                txtLossAmount.Text = string.Empty;
                txtAmount.Text = string.Empty;
                txtDiscPer.Text = string.Empty;
                txtDiscAmt.Text = string.Empty;
                txtNetAmount.Text = string.Empty;
                txtIssueCarat.Text = string.Empty;
                txtIssuePcs.Text = string.Empty;
                txtCarat.Text = string.Empty;

                txtCarat.Focus();
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
                btnSave.Enabled = true;
                m_blnsave = false;
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
            backgroundWorker_MemoReceive.RunWorkerAsync();

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
                txtAmount.Text = string.Format("{0:0.00}", (Val.ToDecimal(txtCarat.Text) + Val.ToDecimal(txtRejCarat.Text)) * Val.ToDecimal(txtRate.Text));

                txtRejCarat.Text = "0";
                txtRejectionPer.Text = "0";
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
                txtAmount.Text = string.Format("{0:0.00}", (Val.ToDecimal(txtCarat.Text) + Val.ToDecimal(txtRejCarat.Text)) * Val.ToDecimal(txtRate.Text));
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void txtRejCarat_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtAmount.Text = string.Format("{0:0.00}", (Val.ToDecimal(txtCarat.Text) + Val.ToDecimal(txtRejCarat.Text)) * Val.ToDecimal(txtRate.Text));

                if (Val.ToDecimal(txtIssueCarat.Text) != 0)
                {
                    txtRejectionPer.Text = Val.ToString(Math.Round((Val.ToDecimal(txtRejCarat.Text) / Val.ToDecimal(txtIssueCarat.Text) * 100), 3));
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
            e.Graphics.DrawLine(pen, 0, 60, 1500, 60);
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
                    m_numCurrentRate = Val.ToDecimal(p_numStockRate);
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

        private void txtRejectionPer_Validated(object sender, EventArgs e)
        {
            if (txtRejectionPer.Text != "")
            {
                txtRejCarat.Text = Val.ToString(Math.Round((Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRejectionPer.Text) / 100), 3));
            }
        }
        private void backgroundWorker_MemoReceive_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Inspection_RecieveProperty objInspectionRecieve_Property = new Inspection_RecieveProperty();
            try
            {
                IntRes = 0;
                int IntCounter = 0;
                int Count = 0;
                int TotalCount = m_dtbInscriptionDetails.Rows.Count;
                Int64 NewmInvoiceid = 0;

                Conn = new BeginTranConnection(true, false);

                objInspectionRecieve_Property.inspection_no = Val.ToString(txtInspectionNo.Text);
                objInspectionRecieve_Property.company_id = Val.ToInt(GlobalDec.gEmployeeProperty.company_id);
                objInspectionRecieve_Property.branch_id = Val.ToInt(GlobalDec.gEmployeeProperty.branch_id);
                objInspectionRecieve_Property.location_id = Val.ToInt(GlobalDec.gEmployeeProperty.location_id);
                objInspectionRecieve_Property.department_id = Val.ToInt(GlobalDec.gEmployeeProperty.department_id);

                objInspectionRecieve_Property.inspection_date = Val.DBDate(dtpInspectionDate.Text);
                objInspectionRecieve_Property.delivery_type_id = Val.ToInt(lueDeliveryType.EditValue);
                objInspectionRecieve_Property.remarks = Val.ToString(txtRemark.Text);
                objInspectionRecieve_Property.form_id = m_numForm_id;

                objInspectionRecieve_Property.Party_Id = Val.ToInt(lueParty.EditValue);
                objInspectionRecieve_Property.Broker_Id = Val.ToInt(lueBroker.EditValue);

                objInspectionRecieve_Property.Special_Remark = Val.ToString(txtSpecialRemark.Text);
                objInspectionRecieve_Property.Client_Remark = Val.ToString(txtClientRemark.Text);
                objInspectionRecieve_Property.Payment_Remark = Val.ToString(txtPaymentRemark.Text);
                objInspectionRecieve_Property.term_days = Val.ToInt(txtTermDays.Text);
                objInspectionRecieve_Property.due_date = Val.DBDate(dtpDueDate.Text);
                objInspectionRecieve_Property.final_days = Val.ToInt(txtFinalTermDays.Text);
                objInspectionRecieve_Property.final_due_date = Val.DBDate(dtpFinalDueDate.Text);
                objInspectionRecieve_Property.exchange_rate = Val.ToDecimal(txtExchangeRate.Text);
                objInspectionRecieve_Property.rate_type = Val.ToString(lueCurrency.Text);
                objInspectionRecieve_Property.demand_no = Val.ToString(txtDemandNo.Text);

                //if (m_dtbInscriptionDetails.Select("flag_check = true").Length > 0)
                //{
                //    m_dtbInscriptionDetails = m_dtbInscriptionDetails.Select("flag_check = true").CopyToDataTable();
                foreach (DataRow drw in m_dtbInscriptionDetails.Rows)
                {
                    objInspectionRecieve_Property.inspection_id = Val.ToInt(drw["inspection_id"]);
                    objInspectionRecieve_Property.assort_id = Val.ToInt(drw["assort_id"]);
                    objInspectionRecieve_Property.sieve_id = Val.ToInt(drw["sieve_id"]);
                    objInspectionRecieve_Property.sub_sieve_id = Val.ToInt(drw["sub_sieve_id"]);
                    objInspectionRecieve_Property.rec_pcs = Val.ToInt(drw["return_pcs"]);
                    objInspectionRecieve_Property.rec_carat = Val.ToDecimal(drw["return_carat"]);
                    objInspectionRecieve_Property.rej_pcs = Val.ToInt(drw["rej_pcs"]);
                    objInspectionRecieve_Property.rej_carat = Val.ToDecimal(drw["rej_carat"]);
                    objInspectionRecieve_Property.rej_per = Val.ToDecimal(drw["rejection_per"]);
                    objInspectionRecieve_Property.loss_pcs = Val.ToInt(drw["loss_pcs"]);
                    objInspectionRecieve_Property.loss_carat = Val.ToDecimal(drw["loss_carat"]);
                    objInspectionRecieve_Property.loss_rate = Val.ToDecimal(drw["loss_rate"]);
                    objInspectionRecieve_Property.loss_amount = Val.ToDecimal(drw["loss_amount"]);
                    objInspectionRecieve_Property.rec_rate = Val.ToDecimal(drw["rate"]);
                    objInspectionRecieve_Property.rec_amount = Val.ToDecimal(drw["amount"]);
                    objInspectionRecieve_Property.discount_per = Val.ToDecimal(drw["discount_per"]);
                    objInspectionRecieve_Property.discount_amount = Val.ToDecimal(drw["discount_amt"]);
                    objInspectionRecieve_Property.net_amount = Val.ToDecimal(drw["net_amount"]);
                    objInspectionRecieve_Property.current_rate = Val.ToDecimal(drw["current_rate"]);
                    objInspectionRecieve_Property.current_amount = Val.ToDecimal(drw["current_amount"]);
                    objInspectionRecieve_Property.inspection_master_id = Val.ToInt(drw["inspection_master_id"]);
                    objInspectionRecieve_Property.flag = Val.ToInt(drw["flag"]);
                    objInspectionRecieve_Property.purchase_rate = Val.ToDecimal(drw["purchase_rate"]);
                    objInspectionRecieve_Property.purchase_amount = Val.ToDecimal(drw["purchase_amount"]);
                    objInspectionRecieve_Property.demand_id = Val.ToInt64(drw["demand_id"]);

                    objInspectionRecieve_Property.IS_Purity = Val.ToBoolean(drw["is_purity"]);
                    objInspectionRecieve_Property.IS_Color = Val.ToBoolean(drw["is_color"]);
                    objInspectionRecieve_Property.IS_Price = Val.ToBoolean(drw["is_price"]);
                    objInspectionRecieve_Property.IS_Cut = Val.ToBoolean(drw["is_cut"]);
                    objInspectionRecieve_Property.IS_Size = Val.ToBoolean(drw["is_Size"]);
                    objInspectionRecieve_Property.Party_Problem = Val.ToBoolean(drw["party_problem"]);
                    objInspectionRecieve_Property.IS_NotOnHand = Val.ToBoolean(drw["is_netting"]);
                    objInspectionRecieve_Property.IS_Sold = Val.ToBoolean(drw["is_sold"]);
                    objInspectionRecieve_Property.IS_Offer = Val.ToBoolean(drw["is_offer"]);
                    objInspectionRecieve_Property.IS_PacketPending = Val.ToBoolean(drw["is_packet_pending"]);
                    objInspectionRecieve_Property.IS_No_Mal = Val.ToBoolean(drw["is_no_mal"]);
                    objInspectionRecieve_Property.IS_Pending_Demand = Val.ToBoolean(drw["is_pending_demand"]);
                    objInspectionRecieve_Property.IS_QTY = Val.ToBoolean(drw["is_qty"]);
                    objInspectionRecieve_Property.IS_Service = Val.ToBoolean(drw["is_service"]);
                    objInspectionRecieve_Property.IS_Selection = Val.ToBoolean(drw["is_selection"]);
                    objInspectionRecieve_Property.Order_Carat = Val.ToDecimal(drw["order_carat"]);
                    objInspectionRecieve_Property.check_flag = Val.ToBoolean(drw["flag_check"]);
                    objInspectionRecieve_Property.offer_rate = Val.ToDecimal(drw["offer_rate"]);

                    objInspectionRecieve_Property = objInspectionRecieve.Save(objInspectionRecieve_Property, DLL.GlobalDec.EnumTran.Continue, Conn);
                    NewmInvoiceid = Val.ToInt64(objInspectionRecieve_Property.inspection_id);

                    Count++;
                    IntCounter++;
                    IntRes++;
                    SetControlPropertyValue(lblProgressCount, "Text", Count.ToString() + "" + "/" + "" + TotalCount.ToString() + " Completed....");
                }
                //}
                //else
                //{
                //    Global.Message("Please Select Any One Inspection Receive Data..");
                //    Conn.Inter1.Commit();
                //    return;
                //}               
                Conn.Inter1.Commit();
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
                objInspectionRecieve_Property = null;
            }
        }
        private void backgroundWorker_MemoReceive_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                panelProgress.Visible = false;
                if (IntRes > 0)
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Inspection Data Save Successfully");
                    }
                    else
                    {
                        Global.Confirm("Inspection Data Update Successfully");
                    }
                    m_dtbMemo = m_dtbInscriptionDetails.Copy();
                    ClearDetails();
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
        private void dgvMemoRecieveDetails_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                if (Val.ToDecimal(clmRecAmount.SummaryItem.SummaryValue) > 0 && Val.ToDecimal(clmSaleCarat.SummaryItem.SummaryValue) > 0)
                {
                    m_numSummRate = Math.Round((Val.ToDecimal(clmRecAmount.SummaryItem.SummaryValue) / Val.ToDecimal(clmSaleCarat.SummaryItem.SummaryValue)), 3, MidpointRounding.AwayFromZero);
                }
                if (Val.ToDecimal(ClmLossAmount.SummaryItem.SummaryValue) > 0 && Val.ToDecimal(clmLossCarat.SummaryItem.SummaryValue) > 0)
                {
                    m_numSummLossRate = Math.Round((Val.ToDecimal(ClmLossAmount.SummaryItem.SummaryValue) / Val.ToDecimal(clmLossCarat.SummaryItem.SummaryValue)), 3, MidpointRounding.AwayFromZero);
                }
                else
                {
                    m_numSummRate = 0;
                    m_numSummLossRate = 0;
                }
                if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == "rate")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                        e.TotalValue = m_numSummRate;
                }
                if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == "loss_rate")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                        e.TotalValue = m_numSummLossRate;
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        private void dgvMemoRecieveDetails_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                objInspectionRecieve = new InspectionRecieve();
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvInspectionRecieveDetails.GetDataRow(e.RowHandle);
                        btnAdd.Text = "&Update";
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt32(Drow["inspection_id"]);
                        lueSieveName.Tag = Val.ToInt64(Drow["sieve_id"]);
                        lueSieveName.Text = Val.ToString(Drow["sieve_name"]);
                        lueSubSieveName.Tag = Val.ToInt64(Drow["sub_sieve_id"]);
                        lueSubSieveName.Text = Val.ToString(Drow["sub_sieve_name"]);
                        lueAssortName.Tag = Val.ToInt64(Drow["assort_id"]);
                        lueAssortName.Text = Val.ToString(Drow["assort_name"]);
                        txtPcs.Text = Val.ToString(Drow["return_pcs"]);
                        txtCarat.Text = Val.ToString(Drow["return_carat"]);
                        txtRejPcs.Text = Val.ToString(Drow["rej_pcs"]);
                        txtRejCarat.Text = Val.ToString(Drow["rej_carat"]);
                        txtLossCarat.Text = Val.ToString(Drow["loss_carat"]);
                        txtLossPcs.Text = Val.ToString(Drow["loss_pcs"]);
                        txtLossRate.Text = Val.ToString(Drow["loss_rate"]);
                        txtLossAmount.Text = Val.ToString(Drow["loss_amount"]);
                        txtRate.Text = Val.ToString(Drow["rate"]);
                        txtAmount.Text = Val.ToString(Drow["amount"]);
                        txtDiscPer.Text = Val.ToString(Drow["discount_per"]);
                        txtDiscAmt.Text = Val.ToString(Drow["discount_amt"]);
                        txtNetAmount.Text = Val.ToString(Drow["net_amount"]);
                        txtRejectionPer.Text = Val.ToString(Drow["rejection_per"]);
                        txtIssuePcs.Text = Val.ToString(Drow["outstanding_pcs"]);
                        txtIssueCarat.Text = Val.ToString(Drow["outstanding_carat"]);
                        m_inspection_id = Val.ToInt(Drow["inspection_id"]);
                        m_numIssueCarat = Val.ToDecimal(Drow["outstanding_carat"]);
                        txtPcs.Focus();
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
                Global.LOOKUPParty(lueParty);
                Global.LOOKUPBroker(lueBroker);
                Global.LOOKUPDeliveryType(lueDeliveryType);

                m_dtbSeller = objSaleInvoice.GetSellerName("Seller");

                lueSeller.Properties.DataSource = m_dtbSeller;
                lueSeller.Properties.ValueMember = "employee_id";
                lueSeller.Properties.DisplayMember = "employee_name";

                m_dtbCurrencyType = Global.CurrencyType();
                lueCurrency.Properties.DataSource = m_dtbCurrencyType;
                lueCurrency.Properties.ValueMember = "currency_id";
                lueCurrency.Properties.DisplayMember = "currency";
                lueCurrency.EditValue = GlobalDec.gEmployeeProperty.currency_id;

                m_dtbAssorts = objAssort.GetData(1);
                lueAssortName.Properties.DataSource = m_dtbAssorts;
                lueAssortName.Properties.ValueMember = "assort_id";
                lueAssortName.Properties.DisplayMember = "assort_name";

                m_dtbSieve = objSieve.GetData(1);
                lueSieveName.Properties.DataSource = m_dtbSieve;
                lueSieveName.Properties.ValueMember = "sieve_id";
                lueSieveName.Properties.DisplayMember = "sieve_name";

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

                DataRow[] dr = m_dtbInscriptionDetails.Select("sieve_id = " + Val.ToInt(lueSieveName.EditValue) + " AND assort_id = " + Val.ToInt(lueAssortName.Tag));

                if (m_dtbInscriptionDetails.Select("assort_id ='" + Val.ToInt(lueAssortName.Tag) + "' AND sieve_id ='" + Val.ToInt(lueSieveName.EditValue) + "'").Length > 0)
                {
                    for (int i = 0; i < m_dtbInscriptionDetails.Rows.Count; i++)
                    {
                        if (m_dtbInscriptionDetails.Select("inspection_id ='" + m_inspection_id + "'").Length > 0)
                        {
                            if (m_dtbInscriptionDetails.Rows[i]["inspection_id"].ToString() == m_inspection_id.ToString())
                            {
                                m_dtbInscriptionDetails.Rows[i]["rec_pcs"] = Val.ToInt(txtPcs.Text).ToString();
                                m_dtbInscriptionDetails.Rows[i]["rec_carat"] = Val.ToDecimal(txtCarat.Text);
                                m_dtbInscriptionDetails.Rows[i]["rej_pcs"] = Val.ToInt(txtRejPcs.Text).ToString();
                                m_dtbInscriptionDetails.Rows[i]["rej_carat"] = Val.ToDecimal(txtRejCarat.Text);
                                m_dtbInscriptionDetails.Rows[i]["loss_pcs"] = Val.ToInt(txtLossPcs.Text);
                                m_dtbInscriptionDetails.Rows[i]["loss_carat"] = Val.ToDecimal(txtLossCarat.Text);
                                m_dtbInscriptionDetails.Rows[i]["loss_rate"] = Val.ToDecimal(txtLossRate.Text);
                                m_dtbInscriptionDetails.Rows[i]["loss_amount"] = Val.ToDecimal(txtLossAmount.Text);
                                m_dtbInscriptionDetails.Rows[i]["rate"] = Val.ToDecimal(txtRate.Text);
                                m_dtbInscriptionDetails.Rows[i]["amount"] = Val.ToDecimal(txtAmount.Text);
                                m_dtbInscriptionDetails.Rows[i]["discount_per"] = Val.ToDecimal(txtDiscPer.Text);
                                m_dtbInscriptionDetails.Rows[i]["discount_amt"] = Val.ToDecimal(txtDiscAmt.Text);
                                m_dtbInscriptionDetails.Rows[i]["net_amount"] = Val.ToDecimal(txtNetAmount.Text);
                                m_dtbInscriptionDetails.Rows[i]["sale_amount"] = (Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text));
                                m_dtbInscriptionDetails.Rows[i]["rejection_per"] = Val.ToDecimal(txtRejectionPer.Text);

                                if (Val.ToDecimal(txtCarat.Text) > 0 || Val.ToDecimal(txtRejCarat.Text) > 0 || Val.ToDecimal(txtLossCarat.Text) > 0)
                                {
                                    m_dtbInscriptionDetails.Rows[i]["flag"] = 1;
                                }
                                else
                                {
                                    m_dtbInscriptionDetails.Rows[i]["flag"] = 0;
                                }
                                m_dtbInscriptionDetails.Rows[i]["current_rate"] = m_numCurrentRate;
                                m_dtbInscriptionDetails.Rows[i]["current_amount"] = m_numCurrentRate * (Val.ToDecimal(txtCarat.Text) + Val.ToDecimal(txtRejCarat.Text));

                                m_dtbInscriptionDetails.Rows[i]["purchase_amount"] = Val.ToDecimal(Val.ToDecimal(m_dtbInscriptionDetails.Rows[i]["purchase_rate"]) * Val.ToDecimal(txtCarat.Text));
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
                if (m_blnsave)
                {
                    var result = DateTime.Compare(Convert.ToDateTime(dtpInspectionDate.Text), DateTime.Today);
                    if (result > 0)
                    {
                        lstError.Add(new ListError(5, "Date Not Be Greater Than Today Date."));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            dtpInspectionDate.Focus();
                        }

                    }
                    if (m_dtbInscriptionDetails.Rows.Count == 0)
                    {
                        lstError.Add(new ListError(22, "Record"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                        }
                    }
                    if (Val.ToDecimal(clmSaleCarat.SummaryItem.SummaryValue) <= 0 && Val.ToDecimal(clmRejCarat.SummaryItem.SummaryValue) <= 0 && Val.ToDecimal(clmLossCarat.SummaryItem.SummaryValue) <= 0 && Val.ToDecimal(clmLossCarat.SummaryItem.SummaryValue) <= 0)
                    {
                        lstError.Add(new ListError(5, "Please enter Sale/Rejection/Loss Carat."));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                        }
                    }
                    decimal OS_Carat = Val.ToDecimal(txtOsCrt.Text);
                    decimal Total_Carat = Val.ToDecimal(clmSaleCarat.SummaryItem.SummaryValue) + Val.ToDecimal(clmRejCarat.SummaryItem.SummaryValue) + Val.ToDecimal(clmLossCarat.SummaryItem.SummaryValue);

                    if (OS_Carat != Total_Carat)
                    {
                        lstError.Add(new ListError(5, "Please difference between OS Carat And Sale/Rejection/Loss Carat."));
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

                    if (Val.ToDouble(txtCarat.Text) == 0 && Val.ToDouble(txtRejCarat.Text) == 0 && Val.ToDouble(txtLossCarat.Text) == 0)
                    {
                        lstError.Add(new ListError(12, "Carat/ Rejection/ Loss"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtCarat.Focus();
                        }
                    }

                    if (Val.ToDouble(txtCarat.Text) < 0 || Val.ToDouble(txtRejCarat.Text) < 0 || Val.ToDouble(txtLossCarat.Text) < 0)
                    {
                        lstError.Add(new ListError(34, "Carat/ Rejection/ Loss"));
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

                    if (Val.ToDouble(txtAmount.Text) == 0 && Val.ToDouble(txtLossAmount.Text) == 0)
                    {
                        lstError.Add(new ListError(12, "Amount/ Loss Amount "));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtAmount.Focus();
                        }
                    }
                    if ((Val.ToInt(txtPcs.Text.ToString()) + Val.ToInt(txtRejPcs.Text.ToString()) + Val.ToInt(txtLossPcs.Text.ToString())) > Val.ToInt(txtOsPcs.Text.ToString()))
                    {
                        lstError.Add(new ListError(33, "Recieve pcs"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtPcs.Focus();
                        }
                    }
                    if ((Val.ToDecimal(txtCarat.Text.ToString()) + Val.ToDecimal(txtRejCarat.Text.ToString()) + Val.ToDecimal(txtLossCarat.Text.ToString())) < Val.ToDecimal(txtIssueCarat.Text.ToString()))
                    {
                        lstError.Add(new ListError(34, "Recieve carat"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtCarat.Focus();
                        }
                    }
                    if ((Val.ToDecimal(txtCarat.Text.ToString()) + Val.ToDecimal(txtRejCarat.Text.ToString()) + Val.ToDecimal(txtLossCarat.Text.ToString())) > Val.ToDecimal(txtIssueCarat.Text.ToString()))
                    {
                        lstError.Add(new ListError(34, "Recieve carat"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtCarat.Focus();
                        }
                    }
                    if (txtAmount.Text == string.Empty)
                    {
                        lstError.Add(new ListError(12, "Amount"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtAmount.Focus();
                        }
                    }
                    decimal Total_ICarat = Val.ToDecimal(txtCarat.Text) + Val.ToDecimal(txtRejCarat.Text) + Val.ToDecimal(txtLossCarat.Text);

                    if (m_numIssueCarat != Total_ICarat)
                    {
                        lstError.Add(new ListError(5, "Please difference between Issue Carat And Sale/Rejection/Loss Carat."));
                        if (!blnFocus)
                        {
                            blnFocus = true;
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
        //private bool SaveDetails()
        //{
        //    bool blnReturn = true;
        //    Inspection_RecieveProperty objInspectionRecieve_Property = new Inspection_RecieveProperty();
        //    try
        //    {
        //        Int64 NewmInvoiceid = 0;

        //        if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
        //        {
        //            Conn = new BeginTranConnection(true, false);
        //        }
        //        else
        //        {
        //            Conn = new BeginTranConnection(false, true);
        //        }

        //        objInspectionRecieve_Property.inspection_no = Val.ToString(txtInspectionNo.Text);
        //        objInspectionRecieve_Property.company_id = Val.ToInt(GlobalDec.gEmployeeProperty.company_id);
        //        objInspectionRecieve_Property.branch_id = Val.ToInt(GlobalDec.gEmployeeProperty.branch_id);
        //        objInspectionRecieve_Property.location_id = Val.ToInt(GlobalDec.gEmployeeProperty.location_id);
        //        objInspectionRecieve_Property.department_id = Val.ToInt(GlobalDec.gEmployeeProperty.department_id);

        //        objInspectionRecieve_Property.inspection_date = Val.DBDate(dtpInspectionDate.Text);
        //        objInspectionRecieve_Property.delivery_type_id = Val.ToInt(lueDeliveryType.EditValue);
        //        objInspectionRecieve_Property.remarks = Val.ToString(txtRemark.Text);
        //        objInspectionRecieve_Property.form_id = m_numForm_id;

        //        objInspectionRecieve_Property.Party_Id = Val.ToInt(lueParty.EditValue);
        //        objInspectionRecieve_Property.Broker_Id = Val.ToInt(lueBroker.EditValue);

        //        objInspectionRecieve_Property.Special_Remark = Val.ToString(txtSpecialRemark.Text);
        //        objInspectionRecieve_Property.Client_Remark = Val.ToString(txtClientRemark.Text);
        //        objInspectionRecieve_Property.Payment_Remark = Val.ToString(txtPaymentRemark.Text);
        //        objInspectionRecieve_Property.term_days = Val.ToInt(txtTermDays.Text);

        //        foreach (DataRow drw in m_dtbInscriptionDetails.Rows)
        //        {
        //            objInspectionRecieve_Property.inspection_id = Val.ToInt(drw["inspection_id"]);
        //            objInspectionRecieve_Property.inspection_master_id = Val.ToInt(drw["inspection_master_id"]);
        //            objInspectionRecieve_Property.assort_id = Val.ToInt(drw["assort_id"]);
        //            objInspectionRecieve_Property.sieve_id = Val.ToInt(drw["sieve_id"]);
        //            objInspectionRecieve_Property.sub_sieve_id = Val.ToInt(drw["sub_sieve_id"]);
        //            objInspectionRecieve_Property.rej_pcs = Val.ToInt(drw["rej_pcs"]);
        //            objInspectionRecieve_Property.rej_carat = Val.ToDecimal(drw["rej_carat"]);
        //            objInspectionRecieve_Property.loss_pcs = Val.ToInt(drw["loss_pcs"]);
        //            objInspectionRecieve_Property.loss_carat = Val.ToDecimal(drw["loss_carat"]);
        //            objInspectionRecieve_Property.loss_rate = Val.ToDecimal(drw["loss_rate"]);
        //            objInspectionRecieve_Property.loss_amount = Val.ToDecimal(drw["loss_amount"]);
        //            objInspectionRecieve_Property.rec_rate = Val.ToDecimal(drw["rate"]);
        //            objInspectionRecieve_Property.rec_amount = Val.ToDecimal(drw["amount"]);
        //            objInspectionRecieve_Property.discount_per = Val.ToDecimal(drw["discount_rate"]);
        //            objInspectionRecieve_Property.discount_amount = Val.ToDecimal(drw["discount_amount"]);
        //            objInspectionRecieve_Property.net_amount = Val.ToDecimal(drw["net_amount"]);
        //            objInspectionRecieve_Property.current_rate = Val.ToDecimal(drw["current_rate"]);
        //            objInspectionRecieve_Property.current_amount = Val.ToDecimal(drw["current_amount"]);
        //            objInspectionRecieve_Property.flag = Val.ToInt(drw["flag"]);

        //            objInspectionRecieve_Property = objInspectionRecieve.Save(objInspectionRecieve_Property, DLL.GlobalDec.EnumTran.Continue, Conn);
        //            NewmInvoiceid = Val.ToInt64(objInspectionRecieve_Property.inspection_id);
        //        }

        //        if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
        //        {
        //            Conn.Inter1.Commit();
        //        }
        //        else
        //        {
        //            Conn.Inter2.Commit();
        //        }
        //        if (NewmInvoiceid == 0)
        //        {
        //            Global.Confirm("Error In Inspection");
        //            txtInspectionNo.Focus();
        //        }
        //        else
        //        {
        //            if (Val.ToInt(lblMode.Tag) == 0)
        //            {
        //                Global.Confirm("Inspection Data Save Successfully");
        //            }
        //            else
        //            {
        //                Global.Confirm("Inspection Data Update Successfully");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        IntRes = -1;
        //        if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
        //        {
        //            Conn.Inter1.Rollback();
        //        }
        //        else
        //        {
        //            Conn.Inter2.Rollback();
        //        }
        //        Conn = null;
        //        Global.Message(ex.ToString());
        //        if (ex.InnerException != null)
        //        {
        //            Global.Message(ex.InnerException.ToString());
        //        }
        //        blnReturn = false;
        //    }
        //    finally
        //    {
        //        objInspectionRecieve_Property = null;
        //    }

        //    return blnReturn;
        //}
        private bool ClearDetails()
        {
            bool blnReturn = true;
            try
            {
                lblMode.Tag = null;
                lueParty.EditValue = System.DBNull.Value;
                lueDeliveryType.EditValue = Convert.ToInt32(GlobalDec.gEmployeeProperty.delivery_type_id);
                txtInspectionNo.Text = string.Empty;
                lueBroker.EditValue = System.DBNull.Value;

                dtpInspectionDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpInspectionDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpInspectionDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpInspectionDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpInspectionDate.EditValue = DateTime.Now;

                lueAssortName.EditValue = System.DBNull.Value;
                lueSieveName.EditValue = System.DBNull.Value;
                lueSubSieveName.EditValue = System.DBNull.Value;

                txtTermDays.Text = string.Empty;
                txtPcs.Text = "0";
                txtCarat.Text = "0";
                txtRejPcs.Text = "0";
                txtRejCarat.Text = "0";
                txtRejectionPer.Text = "0";
                txtRate.Text = "0";
                txtAmount.Text = "0";
                txtDiscPer.Text = "0";
                txtDiscAmt.Text = "0";
                txtNetAmount.Text = "0";
                txtLossCarat.Text = "0";
                txtLossPcs.Text = "0";
                txtLossRate.Text = "0";
                txtLossAmount.Text = "0";
                txtRemark.Text = string.Empty;
                txtSpecialRemark.Text = string.Empty;
                txtPaymentRemark.Text = string.Empty;
                txtClientRemark.Text = string.Empty;
                txtOsPcs.Text = string.Empty;
                txtOsCrt.Text = string.Empty;
                txtIssueCarat.Text = string.Empty;
                txtIssuePcs.Text = string.Empty;
                txtDemandNo.Text = string.Empty;

                m_dtbInscriptionDetails.Clear();
                grdInspectionRecieveDetails.DataSource = null;
                txtInspectionNo.Focus();
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
            objInspectionRecieve = new InspectionRecieve();
            bool blnReturn = true;
            DateTime datFromDate = DateTime.MinValue;
            DateTime datToDate = DateTime.MinValue;
            try
            {
                m_dtbDetails = objInspectionRecieve.GetData(Val.DBDate(dtpFromDate.Text), Val.DBDate(dtpToDate.Text));

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
                objInspectionRecieve = null;
            }

            return blnReturn;
        }
        public void GetData()
        {
            try
            {
                DataTable dtbdetails = new DataTable();

                dtbdetails = objInspectionRecieve.GetTotalInspectionCarat(GlobalDec.gEmployeeProperty.company_id, GlobalDec.gEmployeeProperty.branch_id, GlobalDec.gEmployeeProperty.location_id, GlobalDec.gEmployeeProperty.department_id, Val.ToString(txtInspectionNo.Text));

                if (dtbdetails.Rows.Count > 0)
                {
                    string strFilter = "outstanding_carat > 0";
                    dtbdetails.DefaultView.RowFilter = strFilter;

                    m_dtbInscriptionDetails = dtbdetails.DefaultView.ToTable();

                    DialogResult result = MessageBox.Show("Do You want to recieve this Inspection?", "Confirmation", MessageBoxButtons.YesNoCancel);
                    if (result == DialogResult.Yes)
                    {
                        grdInspectionRecieveDetails.DataSource = m_dtbInscriptionDetails;

                        //for (int i = 0; i <= dgvInspectionRecieveDetails.Rows.Count; i++)
                        //{

                        //}

                        lueParty.EditValue = Val.ToInt(m_dtbInscriptionDetails.Rows[0]["party_id"]);
                        lueBroker.EditValue = Val.ToInt(m_dtbInscriptionDetails.Rows[0]["broker_id"]);
                        lueDeliveryType.EditValue = Val.ToInt(m_dtbInscriptionDetails.Rows[0]["delivery_type_id"]);
                        lueSeller.EditValue = Val.ToInt(m_dtbInscriptionDetails.Rows[0]["seller_id"]);

                        txtBrokeragePer.Text = Val.ToDecimal(m_dtbInscriptionDetails.Rows[0]["broker_per"]).ToString();
                        txtBrokerageAmt.Text = Val.ToDecimal(m_dtbInscriptionDetails.Rows[0]["broker_amt"]).ToString();

                        lblpcs.Visible = true;
                        txtOsPcs.Visible = true;
                        lblcrt.Visible = true;
                        txtOsCrt.Visible = true;
                        txtTermDays.Text = Val.ToString(m_dtbInscriptionDetails.Rows[0]["term_days"]);
                        dtpDueDate.Text = Val.DBDate(Val.ToString(m_dtbInscriptionDetails.Rows[0]["due_date"]));
                        txtExchangeRate.Text = Val.ToString(m_dtbInscriptionDetails.Rows[0]["exchange_rate"]);
                        lueCurrency.Text = Val.ToString(m_dtbInscriptionDetails.Rows[0]["currency_type"]);
                        txtFinalTermDays.Text = Val.ToString(m_dtbInscriptionDetails.Rows[0]["final_term_days"]);
                        dtpFinalDueDate.Text = Val.DBDate(Val.ToString(m_dtbInscriptionDetails.Rows[0]["final_due_date"]));
                        txtDemandNo.Text = Val.ToString(m_dtbInscriptionDetails.Rows[0]["demand_no"]);
                        //txtOsPcs.Text = Val.ToString(Val.ToDecimal(clmOsPcs.SummaryItem.SummaryValue));
                        //txtOsCrt.Text = Val.ToString(Val.ToDecimal(clmOsCarat.SummaryItem.SummaryValue));
                        //txtIssuePcs.Text = Val.ToString(Val.ToDecimal(m_dtbMemoDetails.Compute("SUM(issue_pcs)", "")));
                        //txtIssueCarat.Text = Val.ToString(Val.ToDecimal(m_dtbMemoDetails.Compute("SUM(issue_carat)", "")));

                        DataTable dtbOSCarat = new DataTable();
                        dtbOSCarat = objInspectionRecieve.GetOSCarat(GlobalDec.gEmployeeProperty.company_id, GlobalDec.gEmployeeProperty.branch_id, GlobalDec.gEmployeeProperty.location_id, GlobalDec.gEmployeeProperty.department_id, Val.ToString(txtInspectionNo.Text));

                        if (dtbOSCarat.Rows.Count > 0)
                        {
                            txtOsCrt.Text = Val.ToString(Val.ToDecimal(dtbOSCarat.Rows[0]["os_carat"]));
                            txtOsPcs.Text = Val.ToString(Val.ToDecimal(dtbOSCarat.Rows[0]["os_pcs"]));
                        }
                    }
                    else
                    {
                        txtInspectionNo.Focus();
                        lblpcs.Visible = false;
                        txtOsPcs.Visible = false;
                        lblcrt.Visible = false;
                        txtOsCrt.Visible = false;
                    }
                }
                else
                {
                    lblpcs.Visible = false;
                    txtOsPcs.Visible = false;
                    lblcrt.Visible = false;
                    txtOsCrt.Visible = false;
                }
                dgvInspectionRecieveDetails.BestFitColumns();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        public void FillMemoInvoice(DataTable InspectionDT)
        {
            FrmMemoIssue frmMemo = new FrmMemoIssue();
            Assembly frmAssembly = Assembly.LoadFile(Application.ExecutablePath);

            foreach (Type type in frmAssembly.GetTypes())
            {
                string type1 = type.Name.ToString().ToUpper();
                if (type.BaseType == typeof(DevExpress.XtraEditors.XtraForm))
                {
                    if (type.Name.ToString().ToUpper() == "FRMMEMOISSUE")
                    {
                        XtraForm frmShow = (XtraForm)frmAssembly.CreateInstance(type.ToString());
                        frmShow.MdiParent = Global.gMainFormRef;
                        frmShow.GetType().GetMethod("ShowForm_New").Invoke(frmShow, new object[] { InspectionDT });
                        break;
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
                            dgvInspectionRecieveDetails.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvInspectionRecieveDetails.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvInspectionRecieveDetails.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvInspectionRecieveDetails.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvInspectionRecieveDetails.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvInspectionRecieveDetails.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvInspectionRecieveDetails.ExportToCsv(Filepath);
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

        private void txtBrokeragePer_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                decimal Brokerage_amt = Math.Round((Val.ToDecimal(clmRecAmount.SummaryItem.SummaryValue) - Val.ToDecimal(txtBrokerageAmt.Text)) * Val.ToDecimal(txtBrokeragePer.Text) / 100, 0);
                txtBrokerageAmt.Text = Brokerage_amt.ToString();
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void txtLossCarat_EditValueChanged(object sender, EventArgs e)
        {
            try
            {

                txtLossRate.Text = string.Format("{0:0.00}", (Val.ToDecimal(txtRate.Text)));
                txtLossAmount.Text = string.Format("{0:0.00}", (Val.ToDecimal(txtLossCarat.Text)) * Val.ToDecimal(txtLossRate.Text));

                txtRejCarat.Text = Val.ToString(Val.ToDecimal(txtIssueCarat.Text) - (Val.ToDecimal(txtCarat.Text) + Val.ToDecimal(txtLossCarat.Text)));

            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void txtInspectionNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    //if (Flag == 0)
                    GetData();
                    // m_blncheck = false;
                    //Flag = 0;
                    txtPcs.Text = string.Empty;
                    txtCarat.Text = string.Empty;
                    txtRejPcs.Text = string.Empty;
                    txtRejCarat.Text = string.Empty;
                    txtLossPcs.Text = string.Empty;
                    txtLossCarat.Text = string.Empty;
                    txtIssueCarat.Text = string.Empty;
                    txtIssuePcs.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
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
            backgroundWorkerInspectionRecDelete.RunWorkerAsync();

            BtnDelete.Enabled = true;
        }
        private void backgroundWorkerInspectionRecDelete_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Inspection_RecieveProperty objInspection_RecieveProperty = new Inspection_RecieveProperty();
            InspectionRecieve objInspectionRecieve = new InspectionRecieve();
            try
            {
                if (txtInspectionNo.Text != "")
                {
                    objInspection_RecieveProperty.inspection_no = Val.ToString(txtInspectionNo.Text);

                    IntRes = objInspectionRecieve.Delete(objInspection_RecieveProperty);
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
                objInspection_RecieveProperty = null;
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
        private void backgroundWorkerInspectionRecDelete_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                panelProgress.Visible = false;
                if (IntRes > 0)
                {
                    Global.Confirm("Inspection Receive Data Delete Successfully");
                    ClearDetails();
                }
                else
                {
                    Global.Confirm("Error In Inspection Receive Delete");
                    txtInspectionNo.Focus();
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