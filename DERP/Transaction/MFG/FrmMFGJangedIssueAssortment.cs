using BLL;
using BLL.FunctionClasses.Master.MFG;
using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.PropertyClasses.Transaction;
using BLL.PropertyClasses.Transaction.MFG;
using DERP.Class;
using DERP.Master;
using DERP.Master.MFG;
using DERP.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Transaction.MFG
{
    public partial class FrmMFGJangedIssueAssortment : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        MFGEmployeeTarget objEmployeeTarget;
        MFGSawableRecieve objSawableRecieve;
        MFGProcessReceive objProcessRecieve;
        MfgRoughSieve objRoughSieve;
        MfgRoughClarityMaster objClarity;

        DataTable m_dtbDetail;
        DataTable m_dtbParam;
        DataTable DtAssortment = new DataTable();
        DataTable m_dtbSubProcess;
        DataTable m_dtbIssueProcess;
        DataTable m_dtOutstanding;
        DataTable m_dtbKapan;
        DataTable m_dtbDetails;
        MFGAssortSecond objAssortSecond = new MFGAssortSecond();
        //DataTable DTab_KapanWiseData = new DataTable();

        int m_prd_id;
        int outside;
        int m_fromManagerId;
        int m_IsLot;
        int m_balpcs;
        Int64 m_numForm_id;
        Int64 IntRes;
        Int64 JangedNo_IntRes;
        Int64 Janged_IntRes;
        int m_kapan_id;
        Int64 JangedSrNo_IntRes;
        Int64 Lot_SrNo = 0;

        decimal m_balcarat;
        decimal m_OScarat;
        decimal m_chipyo_rate;
        decimal m_numSummRate;
        decimal m_numSummLRate;

        bool m_blnadd;
        bool m_blnsave;
        bool m_blnflag;
        bool m_blncheckupdate;
        string temp_purity_name = string.Empty;
        string temp_sieve_name = string.Empty;
        #endregion

        #region Constructor
        public FrmMFGJangedIssueAssortment()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            objEmployeeTarget = new MFGEmployeeTarget();
            objSawableRecieve = new MFGSawableRecieve();
            objProcessRecieve = new MFGProcessReceive();
            objRoughSieve = new MfgRoughSieve();
            objClarity = new MfgRoughClarityMaster();

            m_dtbDetail = new DataTable();
            m_dtbParam = new DataTable();
            m_dtbSubProcess = new DataTable();
            m_dtbIssueProcess = new DataTable();
            m_dtOutstanding = new DataTable();
            m_dtbKapan = new DataTable();
            m_dtbDetails = new DataTable();

            //m_Srno = 1;
            //m_update_srno = 1;
            //m_flag = 0;
            m_prd_id = 0;
            m_numForm_id = 0;
            m_kapan_id = 0;
            outside = 0;
            m_fromManagerId = 0;
            m_IsLot = 0;
            m_balpcs = 0;

            //m_cut_no = "";

            m_balcarat = 0;
            //m_numcarat = 0;
            //m_old_carat = 0;
            //m_old_rate = 0;
            //m_old_amount = 0;
            m_OScarat = 0;
            m_chipyo_rate = 0;
            m_numSummRate = 0;
            m_numSummLRate = 0;
            m_blnadd = new bool();
            m_blnsave = new bool();
            m_blnflag = new bool();
            m_blncheckupdate = new bool();
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

        #region Events
        private void RepDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (Global.Confirm("Are you sure delete selected row?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                dgvJangedList.DeleteRow(dgvJangedList.GetRowHandle(dgvJangedList.FocusedRowHandle));
                m_dtbIssueProcess.AcceptChanges();
            }
        }
        private void btnPopUpStock_Click(object sender, EventArgs e)
        {
            GetPendingStock();
        }
        private void FrmMFGProcessIssue_Load(object sender, EventArgs e)
        {
            try
            {
                Global.LOOKUPProcess(lueProcess);
                lueProcess.Text = "ASSORTMENT";
                Global.LOOKUPSubProcess(lueSubProcess);
                Global.LOOKUPCompany_New(lueToCompany);
                Global.LOOKUPBranch_New(lueToBranch);
                Global.LOOKUPLocation_New(lueToLocation);
                Global.LOOKUPDepartment_New(lueToDepartment);

                m_dtbSubProcess = (((DataTable)lueSubProcess.Properties.DataSource).Copy());

                lueProcess_EditValueChanged(null, null);
                lueSubProcess.Text = "JANGED ISSUE";

                dtpIssueDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpIssueDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpIssueDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpIssueDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpIssueDate.EditValue = DateTime.Now;

                dtpFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpFromDate.EditValue = DateTime.Now;

                dtpToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpToDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpToDate.EditValue = DateTime.Now;

                m_dtbKapan = Global.GetKapanAll_Assort();

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
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                btnSave.Enabled = false;
                ObjPer.SetFormPer();
                if (ObjPer.AllowUpdate == false || ObjPer.AllowInsert == false)
                {
                    Global.Message(BLL.GlobalDec.gStrPermissionInsUpdMsg);
                    btnSave.Enabled = true;
                    return;
                }
                //string Str = "";
                //if (Val.DBDate(BLL.GlobalDec.gStrServerDate) != Val.DBDate(dtpIssueDate.Text))
                //{
                //    Str = GlobalDec.CheckLockIsOpenOrNot(Val.DBDate(dtpIssueDate.Text), Val.DBTime(DateTime.Now.ToShortTimeString())); //Val.GetFullTime12());
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
                //        dtpIssueDate.Enabled = true;
                //        dtpIssueDate.Visible = true;
                //    }
                //}
                //foreach (DataRow drw in m_dtbIssueProcess.Rows)
                //{
                //    if (Val.ToDecimal(drw["amount"]) == 0)
                //    {
                //        MessageBox.Show("Amount Should Not Be Blank.");
                //        btnSave.Enabled = true;
                //        return;
                //    }
                //}

                m_blnsave = true;
                m_blnadd = false;
                if (!ValidateDetails())
                {
                    m_blnsave = false;
                    btnSave.Enabled = true;
                    return;
                }

                DialogResult result = MessageBox.Show("Do you want to save Process issue data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result != DialogResult.Yes)
                {
                    btnSave.Enabled = true;
                    return;
                }
                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                backgroundWorker_JangedIssue.RunWorkerAsync();

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
        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            if (!PopulateDetails())
                return;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            Global.Export("xlsx", grvJangedIssueAssortment);
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
                if (lueProcess.EditValue != System.DBNull.Value && lueSubProcess.EditValue != System.DBNull.Value && Val.ToInt64(txtLotID.Text) != 0)
                {
                    DataTable dtIss = new DataTable();
                    dtIss = objProcessRecieve.GetIssueID(Val.ToInt64(txtLotID.Text), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue));
                    if (dtIss.Rows.Count > 0)
                    {
                        Global.Message("Lot is already issue in this process.");
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void lueCutNo_EditValueChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (!m_blnflag)
                {
                    if (lueCutNo.EditValue != System.DBNull.Value)
                    {
                        if (m_dtbParam.Rows.Count > 0)
                        {
                            DataRow[] dr = m_dtbParam.Select("rough_cut_no ='" + Val.ToString(lueCutNo.Text) + "'");
                            if (dr.Length > 0)
                            {
                                if (txtLotID.Text != string.Empty || Val.ToInt64(txtLotID.Text) != 0)
                                {
                                    GetOsCarat(Val.ToInt64(txtLotID.Text));
                                    DataTable dtIssOS = new DataTable();
                                    dtIssOS = objProcessRecieve.Carat_OutStanding_GetData(Val.ToInt64(txtLotID.Text), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue), 0, "I");

                                    if (Val.ToDecimal(m_balcarat) > 0 && dtIssOS.Rows.Count > 0)
                                    {
                                        m_OScarat = Val.ToDecimal(Val.ToDecimal(dtIssOS.Rows[0]["carat"]));
                                    }
                                }
                            }
                        }
                    }
                    m_blnflag = false;
                }
                else
                {
                    m_blnflag = false;
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void txtLotID_Validated(object sender, EventArgs e)
        {
            try
            {
                if (Save_Validate())
                {
                    m_dtbIssueProcess.AcceptChanges();
                    if (m_dtbIssueProcess != null)
                    {
                        if (m_dtbIssueProcess.Rows.Count > 0)
                        {
                            DataRow[] dr = m_dtbIssueProcess.Select("lot_id = " + Val.ToInt64(txtLotID.Text));

                            if (dr.Length > 0)
                            {
                                Global.Message(Val.ToInt64(txtLotID.Text) + " Lot ID already added to the Issue list!");
                                txtLotID.Text = "";
                                txtLotID.Focus();
                                return;
                            }

                            //for (int i = 0; i < m_dtbIssueProcess.Rows.Count; i++)
                            //{
                            //    if (m_dtbIssueProcess.Rows[i]["lot_id"].ToString() == txtLotID.Text)
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

                    if (m_dtbIssueProcess.Rows.Count > 0)
                    {
                        DataTable DTabTemp = new DataTable();
                        DataTable DTab_ValidateLotID = Global.GetRoughStockWise(Val.ToInt(lueKapan.EditValue), Val.ToInt64(txtLotID.Text));

                        if (DTab_ValidateLotID.Rows.Count > 0)
                        {

                        }
                        else
                        {
                            Global.Message("Lot ID Not Issue in Janged");
                            txtLotID.Text = "";
                            txtLotID.Focus();
                            return;
                        }
                        DTabTemp = Global.GetRoughStockWise(Val.ToInt(lueKapan.EditValue), Val.ToInt64(txtLotID.Text));

                        if (DTabTemp.Rows.Count > 0)
                        {
                            txtLotID.Text = "";
                            txtLotID.Focus();
                        }
                        m_dtbIssueProcess.Merge(DTabTemp);
                    }
                    else
                    {
                        DataTable DTab_ValidateLotID = Global.GetRoughStockWise(Val.ToInt(lueKapan.EditValue), Val.ToInt64(txtLotID.Text));

                        if (DTab_ValidateLotID.Rows.Count > 0)
                        {
                        }
                        else
                        {
                            Global.Message("Lot ID Not Issue in Janged");
                            txtLotID.Text = "";
                            txtLotID.Focus();
                            return;
                        }

                        m_dtbIssueProcess = Global.GetRoughStockWise(Val.ToInt(lueKapan.EditValue), Val.ToInt64(txtLotID.Text));

                        if (m_dtbIssueProcess.Rows.Count > 0)
                        {
                            txtLotID.Text = "";
                            txtLotID.Focus();
                        }
                    }

                    grdJangedIssueAssortment.DataSource = m_dtbIssueProcess;
                    grdJangedIssueAssortment.RefreshDataSource();
                    dgvJangedList.BestFitColumns();
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
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
        private void lueKapan_EditValueChanged(object sender, EventArgs e)
        {
            if (lueKapan.Text.ToString() == "" && lueKapan.Text.ToString() == "" && txtLotID.Text.ToString() != "")
            {
                m_IsLot = 1;
            }
            else
            {
                m_IsLot = 0;
            }
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
                lueCutNo.Properties.DataSource = m_dtbParam;
                lueCutNo.Properties.ValueMember = "rough_cut_id";
                lueCutNo.Properties.DisplayMember = "rough_cut_no";
            }
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            MFGJangedIsuRecAssortment objMFGJangedIssue = new MFGJangedIsuRecAssortment();
            DataSet DSetJanged = objMFGJangedIssue.Print_Janged(Val.ToInt32(lueKapan.EditValue), Val.ToInt32(lueCutNo.EditValue), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue), Val.ToString(temp_purity_name), Val.ToInt64(lblLotSRNo.Text));
            //DataTable DTab_IssueJanged = objAssortFirst.Print_Semi_1(Val.ToInt32(lueKapan.EditValue), Val.ToInt32(lueCutNo.EditValue), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue));
            //if (DSetJanged.Tables[0].Rows.Count > 0)
            //{
            //    DSetJanged.Tables[0].Rows.Add(0, "", 0, "", "", 0, "", "", 0, 0, 0, 0, 0, 0, 0, 0);
            //    DSetJanged.Tables[0].Rows.Add(0, "", 0, "", "", 0, "", "", 0, 0, 0, 0, 0, 0, 0, 0);
            //}
            //if (DSetJanged.Tables[1].Rows.Count > 0)
            //{
            //    DSetJanged.Tables[1].Rows.Add(0, "", 0, "", "", 0, "", "", 0, 0, 0, 0, 0, 0, 0, 0);
            //    DSetJanged.Tables[1].Rows.Add(0, "", 0, "", "", 0, "", "", 0, 0, 0, 0, 0, 0, 0, 0);
            //    DSetJanged.Tables[1].Rows.Add(0, "", 0, "", "", 0, "", "", 0, 0, 0, 0, 0, 0, 0, 0);
            //}
            FrmReportViewer FrmReportViewer = new FrmReportViewer();
            //FrmReportViewer.DS.Tables.Add(0);
            foreach (DataTable DTab in DSetJanged.Tables)
                FrmReportViewer.DS.Tables.Add(DTab.Copy());
            FrmReportViewer.GroupBy = "";
            FrmReportViewer.RepName = "";
            FrmReportViewer.RepPara = "";
            this.Cursor = Cursors.Default;
            FrmReportViewer.AllowSetFormula = true;

            FrmReportViewer.ShowForm_SubReport("CrtPolishGrading_Janged_IsuReturn_Main", 120, FrmReportViewer.ReportFolder.ACCOUNT);

            //DTab_IssueJanged = null;
            FrmReportViewer.DS.Tables.Clear();
            FrmReportViewer.DS.Clear();
            FrmReportViewer = null;
        }
        private void lueToLocation_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (e.Button.Index == 1)
                {
                    FrmLocationMaster frmLocation = new FrmLocationMaster();
                    frmLocation.ShowDialog();
                    Global.LOOKUPLocation(lueToLocation);
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void lueToDepartment_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (e.Button.Index == 1)
                {
                    FrmDepartmentMaster frmDepartment = new FrmDepartmentMaster();
                    frmDepartment.ShowDialog();
                    Global.LOOKUPDepartment(lueToDepartment);
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void backgroundWorker_ProcessIssue_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                MFGJangedIsuRecAssortment objMFGJangedIsuRecAssort = new MFGJangedIsuRecAssortment();
                MFGJangedIssue_Property objMFGJangedIssueProperty = new MFGJangedIssue_Property();
                Conn = new BeginTranConnection(true, false);

                IntRes = 0;
                JangedNo_IntRes = 0;
                Janged_IntRes = 0;
                JangedSrNo_IntRes = 0;
                Int64 NewHistory_Union_Id = 0;
                Lot_SrNo = 0;
                try
                {
                    objMFGJangedIssueProperty.kapan_id = Val.ToInt(lueKapan.EditValue);
                    objMFGJangedIssueProperty.rough_cut_id = Val.ToInt(lueCutNo.EditValue);
                    objMFGJangedIssueProperty.process_id = Val.ToInt(lueProcess.EditValue);
                    objMFGJangedIssueProperty.sub_process_id = Val.ToInt(lueSubProcess.EditValue);
                    objMFGJangedIssueProperty.temp_quality_name = Val.ToString(temp_purity_name);
                    objMFGJangedIssueProperty.Del_lot_srno = Val.ToInt64(lblLotSRNo.Text);

                    if (Val.ToInt64(lblLotSRNo.Text) > 0)
                    {
                        //int Lot_Delete = objMFGJangedIsuRecAssort.GetDeleteAssort_ID(objMFGJangedIssueProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                    }

                    foreach (DataRow drw in m_dtbIssueProcess.Rows)
                    {
                        objMFGJangedIssueProperty.janged_id = Val.ToInt(0); //Val.ToInt(drw["janged_id"]);
                        objMFGJangedIssueProperty.janged_no = JangedNo_IntRes;
                        objMFGJangedIssueProperty.lot_id = Val.ToInt(drw["lot_id"]);
                        objMFGJangedIssueProperty.union_id = IntRes;
                        objMFGJangedIssueProperty.kapan_id = Val.ToInt(lueKapan.EditValue);
                        objMFGJangedIssueProperty.rough_cut_id = Val.ToInt(lueCutNo.EditValue);
                        objMFGJangedIssueProperty.janged_date = Val.DBDate(dtpIssueDate.Text);
                        objMFGJangedIssueProperty.to_company_id = Val.ToInt(lueToCompany.EditValue);
                        objMFGJangedIssueProperty.to_branch_id = Val.ToInt(lueToBranch.EditValue);
                        objMFGJangedIssueProperty.to_location_id = Val.ToInt(lueToLocation.EditValue);
                        objMFGJangedIssueProperty.to_department_id = Val.ToInt(lueToDepartment.EditValue);
                        objMFGJangedIssueProperty.employee_id = Val.ToInt(0);

                        objMFGJangedIssueProperty.process_id = Val.ToInt(lueProcess.EditValue);
                        objMFGJangedIssueProperty.sub_process_id = Val.ToInt(lueSubProcess.EditValue);

                        objMFGJangedIssueProperty.rough_sieve_id = Val.ToInt(drw["sieve_id"]);
                        objMFGJangedIssueProperty.purity_id = Val.ToInt(drw["purity_id"]);
                        objMFGJangedIssueProperty.color_id = Val.ToInt(drw["color_id"]);
                        //objMFGJangedIssueProperty.pcs = Val.ToInt(drw["pcs"]);
                        objMFGJangedIssueProperty.carat = Val.ToDecimal(drw["carat"]);
                        objMFGJangedIssueProperty.rate = Val.ToDecimal(drw["rate"]);
                        objMFGJangedIssueProperty.amount = Val.ToDecimal(drw["amount"]);
                        objMFGJangedIssueProperty.percentage = Val.ToDecimal(Math.Round((Val.ToDecimal(drw["carat"]) * 100) / Val.ToDecimal(clmCarat.SummaryItem.SummaryValue), 2));
                        objMFGJangedIssueProperty.prediction_id = Val.ToInt(drw["prd_id"]);
                        objMFGJangedIssueProperty.form_id = Val.ToInt(m_numForm_id);
                        objMFGJangedIssueProperty.is_outside = Val.ToInt(outside);
                        objMFGJangedIssueProperty.janged_union_id = Janged_IntRes;
                        objMFGJangedIssueProperty.history_union_id = NewHistory_Union_Id;
                        objMFGJangedIssueProperty.janged_Srno = JangedSrNo_IntRes;
                        objMFGJangedIssueProperty.lot_srno = Lot_SrNo;
                        objMFGJangedIssueProperty.temp_quality_name = Val.ToString(drw["temp_quality_name"]);
                        objMFGJangedIssueProperty.temp_sieve_name = Val.ToString(drw["temp_sieve_name"]);
                        objMFGJangedIssueProperty.minus2_amt = Val.ToInt(txtMinus2Amt.Text);
                        objMFGJangedIssueProperty.plus2_amt = Val.ToInt(txtPlus2Amt.Text);

                        objMFGJangedIssueProperty = objMFGJangedIsuRecAssort.Save(objMFGJangedIssueProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                        Janged_IntRes = objMFGJangedIssueProperty.janged_union_id;
                        JangedNo_IntRes = objMFGJangedIssueProperty.janged_no;
                        NewHistory_Union_Id = Val.ToInt64(objMFGJangedIssueProperty.history_union_id);
                        JangedSrNo_IntRes = Val.ToInt64(objMFGJangedIssueProperty.janged_Srno);
                        Lot_SrNo = Val.ToInt64(objMFGJangedIssueProperty.lot_srno);
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
        private void backgroundWorker_ProcessIssue_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (JangedNo_IntRes > 0)
                {
                    m_dtbIssueProcess = new DataTable();
                    DialogResult result = MessageBox.Show("Janged Issue Succesfully and janged no is :" + JangedNo_IntRes + "Are you sure print this janged?", "Confirmation", MessageBoxButtons.YesNoCancel);
                    if (result != DialogResult.Yes)
                    {
                        btnSave.Enabled = true;
                        ClearDetails();
                        lblLotSRNo.Text = Lot_SrNo.ToString();
                        return;
                    }
                    lblLotSRNo.Text = Lot_SrNo.ToString();
                    MFGJangedIsuRecAssortment objMFGJangedIssue = new MFGJangedIsuRecAssortment();
                    DataSet DSetJanged = objMFGJangedIssue.Print_Janged(Val.ToInt32(lueKapan.EditValue), Val.ToInt32(lueCutNo.EditValue), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue), Val.ToString(temp_purity_name), Val.ToInt64(lblLotSRNo.Text));

                    FrmReportViewer FrmReportViewer = new FrmReportViewer();
                    //FrmReportViewer.DS.Tables.Add(0);
                    foreach (DataTable DTab in DSetJanged.Tables)
                        FrmReportViewer.DS.Tables.Add(DTab.Copy());
                    FrmReportViewer.GroupBy = "";
                    FrmReportViewer.RepName = "";
                    FrmReportViewer.RepPara = "";
                    this.Cursor = Cursors.Default;
                    FrmReportViewer.AllowSetFormula = true;

                    FrmReportViewer.ShowForm_SubReport("CrtPolishGrading_Janged_IsuReturn_Main", 120, FrmReportViewer.ReportFolder.ACCOUNT);

                    //DTab_IssueJanged = null;
                    FrmReportViewer.DS.Tables.Clear();
                    FrmReportViewer.DS.Clear();
                    FrmReportViewer = null;

                    btnSave.Enabled = true;
                    ClearDetails();
                }
                else
                {
                    Global.Confirm("Error In Janged Issue");
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }
        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                MFGJangedIsuRecAssortment objMFGJangedIsuRec = new MFGJangedIsuRecAssortment();
                MFGProcessIssueProperty objMFGProcessIssueProperty = new MFGProcessIssueProperty();
                objMFGProcessIssueProperty.flag = Val.ToInt(4);
                objMFGProcessIssueProperty.process_id = Val.ToInt(lueProcess.EditValue);
                objMFGProcessIssueProperty.sub_process_id = Val.ToInt(lueSubProcess.EditValue);
                objMFGProcessIssueProperty.location_id = Val.ToInt32(GlobalDec.gEmployeeProperty.location_id);

                DtAssortment = objMFGJangedIsuRec.GetPendingStock(objMFGProcessIssueProperty);

                FrmMFGAssortmentStock FrmAssortmentStock = new FrmMFGAssortmentStock();
                FrmAssortmentStock.FrmMFGJangedIssueAssort = this;
                FrmAssortmentStock.DTab = DtAssortment;
                FrmAssortmentStock.ShowForm(this, Val.ToInt64(lblLotSRNo.Text));
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
            }
        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (GlobalDec.gEmployeeProperty.user_name != "RIKITA")
            {
                Global.Message("Don't have permission...Please Contact to Administrator...");
                return;
            }
            if (Val.ToInt(lblLotSRNo.Text) != 0)
            {
                DialogResult result = MessageBox.Show("Do you want to Delete Janged Issue Assortment data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result != DialogResult.Yes)
                {
                    btnSave.Enabled = true;
                    return;
                }
                IntRes = 0;
                MFGJangedIsuRecAssortment objMFGJangedIsuRecAssort = new MFGJangedIsuRecAssortment();
                MFGJangedIssue_Property objMFGJangedIssueProperty = new MFGJangedIssue_Property();

                objMFGJangedIssueProperty.manager_id = Val.ToInt(0);
                objMFGJangedIssueProperty.employee_id = Val.ToInt(0);
                objMFGJangedIssueProperty.process_id = Val.ToInt(lueProcess.EditValue);
                objMFGJangedIssueProperty.sub_process_id = Val.ToInt(lueSubProcess.EditValue);
                objMFGJangedIssueProperty.kapan_id = Val.ToInt(lueKapan.EditValue);
                objMFGJangedIssueProperty.lot_id = Val.ToInt(0);
                objMFGJangedIssueProperty.rough_cut_id = Val.ToInt(lueCutNo.EditValue);
                objMFGJangedIssueProperty.flag = Val.ToInt(3);
                objMFGJangedIssueProperty.Del_lot_srno = Val.ToInt64(lblLotSRNo.Text);
                objMFGJangedIssueProperty.assort_total_carat = Val.ToDecimal(clmCarat.SummaryItem.SummaryValue);
                objMFGJangedIssueProperty.form_id = m_numForm_id;

                IntRes = objMFGJangedIsuRecAssort.GetDeleteFinalLot_ID(objMFGJangedIssueProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                if (IntRes > 0)
                {
                    Global.Confirm("Janged Issue Assortment Data Deleted Succesfully");
                    ClearDetails();
                    btnSave.Enabled = true;
                }
                else
                {
                    Global.Confirm("Error In Janged Issue Assortment Data");
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

        #region GridEvents
        private void grvJangedIssue_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        if (m_blncheckupdate)
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void dgvJangedList_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                MFGJangedIssue objMFGJangedIssue = new MFGJangedIssue();
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvJangedList.GetDataRow(e.RowHandle);
                        dtpIssueDate.Text = Val.DBDate(Val.ToString(Drow["janged_date"]));
                        lueToCompany.EditValue = Val.ToInt(Drow["company_id"]);
                        lueToBranch.EditValue = Val.ToInt(Drow["branch_id"]);
                        lueToLocation.EditValue = Val.ToInt(Drow["location_id"]);
                        lueToDepartment.EditValue = Val.ToInt(Drow["department_id"]);
                        lueKapan.EditValue = Val.ToInt(Drow["kapan_id"]);
                        grdJangedIssueAssortment.DataSource = m_dtbIssueProcess;

                        ttlbJangedIssue.SelectedTabPage = tblJangeddetail;
                        lueKapan.Focus();
                        m_blncheckupdate = false;
                        btnSave.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void grvJangedIssue_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                if (Val.ToDecimal(clmAmount.SummaryItem.SummaryValue) > 0 && Val.ToDecimal(clmCarat.SummaryItem.SummaryValue) > 0)
                {
                    m_numSummRate = Math.Round((Val.ToDecimal(clmAmount.SummaryItem.SummaryValue) / Val.ToDecimal(clmCarat.SummaryItem.SummaryValue)), 2, MidpointRounding.AwayFromZero);
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
        private void dgvJangedList_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                if (Val.ToDecimal(clmLAmount.SummaryItem.SummaryValue) > 0 && Val.ToDecimal(clmLCarat.SummaryItem.SummaryValue) > 0)
                {
                    m_numSummLRate = Math.Round((Val.ToDecimal(clmLAmount.SummaryItem.SummaryValue) / Val.ToDecimal(clmLCarat.SummaryItem.SummaryValue)), 2, MidpointRounding.AwayFromZero);

                }
                else
                {
                    m_numSummRate = 0;
                }
                if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == "rate")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                        e.TotalValue = m_numSummLRate;
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
                if (m_blnsave)
                {
                    if (m_dtbIssueProcess.Rows.Count == 0)
                    {
                        lstError.Add(new ListError(22, "Record"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                        }
                    }
                    var result = DateTime.Compare(Convert.ToDateTime(dtpIssueDate.Text), DateTime.Today);
                    if (result > 0)
                    {
                        lstError.Add(new ListError(5, " Issue Date Not Be Greater Than Today Date"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            dtpIssueDate.Focus();
                        }
                    }
                    if (Val.ToString(dtpIssueDate.Text) == string.Empty)
                    {
                        lstError.Add(new ListError(22, "Date"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            dtpIssueDate.Focus();
                        }
                    }
                    if (lueToCompany.Text == "")
                    {
                        lstError.Add(new ListError(13, "To Company"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueToCompany.Focus();
                        }
                    }
                    if (lueToBranch.Text == "")
                    {
                        lstError.Add(new ListError(13, "To Branch"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueToBranch.Focus();
                        }
                    }
                    if (lueToLocation.Text == "")
                    {
                        lstError.Add(new ListError(13, "To Location"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueToLocation.Focus();
                        }
                    }
                    if (lueToDepartment.Text == "")
                    {
                        lstError.Add(new ListError(13, "To Department"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueToDepartment.Focus();
                        }
                    }
                    if (txtMinus2Amt.Text == "" || txtMinus2Amt.Text == "0")
                    {
                        lstError.Add(new ListError(13, "-2 Amt"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtMinus2Amt.Focus();
                        }
                    }
                    if (txtPlus2Amt.Text == "" || txtPlus2Amt.Text == "0")
                    {
                        lstError.Add(new ListError(13, "+2 Amt"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtPlus2Amt.Focus();
                        }
                    }
                    //if (Val.ToInt(lueToDepartment.EditValue) == GlobalDec.gEmployeeProperty.department_id)
                    //{
                    //    lstError.Add(new ListError(5, "Lot Not Transfer in a Same Department"));
                    //    if (!blnFocus)
                    //    {
                    //        blnFocus = true;
                    //        lueToDepartment.Focus();
                    //    }
                    //}
                }

                if (m_blnadd)
                {
                    if (Val.ToInt64(txtLotID.Text) == 0)
                    {
                        lstError.Add(new ListError(12, "Lot No"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtLotID.Focus();
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
                if (!GenerateProcessDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }

                dtpIssueDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpIssueDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpIssueDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpIssueDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpIssueDate.EditValue = DateTime.Now;

                dtpFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpFromDate.EditValue = DateTime.Now;

                dtpToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpToDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpToDate.EditValue = DateTime.Now;

                lueProcess.EditValue = System.DBNull.Value;
                lueSubProcess.EditValue = System.DBNull.Value;

                lueToCompany.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.company_id);
                lueToBranch.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.branch_id);
                lueToLocation.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.location_id);
                lueToDepartment.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.department_id);
                txtLotID.Text = string.Empty;
                //m_flag = 0;
                //m_Srno = 1;
                //m_update_srno = 0;
                //m_numcarat = 0;
                //m_old_carat = 0;
                //m_old_rate = 0;
                //m_old_amount = 0;
                m_kapan_id = 0;
                btnSave.Enabled = true;
                m_blncheckupdate = true;

                txtMinus2Amt.Text = "0";
                txtPlus2Amt.Text = "0";

                lueProcess.Text = "ASSORTMENT";
                lueProcess_EditValueChanged(null, null);
                lueSubProcess.Text = "JANGED ISSUE";
                btnSave.Enabled = true;
                lblLotSRNo.Text = "0";
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;

        }
        private bool GenerateProcessDetails()
        {
            bool blnReturn = true;
            try
            {
                if (m_dtbIssueProcess.Rows.Count > 0)
                    m_dtbIssueProcess.Rows.Clear();

                m_dtbIssueProcess = new DataTable();

                m_dtbIssueProcess.Columns.Add("janged_id", typeof(int));
                m_dtbIssueProcess.Columns.Add("janged_date", typeof(DateTime));
                m_dtbIssueProcess.Columns.Add("lot_id", typeof(int));
                m_dtbIssueProcess.Columns.Add("rough_cut_id", typeof(int));
                m_dtbIssueProcess.Columns.Add("cut_no", typeof(string));
                m_dtbIssueProcess.Columns.Add("to_manager", typeof(string));
                m_dtbIssueProcess.Columns.Add("to_manager_id", typeof(int));
                m_dtbIssueProcess.Columns.Add("manager_id", typeof(int));
                m_dtbIssueProcess.Columns.Add("employee", typeof(string));
                m_dtbIssueProcess.Columns.Add("employee_id", typeof(int));
                m_dtbIssueProcess.Columns.Add("process", typeof(string));
                m_dtbIssueProcess.Columns.Add("process_id", typeof(int));
                m_dtbIssueProcess.Columns.Add("subprocess", typeof(string));
                m_dtbIssueProcess.Columns.Add("sub_process_id", typeof(int));
                m_dtbIssueProcess.Columns.Add("sieve_name", typeof(string));
                m_dtbIssueProcess.Columns.Add("rough_sieve_id", typeof(int));
                m_dtbIssueProcess.Columns.Add("purity_name", typeof(string));
                m_dtbIssueProcess.Columns.Add("purity_id", typeof(int));
                m_dtbIssueProcess.Columns.Add("quality_name", typeof(string));
                m_dtbIssueProcess.Columns.Add("quality_id", typeof(int));
                m_dtbIssueProcess.Columns.Add("rough_clarity_name", typeof(string));
                m_dtbIssueProcess.Columns.Add("rough_clarity_id", typeof(int));
                m_dtbIssueProcess.Columns.Add("party_name", typeof(string));
                m_dtbIssueProcess.Columns.Add("party_id", typeof(int));
                m_dtbIssueProcess.Columns.Add("pcs", typeof(int)).DefaultValue = 0;
                m_dtbIssueProcess.Columns.Add("carat", typeof(decimal)).DefaultValue = 0;
                m_dtbIssueProcess.Columns.Add("rr_pcs", typeof(int)).DefaultValue = 0;
                m_dtbIssueProcess.Columns.Add("rr_carat", typeof(decimal)).DefaultValue = 0;
                m_dtbIssueProcess.Columns.Add("rate", typeof(decimal)).DefaultValue = 0;
                m_dtbIssueProcess.Columns.Add("amount", typeof(decimal)).DefaultValue = 0;
                m_dtbIssueProcess.Columns.Add("sr_no", typeof(int)).DefaultValue = 0;
                m_dtbIssueProcess.Columns.Add("prd_id", typeof(int)).DefaultValue = 0;
                m_dtbIssueProcess.Columns.Add("kapan_id", typeof(int)).DefaultValue = 0;
                grdJangedIssueAssortment.DataSource = m_dtbIssueProcess;
                grdJangedIssueAssortment.Refresh();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
        }
        public void GetOsCarat(Int64 lotId)
        {
            try
            {
                MFGProcessIssue objMFGProcessIssue = new MFGProcessIssue();
                DataTable dtChipyoRate = new DataTable();
                if (lotId > 0)
                {
                    dtChipyoRate = objMFGProcessIssue.GetOSRate(Val.ToInt(lueCutNo.EditValue), lotId, Val.ToString(lueProcess.Text));
                    m_dtOutstanding = objSawableRecieve.GetBalanceCarat(lotId, Val.ToInt(lueCutNo.EditValue));

                }
                if (dtChipyoRate.Rows.Count > 0)
                {
                    m_chipyo_rate = Val.ToDecimal(dtChipyoRate.Rows[0]["rate"]);
                }
                else
                {
                    m_chipyo_rate = 0;
                }
                if (m_dtOutstanding.Rows.Count > 0)
                {
                    m_balcarat = Val.ToDecimal(m_dtOutstanding.Rows[0]["balance_carat"]);
                    m_balpcs = Val.ToInt(m_dtOutstanding.Rows[0]["balance_pcs"]);
                    m_fromManagerId = Val.ToInt(m_dtOutstanding.Rows[0]["manager_id"]);
                    lueCutNo.Text = Val.ToString(m_dtOutstanding.Rows[0]["rough_cut_no"]);
                    m_kapan_id = Val.ToInt(m_dtOutstanding.Rows[0]["kapan_id"]);
                    if (Val.ToInt(m_dtOutstanding.Rows[0]["chipyo_prd_id"]) > 0)
                    {
                        m_prd_id = Val.ToInt(m_dtOutstanding.Rows[0]["chipyo_prd_id"]);
                    }
                    if (Val.ToInt(m_dtOutstanding.Rows[0]["sawable_prd_id"]) > 0)
                    {
                        m_prd_id = Val.ToInt(m_dtOutstanding.Rows[0]["sawable_prd_id"]);
                    }
                    else
                    {
                        m_prd_id = Val.ToInt(0);
                    }
                }
                else
                {
                    BLL.General.ShowErrors("Cut No not Found");
                    lueCutNo.EditValue = System.DBNull.Value;
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private bool PopulateDetails()
        {
            MFGJangedIssue objMFGJangedIssue = new MFGJangedIssue();
            bool blnReturn = true;
            DateTime datFromDate = DateTime.MinValue;
            DateTime datToDate = DateTime.MinValue;
            try
            {
                m_dtbDetails = objMFGJangedIssue.GetData(Val.DBDate(dtpFromDate.Text), Val.DBDate(dtpToDate.Text));

                if (m_dtbDetails.Rows.Count == 0)
                {
                    Global.Message("Data Not Found");
                    blnReturn = false;
                }
                grdJangedList.DataSource = m_dtbDetails;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            finally
            {
                objMFGJangedIssue = null;
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
                            grvJangedIssueAssortment.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            grvJangedIssueAssortment.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            grvJangedIssueAssortment.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            grvJangedIssueAssortment.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            grvJangedIssueAssortment.ExportToText(Filepath);
                            break;
                        case "html":
                            grvJangedIssueAssortment.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            grvJangedIssueAssortment.ExportToCsv(Filepath);
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
                if (lueProcess.Text == "")
                {
                    lstError.Add(new ListError(13, "Process"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueProcess.Focus();
                    }
                }
                //if (lueParty.ItemIndex < 0)
                //{
                //    lstError.Add(new ListError(13, "Party"));
                //    if (!blnFocus)
                //    {
                //        blnFocus = true;
                //        lueParty.Focus();
                //    }
                //}
                if (lueSubProcess.Text == "")
                {
                    lstError.Add(new ListError(13, "Sub Process"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueSubProcess.Focus();
                    }
                }
                //if (lueManager.ItemIndex < 0)
                //{
                //    lstError.Add(new ListError(13, "To Manger"));
                //    if (!blnFocus)
                //    {
                //        blnFocus = true;
                //        lueManager.Focus();
                //    }
                //}
            }

            catch (Exception ex)
            {
                lstError.Add(new ListError(ex));
            }
            return (!(BLL.General.ShowErrors(lstError)));
        }
        public void GetPendingStock()
        {
            try
            {
                if (Save_Validate())
                {
                    MFGJangedIsuRecAssortment objMFGJangedIsuRec = new MFGJangedIsuRecAssortment();
                    MFGProcessIssueProperty objMFGProcessIssueProperty = new MFGProcessIssueProperty();
                    objMFGProcessIssueProperty.rough_cut_id = Val.ToInt(lueCutNo.EditValue);
                    objMFGProcessIssueProperty.kapan_id = Val.ToInt(lueKapan.EditValue);
                    objMFGProcessIssueProperty.flag = Val.ToInt(0);
                    objMFGProcessIssueProperty.location_id = Val.ToInt32(GlobalDec.gEmployeeProperty.location_id);

                    DtAssortment = objMFGJangedIsuRec.GetPendingStock(objMFGProcessIssueProperty);

                    FrmMFGAssortmentStock FrmAssortmentStock = new FrmMFGAssortmentStock();
                    FrmAssortmentStock.FrmMFGJangedIssueAssort = this;
                    FrmAssortmentStock.DTab = DtAssortment;
                    FrmAssortmentStock.ShowForm(this);
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
            }
        }
        public void GetStockData(DataTable Stock_Data)
        {
            try
            {
                m_dtbIssueProcess.Rows.Clear();
                m_dtbIssueProcess.AcceptChanges();
                grdJangedIssueAssortment.DataSource = null;

                m_dtbIssueProcess = Stock_Data.Copy();

                lblLotSRNo.Text = "0";

                if (m_dtbIssueProcess.Rows.Count > 0)
                {
                    temp_purity_name = Val.ToString(m_dtbIssueProcess.Rows[0]["temp_quality_name"]);
                    temp_sieve_name = Val.ToString(m_dtbIssueProcess.Rows[0]["temp_sieve_name"]);
                    txtMinus2Amt.Text = Val.ToInt(m_dtbIssueProcess.Rows[0]["minus2_amt"]).ToString();
                    txtPlus2Amt.Text = Val.ToInt(m_dtbIssueProcess.Rows[0]["plus2_amt"]).ToString();
                }
                grdJangedIssueAssortment.DataSource = m_dtbIssueProcess;
                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
            }
        }

        public void GetStockData_New(DataTable Stock_Data)
        {
            try
            {
                m_dtbIssueProcess.Rows.Clear();
                m_dtbIssueProcess.AcceptChanges();
                grdJangedIssueAssortment.DataSource = null;

                m_dtbIssueProcess = Stock_Data.Copy();

                lblLotSRNo.Text = Val.ToString(Val.ToDecimal(m_dtbIssueProcess.Rows[0]["lot_srno"]));
                lueKapan.EditValue = Val.ToInt64(m_dtbIssueProcess.Rows[0]["kapan_id"]);
                lueCutNo.EditValue = Val.ToInt64(m_dtbIssueProcess.Rows[0]["rough_cut_id"]);
                dtpIssueDate.Text = Val.DBDate(m_dtbIssueProcess.Rows[0]["receive_date"].ToString());

                m_dtbIssueProcess = objAssortSecond.AssortJangedGetData(Val.ToInt64(lblLotSRNo.Text), Val.ToInt64(lueKapan.EditValue), Val.ToInt64(lueCutNo.EditValue));

                if (m_dtbIssueProcess.Rows.Count > 0)
                {
                    temp_purity_name = Val.ToString(m_dtbIssueProcess.Rows[0]["temp_quality_name"]);
                    temp_sieve_name = Val.ToString(m_dtbIssueProcess.Rows[0]["temp_sieve_name"]);
                    txtMinus2Amt.Text = Val.ToInt(m_dtbIssueProcess.Rows[0]["minus2_amt"]).ToString();
                    txtPlus2Amt.Text = Val.ToInt(m_dtbIssueProcess.Rows[0]["plus2_amt"]).ToString();
                }
                grdJangedIssueAssortment.DataSource = m_dtbIssueProcess;
                btnSave.Enabled = false;
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
            }
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

        private void txtLotID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}

