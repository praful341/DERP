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
    public partial class FrmMfgSubItemMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.FormEvents objBOFormEvents = new BLL.FormEvents();

        BLL.Validation Val = new BLL.Validation();
        CountryMaster objCountry = new CountryMaster();
        StateMaster objState = new StateMaster();
        CityMaster objCity = new CityMaster();
        CompanyMaster objCompany = new CompanyMaster();
        MfgSubItemMaster ObjMfgSubItem = new MfgSubItemMaster();

        #endregion

        #region Constructor

        public FrmMfgSubItemMaster()
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
            txtSubItem.Text = "";
            lueItem.EditValue = null;
            txtSequenceNo.Text = "";

            dtpOpeningDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            dtpOpeningDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            dtpOpeningDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            dtpOpeningDate.Properties.CharacterCasing = CharacterCasing.Upper;

            dtpOpeningDate.EditValue = "01/Nov/2021";
            txtOpeningRate.Text = string.Empty;
            txtOpeningQty.Text = string.Empty;
            txtOpeningAmount.Text = string.Empty;

            txtModelNo.Text = string.Empty;
            CmbWarrentyYear.SelectedIndex = 0;
            CmbWarrentyMonth.SelectedIndex = 0;

            txtCompany.Text = "";
            txtModelNo.Text = "";
            txtItemCode.Text = "";
            txtTon.Text = "";
            txtType.Text = "";

            lueItem.Focus();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValSave() == false)
            {
                return;
            }

            MfgSubItem_MasterProperty MfgSubItemMasterProperty = new MfgSubItem_MasterProperty();

            MfgSubItemMasterProperty.sub_item_id = Val.ToInt64(lblMode.Tag);
            MfgSubItemMasterProperty.sub_item_name = Val.ToString(txtSubItem.Text);
            MfgSubItemMasterProperty.item_id = Val.ToInt64(lueItem.EditValue);
            MfgSubItemMasterProperty.opening_date = Val.DBDate(dtpOpeningDate.Text);
            MfgSubItemMasterProperty.opening_quantity = Val.ToDecimal(txtOpeningQty.Text);
            MfgSubItemMasterProperty.opening_rate = Val.ToDecimal(txtOpeningRate.Text);
            MfgSubItemMasterProperty.opening_amt = Val.ToDecimal(txtOpeningAmount.Text);
            MfgSubItemMasterProperty.model_no = Val.ToString(txtModelNo.Text);
            MfgSubItemMasterProperty.warranty_year = Val.ToInt32(CmbWarrentyYear.Text);
            MfgSubItemMasterProperty.warranty_month = Val.ToInt32(CmbWarrentyMonth.Text);
            MfgSubItemMasterProperty.company_name = Val.ToString(txtCompany.Text);
            MfgSubItemMasterProperty.item_code = Val.ToString(txtItemCode.Text);
            MfgSubItemMasterProperty.ton = Val.ToDecimal(txtTon.Text);
            MfgSubItemMasterProperty.type = Val.ToString(txtType.Text);
            MfgSubItemMasterProperty.sequence_no = Val.ToInt(txtSequenceNo.Text);

            int IntRes = ObjMfgSubItem.SaveSubItem(MfgSubItemMasterProperty);

            MfgSubItemMasterProperty = null;
            if (IntRes != 0)
            {
                if (Val.ToInt(lblMode.Tag) == 0)
                {
                    Global.Confirm("Sub Item Save Data Successfully");
                }
                else
                {
                    Global.Confirm("Sub Item Update Successfully");
                }
                btnClear_Click(sender, e);
                GetData();
            }
            else
            {
                Global.Message("Erro In Sub Item Save");
            }
        }
        private void FrmItemMaster_Shown(object sender, EventArgs e)
        {
            GetData();

            dtpOpeningDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            dtpOpeningDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            dtpOpeningDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            dtpOpeningDate.Properties.CharacterCasing = CharacterCasing.Upper;
            dtpOpeningDate.EditValue = "01/Nov/2021";

            Global.LOOKUPItem(lueItem);

            lueItem.Focus();
        }
        private void txtOpeningRate_EditValueChanged(object sender, EventArgs e)
        {
            txtOpeningAmount.Text = string.Format("{0:0.00}", Val.ToDecimal(txtOpeningQty.Text) * Val.ToDecimal(txtOpeningRate.Text));
        }
        private void txtOpeningQty_EditValueChanged(object sender, EventArgs e)
        {
            txtOpeningAmount.Text = string.Format("{0:0.00}", Val.ToDecimal(txtOpeningQty.Text) * Val.ToDecimal(txtOpeningRate.Text));
        }
        private void txtTon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }
        private void txtOpeningQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtOpeningRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as DevExpress.XtraEditors.TextEdit).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        #region GridEvents  

        private void GrdDetItemMaster_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (e.Clicks == 2)
                {
                    DataRow DRow = GrdDetSubItemMaster.GetDataRow(e.RowHandle);

                    lblMode.Text = "Edit Mode";
                    lblMode.Tag = Val.ToInt64(DRow["sub_item_id"]);
                    txtSubItem.Text = Val.ToString(DRow["sub_item_name"]);
                    lueItem.EditValue = Val.ToInt64(DRow["item_id"]);
                    dtpOpeningDate.Text = Val.DBDate(DRow["opening_date"].ToString());
                    txtOpeningQty.Text = Val.ToString(DRow["opening_quantity"]);
                    txtOpeningRate.Text = Val.ToString(DRow["opening_rate"]);
                    txtOpeningAmount.Text = Val.ToString(DRow["opening_amt"]);

                    txtModelNo.Text = Val.ToString(DRow["model_no"]);
                    CmbWarrentyYear.Text = Val.ToString(DRow["warranty_year"]);
                    CmbWarrentyMonth.Text = Val.ToString(DRow["warranty_month"]);
                    txtCompany.Text = Val.ToString(DRow["company_name"]);
                    txtItemCode.Text = Val.ToString(DRow["item_code"]);
                    txtTon.Text = Val.ToString(DRow["ton"]);
                    txtType.Text = Val.ToString(DRow["type"]);
                    txtSequenceNo.Text = Val.ToString(DRow["sequence_no"]);

                    lueItem.Focus();
                }
            }
        }

        #endregion

        #endregion

        #region Functions

        private void GetData()
        {
            ObjMfgSubItem.GetData();
            MainGrdSubItemMaster.DataSource = ObjMfgSubItem.DTab;
            MainGrdSubItemMaster.RefreshDataSource();
            GrdDetSubItemMaster.BestFitColumns();
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
                            GrdDetSubItemMaster.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            GrdDetSubItemMaster.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            GrdDetSubItemMaster.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            GrdDetSubItemMaster.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            GrdDetSubItemMaster.ExportToText(Filepath);
                            break;
                        case "html":
                            GrdDetSubItemMaster.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            GrdDetSubItemMaster.ExportToCsv(Filepath);
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
            if (lueItem.EditValue == null)
            {
                Global.Confirm("Item Name Is Required");
                lueItem.Focus();
                return false;
            }
            if (txtSubItem.Text.Length == 0)
            {
                Global.Confirm("Sub Item Name Is Required");
                txtSubItem.Focus();
                return false;
            }
            if (!ObjMfgSubItem.ISExists(txtSubItem.Text, Val.ToInt(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
            {
                Global.Confirm("Sub Item Name Already Exist.");
                txtSubItem.Focus();
                txtSubItem.SelectAll();
                return false;
            }

            //if (LookUpItemType.EditValue == null)
            //{
            //    Global.Confirm("Item Type Name Is Required");
            //    LookUpItemType.Focus();
            //    return false;
            //}

            //if (LookUpUnit.EditValue == null)
            //{
            //    Global.Confirm("Unit Name Is Required");
            //    LookUpUnit.Focus();
            //    return false;
            //}
            return true;
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
