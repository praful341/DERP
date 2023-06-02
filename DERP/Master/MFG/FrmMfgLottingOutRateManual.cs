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
    public partial class FrmMfgLottingOutRateManual : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        FormEvents objBOFormEvents;
        Validation Val;
        BLL.FormPer ObjPer;

        MfgLottingOutRateManual objLottingOutRateManual;

        #endregion

        #region Constructor
        public FrmMfgLottingOutRateManual()
        {
            InitializeComponent();

            objBOFormEvents = new FormEvents();
            Val = new Validation();
            ObjPer = new BLL.FormPer();

            objLottingOutRateManual = new MfgLottingOutRateManual();
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
            objBOFormEvents.ObjToDispose.Add(objLottingOutRateManual);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion

        #region Events   
        private void FrmMfgLottingOutRateManual_Load(object sender, EventArgs e)
        {
            try
            {
                Global.LOOKUPDepartment(lueDepartment);
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

                dtpOutRateDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpOutRateDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpOutRateDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpOutRateDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpOutRateDate.EditValue = DateTime.Now;

                txtOutRate.Text = "";
                lueDepartment.EditValue = null;
                dtpOutRateDate.Focus();
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
        private void txtOutRate_KeyPress(object sender, KeyPressEventArgs e)
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
        private void dgvLottingOutRateMaster_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvLottingOutRateMaster.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt32(Drow["out_rate_id"]);
                        dtpOutRateDate.Text = Val.ToString(Drow["out_rate_date"]);
                        lueDepartment.EditValue = Val.ToInt32(Drow["lotting_dept_id"]);
                        txtOutRate.Text = Val.ToString(Drow["out_rate"]);
                        dtpOutRateDate.Focus();
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
            MfgLottingOutRateManual_MasterProperty MfgLottingOutRateManualProperty = new MfgLottingOutRateManual_MasterProperty();
            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }
                List<ListError> lstError = new List<ListError>();


                MfgLottingOutRateManualProperty.out_rate_id = Val.ToInt64(lblMode.Tag);
                MfgLottingOutRateManualProperty.department_id = Val.ToInt64(lueDepartment.EditValue);
                MfgLottingOutRateManualProperty.out_rate = Val.ToDecimal(txtOutRate.Text);
                MfgLottingOutRateManualProperty.out_rate_date = Val.DBDate(dtpOutRateDate.Text);

                int IntRes = objLottingOutRateManual.Save(MfgLottingOutRateManualProperty);
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Save Lotting OutRate Manual Details");
                    dtpOutRateDate.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Lotting OutRate Manual Save Successfully");
                    }
                    else
                    {
                        Global.Confirm("Lotting OutRate Manual Update Successfully");
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
                MfgLottingOutRateManualProperty = null;
            }

            return blnReturn;
        }
        private bool ValidateDetails()
        {

            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (txtOutRate.Text.Length == 0)
                {
                    lstError.Add(new ListError(12, "Rate"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtOutRate.Focus();
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
                //if (!objLottingOutRateManual.ISExists(txtGroup.Text, Val.ToInt64(lueRoughSieve.EditValue), Val.ToInt64(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
                //{
                //    lstError.Add(new ListError(23, "Group And Sieve"));
                //    if (!blnFocus)
                //    {
                //        blnFocus = true;
                //        txtGroup.Focus();
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
                DataTable DTab = objLottingOutRateManual.GetData();
                grdLottingOutRateMaster.DataSource = DTab;
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
                            dgvLottingOutRateMaster.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvLottingOutRateMaster.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvLottingOutRateMaster.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvLottingOutRateMaster.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvLottingOutRateMaster.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvLottingOutRateMaster.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvLottingOutRateMaster.ExportToCsv(Filepath);
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
