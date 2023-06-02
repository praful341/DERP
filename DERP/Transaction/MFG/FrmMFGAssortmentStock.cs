using BLL;
using DERP.Class;
using DevExpress.XtraGrid;
using System;
using System.Data;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Transaction.MFG
{
    public partial class FrmMFGAssortmentStock : DevExpress.XtraEditors.XtraForm
    {
        #region Declaration


        Validation Val = new Validation();

        DataTable dtPrint = new DataTable();

        public DataTable DTab = new DataTable();
        public string ColumnsToHide = "";
        public bool AllowMultiSelect = false;
        public string ColumnHeaderCaptions = "";
        public string SearchText = "";
        public string SearchField = "";
        public string ValueMember = "";
        public string SelectedValue = "";

        FormEvents objBOFormEvents = new FormEvents();
        public FrmMFGJangedIssueAssortment FrmMFGJangedIssueAssort = new FrmMFGJangedIssueAssortment();
        string FormName = "";
        int m_numSelectedCount = 0;
        int Flag = 0;
        #endregion

        #region Constructor
        public FrmMFGAssortmentStock()
        {
            InitializeComponent();
        }

        public void ShowForm(FrmMFGJangedIssueAssortment ObjForm)
        {
            FrmMFGJangedIssueAssort = new FrmMFGJangedIssueAssortment();
            FrmMFGJangedIssueAssort = ObjForm;
            FormName = "FrmMFGJangedIssueAssortment";
            Flag = 0;
            Val.frmGenSetForPopup(this);
            AttachFormEvents();

            this.Text = "Janged Issue Assortment";

            this.ShowDialog();
        }
        public void ShowForm(FrmMFGJangedIssueAssortment ObjForm, Int64 Lot_SRNO)
        {
            FrmMFGJangedIssueAssort = new FrmMFGJangedIssueAssortment();
            FrmMFGJangedIssueAssort = ObjForm;
            FormName = "FrmMFGJangedIssueAssortment";
            Flag = 1;
            Val.frmGenSetForPopup(this);
            AttachFormEvents();

            this.Text = "Janged Issue Assortment";

            this.ShowDialog();
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

        #region Form Events
        private void FrmJangedConfirm_Load(object sender, EventArgs e)
        {
            try
            {
                ChkAll.Visible = true;
                if (DTab.Columns.Contains("SEL") == false)
                {

                    if (DTab.Columns.Contains("SEL") == false)
                    {

                        DataColumn Col = new DataColumn();
                        Col.ColumnName = "SEL";
                        Col.DataType = typeof(bool);
                        Col.DefaultValue = false;
                        DTab.Columns.Add(Col);
                    }
                }
                DTab.Columns["SEL"].SetOrdinal(0);

                MainGrid.DataSource = DTab;
                MainGrid.RefreshDataSource();

                foreach (DevExpress.XtraGrid.Columns.GridColumn Col in GrdDet.Columns)
                {
                    if (Col.FieldName.ToUpper() == "SEL")
                    {
                        Col.OptionsColumn.AllowEdit = true;
                    }
                    else
                    {
                        Col.OptionsColumn.AllowEdit = false;
                    }
                }

                GridGroupSummaryItem item4 = new GridGroupSummaryItem();
                item4.FieldName = "lot_id";
                item4.SummaryType = DevExpress.Data.SummaryItemType.Count;
                item4.ShowInGroupColumnFooter = GrdDet.Columns["lot_id"];
                GrdDet.GroupSummary.Add(item4);

                //GridGroupSummaryItem item = new GridGroupSummaryItem();
                //item.FieldName = "TOTAL_LOT";
                //item.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                //item.ShowInGroupColumnFooter = GrdDet.Columns["TOTAL_LOT"];
                //GrdDet.GroupSummary.Add(item);

                GridGroupSummaryItem item1 = new GridGroupSummaryItem();
                item1.FieldName = "pcs";
                item1.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                item1.ShowInGroupColumnFooter = GrdDet.Columns["pcs"];
                GrdDet.GroupSummary.Add(item1);

                GridGroupSummaryItem item2 = new GridGroupSummaryItem();
                item2.FieldName = "carat";
                item2.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                item2.ShowInGroupColumnFooter = GrdDet.Columns["carat"];
                GrdDet.GroupSummary.Add(item2);

                GrdDet.BestFitColumns();


                GetSummary();
                txtSelLot.EditValue = "";
                txtSelPcs.EditValue = "";
                txtSelCarat.EditValue = "";
            }
            catch (Exception ex)
            {
                Global.ErrorMessage(ex.Message);
            }
        }
        private void FrmJangedConfirm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void repSelect_CheckedChanged(object sender, EventArgs e)
        {
            GetSummary();
        }
        #endregion

        #region Grid Events
        private void GrdDet_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "SEL")
            {
                if (Val.ToBoolean(GrdDet.GetRowCellValue(e.RowHandle, "SEL")) == true)
                {
                    GrdDet.SetRowCellValue(e.RowHandle, "SEL", false);

                }
                else
                {
                    GrdDet.SetRowCellValue(e.RowHandle, "SEL", true);
                }
                //CalculateTotal();
            }
        }
        private void GrdDet_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == "SEL")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                        e.TotalValue = m_numSelectedCount;
                }

            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }

        #endregion

        #region Other Function
        private void GetSummary()
        {
            try
            {
                double IntSelLot = 0; double DouSelCarat = 0;
                System.Data.DataTable DtTransfer = (System.Data.DataTable)MainGrid.DataSource;
                GrdDet.PostEditor();
                // Global.DtTransfer.AcceptChanges();

                if (DtTransfer != null)
                {
                    if (DtTransfer.Rows.Count > 0)
                    {
                        foreach (DataRow DRow in DtTransfer.Rows)
                        {
                            if (Val.ToString(DRow["SEL"]) == "True")
                            {
                                IntSelLot = IntSelLot + 1;
                                //IntSelPcs = IntSelPcs + Val.Val(DRow["pcs"]);
                                DouSelCarat = DouSelCarat + Val.Val(DRow["carat"]);
                            }
                        }
                    }
                }
                txtSelLot.Text = IntSelLot.ToString();
                //txtSelPcs.Text = IntSelPcs.ToString();
                txtSelCarat.Text = DouSelCarat.ToString();
            }
            catch
            {
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
                            GrdDet.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            GrdDet.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            GrdDet.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            GrdDet.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            GrdDet.ExportToText(Filepath);
                            break;
                        case "html":
                            GrdDet.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            GrdDet.ExportToCsv(Filepath);
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

        #region Repository Events

        private void repSelect_MouseUp(object sender, MouseEventArgs e)
        {
            GetSummary();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (FormName == "FrmMFGJangedIssueAssortment")
            {
                if (Flag == 0)
                {
                    string StrLotList = "";
                    for (int i = 0; i < GrdDet.DataRowCount; i++)
                    {
                        if (GrdDet.GetRowCellValue(i, "SEL").ToString().ToUpper() == "TRUE")
                        {
                            if (StrLotList.Length > 0)
                            {
                                StrLotList = StrLotList + "," + GrdDet.GetRowCellValue(i, "prd_id").ToString();
                            }
                            else
                            {
                                StrLotList = GrdDet.GetRowCellValue(i, "prd_id").ToString();
                            }
                        }
                    }

                    if (StrLotList.Length > 0)
                    {
                        DataTable DTab_Select = DTab.Select("prd_id in(" + StrLotList + ")").CopyToDataTable();
                        this.Close();
                        FrmMFGJangedIssueAssort.GetStockData(DTab_Select);
                    }
                    else
                    {
                        Global.Message("Data Are Not Selected");
                        return;
                    }
                }
                else if (Flag == 1)
                {
                    string StrLotList = "";
                    for (int i = 0; i < GrdDet.DataRowCount; i++)
                    {
                        if (GrdDet.GetRowCellValue(i, "SEL").ToString().ToUpper() == "TRUE")
                        {
                            if (StrLotList.Length > 0)
                            {
                                StrLotList = StrLotList + "," + GrdDet.GetRowCellValue(i, "lot_srno").ToString();
                            }
                            else
                            {
                                StrLotList = GrdDet.GetRowCellValue(i, "lot_srno").ToString();
                            }
                        }
                    }

                    if (StrLotList.Length > 0)
                    {
                        DataTable DTab_Select = DTab.Select("lot_srno in(" + StrLotList + ")").CopyToDataTable();
                        this.Close();
                        FrmMFGJangedIssueAssort.GetStockData_New(DTab_Select);
                    }
                    else
                    {
                        Global.Message("Data Are Not Selected");
                        return;
                    }
                }
            }
        }

        private void GrdDet_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Q)
            {
                try
                {
                    if (ChkAll.Checked)
                    {
                        ChkAll.Checked = false;
                    }
                    else
                    {
                        ChkAll.Checked = true;
                    }
                    for (int i = 0; i < GrdDet.RowCount; i++)
                    {
                        if (ChkAll.Checked == true)
                        {
                            bool b = true;
                            GrdDet.SetRowCellValue(i, "SEL", b);
                        }
                        else
                        {
                            bool b = false;
                            GrdDet.SetRowCellValue(i, "SEL", b);
                        }
                    }
                    GetSummary();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        #endregion

        #region Checkbox Events

        private void ChkAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < GrdDet.RowCount; i++)
                {
                    if (ChkAll.Checked == true)
                    {
                        bool b = true;
                        GrdDet.SetRowCellValue(i, "SEL", b);
                    }
                    else
                    {
                        bool b = false;
                        GrdDet.SetRowCellValue(i, "SEL", b);
                    }
                }
                GetSummary();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void GrdDet_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            //if (e.RowHandle >= 0)
            //{
            //    if (Val.ToBoolean(GrdDet.GetRowCellValue(e.RowHandle, "SEL")) == false)
            //    {
            //        e.Appearance.BackColor = Color.White;
            //    }
            //    else
            //    {
            //        e.Appearance.BackColor = Color.SkyBlue;
            //    }
            //}
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