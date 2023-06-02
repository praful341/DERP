using BLL;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.PropertyClasses.Transaction;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Global = DERP.Class.Global;

namespace DERP.Transaction
{
    public partial class FrmMfgHistoryView : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        Control _NextEnteredControl;
        private List<Control> _tabControls;

        MFGHistoryView objHistoryView;
        AssortMaster objAssort;
        SieveMaster objSieve;

        DataTable DtControlSettings;
        DataTable DTab;
        DataTable m_dtbKapan;
        DataTable m_dtbParam;
        DataTable tdt_;
        DataTable m_Dt;
        DataTable m_dtbReceiveProcess;
        //DataTable DTab_KapanWiseData = new DataTable();
        int m_IsLot = 0;
        bool m_blnflag;
        int m_updateIssueId = 0;
        decimal m_numSummRate = 0;
        Int64 m_Lot_id = 0;
        int m_OldOrgPcs = 0;
        decimal m_OldOrgCarat = 0;
        int m_OldRecPcs = 0;
        decimal m_OldRecCarat = 0;
        int m_OldRRPcs = 0;
        decimal m_OldRRCarat = 0;
        int m_OldRejPcs = 0;
        decimal m_OldRejCarat = 0;
        int m_OldResawPcs = 0;
        decimal m_OldResawCarat = 0;
        int m_OldBreakPcs = 0;
        decimal m_OldBreakCarat = 0;
        int m_OldLostPcs = 0;
        decimal m_OldLostCarat = 0;
        decimal m_OldLossCarat = 0;
        decimal m_OldPlusCarat = 0;
        Int64 Lot_Srno = 0;
        Int64 Process_id = 0;
        Int64 History_Type_id = 0;
        Int64 Janged_ID = 0;
        Int64 Party_ID = 0;
        Int64 Sieve_ID = 0;
        Int64 Rough_Clarity_ID = 0;
        Int64 Quality_ID = 0;

        public string GblLockBarcode { get; set; }

        #endregion

        #region Constructor
        public FrmMfgHistoryView()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            _NextEnteredControl = new Control();
            _tabControls = new List<Control>();

            objHistoryView = new MFGHistoryView();
            objAssort = new AssortMaster();
            objSieve = new SieveMaster();

            DtControlSettings = new DataTable();
            DTab = new DataTable();
            m_dtbKapan = new DataTable();
            m_dtbParam = new DataTable();
            tdt_ = new DataTable();
            m_Dt = new DataTable();
            m_dtbReceiveProcess = new DataTable();

            m_blnflag = new bool();
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
        public void ShowForm_New(Int64 LotId)
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
            // End for Dynamic Setting By Praful On 01022020

            m_Lot_id = LotId;

            this.Show();
            txtLotID.Text = Val.ToString(m_Lot_id);
            txtLotID_Validated(null, null);

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
            //dtpTransactionFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            //dtpTransactionFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            //dtpTransactionFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            //dtpTransactionFromDate.Properties.CharacterCasing = CharacterCasing.Upper;

            //dtpTransactionToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            //dtpTransactionToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            //dtpTransactionToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            //dtpTransactionToDate.Properties.CharacterCasing = CharacterCasing.Upper;

            //dtpTransactionFromDate.EditValue = DateTime.Now;
            //dtpTransactionToDate.EditValue = DateTime.Now;

            //DataTable m_dtbFromAssorts = objAssort.GetData();
            //ChkCmbFromAssort.Properties.DataSource = m_dtbFromAssorts;
            //ChkCmbFromAssort.Properties.ValueMember = "assort_id";
            //ChkCmbFromAssort.Properties.DisplayMember = "assort_name";

            //ChkCmbToAssort.Properties.DataSource = m_dtbFromAssorts;
            //ChkCmbToAssort.Properties.ValueMember = "assort_id";
            //ChkCmbToAssort.Properties.DisplayMember = "assort_name";

            //DataTable m_dtbFromSieve = objSieve.GetData();
            //ChkCmbFromSieve.Properties.DataSource = m_dtbFromSieve;
            //ChkCmbFromSieve.Properties.ValueMember = "sieve_id";
            //ChkCmbFromSieve.Properties.DisplayMember = "sieve_name";

            //ChkCmbToSieve.Properties.DataSource = m_dtbFromSieve;
            //ChkCmbToSieve.Properties.ValueMember = "sieve_id";
            //ChkCmbToSieve.Properties.DisplayMember = "sieve_name";
        }
        private void Btn_Show_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                int Active = chkActive.Checked == true ? 1 : 0;
                DataTable DtTab = new DataTable();
                DtTab = objHistoryView.GetHistoryData(Val.ToInt(lueKapan.EditValue), Val.ToInt(lueCutNo.EditValue), Val.ToInt64(txtLotID.Text), Val.ToInt(Active));
                if (DtTab.Rows.Count > 0)
                {
                    MainDgvHistoryDetail.DataSource = DtTab;
                    this.Cursor = Cursors.Default;
                    (MainDgvHistoryDetail.FocusedView as GridView).MoveLast();
                }
                else
                {
                    Global.Message("Data Not Found");
                    MainDgvHistoryDetail.DataSource = null;
                    Btn_Show.Focus();
                    this.Cursor = Cursors.Default;
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
        private void DgvHistoryDetail_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                if (Val.ToDecimal(clmAmount.SummaryItem.SummaryValue) > 0 && Val.ToDecimal(clmCarat.SummaryItem.SummaryValue) > 0)
                {
                    m_numSummRate = Math.Round((Val.ToDecimal(clmAmount.SummaryItem.SummaryValue) / Val.ToDecimal(clmCarat.SummaryItem.SummaryValue)), 2, MidpointRounding.AwayFromZero);

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
                            DgvHistoryDetail.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            DgvHistoryDetail.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            DgvHistoryDetail.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            DgvHistoryDetail.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            DgvHistoryDetail.ExportToText(Filepath);
                            break;
                        case "html":
                            DgvHistoryDetail.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            DgvHistoryDetail.ExportToCsv(Filepath);
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
                            DgvHistoryDetail.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            DgvHistoryDetail.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            DgvHistoryDetail.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            DgvHistoryDetail.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            DgvHistoryDetail.ExportToText(Filepath);
                            break;
                        case "html":
                            DgvHistoryDetail.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            DgvHistoryDetail.ExportToCsv(Filepath);
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

        private void lueKapan_EditValueChanged(object sender, EventArgs e)
        {
            //if (m_IsLot == 0)
            //{
            m_dtbParam = new DataTable();
            if (lueKapan.Text.ToString() != "")
            {
                m_dtbParam = Global.GetRoughKapanWise_Data(Val.ToInt(lueKapan.EditValue));
                //if (m_dtbParam.Rows.Count == 0)
                //{
                //    m_dtbParam = DTab_KapanWiseData;
                //}
            }
            //if (m_IsLot == 1)
            //{
            //    m_dtbParam = Global.GetRoughKapanWise(0, Val.ToInt64(txtLotID.Text));
            //}
            lueCutNo.Properties.DataSource = m_dtbParam;
            lueCutNo.Properties.ValueMember = "rough_cut_id";
            lueCutNo.Properties.DisplayMember = "rough_cut_no";

            //}
        }

        private void lueCutNo_EditValueChanged(object sender, EventArgs e)
        {
            try
            {

                if (!m_blnflag)
                {
                    if (lueCutNo.EditValue != System.DBNull.Value)
                    {
                        if (m_dtbParam.Rows.Count > 0)
                        {
                            DataRow[] dr = m_dtbParam.Select("rough_cut_no ='" + Val.ToString(lueCutNo.Text) + "'");
                            if (m_IsLot == 0)
                            {
                                if (dr.Length > 0)
                                {
                                    txtLotID.Text = Val.ToString(dr[0]["lot_id"]);
                                    //prdId = Val.ToInt(dr[0]["prediction_id"]);

                                }
                            }
                        }
                    }
                    //if (lueProcess.EditValue != System.DBNull.Value && lueSubProcess.EditValue != System.DBNull.Value && Val.ToInt64(txtLotID.Text) != 0)
                    //{
                    //    DataTable dtIss = new DataTable();
                    //    dtIss = objProcessRecieve.GetIssueID(Val.ToInt64(txtLotID.Text), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue));
                    //    if (dtIss.Rows.Count > 0)
                    //    {
                    //        Global.Message("Lot is already issue in this process.");
                    //    }
                    //}
                    m_blnflag = false;
                }

                else
                {
                    m_blnflag = false;
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void chkActive_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DataTable DtTab = new DataTable();
                int active = chkActive.Checked == true ? 1 : 0;
                DtTab = objHistoryView.GetHistoryData(Val.ToInt(lueKapan.EditValue), Val.ToInt(lueCutNo.EditValue), Val.ToInt64(txtLotID.Text), active);
                if (DtTab.Rows.Count > 0)
                {
                    MainDgvHistoryDetail.DataSource = DtTab;
                    this.Cursor = Cursors.Default;
                }
                else
                {
                    Global.Message("Data Not Found");
                    MainDgvHistoryDetail.DataSource = null;
                    Btn_Show.Focus();
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.ToString());
            }
        }
        private void txtLotID_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void FrmMfgHistoryView_Load(object sender, EventArgs e)
        {
            m_dtbKapan = Global.GetKapanAll();

            lueKapan.Properties.DataSource = m_dtbKapan;
            lueKapan.Properties.ValueMember = "kapan_id";
            lueKapan.Properties.DisplayMember = "kapan_no";

            m_dtbParam = Global.GetRoughCutAll();

            lueCutNo.Properties.DataSource = m_dtbParam;
            lueCutNo.Properties.ValueMember = "rough_cut_id";
            lueCutNo.Properties.DisplayMember = "rough_cut_no";

            Global.LOOKUPDepartment_New(lueToDepartment);
            Global.LOOKUPAllManager(lueManager);
            Global.LOOKUPRoughSieve(lueRoughSieve);
            Global.LOOKUPQuality(lueQuality);
            Global.LOOKUPClarity(lueClarity);
            Global.LOOKUPAllManager(lueUpdManager);
            Global.LOOKUPEmpHistory(lueEmployee);
            Global.LOOKUPParty(lueUpdParty);

            // Add By Praful On 29072021

            //DTab_KapanWiseData = Global.GetRoughStockWise(Val.ToInt(0), Val.ToInt32(0));

            // End By Praful On 29072021

            (MainDgvHistoryDetail.FocusedView as GridView).MoveLast();
            //pnlAllPcsUpdate.Visible = true;

            if (GlobalDec.gEmployeeProperty.role_name.ToUpper() == "SURAT SARIN" || GlobalDec.gEmployeeProperty.role_name.ToUpper() == "SURAT 4P" || GlobalDec.gEmployeeProperty.role_name.ToUpper() == "SURAT MAKABLE" ||
              GlobalDec.gEmployeeProperty.role_name.ToUpper() == "SURAT ADMIN" || GlobalDec.gEmployeeProperty.role_name.ToUpper() == "SURAT ROUGH" || GlobalDec.gEmployeeProperty.role_name.ToUpper() == "SURAT RUSSIAN" ||
              GlobalDec.gEmployeeProperty.role_name.ToUpper() == "SURAT ASSORT" || GlobalDec.gEmployeeProperty.role_name.ToUpper() == "SURAT KATARGAM" || GlobalDec.gEmployeeProperty.role_name.ToUpper() == "SURAT POLISH REPARING" ||
              GlobalDec.gEmployeeProperty.role_name.ToUpper() == "SURAT KAMALA" || GlobalDec.gEmployeeProperty.role_name.ToUpper() == "KAMALA ADMIN" || GlobalDec.gEmployeeProperty.role_name.ToUpper() == "AMBIKA ADMIN")
            {
                if (GlobalDec.gEmployeeProperty.user_name.ToUpper() == "PRAFUL" || GlobalDec.gEmployeeProperty.user_name.ToUpper() == "DAKSHAY" || GlobalDec.gEmployeeProperty.user_name.ToUpper() == "KETAN" ||
                        GlobalDec.gEmployeeProperty.user_name.ToUpper() == "JAYESH" || GlobalDec.gEmployeeProperty.user_name.ToUpper() == "LALU" || GlobalDec.gEmployeeProperty.user_name.ToUpper() == "KAILASH" || GlobalDec.gEmployeeProperty.user_name.ToUpper() == "PRAGNESH" ||
                        GlobalDec.gEmployeeProperty.user_name.ToUpper() == "RONAK" || GlobalDec.gEmployeeProperty.user_name.ToUpper() == "AKSHAY" || GlobalDec.gEmployeeProperty.user_name.ToUpper() == "SUBHASH" ||
                        GlobalDec.gEmployeeProperty.user_name.ToUpper() == "KUNAL" || GlobalDec.gEmployeeProperty.user_name.ToUpper() == "CHIRAG" || GlobalDec.gEmployeeProperty.user_name.ToUpper() == "SAHIL" ||
                        GlobalDec.gEmployeeProperty.user_name.ToUpper() == "PRINCEP" || GlobalDec.gEmployeeProperty.user_name.ToUpper() == "RAHUL" || GlobalDec.gEmployeeProperty.user_name.ToUpper() == "PARAGA"
                        || GlobalDec.gEmployeeProperty.user_name.ToUpper() == "JAYESHJ" || GlobalDec.gEmployeeProperty.user_name.ToUpper() == "BHADRESH" || GlobalDec.gEmployeeProperty.user_name.ToUpper() == "YOGESH"
                        || GlobalDec.gEmployeeProperty.user_name.ToUpper() == "RAVIR" || GlobalDec.gEmployeeProperty.user_name.ToUpper() == "YTHUMMAR" || GlobalDec.gEmployeeProperty.user_name.ToUpper() == "SAROJ" || GlobalDec.gEmployeeProperty.user_name.ToUpper() == "DEVANGI")
                {
                    btnDelete.Visible = true;
                    btnUpdate.Visible = true;
                    btnAllPcsUpdate.Visible = true;
                }
                else if (GlobalDec.gEmployeeProperty.user_name.ToUpper() == "KG")
                {
                    btnDelete.Visible = true;
                }
                else
                {
                    btnDelete.Visible = false;
                    btnUpdate.Visible = false;
                    btnAllPcsUpdate.Visible = false;
                }

                if (GlobalDec.gEmployeeProperty.user_name.ToUpper() == "KANAIYA" || GlobalDec.gEmployeeProperty.user_name.ToUpper() == "PRAFUL")
                {
                    btnUpdate.Visible = true;
                }
            }
            else
            {
                btnDelete.Visible = false;
                btnUpdate.Visible = false;
                btnAllPcsUpdate.Visible = false;
            }

            if (GlobalDec.gEmployeeProperty.role_name.ToUpper() == "SURAT POLISH REPARING")
            {
                if (GlobalDec.gEmployeeProperty.user_name.ToUpper() == "PRAFUL" || GlobalDec.gEmployeeProperty.user_name.ToUpper() == "KANAIYA")
                {
                    chkStockCaratUpdate.Visible = true;
                }
                else
                {
                    chkStockCaratUpdate.Visible = false;
                }
            }
            else
            {
                chkStockCaratUpdate.Visible = false;
            }
        }

        private void txtLotID_Validated(object sender, EventArgs e)
        {
            try
            {
                m_IsLot = 1;
                lueKapan.EditValue = null;
                lueCutNo.EditValue = null;
                MFGProcessReceive objProcessRecieve = new MFGProcessReceive();

                //if (Val.ToInt64(txtLotID.Text) != 0 && Val.ToString(lueKapan.Text) == "" && Val.ToString(lueCutNo.Text) == "")
                //{
                m_dtbParam = Global.GetRoughStock(Val.ToInt(0), Val.ToInt64(txtLotID.Text));
                int Cut_Id = Val.ToInt(m_dtbParam.Rows[0]["rough_cut_id"]);
                if (m_dtbParam.Rows.Count > 0)
                {
                    lueKapan.EditValue = Val.ToInt64(m_dtbParam.Rows[0]["kapan_id"]);

                    MFGProcessIssue objMFGProcessIssue = new MFGProcessIssue();
                    m_blnflag = true;
                    //lueKapan.Text = Val.ToString(m_dtbParam.Rows[0]["kapan_no"]);
                    lueCutNo.Properties.DataSource = m_dtbParam;
                    lueCutNo.Properties.ValueMember = "rough_cut_id";
                    lueCutNo.Properties.DisplayMember = "rough_cut_no";
                    lueCutNo.EditValue = Val.ToInt64(Cut_Id);

                    Btn_Show_Click(null, null);

                }
                else
                {
                    Global.Message("Lot Not Found");
                    txtLotID.Focus();
                    return;
                }
                //}

                m_IsLot = 0;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (DgvHistoryDetail.FocusedRowHandle < 0)
            {
                return;
            }

            tdt_ = new DataTable();
            tdt_ = (DataTable)MainDgvHistoryDetail.DataSource;
            Int64 max_val = 0;
            if (tdt_.Select("lot_id='" + txtLotID.Text + "' AND active='True'").Count() > 0)
            {
                max_val = Convert.ToInt32(tdt_.Select("lot_id='" + txtLotID.Text + "' AND active= 'True'").CopyToDataTable().Compute("max([history_id])", string.Empty));

            }
            else
            {
                max_val = 0;
            }
            DataTable DtHst = new DataTable();
            int LatestId = 0;
            int Active = chkActive.Checked == true ? 1 : 0;
            DtHst = objHistoryView.GetHistoryData(Val.ToInt(lueKapan.EditValue), Val.ToInt(lueCutNo.EditValue), Val.ToInt64(txtLotID.Text), Val.ToInt(Active));
            if (DtHst.Rows.Count > 0)
            {
                LatestId = Convert.ToInt32(DtHst.Select("lot_id='" + txtLotID.Text + "' AND active= 'True'").CopyToDataTable().Compute("max([history_id])", string.Empty));
            }
            else
            {
                LatestId = 0;
            }


            Int64 HST_ID = Val.ToInt64(DgvHistoryDetail.GetRowCellValue(DgvHistoryDetail.FocusedRowHandle, "history_id"));

            if (max_val != HST_ID || LatestId != HST_ID)
            {
                MessageBox.Show("This Transaction Can't Deleted.\n It is not a last Transaction.\n Please Select last Transaction to Delete.");
                return;
            }

            string Sub_Process = Val.ToString(DgvHistoryDetail.GetRowCellValue(DgvHistoryDetail.FocusedRowHandle, "sub_process_name"));
            Int32 History_type = Val.ToInt32(DgvHistoryDetail.GetRowCellValue(DgvHistoryDetail.FocusedRowHandle, "history_type"));

            if (Sub_Process == "4P-OK" && GlobalDec.gEmployeeProperty.role_name == "SURAT 4P" && History_type == 2)
            {
                Global.Message("This Transaction Can't Deleted.\n Please Contact Administrator.\n");
                return;
            }

            m_Dt = tdt_.Select("history_id =" + HST_ID + " AND lot_id='" + txtLotID.Text + "'").CopyToDataTable();

            if (Global.Confirm("Are you sure delete selected row?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                //GridView gridView = MainDgvHistoryDetail.FocusedView as GridView;
                //object row = gridView.GetRow(gridView.FocusedRowHandle);
                //int KapanId = Val.ToInt(Dt.Rows[0]["kapan_id"]);
                //int CutId = Val.ToInt(Dt.Rows[0]["cut_id"]);
                Int64 LotId = Val.ToInt64(m_Dt.Rows[0]["lot_id"]);
                int HistoryId = Val.ToInt(m_Dt.Rows[0]["history_id"]);
                int Res = objHistoryView.DeleteHistory(LotId, HistoryId);
                if (Res > 0)
                {
                    Global.Message("Delete Successful");
                    this.Cursor = Cursors.WaitCursor;
                    DataTable DtTab = new DataTable();
                    Active = chkActive.Checked == true ? 1 : 0;
                    DtTab = objHistoryView.GetHistoryData(Val.ToInt(lueKapan.EditValue), Val.ToInt(lueCutNo.EditValue), Val.ToInt64(txtLotID.Text), Val.ToInt(Active));
                    if (DtTab.Rows.Count > 0)
                    {
                        MainDgvHistoryDetail.DataSource = DtTab;
                        this.Cursor = Cursors.Default;
                        (MainDgvHistoryDetail.FocusedView as GridView).MoveLast();
                    }
                    else
                    {
                        Global.Message("Data Not Found");
                        MainDgvHistoryDetail.DataSource = null;
                        Btn_Show.Focus();
                        this.Cursor = Cursors.Default;
                    }
                }
                //DgvHistoryDetail.GetRowHandle(DgvHistoryDetail.FocusedRowHandle);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            m_updateIssueId = 0;
            if (DgvHistoryDetail.FocusedRowHandle < 0)
            {
                return;
            }
            DataTable Dt_StockData = new DataTable();
            Dt_StockData = objHistoryView.GetLotStockDetail(Val.ToInt32(lueCutNo.EditValue), Val.ToInt64(txtLotID.Text));
            tdt_ = new DataTable();
            tdt_ = (DataTable)MainDgvHistoryDetail.DataSource;

            Int64 HST_ID = Val.ToInt64(DgvHistoryDetail.GetRowCellValue(DgvHistoryDetail.FocusedRowHandle, "history_id"));

            m_Dt = tdt_.Select("history_id >=" + HST_ID + " AND lot_id='" + txtLotID.Text + "'").CopyToDataTable();
            if (Dt_StockData.Rows.Count > 0 && chkPrdUpdate.Checked == false && chkJangedUpdate.Checked == false)
            {
                if (Val.ToInt(Dt_StockData.Rows[0]["department_id"]) == Val.ToInt(m_Dt.Rows[0]["department_id"]))
                {
                    for (int i = 0; i < m_Dt.Rows.Count; i++)
                    {

                        if (Val.ToInt(m_Dt.Rows[i]["department_id"]) != Val.ToInt(Dt_StockData.Rows[0]["department_id"]))
                        {
                            Global.Message("Entry Not Update Because Lot Not In Your Department");
                            return;
                        }
                    }
                }
                else
                {
                    Global.Message("Entry Not Update Because Lot Not In Your Department");
                    return;
                }
            }
            if (Val.ToInt(m_Dt.Rows[0]["history_type"]) == 2 && Val.ToString(m_Dt.Rows[0]["sub_process_name"]) == "ASSORT FINAL" && chkPrdUpdate.Checked == false && chkJangedUpdate.Checked == false)
            {
                DataTable dt_Assort = objHistoryView.CheckFromLot(Val.ToInt64(txtLotID.Text), Val.ToInt(m_Dt.Rows[0]["history_id"]));
                if (Val.ToInt(dt_Assort.Rows[0]["department_id"]) != Val.ToInt(m_Dt.Rows[0]["department_id"]))
                {
                    Global.Message("Entry Not Update Because Lot Not In Your Department");
                    return;
                }
                else
                {
                    pnlAllPcsUpdate.Visible = true;
                    txtPcs.Text = Val.ToInt(m_Dt.Rows[0]["balance_pcs"]).ToString();
                    txtCarat.Text = Val.ToDecimal(m_Dt.Rows[0]["balance_carat"]).ToString();
                }
            }
            else if (Val.ToInt(m_Dt.Rows[0]["history_type"]) == 3 && chkStockCaratUpdate.Checked == true)
            {
                if (Val.ToString(m_Dt.Rows[0]["process_name"]) == "POLISH REPAIRING" && Val.ToString(m_Dt.Rows[0]["current_department"]) == "POLISH REPARING")
                {
                    pnlUpdateIsuRec.Visible = true;
                    pnlManagerUpdate.Visible = false;
                    pnlUpdate.Visible = false;
                    pnlMultiUpdate.Visible = false;
                    pnlPredictionUpdate.Visible = false;
                    txtReturnPcs.Enabled = false;
                    txtBreakPcs.Enabled = false;
                    txtBreakCarat.Enabled = false;
                    txtReSoingPcs.Enabled = false;
                    txtResoingCarat.Enabled = false;
                    txtRejPcs.Enabled = false;
                    txtRejCarat.Enabled = false;


                    lblOsPcs.Text = Val.ToString(m_Dt.Rows[0]["org_pcs"]);
                    lblOsCarat.Text = Val.ToString(m_Dt.Rows[0]["org_carat"]);
                    txtReturnCarat.Text = Val.ToString(m_Dt.Rows[0]["balance_carat"]);
                    txtLossCarat.Text = Val.ToString(m_Dt.Rows[0]["loss_carat"]);
                }
                else
                {
                    Global.Message("Entry Not Update Because Lot Not In Polish Repairing Department");
                    return;
                }

            }
            else if ((Val.ToInt(m_Dt.Rows[0]["history_type"]) == 3 || Val.ToInt(m_Dt.Rows[0]["history_type"]) == 6 || Val.ToInt(m_Dt.Rows[0]["history_type"]) == 4 || Val.ToInt(m_Dt.Rows[0]["history_type"]) == 9 || Val.ToInt(m_Dt.Rows[0]["history_type"]) == 5 || Val.ToInt(m_Dt.Rows[0]["history_type"]) == 10 || chkIsManager.Checked == true) && chkPrdUpdate.Checked == false && chkJangedUpdate.Checked == false)
            {
                pnlUpdateIsuRec.Visible = false;
                pnlManagerUpdate.Visible = false;
                pnlUpdate.Visible = true;
                pnlMultiUpdate.Visible = false;
                pnlPredictionUpdate.Visible = false;
            }
            else if (chkPrdUpdate.Checked == true)
            {
                if (Val.ToInt(m_Dt.Rows[0]["prediction_id"]) > 0)
                {
                    pnlUpdateIsuRec.Visible = false;
                    pnlUpdate.Visible = false;
                    pnlManagerUpdate.Visible = false;
                    pnlMultiUpdate.Visible = false;
                    pnlPredictionUpdate.Visible = true;
                    lueClarity.EditValue = Val.ToInt32(m_Dt.Rows[0]["rough_clarity_id"]);
                    lueQuality.EditValue = Val.ToInt64(m_Dt.Rows[0]["quality_id"]);
                    lueRoughSieve.EditValue = Val.ToInt32(m_Dt.Rows[0]["sieve_id"]);
                }
                else
                {
                    pnlUpdateIsuRec.Visible = false;
                    pnlUpdate.Visible = false;
                    pnlManagerUpdate.Visible = false;
                    pnlMultiUpdate.Visible = false;
                    pnlPredictionUpdate.Visible = false;
                }
            }
            else if (Val.ToInt(m_Dt.Rows[0]["history_type"]) == 2 && Val.ToString(m_Dt.Rows[0]["sub_process_name"]) != "ASSORT FINAL")
            {
                decimal balCarat = 0;
                if (Dt_StockData.Rows.Count > 0)
                {
                    balCarat = Val.ToDecimal(Dt_StockData.Rows[0]["balance_pcs"]);
                    if (balCarat == 0)
                    {
                        Global.Message("Entry Not Update Because Lot is Mixing Another lot");
                        return;
                    }
                }
                else
                {
                    Global.Message("Entry Not Update Please Contact Administrator");
                    return;
                }
                int Srno = 0;
                DataTable dt_MultiEntry = new DataTable();
                //DataTable dt_MultiEntry = objHistoryView.MultiUpdateCheck(Val.ToInt64(txtLotID.Text), Val.ToInt(m_Dt.Rows[0]["history_id"]));
                m_OldLossCarat = 0;
                m_OldLostPcs = 0;
                m_OldLostCarat = 0;
                m_OldRecPcs = 0;
                m_OldRecCarat = 0;
                m_OldRejPcs = 0;
                m_OldRejCarat = 0;
                //m_BalPcs = (m
                //m_BalCarat = 
                m_OldRRPcs = 0;
                m_OldRRCarat = 0;

                m_OldResawPcs = 0;
                m_OldResawCarat = 0;


                m_OldBreakPcs = 0;
                m_OldBreakCarat = 0;


                m_OldOrgPcs = 0;
                m_OldOrgCarat = 0;

                m_OldPlusCarat = 0;
                if (dt_MultiEntry.Rows.Count > 1 && chkJangedUpdate.Checked == false)
                {
                    GenerateProcessDetails();
                    for (int i = 0; i <= dt_MultiEntry.Rows.Count - 1; i++)
                    {
                        DataRow drwNew = m_dtbReceiveProcess.NewRow();
                        //int numLossPcs = Val.ToInt(txtLossPcs.Text);
                        m_OldLossCarat = m_OldLossCarat + Val.ToDecimal(dt_MultiEntry.Rows[i]["loss_carat"]);
                        m_OldLostPcs = m_OldLostPcs + Val.ToInt(dt_MultiEntry.Rows[i]["lost_pcs"]);
                        m_OldLostCarat = m_OldLostCarat + Val.ToDecimal(dt_MultiEntry.Rows[i]["lost_carat"]);
                        m_OldRecPcs = m_OldRecPcs + Val.ToInt(dt_MultiEntry.Rows[i]["pcs"]);
                        m_OldRecCarat = m_OldRecCarat + Val.ToDecimal(dt_MultiEntry.Rows[i]["carat"]);
                        m_OldRejPcs = m_OldRejPcs + Val.ToInt(dt_MultiEntry.Rows[i]["rejection_pcs"]);
                        m_OldRejCarat = m_OldRejCarat + Val.ToDecimal(dt_MultiEntry.Rows[i]["rejection_carat"]);
                        //m_BalPcs = (m_OsPcs - (numLostPcs + numReturnPcs));
                        //m_BalCarat = (m_OsCarat - (numLossCarat + numLostCarat + numReturnCarat));
                        m_OldRRPcs = m_OldRRPcs + Val.ToInt(dt_MultiEntry.Rows[i]["rr_pcs"]);
                        m_OldRRCarat = m_OldRRCarat + Val.ToDecimal(dt_MultiEntry.Rows[i]["rr_carat"]);

                        m_OldResawPcs = m_OldResawPcs + Val.ToInt(dt_MultiEntry.Rows[i]["resoing_pcs"]);
                        m_OldResawCarat = m_OldResawCarat + Val.ToDecimal(dt_MultiEntry.Rows[i]["resoing_carat"]);

                        m_OldBreakPcs = m_OldBreakPcs + Val.ToInt(dt_MultiEntry.Rows[i]["breakage_pcs"]);
                        m_OldBreakCarat = m_OldBreakCarat + Val.ToDecimal(dt_MultiEntry.Rows[i]["breakage_carat"]);

                        m_OldOrgPcs = Val.ToInt(dt_MultiEntry.Rows[0]["org_pcs"]);
                        m_OldOrgCarat = Val.ToDecimal(dt_MultiEntry.Rows[0]["org_carat"]);

                        m_OldPlusCarat = m_OldPlusCarat + Val.ToDecimal(dt_MultiEntry.Rows[i]["carat_plus"]);

                        drwNew["recieve_id"] = Val.ToInt(Val.ToString(dt_MultiEntry.Rows[i]["receive_id"]));
                        drwNew["recieve_date"] = Val.DBDate(Val.ToString(dt_MultiEntry.Rows[i]["receive_date"]));

                        drwNew["lot_id"] = Val.ToInt(dt_MultiEntry.Rows[i]["lot_id"]);

                        drwNew["issue_id"] = Val.ToInt(dt_MultiEntry.Rows[i]["issue_id"]);
                        drwNew["return_pcs"] = Val.ToInt(dt_MultiEntry.Rows[i]["pcs"]);
                        drwNew["return_carat"] = Val.ToDecimal(dt_MultiEntry.Rows[i]["carat"]);

                        //drwNew["loss_pcs"] = numLossPcs;
                        drwNew["loss_carat"] = Val.ToDecimal(dt_MultiEntry.Rows[i]["loss_carat"]);

                        drwNew["lost_pcs"] = Val.ToInt(dt_MultiEntry.Rows[i]["lost_pcs"]);
                        drwNew["lost_carat"] = Val.ToDecimal(dt_MultiEntry.Rows[i]["lost_carat"]);

                        drwNew["rejection_pcs"] = Val.ToInt(dt_MultiEntry.Rows[i]["rejection_pcs"]);
                        drwNew["rejection_carat"] = Val.ToDecimal(dt_MultiEntry.Rows[i]["rejection_carat"]);

                        drwNew["rr_pcs"] = Val.ToInt(dt_MultiEntry.Rows[i]["rr_pcs"]);
                        drwNew["rr_carat"] = Val.ToDecimal(dt_MultiEntry.Rows[i]["rr_carat"]);

                        drwNew["sr_no"] = Srno;

                        drwNew["resoing_pcs"] = Val.ToInt(dt_MultiEntry.Rows[i]["resoing_pcs"]);
                        drwNew["resoing_carat"] = Val.ToDecimal(dt_MultiEntry.Rows[i]["resoing_carat"]);

                        drwNew["breakage_pcs"] = Val.ToInt(dt_MultiEntry.Rows[i]["breakage_pcs"]);
                        drwNew["breakage_carat"] = Val.ToDecimal(dt_MultiEntry.Rows[i]["breakage_carat"]);

                        drwNew["org_pcs"] = Val.ToInt(dt_MultiEntry.Rows[i]["org_pcs"]);
                        drwNew["org_carat"] = Val.ToDecimal(dt_MultiEntry.Rows[i]["org_carat"]);

                        drwNew["carat_plus"] = Val.ToDecimal(dt_MultiEntry.Rows[i]["carat_plus"]);
                        drwNew["history_type"] = Val.ToDecimal(dt_MultiEntry.Rows[i]["history_type"]);
                        drwNew["history_id"] = Val.ToDecimal(dt_MultiEntry.Rows[i]["history_id"]);

                        m_dtbReceiveProcess.Rows.Add(drwNew);
                        Srno++;
                    }
                    pnlUpdateIsuRec.Visible = false;
                    pnlUpdate.Visible = false;
                    pnlManagerUpdate.Visible = false;
                    pnlMultiUpdate.Visible = true;
                    pnlPredictionUpdate.Visible = false;
                }
                else
                {
                    m_updateIssueId = Val.ToInt(m_Dt.Rows[0]["history_id"]);
                    pnlUpdateIsuRec.Visible = false;
                    pnlManagerUpdate.Visible = false;
                    if (GlobalDec.gEmployeeProperty.department_name == "4P" || GlobalDec.gEmployeeProperty.department_name == "XXX-4P")
                    {
                        //txtReturnPcs.Enabled = false;
                        txtOutpcs.Enabled = false;
                        //txtRejPcs.Enabled = false;
                        //txtReSoingPcs.Enabled = false;
                        //txtBreakPcs.Enabled = false;
                        txtLostPcs.Enabled = false;

                        txtReturnPcs.Enabled = true;
                        txtBreakPcs.Enabled = true;
                    }
                    else
                    {
                        txtReturnPcs.Enabled = true;
                        txtOutpcs.Enabled = true;
                        txtRejPcs.Enabled = true;
                        txtReSoingPcs.Enabled = true;
                        txtBreakPcs.Enabled = true;
                        txtLostPcs.Enabled = true;
                    }
                    lblOsPcs.Text = Val.ToString(m_Dt.Rows[0]["org_pcs"]);
                    lblOsCarat.Text = Val.ToString(m_Dt.Rows[0]["org_carat"]);
                    txtReturnPcs.Text = Val.ToString(m_Dt.Rows[0]["balance_pcs"]);
                    txtReturnCarat.Text = Val.ToString(m_Dt.Rows[0]["balance_carat"]);
                    txtOutpcs.Text = Val.ToString(m_Dt.Rows[0]["rr_pcs"]);
                    txtOutCarat.Text = Val.ToString(m_Dt.Rows[0]["rr_carat"]);
                    txtRejPcs.Text = Val.ToString(m_Dt.Rows[0]["rejection_pcs"]);
                    txtRejCarat.Text = Val.ToString(m_Dt.Rows[0]["rejection_carat"]);
                    txtReSoingPcs.Text = Val.ToString(m_Dt.Rows[0]["resoing_pcs"]);
                    txtResoingCarat.Text = Val.ToString(m_Dt.Rows[0]["resoing_carat"]);
                    txtBreakPcs.Text = Val.ToString(m_Dt.Rows[0]["breakage_pcs"]);
                    txtBreakCarat.Text = Val.ToString(m_Dt.Rows[0]["breakage_carat"]);
                    txtLostPcs.Text = Val.ToString(m_Dt.Rows[0]["lost_pcs"]);
                    txtLostCarat.Text = Val.ToString(m_Dt.Rows[0]["lost_carat"]);
                    txtLossCarat.Text = Val.ToString(m_Dt.Rows[0]["loss_carat"]);
                    txtCaratPlus.Text = Val.ToString(m_Dt.Rows[0]["carat_plus"]);

                    m_OldRecPcs = Val.ToInt(m_Dt.Rows[0]["balance_pcs"]);
                    m_OldRecCarat = Val.ToDecimal(m_Dt.Rows[0]["balance_carat"]);
                    m_OldRRPcs = Val.ToInt(m_Dt.Rows[0]["rr_pcs"]);
                    m_OldRRCarat = Val.ToDecimal(m_Dt.Rows[0]["rr_carat"]);
                    m_OldRejPcs = Val.ToInt(m_Dt.Rows[0]["rejection_pcs"]);
                    m_OldRejCarat = Val.ToDecimal(m_Dt.Rows[0]["rejection_carat"]);
                    m_OldResawPcs = Val.ToInt(m_Dt.Rows[0]["resoing_pcs"]);
                    m_OldResawCarat = Val.ToDecimal(m_Dt.Rows[0]["resoing_carat"]);
                    m_OldBreakPcs = Val.ToInt(m_Dt.Rows[0]["breakage_pcs"]);
                    m_OldBreakCarat = Val.ToDecimal(m_Dt.Rows[0]["breakage_carat"]);
                    m_OldLostPcs = Val.ToInt(m_Dt.Rows[0]["lost_pcs"]);
                    m_OldLostCarat = Val.ToDecimal(m_Dt.Rows[0]["lost_carat"]);
                    m_OldLossCarat = Val.ToDecimal(m_Dt.Rows[0]["loss_carat"]);
                    m_OldPlusCarat = Val.ToDecimal(m_Dt.Rows[0]["carat_plus"]);
                    pnlUpdateIsuRec.Visible = true;
                    pnlUpdate.Visible = false;
                    pnlManagerUpdate.Visible = false;
                    pnlPredictionUpdate.Visible = false;
                }
            }
            else
            {
                pnlUpdateIsuRec.Visible = false;
                pnlUpdate.Visible = false;
                pnlManagerUpdate.Visible = false;
                pnlMultiUpdate.Visible = false;
                pnlPredictionUpdate.Visible = false;
                return;
            }

            lueToDepartment.EditValue = Val.ToInt(m_Dt.Rows[0]["department_id"]);
            lueManager.EditValue = Val.ToInt64(m_Dt.Rows[0]["manager_id"]);
            //Int64 max_val = 0;
            //if (tdt_.Select("lot_id='" + txtLotID.Text + "' AND active='True'").Count() > 0)
            //{
            //    max_val = Convert.ToInt32(tdt_.Select("lot_id='" + txtLotID.Text + "' AND active= 'True'").CopyToDataTable().Compute("max([history_id])", string.Empty));
            //}
            //else
            //{
            //    max_val = 0;
            //}

            //if (max_val != HST_ID)
            //{
            //    MessageBox.Show("This Transaction Can't Deleted.\n It is not a last Transaction.\n Please Select last Transaction to Delete.");
            //    return;
            //}
            //DataTable Dt = tdt_.Select("history_id =" + HST_ID + " AND lot_id='" + txtLotID.Text + "'").CopyToDataTable();




        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            int Res = 0;
            if (Val.ToInt(m_Dt.Rows[0]["from_manager_id"]) == Val.ToInt(lueManager.EditValue) || Val.ToInt(m_Dt.Rows[0]["from_department_id"]) == Val.ToInt(lueToDepartment.EditValue))
            {
                //pnlUpdate.Visible = false;
                Global.Message("Not Update Manager/Department same as From Manager / From Department");
                return;
            }
            if (m_Dt.Rows.Count > 0)
            {
                if (Global.Confirm("Are you sure update selected row?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    pnlUpdate.Visible = false;
                    pnlManagerUpdate.Visible = false;
                    chkIsManager.Checked = false;
                    //GridView gridView = MainDgvHistoryDetail.FocusedView as GridView;
                    //object row = gridView.GetRow(gridView.FocusedRowHandle);
                    //int KapanId = Val.ToInt(Dt.Rows[0]["kapan_id"]);
                    //int CutId = Val.ToInt(Dt.Rows[0]["cut_id"]);
                    if (Val.ToInt(m_Dt.Rows[0]["manager_id"]) == Val.ToInt(lueManager.EditValue) && Val.ToInt(m_Dt.Rows[0]["department_id"]) == Val.ToInt(lueToDepartment.EditValue))
                    {
                        pnlUpdate.Visible = false;
                        pnlManagerUpdate.Visible = false;
                        return;
                    }

                    for (int i = 0; i < m_Dt.Rows.Count; i++)
                    {

                        if (i == 0 || Val.ToInt(m_Dt.Rows[i]["history_type"]) == 1 || Val.ToInt(m_Dt.Rows[i]["history_type"]) == 2)
                        {
                            Int64 LotId = Val.ToInt64(m_Dt.Rows[i]["lot_id"]);
                            int HistoryId = Val.ToInt(m_Dt.Rows[i]["history_id"]);
                            int DeptId = Val.ToInt(lueToDepartment.EditValue);
                            int MgrId = Val.ToInt(lueManager.EditValue);
                            Res = objHistoryView.UpdateHistory(LotId, HistoryId, DeptId, MgrId);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                if (Res > 0)
                {
                    Global.Message("Update Successful");
                    this.Cursor = Cursors.WaitCursor;
                    DataTable DtTab = new DataTable();
                    int Active = chkActive.Checked == true ? 1 : 0;
                    DtTab = objHistoryView.GetHistoryData(Val.ToInt(lueKapan.EditValue), Val.ToInt(lueCutNo.EditValue), Val.ToInt64(txtLotID.Text), Val.ToInt(Active));
                    if (DtTab.Rows.Count > 0)
                    {
                        MainDgvHistoryDetail.DataSource = DtTab;
                        this.Cursor = Cursors.Default;
                    }
                    else
                    {
                        Global.Message("Data Not Found");
                        MainDgvHistoryDetail.DataSource = null;
                        Btn_Show.Focus();
                        this.Cursor = Cursors.Default;
                    }
                }
                //DgvHistoryDetail.GetRowHandle(DgvHistoryDetail.FocusedRowHandle);
            }
            else
            {
                pnlUpdate.Visible = false;
                pnlManagerUpdate.Visible = false;
            }
        }

        private void DgvHistoryDetail_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (Val.ToBoolean(DgvHistoryDetail.GetRowCellValue(e.RowHandle, "active")) == false)
            {
                e.Appearance.BeginUpdate();
                e.Appearance.BackColor = Color.PaleVioletRed;
            }
            else
            {
                e.Appearance.BackColor = Color.Transparent;

            }

        }

        private void btnIsURecUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToString(m_Dt.Rows[0]["process_name"]) == "POLISH REPAIRING" && Val.ToString(m_Dt.Rows[0]["current_department"]) == "POLISH REPARING")
                {
                    int Res = 0;
                    decimal TotalCaratSumm = Val.ToDecimal(txtReturnCarat.Text) + Val.ToDecimal(txtOutCarat.Text) + Val.ToDecimal(txtRejCarat.Text) + Val.ToDecimal(txtResoingCarat.Text) + Val.ToDecimal(txtLostCarat.Text) + Val.ToDecimal(txtBreakCarat.Text) + Val.ToDecimal(txtLossCarat.Text);
                    if (Val.ToDecimal(m_Dt.Rows[0]["org_carat"]) + Val.ToDecimal(txtCaratPlus.Text) != TotalCaratSumm)
                    {
                        //pnlUpdate.Visible = false;
                        Global.Message("Carat not Match");
                        return;
                    }
                    if (m_Dt.Rows.Count > 0)
                    {
                        if (Global.Confirm("Are you sure update selected row?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            pnlUpdateIsuRec.Visible = false;
                            pnlManagerUpdate.Visible = false;

                            Int64 LotId = Val.ToInt64(txtLotID.Text);
                            decimal RecCarat = Val.ToDecimal(txtReturnCarat.Text);
                            decimal LossCarat = Val.ToDecimal(txtLossCarat.Text);
                            decimal PlusCarat = Val.ToDecimal(txtCaratPlus.Text);
                            Res = objHistoryView.UpdateStockCaratHistory(LotId, RecCarat, LossCarat, PlusCarat);
                        }
                        if (Res > 0)
                        {
                            Global.Message("Update Successful");
                            this.Cursor = Cursors.WaitCursor;
                            DataTable DtTab = new DataTable();
                            chkIsManager.Checked = false;
                            chkJangedUpdate.Checked = false;
                            chkStockCaratUpdate.Checked = false;
                            chkPrdUpdate.Checked = false;
                            int Active = chkActive.Checked == true ? 1 : 0;
                            DtTab = objHistoryView.GetHistoryData(Val.ToInt(lueKapan.EditValue), Val.ToInt(lueCutNo.EditValue), Val.ToInt64(txtLotID.Text), Val.ToInt(Active));
                            if (DtTab.Rows.Count > 0)
                            {
                                MainDgvHistoryDetail.DataSource = DtTab;
                                this.Cursor = Cursors.Default;
                            }
                            else
                            {
                                Global.Message("Data Not Found");
                                MainDgvHistoryDetail.DataSource = null;
                                Btn_Show.Focus();
                                this.Cursor = Cursors.Default;
                            }
                        }
                        //DgvHistoryDetail.GetRowHandle(DgvHistoryDetail.FocusedRowHandle);
                    }
                    else
                    {
                        pnlUpdateIsuRec.Visible = false;
                        pnlManagerUpdate.Visible = false;
                    }
                }
                else
                {
                    int Res = 0;
                    int p_lastEntryflag = 0;
                    int TotalPcsSumm = Val.ToInt(txtReturnPcs.Text) + Val.ToInt(txtOutpcs.Text) + Val.ToInt(txtRejPcs.Text) + Val.ToInt(txtReSoingPcs.Text) + Val.ToInt(txtLostPcs.Text) + Val.ToInt(txtBreakPcs.Text);
                    decimal TotalCaratSumm = Val.ToDecimal(txtReturnCarat.Text) + Val.ToDecimal(txtOutCarat.Text) + Val.ToDecimal(txtRejCarat.Text) + Val.ToDecimal(txtResoingCarat.Text) + Val.ToDecimal(txtLostCarat.Text) + Val.ToDecimal(txtBreakCarat.Text) + Val.ToDecimal(txtLossCarat.Text);
                    if (Val.ToDecimal(m_Dt.Rows[0]["org_carat"]) + Val.ToDecimal(txtCaratPlus.Text) != TotalCaratSumm)
                    {
                        //pnlUpdate.Visible = false;
                        Global.Message("Carat not Match");
                        return;
                    }
                    if (Val.ToInt(lblOsPcs.Text) != TotalPcsSumm)
                    {
                        //pnlUpdate.Visible = false;
                        Global.Message("Pcs not Match");
                        return;
                    }
                    if (m_Dt.Rows.Count > 0)
                    {
                        if (Global.Confirm("Are you sure update selected row?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            pnlUpdateIsuRec.Visible = false;
                            pnlManagerUpdate.Visible = false;
                            //GridView gridView = MainDgvHistoryDetail.FocusedView as GridView;
                            //object row = gridView.GetRow(gridView.FocusedRowHandle);
                            //int KapanId = Val.ToInt(Dt.Rows[0]["kapan_id"]);
                            //int CutId = Val.ToInt(Dt.Rows[0]["cut_id"]);
                            int count = 0;
                            if (chkJangedUpdate.Checked == false)
                            {
                                for (int i = 0; i < m_Dt.Rows.Count; i++)
                                {
                                    if (m_Dt.Rows.Count == 1)
                                        p_lastEntryflag = 1;
                                    if (m_Dt.Rows.Count - 1 == i)
                                        p_lastEntryflag = 1;

                                    if (i == 0 && Val.ToInt(m_Dt.Rows[i]["history_type"]) == 2 && p_lastEntryflag != 1)
                                    {
                                        Int64 LotId = Val.ToInt64(m_Dt.Rows[i]["lot_id"]);
                                        int HistoryId = Val.ToInt(m_Dt.Rows[i]["history_id"]);
                                        int RecPcs = Val.ToInt(txtReturnPcs.Text);
                                        decimal RecCarat = Val.ToDecimal(txtReturnCarat.Text);
                                        int RRPcs = Val.ToInt(txtOutpcs.Text);
                                        decimal RRCarat = Val.ToDecimal(txtOutCarat.Text);
                                        int RejPcs = Val.ToInt(txtRejPcs.Text);
                                        decimal RejCarat = Val.ToDecimal(txtRejCarat.Text);
                                        int ResawPcs = Val.ToInt(txtReSoingPcs.Text);
                                        decimal ResawCarat = Val.ToDecimal(txtResoingCarat.Text);
                                        int BreakPcs = Val.ToInt(txtBreakPcs.Text);
                                        decimal BreakCarat = Val.ToDecimal(txtBreakCarat.Text);
                                        int LostPcs = Val.ToInt(txtLostPcs.Text);
                                        decimal LostCarat = Val.ToDecimal(txtLostCarat.Text);
                                        decimal LossCarat = Val.ToDecimal(txtLossCarat.Text);
                                        decimal PlusCarat = Val.ToDecimal(txtCaratPlus.Text);
                                        Res = objHistoryView.UpdatePcsCaratHistory(LotId, HistoryId, RecPcs, RecCarat, RRPcs, RRCarat, RejPcs, RejCarat, ResawPcs, ResawCarat, BreakPcs, BreakCarat, LostPcs, LostCarat, LossCarat, PlusCarat, p_lastEntryflag, count);
                                    }
                                    if (i == 0 && Val.ToInt(m_Dt.Rows[i]["history_type"]) == 2 && p_lastEntryflag == 1)
                                    {
                                        Int64 LotId = Val.ToInt64(m_Dt.Rows[i]["lot_id"]);
                                        int HistoryId = Val.ToInt(m_Dt.Rows[i]["history_id"]);
                                        int RecPcs = Val.ToInt(txtReturnPcs.Text);
                                        decimal RecCarat = Val.ToDecimal(txtReturnCarat.Text);
                                        int RRPcs = Val.ToInt(txtOutpcs.Text);
                                        decimal RRCarat = Val.ToDecimal(txtOutCarat.Text);
                                        int RejPcs = Val.ToInt(txtRejPcs.Text);
                                        decimal RejCarat = Val.ToDecimal(txtRejCarat.Text);
                                        int ResawPcs = Val.ToInt(txtReSoingPcs.Text);
                                        decimal ResawCarat = Val.ToDecimal(txtResoingCarat.Text);
                                        int BreakPcs = Val.ToInt(txtBreakPcs.Text);
                                        decimal BreakCarat = Val.ToDecimal(txtBreakCarat.Text);
                                        int LostPcs = Val.ToInt(txtLostPcs.Text);
                                        decimal LostCarat = Val.ToDecimal(txtLostCarat.Text);
                                        decimal LossCarat = Val.ToDecimal(txtLossCarat.Text);
                                        decimal PlusCarat = Val.ToDecimal(txtCaratPlus.Text);
                                        decimal diffLossCarat = Val.ToDecimal(txtLossCarat.Text) - m_OldLossCarat;
                                        decimal diffRecCarat = Val.ToDecimal(txtReturnCarat.Text) - m_OldRecCarat;
                                        Res = objHistoryView.UpdatePcsCaratHistory(LotId, HistoryId, RecPcs, RecCarat, RRPcs, RRCarat, RejPcs, RejCarat, ResawPcs, ResawCarat, BreakPcs, BreakCarat, LostPcs, LostCarat, LossCarat, PlusCarat, p_lastEntryflag, count, diffLossCarat, diffRecCarat);
                                    }
                                    if (Val.ToInt(m_Dt.Rows[i]["history_type"]) == 1)
                                    {
                                        Int64 LotId = Val.ToInt64(m_Dt.Rows[i]["lot_id"]);
                                        int HistoryId = Val.ToInt(m_Dt.Rows[i]["history_id"]);
                                        int RecPcs = Val.ToInt(txtReturnPcs.Text) - m_OldRecPcs;
                                        decimal RecCarat = Val.ToDecimal(txtReturnCarat.Text) - m_OldRecCarat;
                                        int RRPcs = Val.ToInt(txtOutpcs.Text) - m_OldRRPcs;
                                        decimal RRCarat = Val.ToDecimal(txtOutCarat.Text) - m_OldRRCarat;
                                        int RejPcs = Val.ToInt(txtRejPcs.Text) - m_OldRejPcs;
                                        decimal RejCarat = Val.ToDecimal(txtRejCarat.Text) - m_OldRejCarat;
                                        int ResawPcs = Val.ToInt(txtReSoingPcs.Text) - m_OldResawPcs;
                                        decimal ResawCarat = Val.ToDecimal(txtResoingCarat.Text) - m_OldResawCarat;
                                        int BreakPcs = Val.ToInt(txtBreakPcs.Text) - m_OldBreakPcs;
                                        decimal BreakCarat = Val.ToDecimal(txtBreakCarat.Text) - m_OldBreakCarat;
                                        int LostPcs = Val.ToInt(txtLostPcs.Text) - m_OldLostPcs;
                                        decimal LostCarat = Val.ToDecimal(txtLostCarat.Text) - m_OldLostCarat;
                                        decimal LossCarat = Val.ToDecimal(txtLossCarat.Text) - m_OldLossCarat;
                                        decimal PlusCarat = Val.ToDecimal(txtCaratPlus.Text) - m_OldPlusCarat;
                                        Res = objHistoryView.UpdatePcsCaratHistory(LotId, HistoryId, RecPcs, RecCarat, RRPcs, RRCarat, RejPcs, RejCarat, ResawPcs, ResawCarat, BreakPcs, BreakCarat, LostPcs, LostCarat, LossCarat, PlusCarat, p_lastEntryflag, count);
                                    }
                                    if (i > 0 && Val.ToInt(m_Dt.Rows[i]["history_type"]) == 2)
                                    {
                                        Int64 LotId = Val.ToInt64(m_Dt.Rows[i]["lot_id"]);
                                        int HistoryId = Val.ToInt(m_Dt.Rows[i]["history_id"]);
                                        int RecPcs = Val.ToInt(txtReturnPcs.Text) - m_OldRecPcs;
                                        decimal RecCarat = Val.ToDecimal(txtReturnCarat.Text) - m_OldRecCarat;
                                        int RRPcs = Val.ToInt(txtOutpcs.Text) - m_OldRRPcs;
                                        decimal RRCarat = Val.ToDecimal(txtOutCarat.Text) - m_OldRRCarat;
                                        int RejPcs = Val.ToInt(txtRejPcs.Text) - m_OldRejPcs;
                                        decimal RejCarat = Val.ToDecimal(txtRejCarat.Text) - m_OldRejCarat;
                                        int ResawPcs = Val.ToInt(txtReSoingPcs.Text) - m_OldResawPcs;
                                        decimal ResawCarat = Val.ToDecimal(txtResoingCarat.Text) - m_OldResawCarat;
                                        int BreakPcs = Val.ToInt(txtBreakPcs.Text) - m_OldBreakPcs;
                                        decimal BreakCarat = Val.ToDecimal(txtBreakCarat.Text) - m_OldBreakCarat;
                                        int LostPcs = Val.ToInt(txtLostPcs.Text) - m_OldLostPcs;
                                        decimal LostCarat = Val.ToDecimal(txtLostCarat.Text) - m_OldLostCarat;
                                        decimal LossCarat = Val.ToDecimal(txtLossCarat.Text) - m_OldLossCarat;
                                        decimal PlusCarat = Val.ToDecimal(txtCaratPlus.Text) - m_OldPlusCarat;
                                        count = 1;
                                        Res = objHistoryView.UpdatePcsCaratHistory(LotId, HistoryId, RecPcs, RecCarat, RRPcs, RRCarat, RejPcs, RejCarat, ResawPcs, ResawCarat, BreakPcs, BreakCarat, LostPcs, LostCarat, LossCarat, PlusCarat, p_lastEntryflag, count);
                                    }
                                    if (Val.ToInt(m_Dt.Rows[i]["history_type"]) == 3 || Val.ToInt(m_Dt.Rows[i]["history_type"]) == 6)
                                    {
                                        Int64 LotId = Val.ToInt64(m_Dt.Rows[i]["lot_id"]);
                                        int HistoryId = Val.ToInt(m_Dt.Rows[i]["history_id"]);
                                        int RecPcs = Val.ToInt(txtReturnPcs.Text) - m_OldRecPcs;
                                        decimal RecCarat = Val.ToDecimal(txtReturnCarat.Text) - m_OldRecCarat;
                                        int RRPcs = Val.ToInt(txtOutpcs.Text) - m_OldRRPcs;
                                        decimal RRCarat = Val.ToDecimal(txtOutCarat.Text) - m_OldRRCarat;
                                        int RejPcs = Val.ToInt(txtRejPcs.Text) - m_OldRejPcs;
                                        decimal RejCarat = Val.ToDecimal(txtRejCarat.Text) - m_OldRejCarat;
                                        int ResawPcs = Val.ToInt(txtReSoingPcs.Text) - m_OldResawPcs;
                                        decimal ResawCarat = Val.ToDecimal(txtResoingCarat.Text) - m_OldResawCarat;
                                        int BreakPcs = Val.ToInt(txtBreakPcs.Text) - m_OldBreakPcs;
                                        decimal BreakCarat = Val.ToDecimal(txtBreakCarat.Text) - m_OldBreakCarat;
                                        int LostPcs = Val.ToInt(txtLostPcs.Text) - m_OldLostPcs;
                                        decimal LostCarat = Val.ToDecimal(txtLostCarat.Text) - m_OldLostCarat;
                                        decimal LossCarat = Val.ToInt(txtLossCarat.Text) - m_OldLossCarat;
                                        decimal PlusCarat = Val.ToDecimal(txtCaratPlus.Text) - m_OldPlusCarat;
                                        Res = objHistoryView.UpdatePcsCaratHistory(LotId, HistoryId, RecPcs, RecCarat, RRPcs, RRCarat, RejPcs, RejCarat, ResawPcs, ResawCarat, BreakPcs, BreakCarat, LostPcs, LostCarat, LossCarat, PlusCarat, p_lastEntryflag, count);
                                    }
                                    if (Val.ToInt(m_Dt.Rows[i]["history_type"]) != 1 && Val.ToInt(m_Dt.Rows[i]["history_type"]) != 2 && Val.ToInt(m_Dt.Rows[i]["history_type"]) != 3 && Val.ToInt(m_Dt.Rows[i]["history_type"]) != 6)
                                    {
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                Int64 LotId = Val.ToInt64(m_Dt.Rows[0]["lot_id"]);
                                int HistoryId = Val.ToInt(m_Dt.Rows[0]["history_id"]);
                                int RecPcs = Val.ToInt(txtReturnPcs.Text);
                                decimal RecCarat = Val.ToDecimal(txtReturnCarat.Text);
                                int RRPcs = Val.ToInt(txtOutpcs.Text);
                                decimal RRCarat = Val.ToDecimal(txtOutCarat.Text);
                                int RejPcs = Val.ToInt(txtRejPcs.Text);
                                decimal RejCarat = Val.ToDecimal(txtRejCarat.Text);
                                int ResawPcs = Val.ToInt(txtReSoingPcs.Text);
                                decimal ResawCarat = Val.ToDecimal(txtResoingCarat.Text);
                                int BreakPcs = Val.ToInt(txtBreakPcs.Text);
                                decimal BreakCarat = Val.ToDecimal(txtBreakCarat.Text);
                                int LostPcs = Val.ToInt(txtLostPcs.Text);
                                decimal LostCarat = Val.ToDecimal(txtLostCarat.Text);
                                decimal LossCarat = Val.ToDecimal(txtLossCarat.Text);
                                decimal PlusCarat = Val.ToDecimal(txtCaratPlus.Text);
                                decimal diffLossCarat = Val.ToDecimal(txtLossCarat.Text) - m_OldLossCarat;
                                decimal diffRecCarat = Val.ToDecimal(txtReturnCarat.Text) - m_OldRecCarat;
                                Res = objHistoryView.UpdatePcsCaratHistory(LotId, HistoryId, RecPcs, RecCarat, RRPcs, RRCarat, RejPcs, RejCarat, ResawPcs, ResawCarat, BreakPcs, BreakCarat, LostPcs, LostCarat, LossCarat, PlusCarat, 20, count, diffLossCarat, diffRecCarat);
                            }
                        }
                        if (Res > 0)
                        {
                            Global.Message("Update Successful");
                            this.Cursor = Cursors.WaitCursor;
                            DataTable DtTab = new DataTable();
                            chkIsManager.Checked = false;
                            chkJangedUpdate.Checked = false;
                            chkPrdUpdate.Checked = false;
                            int Active = chkActive.Checked == true ? 1 : 0;
                            DtTab = objHistoryView.GetHistoryData(Val.ToInt(lueKapan.EditValue), Val.ToInt(lueCutNo.EditValue), Val.ToInt64(txtLotID.Text), Val.ToInt(Active));
                            if (DtTab.Rows.Count > 0)
                            {
                                MainDgvHistoryDetail.DataSource = DtTab;
                                this.Cursor = Cursors.Default;
                            }
                            else
                            {
                                Global.Message("Data Not Found");
                                MainDgvHistoryDetail.DataSource = null;
                                Btn_Show.Focus();
                                this.Cursor = Cursors.Default;
                            }
                        }
                        //DgvHistoryDetail.GetRowHandle(DgvHistoryDetail.FocusedRowHandle);
                    }
                    else
                    {
                        pnlUpdateIsuRec.Visible = false;
                        pnlManagerUpdate.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }

        private void btnClosePnl_Click(object sender, EventArgs e)
        {
            pnlUpdate.Visible = false;
            pnlManagerUpdate.Visible = false;
            lueToDepartment.EditValue = null;
            lueProcess.EditValue = null;
            lueManager.EditValue = null;
        }

        private void btnPnlPcsClose_Click(object sender, EventArgs e)
        {
            pnlUpdateIsuRec.Visible = false;
            pnlManagerUpdate.Visible = false;
            txtReturnPcs.Text = "";
            txtReturnCarat.Text = "";
            txtOutpcs.Text = "";
            txtOutCarat.Text = "";
            txtRejPcs.Text = "";
            txtRejCarat.Text = "";
            txtReSoingPcs.Text = "";
            txtResoingCarat.Text = "";
            txtBreakPcs.Text = "";
            txtBreakCarat.Text = "";
            txtLostPcs.Text = "";
            txtLostCarat.Text = "";
            txtLossCarat.Text = "";
            txtCaratPlus.Text = "";
        }
        private bool GenerateProcessDetails()
        {
            bool blnReturn = true;
            try
            {
                if (m_dtbReceiveProcess.Rows.Count > 0)
                    m_dtbReceiveProcess.Rows.Clear();

                m_dtbReceiveProcess = new DataTable();

                m_dtbReceiveProcess.Columns.Add("recieve_id", typeof(int));
                m_dtbReceiveProcess.Columns.Add("recieve_date", typeof(DateTime));
                m_dtbReceiveProcess.Columns.Add("lot_id", typeof(int));
                m_dtbReceiveProcess.Columns.Add("kapan_id", typeof(int));
                m_dtbReceiveProcess.Columns.Add("cut_id", typeof(int));
                m_dtbReceiveProcess.Columns.Add("cut_no", typeof(string));
                //m_dtbReceiveProcess.Columns.Add("manager", typeof(string));
                m_dtbReceiveProcess.Columns.Add("manager_id", typeof(int));

                m_dtbReceiveProcess.Columns.Add("employee", typeof(string));
                m_dtbReceiveProcess.Columns.Add("employee_id", typeof(int));

                m_dtbReceiveProcess.Columns.Add("process", typeof(string));
                m_dtbReceiveProcess.Columns.Add("process_id", typeof(int));
                m_dtbReceiveProcess.Columns.Add("machine", typeof(string));
                m_dtbReceiveProcess.Columns.Add("machine_id", typeof(int));

                m_dtbReceiveProcess.Columns.Add("subprocess", typeof(string));
                m_dtbReceiveProcess.Columns.Add("sub_process_id", typeof(int));
                m_dtbReceiveProcess.Columns.Add("return_pcs", typeof(int)).DefaultValue = 0;
                m_dtbReceiveProcess.Columns.Add("return_carat", typeof(decimal)).DefaultValue = 0;
                m_dtbReceiveProcess.Columns.Add("loss_pcs", typeof(int)).DefaultValue = 0;
                m_dtbReceiveProcess.Columns.Add("loss_carat", typeof(decimal)).DefaultValue = 0;
                m_dtbReceiveProcess.Columns.Add("lost_pcs", typeof(int)).DefaultValue = 0;
                m_dtbReceiveProcess.Columns.Add("lost_carat", typeof(decimal)).DefaultValue = 0;
                m_dtbReceiveProcess.Columns.Add("rejection_pcs", typeof(int)).DefaultValue = 0;
                m_dtbReceiveProcess.Columns.Add("rejection_carat", typeof(decimal)).DefaultValue = 0;
                m_dtbReceiveProcess.Columns.Add("rr_pcs", typeof(int)).DefaultValue = 0;
                m_dtbReceiveProcess.Columns.Add("rr_carat", typeof(decimal)).DefaultValue = 0;
                m_dtbReceiveProcess.Columns.Add("sr_no", typeof(decimal)).DefaultValue = 1;
                m_dtbReceiveProcess.Columns.Add("issue_id", typeof(int)).DefaultValue = 0;
                m_dtbReceiveProcess.Columns.Add("balance", typeof(decimal)).DefaultValue = 0;

                m_dtbReceiveProcess.Columns.Add("resoing_pcs", typeof(int)).DefaultValue = 0;
                m_dtbReceiveProcess.Columns.Add("resoing_carat", typeof(decimal)).DefaultValue = 0;

                m_dtbReceiveProcess.Columns.Add("breakage_pcs", typeof(int)).DefaultValue = 0;
                m_dtbReceiveProcess.Columns.Add("breakage_carat", typeof(decimal)).DefaultValue = 0;

                m_dtbReceiveProcess.Columns.Add("org_pcs", typeof(int)).DefaultValue = 0;
                m_dtbReceiveProcess.Columns.Add("org_carat", typeof(decimal)).DefaultValue = 0;

                m_dtbReceiveProcess.Columns.Add("carat_plus", typeof(decimal)).DefaultValue = 0;
                m_dtbReceiveProcess.Columns.Add("final_weight", typeof(decimal)).DefaultValue = 0;
                m_dtbReceiveProcess.Columns.Add("history_type", typeof(string));
                m_dtbReceiveProcess.Columns.Add("history_id", typeof(int)).DefaultValue = 0;

                grdMultiEmployeeReturn.DataSource = m_dtbReceiveProcess;
                grdMultiEmployeeReturn.Refresh();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
        }

        private void btnPnlMutiUpdateClose_Click(object sender, EventArgs e)
        {
            pnlMultiUpdate.Visible = false;
        }

        private void dgvMultiEmployeeReturn_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;
            if (view == null) return;
            if (e.Column.FieldName == "return_pcs")
            {
                string cellValue = Val.ToString(Math.Round(Val.ToDecimal(Val.ToInt(Val.ToInt(e.Value)) / (Val.ToInt(m_OldOrgPcs) / Val.ToDecimal(m_OldOrgCarat))), 3));
                view.SetRowCellValue(e.RowHandle, view.Columns["return_carat"], cellValue);
            }
            if (e.Column.FieldName == "rr_pcs")
            {
                string cellValue = Val.ToString(Math.Round(Val.ToDecimal(Val.ToInt(Val.ToInt(e.Value)) / (Val.ToInt(m_OldOrgPcs) / Val.ToDecimal(m_OldOrgCarat))), 3));
                view.SetRowCellValue(e.RowHandle, view.Columns["rr_carat"], cellValue);
            }
            if (e.Column.FieldName == "rejection_pcs")
            {
                string cellValue = Val.ToString(Math.Round(Val.ToDecimal(Val.ToInt(Val.ToInt(e.Value)) / (Val.ToInt(m_OldOrgPcs) / Val.ToDecimal(m_OldOrgCarat))), 3));
                view.SetRowCellValue(e.RowHandle, view.Columns["rejection_carat"], cellValue);
            }
            if (e.Column.FieldName == "resoing_pcs")
            {
                string cellValue = Val.ToString(Math.Round(Val.ToDecimal(Val.ToInt(Val.ToInt(e.Value)) / (Val.ToInt(m_OldOrgPcs) / Val.ToDecimal(m_OldOrgCarat))), 3));
                view.SetRowCellValue(e.RowHandle, view.Columns["resoing_carat"], cellValue);
            }
            if (e.Column.FieldName == "breakage_pcs")
            {
                string cellValue = Val.ToString(Math.Round(Val.ToDecimal(Val.ToInt(Val.ToInt(e.Value)) / (Val.ToInt(m_OldOrgPcs) / Val.ToDecimal(m_OldOrgCarat))), 3));
                view.SetRowCellValue(e.RowHandle, view.Columns["breakage_carat"], cellValue);
            }
        }

        private void btnMultiUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int Res = 0;
                int p_lastEntryflag = 0;
                int TotalPcsSumm = Val.ToInt(clmReturnPcsUpdate.SummaryItem.SummaryValue) + Val.ToInt(clmRRPcsUpdate.SummaryItem.SummaryValue) + Val.ToInt(clmRejectionPcsUpdate.SummaryItem.SummaryValue) + Val.ToInt(clmResoPcsUpdate.SummaryItem.SummaryValue) + Val.ToInt(clmBreakagePcsUpdate.SummaryItem.SummaryValue) + Val.ToInt(clmLostPcsUpdate.SummaryItem.SummaryValue);
                decimal TotalCaratSumm = Val.ToDecimal(clmReturnCaratUpdate.SummaryItem.SummaryValue) + Val.ToDecimal(clmRRCaratUpdate.SummaryItem.SummaryValue) + Val.ToDecimal(clmRejectionCaratUpdate.SummaryItem.SummaryValue) + Val.ToDecimal(clmResoCaratUpdate.SummaryItem.SummaryValue) + Val.ToDecimal(clmBreakageCaratUpdate.SummaryItem.SummaryValue) + Val.ToDecimal(clmLostCaratUpdate.SummaryItem.SummaryValue) + Val.ToDecimal(clmLossCaratUpdate.SummaryItem.SummaryValue);

                if (Val.ToDecimal(m_OldOrgCarat) + Val.ToDecimal(clmCaratPlusUpdate.SummaryItem.SummaryValue) != TotalCaratSumm)
                {
                    //pnlUpdate.Visible = false;
                    Global.Message("Carat not Match");
                    return;
                }
                if (Val.ToInt(m_OldOrgPcs) != TotalPcsSumm)
                {
                    //pnlUpdate.Visible = false;
                    Global.Message("Pcs not Match");
                    return;
                }
                if (m_Dt.Rows.Count > 0)
                {
                    if (Global.Confirm("Are you sure update selected row?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        pnlUpdateIsuRec.Visible = false;
                        pnlManagerUpdate.Visible = false;
                        //GridView gridView = MainDgvHistoryDetail.FocusedView as GridView;
                        //object row = gridView.GetRow(gridView.FocusedRowHandle);
                        //int KapanId = Val.ToInt(Dt.Rows[0]["kapan_id"]);
                        //int CutId = Val.ToInt(Dt.Rows[0]["cut_id"]);
                        int count = 0;
                        for (int i = 0; i < m_dtbReceiveProcess.Rows.Count; i++)
                        {
                            if (m_dtbReceiveProcess.Rows.Count == 1)
                                p_lastEntryflag = 1;
                            if (m_dtbReceiveProcess.Rows.Count - 1 == i)
                                p_lastEntryflag = 1;

                            if (Val.ToInt(m_dtbReceiveProcess.Rows[i]["history_type"]) == 2)
                            {
                                Int64 LotId = Val.ToInt64(m_dtbReceiveProcess.Rows[i]["lot_id"]);
                                int HistoryId = Val.ToInt(m_dtbReceiveProcess.Rows[i]["history_id"]);
                                int RecPcs = Val.ToInt(m_dtbReceiveProcess.Rows[i]["return_pcs"]);
                                decimal RecCarat = Val.ToDecimal(m_dtbReceiveProcess.Rows[i]["return_carat"]);
                                int RRPcs = Val.ToInt(m_dtbReceiveProcess.Rows[i]["rr_pcs"]);
                                decimal RRCarat = Val.ToDecimal(m_dtbReceiveProcess.Rows[i]["rr_carat"]);
                                int RejPcs = Val.ToInt(m_dtbReceiveProcess.Rows[i]["rejection_pcs"]);
                                decimal RejCarat = Val.ToDecimal(m_dtbReceiveProcess.Rows[i]["rejection_carat"]);
                                int ResawPcs = Val.ToInt(m_dtbReceiveProcess.Rows[i]["resoing_pcs"]);
                                decimal ResawCarat = Val.ToDecimal(m_dtbReceiveProcess.Rows[i]["resoing_Carat"]);
                                int BreakPcs = Val.ToInt(m_dtbReceiveProcess.Rows[i]["breakage_pcs"]);
                                decimal BreakCarat = Val.ToDecimal(m_dtbReceiveProcess.Rows[i]["breakage_carat"]);
                                int LostPcs = Val.ToInt(m_dtbReceiveProcess.Rows[i]["lost_pcs"]);
                                decimal LostCarat = Val.ToDecimal(m_dtbReceiveProcess.Rows[i]["lost_carat"]);
                                decimal LossCarat = Val.ToDecimal(m_dtbReceiveProcess.Rows[i]["loss_carat"]);
                                decimal PlusCarat = Val.ToDecimal(m_dtbReceiveProcess.Rows[i]["carat_plus"]);
                                int DiffRecPcs = Val.ToInt(clmReturnPcsUpdate.SummaryItem.SummaryValue) - Val.ToInt(m_OldRecPcs);
                                decimal DiffRecCarat = Val.ToDecimal(clmReturnCaratUpdate.SummaryItem.SummaryValue) - Val.ToDecimal(m_OldRecCarat);
                                int DiffRRPcs = Val.ToInt(clmRRPcsUpdate.SummaryItem.SummaryValue) - Val.ToInt(m_OldRRPcs);
                                decimal DiffRRCarat = Val.ToDecimal(clmRRCaratUpdate.SummaryItem.SummaryValue) - Val.ToDecimal(m_OldRRCarat);
                                int DiffRejPcs = Val.ToInt(clmRejectionPcsUpdate.SummaryItem.SummaryValue) - Val.ToInt(m_OldRejPcs);
                                decimal DiffRejCarat = Val.ToDecimal(clmRejectionCaratUpdate.SummaryItem.SummaryValue) - Val.ToDecimal(m_OldRejCarat);
                                int DiffResawPcs = Val.ToInt(clmResoPcsUpdate.SummaryItem.SummaryValue) - Val.ToInt(m_OldResawPcs);
                                decimal DiffResawCarat = Val.ToDecimal(clmResoCaratUpdate.SummaryItem.SummaryValue) - Val.ToDecimal(m_OldResawCarat);
                                int DiffBreakPcs = Val.ToInt(clmBreakagePcsUpdate.SummaryItem.SummaryValue) - Val.ToInt(m_OldBreakPcs);
                                decimal DiffBreakCarat = Val.ToDecimal(clmBreakageCaratUpdate.SummaryItem.SummaryValue) - Val.ToDecimal(m_OldBreakCarat);
                                int DiffLostPcs = Val.ToInt(clmLostPcsUpdate.SummaryItem.SummaryValue) - Val.ToInt(m_OldLostPcs);
                                decimal DiffLostCarat = Val.ToDecimal(clmLostCaratUpdate.SummaryItem.SummaryValue) - Val.ToDecimal(m_OldLostCarat);
                                decimal DiffLossCarat = Val.ToDecimal(clmLossCaratUpdate.SummaryItem.SummaryValue) - Val.ToDecimal(m_OldLostCarat);
                                decimal DiffPlusCarat = Val.ToDecimal(clmCaratPlusUpdate.SummaryItem.SummaryValue) - Val.ToDecimal(m_OldPlusCarat);
                                Res = objHistoryView.UpdateMultiPcsCaratHistory(LotId, HistoryId, RecPcs, RecCarat, RRPcs, RRCarat, RejPcs, RejCarat, ResawPcs, ResawCarat, BreakPcs, BreakCarat, LostPcs, LostCarat, LossCarat, PlusCarat, DiffRecPcs, DiffRecCarat, DiffRRPcs, DiffRRCarat, DiffRejPcs, DiffRejCarat, DiffResawPcs, DiffResawCarat, DiffBreakPcs, DiffBreakCarat, DiffLostPcs, DiffLostCarat, DiffLossCarat, DiffPlusCarat, p_lastEntryflag, count);
                            }
                        }
                        //for (int i = 0; i < m_Dt.Rows.Count; i++)
                        //{
                        //    if (m_Dt.Rows.Count == 1)
                        //        p_lastEntryflag = 1;
                        //    if (m_Dt.Rows.Count - 1 == i)
                        //        p_lastEntryflag = 1;

                        //    if (i == 0 && Val.ToInt(m_Dt.Rows[i]["history_type"]) == 2)
                        //    {
                        //        Int64 LotId = Val.ToInt64(m_Dt.Rows[i]["lot_id"]);
                        //        int HistoryId = Val.ToInt(m_Dt.Rows[i]["history_id"]);
                        //        int RecPcs = Val.ToInt(txtReturnPcs.Text);
                        //        decimal RecCarat = Val.ToDecimal(txtReturnCarat.Text);
                        //        int RRPcs = Val.ToInt(txtOutpcs.Text);
                        //        decimal RRCarat = Val.ToDecimal(txtOutCarat.Text);
                        //        int RejPcs = Val.ToInt(txtRejPcs.Text);
                        //        decimal RejCarat = Val.ToDecimal(txtRejCarat.Text);
                        //        int ResawPcs = Val.ToInt(txtReSoingPcs.Text);
                        //        decimal ResawCarat = Val.ToDecimal(txtResoingCarat.Text);
                        //        int BreakPcs = Val.ToInt(txtBreakPcs.Text);
                        //        decimal BreakCarat = Val.ToDecimal(txtBreakCarat.Text);
                        //        int LostPcs = Val.ToInt(txtLostPcs.Text);
                        //        decimal LostCarat = Val.ToDecimal(txtLostCarat.Text);
                        //        decimal LossCarat = Val.ToDecimal(txtLossCarat.Text);
                        //        decimal PlusCarat = Val.ToDecimal(txtCaratPlus.Text);
                        //        Res = objHistoryView.UpdatePcsCaratHistory(LotId, HistoryId, RecPcs, RecCarat, RRPcs, RRCarat, RejPcs, RejCarat, ResawPcs, ResawCarat, BreakPcs, BreakCarat, LostPcs, LostCarat, LossCarat, PlusCarat, p_lastEntryflag, count);
                        //    }
                        //    if (Val.ToInt(m_Dt.Rows[i]["history_type"]) == 1)
                        //    {
                        //        Int64 LotId = Val.ToInt64(m_Dt.Rows[i]["lot_id"]);
                        //        int HistoryId = Val.ToInt(m_Dt.Rows[i]["history_id"]);
                        //        int RecPcs = Val.ToInt(txtReturnPcs.Text) - m_OldRecPcs;
                        //        decimal RecCarat = Val.ToDecimal(txtReturnCarat.Text) - m_OldRecCarat;
                        //        int RRPcs = Val.ToInt(txtOutpcs.Text) - m_OldRRPcs;
                        //        decimal RRCarat = Val.ToDecimal(txtOutCarat.Text) - m_OldRRCarat;
                        //        int RejPcs = Val.ToInt(txtRejPcs.Text) - m_OldRejPcs;
                        //        decimal RejCarat = Val.ToDecimal(txtRejCarat.Text) - m_OldRejCarat;
                        //        int ResawPcs = Val.ToInt(txtReSoingPcs.Text) - m_OldResawPcs;
                        //        decimal ResawCarat = Val.ToDecimal(txtResoingCarat.Text) - m_OldResawCarat;
                        //        int BreakPcs = Val.ToInt(txtBreakPcs.Text) - m_OldBreakPcs;
                        //        decimal BreakCarat = Val.ToDecimal(txtBreakCarat.Text) - m_OldBreakCarat;
                        //        int LostPcs = Val.ToInt(txtLostPcs.Text) - m_OldLostPcs;
                        //        decimal LostCarat = Val.ToDecimal(txtLostCarat.Text) - m_OldLostCarat;
                        //        decimal LossCarat = Val.ToDecimal(txtLossCarat.Text) - m_OldLossCarat;
                        //        decimal PlusCarat = Val.ToDecimal(txtCaratPlus.Text) - m_OldPlusCarat;
                        //        Res = objHistoryView.UpdatePcsCaratHistory(LotId, HistoryId, RecPcs, RecCarat, RRPcs, RRCarat, RejPcs, RejCarat, ResawPcs, ResawCarat, BreakPcs, BreakCarat, LostPcs, LostCarat, LossCarat, PlusCarat, p_lastEntryflag, count);
                        //    }
                        //    if (i > 0 && Val.ToInt(m_Dt.Rows[i]["history_type"]) == 2)
                        //    {
                        //        Int64 LotId = Val.ToInt64(m_Dt.Rows[i]["lot_id"]);
                        //        int HistoryId = Val.ToInt(m_Dt.Rows[i]["history_id"]);
                        //        int RecPcs = Val.ToInt(txtReturnPcs.Text) - m_OldRecPcs;
                        //        decimal RecCarat = Val.ToDecimal(txtReturnCarat.Text) - m_OldRecCarat;
                        //        int RRPcs = Val.ToInt(txtOutpcs.Text) - m_OldRRPcs;
                        //        decimal RRCarat = Val.ToDecimal(txtOutCarat.Text) - m_OldRRCarat;
                        //        int RejPcs = Val.ToInt(txtRejPcs.Text) - m_OldRejPcs;
                        //        decimal RejCarat = Val.ToDecimal(txtRejCarat.Text) - m_OldRejCarat;
                        //        int ResawPcs = Val.ToInt(txtReSoingPcs.Text) - m_OldResawPcs;
                        //        decimal ResawCarat = Val.ToDecimal(txtResoingCarat.Text) - m_OldResawCarat;
                        //        int BreakPcs = Val.ToInt(txtBreakPcs.Text) - m_OldBreakPcs;
                        //        decimal BreakCarat = Val.ToDecimal(txtBreakCarat.Text) - m_OldBreakCarat;
                        //        int LostPcs = Val.ToInt(txtLostPcs.Text) - m_OldLostPcs;
                        //        decimal LostCarat = Val.ToDecimal(txtLostCarat.Text) - m_OldLostCarat;
                        //        decimal LossCarat = Val.ToDecimal(txtLossCarat.Text) - m_OldLossCarat;
                        //        decimal PlusCarat = Val.ToDecimal(txtCaratPlus.Text) - m_OldPlusCarat;
                        //        count = 1;
                        //        Res = objHistoryView.UpdatePcsCaratHistory(LotId, HistoryId, RecPcs, RecCarat, RRPcs, RRCarat, RejPcs, RejCarat, ResawPcs, ResawCarat, BreakPcs, BreakCarat, LostPcs, LostCarat, LossCarat, PlusCarat, p_lastEntryflag, count);
                        //    }
                        //    if (Val.ToInt(m_Dt.Rows[i]["history_type"]) == 3 || Val.ToInt(m_Dt.Rows[i]["history_type"]) == 6)
                        //    {
                        //        Int64 LotId = Val.ToInt64(m_Dt.Rows[i]["lot_id"]);
                        //        int HistoryId = Val.ToInt(m_Dt.Rows[i]["history_id"]);
                        //        int RecPcs = Val.ToInt(txtReturnPcs.Text) - m_OldRecPcs;
                        //        decimal RecCarat = Val.ToDecimal(txtReturnCarat.Text) - m_OldRecCarat;
                        //        int RRPcs = Val.ToInt(txtOutpcs.Text) - m_OldRRPcs;
                        //        decimal RRCarat = Val.ToDecimal(txtOutCarat.Text) - m_OldRRCarat;
                        //        int RejPcs = Val.ToInt(txtRejPcs.Text) - m_OldRejPcs;
                        //        decimal RejCarat = Val.ToDecimal(txtRejCarat.Text) - m_OldRejCarat;
                        //        int ResawPcs = Val.ToInt(txtReSoingPcs.Text) - m_OldResawPcs;
                        //        decimal ResawCarat = Val.ToDecimal(txtResoingCarat.Text) - m_OldResawCarat;
                        //        int BreakPcs = Val.ToInt(txtBreakPcs.Text) - m_OldBreakPcs;
                        //        decimal BreakCarat = Val.ToDecimal(txtBreakCarat.Text) - m_OldBreakCarat;
                        //        int LostPcs = Val.ToInt(txtLostPcs.Text) - m_OldLostPcs;
                        //        decimal LostCarat = Val.ToDecimal(txtLostCarat.Text) - m_OldLostCarat;
                        //        decimal LossCarat = Val.ToInt(txtLossCarat.Text) - m_OldLossCarat;
                        //        decimal PlusCarat = Val.ToDecimal(txtCaratPlus.Text) - m_OldPlusCarat;
                        //        Res = objHistoryView.UpdatePcsCaratHistory(LotId, HistoryId, RecPcs, RecCarat, RRPcs, RRCarat, RejPcs, RejCarat, ResawPcs, ResawCarat, BreakPcs, BreakCarat, LostPcs, LostCarat, LossCarat, PlusCarat, p_lastEntryflag, count);
                        //    }
                        //    if (Val.ToInt(m_Dt.Rows[i]["history_type"]) != 1 && Val.ToInt(m_Dt.Rows[i]["history_type"]) != 2 && Val.ToInt(m_Dt.Rows[i]["history_type"]) != 3 && Val.ToInt(m_Dt.Rows[i]["history_type"]) != 6)
                        //    {
                        //        break;
                        //    }
                        //}
                    }
                    if (Res > 0)
                    {
                        Global.Message("Update Successful");
                        this.Cursor = Cursors.WaitCursor;
                        DataTable DtTab = new DataTable();
                        int Active = chkActive.Checked == true ? 1 : 0;
                        DtTab = objHistoryView.GetHistoryData(Val.ToInt(lueKapan.EditValue), Val.ToInt(lueCutNo.EditValue), Val.ToInt64(txtLotID.Text), Val.ToInt(Active));
                        if (DtTab.Rows.Count > 0)
                        {
                            MainDgvHistoryDetail.DataSource = DtTab;
                            this.Cursor = Cursors.Default;
                            pnlMultiUpdate.Visible = false;
                        }
                        else
                        {
                            Global.Message("Data Not Found");
                            MainDgvHistoryDetail.DataSource = null;
                            Btn_Show.Focus();
                            this.Cursor = Cursors.Default;
                        }
                    }
                    //DgvHistoryDetail.GetRowHandle(DgvHistoryDetail.FocusedRowHandle);
                }
                else
                {
                    pnlMultiUpdate.Visible = false;
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }

        private void btnAllPcsUpdate_Click(object sender, EventArgs e)
        {
            m_updateIssueId = 0;
            if (DgvHistoryDetail.FocusedRowHandle < 0)
            {
                return;
            }
            DataTable Dt_StockData = new DataTable();
            Dt_StockData = objHistoryView.GetLotStockDetail(Val.ToInt32(lueCutNo.EditValue), Val.ToInt64(txtLotID.Text));
            tdt_ = new DataTable();
            tdt_ = (DataTable)MainDgvHistoryDetail.DataSource;

            Int64 HST_ID = Val.ToInt64(DgvHistoryDetail.GetRowCellValue(DgvHistoryDetail.FocusedRowHandle, "history_id"));

            m_Dt = tdt_.Select("history_id >=" + HST_ID + " AND lot_id='" + txtLotID.Text + "'").CopyToDataTable();
            if (Dt_StockData.Rows.Count > 0)
            {
                if (Val.ToInt(Dt_StockData.Rows[0]["department_id"]) == Val.ToInt(m_Dt.Rows[0]["department_id"]) && Val.ToInt(Dt_StockData.Rows[0]["department_id"]) == Val.ToInt(GlobalDec.gEmployeeProperty.department_id))
                {
                    for (int i = 0; i < m_Dt.Rows.Count; i++)
                    {
                        if (Val.ToInt(m_Dt.Rows[i]["department_id"]) != Val.ToInt(Dt_StockData.Rows[0]["department_id"]) && Val.ToInt(m_Dt.Rows[i]["department_id"]) != Val.ToInt(GlobalDec.gEmployeeProperty.department_id))
                        {
                            Global.Message("Entry Not Update Because Lot Not In Your Department");
                            return;
                        }
                    }
                }
                else
                {
                    Global.Message("Entry Not Update Because Lot Not In Your Department");
                    return;
                }
            }
            if (m_Dt.Rows.Count > 0)
            {

                for (int i = 0; i < m_Dt.Rows.Count; i++)
                {
                    if (Val.ToInt(m_Dt.Rows[i]["history_type"]) == 13)
                    {
                        Global.Message("Entry Not Update Because Lot Is Split ");
                        return;
                    }

                }
                pnlAllPcsUpdate.Visible = true;
            }
            pnlAllPcsUpdate.Visible = true;
            txtPcs.Text = Val.ToString(m_Dt.Rows[0]["balance_pcs"]);
            txtCarat.Text = Val.ToString(m_Dt.Rows[0]["balance_carat"]);

        }

        private void btnPcsUpdate_Click(object sender, EventArgs e)
        {
            int Res = 0;
            Int64 LotId = Val.ToInt64(m_Dt.Rows[0]["lot_id"]);
            int HistoryId = Val.ToInt(m_Dt.Rows[0]["history_id"]);
            int pcs = Val.ToInt(txtPcs.Text);
            decimal Carat = Val.ToDecimal(txtCarat.Text);
            if (Val.ToString(m_Dt.Rows[0]["process_name"]) == "ASSORT FINAL")
            {
                Res = objHistoryView.UpdateAssortFinal(LotId, HistoryId, pcs, Carat);
            }
            else
            {
                Res = objHistoryView.UpdateAllPcsHistory(LotId, HistoryId, pcs, Carat, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 12, 12);
            }

            if (Res > 0)
            {
                Global.Message("Update Successful");
                this.Cursor = Cursors.WaitCursor;
                DataTable DtTab = new DataTable();
                int Active = chkActive.Checked == true ? 1 : 0;
                DtTab = objHistoryView.GetHistoryData(Val.ToInt(lueKapan.EditValue), Val.ToInt(lueCutNo.EditValue), Val.ToInt64(txtLotID.Text), Val.ToInt(Active));
                if (DtTab.Rows.Count > 0)
                {
                    MainDgvHistoryDetail.DataSource = DtTab;
                    this.Cursor = Cursors.Default;
                }
                else
                {
                    Global.Message("Data Not Found");
                    MainDgvHistoryDetail.DataSource = null;
                    Btn_Show.Focus();
                    this.Cursor = Cursors.Default;
                }
                pnlAllPcsUpdate.Visible = false;
            }
        }

        private void btnPcsClose_Click(object sender, EventArgs e)
        {
            pnlAllPcsUpdate.Visible = false;
        }

        private void btnPrdClose_Click(object sender, EventArgs e)
        {
            pnlPredictionUpdate.Visible = false;
        }

        private void btnPrdUpdate_Click(object sender, EventArgs e)
        {
            int Res = 0;
            Int64 LotId = Val.ToInt64(m_Dt.Rows[0]["lot_id"]);
            int HistoryId = Val.ToInt(m_Dt.Rows[0]["history_id"]);
            int CharniId = Val.ToInt(lueClarity.EditValue);
            int ShadeId = Val.ToInt(lueRoughSieve.EditValue);
            int QualityId = Val.ToInt(lueQuality.EditValue);
            if (Val.ToInt(m_Dt.Rows[0]["prediction_id"]) > 0)
            {

                Res = objHistoryView.UpdatePrediction(LotId, HistoryId, CharniId, ShadeId, QualityId);
            }

            if (Res > 0)
            {
                Global.Message("Update Successful");
                this.Cursor = Cursors.WaitCursor;
                DataTable DtTab = new DataTable();
                int Active = chkActive.Checked == true ? 1 : 0;
                DtTab = objHistoryView.GetHistoryData(Val.ToInt(lueKapan.EditValue), Val.ToInt(lueCutNo.EditValue), Val.ToInt64(txtLotID.Text), Val.ToInt(Active));
                if (DtTab.Rows.Count > 0)
                {
                    MainDgvHistoryDetail.DataSource = DtTab;
                    this.Cursor = Cursors.Default;
                }
                else
                {
                    Global.Message("Data Not Found");
                    MainDgvHistoryDetail.DataSource = null;
                    Btn_Show.Focus();
                    this.Cursor = Cursors.Default;
                }
                pnlPredictionUpdate.Visible = false;
            }
        }

        private void txtReturnPcs_Validated(object sender, EventArgs e)
        {
            sumData();
        }
        public void sumData()
        {
            txtLossCarat.Text = Val.ToString(Val.ToDecimal(lblOsCarat.Text) - (Val.ToDecimal(txtReturnCarat.Text) + Val.ToDecimal(txtBreakCarat.Text) + Val.ToDecimal(txtResoingCarat.Text) + Val.ToDecimal(txtLostCarat.Text) + Val.ToDecimal(txtOutCarat.Text) + Val.ToDecimal(txtRejCarat.Text)));
        }

        private void txtReturnCarat_EditValueChanged(object sender, EventArgs e)
        {
            sumData();
        }

        private void txtBreakCarat_EditValueChanged(object sender, EventArgs e)
        {
            sumData();
        }

        private void txtCaratPlus_EditValueChanged(object sender, EventArgs e)
        {
            // sumData();
        }

        private void DgvHistoryDetail_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                if (GlobalDec.gEmployeeProperty.role_name == "SURAT ROUGH" || GlobalDec.gEmployeeProperty.role_name == "SURAT ADMIN" || GlobalDec.gEmployeeProperty.role_name == "SURAT MAKABLE")
                {
                    if (GlobalDec.gEmployeeProperty.user_name == "RAHUL" || GlobalDec.gEmployeeProperty.user_name == "PRAFUL" || GlobalDec.gEmployeeProperty.user_name == "PRAGNESH")
                    {
                        if (Global.Confirm("Are you sure Update selected row?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            Lot_Srno = Val.ToInt64(DgvHistoryDetail.GetFocusedRowCellValue("lot_srno").ToString());
                            Process_id = Val.ToInt64(DgvHistoryDetail.GetFocusedRowCellValue("process_id").ToString());
                            History_Type_id = Val.ToInt64(DgvHistoryDetail.GetFocusedRowCellValue("history_type").ToString());
                            Sieve_ID = Val.ToInt64(DgvHistoryDetail.GetFocusedRowCellValue("sieve_id").ToString());
                            Rough_Clarity_ID = Val.ToInt64(DgvHistoryDetail.GetFocusedRowCellValue("rough_clarity_id").ToString());
                            Quality_ID = Val.ToInt64(DgvHistoryDetail.GetFocusedRowCellValue("quality_id").ToString());
                            pnlManagerUpdate.Visible = true;
                        }
                    }
                }
            }
            if (e.KeyCode == Keys.F6)
            {
                if (GlobalDec.gEmployeeProperty.role_name == "SURAT MAKABLE" || GlobalDec.gEmployeeProperty.role_name == "SURAT ADMIN")
                {
                    if (GlobalDec.gEmployeeProperty.user_name == "KAILASH" || GlobalDec.gEmployeeProperty.user_name == "PRAFUL" || GlobalDec.gEmployeeProperty.user_name == "PRAGNESH")
                    {
                        Janged_ID = Val.ToInt64(DgvHistoryDetail.GetFocusedRowCellValue("janged_id").ToString());
                        Party_ID = Val.ToInt64(DgvHistoryDetail.GetFocusedRowCellValue("party_id").ToString());
                        if (Janged_ID != 0)
                        {
                            if (Global.Confirm("Are you sure Update selected row?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                            {
                                Lot_Srno = Val.ToInt64(DgvHistoryDetail.GetFocusedRowCellValue("lot_srno").ToString());
                                Janged_ID = Val.ToInt64(DgvHistoryDetail.GetFocusedRowCellValue("janged_id").ToString());
                                pnlPartyUpdate.Visible = true;
                            }
                        }
                        else
                        {
                            Global.Message("Party Name is Blank...So Please Check");
                            return;
                        }
                    }
                }
            }
            if (e.KeyCode == Keys.F7)
            {
                if (GlobalDec.gEmployeeProperty.role_name == "SURAT MAKABLE" || GlobalDec.gEmployeeProperty.role_name == "SURAT ADMIN")
                {
                    if (GlobalDec.gEmployeeProperty.user_name == "KAILASH" || GlobalDec.gEmployeeProperty.user_name == "PRAFUL" || GlobalDec.gEmployeeProperty.user_name == "PRAGNESH")
                    {
                        if (DgvHistoryDetail.FocusedRowHandle < 0)
                        {
                            return;
                        }

                        tdt_ = new DataTable();
                        tdt_ = (DataTable)MainDgvHistoryDetail.DataSource;
                        Int64 max_val = 0;
                        if (tdt_.Select("lot_id='" + txtLotID.Text + "' AND active='True'").Count() > 0)
                        {
                            max_val = Convert.ToInt32(tdt_.Select("lot_id='" + txtLotID.Text + "' AND active= 'True'").CopyToDataTable().Compute("max([history_id])", string.Empty));

                        }
                        else
                        {
                            max_val = 0;
                        }
                        DataTable DtHst = new DataTable();
                        int LatestId = 0;
                        int Active = chkActive.Checked == true ? 1 : 0;
                        DtHst = objHistoryView.GetHistoryData(Val.ToInt(lueKapan.EditValue), Val.ToInt(lueCutNo.EditValue), Val.ToInt64(txtLotID.Text), Val.ToInt(Active));
                        if (DtHst.Rows.Count > 0)
                        {
                            LatestId = Convert.ToInt32(DtHst.Select("lot_id='" + txtLotID.Text + "' AND active= 'True'").CopyToDataTable().Compute("max([history_id])", string.Empty));
                        }
                        else
                        {
                            LatestId = 0;
                        }


                        Int64 HST_ID = Val.ToInt64(DgvHistoryDetail.GetRowCellValue(DgvHistoryDetail.FocusedRowHandle, "history_id"));

                        if (max_val != HST_ID || LatestId != HST_ID)
                        {
                            MessageBox.Show("This Transaction Can't Deleted.\n It is not a last Transaction.\n Please Select last Transaction to Delete.");
                            return;
                        }

                        Int64 History_Type = Val.ToInt64(DgvHistoryDetail.GetFocusedRowCellValue("history_type").ToString());
                        if (History_Type == 13)
                        {
                            if (Global.Confirm("Are you sure Delete selected row?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                            {
                                Lot_Srno = Val.ToInt64(DgvHistoryDetail.GetFocusedRowCellValue("lot_srno").ToString());
                                txtBalancePcs.Text = Val.ToInt64(DgvHistoryDetail.GetFocusedRowCellValue("balance_pcs")).ToString();
                                txtBalanceCarat.Text = Val.ToDecimal(DgvHistoryDetail.GetFocusedRowCellValue("balance_carat")).ToString();
                                pnlMixSplitUpdate.Visible = true;
                            }
                        }
                        else
                        {
                            Global.Message("This Lot Not a Mix Split Lot ID...So Please Check");
                            return;
                        }
                    }
                }
            }
        }

        private void btnMgrClose_Click(object sender, EventArgs e)
        {
            pnlManagerUpdate.Visible = false;
            Lot_Srno = 0;
            Process_id = 0;
            lueUpdManager.EditValue = null;
            lueEmployee.EditValue = null;
            ChkManager.Checked = false;
            ChkEmployee.Checked = false;
        }

        private void BtnMgrUpdate_Click(object sender, EventArgs e)
        {
            int Res = 0;
            if (ChkManager.Checked == true)
            {
                if (lueUpdManager.Text == "")
                {
                    Global.Message("Please Select Manager");
                    lueManager.Focus();
                    return;
                }
            }
            if (ChkEmployee.Checked == true)
            {
                if (lueEmployee.Text == "")
                {
                    Global.Message("Please Select Employee");
                    lueEmployee.Focus();
                    return;
                }
            }

            if (Global.Confirm("Are you sure update selected row?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                Res = objHistoryView.UpdateManagerAndEmp_History(Lot_Srno, Process_id, Val.ToInt64(lueUpdManager.EditValue), Val.ToInt64(lueEmployee.EditValue), Val.ToInt64(txtLotID.Text), History_Type_id, Sieve_ID, Rough_Clarity_ID, Quality_ID, Val.ToBoolean(ChkManager.Checked), Val.ToBoolean(ChkEmployee.Checked));

                if (Res > 0)
                {
                    Global.Message("Update Successfully");
                    pnlManagerUpdate.Visible = false;
                    Lot_Srno = 0;
                    Process_id = 0;
                    lueUpdManager.EditValue = null;
                    lueEmployee.EditValue = null;
                    ChkManager.Checked = false;
                    ChkEmployee.Checked = false;
                    Btn_Show_Click(null, null);
                }
                else
                {
                    Btn_Show_Click(null, null);
                }
            }
        }

        private void btnPnlPartyClose_Click(object sender, EventArgs e)
        {
            pnlPartyUpdate.Visible = false;
            Lot_Srno = 0;
            Janged_ID = 0;
            lueUpdParty.EditValue = null;
        }

        private void BtnPartyUpdate_Click(object sender, EventArgs e)
        {
            int Res = 0;
            if (lueUpdParty.Text == "")
            {
                Global.Message("Please Select Party");
                lueUpdParty.Focus();
                return;
            }

            if (Global.Confirm("Are you sure update selected row?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                Res = objHistoryView.UpdateParty_History(Lot_Srno, Janged_ID, Val.ToInt64(lueUpdParty.EditValue), Val.ToInt64(txtLotID.Text), Val.ToInt64(Party_ID));

                if (Res > 0)
                {
                    Global.Message("Update Successfully");
                    pnlPartyUpdate.Visible = false;
                    Lot_Srno = 0;
                    Janged_ID = 0;
                    lueUpdParty.EditValue = null;
                    Btn_Show_Click(null, null);
                }
                else
                {
                    Btn_Show_Click(null, null);
                }
            }
        }

        private void BtnDeleteMixSplit_Click(object sender, EventArgs e)
        {
            int Res = 0;

            if (Global.Confirm("Are you sure Delete selected row?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                Res = objHistoryView.DeleteMixSplit_LotWise_History(Lot_Srno, Val.ToInt64(txtBalancePcs.Text), Val.ToDecimal(txtBalanceCarat.Text), Val.ToInt64(txtLotID.Text));

                if (Res > 0)
                {
                    Global.Message("Deleted Successfully");
                    pnlMixSplitUpdate.Visible = false;
                    Lot_Srno = 0;
                    txtBalancePcs.Text = "0";
                    txtBalanceCarat.Text = "0";
                    Btn_Show_Click(null, null);
                }
                else
                {
                    Btn_Show_Click(null, null);
                }
            }
        }

        private void BtnMixLotClose_Click(object sender, EventArgs e)
        {
            pnlMixSplitUpdate.Visible = false;
            Lot_Srno = 0;
            txtBalancePcs.Text = "0";
            txtBalanceCarat.Text = "0";
        }
        private void txtRejCarat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                btnIsURecUpdate.Focus();
            }
        }

        private void txtLotID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}