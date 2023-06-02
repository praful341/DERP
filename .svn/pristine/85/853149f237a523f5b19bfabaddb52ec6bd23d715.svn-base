using BLL;
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
    public partial class FrmStoreDivisionMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        FormEvents objBOFormEvents;
        Validation Val;
        BLL.FormPer ObjPer;

        StoreDivisionMaster objStoreDivision;

        DataTable m_department_type;

        #endregion

        #region Constructor
        public FrmStoreDivisionMaster()
        {
            InitializeComponent();

            objBOFormEvents = new FormEvents();
            Val = new Validation();
            ObjPer = new BLL.FormPer();

            objStoreDivision = new StoreDivisionMaster();

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
            objBOFormEvents.ObjToDispose.Add(objStoreDivision);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }

        #endregion

        #region Events
        private void FrmStoreDivisionMaster_Load(object sender, EventArgs e)
        {
            try
            {
                Global.LOOKUPBranch_New(lueBranch);
                Global.LOOKUPStoreDepartment(LueStoreDepartment);
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
                txtDivisionName.Text = "";
                //lueBranch.EditValue = null;
                //LueStoreDepartment.EditValue = null;
                txtRemark.Text = "";
                txtSequenceNo.Text = "";
                chkActive.Checked = true;
                txtDivisionName.Focus();
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
        private void lueBranch_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //try
            //{
            //    if (e.Button.Index == 1)
            //    {
            //        FrmBranchMaster frmBranch = new FrmBranchMaster();
            //        frmBranch.ShowDialog();
            //        Global.LOOKUPBranch_New(lueBranch);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    General.ShowErrors(ex.ToString());
            //    return;
            //}
        }
        private void LueStoreDepartment_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (e.Button.Index == 1)
                {
                    FrmStoreDepartmentMaster frmStoreDepartment = new FrmStoreDepartmentMaster();
                    frmStoreDepartment.ShowDialog();
                    Global.LOOKUPStoreDepartment(LueStoreDepartment);
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }

        #region GridEvents     

        private void dgvStoreDivisionMaster_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvStoreDivisionMaster.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt64(Drow["division_id"]);
                        txtDivisionName.Text = Val.ToString(Drow["division_name"]);
                        lueBranch.EditValue = Val.ToInt32(Drow["branch_id"]);
                        LueStoreDepartment.EditValue = Val.ToInt64(Drow["department_id"]);
                        txtRemark.Text = Val.ToString(Drow["remarks"]);
                        txtSequenceNo.Text = Val.ToString(Drow["sequence_no"]);
                        chkActive.Checked = Val.ToBoolean(Drow["active"]);
                        txtDivisionName.Focus();
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
            StoreDivision_MasterProperty StoreDivision_MasterProperty = new StoreDivision_MasterProperty();
            StoreDivisionMaster objStoreDivision = new StoreDivisionMaster();

            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }

                StoreDivision_MasterProperty.division_Id = Val.ToInt32(lblMode.Tag);
                StoreDivision_MasterProperty.division_Name = txtDivisionName.Text.ToUpper();
                StoreDivision_MasterProperty.Active = Val.ToBooleanToInt(chkActive.Checked);
                StoreDivision_MasterProperty.Remark = txtRemark.Text.ToUpper();
                StoreDivision_MasterProperty.Sequence_No = Val.ToInt(txtSequenceNo.Text);
                StoreDivision_MasterProperty.branch_Id = Val.ToInt64(lueBranch.EditValue);
                StoreDivision_MasterProperty.department_Id = Val.ToInt64(LueStoreDepartment.EditValue);

                int IntRes = objStoreDivision.Save(StoreDivision_MasterProperty);
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Save Store Division Data");
                    txtDivisionName.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Store Division Data Save Successfully");
                    }
                    else
                    {
                        Global.Confirm("Store Division Data Update Successfully");
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
                StoreDivision_MasterProperty = null;
            }

            return blnReturn;
        }
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (LueStoreDepartment.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Department Name"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        LueStoreDepartment.Focus();
                    }
                }
                if (txtDivisionName.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Division Name"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtDivisionName.Focus();
                    }
                }
                if (!objStoreDivision.ISExists(txtDivisionName.Text, Val.ToInt64(lueBranch.EditValue), Val.ToInt64(LueStoreDepartment.EditValue), Val.ToInt64(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
                {
                    lstError.Add(new ListError(23, "Division Name"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtDivisionName.Focus();
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
                DataTable DTab = objStoreDivision.GetData();
                grdStoreDivisionMaster.DataSource = DTab;
                dgvStoreDivisionMaster.BestFitColumns();
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
                            dgvStoreDivisionMaster.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvStoreDivisionMaster.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvStoreDivisionMaster.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvStoreDivisionMaster.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvStoreDivisionMaster.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvStoreDivisionMaster.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvStoreDivisionMaster.ExportToCsv(Filepath);
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
