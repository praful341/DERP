using BLL;
using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using DERP.Report.Barcode_Print;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Transaction.MFG
{
    public partial class FrmMFGKapanMixing : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        FormEvents objBOFormEvents = new FormEvents();
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);
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
        MFGProcessReceive objProcessRecieve = new MFGProcessReceive();
        DataTable dtIss = new DataTable();
        IDataObject PasteclipData = Clipboard.GetDataObject();
        DataTable DtPending = new DataTable();
        DataTable dtBarcodePrint = new DataTable();
        DataTable DTab_StockData = new DataTable();
        //DataTable DTab_KapanWiseData = new DataTable();
        bool m_blnadd = new bool();
        bool m_blnsave = new bool();
        decimal m_numSummAmount = 0;
        decimal m_numSummRate = 0;
        int m_numForm_id = 0;
        int IntTotalLot = 0;
        double DblTotalCarat = 0.00;
        int IntTotalPcs = 0;
        int m_unionId = 0;
        Int64 IntRes;
        Int64 New_Lot_ID;
        Int64 MixSplit_IntRes;
        DataTable m_dtbStock = new DataTable();
        DataTable DTabTemp = new DataTable();
        Int64 New_IntRes = 0;
        #endregion

        #region Constructor
        public FrmMFGKapanMixing()
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
            //if (Global.HideFormControls(Val.ToInt(ObjPer.form_id), this) != "")
            //{
            //    Global.Message("Select First User Setting...Please Contact to Administrator...");
            //    return;
            //}

            //ControlSettingDT(Val.ToInt(ObjPer.form_id), this);
            //AddGotFocusListener(this);
            //AddKeyPressListener(this);
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

        #region Dynamic Tab Setting

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
            if (item2.Controls.Count > 0)
            {
                foreach (Control item1 in item2.Controls)
                {
                    ControllSettings(item1, DTab);
                }
            }
        }

        #endregion

        #region Validation
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (m_blnsave)
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
                        lstError.Add(new ListError(13, "Rough Cut No"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueCutNo.Focus();
                        }
                    }

                    var result = DateTime.Compare(Convert.ToDateTime(dtpReceiveDate.Text), DateTime.Today);
                    if (result > 0)
                    {
                        lstError.Add(new ListError(5, " Recieve Date Not Be Greater Than Today Date"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            dtpReceiveDate.Focus();
                        }
                    }

                    DataRow[] dr_DTab = DTab_StockData.Select("rough_cut_id = " + Val.ToInt64(lueCutNo.EditValue));

                    if (dr_DTab.Length > 0)
                    {
                        lstError.Add(new ListError(5, "Rough Cut No Already Exist in Mix Data"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueCutNo.Focus();
                        }
                    }
                }

                if (m_blnadd)
                {
                    var result = DateTime.Compare(Convert.ToDateTime(dtpReceiveDate.Text), DateTime.Today);
                    if (result > 0)
                    {
                        lstError.Add(new ListError(5, " Recieve Date Not Be Greater Than Today Date"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            dtpReceiveDate.Focus();
                        }
                    }
                    if (Val.ToString(dtpReceiveDate.Text) == string.Empty)
                    {
                        lstError.Add(new ListError(22, "Date"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            dtpReceiveDate.Focus();
                        }
                    }

                    if (lueMixKapan.Text == "")
                    {
                        lstError.Add(new ListError(13, "Kapan No"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueMixKapan.Focus();
                        }
                    }
                    if (lueMixCutNo.Text == "")
                    {
                        lstError.Add(new ListError(13, "Rough Cut No"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueMixCutNo.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lstError.Add(new ListError(ex));
            }
            return (!(BLL.General.ShowErrors(lstError)));
        }
        #endregion

        #region Events

        private void btnShow_Click(object sender, EventArgs e)
        {
            m_blnadd = true;
            m_blnsave = false;
            if (!ValidateDetails())
            {
                return;
            }
            try
            {
                DTab_StockData.AcceptChanges();
                if (DTab_StockData != null)
                {
                    if (DTab_StockData.Rows.Count > 0)
                    {
                        for (int i = 0; i < DTab_StockData.Rows.Count; i++)
                        {
                            if (Val.ToInt32(DTab_StockData.Rows[i]["rough_cut_id"]) == Val.ToInt64(lueMixCutNo.EditValue))
                            {
                                Global.Message("Rough Cut No: " + DTab_StockData.Rows[i]["rough_cut_no"] + "\n\nalready added to the list!");
                                lueMixCutNo.Text = "";
                                lueMixCutNo.Focus();
                                return;
                            }

                        }
                    }
                }

                if (lueMixCutNo.Text.Length == 0)
                {
                    return;
                }

                MFGProcessIssue objMFGProcessIssue = new MFGProcessIssue();
                MFGProcessIssueProperty objMFGProcessIssueProperty = new MFGProcessIssueProperty();
                objMFGProcessIssueProperty.rough_cut_id = Val.ToInt(lueMixCutNo.EditValue);
                objMFGProcessIssueProperty.kapan_id = Val.ToInt(lueMixKapan.EditValue);

                if (DTab_StockData.Rows.Count > 0)
                {
                    DataTable DTabTemp = new DataTable();

                    DTabTemp = objMFGProcessIssue.GetKapanMixLiveStock(objMFGProcessIssueProperty);
                    if (DTabTemp.Rows.Count > 0)
                    {
                        lueMixCutNo.EditValue = null;
                        lueMixCutNo.Focus();
                    }
                    else
                    {
                        Global.Message("Rough Cut No Not found");
                        lueMixCutNo.EditValue = null;
                        lueMixCutNo.Focus();
                        return;
                    }
                    DTab_StockData.Merge(DTabTemp);
                }
                else
                {
                    DTab_StockData = objMFGProcessIssue.GetKapanMixLiveStock(objMFGProcessIssueProperty);
                    if (DTab_StockData.Rows.Count > 0)
                    {
                        for (int i = 0; i < DTab_StockData.Rows.Count; i++)
                        {
                            if (Val.ToInt(DTab_StockData.Rows[i]["lot_id"]) != 0)
                            {
                                DataTable DtConfirm = Global.CheckConfirmLot(Val.ToInt(DTab_StockData.Rows[i]["lot_id"]));
                                if (DtConfirm.Rows.Count == 0)
                                {
                                    Global.Message("Please Confirm Lot First!!! " + Val.ToInt(DTab_StockData.Rows[i]["lot_id"]));
                                    DTab_StockData = new DataTable();
                                    return;
                                }
                            }

                        }
                    }
                    if (DTab_StockData.Rows.Count > 0)
                    {
                        lueMixCutNo.EditValue = null;
                        lueMixCutNo.Focus();
                    }
                    else
                    {
                        Global.Message("Rough Cut No Not found");
                        lueMixCutNo.EditValue = null;
                        lueMixCutNo.Focus();
                        return;
                    }
                }
                grdDet.DataSource = DTab_StockData;
                grdDet.RefreshDataSource();
                dgvDet.BestFitColumns();
                CalculateSummary();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }

        private void backgroundWorker_KapanMix_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {

                MFGLotSplit objMFGLotSplit = new MFGLotSplit();
                MFGMixSplitProperty objMFGMixSplitProperty = new MFGMixSplitProperty();
                Conn = new BeginTranConnection(true, false);
                //dtBarcodePrint = new DataTable();
                //dtBarcodePrint.Columns.Add("kapan_no", typeof(string));
                //dtBarcodePrint.Columns.Add("sr_no", typeof(int));
                //dtBarcodePrint.Columns.Add("lot_id", typeof(int));
                //dtBarcodePrint.Columns.Add("pcs", typeof(int));
                //dtBarcodePrint.Columns.Add("carat", typeof(decimal));
                //dtBarcodePrint.Columns.Add("receive_date", typeof(string));
                //DataRow drwNew;
                try
                {
                    IntRes = 0;
                    New_Lot_ID = 0;
                    MixSplit_IntRes = 0;
                    Int64 NewHistory_Union_Id = 0;
                    Int64 Lot_SrNo = 0;

                    DataTable Mix_Data = (DataTable)grdDet.DataSource;

                    foreach (DataRow drw in Mix_Data.Rows)
                    {
                        objMFGMixSplitProperty.pcs = Val.ToInt(drw["pcs"]);
                        objMFGMixSplitProperty.carat = Val.ToDecimal(drw["carat"]);
                        m_numSummAmount = Math.Round((Val.ToDecimal(drw["carat"]) * Val.ToDecimal(txtRate.Text)), 2, MidpointRounding.AwayFromZero);
                        objMFGMixSplitProperty.rate = Val.ToDecimal(txtRate.Text);
                        objMFGMixSplitProperty.amount = Val.ToDecimal(m_numSummAmount);

                        objMFGMixSplitProperty.from_pcs = Val.ToInt(drw["pcs"]);
                        objMFGMixSplitProperty.from_carat = Val.ToDecimal(drw["carat"]);
                        objMFGMixSplitProperty.from_lot_id = Val.ToInt64(drw["lot_id"]);
                        objMFGMixSplitProperty.from_kapan_id = Val.ToInt32(drw["kapan_id"]);
                        objMFGMixSplitProperty.from_cut_id = Val.ToInt32(drw["rough_cut_id"]);

                        objMFGMixSplitProperty.form_id = Val.ToInt(m_numForm_id);
                        objMFGMixSplitProperty.to_cut_id = Val.ToInt64(lueCutNo.EditValue);
                        objMFGMixSplitProperty.to_kapan_id = Val.ToInt64(lueKapan.EditValue);
                        objMFGMixSplitProperty.receive_date = Val.DBDate(dtpReceiveDate.Text);

                        objMFGMixSplitProperty.quality_id = Val.ToInt64(drw["quality_id"]);
                        objMFGMixSplitProperty.manager_id = Val.ToInt(drw["manager_id"]);
                        objMFGMixSplitProperty.employee_id = Val.ToInt(drw["employee_id"]);
                        objMFGMixSplitProperty.process_id = Val.ToInt(drw["process_id"]);
                        objMFGMixSplitProperty.sub_process_id = Val.ToInt(drw["sub_process_id"]);
                        objMFGMixSplitProperty.rough_clarity_id = Val.ToInt(drw["rough_clarity_id"]);
                        objMFGMixSplitProperty.rough_sieve_id = Val.ToInt(drw["rough_sieve_id"]);
                        objMFGMixSplitProperty.purity_id = Val.ToInt(drw["purity_id"]);

                        objMFGMixSplitProperty.mix_union_id = MixSplit_IntRes;
                        objMFGMixSplitProperty.union_id = IntRes;
                        objMFGMixSplitProperty.history_union_id = NewHistory_Union_Id;
                        objMFGMixSplitProperty.lot_srno = Lot_SrNo;

                        objMFGMixSplitProperty = objMFGMixSplit.Save_MFGKapanMixData(objMFGMixSplitProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                        New_Lot_ID = objMFGMixSplitProperty.new_lot_id;
                        MixSplit_IntRes = objMFGMixSplitProperty.mix_union_id;
                        IntRes = objMFGMixSplitProperty.union_id;
                        NewHistory_Union_Id = Val.ToInt64(objMFGMixSplitProperty.history_union_id);
                        Lot_SrNo = Val.ToInt64(objMFGMixSplitProperty.lot_srno);
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
                Conn = null;
                Global.Message(ex.ToString());
                if (ex.InnerException != null)
                {
                    Global.Message(ex.InnerException.ToString());
                }
            }
        }

        private void backgroundWorker_KapanMix_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (IntRes > 0)
                {
                    Global.Confirm("Kapan Mix Data Save Succesfully");

                    ClearDetails();

                    //DialogResult result = MessageBox.Show("Do you want to Print Barcode?", "Confirmation", MessageBoxButtons.YesNoCancel);
                    //if (result != DialogResult.Yes)
                    //{
                    //    btnSave.Enabled = true;
                    //    return;
                    //}
                    //else
                    //{
                    //    PrintBarcode();
                    //    btnSave.Enabled = true;
                    //}
                }
                else
                {
                    Global.Confirm("Error In Kapan Mix Data");
                    btnSave.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }

        private void FrmMFGMixSplit_Load(object sender, EventArgs e)
        {
            try
            {
                dtpReceiveDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpReceiveDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpReceiveDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpReceiveDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpReceiveDate.EditValue = DateTime.Now;

                m_dtbKapan = Global.GetKapanAll();

                lueKapan.Properties.DataSource = m_dtbKapan;
                lueKapan.Properties.ValueMember = "kapan_id";
                lueKapan.Properties.DisplayMember = "kapan_no";

                lueMixKapan.Properties.DataSource = m_dtbKapan;
                lueMixKapan.Properties.ValueMember = "kapan_id";
                lueMixKapan.Properties.DisplayMember = "kapan_no";

                m_dtbParam = Global.GetRoughCutAll();

                lueCutNo.Properties.DataSource = m_dtbParam;
                lueCutNo.Properties.ValueMember = "rough_cut_id";
                lueCutNo.Properties.DisplayMember = "rough_cut_no";

                lueMixCutNo.Properties.DataSource = m_dtbParam;
                lueMixCutNo.Properties.ValueMember = "rough_cut_id";
                lueMixCutNo.Properties.DisplayMember = "rough_cut_no";

                // Add By Praful On 29072021

                //DTab_KapanWiseData = Global.GetRoughStockWise(Val.ToInt(0), Val.ToInt32(0));

                // End By Praful On 29072021

                ClearDetails();
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void lueMixKapan_EditValueChanged(object sender, EventArgs e)
        {
            m_dtbParam = new DataTable();
            if (lueMixKapan.Text.ToString() != "")
            {
                m_dtbParam = Global.GetRoughKapanWise_Data(Val.ToInt(lueMixKapan.EditValue));
                //if (m_dtbParam.Rows.Count == 0)
                //{
                //    m_dtbParam = DTab_KapanWiseData;
                //}
            }
            lueMixCutNo.Properties.DataSource = m_dtbParam;
            lueMixCutNo.Properties.ValueMember = "rough_cut_id";
            lueMixCutNo.Properties.DisplayMember = "rough_cut_no";
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
                string Str = "";
                if (Val.DBDate(BLL.GlobalDec.gStrServerDate) != Val.DBDate(dtpReceiveDate.Text))
                {
                    Str = GlobalDec.CheckLockIsOpenOrNot(Val.DBDate(dtpReceiveDate.Text), Val.DBTime(DateTime.Now.ToShortTimeString())); //Val.GetFullTime12());
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
                        dtpReceiveDate.Enabled = true;
                        dtpReceiveDate.Visible = true;
                    }
                }
                btnSave.Enabled = false;

                m_blnsave = true;
                m_blnadd = false;
                if (!ValidateDetails())
                {
                    m_blnsave = false;
                    btnSave.Enabled = true;
                    return;
                }

                DialogResult result = MessageBox.Show("Do you want to save Kapan Mix data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result != DialogResult.Yes)
                {
                    btnSave.Enabled = true;
                    return;
                }

                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                backgroundWorker_KapanMix.RunWorkerAsync();

                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnPopUpStock_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validate_PopUp())
                {

                    DTab_StockData.AcceptChanges();
                    //if (DTab_StockData != null)
                    //{
                    //    if (DTab_StockData.Rows.Count > 0)
                    //    {
                    //        for (int i = 0; i < DTab_StockData.Rows.Count; i++)
                    //        {
                    //            if (Val.ToInt32(DTab_StockData.Rows[i]["rough_cut_id"]) == Val.ToInt64(lueMixCutNo.EditValue))
                    //            {
                    //                Global.Message("Rough Cut No: " + DTab_StockData.Rows[i]["rough_cut_no"] + "\n\nalready added to the list!");
                    //                lueMixCutNo.Text = "";
                    //                lueMixCutNo.Focus();
                    //                return;
                    //            }

                    //        }
                    //    }
                    //}

                    if (lueMixCutNo.Text.Length == 0)
                    {
                        return;
                    }

                    MFGProcessIssue objMFGProcessIssue = new MFGProcessIssue();
                    MFGProcessIssueProperty objMFGProcessIssueProperty = new MFGProcessIssueProperty();
                    objMFGProcessIssueProperty.rough_cut_id = Val.ToInt(lueMixCutNo.EditValue);
                    objMFGProcessIssueProperty.kapan_id = Val.ToInt(lueMixKapan.EditValue);
                    string StrLotList = "";
                    DataTable DTabTemp = new DataTable();

                    DTab_StockData = objMFGProcessIssue.GetKapanMixLiveStock(objMFGProcessIssueProperty);
                    if (GlobalDec.gEmployeeProperty.role_name == "SURAT MAKABLE" || GlobalDec.gEmployeeProperty.role_name == "SURAT SARIN" || GlobalDec.gEmployeeProperty.role_name == "SURAT KATARGAM")
                    {
                        if (DTab_StockData.Rows.Count > 0)
                        {
                            for (int i = 0; i < DTab_StockData.Rows.Count; i++)
                            {
                                if (Val.ToInt(DTab_StockData.Rows[i]["lot_id"]) != 0)
                                {
                                    DataTable DtConfirm = Global.CheckConfirmLot(Val.ToInt(DTab_StockData.Rows[i]["lot_id"]));
                                    if (DTab_StockData.Rows.Count != 0)
                                    {
                                        if (StrLotList.Length > 0)
                                        {
                                            StrLotList = StrLotList + "," + Val.ToInt(DTab_StockData.Rows[i]["lot_id"]);
                                        }
                                        else
                                        {
                                            StrLotList = Val.ToInt(DTab_StockData.Rows[i]["lot_id"]).ToString();
                                        }
                                        //Global.Message("Please Confirm Lot First!!! " + Val.ToInt(DtPending.Rows[i]["lot_id"]));
                                        //DtPending = new DataTable();
                                        //return;
                                    }
                                }

                            }
                            DTab_StockData = DTab_StockData.Select("lot_id in(" + StrLotList + ")").CopyToDataTable();
                        }
                    }
                    //DataTable DTab_Stock = Global.GetStockCutwise(Val.ToInt(lueKapan.EditValue), Val.ToInt(lueCutNo.EditValue));

                    FrmMFGStockConfirm FrmStockConfirm = new FrmMFGStockConfirm();
                    FrmStockConfirm.FrmMFGKapanMixing = this;
                    FrmStockConfirm.DTab = DTab_StockData;
                    FrmStockConfirm.ShowForm(this);
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            FrmMFGLottingSearch FrmSearchLotting = new FrmMFGLottingSearch();
            FrmSearchLotting.FrmMFGKapanMixing = this;
            FrmSearchLotting.ShowForm(this);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearDetails();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            Global.Export("xlsx", dgvDet);
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
                            dgvDet.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvDet.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvDet.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvDet.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvDet.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvDet.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvDet.ExportToCsv(Filepath);
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

        private void MNExportExcel_Click(object sender, EventArgs e)
        {
            Export("xlsx", "Export to Excel", "Excel files 97-2003 (Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*");
        }
        private void dgvDet_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                if (Val.ToDecimal(clmAmount.SummaryItem.SummaryValue) > 0 && Val.ToDecimal(clmCarat.SummaryItem.SummaryValue) > 0)
                {
                    m_numSummRate = Math.Round((Val.ToDecimal(clmAmount.SummaryItem.SummaryValue) / Val.ToDecimal(clmCarat.SummaryItem.SummaryValue)), 2, MidpointRounding.AwayFromZero);
                    txtRate.Text = m_numSummRate.ToString();
                    txtAmount.Text = Val.Val(clmAmount.SummaryItem.SummaryValue).ToString();
                    txtPcs.Text = Val.ToInt(clmPcs.SummaryItem.SummaryValue).ToString();
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
        #endregion

        #region Function

        private void PrintBarcode()
        {
            BarcodePrint printBarCode = new BarcodePrint();
            for (int i = 0; i < dtBarcodePrint.Rows.Count; i++)
            {
                if (dtBarcodePrint.Rows[i]["lot_id"] != null && dtBarcodePrint.Rows[i]["carat"].ToString() != "")
                {
                    printBarCode.AddPkt(dtBarcodePrint.Rows[i]["kapan_no"].ToString(), dtBarcodePrint.Rows[i]["sr_no"].ToString(), Val.ToString(dtBarcodePrint.Rows[i]["receive_date"]),
                        Val.ToInt(dtBarcodePrint.Rows[i]["lot_id"]), Val.ToInt(dtBarcodePrint.Rows[i]["pcs"]), Val.ToDecimal(dtBarcodePrint.Rows[i]["carat"]), true);
                }
            }
            printBarCode.PrintTSC();
        }
        private bool ClearDetails()
        {
            bool blnReturn = true;
            try
            {
                dtpReceiveDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpReceiveDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpReceiveDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpReceiveDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpReceiveDate.EditValue = DateTime.Now;
                lueKapan.EditValue = System.DBNull.Value;
                lueCutNo.EditValue = System.DBNull.Value;
                lueMixKapan.EditValue = System.DBNull.Value;
                lueMixCutNo.EditValue = System.DBNull.Value;
                txtPcs.Text = string.Empty;
                txtRate.Text = string.Empty;
                txtAmount.Text = string.Empty;
                btnSave.Enabled = true;
                DTab_StockData.Rows.Clear();
                DTab_StockData.AcceptChanges();
                m_dtbStock.Rows.Clear();
                m_dtbStock.AcceptChanges();
                grdDet.DataSource = null;
                txtPassword.Text = "";
                grdDet.RefreshDataSource();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
        }
        private bool Validate_PopUp()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                var result = DateTime.Compare(Convert.ToDateTime(dtpReceiveDate.Text), DateTime.Today);
                if (result > 0)
                {
                    lstError.Add(new ListError(5, " Recieve Date Not Be Greater Than Today Date"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        dtpReceiveDate.Focus();
                    }
                }
                if (Val.ToString(dtpReceiveDate.Text) == string.Empty)
                {
                    lstError.Add(new ListError(22, "Date"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        dtpReceiveDate.Focus();
                    }
                }
                if (lueMixKapan.Text == "")
                {
                    lstError.Add(new ListError(13, "Kapan No"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueMixKapan.Focus();
                    }
                }
                if (lueMixCutNo.Text == "")
                {
                    lstError.Add(new ListError(13, "Rough Cut No"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueMixCutNo.Focus();
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
                m_dtbStock.AcceptChanges();
                DTabTemp = Stock_Data.Copy();

                if (m_dtbStock != null)
                {
                    if (m_dtbStock.Rows.Count > 0)
                    {
                        for (int i = 0; i < m_dtbStock.Rows.Count; i++)
                        {
                            for (int j = 0; j < DTabTemp.Rows.Count; j++)
                            {
                                if (m_dtbStock.Rows[i]["lot_id"].ToString() == DTabTemp.Rows[j]["lot_id"].ToString())
                                {
                                    Global.Message(m_dtbStock.Rows[i]["lot_id"].ToString() + " = Lot ID already added to the Issue list!");
                                    return;
                                }
                            }
                        }
                    }
                }
                if (m_dtbStock.Rows.Count > 0)
                {
                    DTabTemp = Stock_Data.Copy();
                    m_dtbStock.Merge(DTabTemp);
                }
                else
                {
                    m_dtbStock = Stock_Data.Copy();
                }

                grdDet.DataSource = m_dtbStock;
                grdDet.RefreshDataSource();
                dgvDet.BestFitColumns();
                CalculateSummary();
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
            }
        }
        private void CalculateSummary()
        {
            dgvDet.PostEditor();
            dgvDet.RefreshData();
            DTab_StockData.AcceptChanges();

            foreach (DataRow DRow in DTab_StockData.Rows)
            {
                IntTotalLot++;
                IntTotalPcs += Val.ToInt(DRow["pcs"]);
                DblTotalCarat += Val.Val(DRow["carat"]);
            }
        }
        public void FillGrid(int UnionId)
        {
            m_unionId = UnionId;
            DTab_StockData = new DataTable();
            m_dtbLotMixSplit = new DataTable();
            DTab_StockData = objProcessRecieve.GetKapanMainData(UnionId);
            if (DTab_StockData.Rows.Count > 0)
            {
                grdDet.DataSource = DTab_StockData;
                grdDet.RefreshDataSource();
                dgvDet.BestFitColumns();
                CalculateSummary();
                btnSave.Enabled = false;
            }
        }
        private bool DeleteDetail()
        {
            bool blnReturn = true;
            MFGLotSplit objMFGLotSplit = new MFGLotSplit();
            MFGMixSplitProperty objMFGMixSplitProperty = new MFGMixSplitProperty();
            Conn = new BeginTranConnection(true, false);
            try
            {
                //if (Val.ToString(lblMode.Tag) != "")
                //{
                //DialogResult result = MessageBox.Show("Do you want to Delete Kapan Mixing data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                //if (result != DialogResult.Yes)
                //{
                //    blnReturn = false;
                //    return blnReturn;
                //}

                //objMFGPurchaseProperty.invoice_no = Val.ToString(lblMode.Tag);
                DataTable DeleteData = (DataTable)grdDet.DataSource;
                //int IntRes = objMFGLotSplit.Delete_KapanMixing(objMFGMixSplitProperty, DeleteData, DLL.GlobalDec.EnumTran.Continue, Conn);

                Conn.Inter1.Commit();

                if (IntRes == -1)
                {
                    Global.Confirm("Error In Kapan Mixing");
                    IntRes = -1;
                    Conn.Inter1.Rollback();
                    Conn = null;
                    return blnReturn;
                    //txtPartyInvoiceNo.Focus();
                }
                else
                {
                    Global.Confirm("Kapan Mixing Data Delete Successfully");
                    ClearDetails();

                }
                //}
                //else
                //{
                //    Global.Message("Invoice ID not found");
                //    blnReturn = false;
                //}
            }
            catch (Exception ex)
            {
                IntRes = -1;
                Conn.Inter1.Rollback();
                Conn = null;
                General.ShowErrors(ex.ToString());
                blnReturn = false;
            }
            finally
            {
                objMFGMixSplitProperty = null;
            }

            return blnReturn;
        }
        #endregion

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_unionId > 0)
                {
                    ObjPer.SetFormPer();
                    if (ObjPer.AllowDelete == false)
                    {
                        Global.Message(BLL.GlobalDec.gStrPermissionDelMsg);
                        return;
                    }
                    btnDelete.Enabled = false;
                    int count = 0;
                    count = objLotSplitReceive.CheckKapanMixing(m_unionId);
                    if (count == 0)
                    {
                        DialogResult result = MessageBox.Show("Do you want to Delete Kapan Mixing data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                        if (result != DialogResult.Yes)
                        {
                            btnDelete.Enabled = true;
                            return;
                        }
                    }
                    else
                    {
                        Global.Message("Kapan Mixing is not last Process");
                        btnDelete.Enabled = true;
                        return;
                    }
                    DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                    PanelLoading.Visible = true;
                    backgroundWorker_KapanMixDelete.RunWorkerAsync();
                    btnDelete.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }

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

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            m_dtbKapan = Global.GetKapanAll();

            lueKapan.Properties.DataSource = m_dtbKapan;
            lueKapan.Properties.ValueMember = "kapan_id";
            lueKapan.Properties.DisplayMember = "kapan_no";

            lueMixKapan.Properties.DataSource = m_dtbKapan;
            lueMixKapan.Properties.ValueMember = "kapan_id";
            lueMixKapan.Properties.DisplayMember = "kapan_no";

            m_dtbParam = Global.GetRoughCutAll();

            lueCutNo.Properties.DataSource = m_dtbParam;
            lueCutNo.Properties.ValueMember = "rough_cut_id";
            lueCutNo.Properties.DisplayMember = "rough_cut_no";

            lueMixCutNo.Properties.DataSource = m_dtbParam;
            lueMixCutNo.Properties.ValueMember = "rough_cut_id";
            lueMixCutNo.Properties.DisplayMember = "rough_cut_no";
        }

        private void backgroundWorker_KapanMixDelete_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            MFGLotSplit objMFGLotSplit = new MFGLotSplit();
            MFGMixSplitProperty objMFGMixSplitProperty = new MFGMixSplitProperty();
            Conn = new BeginTranConnection(true, false);
            try
            {
                //DataTable DeleteData = (DataTable)grdDet.DataSource;
                //int IntRes = objMFGLotSplit.Delete_KapanMixing(objMFGMixSplitProperty, DeleteData, DLL.GlobalDec.EnumTran.Continue, Conn);

                DataTable DeleteData = (DataTable)grdDet.DataSource;

                int IntCounter = 0;
                int Count_New = 0;
                int TotalCount = DeleteData.Rows.Count;
                New_IntRes = 0;
                int count = 0;

                foreach (DataRow drw in DeleteData.Rows)
                {
                    objMFGMixSplitProperty.lot_id = Val.ToInt64(drw["lot_id"]);
                    objMFGMixSplitProperty.count = Val.ToInt32(count);

                    New_IntRes = objMFGLotSplit.Delete_KapanMixing(objMFGMixSplitProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                    count = count + 1;

                    Count_New++;
                    IntCounter++;
                    SetControlPropertyValue(lblProgressCount, "Text", Count_New.ToString() + "" + "/" + "" + TotalCount.ToString() + " Completed....");
                }
                Conn.Inter1.Commit();
            }
            catch (Exception ex)
            {
                IntRes = -1;
                Conn.Inter1.Rollback();
                Conn = null;
                btnDelete.Enabled = true;
                General.ShowErrors(ex.ToString());
            }
            finally
            {
                objMFGMixSplitProperty = null;
                btnDelete.Enabled = true;
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

        private void backgroundWorker_KapanMixDelete_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                PanelLoading.Visible = false;
                if (New_IntRes > 0)
                {
                    Global.Confirm("Kapan Mixing Data Delete Successfully");
                    ClearDetails();
                }
                else
                {
                    Global.Confirm("Error In Kapan Mixing");

                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }

        private bool ValidateDetails_LotID()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (Val.ToString(dtpReceiveDate.Text) == string.Empty)
                {
                    lstError.Add(new ListError(22, "Date"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        dtpReceiveDate.Focus();
                    }
                }

                if (lueMixKapan.Text == "")
                {
                    lstError.Add(new ListError(13, "Kapan No"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueMixKapan.Focus();
                    }
                }
                if (lueMixCutNo.Text == "")
                {
                    lstError.Add(new ListError(13, "Rough Cut No"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueMixCutNo.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                lstError.Add(new ListError(ex));
            }
            return (!(BLL.General.ShowErrors(lstError)));
        }

        private void txtLotId_Validated(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateDetails_LotID())
                {
                    return;
                }

                DTab_StockData.AcceptChanges();
                if (DTab_StockData != null)
                {
                    if (DTab_StockData.Rows.Count > 0)
                    {
                        DataRow[] dr = DTab_StockData.Select("lot_id = " + Val.ToInt64(txtLotId.Text));

                        if (dr.Length > 0)
                        {
                            Global.Message("Lot ID already added to the Kapan Mix list!");
                            txtLotId.Text = "";
                            txtLotId.Focus();
                            return;
                        }

                        //for (int i = 0; i < DTab_StockData.Rows.Count; i++)
                        //{
                        //    if (DTab_StockData.Rows[i]["lot_id"].ToString() == txtLotId.Text)
                        //    {
                        //        Global.Message("Lot ID already added to the Kapan Mix list!");
                        //        txtLotId.Text = "";
                        //        txtLotId.Focus();
                        //        return;
                        //    }
                        //}
                    }
                }

                if (txtLotId.Text.Length == 0)
                {
                    return;
                }

                MFGProcessIssue objMFGProcessIssue = new MFGProcessIssue();
                MFGProcessIssueProperty objMFGProcessIssueProperty = new MFGProcessIssueProperty();
                objMFGProcessIssueProperty.rough_cut_id = Val.ToInt(lueMixCutNo.EditValue);
                objMFGProcessIssueProperty.kapan_id = Val.ToInt(lueMixKapan.EditValue);
                objMFGProcessIssueProperty.lot_id = Val.ToInt64(txtLotId.Text);

                if (DTab_StockData.Rows.Count > 0)
                {
                    DataTable DTabTemp = new DataTable();

                    DataTable DTab_ValidateLotID = objMFGProcessIssue.GetKapanMixLiveStockData(objMFGProcessIssueProperty);// objJangedReturn.Stock_GetData(Val.ToInt64(txtLotId.Text), 0, Val.ToInt32(txtJangedNo.Text));

                    if (DTab_ValidateLotID.Rows.Count > 0)
                    {
                    }
                    else
                    {
                        Global.Message("Lot ID Not In Your Stock");
                        txtLotId.Text = "";
                        txtLotId.Focus();
                        return;
                    }

                    //DTabTemp = objMFGProcessIssue.GetKapanMixLiveStockData(objMFGProcessIssueProperty);

                    if (DTab_ValidateLotID.Rows.Count > 0)
                    {
                        txtLotId.Text = "";
                        txtLotId.Focus();
                    }
                    DTab_StockData.Merge(DTab_ValidateLotID);
                }
                else
                {
                    //DataTable DTab_ValidateLotID = objMFGProcessIssue.GetKapanMixLiveStockData(objMFGProcessIssueProperty);
                    DTab_StockData = objMFGProcessIssue.GetKapanMixLiveStockData(objMFGProcessIssueProperty);

                    if (DTab_StockData.Rows.Count > 0)
                    {
                    }
                    else
                    {
                        Global.Message("Lot ID Not In Your Stock");
                        txtLotId.Text = "";
                        txtLotId.Focus();
                        return;
                    }

                    //DTab_StockData = objMFGProcessIssue.GetKapanMixLiveStockData(objMFGProcessIssueProperty);

                    if (DTab_StockData.Rows.Count > 0)
                    {
                        txtLotId.Text = "";
                        txtLotId.Focus();
                    }
                }
                grdDet.DataSource = DTab_StockData;
                grdDet.RefreshDataSource();
                dgvDet.BestFitColumns();
                (grdDet.FocusedView as GridView).MoveLast();
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }

        private void txtLotId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
