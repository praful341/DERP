using BLL;
using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using DERP.Master;
using DERP.Master.MFG;
using DERP.Report.Barcode_Print;
using DevExpress.XtraEditors;
using DREP.Master.MFG;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Transaction.MFG
{
    public partial class FrmMFGMixSplitLottingSarin : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        FormEvents objBOFormEvents = new FormEvents();
        Validation Val = new Validation();
        BLL.FormPer ObjPer = new BLL.FormPer();
        DataTable m_dtbDetail = new DataTable();
        DataTable m_dtbParam = new DataTable();
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);
        DataTable m_dtbSubProcess = new DataTable();
        BLL.BeginTranConnection Conn;
        DataTable m_dtbLotMixSplit = new DataTable();
        Control _NextEnteredControl = new Control();
        DataTable DtControlSettings = new DataTable();
        MFGLotSplit objLotSplitReceive = new MFGLotSplit();
        DataTable m_dtOutstanding = new DataTable();
        DataTable m_dtbKapan = new DataTable();
        DataTable dtBarcodePrint = new DataTable();
        MFGMixSplit objMFGMixSplit = new MFGMixSplit();
        private List<Control> _tabControls = new List<Control>();
        MFGProcessReceive objProcessRecieve = new MFGProcessReceive();
        MFGLiveStock objLiveStock = new MFGLiveStock();
        DataTable dtIss = new DataTable();
        IDataObject PasteclipData = Clipboard.GetDataObject();
        String PasteData = "";
        DataTable DtPending = new DataTable();
        //DataTable DTab_KapanWiseData = new DataTable();

        int Process_Id = 0;

        bool m_blnadd = new bool();
        bool m_blnsave = new bool();
        int m_Srno = 1;
        int m_update_srno = 1;
        string m_cut_no = "";
        decimal m_numSummRate = 0;
        int m_numForm_id = 0;
        int IntTotalLot = 0;
        double DblTotalCarat = 0.00;
        int IntTotalPcs = 0;
        Int64 IntRes;
        Int64 IntRes_MixSplit;
        Int64 Receive_IntRes;
        Int64 Issue_IntRes;
        Int64 New_Lot_ID;
        Int64 MixSplit_IntRes;
        int Count_Mix = 0;
        int New_IntRes = 0;
        #endregion

        #region Constructor
        public FrmMFGMixSplitLottingSarin()
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
                        lstError.Add(new ListError(5, "Atleast 1 Record must be enter in Mix grid"));
                        if (!blnFocus)
                        {
                            blnFocus = true;

                        }
                    }
                    DateTime endDate = Convert.ToDateTime(DateTime.Today);
                    endDate = endDate.AddDays(3);

                    if (Convert.ToDateTime(dtpReceiveDate.Text) >= endDate)
                    {
                        lstError.Add(new ListError(5, " Recieve Date Not Be Permission After 3 Days Receive this Lot ID...Please Contact to Administrator"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            dtpReceiveDate.Focus();
                        }
                    }
                    //var result = DateTime.Compare(Convert.ToDateTime(dtpReceiveDate.Text), DateTime.Today);
                    //if (result > 0)
                    //{
                    //    lstError.Add(new ListError(5, " Recieve Date Not Be Greater Than Today Date"));
                    //    if (!blnFocus)
                    //    {
                    //        blnFocus = true;
                    //        dtpReceiveDate.Focus();
                    //    }
                    //}
                    if (Val.ToString(dtpReceiveDate.Text) == string.Empty)
                    {
                        lstError.Add(new ListError(22, "Date"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            dtpReceiveDate.Focus();
                        }
                    }

                    if (DTab_StockData.Rows.Count == 0)
                    {
                        lstError.Add(new ListError(5, "Atleast 1 Record must be enter in Split grid"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                        }
                    }

                    decimal Mix_Carat = Val.ToDecimal(clmSplitCarat.SummaryItem.SummaryValue) + Val.ToDecimal(clmLossCarat.SummaryItem.SummaryValue) + Val.ToDecimal(ClmRejectionCarat.SummaryItem.SummaryValue) + Val.ToDecimal(clmSplitRRCarat.SummaryItem.SummaryValue) - Val.ToDecimal(clmCaratPlus.SummaryItem.SummaryValue);

                    if (Val.ToDecimal(clmCarat.SummaryItem.SummaryValue) + Val.ToDecimal(clmRejCarat.SummaryItem.SummaryValue) + Val.ToDecimal(clmRRCarat.SummaryItem.SummaryValue) != Mix_Carat && chkIsLotingDepartment.Checked == false)
                    {
                        lstError.Add(new ListError(5, "Carat not match Please Check.."));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                        }
                    }
                    if (lueProcess.Text == "")
                    {
                        lstError.Add(new ListError(13, "Process"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueProcess.Focus();
                        }
                    }
                    if (lueSubProcess.Text == "")
                    {
                        lstError.Add(new ListError(13, "Sub Process"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueSubProcess.Focus();
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
                        lstError.Add(new ListError(13, "Purity Name"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueQuality.Focus();
                        }
                    }
                    if (lueClarity.Text == "")
                    {
                        lstError.Add(new ListError(13, "Shade Name"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueClarity.Focus();
                        }
                    }
                    if (lueRoughSieve.Text == "")
                    {
                        lstError.Add(new ListError(13, "Charani Name"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueRoughSieve.Focus();
                        }
                    }

                    if (GlobalDec.gEmployeeProperty.department_name != "ROUGH" && GlobalDec.gEmployeeProperty.role_name != "SURAT ASSORT")
                    {
                        if (Val.ToInt(txtPcs.Text) == 0 && Val.ToInt(txtRejPcs.Text) == 0)
                        {
                            lstError.Add(new ListError(12, "Pcs"));
                            if (!blnFocus)
                            {
                                blnFocus = true;
                                txtPcs.Focus();
                            }
                        }
                    }

                    if (Val.ToDecimal(txtCarat.Text) == 0 && Val.ToDecimal(txtRejCarat.Text) == 0)
                    {
                        lstError.Add(new ListError(12, "Carat"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtCarat.Focus();
                        }
                    }

                    if (GlobalDec.gEmployeeProperty.role_name != "SURAT ASSORT")
                    {
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
                var Date = DateTime.Compare(Convert.ToDateTime(dtpReceiveDate.Text), DateTime.Today);
                if (Date < 0)
                {
                    Str = GlobalDec.CheckLockIsOpenOrNot(Val.DBDate(dtpReceiveDate.Text), Val.DBTime(DateTime.Now.ToShortTimeString())); //Val.GetFullTime12());
                    if (Str != "YES")
                    {
                        if (Str != "")
                        {
                            Global.Message(Str);
                            //btnSave.Enabled = true;
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

                DialogResult result = MessageBox.Show("Do you want to save Mix data?", "Confirmation", MessageBoxButtons.YesNoCancel);
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
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            Global.Export("xlsx", dgvDet);
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (AddInGrid())
                {
                    //lueQuality.EditValue = System.DBNull.Value;
                    //lueClarity.EditValue = System.DBNull.Value;
                    //lueRoughSieve.EditValue = System.DBNull.Value;
                    txtPcs.Text = string.Empty;
                    txtCarat.Text = string.Empty;
                    txtRejPcs.Text = string.Empty;
                    txtRejCarat.Text = string.Empty;
                    txtAmount.Text = string.Empty;
                    txtWeightPlus.Text = string.Empty;
                    txtWeightLoss.Text = string.Empty;
                    txtFinalLotID.Text = "";
                    txtRRPcs.Text = string.Empty;
                    txtRRCarat.Text = string.Empty;
                    //txtBalanceCarat.Text = "0";
                    //txtRate.Text = "0";
                    txtPcs.Focus();
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                //blnReturn = false;
            }
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
                if (txtFinalLotID.Text.ToString() != "" && txtFinalLotID.Text.ToString() != "0")
                {
                    DataTable dtStock = objLiveStock.GetData(Val.ToInt32(txtFinalLotID.Text));
                    if (dtStock.Rows.Count > 0)
                    {
                        if (Val.ToInt32(dtStock.Rows[0]["status_id"]) == 1)
                        {
                            blnReturn = false;
                            return blnReturn;
                        }
                    }
                    else
                    {
                        Global.Message("Lot No Not in Your Department.");
                        blnReturn = false;
                        return blnReturn;
                    }
                }
                if (Val.ToString(GlobalDec.gEmployeeProperty.department_name) == "MAKABLE" || Val.ToString(GlobalDec.gEmployeeProperty.department_name) == "SAWABLE")
                {
                    if (Val.ToString(lueDepartment.Text) == "")
                    {
                        Global.Message("Select Department.");
                        blnReturn = false;
                        return blnReturn;
                    }
                }

                if (Val.ToString(GlobalDec.gEmployeeProperty.role_name) == "SURAT MAKABLE" || Val.ToString(GlobalDec.gEmployeeProperty.role_name) == "SAWABLE")
                {
                    if (Val.ToInt(txtFinalLotID.Text) > 0 && Val.ToString(lblMode.Text) == "ADD")
                    {
                        Global.Message("PLz Check Final Lot Id.");
                        blnReturn = false;
                        return blnReturn;
                    }
                }

                if (btnAdd.Text == "&Add")
                {

                    if (Val.ToBoolean(chkAutoSplit.Checked) == true && Val.ToDecimal(txtRRCarat.Text) > 0)
                    {
                        for (int i = 0; i <= 1; i++)
                        {
                            DataRow drwNew = m_dtbLotMixSplit.NewRow();
                            int numPcs = i == 0 ? Val.ToInt(txtPcs.Text) : Val.ToInt(0);
                            decimal numCarat = i == 0 ? Val.ToDecimal(txtCarat.Text) : Val.ToInt(0);
                            int numRejectionPcs = i == 0 ? Val.ToInt(txtRejPcs.Text) : Val.ToInt(0);
                            decimal numRejectionCarat = Val.ToDecimal(txtRejCarat.Text);
                            decimal numLossCarat = Val.ToDecimal(0);
                            decimal numPlusCarat = Val.ToDecimal(0);
                            decimal numRate = Val.ToDecimal(txtRate.Text);
                            decimal numAmount = Val.ToDecimal(numRate) * (i == 0 ? Val.ToDecimal(txtCarat.Text) : Val.ToDecimal(txtRRCarat.Text));
                            int numRRPcs = i == 0 ? Val.ToInt(0) : Val.ToInt(txtRRPcs.Text);
                            decimal numRRCarat = i == 0 ? Val.ToDecimal(0) : Val.ToDecimal(txtRRCarat.Text);

                            drwNew["rough_cut_no"] = Val.ToString(lueCutNo.Text);
                            drwNew["rough_cut_id"] = Val.ToInt(lueCutNo.EditValue);
                            drwNew["lot_id"] = i == 0 ? Val.ToInt(txtFinalLotID.Text) : Val.ToInt(0);

                            drwNew["kapan_id"] = Val.ToInt(lueKapan.EditValue);
                            drwNew["kapan_no"] = Val.ToString(lueKapan.Text);

                            drwNew["rough_sieve_id"] = Val.ToInt(lueRoughSieve.EditValue);
                            drwNew["sieve_name"] = Val.ToString(lueRoughSieve.Text);

                            drwNew["rough_clarity_id"] = Val.ToInt(lueClarity.EditValue);
                            drwNew["rough_clarity_name"] = Val.ToString(lueClarity.Text);
                            drwNew["quality_id"] = Val.ToInt(lueQuality.EditValue);
                            drwNew["quality_name"] = Val.ToString(lueQuality.Text);

                            drwNew["department_id"] = Val.ToInt(lueDepartment.EditValue);
                            drwNew["lotting_department"] = Val.ToString(lueDepartment.Text);

                            drwNew["balance_pcs"] = numPcs;
                            drwNew["balance_carat"] = numCarat;
                            drwNew["rejection_pcs"] = numRejectionPcs;
                            drwNew["rejection_carat"] = numRejectionCarat;
                            drwNew["rr_pcs"] = numRRPcs;
                            drwNew["rr_carat"] = numRRCarat;
                            drwNew["loss_carat"] = numLossCarat;
                            drwNew["plus_carat"] = numPlusCarat;
                            drwNew["rate"] = Math.Round(numRate, 2);
                            drwNew["amount"] = Math.Round(numAmount, 0);
                            drwNew["sr_no"] = m_Srno;
                            drwNew["final_lot_id"] = i == 0 ? Val.ToInt(txtFinalLotID.Text) : Val.ToInt(0);
                            drwNew["edit_flag"] = Val.ToInt(0);
                            m_dtbLotMixSplit.Rows.Add(drwNew);
                            m_Srno++;
                        }
                        grdMixSplit.DataSource = m_dtbLotMixSplit;
                    }
                    else
                    {
                        if (m_dtbLotMixSplit.Select("final_lot_id ='" + Val.ToInt(txtFinalLotID.Text) + "' AND final_lot_id <> 0").Length > 0)
                        {
                            Global.Message("Lot No Already Exist.");
                            blnReturn = false;
                            return blnReturn;
                        }
                        DataRow drwNew = m_dtbLotMixSplit.NewRow();
                        int numPcs = Val.ToInt(txtPcs.Text);
                        decimal numCarat = Val.ToDecimal(txtCarat.Text);
                        int numRejectionPcs = Val.ToInt(txtRejPcs.Text);
                        decimal numRejectionCarat = Val.ToDecimal(txtRejCarat.Text);
                        decimal numLossCarat = Val.ToDecimal(txtWeightLoss.Text);
                        decimal numPlusCarat = Val.ToDecimal(txtWeightPlus.Text);
                        decimal numRate = Val.ToDecimal(txtRate.Text);
                        decimal numAmount = Val.ToDecimal(txtAmount.Text);
                        int numRRPcs = Val.ToInt(txtRRPcs.Text);
                        decimal numRRCarat = Val.ToDecimal(txtRRCarat.Text);

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
                        drwNew["department_id"] = Val.ToInt(lueDepartment.EditValue);
                        drwNew["lotting_department"] = Val.ToString(lueDepartment.Text);
                        drwNew["balance_pcs"] = numPcs;
                        drwNew["balance_carat"] = numCarat;
                        drwNew["rejection_pcs"] = numRejectionPcs;
                        drwNew["rejection_carat"] = numRejectionCarat;
                        drwNew["rr_pcs"] = numRRPcs;
                        drwNew["rr_carat"] = numRRCarat;
                        drwNew["loss_carat"] = numLossCarat;
                        drwNew["plus_carat"] = numPlusCarat;
                        drwNew["rate"] = numRate;
                        drwNew["amount"] = numAmount;
                        drwNew["sr_no"] = m_Srno;
                        drwNew["final_lot_id"] = Val.ToInt(txtFinalLotID.Text);
                        drwNew["edit_flag"] = Val.ToInt(0);
                        m_dtbLotMixSplit.Rows.Add(drwNew);
                        m_Srno++;
                    }
                }
                else if (btnAdd.Text == "&Update")
                {
                    if (m_dtbLotMixSplit.Select("sr_no ='" + Val.ToInt(m_update_srno) + "'").Length > 0)
                    {
                        for (int i = 0; i < m_dtbLotMixSplit.Rows.Count; i++)
                        {
                            if (m_dtbLotMixSplit.Select("rough_cut_no ='" + m_cut_no + "' AND sr_no = '" + m_update_srno + "'").Length > 0)
                            {
                                if (m_dtbLotMixSplit.Rows[dgvMixSplit.FocusedRowHandle]["rough_cut_no"].ToString() == m_cut_no.ToString())
                                {
                                    if (m_dtbLotMixSplit.Select("final_lot_id ='" + Val.ToInt(txtFinalLotID.Text) + "' AND final_lot_id <> 0 AND sr_no <> '" + m_update_srno + "'").Length > 0)
                                    {
                                        Global.Message("Lot No Already Exist.");
                                        blnReturn = false;
                                        return blnReturn;
                                    }
                                    m_dtbLotMixSplit.Rows[dgvMixSplit.FocusedRowHandle]["final_lot_id"] = Val.ToInt(txtFinalLotID.Text);
                                    m_dtbLotMixSplit.Rows[dgvMixSplit.FocusedRowHandle]["sieve_name"] = Val.ToString(lueRoughSieve.Text);
                                    m_dtbLotMixSplit.Rows[dgvMixSplit.FocusedRowHandle]["rough_sieve_id"] = Val.ToInt(lueRoughSieve.EditValue);
                                    m_dtbLotMixSplit.Rows[dgvMixSplit.FocusedRowHandle]["sieve_name"] = Val.ToString(lueRoughSieve.Text);
                                    m_dtbLotMixSplit.Rows[dgvMixSplit.FocusedRowHandle]["quality_name"] = Val.ToString(lueQuality.Text);
                                    m_dtbLotMixSplit.Rows[dgvMixSplit.FocusedRowHandle]["quality_id"] = Val.ToInt(lueQuality.EditValue);
                                    m_dtbLotMixSplit.Rows[dgvMixSplit.FocusedRowHandle]["rough_clarity_name"] = Val.ToString(lueClarity.Text);
                                    m_dtbLotMixSplit.Rows[dgvMixSplit.FocusedRowHandle]["rough_clarity_id"] = Val.ToInt(lueClarity.EditValue);

                                    m_dtbLotMixSplit.Rows[dgvMixSplit.FocusedRowHandle]["balance_pcs"] = Val.ToInt(txtPcs.Text).ToString();
                                    m_dtbLotMixSplit.Rows[dgvMixSplit.FocusedRowHandle]["balance_carat"] = Val.ToDecimal(txtCarat.Text).ToString();
                                    m_dtbLotMixSplit.Rows[dgvMixSplit.FocusedRowHandle]["rejection_pcs"] = Val.ToInt(txtRejPcs.Text).ToString();
                                    m_dtbLotMixSplit.Rows[dgvMixSplit.FocusedRowHandle]["rejection_carat"] = Val.ToDecimal(txtRejCarat.Text).ToString();
                                    m_dtbLotMixSplit.Rows[dgvMixSplit.FocusedRowHandle]["rr_pcs"] = Val.ToInt(txtRRPcs.Text).ToString();
                                    m_dtbLotMixSplit.Rows[dgvMixSplit.FocusedRowHandle]["rr_carat"] = Val.ToDecimal(txtRRCarat.Text).ToString();
                                    m_dtbLotMixSplit.Rows[dgvMixSplit.FocusedRowHandle]["loss_carat"] = Val.ToDecimal(txtWeightLoss.Text).ToString();
                                    m_dtbLotMixSplit.Rows[dgvMixSplit.FocusedRowHandle]["plus_carat"] = Val.ToDecimal(txtWeightPlus.Text);
                                    m_dtbLotMixSplit.Rows[dgvMixSplit.FocusedRowHandle]["rate"] = Val.ToDecimal(txtRate.Text);
                                    m_dtbLotMixSplit.Rows[dgvMixSplit.FocusedRowHandle]["amount"] = Val.ToDecimal(txtAmount.Text);
                                    m_dtbLotMixSplit.Rows[dgvMixSplit.FocusedRowHandle]["department_id"] = Val.ToInt(lueDepartment.EditValue);
                                    m_dtbLotMixSplit.Rows[dgvMixSplit.FocusedRowHandle]["edit_flag"] = Val.ToInt(1);

                                    //m_flag = 0;
                                    break;
                                }
                            }
                        }
                    }
                    btnAdd.Text = "&Add";
                }
                dgvMixSplit.MoveLast();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
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

        DataTable DTab_StockData = new DataTable();
        DataTable DTabTemp = new DataTable();
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
                        //        Global.Message(DTab_StockData.Rows[i]["lot_id"].ToString() + " = Lot ID already added to the Issue list!");
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
                MFGProcessIssue objMFGProcessIssue = new MFGProcessIssue();
                MFGProcessIssueProperty objMFGProcessIssueProperty = new MFGProcessIssueProperty();
                objMFGProcessIssueProperty.rough_cut_id = Val.ToInt(lueMixCutNo.EditValue);
                objMFGProcessIssueProperty.kapan_id = Val.ToInt(lueMixKapan.EditValue);

                if (DTab_StockData.Rows.Count > 0)
                {
                    //DataTable DTab_ValidateBarcode = objMFGMixSplit.Live_Stock_GetData(Val.Trim(txtLotID.Text));


                    DataTable DTab_ValidateBarcode = objMFGProcessIssue.GetPendingDeptStock(Val.ToInt64(txtLotID.Text), objMFGProcessIssueProperty);

                    if (DTab_ValidateBarcode.Rows.Count > 0)
                    {
                        for (int i = 0; i < DTab_ValidateBarcode.Rows.Count; i++)
                        {
                            if (Process_Id != Val.ToInt(DTab_ValidateBarcode.Rows[i]["process_id"]))
                            {
                                Global.Message("Difference Process Name in this lot ID =" + Val.ToInt(DTab_ValidateBarcode.Rows[i]["lot_id"]));
                                txtLotID.Text = "";
                                txtLotID.Focus();
                                return;
                            }
                            //else
                            //{
                            //    int Process_Id_New = Val.ToInt(DTab_ValidateBarcode.Rows[i]["process_id"]);
                            //    Sub_Process_Id = Val.ToInt(DTab_ValidateBarcode.Rows[i]["sub_process_id"]);
                            //    dtIss = objProcessRecieve.GetIssueData(Val.ToString(DTab_ValidateBarcode.Rows[i]["lot_id"]), Val.ToInt32(Process_Id_New), Val.ToInt32(Sub_Process_Id));
                            //    if (dtIss.Rows.Count > 0)
                            //    {
                            //        //Global.Message("Lot is already issue in this process.");
                            //    }
                            //    else
                            //    {
                            //        Global.Message("Process Not Issue in this Lot ID = " + Val.ToString(DTab_ValidateBarcode.Rows[i]["lot_id"]));
                            //        txtLotID.Text = "";
                            //        txtLotID.Focus();
                            //        return;
                            //    }
                            //}
                        }
                    }
                    else
                    {
                        Global.Message("Lot ID Not found");
                        txtLotID.Text = "";
                        txtLotID.Focus();
                        return;
                    }

                    // DTabTemp = objMFGMixSplit.Live_Stock_GetData(Val.Trim(txtLotID.Text));
                    //DTabTemp = objMFGProcessIssue.GetPendingDeptStock(Val.ToInt64(txtLotID.Text), objMFGProcessIssueProperty);

                    if (DTab_ValidateBarcode.Rows.Count > 0)
                    {
                        txtLotID.Text = "";
                        txtLotID.Focus();
                    }

                    DTab_StockData.Merge(DTab_ValidateBarcode);
                }
                else
                {
                    //DataTable DTab_ValidateBarcode = objMFGMixSplit.Live_Stock_GetData(Val.Trim(txtLotID.Text));
                    //DataTable DTab_ValidateBarcode = objMFGProcessIssue.GetPendingDeptStock(Val.ToInt64(txtLotID.Text), objMFGProcessIssueProperty);
                    DTab_StockData = objMFGProcessIssue.GetPendingDeptStock(Val.ToInt64(txtLotID.Text), objMFGProcessIssueProperty);

                    if (DTab_StockData.Rows.Count > 0)
                    {
                        //for (int i = 0; i < DTab_ValidateBarcode.Rows.Count; i++)
                        //{
                        Process_Id = Val.ToInt(DTab_StockData.Rows[0]["process_id"]);
                        //    Sub_Process_Id = Val.ToInt(DTab_ValidateBarcode.Rows[i]["sub_process_id"]);

                        //    dtIss = objProcessRecieve.GetIssueData(Val.ToString(DTab_ValidateBarcode.Rows[i]["lot_id"]), Val.ToInt32(Process_Id), Val.ToInt32(Sub_Process_Id));
                        //    if (dtIss.Rows.Count > 0)
                        //    {
                        //        //Global.Message("Lot is already issue in this process.");
                        //    }
                        //    else
                        //    {
                        //        Global.Message("Process Not Issue in this Lot ID = " + Val.ToString(DTab_ValidateBarcode.Rows[i]["lot_id"]));
                        //        txtLotID.Text = "";
                        //        txtLotID.Focus();
                        //        return;
                        //    }
                        //}
                    }
                    else
                    {
                        Global.Message("Lot ID Not found");
                        txtLotID.Text = "";
                        txtLotID.Focus();
                        return;
                    }

                    //DTab_StockData = objMFGMixSplit.Live_Stock_GetData(Val.Trim(txtLotID.Text));
                    //DTab_StockData = objMFGProcessIssue.GetPendingDeptStock(Val.ToInt64(txtLotID.Text), objMFGProcessIssueProperty);


                    if (DTab_StockData.Rows.Count > 0)
                    {
                        lueKapan.EditValue = Val.ToInt64(DTab_StockData.Rows[0]["kapan_id"]);
                        lueCutNo.EditValue = Val.ToInt64(DTab_StockData.Rows[0]["rough_cut_id"]);



                        txtLotID.Text = "";
                        txtLotID.Focus();
                    }
                }
                if (GlobalDec.gEmployeeProperty.role_name == "SURAT MAKABLE" || GlobalDec.gEmployeeProperty.role_name == "SURAT SARIN" || GlobalDec.gEmployeeProperty.role_name == "SURAT KATARGAM")
                {
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
                }
                grdDet.DataSource = DTab_StockData;
                grdDet.RefreshDataSource();
                dgvDet.BestFitColumns();
                CalculateSummary();
                if (Val.ToBoolean(chkAutoSplit.Checked) == true && Val.ToDecimal(DTab_StockData.Rows[0]["rr_carat"]) > 0)
                {
                    txtFinalLotID.Text = Val.ToString(DTab_StockData.Rows[0]["lot_id"]);
                    txtRRPcs.Text = Val.ToString(DTab_StockData.Rows[0]["rr_pcs"]);
                    txtRRCarat.Text = Val.ToString(DTab_StockData.Rows[0]["rr_carat"]);

                    lueRoughSieve.EditValue = Val.ToInt(DTab_StockData.Rows[0]["rough_sieve_id"]);
                    //drwNew["sieve_name"] = Val.ToString(DTab_StockData.Rows[0]["sieve_name"]);

                    lueClarity.EditValue = Val.ToInt(DTab_StockData.Rows[0]["rough_clarity_id"]);
                    //lueClarity.Text = Val.ToString(DTab_StockData.Rows[0]["rough_clarity_name"]);

                    lueQuality.EditValue = Val.ToInt64(DTab_StockData.Rows[0]["quality_id"]);
                    //lueQuality.Text = Val.ToString(DTab_StockData.Rows[0]["quality_name"]);
                }

            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void backgroundWorker_LotSplit_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {

                MFGLotSplit objMFGLotSplit = new MFGLotSplit();
                MFGMixSplitProperty objMFGMixSplitProperty = new MFGMixSplitProperty();
                Conn = new BeginTranConnection(true, false);
                dtBarcodePrint = new DataTable();
                dtBarcodePrint.AcceptChanges();
                dtBarcodePrint.Columns.Add("kapan_no", typeof(string));
                dtBarcodePrint.Columns.Add("sr_no", typeof(int));
                dtBarcodePrint.Columns.Add("lot_id", typeof(int));
                dtBarcodePrint.Columns.Add("pcs", typeof(int));
                dtBarcodePrint.Columns.Add("carat", typeof(decimal));
                dtBarcodePrint.Columns.Add("receive_date", typeof(string));
                DataRow drwNew;
                try
                {
                    IntRes = 0;
                    Count_Mix = 0;
                    New_Lot_ID = 0;
                    MixSplit_IntRes = 0;
                    IntRes_MixSplit = 0;
                    Receive_IntRes = 0;
                    Issue_IntRes = 0;
                    Int64 NewHistory_Union_Id = 0;
                    Int64 Lot_SrNo = 0;

                    int SrNo = 0;
                    //int Pcs = Val.ToInt(clmPcs.SummaryItem.SummaryValue);
                    //objMFGMixSplitProperty.pcs = Pcs;
                    //objMFGMixSplitProperty.carat = Val.ToDecimal(clmCarat.SummaryItem.SummaryValue);
                    //m_numSummRate = Math.Round((Val.ToDecimal(clmAmount.SummaryItem.SummaryValue) / Val.ToDecimal(clmCarat.SummaryItem.SummaryValue)), 2, MidpointRounding.AwayFromZero);
                    //objMFGMixSplitProperty.rate = Val.ToDecimal(m_numSummRate);
                    //objMFGMixSplitProperty.amount = Val.ToDecimal(clmAmount.SummaryItem.SummaryValue);

                    //objMFGMixSplitProperty = objMFGMixSplit.Save_MFGStockMixData(objMFGMixSplitProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                    //New_Lot_ID = objMFGMixSplitProperty.new_lot_id;

                    decimal Plus_carat = Val.ToDecimal(clmCaratPlus.SummaryItem.SummaryValue);
                    decimal Loss_Carat = Val.ToDecimal(clmLossCarat.SummaryItem.SummaryValue);
                    decimal Rej_Carat = Val.ToDecimal(ClmRejectionCarat.SummaryItem.SummaryValue);
                    decimal RR_Carat = Val.ToDecimal(clmRRCarat.SummaryItem.SummaryValue);
                    decimal org_Carat = Val.ToDecimal(clmOrgCarat.SummaryItem.SummaryValue);
                    decimal k_Carat = Val.ToDecimal(clmKCarat.SummaryItem.SummaryValue);
                    decimal bal_Carat = Val.ToDecimal(clmCarat.SummaryItem.SummaryValue);
                    decimal totalper = Math.Round(Val.ToDecimal(((bal_Carat + Plus_carat - Loss_Carat) / org_Carat) * 100), 3);
                    decimal polishper = 0;
                    if (k_Carat > 0)
                    {
                        polishper = Math.Round(Val.ToDecimal(((bal_Carat + Plus_carat - Loss_Carat) / k_Carat) * 100), 3);
                    }
                    else
                    {
                        polishper = 0;
                    }


                    int IntMixEntry = 0;

                    DataTable Mix_Data = (DataTable)grdDet.DataSource;
                    if ((Mix_Data.Rows.Count == 1 && m_dtbLotMixSplit.Rows.Count == 1) || (Mix_Data.Rows.Count > 1 && m_dtbLotMixSplit.Rows.Count == 1) || (Mix_Data.Rows.Count == 1 && m_dtbLotMixSplit.Rows.Count > 1))
                    {
                        IntMixEntry = 1;
                    }
                    else
                    {
                        IntMixEntry = 0;
                    }

                    #region Many To Many
                    if (IntMixEntry == 0 && lblMode.Text == "NEW")
                    {

                        foreach (DataRow drw in Mix_Data.Rows)
                        {
                            if (Count_Mix == 0)
                            {
                                objMFGMixSplitProperty.pcs = Val.ToInt(clmPcs.SummaryItem.SummaryValue);
                                objMFGMixSplitProperty.carat = Val.ToDecimal(clmCarat.SummaryItem.SummaryValue);
                                objMFGMixSplitProperty.rr_pcs = Val.ToInt(clmRRPcs.SummaryItem.SummaryValue);
                                objMFGMixSplitProperty.rr_carat = Val.ToDecimal(clmRRCarat.SummaryItem.SummaryValue);
                                objMFGMixSplitProperty.rejection_pcs = Val.ToInt(clmRejPcs.SummaryItem.SummaryValue);
                                objMFGMixSplitProperty.rejection_carat = Val.ToDecimal(clmRejCarat.SummaryItem.SummaryValue);
                                m_numSummRate = Math.Round((Val.ToDecimal(clmAmount.SummaryItem.SummaryValue) / Val.ToDecimal(clmCarat.SummaryItem.SummaryValue)), 2, MidpointRounding.AwayFromZero);
                                objMFGMixSplitProperty.rate = Val.ToDecimal(m_numSummRate);
                                objMFGMixSplitProperty.amount = Val.ToDecimal(clmAmount.SummaryItem.SummaryValue);

                                objMFGMixSplitProperty.from_lot_id = Val.ToInt64(drw["lot_id"]);
                                objMFGMixSplitProperty.prediction_id = Val.ToInt64(drw["prediction_id"]);
                                objMFGMixSplitProperty.new_lot_id = Val.ToInt(New_Lot_ID);
                                objMFGMixSplitProperty.receive_date = Val.DBDate(dtpReceiveDate.Text);
                                objMFGMixSplitProperty.from_pcs = Val.ToInt(drw["pcs"]);
                                objMFGMixSplitProperty.from_carat = Val.ToDecimal(drw["carat"]);
                                objMFGMixSplitProperty.from_rr_pcs = Val.ToInt(drw["rr_pcs"]);
                                objMFGMixSplitProperty.from_rr_carat = Val.ToDecimal(drw["rr_carat"]);
                                objMFGMixSplitProperty.from_rejection_pcs = Val.ToInt(drw["rejection_pcs"]);
                                objMFGMixSplitProperty.from_rejection_carat = Val.ToDecimal(drw["rejection_carat"]);
                                objMFGMixSplitProperty.quality_id = Val.ToInt64(drw["quality_id"]);
                                objMFGMixSplitProperty.count = Count_Mix;
                                objMFGMixSplitProperty.mix_union_id = MixSplit_IntRes;
                                objMFGMixSplitProperty.form_id = Val.ToInt(m_numForm_id);
                                objMFGMixSplitProperty.rough_cut_id = Val.ToInt64(lueCutNo.EditValue);
                                objMFGMixSplitProperty.kapan_id = Val.ToInt64(lueKapan.EditValue);
                                objMFGMixSplitProperty.loss_carat = Val.ToDecimal(Loss_Carat);
                                objMFGMixSplitProperty.plus_carat = Val.ToDecimal(Plus_carat);

                                objMFGMixSplitProperty.manager_id = Val.ToInt(drw["manager_id"]);
                                objMFGMixSplitProperty.employee_id = Val.ToInt(drw["employee_id"]);
                                objMFGMixSplitProperty.process_id = Val.ToInt(lueProcess.EditValue);
                                objMFGMixSplitProperty.sub_process_id = Val.ToInt(lueSubProcess.EditValue);
                                objMFGMixSplitProperty.rough_clarity_id = Val.ToInt(drw["rough_clarity_id"]);
                                objMFGMixSplitProperty.rough_sieve_id = Val.ToInt(drw["rough_sieve_id"]);
                                objMFGMixSplitProperty.purity_id = Val.ToInt(drw["purity_id"]);
                                objMFGMixSplitProperty.union_id = IntRes;
                                objMFGMixSplitProperty.receive_union_id = Receive_IntRes;
                                objMFGMixSplitProperty.issue_union_id = Issue_IntRes;
                                objMFGMixSplitProperty.history_union_id = NewHistory_Union_Id;
                                objMFGMixSplitProperty.lot_srno = Lot_SrNo;
                                objMFGMixSplitProperty.is_repeat = chkIsRepeat.Checked == true ? 1 : 0;
                                objMFGMixSplitProperty.transaction_type = "MANY-TO-MANY";
                                //objMFGMixSplitProperty = objMFGMixSplit.Save_MFGStockLotting_MixData_Issue(objMFGMixSplitProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                                objMFGMixSplitProperty = objMFGMixSplit.Save_MFGStockLotting_MixData(objMFGMixSplitProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                                Issue_IntRes = objMFGMixSplitProperty.issue_union_id;
                                NewHistory_Union_Id = Val.ToInt64(objMFGMixSplitProperty.history_union_id);
                                New_Lot_ID = objMFGMixSplitProperty.new_lot_id;
                                MixSplit_IntRes = objMFGMixSplitProperty.mix_union_id;
                                IntRes = objMFGMixSplitProperty.union_id;
                                Receive_IntRes = objMFGMixSplitProperty.receive_union_id;
                                Lot_SrNo = objMFGMixSplitProperty.lot_srno;

                                IntRes_MixSplit = objMFGMixSplit.Update_Balance_Carat(Val.ToString(drw["lot_id"]), DLL.GlobalDec.EnumTran.Continue, Conn);
                                Count_Mix = Count_Mix + 1;
                            }
                            else
                            {
                                objMFGMixSplitProperty.pcs = Val.ToInt(clmPcs.SummaryItem.SummaryValue);
                                objMFGMixSplitProperty.carat = Val.ToDecimal(clmCarat.SummaryItem.SummaryValue);
                                m_numSummRate = Math.Round((Val.ToDecimal(clmAmount.SummaryItem.SummaryValue) / Val.ToDecimal(clmCarat.SummaryItem.SummaryValue)), 2, MidpointRounding.AwayFromZero);
                                objMFGMixSplitProperty.rate = Val.ToDecimal(m_numSummRate);
                                objMFGMixSplitProperty.amount = Val.ToDecimal(clmAmount.SummaryItem.SummaryValue);

                                objMFGMixSplitProperty.from_lot_id = Val.ToInt64(drw["lot_id"]);
                                objMFGMixSplitProperty.new_lot_id = Val.ToInt(New_Lot_ID);
                                objMFGMixSplitProperty.receive_date = Val.DBDate(dtpReceiveDate.Text);
                                objMFGMixSplitProperty.from_pcs = Val.ToInt(drw["pcs"]);
                                objMFGMixSplitProperty.from_carat = Val.ToDecimal(drw["carat"]);
                                objMFGMixSplitProperty.from_rr_pcs = Val.ToInt(drw["rr_pcs"]);
                                objMFGMixSplitProperty.from_rr_carat = Val.ToDecimal(drw["rr_carat"]);
                                objMFGMixSplitProperty.from_rejection_pcs = Val.ToInt(drw["rejection_pcs"]);
                                objMFGMixSplitProperty.from_rejection_carat = Val.ToDecimal(drw["rejection_carat"]);
                                objMFGMixSplitProperty.quality_id = Val.ToInt64(drw["quality_id"]);
                                objMFGMixSplitProperty.count = Count_Mix;
                                objMFGMixSplitProperty.mix_union_id = MixSplit_IntRes;
                                objMFGMixSplitProperty.form_id = Val.ToInt(m_numForm_id);
                                objMFGMixSplitProperty.rough_cut_id = Val.ToInt64(lueCutNo.EditValue);
                                objMFGMixSplitProperty.kapan_id = Val.ToInt64(lueKapan.EditValue);
                                objMFGMixSplitProperty.loss_carat = Val.ToDecimal(0);
                                objMFGMixSplitProperty.plus_carat = Val.ToDecimal(0);

                                objMFGMixSplitProperty.manager_id = Val.ToInt(drw["manager_id"]);
                                objMFGMixSplitProperty.employee_id = Val.ToInt(drw["employee_id"]);
                                objMFGMixSplitProperty.process_id = Val.ToInt(lueProcess.EditValue);
                                objMFGMixSplitProperty.sub_process_id = Val.ToInt(lueSubProcess.EditValue);
                                objMFGMixSplitProperty.rough_clarity_id = Val.ToInt(drw["rough_clarity_id"]);
                                objMFGMixSplitProperty.rough_sieve_id = Val.ToInt(drw["rough_sieve_id"]);
                                objMFGMixSplitProperty.purity_id = Val.ToInt(drw["purity_id"]);

                                objMFGMixSplitProperty.union_id = IntRes;
                                objMFGMixSplitProperty.receive_union_id = Receive_IntRes;
                                objMFGMixSplitProperty.issue_union_id = Issue_IntRes;
                                objMFGMixSplitProperty.history_union_id = NewHistory_Union_Id;
                                objMFGMixSplitProperty.lot_srno = Lot_SrNo;
                                objMFGMixSplitProperty.is_repeat = chkIsRepeat.Checked == true ? 1 : 0;
                                objMFGMixSplitProperty.transaction_type = "MANY-TO-MANY";
                                objMFGMixSplitProperty = objMFGMixSplit.Save_MFGStockLotting_MixData(objMFGMixSplitProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                                New_Lot_ID = objMFGMixSplitProperty.new_lot_id;
                                MixSplit_IntRes = objMFGMixSplitProperty.mix_union_id;
                                IntRes = objMFGMixSplitProperty.union_id;
                                Receive_IntRes = objMFGMixSplitProperty.receive_union_id;
                                Issue_IntRes = objMFGMixSplitProperty.issue_union_id;
                                NewHistory_Union_Id = Val.ToInt64(objMFGMixSplitProperty.history_union_id);
                                Lot_SrNo = objMFGMixSplitProperty.lot_srno;
                                IntRes_MixSplit = objMFGMixSplit.Update_Balance_Carat(Val.ToString(drw["lot_id"]), DLL.GlobalDec.EnumTran.Continue, Conn);
                                Count_Mix = Count_Mix + 1;
                            }
                        }
                        int count = 0;
                        foreach (DataRow drw in m_dtbLotMixSplit.Rows)
                        {
                            int p_InMixGrid = 0;
                            if (Val.ToInt(drw["final_lot_id"]) != 0)
                            {
                                SrNo = objMFGMixSplit.GetLotNO(Val.ToInt(drw["final_lot_id"]), DLL.GlobalDec.EnumTran.Continue, Conn);
                                objMFGMixSplitProperty.sr_no = SrNo;
                                objMFGMixSplitProperty.flag = 1;
                                objMFGMixSplitProperty.to_lot_id = Val.ToInt(drw["final_lot_id"]);
                                if (Mix_Data.Select("lot_id ='" + Val.ToInt(drw["final_lot_id"]) + "'").Length > 0)
                                {
                                    p_InMixGrid = 1;
                                    //blnReturn = false;
                                    //return blnReturn;
                                }
                            }
                            else
                            {
                                SrNo = objMFGMixSplit.GetSrNo(Val.ToInt(drw["rough_cut_id"]), DLL.GlobalDec.EnumTran.Continue, Conn);
                                objMFGMixSplitProperty.sr_no = Val.ToInt(SrNo + 1);
                                objMFGMixSplitProperty.flag = 0;
                                objMFGMixSplitProperty.to_lot_id = Val.ToInt(0);
                            }
                            if (Val.ToString(drw["lotting_department"]) == "RUSSIAN")
                            {
                                if (totalper > 0)
                                {
                                    objMFGMixSplitProperty.k_carat = Math.Round(Val.ToDecimal((Val.ToDecimal(drw["balance_carat"]) * 100) / totalper), 3);
                                }
                                else
                                {
                                    objMFGMixSplitProperty.k_carat = 0;
                                }
                            }
                            else if (Val.ToString(drw["lotting_department"]) == "POLISH")
                            {
                                if (polishper > 0)
                                {
                                    objMFGMixSplitProperty.k_carat = Math.Round(Val.ToDecimal((Val.ToDecimal(drw["balance_carat"]) * 100) / polishper), 3);
                                }
                                else
                                {
                                    objMFGMixSplitProperty.k_carat = 0;
                                }
                            }
                            else if (Val.ToString(drw["lotting_department"]) == "SOWABALE POLISH" || Val.ToString(drw["lotting_department"]) == "SAWABLE")
                            {
                                if (polishper > 0)
                                {
                                    objMFGMixSplitProperty.k_carat = Math.Round(Val.ToDecimal((Val.ToDecimal(drw["balance_carat"]) * 100) / polishper), 3);
                                }
                                else
                                {
                                    objMFGMixSplitProperty.k_carat = 0;
                                }
                            }
                            else
                            {
                                objMFGMixSplitProperty.k_carat = Val.ToDecimal(drw["balance_carat"]);
                            }

                            objMFGMixSplitProperty.receive_date = Val.DBDate(dtpReceiveDate.Text);
                            //objMFGMixSplitProperty.rough_lot_id = Val.ToInt(drw["lot_id"]);
                            objMFGMixSplitProperty.from_lot_id = New_Lot_ID;

                            objMFGMixSplitProperty.in_mixgrid = p_InMixGrid;
                            objMFGMixSplitProperty.rough_cut_id = Val.ToInt(drw["rough_cut_id"]);
                            objMFGMixSplitProperty.rough_sieve_id = Val.ToInt64(drw["rough_sieve_id"]);
                            objMFGMixSplitProperty.quality_id = Val.ToInt64(drw["quality_id"]);
                            objMFGMixSplitProperty.kapan_id = Val.ToInt64(drw["kapan_id"]);
                            objMFGMixSplitProperty.rough_clarity_id = Val.ToInt64(drw["rough_clarity_id"]);
                            objMFGMixSplitProperty.form_id = Val.ToInt(m_numForm_id);

                            objMFGMixSplitProperty.manager_id = Val.ToInt(Mix_Data.Rows[0]["manager_id"]);
                            objMFGMixSplitProperty.employee_id = Val.ToInt(Mix_Data.Rows[0]["employee_id"]);
                            objMFGMixSplitProperty.process_id = Val.ToInt(lueProcess.EditValue);
                            objMFGMixSplitProperty.sub_process_id = Val.ToInt(lueSubProcess.EditValue);
                            //objMFGMixSplitProperty.purity_id = Val.ToInt(drw["purity_id"]);
                            if (count == 0)
                            {
                                objMFGMixSplitProperty.from_pcs = Val.ToInt(clmPcs.SummaryItem.SummaryValue);
                                objMFGMixSplitProperty.from_carat = Val.ToDecimal(clmCarat.SummaryItem.SummaryValue);
                            }
                            else
                            {
                                objMFGMixSplitProperty.from_pcs = Val.ToInt(0);
                                objMFGMixSplitProperty.from_carat = Val.ToDecimal(0);
                            }

                            objMFGMixSplitProperty.from_rr_pcs = Val.ToInt(clmRRPcs.SummaryItem.SummaryValue);
                            objMFGMixSplitProperty.from_rr_carat = Val.ToDecimal(clmRRCarat.SummaryItem.SummaryValue);
                            objMFGMixSplitProperty.from_rejection_pcs = Val.ToInt(clmRejPcs.SummaryItem.SummaryValue);
                            objMFGMixSplitProperty.from_rejection_carat = Val.ToDecimal(clmRejCarat.SummaryItem.SummaryValue);
                            objMFGMixSplitProperty.pcs = Val.ToInt(drw["balance_pcs"]);
                            objMFGMixSplitProperty.carat = Val.ToDecimal(drw["balance_carat"]);
                            objMFGMixSplitProperty.to_rr_pcs = Val.ToInt(drw["rr_pcs"]);
                            objMFGMixSplitProperty.to_rr_carat = Val.ToDecimal(drw["rr_carat"]);
                            objMFGMixSplitProperty.to_rejection_pcs = Val.ToInt(drw["rejection_pcs"]);
                            objMFGMixSplitProperty.to_rejection_carat = Val.ToDecimal(drw["rejection_carat"]);
                            objMFGMixSplitProperty.loss_carat = Val.ToDecimal(0);
                            objMFGMixSplitProperty.plus_carat = Val.ToDecimal(0);
                            objMFGMixSplitProperty.rate = Val.ToDecimal(drw["rate"]);
                            objMFGMixSplitProperty.amount = Val.ToDecimal(drw["amount"]);
                            objMFGMixSplitProperty.union_id = IntRes;
                            objMFGMixSplitProperty.mix_union_id = MixSplit_IntRes;
                            objMFGMixSplitProperty.receive_union_id = Receive_IntRes;
                            objMFGMixSplitProperty.issue_union_id = Issue_IntRes;
                            objMFGMixSplitProperty.history_union_id = NewHistory_Union_Id;
                            objMFGMixSplitProperty.lotting_department_id = Val.ToInt(drw["department_id"]);
                            objMFGMixSplitProperty.lot_srno = Lot_SrNo;
                            objMFGMixSplitProperty.transaction_type = "MANY-TO-MANY";
                            objMFGMixSplitProperty = objMFGMixSplit.Save_MFGMixSplit(objMFGMixSplitProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                            IntRes = objMFGMixSplitProperty.union_id;
                            Receive_IntRes = objMFGMixSplitProperty.receive_union_id;
                            Issue_IntRes = objMFGMixSplitProperty.issue_union_id;
                            NewHistory_Union_Id = Val.ToInt64(objMFGMixSplitProperty.history_union_id);
                            Lot_SrNo = objMFGMixSplitProperty.lot_srno;
                            drwNew = dtBarcodePrint.NewRow();
                            drwNew["lot_id"] = Val.ToInt(objMFGMixSplitProperty.lot_id);
                            drwNew["sr_no"] = Val.ToInt(SrNo + 1);
                            drwNew["receive_date"] = Val.DBDate(dtpReceiveDate.Text);
                            drwNew["kapan_no"] = Val.ToString(drw["kapan_no"]);
                            if (GlobalDec.gEmployeeProperty.department_name == "SARIN")
                            {
                                drwNew["pcs"] = Val.ToInt(drw["balance_pcs"]) > 0 ? Val.ToInt(drw["balance_pcs"]) : Val.ToInt(drw["rr_pcs"]);
                                drwNew["carat"] = Val.ToDecimal(drw["balance_carat"]) > 0 ? Val.ToDecimal(drw["balance_carat"]) : Val.ToDecimal(drw["rr_carat"]);
                            }
                            else
                            {
                                drwNew["pcs"] = Val.ToInt(drw["balance_pcs"]);
                                drwNew["carat"] = Val.ToDecimal(drw["balance_carat"]);
                            }
                            dtBarcodePrint.Rows.Add(drwNew);
                            count++;
                        }
                    }
                    if ((IntMixEntry == 0 && lblMode.Text == "EDIT") || (IntMixEntry == 1 && lblMode.Text == "EDIT"))
                    {
                        if (chkIsLotingDepartment.Checked == true)
                        {
                            int update_flag = 0;
                            foreach (DataRow drw in m_dtbLotMixSplit.Rows)
                            {
                                if (Val.ToInt(drw["final_lot_id"]) != 0)
                                {
                                    objMFGMixSplitProperty.to_lot_id = Val.ToInt(drw["final_lot_id"]);
                                    objMFGMixSplitProperty.lotting_department_id = Val.ToInt(lueDepartment.EditValue);
                                    objMFGMixSplitProperty.process_id = Val.ToInt(lueProcess.EditValue);
                                    objMFGMixSplitProperty.sub_process_id = Val.ToInt(lueSubProcess.EditValue);
                                    update_flag = update_flag + 1;
                                    objMFGMixSplitProperty.upd_flag = Val.ToInt(update_flag);
                                    IntRes = objMFGMixSplit.Update_LottingDepartment(objMFGMixSplitProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                                }
                            }
                        }
                        int count = 0;

                        foreach (DataRow drw in m_dtbLotMixSplit.Rows)
                        {
                            if (Val.ToInt(drw["edit_flag"]) == 1)
                            {
                                int p_InMixGrid = 0;

                                if (Val.ToInt(drw["final_lot_id"]) != 0)
                                {
                                    SrNo = objMFGMixSplit.GetLotNO(Val.ToInt(drw["final_lot_id"]), DLL.GlobalDec.EnumTran.Continue, Conn);
                                    objMFGMixSplitProperty.sr_no = SrNo;
                                    objMFGMixSplitProperty.flag = 1;
                                    objMFGMixSplitProperty.to_lot_id = Val.ToInt(drw["final_lot_id"]);
                                    if (Mix_Data.Select("lot_id ='" + Val.ToInt(drw["final_lot_id"]) + "'").Length > 0)
                                    {
                                        p_InMixGrid = 1;
                                        //blnReturn = false;
                                        //return blnReturn;
                                    }
                                }
                                else
                                {
                                    SrNo = objMFGMixSplit.GetSrNo(Val.ToInt(drw["rough_cut_id"]), DLL.GlobalDec.EnumTran.Continue, Conn);
                                    objMFGMixSplitProperty.sr_no = Val.ToInt(SrNo + 1);
                                    objMFGMixSplitProperty.flag = 0;
                                    objMFGMixSplitProperty.to_lot_id = Val.ToInt(0);
                                }
                                objMFGMixSplitProperty.receive_date = Val.DBDate(dtpReceiveDate.Text);
                                //objMFGMixSplitProperty.rough_lot_id = Val.ToInt(drw["lot_id"]);
                                objMFGMixSplitProperty.from_lot_id = Val.ToInt32(lblMixLot.Text);

                                objMFGMixSplitProperty.in_mixgrid = p_InMixGrid;
                                objMFGMixSplitProperty.rough_cut_id = Val.ToInt(drw["rough_cut_id"]);
                                objMFGMixSplitProperty.rough_sieve_id = Val.ToInt64(drw["rough_sieve_id"]);
                                objMFGMixSplitProperty.quality_id = Val.ToInt64(drw["quality_id"]);
                                objMFGMixSplitProperty.kapan_id = Val.ToInt64(drw["kapan_id"]);
                                objMFGMixSplitProperty.rough_clarity_id = Val.ToInt64(drw["rough_clarity_id"]);
                                objMFGMixSplitProperty.form_id = Val.ToInt(m_numForm_id);

                                objMFGMixSplitProperty.manager_id = Val.ToInt(Mix_Data.Rows[0]["manager_id"]);
                                objMFGMixSplitProperty.employee_id = Val.ToInt(Mix_Data.Rows[0]["employee_id"]);
                                objMFGMixSplitProperty.process_id = Val.ToInt(lueProcess.EditValue);
                                objMFGMixSplitProperty.sub_process_id = Val.ToInt(lueSubProcess.EditValue);
                                //objMFGMixSplitProperty.purity_id = Val.ToInt(drw["purity_id"]);


                                if (count == 0)
                                {
                                    objMFGMixSplitProperty.from_pcs = Val.ToInt(clmPcs.SummaryItem.SummaryValue);
                                    objMFGMixSplitProperty.from_carat = Val.ToDecimal(clmCarat.SummaryItem.SummaryValue);
                                    objMFGMixSplitProperty.plus_carat = Val.ToDecimal(clmCaratPlus.SummaryItem.SummaryValue);
                                    objMFGMixSplitProperty.loss_carat = Val.ToDecimal(clmLossCarat.SummaryItem.SummaryValue);

                                    foreach (DataRow drw1 in Mix_Data.Rows)
                                    {
                                        objMFGMixSplitProperty.lot_srno = Val.ToInt(drw1["lot_id"]);
                                        break;
                                    }
                                }
                                else
                                {
                                    objMFGMixSplitProperty.from_pcs = Val.ToInt(0);
                                    objMFGMixSplitProperty.from_carat = Val.ToDecimal(0);
                                    objMFGMixSplitProperty.lot_srno = 0;
                                }

                                objMFGMixSplitProperty.from_rr_pcs = Val.ToInt(clmRRPcs.SummaryItem.SummaryValue);
                                objMFGMixSplitProperty.from_rr_carat = Val.ToDecimal(clmRRCarat.SummaryItem.SummaryValue);
                                objMFGMixSplitProperty.from_rejection_pcs = Val.ToInt(clmRejPcs.SummaryItem.SummaryValue);
                                objMFGMixSplitProperty.from_rejection_carat = Val.ToDecimal(clmRejCarat.SummaryItem.SummaryValue);
                                objMFGMixSplitProperty.pcs = Val.ToInt(drw["balance_pcs"]);
                                objMFGMixSplitProperty.carat = Val.ToDecimal(drw["balance_carat"]);
                                objMFGMixSplitProperty.to_rr_pcs = Val.ToInt(drw["rr_pcs"]);
                                objMFGMixSplitProperty.to_rr_carat = Val.ToDecimal(drw["rr_carat"]);
                                objMFGMixSplitProperty.to_rejection_pcs = Val.ToInt(drw["rejection_pcs"]);
                                objMFGMixSplitProperty.to_rejection_carat = Val.ToDecimal(drw["rejection_carat"]);
                                objMFGMixSplitProperty.rate = Val.ToDecimal(drw["rate"]);
                                objMFGMixSplitProperty.amount = Val.ToDecimal(drw["amount"]);
                                objMFGMixSplitProperty.union_id = IntRes;
                                objMFGMixSplitProperty.mix_union_id = MixSplit_IntRes;
                                objMFGMixSplitProperty.receive_union_id = Receive_IntRes;
                                objMFGMixSplitProperty.issue_union_id = Issue_IntRes;
                                objMFGMixSplitProperty.history_union_id = NewHistory_Union_Id;
                                objMFGMixSplitProperty.lotting_department_id = Val.ToInt(drw["department_id"]);
                                //objMFGMixSplitProperty.lot_srno = Lot_SrNo;
                                objMFGMixSplitProperty.prediction_id = Val.ToInt(drw["prediction_id"]);
                                objMFGMixSplitProperty.issue_id = Val.ToInt(drw["issue_id"]);
                                objMFGMixSplitProperty.recieve_id = Val.ToInt(drw["receive_id"]);
                                //objMFGMixSplitProperty.plus_carat = Val.ToDecimal(drw["carat_plus"]);
                                //objMFGMixSplitProperty.loss_carat = Val.ToDecimal(drw["loss_carat"]);

                                objMFGMixSplitProperty = objMFGMixSplit.Update_MFGMixSplit(objMFGMixSplitProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                                IntRes = objMFGMixSplitProperty.union_id;
                                Receive_IntRes = objMFGMixSplitProperty.receive_union_id;
                                Issue_IntRes = objMFGMixSplitProperty.issue_union_id;
                                NewHistory_Union_Id = Val.ToInt64(objMFGMixSplitProperty.history_union_id);
                                //Lot_SrNo = objMFGMixSplitProperty.lot_srno;
                                //drwNew = dtBarcodePrint.NewRow();
                                //drwNew["lot_id"] = Val.ToInt(objMFGMixSplitProperty.lot_id);
                                //drwNew["sr_no"] = Val.ToInt(SrNo + 1);
                                //drwNew["receive_date"] = Val.DBDate(dtpReceiveDate.Text);
                                //drwNew["kapan_no"] = Val.ToString(drw["kapan_no"]);
                                //drwNew["pcs"] = Val.ToInt(drw["balance_pcs"]);
                                //drwNew["carat"] = Val.ToDecimal(drw["balance_carat"]);
                                //dtBarcodePrint.Rows.Add(drwNew);
                                count++;
                            }
                        }
                    }
                    #endregion

                    #region One To One And One To Many And Many To One

                    else
                    {
                        Count_Mix = Mix_Data.Rows.Count;

                        if ((Mix_Data.Rows.Count == 1 && m_dtbLotMixSplit.Rows.Count >= 1) || (Mix_Data.Rows.Count == 1 && m_dtbLotMixSplit.Rows.Count == 1))
                        {
                            int count = 0;
                            foreach (DataRow drw in Mix_Data.Rows)
                            {
                                int p_InMixGrid = 0;
                                int p_BalUpdate = 0;
                                foreach (DataRow DRow in m_dtbLotMixSplit.Rows)
                                {
                                    p_InMixGrid = 0;
                                    if (Val.ToInt(DRow["final_lot_id"]) != 0)
                                    {
                                        SrNo = objMFGMixSplit.GetLotNO(Val.ToInt(DRow["final_lot_id"]), DLL.GlobalDec.EnumTran.Continue, Conn);
                                        objMFGMixSplitProperty.sr_no = SrNo;
                                        objMFGMixSplitProperty.flag = 1;
                                        objMFGMixSplitProperty.to_lot_id = Val.ToInt(DRow["final_lot_id"]);

                                        if (Mix_Data.Select("lot_id ='" + Val.ToInt(DRow["final_lot_id"]) + "'").Length > 0)
                                        {
                                            p_InMixGrid = 1;
                                            p_BalUpdate = 1;
                                            //blnReturn = false;
                                            //return blnReturn;
                                        }
                                    }
                                    else
                                    {
                                        SrNo = objMFGMixSplit.GetSrNo(Val.ToInt(drw["rough_cut_id"]), DLL.GlobalDec.EnumTran.Continue, Conn);
                                        objMFGMixSplitProperty.sr_no = Val.ToInt(SrNo + 1);
                                        objMFGMixSplitProperty.flag = 0;
                                        objMFGMixSplitProperty.to_lot_id = Val.ToInt(0);
                                        p_InMixGrid = 0;
                                        //p_BalUpdate = 0;
                                    }
                                    if (Val.ToString(DRow["lotting_department"]) == "RUSSIAN")
                                    {
                                        objMFGMixSplitProperty.k_carat = Math.Round(Val.ToDecimal((Val.ToDecimal(DRow["balance_carat"]) * 100) / totalper), 3);
                                    }
                                    else if (Val.ToString(DRow["lotting_department"]) == "POLISH")
                                    {
                                        if (polishper > 0)
                                        {
                                            objMFGMixSplitProperty.k_carat = Math.Round(Val.ToDecimal((Val.ToDecimal(DRow["balance_carat"]) * 100) / polishper), 3);
                                        }
                                        else
                                        {
                                            objMFGMixSplitProperty.k_carat = 0;
                                        }
                                    }
                                    else if (Val.ToString(DRow["lotting_department"]) == "SOWABALE POLISH" || Val.ToString(DRow["lotting_department"]) == "SAWABLE")
                                    {
                                        if (polishper > 0)
                                        {
                                            objMFGMixSplitProperty.k_carat = Math.Round(Val.ToDecimal((Val.ToDecimal(DRow["balance_carat"]) * 100) / polishper), 3);
                                        }
                                        else
                                        {
                                            objMFGMixSplitProperty.k_carat = 0;
                                        }
                                    }
                                    else
                                    {
                                        objMFGMixSplitProperty.k_carat = Val.ToDecimal(DRow["balance_carat"]);
                                    }
                                    objMFGMixSplitProperty.receive_date = Val.DBDate(dtpReceiveDate.Text);
                                    objMFGMixSplitProperty.from_lot_id = Val.ToInt(drw["lot_id"]);
                                    if (count == 0)
                                    {
                                        objMFGMixSplitProperty.from_pcs = Val.ToInt(drw["pcs"]);
                                        objMFGMixSplitProperty.from_carat = Val.ToDecimal(drw["carat"]);
                                        objMFGMixSplitProperty.from_rr_pcs = Val.ToInt(drw["rr_pcs"]);
                                        objMFGMixSplitProperty.from_rr_carat = Val.ToDecimal(drw["rr_carat"]);
                                        objMFGMixSplitProperty.from_rejection_pcs = Val.ToInt(drw["rejection_pcs"]);
                                        objMFGMixSplitProperty.from_rejection_carat = Val.ToDecimal(drw["rejection_carat"]);
                                        objMFGMixSplitProperty.loss_carat = Val.ToDecimal(Loss_Carat);
                                        objMFGMixSplitProperty.plus_carat = Val.ToDecimal(Plus_carat);
                                    }
                                    else
                                    {
                                        objMFGMixSplitProperty.from_pcs = Val.ToInt(0);
                                        objMFGMixSplitProperty.from_carat = Val.ToDecimal(0);
                                        objMFGMixSplitProperty.from_rr_pcs = Val.ToInt(0);
                                        objMFGMixSplitProperty.from_rr_carat = Val.ToDecimal(0);
                                        objMFGMixSplitProperty.from_rejection_pcs = Val.ToInt(0);
                                        objMFGMixSplitProperty.from_rejection_carat = Val.ToDecimal(0);
                                        objMFGMixSplitProperty.loss_carat = Val.ToDecimal(0);
                                        objMFGMixSplitProperty.plus_carat = Val.ToDecimal(0);

                                    }
                                    objMFGMixSplitProperty.from_cut_id = Val.ToInt(drw["rough_cut_id"]);
                                    objMFGMixSplitProperty.from_kapan_id = Val.ToInt64(drw["kapan_id"]);

                                    objMFGMixSplitProperty.to_rr_pcs = Val.ToInt(DRow["rr_pcs"]);
                                    objMFGMixSplitProperty.to_rr_carat = Val.ToDecimal(DRow["rr_carat"]);
                                    objMFGMixSplitProperty.to_rejection_pcs = Val.ToInt(DRow["rejection_pcs"]);
                                    objMFGMixSplitProperty.to_rejection_carat = Val.ToDecimal(DRow["rejection_carat"]);
                                    objMFGMixSplitProperty.to_cut_id = Val.ToInt(DRow["rough_cut_id"]);
                                    objMFGMixSplitProperty.to_kapan_id = Val.ToInt64(DRow["kapan_id"]);
                                    objMFGMixSplitProperty.to_pcs = Val.ToInt(DRow["balance_pcs"]);
                                    objMFGMixSplitProperty.to_carat = Val.ToDecimal(DRow["balance_carat"]);

                                    objMFGMixSplitProperty.form_id = Val.ToInt(m_numForm_id);

                                    objMFGMixSplitProperty.manager_id = Val.ToInt(drw["manager_id"]);
                                    objMFGMixSplitProperty.employee_id = Val.ToInt(drw["employee_id"]);
                                    objMFGMixSplitProperty.process_id = Val.ToInt(lueProcess.EditValue);
                                    objMFGMixSplitProperty.sub_process_id = Val.ToInt(lueSubProcess.EditValue);
                                    objMFGMixSplitProperty.purity_id = Val.ToInt(drw["purity_id"]);
                                    objMFGMixSplitProperty.rough_clarity_id = Val.ToInt(DRow["rough_clarity_id"]);
                                    objMFGMixSplitProperty.rough_sieve_id = Val.ToInt(DRow["rough_sieve_id"]);
                                    objMFGMixSplitProperty.quality_id = Val.ToInt64(DRow["quality_id"]);

                                    objMFGMixSplitProperty.from_rate = Val.ToDecimal(DRow["rate"]);
                                    objMFGMixSplitProperty.from_amount = Val.ToDecimal(DRow["amount"]);
                                    objMFGMixSplitProperty.in_mixgrid = Val.ToInt(p_InMixGrid);
                                    objMFGMixSplitProperty.union_id = IntRes;
                                    objMFGMixSplitProperty.mix_union_id = MixSplit_IntRes;
                                    objMFGMixSplitProperty.receive_union_id = Receive_IntRes;
                                    objMFGMixSplitProperty.issue_union_id = Issue_IntRes;
                                    objMFGMixSplitProperty.history_union_id = NewHistory_Union_Id;
                                    objMFGMixSplitProperty.lot_srno = Lot_SrNo;
                                    objMFGMixSplitProperty.lotting_department_id = Val.ToInt(DRow["department_id"]);
                                    objMFGMixSplitProperty.is_repeat = chkIsRepeat.Checked == true ? 1 : 0;

                                    if ((Mix_Data.Rows.Count == 1 && m_dtbLotMixSplit.Rows.Count >= 1))
                                    {
                                        objMFGMixSplitProperty.transaction_type = "ONE-TO-MANY";
                                    }
                                    else
                                    {
                                        objMFGMixSplitProperty.transaction_type = "ONE-TO-ONE";
                                    }

                                    objMFGMixSplitProperty = objMFGMixSplit.Save_MFGMixSplitOneToMany(objMFGMixSplitProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                                    IntRes = objMFGMixSplitProperty.union_id;
                                    Receive_IntRes = objMFGMixSplitProperty.receive_union_id;
                                    Issue_IntRes = objMFGMixSplitProperty.issue_union_id;
                                    NewHistory_Union_Id = Val.ToInt64(objMFGMixSplitProperty.history_union_id);
                                    MixSplit_IntRes = Val.ToInt64(objMFGMixSplitProperty.mix_union_id);
                                    Lot_SrNo = Val.ToInt64(objMFGMixSplitProperty.lot_srno);
                                    drwNew = dtBarcodePrint.NewRow();
                                    drwNew["lot_id"] = Val.ToInt(objMFGMixSplitProperty.lot_id);
                                    drwNew["sr_no"] = Val.ToInt(SrNo + 1);
                                    drwNew["receive_date"] = Val.DBDate(dtpReceiveDate.Text);
                                    drwNew["kapan_no"] = Val.ToString(DRow["kapan_no"]);
                                    drwNew["pcs"] = Val.ToInt(DRow["balance_pcs"]);
                                    drwNew["carat"] = Val.ToDecimal(DRow["balance_carat"]);
                                    dtBarcodePrint.Rows.Add(drwNew);
                                    count++;
                                }
                                if (p_BalUpdate == 0)
                                {
                                    IntRes_MixSplit = objMFGMixSplit.Update_Balance_Carat(Val.ToString(drw["lot_id"]), DLL.GlobalDec.EnumTran.Continue, Conn);
                                }

                            }
                        }
                        else if (Mix_Data.Rows.Count >= 1 && m_dtbLotMixSplit.Rows.Count == 1)
                        {
                            Int64 NewLotId = 0;
                            foreach (DataRow drw in m_dtbLotMixSplit.Rows)
                            {
                                int p_InMixGrid = 0;
                                int count = 0;
                                foreach (DataRow DRow in Mix_Data.Rows)
                                {

                                    objMFGMixSplitProperty.receive_date = Val.DBDate(dtpReceiveDate.Text);
                                    objMFGMixSplitProperty.from_lot_id = Val.ToInt(DRow["lot_id"]);

                                    if (Val.ToInt(drw["final_lot_id"]) != 0)
                                    {
                                        SrNo = objMFGMixSplit.GetLotNO(Val.ToInt(drw["final_lot_id"]), DLL.GlobalDec.EnumTran.Continue, Conn);
                                        objMFGMixSplitProperty.sr_no = SrNo;
                                        if (count == 0)
                                        {
                                            objMFGMixSplitProperty.flag = 1;
                                        }
                                        else
                                        {
                                            objMFGMixSplitProperty.flag = 0;
                                        }

                                        objMFGMixSplitProperty.to_lot_id = Val.ToInt(drw["final_lot_id"]);
                                        if (Mix_Data.Select("lot_id ='" + Val.ToInt(drw["final_lot_id"]) + "'").Length > 0)
                                        {
                                            p_InMixGrid = 1;
                                            //blnReturn = false;
                                            //return blnReturn;
                                        }
                                    }
                                    else
                                    {
                                        SrNo = objMFGMixSplit.GetSrNo(Val.ToInt(drw["rough_cut_id"]), DLL.GlobalDec.EnumTran.Continue, Conn);
                                        objMFGMixSplitProperty.sr_no = Val.ToInt(SrNo + 1);
                                        objMFGMixSplitProperty.flag = 0;
                                        objMFGMixSplitProperty.to_lot_id = Val.ToInt(NewLotId);
                                    }
                                    objMFGMixSplitProperty.in_mixgrid = Val.ToInt(p_InMixGrid);
                                    //objMFGMixSplitProperty.to_lot_id = Val.ToInt(NewLotId);
                                    objMFGMixSplitProperty.from_cut_id = Val.ToInt(DRow["rough_cut_id"]);
                                    objMFGMixSplitProperty.from_kapan_id = Val.ToInt64(DRow["kapan_id"]);

                                    if (Val.ToString(drw["lotting_department"]) == "RUSSIAN")
                                    {
                                        objMFGMixSplitProperty.k_carat = Math.Round(Val.ToDecimal((Val.ToDecimal(drw["balance_carat"]) * 100) / totalper), 3);
                                    }
                                    else if (Val.ToString(drw["lotting_department"]) == "POLISH")
                                    {
                                        if (polishper > 0)
                                        {
                                            objMFGMixSplitProperty.k_carat = Math.Round(Val.ToDecimal((Val.ToDecimal(drw["balance_carat"]) * 100) / polishper), 3);
                                        }
                                        else
                                        {
                                            objMFGMixSplitProperty.k_carat = 0;
                                        }
                                    }
                                    else if (Val.ToString(drw["lotting_department"]) == "SOWABALE POLISH" || Val.ToString(drw["lotting_department"]) == "SAWABLE")
                                    {
                                        if (polishper > 0)
                                        {
                                            objMFGMixSplitProperty.k_carat = Math.Round(Val.ToDecimal((Val.ToDecimal(drw["balance_carat"]) * 100) / polishper), 3);
                                        }
                                        else
                                        {
                                            objMFGMixSplitProperty.k_carat = 0;
                                        }
                                    }
                                    else
                                    {
                                        objMFGMixSplitProperty.k_carat = Val.ToDecimal(drw["balance_carat"]);
                                    }
                                    objMFGMixSplitProperty.from_pcs = Val.ToInt(DRow["pcs"]);
                                    objMFGMixSplitProperty.from_carat = Val.ToDecimal(DRow["carat"]);
                                    objMFGMixSplitProperty.from_rr_pcs = Val.ToInt(DRow["rr_pcs"]);
                                    objMFGMixSplitProperty.from_rr_carat = Val.ToDecimal(DRow["rr_carat"]);
                                    objMFGMixSplitProperty.from_rejection_pcs = Val.ToInt(DRow["rejection_pcs"]);
                                    objMFGMixSplitProperty.from_rejection_carat = Val.ToDecimal(DRow["rejection_carat"]);
                                    objMFGMixSplitProperty.to_cut_id = Val.ToInt(drw["rough_cut_id"]);
                                    objMFGMixSplitProperty.to_kapan_id = Val.ToInt64(drw["kapan_id"]);
                                    if (count == 0)
                                    {
                                        objMFGMixSplitProperty.to_pcs = Val.ToInt(drw["balance_pcs"]);
                                        objMFGMixSplitProperty.to_carat = Val.ToDecimal(drw["balance_carat"]);
                                        objMFGMixSplitProperty.to_rr_pcs = Val.ToInt(drw["rr_pcs"]);
                                        objMFGMixSplitProperty.to_rr_carat = Val.ToDecimal(drw["rr_carat"]);
                                        objMFGMixSplitProperty.to_rejection_pcs = Val.ToInt(drw["rejection_pcs"]);
                                        objMFGMixSplitProperty.to_rejection_carat = Val.ToDecimal(drw["rejection_carat"]);
                                    }
                                    else
                                    {
                                        objMFGMixSplitProperty.to_pcs = Val.ToInt(0);
                                        objMFGMixSplitProperty.to_carat = Val.ToDecimal(0);
                                        objMFGMixSplitProperty.to_rr_pcs = Val.ToInt(0);
                                        objMFGMixSplitProperty.to_rr_carat = Val.ToDecimal(0);
                                        objMFGMixSplitProperty.to_rejection_pcs = Val.ToInt(0);
                                        objMFGMixSplitProperty.to_rejection_carat = Val.ToDecimal(0);
                                    }


                                    objMFGMixSplitProperty.form_id = Val.ToInt(m_numForm_id);

                                    objMFGMixSplitProperty.manager_id = Val.ToInt(DRow["manager_id"]);
                                    objMFGMixSplitProperty.employee_id = Val.ToInt(DRow["employee_id"]);
                                    objMFGMixSplitProperty.process_id = Val.ToInt(lueProcess.EditValue);
                                    objMFGMixSplitProperty.sub_process_id = Val.ToInt(lueSubProcess.EditValue);
                                    objMFGMixSplitProperty.purity_id = Val.ToInt(DRow["purity_id"]);
                                    objMFGMixSplitProperty.rough_clarity_id = Val.ToInt(drw["rough_clarity_id"]);
                                    objMFGMixSplitProperty.rough_sieve_id = Val.ToInt(drw["rough_sieve_id"]);
                                    objMFGMixSplitProperty.quality_id = Val.ToInt64(drw["quality_id"]);

                                    objMFGMixSplitProperty.loss_carat = Val.ToDecimal(0);
                                    objMFGMixSplitProperty.plus_carat = Val.ToDecimal(0);
                                    objMFGMixSplitProperty.from_rate = Val.ToDecimal(DRow["rate"]);
                                    objMFGMixSplitProperty.from_amount = Val.ToDecimal(DRow["amount"]);
                                    objMFGMixSplitProperty.rate = Val.ToDecimal(drw["rate"]);
                                    objMFGMixSplitProperty.amount = Val.ToDecimal(drw["amount"]);
                                    objMFGMixSplitProperty.union_id = IntRes;
                                    objMFGMixSplitProperty.mix_union_id = MixSplit_IntRes;
                                    objMFGMixSplitProperty.receive_union_id = Receive_IntRes;
                                    objMFGMixSplitProperty.issue_union_id = Issue_IntRes;
                                    objMFGMixSplitProperty.history_union_id = NewHistory_Union_Id;
                                    objMFGMixSplitProperty.lot_srno = Lot_SrNo;
                                    objMFGMixSplitProperty.lotting_department_id = Val.ToInt(DRow["department_id"]);
                                    objMFGMixSplitProperty.is_repeat = chkIsRepeat.Checked == true ? 1 : 0;
                                    objMFGMixSplitProperty.transaction_type = "MANY-TO-ONE";
                                    objMFGMixSplitProperty = objMFGMixSplit.Save_MFGMixSplitManyToOne(objMFGMixSplitProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                                    IntRes = objMFGMixSplitProperty.union_id;
                                    Receive_IntRes = objMFGMixSplitProperty.receive_union_id;
                                    Issue_IntRes = objMFGMixSplitProperty.issue_union_id;
                                    NewHistory_Union_Id = Val.ToInt64(objMFGMixSplitProperty.history_union_id);
                                    NewLotId = objMFGMixSplitProperty.to_lot_id;
                                    Lot_SrNo = Val.ToInt64(objMFGMixSplitProperty.lot_srno);
                                    MixSplit_IntRes = Val.ToInt64(objMFGMixSplitProperty.mix_union_id);
                                    if (p_InMixGrid == 0)
                                    {
                                        IntRes_MixSplit = objMFGMixSplit.Update_Balance_Carat(Val.ToString(DRow["lot_id"]), DLL.GlobalDec.EnumTran.Continue, Conn);
                                    }
                                    drwNew = dtBarcodePrint.NewRow();
                                    drwNew["lot_id"] = Val.ToInt(objMFGMixSplitProperty.to_lot_id);
                                    drwNew["sr_no"] = Val.ToInt(objMFGMixSplitProperty.sr_no);
                                    drwNew["receive_date"] = Val.DBDate(dtpReceiveDate.Text);
                                    drwNew["kapan_no"] = Val.ToString(drw["kapan_no"]);
                                    drwNew["pcs"] = Val.ToInt(drw["balance_pcs"]);
                                    drwNew["carat"] = Val.ToDecimal(drw["balance_carat"]);
                                    dtBarcodePrint.Rows.Add(drwNew);
                                    count++;
                                }
                            }
                        }
                    }

                    #endregion

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
        private void backgroundWorker_LotSplit_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (IntRes > 0)
                {
                    Global.Confirm("Lot Split Recieve Data Save Succesfully");
                    btnSave.Enabled = true;

                    ClearDetails();

                    if (GlobalDec.gEmployeeProperty.department_name == "MAKABLE" || GlobalDec.gEmployeeProperty.department_name == "SAWABLE" || GlobalDec.gEmployeeProperty.department_name == "SARIN")
                    {

                        DialogResult result = MessageBox.Show("Do you want to Print Barcode?", "Confirmation", MessageBoxButtons.YesNoCancel);
                        if (result != DialogResult.Yes)
                        {
                            btnSave.Enabled = true;
                            return;
                        }
                        else
                        {
                            PrintBarcode();
                            btnSave.Enabled = true;
                        }
                    }
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
                Global.LOOKUPProcess(lueProcess);
                Global.LOOKUPSubProcess(lueSubProcess);
                m_dtbSubProcess = (((DataTable)lueSubProcess.Properties.DataSource).Copy());
                Global.LOOKUPRoughSieve(lueRoughSieve);
                Global.LOOKUPQuality(lueQuality);
                Global.LOOKUPClarity(lueClarity);
                Global.LOOKUPDepartment_New(lueDepartment);

                lblMode.Text = "NEW";
                ClearDetails();

                lueProcess.Text = "4P LOTING";
                lueSubProcess.Text = "4P LOTING";
                lueDepartment.Text = "4P";
                //if (GlobalDec.gEmployeeProperty.role_name == "SURAT SARIN")
                //{
                //    chkAutoSplit.Checked = true;
                //}

                if (GlobalDec.gEmployeeProperty.role_name.ToUpper() == "SURAT MAKABLE" || GlobalDec.gEmployeeProperty.role_name.ToUpper() == "SURAT ADMIN")
                {
                    if (GlobalDec.gEmployeeProperty.user_name.ToUpper() == "PRAFUL" || GlobalDec.gEmployeeProperty.user_name.ToUpper() == "PRAGNESH")
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
            txtAmount.Text = Val.ToString((Val.ToDecimal(txtCarat.Text) + Val.ToDecimal(txtRejCarat.Text)) * Val.ToDecimal(txtRate.Text));
        }
        private void dgvMixSplit_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                DataTable dtAmount = new DataTable();
                dtAmount = (DataTable)grdMixSplit.DataSource;

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
                            rate = Math.Round(amount / carat, 2);
                            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                                e.TotalValue = rate;
                            column = "";
                            lblTotalCrt.Text = Val.ToString(Val.ToDecimal(lblTotalCrt.Text) - carat);

                            carat = 0;
                            amount = 0;
                        }

                        decimal Mix_Carat = Val.ToDecimal(clmCarat.SummaryItem.SummaryValue);
                        decimal Split_Carat = Val.ToDecimal(clmSplitCarat.SummaryItem.SummaryValue);
                        lblTotalCrt.Text = Val.ToString(Mix_Carat - Split_Carat);
                        Mix_Carat = 0;
                        Split_Carat = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        private void txtLotID_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
            {
                IDataObject clipData = Clipboard.GetDataObject();
                String Data = Val.ToString(clipData.GetData(System.Windows.Forms.DataFormats.Text));
                String str1 = Data.Replace("\r\n", ",");                   //data.Replace(\n, ",");
                str1 = str1.Trim();
                str1 = str1.TrimEnd();
                str1 = str1.TrimStart();
                str1 = str1.TrimEnd(',');
                str1 = str1.TrimStart(',');
                txtLotID.Text = str1;
            }
        }
        private void txtLotID_MouseDown(object sender, MouseEventArgs e)
        {
            if (txtLotID.Focus())
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    PasteData = Val.ToString(PasteclipData.GetData(System.Windows.Forms.DataFormats.Text));
                }
            }
        }
        private void dgvMixSplit_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvMixSplit.GetDataRow(e.RowHandle);
                        btnAdd.Text = "&Update";
                        //lueKapan.Text = Val.ToString(Drow["kapan_no"]);
                        lueKapan.EditValue = Val.ToInt64(Drow["kapan_id"]);
                        lueCutNo.EditValue = Val.ToInt64(Drow["rough_cut_id"]);
                        //lueCutNo.Text = Val.ToString(Drow["rough_cut_no"]);
                        txtPcs.Text = Val.ToString(Drow["balance_pcs"]);
                        txtCarat.Text = Val.ToString(Drow["balance_carat"]);
                        txtRejPcs.Text = Val.ToString(Drow["rejection_pcs"]);
                        txtRejCarat.Text = Val.ToString(Drow["rejection_carat"]);
                        txtRRPcs.Text = Val.ToString(Drow["rr_pcs"]);
                        txtRRCarat.Text = Val.ToString(Drow["rr_carat"]);
                        txtWeightLoss.Text = Val.ToString(Drow["loss_carat"]);
                        txtWeightPlus.Text = Val.ToString(Drow["plus_carat"]);
                        txtRate.Text = Val.ToString(Drow["rate"]);
                        txtAmount.Text = Val.ToString(Drow["amount"]);
                        lueQuality.Text = Val.ToString(Drow["quality_name"]);
                        lueQuality.Tag = Val.ToInt(Drow["quality_id"]);
                        lueRoughSieve.EditValue = Val.ToInt(Drow["rough_sieve_id"]);
                        lueClarity.EditValue = Val.ToInt(Drow["rough_clarity_id"]);
                        txtFinalLotID.Text = Val.ToString(Drow["final_lot_id"]);
                        lueDepartment.EditValue = Val.ToInt(Drow["department_id"]);

                        m_cut_no = Val.ToString(Drow["rough_cut_no"]);
                        m_update_srno = Val.ToInt(Drow["sr_no"]);
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void btnPopUpStock_Click(object sender, EventArgs e)
        {
            if (lblMode.Text == "NEW")
                GetPendingStock();
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
                    txtCarat.Text = Val.Val(clmCarat.SummaryItem.SummaryValue).ToString();
                    txtPcs.Text = Val.ToInt(clmPcs.SummaryItem.SummaryValue).ToString();
                    lblTotalCrt.Text = Val.Val(clmCarat.SummaryItem.SummaryValue).ToString();
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
        private void lueMixCutNo_EditValueChanged(object sender, EventArgs e)
        {
            if (Val.ToInt(lueMixCutNo.EditValue) > 0)
            {
                btnPopUpStock.Enabled = true;
            }
            else
            {
                btnPopUpStock.Enabled = false;
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
        private void txtCarat_EditValueChanged(object sender, EventArgs e)
        {
            txtAmount.Text = Val.ToString((Val.ToDecimal(txtCarat.Text) + Val.ToDecimal(txtRejCarat.Text)) * Val.ToDecimal(txtRate.Text));

            //if (chkAutoSplit.Checked)
            //{
            //    txtRRCarat.Text = Val.ToString(Val.ToDecimal(clmCarat.SummaryItem.SummaryValue) + Val.ToDecimal(clmRRCarat.SummaryItem.SummaryValue) - Val.ToDecimal(txtCarat.Text));
            //}
        }
        private void lueKapan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                lueCutNo.Focus();
            }
        }
        private void lueCutNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtPcs.Focus();
            }
        }
        private void lueClarity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtRejPcs.Focus();
            }
        }
        private void txtRejCarat_EditValueChanged(object sender, EventArgs e)
        {
            txtAmount.Text = Val.ToString((Val.ToDecimal(txtCarat.Text) + Val.ToDecimal(txtRejCarat.Text)) * Val.ToDecimal(txtRate.Text));
        }
        private void lueProcess_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (lueProcess.EditValue != System.DBNull.Value)
                {
                    if (m_dtbSubProcess.Rows.Count > 0)
                    {
                        DataTable dtbdetail = m_dtbSubProcess;

                        string strFilter = string.Empty;

                        if (lueProcess.Text != "")
                            strFilter = "process_id = " + lueProcess.EditValue;

                        dtbdetail.DefaultView.RowFilter = strFilter;
                        dtbdetail.DefaultView.ToTable();

                        DataTable dtb = dtbdetail.DefaultView.ToTable();

                        lueSubProcess.Properties.DataSource = dtb;
                        lueSubProcess.Properties.ValueMember = "sub_process_id";
                        lueSubProcess.Properties.DisplayMember = "sub_process_name";
                        lueSubProcess.EditValue = System.DBNull.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void lueProcess_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmProcessMaster frmProcess = new FrmProcessMaster();
                frmProcess.ShowDialog();
                Global.LOOKUPProcess(lueProcess);
            }
        }
        private void lueSubProcess_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmMfgSubProcessMaster frmSubProcess = new FrmMfgSubProcessMaster();
                frmSubProcess.ShowDialog();
                Global.LOOKUPSubProcess(lueSubProcess);
                m_dtbSubProcess = (((DataTable)lueSubProcess.Properties.DataSource).Copy());
            }
        }
        private void repositoryItemBtnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (Global.Confirm("Are you sure delete selected row?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                dgvMixSplit.DeleteRow(dgvMixSplit.GetRowHandle(dgvMixSplit.FocusedRowHandle));
                m_dtbLotMixSplit.AcceptChanges();
            }
        }
        private void repositoryItemBtnDeleteMix_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (Global.Confirm("Are you sure delete selected row?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                dgvDet.DeleteRow(dgvDet.GetRowHandle(dgvDet.FocusedRowHandle));
                DTab_StockData.AcceptChanges();
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            FrmMFGLottingSearch FrmSearchLotting = new FrmMFGLottingSearch();
            FrmSearchLotting.FrmMFGMixSplitLottingSarin = this;
            FrmSearchLotting.ShowForm(this);
        }
        private void txtPcs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (GlobalDec.gEmployeeProperty.role_name != "SURAT ROUGH")
                {
                    if (Val.ToInt(txtPcs.Text) == 0)
                    {
                        txtPcs.Text = string.Empty;
                        txtPcs.Focus();
                    }
                }
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

                m_dtbLotMixSplit.Columns.Add("balance_pcs", typeof(int)).DefaultValue = 0;
                m_dtbLotMixSplit.Columns.Add("balance_carat", typeof(decimal)).DefaultValue = 0;
                m_dtbLotMixSplit.Columns.Add("rejection_pcs", typeof(int)).DefaultValue = 0;
                m_dtbLotMixSplit.Columns.Add("rejection_carat", typeof(decimal)).DefaultValue = 0;
                m_dtbLotMixSplit.Columns.Add("rr_pcs", typeof(int)).DefaultValue = 0;
                m_dtbLotMixSplit.Columns.Add("rr_carat", typeof(decimal)).DefaultValue = 0;
                m_dtbLotMixSplit.Columns.Add("plus_carat", typeof(decimal)).DefaultValue = 0.000;
                m_dtbLotMixSplit.Columns.Add("loss_carat", typeof(decimal)).DefaultValue = 0.000;
                m_dtbLotMixSplit.Columns.Add("sr_no", typeof(decimal)).DefaultValue = 1;
                m_dtbLotMixSplit.Columns.Add("rate", typeof(decimal)).DefaultValue = 0;
                m_dtbLotMixSplit.Columns.Add("amount", typeof(decimal)).DefaultValue = 0;
                m_dtbLotMixSplit.Columns.Add("final_lot_id", typeof(int));
                m_dtbLotMixSplit.Columns.Add("department_id", typeof(int));
                m_dtbLotMixSplit.Columns.Add("lotting_department", typeof(string));
                m_dtbLotMixSplit.Columns.Add("edit_flag", typeof(int)).DefaultValue = 0;
                grdMixSplit.DataSource = m_dtbLotMixSplit;
                grdMixSplit.Refresh();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
        }
        private void CalculateSummary()
        {
            dgvDet.PostEditor();
            dgvDet.RefreshData();
            DTab_StockData.AcceptChanges();

            foreach (DataRow DRow in DTab_StockData.Rows)
            {
                // Total Summary Details
                IntTotalLot++;
                // Comment By Praful On 11102017

                IntTotalPcs += Val.ToInt(DRow["pcs"]);
                DblTotalCarat += Val.Val(DRow["carat"]);
            }
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

                m_dtbKapan = Global.GetKapanAll();

                lueKapan.Properties.DataSource = m_dtbKapan;
                lueKapan.Properties.ValueMember = "kapan_id";
                lueKapan.Properties.DisplayMember = "kapan_no";

                m_dtbParam = Global.GetRoughCutAll();

                lueCutNo.Properties.DataSource = m_dtbParam;
                lueCutNo.Properties.ValueMember = "rough_cut_id";
                lueCutNo.Properties.DisplayMember = "rough_cut_no";

                lueKapan.EditValue = System.DBNull.Value;
                lueCutNo.EditValue = System.DBNull.Value;

                lueMixKapan.EditValue = System.DBNull.Value;
                lueMixCutNo.EditValue = System.DBNull.Value;

                lueQuality.EditValue = System.DBNull.Value;
                lueClarity.EditValue = System.DBNull.Value;
                lueRoughSieve.EditValue = System.DBNull.Value;
                //lueDepartment.EditValue = System.DBNull.Value;
                txtPcs.Text = string.Empty;
                txtCarat.Text = string.Empty;
                txtRate.Text = string.Empty;
                txtAmount.Text = string.Empty;
                txtRejPcs.Text = string.Empty;
                txtRejCarat.Text = string.Empty;
                txtWeightLoss.Text = string.Empty;
                txtWeightPlus.Text = string.Empty;
                btnPopUpStock.Enabled = false;
                txtRRPcs.Text = string.Empty;
                txtRRCarat.Text = string.Empty;
                DTab_StockData.Rows.Clear();
                DTab_StockData.AcceptChanges();
                txtFinalLotID.Text = "";
                txtLotID.Text = string.Empty;
                lblMode.Text = "NEW";
                lblMode.Tag = 0;
                chkIsLotingDepartment.Checked = false;
                chkIsLotingDepartment.Visible = false;
                grdDet.DataSource = null;
                grdDet.RefreshDataSource();
                btnDelete.Enabled = true;
                //grdMixSplit.DataSource = null;
                //grdMixSplit.RefreshDataSource();

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
        private bool Save_Validate()
        {
            bool blnReturn = true;

            try
            {
                m_blnadd = true;
                m_blnsave = false;
                if (!Validate_PopUp())
                {
                    m_blnadd = false;
                    blnReturn = false;
                    return blnReturn;
                }
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
                    lstError.Add(new ListError(13, "Cut No"));
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
        public void FillGrid(int UnionId, string TransType)
        {

            DTab_StockData = new DataTable();
            m_dtbLotMixSplit = new DataTable();
            DTab_StockData = objProcessRecieve.GetLottingMainData(UnionId);
            m_dtbLotMixSplit = objProcessRecieve.GetLottingSplitData(UnionId, TransType);
            if (DTab_StockData.Rows.Count > 0)
            {
                lueMixKapan.EditValue = Val.ToInt64(DTab_StockData.Rows[0]["kapan_id"]);
                lueMixCutNo.EditValue = Val.ToInt64(DTab_StockData.Rows[0]["rough_cut_id"]);
                lueKapan.EditValue = System.DBNull.Value;
                lueCutNo.EditValue = System.DBNull.Value;
                txtLotID.Text = "";
                txtLotID.Focus();

            }

            grdDet.DataSource = DTab_StockData;
            grdDet.RefreshDataSource();
            dgvDet.BestFitColumns();
            CalculateSummary();

            if (m_dtbLotMixSplit.Rows.Count > 0)
            {
                lueProcess.EditValue = Val.ToInt32(m_dtbLotMixSplit.Rows[0]["process_id"]);
                lueSubProcess.EditValue = Val.ToInt32(m_dtbLotMixSplit.Rows[0]["sub_process_id"]);
                lueDepartment.Text = Val.ToString(m_dtbLotMixSplit.Rows[0]["lotting_department"]);
                grdMixSplit.DataSource = m_dtbLotMixSplit;
                lblMode.Text = "EDIT";
                lblMode.Tag = Val.ToInt32(m_dtbLotMixSplit.Rows[0]["lot_srno"]);
                lblMixLot.Text = Val.ToString(m_dtbLotMixSplit.Rows[0]["from_lot_id"]);
                lblTotalCrt.Text = Val.ToString(clmCarat.SummaryItem.SummaryValue);
                chkIsLotingDepartment.Visible = true;
            }
            else
            {
                lblMode.Text = "NEW";
                lblTotalCrt.Text = "0";
                lblMixLot.Text = "0";
            }

        }
        public void GetPendingStock()
        {
            try
            {
                if (Save_Validate())
                {
                    DataTable DTab_Pending_Stock = new DataTable();
                    string StrLotList = "";
                    MFGProcessIssue objMFGProcessIssue = new MFGProcessIssue();
                    MFGProcessIssueProperty objMFGProcessIssueProperty = new MFGProcessIssueProperty();
                    objMFGProcessIssueProperty.rough_cut_id = Val.ToInt(lueMixCutNo.EditValue);
                    objMFGProcessIssueProperty.kapan_id = Val.ToInt(lueMixKapan.EditValue);

                    DtPending = objMFGProcessIssue.GetPendingDeptStock(Val.ToInt64(0), objMFGProcessIssueProperty);

                    if (GlobalDec.gEmployeeProperty.role_name == "SURAT MAKABLE" || GlobalDec.gEmployeeProperty.role_name == "SURAT SARIN" || GlobalDec.gEmployeeProperty.role_name == "SURAT KATARGAM")
                    {
                        if (DtPending.Rows.Count > 0)
                        {
                            for (int i = 0; i < DtPending.Rows.Count; i++)
                            {
                                if (Val.ToInt(DtPending.Rows[i]["lot_id"]) != 0)
                                {
                                    DataTable DtConfirm = Global.CheckConfirmLot(Val.ToInt(DtPending.Rows[i]["lot_id"]));
                                    if (DtConfirm.Rows.Count != 0)
                                    {
                                        if (StrLotList.Length > 0)
                                        {
                                            StrLotList = StrLotList + "," + Val.ToInt(DtPending.Rows[i]["lot_id"]);
                                        }
                                        else
                                        {
                                            StrLotList = Val.ToInt(DtPending.Rows[i]["lot_id"]).ToString();
                                        }
                                        //Global.Message("Please Confirm Lot First!!! " + Val.ToInt(DtPending.Rows[i]["lot_id"]));
                                        //DtPending = new DataTable();
                                        //return;
                                    }
                                }

                            }
                            DtPending = DtPending.Select("lot_id in(" + StrLotList + ")").CopyToDataTable();
                        }
                    }

                    FrmMFGStockConfirm FrmStockConfirm = new FrmMFGStockConfirm();
                    FrmStockConfirm.FrmMFGMixSplitLottingSarin = this;
                    FrmStockConfirm.DTab = DtPending;
                    FrmStockConfirm.ShowForm(this);
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
            }
        }
        private void PrintBarcode()
        {
            BarcodePrint printBarCode = new BarcodePrint();
            //DataTable dtCheckedBarcode = (DataTable)grdBarcodePrint.DataSource;
            //dtCheckedBarcode = dtCheckedBarcode.Select("print = 'True' ").CopyToDataTable();
            for (int i = 0; i < dtBarcodePrint.Rows.Count; i++)
            {
                if (dtBarcodePrint.Rows[i]["lot_id"] != null && dtBarcodePrint.Rows[i]["carat"].ToString() != "")
                {
                    printBarCode.AddPkt(dtBarcodePrint.Rows[i]["kapan_no"].ToString(), dtBarcodePrint.Rows[i]["sr_no"].ToString(), Val.ToString(dtBarcodePrint.Rows[i]["receive_date"]),
                        Val.ToInt(dtBarcodePrint.Rows[i]["lot_id"]), Val.ToInt(dtBarcodePrint.Rows[i]["pcs"]), Math.Round(Val.ToDecimal(dtBarcodePrint.Rows[i]["carat"]), 3), true);
                }
            }
            //printBarCode.PrintDMX();
            printBarCode.PrintTSC();
        }
        public void GetStockData(DataTable Stock_Data)
        {
            try
            {
                DTabTemp = Stock_Data.Copy();
                DTab_StockData.AcceptChanges();
                if (DTab_StockData != null)
                {
                    if (DTab_StockData.Rows.Count > 0)
                    {
                        for (int i = 0; i < DTab_StockData.Rows.Count; i++)
                        {
                            for (int j = 0; j < DTabTemp.Rows.Count; j++)
                            {
                                if (DTab_StockData.Rows[i]["lot_id"].ToString() == DTabTemp.Rows[j]["lot_id"].ToString())
                                {
                                    Global.Message(DTab_StockData.Rows[i]["lot_id"].ToString() + " = Lot ID already added to the Issue list!");
                                    txtLotID.Text = "";
                                    txtLotID.Focus();
                                    return;
                                }
                            }
                        }
                    }
                }

                if (DTab_StockData.Rows.Count > 0)
                {
                    if (DTabTemp.Rows.Count > 0)
                    {
                        Process_Id = Val.ToInt(DTabTemp.Rows[0]["process_id"]);
                        for (int i = 0; i < DTabTemp.Rows.Count; i++)
                        {
                            if (Process_Id != Val.ToInt(DTabTemp.Rows[i]["process_id"]))
                            {
                                Global.Message("Difference Process Name in this lot ID =" + DTabTemp.Rows[i]["lot_id"].ToString());
                                txtLotID.Text = "";
                                txtLotID.Focus();
                                return;
                            }
                        }
                        lueKapan.EditValue = Val.ToInt64(DTabTemp.Rows[0]["kapan_id"]);
                        lueCutNo.EditValue = Val.ToInt64(DTabTemp.Rows[0]["rough_cut_id"]);
                    }
                    DTab_StockData.Merge(DTabTemp);
                }
                else
                {
                    DataTable DTab_ValidateBarcode = new DataTable();
                    if (txtLotID.Text == "")
                    {
                        DTab_ValidateBarcode = DTabTemp.Copy();
                    }
                    else
                    {
                        DTab_ValidateBarcode = objMFGMixSplit.Live_Stock_GetData(Val.Trim(txtLotID.Text));
                    }


                    if (DTab_ValidateBarcode.Rows.Count > 0)
                    {
                    }
                    else
                    {
                        Global.Message("Lot ID Not found");
                        txtLotID.Text = "";
                        txtLotID.Focus();
                        return;
                    }
                    if (txtLotID.Text == "")
                    {
                        DTab_StockData = DTabTemp.Copy();
                    }
                    else
                    {
                        DTab_StockData = objMFGMixSplit.Live_Stock_GetData(Val.Trim(txtLotID.Text));
                    }


                    if (DTab_StockData.Rows.Count > 0)
                    {
                        lueKapan.EditValue = Val.ToInt64(DTab_StockData.Rows[0]["kapan_id"]);
                        lueCutNo.EditValue = Val.ToInt64(DTab_StockData.Rows[0]["rough_cut_id"]);

                        txtLotID.Text = "";
                        txtLotID.Focus();
                    }
                }

                grdDet.DataSource = DTab_StockData;
                grdDet.RefreshDataSource();
                dgvDet.BestFitColumns();
                CalculateSummary();
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
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
        private void Export1(string format, string dlgHeader, string dlgFilter)
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
                            dgvMixSplit.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvMixSplit.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvMixSplit.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvMixSplit.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvMixSplit.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvMixSplit.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvMixSplit.ExportToCsv(Filepath);
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
        private void MNExportExcel_Click1(object sender, EventArgs e)
        {
            //Global.Export("xlsx", dgvRoughClarityMaster);
            Export1("xlsx", "Export to Excel", "Excel files 97-2003 (Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*");
        }
        private void MNExportPDF_Click1(object sender, EventArgs e)
        {
            // Global.Export("pdf", dgvRoughClarityMaster);
            Export1("pdf", "Export Report to PDF", "PDF (*.PDF)|*.PDF");
        }
        private void MNExportTEXT_Click1(object sender, EventArgs e)
        {
            Export1("txt", "Export to Text", "Text files (*.txt)|*.txt|All files (*.*)|*.*");
        }

        private void MNExportHTML_Click1(object sender, EventArgs e)
        {
            Export1("html", "Export to HTML", "Html files (*.html)|*.html|Htm files (*.htm)|*.htm");
        }

        private void MNExportRTF_Click1(object sender, EventArgs e)
        {
            Export1("rtf", "Export to RTF", "Word (*.doc) |*.doc;*.rtf|(*.txt) |*.txt|(*.*) |*.*");
        }

        private void MNExportCSV_Click1(object sender, EventArgs e)
        {
            Export1("csv", "Export Report to CSVB", "csv (*.csv)|*.csv");
        }
        #endregion

        private void repositoryItemBtnDeleteMix1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (Global.Confirm("Are you sure delete selected row?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                dgvDet.DeleteRow(dgvDet.GetRowHandle(dgvDet.FocusedRowHandle));
                DTab_StockData.AcceptChanges();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToInt32(lblMode.Tag) > 0)
                {
                    ObjPer.SetFormPer();
                    if (ObjPer.AllowDelete == false)
                    {
                        Global.Message(BLL.GlobalDec.gStrPermissionDelMsg);
                        return;
                    }
                    btnDelete.Enabled = false;
                    int count = 0;
                    string type = "";
                    if ((DTab_StockData.Rows.Count == 1 && m_dtbLotMixSplit.Rows.Count == 1) || (DTab_StockData.Rows.Count > 1 && m_dtbLotMixSplit.Rows.Count == 1) || (DTab_StockData.Rows.Count == 1 && m_dtbLotMixSplit.Rows.Count > 1))
                    {
                        type = "NOTMANYTOMANY";
                    }
                    else
                    {
                        type = "MANYTOMANY";
                    }
                    count = objLotSplitReceive.CheckLottingMixing(Val.ToInt32(lblMode.Tag), type);
                    //if (count == 0)
                    //{
                    //    if (DeleteDetail())
                    //    {
                    //        ClearDetails();
                    //    }
                    //}
                    //else
                    //{
                    //    Global.Message("Lotting Mixing is not last Process");
                    //    btnDelete.Enabled = true;
                    //    return;
                    //}
                    //btnDelete.Enabled = true;
                    if (count == 0)
                    {
                        DialogResult result = MessageBox.Show("Do you want to Delete Lotting Mixing data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                        if (result != DialogResult.Yes)
                        {
                            btnDelete.Enabled = true;
                            return;
                        }
                        //if (DeleteDetail())
                        //{
                        //    ClearDetails();
                        //}
                    }
                    else
                    {
                        Global.Message("Lotting Mixing is not last Process");
                        btnDelete.Enabled = true;
                        return;
                    }

                    DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                    PanelLoading.Visible = true;
                    backgroundWorker_LotSplitDelete.RunWorkerAsync();
                    btnDelete.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
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
                DialogResult result = MessageBox.Show("Do you want to Delete Lotting Mixing data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result != DialogResult.Yes)
                {
                    blnReturn = false;
                    return blnReturn;
                }

                //objMFGPurchaseProperty.invoice_no = Val.ToString(lblMode.Tag);
                DataTable DeleteData = (DataTable)grdDet.DataSource;
                //int IntRes = objMFGLotSplit.Delete_LottingMixing(objMFGMixSplitProperty, DeleteData, DLL.GlobalDec.EnumTran.Continue, Conn);

                int IntCounter = 0;
                int Count_New = 0;
                int TotalCount = DeleteData.Rows.Count;

                int count = 0;
                foreach (DataRow drw in DeleteData.Rows)
                {
                    objMFGMixSplitProperty.lot_id = Val.ToInt64(drw["lot_id"]);
                    objMFGMixSplitProperty.pcs = Val.ToInt32(drw["pcs"]);
                    objMFGMixSplitProperty.carat = Val.ToDecimal(drw["carat"]);
                    objMFGMixSplitProperty.rough_cut_id = Val.ToInt64(drw["rough_cut_id"]);
                    objMFGMixSplitProperty.kapan_id = Val.ToInt64(drw["kapan_id"]);
                    objMFGMixSplitProperty.lotting_department_id = Val.ToInt64(drw["department_id"]);

                    if (DeleteData.Rows.Count == count + 1)
                    {
                        objMFGMixSplitProperty.count = count + 1;
                    }
                    else
                    {
                        objMFGMixSplitProperty.count = 0;
                    }

                    int IntRes = objMFGLotSplit.Delete_LottingMixing(objMFGMixSplitProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                    count = count + 1;

                    Count_New++;
                    IntCounter++;
                    SetControlPropertyValue(lblProgressCount, "Text", Count_New.ToString() + "" + "/" + "" + TotalCount.ToString() + " Completed....");
                }

                Conn.Inter1.Commit();
                PanelLoading.Visible = false;
                if (IntRes == -1)
                {
                    PanelLoading.Visible = false;
                    Global.Confirm("Error In Lotting Mixing");
                    IntRes = -1;
                    Conn.Inter1.Rollback();
                    Conn = null;
                    return blnReturn;
                    //txtPartyInvoiceNo.Focus();
                }
                else
                {
                    PanelLoading.Visible = false;
                    Global.Confirm("Lotting Mixing Data Delete Successfully");
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
                PanelLoading.Visible = false;
                IntRes = -1;
                Conn.Inter1.Rollback();
                Conn = null;
                General.ShowErrors(ex.ToString());
                blnReturn = false;
            }
            finally
            {
                PanelLoading.Visible = false;
                objMFGMixSplitProperty = null;
                btnDelete.Enabled = true;
            }

            return blnReturn;
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

        private void backgroundWorker_LotSplitDelete_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            MFGLotSplit objMFGLotSplit = new MFGLotSplit();
            MFGMixSplitProperty objMFGMixSplitProperty = new MFGMixSplitProperty();
            Conn = new BeginTranConnection(true, false);
            try
            {
                //if (Val.ToString(lblMode.Tag) != "")
                //{
                //DialogResult result = MessageBox.Show("Do you want to Delete Lotting Mixing data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                //if (result != DialogResult.Yes)
                //{
                //    blnReturn = false;
                //    return blnReturn;
                //}

                //objMFGPurchaseProperty.invoice_no = Val.ToString(lblMode.Tag);
                DataTable DeleteData = (DataTable)grdDet.DataSource;
                //int IntRes = objMFGLotSplit.Delete_LottingMixing(objMFGMixSplitProperty, DeleteData, DLL.GlobalDec.EnumTran.Continue, Conn);

                int IntCounter = 0;
                int Count_New = 0;
                int TotalCount = DeleteData.Rows.Count;
                New_IntRes = 0;

                int count = 0;
                foreach (DataRow drw in DeleteData.Rows)
                {
                    objMFGMixSplitProperty.lot_id = Val.ToInt64(drw["lot_id"]);
                    objMFGMixSplitProperty.pcs = Val.ToInt32(drw["pcs"]);
                    objMFGMixSplitProperty.carat = Val.ToDecimal(drw["carat"]);
                    objMFGMixSplitProperty.rough_cut_id = Val.ToInt64(drw["rough_cut_id"]);
                    objMFGMixSplitProperty.kapan_id = Val.ToInt64(drw["kapan_id"]);
                    objMFGMixSplitProperty.lotting_department_id = Val.ToInt64(drw["department_id"]);

                    if (DeleteData.Rows.Count == count + 1)
                    {
                        objMFGMixSplitProperty.count = count + 1;
                    }
                    else
                    {
                        objMFGMixSplitProperty.count = 0;
                    }

                    New_IntRes = objMFGLotSplit.Delete_LottingMixing(objMFGMixSplitProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                    count = count + 1;

                    Count_New++;
                    IntCounter++;
                    SetControlPropertyValue(lblProgressCount, "Text", Count_New.ToString() + "" + "/" + "" + TotalCount.ToString() + " Completed....");
                }

                Conn.Inter1.Commit();

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
            }
            finally
            {
                objMFGMixSplitProperty = null;
                btnDelete.Enabled = true;
            }
        }

        private void backgroundWorker_LotSplitDelete_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                PanelLoading.Visible = false;
                if (New_IntRes > 0)
                {
                    Global.Confirm("Lotting Mixing Data Delete Successfully");
                    ClearDetails();
                }
                else
                {
                    Global.Confirm("Error In Lotting Mixing");
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
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
