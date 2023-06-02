using BLL;
using BLL.FunctionClasses.Transaction;
using DERP.Class;
using System;
using System.Data;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Transaction.MFG
{
    public partial class FrmMFGSearchBoilIssRec_New : DevExpress.XtraEditors.XtraForm
    {
        #region Declaration


        Validation Val = new Validation();
        MFGProcessReceive objProcessRecieve;
        DataTable dtPrint = new DataTable();
        DataTable m_dtbKapan = new DataTable();
        DataTable m_dtbParam = new DataTable();
        DataTable m_dtbSubProcess = new DataTable();
        public DataTable DTab = new DataTable();
        //DataTable DTab_KapanWiseData = new DataTable();
        public string ColumnsToHide = "";
        public bool AllowMultiSelect = false;
        public string ColumnHeaderCaptions = "";
        public string SearchText = "";
        public string SearchField = "";
        public string ValueMember = "";
        public string SelectedValue = "";

        FormEvents objBOFormEvents = new FormEvents();
        public FrmMFGBoilIssue_New FrmMFGBoilIssue = new FrmMFGBoilIssue_New();
        public FrmMFGBoilRecieve_NEW FrmMFGBoilRecieve = new FrmMFGBoilRecieve_NEW();
        //public FrmMFGLotSplit FrmMFGLotSplit = new FrmMFGLotSplit();
        string FormName = "";
        int m_numSelectedCount = 0;
        #endregion

        #region Constructor
        public FrmMFGSearchBoilIssRec_New()
        {
            InitializeComponent();
            objProcessRecieve = new MFGProcessReceive();
        }
        public void ShowForm(FrmMFGBoilIssue_New ObjForm)
        {
            FrmMFGBoilIssue = new FrmMFGBoilIssue_New();
            FrmMFGBoilIssue = ObjForm;
            FormName = "FrmMFGBoilIssue_New";

            Val.frmGenSetForPopup(this);
            AttachFormEvents();
            this.ShowDialog();
        }
        public void ShowForm(FrmMFGBoilRecieve_NEW ObjForm)
        {
            FrmMFGBoilRecieve = new FrmMFGBoilRecieve_NEW();
            FrmMFGBoilRecieve = ObjForm;
            FormName = "FrmMFGBoilRecieve_NEW";

            Val.frmGenSetForPopup(this);
            AttachFormEvents();
            this.ShowDialog();
        }

        private void AttachFormEvents()
        {
            objBOFormEvents.CurForm = this;
            objBOFormEvents.FormKeyPress = true;
            objBOFormEvents.FormResize = true;
            objBOFormEvents.FormClosing = true;
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }

        #endregion

        #region Form Events
        private void FrmJangedConfirm_Load(object sender, EventArgs e)
        {
            try
            {
                
                Global.LOOKUPProcess(lueProcess);
                Global.LOOKUPSubProcess(lueSubProcess);
                m_dtbSubProcess = (((DataTable)lueSubProcess.Properties.DataSource).Copy());

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

                // Add By Praful On 29072021

                //DTab_KapanWiseData = Global.GetRoughStockWise(Val.ToInt(0), Val.ToInt32(0));

                // End By Praful On 29072021
            }
            catch (Exception ex)
            {
                Global.ErrorMessage(ex.Message);
            }
        }
        private void FrmJangedConfirm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        #endregion

        #region Events
        
        private void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dtab = new DataTable();

            if (FormName == "FrmMFGBoilIssue_New")
            {
                dtab = objProcessRecieve.GetSearchBoilIssRec(Val.ToString(dtpFromDate.Text), Val.ToString(dtpToDate.Text), Val.ToInt(lueProcess.EditValue), Val.ToInt(lueSubProcess.EditValue), Val.ToString(FormName));
            }
            else if (FormName == "FrmMFGBoilRecieve_NEW")
            {
                dtab = objProcessRecieve.GetSearchBoilIssRec(Val.ToString(dtpFromDate.Text), Val.ToString(dtpToDate.Text), Val.ToInt(lueProcess.EditValue), Val.ToInt(lueSubProcess.EditValue), Val.ToString(FormName));
            }
            MainGrid.DataSource = (DataTable)dtab;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lueProcess_EditValueChanged(object sender, EventArgs e)
        {
            if (lueProcess.EditValue != System.DBNull.Value)
            {
                if (m_dtbSubProcess.Rows.Count > 0)
                {
                    DataTable dtbdetail = m_dtbSubProcess;

                    string strFilter = string.Empty;

                    if (lueProcess.Text != "")
                        strFilter = "process_id = " + lueProcess.EditValue;

                    dtbdetail.DefaultView.RowFilter = strFilter;
                    dtbdetail.DefaultView.ToTable();

                    DataTable dtb = dtbdetail.DefaultView.ToTable();

                    lueSubProcess.Properties.DataSource = dtb;
                    lueSubProcess.Properties.ValueMember = "sub_process_id";
                    lueSubProcess.Properties.DisplayMember = "sub_process_name";
                    lueSubProcess.EditValue = System.DBNull.Value;
                }
            }
        }

        #endregion

        #region Grid Events
        private void GrdDet_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "SEL")
            {
                if (Val.ToBoolean(GrdDet.GetRowCellValue(e.RowHandle, "SEL")) == true)
                {
                    GrdDet.SetRowCellValue(e.RowHandle, "SEL", false);

                }
                else
                {
                    GrdDet.SetRowCellValue(e.RowHandle, "SEL", true);
                }
                //CalculateTotal();
            }
        }
        private void GrdDet_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        int unionId = 0;
                        //DataRow Drow = GrdDet.GetDataRow(e.RowHandle);
                        unionId = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, "union_id"));
                        
                        if (FormName == "FrmMFGBoilIssue_New")
                        {
                            unionId = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, "union_id"));
                            this.Close();
                            FrmMFGBoilIssue.FillGrid(unionId);
                        }
                        else if (FormName == "FrmMFGBoilRecieve_NEW")
                        {
                            unionId = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, "union_id"));
                            this.Close();
                            FrmMFGBoilRecieve.FillGrid(unionId);
                        }
                        else
                        {
                            this.Close();
                            FrmMFGBoilIssue.FillGrid(unionId);
                        }

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

        #region Other Function
        private void GetSummary()
        {
            try
            {
                double IntSelPcs = 0; double IntSelLot = 0; double DouSelCarat = 0;
                System.Data.DataTable DtTransfer = (System.Data.DataTable)MainGrid.DataSource;
                GrdDet.PostEditor();
                Global.DtTransfer.AcceptChanges();

                if (DtTransfer != null)
                {
                    if (DtTransfer.Rows.Count > 0)
                    {
                        foreach (DataRow DRow in DtTransfer.Rows)
                        {
                            if (Val.ToString(DRow["SEL"]) == "True")
                            {
                                IntSelLot = IntSelLot + 1;
                                IntSelPcs = IntSelPcs + Val.Val(DRow["pcs"]);
                                DouSelCarat = DouSelCarat + Val.Val(DRow["carat"]);
                            }
                        }
                    }
                }
                //txtSelLot.Text = IntSelLot.ToString();
                //txtSelPcs.Text = IntSelPcs.ToString();
                //txtSelCarat.Text = DouSelCarat.ToString();
            }
            catch
            {
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
                            GrdDet.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            GrdDet.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            GrdDet.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            GrdDet.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            GrdDet.ExportToText(Filepath);
                            break;
                        case "html":
                            GrdDet.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            GrdDet.ExportToCsv(Filepath);
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

        #region Repository Events

        private void repSelect_MouseUp(object sender, MouseEventArgs e)
        {
            GetSummary();
        }

        
        private void GrdDet_KeyUp(object sender, KeyEventArgs e)
        {

        }

        #endregion

        #region Checkbox Events


        private void GrdDet_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {

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
        private void GrdDet_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == "SEL")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                        e.TotalValue = m_numSelectedCount;
                }

            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        private void repSelect_CheckedChanged(object sender, EventArgs e)
        {
            GetSummary();
        }
        #endregion            
    }
}