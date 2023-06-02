using BLL;
using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using DERP.Master.MFG;
using DevExpress.XtraEditors;
using DREP.Master.MFG;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Transaction.MFG
{
    public partial class FRMMFGOnlySplit : DevExpress.XtraEditors.XtraForm
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
        //DataTable DTab_KapanWiseData = new DataTable();
        private List<Control> _tabControls = new List<Control>();

        bool m_blnadd = new bool();
        bool m_blnsave = new bool();
        int m_Srno = 1;
        int m_update_srno = 1;
        string m_cut_no = "";
        int m_numForm_id = 0;
        int IntTotalLot = 0;
        double DblTotalCarat = 0.00;
        Int64 IntRes;
        Int64 MixSplit_IntRes;
        #endregion

        #region Constructor
        public FRMMFGOnlySplit()
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

        #region Validation
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();

            try
            {
                if (m_blnsave)
                {
                    if (m_dtbLotMixSplit.Rows.Count == 0)
                    {
                        lstError.Add(new ListError(22, "Record"));
                        if (!blnFocus)
                        {
                            blnFocus = true;

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
                    if (Val.ToString(dtpReceiveDate.Text) == string.Empty)
                    {
                        lstError.Add(new ListError(22, "Date"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            dtpReceiveDate.Focus();
                        }
                    }

                    if (Val.ToDecimal(txtBalanceCarat.Text) != m_dtbLotMixSplit.AsEnumerable().Sum(x => Val.ToDecimal(x[m_dtbLotMixSplit.Columns["balance_carat"]])))
                    {
                        lstError.Add(new ListError(36, "Transfer Carat not Equal to Balance Carat."));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                        }
                    }
                }

                if (m_blnadd)
                {
                    if (lueCutNo.Text == "")
                    {
                        lstError.Add(new ListError(13, "Cut No"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueCutNo.Focus();
                        }
                    }
                    if (lueQuality.Text == "")
                    {
                        lstError.Add(new ListError(13, "Quality Name"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueQuality.Focus();
                        }
                    }
                    if (lueClarity.Text == "")
                    {
                        lstError.Add(new ListError(13, "Clarity Name"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueClarity.Focus();
                        }
                    }
                    if (lueRoughSieve.Text == "")
                    {
                        lstError.Add(new ListError(13, "Sieve Name"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueRoughSieve.Focus();
                        }
                    }
                    if (Val.ToDecimal(txtCarat.Text) == 0)
                    {
                        lstError.Add(new ListError(12, "Carat"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtCarat.Focus();
                        }
                    }

                    if (Val.ToDecimal(txtRate.Text) == 0)
                    {
                        lstError.Add(new ListError(12, "Rate"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtRate.Focus();
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
                DialogResult result = MessageBox.Show("Do you want to save data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result != DialogResult.Yes)
                {
                    btnSave.Enabled = true;
                    return;
                }

                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                backgroundWorker_LotSplit.RunWorkerAsync();

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
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearDetails();
            //grdProcessWeightLossRecieve.DataSource = null;
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            Global.Export("xlsx", dgvLotSplit);
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (AddInGrid())
                {
                    lueQuality.EditValue = System.DBNull.Value;
                    lueClarity.EditValue = System.DBNull.Value;
                    lueRoughSieve.EditValue = System.DBNull.Value;
                    txtCarat.Text = string.Empty;
                    txtAmount.Text = string.Empty;
                    //txtWeightPlus.Text = string.Empty;
                    //txtWeightLoss.Text = string.Empty;
                    //txtBalanceCarat.Text = "0";
                    txtRate.Text = "0";
                    lueQuality.Focus();
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                //blnReturn = false;
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

                m_dtbParam = Global.GetRoughCutAll();

                lueCutNo.Properties.DataSource = m_dtbParam;
                lueCutNo.Properties.ValueMember = "rough_cut_id";
                lueCutNo.Properties.DisplayMember = "rough_cut_no";

                Global.LOOKUPRoughSieve(lueRoughSieve);
                Global.LOOKUPQuality(lueQuality);
                Global.LOOKUPClarity(lueClarity);

                ClearDetails();

                // Add By Praful On 29072021

                //DTab_KapanWiseData = Global.GetRoughStockWise(Val.ToInt(0), Val.ToInt32(0));

                // End By Praful On 29072021

                txtLotID.Focus();
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void lueCutNo_EditValueChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (!m_blnflag)
            //    {
            //        if (lueCutNo.EditValue != System.DBNull.Value)
            //        {
            //            if (m_dtbParam.Rows.Count > 0)
            //            {
            //                DataRow[] dr = m_dtbParam.Select("rough_cut_no ='" + Val.ToString(lueCutNo.Text) + "'");
            //                if (dr.Length > 0)
            //                {
            //                    txtLotIDSplit.Text = Val.ToString(dr[0]["lot_id"]);
            //                }
            //            }
            //        }
            //    }
            //    else
            //    {
            //        m_blnflag = false;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    BLL.General.ShowErrors(ex);
            //    return;
            //}
        }
        private void lueRoughSieve_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmMfgRoughSieve frmRoughSieve = new FrmMfgRoughSieve();
                frmRoughSieve.ShowDialog();
                Global.LOOKUPRoughSieve(lueRoughSieve);
            }
        }
        private void lueClarity_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmMfgRoughClarityMaster frmClarity = new FrmMfgRoughClarityMaster();
                frmClarity.ShowDialog();
                Global.LOOKUPClarity(lueClarity);
            }
        }
        private void lueQuality_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmMfgQualityMaster frmRoughQuality = new FrmMfgQualityMaster();
                frmRoughQuality.ShowDialog();
                Global.LOOKUPQuality(lueQuality);
            }
        }
        private void txtRate_EditValueChanged(object sender, EventArgs e)
        {
            txtAmount.Text = Val.ToString(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text));
        }
        private void txtWeightPlus_EditValueChanged(object sender, EventArgs e)
        {
            txtAmount.Text = Val.ToString(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text));
        }
        private void dgvMixSplit_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                DataTable dtAmount = new DataTable();
                dtAmount = (DataTable)grdLotSplit.DataSource;

                decimal rate = 0;
                decimal carat = 0;
                decimal amount = 0;

                string column = "";
                for (int j = 0; j <= dtAmount.Columns.Count - 1; j++)
                {
                    if (dtAmount.Columns[j].ToString().Contains("balance_carat"))
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
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        private void grdLotSplit_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                if (Global.Confirm("Are you sure delete selected row?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    dgvLotSplit.DeleteRow(dgvLotSplit.GetRowHandle(dgvLotSplit.FocusedRowHandle));
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

        DataTable DTab_StockData = new DataTable();
        private void txtLotID_Validated(object sender, EventArgs e)
        {
            try
            {
                DTab_StockData.AcceptChanges();
                if (DTab_StockData != null)
                {
                    if (DTab_StockData.Rows.Count > 0)
                    {
                        DataRow[] dr = DTab_StockData.Select("lot_id = " + Val.ToInt64(txtLotID.Text));

                        if (dr.Length > 0)
                        {
                            Global.Message(Val.ToInt64(txtLotID.Text) + " = Lot ID already added to the Issue list!");
                            txtLotID.Text = "";
                            txtLotID.Focus();
                            return;
                        }

                        //for (int i = 0; i < DTab_StockData.Rows.Count; i++)
                        //{
                        //    if (DTab_StockData.Rows[i]["lot_id"].ToString() == txtLotID.Text)
                        //    {
                        //        Global.Message("Lot ID already added to the Issue list!");
                        //        txtLotID.Text = "";
                        //        txtLotID.Focus();
                        //        return;
                        //    }
                        //}
                    }
                }

                if (txtLotID.Text.Length == 0)
                {
                    return;
                }

                DTab_StockData = objMFGMixSplit.Live_Stock_Split_GetData(Val.ToInt64(txtLotID.Text));

                if (DTab_StockData.Rows.Count > 0)
                {
                    lueKapan.EditValue = Val.ToInt64(DTab_StockData.Rows[0]["kapan_id"]);
                    lueCutNo.EditValue = Val.ToInt64(DTab_StockData.Rows[0]["rough_cut_id"]);
                    txtBalancePcs.Text = Val.ToDecimal(DTab_StockData.Rows[0]["balance_pcs"]).ToString();
                    txtBalanceCarat.Text = Val.ToDecimal(DTab_StockData.Rows[0]["balance_carat"]).ToString();
                    txtCarat.Text = Val.ToDecimal(DTab_StockData.Rows[0]["balance_carat"]).ToString();
                    txtRate.Text = Val.ToDecimal(DTab_StockData.Rows[0]["rate"]).ToString();
                    txtAmount.Text = Val.ToDecimal(DTab_StockData.Rows[0]["amount"]).ToString();
                    //txtLotID.Text = "";
                    //txtLotID.Focus();
                }
                else
                {
                    Global.Message("Lot ID Not found");
                    txtLotID.Text = "";
                    txtLotID.Focus();
                    return;
                }

                CalculateSummary();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void CalculateSummary()
        {
            DTab_StockData.AcceptChanges();

            foreach (DataRow DRow in DTab_StockData.Rows)
            {
                // Total Summary Details
                IntTotalLot++;
                // Comment By Praful On 11102017                
                DblTotalCarat += Val.Val(DRow["balance_carat"]);
            }
        }
        private void backgroundWorker_LotSplit_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {

                MFGLotSplit objMFGLotSplit = new MFGLotSplit();
                MFGMixSplitProperty objMFGMixSplitProperty = new MFGMixSplitProperty();
                Conn = new BeginTranConnection(true, false);

                try
                {
                    IntRes = 0;
                    MixSplit_IntRes = 0;
                    //Int64 NewHistory_Union_Id = 0;
                    foreach (DataRow drw in m_dtbLotMixSplit.Rows)
                    {
                        objMFGMixSplitProperty.receive_date = Val.DBDate(dtpReceiveDate.Text);
                        //objMFGMixSplitProperty.rough_lot_id = Val.ToInt(drw["lot_id"]);
                        objMFGMixSplitProperty.from_lot_id = Val.ToInt64(txtLotID.Text);
                        objMFGMixSplitProperty.rough_cut_id = Val.ToInt(drw["rough_cut_id"]);
                        objMFGMixSplitProperty.rough_sieve_id = Val.ToInt64(drw["rough_sieve_id"]);
                        objMFGMixSplitProperty.quality_id = Val.ToInt64(drw["quality_id"]);
                        objMFGMixSplitProperty.kapan_id = Val.ToInt64(drw["kapan_id"]);
                        objMFGMixSplitProperty.rough_clarity_id = Val.ToInt64(drw["rough_clarity_id"]);
                        objMFGMixSplitProperty.form_id = Val.ToInt(m_numForm_id);

                        objMFGMixSplitProperty.from_pcs = Val.ToInt(txtBalancePcs.Text);
                        objMFGMixSplitProperty.from_carat = Val.ToDecimal(txtBalanceCarat.Text);

                        objMFGMixSplitProperty.pcs = Val.ToInt(0);
                        objMFGMixSplitProperty.carat = Val.ToDecimal(drw["balance_carat"]);
                        //objMFGMixSplitProperty.loss_carat = Val.ToDecimal(drw["loss_carat"]);
                        //objMFGMixSplitProperty.plus_carat = Val.ToDecimal(drw["plus_carat"]);
                        objMFGMixSplitProperty.rate = Val.ToDecimal(drw["rate"]);
                        objMFGMixSplitProperty.amount = Val.ToDecimal(drw["amount"]);
                        objMFGMixSplitProperty.union_id = IntRes;
                        objMFGMixSplitProperty.mix_union_id = MixSplit_IntRes;
                        //objMFGMixSplitProperty.history_union_id = NewHistory_Union_Id;


                        objMFGMixSplitProperty = objMFGMixSplit.Save_MFGLotSplit(objMFGMixSplitProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                        IntRes = objMFGMixSplitProperty.union_id;
                        MixSplit_IntRes = objMFGMixSplitProperty.mix_union_id;
                        //NewHistory_Union_Id = Val.ToInt64(objMFGMixSplitProperty.history_union_id);
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
        private void backgroundWorker_LotSplit_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (IntRes > 0)
                {
                    Global.Confirm("Lot Split Recieve Data Save Succesfully");
                    btnSave.Enabled = true;
                    ClearDetails();
                }
                else
                {
                    Global.Confirm("Error In Lot Split Recieve");
                    btnSave.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }

        #endregion

        #region Function
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
        private bool GenerateLotSplitDetails()
        {
            bool blnReturn = true;
            try
            {
                if (m_dtbLotMixSplit.Rows.Count > 0)
                    m_dtbLotMixSplit.Rows.Clear();

                m_dtbLotMixSplit = new DataTable();

                //m_dtbLotMixSplit.Columns.Add("recieve_id", typeof(int));
                //m_dtbLotMixSplit.Columns.Add("recieve_date", typeof(DateTime));
                m_dtbLotMixSplit.Columns.Add("lot_id", typeof(int));
                m_dtbLotMixSplit.Columns.Add("rough_cut_no", typeof(string));
                m_dtbLotMixSplit.Columns.Add("rough_cut_id", typeof(int));
                m_dtbLotMixSplit.Columns.Add("kapan_no", typeof(string));
                m_dtbLotMixSplit.Columns.Add("kapan_id", typeof(int));
                m_dtbLotMixSplit.Columns.Add("quality_name", typeof(string));
                m_dtbLotMixSplit.Columns.Add("quality_id", typeof(int));
                m_dtbLotMixSplit.Columns.Add("rough_clarity_name", typeof(string));
                m_dtbLotMixSplit.Columns.Add("rough_clarity_id", typeof(int));
                m_dtbLotMixSplit.Columns.Add("sieve_name", typeof(string));
                m_dtbLotMixSplit.Columns.Add("rough_sieve_id", typeof(int));

                //m_dtbLotMixSplit.Columns.Add("pcs", typeof(int)).DefaultValue = 0;
                m_dtbLotMixSplit.Columns.Add("balance_carat", typeof(decimal)).DefaultValue = 0;
                //m_dtbLotMixSplit.Columns.Add("plus_carat", typeof(decimal)).DefaultValue = 0;
                //m_dtbLotMixSplit.Columns.Add("loss_carat", typeof(decimal)).DefaultValue = 0;
                m_dtbLotMixSplit.Columns.Add("sr_no", typeof(decimal)).DefaultValue = 1;
                m_dtbLotMixSplit.Columns.Add("rate", typeof(decimal)).DefaultValue = 0;
                m_dtbLotMixSplit.Columns.Add("amount", typeof(decimal)).DefaultValue = 0;

                grdLotSplit.DataSource = m_dtbLotMixSplit;
                grdLotSplit.Refresh();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
        }
        private bool ClearDetails()
        {
            bool blnReturn = true;
            try
            {
                //btnSave.Enabled = true;
                if (!GenerateLotSplitDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }

                dtpReceiveDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpReceiveDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpReceiveDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpReceiveDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpReceiveDate.EditValue = DateTime.Now;

                lueKapan.EditValue = System.DBNull.Value;
                lueCutNo.EditValue = System.DBNull.Value;

                lueQuality.EditValue = System.DBNull.Value;
                lueClarity.EditValue = System.DBNull.Value;
                lueRoughSieve.EditValue = System.DBNull.Value;

                txtCarat.Text = string.Empty;
                txtRate.Text = string.Empty;
                txtAmount.Text = string.Empty;
                //txtWeightLoss.Text = string.Empty;
                //txtWeightPlus.Text = string.Empty;


                DTab_StockData.Rows.Clear();
                DTab_StockData.AcceptChanges();

                txtLotID.Text = string.Empty;
                txtBalanceCarat.Text = string.Empty;
                txtBalancePcs.Text = string.Empty;

                txtLotID.Focus();
                m_Srno = 1;
                m_update_srno = 0;
                btnAdd.Text = "&Add";
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
        }
        private bool AddInGrid()
        {
            bool blnReturn = true;

            try
            {
                m_blnadd = true;
                m_blnsave = false;
                if (!ValidateDetails())
                {
                    m_blnadd = false;
                    blnReturn = false;
                    return blnReturn;
                }
                if (btnAdd.Text == "&Add")
                {

                    DataRow drwNew = m_dtbLotMixSplit.NewRow();
                    //int numPcs = Val.ToInt(txtPcs.Text);
                    decimal numCarat = Val.ToDecimal(txtCarat.Text);
                    //decimal numLossCarat = Val.ToDecimal(txtWeightLoss.Text);
                    //decimal numPlusCarat = Val.ToDecimal(txtWeightPlus.Text);
                    decimal numRate = Val.ToDecimal(txtRate.Text);
                    decimal numAmount = Val.ToDecimal(txtAmount.Text);


                    drwNew["rough_cut_no"] = Val.ToString(lueCutNo.Text);
                    drwNew["rough_cut_id"] = Val.ToInt(lueCutNo.EditValue);
                    drwNew["lot_id"] = Val.ToInt64(txtLotID.Text);

                    drwNew["kapan_id"] = Val.ToInt(lueKapan.EditValue);
                    drwNew["kapan_no"] = Val.ToString(lueKapan.Text);

                    drwNew["rough_sieve_id"] = Val.ToInt(lueRoughSieve.EditValue);
                    drwNew["sieve_name"] = Val.ToString(lueRoughSieve.Text);

                    drwNew["rough_clarity_id"] = Val.ToInt(lueClarity.EditValue);
                    drwNew["rough_clarity_name"] = Val.ToString(lueClarity.Text);
                    drwNew["quality_id"] = Val.ToInt(lueQuality.EditValue);
                    drwNew["quality_name"] = Val.ToString(lueQuality.Text);

                    //drwNew["pcs"] = numPcs;
                    drwNew["balance_carat"] = numCarat;
                    //drwNew["loss_carat"] = numLossCarat;
                    //drwNew["plus_carat"] = numPlusCarat;
                    drwNew["rate"] = numRate;
                    drwNew["amount"] = numAmount;
                    drwNew["sr_no"] = m_Srno;
                    m_dtbLotMixSplit.Rows.Add(drwNew);
                    m_Srno++;
                }
                else if (btnAdd.Text == "&Update")
                {

                    if (m_dtbLotMixSplit.Select("sr_no ='" + Val.ToInt(m_update_srno) + "'").Length > 0)
                    {
                        for (int i = 0; i < m_dtbLotMixSplit.Rows.Count; i++)
                        {
                            if (m_dtbLotMixSplit.Select("rough_cut_no ='" + m_cut_no + "' AND sr_no = '" + m_update_srno + "'").Length > 0)
                            {
                                if (m_dtbLotMixSplit.Rows[m_update_srno - 1]["rough_cut_no"].ToString() == m_cut_no.ToString())
                                {
                                    m_dtbLotMixSplit.Rows[m_update_srno - 1]["lot_id"] = Val.ToString(txtLotID.Text);
                                    m_dtbLotMixSplit.Rows[m_update_srno - 1]["sieve_name"] = Val.ToString(lueRoughSieve.Text);
                                    m_dtbLotMixSplit.Rows[m_update_srno - 1]["rough_sieve_id"] = Val.ToInt(lueRoughSieve.EditValue);
                                    m_dtbLotMixSplit.Rows[m_update_srno - 1]["quality_name"] = Val.ToString(lueQuality.Text);
                                    m_dtbLotMixSplit.Rows[m_update_srno - 1]["quality_id"] = Val.ToInt(lueQuality.EditValue);
                                    m_dtbLotMixSplit.Rows[m_update_srno - 1]["rough_clarity_name"] = Val.ToString(lueClarity.Text);
                                    m_dtbLotMixSplit.Rows[m_update_srno - 1]["rough_clarity_id"] = Val.ToInt(lueClarity.EditValue);

                                    //m_dtbLotMixSplit.Rows[m_update_srno - 1]["pcs"] = Val.ToInt(txtPcs.Text).ToString();
                                    m_dtbLotMixSplit.Rows[m_update_srno - 1]["balance_carat"] = Val.ToDecimal(txtCarat.Text).ToString();
                                    //m_dtbLotMixSplit.Rows[m_update_srno - 1]["loss_carat"] = Val.ToDecimal(txtWeightLoss.Text).ToString();
                                    //m_dtbLotMixSplit.Rows[m_update_srno - 1]["plus_carat"] = Val.ToDecimal(txtWeightPlus.Text).ToString();
                                    m_dtbLotMixSplit.Rows[m_update_srno - 1]["rate"] = Val.ToDecimal(txtRate.Text);
                                    m_dtbLotMixSplit.Rows[m_update_srno - 1]["amount"] = Val.ToDecimal(txtAmount.Text);
                                    //m_flag = 0;
                                    break;
                                }
                            }
                        }
                    }
                    btnAdd.Text = "&Add";
                }
                dgvLotSplit.MoveLast();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
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
                            dgvLotSplit.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvLotSplit.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvLotSplit.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvLotSplit.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvLotSplit.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvLotSplit.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvLotSplit.ExportToCsv(Filepath);
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
