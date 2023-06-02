using BLL.FunctionClasses.Transaction.MFG;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using static DERP.Class.Global;
using Global = DERP.Class.Global;

namespace DERP.Transaction
{
    public partial class FrmMfgLiveStock : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        MFGLiveStock objLiveStock;

        decimal m_numSummRate = 0;
        public string GblLockBarcode { get; set; }
        DataTable DTabSummary = new DataTable();

        #endregion

        #region Constructor
        public FrmMfgLiveStock()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            objLiveStock = new MFGLiveStock();
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
            DTabSummary.Columns.Add(new DataColumn("type", typeof(string)));
            DTabSummary.Columns.Add(new DataColumn("process_name", typeof(string)));
            DTabSummary.Columns.Add(new DataColumn("sub_process_name", typeof(string)));
            DTabSummary.Columns.Add(new DataColumn("lot_id", typeof(string)));
            DTabSummary.Columns.Add(new DataColumn("org_pcs", typeof(int)));
            DTabSummary.Columns.Add(new DataColumn("balance_pcs", typeof(int)));
            DTabSummary.Columns.Add(new DataColumn("balance_carat", typeof(double)));

            this.Show();
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

        #region Events
        private void FrmMfgLiveStock_Load(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                GetData();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.ToString());
            }
            FillSummaryGrid();
        }
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                GetData();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.ToString());
            }
        }
        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Export("xls", "Export to Excel", "Excel files 97-2003 (*.xls)|*.xls|Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*");
        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Export("pdf", "Export Report to PDF", "PDF (*.PDF)|*.PDF");
        }
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Export("txt", "Export to Text", "Text files (*.txt)|*.txt|All files (*.*)|*.*");
        }
        private void dgvMfgLiveStock_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                if (Val.ToDecimal(clmAmount.SummaryItem.SummaryValue) > 0 && Val.ToDecimal(clmCarat.SummaryItem.SummaryValue) > 0)
                {
                    m_numSummRate = Math.Round((Val.ToDecimal(clmAmount.SummaryItem.SummaryValue) / Val.ToDecimal(clmCarat.SummaryItem.SummaryValue)), 0, MidpointRounding.AwayFromZero);

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
        private void btnSummaryExport_Click(object sender, EventArgs e)
        {
            try
            {
                string dlgFilter = "Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                string format = "xlsx";
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = "xlsx";
                svDialog.Title = "Export to Excel";
                svDialog.FileName = "Report";
                svDialog.Filter = dlgFilter;
                GrdDetSummary.OptionsPrint.PrintHeader = true;
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    string Filepath = svDialog.FileName;
                    switch (format)
                    {
                        case "xls":
                            GrdDetSummary.OptionsPrint.AutoWidth = false;
                            MainGridSummary.ExportToXls(Filepath);
                            GrdDetSummary.OptionsPrint.AutoWidth = true;
                            break;
                        case "xlsx":
                            GrdDetSummary.OptionsPrint.AutoWidth = false;
                            MainGridSummary.ExportToXlsx(Filepath);
                            GrdDetSummary.OptionsPrint.AutoWidth = true;
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
        private void dgvMfgLiveStock_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (e.Clicks == 2)
                {
                    if (dgvMfgLiveStock.FocusedRowHandle < 0)
                    {
                        return;
                    }
                    Int64 Lot_ID = Val.ToInt64(dgvMfgLiveStock.GetRowCellValue(dgvMfgLiveStock.FocusedRowHandle, "lot_id"));

                    FrmMfgHistoryView objMfgHistoryView = new FrmMfgHistoryView();
                    Assembly frmAssembly = Assembly.LoadFile(Application.ExecutablePath);

                    foreach (Type type in frmAssembly.GetTypes())
                    {
                        string type1 = type.Name.ToString().ToUpper();
                        if (type.BaseType == typeof(DevExpress.XtraEditors.XtraForm))
                        {
                            if (type.Name.ToString().ToUpper() == "FRMMFGHISTORYVIEW")
                            {
                                XtraForm frmShow = (XtraForm)frmAssembly.CreateInstance(type.ToString());
                                frmShow.MdiParent = Global.gMainFormRef;

                                frmShow.GetType().GetMethod("ShowForm_New").Invoke(frmShow, new object[] { Lot_ID });
                                break;
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region Functions
        public void GetData()
        {
            Global.DtTransfer = objLiveStock.GetData();
            if (Global.DtTransfer.Rows.Count > 0)
            {
                grdMfgLiveStock.DataSource = Global.DtTransfer;
            }
            else
            {
                grdMfgLiveStock.DataSource = null;
            }
        }
        public void FillSummaryGrid()
        {
            DTabSummary.Rows.Clear();
            DataTable DtTransfer = new DataTable();

            DtTransfer = Global.DtTransfer;

            string[] grpArray = new string[3] { "process_name", "kapan_no", "sub_process_name" };
            DataTable temp = new DataTable();
            for (int i = 0; i < grpArray.Length; i++)
            {
                if (grpArray[i] == "process_name")
                {
                    temp = DtTransfer;
                }
                else if (grpArray[i] == "kapan_no")
                {
                    temp = DtTransfer;
                }
                else if (grpArray[i] == "sub_process_name")
                {
                    temp = DtTransfer;
                }

                var query = temp.AsEnumerable()
                                       .GroupBy(w => new
                                       {
                                           process_name = w.Field<string>(grpArray[i]),
                                           kapan_no = w.Field<string>(grpArray[i]),
                                           sub_process_name = w.Field<string>(grpArray[i])
                                       })
                                                .Select(x => new
                                                {
                                                    process_name = x.Key.process_name,
                                                    kapan_no = x.Key.kapan_no,
                                                    sub_process_name = x.Key.sub_process_name,
                                                    lot_id = x.Count(),
                                                    org_pcs = x.Sum(w => Val.Val(w["org_pcs"])),
                                                    balance_pcs = x.Sum(w => Val.Val(w["balance_pcs"])),
                                                    balance_carat = x.Sum(w => Val.Val(w["balance_carat"]))
                                                });
                DataTable DTProcessWise = LINQToDataTable(query);
                foreach (DataRow DRow in DTProcessWise.Rows)
                {
                    if (Val.ToString(DRow["process_name"]).Trim().Equals(string.Empty))
                        continue;
                    DataRow DRNew = DTabSummary.NewRow();
                    DRNew["type"] = grpArray[i];
                    DRNew["process_name"] = DRow["process_name"];
                    DRNew["sub_process_name"] = DRow["sub_process_name"];
                    DRNew["lot_id"] = DRow["lot_id"];
                    DRNew["org_pcs"] = Val.ToInt(DRow["org_pcs"]);
                    DRNew["balance_pcs"] = Val.ToInt(DRow["balance_pcs"]);
                    DRNew["balance_carat"] = Math.Round(Val.Val(DRow["balance_carat"]), 3);
                    DTabSummary.Rows.Add(DRNew);
                }
            }

            MainGridSummary.DataSource = DTabSummary;
            GrdDetSummary.BestFitColumns();
            GrdDetSummary.Columns["type"].Group();
            GrdDetSummary.ExpandAllGroups();
        }
        public DataTable LINQToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            PropertyInfo[] oProps = null;
            if (varlist == null) return dtReturn;
            foreach (T rec in varlist)
            {

                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;
                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }
                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }
                DataRow dr = dtReturn.NewRow();
                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }
                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }

        #endregion

        #region Context Menu Events
        private void MNExportToExcel_Click(object sender, EventArgs e)
        {
            Export("xls", "Export to Excel", "Excel files 97-2003 (*.xls)|*.xls|Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*");
        }
        private void MNExportToText_Click(object sender, EventArgs e)
        {
            Export("txt", "Export to Text", "Text files (*.txt)|*.txt|All files (*.*)|*.*");
        }
        private void MNExportToHTML_Click(object sender, EventArgs e)
        {
            Export("html", "Export to HTML", "Html files (*.html)|*.html|Htm files (*.htm)|*.htm");
        }
        private void MNExportToRTF_Click(object sender, EventArgs e)
        {
            Export("rtf", "Export to RTF", "Word (*.doc) |*.doc;*.rtf|(*.txt) |*.txt|(*.*) |*.*");
        }
        private void MNExportToPDF_Click(object sender, EventArgs e)
        {
            Export("pdf", "Export Report to PDF", "PDF (*.PDF)|*.PDF");
        }
        private void MNExportCSV_Click(object sender, EventArgs e)
        {
            Export("csv", "Export Report to CSVB", "csv (*.csv)|*.csv");
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
                            dgvMfgLiveStock.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvMfgLiveStock.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvMfgLiveStock.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvMfgLiveStock.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvMfgLiveStock.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvMfgLiveStock.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvMfgLiveStock.ExportToCsv(Filepath);
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
        private void Export_Split(string format, string dlgHeader, string dlgFilter)
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
                            dgvMfgLiveStock.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvMfgLiveStock.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvMfgLiveStock.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvMfgLiveStock.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvMfgLiveStock.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvMfgLiveStock.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvMfgLiveStock.ExportToCsv(Filepath);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString(), "Error in Export");
            }
        }

        #endregion
    }
}