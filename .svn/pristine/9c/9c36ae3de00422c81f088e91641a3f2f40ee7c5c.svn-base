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
    public partial class FrmRateDetailMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.FormEvents objBOFormEvents;
        BLL.Validation Val;
        BLL.FormPer ObjPer;

        RateDetailMaster objRateDetail;
        #endregion

        #region Constructor
        public FrmRateDetailMaster()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            Val = new BLL.Validation();
            ObjPer = new BLL.FormPer();

            objRateDetail = new RateDetailMaster();
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
            objBOFormEvents.ObjToDispose.Add(objRateDetail);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion

        #region Events
        private void FrmRateDetailMaster_Load(object sender, System.EventArgs e)
        {
            try
            {
                Global.LOOKUPRate(lueRate);
                Global.LOOKUPRate(lueRate);
                Global.LOOKUPRate(lueRate);
                GetData();
                btnClear_Click(btnClear, null);
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void btnSave_Click(object sender, System.EventArgs e)
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
        private void btnClear_Click(object sender, System.EventArgs e)
        {
            try
            {
                lblMode.Tag = 0;
                lblMode.Text = "Add Mode";
                txtRemark.Text = "";
                lueRate.EditValue = null;
                lueAssort.EditValue = null;
                lueSieve.EditValue = null;
                chkActive.Checked = true;
                txtSequenceNo.Text = "";
                txtRate.Text = "";
                lueRate.Focus();
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void btnExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        #region GridEvents
        private void dgvRateDetailMaster_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (e.Clicks == 2)
                {
                    DataRow Drow = dgvRateDetailMaster.GetDataRow(e.RowHandle);
                    lblMode.Text = "Edit Mode";
                    lblMode.Tag = Val.ToInt64(Drow["company_id"]);
                    lueRate.EditValue = Val.ToInt32(Drow["country_id"]);
                    lueAssort.EditValue = Val.ToInt32(Drow["state_id"]);
                    lueSieve.EditValue = Val.ToInt32(Drow["city_id"]);
                    txtRemark.Text = Val.ToString(Drow["remarks"]);
                    chkActive.Checked = Val.ToBoolean(Drow["active"]);
                    txtRate.Text = Val.ToString(Drow["remarks"]);
                    lueRate.Focus();
                }
            }
        }
        #endregion

        #endregion

        #region Functions
        private bool SaveDetails()
        {
            bool blnReturn = true;
            RateDetail_MasterProperty RateDetMasterProperty = new RateDetail_MasterProperty();
            RateDetailMaster objRateDetail = new RateDetailMaster();

            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }


                RateDetMasterProperty.ratedetail_id = Val.ToInt32(lblMode.Tag);
                RateDetMasterProperty.ratemaster_id = Val.ToInt(lueRate.EditValue);
                RateDetMasterProperty.sieve_id = Val.ToInt(lueSieve.EditValue);
                RateDetMasterProperty.assort_id = Val.ToInt(lueAssort.EditValue);
                RateDetMasterProperty.active = Val.ToBoolean(chkActive.Checked);
                RateDetMasterProperty.remarks = Val.ToString(txtRemark.Text).ToUpper();
                RateDetMasterProperty.sequence_no = Val.ToInt(txtSequenceNo.Text);

                int IntRes = objRateDetail.Save(RateDetMasterProperty);
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Save Rate Details");
                    lueRate.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Rate Details Data Save Successfully");
                    }
                    else
                    {
                        Global.Confirm("Rate Details Data Update Successfully");
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
                RateDetMasterProperty = null;
            }

            return blnReturn;
        }
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (txtRate.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Rate"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtRate.Focus();
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

                if (lueAssort.Text == "")
                {
                    lstError.Add(new ListError(13, "Assort"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueAssort.Focus();
                    }
                }
                if (lueSieve.Text == "")
                {
                    lstError.Add(new ListError(13, "Sieve"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueSieve.Focus();
                    }
                }
                if (lueRate.Text == "")
                {
                    lstError.Add(new ListError(13, "Rate"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueRate.Focus();
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
                DataTable DTab = objRateDetail.GetData();
                grdRateDetailMaster.DataSource = DTab;
                dgvRateDetailMaster.BestFitColumns();
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
                            dgvRateDetailMaster.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvRateDetailMaster.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvRateDetailMaster.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvRateDetailMaster.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvRateDetailMaster.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvRateDetailMaster.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvRateDetailMaster.ExportToCsv(Filepath);
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
