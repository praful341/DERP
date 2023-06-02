using BLL;
using BLL.FunctionClasses.Master;
using BLL.PropertyClasses.Master;
using DERP.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Master
{
    public partial class FrmConfigRoleMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        FormEvents objBOFormEvents;
        Validation Val;
        BLL.FormPer ObjPer;

        ConfigRoleMaster objConfigRole;

        #endregion

        #region Constructor
        public FrmConfigRoleMaster()
        {
            InitializeComponent();

            objBOFormEvents = new FormEvents();
            Val = new Validation();
            ObjPer = new BLL.FormPer();

            objConfigRole = new ConfigRoleMaster();
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
            objBOFormEvents.ObjToDispose.Add(objConfigRole);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }

        #endregion

        #region Events
        private void FrmConfigRoleMaster_Load(object sender, EventArgs e)
        {
            try
            {
                GetData();
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
                txtRoleName.Text = "";
                cmbRoleType.Text = "";
                chkActive.Checked = true;
                txtRoleName.Focus();
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

        #region GridEvents
        private void dgvRoleMaster_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvRoleMaster.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt32(Drow["role_id"]);
                        txtRoleName.Text = Val.ToString(Drow["role_name"]);
                        cmbRoleType.Text = Val.ToString(Drow["role_type"]);
                        chkActive.Checked = Val.ToBoolean(Drow["active"]);
                        txtRoleName.Focus();
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
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (txtRoleName.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Role Name"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtRoleName.Focus();
                    }
                }
                if (cmbRoleType.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Role Type"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        cmbRoleType.Focus();
                    }
                }

                if (!objConfigRole.ISExists(txtRoleName.Text, cmbRoleType.Text, Val.ToInt64(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
                {
                    lstError.Add(new ListError(23, "Role Name"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtRoleName.Focus();
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
            ConfigRole_MasterProperty ConfigRoleMasterProperty = new ConfigRole_MasterProperty();
            ConfigRoleMaster objConfigRole = new ConfigRoleMaster();

            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }


                ConfigRoleMasterProperty.role_id = Val.ToInt32(lblMode.Tag);
                ConfigRoleMasterProperty.role_name = txtRoleName.Text.ToUpper();
                ConfigRoleMasterProperty.active = Val.ToBoolean(chkActive.Checked);
                ConfigRoleMasterProperty.role_type = Val.ToString(cmbRoleType.Text);

                int IntRes = objConfigRole.Save_Role(ConfigRoleMasterProperty);
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Save Role Details");
                    txtRoleName.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Role Details Data Save Successfully");
                    }
                    else
                    {
                        Global.Confirm("Role Details Data Update Successfully");
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
                ConfigRoleMasterProperty = null;
            }

            return blnReturn;
        }
        public void GetData()
        {
            try
            {
                DataTable DTab = objConfigRole.GetData();
                grdRoleMaster.DataSource = DTab;
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
                            dgvRoleMaster.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvRoleMaster.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvRoleMaster.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvRoleMaster.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvRoleMaster.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvRoleMaster.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvRoleMaster.ExportToCsv(Filepath);
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
