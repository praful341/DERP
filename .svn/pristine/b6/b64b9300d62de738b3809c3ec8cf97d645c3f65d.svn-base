using BLL;
using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.PropertyClasses.Transaction.MFG;
using DERP.Class;
using DERP.Master;
using DERP.Master.MFG;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Transaction.MFG
{
    public partial class FrmMFGShineReceive : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        FormEvents objBOFormEvents = new FormEvents();
        Validation Val = new Validation();
        BLL.FormPer ObjPer = new BLL.FormPer();
        DataTable m_dtbDetail = new DataTable();
        DataTable m_dtbParam = new DataTable();
        DataTable m_dtbSubProcess = new DataTable();
        BLL.BeginTranConnection Conn;
        DataTable m_dtbLotSplitProcess = new DataTable();
        Control _NextEnteredControl = new Control();
        DataTable DtControlSettings = new DataTable();
        MFGLotSplit objLotSplitReceive = new MFGLotSplit();
        DataTable m_dtOutstanding = new DataTable();
        DataTable m_DtProcess;
        DataTable m_dtbKapan = new DataTable();
        MFGProcessReceive objProcessReceive = new MFGProcessReceive();
        private List<Control> _tabControls = new List<Control>();
        DataTable dtShineIssueRate = new DataTable();
        MFGMixSplit objMFGMixSplit = new MFGMixSplit();
        //DataTable DTab_KapanWiseData;

        IDataObject PasteclipData = Clipboard.GetDataObject();
        String PasteData = "";


        bool m_blnadd = new bool();
        bool m_blnsave = new bool();
        int m_Srno = 1;
        int m_kapan_id = 0;
        decimal m_numSummRate = 0;
        decimal m_balcarat = 0;
        int m_issue_id;
        int m_manager_id;
        int m_emp_id;
        string m_process = "";
        decimal m_OsCarat;
        int m_numForm_id = 0;
        bool m_blnflag = new bool();
        Int64 IntRes;
        Int64 Receive_IntRes;
        Int64 Issue_IntRes;
        Int64 MixSplit_IntRes;

        #endregion

        #region Constructor
        public FrmMFGShineReceive()
        {
            InitializeComponent();
            //DTab_KapanWiseData = new DataTable();
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
                    if (m_dtbLotSplitProcess.Rows.Count == 0)
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
                    if (Val.ToString(m_process) != "SHINE ISSUE")
                    {
                        lstError.Add(new ListError(5, "Lot Not Issue in Charni process"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtLotID.Focus();
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
        private void RepDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (Global.Confirm("Are you sure delete selected row?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                dgvShineReceive.DeleteRow(dgvShineReceive.GetRowHandle(dgvShineReceive.FocusedRowHandle));
                DTab_StockData.AcceptChanges();
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
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            Global.Export("xlsx", dgvShineReceive);
        }

        DataTable dtIssOS = new DataTable();
        private void lueCutNo_EditValueChanged(object sender, EventArgs e)
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
                                txtLotID.Text = Val.ToString(dr[0]["lot_id"]);
                                m_DtProcess = objProcessReceive.GetIssueProcess(Val.ToInt(dr[0]["lot_id"]), "CHARNI");
                                if (m_DtProcess.Rows.Count > 0)
                                {
                                    MFGProcessReceive objProcessRecieve = new MFGProcessReceive();
                                    if (txtLotID.Text != string.Empty || Val.ToInt64(txtLotID.Text) != 0)
                                    {
                                        GetOsCarat(Val.ToInt64(txtLotID.Text));

                                        m_process = Val.ToString(m_DtProcess.Rows[0]["process"]);

                                        DataTable DTab_Process = objProcessRecieve.MFG_ProcessName_GetData(Val.ToInt64(txtLotID.Text));
                                    }
                                }
                            }
                        }
                    }
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
                    dtIss = objProcessReceive.GetIssueID(Val.ToInt64(txtLotID.Text), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue));
                    if (dtIss.Rows.Count > 0)
                    {
                        m_manager_id = Val.ToInt(dtIss.Rows[0]["manager_id"]);
                        m_emp_id = Val.ToInt(dtIss.Rows[0]["employee_id"]);
                        m_dtOutstanding = objProcessReceive.Carat_OutStanding_GetData(Val.ToInt64(txtLotID.Text), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue), 1, "R");
                        if (m_dtOutstanding.Rows.Count > 0)
                        {
                            m_OsCarat = Val.ToInt32(m_dtOutstanding.Rows[0]["carat"]);
                            m_balcarat = Val.ToDecimal(dtIssOS.Rows[0]["carat"]);
                        }
                        else
                        {
                            m_balcarat = 0;
                        }
                    }
                    else
                    {
                        Global.Message("Lot not issue in this process.");
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }

        private void FrmMFGLotSplit_Load(object sender, EventArgs e)
        {
            try
            {
                Global.LOOKUPProcess(lueProcess);
                Global.LOOKUPSubProcess(lueSubProcess);

                m_dtbSubProcess = (((DataTable)lueSubProcess.Properties.DataSource).Copy());

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

                // Add By Praful On 29072021

                //DTab_KapanWiseData = Global.GetRoughStockWise(Val.ToInt(0), Val.ToInt32(0));

                // End By Praful On 29072021

                ClearDetails();
                lueCutNo.Focus();
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }

        private void lueSubProcess_EditValueChanged(object sender, EventArgs e)
        {
            if (lueProcess.EditValue != System.DBNull.Value && lueSubProcess.EditValue != System.DBNull.Value && Val.ToInt64(txtLotID.Text) != 0)
            {
                DataTable dtIss = new DataTable();
                dtIss = objProcessReceive.GetIssueID(Val.ToInt64(txtLotID.Text), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue));
                if (dtIss.Rows.Count > 0)
                {
                    m_manager_id = Val.ToInt(dtIss.Rows[0]["manager_id"]);
                    m_emp_id = Val.ToInt(dtIss.Rows[0]["employee_id"]);
                    m_issue_id = Val.ToInt(dtIss.Rows[0]["issue_id"]);
                    m_dtOutstanding = objProcessReceive.Carat_OutStanding_GetData(Val.ToInt64(txtLotID.Text), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue), 1, "R");
                    if (m_dtOutstanding.Rows.Count > 0)
                    {
                        m_balcarat = Val.ToDecimal(m_dtOutstanding.Rows[0]["carat"]);
                    }
                    else
                    {
                        m_balcarat = 0;
                        m_OsCarat = 0;
                    }
                }
                else
                {
                    Global.Message("Lot not issue in this process.");
                }
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

        private void grdShineReceive_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                if (Global.Confirm("Are you sure delete selected row?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    dgvShineReceive.DeleteRow(dgvShineReceive.GetRowHandle(dgvShineReceive.FocusedRowHandle));
                }
            }
        }

        private void backgroundWorker_LotSplit_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                MFGShineReceive objShineReceive = new MFGShineReceive();
                MFGShineReceiveProperty objShineReceiveProperty = new MFGShineReceiveProperty();
                Conn = new BeginTranConnection(true, false);

                try
                {
                    IntRes = 0;
                    Receive_IntRes = 0;
                    Issue_IntRes = 0;
                    MixSplit_IntRes = 0;

                    foreach (DataRow drw in m_dtbLotSplitProcess.Rows)
                    {
                        objShineReceiveProperty.receive_date = Val.DBDate(dtpReceiveDate.Text);
                        objShineReceiveProperty.rough_lot_id = Val.ToInt64(txtLotID.Text);
                        objShineReceiveProperty.rough_cut_id = Val.ToInt(drw["rough_cut_id"]);
                        objShineReceiveProperty.kapan_id = Val.ToInt(drw["kapan_id"]);
                        objShineReceiveProperty.quality_id = Val.ToInt(drw["quality_id"]);
                        objShineReceiveProperty.rough_sieve_id = Val.ToInt64(drw["rough_sieve_id"]);
                        objShineReceiveProperty.rough_clarity_id = Val.ToInt(drw["rough_clarity_id"]);
                        objShineReceiveProperty.process_id = Val.ToInt(16);
                        objShineReceiveProperty.sub_process_id = Val.ToInt(1017);
                        objShineReceiveProperty.form_id = Val.ToInt(m_numForm_id);

                        objShineReceiveProperty.pcs = Val.ToInt(drw["balance_pcs"]);
                        objShineReceiveProperty.carat = Val.ToDecimal(drw["balance_carat"]);
                        objShineReceiveProperty.loss_carat = Val.ToDecimal(0);
                        objShineReceiveProperty.plus_carat = Val.ToDecimal(0);
                        objShineReceiveProperty.rate = Val.ToDecimal(drw["rate"]);
                        objShineReceiveProperty.amount = Val.ToDecimal(drw["amount"]);
                        objShineReceiveProperty.union_id = IntRes;
                        objShineReceiveProperty.receive_union_id = Receive_IntRes;
                        objShineReceiveProperty.issue_union_id = Issue_IntRes;
                        objShineReceiveProperty.mix_union_id = MixSplit_IntRes;

                        objShineReceiveProperty = objShineReceive.Save(objShineReceiveProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                        IntRes = objShineReceiveProperty.union_id;
                        Receive_IntRes = objShineReceiveProperty.receive_union_id;
                        Issue_IntRes = objShineReceiveProperty.issue_union_id;
                        MixSplit_IntRes = objShineReceiveProperty.mix_union_id;
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
                            Global.Message("Lot ID already added to the Issue list!");
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

                if (DTab_StockData.Rows.Count > 0)
                {
                    DataTable DTabTemp = new DataTable();
                    DTabTemp = objMFGMixSplit.Shine_Issue_GetData(Val.Trim(txtLotID.Text));

                    if (DTabTemp.Rows.Count > 0)
                    {
                        txtLotID.Text = "";
                        txtLotID.Focus();
                    }
                    else
                    {
                        Global.Message("Lot ID Not found");
                        txtLotID.Text = "";
                        txtLotID.Focus();
                        return;
                    }
                    DTab_StockData.Merge(DTabTemp);
                }
                else
                {
                    DTab_StockData = objMFGMixSplit.Shine_Issue_GetData(Val.Trim(txtLotID.Text));

                    if (DTab_StockData.Rows.Count > 0)
                    {
                        lueKapan.EditValue = Val.ToInt64(DTab_StockData.Rows[0]["kapan_id"]);
                        lueCutNo.EditValue = Val.ToInt64(DTab_StockData.Rows[0]["rough_cut_id"]);
                        txtLotID.Text = "";
                        txtLotID.Focus();
                    }
                    else
                    {
                        Global.Message("Lot ID Not found");
                        txtLotID.Text = "";
                        txtLotID.Focus();
                        return;
                    }
                }

                grdShineReceive.DataSource = DTab_StockData;
                grdShineReceive.RefreshDataSource();
                dgvShineReceive.BestFitColumns();
                CalculateSummary();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }

            //try
            //{
            //    if (m_dtbParam.Rows.Count > 0)
            //    {
            //        if (Val.ToString(txtLotID.Text) != "")
            //        {
            //            DataRow[] dr = m_dtbParam.Select("lot_id =" + Val.ToInt64(txtLotID.Text));
            //            //if(dr.Count ==1)
            //            //{ }
            //            //lueCutNo.Text = Val.ToString(dr[0]["rough_cut_no"]);
            //            //prdId = Val.ToInt(dr[0]["prediction_id"]);
            //            if (txtLotID.Text != string.Empty || Val.ToInt64(txtLotID.Text) != 0)
            //            {
            //                GetOsCarat(Val.ToInt64(txtLotID.Text));
            //                m_DtProcess = objProcessReceive.GetIssueProcess(Val.ToInt64(txtLotID.Text), "SHINE ISSUE");
            //                if (m_DtProcess.Rows.Count > 0)
            //                {
            //                    MFGProcessIssue objMFGProcessIssue = new MFGProcessIssue();
            //                    m_process = Val.ToString(m_DtProcess.Rows[0]["process"]);

            //                    m_blnadd = true;
            //                    m_blnsave = false;
            //                    if (!ValidateDetails())
            //                    {
            //                        return;
            //                    }

            //                    m_blnflag = true;
            //                    DataTable dtIssOS = new DataTable();
            //                    MFGProcessReceive objProcessRecieve = new MFGProcessReceive();
            //                    dtIssOS = objProcessRecieve.Carat_OutStanding_GetData(Val.ToInt64(txtLotID.Text), Val.ToInt(lueProcess.EditValue), Val.ToInt(lueSubProcess.EditValue), 1, "R");
            //                    dtShineIssueRate = objMFGProcessIssue.GetAssortRate(Val.ToInt64(txtLotID.Text), 1);
            //                }
            //                else
            //                {
            //                    // txtBalanceCarat.Text = "0";
            //                }


            //                if (lueProcess.Text != "")
            //                {
            //                    AddInGrid();
            //                    txtLotID.Text = "";
            //                    txtLotID.Focus();
            //                }
            //                // txtBalancePcs.Text = "0";
            //            }
            //            else
            //            {
            //                BLL.General.ShowErrors("Cut No not Found");
            //                //txtPcs.Text = "0";
            //                //txtBalanceCarat.Text = "0";
            //                lueCutNo.EditValue = System.DBNull.Value;
            //            }
            //        }
            //    }

        }
        int IntTotalLot = 0;
        double DblTotalCarat = 0.00;
        int IntTotalPcs = 0;
        private void CalculateSummary()
        {
            dgvShineReceive.PostEditor();
            dgvShineReceive.RefreshData();
            DTab_StockData.AcceptChanges();

            foreach (DataRow DRow in DTab_StockData.Rows)
            {
                IntTotalLot++;
                IntTotalPcs += Val.ToInt(DRow["balance_pcs"]);
                DblTotalCarat += Val.Val(DRow["balance_carat"]);
            }
        }

        private void dgvShineReceive_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
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
        #endregion

        #region Function       
        private bool GenerateLotSplitDetails()
        {
            bool blnReturn = true;
            try
            {
                if (m_dtbLotSplitProcess.Rows.Count > 0)
                    m_dtbLotSplitProcess.Rows.Clear();

                m_dtbLotSplitProcess = new DataTable();

                m_dtbLotSplitProcess.Columns.Add("recieve_id", typeof(int));
                m_dtbLotSplitProcess.Columns.Add("recieve_date", typeof(DateTime));
                m_dtbLotSplitProcess.Columns.Add("lot_id", typeof(int));
                m_dtbLotSplitProcess.Columns.Add("cut_no", typeof(string));
                m_dtbLotSplitProcess.Columns.Add("sieve_name", typeof(string));
                m_dtbLotSplitProcess.Columns.Add("rough_sieve_id", typeof(int));
                m_dtbLotSplitProcess.Columns.Add("rough_clarity_name", typeof(string));
                m_dtbLotSplitProcess.Columns.Add("rough_clarity_id", typeof(int));
                m_dtbLotSplitProcess.Columns.Add("quality_name", typeof(string));
                m_dtbLotSplitProcess.Columns.Add("quality_id", typeof(int));
                m_dtbLotSplitProcess.Columns.Add("process_name", typeof(string));
                m_dtbLotSplitProcess.Columns.Add("process_id", typeof(int));
                m_dtbLotSplitProcess.Columns.Add("sub_process_name", typeof(string));
                m_dtbLotSplitProcess.Columns.Add("sub_process_Id", typeof(int));
                m_dtbLotSplitProcess.Columns.Add("pcs", typeof(int)).DefaultValue = 0;
                m_dtbLotSplitProcess.Columns.Add("carat", typeof(decimal)).DefaultValue = 0;
                m_dtbLotSplitProcess.Columns.Add("rate", typeof(decimal)).DefaultValue = 0;
                m_dtbLotSplitProcess.Columns.Add("amount", typeof(decimal)).DefaultValue = 0;

                grdShineReceive.DataSource = m_dtbLotSplitProcess;
                grdShineReceive.Refresh();
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
                DataRow drwNew = m_dtbLotSplitProcess.NewRow();

                drwNew["recieve_id"] = Val.ToInt(0);
                drwNew["recieve_date"] = Val.DBDate(dtpReceiveDate.Text);
                drwNew["cut_no"] = Val.ToString(lueCutNo.Text);
                drwNew["lot_id"] = Val.ToInt64(txtLotID.Text);
                drwNew["sr_no"] = m_Srno;
                m_dtbLotSplitProcess.Rows.Add(drwNew);
                m_Srno++;

                dgvShineReceive.MoveLast();
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
                lueProcess.EditValue = System.DBNull.Value;
                lueSubProcess.EditValue = System.DBNull.Value;
                txtLotID.Text = string.Empty;
                m_Srno = 1;
                lueCutNo.Focus();
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
                if (lotId > 0)
                {
                    m_dtOutstanding.Rows.Clear();
                    m_dtOutstanding = objLotSplitReceive.GetBalanceCarat(lotId);
                }
                if (m_dtOutstanding.Rows.Count > 0)
                {
                    lueCutNo.EditValue = Val.ToInt64(m_dtOutstanding.Rows[0]["rough_cut_id"]);
                    m_kapan_id = Val.ToInt(m_dtOutstanding.Rows[0]["kapan_id"]);
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
                            dgvShineReceive.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvShineReceive.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvShineReceive.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvShineReceive.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvShineReceive.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvShineReceive.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvShineReceive.ExportToCsv(Filepath);
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
    }
}
