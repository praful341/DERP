using BLL;
using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.PropertyClasses.Transaction;
using BLL.PropertyClasses.Transaction.MFG;
using DERP.Class;
using DERP.Master;
using DERP.Report;
using DevExpress.XtraEditors;
using DREP.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Transaction.MFG
{
    public partial class FrmMFGDepartmentTransfer : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        Control _NextEnteredControl;
        private List<Control> _tabControls;

        MFGDepartmentTransfer objDepartmentTransfer;
        MFGSawableRecieve objSawableRecieve;
        MFGJangedReturn objJangedReturn = new MFGJangedReturn();
        MFGProcessIssue objMFGProcessIssue = new MFGProcessIssue();
        MFGProcessIssueProperty objMFGProcessIssueProperty = new MFGProcessIssueProperty();

        DataTable DtControlSettings;
        DataTable m_dtbDetail;
        DataTable m_dtbParam;
        DataTable m_dtbSubProcess;
        DataTable m_dtbDepartmentTransfer;
        DataTable m_dtOutstanding;
        DataTable DTab_LotId = new DataTable();
        DataTable m_dtbKapan;
        DataTable DtPending = new DataTable();
        DataTable DTabTemp = new DataTable();
        //DataTable DTab_KapanWiseData = new DataTable();

        int from_manager;
        int from_dept;
        int prdId;
        int m_Srno;
        int kapan_id = 0;
        Int64 m_numForm_id;
        Int64 IntRes;
        Int64 Lot_SrNo;

        decimal m_balcarat;
        decimal m_rrcarat;

        bool m_blnadd;
        bool m_blnsave;

        #endregion

        #region Constructor
        public FrmMFGDepartmentTransfer()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            _NextEnteredControl = new Control();
            _tabControls = new List<Control>();

            objDepartmentTransfer = new MFGDepartmentTransfer();
            objSawableRecieve = new MFGSawableRecieve();

            DtControlSettings = new DataTable();
            m_dtbDetail = new DataTable();
            m_dtbParam = new DataTable();
            m_dtbSubProcess = new DataTable();
            m_dtbDepartmentTransfer = new DataTable();
            m_dtOutstanding = new DataTable();
            m_dtbKapan = new DataTable();

            from_manager = 0;
            from_dept = 0;
            prdId = 0;
            m_Srno = 1;
            m_numForm_id = 0;


            m_balcarat = 0;
            m_rrcarat = 0;

            m_blnadd = new bool();
            m_blnsave = new bool();
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

        #region Events
        private void RepDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (Global.Confirm("Are you sure delete selected row?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                dgvDepartmentTransfer.DeleteRow(dgvDepartmentTransfer.GetRowHandle(dgvDepartmentTransfer.FocusedRowHandle));
                m_dtbDepartmentTransfer.AcceptChanges();
            }
        }
        private void FrmMFGProcessIssue_Load(object sender, EventArgs e)
        {
            try
            {
                //Global.LOOKUPDepartment(lueToDepartment);
                Global.LOOKUPDepartment_New(lueToDepartment);
                Global.LOOKUPAllManager(lueManager);
                Global.LOOKUPProcess(lueProcess);

                dtpDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpDate.EditValue = DateTime.Now;

                m_dtbKapan = Global.GetKapanAll();

                lueKapan.Properties.DataSource = m_dtbKapan;
                lueKapan.Properties.ValueMember = "kapan_id";
                lueKapan.Properties.DisplayMember = "kapan_no";

                m_dtbParam = Global.GetRoughCutAll();

                lueCutNo.Properties.DataSource = m_dtbParam;
                lueCutNo.Properties.ValueMember = "rough_cut_id";
                lueCutNo.Properties.DisplayMember = "rough_cut_no";

                //MFGProcessIssue objMFGProcessIssue = new MFGProcessIssue();
                //MFGProcessIssueProperty objMFGProcessIssueProperty = new MFGProcessIssueProperty();

                DtPending = objMFGProcessIssue.GetPendingStock(objMFGProcessIssueProperty);

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
        private void lueProcess_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmProcessMaster frmProcess = new FrmProcessMaster();
                frmProcess.ShowDialog();
                Global.LOOKUPProcess(lueProcess);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (AddInGrid())
                {
                    txtLotID.Text = string.Empty;
                    //lueCutNo.EditValue = System.DBNull.Value;
                    lueLotId.EditValue = System.DBNull.Value;
                    txtPcs.Text = string.Empty;
                    prdId = 0;
                    m_balcarat = 0;
                    txtCarat.Text = string.Empty;
                    lueLotId.Focus();
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
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
                if (Val.DBDate(BLL.GlobalDec.gStrServerDate) != Val.DBDate(dtpDate.Text))
                {
                    Str = GlobalDec.CheckLockIsOpenOrNot(Val.DBDate(dtpDate.Text), Val.DBTime(DateTime.Now.ToShortTimeString())); //Val.GetFullTime12());
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
                        dtpDate.Enabled = true;
                        dtpDate.Visible = true;
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
                if (Val.ToString(GlobalDec.gEmployeeProperty.department_name) == "SARIN" || Val.ToString(GlobalDec.gEmployeeProperty.department_name) == "4P")
                {
                    m_dtbDepartmentTransfer = (DataTable)grdDepartmentTransfer.DataSource;
                    foreach (DataRow drw in m_dtbDepartmentTransfer.Rows)
                    {
                        if (Val.ToString(GlobalDec.gEmployeeProperty.department_name) == "SARIN")
                        {
                            DataTable DT = objDepartmentTransfer.GetDeptISSREC(Val.ToInt(drw["lot_id"]), "SARIN");
                            if (DT.Rows.Count == 0)
                            {
                                Global.Message("This lot ID" + Val.ToInt(drw["lot_id"]) + " is not Completed Sarin Process so please check..");
                                lueLotId.Focus();
                                btnSave.Enabled = true;
                                return;
                            }

                        }
                        if (Val.ToString(GlobalDec.gEmployeeProperty.department_name) == "4P")
                        {
                            DataTable DT = objDepartmentTransfer.GetDeptISSREC(Val.ToInt(drw["lot_id"]), "4P");
                            if (DT.Rows.Count == 0)
                            {
                                Global.Message("This lot ID is not Completed 4P Process so please check..");
                                lueLotId.Focus();
                                btnSave.Enabled = true;
                                return;
                            }

                        }
                    }
                }
                DialogResult result = MessageBox.Show("Do you want to save Department Transfer data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result != DialogResult.Yes)
                {
                    btnSave.Enabled = true;
                    return;
                }

                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;

                panelProgress.Visible = true;
                backgroundWorker_DeptTransfer.RunWorkerAsync();

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
            //grdProcessWeightLossRecieve.DataSource = null;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            Global.Export("xlsx", dgvDepartmentTransfer);
        }
        private void txtLotID_Validated(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToInt64(txtLotID.Text) > 0)
                {
                    //MFGProcessIssue objMFGProcessIssue = new MFGProcessIssue();
                    //MFGProcessIssueProperty objMFGProcessIssueProperty = new MFGProcessIssueProperty();

                    //DtPending = objMFGProcessIssue.GetPendingStock(objMFGProcessIssueProperty);
                    if (DtPending.Select("lot_id in(" + Val.ToInt64(txtLotID.Text) + ")").Length > 0)
                    {
                        DTab_LotId = DtPending.Select("lot_id in(" + Val.ToInt64(txtLotID.Text) + ")").CopyToDataTable();


                        if (DTab_LotId.Rows.Count > 0)
                        {
                            kapan_id = Val.ToInt(DTab_LotId.Rows[0]["kapan_id"]);

                            DataRow[] dr = DTab_LotId.Select("lot_id = " + Val.ToInt64(txtLotID.Text));

                            if (dr.Length > 0)
                            {
                                txtPcs.Text = Val.ToInt(dr[0]["pcs"]).ToString();
                                txtCarat.Text = Val.ToDecimal(dr[0]["carat"]).ToString();
                                btnAdd_Click(null, null);
                                txtLotID.Focus();
                            }
                            else
                            {
                                Global.Message("Lot Not Found.");
                            }
                        }
                    }
                    else
                    {
                        Global.Message("Lot Not Found.");
                    }

                }

            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
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
        private void lueCutNo_EditValueChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    try
            //    {
            //        if (Val.ToInt(lueCutNo.EditValue) > 0)
            //        {
            //            //DTab_LotId = objDepartmentTransfer.GetDataLotID(Val.ToInt(lueCutNo.EditValue));

            //            MFGProcessIssue objMFGProcessIssue = new MFGProcessIssue();
            //            MFGProcessIssueProperty objMFGProcessIssueProperty = new MFGProcessIssueProperty();
            //            objMFGProcessIssueProperty.rough_cut_id = Val.ToInt(lueCutNo.EditValue);
            //            objMFGProcessIssueProperty.kapan_id = Val.ToInt(lueKapan.EditValue);

            //            DtPending = objMFGProcessIssue.GetPendingStock(objMFGProcessIssueProperty);

            //        }
            //        if (DtPending.Rows.Count > 0)
            //        {
            //            lueLotId.Properties.DataSource = DtPending;
            //            lueLotId.Properties.DisplayMember = "lot_id";
            //            lueLotId.Properties.ValueMember = "lot_id";
            //        }
            //        else
            //        {
            //            Global.Message("Lot ID Not Found in this Cut No =" + lueCutNo.Text);
            //            lueLotId.EditValue = null;
            //            return;
            //        }

            //        if (Val.ToInt(lueToDepartment.EditValue) > 0 && Val.ToInt(lueManager.EditValue) > 0 && Val.ToInt(lueProcess.EditValue) > 0)
            //        {
            //            btnAdd.Enabled = true;
            //            btnPopUpStock.Enabled = true;
            //        }
            //        else
            //        {
            //            btnAdd.Enabled = false;
            //            btnPopUpStock.Enabled = false;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Global.Message(ex.ToString());
            //        return;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Global.Message(ex.ToString());
            //    return;
            //}
        }
        private void lueLotId_EditValueChanged(object sender, EventArgs e)
        {
            if (Val.ToInt(lueLotId.EditValue) > 0)
            {
                DTab_LotId = DtPending.Select("lot_id in(" + Val.ToInt(lueLotId.EditValue) + ")").CopyToDataTable();

                kapan_id = Val.ToInt(DTab_LotId.Rows[0]["kapan_id"]);


                DataRow[] dr = DTab_LotId.Select("lot_id = " + Val.ToInt(lueLotId.EditValue));

                if (dr.Length > 0)
                {
                    txtPcs.Text = Val.ToInt(dr[0]["pcs"]).ToString();
                    txtCarat.Text = Val.ToDecimal(dr[0]["carat"]).ToString();
                }
            }
        }
        private void grdDepartmentTransfer_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.F9)
            //{
            //    if (Global.Confirm("Are you sure delete selected row?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            //    {
            //        dgvDepartmentTransfer.DeleteRow(dgvDepartmentTransfer.GetRowHandle(dgvDepartmentTransfer.FocusedRowHandle));
            //    }
            //}
        }
        private void btnPopUpStock_Click(object sender, EventArgs e)
        {
            GetPendingStock();
        }
        private void lueToDepartment_EditValueChanged(object sender, EventArgs e)
        {
            if (Val.ToInt(lueToDepartment.EditValue) > 0 && Val.ToInt(lueManager.EditValue) > 0 && Val.ToInt(lueProcess.EditValue) > 0)
            {
                btnAdd.Enabled = true;
                btnPopUpStock.Enabled = true;
            }
            else
            {
                btnAdd.Enabled = false;
                btnPopUpStock.Enabled = false;
            }
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
        private void lueToDepartment_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmDepartmentMaster frmDepartment = new FrmDepartmentMaster();
                frmDepartment.ShowDialog();
                Global.LOOKUPDepartment_New(lueToDepartment);
            }
        }
        private void backgroundWorker_DeptTransfer_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                //List<ListError> lstError = new List<ListError>();

                //foreach (DataRow drw in m_dtbDepartmentTransfer.Rows)
                //{
                //    rowNo = 1;
                //    if (Val.ToInt(drw["from_manager_id"]) == Val.ToInt(lueManager.EditValue))
                //    {
                //        lstError.Add(new ListError("Same Manager Please Check Row " + rowNo));
                //        m_deptCheck = 1;
                //        break;
                //        return;
                //    }
                //    else
                //    {
                //        m_deptCheck = 0;
                //    }
                //}
                m_dtbDepartmentTransfer.AcceptChanges();
                m_dtbDepartmentTransfer = (DataTable)grdDepartmentTransfer.DataSource;
                MFGDepartmentTransfer objMFGDepartmentTransfer = new MFGDepartmentTransfer();
                MFGDepartmentTransferProperty objMFGDepartmentTransferProperty = new MFGDepartmentTransferProperty();
                Conn = new BeginTranConnection(true, false);
                IntRes = 0;
                Int64 NewHistory_Union_Id = 0;
                Lot_SrNo = 0;

                int IntCounter = 0;
                int Count = 0;
                int TotalCount = m_dtbDepartmentTransfer.Rows.Count;
                try
                {
                    foreach (DataRow drw in m_dtbDepartmentTransfer.Rows)
                    {
                        objMFGDepartmentTransferProperty.union_id = IntRes;
                        objMFGDepartmentTransferProperty.transfer_date = Val.DBDate(dtpDate.Text);
                        objMFGDepartmentTransferProperty.lot_id = Val.ToInt(drw["lot_id"]);
                        objMFGDepartmentTransferProperty.cut_id = Val.ToInt(drw["rough_cut_id"]);
                        objMFGDepartmentTransferProperty.pcs = Val.ToInt(drw["pcs"]);
                        objMFGDepartmentTransferProperty.carat = Val.ToDecimal(drw["carat"]);
                        objMFGDepartmentTransferProperty.rr_pcs = Val.ToInt(0);
                        objMFGDepartmentTransferProperty.rr_carat = Val.ToDecimal(0);
                        //objMFGDepartmentTransferProperty.from_department_id = Val.ToInt(drw["from_department_id"]);
                        //objMFGDepartmentTransferProperty.from_manager_id = Val.ToInt(drw["from_manager_id"]);
                        objMFGDepartmentTransferProperty.to_department_id = Val.ToInt(lueToDepartment.EditValue);
                        objMFGDepartmentTransferProperty.to_manager_id = Val.ToInt64(lueManager.EditValue);
                        objMFGDepartmentTransferProperty.kapan_id = Val.ToInt(drw["kapan_id"]);
                        objMFGDepartmentTransferProperty.to_process_id = Val.ToInt(lueProcess.EditValue);
                        objMFGDepartmentTransferProperty.history_union_id = NewHistory_Union_Id;
                        objMFGDepartmentTransferProperty.form_id = m_numForm_id;
                        objMFGDepartmentTransferProperty.lot_srno = Lot_SrNo;

                        objMFGDepartmentTransferProperty = objMFGDepartmentTransfer.Save(objMFGDepartmentTransferProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                        IntRes = objMFGDepartmentTransferProperty.union_id;
                        NewHistory_Union_Id = Val.ToInt64(objMFGDepartmentTransferProperty.history_union_id);
                        Lot_SrNo = Val.ToInt64(objMFGDepartmentTransferProperty.lot_srno);

                        Count++;
                        IntCounter++;
                        IntRes++;
                        SetControlPropertyValue(lblProgressCount, "Text", Count.ToString() + "" + "/" + "" + TotalCount.ToString() + " Completed....");
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
        private void backgroundWorker_DeptTransfer_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                panelProgress.Visible = false;
                if (IntRes > 0)
                {
                    Global.Confirm("Department Transfer Save Succesfully");
                    grdDepartmentTransfer.DataSource = null;
                    btnSave.Enabled = true;
                    lueCutNo.EditValue = System.DBNull.Value;
                    //ClearDetails();

                    if (Val.ToInt(lueToDepartment.EditValue) == 1004 || Val.ToInt(lueToDepartment.EditValue) == 1005 || Val.ToInt(lueToDepartment.EditValue) == 1003)
                    {
                        MFGJangedIssue objMFGJangedIssue = new MFGJangedIssue();
                        DataTable DTab_DeptIssueJanged = objMFGJangedIssue.GetDepartmentDataDetails(Val.ToInt64(Lot_SrNo));

                        ClearDetails();

                        FrmReportViewer FrmReportViewer = new FrmReportViewer();
                        FrmReportViewer.DS.Tables.Add(DTab_DeptIssueJanged);
                        FrmReportViewer.GroupBy = "";
                        FrmReportViewer.RepName = "";
                        FrmReportViewer.RepPara = "";
                        this.Cursor = Cursors.Default;
                        FrmReportViewer.AllowSetFormula = true;

                        FrmReportViewer.ShowForm_SubReport("Janged_Issue_Main", 120, FrmReportViewer.ReportFolder.ACCOUNT);

                        DTab_DeptIssueJanged = null;
                        FrmReportViewer.DS.Tables.Clear();
                        FrmReportViewer.DS.Clear();
                        FrmReportViewer = null;
                        btnSave.Enabled = true;
                    }
                    else
                    {
                        ClearDetails();
                    }
                }
                else
                {
                    Global.Confirm("Error In Department transfer");
                    btnSave.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            FrmMFGDepartmentSearch FrmSearchProcess = new FrmMFGDepartmentSearch();
            FrmSearchProcess.FrmMFGDepartmentTransfer = this;
            //FrmSearchProcess.DTab = DtPending;
            FrmSearchProcess.ShowForm(this);
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            MFGJangedIssue objMFGJangedIssue = new MFGJangedIssue();
            DataTable DTab_DeptIssueJanged = objMFGJangedIssue.GetDepartmentDataDetails(Val.ToInt64(txtLotSrNo.Text));

            ClearDetails();

            FrmReportViewer FrmReportViewer = new FrmReportViewer();
            FrmReportViewer.DS.Tables.Add(DTab_DeptIssueJanged);
            FrmReportViewer.GroupBy = "";
            FrmReportViewer.RepName = "";
            FrmReportViewer.RepPara = "";
            this.Cursor = Cursors.Default;
            FrmReportViewer.AllowSetFormula = true;

            FrmReportViewer.ShowForm_SubReport("Janged_Issue_Main", 120, FrmReportViewer.ReportFolder.ACCOUNT);

            DTab_DeptIssueJanged = null;
            FrmReportViewer.DS.Tables.Clear();
            FrmReportViewer.DS.Clear();
            FrmReportViewer = null;
            btnSave.Enabled = true;
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (GlobalDec.gEmployeeProperty.user_name == "PRAGNESH" || GlobalDec.gEmployeeProperty.user_name == "JAYESH" || GlobalDec.gEmployeeProperty.user_name == "RAVIR")
                {
                    DialogResult result = MessageBox.Show("Do you want to Delete Department Transfer data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                    if (result != DialogResult.Yes)
                    {
                        return;
                    }

                    this.Cursor = Cursors.WaitCursor;
                    Int64 IntRes = 0;
                    DataTable DTab = (DataTable)grdDepartmentTransfer.DataSource;
                    if (DTab.Rows.Count > 0)
                    {
                        if (txtLotSrNo.Text.ToString() != "" && txtLotSrNo.Text.ToString() != "0")
                        {
                            MFGDepartmentTransfer objMFGDepartmentTransfer = new MFGDepartmentTransfer();
                            MFGDepartmentTransferProperty objMFGDepartmentTransferProperty = new MFGDepartmentTransferProperty();

                            for (int i = 0; i <= DTab.Rows.Count - 1; i++)
                            {
                                objMFGDepartmentTransferProperty.lot_id = Val.ToInt64(DTab.Rows[i]["lot_id"]);
                                objMFGDepartmentTransferProperty = objMFGDepartmentTransfer.Get_LotSrNo(objMFGDepartmentTransferProperty);
                                Int64 Get_Lot_SRNo = objMFGDepartmentTransferProperty.lot_srno;

                                if (Get_Lot_SRNo != Val.ToInt64(txtLotSrNo.Text))
                                {
                                    Global.Message(objMFGDepartmentTransferProperty.lot_id + " : This Lot ID is Not Last Transaction Please Check");
                                    this.Cursor = Cursors.Default;
                                    return;
                                }
                            }

                            for (int j = 0; j <= DTab.Rows.Count - 1; j++)
                            {
                                objMFGDepartmentTransferProperty.lot_id = Val.ToInt64(DTab.Rows[j]["lot_id"]);
                                objMFGDepartmentTransferProperty.lot_srno = Val.ToInt64(DTab.Rows[j]["lot_srno"]);
                                IntRes = objMFGDepartmentTransfer.Dept_Lot_ID_Delete(objMFGDepartmentTransferProperty);
                            }
                            if (IntRes > 0)
                            {
                                Global.Confirm("Department Transfer Deleted Succesfully");
                                grdDepartmentTransfer.DataSource = null;
                                btnSave.Enabled = true;
                                ClearDetails();
                                this.Cursor = Cursors.Default;
                                return;
                            }
                        }
                        else
                        {
                            Global.Message("Before Delete Please Choose Lot SrNo in Box");
                            this.Cursor = Cursors.Default;
                            btnSearch.Focus();
                            return;
                        }
                    }
                    else
                    {
                        Global.Message("Data Not Found in Grid");
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    this.Cursor = Cursors.Default;
                }
                else
                {
                    Global.Message("Don't have User Permission So..Please Contact to Administrator...");
                    return;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                IntRes = -1;
                Global.Message(ex.ToString());
                if (ex.InnerException != null)
                {
                    Global.Message(ex.InnerException.ToString());
                }
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

        #endregion

        #region Function
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
                    DataRow[] dr = null;
                    if (Val.ToInt64(txtLotID.Text) > 0)
                    {
                        dr = m_dtbDepartmentTransfer.Select("lot_id = " + Val.ToInt64(txtLotID.Text));
                    }
                    else
                    {
                        dr = m_dtbDepartmentTransfer.Select("rough_cut_id = " + Val.ToInt(lueCutNo.EditValue) + " AND lot_id = " + (Val.ToInt(lueLotId.Text)));
                    }
                    if (dr.Count() == 1)
                    {
                        Global.Message("Record already exists in grid", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lueLotId.Focus();
                        blnReturn = false;
                        return blnReturn;
                    }
                    DataRow drwNew = m_dtbDepartmentTransfer.NewRow();
                    int numPcs = Val.ToInt(txtPcs.Text);
                    decimal numCarat = Val.ToDecimal(txtCarat.Text);

                    drwNew["transfer_id"] = Val.ToInt(0);
                    //drwNew["transfer_date"] = Val.DBDate(dtpDate.Text);
                    drwNew["rough_cut_id"] = Val.ToInt64(DTab_LotId.Rows[0]["rough_cut_id"]);
                    drwNew["rough_cut_no"] = Val.ToString(DTab_LotId.Rows[0]["rough_cut_no"]);
                    drwNew["lot_id"] = Val.ToInt(lueLotId.EditValue) == 0 ? Val.ToInt64(txtLotID.Text) : Val.ToInt(lueLotId.EditValue);
                    drwNew["prediction_id"] = Val.ToInt(prdId);

                    drwNew["from_manager_id"] = Val.ToInt(from_manager);
                    drwNew["from_department_id"] = Val.ToInt(from_dept);

                    drwNew["kapan_no"] = Val.ToString(DTab_LotId.Rows[0]["kapan_no"]);
                    drwNew["kapan_id"] = Val.ToInt64(DTab_LotId.Rows[0]["kapan_id"]);
                    drwNew["employee_name"] = Val.ToString(DTab_LotId.Rows[0]["employee_name"]);
                    drwNew["employee_id"] = Val.ToInt64(DTab_LotId.Rows[0]["employee_id"]);
                    drwNew["manager_name"] = Val.ToString(DTab_LotId.Rows[0]["manager_name"]);
                    drwNew["manager_id"] = Val.ToInt64(DTab_LotId.Rows[0]["manager_id"]);
                    drwNew["process_name"] = Val.ToString(DTab_LotId.Rows[0]["process_name"]);
                    drwNew["process_id"] = Val.ToInt64(DTab_LotId.Rows[0]["process_id"]);
                    drwNew["sub_process_name"] = Val.ToString(DTab_LotId.Rows[0]["sub_process_name"]);
                    drwNew["sub_process_id"] = Val.ToInt64(DTab_LotId.Rows[0]["sub_process_id"]);
                    drwNew["quality_name"] = Val.ToString(DTab_LotId.Rows[0]["quality_name"]);
                    drwNew["quality_id"] = Val.ToInt64(DTab_LotId.Rows[0]["quality_id"]);
                    drwNew["sieve_name"] = Val.ToString(DTab_LotId.Rows[0]["sieve_name"]);
                    drwNew["rough_sieve_id"] = Val.ToInt64(DTab_LotId.Rows[0]["rough_sieve_id"]);
                    drwNew["rough_clarity_name"] = Val.ToString(DTab_LotId.Rows[0]["rough_clarity_name"]);
                    drwNew["rough_clarity_id"] = Val.ToInt64(DTab_LotId.Rows[0]["rough_clarity_id"]);
                    drwNew["purity_name"] = Val.ToString(DTab_LotId.Rows[0]["purity_name"]);
                    drwNew["purity_id"] = Val.ToInt64(DTab_LotId.Rows[0]["purity_id"]);

                    drwNew["pcs"] = numPcs;
                    drwNew["carat"] = numCarat;
                    drwNew["sr_no"] = m_Srno;
                    m_dtbDepartmentTransfer.Rows.Add(drwNew);
                    m_Srno++;
                }
                //else if (btnAdd.Text == "&Update")
                //{

                //    if (m_dtbDepartmentTransfer.Select("sr_no ='" + Val.ToInt(m_update_srno) + "'").Length > 0)
                //    {
                //        for (int i = 0; i < m_dtbDepartmentTransfer.Rows.Count; i++)
                //        {
                //            if (m_dtbDepartmentTransfer.Select("cut_no ='" + m_cut_no + "' AND sr_no = '" + m_update_srno + "'").Length > 0)
                //            {
                //                if (m_dtbDepartmentTransfer.Rows[m_update_srno - 1]["cut_no"].ToString() == m_cut_no.ToString())
                //                {
                //                    m_dtbDepartmentTransfer.Rows[m_update_srno - 1]["lot_id"] = Val.ToString(lueLotId.EditValue);
                //                    m_dtbDepartmentTransfer.Rows[m_update_srno - 1]["from_department_id"] = Val.ToInt(from_dept);
                //                    m_dtbDepartmentTransfer.Rows[m_update_srno - 1]["from_manager_id"] = Val.ToInt(from_manager);
                //                    m_dtbDepartmentTransfer.Rows[m_update_srno - 1]["pcs"] = Val.ToInt(txtPcs.Text).ToString();
                //                    m_dtbDepartmentTransfer.Rows[m_update_srno - 1]["carat"] = Val.ToDecimal(txtCarat.Text).ToString();
                //                    m_dtbDepartmentTransfer.Rows[m_update_srno - 1]["prediction_id"] = Val.ToInt(prdId);
                //                    break;
                //                }
                //            }
                //        }
                //    }
                //    btnAdd.Text = "&Add";
                //}
                dgvDepartmentTransfer.MoveLast();
                dgvDepartmentTransfer.BestFitColumns();
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
                if (m_blnsave)
                {
                    if (m_dtbDepartmentTransfer.Rows.Count == 0)
                    {
                        lstError.Add(new ListError(22, "Record"));
                        if (!blnFocus)
                        {
                            blnFocus = true;

                        }
                    }
                    DateTime endDate = Convert.ToDateTime(DateTime.Today);
                    endDate = endDate.AddDays(3);

                    if (Convert.ToDateTime(dtpDate.Text) >= endDate)
                    {
                        lstError.Add(new ListError(5, " Transfer Date Not Be Permission After 3 Days Transfer this Lot ID...Please Contact to Administrator"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            dtpDate.Focus();
                        }
                    }
                    //var result = DateTime.Compare(Convert.ToDateTime(dtpDate.Text), DateTime.Today);
                    //if (result > 0)
                    //{
                    //    lstError.Add(new ListError(5, " Recieve Date Not Be Greater Than Today Date"));
                    //    if (!blnFocus)
                    //    {
                    //        blnFocus = true;
                    //        dtpDate.Focus();
                    //    }
                    //}
                    if (Val.ToString(dtpDate.Text) == string.Empty)
                    {
                        lstError.Add(new ListError(22, "Date"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            dtpDate.Focus();
                        }
                    }

                    if (Val.ToInt(GlobalDec.gEmployeeProperty.department_id) == Val.ToInt(lueToDepartment.EditValue))
                    {
                        lstError.Add(new ListError(35, "Date"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueToDepartment.Focus();
                        }
                    }
                }

                if (m_blnadd)
                {
                    m_dtOutstanding = objDepartmentTransfer.GetDeptBalanceCarat(Val.ToInt(lueLotId.EditValue));
                    if (m_dtOutstanding.Rows.Count > 0)
                    {
                        decimal Outstanding_carat = Val.ToDecimal(m_dtOutstanding.Rows[0]["carat"]);
                        if (Outstanding_carat > 0)
                        {
                            //Global.Message("This lot ID is Outstanding so please check..");
                            lstError.Add(new ListError(5, "This lot ID is Outstanding so please check.."));
                            blnFocus = true;
                            lueLotId.Focus();
                        }
                    }

                    //if (Val.ToInt64(txtLotID.Text) == 0)
                    //{
                    //    lstError.Add(new ListError(12, "Lot No"));
                    //    if (!blnFocus)
                    //    {
                    //        blnFocus = true;
                    //        txtLotID.Focus();
                    //    }
                    //}

                    if (lueLotId.Text.Length == 0 && txtLotID.Text.Length == 0)
                    {
                        lstError.Add(new ListError(12, "Lot No"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueLotId.Focus();
                        }
                    }
                    //if (m_balcarat != (Val.ToDecimal(txtCarat.Text) - m_rrcarat))
                    //{
                    //    lstError.Add(new ListError(5, "Carat Mismatch Please Check"));
                    //    if (!blnFocus)
                    //    {
                    //        blnFocus = true;
                    //        txtCarat.Focus();
                    //    }
                    //}

                    if (lueCutNo.Text == "" && txtLotID.Text.Length == 0)
                    {
                        lstError.Add(new ListError(13, "Cut No"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueCutNo.Focus();
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
                    if (lueManager.Text == "")
                    {
                        lstError.Add(new ListError(13, "To Manger"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueManager.Focus();
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
                    if (Val.ToInt(lueManager.EditValue) == Val.ToInt(from_manager))
                    {
                        lstError.Add(new ListError(5, "Not Transfer Lot Same Manager"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueLotId.Focus();
                        }
                    }
                    //if (Val.ToInt(txtRate.Text) == 0)
                    //{
                    //    lstError.Add(new ListError(12, "Rate"));
                    //    if (!blnFocus)
                    //    {
                    //        blnFocus = true;
                    //        txtRate.Focus();
                    //    }
                    //}

                    //if (Val.ToDecimal(txtCarat.Text) > 0 && txtCarat.Text != string.Empty)
                    //{
                    //    if (Val.ToDecimal(txtRRCarat.Text) < (Val.ToDecimal(dgvSawableRecieve.Columns["carat"].SummaryText)+ Val.ToDecimal(dgvSawableRecieve.Columns["rr_carat"].SummaryText) + Val.ToDecimal(txtCarat.Text)))
                    //    {
                    //        lstError.Add(new ListError(5, "Entry Carat not greater than total carat"));
                    //        if (!blnFocus)
                    //        {
                    //            blnFocus = true;
                    //            txtCarat.Focus();
                    //        }
                    //    }
                    //}
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
                if (!GenerateDepartmentDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }

                dtpDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpDate.EditValue = DateTime.Now;

                //lueCutNo.EditValue = System.DBNull.Value;
                lueToDepartment.EditValue = System.DBNull.Value;
                lueManager.EditValue = System.DBNull.Value;
                lueProcess.EditValue = System.DBNull.Value;
                txtPcs.Text = string.Empty;
                txtCarat.Text = string.Empty;
                txtLotID.Text = string.Empty;
                lueLotId.EditValue = System.DBNull.Value;
                btnPopUpStock.Enabled = false;
                txtLotSrNo.Text = string.Empty;
                txtPassword.Text = "";

                m_Srno = 1;
                btnAdd.Text = "&Add";
                lueCutNo.Focus();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
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
        private bool GenerateDepartmentDetails()
        {
            bool blnReturn = true;
            try
            {
                if (m_dtbDepartmentTransfer.Rows.Count > 0)
                    m_dtbDepartmentTransfer.Rows.Clear();

                m_dtbDepartmentTransfer = new DataTable();

                m_dtbDepartmentTransfer.Columns.Add("transfer_id", typeof(int));
                //m_dtbDepartmentTransfer.Columns.Add("recieve_date", typeof(DateTime));
                m_dtbDepartmentTransfer.Columns.Add("lot_id", typeof(int));
                m_dtbDepartmentTransfer.Columns.Add("rough_cut_id", typeof(int));
                m_dtbDepartmentTransfer.Columns.Add("rough_cut_no", typeof(string));
                // m_dtbDepartmentTransfer.Columns.Add("purity_group", typeof(string));
                m_dtbDepartmentTransfer.Columns.Add("from_department_id", typeof(int));

                //m_dtbDepartmentTransfer.Columns.Add("purity_name", typeof(string));
                m_dtbDepartmentTransfer.Columns.Add("from_manager_id", typeof(int));
                m_dtbDepartmentTransfer.Columns.Add("prediction_id", typeof(int));

                m_dtbDepartmentTransfer.Columns.Add("kapan_no", typeof(string));
                m_dtbDepartmentTransfer.Columns.Add("kapan_id", typeof(Int64));
                m_dtbDepartmentTransfer.Columns.Add("employee_name", typeof(string));
                m_dtbDepartmentTransfer.Columns.Add("employee_id", typeof(Int64));
                m_dtbDepartmentTransfer.Columns.Add("manager_name", typeof(string));
                m_dtbDepartmentTransfer.Columns.Add("manager_id", typeof(Int64));
                m_dtbDepartmentTransfer.Columns.Add("process_name", typeof(string));
                m_dtbDepartmentTransfer.Columns.Add("process_id", typeof(Int64));
                m_dtbDepartmentTransfer.Columns.Add("sub_process_name", typeof(string));
                m_dtbDepartmentTransfer.Columns.Add("sub_process_id", typeof(Int64));
                m_dtbDepartmentTransfer.Columns.Add("quality_name", typeof(string));
                m_dtbDepartmentTransfer.Columns.Add("quality_id", typeof(Int64));
                m_dtbDepartmentTransfer.Columns.Add("sieve_name", typeof(string));
                m_dtbDepartmentTransfer.Columns.Add("rough_sieve_id", typeof(Int64));
                m_dtbDepartmentTransfer.Columns.Add("rough_clarity_name", typeof(string));
                m_dtbDepartmentTransfer.Columns.Add("rough_clarity_id", typeof(Int64));
                m_dtbDepartmentTransfer.Columns.Add("purity_name", typeof(string));
                m_dtbDepartmentTransfer.Columns.Add("purity_id", typeof(Int64));

                m_dtbDepartmentTransfer.Columns.Add("pcs", typeof(int)).DefaultValue = 0;
                m_dtbDepartmentTransfer.Columns.Add("carat", typeof(decimal)).DefaultValue = 0;
                m_dtbDepartmentTransfer.Columns.Add("sr_no", typeof(int)).DefaultValue = 1;
                m_dtbDepartmentTransfer.Columns.Add("rr_pcs", typeof(int));
                m_dtbDepartmentTransfer.Columns.Add("rr_carat", typeof(decimal));
                m_dtbDepartmentTransfer.Columns.Add("main_lot_id", typeof(int));
                grdDepartmentTransfer.DataSource = m_dtbDepartmentTransfer;
                grdDepartmentTransfer.Refresh();
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
                    m_dtOutstanding = objSawableRecieve.GetBalanceCarat(lotId, 0);
                }


                if (m_dtOutstanding.Rows.Count > 0)
                {
                    txtCarat.Text = Val.ToString(m_dtOutstanding.Rows[0]["balance_carat"]);
                    txtPcs.Text = Val.ToString(m_dtOutstanding.Rows[0]["balance_pcs"]);
                    from_manager = Val.ToInt(m_dtOutstanding.Rows[0]["manager_id"]);
                    from_dept = Val.ToInt(m_dtOutstanding.Rows[0]["department_id"]);
                    m_balcarat = Val.ToDecimal(m_dtOutstanding.Rows[0]["balance_carat"]);
                    m_rrcarat = Val.ToDecimal(m_dtOutstanding.Rows[0]["rr_carat"]);
                    lueCutNo.Text = Val.ToString(m_dtOutstanding.Rows[0]["rough_cut_no"]);
                }
                else
                {
                    txtPcs.Text = "0";
                    txtCarat.Text = "0";
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        public void GetPendingStock()
        {
            try
            {
                if (lueCutNo.Text == "")
                {
                    Global.Message("Cut No is Required");
                    lueCutNo.Focus();
                    return;
                }
                else if (lueKapan.Text == "")
                {
                    Global.Message("Kapan No is Required");
                    lueKapan.Focus();
                    return;
                }

                MFGProcessIssue objMFGProcessIssue = new MFGProcessIssue();
                MFGProcessIssueProperty objMFGProcessIssueProperty = new MFGProcessIssueProperty();
                objMFGProcessIssueProperty.rough_cut_id = Val.ToInt(lueCutNo.EditValue);
                objMFGProcessIssueProperty.kapan_id = Val.ToInt(lueKapan.EditValue);

                DtPending = objMFGProcessIssue.GetPendingStock(objMFGProcessIssueProperty);

                FrmMFGStockConfirm FrmStockConfirm = new FrmMFGStockConfirm();
                FrmStockConfirm.FrmMFGDepartmentTransfer = this;
                FrmStockConfirm.DTab = DtPending;
                FrmStockConfirm.ShowForm(this);
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
                foreach (DataRow DRow in Stock_Data.Rows)
                {
                    m_dtOutstanding = objDepartmentTransfer.GetDeptBalanceCarat(Val.ToInt(DRow["lot_id"]));
                    if (m_dtOutstanding.Rows.Count > 0)
                    {
                        decimal Outstanding_carat = Val.ToDecimal(m_dtOutstanding.Rows[0]["carat"]);
                        if (Outstanding_carat > 0)
                        {
                            Global.Message(DRow["lot_id"] + " = This lot ID is Outstanding so please check..");
                            lueLotId.Focus();
                            return;
                        }
                    }
                }

                //m_dtbDepartmentTransfer = Stock_Data.Copy();
                //grdDepartmentTransfer.DataSource = m_dtbDepartmentTransfer;

                DTabTemp = Stock_Data.Copy();
                m_dtbDepartmentTransfer.AcceptChanges();
                if (m_dtbDepartmentTransfer != null)
                {
                    if (m_dtbDepartmentTransfer.Rows.Count > 0)
                    {
                        for (int i = 0; i < m_dtbDepartmentTransfer.Rows.Count; i++)
                        {
                            for (int j = 0; j < DTabTemp.Rows.Count; j++)
                            {
                                if (m_dtbDepartmentTransfer.Rows[i]["lot_id"].ToString() == DTabTemp.Rows[j]["lot_id"].ToString())
                                {
                                    Global.Message(m_dtbDepartmentTransfer.Rows[i]["lot_id"].ToString() + " = Lot ID already added to the Department list!");
                                    txtLotID.Text = "";
                                    txtLotID.Focus();
                                    return;
                                }
                            }
                        }
                    }
                }

                if (m_dtbDepartmentTransfer.Rows.Count > 0)
                {

                    DTabTemp = Stock_Data.Copy();

                    m_dtbDepartmentTransfer.Merge(DTabTemp);
                }
                else
                {
                    m_dtbDepartmentTransfer = Stock_Data.Copy();
                }
                grdDepartmentTransfer.DataSource = m_dtbDepartmentTransfer;
                grdDepartmentTransfer.RefreshDataSource();
                dgvDepartmentTransfer.BestFitColumns();
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
                            dgvDepartmentTransfer.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvDepartmentTransfer.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvDepartmentTransfer.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvDepartmentTransfer.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvDepartmentTransfer.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvDepartmentTransfer.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvDepartmentTransfer.ExportToCsv(Filepath);
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
        public void FillGrid(int Lot_SrNo)
        {
            MFGJangedIssue objMFGJangedIssue = new MFGJangedIssue();
            //DataTable DTab_DeptIssueJanged = objMFGJangedIssue.GetDepartmentDataDetails(Lot_SrNo);
            //if (DTab_DeptIssueJanged.Rows.Count > 0)
            //{
            //    txtLotSrNo.Text = Val.ToInt64(DTab_DeptIssueJanged.Rows[0]["lot_srno"]).ToString();
            //}

            DataTable DTab_DeptIssueJanged = objMFGJangedIssue.GetDepartmentDetails(Lot_SrNo);

            if (DTab_DeptIssueJanged.Rows.Count > 0)
            {
                txtLotSrNo.Text = Val.ToInt64(DTab_DeptIssueJanged.Rows[0]["lot_srno"]).ToString();
                //lueToCompany.EditValue = Val.ToInt32(DTab_StockData.Rows[0]["company_id"]);
                //lueToBranch.EditValue = Val.ToInt32(DTab_StockData.Rows[0]["branch_id"]);
                //lueToLocation.EditValue = Val.ToInt32(DTab_StockData.Rows[0]["location_id"]);
                lueToDepartment.EditValue = Val.ToInt32(DTab_DeptIssueJanged.Rows[0]["department_id"]);
                lueProcess.EditValue = Val.ToInt32(DTab_DeptIssueJanged.Rows[0]["process_id"]);
                lueManager.EditValue = Val.ToInt64(DTab_DeptIssueJanged.Rows[0]["employee_id"]);
                //txtJangedNo.Text = Val.ToString(DTab_StockData.Rows[0]["janged_no"]);
                //dtpReturnDate.Text = Val.ToString(DTab_StockData.Rows[0]["janged_date"]);
                //lblMode.Text = "EDIT";
                //lblMode.Tag = Val.ToInt64(DTab_StockData.Rows[0]["janged_no"]);
                //lueToDepartment.Focus();

            }
            else
            {
                txtLotSrNo.Text = "0";
                //lblMode.Text = "NEW";
                //lblTotalCrt.Text = "0";
                //lblMixLot.Text = "0";
            }
            grdDepartmentTransfer.DataSource = DTab_DeptIssueJanged;
            grdDepartmentTransfer.RefreshDataSource();
            dgvDepartmentTransfer.BestFitColumns();
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

        private void lueCutNo_Validated(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    if (Val.ToInt(lueCutNo.EditValue) > 0)
                    {
                        //DTab_LotId = objDepartmentTransfer.GetDataLotID(Val.ToInt(lueCutNo.EditValue));

                        MFGProcessIssue objMFGProcessIssue = new MFGProcessIssue();
                        MFGProcessIssueProperty objMFGProcessIssueProperty = new MFGProcessIssueProperty();
                        objMFGProcessIssueProperty.rough_cut_id = Val.ToInt(lueCutNo.EditValue);
                        objMFGProcessIssueProperty.kapan_id = Val.ToInt(lueKapan.EditValue);

                        DtPending = objMFGProcessIssue.GetPendingStock(objMFGProcessIssueProperty);

                    }
                    if (DtPending.Rows.Count > 0)
                    {
                        lueLotId.Properties.DataSource = DtPending;
                        lueLotId.Properties.DisplayMember = "lot_id";
                        lueLotId.Properties.ValueMember = "lot_id";
                    }
                    else
                    {
                        Global.Message("Lot ID Not Found in this Cut No =" + lueCutNo.Text);
                        lueLotId.EditValue = null;
                        return;
                    }

                    if (Val.ToInt(lueToDepartment.EditValue) > 0 && Val.ToInt(lueManager.EditValue) > 0 && Val.ToInt(lueProcess.EditValue) > 0)
                    {
                        btnAdd.Enabled = true;
                        btnPopUpStock.Enabled = true;
                    }
                    else
                    {
                        btnAdd.Enabled = false;
                        btnPopUpStock.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    Global.Message(ex.ToString());
                    return;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
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

        private void txtMainLotID_Validated(object sender, EventArgs e)
        {
            try
            {
                m_dtbDepartmentTransfer.AcceptChanges();

                if (txtMainLotID.Text.Length == 0)
                {
                    return;
                }
                if (m_dtbDepartmentTransfer.Rows.Count > 0)
                {
                    DataTable dtDistinct = new DataTable();
                    dtDistinct = m_dtbDepartmentTransfer.DefaultView.ToTable(true, "main_lot_id");
                    foreach (DataRow dr in dtDistinct.Rows)
                    {
                        if (Val.ToInt64(dr["main_lot_id"]) == Val.ToInt64(txtMainLotID.Text))
                        {
                            Global.Message(Val.ToInt64(txtMainLotID.Text) + " This Lot ID is Already Added.");
                            txtMainLotID.Text = "";
                            txtMainLotID.Focus();
                            return;
                        }
                    }
                }
                int IsIssue = objMFGProcessIssue.GetOsCheck(Val.ToInt64(txtMainLotID.Text));
                if (IsIssue == 1)
                {
                    Global.Message("Lot ID is Issued in Process.");
                    txtMainLotID.Focus();
                    return;
                }
                DataTable dtTemp = new DataTable();
                dtTemp = Global.GetRoughJangedMainStockWise(Val.ToInt(lueKapan.EditValue), Val.ToInt64(txtMainLotID.Text), "DEPT_TRANSFER");
                
                m_dtbDepartmentTransfer = m_dtbDepartmentTransfer.AsEnumerable().Union(dtTemp.AsEnumerable()).CopyToDataTable();
                //m_dtbReceiveProcess = Global.GetRoughJangedMainStockWise(Val.ToInt(lueKapan.EditValue), Val.ToInt64(txtMainLotID.Text), "GALAXY_RECIEVE");

                if (m_dtbDepartmentTransfer.Rows.Count > 0)
                {

                }
                else
                {
                    Global.Message("Lot ID Not Issue in Galaxy");
                    txtLotID.Text = "";
                    txtLotID.Focus();
                    return;
                }

                grdDepartmentTransfer.DataSource = m_dtbDepartmentTransfer;
                grdDepartmentTransfer.RefreshDataSource();
                dgvDepartmentTransfer.BestFitColumns();
                txtMainLotID.Text = "";
                txtMainLotID.Focus();

            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }

        private void txtMainLotID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
