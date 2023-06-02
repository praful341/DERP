using BLL;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Transaction;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Transaction
{
    public partial class FrmSalesStock : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        SalesStock objSalesStk;
        AssortMaster objAssort;
        SieveMaster objSieve;

        DataTable m_dtbSieve;
        DataTable m_dtbAssorts;
        DataTable m_SalesStock;

        int IntRes;
        decimal m_numSummRate;

        #endregion

        #region Constructor
        public FrmSalesStock()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            objSalesStk = new SalesStock();
            objAssort = new AssortMaster();
            objSieve = new SieveMaster();

            m_dtbSieve = new DataTable();
            m_dtbAssorts = new DataTable();
            m_SalesStock = new DataTable();

            IntRes = 0;
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
            objBOFormEvents.ObjToDispose.Add("");
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion

        #region Events              
        private void FrmSalesStock_Load(object sender, System.EventArgs e)
        {
            try
            {
                m_dtbAssorts = objAssort.GetData(1);
                repositoryLueAssort.DataSource = m_dtbAssorts;
                repositoryLueAssort.ValueMember = "assort_id";
                repositoryLueAssort.DisplayMember = "assort_name";

                m_dtbSieve = objSieve.GetData(1);
                repositoryLueSieve.DataSource = m_dtbSieve;
                repositoryLueSieve.ValueMember = "sieve_id";
                repositoryLueSieve.DisplayMember = "sieve_name";
                GetData();
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void FrmSalesStock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Alt && e.KeyCode == Keys.S)
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
                        if (entry.Key is DevExpress.XtraEditors.LookUpEdit || entry.Key is DevExpress.XtraEditors.DateEdit)
                        {
                            lstError.Add(new ListError(13, entry.Value));
                        }
                        else if (entry.Key is DevExpress.XtraEditors.TextEdit)
                        {
                            lstError.Add(new ListError(12, entry.Value));
                        }
                    }
                    rtnCtrls.First().Key.Focus();
                    BLL.General.ShowErrors(lstError);
                    Cursor.Current = Cursors.Arrow;
                    return;
                }

                DialogResult result = MessageBox.Show("Do you want to save data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result != DialogResult.Yes)
                {
                    //btnSave.Enabled = true;
                    return;
                }

                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                panelProgress.Visible = true;
                backgroundWorker_SalesStock.RunWorkerAsync();
                this.Cursor = Cursors.Default;
                //btnSave.Enabled = true;
            }
        }
        private void backgroundWorker_SalesStock_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.Default;
                if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                {
                    Conn = new BeginTranConnection(true, false);
                }
                else
                {
                    Conn = new BeginTranConnection(false, true);
                }
                Sales_StockProperty objSalesStockProperty = new Sales_StockProperty();
                MFGCutCreate objMFGCutCreate = new MFGCutCreate();
                try
                {
                    m_SalesStock = (DataTable)grdSalesStock.DataSource;
                    if (m_SalesStock.Select("carat > 0").Length > 0 && m_SalesStock.Select("rate > 0").Length > 0 && m_SalesStock.Select("amount > 0").Length > 0 && m_SalesStock.Select("flag = 1").Length > 0)
                    {
                        m_SalesStock = m_SalesStock.Select("carat > 0 and flag = 1").CopyToDataTable();
                        IntRes = 0;
                        int Count = 0;
                        int TotalCount = m_SalesStock.Rows.Count;

                        for (int i = 0; i < m_SalesStock.Rows.Count; i++)
                        {
                            objSalesStockProperty = new Sales_StockProperty();

                            objSalesStockProperty.id = Val.ToInt(m_SalesStock.Rows[i]["id"]);
                            objSalesStockProperty.sr_no = Val.ToInt(m_SalesStock.Rows[i]["sr_no"]);
                            objSalesStockProperty.assort_id = Val.ToInt(m_SalesStock.Rows[i]["assort_id"]);
                            objSalesStockProperty.sieve_id = Val.ToInt(m_SalesStock.Rows[i]["sieve_id"]);
                            objSalesStockProperty.carat = Val.ToDecimal(m_SalesStock.Rows[i]["carat"]);
                            objSalesStockProperty.rate = Val.ToDecimal(m_SalesStock.Rows[i]["rate"]);
                            objSalesStockProperty.amount = Val.ToDecimal(m_SalesStock.Rows[i]["amount"]);
                            objSalesStockProperty.company_id = Val.ToInt(GlobalDec.gEmployeeProperty.company_id);
                            objSalesStockProperty.branch_id = Val.ToInt(GlobalDec.gEmployeeProperty.branch_id);
                            objSalesStockProperty.location_id = Val.ToInt(GlobalDec.gEmployeeProperty.location_id);
                            objSalesStockProperty.department_id = Val.ToInt(GlobalDec.gEmployeeProperty.department_id);
                            objSalesStockProperty.remarks = Val.ToString(m_SalesStock.Rows[i]["remarks"]);
                            objSalesStockProperty.special_remarks = Val.ToString(m_SalesStock.Rows[i]["special_remarks"]);
                            objSalesStockProperty.client_remarks = Val.ToString(m_SalesStock.Rows[i]["client_remarks"]);
                            objSalesStockProperty.payment_remarks = Val.ToString(m_SalesStock.Rows[i]["payment_remarks"]);

                            IntRes = objSalesStk.Save(objSalesStockProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                            SetControlPropertyValue(lblProgressCount, "Text", Count.ToString() + "" + "/" + "" + TotalCount.ToString() + " Completed....");
                        }
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
                    objSalesStockProperty = null;
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
        private void backgroundWorker_SalesStock_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                panelProgress.Visible = false;
                if (IntRes > 0)
                {
                    Global.Confirm("Stock Data Save Successfully");
                    GetData();
                }
                else
                {
                    Global.Confirm("Error In Stock Save");
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }

        #region GridEvents
        private void dgvSalesStock_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            try
            {
                int srno = 0;
                srno = dgvSalesStock.RowCount;
                GridView view = sender as GridView;
                view.SetRowCellValue(e.RowHandle, view.Columns["sr_no"], srno);
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void dgvSalesStock_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                decimal amt = 0;
                GridView view = sender as GridView;
                if (view == null) return;
                if (e.Column.Caption == "Carat" || e.Column.Caption == "Rate")
                {
                    amt = Val.ToDecimal(Val.ToDecimal(view.GetRowCellValue(e.RowHandle, view.Columns["carat"])) * Val.ToDecimal(view.GetRowCellValue(e.RowHandle, view.Columns["rate"])));
                    view.SetRowCellValue(e.RowHandle, view.Columns["amount"], amt);
                    view.SetRowCellValue(e.RowHandle, view.Columns["flag"], 1);

                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }

        }
        private void dgvSalesStock_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                if (Val.ToDecimal(Amount.SummaryItem.SummaryValue) > 0 && Val.ToDecimal(Carat.SummaryItem.SummaryValue) > 0)
                {
                    m_numSummRate = Math.Round((Val.ToDecimal(Amount.SummaryItem.SummaryValue) / Val.ToDecimal(Carat.SummaryItem.SummaryValue)), 2, MidpointRounding.AwayFromZero);

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
                System.Reflection.PropertyInfo[] props = t.GetProperties();
                foreach (System.Reflection.PropertyInfo p in props)
                {
                    if ((p.Name.ToUpper() == propName.ToUpper()))
                    {
                        p.SetValue(oControl, propValue, null);
                    }
                }
            }
        }
        private void GetData()
        {
            try
            {
                DataTable dtSalesStk = new DataTable();
                dtSalesStk = objSalesStk.GetData();
                grdSalesStock.DataSource = dtSalesStk;
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
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
                            dgvSalesStock.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvSalesStock.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvSalesStock.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvSalesStock.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvSalesStock.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvSalesStock.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvSalesStock.ExportToCsv(Filepath);
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
