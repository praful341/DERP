using BLL;
using BLL.FunctionClasses.Master.MFG;
using BLL.FunctionClasses.Rejection;
using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.PropertyClasses.Report;
using BLL.PropertyClasses.Transaction;
using BLL.PropertyClasses.Transaction.MFG;
using DERP.Class;
using DevExpress.Data;
using DevExpress.Diagram.Core;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using static DERP.Class.Global;
using static DERP.Master.FrmPartyMaster;

namespace DERP.Transaction.MFG
{
    public partial class FrmMFGRejectionAvgRate : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;
        DataTable m_dtbSubProcess = new DataTable();
        DataTable m_dtbColor = new DataTable();
        MFGAssortFirst objAssortFirst;
        MFGProcessReceive objProcessReceive;
        MfgRoughSieve objRoughSieve;
        MfgRoughClarityMaster objClarity;
        public static readonly DiagramCommand SaveFileCommand;
        Control _NextEnteredControl;
        private List<Control> _tabControls;

        public New_Report_DetailProperty ObjReportDetailProperty;
        MFGRoughStockEntry objRoughStockEntry = new MFGRoughStockEntry();
        FillCombo ObjFillCombo = new FillCombo();
        DataTable DtControlSettings;
        DataTable dtTemp;
        DataTable m_dtbParam;
        DataTable m_dtCut;
        DataTable m_DtProcess;
        DataTable m_dtbKapan;
        DataTable m_dtOutstanding;
        DataTable DTabQuality = new DataTable();
        DataTable DtRejectionAvgRate = new DataTable();

        Int64 m_numForm_id;
        Int64 IntRes;
        decimal TotalCarat = 0;
        decimal TotalAmount = 0;
        int m_IsLot;

        string StrListTempPurity = string.Empty;

        #endregion

        #region Constructor
        public FrmMFGRejectionAvgRate()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            _NextEnteredControl = new Control();
            _tabControls = new List<Control>();

            objAssortFirst = new MFGAssortFirst();
            objRoughSieve = new MfgRoughSieve();
            objClarity = new MfgRoughClarityMaster();
            ObjReportDetailProperty = new New_Report_DetailProperty();
            objProcessReceive = new MFGProcessReceive();
            DtControlSettings = new DataTable();
            dtTemp = new DataTable();
            m_dtbParam = new DataTable();
            m_dtCut = new DataTable();
            m_DtProcess = new DataTable();
            m_dtOutstanding = new DataTable();
            m_dtbKapan = new DataTable();
            m_numForm_id = 0;
            m_IsLot = 0;
        }
        public void ShowForm()
        {
            ObjPer.FormName = this.Name.ToUpper();
            m_numForm_id = ObjPer.form_id;
            if (ObjPer.CheckPermission() == false)
            {
                Global.Message(BLL.GlobalDec.gStrPermissionViwMsg);
                return;
            }
            Val.frmGenSet(this);
            AttachFormEvents();

            //if (Global.HideFormControls(Val.ToInt(ObjPer.form_id), this) != "")
            //{
            //    Global.Message("Select First User Setting...Please Contact to Administrator...");
            //    return;
            //}

            //ControlSettingDT(Val.ToInt(ObjPer.form_id), this);
            //AddGotFocusListener(this);
            ////AddKeyPressListener(this);
            //this.KeyPreview = true;

            //TabControlsToList(this.Controls);
            //_tabControls = _tabControls.OrderBy(x => x.TabIndex).ToList();

            this.Show();
        }
        private void AttachFormEvents()
        {
            objBOFormEvents.CurForm = this;
            objBOFormEvents.FormKeyPress = true;
            objBOFormEvents.FormKeyDown = true;
            objBOFormEvents.FormResize = true;
            objBOFormEvents.FormClosing = true;
            objBOFormEvents.ObjToDispose.Add("");
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion

        #region Events
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

                string Str = "";
                if (Val.DBDate(BLL.GlobalDec.gStrServerDate) != Val.DBDate(dtpEntryDate.Text))
                {
                    Str = GlobalDec.CheckLockIsOpenOrNot(Val.DBDate(dtpEntryDate.Text), Val.DBTime(DateTime.Now.ToShortTimeString())); //Val.GetFullTime12());
                    if (Str != "YES")
                    {
                        if (Str != "")
                        {
                            Global.Message(Str);
                            return;
                        }
                        else
                        {
                            Global.Message("You Are Not Suppose to Make Entry On Different Date");
                            return;
                        }
                    }
                    else
                    {
                        dtpEntryDate.Enabled = true;
                        dtpEntryDate.Visible = true;
                    }
                }
                List<ListError> lstError = new List<ListError>();
                Dictionary<Control, string> rtnCtrls = new Dictionary<Control, string>();
                rtnCtrls = Global.CheckCompulsoryControls(Val.ToInt(ObjPer.form_id), this);
                if (rtnCtrls.Count > 0)
                {
                    foreach (KeyValuePair<Control, string> entry in rtnCtrls)
                    {
                        if (entry.Key is DevExpress.XtraEditors.LookUpEdit || entry.Key is DevExpress.XtraEditors.DateEdit || entry.Key is DevExpress.XtraEditors.TextEdit)
                        {
                            lstError.Add(new ListError(13, entry.Value));
                        }
                    }
                    rtnCtrls.First().Key.Focus();
                    BLL.General.ShowErrors(lstError);
                    Cursor.Current = Cursors.Arrow;
                    return;
                }

                if (!ValidateDetails())
                {
                    return;
                }
                DialogResult result = MessageBox.Show("Do you want to save Rejection Avg Rate Entry data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result != DialogResult.Yes)
                {
                    btnSave.Enabled = true;
                    return;
                }

                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;

                panelProgress.Visible = true;
                backgroundWorker_RejectionAvgRate.RunWorkerAsync();

                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearDetails();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateDetails())
                {
                    return;
                }


                grdMFGRejAvgRate.DataSource = null;
                DtRejectionAvgRate.Columns.Clear();
                DtRejectionAvgRate.AcceptChanges();
                MFGRejectionAvgRate objMFGRejectionAvgRate = new MFGRejectionAvgRate();
                MFGRejectionAvgRateProperty objMFGRejectionAvgRateProperty = new MFGRejectionAvgRateProperty();
                objMFGRejectionAvgRateProperty.kapan_id = Val.ToInt64(lueKapan.EditValue);
                objMFGRejectionAvgRateProperty.rough_cut_no = Val.ToString(txtRoughCutNo.Text);

                DtRejectionAvgRate = objMFGRejectionAvgRate.MFGRejectionAvgRate_GetData(objMFGRejectionAvgRateProperty);
                lblLotSRNo.Text = "0";


                grdMFGRejAvgRate.DataSource = DtRejectionAvgRate;
                dgvMFGRejAvgRate.OptionsView.ShowFooter = true;
                //dgvMFGRejAvgRate.BestFitColumns();
                dgvMFGRejAvgRate.FocusedColumn = dgvMFGRejAvgRate.Columns["carat"];
                dgvMFGRejAvgRate.ShowEditor();

                dgvMFGRejAvgRate.ClearGrouping();
                dgvMFGRejAvgRate.Columns["group_name"].GroupIndex = 0;
                dgvMFGRejAvgRate.OptionsView.ShowGroupedColumns = false;
                dgvMFGRejAvgRate.ExpandAllGroups();

                decimal Tcarat = 0;
                decimal Tamount = 0;

                for (int i = 0; i <= DtRejectionAvgRate.Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= DtRejectionAvgRate.Columns.Count - 1; j++)
                    {
                        if (DtRejectionAvgRate.Columns[j].ToString().Contains("carat"))
                        {
                            Tcarat += Val.ToDecimal(DtRejectionAvgRate.Rows[i][j]);
                        }

                        if (DtRejectionAvgRate.Columns[j].ToString().Contains("amount"))
                        {
                            Tamount += Val.ToDecimal(DtRejectionAvgRate.Rows[i][j]);
                        }
                        if (DtRejectionAvgRate.Columns[j].ToString().Contains("amount"))
                        {
                            DtRejectionAvgRate.Rows[i][j] = Val.ToString(Math.Round(Tamount, 2));
                            Tcarat = 0;
                            Tamount = 0;
                        }
                    }
                }
                for (int i = 0; i <= DtRejectionAvgRate.Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= DtRejectionAvgRate.Columns.Count - 1; j++)
                    {
                        if (DtRejectionAvgRate.Columns[j].ToString().Contains("carat"))
                        {
                            dgvMFGRejAvgRate.Columns[DtRejectionAvgRate.Columns[j].ToString()].Summary.Add(SummaryItemType.Sum, DtRejectionAvgRate.Columns[j].ToString(), "{0:N3}");
                            dgvMFGRejAvgRate.GroupSummary.Add(SummaryItemType.Sum, DtRejectionAvgRate.Columns[j].ToString(), dgvMFGRejAvgRate.Columns[DtRejectionAvgRate.Columns[j].ToString()], "{0:N3}");

                            dgvMFGRejAvgRate.Columns["carat"].Width = 100;
                        }

                        if (DtRejectionAvgRate.Columns[j].ToString().Contains("rate"))
                        {
                            dgvMFGRejAvgRate.Columns[DtRejectionAvgRate.Columns[j].ToString()].Summary.Add(SummaryItemType.Custom, DtRejectionAvgRate.Columns[j].ToString(), "{0:N3}");
                            dgvMFGRejAvgRate.GroupSummary.Add(SummaryItemType.Custom, DtRejectionAvgRate.Columns[j].ToString(), dgvMFGRejAvgRate.Columns[DtRejectionAvgRate.Columns[j].ToString()], "{0:N3}");
                            dgvMFGRejAvgRate.Columns["rate"].Width = 100;
                        }

                        if (DtRejectionAvgRate.Columns[j].ToString().Contains("amount"))
                        {
                            dgvMFGRejAvgRate.Columns[DtRejectionAvgRate.Columns[j].ToString()].Summary.Add(SummaryItemType.Sum, DtRejectionAvgRate.Columns[j].ToString(), "{0:N3}");
                            dgvMFGRejAvgRate.GroupSummary.Add(SummaryItemType.Sum, DtRejectionAvgRate.Columns[j].ToString(), dgvMFGRejAvgRate.Columns[DtRejectionAvgRate.Columns[j].ToString()], "{0:N3}");
                            dgvMFGRejAvgRate.Columns["amount"].Width = 100;
                        }
                    }
                    break;
                }
                // dgvMFGRejAvgRate.OptionsView.ShowFooter = true;
                dgvMFGRejAvgRate.BestFitColumns();
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (!ValidateDetails())
            //    {
            //        return;
            //    }

            //    MFGPataLotEntry objMFGPataLotEntry = new MFGPataLotEntry();
            //    MFGPataLotEntryProperty objMFGPataLotEntryProperty = new MFGPataLotEntryProperty();
            //    objMFGPataLotEntryProperty.kapan_id = Val.ToInt64(lueKapan.EditValue);
            //    objMFGPataLotEntryProperty.lot_srno = Val.ToInt64(lblLotSRNo.Text);

            //    DtRejectionAvgRate = objMFGPataLotEntry.MFGPataLotEntryGetData(objMFGPataLotEntryProperty);
            //    lblLotSRNo.Text = "0";

            //    grdMFGRejAvgRate.DataSource = DtRejectionAvgRate;
            //    dgvMFGRejAvgRate.OptionsView.ShowFooter = true;
            //    dgvMFGRejAvgRate.BestFitColumns();
            //    dgvMFGRejAvgRate.FocusedColumn = dgvMFGRejAvgRate.Columns["1_carat"];
            //    dgvMFGRejAvgRate.ShowEditor();
            //}
            //catch (Exception ex)
            //{
            //    General.ShowErrors(ex.ToString());
            //}
        }
        private void lueKapan_EditValueChanged(object sender, EventArgs e)
        {
            if (m_IsLot == 0)
            {
                m_dtbParam = new DataTable();
                if (lueKapan.Text.ToString() != "")
                {
                    m_dtbParam = Global.GetRoughKapanWise_RejectionAvgData(Val.ToInt64(lueKapan.EditValue));

                    if (m_dtbParam.Rows.Count > 0 && Val.ToDecimal(m_dtbParam.Rows[0]["kapan_carat"]) > 0)
                    {
                        txtCarat.Text = Val.ToString(Val.ToDecimal(m_dtbParam.Rows[0]["kapan_carat"]));
                        txtRate.Text = Val.ToString(Val.ToDecimal(m_dtbParam.Rows[0]["kapan_rate"]));
                        txtAmount.Text = Val.ToString(Val.ToDecimal(m_dtbParam.Rows[0]["kapan_amount"]));
                        txtCutRate.Text = Val.ToString(Val.ToDecimal(m_dtbParam.Rows[0]["kapan_rate"]));
                        txtCutAmount.Text = Val.ToString(Val.ToDecimal(m_dtbParam.Rows[0]["kapan_amount"]));

                        txtCutCarat.Text = "0";
                        txtRoughCutNo.Text = string.Empty;
                    }
                    else
                    {
                        Global.Message("Carat Not Found");
                        txtCarat.Text = "0";
                        txtRate.Text = "0";
                        txtAmount.Text = "0";
                        txtCutCarat.Text = "0";
                        txtCutRate.Text = "0";
                        txtCutAmount.Text = "0";
                        txtRoughCutNo.Text = string.Empty;
                        return;
                    }
                }
            }
        }

        private void grdProcessReceive_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    (grdMFGRejAvgRate.FocusedView as ColumnView).FocusedRowHandle++;
                    e.Handled = true;
                }
            }
            catch { }
        }
        private void dgvProcessReceive_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                DataTable dtAmount = new DataTable();
                dtAmount = (DataTable)grdMFGRejAvgRate.DataSource;
                string[] col = e.Column.FieldName.Split('_');

                decimal rate = 0;
                decimal carat = 0;
                for (int i = 0; i <= dtAmount.Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= dtAmount.Columns.Count - 1; j++)
                    {
                        if (dtAmount.Columns[j].ToString().Contains("carat"))
                        {
                            carat = Val.ToDecimal(dtAmount.Rows[i][j]);
                        }
                        else if (dtAmount.Columns[j].ToString().Contains("rate"))
                        {
                            rate = Val.ToDecimal(dtAmount.Rows[i][j]);
                        }
                        else if (dtAmount.Columns[j].ColumnName.Contains("amount"))
                        {
                            decimal amount = Math.Round((carat * rate), 0);
                            dtAmount.Rows[i][j] = amount.ToString();
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        private void dgvProcessReceive_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {

        }
        private void txtCarat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as DevExpress.XtraEditors.TextEdit).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        #region GridEvents

        #endregion

        #endregion

        #region Functions
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (lueKapan.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Kapan No"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueKapan.Focus();
                    }
                }
                if (txtRoughCutNo.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Rough Cut No"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtRoughCutNo.Focus();
                    }
                }
                if (txtCarat.Text.ToString() == "" || txtCarat.Text.ToString() == "0")
                {
                    lstError.Add(new ListError(12, "Carat"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtCarat.Focus();
                    }
                }
                if (txtRate.Text.ToString() == "" || txtRate.Text.ToString() == "0")
                {
                    lstError.Add(new ListError(12, "Rate"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtRate.Focus();
                    }
                }
                if (txtAmount.Text.ToString() == "" || txtAmount.Text.ToString() == "0")
                {
                    lstError.Add(new ListError(12, "Amount"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtAmount.Focus();
                    }
                }
                if (txtCutCarat.Text.ToString() == "" || txtCutCarat.Text.ToString() == "0")
                {
                    lstError.Add(new ListError(12, "Cut Carat"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtCutCarat.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                lstError.Add(new ListError(ex));
            }
            return (!(BLL.General.ShowErrors(lstError)));
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
        private void AddKeyPressListener(Control ctrl)
        {
            foreach (Control c in ctrl.Controls)
            {
                c.KeyPress += new KeyPressEventHandler(Control_KeyPress);
                if (c.Controls.Count > 0)
                {
                    AddKeyPressListener(c);
                }
            }
        }
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((Control)sender).Name.ToString().Trim().Equals(string.Empty))
            {
                _NextEnteredControl = (Control)sender;
                if ((Control)sender is LookUpEdit)
                {
                    if (e.KeyChar == Convert.ToChar(Keys.Enter))
                    {
                        SendKeys.Send("{TAB}");
                    }
                }
                if ((Control)sender is CheckedComboBoxEdit)
                {
                    if (e.KeyChar == Convert.ToChar(Keys.Enter))
                    {
                        SendKeys.Send("{TAB}");
                    }
                }
                if ((Control)sender is ButtonEdit)
                {
                    if (e.KeyChar == Convert.ToChar(Keys.Enter))
                    {
                        SendKeys.Send("{TAB}");
                    }
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
                else if ((Control)sender is CheckedComboBoxEdit)
                {
                    ((CheckedComboBoxEdit)(Control)sender).ShowPopup();
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
                            dgvMFGRejAvgRate.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvMFGRejAvgRate.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvMFGRejAvgRate.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvMFGRejAvgRate.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvMFGRejAvgRate.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvMFGRejAvgRate.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvMFGRejAvgRate.ExportToCsv(Filepath);
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
        private bool ClearDetails()
        {
            bool blnReturn = true;
            try
            {
                grdMFGRejAvgRate.DataSource = null;
                txtCarat.Text = "0";
                txtRate.Text = "0";
                txtAmount.Text = "0";
                txtRoughCutNo.Text = string.Empty;
                txtCutCarat.Text = "0";
                txtCutRate.Text = "0";
                txtCutAmount.Text = "0";
                lueKapan.EditValue = null;
                lblLotSRNo.Text = "0";
                btnShow.Enabled = true;
                lueKapan.Focus();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
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
        public void SetControlPropertyValue(Control oControl, string propName, object propValue)
        {
            if (oControl.InvokeRequired)
            {
                SetControlValueCallback d = new SetControlValueCallback(SetControlPropertyValue);
                oControl.Invoke(d, new object[]
                        {
                            oControl,
                            propName,
                            propValue
                        });
            }
            else
            {
                Type t = oControl.GetType();
                PropertyInfo[] props = t.GetProperties();
                foreach (PropertyInfo p in props)
                {
                    if ((p.Name.ToUpper() == propName.ToUpper()))
                    {
                        p.SetValue(oControl, propValue, null);
                    }
                }
            }
        }
        private void FrmMFGRejectionAvgRate_Load(object sender, EventArgs e)
        {
            try
            {
                m_dtbKapan = objRoughStockEntry.Kapan_GetData();

                lueKapan.Properties.DataSource = m_dtbKapan;
                lueKapan.Properties.ValueMember = "kapan_id";
                lueKapan.Properties.DisplayMember = "kapan_no";

                dtpEntryDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpEntryDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpEntryDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpEntryDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpEntryDate.EditValue = DateTime.Now;

                lueKapan.Focus();
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void backgroundWorker_RejectionAvgRate_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                MFGRejectionAvgRate MFGRejectionAvgRate = new MFGRejectionAvgRate();
                MFGRejectionAvgRateProperty objMFGRejectionAvgRateProperty = new MFGRejectionAvgRateProperty();

                Conn = new BeginTranConnection(true, false);

                DataTable m_DTab = new DataTable();
                ArrayList MyArrayList = new ArrayList();

                IntRes = 0;
                try
                {
                    m_DTab = (DataTable)grdMFGRejAvgRate.DataSource;

                    objMFGRejectionAvgRateProperty.kapan_id = Val.ToInt64(lueKapan.EditValue);
                    objMFGRejectionAvgRateProperty.date = Val.DBDate(dtpEntryDate.Text);
                    objMFGRejectionAvgRateProperty.rough_cut_no = Val.ToString(txtRoughCutNo.Text);

                    if (m_DTab.Rows.Count > 0)
                    {
                        int IntCounter = 0;
                        int Count = 0;
                        int TotalCount = m_DTab.Rows.Count;
                        for (int i = 0; i <= m_DTab.Rows.Count - 1; i++)
                        {
                            if (Val.ToDecimal(m_DTab.Rows[i]["carat"]) > 0 && Val.ToDecimal(m_DTab.Rows[i]["rate"]) > 0)
                            {
                                objMFGRejectionAvgRateProperty.purity_id = Val.ToInt64(m_DTab.Rows[i]["purity_id"]);
                                objMFGRejectionAvgRateProperty.carat = Val.ToDecimal(m_DTab.Rows[i]["carat"]);
                                objMFGRejectionAvgRateProperty.rate = Val.ToDecimal(m_DTab.Rows[i]["rate"]);
                                objMFGRejectionAvgRateProperty.amount = Val.ToDecimal(m_DTab.Rows[i]["amount"]);

                                objMFGRejectionAvgRateProperty = MFGRejectionAvgRate.Save(objMFGRejectionAvgRateProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                            }
                            Count++;
                            IntCounter++;
                            IntRes++;
                            SetControlPropertyValue(lblProgressCount, "Text", Count.ToString() + "" + "/" + "" + TotalCount.ToString() + " Completed....");
                        }
                    }
                    Conn.Inter1.Commit();
                }
                catch (Exception ex)
                {
                    IntRes = -1;
                    Conn.Inter1.Rollback();
                    Conn = null;
                    General.ShowErrors(ex.ToString());
                    return;
                }
            }
            catch (Exception ex)
            {
                IntRes = -1;
                Conn.Inter1.Rollback();
                Conn = null;
                Global.Message(ex.ToString());
                if (ex.InnerException != null)
                {
                    Global.Message(ex.InnerException.ToString());
                }
            }
        }
        private void backgroundWorker_RejectionAvgRate_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                panelProgress.Visible = false;

                if (IntRes > 0)
                {
                    DialogResult result = MessageBox.Show("Rejection Avg Rate Entry Save Succesfully");
                    ClearDetails();
                }
                else
                {
                    Global.Confirm("Error in Rejection Avg Rate Entry Data");
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }

        private void txtCutCarat_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtCutAmount.Text = string.Format("{0:0.00}", Val.ToDecimal(txtCutCarat.Text) * Val.ToDecimal(txtCutRate.Text));
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }

        private void dgvMFGRejAvgRate_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            try
            {
                DataTable dtAmount = new DataTable();
                dtAmount = (DataTable)grdMFGRejAvgRate.DataSource;

                decimal rate = 0;
                decimal carat = 0;
                decimal amount = 0;

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
                }


                GridView view = sender as GridView;

                if (dtAmount.Rows.Count > 0)
                {
                    if (e.SummaryProcess == CustomSummaryProcess.Start)
                    {
                        TotalCarat = 0;
                        TotalAmount = 0;
                    }
                    else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                    {
                        TotalCarat = TotalCarat + Val.ToDecimal(dgvMFGRejAvgRate.GetRowCellValue(e.RowHandle, "carat"));
                        TotalAmount = TotalAmount + Val.ToDecimal(dgvMFGRejAvgRate.GetRowCellValue(e.RowHandle, "amount"));
                    }
                    else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                    {
                        if (((GridSummaryItem)e.Item).FieldName.CompareTo("rate") == 0)
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
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }

        private void dgvMFGRejAvgRate_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;

                if (view.FocusedColumn.FieldName.Contains("carat"))
                {
                    double carat = 0.000;
                    if (!double.TryParse(e.Value as string, out carat))
                    {
                        e.Valid = false;
                        e.ErrorText = "Input string was not in a correct format.";
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
    }
}
