using BLL;
using BLL.FunctionClasses.Transaction;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using DREP.Transaction;
using System;
using System.Data;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Transaction.MFG
{
    public partial class FrmMFGKapanUpdateSearch : DevExpress.XtraEditors.XtraForm
    {
        #region Declaration


        Validation Val = new Validation();
        MFGKapanCreate objKapanCreate;
        DataTable dtPrint = new DataTable();
        DataTable m_dtbKapan = new DataTable();
        DataTable m_dtbParam = new DataTable();
        public DataTable DTab = new DataTable();
        public string ColumnsToHide = "";
        public bool AllowMultiSelect = false;
        public string ColumnHeaderCaptions = "";
        public string SearchText = "";
        public string SearchField = "";
        public string ValueMember = "";
        public string SelectedValue = "";

        FormEvents objBOFormEvents = new FormEvents();
        public FrmMFGKapanCreate FrmMFGKapan = new FrmMFGKapanCreate();
        //public FrmMFGKapanMixing FrmMFGKapanMixing = new FrmMFGKapanMixing();

        string FormName = "";
        int m_numSelectedCount = 0;
        int m_KapanId = 0;
        #endregion

        #region Constructor
        public FrmMFGKapanUpdateSearch()
        {
            InitializeComponent();
            objKapanCreate = new MFGKapanCreate();
        }
        public void ShowForm(FrmMFGKapanCreate ObjForm, int KapanId)
        {
            FrmMFGKapan = new FrmMFGKapanCreate();
            FrmMFGKapan = ObjForm;
            FormName = "FrmMFGKapanCreate";
            m_KapanId = KapanId;
            Val.frmGenSetForPopup(this);
            AttachFormEvents();
            this.ShowDialog();


        }
        //public void ShowForm(FrmMFGKapanMixing ObjForm1)
        //{
        //    FrmMFGKapanMixing = new FrmMFGKapanMixing();
        //    FrmMFGKapanMixing = ObjForm1;
        //    FormName = "FrmMFGKapanMixing";

        //    Val.frmGenSetForPopup(this);
        //    AttachFormEvents();
        //    this.ShowDialog();
        //}
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
                DataTable dtab = new DataTable();
                if (FormName == "FrmMFGKapanCreate")
                {
                    dtab = objKapanCreate.GetSearchKapan(Val.ToInt(m_KapanId));

                    if (dtab.Columns.Contains("SEL") == false)
                    {
                        if (dtab.Columns.Contains("SEL") == false)
                        {
                            DataColumn Col = new DataColumn();
                            Col.ColumnName = "SEL";
                            Col.DataType = typeof(bool);
                            Col.DefaultValue = false;
                            dtab.Columns.Add(Col);
                        }
                    }
                    dtab.Columns["SEL"].SetOrdinal(0);
                }

                MainGrid.DataSource = (DataTable)dtab;
                //repEntryDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                //repEntryDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                //repEntryDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                //repEntryDate.Properties.CharacterCasing = CharacterCasing.Upper;


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
        #endregion

        #region Events

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
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
        private void GrdDet_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    //if (e.Clicks == 2)
                    //{
                    //DataRow Drow = GrdDet.GetDataRow(e.RowHandle);
                    //m_MixSplitId = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, "mixsplit_union_id"));
                    //objKapanCreate.Kapan_Date_Update(m_MixSplitId, m_KapanId);
                    //this.Close();

                    //}
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
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
                //txtSelLot.Text = IntSelLot.ToString();
                //txtSelPcs.Text = IntSelPcs.ToString();
                //txtSelCarat.Text = DouSelCarat.ToString();
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

        #endregion

        #region Checkbox Events

        //private void ChkAll_CheckedChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        for (int i = 0; i < GrdDet.RowCount; i++)
        //        {
        //            if (ChkAll.Checked == true)
        //            {
        //                bool b = true;
        //                GrdDet.SetRowCellValue(i, "SEL", b);
        //            }
        //            else
        //            {
        //                bool b = false;
        //                GrdDet.SetRowCellValue(i, "SEL", b);
        //            }
        //        }
        //        GetSummary();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //}
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
        private void repSelect_CheckedChanged(object sender, EventArgs e)
        {
            GetSummary();
        }
        #endregion

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int IntRes = 0;
            DataTable DTabList = new DataTable();
            DTabList = (DataTable)MainGrid.DataSource;
            DTabList = DTabList.Select("SEL = true").CopyToDataTable();
            MFGKapanCreateProperty objKapanProperty = new MFGKapanCreateProperty();
            if (DTabList.Rows.Count > 0)
            {
                foreach (DataRow drw in DTabList.Rows)
                {
                    objKapanProperty.mixsplit_id = Val.ToInt64(drw["mixsplit_id"]);
                    objKapanProperty.kapan_date = Val.DBDate(drw["entry_date"].ToString());
                    objKapanProperty.kapan_id = Val.ToInt64(drw["kapan_id"].ToString());
                    objKapanProperty.rate = Val.ToDecimal(drw["rate"].ToString());

                    //m_MixSplitId = Val.ToInt(drw["mixsplit_id"]);
                    //Entrydate = Val.DBDate(drw["entry_date"]);
                    IntRes = objKapanCreate.Kapan_Date_Update(objKapanProperty);
                }
                if (IntRes > 0)
                {
                    MessageBox.Show("Update Successfully");
                }
                else
                {
                    MessageBox.Show("Error in Update");

                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Select any one Kapan!!!");

            }

        }
    }
}