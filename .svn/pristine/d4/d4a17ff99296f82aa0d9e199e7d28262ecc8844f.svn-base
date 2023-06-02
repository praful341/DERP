using BLL;
using BLL.FunctionClasses.Master.MFG;
using BLL.PropertyClasses.Master.MFG;
using DERP.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DREP.Master.MFG
{
    public partial class FrmMfgRoughClarityMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        FormEvents objBOFormEvents;
        Validation Val;
        BLL.FormPer ObjPer;

        MfgRoughClarityMaster objRoughClarity;

        DataTable m_dtbType;
        #endregion

        #region Constructor
        public FrmMfgRoughClarityMaster()
        {
            InitializeComponent();

            objBOFormEvents = new FormEvents();
            Val = new Validation();
            ObjPer = new BLL.FormPer();

            objRoughClarity = new MfgRoughClarityMaster();

            m_dtbType = new DataTable();
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
            objBOFormEvents.ObjToDispose.Add(objRoughClarity);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }

        #endregion

        #region Events
        private void FrmCountryMaster_Load(object sender, EventArgs e)
        {
            try
            {
                m_dtbType = new DataTable();
                m_dtbType.Columns.Add("type");
                m_dtbType.Rows.Add("NORMAL");
                m_dtbType.Rows.Add("REJECTION");

                lueType.Properties.DataSource = m_dtbType;
                lueType.Properties.ValueMember = "type";
                lueType.Properties.DisplayMember = "type";

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
                txtRoughClarity.Text = "";
                txtRemark.Text = "";
                txtStage.Text = "";
                txtSequenceNo.Text = "";
                lueType.EditValue = null;
                chkActive.Checked = true;
                txtRoughClarity.Focus();
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
        private void dgvRoughClarityMaster_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvRoughClarityMaster.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt32(Drow["rough_clarity_id"]);
                        txtRoughClarity.Text = Val.ToString(Drow["rough_clarity_name"]);
                        lueType.EditValue = Val.ToString(Drow["type"]);
                        txtRemark.Text = Val.ToString(Drow["remarks"]);
                        txtStage.Text = Val.ToString(Drow["stage"]);
                        txtSequenceNo.Text = Val.ToString(Drow["sequence_no"]);
                        chkActive.Checked = Val.ToBoolean(Drow["active"]);
                        txtRoughClarity.Focus();
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
            MfgRoughClarity_MasterProperty MFGRoughClarityMasterProperty = new MfgRoughClarity_MasterProperty();
            MfgRoughClarityMaster objRoughClarity = new MfgRoughClarityMaster();

            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }

                MFGRoughClarityMasterProperty.rough_clarity_id = Val.ToInt32(lblMode.Tag);
                MFGRoughClarityMasterProperty.rough_clarity_name = txtRoughClarity.Text.ToUpper();
                MFGRoughClarityMasterProperty.type = Val.ToString(lueType.EditValue);
                MFGRoughClarityMasterProperty.active = Val.ToBoolean(chkActive.Checked);
                MFGRoughClarityMasterProperty.sequence_no = Val.ToInt32(txtSequenceNo.Text);
                MFGRoughClarityMasterProperty.remarks = txtRemark.Text.ToUpper();
                MFGRoughClarityMasterProperty.stage = Val.ToString(txtStage.Text);

                int IntRes = objRoughClarity.Save(MFGRoughClarityMasterProperty);
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Save Rough Clarity Details");
                    txtRoughClarity.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Rough Clarity Details Data Save Successfully");
                    }
                    else
                    {
                        Global.Confirm("Rough Clarity Details Data Update Successfully");
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
                MFGRoughClarityMasterProperty = null;
            }

            return blnReturn;
        }
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (txtRoughClarity.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Rough Clarity Name"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtRoughClarity.Focus();
                    }
                }

                //if (!objRoughClarity.ISExists(txtRoughClarity.Text, Val.ToInt64(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
                //{
                //    lstError.Add(new ListError(23, "Rough Clarity Name"));
                //    if (!blnFocus)
                //    {
                //        blnFocus = true;
                //        txtRoughClarity.Focus();
                //    }
                //}

                if (txtStage.Text == string.Empty || txtStage.Text == "0")
                {
                    lstError.Add(new ListError(12, "Stage"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtStage.Focus();
                    }
                }
                if (txtSequenceNo.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Sequence No"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtSequenceNo.Focus();
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
                DataTable DTab = objRoughClarity.GetData();
                grdRoughClarityMaster.DataSource = DTab;
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
                            dgvRoughClarityMaster.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvRoughClarityMaster.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvRoughClarityMaster.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvRoughClarityMaster.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvRoughClarityMaster.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvRoughClarityMaster.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvRoughClarityMaster.ExportToCsv(Filepath);
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
