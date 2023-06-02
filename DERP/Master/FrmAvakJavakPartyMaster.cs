using BLL;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Transaction;
using BLL.PropertyClasses.Master;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DERP.Master
{
    public partial class FrmAvakJavakPartyMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.FormPer ObjPer = new BLL.FormPer();
        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents = new BLL.FormEvents();
        BLL.Validation Val = new BLL.Validation();
        AvakJavakPartyMaster objAvakJavakParty = new AvakJavakPartyMaster();
        DataTable m_dtbDivision = new DataTable();
        int m_numForm_id = 0;
        int IntRes = 0;
        Control _NextEnteredControl = new Control();
        DataTable DtControlSettings = new DataTable();
        private List<Control> _tabControls = new List<Control>();

        #endregion

        #region Constructor
        public FrmAvakJavakPartyMaster()
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
            objBOFormEvents.ObjToDispose.Add(objAvakJavakParty);
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

            txtPartyName.Text = "";
            txtOpeningBalance.Text = null;
            LuePartyGroup.EditValue = null;
            lueDivision.EditValue = null;

            txtAddress.Text = "";
            txtPhone1.Text = "";
            txtPhone2.Text = "";
            chkActive.Checked = true;
            //DTPEntryDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            //DTPEntryDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            //DTPEntryDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            //DTPEntryDate.Properties.CharacterCasing = CharacterCasing.Upper;
            //DTPEntryDate.EditValue = DateTime.Now;
            txtPartyName.Focus();
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

            backgroundWorker_AvakJavakPartyMaster.RunWorkerAsync();

            btnSave.Enabled = true;
        }
        private void FrmAvakJavakPartyMaster_Load(object sender, EventArgs e)
        {
            Global.LOOKUP_AvakJavakPartyGroup(LuePartyGroup);

            DTPEntryDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPEntryDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPEntryDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPEntryDate.Properties.CharacterCasing = CharacterCasing.Upper;
            DTPEntryDate.EditValue = DateTime.Now;

            m_dtbDivision.Columns.Add("division");
            m_dtbDivision.Rows.Add("Office");

            lueDivision.Properties.DataSource = m_dtbDivision;
            lueDivision.Properties.ValueMember = "division";
            lueDivision.Properties.DisplayMember = "division";

            lueDivision.EditValue = Val.ToString(m_dtbDivision.Rows[0]["division"].ToString());

            GetData();
            btnClear_Click(btnClear, null);
        }
        private void backgroundWorker_AvakJavakPartyMaster_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            //if (GlobalDec.gEmployeeProperty.Allow_Developer == 1)
            //{
            AvakJavakParty_MasterProperty AvakJavakPartyMasterProperty = new AvakJavakParty_MasterProperty();
            try
            {
                if (!ValidateDetails())
                {
                    return;
                }

                if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                {
                    Conn = new BeginTranConnection(true, false);
                }
                else
                {
                    Conn = new BeginTranConnection(false, true);
                }
                //Conn = new BeginTranConnection(true, false);

                AvakJavakPartyMasterProperty.party_id = Val.ToInt32(lblMode.Tag);
                AvakJavakPartyMasterProperty.date = Val.DBDate(DTPEntryDate.Text);
                AvakJavakPartyMasterProperty.party_name = Val.ToString(txtPartyName.Text);
                AvakJavakPartyMasterProperty.address = Val.ToString(txtAddress.Text);
                AvakJavakPartyMasterProperty.phone_1 = Val.ToString(txtPhone1.Text);
                AvakJavakPartyMasterProperty.phone_2 = Val.ToString(txtPhone2.Text);
                AvakJavakPartyMasterProperty.opening_balance = Val.ToDecimal(txtOpeningBalance.Text);
                AvakJavakPartyMasterProperty.party_group_id = Val.ToInt64(LuePartyGroup.EditValue);
                AvakJavakPartyMasterProperty.division = Val.ToString(lueDivision.Text);
                AvakJavakPartyMasterProperty.active = Val.ToBoolean(chkActive.Checked);

                IntRes = objAvakJavakParty.Save(AvakJavakPartyMasterProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                //Conn.Inter1.Commit();
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
                AvakJavakPartyMasterProperty = null;
            }
            //}
            //else
            //{
            //    Global.Message("Avak Javak Party Data Not Found");
            //    return;
            //}
        }

        private void backgroundWorker_AvakJavakPartyMaster_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Avak Javak Party Master");
                    txtPartyName.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Avak Javak Party Master Save Successfully");
                    }
                    else
                    {
                        Global.Confirm("Avak Javak Party Master Updated Successfully");
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

        #region "Grid Events" 
        private void dgvoughSalePaymentEntry_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (e.Clicks == 2)
                {
                    DataRow Drow = grvAvakJavakParty.GetDataRow(e.RowHandle);
                    lblMode.Text = "Edit Mode";
                    lblMode.Tag = Val.ToInt64(Drow["party_id"]);
                    DTPEntryDate.EditValue = Val.DBDate(Drow["date"].ToString());
                    txtPartyName.Text = Val.ToString(Drow["party_name"]);
                    txtAddress.Text = Val.ToString(Drow["address"]);
                    txtPhone1.Text = Val.ToString(Drow["phone_1"]);
                    txtPhone2.Text = Val.ToString(Drow["phone_2"]);
                    txtOpeningBalance.Text = Val.ToString(Drow["opening_balance"]);
                    LuePartyGroup.EditValue = Val.ToInt64(Drow["party_group_id"]);
                    lueDivision.Text = Val.ToString(Drow["division"]);
                    chkActive.Checked = Val.ToBoolean(Drow["is_active"]);
                    txtPartyName.Focus();
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
                if (txtPartyName.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Party Name"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtPartyName.Focus();
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
            DataTable DTab = objAvakJavakParty.GetData_AvakJavakParty(0);

            if (DTab.Rows.Count > 0)
            {
                grdAvakJavakParty.DataSource = DTab;
                grvAvakJavakParty.BestFitColumns();
            }
            else
            {
                //Global.Message("Avak Javak Cash Transfer Data Not Found");
                grdAvakJavakParty.DataSource = null;
                return;
            }

            //}
            //else
            //{
            //    Global.Message("Avak Javak Party Master Data Not Found");
            //    return;
            //}
        }

        #endregion

        #region Export Grid
        private void MNExportExcel_Click(object sender, EventArgs e)
        {
            Global.Export("xlsx", grvAvakJavakParty);
        }
        #endregion

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ObjPer.SetFormPer();
            if (ObjPer.AllowDelete == false)
            {
                Global.Message(BLL.GlobalDec.gStrPermissionDelMsg);
                return;
            }
            btnDelete.Enabled = false;
            AvakJavakParty_MasterProperty AvakJavakPartyMasterProperty = new AvakJavakParty_MasterProperty();

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
                AvakJavakPartyMasterProperty.party_id = Val.ToInt(lblMode.Tag);

                IntRes = objAvakJavakParty.Delete(AvakJavakPartyMasterProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                //Conn.Inter1.Commit();

                Conn.Inter1.Commit();

                if (IntRes > 0)
                {
                    Global.Confirm("Avak Javak Party Master Data Delete Successfully");
                    GetData();
                    btnClear_Click(sender, e);
                }
                else
                {
                    Global.Confirm("Error In Avak Javak Party Master Delete");
                    Conn.Inter2.Rollback();
                    DTPEntryDate.Focus();
                }
            }
            else
            {
                btnDelete.Enabled = true;
                Global.Message("Party ID not found");
                Conn.Inter1.Rollback();
                Conn = null;
                return;
            }
            btnDelete.Enabled = true;
        }

        private void txtOpeningBalance_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtPhone1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPhone2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
