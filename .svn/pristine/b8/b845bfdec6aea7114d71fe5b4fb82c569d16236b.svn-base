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
    public partial class FrmMfgRoughCutTypeMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        FormEvents objBOFormEvents;
        Validation Val;
        BLL.FormPer ObjPer;

        MfgRoughCutTypeMaster objRoughCutType;
        #endregion

        #region Constructor
        public FrmMfgRoughCutTypeMaster()
        {
            InitializeComponent();

            objBOFormEvents = new FormEvents();
            Val = new Validation();
            ObjPer = new BLL.FormPer();

            objRoughCutType = new MfgRoughCutTypeMaster();
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
            objBOFormEvents.ObjToDispose.Add(objRoughCutType);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion

        #region Events
        private void FrmMfgRoughCutTypeMaster_Load(object sender, EventArgs e)
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
                txtRoughCutType.Text = string.Empty;
                txtRemark.Text = string.Empty;
                chkActive.Checked = true;
                txtRoughCutType.Focus();
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
        private void dgvMfgRoughCutTypeMaster_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvRoughCutTypeMaster.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt64(Drow["rough_cuttype_id"]);
                        txtRoughCutType.Text = Val.ToString(Drow["rough_cuttype_name"]);
                        txtRemark.Text = Val.ToString(Drow["remarks"]);
                        chkActive.Checked = Val.ToBoolean(Drow["active"]);
                        txtRoughCutType.Focus();
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
            MfgRoughCutType_MasterProperty RoughCutTypeMasterProperty = new MfgRoughCutType_MasterProperty();

            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }
                RoughCutTypeMasterProperty.rough_cuttype_id = Val.ToInt32(lblMode.Tag);
                RoughCutTypeMasterProperty.rough_cuttype_name = txtRoughCutType.Text.ToUpper();
                RoughCutTypeMasterProperty.remarks = txtRemark.Text.ToUpper();
                RoughCutTypeMasterProperty.active = Val.ToBoolean(chkActive.Checked);

                int IntRes = objRoughCutType.Save(RoughCutTypeMasterProperty);
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Save Rough Cuttype Details");
                    txtRoughCutType.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Rough Cuttype Details Data Successfully");
                    }
                    else
                    {
                        Global.Confirm("Rough Cuttype Details  Update Successfully");
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
                RoughCutTypeMasterProperty = null;
            }

            return blnReturn;
        }
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (txtRoughCutType.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Rough Cut Type"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtRoughCutType.Focus();
                    }
                }


                if (!objRoughCutType.ISExists(Val.ToString(txtRoughCutType.Text), Val.ToInt(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
                {
                    lstError.Add(new ListError(23, "Rough Cut Type"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtRoughCutType.Focus();
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
                DataTable DTab = objRoughCutType.GetData();
                grdRoughCutTypeMaster.DataSource = DTab;
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
                            dgvRoughCutTypeMaster.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvRoughCutTypeMaster.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvRoughCutTypeMaster.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvRoughCutTypeMaster.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvRoughCutTypeMaster.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvRoughCutTypeMaster.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvRoughCutTypeMaster.ExportToCsv(Filepath);
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
