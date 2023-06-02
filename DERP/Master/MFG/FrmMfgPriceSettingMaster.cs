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
    public partial class FrmMfgPriceSettingMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member
        BLL.FormEvents objBOFormEvents;
        BLL.Validation Val;
        BLL.FormPer ObjPer;

        MfgPriceSettingMaster objPriceSetting;
        #endregion

        #region Constructor
        public FrmMfgPriceSettingMaster()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            Val = new BLL.Validation();
            ObjPer = new BLL.FormPer();

            objPriceSetting = new MfgPriceSettingMaster();
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
            objBOFormEvents.ObjToDispose.Add(objPriceSetting);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion

        #region Events
        private void FrmMfgTensionTypeMaster_Load(object sender, EventArgs e)
        {
            try
            {
                GetData();
                btnClear_Click(btnClear, null);
                Global.LOOKUPCompany(lueCompany);
                Global.LOOKUPBranch(lueBranch);
                Global.LOOKUPLocation(lueLocation);
                Global.LOOKUPCurrency(lueCurrency);
                Global.LOOKUPRate(lueRateType);

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
                txtPriceSettingName.Text = string.Empty;
                txtPriceSettingColumn.Text = string.Empty;
                txtDays.Text = string.Empty;
                txtPercentage.Text = string.Empty;
                lueCompany.EditValue = null;
                lueBranch.EditValue = null;
                lueLocation.EditValue = null;
                lueCurrency.EditValue = null;
                lueRateType.EditValue = null;

                txtPriceSettingName.Focus();
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
        private void dgvMfgTensionTypeMaster_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvPriceSettingMaster.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt64(Drow["setting_id"]);
                        txtPriceSettingName.Text = Val.ToString(Drow["setting_name"]);
                        txtPriceSettingColumn.Text = Val.ToString(Drow["setting_column"]);
                        lueCompany.EditValue = Val.ToInt(Drow["company_id"]);
                        lueBranch.EditValue = Val.ToInt(Drow["branch_id"]);
                        lueLocation.EditValue = Val.ToInt(Drow["location_id"]);
                        lueCurrency.EditValue = Val.ToInt(Drow["currency_id"]);
                        lueRateType.EditValue = Val.ToInt(Drow["rate_type_id"]);
                        txtPercentage.Text = Val.ToString(Drow["per"]);
                        txtDays.Text = Val.ToString(Drow["days"]);

                        txtPriceSettingName.Focus();
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
            MfgPrice_SettingProperty PriceSettingProperty = new MfgPrice_SettingProperty();

            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }
                PriceSettingProperty.setting_id = Val.ToInt32(lblMode.Tag);
                PriceSettingProperty.setting_name = txtPriceSettingName.Text.ToUpper();
                PriceSettingProperty.setting_column = Val.ToString(txtPriceSettingColumn.Text);
                PriceSettingProperty.company_id = Val.ToInt(lueCompany.EditValue);
                PriceSettingProperty.branch_id = Val.ToInt(lueBranch.EditValue);
                PriceSettingProperty.location_id = Val.ToInt(lueLocation.EditValue);
                PriceSettingProperty.currency_id = Val.ToInt(lueCurrency.EditValue);
                PriceSettingProperty.rate_type_id = Val.ToInt(lueRateType.EditValue);
                PriceSettingProperty.per = Val.ToDecimal(txtPercentage.Text);
                PriceSettingProperty.days = Val.ToInt(txtDays.Text);

                int IntRes = objPriceSetting.Save(PriceSettingProperty);
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Save Setting Details");
                    txtPriceSettingName.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Setting Details Data Successfully");
                    }
                    else
                    {
                        Global.Confirm("Setting Details  Update Successfully");
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
                PriceSettingProperty = null;
            }

            return blnReturn;
        }
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (txtPriceSettingName.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Setting Name"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtPriceSettingName.Focus();
                    }
                }
                if (txtPriceSettingColumn.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Setting Column"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtPriceSettingColumn.Focus();
                    }
                }

                //if (!objPriceSetting.ISExists(txtPriceSettingName.Text, Val.ToInt64(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
                //{
                //    lstError.Add(new ListError(23, "Machine Name"));
                //    if (!blnFocus)
                //    {
                //        blnFocus = true;
                //        txtPriceSettingName.Focus();
                //    }

                //}
                //if (txtPercentage.Text == string.Empty)
                //{
                //    lstError.Add(new ListError(12, "Percentage"));
                //    if (!blnFocus)
                //    {
                //        blnFocus = true;
                //        txtPriceSettingColumn.Focus();
                //    }
                //}
                if (lueCompany.Text == string.Empty)
                {
                    lstError.Add(new ListError(13, "Company"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueCompany.Focus();
                    }
                }
                if (lueBranch.Text == string.Empty)
                {
                    lstError.Add(new ListError(13, "Branch"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueBranch.Focus();
                    }
                }
                if (lueLocation.Text == string.Empty)
                {
                    lstError.Add(new ListError(13, "Location"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueLocation.Focus();
                    }
                }
                if (lueCurrency.Text == string.Empty)
                {
                    lstError.Add(new ListError(13, "Currency"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueCurrency.Focus();
                    }
                }
                if (lueRateType.Text == string.Empty)
                {
                    lstError.Add(new ListError(13, "Rate Type"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueRateType.Focus();
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
                DataTable DTab = objPriceSetting.GetData();
                grdPriceSettingMaster.DataSource = DTab;
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
                            dgvPriceSettingMaster.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvPriceSettingMaster.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvPriceSettingMaster.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvPriceSettingMaster.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvPriceSettingMaster.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvPriceSettingMaster.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvPriceSettingMaster.ExportToCsv(Filepath);
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
