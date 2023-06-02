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
    public partial class FrmMfgWeightValidationMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        FormEvents objBOFormEvents;
        Validation Val;
        BLL.FormPer ObjPer;

        MfgWeightValidationMaster objWtVal;
        #endregion

        #region Constructor
        public FrmMfgWeightValidationMaster()
        {
            InitializeComponent();

            objBOFormEvents = new FormEvents();
            Val = new Validation();
            ObjPer = new BLL.FormPer();

            objWtVal = new MfgWeightValidationMaster();
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
            objBOFormEvents.ObjToDispose.Add(objWtVal);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion

        #region Events
        private void FrmMfgWeightValidationMaster_Load(object sender, EventArgs e)
        {
            try
            {
                GetData();
                btnClear_Click(btnClear, null);
                Global.LOOKUPCompany(lueCompany);
                Global.LOOKUPBranch(lueBranch);
                Global.LOOKUPLocation(lueLocation);
                Global.LOOKUPDepartment(lueDepartment);
                Global.LOOKUPProcess(lueProcess);
                DataTable dtValType = new DataTable();
                dtValType.Columns.Add("validation_type");
                dtValType.Rows.Add("VALUE");
                dtValType.Rows.Add("PERCENT");

                lueValidationType.Properties.DataSource = dtValType;
                lueValidationType.Properties.ValueMember = "validation_type";
                lueValidationType.Properties.DisplayMember = "validation_type";
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
                txtFromValue.Text = "0";
                lueCompany.EditValue = null;
                lueBranch.EditValue = null;
                lueLocation.EditValue = null;
                lueDepartment.EditValue = null;
                lueValidationType.EditValue = null;
                lueProcess.EditValue = null;
                txtToValue.Text = "0";
                txtRemark.Text = string.Empty;
                chkActive.Checked = true;
                txtFromValue.Focus();
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
        private void dgvMfgWeightValidationMaster_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvWtValidationMaster.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt64(Drow["weight_id"]);
                        lueCompany.EditValue = Val.ToInt32(Drow["company_id"]);
                        lueBranch.EditValue = Val.ToInt32(Drow["branch_id"]);
                        lueLocation.EditValue = Val.ToInt32(Drow["location_id"]);
                        lueDepartment.EditValue = Val.ToInt32(Drow["department_id"]);
                        lueValidationType.EditValue = Val.ToString(Drow["validation_type"]);
                        lueProcess.EditValue = Val.ToInt32(Drow["process_id"]);
                        txtFromValue.Text = Val.ToString(Drow["value_from"]);
                        txtToValue.Text = Val.ToString(Drow["value_to"]);
                        txtRemark.Text = Val.ToString(Drow["remarks"]);
                        chkActive.Checked = Val.ToBoolean(Drow["active"]);
                        lueCompany.Focus();
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
            MfgWeightValidation_MasterProperty WtValMasterProperty = new MfgWeightValidation_MasterProperty();

            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }
                WtValMasterProperty.weight_id = Val.ToInt32(lblMode.Tag);
                WtValMasterProperty.company_id = Val.ToInt32(lueCompany.EditValue);
                WtValMasterProperty.branch_id = Val.ToInt32(lueBranch.EditValue);
                WtValMasterProperty.location_id = Val.ToInt32(lueLocation.EditValue);
                WtValMasterProperty.department_id = Val.ToInt32(lueDepartment.EditValue);
                WtValMasterProperty.validation_type = Val.ToString(lueValidationType.EditValue);
                WtValMasterProperty.process_id = Val.ToInt32(lueProcess.EditValue);
                WtValMasterProperty.value_from = Val.ToDecimal(txtFromValue.Text);
                WtValMasterProperty.value_to = Val.ToDecimal(txtToValue.Text);
                WtValMasterProperty.remarks = txtRemark.Text.ToUpper();
                WtValMasterProperty.active = Val.ToBoolean(chkActive.Checked);

                int IntRes = objWtVal.Save(WtValMasterProperty);
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Save Weight Validation Details");
                    txtFromValue.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Weight Validation Details Data Successfully");
                    }
                    else
                    {
                        Global.Confirm("Weight Validation Details  Update Successfully");
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
                WtValMasterProperty = null;
            }

            return blnReturn;
        }
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (lueCompany.Text == "")
                {
                    lstError.Add(new ListError(13, "Company"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueCompany.Focus();
                    }
                }
                if (lueBranch.Text == "")
                {
                    lstError.Add(new ListError(13, "Branch"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueBranch.Focus();
                    }
                }
                if (lueLocation.Text == "")
                {
                    lstError.Add(new ListError(13, "Location"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueLocation.Focus();
                    }
                }
                if (lueDepartment.Text == "")
                {
                    lstError.Add(new ListError(13, "Department"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueDepartment.Focus();
                    }

                }
                if (lueProcess.Text == "")
                {
                    lstError.Add(new ListError(13, "Process"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueProcess.Focus();
                    }
                }
                if (lueValidationType.Text == "")
                {
                    lstError.Add(new ListError(13, "Validation Type"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueValidationType.Focus();
                    }
                }

                if (string.IsNullOrEmpty(txtFromValue.Text))
                {
                    Global.Message("Value From Is Required");
                    txtFromValue.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtToValue.Text))
                {
                    lstError.Add(new ListError(12, "Value To"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtToValue.Focus();
                    }
                }
                if (Val.ToDecimal(txtFromValue.Text) > Val.ToDecimal(txtToValue.Text))
                {
                    lstError.Add(new ListError(5, "Value From Can't be Greater then Value To"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtToValue.Focus();
                    }

                }
                if (!objWtVal.ISExists(Val.ToInt(lueCompany.EditValue), Val.ToInt(lueBranch.EditValue), Val.ToInt(lueLocation.EditValue), Val.ToInt(lueDepartment.EditValue), Val.ToInt(lueProcess.EditValue), Val.ToInt64(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
                {
                    lstError.Add(new ListError(23, "Value Exist"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtFromValue.Focus();
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
                DataTable DTab = objWtVal.GetData();
                grdWtValidationMaster.DataSource = DTab;
                dgvWtValidationMaster.BestFitColumns();
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
                            dgvWtValidationMaster.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvWtValidationMaster.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvWtValidationMaster.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvWtValidationMaster.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvWtValidationMaster.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvWtValidationMaster.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvWtValidationMaster.ExportToCsv(Filepath);
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
