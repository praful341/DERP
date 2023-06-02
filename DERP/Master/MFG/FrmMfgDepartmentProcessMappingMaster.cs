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
    public partial class FrmMfgDepartmentProcessMappingMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.FormEvents objBOFormEvents;
        BLL.Validation Val;
        BLL.FormPer ObjPer;

        MfgDepartmentProcessMappingMaster objDeptProcessMapping;
        #endregion

        #region Constructor
        public FrmMfgDepartmentProcessMappingMaster()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            Val = new BLL.Validation();
            ObjPer = new BLL.FormPer();

            objDeptProcessMapping = new MfgDepartmentProcessMappingMaster();
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
            objBOFormEvents.ObjToDispose.Add(objDeptProcessMapping);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion

        #region Events
        private void FrmMfgDepartmentProcessMappingMaster_Load(object sender, EventArgs e)
        {
            try
            {
                GetData();
                btnClear_Click(btnClear, null);
                Global.LOOKUPDepartment(lueDepartment);
                Global.LOOKUPProcess(lueProcess);
                Global.LOOKUPSubProcess(lueSubProcess);
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
                lueDepartment.EditValue = null;
                lueProcess.EditValue = null;
                lueSubProcess.EditValue = null;
                txtSequenceNo.Text = string.Empty;
                txtRemark.Text = string.Empty;
                chkActive.Checked = true;
                lueDepartment.Focus();
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
        private void dgvMfgDepartmentProcessMapping_Master_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvDeptProcessMappingMaster.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt64(Drow["dept_process_mapping_id"]);
                        lueDepartment.EditValue = Val.ToInt(Drow["department_id"]);
                        lueProcess.EditValue = Val.ToInt(Drow["process_id"]);
                        lueSubProcess.EditValue = Val.ToInt(Drow["sub_process_id"]);
                        txtSequenceNo.Text = Val.ToString(Drow["sequence_no"]);
                        txtRemark.Text = Val.ToString(Drow["remarks"]);
                        chkActive.Checked = Val.ToBoolean(Drow["active"]);
                        lueDepartment.Focus();
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
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (lueDepartment.Text == "")
                {
                    lstError.Add(new ListError(13, "Department"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueDepartment.Focus();
                    }
                }


                if (!objDeptProcessMapping.ISExists(Val.ToInt(lueDepartment.EditValue), Val.ToInt(lueProcess.EditValue), Val.ToInt(lueSubProcess.EditValue), Val.ToInt64(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
                {
                    lstError.Add(new ListError(23, "Already Exist"));
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
                        lueDepartment.Focus();
                    }
                }
                if (lueSubProcess.Text == "")
                {
                    lstError.Add(new ListError(13, "Sub Process"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueSubProcess.Focus();
                    }
                }
                if (txtSequenceNo.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Sequence No"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtSequenceNo.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                lstError.Add(new ListError(ex));
            }
            return (!(BLL.General.ShowErrors(lstError)));

        }
        private bool SaveDetails()
        {
            bool blnReturn = true;
            MfgDepartmentProcessMapping_MasterProperty DeptProcessMappMasterProperty = new MfgDepartmentProcessMapping_MasterProperty();

            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }
                DeptProcessMappMasterProperty.dept_process_mapping_id = Val.ToInt32(lblMode.Tag);
                DeptProcessMappMasterProperty.department_id = Val.ToInt(lueDepartment.EditValue);
                DeptProcessMappMasterProperty.process_id = Val.ToInt(lueProcess.EditValue);
                DeptProcessMappMasterProperty.sub_process_id = Val.ToInt(lueSubProcess.EditValue);
                DeptProcessMappMasterProperty.sequence_no = Val.ToInt(txtSequenceNo.Text);
                DeptProcessMappMasterProperty.remarks = txtRemark.Text.ToUpper();
                DeptProcessMappMasterProperty.active = Val.ToBoolean(chkActive.Checked);

                int IntRes = objDeptProcessMapping.Save(DeptProcessMappMasterProperty);
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Save Department Process Mapping Details");
                    lueDepartment.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Department Process Mapping Details Data Successfully");
                    }
                    else
                    {
                        Global.Confirm("Department Process Mapping Details  Update Successfully");
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
                DeptProcessMappMasterProperty = null;
            }

            return blnReturn;
        }
        public void GetData()
        {
            try
            {
                DataTable DTab = objDeptProcessMapping.GetData();
                grdDeptProcessMappingMaster.DataSource = DTab;
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
                            dgvDeptProcessMappingMaster.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvDeptProcessMappingMaster.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvDeptProcessMappingMaster.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvDeptProcessMappingMaster.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvDeptProcessMappingMaster.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvDeptProcessMappingMaster.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvDeptProcessMappingMaster.ExportToCsv(Filepath);
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
