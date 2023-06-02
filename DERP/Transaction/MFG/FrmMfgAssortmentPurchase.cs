using BLL;
using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.PropertyClasses.Transaction;
using BLL.PropertyClasses.Transaction.MFG;
using DERP.Class;
using DERP.Master;
using DERP.Report;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Transaction.MFG
{
    public partial class FrmMfgAssortmentPurchase : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        DataTable m_dtbKapan;

        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        private List<Control> _tabControls = new List<Control>();
        Control _NextEnteredControl = new Control();
        DataTable DtControlSettings;

        MFGAssortmentPurchase objAssortPurchase;
        int m_numForm_id;
        int IntRes;
        decimal m_numSummRate = 0;

        #endregion

        #region Constructor
        public FrmMfgAssortmentPurchase()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            objAssortPurchase = new MFGAssortmentPurchase();
            m_dtbKapan = new DataTable();
            DtControlSettings = new DataTable();
            m_numForm_id = 0;
            IntRes = 0;
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
            objBOFormEvents.ObjToDispose.Add(objAssortPurchase);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
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
        private void FrmRejectionToMakableTransferConfirm_Load(object sender, EventArgs e)
        {
            try
            {
                dtpAssortPurchaseDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpAssortPurchaseDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpAssortPurchaseDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpAssortPurchaseDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpAssortPurchaseDate.EditValue = DateTime.Now;

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

                DtpPrintDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                DtpPrintDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                DtpPrintDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                DtpPrintDate.Properties.CharacterCasing = CharacterCasing.Upper;
                DtpPrintDate.EditValue = DateTime.Now;

                Global.LOOKUPAssortLocation(lueAssortLocation);
                ClearDetails();
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ObjPer.FormName = this.Name.ToUpper();
                ObjPer.SetFormPer();
                if (ObjPer.AllowInsert == false)
                {
                    Global.Message(BLL.GlobalDec.gStrPermissionInsUpdMsg);
                    return;
                }
                if (lblMode.Text == "Edit Mode")
                {
                    if (ObjPer.AllowUpdate == false)
                    {
                        Global.Message(BLL.GlobalDec.gStrPermissionInsUpdMsg);
                        return;
                    }
                }
                btnSave.Enabled = false;

                if (SaveDetails())
                {
                    GetData();
                    btnClear_Click(sender, e);
                }

                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                this.Cursor = Cursors.Default;
                btnSave.Enabled = true;
                return;
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearDetails();
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
        private void btnDelete_Click(object sender, EventArgs e)
        {
            //if (Val.ToString(lblMode.Text) != "Edit Mode")
            //{
            //    return;
            //}
            //btnDelete.Enabled = false;
            DialogResult result = MessageBox.Show("Do you want to Delete data?", "Confirmation", MessageBoxButtons.YesNoCancel);
            if (result != DialogResult.Yes)
            {
                btnDelete.Enabled = true;
                return;
            }
            MFGAssortmentPurchaseProperty MFGAssortmentPurchaseProperty = new MFGAssortmentPurchaseProperty();
            objAssortPurchase = new MFGAssortmentPurchase();
            try
            {
                DataTable DTab = (DataTable)grdAssortmentPurchase.DataSource;

                DTab.AcceptChanges();
                if (DTab.Select("SEL = 'True' ").Length == 0)
                {
                    Global.Message("Please Select Atleast One Entry.");
                    return;
                }

                if (DTab.Rows.Count > 0)
                {
                    DataTable dtKapanData = new DataTable();
                    dtKapanData = DTab.Select("SEL = 'True' ").CopyToDataTable();

                    foreach (DataRow DR in dtKapanData.Rows)
                    {
                        MFGAssortmentPurchaseProperty.assort_purchase_id = Val.ToInt(DR["assort_purchase_id"]);
                        IntRes = objAssortPurchase.Delete(MFGAssortmentPurchaseProperty);
                    }
                }

                //MFGAssortmentPurchaseProperty.assort_purchase_id = Val.ToInt(lblMode.Tag);
                //IntRes = objAssortPurchase.Delete(MFGAssortmentPurchaseProperty);

                if (IntRes > 0)
                {
                    Global.Confirm("Assortment Purchase Data Deleted Succesfully");
                    btnDelete.Enabled = true;
                    btnClear_Click(null, null);
                    GetData();
                }
                else
                {
                    Global.Confirm("Error In Assortment Purchase Data");
                    btnDelete.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                IntRes = -1;
                General.ShowErrors(ex.ToString());
                btnDelete.Enabled = true;
                return;
            }
            finally
            {
                MFGAssortmentPurchaseProperty = null;
                btnDelete.Enabled = true;
            }
        }
        private void txtCarat_EditValueChanged(object sender, EventArgs e)
        {
            txtAmount.Text = Val.ToString(Math.Round(Val.ToDecimal(Val.ToDecimal(txtPurchaseCarat.Text) * Val.ToDecimal(txtRate.Text)), 0));
        }
        private void txtRate_EditValueChanged(object sender, EventArgs e)
        {
            txtAmount.Text = Val.ToString(Math.Round(Val.ToDecimal(Val.ToDecimal(txtPurchaseCarat.Text) * Val.ToDecimal(txtRate.Text)), 0));
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            GetData();
        }
        private void lueAssortLocation_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmAssortLocationMaster frmAssortLocation = new FrmAssortLocationMaster();
                frmAssortLocation.ShowDialog();
                Global.LOOKUPAssortLocation(lueAssortLocation);
            }
        }
        private void BtnExport_Click(object sender, EventArgs e)
        {
            Export("xlsx", "Export to Excel", "Excel files 97-2003 (Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*");
        }

        #region GridEvents      

        private void dgvAssortmentPurchase_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            if (Val.ToDecimal(clmAmount.SummaryItem.SummaryValue) > 0 && Val.ToDecimal(clmPurchaseCarat.SummaryItem.SummaryValue) > 0)
            {
                m_numSummRate = Math.Round((Val.ToDecimal(clmAmount.SummaryItem.SummaryValue) / Val.ToDecimal(clmPurchaseCarat.SummaryItem.SummaryValue)), 0, MidpointRounding.AwayFromZero);
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
        private void dgvAssortmentPurchase_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvAssortmentPurchase.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt64(Drow["assort_purchase_id"]);
                        dtpAssortPurchaseDate.Text = Val.DBDate(Val.ToString(Drow["assort_purchase_date1"]));
                        lueAssortLocation.EditValue = Val.ToInt64(Drow["assort_location_id"]);
                        txtPurchaseCarat.Text = Val.ToString(Drow["purchase_carat"]);
                        txtNetCarat.Text = Val.ToString(Drow["net_carat"]);
                        txtRate.Text = Val.ToString(Drow["rate"]);
                        txtLess.Text = Val.ToString(Drow["less"]);
                        txtRemark.Text = Val.ToString(Drow["remarks"]);
                        txtKapanCutNo.Text = Val.ToString(Drow["kapan_cut_no"]);
                        txtSrNo.Text = Val.ToString(Drow["sr_no"]);
                        btnDelete.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void txtPurchaseCarat_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            //{
            //    e.Handled = true;
            //}

            //// only allow one decimal point
            //if (e.KeyChar == '.' && (sender as DevExpress.XtraEditors.TextEdit).Text.IndexOf('.') > -1)
            //{
            //    e.Handled = true;
            //}
        }
        private void txtNetCarat_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            //{
            //    e.Handled = true;
            //}

            //// only allow one decimal point
            //if (e.KeyChar == '.' && (sender as DevExpress.XtraEditors.TextEdit).Text.IndexOf('.') > -1)
            //{
            //    e.Handled = true;
            //}
        }
        private void txtRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            //{
            //    e.Handled = true;
            //}

            //// only allow one decimal point
            //if (e.KeyChar == '.' && (sender as DevExpress.XtraEditors.TextEdit).Text.IndexOf('.') > -1)
            //{
            //    e.Handled = true;
            //}
        }
        private void txtLess_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            //{
            //    e.Handled = true;
            //}

            //// only allow one decimal point
            //if (e.KeyChar == '.' && (sender as DevExpress.XtraEditors.TextEdit).Text.IndexOf('.') > -1)
            //{
            //    e.Handled = true;
            //}
        }
        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            //{
            //    e.Handled = true;
            //}

            //// only allow one decimal point
            //if (e.KeyChar == '.' && (sender as DevExpress.XtraEditors.TextEdit).Text.IndexOf('.') > -1)
            //{
            //    e.Handled = true;
            //}
        }

        #endregion

        #endregion

        #region Functions  
        private bool ValidateDetails()
        {

            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {

                if (Val.ToString(dtpAssortPurchaseDate.Text) == string.Empty)
                {
                    lstError.Add(new ListError(22, "Date"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        dtpAssortPurchaseDate.Focus();
                    }
                }
                if (lueAssortLocation.Text.Length == 0)
                {
                    lstError.Add(new ListError(12, "Location"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueAssortLocation.Focus();
                    }
                }
                if (txtKapanCutNo.Text == "")
                {
                    lstError.Add(new ListError(12, "Kapan Cut No"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtKapanCutNo.Focus();
                    }
                }

                if (Val.ToDecimal(txtPurchaseCarat.Text) == 0)
                {
                    lstError.Add(new ListError(5, "Carat Not Be Blank!!"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtPurchaseCarat.Focus();
                    }
                }
                if (Val.ToDecimal(txtRate.Text) == 0)
                {
                    lstError.Add(new ListError(5, "Rate Not Be Blank!!"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtRate.Focus();
                    }
                }
                if (Val.ToDecimal(txtAmount.Text) == 0)
                {
                    lstError.Add(new ListError(5, "Amount Not Be Blank!!"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtPurchaseCarat.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                lstError.Add(new ListError(ex));
            }
            return (!(BLL.General.ShowErrors(lstError)));
        }
        private void GetData()
        {
            objAssortPurchase = new MFGAssortmentPurchase();
            //DateTime datFromDate = DateTime.MinValue;
            //DateTime datToDate = DateTime.MinValue;
            chkAll.Checked = false;
            try
            {
                DataTable dtKapanData = new DataTable();
                dtKapanData = objAssortPurchase.GetData(Val.DBDate(dtpFromDate.Text), Val.DBDate(dtpToDate.Text));

                if (dtKapanData.Columns.Contains("SEL") == false)
                {
                    if (dtKapanData.Columns.Contains("SEL") == false)
                    {
                        DataColumn Col = new DataColumn();
                        Col.ColumnName = "SEL";
                        Col.DataType = typeof(bool);
                        Col.DefaultValue = false;
                        dtKapanData.Columns.Add(Col);
                    }
                }
                dtKapanData.Columns["SEL"].SetOrdinal(0);

                grdAssortmentPurchase.DataSource = dtKapanData;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
            finally
            {
                objAssortPurchase = null;
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
                            dgvAssortmentPurchase.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvAssortmentPurchase.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvAssortmentPurchase.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvAssortmentPurchase.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvAssortmentPurchase.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvAssortmentPurchase.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvAssortmentPurchase.ExportToCsv(Filepath);
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
        private bool ClearDetails()
        {
            bool blnReturn = true;
            try
            {
                //dtpAssortPurchaseDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                //dtpAssortPurchaseDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                //dtpAssortPurchaseDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                //dtpAssortPurchaseDate.Properties.CharacterCasing = CharacterCasing.Upper;
                //dtpAssortPurchaseDate.EditValue = DateTime.Now;

                //lueAssortLocation.EditValue = null;
                lblMode.Text = "Add Mode";
                //txtKapanCutNo.Text = "";
                lblMode.Tag = "0";
                btnSave.Enabled = true;
                // btnDelete.Enabled = false;
                //lueAssortLocation.Focus();

                txtPurchaseCarat.Text = "0";
                txtNetCarat.Text = "0";
                txtRate.Text = "0";
                txtAmount.Text = "0";
                txtLess.Text = "0";
                txtRemark.Text = "";
                txtSrNo.Text = "";
                txtSrNo.Focus();
                chkAll.Checked = false;
                GetData();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
        }
        private bool SaveDetails()
        {
            bool blnReturn = true;
            MFGAssortmentPurchaseProperty MFGAssortmentPurchaseProperty = new MFGAssortmentPurchaseProperty();
            MFGAssortmentPurchase objAssortPurchase = new MFGAssortmentPurchase();
            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }
                MFGAssortmentPurchaseProperty.assort_purchase_id = Val.ToInt64(lblMode.Tag);
                MFGAssortmentPurchaseProperty.assort_purchase_date = Val.ToString(dtpAssortPurchaseDate.Text);
                MFGAssortmentPurchaseProperty.assort_location_id = Val.ToInt64(lueAssortLocation.EditValue);
                MFGAssortmentPurchaseProperty.purchase_carat = Val.ToDecimal(txtPurchaseCarat.Text);
                MFGAssortmentPurchaseProperty.net_carat = Val.ToDecimal(txtNetCarat.Text);
                MFGAssortmentPurchaseProperty.rate = Val.ToDecimal(txtRate.Text);
                MFGAssortmentPurchaseProperty.amount = Val.ToDecimal(txtAmount.Text);
                MFGAssortmentPurchaseProperty.less = Val.ToDecimal(txtLess.Text);
                MFGAssortmentPurchaseProperty.remarks = Val.ToString(txtRemark.Text);
                MFGAssortmentPurchaseProperty.kapan_cut_no = Val.ToString(txtKapanCutNo.Text);
                MFGAssortmentPurchaseProperty.sr_no = Val.ToString(txtSrNo.Text);

                int IntRes = objAssortPurchase.Save(MFGAssortmentPurchaseProperty);
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Assortment Purchase");
                    lueAssortLocation.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Assortment Purchase Saved Data Successfully");
                    }
                    else
                    {
                        Global.Confirm("Assortment Purchase Update Successfully");
                    }
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                blnReturn = false;
            }
            finally
            {
                MFGAssortmentPurchaseProperty = null;
            }

            return blnReturn;
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

        private void btn_Print_Click(object sender, EventArgs e)
        {
            objAssortPurchase = new MFGAssortmentPurchase();
            try
            {
                if (ChkCheckPrint.Checked == false)
                {
                    DataTable dtKapanData = new DataTable();

                    dtKapanData = objAssortPurchase.GetData_Print(Val.DBDate(""), Val.DBDate(""), Val.DBDate(DtpPrintDate.Text));

                    FrmReportViewer FrmReportViewer = new FrmReportViewer();
                    FrmReportViewer.DS.Tables.Add(dtKapanData);
                    FrmReportViewer.GroupBy = "";
                    FrmReportViewer.RepName = "";
                    FrmReportViewer.RepPara = "";
                    this.Cursor = Cursors.Default;
                    FrmReportViewer.AllowSetFormula = true;

                    FrmReportViewer.ShowForm("Assort_Purchase_Entry_One", 120, FrmReportViewer.ReportFolder.ACCOUNT);

                    dtKapanData = null;
                    FrmReportViewer.DS.Tables.Clear();
                    FrmReportViewer.DS.Clear();
                    FrmReportViewer = null;
                }
                else if (ChkCheckPrint.Checked == true)
                {
                    DataTable DTab = (DataTable)grdAssortmentPurchase.DataSource;

                    DTab.AcceptChanges();
                    if (DTab.Select("SEL = 'True' ").Length == 0)
                    {
                        Global.Message("Please Select Atleast One Entry.");
                        return;
                    }

                    if (DTab.Rows.Count > 0)
                    {
                        DataTable dtKapanData = new DataTable();
                        dtKapanData = DTab.Select("SEL = 'True' ").CopyToDataTable();

                        FrmReportViewer FrmReportViewer = new FrmReportViewer();
                        FrmReportViewer.DS.Tables.Add(dtKapanData);
                        FrmReportViewer.GroupBy = "";
                        FrmReportViewer.RepName = "";
                        FrmReportViewer.RepPara = "";
                        this.Cursor = Cursors.Default;
                        FrmReportViewer.AllowSetFormula = true;

                        FrmReportViewer.ShowForm("Assort_Purchase_Entry_One", 120, FrmReportViewer.ReportFolder.ACCOUNT);

                        dtKapanData = null;
                        FrmReportViewer.DS.Tables.Clear();
                        FrmReportViewer.DS.Clear();
                        FrmReportViewer = null;
                    }

                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
            finally
            {
                objAssortPurchase = null;
            }
        }

        private void btn_AllInOne_Click(object sender, EventArgs e)
        {
            objAssortPurchase = new MFGAssortmentPurchase();
            try
            {
                if (ChkCheckPrint.Checked == false)
                {
                    DataTable dtKapanData = new DataTable();
                    dtKapanData = objAssortPurchase.GetData_Print_All(Val.DBDate(dtpFromDate.Text), Val.DBDate(dtpToDate.Text), Val.DBDate(""));

                    FrmReportViewer FrmReportViewer = new FrmReportViewer();
                    FrmReportViewer.DS.Tables.Add(dtKapanData);
                    FrmReportViewer.GroupBy = "";
                    FrmReportViewer.RepName = "";
                    FrmReportViewer.RepPara = "";
                    this.Cursor = Cursors.Default;
                    FrmReportViewer.AllowSetFormula = true;

                    FrmReportViewer.ShowForm("Assort_Purchase_Entry", 120, FrmReportViewer.ReportFolder.ACCOUNT);

                    dtKapanData = null;
                    FrmReportViewer.DS.Tables.Clear();
                    FrmReportViewer.DS.Clear();
                    FrmReportViewer = null;
                }
                else if (ChkCheckPrint.Checked == true)
                {
                    DataTable DTab = (DataTable)grdAssortmentPurchase.DataSource;

                    DTab.AcceptChanges();
                    if (DTab.Select("SEL = 'True' ").Length == 0)
                    {
                        Global.Message("Please Select Atleast One Entry.");
                        return;
                    }

                    if (DTab.Rows.Count > 0)
                    {
                        DataTable dtKapanData = new DataTable();
                        dtKapanData = DTab.Select("SEL = 'True' ").CopyToDataTable();

                        FrmReportViewer FrmReportViewer = new FrmReportViewer();
                        FrmReportViewer.DS.Tables.Add(dtKapanData);
                        FrmReportViewer.GroupBy = "";
                        FrmReportViewer.RepName = "";
                        FrmReportViewer.RepPara = "";
                        this.Cursor = Cursors.Default;
                        FrmReportViewer.AllowSetFormula = true;

                        FrmReportViewer.ShowForm("Assort_Purchase_Entry", 120, FrmReportViewer.ReportFolder.ACCOUNT);

                        dtKapanData = null;
                        FrmReportViewer.DS.Tables.Clear();
                        FrmReportViewer.DS.Clear();
                        FrmReportViewer = null;
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
            finally
            {
                objAssortPurchase = null;
            }
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dgvAssortmentPurchase.RowCount; i++)
                {
                    dgvAssortmentPurchase.SetRowCellValue(i, "SEL", chkAll.Checked);
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
