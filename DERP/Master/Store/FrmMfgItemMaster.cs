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
    public partial class FrmMfgItemMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.FormEvents objBOFormEvents = new BLL.FormEvents();

        BLL.Validation Val = new BLL.Validation();
        CountryMaster objCountry = new CountryMaster();
        StateMaster objState = new StateMaster();
        CityMaster objCity = new CityMaster();
        CompanyMaster objCompany = new CompanyMaster();
        MfgItemMaster ObjMfgItem = new MfgItemMaster();

        #endregion

        #region Constructor

        public FrmMfgItemMaster()
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
            txtItemName.Text = "";
            txtRemark.Text = "";
            txtSequenceNo.Text = "";
            LookUpItemType.EditValue = null;

            LookUpUnit.EditValue = null;
            RbtStatus.SelectedIndex = 0;
            dtpOpeningDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            dtpOpeningDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            dtpOpeningDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            dtpOpeningDate.Properties.CharacterCasing = CharacterCasing.Upper;
            dtpOpeningDate.EditValue = DateTime.Now;
            txtModelNo.Text = "";
            txtItemCode.Text = "";
            txtWarrantyYear.Text = "";
            txtWarrantyMonth.Text = "";
            txtTon.Text = "";
            txtType.Text = "";
            txtCompany.Text = "";

            txtItemCode.Focus();
            txtRate.Text = string.Empty;
            txtOpeningQty.Text = string.Empty;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValSave() == false)
            {
                return;
            }

            MfgItem_MasterProperty MfgItemMasterProperty = new MfgItem_MasterProperty();


            MfgItemMasterProperty.item_id = Val.ToInt64(lblMode.Tag);
            MfgItemMasterProperty.item_name = txtItemName.Text;
            MfgItemMasterProperty.item_type_id = Val.ToInt64(LookUpItemType.EditValue);
            MfgItemMasterProperty.opening_date = Val.ToString(dtpOpeningDate.Text);
            MfgItemMasterProperty.warranty_year = Val.ToInt32(txtWarrantyYear.Text);
            MfgItemMasterProperty.warranty_month = Val.ToInt32(txtWarrantyMonth.Text);
            MfgItemMasterProperty.model_no = Val.ToString(txtModelNo.Text);
            MfgItemMasterProperty.item_code = Val.ToString(txtItemCode.Text);
            MfgItemMasterProperty.ton = Val.ToDecimal(txtTon.Text);
            MfgItemMasterProperty.company_name = Val.ToString(txtCompany.Text);
            MfgItemMasterProperty.type = Val.ToString(LookUpItemType.Text);
            MfgItemMasterProperty.active = Val.ToInt(RbtStatus.Text);
            MfgItemMasterProperty.remark = txtRemark.Text;
            MfgItemMasterProperty.sequence_no = Val.ToInt(txtSequenceNo.Text);

            MfgItemMasterProperty.unit_id = Val.ToInt64(LookUpUnit.EditValue);

            MfgItemMasterProperty.opening_qty = Val.ToInt64(txtOpeningQty.Text);
            MfgItemMasterProperty.rate = Val.ToDecimal(txtRate.Text);

            int IntRes = ObjMfgItem.SaveItem(MfgItemMasterProperty);

            MfgItemMasterProperty = null;
            if (IntRes != 0)
            {
                if (Val.ToInt(lblMode.Tag) == 0)
                {
                    Global.Confirm("Item Save Data Successfully");
                }
                else
                {
                    Global.Confirm("Item Update Successfully");
                }
                btnClear_Click(sender, e);
                GetData();
            }
            else
            {
                Global.Message("Erro In Item Save");
            }
        }
        private void FrmItemMaster_Shown(object sender, EventArgs e)
        {
            GetData();

            Global.LOOKUPItemType(LookUpItemType);
            Global.LOOKUPUnit(LookUpUnit);
            btnClear_Click(null, null);
            txtItemCode.Focus();
        }
        private void LookUpItemType_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmMfgItemTypeMaster frmCnt = new FrmMfgItemTypeMaster();
                frmCnt.ShowDialog();
                Global.LOOKUPItemType(LookUpItemType);
            }
        }
        private void LookUpUnit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmMfgUnitMaster frmCnt = new FrmMfgUnitMaster();
                frmCnt.ShowDialog();
                Global.LOOKUPUnit(LookUpUnit);
            }
        }

        #region GridEvents  

        private void GrdDetItemMaster_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (e.Clicks == 2)
                {
                    DataRow DRow = GrdDetItemMaster.GetDataRow(e.RowHandle);

                    lblMode.Text = "Edit Mode";
                    lblMode.Tag = Val.ToInt64(DRow["item_id"]);
                    txtItemName.Text = Val.ToString(DRow["item_name"]);
                    LookUpItemType.EditValue = Val.ToInt32(DRow["item_type_id"]);
                    LookUpUnit.EditValue = Val.ToInt32(DRow["unit_id"]);
                    RbtStatus.EditValue = Convert.ToInt32(DRow["active"]);
                    txtRemark.Text = Val.ToString(DRow["remark"]);
                    dtpOpeningDate.Text = Val.ToString(DRow["opening_date"]);
                    txtModelNo.Text = Val.ToString(DRow["model_no"]);
                    txtItemCode.Text = Val.ToString(DRow["item_code"]);
                    txtWarrantyYear.Text = Val.ToString(DRow["warranty_year"]);
                    txtWarrantyMonth.Text = Val.ToString(DRow["warranty_month"]);
                    txtTon.Text = Val.ToString(DRow["ton"]);
                    txtType.Text = Val.ToString(DRow["type"]);
                    txtCompany.Text = Val.ToString(DRow["company_name"]);
                    txtRate.Text = Val.ToString(DRow["rate"]);
                    txtOpeningQty.Text = Val.ToString(DRow["opening_quantity"]);
                    txtSequenceNo.Text = Val.ToString(DRow["sequence_no"]);
                    txtItemCode.Focus();
                }
            }
        }

        #endregion

        #endregion

        #region Functions

        private void GetData()
        {
            ObjMfgItem.GetData();
            MainGrdItemMaster.DataSource = ObjMfgItem.DTab;
            MainGrdItemMaster.RefreshDataSource();
            GrdDetItemMaster.BestFitColumns();
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
                            GrdDetItemMaster.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            GrdDetItemMaster.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            GrdDetItemMaster.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            GrdDetItemMaster.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            GrdDetItemMaster.ExportToText(Filepath);
                            break;
                        case "html":
                            GrdDetItemMaster.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            GrdDetItemMaster.ExportToCsv(Filepath);
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
            if (txtItemName.Text.Length == 0)
            {
                Global.Confirm("Item Name Is Required");
                txtItemName.Focus();
                return false;
            }
            if (!ObjMfgItem.ISExists(txtItemName.Text, Val.ToInt(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
            {
                Global.Confirm("Item Name Already Exist.");
                txtItemName.Focus();
                txtItemName.SelectAll();
                return false;
            }

            if (LookUpItemType.EditValue == null)
            {
                Global.Confirm("Item Type Name Is Required");
                LookUpItemType.Focus();
                return false;
            }

            if (LookUpUnit.EditValue == null)
            {
                Global.Confirm("Unit Name Is Required");
                LookUpUnit.Focus();
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
