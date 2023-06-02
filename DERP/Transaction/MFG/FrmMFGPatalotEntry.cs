using BLL;
using BLL.FunctionClasses.Master.MFG;
using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.PropertyClasses.Report;
using BLL.PropertyClasses.Transaction;
using BLL.PropertyClasses.Transaction.MFG;
using DERP.Class;
using DERP.Report;
using DevExpress.Data;
using DevExpress.Diagram.Core;
using DevExpress.XtraEditors;
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
    public partial class FrmMFGPatalotEntry : DevExpress.XtraEditors.XtraForm
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
        //DataTable DTab_KapanWiseData = new DataTable();
        public static readonly DiagramCommand SaveFileCommand;
        Control _NextEnteredControl;
        private List<Control> _tabControls;

        public New_Report_DetailProperty ObjReportDetailProperty;
        FillCombo ObjFillCombo = new FillCombo();
        DataTable DtControlSettings;
        DataTable dtTemp;
        DataTable m_dtbParam;
        DataTable m_dtCut;
        DataTable m_DtProcess;
        DataTable m_dtbKapan;
        DataTable m_dtOutstanding;
        DataTable DtPataLotEntry = new DataTable();
        DataTable DTabQuality = new DataTable();
        DataTable DtPataLot = new DataTable();

        Int64 m_numForm_id;
        Int64 IntRes;
        Int64 Lot_SrNo = 0;
        int m_IsLot;

        string StrListTempPurity = string.Empty;

        #endregion

        #region Constructor
        public FrmMFGPatalotEntry()
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

            if (Global.HideFormControls(Val.ToInt(ObjPer.form_id), this) != "")
            {
                Global.Message("Select First User Setting...Please Contact to Administrator...");
                return;
            }

            ControlSettingDT(Val.ToInt(ObjPer.form_id), this);
            AddGotFocusListener(this);
            //AddKeyPressListener(this);
            this.KeyPreview = true;

            TabControlsToList(this.Controls);
            _tabControls = _tabControls.OrderBy(x => x.TabIndex).ToList();

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
                if (Val.DBDate(BLL.GlobalDec.gStrServerDate) != Val.DBDate(dtpMixDate.Text))
                {
                    Str = GlobalDec.CheckLockIsOpenOrNot(Val.DBDate(dtpMixDate.Text), Val.DBTime(DateTime.Now.ToShortTimeString())); //Val.GetFullTime12());
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
                        dtpMixDate.Enabled = true;
                        dtpMixDate.Visible = true;
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
                DialogResult result = MessageBox.Show("Do you want to save PataLot Entry data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result != DialogResult.Yes)
                {
                    btnSave.Enabled = true;
                    return;
                }

                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;

                panelProgress.Visible = true;
                backgroundWorker_PataLotEntry.RunWorkerAsync();

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
                DataTable DTab_List = new DataTable();
                MFGPataLotEntry objMFGPataLotEntry = new MFGPataLotEntry();
                MFGPataLotEntryProperty objMFGPataLotEntryProperty = new MFGPataLotEntryProperty();
                objMFGPataLotEntryProperty.from_date = Val.DBDate(DateTime.Now.ToShortDateString());
                objMFGPataLotEntryProperty.to_date = Val.DBDate(DateTime.Now.ToShortDateString());

                DTab_List = objMFGPataLotEntry.GetPendingStock(objMFGPataLotEntryProperty);

                FrmMFGPataLotEntryStock FrmMFGPataLotEntryStock = new FrmMFGPataLotEntryStock();
                FrmMFGPataLotEntryStock.FrmMFGPatalotEntry = this;
                FrmMFGPataLotEntryStock.DTab = DtPataLot;
                FrmMFGPataLotEntryStock.ShowForm(this, DTab_List);
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
            }
        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (Val.ToInt(lblLotSRNo.Text) != 0)
            {
                DialogResult result = MessageBox.Show("Do you want to Delete Pata Lot Entry Data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result != DialogResult.Yes)
                {
                    btnSave.Enabled = true;
                    return;
                }

                IntRes = 0;

                MFGPataLotEntry MFGPataLotEntry = new MFGPataLotEntry();
                MFGPataLotEntryProperty objMFGPataLotEntryProperty = new MFGPataLotEntryProperty();

                objMFGPataLotEntryProperty.lot_srno = Val.ToInt64(lblLotSRNo.Text);

                IntRes = MFGPataLotEntry.GetDeletePataLotEntry(objMFGPataLotEntryProperty);

                if (IntRes > 0)
                {
                    Global.Confirm("Pata Lot Entry Data Deleted Succesfully");
                    ClearDetails();
                    btnSave.Enabled = true;
                }
                else
                {
                    Global.Confirm("Error In Pata Lot Entry Data");
                    btnSave.Enabled = true;
                }
            }
            else
            {
                Global.Confirm("Not Selected Any Data are Deleted..");
                btnSave.Enabled = true;
                return;
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateDetails())
                {
                    return;
                }

                MFGPataLotEntry objMFGPataLotEntry = new MFGPataLotEntry();
                MFGPataLotEntryProperty objMFGPataLotEntryProperty = new MFGPataLotEntryProperty();
                objMFGPataLotEntryProperty.rough_cut_id = Val.ToInt64(lueRoughCutNo.EditValue);
                objMFGPataLotEntryProperty.kapan_id = Val.ToInt64(lueKapan.EditValue);
                objMFGPataLotEntryProperty.lot_srno = Val.ToInt64(lblLotSRNo.Text);

                DtPataLotEntry = objMFGPataLotEntry.MFGPataLotEntryGetData(objMFGPataLotEntryProperty);
                lblLotSRNo.Text = "0";

                //if (lblLotSRNo.Text != "0")
                //{
                //    txtTotalCarat.Text = Val.ToDecimal(DtPataLotEntry.Rows[0]["total_carat"]).ToString();
                //    txtTotalPcs.Text = Val.ToInt64(DtPataLotEntry.Rows[0]["total_pcs"]).ToString();
                //}
                grdPataLotEntry.DataSource = DtPataLotEntry;
                dgvPataLotEntry.OptionsView.ShowFooter = true;
                dgvPataLotEntry.BestFitColumns();
                dgvPataLotEntry.FocusedColumn = dgvPataLotEntry.Columns["1_carat"];
                dgvPataLotEntry.ShowEditor();
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
            }
        }
        private void lueKapan_EditValueChanged(object sender, EventArgs e)
        {
            if (m_IsLot == 0)
            {
                m_dtbParam = new DataTable();
                if (lueKapan.Text.ToString() != "")
                {
                    m_dtbParam = Global.GetRoughKapanWise_Data(Val.ToInt(lueKapan.EditValue));
                }
                lueRoughCutNo.Properties.DataSource = m_dtbParam;
                lueRoughCutNo.Properties.ValueMember = "rough_cut_id";
                lueRoughCutNo.Properties.DisplayMember = "rough_cut_no";
            }
        }

        private void grdProcessReceive_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle++;
                    e.Handled = true;
                }
            }
            catch { }
        }
        private void dgvProcessReceive_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            //try
            //{
            //    DataTable dtAmount = new DataTable();
            //    dtAmount = (DataTable)grdPataLotEntry.DataSource;
            //    string[] col = e.Column.FieldName.Split('_');
            //    string currcol = "";
            //    if (col.Length > 3 && e.Column.FieldName.Contains("_") && col != null)
            //    {
            //        currcol = col[0] + "_" + col[1];
            //    }
            //    decimal carat = 0;
            //    decimal total = 0;
            //    decimal perTotal = 0;
            //    string colname = "";
            //    for (int i = 0; i <= dtAmount.Rows.Count - 1; i++)
            //    {
            //        for (int j = 0; j <= dtAmount.Columns.Count - 1; j++)
            //        {

            //            if (dtAmount.Columns[j].ToString().Contains("carat") && dtAmount.Columns[j].ColumnName.Contains(currcol))
            //            {
            //                perTotal = 0;
            //                carat = Val.ToDecimal(dtAmount.Rows[i][j]);
            //                total += carat;
            //                perTotal = carat;
            //                //Total_Amount = Total_Amount + Val.ToDecimal(dtAmount.Rows[i][j]);
            //                colname = currcol;
            //            }
            //            //if (dtAmount.Columns[j].ToString().Contains("per(%)") && dtAmount.Columns[j].ColumnName.Contains(colname))
            //            //{
            //            //    //if (Val.ToDecimal(lblOsCarat.Text) > 0)
            //            //    //{
            //            //    //    Percent = (perTotal * 100) / Val.ToDecimal(lblOsCarat.Text);
            //            //    //    dtAmount.Rows[i][j] = Math.Round(Percent, 2).ToString();
            //            //    //}
            //            //    //else
            //            //    //{
            //            //    //    dtAmount.Rows[i][j] = 0;
            //            //    //}
            //            //    if (Val.ToDecimal(txtCarat.Text) > 0)
            //            //    {
            //            //        Percent = (perTotal * 100) / Val.ToDecimal(txtCarat.Text);
            //            //        dtAmount.Rows[i][j] = Math.Round(Percent, 2).ToString();
            //            //    }
            //            //    else
            //            //    {
            //            //        dtAmount.Rows[i][j] = 0;
            //            //    }

            //            //}

            //            //if (dtAmount.Columns[j].ToString().Contains("carat"))
            //            //{
            //            //    carat = dtAmount.AsEnumerable().Sum(x => Val.ToDecimal(x[dtAmount.Columns[j]]));
            //            //}

            //            if (dtAmount.Columns[j].ColumnName.Contains("Total"))
            //            {
            //                dtAmount.Rows[i][j] = Math.Round(total, 3).ToString();

            //                //perTotal = carat;
            //                //colname = currcol;
            //                //break;
            //            }
            //            //if (dtAmount.Columns[j].ColumnName.Contains("Total%"))
            //            //{
            //            //    //if (Val.ToDecimal(lblOsCarat.Text) > 0)
            //            //    //{
            //            //    //    Percent = (total * 100) / Val.ToDecimal(lblOsCarat.Text);
            //            //    //    dtAmount.Rows[i][j] = Math.Round(Percent, 2).ToString();
            //            //    //}
            //            //    //else
            //            //    //{
            //            //    //    dtAmount.Rows[i][j] = 0;
            //            //    //}
            //            //    //break;
            //            //    if (Val.ToDecimal(txtCarat.Text) > 0)
            //            //    {
            //            //        Percent = (total * 100) / Val.ToDecimal(txtCarat.Text);
            //            //        dtAmount.Rows[i][j] = Math.Round(Percent, 2).ToString();
            //            //    }
            //            //    else
            //            //    {
            //            //        dtAmount.Rows[i][j] = 0;
            //            //    }
            //            //    break;
            //            //}

            //        }
            //        total = 0;
            //        dtAmount.AcceptChanges();
            // dgvPataLotEntry.BestFitColumns();
            //    }
            //    //CalculateTotal();
            //}
            //catch (Exception ex)
            //{
            //    BLL.General.ShowErrors(ex);
            //}
        }
        private void dgvProcessReceive_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
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
        private void FrmMFGAssortFirst_Load(object sender, EventArgs e)
        {
            try
            {
                m_dtbKapan = Global.GetKapanAll_Assort();

                lueKapan.Properties.DataSource = m_dtbKapan;
                lueKapan.Properties.ValueMember = "kapan_id";
                lueKapan.Properties.DisplayMember = "kapan_no";

                m_dtCut = Global.GetRoughCutAll();
                lueRoughCutNo.Properties.DataSource = m_dtCut;
                lueRoughCutNo.Properties.ValueMember = "rough_cut_id";
                lueRoughCutNo.Properties.DisplayMember = "rough_cut_no";

                dtpMixDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpMixDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpMixDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpMixDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpMixDate.EditValue = DateTime.Now;

                m_dtbParam = Global.GetRoughCutAll();

                lueKapan.Focus();
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
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
        private void dgvProcessReceive_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            try
            {
                DataTable dtAmount = new DataTable();
                dtAmount = (DataTable)grdPataLotEntry.DataSource;

                decimal percentage = 0;
                decimal totcarat = 0;
                string column = "";
                for (int j = 0; j <= dtAmount.Columns.Count - 1; j++)
                {
                    if (dtAmount.Columns[j].ToString().Contains("per(%)"))
                    {
                        column = dtAmount.Columns[j].ToString();

                    }
                    if (dtAmount.Columns[j].ColumnName.Contains("carat"))
                    {
                        totcarat = dtAmount.AsEnumerable().Sum(x => Val.ToDecimal(x[dtAmount.Columns[j]]));
                    }
                    if (totcarat > 0 && Val.ToDecimal(txtTotalCarat.Text) > 0)
                    {
                        if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == column)
                        {
                            percentage = Math.Round(totcarat * 100 / Val.ToDecimal(txtTotalCarat.Text), 3);
                            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                                e.TotalValue = percentage;
                            column = "";
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
                if (lueRoughCutNo.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Rough Cut No"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueRoughCutNo.Focus();
                    }
                }
                if (txtTotalCarat.Text.ToString() == "" || txtTotalCarat.Text.ToString() == "0")
                {
                    lstError.Add(new ListError(12, "Carat"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtTotalCarat.Focus();
                    }
                }
                if (txtTotalPcs.Text.ToString() == "" || txtTotalPcs.Text.ToString() == "0")
                {
                    lstError.Add(new ListError(12, "Pcs"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtTotalPcs.Focus();
                    }
                }
                DateTime endDate = Convert.ToDateTime(DateTime.Today);
                endDate = endDate.AddDays(3);

                if (Convert.ToDateTime(dtpMixDate.Text) >= endDate)
                {
                    lstError.Add(new ListError(5, " Mix Date Not Be Permission After 3 Days in this Rough Cut No...Please Contact to Administrator"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        dtpMixDate.Focus();
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
                            dgvPataLotEntry.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvPataLotEntry.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvPataLotEntry.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvPataLotEntry.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvPataLotEntry.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvPataLotEntry.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvPataLotEntry.ExportToCsv(Filepath);
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
        private bool Validate_PopUp()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();

            try
            {
                if (lueKapan.Text == "")
                {
                    lstError.Add(new ListError(13, "Kapan No"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueKapan.Focus();
                    }
                }
                if (lueRoughCutNo.Text == "")
                {
                    lstError.Add(new ListError(13, "Cut No"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueRoughCutNo.Focus();
                    }
                }
            }

            catch (Exception ex)
            {
                lstError.Add(new ListError(ex));
            }
            return (!(BLL.General.ShowErrors(lstError)));
        }
        public void GetStockData(DataTable Stock_Data)
        {
            try
            {
                DataTable DTab_PataLot = Stock_Data.Copy();

                lblLotSRNo.Text = Val.ToString(Val.ToDecimal(DTab_PataLot.Rows[0]["lot_srno"]));
                lueKapan.EditValue = Val.ToInt64(DTab_PataLot.Rows[0]["kapan_id"]);
                lueRoughCutNo.EditValue = Val.ToInt64(DTab_PataLot.Rows[0]["rough_cut_id"]);
                txtTotalPcs.Text = Val.ToDecimal(DTab_PataLot.Rows[0]["total_pcs"]).ToString();
                txtTotalCarat.Text = Val.ToDecimal(DTab_PataLot.Rows[0]["total_carat"]).ToString();
                dtpMixDate.Text = Val.DBDate(DTab_PataLot.Rows[0]["mix_date"].ToString());

                MFGPataLotEntry objMFGPataLotEntry = new MFGPataLotEntry();
                MFGPataLotEntryProperty objMFGPataLotEntryProperty = new MFGPataLotEntryProperty();
                objMFGPataLotEntryProperty.rough_cut_id = Val.ToInt64(lueRoughCutNo.EditValue);
                objMFGPataLotEntryProperty.kapan_id = Val.ToInt64(lueKapan.EditValue);
                objMFGPataLotEntryProperty.lot_srno = Val.ToInt64(lblLotSRNo.Text);

                DtPataLotEntry = objMFGPataLotEntry.MFGPataLotEntryGetDataList(objMFGPataLotEntryProperty);

                grdPataLotEntry.DataSource = DtPataLotEntry;
                dgvPataLotEntry.OptionsView.ShowFooter = true;
                dgvPataLotEntry.BestFitColumns();
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
            }
        }
        private bool ClearDetails()
        {
            bool blnReturn = true;
            try
            {
                //lueKapan.EditValue = null;
                //lueRoughCutNo.EditValue = null;
                grdPataLotEntry.DataSource = null;
                txtTotalPcs.Text = "0";
                txtTotalCarat.Text = "0";
                lblLotSRNo.Text = "0";
                btnSearch.Enabled = true;
                txtTotalPcs.Focus();
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

        private void backgroundWorker_PataLotEntry_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                MFGPataLotEntry MFGPataLotEntry = new MFGPataLotEntry();
                MFGPataLotEntryProperty objMFGPataLotEntryProperty = new MFGPataLotEntryProperty();

                if (txtTotalCarat.Text.ToString() == "" || txtTotalCarat.Text.ToString() == "0")
                {
                    grdPataLotEntry.DataSource = null;
                    Global.Message("Carat cannot be blank.");
                    return;
                }

                Conn = new BeginTranConnection(true, false);

                DataTable m_DTab = new DataTable();
                ArrayList MyArrayList = new ArrayList();

                IntRes = 0;
                Lot_SrNo = 0;
                try
                {
                    m_DTab = (DataTable)grdPataLotEntry.DataSource;

                    objMFGPataLotEntryProperty.kapan_id = Val.ToInt(lueKapan.EditValue);
                    objMFGPataLotEntryProperty.rough_cut_id = Val.ToInt(lueRoughCutNo.EditValue);
                    objMFGPataLotEntryProperty.date = Val.DBDate(dtpMixDate.Text);

                    objMFGPataLotEntryProperty.carat = Val.ToDecimal(txtTotalCarat.Text);
                    objMFGPataLotEntryProperty.total_pcs = Val.ToInt(txtTotalPcs.Text);

                    if (lblLotSRNo.Text.ToString() != "0")
                    {
                        Lot_SrNo = Val.ToInt64(lblLotSRNo.Text);
                    }
                    else
                    {
                        Lot_SrNo = 0;
                    }

                    if (m_DTab.Rows.Count > 0)
                    {
                        int IntCounter = 0;
                        int Count = 0;
                        int TotalCount = m_DTab.Rows.Count;
                        for (int i = 0; i <= m_DTab.Rows.Count - 1; i++)
                        {
                            objMFGPataLotEntryProperty.lot_srno = Lot_SrNo;

                            objMFGPataLotEntryProperty.mix_id = Val.ToInt64(m_DTab.Rows[i]["mix_id"]);
                            objMFGPataLotEntryProperty.k_process_id = Val.ToInt64(m_DTab.Rows[i]["k_process_id"]);
                            objMFGPataLotEntryProperty.t_1_per = Val.ToDecimal(m_DTab.Rows[i]["1_per"]);
                            objMFGPataLotEntryProperty.t_1_carat = Val.ToDecimal(m_DTab.Rows[i]["1_carat"]);

                            objMFGPataLotEntryProperty.t_2_per = Val.ToDecimal(m_DTab.Rows[i]["2_per"]);
                            objMFGPataLotEntryProperty.t_2_carat = Val.ToDecimal(m_DTab.Rows[i]["2_carat"]);

                            objMFGPataLotEntryProperty.t_3_per = Val.ToDecimal(m_DTab.Rows[i]["3_per"]);
                            objMFGPataLotEntryProperty.t_3_carat = Val.ToDecimal(m_DTab.Rows[i]["3_carat"]);

                            objMFGPataLotEntryProperty.t_4_per = Val.ToDecimal(m_DTab.Rows[i]["4_per"]);
                            objMFGPataLotEntryProperty.t_4_carat = Val.ToDecimal(m_DTab.Rows[i]["4_carat"]);

                            objMFGPataLotEntryProperty.t_5_per = Val.ToDecimal(m_DTab.Rows[i]["5_per"]);
                            objMFGPataLotEntryProperty.t_5_carat = Val.ToDecimal(m_DTab.Rows[i]["5_carat"]);

                            objMFGPataLotEntryProperty.t_6_per = Val.ToDecimal(m_DTab.Rows[i]["6_per"]);
                            objMFGPataLotEntryProperty.t_6_carat = Val.ToDecimal(m_DTab.Rows[i]["6_carat"]);

                            objMFGPataLotEntryProperty.t_7_per = Val.ToDecimal(m_DTab.Rows[i]["7_per"]);
                            objMFGPataLotEntryProperty.t_7_carat = Val.ToDecimal(m_DTab.Rows[i]["7_carat"]);

                            objMFGPataLotEntryProperty.t_8_per = Val.ToDecimal(m_DTab.Rows[i]["8_per"]);
                            objMFGPataLotEntryProperty.t_8_carat = Val.ToDecimal(m_DTab.Rows[i]["8_carat"]);

                            objMFGPataLotEntryProperty.t_9_per = Val.ToDecimal(m_DTab.Rows[i]["9_per"]);
                            objMFGPataLotEntryProperty.t_9_carat = Val.ToDecimal(m_DTab.Rows[i]["9_carat"]);

                            objMFGPataLotEntryProperty.t_10_per = Val.ToDecimal(m_DTab.Rows[i]["10_per"]);
                            objMFGPataLotEntryProperty.t_10_carat = Val.ToDecimal(m_DTab.Rows[i]["10_carat"]);

                            objMFGPataLotEntryProperty.t_11_per = Val.ToDecimal(m_DTab.Rows[i]["11_per"]);
                            objMFGPataLotEntryProperty.t_11_carat = Val.ToDecimal(m_DTab.Rows[i]["11_carat"]);

                            objMFGPataLotEntryProperty.t_12_per = Val.ToDecimal(m_DTab.Rows[i]["12_per"]);
                            objMFGPataLotEntryProperty.t_12_carat = Val.ToDecimal(m_DTab.Rows[i]["12_carat"]);

                            objMFGPataLotEntryProperty.t_13_per = Val.ToDecimal(m_DTab.Rows[i]["13_per"]);
                            objMFGPataLotEntryProperty.t_13_carat = Val.ToDecimal(m_DTab.Rows[i]["13_carat"]);

                            objMFGPataLotEntryProperty.t_14_per = Val.ToDecimal(m_DTab.Rows[i]["14_per"]);
                            objMFGPataLotEntryProperty.t_14_carat = Val.ToDecimal(m_DTab.Rows[i]["14_carat"]);

                            objMFGPataLotEntryProperty.t_15_per = Val.ToDecimal(m_DTab.Rows[i]["15_per"]);
                            objMFGPataLotEntryProperty.t_15_carat = Val.ToDecimal(m_DTab.Rows[i]["15_carat"]);

                            objMFGPataLotEntryProperty.t_16_per = Val.ToDecimal(m_DTab.Rows[i]["16_per"]);
                            objMFGPataLotEntryProperty.t_16_carat = Val.ToDecimal(m_DTab.Rows[i]["16_carat"]);

                            objMFGPataLotEntryProperty.t_17_per = Val.ToDecimal(m_DTab.Rows[i]["17_per"]);
                            objMFGPataLotEntryProperty.t_17_carat = Val.ToDecimal(m_DTab.Rows[i]["17_carat"]);

                            objMFGPataLotEntryProperty.t_18_per = Val.ToDecimal(m_DTab.Rows[i]["18_per"]);
                            objMFGPataLotEntryProperty.t_18_carat = Val.ToDecimal(m_DTab.Rows[i]["18_carat"]);

                            objMFGPataLotEntryProperty.t_19_per = Val.ToDecimal(m_DTab.Rows[i]["19_per"]);
                            objMFGPataLotEntryProperty.t_19_carat = Val.ToDecimal(m_DTab.Rows[i]["19_carat"]);

                            objMFGPataLotEntryProperty.t_19_per = Val.ToDecimal(m_DTab.Rows[i]["20_per"]);
                            objMFGPataLotEntryProperty.t_19_carat = Val.ToDecimal(m_DTab.Rows[i]["20_carat"]);

                            objMFGPataLotEntryProperty = MFGPataLotEntry.Save(objMFGPataLotEntryProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                            Lot_SrNo = Val.ToInt64(objMFGPataLotEntryProperty.lot_srno);

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
        private void backgroundWorker_PataLotEntry_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                panelProgress.Visible = false;

                if (IntRes > 0)
                {
                    DialogResult result = MessageBox.Show("PataLot Entry Save Succesfully and Lot SrNo is : " + Lot_SrNo + " Are you sure print this Lot PataEntry Report?", "Confirmation", MessageBoxButtons.YesNoCancel);
                    if (result != DialogResult.Yes)
                    {
                        btnSave.Enabled = true;
                        ClearDetails();
                        return;
                    }

                    DataTable DtPataLotPrintData = new DataTable();
                    MFGPataLotEntry objMFGPataLotEntry = new MFGPataLotEntry();
                    MFGPataLotEntryProperty objMFGPataLotEntryProperty = new MFGPataLotEntryProperty();

                    objMFGPataLotEntryProperty.rough_cut_id = Val.ToInt64(lueRoughCutNo.EditValue);
                    objMFGPataLotEntryProperty.kapan_id = Val.ToInt64(lueKapan.EditValue);
                    objMFGPataLotEntryProperty.lot_srno_list = Val.ToString(Lot_SrNo);

                    DtPataLotPrintData = objMFGPataLotEntry.MFGPataLotEntryPrintGetData(objMFGPataLotEntryProperty);

                    decimal p_numLoning_Per = 0;
                    decimal p_numMathala_Weight = 0;
                    decimal p_numKachha_Weight = 0;

                    foreach (DataRow Drw in DtPataLotPrintData.Rows)
                    {
                        Drw["k_per"] = 0;
                        if (Val.ToDecimal(Drw["k_carat"]) > 0)
                        {
                            Drw["koning_per"] = Math.Round((Val.ToDecimal(Drw["koning_carat"]) / Val.ToDecimal(Drw["k_carat"])) * 100, 2);
                        }
                        if (Val.ToDecimal(Drw["koning_carat"]) > 0)
                        {
                            Drw["tal_per"] = Math.Round((Val.ToDecimal(Drw["tal_carat"]) / Val.ToDecimal(Drw["koning_carat"])) * 100, 2);
                        }
                        if (Val.ToDecimal(Drw["tal_carat"]) > 0)
                        {
                            Drw["pel_per"] = Math.Round((Val.ToDecimal(Drw["pel_carat"]) / Val.ToDecimal(Drw["tal_carat"])) * 100, 2);
                        }
                        if (Val.ToDecimal(Drw["pel_carat"]) > 0)
                        {
                            Drw["matala_per"] = Math.Round((Val.ToDecimal(Drw["matala_carat"]) / Val.ToDecimal(Drw["pel_carat"])) * 100, 2);
                        }
                        p_numLoning_Per = Val.ToDecimal(Drw["koning_carat"]);
                        p_numMathala_Weight = Val.ToDecimal(Drw["matala_carat"]);
                        p_numKachha_Weight = Val.ToDecimal(Drw["k_carat"]);

                        if (p_numLoning_Per > 0)
                        {
                            Drw["pata_ok_per"] = Val.ToString(Math.Round((Val.ToDecimal(Drw["t_wt"]) / p_numLoning_Per) * 100, 2));
                        }
                        if (p_numKachha_Weight > 0)
                        {
                            Drw["total_per"] = Val.ToString(Math.Round((Val.ToDecimal(p_numMathala_Weight) / p_numKachha_Weight) * 100, 2));
                        }
                    }

                    //foreach (DataRow Drw in DtPataLotPrintData.Tables[1].Rows)
                    //{
                    //    if (p_numLoning_Per > 0)
                    //    {
                    //        Drw["pata_ok_per"] = Val.ToString(Math.Round((Val.ToDecimal(Drw["t_wt"]) / p_numLoning_Per) * 100, 2));
                    //    }
                    //    if (p_numKachha_Weight > 0)
                    //    {
                    //        Drw["total_per"] = Val.ToString(Math.Round((Val.ToDecimal(p_numMathala_Weight) / p_numKachha_Weight) * 100, 2));
                    //    }
                    //}

                    ClearDetails();

                    FrmReportViewer FrmReportViewer = new FrmReportViewer();
                    FrmReportViewer.DS.Tables.Add(DtPataLotPrintData);
                    FrmReportViewer.GroupBy = "";
                    FrmReportViewer.RepName = "";
                    FrmReportViewer.RepPara = "";
                    this.Cursor = Cursors.Default;
                    FrmReportViewer.AllowSetFormula = true;

                    FrmReportViewer.ShowForm("PataLot_Entry_Part2", 120, FrmReportViewer.ReportFolder.PATA_LOT_ENTRY);

                    DtPataLotPrintData = null;
                    FrmReportViewer.DS.Tables.Clear();
                    FrmReportViewer.DS.Clear();
                    FrmReportViewer = null;
                }
                else
                {
                    Global.Confirm("Error in PataLot Entry Data");
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }
        private void rep_txt_1_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit textEditor = (TextEdit)sender;

            int rowindex = dgvPataLotEntry.FocusedRowHandle;
            int RowNumber = dgvPataLotEntry.FocusedRowHandle;
            decimal Current_Carat = Val.ToDecimal(textEditor.EditValue);
            if (rowindex == 2)
            {
                decimal Previous_Carat = Val.ToDecimal(dgvPataLotEntry.GetRowCellValue(rowindex - 1, "1_carat"));
                dgvPataLotEntry.SetRowCellValue(rowindex, "1_per", Math.Round(Val.ToDecimal((Current_Carat / Previous_Carat) * 100), 2));
            }
            else if (rowindex == 3)
            {
                decimal Previous_Carat = Val.ToDecimal(dgvPataLotEntry.GetRowCellValue(rowindex - 2, "1_carat"));
                dgvPataLotEntry.SetRowCellValue(rowindex, "1_per", Math.Round(Val.ToDecimal((Current_Carat / Previous_Carat) * 100), 2));
            }
            else
            {
                dgvPataLotEntry.SetRowCellValue(rowindex, "1_per", 0);
            }
        }
        private void rep_txt_2_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit textEditor = (TextEdit)sender;

            int rowindex = dgvPataLotEntry.FocusedRowHandle;
            int RowNumber = dgvPataLotEntry.FocusedRowHandle;
            decimal Current_Carat = Val.ToDecimal(textEditor.EditValue);
            if (rowindex == 2)
            {
                decimal Previous_Carat = Val.ToDecimal(dgvPataLotEntry.GetRowCellValue(rowindex - 1, "2_carat"));
                dgvPataLotEntry.SetRowCellValue(rowindex, "2_per", Math.Round(Val.ToDecimal((Current_Carat / Previous_Carat) * 100), 2));
            }
            else if (rowindex == 3)
            {
                decimal Previous_Carat = Val.ToDecimal(dgvPataLotEntry.GetRowCellValue(rowindex - 2, "2_carat"));
                dgvPataLotEntry.SetRowCellValue(rowindex, "2_per", Math.Round(Val.ToDecimal((Current_Carat / Previous_Carat) * 100), 2));
            }
            else
            {
                dgvPataLotEntry.SetRowCellValue(rowindex, "2_per", 0);
            }
        }
        private void rep_txt_3_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit textEditor = (TextEdit)sender;

            int rowindex = dgvPataLotEntry.FocusedRowHandle;
            int RowNumber = dgvPataLotEntry.FocusedRowHandle;
            decimal Current_Carat = Val.ToDecimal(textEditor.EditValue);
            if (rowindex == 2)
            {
                decimal Previous_Carat = Val.ToDecimal(dgvPataLotEntry.GetRowCellValue(rowindex - 1, "3_carat"));
                dgvPataLotEntry.SetRowCellValue(rowindex, "3_per", Math.Round(Val.ToDecimal((Current_Carat / Previous_Carat) * 100), 2));
            }
            else if (rowindex == 3)
            {
                decimal Previous_Carat = Val.ToDecimal(dgvPataLotEntry.GetRowCellValue(rowindex - 2, "3_carat"));
                dgvPataLotEntry.SetRowCellValue(rowindex, "3_per", Math.Round(Val.ToDecimal((Current_Carat / Previous_Carat) * 100), 2));
            }
            else
            {
                dgvPataLotEntry.SetRowCellValue(rowindex, "3_per", 0);
            }
        }
        private void rep_txt_4_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit textEditor = (TextEdit)sender;

            int rowindex = dgvPataLotEntry.FocusedRowHandle;
            int RowNumber = dgvPataLotEntry.FocusedRowHandle;
            decimal Current_Carat = Val.ToDecimal(textEditor.EditValue);
            if (rowindex == 2)
            {
                decimal Previous_Carat = Val.ToDecimal(dgvPataLotEntry.GetRowCellValue(rowindex - 1, "4_carat"));
                dgvPataLotEntry.SetRowCellValue(rowindex, "4_per", Math.Round(Val.ToDecimal((Current_Carat / Previous_Carat) * 100), 2));
            }
            else if (rowindex == 3)
            {
                decimal Previous_Carat = Val.ToDecimal(dgvPataLotEntry.GetRowCellValue(rowindex - 2, "4_carat"));
                dgvPataLotEntry.SetRowCellValue(rowindex, "4_per", Math.Round(Val.ToDecimal((Current_Carat / Previous_Carat) * 100), 2));
            }
            else
            {
                dgvPataLotEntry.SetRowCellValue(rowindex, "4_per", 0);
            }
        }
        private void rep_txt_5_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit textEditor = (TextEdit)sender;

            int rowindex = dgvPataLotEntry.FocusedRowHandle;
            int RowNumber = dgvPataLotEntry.FocusedRowHandle;
            decimal Current_Carat = Val.ToDecimal(textEditor.EditValue);
            if (rowindex == 2)
            {
                decimal Previous_Carat = Val.ToDecimal(dgvPataLotEntry.GetRowCellValue(rowindex - 1, "5_carat"));
                dgvPataLotEntry.SetRowCellValue(rowindex, "5_per", Math.Round(Val.ToDecimal((Current_Carat / Previous_Carat) * 100), 2));
            }
            else if (rowindex == 3)
            {
                decimal Previous_Carat = Val.ToDecimal(dgvPataLotEntry.GetRowCellValue(rowindex - 2, "5_carat"));
                dgvPataLotEntry.SetRowCellValue(rowindex, "5_per", Math.Round(Val.ToDecimal((Current_Carat / Previous_Carat) * 100), 2));
            }
            else
            {
                dgvPataLotEntry.SetRowCellValue(rowindex, "5_per", 0);
            }
        }
        private void rep_txt_6_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit textEditor = (TextEdit)sender;

            int rowindex = dgvPataLotEntry.FocusedRowHandle;
            int RowNumber = dgvPataLotEntry.FocusedRowHandle;
            decimal Current_Carat = Val.ToDecimal(textEditor.EditValue);
            if (rowindex == 2)
            {
                decimal Previous_Carat = Val.ToDecimal(dgvPataLotEntry.GetRowCellValue(rowindex - 1, "6_carat"));
                dgvPataLotEntry.SetRowCellValue(rowindex, "6_per", Math.Round(Val.ToDecimal((Current_Carat / Previous_Carat) * 100), 2));
            }
            else if (rowindex == 3)
            {
                decimal Previous_Carat = Val.ToDecimal(dgvPataLotEntry.GetRowCellValue(rowindex - 2, "6_carat"));
                dgvPataLotEntry.SetRowCellValue(rowindex, "6_per", Math.Round(Val.ToDecimal((Current_Carat / Previous_Carat) * 100), 2));
            }
            else
            {
                dgvPataLotEntry.SetRowCellValue(rowindex, "6_per", 0);
            }
        }
        private void rep_txt_7_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit textEditor = (TextEdit)sender;

            int rowindex = dgvPataLotEntry.FocusedRowHandle;
            int RowNumber = dgvPataLotEntry.FocusedRowHandle;
            decimal Current_Carat = Val.ToDecimal(textEditor.EditValue);
            if (rowindex == 2)
            {
                decimal Previous_Carat = Val.ToDecimal(dgvPataLotEntry.GetRowCellValue(rowindex - 1, "7_carat"));
                dgvPataLotEntry.SetRowCellValue(rowindex, "7_per", Math.Round(Val.ToDecimal((Current_Carat / Previous_Carat) * 100), 2));
            }
            else if (rowindex == 3)
            {
                decimal Previous_Carat = Val.ToDecimal(dgvPataLotEntry.GetRowCellValue(rowindex - 2, "7_carat"));
                dgvPataLotEntry.SetRowCellValue(rowindex, "7_per", Math.Round(Val.ToDecimal((Current_Carat / Previous_Carat) * 100), 2));
            }
            else
            {
                dgvPataLotEntry.SetRowCellValue(rowindex, "7_per", 0);
            }
        }
        private void rep_txt_8_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit textEditor = (TextEdit)sender;

            int rowindex = dgvPataLotEntry.FocusedRowHandle;
            int RowNumber = dgvPataLotEntry.FocusedRowHandle;
            decimal Current_Carat = Val.ToDecimal(textEditor.EditValue);
            if (rowindex == 2)
            {
                decimal Previous_Carat = Val.ToDecimal(dgvPataLotEntry.GetRowCellValue(rowindex - 1, "8_carat"));
                dgvPataLotEntry.SetRowCellValue(rowindex, "8_per", Math.Round(Val.ToDecimal((Current_Carat / Previous_Carat) * 100), 2));
            }
            else if (rowindex == 3)
            {
                decimal Previous_Carat = Val.ToDecimal(dgvPataLotEntry.GetRowCellValue(rowindex - 2, "8_carat"));
                dgvPataLotEntry.SetRowCellValue(rowindex, "8_per", Math.Round(Val.ToDecimal((Current_Carat / Previous_Carat) * 100), 2));
            }
            else
            {
                dgvPataLotEntry.SetRowCellValue(rowindex, "8_per", 0);
            }
        }
        private void rep_txt_9_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit textEditor = (TextEdit)sender;

            int rowindex = dgvPataLotEntry.FocusedRowHandle;
            int RowNumber = dgvPataLotEntry.FocusedRowHandle;
            decimal Current_Carat = Val.ToDecimal(textEditor.EditValue);
            if (rowindex == 2)
            {
                decimal Previous_Carat = Val.ToDecimal(dgvPataLotEntry.GetRowCellValue(rowindex - 1, "9_carat"));
                dgvPataLotEntry.SetRowCellValue(rowindex, "9_per", Math.Round(Val.ToDecimal((Current_Carat / Previous_Carat) * 100), 2));
            }
            else if (rowindex == 3)
            {
                decimal Previous_Carat = Val.ToDecimal(dgvPataLotEntry.GetRowCellValue(rowindex - 2, "9_carat"));
                dgvPataLotEntry.SetRowCellValue(rowindex, "9_per", Math.Round(Val.ToDecimal((Current_Carat / Previous_Carat) * 100), 2));
            }
            else
            {
                dgvPataLotEntry.SetRowCellValue(rowindex, "9_per", 0);
            }
        }
        private void rep_txt_10_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit textEditor = (TextEdit)sender;

            int rowindex = dgvPataLotEntry.FocusedRowHandle;
            int RowNumber = dgvPataLotEntry.FocusedRowHandle;
            decimal Current_Carat = Val.ToDecimal(textEditor.EditValue);
            if (rowindex == 2)
            {
                decimal Previous_Carat = Val.ToDecimal(dgvPataLotEntry.GetRowCellValue(rowindex - 1, "10_carat"));
                dgvPataLotEntry.SetRowCellValue(rowindex, "10_per", Math.Round(Val.ToDecimal((Current_Carat / Previous_Carat) * 100), 2));
            }
            else if (rowindex == 3)
            {
                decimal Previous_Carat = Val.ToDecimal(dgvPataLotEntry.GetRowCellValue(rowindex - 2, "10_carat"));
                dgvPataLotEntry.SetRowCellValue(rowindex, "10_per", Math.Round(Val.ToDecimal((Current_Carat / Previous_Carat) * 100), 2));
            }
            else
            {
                dgvPataLotEntry.SetRowCellValue(rowindex, "10_per", 0);
            }
        }
        private void rep_txt_11_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit textEditor = (TextEdit)sender;

            int rowindex = dgvPataLotEntry.FocusedRowHandle;
            int RowNumber = dgvPataLotEntry.FocusedRowHandle;
            decimal Current_Carat = Val.ToDecimal(textEditor.EditValue);
            if (rowindex == 2)
            {
                decimal Previous_Carat = Val.ToDecimal(dgvPataLotEntry.GetRowCellValue(rowindex - 1, "11_carat"));
                dgvPataLotEntry.SetRowCellValue(rowindex, "11_per", Math.Round(Val.ToDecimal((Current_Carat / Previous_Carat) * 100), 2));
            }
            else if (rowindex == 3)
            {
                decimal Previous_Carat = Val.ToDecimal(dgvPataLotEntry.GetRowCellValue(rowindex - 2, "11_carat"));
                dgvPataLotEntry.SetRowCellValue(rowindex, "11_per", Math.Round(Val.ToDecimal((Current_Carat / Previous_Carat) * 100), 2));
            }
            else
            {
                dgvPataLotEntry.SetRowCellValue(rowindex, "11_per", 0);
            }
        }
        private void rep_txt_12_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit textEditor = (TextEdit)sender;

            int rowindex = dgvPataLotEntry.FocusedRowHandle;
            int RowNumber = dgvPataLotEntry.FocusedRowHandle;
            decimal Current_Carat = Val.ToDecimal(textEditor.EditValue);
            if (rowindex == 2)
            {
                decimal Previous_Carat = Val.ToDecimal(dgvPataLotEntry.GetRowCellValue(rowindex - 1, "12_carat"));
                dgvPataLotEntry.SetRowCellValue(rowindex, "12_per", Math.Round(Val.ToDecimal((Current_Carat / Previous_Carat) * 100), 2));
            }
            else if (rowindex == 3)
            {
                decimal Previous_Carat = Val.ToDecimal(dgvPataLotEntry.GetRowCellValue(rowindex - 2, "12_carat"));
                dgvPataLotEntry.SetRowCellValue(rowindex, "12_per", Math.Round(Val.ToDecimal((Current_Carat / Previous_Carat) * 100), 2));
            }
            else
            {
                dgvPataLotEntry.SetRowCellValue(rowindex, "12_per", 0);
            }
        }
        private void rep_txt_13_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit textEditor = (TextEdit)sender;

            int rowindex = dgvPataLotEntry.FocusedRowHandle;
            int RowNumber = dgvPataLotEntry.FocusedRowHandle;
            decimal Current_Carat = Val.ToDecimal(textEditor.EditValue);
            if (rowindex == 2)
            {
                decimal Previous_Carat = Val.ToDecimal(dgvPataLotEntry.GetRowCellValue(rowindex - 1, "13_carat"));
                dgvPataLotEntry.SetRowCellValue(rowindex, "13_per", Math.Round(Val.ToDecimal((Current_Carat / Previous_Carat) * 100), 2));
            }
            else if (rowindex == 3)
            {
                decimal Previous_Carat = Val.ToDecimal(dgvPataLotEntry.GetRowCellValue(rowindex - 2, "13_carat"));
                dgvPataLotEntry.SetRowCellValue(rowindex, "13_per", Math.Round(Val.ToDecimal((Current_Carat / Previous_Carat) * 100), 2));
            }
            else
            {
                dgvPataLotEntry.SetRowCellValue(rowindex, "13_per", 0);
            }
        }
        private void rep_txt_14_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit textEditor = (TextEdit)sender;

            int rowindex = dgvPataLotEntry.FocusedRowHandle;
            int RowNumber = dgvPataLotEntry.FocusedRowHandle;
            decimal Current_Carat = Val.ToDecimal(textEditor.EditValue);
            if (rowindex == 2)
            {
                decimal Previous_Carat = Val.ToDecimal(dgvPataLotEntry.GetRowCellValue(rowindex - 1, "14_carat"));
                dgvPataLotEntry.SetRowCellValue(rowindex, "14_per", Math.Round(Val.ToDecimal((Current_Carat / Previous_Carat) * 100), 2));
            }
            else if (rowindex == 3)
            {
                decimal Previous_Carat = Val.ToDecimal(dgvPataLotEntry.GetRowCellValue(rowindex - 2, "14_carat"));
                dgvPataLotEntry.SetRowCellValue(rowindex, "14_per", Math.Round(Val.ToDecimal((Current_Carat / Previous_Carat) * 100), 2));
            }
            else
            {
                dgvPataLotEntry.SetRowCellValue(rowindex, "14_per", 0);
            }
        }
        private void rep_txt_15_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit textEditor = (TextEdit)sender;

            int rowindex = dgvPataLotEntry.FocusedRowHandle;
            int RowNumber = dgvPataLotEntry.FocusedRowHandle;
            decimal Current_Carat = Val.ToDecimal(textEditor.EditValue);
            if (rowindex == 2)
            {
                decimal Previous_Carat = Val.ToDecimal(dgvPataLotEntry.GetRowCellValue(rowindex - 1, "15_carat"));
                dgvPataLotEntry.SetRowCellValue(rowindex, "15_per", Math.Round(Val.ToDecimal((Current_Carat / Previous_Carat) * 100), 2));
            }
            else if (rowindex == 3)
            {
                decimal Previous_Carat = Val.ToDecimal(dgvPataLotEntry.GetRowCellValue(rowindex - 2, "15_carat"));
                dgvPataLotEntry.SetRowCellValue(rowindex, "15_per", Math.Round(Val.ToDecimal((Current_Carat / Previous_Carat) * 100), 2));
            }
            else
            {
                dgvPataLotEntry.SetRowCellValue(rowindex, "15_per", 0);
            }
        }
        private void rep_txt_16_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit textEditor = (TextEdit)sender;

            int rowindex = dgvPataLotEntry.FocusedRowHandle;
            int RowNumber = dgvPataLotEntry.FocusedRowHandle;
            decimal Current_Carat = Val.ToDecimal(textEditor.EditValue);
            if (rowindex == 2)
            {
                decimal Previous_Carat = Val.ToDecimal(dgvPataLotEntry.GetRowCellValue(rowindex - 1, "16_carat"));
                dgvPataLotEntry.SetRowCellValue(rowindex, "16_per", Math.Round(Val.ToDecimal((Current_Carat / Previous_Carat) * 100), 2));
            }
            else if (rowindex == 3)
            {
                decimal Previous_Carat = Val.ToDecimal(dgvPataLotEntry.GetRowCellValue(rowindex - 2, "16_carat"));
                dgvPataLotEntry.SetRowCellValue(rowindex, "16_per", Math.Round(Val.ToDecimal((Current_Carat / Previous_Carat) * 100), 2));
            }
            else
            {
                dgvPataLotEntry.SetRowCellValue(rowindex, "16_per", 0);
            }
        }
        private void rep_txt_17_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit textEditor = (TextEdit)sender;

            int rowindex = dgvPataLotEntry.FocusedRowHandle;
            int RowNumber = dgvPataLotEntry.FocusedRowHandle;
            decimal Current_Carat = Val.ToDecimal(textEditor.EditValue);
            if (rowindex == 2)
            {
                decimal Previous_Carat = Val.ToDecimal(dgvPataLotEntry.GetRowCellValue(rowindex - 1, "17_carat"));
                dgvPataLotEntry.SetRowCellValue(rowindex, "17_per", Math.Round(Val.ToDecimal((Current_Carat / Previous_Carat) * 100), 2));
            }
            else if (rowindex == 3)
            {
                decimal Previous_Carat = Val.ToDecimal(dgvPataLotEntry.GetRowCellValue(rowindex - 2, "17_carat"));
                dgvPataLotEntry.SetRowCellValue(rowindex, "17_per", Math.Round(Val.ToDecimal((Current_Carat / Previous_Carat) * 100), 2));
            }
            else
            {
                dgvPataLotEntry.SetRowCellValue(rowindex, "17_per", 0);
            }
        }
        private void rep_txt_18_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit textEditor = (TextEdit)sender;

            int rowindex = dgvPataLotEntry.FocusedRowHandle;
            int RowNumber = dgvPataLotEntry.FocusedRowHandle;
            decimal Current_Carat = Val.ToDecimal(textEditor.EditValue);
            if (rowindex == 2)
            {
                decimal Previous_Carat = Val.ToDecimal(dgvPataLotEntry.GetRowCellValue(rowindex - 1, "18_carat"));
                dgvPataLotEntry.SetRowCellValue(rowindex, "18_per", Math.Round(Val.ToDecimal((Current_Carat / Previous_Carat) * 100), 2));
            }
            else if (rowindex == 3)
            {
                decimal Previous_Carat = Val.ToDecimal(dgvPataLotEntry.GetRowCellValue(rowindex - 2, "18_carat"));
                dgvPataLotEntry.SetRowCellValue(rowindex, "18_per", Math.Round(Val.ToDecimal((Current_Carat / Previous_Carat) * 100), 2));
            }
            else
            {
                dgvPataLotEntry.SetRowCellValue(rowindex, "18_per", 0);
            }
        }
        private void rep_txt_19_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit textEditor = (TextEdit)sender;

            int rowindex = dgvPataLotEntry.FocusedRowHandle;
            int RowNumber = dgvPataLotEntry.FocusedRowHandle;
            decimal Current_Carat = Val.ToDecimal(textEditor.EditValue);
            if (rowindex == 2)
            {
                decimal Previous_Carat = Val.ToDecimal(dgvPataLotEntry.GetRowCellValue(rowindex - 1, "19_carat"));
                dgvPataLotEntry.SetRowCellValue(rowindex, "19_per", Math.Round(Val.ToDecimal((Current_Carat / Previous_Carat) * 100), 2));
            }
            else if (rowindex == 3)
            {
                decimal Previous_Carat = Val.ToDecimal(dgvPataLotEntry.GetRowCellValue(rowindex - 2, "19_carat"));
                dgvPataLotEntry.SetRowCellValue(rowindex, "19_per", Math.Round(Val.ToDecimal((Current_Carat / Previous_Carat) * 100), 2));
            }
            else
            {
                dgvPataLotEntry.SetRowCellValue(rowindex, "19_per", 0);
            }
        }
        private void rep_txt_20_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit textEditor = (TextEdit)sender;

            int rowindex = dgvPataLotEntry.FocusedRowHandle;
            int RowNumber = dgvPataLotEntry.FocusedRowHandle;
            decimal Current_Carat = Val.ToDecimal(textEditor.EditValue);
            if (rowindex == 2)
            {
                decimal Previous_Carat = Val.ToDecimal(dgvPataLotEntry.GetRowCellValue(rowindex - 1, "20_carat"));
                dgvPataLotEntry.SetRowCellValue(rowindex, "20_per", Math.Round(Val.ToDecimal((Current_Carat / Previous_Carat) * 100), 2));
            }
            else if (rowindex == 3)
            {
                decimal Previous_Carat = Val.ToDecimal(dgvPataLotEntry.GetRowCellValue(rowindex - 2, "20_carat"));
                dgvPataLotEntry.SetRowCellValue(rowindex, "20_per", Math.Round(Val.ToDecimal((Current_Carat / Previous_Carat) * 100), 2));
            }
            else
            {
                dgvPataLotEntry.SetRowCellValue(rowindex, "20_per", 0);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (!ValidateDetails())
            {
                return;
            }

            DataTable DtPataLotPrintData = new DataTable();
            MFGPataLotEntry objMFGPataLotEntry = new MFGPataLotEntry();
            MFGPataLotEntryProperty objMFGPataLotEntryProperty = new MFGPataLotEntryProperty();

            objMFGPataLotEntryProperty.rough_cut_id = Val.ToInt64(lueRoughCutNo.EditValue);
            objMFGPataLotEntryProperty.kapan_id = Val.ToInt64(lueKapan.EditValue);
            objMFGPataLotEntryProperty.lot_srno_list = Val.ToString(lblLotSRNo.Text);

            DtPataLotPrintData = objMFGPataLotEntry.MFGPataLotEntryPrintGetData(objMFGPataLotEntryProperty);

            decimal p_numLoning_Per = 0;
            decimal p_numMathala_Weight = 0;
            decimal p_numKachha_Weight = 0;

            foreach (DataRow Drw in DtPataLotPrintData.Rows)
            {
                Drw["k_per"] = 0;
                if (Val.ToDecimal(Drw["k_carat"]) > 0)
                {
                    Drw["koning_per"] = Math.Round((Val.ToDecimal(Drw["koning_carat"]) / Val.ToDecimal(Drw["k_carat"])) * 100, 2);
                }
                if (Val.ToDecimal(Drw["koning_carat"]) > 0)
                {
                    Drw["tal_per"] = Math.Round((Val.ToDecimal(Drw["tal_carat"]) / Val.ToDecimal(Drw["koning_carat"])) * 100, 2);
                }
                if (Val.ToDecimal(Drw["tal_carat"]) > 0)
                {
                    Drw["pel_per"] = Math.Round((Val.ToDecimal(Drw["pel_carat"]) / Val.ToDecimal(Drw["tal_carat"])) * 100, 2);
                }
                if (Val.ToDecimal(Drw["pel_carat"]) > 0)
                {
                    Drw["matala_per"] = Math.Round((Val.ToDecimal(Drw["matala_carat"]) / Val.ToDecimal(Drw["pel_carat"])) * 100, 2);
                }
                p_numLoning_Per = Val.ToDecimal(Drw["koning_carat"]);
                p_numMathala_Weight = Val.ToDecimal(Drw["matala_carat"]);
                p_numKachha_Weight = Val.ToDecimal(Drw["k_carat"]);

                if (p_numLoning_Per > 0)
                {
                    Drw["pata_ok_per"] = Val.ToString(Math.Round((Val.ToDecimal(Drw["t_wt"]) / p_numLoning_Per) * 100, 2));
                }
                if (p_numKachha_Weight > 0)
                {
                    Drw["total_per"] = Val.ToString(Math.Round((Val.ToDecimal(p_numMathala_Weight) / p_numKachha_Weight) * 100, 2));
                }
            }

            //foreach (DataRow Drw in DtPataLotPrintData.Tables[1].Rows)
            //{
            //    if (p_numLoning_Per > 0)
            //    {
            //        Drw["pata_ok_per"] = Val.ToString(Math.Round((Val.ToDecimal(Drw["t_wt"]) / p_numLoning_Per) * 100, 2));
            //    }
            //    if (p_numKachha_Weight > 0)
            //    {
            //        Drw["total_per"] = Val.ToString(Math.Round((Val.ToDecimal(p_numMathala_Weight) / p_numKachha_Weight) * 100, 2));
            //    }
            //}

            //FrmReportViewer FrmReportViewer = new FrmReportViewer();
            //foreach (DataTable DTab in DtPataLotPrintData.Tables)
            //    FrmReportViewer.DS.Tables.Add(DTab.Copy());
            //FrmReportViewer.GroupBy = "";
            //FrmReportViewer.RepName = "";
            //FrmReportViewer.RepPara = "";
            //this.Cursor = Cursors.Default;
            //FrmReportViewer.AllowSetFormula = true;

            ////FrmReportViewer.ShowForm_SubReport("PataLot_Entry_MainRPT", 120, FrmReportViewer.ReportFolder.PATA_LOT_ENTRY);
            //FrmReportViewer.ShowForm_SubReport("PataLot_Entry_Part2", 120, FrmReportViewer.ReportFolder.PATA_LOT_ENTRY);

            FrmReportViewer FrmReportViewer = new FrmReportViewer();
            FrmReportViewer.DS.Tables.Add(DtPataLotPrintData);
            FrmReportViewer.GroupBy = "";
            FrmReportViewer.RepName = "";
            FrmReportViewer.RepPara = "";
            this.Cursor = Cursors.Default;
            FrmReportViewer.AllowSetFormula = true;

            FrmReportViewer.ShowForm("PataLot_Entry_Part2", 120, FrmReportViewer.ReportFolder.PATA_LOT_ENTRY);

            DtPataLotPrintData = null;
            FrmReportViewer.DS.Tables.Clear();
            FrmReportViewer.DS.Clear();
            FrmReportViewer = null;
        }

        private void rep_txt_1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dgvPataLotEntry.IsLastRow && dgvPataLotEntry.FocusedColumn.Caption == "1")
            {
                dgvPataLotEntry.FocusedColumn = dgvPataLotEntry.Columns["2_carat"];
                dgvPataLotEntry.ShowEditor();
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                e.Handled = true;
            }
            if (e.KeyCode == Keys.S)
            {
                btnSave_Click(null, null);
            }
        }

        private void rep_txt_2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dgvPataLotEntry.IsLastRow && dgvPataLotEntry.FocusedColumn.Caption == "2")
            {
                dgvPataLotEntry.FocusedColumn = dgvPataLotEntry.Columns["3_carat"];
                dgvPataLotEntry.ShowEditor();
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                e.Handled = true;
            }
            if (e.KeyCode == Keys.S)
            {
                btnSave_Click(null, null);
            }
        }

        private void rep_txt_3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dgvPataLotEntry.IsLastRow && dgvPataLotEntry.FocusedColumn.Caption == "3")
            {
                dgvPataLotEntry.FocusedColumn = dgvPataLotEntry.Columns["4_carat"];
                dgvPataLotEntry.ShowEditor();
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                e.Handled = true;
            }
            if (e.KeyCode == Keys.S)
            {
                btnSave_Click(null, null);
            }
        }

        private void rep_txt_4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dgvPataLotEntry.IsLastRow && dgvPataLotEntry.FocusedColumn.Caption == "4")
            {
                dgvPataLotEntry.FocusedColumn = dgvPataLotEntry.Columns["5_carat"];
                dgvPataLotEntry.ShowEditor();
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                e.Handled = true;
            }
            if (e.KeyCode == Keys.S)
            {
                btnSave_Click(null, null);
            }
        }

        private void rep_txt_5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dgvPataLotEntry.IsLastRow && dgvPataLotEntry.FocusedColumn.Caption == "5")
            {
                dgvPataLotEntry.FocusedColumn = dgvPataLotEntry.Columns["6_carat"];
                dgvPataLotEntry.ShowEditor();
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                e.Handled = true;
            }
            if (e.KeyCode == Keys.S)
            {
                btnSave_Click(null, null);
            }
        }

        private void rep_txt_6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dgvPataLotEntry.IsLastRow && dgvPataLotEntry.FocusedColumn.Caption == "6")
            {
                dgvPataLotEntry.FocusedColumn = dgvPataLotEntry.Columns["7_carat"];
                dgvPataLotEntry.ShowEditor();
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                e.Handled = true;
            }
            if (e.KeyCode == Keys.S)
            {
                btnSave_Click(null, null);
            }
        }

        private void rep_txt_7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dgvPataLotEntry.IsLastRow && dgvPataLotEntry.FocusedColumn.Caption == "7")
            {
                dgvPataLotEntry.FocusedColumn = dgvPataLotEntry.Columns["8_carat"];
                dgvPataLotEntry.ShowEditor();
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                e.Handled = true;
            }
            if (e.KeyCode == Keys.S)
            {
                btnSave_Click(null, null);
            }
        }

        private void rep_txt_8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dgvPataLotEntry.IsLastRow && dgvPataLotEntry.FocusedColumn.Caption == "8")
            {
                dgvPataLotEntry.FocusedColumn = dgvPataLotEntry.Columns["9_carat"];
                dgvPataLotEntry.ShowEditor();
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                e.Handled = true;
            }
            if (e.KeyCode == Keys.S)
            {
                btnSave_Click(null, null);
            }
        }

        private void rep_txt_9_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dgvPataLotEntry.IsLastRow && dgvPataLotEntry.FocusedColumn.Caption == "9")
            {
                dgvPataLotEntry.FocusedColumn = dgvPataLotEntry.Columns["10_carat"];
                dgvPataLotEntry.ShowEditor();
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                e.Handled = true;
            }
            if (e.KeyCode == Keys.S)
            {
                btnSave_Click(null, null);
            }
        }

        private void rep_txt_10_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dgvPataLotEntry.IsLastRow && dgvPataLotEntry.FocusedColumn.Caption == "10")
            {
                dgvPataLotEntry.FocusedColumn = dgvPataLotEntry.Columns["11_carat"];
                dgvPataLotEntry.ShowEditor();
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                e.Handled = true;
            }
            if (e.KeyCode == Keys.S)
            {
                btnSave_Click(null, null);
            }
        }

        private void rep_txt_11_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dgvPataLotEntry.IsLastRow && dgvPataLotEntry.FocusedColumn.Caption == "11")
            {
                dgvPataLotEntry.FocusedColumn = dgvPataLotEntry.Columns["12_carat"];
                dgvPataLotEntry.ShowEditor();
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                e.Handled = true;
            }
            if (e.KeyCode == Keys.S)
            {
                btnSave_Click(null, null);
            }
        }

        private void rep_txt_12_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dgvPataLotEntry.IsLastRow && dgvPataLotEntry.FocusedColumn.Caption == "12")
            {
                dgvPataLotEntry.FocusedColumn = dgvPataLotEntry.Columns["13_carat"];
                dgvPataLotEntry.ShowEditor();
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                e.Handled = true;
            }
            if (e.KeyCode == Keys.S)
            {
                btnSave_Click(null, null);
            }
        }

        private void rep_txt_13_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dgvPataLotEntry.IsLastRow && dgvPataLotEntry.FocusedColumn.Caption == "13")
            {
                dgvPataLotEntry.FocusedColumn = dgvPataLotEntry.Columns["14_carat"];
                dgvPataLotEntry.ShowEditor();
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                e.Handled = true;
            }
            if (e.KeyCode == Keys.S)
            {
                btnSave_Click(null, null);
            }
        }

        private void rep_txt_14_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dgvPataLotEntry.IsLastRow && dgvPataLotEntry.FocusedColumn.Caption == "14")
            {
                dgvPataLotEntry.FocusedColumn = dgvPataLotEntry.Columns["15_carat"];
                dgvPataLotEntry.ShowEditor();
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                e.Handled = true;
            }
            if (e.KeyCode == Keys.S)
            {
                btnSave_Click(null, null);
            }
        }

        private void rep_txt_15_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dgvPataLotEntry.IsLastRow && dgvPataLotEntry.FocusedColumn.Caption == "15")
            {
                dgvPataLotEntry.FocusedColumn = dgvPataLotEntry.Columns["16_carat"];
                dgvPataLotEntry.ShowEditor();
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                e.Handled = true;
            }
            if (e.KeyCode == Keys.S)
            {
                btnSave_Click(null, null);
            }
        }

        private void rep_txt_16_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dgvPataLotEntry.IsLastRow && dgvPataLotEntry.FocusedColumn.Caption == "16")
            {
                dgvPataLotEntry.FocusedColumn = dgvPataLotEntry.Columns["17_carat"];
                dgvPataLotEntry.ShowEditor();
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                e.Handled = true;
            }
            if (e.KeyCode == Keys.S)
            {
                btnSave_Click(null, null);
            }
        }

        private void rep_txt_17_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dgvPataLotEntry.IsLastRow && dgvPataLotEntry.FocusedColumn.Caption == "17")
            {
                dgvPataLotEntry.FocusedColumn = dgvPataLotEntry.Columns["18_carat"];
                dgvPataLotEntry.ShowEditor();
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                e.Handled = true;
            }
            if (e.KeyCode == Keys.S)
            {
                btnSave_Click(null, null);
            }
        }

        private void rep_txt_18_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dgvPataLotEntry.IsLastRow && dgvPataLotEntry.FocusedColumn.Caption == "18")
            {
                dgvPataLotEntry.FocusedColumn = dgvPataLotEntry.Columns["19_carat"];
                dgvPataLotEntry.ShowEditor();
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                e.Handled = true;
            }
            if (e.KeyCode == Keys.S)
            {
                btnSave_Click(null, null);
            }
        }

        private void rep_txt_19_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dgvPataLotEntry.IsLastRow && dgvPataLotEntry.FocusedColumn.Caption == "19")
            {
                dgvPataLotEntry.FocusedColumn = dgvPataLotEntry.Columns["20_carat"];
                dgvPataLotEntry.ShowEditor();
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                (grdPataLotEntry.FocusedView as ColumnView).FocusedRowHandle--;
                e.Handled = true;
            }
            if (e.KeyCode == Keys.S)
            {
                btnSave_Click(null, null);
            }
        }

        private void rep_txt_20_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dgvPataLotEntry.IsLastRow && dgvPataLotEntry.FocusedColumn.Caption == "20")
            {
                btnSave.Focus();
            }
            if (e.KeyCode == Keys.S)
            {
                btnSave_Click(null, null);
            }
        }

        private void grdPataLotEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S)
            {
                btnSave_Click(null, null);
            }
        }
    }
}
