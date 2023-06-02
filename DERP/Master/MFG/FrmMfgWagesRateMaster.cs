using BLL;
using BLL.FunctionClasses.Master;
using BLL.PropertyClasses.Master;
using DERP.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Master.MFG
{
    public partial class FrmMfgWagesRateMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        FormEvents objBOFormEvents;
        Validation Val;
        BLL.FormPer ObjPer;

        FillCombo ObjFillCombo;
        MfgWagesRateMaster ObjMfgWagesRate;
        DataTable m_dtbSubProcess = new DataTable();
        #endregion

        #region Constructor
        public FrmMfgWagesRateMaster()
        {
            InitializeComponent();

            objBOFormEvents = new FormEvents();
            Val = new Validation();
            ObjPer = new BLL.FormPer();

            ObjFillCombo = new FillCombo();
            ObjMfgWagesRate = new MfgWagesRateMaster();
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
            objBOFormEvents.ObjToDispose.Add(ObjMfgWagesRate);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion

        #region Events     
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
                txtPerHrsRate.Text = "";
                txtRemark.Text = "";
                txtPerPcsRate.Text = "";
                txtPerHrsRate.Text = "";
                txtPerCrtsRate.Text = "";
                cmbWagesType.SelectedIndex = 0;
                chkActive.Checked = true;
                lueCompany.EditValue = null;
                lueBranch.EditValue = null;
                lueLocation.EditValue = null;
                lueDepartment.EditValue = null;
                lueRoughSieve.EditValue = null;
                lueType.EditValue = null;
                lueProcess.EditValue = null;
                lueSubProcess.EditValue = null;
                txtPerPcsRate.Focus();
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }

        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void FrmMfgWagesRateMaster_Load(object sender, EventArgs e)
        {
            try
            {
                GetData();
                btnClear_Click(btnClear, null);
                Global.LOOKUPCompany_New(lueCompany);
                Global.LOOKUPBranch_New(lueBranch);
                Global.LOOKUPLocation_New(lueLocation);
                Global.LOOKUPDepartment_New(lueDepartment);
                Global.LOOKUPMfgPacketTypeWages(lueType);
                Global.LOOKUPRoughSieve(lueRoughSieve);
                Global.LOOKUPProcess(lueProcess);
                Global.LOOKUPSubProcess(lueSubProcess);
                m_dtbSubProcess = (((DataTable)lueSubProcess.Properties.DataSource).Copy());
                chkActive.Checked = true;
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
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
                        lueSubProcess.EditValue = System.DBNull.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }

        #region GridEvents
        private void dgvMfgWagesRateMaster_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvMfgWagesRateMaster.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt64(Drow["wages_rate_id"]);
                        lueCompany.EditValue = Val.ToInt(Drow["company_id"]);
                        lueBranch.EditValue = Val.ToInt(Drow["branch_id"]);
                        lueLocation.EditValue = Val.ToInt(Drow["location_id"]);
                        lueDepartment.EditValue = Val.ToInt(Drow["department_id"]);
                        lueProcess.EditValue = Val.ToInt(Drow["process_id"]);
                        lueSubProcess.EditValue = Val.ToInt(Drow["sub_process_id"]);
                        cmbWagesType.SelectedText = Val.ToString(Drow["wages_type"]);
                        txtPerPcsRate.Text = Val.ToDecimal(Drow["per_pcs_rate"]).ToString();
                        txtPerCrtsRate.Text = Val.ToDecimal(Drow["per_carat_rate"]).ToString();
                        txtPerHrsRate.Text = Val.ToDecimal(Drow["per_hrs_rate"]).ToString();
                        txtRemark.Text = Val.ToString(Drow["remarks"]);
                        chkActive.Checked = Val.ToBoolean(Drow["active"]);
                        lueType.EditValue = Val.ToInt(Drow["packet_type_id"]);
                        lueRoughSieve.EditValue = Val.ToInt(Drow["rough_sieve_id"]);
                        txtPerPcsRate.Focus();
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
                    lstError.Add(new ListError(12, "Company"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueCompany.Focus();
                    }
                }
                if (lueBranch.Text == "")
                {
                    lstError.Add(new ListError(12, "Branch"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueBranch.Focus();
                    }
                }
                if (lueLocation.Text == "")
                {
                    lstError.Add(new ListError(12, "Location"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueLocation.Focus();
                    }
                }
                if (lueDepartment.Text == "")
                {
                    lstError.Add(new ListError(12, "Department"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueDepartment.Focus();
                    }
                }
                if (lueType.Text == "")
                {
                    lstError.Add(new ListError(12, "Packet Type"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueType.Focus();
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
            MfgWagesRate_MasterProperty MfgWagesRateMasterProperty = new MfgWagesRate_MasterProperty();

            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }
                // List<ListError> lstError = new List<ListError>();


                MfgWagesRateMasterProperty.wages_rate_id = Val.ToInt32(lblMode.Tag);


                MfgWagesRateMasterProperty.company_id = Val.ToInt(lueCompany.EditValue);
                MfgWagesRateMasterProperty.branch_id = Val.ToInt(lueBranch.EditValue);
                MfgWagesRateMasterProperty.location_id = Val.ToInt(lueLocation.EditValue);
                MfgWagesRateMasterProperty.department_id = Val.ToInt(lueDepartment.EditValue);
                MfgWagesRateMasterProperty.wages_type = Val.ToString(cmbWagesType.Text);

                MfgWagesRateMasterProperty.per_pcs_rate = Val.ToDecimal(txtPerPcsRate.Text);
                MfgWagesRateMasterProperty.per_carat_rate = Val.ToDecimal(txtPerCrtsRate.Text);
                MfgWagesRateMasterProperty.per_hrs_rate = Val.ToDecimal(txtPerHrsRate.Text);
                MfgWagesRateMasterProperty.active = Val.ToBoolean(chkActive.Checked);
                MfgWagesRateMasterProperty.remarks = txtRemark.Text.ToUpper();
                MfgWagesRateMasterProperty.packet_type_id = Val.ToInt(lueType.EditValue);
                MfgWagesRateMasterProperty.rough_sieve_id = Val.ToInt(lueRoughSieve.EditValue);
                MfgWagesRateMasterProperty.process_id = Val.ToInt(lueProcess.EditValue);
                MfgWagesRateMasterProperty.sub_process_id = Val.ToInt(lueSubProcess.EditValue);

                int IntRes = ObjMfgWagesRate.Save(MfgWagesRateMasterProperty);
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Save Wages Rate Details");
                    txtPerHrsRate.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Wages Rate Details Data Save Successfully");
                    }
                    else
                    {
                        Global.Confirm("Wages Rate Details Data Update Successfully");
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
                MfgWagesRateMasterProperty = null;
            }

            return blnReturn;
        }
        public void GetData()
        {
            try
            {
                DataTable DTab = ObjMfgWagesRate.GetData();
                grdMfgWagesRateMaster.DataSource = DTab;
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
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
                            dgvMfgWagesRateMaster.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvMfgWagesRateMaster.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvMfgWagesRateMaster.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvMfgWagesRateMaster.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvMfgWagesRateMaster.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvMfgWagesRateMaster.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvMfgWagesRateMaster.ExportToCsv(Filepath);
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
