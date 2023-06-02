using BLL;
using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.PropertyClasses.Transaction;
using BLL.PropertyClasses.Transaction.MFG;
using DERP.Class;
using DERP.Master;
using DERP.Master.MFG;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DREP.Master;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace DERP.Transaction.MFG
{
    public partial class FrmMFGJangedReturnManual : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        Control _NextEnteredControl;
        private List<Control> _tabControls;

        MFGProcessReceive objProcessRecieve;
        MFGJangedReturnManual objJangedReturn;

        DataTable DtControlSettings;
        DataTable m_dtbDetail;
        DataTable m_dtbParam;
        DataTable m_dtbSubProcess;
        DataTable m_dtbReceiveProcess;
        DataTable m_dtOutstanding;
        DataTable m_dtbDetails;
        DataTable m_dtbKapan;
        DataTable dtGrid;
        //DataTable DTab_KapanWiseData = new DataTable();

        int m_issue_id;
        int m_manager_id;
        int m_emp_id;
        int m_IsLot;
        Int64 m_numForm_id;
        Int64 IntRes;
        Int64 m_kapan_id;
        int m_emp;

        decimal m_OsCarat;
        int m_OsPcs;

        Int64 Prediction_Id;
        private bool comOpen;

        //private string readBuffer;

        //private int Bytenumber;

        //private char[] byteEnd;

        //[AccessedThroughProperty("SerialPort1")]
        //private SerialPort _SerialPort1;

        //[AccessedThroughProperty("BtnOpen")]
        //private Button _BtnOpen;

        DataTable dtIssOS;

        #endregion

        #region Constructor
        public FrmMFGJangedReturnManual()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            _NextEnteredControl = new Control();
            _tabControls = new List<Control>();

            objProcessRecieve = new MFGProcessReceive();
            objJangedReturn = new MFGJangedReturnManual();

            DtControlSettings = new DataTable();
            m_dtbDetail = new DataTable();
            m_dtbParam = new DataTable();
            m_dtbSubProcess = new DataTable();
            m_dtbReceiveProcess = new DataTable();
            m_dtOutstanding = new DataTable();
            m_dtbDetails = new DataTable();
            m_dtbKapan = new DataTable();
            dtIssOS = new DataTable();
            dtGrid = new DataTable();
            m_issue_id = 0;
            m_manager_id = 0;
            m_emp_id = 0;
            m_numForm_id = 0;
            m_kapan_id = 0;
            m_IsLot = 0;
            m_emp = 0;
            Prediction_Id = 0;
            m_OsCarat = 0;
            m_OsPcs = 0;
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

        #region Settings
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
        public void NumberCheckwithpoint(KeyPressEventArgs e, Control str, int S)
        {
            if ((Strings.Asc(e.KeyChar) < 0x30) | (Strings.Asc(e.KeyChar) > 0x39))
            {
                e.Handled = true;
            }
            if ((Strings.Asc(e.KeyChar) == 8) | (Strings.Asc(e.KeyChar) == 0x2e))
            {
                e.Handled = false;
            }
            if ((Strings.Asc(e.KeyChar) == 13) && (S != 0))
            {
                if (S == 1)
                {
                    str.Text = Strings.Format(Conversion.Val(str.Text), "#####0.0");
                }
                else if (S == 2)
                {
                    str.Text = Strings.Format(Conversion.Val(str.Text), "#####0.00");
                }
                else if (S == 3)
                {
                    str.Text = Strings.Format(Conversion.Val(str.Text), "#####0.000");
                }
            }
        }

        #endregion

        #region Events

        private void FrmMFGProcessWeightLossRecieve_Load(object sender, EventArgs e)
        {
            Global.LOOKUPEmp(lueEmployee);
            Global.LOOKUPAllManager(lueFromManager);
            Global.LOOKUPAllManager(lueToManager);
            Global.LOOKUPProcess(lueFromProcess);
            Global.LOOKUPProcess(lueToProcess);
            Global.LOOKUPSubProcess(lueFromSubProcess);
            Global.LOOKUPSubProcess(lueToSubProcess);
            Global.LOOKUPCompany(lueFromCompany);
            Global.LOOKUPCompany_New(lueToCompany);
            Global.LOOKUPBranch(lueFromBranch);
            Global.LOOKUPBranch_New(lueToBranch);
            Global.LOOKUPLocation(lueFromLocation);
            Global.LOOKUPLocation_New(lueToLocation);
            Global.LOOKUPDepartment(lueFromDepartment);
            Global.LOOKUPDepartment_New(lueToDepartment);
            Global.LOOKUPParty(lueFromParty);
            Global.LOOKUPParty(lueToParty);
            Global.LOOKUPParty(lueParty);

            m_dtbSubProcess = (((DataTable)lueToSubProcess.Properties.DataSource).Copy());

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
            if (GlobalDec.gEmployeeProperty.role_name == "SURAT MAKABLE")
            {
                lueToProcess.EditValue = 4;
                lueToSubProcess.EditValue = 2005;
                lueToManager.EditValue = Val.ToInt64(4018);
                lueToParty.EditValue = Val.ToInt32(132);
            }
            if (GlobalDec.gEmployeeProperty.role_name == "SURAT RUSSIAN")
            {
                lueToProcess.EditValue = 2009;
                lueToSubProcess.EditValue = 2023;
                //lueToManager.EditValue = Val.ToInt64(34515);
                lueToManager.EditValue = Val.ToInt64(55883);
                lueToParty.EditValue = Val.ToInt32(1351);
            }
            lueEmployee.Visible = false;

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
                btnSave.Enabled = false;
                string Str = "";
                var Date = DateTime.Compare(Convert.ToDateTime(dtpReceiveDate.Text), DateTime.Today);
                if (Date < 0 && Val.ToString(lblMode.Text) == "NEW")
                {
                    Str = GlobalDec.CheckLockIsOpenOrNot(Val.DBDate(dtpReceiveDate.Text), Val.DBTime(DateTime.Now.ToShortTimeString())); //Val.GetFullTime12());
                    if (Str != "YES")
                    {
                        if (Str != "")
                        {
                            Global.Message(Str);
                            btnSave.Enabled = true;
                            return;
                        }
                        else
                        {
                            Global.Message("You Are Not Suppose to Make Entry On Different Date");
                            btnSave.Enabled = true;
                            return;
                        }
                    }
                    else
                    {
                        dtpReceiveDate.Enabled = true;
                        dtpReceiveDate.Visible = true;
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

                backgroundWorker_ProcRecWeightLoss.RunWorkerAsync();

                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                btnSave.Enabled = true;
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
        private void lueProcess_EditValueChanged_2(object sender, EventArgs e)
        {
            try
            {
                if (lueToProcess.EditValue != System.DBNull.Value)
                {
                    if (m_dtbSubProcess.Rows.Count > 0)
                    {
                        DataTable dtbdetail = m_dtbSubProcess;

                        string strFilter = string.Empty;

                        if (lueToProcess.Text != "")
                            strFilter = "process_id = " + lueToProcess.EditValue;


                        dtbdetail.DefaultView.RowFilter = strFilter;
                        dtbdetail.DefaultView.ToTable();

                        DataTable dtb = dtbdetail.DefaultView.ToTable();

                        lueToSubProcess.Properties.DataSource = dtb;
                        lueToSubProcess.Properties.ValueMember = "sub_process_id";
                        lueToSubProcess.Properties.DisplayMember = "sub_process_name";
                        lueToSubProcess.EditValue = System.DBNull.Value;
                    }
                }
                if (lueToProcess.EditValue != System.DBNull.Value && lueToSubProcess.EditValue != System.DBNull.Value && Val.ToInt64(txtLotID.Text) != 0)
                {
                    DataTable dtIss = new DataTable();
                    dtIss = objProcessRecieve.GetIssueID(Val.ToInt64(txtLotID.Text), Val.ToInt32(lueToProcess.EditValue), Val.ToInt32(lueToSubProcess.EditValue));
                    if (dtIss.Rows.Count > 0)
                    {
                        m_manager_id = Val.ToInt(dtIss.Rows[0]["manager_id"]);
                        m_emp_id = Val.ToInt(dtIss.Rows[0]["employee_id"]);
                        m_dtOutstanding = objProcessRecieve.Carat_Sarin_OutStanding_GetData(Val.ToInt64(txtLotID.Text), Val.ToInt32(lueToProcess.EditValue), Val.ToInt32(lueToSubProcess.EditValue), 1, "R");
                        if (m_dtOutstanding.Rows.Count > 0)
                        {
                            m_OsCarat = Val.ToInt32(m_dtOutstanding.Rows[0]["carat"]);
                            txtReturnCarat.Text = Val.ToString(m_OsCarat);
                            m_emp = objProcessRecieve.GetEmployee(Val.ToInt(dtIssOS.Rows[0]["lot_id"]), Val.ToInt(dtIssOS.Rows[0]["process_id"]), Val.ToInt(dtIssOS.Rows[0]["sub_process_id"]));
                            if (m_emp != 0)
                            {
                                lueEmployee.EditValue = Val.ToInt64(m_emp);
                            }
                            else
                                lueEmployee.EditValue = null;
                        }
                        else
                        {
                            txtReturnCarat.Text = "0";
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
        private void lueCutNo_EditValueChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (m_IsLot == 0)
                {
                    if (lueCutNo.EditValue != System.DBNull.Value && lueKapan.EditValue != null)
                    {
                        if (m_dtbParam.Rows.Count > 0)
                        {
                            DataRow[] dr = m_dtbParam.Select("rough_cut_no ='" + Val.ToString(lueCutNo.Text) + "'");
                            txtLotID.Text = Val.ToString(dr[0]["lot_id"]);
                            MFGProcessReceive objProcessRecieve = new MFGProcessReceive();

                            if (txtLotID.Text != string.Empty || Val.ToInt64(txtLotID.Text) != 0)
                            {
                                //GetOsCarat(Val.ToInt64(txtLotID.Text));
                                GetOsCarat(Val.ToInt64(txtLotID.Text));

                                dtIssOS = objProcessRecieve.Carat_Sarin_OutStanding_GetData(Val.ToInt64(txtLotID.Text), Val.ToInt(0), Val.ToInt(0), 1, "R");
                                if (dtIssOS.Rows.Count == 1)
                                {
                                    lueToProcess.Text = Val.ToString(dtIssOS.Rows[0]["process_name"]);
                                    lueToSubProcess.Text = Val.ToString(dtIssOS.Rows[0]["sub_process_name"]);
                                    lblOsPcs.Text = Val.ToString(Val.ToInt(dtIssOS.Rows[0]["pcs"]));
                                    lblOsCarat.Text = Val.ToString(Val.ToDecimal(dtIssOS.Rows[0]["carat"]));

                                }
                                if (dtIssOS.Rows.Count > 0)
                                {
                                    lueToProcess.Properties.DataSource = dtIssOS;
                                    lueToProcess.Properties.DisplayMember = "process_name";
                                    lueToProcess.Properties.ValueMember = "process_id";

                                }
                                else
                                {
                                    Global.Message("Lot Not Issue");
                                    lueToProcess.Enabled = true;
                                    lueToSubProcess.Enabled = true;
                                    lueToProcess.EditValue = null;
                                    lueToSubProcess.EditValue = null;
                                    lblOsPcs.Text = Val.ToString("0");
                                    lblOsCarat.Text = Val.ToString("0.00");
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
            if (lueToProcess.EditValue != System.DBNull.Value && lueToSubProcess.EditValue != System.DBNull.Value && Val.ToInt64(txtLotID.Text) != 0)
            {
                DataTable dtIss = new DataTable();
                dtIss = objProcessRecieve.GetIssueID(Val.ToInt64(txtLotID.Text), Val.ToInt32(lueToProcess.EditValue), Val.ToInt32(lueToSubProcess.EditValue));
                if (dtIss.Rows.Count > 0)
                {
                    m_manager_id = Val.ToInt(dtIss.Rows[0]["manager_id"]);
                    m_emp_id = Val.ToInt(dtIss.Rows[0]["employee_id"]);
                    m_issue_id = Val.ToInt(dtIss.Rows[0]["issue_id"]);
                    m_dtOutstanding = objProcessRecieve.Carat_Sarin_OutStanding_GetData(Val.ToInt64(txtLotID.Text), Val.ToInt32(lueToProcess.EditValue), Val.ToInt32(lueToSubProcess.EditValue), 1, "R");
                    if (m_dtOutstanding.Rows.Count > 0)
                    {
                        //txtReturnCarat.Text = Val.ToString(m_dtOutstanding.Rows[0]["carat"]);
                        m_OsCarat = Val.ToDecimal(m_dtOutstanding.Rows[0]["carat"]);
                        m_OsPcs = Val.ToInt(m_dtOutstanding.Rows[0]["pcs"]);
                        lblOsPcs.Text = Val.ToString(m_dtOutstanding.Rows[0]["pcs"]);
                        lblOsCarat.Text = Val.ToString(m_OsCarat);
                        lueToManager.EditValue = Val.ToInt64(m_manager_id);
                        m_emp = objProcessRecieve.GetEmployee(Val.ToInt64(txtLotID.Text), Val.ToInt32(lueToProcess.EditValue), Val.ToInt32(lueToSubProcess.EditValue));
                        if (m_emp != 0)
                        {
                            lueEmployee.EditValue = Val.ToInt64(m_emp);
                        }
                        else
                            lueEmployee.EditValue = null;
                    }
                    else
                    {
                        txtReturnCarat.Text = "0";
                        lblOsCarat.Text = "0";
                    }

                }
                else
                {
                    Global.Message("Lot not issue in this process.");
                }


                if (lueToSubProcess.Text == "MAPPING")
                {
                    lblRetPcs.Visible = true;
                    txtReturnPcs.Visible = true;

                    lblRetCarat.Visible = true;
                    txtReturnCarat.Visible = true;

                    lblOutPcs.Visible = true;
                    lblOutCarat.Visible = true;

                    txtOutpcs.Visible = true;
                    txtOutCarat.Visible = true;

                    lblRejPcs.Visible = false;
                    txtRejPcs.Visible = false;

                    lblRejCarat.Visible = false;
                    txtRejCarat.Visible = false;

                    lblResoingPcs.Visible = false;
                    lblResoingCarat.Visible = false;

                    txtReSoingPcs.Visible = false;
                    txtResoingCarat.Visible = false;

                    lblLostCarat.Visible = false;
                    lblLostPcs.Visible = false;

                    lblBreakragePcs.Visible = false;
                    lblBreakrageCarat.Visible = false;
                    txtBreakPcs.Visible = false;
                    txtBreakCarat.Visible = false;

                    txtLostPcs.Visible = false;
                    txtLostCarat.Visible = false;

                    txtLossCarat.Visible = false;
                    txtLossPcs.Visible = false;
                    lblLossPcs.Visible = false;
                    lblLossCarat.Visible = false;
                }
                else if (lueToSubProcess.Text == "STICTHING")
                {
                    lblRetPcs.Visible = true;
                    txtReturnPcs.Visible = true;

                    lblRetCarat.Visible = true;
                    txtReturnCarat.Visible = true;

                    lblOutPcs.Visible = false;
                    lblOutCarat.Visible = false;

                    txtOutpcs.Visible = false;
                    txtOutCarat.Visible = false;

                    lblRejPcs.Visible = false;
                    txtRejPcs.Visible = false;

                    lblRejCarat.Visible = false;
                    txtRejCarat.Visible = false;

                    lblResoingPcs.Visible = false;
                    lblResoingCarat.Visible = false;

                    txtReSoingPcs.Visible = false;
                    txtResoingCarat.Visible = false;

                    lblLostCarat.Visible = true;
                    lblLostPcs.Visible = true;
                    txtLostPcs.Visible = true;
                    txtLostCarat.Visible = true;

                    lblBreakragePcs.Visible = false;
                    lblBreakrageCarat.Visible = false;
                    txtBreakPcs.Visible = false;
                    txtBreakCarat.Visible = false;

                    txtLossCarat.Visible = false;
                    txtLossPcs.Visible = false;
                    lblLossPcs.Visible = false;
                    lblLossCarat.Visible = false;
                }
                else if (lueToSubProcess.Text == "ROBOT OPERATOR")
                {
                    lblRetPcs.Visible = true;
                    txtReturnPcs.Visible = true;

                    lblRetCarat.Visible = true;
                    txtReturnCarat.Visible = true;

                    lblOutPcs.Visible = false;
                    lblOutCarat.Visible = false;

                    txtOutpcs.Visible = false;
                    txtOutCarat.Visible = false;

                    lblRejPcs.Visible = true;
                    txtRejPcs.Visible = true;

                    lblRejCarat.Visible = true;
                    txtRejCarat.Visible = true;

                    lblResoingPcs.Visible = true;
                    lblResoingCarat.Visible = true;

                    txtReSoingPcs.Visible = true;
                    txtResoingCarat.Visible = true;

                    lblLostCarat.Visible = false;
                    lblLostPcs.Visible = false;
                    txtLostPcs.Visible = false;
                    txtLostCarat.Visible = false;

                    lblBreakragePcs.Visible = true;
                    lblBreakrageCarat.Visible = true;
                    txtBreakPcs.Visible = true;
                    txtBreakCarat.Visible = true;

                    txtLossCarat.Visible = false;
                    txtLossPcs.Visible = false;
                    lblLossPcs.Visible = false;
                    lblLossCarat.Visible = false;
                }
                else if (lueToSubProcess.Text == "MANUAL OPERATOR")
                {
                    lblRetPcs.Visible = true;
                    txtReturnPcs.Visible = true;

                    lblRetCarat.Visible = true;
                    txtReturnCarat.Visible = true;

                    lblOutPcs.Visible = true;
                    lblOutCarat.Visible = true;

                    txtOutpcs.Visible = true;
                    txtOutCarat.Visible = true;

                    lblRejPcs.Visible = false;
                    txtRejPcs.Visible = false;

                    lblRejCarat.Visible = false;
                    txtRejCarat.Visible = false;

                    lblResoingPcs.Visible = false;
                    lblResoingCarat.Visible = false;

                    txtReSoingPcs.Visible = false;
                    txtResoingCarat.Visible = false;

                    lblLostCarat.Visible = false;
                    lblLostPcs.Visible = false;
                    txtLostPcs.Visible = false;
                    txtLostCarat.Visible = false;

                    lblBreakragePcs.Visible = false;
                    lblBreakrageCarat.Visible = false;
                    txtBreakPcs.Visible = false;
                    txtBreakCarat.Visible = false;

                    txtLossCarat.Visible = false;
                    txtLossPcs.Visible = false;
                    lblLossPcs.Visible = false;
                    lblLossCarat.Visible = false;
                }
                if (lueToSubProcess.Text == "4P-OK")
                {
                    lblRetPcs.Visible = true;
                    txtReturnPcs.Visible = true;

                    lblRetCarat.Visible = true;
                    txtReturnCarat.Visible = true;

                    lblOutPcs.Visible = false;
                    lblOutCarat.Visible = false;

                    txtOutpcs.Visible = false;
                    txtOutCarat.Visible = false;

                    lblRejPcs.Visible = false;
                    txtRejPcs.Visible = false;

                    lblRejCarat.Visible = false;
                    txtRejCarat.Visible = false;

                    lblResoingPcs.Visible = false;
                    lblResoingCarat.Visible = false;

                    txtReSoingPcs.Visible = false;
                    txtResoingCarat.Visible = false;

                    lblLostCarat.Visible = false;
                    lblLostPcs.Visible = false;

                    lblBreakragePcs.Visible = false;
                    lblBreakrageCarat.Visible = false;
                    txtBreakPcs.Visible = false;
                    txtBreakCarat.Visible = false;

                    txtLostPcs.Visible = false;
                    txtLostCarat.Visible = false;

                    txtLossCarat.Visible = false;
                    txtLossPcs.Visible = false;
                    lblLossPcs.Visible = false;
                    lblLossCarat.Visible = false;
                }
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
            }
            else
            {
                Global.Confirm("Error In Demand Noting Data");
                lueCutNo.Focus();
            }
        }
        private void txtReturnPcs_Validated(object sender, EventArgs e)
        {

            txtTotalPcs.Text = Val.ToString(Val.ToInt(txtReturnPcs.Text) + Val.ToInt(txtOutpcs.Text) + Val.ToInt(txtReSoingPcs.Text) + Val.ToInt(txtRejPcs.Text) + Val.ToInt(txtBreakPcs.Text) + Val.ToInt(txtLossPcs.Text) + Val.ToInt(txtLostPcs.Text));
        }
        private void txtOutpcs_Validated(object sender, EventArgs e)
        {
            txtTotalPcs.Text = Val.ToString(Val.ToInt(txtReturnPcs.Text) + Val.ToInt(txtOutpcs.Text) + Val.ToInt(txtReSoingPcs.Text) + Val.ToInt(txtRejPcs.Text) + Val.ToInt(txtBreakPcs.Text) + Val.ToInt(txtLossPcs.Text) + Val.ToInt(txtLostPcs.Text));
        }
        private void txtRejPcs_Validated(object sender, EventArgs e)
        {
            txtTotalPcs.Text = Val.ToString(Val.ToInt(txtReturnPcs.Text) + Val.ToInt(txtOutpcs.Text) + Val.ToInt(txtReSoingPcs.Text) + Val.ToInt(txtRejPcs.Text) + Val.ToInt(txtBreakPcs.Text) + Val.ToInt(txtLossPcs.Text) + Val.ToInt(txtLostPcs.Text));
        }
        private void txtLossPcs_Validated(object sender, EventArgs e)
        {
            txtTotalPcs.Text = Val.ToString(Val.ToInt(txtReturnPcs.Text) + Val.ToInt(txtOutpcs.Text) + Val.ToInt(txtReSoingPcs.Text) + Val.ToInt(txtRejPcs.Text) + Val.ToInt(txtBreakPcs.Text) + Val.ToInt(txtLossPcs.Text) + Val.ToInt(txtLostPcs.Text));
        }
        private void txtLostPcs_Validated(object sender, EventArgs e)
        {
            txtTotalPcs.Text = Val.ToString(Val.ToInt(txtReturnPcs.Text) + Val.ToInt(txtOutpcs.Text) + Val.ToInt(txtReSoingPcs.Text) + Val.ToInt(txtRejPcs.Text) + Val.ToInt(txtBreakPcs.Text) + Val.ToInt(txtLossPcs.Text) + Val.ToInt(txtLostPcs.Text));
        }
        private void txtReturnPcs_EditValueChanged(object sender, EventArgs e)
        {
            //if (Val.ToInt(txtReturnPcs.Text) < Val.ToInt(lblOsPcs.Text))
            //{
            txtBreakPcs.Text = Val.ToString(Val.ToInt(lblOsPcs.Text) - Val.ToInt(txtReturnPcs.Text));
            txtTotalPcs.Text = Val.ToString(Val.ToInt(txtReturnPcs.Text) + Val.ToInt(txtOutpcs.Text) + Val.ToInt(txtReSoingPcs.Text) + Val.ToInt(txtRejPcs.Text) + Val.ToInt(txtBreakPcs.Text) + Val.ToInt(txtLossPcs.Text) + Val.ToInt(txtLostPcs.Text));

            //lblWagesAmt.Text = "0";
            //}
            //else
            //{
            //    Global.Message("Return Pcs more then Outstanding Pcs");
            //    txtReturnPcs.Text = "0";
            //    return;
            //}
        }
        private void txtReSoingPcs_Validated(object sender, EventArgs e)
        {
            txtTotalPcs.Text = Val.ToString(Val.ToInt(txtReturnPcs.Text) + Val.ToInt(txtOutpcs.Text) + Val.ToInt(txtReSoingPcs.Text) + Val.ToInt(txtRejPcs.Text) + Val.ToInt(txtBreakPcs.Text) + Val.ToInt(txtLossPcs.Text) + Val.ToInt(txtLostPcs.Text));
        }
        private void txtBreakPcs_Validated(object sender, EventArgs e)
        {
            txtTotalPcs.Text = Val.ToString(Val.ToInt(txtReturnPcs.Text) + Val.ToInt(txtOutpcs.Text) + Val.ToInt(txtReSoingPcs.Text) + Val.ToInt(txtRejPcs.Text) + Val.ToInt(txtBreakPcs.Text) + Val.ToInt(txtLossPcs.Text) + Val.ToInt(txtLostPcs.Text));
        }
        private void txtOutCarat_Validated(object sender, EventArgs e)
        {
            txtTotalCarat.Text = Val.ToString(Val.ToDecimal(txtReturnCarat.Text) + Val.ToDecimal(txtOutCarat.Text) + Val.ToDecimal(txtResoingCarat.Text) + Val.ToDecimal(txtRejCarat.Text) + Val.ToDecimal(txtBreakCarat.Text) + Val.ToDecimal(txtLossCarat.Text) + Val.ToDecimal(txtLostCarat.Text));
        }

        private void txtRejCarat_Validated(object sender, EventArgs e)
        {
            txtTotalCarat.Text = Val.ToString(Val.ToDecimal(txtReturnCarat.Text) + Val.ToDecimal(txtOutCarat.Text) + Val.ToDecimal(txtResoingCarat.Text) + Val.ToDecimal(txtRejCarat.Text) + Val.ToDecimal(txtBreakCarat.Text) + Val.ToDecimal(txtLossCarat.Text) + Val.ToDecimal(txtLostCarat.Text));
        }

        private void txtResoingCarat_Validated(object sender, EventArgs e)
        {
            txtTotalCarat.Text = Val.ToString(Val.ToDecimal(txtReturnCarat.Text) + Val.ToDecimal(txtOutCarat.Text) + Val.ToDecimal(txtResoingCarat.Text) + Val.ToDecimal(txtRejCarat.Text) + Val.ToDecimal(txtBreakCarat.Text) + Val.ToDecimal(txtLossCarat.Text) + Val.ToDecimal(txtLostCarat.Text));
        }

        private void txtBreakCarat_Validated(object sender, EventArgs e)
        {
            txtTotalCarat.Text = Val.ToString(Val.ToDecimal(txtReturnCarat.Text) + Val.ToDecimal(txtOutCarat.Text) + Val.ToDecimal(txtResoingCarat.Text) + Val.ToDecimal(txtRejCarat.Text) + Val.ToDecimal(txtBreakCarat.Text) + Val.ToDecimal(txtLossCarat.Text) + Val.ToDecimal(txtLostCarat.Text));
        }

        private void txtLostCarat_Validated(object sender, EventArgs e)
        {
            txtTotalCarat.Text = Val.ToString(Val.ToDecimal(txtReturnCarat.Text) + Val.ToDecimal(txtOutCarat.Text) + Val.ToDecimal(txtResoingCarat.Text) + Val.ToDecimal(txtRejCarat.Text) + Val.ToDecimal(txtBreakCarat.Text) + Val.ToDecimal(txtLossCarat.Text) + Val.ToDecimal(txtLostCarat.Text));
        }

        private void txtLossCarat_Validated(object sender, EventArgs e)
        {
            txtTotalCarat.Text = Val.ToString(Val.ToDecimal(txtReturnCarat.Text) + Val.ToDecimal(txtOutCarat.Text) + Val.ToDecimal(txtResoingCarat.Text) + Val.ToDecimal(txtRejCarat.Text) + Val.ToDecimal(txtBreakCarat.Text) + Val.ToDecimal(txtLossCarat.Text) + Val.ToDecimal(txtLostCarat.Text));
        }
        private void txtLotID_Validated(object sender, EventArgs e)
        {
            m_IsLot = 1;
            lueKapan.EditValue = null;
            lueCutNo.EditValue = null;

            MFGProcessReceive objProcessRecieve = new MFGProcessReceive();
            DataTable DTLotDetail = new DataTable();
            if (Val.ToInt64(txtLotID.Text) != 0 && Val.ToInt64(lueKapan.EditValue) == 0 && Val.ToInt64(lueCutNo.EditValue) == 0)
            {
                //m_dtbParam = Global.GetRoughKapanWise(Val.ToInt(0), Val.ToInt64(txtLotID.Text));
                //if (m_dtbParam.Rows.Count > 0)
                //{
                //    lueKapan.EditValue = Val.ToInt64(m_dtbParam.Rows[0]["kapan_id"]);
                //    lueCutNo.EditValue = Val.ToInt64(m_dtbParam.Rows[0]["rough_cut_id"]);

                DataTable DTab_OS = objJangedReturn.GetJangedOutstanding(Val.ToInt64(txtLotID.Text));

                if (DTab_OS.Rows.Count > 0)
                {
                    DTLotDetail = objJangedReturn.JangedData(Val.ToInt(DTab_OS.Rows[0]["janged_no"]), Val.ToInt64(txtLotID.Text));
                }
                else
                {
                    Global.Message("Lot Not Issue in Janged.");
                    m_IsLot = 0;
                    return;
                }

                if (DTLotDetail.Rows.Count > 0)
                {
                    txtJangedNo.Text = Val.ToString(DTab_OS.Rows[0]["janged_no"]);
                    lueKapan.EditValue = Val.ToInt64(DTLotDetail.Rows[0]["kapan_id"]);
                    lueCutNo.EditValue = Val.ToInt64(DTLotDetail.Rows[0]["rough_cut_id"]);
                    lueFromProcess.EditValue = Val.ToInt(DTLotDetail.Rows[0]["process_id"]);
                    lueFromSubProcess.EditValue = Val.ToInt(DTLotDetail.Rows[0]["sub_process_id"]);
                    lueFromCompany.EditValue = Val.ToInt(DTLotDetail.Rows[0]["company_id"]);
                    lueFromBranch.EditValue = Val.ToInt(DTLotDetail.Rows[0]["branch_id"]);
                    lueFromLocation.EditValue = Val.ToInt(DTLotDetail.Rows[0]["location_id"]);
                    lueFromDepartment.EditValue = Val.ToInt(DTLotDetail.Rows[0]["to_department_id"]);
                    lueFromManager.EditValue = Val.ToInt64(DTLotDetail.Rows[0]["manager_id"]);
                    lueFromParty.EditValue = Val.ToInt(DTLotDetail.Rows[0]["party_id"]);
                    Prediction_Id = Val.ToInt64(DTLotDetail.Rows[0]["prediction_id"]);
                    //txtReturnPcs.Text = Val.ToString(DTLotDetail.Rows[0]["issue_pcs"]);
                    //txtReturnCarat.Text = Val.ToString(DTLotDetail.Rows[0]["issue_carat"]);
                    txtReturnPcs.Text = Val.ToString(0);
                    txtReturnCarat.Text = Val.ToString(0);
                    txtLossPcs.Text = Val.ToString(0);
                    txtLossCarat.Text = Val.ToString(0);
                    txtLostPcs.Text = Val.ToString(0);
                    txtLostCarat.Text = Val.ToString(0);
                    txtRejPcs.Text = Val.ToString(0);
                    txtRejCarat.Text = Val.ToString(0);
                    txtReSoingPcs.Text = Val.ToString(0);
                    txtResoingCarat.Text = Val.ToString(0);
                    txtOutpcs.Text = Val.ToString(0);
                    txtOutCarat.Text = Val.ToString(0);
                    txtBreakPcs.Text = Val.ToString(0);
                    txtBreakCarat.Text = Val.ToString(0);
                    txtRate.Text = Val.ToString(DTLotDetail.Rows[0]["rate"]);
                    lblOsCarat.Text = Val.ToString(DTLotDetail.Rows[0]["issue_carat"]);
                    lblOsPcs.Text = Val.ToString(DTLotDetail.Rows[0]["issue_pcs"]);
                }

                else
                {
                    lueToProcess.Enabled = true;
                    lueToSubProcess.Enabled = true;
                    lueToProcess.EditValue = null;
                    lueToSubProcess.EditValue = null;
                    Prediction_Id = 0;
                    txtReturnPcs.Text = string.Empty;
                    txtReturnCarat.Text = string.Empty;
                    txtLossPcs.Text = string.Empty;
                    txtLossCarat.Text = string.Empty;
                    txtLostPcs.Text = string.Empty;
                    txtLostCarat.Text = string.Empty;
                    txtRejPcs.Text = string.Empty;
                    txtRejCarat.Text = string.Empty;
                    lblOsCarat.Text = "0";
                    lblOsPcs.Text = "0";
                    m_OsCarat = 0;
                    m_OsPcs = 0;
                    lblOsCarat.Text = Val.ToString("0.00");
                }
            }
            m_IsLot = 0;
        }
        private void lueProcess_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmProcessMaster frmProcess = new FrmProcessMaster();
                frmProcess.ShowDialog();
                Global.LOOKUPProcess(lueToProcess);
            }
        }
        private void lueSubProcess_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmMfgSubProcessMaster frmSubProcess = new FrmMfgSubProcessMaster();
                frmSubProcess.ShowDialog();
                Global.LOOKUPSubProcess(lueToSubProcess);
                m_dtbSubProcess = (((DataTable)lueToSubProcess.Properties.DataSource).Copy());
            }
        }
        private void lueManager_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmEmployeeMaster frmManager = new FrmEmployeeMaster();
                frmManager.ShowDialog();
                Global.LOOKUPManager(lueToManager);
            }
        }
        private void lueEmployee_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmEmployeeMaster frmEmployee = new FrmEmployeeMaster();
                frmEmployee.ShowDialog();
                Global.LOOKUPEmp(lueEmployee);
            }
        }
        private void backgroundWorker_JangedManual_Return_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                MFGJangedReturnManual MFGJangedReturn = new MFGJangedReturnManual();
                MFGJangedReturn_Property objMFGJangedReturnProperty = new MFGJangedReturn_Property();

                Conn = new BeginTranConnection(true, false);

                DataTable m_DTab = new DataTable();

                Int64 Lot_SrNo = 0;
                try
                {
                    objMFGJangedReturnProperty.janged_no = Val.ToInt64(txtJangedNo.Text);
                    objMFGJangedReturnProperty.pcs = Val.ToInt(txtReturnPcs.Text);
                    objMFGJangedReturnProperty.carat = Val.ToDecimal(txtReturnCarat.Text);
                    objMFGJangedReturnProperty.rejection_pcs = Val.ToInt(txtRejPcs.Text);
                    objMFGJangedReturnProperty.rejection_carat = Val.ToDecimal(txtRejCarat.Text);

                    objMFGJangedReturnProperty.rr_pcs = Val.ToInt(txtOutpcs.Text);
                    objMFGJangedReturnProperty.rr_carat = Val.ToDecimal(txtOutCarat.Text);
                    objMFGJangedReturnProperty.resoing_pcs = Val.ToInt(txtReSoingPcs.Text);
                    objMFGJangedReturnProperty.resoing_carat = Val.ToDecimal(txtResoingCarat.Text);
                    objMFGJangedReturnProperty.breakage_pcs = Val.ToInt(txtBreakPcs.Text);
                    objMFGJangedReturnProperty.breakage_carat = Val.ToDecimal(txtBreakCarat.Text);
                    objMFGJangedReturnProperty.loss_pcs = Val.ToInt(txtLossPcs.Text);
                    objMFGJangedReturnProperty.loss_carat = Val.ToDecimal(txtLossCarat.Text);
                    objMFGJangedReturnProperty.lost_pcs = Val.ToInt(txtLostPcs.Text);
                    objMFGJangedReturnProperty.lost_carat = Val.ToDecimal(txtLostCarat.Text);
                    objMFGJangedReturnProperty.rate = Val.ToDecimal(txtRate.Text);
                    objMFGJangedReturnProperty.amount = Val.ToDecimal(txtAmount.Text);
                    objMFGJangedReturnProperty.from_company_id = Val.ToInt(lueFromCompany.EditValue);
                    objMFGJangedReturnProperty.from_branch_id = Val.ToInt(lueFromBranch.EditValue);
                    objMFGJangedReturnProperty.from_location_id = Val.ToInt(lueFromLocation.EditValue);
                    objMFGJangedReturnProperty.from_department_id = Val.ToInt(lueFromDepartment.EditValue);
                    objMFGJangedReturnProperty.lot_id = Val.ToInt64(txtLotID.Text);
                    objMFGJangedReturnProperty.janged_date = Val.DBDate(dtpReceiveDate.Text);
                    objMFGJangedReturnProperty.form_id = Val.ToInt(m_numForm_id);
                    objMFGJangedReturnProperty.rough_cut_id = Val.ToInt64(lueCutNo.EditValue);
                    objMFGJangedReturnProperty.kapan_id = Val.ToInt64(lueKapan.EditValue);
                    objMFGJangedReturnProperty.prediction_id = Val.ToInt64(Prediction_Id);
                    objMFGJangedReturnProperty.party_id = Val.ToInt64(lueToParty.EditValue);

                    objMFGJangedReturnProperty.from_manager_id = Val.ToInt(lueFromManager.EditValue);
                    objMFGJangedReturnProperty.to_manager_id = Val.ToInt(lueToManager.EditValue);
                    objMFGJangedReturnProperty.employee_id = Val.ToInt(lueEmployee.EditValue);
                    objMFGJangedReturnProperty.from_process_id = Val.ToInt(lueFromProcess.EditValue);
                    objMFGJangedReturnProperty.to_process_id = Val.ToInt(lueToProcess.EditValue);
                    objMFGJangedReturnProperty.sub_process_id = Val.ToInt(lueFromSubProcess.EditValue);
                    objMFGJangedReturnProperty.to_sub_process_id = Val.ToInt(lueToSubProcess.EditValue);
                    objMFGJangedReturnProperty.lot_srno = Lot_SrNo;
                    if (Val.ToString(lblMode.Text) == "NEW")
                    {
                        IntRes = MFGJangedReturn.Save(objMFGJangedReturnProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                    }
                    if (Val.ToString(lblMode.Text) == "EDIT")
                    {
                        IntRes = MFGJangedReturn.Update(objMFGJangedReturnProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                    }

                    Conn.Inter1.Commit();
                }
                catch (Exception ex)
                {
                    IntRes = -1;
                    Conn.Inter1.Rollback();
                    Conn = null;
                    General.ShowErrors(ex.ToString());
                    btnSave.Enabled = true;
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
                    btnSave.Enabled = true;
                }
            }
        }
        private void backgroundWorker_JangedManual_Return_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (IntRes > 0)
                {
                    if (Val.ToString(lblMode.Text) == "NEW")
                    {
                        Global.Confirm("Process Recieve Data Save Succesfully");
                    }
                    if (Val.ToString(lblMode.Text) == "EDIT")
                    {
                        Global.Confirm("Process Recieve Data Update Succesfully");
                    }
                    ClearDetails();
                    lblMode.Text = "EDIT";
                    if (Val.ToString(lblMode.Text) == "EDIT" && chkIsKapan.Checked == true)
                    {
                        dgvRecieveLots.Columns["lot_id"].ClearFilter();
                        dgvRecieveLots.FocusedRowHandle = GridControl.AutoFilterRowHandle;
                        dgvRecieveLots.FocusedColumn = dgvRecieveLots.Columns["lot_id"];
                        dgvRecieveLots.ShowEditor();
                        //dgvRecieveLots.FocusedColumn = dgvRecieveLots.Columns["lot_id"];
                    }
                    lblMode.Text = "NEW";
                    btnSave.Enabled = true;
                }
                else
                {
                    Global.Confirm("Error In Process Recieve");
                    btnSave.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }
        private void txtReturnCarat_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (sender.Equals(txtReturnCarat))
                {
                    NumberCheckwithpoint(e, (Control)sender, 3);
                }
                if (Strings.Asc(e.KeyChar) == 13)
                {
                    if (sender.Equals(txtLotID))
                    {
                        //txtReturnCarat.Focus();
                    }
                    else if (sender.Equals(txtReturnCarat))
                    {
                    }
                    else
                    {
                        //SendKeys.Send("{TAB}");
                    }
                }

                if (Val.ToDecimal(lblOsCarat.Text) > 0)
                {
                    //lblPer.Text = Val.ToString((Val.ToDecimal(txtReturnCarat.Text) / Val.ToDecimal(lblOsCarat.Text)) * 100);
                }
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                Exception ex2 = ex;
                Interaction.MsgBox(ex2.Message);
                ProjectData.ClearProjectError();
            }
        }
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (comOpen)
            {
                try
                {
                    //byteEnd = serialPort2.NewLine.ToCharArray();
                    //Bytenumber = serialPort2.BytesToRead;
                    //readBuffer = serialPort2.ReadLine();
                    //Invoke(new EventHandler(DoUpdate));
                }
                catch (Exception ex)
                {
                    Global.Message(ex.ToString());
                }
            }
        }
        private void btnOpenPort_Click(object sender, EventArgs e)
        {
            btnClosePort_Click(RuntimeHelpers.GetObjectValue(sender), e);
            try
            {
                //serialPort2.PortName = "COM4";
                //serialPort2.BaudRate = 9600;
                //serialPort2.Parity = Parity.None;
                //serialPort2.DataBits = 8;
                //serialPort2.StopBits = StopBits.Two;
                //serialPort2.Handshake = Handshake.None;
                //serialPort2.RtsEnable = false;
                //serialPort2.ReceivedBytesThreshold = 1;
                //serialPort2.NewLine = Environment.NewLine;
                //serialPort2.ReadTimeout = 10000;
                try
                {
                    //serialPort2.Open();
                    //comOpen = serialPort2.IsOpen;
                }
                catch (Exception ex)
                {
                    comOpen = false;
                    Global.Message(ex.ToString());
                }

                if (comOpen)
                {
                    //serialPort2.WriteLine(txtReturnCarat.Text);
                    EnDis(O: false, C: true);
                    //txtReturnCarat.Focus();
                }
            }
            catch (Exception ex3)
            {
                Global.Message(ex3.ToString());
            }
        }
        private void btnClosePort_Click(object sender, EventArgs e)
        {
            try
            {
                if (comOpen)
                {
                    //serialPort2.DiscardInBuffer();
                    //serialPort2.Close();
                }
                comOpen = false;
                //TxtPort.BackColor = Color.Red;
                EnDis(O: true, C: false);
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
            }
        }
        private void txtRate_EditValueChanged(object sender, EventArgs e)
        {
            if (txtRate.Text != "")
            {
                txtAmount.Text = Val.ToString(Math.Round(Val.ToDecimal(txtRate.Text) * Val.ToDecimal(txtReturnCarat.Text), 3));
            }
            else
            {
                txtAmount.Text = "0";
            }
        }
        private void lueToProcess_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (lueToProcess.EditValue != System.DBNull.Value)
                {
                    if (m_dtbSubProcess.Rows.Count > 0)
                    {
                        DataTable dtbdetail = m_dtbSubProcess;

                        string strFilter = string.Empty;

                        if (lueToProcess.Text != "")
                            strFilter = "process_id = " + lueToProcess.EditValue;


                        dtbdetail.DefaultView.RowFilter = strFilter;
                        dtbdetail.DefaultView.ToTable();

                        DataTable dtb = dtbdetail.DefaultView.ToTable();

                        lueToSubProcess.Properties.DataSource = dtb;
                        lueToSubProcess.Properties.ValueMember = "sub_process_id";
                        lueToSubProcess.Properties.DisplayMember = "sub_process_name";
                        lueToSubProcess.EditValue = System.DBNull.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void txtReturnCarat_EditValueChanged(object sender, EventArgs e)
        {
            txtLossCarat.Text = Val.ToString(Val.ToDecimal(lblOsCarat.Text) - Val.ToDecimal(txtReturnCarat.Text));
            txtTotalCarat.Text = Val.ToString(Val.ToDecimal(txtReturnCarat.Text) + Val.ToDecimal(txtOutCarat.Text) + Val.ToDecimal(txtResoingCarat.Text) + Val.ToDecimal(txtRejCarat.Text) + Val.ToDecimal(txtBreakCarat.Text) + Val.ToDecimal(txtLossCarat.Text) + Val.ToDecimal(txtLostCarat.Text));
            txtAmount.Text = Val.ToString(Math.Round(Val.ToDecimal(txtReturnCarat.Text) * Val.ToDecimal(txtRate.Text), 3));
            if (Val.ToDecimal(lblOsCarat.Text) > 0)
            {
                txtPercentage.Text = Val.ToString(Math.Round((Val.ToDecimal(txtReturnCarat.Text) / Val.ToDecimal(lblOsCarat.Text)) * 100, 3));
            }
        }
        private void chkIsKapan_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsKapan.Checked == true)
            {
                lueKapan.Enabled = true;
                lueCutNo.Enabled = true;
                lueKapan.ReadOnly = false;
                lueCutNo.ReadOnly = false;
                pnlUpdateLots.Visible = true;
                txtLotID.Enabled = false;
            }
            else
            {
                lueKapan.Enabled = false;
                lueCutNo.Enabled = false;
                lueKapan.ReadOnly = true;
                lueCutNo.ReadOnly = true;
                pnlUpdateLots.Visible = false;
                lueKapan.EditValue = null;
                lueCutNo.EditValue = null;
                txtLotID.Enabled = true;
                grdRecieveLots.DataSource = null;

            }
        }
        private void lueCutNo_EditValueChanged(object sender, EventArgs e)
        {
            if (Val.ToInt(lueKapan.EditValue) != 0 && Val.ToInt(lueCutNo.EditValue) != 0 & chkIsKapan.Checked == true)
            {
                dtGrid = objJangedReturn.JangedRecieveData(Val.ToInt32(lueKapan.EditValue), Val.ToInt32(lueCutNo.EditValue));
                grdRecieveLots.DataSource = dtGrid;
            }
        }
        private void lueKapan_EditValueChanged_1(object sender, EventArgs e)
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

        #region GridEvents
        private void dgvRecieveLots_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                if (dtGrid.Rows.Count > 0)
                {

                    DataTable dtLot = new DataTable();
                    dtLot = dtGrid.Select("lot_id =" + Val.ToInt(dgvRecieveLots.GetDataRow(e.RowHandle)["lot_id"].ToString())).CopyToDataTable();
                    dtpReceiveDate.EditValue = Val.DBDate(Val.ToString(dtLot.Rows[0]["janged_date"]));
                    txtLotID.Text = Val.ToString(dtLot.Rows[0]["lot_id"]);
                    txtJangedNo.Text = Val.ToString(dtLot.Rows[0]["janged_no"]);
                    lueKapan.EditValue = Val.ToInt64(dtLot.Rows[0]["kapan_id"]);
                    lueCutNo.EditValue = Val.ToInt64(dtLot.Rows[0]["rough_cut_id"]);
                    lueFromProcess.EditValue = Val.ToInt(dtLot.Rows[0]["from_process"]);
                    lueFromSubProcess.EditValue = Val.ToInt(dtLot.Rows[0]["from_sub_process"]);
                    lueFromCompany.EditValue = Val.ToInt(dtLot.Rows[0]["from_company_id"]);
                    lueFromBranch.EditValue = Val.ToInt(dtLot.Rows[0]["from_branch_id"]);
                    lueFromLocation.EditValue = Val.ToInt(dtLot.Rows[0]["from_location_id"]);
                    lueFromDepartment.EditValue = Val.ToInt(dtLot.Rows[0]["from_department_id"]);
                    lueToCompany.EditValue = Val.ToInt(dtLot.Rows[0]["company_id"]);
                    lueToBranch.EditValue = Val.ToInt(dtLot.Rows[0]["branch_id"]);
                    lueToLocation.EditValue = Val.ToInt(dtLot.Rows[0]["location_id"]);
                    lueToDepartment.EditValue = Val.ToInt(dtLot.Rows[0]["department_id"]);
                    lueFromManager.EditValue = Val.ToInt64(dtLot.Rows[0]["from_manager"]);
                    lueFromParty.EditValue = Val.ToInt(dtLot.Rows[0]["from_party"]);
                    txtReturnPcs.Text = Val.ToString(Val.ToInt(dtLot.Rows[0]["pcs"]));
                    txtReturnCarat.Text = Val.ToString(Val.ToDecimal(dtLot.Rows[0]["Carat"]));
                    txtLossPcs.Text = Val.ToString(0);
                    txtLossCarat.Text = Val.ToString(Val.ToDecimal(dtLot.Rows[0]["loss_carat"]));
                    txtLostPcs.Text = Val.ToString(0);
                    txtLostCarat.Text = Val.ToString(0);
                    txtRejPcs.Text = Val.ToString(0);
                    txtRejCarat.Text = Val.ToString(0);
                    txtReSoingPcs.Text = Val.ToString(0);
                    txtResoingCarat.Text = Val.ToString(0);
                    txtOutpcs.Text = Val.ToString(0);
                    txtOutCarat.Text = Val.ToString(0);
                    txtBreakPcs.Text = Val.ToString(dtLot.Rows[0]["breakage_pcs"]);
                    txtBreakCarat.Text = Val.ToString(0);
                    txtRate.Text = Val.ToString(dtLot.Rows[0]["rate"]);
                    lblOsCarat.Text = Val.ToString(dtLot.Rows[0]["issue_carat"]);
                    lblOsPcs.Text = Val.ToString(dtLot.Rows[0]["issue_pcs"]);
                    txtPercentage.Text = Val.ToString(Math.Round((Val.ToDecimal(txtReturnCarat.Text) / Val.ToDecimal(lblOsCarat.Text)) * 100, 3));
                    lblMode.Text = "EDIT";
                }
                else
                {
                    lueToProcess.Enabled = true;
                    lueToSubProcess.Enabled = true;
                    lueToProcess.EditValue = null;
                    lueToSubProcess.EditValue = null;
                    txtReturnPcs.Text = string.Empty;
                    txtReturnCarat.Text = string.Empty;
                    txtLossPcs.Text = string.Empty;
                    txtLossCarat.Text = string.Empty;
                    txtLostPcs.Text = string.Empty;
                    txtLostCarat.Text = string.Empty;
                    txtRejPcs.Text = string.Empty;
                    txtRejCarat.Text = string.Empty;
                    lblOsCarat.Text = "0";
                    lblOsPcs.Text = "0";
                    m_OsCarat = 0;
                    m_OsPcs = 0;
                    lblOsCarat.Text = Val.ToString("0.00");
                    lblMode.Text = "NEW";
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
                DateTime endDate = Convert.ToDateTime(DateTime.Today);
                endDate = endDate.AddDays(3);

                if (Convert.ToDateTime(dtpReceiveDate.Text) >= endDate)
                {
                    lstError.Add(new ListError(5, " Return Date Not Be Permission After 3 Days in this Lot ID...Please Contact to Administrator"));
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
                if (Val.ToString(txtLotID.Text) == "")
                {
                    lstError.Add(new ListError(12, "Lot No"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtLotID.Focus();
                    }
                }
                if (Val.ToInt64(txtLotID.Text) == 0)
                {
                    lstError.Add(new ListError(12, "Lot No"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtLotID.Focus();
                    }
                }
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

                if (Val.ToString(GlobalDec.gEmployeeProperty.role_name) == "SURAT RUSSIAN")
                {
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

                if ((Val.ToString(GlobalDec.gEmployeeProperty.department_name) == "MAKABLE" || Val.ToString(GlobalDec.gEmployeeProperty.department_name) == "SAWABLE") && Val.ToString(lblMode.Text) == "NEW")
                {
                    if (Val.ToString(lueFromParty.Text) != Val.ToString(lueParty.Text))
                    {
                        lstError.Add(new ListError(5, "Different Party Please Check!!!"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueCutNo.Focus();
                        }
                    }
                }

                if ((txtLossCarat.Text == string.Empty || txtLossCarat.Text == "0") && (txtReturnCarat.Text == string.Empty || txtReturnCarat.Text == "0"))// && (txtLostCarat.Text == string.Empty || txtLostCarat.Text == "0") && (txtRejCarat.Text == string.Empty || txtRejCarat.Text == "0") && (txtOutCarat.Text == string.Empty || txtOutCarat.Text == "0") && (txtResoingCarat.Text == string.Empty || txtResoingCarat.Text == "0") && (txtBreakCarat.Text == string.Empty || txtBreakCarat.Text == "0"))
                {
                    lstError.Add(new ListError(5, "Return Carat can not be blank!!!"));
                    lstError.Add(new ListError(5, "Loss Carat can not be blank!!!"));
                    //lstError.Add(new ListError(5, "Lost Carat can not be blank!!!"));
                    //lstError.Add(new ListError(5, "Rejection Carat can not be blank!!!"));
                    //lstError.Add(new ListError(5, "RR Carat can not be blank!!!"));
                    //lstError.Add(new ListError(5, "Resawing Carat can not be blank!!!"));
                    lstError.Add(new ListError(5, "Breakage Carat can not be blank!!!"));

                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtLossCarat.Focus();
                    }
                }
                if (Val.ToInt(lblOsPcs.Text) != 0)
                {
                    if ((txtLossPcs.Text == string.Empty || txtLossPcs.Text == "0") && (txtReturnPcs.Text == string.Empty || txtReturnPcs.Text == "0"))// && (txtLostPcs.Text == string.Empty || txtLostPcs.Text == "0") && (txtRejPcs.Text == string.Empty || txtRejPcs.Text == "0") && (txtOutpcs.Text == string.Empty || txtOutpcs.Text == "0") && (txtReSoingPcs.Text == string.Empty || txtReSoingPcs.Text == "0") && (txtBreakPcs.Text == string.Empty || txtBreakPcs.Text == "0"))
                    {
                        lstError.Add(new ListError(5, "Return Pcs can not be blank!!!"));
                        lstError.Add(new ListError(5, "Loss Pcs can not be blank!!!"));
                        //lstError.Add(new ListError(5, "Lost Pcs can not be blank!!!"));
                        //lstError.Add(new ListError(5, "Rejection Pcs can not be blank!!!"));
                        //lstError.Add(new ListError(5, "RR Pcs can not be blank!!!"));
                        //lstError.Add(new ListError(5, "Resawing Pcs can not be blank!!!"));
                        lstError.Add(new ListError(5, "Breakage Pcs can not be blank!!!"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtLossCarat.Focus();
                        }
                    }
                }
                if (Val.ToDecimal(lblOsCarat.Text) != Val.ToDecimal(txtReturnCarat.Text) + Val.ToDecimal(txtRejCarat.Text) + Val.ToDecimal(txtLossCarat.Text) + Val.ToDecimal(txtLostCarat.Text) + Val.ToDecimal(txtOutCarat.Text) + Val.ToDecimal(txtResoingCarat.Text) + Val.ToDecimal(txtBreakCarat.Text))
                {
                    lstError.Add(new ListError(5, "Entry Carat not greater than total Carat"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtLossCarat.Focus();
                    }
                }
                if (Val.ToDecimal(lblOsPcs.Text) != Val.ToDecimal(txtReturnPcs.Text) + Val.ToDecimal(txtRejPcs.Text) + Val.ToDecimal(txtLossPcs.Text) + Val.ToDecimal(txtLostPcs.Text) + Val.ToDecimal(txtOutpcs.Text) + Val.ToDecimal(txtReSoingPcs.Text) + Val.ToDecimal(txtBreakPcs.Text))
                {
                    lstError.Add(new ListError(5, "Entry Pcs not greater than total Pcs"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtLossCarat.Focus();
                    }
                }

                if (Val.ToDecimal(lblOsCarat.Text) < Val.ToDecimal(txtReturnCarat.Text))
                {
                    lstError.Add(new ListError(5, "Enter carat not greater then O/s carat"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtReturnCarat.Focus();
                    }
                }

                if (Val.ToDecimal(lblOsPcs.Text) < Val.ToDecimal(txtReturnPcs.Text))
                {
                    lstError.Add(new ListError(5, "Enter pcs not greater then O/s pcs"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtReturnPcs.Focus();
                    }
                }

                if (Val.ToDecimal(txtAmount.Text) == 0)
                {
                    lstError.Add(new ListError(5, "Amount should not blank"));
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
                if (!GenerateProcessDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }

                if ((GlobalDec.gEmployeeProperty.user_name == "KANAIYA" && Val.ToString(lblMode.Text) == "EDIT") || (GlobalDec.gEmployeeProperty.user_name == "PARAG" && Val.ToString(lblMode.Text) == "EDIT") || (GlobalDec.gEmployeeProperty.user_name == "KAILASH" && Val.ToString(lblMode.Text) == "EDIT"))
                {
                    lueKapan.Enabled = true;
                    lueCutNo.Enabled = true;
                }
                else
                {
                    lueKapan.EditValue = System.DBNull.Value;
                    lueCutNo.EditValue = System.DBNull.Value;

                    lueFromParty.EditValue = System.DBNull.Value;
                    lueFromManager.EditValue = System.DBNull.Value;
                    lueEmployee.EditValue = System.DBNull.Value;

                    lueToCompany.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.company_id);
                    lueToBranch.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.branch_id);

                    lueToLocation.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.location_id);
                    lueToDepartment.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.department_id);

                    lueFromCompany.EditValue = null;
                    lueFromBranch.EditValue = null;
                    lueFromLocation.EditValue = null;
                    lueFromDepartment.EditValue = null;

                    lueFromProcess.EditValue = System.DBNull.Value;
                    lueFromSubProcess.EditValue = System.DBNull.Value;

                    lueKapan.Enabled = false;
                    lueCutNo.Enabled = false;
                }

                txtReturnPcs.Text = string.Empty;
                txtReturnCarat.Text = string.Empty;
                txtLossPcs.Text = string.Empty;
                txtLossCarat.Text = string.Empty;
                txtLostPcs.Text = string.Empty;
                txtLostCarat.Text = string.Empty;
                txtRejPcs.Text = string.Empty;
                txtRejCarat.Text = string.Empty;
                txtOutpcs.Text = string.Empty;
                txtOutCarat.Text = string.Empty;
                txtReSoingPcs.Text = string.Empty;
                txtResoingCarat.Text = string.Empty;
                txtBreakPcs.Text = string.Empty;
                txtBreakCarat.Text = string.Empty;
                txtLotID.Text = string.Empty;
                txtRate.Text = string.Empty;
                txtAmount.Text = string.Empty;
                txtTotalPcs.Text = string.Empty;
                txtTotalCarat.Text = string.Empty;
                txtJangedNo.Text = string.Empty;
                txtPercentage.Text = string.Empty;
                Prediction_Id = 0;
                if (GlobalDec.gEmployeeProperty.role_name == "SURAT MAKABLE")
                {
                    lueToProcess.EditValue = 4;
                    lueToSubProcess.EditValue = 2005;
                    lueToManager.EditValue = Val.ToInt64(4018);
                    lueToParty.EditValue = Val.ToInt32(132);
                }
                if (GlobalDec.gEmployeeProperty.role_name == "SURAT RUSSIAN")
                {
                    lueToProcess.EditValue = 2009;
                    lueToSubProcess.EditValue = 2023;
                    //lueToManager.EditValue = System.DBNull.Value;
                    //lueToManager.EditValue = Val.ToInt64(34515);
                    lueToParty.EditValue = Val.ToInt32(1351);
                }

                if ((GlobalDec.gEmployeeProperty.user_name == "KANAIYA" && Val.ToString(lblMode.Text) == "EDIT") || (GlobalDec.gEmployeeProperty.user_name == "PARAG" && Val.ToString(lblMode.Text) == "EDIT") || (GlobalDec.gEmployeeProperty.user_name == "KAILASH" && Val.ToString(lblMode.Text) == "EDIT"))
                {
                    chkIsKapan.Checked = true;
                    pnlUpdateLots.Visible = true;
                }
                else
                {
                    chkIsKapan.Checked = false;
                    pnlUpdateLots.Visible = false;
                }
                lblOsCarat.Text = "0";
                lblOsPcs.Text = "0";
                lblMode.Text = "NEW";

                m_manager_id = 0;
                m_emp_id = 0;
                txtLotID.Focus();
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
                m_dtbReceiveProcess.Columns.Add("kapan_id", typeof(int));
                m_dtbReceiveProcess.Columns.Add("cut_id", typeof(int));
                m_dtbReceiveProcess.Columns.Add("cut_no", typeof(string));
                m_dtbReceiveProcess.Columns.Add("manager_id", typeof(int));

                m_dtbReceiveProcess.Columns.Add("employee", typeof(string));
                m_dtbReceiveProcess.Columns.Add("employee_id", typeof(int));
                m_dtbReceiveProcess.Columns.Add("process", typeof(string));
                m_dtbReceiveProcess.Columns.Add("process_id", typeof(int));
                m_dtbReceiveProcess.Columns.Add("machine", typeof(string));
                m_dtbReceiveProcess.Columns.Add("machine_id", typeof(int));
                m_dtbReceiveProcess.Columns.Add("subprocess", typeof(string));
                m_dtbReceiveProcess.Columns.Add("sub_process_id", typeof(int));
                m_dtbReceiveProcess.Columns.Add("return_pcs", typeof(int)).DefaultValue = 0;
                m_dtbReceiveProcess.Columns.Add("return_carat", typeof(decimal)).DefaultValue = 0;
                m_dtbReceiveProcess.Columns.Add("loss_pcs", typeof(int)).DefaultValue = 0;
                m_dtbReceiveProcess.Columns.Add("loss_carat", typeof(decimal)).DefaultValue = 0;
                m_dtbReceiveProcess.Columns.Add("lost_pcs", typeof(int)).DefaultValue = 0;
                m_dtbReceiveProcess.Columns.Add("lost_carat", typeof(decimal)).DefaultValue = 0;
                m_dtbReceiveProcess.Columns.Add("rejection_pcs", typeof(int)).DefaultValue = 0;
                m_dtbReceiveProcess.Columns.Add("rejection_carat", typeof(decimal)).DefaultValue = 0;
                m_dtbReceiveProcess.Columns.Add("rr_pcs", typeof(int)).DefaultValue = 0;
                m_dtbReceiveProcess.Columns.Add("rr_carat", typeof(decimal)).DefaultValue = 0;
                m_dtbReceiveProcess.Columns.Add("sr_no", typeof(decimal)).DefaultValue = 1;
                m_dtbReceiveProcess.Columns.Add("issue_id", typeof(int)).DefaultValue = 0;
                m_dtbReceiveProcess.Columns.Add("balance", typeof(decimal)).DefaultValue = 0;
                m_dtbReceiveProcess.Columns.Add("resoing_pcs", typeof(int)).DefaultValue = 0;
                m_dtbReceiveProcess.Columns.Add("resoing_carat", typeof(decimal)).DefaultValue = 0;

                m_dtbReceiveProcess.Columns.Add("breakage_pcs", typeof(int)).DefaultValue = 0;
                m_dtbReceiveProcess.Columns.Add("breakage_carat", typeof(decimal)).DefaultValue = 0;
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
        public void EnDis(bool O, bool C)
        {
            //btnOpenPort.Enabled = O;
            //btnClosePort.Enabled = C;
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
