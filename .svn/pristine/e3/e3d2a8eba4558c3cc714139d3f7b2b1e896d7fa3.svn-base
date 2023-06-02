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
    public partial class FrmMfgProcessMappingMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.FormEvents objBOFormEvents;
        BLL.Validation Val;
        BLL.FormPer ObjPer;

        MFGProcessMapping objProcessMapping;

        DataTable m_dtbSubProcess;
        #endregion

        #region Constructor
        public FrmMfgProcessMappingMaster()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            Val = new BLL.Validation();
            ObjPer = new BLL.FormPer();

            objProcessMapping = new MFGProcessMapping();

            m_dtbSubProcess = new DataTable();
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
            objBOFormEvents.ObjToDispose.Add(objProcessMapping);
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
                Global.LOOKUPCompany_New(lueCompany);
                Global.LOOKUPBranch_New(lueBranch);
                Global.LOOKUPLocation_New(lueLocation);
                Global.LOOKUPDepartment_New(lueDepartment);
                Global.LOOKUPProcessAll(lueProcess);
                Global.LOOKUPSubProcess(lueSubProcess);
                m_dtbSubProcess = (((DataTable)lueSubProcess.Properties.DataSource).Copy());
                lueCompany.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.company_id);
                lueBranch.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.branch_id);
                lueLocation.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.location_id);
                lueDepartment.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.department_id);
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
                lueCompany.EditValue = null;
                lueBranch.EditValue = null;
                lueLocation.EditValue = null;
                lueDepartment.EditValue = null;
                lueProcess.EditValue = null;
                lueSubProcess.EditValue = null;
                chkIsDefault.Checked = false;
                chkActive.Checked = true;
                lueCompany.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.company_id);
                lueBranch.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.branch_id);
                lueLocation.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.location_id);
                lueDepartment.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.department_id);
                lueCompany.Focus();
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
        private void lueProcess_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (lueProcess.EditValue != System.DBNull.Value)
                {
                    if (m_dtbSubProcess.Rows.Count > 0)
                    {
                        DataTable dtbdetail = m_dtbSubProcess;

                        string strFilter = string.Empty;

                        if (lueProcess.Text != "")
                            strFilter = "process_id = " + lueProcess.EditValue;


                        dtbdetail.DefaultView.RowFilter = strFilter;
                        dtbdetail.DefaultView.ToTable();

                        DataTable dtb = dtbdetail.DefaultView.ToTable();

                        lueSubProcess.Properties.DataSource = dtb;
                        lueSubProcess.Properties.ValueMember = "sub_process_id";
                        lueSubProcess.Properties.DisplayMember = "sub_process_name";

                        lueSubProcess.EditValue = null;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
                return;
            }
        }

        #region GridEvents
        private void dgvMfgProcessMapping_Master_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvProcessMappingMaster.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt64(Drow["process_mapping_id"]);
                        lueCompany.EditValue = Val.ToInt(Drow["company_id"]);
                        lueBranch.EditValue = Val.ToInt(Drow["branch_id"]);
                        lueLocation.EditValue = Val.ToInt(Drow["location_id"]);
                        lueDepartment.EditValue = Val.ToInt(Drow["department_id"]);
                        lueProcess.EditValue = Val.ToInt(Drow["process_id"]);
                        lueSubProcess.EditValue = Val.ToInt(Drow["sub_process_id"]);
                        chkIsDefault.Checked = Val.ToBoolean(Drow["is_default"]);
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
                        lueDepartment.Focus();
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


                if (!objProcessMapping.ISExists(Val.ToInt(lueCompany.EditValue), Val.ToInt(lueBranch.EditValue), Val.ToInt(lueLocation.EditValue), Val.ToInt(lueDepartment.EditValue), Val.ToInt(lueProcess.EditValue), Val.ToInt(lueSubProcess.EditValue), Val.ToInt64(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
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
                DeptProcessMappMasterProperty.process_mapping_id = Val.ToInt32(lblMode.Tag);
                DeptProcessMappMasterProperty.company_id = Val.ToInt(lueCompany.EditValue);
                DeptProcessMappMasterProperty.branch_id = Val.ToInt(lueBranch.EditValue);
                DeptProcessMappMasterProperty.location_id = Val.ToInt(lueLocation.EditValue);
                DeptProcessMappMasterProperty.department_id = Val.ToInt(lueDepartment.EditValue);
                DeptProcessMappMasterProperty.process_id = Val.ToInt(lueProcess.EditValue);
                DeptProcessMappMasterProperty.sub_process_id = Val.ToInt(lueSubProcess.EditValue);
                DeptProcessMappMasterProperty.is_default = Val.ToBoolean(chkIsDefault.Checked);
                //DeptProcessMappMasterProperty.active = Val.ToBoolean(chkActive.Checked);

                int IntRes = objProcessMapping.Save(DeptProcessMappMasterProperty);
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Save Process Mapping Details");
                    lueDepartment.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Process Mapping Details Data Successfully");
                    }
                    else
                    {
                        Global.Confirm("Process Mapping Details  Update Successfully");
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
                DataTable DTab = objProcessMapping.GetData();
                grdProcessMappingMaster.DataSource = DTab;
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
                            dgvProcessMappingMaster.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvProcessMappingMaster.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvProcessMappingMaster.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvProcessMappingMaster.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvProcessMappingMaster.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvProcessMappingMaster.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvProcessMappingMaster.ExportToCsv(Filepath);
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
