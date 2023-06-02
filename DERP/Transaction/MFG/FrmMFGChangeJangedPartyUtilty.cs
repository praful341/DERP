using BLL;
using BLL.FunctionClasses.Transaction.MFG;
using DERP.Class;
using System;
using System.Data;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Transaction
{
    public partial class FrmMFGChangeJangedPartyUtilty : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        //BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        MfgDepartmentTransferConfirm objDepartmentCnf;
        DataTable m_dtbDetails;
        DataTable m_dtbDetailsGetData;
        int m_numForm_id;       
        DataTable m_dtbParam;
        #endregion

        #region Constructor
        public FrmMFGChangeJangedPartyUtilty()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            objDepartmentCnf = new MfgDepartmentTransferConfirm();
            m_dtbDetails = new DataTable();
            m_dtbParam = new DataTable();
            m_dtbDetailsGetData = new DataTable();
            m_numForm_id = 0;           
        }

        public void ShowForm()
        {
            ObjPer.FormName = this.Name.ToUpper();
            m_numForm_id = ObjPer.form_id;
            if (ObjPer.CheckPermission() == false)
            {
                Global.Message(BLL.GlobalDec.gStrPermissionViwMsg);
                return;
            }
            if (Global.CheckDefault() == 0)
            {
                Global.Message("Please Check User Default Setting");
                this.Close();
                return;
            }
            if (GlobalDec.gEmployeeProperty.user_name != "KAILASH" & GlobalDec.gEmployeeProperty.user_name != "PRIYANKA" & GlobalDec.gEmployeeProperty.user_name != "PRAFUL")
            {
                Global.Message("Don't have permission...Please Contact to Administrator...");
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
            objBOFormEvents.ObjToDispose.Add(objDepartmentCnf);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion   

        #region Events           
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region GridEvents

        private void dgvDepartmentTransferConfirm_ColumnFilterChanged(object sender, EventArgs e)
        {
            if (dgvChangedJangedPartyUtility.ActiveFilterString != "")
            {
                //filterFlag = 1;
                //chkAll.Checked = false;
                //filterFlag = 0;
            }
            else
            {
                //filterFlag = 1;
                //chkAll.Checked = false;
                //filterFlag = 0;
            }
        }
        #endregion

        #endregion

        #region Functions
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
                            dgvChangedJangedPartyUtility.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvChangedJangedPartyUtility.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvChangedJangedPartyUtility.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvChangedJangedPartyUtility.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvChangedJangedPartyUtility.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvChangedJangedPartyUtility.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvChangedJangedPartyUtility.ExportToCsv(Filepath);
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

        private void btnShow_Click(object sender, EventArgs e)
        {
            DataTable DTab_ValidateLotID = new DataTable();
            if (txtLotID.Text != "")
            {
                DTab_ValidateLotID = objDepartmentCnf.Changed_Janged_Party_GetData(Val.ToInt64(txtLotID.Text));
            }
            else
            {
                DTab_ValidateLotID = objDepartmentCnf.Changed_Janged_Party_GetData(Val.ToInt32(0));
            }

            grdChangedJangedPartyUtility.DataSource = DTab_ValidateLotID;
            grdChangedJangedPartyUtility.RefreshDataSource();
            dgvChangedJangedPartyUtility.BestFitColumns();
        }
        private void txtLotID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtLotID.Text = "0";
            grdChangedJangedPartyUtility.DataSource = null;
            txtLotID.Focus();
        }

        private void FrmMFGChangeJangedPartyUtilty_Load(object sender, EventArgs e)
        {
            txtLotID.Focus();
        }

        private void txtLotID_Validated(object sender, EventArgs e)
        {
            DataTable DTab_ValidateLotID = new DataTable();
            if (txtLotID.Text != "")
            {
                DTab_ValidateLotID = objDepartmentCnf.Changed_Janged_Party_GetData(Val.ToInt64(txtLotID.Text));
            }
            else
            {
                DTab_ValidateLotID = objDepartmentCnf.Changed_Janged_Party_GetData(Val.ToInt32(0));
            }

            grdChangedJangedPartyUtility.DataSource = DTab_ValidateLotID;
            grdChangedJangedPartyUtility.RefreshDataSource();
            dgvChangedJangedPartyUtility.BestFitColumns();
        }
    }
}
