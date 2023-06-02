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
    public partial class FrmMfgDepartmentWiseSalary : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        FormEvents objBOFormEvents;
        Validation Val;
        BLL.FormPer ObjPer;

        MfgDepartmentWiseSalary objDepartmentWiseSalary;
        #endregion

        #region Constructor
        public FrmMfgDepartmentWiseSalary()
        {
            InitializeComponent();

            objBOFormEvents = new FormEvents();
            Val = new Validation();
            ObjPer = new BLL.FormPer();

            objDepartmentWiseSalary = new MfgDepartmentWiseSalary();
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
            objBOFormEvents.ObjToDispose.Add(objDepartmentWiseSalary);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion

        #region Events     
        private void FrmMfgDepartmentWiseSalary_Load(object sender, EventArgs e)
        {
            try
            {
                Global.LOOKUPDepartment_New(lueDepartment);
                Global.LOOKUPProcess(lueProcess);
                GetData();

                dtpSalaryDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpSalaryDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpSalaryDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpSalaryDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpSalaryDate.EditValue = DateTime.Now;

                btnClear_Click(btnClear, null);
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
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
                txtRemark.Text = "";
                txtTotalSalary.Text = "0";
                lueDepartment.EditValue = null;
                lueProcess.EditValue = null;
                lueDepartment.Focus();
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lueDepartment_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (e.Button.Index == 1)
                {
                    FrmDepartmentMaster frmDepartment = new FrmDepartmentMaster();
                    frmDepartment.ShowDialog();
                    Global.LOOKUPDepartment(lueDepartment);
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }

        #region GridEvents
        private void dgvDepartmentWiseSalary_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvDepartmentWiseSalary.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt32(Drow["salary_id"]);
                        dtpSalaryDate.Text = Val.DBDate(Drow["salary_date"].ToString());
                        lueDepartment.EditValue = Val.ToInt32(Drow["department_id"]);
                        lueProcess.EditValue = Val.ToInt32(Drow["process_id"]);
                        txtTotalSalary.Text = Val.ToString(Drow["total_salary"]);
                        txtRemark.Text = Val.ToString(Drow["remarks"]);
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
        private bool SaveDetails()
        {
            bool blnReturn = true;
            MfgDepartmentWiseSalaryProperty MfgDepartmentWiseSalaryProperty = new MfgDepartmentWiseSalaryProperty();
            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }
                List<ListError> lstError = new List<ListError>();


                MfgDepartmentWiseSalaryProperty.salary_id = Val.ToInt32(lblMode.Tag);
                MfgDepartmentWiseSalaryProperty.salary_date = Val.DBDate(dtpSalaryDate.Text);
                MfgDepartmentWiseSalaryProperty.department_id = Val.ToInt64(lueDepartment.EditValue);
                MfgDepartmentWiseSalaryProperty.process_id = Val.ToInt64(lueProcess.EditValue);
                MfgDepartmentWiseSalaryProperty.remarks = txtRemark.Text.ToUpper();
                MfgDepartmentWiseSalaryProperty.total_salary = Val.ToDecimal(txtTotalSalary.Text);

                int IntRes = objDepartmentWiseSalary.Save(MfgDepartmentWiseSalaryProperty);
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Save DepartmentWise Salary Details");
                    lueDepartment.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("DepartmentWise Salary Save Successfully");
                    }
                    else
                    {
                        Global.Confirm("DepartmentWise Salary Update Successfully");
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
                MfgDepartmentWiseSalaryProperty = null;
            }

            return blnReturn;
        }
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (lueDepartment.Text.Length == 0)
                {
                    lstError.Add(new ListError(12, "Department"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueDepartment.Focus();
                    }
                }
                if (lueProcess.Text.Length == 0)
                {
                    lstError.Add(new ListError(12, "Process"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueProcess.Focus();
                    }
                }
                //if (!objQuality.ISExists(txtQuality.Text, Val.ToInt64(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
                //{
                //    lstError.Add(new ListError(23, "Quality"));
                //    if (!blnFocus)
                //    {
                //        blnFocus = true;
                //        txtQuality.Focus();
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
                DataTable DTab = objDepartmentWiseSalary.GetData();
                grdDepartmentWiseSalary.DataSource = DTab;
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
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
                            dgvDepartmentWiseSalary.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvDepartmentWiseSalary.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvDepartmentWiseSalary.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvDepartmentWiseSalary.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvDepartmentWiseSalary.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvDepartmentWiseSalary.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvDepartmentWiseSalary.ExportToCsv(Filepath);
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
