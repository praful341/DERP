using BLL;
using BLL.FunctionClasses.Transaction;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using DERP.Report.Barcode_Print;
using DevExpress.XtraGrid;
using System;
using System.Data;
using System.Windows.Forms;

namespace DERP.Transaction.MFG
{
    public partial class FrmMFGBarcodePrint : DevExpress.XtraEditors.XtraForm
    {
        #region Declaration

        Validation Val = new Validation();
        DataTable dtPrint = new DataTable();

        FormPer ObjPer = new FormPer();
        public DataTable DTab = new DataTable();
        public string ColumnsToHide = "";
        public bool AllowMultiSelect = false;
        public string ColumnHeaderCaptions = "";
        public string SearchText = "";
        public string SearchField = "";
        public string ValueMember = "";
        public string SelectedValue = "";
        DataTable m_dtbParam = new DataTable();
        DataTable m_dtbKapan = new DataTable();
        FormEvents objBOFormEvents = new FormEvents();
        public FrmMFGProcessIssue FrmMFGProcessIssue = new FrmMFGProcessIssue();
        public FrmMFGDepartmentTransfer FrmMFGDepartmentTransfer = new FrmMFGDepartmentTransfer();
        public FrmMFGMixSplit FrmMFGMixSplit = new FrmMFGMixSplit();
        //DataTable DTab_KapanWiseData = new DataTable();

        #endregion

        #region Constructor
        public FrmMFGBarcodePrint()
        {
            InitializeComponent();
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
            objBOFormEvents.FormResize = true;
            objBOFormEvents.FormClosing = true;
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }

        #endregion

        #region Form Events
        private void FrmMFGBarcodePrint_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        #endregion

        #region Events
        private void BtnPrint_Click(object sender, EventArgs e)
        {

            if (ObjPer.AllowPrint == false)
            {
                Global.Message(BLL.GlobalDec.gStrPermissionPrintMsg);
                return;
            }
            BtnPrint.Enabled = false;
            DialogResult result = MessageBox.Show("Do you want to Barcode Print?", "Confirmation", MessageBoxButtons.YesNoCancel);
            if (result != DialogResult.Yes)
            {
                BtnPrint.Enabled = true;
                return;
            }

            BarcodePrint printBarCode = new BarcodePrint();

            DataTable dtCheckedBarcode = (DataTable)MainGrid.DataSource;
            dtCheckedBarcode.AcceptChanges();
            if (dtCheckedBarcode.Select("SEL = 'True' ").Length > 0)
            {
                dtCheckedBarcode = dtCheckedBarcode.Select("SEL = 'True' ").CopyToDataTable();
                for (int i = 0; i < dtCheckedBarcode.Rows.Count; i++)
                {
                    string Sub_lot_no = Val.ToString(dtCheckedBarcode.Rows[i]["sub_lot_no"].ToString());
                    if (Sub_lot_no.ToString() != "")
                    {
                        if (dtCheckedBarcode.Rows[i]["lot_id"] != null && dtCheckedBarcode.Rows[i]["carat"].ToString() != "")
                        {
                            printBarCode.AddPkt(dtCheckedBarcode.Rows[i]["rough_cut_no"].ToString(), Sub_lot_no, Val.DBDate(dtCheckedBarcode.Rows[i]["entry_date"].ToString()),
                                Val.ToInt(dtCheckedBarcode.Rows[i]["lot_id"]), Val.ToInt(dtCheckedBarcode.Rows[i]["pcs"]), Val.ToDecimal(dtCheckedBarcode.Rows[i]["carat"]), true);
                        }
                    }
                    else
                    {
                        printBarCode.AddPkt(dtCheckedBarcode.Rows[i]["rough_cut_no"].ToString(), Val.ToString(i + 1), Val.DBDate(dtCheckedBarcode.Rows[i]["entry_date"].ToString()),
                                Val.ToInt(dtCheckedBarcode.Rows[i]["lot_id"]), Val.ToInt(dtCheckedBarcode.Rows[i]["pcs"]), Val.ToDecimal(dtCheckedBarcode.Rows[i]["carat"]), true);
                    }

                }
                printBarCode.PrintTSC();
            }
            else
            {
                Global.Message("Please Select One Lot Atleast.");
                BtnPrint.Enabled = true;
                return;
            }
            BtnPrint.Enabled = true;
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

        private void lueCutNo_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (lueKapan.Text == "")
                {
                    Global.Message("Kapan No is Required");
                    lueKapan.Focus();
                    return;
                }
                int active = chkAllLot.Checked == true ? 1 : 0;
                MFGProcessIssue objMFGProcessIssue = new MFGProcessIssue();
                MFGProcessIssueProperty objMFGProcessIssueProperty = new MFGProcessIssueProperty();
                objMFGProcessIssueProperty.rough_cut_id = Val.ToInt(lueCutNo.EditValue);
                objMFGProcessIssueProperty.kapan_id = Val.ToInt(lueKapan.EditValue);

                DTab = objMFGProcessIssue.GetBarcodePrint(objMFGProcessIssueProperty, active);

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
                Global.Message(ex.ToString());
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void FrmMFGBarcodePrint_Load(object sender, EventArgs e)
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

                // Add By Praful On 29072021

                //DTab_KapanWiseData = Global.GetRoughStockWise(Val.ToInt(0), Val.ToInt32(0));

                // End By Praful On 29072021
            }
            catch (Exception ex)
            {
                Global.ErrorMessage(ex.Message);
            }
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
            }
        }
        private void GrdDet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        #endregion

        #region Other Function

        private void GetSummary()
        {
            try
            {
                double IntSelPcs = 0; double IntSelLot = 0; double DouSelCarat = 0;
                System.Data.DataTable DtTransfer = (System.Data.DataTable)MainGrid.DataSource;
                GrdDet.PostEditor();
                Global.DtTransfer.AcceptChanges();

                if (DtTransfer != null)
                {
                    if (DtTransfer.Rows.Count > 0)
                    {
                        foreach (DataRow DRow in DtTransfer.Rows)
                        {
                            if (Val.ToString(DRow["SEL"]) == "True")
                            {
                                IntSelLot = IntSelLot + 1;
                                IntSelPcs = IntSelPcs + Val.Val(DRow["pcs"]);
                                DouSelCarat = DouSelCarat + Val.Val(DRow["carat"]);
                            }
                        }
                    }
                }
                txtSelLot.Text = IntSelLot.ToString();
                txtSelPcs.Text = IntSelPcs.ToString();
                txtSelCarat.Text = DouSelCarat.ToString();
            }
            catch
            {
            }
        }

        #endregion

        #region Repository Events
        private void repSelect_CheckedChanged(object sender, EventArgs e)
        {
            GetSummary();
        }

        private void repSelect_MouseUp(object sender, MouseEventArgs e)
        {
            GetSummary();
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
        #endregion

        private void chkAllLot_CheckedChanged(object sender, EventArgs e)
        {

            try
            {
                if (lueKapan.Text == "")
                {
                    Global.Message("Kapan No is Required");
                    lueKapan.Focus();
                    return;
                }
                int active = chkAllLot.Checked == true ? 1 : 0;
                MFGProcessIssue objMFGProcessIssue = new MFGProcessIssue();
                MFGProcessIssueProperty objMFGProcessIssueProperty = new MFGProcessIssueProperty();
                objMFGProcessIssueProperty.rough_cut_id = Val.ToInt(lueCutNo.EditValue);
                objMFGProcessIssueProperty.kapan_id = Val.ToInt(lueKapan.EditValue);

                DTab = objMFGProcessIssue.GetBarcodePrint(objMFGProcessIssueProperty, active);

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
                Global.Message(ex.ToString());
            }
        }

        private void txtLotID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtLotID_Validated(object sender, EventArgs e)
        {
            if (txtLotID.Text.Length == 0)
            {
                return;
            }

            foreach (DataRow DRow in DTab.Rows)
            {
                if (Val.ToString(DRow["lot_id"]) == txtLotID.Text)
                {
                    DRow["SEL"] = true;
                }
            }
            GetSummary();
            txtLotID.Text = "";
            txtLotID.Focus();
        }
    }
}