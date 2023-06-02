using BLL;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Master.MFG;
using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.PropertyClasses.Transaction;
using BLL.PropertyClasses.Transaction.MFG;
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
    public partial class FrmMFGJangedIssue : DevExpress.XtraEditors.XtraForm
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
        DataTable DtPending = new DataTable();
        DataTable m_dtbSubProcess;
        DataTable m_dtbIssueProcess;
        DataTable m_dtOutstanding;
        DataTable m_dtbKapan;
        DataTable m_dtbDetails;
        //DataTable DTab_KapanWiseData = new DataTable();

        int m_Srno;
        int m_update_srno;
        int m_flag;
        int m_prd_id;
        int outside;
        int IntAutoConfirm = 0;
        int m_fromManagerId;
        int m_IsLot;
        int m_balpcs;
        Int64 m_numForm_id;
        Int64 IntRes;
        Int64 Issue_IntRes;
        Int64 JangedNo_IntRes;
        Int64 Dept_IntRes;
        Int64 Janged_IntRes;
        int m_kapan_id;
        Int64 JangedSrNo_IntRes;
        string m_cut_no;
        decimal m_balcarat;
        decimal m_numcarat;
        decimal m_old_carat;
        decimal m_old_rate;
        decimal m_old_amount;
        decimal m_OScarat;
        decimal m_chipyo_rate;
        decimal m_numSummRate;
        decimal m_numSummLRate;

        bool m_blnadd;
        bool m_blnsave;
        bool m_blnflag;
        bool m_blncheckupdate;
        #endregion

        #region Constructor
        public FrmMFGJangedIssue()
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
            m_dtbDetails = new DataTable();

            m_Srno = 1;
            m_update_srno = 1;
            m_flag = 0;
            m_prd_id = 0;
            m_numForm_id = 0;
            m_kapan_id = 0;
            outside = 0;
            m_fromManagerId = 0;
            m_IsLot = 0;
            m_balpcs = 0;

            m_cut_no = "";

            m_balcarat = 0;
            m_numcarat = 0;
            m_old_carat = 0;
            m_old_rate = 0;
            m_old_amount = 0;
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
                Global.LOOKUPParty(lueParty);
                Global.LOOKUPClarity(lueClarity);
                Global.LOOKUPQuality(lueQuality);

                Global.LOOKUPCompany_New(lueToCompany);
                Global.LOOKUPBranch_New(lueToBranch);
                Global.LOOKUPLocation_New(lueToLocation);
                Global.LOOKUPDepartment_New(lueToDepartment);

                m_dtbSubProcess = (((DataTable)lueSubProcess.Properties.DataSource).Copy());

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
                    luePurity.EditValue = System.DBNull.Value;
                    lueSieve.EditValue = System.DBNull.Value;
                    lueQuality.EditValue = System.DBNull.Value;
                    lueClarity.EditValue = System.DBNull.Value;
                    txtPcs.Text = string.Empty;
                    txtIssCarat.Text = string.Empty;
                    //txtRate.Text = string.Empty;
                    txtAmount.Text = string.Empty;
                    lueSieve.Focus();
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
            Global.Export("xlsx", grvJangedIssue);
        }
        private void lueDepartment_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToInt(lueEmployee.EditValue) > 0 && Val.ToInt(lueManager.EditValue) > 0 && Val.ToInt(lueSubProcess.EditValue) > 0)
                {
                    btnAdd.Enabled = true;
                }
                else
                {
                    btnAdd.Enabled = false;
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
            try
            {
                if (Val.ToInt(lueEmployee.EditValue) > 0 && Val.ToInt(lueManager.EditValue) > 0 && Val.ToInt(lueSubProcess.EditValue) > 0)
                {
                    btnAdd.Enabled = true;
                }
                else
                {
                    btnAdd.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void lueManager_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToInt(lueEmployee.EditValue) > 0 && Val.ToInt(lueManager.EditValue) > 0 && Val.ToInt(lueSubProcess.EditValue) > 0)
                {
                    btnAdd.Enabled = true;
                }
                else
                {
                    btnAdd.Enabled = false;
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
            //try
            //{
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
            //        if (lueProcess.Text == "CHIPIYO FINAL" || lueProcess.Text == "ASSORT" || lueProcess.Text == "CHIPIYO")
            //        {
            //            MFGProcessIssue objMFGProcessIssue = new MFGProcessIssue();
            //            DataTable dtChipyoRate = new DataTable();

            //            dtChipyoRate = objMFGProcessIssue.GetOSRate(Val.ToInt(lueCutNo.EditValue), Val.ToInt64(txtLotID.Text), Val.ToString(lueProcess.Text));

            //            if (dtChipyoRate.Rows.Count > 0)
            //            {
            //                m_chipyo_rate = Val.ToDecimal(dtChipyoRate.Rows[0]["rate"]);
            //            }
            //            else
            //            {
            //                m_chipyo_rate = 0;
            //            }
            //            txtRate.Text = Val.ToString(Math.Round(m_chipyo_rate, 2));
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
            //    if (Val.ToInt(lueEmployee.EditValue) > 0 && Val.ToInt(lueManager.EditValue) > 0 && Val.ToInt(lueSubProcess.EditValue) > 0)
            //    {
            //        btnAdd.Enabled = true;
            //    }
            //    else
            //    {
            //        btnAdd.Enabled = false;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Global.Message(ex.ToString());
            //    return;
            //}
        }
        private void lueCutNo_EditValueChanged_1(object sender, EventArgs e)
        {
            //try
            //{
            //    if (!m_blnflag)
            //    {
            //        if (lueCutNo.EditValue != System.DBNull.Value)
            //        {
            //            if (m_dtbParam.Rows.Count > 0)
            //            {
            //                DataRow[] dr = m_dtbParam.Select("rough_cut_no ='" + Val.ToString(lueCutNo.Text) + "'");
            //                if (dr.Length > 0)
            //                {
            //                    txtLotID.Text = Val.ToString(dr[0]["lot_id"]);
            //                    if (txtLotID.Text != string.Empty || Val.ToInt64(txtLotID.Text) != 0)
            //                    {
            //                        GetOsCarat(Val.ToInt64(txtLotID.Text));
            //                        DataTable dtIssOS = new DataTable();
            //                        dtIssOS = objProcessRecieve.Carat_OutStanding_GetData(Val.ToInt64(txtLotID.Text), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue), 0, "I");

            //                        if (dtIssOS.Rows.Count > 0)
            //                        {
            //                            m_OScarat = Val.ToDecimal(Val.ToDecimal(dtIssOS.Rows[0]["carat"]));
            //                            txtBalCarat.Text = Val.ToString(m_OScarat);
            //                            lblOsCarat.Text = Val.ToString(m_OScarat);
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //        m_blnflag = false;
            //    }
            //    else
            //    {
            //        m_blnflag = false;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    BLL.General.ShowErrors(ex);
            //    return;
            //}
        }
        private void txtLotID_Validated(object sender, EventArgs e)
        {
            try
            {
                if (m_dtbParam.Rows.Count > 0)
                {
                    if (Val.ToInt64(txtLotID.Text) != 0)
                    {
                        DataRow[] dr = m_dtbParam.Select("lot_id =" + Val.ToInt64(txtLotID.Text));
                        if (dr.Length == 0)
                        {
                            m_dtbParam = Global.GetRoughKapanWise(0, Val.ToInt64(txtLotID.Text));
                            if (m_dtbParam.Rows.Count == 0)
                            {
                                Global.Message("Lot Not found");
                                return;
                            }
                            else
                            {
                                DataRow[] dr1 = m_dtbParam.Select("lot_id =" + Val.ToInt64(txtLotID.Text));
                                lueKapan.Text = Val.ToString(dr1[0]["kapan_no"]);
                                lueCutNo.Text = Val.ToString(dr1[0]["rough_cut_no"]);
                            }
                        }
                        else
                        {
                            DataRow[] dr1 = m_dtbParam.Select("lot_id =" + Val.ToInt64(txtLotID.Text));
                            //lueKapan.Text = Val.ToString(dr1[0]["kapan_no"]);
                            //lueCutNo.Text = Val.ToString(dr1[0]["rough_cut_no"]);
                        }
                        if (txtLotID.Text != string.Empty || Val.ToInt64(txtLotID.Text) != 0)
                        {
                            m_blnflag = true;
                            GetOsCarat(Val.ToInt64(txtLotID.Text));

                            DataTable dtIssOS = new DataTable();
                            dtIssOS = objProcessRecieve.Carat_OutStanding_GetData(Val.ToInt64(txtLotID.Text), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue), 0, "I");

                            if (dtIssOS.Rows.Count > 0)
                            {
                                m_OScarat = Val.ToDecimal(Val.ToDecimal(dtIssOS.Rows[0]["carat"]));
                                txtBalCarat.Text = Val.ToString(m_OScarat);
                                lblOsCarat.Text = Val.ToString(m_OScarat);
                                lblOsPcs.Text = Val.ToString(m_balpcs);
                            }
                            else if (dtIssOS.Rows.Count == 0)
                            {
                                m_OScarat = Val.ToDecimal(m_balcarat);
                                txtBalCarat.Text = Val.ToString(m_OScarat);
                                lblOsCarat.Text = Val.ToString(m_OScarat);
                                lblOsPcs.Text = Val.ToString(m_balpcs);
                            }
                        }
                        else
                        {
                            txtIssCarat.Text = "0";
                            txtPcs.Text = "0";
                            lblOsCarat.Text = "0";
                            lblOsPcs.Text = "0.00";
                        }
                    }
                    else
                    {
                        BLL.General.ShowErrors("Cut No not Found");
                        txtPcs.Text = "0";
                        txtIssCarat.Text = "0";
                        lueCutNo.EditValue = System.DBNull.Value;
                        m_blnflag = false;
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
                    if (lueKapan.Text.ToString() == "" && txtLotID.Text.ToString() != "" || m_dtbParam.Rows.Count == 0)
                    {
                        m_dtbParam = Global.GetRoughKapanWise(Val.ToInt(0), Val.ToInt64(txtLotID.Text));
                        if (m_dtbParam.Rows.Count == 0)
                        {
                            Global.Message("Lot Not found");
                            return;
                        }
                    }
                    //lueKapan.Text = Val.ToString(m_dtbParam.Rows[0]["kapan_no"]);
                    lueCutNo.Properties.DataSource = m_dtbParam;
                    lueCutNo.Properties.ValueMember = "rough_cut_id";
                    lueCutNo.Properties.DisplayMember = "rough_cut_no";
                    lueCutNo.Text = Val.ToString(m_dtbParam.Rows[0]["rough_cut_no"]);
                    lueKapan.Text = Val.ToString(m_dtbParam.Rows[0]["kapan_no"]);
                    if (txtLotID.Text != string.Empty || Val.ToInt64(txtLotID.Text) != 0)
                    {
                        m_blnflag = true;
                        GetOsCarat(Val.ToInt64(txtLotID.Text));
                        DataTable dtIssOS = new DataTable();
                        dtIssOS = objProcessRecieve.Carat_OutStanding_GetData(Val.ToInt64(txtLotID.Text), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue), 0, "I");

                        if (dtIssOS.Rows.Count > 0)
                        {
                            m_OScarat = Val.ToDecimal(Val.ToDecimal(dtIssOS.Rows[0]["carat"]));
                            m_balpcs = Val.ToInt(Val.ToDecimal(dtIssOS.Rows[0]["pcs"]));
                            txtBalCarat.Text = Val.ToString(m_OScarat);
                            lblOsCarat.Text = Val.ToString(m_OScarat);
                            lblOsPcs.Text = Val.ToString(m_balpcs);
                        }
                        else if (dtIssOS.Rows.Count == 0)
                        {
                            m_OScarat = Val.ToDecimal(m_balcarat);
                            txtBalCarat.Text = Val.ToString(m_OScarat);
                            lblOsCarat.Text = Val.ToString(m_OScarat);
                            lblOsPcs.Text = Val.ToString(m_balpcs);
                        }
                    }
                    m_IsLot = 1;
                }
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
        private void lueEmployee_EditValueChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToInt(lueEmployee.EditValue) > 0 && Val.ToInt(lueManager.EditValue) > 0 && Val.ToInt(lueSubProcess.EditValue) > 0)
                {
                    btnAdd.Enabled = true;
                }
                else
                {
                    btnAdd.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
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
        private void lueParty_EditValueChanged(object sender, EventArgs e)
        {
            PartyMaster objParty = new PartyMaster();
            DataTable Party = objParty.GetData(1);
            Party.DefaultView.RowFilter = "party_id = '" + Val.ToInt(lueParty.EditValue) + "'";
            Party.DefaultView.ToTable();
            DataTable dtbParty = Party.DefaultView.ToTable();
            if (dtbParty.Rows.Count > 0)
            {
                outside = Val.ToBooleanToInt(dtbParty.Rows[0]["is_outside"]);
                IntAutoConfirm = Val.ToBooleanToInt(dtbParty.Rows[0]["is_autoconfirm"]);
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
                decimal amount = Val.ToDecimal(txtIssCarat.Text) * Val.ToDecimal(txtRate.Text);
                txtAmount.Text = Val.ToString(amount);
            }
            else
            {
                txtAmount.Text = Val.ToString("0");
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
            MFGJangedIssue objMFGJangedIssue = new MFGJangedIssue();
            DataTable DTab_IssueJanged = objMFGJangedIssue.GetDataDetails(Val.ToInt64(txtJangedNo.Text));

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
        }

        private void lueParty_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (e.Button.Index == 1)
                {
                    FrmPartyMaster frmParty = new FrmPartyMaster();
                    frmParty.ShowDialog();
                    Global.LOOKUPParty(lueParty);
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
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

        private void lueClarity_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (e.Button.Index == 1)
                {
                    FrmMfgRoughClarityMaster frmRoughClarity = new FrmMfgRoughClarityMaster();
                    frmRoughClarity.ShowDialog();
                    Global.LOOKUPClarity(lueClarity);
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
                MFGJangedIssue objMFGJangedIssue = new MFGJangedIssue();
                MFGJangedIssue_Property objMFGJangedIssueProperty = new MFGJangedIssue_Property();
                Conn = new BeginTranConnection(true, false);

                IntRes = 0;
                Issue_IntRes = 0;
                JangedNo_IntRes = 0;
                Dept_IntRes = 0;
                Janged_IntRes = 0;
                JangedSrNo_IntRes = 0;
                Int64 NewHistory_Union_Id = 0;
                Int64 Lot_SrNo = 0;

                int IntCounter = 0;
                int Count = 0;
                int TotalCount = m_dtbIssueProcess.Rows.Count;
                try
                {
                    foreach (DataRow drw in m_dtbIssueProcess.Rows)
                    {
                        objMFGJangedIssueProperty.janged_id = Val.ToInt(drw["janged_id"]);
                        objMFGJangedIssueProperty.janged_no = JangedNo_IntRes;
                        objMFGJangedIssueProperty.lot_id = Val.ToInt(drw["lot_id"]);
                        objMFGJangedIssueProperty.union_id = IntRes;
                        objMFGJangedIssueProperty.kapan_id = Val.ToInt(drw["kapan_id"]);
                        objMFGJangedIssueProperty.rough_cut_id = Val.ToInt(drw["rough_cut_id"]);
                        objMFGJangedIssueProperty.janged_date = Val.DBDate(Val.ToString(drw["janged_date"]));
                        objMFGJangedIssueProperty.to_company_id = Val.ToInt(lueToCompany.EditValue);
                        objMFGJangedIssueProperty.to_branch_id = Val.ToInt(lueToBranch.EditValue);
                        objMFGJangedIssueProperty.to_location_id = Val.ToInt(lueToLocation.EditValue);
                        objMFGJangedIssueProperty.to_department_id = Val.ToInt(lueToDepartment.EditValue);
                        objMFGJangedIssueProperty.manager_id = Val.ToInt(drw["manager_id"]);
                        objMFGJangedIssueProperty.to_manager_id = Val.ToInt(drw["to_manager_id"]); //Val.ToInt(lueManager.EditValue);
                        objMFGJangedIssueProperty.employee_id = Val.ToInt(drw["employee_id"]);

                        objMFGJangedIssueProperty.process_id = Val.ToInt(drw["process_id"]);

                        DataTable Dtab_Process_Setting = objMFGJangedIssue.GetData_Process_Setting(objMFGJangedIssueProperty);

                        if (Dtab_Process_Setting.Rows.Count > 0)
                        {
                            objMFGJangedIssueProperty.is_process_setting_flag = Val.ToInt(Dtab_Process_Setting.Rows[0]["is_flag"]);
                        }
                        else
                        {
                            objMFGJangedIssueProperty.is_process_setting_flag = Val.ToInt(0);
                        }

                        objMFGJangedIssueProperty.sub_process_id = Val.ToInt(drw["sub_process_id"]);

                        objMFGJangedIssueProperty.rough_sieve_id = Val.ToInt(drw["rough_sieve_id"]);
                        objMFGJangedIssueProperty.purity_id = Val.ToInt(drw["purity_id"]);
                        objMFGJangedIssueProperty.quality_id = Val.ToInt(drw["quality_id"]);
                        objMFGJangedIssueProperty.rough_clarity_id = Val.ToInt(drw["rough_clarity_id"]);
                        objMFGJangedIssueProperty.party_id = Val.ToInt(drw["party_id"]);
                        objMFGJangedIssueProperty.pcs = Val.ToInt(drw["pcs"]);
                        objMFGJangedIssueProperty.carat = Val.ToDecimal(drw["carat"]);
                        objMFGJangedIssueProperty.rate = Val.ToDecimal(drw["rate"]);
                        objMFGJangedIssueProperty.amount = Val.ToDecimal(drw["amount"]);
                        objMFGJangedIssueProperty.form_id = Val.ToInt(m_numForm_id);
                        objMFGJangedIssueProperty.is_outside = Val.ToInt(outside);
                        objMFGJangedIssueProperty.issue_union_id = Issue_IntRes;
                        objMFGJangedIssueProperty.janged_union_id = Janged_IntRes;
                        objMFGJangedIssueProperty.dept_union_id = Dept_IntRes;
                        objMFGJangedIssueProperty.history_union_id = NewHistory_Union_Id;
                        objMFGJangedIssueProperty.janged_Srno = JangedSrNo_IntRes;
                        objMFGJangedIssueProperty.lot_srno = Lot_SrNo;

                        objMFGJangedIssueProperty = objMFGJangedIssue.Save(objMFGJangedIssueProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                        IntRes = objMFGJangedIssueProperty.union_id;
                        Issue_IntRes = objMFGJangedIssueProperty.issue_union_id;
                        Janged_IntRes = objMFGJangedIssueProperty.janged_union_id;
                        JangedNo_IntRes = objMFGJangedIssueProperty.janged_no;
                        Dept_IntRes = objMFGJangedIssueProperty.dept_union_id;
                        NewHistory_Union_Id = Val.ToInt64(objMFGJangedIssueProperty.history_union_id);
                        JangedSrNo_IntRes = Val.ToInt64(objMFGJangedIssueProperty.janged_Srno);
                        Lot_SrNo = Val.ToInt64(objMFGJangedIssueProperty.lot_srno);

                        Count++;
                        IntCounter++;
                        SetControlPropertyValue(lblProgressCount, "Text", Count.ToString() + "" + "/" + "" + TotalCount.ToString() + " Completed....");
                    }

                    if (IntAutoConfirm == 1)
                    {
                        objMFGJangedIssueProperty = new MFGJangedIssue_Property();
                        objMFGJangedIssueProperty.janged_date = Val.DBDate(dtpIssueDate.Text);
                        objMFGJangedIssueProperty.janged_Srno = Val.ToInt64(JangedSrNo_IntRes);
                        objMFGJangedIssueProperty.receive_date = Val.DBDate(dtpIssueDate.Text);
                        objMFGJangedIssueProperty.to_department_id = Val.ToInt(lueToDepartment.EditValue);

                        IntRes += objMFGJangedIssue.SaveIssueJangedGoodsReceive(objMFGJangedIssueProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
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
                    DialogResult result = MessageBox.Show("Janged Issue Succesfully and janged no is : " + JangedNo_IntRes + " Are you sure print this janged?", "Confirmation", MessageBoxButtons.YesNoCancel);
                    if (result != DialogResult.Yes)
                    {
                        btnSave.Enabled = true;
                        ClearDetails();
                        return;
                    }

                    MFGJangedIssue objMFGJangedIssue = new MFGJangedIssue();
                    DataTable DTab_IssueJanged = objMFGJangedIssue.GetDataDetails(Val.ToInt64(JangedNo_IntRes));

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
        private void lueCutNo_Validated(object sender, EventArgs e)
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
                                if (txtLotID.Text != string.Empty || Val.ToInt64(txtLotID.Text) != 0)
                                {
                                    GetOsCarat(Val.ToInt64(txtLotID.Text));
                                    DataTable dtIssOS = new DataTable();
                                    dtIssOS = objProcessRecieve.Carat_OutStanding_GetData(Val.ToInt64(txtLotID.Text), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue), 0, "I");

                                    if (dtIssOS.Rows.Count > 0)
                                    {
                                        m_OScarat = Val.ToDecimal(Val.ToDecimal(dtIssOS.Rows[0]["carat"]));
                                        txtBalCarat.Text = Val.ToString(m_OScarat);
                                        lblOsCarat.Text = Val.ToString(m_OScarat);
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
                            DataRow Drow = grvJangedIssue.GetDataRow(e.RowHandle);
                            btnAdd.Text = "&Update";
                            lueCutNo.Text = Val.ToString(Drow["cut_no"]);
                            lueCutNo.EditValue = Val.ToInt(Drow["rough_cut_id"]);
                            txtLotID.Text = Val.ToString(Drow["lot_id"]);
                            txtPcs.Text = Val.ToString(Drow["pcs"]);
                            txtIssCarat.Text = Val.ToString(Drow["carat"]);
                            txtRate.Text = Val.ToString(Drow["rate"]);
                            txtAmount.Text = Val.ToString(Drow["amount"]);
                            lueManager.EditValue = Val.ToInt(Drow["to_manager_id"]);
                            lueProcess.EditValue = Val.ToInt(Drow["process_id"]);
                            lueSubProcess.EditValue = Val.ToInt(Drow["sub_process_id"]);
                            lueSieve.EditValue = Val.ToInt(Drow["rough_sieve_id"]);
                            luePurity.EditValue = Val.ToInt(Drow["purity_id"]);
                            lueQuality.EditValue = Val.ToInt(Drow["quality_id"]);
                            lueClarity.EditValue = Val.ToInt(Drow["rough_clarity_id"]);
                            lueParty.EditValue = Val.ToInt(Drow["party_id"]);
                            m_numcarat = Val.ToDecimal(Drow["carat"]);
                            m_cut_no = Val.ToString(Drow["cut_no"]);
                            m_update_srno = Val.ToInt(Drow["sr_no"]);
                            m_old_carat = Val.ToDecimal(Drow["carat"]);
                            m_old_rate = Val.ToDecimal(Drow["rate"]);
                            m_old_amount = Val.ToDecimal(Drow["amount"]);
                            m_fromManagerId = Val.ToInt(Drow["manager_id"]);
                            m_flag = Val.ToInt(1);
                            m_blnflag = true;
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
                        txtJangedNo.Text = Val.ToString(Val.ToString(Drow["janged_no"]));
                        dtpIssueDate.Text = Val.DBDate(Val.ToString(Drow["janged_date"]));
                        lueToCompany.EditValue = Val.ToInt(Drow["company_id"]);
                        lueToBranch.EditValue = Val.ToInt(Drow["branch_id"]);
                        lueToLocation.EditValue = Val.ToInt(Drow["location_id"]);
                        lueToDepartment.EditValue = Val.ToInt(Drow["department_id"]);
                        lueParty.EditValue = Val.ToInt(Drow["party_id"]);
                        lueKapan.EditValue = Val.ToInt(Drow["kapan_id"]);
                        lueManager.EditValue = Val.ToInt(Drow["manager_id"]);
                        lueEmployee.EditValue = Val.ToInt(Drow["employee_id"]);

                        m_dtbIssueProcess = objMFGJangedIssue.GetDataDetails(Val.ToInt(txtJangedNo.Text));

                        grdJangedIssue.DataSource = m_dtbIssueProcess;
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
                    DataRow[] dr = m_dtbIssueProcess.Select("cut_no = '" + Val.ToString(lueCutNo.Text) + "' AND lot_id = " + Val.ToInt64(txtLotID.Text) + " AND rough_sieve_id = " + Val.ToInt(lueSieve.EditValue) + " AND purity_id = " + Val.ToInt(luePurity.EditValue));

                    if (dr.Count() == 1)
                    {
                        Global.Message("Record already exists in grid", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lueManager.Focus();
                        blnReturn = false;
                        return blnReturn;
                    }
                    DataRow[] drCut = m_dtbIssueProcess.Select("cut_no <> '" + Val.ToString(lueCutNo.Text) + "'");
                    if (drCut.Count() > 1)
                    {
                        Global.Message("Different cut selected please check", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lueManager.Focus();
                        blnReturn = false;
                        return blnReturn;
                    }

                    DataRow drwNew = m_dtbIssueProcess.NewRow();
                    int numPcs = Val.ToInt(txtPcs.Text);
                    decimal numCarat = Val.ToDecimal(txtIssCarat.Text);
                    decimal numRate = Val.ToDecimal(txtRate.Text);
                    decimal numAmount = Val.ToDecimal(txtAmount.Text);

                    drwNew["janged_id"] = Val.ToInt(0);
                    drwNew["janged_date"] = Val.DBDate(dtpIssueDate.Text);

                    drwNew["cut_no"] = Val.ToString(lueCutNo.Text);
                    drwNew["rough_cut_id"] = Val.ToString(lueCutNo.EditValue);
                    drwNew["lot_id"] = Val.ToInt64(txtLotID.Text);
                    drwNew["to_manager_id"] = Val.ToInt(lueManager.EditValue);
                    drwNew["to_manager"] = Val.ToString(lueManager.Text);
                    drwNew["manager_id"] = Val.ToInt(m_fromManagerId);
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
                    drwNew["quality_id"] = Val.ToInt(lueQuality.EditValue);
                    drwNew["quality_name"] = Val.ToString(lueQuality.Text);
                    drwNew["rough_clarity_id"] = Val.ToInt(lueClarity.EditValue);
                    drwNew["rough_clarity_name"] = Val.ToString(lueClarity.Text);
                    drwNew["party_id"] = Val.ToInt(lueParty.EditValue);
                    drwNew["party_name"] = Val.ToString(lueParty.Text);
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
                            if (m_dtbIssueProcess.Select("cut_no ='" + m_cut_no + "' AND sr_no = '" + m_update_srno + "'").Length > 0)
                            {
                                if (m_dtbIssueProcess.Rows[grvJangedIssue.FocusedRowHandle]["cut_no"].ToString() == m_cut_no.ToString())
                                {
                                    m_dtbIssueProcess.Rows[grvJangedIssue.FocusedRowHandle]["lot_id"] = Val.ToString(txtLotID.Text);
                                    m_dtbIssueProcess.Rows[grvJangedIssue.FocusedRowHandle]["manager_id"] = Val.ToInt(m_fromManagerId);
                                    m_dtbIssueProcess.Rows[grvJangedIssue.FocusedRowHandle]["to_manager_id"] = Val.ToInt(lueManager.EditValue);
                                    m_dtbIssueProcess.Rows[grvJangedIssue.FocusedRowHandle]["to_manager"] = Val.ToString(lueManager.Text);
                                    m_dtbIssueProcess.Rows[grvJangedIssue.FocusedRowHandle]["employee_id"] = Val.ToInt(lueEmployee.EditValue);
                                    m_dtbIssueProcess.Rows[grvJangedIssue.FocusedRowHandle]["employee"] = Val.ToString(lueEmployee.Text);
                                    m_dtbIssueProcess.Rows[grvJangedIssue.FocusedRowHandle]["process_id"] = Val.ToInt(lueProcess.EditValue);
                                    m_dtbIssueProcess.Rows[grvJangedIssue.FocusedRowHandle]["process"] = Val.ToString(lueProcess.Text);
                                    m_dtbIssueProcess.Rows[grvJangedIssue.FocusedRowHandle]["sub_process_id"] = Val.ToInt(lueSubProcess.EditValue);
                                    m_dtbIssueProcess.Rows[grvJangedIssue.FocusedRowHandle]["subprocess"] = Val.ToString(lueSubProcess.Text);
                                    m_dtbIssueProcess.Rows[grvJangedIssue.FocusedRowHandle]["rough_sieve_id"] = Val.ToInt(lueSieve.EditValue);
                                    m_dtbIssueProcess.Rows[grvJangedIssue.FocusedRowHandle]["sieve_name"] = Val.ToString(lueSieve.Text);
                                    m_dtbIssueProcess.Rows[grvJangedIssue.FocusedRowHandle]["purity_id"] = Val.ToInt(luePurity.EditValue);
                                    m_dtbIssueProcess.Rows[grvJangedIssue.FocusedRowHandle]["purity_name"] = Val.ToString(luePurity.Text);
                                    m_dtbIssueProcess.Rows[grvJangedIssue.FocusedRowHandle]["quality_id"] = Val.ToInt(lueQuality.EditValue);
                                    m_dtbIssueProcess.Rows[grvJangedIssue.FocusedRowHandle]["quality_name"] = Val.ToString(lueQuality.Text);
                                    m_dtbIssueProcess.Rows[grvJangedIssue.FocusedRowHandle]["rough_clarity_id"] = Val.ToInt(lueClarity.EditValue);
                                    m_dtbIssueProcess.Rows[grvJangedIssue.FocusedRowHandle]["rough_clarity_name"] = Val.ToString(lueClarity.Text);
                                    m_dtbIssueProcess.Rows[grvJangedIssue.FocusedRowHandle]["party_id"] = Val.ToInt(lueParty.EditValue);
                                    m_dtbIssueProcess.Rows[grvJangedIssue.FocusedRowHandle]["party_name"] = Val.ToString(lueParty.Text);
                                    m_dtbIssueProcess.Rows[grvJangedIssue.FocusedRowHandle]["pcs"] = Val.ToInt(txtPcs.Text).ToString();
                                    m_dtbIssueProcess.Rows[grvJangedIssue.FocusedRowHandle]["carat"] = Val.ToDecimal(txtIssCarat.Text).ToString();
                                    m_dtbIssueProcess.Rows[grvJangedIssue.FocusedRowHandle]["rate"] = Val.ToDecimal(txtRate.Text).ToString();
                                    m_dtbIssueProcess.Rows[grvJangedIssue.FocusedRowHandle]["amount"] = Val.ToDecimal(txtAmount.Text).ToString();
                                    m_dtbIssueProcess.Rows[grvJangedIssue.FocusedRowHandle]["prd_id"] = Val.ToInt(m_prd_id);
                                    m_dtbIssueProcess.Rows[grvJangedIssue.FocusedRowHandle]["kapan_id"] = Val.ToInt(m_kapan_id);
                                    m_flag = Val.ToInt(0);

                                    break;
                                }
                            }
                        }
                    }
                    btnAdd.Text = "&Add";
                }
                grvJangedIssue.MoveLast();

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
                    if (Val.ToDecimal(lblOsCarat.Text) < ((Val.ToDecimal(txtIssCarat.Text) + Val.ToDecimal(clmCarat.SummaryItem.SummaryValue)) - m_old_carat))
                    {
                        lstError.Add(new ListError(5, "Issue carat more than balance carat"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtIssCarat.Focus();
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
                    if (lueSieve.Text == "")
                    {
                        lstError.Add(new ListError(13, "Sieve"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueSieve.Focus();
                        }
                    }
                    if (luePurity.Text == "")
                    {
                        lstError.Add(new ListError(13, "Purity"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            luePurity.Focus();
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
                    //if (m_dtbIssueProcess.Rows.Count > 0)
                    //{

                    //    DataRow[] drParty = m_dtbIssueProcess.Select("party_name <> '" + Val.ToString(lueParty.Text) + "'");
                    //    if (drParty.Count() > 0)
                    //    {
                    //        lstError.Add(new ListError(5, "Different party selected please check"));
                    //        if (!blnFocus)
                    //        {
                    //            blnFocus = true;
                    //            lueParty.Focus();
                    //        }
                    //    }
                    //    DataRow[] drManager = m_dtbIssueProcess.Select("to_manager <> '" + Val.ToString(lueManager.Text) + "'");
                    //    if (drManager.Count() > 0)
                    //    {
                    //        lstError.Add(new ListError(5, "Different Manager selected please check"));
                    //        if (!blnFocus)
                    //        {
                    //            blnFocus = true;
                    //            lueManager.Focus();
                    //        }
                    //    }
                    //    DataRow[] drEmployee = m_dtbIssueProcess.Select("employee <> '" + Val.ToString(lueEmployee.Text) + "'");
                    //    if (drEmployee.Count() > 0)
                    //    {
                    //        lstError.Add(new ListError(5, "Different Employee selected please check"));
                    //        if (!blnFocus)
                    //        {
                    //            blnFocus = true;
                    //            lueEmployee.Focus();
                    //        }
                    //    }
                    //}

                    if (lueProcess.Text != "ASSORT" && lueProcess.Text != "AKHU BHAR")
                    {
                        if (Val.ToDecimal(lblOsCarat.Text) != Val.ToDecimal(txtIssCarat.Text))
                        {
                            lstError.Add(new ListError(5, "Balance carat not equal issue carat"));
                            if (!blnFocus)
                            {
                                blnFocus = true;
                                lueProcess.Focus();
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

                lueKapan.EditValue = System.DBNull.Value;
                lueCutNo.EditValue = System.DBNull.Value;
                lueManager.EditValue = System.DBNull.Value;
                lueEmployee.EditValue = System.DBNull.Value;

                lueProcess.EditValue = System.DBNull.Value;
                lueSubProcess.EditValue = System.DBNull.Value;

                lueSieve.EditValue = System.DBNull.Value;
                luePurity.EditValue = System.DBNull.Value;

                lueClarity.EditValue = System.DBNull.Value;
                lueQuality.EditValue = System.DBNull.Value;
                lueParty.EditValue = System.DBNull.Value;

                lueToCompany.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.company_id);
                lueToBranch.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.branch_id);
                lueToLocation.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.location_id);
                lueToDepartment.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.department_id);

                txtPcs.Text = string.Empty;
                txtBalCarat.Text = string.Empty;
                txtLotID.Text = string.Empty;
                txtIssCarat.Text = string.Empty;
                txtJangedNo.Text = string.Empty;
                lblOsPcs.Text = "0";
                lblOsCarat.Text = "0.00";
                m_blnflag = false;
                m_flag = 0;
                m_Srno = 1;
                m_update_srno = 0;
                m_numcarat = 0;
                m_old_carat = 0;
                m_old_rate = 0;
                m_old_amount = 0;
                m_kapan_id = 0;
                btnAdd.Text = "&Add";
                btnSave.Enabled = true;
                m_blncheckupdate = true;
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
                grdJangedIssue.DataSource = m_dtbIssueProcess;
                grdJangedIssue.Refresh();
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
                else
                {
                    BLL.General.ShowErrors("Cut No not Found");
                    txtPcs.Text = "0";
                    txtIssCarat.Text = "0";
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
                            grvJangedIssue.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            grvJangedIssue.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            grvJangedIssue.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            grvJangedIssue.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            grvJangedIssue.ExportToText(Filepath);
                            break;
                        case "html":
                            grvJangedIssue.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            grvJangedIssue.ExportToCsv(Filepath);
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
        private void btnPopUpStock_Click(object sender, EventArgs e)
        {
            GetPendingStock();
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
                if (lueSieve.Text == "")
                {
                    lstError.Add(new ListError(13, "Sieve"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueSieve.Focus();
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

                    DtPending = objMFGProcessIssue.GetPendingStock(objMFGProcessIssueProperty);

                    FrmMFGStockConfirm FrmStockConfirm = new FrmMFGStockConfirm();
                    FrmStockConfirm.FrmMFGJangedIssue = this;
                    FrmStockConfirm.DTab = DtPending;
                    FrmStockConfirm.ShowForm(this);
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
                grdJangedIssue.DataSource = m_dtbIssueProcess;
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
                    if (lueProcess.Text == "CHIPIYO FINAL" || lueProcess.Text == "ASSORT" || lueProcess.Text == "CHIPIYO")
                    {
                        MFGProcessIssue objMFGProcessIssue = new MFGProcessIssue();
                        DataTable dtChipyoRate = new DataTable();

                        dtChipyoRate = objMFGProcessIssue.GetOSRate(Val.ToInt(lueCutNo.EditValue), Val.ToInt64(txtLotID.Text), Val.ToString(lueProcess.Text));

                        if (dtChipyoRate.Rows.Count > 0)
                        {
                            m_chipyo_rate = Val.ToDecimal(dtChipyoRate.Rows[0]["rate"]);
                        }
                        else
                        {
                            m_chipyo_rate = 0;
                        }
                        txtRate.Text = Val.ToString(Math.Round(m_chipyo_rate, 2));
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
                if (Val.ToInt(lueEmployee.EditValue) > 0 && Val.ToInt(lueManager.EditValue) > 0 && Val.ToInt(lueSubProcess.EditValue) > 0)
                {
                    btnAdd.Enabled = true;
                }
                else
                {
                    btnAdd.Enabled = false;
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
    }
}

