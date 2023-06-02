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

namespace DREP.Transaction
{
    public partial class FrmDemandNoting : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member
        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        Control _NextEnteredControl;
        private List<Control> _tabControls;
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        DemandNoting objDemandNoting;
        UserAuthentication objUserAuthentication;
        AssortMaster objAssort;
        SieveMaster objSieve;
        RateMaster objRate;

        DataTable DtControlSettings;
        DataTable m_dtbAssorts;
        DataTable m_dtbSieve;
        DataTable m_dtbDemandDetails;
        DataTable m_dtbCurrency;
        DataTable m_dtbDetails;
        DataTable m_dtbStatus;
        DataTable m_opDate;
        DataTable m_dtbCurrencyType;
        DataTable m_dtbCutting;
        DataTable m_dtbPurity;
        DataTable DTB_Details;
        DataTable m_dtbFillInspection;
        DataTable dtbFillInspection;

        int m_numForm_id;
        int IntRes;
        int Demand_No;

        decimal m_current_rate;
        decimal m_current_amount;
        decimal m_numTotalCarats;
        decimal m_numTotalAmount;
        decimal m_numSummRate;

        bool m_blnadd;
        bool m_blnsave;
        int m_demand_id;
        int m_seq_srno;
        int m_update_srno;

        #endregion

        #region Constructor
        public FrmDemandNoting()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            _NextEnteredControl = new Control();
            _tabControls = new List<Control>();

            objDemandNoting = new DemandNoting();
            objUserAuthentication = new UserAuthentication();
            objAssort = new AssortMaster();
            objSieve = new SieveMaster();
            objRate = new RateMaster();

            DtControlSettings = new DataTable();
            m_dtbAssorts = new DataTable();
            m_dtbSieve = new DataTable();
            m_dtbDemandDetails = new DataTable();
            m_dtbCurrency = new DataTable();
            m_dtbDetails = new DataTable();
            m_dtbStatus = new DataTable();
            m_opDate = new DataTable();
            m_dtbCurrencyType = new DataTable();
            m_dtbCutting = new DataTable();
            m_dtbPurity = new DataTable();
            DTB_Details = new DataTable();
            m_numForm_id = 0;
            IntRes = 0;
            Demand_No = 0;

            m_current_rate = 0;
            m_current_amount = 0;
            m_numTotalCarats = 0;
            m_numTotalAmount = 0;
            m_demand_id = 0;
            m_seq_srno = 0;
            m_update_srno = 0;

            m_blnadd = new bool();
            m_blnsave = new bool();

            m_dtbFillInspection = new DataTable();

            dtbFillInspection = new DataTable();
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
            objBOFormEvents.ObjToDispose.Add(objDemandNoting);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);

        }

        #endregion

        #region Events
        private void FrmDemandNoting_Load(object sender, EventArgs e)
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
                    ttlbDemand.SelectedTabPage = tblDemandDetail;
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
                //lueSieveName.EditValue = DBNull.Value;
                //lueSubSieveName.EditValue = DBNull.Value;
                txtPcs.Text = string.Empty;
                txtCarat.Text = string.Empty;
                txtOfferRate.Text = string.Empty;
                txtAmount.Text = string.Empty;
                txtQuality.Text = string.Empty;

                txtDemandRate.Text = string.Empty;
                txtDemandAmount.Text = string.Empty;

                //txtFromRate.Text = string.Empty;
                //txtToRate.Text = string.Empty;
                //lueShade.EditValue = DBNull.Value;
                //luePurity.EditValue = DBNull.Value;
                //lueCutting.Text = string.Empty;
                //txtFinalRate.Text = string.Empty;
                //txtPacketTime.Text = string.Empty;
                //txtRecTime.Text = string.Empty;
                //txtTermDays.Text = string.Empty;

                lueAssortName.Focus();
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
            backgroundWorker_DemandNoting.RunWorkerAsync();

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
                txtAmount.Text = string.Format("{0:0.00}", Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtOfferRate.Text));
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
            txtAmount.Text = string.Format("{0:0.00}", Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtOfferRate.Text));
            //m_current_amount = Val.ToDecimal(txtAmount.Text);
        }
        private void lueAssortName_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                InspectionIssue objInspectionIssue = new InspectionIssue();

                DataTable m_dtbStockCarat = new DataTable();

                m_dtbStockCarat = objInspectionIssue.GetStockCarat(GlobalDec.gEmployeeProperty.company_id, GlobalDec.gEmployeeProperty.branch_id, GlobalDec.gEmployeeProperty.location_id, GlobalDec.gEmployeeProperty.department_id, Val.ToInt(lueAssortName.EditValue), Val.ToInt(lueSieveName.EditValue));

                if (m_dtbStockCarat.Rows.Count > 0)
                {
                    lblBalCrt.Text = Val.ToString(Val.ToDecimal(m_dtbStockCarat.Rows[0]["cl_carat"]));
                }
                else
                {
                    lblBalCrt.Text = "0";
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
                //lueAssortName_EditValueChanged(null, null);
                BranchTransfer objBranchTransfer = new BranchTransfer();

                double numFromRate = 0;
                double numToRate = 0;
                double numAvgRate = 0;

                double numFromNewRange = 0;
                double numToNewRange = 0;
                double numDiscountPer = 0;
                double numDiscountAmount = 0;
                double numTermDays = 0;
                double numTermAmount = 0;
                double numFinalRate = 0;

                numFromRate = Val.ToDouble(txtFromRate.Text);
                numToRate = Val.ToDouble(txtToRate.Text);
                numTermDays = Val.ToDouble(txtTermDays.Text);

                if (numFromRate > 0 && numToRate > 0)
                {
                    numAvgRate = (numFromRate + numToRate) / 2;

                    numDiscountPer = Val.ToDouble(txtDiscountPer.Text);

                    numDiscountAmount = (numAvgRate / 100) * numDiscountPer;
                    numTermAmount = Val.ToDouble((numAvgRate / 100) * (numTermDays * 0.033));
                    numFinalRate = numAvgRate - (numDiscountAmount + numTermAmount);



                    //numFinalRate = numFinalRate - (numFinalRate / 100) * 10;

                    //txtFinalRate.Text = Val.ToString(Math.Ceiling(numFinalRate));

                    txtFinalRate.Text = Val.ToString(Math.Round((numFinalRate) / 100, 0) * 100);

                    numFromNewRange = numFinalRate - 1000;
                    numToNewRange = numFinalRate + 1000;
                }

                //if (numFromRate > 0 && numToRate > 0)
                //{
                //    numAvgRate = (numFromRate + numToRate) / 2;
                //    numAvgRate = numAvgRate + (numAvgRate / 100) * 10;
                //}

                //numFromNewRange = numAvgRate - 1000;
                //numToNewRange = numAvgRate + 1000;

                grdRecieveLots.DataSource = null;

                DTB_Details = objBranchTransfer.GetLetestNo(Val.ToInt(lueSieveName.EditValue), Val.ToInt(lueShade.EditValue), numFromNewRange, numToNewRange);

                if (DTB_Details.Rows.Count > 0)
                {
                    grdRecieveLots.DataSource = DTB_Details;

                }

                lueAssortName_EditValueChanged(null, null);

            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
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
        private void txtDemandRate_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtDemandAmount.Text = string.Format("{0:0.00}", Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtDemandRate.Text));
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
            e.Graphics.DrawLine(pen, 0, 75, 1500, 75);
        }
        private void txtFromRate_Validated(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToDecimal(txtToRate.Text) != 0)
                {
                    if (Val.ToDecimal(txtFromRate.Text) > Val.ToDecimal(txtToRate.Text))
                    {
                        Global.Message("From Rate Not Greater then To Rate");
                        txtFromRate.Text = "0";
                        txtFromRate.Focus();
                        return;
                    }
                }

                if (Val.ToDecimal(txtFromRate.Text) > 0 && Val.ToDecimal(txtToRate.Text) > 0)
                {
                    DataTable dtRateAssort = new DataTable();
                    dtRateAssort = objDemandNoting.GetRateWiseAssort(Val.ToDecimal(txtFromRate.Text), Val.ToDecimal(txtToRate.Text), Val.ToInt(lueShade.EditValue));
                    if (dtRateAssort.Rows.Count > 0)
                    {
                        lueAssortName.Properties.DataSource = dtRateAssort;
                        lueAssortName.Properties.ValueMember = "assort_id";
                        lueAssortName.Properties.DisplayMember = "assort_name";
                    }
                    else
                    {
                        DataTable Dtab_Assort = new DataTable();
                        objAssort = new AssortMaster();
                        Dtab_Assort = objAssort.GetData(1);
                        lueAssortName.Properties.DataSource = Dtab_Assort;
                        lueAssortName.Properties.ValueMember = "assort_id";
                        lueAssortName.Properties.DisplayMember = "assort_name";
                    }
                    //


                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void txtToRate_Validated(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToDecimal(txtToRate.Text) != 0)
                {
                    if (Val.ToDecimal(txtFromRate.Text) > Val.ToDecimal(txtToRate.Text))
                    {
                        Global.Message("To Rate Not less then From Rate");
                        txtToRate.Text = "0";
                        txtToRate.Focus();
                        return;
                    }
                    //if (Val.ToDecimal(txtFromRate.Text) > 0 && Val.ToDecimal(txtToRate.Text) > 0)
                    //{
                    //    DataTable dtRateAssort = new DataTable();
                    //    dtRateAssort = objDemandNoting.GetRateWiseAssort(Val.ToDecimal(txtFromRate.Text), Val.ToDecimal(txtToRate.Text), Val.ToInt(lueShade.EditValue));
                    //    if (dtRateAssort.Rows.Count > 0)
                    //    {
                    //        lueAssortName.Properties.DataSource = dtRateAssort;
                    //        lueAssortName.Properties.ValueMember = "assort_id";
                    //        lueAssortName.Properties.DisplayMember = "assort_name";
                    //    }
                    //    else
                    //    {
                    //        DataTable DTab_Assort = new DataTable();
                    //        objAssort = new AssortMaster();
                    //        DTab_Assort = objAssort.GetData(1);
                    //        lueAssortName.Properties.DataSource = DTab_Assort;
                    //        lueAssortName.Properties.ValueMember = "assort_id";
                    //        lueAssortName.Properties.DisplayMember = "assort_name";
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
            finally
            {
                //objDemandNoting = null;
                //objAssort = null;
            }
        }
        private void backgroundWorker_DemandNoting_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
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
                Demand_NotingProperty objDemandNoting_Property = new Demand_NotingProperty();
                DemandNoting objDemandNoting = new DemandNoting();
                try
                {
                    Int64 NewDemandNo = 0;
                    Int64 NewDemandID = 0;
                    Demand_No = 0;
                    //Int64 DemandNo = 0;
                    //objDemandNoting_Property.demand_id = Val.ToInt(lblMode.Tag);
                    // objDemandNoting_Property.demand_No = Val.ToInt(txtDemandNo.Text);
                    objDemandNoting_Property.demand_name = Val.ToString(txtDemandName.Text);
                    objDemandNoting_Property.demand_date = Val.DBDate(dtpDemandDate.Text);

                    objDemandNoting_Property.Party_Id = Val.ToInt(lueParty.EditValue);
                    objDemandNoting_Property.Broker_Id = Val.ToInt(lueBroker.EditValue);

                    objDemandNoting_Property.company_id = Val.ToInt(GlobalDec.gEmployeeProperty.company_id);
                    objDemandNoting_Property.branch_id = Val.ToInt(GlobalDec.gEmployeeProperty.branch_id);
                    objDemandNoting_Property.location_id = Val.ToInt(GlobalDec.gEmployeeProperty.location_id);
                    objDemandNoting_Property.department_id = Val.ToInt(GlobalDec.gEmployeeProperty.department_id);
                    objDemandNoting_Property.term_days = Val.ToInt16(txtTermDays.Text);
                    objDemandNoting_Property.currency_id = Val.ToInt16(lueCurrency.EditValue);
                    objDemandNoting_Property.form_id = m_numForm_id;
                    objDemandNoting_Property.Remark = Val.ToString(txtRemark.Text);
                    objDemandNoting_Property.Special_Remark = Val.ToString("");
                    objDemandNoting_Property.Client_Remark = Val.ToString("");
                    objDemandNoting_Property.Payment_Remark = Val.ToString("");
                    objDemandNoting_Property.Status = Val.ToString(lueStatus.Text);
                    objDemandNoting_Property.demand_deal_final = Val.ToBoolean(chkDemandDeal.Checked);

                    objDemandNoting_Property.IS_Purity = Val.ToBoolean(chkISPurity.Checked);
                    objDemandNoting_Property.IS_Color = Val.ToBoolean(chkISColor.Checked);
                    objDemandNoting_Property.IS_Price = Val.ToBoolean(chkISPrice.Checked);
                    objDemandNoting_Property.IS_Cut = Val.ToBoolean(chkISCut.Checked);
                    objDemandNoting_Property.IS_Size = Val.ToBoolean(chkISSize.Checked);
                    objDemandNoting_Property.IS_NotOnHand = Val.ToBoolean(chkISNotOnHand.Checked);
                    objDemandNoting_Property.IS_Sold = Val.ToBoolean(chkISSold.Checked);
                    objDemandNoting_Property.IS_Offer = Val.ToBoolean(chkISOffer.Checked);
                    //objDemandNoting_Property.IS_PacketPending = Val.ToBoolean(chkPacketPending.Checked);
                    objDemandNoting_Property.IS_QTY = Val.ToBoolean(chkQty.Checked);
                    objDemandNoting_Property.IS_Service = Val.ToBoolean(chkService.Checked);
                    objDemandNoting_Property.IS_Selection = Val.ToBoolean(chkSelection.Checked);

                    objDemandNoting_Property.Demand_Time = Val.ToInt(txtDemamdTime.Text);

                    objDemandNoting_Property.Packet_Time = Val.ToInt(txtPacketTime.Text);
                    objDemandNoting_Property.Packet_Date = Val.DBDate(dtpPacketDate.Text);

                    objDemandNoting_Property.Rec_Time = Val.ToInt(txtRecTime.Text);
                    objDemandNoting_Property.Rec_Date = Val.DBDate(dtpRecDate.Text);

                    objDemandNoting_Property.Party_Choice = Val.ToString(ListPartyChoice.Text);
                    objDemandNoting_Property.IS_No_Mal = Val.ToBoolean(chkNoMal.Checked);
                    //objDemandNoting_Property.IS_Pending_Demand = Val.ToBoolean(chkPendingDemand.Checked);
                    objDemandNoting_Property.Discount_Per = Val.ToDecimal(txtDiscountPer.Text);

                    objDemandNoting_Property.Order_Carat = Val.ToDecimal(txtOrderCarat.Text);

                    //int IntRes = objDemandNoting.Save(objDemandNoting_Property);

                    int IntCounter = 0;
                    int Count = 0;
                    int TotalCount = m_dtbDemandDetails.Rows.Count;
                    int IntDemandMasterID = 0;
                    //NewDemandNo = "";
                    if (Val.ToInt(lblDemandMasterId.Tag) == 0)
                    {
                        IntDemandMasterID = Val.ToInt(objDemandNoting.FindMaxMemoMasterID());
                        objDemandNoting_Property.demand_master_id = IntDemandMasterID;
                    }
                    else
                    {
                        objDemandNoting_Property.demand_master_id = Val.ToInt(lblDemandMasterId.Tag);
                    }

                    foreach (DataRow drw in m_dtbDemandDetails.Rows)
                    {
                        if (txtDemandNo.Text != "")
                        {
                            objDemandNoting_Property.demand_No = Val.ToInt(txtDemandNo.Text);
                        }
                        else
                        {
                            objDemandNoting_Property.demand_No = Val.ToInt(Demand_No);
                        }
                        objDemandNoting_Property.demand_srno = Val.ToInt64(NewDemandNo);
                        objDemandNoting_Property.demand_id = Val.ToInt(drw["demand_id"]);
                        objDemandNoting_Property.rough_shade_id = Val.ToInt(drw["rough_shade_id"]);
                        objDemandNoting_Property.assort_id = Val.ToInt(drw["assort_id"]);
                        objDemandNoting_Property.sieve_id = Val.ToInt(drw["sieve_id"]);
                        objDemandNoting_Property.sub_sieve_id = Val.ToInt(drw["sub_sieve_id"]);
                        objDemandNoting_Property.from_rate = Val.ToDecimal(drw["from_rate"]);
                        objDemandNoting_Property.to_rate = Val.ToDecimal(drw["to_rate"]);
                        objDemandNoting_Property.pcs = Val.ToInt(drw["pcs"]);
                        objDemandNoting_Property.carat = Val.ToDecimal(drw["carat"]);
                        objDemandNoting_Property.rate = Val.ToDecimal(drw["rate"]);
                        objDemandNoting_Property.amount = Val.ToDecimal(drw["amount"]);

                        objDemandNoting_Property.current_rate = Val.ToDecimal(drw["current_rate"]);
                        objDemandNoting_Property.current_amount = Val.ToDecimal(drw["current_amount"]);

                        objDemandNoting_Property.demand_rate = Val.ToDecimal(drw["demand_rate"]);
                        objDemandNoting_Property.demand_amount = Val.ToDecimal(drw["demand_amount"]);
                        objDemandNoting_Property.quality = Val.ToInt32(drw["quality"]);
                        objDemandNoting_Property.purity_id = Val.ToInt32(drw["purity_id"]);
                        objDemandNoting_Property.cutting = Val.ToString(drw["cutting"]);
                        objDemandNoting_Property.suggest_assort_id = Val.ToString(drw["suggest_assort_id"]);
                        objDemandNoting_Property.suggest_assort_name = Val.ToString(drw["suggest_assort_name"]);
                        objDemandNoting_Property.Final_Rate = Val.ToDecimal(drw["final_rate"]);
                        objDemandNoting_Property.OK_Rate = Val.ToDecimal(drw["ok_rate"]);

                        objDemandNoting_Property.IS_PacketPending = Val.ToBoolean(drw["is_packet_pending"]);
                        objDemandNoting_Property.IS_Pending_Demand = Val.ToBoolean(drw["is_pending_demand"]);

                        objDemandNoting_Property = objDemandNoting.Save(objDemandNoting_Property, DLL.GlobalDec.EnumTran.Continue, Conn);

                        NewDemandNo = Val.ToInt64(objDemandNoting_Property.demand_srno);
                        Demand_No = Val.ToInt32(objDemandNoting_Property.demand_No);
                        NewDemandID = Val.ToInt64(objDemandNoting_Property.demand_id);

                        drw["demand_id"] = NewDemandID;

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
                    dtbFillInspection = m_dtbDemandDetails.Copy();

                    dtbFillInspection.Columns.Add("demand_date", typeof(string));
                    dtbFillInspection.Columns.Add("demand_no", typeof(string));
                    dtbFillInspection.Columns.Add("Party_Id", typeof(int));
                    dtbFillInspection.Columns.Add("Broker_Id", typeof(int));

                    dtbFillInspection.Columns.Add("company_id", typeof(int));
                    dtbFillInspection.Columns.Add("branch_id", typeof(int));
                    dtbFillInspection.Columns.Add("location_id", typeof(int));
                    dtbFillInspection.Columns.Add("department_id", typeof(int));

                    if (dtbFillInspection.Columns.Contains("term_days"))
                    {

                    }
                    else
                    {
                        dtbFillInspection.Columns.Add("term_days", typeof(int));
                    }
                    dtbFillInspection.Columns.Add("due_date", typeof(string));
                    dtbFillInspection.Columns.Add("currency_id", typeof(int));
                    dtbFillInspection.Columns.Add("currency_type", typeof(string));
                    dtbFillInspection.Columns.Add("exchange_rate", typeof(decimal));
                    dtbFillInspection.Columns.Add("delivery_type_id", typeof(int));
                    dtbFillInspection.Columns.Add("sr_no", typeof(int));
                    dtbFillInspection.Columns.Add("flag_issue_check", typeof(int));

                    int Srno = 1;

                    foreach (DataRow Drw in dtbFillInspection.Rows)
                    {
                        BranchTransfer objBranch = new BranchTransfer();

                        string p_numNewStockRate = objBranch.GetLetestPrice(Val.ToInt(Drw["assort_id"]), Val.ToInt(Drw["sieve_id"]));

                        Drw["sr_no"] = Srno;
                        Drw["demand_date"] = Val.DBDate(dtpDemandDate.Text);
                        Drw["demand_no"] = Val.ToString(Demand_No);
                        Drw["party_id"] = Val.ToInt32(lueParty.EditValue);
                        Drw["broker_id"] = Val.ToInt32(lueBroker.EditValue);
                        Drw["company_id"] = Val.ToInt(GlobalDec.gEmployeeProperty.company_id);
                        Drw["branch_id"] = Val.ToInt(GlobalDec.gEmployeeProperty.branch_id);
                        Drw["location_id"] = Val.ToInt(GlobalDec.gEmployeeProperty.location_id);
                        Drw["department_id"] = Val.ToInt(GlobalDec.gEmployeeProperty.department_id);
                        Drw["term_days"] = Val.ToInt16(txtTermDays.Text);
                        Drw["currency_id"] = Val.ToInt32(lueCurrency.EditValue);
                        Drw["currency_type"] = Val.ToString(lueCurrency.Text);
                        Drw["exchange_rate"] = Val.ToDecimal(1);
                        Drw["delivery_type_id"] = Val.ToInt32(1);

                        Drw["rate"] = Val.ToDecimal(p_numNewStockRate);
                        Drw["amount"] = Math.Round(Val.ToDecimal(p_numNewStockRate) * Val.ToDecimal(Drw["carat"]), 0);
                        if (Val.ToBoolean(Drw["is_packet_pending"]).ToString() == "True")
                        {
                            Drw["flag_issue_check"] = Val.ToInt(1);
                        }
                        else
                        {
                            Drw["flag_issue_check"] = Val.ToInt(0);
                        }

                        Srno++;
                    }
                    objDemandNoting_Property = null;
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
        }
        private void backgroundWorker_DemandNoting_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                panelProgress.Visible = false;
                if (IntRes > 0)
                {
                    Global.Confirm("Demand Noting Data Save Successfully");
                }
                else
                {
                    Global.Confirm("Error In Demand Noting Data");
                    txtDemandNo.Focus();
                }

                DialogResult result = MessageBox.Show("Do you want to Inpection Issue data?", "Confirmation", MessageBoxButtons.YesNoCancel);

                if (result == DialogResult.Yes)
                {
                    FillInspectionIssueData(dtbFillInspection);
                }

                ClearDetails();
                PopulateDetails();
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }

        public void FillInspectionIssueData(DataTable m_dtbFillInspection)
        {
            FrmInspectionIssue frmInspection = new FrmInspectionIssue();

            Assembly frmAssembly = Assembly.LoadFile(Application.ExecutablePath);

            foreach (Type type in frmAssembly.GetTypes())
            {
                string type1 = type.Name.ToString().ToUpper();

                if (type.BaseType == typeof(DevExpress.XtraEditors.XtraForm))
                {
                    if (type.Name.ToString().ToUpper() == "FRMINSPECTIONISSUE")
                    {
                        XtraForm frmShow = (XtraForm)frmAssembly.CreateInstance(type.ToString());
                        frmShow.MdiParent = Global.gMainFormRef;
                        frmShow.GetType().GetMethod("ShowForm_New").Invoke(frmShow, new object[] { m_dtbFillInspection });
                        break;
                    }
                }
            }
        }
        #region GridEvents
        private void dgvDemand_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                objDemandNoting = new DemandNoting();
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        //btnSave.Enabled = false;
                        DataRow Drow = dgvDemand.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        //lblMode.Tag = Val.ToInt32(Drow["purchase_id"]);

                        dtpDemandDate.Text = Val.DBDate(Val.ToString(Drow["demand_date"]));
                        txtDemandNo.Text = Val.ToString(Drow["demand_no"]);
                        txtDemandName.Text = Val.ToString(Drow["demand_name"]);
                        lueParty.EditValue = Val.ToInt(Drow["party_id"]);
                        lueBroker.EditValue = Val.ToInt(Drow["broker_id"]);
                        txtRemark.Text = Val.ToString(Drow["remarks"]);
                        //txtSpecialRemark.Text = Val.ToString(Drow["special_remarks"]);
                        //txtClientRemark.Text = Val.ToString(Drow["client_remarks"]);
                        //txtPaymentRemark.Text = Val.ToString(Drow["payment_remarks"]);
                        txtTermDays.Text = Val.ToString(Drow["term_days"]);
                        lueCurrency.EditValue = Val.ToInt(Drow["currency_id"]);
                        lueStatus.Text = Val.ToString(Drow["status"]);

                        Int64 Demand_Srno = Val.ToInt64(Drow["demand_srno"]);
                        lblDemandMasterId.Tag = Val.ToString(Drow["demand_master_id"]);

                        chkDemandDeal.Checked = Val.ToBoolean(Drow["demand_deal_final"]);

                        chkISPurity.Checked = Val.ToBoolean(Drow["is_purity"]);
                        chkISColor.Checked = Val.ToBoolean(Drow["is_color"]);
                        chkISPrice.Checked = Val.ToBoolean(Drow["is_price"]);
                        chkISCut.Checked = Val.ToBoolean(Drow["is_cut"]);
                        chkISNotOnHand.Checked = Val.ToBoolean(Drow["is_netting"]);
                        chkISOffer.Checked = Val.ToBoolean(Drow["is_offer"]);
                        chkISSold.Checked = Val.ToBoolean(Drow["is_sold"]);
                        chkISSize.Checked = Val.ToBoolean(Drow["is_size"]);
                        //chkPacketPending.Checked = Val.ToBoolean(Drow["is_packet_pending"]);
                        chkQty.Checked = Val.ToBoolean(Drow["is_qty"]);
                        chkService.Checked = Val.ToBoolean(Drow["is_service"]);
                        chkSelection.Checked = Val.ToBoolean(Drow["is_selection"]);

                        chkNoMal.Checked = Val.ToBoolean(Drow["is_no_mal"]);
                        //chkPendingDemand.Checked = Val.ToBoolean(Drow["is_pending_demand"]);

                        txtDemamdTime.Text = Val.ToString(Drow["demand_time"]);
                        txtPacketTime.Text = Val.ToString(Drow["packet_time"]);
                        dtpPacketDate.Text = Val.DBDate(Val.ToString(Drow["packet_date"]));
                        ListPartyChoice.Text = Val.ToString(Drow["party_choice"]);

                        dtpRecDate.Text = Val.DBDate(Val.ToString(Drow["receive_date"]));
                        txtRecTime.Text = Val.ToString(Drow["receive_time"]);
                        txtDiscountPer.Text = Val.ToString(Drow["discount_per"]);

                        txtOrderCarat.Text = Val.ToString(Drow["order_carat"]);

                        m_dtbDemandDetails = objDemandNoting.GetDataDetails(Demand_Srno);

                        grdDemandDetails.DataSource = m_dtbDemandDetails;

                        ttlbDemand.SelectedTabPage = tblDemandDetail;
                        txtDemandNo.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void dgvDemandDetails_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
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
        private void dgvDemand_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
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
                Global.LOOKUPParty(lueParty);
                Global.LOOKUPBroker(lueBroker);
                Global.LOOKUPRoughShade(lueShade);

                Global.LOOKUPParty(luePartyList);
                Global.LOOKUPBroker(lueBrokerList);

                Global.LOOKUPPurity(luePurity);
                m_dtbPurity = (DataTable)luePurity.Properties.DataSource;

                m_dtbPurity.DefaultView.RowFilter = "purity_group = 'SALES' ";
                DataTable dtbdetail = m_dtbPurity.DefaultView.ToTable();

                luePurity.Properties.DataSource = dtbdetail;
                luePurity.Properties.ValueMember = "purity_id";
                luePurity.Properties.DisplayMember = "purity_name";


                m_dtbAssorts = objAssort.GetData(1);
                lueAssortName.Properties.DataSource = m_dtbAssorts;
                lueAssortName.Properties.ValueMember = "assort_id";
                lueAssortName.Properties.DisplayMember = "assort_name";

                m_dtbSieve = objSieve.GetData(1);
                lueSieveName.Properties.DataSource = m_dtbSieve;
                lueSieveName.Properties.ValueMember = "sieve_id";
                lueSieveName.Properties.DisplayMember = "sieve_name";

                m_dtbStatus.Columns.Add("Status");
                m_dtbStatus.Rows.Add("Completed");
                m_dtbStatus.Rows.Add("Pending");

                lueStatus.Properties.DataSource = m_dtbStatus;
                lueStatus.Properties.ValueMember = "Status";
                lueStatus.Properties.DisplayMember = "Status";
                lueStatus.EditValue = "Pending";

                m_dtbCutting.Columns.Add("Cutting");
                m_dtbCutting.Rows.Add("EX");
                m_dtbCutting.Rows.Add("VG");
                m_dtbCutting.Rows.Add("Good");
                m_dtbCutting.Rows.Add("Single");

                lueCutting.Properties.DataSource = m_dtbCutting;
                lueCutting.Properties.ValueMember = "Cutting";
                lueCutting.Properties.DisplayMember = "Cutting";


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

                dtpDemandDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpDemandDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpDemandDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpDemandDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpDemandDate.EditValue = DateTime.Now;

                dtpPacketDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpPacketDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpPacketDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpPacketDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpPacketDate.EditValue = DateTime.Now;

                dtpRecDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpRecDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpRecDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpRecDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpRecDate.EditValue = DateTime.Now;

                DataTable DTabPartyChoice = new DataTable();
                DTabPartyChoice.Columns.Add("party_choice", typeof(String));
                DTabPartyChoice.Rows.Add("Color");
                DTabPartyChoice.Rows.Add("Purity");
                DTabPartyChoice.Rows.Add("Price");
                DTabPartyChoice.Rows.Add("Cut");

                ListPartyChoice.Properties.DataSource = DTabPartyChoice;
                ListPartyChoice.Properties.DisplayMember = "party_choice";
                ListPartyChoice.Properties.ValueMember = "party_choice";

                lueBroker.Focus();
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
                string suggAssortId = "";
                string suggAssortName = "";
                if (DTB_Details.Rows.Count > 0)
                {
                    foreach (DataRow drow in DTB_Details.Rows)
                    {
                        if (suggAssortId != "" && suggAssortName != "")
                        {
                            suggAssortId = suggAssortId + Val.ToString(drow["assort_id"]) + ",";
                            suggAssortName = suggAssortName + Val.ToString(drow["assort_name"]) + ",";
                        }
                        else
                        {
                            suggAssortId = Val.ToString(drow["assort_id"]) + ",";
                            suggAssortName = Val.ToString(drow["assort_name"]) + ",";
                        }
                    }
                    suggAssortId = suggAssortId.Remove(suggAssortId.Length - 1, 1);
                    suggAssortName = suggAssortName.Remove(suggAssortName.Length - 1, 1);
                }
                //decimal numStockCarat = 0;

                if (btnAdd.Text == "&Add")
                {
                    DataRow[] dr = m_dtbDemandDetails.Select("sieve_id = " + Val.ToInt(lueSieveName.EditValue) + " AND assort_id = " + Val.ToInt(lueAssortName.EditValue));
                    if (dr.Count() == 1)
                    {
                        Global.Message("Record already exists in grid", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lueAssortName.Focus();
                        blnReturn = false;
                        return blnReturn;
                    }

                    DataRow drwNew = m_dtbDemandDetails.NewRow();
                    decimal numCarat = Val.ToDecimal(txtCarat.Text);
                    decimal numRate = Val.ToDecimal(txtOfferRate.Text);
                    decimal numAmount = Val.ToDecimal(txtAmount.Text);
                    int numPcs = Val.ToInt(txtPcs.Text);

                    drwNew["demand_id"] = Val.ToInt(0);

                    drwNew["from_rate"] = Val.ToDecimal(txtFromRate.Text);
                    drwNew["to_rate"] = Val.ToDecimal(txtToRate.Text);

                    drwNew["assort_id"] = Val.ToInt(lueAssortName.EditValue);
                    drwNew["assort_name"] = Val.ToString(lueAssortName.Text);

                    drwNew["sieve_id"] = Val.ToInt(lueSieveName.EditValue);
                    drwNew["sieve_name"] = Val.ToString(lueSieveName.Text);

                    drwNew["sub_sieve_id"] = Val.ToInt(lueSubSieveName.EditValue);
                    drwNew["sub_sieve_name"] = Val.ToString(lueSubSieveName.Text);

                    drwNew["rough_shade_id"] = Val.ToInt(lueShade.EditValue);
                    drwNew["rough_shade_name"] = Val.ToString(lueShade.Text);

                    drwNew["pcs"] = numPcs;
                    drwNew["carat"] = numCarat;
                    drwNew["rate"] = Val.ToDecimal(txtOfferRate.Text);
                    drwNew["final_rate"] = Val.ToDecimal(txtFinalRate.Text);
                    drwNew["ok_rate"] = Val.ToDecimal(txtOKRate.Text);
                    drwNew["amount"] = Math.Round(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtOfferRate.Text), 3);

                    drwNew["current_rate"] = m_current_rate;
                    drwNew["current_amount"] = m_current_amount;

                    drwNew["demand_rate"] = Val.ToDecimal(txtDemandRate.Text);
                    drwNew["demand_amount"] = Math.Round(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtDemandRate.Text), 3);
                    drwNew["quality"] = Val.ToInt(txtQuality.Text);

                    drwNew["purity_id"] = Val.ToInt(luePurity.EditValue);
                    drwNew["purity_name"] = Val.ToString(luePurity.Text);
                    drwNew["suggest_assort_id"] = Val.ToString(suggAssortId);
                    drwNew["suggest_assort_name"] = Val.ToString(suggAssortName);

                    drwNew["cutting"] = Val.ToString(lueCutting.Text);
                    m_seq_srno = m_seq_srno + 1;
                    drwNew["seq_sr_no"] = Val.ToInt(m_seq_srno);

                    m_dtbDemandDetails.Rows.Add(drwNew);

                    dgvDemandDetails.MoveLast();
                }
                else if (btnAdd.Text == "&Update")
                {
                    if (m_dtbDemandDetails.Select("assort_id ='" + Val.ToInt(lueAssortName.EditValue) + "' AND sieve_id ='" + Val.ToInt(lueSieveName.EditValue) + "'").Length > 0)
                    {
                        for (int i = 0; i < m_dtbDemandDetails.Rows.Count; i++)
                        {
                            if (m_dtbDemandDetails.Select("demand_id ='" + m_demand_id + "' AND seq_sr_no = '" + m_update_srno + "'").Length > 0)
                            {
                                if (m_dtbDemandDetails.Rows[m_update_srno - 1]["demand_id"].ToString() == m_demand_id.ToString())
                                {
                                    m_dtbDemandDetails.Rows[m_update_srno - 1]["carat"] = Val.ToDecimal(txtCarat.Text).ToString();
                                    m_dtbDemandDetails.Rows[m_update_srno - 1]["pcs"] = Val.ToInt(txtPcs.Text);
                                    m_dtbDemandDetails.Rows[m_update_srno - 1]["rate"] = Val.ToDecimal(txtOfferRate.Text);
                                    m_dtbDemandDetails.Rows[m_update_srno - 1]["current_rate"] = m_current_rate;
                                    m_dtbDemandDetails.Rows[m_update_srno - 1]["current_amount"] = m_current_amount;

                                    m_dtbDemandDetails.Rows[m_update_srno - 1]["demand_rate"] = Val.ToDecimal(txtDemandRate.Text);
                                    m_dtbDemandDetails.Rows[m_update_srno - 1]["demand_amount"] = Math.Round(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtDemandRate.Text), 3);
                                    m_dtbDemandDetails.Rows[m_update_srno - 1]["quality"] = Val.ToInt(txtQuality.Text);
                                    m_dtbDemandDetails.Rows[m_update_srno - 1]["final_rate"] = Val.ToDecimal(txtFinalRate.Text);
                                    m_dtbDemandDetails.Rows[m_update_srno - 1]["ok_rate"] = Val.ToDecimal(txtOKRate.Text);

                                    m_dtbDemandDetails.Rows[m_update_srno - 1]["from_rate"] = txtFromRate.Text;
                                    m_dtbDemandDetails.Rows[m_update_srno - 1]["to_rate"] = txtToRate.Text;

                                    m_dtbDemandDetails.Rows[m_update_srno - 1]["rough_shade_id"] = lueShade.EditValue;
                                    m_dtbDemandDetails.Rows[m_update_srno - 1]["rough_shade_name"] = lueShade.Text;

                                    m_dtbDemandDetails.Rows[m_update_srno - 1]["purity_name"] = luePurity.Text;
                                    m_dtbDemandDetails.Rows[m_update_srno - 1]["purity_id"] = luePurity.EditValue;

                                    m_dtbDemandDetails.Rows[m_update_srno - 1]["sieve_name"] = lueSieveName.Text;
                                    m_dtbDemandDetails.Rows[m_update_srno - 1]["sieve_id"] = lueSieveName.EditValue;

                                    m_dtbDemandDetails.Rows[m_update_srno - 1]["assort_name"] = lueAssortName.Text;
                                    m_dtbDemandDetails.Rows[m_update_srno - 1]["assort_id"] = lueAssortName.EditValue;

                                    break;
                                }
                            }
                        }
                        //m_dtbDemandDetails.Rows[0]["carat"] = Val.ToDecimal(txtCarat.Text).ToString();
                        //m_dtbDemandDetails.Rows[0]["pcs"] = Val.ToInt(txtPcs.Text);
                        //m_dtbDemandDetails.Rows[0]["rate"] = Val.ToDecimal(txtOfferRate.Text);
                        //m_dtbDemandDetails.Rows[0]["current_rate"] = m_current_rate;
                        //m_dtbDemandDetails.Rows[0]["current_amount"] = m_current_amount;

                        //m_dtbDemandDetails.Rows[0]["demand_rate"] = Val.ToDecimal(txtDemandRate.Text);
                        //m_dtbDemandDetails.Rows[0]["demand_amount"] = Math.Round(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtDemandRate.Text), 3);
                        //m_dtbDemandDetails.Rows[0]["quality"] = Val.ToInt(txtQuality.Text);
                        //m_dtbDemandDetails.Rows[0]["final_rate"] = Val.ToDecimal(txtFinalRate.Text);
                        //m_dtbDemandDetails.Rows[0]["ok_rate"] = Val.ToDecimal(txtOKRate.Text);

                        //m_dtbDemandDetails.Rows[0]["from_rate"] = txtFromRate.Text;
                        //m_dtbDemandDetails.Rows[0]["to_rate"] = txtToRate.Text;

                        //m_dtbDemandDetails.Rows[0]["rough_shade_id"] = lueShade.EditValue;
                        //m_dtbDemandDetails.Rows[0]["rough_shade_name"] = lueShade.Text;

                        //m_dtbDemandDetails.Rows[0]["purity_name"] = luePurity.Text;
                        //m_dtbDemandDetails.Rows[0]["purity_id"] = luePurity.EditValue;

                        //m_dtbDemandDetails.Rows[0]["sieve_name"] = lueSieveName.Text;
                        //m_dtbDemandDetails.Rows[0]["sieve_id"] = lueSieveName.EditValue;

                        //m_dtbDemandDetails.Rows[0]["assort_name"] = lueAssortName.Text;
                        //m_dtbDemandDetails.Rows[0]["assort_id"] = lueAssortName.EditValue;

                        btnAdd.Text = "&Add";
                    }
                    else
                    {
                        for (int i = 0; i < m_dtbDemandDetails.Rows.Count; i++)
                        {
                            if (m_dtbDemandDetails.Select("demand_id ='" + m_demand_id + "' AND seq_sr_no = '" + m_update_srno + "'").Length > 0)
                            {
                                if (m_dtbDemandDetails.Rows[m_update_srno - 1]["demand_id"].ToString() == m_demand_id.ToString())
                                {
                                    m_dtbDemandDetails.Rows[m_update_srno - 1]["carat"] = Val.ToDecimal(txtCarat.Text).ToString();
                                    m_dtbDemandDetails.Rows[m_update_srno - 1]["pcs"] = Val.ToInt(txtPcs.Text);
                                    m_dtbDemandDetails.Rows[m_update_srno - 1]["rate"] = Val.ToDecimal(txtOfferRate.Text);
                                    m_dtbDemandDetails.Rows[m_update_srno - 1]["current_rate"] = m_current_rate;
                                    m_dtbDemandDetails.Rows[m_update_srno - 1]["current_amount"] = m_current_amount;

                                    m_dtbDemandDetails.Rows[m_update_srno - 1]["demand_rate"] = Val.ToDecimal(txtDemandRate.Text);
                                    m_dtbDemandDetails.Rows[m_update_srno - 1]["demand_amount"] = Math.Round(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtDemandRate.Text), 3);
                                    m_dtbDemandDetails.Rows[m_update_srno - 1]["quality"] = Val.ToInt(txtQuality.Text);
                                    m_dtbDemandDetails.Rows[m_update_srno - 1]["final_rate"] = Val.ToDecimal(txtFinalRate.Text);
                                    m_dtbDemandDetails.Rows[m_update_srno - 1]["ok_rate"] = Val.ToDecimal(txtOKRate.Text);

                                    m_dtbDemandDetails.Rows[m_update_srno - 1]["from_rate"] = txtFromRate.Text;
                                    m_dtbDemandDetails.Rows[m_update_srno - 1]["to_rate"] = txtToRate.Text;

                                    m_dtbDemandDetails.Rows[m_update_srno - 1]["rough_shade_id"] = lueShade.EditValue;
                                    m_dtbDemandDetails.Rows[m_update_srno - 1]["rough_shade_name"] = lueShade.Text;

                                    m_dtbDemandDetails.Rows[m_update_srno - 1]["purity_name"] = luePurity.Text;
                                    m_dtbDemandDetails.Rows[m_update_srno - 1]["purity_id"] = luePurity.EditValue;

                                    m_dtbDemandDetails.Rows[m_update_srno - 1]["sieve_name"] = lueSieveName.Text;
                                    m_dtbDemandDetails.Rows[m_update_srno - 1]["sieve_id"] = lueSieveName.EditValue;

                                    m_dtbDemandDetails.Rows[m_update_srno - 1]["assort_name"] = lueAssortName.Text;
                                    m_dtbDemandDetails.Rows[m_update_srno - 1]["assort_id"] = lueAssortName.EditValue;
                                }
                            }
                        }
                        btnAdd.Text = "&Add";
                    }
                    dgvDemandDetails.MoveLast();
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
                    if (m_dtbDemandDetails.Rows.Count == 0)
                    {
                        lstError.Add(new ListError(22, "Record"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                        }
                    }
                    //if (chkDemandDeal.Checked == false && txtRemark.Text == string.Empty)
                    //{
                    //    lstError.Add(new ListError(12, "Entry Remark"));
                    //    if (!blnFocus)
                    //    {
                    //        blnFocus = true;
                    //        txtRemark.Focus();
                    //    }
                    //}
                    var result = DateTime.Compare(Convert.ToDateTime(dtpDemandDate.Text), DateTime.Today);
                    if (result > 0)
                    {
                        lstError.Add(new ListError(5, "Demand Date Not Be Greater Than Today Date"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            dtpDemandDate.Focus();
                        }
                    }
                    if (lueCurrency.Text == string.Empty)
                    {
                        lstError.Add(new ListError(3, "Currency Type"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueCurrency.Focus();
                        }
                    }
                    if (lueParty.Text == "" && lueBroker.Text == "")
                    {
                        lstError.Add(new ListError(13, "Party / Broker"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueShade.Focus();
                        }
                    }

                }

                if (m_blnadd)
                {
                    if (Val.ToDecimal(txtFromRate.Text) == 0)
                    {
                        lstError.Add(new ListError(12, "From Rate"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtFromRate.Focus();
                        }
                    }
                    if (Val.ToDecimal(txtToRate.Text) == 0)
                    {
                        lstError.Add(new ListError(12, "To Rate"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtToRate.Focus();
                        }
                    }

                    //if (lueCutting.ItemIndex < 0)
                    //{
                    //    lstError.Add(new ListError(13, "Cutting"));
                    //    if (!blnFocus)
                    //    {
                    //        blnFocus = true;
                    //        lueCutting.Focus();
                    //    }
                    //}


                    //if (luePurity.ItemIndex < 0)
                    //{
                    //    lstError.Add(new ListError(13, "Purity"));
                    //    if (!blnFocus)
                    //    {
                    //        blnFocus = true;
                    //        luePurity.Focus();
                    //    }
                    //}



                    if (lueShade.Text == "" && lueAssortName.Text == "")
                    {
                        lstError.Add(new ListError(13, "Color / Assort"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueShade.Focus();
                        }
                    }


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
                    //if (lueSubSieveName.ItemIndex < 0)
                    //{
                    //    lstError.Add(new ListError(13, "Sub Sieve"));
                    //    if (!blnFocus)
                    //    {
                    //        blnFocus = true;
                    //        lueSubSieveName.Focus();
                    //    }
                    //}

                    //if (Val.ToDouble(txtCarat.Text) == 0)
                    //{
                    //    lstError.Add(new ListError(12, "Carat"));
                    //    if (!blnFocus)
                    //    {
                    //        blnFocus = true;
                    //        txtCarat.Focus();
                    //    }
                    //}

                    //if (Val.ToDouble(txtRate.Text) == 0)
                    //{
                    //    lstError.Add(new ListError(12, "Rate"));
                    //    if (!blnFocus)
                    //    {
                    //        blnFocus = true;
                    //        txtRate.Focus();
                    //    }
                    //}

                    //if (Val.ToDouble(txtAmount.Text) == 0)
                    //{
                    //    lstError.Add(new ListError(12, "Amount"));
                    //    if (!blnFocus)
                    //    {
                    //        blnFocus = true;
                    //        txtAmount.Focus();
                    //    }
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
                if (!GenerateDemandDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }

                lblMode.Tag = 0;
                lblMode.Text = "Add Mode";
                lueParty.EditValue = System.DBNull.Value;
                lueBroker.EditValue = System.DBNull.Value;
                lueCurrency.EditValue = Val.ToString(GlobalDec.gEmployeeProperty.currency_id);
                txtDemandNo.Text = string.Empty;
                txtDemandName.Text = string.Empty;
                txtTermDays.Text = string.Empty;

                //m_opDate = Global.GetDate();
                dtpFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
                //dtpFromDate.EditValue = Val.DBDate(m_opDate.Rows[0]["opening_date"].ToString());
                dtpFromDate.EditValue = DateTime.Now;

                dtpDemandDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpDemandDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpDemandDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpDemandDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpDemandDate.EditValue = DateTime.Now;

                dtpPacketDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpPacketDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpPacketDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpPacketDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpPacketDate.EditValue = DateTime.Now;

                dtpRecDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpRecDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpRecDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpRecDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpRecDate.EditValue = DateTime.Now;

                lueShade.EditValue = System.DBNull.Value;
                lueAssortName.EditValue = System.DBNull.Value;
                lueSieveName.EditValue = System.DBNull.Value;
                lueSubSieveName.EditValue = System.DBNull.Value;
                txtFromRate.Text = string.Empty;
                txtToRate.Text = string.Empty;
                txtPcs.Text = string.Empty;
                txtCarat.Text = string.Empty;
                txtOfferRate.Text = string.Empty;
                txtAmount.Text = string.Empty;
                txtRemark.Text = string.Empty;
                lblDemandMasterId.Tag = string.Empty;
                lueStatus.EditValue = "Pending";
                chkDemandDeal.Checked = false;
                chkISPurity.Checked = false;
                chkISColor.Checked = false;
                chkISPrice.Checked = false;
                chkISSize.Checked = false;
                chkISCut.Checked = false;
                chkISNotOnHand.Checked = false;
                chkISSold.Checked = false;
                chkISOffer.Checked = false;
                chkPendingDemand.Checked = false;
                chkPacketPending.Checked = false;
                chkService.Checked = false;
                chkQty.Checked = false;
                chkNoMal.Checked = false;
                txtOrderCarat.Text = string.Empty;
                chkSelection.Checked = false;

                txtPacketTime.Text = string.Empty;
                txtDemamdTime.Text = string.Empty;
                txtFinalRate.Text = string.Empty;
                txtOKRate.Text = string.Empty;
                txtDiscountPer.Text = string.Empty;

                ListPartyChoice.Properties.Items.Clear();

                //for (int i = 0; i < ListPartyChoice.Properties.Items.Count; i++)
                //    ListPartyChoice.Properties.Items[i].CheckState = CheckState.Unchecked;

                DataTable DTabPartyChoice = new DataTable();
                DTabPartyChoice.Columns.Add("party_choice", typeof(String));
                DTabPartyChoice.Rows.Add("Color");
                DTabPartyChoice.Rows.Add("Purity");
                DTabPartyChoice.Rows.Add("Price");
                DTabPartyChoice.Rows.Add("Cut");

                ListPartyChoice.Properties.DataSource = DTabPartyChoice;
                ListPartyChoice.Properties.DisplayMember = "party_choice";
                ListPartyChoice.Properties.ValueMember = "party_choice";

                btnAdd.Text = "&Add";
                txtDemandNo.Focus();
                btnSave.Enabled = true;
                txtRecTime.Text = string.Empty;

            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
        }
        private bool GenerateDemandDetails()
        {
            bool blnReturn = true;
            try
            {
                if (m_dtbDemandDetails.Rows.Count > 0)
                    m_dtbDemandDetails.Rows.Clear();

                m_dtbDemandDetails = new DataTable();

                m_dtbDemandDetails.Columns.Add("demand_id", typeof(int));
                m_dtbDemandDetails.Columns.Add("assort_id", typeof(int));
                m_dtbDemandDetails.Columns.Add("assort_name", typeof(string));
                m_dtbDemandDetails.Columns.Add("sieve_id", typeof(int));
                m_dtbDemandDetails.Columns.Add("sieve_name", typeof(string));
                m_dtbDemandDetails.Columns.Add("sub_sieve_id", typeof(int));
                m_dtbDemandDetails.Columns.Add("sub_sieve_name", typeof(string));
                m_dtbDemandDetails.Columns.Add("pcs", typeof(int)).DefaultValue = 0;
                m_dtbDemandDetails.Columns.Add("carat", typeof(decimal)).DefaultValue = 0;
                m_dtbDemandDetails.Columns.Add("rate", typeof(decimal)).DefaultValue = 0;
                m_dtbDemandDetails.Columns.Add("amount", typeof(decimal)).DefaultValue = 0;
                m_dtbDemandDetails.Columns.Add("current_rate", typeof(decimal)).DefaultValue = 0;
                m_dtbDemandDetails.Columns.Add("current_amount", typeof(decimal)).DefaultValue = 0;
                m_dtbDemandDetails.Columns.Add("demand_rate", typeof(decimal)).DefaultValue = 0;
                m_dtbDemandDetails.Columns.Add("demand_amount", typeof(decimal)).DefaultValue = 0;
                m_dtbDemandDetails.Columns.Add("quality", typeof(int)).DefaultValue = 0;

                m_dtbDemandDetails.Columns.Add("from_rate", typeof(decimal)).DefaultValue = 0;
                m_dtbDemandDetails.Columns.Add("to_rate", typeof(decimal)).DefaultValue = 0;
                m_dtbDemandDetails.Columns.Add("rough_shade_id", typeof(int));
                m_dtbDemandDetails.Columns.Add("rough_shade_name", typeof(string));

                m_dtbDemandDetails.Columns.Add("purity_id", typeof(int));
                m_dtbDemandDetails.Columns.Add("purity_name", typeof(string));
                m_dtbDemandDetails.Columns.Add("suggest_assort_id", typeof(string));
                m_dtbDemandDetails.Columns.Add("suggest_assort_name", typeof(string));
                // m_dtbDemandDetails.Columns.Add("term_days", typeof(int));

                m_dtbDemandDetails.Columns.Add("cutting", typeof(string));

                m_dtbDemandDetails.Columns.Add("final_rate", typeof(decimal)).DefaultValue = 0;
                m_dtbDemandDetails.Columns.Add("ok_rate", typeof(decimal)).DefaultValue = 0;
                m_dtbDemandDetails.Columns.Add("seq_sr_no", typeof(int));

                m_dtbDemandDetails.Columns.Add("is_pending_demand", typeof(Boolean));
                m_dtbDemandDetails.Columns.Add("is_packet_pending", typeof(Boolean));

                m_seq_srno = 0;

                grdDemandDetails.DataSource = m_dtbDemandDetails;
                grdDemandDetails.Refresh();
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
            objDemandNoting = new DemandNoting();
            bool blnReturn = true;
            DateTime datFromDate = DateTime.MinValue;
            DateTime datToDate = DateTime.MinValue;
            try
            {
                m_dtbDetails = objDemandNoting.GetData(Val.DBDate(dtpFromDate.Text), Val.DBDate(dtpToDate.Text), Val.ToInt32(luePartyList.EditValue), Val.ToInt32(lueBrokerList.EditValue));

                if (m_dtbDetails.Rows.Count == 0)
                {
                    Global.Message("Data Not Found");
                    blnReturn = false;
                }

                grdDemand.DataSource = m_dtbDetails;
                dgvDemand.BestFitColumns();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            finally
            {
                objDemandNoting = null;
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


        #endregion

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ObjPer.SetFormPer();
            if (ObjPer.AllowUpdate == false || ObjPer.AllowInsert == false)
            {
                Global.Message(BLL.GlobalDec.gStrPermissionInsUpdMsg);
                return;
            }

            DialogResult result = MessageBox.Show("Do you want to delete data?", "Confirmation", MessageBoxButtons.YesNoCancel);
            if (result != DialogResult.Yes)
            {
                btnSave.Enabled = true;
                return;
            }

            Cursor.Current = Cursors.Default;
            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                Conn = new BeginTranConnection(true, false);
            }
            else
            {
                Conn = new BeginTranConnection(false, true);
            }
            Demand_NotingProperty objDemandNoting_Property = new Demand_NotingProperty();
            DemandNoting objDemandNoting = new DemandNoting();

            try
            {
                objDemandNoting_Property.demand_No = Val.ToInt(txtDemandNo.Text);
                IntRes = objDemandNoting.Delete(objDemandNoting_Property, DLL.GlobalDec.EnumTran.Continue, Conn);

                if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                {
                    Conn.Inter1.Commit();
                }
                else
                {
                    Conn.Inter2.Commit();
                }

                if (IntRes > 0)
                {
                    Global.Confirm("Demand Noting Data Delete Successfully");
                    ClearDetails();
                    PopulateDetails();
                }
                else
                {
                    Global.Confirm("Error In Demand Noting Data");
                    txtDemandNo.Focus();
                }

            }
            catch (Exception ex)
            {
                IntRes = -1;
                Conn.Inter1.Rollback();
                Conn.Inter2.Rollback();
                Conn = null;
                General.ShowErrors(ex.ToString());
                return;
            }
            finally
            {
                objDemandNoting_Property = null;
            }
        }

        private void lueShade_EditValueChanged(object sender, EventArgs e)
        {
            txtToRate_Validated(null, null);
            lueSieveName_EditValueChanged(null, null);
        }

        private void BtnClearList_Click(object sender, EventArgs e)
        {
            lueBrokerList.EditValue = null;
            luePartyList.EditValue = null;
        }

        private void repIsColor_QueryCheckStateByValue(object sender, DevExpress.XtraEditors.Controls.QueryCheckStateByValueEventArgs e)
        {
            string val = e.CheckState.ToString();
        }

        private void dgvDemandDetails_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvDemandDetails.GetDataRow(e.RowHandle);

                        txtFromRate.Text = Val.ToString(Drow["from_rate"]);
                        txtToRate.Text = Val.ToString(Drow["to_rate"]);
                        lueShade.EditValue = Val.ToInt32(Drow["rough_shade_id"]);
                        luePurity.EditValue = Val.ToInt32(Drow["purity_id"]);
                        lueSieveName.EditValue = Val.ToInt32(Drow["sieve_id"]);
                        lueAssortName.EditValue = Val.ToInt32(Drow["assort_id"]);

                        txtOfferRate.Text = Val.ToString(Drow["rate"]);
                        txtCarat.Text = Val.ToString(Drow["carat"]);
                        txtFinalRate.Text = Val.ToString(Drow["final_rate"]);
                        txtOKRate.Text = Val.ToString(Drow["ok_rate"]);
                        m_demand_id = Val.ToInt(Drow["demand_id"]);
                        m_update_srno = Val.ToInt(Drow["seq_sr_no"]);
                        btnAdd.Text = "&Update";
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void txtTermDays_EditValueChanged(object sender, EventArgs e)
        {
            lueSieveName_EditValueChanged(null, null);
        }
        private void txtDiscountPer_EditValueChanged(object sender, EventArgs e)
        {
            lueSieveName_EditValueChanged(null, null);
        }

        private void txtFromRate_EditValueChanged(object sender, EventArgs e)
        {
            lueSieveName_EditValueChanged(null, null);
        }

        private void txtToRate_EditValueChanged(object sender, EventArgs e)
        {
            lueSieveName_EditValueChanged(null, null);
        }
    }
}