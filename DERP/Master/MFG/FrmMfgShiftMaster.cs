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
    public partial class FrmMfgShiftMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member
        FormEvents objBOFormEvents;
        Validation Val;
        BLL.FormPer ObjPer;

        MfgShiftMaster objShift;
        #endregion

        #region Constructor
        public FrmMfgShiftMaster()
        {
            InitializeComponent();
            objBOFormEvents = new FormEvents();
            Val = new Validation();
            ObjPer = new BLL.FormPer();

            objShift = new MfgShiftMaster();
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
            objBOFormEvents.ObjToDispose.Add(objShift);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion

        #region Events
        private void FrmMfgShiftMaster_Load(object sender, EventArgs e)
        {
            try
            {
                GetData();
                btnClear_Click(btnClear, null);
                Global.LOOKUPShift(lueNextShift);
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
                txtShiftName.Text = string.Empty;
                txtShiftStartTime.Text = string.Empty;
                txtShiftEndTime.Text = string.Empty;
                txtPunchStartTime.Text = string.Empty;
                txtPunchEndTime.Text = string.Empty;
                txtLunchStartTime.Text = string.Empty;
                txtLunchEndTime.Text = string.Empty;
                txtHalfDayAfter.Text = string.Empty;
                txtHalfDayBefore.Text = string.Empty;
                txtGraceTime.Text = string.Empty;
                txtIntervalStartTime.Text = string.Empty;
                txtIntervalEndTime.Text = string.Empty;
                lueNextShift.EditValue = null;
                txtTotalShiftHrs.Text = string.Empty;
                txtShiftType.Text = string.Empty;
                txtRemark.Text = string.Empty;
                chkActive.Checked = true;
                //btnSave.Enabled = true;
                txtShiftName.Focus();
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
        private void dgvMfgShiftMaster_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvShiftMaster.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt64(Drow["shift_id"]);
                        txtShiftName.Text = Val.ToString(Drow["shift_name"]);
                        txtShiftStartTime.Text = Val.ToString(Drow["start_time"]);
                        txtShiftEndTime.Text = Val.ToString(Drow["end_time"]);
                        txtPunchStartTime.Text = Val.ToString(Drow["punch_start_time"]);
                        txtPunchEndTime.Text = Val.ToString(Drow["punch_end_time"]);
                        txtLunchStartTime.Text = Val.ToString(Drow["lunch_start_time"]);
                        txtPunchEndTime.Text = Val.ToString(Drow["lunch_end_time"]);
                        txtHalfDayAfter.Text = Val.ToString(Drow["half_day_after"]);
                        txtHalfDayBefore.Text = Val.ToString(Drow["half_day_before"]);
                        txtGraceTime.Text = Val.ToString(Drow["grace_time"]);
                        txtIntervalStartTime.Text = Val.ToString(Drow["start_time_interval"]);
                        txtIntervalEndTime.Text = Val.ToString(Drow["end_time_interval"]);
                        lueNextShift.EditValue = Val.ToInt32(Drow["next_shift_id"]);
                        txtTotalShiftHrs.Text = Val.ToString(Drow["total_shift_hrs"]);
                        txtShiftType.Text = Val.ToString(Drow["shift_type"]);
                        txtRemark.Text = Val.ToString(Drow["remarks"]);
                        chkActive.Checked = Val.ToBoolean(Drow["active"]);
                        txtShiftName.Focus();
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

        #region Functions
        private bool SaveDetails()
        {
            bool blnReturn = true;
            MfgShift_MasterProperty ShiftMasterProperty = new MfgShift_MasterProperty();

            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }
                ShiftMasterProperty.shift_id = Val.ToInt32(lblMode.Tag);
                ShiftMasterProperty.shift_name = txtShiftName.Text.ToUpper();
                ShiftMasterProperty.start_time = Val.ToString(txtShiftStartTime.Text);
                ShiftMasterProperty.end_time = Val.ToString(txtShiftEndTime.Text);
                ShiftMasterProperty.punch_start_time = Val.ToString(txtPunchStartTime.Text);
                ShiftMasterProperty.punch_end_time = Val.ToString(txtPunchEndTime.Text);
                ShiftMasterProperty.lunch_start_time = Val.ToString(txtLunchStartTime.Text);
                ShiftMasterProperty.lunch_end_time = Val.ToString(txtLunchEndTime.Text);
                ShiftMasterProperty.half_day_after = Val.ToString(txtHalfDayAfter.Text);
                ShiftMasterProperty.half_day_before = Val.ToString(txtHalfDayBefore.Text);
                ShiftMasterProperty.grace_time = Val.ToString(txtGraceTime.Text);
                ShiftMasterProperty.start_time_interval = Val.ToString(txtIntervalStartTime.Text);
                ShiftMasterProperty.end_time_interval = Val.ToString(txtIntervalEndTime.Text);
                ShiftMasterProperty.next_shift_id = Val.ToInt(lueNextShift.EditValue);
                ShiftMasterProperty.total_shift_hrs = Val.ToString(txtTotalShiftHrs.Text);
                ShiftMasterProperty.shift_type = Val.ToString(txtShiftType.Text);
                ShiftMasterProperty.remarks = txtRemark.Text.ToUpper();
                ShiftMasterProperty.active = Val.ToBoolean(chkActive.Checked);

                int IntRes = objShift.Save(ShiftMasterProperty);
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Save Shift Details");
                    txtShiftName.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Shift Details Data Successfully");
                    }
                    else
                    {
                        Global.Confirm("Shift Details  Update Successfully");
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
                ShiftMasterProperty = null;
            }

            return blnReturn;
        }
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (txtShiftName.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Shift Name"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtShiftName.Focus();
                    }
                }
                if (txtShiftStartTime.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Shift Start Time"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtShiftStartTime.Focus();
                    }
                }
                if (txtShiftEndTime.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Shift End Time"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtShiftEndTime.Focus();
                    }
                }

                if (!objShift.ISExists(txtShiftName.Text, Val.ToInt64(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
                {
                    lstError.Add(new ListError(23, "Tension Type"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtShiftName.Focus();
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
                DataTable DTab = objShift.GetData();
                grdShiftMaster.DataSource = DTab;
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
                            dgvShiftMaster.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvShiftMaster.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvShiftMaster.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvShiftMaster.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvShiftMaster.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvShiftMaster.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvShiftMaster.ExportToCsv(Filepath);
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
