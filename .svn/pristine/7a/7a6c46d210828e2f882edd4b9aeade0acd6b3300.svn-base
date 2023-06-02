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
    public partial class FrmMfgMachineMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member
        BLL.FormEvents objBOFormEvents;
        BLL.Validation Val;
        BLL.FormPer ObjPer;

        MfgMachineMaster objMachine;
        #endregion

        #region Constructor
        public FrmMfgMachineMaster()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            Val = new BLL.Validation();
            ObjPer = new BLL.FormPer();

            objMachine = new MfgMachineMaster();
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
            objBOFormEvents.ObjToDispose.Add(objMachine);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion

        #region Events
        private void FrmMfgTensionTypeMaster_Load(object sender, EventArgs e)
        {
            try
            {
                GetData();
                btnClear_Click(btnClear, null);
                Global.LOOKUPCompany(lueCompany);
                Global.LOOKUPBranch(lueBranch);
                Global.LOOKUPLocation(lueLocation);
                Global.LOOKUPDepartment(lueDepartment);
                Global.LOOKUPTeam(lueTeam);
                Global.LOOKUPGroup(lueGroup);
                Global.LOOKUPProcess(lueProcess);
                Global.LOOKUPMachineType(lueMachineType);

                lueCompany.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.company_id);
                lueBranch.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.branch_id);
                lueLocation.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.location_id);
                lueDepartment.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.department_id);

                dtpAcquiredDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpAcquiredDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpAcquiredDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpAcquiredDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpAcquiredDate.EditValue = DateTime.Now;

                dtpInstallationDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpInstallationDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpInstallationDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpInstallationDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpInstallationDate.EditValue = DateTime.Now;
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
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                lblMode.Tag = 0;
                lblMode.Text = "Add Mode";
                txtMachineName.Text = string.Empty;
                txtManualCode.Text = string.Empty;
                txtModelNo.Text = string.Empty;
                txtSerialNo.Text = string.Empty;
                lueCompany.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.company_id);
                lueBranch.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.branch_id);
                lueLocation.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.location_id);
                lueDepartment.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.department_id);
                lueMachineType.EditValue = null;
                lueTeam.EditValue = null;
                lueGroup.EditValue = null;
                lueProcess.EditValue = null;
                txtVendorName.Text = string.Empty;
                txtCategory.Text = string.Empty;
                txtDepriciable.Text = string.Empty;
                txtElectricityPerHrs.Text = string.Empty;
                txtMachineActivity.Text = string.Empty;
                txtRemark.Text = string.Empty;
                txtPurchaseRate.Text = string.Empty;
                chkActive.Checked = true;
                dtpAcquiredDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpAcquiredDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpAcquiredDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpAcquiredDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpAcquiredDate.EditValue = DateTime.Now;

                dtpInstallationDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpInstallationDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpInstallationDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpInstallationDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpInstallationDate.EditValue = DateTime.Now;

                dtpPurchaseDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpPurchaseDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpPurchaseDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpPurchaseDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpPurchaseDate.EditValue = DateTime.Now;

                dtpRobertKitDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpRobertKitDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpRobertKitDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpRobertKitDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpRobertKitDate.EditValue = DateTime.Now;
                txtMachineName.Focus();
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
        private void dgvMfgTensionTypeMaster_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvMachineMaster.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt64(Drow["machine_id"]);
                        txtMachineName.Text = Val.ToString(Drow["machine_name"]);
                        txtManualCode.Text = Val.ToString(Drow["manual_code"]);
                        txtModelNo.Text = Val.ToString(Drow["model_no"]);
                        txtSerialNo.Text = Val.ToString(Drow["serial_no"]);
                        lueCompany.EditValue = Val.ToInt(Drow["company_id"]);
                        lueBranch.EditValue = Val.ToInt(Drow["branch_id"]);
                        lueLocation.EditValue = Val.ToInt(Drow["location_id"]);
                        lueDepartment.EditValue = Val.ToInt(Drow["department_id"]);
                        lueTeam.EditValue = Val.ToInt(Drow["team_id"]);
                        lueGroup.EditValue = Val.ToInt(Drow["group_id"]);
                        lueProcess.EditValue = Val.ToInt(Drow["process_id"]);
                        txtVendorName.Text = Val.ToString(Drow["vendor_name"]);
                        txtCategory.Text = Val.ToString(Drow["category_name"]);
                        lueMachineType.EditValue = Val.ToInt(Drow["machine_type_id"]);
                        dtpAcquiredDate.Text = Val.ToString(Drow["date_acquired"]);
                        dtpInstallationDate.Text = Val.ToString(Drow["installation_date"]);
                        txtPurchaseRate.Text = Val.ToString(Drow["purchase_rate"]);
                        dtpPurchaseDate.Text = Val.ToString(Drow["purchase_date"]);
                        dtpRobertKitDate.Text = Val.ToString(Drow["robert_kit_date"]);
                        txtDepriciable.Text = Val.ToString(Drow["depriciable_life"]);
                        txtElectricityPerHrs.Text = Val.ToString(Drow["electricity_per_hrs"]);
                        txtMachineActivity.Text = Val.ToString(Drow["machine_activity"]);
                        txtRemark.Text = Val.ToString(Drow["remarks"]);
                        chkActive.Checked = Val.ToBoolean(Drow["active"]);
                        txtMachineName.Focus();
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
            MfgMachine_MasterProperty MachineMasterProperty = new MfgMachine_MasterProperty();

            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }
                MachineMasterProperty.machine_id = Val.ToInt32(lblMode.Tag);
                MachineMasterProperty.machine_name = txtMachineName.Text.ToUpper();
                MachineMasterProperty.manual_code = Val.ToString(txtManualCode.Text);
                MachineMasterProperty.model_no = Val.ToString(txtModelNo.Text);
                MachineMasterProperty.serial_no = Val.ToString(txtSerialNo.Text);
                MachineMasterProperty.company_id = Val.ToInt(lueCompany.EditValue);
                MachineMasterProperty.branch_id = Val.ToInt(lueBranch.EditValue);
                MachineMasterProperty.location_id = Val.ToInt(lueLocation.EditValue);
                MachineMasterProperty.department_id = Val.ToInt(lueDepartment.EditValue);
                MachineMasterProperty.team_id = Val.ToInt(lueTeam.EditValue);
                MachineMasterProperty.group_id = Val.ToInt(lueGroup.EditValue);
                MachineMasterProperty.process_id = Val.ToInt(lueProcess.EditValue);
                MachineMasterProperty.vendor_name = Val.ToString(txtVendorName.Text);
                MachineMasterProperty.category_name = Val.ToString(txtCategory.Text);
                MachineMasterProperty.machine_type_id = Val.ToInt(lueMachineType.EditValue);
                MachineMasterProperty.date_acquired = Val.DBDate(dtpAcquiredDate.Text);
                MachineMasterProperty.installation_date = Val.DBDate(dtpInstallationDate.Text);
                MachineMasterProperty.purchase_date = Val.DBDate(dtpPurchaseDate.Text);
                MachineMasterProperty.purchase_rate = Val.ToDecimal(txtPurchaseRate.Text);
                MachineMasterProperty.robert_kit_date = Val.DBDate(dtpRobertKitDate.Text);
                MachineMasterProperty.depriciable_life = Val.ToDecimal(txtDepriciable.Text);
                MachineMasterProperty.electricity_per_hrs = Val.ToDecimal(txtElectricityPerHrs.Text);
                MachineMasterProperty.machine_activity = Val.ToString(txtMachineActivity.Text);
                MachineMasterProperty.remarks = txtRemark.Text.ToUpper();
                MachineMasterProperty.active = Val.ToBoolean(chkActive.Checked);

                int IntRes = objMachine.Save(MachineMasterProperty);
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Save Machine Details");
                    txtMachineName.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Machine Details Data Successfully");
                    }
                    else
                    {
                        Global.Confirm("Machine Type Details  Update Successfully");
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
                MachineMasterProperty = null;
            }

            return blnReturn;
        }
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (txtMachineName.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Machine Name"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtMachineName.Focus();
                    }
                }


                if (!objMachine.ISExists(txtMachineName.Text, Val.ToInt64(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
                {
                    lstError.Add(new ListError(23, "Machine Name"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtMachineName.Focus();
                    }

                }
                if (txtManualCode.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Manual Code"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtManualCode.Focus();
                    }
                }
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
                DataTable DTab = objMachine.GetData();
                grdMachineMaster.DataSource = DTab;
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
                            dgvMachineMaster.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvMachineMaster.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvMachineMaster.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvMachineMaster.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvMachineMaster.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvMachineMaster.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvMachineMaster.ExportToCsv(Filepath);
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
    }
}
