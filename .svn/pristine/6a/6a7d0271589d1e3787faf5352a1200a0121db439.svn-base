using BLL;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using System;
using System.Data;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Transaction.MFG
{
    public partial class FrmMFGAssortmentFirstStock : DevExpress.XtraEditors.XtraForm
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
        public FrmMFGAssortFirst FrmMFGAssortFirst = new FrmMFGAssortFirst();
        public FrmMFGAssortSecond FrmMFGAssortSecond = new FrmMFGAssortSecond();
        string FormName = "";
        int m_numSelectedCount = 0;
        DataTable DtAssortment = new DataTable();
        DataTable m_dtbSubProcess = new DataTable();
        DataTable m_dtbColor = new DataTable();
        FillCombo ObjFillCombo = new FillCombo();
        DataTable m_dtbKapan = new DataTable();
        DataTable m_dtCut = new DataTable();
        DataTable m_dtbParam = new DataTable();
        //DataTable DTab_KapanWiseData = new DataTable();

        #endregion

        #region Constructor
        public FrmMFGAssortmentFirstStock()
        {
            InitializeComponent();
        }

        public void ShowForm(FrmMFGAssortFirst ObjForm, Int32 Type)
        {
            FrmMFGAssortFirst = new FrmMFGAssortFirst();
            FrmMFGAssortFirst = ObjForm;
            FormName = "FrmMFGAssortFirst";

            Val.frmGenSetForPopup(this);
            AttachFormEvents();

            this.Text = "Semi - 1";

            if (Type == 1)
            {
                RBtnType.SelectedIndex = 0;
            }
            else
            {
                RBtnType.SelectedIndex = 1;
            }
            this.ShowDialog();
        }
        public void ShowForm(FrmMFGAssortSecond ObjForm, Int32 Type)
        {
            FrmMFGAssortSecond = new FrmMFGAssortSecond();
            FrmMFGAssortSecond = ObjForm;
            FormName = "FrmMFGAssortSecond";

            Val.frmGenSetForPopup(this);
            AttachFormEvents();

            this.Text = "Semi - 2";

            if (Type == 1)
            {
                RBtnType.SelectedIndex = 0;
            }
            else
            {
                RBtnType.SelectedIndex = 1;
            }

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

                m_dtbKapan = Global.GetKapanAll_Assort();

                lueKapan.Properties.DataSource = m_dtbKapan;
                lueKapan.Properties.ValueMember = "kapan_id";
                lueKapan.Properties.DisplayMember = "kapan_no";

                m_dtCut = Global.GetRoughCutAll();
                lueCutNo.Properties.DataSource = m_dtCut;
                lueCutNo.Properties.ValueMember = "rough_cut_id";
                lueCutNo.Properties.DisplayMember = "rough_cut_no";

                dtpFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpFromDate.EditValue = DateTime.Now;

                dtpToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpToDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpToDate.EditValue = DateTime.Now;

                Global.LOOKUPSieve(lueSieve);

                //if (RBtnType.EditValue.ToString() == "1")
                //{
                //    lueSieve.SetEditValue("13,16");
                //}
                //else
                //{
                //    lueSieve.SetEditValue("13,16,17");
                //}

                DataTable DTabQuality = ObjFillCombo.FillCmb(FillCombo.TABLE.Quality_Master);
                DTabQuality.DefaultView.Sort = "quality_name";
                DTabQuality = DTabQuality.DefaultView.ToTable();

                ListQuality.Properties.DataSource = DTabQuality;
                ListQuality.Properties.DisplayMember = "quality_name";
                ListQuality.Properties.ValueMember = "quality_id";

                // Add By Praful On 29072021

                //DTab_KapanWiseData = Global.GetRoughStockWise(Val.ToInt(0), Val.ToInt32(0));

                // End By Praful On 29072021

                Global.LOOKUPProcess(lueProcess);
                Global.LOOKUPRoughShade(lueColor);

                lueProcess.Text = "ASSORTMENT";

                Global.LOOKUPSubProcess(lueSubProcess);
                m_dtbSubProcess = (((DataTable)lueSubProcess.Properties.DataSource).Copy());

                m_dtbColor = (((DataTable)lueColor.Properties.DataSource).Copy());
                string Color = string.Empty;

                if (FormName == "FrmMFGAssortFirst")
                {
                    if (RBtnType.EditValue.ToString() == "1")
                    {
                        lueSieve.SetEditValue("13,16");

                        foreach (DataRow DR in m_dtbColor.Rows)
                        {
                            if (DR["color_name"].ToString() == "WHITE" || DR["color_name"].ToString() == "NATTS" || DR["color_name"].ToString() == "LB" || DR["color_name"].ToString() == "LC"
                                 || DR["color_name"].ToString() == "MEL" || DR["color_name"].ToString() == "CVD" || DR["color_name"].ToString() == "-70"
                                 || DR["color_name"].ToString() == "BROKEN" || DR["color_name"].ToString() == "+6.5"
                                 || DR["color_name"].ToString() == "AT"
                                 || DR["color_name"].ToString() == "FANCY"
                                 || DR["color_name"].ToString() == "NOLB"
                                 || DR["color_name"].ToString() == "ATLB"
                                 || DR["color_name"].ToString() == "NWLB"
                                 || DR["color_name"].ToString() == "G"
                                 || DR["color_name"].ToString() == "LB"
                                 || DR["color_name"].ToString() == "N.O"
                                 || DR["color_name"].ToString() == "ATLC"
                                 || DR["color_name"].ToString() == "NWLC"
                                 )
                            {
                                Color = Color + "," + DR["color_id"];
                                lueColor.SetEditValue(Color);
                            }
                        }
                    }
                    else
                    {
                        lueSieve.SetEditValue("13,16,17");

                        foreach (DataRow DR in m_dtbColor.Rows)
                        {
                            if (DR["color_name"].ToString() == "W"
                                || DR["color_name"].ToString() == "GAA"
                                || DR["color_name"].ToString() == "LCH"
                                || DR["color_name"].ToString() == "LCI"
                                || DR["color_name"].ToString() == "LBGH"
                                || DR["color_name"].ToString() == "BROKEN"
                                )
                            {
                                Color = Color + "," + DR["color_id"];
                                lueColor.SetEditValue(Color);
                            }
                        }
                    }


                    lueSubProcess.Text = "SEMI-1";
                }
                else if (FormName == "FrmMFGAssortSecond")
                {
                    if (RBtnType.EditValue.ToString() == "1")
                    {
                        lueSieve.SetEditValue("13,16");

                        foreach (DataRow DR in m_dtbColor.Rows)
                        {
                            if (DR["color_name"].ToString() == "WHITE" || DR["color_name"].ToString() == "NATTS" || DR["color_name"].ToString() == "LB" || DR["color_name"].ToString() == "LC"
                                || DR["color_name"].ToString() == "MEL" || DR["color_name"].ToString() == "CVD" || DR["color_name"].ToString() == "-70"
                                || DR["color_name"].ToString() == "BROKEN" || DR["color_name"].ToString() == "+6.5"
                                || DR["color_name"].ToString() == "AT"
                                || DR["color_name"].ToString() == "FANCY"
                                //|| DR["color_name"].ToString() == "NOLB"
                                //|| DR["color_name"].ToString() == "ATLB"
                                //|| DR["color_name"].ToString() == "NWLB"
                                || DR["color_name"].ToString() == "G"
                                || DR["color_name"].ToString() == "N.O"
                        //|| DR["color_name"].ToString() == "ATLC"
                        //|| DR["color_name"].ToString() == "NWLC"
                        //|| DR["color_name"].ToString() == "W"
                        //|| DR["color_name"].ToString() == "GAA"
                        //|| DR["color_name"].ToString() == "LCH"
                        //|| DR["color_name"].ToString() == "LCI"
                        //|| DR["color_name"].ToString() == "LBGH"
                        )
                            {
                                Color = Color + "," + DR["color_id"];
                                lueColor.SetEditValue(Color);
                            }
                        }
                    }
                    else
                    {
                        lueSieve.SetEditValue("13,16,17");

                        foreach (DataRow DR in m_dtbColor.Rows)
                        {
                            if (DR["color_name"].ToString() == "WHITE" || DR["color_name"].ToString() == "NATTS" || DR["color_name"].ToString() == "LB" || DR["color_name"].ToString() == "LC"
                                || DR["color_name"].ToString() == "MEL" || DR["color_name"].ToString() == "CVD" || DR["color_name"].ToString() == "-70"
                                || DR["color_name"].ToString() == "BROKEN" || DR["color_name"].ToString() == "+6.5"
                                || DR["color_name"].ToString() == "NOLB"
                                || DR["color_name"].ToString() == "ATLB"
                                || DR["color_name"].ToString() == "NWLB"
                                || DR["color_name"].ToString() == "G"
                                || DR["color_name"].ToString() == "ATLC"
                                || DR["color_name"].ToString() == "NWLC"
                                )
                            {
                                Color = Color + "," + DR["color_id"];
                                lueColor.SetEditValue(Color);
                            }
                        }
                    }
                    lueSubProcess.Text = "SEMI-2";
                }

                GetSummary();
                txtSelLot.EditValue = "";
                //txtSelPcs.EditValue = "";
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
        private void repSelect_CheckedChanged(object sender, EventArgs e)
        {
            GetSummary();
        }
        private void BtnExit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            MFGJangedIsuRecAssortment objMFGJangedIsuRec = new MFGJangedIsuRecAssortment();
            MFGProcessIssueProperty objMFGProcessIssueProperty = new MFGProcessIssueProperty();
            objMFGProcessIssueProperty.rough_cut_id = Val.ToInt(lueCutNo.EditValue);
            objMFGProcessIssueProperty.kapan_id = Val.ToInt(lueKapan.EditValue);

            if (FormName == "FrmMFGAssortFirst")
            {
                objMFGProcessIssueProperty.flag = Val.ToInt(1);
            }
            else if (FormName == "FrmMFGAssortSecond")
            {
                objMFGProcessIssueProperty.flag = Val.ToInt(2);
            }
            objMFGProcessIssueProperty.process_id = Val.ToInt(lueProcess.EditValue);
            objMFGProcessIssueProperty.sub_process_id = Val.ToInt(lueSubProcess.EditValue);
            objMFGProcessIssueProperty.temp_sieve_name = lueSieve.Text.ToString();
            objMFGProcessIssueProperty.temp_purity_name = ListQuality.Text.ToString();
            objMFGProcessIssueProperty.from_date = Val.DBDate(dtpFromDate.Text);
            objMFGProcessIssueProperty.to_date = Val.DBDate(dtpToDate.Text);

            if (GlobalDec.gEmployeeProperty.user_name == "RIKITA")
            {
                if (RBtnType.EditValue.ToString() == "1")
                {
                    objMFGProcessIssueProperty.location_id = Val.ToInt32(1);
                }
                else
                {
                    objMFGProcessIssueProperty.location_id = Val.ToInt32(2);
                }
            }
            else
            {
                if (RBtnType.EditValue.ToString() == "1")
                {
                    objMFGProcessIssueProperty.location_id = Val.ToInt32(1);
                }
                else
                {
                    objMFGProcessIssueProperty.location_id = Val.ToInt32(2);
                }
            }

            this.Cursor = Cursors.WaitCursor;
            DTab = objMFGJangedIsuRec.GetPendingStock(objMFGProcessIssueProperty);
            this.Cursor = Cursors.Default;
            if (DTab.Rows.Count > 0)
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

                MainGrid.DataSource = (DataTable)DTab;
                MainGrid.RefreshDataSource();
                GrdDet.BestFitColumns();
            }
            else
            {
                Global.Message("Data Not Found...");
                ChkAll.Checked = false;
                MainGrid.DataSource = null;
            }
            //DataTable dtab = objProcessRecieve.GetSearchProcess(Val.ToString(dtpFromDate.Text), Val.ToString(dtpToDate.Text), Val.ToInt(lueKapan.EditValue), Val.ToInt(lueCutNo.EditValue), Val.ToInt(lueProcess.EditValue), Val.ToInt(lueSubProcess.EditValue));
        }
        private void lueProcess_EditValueChanged(object sender, EventArgs e)
        {
            if (lueProcess.EditValue != System.DBNull.Value)
            {
                if (m_dtbSubProcess.Rows.Count > 0)
                {
                    DataTable dtbdetail = m_dtbSubProcess;

                    string strFilter = string.Empty;

                    if (lueProcess.Text != "")
                        strFilter = "process_id = " + lueProcess.EditValue;


                    dtbdetail.DefaultView.RowFilter = strFilter;
                    dtbdetail.DefaultView.ToTable();

                    DataTable dtb = dtbdetail.DefaultView.ToTable();

                    lueSubProcess.Properties.DataSource = dtb;
                    lueSubProcess.Properties.ValueMember = "sub_process_id";
                    lueSubProcess.Properties.DisplayMember = "sub_process_name";
                    lueSubProcess.EditValue = System.DBNull.Value;
                }
            }
        }
        private void lueKapan_EditValueChanged(object sender, EventArgs e)
        {
            m_dtbParam = new DataTable();
            if (lueKapan.Text.ToString() != "")
            {
                m_dtbParam = Global.GetRoughKapanWise_Data(Val.ToInt(lueKapan.EditValue));
                //if (m_dtbParam.Rows.Count == 0)
                //{
                //    m_dtbParam = DTab_KapanWiseData;
                //}
            }
            lueCutNo.Properties.DataSource = m_dtbParam;
            lueCutNo.Properties.ValueMember = "rough_cut_id";
            lueCutNo.Properties.DisplayMember = "rough_cut_no";
        }
        private void repSelect_QueryValueByCheckState(object sender, DevExpress.XtraEditors.Controls.QueryValueByCheckStateEventArgs e)
        {
            CheckEdit edit = sender as CheckEdit;
            object val = edit.EditValue;
            Int64 Lot_Srno;
            switch (e.CheckState)
            {
                case CheckState.Checked:
                    if (val is bool)
                        e.Value = true;
                    Lot_Srno = Val.ToInt64(GrdDet.GetRowCellValue(GrdDet.FocusedRowHandle, "lot_srno"));
                    for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                    {
                        if (Val.ToInt64(GrdDet.GetRowCellValue(IntI, "lot_srno")) == Lot_Srno)
                        {
                            GrdDet.SetRowCellValue(IntI, "SEL", e.Value);
                        }
                    }
                    break;
                case CheckState.Unchecked:
                    if (val is bool)
                        e.Value = false;
                    Lot_Srno = Val.ToInt64(GrdDet.GetRowCellValue(GrdDet.FocusedRowHandle, "lot_srno"));
                    for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                    {
                        if (Val.ToInt64(GrdDet.GetRowCellValue(IntI, "lot_srno")) == Lot_Srno)
                        {
                            GrdDet.SetRowCellValue(IntI, "SEL", e.Value);
                        }
                    }
                    break;
            }
            GetSummary();
        }
        private void RBtnType_EditValueChanged(object sender, EventArgs e)
        {
            if (RBtnType.EditValue.ToString() == "1")
            {
                lueSieve.SetEditValue("13,16");
            }
            else
            {
                lueSieve.SetEditValue("13,16,17");
            }
        }
        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
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
                //Global.DtTransfer.AcceptChanges();

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
            DataTable dtCheckedBarcode = (DataTable)MainGrid.DataSource;
            dtCheckedBarcode.AcceptChanges();
            if (dtCheckedBarcode.Select("SEL = 'True' ").Length > 1)
            {
                Global.Message("Please Select One Lot Atleast.");
                return;
            }

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
            if (FormName == "FrmMFGAssortFirst")
            {
                if (StrLotList.Length > 0)
                {
                    DataTable DTab_Select = DTab.Select("lot_srno in(" + StrLotList + ")").CopyToDataTable();
                    this.Close();
                    FrmMFGAssortFirst.GetStockData(DTab_Select);
                }
                else
                {
                    Global.Message("Data Are Not Selected");
                    return;
                }
            }
            if (FormName == "FrmMFGAssortSecond")
            {
                if (StrLotList.Length > 0)
                {
                    DataTable DTab_Select = DTab.Select("lot_srno in(" + StrLotList + ")").CopyToDataTable();
                    this.Close();
                    FrmMFGAssortSecond.GetStockData(DTab_Select);
                }
                else
                {
                    Global.Message("Data Are Not Selected");
                    return;
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