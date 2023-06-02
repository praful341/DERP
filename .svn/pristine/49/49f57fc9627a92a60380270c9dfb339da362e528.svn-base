using BLL;
using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.PropertyClasses.Transaction;
using BLL.PropertyClasses.Transaction.MFG;
using DERP.Class;
using DERP.Master;
using DERP.Master.MFG;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace DERP.Transaction.MFG
{
    public partial class FrmMFGProcessWeightLossRecieve : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        Control _NextEnteredControl;
        private List<Control> _tabControls;

        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        MFGProcessReceive objProcessRecieve;

        DataTable DtControlSettings;
        DataTable m_dtbDetail;
        DataTable m_dtbParam;
        DataTable m_dtbSubProcess;
        DataTable m_dtbReceiveProcess;
        DataTable m_dtOutstanding;
        DataTable m_dtbDetails;
        DataTable m_dtbKapan;
        DataTable DtPending = new DataTable();
        DataTable DTabTemp = new DataTable();
        DataTable DTab_StockData = new DataTable();
        DataTable dtIss = new DataTable();
        //DataTable DTab_KapanWiseData;

        MFGMixSplit objMFGMixSplit = new MFGMixSplit();
        int m_Srno;
        int m_update_srno;
        int m_issue_id;
        int m_manager_id;
        int m_emp_id;
        int m_IsLot;
        int m_OsPcs;

        Int64 m_numForm_id;
        Int64 IntRes;
        Int64 m_kapan_id;
        string m_cut_no;

        decimal m_numlosscarat;
        decimal m_numlostcarat;
        decimal m_old_losscarat;
        decimal m_old_lostcarat;
        decimal m_OsCarat;
        decimal m_BalCarat;

        bool m_blnadd;
        bool m_blnsave;

        #endregion

        #region Constructor
        public FrmMFGProcessWeightLossRecieve()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            _NextEnteredControl = new Control();
            _tabControls = new List<Control>();

            objProcessRecieve = new MFGProcessReceive();

            DtControlSettings = new DataTable();
            m_dtbDetail = new DataTable();
            m_dtbParam = new DataTable();
            m_dtbSubProcess = new DataTable();
            m_dtbReceiveProcess = new DataTable();
            m_dtOutstanding = new DataTable();
            m_dtbDetails = new DataTable();
            m_dtbKapan = new DataTable();
            //DTab_KapanWiseData = new DataTable();

            m_Srno = 1;
            m_update_srno = 1;
            m_issue_id = 0;
            m_manager_id = 0;
            m_emp_id = 0;
            m_numForm_id = 0;
            m_kapan_id = 0;
            m_IsLot = 0;
            m_OsPcs = 0;

            m_cut_no = "";

            m_numlosscarat = 0;
            m_numlostcarat = 0;
            m_old_losscarat = 0;
            m_old_lostcarat = 0;
            m_OsCarat = 0;
            m_BalCarat = 0;

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
                else if ((Control)sender is CheckedComboBoxEdit)
                {
                    if (e.KeyChar == Convert.ToChar(Keys.Enter))
                    {
                        SendKeys.Send("{TAB}");
                    }
                }
                else if ((Control)sender is SimpleButton)
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

        #region Events
        private void btnPopUpStock_Click(object sender, EventArgs e)
        {
            GetPendingStock();
        }
        private void txtLotID_Validated(object sender, EventArgs e)
        {
            m_IsLot = 1;
            lueKapan.EditValue = null;
            lueCutNo.EditValue = null;
            if (m_dtbParam.Rows.Count > 0)
            {
                if (Val.ToInt64(txtLotID.Text) != 0)
                {
                    DataRow[] dr = m_dtbParam.Select("lot_id =" + Val.ToInt64(txtLotID.Text));
                    if (txtLotID.Text != string.Empty || Val.ToInt64(txtLotID.Text) != 0)
                    {
                        lueKapan.Text = Val.ToString(m_dtbParam.Rows[0]["kapan_no"]);
                        lueCutNo.Text = Val.ToString(m_dtbParam.Rows[0]["rough_cut_no"]);
                        DataTable DTab_Process = objProcessRecieve.Carat_OutStanding_GetData(Val.ToInt64(txtLotID.Text), Val.ToInt32(0), Val.ToInt32(0), 0, "R");

                        if (DTab_Process.Rows.Count > 0)
                        {
                            lueProcess.Properties.DataSource = DTab_Process;
                            lueProcess.Properties.DisplayMember = "process_name";
                            lueProcess.Properties.ValueMember = "process_id";
                        }
                        else
                        {
                            Global.Message("Lot Not Issue");
                            lblOsPcs.Text = "0";
                            lblOsCarat.Text = "0.00";
                            m_OsPcs = 0;
                            m_OsCarat = 0;
                            lueProcess.Enabled = true;
                            lueSubProcess.Enabled = true;
                            lueProcess.EditValue = null;
                            lueSubProcess.EditValue = null;
                            txtLotID.Focus();
                        }
                        if (DTab_Process.Rows.Count == 1)
                        {
                            lblOsPcs.Text = Val.ToString(DTab_Process.Rows[0]["pcs"]);
                            lblOsCarat.Text = Val.ToString(DTab_Process.Rows[0]["carat"]);
                            m_OsPcs = Val.ToInt(DTab_Process.Rows[0]["pcs"]);
                            m_OsCarat = Val.ToDecimal(DTab_Process.Rows[0]["carat"]);
                            txtReturnPcs.Text = Val.ToString(DTab_Process.Rows[0]["pcs"]);
                            txtReturnCarat.Text = Val.ToString(DTab_Process.Rows[0]["carat"]);
                            lueProcess.Text = Val.ToString(DTab_Process.Rows[0]["process_name"]);
                            lueSubProcess.Text = Val.ToString(DTab_Process.Rows[0]["sub_process_name"]);
                            lueProcess.Enabled = false;
                            lueSubProcess.Enabled = false;
                        }
                        else
                        {
                            lblOsPcs.Text = "0";
                            lblOsCarat.Text = "0.00";
                            m_OsPcs = 0;
                            m_OsCarat = 0;
                            txtReturnPcs.Text = "";
                            txtReturnCarat.Text = "";
                            lueProcess.Enabled = true;
                            lueSubProcess.Enabled = true;
                            lueProcess.EditValue = null;
                            lueSubProcess.EditValue = null;
                        }
                    }
                    m_IsLot = 0;
                }
                else
                {
                    BLL.General.ShowErrors("Cut No not Found");
                    lueCutNo.EditValue = System.DBNull.Value;
                    m_IsLot = 0;
                }
            }
            else if (m_dtbParam.Rows.Count == 0 && txtLotID.Text.ToString() != "")
            {
                if (lueKapan.Text.ToString() == "" && lueKapan.Text.ToString() == "" && txtLotID.Text.ToString() != "")
                {
                    m_IsLot = 1;
                }
                else
                {
                    m_IsLot = 0;
                }
                m_dtbParam = new DataTable();
                if (lueKapan.Text.ToString() == "" && txtLotID.Text.ToString() != "")
                {
                    m_dtbParam = Global.GetRoughKapanWise(Val.ToInt(lueKapan.EditValue), Val.ToInt64(txtLotID.Text));
                }
                lueKapan.Text = Val.ToString(m_dtbParam.Rows[0]["kapan_no"]);
                lueCutNo.Properties.DataSource = m_dtbParam;
                lueCutNo.Properties.ValueMember = "rough_cut_id";
                lueCutNo.Properties.DisplayMember = "rough_cut_no";
                lueCutNo.Text = Val.ToString(m_dtbParam.Rows[0]["rough_cut_no"]);
                if (txtLotID.Text != string.Empty || Val.ToInt64(txtLotID.Text) != 0)
                {
                    GetOsCarat(Val.ToInt64(txtLotID.Text));
                    DataTable DTab_Process = objProcessRecieve.Carat_OutStanding_GetData(Val.ToInt64(txtLotID.Text), Val.ToInt32(0), Val.ToInt32(0), 0, "R");

                    if (DTab_Process.Rows.Count > 0)
                    {
                        lueProcess.Properties.DataSource = DTab_Process;
                        lueProcess.Properties.DisplayMember = "process_name";
                        lueProcess.Properties.ValueMember = "process_id";
                    }
                    else
                    {
                        Global.Message("Lot Not Issue");
                        lblOsPcs.Text = "0";
                        lblOsCarat.Text = "0.00";
                        m_OsPcs = 0;
                        m_OsCarat = 0;
                        lueProcess.Enabled = true;
                        lueSubProcess.Enabled = true;
                        lueProcess.EditValue = null;
                        lueSubProcess.EditValue = null;
                    }
                    if (DTab_Process.Rows.Count == 1)
                    {
                        lblOsPcs.Text = Val.ToString(DTab_Process.Rows[0]["pcs"]);
                        lblOsCarat.Text = Val.ToString(DTab_Process.Rows[0]["carat"]);
                        m_OsPcs = Val.ToInt(DTab_Process.Rows[0]["pcs"]);
                        m_OsCarat = Val.ToDecimal(DTab_Process.Rows[0]["carat"]);
                        lueProcess.Text = Val.ToString(DTab_Process.Rows[0]["process_name"]);
                        lueSubProcess.Text = Val.ToString(DTab_Process.Rows[0]["sub_process_name"]);
                        lueProcess.Enabled = false;
                        lueSubProcess.Enabled = false;
                    }
                    else
                    {
                        lblOsPcs.Text = "0";
                        lblOsCarat.Text = "0.00";
                        m_OsPcs = 0;
                        m_OsCarat = 0;
                        lueProcess.Enabled = true;
                        lueSubProcess.Enabled = true;
                        lueProcess.EditValue = null;
                        lueSubProcess.EditValue = null;
                    }
                }
                m_IsLot = 0;
            }
            m_IsLot = 0;
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
        private void backgroundWorker_ProcRecWeightLoss_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                MFGProcessWeightLossRecieve objMFGProcessWeightLoss = new MFGProcessWeightLossRecieve();
                MFGProcessWeightLossRecieve_Property objMFGProcessIssueProperty = new MFGProcessWeightLossRecieve_Property();
                Conn = new BeginTranConnection(true, false);

                IntRes = 0;
                Int64 NewHistory_Union_Id = 0;
                Int64 Lot_SrNo = 0;

                int IntCounter = 0;
                int Count = 0;
                int TotalCount = m_dtbReceiveProcess.Rows.Count;
                try
                {
                    foreach (DataRow drw in m_dtbReceiveProcess.Rows)
                    {
                        objMFGProcessIssueProperty.issue_id = Val.ToInt(drw["issue_id"]);
                        objMFGProcessIssueProperty.lot_id = Val.ToInt(drw["lot_id"]);
                        objMFGProcessIssueProperty.receive_date = Val.DBDate(Val.ToString(drw["recieve_date"]));
                        objMFGProcessIssueProperty.manager_id = Val.ToInt(drw["manager_id"]);
                        objMFGProcessIssueProperty.employee_id = Val.ToInt(drw["employee_id"]);
                        objMFGProcessIssueProperty.process_id = Val.ToInt(drw["process_id"]);//Val.ToInt(lueProcess.EditValue); //Val.ToInt(drw["process_id"]);
                        objMFGProcessIssueProperty.sub_process_id = Val.ToInt(drw["sub_process_id"]);  //Val.ToInt(lueSubProcess.EditValue); //Val.ToInt(drw["sub_process_id"]);
                        objMFGProcessIssueProperty.pcs = Val.ToInt(drw["return_pcs"]);
                        objMFGProcessIssueProperty.carat = Val.ToDecimal(drw["return_carat"]);
                        objMFGProcessIssueProperty.loss_pcs = Val.ToInt(drw["loss_pcs"]);
                        objMFGProcessIssueProperty.loss_carat = Val.ToDecimal(drw["loss_carat"]);
                        objMFGProcessIssueProperty.lost_pcs = Val.ToInt(drw["lost_pcs"]);
                        objMFGProcessIssueProperty.lost_carat = Val.ToDecimal(drw["lost_carat"]);
                        objMFGProcessIssueProperty.rejection_pcs = Val.ToInt(drw["rejection_pcs"]);
                        objMFGProcessIssueProperty.rejection_carat = Val.ToDecimal(drw["rejection_carat"]);
                        if (Val.ToDecimal(drw["balance"]) == 0)
                        {
                            objMFGProcessIssueProperty.is_outstanding = 1;
                        }
                        else
                        {
                            objMFGProcessIssueProperty.is_outstanding = 0;
                        }
                        objMFGProcessIssueProperty.form_id = m_numForm_id;
                        objMFGProcessIssueProperty.rough_cut_id = Val.ToInt32(drw["rough_cut_id"]);
                        objMFGProcessIssueProperty.kapan_id = Val.ToInt64(drw["kapan_id"]); // m_kapan_id;
                        objMFGProcessIssueProperty.receive_union_id = IntRes;
                        objMFGProcessIssueProperty.history_union_id = NewHistory_Union_Id;
                        objMFGProcessIssueProperty.lot_srno = Lot_SrNo;

                        objMFGProcessIssueProperty = objMFGProcessWeightLoss.Save(objMFGProcessIssueProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                        IntRes = objMFGProcessIssueProperty.receive_union_id;
                        NewHistory_Union_Id = Val.ToInt64(objMFGProcessIssueProperty.history_union_id);
                        Lot_SrNo = Val.ToInt64(objMFGProcessIssueProperty.lot_srno);

                        Count++;
                        IntCounter++;
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
        private void backgroundWorker_ProcRecWeightLoss_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                panelProgress.Visible = false;
                if (IntRes > 0)
                {
                    Global.Confirm("Process Recieve Data Save Succesfully");
                    grdProcessWeightLossRecieve.DataSource = null;
                    ClearDetails();
                }
                else
                {
                    Global.Confirm("Error In Process Recieve");
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (AddInGrid())
                {
                    txtReturnPcs.Text = "0";
                    txtReturnCarat.Text = "0";
                    txtLossPcs.Text = "0";
                    txtLossCarat.Text = "0";
                    txtLostPcs.Text = "0";
                    txtLostCarat.Text = "0";
                    txtRejPcs.Text = "0";
                    txtRejCarat.Text = "0";
                    lblOsCarat.Text = "0";
                    lblOsPcs.Text = "0";
                    m_manager_id = 0;
                    m_emp_id = 0;
                    m_BalCarat = 0;
                    chkWithoutLoss.Checked = false;
                    lueCutNo.Focus();
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            ObjPer.SetFormPer();
            if (ObjPer.AllowUpdate == false || ObjPer.AllowInsert == false)
            {
                Global.Message(BLL.GlobalDec.gStrPermissionInsUpdMsg);
                return;
            }
            MFGProcessWeightLossRecieve objProcessWeightLoss = new MFGProcessWeightLossRecieve();
            DataTable dtProcess = new DataTable();
            int i = 1;
            foreach (DataRow drw in m_dtbReceiveProcess.Rows)
            {
                dtProcess = objProcessWeightLoss.CheckProcess(Val.ToInt(drw["lot_id"]));
                if (Val.ToInt(drw["process_id"]) != Val.ToInt(dtProcess.Rows[0]["process_id"]))
                {
                    Global.Message("Last Process is different in row:" + i);
                    return;
                }
                i++;
            }
            DialogResult result = MessageBox.Show("Do you want to delete data?", "Confirmation", MessageBoxButtons.YesNoCancel);
            if (result != DialogResult.Yes)
            {
                btnSave.Enabled = true;
                return;
            }

            Cursor.Current = Cursors.Default;
            Conn = new BeginTranConnection(true, false);
            MFGProcessWeightLossRecieve_Property objProcessRecieve_Property = new MFGProcessWeightLossRecieve_Property();
            objProcessWeightLoss = new MFGProcessWeightLossRecieve();

            foreach (DataRow drw in m_dtbReceiveProcess.Rows)
            {
                objProcessRecieve_Property.recieve_id = Val.ToInt(drw["recieve_id"]);
                objProcessRecieve_Property.issue_id = Val.ToInt(drw["issue_id"]);
                objProcessRecieve_Property.lot_id = Val.ToInt(drw["lot_id"]);
                objProcessRecieve_Property.loss_carat = Val.ToDecimal(drw["loss_carat"]);
                objProcessRecieve_Property.lost_carat = Val.ToDecimal(drw["lost_carat"]);
                IntRes = objProcessWeightLoss.Delete(objProcessRecieve_Property, DLL.GlobalDec.EnumTran.Continue, Conn);
            }
            Conn.Inter1.Commit();
            if (IntRes > 0)
            {
                Global.Confirm("Weight Loss Entry Delete Successfully");
                ClearDetails();
                PopulateDetails();
            }
            else
            {
                Global.Confirm("Error In Demand Noting Data");
                lueCutNo.Focus();
            }
        }
        private void RepDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (Global.Confirm("Are you sure delete selected row?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                dgvProcessWeightLossRecieve.DeleteRow(dgvProcessWeightLossRecieve.GetRowHandle(dgvProcessWeightLossRecieve.FocusedRowHandle));
                m_dtbReceiveProcess.AcceptChanges();
            }
        }
        private void FrmMFGProcessWeightLossRecieve_Load(object sender, EventArgs e)
        {
            //Global.LOOKUPEmp(lueEmployee);
            //Global.LOOKUPManager(lueManager);

            Global.LOOKUPProcess(lueProcess);
            Global.LOOKUPSubProcess(lueSubProcess);

            m_dtbSubProcess = (((DataTable)lueSubProcess.Properties.DataSource).Copy());

            dtpReceiveDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            dtpReceiveDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            dtpReceiveDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            dtpReceiveDate.Properties.CharacterCasing = CharacterCasing.Upper;
            dtpReceiveDate.EditValue = DateTime.Now;

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

            m_dtbKapan = Global.GetKapanAll();

            lueKapan.Properties.DataSource = m_dtbKapan;
            lueKapan.Properties.ValueMember = "kapan_id";
            lueKapan.Properties.DisplayMember = "kapan_no";

            m_dtbParam = Global.GetRoughCutAll();

            lueCutNo.Properties.DataSource = m_dtbParam;
            lueCutNo.Properties.ValueMember = "rough_cut_id";
            lueCutNo.Properties.DisplayMember = "rough_cut_no";

            if (GlobalDec.gEmployeeProperty.role_name == "SURAT ROUGH" || GlobalDec.gEmployeeProperty.role_name == "SURAT KAMALA")
            {
                chkWithoutLoss.Visible = true;
                chkWithoutLoss.Checked = false;
            }
            else
            {
                chkWithoutLoss.Visible = false;
                chkWithoutLoss.Checked = false;
            }

            // Add By Praful On 29072021

            //DTab_KapanWiseData = Global.GetRoughStockWise(Val.ToInt(0), Val.ToInt32(0));

            // End By Praful On 29072021

            ClearDetails();
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

                panelProgress.Visible = true;
                backgroundWorker_ProcRecWeightLoss.RunWorkerAsync();

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
        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            if (!PopulateDetails())
                return;
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            Global.Export("xlsx", dgvProcessWeightLossRecieve);
        }
        private void lueProcess_EditValueChanged_2(object sender, EventArgs e)
        {
            try
            {
                if (lueProcess.Text != "LS" && lueProcess.Text != "LS ASSORT" && lueProcess.Text != "2P SIGN CHECK" && lueProcess.Text != "FARSHI ASSORT" && lueProcess.Text != "LS SOWING" && lueProcess.Text != "2P SOWING" && lueProcess.Text != "EX SOWING" && lueProcess.Text != "GALAXY")
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
                            m_manager_id = Val.ToInt(dtIss.Rows[0]["manager_id"]);
                            m_emp_id = Val.ToInt(dtIss.Rows[0]["employee_id"]);
                            m_dtOutstanding = objProcessRecieve.Carat_OutStanding_GetData(Val.ToInt64(txtLotID.Text), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue), 1, "R");
                            if (m_dtOutstanding.Rows.Count > 0)
                            {
                                m_OsCarat = Val.ToInt32(m_dtOutstanding.Rows[0]["carat"]);
                                txtReturnCarat.Text = Val.ToString(m_OsCarat);
                                txtReturnPcs.Text = Val.ToString(m_dtOutstanding.Rows[0]["pcs"]);
                                lblOsPcs.Text = Val.ToString(m_dtOutstanding.Rows[0]["pcs"]);
                                lblOsCarat.Text = Val.ToString(m_dtOutstanding.Rows[0]["carat"]);
                            }
                            else
                            {
                                txtReturnCarat.Text = "0";
                                txtReturnPcs.Text = "0";
                                lblOsPcs.Text = "0";
                                lblOsCarat.Text = "0.00";
                            }
                        }
                        else
                        {
                            Global.Message("Lot not issue in this process.");
                        }
                    }
                }
                else
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
                if (lueCutNo.EditValue != null && lueKapan.EditValue != null && txtLotID.Text == "")
                {
                    m_IsLot = 0;
                }
                if (lueCutNo.EditValue != System.DBNull.Value)
                {
                    if (m_dtbParam.Rows.Count > 0 && m_IsLot == 0)
                    {
                        DataRow[] dr = m_dtbParam.Select("rough_cut_no ='" + Val.ToString(lueCutNo.Text) + "'");
                        if (m_IsLot == 0)
                        {
                            txtLotID.Text = Val.ToString(dr[0]["lot_id"]);
                        }
                        if ((lueProcess.Text != "LS" && lueProcess.Text != "") && (lueProcess.Text != "LS ASSORT" && lueProcess.Text != "") && (lueProcess.Text != "2P SIGN CHECK" && lueProcess.Text != "") && (lueProcess.Text != "FARSHI ASSORT" && lueProcess.Text != "") && (lueProcess.Text != "LS SOWING" && lueProcess.Text != "") && (lueProcess.Text != "2P SOWING" && lueProcess.Text != "") && (lueProcess.Text != "EX SOWING" && lueProcess.Text != "") && (lueProcess.Text != "GALAXY" && lueProcess.Text != ""))
                        {
                            if (txtLotID.Text != string.Empty || Val.ToInt64(txtLotID.Text) != 0)
                            {
                                m_dtOutstanding = objProcessRecieve.Carat_OutStanding_GetData(Val.ToInt64(txtLotID.Text), Val.ToInt32(0), Val.ToInt32(0), 1, "R");
                                if (m_dtOutstanding.Rows.Count > 0)
                                {
                                    lueProcess.Properties.DataSource = m_dtOutstanding;
                                    lueProcess.Properties.DisplayMember = "process_name";
                                    lueProcess.Properties.ValueMember = "process_id";
                                }
                                else
                                {
                                    Global.Message("No issue Data found!!!");
                                    lueProcess.EditValue = null;
                                    lueSubProcess.EditValue = null;
                                    lblOsPcs.Text = "0";
                                    lblOsCarat.Text = "0.00";
                                    txtReturnPcs.Text = "";
                                    txtReturnCarat.Text = "";
                                    lueProcess.Enabled = true;
                                    lueSubProcess.Enabled = true;
                                    txtLotID.Focus();
                                    return;
                                }
                                if (m_dtOutstanding.Rows.Count == 1)
                                {
                                    lueProcess.Text = Val.ToString(m_dtOutstanding.Rows[0]["process_name"]);
                                    lueSubProcess.Text = Val.ToString(m_dtOutstanding.Rows[0]["sub_process_name"]);
                                    lblOsPcs.Text = Val.ToString(m_dtOutstanding.Rows[0]["pcs"]);
                                    lblOsCarat.Text = Val.ToString(m_dtOutstanding.Rows[0]["carat"]);
                                    txtReturnPcs.Text = Val.ToString(m_dtOutstanding.Rows[0]["pcs"]);
                                    txtReturnCarat.Text = Val.ToString(m_dtOutstanding.Rows[0]["carat"]);
                                    m_OsCarat = Val.ToDecimal(txtReturnCarat.Text);
                                    lueProcess.Enabled = false;
                                    lueSubProcess.Enabled = false;
                                }
                                else
                                {
                                    Global.Message("No issue Data found!!!");
                                    lueProcess.EditValue = null;
                                    lueSubProcess.EditValue = null;
                                    lblOsPcs.Text = "0";
                                    lblOsCarat.Text = "0";
                                    txtReturnPcs.Text = "";
                                    txtReturnCarat.Text = "";
                                    lueProcess.Enabled = true;
                                    lueSubProcess.Enabled = true;
                                    txtLotID.Focus();
                                    return;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void txtLotID_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtLotID.Text != string.Empty || Val.ToInt64(txtLotID.Text) != 0)
                {
                    GetOsCarat(Val.ToInt64(txtLotID.Text));
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void lueSubProcess_EditValueChanged(object sender, EventArgs e)
        {
            if (lueProcess.EditValue != null && lueSubProcess.EditValue != System.DBNull.Value && Val.ToInt64(txtLotID.Text) != 0)
            {
                if (lueProcess.Text != "LS" && lueProcess.Text != "LS ASSORT" && lueProcess.Text != "2P SIGN CHECK" && lueProcess.Text != "FARSHI ASSORT" && lueProcess.Text != "LS SOWING" && lueProcess.Text != "2P SOWING" && lueProcess.Text != "EX SOWING" && lueProcess.Text != "GALAXY")
                {
                    DataTable dtIss = new DataTable();
                    dtIss = objProcessRecieve.GetIssueID(Val.ToInt64(txtLotID.Text), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue));
                    if (dtIss.Rows.Count > 0)
                    {
                        m_manager_id = Val.ToInt(dtIss.Rows[0]["manager_id"]);
                        m_emp_id = Val.ToInt(dtIss.Rows[0]["employee_id"]);
                        m_issue_id = Val.ToInt(dtIss.Rows[0]["issue_id"]);
                        m_dtOutstanding = objProcessRecieve.Carat_OutStanding_GetData(Val.ToInt64(txtLotID.Text), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue), 1, "R");
                        if (m_dtOutstanding.Rows.Count > 0)
                        {
                            txtReturnCarat.Text = Val.ToString(m_dtOutstanding.Rows[0]["carat"]);
                            txtReturnPcs.Text = Val.ToString(m_dtOutstanding.Rows[0]["pcs"]);
                            m_OsPcs = Val.ToInt(m_dtOutstanding.Rows[0]["pcs"]);
                            m_OsCarat = Val.ToDecimal(txtReturnCarat.Text);
                            lblOsCarat.Text = Val.ToString(m_OsCarat);
                            lblOsPcs.Text = Val.ToString(m_dtOutstanding.Rows[0]["pcs"]);
                        }
                        else
                        {
                            txtReturnPcs.Text = "0";
                            txtReturnCarat.Text = "0.00";
                            lblOsCarat.Text = "0.00";
                            lblOsPcs.Text = "0";
                        }
                    }
                    else
                    {
                        Global.Message("Lot not issue in this process.");
                    }
                }
                else
                {
                    btnPopUpStock.Enabled = true;
                }
            }
        }
        private void txtLossCarat_Validated(object sender, EventArgs e)
        {
            txtReturnCarat.Text = Val.ToString(m_OsCarat - (Val.ToDecimal(txtLossCarat.Text) + Val.ToDecimal(txtLostCarat.Text) + Val.ToDecimal(txtRejCarat.Text)));
            if (Val.ToDecimal(m_OsCarat) < (Val.ToDecimal(txtLossCarat.Text) + Val.ToDecimal(txtLostCarat.Text) + Val.ToDecimal(txtReturnCarat.Text) + Val.ToDecimal(txtRejCarat.Text)))
            {
                Global.Message("Carat not greater than Oustanding Carat.");
            }
        }
        private void txtLostCarat_Validated(object sender, EventArgs e)
        {
            txtReturnCarat.Text = Val.ToString(m_OsCarat - (Val.ToDecimal(txtLossCarat.Text) + Val.ToDecimal(txtLostCarat.Text) + Val.ToDecimal(txtRejCarat.Text)));
            if (Val.ToDecimal(m_OsCarat) < (Val.ToDecimal(txtLossCarat.Text) + Val.ToDecimal(txtLostCarat.Text) + Val.ToDecimal(txtReturnCarat.Text) + Val.ToDecimal(txtRejCarat.Text)))
            {
                Global.Message("Carat not greater than Oustanding Carat.");
            }

        }
        private void txtReturnCarat_Validated(object sender, EventArgs e)
        {
            if (Val.ToDecimal(m_OsCarat) < (Val.ToDecimal(txtLossCarat.Text) + Val.ToDecimal(txtLostCarat.Text) + Val.ToDecimal(txtReturnCarat.Text) + Val.ToDecimal(txtRejCarat.Text)))
            {
                Global.Message("Carat not greater than Oustanding Carat.");
            }
        }
        private void txtRejCarat_Validated(object sender, EventArgs e)
        {
            txtReturnCarat.Text = Val.ToString(m_OsCarat - (Val.ToDecimal(txtLossCarat.Text) + Val.ToDecimal(txtLostCarat.Text) + Val.ToDecimal(txtRejCarat.Text)));

            if (Val.ToDecimal(m_OsCarat) < (Val.ToDecimal(txtLossCarat.Text) + Val.ToDecimal(txtLostCarat.Text) + Val.ToDecimal(txtReturnCarat.Text) + Val.ToDecimal(txtRejCarat.Text)))
            {
                Global.Message("Carat not greater than Oustanding Carat.");
            }
        }
        private void txtLossPcs_Validated(object sender, EventArgs e)
        {
            txtReturnPcs.Text = Val.ToString(m_OsPcs - (Val.ToInt(txtLossPcs.Text) + Val.ToInt(txtLostPcs.Text) + Val.ToInt(txtRejPcs.Text)));
            if (Val.ToInt(m_OsPcs) < (Val.ToInt(txtLossPcs.Text) + Val.ToInt(txtLostPcs.Text) + Val.ToInt(txtReturnPcs.Text) + Val.ToInt(txtRejPcs.Text)))
            {
                Global.Message("Pcs not greater than Oustanding Pcs.");
            }
        }
        private void txtLostPcs_Validated(object sender, EventArgs e)
        {
            txtReturnPcs.Text = Val.ToString(m_OsPcs - (Val.ToInt(txtLossPcs.Text) + Val.ToInt(txtLostPcs.Text) + Val.ToInt(txtRejPcs.Text)));
            if (Val.ToInt(m_OsPcs) < (Val.ToInt(txtLossPcs.Text) + Val.ToInt(txtLostPcs.Text) + Val.ToInt(txtReturnPcs.Text) + Val.ToInt(txtRejPcs.Text)))
            {
                Global.Message("Pcs not greater than Oustanding Pcs.");
            }
        }
        private void txtRejPcs_Validated(object sender, EventArgs e)
        {
            txtReturnPcs.Text = Val.ToString(m_OsPcs - (Val.ToInt(txtLossPcs.Text) + Val.ToInt(txtLostPcs.Text) + Val.ToInt(txtRejPcs.Text)));

            if (Val.ToInt(m_OsPcs) < (Val.ToInt(txtLossPcs.Text) + Val.ToInt(txtLostPcs.Text) + Val.ToInt(txtReturnPcs.Text) + Val.ToInt(txtRejPcs.Text)))
            {
                Global.Message("Pcs not greater than Oustanding Pcs.");
            }
        }
        private void txtReturnPcs_Validated(object sender, EventArgs e)
        {
            if (Val.ToInt(m_OsPcs) < (Val.ToInt(txtLossPcs.Text) + Val.ToInt(txtLostPcs.Text) + Val.ToInt(txtReturnPcs.Text) + Val.ToInt(txtRejPcs.Text)))
            {
                Global.Message("Pcs not greater than Oustanding Pcs.");
            }
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
                lueCutNo.Properties.DataSource = m_dtbParam;
                lueCutNo.Properties.ValueMember = "rough_cut_id";
                lueCutNo.Properties.DisplayMember = "rough_cut_no";
            }
        }
        private void txtReturnCarat_EditValueChanged(object sender, EventArgs e)
        {
            if (GlobalDec.gEmployeeProperty.department_name == "LS SOWING" || GlobalDec.gEmployeeProperty.department_name == "SOWING")
            {
                txtLossCarat.Text = Val.ToDecimal(Val.ToDecimal(lblOsCarat.Text) - Val.ToDecimal(txtReturnCarat.Text)).ToString();
            }
        }
        private void btnSearchRec_Click(object sender, EventArgs e)
        {
            FrmMFGSearchProcess FrmSearchProcess = new FrmMFGSearchProcess();
            FrmSearchProcess.FrmMFGProcessWeightLossRecieve = this;
            //FrmSearchProcess.DTab = DtPending;
            FrmSearchProcess.ShowForm(this);
        }
        private void txtReturnPcs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtReturnCarat_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txtLossPcs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtLossCarat_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txtLostPcs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtLostCarat_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txtRejPcs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtRejCarat_KeyPress(object sender, KeyPressEventArgs e)
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

        #region Grid Event
        private void dgvProcessWeightLossRecieve_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvProcessWeightLossRecieve.GetDataRow(e.RowHandle);
                        btnAdd.Text = "&Update";
                        lueCutNo.Text = Val.ToString(Drow["rough_cut_no"]);
                        txtLotID.Text = Val.ToString(Drow["lot_id"]);
                        txtReturnPcs.Text = Val.ToString(Drow["return_pcs"]);
                        txtReturnCarat.Text = Val.ToString(Drow["return_carat"]);
                        txtLossPcs.Text = Val.ToString(Drow["loss_pcs"]);
                        txtLossCarat.Text = Val.ToString(Drow["loss_carat"]);
                        txtLostPcs.Text = Val.ToString(Drow["lost_pcs"]);
                        txtLostCarat.Text = Val.ToString(Drow["lost_carat"]);
                        lueProcess.EditValue = Val.ToInt32(Drow["process_id"]);
                        lueProcess.Text = Val.ToString(Drow["process"]);
                        lueSubProcess.EditValue = Val.ToInt32(Drow["sub_process_id"]);
                        m_issue_id = Val.ToInt32(Drow["issue_id"]);
                        m_numlosscarat = Val.ToDecimal(Drow["loss_carat"]);
                        m_numlostcarat = Val.ToDecimal(Drow["lost_carat"]);
                        m_cut_no = Val.ToString(Drow["rough_cut_no"]);
                        m_update_srno = Val.ToInt(Drow["sr_no"]);
                        m_old_losscarat = Val.ToDecimal(Drow["loss_carat"]);
                        m_old_lostcarat = Val.ToDecimal(Drow["lost_carat"]);
                        m_manager_id = Val.ToInt(Drow["manager_id"]);
                        m_emp_id = Val.ToInt(Drow["employee_id"]);
                        m_OsCarat = Val.ToDecimal(txtReturnCarat.Text);
                        m_OsPcs = Val.ToInt(txtReturnPcs.Text);
                        decimal OS_Carat = Val.ToDecimal(txtReturnCarat.Text) + Val.ToDecimal(txtLossCarat.Text) + Val.ToDecimal(txtRejCarat.Text) + Val.ToDecimal(txtLostCarat.Text);
                        int OS_Pcs = Val.ToInt(txtReturnPcs.Text) + Val.ToInt(txtLossPcs.Text) + Val.ToInt(txtRejPcs.Text) + Val.ToInt(txtLostPcs.Text);
                        lblOsCarat.Text = OS_Carat.ToString();
                        lblOsPcs.Text = OS_Pcs.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void dgvWeightLossRec_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvWeightLossRec.GetDataRow(e.RowHandle);
                        DataRow drwNew = m_dtbReceiveProcess.NewRow();

                        int numLossPcs = Val.ToInt32(Drow["loss_pcs"]);
                        decimal numLossCarat = Val.ToDecimal(Drow["loss_carat"]);
                        int numLostPcs = Val.ToInt32(Drow["lost_pcs"]);
                        decimal numLostCarat = Val.ToDecimal(Drow["lost_carat"]);
                        int numReturnPcs = Val.ToInt32(Drow["pcs"]);
                        decimal numReturnCarat = Val.ToDecimal(Drow["carat"]);
                        dtpReceiveDate.Text = Val.ToString(Drow["receive_date"]);
                        drwNew["recieve_id"] = Val.ToInt32(Drow["receive_id"]);
                        drwNew["recieve_date"] = Val.DBDate(Drow["receive_date"].ToString());
                        drwNew["rough_cut_no"] = Val.ToString(Drow["rough_cut_no"]);
                        drwNew["lot_id"] = Val.ToInt32(Drow["lot_id"]);
                        drwNew["manager_id"] = Val.ToInt32(Drow["manager_id"]);
                        drwNew["employee_id"] = Val.ToInt32(Drow["employee_id"]);
                        drwNew["process_id"] = Val.ToInt32(Drow["process_id"]);
                        drwNew["sub_process_id"] = Val.ToInt32(Drow["sub_process_id"]);
                        drwNew["issue_id"] = Val.ToInt32(Drow["issue_id"]);
                        drwNew["return_pcs"] = numReturnPcs;
                        drwNew["return_carat"] = numReturnCarat;
                        drwNew["loss_pcs"] = numLossPcs;
                        drwNew["loss_carat"] = numLossCarat;
                        drwNew["lost_pcs"] = numLostPcs;
                        drwNew["lost_carat"] = numLostCarat;
                        drwNew["sr_no"] = m_Srno;
                        m_dtbReceiveProcess.Rows.Add(drwNew);
                        grdProcessWeightLossRecieve.DataSource = m_dtbReceiveProcess;

                        ttlbWeightLossRecieve.SelectedTabPage = tblWeightLossdetail;
                        lueCutNo.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }

        #endregion

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
                    DataRow drwNew = m_dtbReceiveProcess.NewRow();
                    int numLossPcs = Val.ToInt(txtLossPcs.Text);
                    decimal numLossCarat = Val.ToDecimal(txtLossCarat.Text);
                    int numLostPcs = Val.ToInt(txtLostPcs.Text);
                    decimal numLostCarat = Val.ToDecimal(txtLostCarat.Text);
                    int numReturnPcs = Val.ToInt(txtReturnPcs.Text);
                    decimal numReturnCarat = Val.ToDecimal(txtReturnCarat.Text);
                    int numRejPcs = Val.ToInt(txtRejPcs.Text);
                    decimal numRejCarat = Val.ToDecimal(txtRejCarat.Text);
                    m_BalCarat = (m_OsCarat - (numLossCarat + numLostCarat + numReturnCarat));
                    drwNew["recieve_id"] = Val.ToInt(0);
                    drwNew["recieve_date"] = Val.DBDate(dtpReceiveDate.Text);
                    drwNew["rough_cut_no"] = Val.ToString(lueCutNo.Text);
                    drwNew["rough_cut_id"] = Val.ToString(lueCutNo.EditValue);
                    drwNew["lot_id"] = Val.ToInt64(txtLotID.Text);
                    drwNew["kapan_id"] = Val.ToString(lueKapan.EditValue);
                    drwNew["manager_id"] = Val.ToInt(m_manager_id);
                    drwNew["employee_id"] = Val.ToInt(m_emp_id);
                    drwNew["process_id"] = Val.ToInt(lueProcess.EditValue);
                    drwNew["process"] = Val.ToString(lueProcess.Text);
                    drwNew["sub_process_id"] = Val.ToInt(lueSubProcess.EditValue);
                    drwNew["subprocess"] = Val.ToString(lueSubProcess.Text);
                    drwNew["issue_id"] = Val.ToInt(m_issue_id);
                    drwNew["return_pcs"] = numReturnPcs;
                    drwNew["return_carat"] = numReturnCarat;
                    drwNew["loss_pcs"] = numLossPcs;
                    drwNew["loss_carat"] = numLossCarat;
                    drwNew["lost_pcs"] = numLostPcs;
                    drwNew["lost_carat"] = numLostCarat;
                    drwNew["rejection_pcs"] = numRejPcs;
                    drwNew["rejection_carat"] = numRejCarat;
                    drwNew["sr_no"] = m_Srno;
                    drwNew["balance"] = m_BalCarat;
                    m_dtbReceiveProcess.Rows.Add(drwNew);
                    m_Srno++;
                }
                else if (btnAdd.Text == "&Update")
                {

                    if (m_dtbReceiveProcess.Select("sr_no ='" + Val.ToInt(m_update_srno) + "'").Length > 0)
                    {
                        for (int i = 0; i < m_dtbReceiveProcess.Rows.Count; i++)
                        {
                            if (m_dtbReceiveProcess.Select("rough_cut_no ='" + m_cut_no + "' AND sr_no = '" + m_update_srno + "'").Length > 0)
                            {
                                if (m_dtbReceiveProcess.Rows[dgvProcessWeightLossRecieve.FocusedRowHandle]["rough_cut_no"].ToString() == m_cut_no.ToString())
                                {
                                    m_dtbReceiveProcess.Rows[dgvProcessWeightLossRecieve.FocusedRowHandle]["issue_id"] = m_issue_id;
                                    m_dtbReceiveProcess.Rows[dgvProcessWeightLossRecieve.FocusedRowHandle]["lot_id"] = Val.ToString(txtLotID.Text);
                                    m_dtbReceiveProcess.Rows[dgvProcessWeightLossRecieve.FocusedRowHandle]["return_pcs"] = Val.ToInt(txtReturnPcs.Text).ToString();
                                    m_dtbReceiveProcess.Rows[dgvProcessWeightLossRecieve.FocusedRowHandle]["return_carat"] = Val.ToString(txtReturnCarat.Text).ToString();
                                    m_dtbReceiveProcess.Rows[dgvProcessWeightLossRecieve.FocusedRowHandle]["loss_pcs"] = Val.ToInt(txtLossPcs.Text).ToString();
                                    m_dtbReceiveProcess.Rows[dgvProcessWeightLossRecieve.FocusedRowHandle]["loss_carat"] = Val.ToDecimal(txtLossCarat.Text);
                                    m_dtbReceiveProcess.Rows[dgvProcessWeightLossRecieve.FocusedRowHandle]["lost_carat"] = Val.ToDecimal(txtLostCarat.Text).ToString();
                                    m_dtbReceiveProcess.Rows[dgvProcessWeightLossRecieve.FocusedRowHandle]["lost_pcs"] = Val.ToInt(txtLostPcs.Text);
                                    m_dtbReceiveProcess.Rows[dgvProcessWeightLossRecieve.FocusedRowHandle]["rejection_carat"] = Val.ToDecimal(txtRejCarat.Text).ToString();
                                    m_dtbReceiveProcess.Rows[dgvProcessWeightLossRecieve.FocusedRowHandle]["rejection_pcs"] = Val.ToInt(txtRejPcs.Text);
                                    m_BalCarat = (m_OsCarat - (Val.ToDecimal(txtLossCarat.Text) + Val.ToDecimal(txtLostCarat.Text) + Val.ToDecimal(txtReturnCarat.Text) + Val.ToDecimal(txtRejCarat.Text)));
                                    m_dtbReceiveProcess.Rows[dgvProcessWeightLossRecieve.FocusedRowHandle]["balance"] = Val.ToDecimal(m_BalCarat);
                                    m_dtbReceiveProcess.Rows[dgvProcessWeightLossRecieve.FocusedRowHandle]["process_id"] = Val.ToInt(lueProcess.EditValue);
                                    m_dtbReceiveProcess.Rows[dgvProcessWeightLossRecieve.FocusedRowHandle]["process"] = Val.ToString(lueProcess.Text);
                                    m_dtbReceiveProcess.Rows[dgvProcessWeightLossRecieve.FocusedRowHandle]["sub_process_id"] = Val.ToInt(lueSubProcess.EditValue);
                                    m_dtbReceiveProcess.Rows[dgvProcessWeightLossRecieve.FocusedRowHandle]["subprocess"] = Val.ToString(lueSubProcess.Text);
                                    m_dtbReceiveProcess.Rows[dgvProcessWeightLossRecieve.FocusedRowHandle]["employee_id"] = Val.ToInt(m_manager_id).ToString();
                                    m_dtbReceiveProcess.Rows[dgvProcessWeightLossRecieve.FocusedRowHandle]["employee_id"] = Val.ToInt(m_emp_id).ToString();
                                    break;
                                }
                            }
                        }
                    }
                    btnAdd.Text = "&Add";
                }
                dgvProcessWeightLossRecieve.MoveLast();
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
                    if (m_dtbReceiveProcess.Rows.Count == 0)
                    {
                        lstError.Add(new ListError(22, "Record"));
                        if (!blnFocus)
                        {
                            blnFocus = true;

                        }
                    }

                    if (GlobalDec.gEmployeeProperty.role_name.ToUpper() != "SURAT KAMALA")
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
                    if (lueProcess.Text != "LS" && lueProcess.Text != "LS ASSORT" && lueProcess.Text != "2P SIGN CHECK" && lueProcess.Text != "FARSHI ASSORT" && lueProcess.Text != "LS SOWING" && lueProcess.Text != "2P SOWING" && lueProcess.Text != "EX SOWING" && lueProcess.Text != "GALAXY")
                    {
                        if (Val.ToInt(lblOsPcs.Text) > 0)
                        {
                            if ((txtLossPcs.Text == string.Empty || txtLossPcs.Text == "0") && (txtLostPcs.Text == string.Empty || txtLostPcs.Text == "0") && (txtRejPcs.Text == string.Empty || txtRejPcs.Text == "0"))
                            {
                                lstError.Add(new ListError(5, "Loss Pcs can not be blank!!!"));
                                lstError.Add(new ListError(5, "Lost Pcs can not be blank!!!"));
                                lstError.Add(new ListError(5, "Rejection Pcs can not be blank!!!"));
                                if (!blnFocus)
                                {
                                    blnFocus = true;
                                    txtLossPcs.Focus();
                                }
                            }
                        }
                    }
                    if (GlobalDec.gEmployeeProperty.role_name == "SURAT ROUGH" || GlobalDec.gEmployeeProperty.role_name == "SURAT KAMALA")
                    {
                        if (chkWithoutLoss.Checked == true)
                        {

                        }
                        else
                        {
                            if ((txtLossCarat.Text == string.Empty || txtLossCarat.Text == "0") && (txtLostCarat.Text == string.Empty || txtLostCarat.Text == "0") && (txtRejCarat.Text == string.Empty || txtRejCarat.Text == "0"))
                            {
                                lstError.Add(new ListError(5, "Loss Carat can not be blank!!!"));
                                lstError.Add(new ListError(5, "Lost Carat can not be blank!!!"));
                                lstError.Add(new ListError(5, "Rejection Carat can not be blank!!!"));
                                if (!blnFocus)
                                {
                                    blnFocus = true;
                                    txtLossCarat.Focus();
                                }
                            }
                        }
                    }
                    else
                    {
                        if ((txtLossCarat.Text == string.Empty || txtLossCarat.Text == "0") && (txtLostCarat.Text == string.Empty || txtLostCarat.Text == "0") && (txtRejCarat.Text == string.Empty || txtRejCarat.Text == "0"))
                        {
                            lstError.Add(new ListError(5, "Loss Carat can not be blank!!!"));
                            lstError.Add(new ListError(5, "Lost Carat can not be blank!!!"));
                            lstError.Add(new ListError(5, "Rejection Carat can not be blank!!!"));
                            if (!blnFocus)
                            {
                                blnFocus = true;
                                txtLossCarat.Focus();
                            }
                        }
                        chkWithoutLoss.Visible = false;
                        chkWithoutLoss.Checked = false;
                    }
                    if (Val.ToInt(txtLostPcs.Text) > 0 && txtLostPcs.Text != string.Empty)
                    {
                        if (Val.ToInt(lblOsPcs.Text) < (Val.ToInt(dgvProcessWeightLossRecieve.Columns["loss_pcs"].SummaryText) + Val.ToInt(dgvProcessWeightLossRecieve.Columns["lost_pcs"].SummaryText) + Val.ToInt(txtLostPcs.Text) + Val.ToInt(dgvProcessWeightLossRecieve.Columns["rejection_pcs"].SummaryText) + Val.ToInt(txtRejPcs.Text) + Val.ToInt(dgvProcessWeightLossRecieve.Columns["return_pcs"].SummaryText) + Val.ToInt(txtReturnPcs.Text)))
                        {
                            lstError.Add(new ListError(5, "Entry Pcs not greater than total Pcs"));
                            if (!blnFocus)
                            {
                                blnFocus = true;
                                txtLostPcs.Focus();
                            }
                        }
                    }
                    if (Val.ToDecimal(txtLostCarat.Text) > 0 && txtLostCarat.Text != string.Empty)
                    {
                        if (Val.ToDecimal(lblOsCarat.Text) < (Val.ToDecimal(dgvProcessWeightLossRecieve.Columns["loss_carat"].SummaryText) + Val.ToDecimal(dgvProcessWeightLossRecieve.Columns["lost_carat"].SummaryText) + Val.ToDecimal(txtLostCarat.Text) + Val.ToDecimal(dgvProcessWeightLossRecieve.Columns["rejection_carat"].SummaryText) + Val.ToDecimal(txtRejCarat.Text) + Val.ToDecimal(dgvProcessWeightLossRecieve.Columns["return_carat"].SummaryText) + Val.ToDecimal(txtReturnCarat.Text)))
                        {
                            lstError.Add(new ListError(5, "Entry Carat not greater than total carat"));
                            if (!blnFocus)
                            {
                                blnFocus = true;
                                txtLostCarat.Focus();
                            }
                        }
                    }
                    if (Val.ToInt(txtLossPcs.Text) > 0 && txtLossPcs.Text != string.Empty)
                    {
                        if (Val.ToInt(lblOsPcs.Text) < (Val.ToInt(txtLossPcs.Text) + Val.ToInt(txtLostPcs.Text) + Val.ToInt(txtRejPcs.Text) + Val.ToInt(txtReturnPcs.Text)))
                        {
                            lstError.Add(new ListError(5, "Entry Pcs not greater than total pcs"));
                            if (!blnFocus)
                            {
                                blnFocus = true;
                                txtLossPcs.Focus();
                            }
                        }
                    }
                    if (Val.ToDecimal(txtLossCarat.Text) > 0 && txtLossCarat.Text != string.Empty)
                    {
                        if (Val.ToDecimal(lblOsCarat.Text) < (Val.ToDecimal(txtLossCarat.Text) + Val.ToDecimal(txtLostCarat.Text) + Val.ToDecimal(txtRejCarat.Text) + Val.ToDecimal(txtReturnCarat.Text)))
                        {
                            lstError.Add(new ListError(5, "Entry Carat not greater than total carat"));
                            if (!blnFocus)
                            {
                                blnFocus = true;
                                txtLossCarat.Focus();
                            }
                        }
                    }
                    if ((Val.ToInt(txtLostPcs.Text) > 0 && txtLostPcs.Text != string.Empty) && (Val.ToInt(txtLossPcs.Text) > 0 && txtLossPcs.Text != string.Empty))
                    {
                        if (Val.ToInt(lblOsPcs.Text) < (Val.ToInt(txtRejPcs.Text) + Val.ToInt(txtReturnPcs.Text) + Val.ToInt(txtLossPcs.Text) + Val.ToInt(txtLostPcs.Text)))
                        {
                            lstError.Add(new ListError(5, "Entry Pcs not greater than total pcs"));
                            if (!blnFocus)
                            {
                                blnFocus = true;
                                txtLossPcs.Focus();
                            }
                        }
                    }
                    if ((Val.ToDecimal(txtLostCarat.Text) > 0 && txtLostCarat.Text != string.Empty) && (Val.ToDecimal(txtLossCarat.Text) > 0 && txtLossCarat.Text != string.Empty))
                    {
                        if (Val.ToDecimal(lblOsCarat.Text) < (Val.ToDecimal(txtRejCarat.Text) + Val.ToDecimal(txtReturnCarat.Text) + Val.ToDecimal(txtLossCarat.Text) + Val.ToDecimal(txtLostCarat.Text)))
                        {
                            lstError.Add(new ListError(5, "Entry Carat not greater than total carat"));
                            if (!blnFocus)
                            {
                                blnFocus = true;
                                txtLossCarat.Focus();
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
                dtpReceiveDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpReceiveDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpReceiveDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpReceiveDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpReceiveDate.EditValue = DateTime.Now;

                lueKapan.EditValue = System.DBNull.Value;
                lueCutNo.EditValue = System.DBNull.Value;
                //lueManager.EditValue = System.DBNull.Value;
                //lueEmployee.EditValue = System.DBNull.Value;

                lueProcess.EditValue = System.DBNull.Value;
                lueSubProcess.EditValue = System.DBNull.Value;
                lueProcess.Enabled = true;
                lueSubProcess.Enabled = true;
                txtReturnPcs.Text = string.Empty;
                txtReturnCarat.Text = string.Empty;
                txtLossPcs.Text = string.Empty;
                txtLossCarat.Text = string.Empty;
                txtLostPcs.Text = string.Empty;
                txtLostCarat.Text = string.Empty;
                txtRejPcs.Text = string.Empty;
                txtRejCarat.Text = string.Empty;
                txtLotID.Text = string.Empty;
                lblOsCarat.Text = "0.00";
                lblOsPcs.Text = "0";
                m_manager_id = 0;
                m_emp_id = 0;
                m_BalCarat = 0;
                m_Srno = 1;
                m_IsLot = 0;
                m_update_srno = 1;
                btnAdd.Text = "&Add";
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
                if (m_dtbReceiveProcess.Rows.Count > 0)
                    m_dtbReceiveProcess.Rows.Clear();

                m_dtbReceiveProcess = new DataTable();

                m_dtbReceiveProcess.Columns.Add("recieve_id", typeof(int));
                m_dtbReceiveProcess.Columns.Add("recieve_date", typeof(DateTime));
                m_dtbReceiveProcess.Columns.Add("lot_id", typeof(int));
                m_dtbReceiveProcess.Columns.Add("rough_cut_no", typeof(string));
                m_dtbReceiveProcess.Columns.Add("rough_cut_id", typeof(string));
                m_dtbReceiveProcess.Columns.Add("manager_id", typeof(int));
                m_dtbReceiveProcess.Columns.Add("kapan_id", typeof(int)).DefaultValue = 0;
                m_dtbReceiveProcess.Columns.Add("employee_id", typeof(int));
                m_dtbReceiveProcess.Columns.Add("process", typeof(string));
                m_dtbReceiveProcess.Columns.Add("process_id", typeof(int));
                m_dtbReceiveProcess.Columns.Add("subprocess", typeof(string));
                m_dtbReceiveProcess.Columns.Add("sub_process_id", typeof(int));
                m_dtbReceiveProcess.Columns.Add("return_pcs", typeof(int)).DefaultValue = 0;
                m_dtbReceiveProcess.Columns.Add("return_carat", typeof(decimal));
                m_dtbReceiveProcess.Columns.Add("loss_pcs", typeof(int)).DefaultValue = 0;
                m_dtbReceiveProcess.Columns.Add("loss_carat", typeof(decimal));
                m_dtbReceiveProcess.Columns.Add("lost_pcs", typeof(int)).DefaultValue = 0;
                m_dtbReceiveProcess.Columns.Add("lost_carat", typeof(decimal));
                m_dtbReceiveProcess.Columns.Add("rejection_pcs", typeof(int)).DefaultValue = 0;
                m_dtbReceiveProcess.Columns.Add("rejection_carat", typeof(decimal));
                m_dtbReceiveProcess.Columns.Add("sr_no", typeof(int)).DefaultValue = 1;
                m_dtbReceiveProcess.Columns.Add("issue_id", typeof(int)).DefaultValue = 0;
                m_dtbReceiveProcess.Columns.Add("balance", typeof(decimal));

                grdProcessWeightLossRecieve.DataSource = m_dtbReceiveProcess;
                grdProcessWeightLossRecieve.Refresh();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
        }
        private bool PopulateDetails()
        {
            bool blnReturn = true;
            MFGProcessWeightLossRecieve objWeightLossRec = new MFGProcessWeightLossRecieve();
            DateTime datFromDate = DateTime.MinValue;
            DateTime datToDate = DateTime.MinValue;
            try
            {
                m_dtbDetails = objWeightLossRec.GetData(Convert.ToDateTime(dtpFromDate.Text), Convert.ToDateTime(dtpToDate.Text));

                if (m_dtbDetails.Rows.Count == 0)
                {
                    Global.Message("Data Not Found");
                    blnReturn = false;
                }

                grdWeightLossRec.DataSource = m_dtbDetails;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            finally
            {
                objWeightLossRec = null;
            }

            return blnReturn;
        }
        public void GetOsCarat(Int64 lotId)
        {
            try
            {
                m_dtOutstanding = objProcessRecieve.GetBalanceCarat(lotId);

                if (m_dtOutstanding.Rows.Count > 0)
                {
                    m_kapan_id = Val.ToInt(m_dtOutstanding.Rows[0]["kapan_id"]);
                }
                else
                {
                    lblOsCarat.Text = "0";
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

                DtPending = objMFGProcessIssue.GetPendingLSIssueStock(objMFGProcessIssueProperty);

                FrmMFGStockConfirm FrmStockConfirm = new FrmMFGStockConfirm();
                FrmStockConfirm.FrmMFGProcessWeightLossRecieve = this;
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
                //if (GlobalDec.gEmployeeProperty.role_name == "SURAT KAMALA")
                //{
                //    foreach (DataRow drw in Stock_Data.Rows)
                //    {
                //        MFGProcessIssue objMFGProcessIssue = new MFGProcessIssue();
                //        MFGProcessIssueProperty objMFGProcessIssueProperty = new MFGProcessIssueProperty();

                //        objMFGProcessIssueProperty.lot_id = Val.ToInt64(drw["lot_id"]);
                //        objMFGProcessIssueProperty.process_id = Val.ToInt32(drw["process_id"]);
                //        objMFGProcessIssueProperty.sub_process_id = Val.ToInt32(drw["sub_process_id"]);
                //        objMFGProcessIssueProperty = objMFGProcessIssue.GetData_PrevProcessRec(objMFGProcessIssueProperty);

                //        if (objMFGProcessIssueProperty.Messgae != "" && objMFGProcessIssueProperty.Messgae != null)
                //        {
                //            Global.Message(objMFGProcessIssueProperty.Messgae);
                //            btnSave.Enabled = true;
                //            return;
                //        }
                //    }
                //}

                DTabTemp = Stock_Data.Copy();
                m_dtbReceiveProcess.AcceptChanges();
                if (m_dtbReceiveProcess != null)
                {
                    foreach (DataRow DRow in m_dtbReceiveProcess.Rows)
                    {
                        m_dtOutstanding = objProcessRecieve.Carat_OutStanding_GetData(Val.ToInt32(DRow["lot_id"]), Val.ToInt32(0), Val.ToInt32(0), 1, "R");
                        if (m_dtOutstanding.Rows.Count > 0)
                        {
                            //lueProcess.Properties.DataSource = m_dtOutstanding;
                            //lueProcess.Properties.DisplayMember = "process_name";
                            //lueProcess.Properties.ValueMember = "process_id";
                        }
                        else
                        {
                            Global.Message("This Lot ID =" + Val.ToInt32(DRow["lot_id"]) + " No issue Data found!!!");
                            lueProcess.EditValue = null;
                            lueSubProcess.EditValue = null;
                            lblOsPcs.Text = "0";
                            lblOsCarat.Text = "0.00";
                            txtReturnPcs.Text = "";
                            txtReturnCarat.Text = "";
                            lueProcess.Enabled = true;
                            lueSubProcess.Enabled = true;
                            txtLotID.Focus();
                            return;
                        }
                    }

                    if (m_dtbReceiveProcess.Rows.Count > 0)
                    {
                        for (int i = 0; i < m_dtbReceiveProcess.Rows.Count; i++)
                        {
                            for (int j = 0; j < DTabTemp.Rows.Count; j++)
                            {
                                if (m_dtbReceiveProcess.Rows[i]["lot_id"].ToString() == DTabTemp.Rows[j]["lot_id"].ToString())
                                {
                                    Global.Message(m_dtbReceiveProcess.Rows[i]["lot_id"].ToString() + " = Lot ID already added to the Issue list!");
                                    txtLotID.Text = "";
                                    txtLotID.Focus();
                                    return;
                                }
                            }
                        }
                    }
                }

                if (m_dtbReceiveProcess.Rows.Count > 0)
                {
                    DTabTemp = Stock_Data.Copy();

                    m_dtbReceiveProcess.Merge(DTabTemp);
                }
                else
                {
                    m_dtbReceiveProcess = Stock_Data.Copy();

                    foreach (DataRow DRow in m_dtbReceiveProcess.Rows)
                    {
                        m_dtOutstanding = objProcessRecieve.Carat_OutStanding_GetData(Val.ToInt32(DRow["lot_id"]), Val.ToInt32(0), Val.ToInt32(0), 1, "R");
                        if (m_dtOutstanding.Rows.Count > 0)
                        {
                            //lueProcess.Properties.DataSource = m_dtOutstanding;
                            //lueProcess.Properties.DisplayMember = "process_name";
                            //lueProcess.Properties.ValueMember = "process_id";
                        }
                        else
                        {
                            Global.Message("This Lot ID =" + Val.ToInt32(DRow["lot_id"]) + " No issue Data found!!!");
                            lueProcess.EditValue = null;
                            lueSubProcess.EditValue = null;
                            lblOsPcs.Text = "0";
                            lblOsCarat.Text = "0.00";
                            txtReturnPcs.Text = "";
                            txtReturnCarat.Text = "";
                            lueProcess.Enabled = true;
                            lueSubProcess.Enabled = true;
                            txtLotID.Focus();
                            return;
                        }
                    }
                }

                int Sr_No = 0;
                foreach (DataRow DRow in m_dtbReceiveProcess.Rows)
                {
                    DRow["recieve_date"] = Val.DBDate(dtpReceiveDate.Text);
                    //DRow["sub_process_id"] = Val.ToInt64(lueSubProcess.EditValue);
                    //DRow["subprocess"] = Val.ToString(lueSubProcess.Text);
                    //DRow["process_id"] = Val.ToInt64(lueProcess.EditValue);
                    //DRow["process"] = Val.ToString(lueProcess.Text);
                    Sr_No = Sr_No + 1;
                    DRow["sr_no"] = Sr_No;
                }

                grdProcessWeightLossRecieve.DataSource = m_dtbReceiveProcess;
                grdProcessWeightLossRecieve.RefreshDataSource();
                dgvProcessWeightLossRecieve.BestFitColumns();
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
            }
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
