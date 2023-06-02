using BLL;
using BLL.FunctionClasses.Master.MFG;
using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using DERP.Master;
using DERP.Master.MFG;
using DevExpress.XtraEditors;
using DREP.Master;
using DREP.Master.MFG;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using static DERP.Class.Global;
using static DERP.Master.FrmMappingMaster;

namespace DERP.Transaction.MFG
{
    public partial class FrmMFGProcessIssueFactory : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        Control _NextEnteredControl;
        private List<Control> _tabControls;

        MFGEmployeeTarget objEmployeeTarget;
        MFGSawableRecieve objSawableRecieve;
        MFGProcessReceive objProcessRecieve;
        MfgRoughSieve objRoughSieve;
        MfgRoughClarityMaster objClarity;
        MFGProcessIssue objMFGProcessIssue;
        MFGCutCreate objCutCreate;

        DataTable DtControlSettings;
        DataTable m_dtbDetail;
        DataTable m_dtbParam;
        DataTable m_dtbSubProcess;
        DataTable m_dtbIssueProcess;
        DataTable m_dtOutstanding;
        DataTable m_dtbKapan;
        IDataObject PasteclipData = Clipboard.GetDataObject();
        DataTable dtOsRate = new DataTable();
        DataTable dtAssortRate = new DataTable();
        DataTable DtPending = new DataTable();
        //DataTable DTab_KapanWiseData = new DataTable();
        string PasteData = string.Empty;
        int m_Srno;
        int m_update_srno;
        int m_flag;
        int m_prd_id;
        Int64 m_numForm_id;
        Int64 Issue_IntRes;
        int m_kapan_id;
        int m_IsLot;
        int m_OsPcs;

        decimal m_balcarat;
        decimal m_old_carat;
        decimal m_OScarat;
        decimal m_os_rate;
        decimal m_assort_rate;

        bool m_blnadd;
        bool m_blnsave;
        bool m_blnflag;

        int m_RejPcs;
        decimal m_RejCarat;
        int m_OutPcs;
        decimal m_OutCarat;
        int m_RecPcs;
        decimal m_RecCarat;
        int m_ResoingPcs;
        decimal m_ResoingCarat;
        string manager_short_name = string.Empty;

        #endregion

        #region Constructor
        public FrmMFGProcessIssueFactory()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            _NextEnteredControl = new Control();
            _tabControls = new List<Control>();

            objEmployeeTarget = new MFGEmployeeTarget();
            objSawableRecieve = new MFGSawableRecieve();
            objProcessRecieve = new MFGProcessReceive();
            objRoughSieve = new MfgRoughSieve();
            objClarity = new MfgRoughClarityMaster();
            objMFGProcessIssue = new MFGProcessIssue();
            objCutCreate = new MFGCutCreate();

            DtControlSettings = new DataTable();
            m_dtbDetail = new DataTable();
            m_dtbParam = new DataTable();
            m_dtbSubProcess = new DataTable();
            m_dtbIssueProcess = new DataTable();
            m_dtOutstanding = new DataTable();
            m_dtbKapan = new DataTable();

            m_Srno = 1;
            m_update_srno = 1;
            m_flag = 0;
            m_prd_id = 0;
            m_numForm_id = 0;
            m_kapan_id = 0;
            m_IsLot = 0;
            m_OsPcs = 0;
            m_balcarat = 0;
            m_old_carat = 0;
            m_OScarat = 0;
            m_os_rate = 0;
            m_assort_rate = 0;
            m_blnadd = new bool();
            m_blnsave = new bool();
            m_blnflag = new bool();
            m_RejPcs = 0;
            m_RejCarat = 0;
            m_OutPcs = 0;
            m_OutCarat = 0;
            m_RecPcs = 0;
            m_RecCarat = 0;
            m_ResoingPcs = 0;
            m_ResoingCarat = 0;
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
        private void ControllSettings(Control item2, DataTable DTab)
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
        private void RepDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (Global.Confirm("Are you sure delete selected row?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                grvProcessIssue.DeleteRow(grvProcessIssue.GetRowHandle(grvProcessIssue.FocusedRowHandle));
                m_dtbIssueProcess.AcceptChanges();
            }
        }
        private void FrmMFGProcessIssue_Load(object sender, EventArgs e)
        {
            try
            {
                Global.LOOKUPEmp(lueEmployee);
                Global.LOOKUPManager(lueManager);
                Global.LOOKUPProcess(lueProcess);
                Global.LOOKUPSubProcess(lueSubProcess);
                Global.LOOKUPRoughSieve(lueSieve);
                Global.LOOKUPPurity(luePurity);
                Global.LOOKUPQuality(lueQuality);
                Global.LOOKUPClarity(lueclarity);

                m_dtbSubProcess = (((DataTable)lueSubProcess.Properties.DataSource).Copy());

                dtpIssueDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpIssueDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpIssueDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpIssueDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpIssueDate.EditValue = DateTime.Now;

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

                DataTable dtbProcess = (((DataTable)lueProcess.Properties.DataSource).Copy());
                if (dtbProcess.Rows.Count > 0)
                {
                    dtbProcess = dtbProcess.Select("is_default = 'True'").CopyToDataTable();
                    lueProcess.EditValue = Val.ToInt(dtbProcess.Rows[0]["process_id"]);
                }
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
                if (GlobalDec.gEmployeeProperty.user_name == "VISHAL4P")
                {
                    lueSubProcess.EditValue = 3039;
                }
                if (GlobalDec.gEmployeeProperty.user_name == "PRINCE")
                {
                    DataTable dtbProcess_Patta = (((DataTable)lueProcess.Properties.DataSource).Copy());
                    dtbProcess_Patta = dtbProcess_Patta.Select("process_name = 'PATTA ASSORT'").CopyToDataTable();
                    lueProcess.EditValue = Val.ToInt(dtbProcess_Patta.Rows[0]["process_id"]);
                    lueProcess.Text = "PATTA ASSORT";
                    lueSubProcess.Text = "PATTA ASSORT";
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (AddInGrid())
                {
                    lueProcess.Enabled = false;
                    //lueSubProcess.Enabled = false;
                    luePurity.Enabled = false;
                    lueQuality.Enabled = false;
                    lueSieve.Enabled = false;
                    txtIssuePcs.Text = string.Empty;
                    txtIssCarat.Text = string.Empty;
                    txtRate.Text = string.Empty;
                    txtAmount.Text = string.Empty;
                    txtLotID.Text = string.Empty;
                    txtRejCarat.Text = string.Empty;
                    txtRejPcs.Text = string.Empty;
                    txtOutCarat.Text = string.Empty;
                    txtOutpcs.Text = string.Empty;
                    txtIssuePcs.Text = string.Empty;
                    txtIssCarat.Text = string.Empty;
                    txtLotID.Focus();
                    lblWagesRate.Text = "0";
                    lblWagesAmt.Text = "0";
                    lblOsPcs.Text = "0";
                    lblOsCarat.Text = "0";
                    lbltxttotalpcs.Text = "0";
                    lbltxttotalCarat.Text = "0";
                    m_os_rate = 0;
                    m_RejCarat = 0;
                    m_RejPcs = 0;
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
                var Date = DateTime.Compare(Convert.ToDateTime(dtpIssueDate.Text), DateTime.Today);
                if (Date < 0)
                {
                    Str = GlobalDec.CheckLockIsOpenOrNot(Val.DBDate(dtpIssueDate.Text), Val.DBTime(DateTime.Now.ToShortTimeString())); //Val.GetFullTime12());
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
                        dtpIssueDate.Enabled = true;
                        dtpIssueDate.Visible = true;
                    }
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
                //        dtpIssueDate.Enabled = true;
                //        dtpIssueDate.Visible = true;
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
                foreach (DataRow drw in m_dtbIssueProcess.Rows)
                {
                    if (GlobalDec.gEmployeeProperty.role_name == "SURAT 4P" || GlobalDec.gEmployeeProperty.role_name == "SURAT 4P OK" || GlobalDec.gEmployeeProperty.role_name == "SURAT SARIN" || GlobalDec.gEmployeeProperty.role_name == "SURAT SARIN ENTRY" || GlobalDec.gEmployeeProperty.role_name == "SURAT 4P ENTRY" || GlobalDec.gEmployeeProperty.role_name == "SURAT RUSSIAN" || GlobalDec.gEmployeeProperty.role_name == "SURAT POLISH REPARING" || GlobalDec.gEmployeeProperty.role_name == "SURAT KATARGAM" || GlobalDec.gEmployeeProperty.role_name == "SURAT KAMALA")
                    {
                        MFGProcessIssue objMFGProcessIssue = new MFGProcessIssue();
                        MFGProcessIssueProperty objMFGProcessIssueProperty = new MFGProcessIssueProperty();

                        objMFGProcessIssueProperty.lot_id = Val.ToInt64(drw["lot_id"]);
                        objMFGProcessIssueProperty.process_id = Val.ToInt32(lueProcess.EditValue);
                        objMFGProcessIssueProperty.sub_process_id = Val.ToInt32(lueSubProcess.EditValue);
                        objMFGProcessIssueProperty = objMFGProcessIssue.GetData_PrevProcess(objMFGProcessIssueProperty);

                        if (objMFGProcessIssueProperty.Messgae != "" && objMFGProcessIssueProperty.Messgae != null)
                        {
                            Global.Message(objMFGProcessIssueProperty.Messgae);
                            btnSave.Enabled = true;
                            return;
                        }
                    }
                    int DateCheck = Global.ValidateDate(Val.ToInt(drw["lot_id"]), Val.ToString(dtpIssueDate.Text));
                    if (DateCheck == 0)
                    {
                        Global.Message("Plz Check Recieve Date is less than Janged Date " + Val.ToInt(drw["lot_id"]));
                        btnSave.Enabled = true;
                        return;
                    }
                }

                DialogResult result = MessageBox.Show("Do you want to save Process issue data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result != DialogResult.Yes)
                {
                    btnSave.Enabled = true;
                    return;
                }

                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                panelProgress.Visible = true;
                backgroundWorker_ProcessIssue.RunWorkerAsync();

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
        private void btnExport_Click(object sender, EventArgs e)
        {
            Global.Export("xlsx", grvProcessIssue);
        }
        private void lueProcess_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                MFGProcessIssue objMFGProcessIssue = new MFGProcessIssue();
                if (Val.ToInt(lueEmployee.EditValue) > 0 && Val.ToInt(lueManager.EditValue) > 0 && Val.ToInt(lueSubProcess.EditValue) > 0)
                {

                    dtOsRate = objMFGProcessIssue.GetOSRate(Val.ToInt(lueCutNo.EditValue), Val.ToInt64(txtLotID.Text), Val.ToString(lueProcess.Text));

                    if (dtOsRate.Rows.Count > 0)
                    {
                        m_os_rate = Val.ToDecimal(dtOsRate.Rows[0]["rate"]);
                        txtRate.Text = Val.ToString(m_os_rate);
                    }
                    else
                    {
                        m_os_rate = objMFGProcessIssue.GetLatestRate(Val.ToInt64(txtLotID.Text));
                        m_os_rate = 0;
                        txtRate.Text = Val.ToString(m_os_rate);
                    }



                    //btnAdd.Enabled = true;
                    btnPopUpStock.Enabled = true;
                }
                else
                {
                    // btnAdd.Enabled = false;
                    btnPopUpStock.Enabled = false;
                }

                txtRejPcs.Text = Val.ToString(m_RejPcs);
                txtRejCarat.Text = Val.ToString(m_RejCarat);

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
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void lueEmployee_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToInt(lueEmployee.EditValue) > 0 && Val.ToInt(lueManager.EditValue) > 0 && Val.ToInt(lueSubProcess.EditValue) > 0)
                {
                    // btnAdd.Enabled = true;
                    btnPopUpStock.Enabled = true;
                }
                else
                {
                    //btnAdd.Enabled = false;
                    btnPopUpStock.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void lueProcess_EditValueChanged_2(object sender, EventArgs e)
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
                        if (GlobalDec.gEmployeeProperty.user_name == "4POK")
                        {
                            lueSubProcess.EditValue = Val.ToInt32(2024);
                            lueSubProcess.Enabled = false;
                        }
                        else
                        {
                            lueSubProcess.EditValue = System.DBNull.Value;
                            lueSubProcess.Enabled = true;
                        }

                    }

                    MFGProcessIssue objMFGProcessIssue = new MFGProcessIssue();
                    dtOsRate = objMFGProcessIssue.GetOSRate(Val.ToInt(lueCutNo.EditValue), Val.ToInt64(txtLotID.Text), Val.ToString(lueProcess.Text));

                    if (dtOsRate.Rows.Count > 0)
                    {
                        m_os_rate = Val.ToDecimal(dtOsRate.Rows[0]["rate"]);
                        txtRate.Text = Val.ToString(m_os_rate);
                    }
                    else
                    {
                        m_os_rate = 0;
                        txtRate.Text = Val.ToString(m_os_rate);
                    }

                    if (lueProcess.Text == "SHINE ISSUE")
                    {
                        if (dtAssortRate.Rows.Count > 0)
                        {
                            txtRate.Text = Val.ToString(dtAssortRate.Rows[0]["rate"]);
                            lueclarity.EditValue = Val.ToInt(dtAssortRate.Rows[0]["rough_clarity_id"]);
                            lueSieve.EditValue = Val.ToInt(dtAssortRate.Rows[0]["rough_sieve_id"]);
                            lueQuality.Tag = Val.ToString(dtAssortRate.Rows[0]["quality_id"]);
                            lueQuality.Text = Val.ToString(dtAssortRate.Rows[0]["quality_name"]);
                            luePurity.EditValue = Val.ToInt(dtAssortRate.Rows[0]["purity_id"]);
                        }
                        else
                        {
                            Global.Message("Assort process is not complete.");
                        }
                    }

                    //DataTable process = objEmployee.GetProcessWiseEmployee(Val.ToInt(lueProcess.EditValue));

                    //lueEmployee.Properties.DataSource = process;
                    //lueEmployee.Properties.ValueMember = "employee_id";
                    //lueEmployee.Properties.DisplayMember = "employee_name";
                    //lueEmployee.ClosePopup();
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
                if (m_IsLot == 0)
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

                                    if (txtLotID.Text != string.Empty || Val.ToInt64(txtLotID.Text) != 0)
                                    {
                                        GetOsCarat(Val.ToInt64(txtLotID.Text));
                                        DataTable dtIssOS = new DataTable();
                                        dtIssOS = objProcessRecieve.Carat_Sarin_OutStanding_GetData(Val.ToInt64(txtLotID.Text), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue), 0, "I");

                                        if (dtIssOS.Rows.Count > 0)
                                        {
                                            if (Val.ToDecimal(dtIssOS.Rows[0]["outstanding_carat"]) > 0)
                                            {
                                                m_OScarat = Val.ToDecimal(Val.ToDecimal(dtIssOS.Rows[0]["outstanding_carat"]));

                                                txtBalCarat.Text = Val.ToString(m_OScarat);
                                                txtIssCarat.Text = Val.ToString(m_OScarat);
                                                lblOsCarat.Text = Val.ToString(Val.ToDecimal(dtIssOS.Rows[0]["outstanding_carat"]));
                                                lblOsPcs.Text = Val.ToString(Val.ToDecimal(dtIssOS.Rows[0]["outstanding_pcs"]));
                                                txtIssuePcs.Text = Val.ToString(Val.ToDecimal(dtIssOS.Rows[0]["outstanding_pcs"]));
                                            }
                                        }
                                        else
                                        {
                                            txtLotID.Text = null;
                                            txtBalCarat.Text = string.Empty;
                                            txtIssCarat.Text = string.Empty;
                                            txtIssuePcs.Text = string.Empty;
                                            lblOsCarat.Text = "0.00";
                                            lblOsPcs.Text = "0";
                                        }
                                    }
                                }
                                if (lueProcess.Text == "CHIPIYO FINAL")
                                {
                                    txtRate.Text = Val.ToString(Math.Round(m_os_rate, 2));
                                }
                                else if (lueProcess.Text == "SHINE ISSUE")
                                {
                                    txtRate.Text = Val.ToString(Math.Round(m_assort_rate, 2));
                                }
                                else
                                {
                                    txtRate.Text = Val.ToString("0");
                                }
                            }
                        }
                        m_blnflag = false;
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
        private void txtLotID_Validated(object sender, EventArgs e)
        {
            try
            {
                //if (txtLotID.Text != "")
                //{
                //    MFGCutCreateProperty oProperty = new MFGCutCreateProperty();
                //    oProperty.lock_date = Val.DBDate(dtpIssueDate.Text);
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


                m_IsLot = 1;
                lueKapan.EditValue = null;
                lueCutNo.EditValue = null;

                MFGProcessReceive objProcessRecieve = new MFGProcessReceive();
                if (Val.ToInt64(txtLotID.Text) != 0)
                {
                    DataTable DtConfirm = Global.CheckConfirmLot(Val.ToInt64(txtLotID.Text));
                    if (DtConfirm.Rows.Count == 0)
                    {
                        Global.Message("Please Confirm Lot First!!!");
                        return;
                    }
                }
                if (Val.ToInt64(txtLotID.Text) != 0 && Val.ToString(lueKapan.Text) == "" && Val.ToString(lueCutNo.Text) == "")
                {
                    m_dtbParam = Global.GetRoughStockWise(Val.ToInt(0), Val.ToInt64(txtLotID.Text));

                    if (m_dtbParam.Rows.Count > 0)
                    {
                        lueKapan.EditValue = Val.ToInt64(m_dtbParam.Rows[0]["kapan_id"]);

                        MFGProcessIssue objMFGProcessIssue = new MFGProcessIssue();
                        m_blnflag = true;
                        //lueKapan.Text = Val.ToString(m_dtbParam.Rows[0]["kapan_no"]);
                        lueCutNo.Properties.DataSource = m_dtbParam;
                        lueCutNo.Properties.ValueMember = "rough_cut_id";
                        lueCutNo.Properties.DisplayMember = "rough_cut_no";
                        lueCutNo.Text = Val.ToString(m_dtbParam.Rows[0]["rough_cut_no"]);
                        lueManager.EditValue = Val.ToInt64(m_dtbParam.Rows[0]["manager_id"]);
                        //lueManager.Text = Val.ToString(m_dtbParam.Rows[0]["manager_name"]);
                        manager_short_name = Val.ToString(m_dtbParam.Rows[0]["manager_short_name"]);

                        m_os_rate = objMFGProcessIssue.GetLatestRate(Val.ToInt64(txtLotID.Text));
                        DataTable dtIssOS = new DataTable();
                        dtIssOS = objProcessRecieve.Carat_Sarin_OutStanding_GetData(Val.ToInt64(txtLotID.Text), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue), 0, "I");

                        dtAssortRate = objMFGProcessIssue.GetAssortRate(Val.ToInt64(txtLotID.Text), 0);

                        DataTable dtb_StockCarat = objSawableRecieve.GetBalanceCarat(Val.ToInt64(txtLotID.Text), 0);
                        m_kapan_id = Val.ToInt(lueKapan.EditValue);

                        DataTable dtWagesRate = new DataTable();
                        dtWagesRate = objMFGProcessIssue.GetWagesRate(Val.ToInt64(txtLotID.Text), GlobalDec.gEmployeeProperty.department_name, Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue));
                        if (dtWagesRate.Rows.Count > 0)
                        {
                            lblWagesRate.Text = Val.ToString(Val.ToDecimal(dtWagesRate.Rows[0]["rate"]));
                        }
                        if (dtb_StockCarat.Rows.Count > 0)
                        {
                            if (Val.ToInt(dtb_StockCarat.Rows[0]["chipyo_prd_id"]) > 0)
                                m_prd_id = Val.ToInt(dtb_StockCarat.Rows[0]["chipyo_prd_id"]);
                            if (Val.ToInt(dtb_StockCarat.Rows[0]["sawable_prd_id"]) > 0)
                                m_prd_id = Val.ToInt(dtb_StockCarat.Rows[0]["sawable_prd_id"]);
                            else
                                m_prd_id = Val.ToInt(0);
                        }

                        if (dtIssOS.Rows.Count > 0)
                        {
                            if (Val.ToDecimal(dtIssOS.Rows[0]["outstanding_carat"]) > 0)
                            {
                                m_OScarat = Val.ToDecimal(Val.ToDecimal(dtIssOS.Rows[0]["outstanding_carat"]));
                                m_OsPcs = Val.ToInt(Val.ToDecimal(dtIssOS.Rows[0]["outstanding_pcs"]));
                                txtBalCarat.Text = Val.ToString(m_OScarat);

                                lblOsPcs.Text = Val.ToString(m_OsPcs);
                                lblOsCarat.Text = Val.ToString(m_OScarat);

                                m_RejPcs = Val.ToInt(dtIssOS.Rows[0]["rejection_pcs"]);
                                m_RejCarat = Val.ToDecimal(dtIssOS.Rows[0]["rejection_carat"]);

                                m_OutPcs = Val.ToInt(dtIssOS.Rows[0]["out_pcs"]);
                                m_OutCarat = Val.ToDecimal(dtIssOS.Rows[0]["out_carat"]);

                                m_RecPcs = Val.ToInt(dtIssOS.Rows[0]["balance_pcs"]);
                                m_RecCarat = Val.ToDecimal(dtIssOS.Rows[0]["balance_carat"]);

                                m_ResoingPcs = Val.ToInt(dtIssOS.Rows[0]["resoing_pcs"]);
                                m_ResoingCarat = Val.ToDecimal(dtIssOS.Rows[0]["resoing_carat"]);

                                //m_BreakPcs = Val.ToInt(dtIssOS.Rows[0]["breakage_pcs"]);
                                //m_BreakCarat = Val.ToDecimal(dtIssOS.Rows[0]["breakage_carat"]);

                                txtIssuePcs.Text = Val.ToString(m_RecPcs);
                                txtOutpcs.Text = Val.ToString(m_OutPcs);
                                txtReSoingPcs.Text = Val.ToString(m_ResoingPcs);
                                //txtBreakPcs.Text = Val.ToString(m_BreakPcs);
                                txtRejPcs.Text = Val.ToString(m_RejPcs);

                                txtIssCarat.Text = Val.ToString(m_RecCarat);
                                txtOutCarat.Text = Val.ToString(m_OutCarat);
                                txtResoingCarat.Text = Val.ToString(m_ResoingCarat);
                                //txtBreakCarat.Text = Val.ToString(m_BreakCarat);
                                txtRejCarat.Text = Val.ToString(m_RejCarat);
                            }
                            else
                            {
                                Global.Message("Already Issue this Lot");
                                return;
                            }
                        }

                        if (lueProcess.Text == "CHIPIYO FINAL")
                        {
                            lueclarity.EditValue = Val.ToInt(m_dtbParam.Rows[0]["rough_clarity_id"]);
                            lueSieve.EditValue = Val.ToInt(m_dtbParam.Rows[0]["rough_sieve_id"]);
                            lueQuality.EditValue = Val.ToInt(m_dtbParam.Rows[0]["quality_id"]);
                            lueclarity.Text = Val.ToString(m_dtbParam.Rows[0]["rough_clarity_name"]);
                            lueSieve.Text = Val.ToString(m_dtbParam.Rows[0]["sieve_name"]);
                            lueQuality.Text = Val.ToString(m_dtbParam.Rows[0]["quality_name"]);
                            luePurity.EditValue = Val.ToInt(m_dtbParam.Rows[0]["purity_name"]);
                            txtRate.Text = Val.ToString(Math.Round(m_os_rate, 2));
                        }
                        else if (lueProcess.Text == "SHINE ISSUE")
                        {
                            txtRate.Text = Val.ToString(dtAssortRate.Rows[0]["rate"]);
                            lueclarity.EditValue = Val.ToInt(dtAssortRate.Rows[0]["rough_clarity_id"]);
                            lueSieve.EditValue = Val.ToInt(dtAssortRate.Rows[0]["rough_sieve_id"]);
                            lueQuality.Visible = true;
                            lueQuality.EditValue = Val.ToInt(dtAssortRate.Rows[0]["quality_id"]);
                            lueQuality.Text = Val.ToString(dtAssortRate.Rows[0]["quality_name"]);
                            lueQuality.Visible = false;
                            luePurity.EditValue = Val.ToInt(dtAssortRate.Rows[0]["purity_id"]);
                        }
                        else if (lueProcess.Text == "POLISH REPAIRING")
                        {
                            lueclarity.EditValue = Val.ToInt(m_dtbParam.Rows[0]["rough_clarity_id"]);
                            lueSieve.EditValue = Val.ToInt(m_dtbParam.Rows[0]["rough_sieve_id"]);
                            lueQuality.EditValue = Val.ToInt(m_dtbParam.Rows[0]["quality_id"]);
                            luePurity.EditValue = Val.ToInt(m_dtbParam.Rows[0]["purity_id"]);
                            lueQuality.Text = Val.ToString(m_dtbParam.Rows[0]["quality_name"]);
                            lueclarity.Text = m_dtbParam.Rows[0]["rough_clarity_name"].ToString();
                            lueSieve.Text = Val.ToString(m_dtbParam.Rows[0]["sieve_name"]);
                            luePurity.EditValue = Val.ToString(m_dtbParam.Rows[0]["purity_name"]);
                            txtRate.Text = Val.ToString(Math.Round(m_os_rate, 2));
                        }
                        else
                        {
                            lueclarity.EditValue = Val.ToInt(m_dtbParam.Rows[0]["rough_clarity_id"]);
                            lueSieve.EditValue = Val.ToInt(m_dtbParam.Rows[0]["rough_sieve_id"]);
                            lueQuality.EditValue = Val.ToInt(m_dtbParam.Rows[0]["quality_id"]);
                            luePurity.EditValue = Val.ToInt(m_dtbParam.Rows[0]["purity_id"]);
                            lueQuality.Text = Val.ToString(m_dtbParam.Rows[0]["quality_name"]);
                            lueclarity.Text = m_dtbParam.Rows[0]["rough_clarity_name"].ToString();
                            lueSieve.Text = Val.ToString(m_dtbParam.Rows[0]["sieve_name"]);
                            luePurity.EditValue = Val.ToString(m_dtbParam.Rows[0]["purity_name"]);
                            txtRate.Text = Val.ToString(Math.Round(m_os_rate, 2));
                        }
                        if (lueSubProcess.Text == "MANUAL OPERATOR")
                        {
                            //lblOsPcs.Text = Val.ToString(m_RejPcs);
                            //lblOsCarat.Text = Val.ToString(m_RejCarat);

                            txtRejPcs.Text = Val.ToString(m_RejPcs);
                            txtRejCarat.Text = Val.ToString(m_RejCarat);
                        }
                    }
                    else
                    {
                        txtIssCarat.Text = "0";
                        txtIssuePcs.Text = "0";
                        lblOsPcs.Text = "0";
                        lblOsCarat.Text = "0.00";
                    }
                }
                else
                {
                    lblOsPcs.Text = "0";
                    lblOsCarat.Text = "0.00";
                    txtLotID.Text = "";
                    txtIssCarat.Text = "0";
                    txtIssuePcs.Text = "0";
                    m_IsLot = 0;
                    return;
                }

                m_IsLot = 0;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void lueEmployee_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmEmployeeMaster frmEmployee = new FrmEmployeeMaster();
                frmEmployee.ShowDialog();
                Global.LOOKUPEmp(lueEmployee);
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
        private void lueclarity_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmMfgRoughClarityMaster frmRoughClarity = new FrmMfgRoughClarityMaster();
                frmRoughClarity.ShowDialog();
                Global.LOOKUPClarity(lueclarity);
            }
        }
        private void btnPopUpStock_Click(object sender, EventArgs e)
        {
            GetPendingStock();
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
        private void lueSieve_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmMfgRoughSieve frmRoughSieve = new FrmMfgRoughSieve();
                frmRoughSieve.ShowDialog();
                Global.LOOKUPRoughSieve(lueSieve);
            }
        }
        private void luePurity_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmClarityMaster frmPurity = new FrmClarityMaster();
                frmPurity.ShowDialog();
                Global.LOOKUPPurity(luePurity);
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
        private void txtIssCarat_EditValueChanged(object sender, EventArgs e)
        {
            if (Val.ToDecimal(txtIssCarat.Text) != 0 && Val.ToDecimal(txtRate.Text) != 0)
            {
                decimal amount = Val.ToDecimal(txtIssCarat.Text) * Val.ToDecimal(txtRate.Text);
                txtAmount.Text = Val.ToString(amount);
            }
            else
            {
                txtAmount.Text = Val.ToString("0");
            }
        }
        private void txtRate_EditValueChanged(object sender, EventArgs e)
        {
            if (Val.ToDecimal(txtIssCarat.Text) != 0 && Val.ToDecimal(txtRate.Text) != 0)
            {
                decimal amount = Math.Round(Val.ToDecimal(txtIssCarat.Text) * Val.ToDecimal(txtRate.Text), 4);
                txtAmount.Text = Val.ToString(amount);
            }
            else
            {
                txtAmount.Text = Val.ToString("0");
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
                if (m_IsLot == 1)
                {
                    m_dtbParam = Global.GetRoughKapanWise(0, Val.ToInt64(txtLotID.Text));
                }
                lueCutNo.Properties.DataSource = m_dtbParam;
                lueCutNo.Properties.ValueMember = "rough_cut_id";
                lueCutNo.Properties.DisplayMember = "rough_cut_no";
                if (lueProcess.Text == "CHIPIYO FINAL")
                {
                    txtRate.Text = Val.ToString(Math.Round(m_os_rate, 2));
                }
                else if (lueProcess.Text == "SHINE ISSUE")
                {
                    txtRate.Text = Val.ToString(Math.Round(m_assort_rate, 2));
                }
                else
                {
                    txtRate.Text = "0";
                }
            }
        }
        private void backgroundWorker_ProcessIssue_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                MFGProcessIssue objMFGProcessIssue = new MFGProcessIssue();
                MFGProcessIssueProperty objMFGProcessIssueProperty = new MFGProcessIssueProperty();

                Conn = new BeginTranConnection(true, false);

                Issue_IntRes = 0;
                Int64 NewHistory_Union_Id = 0;

                int IntCounter = 0;
                int Count = 0;

                try
                {
                    int TotalCount = m_dtbIssueProcess.Rows.Count;

                    foreach (DataRow drw in m_dtbIssueProcess.Rows)
                    {
                        objMFGProcessIssueProperty.Issue_id = Val.ToInt(drw["issue_id"]);
                        objMFGProcessIssueProperty.rough_cut_id = Val.ToInt(drw["rough_cut_id"]);
                        objMFGProcessIssueProperty.lot_id = Val.ToInt(drw["lot_id"]);
                        objMFGProcessIssueProperty.issue_date = Val.DBDate(dtpIssueDate.Text);// Val.DBDate(Val.ToString(drw["issue_date"]));
                        objMFGProcessIssueProperty.manager_id = Val.ToInt(drw["manager_id"]);
                        objMFGProcessIssueProperty.employee_id = Val.ToInt(drw["employee_id"]);

                        objMFGProcessIssueProperty.process_id = Val.ToInt(lueProcess.EditValue); //Val.ToInt(drw["process_id"]);
                        objMFGProcessIssueProperty.sub_process_id = Val.ToInt(lueSubProcess.EditValue); //Val.ToInt(drw["sub_process_id"]);
                        objMFGProcessIssueProperty.quality_id = Val.ToInt(drw["quality_id"]);
                        objMFGProcessIssueProperty.rough_clarity_id = Val.ToInt(drw["rough_clarity_id"]);

                        objMFGProcessIssueProperty.rough_sieve_id = Val.ToInt(drw["rough_sieve_id"]);
                        objMFGProcessIssueProperty.purity_id = Val.ToInt(drw["purity_id"]);

                        objMFGProcessIssueProperty.pcs = Val.ToInt(drw["pcs"]);
                        objMFGProcessIssueProperty.carat = Val.ToDecimal(drw["carat"]);
                        objMFGProcessIssueProperty.rate = Val.ToDecimal(drw["rate"]);
                        objMFGProcessIssueProperty.amount = Val.ToDecimal(drw["amount"]);
                        objMFGProcessIssueProperty.prd_id = Val.ToInt(drw["prd_id"]);
                        objMFGProcessIssueProperty.form_id = Val.ToInt(m_numForm_id);
                        //objMFGProcessIssueProperty.union_id = IntRes;
                        objMFGProcessIssueProperty.issue_union_id = Issue_IntRes;
                        objMFGProcessIssueProperty.kapan_id = Val.ToInt(drw["kapan_id"]);
                        objMFGProcessIssueProperty.rejection_pcs = Val.ToInt(drw["rejection_pcs"]);
                        objMFGProcessIssueProperty.rejection_carat = Val.ToDecimal(drw["rejection_carat"]);
                        objMFGProcessIssueProperty.rr_pcs = Val.ToInt(drw["rr_pcs"]);
                        objMFGProcessIssueProperty.rr_carat = Val.ToDecimal(drw["rr_carat"]);
                        objMFGProcessIssueProperty.resoing_pcs = Val.ToInt(drw["resoing_pcs"]);
                        objMFGProcessIssueProperty.resoing_carat = Val.ToDecimal(drw["resoing_carat"]);
                        objMFGProcessIssueProperty.breakage_pcs = Val.ToInt(drw["breakage_pcs"]);
                        objMFGProcessIssueProperty.breakage_carat = Val.ToDecimal(drw["breakage_carat"]);

                        m_old_carat = Val.ToDecimal(drw["carat"]);
                        objMFGProcessIssueProperty.history_union_id = NewHistory_Union_Id;
                        objMFGProcessIssueProperty = objMFGProcessIssue.Issue_Factory_Save(objMFGProcessIssueProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                        //IntRes = objMFGProcessIssueProperty.union_id;
                        Issue_IntRes = objMFGProcessIssueProperty.issue_union_id;
                        NewHistory_Union_Id = Val.ToInt64(objMFGProcessIssueProperty.history_union_id);

                        Count++;
                        IntCounter++;
                        SetControlPropertyValue(lblProgressCount, "Text", Count.ToString() + "" + "/" + "" + TotalCount.ToString() + " Completed....");
                    }
                    Conn.Inter1.Commit();
                }
                catch (Exception ex)
                {
                    Issue_IntRes = -1;
                    Conn.Inter1.Rollback();
                    Conn = null;
                    General.ShowErrors(ex.ToString());
                    btnSave.Enabled = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                Issue_IntRes = -1;
                Conn.Inter1.Rollback();
                Conn = null;
                Global.Message(ex.ToString());
                btnSave.Enabled = true;
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
        private void backgroundWorker_ProcessIssue_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                panelProgress.Visible = false;
                if (Issue_IntRes > 0)
                {
                    Global.Confirm("Process Issue Data Save Succesfully");
                    ClearDetails();
                }
                else
                {
                    Global.Confirm("Error In Process Issue");
                    btnSave.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
                btnSave.Enabled = true;
            }
        }
        private void txtPcs_EditValueChanged(object sender, EventArgs e)
        {
            CalculateTotal();
        }
        private void txtRejPcs_EditValueChanged(object sender, EventArgs e)
        {
            CalculateTotal();
        }
        private void txtOutpcs_EditValueChanged(object sender, EventArgs e)
        {
            CalculateTotal();
        }
        private void txtReSoingPcs_EditValueChanged(object sender, EventArgs e)
        {
            CalculateTotal();
        }
        private void txtBreakPcs_EditValueChanged(object sender, EventArgs e)
        {
            CalculateTotal();
        }

        #region GridEvents
        private void grvProcessIssue_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                //if (e.RowHandle >= 0)
                //{
                //    if (e.Clicks == 2)
                //    {
                //        DataRow Drow = grvProcessIssue.GetDataRow(e.RowHandle);
                //        btnAdd.Text = "&Update";
                //        lueCutNo.Text = Val.ToString(Drow["rough_cut_no"]);
                //        lueCutNo.EditValue = Val.ToInt(Drow["rough_cut_id"]);
                //        txtLotID.Text = Val.ToString(Drow["lot_id"]);
                //        txtIssuePcs.Text = Val.ToString(Drow["pcs"]);
                //        txtIssCarat.Text = Val.ToString(Drow["carat"]);
                //        txtRate.Text = Val.ToString(Drow["rate"]);
                //        txtAmount.Text = Val.ToString(Drow["amount"]);
                //        lueManager.EditValue = Val.ToInt(Drow["manager_id"]);
                //        lueProcess.EditValue = Val.ToInt(Drow["process_id"]);
                //        lueSubProcess.EditValue = Val.ToInt(Drow["sub_process_id"]);
                //        lueSieve.EditValue = Val.ToInt(Drow["rough_sieve_id"]);
                //        luePurity.EditValue = Val.ToInt(Drow["purity_id"]);
                //        lueQuality.EditValue = Val.ToInt(Drow["quality_name"]);
                //        lueclarity.EditValue = Val.ToInt(Drow["rough_clarity_id"]);
                //        txtRejPcs.Text = Val.ToString(Drow["rejection_pcs"]);
                //        txtRejCarat.Text = Val.ToString(Drow["rejection_carat"]);
                //        txtOutpcs.Text = Val.ToString(Drow["rr_pcs"]);
                //        txtOutCarat.Text = Val.ToString(Drow["rr_carat"]);
                //        txtReSoingPcs.Text = Val.ToString(Drow["resoing_pcs"]);
                //        txtResoingCarat.Text = Val.ToString(Drow["resoing_carat"]);
                //        txtBreakPcs.Text = Val.ToString(Drow["breakage_pcs"]);
                //        txtBreakCarat.Text = Val.ToString(Drow["breakage_carat"]);
                //        m_numcarat = Val.ToDecimal(Drow["carat"]);
                //        m_cut_no = Val.ToString(Drow["rough_cut_no"]);
                //        m_update_srno = Val.ToInt(Drow["sr_no"]);
                //        m_old_carat = Val.ToDecimal(Drow["carat"]);
                //        m_old_rate = Val.ToDecimal(Drow["rate"]);
                //        m_old_amount = Val.ToDecimal(Drow["amount"]);
                //        m_flag = Val.ToInt(1);
                //        m_blnflag = true;
                //    }
                //}
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
                    if (GlobalDec.gEmployeeProperty.role_name == "SURAT 4P" || GlobalDec.gEmployeeProperty.role_name == "SURAT 4P OK" || GlobalDec.gEmployeeProperty.role_name == "SURAT SARIN" || GlobalDec.gEmployeeProperty.role_name == "SURAT SARIN ENTRY" || GlobalDec.gEmployeeProperty.role_name == "SURAT 4P ENTRY" || GlobalDec.gEmployeeProperty.role_name == "SURAT RUSSIAN" || GlobalDec.gEmployeeProperty.role_name == "SURAT POLISH REPARING" || GlobalDec.gEmployeeProperty.role_name == "SURAT MAKABLE" || GlobalDec.gEmployeeProperty.role_name == "SURAT KATARGAM")
                    {
                        MFGProcessIssue objMFGProcessIssue = new MFGProcessIssue();
                        MFGProcessIssueProperty objMFGProcessIssueProperty = new MFGProcessIssueProperty();

                        objMFGProcessIssueProperty.lot_id = Val.ToInt64(txtLotID.Text);
                        objMFGProcessIssueProperty.process_id = Val.ToInt32(lueProcess.EditValue);
                        objMFGProcessIssueProperty.sub_process_id = Val.ToInt32(lueSubProcess.EditValue);
                        objMFGProcessIssueProperty = objMFGProcessIssue.GetData_PrevProcess(objMFGProcessIssueProperty);

                        if (objMFGProcessIssueProperty.Messgae != "" && objMFGProcessIssueProperty.Messgae != null)
                        {
                            Global.Message(objMFGProcessIssueProperty.Messgae);
                            return blnReturn;
                        }
                    }

                    DataRow[] dr = m_dtbIssueProcess.Select("lot_id = '" + Val.ToString(txtLotID.Text) + "' and employee_id = '" + Val.ToInt32(lueEmployee.EditValue) + "'");

                    if (dr.Count() == 1)
                    {
                        Global.Message("Record already exists in grid", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lueManager.Focus();
                        blnReturn = false;
                        return blnReturn;
                    }
                    //DataRow[] drCut = m_dtbIssueProcess.Select("rough_cut_no <> '" + Val.ToString(lueCutNo.Text) + "'");
                    //if (drCut.Count() > 1)
                    //{
                    //    Global.Message("Different cut selected please check", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    lueManager.Focus();
                    //    blnReturn = false;
                    //    return blnReturn;
                    //}

                    DataRow drwNew = m_dtbIssueProcess.NewRow();
                    int numPcs = Val.ToInt(txtIssuePcs.Text);
                    decimal numCarat = Val.ToDecimal(txtIssCarat.Text);
                    decimal numRate = Val.ToDecimal(m_os_rate);
                    decimal numAmount = Val.ToDecimal(txtAmount.Text);
                    int numorgPcs = Val.ToInt(lblOsPcs.Text);
                    decimal numorgCarat = Val.ToDecimal(lblOsCarat.Text);

                    drwNew["issue_id"] = Val.ToInt(0);
                    drwNew["issue_date"] = Val.DBDate(dtpIssueDate.Text);
                    drwNew["rough_cut_no"] = Val.ToString(lueCutNo.Text);
                    drwNew["rough_cut_id"] = Val.ToString(lueCutNo.EditValue);
                    drwNew["lot_id"] = Val.ToInt64(txtLotID.Text);
                    drwNew["manager_id"] = Val.ToInt(lueManager.EditValue);
                    drwNew["manager"] = Val.ToString(manager_short_name); //Val.ToString(lueManager.Text);

                    drwNew["employee_id"] = Val.ToInt(lueEmployee.EditValue);
                    drwNew["employee"] = Val.ToString(lueEmployee.Text);
                    drwNew["process_id"] = Val.ToInt(lueProcess.EditValue);
                    drwNew["process"] = Val.ToString(lueProcess.Text);
                    drwNew["sub_process_id"] = Val.ToInt(lueSubProcess.EditValue);
                    drwNew["subprocess"] = Val.ToString(lueSubProcess.Text);
                    drwNew["rough_sieve_id"] = Val.ToInt(lueSieve.EditValue);
                    drwNew["sieve_name"] = Val.ToString(lueSieve.Text);
                    drwNew["purity_id"] = Val.ToInt(luePurity.EditValue);
                    drwNew["purity_name"] = Val.ToString(luePurity.Text);
                    drwNew["quality_id"] = Val.ToInt32(lueQuality.EditValue);
                    drwNew["quality_name"] = Val.ToString(lueQuality.Text);
                    drwNew["rough_clarity_id"] = Val.ToInt(lueclarity.EditValue);
                    drwNew["rough_clarity_name"] = Val.ToString(lueclarity.Text);
                    drwNew["prd_id"] = m_prd_id;
                    drwNew["pcs"] = numPcs;
                    drwNew["carat"] = numCarat;
                    drwNew["rate"] = numRate;
                    drwNew["amount"] = numAmount;
                    drwNew["sr_no"] = m_Srno;
                    drwNew["kapan_id"] = m_kapan_id;
                    drwNew["rr_pcs"] = Val.ToInt(txtOutpcs.Text);
                    drwNew["rr_carat"] = Val.ToDecimal(txtOutCarat.Text);
                    drwNew["rejection_pcs"] = Val.ToInt(txtRejPcs.Text);
                    drwNew["rejection_carat"] = Val.ToDecimal(txtRejCarat.Text);
                    drwNew["resoing_pcs"] = Val.ToInt(txtReSoingPcs.Text);
                    drwNew["resoing_carat"] = Val.ToDecimal(txtResoingCarat.Text);
                    drwNew["breakage_pcs"] = Val.ToInt(txtBreakPcs.Text);
                    drwNew["breakage_carat"] = Val.ToDecimal(txtBreakCarat.Text);
                    drwNew["org_pcs"] = numorgPcs;
                    drwNew["org_carat"] = numorgCarat;

                    m_dtbIssueProcess.Rows.Add(drwNew);
                    m_Srno++;
                }
                //else if (btnAdd.Text == "&Update")
                //{

                //    if (m_dtbIssueProcess.Select("sr_no ='" + Val.ToInt(m_update_srno) + "'").Length > 0)
                //    {
                //        for (int i = 0; i < m_dtbIssueProcess.Rows.Count; i++)
                //        {
                //            if (m_dtbIssueProcess.Select("rough_cut_no ='" + m_cut_no + "' AND sr_no = '" + m_update_srno + "'").Length > 0)
                //            {
                //                if (m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["rough_cut_no"].ToString() == m_cut_no.ToString())
                //                {
                //                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["lot_id"] = Val.ToString(txtLotID.Text);
                //                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["manager_id"] = Val.ToInt(lueManager.EditValue);
                //                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["manager"] = Val.ToString(lueManager.Text);
                //                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["employee_id"] = Val.ToInt(lueEmployee.EditValue);
                //                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["employee"] = Val.ToString(lueEmployee.Text);
                //                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["process_id"] = Val.ToInt(lueProcess.EditValue);
                //                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["process"] = Val.ToString(lueProcess.Text);
                //                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["sub_process_id"] = Val.ToInt(lueSubProcess.EditValue);
                //                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["subprocess"] = Val.ToString(lueSubProcess.Text);
                //                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["rough_sieve_id"] = Val.ToInt(lueSieve.EditValue);
                //                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["sieve_name"] = Val.ToString(lueSieve.Text);
                //                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["purity_id"] = Val.ToInt(luePurity.EditValue);
                //                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["purity_name"] = Val.ToString(luePurity.Text);
                //                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["quality_id"] = Val.ToInt(lueQuality.EditValue);
                //                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["quality_name"] = Val.ToString(lueQuality.Text);
                //                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["rough_clarity_id"] = Val.ToInt(lueclarity.EditValue);
                //                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["rough_clarity_name"] = Val.ToString(lueclarity.Text);
                //                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["pcs"] = Val.ToInt(txtIssuePcs.Text).ToString();
                //                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["carat"] = Val.ToDecimal(txtIssCarat.Text).ToString();
                //                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["rate"] = Val.ToDecimal(txtRate.Text).ToString();
                //                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["amount"] = Val.ToDecimal(txtAmount.Text).ToString();
                //                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["prd_id"] = Val.ToInt(m_prd_id);
                //                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["kapan_id"] = Val.ToInt(m_kapan_id);
                //                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["rr_pcs"] = Val.ToInt(txtOutpcs.Text);
                //                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["rr_carat"] = Val.ToDecimal(txtOutCarat.Text);
                //                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["rejection_pcs"] = Val.ToInt(txtRejPcs.Text);
                //                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["rejection_carat"] = Val.ToDecimal(txtRejCarat.Text);
                //                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["resoing_pcs"] = Val.ToInt(txtReSoingPcs.Text);
                //                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["resoing_carat"] = Val.ToDecimal(txtResoingCarat.Text);
                //                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["breakage_pcs"] = Val.ToInt(txtBreakPcs.Text);
                //                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["breakage_carat"] = Val.ToDecimal(txtBreakCarat.Text);
                //                    m_flag = Val.ToInt(0);
                //                    break;
                //                }
                //            }
                //        }
                //    }
                //    btnAdd.Text = "&Add";
                //}
                grvProcessIssue.MoveLast();
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
                    if (m_dtbIssueProcess.Rows.Count == 0)
                    {
                        lstError.Add(new ListError(22, "Record"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                        }
                    }

                    if (GlobalDec.gEmployeeProperty.role_name.ToUpper() == "SURAT KAMALA" || GlobalDec.gEmployeeProperty.role_name.ToUpper() == "KAMALA ADMIN")
                    {
                        DateTime endDate = Convert.ToDateTime(DateTime.Today);
                        endDate = endDate.AddDays(15);

                        if (Convert.ToDateTime(dtpIssueDate.Text) >= endDate)
                        {
                            lstError.Add(new ListError(5, " Issue Date Not Be Permission After 15 Days Issue this Lot ID...Please Contact to Administrator"));
                            if (!blnFocus)
                            {
                                blnFocus = true;
                                dtpIssueDate.Focus();
                            }
                        }
                    }
                    else
                    {
                        DateTime endDate = Convert.ToDateTime(DateTime.Today);
                        endDate = endDate.AddDays(3);

                        if (Convert.ToDateTime(dtpIssueDate.Text) >= endDate)
                        {
                            lstError.Add(new ListError(5, " Issue Date Not Be Permission After 3 Days Issue this Lot ID...Please Contact to Administrator"));
                            if (!blnFocus)
                            {
                                blnFocus = true;
                                dtpIssueDate.Focus();
                            }
                        }
                    }

                    //var result = DateTime.Compare(Convert.ToDateTime(dtpIssueDate.Text), DateTime.Today);
                    //if (result > 0)
                    //{
                    //    lstError.Add(new ListError(5, " Issue Date Not Be Greater Than Today Date"));
                    //    if (!blnFocus)
                    //    {
                    //        blnFocus = true;
                    //        dtpIssueDate.Focus();
                    //    }
                    //}
                    if (Val.ToString(dtpIssueDate.Text) == string.Empty)
                    {
                        lstError.Add(new ListError(22, "Date"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            dtpIssueDate.Focus();
                        }
                    }

                    // Add Validation By Praful On 06112020

                    int tmpOrgPcs = 0;
                    decimal tmpOrgCarat = 0;

                    int tmpReceivePcs = 0;

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

                    for (int i = 0; i < m_dtbIssueProcess.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            tmpOrgPcs = Val.ToInt32(m_dtbIssueProcess.Rows[i]["org_pcs"].ToString());
                        }

                        if (i > 0)
                        {
                            if (m_dtbIssueProcess.Rows[i]["lot_id"].ToString() != m_dtbIssueProcess.Rows[i - 1]["lot_id"].ToString())
                            {
                                if (tmpOrgPcs - (tmpReceivePcs + tmpRRPcs + tmpRejPcs + tmpBreakagePcs + tmpReSawingPcs + tmpLostPcs) > 0)
                                {
                                    Global.Message("Pcs not match " + m_dtbIssueProcess.Rows[i]["lot_id"].ToString());
                                    return false;
                                }
                                if (tmpOrgPcs - (tmpReceivePcs + tmpRRPcs + tmpRejPcs + tmpBreakagePcs + tmpReSawingPcs + tmpLostPcs) < 0)
                                {
                                    Global.Message("Pcs not match " + m_dtbIssueProcess.Rows[i - 1]["lot_id"].ToString());
                                    return false;
                                }

                                tmpOrgPcs = Val.ToInt32(m_dtbIssueProcess.Rows[i]["org_pcs"].ToString());

                                tmpReceivePcs = 0;
                                tmpRRPcs = 0;
                                tmpRejPcs = 0;
                                tmpBreakagePcs = 0;
                                tmpReSawingPcs = 0;
                                tmpLostPcs = 0;
                            }
                        }

                        tmpReceivePcs = tmpReceivePcs + Val.ToInt32(m_dtbIssueProcess.Rows[i]["pcs"].ToString());

                        tmpRRPcs = tmpRRPcs + Val.ToInt32(m_dtbIssueProcess.Rows[i]["rr_pcs"].ToString());

                        tmpRejPcs = tmpRejPcs + Val.ToInt32(m_dtbIssueProcess.Rows[i]["rejection_pcs"].ToString());

                        tmpBreakagePcs = tmpBreakagePcs + Val.ToInt32(m_dtbIssueProcess.Rows[i]["breakage_pcs"].ToString());

                        tmpReSawingPcs = tmpReSawingPcs + Val.ToInt32(m_dtbIssueProcess.Rows[i]["resoing_pcs"].ToString());

                        //tmpLostPcs = tmpLostPcs + Val.ToInt32(m_dtbIssueProcess.Rows[i]["lost_pcs"].ToString());

                        if (i == 0)
                        {
                            if (i == (m_dtbIssueProcess.Rows.Count - 1))
                            {
                                if (tmpOrgPcs - (tmpReceivePcs + tmpRRPcs + tmpRejPcs + tmpBreakagePcs + tmpReSawingPcs + tmpLostPcs) > 0)
                                {
                                    Global.Message("Pcs not match : Lot ID =  " + m_dtbIssueProcess.Rows[i]["lot_id"].ToString());
                                    return false;
                                }
                                if (tmpOrgPcs - (tmpReceivePcs + tmpRRPcs + tmpRejPcs + tmpBreakagePcs + tmpReSawingPcs + tmpLostPcs) < 0)
                                {
                                    Global.Message("Pcs not match : Lot ID = " + m_dtbIssueProcess.Rows[i]["lot_id"].ToString());
                                    return false;
                                }

                                tmpReceivePcs = 0;
                                tmpRRPcs = 0;

                                tmpRejPcs = 0;

                                tmpBreakagePcs = 0;

                                tmpReSawingPcs = 0;

                                tmpLostPcs = 0;
                            }
                        }
                        if (i > 0)
                        {
                            if (i == (m_dtbIssueProcess.Rows.Count - 1))
                            {
                                if (tmpOrgPcs - (tmpReceivePcs + tmpRRPcs + tmpRejPcs + tmpBreakagePcs + tmpReSawingPcs + tmpLostPcs) > 0)
                                {
                                    Global.Message("Pcs not match : Lot ID = " + m_dtbIssueProcess.Rows[i]["lot_id"].ToString());
                                    return false;
                                }
                                if (tmpOrgPcs - (tmpReceivePcs + tmpRRPcs + tmpRejPcs + tmpBreakagePcs + tmpReSawingPcs + tmpLostPcs) < 0)
                                {
                                    Global.Message("Pcs not match : Lot ID = " + m_dtbIssueProcess.Rows[i - 1]["lot_id"].ToString());
                                    return false;
                                }

                                tmpReceivePcs = 0;
                                tmpRRPcs = 0;
                                tmpRejPcs = 0;
                                tmpBreakagePcs = 0;
                                tmpReSawingPcs = 0;
                                tmpLostPcs = 0;
                            }
                        }
                    }

                    tmpOrgPcs = 0;
                    tmpOrgCarat = 0;
                    tmpReceivePcs = 0;
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
                    tmpOrgCarat = 0;
                    decimal tmpIssueCarat = 0;
                    tmpRRCarat = 0;
                    tmpRejCarat = 0;
                    tmpBreakageCarat = 0;
                    tmpReSawingCarat = 0;
                    tmpLostCarat = 0;
                    decimal Diff_Carat = 0;

                    for (int i = 0; i < m_dtbIssueProcess.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            tmpOrgCarat = Val.ToDecimal(m_dtbIssueProcess.Rows[i]["org_carat"].ToString());
                        }

                        if (i > 0)
                        {
                            if (m_dtbIssueProcess.Rows[i]["lot_id"].ToString() != m_dtbIssueProcess.Rows[i - 1]["lot_id"].ToString())
                            {
                                if (tmpOrgCarat - (tmpIssueCarat + tmpRRCarat + tmpRejCarat + tmpBreakageCarat + tmpReSawingCarat) < 0)
                                {
                                    Diff_Carat = Val.ToDecimal(Math.Abs(tmpOrgCarat - (tmpIssueCarat + tmpRRCarat + tmpRejCarat + tmpBreakageCarat + tmpReSawingCarat)));

                                    if (Diff_Carat > Val.ToDecimal(0.05))
                                    {
                                        lstError.Add(new ListError(5, "Diff Carat Not Greater Then 0.05"));
                                        if (!blnFocus)
                                        {
                                            blnFocus = true;
                                            Diff_Carat = 0;
                                        }
                                    }
                                    else
                                    {
                                        //m_dtbIssueProcess.Rows[i - 1]["carat_plus"]
                                        m_dtbIssueProcess.Rows[i - 1]["carat"] = tmpIssueCarat - Diff_Carat;
                                        Diff_Carat = 0;
                                    }
                                }
                                else
                                {
                                    Diff_Carat = Val.ToDecimal(Math.Abs(tmpOrgCarat - (tmpIssueCarat + tmpRRCarat + tmpRejCarat + tmpBreakageCarat + tmpReSawingCarat)));

                                    if (Diff_Carat > Val.ToDecimal(0.05))
                                    {
                                        lstError.Add(new ListError(5, "Diff Carat Not Greater Then 0.05"));
                                        if (!blnFocus)
                                        {
                                            blnFocus = true;
                                            Diff_Carat = 0;
                                        }
                                    }
                                    else
                                    {
                                        //m_dtbIssueProcess.Rows[i - 1]["loss_carat"]
                                        m_dtbIssueProcess.Rows[i - 1]["carat"] = tmpIssueCarat + Diff_Carat;
                                        Diff_Carat = 0;
                                    }
                                }

                                tmpOrgCarat = Val.ToDecimal(m_dtbIssueProcess.Rows[i]["org_carat"].ToString());

                                tmpIssueCarat = 0;
                                tmpRRCarat = 0;
                                tmpRejCarat = 0;
                                tmpBreakageCarat = 0;
                                tmpReSawingCarat = 0;
                                Diff_Carat = 0;
                            }
                        }
                        tmpIssueCarat = tmpIssueCarat + Val.ToDecimal(m_dtbIssueProcess.Rows[i]["carat"].ToString());
                        tmpRRCarat = tmpRRCarat + Val.ToDecimal(m_dtbIssueProcess.Rows[i]["rr_carat"].ToString());
                        tmpRejCarat = tmpRejCarat + Val.ToDecimal(m_dtbIssueProcess.Rows[i]["rejection_carat"].ToString());
                        tmpBreakageCarat = tmpBreakageCarat + Val.ToDecimal(m_dtbIssueProcess.Rows[i]["breakage_carat"].ToString());
                        tmpReSawingCarat = tmpReSawingCarat + Val.ToDecimal(m_dtbIssueProcess.Rows[i]["resoing_carat"].ToString());

                        if (i == 0)
                        {
                            if (i == (m_dtbIssueProcess.Rows.Count - 1))
                            {
                                if (tmpOrgCarat - (tmpIssueCarat + tmpRRCarat + tmpRejCarat + tmpBreakageCarat + tmpReSawingCarat + tmpLostCarat) < 0)
                                {
                                    Diff_Carat = Val.ToDecimal(Math.Abs(tmpOrgCarat - (tmpIssueCarat + tmpRRCarat + tmpRejCarat + tmpBreakageCarat + tmpReSawingCarat)));

                                    if (Diff_Carat > Val.ToDecimal(0.05))
                                    {
                                        lstError.Add(new ListError(5, "Diff Carat Not Greater Then 0.05"));
                                        if (!blnFocus)
                                        {
                                            blnFocus = true;
                                            Diff_Carat = 0;
                                        }
                                    }
                                    else
                                    {
                                        m_dtbIssueProcess.Rows[i]["carat"] = tmpIssueCarat - Diff_Carat;
                                        //m_dtbIssueProcess.Rows[i]["carat_plus"] = Val.ToDecimal(Math.Abs(tmpOrgCarat - (tmpIssueCarat + tmpRRCarat + tmpRejCarat + tmpBreakageCarat + tmpReSawingCarat)));
                                    }
                                }
                                else
                                {
                                    Diff_Carat = Val.ToDecimal(Math.Abs(tmpOrgCarat - (tmpIssueCarat + tmpRRCarat + tmpRejCarat + tmpBreakageCarat + tmpReSawingCarat)));

                                    if (Diff_Carat > Val.ToDecimal(0.05))
                                    {
                                        lstError.Add(new ListError(5, "Diff Carat Not Greater Then 0.05"));
                                        if (!blnFocus)
                                        {
                                            blnFocus = true;
                                            Diff_Carat = 0;
                                        }
                                    }
                                    else
                                    {
                                        m_dtbIssueProcess.Rows[i]["carat"] = tmpIssueCarat + Diff_Carat;
                                        //m_dtbIssueProcess.Rows[i]["loss_carat"] = Val.ToDecimal(Math.Abs(tmpOrgCarat - (tmpIssueCarat + tmpRRCarat + tmpRejCarat + tmpBreakageCarat + tmpReSawingCarat)));
                                    }
                                }
                                tmpIssueCarat = 0;
                                tmpRRCarat = 0;
                                tmpRejCarat = 0;
                                tmpBreakageCarat = 0;
                                tmpReSawingCarat = 0;
                                Diff_Carat = 0;
                                // Add By Praful On 25122020
                                tmpIssueCarat = tmpIssueCarat + Val.ToDecimal(m_dtbIssueProcess.Rows[i]["carat"].ToString());
                                tmpRRCarat = tmpRRCarat + Val.ToDecimal(m_dtbIssueProcess.Rows[i]["rr_carat"].ToString());
                                tmpRejCarat = tmpRejCarat + Val.ToDecimal(m_dtbIssueProcess.Rows[i]["rejection_carat"].ToString());
                                tmpBreakageCarat = tmpBreakageCarat + Val.ToDecimal(m_dtbIssueProcess.Rows[i]["breakage_carat"].ToString());
                                tmpReSawingCarat = tmpReSawingCarat + Val.ToDecimal(m_dtbIssueProcess.Rows[i]["resoing_carat"].ToString());
                                // End
                            }
                        }
                        if (i > 0)
                        {
                            if (tmpOrgCarat - (tmpIssueCarat + tmpRRCarat + tmpRejCarat + tmpBreakageCarat + tmpReSawingCarat) < 0)
                            {
                                Diff_Carat = Val.ToDecimal(Math.Abs(tmpOrgCarat - (tmpIssueCarat + tmpRRCarat + tmpRejCarat + tmpBreakageCarat + tmpReSawingCarat)));

                                if (Diff_Carat > Val.ToDecimal(0.05))
                                {
                                    lstError.Add(new ListError(5, "Diff Carat Not Greater Then 0.05"));
                                    if (!blnFocus)
                                    {
                                        blnFocus = true;
                                        Diff_Carat = 0;
                                    }
                                }
                                else
                                {
                                    //m_dtbIssueProcess.Rows[i - 1]["carat"] = tmpIssueCarat - Diff_Carat;
                                    m_dtbIssueProcess.Rows[i - 1]["carat_plus"] = Val.ToDecimal(Math.Abs(tmpOrgCarat - (tmpIssueCarat + tmpRRCarat + tmpRejCarat + tmpBreakageCarat + tmpReSawingCarat)));
                                }
                            }
                            else
                            {
                                Diff_Carat = Val.ToDecimal(Math.Abs(tmpOrgCarat - (tmpIssueCarat + tmpRRCarat + tmpRejCarat + tmpBreakageCarat + tmpReSawingCarat)));

                                if (Diff_Carat > Val.ToDecimal(0.05))
                                {
                                    lstError.Add(new ListError(5, "Diff Carat Not Greater Then 0.05"));
                                    if (!blnFocus)
                                    {
                                        blnFocus = true;
                                        Diff_Carat = 0;
                                    }
                                }
                                else
                                {
                                    //m_dtbIssueProcess.Rows[i - 1]["carat"] = tmpIssueCarat + Diff_Carat;
                                    m_dtbIssueProcess.Rows[i - 1]["loss_carat"] = Val.ToDecimal(Math.Abs(tmpOrgCarat - (tmpIssueCarat + tmpRRCarat + tmpRejCarat + tmpBreakageCarat + tmpReSawingCarat)));
                                }
                            }
                            tmpIssueCarat = 0;
                            tmpRRCarat = 0;
                            tmpRejCarat = 0;
                            tmpBreakageCarat = 0;
                            tmpReSawingCarat = 0;
                            Diff_Carat = 0;
                            // Add By Praful On 25122020
                            tmpIssueCarat = tmpIssueCarat + Val.ToDecimal(m_dtbIssueProcess.Rows[i]["carat"].ToString());
                            tmpRRCarat = tmpRRCarat + Val.ToDecimal(m_dtbIssueProcess.Rows[i]["rr_carat"].ToString());
                            tmpRejCarat = tmpRejCarat + Val.ToDecimal(m_dtbIssueProcess.Rows[i]["rejection_carat"].ToString());
                            tmpBreakageCarat = tmpBreakageCarat + Val.ToDecimal(m_dtbIssueProcess.Rows[i]["breakage_carat"].ToString());
                            tmpReSawingCarat = tmpReSawingCarat + Val.ToDecimal(m_dtbIssueProcess.Rows[i]["resoing_carat"].ToString());
                            // End
                        }



                    }
                    // End Validation By Praful On 06112020
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
                    if (lueManager.Text == "")
                    {
                        lstError.Add(new ListError(13, "To Manger"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueManager.Focus();
                        }
                    }
                    if (lueEmployee.Text == "")
                    {
                        lstError.Add(new ListError(13, "Employee"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueEmployee.Focus();
                        }
                    }
                    if (Val.ToDecimal(txtIssCarat.Text) == 0 && Val.ToDecimal(txtOutpcs.Text) == 0 && Val.ToDecimal(txtRejPcs.Text) == 0 && Val.ToDecimal(txtReSoingPcs.Text) == 0 && Val.ToDecimal(txtBreakPcs.Text) == 0)
                    {
                        lstError.Add(new ListError(12, "Carat/Out/Rejection/Resoing/Breakage"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtBalCarat.Focus();
                        }
                    }

                    DataView view = new DataView(m_dtbIssueProcess);
                    DataTable distinctValues = new DataTable();
                    distinctValues = view.ToTable(true, "lot_id");
                    if (distinctValues.Rows.Count > 0)
                    {
                        foreach (DataRow Dr in distinctValues.Rows)
                        {
                            int balance_pcs = 0;
                            int rejection_pcs = 0;
                            int rr_pcs = 0;
                            int resoing_pcs = 0;
                            int breakage_pcs = 0;
                            int total_Pcs = 0;
                            if (m_flag == 0)
                            {
                                balance_pcs = Val.ToInt(m_dtbIssueProcess.Compute("SUM(pcs)", "lot_id =" + Val.ToString(txtLotID.Text) + ""));
                                rejection_pcs = Val.ToInt(m_dtbIssueProcess.Compute("SUM(rejection_pcs)", "lot_id =" + Val.ToString(txtLotID.Text) + ""));
                                rr_pcs = Val.ToInt(m_dtbIssueProcess.Compute("SUM(rr_pcs)", "lot_id =" + Val.ToString(txtLotID.Text) + ""));
                                resoing_pcs = Val.ToInt(m_dtbIssueProcess.Compute("SUM(resoing_pcs)", "lot_id =" + Val.ToString(txtLotID.Text) + ""));
                                breakage_pcs = Val.ToInt(m_dtbIssueProcess.Compute("SUM(breakage_pcs)", "lot_id =" + Val.ToString(txtLotID.Text) + ""));
                                total_Pcs = Val.ToInt(txtIssuePcs.Text) + Val.ToInt(txtRejPcs.Text) + Val.ToInt(txtRRPcs.Text) + Val.ToInt(txtReSoingPcs.Text) + Val.ToInt(txtBreakPcs.Text);
                            }
                            else
                            {
                                balance_pcs = Val.ToInt(m_dtbIssueProcess.Compute("SUM(pcs)", "lot_id =" + Dr["lot_id"] + " AND sr_no <> '" + m_update_srno + "'"));
                                rejection_pcs = Val.ToInt(m_dtbIssueProcess.Compute("SUM(rejection_pcs)", "lot_id =" + Dr["lot_id"] + " AND sr_no <> '" + m_update_srno + "'"));
                                rr_pcs = Val.ToInt(m_dtbIssueProcess.Compute("SUM(rr_pcs)", "lot_id =" + Dr["lot_id"] + " AND sr_no <> '" + m_update_srno + "'"));
                                resoing_pcs = Val.ToInt(m_dtbIssueProcess.Compute("SUM(resoing_pcs)", "lot_id =" + Dr["lot_id"] + " AND sr_no <> '" + m_update_srno + "'"));
                                breakage_pcs = Val.ToInt(m_dtbIssueProcess.Compute("SUM(breakage_pcs)", "lot_id =" + Dr["lot_id"] + " AND sr_no <> '" + m_update_srno + "'"));
                                total_Pcs = Val.ToInt(txtIssuePcs.Text) + Val.ToInt(txtRejPcs.Text) + Val.ToInt(txtRRPcs.Text) + Val.ToInt(txtReSoingPcs.Text) + Val.ToInt(txtBreakPcs.Text);
                            }
                            if ((Val.ToInt(lblOsPcs.Text)) < (Val.ToInt(balance_pcs) + Val.ToInt(rejection_pcs) + Val.ToInt(rr_pcs) + Val.ToInt(resoing_pcs) + Val.ToInt(breakage_pcs) + total_Pcs))
                            {
                                lstError.Add(new ListError(5, "Issue Pcs not equal to balance Pcs in this Lot = " + Val.ToString(txtLotID.Text)));
                                if (!blnFocus)
                                {
                                    blnFocus = true;
                                    txtIssuePcs.Focus();
                                }
                            }
                        }
                    }
                    if ((Val.ToInt(lblOsPcs.Text)) < (Val.ToInt(txtIssuePcs.Text) + Val.ToInt(txtRejPcs.Text) + Val.ToInt(txtRRPcs.Text) + Val.ToInt(txtReSoingPcs.Text) + Val.ToInt(txtBreakPcs.Text)))
                    {
                        lstError.Add(new ListError(5, "Issue Pcs not equal to balance Pcs in this Lot = " + Val.ToString(txtLotID.Text)));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtIssuePcs.Focus();
                        }
                    }

                    //if (Val.ToInt(lblOsPcs.Text) < Val.ToInt(txtIssuePcs.Text) + Val.ToInt(txtRejPcs.Text))
                    //{
                    //    lstError.Add(new ListError(5, "Entry Pcs not greater than total Pcs"));
                    //    if (!blnFocus)
                    //    {
                    //        blnFocus = true;
                    //        txtIssuePcs.Focus();
                    //    }
                    //}

                    //if (Val.ToDecimal(lblOsCarat.Text) < Val.ToDecimal(txtIssCarat.Text) + Val.ToDecimal(txtRejCarat.Text))
                    //{
                    //    lstError.Add(new ListError(5, "Entry Carat not greater than total Carat"));
                    //    if (!blnFocus)
                    //    {
                    //        blnFocus = true;
                    //        txtIssCarat.Focus();
                    //    }
                    //}

                    bool is_Estimation = Val.ToBoolean(((DataRowView)lueProcess.GetSelectedDataRow())["is_estimation"]);

                    if (!is_Estimation)
                    {
                        if (Val.ToDecimal(txtRate.Text) == 0)
                        {
                            lstError.Add(new ListError(12, "Rate"));
                            if (!blnFocus)
                            {
                                blnFocus = true;
                                txtRate.Focus();
                            }
                        }
                        if (Val.ToDecimal(txtAmount.Text) == 0)
                        {
                            lstError.Add(new ListError(12, "Amount"));
                            if (!blnFocus)
                            {
                                blnFocus = true;
                                txtAmount.Focus();
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
                dtpIssueDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpIssueDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpIssueDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpIssueDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpIssueDate.EditValue = DateTime.Now;

                lueKapan.EditValue = System.DBNull.Value;
                lueCutNo.EditValue = System.DBNull.Value;
                lueManager.EditValue = System.DBNull.Value;
                lueEmployee.EditValue = System.DBNull.Value;

                //lueProcess.EditValue = System.DBNull.Value;
                lueSubProcess.EditValue = System.DBNull.Value;

                lueSieve.EditValue = System.DBNull.Value;
                luePurity.EditValue = System.DBNull.Value;
                lueQuality.EditValue = System.DBNull.Value;
                lueclarity.EditValue = System.DBNull.Value;

                //if (GblLockBarcode != "")
                //{
                //    int IntReso = objCutCreate.LockDelete(3, 0, GblLockBarcode); //For Unlock All Lot_ID

                //    if (IntReso > 0)
                //    {
                //        GblLockBarcode = string.Empty;
                //    }
                //}

                txtIssuePcs.Text = string.Empty;
                txtBalCarat.Text = string.Empty;
                txtLotID.Text = string.Empty;
                txtIssCarat.Text = string.Empty;
                txtResoingCarat.Text = string.Empty;
                txtReSoingPcs.Text = string.Empty;
                txtOutCarat.Text = string.Empty;
                txtOutpcs.Text = string.Empty;
                txtRejPcs.Text = string.Empty;
                txtRejCarat.Text = string.Empty;
                txtBreakCarat.Text = string.Empty;
                txtBreakPcs.Text = string.Empty;
                txtRate.Text = string.Empty;
                txtAmount.Text = string.Empty;
                lueProcess.Enabled = false;
                //lueSubProcess.Enabled = true;
                lueEmployee.Enabled = true;
                luePurity.Enabled = true;
                lueQuality.Enabled = true;
                lueSieve.Enabled = true;
                btnPopUpStock.Enabled = false;
                lblOsPcs.Text = "0";
                lblOsCarat.Text = "0.00";
                lblWagesRate.Text = "0.00";
                lblWagesAmt.Text = "0.00";
                lbltxttotalpcs.Text = "0";
                lbltxttotalCarat.Text = "0.00";
                m_flag = 0;
                m_Srno = 1;
                m_update_srno = 0;
                m_old_carat = 0;
                m_kapan_id = 0;
                btnAdd.Text = "&Add";
                m_IsLot = 0;
                if (GlobalDec.gEmployeeProperty.user_name == "4POK")
                {
                    lueSubProcess.EditValue = Val.ToInt32(2024);
                    lueSubProcess.Enabled = false;
                }
                if (GlobalDec.gEmployeeProperty.user_name == "VISHAL4P")
                {
                    lueSubProcess.EditValue = Val.ToInt32(3039);
                }
                else if (GlobalDec.gEmployeeProperty.user_name == "PRINCE")
                {
                    DataTable dtbProcess_Patta = (((DataTable)lueProcess.Properties.DataSource).Copy());
                    dtbProcess_Patta = dtbProcess_Patta.Select("process_name = 'PATTA ASSORT'").CopyToDataTable();
                    lueProcess.EditValue = Val.ToInt(dtbProcess_Patta.Rows[0]["process_id"]);
                    lueProcess.Text = "PATTA ASSORT";
                    lueSubProcess.Text = "PATTA ASSORT";
                }
                else
                {
                    lueSubProcess.EditValue = System.DBNull.Value;
                    lueSubProcess.Enabled = true;
                }
                txtLotID.Focus();
                txtIssuePcs.Text = string.Empty;
                txtRejPcs.Text = string.Empty;
                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                btnSave.Enabled = true;
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

                m_dtbIssueProcess.Columns.Add("issue_id", typeof(int));
                m_dtbIssueProcess.Columns.Add("issue_date", typeof(DateTime));
                m_dtbIssueProcess.Columns.Add("lot_id", typeof(int));
                m_dtbIssueProcess.Columns.Add("rough_cut_id", typeof(int));
                m_dtbIssueProcess.Columns.Add("rough_cut_no", typeof(string));
                m_dtbIssueProcess.Columns.Add("manager", typeof(string));
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

                m_dtbIssueProcess.Columns.Add("pcs", typeof(int)).DefaultValue = 0;
                m_dtbIssueProcess.Columns.Add("carat", typeof(decimal)).DefaultValue = 0;
                m_dtbIssueProcess.Columns.Add("rr_pcs", typeof(int)).DefaultValue = 0;
                m_dtbIssueProcess.Columns.Add("rr_carat", typeof(decimal)).DefaultValue = 0;

                m_dtbIssueProcess.Columns.Add("rejection_pcs", typeof(int)).DefaultValue = 0;
                m_dtbIssueProcess.Columns.Add("rejection_carat", typeof(decimal)).DefaultValue = 0;
                m_dtbIssueProcess.Columns.Add("resoing_pcs", typeof(int)).DefaultValue = 0;
                m_dtbIssueProcess.Columns.Add("resoing_carat", typeof(decimal)).DefaultValue = 0;
                m_dtbIssueProcess.Columns.Add("breakage_pcs", typeof(int)).DefaultValue = 0;
                m_dtbIssueProcess.Columns.Add("breakage_carat", typeof(decimal)).DefaultValue = 0;

                m_dtbIssueProcess.Columns.Add("rate", typeof(decimal)).DefaultValue = 0;
                m_dtbIssueProcess.Columns.Add("amount", typeof(decimal)).DefaultValue = 0;
                m_dtbIssueProcess.Columns.Add("sr_no", typeof(int)).DefaultValue = 0;
                m_dtbIssueProcess.Columns.Add("prd_id", typeof(int)).DefaultValue = 0;
                m_dtbIssueProcess.Columns.Add("kapan_id", typeof(int)).DefaultValue = 0;
                m_dtbIssueProcess.Columns.Add("org_pcs", typeof(int)).DefaultValue = 0;
                m_dtbIssueProcess.Columns.Add("org_carat", typeof(decimal)).DefaultValue = 0;

                m_dtbIssueProcess.Columns.Add("carat_plus", typeof(decimal)).DefaultValue = 0;
                m_dtbIssueProcess.Columns.Add("loss_carat", typeof(decimal)).DefaultValue = 0;

                grdProcessIssue.DataSource = m_dtbIssueProcess;
                grdProcessIssue.Refresh();
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

                if (lotId > 0)
                {
                    dtAssortRate = objMFGProcessIssue.GetAssortRate(lotId, 0);
                    m_dtOutstanding = objSawableRecieve.GetBalanceCarat(lotId, 0);
                }

                if (m_dtOutstanding.Rows.Count > 0)
                {
                    m_balcarat = Val.ToDecimal(m_dtOutstanding.Rows[0]["balance_carat"]);
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
                    if (Val.ToInt(m_dtOutstanding.Rows[0]["sawable_prd_id"]) > 0)
                    {
                        m_prd_id = Val.ToInt(m_dtOutstanding.Rows[0]["sawable_prd_id"]);
                    }
                    if (Val.ToInt(m_dtOutstanding.Rows[0]["janged_prd_id"]) > 0)
                    {
                        m_prd_id = Val.ToInt(m_dtOutstanding.Rows[0]["janged_prd_id"]);
                    }
                    else
                    {
                        m_prd_id = Val.ToInt(0);
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
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
                if (lueManager.Text == "")
                {
                    lstError.Add(new ListError(13, "To Manger"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueManager.Focus();
                    }
                }
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
                    MFGProcessIssue objMFGProcessIssue = new MFGProcessIssue();
                    MFGProcessIssueProperty objMFGProcessIssueProperty = new MFGProcessIssueProperty();
                    objMFGProcessIssueProperty.rough_cut_id = Val.ToInt(lueCutNo.EditValue);
                    objMFGProcessIssueProperty.kapan_id = Val.ToInt(lueKapan.EditValue);
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
                m_dtbIssueProcess = Stock_Data.Copy();
                grdProcessIssue.DataSource = m_dtbIssueProcess;
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
                            grvProcessIssue.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            grvProcessIssue.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            grvProcessIssue.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            grvProcessIssue.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            grvProcessIssue.ExportToText(Filepath);
                            break;
                        case "html":
                            grvProcessIssue.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            grvProcessIssue.ExportToCsv(Filepath);
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
        public void CalculateTotal()
        {
            try
            {
                if (Val.ToInt(txtRejPcs.Text) != 0 || txtIssuePcs.Text != "" || Val.ToInt(txtOutpcs.Text) != 0)
                {
                    lblWagesAmt.Text = Val.ToString(Math.Round(Val.ToDecimal(lblWagesRate.Text) * Val.ToDecimal(txtIssuePcs.Text), 2));

                    if (Val.ToInt(txtIssuePcs.Text) != 0 && Val.ToDecimal(lblOsCarat.Text) != 0 && Val.ToInt(lblOsPcs.Text) != 0)
                    {
                        txtIssCarat.Text = Val.ToString(Math.Round(Val.ToDecimal(Val.ToInt(txtIssuePcs.Text) / (Val.ToInt(lblOsPcs.Text) / Val.ToDecimal(lblOsCarat.Text))), 3));
                    }
                    else
                    {
                        txtIssCarat.Text = "0";
                    }
                    if (Val.ToInt(txtOutpcs.Text) != 0 && Val.ToDecimal(lblOsCarat.Text) != 0 && Val.ToInt(lblOsPcs.Text) != 0)
                    {
                        txtOutCarat.Text = Val.ToString(Math.Round(Val.ToDecimal(Val.ToInt(txtOutpcs.Text) / (Val.ToInt(lblOsPcs.Text) / Val.ToDecimal(lblOsCarat.Text))), 3));
                    }
                    else
                    {
                        txtOutCarat.Text = "0";
                    }
                    if (Val.ToInt(txtRejPcs.Text) != 0 && Val.ToDecimal(lblOsCarat.Text) != 0 && Val.ToInt(lblOsPcs.Text) != 0)
                    {
                        txtRejCarat.Text = Val.ToString(Math.Round(Val.ToDecimal(Val.ToInt(txtRejPcs.Text) / (Val.ToInt(lblOsPcs.Text) / Val.ToDecimal(lblOsCarat.Text))), 3));
                    }
                    else
                    {
                        txtRejCarat.Text = "0";
                    }
                    if (Val.ToInt(txtReSoingPcs.Text) != 0 && Val.ToDecimal(lblOsCarat.Text) != 0 && Val.ToInt(lblOsPcs.Text) != 0)
                    {
                        txtResoingCarat.Text = Val.ToString(Math.Round(Val.ToDecimal(Val.ToInt(txtReSoingPcs.Text) / (Val.ToInt(lblOsPcs.Text) / Val.ToDecimal(lblOsCarat.Text))), 3));
                    }
                    else
                    {
                        txtResoingCarat.Text = "0";
                    }
                    if (Val.ToInt(txtBreakPcs.Text) != 0 && Val.ToDecimal(lblOsCarat.Text) != 0 && Val.ToInt(lblOsPcs.Text) != 0)
                    {
                        txtBreakCarat.Text = Val.ToString(Math.Round(Val.ToDecimal(Val.ToInt(txtBreakPcs.Text) / (Val.ToInt(lblOsPcs.Text) / Val.ToDecimal(lblOsCarat.Text))), 3));
                    }
                    else
                    {
                        txtBreakCarat.Text = "0";
                    }
                    lbltxttotalpcs.Text = Val.ToString(Val.ToInt(txtIssuePcs.Text) + Val.ToInt(txtRejPcs.Text) + Val.ToInt(txtOutpcs.Text) + Val.ToInt(txtReSoingPcs.Text) + Val.ToInt(txtBreakPcs.Text));

                    lbltxttotalCarat.Text = Val.ToString(Val.ToDecimal(txtIssCarat.Text) + Val.ToDecimal(txtRejCarat.Text) + Val.ToDecimal(txtOutCarat.Text) + Val.ToDecimal(txtResoingCarat.Text) + Val.ToInt(txtBreakCarat.Text));

                }
                else
                {
                    lblWagesAmt.Text = string.Empty;

                    txtIssuePcs.Text = string.Empty;
                    txtRejPcs.Text = string.Empty;
                    txtOutpcs.Text = string.Empty;
                    txtReSoingPcs.Text = string.Empty;
                    txtBreakPcs.Text = string.Empty;

                    txtIssCarat.Text = string.Empty;
                    txtRejCarat.Text = string.Empty;
                    txtOutCarat.Text = string.Empty;
                    txtResoingCarat.Text = string.Empty;
                    txtBreakCarat.Text = string.Empty;

                    lbltxttotalpcs.Text = string.Empty;
                    lbltxttotalCarat.Text = string.Empty;


                }
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

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            FrmMFGLottingSearch FrmSearchLotting = new FrmMFGLottingSearch();
            FrmSearchLotting.FrmMFGProcessIssueFactory = this;
            FrmSearchLotting.ShowForm(this);
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
