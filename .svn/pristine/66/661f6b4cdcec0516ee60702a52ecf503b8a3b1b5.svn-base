using BLL;
using BLL.FunctionClasses.Master.MFG;
using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.PropertyClasses.Transaction.MFG;
using DERP.Class;
using DERP.Master;
using DERP.Report;
using DevExpress.XtraGrid.Views.Grid;
using DREP.Master;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Transaction.MFG
{
    public partial class FrmMFGJangedReturn : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        MFGProcessReceive objProcessReceive;
        MFGJangedReceive objMFGJangedReceive;
        MfgRoughSieve objRoughSieve;
        MfgQualityMaster objQuality;
        MfgRoughClarityMaster objRoughClarity;

        DataTable dtTemp;
        DataTable m_dtbParam;
        DataTable m_dtCut;
        DataTable m_dtbType;
        DataTable m_dtbKapan;
        DataTable m_dtbSubProcess;
        MFGJangedReturn objJangedReturn = new MFGJangedReturn();
        DataTable dtIss = new DataTable();
        DataTable DTab_StockData;

        Int64 m_numForm_id;
        Int64 JangedNo_IntRes;
        Int64 Dept_IntRes;
        Int64 Janged_IntRes;
        Int64 JangedSrNo_IntRes;

        #endregion

        #region Constructor
        public FrmMFGJangedReturn()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            objProcessReceive = new MFGProcessReceive();
            objMFGJangedReceive = new MFGJangedReceive();
            objRoughSieve = new MfgRoughSieve();
            objQuality = new MfgQualityMaster();
            objRoughClarity = new MfgRoughClarityMaster();
            DTab_StockData = new DataTable();
            dtTemp = new DataTable();
            m_dtbParam = new DataTable();
            m_dtCut = new DataTable();
            m_dtbKapan = new DataTable();
            m_dtbSubProcess = new DataTable();
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

        #region Events
        private void txtLotId_Validated(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateDetails())
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
                            Global.Message(Val.ToInt64(txtLotId.Text) + " = Lot ID already added to the Issue list!");
                            txtLotId.Text = "";
                            txtLotId.Focus();
                            return;
                        }

                        //for (int i = 0; i < DTab_StockData.Rows.Count; i++)
                        //{
                        //    if (DTab_StockData.Rows[i]["lot_id"].ToString() == txtLotId.Text)
                        //    {
                        //        Global.Message("Lot ID already added to the Issue list!");
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

                if (Val.ToInt64(txtLotId.Text) != 0)
                {
                    if (lueToDepartment.Text == "MAKABLE" && Val.ToString(GlobalDec.gEmployeeProperty.department_name) == "SARIN")
                    {
                        if (Global.CheckEstimationDoneOrNot(Val.ToInt64(txtLotId.Text)).ToString().Trim().Equals(string.Empty))
                        {
                            Global.Message("Estimation Entry Not Done in this Lot ID =" + Val.ToInt64(txtLotId.Text));
                            return;
                        }
                    }
                    else if (lueToDepartment.Text == "4P" && Val.ToString(GlobalDec.gEmployeeProperty.department_name) == "SARIN")
                    {
                        if (Global.CheckEstimationDoneOrNot(Val.ToInt64(txtLotId.Text)).ToString().Trim().Equals(string.Empty))
                        {
                            Global.Message("Estimation Entry Not Done in this Lot ID =" + Val.ToInt64(txtLotId.Text));
                            return;
                        }
                    }
                }

                if (Val.ToString(GlobalDec.gEmployeeProperty.department_name) == "SARIN")
                {
                    MFGJangedReturn MFGJangedReturn = new MFGJangedReturn();
                    MFGJangedReturn_Property objMFGJangedReturnProperty = new MFGJangedReturn_Property();

                    objMFGJangedReturnProperty.lot_id = Val.ToInt64(txtLotId.Text);

                    DataTable DTab_ProcessIDCount = objJangedReturn.Process_CountData(objMFGJangedReturnProperty);

                    if (DTab_ProcessIDCount.Rows[0]["CNT"].ToString() == "0")
                    {
                        Global.Message("Mapping Process not Completed in this Lot ID =" + Val.ToString(txtLotId.Text));
                        return;
                    }
                }
                else if (Val.ToString(GlobalDec.gEmployeeProperty.department_name) == "4P" || Val.ToString(GlobalDec.gEmployeeProperty.department_name) == "XXX-4P")
                {
                    MFGJangedReturn MFGJangedReturn = new MFGJangedReturn();
                    MFGJangedReturn_Property objMFGJangedReturnProperty = new MFGJangedReturn_Property();

                    objMFGJangedReturnProperty.lot_id = Val.ToInt64(txtLotId.Text);

                    DataTable DTab_ProcessIDCount = objJangedReturn.Process_CountData(objMFGJangedReturnProperty);

                    if (DTab_ProcessIDCount.Rows[0]["CNT"].ToString() == "0")
                    {
                        Global.Message("4P OK Process not Completed in this Lot ID =" + Val.ToString(txtLotId.Text));
                        return;
                    }
                }
                else if (Val.ToString(GlobalDec.gEmployeeProperty.department_name) == "4P SAWING")
                {
                    MFGJangedReturn MFGJangedReturn = new MFGJangedReturn();
                    MFGJangedReturn_Property objMFGJangedReturnProperty = new MFGJangedReturn_Property();

                    objMFGJangedReturnProperty.lot_id = Val.ToInt64(txtLotId.Text);

                    DataTable DTab_ProcessIDCount = objJangedReturn.Process_CountData(objMFGJangedReturnProperty);

                    if (DTab_ProcessIDCount.Rows[0]["CNT"].ToString() == "0")
                    {
                        Global.Message("SAWING OPERATOR Process not Completed in this Lot ID =" + Val.ToString(txtLotId.Text));
                        return;
                    }
                }
                else if (Val.ToString(GlobalDec.gEmployeeProperty.department_name) == "4P PLAT")
                {
                    MFGJangedReturn MFGJangedReturn = new MFGJangedReturn();
                    MFGJangedReturn_Property objMFGJangedReturnProperty = new MFGJangedReturn_Property();

                    objMFGJangedReturnProperty.lot_id = Val.ToInt64(txtLotId.Text);

                    DataTable DTab_ProcessIDCount = objJangedReturn.Process_CountData(objMFGJangedReturnProperty);

                    if (DTab_ProcessIDCount.Rows[0]["CNT"].ToString() == "0")
                    {
                        Global.Message("PLAT OPERATOR Process not Completed in this Lot ID =" + Val.ToString(txtLotId.Text));
                        return;
                    }
                }
                else if (Val.ToString(GlobalDec.gEmployeeProperty.department_name) == "RUSSIAN")
                {
                    MFGJangedReturn MFGJangedReturn = new MFGJangedReturn();
                    MFGJangedReturn_Property objMFGJangedReturnProperty = new MFGJangedReturn_Property();

                    objMFGJangedReturnProperty.lot_id = Val.ToInt64(txtLotId.Text);

                    DataTable DTab_ProcessIDCount = objJangedReturn.Process_CountData(objMFGJangedReturnProperty);

                    if (DTab_ProcessIDCount.Rows[0]["CNT"].ToString() == "0")
                    {
                        Global.Message("Russian PATTA-OK (MANAGER) Process not Completed in this Lot ID =" + Val.ToString(txtLotId.Text));
                        return;
                    }
                }
                if (DTab_StockData.Rows.Count > 0)
                {
                    DataTable DTabTemp = new DataTable();

                    DataTable DTab_ValidateLotID = objJangedReturn.Stock_GetData(Val.ToInt64(txtLotId.Text), 0, Val.ToInt32(txtJangedNo.Text));

                    if (DTab_ValidateLotID.Rows.Count > 0)
                    {
                    }
                    else
                    {
                        Global.Message("Lot ID Not Issue in Janged");
                        txtLotId.Text = "";
                        txtLotId.Focus();
                        return;
                    }

                    //DTabTemp = objJangedReturn.Stock_GetData(Val.ToInt64(txtLotId.Text), 0, Val.ToInt32(txtJangedNo.Text));

                    if (DTab_ValidateLotID.Rows.Count > 0)
                    {
                        txtLotId.Text = "";
                        txtLotId.Focus();
                    }
                    DTab_StockData.Merge(DTab_ValidateLotID);
                }
                else
                {
                    //DataTable DTab_ValidateLotID = objJangedReturn.Stock_GetData(Val.ToInt64(txtLotId.Text), 0, Val.ToInt32(txtJangedNo.Text));
                    DTab_StockData = objJangedReturn.Stock_GetData(Val.ToInt64(txtLotId.Text), 0, Val.ToInt32(txtJangedNo.Text));

                    if (DTab_StockData.Rows.Count > 0)
                    {
                    }
                    else
                    {
                        Global.Message("Lot ID Not Issue in Janged");
                        txtLotId.Text = "";
                        txtLotId.Focus();
                        return;
                    }

                    //DTab_StockData = objJangedReturn.Stock_GetData(Val.ToInt64(txtLotId.Text), 0, Val.ToInt32(txtJangedNo.Text));

                    if (DTab_StockData.Rows.Count > 0)
                    {
                        txtLotId.Text = "";
                        txtLotId.Focus();
                    }
                }
                grdJangedReturn.DataSource = DTab_StockData;
                grdJangedReturn.RefreshDataSource();
                dgvJangedReturn.BestFitColumns();
                (grdJangedReturn.FocusedView as GridView).MoveLast();
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void RepDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (Global.Confirm("Are you sure delete selected row?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                dgvJangedReturn.DeleteRow(dgvJangedReturn.GetRowHandle(dgvJangedReturn.FocusedRowHandle));
                DTab_StockData.AcceptChanges();
            }
        }
        private void FrmMFGJangedReturn_Load(object sender, EventArgs e)
        {
            try
            {
                m_dtbType = new DataTable();
                m_dtbType.Columns.Add("type");
                m_dtbType.Rows.Add("BOTH");
                m_dtbType.Rows.Add("NORMAL");
                m_dtbType.Rows.Add("REJECTION");

                dtpReturnDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpReturnDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpReturnDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpReturnDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpReturnDate.EditValue = DateTime.Now;

                Global.LOOKUPCompany_New(lueToCompany);
                Global.LOOKUPBranch_New(lueToBranch);
                Global.LOOKUPLocation_New(lueToLocation);
                Global.LOOKUPDepartment_New(lueToDepartment);
                Global.LOOKUPAllManager(lueToManager);
                Global.LOOKUPProcess(lueToProcess);
                btnDelete.Visible = false;
                lblMode.Text = "NEW";
                txtJangedNo.Focus();
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
                ObjPer.SetFormPer();
                if (ObjPer.AllowUpdate == false || ObjPer.AllowInsert == false)
                {
                    Global.Message(BLL.GlobalDec.gStrPermissionInsUpdMsg);
                    return;
                }
                string Str = "";
                var Date = DateTime.Compare(Convert.ToDateTime(dtpReturnDate.Text), DateTime.Today);
                if (Date < 0)
                {
                    Str = GlobalDec.CheckLockIsOpenOrNot(Val.DBDate(dtpReturnDate.Text), Val.DBTime(DateTime.Now.ToShortTimeString())); //Val.GetFullTime12());
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
                        dtpReturnDate.Enabled = true;
                        dtpReturnDate.Visible = true;
                    }
                }
                //if (Val.DBDate(BLL.GlobalDec.gStrServerDate) != Val.DBDate(dtpReturnDate.Text))
                //{
                //    Str = GlobalDec.CheckLockIsOpenOrNot(Val.DBDate(dtpReturnDate.Text), Val.DBTime(DateTime.Now.ToShortTimeString())); //Val.GetFullTime12());
                //    if (Str != "YES")
                //    {
                //        if (Str != "")
                //        {
                //            Global.Message(Str);
                //            return;
                //        }
                //        else
                //        {
                //            Global.Message("You Are Not Suppose to Make Entry On Different Date");
                //            return;
                //        }
                //    }
                //    else
                //    {
                //        dtpReturnDate.Enabled = true;
                //        dtpReturnDate.Visible = true;
                //    }
                //}

                btnSave.Enabled = false;
                DataTable dtTemp = new DataTable();
                dtTemp = (DataTable)grdJangedReturn.DataSource;
                List<ListError> lstError = new List<ListError>();
                if (dtTemp == null)
                {
                    Global.Message("Atleast 1 record must be enter in grid");
                    btnSave.Enabled = true;
                    return;

                }
                if (dtTemp.Rows.Count > 0 && Val.ToString(lblMode.Text) == "NEW")
                {
                    foreach (DataRow drwDate in dtTemp.Rows)
                    {
                        int DateCheck = Global.ValidateDate(Val.ToInt(drwDate["lot_id"]), Val.ToString(dtpReturnDate.Text));
                        if (DateCheck == 0)
                        {
                            Global.Message("Plz Check Recieve Date is less than Janged Date " + Val.ToInt(drwDate["lot_id"]));
                            btnSave.Enabled = true;
                            return;
                        }
                    }
                }
                if (!ValidateDetails())
                {
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

                panelProgress.Visible = true;
                backgroundWorker_JangedReturn.RunWorkerAsync();

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
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void backgroundWorker_JangedReturn_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                MFGJangedReturn MFGJangedReturn = new MFGJangedReturn();
                MFGJangedReturn_Property objMFGJangedReturnProperty = new MFGJangedReturn_Property();

                Conn = new BeginTranConnection(true, false);

                DataTable m_DTab = new DataTable();
                ArrayList MyArrayList = new ArrayList();

                JangedNo_IntRes = 0;
                Dept_IntRes = 0;
                Janged_IntRes = 0;
                JangedSrNo_IntRes = 0;
                Int64 NewHistory_Union_Id = 0;
                Int64 Lot_SrNo = 0;

                int IntCounter = 0;
                int Count = 0;

                try
                {
                    if (Val.ToString(lblMode.Text) == "EDIT")
                    {
                        objMFGJangedReturnProperty.to_company_id = Val.ToInt(lueToCompany.EditValue);
                        objMFGJangedReturnProperty.to_branch_id = Val.ToInt(lueToBranch.EditValue);
                        objMFGJangedReturnProperty.to_location_id = Val.ToInt(lueToLocation.EditValue);
                        objMFGJangedReturnProperty.to_department_id = Val.ToInt(lueToDepartment.EditValue);
                        objMFGJangedReturnProperty.to_manager_id = Val.ToInt(lueToManager.EditValue);
                        objMFGJangedReturnProperty.to_process_id = Val.ToInt(lueToProcess.EditValue);
                        objMFGJangedReturnProperty.janged_no = Val.ToInt64(lblMode.Tag);
                        objMFGJangedReturnProperty.janged_date = Val.DBDate(dtpReturnDate.Text);
                        JangedNo_IntRes = MFGJangedReturn.Update(objMFGJangedReturnProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                    }
                    else
                    {
                        DataTable Janged_Data = (DataTable)grdJangedReturn.DataSource;

                        int TotalCount = Janged_Data.Rows.Count;

                        foreach (DataRow drw in Janged_Data.Rows)
                        {
                            objMFGJangedReturnProperty.pcs = Val.ToInt(drw["pcs"]);
                            objMFGJangedReturnProperty.carat = Val.ToDecimal(drw["carat"]);
                            objMFGJangedReturnProperty.rr_pcs = Val.ToInt(drw["rr_pcs"]);
                            objMFGJangedReturnProperty.rr_carat = Val.ToDecimal(drw["rr_carat"]);
                            objMFGJangedReturnProperty.rejection_pcs = Val.ToInt(drw["rejection_pcs"]);
                            objMFGJangedReturnProperty.rejection_carat = Val.ToDecimal(drw["rejection_carat"]);
                            objMFGJangedReturnProperty.breakage_pcs = Val.ToInt(drw["breakage_pcs"]);
                            objMFGJangedReturnProperty.breakage_carat = Val.ToDecimal(drw["breakage_carat"]);
                            objMFGJangedReturnProperty.rate = Val.ToDecimal(drw["rate"]);
                            objMFGJangedReturnProperty.amount = Val.ToDecimal(drw["amount"]);

                            objMFGJangedReturnProperty.to_company_id = Val.ToInt(lueToCompany.EditValue);
                            objMFGJangedReturnProperty.to_branch_id = Val.ToInt(lueToBranch.EditValue);
                            objMFGJangedReturnProperty.to_location_id = Val.ToInt(lueToLocation.EditValue);
                            objMFGJangedReturnProperty.to_department_id = Val.ToInt(lueToDepartment.EditValue);

                            objMFGJangedReturnProperty.lot_id = Val.ToInt64(drw["lot_id"]);
                            objMFGJangedReturnProperty.janged_date = Val.DBDate(dtpReturnDate.Text);
                            objMFGJangedReturnProperty.quality_id = Val.ToInt64(drw["quality_id"]);
                            objMFGJangedReturnProperty.form_id = Val.ToInt(m_numForm_id);
                            objMFGJangedReturnProperty.rough_cut_id = Val.ToInt64(drw["rough_cut_id"]);
                            objMFGJangedReturnProperty.kapan_id = Val.ToInt64(drw["kapan_id"]);
                            objMFGJangedReturnProperty.prediction_id = Val.ToInt64(drw["prediction_id"]);

                            objMFGJangedReturnProperty.from_manager_id = Val.ToInt(drw["manager_id"]);
                            objMFGJangedReturnProperty.to_manager_id = Val.ToInt(lueToManager.EditValue);
                            objMFGJangedReturnProperty.employee_id = Val.ToInt(drw["employee_id"]);
                            //objMFGJangedReturnProperty.process_id = Val.ToInt(drw["process_id"]);
                            objMFGJangedReturnProperty.from_process_id = Val.ToInt(drw["process_id"]);
                            objMFGJangedReturnProperty.to_process_id = Val.ToInt(lueToProcess.EditValue);

                            objMFGJangedReturnProperty.sub_process_id = Val.ToInt(drw["sub_process_id"]);
                            objMFGJangedReturnProperty.rough_clarity_id = Val.ToInt(drw["rough_clarity_id"]);
                            objMFGJangedReturnProperty.rough_sieve_id = Val.ToInt(drw["rough_sieve_id"]);
                            objMFGJangedReturnProperty.purity_id = Val.ToInt(drw["purity_id"]);

                            objMFGJangedReturnProperty.history_union_id = NewHistory_Union_Id;
                            objMFGJangedReturnProperty.prev_lot_id = Val.ToInt64(drw["prev_lot_id"]);
                            objMFGJangedReturnProperty.lot_srno = Lot_SrNo;

                            objMFGJangedReturnProperty = MFGJangedReturn.Save(objMFGJangedReturnProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                            Janged_IntRes = objMFGJangedReturnProperty.janged_union_id;
                            JangedNo_IntRes = objMFGJangedReturnProperty.janged_no;
                            Dept_IntRes = objMFGJangedReturnProperty.dept_union_id;
                            NewHistory_Union_Id = Val.ToInt64(objMFGJangedReturnProperty.history_union_id);
                            JangedSrNo_IntRes = objMFGJangedReturnProperty.janged_srno;
                            Lot_SrNo = Val.ToInt64(objMFGJangedReturnProperty.lot_srno);

                            Count++;
                            IntCounter++;
                            SetControlPropertyValue(lblProgressCount, "Text", Count.ToString() + "" + "/" + "" + TotalCount.ToString() + " Completed....");
                        }
                    }
                    Conn.Inter1.Commit();
                }
                catch (Exception ex)
                {
                    JangedNo_IntRes = -1;
                    Conn.Inter1.Rollback();
                    Conn = null;
                    General.ShowErrors(ex.ToString());
                    return;
                }
            }
            catch (Exception ex)
            {
                JangedNo_IntRes = -1;
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
        private void backgroundWorker_JangedReturn_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                panelProgress.Visible = false;
                if (JangedNo_IntRes > 0)
                {
                    if (Val.ToString(lblMode.Text) == "EDIT")
                    {
                        Global.Message("Janged Return Update Succesfully");
                        btnSave.Enabled = true;
                        ClearDetails();
                        return;
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("Janged Return Save Succesfully and janged no is : " + JangedNo_IntRes + " Are you sure print this janged?", "Confirmation", MessageBoxButtons.YesNoCancel);
                        if (result != DialogResult.Yes)
                        {
                            btnSave.Enabled = true;
                            ClearDetails();
                            return;
                        }

                        //if (GlobalDec.gEmployeeProperty.role_name == "SURAT KAMALA")
                        //{
                        //    MFGJangedIssue objMFGJangedIssue = new MFGJangedIssue();
                        //    DataTable DTab_IssueJanged = objMFGJangedIssue.GetData_JangedReturn_Galaxy(Val.ToInt64(JangedNo_IntRes));

                        //    ClearDetails();

                        //    FrmReportViewer FrmReportViewer = new FrmReportViewer();
                        //    FrmReportViewer.DS.Tables.Add(DTab_IssueJanged);
                        //    FrmReportViewer.GroupBy = "";
                        //    FrmReportViewer.RepName = "";
                        //    FrmReportViewer.RepPara = "";
                        //    this.Cursor = Cursors.Default;
                        //    FrmReportViewer.AllowSetFormula = true;

                        //    FrmReportViewer.ShowForm_SubReport("Janged_Issue_Galaxy_Main", 120, FrmReportViewer.ReportFolder.ACCOUNT);


                        //    DTab_IssueJanged = null;
                        //    FrmReportViewer.DS.Tables.Clear();
                        //    FrmReportViewer.DS.Clear();
                        //    FrmReportViewer = null;
                        //    btnSave.Enabled = true;
                        //}
                        //else
                        //{
                        MFGJangedIssue objMFGJangedIssue = new MFGJangedIssue();
                        DataTable DTab_IssueJanged = objMFGJangedIssue.GetDataDetails_JangedReturn(Val.ToInt64(JangedNo_IntRes));

                        ClearDetails();

                        FrmReportViewer FrmReportViewer = new FrmReportViewer();
                        FrmReportViewer.DS.Tables.Add(DTab_IssueJanged);
                        FrmReportViewer.GroupBy = "";
                        FrmReportViewer.RepName = "";
                        FrmReportViewer.RepPara = "";
                        this.Cursor = Cursors.Default;
                        FrmReportViewer.AllowSetFormula = true;

                        FrmReportViewer.ShowForm_SubReport("Janged_Issue_Main", 120, FrmReportViewer.ReportFolder.ACCOUNT);


                        DTab_IssueJanged = null;
                        FrmReportViewer.DS.Tables.Clear();
                        FrmReportViewer.DS.Clear();
                        FrmReportViewer = null;
                        btnSave.Enabled = true;
                        //}
                    }
                    //ClearDetails();
                    //dgvJangedReturn.DataSource = null;

                    //Global.Confirm("Janged Return Save Succesfully");
                    //btnClear_Click(null, null);
                }
                else
                {
                    Global.Confirm("Error In Janged Return");
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }
        private void lueToLocation_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (e.Button.Index == 1)
                {
                    FrmLocationMaster frmLocation = new FrmLocationMaster();
                    frmLocation.ShowDialog();
                    Global.LOOKUPLocation_New(lueToLocation);
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
                    Global.LOOKUPDepartment_New(lueToDepartment);
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void lueToManager_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmEmployeeMaster frmManager = new FrmEmployeeMaster();
                frmManager.ShowDialog();
                Global.LOOKUPAllManager(lueToManager);
            }
        }
        private void lueToProcess_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmProcessMaster frmProcess = new FrmProcessMaster();
                frmProcess.ShowDialog();
                Global.LOOKUPProcess(lueToProcess);
            }
        }
        private void btnPopUpStock_Click(object sender, EventArgs e)
        {
            GetStock();
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            //if (GlobalDec.gEmployeeProperty.role_name == "SURAT KAMALA")
            //{
            //    MFGJangedIssue objMFGJangedIssue = new MFGJangedIssue();
            //    DataTable DTab_IssueJanged = objMFGJangedIssue.GetData_JangedReturn_Galaxy(Val.ToInt64(txtJangedNo.Text));

            //    FrmReportViewer FrmReportViewer = new FrmReportViewer();
            //    FrmReportViewer.DS.Tables.Add(DTab_IssueJanged);
            //    FrmReportViewer.GroupBy = "";
            //    FrmReportViewer.RepName = "";
            //    FrmReportViewer.RepPara = "";
            //    this.Cursor = Cursors.Default;
            //    FrmReportViewer.AllowSetFormula = true;

            //    FrmReportViewer.ShowForm_SubReport("Janged_Issue_Galaxy_Main", 120, FrmReportViewer.ReportFolder.ACCOUNT);

            //    DTab_IssueJanged = null;
            //    FrmReportViewer.DS.Tables.Clear();
            //    FrmReportViewer.DS.Clear();
            //    FrmReportViewer = null;
            //}
            //else
            //{
            MFGJangedIssue objMFGJangedIssue = new MFGJangedIssue();
            DataTable DTab_IssueJanged = objMFGJangedIssue.GetDataDetails_JangedReturn(Val.ToInt64(txtJangedNo.Text));

            FrmReportViewer FrmReportViewer = new FrmReportViewer();
            FrmReportViewer.DS.Tables.Add(DTab_IssueJanged);
            FrmReportViewer.GroupBy = "";
            FrmReportViewer.RepName = "";
            FrmReportViewer.RepPara = "";
            this.Cursor = Cursors.Default;
            FrmReportViewer.AllowSetFormula = true;

            FrmReportViewer.ShowForm_SubReport("Janged_Issue_Main", 120, FrmReportViewer.ReportFolder.ACCOUNT);

            DTab_IssueJanged = null;
            FrmReportViewer.DS.Tables.Clear();
            FrmReportViewer.DS.Clear();
            FrmReportViewer = null;
            //}
        }
        private void btnSearchData_Click(object sender, EventArgs e)
        {
            FrmMFGSearchProcess FrmSearchProcess = new FrmMFGSearchProcess();
            FrmSearchProcess.FrmMFGJangedReturn = this;
            //FrmSearchProcess.DTab = DtPending;
            FrmSearchProcess.ShowForm(this);
        }

        #region Grid Event
        private void grdJangedReturn_ViewRegistered(object sender, DevExpress.XtraGrid.ViewOperationEventArgs e)
        {
            (e.View as GridView).OptionsNavigation.AutoFocusNewRow = true;
        }

        #endregion

        #endregion

        #region Functions

        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();

            try
            {
                //if (txtJangedNo.Text == "")
                //{
                //    lstError.Add(new ListError(5, "Janged No is Required.."));
                //    if (!blnFocus)
                //    {
                //        blnFocus = true;
                //        txtJangedNo.Focus();
                //    }
                //}
                //var result = DateTime.Compare(Convert.ToDateTime(dtpReturnDate.Text), DateTime.Today);
                //if (result > 0)
                //{
                //    lstError.Add(new ListError(5, " Return Date Not Be Greater Than Today Date"));
                //    if (!blnFocus)
                //    {
                //        blnFocus = true;
                //        dtpReturnDate.Focus();
                //    }
                //}
                DateTime endDate = Convert.ToDateTime(DateTime.Today);
                endDate = endDate.AddDays(15);

                if (Convert.ToDateTime(dtpReturnDate.Text) >= endDate)
                {
                    lstError.Add(new ListError(5, " Return Date Not Be Permission After 3 Days Return this Lot ID...Please Contact to Administrator"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        dtpReturnDate.Focus();
                    }
                }
                if (Val.ToString(dtpReturnDate.Text) == string.Empty)
                {
                    lstError.Add(new ListError(22, "Date"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        dtpReturnDate.Focus();
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

                if (GlobalDec.gEmployeeProperty.user_name != "HARESH" && GlobalDec.gEmployeeProperty.role_name != "GALAXY DW")
                {
                    if (Val.ToInt(lueToDepartment.EditValue) == GlobalDec.gEmployeeProperty.department_id)
                    {
                        lstError.Add(new ListError(5, "Lot Not Return in a Same Department"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueToDepartment.Focus();
                        }
                    }
                }

                if (lueToProcess.Text == "")
                {
                    lstError.Add(new ListError(13, "To Process"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueToProcess.Focus();
                    }
                }
                if (lueToManager.Text == "")
                {
                    lstError.Add(new ListError(13, "To Manager"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueToManager.Focus();
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
                dtpReturnDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpReturnDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpReturnDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpReturnDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpReturnDate.EditValue = DateTime.Now;
                txtLotId.Text = "0";
                grdJangedReturn.DataSource = null;
                DTab_StockData.Rows.Clear();
                DTab_StockData.Columns.Clear();
                lblMode.Text = "NEW";
                lblMode.Tag = Val.ToInt64(0);
                lueToCompany.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.company_id);
                lueToBranch.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.branch_id);
                lueToLocation.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.location_id);
                lueToDepartment.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.department_id);
                btnDelete.Visible = false;
                txtJangedNo.Focus();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
        }
        public void GetStockData(DataTable Stock_Data)
        {
            try
            {
                if (lueToDepartment.Text == "MAKABLE" && Val.ToString(GlobalDec.gEmployeeProperty.department_name) == "SARIN")
                {
                    foreach (DataRow DRow in Stock_Data.Rows)
                    {
                        if (Global.CheckEstimationDoneOrNot(Val.ToInt64(DRow["lot_id"])).ToString().Trim().Equals(string.Empty))
                        {
                            Global.Message("Estimation Entry Not Done in this Lot ID =" + Val.ToInt64(DRow["lot_id"]));
                            return;
                        }
                    }
                }
                else if (lueToDepartment.Text == "4P" && Val.ToString(GlobalDec.gEmployeeProperty.department_name) == "SARIN")
                {
                    foreach (DataRow DRow in Stock_Data.Rows)
                    {
                        if (Global.CheckEstimationDoneOrNot(Val.ToInt64(DRow["lot_id"])).ToString().Trim().Equals(string.Empty))
                        {
                            Global.Message("Estimation Entry Not Done in this Lot ID =" + Val.ToInt64(DRow["lot_id"]));
                            return;
                        }
                    }
                }

                m_dtbSubProcess = Stock_Data.Copy();
                grdJangedReturn.DataSource = m_dtbSubProcess;
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
            }
        }
        public void GetStock()
        {
            try
            {

                DTab_StockData = objJangedReturn.ReturnStock_GetData();

                FrmMFGStockConfirm FrmStockConfirm = new FrmMFGStockConfirm();
                FrmStockConfirm.FrmMFGJangedReturn = this;
                FrmStockConfirm.DTab = DTab_StockData;
                FrmStockConfirm.ShowForm(this);

            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
            }
        }
        public void FillGrid(int UnionId)
        {
            DtTransfer = new DataTable();
            DTab_StockData = objJangedReturn.JangedReturn_GetData(UnionId);

            if (DTab_StockData.Rows.Count > 0)
            {
                lueToCompany.EditValue = Val.ToInt32(DTab_StockData.Rows[0]["company_id"]);
                lueToBranch.EditValue = Val.ToInt32(DTab_StockData.Rows[0]["branch_id"]);
                lueToLocation.EditValue = Val.ToInt32(DTab_StockData.Rows[0]["location_id"]);
                lueToDepartment.EditValue = Val.ToInt32(DTab_StockData.Rows[0]["department_id"]);
                lueToProcess.EditValue = Val.ToInt32(DTab_StockData.Rows[0]["process_id"]);
                lueToManager.EditValue = Val.ToInt64(DTab_StockData.Rows[0]["manager_id"]);
                txtJangedNo.Text = Val.ToString(DTab_StockData.Rows[0]["janged_no"]);
                dtpReturnDate.Text = Val.ToString(DTab_StockData.Rows[0]["janged_date"]);
                lblMode.Text = "EDIT";
                lblMode.Tag = Val.ToInt64(DTab_StockData.Rows[0]["janged_no"]);

                if (lueToDepartment.Text == "MAKABLE" && Val.ToString(GlobalDec.gEmployeeProperty.department_name) == "SARIN")
                {
                    foreach (DataRow DRow in DTab_StockData.Rows)
                    {
                        if (Global.CheckEstimationDoneOrNot(Val.ToInt64(DRow["lot_id"])).ToString().Trim().Equals(string.Empty))
                        {
                            Global.Message("Estimation Entry Not Done in this Lot ID =" + Val.ToInt64(DRow["lot_id"]));
                            return;
                        }
                    }
                }
                else if (lueToDepartment.Text == "4P" && Val.ToString(GlobalDec.gEmployeeProperty.department_name) == "SARIN")
                {
                    foreach (DataRow DRow in DTab_StockData.Rows)
                    {
                        if (Global.CheckEstimationDoneOrNot(Val.ToInt64(DRow["lot_id"])).ToString().Trim().Equals(string.Empty))
                        {
                            Global.Message("Estimation Entry Not Done in this Lot ID =" + Val.ToInt64(DRow["lot_id"]));
                            return;
                        }
                    }
                }
                if (GlobalDec.gEmployeeProperty.role_name == "SURAT ADMIN")
                {
                    btnDelete.Visible = true;
                }
                else
                {
                    btnDelete.Visible = false;
                }
                lueToDepartment.Focus();
            }
            else
            {
                //lblMode.Text = "NEW";
                //lblTotalCrt.Text = "0";
                //lblMixLot.Text = "0";
            }
            grdJangedReturn.DataSource = DTab_StockData;
            grdJangedReturn.RefreshDataSource();
            dgvJangedReturn.BestFitColumns();
        }

        #endregion

        #region Export Grid

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
                            dgvJangedReturn.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvJangedReturn.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvJangedReturn.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvJangedReturn.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvJangedReturn.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvJangedReturn.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvJangedReturn.ExportToCsv(Filepath);
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

        private void txtLotId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtMainLotID_KeyPress(object sender, KeyPressEventArgs e)
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
                if (!ValidateDetails())
                {
                    return;
                }
                DTab_StockData.AcceptChanges();
                if (DTab_StockData != null)
                {
                    if (DTab_StockData.Rows.Count > 0)
                    {
                        DataRow[] dr = DTab_StockData.Select("fac_main_lot_id = " + Val.ToInt64(txtMainLotID.Text));

                        if (dr.Length > 0)
                        {
                            Global.Message("Main Lot ID already added to the Issue list!");
                            txtMainLotID.Text = "";
                            txtMainLotID.Focus();
                            return;
                        }

                        //for (int i = 0; i < DTab_StockData.Rows.Count; i++)
                        //{
                        //    if (DTab_StockData.Rows[i]["fac_main_lot_id"].ToString() == txtMainLotID.Text)
                        //    {
                        //        Global.Message("Main Lot ID already added to the Issue list!");
                        //        txtMainLotID.Text = "";
                        //        txtMainLotID.Focus();
                        //        return;
                        //    }
                        //}
                    }
                }

                if (txtMainLotID.Text.Length == 0)
                {
                    return;
                }

                if (DTab_StockData.Rows.Count > 0)
                {
                    DataTable DTabTemp = new DataTable();

                    DataTable DTab_ValidateLotID = objJangedReturn.Main_LotID_Stock_GetData(0, Val.ToInt64(txtMainLotID.Text), Val.ToInt32(txtJangedNo.Text));

                    if (DTab_ValidateLotID.Rows.Count > 0)
                    {
                    }
                    else
                    {
                        Global.Message("Main Lot ID Not Issue in Janged");
                        txtMainLotID.Text = "";
                        txtMainLotID.Focus();
                        return;
                    }

                    DTabTemp = objJangedReturn.Main_LotID_Stock_GetData(0, Val.ToInt64(txtMainLotID.Text), Val.ToInt32(txtJangedNo.Text));

                    if (DTabTemp.Rows.Count > 0)
                    {
                        txtMainLotID.Text = "";
                        txtMainLotID.Focus();
                    }
                    DTab_StockData.Merge(DTabTemp);
                }
                else
                {
                    DataTable DTab_ValidateLotID = objJangedReturn.Main_LotID_Stock_GetData(0, Val.ToInt64(txtMainLotID.Text), Val.ToInt32(txtJangedNo.Text));

                    if (DTab_ValidateLotID.Rows.Count > 0)
                    {
                    }
                    else
                    {
                        Global.Message("Lot ID Not Issue in Janged");
                        txtMainLotID.Text = "";
                        txtMainLotID.Focus();
                        return;
                    }

                    DTab_StockData = objJangedReturn.Main_LotID_Stock_GetData(0, Val.ToInt64(txtMainLotID.Text), Val.ToInt32(txtJangedNo.Text));

                    if (DTab_StockData.Rows.Count > 0)
                    {
                        txtMainLotID.Text = "";
                        txtMainLotID.Focus();
                    }
                }
                grdJangedReturn.DataSource = DTab_StockData;
                grdJangedReturn.RefreshDataSource();
                dgvJangedReturn.BestFitColumns();
                (grdJangedReturn.FocusedView as GridView).MoveLast();
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                MFGJangedReturn MFGJangedReturn = new MFGJangedReturn();
                MFGJangedReturn_Property objMFGJangedReturnProperty = new MFGJangedReturn_Property();
                Conn = new BeginTranConnection(true, false);
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

                    count = MFGJangedReturn.CheckJanged(Val.ToInt32(lblMode.Tag));
                    if (count == 0)
                    {
                        DialogResult result = MessageBox.Show("Do you want to Delete Janged Return data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                        if (result != DialogResult.Yes)
                        {
                            btnDelete.Enabled = true;
                            return;
                        }

                        objMFGJangedReturnProperty.janged_no = Val.ToInt32(lblMode.Tag);

                        int IntRes = MFGJangedReturn.JangedRecieveDelete(objMFGJangedReturnProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                        Conn.Inter1.Commit();

                        if (IntRes == -1)
                        {
                            Global.Confirm("Error In Delete Janged Return");
                            IntRes = -1;
                            Conn.Inter1.Rollback();
                            Conn = null;
                            return;
                            //txtPartyInvoiceNo.Focus();
                        }
                        else
                        {
                            Global.Confirm("Janged Return Data Delete Successfully");
                            ClearDetails();

                        }
                    }
                    else
                    {
                        Global.Message("Janged Already Confirm!!!");
                        btnDelete.Enabled = true;
                        return;
                    }
                    btnDelete.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                btnDelete.Enabled = true;
            }
        }
    }
}
