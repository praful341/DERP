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
    public partial class FrmProcessMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member
        FormEvents objBOFormEvents;
        Validation Val;
        BLL.FormPer ObjPer;

        ProcessMaster objProcess;

        DataTable m_dtbProcesstype;
        #endregion

        #region Constructor
        public FrmProcessMaster()
        {
            InitializeComponent();

            objBOFormEvents = new FormEvents();
            Val = new Validation();
            ObjPer = new BLL.FormPer();

            objProcess = new ProcessMaster();

            m_dtbProcesstype = new DataTable();
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
            chkActive.Checked = true;
        }
        private void AttachFormEvents()
        {
            objBOFormEvents.CurForm = this;
            objBOFormEvents.FormKeyPress = true;
            objBOFormEvents.FormKeyDown = true;
            objBOFormEvents.FormResize = true;
            objBOFormEvents.FormClosing = true;
            objBOFormEvents.ObjToDispose.Add(objProcess);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion

        #region Events
        private void FrmProcessMaster_Load(object sender, EventArgs e)
        {
            m_dtbProcesstype = new DataTable();
            m_dtbProcesstype.Columns.Add("process_type");
            m_dtbProcesstype.Rows.Add("MFG");
            m_dtbProcesstype.Rows.Add("Prepolishing");
            chkActive.Checked = true;
            try
            {
                lueProcessType.Properties.DataSource = m_dtbProcesstype;
                lueProcessType.Properties.ValueMember = "process_type";
                lueProcessType.Properties.DisplayMember = "process_type";
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
                txtRemark.Text = string.Empty;
                lueProcessType.EditValue = null;
                chkActive.Checked = true;
                chkIsEstimation.Checked = false;
                txtStage.Text = string.Empty;
                txtProcessName.Text = string.Empty;
                txtSequenceNo.Text = string.Empty;
                txtProcessName.Focus();
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
        private void dgvProcessMaster_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvProcessMaster.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt32(Drow["process_id"]);
                        txtProcessName.Text = Val.ToString(Drow["process_name"]);
                        lueProcessType.EditValue = Val.ToString(Drow["process_type"]);
                        txtStage.Text = Val.ToString(Drow["stage"]);
                        txtRemark.Text = Val.ToString(Drow["remarks"]);
                        txtSequenceNo.Text = Val.ToString(Drow["sequence_no"]);
                        chkIsEstimation.Checked = Val.ToBoolean(Drow["is_estimation"]);
                        chkActive.Checked = Val.ToBoolean(Drow["active"]);
                        txtProcessName.Focus();
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
            Process_MasterProperty ProcessMasterProperty = new Process_MasterProperty();

            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }

                ProcessMasterProperty.process_id = Val.ToInt32(lblMode.Tag);
                ProcessMasterProperty.process_name = txtProcessName.Text.ToUpper();
                ProcessMasterProperty.process_type = Val.ToString(lueProcessType.EditValue);
                ProcessMasterProperty.stage = Val.ToString(txtStage.Text);
                ProcessMasterProperty.sequence_no = Val.ToInt32(txtSequenceNo.Text);
                ProcessMasterProperty.is_estimation = Val.ToBoolean(chkIsEstimation.Checked);
                ProcessMasterProperty.active = Val.ToBoolean(chkActive.Checked);
                ProcessMasterProperty.remarks = txtRemark.Text.ToUpper();

                int IntRes = objProcess.Save(ProcessMasterProperty);
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Save Process Details");
                    txtProcessName.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Process Details Data Save Successfully");
                    }
                    else
                    {
                        Global.Confirm("Process Details Data Update Successfully");
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
                ProcessMasterProperty = null;
            }

            return blnReturn;
        }
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (txtProcessName.Text == string.Empty)
                {
                    lstError.Add(new ListError(13, "Process Name"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtProcessName.Focus();
                    }
                }

                if (lueProcessType.Text == "")
                {
                    lstError.Add(new ListError(13, "Process Type"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueProcessType.Focus();
                    }
                }
                if (txtSequenceNo.Text == string.Empty)
                {
                    lstError.Add(new ListError(13, "Sequence No"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtSequenceNo.Focus();
                    }
                }
                if (!objProcess.ISExists(txtProcessName.Text, Val.ToInt(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
                {
                    lstError.Add(new ListError(23, "Process Name"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtProcessName.Focus();
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
                DataTable DTab = objProcess.GetData();
                grdProcessMaster.DataSource = DTab;
                dgvProcessMaster.BestFitColumns();
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
                            dgvProcessMaster.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvProcessMaster.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvProcessMaster.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvProcessMaster.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvProcessMaster.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvProcessMaster.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvProcessMaster.ExportToCsv(Filepath);
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
