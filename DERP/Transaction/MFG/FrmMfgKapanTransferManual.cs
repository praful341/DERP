using BLL;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.PropertyClasses.Transaction.MFG;
using DERP.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Transaction.MFG
{
    public partial class FrmMfgKapanTransferManual : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        DataTable m_dtbKapan;

        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        MFGKapanTransferManual objKapanTrf;
        int m_numForm_id;
        int IntRes;
        decimal m_numSummRate = 0;

        #endregion

        #region Constructor
        public FrmMfgKapanTransferManual()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            objKapanTrf = new MFGKapanTransferManual();
            m_dtbKapan = new DataTable();
            m_numForm_id = 0;
            IntRes = 0;
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
            objBOFormEvents.ObjToDispose.Add(objKapanTrf);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion   

        #region Events     
        private void FrmRejectionToMakableTransferConfirm_Load(object sender, EventArgs e)
        {
            try
            {
                dtpConfirmDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpConfirmDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpConfirmDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpConfirmDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpConfirmDate.EditValue = DateTime.Now;

                m_dtbKapan = Global.GetKapanAll();

                lueKapan.Properties.DataSource = m_dtbKapan;
                lueKapan.Properties.ValueMember = "kapan_id";
                lueKapan.Properties.DisplayMember = "kapan_no";

                lueToKapan.Properties.DataSource = m_dtbKapan;
                lueToKapan.Properties.ValueMember = "kapan_id";
                lueToKapan.Properties.DisplayMember = "kapan_no";
                ClearDetails();
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                this.Cursor = Cursors.Default;
                btnSave.Enabled = true;
                return;
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearDetails();
        }
        public void SetControlPropertyValue(Control oControl, string propName, object propValue)
        {
            if (oControl.InvokeRequired)
            {
                SetControlValueCallback d = new SetControlValueCallback(SetControlPropertyValue);
                oControl.Invoke(d, new object[]
                        {
                            oControl,
                            propName,
                            propValue
                        });
            }
            else
            {
                Type t = oControl.GetType();
                PropertyInfo[] props = t.GetProperties();
                foreach (PropertyInfo p in props)
                {
                    if ((p.Name.ToUpper() == propName.ToUpper()))
                    {
                        p.SetValue(oControl, propValue, null);
                    }
                }
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (Val.ToString(lblMode.Text) != "Edit Mode")
            {
                return;
            }
            btnDelete.Enabled = false;
            DialogResult result = MessageBox.Show("Do you want to Delete data?", "Confirmation", MessageBoxButtons.YesNoCancel);
            if (result != DialogResult.Yes)
            {
                btnDelete.Enabled = true;
                return;
            }
            MFGKapanTransfer_ManualProperty KapanTransferProperty = new MFGKapanTransfer_ManualProperty();
            try
            {

                KapanTransferProperty.transfer_id = Val.ToInt(lblMode.Tag);
                IntRes = objKapanTrf.Delete(KapanTransferProperty);

                if (IntRes > 0)
                {
                    Global.Confirm("Kapan Transfer Data Deleted Succesfully");
                    btnDelete.Enabled = true;
                    btnClear_Click(null, null);
                    GetData();
                }
                else
                {
                    Global.Confirm("Error In Kapan Transfer Data");
                    btnDelete.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                IntRes = -1;
                General.ShowErrors(ex.ToString());
                btnDelete.Enabled = true;
                return;
            }
            finally
            {
                KapanTransferProperty = null;
                btnDelete.Enabled = true;
            }
        }
        private void txtCarat_EditValueChanged(object sender, EventArgs e)
        {
            txtAmount.Text = Val.ToString(Math.Round(Val.ToDecimal(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text)), 0));
        }
        private void txtRate_EditValueChanged(object sender, EventArgs e)
        {
            txtAmount.Text = Val.ToString(Math.Round(Val.ToDecimal(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text)), 0));
        }

        #region GridEvents      

        private void dgvRejToMakTransfer_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvKapanTransfer.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt64(Drow["transfer_id"]);
                        dtpConfirmDate.Text = Val.DBDate(Val.ToString(Drow["transfer_date"]));
                        lueKapan.EditValue = Val.ToInt64(Drow["from_kapan_id"]);
                        lueToKapan.EditValue = Val.ToInt64(Drow["to_kapan_id"]);
                        txtCarat.Text = Val.ToString(Drow["carat"]);
                        txtRate.Text = Val.ToString(Drow["rate"]);
                        btnDelete.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void dgvRejToMakTransfer_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            if (Val.ToDecimal(clmAmount.SummaryItem.SummaryValue) > 0 && Val.ToDecimal(clmCarat.SummaryItem.SummaryValue) > 0)
            {
                m_numSummRate = Math.Round((Val.ToDecimal(clmAmount.SummaryItem.SummaryValue) / Val.ToDecimal(clmCarat.SummaryItem.SummaryValue)), 3, MidpointRounding.AwayFromZero);

            }
            else
            {
                m_numSummRate = 0;
            }
            if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == "rate")
            {
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    e.TotalValue = m_numSummRate;
            }
        }

        #endregion

        #endregion

        #region Functions  
        private bool ValidateDetails()
        {

            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {

                if (Val.ToString(dtpConfirmDate.Text) == string.Empty)
                {
                    lstError.Add(new ListError(22, "Date"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        dtpConfirmDate.Focus();
                    }
                }

                //if (!objKapanTrf.ISExists(Val.ToInt32(lueKapan.EditValue), Val.ToInt32(lueKapan.EditValue)).ToString().Trim().Equals(string.Empty))
                //{
                //    lstError.Add(new ListError(23, "From Kapan"));
                //    if (!blnFocus)
                //    {
                //        blnFocus = true;
                //        lueKapan.Focus();
                //    }

                //}
                if (lueKapan.Text.Length == 0)
                {
                    lstError.Add(new ListError(12, "F. Kapan"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueKapan.Focus();
                    }
                }
                if (lueToKapan.Text.Length == 0)
                {
                    lstError.Add(new ListError(12, "T.Kapan"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueToKapan.Focus();
                    }
                }

                if (Val.ToDecimal(txtCarat.Text) == 0)
                {
                    lstError.Add(new ListError(5, "Carat Not Be Blank!!"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtCarat.Focus();
                    }
                }
                if (Val.ToDecimal(txtRate.Text) == 0)
                {
                    lstError.Add(new ListError(5, "Rate Not Be Blank!!"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtRate.Focus();
                    }
                }
                if (Val.ToDecimal(txtAmount.Text) == 0)
                {
                    lstError.Add(new ListError(5, "Amount Not Be Blank!!"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtCarat.Focus();
                    }
                }
                //if (Val.ToDecimal(txtCarat.Text) > Val.ToDecimal(lblOsCarat.Text))
                //{
                //    lstError.Add(new ListError(5, "Carat Not Be More Than Balance Carat!!"));
                //    if (!blnFocus)
                //    {
                //        blnFocus = true;
                //        txtCarat.Focus();
                //    }
                //}
            }
            catch (Exception ex)
            {
                lstError.Add(new ListError(ex));
            }
            return (!(BLL.General.ShowErrors(lstError)));
        }
        private void GetData()
        {
            objKapanTrf = new MFGKapanTransferManual();
            DateTime datFromDate = DateTime.MinValue;
            DateTime datToDate = DateTime.MinValue;
            try
            {
                DataTable dtKapanData = new DataTable();
                dtKapanData = objKapanTrf.GetData(Val.DBDate(dtpFromDate.Text), Val.DBDate(dtpToDate.Text));

                grdKapanTransfer.DataSource = dtKapanData;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
            finally
            {
                objKapanTrf = null;
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
                            dgvKapanTransfer.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvKapanTransfer.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvKapanTransfer.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvKapanTransfer.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvKapanTransfer.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvKapanTransfer.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvKapanTransfer.ExportToCsv(Filepath);
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

        private bool ClearDetails()
        {
            bool blnReturn = true;
            try
            {

                txtCarat.Text = "0";
                txtRate.Text = "0";
                txtAmount.Text = "0";
                lueKapan.EditValue = null;
                lueToKapan.EditValue = null;
                lblOsCarat.Text = "0";
                lblRate.Text = "0";
                lblAmt.Text = "0";
                lblMode.Text = "Add Mode";
                lblMode.Tag = "0";
                //dtpConfirmDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                //dtpConfirmDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                //dtpConfirmDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                //dtpConfirmDate.Properties.CharacterCasing = CharacterCasing.Upper;
                //dtpConfirmDate.EditValue = DateTime.Now;
                btnSave.Enabled = true;
                btnDelete.Enabled = false;
                lueKapan.Focus();

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

                GetData();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
        }
        private bool SaveDetails()
        {
            bool blnReturn = true;
            MFGKapanTransfer_ManualProperty KapanTransferProperty = new MFGKapanTransfer_ManualProperty();

            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }
                KapanTransferProperty.transfer_id = Val.ToInt32(lblMode.Tag);
                KapanTransferProperty.transfer_date = Val.ToString(dtpConfirmDate.Text);
                KapanTransferProperty.from_kapan_id = Val.ToInt32(lueKapan.EditValue);
                KapanTransferProperty.to_kapan_id = Val.ToInt32(lueToKapan.EditValue);
                KapanTransferProperty.carat = Val.ToDecimal(txtCarat.Text);
                KapanTransferProperty.rate = Val.ToDecimal(txtRate.Text);
                KapanTransferProperty.amount = Val.ToDecimal(txtAmount.Text);

                int IntRes = objKapanTrf.Save(KapanTransferProperty);
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Kapan Transfer");
                    lueKapan.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Kapan Transfer Data Successfully");
                    }
                    else
                    {
                        Global.Confirm("Kapan Transfer Update Successfully");
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
                KapanTransferProperty = null;
            }

            return blnReturn;
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

        private void lueKapan_EditValueChanged(object sender, EventArgs e)
        {
            if (lueKapan.Text != "")
            {
                objKapanTrf = new MFGKapanTransferManual();
                DataTable dtKapanCarat = new DataTable();
                dtKapanCarat = objKapanTrf.KapanCarat(Val.ToInt32(lueKapan.EditValue));
                if (dtKapanCarat.Rows.Count > 0)
                {
                    lblOsCarat.Text = Val.ToString(dtKapanCarat.Rows[0]["carat"]);
                    lblRate.Text = Val.ToString(Math.Round(Val.ToDecimal(dtKapanCarat.Rows[0]["rate"]), 2));
                    lblAmt.Text = Val.ToString(Math.Round(Val.ToDecimal(dtKapanCarat.Rows[0]["amount"]), 2));
                }
                else
                {
                    lblOsCarat.Text = "0";
                    lblRate.Text = "0";
                    lblAmt.Text = "0";
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            GetData();
        }
    }
}
