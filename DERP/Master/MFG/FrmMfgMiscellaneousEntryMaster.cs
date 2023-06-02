using BLL;
using BLL.FunctionClasses.Master.MFG;
using BLL.PropertyClasses.Master.MFG;
using DERP.Class;
using DERP.Master.Store;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Master.MFG
{
    public partial class FrmMfgMiscellaneousEntryMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.FormEvents objBOFormEvents;
        BLL.Validation Val;
        BLL.FormPer ObjPer;

        MfgMiscellaneousEntryMaster objMiscellaneous;
        #endregion

        #region Constructor 
        public FrmMfgMiscellaneousEntryMaster()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            Val = new BLL.Validation();
            ObjPer = new BLL.FormPer();

            objMiscellaneous = new MfgMiscellaneousEntryMaster();
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
            objBOFormEvents.ObjToDispose.Add(objMiscellaneous);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion

        #region Events
        private void FrmMfgMachineCompanyMaster_Load(object sender, EventArgs e)
        {
            try
            {

                //GetData();
                Global.LOOKUPBranch_New(lueBranch);
                Global.LOOKUPBillManager(lueManager);


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
                txtPartyName.Text = string.Empty;
                //DTPPurchaseDate.Text = GetDate;
                txtMobile.Text = string.Empty;
                txtItemName.Text = string.Empty;
                txtQty.Text = string.Empty;
                txtRate.Text = string.Empty;
                txtAmount.Text = string.Empty;
                txtDepartment.Text = string.Empty;
                txtRemark.Text = string.Empty;
                lueBranch.EditValue = null;
                lueManager.EditValue = null;
                txtPartyName.Focus();
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

                DTPPurchaseDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                DTPPurchaseDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                DTPPurchaseDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                DTPPurchaseDate.Properties.CharacterCasing = CharacterCasing.Upper;
                DTPPurchaseDate.EditValue = DateTime.Now;
                GetData();
                txtPartyName.Focus();


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
        private void dgvMfgItemCompanyMaster_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvItemCompanyMaster.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt64(Drow["miscellaneous_id"]);
                        txtPartyName.Text = Val.ToString(Drow["party_name"]);
                        DTPPurchaseDate.Text = Val.ToString(Drow["purchase_date"]);
                        lueBranch.Text = Val.ToString(Drow["branch_name"]);
                        lueBranch.EditValue = Val.ToInt(Drow["branch_id"]);
                        lueManager.Text = Val.ToString(Drow["manager_name"]);
                        lueManager.EditValue = Val.ToInt(Drow["manager_id"]);
                        txtMobile.Text = Val.ToString(Drow["mobile_no"]);
                        txtItemName.Text = Val.ToString(Drow["item_name"]);
                        txtQty.Text = Val.ToString(Drow["qty"]);
                        txtRate.Text = Val.ToString(Drow["rate"]);
                        txtDepartment.Text = Val.ToString(Drow["department"]);
                        txtAmount.Text = Val.ToString(Drow["amount"]);
                        txtRemark.Text = Val.ToString(Drow["remark"]);
                        txtPartyName.Focus();
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
            MfgMiscellaneousEntryMasterProperty Miscellaneous = new MfgMiscellaneousEntryMasterProperty();

            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }
                Miscellaneous.miscellaneous_id = Val.ToInt32(lblMode.Tag);
                Miscellaneous.party_name = txtPartyName.Text;
                Miscellaneous.purchase_date = DTPPurchaseDate.DateTime;
                Miscellaneous.mobile_number = Val.ToInt64(txtMobile.Text);
                Miscellaneous.item_name = txtItemName.Text;
                Miscellaneous.qty = Val.ToInt(txtQty.Text);
                Miscellaneous.branch_id = Val.ToInt(lueBranch.EditValue);
                Miscellaneous.manager_id = Val.ToInt(lueManager.EditValue);
                Miscellaneous.rate = Val.ToDecimal(txtRate.Text);
                Miscellaneous.department = Val.ToString(txtDepartment.Text);
                Miscellaneous.amount = Val.ToDecimal(txtAmount.Text);
                Miscellaneous.remark = txtRemark.Text;

                int IntRes = objMiscellaneous.Save(Miscellaneous);
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Saving Miscellaneous Bill Entry");
                    txtPartyName.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Miscellaneous Bill Entry Saved Successfully");
                    }
                    else
                    {
                        Global.Confirm("Miscellaneous Bill Entry Updated Successfully");
                    }
                }

            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                blnReturn = false;
            }
            //finally
            //{
            //    MachineCompanyMasterProperty = null;
            //}

            return blnReturn;
        }
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (DTPPurchaseDate.Text == null)
                {
                    lstError.Add(new ListError(12, "Purchase Date"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        DTPPurchaseDate.Focus();
                    }
                }

                if (txtItemName.Text == null)
                {
                    lstError.Add(new ListError(12, "Item Name"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtItemName.Focus();
                    }
                }

                if (txtQty.Text == null)
                {
                    lstError.Add(new ListError(12, "Qty"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtQty.Focus();
                    }
                }

                if (txtRate.Text == null)
                {
                    lstError.Add(new ListError(12, "Rate"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtRate.Focus();
                    }
                }
                //if (!objMachineCompany.ISExists(txtPartyName.Text, Val.ToInt64(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
                //{
                //    lstError.Add(new ListError(23, "Company"));
                //    if (!blnFocus)
                //    {
                //        blnFocus = true;
                //        txtPartyName.Focus();
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
                DataTable DTab = objMiscellaneous.GetData(Val.DBDate(dtpFromDate.Text), Val.DBDate(dtpToDate.Text));
                grdItemCompanyMaster.DataSource = DTab;
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
                            dgvItemCompanyMaster.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvItemCompanyMaster.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvItemCompanyMaster.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvItemCompanyMaster.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvItemCompanyMaster.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvItemCompanyMaster.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvItemCompanyMaster.ExportToCsv(Filepath);
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

        private void DTPPurchaseDate_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtQty_EditValueChanged(object sender, EventArgs e)
        {
            if ((txtQty.Text == "" || txtQty.Text == string.Empty) && (txtRate.Text == "" || txtRate.Text == string.Empty))
            {
                txtAmount.Text = string.Empty;
            }
            else
            {
                txtAmount.Text = Val.ToString(Val.ToDecimal(txtQty.Text) * Val.ToDecimal(txtRate.Text));
            }
        }

        private void txtRate_EditValueChanged(object sender, EventArgs e)
        {
            if ((txtQty.Text == "" || txtQty.Text == string.Empty) && (txtRate.Text == "" || txtRate.Text == string.Empty))
            {
                txtAmount.Text = string.Empty;
            }
            else
            {
                txtAmount.Text = Val.ToString(Val.ToDecimal(txtQty.Text) * Val.ToDecimal(txtRate.Text));
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable DTab = objMiscellaneous.GetData(Val.DBDate(dtpFromDate.Text), Val.DBDate(dtpToDate.Text));
                grdItemCompanyMaster.DataSource = DTab;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }

        }

        private void panelControl5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lueManager_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (e.Button.Index == 1)
                {
                    FrmMstBillManagerMaster billManager = new FrmMstBillManagerMaster();
                    billManager.ShowDialog();
                    Global.LOOKUPBillManager(lueManager);
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
    }
}
