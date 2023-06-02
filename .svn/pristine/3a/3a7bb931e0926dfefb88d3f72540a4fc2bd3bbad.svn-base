using BLL;
using BLL.FunctionClasses.Master.MFG;
using BLL.PropertyClasses.Master.MFG;
using DERP.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Master.MFG
{
    public partial class FrmMfgMachineItemUsedEntry : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.FormEvents objBOFormEvents;
        BLL.Validation Val;
        BLL.FormPer ObjPer;
        int m_ItemId = 0;
        MfgMachineItemUsedEntry objMachineItem;
        #endregion

        #region Constructor
        public FrmMfgMachineItemUsedEntry()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            Val = new BLL.Validation();
            ObjPer = new BLL.FormPer();

            objMachineItem = new MfgMachineItemUsedEntry();
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
            this.Show();
        }
        private void AttachFormEvents()
        {
            objBOFormEvents.CurForm = this;
            objBOFormEvents.FormKeyPress = true;
            objBOFormEvents.FormKeyDown = true;
            objBOFormEvents.FormResize = true;
            objBOFormEvents.FormClosing = true;
            objBOFormEvents.ObjToDispose.Add(objMachineItem);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion

        #region Events
        private void FrmMfgMachineItemMaster_Load(object sender, EventArgs e)
        {
            try
            {
                //Global.LOOKUPMachineEntry(lueItemName);
                Global.LOOKUPMachine(lueMachine);
                //DataTable dtIss = objMachineItem.GetData(0);
                //if(dtIss.Rows.Count > 0)
                //{
                //    lueItemName.Properties.ValueMember = "machine_item_id";
                //    lueItemName.Properties.DisplayMember = "item_name";
                //}
                //else
                //{
                //    lueItemName.EditValue = null;
                //}

                //DTPPurchaseDate.EditValue = DateTime.Now;
                //DTPInstallDate.EditValue = DateTime.Now;
                //dtpFromDate.EditValue = DateTime.Now;
                //dtpToDate.EditValue = DateTime.Now;
                var today = DateTime.Now;
                var From_Date = new DateTime(today.Year, today.Month, 1);
                dtpFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpFromDate.EditValue = From_Date;

                dtpToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpToDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpToDate.EditValue = DateTime.Now;
                GetData();
                btnClear_Click(btnClear, null);

            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            ObjPer.FormName = this.Name.ToUpper();
            ObjPer.SetFormPer();
            if (ObjPer.AllowUpdate == false || ObjPer.AllowInsert == false)
            {
                Global.Message(BLL.GlobalDec.gStrPermissionInsUpdMsg);
                return;
            }
            btnSave.Enabled = false;

            if (SaveDetails())
            {
                GetData();
                btnClear_Click(sender, e);
            }

            btnSave.Enabled = true;
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                lblMode.Tag = 0;
                lblMode.Text = "Add Mode";
                lueMachine.EditValue = null;
                lueItemName.EditValue = null;
                txtCompanyName.Text = string.Empty;
                txtPartyName.Text = string.Empty;
                txtAvailableQty.Text = string.Empty;
                txtSerialNumber.Text = string.Empty;
                //txtQty.Text = string.Empty;
                txtRate.Text = string.Empty;
                txtAmount.Text = string.Empty;
                txtRemark.Text = string.Empty;
                txtQty.Text = Val.ToString("1");
                var today = DateTime.Now;
                var From_Date = new DateTime(today.Year, today.Month, 1);
                dtpFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpFromDate.EditValue = From_Date;

                dtpToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpToDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpToDate.EditValue = DateTime.Now;
                //dtpFromDate.EditValue = DateTime.Now;
                //dtpToDate.EditValue = DateTime.Now;
                DTPInstallDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                DTPInstallDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                DTPInstallDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                DTPInstallDate.Properties.CharacterCasing = CharacterCasing.Upper;
                DTPInstallDate.EditValue = DateTime.Now;

                //DTPInstallDate.EditValue = DateTime.Now;
                DataTable dtIss = objMachineItem.GetData(0);
                if (dtIss.Rows.Count > 0)
                {
                    lueItemName.Properties.DataSource = dtIss;
                    lueItemName.Properties.ValueMember = "machine_detail_id";
                    lueItemName.Properties.DisplayMember = "item_name";
                }
                else
                {
                    lueItemName.Properties.DataSource = dtIss;
                    lueItemName.EditValue = null;
                }
                //chkActive.Checked = true;
                m_ItemId = 0;
                GetData();
                lueItemName.Focus();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region GridEvents       
        private void dgvMfgGroupMaster_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvMachineItemMaster.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt64(Drow["item_used_id"]);
                        lueMachine.EditValue = Val.ToInt32(Drow["machine_id"]);
                        //lueMachine.Text = Val.ToString(Drow["machine_name"]);
                        txtCompanyName.Text = Val.ToString(Drow["company_name"]);
                        txtCompanyName.Tag = Val.ToInt64(Drow["company_id"]);
                        lueItemName.EditValue = Val.ToInt64(Drow["item_id"]);
                        lueItemName.Text = Val.ToString(Drow["item_name"]);
                        txtPartyName.Text = Val.ToString(Drow["party_name"]);
                        txtPartyName.Tag = Val.ToInt(Drow["party_id"]);
                        DTPInstallDate.Text = Val.ToString(Drow["install_date"]);
                        txtQty.Text = Val.ToString(Drow["qty"]);
                        txtSerialNumber.Text = Val.ToString(Drow["srno"]);
                        txtRate.Text = Val.ToString(Drow["rate"]);
                        txtAmount.Text = Val.ToString(Drow["amount"]);
                        //DTPWarrantyFinalDate.Text = Val.ToDate;
                        txtRemark.Text = Val.ToString(Drow["remarks"]);

                        lueMachine.Focus();
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
        private bool SaveDetails()
        {
            bool blnReturn = true;
            MfgMachineItemUsedEntryProperty ItemMasterProperty = new MfgMachineItemUsedEntryProperty();

            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }

                ItemMasterProperty.item_used_id = Val.ToInt32(lblMode.Tag);
                ItemMasterProperty.machine_number = Val.ToInt(lueMachine.EditValue);
                ItemMasterProperty.machine_detail_id = Val.ToInt32(lueItemName.EditValue);
                ItemMasterProperty.machine_item_id = Val.ToInt32(m_ItemId);
                ItemMasterProperty.machine_company_id = Val.ToInt32(txtCompanyName.Tag);
                ItemMasterProperty.party_name = Val.ToString(txtPartyName.Text);
                ItemMasterProperty.machine_party_id = Val.ToInt32(txtPartyName.Tag);
                ItemMasterProperty.install_date = Val.DBDate(DTPInstallDate.Text);
                ItemMasterProperty.qty = Val.ToInt32(txtQty.Text);
                ItemMasterProperty.rate = Val.ToDecimal(txtRate.Text);
                ItemMasterProperty.amount = Val.ToDecimal(txtAmount.Text);
                ItemMasterProperty.srno = Val.ToString(txtSerialNumber.Text);
                ItemMasterProperty.remark = txtRemark.Text.ToUpper();
                //ItemMasterProperty.active = Val.ToBoolean(chkActive.Checked);

                int IntRes = objMachineItem.Save(ItemMasterProperty);
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Saving Machine Detail Entry");
                    lueMachine.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Machine Item Details Entry Saved Successfully");
                    }
                    else
                    {
                        Global.Confirm("Machine Item Details Entry Updated Successfully");
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
                ItemMasterProperty = null;
            }

            return blnReturn;
        }
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                //if (lueItemCompany.Text == string.Empty)
                //{
                //    lstError.Add(new ListError(12, "Company Name"));
                //    if (!blnFocus)
                //    {
                //        blnFocus = true;
                //        lueItemCompany.Focus();
                //    }
                //}


                //if (!objMachineItem.ISExists(lueItemCompany.Text, Val.ToInt64(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
                //{
                //    lstError.Add(new ListError(23, "Item"));
                //    if (!blnFocus)
                //    {
                //        blnFocus = true;
                //        lueItemCompany.Focus();
                //    }

                //}

                if (lueItemName.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Item Name"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueItemName.Focus();
                    }
                }

                //if (lueParty.Text == string.Empty)
                //{
                //    lstError.Add(new ListError(12, "Party Name"));
                //    if (!blnFocus)
                //    {
                //        blnFocus = true;
                //        lueParty.Focus();
                //    }
                //}

                if (DTPInstallDate.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Purchase Date"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        DTPInstallDate.Focus();
                    }
                }

                if (DTPInstallDate.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Install Date"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        DTPInstallDate.Focus();
                    }
                }

                //if (txtSerialNumber.Text == string.Empty)
                //{
                //    lstError.Add(new ListError(12, "Serial Number"));
                //    if (!blnFocus)
                //    {
                //        blnFocus = true;
                //        txtSerialNumber.Focus();
                //    }
                //}

                if (txtRate.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Rate"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtRate.Focus();
                    }
                }

                //if (txtAmount.Text == string.Empty)
                //{
                //    lstError.Add(new ListError(12, "Amount"));
                //    if (!blnFocus)
                //    {
                //        blnFocus = true;
                //        txtAmount.Focus();
                //    }
                //}
                //if (txtYear.Text == string.Empty || txtMonth.Text == string.Empty || txtDays.Text == string.Empty)
                //{
                //    lstError.Add(new ListError(12, "Year/Month/Days"));
                //    if (!blnFocus)
                //    {
                //        blnFocus = true;
                //        txtYear.Focus();
                //    }
                //}
            }
            catch (Exception ex)
            {
                lstError.Add(new ListError(ex));
            }
            return (!(BLL.General.ShowErrors(lstError)));

        }
        public void GetData()
        {
            try
            {
                DataTable DTab = objMachineItem.GetData1(Val.DBDate(dtpFromDate.Text), Val.DBDate(dtpToDate.Text));
                grdMachineItemMaster.DataSource = DTab;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
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
                            dgvMachineItemMaster.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvMachineItemMaster.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvMachineItemMaster.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvMachineItemMaster.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvMachineItemMaster.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvMachineItemMaster.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvMachineItemMaster.ExportToCsv(Filepath);
                            break;
                    }

                    if (format.Equals(Exports.xlsx.ToString()))
                    {
                        if (Global.Confirm("Export Done\n\nDo you want to open Excel File?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(Filepath);
                        }
                    }
                    else if (format.Equals(Exports.pdf.ToString()))
                    {
                        if (Global.Confirm("Export Done\n\nDo you want to open PDF File?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(Filepath);
                        }
                    }
                    else
                    {
                        if (Global.Confirm("Export Done\n\nDo you want to open file?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
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

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelControl5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void labelControl9_Click(object sender, EventArgs e)
        {

        }

        private void labelControl10_Click(object sender, EventArgs e)
        {

        }

        private void labelControl21_Click(object sender, EventArgs e)
        {

        }

        private void lueItemCompany_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void labelControl24_Click(object sender, EventArgs e)
        {

        }

        private void dockPanel1_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable DTab = objMachineItem.GetData1(Val.DBDate(dtpFromDate.Text), Val.DBDate(dtpToDate.Text));
                grdMachineItemMaster.DataSource = DTab;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }

        }

        private void DTPWarrantyFinalDate_EditValueChanged(object sender, EventArgs e)
        {

        }

        //private void txtYear_EditValueChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (DTPInstallDate.Text.Length <= 0 || DTPInstallDate.Text == "")
        //        {
        //            txtYear.Text = "";
        //            txtMonth.Text = "";
        //            txtDays.Text = "";
        //            DTPWarrantyFinalDate.EditValue = null;

        //        }
        //        else
        //        {

        //            Double Days = (Val.ToDouble(txtYear.Text)*365)+ (Val.ToDouble(txtMonth.Text) *30) + Val.ToDouble(txtDays.Text);
        //            DateTime Date = Convert.ToDateTime(DTPInstallDate.EditValue).AddDays(Val.ToDouble(Days));
        //            DTPWarrantyFinalDate.EditValue = Val.DBDate(Date.ToShortDateString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        General.ShowErrors(ex.ToString());
        //        return;
        //    }
        //}

        //private void txtMonth_EditValueChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (DTPInstallDate.Text.Length <= 0 || DTPInstallDate.Text == "")
        //        {
        //            txtYear.Text = "";
        //            txtMonth.Text = "";
        //            txtDays.Text = "";
        //            DTPWarrantyFinalDate.EditValue = null;

        //        }
        //        else
        //        {

        //            Double Days = (Val.ToDouble(txtYear.Text) * 365) + (Val.ToDouble(txtMonth.Text) * 30) + Val.ToDouble(txtDays.Text);
        //            DateTime Date = Convert.ToDateTime(DTPInstallDate.EditValue).AddDays(Val.ToDouble(Days));
        //            DTPWarrantyFinalDate.EditValue = Val.DBDate(Date.ToShortDateString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        General.ShowErrors(ex.ToString());
        //        return;
        //    }
        //}

        //private void txtDays_EditValueChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (DTPInstallDate.Text.Length <= 0 || DTPInstallDate.Text == "")
        //        {
        //            txtYear.Text = "";
        //            txtMonth.Text = "";
        //            txtDays.Text = "";
        //            DTPWarrantyFinalDate.EditValue = null;

        //        }
        //        else
        //        {

        //            Double Days = (Val.ToDouble(txtYear.Text) * 365) + (Val.ToDouble(txtMonth.Text) * 30) + Val.ToDouble(txtDays.Text);
        //            DateTime Date = Convert.ToDateTime(DTPInstallDate.EditValue).AddDays(Val.ToDouble(Days));
        //            DTPWarrantyFinalDate.EditValue = Val.DBDate(Date.ToShortDateString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        General.ShowErrors(ex.ToString());
        //        return;
        //    }
        //}

        private void txtYear_Properties_EditValueChanged(object sender, EventArgs e)
        {

        }

        //private void txtRate_EditValueChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if ((txtSerialNumber.Text.Length <= 0 || txtSerialNumber.Text == "" ) && (txtRate.Text.Length <= 0 || txtRate.Text == ""))
        //        {
        //            txtSerialNumber.Text = "";
        //            txtRate.Text = "";
        //            txtAmount.Text = "";
        //        }
        //        else
        //        {
        //            int a = Val.ToInt32(txtSerialNumber.Text) * Val.ToInt32(txtRate.Text);
        //            txtAmount.Text = Convert.ToString(a);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        General.ShowErrors(ex.ToString());
        //        return;
        //    }
        //}

        //private void txtQty_EditValueChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if ((txtSerialNumber.Text.Length <= 0 || txtSerialNumber.Text == "") && (txtRate.Text.Length <= 0 || txtRate.Text == ""))
        //        {
        //            txtSerialNumber.Text = "";
        //            txtRate.Text = "";
        //            txtAmount.Text = "";
        //        }
        //        else
        //        {
        //            int a = Val.ToInt32(txtSerialNumber.Text) * Val.ToInt32(txtRate.Text);
        //            txtAmount.Text = Convert.ToString(a);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        General.ShowErrors(ex.ToString());
        //        return;
        //    }
        //}

        private void lueItemName_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (lueItemName.EditValue != null && lueItemName.EditValue != System.DBNull.Value)
                {
                    DataTable dtIss = new DataTable();
                    string strFilter = string.Empty;
                    dtIss = objMachineItem.GetData(Val.ToInt32(lueItemName.EditValue));
                    //if (Val.ToInt32(lueItemName.EditValue) > 0)
                    //    strFilter = "purchase_detail_id = " + Val.ToInt32(lueInvoiceNo.EditValue);

                    //dtIss.DefaultView.RowFilter = strFilter;
                    //dtIss.DefaultView.ToTable();

                    //DataTable dtb = dtIss.DefaultView.ToTable();
                    if (dtIss.Rows.Count > 0)
                    {
                        txtCompanyName.Text = Val.ToString(dtIss.Rows[0]["company_name"]);
                        txtCompanyName.Tag = Val.ToInt(dtIss.Rows[0]["item_company_id"]);
                        m_ItemId = Val.ToInt(dtIss.Rows[0]["machine_item_id"]);
                        txtPartyName.Text = Val.ToString(dtIss.Rows[0]["party_name"]);
                        txtPartyName.Tag = Val.ToInt(dtIss.Rows[0]["item_party_id"]);
                        txtAvailableQty.Text = Val.ToString(dtIss.Rows[0]["qty"]);
                        txtRate.Text = Val.ToString(dtIss.Rows[0]["rate"]);
                        txtAmount.Text = Val.ToString(Val.ToDecimal(txtRate.Text) * Val.ToDecimal(txtQty.Text));
                        txtRemark.Text = Val.ToString(dtIss.Rows[0]["remarks"]);

                    }


                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }

        private void DTPPurchaseDate_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void lueItemName_DragOver(object sender, DragEventArgs e)
        {

        }

        private void grdMachineItemMaster_Click(object sender, EventArgs e)
        {

        }
        private void lueMachine_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void lueMachine_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmMfgMachineMaster frmMachine = new FrmMfgMachineMaster();
                frmMachine.ShowDialog();
                Global.LOOKUPMachine(lueMachine);
            }
        }

        private void lueItemName_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmMachineDetailEntry FrmMachineDetail = new FrmMachineDetailEntry();
                FrmMachineDetail.ShowDialog();
                DataTable dtIss = objMachineItem.GetData(0);
                if (dtIss.Rows.Count > 0)
                {
                    lueItemName.Properties.DataSource = dtIss;
                    lueItemName.Properties.ValueMember = "machine_detail_id";
                    lueItemName.Properties.DisplayMember = "item_name";
                }
                else
                {
                    lueItemName.Properties.DataSource = dtIss;
                    lueItemName.EditValue = null;
                }
            }
        }
    }
}
