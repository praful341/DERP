using BLL;
using BLL.FunctionClasses.Transaction;
using DERP.Class;
using DevExpress.XtraPrinting;
using Spire.Xls;
using System;
using System.Data;
using System.Windows.Forms;


namespace DREP.Transaction
{
    public partial class FrmProfitLoss : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        FillCombo ObjFillCombo;
        DataSet m_dtbProfitLoss;
        ProfitLossReport objProfitLossReport;

        #endregion

        #region Constructor
        public FrmProfitLoss()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();
            ObjFillCombo = new FillCombo();
            m_dtbProfitLoss = new DataSet();
            objProfitLossReport = new ProfitLossReport();
        }
        public void ShowForm()
        {
            ObjPer.FormName = this.Name.ToUpper();
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
        private void FrmMFGCutWiseView_Load(object sender, EventArgs e)
        {
            try
            {
                ObjFillCombo.user_id = GlobalDec.gEmployeeProperty.user_id;

                DataTable DTabCompany = ObjFillCombo.FillCmb(FillCombo.TABLE.Company_Master);
                DTabCompany.DefaultView.Sort = "Company_Name";
                DTabCompany = DTabCompany.DefaultView.ToTable();

                ListCompany.Properties.DataSource = DTabCompany;
                ListCompany.Properties.DisplayMember = "Company_Name";
                ListCompany.Properties.ValueMember = "company_id";

                ListCompany.SetEditValue(BLL.GlobalDec.gEmployeeProperty.company_id.ToString());


                DataTable DTabBranch = ObjFillCombo.FillCmb(FillCombo.TABLE.Branch_Master);
                DTabBranch.DefaultView.Sort = "Branch_Name";
                DTabBranch = DTabBranch.DefaultView.ToTable();

                ListBranch.Properties.DataSource = DTabBranch;
                ListBranch.Properties.DisplayMember = "Branch_Name";
                ListBranch.Properties.ValueMember = "branch_id";

                ListBranch.SetEditValue(BLL.GlobalDec.gEmployeeProperty.branch_id.ToString());


                DataTable DTabLocation = ObjFillCombo.FillCmb(FillCombo.TABLE.Location_Master);
                DTabLocation.DefaultView.Sort = "Location_Name";
                DTabLocation = DTabLocation.DefaultView.ToTable();

                ListLocation.Properties.DataSource = DTabLocation;
                ListLocation.Properties.DisplayMember = "Location_Name";
                ListLocation.Properties.ValueMember = "location_id";

                ListLocation.SetEditValue(BLL.GlobalDec.gEmployeeProperty.location_id.ToString());

                DTPFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                DTPFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                DTPFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                DTPFromDate.Properties.CharacterCasing = CharacterCasing.Upper;

                DTPToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                DTPToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                DTPToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                DTPToDate.Properties.CharacterCasing = CharacterCasing.Upper;

                DTPFromDate.EditValue = DateTime.Now;
                DTPToDate.EditValue = DateTime.Now;
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            TabControl0();
            this.Cursor = Cursors.Default;

        }
        private void ContextMNExport1_Click(object sender, EventArgs e)
        {
            ExportMultipleGrid();
        }

        #endregion

        #region "TabControl 0"
        public void TabControl0()
        {
            try
            {
                decimal numExpAmt = 0;
                decimal numTotalCreditAmt = 0;
                decimal numStockAmt = 0;
                decimal numTotalIncomeAmt = 0;
                decimal numFinalTotal = 0;

                m_dtbProfitLoss = objProfitLossReport.GetProfitLoss(Val.Trim(ListCompany.Properties.GetCheckedItems()), Val.Trim(ListBranch.Properties.GetCheckedItems()), Val.Trim(ListLocation.Properties.GetCheckedItems()), Val.DBDate(DTPFromDate.Text), Val.DBDate(DTPToDate.Text));

                foreach (DataRow Dr in m_dtbProfitLoss.Tables[0].Rows)
                {
                    if (Val.ToInt(Dr["is_exp"]) == 1)
                    {
                        numExpAmt += Val.ToDecimal(Dr["debit_Amount"]);
                    }
                    if (Val.ToString(Dr["debit_ledger"]) == "Total Expenses")
                    {
                        Dr[4] = Val.ToDecimal(numExpAmt);
                    }
                }
                foreach (DataRow Dr in m_dtbProfitLoss.Tables[0].Rows)
                {
                    if (Val.ToString(Dr["debit_ledger"]) == "Total")
                    {
                        numTotalCreditAmt = Val.ToDecimal(Dr["debit_Amount"]);
                    }
                }
                foreach (DataRow Dr in m_dtbProfitLoss.Tables[1].Rows)
                {
                    if (Val.ToString(Dr["credit_ledger"]) == "Total")
                    {
                        Dr[2] = Val.ToDecimal(numTotalCreditAmt);
                    }
                    if (Val.ToInt(Dr["is_stock"]) == 1)
                    {
                        numStockAmt += Val.ToDecimal(Dr["credit_Amount"]);
                    }
                    if (Val.ToString(Dr["credit_ledger"]) == "Gross Loss")
                    {
                        if (Val.ToDecimal(numTotalCreditAmt - numStockAmt) <= 0)
                        {
                            Dr[2] = 0;
                        }
                        else
                        {
                            Dr[2] = Val.ToDecimal(numTotalCreditAmt - numStockAmt);
                        }
                    }
                    if (Val.ToString(Dr["credit_ledger"]) == "Gross Profit")
                    {
                        if (Val.ToDecimal(numTotalCreditAmt - numStockAmt) <= 0)
                        {
                            Dr[2] = Val.ToDecimal(numStockAmt - numTotalCreditAmt);
                        }
                    }
                }
                foreach (DataRow Dr in m_dtbProfitLoss.Tables[0].Rows)
                {
                    if (Val.ToString(Dr["debit_ledger"]) == "Gross Loss")
                    {
                        if (Val.ToDecimal(numTotalCreditAmt - numStockAmt) <= 0)
                        {
                            Dr[4] = 0;
                        }
                        else
                        {
                            Dr[4] = Val.ToDecimal(numTotalCreditAmt - numStockAmt);
                        }
                    }

                    if (Val.ToString(Dr["debit_ledger"]) == "Gross Profit")
                    {
                        if (Val.ToDecimal(numTotalCreditAmt - numStockAmt) <= 0)
                        {
                            Dr[2] = Val.ToDecimal(numStockAmt - numTotalCreditAmt);
                        }
                        else
                        {
                            Dr[2] = 0;
                        }
                    }

                    numFinalTotal += Val.ToDecimal(Dr["total"]);

                    if (Val.ToString(Dr["debit_ledger"]) == "Final Total")
                    {
                        Dr[4] = numFinalTotal;
                    }
                }
                foreach (DataRow Dr in m_dtbProfitLoss.Tables[1].Rows)
                {
                    if (Val.ToInt(Dr["is_income"]) == 1)
                    {
                        numTotalIncomeAmt += Val.ToDecimal(Dr["credit_Amount"]);
                    }

                    if (Val.ToString(Dr["credit_ledger"]) == "Final Total")
                    {
                        Dr[2] = numFinalTotal;
                    }
                    if (Val.ToString(Dr["credit_ledger"]) == "Net Loss")
                    {
                        if (Val.ToDecimal(numTotalCreditAmt - numStockAmt) <= 0)
                        {
                            Dr[2] = 0;
                        }
                        else
                        {
                            Dr[2] = numFinalTotal - numTotalIncomeAmt;
                        }
                    }
                }

                decimal diffPLamt = Val.ToDecimal(numTotalCreditAmt - numStockAmt);

                if (diffPLamt <= 0)
                {
                    foreach (DataRow Dr in m_dtbProfitLoss.Tables[1].Rows)
                    {
                        if (Val.ToString(Dr["credit_ledger"]) == "Final Total")
                        {
                            Dr[2] = numTotalIncomeAmt;
                        }

                        if (Val.ToString(Dr["credit_ledger"]) == "Gross Loss" || Val.ToString(Dr["credit_ledger"]) == "Net Loss")
                        {
                            Dr[1] = "";
                        }

                    }

                    foreach (DataRow Dr in m_dtbProfitLoss.Tables[0].Rows)
                    {
                        if (Val.ToString(Dr["debit_ledger"]) == "Final Total")
                        {
                            Dr[4] = numTotalIncomeAmt;
                        }
                        if (Val.ToString(Dr["debit_ledger"]) == "Net Profit")
                        {
                            Dr[4] = numTotalIncomeAmt - (numExpAmt);
                        }

                        if (Val.ToString(Dr["debit_ledger"]) == "Gross Loss")
                        {
                            Dr[1] = "";
                        }
                    }
                }
                else
                {
                    foreach (DataRow Dr in m_dtbProfitLoss.Tables[0].Rows)
                    {
                        if (Val.ToString(Dr["debit_ledger"]) == "Gross Profit" || Val.ToString(Dr["debit_ledger"]) == "Net Profit")
                        {
                            Dr[1] = "";
                        }
                    }
                    foreach (DataRow Dr in m_dtbProfitLoss.Tables[1].Rows)
                    {
                        if (Val.ToString(Dr["credit_ledger"]) == "Gross Profit")
                        {
                            Dr[1] = "";
                        }
                    }
                }

                if (m_dtbProfitLoss.Tables[0].Rows.Count > 0)
                {
                    grdProfitLossDebit.DataSource = m_dtbProfitLoss.Tables[0];
                }
                if (m_dtbProfitLoss.Tables[1].Rows.Count > 0)
                {
                    grdProfitLossCredit.DataSource = m_dtbProfitLoss.Tables[1];
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }

        #endregion

        #region Function

        public void ExportMultipleGrid()
        {
            grdProfitLossDebit.ForceInitialize();
            grdProfitLossCredit.ForceInitialize();

            compositeLink.CreatePageForEachLink();
            string format = ("xlsx").ToLower();
            XlsxExportOptions options = new DevExpress.XtraPrinting.XlsxExportOptions();
            options.ExportMode = XlsxExportMode.SingleFilePageByPage;
            SaveFileDialog svDialog = new SaveFileDialog();
            svDialog.DefaultExt = format;
            svDialog.FileName = "Report";
            string Filepath = System.Windows.Forms.Application.StartupPath + @"\FORMAT\Report.xlsx";
            compositeLink.ExportToXlsx(Filepath, options);

            Workbook MerBook = new Workbook();
            MerBook.LoadFromFile(Filepath);
            Worksheet MerSheet = MerBook.Worksheets[0];

            Workbook SouBook1 = new Workbook();
            SouBook1.LoadFromFile(Filepath);
            int a = SouBook1.Worksheets[1].LastRow;
            int b = SouBook1.Worksheets[1].LastColumn;
            SouBook1.Worksheets[1].Range[1, 1, a, b].Copy(MerSheet.Range[1, 6, a + MerSheet.LastRow, 10]);
            MerBook.Worksheets[0].Columns[5].ColumnWidth = 35;
            MerBook.Worksheets[0].Columns[6].ColumnWidth = 25;
            Worksheet sheet2 = MerBook.Worksheets[1];
            sheet2.Remove();

            if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
            {
                Filepath = svDialog.FileName;
                if (Global.Confirm("Export Done\n\nYou Want To Open Excel File ?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    MerBook.SaveToFile(Filepath, ExcelVersion.Version2010);
                    System.Diagnostics.Process.Start(Filepath);
                }
            }
        }

        #endregion
    }
}