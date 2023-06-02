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
    public partial class FrmMfgRoughSieve : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        FormEvents objBOFormEvents;
        Validation Val;
        BLL.FormPer ObjPer;

        MfgRoughSieve objRoughSieve;
        DataTable m_dtbType;
        #endregion

        #region Constructor
        public FrmMfgRoughSieve()
        {
            InitializeComponent();

            objBOFormEvents = new FormEvents();
            Val = new Validation();
            ObjPer = new BLL.FormPer();

            objRoughSieve = new MfgRoughSieve();
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
            objBOFormEvents.ObjToDispose.Add(objRoughSieve);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion

        #region Events     
        private void FrmMfgRoughSieve_Load(object sender, EventArgs e)
        {
            try
            {
                m_dtbType = new DataTable();
                m_dtbType.Columns.Add("type");
                m_dtbType.Rows.Add("CHARNI");
                m_dtbType.Rows.Add("SIEVE");

                lueType.Properties.DataSource = m_dtbType;
                lueType.Properties.ValueMember = "type";
                lueType.Properties.DisplayMember = "type";
                lueType.EditValue = "CHARNI";
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
                txtRoughSieveName.Text = "";
                txtRemark.Text = "";
                txtSequenceNo.Text = "";
                chkActive.Checked = true;
                lueType.EditValue = "CHARNI";
                txtRoughSieveName.Focus();
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
        private void dgvRoughSieveMaster_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvRoughSieveMaster.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt32(Drow["rough_sieve_id"]);
                        txtRoughSieveName.Text = Val.ToString(Drow["sieve_name"]);
                        txtRemark.Text = Val.ToString(Drow["remarks"]);
                        txtSequenceNo.Text = Val.ToString(Drow["sequence_no"]);
                        chkActive.Checked = Val.ToBoolean(Drow["active"]);
                        lueType.EditValue = Val.ToString(Drow["type"]);
                        txtRoughSieveName.Focus();
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
            MfgRough_SieveProperty RoughSieveProperty = new MfgRough_SieveProperty();
            //AssortMaster objAssort = new AssortMaster();
            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }
                List<ListError> lstError = new List<ListError>();


                RoughSieveProperty.rough_sieve_id = Val.ToInt32(lblMode.Tag);
                RoughSieveProperty.sieve_name = txtRoughSieveName.Text.ToUpper();
                RoughSieveProperty.active = Val.ToBoolean(chkActive.Checked);
                RoughSieveProperty.remarks = txtRemark.Text.ToUpper();
                RoughSieveProperty.sequence_no = Val.ToInt(txtSequenceNo.Text);
                RoughSieveProperty.type = Val.ToString(lueType.EditValue);

                int IntRes = objRoughSieve.Save(RoughSieveProperty);
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Save Rough Sieve Details");
                    txtRoughSieveName.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Rough Sieve Details Data Save Successfully");
                    }
                    else
                    {
                        Global.Confirm("Rough Sieve Details Data Update Successfully");
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
                RoughSieveProperty = null;
            }

            return blnReturn;
        }
        private bool ValidateDetails()
        {

            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (txtRoughSieveName.Text.Length == 0)
                {
                    lstError.Add(new ListError(12, "Sieve"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtRoughSieveName.Focus();
                    }
                }
                if (txtSequenceNo.Text.Length == 0)
                {
                    lstError.Add(new ListError(12, "Sequence No"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtSequenceNo.Focus();
                    }
                }
                if (!objRoughSieve.ISExists(txtRoughSieveName.Text, Val.ToInt64(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
                {
                    lstError.Add(new ListError(23, "Rough Sieve"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtRoughSieveName.Focus();
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
                DataTable DTab = objRoughSieve.GetData();
                grdRoughSieveMaster.DataSource = DTab;
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
                            dgvRoughSieveMaster.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvRoughSieveMaster.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvRoughSieveMaster.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvRoughSieveMaster.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvRoughSieveMaster.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvRoughSieveMaster.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvRoughSieveMaster.ExportToCsv(Filepath);
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
