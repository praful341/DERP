using BLL;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.PropertyClasses.Transaction.MFG;
using DERP.Class;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Transaction.MFG
{
    public partial class FrmMFGFactoryMix : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        MFGFactoryMix objMFGFactoryMix = new MFGFactoryMix();
        DataTable DTab_StockData;
        Int64 IntRes;
        Int64 m_numForm_id;
        decimal m_numSummRate = 0;
        decimal m_numRate = 0;
        #endregion

        #region Constructor
        public FrmMFGFactoryMix()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();
            DTab_StockData = new DataTable();
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
            objBOFormEvents.ObjToDispose.Add("");
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion

        #region Events
        private void RepDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            dgvFactoryMix.DeleteRow(dgvFactoryMix.GetRowHandle(dgvFactoryMix.FocusedRowHandle));
            DTab_StockData.AcceptChanges();
        }
        private void FrmMFGFactoryMix_Load(object sender, EventArgs e)
        {
            try
            {
                dtpEntryDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpEntryDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpEntryDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpEntryDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpEntryDate.EditValue = DateTime.Now;
                txtFacMainLotId.Focus();
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ObjPer.SetFormPer();
                if (ObjPer.AllowUpdate == false || ObjPer.AllowInsert == false)
                {
                    Global.Message(BLL.GlobalDec.gStrPermissionInsUpdMsg);
                    return;
                }
                string Str = "";
                if (Val.DBDate(BLL.GlobalDec.gStrServerDate) != Val.DBDate(dtpEntryDate.Text))
                {
                    Str = GlobalDec.CheckLockIsOpenOrNot(Val.DBDate(dtpEntryDate.Text), Val.DBTime(DateTime.Now.ToShortTimeString())); //Val.GetFullTime12());
                    if (Str != "YES")
                    {
                        if (Str != "")
                        {
                            Global.Message(Str);
                            return;
                        }
                        else
                        {
                            Global.Message("You Are Not Suppose to Make Entry On Different Date");
                            return;
                        }
                    }
                    else
                    {
                        dtpEntryDate.Enabled = true;
                        dtpEntryDate.Visible = true;
                    }
                }

                btnSave.Enabled = false;
                DataTable dtTemp = new DataTable();
                dtTemp = (DataTable)grdFactoryMix.DataSource;
                List<ListError> lstError = new List<ListError>();
                if (dtTemp == null)
                {
                    Global.Message("Atleast 1 record must be enter in grid");
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
                backgroundWorker_FactoryMix.RunWorkerAsync();

                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearDetails();
        }
        private bool ClearDetails()
        {
            bool blnReturn = true;
            try
            {
                dtpEntryDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpEntryDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpEntryDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpEntryDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpEntryDate.EditValue = DateTime.Now;
                txtFacMainLotId.Text = "0";
                grdFactoryMix.DataSource = null;
                DTab_StockData.Rows.Clear();
                DTab_StockData.Columns.Clear();
                txtFacMainLotId.Focus();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvFactoryMix_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                if (Val.ToDecimal(clmAmount.SummaryItem.SummaryValue) > 0 && Val.ToDecimal(clmCarat.SummaryItem.SummaryValue) > 0)
                {
                    m_numSummRate = Math.Round((Val.ToDecimal(clmAmount.SummaryItem.SummaryValue) / Val.ToDecimal(clmCarat.SummaryItem.SummaryValue)), 2, MidpointRounding.AwayFromZero);
                    m_numRate = m_numSummRate;
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
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }

        private void backgroundWorker_FactoryMix_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                MFGFactoryMix MFGFactoryMix = new MFGFactoryMix();
                MFGFactoryMix_Property objMFGFactoryMixProperty = new MFGFactoryMix_Property();

                Conn = new BeginTranConnection(true, false);

                DataTable m_DTab = new DataTable();
                ArrayList MyArrayList = new ArrayList();

                IntRes = 0;
                Int64 NewHistory_Union_Id = 0;
                Int64 Lot_SrNo = 0;
                try
                {

                    DataTable Fac_Mix_Data = (DataTable)grdFactoryMix.DataSource;
                    int i = 1;
                    foreach (DataRow drw in Fac_Mix_Data.Rows)
                    {
                        if (i == 1)
                        {
                            objMFGFactoryMixProperty.total_pcs = Val.ToInt(clmPcs.SummaryItem.SummaryValue);
                            objMFGFactoryMixProperty.total_carat = Val.ToDecimal(clmCarat.SummaryItem.SummaryValue);
                            objMFGFactoryMixProperty.total_breakage_pcs = Val.ToInt(clmBreakPcs.SummaryItem.SummaryValue);
                            objMFGFactoryMixProperty.total_breakage_carat = Val.ToDecimal(clmBreakCarat.SummaryItem.SummaryValue);
                            objMFGFactoryMixProperty.total_loss_carat = Val.ToDecimal(clmLossCarat.SummaryItem.SummaryValue);
                        }
                        else
                        {
                            objMFGFactoryMixProperty.total_pcs = Val.ToInt(0);
                            objMFGFactoryMixProperty.total_carat = Val.ToDecimal(0);
                            objMFGFactoryMixProperty.total_breakage_pcs = Val.ToInt(0);
                            objMFGFactoryMixProperty.total_breakage_carat = Val.ToDecimal(0);
                            objMFGFactoryMixProperty.total_loss_carat = Val.ToDecimal(0);
                        }
                        objMFGFactoryMixProperty.pcs = Val.ToInt(drw["pcs"]);
                        objMFGFactoryMixProperty.carat = Val.ToDecimal(drw["carat"]);
                        objMFGFactoryMixProperty.breakage_pcs = Val.ToInt(drw["breakage_pcs"]);
                        objMFGFactoryMixProperty.breakage_carat = Val.ToDecimal(drw["breakage_carat"]);
                        objMFGFactoryMixProperty.loss_carat = Val.ToDecimal(drw["loss_carat"]);
                        objMFGFactoryMixProperty.rate = Val.ToDecimal(drw["rate"]);
                        objMFGFactoryMixProperty.amount = Val.ToDecimal(drw["amount"]);
                        objMFGFactoryMixProperty.from_company_id = Val.ToInt(drw["company_id"]);
                        objMFGFactoryMixProperty.from_branch_id = Val.ToInt(drw["branch_id"]);
                        objMFGFactoryMixProperty.from_location_id = Val.ToInt(drw["location_id"]);
                        objMFGFactoryMixProperty.from_department_id = Val.ToInt(drw["department_id"]);
                        objMFGFactoryMixProperty.lot_id = Val.ToInt64(drw["lot_id"]);
                        objMFGFactoryMixProperty.fac_main_lot_id = Val.ToInt64(drw["fac_main_lot_id"]);
                        objMFGFactoryMixProperty.Fac_Mix_date = Val.DBDate(dtpEntryDate.Text);
                        objMFGFactoryMixProperty.quality_id = Val.ToInt64(drw["quality_id"]);
                        objMFGFactoryMixProperty.form_id = Val.ToInt(m_numForm_id);
                        objMFGFactoryMixProperty.rough_cut_id = Val.ToInt64(drw["rough_cut_id"]);
                        objMFGFactoryMixProperty.kapan_id = Val.ToInt64(drw["kapan_id"]);
                        objMFGFactoryMixProperty.prediction_id = Val.ToInt64(drw["prediction_id"]);
                        objMFGFactoryMixProperty.manager_id = Val.ToInt(drw["manager_id"]);
                        objMFGFactoryMixProperty.employee_id = Val.ToInt(drw["employee_id"]);
                        objMFGFactoryMixProperty.process_id = Val.ToInt(drw["process_id"]);
                        objMFGFactoryMixProperty.sub_process_id = Val.ToInt(drw["sub_process_id"]);
                        objMFGFactoryMixProperty.rough_clarity_id = Val.ToInt(drw["rough_clarity_id"]);
                        objMFGFactoryMixProperty.rough_sieve_id = Val.ToInt(drw["rough_sieve_id"]);
                        objMFGFactoryMixProperty.purity_id = Val.ToInt(drw["purity_id"]);
                        objMFGFactoryMixProperty.union_id = IntRes;
                        objMFGFactoryMixProperty.history_union_id = NewHistory_Union_Id;
                        objMFGFactoryMixProperty.lot_srno = Lot_SrNo;

                        objMFGFactoryMixProperty = MFGFactoryMix.Save(objMFGFactoryMixProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                        IntRes = Val.ToInt64(objMFGFactoryMixProperty.union_id);
                        NewHistory_Union_Id = Val.ToInt64(objMFGFactoryMixProperty.history_union_id);
                        Lot_SrNo = Val.ToInt64(objMFGFactoryMixProperty.lot_srno);
                        i++;
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
            }
            catch (Exception ex)
            {
                IntRes = -1;
                Conn.Inter1.Rollback();
                Conn = null;
                Global.Message(ex.ToString());
                if (ex.InnerException != null)
                {
                    Global.Message(ex.InnerException.ToString());
                }
            }
        }

        private void backgroundWorker_FactoryMix_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (IntRes > 0)
                {
                    Global.Confirm("Factory Mix Data Save Succesfully");
                    btnClear_Click(null, null);
                }
                else
                {
                    Global.Confirm("Error In Factory Mix Data");
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }

        #endregion

        #region Functions 
        private void txtFacMainLotId_Validated(object sender, EventArgs e)
        {
            try
            {
                DTab_StockData.AcceptChanges();

                if (txtFacMainLotId.Text.Length == 0)
                {
                    return;
                }

                DataTable DTab_ValidateLotID = objMFGFactoryMix.Stock_GetData(0, Val.ToInt(txtFacMainLotId.Text));

                if (DTab_ValidateLotID.Rows.Count > 0)
                {
                    string GblEmpBarcode = "";
                    for (int i = 0; i < DTab_StockData.Rows.Count; i++)
                    {
                        if (DTab_StockData.Rows[i]["fac_main_lot_id"].ToString() != txtFacMainLotId.Text)
                        {
                            Global.Message("Main Lot ID is different Compare to Grid!");
                            txtFacMainLotId.Text = "";
                            txtFacMainLotId.Focus();
                            return;
                        }
                        if (DTab_StockData.Rows[i]["status_id"].ToString() == "1")
                        {
                            GblEmpBarcode = GblEmpBarcode + DTab_StockData.Rows[i]["lot_id"] + ",";
                        }
                    }

                    if (GblEmpBarcode.Length > 0)
                    {
                        GblEmpBarcode = GblEmpBarcode.Remove(GblEmpBarcode.Count() - 1, 1) + " This Lot ID already Issue in the Employee \n";
                    }
                }
                else
                {
                    Global.Message("Main Lot ID Not Found");
                    txtFacMainLotId.Text = "";
                    txtFacMainLotId.Focus();
                    return;
                }

                //DTab_StockData = objMFGFactoryMix.Stock_GetData(0, Val.ToInt(txtFacMainLotId.Text));

                if (DTab_ValidateLotID.Rows.Count > 0)
                {
                    txtFacMainLotId.Text = "";
                    txtFacMainLotId.Focus();
                }

                grdFactoryMix.DataSource = DTab_ValidateLotID;
                grdFactoryMix.RefreshDataSource();
                dgvFactoryMix.BestFitColumns();
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }

        #endregion

        #region Export Grid

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
                            dgvFactoryMix.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvFactoryMix.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvFactoryMix.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvFactoryMix.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvFactoryMix.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvFactoryMix.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvFactoryMix.ExportToCsv(Filepath);
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

        private void txtFacMainLotId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
