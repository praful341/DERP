﻿using BLL;
using BLL.FunctionClasses.Master.Store;
using BLL.PropertyClasses.Master.Store;
using DERP.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Master.Store
{
    public partial class FrmStoreDepartmentMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        FormEvents objBOFormEvents;
        Validation Val;
        BLL.FormPer ObjPer;

        StoreDepartmentMaster objStoreDepartment;

        DataTable m_department_type;

        #endregion

        #region Constructor
        public FrmStoreDepartmentMaster()
        {
            InitializeComponent();

            objBOFormEvents = new FormEvents();
            Val = new Validation();
            ObjPer = new BLL.FormPer();

            objStoreDepartment = new StoreDepartmentMaster();

            m_department_type = new DataTable();
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
            objBOFormEvents.ObjToDispose.Add(objStoreDepartment);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }

        #endregion

        #region Events
        private void FrmStoreDepartmentMaster_Load(object sender, EventArgs e)
        {
            try
            {
                GetData();
                btnClear_Click(btnClear, null);
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void btnSave_Click(object sender, System.EventArgs e)
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
        private void btnClear_Click(object sender, System.EventArgs e)
        {
            try
            {
                lblMode.Tag = 0;
                lblMode.Text = "Add Mode";
                txtDeptName.Text = "";
                txtRemark.Text = "";
                txtSequenceNo.Text = "";
                chkActive.Checked = true;
                txtDeptName.Focus();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void btnExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        #region GridEvents      
        private void dgvStoreDepartmentMaster_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvStoreDepartmentMaster.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt32(Drow["department_id"]);
                        txtDeptName.Text = Val.ToString(Drow["department_name"]);
                        txtRemark.Text = Val.ToString(Drow["remarks"]);
                        txtSequenceNo.Text = Val.ToString(Drow["sequence_no"]);
                        chkActive.Checked = Val.ToBoolean(Drow["active"]);
                        txtDeptName.Focus();
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

        #region Functions
        private bool SaveDetails()
        {
            bool blnReturn = true;
            StoreDepartment_MasterProperty StoreDepartment_MasterProperty = new StoreDepartment_MasterProperty();
            StoreDepartmentMaster objStoreDepartment = new StoreDepartmentMaster();
            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }

                StoreDepartment_MasterProperty.Department_Id = Val.ToInt64(lblMode.Tag);
                StoreDepartment_MasterProperty.Department_Name = txtDeptName.Text.ToUpper();
                StoreDepartment_MasterProperty.Active = Val.ToBooleanToInt(chkActive.Checked);
                StoreDepartment_MasterProperty.Remark = txtRemark.Text.ToUpper();
                StoreDepartment_MasterProperty.Sequence_No = Val.ToInt(txtSequenceNo.Text);

                int IntRes = objStoreDepartment.Save(StoreDepartment_MasterProperty);
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Save Store Department Data");
                    txtDeptName.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Store Department Data Save Successfully");
                    }
                    else
                    {
                        Global.Confirm("Store Department Data Update Successfully");
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
                StoreDepartment_MasterProperty = null;
            }

            return blnReturn;
        }
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (txtDeptName.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Department Name"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtDeptName.Focus();
                    }
                }
                if (!objStoreDepartment.ISExists(txtDeptName.Text, Val.ToInt64(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
                {
                    lstError.Add(new ListError(23, "Department Name"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtDeptName.Focus();
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
                DataTable DTab = objStoreDepartment.GetData();
                grdStoreDepartmentMaster.DataSource = DTab;
                dgvStoreDepartmentMaster.BestFitColumns();
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
                            dgvStoreDepartmentMaster.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvStoreDepartmentMaster.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvStoreDepartmentMaster.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvStoreDepartmentMaster.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvStoreDepartmentMaster.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvStoreDepartmentMaster.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvStoreDepartmentMaster.ExportToCsv(Filepath);
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
