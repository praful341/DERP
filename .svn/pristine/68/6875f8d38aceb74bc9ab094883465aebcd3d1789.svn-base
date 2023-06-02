using BLL;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Report;
using BLL.FunctionClasses.Transaction;
using BLL.PropertyClasses.Report;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Master
{
    public partial class FrmRangeMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member
        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);
        RateTypeMaster objRateType = new RateTypeMaster();
        DataTable RateType = new DataTable();
        PriceImport ObjPrcImp;
        ReportParams_Property ReportParams_Property = new BLL.PropertyClasses.Report.ReportParams_Property();
        ReportParams ObjReportParams = new ReportParams();
        DataTable DtSieve;
        DataTable DtColor;
        DataTable DTab_Data;
        DataTable DColor;
        DataTable DSieve;
        DataTable m_dtbRateDetails;
        DataTable m_dtbPriceImport;
        int m_numForm_id;
        int i;
        int IntRes;
        #endregion

        #region Constructor
        public FrmRangeMaster()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();


            ObjPrcImp = new PriceImport();

            DtSieve = new SieveMaster().GetData();
            DtColor = new ColorMaster().GetData();
            DTab_Data = new DataTable();
            DColor = new DataTable();
            DSieve = new DataTable();
            m_dtbRateDetails = new DataTable();
            m_dtbPriceImport = new DataTable();

            m_numForm_id = 0;
            i = 0;
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
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion

        #region "Events" 
        private void FrmRangeMaster_Load(object sender, EventArgs e)
        {
            try
            {

                RepLueColor.DataSource = DtColor;
                RepLueColor.ValueMember = "color_id";
                RepLueColor.DisplayMember = "color_name";

                RepLueSieve.DataSource = DtSieve;
                RepLueSieve.ValueMember = "sieve_id";
                RepLueSieve.DisplayMember = "sieve_name";

                btnShow_Click(null, null);
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

                List<ListError> lstError = new List<ListError>();
                Dictionary<Control, string> rtnCtrls = new Dictionary<Control, string>();
                rtnCtrls = Global.CheckCompulsoryControls(Val.ToInt(ObjPer.form_id), this);
                if (rtnCtrls.Count > 0)
                {
                    foreach (KeyValuePair<Control, string> entry in rtnCtrls)
                    {
                        if (entry.Key is DevExpress.XtraEditors.LookUpEdit || entry.Key is DevExpress.XtraEditors.DateEdit || entry.Key is DevExpress.XtraEditors.TextEdit)
                        {
                            lstError.Add(new ListError(13, entry.Value));
                        }
                    }
                    rtnCtrls.First().Key.Focus();
                    BLL.General.ShowErrors(lstError);
                    Cursor.Current = Cursors.Arrow;
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                DTab_Data = new DataTable();
                DTab_Data.AcceptChanges();
                DTab_Data = (DataTable)grdRange.DataSource;

                if (DTab_Data != null)
                {

                }
                else
                {
                    Global.Message("Data not found in Grid Please check data");
                    this.Cursor = Cursors.Default;
                    return;
                }

                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;

                panelProgress.Visible = true;
                backgroundWorker_PriceImport.RunWorkerAsync();

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                this.Cursor = Cursors.Default;
                return;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                m_dtbPriceImport.Rows.Clear();
                grdRange.DataSource = null;
                btnShow_Click(null, null);
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void backgroundWorker_PriceImport_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                Conn = new BeginTranConnection(true, false);

                PriceImportProperty objPrcImpProperty = new PriceImportProperty();
                try
                {
                    IntRes = 0;
                    int IntCounter = 0;
                    int Count = 0;
                    DTab_Data.AcceptChanges();
                    int TotalCount = DTab_Data.Rows.Count;
                    i = 0;

                    objPrcImpProperty.form_id = m_numForm_id;

                    foreach (DataRow DRow in DTab_Data.Rows)
                    {
                        objPrcImpProperty = new PriceImportProperty();

                        if (Val.ToString(DRow["range_id"]) == "")
                        {
                            objPrcImpProperty.range_id = Val.ToInt64(0);
                        }
                        else
                        {
                            objPrcImpProperty.range_id = Val.ToInt64(DRow["range_id"]);
                        }

                        objPrcImpProperty.color_id = Val.ToInt(DRow["color_id"]);
                        objPrcImpProperty.sieve_id = Val.ToInt(DRow["sieve_id"]);

                        if (DRow["color_id"] != null)
                        {
                            if (Val.ToString(DRow["color_id"]) != "")
                            {
                                if (DtColor.Select("color_id =" + Val.ToInt(DRow["color_id"])).Length > 0)
                                {
                                    DColor = DtColor.Select("color_id =" + Val.ToString(DRow["color_id"])).CopyToDataTable();
                                }
                                else
                                {
                                    Global.Message("Color Not found in Master" + Val.ToString(DRow["color_name"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.Cursor = Cursors.Default;
                                    return;
                                }
                            }
                        }

                        if (DRow["sieve_id"] != null)
                        {
                            if (Val.ToString(DRow["sieve_id"]) != "")
                            {
                                if (DtSieve.Select("sieve_id =" + Val.ToInt(DRow["sieve_id"])).Length > 0)
                                {
                                    DSieve = DtSieve.Select("sieve_id =" + Val.ToString(DRow["sieve_id"])).CopyToDataTable();
                                }
                                else
                                {
                                    Global.Message("Sieve Not found in Master" + Val.ToString(DRow["sieve_name"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.Cursor = Cursors.Default;
                                    return;
                                }
                            }
                        }
                        objPrcImpProperty.color_name = Val.ToString(DColor.Rows[0]["color_name"]);
                        objPrcImpProperty.size_name = Val.ToString(DSieve.Rows[0]["sieve_name"]);
                        objPrcImpProperty.from_rate = Val.ToDecimal(DRow["from_rate"]);
                        objPrcImpProperty.to_rate = Val.ToDecimal(DRow["to_rate"]);
                        objPrcImpProperty.range = Val.ToString(DRow["range"]);
                        objPrcImpProperty.count = 0;
                        i++;
                        objPrcImpProperty.sequence_no = i;
                        IntRes = ObjPrcImp.RangeMasterSave(objPrcImpProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                        Count++;
                        IntCounter++;
                        IntRes++;
                        SetControlPropertyValue(lblProgressCount, "Text", Count.ToString() + "" + "/" + "" + TotalCount.ToString() + " Completed....");
                    }
                    Conn.Inter1.Commit();
                }
                catch (Exception ex)
                {
                    panelProgress.Visible = false;
                    IntRes = -1;
                    Conn.Inter1.Rollback();
                    Conn = null;
                    General.ShowErrors(ex.ToString());
                    return;
                }
                finally
                {
                    objPrcImpProperty = null;
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
        private void backgroundWorker_PriceImport_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                panelProgress.Visible = false;
                if (IntRes > 0)
                {
                    Global.Confirm("Range Master Data Save Successfully");
                    i = 0;
                    btnClear_Click(null, null);
                    this.Cursor = Cursors.Default;
                }
                else
                {
                    Global.Confirm("Error In Range Master");
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }
        #endregion

        #region "Function"
        public System.Data.DataTable WorksheetToDataTable(ExcelWorksheet ws, bool hasHeader = true)
        {
            System.Data.DataTable dt = new System.Data.DataTable(ws.Name);
            int totalCols = ws.Dimension.End.Column;
            int totalRows = ws.Dimension.End.Row;
            int startRow = hasHeader ? 2 : 1;
            ExcelRange wsRow;
            DataRow dr;
            foreach (var firstRowCell in ws.Cells[1, 1, 1, totalCols])
            {
                dt.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
            }

            for (int rowNum = startRow; rowNum <= totalRows; rowNum++)
            {
                wsRow = ws.Cells[rowNum, 1, rowNum, totalCols];
                dr = dt.NewRow();
                foreach (var cell in wsRow)
                {
                    dr[cell.Start.Column - 1] = cell.Text;
                }

                dt.Rows.Add(dr);
            }

            return dt;
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
                            dgvRange.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvRange.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvRange.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvRange.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvRange.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvRange.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvRange.ExportToCsv(Filepath);
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

        private void btnShow_Click(object sender, EventArgs e)
        {
            DataTable dtRange = new DataTable();
            dtRange = objRateType.GetRangeData();
            if (dtRange.Rows.Count > 0)
            {
                grdRange.DataSource = dtRange;
            }
            else
            {
                grdRange.DataSource = null;
            }
        }
        private void RepDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (Global.Confirm("Are you sure delete selected row?", "Price", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                PriceImportProperty PriceImportProperty = new PriceImportProperty();
                int IntRes = 0;
                Int64 Range_ID = Val.ToInt64(dgvRange.GetFocusedRowCellValue("range_id").ToString());
                PriceImportProperty.range_id = Val.ToInt64(Range_ID);

                if (Range_ID == 0)
                {
                    dgvRange.DeleteRow(dgvRange.GetRowHandle(dgvRange.FocusedRowHandle));
                }
                else
                {
                    IntRes = ObjPrcImp.DeleteRangeMaster(PriceImportProperty);
                    dgvRange.DeleteRow(dgvRange.GetRowHandle(dgvRange.FocusedRowHandle));
                }

                if (IntRes == -1)
                {
                    Global.Confirm("Error in Detail Deleted Data.");
                    PriceImportProperty = null;
                }
                else
                {
                    Global.Confirm("Detail Deleted successfully...");
                    PriceImportProperty = null;
                }
            }
        }
    }
}
