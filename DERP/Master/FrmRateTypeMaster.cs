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
    public partial class FrmRateTypeMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        FormEvents objBOFormEvents;
        Validation Val;
        BLL.FormPer ObjPer;

        RateTypeMaster objRateType;
        #endregion

        #region Constructor
        public FrmRateTypeMaster()
        {
            InitializeComponent();

            objBOFormEvents = new FormEvents();
            Val = new Validation();
            ObjPer = new BLL.FormPer();

            objRateType = new RateTypeMaster();
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
            objBOFormEvents.ObjToDispose.Add(objRateType);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion

        #region Events       
        private void FrmRateTypeMaster_Load(object sender, EventArgs e)
        {
            try
            {
                Global.LOOKUPCurrency(lueFCurrency);
                Global.LOOKUPCurrency(lueTCurrency);
                Global.LOOKUPLocation(lueLocation);
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
            lblMode.Tag = 0;
            lblMode.Text = "Add Mode";
            txtRateType.Text = "";
            txtRemark.Text = "";
            txtSequenceNo.Text = "";
            lueFCurrency.EditValue = null;
            lueTCurrency.EditValue = null;
            lueLocation.EditValue = null;
            chkActive.Checked = true;
            txtRateType.Focus();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region GridEvents
        private void dgvRateTypeMaster_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvRateTypeMaster.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt32(Drow["ratetype_id"]);
                        txtRateType.Text = Val.ToString(Drow["rate_type"]);
                        txtRemark.Text = Val.ToString(Drow["remarks"]);
                        txtSequenceNo.Text = Val.ToString(Drow["sequence_no"]);
                        lueFCurrency.EditValue = Val.ToInt32(Drow["from_currency_id"]);
                        lueTCurrency.EditValue = Val.ToInt32(Drow["to_currency_id"]);
                        lueLocation.EditValue = Val.ToInt32(Drow["location_id"]);
                        chkActive.Checked = Val.ToBoolean(Drow["active"]);
                        txtRateType.Focus();
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
            RateTypeMaster objRateType = new RateTypeMaster();
            RateType_MasterProperty RateTypeMasterProperty = new RateType_MasterProperty();

            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }

                RateTypeMasterProperty.ratetype_id = Val.ToInt32(lblMode.Tag);
                RateTypeMasterProperty.ratetype = txtRateType.Text.ToUpper();
                RateTypeMasterProperty.active = Val.ToBoolean(chkActive.Checked);
                RateTypeMasterProperty.remarks = txtRemark.Text.ToUpper();
                RateTypeMasterProperty.sequence_no = Val.ToInt(txtSequenceNo.Text);
                RateTypeMasterProperty.from_currency_id = Val.ToInt32(lueFCurrency.EditValue);
                RateTypeMasterProperty.to_currency_id = Val.ToInt32(lueTCurrency.EditValue);
                RateTypeMasterProperty.location_id = Val.ToInt32(lueLocation.EditValue);

                int IntRes = objRateType.Save(RateTypeMasterProperty);
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Save Rate Type Details");
                    txtRateType.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Rate Type Details Data Save Successfully");
                    }
                    else
                    {
                        Global.Confirm("Rate Type Details Data Update Successfully");
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
                RateTypeMasterProperty = null;
            }

            return blnReturn;
        }
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (txtRateType.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Rate Type"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtRateType.Focus();
                    }
                }

                if (!objRateType.ISExists(txtRateType.Text, Val.ToInt64(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
                {
                    lstError.Add(new ListError(23, "Rate Type"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtRateType.Focus();
                    }
                }

                if (txtSequenceNo.Text == string.Empty || txtSequenceNo.Text == "0")
                {
                    lstError.Add(new ListError(12, "Sequence No"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtSequenceNo.Focus();
                    }
                }
                if (lueLocation.Text == "")
                {
                    lstError.Add(new ListError(13, "location"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueLocation.Focus();
                    }
                }
                if (lueFCurrency.Text == "")
                {
                    lstError.Add(new ListError(13, "From Currency"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueFCurrency.Focus();
                    }
                }
                if (lueTCurrency.Text == "")
                {
                    lstError.Add(new ListError(13, "To Currency"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueTCurrency.Focus();
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
                DataTable DTab = objRateType.GetData();
                grdRateTypeMaster.DataSource = DTab;
                grdRateTypeMaster.RefreshDataSource();
                dgvRateTypeMaster.BestFitColumns();
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
                            dgvRateTypeMaster.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvRateTypeMaster.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvRateTypeMaster.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvRateTypeMaster.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvRateTypeMaster.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvRateTypeMaster.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvRateTypeMaster.ExportToCsv(Filepath);
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
