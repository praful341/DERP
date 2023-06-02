using BLL;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Master.Store;
using BLL.FunctionClasses.Report;
using BLL.FunctionClasses.Transaction;
using BLL.PropertyClasses.Report;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Transaction
{
    public partial class FrmStorePartyOpening : DevExpress.XtraEditors.XtraForm
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

        DataTable DtControlSettings;
        DataTable DtParty;
        DataTable DtItem;
        DataTable DtSubItem;
        DataTable DTabFile;
        DataTable DTab_Data;
        DataTable DParty;
        DataTable DItem;
        DataTable DSubItem;
        DataTable m_dtbRateDetails;
        DataTable m_dtbPriceImport;
        int m_numForm_id;
        int IntRes;
        int m_RateId;
        #endregion

        #region Constructor
        public FrmStorePartyOpening()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            ObjPrcImp = new PriceImport();

            DtControlSettings = new DataTable();
            DtParty = new StorePartyMaster().GetData();
            DtItem = new MfgItemMaster().GetData();
            DtSubItem = new MfgSubItemMaster().GetData();
            DTabFile = new DataTable();
            DTab_Data = new DataTable();
            DParty = new DataTable();
            DItem = new DataTable();
            DSubItem = new DataTable();
            m_dtbRateDetails = new DataTable();
            m_dtbPriceImport = new DataTable();

            m_numForm_id = 0;           
            IntRes = 0;
            m_RateId = 0;
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
        private void FrmPriceImport_Shown(object sender, System.EventArgs e)
        {
            try
            {
                dtpDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpDate.Properties.CharacterCasing = CharacterCasing.Upper;

                dtpDate.EditValue = DateTime.Now;
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
                var result = DateTime.Compare(Convert.ToDateTime(dtpDate.Text), DateTime.Today);
                if (result > 0)
                {
                    Global.Message("Date Not Be Greater Than Today Date");
                    dtpDate.Focus();
                    return;

                }
                ObjPer.SetFormPer();
                if (ObjPer.AllowUpdate == false || ObjPer.AllowInsert == false)
                {
                    Global.Message(BLL.GlobalDec.gStrPermissionInsUpdMsg);
                    return;
                }

                //List<ListError> lstError = new List<ListError>();
                //Dictionary<Control, string> rtnCtrls = new Dictionary<Control, string>();
                //rtnCtrls = Global.CheckCompulsoryControls(Val.ToInt(ObjPer.form_id), this);
                //if (rtnCtrls.Count > 0)
                //{
                //    foreach (KeyValuePair<Control, string> entry in rtnCtrls)
                //    {
                //        if (entry.Key is DevExpress.XtraEditors.LookUpEdit || entry.Key is DevExpress.XtraEditors.DateEdit || entry.Key is DevExpress.XtraEditors.TextEdit)
                //        {
                //            lstError.Add(new ListError(13, entry.Value));
                //        }
                //    }
                //    rtnCtrls.First().Key.Focus();
                //    BLL.General.ShowErrors(lstError);
                //    Cursor.Current = Cursors.Arrow;
                //    return;
                //}

                this.Cursor = Cursors.WaitCursor;
                DTab_Data = new DataTable();
                DTab_Data = (DataTable)grdPartyOpening.DataSource;
                if (m_RateId != 0)
                {
                    if (DTab_Data.Rows.Count > 0)
                    {
                        foreach (DataRow DRow in DTab_Data.Rows)
                        {
                            if (DRow["party_id"] != null)
                            {
                                if (Val.ToString(DRow["party_id"]) != "")
                                {
                                    if (DtParty.Select("party_id =" + Val.ToInt(DRow["party_id"])).Length > 0)
                                    {
                                        DParty = DtParty.Select("party_id =" + Val.ToString(DRow["party_id"])).CopyToDataTable();
                                    }
                                    else
                                    {
                                        Global.Message("Store Party Not found in Master" + Val.ToString(DRow["party"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        this.Cursor = Cursors.Default;
                                        return;
                                    }
                                }
                            }
                            if (DRow["item_id"] != null)
                            {
                                if (Val.ToString(DRow["item_id"]) != "")
                                {
                                    if (DtItem.Select("item_id ='" + Val.ToString(DRow["item_id"]) + "'").Length > 0)
                                    {
                                        DItem = DtItem.Select("item_id ='" + Val.ToString(DRow["item_id"]) + "'").CopyToDataTable();
                                    }
                                    else
                                    {
                                        Global.Message("Store Item Not found in Master" + Val.ToString(DRow["item"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        this.Cursor = Cursors.Default;
                                        return;
                                    }
                                }
                            }
                            if (DRow["sub_item_id"] != null)
                            {
                                if (Val.ToString(DRow["sub_item_id"]) != "")
                                {
                                    if (DtSubItem.Select("sub_item_id ='" + Val.ToString(DRow["sub_item_id"]) + "'").Length > 0)
                                    {
                                        DSubItem = DtSubItem.Select("sub_item_id ='" + Val.ToString(DRow["sub_item_id"]) + "'").CopyToDataTable();
                                    }
                                    else
                                    {
                                        Global.Message("Store Sub Item Not found in Master" + Val.ToString(DRow["sub_item"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        this.Cursor = Cursors.Default;
                                        return;
                                    }
                                }
                            }
                            if (DParty.Rows.Count > 0)
                            {
                                DRow["party"] = Val.ToString(DParty.Rows[0]["party_name"]);
                            }
                            if (DItem.Rows.Count > 0)
                            {
                                DRow["item"] = Val.ToString(DItem.Rows[0]["item_name"]);
                            }
                            if (DSubItem.Rows.Count > 0)
                            {
                                DRow["sub_item"] = Val.ToString(DSubItem.Rows[0]["sub_item_name"]);
                            }
                        }
                    }
                }

                int rowNo = 1;

                //DTab_Data = (DataTable)grdPriceImport.DataSource;

                DataTable distinct = DTab_Data.DefaultView.ToTable(true, "party", "item", "sub_item");
                if (distinct.Rows.Count != DTab_Data.Rows.Count)
                {
                    Global.Message("Please Check File Duplicate Data Found!!");
                    this.Cursor = Cursors.Default;
                    return;
                }
                if (DTab_Data != null)
                {
                    foreach (DataRow DRow in DTab_Data.Rows)
                    {
                        if (DRow["party"] != null)
                        {
                            if (Val.ToString(DRow["party"]) != "")
                            {
                                if (DtParty.Select("party_name ='" + Val.ToString(DRow["party"]) + "'").Length > 0)
                                {
                                    DParty = DtParty.Select("party_name ='" + Val.ToString(DRow["party"]) + "'").CopyToDataTable();
                                }
                                else
                                {
                                    Global.Message("Store Party Not found in Master" + Val.ToString(DRow["party"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                            else
                            {
                                Global.Message("Store Party Name Blank at Row No :" + rowNo);
                                this.Cursor = Cursors.Default;
                                return;
                            }
                        }
                        else
                        {
                            Global.Message("Store Party Name are not found :" + Val.ToString(DRow["party"]));
                            this.Cursor = Cursors.Default;
                            return;
                        }
                        if (DRow["item"] != null)
                        {
                            if (Val.ToString(DRow["item"]) != "")
                            {
                                if (DtItem.Select("item_name ='" + Val.ToString(DRow["item"]) + "'").Length > 0)
                                {
                                    DItem = DtItem.Select("item_name ='" + Val.ToString(DRow["item"]) + "'").CopyToDataTable();
                                }
                                else
                                {
                                    Global.Message("Store Item Not found in Master" + Val.ToString(DRow["item"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                            else
                            {
                                Global.Message("Store Item Name Blank at Row No :" + rowNo);
                                this.Cursor = Cursors.Default;
                                return;
                            }
                        }
                        else
                        {
                            Global.Message("Store Item Name are not found :" + Val.ToString(DRow["item"]));
                            this.Cursor = Cursors.Default;
                            return;
                        }
                        if (DRow["sub_item"] != null)
                        {
                            if (Val.ToString(DRow["sub_item"]) != "")
                            {
                                if (DtSubItem.Select("sub_item_name ='" + Val.ToString(DRow["sub_item"]) + "'").Length > 0)
                                {
                                    DSubItem = DtSubItem.Select("sub_item_name ='" + Val.ToString(DRow["sub_item"]) + "'").CopyToDataTable();
                                }
                                else
                                {
                                    Global.Message("Store Item Not found in Master" + Val.ToString(DRow["sub_item"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                            else
                            {
                                Global.Message("Store Sub Item Name Blank at Row No :" + rowNo);
                                this.Cursor = Cursors.Default;
                                return;
                            }
                        }
                        else
                        {
                            Global.Message("Store Sub Item Name are not found :" + Val.ToString(DRow["item"]));
                            this.Cursor = Cursors.Default;
                            return;
                        }
                        rowNo++;
                    }
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
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            List<ListError> lstError = new List<ListError>();
            Dictionary<Control, string> rtnCtrls = new Dictionary<Control, string>();
            rtnCtrls = Global.CheckCompulsoryControls(Val.ToInt(ObjPer.form_id), this);
            try
            {
                if (Val.ToString(dtpDate.Text) != null)
                {
                    OpenFileDialog OpenDialog = new OpenFileDialog();
                    if (OpenDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                    {
                        return;
                    }
                    txtFileName.Text = OpenDialog.FileName;
                    OpenDialog.Dispose();
                    OpenDialog = null;
                    DTabFile.Rows.Clear();
                    try
                    {
                        Stream s = File.Open(txtFileName.Text, FileMode.Open, FileAccess.Read, FileShare.None);
                        s.Close();
                    }
                    catch (Exception ex)
                    {
                        Global.Message("File Is open please close the excel file.");
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    if (File.Exists(txtFileName.Text) == false)
                    {
                        Global.Message("File Is Not Exists To The Path");
                        return;
                    }

                    this.Cursor = Cursors.WaitCursor;
                    grdPartyOpening.DataSource = null;

                    if (txtFileName.Text.Length != 0)
                    {
                        using (var pck = new ExcelPackage(new FileInfo(txtFileName.Text)))
                        {
                            ExcelWorksheet ws = pck.Workbook.Worksheets[1];
                            DTabFile = WorksheetToDataTable(ws, true);
                        }
                    }

                    m_dtbPriceImport = new DataTable();
                    m_dtbPriceImport.Columns.Add("party_id", typeof(int)).DefaultValue = 0;
                    m_dtbPriceImport.Columns.Add("party", typeof(string));
                    m_dtbPriceImport.Columns.Add("item_id", typeof(int)).DefaultValue = 0;
                    m_dtbPriceImport.Columns.Add("item", typeof(string));
                    m_dtbPriceImport.Columns.Add("sub_item_id", typeof(int)).DefaultValue = 0;
                    m_dtbPriceImport.Columns.Add("sub_item", typeof(string));
                    m_dtbPriceImport.Columns.Add("qty", typeof(decimal)).DefaultValue = 0;
                    m_dtbPriceImport.Columns.Add("rate", typeof(decimal)).DefaultValue = 0;
                    m_dtbPriceImport.Columns.Add("amount", typeof(decimal)).DefaultValue = 0;
                    if (DTabFile.Rows.Count > 0)
                    {
                        foreach (DataRow DRow in DTabFile.Rows)
                        {
                            if (DRow["party"] != null)
                            {
                                if (Val.ToString(DRow["party"]) != "")
                                {
                                    if (DtParty.Select("party_name ='" + Val.ToString(DRow["party"]) + "'").Length > 0)
                                    {
                                        DParty = DtParty.Select("party_name ='" + Val.ToString(DRow["party"]) + "'").CopyToDataTable();
                                    }
                                    else
                                    {
                                        Global.Message("Store Party Not found in Master : " + Val.ToString(DRow["party"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        this.Cursor = Cursors.Default;
                                        return;
                                    }
                                }
                            }
                            if (DRow["item"] != null)
                            {
                                if (Val.ToString(DRow["item"]) != "")
                                {
                                    if (DtItem.Select("item_name ='" + Val.ToString(DRow["item"]) + "'").Length > 0)
                                    {
                                        DItem = DtItem.Select("item_name ='" + Val.ToString(DRow["item"]) + "'").CopyToDataTable();
                                    }
                                    else
                                    {
                                        Global.Message("Store Item Not found in Master : " + Val.ToString(DRow["item"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        this.Cursor = Cursors.Default;
                                        return;
                                    }
                                }
                            }
                            if (DRow["sub_item"] != null)
                            {
                                if (Val.ToString(DRow["sub_item"]) != "")
                                {
                                    if (DtSubItem.Select("sub_item_name ='" + Val.ToString(DRow["sub_item"]) + "'").Length > 0)
                                    {
                                        DSubItem = DtSubItem.Select("sub_item_name ='" + Val.ToString(DRow["sub_item"]) + "'").CopyToDataTable();
                                    }
                                    else
                                    {
                                        Global.Message("Store Sub Item Not found in Master : " + Val.ToString(DRow["sub_item"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        this.Cursor = Cursors.Default;
                                        return;
                                    }
                                }
                            }
                            DataRow drwNew = m_dtbPriceImport.NewRow();
                            if (DParty.Rows.Count > 0)
                            {
                                drwNew["party_id"] = Val.ToInt(DParty.Rows[0]["party_id"]);
                                drwNew["party"] = Val.ToString(DParty.Rows[0]["party_name"]);
                            }
                            if (DItem.Rows.Count > 0)
                            {
                                drwNew["item_id"] = Val.ToInt(DItem.Rows[0]["item_id"]);
                                drwNew["item"] = Val.ToString(DItem.Rows[0]["item_name"]);
                            }
                            if (DSubItem.Rows.Count > 0)
                            {
                                drwNew["sub_item_id"] = Val.ToInt(DSubItem.Rows[0]["sub_item_id"]);
                                drwNew["sub_item"] = Val.ToString(DSubItem.Rows[0]["sub_item_name"]);
                            }
                            if (Val.ToDecimal(DRow["qty"]) > 0)
                            {
                                drwNew["qty"] = Val.ToDecimal(DRow["qty"]);
                            }
                            m_dtbPriceImport.Rows.Add(drwNew);

                            dgvPartyOpening.MoveLast();
                        }
                    }
                    grdPartyOpening.DataSource = m_dtbPriceImport;

                    this.Cursor = Cursors.Default;
                }
                else
                {
                    Global.Message("Date should not be blank");
                    return;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
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
                txtFileName.Text = "";
                dtpDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpDate.Properties.CharacterCasing = CharacterCasing.Upper;

                dtpDate.EditValue = DateTime.Now;
                m_dtbPriceImport.Rows.Clear();
                DTabFile.Rows.Clear();
                grdPartyOpening.DataSource = null;
                dtpDate_EditValueChanged(null, null);
                dtpDate.Focus();
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void lblFormatSample_Click(object sender, EventArgs e)
        {
            Global.CopyFormat(System.Windows.Forms.Application.StartupPath + @"\FORMAT\PartyOpening.xlsx", "PartyOpening.xlsx", "xlsx");
        }
        private void backgroundWorker_PriceImport_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                Conn = new BeginTranConnection(true, false);

                StorePartyOpeningProperty objPartyOpeningProperty = new StorePartyOpeningProperty();
                try
                {
                    IntRes = 0;
                    int IntCounter = 0;
                    int Count = 0;
                    int TotalCount = DTab_Data.Rows.Count;

                    foreach (DataRow DRow in DTab_Data.Rows)
                    {
                        objPartyOpeningProperty = new StorePartyOpeningProperty();
                        objPartyOpeningProperty.opening_date = Val.DBDate(dtpDate.Text);
                        objPartyOpeningProperty.party_id = Val.ToInt64(DRow["party_id"]);
                        objPartyOpeningProperty.item_id = Val.ToInt(DRow["item_id"]);
                        objPartyOpeningProperty.sub_item_id = Val.ToInt(DRow["sub_item_id"]);

                        objPartyOpeningProperty.qty = Val.ToDecimal(DRow["qty"]);
                        objPartyOpeningProperty.rate = Val.ToDecimal(DRow["rate"]);
                        objPartyOpeningProperty.amount = Val.ToDecimal(DRow["amount"]);

                        IntRes = ObjPrcImp.PartyOpeningSave(objPartyOpeningProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                        Count++;
                        IntCounter++;
                        IntRes++;
                        SetControlPropertyValue(lblProgressCount, "Text", Count.ToString() + "" + "/" + "" + TotalCount.ToString() + " Completed....");
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
                    objPartyOpeningProperty = null;
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
                    Global.Confirm("Store Party Opening Data Save Successfully");
                    btnClear_Click(null, null);
                    this.Cursor = Cursors.Default;
                }
                else
                {
                    Global.Confirm("Error In Store Party Opening Import");
                    this.Cursor = Cursors.Default;
                    dtpDate.Focus();
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
                            dgvPartyOpening.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvPartyOpening.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvPartyOpening.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvPartyOpening.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvPartyOpening.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvPartyOpening.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvPartyOpening.ExportToCsv(Filepath);
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
            //Global.Export("xlsx", dgvRoughClarityMaster);
            Export("xlsx", "Export to Excel", "Excel files 97-2003 (Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*");
        }
        private void MNExportPDF_Click(object sender, EventArgs e)
        {
            // Global.Export("pdf", dgvRoughClarityMaster);
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

        private void dtpDate_EditValueChanged(object sender, EventArgs e)
        {
            m_RateId = 0;
            if (Val.ToString(dtpDate.Text) != null)
            {
                DataTable dtPartyOpening = new DataTable();
                dtPartyOpening = objRateType.GetPartyOpeningData(Val.DBDate(dtpDate.Text));
                if (dtPartyOpening.Rows.Count > 0)
                {
                    //m_dtbPriceImport = new DataTable();
                    //m_dtbPriceImport.Columns.Add("party_id", typeof(int)).DefaultValue = 0;
                    //m_dtbPriceImport.Columns.Add("party", typeof(string));
                    //m_dtbPriceImport.Columns.Add("item_id", typeof(int)).DefaultValue = 0;
                    //m_dtbPriceImport.Columns.Add("item", typeof(string));
                    //m_dtbPriceImport.Columns.Add("sub_item_id", typeof(int)).DefaultValue = 0;
                    //m_dtbPriceImport.Columns.Add("sub_item", typeof(string));
                    //m_dtbPriceImport.Columns.Add("qty", typeof(decimal)).DefaultValue = 0;
                    //m_dtbPriceImport.Columns.Add("rate", typeof(decimal)).DefaultValue = 0;
                    //m_dtbPriceImport.Columns.Add("amount", typeof(decimal)).DefaultValue = 0;
                    grdPartyOpening.DataSource = dtPartyOpening;
                }
                else
                {
                    grdPartyOpening.DataSource = null;
                    m_RateId = 0;
                }
            }
        }

        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (dtpDate.Text == "")
                {
                    lstError.Add(new ListError(13, "Date"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        dtpDate.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                lstError.Add(new ListError(ex));
            }
            return (!(BLL.General.ShowErrors(lstError)));
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (!ValidateDetails())
            {
                return;
            }

            DataTable dtPartyOpening = new DataTable();
            dtPartyOpening = objRateType.GetPartyOpeningData(Val.DBDate(dtpDate.Text));
            if (dtPartyOpening.Rows.Count > 0)
            {
                grdPartyOpening.DataSource = dtPartyOpening;
                dgvPartyOpening.BestFitColumns();
            }
            else
            {
                grdPartyOpening.DataSource = null;
            }
        }

        private void FrmStorePartyOpening_Load(object sender, EventArgs e)
        {
            try
            {
                dtpDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpDate.EditValue = DateTime.Now;

                dtpDate.Focus();
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }

        private void CalculateGridAmount(int rowindex)
        {
            try
            {
                dgvPartyOpening.SetRowCellValue(rowindex, "amount", (Math.Round(Val.ToDouble(dgvPartyOpening.GetRowCellValue(rowindex, "qty")) * Val.ToDouble(dgvPartyOpening.GetRowCellValue(rowindex, "rate")), 2)));
            }
            catch (Exception)
            {
            }
        }

        private void dgvPartyOpening_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            CalculateGridAmount(dgvPartyOpening.FocusedRowHandle);
        }

        private void dgvPartyOpening_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            CalculateGridAmount(e.PrevFocusedRowHandle);
        }

        private void dgvPartyOpening_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            CalculateGridAmount(dgvPartyOpening.FocusedRowHandle);
        }

        private void dgvPartyOpening_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                DataTable dtAmount = new DataTable();
                dtAmount = (DataTable)grdPartyOpening.DataSource;

                decimal rate = 0;
                decimal carat = 0;
                decimal amount = 0;
                string column = "";
                for (int j = 0; j <= dtAmount.Columns.Count - 1; j++)
                {
                    if (dtAmount.Columns[j].ToString().Contains("qty"))
                    {
                        carat = dtAmount.AsEnumerable().Sum(x => Val.ToDecimal(x[dtAmount.Columns[j]]));
                    }
                    if (dtAmount.Columns[j].ToString().Contains("amount"))
                    {
                        amount = dtAmount.AsEnumerable().Sum(x => Math.Round(Val.ToDecimal(x[dtAmount.Columns[j]]), 0));
                    }
                    if (dtAmount.Columns[j].ToString().Contains("rate"))
                    {
                        column = dtAmount.Columns[j].ToString();
                        amount = 0;
                    }
                    if (Val.ToDecimal(amount) > 0 && Val.ToDecimal(carat) > 0)
                    {
                        if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == column)
                        {
                            rate = Math.Round(amount / carat, 2);
                            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                                e.TotalValue = rate;
                            column = "";
                            carat = 0;
                            amount = 0;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
    }
}
