using BLL;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Master.MFG;
using BLL.FunctionClasses.Transaction.MFG;
using DERP.Class;
using DevExpress.Data;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraPrinting;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DREP.Transaction
{
    public partial class FrmMFGAssortView : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;
        MfgCutWiseView objCutwise;
        MFGAssortFirst objAssortFirst;
        MfgRoughClarityMaster objClarity;
        MfgQualityMaster objQuality;
        MfgRoughSieve objRoughSieve;
        ClarityMaster objPurity;
        MfgRoughClarityMaster objRoughClarity;
        DataTable m_dtbParam;
        DataTable m_dtbKapan;

        #endregion

        #region Constructor
        public FrmMFGAssortView()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();
            objCutwise = new MfgCutWiseView();
            objAssortFirst = new MFGAssortFirst();
            objClarity = new MfgRoughClarityMaster();
            objQuality = new MfgQualityMaster();
            objRoughSieve = new MfgRoughSieve();
            objPurity = new ClarityMaster();
            objRoughClarity = new MfgRoughClarityMaster();
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
                m_dtbKapan = Global.GetKapanAll_Assort();

                lueKapan.Properties.DataSource = m_dtbKapan;
                lueKapan.Properties.ValueMember = "kapan_id";
                lueKapan.Properties.DisplayMember = "kapan_no";

                m_dtbParam = Global.GetRoughCutAll();

                lueCutNo.Properties.DataSource = m_dtbParam;
                lueCutNo.Properties.ValueMember = "rough_cut_id";
                lueCutNo.Properties.DisplayMember = "rough_cut_no";

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
            TabControl1();

            this.Cursor = Cursors.Default;
        }

        private void MNExportExcel_Click(object sender, EventArgs e)
        {
            ExportMultipleGrid();
        }
        private void ContextMNExport2_Click(object sender, EventArgs e)
        {
            Global.Export("xlsx", dgvNorDetail);
        }
        private void lueKapan_EditValueChanged(object sender, EventArgs e)
        {
            m_dtbParam = new DataTable();
            if (lueKapan.Text.ToString() != "")
            {
                m_dtbParam = objCutwise.GetRoughKapanWise(Val.ToInt(lueKapan.EditValue));
            }
            lueCutNo.Properties.DataSource = m_dtbParam;
            lueCutNo.Properties.ValueMember = "rough_cut_id";
            lueCutNo.Properties.DisplayMember = "rough_cut_no";
        }

        private void dgvNorDetail_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            try
            {
                DataTable dtAmount = new DataTable();
                dtAmount = (DataTable)grdDetail.DataSource;

                decimal rate = 0;
                decimal carat = 0;
                decimal amount = 0;
                decimal totrate = 0;
                decimal totcarat = 0;
                decimal totamount = 0;

                string column = "";
                for (int j = 0; j <= dtAmount.Columns.Count - 1; j++)
                {
                    if (dtAmount.Columns[j].ToString().Contains("carat"))
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
                            rate = Math.Round(amount / carat, 3);
                            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                                e.TotalValue = rate;
                            column = "";
                            carat = 0;
                            amount = 0;
                        }
                    }
                    if (dtAmount.Columns[j].ColumnName.Contains("Total"))
                    {
                        totcarat = dtAmount.AsEnumerable().Sum(x => Val.ToDecimal(x[dtAmount.Columns[j]]));
                    }
                    if (dtAmount.Columns[j].ColumnName.Contains("Amount"))
                    {
                        totamount = dtAmount.AsEnumerable().Sum(x => Math.Round(Val.ToDecimal(x[dtAmount.Columns[j]]), 0));
                    }

                    if (Val.ToDecimal(totamount) > 0 && Val.ToDecimal(totcarat) > 0)
                    {
                        if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == "Rate")
                        {
                            totrate = Math.Round(totamount / totcarat, 3);
                            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                                e.TotalValue = totrate;
                            totamount = 0;
                            totcarat = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Global.Export("xlsx", dgvKapanRecPending);
        }
        #endregion

        #region Functions
        public void ExportMultipleGrid()
        {
            //grdCutWise.ForceInitialize();
            grdDetail.ForceInitialize();
            //grdCutSplit.ForceInitialize();

            compositeLink.CreatePageForEachLink();
            string format = ("xlsx").ToLower();
            XlsxExportOptions options = new DevExpress.XtraPrinting.XlsxExportOptions();
            options.ExportMode = XlsxExportMode.SingleFilePageByPage;

            SaveFileDialog svDialog = new SaveFileDialog();
            svDialog.DefaultExt = format;
            svDialog.FileName = "Report";
            string Filepath = string.Empty;
            if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
            {
                Filepath = svDialog.FileName;

                if (format.Equals(Exports.xlsx.ToString()))
                {
                    if (Global.Confirm("Export Done\n\nYou Want To Open Excel File ?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        compositeLink.ExportToXlsx(Filepath, options);
                        System.Diagnostics.Process.Start(Filepath);
                    }
                }
            }
        }


        #region "TabControl 1"
        public void TabControl1()
        {
            try
            {
                DataTable dtList = new DataTable();
                dgvNorDetail.Columns.Clear();

                if (lueCutNo.Text != string.Empty)
                {
                    //pivot pt = new pivot(objCutwise.GetShowData(Val.ToInt(lueCutNo.EditValue), Val.ToString(lueClarity.EditValue), Val.ToString(lueSieve.EditValue)));
                    //dtList = pt.PivotDataSuperPlus(new string[] { "clarity_id", "clarity" }, new string[] { "carat", "rate", "amount" }, new AggregateFunction[] { AggregateFunction.Sum, AggregateFunction.Average, AggregateFunction.Sum }, new string[] { "sieve" });
                    //dtList = objAssortFirst.AssortFirstView(Val.ToInt(lueKapan.EditValue), Val.ToInt(lueCutNo.EditValue));
                    
                    grdDetail.DataSource = dtList;


                    dgvNorDetail.Columns["sequence_no"].Visible = false;
                    dgvNorDetail.Columns["color_id"].Visible = false;
                    dgvNorDetail.Columns["color"].OptionsColumn.ReadOnly = true;
                    dgvNorDetail.Columns["color"].OptionsColumn.AllowFocus = false;
                    dgvNorDetail.Columns["Total"].OptionsColumn.ReadOnly = true;
                    dgvNorDetail.Columns["Total"].OptionsColumn.AllowFocus = false;
                    dgvNorDetail.Columns["color"].Fixed = FixedStyle.Left;

                    dgvNorDetail.ClearGrouping();
                    dgvNorDetail.Columns["type"].GroupIndex = 0;
                    //dgvNorDetail.Columns["sequence_no"].SortOrder = ColumnSortOrder.Descending;
                    dgvNorDetail.OptionsView.ShowGroupedColumns = false;
                    dgvNorDetail.ExpandAllGroups();
                    dgvNorDetail.OptionsPrint.PrintFooter = false;
                    dgvNorDetail.Columns[3].Caption = "#";
                    decimal Tcarat = 0;

                    for (int i = 0; i <= dtList.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j <= dtList.Columns.Count - 1; j++)
                        {
                            if (j >= 2 && j != 6)
                            {
                                Tcarat += Val.ToDecimal(dtList.Rows[i][j]);
                            }
                        }
                        //break;
                        Tcarat = 0;
                    }
                    for (int i = 0; i <= dtList.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j <= dtList.Columns.Count - 1; j++)
                        {

                            if (dtList.Columns[j].ToString().Contains("Total"))
                            {
                                string total = dtList.Columns[j].ToString();
                                GridColumn column4 = dgvNorDetail.Columns[total];
                                dgvNorDetail.Columns[total].SummaryItem.DisplayFormat = "{0:n3}";
                                column4.SummaryItem.SummaryType = SummaryItemType.Sum;
                            }
                        }
                        break;
                    }
                }
                else
                {
                    General.ShowErrors("Please select cut no");
                }
                dgvNorDetail.OptionsView.ShowFooter = true;
                dgvNorDetail.BestFitColumns();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }

        }
        #endregion

        #endregion 
    }
}