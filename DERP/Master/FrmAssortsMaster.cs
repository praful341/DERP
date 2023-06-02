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
    public partial class FrmAssortsMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        FormEvents objBOFormEvents;
        Validation Val;
        BLL.FormPer ObjPer;

        AssortMaster objAssort;
        SieveMaster objSieve;
        CompanyMaster objCompany;
        BranchMaster objBranch;
        LocationMaster objLocation;
        DepartmentMaster objDepartment;
        #endregion

        #region Constructor
        public FrmAssortsMaster()
        {
            InitializeComponent();

            objBOFormEvents = new FormEvents();
            Val = new Validation();
            ObjPer = new BLL.FormPer();

            objAssort = new AssortMaster();
            objSieve = new SieveMaster();
            objCompany = new CompanyMaster();
            objBranch = new BranchMaster();
            objLocation = new LocationMaster();
            objDepartment = new DepartmentMaster();
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
            objBOFormEvents.ObjToDispose.Add(objAssort);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion

        #region Events     
        private void FrmAssortMaster_Load(object sender, EventArgs e)
        {
            try
            {
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
            try
            {
                lblMode.Tag = 0;
                lblMode.Text = "Add Mode";
                txtAssortName.Text = "";
                txtRemark.Text = "";
                txtSequenceNo.Text = "";
                chkActive.Checked = true;
                txtAssortName.Focus();
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region GridEvents
        private void dgvAssortsMaster_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvAssortsMaster.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt32(Drow["assort_id"]);
                        txtAssortName.Text = Val.ToString(Drow["assort_name"]);
                        txtRemark.Text = Val.ToString(Drow["remarks"]);
                        txtSequenceNo.Text = Val.ToString(Drow["sequence_no"]);
                        chkActive.Checked = Val.ToBoolean(Drow["active"]);
                        txtAssortName.Focus();
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
            Assort_MasterProperty AssortMasterProperty = new Assort_MasterProperty();
            AssortMaster objAssort = new AssortMaster();
            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }
                objLocation = new LocationMaster();
                objSieve = new SieveMaster();
                List<ListError> lstError = new List<ListError>();
                DataTable Sieve = new DataTable();
                DataTable Company = new DataTable();
                DataTable Branch = new DataTable();
                DataTable Location = new DataTable();
                DataTable department = new DataTable();
                Location = objLocation.GetLocationWiseCompanyBranch();
                Sieve = objSieve.GetData();
                int assortID = 0;
                int IntRes = 0;
                int locCount = Location.Rows.Count;
                int sieveCount = Sieve.Rows.Count;
                AssortMasterProperty.assort_id = Val.ToInt32(lblMode.Tag);
                AssortMasterProperty.assortname = txtAssortName.Text.ToUpper();
                AssortMasterProperty.active = Val.ToBoolean(chkActive.Checked);
                AssortMasterProperty.remarks = txtRemark.Text.ToUpper();
                AssortMasterProperty.sequence_no = Val.ToInt(txtSequenceNo.Text);
                assortID = objAssort.Save(AssortMasterProperty);
                if (Val.ToInt32(lblMode.Tag) == 0)
                {
                    for (int i = 0; i <= locCount - 1; i++)
                    {
                        for (int j = 0; j <= sieveCount - 1; j++)
                        {
                            AssortMasterProperty.assort_id = Val.ToInt32(assortID);
                            AssortMasterProperty.sieve_id = Val.ToInt32(Sieve.Rows[j]["sieve_id"]);
                            AssortMasterProperty.company_id = Val.ToInt32(Location.Rows[i]["company_id"]);
                            AssortMasterProperty.branch_id = Val.ToInt32(Location.Rows[i]["branch_id"]);
                            AssortMasterProperty.location_id = Val.ToInt32(Location.Rows[i]["location_id"]);
                            AssortMasterProperty.department_id = Val.ToInt32(Location.Rows[i]["department_id"]);
                            IntRes = objAssort.StockSave(AssortMasterProperty);
                        }
                    }
                }

                if (IntRes == -1)
                {
                    Global.Confirm("Error In Save Assort Details");
                    txtAssortName.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Assort Details Data Save Successfully");
                    }
                    else
                    {
                        Global.Confirm("Assort Details Data Update Successfully");
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
                AssortMasterProperty = null;
            }

            return blnReturn;
        }
        private bool ValidateDetails()
        {

            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (txtAssortName.Text.Length == 0)
                {
                    lstError.Add(new ListError(12, "Assort"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtAssortName.Focus();
                    }
                }

                if (!objAssort.ISExists(txtAssortName.Text, Val.ToInt64(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
                {
                    lstError.Add(new ListError(23, "Assort"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtAssortName.Focus();
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
                DataTable DTab = objAssort.GetData();
                grdAssortsMaster.DataSource = DTab;
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
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
                            dgvAssortsMaster.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvAssortsMaster.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvAssortsMaster.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvAssortsMaster.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvAssortsMaster.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvAssortsMaster.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvAssortsMaster.ExportToCsv(Filepath);
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
