using BLL;
using BLL.FunctionClasses.Master;
using BLL.PropertyClasses.Master;
using DERP.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DREP.Master
{
    public partial class FrmGalaxyUserMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.FormEvents objBOFormEvents;
        BLL.Validation Val;
        BLL.FormPer ObjPer;
        FillCombo ObjFillCombo = new FillCombo();

        UserMaster objUser;

        #endregion

        #region Constructor

        public FrmGalaxyUserMaster()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            Val = new BLL.Validation();
            ObjPer = new BLL.FormPer();

            objUser = new UserMaster();
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
            objBOFormEvents.ObjToDispose.Add(objUser);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }

        #endregion

        #region Events
        private void FrmUserMaster_Load(object sender, EventArgs e)
        {
            try
            {
                GetData();
                btnClear_Click(btnClear, null);

                if ((GlobalDec.gEmployeeProperty.user_name == "YOGESH" && GlobalDec.gEmployeeProperty.role_name == "GALAXY ADMIN") || GlobalDec.gEmployeeProperty.role_name == "SURAT ADMIN")
                {
                    labelControl1.Visible = true;
                    txtSalary.Visible = true;
                }
                else
                {
                    labelControl1.Visible = false;
                    txtSalary.Visible = false;
                }
                txtConfigUserName.Focus();
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
                txtConfigUserName.Text = "";
                txtMachineName.Text = "";
                txtMachineName.Enabled = true;
                ChkIsChecker.Checked = false;
                ChkIsManager.Checked = false;
                ChkIsPlanner.Checked = false;
                txtSalary.Text = string.Empty;
                txtMachineName.Focus();
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
        private void dgvUserMaster_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvGalaxyUserMaster.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt64(Drow["config_user_id"]);
                        txtConfigUserName.Text = Val.ToString(Drow["config_user_name"]);
                        txtMachineName.Text = Val.ToString(Drow["machine_name"]);
                        txtSalary.Text = Val.ToString(Drow["salary"]);
                        ChkIsChecker.Checked = Val.ToBoolean(Drow["is_checker"]);
                        ChkIsPlanner.Checked = Val.ToBoolean(Drow["is_planner"]);
                        ChkIsManager.Checked = Val.ToBoolean(Drow["is_manager"]);
                        txtMachineName.Enabled = false;
                        txtConfigUserName.Focus();
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
                //if (txtConfigUserName.Text == string.Empty)
                //{
                //    lstError.Add(new ListError(12, "User Name"));
                //    if (!blnFocus)
                //    {
                //        blnFocus = true;
                //        txtConfigUserName.Focus();
                //    }
                //}
                if (txtMachineName.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Machine Name"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtMachineName.Focus();
                    }
                }

                if (!objUser.ISExists_Machine(txtMachineName.Text, Val.ToInt64(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
                {
                    lstError.Add(new ListError(23, "Machine Name"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtMachineName.Focus();
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
                DataTable DTab = objUser.Config_Galaxy_GetData();
                if ((GlobalDec.gEmployeeProperty.user_name == "YOGESH" && GlobalDec.gEmployeeProperty.role_name == "GALAXY ADMIN") || GlobalDec.gEmployeeProperty.role_name == "SURAT ADMIN")
                {
                    grdGalaxyUserMaster.DataSource = DTab;
                    dgvGalaxyUserMaster.BestFitColumns();
                }
                else
                {
                    grdGalaxyUserMaster.DataSource = DTab;
                    dgvGalaxyUserMaster.Columns["salary"].Visible = false;
                    dgvGalaxyUserMaster.BestFitColumns();
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private bool SaveDetails()
        {
            bool blnReturn = true;
            UserMaster objUser = new UserMaster();
            User_MasterProperty UserMasterProperty = new User_MasterProperty();
            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }

                UserMasterProperty.config_user_id = Val.ToInt32(lblMode.Tag);
                UserMasterProperty.config_user_name = Val.ToString(txtConfigUserName.Text).ToUpper();
                UserMasterProperty.machine_name = Val.ToString(txtMachineName.Text).ToUpper();

                UserMasterProperty.salary = Val.ToDecimal(txtSalary.Text);
                UserMasterProperty.is_checker = Val.ToBoolean(ChkIsChecker.Checked);
                UserMasterProperty.is_planner = Val.ToBoolean(ChkIsPlanner.Checked);
                UserMasterProperty.is_manager = Val.ToBoolean(ChkIsManager.Checked);

                int IntRes = objUser.Save_Config_Galaxy_User(UserMasterProperty);
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Save Config User Details");
                    txtConfigUserName.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Config User Details Data Save Successfully");
                    }
                    else
                    {
                        Global.Confirm("Config User Details Data Update Successfully");
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
                UserMasterProperty = null;
            }

            return blnReturn;
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
                            dgvGalaxyUserMaster.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvGalaxyUserMaster.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvGalaxyUserMaster.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvGalaxyUserMaster.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvGalaxyUserMaster.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvGalaxyUserMaster.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvGalaxyUserMaster.ExportToCsv(Filepath);
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
            Export("xlsx", "Export to Excel", "Excel files 97-2003 (Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*");
        }
        private void MNExportPDF_Click(object sender, EventArgs e)
        {
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
