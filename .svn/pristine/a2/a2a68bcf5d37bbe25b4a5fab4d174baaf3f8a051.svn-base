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
    public partial class FrmStorePartyMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.FormEvents objBOFormEvents = new BLL.FormEvents();

        BLL.Validation Val = new BLL.Validation();
        CountryMaster objCountry = new CountryMaster();
        StateMaster objState = new StateMaster();
        CityMaster objCity = new CityMaster();
        CompanyMaster objCompany = new CompanyMaster();
        MfgItemMaster ObjMfgItem = new MfgItemMaster();
        StorePartyMaster ObjStoreParty = new StorePartyMaster();

        #endregion

        #region Constructor

        public FrmStorePartyMaster()
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
            txtPartyName.Text = "";
            txtAddress.Text = "";
            txtMobile.Text = "";
            txtContactPerson.Text = "";
            luePartyGroup.EditValue = null;
            txtCSTNo.Text = "";
            txtPANNo.Text = "";
            txtGSTNo.Text = "";
            txtPartyName.Focus();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValSave() == false)
            {
                return;
            }

            StorePartyMasterProperty StorePartyMasterProperty = new StorePartyMasterProperty();

            StorePartyMasterProperty.party_id = Val.ToInt64(lblMode.Tag);
            StorePartyMasterProperty.party_name = txtPartyName.Text;
            StorePartyMasterProperty.mobile_no = Val.ToString(txtMobile.Text);
            StorePartyMasterProperty.address = Val.ToString(txtAddress.Text);
            StorePartyMasterProperty.contect_person = Val.ToString(txtContactPerson.Text);
            StorePartyMasterProperty.cst_no = Val.ToString(txtCSTNo.Text);
            StorePartyMasterProperty.gst_no = Val.ToString(txtGSTNo.Text);
            StorePartyMasterProperty.pan_no = Val.ToString(txtPANNo.Text);
            StorePartyMasterProperty.party_group_id = Val.ToInt64(luePartyGroup.EditValue);

            int IntRes = ObjStoreParty.SaveStoreManager(StorePartyMasterProperty);

            StorePartyMasterProperty = null;
            if (IntRes != 0)
            {
                if (Val.ToInt(lblMode.Tag) == 0)
                {
                    Global.Confirm("Party Master Save Data Successfully");
                }
                else
                {
                    Global.Confirm("Party Master Update Successfully");
                }
                btnClear_Click(sender, e);
                GetData();
            }
            else
            {
                Global.Message("Erro In Party Master Save");
            }
        }
        private void FrmStorePartyMaster_Shown(object sender, EventArgs e)
        {
            Global.LOOKUPPartyGroup(luePartyGroup);

            GetData();

            txtPartyName.Focus();
        }
        private void txtMobile_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        #region GridEvents  

        private void GrdDetPartyMaster_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (e.Clicks == 2)
                {
                    DataRow DRow = GrdDetPartyMaster.GetDataRow(e.RowHandle);

                    lblMode.Text = "Edit Mode";
                    lblMode.Tag = Val.ToInt64(DRow["party_id"]);
                    txtPartyName.Text = Val.ToString(DRow["party_name"]);
                    txtMobile.Text = Val.ToString(DRow["mobile_no"]);
                    txtAddress.Text = Val.ToString(DRow["address"]);
                    txtContactPerson.Text = Val.ToString(DRow["contect_person"]);
                    luePartyGroup.EditValue = Val.ToInt32(DRow["party_group_id"]);
                    txtCSTNo.Text = Val.ToString(DRow["cst_no"]);
                    txtGSTNo.Text = Val.ToString(DRow["gst_no"]);
                    txtPANNo.Text = Val.ToString(DRow["pan_no"]);

                    txtPartyName.Focus();
                }
            }
        }

        #endregion

        #endregion

        #region Functions

        private void GetData()
        {
            ObjStoreParty.GetData();
            MainGrdPartyMaster.DataSource = ObjStoreParty.DTab;
            MainGrdPartyMaster.RefreshDataSource();
            GrdDetPartyMaster.BestFitColumns();
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
                            GrdDetPartyMaster.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            GrdDetPartyMaster.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            GrdDetPartyMaster.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            GrdDetPartyMaster.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            GrdDetPartyMaster.ExportToText(Filepath);
                            break;
                        case "html":
                            GrdDetPartyMaster.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            GrdDetPartyMaster.ExportToCsv(Filepath);
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
            if (txtPartyName.Text.Length == 0)
            {
                Global.Confirm("Party Name Is Required");
                txtPartyName.Focus();
                return false;
            }
            if (!ObjStoreParty.ISExists(txtPartyName.Text, Val.ToInt(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
            {
                Global.Confirm("Party Name Already Exist.");
                txtPartyName.Focus();
                txtPartyName.SelectAll();
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
