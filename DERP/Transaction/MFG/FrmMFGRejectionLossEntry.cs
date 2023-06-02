using BLL;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.PropertyClasses.Transaction.MFG;
using DERP.Class;
using System;
using System.Data;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Master
{
    public partial class FrmMFGRejectionLossEntry : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        FormEvents objBOFormEvents;
        Validation Val;
        BLL.FormPer ObjPer;

        MFGRejectionLossEntry objRejectionEntry;
        DataTable m_department_type;
        DataTable m_dtbKapan = new DataTable();
        #endregion

        #region Constructor
        public FrmMFGRejectionLossEntry()
        {
            InitializeComponent();

            objBOFormEvents = new FormEvents();
            Val = new Validation();
            ObjPer = new BLL.FormPer();

            objRejectionEntry = new MFGRejectionLossEntry();

            m_department_type = new DataTable();
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
            objBOFormEvents.ObjToDispose.Add(objRejectionEntry);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }

        #endregion

        #region Events
        private void FrmMFGRejectionLossEntry_Load(object sender, System.EventArgs e)
        {
            try
            {
                GetData();
                btnClear_Click(btnClear, null);

                m_dtbKapan = Global.GetKapanAll();

                lueKapan.Properties.DataSource = m_dtbKapan;
                lueKapan.Properties.ValueMember = "kapan_id";
                lueKapan.Properties.DisplayMember = "kapan_no";

                lueSearchKapan.Properties.DataSource = m_dtbKapan;
                lueSearchKapan.Properties.ValueMember = "kapan_id";
                lueSearchKapan.Properties.DisplayMember = "kapan_no";
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

                dtpDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpDate.Properties.CharacterCasing = CharacterCasing.Upper;
                //dtpDate.EditValue = DateTime.Now;

                dtpFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
                //dtpFromDate.EditValue = DateTime.Now;

                dtpToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpToDate.Properties.CharacterCasing = CharacterCasing.Upper;
                //dtpToDate.EditValue = DateTime.Now;

                lueKapan.EditValue = null;
                lueSearchKapan.EditValue = null;
                txtLabourAmount.Text = "";
                txtLottingLossAmount.Text = "";
                txtLottingLossCarat.Text = "";
                txtLottingLossRate.Text = "";
                txtLottingRejAmount.Text = "";
                txtLottingRejCarat.Text = "";
                txtLottingRejrate.Text = "";
                txtNiravBhaiCarat.Text = "";
                txtNiravBhaiRate.Text = "";
                txtNiravBhaiAmount.Text = "";
                txtOpeningAmount.Text = "";
                txtOpeningCarat.Text = "";
                txtOpeningRate.Text = "";
                txtRejLossAmount.Text = "";
                txtRejLossCarat.Text = "";
                txtRoughLossAmount.Text = "";
                txtRoughLossCarat.Text = "";
                txtRoughLossRate.Text = "";
                txtRejLossRate.Text = "";
                txtInwardCarat.Text = "";
                txtInwardRate.Text = "";
                txtInwardAmount.Text = "";
                btnDelete.Visible = false;
                lueKapan.Focus();
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
            DialogResult result = MessageBox.Show("Do you want to Delete Rejection data?", "Confirmation", MessageBoxButtons.YesNoCancel);
            if (result != DialogResult.Yes)
            {
                btnDelete.Visible = false;
                return;
            }
            objRejectionEntry.Delete(Val.ToInt32(lblMode.Tag));
            GetData();
            btnClear_Click(null, null);
        }
        private void txtOpeningRate_EditValueChanged(object sender, EventArgs e)
        {
            txtOpeningAmount.Text = Val.ToString(Val.ToDecimal(txtOpeningCarat.Text) * Val.ToDecimal(txtOpeningRate.Text));
        }
        private void txtOpeningCarat_EditValueChanged(object sender, EventArgs e)
        {
            txtOpeningAmount.Text = Val.ToString(Val.ToDecimal(txtOpeningCarat.Text) * Val.ToDecimal(txtOpeningRate.Text));
        }
        private void txtRejLossCarat_EditValueChanged(object sender, EventArgs e)
        {
            txtRejLossAmount.Text = Val.ToString(Val.ToDecimal(txtRejLossCarat.Text) * Val.ToDecimal(txtRejLossRate.Text));
        }
        private void txtRejLossRate_EditValueChanged(object sender, EventArgs e)
        {
            txtRejLossAmount.Text = Val.ToString(Val.ToDecimal(txtRejLossCarat.Text) * Val.ToDecimal(txtRejLossRate.Text));
        }
        private void txtRoughLossCarat_EditValueChanged(object sender, EventArgs e)
        {
            txtRoughLossAmount.Text = Val.ToString(Val.ToDecimal(txtRoughLossCarat.Text) * Val.ToDecimal(txtRoughLossRate.Text));
        }
        private void txtRoughLossRate_EditValueChanged(object sender, EventArgs e)
        {
            txtRoughLossAmount.Text = Val.ToString(Val.ToDecimal(txtRoughLossCarat.Text) * Val.ToDecimal(txtRoughLossRate.Text));
        }
        private void txtLottingLossCarat_EditValueChanged(object sender, EventArgs e)
        {
            txtLottingLossAmount.Text = Val.ToString(Val.ToDecimal(txtLottingLossCarat.Text) * Val.ToDecimal(txtLottingLossRate.Text));
        }
        private void txtLottingLossRate_EditValueChanged(object sender, EventArgs e)
        {
            txtLottingLossAmount.Text = Val.ToString(Val.ToDecimal(txtLottingLossCarat.Text) * Val.ToDecimal(txtLottingLossRate.Text));
        }
        private void txtLottingRejCarat_EditValueChanged(object sender, EventArgs e)
        {
            txtLottingRejAmount.Text = Val.ToString(Val.ToDecimal(txtLottingRejCarat.Text) * Val.ToDecimal(txtLottingRejrate.Text));
        }
        private void txtLottingRejrate_EditValueChanged(object sender, EventArgs e)
        {
            txtLottingRejAmount.Text = Val.ToString(Val.ToDecimal(txtLottingRejCarat.Text) * Val.ToDecimal(txtLottingRejrate.Text));
        }
        private void txtNiravBhaiCarat_EditValueChanged(object sender, EventArgs e)
        {
            txtNiravBhaiAmount.Text = Val.ToString(Val.ToDecimal(txtNiravBhaiCarat.Text) * Val.ToDecimal(txtNiravBhaiRate.Text));
        }
        private void txtNiravBhaiRate_EditValueChanged(object sender, EventArgs e)
        {
            txtNiravBhaiAmount.Text = Val.ToString(Val.ToDecimal(txtNiravBhaiCarat.Text) * Val.ToDecimal(txtNiravBhaiRate.Text));
        }
        private void txtInwardRate_EditValueChanged(object sender, EventArgs e)
        {
            txtInwardAmount.Text = Val.ToString(Val.ToDecimal(txtInwardCarat.Text) * Val.ToDecimal(txtInwardRate.Text));
        }
        private void txtInwardCarat_EditValueChanged(object sender, EventArgs e)
        {
            txtInwardAmount.Text = Val.ToString(Val.ToDecimal(txtInwardCarat.Text) * Val.ToDecimal(txtInwardRate.Text));
        }

        #region GridEvents               
        private void dgvRejectionEntry_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvRejectionLossEntry.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt32(Drow["rej_loss_id"]);
                        dtpDate.EditValue = Val.ToString(Drow["date"]);
                        lueKapan.EditValue = Val.ToInt64(Drow["kapan_id"]);
                        txtLabourAmount.Text = Val.ToString(Val.ToDecimal(Drow["labour"]));
                        txtLottingLossAmount.Text = Val.ToString(Val.ToDecimal(Drow["lotting_loss_amount"]));
                        txtLottingLossCarat.Text = Val.ToString(Val.ToDecimal(Drow["lotting_loss_carat"]));
                        txtLottingLossRate.Text = Val.ToString(Val.ToDecimal(Drow["lotting_loss_rate"]));
                        txtLottingRejAmount.Text = Val.ToString(Val.ToDecimal(Drow["lotting_rej_amount"]));
                        txtLottingRejCarat.Text = Val.ToString(Val.ToDecimal(Drow["lotting_rej_carat"]));
                        txtLottingRejrate.Text = Val.ToString(Val.ToDecimal(Drow["lotting_rej_rate"]));
                        txtNiravBhaiCarat.Text = Val.ToString(Val.ToDecimal(Drow["niravbhai_rej_carat"]));
                        txtNiravBhaiRate.Text = Val.ToString(Val.ToDecimal(Drow["niravbhai_rej_rate"]));
                        txtNiravBhaiAmount.Text = Val.ToString(Val.ToDecimal(Drow["niravbhai_rej_amount"]));
                        txtOpeningAmount.Text = Val.ToString(Val.ToDecimal(Drow["opening_amount"]));
                        txtOpeningCarat.Text = Val.ToString(Val.ToDecimal(Drow["opening_carat"]));
                        txtOpeningRate.Text = Val.ToString(Val.ToDecimal(Drow["opening_rate"]));

                        txtRejLossAmount.Text = Val.ToString(Val.ToDecimal(Drow["rej_loss_amount"]));
                        txtRejLossCarat.Text = Val.ToString(Val.ToDecimal(Drow["rej_loss_carat"]));
                        txtRejLossRate.Text = Val.ToString(Val.ToDecimal(Drow["rej_loss_rate"]));

                        txtRoughLossAmount.Text = Val.ToString(Val.ToDecimal(Drow["rough_loss_amount"]));
                        txtRoughLossCarat.Text = Val.ToString(Val.ToDecimal(Drow["rough_loss_carat"]));
                        txtRoughLossRate.Text = Val.ToString(Val.ToDecimal(Drow["rough_loss_rate"]));

                        txtInwardCarat.Text = Val.ToString(Val.ToDecimal(Drow["inward_carat"]));
                        txtInwardRate.Text = Val.ToString(Val.ToDecimal(Drow["inward_rate"]));
                        txtInwardAmount.Text = Val.ToString(Val.ToDecimal(Drow["inward_amount"]));

                        btnDelete.Visible = true;
                        lueKapan.Focus();
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

        #region Functions
        private bool SaveDetails()
        {
            bool blnReturn = true;
            MFGRejectionLoss_EntryProperty RejectionLossProperty = new MFGRejectionLoss_EntryProperty();
            MFGRejectionLossEntry objRejectionEntry = new MFGRejectionLossEntry();

            try
            {
                RejectionLossProperty.rej_loss_id = Val.ToInt32(lblMode.Tag);
                RejectionLossProperty.date = Val.DBDate(dtpDate.Text);
                RejectionLossProperty.kapan_id = Val.ToInt32(lueKapan.EditValue);
                RejectionLossProperty.opening_carat = Val.ToDecimal(txtOpeningCarat.Text);
                RejectionLossProperty.opening_rate = Val.ToDecimal(txtOpeningRate.Text);
                RejectionLossProperty.opening_amount = Val.ToDecimal(txtOpeningAmount.Text);
                RejectionLossProperty.rej_loss_carat = Val.ToDecimal(txtRejLossCarat.Text);
                RejectionLossProperty.rej_loss_rate = Val.ToDecimal(txtRejLossRate.Text);
                RejectionLossProperty.rej_loss_amount = Val.ToDecimal(txtRejLossAmount.Text);
                RejectionLossProperty.rough_loss_carat = Val.ToDecimal(txtRoughLossCarat.Text);
                RejectionLossProperty.rough_loss_rate = Val.ToDecimal(txtRoughLossRate.Text);
                RejectionLossProperty.rough_loss_amount = Val.ToDecimal(txtRoughLossAmount.Text);
                RejectionLossProperty.lotting_loss_carat = Val.ToDecimal(txtLottingLossCarat.Text);
                RejectionLossProperty.lotting_loss_rate = Val.ToDecimal(txtLottingLossRate.Text);
                RejectionLossProperty.lotting_loss_amount = Val.ToDecimal(txtLottingLossAmount.Text);
                RejectionLossProperty.lotting_rej_carat = Val.ToDecimal(txtLottingRejCarat.Text);
                RejectionLossProperty.lotting_rej_rate = Val.ToDecimal(txtLottingRejrate.Text);
                RejectionLossProperty.lotting_rej_amount = Val.ToDecimal(txtLottingRejAmount.Text);
                RejectionLossProperty.niravbhai_rej_carat = Val.ToDecimal(txtNiravBhaiCarat.Text);
                RejectionLossProperty.niravbhai_rej_rate = Val.ToDecimal(txtNiravBhaiRate.Text);
                RejectionLossProperty.niravbhai_rej_amount = Val.ToDecimal(txtNiravBhaiAmount.Text);
                RejectionLossProperty.labour = Val.ToDecimal(txtLabourAmount.Text);
                RejectionLossProperty.inward_carat = Val.ToDecimal(txtInwardCarat.Text);
                RejectionLossProperty.inward_rate = Val.ToDecimal(txtInwardRate.Text);
                RejectionLossProperty.inward_amount = Val.ToDecimal(txtInwardAmount.Text);

                //RejectionLossProperty.form_id = Val.ToInt32(for);
                int IntRes = objRejectionEntry.Save(RejectionLossProperty);
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Save Rejection Loss Data");
                    lueKapan.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Rejection Loss Save Successfully");
                    }
                    else
                    {
                        Global.Confirm("Rejection Loss Update Successfully");
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
                RejectionLossProperty = null;
            }

            return blnReturn;
        }
        public void GetData()
        {
            try
            {
                DataTable DTab = objRejectionEntry.GetData(Val.DBDate(dtpFromDate.Text), Val.DBDate(dtpToDate.Text), Val.ToInt32(lueSearchKapan.EditValue));
                grdRejectionLossEntry.DataSource = DTab;
                dgvRejectionLossEntry.BestFitColumns();
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
                            dgvRejectionLossEntry.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvRejectionLossEntry.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvRejectionLossEntry.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvRejectionLossEntry.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvRejectionLossEntry.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvRejectionLossEntry.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvRejectionLossEntry.ExportToCsv(Filepath);
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
