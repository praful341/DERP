using BLL;
using BLL.FunctionClasses.Rejection;
using BLL.PropertyClasses.Rejection;
using DERP.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Rejection
{
    public partial class FrmMFGRejectionRateEntry : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.BeginTranConnection Conn;
        FormEvents objBOFormEvents;
        Validation Val;
        BLL.FormPer ObjPer;
        Int64 IntRes;
        MfgRejectionRateEntry objRejectionRate = new MfgRejectionRateEntry();

        #endregion

        #region Constructor

        public FrmMFGRejectionRateEntry()
        {
            InitializeComponent();
            objBOFormEvents = new FormEvents();
            Val = new Validation();
            ObjPer = new BLL.FormPer();
            objRejectionRate = new MfgRejectionRateEntry();
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
            objBOFormEvents.ObjToDispose.Add(objRejectionRate);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }

        #endregion

        #region Events

        private void FrmMFGRejectionRateEntry_Load(object sender, EventArgs e)
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

        private void backgroundWorker_MFGRejectionRateEntry_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.Default;
                Conn = new BeginTranConnection(true, false);

                MFGRejectionRateEntryProperty RejectionRateEntryProperty = new MFGRejectionRateEntryProperty();
                MfgRejectionRateEntry objRejectionRateEntry = new MfgRejectionRateEntry();
                IntRes = 0;
                try
                {
                    DataTable m_dtbRejRate = (DataTable)GrdRejectionRate.DataSource;
                    foreach (DataRow drw in m_dtbRejRate.Rows)
                    {
                        if (lblUnionID.Text == "0")
                        {
                            RejectionRateEntryProperty.union_id = IntRes;
                        }
                        else
                        {
                            RejectionRateEntryProperty.union_id = Val.ToInt64(lblUnionID.Text);
                        }
                        RejectionRateEntryProperty.rate_id = Val.ToInt64(drw["rate_id"]);
                        RejectionRateEntryProperty.rate = Val.ToDecimal(drw["rate"]);
                        RejectionRateEntryProperty.date = Val.DBDate(DtpEntryDate.Text);
                        RejectionRateEntryProperty.time = Val.ToString(txtTime.Text);
                        RejectionRateEntryProperty.purity_id = Val.ToInt64(drw["purity_id"]);
                        RejectionRateEntryProperty.type = Val.ToString(drw["type"]);
                        RejectionRateEntryProperty = objRejectionRateEntry.Save(RejectionRateEntryProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                        IntRes = RejectionRateEntryProperty.union_id;
                    }
                    Conn.Inter1.Commit();
                }
                catch (Exception ex)
                {
                    IntRes = -1;
                    Conn.Inter1.Rollback();
                    Conn = null;
                    General.ShowErrors(ex.ToString());
                    return;
                }
                finally
                {
                    RejectionRateEntryProperty = null;
                }
            }
            catch (Exception ex)
            {
                IntRes = -1;
                Conn.Inter1.Rollback();
                Conn = null;
                General.ShowErrors(ex.ToString());
                if (ex.InnerException != null)
                {
                    Global.Message(ex.InnerException.ToString());
                }
            }
        }

        private void backgroundWorker_MFGRejectionRateEntry_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (IntRes > 0)
                {
                    Global.Confirm("Rejection Rate Entry Data Save Successfully");
                    btnClear_Click(null, null);
                }
                else
                {
                    Global.Confirm("Error In Rough Rejection Rate Entry");
                    DtpEntryDate.Focus();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            MFGRejectionRateEntryProperty RejectionRateEntryProperty = new MFGRejectionRateEntryProperty();
            MfgRejectionRateEntry objRejectionRateEntry = new MfgRejectionRateEntry();

            RejectionRateEntryProperty.from_date = Val.DBDate(dtpSearchFromDate.Text);
            RejectionRateEntryProperty.to_date = Val.DBDate(dtpSearchToDate.Text);

            DataTable DTab_Data = objRejectionRateEntry.RejEntry_GetData(RejectionRateEntryProperty);

            grdRejectionRateList.DataSource = DTab_Data;
            dgvRejectionRateList.BestFitColumns();
        }

        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (Val.ToString(DtpEntryDate.Text) == string.Empty)
                {
                    lstError.Add(new ListError(22, "Date"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        DtpEntryDate.Focus();
                    }
                }

                if (lblUnionID.Text == "0")
                {
                    MFGRejectionRateEntryProperty RejectionRateEntryProperty = new MFGRejectionRateEntryProperty();
                    MfgRejectionRateEntry objRejectionRateEntry = new MfgRejectionRateEntry();

                    RejectionRateEntryProperty.date = Val.DBDate(DtpEntryDate.Text);

                    DataTable DTab = objRejectionRateEntry.Rate_Exist_GetData(RejectionRateEntryProperty);

                    if (DTab.Rows.Count > 0)
                    {
                        lstError.Add(new ListError(5, "Rejection Rate Data Already Exist in This Date"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            DtpEntryDate.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lstError.Add(new ListError(ex));
            }
            return (!(BLL.General.ShowErrors(lstError)));
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

            if (!ValidateDetails())
            {
                btnSave.Enabled = true;
                return;
            }

            DialogResult result = MessageBox.Show("Do you want to save data?", "Confirmation", MessageBoxButtons.YesNoCancel);
            if (result != DialogResult.Yes)
            {
                btnSave.Enabled = true;
                return;
            }

            DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
            backgroundWorker_MFGRejectionRateEntry.RunWorkerAsync();

            btnSave.Enabled = true;
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                dtpSearchFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpSearchFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpSearchFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpSearchFromDate.Properties.CharacterCasing = CharacterCasing.Upper;

                dtpSearchToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpSearchToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpSearchToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpSearchToDate.Properties.CharacterCasing = CharacterCasing.Upper;

                dtpSearchFromDate.EditValue = DateTime.Now;
                dtpSearchToDate.EditValue = DateTime.Now;

                DtpEntryDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                DtpEntryDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                DtpEntryDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                DtpEntryDate.Properties.CharacterCasing = CharacterCasing.Upper;

                DtpEntryDate.EditValue = DateTime.Now;

                txtTime.Text = GlobalDec.gStr_SystemTime;

                GetData();
                BtnSearch_Click(null, null);
                lblUnionID.Text = "0";

                DtpEntryDate.Focus();
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

        private void dgvRejectionRateList_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        MfgRejectionRateEntry objRejectionRateEntry = new MfgRejectionRateEntry();

                        DataRow Drow = dgvRejectionRateList.GetDataRow(e.RowHandle);
                        lblUnionID.Text = Val.ToString(Drow["union_id"]);
                        DtpEntryDate.Text = Val.DBDate(Drow["date"].ToString());
                        txtTime.Text = Val.ToString(Drow["time"]);

                        DataTable m_dtbRejRate = objRejectionRateEntry.GetDetail_Data(Val.ToInt64(lblUnionID.Text));
                        GrdRejectionRate.DataSource = m_dtbRejRate;
                        dgvRejectionRate.BestFitColumns();
                        DtpEntryDate.Focus();
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
        public void GetData()
        {
            try
            {
                DataTable DTab = objRejectionRate.GetData(1);
                GrdRejectionRate.DataSource = DTab;
                dgvRejectionRate.BestFitColumns();
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
                            dgvRejectionRateList.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvRejectionRateList.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvRejectionRateList.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvRejectionRateList.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvRejectionRateList.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvRejectionRateList.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvRejectionRateList.ExportToCsv(Filepath);
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
