using BLL;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Master.MFG;
using BLL.FunctionClasses.Transaction.MFG;
using DERP;
using DERP.Class;
using DevExpress.Data;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DREP.Transaction
{
    public partial class FrmMFGCutWiseView : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;
        MfgCutWiseView objCutwise;
        MfgRoughClarityMaster objClarity;
        MfgQualityMaster objQuality;
        MfgRoughSieve objRoughSieve;
        ClarityMaster objPurity;
        MfgRoughClarityMaster objRoughClarity;
        DataTable m_dtbParam;
        DataTable m_dtbKapan;
        DataTable m_dtbCutWise;
        DataTable dtList;
        decimal[] arrCarat;
        decimal[] arrAmount;
        decimal TotalCarat = 0;
        decimal TotalAmount = 0;

        #endregion

        #region Constructor
        public FrmMFGCutWiseView()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();
            objCutwise = new MfgCutWiseView();
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
                m_dtbKapan = Global.GetKapanAll();

                lueKapan.Properties.DataSource = m_dtbKapan;
                lueKapan.Properties.ValueMember = "kapan_id";
                lueKapan.Properties.DisplayMember = "kapan_no";

                m_dtbParam = Global.GetRoughCutAll();

                lueCutNo.Properties.DataSource = m_dtbParam;
                lueCutNo.Properties.ValueMember = "rough_cut_id";
                lueCutNo.Properties.DisplayMember = "rough_cut_no";

                DataTable dtbSieve = new DataTable();
                dtbSieve = objRoughSieve.GetData();
                lueSieve.Properties.DataSource = dtbSieve;
                lueSieve.Properties.ValueMember = "rough_sieve_id";
                lueSieve.Properties.DisplayMember = "sieve_name";

                DataTable dtbQuality = new DataTable();
                dtbQuality = objQuality.GetData();
                lueQuality.Properties.DataSource = dtbQuality;
                lueQuality.Properties.ValueMember = "quality_id";
                lueQuality.Properties.DisplayMember = "quality_name";


                DataTable dtbClarity = new DataTable();
                dtbClarity = objClarity.GetData();
                lueClarity.Properties.DataSource = dtbClarity;
                lueClarity.Properties.ValueMember = "rough_clarity_id";
                lueClarity.Properties.DisplayMember = "rough_clarity_name";

                DataTable dtbPurity = new DataTable();
                dtbPurity = objPurity.GetData();
                luePurity.Properties.DataSource = dtbPurity;
                luePurity.Properties.ValueMember = "purity_id";
                luePurity.Properties.DisplayMember = "purity_name";
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
            panelProgress.Visible = true;
            arrCarat = null;
            arrAmount = null;
            TabControl0();
            TabControl1();
            TabControl2();
            TabControl3();
            // TabControl4();
            TabControl5();
            TabControl6();
            this.Cursor = Cursors.Default;
            panelProgress.Visible = false;
        }

        private void MNExportExcel_Click(object sender, EventArgs e)
        {
            ExportMultipleGrid();
        }
        private void ContextMNExport2_Click(object sender, EventArgs e)
        {
            Global.Export("xlsx", dgvShadeWiseCutView);
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
        private void dgvCutWise_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column == clmCut || e.Column == clmLCarat || e.Column == clmLRate || e.Column == clmLAmount)
            {
                if (e.RowHandle == 0 || e.RowHandle == 1 || e.RowHandle == 2 || e.RowHandle == 3)
                {
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }
            }
            string StrParticulars = Val.ToString(dgvCutWise.GetRowCellValue(e.RowHandle, "rough_cut_no")).ToUpper();

            if ((StrParticulars == "SOYEBLE") || (StrParticulars == "CHIPIYO"))
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }

            if ((StrParticulars == "."))
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
        }

        private void dgvShineLs_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.Column == clmCut || e.Column == clmLCarat || e.Column == clmLRate || e.Column == clmLAmount)
            {
                if (e.RowHandle == 0 || e.RowHandle == 1 || e.RowHandle == 2 || e.RowHandle == 3)
                {
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }
            }
            string StrParticulars = Val.ToString(dgvShineLs.GetRowCellValue(e.RowHandle, "quality_name")).ToUpper();

            if ((StrParticulars == "TOTAL:"))
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }

            if ((StrParticulars == "."))
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
        }
        private void dgvCutSplit_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            GridView view = sender as GridView;

            DataTable dtAmount = (DataTable)grdCutSplit.DataSource;
            string[] SplitArray = null;

            if (dtAmount.Rows.Count > 0)
            {
                int columnCount = 0;
                string[] str = new string[dtAmount.Columns.Count];

                foreach (DataColumn dc in dtAmount.Columns)
                {
                    string abc = dc.ToString();
                    string[] Split = abc.Split(new Char[] { '_' });
                    if (dc.ToString().Contains("carat"))
                    {
                        if (Split.Length == 2)
                        {
                            str[columnCount] = Split[0].ToString();
                            columnCount++;
                        }
                        else if (Split.Length == 3)
                        {
                            str[columnCount] = Split[0].ToString() + "_" + Split[1].ToString();
                            columnCount++;
                        }
                        else if (Split.Length == 4)
                        {
                            str[columnCount] = Split[0].ToString() + "_" + Split[1].ToString() + "_" + Split[2].ToString();
                            columnCount++;
                        }
                    }
                    else if (dc.ToString().Contains("rate"))
                    {
                        if (Split.Length == 2)
                        {
                            str[columnCount] = Split[0].ToString();
                            columnCount++;
                        }
                        else if (Split.Length == 3)
                        {
                            str[columnCount] = Split[0].ToString() + "_" + Split[1].ToString();
                            columnCount++;
                        }
                        else if (Split.Length == 4)
                        {
                            str[columnCount] = Split[0].ToString() + "_" + Split[1].ToString() + "_" + Split[2].ToString();
                            columnCount++;
                        }
                    }
                    else if (dc.ToString().Contains("amount"))
                    {
                        if (Split.Length == 2)
                        {
                            str[columnCount] = Split[0].ToString();
                            columnCount++;
                        }
                        else if (Split.Length == 3)
                        {
                            str[columnCount] = Split[0].ToString() + "_" + Split[1].ToString();
                            columnCount++;
                        }
                        else if (Split.Length == 4)
                        {
                            str[columnCount] = Split[0].ToString() + "_" + Split[1].ToString() + "_" + Split[2].ToString();
                            columnCount++;
                        }
                    }
                }
                SplitArray = str.Where(c => c != null).Distinct().ToArray();
                if (arrCarat == null || arrCarat.Length == 0)
                {
                    arrCarat = new decimal[SplitArray.Length];
                    arrAmount = new decimal[SplitArray.Length];
                }

                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    TotalCarat = 0;
                    TotalAmount = 0;
                    for (int i = 0; i < SplitArray.Length; i++)
                    {
                        arrCarat[i] = 0;
                        arrAmount[i] = 0;
                    }
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    for (int i = 0; i < SplitArray.Length; i++)
                    {
                        arrCarat[i] = arrCarat[i] + Val.ToDecimal(dgvCutSplit.GetRowCellValue(e.RowHandle, SplitArray[i] + "_carat"));
                        arrAmount[i] = arrAmount[i] + Val.ToDecimal(dgvCutSplit.GetRowCellValue(e.RowHandle, SplitArray[i] + "_amount"));
                    }
                    TotalCarat = TotalCarat + Val.ToDecimal(dgvCutSplit.GetRowCellValue(e.RowHandle, "Total"));
                    TotalAmount = TotalAmount + Val.ToDecimal(dgvCutSplit.GetRowCellValue(e.RowHandle, "Amount"));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    for (int i = 0; i < SplitArray.Length; i++)
                    {
                        if (((GridSummaryItem)e.Item).FieldName.CompareTo(SplitArray[i] + "_rate") == 0)
                        {
                            if (arrCarat[i] > 0)
                            {
                                e.TotalValue = Math.Round(arrAmount[i] / arrCarat[i], 2);
                            }
                            else
                            {
                                e.TotalValue = 0;
                            }
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("Rate") == 0)
                    {
                        if (TotalCarat != 0)
                        {
                            e.TotalValue = Math.Round(TotalAmount / TotalCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
        }

        private void dgvKapanRecPending_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            try
            {
                DataTable dtAmount = new DataTable();
                dtAmount = (DataTable)grdKapanRecPending.DataSource;

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
                    if (dtAmount.Columns[j].ColumnName.Contains("Carat"))
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
            Global.Export("xlsx", dgvKapanRecPending);
        }

        #region Grid Event
        private void dgvLSAssortFinalCutView_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            GridView view = sender as GridView;

            DataTable dtAmount = (DataTable)grdLSAssortFinalCutView.DataSource;
            string[] SplitArray = null;

            if (dtAmount.Rows.Count > 0)
            {
                int columnCount = 0;
                string[] str = new string[dtAmount.Columns.Count];

                foreach (DataColumn dc in dtAmount.Columns)
                {
                    string abc = dc.ToString();
                    string[] Split = abc.Split(new Char[] { '_' });
                    if (dc.ToString().Contains("carat"))
                    {
                        if (Split.Length == 2)
                        {
                            str[columnCount] = Split[0].ToString();
                            columnCount++;
                        }
                        else if (Split.Length == 3)
                        {
                            str[columnCount] = Split[0].ToString() + "_" + Split[1].ToString();
                            columnCount++;
                        }
                    }
                    else if (dc.ToString().Contains("rate"))
                    {
                        if (Split.Length == 2)
                        {
                            str[columnCount] = Split[0].ToString();
                            columnCount++;
                        }
                        else if (Split.Length == 3)
                        {
                            str[columnCount] = Split[0].ToString() + "_" + Split[1].ToString();
                            columnCount++;
                        }
                    }
                    else if (dc.ToString().Contains("amount"))
                    {
                        if (Split.Length == 2)
                        {
                            str[columnCount] = Split[0].ToString();
                            columnCount++;
                        }
                        else if (Split.Length == 3)
                        {
                            str[columnCount] = Split[0].ToString() + "_" + Split[1].ToString();
                            columnCount++;
                        }
                    }
                }
                SplitArray = str.Where(c => c != null).Distinct().ToArray();
                if (arrCarat == null || arrCarat.Length == 0)
                {
                    arrCarat = new decimal[SplitArray.Length];
                    arrAmount = new decimal[SplitArray.Length];
                }

                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    TotalCarat = 0;
                    TotalAmount = 0;
                    for (int i = 0; i < SplitArray.Length; i++)
                    {
                        arrCarat[i] = 0;
                        arrAmount[i] = 0;
                    }
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    for (int i = 0; i < SplitArray.Length; i++)
                    {
                        arrCarat[i] = arrCarat[i] + Val.ToDecimal(dgvLSAssortFinalCutView.GetRowCellValue(e.RowHandle, SplitArray[i] + "_carat"));
                        arrAmount[i] = arrAmount[i] + Val.ToDecimal(dgvLSAssortFinalCutView.GetRowCellValue(e.RowHandle, SplitArray[i] + "_amount"));
                    }
                    TotalCarat = TotalCarat + Val.ToDecimal(dgvLSAssortFinalCutView.GetRowCellValue(e.RowHandle, "Total"));
                    TotalAmount = TotalAmount + Val.ToDecimal(dgvLSAssortFinalCutView.GetRowCellValue(e.RowHandle, "Amount"));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    for (int i = 0; i < SplitArray.Length; i++)
                    {
                        if (((GridSummaryItem)e.Item).FieldName.CompareTo(SplitArray[i] + "_rate") == 0)
                        {
                            if (arrCarat[i] > 0)
                            {
                                e.TotalValue = Math.Round(arrAmount[i] / arrCarat[i], 2);
                            }
                            else
                            {
                                e.TotalValue = 0;
                            }
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("Rate") == 0)
                    {
                        if (TotalCarat != 0)
                        {
                            e.TotalValue = Math.Round(TotalAmount / TotalCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
        }
        #endregion

        #endregion

        #region Functions
        public void ExportMultipleGrid()
        {
            grdCutWise.ForceInitialize();
            grdDetail.ForceInitialize();
            grdCutSplit.ForceInitialize();

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

        private void dgvShadeWiseCutView_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            GridView view = sender as GridView;

            DataTable dtAmount = (DataTable)grdShadeWiseCutView.DataSource;
            string[] SplitArray = null;

            if (dtAmount.Rows.Count > 0)
            {
                int columnCount = 0;
                string[] str = new string[dtAmount.Columns.Count];

                foreach (DataColumn dc in dtAmount.Columns)
                {
                    string abc = dc.ToString();
                    string[] Split = abc.Split(new Char[] { '_' });
                    if (dc.ToString().Contains("carat"))
                    {
                        if (Split.Length == 2)
                        {
                            str[columnCount] = Split[0].ToString();
                            columnCount++;
                        }
                        else if (Split.Length == 3)
                        {
                            str[columnCount] = Split[0].ToString() + "_" + Split[1].ToString();
                            columnCount++;
                        }
                    }
                    else if (dc.ToString().Contains("rate"))
                    {
                        if (Split.Length == 2)
                        {
                            str[columnCount] = Split[0].ToString();
                            columnCount++;
                        }
                        else if (Split.Length == 3)
                        {
                            str[columnCount] = Split[0].ToString() + "_" + Split[1].ToString();
                            columnCount++;
                        }
                    }
                    else if (dc.ToString().Contains("amount"))
                    {
                        if (Split.Length == 2)
                        {
                            str[columnCount] = Split[0].ToString();
                            columnCount++;
                        }
                        else if (Split.Length == 3)
                        {
                            str[columnCount] = Split[0].ToString() + "_" + Split[1].ToString();
                            columnCount++;
                        }
                    }
                }
                SplitArray = str.Where(c => c != null).Distinct().ToArray();
                if (arrCarat == null || arrCarat.Length == 0)
                {
                    arrCarat = new decimal[SplitArray.Length];
                    arrAmount = new decimal[SplitArray.Length];
                }

                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    TotalCarat = 0;
                    TotalAmount = 0;
                    for (int i = 0; i < SplitArray.Length; i++)
                    {
                        arrCarat[i] = 0;
                        arrAmount[i] = 0;
                    }
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    for (int i = 0; i < SplitArray.Length; i++)
                    {
                        arrCarat[i] = arrCarat[i] + Val.ToDecimal(dgvShadeWiseCutView.GetRowCellValue(e.RowHandle, SplitArray[i] + "_carat"));
                        arrAmount[i] = arrAmount[i] + Val.ToDecimal(dgvShadeWiseCutView.GetRowCellValue(e.RowHandle, SplitArray[i] + "_amount"));
                    }
                    TotalCarat = TotalCarat + Val.ToDecimal(dgvShadeWiseCutView.GetRowCellValue(e.RowHandle, "Total"));
                    TotalAmount = TotalAmount + Val.ToDecimal(dgvShadeWiseCutView.GetRowCellValue(e.RowHandle, "Amount"));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    for (int i = 0; i < SplitArray.Length; i++)
                    {
                        if (((GridSummaryItem)e.Item).FieldName.CompareTo(SplitArray[i] + "_rate") == 0)
                        {
                            if (arrCarat[i] > 0)
                            {
                                e.TotalValue = Math.Round(arrAmount[i] / arrCarat[i], 2);
                            }
                            else
                            {
                                e.TotalValue = 0;
                            }
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("Rate") == 0)
                    {
                        if (TotalCarat != 0)
                        {
                            e.TotalValue = Math.Round(TotalAmount / TotalCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
        }

        private void dgvShineLs_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            GridView view = sender as GridView;

            DataTable dtAmount = (DataTable)grdShineLs.DataSource;
            string[] SplitArray = null;

            if (dtAmount.Rows.Count > 0)
            {
                int columnCount = 0;
                string[] str = new string[dtAmount.Columns.Count];

                foreach (DataColumn dc in dtAmount.Columns)
                {
                    string abc = dc.ToString();
                    string[] Split = abc.Split(new Char[] { '_' });
                    if (dc.ToString().Contains("carat"))
                    {
                        if (Split.Length == 2)
                        {
                            str[columnCount] = Split[0].ToString();
                            columnCount++;
                        }
                        else if (Split.Length == 3)
                        {
                            str[columnCount] = Split[0].ToString() + "_" + Split[1].ToString();
                            columnCount++;
                        }
                        else if (Split.Length == 4)
                        {
                            str[columnCount] = Split[0].ToString() + "_" + Split[1].ToString() + "_" + Split[2].ToString();
                            columnCount++;
                        }
                    }
                    else if (dc.ToString().Contains("rate"))
                    {
                        if (Split.Length == 2)
                        {
                            str[columnCount] = Split[0].ToString();
                            columnCount++;
                        }
                        else if (Split.Length == 3)
                        {
                            str[columnCount] = Split[0].ToString() + "_" + Split[1].ToString();
                            columnCount++;
                        }
                        else if (Split.Length == 4)
                        {
                            str[columnCount] = Split[0].ToString() + "_" + Split[1].ToString() + "_" + Split[2].ToString();
                            columnCount++;
                        }
                    }
                    else if (dc.ToString().Contains("amount"))
                    {
                        if (Split.Length == 2)
                        {
                            str[columnCount] = Split[0].ToString();
                            columnCount++;
                        }
                        else if (Split.Length == 3)
                        {
                            str[columnCount] = Split[0].ToString() + "_" + Split[1].ToString();
                            columnCount++;
                        }
                        else if (Split.Length == 4)
                        {
                            str[columnCount] = Split[0].ToString() + "_" + Split[1].ToString() + "_" + Split[2].ToString();
                            columnCount++;
                        }
                    }
                }
                SplitArray = str.Where(c => c != null).Distinct().ToArray();
                if (arrCarat == null || arrCarat.Length == 0)
                {
                    arrCarat = new decimal[SplitArray.Length];
                    arrAmount = new decimal[SplitArray.Length];
                }

                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    TotalCarat = 0;
                    TotalAmount = 0;
                    for (int i = 0; i < SplitArray.Length; i++)
                    {
                        arrCarat[i] = 0;
                        arrAmount[i] = 0;
                    }
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    for (int i = 0; i < SplitArray.Length; i++)
                    {
                        arrCarat[i] = arrCarat[i] + Val.ToDecimal(dgvShineLs.GetRowCellValue(e.RowHandle, SplitArray[i] + "_carat"));
                        arrAmount[i] = arrAmount[i] + Val.ToDecimal(dgvShineLs.GetRowCellValue(e.RowHandle, SplitArray[i] + "_amount"));
                    }
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    for (int i = 0; i < SplitArray.Length; i++)
                    {
                        if (((GridSummaryItem)e.Item).FieldName.CompareTo(SplitArray[i] + "_rate") == 0)
                        {
                            if (arrCarat[i] > 0)
                            {
                                e.TotalValue = Math.Round(arrAmount[i] / arrCarat[i], 2);
                            }
                            else
                            {
                                e.TotalValue = 0;
                            }
                        }
                    }
                }
            }
        }

        #region "TabControl 0"
        public void TabControl0()
        {
            try
            {
                m_dtbCutWise = objCutwise.GetData(Val.ToInt(lueCutNo.EditValue));
                if (m_dtbCutWise.Rows.Count > 0)
                {
                    grdCutWise.DataSource = m_dtbCutWise;
                    dgvCutWise.Columns[0].Caption = " ";
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        #endregion

        #region "TabControl 1"
        public void TabControl1()
        {
            try
            {
                DataTable dtList = new DataTable();
                dgvNorDetail.Columns.Clear();

                if (lueCutNo.Text != string.Empty)
                {
                    pivot pt = new pivot(objCutwise.GetShowData(Val.ToInt(lueCutNo.EditValue), Val.ToString(lueClarity.EditValue), Val.ToString(lueSieve.EditValue)));
                    dtList = pt.PivotDataSuperPlus(new string[] { "clarity_id", "clarity" }, new string[] { "carat", "rate", "amount" }, new AggregateFunction[] { AggregateFunction.Sum, AggregateFunction.Average, AggregateFunction.Sum }, new string[] { "sieve" });

                    DataTable DTab_Sieve = objRoughSieve.GetData();
                    DataTable Merge_Data = new DataTable();
                    Merge_Data.Columns.Add("Clarity_Sieve_Carat", typeof(string));
                    Merge_Data.Columns.Add("Clarity_Sieve_Rate", typeof(string));
                    Merge_Data.Columns.Add("Clarity_Sieve_Amount", typeof(string));


                    for (int j = 0; j < DTab_Sieve.Rows.Count; j++)
                    {
                        //string Merge = DTab_Clarity.Rows[i]["rough_clarity_id"] + "_" + DTab_Sieve.Rows[j]["rough_sieve_id"] + "_" + DTab_Clarity.Rows[i]["rough_clarity_name"] + DTab_Sieve.Rows[j]["sieve_name"] + "_carat";
                        //Merge_Data.Rows.Add(DTab_Clarity.Rows[i]["rough_clarity_name"].ToString() + DTab_Sieve.Rows[j]["sieve_name"].ToString() + "_carat", DTab_Clarity.Rows[i]["rough_clarity_name"].ToString() + DTab_Sieve.Rows[j]["sieve_name"].ToString() + "_rate", DTab_Clarity.Rows[i]["rough_clarity_name"].ToString() + DTab_Sieve.Rows[j]["sieve_name"].ToString() + "_amount");
                        Merge_Data.Rows.Add(DTab_Sieve.Rows[j]["rough_sieve_id"] + "_" + DTab_Sieve.Rows[j]["sieve_name"] + "_carat", DTab_Sieve.Rows[j]["rough_sieve_id"] + "_" + DTab_Sieve.Rows[j]["sieve_name"] + "_rate", DTab_Sieve.Rows[j]["rough_sieve_id"] + "_" + DTab_Sieve.Rows[j]["sieve_name"] + "_amount");
                    }

                    int carat_seq = 2;
                    int rate_seq = 3;
                    int amount_seq = 4;

                    for (int i = 0; i < Merge_Data.Rows.Count; i++)
                    {
                        if (dtList.Columns.Contains(Merge_Data.Rows[i][0].ToString()))
                        {
                            dtList.Columns[Merge_Data.Rows[i][0].ToString()].SetOrdinal(carat_seq);
                            carat_seq = carat_seq + 3;
                        }
                        if (dtList.Columns.Contains(Merge_Data.Rows[i][1].ToString()))
                        {
                            dtList.Columns[Merge_Data.Rows[i][1].ToString()].SetOrdinal(rate_seq);
                            rate_seq = rate_seq + 3;
                        }
                        if (dtList.Columns.Contains(Merge_Data.Rows[i][2].ToString()))
                        {
                            dtList.Columns[Merge_Data.Rows[i][2].ToString()].SetOrdinal(amount_seq);
                            amount_seq = amount_seq + 3;
                        }
                        dtList.AcceptChanges();
                    }

                    //for (int i = 0; i < Merge_Data.Rows.Count; i++)
                    //{
                    //    for (int j = 0; j < dtList.Columns.Count; j++)
                    //    {
                    //        if (dtList.Columns.Contains(Merge_Data.Rows[i][0].ToString()))
                    //        {
                    //            dtList.Columns[Merge_Data.Rows[i][0].ToString()].SetOrdinal(carat_seq);
                    //            carat_seq = carat_seq + 3;
                    //            break;
                    //        }
                    //    }
                    //}

                    //for (int i = 0; i < Merge_Data.Rows.Count; i++)
                    //{
                    //    for (int j = 0; j < dtList.Columns.Count; j++)
                    //    {
                    //        if (dtList.Columns.Contains(Merge_Data.Rows[i][1].ToString()))
                    //        {
                    //            dtList.Columns[Merge_Data.Rows[i][1].ToString()].SetOrdinal(rate_seq);
                    //            rate_seq = rate_seq + 3;
                    //            break;
                    //        }
                    //    }
                    //}

                    //for (int i = 0; i < Merge_Data.Rows.Count; i++)
                    //{
                    //    for (int j = 0; j < dtList.Columns.Count; j++)
                    //    {
                    //        if (dtList.Columns.Contains(Merge_Data.Rows[i][2].ToString()))
                    //        {
                    //            dtList.Columns[Merge_Data.Rows[i][2].ToString()].SetOrdinal(amount_seq);
                    //            amount_seq = amount_seq + 3;
                    //            break;
                    //        }
                    //    }
                    //}

                    for (int i = dtList.Columns.Count - 1; i >= 2; i--)
                    {
                        string strNew = Val.ToString(dtList.Columns[i]);
                        string abc = strNew.ToString();
                        string[] Split = abc.Split(new Char[] { '_' });
                        if (Split.Length == 3)
                        {
                            dtList.Columns[Val.ToString(dtList.Columns[i])].ColumnName = strNew.Split('_')[1] + "_" + strNew.Split('_')[2];
                        }
                        else if (Split.Length == 4)
                        {
                            dtList.Columns[Val.ToString(dtList.Columns[i])].ColumnName = strNew.Split('_')[2] + "_" + strNew.Split('_')[3];
                        }
                        else if (Split.Length == 5)
                        {
                            dtList.Columns[Val.ToString(dtList.Columns[i])].ColumnName = strNew.Split('_')[2] + "_" + strNew.Split('_')[3] + "_" + strNew.Split('_')[4];
                        }
                    }

                    DataColumn Total = new System.Data.DataColumn("Total", typeof(System.Decimal));
                    DataColumn Rate = new System.Data.DataColumn("Rate", typeof(System.Decimal));
                    DataColumn Amount = new System.Data.DataColumn("Amount", typeof(System.Decimal));
                    Total.DefaultValue = "0";
                    Rate.DefaultValue = "0";
                    Amount.DefaultValue = "0";
                    dtList.Columns.Add(Total);
                    dtList.Columns.Add(Rate);
                    dtList.Columns.Add(Amount);
                    grdDetail.DataSource = dtList;

                    dgvNorDetail.Columns[1].Caption = " ";

                    dgvNorDetail.Columns["clarity_id"].Visible = false;
                    dgvNorDetail.Columns["clarity"].OptionsColumn.ReadOnly = true;
                    dgvNorDetail.Columns["clarity"].OptionsColumn.AllowFocus = false;
                    dgvNorDetail.Columns["Total"].OptionsColumn.ReadOnly = true;
                    dgvNorDetail.Columns["Total"].OptionsColumn.AllowFocus = false;
                    dgvNorDetail.Columns["Rate"].OptionsColumn.ReadOnly = true;
                    dgvNorDetail.Columns["Rate"].OptionsColumn.AllowFocus = false;
                    dgvNorDetail.Columns["Amount"].OptionsColumn.ReadOnly = true;
                    dgvNorDetail.Columns["Amount"].OptionsColumn.AllowFocus = false;
                    dgvNorDetail.Columns["clarity"].Fixed = FixedStyle.Left;
                    //foreach (DataRow row in dtList.Rows)
                    //{
                    //    foreach (DataColumn column in dtList.Columns)
                    //    {
                    //        ColumnName = column.ColumnName;
                    //        col = ColumnName.Split('_');
                    //        if (col.Length > 2)
                    //        {
                    //            currcol = col[1];
                    //            dgvNorDetail.Columns[ColumnName].Caption = currcol;
                    //        }
                    //    }
                    //}
                    decimal Tcarat = 0;
                    decimal Trate = 0;
                    decimal Tamount = 0;
                    //for (int i = 0; i <= dtList.Rows.Count - 1; i++)
                    //{
                    //    for (int j = 0; j <= dtList.Columns.Count - 1; j++)
                    //    {
                    //        if (dtList.Columns[j].ToString().Contains("amount"))
                    //        {
                    //            dgvNorDetail.Columns[j].OptionsColumn.AllowEdit = false;
                    //        }
                    //    }
                    //}
                    for (int i = 0; i <= dtList.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j <= dtList.Columns.Count - 1; j++)
                        {
                            if (dtList.Columns[j].ToString().Contains("carat"))
                            {
                                Tcarat += Val.ToDecimal(dtList.Rows[i][j]);
                            }

                            //if (dtList.Columns[j].ToString().Contains("rate"))
                            //{
                            //    rate = 
                            //}

                            if (dtList.Columns[j].ToString().Contains("amount"))
                            {
                                Tamount += Val.ToDecimal(dtList.Rows[i][j]);
                            }
                            if (dtList.Columns[j].ToString().Contains("Total"))
                            {
                                dtList.Rows[i][j] = Val.ToString(Math.Round(Tcarat, 3));
                            }
                            if (dtList.Columns[j].ToString().Contains("Rate"))
                            {
                                if (Tcarat != 0 && Tamount != 0)
                                {
                                    Trate = Tamount / Tcarat;
                                }
                                else
                                {
                                    Trate = 0;
                                }
                                dtList.Rows[i][j] = Val.ToString(Math.Round(Trate, 2));
                            }
                            if (dtList.Columns[j].ToString().Contains("Amount"))
                            {
                                dtList.Rows[i][j] = Val.ToString(Math.Round(Tamount, 2));
                                Tcarat = 0;
                                Trate = 0;
                                Tamount = 0;
                            }
                        }
                        //break;
                    }
                    for (int i = 0; i <= dtList.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j <= dtList.Columns.Count - 1; j++)
                        {
                            if (dtList.Columns[j].ToString().Contains("carat"))
                            {
                                string carat = dtList.Columns[j].ToString();
                                GridColumn column1 = dgvNorDetail.Columns[carat];
                                dgvNorDetail.Columns[carat].SummaryItem.DisplayFormat = "{0:n3}";
                                column1.SummaryItem.SummaryType = SummaryItemType.Sum;
                            }

                            if (dtList.Columns[j].ToString().Contains("rate"))
                            {
                                string rate = dtList.Columns[j].ToString();
                                GridColumn column2 = dgvNorDetail.Columns[rate];
                                dgvNorDetail.Columns[rate].SummaryItem.DisplayFormat = " {0:n3}";
                                column2.SummaryItem.SummaryType = SummaryItemType.Custom;
                            }

                            if (dtList.Columns[j].ToString().Contains("amount"))
                            {
                                string amount = dtList.Columns[j].ToString();
                                GridColumn column3 = dgvNorDetail.Columns[amount];
                                dgvNorDetail.Columns[amount].SummaryItem.DisplayFormat = "{0:n3}";
                                column3.SummaryItem.SummaryType = SummaryItemType.Sum;
                            }
                            if (dtList.Columns[j].ToString().Contains("Total"))
                            {
                                string total = dtList.Columns[j].ToString();
                                GridColumn column4 = dgvNorDetail.Columns[total];
                                dgvNorDetail.Columns[total].SummaryItem.DisplayFormat = "{0:n3}";
                                column4.SummaryItem.SummaryType = SummaryItemType.Sum;
                            }
                            if (dtList.Columns[j].ColumnName.Contains("Rate"))
                            {
                                string totrate = dtList.Columns[j].ToString();
                                GridColumn column5 = dgvNorDetail.Columns[totrate];
                                dgvNorDetail.Columns[totrate].SummaryItem.DisplayFormat = "{0:n3}";
                                column5.SummaryItem.SummaryType = SummaryItemType.Custom;
                            }
                            if (dtList.Columns[j].ToString().Contains("Amount"))
                            {
                                string tamount = dtList.Columns[j].ToString();
                                GridColumn column4 = dgvNorDetail.Columns[tamount];
                                dgvNorDetail.Columns[tamount].SummaryItem.DisplayFormat = "{0:n3}";
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
                //ShowSummary();
                dgvNorDetail.BestFitColumns();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }

        }
        #endregion

        #region "TabControl 2"

        public void TabControl2()
        {
            try
            {

                objCutwise = new MfgCutWiseView();
                DataTable dtList = new DataTable();
                dgvCutSplit.Columns.Clear();

                pivot pt = new pivot(objCutwise.GetCutSplitData(Val.ToInt(lueCutNo.EditValue), Val.ToString(lueQuality.EditValue), Val.ToString(lueSieve.EditValue)));
                dtList = pt.PivotDataSuperPlus(new string[] { "Group", "quality" }, new string[] { "carat", "rate", "amount" }, new AggregateFunction[] { AggregateFunction.Sum, AggregateFunction.Average, AggregateFunction.Sum }, new string[] { "sieve_name" });

                DataTable DTab_Clarity = objRoughClarity.GetData();
                DataTable DTab_Sieve = objRoughSieve.GetData();
                DataTable Merge_Data = new DataTable();
                Merge_Data.Columns.Add("Clarity_Sieve_Carat", typeof(string));
                Merge_Data.Columns.Add("Clarity_Sieve_Rate", typeof(string));
                Merge_Data.Columns.Add("Clarity_Sieve_Amount", typeof(string));

                for (int i = 0; i < DTab_Clarity.Rows.Count; i++)
                {
                    for (int j = 0; j < DTab_Sieve.Rows.Count; j++)
                    {
                        //string Merge = DTab_Clarity.Rows[i]["rough_clarity_id"] + "_" + DTab_Sieve.Rows[j]["rough_sieve_id"] + "_" + DTab_Clarity.Rows[i]["rough_clarity_name"] + DTab_Sieve.Rows[j]["sieve_name"] + "_carat";
                        //Merge_Data.Rows.Add(DTab_Clarity.Rows[i]["rough_clarity_name"].ToString() + DTab_Sieve.Rows[j]["sieve_name"].ToString() + "_carat", DTab_Clarity.Rows[i]["rough_clarity_name"].ToString() + DTab_Sieve.Rows[j]["sieve_name"].ToString() + "_rate", DTab_Clarity.Rows[i]["rough_clarity_name"].ToString() + DTab_Sieve.Rows[j]["sieve_name"].ToString() + "_amount");
                        Merge_Data.Rows.Add(DTab_Clarity.Rows[i]["rough_clarity_id"] + "_" + DTab_Sieve.Rows[j]["rough_sieve_id"] + "_" + DTab_Clarity.Rows[i]["rough_clarity_name"] + "_" + DTab_Clarity.Rows[i]["clarity_type"] + "_" + DTab_Sieve.Rows[j]["sieve_name"] + "_carat", DTab_Clarity.Rows[i]["rough_clarity_id"] + "_" + DTab_Sieve.Rows[j]["rough_sieve_id"] + "_" + DTab_Clarity.Rows[i]["rough_clarity_name"] + "_" + DTab_Clarity.Rows[i]["clarity_type"] + "_" + DTab_Sieve.Rows[j]["sieve_name"] + "_rate", DTab_Clarity.Rows[i]["rough_clarity_id"] + "_" + DTab_Sieve.Rows[j]["rough_sieve_id"] + "_" + DTab_Clarity.Rows[i]["rough_clarity_name"] + "_" + DTab_Clarity.Rows[i]["clarity_type"] + "_" + DTab_Sieve.Rows[j]["sieve_name"] + "_amount");
                    }
                }
                int carat_seq = 2;
                int rate_seq = 3;
                int amount_seq = 4;
                for (int i = 0; i < Merge_Data.Rows.Count; i++)
                {
                    if (dtList.Columns.Contains(Merge_Data.Rows[i][0].ToString()))
                    {
                        dtList.Columns[Merge_Data.Rows[i][0].ToString()].SetOrdinal(carat_seq);
                        carat_seq = carat_seq + 3;
                    }
                    if (dtList.Columns.Contains(Merge_Data.Rows[i][1].ToString()))
                    {
                        dtList.Columns[Merge_Data.Rows[i][1].ToString()].SetOrdinal(rate_seq);
                        rate_seq = rate_seq + 3;
                    }
                    if (dtList.Columns.Contains(Merge_Data.Rows[i][2].ToString()))
                    {
                        dtList.Columns[Merge_Data.Rows[i][2].ToString()].SetOrdinal(amount_seq);
                        amount_seq = amount_seq + 3;
                    }
                    dtList.AcceptChanges();
                }

                //for (int i = 0; i < Merge_Data.Rows.Count; i++)
                //{
                //    for (int j = 0; j < dtList.Columns.Count; j++)
                //    {
                //        if (dtList.Columns.Contains(Merge_Data.Rows[i][0].ToString()))
                //        {
                //            dtList.Columns[Merge_Data.Rows[i][0].ToString()].SetOrdinal(carat_seq);
                //            dtList.AcceptChanges();
                //            carat_seq = carat_seq + 3;
                //            break;
                //        }
                //    }
                //}
                //dtList.AcceptChanges();
                //for (int i = 0; i < Merge_Data.Rows.Count; i++)
                //{
                //    for (int j = 0; j < dtList.Columns.Count; j++)
                //    {
                //        if (dtList.Columns.Contains(Merge_Data.Rows[i][1].ToString()))
                //        {
                //            dtList.Columns[Merge_Data.Rows[i][1].ToString()].SetOrdinal(rate_seq);
                //            dtList.AcceptChanges();
                //            rate_seq = rate_seq + 3;
                //            break;
                //        }
                //    }
                //}
                //dtList.AcceptChanges();
                //for (int i = 0; i < Merge_Data.Rows.Count; i++)
                //{
                //    for (int j = 0; j < dtList.Columns.Count; j++)
                //    {
                //        if (dtList.Columns.Contains(Merge_Data.Rows[i][2].ToString()))
                //        {
                //            dtList.Columns[Merge_Data.Rows[i][2].ToString()].SetOrdinal(amount_seq);
                //            dtList.AcceptChanges();
                //            amount_seq = amount_seq + 3;
                //            break;
                //        }
                //    }
                //}
                //dtList.AcceptChanges();
                for (int i = dtList.Columns.Count - 1; i >= 2; i--)
                {
                    string strNew = Val.ToString(dtList.Columns[i]);
                    string abc = strNew.ToString();
                    string[] Split = abc.Split(new Char[] { '_' });
                    if (Split.Length == 5)
                    {
                        dtList.Columns[Val.ToString(dtList.Columns[i])].ColumnName = strNew.Split('_')[2] + "_" + strNew.Split('_')[3] + "_" + strNew.Split('_')[4];
                    }
                    else if (Split.Length == 6)
                    {
                        dtList.Columns[Val.ToString(dtList.Columns[i])].ColumnName = strNew.Split('_')[2] + "_" + strNew.Split('_')[3] + "_" + strNew.Split('_')[4] + "_" + strNew.Split('_')[5];
                    }
                }


                DataColumn Total = new System.Data.DataColumn("Total", typeof(System.Decimal));
                DataColumn Rate = new System.Data.DataColumn("Rate", typeof(System.Decimal));
                DataColumn Amount = new System.Data.DataColumn("Amount", typeof(System.Decimal));

                Total.DefaultValue = "0";
                Rate.DefaultValue = "0";
                Amount.DefaultValue = "0";
                dtList.Columns.Add(Total);
                dtList.Columns.Add(Rate);
                dtList.Columns.Add(Amount);

                grdCutSplit.DataSource = dtList;
                dgvCutSplit.Columns[1].Caption = " ";
                dgvCutSplit.Columns["Group"].Fixed = FixedStyle.Left;

                dgvCutSplit.ClearGrouping();
                dgvCutSplit.Columns["Group"].GroupIndex = 0;
                dgvCutSplit.OptionsView.ShowGroupedColumns = false;
                dgvCutSplit.ExpandAllGroups();
                dgvCutSplit.OptionsPrint.PrintFooter = false;

                decimal Tcarat = 0;
                decimal Trate = 0;
                decimal Tamount = 0;
                //for (int i = 0; i <= dtList.Rows.Count - 1; i++)
                //{
                //    for (int j = 0; j <= dtList.Columns.Count - 1; j++)
                //    {
                //        if (dtList.Columns[j].ToString().Contains("amount"))
                //        {
                //            dgvCutSplit.Columns[j].OptionsColumn.AllowEdit = false;
                //        }
                //    }
                //}
                for (int i = 0; i <= dtList.Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= dtList.Columns.Count - 1; j++)
                    {
                        if (dtList.Columns[j].ToString().Contains("carat"))
                        {
                            Tcarat += Val.ToDecimal(dtList.Rows[i][j]);
                        }

                        //if (dtList.Columns[j].ToString().Contains("rate"))
                        //{
                        //    rate = 
                        //}

                        if (dtList.Columns[j].ToString().Contains("amount"))
                        {
                            Tamount += Val.ToDecimal(dtList.Rows[i][j]);
                        }
                        if (dtList.Columns[j].ToString().Contains("Total"))
                        {
                            dtList.Rows[i][j] = Val.ToString(Math.Round(Tcarat, 3));
                        }
                        if (dtList.Columns[j].ToString().Contains("Rate"))
                        {
                            if (Tcarat != 0 && Tamount != 0)
                            {
                                Trate = Tamount / Tcarat;
                            }
                            else
                            {
                                Trate = 0;
                            }
                            dtList.Rows[i][j] = Val.ToString(Math.Round(Trate, 2));
                        }
                        if (dtList.Columns[j].ToString().Contains("Amount"))
                        {
                            dtList.Rows[i][j] = Val.ToString(Math.Round(Tamount, 2));
                            Tcarat = 0;
                            Trate = 0;
                            Tamount = 0;
                        }
                    }
                    //break;
                }
                for (int i = 0; i <= dtList.Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= dtList.Columns.Count - 1; j++)
                    {
                        if (dtList.Columns[j].ToString().Contains("carat"))
                        {
                            dgvCutSplit.Columns[dtList.Columns[j].ToString()].Summary.Add(SummaryItemType.Sum, dtList.Columns[j].ToString(), "{0:N3}");
                            dgvCutSplit.GroupSummary.Add(SummaryItemType.Sum, dtList.Columns[j].ToString(), dgvCutSplit.Columns[dtList.Columns[j].ToString()], "{0:N3}");
                        }

                        if (dtList.Columns[j].ToString().Contains("rate"))
                        {
                            dgvCutSplit.Columns[dtList.Columns[j].ToString()].Summary.Add(SummaryItemType.Custom, dtList.Columns[j].ToString(), "{0:N3}");
                            dgvCutSplit.GroupSummary.Add(SummaryItemType.Custom, dtList.Columns[j].ToString(), dgvCutSplit.Columns[dtList.Columns[j].ToString()], "{0:N3}");
                        }

                        if (dtList.Columns[j].ToString().Contains("amount"))
                        {
                            dgvCutSplit.Columns[dtList.Columns[j].ToString()].Summary.Add(SummaryItemType.Sum, dtList.Columns[j].ToString(), "{0:N3}");
                            dgvCutSplit.GroupSummary.Add(SummaryItemType.Sum, dtList.Columns[j].ToString(), dgvCutSplit.Columns[dtList.Columns[j].ToString()], "{0:N3}");
                        }
                        if (dtList.Columns[j].ToString().Contains("Total"))
                        {
                            dgvCutSplit.Columns[dtList.Columns[j].ToString()].Summary.Add(SummaryItemType.Sum, dtList.Columns[j].ToString(), "{0:N3}");
                            dgvCutSplit.GroupSummary.Add(SummaryItemType.Sum, dtList.Columns[j].ToString(), dgvCutSplit.Columns[dtList.Columns[j].ToString()], "{0:N3}");
                        }
                        if (dtList.Columns[j].ColumnName.Contains("Rate"))
                        {
                            dgvCutSplit.Columns[dtList.Columns[j].ToString()].Summary.Add(SummaryItemType.Custom, dtList.Columns[j].ToString(), "{0:N3}");
                            dgvCutSplit.GroupSummary.Add(SummaryItemType.Custom, dtList.Columns[j].ToString(), dgvCutSplit.Columns[dtList.Columns[j].ToString()], "{0:N3}");
                        }
                        if (dtList.Columns[j].ToString().Contains("Amount"))
                        {
                            //string tamount = dtList.Columns[j].ToString();
                            //GridColumn column4 = dgvCutSplit.Columns[tamount];
                            //dgvCutSplit.Columns[tamount].SummaryItem.DisplayFormat = "{0:n3}";
                            //column4.SummaryItem.SummaryType = SummaryItemType.Sum;

                            //GridGroupSummaryItem item = new GridGroupSummaryItem();
                            //item.FieldName = dtList.Columns[j].ToString();
                            //item.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                            //item.ShowInGroupColumnFooter = dgvCutSplit.Columns[dtList.Columns[j].ToString()];
                            //dgvCutSplit.GroupSummary.Add(item);

                            dgvCutSplit.Columns[dtList.Columns[j].ToString()].Summary.Add(SummaryItemType.Sum, dtList.Columns[j].ToString(), "{0:N3}");
                            dgvCutSplit.GroupSummary.Add(SummaryItemType.Sum, dtList.Columns[j].ToString(), dgvCutSplit.Columns[dtList.Columns[j].ToString()], "{0:N3}");
                        }
                    }
                    break;
                }

                //dgvCutSplit.OptionsView.ShowFooter = false;
                dgvCutSplit.OptionsView.ShowFooter = true;
                dgvCutSplit.BestFitColumns();

            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }

        }

        #endregion

        #region "TabControl 3"
        public void TabControl3()
        {
            try
            {
                dtList = new DataTable();
                dgvShadeWiseCutView.Columns.Clear();

                //string ColumnName;
                if (lueCutNo.Text != string.Empty)
                {
                    pivot pt = new pivot(objCutwise.GetCutWiseShadeView(Val.ToInt(lueCutNo.EditValue), Val.ToString(lueClarity.EditValue), Val.ToString(luePurity.EditValue), Val.ToString(lueQuality.EditValue)));
                    dtList = pt.PivotDataSuperPlus(new string[] { "Group", "purity_name" }, new string[] { "carat", "rate", "amount" }, new AggregateFunction[] { AggregateFunction.Sum, AggregateFunction.Average, AggregateFunction.Sum }, new string[] { "rough_clarity" });


                    DataTable DTab_Clarity = objRoughClarity.GetData();
                    DataTable Merge_Data = new DataTable();
                    Merge_Data.Columns.Add("Clarity_Sieve_Carat", typeof(string));
                    Merge_Data.Columns.Add("Clarity_Sieve_Rate", typeof(string));
                    Merge_Data.Columns.Add("Clarity_Sieve_Amount", typeof(string));


                    for (int j = 0; j < DTab_Clarity.Rows.Count; j++)
                    {
                        //string Merge = DTab_Clarity.Rows[i]["rough_clarity_id"] + "_" + DTab_Sieve.Rows[j]["rough_sieve_id"] + "_" + DTab_Clarity.Rows[i]["rough_clarity_name"] + DTab_Sieve.Rows[j]["sieve_name"] + "_carat";
                        //Merge_Data.Rows.Add(DTab_Clarity.Rows[i]["rough_clarity_name"].ToString() + DTab_Sieve.Rows[j]["sieve_name"].ToString() + "_carat", DTab_Clarity.Rows[i]["rough_clarity_name"].ToString() + DTab_Sieve.Rows[j]["sieve_name"].ToString() + "_rate", DTab_Clarity.Rows[i]["rough_clarity_name"].ToString() + DTab_Sieve.Rows[j]["sieve_name"].ToString() + "_amount");
                        Merge_Data.Rows.Add(DTab_Clarity.Rows[j]["rough_clarity_id"] + "_" + DTab_Clarity.Rows[j]["rough_clarity_name"] + "_" + DTab_Clarity.Rows[j]["clarity_type"] + "_carat", DTab_Clarity.Rows[j]["rough_clarity_id"] + "_" + DTab_Clarity.Rows[j]["rough_clarity_name"] + "_" + DTab_Clarity.Rows[j]["clarity_type"] + "_rate", DTab_Clarity.Rows[j]["rough_clarity_id"] + "_" + DTab_Clarity.Rows[j]["rough_clarity_name"] + "_" + DTab_Clarity.Rows[j]["clarity_type"] + "_amount");
                    }

                    int carat_seq = 2;
                    int rate_seq = 3;
                    int amount_seq = 4;

                    for (int i = 0; i < Merge_Data.Rows.Count; i++)
                    {
                        if (dtList.Columns.Contains(Merge_Data.Rows[i][0].ToString()))
                        {
                            dtList.Columns[Merge_Data.Rows[i][0].ToString()].SetOrdinal(carat_seq);
                            carat_seq = carat_seq + 3;
                        }
                        if (dtList.Columns.Contains(Merge_Data.Rows[i][1].ToString()))
                        {
                            dtList.Columns[Merge_Data.Rows[i][1].ToString()].SetOrdinal(rate_seq);
                            rate_seq = rate_seq + 3;
                        }
                        if (dtList.Columns.Contains(Merge_Data.Rows[i][2].ToString()))
                        {
                            dtList.Columns[Merge_Data.Rows[i][2].ToString()].SetOrdinal(amount_seq);
                            amount_seq = amount_seq + 3;
                        }
                        dtList.AcceptChanges();
                    }


                    for (int i = dtList.Columns.Count - 1; i >= 2; i--)
                    {
                        string strNew = Val.ToString(dtList.Columns[i]);
                        string abc = strNew.ToString();
                        string[] Split = abc.Split(new Char[] { '_' });
                        if (Split.Length == 4)
                        {
                            dtList.Columns[Val.ToString(dtList.Columns[i])].ColumnName = strNew.Split('_')[1] + "_" + strNew.Split('_')[2] + "_" + strNew.Split('_')[3];
                        }
                        if (Split.Length == 5)
                        {
                            dtList.Columns[Val.ToString(dtList.Columns[i])].ColumnName = strNew.Split('_')[1] + " " + strNew.Split('_')[2] + "_" + strNew.Split('_')[3] + "_" + strNew.Split('_')[4];
                        }
                        else if (Split.Length == 6)
                        {
                            dtList.Columns[Val.ToString(dtList.Columns[i])].ColumnName = strNew.Split('_')[1] + "_" + strNew.Split('_')[2] + " " + strNew.Split('_')[3] + "_" + strNew.Split('_')[4] + "_" + strNew.Split('_')[5];
                        }
                    }

                    DataColumn Total = new System.Data.DataColumn("Total", typeof(System.Decimal));
                    DataColumn Rate = new System.Data.DataColumn("Rate", typeof(System.Decimal));
                    DataColumn Amount = new System.Data.DataColumn("Amount", typeof(System.Decimal));
                    Total.DefaultValue = "0";
                    Rate.DefaultValue = "0";
                    Amount.DefaultValue = "0";
                    dtList.Columns.Add(Total);
                    dtList.Columns.Add(Rate);
                    dtList.Columns.Add(Amount);
                    //string[] col;
                    //string currcol;
                    grdShadeWiseCutView.DataSource = dtList;

                    dgvShadeWiseCutView.Columns[1].Caption = " ";
                    dgvShadeWiseCutView.Columns["Group"].Fixed = FixedStyle.Left;

                    dgvShadeWiseCutView.ClearGrouping();
                    dgvShadeWiseCutView.Columns["Group"].GroupIndex = 0;
                    dgvShadeWiseCutView.OptionsView.ShowGroupedColumns = false;
                    dgvShadeWiseCutView.ExpandAllGroups();
                    dgvShadeWiseCutView.OptionsPrint.PrintFooter = false;

                    dgvShadeWiseCutView.Columns["purity_name"].OptionsColumn.ReadOnly = true;
                    dgvShadeWiseCutView.Columns["purity_name"].OptionsColumn.AllowFocus = false;
                    dgvShadeWiseCutView.Columns["Total"].OptionsColumn.ReadOnly = true;
                    dgvShadeWiseCutView.Columns["Total"].OptionsColumn.AllowFocus = false;
                    dgvShadeWiseCutView.Columns["Rate"].OptionsColumn.ReadOnly = true;
                    dgvShadeWiseCutView.Columns["Rate"].OptionsColumn.AllowFocus = false;
                    dgvShadeWiseCutView.Columns["Amount"].OptionsColumn.ReadOnly = true;
                    dgvShadeWiseCutView.Columns["Amount"].OptionsColumn.AllowFocus = false;

                    decimal Tcarat = 0;
                    decimal Trate = 0;
                    decimal Tamount = 0;
                    for (int i = 0; i <= dtList.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j <= dtList.Columns.Count - 1; j++)
                        {
                            if (dtList.Columns[j].ToString().Contains("amount"))
                            {
                                dgvShadeWiseCutView.Columns[j].OptionsColumn.AllowEdit = false;
                            }
                        }
                    }

                    for (int i = 0; i <= dtList.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j <= dtList.Columns.Count - 1; j++)
                        {
                            if (dtList.Columns[j].ToString().Contains("carat"))
                            {
                                Tcarat += Val.ToDecimal(dtList.Rows[i][j]);
                            }

                            if (dtList.Columns[j].ToString().Contains("amount"))
                            {
                                Tamount += Val.ToDecimal(dtList.Rows[i][j]);
                            }
                            if (dtList.Columns[j].ToString().Contains("Total"))
                            {
                                dtList.Rows[i][j] = Val.ToString(Math.Round(Tcarat, 3));
                            }
                            if (dtList.Columns[j].ToString().Contains("Rate"))
                            {
                                if (Tcarat != 0 && Tamount != 0)
                                {
                                    Trate = Tamount / Tcarat;
                                }
                                else
                                {
                                    Trate = 0;
                                }
                                dtList.Rows[i][j] = Val.ToString(Math.Round(Trate, 2));
                            }
                            if (dtList.Columns[j].ToString().Contains("Amount"))
                            {
                                dtList.Rows[i][j] = Val.ToString(Math.Round(Tamount, 2));
                                Tcarat = 0;
                                Trate = 0;
                                Tamount = 0;
                            }
                        }
                        //break;
                    }

                    dtList.Rows[0]["Total"] = 0;
                    dtList.Rows[0]["Rate"] = 0;
                    dtList.Rows[0]["Amount"] = 0;

                    for (int i = 0; i <= dtList.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j <= dtList.Columns.Count - 1; j++)
                        {
                            if (dtList.Columns[j].ToString().Contains("carat"))
                            {
                                dgvShadeWiseCutView.Columns[dtList.Columns[j].ToString()].Summary.Add(SummaryItemType.Sum, dtList.Columns[j].ToString(), "{0:N3}");
                                dgvShadeWiseCutView.GroupSummary.Add(SummaryItemType.Sum, dtList.Columns[j].ToString(), dgvShadeWiseCutView.Columns[dtList.Columns[j].ToString()], "{0:N3}");
                                //string carat = dtList.Columns[j].ToString();
                                //GridColumn column1 = dgvShadeWiseCutView.Columns[carat];
                                //dgvShadeWiseCutView.Columns[carat].SummaryItem.DisplayFormat = " {0:n3}";
                                //column1.SummaryItem.SummaryType = SummaryItemType.Sum;

                                //GridGroupSummaryItem item = new GridGroupSummaryItem();
                                //item.FieldName = dtList.Columns[j].ToString();
                                //item.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                                //item.ShowInGroupColumnFooter = dgvShadeWiseCutView.Columns[dtList.Columns[j].ToString()];
                                //dgvShadeWiseCutView.GroupSummary.Add(item);
                            }

                            if (dtList.Columns[j].ToString().Contains("rate"))
                            {
                                dgvShadeWiseCutView.Columns[dtList.Columns[j].ToString()].Summary.Add(SummaryItemType.Custom, dtList.Columns[j].ToString(), "{0:N3}");
                                dgvShadeWiseCutView.GroupSummary.Add(SummaryItemType.Custom, dtList.Columns[j].ToString(), dgvShadeWiseCutView.Columns[dtList.Columns[j].ToString()], "{0:N3}");
                            }

                            if (dtList.Columns[j].ToString().Contains("amount"))
                            {
                                dgvShadeWiseCutView.Columns[dtList.Columns[j].ToString()].Summary.Add(SummaryItemType.Sum, dtList.Columns[j].ToString(), "{0:N3}");
                                dgvShadeWiseCutView.GroupSummary.Add(SummaryItemType.Sum, dtList.Columns[j].ToString(), dgvShadeWiseCutView.Columns[dtList.Columns[j].ToString()], "{0:N3}");
                            }
                            if (dtList.Columns[j].ToString().Contains("Total"))
                            {
                                dgvShadeWiseCutView.Columns[dtList.Columns[j].ToString()].Summary.Add(SummaryItemType.Sum, dtList.Columns[j].ToString(), "{0:N3}");
                                dgvShadeWiseCutView.GroupSummary.Add(SummaryItemType.Sum, dtList.Columns[j].ToString(), dgvShadeWiseCutView.Columns[dtList.Columns[j].ToString()], "{0:N3}");
                            }
                            if (dtList.Columns[j].ColumnName.Contains("Rate"))
                            {
                                dgvShadeWiseCutView.Columns[dtList.Columns[j].ToString()].Summary.Add(SummaryItemType.Custom, dtList.Columns[j].ToString(), "{0:N3}");
                                dgvShadeWiseCutView.GroupSummary.Add(SummaryItemType.Custom, dtList.Columns[j].ToString(), dgvShadeWiseCutView.Columns[dtList.Columns[j].ToString()], "{0:N3}");
                            }
                            if (dtList.Columns[j].ToString().Contains("Amount"))
                            {
                                dgvShadeWiseCutView.Columns[dtList.Columns[j].ToString()].Summary.Add(SummaryItemType.Sum, dtList.Columns[j].ToString(), "{0:N3}");
                                dgvShadeWiseCutView.GroupSummary.Add(SummaryItemType.Sum, dtList.Columns[j].ToString(), dgvShadeWiseCutView.Columns[dtList.Columns[j].ToString()], "{0:N3}");
                            }
                        }
                        break;
                    }
                }
                else
                {
                    General.ShowErrors("Please select cut no");
                }
                // dgvShadeWiseCutView.OptionsView.ShowFooter = true;
                dgvShadeWiseCutView.BestFitColumns();

            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }

        }
        #endregion

        #region "TabControl 4"
        public void TabControl4()
        {
            try
            {
                DataTable dtList = new DataTable();
                //grdKapanRecPending.DataSource = null;
                //grdKapanRecPending.RefreshDataSource();
                //grdKapanRecPending.Refresh();
                //dgvKapanRecPending.OptionsView.ShowFooter = false;
                //dgvKapanRecPending.Columns.Clear();   


                pivot pt = new pivot(objCutwise.GetIssuePendingData(Val.ToString(lueKapan.EditValue), Val.ToString(lueCutNo.EditValue), null, null));
                dtList = pt.PivotDataSuperPlus(new string[] { "kapan_no", "kapan_date" }, new string[] { "carat", "rate", "amount" }, new AggregateFunction[] { AggregateFunction.Sum, AggregateFunction.Average, AggregateFunction.Sum }, new string[] { "Process_Name" });

                DataColumn Carat = new System.Data.DataColumn("Carat", typeof(System.Decimal));
                DataColumn Rate = new System.Data.DataColumn("Rate", typeof(System.Decimal));
                DataColumn Amount = new System.Data.DataColumn("Amount", typeof(System.Decimal));

                Carat.DefaultValue = "0";
                Rate.DefaultValue = "0";
                Amount.DefaultValue = "0";

                dtList.Columns.Add(Carat);
                dtList.Columns.Add(Rate);
                dtList.Columns.Add(Amount);

                grdKapanRecPending.DataSource = dtList;

                dgvKapanRecPending.Columns["Kapan_carat"].Caption = "Carat";
                dgvKapanRecPending.Columns["Kapan_rate"].Caption = "Rate";
                dgvKapanRecPending.Columns["Kapan_amount"].Caption = "Amount";
                dgvKapanRecPending.Columns["SOYEBLE_carat"].Caption = "Carat";
                dgvKapanRecPending.Columns["SOYEBLE_rate"].Caption = "Rate";
                dgvKapanRecPending.Columns["SOYEBLE_amount"].Caption = "Amount";
                dgvKapanRecPending.Columns["CHIPIYO_FINAL_carat"].Caption = "Carat";
                dgvKapanRecPending.Columns["CHIPIYO_FINAL_rate"].Caption = "Rate";
                dgvKapanRecPending.Columns["CHIPIYO_FINAL_amount"].Caption = "Amount";
                dgvKapanRecPending.Columns["ASSORT_FINAL_carat"].Caption = "Carat";
                dgvKapanRecPending.Columns["ASSORT_FINAL_rate"].Caption = "Rate";
                dgvKapanRecPending.Columns["ASSORT_FINAL_amount"].Caption = "Amount";

                dgvKapanRecPending.Columns["Carat"].Caption = "Carat";
                dgvKapanRecPending.Columns["Rate"].Caption = "Rate";
                dgvKapanRecPending.Columns["Amount"].Caption = "Amount";

                decimal Tcarat = 0;
                decimal Trate = 0;
                decimal Tamount = 0;

                for (int i = 0; i <= dtList.Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= dtList.Columns.Count - 1; j++)
                    {
                        if (dtList.Columns[j].ToString().Contains("carat"))
                        {
                            Tcarat += Val.ToDecimal(dtList.Rows[i][j]);
                        }

                        if (dtList.Columns[j].ToString().Contains("amount"))
                        {
                            Tamount += Val.ToDecimal(dtList.Rows[i][j]);
                        }
                        if (dtList.Columns[j].ToString().Contains("Carat"))
                        {
                            if (Val.ToString(dtList.Rows[i][0]) == Val.ToString(lueCutNo.Text))
                            {
                                dtList.Rows[i][j] = Val.ToString("0");
                            }
                            else
                            {
                                dtList.Rows[i][j] = Val.ToString(Math.Round(Tcarat, 3));
                            }
                        }
                        if (dtList.Columns[j].ToString().Contains("Rate"))
                        {
                            if (Tcarat != 0 && Tamount != 0)
                            {
                                Trate = Tamount / Tcarat;
                            }
                            else
                            {
                                Trate = 0;
                            }
                            if (Val.ToString(dtList.Rows[i][0]) == Val.ToString(lueCutNo.Text))
                            {
                                dtList.Rows[i][j] = Val.ToString("0");
                            }
                            else
                            {
                                dtList.Rows[i][j] = Val.ToString(Math.Round(Trate, 2));
                            }
                        }
                        if (dtList.Columns[j].ToString().Contains("Amount"))
                        {
                            if (Val.ToString(dtList.Rows[i][0]) == Val.ToString(lueCutNo.Text))
                            {
                                dtList.Rows[i][j] = Val.ToString("0");
                            }
                            else
                            {
                                dtList.Rows[i][j] = Val.ToString(Math.Round(Tamount, 2));
                            }
                            Tcarat = 0;
                            Trate = 0;
                            Tamount = 0;
                        }
                    }
                }
                foreach (GridColumn column in dgvKapanRecPending.Columns)
                {
                    GridSummaryItem item = column.SummaryItem;
                    if (item != null)
                        column.Summary.Remove(item);
                }
                for (int i = 0; i <= dtList.Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= dtList.Columns.Count - 1; j++)
                    {
                        if (dtList.Columns[j].ToString().Contains("carat"))
                        {
                            //string carat = dtList.Columns[j].ToString();
                            //GridColumn column1 = dgvKapanRecPending.Columns[carat];
                            //dgvKapanRecPending.Columns[carat].SummaryItem.DisplayFormat = "{0:n3}";
                            //column1.SummaryItem.SummaryType = SummaryItemType.Sum;

                            dgvKapanRecPending.Columns[dtList.Columns[j].ToString()].Summary.Add(SummaryItemType.Sum, dtList.Columns[j].ToString(), "{0:N3}");
                        }

                        if (dtList.Columns[j].ToString().Contains("rate"))
                        {
                            dgvKapanRecPending.Columns[dtList.Columns[j].ToString()].Summary.Add(SummaryItemType.Custom, dtList.Columns[j].ToString(), "{0:N3}");
                        }

                        if (dtList.Columns[j].ToString().Contains("amount"))
                        {
                            dgvKapanRecPending.Columns[dtList.Columns[j].ToString()].Summary.Add(SummaryItemType.Sum, dtList.Columns[j].ToString(), "{0:N3}");
                        }
                        if (dtList.Columns[j].ToString().Contains("Carat"))
                        {
                            dgvKapanRecPending.Columns[dtList.Columns[j].ToString()].Summary.Add(SummaryItemType.Sum, dtList.Columns[j].ToString(), "{0:N3}");
                        }
                        if (dtList.Columns[j].ColumnName.Contains("Rate"))
                        {
                            dgvKapanRecPending.Columns[dtList.Columns[j].ToString()].Summary.Add(SummaryItemType.Custom, dtList.Columns[j].ToString(), "{0:N3}");
                        }
                        if (dtList.Columns[j].ToString().Contains("Amount"))
                        {
                            dgvKapanRecPending.Columns[dtList.Columns[j].ToString()].Summary.Add(SummaryItemType.Sum, dtList.Columns[j].ToString(), "{0:N3}");
                        }
                    }
                    break;
                }
                this.Cursor = Cursors.Default;
                dgvKapanRecPending.OptionsView.ShowFooter = true;
                dgvKapanRecPending.BestFitColumns();

            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }

        #endregion

        #region "TabControl 5"
        public void TabControl5()
        {
            try
            {
                DataTable dtList = new DataTable();
                DataTable dtNewList = new DataTable();

                //pivot pt = new pivot(objCutwise.GetShineIssueRecieveData(Val.ToString(lueKapan.EditValue), Val.ToString(lueCutNo.EditValue), null, null));
                //dtList = pt.PivotDataSuperPlus(new string[] { "quality_name", "clarity_group" }, new string[] { "carat", "rate", "amount" }, new AggregateFunction[] { AggregateFunction.Sum, AggregateFunction.Average, AggregateFunction.Sum }, new string[] { "Type" });
                //dtNewList = dtList.Copy();

                dtNewList = objCutwise.GetShineIssueRecieveData(Val.ToString(lueKapan.EditValue), Val.ToString(lueCutNo.EditValue), null, null);
                decimal Tcarat = 0;
                decimal Tamount = 0;

                for (int i = 0; i <= dtNewList.Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= dtNewList.Columns.Count - 1; j++)
                    {
                        if (dtNewList.Columns[j].ToString().Contains("carat"))
                        {
                            Tcarat += Val.ToDecimal(dtNewList.Rows[i][j]);
                        }

                        if (dtNewList.Columns[j].ToString().Contains("amount"))
                        {
                            Tamount += Val.ToDecimal(dtNewList.Rows[i][j]);
                        }
                    }
                }
                //for (int i = 0; i <= dtNewList.Rows.Count - 1; i++)
                //{
                //    for (int j = 0; j <= dtNewList.Columns.Count - 1; j++)
                //    {
                //        if (dtNewList.Columns[j].ToString().Contains("carat"))
                //        {
                //            dgvShineLs.Columns[dtNewList.Columns[j].ToString()].Summary.Add(SummaryItemType.Sum, dtNewList.Columns[j].ToString(), "{0:N3}");
                //            dgvShineLs.GroupSummary.Add(SummaryItemType.Sum, dtNewList.Columns[j].ToString(), dgvShineLs.Columns[dtNewList.Columns[j].ToString()], "{0:N3}");
                //        }

                //        if (dtNewList.Columns[j].ToString().Contains("rate"))
                //        {
                //            dgvShineLs.Columns[dtNewList.Columns[j].ToString()].Summary.Add(SummaryItemType.Custom, dtNewList.Columns[j].ToString(), "{0:N3}");
                //            dgvShineLs.GroupSummary.Add(SummaryItemType.Custom, dtNewList.Columns[j].ToString(), dgvShineLs.Columns[dtNewList.Columns[j].ToString()], "{0:N3}");
                //        }

                //        if (dtNewList.Columns[j].ToString().Contains("amount"))
                //        {
                //            dgvShineLs.Columns[dtNewList.Columns[j].ToString()].Summary.Add(SummaryItemType.Sum, dtNewList.Columns[j].ToString(), "{0:N3}");
                //            dgvShineLs.GroupSummary.Add(SummaryItemType.Sum, dtNewList.Columns[j].ToString(), dgvShineLs.Columns[dtNewList.Columns[j].ToString()], "{0:N3}");
                //        }
                //    }
                //    break;
                //}

                //foreach (GridColumn column in dgvShineLs.Columns)
                //{
                //    GridSummaryItem item = column.SummaryItem;
                //    if (item != null)
                //        column.Summary.Remove(item);
                //}

                grdShineLs.DataSource = dtNewList;
                dgvShineLs.OptionsView.ShowFooter = true;
                dgvShineLs.BestFitColumns();

                dgvShineLs.Columns[2].Caption = "Carat";
                dgvShineLs.Columns[3].Caption = "Rate";
                dgvShineLs.Columns[4].Caption = "Amount";

                dgvShineLs.Columns[5].Caption = "Carat";
                dgvShineLs.Columns[6].Caption = "Rate";
                dgvShineLs.Columns[7].Caption = "Amount";
                //dgvShineLs.Columns["clarity_group"].Fixed = FixedStyle.Left;

                dgvShineLs.ClearGrouping();
                dgvShineLs.Columns["clarity_group"].GroupIndex = 0;
                dgvShineLs.OptionsView.ShowGroupedColumns = false;
                dgvShineLs.ExpandAllGroups();
                dgvShineLs.OptionsPrint.PrintFooter = false;

                this.Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }

        #endregion

        #region "TabControl 6"
        public void TabControl6()
        {
            try
            {
                dtList = new DataTable();
                dgvLSAssortFinalCutView.Columns.Clear();

                //string ColumnName;
                if (lueCutNo.Text != string.Empty)
                {
                    pivot pt = new pivot(objCutwise.GetCutWiseLSAssortView(Val.ToInt(lueCutNo.EditValue), Val.ToString(lueClarity.EditValue), Val.ToString(luePurity.EditValue), Val.ToString(lueQuality.EditValue)));
                    dtList = pt.PivotDataSuperPlus(new string[] { "Group", "purity_name" }, new string[] { "carat", "rate", "amount" }, new AggregateFunction[] { AggregateFunction.Sum, AggregateFunction.Average, AggregateFunction.Sum }, new string[] { "rough_clarity" });


                    DataTable DTab_Clarity = objRoughClarity.GetData();
                    DataTable Merge_Data = new DataTable();
                    Merge_Data.Columns.Add("Clarity_Sieve_Carat", typeof(string));
                    Merge_Data.Columns.Add("Clarity_Sieve_Rate", typeof(string));
                    Merge_Data.Columns.Add("Clarity_Sieve_Amount", typeof(string));


                    for (int j = 0; j < DTab_Clarity.Rows.Count; j++)
                    {
                        //string Merge = DTab_Clarity.Rows[i]["rough_clarity_id"] + "_" + DTab_Sieve.Rows[j]["rough_sieve_id"] + "_" + DTab_Clarity.Rows[i]["rough_clarity_name"] + DTab_Sieve.Rows[j]["sieve_name"] + "_carat";
                        //Merge_Data.Rows.Add(DTab_Clarity.Rows[i]["rough_clarity_name"].ToString() + DTab_Sieve.Rows[j]["sieve_name"].ToString() + "_carat", DTab_Clarity.Rows[i]["rough_clarity_name"].ToString() + DTab_Sieve.Rows[j]["sieve_name"].ToString() + "_rate", DTab_Clarity.Rows[i]["rough_clarity_name"].ToString() + DTab_Sieve.Rows[j]["sieve_name"].ToString() + "_amount");
                        Merge_Data.Rows.Add(DTab_Clarity.Rows[j]["rough_clarity_id"] + "_" + DTab_Clarity.Rows[j]["rough_clarity_name"] + "_" + DTab_Clarity.Rows[j]["clarity_type"] + "_carat", DTab_Clarity.Rows[j]["rough_clarity_id"] + "_" + DTab_Clarity.Rows[j]["rough_clarity_name"] + "_" + DTab_Clarity.Rows[j]["clarity_type"] + "_rate", DTab_Clarity.Rows[j]["rough_clarity_id"] + "_" + DTab_Clarity.Rows[j]["rough_clarity_name"] + "_" + DTab_Clarity.Rows[j]["clarity_type"] + "_amount");
                    }

                    int carat_seq = 2;
                    int rate_seq = 3;
                    int amount_seq = 4;

                    for (int i = 0; i < Merge_Data.Rows.Count; i++)
                    {
                        if (dtList.Columns.Contains(Merge_Data.Rows[i][0].ToString()))
                        {
                            dtList.Columns[Merge_Data.Rows[i][0].ToString()].SetOrdinal(carat_seq);
                            carat_seq = carat_seq + 3;
                        }
                        if (dtList.Columns.Contains(Merge_Data.Rows[i][1].ToString()))
                        {
                            dtList.Columns[Merge_Data.Rows[i][1].ToString()].SetOrdinal(rate_seq);
                            rate_seq = rate_seq + 3;
                        }
                        if (dtList.Columns.Contains(Merge_Data.Rows[i][2].ToString()))
                        {
                            dtList.Columns[Merge_Data.Rows[i][2].ToString()].SetOrdinal(amount_seq);
                            amount_seq = amount_seq + 3;
                        }
                        dtList.AcceptChanges();
                    }


                    for (int i = dtList.Columns.Count - 1; i >= 2; i--)
                    {
                        string strNew = Val.ToString(dtList.Columns[i]);
                        string abc = strNew.ToString();
                        string[] Split = abc.Split(new Char[] { '_' });
                        if (Split.Length == 4)
                        {
                            dtList.Columns[Val.ToString(dtList.Columns[i])].ColumnName = strNew.Split('_')[1] + "_" + strNew.Split('_')[2] + "_" + strNew.Split('_')[3];
                        }
                        if (Split.Length == 5)
                        {
                            dtList.Columns[Val.ToString(dtList.Columns[i])].ColumnName = strNew.Split('_')[1] + " " + strNew.Split('_')[2] + "_" + strNew.Split('_')[3] + "_" + strNew.Split('_')[4];
                        }
                        else if (Split.Length == 6)
                        {
                            dtList.Columns[Val.ToString(dtList.Columns[i])].ColumnName = strNew.Split('_')[1] + "_" + strNew.Split('_')[2] + " " + strNew.Split('_')[3] + "_" + strNew.Split('_')[4] + "_" + strNew.Split('_')[5];
                        }
                    }

                    DataColumn Total = new System.Data.DataColumn("Total", typeof(System.Decimal));
                    DataColumn Rate = new System.Data.DataColumn("Rate", typeof(System.Decimal));
                    DataColumn Amount = new System.Data.DataColumn("Amount", typeof(System.Decimal));
                    Total.DefaultValue = "0";
                    Rate.DefaultValue = "0";
                    Amount.DefaultValue = "0";
                    dtList.Columns.Add(Total);
                    dtList.Columns.Add(Rate);
                    dtList.Columns.Add(Amount);
                    //string[] col;
                    //string currcol;
                    grdLSAssortFinalCutView.DataSource = dtList;

                    dgvLSAssortFinalCutView.Columns[1].Caption = " ";
                    dgvLSAssortFinalCutView.Columns["Group"].Fixed = FixedStyle.Left;

                    dgvLSAssortFinalCutView.ClearGrouping();
                    dgvLSAssortFinalCutView.Columns["Group"].GroupIndex = 0;
                    dgvLSAssortFinalCutView.OptionsView.ShowGroupedColumns = false;
                    dgvLSAssortFinalCutView.ExpandAllGroups();
                    dgvLSAssortFinalCutView.OptionsPrint.PrintFooter = false;

                    dgvLSAssortFinalCutView.Columns["purity_name"].OptionsColumn.ReadOnly = true;
                    dgvLSAssortFinalCutView.Columns["purity_name"].OptionsColumn.AllowFocus = false;
                    dgvLSAssortFinalCutView.Columns["Total"].OptionsColumn.ReadOnly = true;
                    dgvLSAssortFinalCutView.Columns["Total"].OptionsColumn.AllowFocus = false;
                    dgvLSAssortFinalCutView.Columns["Rate"].OptionsColumn.ReadOnly = true;
                    dgvLSAssortFinalCutView.Columns["Rate"].OptionsColumn.AllowFocus = false;
                    dgvLSAssortFinalCutView.Columns["Amount"].OptionsColumn.ReadOnly = true;
                    dgvLSAssortFinalCutView.Columns["Amount"].OptionsColumn.AllowFocus = false;

                    decimal Tcarat = 0;
                    decimal Trate = 0;
                    decimal Tamount = 0;
                    for (int i = 0; i <= dtList.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j <= dtList.Columns.Count - 1; j++)
                        {
                            if (dtList.Columns[j].ToString().Contains("amount"))
                            {
                                dgvLSAssortFinalCutView.Columns[j].OptionsColumn.AllowEdit = false;
                            }
                        }
                    }

                    for (int i = 0; i <= dtList.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j <= dtList.Columns.Count - 1; j++)
                        {
                            if (dtList.Columns[j].ToString().Contains("carat"))
                            {
                                Tcarat += Val.ToDecimal(dtList.Rows[i][j]);
                            }

                            if (dtList.Columns[j].ToString().Contains("amount"))
                            {
                                Tamount += Val.ToDecimal(dtList.Rows[i][j]);
                            }
                            if (dtList.Columns[j].ToString().Contains("Total"))
                            {
                                dtList.Rows[i][j] = Val.ToString(Math.Round(Tcarat, 3));
                            }
                            if (dtList.Columns[j].ToString().Contains("Rate"))
                            {
                                if (Tcarat != 0 && Tamount != 0)
                                {
                                    Trate = Tamount / Tcarat;
                                }
                                else
                                {
                                    Trate = 0;
                                }
                                dtList.Rows[i][j] = Val.ToString(Math.Round(Trate, 2));
                            }
                            if (dtList.Columns[j].ToString().Contains("Amount"))
                            {
                                dtList.Rows[i][j] = Val.ToString(Math.Round(Tamount, 2));
                                Tcarat = 0;
                                Trate = 0;
                                Tamount = 0;
                            }
                        }
                        //break;
                    }

                    for (int i = 0; i <= dtList.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j <= dtList.Columns.Count - 1; j++)
                        {
                            if (dtList.Columns[j].ToString().Contains("carat"))
                            {
                                dgvLSAssortFinalCutView.Columns[dtList.Columns[j].ToString()].Summary.Add(SummaryItemType.Sum, dtList.Columns[j].ToString(), "{0:N3}");
                                dgvLSAssortFinalCutView.GroupSummary.Add(SummaryItemType.Sum, dtList.Columns[j].ToString(), dgvLSAssortFinalCutView.Columns[dtList.Columns[j].ToString()], "{0:N3}");
                                //string carat = dtList.Columns[j].ToString();
                                //GridColumn column1 = dgvShadeWiseCutView.Columns[carat];
                                //dgvShadeWiseCutView.Columns[carat].SummaryItem.DisplayFormat = " {0:n3}";
                                //column1.SummaryItem.SummaryType = SummaryItemType.Sum;

                                //GridGroupSummaryItem item = new GridGroupSummaryItem();
                                //item.FieldName = dtList.Columns[j].ToString();
                                //item.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                                //item.ShowInGroupColumnFooter = dgvShadeWiseCutView.Columns[dtList.Columns[j].ToString()];
                                //dgvShadeWiseCutView.GroupSummary.Add(item);
                            }

                            if (dtList.Columns[j].ToString().Contains("rate"))
                            {
                                dgvLSAssortFinalCutView.Columns[dtList.Columns[j].ToString()].Summary.Add(SummaryItemType.Custom, dtList.Columns[j].ToString(), "{0:N3}");
                                dgvLSAssortFinalCutView.GroupSummary.Add(SummaryItemType.Custom, dtList.Columns[j].ToString(), dgvLSAssortFinalCutView.Columns[dtList.Columns[j].ToString()], "{0:N3}");
                            }

                            if (dtList.Columns[j].ToString().Contains("amount"))
                            {
                                dgvLSAssortFinalCutView.Columns[dtList.Columns[j].ToString()].Summary.Add(SummaryItemType.Sum, dtList.Columns[j].ToString(), "{0:N3}");
                                dgvLSAssortFinalCutView.GroupSummary.Add(SummaryItemType.Sum, dtList.Columns[j].ToString(), dgvLSAssortFinalCutView.Columns[dtList.Columns[j].ToString()], "{0:N3}");
                            }
                            if (dtList.Columns[j].ToString().Contains("Total"))
                            {
                                dgvLSAssortFinalCutView.Columns[dtList.Columns[j].ToString()].Summary.Add(SummaryItemType.Sum, dtList.Columns[j].ToString(), "{0:N3}");
                                dgvLSAssortFinalCutView.GroupSummary.Add(SummaryItemType.Sum, dtList.Columns[j].ToString(), dgvLSAssortFinalCutView.Columns[dtList.Columns[j].ToString()], "{0:N3}");
                            }
                            if (dtList.Columns[j].ColumnName.Contains("Rate"))
                            {
                                dgvLSAssortFinalCutView.Columns[dtList.Columns[j].ToString()].Summary.Add(SummaryItemType.Custom, dtList.Columns[j].ToString(), "{0:N3}");
                                dgvLSAssortFinalCutView.GroupSummary.Add(SummaryItemType.Custom, dtList.Columns[j].ToString(), dgvLSAssortFinalCutView.Columns[dtList.Columns[j].ToString()], "{0:N3}");
                            }
                            if (dtList.Columns[j].ToString().Contains("Amount"))
                            {
                                dgvLSAssortFinalCutView.Columns[dtList.Columns[j].ToString()].Summary.Add(SummaryItemType.Sum, dtList.Columns[j].ToString(), "{0:N3}");
                                dgvLSAssortFinalCutView.GroupSummary.Add(SummaryItemType.Sum, dtList.Columns[j].ToString(), dgvLSAssortFinalCutView.Columns[dtList.Columns[j].ToString()], "{0:N3}");
                            }
                        }
                        break;
                    }
                }
                else
                {
                    General.ShowErrors("Please select cut no");
                }
                dgvLSAssortFinalCutView.OptionsView.ShowFooter = true;
                dgvLSAssortFinalCutView.BestFitColumns();

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