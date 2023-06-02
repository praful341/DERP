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
    public partial class FrmMfgProcessAgeingMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.FormEvents objBOFormEvents;
        BLL.Validation Val;
        BLL.FormPer ObjPer;

        MfgProcessAgeingMaster objProcAgeing;
        #endregion

        #region Constructor
        public FrmMfgProcessAgeingMaster()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            Val = new BLL.Validation();
            ObjPer = new BLL.FormPer();

            objProcAgeing = new MfgProcessAgeingMaster();
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
            objBOFormEvents.ObjToDispose.Add(objProcAgeing);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion

        #region Events
        private void FrmMfgProcessAgeingMaster_Load(object sender, EventArgs e)
        {
            try
            {
                GetData();
                btnClear_Click(btnClear, null);
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
                txtAgeingDays.Text = "0";
                lueProcess.EditValue = null;
                lueSubProcess.EditValue = null;
                txtRemark.Text = string.Empty;
                chkActive.Checked = true;
                txtAgeingDays.Focus();
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
        private void dgvMfgProcessAgeingMaster_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvProcessAgeingMaster.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt64(Drow["process_ageing_id"]);
                        lueProcess.EditValue = Val.ToInt32(Drow["process_id"]);
                        lueSubProcess.EditValue = Val.ToInt32(Drow["sub_process_id"]);
                        txtAgeingDays.Text = Val.ToString(Drow["ageing_days"]);
                        txtRemark.Text = Val.ToString(Drow["remarks"]);
                        chkActive.Checked = Val.ToBoolean(Drow["active"]);
                        lueProcess.Focus();
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
            MfgProcessAgeing_MasterProperty ProcessAgeingMasterProperty = new MfgProcessAgeing_MasterProperty();

            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }
                ProcessAgeingMasterProperty.process_ageing_id = Val.ToInt32(lblMode.Tag);
                ProcessAgeingMasterProperty.process_id = Val.ToInt32(lueProcess.EditValue);
                ProcessAgeingMasterProperty.sub_process_id = Val.ToInt32(lueSubProcess.EditValue);
                ProcessAgeingMasterProperty.ageing_days = Val.ToInt(txtAgeingDays.Text);
                ProcessAgeingMasterProperty.remarks = txtRemark.Text.ToUpper();
                ProcessAgeingMasterProperty.active = Val.ToBoolean(chkActive.Checked);

                int IntRes = objProcAgeing.Save(ProcessAgeingMasterProperty);
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Save Process Ageing Days Details");
                    txtAgeingDays.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Process Ageing Days Details Data Successfully");
                    }
                    else
                    {
                        Global.Confirm("Process Ageing Days Details  Update Successfully");
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
                ProcessAgeingMasterProperty = null;
            }

            return blnReturn;
        }
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (lueProcess.Text == "")
                {
                    lstError.Add(new ListError(13, "Process"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueProcess.Focus();
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

                if (string.IsNullOrEmpty(txtAgeingDays.Text))
                {
                    lstError.Add(new ListError(12, "Ageing Days"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtAgeingDays.Focus();
                    }
                }

                if (!objProcAgeing.ISExists(Val.ToInt(lueProcess.EditValue), Val.ToInt(lueSubProcess.EditValue), Val.ToInt64(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
                {
                    lstError.Add(new ListError(23, "Ageing Days Exist"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueProcess.Focus();
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
                DataTable DTab = objProcAgeing.GetData();
                grdProcessAgeingMaster.DataSource = DTab;
                dgvProcessAgeingMaster.BestFitColumns();
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
                            dgvProcessAgeingMaster.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvProcessAgeingMaster.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvProcessAgeingMaster.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvProcessAgeingMaster.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvProcessAgeingMaster.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvProcessAgeingMaster.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvProcessAgeingMaster.ExportToCsv(Filepath);
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
