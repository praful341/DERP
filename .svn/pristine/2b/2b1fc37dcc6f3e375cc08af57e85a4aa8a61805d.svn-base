using BLL;
using BLL.FunctionClasses.Master;
using BLL.PropertyClasses.Master;
using DERP.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DREP.Master
{
    public partial class FrmCutMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        FormEvents objBOFormEvents;
        Validation Val;
        BLL.FormPer ObjPer;

        CutMaster objCut;

        #endregion

        #region Constructor

        public FrmCutMaster()
        {
            InitializeComponent();

            objBOFormEvents = new FormEvents();
            Val = new Validation();
            ObjPer = new BLL.FormPer();

            objCut = new CutMaster();
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
            objBOFormEvents.ObjToDispose.Add(objCut);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }

        #endregion

        #region Events
        private void FrmCutMaster_Load(object sender, EventArgs e)
        {
            try
            {
                GetData();
                btnClear_Click(btnClear, null);
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
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
            try
            {
                lblMode.Tag = 0;
                lblMode.Text = "Add Mode";
                txtCutName.Text = "";
                txtRemarks.Text = "";
                txtShortName.Text = "";
                txtSequenceNo.Text = "";
                chkActive.Checked = true;
                txtShortName.Focus();
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
        private void dgvCutMaster_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvCutMaster.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt32(Drow["cut_id"]);
                        txtCutName.Text = Val.ToString(Drow["cut_name"]);
                        txtShortName.Text = Val.ToString(Drow["cut_shortname"]);
                        txtRemarks.Text = Val.ToString(Drow["remarks"]);
                        txtSequenceNo.Text = Val.ToString(Drow["sequence_no"]);
                        chkActive.Checked = Val.ToBoolean(Drow["active"]);
                        txtCutName.Focus();
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

        #region Functions
        private bool SaveDetails()
        {
            bool blnReturn = true;
            Cut_MasterProperty CutMasterProperty = new Cut_MasterProperty();
            CutMaster objCut = new CutMaster();

            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }

                CutMasterProperty.cut_id = Val.ToInt32(lblMode.Tag);
                CutMasterProperty.cut_name = txtCutName.Text.ToUpper();
                CutMasterProperty.cut_shortname = txtShortName.Text.ToUpper();
                CutMasterProperty.active = Val.ToBoolean(chkActive.Checked);
                CutMasterProperty.remarks = txtRemarks.Text.ToUpper();
                CutMasterProperty.sequence_no = Val.ToInt(txtSequenceNo.Text);

                int IntRes = objCut.Save(CutMasterProperty);
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Save Cut Details");
                    txtShortName.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Cut Details Data Save Successfully");
                    }
                    else
                    {
                        Global.Confirm("Cut Details Data Update Successfully");
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
                CutMasterProperty = null;
            }

            return blnReturn;
        }
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (txtShortName.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Cut Short Name"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtShortName.Focus();
                    }
                }

                if (txtCutName.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Cut Name"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtCutName.Focus();
                    }
                }

                if (!objCut.ISExists(txtCutName.Text, Val.ToInt64(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
                {
                    lstError.Add(new ListError(23, "Cut Name"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtCutName.Focus();
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
                DataTable DTab = objCut.GetData();
                grdCutMaster.DataSource = DTab;
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
                            dgvCutMaster.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvCutMaster.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvCutMaster.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvCutMaster.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvCutMaster.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvCutMaster.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvCutMaster.ExportToCsv(Filepath);
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
