using BLL;
using BLL.FunctionClasses.Master.MFG;
using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using DERP.Master;
using DERP.Master.MFG;
using DERP.Report;
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

namespace DERP.Transaction.MFG
{
    public partial class FrmMFGProcessIssue : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        Control _NextEnteredControl;
        private List<Control> _tabControls;

        MFGEmployeeTarget objEmployeeTarget;
        MFGSawableRecieve objSawableRecieve;
        MFGProcessReceive objProcessRecieve;
        MfgRoughSieve objRoughSieve;
        MfgRoughClarityMaster objClarity;

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
        DataTable DTabTemp = new DataTable();
        string PasteData = string.Empty;
        //DataTable DTab_KapanWiseData;
        int m_Srno;
        int m_update_srno;
        int m_flag;
        int m_prd_id;
        Int64 m_numForm_id;
        Int64 IntRes;
        Int64 Issue_IntRes;
        int m_kapan_id;
        int m_IsLot;
        int m_OsPcs;
        string m_cut_no;

        decimal m_balcarat;
        decimal m_numcarat;
        decimal m_old_carat;
        decimal m_old_rate;
        decimal m_old_amount;
        decimal m_OScarat;
        decimal m_os_rate;
        decimal m_assort_rate;

        bool m_blnadd;
        bool m_blnsave;
        bool m_blnflag;
        Int64 Lot_SrNo_Print = 0;
        #endregion

        #region Constructor
        public FrmMFGProcessIssue()
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

            DtControlSettings = new DataTable();
            m_dtbDetail = new DataTable();
            m_dtbParam = new DataTable();
            m_dtbSubProcess = new DataTable();
            m_dtbIssueProcess = new DataTable();
            m_dtOutstanding = new DataTable();
            m_dtbKapan = new DataTable();
            // DTab_KapanWiseData = new DataTable();

            m_Srno = 1;
            m_update_srno = 1;
            m_flag = 0;
            m_prd_id = 0;
            m_numForm_id = 0;
            m_kapan_id = 0;
            m_IsLot = 0;
            m_OsPcs = 0;

            m_cut_no = "";

            m_balcarat = 0;
            m_numcarat = 0;
            m_old_carat = 0;
            m_old_rate = 0;
            m_old_amount = 0;
            m_OScarat = 0;
            m_os_rate = 0;
            m_assort_rate = 0;
            m_blnadd = new bool();
            m_blnsave = new bool();
            m_blnflag = new bool();
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

        # region Dynamic Tab Setting
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
        private void txtPcs_EditValueChanged(object sender, EventArgs e)
        {
            if (txtPcs.Text != "")
            {
                lblWagesAmt.Text = Val.ToString(Math.Round(Val.ToDecimal(lblWagesRate.Text) * Val.ToDecimal(txtPcs.Text), 2));
            }
            else
                lblWagesAmt.Text = "0";
        }

        private void RepBtnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
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

                ClearDetails();

                // Add By Praful On 29072021

                //DTab_KapanWiseData = Global.GetRoughStockWise(Val.ToInt(0), Val.ToInt32(0));

                // End By Praful On 29072021

                DataTable dtbProcess = (((DataTable)lueProcess.Properties.DataSource).Copy());
                if (dtbProcess.Rows.Count > 0)
                {
                    dtbProcess = dtbProcess.Select("is_default = 'True'").CopyToDataTable();
                    lueProcess.EditValue = Val.ToInt(dtbProcess.Rows[0]["process_id"]);
                }
                if (GlobalDec.gEmployeeProperty.role_name == "SURAT KAMALA")
                {
                    MFGProcessIssue objMFGProcessIssue = new MFGProcessIssue();
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
                if (Val.DBDate(BLL.GlobalDec.gStrServerDate) != Val.DBDate(dtpIssueDate.Text))
                {
                    Str = GlobalDec.CheckLockIsOpenOrNot(Val.DBDate(dtpIssueDate.Text), Val.DBTime(DateTime.Now.ToShortTimeString())); //Val.GetFullTime12());
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
                        dtpIssueDate.Enabled = true;
                        dtpIssueDate.Visible = true;
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
                if (Val.ToInt(lueEmployee.EditValue) > 0 && Val.ToInt(lueManager.EditValue) > 0 && Val.ToInt(lueSubProcess.EditValue) > 0)
                {
                    MFGProcessIssue objMFGProcessIssue = new MFGProcessIssue();
                    DataTable dtWagesRate = new DataTable();

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


                    btnAdd.Enabled = true;
                    btnPopUpStock.Enabled = true;
                }
                else
                {
                    if (GlobalDec.gEmployeeProperty.role_name == "SURAT KAMALA")
                    {
                        MFGProcessIssue objMFGProcessIssue = new MFGProcessIssue();
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
        private void lueEmployee_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToInt(lueEmployee.EditValue) > 0 && Val.ToInt(lueManager.EditValue) > 0 && Val.ToInt(lueSubProcess.EditValue) > 0)
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
        private void lueProcess_EditValueChanged_2(object sender, EventArgs e)
        {
            try
            {
                if (GlobalDec.gEmployeeProperty.role_name == "SURAT KAMALA")
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
                    }
                }
                //    if (lueProcess.EditValue != System.DBNull.Value)
                //    {
                //        if (m_dtbSubProcess.Rows.Count > 0)
                //        {
                //            DataTable dtbdetail = m_dtbSubProcess;

                //            string strFilter = string.Empty;

                //            if (lueProcess.Text != "")
                //                strFilter = "process_id = " + lueProcess.EditValue;

                //            dtbdetail.DefaultView.RowFilter = strFilter;
                //            dtbdetail.DefaultView.ToTable();

                //            DataTable dtb = dtbdetail.DefaultView.ToTable();

                //            lueSubProcess.Properties.DataSource = dtb;
                //            lueSubProcess.Properties.ValueMember = "sub_process_id";
                //            lueSubProcess.Properties.DisplayMember = "sub_process_name";
                //            lueSubProcess.EditValue = System.DBNull.Value;
                //        }

                //        MFGProcessIssue objMFGProcessIssue = new MFGProcessIssue();


                //        dtOsRate = objMFGProcessIssue.GetOSRate(Val.ToInt(lueCutNo.EditValue), Val.ToInt64(txtLotID.Text), Val.ToString(lueProcess.Text));

                //        if (dtOsRate.Rows.Count > 0)
                //        {
                //            m_os_rate = Val.ToDecimal(dtOsRate.Rows[0]["rate"]);
                //            txtRate.Text = Val.ToString(m_os_rate);
                //        }
                //        else
                //        {
                //            m_os_rate = 0;
                //            txtRate.Text = Val.ToString(m_os_rate);
                //        }

                //        if (lueProcess.Text == "SHINE ISSUE")
                //        {
                //            if (dtAssortRate.Rows.Count > 0)
                //            {
                //                txtRate.Text = Val.ToString(dtAssortRate.Rows[0]["rate"]);
                //                lueclarity.EditValue = Val.ToInt(dtAssortRate.Rows[0]["rough_clarity_id"]);
                //                lueSieve.EditValue = Val.ToInt(dtAssortRate.Rows[0]["rough_sieve_id"]);
                //                lueQuality.Tag = Val.ToString(dtAssortRate.Rows[0]["quality_id"]);
                //                lueQuality.Text = Val.ToString(dtAssortRate.Rows[0]["quality_name"]);
                //                luePurity.EditValue = Val.ToInt(dtAssortRate.Rows[0]["purity_id"]);
                //            }
                //            else
                //            {
                //                Global.Message("Assort process is not complete.");
                //            }
                //        }


                //    }
                //    if (lueProcess.EditValue != System.DBNull.Value && lueSubProcess.EditValue != System.DBNull.Value && Val.ToInt64(txtLotID.Text) != 0)
                //    {
                //        DataTable dtIss = new DataTable();
                //        dtIss = objProcessRecieve.GetIssueID(Val.ToInt64(txtLotID.Text), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue));
                //        if (dtIss.Rows.Count > 0)
                //        {
                //            Global.Message("Lot is already issue in this process.");
                //        }
                //    }
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
                                        dtIssOS = objProcessRecieve.Carat_OutStanding_GetData(Val.ToInt64(txtLotID.Text), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue), 0, "I");

                                        if (dtIssOS.Rows.Count > 0)
                                        {
                                            if (Val.ToDecimal(dtIssOS.Rows[0]["carat"]) > 0)
                                            {
                                                m_OScarat = Val.ToDecimal(Val.ToDecimal(dtIssOS.Rows[0]["carat"]));
                                                txtBalCarat.Text = Val.ToString(m_OScarat);
                                                txtIssCarat.Text = Val.ToString(m_OScarat);
                                                lblOsCarat.Text = Val.ToString(Val.ToDecimal(dtIssOS.Rows[0]["carat"]));
                                                lblOsPcs.Text = Val.ToString(Val.ToDecimal(dtIssOS.Rows[0]["pcs"]));
                                                txtPcs.Text = Val.ToString(Val.ToDecimal(dtIssOS.Rows[0]["pcs"]));
                                            }
                                        }
                                        else
                                        {
                                            txtLotID.Text = null;
                                            txtBalCarat.Text = string.Empty;
                                            txtIssCarat.Text = string.Empty;
                                            txtPcs.Text = string.Empty;
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
                        if (Val.ToInt(lueSubProcess.EditValue) > 0)
                        {
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
                m_IsLot = 1;
                lueKapan.EditValue = null;
                lueCutNo.EditValue = null;
                MFGProcessReceive objProcessRecieve = new MFGProcessReceive();

                if (Val.ToInt64(txtLotID.Text) != 0 && Val.ToString(lueKapan.Text) == "" && Val.ToString(lueCutNo.Text) == "")
                {
                    m_dtbParam = Global.GetRoughStockWise(Val.ToInt(0), Val.ToInt64(txtLotID.Text));

                    if (m_dtbParam.Rows.Count > 0)
                    {
                        lueKapan.EditValue = Val.ToInt64(m_dtbParam.Rows[0]["kapan_id"]);
                        Int64 CutId = Val.ToInt64(m_dtbParam.Rows[0]["rough_cut_id"]);
                        Int64 ManagerId = Val.ToInt64(m_dtbParam.Rows[0]["manager_id"]);
                        int ClarityId = Val.ToInt(m_dtbParam.Rows[0]["rough_clarity_id"]);
                        int SieveId = Val.ToInt(m_dtbParam.Rows[0]["rough_sieve_id"]);
                        int QualityId = Val.ToInt(m_dtbParam.Rows[0]["quality_id"]);
                        int PurityId = Val.ToInt(m_dtbParam.Rows[0]["purity_name"]);
                        MFGProcessIssue objMFGProcessIssue = new MFGProcessIssue();
                        m_blnflag = true;
                        //m_dtbParam = Global.GetRoughKapanWise(Val.ToInt(lueKapan.EditValue));
                        m_dtbParam = Global.GetRoughKapanWise_Data(Val.ToInt(lueKapan.EditValue));
                        //if (m_dtbParam.Rows.Count == 0)
                        //{
                        //    m_dtbParam = DTab_KapanWiseData;
                        //}
                        //lueKapan.Text = Val.ToString(m_dtbParam.Rows[0]["kapan_no"]);
                        lueCutNo.Properties.DataSource = m_dtbParam;
                        lueCutNo.Properties.ValueMember = "rough_cut_id";
                        lueCutNo.Properties.DisplayMember = "rough_cut_no";
                        lueCutNo.EditValue = Val.ToInt64(CutId);
                        lueManager.EditValue = Val.ToInt64(ManagerId);

                        DataTable dtIssOS = new DataTable();
                        dtIssOS = objProcessRecieve.Carat_OutStanding_GetData(Val.ToInt64(txtLotID.Text), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue), 0, "I");

                        dtAssortRate = objMFGProcessIssue.GetAssortRate(Val.ToInt64(txtLotID.Text), 0);

                        DataTable dtb_StockCarat = objSawableRecieve.GetBalanceCarat(Val.ToInt64(txtLotID.Text), 0);
                        m_kapan_id = Val.ToInt(lueKapan.EditValue);

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
                            m_OScarat = Val.ToDecimal(Val.ToDecimal(dtIssOS.Rows[0]["carat"]));
                            m_OsPcs = Val.ToInt(Val.ToDecimal(dtIssOS.Rows[0]["pcs"]));
                            txtBalCarat.Text = Val.ToString(m_OScarat);
                            lblOsPcs.Text = Val.ToString(m_OsPcs);
                            lblOsCarat.Text = Val.ToString(m_OScarat);
                            txtIssCarat.Text = Val.ToString(m_OScarat);
                            txtPcs.Text = Val.ToString(m_OsPcs);
                        }
                        if (lueProcess.Text == "CHIPIYO FINAL")
                        {
                            lueclarity.EditValue = Val.ToInt(ClarityId);
                            lueSieve.EditValue = Val.ToInt(SieveId);
                            lueQuality.EditValue = Val.ToInt(QualityId);
                            //lueclarity.Text = Val.ToString(m_dtbParam.Rows[0]["rough_clarity_name"]);
                            //lueSieve.Text = Val.ToString(m_dtbParam.Rows[0]["sieve_name"]);
                            //lueQuality.Text = Val.ToString(m_dtbParam.Rows[0]["quality_name"]);
                            luePurity.EditValue = Val.ToInt(PurityId);
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
                        else
                        {
                            lueclarity.EditValue = Val.ToInt(ClarityId);
                            lueSieve.EditValue = Val.ToInt(SieveId);
                            lueQuality.EditValue = Val.ToInt(QualityId);
                            luePurity.EditValue = Val.ToInt(PurityId);
                            //lueQuality.Text = Val.ToString(m_dtbParam.Rows[0]["quality_name"]);
                            //lueclarity.Text = m_dtbParam.Rows[0]["rough_clarity_name"].ToString();
                            //lueSieve.Text = Val.ToString(m_dtbParam.Rows[0]["sieve_name"]);
                            //luePurity.EditValue = Val.ToString(m_dtbParam.Rows[0]["purity_name"]);
                        }
                    }
                    else
                    {
                        txtIssCarat.Text = "0";
                        txtPcs.Text = "0";
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
                    txtPcs.Text = "0";
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
                decimal amount = Math.Round(Val.ToDecimal(txtIssCarat.Text) * Val.ToDecimal(txtRate.Text), 2);
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

                IntRes = 0;
                Issue_IntRes = 0;
                Int64 NewHistory_Union_Id = 0;
                Int64 Lot_SrNo = 0;

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
                        objMFGProcessIssueProperty.union_id = IntRes;
                        objMFGProcessIssueProperty.issue_union_id = Issue_IntRes;
                        objMFGProcessIssueProperty.kapan_id = Val.ToInt(drw["kapan_id"]);
                        m_old_carat = Val.ToDecimal(drw["carat"]);
                        objMFGProcessIssueProperty.history_union_id = NewHistory_Union_Id;
                        objMFGProcessIssueProperty.lot_srno = Lot_SrNo;

                        objMFGProcessIssueProperty = objMFGProcessIssue.Save(objMFGProcessIssueProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                        IntRes = objMFGProcessIssueProperty.union_id;
                        Issue_IntRes = objMFGProcessIssueProperty.issue_union_id;
                        NewHistory_Union_Id = Val.ToInt64(objMFGProcessIssueProperty.history_union_id);
                        Lot_SrNo = Val.ToInt64(objMFGProcessIssueProperty.lot_srno);
                        Lot_SrNo_Print = Val.ToInt64(objMFGProcessIssueProperty.lot_srno);

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
        private void backgroundWorker_ProcessIssue_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                panelProgress.Visible = false;
                if (IntRes > 0)
                {
                    if (GlobalDec.gEmployeeProperty.role_name == "SURAT ROUGH")
                    {
                        DialogResult result = MessageBox.Show("Process Issue Data Save Succesfully and Lot SrNo is : " + Lot_SrNo_Print + " Are you sure print this janged?", "Confirmation", MessageBoxButtons.YesNoCancel);
                        if (result != DialogResult.Yes)
                        {
                            btnSave.Enabled = true;
                            ClearDetails();
                            return;
                        }

                        MFGJangedIssue objMFGJangedIssue = new MFGJangedIssue();
                        DataTable DTab_IssueJanged = objMFGJangedIssue.GetProcessJanged_DataDetails(Val.ToInt64(Lot_SrNo_Print));

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
                    }
                    else
                    {
                        Global.Confirm("Process Issue Data Save Succesfully");
                        ClearDetails();
                    }
                }
                else
                {
                    Global.Confirm("Error In Process Issue");
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }
        private void grvProcessIssue_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = grvProcessIssue.GetDataRow(e.RowHandle);
                        btnAdd.Text = "&Update";
                        lueCutNo.Text = Val.ToString(Drow["rough_cut_no"]);
                        lueCutNo.EditValue = Val.ToInt(Drow["rough_cut_id"]);
                        txtLotID.Text = Val.ToString(Drow["lot_id"]);
                        txtPcs.Text = Val.ToString(Drow["pcs"]);
                        txtIssCarat.Text = Val.ToString(Drow["carat"]);
                        txtRate.Text = Val.ToString(Drow["rate"]);
                        txtAmount.Text = Val.ToString(Drow["amount"]);
                        lueManager.EditValue = Val.ToInt(Drow["manager_id"]);
                        lueProcess.EditValue = Val.ToInt64(Drow["process_id"]);
                        lueSubProcess.EditValue = Val.ToInt(Drow["sub_process_id"]);
                        lueSieve.EditValue = Val.ToInt(Drow["rough_sieve_id"]);
                        luePurity.EditValue = Val.ToInt(Drow["purity_id"]);
                        lueQuality.EditValue = Val.ToInt64(Drow["quality_id"]);
                        lueclarity.EditValue = Val.ToInt(Drow["rough_clarity_id"]);
                        m_numcarat = Val.ToDecimal(Drow["carat"]);
                        m_cut_no = Val.ToString(Drow["rough_cut_no"]);
                        m_update_srno = Val.ToInt(Drow["sr_no"]);
                        m_old_carat = Val.ToDecimal(Drow["carat"]);
                        m_old_rate = Val.ToDecimal(Drow["rate"]);
                        m_old_amount = Val.ToDecimal(Drow["amount"]);
                        m_flag = Val.ToInt(1);
                        m_blnflag = true;
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            FrmMFGSearchProcess FrmSearchProcess = new FrmMFGSearchProcess();
            FrmSearchProcess.FrmMFGProcessIssue = this;
            //FrmSearchProcess.DTab = DtPending;
            FrmSearchProcess.ShowForm(this);
        }
        private void txtPcs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtIssCarat_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txtRate_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
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
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (AddInGrid())
                {
                    lueProcess.Enabled = false;
                    lueSubProcess.Enabled = false;
                    luePurity.Enabled = false;
                    lueQuality.Enabled = false;
                    lueSieve.Enabled = false;
                    lueclarity.Enabled = false;

                    luePurity.EditValue = null;
                    lueQuality.EditValue = null;
                    lueSieve.EditValue = null;
                    lueclarity.EditValue = null;

                    txtPcs.Text = string.Empty;
                    txtIssCarat.Text = string.Empty;
                    //txtRate.Text = string.Empty;
                    //txtAmount.Text = string.Empty;
                    //txtLotID.Text = string.Empty;
                    //lblOsPcs.Text = string.Empty;
                    //lblOsCarat.Text = string.Empty;
                    ControlSettingDT(Val.ToInt(ObjPer.form_id), this);
                    AddGotFocusListener(this);
                    this.KeyPreview = true;

                    TabControlsToList(this.Controls);
                    _tabControls = _tabControls.OrderBy(x => x.TabIndex).ToList();
                    //lueKapan.EditValue = null;
                    //lueCutNo.EditValue = null;
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            FrmMFGSearchProcess FrmSearchProcess = new FrmMFGSearchProcess();
            FrmSearchProcess.FrmMFGProcessIssue = this;
            //FrmSearchProcess.DTab = DtPending;
            FrmSearchProcess.ShowForm(this);
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
                    DataRow[] dr = m_dtbIssueProcess.Select("rough_cut_no = '" + Val.ToString(lueCutNo.Text) + "' AND lot_id = " + Val.ToInt64(txtLotID.Text) + " AND rough_sieve_id = " + Val.ToInt(lueSieve.EditValue) + " AND purity_id = " + Val.ToInt(luePurity.EditValue));

                    if (dr.Count() == 1)
                    {
                        Global.Message("Record already exists in grid", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lueManager.Focus();
                        blnReturn = false;
                        return blnReturn;
                    }
                    DataRow[] dr1 = m_dtbIssueProcess.Select("rough_cut_no = '" + Val.ToString(lueCutNo.Text) + "' AND lot_id = " + Val.ToInt64(txtLotID.Text) + " AND rough_sieve_id = " + Val.ToInt(lueSieve.EditValue));

                    if (dr1.Count() > 0)
                    {
                        Global.Message("Record already exists in grid", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lueCutNo.EditValue = null;
                        lueKapan.EditValue = null;
                        lueKapan.Focus();
                        blnReturn = false;
                        return blnReturn;
                    }
                    DataRow drwNew = m_dtbIssueProcess.NewRow();
                    int numPcs = Val.ToInt(txtPcs.Text);
                    decimal numCarat = Val.ToDecimal(txtIssCarat.Text);
                    decimal numRate = Val.ToDecimal(txtRate.Text);
                    decimal numAmount = Val.ToDecimal(txtAmount.Text);

                    drwNew["issue_id"] = Val.ToInt(0);
                    drwNew["issue_date"] = Val.DBDate(dtpIssueDate.Text);
                    drwNew["rough_cut_no"] = Val.ToString(lueCutNo.Text);
                    drwNew["rough_cut_id"] = Val.ToString(lueCutNo.EditValue);
                    drwNew["lot_id"] = Val.ToInt64(txtLotID.Text);
                    drwNew["manager_id"] = Val.ToInt(lueManager.EditValue);
                    drwNew["manager"] = Val.ToString(lueManager.Text);
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
                    m_dtbIssueProcess.Rows.Add(drwNew);
                    m_Srno++;
                }
                else if (btnAdd.Text == "&Update")
                {

                    if (m_dtbIssueProcess.Select("sr_no ='" + Val.ToInt(m_update_srno) + "'").Length > 0)
                    {
                        for (int i = 0; i < m_dtbIssueProcess.Rows.Count; i++)
                        {
                            if (m_dtbIssueProcess.Select("rough_cut_no ='" + m_cut_no + "' AND sr_no = '" + m_update_srno + "'").Length > 0)
                            {
                                if (m_dtbIssueProcess.Rows[m_update_srno - 1]["rough_cut_no"].ToString() == m_cut_no.ToString())
                                {
                                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["lot_id"] = Val.ToString(txtLotID.Text);
                                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["manager_id"] = Val.ToInt(lueManager.EditValue);
                                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["manager"] = Val.ToString(lueManager.Text);
                                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["employee_id"] = Val.ToInt(lueEmployee.EditValue);
                                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["employee"] = Val.ToString(lueEmployee.Text);
                                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["process_id"] = Val.ToInt(lueProcess.EditValue);
                                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["process"] = Val.ToString(lueProcess.Text);
                                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["sub_process_id"] = Val.ToInt(lueSubProcess.EditValue);
                                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["subprocess"] = Val.ToString(lueSubProcess.Text);
                                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["rough_sieve_id"] = Val.ToInt(lueSieve.EditValue);
                                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["sieve_name"] = Val.ToString(lueSieve.Text);
                                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["purity_id"] = Val.ToInt(luePurity.EditValue);
                                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["purity_name"] = Val.ToString(luePurity.Text);
                                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["quality_id"] = Val.ToInt(lueQuality.EditValue);
                                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["quality_name"] = Val.ToString(lueQuality.Text);
                                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["rough_clarity_id"] = Val.ToInt(lueclarity.EditValue);
                                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["rough_clarity_name"] = Val.ToString(lueclarity.Text);
                                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["pcs"] = Val.ToInt(txtPcs.Text).ToString();
                                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["carat"] = Val.ToDecimal(txtIssCarat.Text).ToString();
                                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["rate"] = Val.ToDecimal(txtRate.Text).ToString();
                                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["amount"] = Val.ToDecimal(txtAmount.Text).ToString();
                                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["prd_id"] = Val.ToInt(m_prd_id);
                                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["kapan_id"] = Val.ToInt(m_kapan_id);
                                    m_flag = Val.ToInt(0);
                                    break;
                                }
                            }
                        }
                    }
                    btnAdd.Text = "&Add";
                }
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
                    if (Val.ToDecimal(txtIssCarat.Text) == 0)
                    {
                        lstError.Add(new ListError(12, "Carat"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtBalCarat.Focus();
                        }
                    }
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


                lueSieve.EditValue = System.DBNull.Value;
                luePurity.EditValue = System.DBNull.Value;
                lueQuality.EditValue = System.DBNull.Value;
                lueclarity.EditValue = System.DBNull.Value;

                txtPcs.Text = string.Empty;
                txtBalCarat.Text = string.Empty;
                txtLotID.Text = string.Empty;
                txtIssCarat.Text = string.Empty;

                //lueProcess.Enabled = true;
                lueSubProcess.Enabled = true;
                lueEmployee.Enabled = true;
                luePurity.Enabled = true;
                lueQuality.Enabled = false;
                lueSieve.Enabled = true;
                btnPopUpStock.Enabled = false;
                lblOsPcs.Text = "0";
                lblOsCarat.Text = "0.00";
                m_flag = 0;
                m_Srno = 1;
                m_update_srno = 0;
                m_numcarat = 0;
                m_old_carat = 0;
                m_old_rate = 0;
                m_old_amount = 0;
                m_kapan_id = 0;
                txtLotSrNo.Text = "0";
                btnAdd.Text = "&Add";
                m_IsLot = 0;
                m_blnflag = false;
                Lot_SrNo_Print = 0;
                btnSave.Enabled = true;

                if (GlobalDec.gEmployeeProperty.role_name == "SURAT KAMALA")
                {
                    dtpIssueDate.Focus();
                }
                else
                {
                    lueProcess.EditValue = System.DBNull.Value;
                    lueSubProcess.EditValue = System.DBNull.Value;
                    txtLotID.Focus();
                }
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
                m_dtbIssueProcess.Columns.Add("rate", typeof(decimal)).DefaultValue = 0;
                m_dtbIssueProcess.Columns.Add("amount", typeof(decimal)).DefaultValue = 0;
                m_dtbIssueProcess.Columns.Add("sr_no", typeof(int)).DefaultValue = 0;
                m_dtbIssueProcess.Columns.Add("prd_id", typeof(int)).DefaultValue = 0;
                m_dtbIssueProcess.Columns.Add("kapan_id", typeof(int)).DefaultValue = 0;
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
                    //lueCutNo.Text = Val.ToString(m_dtOutstanding.Rows[0]["rough_cut_no"]);
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

                    if (lueProcess.Text == "SOYEBAL CHIPIYO")
                    {
                        if (lblOsCarat.Text.ToString() == "0.000")
                        {
                            Global.Message("OS Carat is Zero Please Check this Cut No = " + lueCutNo.Text);
                            return;
                        }

                        DtPending = objMFGProcessIssue.GetCharniPendingStock(objMFGProcessIssueProperty, 0);
                    }
                    else if (lueProcess.Text == "SOYEBLE")
                    {
                        if (lblOsCarat.Text.ToString() == "0.000")
                        {
                            Global.Message("OS Carat is Zero Please Check this Cut No = " + lueCutNo.Text);
                            return;
                        }

                        DtPending = objMFGProcessIssue.GetCharniPendingStock(objMFGProcessIssueProperty, 1);
                    }
                    //else if (lueProcess.Text == "CHIPIYO")
                    //{
                    //    if (lblOsCarat.Text.ToString() == "0.000")
                    //    {
                    //        Global.Message("OS Carat is Zero Please Check this Cut No = " + lueCutNo.Text);
                    //        return;
                    //    }

                    //    DtPending = objMFGProcessIssue.GetCharniPendingStock(objMFGProcessIssueProperty, 2);
                    //}
                    else
                    {
                        DtPending = objMFGProcessIssue.GetPendingStock(objMFGProcessIssueProperty);
                    }

                    FrmMFGStockConfirm FrmStockConfirm = new FrmMFGStockConfirm();
                    FrmStockConfirm.FrmMFGProcessIssue = this;
                    FrmStockConfirm.DTab = DtPending;
                    FrmStockConfirm.ShowForm(this);
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
            }
        }
        public void FillGrid(int Lot_SRNo)
        {
            MFGJangedIssue objMFGJangedIssue = new MFGJangedIssue();

            DataTable DTab_DeptIssueJanged = objMFGJangedIssue.GetIssueDetails(Lot_SRNo);

            btnSave.Enabled = false;

            if (DTab_DeptIssueJanged.Rows.Count > 0)
            {
                dtpIssueDate.Text = Val.DBDate(DTab_DeptIssueJanged.Rows[0]["issue_date"].ToString());
                txtLotSrNo.Text = Val.ToInt64(DTab_DeptIssueJanged.Rows[0]["lot_srno"]).ToString();
                lueProcess.EditValue = Val.ToInt32(DTab_DeptIssueJanged.Rows[0]["process_id"]);
                lueSubProcess.EditValue = Val.ToInt32(DTab_DeptIssueJanged.Rows[0]["sub_process_id"]);
                lueEmployee.EditValue = Val.ToInt64(DTab_DeptIssueJanged.Rows[0]["employee_id"]);
                lueManager.EditValue = Val.ToInt64(DTab_DeptIssueJanged.Rows[0]["manager_id"]);
            }
            else
            {
                txtLotSrNo.Text = "0";
            }
            grdProcessIssue.DataSource = DTab_DeptIssueJanged;
            grdProcessIssue.RefreshDataSource();
            grvProcessIssue.BestFitColumns();


        }
        public void GetStockData(DataTable Stock_Data)
        {
            try
            {
                //m_dtbIssueProcess = Stock_Data.Copy();
                //m_dtbIssueProcess.AcceptChanges();
                //if (Stock_Data != null)
                //{
                //    if (Stock_Data.Rows.Count > 0)
                //    {
                //        for (int i = 0; i < Stock_Data.Rows.Count; i++)
                //        {
                //            if (Stock_Data.Rows[i]["lot_id"].ToString() == txtLotID.Text)
                //            {
                //                Global.Message("Lot ID already added to the Issue list!");
                //                return;
                //            }
                //        }
                //    }
                //}

                //if (txtLotId.Text.Length == 0)
                //{
                //    return;
                //}
                if (GlobalDec.gEmployeeProperty.role_name == "SURAT KAMALA")
                {
                    foreach (DataRow drw in Stock_Data.Rows)
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
                }

                DTabTemp = Stock_Data.Copy();
                m_dtbIssueProcess.AcceptChanges();
                if (m_dtbIssueProcess != null)
                {
                    if (m_dtbIssueProcess.Rows.Count > 0)
                    {
                        for (int i = 0; i < m_dtbIssueProcess.Rows.Count; i++)
                        {
                            for (int j = 0; j < DTabTemp.Rows.Count; j++)
                            {
                                if (m_dtbIssueProcess.Rows[i]["lot_id"].ToString() == DTabTemp.Rows[j]["lot_id"].ToString())
                                {
                                    Global.Message(m_dtbIssueProcess.Rows[i]["lot_id"].ToString() + " = Lot ID already added to the Issue list!");
                                    txtLotID.Text = "";
                                    txtLotID.Focus();
                                    return;
                                }
                            }
                        }
                    }
                }

                if (m_dtbIssueProcess.Rows.Count > 0)
                {
                    //DataTable DTab_ValidateLotID = Stock_Data.Copy();

                    //if (DTab_ValidateLotID.Rows.Count > 0)
                    //{
                    //}
                    //else
                    //{
                    //    Global.Message("Lot ID Not Issue in Janged");
                    //    txtLotId.Text = "";
                    //    txtLotId.Focus();
                    //    return;
                    //}

                    DTabTemp = Stock_Data.Copy();

                    //if (DTabTemp.Rows.Count > 0)
                    //{
                    //    txtLotId.Text = "";
                    //    txtLotId.Focus();
                    //}
                    m_dtbIssueProcess.Merge(DTabTemp);
                }
                else
                {
                    //DataTable DTab_ValidateLotID = Stock_Data.Copy();

                    //if (DTab_ValidateLotID.Rows.Count > 0)
                    //{
                    //}
                    //else
                    //{
                    //    Global.Message("Lot ID Not Issue in Janged");
                    //    txtLotId.Text = "";
                    //    txtLotId.Focus();
                    //    return;
                    //}

                    m_dtbIssueProcess = Stock_Data.Copy();

                    //if (DTab_StockData.Rows.Count > 0)
                    //{
                    //    txtLotId.Text = "";
                    //    txtLotId.Focus();
                    //}
                }
                //grdJangedReturn.DataSource = DTab_StockData;
                //grdJangedReturn.RefreshDataSource();
                //dgvJangedReturn.BestFitColumns();

                int Sr_No = 0;
                foreach (DataRow DRow in m_dtbIssueProcess.Rows)
                {
                    DRow["manager_id"] = Val.ToInt64(lueManager.EditValue);
                    DRow["manager"] = Val.ToString(lueManager.Text);
                    DRow["employee_id"] = Val.ToInt64(lueEmployee.EditValue);
                    DRow["employee"] = Val.ToString(lueEmployee.Text);
                    DRow["sub_process_id"] = Val.ToInt64(lueSubProcess.EditValue);
                    DRow["subprocess"] = Val.ToString(lueSubProcess.Text);
                    DRow["process_id"] = Val.ToInt64(lueProcess.EditValue);
                    DRow["process"] = Val.ToString(lueProcess.Text);
                    Sr_No = Sr_No + 1;
                    DRow["sr_no"] = Sr_No;
                }

                grdProcessIssue.DataSource = m_dtbIssueProcess;
                grdProcessIssue.RefreshDataSource();
                grvProcessIssue.BestFitColumns();
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

        private void lueProcess_Validated(object sender, EventArgs e)
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            MFGJangedIssue objMFGJangedIssue = new MFGJangedIssue();
            DataTable DTab_IssueJanged = objMFGJangedIssue.GetProcessJanged_DataDetails(Val.ToInt64(txtLotSrNo.Text));

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
