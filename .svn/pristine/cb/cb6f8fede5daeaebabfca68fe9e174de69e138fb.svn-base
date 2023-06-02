using BLL;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.PropertyClasses.Transaction;
using BLL.PropertyClasses.Transaction.MFG;
using DERP.Class;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DERP.Transaction.MFG
{
    public partial class FrmMFGTRNEstimation : DevExpress.XtraEditors.XtraForm
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
        ProcessMaster objProcessMaster = new ProcessMaster();
        DataTable process = new DataTable();
        DataTable DTab_StockData = new DataTable();
        //DataTable DTab_KapanWiseData = new DataTable();
        int m_numForm_id = 0;
        Int64 IntRes;
        #endregion

        #region Constructor
        public FrmMFGTRNEstimation()
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

        private void dgvSarinProcessGrp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dgvSarinProcessGrp.FocusedColumn.Caption == "WT")
            {
                if (dgvSarinProcessGrp.IsLastRow)
                {
                    btnSave_Click(null, null);
                }
            }
        }

        private void dgvRussianProcessGrp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dgvRussianProcessGrp.FocusedColumn.Caption == "WT")
            {
                if (dgvRussianProcessGrp.IsLastRow)
                {
                    btnSave_Click(null, null);
                }
            }
        }

        private void dgvPolishProcessGrp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dgvPolishProcessGrp.FocusedColumn.Caption == "WT")
            {
                if (dgvPolishProcessGrp.IsLastRow)
                {
                    btnSave_Click(null, null);
                }
            }
        }

        private void FrmMFGTRNEstimation_Load(object sender, EventArgs e)
        {
            try
            {
                Global.LOOKUPProcess(lueProcessSarin);
                Global.LOOKUPProcess(lueProcessRussian);
                Global.LOOKUPProcess(lueProcessPolish);

                dtpEntryDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpEntryDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpEntryDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpEntryDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpEntryDate.EditValue = DateTime.Now;

                DtpSarinDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                DtpSarinDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                DtpSarinDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                DtpSarinDate.Properties.CharacterCasing = CharacterCasing.Upper;
                DtpSarinDate.EditValue = DateTime.Now;

                DtpRussianDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                DtpRussianDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                DtpRussianDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                DtpRussianDate.Properties.CharacterCasing = CharacterCasing.Upper;
                DtpRussianDate.EditValue = DateTime.Now;

                DtpPolishDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                DtpPolishDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                DtpPolishDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                DtpPolishDate.Properties.CharacterCasing = CharacterCasing.Upper;
                DtpPolishDate.EditValue = DateTime.Now;

                m_dtbKapan = Global.GetKapanAll();

                lueKapan.Properties.DataSource = m_dtbKapan;
                lueKapan.Properties.ValueMember = "kapan_id";
                lueKapan.Properties.DisplayMember = "kapan_no";

                m_dtbParam = Global.GetRoughCutAll();

                lueCutNo.Properties.DataSource = m_dtbParam;
                lueCutNo.Properties.ValueMember = "rough_cut_id";
                lueCutNo.Properties.DisplayMember = "rough_cut_no";

                // Add By Praful On 29072021

                //DTab_KapanWiseData = Global.GetRoughStockWise(Val.ToInt(0), Val.ToInt32(0));

                // End By Praful On 29072021

                ClearDetails();

                process = objProcessMaster.GetData(1);
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
                int isExist = objMFGTRNEstimation.CheckEstExist(Val.ToInt64(txtLotID.Text));
                if (isExist == 1004 && GlobalDec.gEmployeeProperty.department_id == 2013)
                {
                    Global.Message("Estimation Already Done");
                    btnSave.Enabled = true;
                    return;
                }
                else if (isExist == 2013 && GlobalDec.gEmployeeProperty.department_id == 1004)
                {
                    Global.Message("Estimation Already Done");
                    btnSave.Enabled = true;
                    return;
                }
                else if (isExist == 1004 && GlobalDec.gEmployeeProperty.department_id == 1003 && lueProcessSarin.Text == "SARIN")
                {
                    Global.Message("Estimation Already Done");
                    btnSave.Enabled = true;
                    return;
                }
                else if (isExist == 1003 && GlobalDec.gEmployeeProperty.department_id == 1004 && lueProcessSarin.Text == "SARIN")
                {
                    Global.Message("Estimation Already Done");
                    btnSave.Enabled = true;
                    return;
                }
                else if (isExist == 1004 && GlobalDec.gEmployeeProperty.branch_id == 9 && GlobalDec.gEmployeeProperty.department_id == 1004)
                {
                    Global.Message("Estimation Already Done");
                    btnSave.Enabled = true;
                    return;
                }
                else if (isExist == 1004 && GlobalDec.gEmployeeProperty.branch_id == 14 && GlobalDec.gEmployeeProperty.department_id == 1004)
                {
                    Global.Message("Estimation Already Done");
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
                backgroundWorker_Estimation.RunWorkerAsync();

                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }

        private void backgroundWorker_Estimation_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
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
                    int previous_pcs = 0;
                    decimal previous_carat = 0;

                    DataTable SarinEstimate_Data = (DataTable)grdSarinProcessGrp.DataSource;
                    if (SarinEstimate_Data != null)
                    {
                        if (SarinEstimate_Data.Rows.Count > 0)
                        {

                            foreach (DataRow drw in SarinEstimate_Data.Rows)
                            {
                                //if (Val.ToDecimal(drw["carat"]) != 0 && Val.ToString(drw["sub_process_name"]) != "")
                                if (Val.ToString(drw["sub_process_name"]) != "")
                                {
                                    if (count == 0)
                                    {
                                        objMFGTRNEstimationProperty.previous_pcs = Val.ToInt(txtPcs.Text);
                                        objMFGTRNEstimationProperty.previous_carat = Val.ToDecimal(txtCarat.Text);
                                    }
                                    else
                                    {
                                        objMFGTRNEstimationProperty.previous_pcs = 0;
                                        objMFGTRNEstimationProperty.previous_carat = previous_carat;
                                    }
                                    objMFGTRNEstimationProperty.org_pcs = Val.ToInt(txtPcs.Text);
                                    objMFGTRNEstimationProperty.org_carat = Val.ToDecimal(txtCarat.Text);
                                    objMFGTRNEstimationProperty.carat = Val.ToDecimal(drw["carat"]);
                                    objMFGTRNEstimationProperty.lot_id = Val.ToInt64(txtLotID.Text);
                                    objMFGTRNEstimationProperty.estimation_date = Val.DBDate(dtpEntryDate.Text);
                                    objMFGTRNEstimationProperty.sarin_date = Val.DBDate(DtpSarinDate.Text);
                                    objMFGTRNEstimationProperty.flag = 0;

                                    previous_carat = Val.ToDecimal(drw["carat"]);

                                    objMFGTRNEstimationProperty.form_id = Val.ToInt(m_numForm_id);
                                    objMFGTRNEstimationProperty.rough_cut_id = Val.ToInt64(lueCutNo.EditValue);
                                    objMFGTRNEstimationProperty.kapan_id = Val.ToInt64(lueKapan.EditValue);
                                    objMFGTRNEstimationProperty.process_id = Val.ToInt(lueProcessSarin.EditValue);
                                    objMFGTRNEstimationProperty.sub_process_id = Val.ToInt64(drw["sub_process_id"]);
                                    objMFGTRNEstimationProperty.average_per = Val.ToDecimal(drw["average_per"]);

                                    IntRes = objMFGTRNEstimation.Save_MFGTRNEstimation(objMFGTRNEstimationProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                                    count = count + 1;
                                }
                            }
                        }
                    }

                    DataTable RussianEstimate_Data = (DataTable)grdRussianProcessGrp.DataSource;
                    count = 0;
                    previous_pcs = 0;
                    previous_carat = 0;

                    if (RussianEstimate_Data != null)
                    {
                        if (RussianEstimate_Data.Rows.Count > 0)
                        {
                            foreach (DataRow drw in RussianEstimate_Data.Rows)
                            {
                                //if (Val.ToDecimal(drw["carat"]) != 0 && Val.ToString(drw["sub_process_name"]) != "")
                                //{
                                if (Val.ToString(drw["sub_process_name"]) != "")
                                {
                                    if (count == 0)
                                    {
                                        objMFGTRNEstimationProperty.previous_pcs = Val.ToInt(txtPcs.Text);
                                        objMFGTRNEstimationProperty.previous_carat = Val.ToDecimal(txtCarat.Text);
                                    }
                                    else
                                    {
                                        objMFGTRNEstimationProperty.previous_pcs = previous_pcs;
                                        objMFGTRNEstimationProperty.previous_carat = previous_carat;
                                    }
                                    objMFGTRNEstimationProperty.org_pcs = Val.ToInt(txtPcs.Text);
                                    objMFGTRNEstimationProperty.org_carat = Val.ToDecimal(txtCarat.Text);
                                    objMFGTRNEstimationProperty.pcs = Val.ToInt(drw["pcs"]);
                                    objMFGTRNEstimationProperty.carat = Val.ToDecimal(drw["carat"]);
                                    objMFGTRNEstimationProperty.lot_id = Val.ToInt64(txtLotID.Text);
                                    objMFGTRNEstimationProperty.estimation_date = Val.DBDate(dtpEntryDate.Text);
                                    objMFGTRNEstimationProperty.russian_date = Val.DBDate(DtpRussianDate.Text);
                                    objMFGTRNEstimationProperty.flag = 1;
                                    previous_pcs = Val.ToInt(drw["pcs"]);
                                    previous_carat = Val.ToDecimal(drw["carat"]);
                                    objMFGTRNEstimationProperty.form_id = Val.ToInt(m_numForm_id);
                                    objMFGTRNEstimationProperty.rough_cut_id = Val.ToInt64(lueCutNo.EditValue);
                                    objMFGTRNEstimationProperty.kapan_id = Val.ToInt64(lueKapan.EditValue);
                                    objMFGTRNEstimationProperty.process_id = Val.ToInt(lueProcessRussian.EditValue);
                                    objMFGTRNEstimationProperty.sub_process_id = Val.ToInt64(drw["sub_process_id"]);
                                    objMFGTRNEstimationProperty.average_per = Val.ToDecimal(drw["average_per"]);
                                    objMFGTRNEstimationProperty.rate = Val.ToDecimal(drw["rate"]);

                                    IntRes = objMFGTRNEstimation.Save_MFGTRNEstimation(objMFGTRNEstimationProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                                    count = count + 1;
                                }
                            }
                        }
                    }

                    DataTable PolishEstimate_Data = (DataTable)grdPolishProcessGrp.DataSource;
                    count = 0;
                    previous_pcs = 0;
                    previous_carat = 0;

                    if (PolishEstimate_Data != null)
                    {
                        if (PolishEstimate_Data.Rows.Count > 0)
                        {
                            foreach (DataRow drw in PolishEstimate_Data.Rows)
                            {
                                // if (Val.ToDecimal(drw["carat"]) != 0 && Val.ToString(drw["sub_process_name"]) != "")
                                if (Val.ToString(drw["sub_process_name"]) != "")
                                {
                                    if (count == 0)
                                    {
                                        objMFGTRNEstimationProperty.previous_pcs = Val.ToInt(txtPcs.Text);
                                        objMFGTRNEstimationProperty.previous_carat = Val.ToDecimal(txtCarat.Text);
                                    }
                                    else
                                    {
                                        objMFGTRNEstimationProperty.previous_pcs = previous_pcs;
                                        objMFGTRNEstimationProperty.previous_carat = previous_carat;
                                    }
                                    objMFGTRNEstimationProperty.org_pcs = Val.ToInt(txtPcs.Text);
                                    objMFGTRNEstimationProperty.org_carat = Val.ToDecimal(txtCarat.Text);
                                    objMFGTRNEstimationProperty.pcs = Val.ToInt(drw["pcs"]);
                                    objMFGTRNEstimationProperty.carat = Val.ToDecimal(drw["carat"]);
                                    objMFGTRNEstimationProperty.lot_id = Val.ToInt64(txtLotID.Text);
                                    objMFGTRNEstimationProperty.estimation_date = Val.DBDate(dtpEntryDate.Text);
                                    objMFGTRNEstimationProperty.polish_date = Val.DBDate(DtpPolishDate.Text);
                                    objMFGTRNEstimationProperty.flag = 2;
                                    previous_pcs = Val.ToInt(drw["pcs"]);
                                    previous_carat = Val.ToDecimal(drw["carat"]);

                                    objMFGTRNEstimationProperty.form_id = Val.ToInt(m_numForm_id);
                                    objMFGTRNEstimationProperty.rough_cut_id = Val.ToInt64(lueCutNo.EditValue);
                                    objMFGTRNEstimationProperty.kapan_id = Val.ToInt64(lueKapan.EditValue);
                                    objMFGTRNEstimationProperty.process_id = Val.ToInt(lueProcessPolish.EditValue);
                                    objMFGTRNEstimationProperty.sub_process_id = Val.ToInt64(drw["sub_process_id"]);
                                    objMFGTRNEstimationProperty.average_per = Val.ToDecimal(drw["average_per"]);
                                    objMFGTRNEstimationProperty.rate = Val.ToDecimal(drw["rate"]);

                                    IntRes = objMFGTRNEstimation.Save_MFGTRNEstimation(objMFGTRNEstimationProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                                    count = count + 1;
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
        private void backgroundWorker_Estimation_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (IntRes > 0)
                {
                    Global.Confirm("Estimation Data Save Succesfully");
                    btnSave.Enabled = true;
                    ClearDetails();
                }
                else
                {
                    Global.Confirm("Error In Estimation Data");
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
        private void RepWeight_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit textEditor = (TextEdit)sender;

            int rowindex = dgvSarinProcessGrp.FocusedRowHandle;
            int RowNumber = dgvSarinProcessGrp.FocusedRowHandle;
            decimal Current_Carat = Val.ToDecimal(textEditor.EditValue);
            if (rowindex == 1)
            {
                decimal Previous_Carat = Val.ToDecimal(dgvSarinProcessGrp.GetRowCellValue(rowindex - 1, "carat"));
                dgvSarinProcessGrp.SetRowCellValue(rowindex, "average_per", Math.Round(Val.ToDecimal((Current_Carat / Previous_Carat) * 100), 2));
            }
            if (rowindex == 2)
            {
                decimal Previous_Carat = Val.ToDecimal(dgvSarinProcessGrp.GetRowCellValue(rowindex - 2, "carat"));
                dgvSarinProcessGrp.SetRowCellValue(rowindex, "average_per", Math.Round(Val.ToDecimal((Current_Carat / Previous_Carat) * 100), 2));
            }
            else
            {
                dgvSarinProcessGrp.SetRowCellValue(rowindex, "average_per", Math.Round(Val.ToDecimal(Current_Carat) / Val.ToDecimal(txtCarat.Text) * 100, 2));
            }
        }
        private void RepPolWeight_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit textEditor = (TextEdit)sender;

            int rowindex = dgvPolishProcessGrp.FocusedRowHandle;
            int RowNumber = dgvPolishProcessGrp.FocusedRowHandle;
            decimal Current_Carat = Val.ToDecimal(textEditor.EditValue);
            if (rowindex >= 1)
            {
                decimal Previous_Carat = Val.ToDecimal(dgvPolishProcessGrp.GetRowCellValue(rowindex - 1, "carat"));
                if (Previous_Carat > 0)
                {
                    dgvPolishProcessGrp.SetRowCellValue(rowindex, "average_per", Math.Round(Val.ToDecimal((Current_Carat / Previous_Carat) * 100), 2));
                }
                else
                {
                    dgvPolishProcessGrp.SetRowCellValue(rowindex, "average_per", 0);
                }
                dgvPolishProcessGrp.SetRowCellValue(rowindex, "total", Math.Round(Val.ToDouble(dgvPolishProcessGrp.GetRowCellValue(rowindex, "rate")) * Val.ToDouble(dgvPolishProcessGrp.GetRowCellValue(rowindex, "pcs")), 2));
            }
            else
            {
                dgvPolishProcessGrp.SetRowCellValue(rowindex, "average_per", Math.Round(Val.ToDecimal(Current_Carat) / Val.ToDecimal(txtCarat.Text) * 100, 2));
                dgvPolishProcessGrp.SetRowCellValue(rowindex, "total", Math.Round(Val.ToDouble(dgvPolishProcessGrp.GetRowCellValue(rowindex, "rate")) * Val.ToDouble(dgvPolishProcessGrp.GetRowCellValue(rowindex, "pcs")), 2));
            }
        }
        private void RepRussianWeight_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit textEditor = (TextEdit)sender;

            int rowindex = dgvRussianProcessGrp.FocusedRowHandle;
            int RowNumber = dgvRussianProcessGrp.FocusedRowHandle;
            decimal Current_Carat = Val.ToDecimal(textEditor.EditValue);
            if (rowindex >= 1)
            {
                decimal Previous_Carat = Val.ToDecimal(dgvRussianProcessGrp.GetRowCellValue(rowindex - 1, "carat"));
                if (Previous_Carat > 0)
                {
                    dgvRussianProcessGrp.SetRowCellValue(rowindex, "average_per", Math.Round(Val.ToDecimal((Current_Carat / Previous_Carat) * 100), 2));
                }
                else
                {
                    dgvRussianProcessGrp.SetRowCellValue(rowindex, "average_per", 0);
                }
                dgvRussianProcessGrp.SetRowCellValue(rowindex, "total", Math.Round(Val.ToDouble(dgvRussianProcessGrp.GetRowCellValue(rowindex, "rate")) * Val.ToDouble(dgvRussianProcessGrp.GetRowCellValue(rowindex, "pcs")), 2));
            }
            else
            {
                dgvRussianProcessGrp.SetRowCellValue(rowindex, "average_per", Math.Round(Val.ToDecimal(Current_Carat) / Val.ToDecimal(txtCarat.Text) * 100, 2));
                dgvRussianProcessGrp.SetRowCellValue(rowindex, "total", Math.Round(Val.ToDouble(dgvRussianProcessGrp.GetRowCellValue(rowindex, "rate")) * Val.ToDouble(dgvRussianProcessGrp.GetRowCellValue(rowindex, "pcs")), 2));
            }
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
                //objMFGProcessIssueProperty.rough_cut_id = Val.ToInt(lueCutNo.EditValue);
                //objMFGProcessIssueProperty.kapan_id = Val.ToInt(lueKapan.EditValue);
                objMFGProcessIssueProperty.flag = Val.ToInt(1);

                DTab_StockData = objMFGProcessIssue.GetPendingDeptStock(Val.ToInt64(txtLotID.Text), objMFGProcessIssueProperty);

                if (DTab_StockData.Rows.Count > 0)
                {

                    txtPcs.Text = Val.ToInt32(DTab_StockData.Rows[0]["org_pcs"]).ToString();
                    txtCarat.Text = Val.ToDecimal(DTab_StockData.Rows[0]["org_carat"]).ToString();
                    //if (GlobalDec.gEmployeeProperty.role_name == "SURAT MAKABLE")
                    //{
                    //    txtCarat.Text = Val.ToDecimal(DTab_StockData.Rows[0]["org_carat"]).ToString();
                    //}
                    //else
                    //{
                    //    txtCarat.Text = Val.ToDecimal(DTab_StockData.Rows[0]["carat"]).ToString();
                    //}
                    lueKapan.EditValue = Val.ToInt64(DTab_StockData.Rows[0]["kapan_id"]);
                    lueCutNo.EditValue = Val.ToInt64(DTab_StockData.Rows[0]["rough_cut_id"]);

                    if (ChkSarin.Checked == true)
                    {
                        panelControl4.Enabled = true;
                        panelControl7.Enabled = false;
                        panelControl10.Enabled = false;

                        DataTable DTab_GetProcessSarin = process.Select("process_name ='SARIN'").CopyToDataTable();

                        lueProcessSarin.EditValue = Val.ToInt32(DTab_GetProcessSarin.Rows[0]["process_id"]);

                        objMFGTRNEstimationProperty.entry_date = Val.DBDate(dtpEntryDate.Text);
                        objMFGTRNEstimationProperty.process_id = Val.ToInt32(lueProcessSarin.EditValue);
                        objMFGTRNEstimationProperty.lot_id = Val.ToInt32(txtLotID.EditValue);
                        objMFGTRNEstimationProperty.kapan_id = Val.ToInt32(lueKapan.EditValue);
                        objMFGTRNEstimationProperty.rough_cut_id = Val.ToInt32(lueCutNo.EditValue);

                        DataTable DTabDataSarin = objMFGTRNEstimation.GetSarinData(objMFGTRNEstimationProperty);

                        if (DTabDataSarin.Rows.Count > 0)
                        {
                            for (int i = DTabDataSarin.Rows.Count - 1; i >= 0; i--)
                            {
                                DataRow dr = DTabDataSarin.Rows[i];
                                if (dr["sub_process_name"].ToString() == "SAWABLE" || dr["sub_process_name"].ToString() == "MATHALA")
                                    dr.Delete();
                            }
                            DTabDataSarin.AcceptChanges();
                            if (DTabDataSarin.Rows[0]["sarin_date"].ToString() != "")
                            {
                                DtpSarinDate.Text = Val.DBDate(DTabDataSarin.Rows[0]["sarin_date"].ToString());
                                decimal SarinEstcarat = Val.ToDecimal(DTabDataSarin.Rows[0]["org_carat"]);
                                txtCarat.Text = Val.ToDecimal(SarinEstcarat).ToString();
                            }
                            else
                            {
                                DtpSarinDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                                DtpSarinDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                                DtpSarinDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                                DtpSarinDate.Properties.CharacterCasing = CharacterCasing.Upper;
                                DtpSarinDate.EditValue = DateTime.Now;
                            }
                        }
                        grdRussianProcessGrp.DataSource = null;
                        grdPolishProcessGrp.DataSource = null;
                        grdSarinProcessGrp.DataSource = DTabDataSarin;
                        grdSarinProcessGrp.Focus();
                    }
                    else
                    {
                        if (GlobalDec.gEmployeeProperty.role_name == "SURAT SARIN" || GlobalDec.gEmployeeProperty.role_name == "SURAT KATARGAM")
                        {
                            panelControl4.Enabled = true;
                            panelControl7.Enabled = false;
                            panelControl10.Enabled = false;

                            DataTable DTab_GetProcessSarin = process.Select("process_name ='SARIN'").CopyToDataTable();

                            lueProcessSarin.EditValue = Val.ToInt32(DTab_GetProcessSarin.Rows[0]["process_id"]);

                            objMFGTRNEstimationProperty.entry_date = Val.DBDate(dtpEntryDate.Text);
                            objMFGTRNEstimationProperty.process_id = Val.ToInt32(lueProcessSarin.EditValue);
                            objMFGTRNEstimationProperty.lot_id = Val.ToInt32(txtLotID.EditValue);
                            objMFGTRNEstimationProperty.kapan_id = Val.ToInt32(lueKapan.EditValue);
                            objMFGTRNEstimationProperty.rough_cut_id = Val.ToInt32(lueCutNo.EditValue);

                            DataTable DTabDataSarin = objMFGTRNEstimation.GetSarinData(objMFGTRNEstimationProperty);

                            if (DTabDataSarin.Rows.Count > 0)
                            {
                                if (DTabDataSarin.Rows[0]["sarin_date"].ToString() != "")
                                {
                                    DtpSarinDate.Text = Val.DBDate(DTabDataSarin.Rows[0]["sarin_date"].ToString());
                                    decimal SarinEstcarat = Val.ToDecimal(DTabDataSarin.Rows[0]["org_carat"]);
                                    txtCarat.Text = Val.ToDecimal(SarinEstcarat).ToString();
                                }
                                else
                                {
                                    DtpSarinDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                                    DtpSarinDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                                    DtpSarinDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                                    DtpSarinDate.Properties.CharacterCasing = CharacterCasing.Upper;
                                    DtpSarinDate.EditValue = DateTime.Now;
                                }
                            }
                            grdRussianProcessGrp.DataSource = null;
                            grdPolishProcessGrp.DataSource = null;
                            grdSarinProcessGrp.DataSource = DTabDataSarin;
                            grdSarinProcessGrp.Focus();
                        }
                        if (GlobalDec.gEmployeeProperty.role_name == "SURAT RUSSIAN")
                        {
                            panelControl4.Enabled = false;
                            panelControl7.Enabled = true;
                            panelControl10.Enabled = false;
                            DataTable DTab_GetProcessRussian = new DataTable();

                            if (GlobalDec.gEmployeeProperty.department_name == "FARSI RUSSION" || GlobalDec.gEmployeeProperty.department_name == "G FR")
                            {
                                DTab_GetProcessRussian = process.Select("process_name ='FARSI RUSSIAN'").CopyToDataTable();
                            }
                            else
                            {
                                DTab_GetProcessRussian = process.Select("process_name ='RUSSIAN'").CopyToDataTable();
                            }
                            lueProcessRussian.EditValue = Val.ToInt32(DTab_GetProcessRussian.Rows[0]["process_id"]);

                            objMFGTRNEstimationProperty.process_id = Val.ToInt32(lueProcessRussian.EditValue);
                            objMFGTRNEstimationProperty.entry_date = Val.DBDate(dtpEntryDate.Text);
                            objMFGTRNEstimationProperty.lot_id = Val.ToInt32(txtLotID.EditValue);
                            objMFGTRNEstimationProperty.kapan_id = Val.ToInt32(lueKapan.EditValue);
                            objMFGTRNEstimationProperty.rough_cut_id = Val.ToInt32(lueCutNo.EditValue);

                            DataTable DTabDataRussain = objMFGTRNEstimation.GetSarinData(objMFGTRNEstimationProperty);
                            if ((GlobalDec.gEmployeeProperty.department_name == "RUSSIAN" || GlobalDec.gEmployeeProperty.department_name == "FARSI RUSSION" || GlobalDec.gEmployeeProperty.department_name == "G FR"))
                            {
                                if (Val.ToInt32(DTab_StockData.Rows[0]["department_id"]) != Val.ToInt32(GlobalDec.gEmployeeProperty.department_id))
                                {
                                    if (Val.ToInt32(GlobalDec.gEmployeeProperty.department_id) != Val.ToInt32(DTabDataRussain.Rows[0]["department_id"]))
                                    {
                                        DTab_StockData.Rows.Clear();
                                        DTab_StockData.AcceptChanges();
                                        grdRussianProcessGrp.DataSource = null;
                                        grdSarinProcessGrp.DataSource = null;
                                        grdPolishProcessGrp.DataSource = null;
                                        txtPcs.Text = "0";
                                        txtCarat.Text = "0";
                                        Global.Message("Lot Not in Your Department Plz Check!!!");
                                        txtLotID.Focus();
                                        return;
                                    }
                                }
                            }
                            if (DTabDataRussain.Rows.Count > 0)
                            {
                                if (DTabDataRussain.Rows[0]["russian_date"].ToString() != "")
                                {
                                    DtpRussianDate.Text = Val.DBDate(DTabDataRussain.Rows[0]["russian_date"].ToString());
                                }
                                else
                                {
                                    DtpRussianDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                                    DtpRussianDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                                    DtpRussianDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                                    DtpRussianDate.Properties.CharacterCasing = CharacterCasing.Upper;
                                    DtpRussianDate.EditValue = DateTime.Now;
                                }
                            }
                            grdRussianProcessGrp.DataSource = DTabDataRussain;
                            grdRussianProcessGrp.Focus();
                        }
                        if (GlobalDec.gEmployeeProperty.role_name == "SURAT MAKABLE" || GlobalDec.gEmployeeProperty.role_name == "SURAT POLISH REPARING")
                        {
                            panelControl4.Enabled = false;
                            panelControl7.Enabled = false;
                            panelControl10.Enabled = true;

                            DataTable DTab_GetProcessPolish = process.Select("process_name ='POLISH'").CopyToDataTable();

                            lueProcessPolish.EditValue = Val.ToInt32(DTab_GetProcessPolish.Rows[0]["process_id"]);

                            objMFGTRNEstimationProperty.process_id = Val.ToInt32(lueProcessPolish.EditValue);
                            objMFGTRNEstimationProperty.entry_date = Val.DBDate(dtpEntryDate.Text);
                            objMFGTRNEstimationProperty.lot_id = Val.ToInt32(txtLotID.EditValue);
                            objMFGTRNEstimationProperty.kapan_id = Val.ToInt32(lueKapan.EditValue);
                            objMFGTRNEstimationProperty.rough_cut_id = Val.ToInt32(lueCutNo.EditValue);

                            DataTable DTabDataPolish = objMFGTRNEstimation.GetSarinData(objMFGTRNEstimationProperty);

                            if (DTabDataPolish.Rows.Count > 0)
                            {
                                if (DTabDataPolish.Rows[0]["polish_date"].ToString() != "")
                                {
                                    DtpPolishDate.Text = Val.DBDate(DTabDataPolish.Rows[0]["polish_date"].ToString());
                                }
                                else
                                {
                                    DtpPolishDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                                    DtpPolishDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                                    DtpPolishDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                                    DtpPolishDate.Properties.CharacterCasing = CharacterCasing.Upper;
                                    DtpPolishDate.EditValue = DateTime.Now;
                                }
                            }
                            grdRussianProcessGrp.DataSource = null;
                            grdSarinProcessGrp.DataSource = null;
                            grdPolishProcessGrp.DataSource = DTabDataPolish;
                            grdPolishProcessGrp.Focus();
                        }
                    }
                }
                else
                {
                    Global.Message("Lot ID Not found");
                    txtLotID.Text = "";
                    txtLotID.Focus();
                    lueProcessSarin.EditValue = null;
                    lueProcessRussian.EditValue = null;
                    return;
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }

        #region Grid Event

        private void dgvPolishProcessGrp_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            CalculateGridAmount(dgvPolishProcessGrp.FocusedRowHandle);
        }
        private void dgvPolishProcessGrp_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            CalculateGridAmount(e.PrevFocusedRowHandle);
        }
        private void dgvPolishProcessGrp_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            GridView view = sender as GridView;

            if (view.FocusedColumn.FieldName == "pcs")
            {
                string brd = e.Value as string;
                if (Val.ToInt32(brd) > Val.ToInt32(txtPcs.Text))
                {
                    e.Valid = false;
                    e.ErrorText = "Estimation Pcs not more then Balance Pcs.";
                }
            }
            else if (view.FocusedColumn.FieldName == "carat")
            {
                string brd = e.Value as string;
                if (Val.ToDecimal(brd) > Val.ToDecimal(txtCarat.Text))
                {
                    e.Valid = false;
                    e.ErrorText = "Estimation Carat not more then Balance carat.";
                }
            }
        }
        private void dgvSarinProcessGrp_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            GridView view = sender as GridView;

            if (view.FocusedColumn.FieldName == "carat")
            {
                string brd = e.Value as string;

                if (Val.ToDecimal(brd) > (Val.ToDecimal(txtCarat.Text) + (Val.ToDecimal(txtCarat.Text) / 100) * 10))
                {
                    e.Valid = false;
                    e.ErrorText = "Estimation Carat not more then Balance carat.";
                }
            }
        }
        private void dgvRussianProcessGrp_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            GridView view = sender as GridView;

            if (view.FocusedColumn.FieldName == "pcs")
            {
                string brd = e.Value as string;
                if (Val.ToInt32(brd) > Val.ToInt32(txtPcs.Text))
                {
                    e.Valid = false;
                    e.ErrorText = "Estimation Pcs not more then Balance Pcs.";
                }
            }
            else if (view.FocusedColumn.FieldName == "carat")
            {
                string brd = e.Value as string;
                if (Val.ToDecimal(brd) > Val.ToDecimal(txtCarat.Text))
                {
                    e.Valid = false;
                    e.ErrorText = "Estimation Carat not more then Balance carat.";
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

                DtpSarinDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                DtpSarinDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                DtpSarinDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                DtpSarinDate.Properties.CharacterCasing = CharacterCasing.Upper;
                DtpSarinDate.EditValue = DateTime.Now;

                DtpRussianDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                DtpRussianDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                DtpRussianDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                DtpRussianDate.Properties.CharacterCasing = CharacterCasing.Upper;
                DtpRussianDate.EditValue = DateTime.Now;

                DtpPolishDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                DtpPolishDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                DtpPolishDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                DtpPolishDate.Properties.CharacterCasing = CharacterCasing.Upper;
                DtpPolishDate.EditValue = DateTime.Now;

                lueKapan.EditValue = System.DBNull.Value;
                lueCutNo.EditValue = System.DBNull.Value;
                lueProcessSarin.EditValue = System.DBNull.Value;
                lueProcessRussian.EditValue = System.DBNull.Value;
                lueProcessPolish.EditValue = System.DBNull.Value;

                txtPcs.Text = string.Empty;
                txtCarat.Text = string.Empty;

                DTab_StockData.Rows.Clear();
                DTab_StockData.AcceptChanges();

                grdRussianProcessGrp.DataSource = null;
                grdSarinProcessGrp.DataSource = null;
                grdPolishProcessGrp.DataSource = null;
                txtPassword.Text = "";

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
        private void CalculateGridAmount(int rowindex)
        {
            try
            {
                dgvPolishProcessGrp.SetRowCellValue(rowindex, "total", Math.Round((Val.ToDouble(dgvPolishProcessGrp.GetRowCellValue(rowindex, "rate")) * Val.ToDouble(dgvPolishProcessGrp.GetRowCellValue(rowindex, "pcs"))), 2));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
            if (GlobalDec.gEmployeeProperty.role_name == "SURAT MAKABLE" || GlobalDec.gEmployeeProperty.role_name == "SURAT KATARGAM" || GlobalDec.gEmployeeProperty.role_name == "SURAT SARIN")
            {
                MFGTRNEstimation objMFGTRNEstimation = new MFGTRNEstimation();
                MFGTRNEstimation_Property objMFGTRNEstimationProperty = new MFGTRNEstimation_Property();
                try
                {
                    DialogResult result = MessageBox.Show("Do you want to Delete Estimation data?", "Confirmation", MessageBoxButtons.YesNo);
                    if (result != DialogResult.Yes)
                    {
                        btnSave.Enabled = true;
                        return;
                    }
                    btnDelete.Enabled = false;

                    objMFGTRNEstimationProperty.lot_id = Val.ToInt64(txtLotID.Text);
                    objMFGTRNEstimationProperty.rough_cut_id = Val.ToInt64(lueCutNo.EditValue);
                    objMFGTRNEstimationProperty.kapan_id = Val.ToInt64(lueKapan.EditValue);


                    DataTable SarinEstimate_Data = (DataTable)grdSarinProcessGrp.DataSource;
                    if (SarinEstimate_Data != null)
                    {
                        if (SarinEstimate_Data.Rows.Count > 0)
                        {
                            objMFGTRNEstimationProperty.process_id = Val.ToInt(lueProcessSarin.EditValue);
                        }
                    }
                    DataTable RussianEstimate_Data = (DataTable)grdRussianProcessGrp.DataSource;
                    if (RussianEstimate_Data != null)
                    {
                        if (RussianEstimate_Data.Rows.Count > 0)
                        {
                            objMFGTRNEstimationProperty.process_id = Val.ToInt(lueProcessRussian.EditValue);
                        }
                    }

                    DataTable PolishEstimate_Data = (DataTable)grdPolishProcessGrp.DataSource;

                    if (PolishEstimate_Data != null)
                    {
                        if (PolishEstimate_Data.Rows.Count > 0)
                        {
                            objMFGTRNEstimationProperty.process_id = Val.ToInt(lueProcessPolish.EditValue);
                        }
                    }

                    int IntRes = objMFGTRNEstimation.Estimation_Data_Delete(objMFGTRNEstimationProperty);

                    if (IntRes > 0)
                    {
                        Global.Confirm("Estimation Data Deleted Succesfully");
                        btnSave.Enabled = true;
                        btnDelete.Enabled = true;
                        ClearDetails();
                    }
                    else
                    {
                        Global.Confirm("Error In Estimation Data");
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
        private void txtLotID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
