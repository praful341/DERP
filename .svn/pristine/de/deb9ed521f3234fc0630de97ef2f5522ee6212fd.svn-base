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
    public partial class FrmMfgEmployeeWagesBenchMark : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        FormEvents objBOFormEvents;
        Validation Val;
        BLL.FormPer ObjPer;

        MfgEmployeeWagesBenchMark objEmployeeWagesBenchMark;
        #endregion

        #region Constructor
        public FrmMfgEmployeeWagesBenchMark()
        {
            InitializeComponent();

            objBOFormEvents = new FormEvents();
            Val = new Validation();
            ObjPer = new BLL.FormPer();

            objEmployeeWagesBenchMark = new MfgEmployeeWagesBenchMark();
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
            objBOFormEvents.ObjToDispose.Add(objEmployeeWagesBenchMark);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion

        #region Events   
        private void FrmMfgEmployeeWagesBenchMark_Load(object sender, EventArgs e)
        {
            try
            {
                Global.LOOKUPRoughSieve(lueRoughSieve);

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
                txtRate.Text = "";
                txtSize.Text = "";
                txtRemark.Text = "";
                txtGroup.Text = "";
                txtSequenceNo.Text = "";
                lueRoughSieve.EditValue = null;
                txtGroup.Focus();
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
        private void lueRoughSieve_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmMfgRoughSieve objRoughSieve = new FrmMfgRoughSieve();
                objRoughSieve.ShowDialog();
                Global.LOOKUPRoughSieve(lueRoughSieve);
            }
        }
        private void txtRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as DevExpress.XtraEditors.TextEdit).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }
        private void txtSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as DevExpress.XtraEditors.TextEdit).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        #region GridEvents
        private void dgvEmployeeWagesBenchMarkMaster_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvEmployeeWagesBenchMarkMaster.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt32(Drow["benchmark_id"]);
                        txtGroup.Text = Val.ToString(Drow["group_name"]);
                        lueRoughSieve.EditValue = Val.ToInt32(Drow["rough_sieve_id"]);
                        txtRate.Text = Val.ToString(Drow["rate"]);
                        txtSize.Text = Val.ToString(Drow["size"]);
                        txtRemark.Text = Val.ToString(Drow["remarks"]);
                        txtSequenceNo.Text = Val.ToString(Drow["sequence_no"]);
                        txtGroup.Focus();
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
            MfgEmployeeWagesBenchMark_MasterProperty MfgEmployeeWagesBenchMarkProperty = new MfgEmployeeWagesBenchMark_MasterProperty();
            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }
                List<ListError> lstError = new List<ListError>();


                MfgEmployeeWagesBenchMarkProperty.benchmark_id = Val.ToInt64(lblMode.Tag);
                MfgEmployeeWagesBenchMarkProperty.group_name = txtGroup.Text.ToUpper();
                MfgEmployeeWagesBenchMarkProperty.rough_sieve_id = Val.ToInt64(lueRoughSieve.EditValue);
                MfgEmployeeWagesBenchMarkProperty.remarks = txtRemark.Text.ToUpper();
                MfgEmployeeWagesBenchMarkProperty.sequence_no = Val.ToInt(txtSequenceNo.Text);
                MfgEmployeeWagesBenchMarkProperty.rate = Val.ToDecimal(txtRate.Text);
                MfgEmployeeWagesBenchMarkProperty.size = Val.ToDecimal(txtSize.Text);

                int IntRes = objEmployeeWagesBenchMark.Save(MfgEmployeeWagesBenchMarkProperty);
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Save Employee BenchMark Details");
                    txtGroup.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Employee BenchMark Save Successfully");
                    }
                    else
                    {
                        Global.Confirm("Employee BenchMark Update Successfully");
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
                MfgEmployeeWagesBenchMarkProperty = null;
            }

            return blnReturn;
        }
        private bool ValidateDetails()
        {

            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (txtGroup.Text.Length == 0)
                {
                    lstError.Add(new ListError(12, "Group"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtGroup.Focus();
                    }
                }
                if (lueRoughSieve.EditValue.ToString() == "")
                {
                    lstError.Add(new ListError(12, "Rough Sieve"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueRoughSieve.Focus();
                    }
                }
                if (txtRate.Text.Length == 0)
                {
                    lstError.Add(new ListError(12, "Rate"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtRate.Focus();
                    }
                }
                if (txtSize.Text.Length == 0)
                {
                    lstError.Add(new ListError(12, "Size"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtSize.Focus();
                    }
                }
                if (!objEmployeeWagesBenchMark.ISExists(txtGroup.Text, Val.ToInt64(lueRoughSieve.EditValue), Val.ToInt64(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
                {
                    lstError.Add(new ListError(23, "Group And Sieve"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtGroup.Focus();
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
                DataTable DTab = objEmployeeWagesBenchMark.GetData();
                grdEmployeeWagesBenchMarkMaster.DataSource = DTab;
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
                            dgvEmployeeWagesBenchMarkMaster.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvEmployeeWagesBenchMarkMaster.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvEmployeeWagesBenchMarkMaster.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvEmployeeWagesBenchMarkMaster.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvEmployeeWagesBenchMarkMaster.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvEmployeeWagesBenchMarkMaster.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvEmployeeWagesBenchMarkMaster.ExportToCsv(Filepath);
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
