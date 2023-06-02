using BLL;
using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.PropertyClasses.Transaction;
using BLL.PropertyClasses.Transaction.MFG;
using DERP.Class;
using DERP.Master;
using DERP.Master.MFG;
using DevExpress.XtraEditors;
using DREP.Master;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using static DERP.Master.FrmMappingMaster;

namespace DERP.Transaction.MFG
{
    public partial class FrmMFGGalaxyMultiEployeeReturn : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        Control _NextEnteredControl;
        private List<Control> _tabControls;

        MFGProcessReceive objProcessRecieve;
        MultiEmployeeReturn objMFGMultiEmp = new MultiEmployeeReturn();
        MFGCutCreate objCutCreate;
        MFGProcessIssue objMFGProcessIssue;

        DataTable DtControlSettings;
        DataTable m_dtbDetail;
        DataTable m_dtbParam;
        DataTable m_dtbSubProcess;
        DataTable m_dtbReceiveProcess;
        DataTable m_dtOutstanding;
        DataTable m_dtbDetails;
        DataTable m_dtbKapan;

        int m_Srno;
        int m_update_srno;
        Int64 m_issue_id;
        int m_manager_id;
        int m_emp_id;
        Int64 m_numForm_id;
        Int64 IntRes;
        Int64 m_kapan_id;
        int m_BalPcs;
        int m_emp;
        string m_cut_no;

        int m_old_RetPcs;
        int m_old_OutPcs;
        int m_old_RejPcs;
        int m_old_ResawPcs;
        int m_old_BrkPcs;
        int m_old_LossPcs;
        int m_old_LostPcs;
        int m_isMainLot;
        decimal m_old_OutCarat;
        decimal m_old_RejCarat;
        decimal m_old_ResawCarat;
        decimal m_old_BrkCarat;
        decimal m_old_RetCarat;
        decimal m_numlosscarat;
        decimal m_numlostcarat;
        decimal m_old_losscarat;
        decimal m_old_lostcarat;
        decimal m_OsCarat;
        int m_OsPcs;
        decimal m_OrgCarat;
        int m_OrgPcs;
        decimal m_BalCarat;

        bool m_blnadd;
        bool m_blnsave;

        private bool comOpen;

        private string readBuffer;

        private int Bytenumber;

        private char[] byteEnd;
        string manager_short_name = string.Empty;

        //[AccessedThroughProperty("SerialPort1")]
        //private SerialPort _SerialPort1;

        [AccessedThroughProperty("BtnOpen")]
        private Button _BtnOpen;
        string Main_Lot_ID = string.Empty;

        DataTable dtIssOS;

        #endregion

        #region Constructor
        public FrmMFGGalaxyMultiEployeeReturn()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            _NextEnteredControl = new Control();
            _tabControls = new List<Control>();

            objProcessRecieve = new MFGProcessReceive();
            objCutCreate = new MFGCutCreate();
            objMFGProcessIssue = new MFGProcessIssue();
            DtControlSettings = new DataTable();
            m_dtbDetail = new DataTable();
            m_dtbParam = new DataTable();
            m_dtbSubProcess = new DataTable();
            m_dtbReceiveProcess = new DataTable();
            m_dtOutstanding = new DataTable();
            m_dtbDetails = new DataTable();
            m_dtbKapan = new DataTable();
            dtIssOS = new DataTable();

            m_Srno = 1;
            m_update_srno = 1;
            m_issue_id = 0;
            m_manager_id = 0;
            m_emp_id = 0;
            m_numForm_id = 0;
            m_kapan_id = 0;
            m_BalPcs = 0;
            m_emp = 0;
            m_isMainLot = 0;
            m_cut_no = "";

            m_numlosscarat = 0;
            m_numlostcarat = 0;
            m_old_losscarat = 0;
            m_old_lostcarat = 0;
            m_OsCarat = 0;
            m_OsPcs = 0;
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
            if (GlobalDec.gEmployeeProperty.department_name == "4P SAWING" || GlobalDec.gEmployeeProperty.department_name == "4P PLAT")
            {
                Global.Message("Don't have permission...Please Contact to Administrator...");
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
        private void RepDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (Global.Confirm("Are you sure delete selected row?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                dgvMultiEmployeeReturn.DeleteRow(dgvMultiEmployeeReturn.GetRowHandle(dgvMultiEmployeeReturn.FocusedRowHandle));
                m_dtbReceiveProcess.AcceptChanges();
            }
        }
        private void FrmMFGProcessWeightLossRecieve_Load(object sender, EventArgs e)
        {
            Global.LOOKUPEmp(lueEmployee);
            Global.LOOKUPManager(lueManager);
            Global.LOOKUPMachine(lueMachine);
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

            ClearDetails();

            if (lueSubProcess.Text == "")
            {
                //lblRetPcs.Visible = false;
                //txtReturnPcs.Visible = false;

                //lblRetCarat.Visible = false;
                //txtReturnCarat.Visible = false;
                if (GlobalDec.gEmployeeProperty.user_name == "KETAN")
                {
                    lblOutPcs.Visible = true;
                    lblOutCarat.Visible = true;

                    txtOutpcs.Visible = true;
                    txtOutCarat.Visible = true;

                    txtOutpcs.Enabled = true;
                }
                else
                {
                    lblOutPcs.Visible = false;
                    lblOutCarat.Visible = false;

                    txtOutpcs.Visible = false;
                    txtOutCarat.Visible = false;
                }
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
                lblFinalWeight.Visible = false;
                txtCaratPlus.Visible = false;
                lblCaratPlus.Visible = false;
            }
            if (GlobalDec.gEmployeeProperty.role_name == "SURAT 4P OK")
            {
                btnOpenPort_Click(RuntimeHelpers.GetObjectValue(sender), e);
            }
            else
            {
                txtBoilingLotID.Visible = false;
                label7.Visible = false;
                txtBoilingLotID.Text = string.Empty;
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
                //string Str = "";
                //if (Val.DBDate(BLL.GlobalDec.gStrServerDate) != Val.DBDate(dtpReceiveDate.Text))
                //{
                //    Str = GlobalDec.CheckLockIsOpenOrNot(Val.DBDate(dtpReceiveDate.Text), Val.DBTime(DateTime.Now.ToShortTimeString())); //Val.GetFullTime12());
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
                //        dtpReceiveDate.Enabled = true;
                //        dtpReceiveDate.Visible = true;
                //    }
                //}

                btnSave.Enabled = false;

                m_blnsave = true;
                m_blnadd = false;
                if (!ValidateDetails())
                {
                    m_blnsave = false;
                    btnSave.Enabled = true;
                    return;
                }
                if (Main_Lot_ID == "")
                {
                    foreach (DataRow drw in m_dtbReceiveProcess.Rows)
                    {
                        if (GlobalDec.gEmployeeProperty.role_name == "SURAT 4P" || GlobalDec.gEmployeeProperty.role_name == "SURAT 4P OK" || GlobalDec.gEmployeeProperty.role_name == "SURAT SARIN" || GlobalDec.gEmployeeProperty.role_name == "SURAT 4P ENTRY" || GlobalDec.gEmployeeProperty.role_name == "SURAT RUSSIAN" || GlobalDec.gEmployeeProperty.role_name == "SURAT POLISH REPARING" || GlobalDec.gEmployeeProperty.role_name == "SURAT KATARGAM" || GlobalDec.gEmployeeProperty.role_name == "SURAT KAMALA" || GlobalDec.gEmployeeProperty.role_name == "SURAT GALAXY")
                        {
                            MFGProcessIssue objMFGProcessIssue = new MFGProcessIssue();
                            MFGProcessIssueProperty objMFGProcessIssueProperty = new MFGProcessIssueProperty();

                            objMFGProcessIssueProperty.lot_id = Val.ToInt64(drw["lot_id"]);
                            objMFGProcessIssueProperty.process_id = Val.ToInt32(drw["process_id"]);
                            objMFGProcessIssueProperty.sub_process_id = Val.ToInt32(drw["sub_process_id"]);
                            objMFGProcessIssueProperty = objMFGProcessIssue.GetData_PrevProcessRec(objMFGProcessIssueProperty);

                            if (objMFGProcessIssueProperty.Messgae != "" && objMFGProcessIssueProperty.Messgae != null)
                            {
                                Global.Message(objMFGProcessIssueProperty.Messgae);
                                btnSave.Enabled = true;
                                return;
                            }
                        }
                        int DateCheck = Global.ValidateDate(Val.ToInt(drw["lot_id"]), Val.ToString(dtpReceiveDate.Text));
                        if (DateCheck == 0)
                        {
                            Global.Message("Plz Check Recieve Date is less than Janged Date " + Val.ToInt(drw["lot_id"]));
                            btnSave.Enabled = true;
                            return;
                        }
                    }
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
            Global.Export("xlsx", dgvMultiEmployeeReturn);
        }
        private void lueProcess_EditValueChanged_2(object sender, EventArgs e)
        {
            try
            {
                MFGProcessIssue objMFGProcessIssue = new MFGProcessIssue();
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

                        if (lueProcess.EditValue != null)
                        {
                            DataTable dtbEmployee = new DataTable();
                            dtbEmployee = objMFGProcessIssue.GetEmployeeMapping(Val.ToInt(lueProcess.EditValue), Val.ToInt(lueSubProcess.EditValue));
                            if (dtbEmployee.Rows.Count > 0)
                            {
                                lueEmployee.Properties.DataSource = dtbEmployee;
                                lueEmployee.Properties.DisplayMember = "employee_name";
                                lueEmployee.Properties.ValueMember = "employee_id";
                            }
                        }
                    }

                    if (lueProcess.Text == "4P-OK")
                    {
                        lblRetPcs.Enabled = false;
                        txtReturnPcs.Enabled = false;

                        lblRetCarat.Enabled = false;
                        txtReturnCarat.Enabled = false;

                        lblOutPcs.Enabled = false;
                        lblOutCarat.Enabled = false;

                        txtOutpcs.Enabled = false;
                        txtOutCarat.Enabled = false;

                        lblRejPcs.Enabled = false;
                        txtRejPcs.Enabled = false;

                        lblRejCarat.Enabled = false;
                        txtRejCarat.Enabled = false;

                        lblResoingPcs.Enabled = false;
                        lblResoingCarat.Enabled = false;

                        txtReSoingPcs.Enabled = false;
                        txtResoingCarat.Enabled = false;

                        lblLostCarat.Enabled = false;
                        lblLostPcs.Enabled = false;

                        lblBreakragePcs.Enabled = false;
                        lblBreakrageCarat.Enabled = false;
                        txtBreakPcs.Enabled = false;
                        txtBreakCarat.Enabled = false;

                        txtLostPcs.Enabled = false;
                        txtLostCarat.Enabled = false;

                        txtLossCarat.Enabled = false;
                        txtLossPcs.Enabled = false;
                        lblLossPcs.Enabled = false;
                        lblLossCarat.Enabled = false;
                        lblFinalWeight.Enabled = false;
                        txtCaratPlus.Enabled = false;
                        lblCaratPlus.Enabled = false;
                    }
                    if (lueProcess.Text == "RUSSIAN" || lueProcess.Text == "FARSI RUSSIAN" || lueProcess.Text == "CHAPKA")
                    {
                        lblRetPcs.Enabled = true;
                        txtReturnPcs.Enabled = true;

                        lblRetPcs.Visible = true;
                        txtReturnPcs.Visible = true;

                        lblRetCarat.Enabled = true;
                        txtReturnCarat.Enabled = true;

                        lblRetCarat.Visible = true;
                        txtReturnCarat.Visible = true;

                        lblOutPcs.Enabled = false;
                        lblOutCarat.Enabled = false;

                        txtOutpcs.Enabled = false;
                        txtOutCarat.Enabled = false;

                        lblRejPcs.Enabled = false;
                        txtRejPcs.Enabled = false;

                        lblRejCarat.Enabled = false;
                        txtRejCarat.Enabled = false;

                        lblResoingPcs.Enabled = false;
                        lblResoingCarat.Enabled = false;

                        txtReSoingPcs.Enabled = false;
                        txtResoingCarat.Enabled = false;

                        lblLostCarat.Enabled = true;
                        lblLostPcs.Enabled = true;

                        lblBreakragePcs.Enabled = true;
                        lblBreakrageCarat.Enabled = true;
                        txtBreakPcs.Enabled = true;
                        txtBreakCarat.Enabled = true;

                        lblBreakragePcs.Visible = true;
                        lblBreakrageCarat.Visible = true;
                        txtBreakPcs.Visible = true;
                        txtBreakCarat.Visible = true;

                        txtLostPcs.Enabled = true;
                        txtLostCarat.Enabled = true;

                        lblOutPcs.Visible = false;
                        lblOutCarat.Visible = false;
                        txtOutpcs.Visible = false;
                        txtOutCarat.Visible = false;

                        txtLossCarat.Enabled = true;
                        txtLossCarat.Visible = true;
                        txtLossPcs.Enabled = false;
                        lblLossPcs.Enabled = false;
                        lblLossCarat.Enabled = true;
                        lblLossCarat.Visible = true;
                        lblFinalWeight.Enabled = false;
                        txtCaratPlus.Enabled = false;
                        lblCaratPlus.Enabled = false;
                    }
                    if (lueProcess.Text == "4P PLAT")
                    {
                        lblRetPcs.Enabled = true;
                        txtReturnPcs.Enabled = true;

                        lblRetCarat.Enabled = false;
                        txtReturnCarat.Enabled = false;

                        lblRetPcs.Visible = true;
                        txtReturnPcs.Visible = true;

                        lblRetCarat.Visible = true;
                        txtReturnCarat.Visible = true;

                        lblOutPcs.Enabled = false;
                        lblOutCarat.Enabled = false;

                        txtOutpcs.Enabled = false;
                        txtOutCarat.Enabled = false;

                        lblRejPcs.Enabled = false;
                        txtRejPcs.Enabled = false;

                        lblRejCarat.Enabled = false;
                        txtRejCarat.Enabled = false;

                        lblResoingPcs.Enabled = false;
                        lblResoingCarat.Enabled = false;

                        txtReSoingPcs.Enabled = false;
                        txtResoingCarat.Enabled = false;

                        lblLostCarat.Enabled = false;
                        lblLostPcs.Enabled = false;

                        lblBreakragePcs.Enabled = false;
                        lblBreakrageCarat.Enabled = false;
                        txtBreakPcs.Enabled = false;
                        txtBreakCarat.Enabled = false;

                        txtLostPcs.Enabled = false;
                        txtLostCarat.Enabled = false;

                        txtLossCarat.Enabled = false;
                        txtLossPcs.Enabled = false;
                        lblLossPcs.Enabled = false;
                        lblLossCarat.Enabled = false;
                        lblFinalWeight.Enabled = false;
                        txtCaratPlus.Enabled = false;
                        lblCaratPlus.Enabled = false;
                    }
                    if (lueProcess.Text == "4P SAWING")
                    {
                        lblRetPcs.Enabled = true;
                        txtReturnPcs.Enabled = true;

                        lblRetCarat.Enabled = false;
                        txtReturnCarat.Enabled = false;

                        lblRetCarat.Visible = true;
                        txtReturnCarat.Visible = true;

                        lblRetPcs.Visible = true;
                        txtReturnPcs.Visible = true;

                        lblOutPcs.Enabled = false;
                        lblOutCarat.Enabled = false;

                        txtOutpcs.Enabled = false;
                        txtOutCarat.Enabled = false;

                        lblRejPcs.Enabled = false;
                        txtRejPcs.Enabled = false;

                        lblRejCarat.Enabled = false;
                        txtRejCarat.Enabled = false;

                        lblResoingPcs.Enabled = false;
                        lblResoingCarat.Enabled = false;

                        txtReSoingPcs.Enabled = false;
                        txtResoingCarat.Enabled = false;

                        lblLostCarat.Enabled = false;
                        lblLostPcs.Enabled = false;

                        lblBreakragePcs.Enabled = false;
                        lblBreakrageCarat.Enabled = false;
                        txtBreakPcs.Enabled = false;
                        txtBreakCarat.Enabled = false;

                        txtLostPcs.Enabled = false;
                        txtLostCarat.Enabled = false;

                        txtLossCarat.Enabled = false;
                        txtLossPcs.Enabled = false;
                        lblLossPcs.Enabled = false;
                        lblLossCarat.Enabled = false;
                        lblFinalWeight.Enabled = false;
                        txtCaratPlus.Enabled = false;
                        lblCaratPlus.Enabled = false;
                    }
                    if ((GlobalDec.gEmployeeProperty.role_name == "SURAT 4P" && GlobalDec.gEmployeeProperty.user_name == "KETAN") || (GlobalDec.gEmployeeProperty.role_name == "SURAT KATARGAM"))
                    {
                        lblMWeight.Visible = true;
                        txtManualCarat.Visible = true;
                        txtManualCarat.Enabled = true;
                        lblMPer.Visible = true;
                        txtManualPer.Visible = true;
                        txtManualPer.Enabled = true;
                    }
                    if ((GlobalDec.gEmployeeProperty.role_name == "GALAXY DW"))
                    {
                        lblLossCarat.Visible = true;
                        txtLossCarat.Visible = true;

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
                        if (GlobalDec.gEmployeeProperty.department_name == "CHAPKA")
                        {
                            m_dtOutstanding = objProcessRecieve.Carat_Chapka_OutStanding_GetData(Val.ToInt64(txtLotID.Text), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue), 1, "R");
                        }
                        else
                        {
                            m_dtOutstanding = objProcessRecieve.Carat_Sarin_OutStanding_GetData(Val.ToInt64(txtLotID.Text), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue), 1, "R");
                        }
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
        private void lueSubProcess_EditValueChanged(object sender, EventArgs e)
        {
            if ((GlobalDec.gEmployeeProperty.user_name == "JIYA" || GlobalDec.gEmployeeProperty.user_name == "SAROJ" || GlobalDec.gEmployeeProperty.user_name == "RUSHIKESH")
                && GlobalDec.gEmployeeProperty.department_name == "GALAXY 4P" && lueProcess.Text == "GALAXY" && lueSubProcess.Text == "4P OK")
            {
                lblBreakragePcs.Visible = true;
                lblBreakrageCarat.Visible = true;

                lblBreakragePcs.Enabled = true;
                lblBreakrageCarat.Enabled = true;

                txtBreakPcs.Visible = true;
                txtBreakCarat.Visible = true;

                txtBreakPcs.Enabled = true;
                txtBreakCarat.Enabled = true;

                lblRetPcs.Visible = true;
                lblRetCarat.Visible = true;

                txtReturnPcs.Visible = true;
                txtReturnCarat.Visible = true;

                txtReturnPcs.Enabled = true;
                txtReturnCarat.Enabled = true;
            }
            if (lueSubProcess.Text == "MARKING" && (GlobalDec.gEmployeeProperty.user_name == "HARESH" || GlobalDec.gEmployeeProperty.user_name == "CHIRAGD"))
            {
                txtOutpcs.Enabled = true;
                txtOutCarat.Enabled = true;

                txtOutpcs.Visible = true;
                txtOutCarat.Visible = true;

            }
            if (lueSubProcess.Text == "SETUP")
            {
                lblRetPcs.Visible = true;
                txtReturnPcs.Visible = true;

                lblRetCarat.Visible = true;
                txtReturnCarat.Visible = true;

                lblLostPcs.Visible = true;
                txtLostPcs.Visible = true;

                lblLostCarat.Visible = true;
                txtLostCarat.Visible = true;

                lblRetPcs.Enabled = true;
                txtReturnPcs.Enabled = true;

                lblRetCarat.Enabled = true;
                txtReturnCarat.Enabled = true;

                lblLostPcs.Enabled = true;
                txtLostPcs.Enabled = true;

                lblLostCarat.Enabled = true;
                txtLostCarat.Enabled = true;

                /////////////////////////
                lblBreakragePcs.Visible = false;
                txtBreakPcs.Visible = false;

                lblBreakrageCarat.Visible = false;
                txtBreakCarat.Visible = false;

                lblLostPcs.Visible = false;
                txtLostPcs.Visible = false;

                lblLostCarat.Visible = false;
                txtLostCarat.Visible = false;

                lblBreakragePcs.Enabled = false;
                txtBreakPcs.Enabled = false;

                lblBreakrageCarat.Enabled = false;
                txtBreakCarat.Enabled = false;

                lblLostPcs.Enabled = false;
                txtLostPcs.Enabled = false;

                lblLostCarat.Enabled = false;
                txtLostCarat.Enabled = false;

                ////

                lblOutPcs.Visible = false;
                txtOutpcs.Visible = false;

                lblOutCarat.Visible = false;
                txtOutCarat.Visible = false;

                lblOutPcs.Enabled = false;
                txtOutpcs.Enabled = false;

                lblOutCarat.Enabled = false;
                txtOutCarat.Enabled = false;

            }
            if (lueSubProcess.Text == "DROPING")
            {
                lblBreakragePcs.Visible = true;
                txtBreakPcs.Visible = true;

                lblBreakrageCarat.Visible = true;
                txtBreakCarat.Visible = true;

                lblLostPcs.Visible = true;
                txtLostPcs.Visible = true;

                lblLostCarat.Visible = true;
                txtLostCarat.Visible = true;

                lblBreakragePcs.Enabled = true;
                txtBreakPcs.Enabled = true;

                lblBreakrageCarat.Enabled = true;
                txtBreakCarat.Enabled = true;

                lblLostPcs.Enabled = true;
                txtLostPcs.Enabled = true;

                lblLostCarat.Enabled = true;
                txtLostCarat.Enabled = true;

                ////////////////////

                //lblRetPcs.Visible = false;
                //txtReturnPcs.Visible = false;

                //lblRetCarat.Visible = false;
                //txtReturnCarat.Visible = false;

                lblLostPcs.Visible = false;
                txtLostPcs.Visible = false;

                lblLostCarat.Visible = false;
                txtLostCarat.Visible = false;

                //lblRetPcs.Enabled = false;
                //txtReturnPcs.Enabled = false;

                //lblRetCarat.Enabled = false;
                //txtReturnCarat.Enabled = false;

                lblLostPcs.Enabled = false;
                txtLostPcs.Enabled = false;

                lblLostCarat.Enabled = false;
                txtLostCarat.Enabled = false;

                ////
                lblOutPcs.Visible = false;
                txtOutpcs.Visible = false;

                lblOutCarat.Visible = false;
                txtOutCarat.Visible = false;

                lblOutPcs.Enabled = false;
                txtOutpcs.Enabled = false;

                lblOutCarat.Enabled = false;
                txtOutCarat.Enabled = false;
            }
            if (lueSubProcess.Text == "MARKING" || lueSubProcess.Text == "PLANNING")
            {

                lblOutPcs.Visible = true;
                txtOutpcs.Visible = true;

                lblOutCarat.Visible = true;
                txtOutCarat.Visible = true;

                lblOutPcs.Enabled = true;
                txtOutpcs.Enabled = true;

                lblOutCarat.Enabled = true;
                txtOutCarat.Enabled = true;

                ///////////
                //lblRetPcs.Visible = false;
                //txtReturnPcs.Visible = false;

                //lblRetCarat.Visible = false;
                //txtReturnCarat.Visible = false;

                lblLostPcs.Visible = false;
                txtLostPcs.Visible = false;

                lblLostCarat.Visible = false;
                txtLostCarat.Visible = false;

                //lblRetPcs.Enabled = false;
                //txtReturnPcs.Enabled = false;

                //lblRetCarat.Enabled = false;
                //txtReturnCarat.Enabled = false;

                lblLostPcs.Enabled = false;
                txtLostPcs.Enabled = false;

                lblLostCarat.Enabled = false;
                txtLostCarat.Enabled = false;

                lblBreakragePcs.Visible = false;
                txtBreakPcs.Visible = false;

                lblBreakrageCarat.Visible = false;
                txtBreakCarat.Visible = false;

                lblLostPcs.Visible = false;
                txtLostPcs.Visible = false;

                lblLostCarat.Visible = false;
                txtLostCarat.Visible = false;

                lblBreakragePcs.Enabled = false;
                txtBreakPcs.Enabled = false;

                lblBreakrageCarat.Enabled = false;
                txtBreakCarat.Enabled = false;

                lblLostPcs.Enabled = false;
                txtLostPcs.Enabled = false;

                lblLostCarat.Enabled = false;
                txtLostCarat.Enabled = false;
            }
            if (lueSubProcess.Text == "SCANING" || lueSubProcess.Text == "4P")
            {

                if (GlobalDec.gEmployeeProperty.department_name == "GALAXY" && GlobalDec.gEmployeeProperty.branch_name == "KAMALA ESTATE" && lueSubProcess.Text == "SCANING")
                {

                    lblRejPcs.Visible = true;
                    txtRejPcs.Visible = true;

                    lblRejPcs.Enabled = true;
                    txtRejPcs.Enabled = true;

                    lblRejCarat.Visible = true;
                    txtRejCarat.Visible = true;

                    lblRejCarat.Enabled = true;
                    txtRejCarat.Enabled = true;

                    lblOutPcs.Visible = false;
                    txtOutpcs.Visible = false;

                    lblOutCarat.Visible = false;
                    txtOutCarat.Visible = false;

                    lblOutPcs.Enabled = false;
                    txtOutpcs.Enabled = false;

                    lblOutCarat.Enabled = false;
                    txtOutCarat.Enabled = false;

                    ///////////
                    //lblRetPcs.Visible = false;
                    //txtReturnPcs.Visible = false;

                    //lblRetCarat.Visible = false;
                    //txtReturnCarat.Visible = false;

                    lblLostPcs.Visible = false;
                    txtLostPcs.Visible = false;

                    lblLostCarat.Visible = false;
                    txtLostCarat.Visible = false;

                    //lblRetPcs.Enabled = false;
                    //txtReturnPcs.Enabled = false;

                    //lblRetCarat.Enabled = false;
                    //txtReturnCarat.Enabled = false;

                    lblLostPcs.Enabled = false;
                    txtLostPcs.Enabled = false;

                    lblLostCarat.Enabled = false;
                    txtLostCarat.Enabled = false;

                    lblBreakragePcs.Visible = true;
                    txtBreakPcs.Visible = true;

                    lblBreakrageCarat.Visible = true;
                    txtBreakCarat.Visible = true;

                    lblLostPcs.Visible = false;
                    txtLostPcs.Visible = false;

                    lblLostCarat.Visible = false;
                    txtLostCarat.Visible = false;

                    lblBreakragePcs.Enabled = true;
                    txtBreakPcs.Enabled = true;

                    lblBreakrageCarat.Enabled = true;
                    txtBreakCarat.Enabled = true;

                    lblLostPcs.Enabled = false;
                    txtLostPcs.Enabled = false;

                    lblLostCarat.Enabled = false;
                    txtLostCarat.Enabled = false;
                }
                else
                {
                    lblOutPcs.Visible = true;
                    txtOutpcs.Visible = true;

                    lblOutCarat.Visible = true;
                    txtOutCarat.Visible = true;

                    lblOutPcs.Enabled = true;
                    txtOutpcs.Enabled = true;

                    lblOutCarat.Enabled = true;
                    txtOutCarat.Enabled = true;

                    ///////////
                    //lblRetPcs.Visible = false;
                    //txtReturnPcs.Visible = false;

                    //lblRetCarat.Visible = false;
                    //txtReturnCarat.Visible = false;

                    lblLostPcs.Visible = false;
                    txtLostPcs.Visible = false;

                    lblLostCarat.Visible = false;
                    txtLostCarat.Visible = false;

                    //lblRetPcs.Enabled = false;
                    //txtReturnPcs.Enabled = false;

                    //lblRetCarat.Enabled = false;
                    //txtReturnCarat.Enabled = false;

                    lblLostPcs.Enabled = false;
                    txtLostPcs.Enabled = false;

                    lblLostCarat.Enabled = false;
                    txtLostCarat.Enabled = false;

                    lblBreakragePcs.Visible = true;
                    txtBreakPcs.Visible = true;

                    lblBreakrageCarat.Visible = true;
                    txtBreakCarat.Visible = true;

                    lblLostPcs.Visible = false;
                    txtLostPcs.Visible = false;

                    lblLostCarat.Visible = false;
                    txtLostCarat.Visible = false;

                    lblBreakragePcs.Enabled = true;
                    txtBreakPcs.Enabled = true;

                    lblBreakrageCarat.Enabled = true;
                    txtBreakCarat.Enabled = true;

                    lblLostPcs.Enabled = false;
                    txtLostPcs.Enabled = false;

                    lblLostCarat.Enabled = false;
                    txtLostCarat.Enabled = false;
                }
            }
            if (lueSubProcess.Text == "ACTIVE PART" || lueSubProcess.Text == "LS OK")
            {
                lblRetPcs.Visible = true;
                txtReturnPcs.Visible = true;

                lblRetCarat.Visible = true;
                txtReturnCarat.Visible = true;

                lblLostPcs.Visible = false;
                txtLostPcs.Visible = false;

                lblLostCarat.Visible = false;
                txtLostCarat.Visible = false;

                lblRetPcs.Enabled = true;
                txtReturnPcs.Enabled = true;

                lblRetCarat.Enabled = true;
                txtReturnCarat.Enabled = true;

                lblLostPcs.Enabled = false;
                txtLostPcs.Enabled = false;

                lblLostCarat.Enabled = false;
                txtLostCarat.Enabled = false;

                /////////////////////////
                lblBreakragePcs.Visible = false;
                txtBreakPcs.Visible = false;

                lblBreakrageCarat.Visible = false;
                txtBreakCarat.Visible = false;

                lblLostPcs.Visible = false;
                txtLostPcs.Visible = false;

                lblLostCarat.Visible = false;
                txtLostCarat.Visible = false;

                lblBreakragePcs.Enabled = false;
                txtBreakPcs.Enabled = false;

                lblBreakrageCarat.Enabled = false;
                txtBreakCarat.Enabled = false;

                lblLostPcs.Enabled = false;
                txtLostPcs.Enabled = false;

                lblLostCarat.Enabled = false;
                txtLostCarat.Enabled = false;

                ////

                lblOutPcs.Visible = true;
                txtOutpcs.Visible = true;

                lblOutCarat.Visible = true;
                txtOutCarat.Visible = true;

                lblOutPcs.Enabled = true;
                txtOutpcs.Enabled = true;

                lblOutCarat.Enabled = true;
                txtOutCarat.Enabled = true;

            }
            if (lueProcess.EditValue != System.DBNull.Value && lueSubProcess.EditValue != System.DBNull.Value && Val.ToInt64(txtLotID.Text) != 0)
            {

                DataTable dtbEmployee = new DataTable();
                MFGProcessIssue objMFGProcessIssue = new MFGProcessIssue();
                dtbEmployee = objMFGProcessIssue.GetEmployeeMapping(Val.ToInt(lueProcess.EditValue), Val.ToInt(lueSubProcess.EditValue));
                if (dtbEmployee.Rows.Count > 0)
                {
                    lueEmployee.Properties.DataSource = dtbEmployee;
                    lueEmployee.Properties.DisplayMember = "employee_name";
                    lueEmployee.Properties.ValueMember = "employee_id";
                }

                DataTable dtIss = new DataTable();
                dtIss = objProcessRecieve.GetIssueID(Val.ToInt64(txtLotID.Text), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue));
                if (dtIss.Rows.Count > 0)
                {
                    m_manager_id = Val.ToInt(dtIss.Rows[0]["manager_id"]);
                    m_emp_id = Val.ToInt(dtIss.Rows[0]["employee_id"]);
                    m_issue_id = Val.ToInt64(dtIss.Rows[0]["issue_id"]);
                    if (GlobalDec.gEmployeeProperty.department_name == "CHAPKA")
                    {
                        m_dtOutstanding = objProcessRecieve.Carat_Chapka_OutStanding_GetData(Val.ToInt64(txtLotID.Text), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue), 1, "R");
                    }
                    else
                    {
                        m_dtOutstanding = objProcessRecieve.Carat_Sarin_OutStanding_GetData(Val.ToInt64(txtLotID.Text), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue), 1, "R");
                    }

                    if (m_dtOutstanding.Rows.Count > 0)
                    {
                        m_OsPcs = Val.ToInt(m_dtOutstanding.Rows[0]["outstanding_pcs"]);
                        m_OsCarat = Val.ToDecimal(m_dtOutstanding.Rows[0]["outstanding_carat"]);

                        lblOsPcs.Text = Val.ToString(m_dtOutstanding.Rows[0]["outstanding_pcs"]);
                        lblOsCarat.Text = Val.ToString(m_dtOutstanding.Rows[0]["outstanding_carat"]);

                        txtReturnPcs.Text = Val.ToString(m_dtOutstanding.Rows[0]["balance_pcs"]);
                        txtReturnCarat.Text = Val.ToString(m_dtOutstanding.Rows[0]["balance_carat"]);

                        txtOutpcs.Text = Val.ToString(m_dtOutstanding.Rows[0]["out_pcs"]);
                        txtOutCarat.Text = Val.ToString(m_dtOutstanding.Rows[0]["out_carat"]);

                        txtRejPcs.Text = Val.ToString(m_dtOutstanding.Rows[0]["rejection_pcs"]);
                        txtRejCarat.Text = Val.ToString(m_dtOutstanding.Rows[0]["rejection_carat"]);

                        txtBreakPcs.Text = Val.ToString(m_dtOutstanding.Rows[0]["breakage_pcs"]);
                        txtBreakCarat.Text = Val.ToString(m_dtOutstanding.Rows[0]["breakage_carat"]);

                        txtReSoingPcs.Text = Val.ToString(m_dtOutstanding.Rows[0]["resoing_pcs"]);
                        txtResoingCarat.Text = Val.ToString(m_dtOutstanding.Rows[0]["resoing_carat"]);

                        lueProcess.EditValue = Val.ToInt32(m_dtOutstanding.Rows[0]["process_id"]);
                        lueSubProcess.EditValue = Val.ToInt32(m_dtOutstanding.Rows[0]["sub_process_id"]);

                        lueManager.EditValue = Val.ToInt64(m_manager_id);
                        lueEmployee.EditValue = Val.ToInt64(m_emp_id);
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

                if (lueSubProcess.Text == "SETUP")
                {
                    lblRetPcs.Visible = true;
                    txtReturnPcs.Visible = true;

                    lblRetCarat.Visible = true;
                    txtReturnCarat.Visible = true;

                    lblLostPcs.Visible = true;
                    txtLostPcs.Visible = true;

                    lblLostCarat.Visible = true;
                    txtLostCarat.Visible = true;

                    lblRetPcs.Enabled = true;
                    txtReturnPcs.Enabled = true;

                    lblRetCarat.Enabled = true;
                    txtReturnCarat.Enabled = true;

                    lblLostPcs.Enabled = true;
                    txtLostPcs.Enabled = true;

                    lblLostCarat.Enabled = true;
                    txtLostCarat.Enabled = true;
                }
                if (lueSubProcess.Text == "DROPING")
                {
                    lblBreakragePcs.Visible = true;
                    txtBreakPcs.Visible = true;

                    lblBreakrageCarat.Visible = true;
                    txtBreakCarat.Visible = true;

                    lblLostPcs.Visible = true;
                    txtLostPcs.Visible = true;

                    lblLostCarat.Visible = true;
                    txtLostCarat.Visible = true;

                    lblBreakragePcs.Enabled = true;
                    txtBreakPcs.Enabled = true;

                    lblBreakrageCarat.Enabled = true;
                    txtBreakCarat.Enabled = true;

                    lblLostPcs.Enabled = true;
                    txtLostPcs.Enabled = true;

                    lblLostCarat.Enabled = true;
                    txtLostCarat.Enabled = true;


                }
                if (lueSubProcess.Text == "MARKING" || lueSubProcess.Text == "PLANNING")
                {

                    lblOutPcs.Visible = true;
                    txtOutpcs.Visible = true;

                    lblOutCarat.Visible = true;
                    txtOutCarat.Visible = true;

                    lblOutPcs.Enabled = true;
                    txtOutpcs.Enabled = true;

                    lblOutCarat.Enabled = true;
                    txtOutCarat.Enabled = true;
                }
                if (lueSubProcess.Text == "SCANING" || lueSubProcess.Text == "4P")
                {

                    lblOutPcs.Visible = true;
                    txtOutpcs.Visible = true;

                    lblOutCarat.Visible = true;
                    txtOutCarat.Visible = true;

                    lblOutPcs.Enabled = true;
                    txtOutpcs.Enabled = true;

                    lblOutCarat.Enabled = true;
                    txtOutCarat.Enabled = true;

                    lblBreakragePcs.Visible = true;
                    lblBreakrageCarat.Visible = true;
                    txtBreakPcs.Visible = true;
                    txtBreakCarat.Visible = true;

                    lblBreakragePcs.Enabled = true;
                    txtBreakPcs.Enabled = true;

                    lblBreakrageCarat.Enabled = true;
                    txtBreakCarat.Enabled = true;
                }
                if (lueSubProcess.Text == "ACTIVE PART" || lueSubProcess.Text == "LS OK")
                {
                    lblRetPcs.Visible = true;
                    txtReturnPcs.Visible = true;

                    lblRetCarat.Visible = true;
                    txtReturnCarat.Visible = true;

                    lblLostPcs.Visible = false;
                    txtLostPcs.Visible = false;

                    lblLostCarat.Visible = false;
                    txtLostCarat.Visible = false;

                    lblRetPcs.Enabled = true;
                    txtReturnPcs.Enabled = true;

                    lblRetCarat.Enabled = true;
                    txtReturnCarat.Enabled = true;

                    lblLostPcs.Enabled = false;
                    txtLostPcs.Enabled = false;

                    lblLostCarat.Enabled = false;
                    txtLostCarat.Enabled = false;

                    lblOutPcs.Visible = true;
                    lblOutCarat.Visible = true;

                    txtOutpcs.Visible = true;
                    txtOutCarat.Visible = true;

                    lblOutPcs.Enabled = true;
                    lblOutCarat.Enabled = true;

                    txtOutpcs.Enabled = true;
                    txtOutCarat.Enabled = true;
                }

                if (lueSubProcess.Text == "MAPPING" || lueSubProcess.Text == "SAWABLE FARSI SARIN" || lueSubProcess.Text == "2P SARIN")
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

                    lblBreakragePcs.Visible = true;
                    lblBreakrageCarat.Visible = true;
                    txtBreakPcs.Visible = true;
                    txtBreakCarat.Visible = true;

                    txtLostPcs.Visible = false;
                    txtLostCarat.Visible = false;

                    txtLossCarat.Visible = false;
                    txtLossPcs.Visible = false;
                    lblLossPcs.Visible = false;
                    lblLossCarat.Visible = false;
                    lblFinalWeight.Visible = false;
                    txtCaratPlus.Visible = false;
                    lblCaratPlus.Visible = false;
                }
                else if (lueSubProcess.Text == "STICTHING" || lueSubProcess.Text == "4P OK" || lueSubProcess.Text == "ACTIVE PART" || lueSubProcess.Text == "LS OK")
                {
                    lblRetPcs.Visible = true;
                    txtReturnPcs.Visible = true;

                    lblRetCarat.Visible = true;
                    txtReturnCarat.Visible = true;

                    lblOutPcs.Visible = true;
                    lblOutCarat.Visible = true;

                    txtOutpcs.Visible = true;
                    txtOutCarat.Visible = true;

                    lblOutPcs.Enabled = true;
                    lblOutCarat.Enabled = true;

                    txtOutpcs.Enabled = true;
                    txtOutCarat.Enabled = true;

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

                    lblLostCarat.Enabled = false;
                    lblLostPcs.Enabled = false;
                    txtLostPcs.Enabled = false;
                    txtLostCarat.Enabled = false;

                    lblBreakragePcs.Visible = false;
                    lblBreakrageCarat.Visible = false;
                    txtBreakPcs.Visible = false;
                    txtBreakCarat.Visible = false;

                    txtLossCarat.Visible = false;
                    txtLossPcs.Visible = false;
                    lblLossPcs.Visible = false;
                    lblLossCarat.Visible = false;
                    lblFinalWeight.Visible = false;
                    txtCaratPlus.Visible = false;
                    lblCaratPlus.Visible = false;
                }
                else if (lueSubProcess.Text == "ROBOT OPERATOR")
                {
                    lblRetPcs.Visible = true;
                    txtReturnPcs.Visible = true;

                    lblRetCarat.Visible = true;
                    txtReturnCarat.Visible = true;

                    lblOutPcs.Visible = true;
                    lblOutCarat.Visible = true;

                    txtOutpcs.Visible = true;
                    txtOutCarat.Visible = true;

                    lblOutPcs.Enabled = false;
                    lblOutCarat.Enabled = false;

                    txtOutpcs.Enabled = false;
                    txtOutCarat.Enabled = false;

                    lblRejPcs.Visible = true;
                    txtRejPcs.Visible = true;

                    lblRejCarat.Visible = true;
                    txtRejCarat.Visible = true;

                    lblResoingPcs.Visible = true;
                    lblResoingCarat.Visible = true;

                    txtReSoingPcs.Visible = true;
                    txtResoingCarat.Visible = true;

                    lblLostCarat.Visible = true;
                    lblLostPcs.Visible = true;
                    txtLostPcs.Visible = true;
                    txtLostCarat.Visible = true;

                    lblLostCarat.Enabled = false;
                    lblLostPcs.Enabled = false;
                    txtLostPcs.Enabled = false;
                    txtLostCarat.Enabled = false;

                    lblBreakragePcs.Visible = true;
                    lblBreakrageCarat.Visible = true;
                    txtBreakPcs.Visible = true;
                    txtBreakCarat.Visible = true;

                    txtLossCarat.Visible = false;
                    txtLossPcs.Visible = false;
                    lblLossPcs.Visible = false;
                    lblLossCarat.Visible = false;
                    lblFinalWeight.Visible = false;
                    txtCaratPlus.Visible = false;
                    lblCaratPlus.Visible = false;
                }
                else if (lueSubProcess.Text == "MANUAL OPERATOR")
                {
                    lblRetPcs.Visible = true;
                    txtReturnPcs.Visible = true;

                    lblRetCarat.Visible = true;
                    txtReturnCarat.Visible = true;

                    lblOutPcs.Visible = true;
                    lblOutCarat.Visible = true;

                    txtOutpcs.Visible = true;
                    txtOutCarat.Visible = true;

                    lblOutPcs.Enabled = false;
                    lblOutCarat.Enabled = false;

                    txtOutpcs.Enabled = false;
                    txtOutCarat.Enabled = false;

                    lblRejPcs.Visible = true;
                    txtRejPcs.Visible = true;

                    lblRejCarat.Visible = true;
                    txtRejCarat.Visible = true;

                    lblResoingPcs.Visible = true;
                    lblResoingCarat.Visible = true;

                    txtReSoingPcs.Visible = true;
                    txtResoingCarat.Visible = true;

                    lblLostCarat.Visible = true;
                    lblLostPcs.Visible = true;
                    txtLostPcs.Visible = true;
                    txtLostCarat.Visible = true;

                    lblLostCarat.Enabled = false;
                    lblLostPcs.Enabled = false;
                    txtLostPcs.Enabled = false;
                    txtLostCarat.Enabled = false;

                    lblBreakragePcs.Visible = true;
                    lblBreakrageCarat.Visible = true;
                    txtBreakPcs.Visible = true;
                    txtBreakCarat.Visible = true;

                    txtLossCarat.Visible = false;
                    txtLossPcs.Visible = false;
                    lblLossPcs.Visible = false;
                    lblLossCarat.Visible = false;
                    lblFinalWeight.Visible = false;
                    txtCaratPlus.Visible = false;
                    lblCaratPlus.Visible = false;
                }
                if (lueSubProcess.Text == "4P-OK")
                {
                    lblRetPcs.Visible = true;
                    txtReturnPcs.Visible = true;

                    lblRetCarat.Visible = true;
                    txtReturnCarat.Visible = true;

                    lblOutPcs.Visible = true;
                    lblOutCarat.Visible = true;

                    txtOutpcs.Visible = true;
                    txtOutCarat.Visible = true;

                    lblOutPcs.Enabled = false;
                    lblOutCarat.Enabled = false;

                    txtOutpcs.Enabled = false;
                    txtOutCarat.Enabled = false;

                    lblRejPcs.Visible = true;
                    txtRejPcs.Visible = true;

                    lblRejCarat.Visible = true;
                    txtRejCarat.Visible = true;

                    //lblResoingPcs.Visible = false;
                    //lblResoingCarat.Visible = false;

                    //txtReSoingPcs.Visible = false;
                    //txtResoingCarat.Visible = false;

                    lblResoingPcs.Visible = true;
                    lblResoingCarat.Visible = true;

                    txtReSoingPcs.Visible = true;
                    txtResoingCarat.Visible = true;

                    lblLostCarat.Visible = true;
                    lblLostPcs.Visible = true;
                    lblLostCarat.Enabled = false;
                    lblLostPcs.Enabled = false;
                    txtLostPcs.Enabled = false;
                    txtLostCarat.Enabled = false;

                    lueEmployee.Enabled = true;

                    //lblBreakragePcs.Visible = false;
                    //lblBreakrageCarat.Visible = false;
                    //txtBreakPcs.Visible = false;
                    //txtBreakCarat.Visible = false;

                    lblBreakragePcs.Visible = true;
                    lblBreakrageCarat.Visible = true;
                    txtBreakPcs.Visible = true;
                    txtBreakCarat.Visible = true;

                    txtLostPcs.Visible = true;
                    txtLostCarat.Visible = true;

                    txtLossCarat.Visible = true;
                    //txtLossPcs.Visible = true;
                    //lblLossPcs.Visible = true;
                    lblLossCarat.Visible = true;
                    lblFinalWeight.Visible = true;
                    txtCaratPlus.Visible = false;
                    lblCaratPlus.Visible = false;
                }
                if (lueSubProcess.Text == "POLISH REPAIRING")
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

                    lblBreakragePcs.Visible = true;
                    lblBreakrageCarat.Visible = true;
                    txtBreakPcs.Visible = true;
                    txtBreakCarat.Visible = true;

                    txtLostPcs.Visible = false;
                    txtLostCarat.Visible = false;

                    txtLossCarat.Visible = true;
                    //txtLossPcs.Visible = true;
                    //lblLossPcs.Visible = true;
                    lblLossCarat.Visible = true;
                    lblFinalWeight.Visible = true;
                    txtCaratPlus.Visible = false;
                    lblCaratPlus.Visible = false;
                }
                if ((GlobalDec.gEmployeeProperty.role_name == "GALAXY DW" && lueSubProcess.Text == "4P OK") || (GlobalDec.gEmployeeProperty.role_name == "GALAXY DW" && lueSubProcess.Text == "ACTIVE PART"))
                {
                    lblLossCarat.Visible = true;
                    txtLossCarat.Visible = true;

                }
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (AddInGrid())
                {
                    txtManualPer.Text = "0";
                    txtManualCarat.Text = "0";

                    txtReturnPcs.Text = "0";
                    txtReturnCarat.Text = "0";

                    txtLossPcs.Text = "0";
                    txtLossCarat.Text = "0";

                    txtLostPcs.Text = "0";
                    txtLostCarat.Text = "0";

                    txtRejPcs.Text = "0";
                    txtRejCarat.Text = "0";

                    txtOutpcs.Text = "0";
                    txtOutCarat.Text = "0";

                    txtResoingCarat.Text = "0";
                    txtReSoingPcs.Text = "0";

                    txtBreakPcs.Text = "0";
                    txtBreakCarat.Text = "0";

                    lblWagesRate.Text = "0";
                    lblWagesAmt.Text = "0";

                    txtCaratPlus.Text = "0";

                    lblTotalPcs.Text = "0";
                    lblTotalCarat.Text = "0";
                    lblFinalWeight.Text = "0";
                    lblPer.Text = "0";
                    lblLotNo.Text = "0";

                    m_emp_id = 0;
                    m_BalCarat = 0;
                    txtLotID.Text = "";
                    lblOsPcs.Text = "";
                    lblOsCarat.Text = "";
                    lueEmployee.EditValue = null;

                    if (GlobalDec.gEmployeeProperty.role_name == "SURAT 4P OK" || GlobalDec.gEmployeeProperty.role_name == "SURAT KAMALA" || GlobalDec.gEmployeeProperty.role_name == "SURAT GALAXY")
                    {
                        txtLotID.Focus();
                    }
                    else if (GlobalDec.gEmployeeProperty.role_name == "SURAT POLISH REPARING" || GlobalDec.gEmployeeProperty.role_name == "SURAT MAKABLE")
                    {
                        txtLotID.Focus();
                    }
                    else if (GlobalDec.gEmployeeProperty.role_name == "SURAT 4P" || GlobalDec.gEmployeeProperty.role_name == "SURAT SARIN" || GlobalDec.gEmployeeProperty.role_name == "SURAT 4P ENTRY" || GlobalDec.gEmployeeProperty.role_name == "SURAT KATARGAM")
                    {
                        lueMachine.Focus();
                    }

                    //_NextEnteredControl = new Control();
                    //_tabControls = new List<Control>();

                    //ControlSettingDT(Val.ToInt(ObjPer.form_id), this);
                    //AddGotFocusListener(this);
                    //AddKeyPressListener(this);
                    //this.KeyPreview = true;

                    //TabControlsToList(this.Controls);
                    //_tabControls = _tabControls.OrderBy(x => x.TabIndex).ToList();
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
        private void lueMachine_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmMfgMachineMaster frmMachine = new FrmMfgMachineMaster();
                frmMachine.ShowDialog();
                Global.LOOKUPMachine(lueMachine);
            }
        }
        private void txtReturnPcs_Validated(object sender, EventArgs e)
        {
            if (Val.ToInt(txtReturnPcs.Text) != 0 && Val.ToDecimal(lblOsCarat.Text) != 0 && Val.ToInt(lblOsPcs.Text) != 0)
            {
                if (Val.ToString(lueSubProcess.Text) != "GALAXY LS" && Val.ToString(lueSubProcess.Text) != "LS" && Val.ToString(lueSubProcess.Text) != "4P OK" && Val.ToString(lueSubProcess.Text) != "ACTIVE PART" && Val.ToString(lueSubProcess.Text) != "LS OK" && Val.ToString(lueSubProcess.Text) != "GALAXY LS" && Val.ToString(lueSubProcess.Text) != "ACTIVE PART LS" && Val.ToString(lueSubProcess.Text) != "GALAXY 4P OK")
                {
                    txtReturnCarat.Text = Val.ToString(Math.Round(Val.ToDecimal(Val.ToInt(txtReturnPcs.Text) / (Val.ToInt(lblOsPcs.Text) / Val.ToDecimal(lblOsCarat.Text))), 3));
                }
                sumData();
            }
            else
            {
                txtReturnCarat.Text = "0";
                sumData();
            }
        }
        private void txtRRpcs_Validated(object sender, EventArgs e)
        {
            if (Val.ToInt(txtOutpcs.Text) != 0 && Val.ToDecimal(lblOsCarat.Text) != 0 && Val.ToInt(lblOsPcs.Text) != 0)
            {
                txtOutCarat.Text = Val.ToString(Math.Round(Val.ToDecimal(Val.ToInt(txtOutpcs.Text) / (Val.ToInt(lblOsPcs.Text) / Val.ToDecimal(lblOsCarat.Text))), 3));
                sumData();
            }
            else
            {
                txtOutCarat.Text = "0";
                sumData();
            }
        }
        private void txtRejPcs_Validated(object sender, EventArgs e)
        {
            if (Val.ToInt(txtRejPcs.Text) != 0 && Val.ToDecimal(lblOsCarat.Text) != 0 && Val.ToInt(lblOsPcs.Text) != 0)
            {
                txtRejCarat.Text = Val.ToString(Math.Round(Val.ToDecimal(Val.ToInt(txtRejPcs.Text) / (Val.ToInt(lblOsPcs.Text) / Val.ToDecimal(lblOsCarat.Text))), 3));
                sumData();
            }
            else
            {
                txtRejCarat.Text = "0";
                sumData();
            }
        }
        private void txtLossPcs_Validated(object sender, EventArgs e)
        {
            sumData();
            //if (Val.ToInt(txtLossPcs.Text) != 0 && Val.ToDecimal(lblOsCarat.Text) != 0 && Val.ToInt(lblOsPcs.Text) != 0)
            //{
            //    txtLossCarat.Text = Val.ToString(Math.Round(Val.ToDecimal(Val.ToInt(txtLossPcs.Text) / (Val.ToInt(lblOsPcs.Text) / Val.ToDecimal(lblOsCarat.Text))), 3));
            //}
            //else
            //{
            //    txtLossCarat.Text = "0";
            //}
        }
        private void txtLostPcs_Validated(object sender, EventArgs e)
        {
            if (Val.ToInt(txtLostPcs.Text) != 0 && Val.ToDecimal(lblOsCarat.Text) != 0 && Val.ToInt(lblOsPcs.Text) != 0)
            {
                if (Val.ToString(lueSubProcess.Text) == "4P-OK")
                {
                    txtReturnPcs.Text = Val.ToString(Val.ToInt(lblOsPcs.Text) - Val.ToInt(txtOutpcs.Text) - Val.ToInt(txtBreakPcs.Text) - Val.ToInt(txtLostPcs.Text));
                }
                else
                {
                    txtLostCarat.Text = Val.ToString(Math.Round(Val.ToDecimal(Val.ToInt(txtLostPcs.Text) / (Val.ToInt(lblOsPcs.Text) / Val.ToDecimal(lblOsCarat.Text))), 3));
                }
                sumData();
            }
            else
            {
                if (Val.ToString(lueSubProcess.Text) == "4P-OK")
                {
                    txtReturnPcs.Text = Val.ToString(Val.ToInt(lblOsPcs.Text) - Val.ToInt(txtOutpcs.Text) - Val.ToInt(txtBreakPcs.Text) - Val.ToInt(txtLostPcs.Text));
                }
                txtLostCarat.Text = "0";
                sumData();
            }
        }
        private void txtLossCarat_Validated(object sender, EventArgs e)
        {
            sumData();
        }
        private void txtCaratPlus_Validated(object sender, EventArgs e)
        {

            sumData();
        }
        private void txtReturnCarat_Validated(object sender, EventArgs e)
        {
            txtLossCarat.Text = Val.ToString(Val.ToDecimal(lblTotalCarat.Text) - Val.ToDecimal(txtReturnCarat.Text) - Val.ToDecimal(txtOutCarat.Text));
            sumData();
        }
        private void txtOutCarat_Validated(object sender, EventArgs e)
        {
            sumData();
        }
        private void txtRejCarat_Validated(object sender, EventArgs e)
        {
            sumData();
        }

        private void txtResoingCarat_Validated(object sender, EventArgs e)
        {
            sumData();
        }
        private void txtBreakCarat_Validated(object sender, EventArgs e)
        {
            sumData();
        }
        private void txtLostCarat_Validated(object sender, EventArgs e)
        {
            sumData();
        }
        private void txtReturnPcs_EditValueChanged(object sender, EventArgs e)
        {

            if (txtReturnPcs.Text != "")
            {
                lblWagesAmt.Text = Val.ToString(Math.Round(Val.ToDecimal(lblWagesRate.Text) * Val.ToDecimal(txtReturnPcs.Text), 2));
                sumData();
                if (lueSubProcess.Text == "MARKING")
                {
                    decimal perPieceCarat = Math.Round(Val.ToDecimal(lblOsCarat.Text) / Val.ToDecimal(lblOsPcs.Text), 3);
                    txtReturnCarat.Text = Val.ToString(Math.Round(Val.ToDecimal(txtReturnPcs.Text), 3) * perPieceCarat);
                    txtOutpcs.Text = Val.ToString(Val.ToInt(lblOsPcs.Text) - Val.ToInt(txtReturnPcs.Text));
                    txtOutCarat.Text = Val.ToString(Val.ToInt(txtOutpcs.Text) * perPieceCarat);
                }
            }
            else
            {
                lblWagesAmt.Text = "0";
                sumData();
            }
            if (txtReturnPcs.Text != "" && GlobalDec.gEmployeeProperty.role_name == "SURAT POLISH REPARING")
            {
                txtBreakPcs.Text = Val.ToString(Val.ToInt32(lblOsPcs.Text) - Val.ToInt32(txtReturnPcs.Text));
            }
        }
        private void txtReSoingPcs_Validated(object sender, EventArgs e)
        {
            if (Val.ToInt(txtReSoingPcs.Text) != 0 && Val.ToDecimal(lblOsCarat.Text) != 0 && Val.ToInt(lblOsPcs.Text) != 0)
            {
                txtResoingCarat.Text = Val.ToString(Math.Round(Val.ToDecimal(Val.ToInt(txtReSoingPcs.Text) / (Val.ToInt(lblOsPcs.Text) / Val.ToDecimal(lblOsCarat.Text))), 3));
                sumData();
            }
            else
            {
                txtResoingCarat.Text = "0";
                sumData();
            }
        }
        private void txtBreakPcs_Validated(object sender, EventArgs e)
        {

            decimal Break_Carat = 0;
            decimal Return_Carat = 0;
            if (Val.ToInt(txtBreakPcs.Text) != 0 && Val.ToDecimal(lblOsCarat.Text) != 0 && Val.ToInt(lblOsPcs.Text) != 0)
            {
                //if (GlobalDec.gEmployeeProperty.role_name == "SURAT RUSSIAN")
                //{
                //    txtBreakCarat.Text = Val.ToString(Math.Round(Val.ToDecimal(Val.ToInt(txtBreakPcs.Text) / (Val.ToInt(lblOsPcs.Text) / Val.ToDecimal(lblOsCarat.Text))), 3));
                //}
                if (GlobalDec.gEmployeeProperty.role_name == "SURAT POLISH REPARING" || GlobalDec.gEmployeeProperty.role_name == "SURAT RUSSIAN")
                {
                    txtBreakCarat.Text = Val.ToString(0);
                }
                else
                {
                    Break_Carat = Val.ToDecimal(Math.Round(Val.ToDecimal(Val.ToInt(txtBreakPcs.Text) / (Val.ToInt(lblOsPcs.Text) / Val.ToDecimal(lblOsCarat.Text))), 3));
                    Return_Carat = Val.ToDecimal(Math.Round(Val.ToDecimal(Val.ToInt(txtReturnPcs.Text) / (Val.ToInt(lblOsPcs.Text) / Val.ToDecimal(lblOsCarat.Text))), 3));
                    txtReturnCarat.Text = Val.ToString(Break_Carat + Return_Carat);
                }
                //txtBreakCarat.Text = Val.ToString(Math.Round(Val.ToDecimal(Val.ToInt(txtBreakPcs.Text) / (Val.ToInt(lblOsPcs.Text) / Val.ToDecimal(lblOsCarat.Text))), 3));

                sumData();
            }
            else
            {
                txtBreakCarat.Text = "0";
                sumData();
            }
        }
        private void txtLotID_Validated(object sender, EventArgs e)
        {
            //if (txtLotID.Text != "")
            //{
            //    MFGCutCreateProperty oProperty = new MFGCutCreateProperty();
            //    oProperty.lock_date = Val.DBDate(dtpReceiveDate.Text);
            //    oProperty.lock_type_id = (int)GlobalDec.LOCKTYPE.LOT_ID;
            //    oProperty.lot_id = Val.ToInt64(txtLotID.Text);
            //    DataTable DTab = objCutCreate.LockGetData(oProperty);
            //    if (DTab.Rows.Count > 0)
            //    {
            //        GblLockBarcodeMsg = GblLockBarcodeMsg + "[" + DTab.Rows[0]["lot_id"] + "]" + " Is Already Lock By \n" + "User : " + DTab.Rows[0]["user_name"] + "\n" + "IP : " + DTab.Rows[0]["ip_address"] + "\n";
            //        GblLockBarcode = GblLockBarcode + DTab.Rows[0]["lot_id"] + ",";
            //        Global.ErrorMessage("There is issue with Locking Lot ID! " + DTab.Rows[0]["user_name"] + ", " + DTab.Rows[0]["ip_address"] + " Please contact Administrator!");
            //        return;
            //    }
            //    else
            //    {
            //        int IntReso = objCutCreate.LockSave(oProperty);
            //    }
            //}
            MFGProcessReceive objProcessRecieve = new MFGProcessReceive();

            if (Val.ToInt64(txtLotID.Text) != 0)
            {
                int LotNo = 0;
                m_dtbParam = Global.GetRoughStockWise(Val.ToInt(0), Val.ToInt64(txtLotID.Text));
                if (m_dtbParam.Rows.Count > 0)
                {
                    int Cut_id = Val.ToInt(m_dtbParam.Rows[0]["rough_cut_id"]);
                    lueKapan.EditValue = Val.ToInt64(m_dtbParam.Rows[0]["kapan_id"]);
                    lueCutNo.EditValue = Val.ToInt64(Cut_id);
                    manager_short_name = Val.ToString(m_dtbParam.Rows[0]["manager_short_name"]);
                    LotNo = Val.ToInt(m_dtbParam.Rows[0]["lot_no"]);
                    lblLotNo.Text = Val.ToString(LotNo);
                    m_OrgPcs = Val.ToInt32(m_dtbParam.Rows[0]["org_pcs"]);
                    m_OrgCarat = Val.ToDecimal(m_dtbParam.Rows[0]["org_carat"]);
                    DataTable DTab_Process = new DataTable();
                    if (GlobalDec.gEmployeeProperty.department_id == 2021)
                    {
                        DTab_Process = objProcessRecieve.Carat_Chapka_OutStanding_GetData(Val.ToInt64(txtLotID.Text), Val.ToInt32(0), Val.ToInt32(0), 0, "R");
                    }
                    else
                    {
                        DTab_Process = objProcessRecieve.Carat_Sarin_OutStanding_GetData(Val.ToInt64(txtLotID.Text), Val.ToInt32(0), Val.ToInt32(0), 0, "R");
                    }

                    if (DTab_Process.Rows.Count == 1)
                    {
                        lueProcess.EditValue = Val.ToInt32(DTab_Process.Rows[0]["process_id"]);
                        lueSubProcess.EditValue = Val.ToInt32(DTab_Process.Rows[0]["sub_process_id"]);

                        lueSubProcess_EditValueChanged(null, null);

                        sumData();

                        MFGProcessIssue objMFGProcessIssue = new MFGProcessIssue();
                        DataTable dtWagesRate = new DataTable();

                        dtWagesRate = objMFGProcessIssue.GetWagesRate(Val.ToInt64(txtLotID.Text), GlobalDec.gEmployeeProperty.department_name, Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue));

                        if (dtWagesRate.Rows.Count > 0)
                        {
                            lblWagesRate.Text = Val.ToString(Val.ToDecimal(dtWagesRate.Rows[0]["rate"]));
                        }
                    }
                }
                else
                {
                    BLL.General.ShowErrors("Lot ID Not in Stock");
                    lueCutNo.EditValue = System.DBNull.Value;
                }

                //if (Val.ToString(lueSubProcess.Text) == "4P-OK")
                //{
                //    txtLotID.Focus();
                //}
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
        private void lueManager_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmEmployeeMaster frmManager = new FrmEmployeeMaster();
                frmManager.ShowDialog();
                Global.LOOKUPManager(lueManager);
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
        private void backgroundWorker_ProcRecWeightLoss_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                MultiEmployeeReturn objMFGMultiEmp = new MultiEmployeeReturn();
                MFGProcessWeightLossRecieve_Property objMFGProcessIssueProperty = new MFGProcessWeightLossRecieve_Property();
                //foreach (DataRow drwDate in m_dtbReceiveProcess.Rows)
                //{
                //    int DateCheck = Global.ValidateDate(Val.ToInt(drwDate["lot_id"]), Val.ToString(dtpReceiveDate.Text));
                //    if (DateCheck == 0)
                //    {
                //        Global.Message("Plz Check Recieve Date is less than Janged Date " + Val.ToInt(drwDate["lot_id"]));
                //        IntRes = -1;
                //        btnSave.Enabled = true;
                //        return;
                //    }
                //}
                Conn = new BeginTranConnection(true, false);

                IntRes = 0;
                Int64 NewHistory_Union_Id = 0;
                Int64 New_Union_Id = 0;
                Int64 Lot_SrNo = 0;

                int IntCounter = 0;
                int Count = 0;

                DataTable dt = new DataTable();
                DataTable newDt = new DataTable();
                if (Val.ToInt32(m_isMainLot) == 1)
                {
                    dt.Columns.Add("lot_id", typeof(Int64));
                    dt.Columns.Add("return_pcs", typeof(int));
                    dt.Columns.Add("return_carat", typeof(decimal));
                    dt.Columns.Add("rr_pcs", typeof(int));
                    dt.Columns.Add("rr_carat", typeof(decimal));
                    dt.Columns.Add("resoing_pcs", typeof(int));
                    dt.Columns.Add("resoing_carat", typeof(decimal));
                    dt.Columns.Add("breakage_pcs", typeof(int));
                    dt.Columns.Add("breakage_carat", typeof(decimal));
                    dt.Columns.Add("rejection_pcs", typeof(int));
                    dt.Columns.Add("rejection_carat", typeof(decimal));
                    dt.Columns.Add("loss_pcs", typeof(int));
                    dt.Columns.Add("loss_carat", typeof(decimal));
                    dt.Columns.Add("lost_pcs", typeof(int));
                    dt.Columns.Add("lost_carat", typeof(decimal));
                    dt.Columns.Add("final_weight", typeof(decimal));
                    dt.Columns.Add("carat_plus", typeof(decimal));
                    var newDt1 = m_dtbReceiveProcess.AsEnumerable()
                  .GroupBy(r => r.Field<Int64>("lot_id"))
                  .Select(g =>
                  {
                      var row = dt.NewRow();

                      row["lot_id"] = g.Key;
                      row["return_pcs"] = g.Sum(r => r.Field<int>("return_pcs"));
                      row["return_carat"] = g.Sum(r => r.Field<decimal>("return_carat"));
                      row["rr_pcs"] = g.Sum(r => r.Field<int>("rr_pcs"));
                      row["rr_carat"] = g.Sum(r => r.Field<decimal>("rr_carat"));
                      row["resoing_pcs"] = g.Sum(r => r.Field<int>("resoing_pcs"));
                      row["resoing_carat"] = g.Sum(r => r.Field<decimal>("resoing_carat"));
                      row["breakage_pcs"] = g.Sum(r => r.Field<int>("breakage_pcs"));
                      row["breakage_carat"] = g.Sum(r => r.Field<decimal>("breakage_carat"));
                      row["rejection_pcs"] = g.Sum(r => r.Field<int>("rejection_pcs"));
                      row["rejection_carat"] = g.Sum(r => r.Field<decimal>("rejection_carat"));
                      row["loss_pcs"] = g.Sum(r => r.Field<int>("loss_pcs"));
                      row["loss_carat"] = g.Sum(r => r.Field<decimal>("loss_carat"));
                      row["lost_pcs"] = g.Sum(r => r.Field<int>("lost_pcs"));
                      row["lost_carat"] = g.Sum(r => r.Field<decimal>("lost_carat"));
                      row["final_weight"] = g.Sum(r => r.Field<decimal>("final_weight"));
                      row["carat_plus"] = g.Sum(r => r.Field<decimal>("carat_plus"));

                      return row;
                  }).CopyToDataTable();
                    newDt = newDt1.Copy();
                }
                else
                {
                    dt.Columns.Add("lot_id", typeof(int));
                    dt.Columns.Add("return_pcs", typeof(int));
                    dt.Columns.Add("return_carat", typeof(decimal));
                    dt.Columns.Add("rr_pcs", typeof(int));
                    dt.Columns.Add("rr_carat", typeof(decimal));
                    dt.Columns.Add("resoing_pcs", typeof(int));
                    dt.Columns.Add("resoing_carat", typeof(decimal));
                    dt.Columns.Add("breakage_pcs", typeof(int));
                    dt.Columns.Add("breakage_carat", typeof(decimal));
                    dt.Columns.Add("rejection_pcs", typeof(int));
                    dt.Columns.Add("rejection_carat", typeof(decimal));
                    dt.Columns.Add("loss_pcs", typeof(int));
                    dt.Columns.Add("loss_carat", typeof(decimal));
                    dt.Columns.Add("lost_pcs", typeof(int));
                    dt.Columns.Add("lost_carat", typeof(decimal));
                    dt.Columns.Add("final_weight", typeof(decimal));
                    dt.Columns.Add("carat_plus", typeof(decimal));
                    var newDt1 = m_dtbReceiveProcess.AsEnumerable()
                  .GroupBy(r => r.Field<int>("lot_id"))
                  .Select(g =>
                  {
                      var row = dt.NewRow();

                      row["lot_id"] = g.Key;
                      row["return_pcs"] = g.Sum(r => r.Field<int>("return_pcs"));
                      row["return_carat"] = g.Sum(r => r.Field<decimal>("return_carat"));
                      row["rr_pcs"] = g.Sum(r => r.Field<int>("rr_pcs"));
                      row["rr_carat"] = g.Sum(r => r.Field<decimal>("rr_carat"));
                      row["resoing_pcs"] = g.Sum(r => r.Field<int>("resoing_pcs"));
                      row["resoing_carat"] = g.Sum(r => r.Field<decimal>("resoing_carat"));
                      row["breakage_pcs"] = g.Sum(r => r.Field<int>("breakage_pcs"));
                      row["breakage_carat"] = g.Sum(r => r.Field<decimal>("breakage_carat"));
                      row["rejection_pcs"] = g.Sum(r => r.Field<int>("rejection_pcs"));
                      row["rejection_carat"] = g.Sum(r => r.Field<decimal>("rejection_carat"));
                      row["loss_pcs"] = g.Sum(r => r.Field<int>("loss_pcs"));
                      row["loss_carat"] = g.Sum(r => r.Field<decimal>("loss_carat"));
                      row["lost_pcs"] = g.Sum(r => r.Field<int>("lost_pcs"));
                      row["lost_carat"] = g.Sum(r => r.Field<decimal>("lost_carat"));
                      row["final_weight"] = g.Sum(r => r.Field<decimal>("final_weight"));
                      row["carat_plus"] = g.Sum(r => r.Field<decimal>("carat_plus"));

                      return row;
                  }).CopyToDataTable();
                    newDt = newDt1.Copy();
                }
                //foreach(DataRow drRow in newDt.Rows)
                //{
                //    dt.Rows.Add(drRow);
                //}

                //object sum = m_dtbReceiveProcess.Compute("sum(return_pcs)", "lot_id");
                try
                {
                    int TotalCount = m_dtbReceiveProcess.Rows.Count;

                    foreach (DataRow drw in m_dtbReceiveProcess.Rows)
                    {

                        DataTable dtIss = objProcessRecieve.GetIssueID(Val.ToInt64(drw["lot_id"]), Val.ToInt32(drw["process_id"]), Val.ToInt32(drw["sub_process_id"]));
                        if (dtIss.Rows.Count > 0)
                        {
                            m_issue_id = Val.ToInt64(dtIss.Rows[0]["issue_id"]);
                            objMFGProcessIssueProperty.issue_id = Val.ToInt64(m_issue_id);
                        }
                        else
                        {
                            objMFGProcessIssueProperty.issue_id = Val.ToInt(drw["issue_id"]);
                        }

                        objMFGProcessIssueProperty.lot_id = Val.ToInt64(drw["lot_id"]);
                        //objMFGProcessIssueProperty.receive_date = Val.DBDate(Val.ToString(drw["recieve_date"]));
                        objMFGProcessIssueProperty.receive_date = Val.DBDate(Val.ToString(dtpReceiveDate.EditValue));
                        //objMFGProcessIssueProperty.issue_date = Val.DBDate(dtpIssueDate.Text);
                        objMFGProcessIssueProperty.manager_id = Val.ToInt(drw["manager_id"]);
                        objMFGProcessIssueProperty.employee_id = Val.ToInt(drw["employee_id"]);

                        objMFGProcessIssueProperty.process_id = Val.ToInt(drw["process_id"]); //Val.ToInt(drw["process_id"]);
                        objMFGProcessIssueProperty.sub_process_id = Val.ToInt(drw["sub_process_id"]); //Val.ToInt(drw["sub_process_id"]);
                        objMFGProcessIssueProperty.machine_id = Val.ToInt(drw["machine_id"]);//Val.ToInt(lueMachine.EditValue);
                        objMFGProcessIssueProperty.pcs = Val.ToInt(drw["return_pcs"]);
                        objMFGProcessIssueProperty.carat = Val.ToDecimal(drw["return_carat"]);
                        objMFGProcessIssueProperty.rr_pcs = Val.ToInt(drw["rr_pcs"]);
                        objMFGProcessIssueProperty.rr_carat = Val.ToDecimal(drw["rr_carat"]);
                        objMFGProcessIssueProperty.loss_pcs = Val.ToInt(drw["loss_pcs"]);
                        objMFGProcessIssueProperty.loss_carat = Val.ToDecimal(drw["loss_carat"]);
                        objMFGProcessIssueProperty.lost_pcs = Val.ToInt(drw["lost_pcs"]);
                        objMFGProcessIssueProperty.lost_carat = Val.ToDecimal(drw["lost_carat"]);
                        objMFGProcessIssueProperty.rejection_pcs = Val.ToInt(drw["rejection_pcs"]);
                        objMFGProcessIssueProperty.rejection_carat = Val.ToDecimal(drw["rejection_carat"]);

                        objMFGProcessIssueProperty.carat_plus = Val.ToDecimal(drw["carat_plus"]);
                        objMFGProcessIssueProperty.final_weight = Val.ToDecimal(drw["final_weight"]);

                        objMFGProcessIssueProperty.resoing_pcs = Val.ToInt(drw["resoing_pcs"]);
                        objMFGProcessIssueProperty.resoing_carat = Val.ToDecimal(drw["resoing_carat"]);

                        objMFGProcessIssueProperty.braking_pcs = Val.ToInt(drw["breakage_pcs"]);
                        objMFGProcessIssueProperty.braking_carat = Val.ToDecimal(drw["breakage_carat"]);
                        objMFGProcessIssueProperty.total_flag = 0;
                        if (Val.ToDecimal(drw["balance"]) == 0)
                        {
                            objMFGProcessIssueProperty.is_outstanding = 1;
                        }
                        else
                        {
                            objMFGProcessIssueProperty.is_outstanding = 0;
                        }
                        objMFGProcessIssueProperty.form_id = m_numForm_id;
                        objMFGProcessIssueProperty.rough_cut_id = Val.ToInt(drw["cut_id"]);
                        objMFGProcessIssueProperty.kapan_id = Val.ToInt64(drw["kapan_id"]); // m_kapan_id;
                        objMFGProcessIssueProperty.receive_union_id = IntRes;
                        objMFGProcessIssueProperty.history_union_id = NewHistory_Union_Id;
                        objMFGProcessIssueProperty.union_id = New_Union_Id;
                        objMFGProcessIssueProperty.lot_srno = Lot_SrNo;

                        objMFGProcessIssueProperty.boiling_lot_id = Val.ToInt64(txtBoilingLotID.Text);

                        objMFGProcessIssueProperty = objMFGMultiEmp.Save(objMFGProcessIssueProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                        IntRes = objMFGProcessIssueProperty.receive_union_id;
                        NewHistory_Union_Id = Val.ToInt64(objMFGProcessIssueProperty.history_union_id);
                        New_Union_Id = Val.ToInt64(objMFGProcessIssueProperty.union_id);
                        Lot_SrNo = Val.ToInt64(objMFGProcessIssueProperty.lot_srno);

                        Count++;
                        IntCounter++;
                        SetControlPropertyValue(lblProgressCount, "Text", Count.ToString() + "" + "/" + "" + TotalCount.ToString() + " Completed....");

                    }
                    if (newDt.Rows.Count > 0 && IntRes > 0)
                    {
                        foreach (DataRow totaldrw in newDt.Rows)
                        {
                            objMFGProcessIssueProperty.lot_id = Val.ToInt(totaldrw["lot_id"]);
                            objMFGProcessIssueProperty.pcs = Val.ToInt(totaldrw["return_pcs"]);
                            objMFGProcessIssueProperty.carat = Val.ToDecimal(totaldrw["return_carat"]);
                            objMFGProcessIssueProperty.rr_pcs = Val.ToInt(totaldrw["rr_pcs"]);
                            objMFGProcessIssueProperty.rr_carat = Val.ToDecimal(totaldrw["rr_carat"]);
                            objMFGProcessIssueProperty.rejection_pcs = Val.ToInt(totaldrw["rejection_pcs"]);
                            objMFGProcessIssueProperty.rejection_carat = Val.ToDecimal(totaldrw["rejection_carat"]);
                            objMFGProcessIssueProperty.resoing_pcs = Val.ToInt(totaldrw["resoing_pcs"]);
                            objMFGProcessIssueProperty.resoing_carat = Val.ToDecimal(totaldrw["resoing_carat"]);
                            objMFGProcessIssueProperty.braking_pcs = Val.ToInt(totaldrw["breakage_pcs"]);
                            objMFGProcessIssueProperty.braking_carat = Val.ToDecimal(totaldrw["breakage_carat"]);
                            objMFGProcessIssueProperty.carat_plus = Val.ToDecimal(totaldrw["carat_plus"]);
                            objMFGProcessIssueProperty.final_weight = Val.ToDecimal(totaldrw["final_weight"]);
                            objMFGProcessIssueProperty.loss_pcs = Val.ToInt(totaldrw["loss_pcs"]);
                            objMFGProcessIssueProperty.loss_carat = Val.ToDecimal(totaldrw["loss_carat"]);
                            objMFGProcessIssueProperty.lost_pcs = Val.ToInt(totaldrw["lost_pcs"]);
                            objMFGProcessIssueProperty.lost_carat = Val.ToDecimal(totaldrw["lost_carat"]);
                            objMFGProcessIssueProperty.total_flag = Val.ToInt(1);
                            objMFGProcessIssueProperty.lot_srno = Lot_SrNo;
                            objMFGProcessIssueProperty.boiling_lot_id = Val.ToInt64(txtBoilingLotID.Text);

                            objMFGProcessIssueProperty = objMFGMultiEmp.Save(objMFGProcessIssueProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
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
                    grdMultiEmployeeReturn.DataSource = null;
                    ClearDetails();
                }
                else
                {
                    Global.Confirm("Error In Process Recieve");
                    Main_Lot_ID = string.Empty;
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
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
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
                    byteEnd = serialPort2.NewLine.ToCharArray();
                    Bytenumber = serialPort2.BytesToRead;
                    readBuffer = serialPort2.ReadLine();
                    Invoke(new EventHandler(DoUpdate));
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
                serialPort2.PortName = "COM4";
                serialPort2.BaudRate = 9600;
                serialPort2.Parity = Parity.None;
                serialPort2.DataBits = 8;
                serialPort2.StopBits = StopBits.Two;
                serialPort2.Handshake = Handshake.None;
                serialPort2.RtsEnable = false;
                serialPort2.ReceivedBytesThreshold = 1;
                serialPort2.NewLine = Environment.NewLine;
                serialPort2.ReadTimeout = 10000;
                try
                {
                    serialPort2.Open();
                    comOpen = serialPort2.IsOpen;
                }
                catch (Exception ex)
                {
                    comOpen = false;
                    Global.Message(ex.ToString());
                }

                if (comOpen)
                {
                    serialPort2.WriteLine(txtReturnCarat.Text);
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
                    serialPort2.DiscardInBuffer();
                    serialPort2.Close();
                }
                comOpen = false;
                TxtPort.BackColor = Color.Red;
                EnDis(O: true, C: false);
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
            }
        }

        #region GridEvents
        private void dgvProcessWeightLossRecieve_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvMultiEmployeeReturn.GetDataRow(e.RowHandle);
                        btnAdd.Text = "&Update";
                        lueCutNo.Text = Val.ToString(Drow["cut_no"]);
                        txtLotID.Text = Val.ToString(Drow["lot_id"]);
                        txtReturnPcs.Text = Val.ToString(Drow["return_pcs"]);
                        txtReturnCarat.Text = Val.ToString(Drow["return_carat"]);
                        txtOutpcs.Text = Val.ToString(Drow["rr_pcs"]);
                        txtOutCarat.Text = Val.ToString(Drow["rr_carat"]);
                        txtRejPcs.Text = Val.ToString(Drow["rejection_pcs"]);
                        txtRejCarat.Text = Val.ToString(Drow["rejection_carat"]);
                        txtReSoingPcs.Text = Val.ToString(Drow["resoing_pcs"]);
                        txtResoingCarat.Text = Val.ToString(Drow["resoing_carat"]);
                        txtBreakPcs.Text = Val.ToString(Drow["breakage_pcs"]);
                        txtBreakCarat.Text = Val.ToString(Drow["breakage_carat"]);
                        //txtLossPcs.Text = Val.ToString(Drow["loss_pcs"]);
                        txtLossCarat.Text = Val.ToString(Drow["loss_carat"]);
                        txtLostPcs.Text = Val.ToString(Drow["lost_pcs"]);
                        txtLostCarat.Text = Val.ToString(Drow["lost_carat"]);
                        txtCaratPlus.Text = Val.ToString(Drow["carat_plus"]);
                        lueProcess.EditValue = Val.ToInt32(Drow["process_id"]);
                        lueSubProcess.EditValue = Val.ToInt32(Drow["sub_process_id"]);
                        lueEmployee.EditValue = Val.ToInt64(Drow["employee_id"]);

                        m_issue_id = Val.ToInt64(Drow["issue_id"]);
                        m_numlosscarat = Val.ToDecimal(Drow["loss_carat"]);
                        m_numlostcarat = Val.ToDecimal(Drow["lost_carat"]);
                        m_cut_no = Val.ToString(Drow["cut_no"]);
                        m_update_srno = Val.ToInt(Drow["sr_no"]);
                        m_old_RetPcs = Val.ToInt(Drow["return_pcs"]);
                        m_old_RetCarat = Val.ToDecimal(Drow["return_carat"]);
                        m_old_OutPcs = Val.ToInt(Drow["rr_pcs"]);
                        m_old_OutCarat = Val.ToDecimal(Drow["rr_carat"]);
                        m_old_RejPcs = Val.ToInt(Drow["rejection_pcs"]);
                        m_old_RejCarat = Val.ToDecimal(Drow["rejection_carat"]);
                        m_old_ResawPcs = Val.ToInt(Drow["resoing_pcs"]);
                        m_old_ResawCarat = Val.ToDecimal(Drow["resoing_carat"]);
                        m_old_BrkPcs = Val.ToInt(Drow["breakage_pcs"]);
                        m_old_BrkCarat = Val.ToDecimal(Drow["breakage_carat"]);
                        m_old_LossPcs = Val.ToInt(Drow["loss_pcs"]);
                        m_old_LostPcs = Val.ToInt(Drow["lost_pcs"]);
                        m_old_losscarat = Val.ToDecimal(Drow["loss_carat"]);
                        m_old_lostcarat = Val.ToDecimal(Drow["lost_carat"]);
                        m_manager_id = Val.ToInt(Drow["manager_id"]);
                        m_emp_id = Val.ToInt(Drow["employee_id"]);
                        DataTable DTab_Process = objProcessRecieve.Carat_Sarin_OutStanding_GetData(Val.ToInt64(txtLotID.Text), Val.ToInt32(0), Val.ToInt32(0), 0, "R");
                        lblOsPcs.Text = Val.ToString(DTab_Process.Rows[0]["outstanding_pcs"]);
                        lblOsCarat.Text = Val.ToString(DTab_Process.Rows[0]["outstanding_carat"]);
                        sumData();
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
                        drwNew["cut_no"] = Val.ToString(Drow["rough_cut_no"]);
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
                        grdMultiEmployeeReturn.DataSource = m_dtbReceiveProcess;

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
                    //DataRow[] dr = m_dtbReceiveProcess.Select("employee = '" + Val.ToString(lueEmployee.Text) + "'");
                    DataRow[] dr = m_dtbReceiveProcess.Select("lot_id = '" + Val.ToString(txtLotID.Text) + "' and employee_id = '" + Val.ToInt32(lueEmployee.EditValue) + "' and machine_id = '" + Val.ToInt32(lueMachine.EditValue) + "'");

                    if (dr.Count() == 1)
                    {
                        Global.Message("Record already exists in grid", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lueEmployee.Focus();
                        blnReturn = false;
                        return blnReturn;
                    }

                    DataRow drwNew = m_dtbReceiveProcess.NewRow();
                    //int numLossPcs = Val.ToInt(txtLossPcs.Text);
                    decimal numLossCarat = Val.ToDecimal(txtLossCarat.Text);
                    int numLostPcs = Val.ToInt(txtLostPcs.Text);
                    decimal numLostCarat = Val.ToDecimal(txtLostCarat.Text);
                    int numReturnPcs = Val.ToInt(txtReturnPcs.Text);
                    decimal numReturnCarat = Val.ToDecimal(txtReturnCarat.Text);
                    int numRejPcs = Val.ToInt(txtRejPcs.Text);
                    decimal numRejCarat = Val.ToDecimal(txtRejCarat.Text);
                    m_BalPcs = (m_OsPcs - (numLostPcs + numReturnPcs));
                    m_BalCarat = (m_OsCarat - (numLossCarat + numLostCarat + numReturnCarat));
                    int numRRPcs = Val.ToInt(txtOutpcs.Text);
                    decimal numRRCarat = Val.ToDecimal(txtOutCarat.Text);

                    int numResoingPcs = Val.ToInt(txtReSoingPcs.Text);
                    decimal numResoingCarat = Val.ToDecimal(txtResoingCarat.Text);

                    int numBrackPcs = Val.ToInt(txtBreakPcs.Text);
                    decimal numBrackCarat = Val.ToDecimal(txtBreakCarat.Text);

                    int numorgPcs = Val.ToInt(lblOsPcs.Text);
                    decimal numorgCarat = Val.ToDecimal(lblOsCarat.Text);

                    decimal numCaratPlus = Val.ToDecimal(txtCaratPlus.Text);

                    //drwNew["cut_no"] = Val.ToString(lueCutNo.Text);

                    drwNew["recieve_id"] = Val.ToInt(0);
                    drwNew["recieve_date"] = Val.DBDate(dtpReceiveDate.Text);

                    drwNew["kapan_id"] = Val.ToString(lueKapan.EditValue);
                    drwNew["cut_id"] = Val.ToString(lueCutNo.EditValue);
                    drwNew["cut_no"] = Val.ToString(lueCutNo.Text);
                    drwNew["lot_id"] = Val.ToInt64(txtLotID.Text);

                    drwNew["manager"] = Val.ToString(manager_short_name); //Val.ToString(lueManager.Text);
                    drwNew["manager_id"] = Val.ToInt(m_manager_id);

                    drwNew["employee_id"] = Val.ToInt(lueEmployee.EditValue);
                    drwNew["employee"] = Val.ToString(lueEmployee.Text);

                    drwNew["process_id"] = Val.ToInt(lueProcess.EditValue);
                    drwNew["process"] = Val.ToString(lueProcess.Text);

                    drwNew["sub_process_id"] = Val.ToInt(lueSubProcess.EditValue);
                    drwNew["subprocess"] = Val.ToString(lueSubProcess.Text);

                    drwNew["machine_id"] = Val.ToInt(lueMachine.EditValue);
                    drwNew["machine"] = Val.ToString(lueMachine.Text);
                    drwNew["lot_no"] = Val.ToString(lblLotNo.Text);
                    drwNew["issue_id"] = Val.ToInt64(m_issue_id);
                    drwNew["return_pcs"] = numReturnPcs;
                    drwNew["return_carat"] = numReturnCarat;

                    //drwNew["loss_pcs"] = numLossPcs;
                    drwNew["loss_carat"] = numLossCarat;

                    drwNew["lost_pcs"] = numLostPcs;
                    drwNew["lost_carat"] = numLostCarat;

                    drwNew["rejection_pcs"] = numRejPcs;
                    drwNew["rejection_carat"] = numRejCarat;

                    drwNew["rr_pcs"] = numRRPcs;
                    drwNew["rr_carat"] = numRRCarat;

                    drwNew["sr_no"] = m_Srno;
                    drwNew["balance"] = m_BalCarat;

                    drwNew["resoing_pcs"] = numResoingPcs;
                    drwNew["resoing_carat"] = numResoingCarat;

                    drwNew["breakage_pcs"] = numBrackPcs;
                    drwNew["breakage_carat"] = numBrackCarat;

                    drwNew["org_pcs"] = numorgPcs;
                    drwNew["org_carat"] = numorgCarat;

                    drwNew["carat_plus"] = numCaratPlus;

                    m_dtbReceiveProcess.Rows.Add(drwNew);
                    m_Srno++;
                }
                else if (btnAdd.Text == "&Update")
                {
                    if (m_dtbReceiveProcess.Select("sr_no ='" + Val.ToInt(m_update_srno) + "'").Length > 0)
                    {
                        for (int i = 0; i < m_dtbReceiveProcess.Rows.Count; i++)
                        {
                            if (m_dtbReceiveProcess.Select("cut_no ='" + m_cut_no + "' AND sr_no = '" + m_update_srno + "'").Length > 0)
                            {
                                if (m_dtbReceiveProcess.Rows[dgvMultiEmployeeReturn.FocusedRowHandle]["cut_no"].ToString() == m_cut_no.ToString())
                                {
                                    m_dtbReceiveProcess.Rows[dgvMultiEmployeeReturn.FocusedRowHandle]["issue_id"] = m_issue_id;
                                    m_dtbReceiveProcess.Rows[dgvMultiEmployeeReturn.FocusedRowHandle]["lot_id"] = Val.ToString(txtLotID.Text);
                                    m_dtbReceiveProcess.Rows[dgvMultiEmployeeReturn.FocusedRowHandle]["return_pcs"] = Val.ToInt(txtReturnPcs.Text).ToString();
                                    m_dtbReceiveProcess.Rows[dgvMultiEmployeeReturn.FocusedRowHandle]["return_carat"] = Val.ToString(txtReturnCarat.Text).ToString();
                                    //m_dtbReceiveProcess.Rows[dgvMultiEmployeeReturn.FocusedRowHandle]["loss_pcs"] = Val.ToInt(txtLossPcs.Text).ToString();
                                    m_dtbReceiveProcess.Rows[dgvMultiEmployeeReturn.FocusedRowHandle]["loss_carat"] = Val.ToString(txtLossCarat.Text).ToString();
                                    m_dtbReceiveProcess.Rows[dgvMultiEmployeeReturn.FocusedRowHandle]["lost_carat"] = Val.ToDecimal(txtLostCarat.Text).ToString();
                                    m_dtbReceiveProcess.Rows[dgvMultiEmployeeReturn.FocusedRowHandle]["lost_pcs"] = Val.ToInt(txtLostPcs.Text);
                                    m_dtbReceiveProcess.Rows[dgvMultiEmployeeReturn.FocusedRowHandle]["rejection_carat"] = Val.ToDecimal(txtRejCarat.Text).ToString();
                                    m_dtbReceiveProcess.Rows[dgvMultiEmployeeReturn.FocusedRowHandle]["rejection_pcs"] = Val.ToInt(txtRejPcs.Text);
                                    m_dtbReceiveProcess.Rows[dgvMultiEmployeeReturn.FocusedRowHandle]["rr_carat"] = Val.ToDecimal(txtOutCarat.Text).ToString();
                                    m_dtbReceiveProcess.Rows[dgvMultiEmployeeReturn.FocusedRowHandle]["rr_pcs"] = Val.ToInt(txtOutpcs.Text);
                                    m_dtbReceiveProcess.Rows[dgvMultiEmployeeReturn.FocusedRowHandle]["resoing_pcs"] = Val.ToInt(txtReSoingPcs.Text).ToString();
                                    m_dtbReceiveProcess.Rows[dgvMultiEmployeeReturn.FocusedRowHandle]["resoing_carat"] = Val.ToDecimal(txtResoingCarat.Text);

                                    m_dtbReceiveProcess.Rows[dgvMultiEmployeeReturn.FocusedRowHandle]["breakage_pcs"] = Val.ToInt(txtBreakPcs.Text).ToString();
                                    m_dtbReceiveProcess.Rows[dgvMultiEmployeeReturn.FocusedRowHandle]["breakage_carat"] = Val.ToDecimal(txtBreakCarat.Text);

                                    m_dtbReceiveProcess.Rows[dgvMultiEmployeeReturn.FocusedRowHandle]["carat_plus"] = Val.ToDecimal(txtCaratPlus.Text);

                                    m_BalPcs = (m_OsPcs - (Val.ToInt(txtLostPcs.Text) + Val.ToInt(txtReturnPcs.Text) + Val.ToInt(txtRejPcs.Text) + Val.ToInt(txtOutpcs.Text) + Val.ToInt(txtReSoingPcs.Text) + Val.ToInt(txtBreakPcs.Text)));
                                    m_BalCarat = (m_OsCarat - (Val.ToDecimal(txtLossCarat.Text) + Val.ToDecimal(txtLostCarat.Text) + Val.ToDecimal(txtReturnCarat.Text) + Val.ToDecimal(txtRejCarat.Text) + Val.ToInt(txtOutCarat.Text) + Val.ToInt(txtResoingCarat.Text) + Val.ToInt(txtBreakCarat.Text)));
                                    m_dtbReceiveProcess.Rows[dgvMultiEmployeeReturn.FocusedRowHandle]["balance"] = Val.ToDecimal(m_BalCarat);
                                    m_dtbReceiveProcess.Rows[dgvMultiEmployeeReturn.FocusedRowHandle]["process_id"] = Val.ToInt(lueProcess.EditValue).ToString();
                                    m_dtbReceiveProcess.Rows[dgvMultiEmployeeReturn.FocusedRowHandle]["process"] = Val.ToString(lueProcess.Text);
                                    m_dtbReceiveProcess.Rows[dgvMultiEmployeeReturn.FocusedRowHandle]["manager_id"] = Val.ToInt(m_manager_id).ToString();
                                    m_dtbReceiveProcess.Rows[dgvMultiEmployeeReturn.FocusedRowHandle]["employee"] = Val.ToString(lueEmployee.Text);
                                    m_dtbReceiveProcess.Rows[dgvMultiEmployeeReturn.FocusedRowHandle]["employee_id"] = Val.ToInt(lueEmployee.EditValue);
                                    m_dtbReceiveProcess.Rows[dgvMultiEmployeeReturn.FocusedRowHandle]["machine"] = Val.ToString(lueMachine.Text);
                                    m_dtbReceiveProcess.Rows[dgvMultiEmployeeReturn.FocusedRowHandle]["machine_id"] = Val.ToInt(lueMachine.EditValue);
                                    break;
                                }
                            }
                        }
                    }
                    btnAdd.Text = "&Add";
                }

                dgvMultiEmployeeReturn.MoveLast();
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

                    DateTime endDate = Convert.ToDateTime(DateTime.Today);
                    endDate = endDate.AddDays(15);

                    if (Convert.ToDateTime(dtpReceiveDate.Text) >= endDate)
                    {
                        lstError.Add(new ListError(5, " Recieve Date Not Be Permission After 3 Days Receive this Lot ID...Please Contact to Administrator"));
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
                    int tmpOrgPcs = 0;
                    decimal tmpOrgCarat = 0;

                    int tmpReceivePcs = 0;
                    decimal tmpReceiveCarat = 0;

                    int tmpRRPcs = 0;
                    decimal tmpRRCarat = 0;

                    int tmpRejPcs = 0;
                    decimal tmpRejCarat = 0;

                    int tmpBreakagePcs = 0;
                    decimal tmpBreakageCarat = 0;

                    int tmpReSawingPcs = 0;
                    decimal tmpReSawingCarat = 0;

                    int tmpLostPcs = 0;
                    decimal tmpLostCarat = 0;
                    decimal tmpLossCarat = 0;
                    //for (int i = 0; i < m_dtbReceiveProcess.Rows.Count; i++)
                    //{
                    //    if (i == 0)
                    //    {
                    //        tmpOrgPcs = Val.ToInt32(m_dtbReceiveProcess.Rows[i]["org_pcs"].ToString());
                    //    }

                    //    if (i > 0)
                    //    {
                    //        if (m_dtbReceiveProcess.Rows[i]["lot_id"].ToString() != m_dtbReceiveProcess.Rows[i - 1]["lot_id"].ToString())
                    //        {
                    //            if (tmpOrgPcs - (tmpReceivePcs + tmpRRPcs + tmpRejPcs + tmpBreakagePcs + tmpReSawingPcs + tmpLostPcs) > 0)
                    //            {
                    //                Global.Message("Pcs not match " + m_dtbReceiveProcess.Rows[i]["lot_id"].ToString());
                    //                return false;
                    //            }
                    //            if (tmpOrgPcs - (tmpReceivePcs + tmpRRPcs + tmpRejPcs + tmpBreakagePcs + tmpReSawingPcs + tmpLostPcs) < 0)
                    //            {
                    //                Global.Message("Pcs not match " + m_dtbReceiveProcess.Rows[i - 1]["lot_id"].ToString());
                    //                return false;
                    //            }

                    //            tmpOrgPcs = Val.ToInt32(m_dtbReceiveProcess.Rows[i]["org_pcs"].ToString());

                    //            tmpReceivePcs = 0;
                    //            tmpRRPcs = 0;
                    //            tmpRejPcs = 0;
                    //            tmpBreakagePcs = 0;
                    //            tmpReSawingPcs = 0;
                    //            tmpLostPcs = 0;
                    //        }
                    //    }
                    //    tmpReceivePcs = tmpReceivePcs + Val.ToInt32(m_dtbReceiveProcess.Rows[i]["return_pcs"].ToString());

                    //    tmpRRPcs = tmpRRPcs + Val.ToInt32(m_dtbReceiveProcess.Rows[i]["rr_pcs"].ToString());

                    //    tmpRejPcs = tmpRejPcs + Val.ToInt32(m_dtbReceiveProcess.Rows[i]["rejection_pcs"].ToString());

                    //    tmpBreakagePcs = tmpBreakagePcs + Val.ToInt32(m_dtbReceiveProcess.Rows[i]["breakage_pcs"].ToString());

                    //    tmpReSawingPcs = tmpReSawingPcs + Val.ToInt32(m_dtbReceiveProcess.Rows[i]["resoing_pcs"].ToString());
                    //    tmpLostPcs = tmpLostPcs + Val.ToInt32(m_dtbReceiveProcess.Rows[i]["lost_pcs"].ToString());
                    //    //if (i == 0)
                    //    //{
                    //    //    if (i == (m_dtbReceiveProcess.Rows.Count - 1))
                    //    //    {
                    //    //        if (Val.ToString(lueSubProcess.Text) == "4P-OK")
                    //    //        {
                    //    //            if (tmpOrgPcs - (tmpReceivePcs + tmpRRPcs + tmpBreakagePcs + tmpLostPcs) > 0)
                    //    //            {
                    //    //                Global.Message("Pcs not match " + m_dtbReceiveProcess.Rows[i - 1]["lot_id"].ToString());
                    //    //                return false;
                    //    //            }
                    //    //            if (tmpOrgPcs - (tmpReceivePcs + tmpRRPcs + tmpBreakagePcs + tmpLostPcs) < 0)
                    //    //            {
                    //    //                Global.Message("Pcs not match " + m_dtbReceiveProcess.Rows[i - 1]["lot_id"].ToString());
                    //    //                return false;
                    //    //            }
                    //    //        }
                    //    //        else
                    //    //        {
                    //    //            if (tmpOrgPcs - (tmpReceivePcs + tmpRRPcs + tmpRejPcs + tmpBreakagePcs + tmpReSawingPcs + tmpLostPcs) > 0)
                    //    //            {
                    //    //                Global.Message("Pcs not match " + m_dtbReceiveProcess.Rows[i]["lot_id"].ToString());
                    //    //                return false;
                    //    //            }
                    //    //            if (tmpOrgPcs - (tmpReceivePcs + tmpRRPcs + tmpRejPcs + tmpBreakagePcs + tmpReSawingPcs + tmpLostPcs) < 0)
                    //    //            {
                    //    //                Global.Message("Pcs not match " + m_dtbReceiveProcess.Rows[i]["lot_id"].ToString());
                    //    //                return false;
                    //    //            }
                    //    //        }

                    //    //        tmpReceivePcs = 0;
                    //    //        tmpRRPcs = 0;

                    //    //        tmpRejPcs = 0;

                    //    //        tmpBreakagePcs = 0;

                    //    //        tmpReSawingPcs = 0;

                    //    //        tmpLostPcs = 0;
                    //    //    }
                    //    //}
                    //    //if (i > 0)
                    //    //{
                    //    //    if (i == (m_dtbReceiveProcess.Rows.Count - 1))
                    //    //    {
                    //    //        if (Val.ToString(lueSubProcess.Text) == "4P-OK")
                    //    //        {
                    //    //            if (tmpOrgPcs - (tmpReceivePcs + tmpRRPcs + tmpBreakagePcs + tmpLostPcs) > 0)
                    //    //            {
                    //    //                Global.Message("Pcs not match " + m_dtbReceiveProcess.Rows[i - 1]["lot_id"].ToString());
                    //    //                return false;
                    //    //            }
                    //    //            if (tmpOrgPcs - (tmpReceivePcs + tmpRRPcs + tmpBreakagePcs + tmpLostPcs) < 0)
                    //    //            {
                    //    //                Global.Message("Pcs not match " + m_dtbReceiveProcess.Rows[i - 1]["lot_id"].ToString());
                    //    //                return false;
                    //    //            }
                    //    //        }
                    //    //        else
                    //    //        {
                    //    //            if (tmpOrgPcs - (tmpReceivePcs + tmpRRPcs + tmpRejPcs + tmpBreakagePcs + tmpReSawingPcs + tmpLostPcs) > 0)
                    //    //            {
                    //    //                Global.Message("Pcs not match " + m_dtbReceiveProcess.Rows[i - 1]["lot_id"].ToString());
                    //    //                return false;
                    //    //            }
                    //    //            if (tmpOrgPcs - (tmpReceivePcs + tmpRRPcs + tmpRejPcs + tmpBreakagePcs + tmpReSawingPcs + tmpLostPcs) < 0)
                    //    //            {
                    //    //                Global.Message("Pcs not match " + m_dtbReceiveProcess.Rows[i - 1]["lot_id"].ToString());
                    //    //                return false;
                    //    //            }
                    //    //        }

                    //    //        tmpReceivePcs = 0;
                    //    //        tmpRRPcs = 0;
                    //    //        tmpRejPcs = 0;
                    //    //        tmpBreakagePcs = 0;
                    //    //        tmpReSawingPcs = 0;
                    //    //        tmpLostPcs = 0;
                    //    //    }
                    //    //}
                    //}

                    tmpOrgPcs = 0;
                    tmpOrgCarat = 0;

                    tmpReceivePcs = 0;
                    tmpReceiveCarat = 0;

                    tmpRRPcs = 0;
                    tmpRRCarat = 0;

                    tmpRejPcs = 0;
                    tmpRejCarat = 0;

                    tmpBreakagePcs = 0;
                    tmpBreakageCarat = 0;

                    tmpReSawingPcs = 0;
                    tmpReSawingCarat = 0;

                    tmpLostPcs = 0;
                    tmpLostCarat = 0;

                    tmpLossCarat = 0;

                    for (int i = 0; i < m_dtbReceiveProcess.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            tmpOrgPcs = Val.ToInt32(m_dtbReceiveProcess.Rows[i]["org_pcs"].ToString());
                            tmpOrgCarat = Val.ToDecimal(m_dtbReceiveProcess.Rows[i]["org_carat"].ToString());
                        }

                        if (i > 0)
                        {
                            if (m_dtbReceiveProcess.Rows[i]["lot_id"].ToString() != m_dtbReceiveProcess.Rows[i - 1]["lot_id"].ToString())
                            {
                                if (tmpOrgCarat - (tmpReceiveCarat + tmpRRCarat + tmpRejCarat + tmpBreakageCarat + tmpReSawingCarat + tmpLostCarat + tmpLossCarat) < 0)
                                {
                                    if (Val.ToDecimal(Math.Abs(tmpOrgCarat - (tmpReceiveCarat + tmpRRCarat + tmpRejCarat + tmpBreakageCarat + tmpReSawingCarat + tmpLostCarat + tmpLossCarat))) > Val.ToDecimal(0.100))
                                    {
                                        Global.Message("Carat not match " + m_dtbReceiveProcess.Rows[i - 1]["lot_id"].ToString());
                                        return false;
                                    }
                                    else
                                    {
                                        if (m_OrgCarat < (tmpReceiveCarat + tmpRRCarat + tmpRejCarat + tmpBreakageCarat + tmpReSawingCarat + tmpLostCarat + tmpLossCarat) && (Val.ToString(GlobalDec.gEmployeeProperty.department_name) == "RUSSIAN" || Val.ToString(GlobalDec.gEmployeeProperty.department_name) == "FARSI RUSSION"))
                                        {
                                            Global.Message("Carat not match " + m_dtbReceiveProcess.Rows[i - 1]["lot_id"].ToString());
                                            return false;
                                        }
                                        else
                                        {
                                            m_dtbReceiveProcess.Rows[i - 1]["carat_plus"] = Val.ToDecimal(Math.Abs(tmpOrgCarat - (tmpReceiveCarat + tmpRRCarat + tmpRejCarat + tmpBreakageCarat + tmpReSawingCarat + tmpLostCarat + tmpLossCarat)));
                                        }

                                    }

                                }
                                else
                                {
                                    m_dtbReceiveProcess.Rows[i - 1]["loss_carat"] = tmpLossCarat + Val.ToDecimal(Math.Abs(tmpOrgCarat - (tmpReceiveCarat + tmpRRCarat + tmpRejCarat + tmpBreakageCarat + tmpReSawingCarat + tmpLostCarat + tmpLossCarat)));
                                }

                                tmpOrgPcs = Val.ToInt32(m_dtbReceiveProcess.Rows[i]["org_pcs"].ToString());
                                tmpOrgCarat = Val.ToDecimal(m_dtbReceiveProcess.Rows[i]["org_carat"].ToString());

                                tmpReceivePcs = 0;
                                tmpReceiveCarat = 0;

                                tmpRRPcs = 0;
                                tmpRRCarat = 0;

                                tmpRejPcs = 0;
                                tmpRejCarat = 0;

                                tmpBreakagePcs = 0;
                                tmpBreakageCarat = 0;

                                tmpReSawingPcs = 0;
                                tmpReSawingCarat = 0;

                                tmpLostPcs = 0;
                                tmpLostCarat = 0;

                                tmpLossCarat = 0;
                            }
                        }
                        tmpReceivePcs = tmpReceivePcs + Val.ToInt32(m_dtbReceiveProcess.Rows[i]["return_pcs"].ToString());
                        tmpReceiveCarat = tmpReceiveCarat + Val.ToDecimal(m_dtbReceiveProcess.Rows[i]["return_carat"].ToString());

                        tmpRRPcs = tmpRRPcs + Val.ToInt32(m_dtbReceiveProcess.Rows[i]["rr_pcs"].ToString());
                        tmpRRCarat = tmpRRCarat + Val.ToDecimal(m_dtbReceiveProcess.Rows[i]["rr_carat"].ToString());

                        tmpRejPcs = tmpRejPcs + Val.ToInt32(m_dtbReceiveProcess.Rows[i]["rejection_pcs"].ToString());
                        tmpRejCarat = tmpRejCarat + Val.ToDecimal(m_dtbReceiveProcess.Rows[i]["rejection_carat"].ToString());

                        tmpBreakagePcs = tmpBreakagePcs + Val.ToInt32(m_dtbReceiveProcess.Rows[i]["breakage_pcs"].ToString());
                        tmpBreakageCarat = tmpBreakageCarat + Val.ToDecimal(m_dtbReceiveProcess.Rows[i]["breakage_carat"].ToString());

                        tmpReSawingPcs = tmpReSawingPcs + Val.ToInt32(m_dtbReceiveProcess.Rows[i]["resoing_pcs"].ToString());
                        tmpReSawingCarat = tmpReSawingCarat + Val.ToDecimal(m_dtbReceiveProcess.Rows[i]["resoing_carat"].ToString());

                        tmpLostPcs = tmpLostPcs + Val.ToInt32(m_dtbReceiveProcess.Rows[i]["lost_pcs"].ToString());
                        tmpLostCarat = tmpLostCarat + Val.ToDecimal(m_dtbReceiveProcess.Rows[i]["lost_carat"].ToString());

                        tmpLossCarat = tmpLossCarat + Val.ToDecimal(m_dtbReceiveProcess.Rows[i]["loss_carat"].ToString());
                        if (i == 0)
                        {
                            if (i == (m_dtbReceiveProcess.Rows.Count - 1))
                            {
                                if (tmpOrgCarat - (tmpReceiveCarat + tmpRRCarat + tmpRejCarat + tmpBreakageCarat + tmpReSawingCarat + tmpLostCarat + tmpLossCarat) < 0)
                                {
                                    if (Val.ToDecimal(Math.Abs(tmpOrgCarat - (tmpReceiveCarat + tmpRRCarat + tmpRejCarat + tmpBreakageCarat + tmpReSawingCarat + tmpLostCarat + tmpLossCarat))) > Val.ToDecimal(0.100))
                                    {
                                        Global.Message("Carat not match " + m_dtbReceiveProcess.Rows[i]["lot_id"].ToString());
                                        return false;
                                    }
                                    else
                                    {
                                        if (m_OrgCarat < (tmpReceiveCarat + tmpRRCarat + tmpRejCarat + tmpBreakageCarat + tmpReSawingCarat + tmpLostCarat + tmpLossCarat) && (Val.ToString(GlobalDec.gEmployeeProperty.department_name) == "RUSSIAN" || Val.ToString(GlobalDec.gEmployeeProperty.department_name) == "FARSI RUSSION"))
                                        {
                                            Global.Message("Carat not match " + m_dtbReceiveProcess.Rows[i]["lot_id"].ToString());
                                            return false;
                                        }
                                        else
                                        {
                                            m_dtbReceiveProcess.Rows[i]["carat_plus"] = Val.ToDecimal(Math.Abs(tmpOrgCarat - (tmpReceiveCarat + tmpRRCarat + tmpRejCarat + tmpBreakageCarat + tmpReSawingCarat + tmpLostCarat + tmpLossCarat)));
                                        }
                                    }
                                }
                                else
                                {
                                    m_dtbReceiveProcess.Rows[i]["loss_carat"] = tmpLossCarat + Val.ToDecimal(Math.Abs(tmpOrgCarat - (tmpReceiveCarat + tmpRRCarat + tmpRejCarat + tmpBreakageCarat + tmpReSawingCarat + tmpLostCarat + tmpLossCarat)));
                                }
                                tmpReceivePcs = 0;
                                tmpReceiveCarat = 0;

                                tmpRRPcs = 0;
                                tmpRRCarat = 0;

                                tmpRejPcs = 0;
                                tmpRejCarat = 0;

                                tmpBreakagePcs = 0;
                                tmpBreakageCarat = 0;

                                tmpReSawingPcs = 0;
                                tmpReSawingCarat = 0;

                                tmpLostPcs = 0;
                                tmpLostCarat = 0;

                                tmpLossCarat = 0;
                            }
                        }
                        if (i > 0)
                        {
                            if (i == (m_dtbReceiveProcess.Rows.Count - 1))
                            {
                                if (tmpOrgCarat - (tmpReceiveCarat + tmpRRCarat + tmpRejCarat + tmpBreakageCarat + tmpReSawingCarat + tmpLostCarat + tmpLossCarat) < 0)
                                {
                                    if (Val.ToDecimal(Math.Abs(tmpOrgCarat - (tmpReceiveCarat + tmpRRCarat + tmpRejCarat + tmpBreakageCarat + tmpReSawingCarat + tmpLostCarat + tmpLossCarat))) > Val.ToDecimal(0.100))
                                    {
                                        Global.Message("Carat not match " + m_dtbReceiveProcess.Rows[i - 1]["lot_id"].ToString());
                                        return false;
                                    }
                                    else
                                    {
                                        if (m_OrgCarat < (tmpReceiveCarat + tmpRRCarat + tmpRejCarat + tmpBreakageCarat + tmpReSawingCarat + tmpLostCarat + tmpLossCarat) && (Val.ToString(GlobalDec.gEmployeeProperty.department_name) == "RUSSIAN" || Val.ToString(GlobalDec.gEmployeeProperty.department_name) == "FARSI RUSSION"))
                                        {
                                            Global.Message("Carat not match " + m_dtbReceiveProcess.Rows[i - 1]["lot_id"].ToString());
                                            return false;
                                        }
                                        else
                                        {
                                            m_dtbReceiveProcess.Rows[i]["carat_plus"] = Val.ToDecimal(Math.Abs(tmpOrgCarat - (tmpReceiveCarat + tmpRRCarat + tmpRejCarat + tmpBreakageCarat + tmpReSawingCarat + tmpLostCarat + tmpLossCarat)));
                                        }
                                    }
                                }
                                else
                                {
                                    m_dtbReceiveProcess.Rows[i]["loss_carat"] = tmpLossCarat + Val.ToDecimal(Math.Abs(tmpOrgCarat - (tmpReceiveCarat + tmpRRCarat + tmpRejCarat + tmpBreakageCarat + tmpReSawingCarat + tmpLostCarat + tmpLossCarat)));
                                }
                                tmpReceivePcs = 0;
                                tmpReceiveCarat = 0;

                                tmpRRPcs = 0;
                                tmpRRCarat = 0;

                                tmpRejPcs = 0;
                                tmpRejCarat = 0;

                                tmpBreakagePcs = 0;
                                tmpBreakageCarat = 0;

                                tmpReSawingPcs = 0;
                                tmpReSawingCarat = 0;

                                tmpLostPcs = 0;
                                tmpLostCarat = 0;

                                tmpLossCarat = 0;
                            }
                        }
                    }
                }
                if (m_blnadd)
                {
                    if (Val.ToString(lueSubProcess.Text) == "4P-OK")
                    {
                        if (Val.ToDecimal(lblPer.Text) > 70)
                        {
                            lstError.Add(new ListError(5, "Please Check Kati Weight Greter Than (70%)"));
                            if (!blnFocus)
                            {
                                blnFocus = true;
                                txtLotID.Focus();
                            }
                        }
                        if (Val.ToDecimal(txtManualPer.Text) > 70)
                        {
                            lstError.Add(new ListError(5, "Please Check Kati Weight Greter Than (70%)"));
                            if (!blnFocus)
                            {
                                blnFocus = true;
                                txtLotID.Focus();
                            }
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
                    if (lueCutNo.Text == "")
                    {
                        lstError.Add(new ListError(13, "Cut No"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueManager.Focus();
                        }
                    }
                    if ((txtLossCarat.Text == string.Empty || txtLossCarat.Text == "0") && (txtReturnCarat.Text == string.Empty || txtReturnCarat.Text == "0") && (txtLostCarat.Text == string.Empty || txtLostCarat.Text == "0") && (txtRejCarat.Text == string.Empty || txtRejCarat.Text == "0") && (txtOutCarat.Text == string.Empty || txtOutCarat.Text == "0") && (txtResoingCarat.Text == string.Empty || txtResoingCarat.Text == "0") && (txtBreakCarat.Text == string.Empty || txtBreakCarat.Text == "0") && (txtCaratPlus.Text == string.Empty || txtCaratPlus.Text == "0"))
                    {
                        lstError.Add(new ListError(5, "Return Carat can not be blank!!!"));
                        lstError.Add(new ListError(5, "Loss Carat can not be blank!!!"));
                        lstError.Add(new ListError(5, "Lost Carat can not be blank!!!"));
                        lstError.Add(new ListError(5, "Rejection Carat can not be blank!!!"));
                        lstError.Add(new ListError(5, "RR Carat can not be blank!!!"));
                        lstError.Add(new ListError(5, "Resawing Carat can not be blank!!!"));
                        lstError.Add(new ListError(5, "Breakage Carat can not be blank!!!"));

                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtLossCarat.Focus();
                        }
                    }

                    //if (((txtReturnPcs.Text == string.Empty || txtReturnPcs.Text == "0") && (txtLostPcs.Text == string.Empty || txtLostPcs.Text == "0") && (txtRejPcs.Text == string.Empty || txtRejPcs.Text == "0") && (txtOutpcs.Text == string.Empty || txtOutpcs.Text == "0") && (txtReSoingPcs.Text == string.Empty || txtReSoingPcs.Text == "0") && (txtBreakPcs.Text == string.Empty || txtBreakPcs.Text == "0")) && GlobalDec.gEmployeeProperty.department_name != "CHAPKA")
                    //{
                    //    lstError.Add(new ListError(5, "Return Pcs can not be blank!!!"));
                    //    lstError.Add(new ListError(5, "Loss Pcs can not be blank!!!"));
                    //    lstError.Add(new ListError(5, "Lost Pcs can not be blank!!!"));
                    //    lstError.Add(new ListError(5, "Rejection Pcs can not be blank!!!"));
                    //    lstError.Add(new ListError(5, "RR Pcs can not be blank!!!"));
                    //    lstError.Add(new ListError(5, "Resawing Pcs can not be blank!!!"));
                    //    lstError.Add(new ListError(5, "Breakage Pcs can not be blank!!!"));
                    //    if (!blnFocus)
                    //    {
                    //        blnFocus = true;
                    //        txtLossCarat.Focus();
                    //    }
                    //}

                    //if (((txtReturnPcs.Text != string.Empty || txtReturnPcs.Text != "0") && (txtLostPcs.Text != string.Empty || txtLostPcs.Text != "0") && (txtRejPcs.Text != string.Empty || txtRejPcs.Text == "0") && (txtOutpcs.Text == string.Empty || txtOutpcs.Text == "0") && (txtReSoingPcs.Text != string.Empty || txtReSoingPcs.Text != "0") && (txtBreakPcs.Text != string.Empty || txtBreakPcs.Text != "0")) && GlobalDec.gEmployeeProperty.department_name != "CHAPKA")
                    //{
                    //    if (Val.ToInt(txtLostPcs.Text) > Val.ToInt32(lblOsPcs.Text))
                    //    {
                    //        lstError.Add(new ListError(5, "Lost Pcs not Greater than OS Pcs!!!"));
                    //        if (!blnFocus)
                    //        {
                    //            blnFocus = true;
                    //            txtLostPcs.Focus();
                    //        }
                    //    }
                    //}

                    // Add By Praful On 17122022
                    if (txtReturnPcs.Text != string.Empty)
                    {
                        if (Val.ToString(lueSubProcess.Text) != "GALAXY LS" && Val.ToString(lueSubProcess.Text) != "LS" && Val.ToString(lueSubProcess.Text) != "4P OK" && Val.ToString(lueSubProcess.Text) != "ACTIVE PART" && Val.ToString(lueSubProcess.Text) != "LS OK" && Val.ToString(lueSubProcess.Text) != "GALAXY LS" && Val.ToString(lueSubProcess.Text) != "ACTIVE PART LS" && Val.ToString(lueSubProcess.Text) != "GALAXY 4P OK")
                        {
                            if (Val.ToInt(txtReturnPcs.Text) > Val.ToInt32(lblOsPcs.Text))
                            {
                                lstError.Add(new ListError(5, "Return Pcs not Greater than OS Pcs!!!"));
                                if (!blnFocus)
                                {
                                    blnFocus = true;
                                    txtReturnPcs.Focus();
                                }
                            }
                        }
                    }
                    //End

                    //if (Val.ToString(lueSubProcess.Text) == "4P-OK" || Val.ToString(lueSubProcess.Text) == "4P OK")
                    //{
                    //    if ((txtReturnPcs.Text != string.Empty || txtReturnPcs.Text != "0"))
                    //    {
                    //        if (Val.ToInt(txtReturnPcs.Text) < Val.ToInt32(lblOsPcs.Text))
                    //        {
                    //            lstError.Add(new ListError(5, "Return Pcs not Less than OS Pcs!!!"));
                    //            if (!blnFocus)
                    //            {
                    //                blnFocus = true;
                    //                txtReturnPcs.Focus();
                    //            }
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    if ((txtReturnPcs.Text != string.Empty || txtReturnPcs.Text != "0"))
                    //    {
                    //        if (Val.ToInt(txtReturnPcs.Text) + Val.ToInt(txtOutpcs.Text) < Val.ToInt32(lblOsPcs.Text))
                    //        {
                    //            lstError.Add(new ListError(5, "Return Pcs not Less than OS Pcs!!!"));
                    //            if (!blnFocus)
                    //            {
                    //                blnFocus = true;
                    //                txtReturnPcs.Focus();
                    //            }
                    //        }
                    //    }
                    //}
                    if (Val.ToString(lueSubProcess.Text) == "ROBOT OPERATOR")
                    {
                        if (Val.ToString(lueMachine.Text) == "")
                        {
                            lstError.Add(new ListError(13, "Machine Name"));
                            if (!blnFocus)
                            {
                                blnFocus = true;
                                lueMachine.Focus();
                            }
                        }
                    }
                    if (lueSubProcess.Text.ToString() != "ROBOT OPERATOR" && lueSubProcess.Text.ToString() != "MANUAL OPERATOR")
                    {
                        if (Val.ToDecimal(txtReturnCarat.Text) <= 0)
                        {
                            lstError.Add(new ListError(5, "Recieve carat should not be zero or less than zero!!!"));
                            if (!blnFocus)
                            {
                                blnFocus = true;
                                txtReturnCarat.Focus();
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
                lueManager.EditValue = System.DBNull.Value;
                lueEmployee.EditValue = System.DBNull.Value;

                lueProcess.EditValue = System.DBNull.Value;
                lueSubProcess.EditValue = System.DBNull.Value;

                //if (GblLockBarcode != "")
                //{
                //    int IntReso = objCutCreate.LockDelete(3, 0, GblLockBarcode); //For Unlock All Lot_ID

                //    if (IntReso > 0)
                //    {
                //        GblLockBarcode = string.Empty;
                //    }
                //}

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
                txtCaratPlus.Text = string.Empty;
                txtBreakPcs.Text = string.Empty;
                txtBreakCarat.Text = string.Empty;
                txtReSoingPcs.Text = string.Empty;
                txtResoingCarat.Text = string.Empty;
                txtLotID.Text = string.Empty;
                Main_Lot_ID = string.Empty;
                lblOsCarat.Text = "0";
                lblOsPcs.Text = "0";
                lblWagesRate.Text = "0";
                lblWagesAmt.Text = "0";
                lblTotalCarat.Text = "0";
                lblTotalPcs.Text = "0";
                lblFinalWeight.Text = "0";
                lblPer.Text = "0";
                m_manager_id = 0;
                m_emp_id = 0;
                m_BalCarat = 0;
                m_Srno = 1;
                m_update_srno = 1;
                btnAdd.Text = "&Add";
                txtMainLotID.Text = "";
                m_isMainLot = 0;
                grdPendingLots.DataSource = null;

                if (GlobalDec.gEmployeeProperty.role_name == "SURAT POLISH REPARING")
                {
                    txtBoilingLotID.Visible = true;
                    label7.Visible = true;
                    txtBoilingLotID.Text = Val.ToInt64(objMFGMultiEmp.FindMaxBoilingLotID()).ToString();
                }

                else
                {
                    txtBoilingLotID.Visible = false;
                    label7.Visible = false;
                    txtBoilingLotID.Text = string.Empty;
                }

                lueMachine.Focus();
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
                m_dtbReceiveProcess.Columns.Add("manager", typeof(string));
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

                m_dtbReceiveProcess.Columns.Add("org_pcs", typeof(int)).DefaultValue = 0;
                m_dtbReceiveProcess.Columns.Add("org_carat", typeof(decimal)).DefaultValue = 0;

                m_dtbReceiveProcess.Columns.Add("carat_plus", typeof(decimal)).DefaultValue = 0;
                m_dtbReceiveProcess.Columns.Add("final_weight", typeof(decimal)).DefaultValue = 0;
                m_dtbReceiveProcess.Columns.Add("lot_no", typeof(int)).DefaultValue = 0;
                m_dtbReceiveProcess.Columns.Add("main_lot_id", typeof(int));

                grdMultiEmployeeReturn.DataSource = m_dtbReceiveProcess;
                grdMultiEmployeeReturn.Refresh();
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
        public void DoUpdate(object sender, EventArgs e)
        {
            string val = "";
            if (readBuffer.ToString().Contains("+"))
            {
                val = readBuffer.ToString().Split('+')[1].Replace(" ct", "").Replace(" ", "");
            }
            else
            {
                val = readBuffer.ToString();
            }
            //txtReturnCarat.Text = val;
            decimal FinalWeight = Math.Round(Convert.ToDecimal(val), 3);
            lblFinalWeight.Text = FinalWeight.ToString();
            txtReturnCarat.Text = FinalWeight.ToString();

            if (Convert.ToDecimal(lblOsCarat.Text) > 0 && Convert.ToDecimal(lblFinalWeight.Text) > 0)
            {
                lblPer.Text = Val.ToString(Math.Round((Convert.ToDecimal(lblFinalWeight.Text) / Convert.ToDecimal(lblOsCarat.Text)) * 100, 2));
            }




            //txtReturnCarat.Text = lblOsCarat.Text;
            if (Val.ToString(lueSubProcess.Text) == "4P-OK")
            {
                txtReturnPcs.Text = Val.ToString(Val.ToInt(lblOsPcs.Text) - Val.ToInt(txtOutpcs.Text) - Val.ToInt(txtBreakPcs.Text) - Val.ToInt(txtLostPcs.Text));
            }
            else
            {
                txtReturnPcs.Text = Val.ToString(Val.ToInt(lblOsPcs.Text) - Val.ToInt(txtOutpcs.Text) - Val.ToInt(txtBreakPcs.Text));
            }


            txtLossCarat.Text = Convert.ToString(Convert.ToDecimal(Convert.ToString(lblOsCarat.Text)) - Convert.ToDecimal(Convert.ToString(lblFinalWeight.Text)) - Val.ToDecimal(txtBreakCarat.Text) - Val.ToDecimal(txtOutCarat.Text));
            sumData();
            txtReSoingPcs.Text = "0";
            //txtBreakPcs.Text = "0";
            if (Val.ToString(lueSubProcess.Text) != "4P-OK")
            {
                txtLostPcs.Text = "0";
            }

            txtRejPcs.Text = "0";
            txtResoingCarat.Text = "0";
            //txtBreakCarat.Text = "0";
            txtLostCarat.Text = "0";
            txtRejCarat.Text = "0";
        }
        public void EnDis(bool O, bool C)
        {
            btnOpenPort.Enabled = O;
            btnClosePort.Enabled = C;
        }
        public void sumData()
        {
            lblTotalPcs.Text = Val.ToString(Val.ToInt(txtReturnPcs.Text) + Val.ToInt(txtOutpcs.Text) + Val.ToInt(txtRejPcs.Text) + Val.ToInt(txtReSoingPcs.Text) + Val.ToInt(txtBreakPcs.Text) + Val.ToInt(txtLostPcs.Text));
            lblTotalCarat.Text = Val.ToString(Val.ToDecimal(txtReturnCarat.Text) + Val.ToDecimal(txtOutCarat.Text) + Val.ToDecimal(txtRejCarat.Text) + Val.ToDecimal(txtResoingCarat.Text) + Val.ToDecimal(txtBreakCarat.Text) + Val.ToDecimal(txtLossCarat.Text) + Val.ToDecimal(txtLostCarat.Text) + Val.ToDecimal(txtCaratPlus.Text));
        }
        #endregion

        private void txtManualCarat_EditValueChanged(object sender, EventArgs e)
        {
            txtReturnCarat.Text = Val.ToDecimal(txtManualCarat.Text).ToString();

            if (Convert.ToDecimal(lblOsCarat.Text) > 0 && Convert.ToDecimal(txtManualCarat.Text) > 0)
            {
                txtManualPer.Text = Val.ToString(Math.Round((Convert.ToDecimal(txtManualCarat.Text) / Convert.ToDecimal(lblOsCarat.Text)) * 100, 2));
            }

            txtReturnPcs.Text = Val.ToString(Val.ToInt(lblOsPcs.Text) - Val.ToInt(txtOutpcs.Text) - Val.ToInt(txtBreakPcs.Text));
            txtLossCarat.Text = Convert.ToString(Convert.ToDecimal(Convert.ToString(lblOsCarat.Text)) - Convert.ToDecimal(Convert.ToString(txtManualCarat.Text)) - Val.ToInt(txtBreakCarat.Text));

            txtReSoingPcs.Text = "0";
            //txtBreakPcs.Text = "0";
            //txtLostPcs.Text = "0";
            txtRejPcs.Text = "0";
            txtResoingCarat.Text = "0";
            //txtBreakCarat.Text = "0";
            txtLostCarat.Text = "0";
            txtRejCarat.Text = "0";

            sumData();
        }
        private void txtManualCarat_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == Convert.ToChar(Keys.Enter))
            //{
            //    btnAdd.Focus();
            //}
        }
        private void btnSearch1_Click(object sender, EventArgs e)
        {
            FrmMFGLottingSearch FrmSearchLotting = new FrmMFGLottingSearch();
            FrmSearchLotting.FrmMFGGalaxyMultiEployeeReturn = this;
            FrmSearchLotting.ShowForm(this);
        }

        private void textEdit1_Validated(object sender, EventArgs e)
        {
            try
            {
                m_dtbReceiveProcess.AcceptChanges();

                if (txtMainLotID.Text.Length == 0)
                {
                    return;
                }
                if (m_dtbReceiveProcess.Rows.Count > 0)
                {
                    DataTable dtDistinct = new DataTable();
                    dtDistinct = m_dtbReceiveProcess.DefaultView.ToTable(true, "main_lot_id");
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
                if (IsIssue == 0)
                {
                    Global.Message("Lot ID Already Recieved.");
                    txtMainLotID.Text = "";
                    txtMainLotID.Focus();
                    return;
                }
                DataTable dtTemp = new DataTable();
                dtTemp = Global.GetRoughJangedMainStockWise(Val.ToInt(lueKapan.EditValue), Val.ToInt64(txtMainLotID.Text), "GALAXY_RECIEVE");
                m_isMainLot = 1;
                m_dtbReceiveProcess = m_dtbReceiveProcess.AsEnumerable().Union(dtTemp.AsEnumerable()).CopyToDataTable();
                //m_dtbReceiveProcess = Global.GetRoughJangedMainStockWise(Val.ToInt(lueKapan.EditValue), Val.ToInt64(txtMainLotID.Text), "GALAXY_RECIEVE");

                if (m_dtbReceiveProcess.Rows.Count > 0)
                {

                }
                else
                {
                    Global.Message("Lot ID Not Issue in Galaxy");
                    txtLotID.Text = "";
                    txtLotID.Focus();
                    return;
                }
                Main_Lot_ID = Main_Lot_ID + "," + Val.ToString(txtMainLotID.Text);
                grdMultiEmployeeReturn.DataSource = m_dtbReceiveProcess;
                grdMultiEmployeeReturn.RefreshDataSource();
                dgvMultiEmployeeReturn.BestFitColumns();
                txtMainLotID.Text = "";
                txtMainLotID.Focus();

            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }

        private void textEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
