using BLL;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Transaction;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Transaction
{
    public partial class FrmOpeningStock : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        OpeningStock ObjOpening;

        DataTable DTab;
        DataTable DTab_Data;
        DataTable Dshape;
        DataTable Dcolor;
        DataTable Dclarity;
        DataTable DCut;
        DataTable DPolish;
        DataTable DSym;
        DataTable DFlou;
        DataTable DAssort;
        DataTable DSieve;
        DataTable DtSieve;
        DataTable Dtshape;
        DataTable Dtcolor;
        DataTable Dtclarity;
        DataTable DtCut;
        DataTable DtFlou;
        DataTable DtAssort;

        int m_numForm_id = 0;
        int IntRes = 0;

        decimal m_numSummRate;
        #endregion

        #region Constructor
        public FrmOpeningStock()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            ObjOpening = new OpeningStock();

            DTab = new DataTable();
            DTab_Data = new DataTable();
            Dshape = new DataTable();
            Dcolor = new DataTable();
            Dclarity = new DataTable();
            DCut = new DataTable();
            DPolish = new DataTable();
            DSym = new DataTable();
            DFlou = new DataTable();
            DAssort = new DataTable();
            DSieve = new DataTable();
            DtSieve = new SieveMaster().GetData();
            Dtshape = new ShapeMaster().GetData();
            Dtcolor = new ColorMaster().GetData();
            Dtclarity = new ClarityMaster().GetData();
            DtCut = new CutMaster().GetData();
            DtFlou = new FlourescenceMaster().GetData();
            DtAssort = new AssortMaster().GetData();
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

        #region Events
        private void FrmOpeningStock_Load(object sender, EventArgs e)
        {
            DataTable DTab = ObjOpening.Check_Opening_Stock();
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (DTab.Rows.Count > 0)
                {
                    btnBrowse.Enabled = false;
                    //btnBrowse.Visible = false;
                }
                else
                {
                    btnBrowse.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                lstError.Add(new ListError(ex));
            }
        }
        private void FrmOpeningStock_Shown(object sender, System.EventArgs e)
        {
            try
            {
                dtpOpeningDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpOpeningDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpOpeningDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpOpeningDate.Properties.CharacterCasing = CharacterCasing.Upper;

                dtpOpeningDate.EditValue = DateTime.Now;

                dtpOpeningDate_EditValueChanged(null, null);
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

                var result = DateTime.Compare(Convert.ToDateTime(dtpOpeningDate.Text), DateTime.Today);
                if (result > 0)
                {
                    Global.Message("Opening Date Not Be Greater Than Today Date");
                    dtpOpeningDate.Focus();
                    return;

                }
                ObjPer.SetFormPer();
                if (ObjPer.AllowUpdate == false || ObjPer.AllowInsert == false)
                {
                    Global.Message(BLL.GlobalDec.gStrPermissionInsUpdMsg);
                    return;
                }
                this.Cursor = Cursors.WaitCursor;

                DTab_Data = (DataTable)grdOpeningStock.DataSource;

                foreach (DataRow DRow in DTab_Data.Rows)
                {
                    if (DTab_Data.Select("sieve ='" + Val.ToString(DRow["sieve"]) + "' And assort = '" + Val.ToString(DRow["assort"]) + "'").Length > 1)
                    {
                        Global.Message("Duplicate Value found in Sieve: " + Val.ToString(DRow["sieve"]) + " AND Assort: " + Val.ToString(DRow["assort"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (DRow["assort"] != null)
                    {
                        if (Val.ToString(DRow["assort"]) != "")
                        {
                            if (DtAssort.Select("assort_name ='" + Val.ToString(DRow["assort"]) + "'").Length > 0)
                            {
                                DAssort = DtAssort.Select("assort_name ='" + Val.ToString(DRow["assort"]) + "'").CopyToDataTable();
                            }
                            else
                            {
                                Global.Message("Assort Not found in Master" + Val.ToString(DRow["assort"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                    }
                    else
                    {
                        Global.Message("Assort Name are not found :" + Val.ToString(DRow["assort"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    if (DRow["sieve"] != null)
                    {
                        if (Val.ToString(DRow["sieve"]) != "")
                        {
                            if (DtSieve.Select("sieve_name ='" + Val.ToString(DRow["sieve"]) + "'").Length > 0)
                            {
                                DSieve = DtSieve.Select("sieve_name ='" + Val.ToString(DRow["sieve"]) + "'").CopyToDataTable();
                            }
                            else
                            {
                                Global.Message("Sieve Not found in Master" + Val.ToString(DRow["sieve"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                    }
                    if (DAssort.Rows.Count > 0 && DSieve.Rows.Count > 0)
                    {

                        DataTable DtRateCheck = new DataTable();
                        DtRateCheck = ObjOpening.Check_RateDetail(Val.ToInt(DAssort.Rows[0]["assort_id"]), Val.ToInt(DSieve.Rows[0]["sieve_id"]));
                        if (DtRateCheck.Rows.Count == 0)
                        {
                            Global.Message("Rate Detail Not found in Master Assort Name = " + Val.ToString(DRow["assort"]) + " AND Sieve Name = " + Val.ToString(DRow["sieve"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Cursor = Cursors.Default;
                            return;
                        }
                    }
                    else
                    {
                        Global.Message("Sieve Name are not found :" + Val.ToString(DRow["sieve"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Cursor = Cursors.Default;
                        return;
                    }
                }
                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;

                panelProgress.Visible = true;
                backgroundWorker_Opening.RunWorkerAsync();

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                this.Cursor = Cursors.Default;
                return;
            }
        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog OpenDialog = new OpenFileDialog();
                if (OpenDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }
                txtFileName.Text = OpenDialog.FileName;
                OpenDialog.Dispose();
                OpenDialog = null;

                if (File.Exists(txtFileName.Text) == false)
                {
                    Global.Message("File Is Not Exists To The Path");
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                grdOpeningStock.DataSource = null;

                System.Data.DataTable DTabFile = new System.Data.DataTable();

                if (txtFileName.Text.Length != 0)
                {
                    using (var pck = new ExcelPackage(new FileInfo(txtFileName.Text)))
                    {
                        ExcelWorksheet ws = pck.Workbook.Worksheets[1];
                        DTabFile = WorksheetToDataTable(ws, true);
                    }
                }
                grdOpeningStock.DataSource = DTabFile;
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                this.Cursor = Cursors.Default;
                return;
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtFileName.Text = "";
                dtpOpeningDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpOpeningDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpOpeningDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpOpeningDate.Properties.CharacterCasing = CharacterCasing.Upper;

                dtpOpeningDate.EditValue = DateTime.Now;
                grdOpeningStock.DataSource = null;
                dtpOpeningDate_EditValueChanged(null, null);
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void dtpOpeningDate_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                OpeningStockProperty objOpeningProperty = new OpeningStockProperty();

                objOpeningProperty.opening_date = Val.DBDate(dtpOpeningDate.Text);

                DataTable DTab = ObjOpening.Opening_GetData(objOpeningProperty);

                if (DTab.Rows.Count > 0)
                {
                    grdOpeningStock.DataSource = DTab;
                    dgvOpeningStock.BestFitColumns();
                    PanelSave.Visible = false;
                    //btnBrowse.Enabled = false;
                }
                else
                {
                    grdOpeningStock.DataSource = null;
                    dgvOpeningStock.BestFitColumns();
                    PanelSave.Visible = true;
                    btnBrowse.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void lblFormatSample_Click(object sender, EventArgs e)
        {
            Global.CopyFormat(System.Windows.Forms.Application.StartupPath + @"\FORMAT\Opening_Stock.xlsx", "Opening_Stock.xlsx", "xlsx");
        }
        private void backgroundWorker_Opening_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                {
                    Conn = new BeginTranConnection(true, false);
                }
                else
                {
                    Conn = new BeginTranConnection(false, true);
                }
                OpeningStockProperty objOpeningProperty = new OpeningStockProperty();
                try
                {
                    IntRes = 0;

                    int IntCounter = 0;
                    int Count = 0;
                    int TotalCount = DTab_Data.Rows.Count;

                    foreach (DataRow DRow in DTab_Data.Rows)
                    {
                        objOpeningProperty = new OpeningStockProperty();
                        objOpeningProperty.opening_date = Val.DBDate(dtpOpeningDate.Text);

                        if (DRow["assort"] != null)
                        {
                            if (Val.ToString(DRow["assort"]) != "")
                            {
                                if (DtAssort.Select("assort_name ='" + Val.ToString(DRow["assort"]) + "'").Length > 0)
                                {
                                    DAssort = DtAssort.Select("assort_name ='" + Val.ToString(DRow["assort"]) + "'").CopyToDataTable();
                                }
                            }
                        }

                        if (DRow["sieve"] != null)
                        {
                            if (Val.ToString(DRow["sieve"]) != "")
                            {
                                if (DtSieve.Select("sieve_name ='" + Val.ToString(DRow["sieve"]) + "'").Length > 0)
                                {
                                    DSieve = DtSieve.Select("sieve_name ='" + Val.ToString(DRow["sieve"]) + "'").CopyToDataTable();
                                }
                            }
                        }

                        if (DRow["shape"] != null)
                        {
                            if (Val.ToString(DRow["shape"]) != "")
                            {
                                if (Dtshape.Select("shape_name ='" + Val.ToString(DRow["shape"]) + "'").Length > 0)
                                {
                                    Dshape = Dtshape.Select("shape_name ='" + Val.ToString(DRow["shape"]) + "'").CopyToDataTable();
                                    objOpeningProperty.shape_id = Val.ToInt(Dshape.Rows[0]["shape_id"]);
                                }
                            }
                        }

                        if (DRow["color"] != null)
                        {
                            if (Val.ToString(DRow["color"]) != "")
                            {
                                if (Dtcolor.Select("color_name ='" + Val.ToString(DRow["color"]) + "'").Length > 0)
                                {
                                    Dcolor = Dtcolor.Select("color_name ='" + Val.ToString(DRow["color"]) + "'").CopyToDataTable();
                                    objOpeningProperty.color_id = Val.ToInt(Dcolor.Rows[0]["color_id"]);
                                }
                            }
                        }
                        if (DRow["clarity"] != null)
                        {
                            if (Val.ToString(DRow["clarity"]) != "")
                            {
                                if (Dtclarity.Select("Purity_name ='" + Val.ToString(DRow["clarity"]) + "'").Length > 0)
                                {
                                    Dclarity = Dtclarity.Select("Purity_name ='" + Val.ToString(DRow["clarity"]) + "'").CopyToDataTable();
                                    objOpeningProperty.purity_id = Val.ToInt(Dclarity.Rows[0]["purity_id"]);
                                }
                            }
                        }
                        if (DRow["Cut"] != null)
                        {
                            if (Val.ToString(DRow["Cut"]) != "")
                            {
                                if (DtCut.Select("cut_name ='" + Val.ToString(DRow["cut"]) + "'").Length > 0)
                                {
                                    DCut = DtCut.Select("cut_name ='" + Val.ToString(DRow["cut"]) + "'").CopyToDataTable();
                                    objOpeningProperty.cut_id = Val.ToInt(DCut.Rows[0]["cut_id"]);
                                }
                            }
                        }
                        if (DRow["polish"] != null)
                        {
                            if (Val.ToString(DRow["polish"]) != "")
                            {
                                if (DtCut.Select("cut_name ='" + Val.ToString(DRow["polish"]) + "'").Length > 0)
                                {
                                    DPolish = DtCut.Select("cut_name ='" + Val.ToString(DRow["polish"]) + "'").CopyToDataTable();
                                    objOpeningProperty.polish_id = Val.ToInt(DPolish.Rows[0]["cut_id"]);
                                }
                            }
                        }
                        if (DRow["symmetry"] != null)
                        {
                            if (Val.ToString(DRow["symmetry"]) != "")
                            {
                                if (DtCut.Select("cut_name ='" + Val.ToString(DRow["symmetry"]) + "'").Length > 0)
                                {
                                    DSym = DtCut.Select("cut_name ='" + Val.ToString(DRow["symmetry"]) + "'").CopyToDataTable();
                                    objOpeningProperty.symmetry_id = Val.ToInt(DSym.Rows[0]["cut_id"]);
                                }
                            }
                        }
                        if (DRow["fluorescence"] != null)
                        {
                            if (Val.ToString(DRow["fluorescence"]) != "")
                            {
                                if (DtFlou.Select("fluorescence_name ='" + Val.ToString(DRow["fluorescence"]) + "'").Length > 0)
                                {
                                    DFlou = DtFlou.Select("fluorescence_name ='" + Val.ToString(DRow["fluorescence"]) + "'").CopyToDataTable();
                                    objOpeningProperty.fluorescence_id = Val.ToInt(DFlou.Rows[0]["fluorescence_id"]);
                                }
                            }
                        }

                        objOpeningProperty.assort_id = Val.ToInt(DAssort.Rows[0]["assort_id"]);
                        objOpeningProperty.sieve_id = Val.ToInt(DSieve.Rows[0]["sieve_id"]);
                        objOpeningProperty.pcs = Val.ToInt(DRow["pcs"]);
                        objOpeningProperty.carat = Val.ToDecimal(DRow["carat"]);
                        objOpeningProperty.parakhrate = Val.ToDecimal(DRow["rate"]);
                        objOpeningProperty.parakhamount = Val.ToDecimal(DRow["amount"]);
                        objOpeningProperty.status_id = 12;

                        objOpeningProperty.currency_id = Val.ToInt32(GlobalDec.gEmployeeProperty.currency_id);

                        IntRes = ObjOpening.Save(objOpeningProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                        Count++;
                        IntCounter++;
                        IntRes++;
                        SetControlPropertyValue(lblProgressCount, "Text", Count.ToString() + "" + "/" + "" + TotalCount.ToString() + " Completed....");
                    }
                    if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                    {
                        Conn.Inter1.Commit();
                    }
                    else
                    {
                        Conn.Inter2.Commit();
                    }
                }
                catch (Exception ex)
                {
                    IntRes = -1;
                    if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                    {
                        Conn.Inter1.Rollback();
                    }
                    else
                    {
                        Conn.Inter2.Rollback();
                    }
                    Conn = null;
                    General.ShowErrors(ex.ToString());
                    return;
                }
                finally
                {
                    objOpeningProperty = null;
                }
            }
            catch (Exception ex)
            {
                IntRes = -1;
                if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                {
                    Conn.Inter1.Rollback();
                }
                else
                {
                    Conn.Inter2.Rollback();
                }
                Conn = null;
                Global.Message(ex.ToString());
                if (ex.InnerException != null)
                {
                    Global.Message(ex.InnerException.ToString());
                }
            }
        }
        private void backgroundWorker_Opening_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                panelProgress.Visible = false;
                if (IntRes > 0)
                {
                    Global.Confirm("Stock Details Data Save Successfully");
                    btnClear_Click(null, null);
                    this.Cursor = Cursors.Default;
                }
                else
                {
                    Global.Confirm("Error In Save Stock Details");
                    this.Cursor = Cursors.Default;
                    dtpOpeningDate.Focus();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }
        #region GridEvents
        private void dgvOpeningStock_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                if (Val.ToDecimal(Amount.SummaryItem.SummaryValue) > 0 && Val.ToDecimal(Carat.SummaryItem.SummaryValue) > 0)
                {
                    m_numSummRate = Math.Round((Val.ToDecimal(Amount.SummaryItem.SummaryValue) / Val.ToDecimal(Carat.SummaryItem.SummaryValue)), 3, MidpointRounding.AwayFromZero);

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
        #endregion

        #endregion

        #region Functions
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
                            dgvOpeningStock.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvOpeningStock.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvOpeningStock.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvOpeningStock.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvOpeningStock.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvOpeningStock.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvOpeningStock.ExportToCsv(Filepath);
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
