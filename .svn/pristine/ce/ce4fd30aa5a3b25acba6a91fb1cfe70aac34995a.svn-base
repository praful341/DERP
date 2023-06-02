using BLL;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Master.MFG;
using BLL.PropertyClasses.Master.MFG;
using DERP.Class;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Master.MFG
{
    public partial class FrmMfgProductionCapacityMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member
        BLL.BeginTranConnection Conn;
        FormEvents objBOFormEvents;
        Validation Val;
        BLL.FormPer ObjPer;

        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        MfgProductionCapacityMaster ObjProductionCapacity;

        DataTable DTab;
        DataTable DTab_Data;
        DataTable DAssort;
        DataTable DSieve;
        DataTable DtSieve;
        DataTable DtAssort;

        int m_numForm_id;
        int IntRes;

        bool m_blnsave;

        #endregion

        #region Constructor
        public FrmMfgProductionCapacityMaster()
        {
            InitializeComponent();

            objBOFormEvents = new FormEvents();
            Val = new Validation();
            ObjPer = new BLL.FormPer();

            ObjProductionCapacity = new MfgProductionCapacityMaster();

            DTab = new DataTable();
            DTab_Data = new DataTable();
            DAssort = new DataTable();
            DSieve = new DataTable();
            DtSieve = new SieveMaster().GetData();
            DtAssort = new AssortMaster().GetData();

            m_numForm_id = 0;
            IntRes = 0;

            m_blnsave = new bool();
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
        private void FrmMfgProductionCapacity_Load(object sender, EventArgs e)
        {
            try
            {
                Global.LOOKUPCompany(lueCompany);
                Global.LOOKUPBranch(lueBranch);
                Global.LOOKUPLocation(lueLocation);
                Global.LOOKUPDepartment(lueDepartment);
                GetData();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void FrmMfgProductionCapacity_Shown(object sender, System.EventArgs e)
        {
            try
            {
                dtpCapacityDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpCapacityDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpCapacityDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpCapacityDate.Properties.CharacterCasing = CharacterCasing.Upper;

                dtpCapacityDate.EditValue = DateTime.Now;

                dtpCapacityDate_EditValueChanged(null, null);
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                //var result = DateTime.Compare(Convert.ToDateTime(dtpCapacityDate.Text), DateTime.Today);
                //if (result > 0)
                //{
                //    Global.Message("Capacity Date Not Be Greater Than Today Date");
                //    dtpCapacityDate.Focus();
                //    return;

                //}
                m_blnsave = true;
                if (!ValidateDetails())
                {
                    m_blnsave = false;
                    btnSave.Enabled = true;
                    this.Cursor = Cursors.Default;
                    return;
                }
                ObjPer.FormName = this.Name.ToUpper();
                ObjPer.SetFormPer();
                if (ObjPer.AllowUpdate == false || ObjPer.AllowInsert == false)
                {
                    Global.Message(BLL.GlobalDec.gStrPermissionInsUpdMsg);
                    return;
                }
                this.Cursor = Cursors.WaitCursor;

                DTab_Data = (DataTable)grdProductionCapacity.DataSource;
                if (m_blnsave == true)
                {
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
                                    this.Cursor = Cursors.Default;
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
                                    this.Cursor = Cursors.Default;
                                    return;
                                }
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
                    backgroundWorker_ProductCapacity.RunWorkerAsync();
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                this.Cursor = Cursors.Default;
                return;
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtFileName.Text = "";
                lueCompany.EditValue = 0;
                lueBranch.EditValue = 0;
                lueLocation.EditValue = 0;
                lueDepartment.EditValue = 0;
                dtpCapacityDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpCapacityDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpCapacityDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpCapacityDate.Properties.CharacterCasing = CharacterCasing.Upper;

                dtpCapacityDate.EditValue = DateTime.Now;
                grdProductionCapacity.DataSource = null;
                dtpCapacityDate_EditValueChanged(null, null);
                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
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
                grdProductionCapacity.DataSource = null;

                System.Data.DataTable DTabFile = new System.Data.DataTable();

                if (txtFileName.Text.Length != 0)
                {
                    using (var pck = new ExcelPackage(new FileInfo(txtFileName.Text)))
                    {
                        ExcelWorksheet ws = pck.Workbook.Worksheets[1];
                        DTabFile = WorksheetToDataTable(ws, true);
                    }
                }
                grdProductionCapacity.DataSource = DTabFile;
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                this.Cursor = Cursors.Default;
                return;
            }
        }
        private void dtpCapacityDate_EditValueChanged(object sender, EventArgs e)
        {
            //OpeningStockProperty objOpeningProperty = new OpeningStockProperty();

            //objOpeningProperty.opening_date = Val.DBDate(dtpOpeningDate.Text);

            //DataTable DTab = ObjOpening.Opening_GetData(objOpeningProperty);

            //if (DTab.Rows.Count > 0)
            //{
            //    grdOpeningStock.DataSource = DTab;
            //    dgvOpeningStock.BestFitColumns();
            //    PanelSave.Visible = false;
            //    //btnBrowse.Enabled = false;
            //}
            //else
            //{
            //    grdOpeningStock.DataSource = null;
            //    dgvOpeningStock.BestFitColumns();
            //    PanelSave.Visible = true;
            //    btnBrowse.Enabled = true;
            //}
        }
        private void lblFormatSample_Click(object sender, EventArgs e)
        {
            Global.CopyFormat(System.Windows.Forms.Application.StartupPath + @"\FORMAT\Capacity_Format.xlsx", "Capacity_Format.xlsx", "xlsx");
        }
        private void backgroundWorker_ProductCapacity_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                Conn = new BeginTranConnection(true, false);
                MfgProductionCapacity_MasterProperty objProCapacityProperty = new MfgProductionCapacity_MasterProperty();
                try
                {
                    IntRes = 0;

                    int IntCounter = 0;
                    int Count = 0;
                    int TotalCount = DTab_Data.Rows.Count;
                    objProCapacityProperty.company_id = Val.ToInt(lueCompany.EditValue);
                    objProCapacityProperty.branch_id = Val.ToInt(lueBranch.EditValue);
                    objProCapacityProperty.location_id = Val.ToInt(lueLocation.EditValue);
                    objProCapacityProperty.department_id = Val.ToInt(lueDepartment.EditValue);
                    objProCapacityProperty.capacity_date = Val.DBDate(dtpCapacityDate.Text);
                    IntRes = ObjProductionCapacity.Delete(objProCapacityProperty, DLL.GlobalDec.EnumTran.Start, Conn);
                    foreach (DataRow DRow in DTab_Data.Rows)
                    {
                        objProCapacityProperty = new MfgProductionCapacity_MasterProperty();
                        ObjProductionCapacity = new MfgProductionCapacityMaster();

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

                        objProCapacityProperty.assort_id = Val.ToInt(DAssort.Rows[0]["assort_id"]);
                        objProCapacityProperty.sieve_id = Val.ToInt(DSieve.Rows[0]["sieve_id"]);
                        objProCapacityProperty.capacity = Val.ToDecimal(DRow["capacity"]);
                        objProCapacityProperty.target = Val.ToDecimal(DRow["target"]);
                        objProCapacityProperty.company_id = Val.ToInt(lueCompany.EditValue);
                        objProCapacityProperty.branch_id = Val.ToInt(lueBranch.EditValue);
                        objProCapacityProperty.location_id = Val.ToInt(lueLocation.EditValue);
                        objProCapacityProperty.department_id = Val.ToInt(lueDepartment.EditValue);
                        objProCapacityProperty.capacity_date = Val.DBDate(dtpCapacityDate.Text);
                        IntRes = ObjProductionCapacity.Save(objProCapacityProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

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
                    objProCapacityProperty = null;
                }
            }
            catch (Exception ex)
            {
                IntRes = -1;
                Conn = null;
                Global.Message(ex.ToString());
                if (ex.InnerException != null)
                {
                    Global.Message(ex.InnerException.ToString());
                }
            }
        }
        private void backgroundWorker_ProductCapacity_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                panelProgress.Visible = false;
                if (IntRes > 0)
                {
                    Global.Confirm("Production Capacity Save Successfully");
                    btnClear_Click(null, null);
                    this.Cursor = Cursors.Default;
                    GetData();
                }
                else
                {
                    Global.Confirm("Error In Save Production Capacity");
                    this.Cursor = Cursors.Default;
                    dtpCapacityDate.Focus();
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
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();

            try
            {
                if (m_blnsave)
                {

                    var result = DateTime.Compare(Convert.ToDateTime(dtpCapacityDate.Text), DateTime.Today);
                    if (result > 0)
                    {
                        lstError.Add(new ListError(5, "Capacity Date Not Be Greater Than Today Date"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            dtpCapacityDate.Focus();
                        }
                    }

                    if (lueCompany.Text == "")
                    {
                        lstError.Add(new ListError(13, "Company"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueCompany.Focus();
                        }
                    }

                    if (lueBranch.Text == "")
                    {
                        lstError.Add(new ListError(13, "Branch"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueBranch.Focus();
                        }
                    }
                    if (lueLocation.Text == "")
                    {
                        lstError.Add(new ListError(13, "Location"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueLocation.Focus();
                        }
                    }
                    if (lueDepartment.Text == "")
                    {
                        lstError.Add(new ListError(13, "Department"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueDepartment.Focus();
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
        public void GetData()
        {
            try
            {
                DataTable DTab = ObjProductionCapacity.GetData();
                if (DTab.Rows.Count > 0)
                {
                    grdProductionCapacity.DataSource = DTab;
                    lueCompany.EditValue = Val.ToInt(DTab.Rows[0]["company_id"]);
                    lueBranch.EditValue = Val.ToInt(DTab.Rows[0]["branch_id"]);
                    lueLocation.EditValue = Val.ToInt(DTab.Rows[0]["location_id"]);
                    lueDepartment.EditValue = Val.ToInt(DTab.Rows[0]["department_id"]);
                    dtpCapacityDate.EditValue = Val.DBDate(DTab.Rows[0]["capacity_date"].ToString());
                    btnSave.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }

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
                            dgvProductionCapacity.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvProductionCapacity.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvProductionCapacity.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvProductionCapacity.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvProductionCapacity.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvProductionCapacity.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvProductionCapacity.ExportToCsv(Filepath);
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

    }
}
