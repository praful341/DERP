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
    public partial class FrmMFGLottingRateUpdate : DevExpress.XtraEditors.XtraForm
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
        //DataTable DTab_KapanWiseData = new DataTable();
        int m_numForm_id = 0;
        Int64 IntRes;
        decimal m_numSummSarinRate = 0;
        decimal m_numSummRussianRate = 0;
        decimal m_numSummPolishRate = 0;
        decimal m_numSummSawableRate = 0;
        decimal m_numSummSawablePolishRate = 0;
        DataTable DTabDataPolish = new DataTable();
        #endregion

        #region Constructor
        public FrmMFGLottingRateUpdate()
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

        private void lueLottingDepartment_EditValueChanged(object sender, EventArgs e)
        {
            if (lueLottingDepartment.Text != "" && lueLottingDepartment.EditValue != DBNull.Value)
            {
                DataRow[] dr = m_dtbParam.Select("rough_cut_no ='" + Val.ToString(lueCutNo.Text) + "'");
                if (dr.Length > 0)
                {
                    DataTable dtIssOS = new DataTable();
                    //dtIssOS = objMFGLottingDepartment.Carat_GetData(Val.ToInt64(lueKapan.EditValue), Val.ToInt64(lueCutNo.EditValue), Val.ToString(lueLottingDepartment.Text));
                    dtIssOS = objMFGLottingDepartment.CaratWise_GetData(Val.ToInt64(lueKapan.EditValue), Val.ToInt64(lueCutNo.EditValue), Val.ToString(lueLottingDepartment.Text));

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
        private void lueCutNo_EditValueChanged(object sender, EventArgs e)
        {
            if (lueLottingDepartment.Text != "" && lueLottingDepartment.EditValue != DBNull.Value)
            {
                DataRow[] dr = m_dtbParam.Select("rough_cut_no ='" + Val.ToString(lueCutNo.Text) + "'");
                if (dr.Length > 0)
                {
                    DataTable dtIssOS = new DataTable();
                    //dtIssOS = objMFGLottingDepartment.Carat_GetData(Val.ToInt64(lueKapan.EditValue), Val.ToInt64(lueCutNo.EditValue), Val.ToString(lueLottingDepartment.Text));
                    dtIssOS = objMFGLottingDepartment.CaratWise_GetData(Val.ToInt64(lueKapan.EditValue), Val.ToInt64(lueCutNo.EditValue), Val.ToString(lueLottingDepartment.Text));

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
        private void FrmMFGLottingRateUpdate_Load(object sender, EventArgs e)
        {
            try
            {
                Global.LOOKUPDepartment_New(lueLottingDepartment);
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

                DataTable dtbLottingDepartment = (((DataTable)lueLottingDepartment.Properties.DataSource).Copy());

                if (dtbLottingDepartment.Select("department_id in(1004,2007,2009,2012,2017)").Length > 0)
                {
                    dtbLottingDepartment = dtbLottingDepartment.Select("department_id in(1004,2007,2009,2012,2017)").CopyToDataTable();

                    if (dtbLottingDepartment.Rows.Count > 0)
                    {
                        lueLottingDepartment.Properties.DataSource = dtbLottingDepartment;
                        lueLottingDepartment.Properties.ValueMember = "department_id";
                        lueLottingDepartment.Properties.DisplayMember = "department_name";
                    }
                }
                else
                {
                    lueLottingDepartment.Properties.DataSource = null;
                }
                // Add By Praful On 29072021

                //DTab_KapanWiseData = Global.GetRoughStockWise(Val.ToInt(0), Val.ToInt32(0));

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

                DialogResult result = MessageBox.Show("Do you want to save Lotting Rate Update data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result != DialogResult.Yes)
                {
                    btnSave.Enabled = true;
                    return;
                }

                PanelLoading.Visible = true;

                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                backgroundWorker_LottingRateUpdate.RunWorkerAsync();

                //PanelLoading.Visible = false;
                btnSave.Enabled = false;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        private void backgroundWorker_LottingRateUpdate_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                MFGLottingDepartment objMFGLottingDepartment = new MFGLottingDepartment();
                MFGLottingDepartmentProperty objMFGLottingDepartmentProperty = new MFGLottingDepartmentProperty();
                Conn = new BeginTranConnection(true, false);
                try
                {
                    IntRes = 0;

                    objMFGLottingDepartmentProperty.carat = Val.ToDecimal(txtCarat.Text);
                    objMFGLottingDepartmentProperty.main_rate = Val.ToDecimal(txtRate.Text);
                    objMFGLottingDepartmentProperty.amount = Val.ToDecimal(txtAmount.Text);

                    if (lueLottingDepartment.Text == "SARIN")
                    {
                        DataTable SarinEstimate_Data = (DataTable)grdSarinProcess.DataSource;
                        if (SarinEstimate_Data != null)
                        {
                            if (SarinEstimate_Data.Rows.Count > 0)
                            {
                                foreach (DataRow drw in SarinEstimate_Data.Rows)
                                {
                                    objMFGLottingDepartmentProperty.kapan_id = Val.ToInt64(lueKapan.EditValue);
                                    objMFGLottingDepartmentProperty.rough_cut_id = Val.ToInt64(lueCutNo.EditValue);
                                    objMFGLottingDepartmentProperty.lotting_department_name = Val.ToString(lueLottingDepartment.Text);
                                    objMFGLottingDepartmentProperty.type = Val.ToString(drw["type"]);
                                    objMFGLottingDepartmentProperty.rate = Val.ToDecimal(drw["rate"]);

                                    IntRes = objMFGLottingDepartment.Save_MFGLottingDepartment(objMFGLottingDepartmentProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                                }
                            }
                        }
                    }
                    else if (lueLottingDepartment.Text == "RUSSIAN")
                    {
                        DataTable RussianEstimate_Data = (DataTable)grdRussianProcess.DataSource;
                        if (RussianEstimate_Data != null)
                        {
                            if (RussianEstimate_Data.Rows.Count > 0)
                            {
                                foreach (DataRow drw in RussianEstimate_Data.Rows)
                                {
                                    objMFGLottingDepartmentProperty.kapan_id = Val.ToInt64(lueKapan.EditValue);
                                    objMFGLottingDepartmentProperty.rough_cut_id = Val.ToInt64(lueCutNo.EditValue);
                                    objMFGLottingDepartmentProperty.lotting_department_name = Val.ToString(lueLottingDepartment.Text);
                                    objMFGLottingDepartmentProperty.type = Val.ToString(drw["type"]);
                                    objMFGLottingDepartmentProperty.rate = Val.ToDecimal(drw["rate"]);

                                    IntRes = objMFGLottingDepartment.Save_MFGLottingDepartment(objMFGLottingDepartmentProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                                }
                            }
                        }
                    }
                    else if (lueLottingDepartment.Text == "POLISH")
                    {
                        DataTable PolishEstimate_Data = (DataTable)grdPolishProcess.DataSource;
                        if (PolishEstimate_Data != null)
                        {
                            if (PolishEstimate_Data.Rows.Count > 0)
                            {
                                objMFGLottingDepartmentProperty.kapan_id = Val.ToInt64(lueKapan.EditValue);
                                objMFGLottingDepartmentProperty.rough_cut_id = Val.ToInt64(lueCutNo.EditValue);
                                objMFGLottingDepartmentProperty.lotting_department_name = Val.ToString(lueLottingDepartment.Text);
                                objMFGLottingDepartmentProperty.type = Val.ToString("EX-2");
                                objMFGLottingDepartmentProperty.rate = Val.ToDecimal(txtEx2Rate.Text);

                                if (objMFGLottingDepartmentProperty.rate > 0)
                                {
                                    IntRes = objMFGLottingDepartment.Save_MFGLottingDepartment(objMFGLottingDepartmentProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                                }

                                foreach (DataRow drw in PolishEstimate_Data.Rows)
                                {
                                    objMFGLottingDepartmentProperty.kapan_id = Val.ToInt64(lueKapan.EditValue);
                                    objMFGLottingDepartmentProperty.rough_cut_id = Val.ToInt64(lueCutNo.EditValue);
                                    objMFGLottingDepartmentProperty.lotting_department_name = Val.ToString(lueLottingDepartment.Text);
                                    objMFGLottingDepartmentProperty.type = Val.ToString(drw["type"]);
                                    objMFGLottingDepartmentProperty.rate = Val.ToDecimal(drw["rate"]);

                                    IntRes = objMFGLottingDepartment.Save_MFGLottingDepartment(objMFGLottingDepartmentProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                                }
                            }
                        }
                    }
                    if (lueLottingDepartment.Text == "SAWABLE")
                    {
                        DataTable SawableEstimate_Data = (DataTable)grdSawableProcess.DataSource;
                        if (SawableEstimate_Data != null)
                        {
                            if (SawableEstimate_Data.Rows.Count > 0)
                            {
                                foreach (DataRow drw in SawableEstimate_Data.Rows)
                                {
                                    objMFGLottingDepartmentProperty.kapan_id = Val.ToInt64(lueKapan.EditValue);
                                    objMFGLottingDepartmentProperty.rough_cut_id = Val.ToInt64(lueCutNo.EditValue);
                                    objMFGLottingDepartmentProperty.lotting_department_name = Val.ToString(lueLottingDepartment.Text);
                                    objMFGLottingDepartmentProperty.type = Val.ToString(drw["type"]);
                                    objMFGLottingDepartmentProperty.rate = Val.ToDecimal(drw["rate"]);

                                    IntRes = objMFGLottingDepartment.Save_MFGLottingDepartment(objMFGLottingDepartmentProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                                }
                            }
                        }
                    }
                    if (lueLottingDepartment.Text == "SOWABALE POLISH")
                    {
                        DataTable SawablePolishEstimate_Data = (DataTable)grdSawablePolishProcess.DataSource;
                        if (SawablePolishEstimate_Data != null)
                        {
                            if (SawablePolishEstimate_Data.Rows.Count > 0)
                            {
                                foreach (DataRow drw in SawablePolishEstimate_Data.Rows)
                                {
                                    objMFGLottingDepartmentProperty.kapan_id = Val.ToInt64(lueKapan.EditValue);
                                    objMFGLottingDepartmentProperty.rough_cut_id = Val.ToInt64(lueCutNo.EditValue);
                                    objMFGLottingDepartmentProperty.lotting_department_name = Val.ToString(lueLottingDepartment.Text);
                                    objMFGLottingDepartmentProperty.type = Val.ToString(drw["type"]);
                                    objMFGLottingDepartmentProperty.rate = Val.ToDecimal(drw["rate"]);

                                    IntRes = objMFGLottingDepartment.Save_MFGLottingDepartment(objMFGLottingDepartmentProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                                }
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
        private void backgroundWorker_LottingRateUpdate_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                PanelLoading.Visible = false;
                if (IntRes > 0)
                {
                    Global.Confirm("Lotting Department Rate Data Save Succesfully");
                    btnSave.Enabled = true;
                    ClearDetails();
                }
                else
                {
                    Global.Confirm("Error In Lotting Department Rate Data");
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
            if (!ValidateDetails())
            {
                btnSave.Enabled = true;
                return;
            }

            MFGLottingDepartmentProperty objMFGLottingDepartmentProperty = new MFGLottingDepartmentProperty();
            objMFGLottingDepartmentProperty.kapan_id = Val.ToInt64(lueKapan.EditValue);
            objMFGLottingDepartmentProperty.rough_cut_id = Val.ToInt64(lueCutNo.EditValue);
            objMFGLottingDepartmentProperty.lotting_department_name = Val.ToString(lueLottingDepartment.Text);

            if (RBtnType.EditValue.ToString() == "M")
            {
                if (lueLottingDepartment.Text == "SARIN")
                {
                    DataTable DTabDataSarin = objMFGLottingDepartment.GetSarinData(objMFGLottingDepartmentProperty);
                    if (DTabDataSarin.Rows.Count > 0)
                    {
                        grdSarinProcess.DataSource = DTabDataSarin;
                        dgvSarinProcess.BestFitColumns();
                        grdSarinProcess.Focus();
                        grdRussianProcess.DataSource = null;
                        grdPolishProcess.DataSource = null;
                    }
                    else
                    {
                        Global.Message("Data Not Found");
                        grdSarinProcess.DataSource = null;
                        grdRussianProcess.DataSource = null;
                        grdPolishProcess.DataSource = null;
                    }

                }
                else if (lueLottingDepartment.Text == "RUSSIAN")
                {
                    DataTable DTabDataRussain = objMFGLottingDepartment.GetSarinData(objMFGLottingDepartmentProperty);

                    if (DTabDataRussain.Rows.Count > 0)
                    {
                        grdRussianProcess.DataSource = DTabDataRussain;
                        dgvRussianProcess.BestFitColumns();
                        grdRussianProcess.Focus();
                        grdSarinProcess.DataSource = null;
                        grdPolishProcess.DataSource = null;
                    }
                    else
                    {
                        Global.Message("Data Not Found");
                        grdSarinProcess.DataSource = null;
                        grdRussianProcess.DataSource = null;
                        grdPolishProcess.DataSource = null;
                    }
                }
                else if (lueLottingDepartment.Text == "POLISH")
                {
                    DTabDataPolish = new DataTable();
                    DTabDataPolish = objMFGLottingDepartment.GetSarinData(objMFGLottingDepartmentProperty);

                    if (DTabDataPolish.Rows.Count > 0)
                    {
                        txtEx2Carat.Text = Val.ToDecimal(DTabDataPolish.Rows[0]["carat"]).ToString();
                        decimal numEx2Amount = Val.ToDecimal(DTabDataPolish.Rows[0]["amount"]);
                        txtEx2Amount.Text = Val.ToString(numEx2Amount);

                        txtEx2Rate.Text = Val.ToDecimal(DTabDataPolish.Rows[0]["rate"]).ToString();


                        DTabDataPolish.Rows[0].Delete();
                        DTabDataPolish.AcceptChanges();

                        grdPolishProcess.DataSource = DTabDataPolish;
                        dgvPolishProcess.BestFitColumns();
                        //grdPolishProcess.Focus();
                        txtEx2Rate.Focus();
                        grdRussianProcess.DataSource = null;
                        grdSarinProcess.DataSource = null;
                    }
                    else
                    {
                        Global.Message("Data Not Found");
                        grdSarinProcess.DataSource = null;
                        grdRussianProcess.DataSource = null;
                        grdPolishProcess.DataSource = null;
                    }
                }
            }
            else if (RBtnType.EditValue.ToString() == "S")
            {
                if (lueLottingDepartment.Text == "SAWABLE")
                {
                    DataTable DTabDataSawable = objMFGLottingDepartment.GetSarinData(objMFGLottingDepartmentProperty);

                    if (DTabDataSawable.Rows.Count > 0)
                    {
                        grdSawableProcess.DataSource = DTabDataSawable;
                        dgvSawableProcess.BestFitColumns();
                        grdSawableProcess.Focus();
                        grdSawablePolishProcess.DataSource = null;
                    }
                    else
                    {
                        Global.Message("Data Not Found");
                        grdSawablePolishProcess.DataSource = null;
                        grdSawableProcess.DataSource = null;
                    }
                }
                else if (lueLottingDepartment.Text == "SOWABALE POLISH")
                {
                    DataTable DTabDataSawablePolish = objMFGLottingDepartment.GetSarinData(objMFGLottingDepartmentProperty);

                    if (DTabDataSawablePolish.Rows.Count > 0)
                    {
                        grdSawablePolishProcess.DataSource = DTabDataSawablePolish;
                        dgvSawablePolishProcess.BestFitColumns();
                        grdSawablePolishProcess.Focus();
                        grdSawableProcess.DataSource = null;
                    }
                    else
                    {
                        Global.Message("Data Not Found");
                        grdSawablePolishProcess.DataSource = null;
                        grdSawableProcess.DataSource = null;
                    }
                }
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

            int rowindex = dgvSarinProcess.FocusedRowHandle;
            int RowNumber = dgvSarinProcess.FocusedRowHandle;
            decimal Current_Rate = Val.ToDecimal(textEditor.EditValue);
            if (rowindex >= 1)
            {
                decimal Amount = Val.ToDecimal(txtAmount.Text);
                decimal Grid_Carat = Val.ToDecimal(dgvSarinProcess.GetRowCellValue(rowindex, "carat"));
                decimal Prev_Carat = Val.ToDecimal(dgvSarinProcess.GetRowCellValue(rowindex - 1, "carat"));

                decimal Grid_Amount = Val.ToDecimal(Current_Rate * Grid_Carat);
                decimal Diff_Amount = Amount - Grid_Amount;

                dgvSarinProcess.SetRowCellValue(rowindex - 1, "amount", Math.Round(Val.ToDecimal(Diff_Amount), 3));
                dgvSarinProcess.SetRowCellValue(rowindex - 1, "rate", Math.Round(Val.ToDecimal(Diff_Amount) / Val.ToDecimal(Prev_Carat), 3));
                dgvSarinProcess.SetRowCellValue(rowindex, "amount", Math.Round(Val.ToDecimal(Grid_Amount), 3));
            }
        }
        private void RepRussianRate_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit textEditor = (TextEdit)sender;

            int rowindex = dgvRussianProcess.FocusedRowHandle;
            int RowNumber = dgvRussianProcess.FocusedRowHandle;
            decimal Current_Rate = Val.ToDecimal(textEditor.EditValue);
            if (rowindex >= 1)
            {
                decimal Amount = Val.ToDecimal(txtAmount.Text);
                decimal Grid_Carat = Val.ToDecimal(dgvRussianProcess.GetRowCellValue(rowindex, "carat"));
                decimal Prev_Carat = Val.ToDecimal(dgvRussianProcess.GetRowCellValue(rowindex - 1, "carat"));

                decimal Grid_Amount = Val.ToDecimal(Current_Rate * Grid_Carat);
                decimal Diff_Amount = Amount - Grid_Amount;

                dgvRussianProcess.SetRowCellValue(rowindex - 1, "amount", Math.Round(Val.ToDecimal(Diff_Amount), 3));
                dgvRussianProcess.SetRowCellValue(rowindex - 1, "rate", Math.Round(Val.ToDecimal(Diff_Amount) / Val.ToDecimal(Prev_Carat), 3));
                dgvRussianProcess.SetRowCellValue(rowindex, "amount", Math.Round(Val.ToDecimal(Grid_Amount), 3));
            }
        }
        private void RepPolishRate_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit textEditor = (TextEdit)sender;

            int rowindex = dgvPolishProcess.FocusedRowHandle;
            int RowNumber = dgvPolishProcess.FocusedRowHandle;
            decimal Current_Rate = Val.ToDecimal(textEditor.EditValue);
            if (rowindex >= 1)
            {
                decimal Amount = Val.ToDecimal(txtAmount.Text);
                decimal Grid_Carat = Val.ToDecimal(dgvPolishProcess.GetRowCellValue(rowindex, "carat"));
                decimal Prev_Carat = Val.ToDecimal(dgvPolishProcess.GetRowCellValue(rowindex - 1, "carat"));
                decimal Ex2_Amount = Val.ToDecimal(txtEx2Amount.Text);

                decimal Grid_Amount = Val.ToDecimal(Current_Rate * Grid_Carat);
                decimal Diff_Amount = (Amount + Ex2_Amount) - Grid_Amount;
                //decimal Diff_Amount = Amount - Grid_Amount;

                dgvPolishProcess.SetRowCellValue(rowindex - 1, "amount", Math.Round(Val.ToDecimal(Diff_Amount), 3));
                dgvPolishProcess.SetRowCellValue(rowindex - 1, "rate", Math.Round(Val.ToDecimal(Diff_Amount) / Val.ToDecimal(Prev_Carat), 3));
                dgvPolishProcess.SetRowCellValue(rowindex, "amount", Math.Round(Val.ToDecimal(Grid_Amount), 3));
            }
            //else
            //{
            //    decimal Amount = Val.ToDecimal(txtAmount.Text);
            //    decimal Grid_Carat = Val.ToDecimal(dgvPolishProcess.GetRowCellValue(rowindex, "carat"));
            //    decimal Next_Amount = Val.ToDecimal(dgvPolishProcess.GetRowCellValue(rowindex + 1, "amount"));
            //    decimal Next_Carat = Val.ToDecimal(dgvPolishProcess.GetRowCellValue(rowindex + 1, "carat"));

            //    decimal Grid_Amount = Val.ToDecimal(Current_Rate * Grid_Carat);
            //    decimal Diff_Amount = Amount + Grid_Amount;
            //    //decimal Diff_Amount = Amount - Grid_Amount;

            //    dgvPolishProcess.SetRowCellValue(rowindex, "amount", Math.Round(Val.ToDecimal(Grid_Amount), 3));
            //    dgvPolishProcess.SetRowCellValue(rowindex + 1, "rate", Math.Round(Val.ToDecimal(Diff_Amount) / Val.ToDecimal(Next_Carat), 3));
            //    dgvPolishProcess.SetRowCellValue(rowindex + 1, "amount", Math.Round(Val.ToDecimal(Diff_Amount), 3));
            //}
        }
        private void RepSawableRate_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit textEditor = (TextEdit)sender;

            int rowindex = dgvSawableProcess.FocusedRowHandle;
            int RowNumber = dgvSawableProcess.FocusedRowHandle;
            decimal Current_Rate = Val.ToDecimal(textEditor.EditValue);
            if (rowindex >= 1)
            {
                decimal Amount = Val.ToDecimal(txtAmount.Text);
                decimal Grid_Carat = Val.ToDecimal(dgvSawableProcess.GetRowCellValue(rowindex, "carat"));
                decimal Prev_Carat = Val.ToDecimal(dgvSawableProcess.GetRowCellValue(rowindex - 1, "carat"));

                decimal Grid_Amount = Val.ToDecimal(Current_Rate * Grid_Carat);
                decimal Diff_Amount = Amount - Grid_Amount;

                dgvSawableProcess.SetRowCellValue(rowindex - 1, "amount", Math.Round(Val.ToDecimal(Diff_Amount), 3));
                dgvSawableProcess.SetRowCellValue(rowindex - 1, "rate", Math.Round(Val.ToDecimal(Diff_Amount) / Val.ToDecimal(Prev_Carat), 3));
                dgvSawableProcess.SetRowCellValue(rowindex, "amount", Math.Round(Val.ToDecimal(Grid_Amount), 3));
            }
        }
        private void RepSawablePolishRate_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit textEditor = (TextEdit)sender;

            int rowindex = dgvSawablePolishProcess.FocusedRowHandle;
            int RowNumber = dgvSawablePolishProcess.FocusedRowHandle;
            decimal Current_Rate = Val.ToDecimal(textEditor.EditValue);
            if (rowindex >= 1)
            {
                //decimal Amount = Val.ToDecimal(txtAmount.Text);
                //decimal Grid_Carat = Val.ToDecimal(dgvSawablePolishProcess.GetRowCellValue(rowindex, "carat"));
                //decimal Prev_Carat = Val.ToDecimal(dgvSawablePolishProcess.GetRowCellValue(rowindex - 1, "carat"));

                //decimal Grid_Amount = Val.ToDecimal(Current_Rate * Grid_Carat);
                //decimal Diff_Amount = Amount - Grid_Amount;

                //dgvSawablePolishProcess.SetRowCellValue(rowindex - 1, "amount", Math.Round(Val.ToDecimal(Diff_Amount), 3));
                //dgvSawablePolishProcess.SetRowCellValue(rowindex - 1, "rate", Math.Round(Val.ToDecimal(Diff_Amount) / Val.ToDecimal(Prev_Carat), 3));
                //dgvSawablePolishProcess.SetRowCellValue(rowindex, "amount", Math.Round(Val.ToDecimal(Grid_Amount), 3));
            }
            else
            {
                decimal Amount = Val.ToDecimal(txtAmount.Text);
                decimal Grid_Carat = Val.ToDecimal(dgvSawablePolishProcess.GetRowCellValue(rowindex, "carat"));
                decimal Next_Carat = Val.ToDecimal(dgvSawablePolishProcess.GetRowCellValue(rowindex + 1, "carat"));

                decimal Grid_Amount = Val.ToDecimal(Current_Rate * Grid_Carat);
                decimal Diff_Amount = Amount - Grid_Amount;

                dgvSawablePolishProcess.SetRowCellValue(rowindex, "amount", Math.Round(Val.ToDecimal(Grid_Amount), 3));
                dgvSawablePolishProcess.SetRowCellValue(rowindex + 1, "amount", Math.Round(Val.ToDecimal(Diff_Amount), 3));
                dgvSawablePolishProcess.SetRowCellValue(rowindex + 1, "rate", Math.Round(Val.ToDecimal(Diff_Amount) / Val.ToDecimal(Next_Carat), 3));
            }
        }
        private void txtEx2Rate_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtEx2Amount.Text = string.Format("{0:0.00}", Val.ToDecimal(txtEx2Carat.Text) * Val.ToDecimal(txtEx2Rate.Text));

                //DataTable dtb = (DataTable)grdPolishProcess.DataSource;

                //DTabDataPolish.Rows[0].Delete();
                //DTabDataPolish.AcceptChanges();

                if (DTabDataPolish.Rows.Count > 0)
                {
                    decimal Amount = Val.ToDecimal(txtAmount.Text) + Val.ToDecimal(txtEx2Amount.Text);
                    decimal Carat = Val.ToDecimal(DTabDataPolish.Rows[0]["carat"]);

                    DTabDataPolish.Rows[0]["amount"] = Amount;

                    if (Carat > 0)
                    {
                        DTabDataPolish.Rows[0]["rate"] = Math.Round(Val.ToDecimal(Amount / Carat), 2);
                    }
                    else
                    {
                        DTabDataPolish.Rows[0]["rate"] = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        private void RBtnType_EditValueChanged(object sender, EventArgs e)
        {
            Global.LOOKUPDepartment_New(lueLottingDepartment);
            DataTable dtbLottingDepartment = (((DataTable)lueLottingDepartment.Properties.DataSource).Copy());

            if (RBtnType.EditValue.ToString() == "M")
            {
                lblRussian.Text = "Russian";
                lblSarin.Text = "Sarin";
                lblPolish.Text = "Polish";
                grdSawablePolishProcess.DataSource = null;
                grdSawableProcess.DataSource = null;
                grdSarinProcess.DataSource = null;
                grdPolishProcess.DataSource = null;
                grdRussianProcess.DataSource = null;
                grdSawablePolishProcess.Visible = false;
                grdSawableProcess.Visible = false;
                grdSarinProcess.Visible = true;
                grdPolishProcess.Visible = true;
                grdRussianProcess.Visible = true;

                lblType.Visible = true;
                lblEx2.Visible = true;
                lblEx2Carat.Visible = true;
                lblEx2Rate.Visible = true;
                lblEx2Amount.Visible = true;
                txtEx2Carat.Visible = true;
                txtEx2Rate.Visible = true;
                txtEx2Amount.Visible = true;


                if (dtbLottingDepartment.Select("department_id in(1004,2007,2009)").Length > 0)
                {
                    dtbLottingDepartment = dtbLottingDepartment.Select("department_id in(1004,2007,2009)").CopyToDataTable();

                    if (dtbLottingDepartment.Rows.Count > 0)
                    {
                        lueLottingDepartment.Properties.DataSource = dtbLottingDepartment;
                        lueLottingDepartment.Properties.ValueMember = "department_id";
                        lueLottingDepartment.Properties.DisplayMember = "department_name";
                    }
                }
                else
                {
                    lueLottingDepartment.Properties.DataSource = null;
                }
            }
            else if (RBtnType.EditValue.ToString() == "S")
            {
                lblSarin.Text = "Sawable";
                lblRussian.Text = "Sawable Polish";
                lblPolish.Text = "";
                grdSawablePolishProcess.Visible = true;
                grdSawableProcess.Visible = true;
                grdSawablePolishProcess.DataSource = null;
                grdSawableProcess.DataSource = null;
                grdSarinProcess.DataSource = null;
                grdPolishProcess.DataSource = null;
                grdRussianProcess.DataSource = null;
                grdSarinProcess.Visible = false;
                grdPolishProcess.Visible = false;
                grdRussianProcess.Visible = false;

                lblType.Visible = false;
                lblEx2.Visible = false;
                lblEx2Carat.Visible = false;
                lblEx2Rate.Visible = false;
                lblEx2Amount.Visible = false;
                txtEx2Carat.Visible = false;
                txtEx2Rate.Visible = false;
                txtEx2Amount.Visible = false;

                if (dtbLottingDepartment.Select("department_id in(2012,2017)").Length > 0)
                {
                    dtbLottingDepartment = dtbLottingDepartment.Select("department_id in(2012,2017)").CopyToDataTable();

                    if (dtbLottingDepartment.Rows.Count > 0)
                    {
                        lueLottingDepartment.Properties.DataSource = dtbLottingDepartment;
                        lueLottingDepartment.Properties.ValueMember = "department_id";
                        lueLottingDepartment.Properties.DisplayMember = "department_name";
                    }
                }
                else
                {
                    lueLottingDepartment.Properties.DataSource = null;
                }
            }
        }

        #region Grid Event

        private void dgvSarinProcess_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            CalculateGridAmount_Sarin(dgvSarinProcess.FocusedRowHandle);
        }
        private void dgvSarinProcess_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            CalculateGridAmount_Sarin(e.PrevFocusedRowHandle);
        }
        private void dgvRussianProcess_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            CalculateGridAmount_Russian(dgvRussianProcess.FocusedRowHandle);
        }
        private void dgvRussianProcess_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            CalculateGridAmount_Russian(e.PrevFocusedRowHandle);
        }
        private void dgvPolishProcess_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            CalculateGridAmount_Polish(e.PrevFocusedRowHandle);
        }
        private void dgvPolishProcess_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            CalculateGridAmount_Polish(dgvPolishProcess.FocusedRowHandle);
        }
        private void dgvSawablePolishProcess_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            CalculateGridAmount_SawablePolish(dgvSawablePolishProcess.FocusedRowHandle);
        }
        private void dgvSawableProcess_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            CalculateGridAmount_Sawable(dgvSawableProcess.FocusedRowHandle);
        }
        private void dgvSawablePolishProcess_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            CalculateGridAmount_SawablePolish(e.PrevFocusedRowHandle);
        }
        private void dgvSawableProcess_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            CalculateGridAmount_Sawable(e.PrevFocusedRowHandle);
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
        private void dgvRussianProcess_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                if (Val.ToDecimal(ClmRussainAmount.SummaryItem.SummaryValue) > 0 && Val.ToDecimal(ClmRussainCarat.SummaryItem.SummaryValue) > 0)
                {
                    m_numSummRussianRate = Math.Round((Val.ToDecimal(ClmRussainAmount.SummaryItem.SummaryValue) / Val.ToDecimal(ClmRussainCarat.SummaryItem.SummaryValue)), 3, MidpointRounding.AwayFromZero);

                }
                else
                {
                    m_numSummRussianRate = 0;
                }
                if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == "rate")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                        e.TotalValue = m_numSummRussianRate;
                }

            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        private void dgvPolishProcess_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                if (Val.ToDecimal(clmPolishAmount.SummaryItem.SummaryValue) > 0 && Val.ToDecimal(ClmPolishCarat.SummaryItem.SummaryValue) > 0)
                {
                    m_numSummPolishRate = Math.Round((Val.ToDecimal(clmPolishAmount.SummaryItem.SummaryValue) / Val.ToDecimal(ClmPolishCarat.SummaryItem.SummaryValue)), 3, MidpointRounding.AwayFromZero);

                }
                else
                {
                    m_numSummPolishRate = 0;
                }
                if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == "rate")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                        e.TotalValue = m_numSummPolishRate;
                }

            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        private void dgvSawablePolishProcess_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                if (Val.ToDecimal(clmSawablePolishAmount.SummaryItem.SummaryValue) > 0 && Val.ToDecimal(clmSawablePolishCarat.SummaryItem.SummaryValue) > 0)
                {
                    m_numSummSawablePolishRate = Math.Round((Val.ToDecimal(clmSawablePolishAmount.SummaryItem.SummaryValue) / Val.ToDecimal(clmSawablePolishCarat.SummaryItem.SummaryValue)), 3, MidpointRounding.AwayFromZero);
                }
                else
                {
                    m_numSummSawablePolishRate = 0;
                }
                if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == "rate")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                        e.TotalValue = m_numSummSawablePolishRate;
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        private void dgvSawableProcess_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                if (Val.ToDecimal(clmSawableAmount.SummaryItem.SummaryValue) > 0 && Val.ToDecimal(clmSawableCarat.SummaryItem.SummaryValue) > 0)
                {
                    m_numSummSawableRate = Math.Round((Val.ToDecimal(clmSawableAmount.SummaryItem.SummaryValue) / Val.ToDecimal(clmSawableCarat.SummaryItem.SummaryValue)), 3, MidpointRounding.AwayFromZero);

                }
                else
                {
                    m_numSummSawableRate = 0;
                }
                if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == "rate")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                        e.TotalValue = m_numSummSawableRate;
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        private void dgvRussianProcess_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dgvRussianProcess.FocusedColumn.Caption == "Rate")
            {
                if (dgvRussianProcess.IsLastRow)
                {
                    btnSave_Click(null, null);
                }
            }
        }
        private void dgvPolishProcess_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dgvPolishProcess.FocusedColumn.Caption == "Rate")
            {
                if (dgvPolishProcess.IsLastRow)
                {
                    btnSave_Click(null, null);
                }
            }
        }
        private void dgvSarinProcess_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dgvPolishProcess.FocusedColumn.Caption == "Rate")
            {
                if (dgvPolishProcess.IsLastRow)
                {
                    btnSave_Click(null, null);
                }
            }
        }
        private void dgvSawablePolishProcess_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dgvSawablePolishProcess.FocusedColumn.Caption == "Rate")
            {
                if (dgvSawablePolishProcess.IsLastRow)
                {
                    btnSave_Click(null, null);
                }
            }
        }
        private void dgvSawableProcess_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dgvSawableProcess.FocusedColumn.Caption == "Rate")
            {
                if (dgvSawableProcess.IsLastRow)
                {
                    btnSave_Click(null, null);
                }
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
                if (Val.ToString(lueLottingDepartment.Text) == "")
                {
                    lstError.Add(new ListError(13, "Lotting Department"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueLottingDepartment.Focus();
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

                lueLottingDepartment.EditValue = System.DBNull.Value;
                //lueKapan.EditValue = System.DBNull.Value;
                //lueCutNo.EditValue = System.DBNull.Value;

                txtCarat.Text = string.Empty;
                txtRate.Text = string.Empty;
                txtAmount.Text = string.Empty;
                grdRussianProcess.DataSource = null;
                grdSarinProcess.DataSource = null;
                grdPolishProcess.DataSource = null;
                grdSawableProcess.DataSource = null;
                grdSawablePolishProcess.DataSource = null;

                txtEx2Carat.Text = string.Empty;
                txtEx2Rate.Text = string.Empty;
                txtEx2Amount.Text = string.Empty;

                //RBtnType.SelectedIndex = 0;
                RBtnType_EditValueChanged(null, null);
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
                dgvSarinProcess.SetRowCellValue(rowindex, "amount", Math.Round((Val.ToDouble(dgvSarinProcess.GetRowCellValue(rowindex, "rate")) * Val.ToDouble(dgvSarinProcess.GetRowCellValue(rowindex, "carat"))), 2));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void CalculateGridAmount_Polish(int rowindex)
        {
            try
            {
                dgvPolishProcess.SetRowCellValue(rowindex, "amount", Math.Round((Val.ToDouble(dgvPolishProcess.GetRowCellValue(rowindex, "rate")) * Val.ToDouble(dgvPolishProcess.GetRowCellValue(rowindex, "carat"))), 2));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void CalculateGridAmount_Russian(int rowindex)
        {
            try
            {
                dgvRussianProcess.SetRowCellValue(rowindex, "amount", Math.Round((Val.ToDouble(dgvRussianProcess.GetRowCellValue(rowindex, "rate")) * Val.ToDouble(dgvRussianProcess.GetRowCellValue(rowindex, "carat"))), 2));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void CalculateGridAmount_SawablePolish(int rowindex)
        {
            try
            {
                dgvSawablePolishProcess.SetRowCellValue(rowindex, "amount", Math.Round((Val.ToDouble(dgvSawablePolishProcess.GetRowCellValue(rowindex, "rate")) * Val.ToDouble(dgvSawablePolishProcess.GetRowCellValue(rowindex, "carat"))), 2));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void CalculateGridAmount_Sawable(int rowindex)
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

        private void txtEx2Rate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dgvPolishProcess.BestFitColumns();
                grdPolishProcess.Focus();
                panelControl11.Focus();
                grdPolishProcess.Focus();
            }
        }
    }
}
