using BLL;
using BLL.FunctionClasses.Account;
using BLL.FunctionClasses.Transaction;
using BLL.PropertyClasses.Account;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DREP.Account
{
    public partial class FrmAvakJavakCashTransfer : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.FormPer ObjPer = new BLL.FormPer();
        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents = new BLL.FormEvents();
        BLL.Validation Val = new BLL.Validation();
        AvakJavakCashTransfer objAvakJavakCashTransfer = new AvakJavakCashTransfer();
        int m_numForm_id = 0;
        int IntRes = 0;
        Control _NextEnteredControl = new Control();
        DataTable DtControlSettings = new DataTable();
        private List<Control> _tabControls = new List<Control>();
        FillCombo ObjFillCombo = new FillCombo();
        #endregion

        #region Constructor
        public FrmAvakJavakCashTransfer()
        {
            InitializeComponent();
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

            this.Show();
        }
        private void AttachFormEvents()
        {
            objBOFormEvents.CurForm = this;
            objBOFormEvents.FormKeyPress = true;
            objBOFormEvents.FormKeyDown = true;
            objBOFormEvents.FormResize = true;
            objBOFormEvents.FormClosing = true;
            objBOFormEvents.ObjToDispose.Add(objAvakJavakCashTransfer);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }

        #endregion

        #region Dynamic Tab Control

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
                if ((Control)sender is CheckedComboBoxEdit)
                {
                    if (e.KeyChar == Convert.ToChar(Keys.Enter))
                    {
                        SendKeys.Send("{TAB}");
                    }
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
                else if ((Control)sender is CheckedComboBoxEdit)
                {
                    ((CheckedComboBoxEdit)(Control)sender).ShowPopup();
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

            txtAmount.Text = null;
            LueFromParty.EditValue = null;
            LueToParty.EditValue = null;
            //LueSearchParty.EditValue = null;
            txtRemark.Text = "";

            //DTPEntryDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            //DTPEntryDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            //DTPEntryDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            //DTPEntryDate.Properties.CharacterCasing = CharacterCasing.Upper;
            //DTPEntryDate.EditValue = DateTime.Now;

            //DTPFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            //DTPFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            //DTPFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            //DTPFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
            //DTPFromDate.EditValue = DateTime.Now;

            //DTPToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            //DTPToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            //DTPToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            //DTPToDate.Properties.CharacterCasing = CharacterCasing.Upper;
            //DTPToDate.EditValue = DateTime.Now;

            LueFromParty.Focus();
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
                btnSave.Focus();
                return;
            }

            DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;

            backgroundWorker_AvakJavakCashTransfer.RunWorkerAsync();

            btnSave.Enabled = true;
        }
        private void FrmAvakJavakCashTransfer_Load(object sender, EventArgs e)
        {
            //Global.Message("'" + Val.ToInt32(GlobalDec.gEmployeeProperty.user_id) + "'");

            //if (GlobalDec.gEmployeeProperty.Allow_Developer == 1)
            //{
            Global.LOOKUP_AvakJavakParty(LueFromParty);
            Global.LOOKUP_AvakJavakParty(LueToParty);
            Global.LOOKUP_AvakJavakParty(LueSearchParty);
            //}

            ObjFillCombo.user_id = GlobalDec.gEmployeeProperty.user_id;

            DTPEntryDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPEntryDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPEntryDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPEntryDate.Properties.CharacterCasing = CharacterCasing.Upper;
            DTPEntryDate.EditValue = DateTime.Now;

            DTPFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
            DTPFromDate.EditValue = DateTime.Now;

            DTPToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPToDate.Properties.CharacterCasing = CharacterCasing.Upper;
            DTPToDate.EditValue = DateTime.Now;

            DataTable DTabLocation = ObjFillCombo.FillCmb(FillCombo.TABLE.Location_Master);
            DTabLocation.DefaultView.Sort = "Location_Name";
            DTabLocation = DTabLocation.DefaultView.ToTable();

            lueLocation.Properties.DataSource = DTabLocation;
            lueLocation.Properties.DisplayMember = "Location_Name";
            lueLocation.Properties.ValueMember = "location_id";

            //GetData();
            btnClear_Click(btnClear, null);
        }
        private void backgroundWorker_AvakJavakCashTransfer_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            //if (GlobalDec.gEmployeeProperty.Allow_Developer == 1)
            //{
            AvakJavakCashTransferProperty AvakJavakCashTransferProperty = new AvakJavakCashTransferProperty();
            try
            {
                if (!ValidateDetails())
                {
                    return;
                }
                Conn = new BeginTranConnection(true, false);

                // Conn = new BeginTranConnection(true, false);

                AvakJavakCashTransferProperty.cash_transfer_id = Val.ToInt32(lblMode.Tag);
                AvakJavakCashTransferProperty.cash_transfer_date = Val.DBDate(DTPEntryDate.Text);
                AvakJavakCashTransferProperty.from_party_id = Val.ToInt64(LueFromParty.EditValue);
                AvakJavakCashTransferProperty.to_party_id = Val.ToInt64(LueToParty.EditValue);
                AvakJavakCashTransferProperty.amount = Val.ToDecimal(txtAmount.Text);
                AvakJavakCashTransferProperty.remarks = Val.ToString(txtRemark.Text);

                IntRes = objAvakJavakCashTransfer.Save(AvakJavakCashTransferProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                // Conn.Inter1.Commit();

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
                AvakJavakCashTransferProperty = null;
            }
            //}
            //else
            //{
            //    Global.Message("Cash Transfer Data Not Saved");
            //    return;
            //}
        }

        private void backgroundWorker_AvakJavakCashTransfer_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Avak Javak Cash Transfer");
                    DTPEntryDate.Focus();
                }
                else
                {
                    //if (Val.ToInt(lblMode.Tag) == 0)
                    //{
                    //    Global.Confirm("Avak Javak Cash Transfer Save Successfully");
                    //}
                    //else
                    //{
                    //    Global.Confirm("Avak Javak Cash Transfer Updated Successfully");
                    //}
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

        #region "Grid Events" 
        private void dgvoughSalePaymentEntry_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (e.Clicks == 2)
                {
                    DataRow Drow = grvAvakJavakCasgTransfer.GetDataRow(e.RowHandle);
                    lblMode.Text = "Edit Mode";
                    lblMode.Tag = Val.ToInt64(Drow["cash_transfer_id"]);
                    DTPEntryDate.EditValue = Val.DBDate(Drow["cash_transfer_date"].ToString());
                    LueFromParty.EditValue = Val.ToInt64(Drow["from_party_id"]);
                    LueToParty.EditValue = Val.ToInt64(Drow["to_party_id"]);
                    txtRemark.Text = Val.ToString(Drow["remarks"]);
                    txtAmount.Text = Val.ToString(Drow["amount"]);
                    DTPEntryDate.Focus();
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
                if (LueFromParty.Text == "")
                {
                    lstError.Add(new ListError(13, "From Party"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        LueFromParty.Focus();
                    }
                }
                if (LueToParty.Text == "")
                {
                    lstError.Add(new ListError(13, "To Party"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        LueToParty.Focus();
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
            //if (GlobalDec.gEmployeeProperty.Allow_Developer == 1)
            //{
            AvakJavakCashTransferProperty AvakJavakCashTransferProperty = new AvakJavakCashTransferProperty();

            AvakJavakCashTransferProperty.from_date = Val.DBDate(DTPFromDate.Text);
            AvakJavakCashTransferProperty.to_date = Val.DBDate(DTPToDate.Text);
            AvakJavakCashTransferProperty.from_party_id = Val.ToInt64(LueSearchParty.EditValue);
            AvakJavakCashTransferProperty.location_id = Val.Trim(lueLocation.Properties.GetCheckedItems());

            DataTable DTab = objAvakJavakCashTransfer.GetData_AvakJavakCashTransfer(AvakJavakCashTransferProperty);

            if (DTab.Rows.Count > 0)
            {
                grdAvakJavakCashTransfer.DataSource = DTab;
                grvAvakJavakCasgTransfer.BestFitColumns();
            }
            else
            {
                //Global.Message("Avak Javak Cash Transfer Data Not Found");
                grdAvakJavakCashTransfer.DataSource = null;
                return;
            }
            //}
            //else
            //{
            //    Global.Message("Cash Transfer Data Not Found");
            //    return;
            //}
        }

        #endregion

        #region Export Grid
        private void MNExportExcel_Click(object sender, EventArgs e)
        {
            Global.Export("xlsx", grvAvakJavakCasgTransfer);
        }
        #endregion

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            GetData();
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
            AvakJavakCashTransferProperty AvakJavakCashTransferProperty = new AvakJavakCashTransferProperty();

            DialogResult result = MessageBox.Show("Do you want to Delete data?", "Confirmation", MessageBoxButtons.YesNoCancel);
            if (result != DialogResult.Yes)
            {
                btnDelete.Enabled = true;
                return;
            }

            if (Val.ToInt(lblMode.Tag) != 0)
            {
                Conn = new BeginTranConnection(true, false);
                //Conn = new BeginTranConnection(false, true);
                IntRes = 0;
                AvakJavakCashTransferProperty.cash_transfer_id = Val.ToInt(lblMode.Tag);

                IntRes = objAvakJavakCashTransfer.Delete(AvakJavakCashTransferProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                //Conn.Inter1.Commit();

                Conn.Inter1.Commit();

                if (IntRes > 0)
                {
                    Global.Confirm("Avak Javak Cash Transfer Data Delete Successfully");
                    GetData();
                    btnClear_Click(sender, e);
                }
                else
                {
                    Global.Confirm("Error In Avak Javak Cash Transfer Delete");
                    Conn.Inter1.Rollback();
                    DTPEntryDate.Focus();
                }
            }
            else
            {
                btnDelete.Enabled = true;
                Global.Message("Avak Javak Cash Transfer Data not found");
                Conn = null;
                return;
            }
            btnDelete.Enabled = true;
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

        private void txtBalance_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}
