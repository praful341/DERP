using BLL;
using BLL.FunctionClasses.Master.MFG;
using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.PropertyClasses.Report;
using BLL.PropertyClasses.Transaction;
using BLL.PropertyClasses.Transaction.MFG;
using DERP.Class;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DERP.Transaction.MFG
{
    public partial class FrmMFGCostingManual : DevExpress.XtraEditors.XtraForm
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

        public New_Report_DetailProperty ObjReportDetailProperty;
        private List<Control> _tabControls = new List<Control>();
        Control _NextEnteredControl = new Control();
        FillCombo ObjFillCombo = new FillCombo();
        DataTable DtControlSettings;
        DataTable dtTemp;
        DataTable m_dtbParam;
        DataTable m_dtCut;
        DataTable m_DtProcess;
        DataTable m_dtbKapan;
        DataTable m_dtOutstanding;
        DataTable DtAssortment = new DataTable();
        DataTable DTabQuality = new DataTable();
        //DataTable DTab_KapanWiseData = new DataTable();

        decimal num_costing_amt = 0;

        Int64 m_numForm_id;
        Int64 IntRes;
        int m_IsLot;

        string StrListTempPurity = string.Empty;

        #endregion

        #region Constructor
        public FrmMFGCostingManual()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

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
            // AddKeyPressListener(this);
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
                btnSave.Enabled = false;

                //string Str = "";
                //if (Val.DBDate(BLL.GlobalDec.gStrServerDate) != Val.DBDate(dtpCostDate.Text))
                //{
                //    Str = GlobalDec.CheckLockIsOpenOrNot(Val.DBDate(dtpCostDate.Text), Val.DBTime(DateTime.Now.ToShortTimeString())); //Val.GetFullTime12());
                //    if (Str != "YES")
                //    {
                //        if (Str != "")
                //        {
                //            Global.Message(Str);
                //            btnSave.Enabled = true;
                //            return;
                //        }
                //        else
                //        {
                //            Global.Message("You Are Not Suppose to Make Entry On Different Date");
                //            btnSave.Enabled = true;
                //            return;
                //        }
                //    }
                //    else
                //    {
                //        dtpCostDate.Enabled = true;
                //        dtpCostDate.Visible = true;
                //    }
                //}

                if (!ValidateDetails())
                {
                    btnSave.Enabled = true;
                    return;
                }
                DialogResult result = MessageBox.Show("Do you want to save Costing Manual data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result != DialogResult.Yes)
                {
                    btnSave.Enabled = true;
                    return;
                }

                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                backgroundWorker_CostingManual.RunWorkerAsync();

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
        private void txtKapanCarat_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txtLabourRate_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txtPolishCarat_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txtPolishPer_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txtCosting_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txtAverage_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txtCostingAmt_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txtkapanPcs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtPolishPcs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtDiffPcs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtDiffPer_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txtRGhat_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txtBGhat_KeyPress(object sender, KeyPressEventArgs e)
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
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                RBtnType.Enabled = false;
                if (!ValidateDetails())
                {
                    return;
                }
                dtTemp = new DataTable();
                lblCostID.Text = "0";

                //DataTable dtnew = objAssortFirst.AssortFirstGetData(Val.ToString(lueColor.EditValue), Val.ToString(lueSieve.EditValue), Val.ToInt32(lueKapan.EditValue), Val.ToInt32(lueCutNo.EditValue), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue), Val.ToString(ListQuality.Text), Val.ToString(lueSieve.Text), Val.ToInt64(0), Val.ToInt64(RBtnType.EditValue));

            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
            }
        }
        private void lueCutNo_EditValueChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    MFGProcessReceive objProcessRecieve = new MFGProcessReceive();

            //    m_dtOutstanding = Global.GetStockCutwise(Val.ToInt(lueKapan.EditValue), Val.ToInt(lueCutNo.EditValue));

            //    if (m_dtOutstanding.Rows.Count > 0)
            //    {
            //        m_kapan_id = Val.ToInt(m_dtOutstanding.Rows[0]["kapan_id"]);
            //    }
            //    else
            //    {
            //        return;
            //    }

            //}
            //catch (Exception ex)
            //{
            //    BLL.General.ShowErrors(ex);
            //    return;
            //}
        }
        private void lueKapan_EditValueChanged(object sender, EventArgs e)
        {
            if (m_IsLot == 0)
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
                //lueCutNo.Properties.DataSource = m_dtbParam;
                //lueCutNo.Properties.ValueMember = "rough_cut_id";
                //lueCutNo.Properties.DisplayMember = "rough_cut_no";
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

                //m_dtCut = Global.GetRoughCutAll();
                //lueCutNo.Properties.DataSource = m_dtCut;
                //lueCutNo.Properties.ValueMember = "rough_cut_id";
                //lueCutNo.Properties.DisplayMember = "rough_cut_no";

                dtpCostDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpCostDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpCostDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpCostDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpCostDate.EditValue = DateTime.Now;

                DTabQuality = ObjFillCombo.FillCmb(FillCombo.TABLE.Quality_Master);
                DTabQuality.DefaultView.Sort = "quality_name";
                DTabQuality = DTabQuality.DefaultView.ToTable();

                // Add By Praful On 29072021

                //DTab_KapanWiseData = Global.GetRoughStockWise(Val.ToInt(0), Val.ToInt32(0));

                // End By Praful On 29072021

                m_dtbParam = Global.GetRoughCutAll();
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                //MFGCostingManual objMFGCostingManual = new MFGCostingManual();
                //MFGCostingManualProperty objMFGCostingManualProperty = new MFGCostingManualProperty();
                //objMFGCostingManualProperty.from_date = Val.DBDate(dtpCostDate.Text);
                //objMFGCostingManualProperty.to_date = Val.DBDate(dtpCostDate.Text);
                //objMFGCostingManualProperty.kapan_id = Val.ToInt64(lueKapan.EditValue);

                //if (RBtnType.EditValue.ToString() == "1")
                //{
                //    objMFGCostingManualProperty.location_id = Val.ToInt32(1);
                //}
                //else
                //{
                //    objMFGCostingManualProperty.location_id = Val.ToInt32(2);
                //}

                //DtAssortment = objMFGCostingManual.GetCostingManualStock(objMFGCostingManualProperty);

                FrmMFGCostingManualStk FrmMFGCostingManualStk = new FrmMFGCostingManualStk();
                FrmMFGCostingManualStk.FrmMFGCostingManual = this;
                FrmMFGCostingManualStk.DTab = DtAssortment;
                FrmMFGCostingManualStk.ShowForm(this, Val.ToInt32(RBtnType.EditValue));
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
            }
        }
        private void backgroundWorker_CostingManual_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                MFGCostingManual MFGCostingManual = new MFGCostingManual();
                MFGCostingManualProperty objMFGCostingManualProperty = new MFGCostingManualProperty();
                Conn = new BeginTranConnection(true, false);

                IntRes = 0;
                try
                {
                    // txtDiffPcs.Enabled = true;
                    objMFGCostingManualProperty.cost_id = Val.ToInt64(lblCostID.Text);
                    objMFGCostingManualProperty.cost_date = Val.DBDate(dtpCostDate.Text);
                    objMFGCostingManualProperty.kapan_id = Val.ToInt64(lueKapan.EditValue);
                    objMFGCostingManualProperty.rough_cut_no = Val.ToString(txtCutNo.Text);
                    objMFGCostingManualProperty.kapan_carat = Val.ToDecimal(txtKapanCarat.Text);
                    objMFGCostingManualProperty.rate = Val.ToDecimal(txtRate.Text);
                    objMFGCostingManualProperty.labour_rate = Val.ToDecimal(txtLabourRate.Text);
                    objMFGCostingManualProperty.polish_carat = Val.ToDecimal(txtPolishCarat.Text);
                    objMFGCostingManualProperty.polish_per = Val.ToDecimal(txtPolishPer.Text);
                    objMFGCostingManualProperty.costing = Val.ToDecimal(txtCosting.Text);
                    objMFGCostingManualProperty.average = Val.ToDecimal(txtAverage.Text);
                    objMFGCostingManualProperty.costing_amt = Val.ToDecimal(txtCostingAmt.Text);
                    objMFGCostingManualProperty.kapan_pcs = Val.ToInt32(txtkapanPcs.Text);
                    objMFGCostingManualProperty.polish_pcs = Val.ToInt32(txtPolishPcs.Text);
                    objMFGCostingManualProperty.diff_pcs = objMFGCostingManualProperty.kapan_pcs - objMFGCostingManualProperty.polish_pcs;
                    objMFGCostingManualProperty.diff_per = Val.ToDecimal(txtDiffPer.Text);
                    objMFGCostingManualProperty.r_ghat = Val.ToDecimal(txtRGhat.Text);
                    objMFGCostingManualProperty.b_ghat = Val.ToDecimal(txtBGhat.Text);

                    if (RBtnType.EditValue.ToString() == "1")
                    {
                        objMFGCostingManualProperty.location_id = Val.ToInt32(1);
                    }
                    else
                    {
                        objMFGCostingManualProperty.location_id = Val.ToInt32(2);
                    }


                    IntRes = MFGCostingManual.Save(objMFGCostingManualProperty);

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
        private void backgroundWorker_CostingManual_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (IntRes > 0)
                {
                    Global.Confirm("Costing Manual Data Save Succesfully");
                    ClearDetails();
                    btnSave.Enabled = true;
                }
                else
                {
                    Global.Confirm("Error In Costing Manual Data");
                    btnSave.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }
        private void txtPolishCarat_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                decimal numKapan_Carat = Val.ToDecimal(txtKapanCarat.Text);
                decimal numRate = Val.ToDecimal(txtRate.Text);
                decimal numPolish_Carat = Val.ToDecimal(txtPolishCarat.Text);
                num_costing_amt = Math.Round(Val.ToDecimal(numKapan_Carat * numRate), 0);

                //if (numPolish_Carat > 0)
                //{
                //    txtCosting.Text = Val.ToString(Math.Round(Val.ToDecimal((numKapan_Carat * numRate) / numPolish_Carat), 0));
                //}

                txtCostingAmt.Text = Val.ToString(Math.Round(Val.ToDecimal(numKapan_Carat * numRate), 0));

                if (numKapan_Carat > 0)
                    txtPolishPer.Text = Val.ToString(Math.Round(Val.ToDecimal((numPolish_Carat / numKapan_Carat) * 100), 2));
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }
        private void txtkapanPcs_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtDiffPcs.Text = Val.ToString(Val.ToDecimal(txtkapanPcs.Text) - Val.ToDecimal(txtPolishPcs.Text));
                if (Val.ToDecimal(txtkapanPcs.Text) > 0)
                    txtDiffPer.Text = Val.ToString(Math.Round(Val.ToDecimal(txtDiffPcs.Text) / Val.ToDecimal(txtkapanPcs.Text) * 100, 2));
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }
        private void txtPolishPcs_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                //decimal num_costing_amt = 0;
                int num_polish_pcs = 0;
                decimal num_labour_rate = 0;
                txtDiffPcs.Text = Val.ToString(Val.ToDecimal(txtkapanPcs.Text) - Val.ToDecimal(txtPolishPcs.Text));
                //num_costing_amt = Val.ToDecimal(txtCostingAmt.Text);
                num_polish_pcs = Val.ToInt32(txtPolishPcs.Text);
                num_labour_rate = Val.ToDecimal(txtLabourRate.Text);
                txtCostingAmt.Text = Val.ToString(Val.ToDecimal(num_costing_amt + (num_polish_pcs * num_labour_rate)));
                decimal numPolish_Carat = Val.ToDecimal(txtPolishCarat.Text);

                if (numPolish_Carat > 0)
                {
                    txtCosting.Text = Val.ToString(Math.Round((Val.ToDecimal(num_costing_amt + (num_polish_pcs * num_labour_rate)) / numPolish_Carat) / 100, 2));
                }

                if (Val.ToDecimal(txtkapanPcs.Text) > 0)
                    txtDiffPer.Text = Val.ToString(Math.Round(Val.ToDecimal(txtDiffPcs.Text) / Val.ToInt(txtkapanPcs.Text) * 100, 2));
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }
        private void txtRate_EditValueChanged(object sender, EventArgs e)
        {
            txtPolishCarat_EditValueChanged(null, null);
            txtPolishPcs_EditValueChanged(null, null);
        }
        private void txtKapanCarat_EditValueChanged(object sender, EventArgs e)
        {
            txtPolishCarat_EditValueChanged(null, null);
            txtPolishPcs_EditValueChanged(null, null);
        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (Val.ToInt(lblCostID.Text) != 0)
            {
                DialogResult result = MessageBox.Show("Do you want to Delete Costing Manual data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result != DialogResult.Yes)
                {
                    btnSave.Enabled = true;
                    return;
                }

                MFGCostingManual MFGCostingManual = new MFGCostingManual();
                MFGCostingManualProperty objMFGCostingManualProperty = new MFGCostingManualProperty();

                objMFGCostingManualProperty.cost_id = Val.ToInt64(lblCostID.Text);

                IntRes = MFGCostingManual.Delete(objMFGCostingManualProperty);

                if (IntRes > 0)
                {
                    Global.Confirm("Costing Manual Data Deleted Succesfully");
                    ClearDetails();
                    btnSave.Enabled = true;
                }
                else
                {
                    Global.Confirm("Error In Costing Manual Data");
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

        #endregion

        #region Functions
        private bool ClearDetails()
        {
            bool blnReturn = true;
            try
            {
                dtpCostDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpCostDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpCostDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpCostDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpCostDate.EditValue = DateTime.Now;

                lueKapan.EditValue = System.DBNull.Value;
                txtCutNo.Text = "";

                txtKapanCarat.Text = "0";
                txtLabourRate.Text = "0";
                txtRate.Text = "0";
                txtPolishCarat.Text = "0";
                txtPolishPcs.Text = "0";
                txtPolishPer.Text = "0";
                txtCosting.Text = "0";
                txtCostingAmt.Text = "0";
                txtAverage.Text = "0";
                txtkapanPcs.Text = "0";
                txtDiffPcs.Text = "0";
                txtDiffPer.Text = "0";
                txtBGhat.Text = "0";
                txtRGhat.Text = "0";
                lblCostID.Text = "0";
                //RBtnType.SelectedIndex = 0;
                RBtnType.Enabled = true;
                btnSave.Enabled = true;
                lueKapan.Focus();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
        }
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
                if (txtCutNo.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Cut No"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtCutNo.Focus();
                    }
                }
                if (txtKapanCarat.Text.ToString() == "" || txtKapanCarat.Text.ToString() == "0")
                {
                    lstError.Add(new ListError(12, "Kapan WT"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtKapanCarat.Focus();
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
                if (txtCutNo.Text == "")
                {
                    lstError.Add(new ListError(13, "Cut No"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtCutNo.Focus();
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
                DataTable DTab_Stk = Stock_Data.Copy();

                if (DTab_Stk.Rows.Count > 0)
                {
                    if (Val.ToInt32(DTab_Stk.Rows[0]["location_id"]) == 1)
                    {
                        RBtnType.SelectedIndex = 0;
                    }
                    else
                    {
                        RBtnType.SelectedIndex = 1;
                    }

                    lblCostID.Text = Val.ToString(DTab_Stk.Rows[0]["cost_id"]);
                    dtpCostDate.Text = Val.ToString(DTab_Stk.Rows[0]["cost_date"]);
                    lueKapan.EditValue = Val.ToInt64(DTab_Stk.Rows[0]["kapan_id"]);
                    txtCutNo.Text = Val.ToString(DTab_Stk.Rows[0]["rough_cut_no"]);
                    txtKapanCarat.Text = Val.ToString(DTab_Stk.Rows[0]["kapan_carat"]);
                    txtRate.Text = Val.ToString(DTab_Stk.Rows[0]["rate"]);
                    txtLabourRate.Text = Val.ToString(DTab_Stk.Rows[0]["labour_rate"]);
                    txtPolishCarat.Text = Val.ToString(DTab_Stk.Rows[0]["polish_carat"]);
                    txtPolishPer.Text = Val.ToString(DTab_Stk.Rows[0]["polish_per"]);
                    txtCosting.Text = Val.ToString(DTab_Stk.Rows[0]["costing"]);
                    txtAverage.Text = Val.ToString(DTab_Stk.Rows[0]["average"]);
                    txtCostingAmt.Text = Val.ToString(DTab_Stk.Rows[0]["costing_amt"]);
                    txtkapanPcs.Text = Val.ToString(DTab_Stk.Rows[0]["kapan_pcs"]);
                    txtPolishPcs.Text = Val.ToString(DTab_Stk.Rows[0]["polish_pcs"]);
                    txtDiffPcs.Text = Val.ToInt32(DTab_Stk.Rows[0]["diff_pcs"]).ToString();
                    txtDiffPer.Text = Val.ToString(DTab_Stk.Rows[0]["diff_per"]);
                    txtRGhat.Text = Val.ToString(DTab_Stk.Rows[0]["r_ghat"]);
                    txtBGhat.Text = Val.ToString(DTab_Stk.Rows[0]["b_ghat"]);
                    RBtnType.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
            }
        }

        #endregion
    }
}
