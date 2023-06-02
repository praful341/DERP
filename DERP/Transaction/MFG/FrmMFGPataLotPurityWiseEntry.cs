using BLL;
using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.PropertyClasses.Transaction;
using BLL.PropertyClasses.Transaction.MFG;
using DERP.Class;
using DevExpress.XtraEditors;
using DREP.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DERP.Transaction.MFG
{
    public partial class FrmMFGPataLotPurityWiseEntry : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        FormEvents objBOFormEvents = new FormEvents();
        Validation Val = new Validation();
        BLL.FormPer ObjPer = new BLL.FormPer();
        DataTable m_dtbDetail = new DataTable();
        DataTable m_dtbParam = new DataTable();
        DataTable m_dtbSubProcess = new DataTable();
        BLL.BeginTranConnection Conn;
        DataTable m_dtbLotMixSplit = new DataTable();
        Control _NextEnteredControl = new Control();
        DataTable DtControlSettings = new DataTable();
        MFGLotSplit objLotSplitReceive = new MFGLotSplit();
        DataTable m_dtOutstanding = new DataTable();
        DataTable m_dtbKapan = new DataTable();
        MFGMixSplit objMFGMixSplit = new MFGMixSplit();
        private List<Control> _tabControls = new List<Control>();
        MFGTRNEstimation objMFGTRNEstimation = new MFGTRNEstimation();
        //MfgQualityMaster objQualityMaster = new MfgQualityMaster();
        //DataTable Quality = new DataTable();
        DataTable DTab_StockData = new DataTable();
        //DataTable DTab_KapanWiseData = new DataTable();
        int m_numForm_id = 0;
        Int64 IntRes;
        #endregion

        #region Constructor
        public FrmMFGPataLotPurityWiseEntry()
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
            this.KeyPreview = true;

            TabControlsToList(this.Controls);
            _tabControls = _tabControls.OrderBy(x => x.TabIndex).ToList();
            txtLotID.Focus();
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

        private void dgvRussianProcessGrp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dgvPataLotPurityWise.FocusedColumn.Caption == "WT")
            {
                if (dgvPataLotPurityWise.IsLastRow)
                {
                    btnSave_Click(null, null);
                }
            }
        }

        private void FrmMFGTRNEstimation_Load(object sender, EventArgs e)
        {
            try
            {
                dtpEntryDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpEntryDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpEntryDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpEntryDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpEntryDate.EditValue = DateTime.Now;

                m_dtbKapan = Global.GetKapanAll();

                lueKapan.Properties.DataSource = m_dtbKapan;
                lueKapan.Properties.ValueMember = "kapan_id";
                lueKapan.Properties.DisplayMember = "kapan_no";

                m_dtbParam = Global.GetRoughCutAll();

                lueCutNo.Properties.DataSource = m_dtbParam;
                lueCutNo.Properties.ValueMember = "rough_cut_id";
                lueCutNo.Properties.DisplayMember = "rough_cut_no";

                //Global.LOOKUPManager(lueManager);
                Global.LOOKUPAllManager(lueManager);
                Global.LOOKUPRoughSieve(lueRoughSieve);
                Global.LOOKUPQuality(lueQuality);
                Global.LOOKUPClarity(lueClarity);

                ClearDetails();

                //Quality = objQualityMaster.GetData(1);
                txtLotID.Focus();
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
            }
            lueCutNo.Properties.DataSource = m_dtbParam;
            lueCutNo.Properties.ValueMember = "rough_cut_id";
            lueCutNo.Properties.DisplayMember = "rough_cut_no";
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
                DialogResult result = MessageBox.Show("Do you want to save Estimation data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result != DialogResult.Yes)
                {
                    btnSave.Enabled = true;
                    return;
                }

                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                backgroundWorker_PataLotPurityWise.RunWorkerAsync();

                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }

        private void backgroundWorker_PataLotPurityWise_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                MFGTRNEstimation objMFGTRNEstimation = new MFGTRNEstimation();
                MFGTRNEstimation_Property objMFGTRNEstimationProperty = new MFGTRNEstimation_Property();
                Conn = new BeginTranConnection(true, false);
                try
                {
                    IntRes = 0;
                    int count = 0;

                    DataTable PataLotPurityWise_Data = (DataTable)grdPataLotPurityWise.DataSource;
                    count = 0;

                    if (PataLotPurityWise_Data != null)
                    {
                        if (PataLotPurityWise_Data.Rows.Count > 0)
                        {
                            foreach (DataRow drw in PataLotPurityWise_Data.Rows)
                            {
                                if (Val.ToString(drw["quality_name"]) != "")
                                {
                                    objMFGTRNEstimationProperty.carat = Val.ToDecimal(drw["carat"]);
                                    objMFGTRNEstimationProperty.lot_id = Val.ToInt64(txtLotID.Text);
                                    objMFGTRNEstimationProperty.patalot_date = Val.DBDate(dtpEntryDate.Text);
                                    objMFGTRNEstimationProperty.machine_name = Val.ToString(txtMachine.Text);
                                    objMFGTRNEstimationProperty.form_id = Val.ToInt(m_numForm_id);
                                    objMFGTRNEstimationProperty.rough_cut_id = Val.ToInt64(lueCutNo.EditValue);
                                    objMFGTRNEstimationProperty.kapan_id = Val.ToInt64(lueKapan.EditValue);
                                    objMFGTRNEstimationProperty.quality_id = Val.ToInt64(drw["quality_id"]);
                                    objMFGTRNEstimationProperty.total_carat = Val.ToDecimal(txtCarat.Text);
                                    objMFGTRNEstimationProperty.manager_id = Val.ToInt64(lueManager.EditValue);

                                    objMFGTRNEstimationProperty.prev_quality_id = Val.ToInt64(lueQuality.EditValue);
                                    objMFGTRNEstimationProperty.prev_rough_sieve_id = Val.ToInt64(lueRoughSieve.EditValue);
                                    objMFGTRNEstimationProperty.prev_rough_clarity_id = Val.ToInt64(lueClarity.EditValue);

                                    IntRes = objMFGTRNEstimation.Save_MFGPataLotPurityWise(objMFGTRNEstimationProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                                    count = count + 1;
                                }
                            }
                        }
                    }

                    //DataTable PataLotClarityWise_Data = (DataTable)grdPataLotClarityWise.DataSource;
                    //clarity_count = 0;
                    //objMFGTRNEstimationProperty = new MFGTRNEstimation_Property();
                    //objMFGTRNEstimation = new MFGTRNEstimation();
                    //if (PataLotClarityWise_Data != null)
                    //{
                    //    if (PataLotClarityWise_Data.Rows.Count > 0)
                    //    {
                    //        foreach (DataRow drw in PataLotClarityWise_Data.Rows)
                    //        {
                    //            if (Val.ToString(drw["rough_clarity_name"]) != "")
                    //            {
                    //                objMFGTRNEstimationProperty.carat = Val.ToDecimal(drw["carat"]);
                    //                objMFGTRNEstimationProperty.lot_id = Val.ToInt64(txtLotID.Text);
                    //                objMFGTRNEstimationProperty.patalot_date = Val.DBDate(dtpEntryDate.Text);
                    //                objMFGTRNEstimationProperty.machine_name = Val.ToString(txtMachine.Text);
                    //                objMFGTRNEstimationProperty.form_id = Val.ToInt(m_numForm_id);
                    //                objMFGTRNEstimationProperty.rough_cut_id = Val.ToInt64(lueCutNo.EditValue);
                    //                objMFGTRNEstimationProperty.kapan_id = Val.ToInt64(lueKapan.EditValue);
                    //                objMFGTRNEstimationProperty.rough_clarity_id = Val.ToInt64(drw["rough_clarity_id"]);
                    //                objMFGTRNEstimationProperty.total_carat = Val.ToDecimal(txtCarat.Text);

                    //                IntRes = objMFGTRNEstimation.Save_MFGPataLotPurityWise(objMFGTRNEstimationProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                    //                clarity_count = clarity_count + 1;
                    //            }
                    //        }
                    //    }
                    //}
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
        private void backgroundWorker_PataLotPurityWise_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (IntRes > 0)
                {
                    Global.Confirm("PataLot Purity Wise Data Save Succesfully");
                    btnSave.Enabled = true;
                    ClearDetails();
                }
                else
                {
                    Global.Confirm("Error In PataLot Purity Wise Data");
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
        private void txtLotID_Validated(object sender, EventArgs e)
        {
            try
            {
                if (txtLotID.Text.Length == 0)
                {
                    return;
                }

                MFGProcessIssue objMFGProcessIssue = new MFGProcessIssue();
                MFGProcessIssueProperty objMFGProcessIssueProperty = new MFGProcessIssueProperty();
                MFGTRNEstimation_Property objMFGTRNEstimationProperty = new MFGTRNEstimation_Property();
                objMFGProcessIssueProperty.flag = Val.ToInt(2);

                DTab_StockData = objMFGProcessIssue.GetPendingDeptStock(Val.ToInt64(txtLotID.Text), objMFGProcessIssueProperty);

                if (DTab_StockData.Rows.Count > 0)
                {
                    lueKapan.EditValue = Val.ToInt64(DTab_StockData.Rows[0]["kapan_id"]);
                    lueCutNo.EditValue = Val.ToInt64(DTab_StockData.Rows[0]["rough_cut_id"]);
                    txtCarat.Text = Val.ToDecimal(DTab_StockData.Rows[0]["carat"]).ToString();
                    txtMachine.Text = Val.ToString(DTab_StockData.Rows[0]["machine_name"]).ToString();
                    lueManager.EditValue = Val.ToInt64(DTab_StockData.Rows[0]["employee_id"]);
                    lueRoughSieve.EditValue = Val.ToInt32(DTab_StockData.Rows[0]["rough_sieve_id"]);
                    lueQuality.EditValue = Val.ToInt64(DTab_StockData.Rows[0]["quality_id"]);
                    lueClarity.EditValue = Val.ToInt32(DTab_StockData.Rows[0]["rough_clarity_id"]);

                    if (GlobalDec.gEmployeeProperty.role_name == "SURAT MAKABLE" || GlobalDec.gEmployeeProperty.role_name == "SURAT MAKABLE 1" || GlobalDec.gEmployeeProperty.role_name == "SURAT POLISH REPARING")
                    {
                        panelControl4.Enabled = false;
                        panelControl7.Enabled = true;
                        panelControl10.Enabled = true;

                        objMFGTRNEstimationProperty.entry_date = Val.DBDate(dtpEntryDate.Text);
                        objMFGTRNEstimationProperty.lot_id = Val.ToInt64(txtLotID.EditValue);
                        objMFGTRNEstimationProperty.kapan_id = Val.ToInt64(lueKapan.EditValue);
                        objMFGTRNEstimationProperty.rough_cut_id = Val.ToInt64(lueCutNo.EditValue);

                        DataTable DTabDataPataLotPurityWise = objMFGTRNEstimation.GetPurityWiseData(objMFGTRNEstimationProperty);

                        if (DTabDataPataLotPurityWise.Rows.Count > 0)
                        {
                            if (Val.ToDecimal(DTabDataPataLotPurityWise.Rows[0]["total_carat"]) != 0)
                            {
                                txtMachine.Text = Val.ToString(DTabDataPataLotPurityWise.Rows[0]["machine_name"]);
                                //txtMachine.Tag = Val.ToString(DTabDataPataLotPurityWise.Rows[0]["machine_id"]);
                                txtCarat.Text = Val.ToDecimal(DTabDataPataLotPurityWise.Rows[0]["total_carat"]).ToString();

                                if (Val.ToInt32(DTabDataPataLotPurityWise.Rows[0]["employee_id"]) != 0)
                                {
                                    lueManager.EditValue = Val.ToInt32(DTabDataPataLotPurityWise.Rows[0]["employee_id"]);
                                }

                                if (Val.ToInt32(DTabDataPataLotPurityWise.Rows[0]["prev_rough_sieve_id"]) != 0)
                                {
                                    lueRoughSieve.EditValue = Val.ToInt32(DTabDataPataLotPurityWise.Rows[0]["prev_rough_sieve_id"]);
                                }
                                if (Val.ToInt32(DTabDataPataLotPurityWise.Rows[0]["prev_quality_id"]) != 0)
                                {
                                    lueQuality.EditValue = Val.ToInt32(DTabDataPataLotPurityWise.Rows[0]["prev_quality_id"]);
                                }
                                if (Val.ToInt32(DTabDataPataLotPurityWise.Rows[0]["prev_rough_clarity_id"]) != 0)
                                {
                                    lueClarity.EditValue = Val.ToInt32(DTabDataPataLotPurityWise.Rows[0]["prev_rough_clarity_id"]);
                                }
                            }
                            grdPataLotPurityWise.DataSource = DTabDataPataLotPurityWise;
                        }
                        //if (DTabDataPataLotPurityWise.Tables[1].Rows.Count > 0)
                        //{
                        //    if (Val.ToDecimal(DTabDataPataLotPurityWise.Tables[1].Rows[0]["total_carat"]) != 0)
                        //    {
                        //        txtMachine.Text = Val.ToString(DTabDataPataLotPurityWise.Tables[1].Rows[0]["machine_name"]);
                        //        //txtMachine.Tag = Val.ToString(DTabDataPataLotPurityWise.Rows[0]["machine_id"]);
                        //        txtCarat.Text = Val.ToDecimal(DTabDataPataLotPurityWise.Tables[1].Rows[0]["total_carat"]).ToString();
                        //    }
                        //    grdPataLotClarityWise.DataSource = DTabDataPataLotPurityWise.Tables[1];
                        //}

                        //grdPataLotPurityWise.Focus();
                        //txtMachine.Focus();
                        dgvPataLotPurityWise.FocusedColumn = dgvPataLotPurityWise.Columns["carat"];
                        dgvPataLotPurityWise.ShowEditor();
                    }
                    txtLotID.Enabled = false;
                }
                else
                {
                    Global.Message("Lot ID Not found");
                    txtLotID.Text = "";
                    txtLotID.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }

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
                //if (txtMachine.Text == "")
                //{
                //    lstError.Add(new ListError(13, "Machine"));
                //    if (!blnFocus)
                //    {
                //        blnFocus = true;
                //        txtMachine.Focus();
                //    }
                //}
                if (txtLotID.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Lot ID"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtLotID.Focus();
                    }
                }
                if (txtLotID.Text == "0")
                {
                    lstError.Add(new ListError(12, "Lot ID"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtLotID.Focus();
                    }
                }
                DateTime endDate = Convert.ToDateTime(DateTime.Today);
                endDate = endDate.AddDays(3);

                if (Convert.ToDateTime(dtpEntryDate.Text) >= endDate)
                {
                    lstError.Add(new ListError(5, " Entry Date Not Be Permission After 3 Days in this Lot ID...Please Contact to Administrator"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        dtpEntryDate.Focus();
                    }
                }

                decimal Total_Carat = Val.ToDecimal(dgvPataLotPurityWise.Columns["carat"].SummaryItem.SummaryValue);
                decimal Carat = Val.ToDecimal(txtCarat.Text);
                decimal Diff_Carat = Carat - Total_Carat;

                if (Diff_Carat > Val.ToDecimal(0.04) || Diff_Carat < Val.ToDecimal(-0.04))
                {
                    lstError.Add(new ListError(5, "Total Carat Not Greater Then Actual Carat"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        grdPataLotPurityWise.Focus();
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

                lueKapan.EditValue = System.DBNull.Value;
                lueCutNo.EditValue = System.DBNull.Value;
                lueManager.EditValue = System.DBNull.Value;

                lueRoughSieve.EditValue = System.DBNull.Value;
                lueQuality.EditValue = System.DBNull.Value;
                lueClarity.EditValue = System.DBNull.Value;

                txtMachine.Text = string.Empty;
                txtCarat.Text = string.Empty;

                DTab_StockData.Rows.Clear();
                DTab_StockData.AcceptChanges();

                grdPataLotPurityWise.DataSource = null;
                grdPataLotClarityWise.DataSource = null;
                txtPassword.Text = "";
                txtLotID.Enabled = true;
                txtLotID.Text = string.Empty;
                txtLotID.Focus();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
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
                        //((LookUpEdit)(Control)sender).ClosePopup();
                    }

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

        #endregion

        private void txtPassword_EditValueChanged(object sender, EventArgs e)
        {
            if (txtPassword.Text != "")
            {
                if (Val.ToString(txtPassword.Text) == "123")
                {
                    btnDelete.Visible = true;
                }
                else
                {
                    btnDelete.Visible = false;
                }
            }
            else
            {
                btnDelete.Visible = false;
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (GlobalDec.gEmployeeProperty.role_name == "SURAT MAKABLE" || GlobalDec.gEmployeeProperty.role_name == "SURAT MAKABLE 1")
            {
                MFGTRNEstimation objMFGTRNEstimation = new MFGTRNEstimation();
                MFGTRNEstimation_Property objMFGTRNEstimationProperty = new MFGTRNEstimation_Property();
                try
                {
                    DialogResult result = MessageBox.Show("Do you want to Delete PataLot Purity Wise data?", "Confirmation", MessageBoxButtons.YesNo);
                    if (result != DialogResult.Yes)
                    {
                        btnSave.Enabled = true;
                        return;
                    }
                    btnDelete.Enabled = false;

                    objMFGTRNEstimationProperty.lot_id = Val.ToInt64(txtLotID.Text);
                    objMFGTRNEstimationProperty.rough_cut_id = Val.ToInt64(lueCutNo.EditValue);
                    objMFGTRNEstimationProperty.kapan_id = Val.ToInt64(lueKapan.EditValue);

                    int IntRes = objMFGTRNEstimation.PataLot_PurityWise_Data_Delete(objMFGTRNEstimationProperty);

                    if (IntRes > 0)
                    {
                        Global.Confirm("PataLot Purity Wise Data Deleted Succesfully");
                        btnSave.Enabled = true;
                        btnDelete.Enabled = true;
                        ClearDetails();
                    }
                    else
                    {
                        Global.Confirm("Error In PataLot Purity Wise Data");
                        btnDelete.Enabled = true;
                        btnSave.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    IntRes = -1;
                    General.ShowErrors(ex.ToString());
                    btnDelete.Enabled = true;
                    btnSave.Enabled = true;
                    return;
                }
                finally
                {
                    objMFGTRNEstimationProperty = null;
                }
            }
            else
            {
                Global.Message("You Not Authorised Delete Option..Please Contact System Administration..");
                return;
            }
        }
        private void txtMachine_Validated(object sender, EventArgs e)
        {
            grdPataLotPurityWise.Focus();
        }

        private void lueManager_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmEmployeeMaster frmManager = new FrmEmployeeMaster();
                frmManager.ShowDialog();
                Global.LOOKUPAllManager(lueManager);
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
