using BLL;
using BLL.FunctionClasses.Rejection;
using BLL.PropertyClasses.Transaction.MFG;
using DERP.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Rejection
{
    public partial class FrmMFGRoughStockEntry : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        FormEvents objBOFormEvents;
        Validation Val;
        BLL.FormPer ObjPer;
        MFGRoughStockEntry objRoughStockEntry;
        DataTable m_department_type;
        DataTable m_dtbType;
        decimal m_numTotalCarats;
        decimal m_numTotalAmount;

        #endregion

        #region Constructor
        public FrmMFGRoughStockEntry()
        {
            InitializeComponent();
            objBOFormEvents = new FormEvents();
            Val = new Validation();
            ObjPer = new BLL.FormPer();
            objRoughStockEntry = new MFGRoughStockEntry();
            m_department_type = new DataTable();
            m_numTotalCarats = 0;
            m_numTotalAmount = 0;
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
            objBOFormEvents.ObjToDispose.Add(objRoughStockEntry);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }

        #endregion

        #region Events
        private void FrmMFGRoughStockEntry_Load(object sender, EventArgs e)
        {
            try
            {
                dtpDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpDate.EditValue = DateTime.Now;

                dtpFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpFromDate.EditValue = DateTime.Now;

                dtpToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpToDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpToDate.EditValue = DateTime.Now;

                m_dtbType = new DataTable();
                m_dtbType.Columns.Add("type");
                m_dtbType.Rows.Add("ROUGH");
                m_dtbType.Rows.Add("REJECTION");

                lueType.Properties.DataSource = m_dtbType;
                lueType.Properties.ValueMember = "type";
                lueType.Properties.DisplayMember = "type";
                lueType.EditValue = "ROUGH";

                GetData();
                btnClear_Click(btnClear, null);
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void btnSave_Click(object sender, System.EventArgs e)
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
        private void btnClear_Click(object sender, System.EventArgs e)
        {
            try
            {
                lblMode.Tag = 0;
                lblMode.Text = "Add Mode";
                txtKapanNo.Text = "";
                txtAmount.Text = "";
                txtPcs.Text = "";
                txtWt.Text = "";
                txtRate.Text = "";
                lueType.ItemIndex = 0;
                txtKapanNo.Enabled = true;
                txtKapanNo.Focus();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void btnExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            GetData();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (GlobalDec.gEmployeeProperty.user_name != "JAYESH")
            {
                Global.Message("Don't have Permission...So Please Contact to Administrator");
                return;
            }
            if (Val.ToInt32(lblMode.Tag) != 0)
            {
                DialogResult result = MessageBox.Show("Do you want to Delete Rough Stock data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result != DialogResult.Yes)
                {
                    return;
                }
                int IntRes = objRoughStockEntry.Delete(Val.ToInt32(lblMode.Tag));

                if (IntRes == -1)
                {
                    Global.Confirm("Error In Delete Rough Stock Data");
                    txtKapanNo.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Rough Stock Data Delete Successfully");
                        GetData();
                        btnClear_Click(null, null);
                    }
                    else
                    {
                        Global.Confirm("Rough Stock Data Delete Successfully");
                        GetData();
                        btnClear_Click(null, null);
                    }
                }
            }
            else
            {
                Global.Confirm("Not Selected Any Data are Deleted..");
                return;
            }
        }
        private void txtRate_EditValueChanged(object sender, EventArgs e)
        {
            txtAmount.Text = Val.ToString(Val.ToDecimal(txtWt.Text) * Val.ToDecimal(txtRate.Text));
        }
        private void txtWt_EditValueChanged(object sender, EventArgs e)
        {
            txtAmount.Text = Val.ToString(Val.ToDecimal(txtWt.Text) * Val.ToDecimal(txtRate.Text));
        }
        private void txtRate_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txtWt_KeyPress(object sender, KeyPressEventArgs e)
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
        private void dgvRoughStockEntry_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvRoughStockEntry.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt32(Drow["kapan_id"]);
                        dtpDate.EditValue = Val.DBDate(Drow["kapan_date"].ToString());
                        txtKapanNo.Text = Val.ToString(Drow["kapan_no"]);
                        txtPcs.Text = Val.ToString(Val.ToDecimal(Drow["pcs"]));
                        txtAmount.Text = Val.ToString(Val.ToDecimal(Drow["amount"]));
                        txtWt.Text = Val.ToString(Val.ToDecimal(Drow["carat"]));
                        txtRate.Text = Val.ToString(Val.ToDecimal(Drow["rate"]));
                        lueType.Text = Val.ToString(Val.ToDecimal(Drow["type"]));
                        btnDelete.Visible = true;
                        txtKapanNo.Enabled = false;
                        txtPcs.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void dgvRoughStockEntry_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                m_numTotalCarats = Math.Round(Val.ToDecimal(clmCarat.SummaryItem.SummaryValue), 2, MidpointRounding.AwayFromZero);

                if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == "rate")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                        m_numTotalAmount = 0;
                    else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                        m_numTotalAmount += (Val.ToDecimal(e.GetValue("carat")) * Val.ToDecimal(e.GetValue("rate")));
                    else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    {
                        if (m_numTotalAmount > 0 && m_numTotalCarats > 0)
                            e.TotalValue = Math.Round((m_numTotalAmount / m_numTotalCarats), 2, MidpointRounding.AwayFromZero);
                        else
                            e.TotalValue = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }

        #endregion

        #endregion

        #region Functions

        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (Val.ToString(txtKapanNo.Text) == "")
                {
                    lstError.Add(new ListError(12, "Kapan No"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtKapanNo.Focus();
                    }
                }


            }
            catch (Exception ex)
            {
                lstError.Add(new ListError(ex));
            }
            return (!(BLL.General.ShowErrors(lstError)));
        }
        private bool SaveDetails()
        {
            bool blnReturn = true;
            MFGRoughStock_EntryProperty RoughStockProperty = new MFGRoughStock_EntryProperty();
            MFGRoughStockEntry objRoughStockEntry = new MFGRoughStockEntry();

            if (!ValidateDetails())
            {
                blnReturn = false;
                return blnReturn;
            }

            if (lblMode.Text == "Add Mode")
            {
                while (objRoughStockEntry.ISExists_Kapan_No(Val.ToString(txtKapanNo.Text)) == true)
                {
                    Global.Confirm("This Kapan No already Created Please Check");
                    return false;
                }
            }

            try
            {
                RoughStockProperty.kapan_id = Val.ToInt32(lblMode.Tag);
                RoughStockProperty.kapan_date = Val.DBDate(dtpDate.Text);
                RoughStockProperty.kapan_no = Val.ToString(txtKapanNo.Text);
                RoughStockProperty.carat = Val.ToDecimal(txtWt.Text);
                RoughStockProperty.rate = Val.ToDecimal(txtRate.Text);
                RoughStockProperty.pcs = Val.ToDecimal(txtPcs.Text);
                RoughStockProperty.amount = Val.ToDecimal(txtAmount.Text);
                RoughStockProperty.type = Val.ToString(lueType.Text);

                int IntRes = objRoughStockEntry.Save(RoughStockProperty);
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Save Rough Stock Data");
                    txtKapanNo.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Rough Stock Save Successfully");
                    }
                    else
                    {
                        Global.Confirm("Rough Stock Update Successfully");
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
                RoughStockProperty = null;
            }
            return blnReturn;
        }
        public void GetData()
        {
            try
            {
                DataTable DTab = objRoughStockEntry.GetData(Val.DBDate(dtpFromDate.Text), Val.DBDate(dtpToDate.Text));
                grdRoughStockEntry.DataSource = DTab;
                dgvRoughStockEntry.BestFitColumns();
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
                            dgvRoughStockEntry.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvRoughStockEntry.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvRoughStockEntry.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvRoughStockEntry.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvRoughStockEntry.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvRoughStockEntry.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvRoughStockEntry.ExportToCsv(Filepath);
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

        private void txtPcs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
