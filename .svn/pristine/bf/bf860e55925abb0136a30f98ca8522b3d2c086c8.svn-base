using BLL;
using BLL.FunctionClasses.Master.HR;
using BLL.PropertyClasses.Master.HR;
using DERP.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DREP.Master.HR
{
    public partial class FrmHRFactoryMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member        

        BLL.FormEvents objBOFormEvents = new BLL.FormEvents();
        BLL.Validation Val = new BLL.Validation();
        BLL.FormPer ObjPer = new BLL.FormPer();
        HRFactoryMaster objHRFactory = new HRFactoryMaster();

        #endregion

        #region Constructor
        public FrmHRFactoryMaster()
        {
            InitializeComponent();
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
            objBOFormEvents.ObjToDispose.Add(objHRFactory);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }

        #endregion

        #region Events
        private void FrmHRFactoryMaster_Load(object sender, EventArgs e)
        {
            try
            {
                GetData();
                btnClear_Click(btnClear, null);
                txtFactoryName.Focus();
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
                txtFactoryName.Text = "";
                txtFactoryShortName.Text = "";
                chkActive.Checked = true;
                txtFactoryName.Focus();
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

        private void dgvFactoryMaster_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvFactoryMaster.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt64(Drow["factory_id"]);
                        txtFactoryName.Text = Val.ToString(Drow["factory_name"]);
                        txtFactoryShortName.Text = Val.ToString(Drow["factory_short_name"]);
                        chkActive.Checked = Val.ToBoolean(Drow["active"]);
                        txtFactoryName.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void dgvEmployeeMaster_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                string StrCategory = Val.ToString(dgvFactoryMaster.GetRowCellDisplayText(e.RowHandle, dgvFactoryMaster.Columns["active"]));

                if (StrCategory == "Unchecked")
                {
                    e.Appearance.BackColor = BLL.GlobalDec.ABColor;
                    e.Appearance.BackColor2 = BLL.GlobalDec.ABColor2;
                    //e.Appearance.BackColor2 = Color.White;
                }
                else if (StrCategory == "Checked")
                {
                    e.Appearance.BackColor = Color.Transparent;
                    e.Appearance.BackColor2 = Color.Transparent;
                }
            }
        }
        #endregion

        #endregion

        #region Functions

        private bool SaveDetails()
        {
            bool blnReturn = true;
            HRFactory_MasterProperty HRFactoryMasterProperty = new HRFactory_MasterProperty();
            HRFactoryMaster objHRFactory = new HRFactoryMaster();

            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }

                HRFactoryMasterProperty.factory_id = Val.ToInt64(lblMode.Tag);
                HRFactoryMasterProperty.factory_name = Val.ToString(txtFactoryName.Text).ToUpper();
                HRFactoryMasterProperty.factory_short_name = Val.ToString(txtFactoryShortName.Text).ToUpper();
                HRFactoryMasterProperty.active = Val.ToBoolean(chkActive.Checked);

                int IntRes = objHRFactory.Save(HRFactoryMasterProperty);
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Save HR Factory Details");
                    txtFactoryName.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("HR Factory Details Data Save Successfully");
                    }
                    else
                    {
                        Global.Confirm("HR Factory Details Data Update Successfully");
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
                HRFactoryMasterProperty = null;
            }

            return blnReturn;
        }

        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (txtFactoryName.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Factory Name"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtFactoryName.Focus();
                    }
                }
                if (txtFactoryShortName.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Factory Short Name"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtFactoryShortName.Focus();
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
            List<ListError> lstError = new List<ListError>();
            try
            {
                DataTable DTab = objHRFactory.GetData();
                grdFactoryMaster.DataSource = DTab;
                dgvFactoryMaster.BestFitColumns();
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
                            dgvFactoryMaster.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvFactoryMaster.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvFactoryMaster.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvFactoryMaster.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvFactoryMaster.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvFactoryMaster.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvFactoryMaster.ExportToCsv(Filepath);
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
            Export("xlsx", "Export to Excel", "Excel files 97-2003 (Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*");
        }
        private void MNExportPDF_Click(object sender, EventArgs e)
        {
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
