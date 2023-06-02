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
    public partial class FrmMfgDepartmentShiftMappingMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.FormEvents objBOFormEvents;
        BLL.Validation Val;
        BLL.FormPer ObjPer;

        MfgDepartmentShiftMappingMaster objDeptShiftMap;

        #endregion

        #region Constructor
        public FrmMfgDepartmentShiftMappingMaster()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            Val = new BLL.Validation();
            ObjPer = new BLL.FormPer();

            objDeptShiftMap = new MfgDepartmentShiftMappingMaster();

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
            objBOFormEvents.ObjToDispose.Add(objDeptShiftMap);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion

        #region Events
        private void FrmMfgDepartmentShiftMappingMaster_Load(object sender, EventArgs e)
        {
            try
            {
                GetData();
                btnClear_Click(btnClear, null);
                Global.LOOKUPCompany(lueCompany);
                Global.LOOKUPBranch(lueBranch);
                Global.LOOKUPLocation(lueLocation);
                Global.LOOKUPDepartment(lueDepartment);
                DataTable dtShift = new DataTable();
                dtShift.Columns.Add("shift_id");
                dtShift.Columns.Add("shift_name");
                dtShift.Rows.Add("1", "General");
                dtShift.Rows.Add("2", "1st Shift");
                dtShift.Rows.Add("3", "2nd Shift");

                lueShift.Properties.DataSource = dtShift;
                lueShift.Properties.ValueMember = "shift_id";
                lueShift.Properties.DisplayMember = "shift_name";
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
                lueCompany.EditValue = null;
                lueBranch.EditValue = null;
                lueLocation.EditValue = null;
                lueDepartment.EditValue = null;
                lueShift.EditValue = null;
                txtRemark.Text = string.Empty;
                chkActive.Checked = true;
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

        #region GridEvents      
        private void dgvMfgDepartmentShiftMappingMaster_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvDepartmentShiftMappingMaster.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt64(Drow["dept_shift_map_id"]);
                        lueCompany.EditValue = Val.ToInt32(Drow["company_id"]);
                        lueBranch.EditValue = Val.ToInt32(Drow["branch_id"]);
                        lueLocation.EditValue = Val.ToInt32(Drow["location_id"]);
                        lueDepartment.EditValue = Val.ToInt32(Drow["department_id"]);
                        lueShift.EditValue = Val.ToString(Drow["shift_id"]);
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
            MfgDepartmentShiftMapping_MasterProperty DeptShiftMapMasterProperty = new MfgDepartmentShiftMapping_MasterProperty();

            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }
                DeptShiftMapMasterProperty.dept_shift_map_id = Val.ToInt32(lblMode.Tag);
                DeptShiftMapMasterProperty.company_id = Val.ToInt32(lueCompany.EditValue);
                DeptShiftMapMasterProperty.branch_id = Val.ToInt32(lueBranch.EditValue);
                DeptShiftMapMasterProperty.location_id = Val.ToInt32(lueLocation.EditValue);
                DeptShiftMapMasterProperty.department_id = Val.ToInt32(lueDepartment.EditValue);
                DeptShiftMapMasterProperty.shift_id = Val.ToInt(lueShift.EditValue);
                DeptShiftMapMasterProperty.remarks = txtRemark.Text.ToUpper();
                DeptShiftMapMasterProperty.active = Val.ToBoolean(chkActive.Checked);

                int IntRes = objDeptShiftMap.Save(DeptShiftMapMasterProperty);
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Save Department Shift Mapping Details");
                    lueCompany.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Department Shift Mapping Details Data Successfully");
                    }
                    else
                    {
                        Global.Confirm("Department Shift Mapping Details  Update Successfully");
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
                DeptShiftMapMasterProperty = null;
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

                if (lueShift.Text == "")
                {
                    lstError.Add(new ListError(13, "Shift"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueShift.Focus();
                    }
                }

                if (!objDeptShiftMap.ISExists(Val.ToInt(lueCompany.EditValue), Val.ToInt(lueBranch.EditValue), Val.ToInt(lueLocation.EditValue), Val.ToInt(lueDepartment.EditValue), Val.ToInt(lueShift.EditValue), Val.ToInt64(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
                {
                    lstError.Add(new ListError(23, "Ageing Exist"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueCompany.Focus();
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
                DataTable DTab = objDeptShiftMap.GetData();
                grdDepartmentShiftMappingMaster.DataSource = DTab;
                dgvDepartmentShiftMappingMaster.BestFitColumns();
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
                            dgvDepartmentShiftMappingMaster.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvDepartmentShiftMappingMaster.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvDepartmentShiftMappingMaster.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvDepartmentShiftMappingMaster.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvDepartmentShiftMappingMaster.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvDepartmentShiftMappingMaster.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvDepartmentShiftMappingMaster.ExportToCsv(Filepath);
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
