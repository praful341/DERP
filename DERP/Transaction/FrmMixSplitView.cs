using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Transaction;
using BLL.PropertyClasses.Transaction;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Global = DERP.Class.Global;

namespace DERP.Transaction
{
    public partial class FrmMixSplitView : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member
        
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        Control _NextEnteredControl;
        private List<Control> _tabControls;

        MixSplit objMixSplit;
        AssortMaster objAssort;
        SieveMaster objSieve;

        DataTable DtControlSettings;
        DataTable DTab;
        public string GblLockBarcode { get; set; }

        #endregion

        #region Constructor
        public FrmMixSplitView()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            _NextEnteredControl = new Control();
            _tabControls = new List<Control>();

            objMixSplit = new MixSplit();
            objAssort = new AssortMaster();
            objSieve = new SieveMaster();

            DtControlSettings = new DataTable();
            DTab = new DataTable();
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

            // for Dynamic Setting By Praful On 01022020

            //Global.HideFormControls(Val.ToInt(ObjPer.form_id), this);

            ControlSettingDT(Val.ToInt(ObjPer.form_id), this);
            AddGotFocusListener(this);
            this.KeyPreview = true;

            TabControlsToList(this.Controls);
            _tabControls = _tabControls.OrderBy(x => x.TabIndex).ToList();

            // End for Dynamic Setting By Praful On 01022020

            this.Show();
        }

        private void AddGotFocusListener(Control ctrl)
        {
            foreach (Control c in ctrl.Controls)
            {
                c.GotFocus += new EventHandler(Control_GotFocus);
                if (c.Controls.Count > 0)
                {
                    AddGotFocusListener(c);
                }
            }
        }
        private void Control_GotFocus(object sender, EventArgs e)
        {
            if (!((Control)sender).Name.ToString().Trim().Equals(string.Empty))
            {
                _NextEnteredControl = (Control)sender;
                if ((Control)sender is LookUpEdit)
                {
                    ((LookUpEdit)(Control)sender).ShowPopup();
                }
                if ((Control)sender is CheckedComboBoxEdit)
                {
                    ((CheckedComboBoxEdit)(Control)sender).ShowPopup();
                }
                if ((Control)sender is ComboBoxEdit)
                {
                    ((ComboBoxEdit)(Control)sender).ShowPopup();
                }
            }
        }

        private void TabControlsToList(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control.TabStop)
                    _tabControls.Add(control);
                if (control.HasChildren)
                    TabControlsToList(control.Controls);
            }
        }
        private void ControlSettingDT(int FormCode, Form pForm)
        {
            BLL.Validation Val = new BLL.Validation();
            Single_Setting ObjSingleSettings = new Single_Setting();
            Single_SettingProperty Property = new Single_SettingProperty();

            Property.role_id = Val.ToInt(BLL.GlobalDec.gEmployeeProperty.role_id);
            Property.form_id = Val.ToInt(FormCode);
            DataTable DtColSetting = ObjSingleSettings.GetData(Property);

            DataTable DtFilterColSetting = (from DataRow dr in DtColSetting.Rows
                                            where Val.ToBooleanToInt(dr["is_control"]) == 1
                                            && dr["column_type"].ToString() != "LABEL"
                                            select dr).CopyToDataTable();
            DevExpress.XtraLayout.LayoutControl l = new DevExpress.XtraLayout.LayoutControl();
            l.OptionsFocus.EnableAutoTabOrder = false;

            if (DtFilterColSetting.Rows.Count > 0)
            {
                DtControlSettings = DtFilterColSetting;
                foreach (Control item1 in pForm.Controls)
                {
                    ControllSettings(item1, DtFilterColSetting);
                }
            }
        }

        private static void ControllSettings(Control item2, DataTable DTab)
        {
            BLL.Validation Val = new BLL.Validation();

            //else
            {
                var VarControlSetting = (from DataRow dr in DTab.Rows
                                         where dr["column_name"].ToString() == item2.Name.ToString()
                                         select dr);

                if (VarControlSetting.Count() > 0)
                {
                    DataRow DRow = VarControlSetting.CopyToDataTable().Rows[0];
                    if (item2.Name.ToString() == Val.ToString(DRow["column_name"]))
                    {
                        if (!(item2 is TextEdit))
                        {
                            if (!(item2 is DateTimePicker))
                            {
                                if (!(item2 is DevExpress.XtraEditors.TextEdit))
                                {
                                    item2.Text = (Val.ToBooleanToInt(DRow["is_compulsory"]).Equals(0) ? Val.ToString(DRow["caption"]) : "* " + Val.ToString(DRow["caption"]));
                                }
                            }
                        }
                        if (Val.ToInt(DRow["tab_index"]) >= 0)
                        {
                            if (item2.CanSelect)
                                item2.TabStop = true;
                        }
                        else
                            item2.TabStop = false;
                        if (Val.ToBooleanToInt(DRow["is_visible"]).Equals(1))
                        {
                            item2.Visible = true;
                            item2.TabStop = true;
                        }
                        else
                        {
                            item2.Visible = false;
                            item2.TabStop = false;
                        }

                        item2.TabIndex = Val.ToInt(DRow["tab_index"]);
                        if (item2.TabIndex == 1)
                        {
                            item2.Select();
                            item2.Focus();
                        }
                        if (Val.ToBooleanToInt(DRow["is_editable"]).Equals(1))
                        {
                            item2.Enabled = true;
                        }
                        else
                        {
                            item2.Enabled = false;
                        }
                    }
                }
                else
                {
                    item2.TabStop = false;
                }
            }
            if (item2.Controls.Count > 0)
            {
                foreach (Control item1 in item2.Controls)
                {
                    ControllSettings(item1, DTab);
                }
            }
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

        #region Events
        private void FrmMixSplitView_Shown(object sender, EventArgs e)
        {
            dtpTransactionFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            dtpTransactionFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            dtpTransactionFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            dtpTransactionFromDate.Properties.CharacterCasing = CharacterCasing.Upper;

            dtpTransactionToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            dtpTransactionToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            dtpTransactionToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            dtpTransactionToDate.Properties.CharacterCasing = CharacterCasing.Upper;

            dtpTransactionFromDate.EditValue = DateTime.Now;
            dtpTransactionToDate.EditValue = DateTime.Now;

            DataTable m_dtbFromAssorts = objAssort.GetData();
            ChkCmbFromAssort.Properties.DataSource = m_dtbFromAssorts;
            ChkCmbFromAssort.Properties.ValueMember = "assort_id";
            ChkCmbFromAssort.Properties.DisplayMember = "assort_name";

            ChkCmbToAssort.Properties.DataSource = m_dtbFromAssorts;
            ChkCmbToAssort.Properties.ValueMember = "assort_id";
            ChkCmbToAssort.Properties.DisplayMember = "assort_name";

            DataTable m_dtbFromSieve = objSieve.GetData();
            ChkCmbFromSieve.Properties.DataSource = m_dtbFromSieve;
            ChkCmbFromSieve.Properties.ValueMember = "sieve_id";
            ChkCmbFromSieve.Properties.DisplayMember = "sieve_name";

            ChkCmbToSieve.Properties.DataSource = m_dtbFromSieve;
            ChkCmbToSieve.Properties.ValueMember = "sieve_id";
            ChkCmbToSieve.Properties.DisplayMember = "sieve_name";
        }
        private void Btn_Show_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;


                MixSplitProperty objMixSplitProperty = new MixSplitProperty();

                objMixSplitProperty.from_assort = ChkCmbFromAssort.Properties.GetCheckedItems().ToString();
                objMixSplitProperty.to_assort = ChkCmbToAssort.Properties.GetCheckedItems().ToString();
                objMixSplitProperty.from_sieve = ChkCmbFromSieve.Properties.GetCheckedItems().ToString();
                objMixSplitProperty.To_sieve = ChkCmbToSieve.Properties.GetCheckedItems().ToString();
                objMixSplitProperty.mixsplit_fromdate = Val.DBDate(dtpTransactionFromDate.Text);
                objMixSplitProperty.mixsplit_todate = Val.DBDate(dtpTransactionToDate.Text);
                if (CmbType.Text.ToString() == "MIX")
                {
                    objMixSplitProperty.mixsplit_type_id = Val.ToInt(1);
                }
                else if (CmbType.Text.ToString() == "SPLIT")
                {
                    objMixSplitProperty.mixsplit_type_id = Val.ToInt(2);
                }
                else if (CmbType.Text.ToString() == "")
                {
                    objMixSplitProperty.mixsplit_type_id = Val.ToInt(0);
                }
                if (Val.ToString(this.SelRdo.EditValue) == "S")
                {
                    objMixSplitProperty.trn_type = Val.ToString("Summary");
                }
                else
                {
                    objMixSplitProperty.trn_type = Val.ToString("Detail");
                }
                DTab = objMixSplit.GetMixSplitView(objMixSplitProperty);
                if (DTab.Rows.Count > 0)
                {
                    this.Cursor = Cursors.Default;

                    if (Val.ToString(this.SelRdo.EditValue) == "S")
                    {
                        MainDgvSplitDetail.Visible = false;
                        MainDgvMixDetail.Visible = true;
                        MainDgvMixDetail.DataSource = DTab;
                        DgvMixDetails.BestFitColumns();

                        GridColumnSummaryItem Itm1 = new GridColumnSummaryItem();
                        DgvMixDetails.Columns["From Pcs"].Summary.Clear();
                        Itm1.FieldName = "From Pcs";
                        Itm1.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        DgvMixDetails.Columns["From Pcs"].Summary.Add(Itm1);

                        GridColumnSummaryItem Itm2 = new GridColumnSummaryItem();
                        DgvMixDetails.Columns["From Carat"].Summary.Clear();
                        Itm2.FieldName = "From Carat";
                        Itm2.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        DgvMixDetails.Columns["From Carat"].Summary.Add(Itm2);

                        GridColumnSummaryItem Itm4 = new GridColumnSummaryItem();
                        DgvMixDetails.Columns["From Amount"].Summary.Clear();
                        Itm4.FieldName = "From Amount";
                        Itm4.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        DgvMixDetails.Columns["From Amount"].Summary.Add(Itm4);

                        GridColumnSummaryItem Itm5 = new GridColumnSummaryItem();
                        DgvMixDetails.Columns["To Pcs"].Summary.Clear();
                        Itm5.FieldName = "To Pcs";
                        Itm5.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        DgvMixDetails.Columns["To Pcs"].Summary.Add(Itm5);

                        GridColumnSummaryItem Itm6 = new GridColumnSummaryItem();
                        DgvMixDetails.Columns["To Carat"].Summary.Clear();
                        Itm6.FieldName = "To Carat";
                        Itm6.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        DgvMixDetails.Columns["To Carat"].Summary.Add(Itm6);


                        GridColumnSummaryItem Itm8 = new GridColumnSummaryItem();
                        DgvMixDetails.Columns["To Amount"].Summary.Clear();
                        Itm8.FieldName = "To Amount";
                        Itm8.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        DgvMixDetails.Columns["To Amount"].Summary.Add(Itm8);

                        GridColumnSummaryItem Itm9 = new GridColumnSummaryItem();
                        DgvMixDetails.Columns["Loss Carat"].Summary.Clear();
                        Itm9.FieldName = "Loss Carat";
                        Itm9.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        DgvMixDetails.Columns["Loss Carat"].Summary.Add(Itm9);

                        GridColumnSummaryItem Itm10 = new GridColumnSummaryItem();
                        DgvMixDetails.Columns["Carat Plus"].Summary.Clear();
                        Itm10.FieldName = "Carat Plus";
                        Itm10.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        DgvMixDetails.Columns["Carat Plus"].Summary.Add(Itm10);

                        DgvMixDetails.BeginSummaryUpdate();
                        DgvMixDetails.EndSummaryUpdate();
                    }
                    else
                    {
                        MainDgvSplitDetail.Visible = true;
                        MainDgvMixDetail.Visible = false;
                        MainDgvSplitDetail.DataSource = DTab;
                        DgvSplitDetail.BestFitColumns();

                        GridColumnSummaryItem Itm1 = new GridColumnSummaryItem();
                        DgvSplitDetail.Columns["From Pcs"].Summary.Clear();
                        Itm1.FieldName = "From Pcs";
                        Itm1.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        DgvSplitDetail.Columns["From Pcs"].Summary.Add(Itm1);

                        GridColumnSummaryItem Itm2 = new GridColumnSummaryItem();
                        DgvSplitDetail.Columns["From Carat"].Summary.Clear();
                        Itm2.FieldName = "From Carat";
                        Itm2.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        DgvSplitDetail.Columns["From Carat"].Summary.Add(Itm2);

                        GridColumnSummaryItem Itm4 = new GridColumnSummaryItem();
                        DgvSplitDetail.Columns["From Amount"].Summary.Clear();
                        Itm4.FieldName = "From Amount";
                        Itm4.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        DgvSplitDetail.Columns["From Amount"].Summary.Add(Itm4);

                        GridColumnSummaryItem Itm5 = new GridColumnSummaryItem();
                        DgvSplitDetail.Columns["To Pcs"].Summary.Clear();
                        Itm5.FieldName = "To Pcs";
                        Itm5.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        DgvSplitDetail.Columns["To Pcs"].Summary.Add(Itm5);

                        GridColumnSummaryItem Itm6 = new GridColumnSummaryItem();
                        DgvSplitDetail.Columns["To Carat"].Summary.Clear();
                        Itm6.FieldName = "To Carat";
                        Itm6.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        DgvSplitDetail.Columns["To Carat"].Summary.Add(Itm6);


                        GridColumnSummaryItem Itm8 = new GridColumnSummaryItem();
                        DgvSplitDetail.Columns["To Amount"].Summary.Clear();
                        Itm8.FieldName = "To Amount";
                        Itm8.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        DgvSplitDetail.Columns["To Amount"].Summary.Add(Itm8);

                        GridColumnSummaryItem Itm9 = new GridColumnSummaryItem();
                        DgvSplitDetail.Columns["Loss Carat"].Summary.Clear();
                        Itm9.FieldName = "Loss Carat";
                        Itm9.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        DgvSplitDetail.Columns["Loss Carat"].Summary.Add(Itm9);

                        GridColumnSummaryItem Itm10 = new GridColumnSummaryItem();
                        DgvSplitDetail.Columns["Carat Plus"].Summary.Clear();
                        Itm10.FieldName = "Carat Plus";
                        Itm10.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        DgvSplitDetail.Columns["Carat Plus"].Summary.Add(Itm10);
                    }
                }
                else
                {
                    Global.Message("Data Not Found");
                    MainDgvMixDetail.DataSource = null;
                    MainDgvSplitDetail.DataSource = null;
                    DgvSplitDetail.BestFitColumns();
                    DgvMixDetails.BestFitColumns();
                    this.Cursor = Cursors.Default;
                    return;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.ToString());
            }
        }
        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Export("xls", "Export to Excel", "Excel files 97-2003 (*.xls)|*.xls|Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*");
        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Export("pdf", "Export Report to PDF", "PDF (*.PDF)|*.PDF");
        }
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Export("txt", "Export to Text", "Text files (*.txt)|*.txt|All files (*.*)|*.*");
        }
        #region Context Menu Events
        private void MNExportToExcel_Click(object sender, EventArgs e)
        {
            Export("xls", "Export to Excel", "Excel files 97-2003 (*.xls)|*.xls|Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*");
        }
        private void MNExportToText_Click(object sender, EventArgs e)
        {
            Export("txt", "Export to Text", "Text files (*.txt)|*.txt|All files (*.*)|*.*");
        }
        private void MNExportToHTML_Click(object sender, EventArgs e)
        {
            Export("html", "Export to HTML", "Html files (*.html)|*.html|Htm files (*.htm)|*.htm");
        }
        private void MNExportToRTF_Click(object sender, EventArgs e)
        {
            Export("rtf", "Export to RTF", "Word (*.doc) |*.doc;*.rtf|(*.txt) |*.txt|(*.*) |*.*");
        }
        private void MNExportToPDF_Click(object sender, EventArgs e)
        {
            Export("pdf", "Export Report to PDF", "PDF (*.PDF)|*.PDF");
        }
        private void MNExportCSV_Click(object sender, EventArgs e)
        {
            Export("csv", "Export Report to CSVB", "csv (*.csv)|*.csv");
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
                            DgvMixDetails.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            DgvMixDetails.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            DgvMixDetails.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            DgvMixDetails.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            DgvMixDetails.ExportToText(Filepath);
                            break;
                        case "html":
                            DgvMixDetails.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            DgvMixDetails.ExportToCsv(Filepath);
                            break;
                    }
                }
            }


            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString(), "Error in Export");
            }
        }
        private void Export_Split(string format, string dlgHeader, string dlgFilter)
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
                            DgvSplitDetail.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            DgvSplitDetail.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            DgvSplitDetail.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            DgvSplitDetail.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            DgvSplitDetail.ExportToText(Filepath);
                            break;
                        case "html":
                            DgvSplitDetail.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            DgvSplitDetail.ExportToCsv(Filepath);
                            break;
                    }
                }
            }


            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString(), "Error in Export");
            }
        }

        #endregion

        #endregion

        #region Functions
        public void LoadPacketData(string pStrBarcodeList)
        {
            try
            {
                this.GblLockBarcode = string.Join(",", pStrBarcodeList);

                Btn_Show_Click(null, null);
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        #endregion
    }
}