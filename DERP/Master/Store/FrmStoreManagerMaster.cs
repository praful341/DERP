using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Master.Store;
using BLL.PropertyClasses.Master.Store;
using DERP.Class;
using System;
using System.Data;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Master.Store
{
    public partial class FrmStoreManagerMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.FormEvents objBOFormEvents = new BLL.FormEvents();

        BLL.Validation Val = new BLL.Validation();
        CountryMaster objCountry = new CountryMaster();
        StateMaster objState = new StateMaster();
        CityMaster objCity = new CityMaster();
        CompanyMaster objCompany = new CompanyMaster();
        MfgItemMaster ObjMfgItem = new MfgItemMaster();
        StoreManagerMaster ObjStoreManager = new StoreManagerMaster();

        #endregion

        #region Constructor

        public FrmStoreManagerMaster()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.frmGenSet(this);
            AttachFormEvents();
            this.Show();
            GetData();
            btnClear_Click(btnClear, null);
        }

        private void AttachFormEvents()
        {
            objBOFormEvents.CurForm = this;
            objBOFormEvents.FormKeyPress = true;
            objBOFormEvents.FormKeyDown = true;
            objBOFormEvents.FormResize = true;
            objBOFormEvents.FormClosing = true;
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }

        #endregion

        #region Events

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            lblMode.Tag = 0;
            lblMode.Text = "Add Mode";
            txtManagerName.Text = "";
            txtAddress.Text = "";
            txtMobile.Text = "";
            lueCity.EditValue = null;
            lueState.EditValue = null;
            lueCompany.EditValue = null;
            lueDepartment.EditValue = null;
            lueLocation.EditValue = null;
            txtSalary.Text = string.Empty;
            txtManagerName.Focus();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValSave() == false)
            {
                return;
            }

            StoreManagerMasterProperty StoreManagerMasterProperty = new StoreManagerMasterProperty();

            StoreManagerMasterProperty.manager_id = Val.ToInt64(lblMode.Tag);
            StoreManagerMasterProperty.manager_name = txtManagerName.Text;
            StoreManagerMasterProperty.mobile_no = Val.ToString(txtMobile.Text);
            StoreManagerMasterProperty.address = Val.ToString(txtAddress.Text);
            StoreManagerMasterProperty.salary = Val.ToDecimal(txtSalary.Text);
            StoreManagerMasterProperty.city_id = Val.ToInt64(lueCity.EditValue);
            StoreManagerMasterProperty.state_id = Val.ToInt64(lueState.EditValue);
            StoreManagerMasterProperty.company_id = Val.ToInt64(lueCompany.EditValue);
            StoreManagerMasterProperty.location_id = Val.ToInt64(lueLocation.EditValue);
            StoreManagerMasterProperty.department_id = Val.ToInt64(lueDepartment.EditValue);

            int IntRes = ObjStoreManager.SaveStoreManager(StoreManagerMasterProperty);

            StoreManagerMasterProperty = null;
            if (IntRes != 0)
            {
                if (Val.ToInt(lblMode.Tag) == 0)
                {
                    Global.Confirm("Manager Save Data Successfully");
                }
                else
                {
                    Global.Confirm("Manager Update Successfully");
                }
                btnClear_Click(sender, e);
                GetData();
            }
            else
            {
                Global.Message("Erro In Manager Save");
            }
        }
        private void FrmStoreManagerMaster_Shown(object sender, EventArgs e)
        {
            Global.LOOKUPCompany(lueCompany);
            Global.LOOKUPLocation(lueLocation);
            Global.LOOKUPDepartment(lueDepartment);
            Global.LOOKUPState(lueState);
            Global.LOOKUPCity(lueCity);

            GetData();

            txtManagerName.Focus();
        }
        private void txtMobile_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        #region GridEvents  

        private void GrdDetManagerMaster_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (e.Clicks == 2)
                {
                    DataRow DRow = GrdDetManagerMaster.GetDataRow(e.RowHandle);

                    lblMode.Text = "Edit Mode";
                    lblMode.Tag = Val.ToInt64(DRow["manager_id"]);
                    txtManagerName.Text = Val.ToString(DRow["manager_name"]);
                    txtMobile.Text = Val.ToString(DRow["mobile_no"]);
                    txtAddress.Text = Val.ToString(DRow["address"]);
                    txtSalary.Text = Val.ToString(DRow["salary"]);

                    lueCity.EditValue = Val.ToInt32(DRow["city_id"]);
                    lueState.EditValue = Val.ToInt32(DRow["state_id"]);
                    lueCompany.EditValue = Val.ToInt32(DRow["company_id"]);
                    lueDepartment.EditValue = Val.ToInt32(DRow["department_id"]);
                    lueLocation.EditValue = Val.ToInt32(DRow["location_id"]);

                    txtManagerName.Focus();
                }
            }
        }

        #endregion

        #endregion

        #region Functions

        private void GetData()
        {
            ObjStoreManager.GetData();
            MainGrdManagerMaster.DataSource = ObjStoreManager.DTab;
            MainGrdManagerMaster.RefreshDataSource();
            GrdDetManagerMaster.BestFitColumns();
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
                            GrdDetManagerMaster.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            GrdDetManagerMaster.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            GrdDetManagerMaster.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            GrdDetManagerMaster.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            GrdDetManagerMaster.ExportToText(Filepath);
                            break;
                        case "html":
                            GrdDetManagerMaster.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            GrdDetManagerMaster.ExportToCsv(Filepath);
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

        #region Validation
        private bool ValSave()
        {
            if (txtManagerName.Text.Length == 0)
            {
                Global.Confirm("Manager Name Is Required");
                txtManagerName.Focus();
                return false;
            }
            if (!ObjStoreManager.ISExists(txtManagerName.Text, Val.ToInt(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
            {
                Global.Confirm("Manager Name Already Exist.");
                txtManagerName.Focus();
                txtManagerName.SelectAll();
                return false;
            }

            return true;
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
