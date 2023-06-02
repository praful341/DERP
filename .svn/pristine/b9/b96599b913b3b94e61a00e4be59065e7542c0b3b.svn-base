using BLL;
using BLL.FunctionClasses.Rejection;
using BLL.FunctionClasses.Transaction;
using BLL.PropertyClasses.Rejection;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Rejection
{
    public partial class FrmMFGRejectionInternalTransfer : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        FormEvents objBOFormEvents;
        Validation Val;
        BLL.FormPer ObjPer;
        Int64 IntRes;

        Control _NextEnteredControl;
        private List<Control> _tabControls;
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        MFGRejectionInternalTransfer objRejectionInternalTransfer = new MFGRejectionInternalTransfer();
        DataTable m_dtbType;
        DataTable RejPurity;
        MfgRejectionPurityMaster objRejPurity = new MfgRejectionPurityMaster();
        DataTable DtControlSettings;
        decimal m_numTotalCarats;
        decimal m_numTotalAmount;

        #endregion

        #region Constructor
        public FrmMFGRejectionInternalTransfer()
        {
            InitializeComponent();

            objBOFormEvents = new FormEvents();
            Val = new Validation();
            ObjPer = new BLL.FormPer();

            objRejectionInternalTransfer = new MFGRejectionInternalTransfer();
            RejPurity = new DataTable();
            DtControlSettings = new DataTable();
            _NextEnteredControl = new Control();
            _tabControls = new List<Control>();
            m_numTotalCarats = 0;
            m_numTotalAmount = 0;
        }

        public void ShowForm()
        {
            ObjPer.FormName = this.Name.ToUpper();
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
            objBOFormEvents.ObjToDispose.Add(objRejectionInternalTransfer);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }

        #endregion

        #region Events
        private void FrmMFGRejectionInternalTransfer_Load(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    m_dtbType = new DataTable();
                    m_dtbType.Columns.Add("type");
                    m_dtbType.Rows.Add("BOTH");
                    m_dtbType.Rows.Add("NORMAL");
                    m_dtbType.Rows.Add("REJECTION");

                    lueType.Properties.DataSource = m_dtbType;
                    lueType.Properties.ValueMember = "type";
                    lueType.Properties.DisplayMember = "type";

                    RejPurity = objRejPurity.GetData(1);
                    lueFromPurity.Properties.DataSource = RejPurity;
                    lueFromPurity.Properties.ValueMember = "purity_id";
                    lueFromPurity.Properties.DisplayMember = "purity_name";

                    lueToPurity.Properties.DataSource = RejPurity;
                    lueToPurity.Properties.ValueMember = "purity_id";
                    lueToPurity.Properties.DisplayMember = "purity_name";

                    dtpSearchFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                    dtpSearchFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                    dtpSearchFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                    dtpSearchFromDate.Properties.CharacterCasing = CharacterCasing.Upper;

                    dtpSearchToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                    dtpSearchToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                    dtpSearchToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                    dtpSearchToDate.Properties.CharacterCasing = CharacterCasing.Upper;

                    dtpSearchFromDate.EditValue = DateTime.Now;
                    dtpSearchToDate.EditValue = DateTime.Now;

                    DtpEntryDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                    DtpEntryDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                    DtpEntryDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                    DtpEntryDate.Properties.CharacterCasing = CharacterCasing.Upper;

                    BtnSearch_Click(null, null);

                    DtpEntryDate.EditValue = DateTime.Now;
                }
                catch (Exception ex)
                {
                    General.ShowErrors(ex.ToString());
                    return;
                }
                btnClear_Click(btnClear, null);
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void backgroundWorker_MFGRejectionInternalTransfer_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.Default;

                MFGRejectionInternalTransferProperty RejectionInternalTransferProperty = new MFGRejectionInternalTransferProperty();
                MFGRejectionInternalTransfer objRejectionInternalTransfer = new MFGRejectionInternalTransfer();
                IntRes = 0;
                try
                {
                    RejectionInternalTransferProperty.transfer_id = Val.ToInt64(lblID.Text);
                    RejectionInternalTransferProperty.transfer_date = Val.DBDate(DtpEntryDate.Text);
                    RejectionInternalTransferProperty.from_purity_id = Val.ToInt64(lueFromPurity.EditValue);
                    RejectionInternalTransferProperty.to_purity_id = Val.ToInt64(lueToPurity.EditValue);
                    RejectionInternalTransferProperty.type = Val.ToString(lueType.Text);

                    if (Val.ToInt64(lblID.Text) == 0)
                    {
                        RejectionInternalTransferProperty.pcs = Val.ToDecimal(txtPcs.Text);
                        RejectionInternalTransferProperty.carat = Val.ToDecimal(txtCarat.Text);
                        RejectionInternalTransferProperty.rate = Val.ToDecimal(txtRate.Text);
                        RejectionInternalTransferProperty.amount = Val.ToDecimal(txtAmount.Text);
                    }
                    else
                    {
                        RejectionInternalTransferProperty.pcs = Val.ToDecimal(txtPcs.Text);
                        RejectionInternalTransferProperty.carat = Val.ToDecimal(txtCarat.Text);
                        RejectionInternalTransferProperty.rate = Val.ToDecimal(txtRate.Text);
                        RejectionInternalTransferProperty.amount = Val.ToDecimal(txtAmount.Text);
                    }

                    //RejectionInternalTransferProperty.carat = Val.ToDecimal(txtCarat.Text);
                    //RejectionInternalTransferProperty.rate = Val.ToDecimal(txtRate.Text);
                    //RejectionInternalTransferProperty.amount = Val.ToDecimal(txtAmount.Text);

                    RejectionInternalTransferProperty.remarks = Val.ToString(txtRemark.Text);
                    RejectionInternalTransferProperty.total_carat = Val.ToDecimal(txtPendingWt.Text);
                    RejectionInternalTransferProperty.diff_carat = Val.ToDecimal(lblDiffCarat.Text);

                    IntRes = objRejectionInternalTransfer.Save(RejectionInternalTransferProperty);
                }
                catch (Exception ex)
                {
                    IntRes = -1;
                    General.ShowErrors(ex.ToString());
                    return;
                }
                finally
                {
                    RejectionInternalTransferProperty = null;
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
        }
        private void backgroundWorker_MFGRejectionInternalTransfer_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (IntRes > 0)
                {
                    Global.Confirm("Rejection Internal Transfer Data Save Successfully");
                    btnClear_Click(null, null);
                }
                else
                {
                    Global.Confirm("Error In Rejection Internal Transfer");
                    DtpEntryDate.Focus();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            MFGRejectionInternalTransferProperty RejectionInternalTransferProperty = new MFGRejectionInternalTransferProperty();
            MFGRejectionInternalTransfer objRejectionInternalTransfer = new MFGRejectionInternalTransfer();

            RejectionInternalTransferProperty.from_date = Val.DBDate(dtpSearchFromDate.Text);
            RejectionInternalTransferProperty.to_date = Val.DBDate(dtpSearchToDate.Text);

            DataTable DTab_Data = objRejectionInternalTransfer.RejInternal_Trf_GetData(RejectionInternalTransferProperty);

            grdRejectionInternalTrf.DataSource = DTab_Data;
            dgvRejectionInternalTrf.BestFitColumns();
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

            DialogResult result = MessageBox.Show("Do you want to save data?", "Confirmation", MessageBoxButtons.YesNoCancel);
            if (result != DialogResult.Yes)
            {
                btnSave.Enabled = true;
                return;
            }

            DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
            backgroundWorker_MFGRejectionInternalTransfer.RunWorkerAsync();

            btnSave.Enabled = true;
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                //dtpSearchFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                //dtpSearchFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                //dtpSearchFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                //dtpSearchFromDate.Properties.CharacterCasing = CharacterCasing.Upper;

                //dtpSearchToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                //dtpSearchToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                //dtpSearchToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                //dtpSearchToDate.Properties.CharacterCasing = CharacterCasing.Upper;

                //dtpSearchFromDate.EditValue = DateTime.Now;
                //dtpSearchToDate.EditValue = DateTime.Now;

                DtpEntryDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                DtpEntryDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                DtpEntryDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                DtpEntryDate.Properties.CharacterCasing = CharacterCasing.Upper;

                DtpEntryDate.EditValue = DateTime.Now;

                txtPendingWt.Text = "0";
                txtPcs.Text = "0";
                txtCarat.Text = "0";
                txtRate.Text = "0";
                txtAmount.Text = "0";
                txtFromPurityGroup.Text = "";
                txtToPurityGroup.Text = "";
                lueFromPurity.EditValue = null;
                lueToPurity.EditValue = null;
                lueType.EditValue = null;
                lblID.Text = "0";
                txtRemark.Text = "";

                BtnSearch_Click(null, null);
                lblID.Text = "0";
                lblCarat.Text = "0";
                lblDiffCarat.Text = "0";

                DtpEntryDate.Focus();
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (GlobalDec.gEmployeeProperty.user_name != "JAYESH")
            {
                Global.Message("Don't have Permission...So Please Contact to Administrator");
                return;
            }
            if (Val.ToInt(lblID.Text) != 0)
            {
                DialogResult result = MessageBox.Show("Do you want to Delete Rejection Internal Transfer data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result != DialogResult.Yes)
                {
                    return;
                }
                IntRes = 0;
                MFGRejectionInternalTransferProperty RejectionInternalTransferProperty = new MFGRejectionInternalTransferProperty();
                MFGRejectionInternalTransfer objRejectionInternalTransfer = new MFGRejectionInternalTransfer();

                RejectionInternalTransferProperty.transfer_id = Val.ToInt(lblID.Text);
                RejectionInternalTransferProperty.from_purity_id = Val.ToInt64(lueFromPurity.EditValue);
                RejectionInternalTransferProperty.to_purity_id = Val.ToInt64(lueToPurity.EditValue);
                RejectionInternalTransferProperty.pcs = Val.ToDecimal(txtPcs.Text);
                RejectionInternalTransferProperty.carat = Val.ToDecimal(txtCarat.Text);

                IntRes = objRejectionInternalTransfer.GetDeleteRejectionInternal_Transfer_ID(RejectionInternalTransferProperty);

                if (IntRes > 0)
                {
                    Global.Confirm("Rejection Internal Transfer Data Deleted Succesfully");
                    btnClear_Click(null, null);
                }
                else
                {
                    Global.Confirm("Error In Rejection Internal Transfer Data");
                }
            }
            else
            {
                Global.Confirm("Not Selected Any Data are Deleted..");
                return;
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void lueFromPurity_EditValueChanged(object sender, EventArgs e)
        {
            if (lueFromPurity.Text != "")
            {
                DataTable DTabRejPurity = RejPurity.Select("purity_id =" + Val.ToInt64(lueFromPurity.EditValue)).CopyToDataTable();

                if (DTabRejPurity.Rows.Count > 0)
                {
                    DataTable DTab_StockData = objRejectionInternalTransfer.Rej_Purity_Stock_Data(Val.ToInt64(lueFromPurity.EditValue));

                    if (DTab_StockData.Rows.Count > 0)
                    {
                        txtPendingWt.Text = Val.ToDecimal(DTab_StockData.Rows[0]["carat"]).ToString();
                        txtFromPurityGroup.Text = Val.ToString(DTabRejPurity.Rows[0]["group_name"]);
                    }
                    else
                    {
                        txtPendingWt.Text = Val.ToDecimal(0).ToString();
                        txtFromPurityGroup.Text = Val.ToString("");
                    }
                }
            }
        }
        private void lueToPurity_EditValueChanged(object sender, EventArgs e)
        {
            if (lueToPurity.Text != "")
            {
                DataTable DTabRejPurity = RejPurity.Select("purity_id =" + Val.ToInt64(lueToPurity.EditValue)).CopyToDataTable();

                if (DTabRejPurity.Rows.Count > 0)
                {
                    txtToPurityGroup.Text = Val.ToString(DTabRejPurity.Rows[0]["group_name"]);
                }
            }
        }
        private void txtRate_EditValueChanged(object sender, EventArgs e)
        {
            txtAmount.Text = string.Format("{0:0.00}", Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text));
        }
        private void txtCarat_EditValueChanged(object sender, EventArgs e)
        {
            txtAmount.Text = string.Format("{0:0.00}", Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text));
            if (lblID.Text == "0")
            {
                lblCarat.Text = txtCarat.Text;
                lblDiffCarat.Text = Val.ToString(Val.ToDecimal(txtCarat.Text) - Val.ToDecimal(lblCarat.Text));
            }
            else
            {
                lblDiffCarat.Text = Val.ToString(Val.ToDecimal(txtCarat.Text) - Val.ToDecimal(lblCarat.Text));
            }
        }
        private void lueFromPurity_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmMfgRejectionPurityMaster objRejectionPurity = new FrmMfgRejectionPurityMaster();
                objRejectionPurity.ShowDialog();
                Global.LOOKUPRejPurity(lueFromPurity);
            }
        }
        private void lueToPurity_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmMfgRejectionPurityMaster objRejectionPurity = new FrmMfgRejectionPurityMaster();
                objRejectionPurity.ShowDialog();
                Global.LOOKUPRejPurity(lueToPurity);
            }
        }
        private void txtCarat_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txtRate_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txtPendingWt_KeyPress(object sender, KeyPressEventArgs e)
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

        #region GridEvents
        private void dgvRejectionInternalTrf_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        MfgRejectionRateEntry objRejectionRateEntry = new MfgRejectionRateEntry();

                        DataRow Drow = dgvRejectionInternalTrf.GetDataRow(e.RowHandle);
                        lblID.Text = Val.ToString(Drow["transfer_id"]);
                        DtpEntryDate.Text = Val.DBDate(Drow["transfer_date"].ToString());
                        lueType.EditValue = Val.ToString(Drow["type"]);
                        lueFromPurity.EditValue = Val.ToInt64(Drow["from_purity_id"]);
                        lueToPurity.EditValue = Val.ToInt64(Drow["to_purity_id"]);
                        txtFromPurityGroup.Text = Val.ToString(Drow["from_purity_group"]);
                        txtToPurityGroup.Text = Val.ToString(Drow["to_purity_group"]);
                        txtPcs.Text = Val.ToString(Drow["pcs"]);
                        txtCarat.Text = Val.ToString(Drow["carat"]);
                        txtRate.Text = Val.ToString(Drow["rate"]);
                        txtAmount.Text = Val.ToString(Drow["amount"]);
                        txtRemark.Text = Val.ToString(Drow["remarks"]);
                        lblCarat.Text = Val.ToString(Drow["carat"]);
                        DtpEntryDate.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void dgvRejectionInternalTrf_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                m_numTotalCarats = Math.Round(Val.ToDecimal(ClmCarat.SummaryItem.SummaryValue), 2, MidpointRounding.AwayFromZero);

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

        #endregion

        #endregion

        #region Functions      

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
                            dgvRejectionInternalTrf.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvRejectionInternalTrf.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvRejectionInternalTrf.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvRejectionInternalTrf.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvRejectionInternalTrf.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvRejectionInternalTrf.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvRejectionInternalTrf.ExportToCsv(Filepath);
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
            Export("xlsx", "Export to Excel", "Excel files 97-2003 (Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*");
        }
        private void MNExportPDF_Click(object sender, EventArgs e)
        {
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
