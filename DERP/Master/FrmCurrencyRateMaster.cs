using BLL;
using BLL.FunctionClasses.Account;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Report;
using BLL.PropertyClasses.Account;
using BLL.PropertyClasses.Master;
using BLL.PropertyClasses.Report;
using DERP.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Master
{
    public partial class FrmCurrencyRateMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.BeginTranConnection Conn;
        FormEvents objBOFormEvents;
        Validation Val;
        BLL.FormPer ObjPer;

        CurrencyRateMaster objCurrRate;
        RateTypeMaster objRateType;
        Int64 IntRes;
        DataTable DTab = new DataTable();
        DataTable DTab_Rate_Type = new DataTable();
        ReportParams_Property ReportParams_Property = new BLL.PropertyClasses.Report.ReportParams_Property();
        ReportParams ObjReportParams = new ReportParams();
        ExpenseEntryMaster objExpenseEntry = new ExpenseEntryMaster();
        IncomeEntryMaster objIncomeEntry = new IncomeEntryMaster();
        #endregion

        #region Constructor
        public FrmCurrencyRateMaster()
        {
            InitializeComponent();

            objBOFormEvents = new FormEvents();
            Val = new Validation();
            ObjPer = new BLL.FormPer();

            objCurrRate = new CurrencyRateMaster();
            objRateType = new RateTypeMaster();
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
            objBOFormEvents.ObjToDispose.Add(objCurrRate);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion

        #region Events 
        private void FrmCurrencyRateMaster_Load(object sender, EventArgs e)
        {
            try
            {
                Global.LOOKUPCurrency(lueFCurrency);
                Global.LOOKUPCurrency(lueTCurrency);
                GetData();
                btnClear_Click(btnClear, null);
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
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

                btnSave.Enabled = false;

                if (!ValidateDetails())
                {
                    btnSave.Enabled = true;
                    return;
                }

                DialogResult result = MessageBox.Show("Do you want to save Process Currency Rate data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result != DialogResult.Yes)
                {
                    btnSave.Enabled = true;
                    return;
                }

                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                backgroundWorker_CurrencyRateType.RunWorkerAsync();

                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }



            //btnSave.Enabled = false;
            //if (SaveDetails())
            //{
            //    GetData();
            //    btnClear_Click(sender, e);
            //}
            //btnSave.Enabled = true;

        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                lblMode.Tag = 0;
                lblMode.Text = "Add Mode";
                //dtpCurrRateDate.Text = "";
                lueFCurrency.EditValue = null;
                lueTCurrency.EditValue = null;
                txtRate.Text = "";
                chkActive.Checked = true;

                dtpCurrRateDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpCurrRateDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpCurrRateDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpCurrRateDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpCurrRateDate.EditValue = DateTime.Now;

                dtpCurrRateDate.Focus();
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
        private void backgroundWorker_CurrencyRateType_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            IntRes = 0;
            CurrencyRate_MasterProperty CurrRateMasterProperty = new CurrencyRate_MasterProperty();
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

                DataTable DTab_Max = DTab.Select("from_currency_id =" + Val.ToInt32(lueFCurrency.EditValue) + " AND to_currency_id =" + Val.ToInt32(lueTCurrency.EditValue) + " AND currency_date = MAX(currency_date)").CopyToDataTable();

                if (DTab_Max.Rows.Count > 0)
                {
                    decimal rate = Val.ToDecimal(txtRate.Text);
                    decimal difrence_rate = rate - Val.ToDecimal(DTab_Max.Rows[0]["rate"]);
                    DTab_Rate_Type = objRateType.GetData();

                    if (DTab_Rate_Type.Rows.Count > 0)
                    {
                        DataTable Dtab_Rate = DTab_Rate_Type.Select("from_currency_id =" + Val.ToInt32(lueFCurrency.EditValue) + " AND to_currency_id =" + Val.ToInt32(lueTCurrency.EditValue)).CopyToDataTable();
                        Int64 Ledger_Id = objIncomeEntry.ISLadgerName_GetData("EX. DIFF.");

                        if (Dtab_Rate.Rows.Count > 0 || Dtab_Rate != null)
                        {
                            if (difrence_rate > 0)
                            {
                                for (int i = 0; i < Dtab_Rate.Rows.Count; i++)
                                {
                                    ReportParams_Property.From_Date = Val.DBDate(dtpCurrRateDate.Text);
                                    ReportParams_Property.To_Date = Val.DBDate(dtpCurrRateDate.Text);
                                    ReportParams_Property.company_id = Val.ToString(GlobalDec.gEmployeeProperty.company_id);
                                    ReportParams_Property.branch_id = Val.ToString(GlobalDec.gEmployeeProperty.branch_id);
                                    ReportParams_Property.location_id = Val.ToString(Dtab_Rate.Rows[i]["location_id"]);
                                    ReportParams_Property.department_id = Val.ToString(GlobalDec.gEmployeeProperty.department_id);

                                    DataTable DTab_Stock = ObjReportParams.GetLiveStock(ReportParams_Property, "RPT_StockLedger_InOut_New");

                                    if (DTab_Stock.Rows.Count > 0)
                                    {
                                        decimal Sum = Val.ToDecimal(DTab_Stock.Compute("Sum(closing_carat)", ""));
                                        IncomeEntry_MasterProperty IncomeEntryMasterProperty = new IncomeEntry_MasterProperty();
                                        IncomeEntryMasterProperty.income_id = Val.ToInt64(0);
                                        IncomeEntryMasterProperty.income_date = Val.DBDate(dtpCurrRateDate.Text);
                                        IncomeEntryMasterProperty.ledger_id = Val.ToInt32(Ledger_Id);
                                        IncomeEntryMasterProperty.bank_id = Val.ToInt32(1);
                                        IncomeEntryMasterProperty.transaction_type = Val.ToString("Cash");
                                        IncomeEntryMasterProperty.amount = Val.ToDecimal(Sum * difrence_rate);
                                        IncomeEntryMasterProperty.remarks = Val.ToString("");
                                        IncomeEntryMasterProperty.special_remarks = Val.ToString("");
                                        IncomeEntryMasterProperty.client_remarks = Val.ToString("");
                                        IncomeEntryMasterProperty.payment_remarks = Val.ToString("");
                                        IncomeEntryMasterProperty.invoice_id = Val.ToInt(0);
                                        IncomeEntryMasterProperty.exchange_rate = Val.ToDecimal(txtRate.Text);

                                        IntRes = objIncomeEntry.IncomeEntry_Save(IncomeEntryMasterProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                                    }
                                }
                            }
                            else if (difrence_rate < 0)
                            {
                                for (int i = 0; i < Dtab_Rate.Rows.Count; i++)
                                {
                                    ReportParams_Property.From_Date = Val.DBDate(dtpCurrRateDate.Text);
                                    ReportParams_Property.To_Date = Val.DBDate(dtpCurrRateDate.Text);
                                    ReportParams_Property.company_id = Val.ToString(GlobalDec.gEmployeeProperty.company_id);
                                    ReportParams_Property.branch_id = Val.ToString(GlobalDec.gEmployeeProperty.branch_id);
                                    ReportParams_Property.location_id = Val.ToString(Dtab_Rate.Rows[i]["location_id"]);
                                    ReportParams_Property.department_id = Val.ToString(GlobalDec.gEmployeeProperty.department_id);

                                    DataTable DTab_Stock = ObjReportParams.GetLiveStock(ReportParams_Property, "RPT_StockLedger_InOut_New");

                                    if (DTab_Stock.Rows.Count > 0)
                                    {
                                        decimal Sum = Val.ToDecimal(DTab_Stock.Compute("Sum(closing_carat)", ""));
                                        ExpenseEntry_MasterProperty ExpenseEntryMasterProperty = new ExpenseEntry_MasterProperty();
                                        ExpenseEntryMasterProperty.expense_id = Val.ToInt64(0);
                                        ExpenseEntryMasterProperty.expense_date = Val.DBDate(dtpCurrRateDate.Text);
                                        ExpenseEntryMasterProperty.ledger_id = Val.ToInt64(Ledger_Id);
                                        ExpenseEntryMasterProperty.transaction_type = Val.ToString("Cash");
                                        ExpenseEntryMasterProperty.bank_id = Val.ToInt32(1);
                                        ExpenseEntryMasterProperty.amount = Val.ToDecimal(Sum * -(difrence_rate));
                                        ExpenseEntryMasterProperty.remarks = Val.ToString("");
                                        ExpenseEntryMasterProperty.special_remarks = Val.ToString("");
                                        ExpenseEntryMasterProperty.purchase_id = Val.ToInt(0);
                                        ExpenseEntryMasterProperty.exchange_rate = Val.ToDecimal(txtRate.Text);
                                        IntRes = objExpenseEntry.ExpenseEntry_Save(ExpenseEntryMasterProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                                    }
                                }
                            }
                        }
                    }

                    CurrRateMasterProperty.currency_rate_id = Val.ToInt32(lblMode.Tag);
                    CurrRateMasterProperty.currency_date = Val.DBDate(dtpCurrRateDate.Text).ToUpper();
                    CurrRateMasterProperty.from_currency_id = Val.ToInt32(lueFCurrency.EditValue);
                    CurrRateMasterProperty.to_currency_id = Val.ToInt32(lueTCurrency.EditValue);
                    CurrRateMasterProperty.rate = Val.ToDecimal(txtRate.Text);
                    CurrRateMasterProperty.active = Val.ToBoolean(chkActive.Checked);

                    IntRes = objCurrRate.Save(CurrRateMasterProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                    if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                    {
                        Conn.Inter1.Commit();
                    }
                    else
                    {
                        Conn.Inter2.Commit();
                    }
                }
                else
                {
                    Global.Message("Data Not Found");
                    return;
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
            finally
            {
                CurrRateMasterProperty = null;
            }
        }
        private void backgroundWorker_CurrencyRateType_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (IntRes > 0)
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Currency Rate Save Successfully");
                        GetData();
                        btnClear_Click(sender, e);

                    }
                    else
                    {
                        Global.Confirm("Currency Rate Update Successfully");
                        GetData();
                        btnClear_Click(sender, e);
                    }
                }
                else
                {
                    Global.Confirm("Error In Save Currency Rate");
                    dtpCurrRateDate.Focus();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
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
                            dgvCurrencyRateMaster.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvCurrencyRateMaster.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvCurrencyRateMaster.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvCurrencyRateMaster.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvCurrencyRateMaster.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvCurrencyRateMaster.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvCurrencyRateMaster.ExportToCsv(Filepath);
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
        #region GridEvents
        private void dgvCurrencyRateMaster_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvCurrencyRateMaster.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt32(Drow["currency_rate_id"]);
                        dtpCurrRateDate.Text = Val.DBDate(Val.ToString(Drow["currency_date"]));
                        txtRate.Text = Val.ToString(Drow["rate"]);
                        lueFCurrency.EditValue = Val.ToInt32(Drow["from_currency_id"]);
                        lueTCurrency.EditValue = Val.ToInt32(Drow["to_currency_id"]);
                        chkActive.Checked = Val.ToBoolean(Drow["active"]);
                        dtpCurrRateDate.Focus();
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
        private bool ValidateDetails()
        {

            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (dtpCurrRateDate.Text == string.Empty)
                {
                    lstError.Add(new ListError(13, "Currency Date"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        dtpCurrRateDate.Focus();
                    }
                }
                if (lueFCurrency.Text == "")
                {
                    lstError.Add(new ListError(13, "From Currency"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueFCurrency.Focus();
                    }
                }
                if (lueTCurrency.Text == "")
                {
                    lstError.Add(new ListError(13, "To Currency"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueTCurrency.Focus();
                    }
                }

                if (txtRate.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Rate"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtRate.Focus();
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
                DTab = objCurrRate.GetData();
                grdCurrencyRateMaster.DataSource = DTab;
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
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
