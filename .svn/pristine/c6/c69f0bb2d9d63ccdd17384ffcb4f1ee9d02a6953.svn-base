using BLL;
using BLL.FunctionClasses.Transaction;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DERP.Transaction.MFG
{
    public partial class FrmMFGCleaningRateUpdate : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        FormEvents objBOFormEvents = new FormEvents();
        Validation Val = new Validation();
        BLL.FormPer ObjPer = new BLL.FormPer();
        DataTable m_dtbParam = new DataTable();
        BLL.BeginTranConnection Conn;
        DataTable m_dtbLotMixSplit = new DataTable();
        Control _NextEnteredControl = new Control();
        DataTable DtControlSettings = new DataTable();
        DataTable m_dtbKapan = new DataTable();
        private List<Control> _tabControls = new List<Control>();
        MFGLottingDepartment objMFGLottingDepartment = new MFGLottingDepartment();
        int m_numForm_id = 0;
        Int64 IntRes;
        decimal m_numSummSarinRate = 0;
        decimal m_numSummSawablePurityRate = 0;
        DataTable DTabDataPolish = new DataTable();
        //DataTable DTab_KapanWiseData = new DataTable();

        #endregion

        #region Constructor
        public FrmMFGCleaningRateUpdate()
        {
            InitializeComponent();
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
            AddKeyPressListener(this);
            this.KeyPreview = true;

            TabControlsToList(this.Controls);
            _tabControls = _tabControls.OrderBy(x => x.TabIndex).ToList();

            lueKapan.Focus();
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

        private void lueCutNo_EditValueChanged(object sender, EventArgs e)
        {
            DataRow[] dr = m_dtbParam.Select("rough_cut_no ='" + Val.ToString(lueCutNo.Text) + "'");
            if (dr.Length > 0)
            {
                DataTable dtIssOS = new DataTable();
                dtIssOS = objMFGLottingDepartment.CutNo_Carat_GetData(Val.ToInt64(lueKapan.EditValue), Val.ToInt64(lueCutNo.EditValue));

                if (lueKapan.Text.ToString() != "")
                {
                    if (dtIssOS.Rows.Count > 0)
                    {
                        if (Val.ToDecimal(dtIssOS.Rows[0]["carat"]) > 0)
                        {
                            txtCarat.Text = Val.ToDecimal(dtIssOS.Rows[0]["carat"]).ToString();
                            txtRate.Text = Val.ToDecimal(dtIssOS.Rows[0]["rate"]).ToString();
                            txtAmount.Text = Val.ToDecimal(dtIssOS.Rows[0]["amount"]).ToString();
                        }
                        else
                        {
                            txtCarat.Text = "0";
                            txtRate.Text = "0";
                            txtAmount.Text = "0";
                        }
                    }
                    else
                    {
                        txtCarat.Text = "0";
                        txtRate.Text = "0";
                        txtAmount.Text = "0";
                    }
                }
            }
        }
        private void FrmMFGCleaningRateUpdate_Load(object sender, EventArgs e)
        {
            try
            {
                dtpEntryDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpEntryDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpEntryDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpEntryDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpEntryDate.EditValue = DateTime.Now;

                m_dtbKapan = Global.GetKapanLottingAll();

                lueKapan.Properties.DataSource = m_dtbKapan;
                lueKapan.Properties.ValueMember = "kapan_id";
                lueKapan.Properties.DisplayMember = "kapan_no";

                m_dtbParam = Global.GetRoughCutAll();

                lueCutNo.Properties.DataSource = m_dtbParam;
                lueCutNo.Properties.ValueMember = "rough_cut_id";
                lueCutNo.Properties.DisplayMember = "rough_cut_no";

                // Add By Praful On 29072021

                // DTab_KapanWiseData = Global.GetRoughStockWise(Val.ToInt(0), Val.ToInt32(0));

                // End By Praful On 29072021

                ClearDetails();
                lueKapan.Focus();
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
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

            DataTable dtIssOS = new DataTable();
            dtIssOS = objMFGLottingDepartment.Kapan_Carat_GetData(Val.ToInt64(lueKapan.EditValue));

            if (lueKapan.Text.ToString() != "")
            {
                if (dtIssOS.Rows.Count > 0)
                {
                    if (Val.ToDecimal(dtIssOS.Rows[0]["carat"]) > 0)
                    {
                        txtCarat.Text = Val.ToDecimal(dtIssOS.Rows[0]["carat"]).ToString();
                        txtRate.Text = Val.ToDecimal(dtIssOS.Rows[0]["rate"]).ToString();
                        txtAmount.Text = Val.ToDecimal(dtIssOS.Rows[0]["amount"]).ToString();
                    }
                    else
                    {
                        txtCarat.Text = "0";
                        txtRate.Text = "0";
                        txtAmount.Text = "0";
                    }
                }
                else
                {
                    txtCarat.Text = "0";
                    txtRate.Text = "0";
                    txtAmount.Text = "0";
                }
            }
        }
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
                btnSave.Enabled = false;

                if (!ValidateDetails())
                {
                    btnSave.Enabled = true;
                    return;
                }

                DialogResult result = MessageBox.Show("Do you want to save Cleaning Rate Update data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result != DialogResult.Yes)
                {
                    btnSave.Enabled = true;
                    return;
                }

                PanelLoading.Visible = true;

                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                backgroundWorker_CleaningRateUpdate.RunWorkerAsync();

                PanelLoading.Visible = false;
                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        private void backgroundWorker_CleaningRateUpdate_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                MFGLottingDepartment objMFGLottingDepartment = new MFGLottingDepartment();
                MFGLottingDepartmentProperty objMFGLottingDepartmentProperty = new MFGLottingDepartmentProperty();
                Conn = new BeginTranConnection(true, false);
                try
                {
                    IntRes = 0;

                    DataTable Sawable_Data = (DataTable)grdSawableProcess.DataSource;
                    if (Sawable_Data != null)
                    {
                        if (Sawable_Data.Rows.Count > 0)
                        {
                            foreach (DataRow drw in Sawable_Data.Rows)
                            {
                                objMFGLottingDepartmentProperty.kapan_id = Val.ToInt64(lueKapan.EditValue);
                                objMFGLottingDepartmentProperty.rough_cut_id = Val.ToInt64(lueCutNo.EditValue);
                                objMFGLottingDepartmentProperty.type = Val.ToString("4P SAWING");
                                objMFGLottingDepartmentProperty.rate = Val.ToDecimal(drw["rate"]);
                                objMFGLottingDepartmentProperty.lot_id = Val.ToInt64(drw["lot_id"]);

                                IntRes = objMFGLottingDepartment.Save_MFGRough_CleningDepartment(objMFGLottingDepartmentProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                            }
                        }
                    }
                    DataTable Sawable_ORN_Data = (DataTable)grdSawablePurity.DataSource;
                    if (Sawable_ORN_Data != null)
                    {
                        if (Sawable_ORN_Data.Rows.Count > 0)
                        {
                            foreach (DataRow drw in Sawable_ORN_Data.Rows)
                            {
                                objMFGLottingDepartmentProperty.kapan_id = Val.ToInt64(lueKapan.EditValue);
                                objMFGLottingDepartmentProperty.rough_cut_id = Val.ToInt64(lueCutNo.EditValue);
                                objMFGLottingDepartmentProperty.type = Val.ToString("ORN");
                                objMFGLottingDepartmentProperty.rate = Val.ToDecimal(drw["rate"]);
                                objMFGLottingDepartmentProperty.lot_id = Val.ToInt64(drw["lot_id"]);
                                objMFGLottingDepartmentProperty.type_sawable_purity = Val.ToString(drw["type"]);

                                IntRes = objMFGLottingDepartment.Save_MFGRough_CleningDepartment(objMFGLottingDepartmentProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                            }
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

        private void backgroundWorker_CleaningRateUpdate_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                PanelLoading.Visible = false;
                if (IntRes > 0)
                {
                    Global.Confirm("Cleaning Rate Data Save Succesfully");
                    btnSave.Enabled = true;
                    ClearDetails();
                }
                else
                {
                    Global.Confirm("Error In Cleaning Rate Data");
                    btnSave.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearDetails();
        }
        private void btnShow_Click(object sender, EventArgs e)
        {
            MFGLottingDepartmentProperty objMFGLottingDepartmentProperty = new MFGLottingDepartmentProperty();
            objMFGLottingDepartmentProperty.kapan_id = Val.ToInt64(lueKapan.EditValue);
            objMFGLottingDepartmentProperty.rough_cut_id = Val.ToInt64(lueCutNo.EditValue);

            DataSet DTabDataSarin = objMFGLottingDepartment.GetSawableData(objMFGLottingDepartmentProperty);
            if (DTabDataSarin.Tables[0] != null)
            {
                grdSawableProcess.DataSource = DTabDataSarin.Tables[0];
                dgvSawableProcess.BestFitColumns();
                grdSawableProcess.Focus();
            }
            else
            {
                grdSawableProcess.DataSource = null;
            }
            if (DTabDataSarin.Tables[1] != null)
            {
                grdSawablePurity.DataSource = DTabDataSarin.Tables[1];
                dgvSawablePurity.BestFitColumns();
            }
            else
            {
                grdSawablePurity.DataSource = null;
            }
        }
        private void txtRate_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtAmount.Text = string.Format("{0:0.00}", Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text));
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        private void RepRate_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit textEditor = (TextEdit)sender;

            int rowindex = dgvSawableProcess.FocusedRowHandle;
            int RowNumber = dgvSawableProcess.FocusedRowHandle;
            decimal Current_Rate = Val.ToDecimal(textEditor.EditValue);

            decimal Grid_Carat = Val.ToDecimal(dgvSawableProcess.GetRowCellValue(rowindex, "carat"));
            decimal Grid_Amount = Val.ToDecimal(Current_Rate * Grid_Carat);
            dgvSawableProcess.SetRowCellValue(rowindex, "amount", Math.Round(Val.ToDecimal(Grid_Amount), 3));
        }
        #region Grid Event

        private void dgvSarinProcess_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            CalculateGridAmount_Sarin(dgvSawableProcess.FocusedRowHandle);
        }
        private void dgvSarinProcess_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            CalculateGridAmount_Sarin(e.PrevFocusedRowHandle);
        }
        private void dgvSawablePurity_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            CalculateGridAmount_SawablePurity(dgvSawablePurity.FocusedRowHandle);
        }

        private void dgvSawablePurity_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            CalculateGridAmount_SawablePurity(dgvSawablePurity.FocusedRowHandle);
        }
        private void dgvSarinProcess_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                if (Val.ToDecimal(ClmSarinAmount.SummaryItem.SummaryValue) > 0 && Val.ToDecimal(clmSarinCarat.SummaryItem.SummaryValue) > 0)
                {
                    m_numSummSarinRate = Math.Round((Val.ToDecimal(ClmSarinAmount.SummaryItem.SummaryValue) / Val.ToDecimal(clmSarinCarat.SummaryItem.SummaryValue)), 3, MidpointRounding.AwayFromZero);
                }
                else
                {
                    m_numSummSarinRate = 0;
                }
                if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == "rate")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                        e.TotalValue = m_numSummSarinRate;
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }

        #endregion

        #endregion

        #region Function 
        private bool ValidateDetails()
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
                if (lueCutNo.Text == "")
                {
                    lstError.Add(new ListError(13, "Cut No"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueCutNo.Focus();
                    }
                }
            }

            catch (Exception ex)
            {
                lstError.Add(new ListError(ex));
            }
            return (!(BLL.General.ShowErrors(lstError)));
        }
        private bool ClearDetails()
        {
            bool blnReturn = true;
            try
            {
                dtpEntryDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpEntryDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpEntryDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpEntryDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpEntryDate.EditValue = DateTime.Now;

                //txtCarat.Text = string.Empty;
                //txtRate.Text = string.Empty;
                //txtAmount.Text = string.Empty;
                grdSawableProcess.DataSource = null;
                grdSawablePurity.DataSource = null;
                lueCutNo.Focus();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
        }
        private void CalculateGridAmount_Sarin(int rowindex)
        {
            try
            {
                dgvSawableProcess.SetRowCellValue(rowindex, "amount", Math.Round((Val.ToDouble(dgvSawableProcess.GetRowCellValue(rowindex, "rate")) * Val.ToDouble(dgvSawableProcess.GetRowCellValue(rowindex, "carat"))), 2));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void CalculateGridAmount_SawablePurity(int rowindex)
        {
            try
            {
                dgvSawablePurity.SetRowCellValue(rowindex, "amount", Math.Round((Val.ToDouble(dgvSawablePurity.GetRowCellValue(rowindex, "rate")) * Val.ToDouble(dgvSawablePurity.GetRowCellValue(rowindex, "carat"))), 2));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

        #region Dynamic Tab Control

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

        #endregion

        private void dgvSawablePurity_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                if (Val.ToDecimal(ClmSawablePurityAmount.SummaryItem.SummaryValue) > 0 && Val.ToDecimal(ClmSawablePurityCarat.SummaryItem.SummaryValue) > 0)
                {
                    m_numSummSawablePurityRate = Math.Round((Val.ToDecimal(ClmSawablePurityAmount.SummaryItem.SummaryValue) / Val.ToDecimal(ClmSawablePurityCarat.SummaryItem.SummaryValue)), 3, MidpointRounding.AwayFromZero);
                }
                else
                {
                    m_numSummSawablePurityRate = 0;
                }
                if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == "rate")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                        e.TotalValue = m_numSummSawablePurityRate;
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }

        private void RepSawablePurityRate_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit textEditor = (TextEdit)sender;

            int rowindex = dgvSawablePurity.FocusedRowHandle;
            int RowNumber = dgvSawablePurity.FocusedRowHandle;
            decimal Current_Rate = Val.ToDecimal(textEditor.EditValue);

            decimal Grid_Carat = Val.ToDecimal(dgvSawablePurity.GetRowCellValue(rowindex, "carat"));
            decimal Grid_Amount = Val.ToDecimal(Current_Rate * Grid_Carat);
            dgvSawablePurity.SetRowCellValue(rowindex, "amount", Math.Round(Val.ToDecimal(Grid_Amount), 3));
        }

        private void RepRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dgvSawableProcess.FocusedColumn.Caption == "Rate")
            {
                if (dgvSawableProcess.IsLastRow)
                {
                    panelControl4.Focus();
                    grdSawablePurity.DefaultView.Focus();
                    dgvSawablePurity.Focus();
                    dgvSawablePurity.Columns[0].OptionsColumn.AllowFocus = true;
                }
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

        private void txtRate_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}
