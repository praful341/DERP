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
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using static DERP.Class.Global;

namespace DERP.Transaction.MFG
{
    public partial class FrmMFGJangedIssueLotting : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        Control _NextEnteredControl;
        private List<Control> _tabControls;
        DataTable DtControlSettings;

        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        MFGEmployeeTarget objEmployeeTarget;
        MFGSawableRecieve objSawableRecieve;
        MFGProcessReceive objProcessRecieve;
        MfgRoughSieve objRoughSieve;
        MfgRoughClarityMaster objClarity;

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
        public FrmMFGJangedIssueLotting()
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

            DtControlSettings = new DataTable();
            _tabControls = new List<Control>();

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
            try
            {
                foreach (Control control in controls)
                {
                    if (control.TabStop)
                        _tabControls.Add(control);
                    if (control.HasChildren)
                        TabControlsToList(control.Controls);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
                grvJangedIssueLotting.DeleteRow(grvJangedIssueLotting.GetRowHandle(grvJangedIssueLotting.FocusedRowHandle));
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
                Global.LOOKUPAllManager(lueManager);
                Global.LOOKUPProcess(lueProcess);
                Global.LOOKUPSubProcess(lueSubProcess);
                Global.LOOKUPParty(lueParty);
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
                string Str = "";
                if (Val.DBDate(BLL.GlobalDec.gStrServerDate) != Val.DBDate(dtpIssueDate.Text))
                {
                    Str = GlobalDec.CheckLockIsOpenOrNot(Val.DBDate(dtpIssueDate.Text), Val.DBTime(DateTime.Now.ToShortTimeString())); //Val.GetFullTime12());
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
                        dtpIssueDate.Enabled = true;
                        dtpIssueDate.Visible = true;
                    }
                }
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
            Global.Export("xlsx", grvJangedIssueLotting);
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
                    if (Val.ToInt64(txtLotID.Text) != 0)
                    {
                        if (lueToDepartment.Text == "4P")
                        {

                            if (Global.CheckEstimationDoneOrNot(Val.ToInt64(txtLotID.Text)).ToString().Trim().Equals(string.Empty))
                            {
                                Global.Message("Estimation Entry Not Done in this Lot ID =" + Val.ToInt64(txtLotID.Text));
                                return;
                            }
                        }

                        DataTable DtConfirm = Global.CheckConfirmLot(Val.ToInt64(txtLotID.Text));
                        if (DtConfirm.Rows.Count == 0)
                        {
                            Global.Message("Please Confirm Lot First!!!");
                            return;
                        }
                    }

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
                        DataTable DTab_ValidateLotID = Global.GetRoughJangedStockWise(Val.ToInt(lueKapan.EditValue), Val.ToInt64(txtLotID.Text));

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
                        //DTabTemp = Global.GetRoughJangedStockWise(Val.ToInt(lueKapan.EditValue), Val.ToInt64(txtLotID.Text));

                        if (DTab_ValidateLotID.Rows.Count > 0)
                        {
                            txtLotID.Text = "";
                            txtLotID.Focus();
                        }
                        m_dtbIssueProcess.Merge(DTab_ValidateLotID);
                    }
                    else
                    {
                        //DataTable DTab_ValidateLotID = Global.GetRoughJangedStockWise(Val.ToInt(lueKapan.EditValue), Val.ToInt64(txtLotID.Text));
                        m_dtbIssueProcess = Global.GetRoughJangedStockWise(Val.ToInt(lueKapan.EditValue), Val.ToInt64(txtLotID.Text));

                        if (m_dtbIssueProcess.Rows.Count > 0)
                        {
                        }
                        else
                        {
                            Global.Message("Lot ID Not Issue in Janged");
                            txtLotID.Text = "";
                            txtLotID.Focus();
                            return;
                        }

                        //m_dtbIssueProcess = Global.GetRoughJangedStockWise(Val.ToInt(lueKapan.EditValue), Val.ToInt64(txtLotID.Text));

                        if (m_dtbIssueProcess.Rows.Count > 0)
                        {
                            if (Val.ToInt64(txtLotID.Text) != 0)
                            {
                                DataTable DtConfirm = Global.CheckConfirmLot(Val.ToInt64(txtLotID.Text));
                                if (DtConfirm.Rows.Count == 0)
                                {
                                    Global.Message("Please Confirm Lot First!!!");
                                    return;
                                }
                            }
                            txtLotID.Text = "";
                            txtLotID.Focus();
                        }
                    }

                    grdJangedIssueLotting.DataSource = m_dtbIssueProcess;
                    grdJangedIssueLotting.RefreshDataSource();
                    dgvJangedList.BestFitColumns();
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
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
            //if (m_LotId == 1)
            //{
            //    lueCutNo.EditValue = Val.ToInt64(m_cutId);
            //}
            //m_cutId = 0;
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (lueToDepartment.Text == "POLISH" || lueToDepartment.Text == "SOWABALE POLISH")
            {
                MFGJangedIssue objMFGJangedIssue = new MFGJangedIssue();
                DataTable DTab_IssueJanged = objMFGJangedIssue.Polish_GetDataDetails(Val.ToInt64(txtJangedNo.Text));

                ClearDetails();
                FrmReportViewer FrmReportViewer = new FrmReportViewer();
                FrmReportViewer.DS.Tables.Add(DTab_IssueJanged);
                FrmReportViewer.GroupBy = "";
                FrmReportViewer.RepName = "";
                FrmReportViewer.RepPara = "";
                this.Cursor = Cursors.Default;
                FrmReportViewer.AllowSetFormula = true;

                FrmReportViewer.ShowForm_SubReport("Janged_Issue_Lalubhai_Main", 120, FrmReportViewer.ReportFolder.ACCOUNT);

                DTab_IssueJanged = null;
                FrmReportViewer.DS.Tables.Clear();
                FrmReportViewer.DS.Clear();
                FrmReportViewer = null;
                btnSave.Enabled = true;

            }
            else
            {
                MFGJangedIssue objMFGJangedIssue = new MFGJangedIssue();
                DataTable DTab_IssueJanged = objMFGJangedIssue.GetDataDetails(Val.ToInt64(txtJangedNo.Text));

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
                txtJangedNo.Text = "";
                //lueKapan.Focus();
            }
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
        private void backgroundWorker_ProcessIssue_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                MFGJangedIssue objMFGJangedIssue = new MFGJangedIssue();
                MFGJangedIssue_Property objMFGJangedIssueProperty = new MFGJangedIssue_Property();
                //foreach (DataRow drw in m_dtbIssueProcess.Rows)
                //{
                //    int lotting_dept_id = 0;
                //    //lotting_dept_id = objMFGJangedIssue.GetLottingDepartment(Val.ToInt(drw["lot_id"]));
                //    if(lotting_dept_id != Val.ToInt(lueToDepartment.EditValue))
                //    {
                //        Global.Message("Lotting Department Different in This Lot Id " + Val.ToInt(drw["lot_id"]));
                //        return;
                //    }

                //}

                if (lueToDepartment.Text == "POLISH")
                {
                    if (m_dtbIssueProcess.Rows.Count > 9)
                    {
                        Global.Message("Maximum 9 Lot ID Insert In Grid...");
                        return;
                    }
                }

                Conn = new BeginTranConnection(true, false);

                IntRes = 0;
                Issue_IntRes = 0;
                JangedNo_IntRes = 0;
                Dept_IntRes = 0;
                Janged_IntRes = 0;
                JangedSrNo_IntRes = 0;
                Int64 NewHistory_Union_Id = 0;
                Int64 Lot_SrNo = 0;
                if (lblMode.Text == "EDIT")
                {
                    Lot_SrNo = Val.ToInt32(btnSave.Tag);
                    JangedNo_IntRes = Val.ToInt32(lblMode.Tag);
                }
                int IntCounter = 0;
                int Count = 0;
                int TotalCount = m_dtbIssueProcess.Rows.Count;
                int to_branchId = Val.ToInt(lueToBranch.EditValue);
                try
                {
                    if ((Val.ToString(lueToDepartment.Text) == "GALAXY" && Val.ToString(GlobalDec.gEmployeeProperty.department_name) == "MAKABLE" && Val.ToString(lueToBranch.Text) == "AMBIKA") || (Val.ToString(lueToDepartment.Text) == "GALAXY DW" && Val.ToString(GlobalDec.gEmployeeProperty.department_name) == "MAKABLE" && Val.ToString(lueToBranch.Text) == "AMBIKA"))
                    {
                        //if (m_dtbIssueProcess.Rows.Count > 500)
                        //{
                        //    Global.Message("Maximum 500 Lot ID Insert In Grid...");
                        //    return;
                        //}
                    }
                    foreach (DataRow drw in m_dtbIssueProcess.Rows)
                    {
                        objMFGJangedIssueProperty.janged_id = Val.ToInt(0); //Val.ToInt(drw["janged_id"]);
                        objMFGJangedIssueProperty.janged_no = JangedNo_IntRes;
                        objMFGJangedIssueProperty.lot_id = Val.ToInt64(drw["lot_id"]);
                        objMFGJangedIssueProperty.union_id = IntRes;
                        objMFGJangedIssueProperty.kapan_id = Val.ToInt32(drw["kapan_id"]);  //Val.ToInt(lueKapan.EditValue); // Val.ToInt(drw["kapan_id"]);
                        objMFGJangedIssueProperty.rough_cut_id = Val.ToInt32(drw["rough_cut_id"]); //Val.ToInt(lueCutNo.EditValue); // Val.ToInt(drw["rough_cut_id"]);
                        objMFGJangedIssueProperty.janged_date = Val.DBDate(dtpIssueDate.Text); // Val.DBDate(Val.ToString(drw["janged_date"]));
                        objMFGJangedIssueProperty.to_company_id = Val.ToInt(lueToCompany.EditValue);
                        objMFGJangedIssueProperty.to_branch_id = Val.ToInt(to_branchId);
                        objMFGJangedIssueProperty.to_location_id = Val.ToInt(lueToLocation.EditValue);
                        objMFGJangedIssueProperty.to_department_id = Val.ToInt(lueToDepartment.EditValue);
                        objMFGJangedIssueProperty.manager_id = Val.ToInt(lueManager.EditValue); //Val.ToInt(drw["manager_id"]);
                        objMFGJangedIssueProperty.to_manager_id = Val.ToInt(lueManager.EditValue);
                        objMFGJangedIssueProperty.employee_id = Val.ToInt(0); // Val.ToInt(drw["employee_id"]);

                        objMFGJangedIssueProperty.process_id = Val.ToInt(lueProcess.EditValue); //Val.ToInt(drw["process_id"]);

                        DataTable Dtab_Process_Setting = objMFGJangedIssue.GetData_Process_Setting(objMFGJangedIssueProperty);

                        if (Dtab_Process_Setting.Rows.Count > 0)
                        {
                            objMFGJangedIssueProperty.is_process_setting_flag = Val.ToInt(Dtab_Process_Setting.Rows[0]["is_flag"]);
                        }
                        else
                        {
                            objMFGJangedIssueProperty.is_process_setting_flag = Val.ToInt(0);
                        }

                        objMFGJangedIssueProperty.sub_process_id = Val.ToInt(lueSubProcess.EditValue); // Val.ToInt(drw["sub_process_id"]);

                        objMFGJangedIssueProperty.rough_sieve_id = Val.ToInt(drw["rough_sieve_id"]);
                        objMFGJangedIssueProperty.purity_id = Val.ToInt(drw["purity_id"]);
                        objMFGJangedIssueProperty.quality_id = Val.ToInt(drw["quality_id"]);
                        objMFGJangedIssueProperty.rough_clarity_id = Val.ToInt(drw["rough_clarity_id"]);
                        objMFGJangedIssueProperty.party_id = Val.ToInt(lueParty.EditValue); // Val.ToInt(drw["party_id"]);
                        objMFGJangedIssueProperty.pcs = Val.ToInt(drw["pcs"]);
                        objMFGJangedIssueProperty.carat = Val.ToDecimal(drw["carat"]);
                        objMFGJangedIssueProperty.rate = Val.ToDecimal(drw["rate"]);
                        objMFGJangedIssueProperty.amount = Val.ToDecimal(drw["amount"]);
                        //objMFGProcessIssueProperty.prd_id = Val.ToInt(drw["prd_id"]);
                        objMFGJangedIssueProperty.form_id = Val.ToInt(m_numForm_id);
                        objMFGJangedIssueProperty.is_outside = Val.ToInt(outside);
                        objMFGJangedIssueProperty.issue_union_id = Issue_IntRes;
                        objMFGJangedIssueProperty.janged_union_id = Janged_IntRes;
                        objMFGJangedIssueProperty.dept_union_id = Dept_IntRes;
                        objMFGJangedIssueProperty.history_union_id = NewHistory_Union_Id;
                        objMFGJangedIssueProperty.janged_Srno = JangedSrNo_IntRes;
                        objMFGJangedIssueProperty.lot_srno = Lot_SrNo;
                        if (lblMode.Text == "EDIT")
                        {
                            objMFGJangedIssueProperty.action = 1;
                        }
                        else
                        {
                            objMFGJangedIssueProperty.action = 0;
                        }

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

                    if ((Val.ToString(lueToDepartment.Text) == "GALAXY" && Val.ToString(GlobalDec.gEmployeeProperty.department_name) == "MAKABLE" && Val.ToString(lueToBranch.Text) == "AMBIKA") || (Val.ToString(lueToDepartment.Text) == "GALAXY DW" && Val.ToString(GlobalDec.gEmployeeProperty.department_name) == "MAKABLE" && Val.ToString(lueToBranch.Text) == "AMBIKA"))
                    {
                        DateTime date_month = Convert.ToDateTime(dtpIssueDate.Text);
                        string text_days = date_month.ToString("dd");
                        string text_month = date_month.ToString("MM");
                        int count = m_dtbIssueProcess.Rows.Count;

                        String Todaysdate = DateTime.Now.ToString("dd-MM-yyyy");
                        if (!Directory.Exists("\\Galaxy_XML\\" + Todaysdate))
                        {
                            Directory.CreateDirectory("\\Galaxy_XML\\" + Todaysdate);
                        }
                        //if (!Directory.Exists("D:\\DERP_Software\\Galaxy_XML\\" + Todaysdate))
                        //{
                        //    Directory.CreateDirectory("D:\\DERP_Software\\Galaxy_XML\\" + Todaysdate);
                        //}

                        var newValue = "\\Galaxy_XML\\" + Todaysdate + "\\";
                        //XmlTextWriter xmlWriter = new XmlTextWriter(@"D:\\DERP_Software\\Galaxy_XML\\ " + Todaysdate + "\\" + m_dtbIssueProcess.Rows[0]["kapan_no"].ToString() + "(" + text_days + "-" + text_month + ")(" + count + "-" + JangedNo_IntRes + ")" + ".xml", Encoding.UTF8);
                        XmlTextWriter xmlWriter = new XmlTextWriter(newValue + m_dtbIssueProcess.Rows[0]["kapan_no"].ToString() + "(" + text_days + "-" + text_month + ")(" + count + "-" + JangedNo_IntRes + ")" + ".xml", Encoding.UTF8);


                        xmlWriter.WriteStartDocument(true);
                        xmlWriter.WriteStartElement("Galaxy"); //Root Element
                        xmlWriter.WriteStartElement("StoneCollection"); //Department Element

                        xmlWriter.WriteStartAttribute("version"); //Attribute "Name"
                        xmlWriter.WriteString("1.0.0"); //Attribute Value 
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartElement("stones"); //Started Employees Element
                        foreach (DataRow DR in m_dtbIssueProcess.Rows)
                        {
                            var parts = Val.ToString(DR["kapan_no"]).Split('-');

                            var cut_no = Val.ToString(DR["rough_cut_no"]).Split('-');

                            string last_cut_no = cut_no.Last();
                            string first_cut_no = cut_no.First();


                            xmlWriter.WriteStartElement("stone"); //Started Employee Element

                            DateTime dt = Convert.ToDateTime(dtpIssueDate.Text);
                            string text = dt.ToString("dd");

                            xmlWriter.WriteStartAttribute("weight");//Attribute "Age"
                            xmlWriter.WriteString(Val.ToString(DR["carat"]));//Attribute Value
                            xmlWriter.WriteEndAttribute();

                            xmlWriter.WriteStartAttribute("process_type");//Attribute "Age"
                            xmlWriter.WriteString(Val.ToString("Galaxy 1000"));//Attribute Value
                            xmlWriter.WriteEndAttribute();

                            xmlWriter.WriteStartAttribute("stone_name");//Attribute "Age"
                            //xmlWriter.WriteString(Val.ToString(first_cut_no + "-" + last_cut_no) + "-" + Val.ToString(DR["lot_no"]));//Attribute Value
                            xmlWriter.WriteString(Val.ToString(first_cut_no + cut_no[1] + last_cut_no) + "-" + Val.ToString(DR["lot_no"]));//Attribute Value
                            xmlWriter.WriteEndAttribute();

                            xmlWriter.WriteStartAttribute("package_name"); //Attribute "Name"
                            //xmlWriter.WriteString(Val.ToString(DR["kapan_no"] + "-" + JangedNo_IntRes + "(" + text + ")" + count)); //Attribute Value 
                            xmlWriter.WriteString(Val.ToString(parts[0] + "-" + JangedNo_IntRes + "(" + text + ")" + count)); //Attribute Value 
                            xmlWriter.WriteEndAttribute();

                            xmlWriter.WriteEndElement(); //End of Employee Element                    
                        }

                        xmlWriter.WriteEndElement(); //End of Employees Element
                        xmlWriter.WriteEndElement(); //End of Department Element
                        xmlWriter.WriteEndElement(); //End of Root Element

                        xmlWriter.WriteEndDocument();
                        xmlWriter.Flush();
                        xmlWriter.Close();
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
                    m_dtbIssueProcess = new DataTable();
                    DialogResult result = MessageBox.Show("Janged Issue Succesfully and janged no is : " + JangedNo_IntRes + " Are you sure print this janged?", "Confirmation", MessageBoxButtons.YesNoCancel);
                    if (result != DialogResult.Yes)
                    {
                        btnSave.Enabled = true;
                        ClearDetails();
                        return;
                    }



                    if (lueToDepartment.Text == "POLISH" || lueToDepartment.Text == "SOWABALE POLISH")
                    {
                        MFGJangedIssue objMFGJangedIssue = new MFGJangedIssue();
                        DataTable DTab_IssueJanged = objMFGJangedIssue.Polish_GetDataDetails(Val.ToInt64(JangedNo_IntRes));

                        ClearDetails();
                        FrmReportViewer FrmReportViewer = new FrmReportViewer();
                        FrmReportViewer.DS.Tables.Add(DTab_IssueJanged);
                        FrmReportViewer.GroupBy = "";
                        FrmReportViewer.RepName = "";
                        FrmReportViewer.RepPara = "";
                        this.Cursor = Cursors.Default;
                        FrmReportViewer.AllowSetFormula = true;

                        FrmReportViewer.ShowForm_SubReport("Janged_Issue_Lalubhai_Main", 120, FrmReportViewer.ReportFolder.ACCOUNT);

                        DTab_IssueJanged = null;
                        FrmReportViewer.DS.Tables.Clear();
                        FrmReportViewer.DS.Clear();
                        FrmReportViewer = null;
                        btnSave.Enabled = true;

                    }
                    else
                    {
                        MFGJangedIssue objMFGJangedIssue = new MFGJangedIssue();
                        DataTable DTab_IssueJanged = objMFGJangedIssue.GetDataDetails(Val.ToInt64(JangedNo_IntRes));

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
                }
                else if (lblMode.Text == "EDIT")
                {
                    Global.Confirm("Janged Issue Update Succesfully");
                    btnSave.Enabled = true;
                    ClearDetails();
                    return;
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
        private void IssueExcel_Click(object sender, EventArgs e)
        {
            Export_IssueList("xlsx", "Export to Excel", "Excel files 97-2003 (Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*");
        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Export("pdf", "Export Report to PDF", "PDF (*.PDF)|*.PDF");
        }
        private void lueToDepartment_EditValueChanged(object sender, EventArgs e)
        {
            if (lueToDepartment.Text == "POLISH")
            {
                lueProcess.Text = "POLISH";
                lueProcess.EditValue = Val.ToInt(8);
                lueSubProcess.Text = "POLISH";
                lueSubProcess.EditValue = Val.ToInt(11);

            }
            else if (lueToDepartment.Text == "RUSSIAN")
            {
                lueProcess.Text = "RUSSIAN";
                lueProcess.EditValue = Val.ToInt(2009);
                lueSubProcess.Text = "RUSSIAN";
                lueSubProcess.EditValue = Val.ToInt(2013);

            }
            else if (lueToDepartment.Text == "SARIN")
            {
                lueProcess.Text = "SARIN";
                lueProcess.EditValue = Val.ToInt(2007);
                lueSubProcess.Text = "SARIN";
                lueSubProcess.EditValue = Val.ToInt(6);

            }
            else
            {
                lueProcess.Text = "";
                lueProcess.EditValue = Val.ToInt(0);
                lueSubProcess.Text = "";
                lueSubProcess.EditValue = Val.ToInt(0);
            }
            if (Val.ToString(lueToDepartment.Text) == "GALAXY" && Val.ToString(GlobalDec.gEmployeeProperty.department_name) == "MAKABLE" && Val.ToString(lueToBranch.Text) == "AMBIKA")
            {
                lblMainLotID.Visible = true;
                txtMainLotID.Visible = true;
            }
            else if (Val.ToString(lueToDepartment.Text) == "GALAXY" && Val.ToString(GlobalDec.gEmployeeProperty.department_name) == "MAKABLE" && Val.ToString(lueToBranch.Text) == "JOBWORK")
            {
                lblMainLotID.Visible = true;
                txtMainLotID.Visible = true;
            }
            else if (Val.ToString(lueToDepartment.Text) == "GALAXY DW" && Val.ToString(GlobalDec.gEmployeeProperty.department_name) == "MAKABLE" && Val.ToString(lueToBranch.Text) == "AMBIKA")
            {
                lblMainLotID.Visible = true;
                txtMainLotID.Visible = true;
            }
            else
            {
                lblMainLotID.Visible = false;
                txtMainLotID.Visible = false;
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
                            DataRow Drow = grvJangedIssueLotting.GetDataRow(e.RowHandle);
                            btnAdd.Text = "&Update";
                            lueCutNo.Text = Val.ToString(Drow["cut_no"]);
                            lueCutNo.EditValue = Val.ToInt(Drow["rough_cut_id"]);
                            txtLotID.Text = Val.ToString(Drow["lot_id"]);
                            lueManager.EditValue = Val.ToInt(Drow["to_manager_id"]);
                            lueProcess.EditValue = Val.ToInt(Drow["process_id"]);
                            lueSubProcess.EditValue = Val.ToInt(Drow["sub_process_id"]);
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
                        dtpIssueDate.Text = Val.DBDate(Val.ToString(Drow["janged_date"]));
                        lueToCompany.EditValue = Val.ToInt(Drow["company_id"]);
                        lueToBranch.EditValue = Val.ToInt(Drow["branch_id"]);
                        lueToLocation.EditValue = Val.ToInt(Drow["location_id"]);
                        lueToDepartment.EditValue = Val.ToInt(Drow["department_id"]);
                        lueParty.EditValue = Val.ToInt(Drow["party_id"]);
                        lueKapan.EditValue = Val.ToInt64(Drow["kapan_id"]);
                        lueManager.EditValue = Val.ToInt64(Drow["manager_id"]);
                        lueProcess.EditValue = Val.ToInt(Drow["process_id"]);
                        lueSubProcess.EditValue = Val.ToInt(Drow["sub_process_id"]);
                        txtJangedNo.Text = Val.ToString(Drow["janged_no"]);
                        lueCutNo.EditValue = Val.ToInt64(Drow["rough_cut_id"]);
                        lblMode.Text = "EDIT";
                        lblMode.Tag = Val.ToString(Drow["janged_no"]);
                        m_dtbIssueProcess = objMFGJangedIssue.GetDataJanged(Val.ToInt64(txtJangedNo.Text));
                        if (m_dtbIssueProcess.Rows.Count > 0)
                        {
                            btnSave.Tag = Val.ToInt32(m_dtbIssueProcess.Rows[0]["lot_srno"]);
                        }
                        grdJangedIssueLotting.DataSource = m_dtbIssueProcess;

                        ttlbJangedIssue.SelectedTabPage = tblJangeddetail;
                        lueKapan.Focus();
                        m_blncheckupdate = false;
                        btnSave.Enabled = true;
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
                    DataRow drwNew = m_dtbIssueProcess.NewRow();

                    drwNew["janged_id"] = Val.ToInt(0);
                    drwNew["janged_date"] = Val.DBDate(dtpIssueDate.Text);

                    drwNew["cut_no"] = Val.ToString(lueCutNo.Text);
                    drwNew["rough_cut_id"] = Val.ToString(lueCutNo.EditValue);
                    drwNew["lot_id"] = Val.ToInt64(txtLotID.Text);

                    drwNew["to_manager_id"] = Val.ToInt(lueManager.EditValue);
                    drwNew["to_manager"] = Val.ToString(lueManager.Text);

                    drwNew["manager_id"] = Val.ToInt(m_fromManagerId);

                    drwNew["process_id"] = Val.ToInt(lueProcess.EditValue);
                    drwNew["process"] = Val.ToString(lueProcess.Text);

                    drwNew["sub_process_id"] = Val.ToInt(lueSubProcess.EditValue);
                    drwNew["subprocess"] = Val.ToString(lueSubProcess.Text);

                    drwNew["party_id"] = Val.ToInt(lueParty.EditValue);
                    drwNew["party_name"] = Val.ToString(lueParty.Text);

                    drwNew["prd_id"] = m_prd_id;
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
                                if (m_dtbIssueProcess.Rows[m_update_srno - 1]["cut_no"].ToString() == m_cut_no.ToString())
                                {
                                    m_dtbIssueProcess.Rows[m_update_srno - 1]["lot_id"] = Val.ToString(txtLotID.Text);
                                    m_dtbIssueProcess.Rows[m_update_srno - 1]["manager_id"] = Val.ToInt(m_fromManagerId);
                                    m_dtbIssueProcess.Rows[m_update_srno - 1]["to_manager_id"] = Val.ToInt(lueManager.EditValue);
                                    m_dtbIssueProcess.Rows[m_update_srno - 1]["to_manager"] = Val.ToString(lueManager.Text);
                                    m_dtbIssueProcess.Rows[m_update_srno - 1]["process_id"] = Val.ToInt(lueProcess.EditValue);
                                    m_dtbIssueProcess.Rows[m_update_srno - 1]["process"] = Val.ToString(lueProcess.Text);
                                    m_dtbIssueProcess.Rows[m_update_srno - 1]["sub_process_id"] = Val.ToInt(lueSubProcess.EditValue);
                                    m_dtbIssueProcess.Rows[m_update_srno - 1]["subprocess"] = Val.ToString(lueSubProcess.Text);
                                    m_dtbIssueProcess.Rows[m_update_srno - 1]["prd_id"] = Val.ToInt(m_prd_id);
                                    m_dtbIssueProcess.Rows[m_update_srno - 1]["kapan_id"] = Val.ToInt(m_kapan_id);
                                    m_flag = Val.ToInt(0);

                                    break;
                                }
                            }
                        }
                    }
                    btnAdd.Text = "&Add";
                }
                grvJangedIssueLotting.MoveLast();

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
                    if (Val.ToInt(lueToDepartment.EditValue) == GlobalDec.gEmployeeProperty.department_id)
                    {
                        lstError.Add(new ListError(5, "Lot Not Transfer in a Same Department"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueToDepartment.Focus();
                        }
                    }
                    if (GlobalDec.gEmployeeProperty.department_name == "MAKABLE" && lueToDepartment.Text == "POLISH")
                    {
                        DataTable dtb = new DataTable();
                        MFGJangedIssue objMFGJangedIssue = new MFGJangedIssue();

                        dtb = objMFGJangedIssue.GetDataFactoryLock(Val.ToInt64(lueParty.EditValue));

                        if (dtb.Rows.Count > 0)
                        {
                            int total_pcs = Val.ToInt(clmPcs.SummaryItem.SummaryValue);

                            int issue_pcs = Val.ToInt(dtb.Rows[0]["issue_pcs"]);
                            int lock_pcs = Val.ToInt(dtb.Rows[0]["lock_pcs"]);
                            int diff_pcs = total_pcs + issue_pcs - lock_pcs;

                            if (total_pcs + issue_pcs > lock_pcs)
                            {
                                lstError.Add(new ListError(5, "Issue pcs More then total lock Pcs Diff is : " + diff_pcs + " Pcs "));
                                if (!blnFocus)
                                {
                                    blnFocus = true;
                                    dtpIssueDate.Focus();
                                }
                            }
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
                    //if (lueManager.Text == "")
                    //{
                    //    lstError.Add(new ListError(13, "To Manger"));
                    //    if (!blnFocus)
                    //    {
                    //        blnFocus = true;
                    //        lueManager.Focus();
                    //    }
                    //}
                    if (m_dtbIssueProcess.Rows.Count > 0)
                    {

                        DataRow[] drParty = m_dtbIssueProcess.Select("party_name <> '" + Val.ToString(lueParty.Text) + "'");
                        if (drParty.Count() > 0)
                        {
                            lstError.Add(new ListError(5, "Different party selected please check"));
                            if (!blnFocus)
                            {
                                blnFocus = true;
                                lueParty.Focus();
                            }
                        }
                        //DataRow[] drManager = m_dtbIssueProcess.Select("to_manager <> '" + Val.ToString(lueManager.Text) + "'");
                        //if (drManager.Count() > 0)
                        //{
                        //    lstError.Add(new ListError(5, "Different Manager selected please check"));
                        //    if (!blnFocus)
                        //    {
                        //        blnFocus = true;
                        //        lueManager.Focus();
                        //    }
                        //}
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
                lueProcess.EditValue = System.DBNull.Value;
                lueSubProcess.EditValue = System.DBNull.Value;
                lueParty.EditValue = System.DBNull.Value;

                lueToCompany.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.company_id);
                lueToBranch.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.branch_id);
                lueToLocation.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.location_id);
                lueToDepartment.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.department_id);
                txtLotID.Text = string.Empty;
                txtMainLotID.Text = string.Empty;
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
                btnDelete.Enabled = true;
                m_blncheckupdate = true;
                lblMainLotID.Visible = false;
                txtMainLotID.Visible = false;
                lueToBranch.Focus();
                lblMode.Text = "NEW";
                lblMode.Tag = 0;
                btnSave.Tag = 0;
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
                m_dtbIssueProcess.Columns.Add("fac_main_lot_id", typeof(int)).DefaultValue = 0;
                grdJangedIssueLotting.DataSource = m_dtbIssueProcess;
                grdJangedIssueLotting.Refresh();
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
                            grvJangedIssueLotting.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            grvJangedIssueLotting.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            grvJangedIssueLotting.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            grvJangedIssueLotting.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            grvJangedIssueLotting.ExportToText(Filepath);
                            break;
                        case "html":
                            grvJangedIssueLotting.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            grvJangedIssueLotting.ExportToCsv(Filepath);
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

        private void Export_IssueList(string format, string dlgHeader, string dlgFilter)
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
                            grdJangedList.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            grdJangedList.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            grdJangedList.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            grdJangedList.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            grdJangedList.ExportToText(Filepath);
                            break;
                        case "html":
                            grdJangedList.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            grdJangedList.ExportToCsv(Filepath);
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
        private bool Save_Validate_MainLotID()
        {
            bool blnReturn = true;

            try
            {
                m_blnadd = true;
                m_blnsave = false;
                if (!Validate_MainLotID_PopUp())
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
        private bool Validate_MainLotID_PopUp()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();

            try
            {
                if (lueProcess.Text == "")
                {
                    lstError.Add(new ListError(13, "Process"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueProcess.Focus();
                    }
                }
                if (lueParty.Text == "")
                {
                    lstError.Add(new ListError(13, "Party"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueParty.Focus();
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
            catch (Exception ex)
            {
                lstError.Add(new ListError(ex));
            }
            return (!(BLL.General.ShowErrors(lstError)));
        }
        private bool Validate_PopUp()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();

            try
            {
                if (lueCutNo.Text == "" && chkLot.Checked == false)
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
                if (lueParty.Text == "")
                {
                    lstError.Add(new ListError(13, "Party"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueParty.Focus();
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
                    string StrLotList = "";
                    MFGProcessIssue objMFGProcessIssue = new MFGProcessIssue();
                    MFGProcessIssueProperty objMFGProcessIssueProperty = new MFGProcessIssueProperty();
                    objMFGProcessIssueProperty.rough_cut_id = Val.ToInt(lueCutNo.EditValue);
                    objMFGProcessIssueProperty.kapan_id = Val.ToInt(lueKapan.EditValue);

                    DtPending = objMFGProcessIssue.GetPendingStock(objMFGProcessIssueProperty);
                    if (GlobalDec.gEmployeeProperty.role_name == "SURAT MAKABLE" || GlobalDec.gEmployeeProperty.role_name == "SURAT SARIN" || GlobalDec.gEmployeeProperty.role_name == "SURAT KATARGAM")
                    {
                        if (DtPending.Rows.Count > 0)
                        {
                            for (int i = 0; i < DtPending.Rows.Count; i++)
                            {
                                if (Val.ToInt(DtPending.Rows[i]["lot_id"]) != 0)
                                {
                                    DataTable DtConfirm = Global.CheckConfirmLot(Val.ToInt(DtPending.Rows[i]["lot_id"]));
                                    if (DtConfirm.Rows.Count != 0)
                                    {
                                        if (StrLotList.Length > 0)
                                        {
                                            StrLotList = StrLotList + "," + Val.ToInt(DtPending.Rows[i]["lot_id"]);
                                        }
                                        else
                                        {
                                            StrLotList = Val.ToInt(DtPending.Rows[i]["lot_id"]).ToString();
                                        }
                                    }
                                }
                            }
                            if (StrLotList != "")
                            {
                                DtPending = DtPending.Select("lot_id in(" + StrLotList + ")").CopyToDataTable();
                            }
                            else
                            {
                                DtPending = new DataTable();
                            }
                        }
                    }
                    FrmMFGStockConfirm FrmStockConfirm = new FrmMFGStockConfirm();
                    FrmStockConfirm.FrmMFGJangedIssueLotting = this;
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
                if (lueToDepartment.Text == "POLISH" || lueToDepartment.Text == "SOWABALE POLISH")
                {
                    if (m_dtbIssueProcess.Rows.Count > 9)
                    {
                        Global.Message("Maximum 9 Lot ID Insert In Grid...");
                        return;
                    }
                }
                if (lueToDepartment.Text == "4P")
                {
                    foreach (DataRow drw in m_dtbIssueProcess.Rows)
                    {
                        if (Global.CheckEstimationDoneOrNot(Val.ToInt64(drw["lot_id"])).ToString().Trim().Equals(string.Empty))
                        {
                            Global.Message("Estimation Entry Not Done in this Lot ID =" + Val.ToInt64(drw["lot_id"]));
                            return;
                        }
                    }
                }

                DataTable DTab = new DataTable();
                foreach (DataRow drw in m_dtbIssueProcess.Rows)
                {
                    MFGJangedIssue objMFGJangedIssue = new MFGJangedIssue();
                    MFGJangedIssue_Property objMFGJangedIssueProperty = new MFGJangedIssue_Property();

                    objMFGJangedIssueProperty.lot_id = Val.ToInt(drw["lot_id"]);
                    objMFGJangedIssueProperty.to_department_id = Val.ToInt(lueToDepartment.EditValue);

                    DTab = objMFGJangedIssue.GetJangedIssue_Validate(objMFGJangedIssueProperty);

                    if (DTab.Rows.Count > 0)
                    {
                        Global.Message("This Lot ID : " + Val.ToInt(drw["lot_id"]) + " Already Issued");
                        return;
                    }
                }
                grdJangedIssueLotting.DataSource = m_dtbIssueProcess;
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                MFGJangedIssue objMFGJangedIssue = new MFGJangedIssue();
                MFGJangedIssue_Property objMFGJangedIssueProperty = new MFGJangedIssue_Property();
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

                    count = objMFGJangedIssue.CheckJanged(Val.ToInt32(lblMode.Tag));
                    if (count == 0)
                    {
                        DialogResult result = MessageBox.Show("Do you want to Delete Janged Issue data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                        if (result != DialogResult.Yes)
                        {
                            btnDelete.Enabled = true;
                            return;
                        }

                        objMFGJangedIssueProperty.janged_no = Val.ToInt32(lblMode.Tag);

                        int IntRes = objMFGJangedIssue.JangedIssueDelete(objMFGJangedIssueProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                        Conn.Inter1.Commit();

                        if (IntRes == -1)
                        {
                            Global.Confirm("Error In Delete Janged Issue");
                            IntRes = -1;
                            Conn.Inter1.Rollback();
                            Conn = null;
                            return;
                            //txtPartyInvoiceNo.Focus();
                        }
                        else
                        {
                            Global.Confirm("Janged Issue Data Delete Successfully");
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

        private void txtLotID_KeyPress(object sender, KeyPressEventArgs e)
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
                if (Save_Validate_MainLotID())
                {
                    m_dtbIssueProcess.AcceptChanges();
                    if (txtMainLotID.Text.Length == 0)
                    {
                        return;
                    }

                    if ((Val.ToString(lueToDepartment.Text) == "GALAXY" && Val.ToString(GlobalDec.gEmployeeProperty.department_name) == "MAKABLE" && Val.ToString(lueToBranch.Text) == "JOBWORK") || (Val.ToString(lueToDepartment.Text) == "GALAXY DW" && Val.ToString(GlobalDec.gEmployeeProperty.department_name) == "MAKABLE" && Val.ToString(lueToBranch.Text) == "AMBIKA"))
                    {
                        if (m_dtbIssueProcess != null)
                        {
                            if (m_dtbIssueProcess.Rows.Count > 0)
                            {
                                DataRow[] dr = m_dtbIssueProcess.Select("main_lot_id = " + Val.ToInt64(txtMainLotID.Text));

                                if (dr.Length > 0)
                                {
                                    Global.Message(Val.ToInt64(txtMainLotID.Text) + " Main Lot ID already added to the Issue list!");
                                    txtMainLotID.Text = "";
                                    txtMainLotID.Focus();
                                    return;
                                }

                                //for (int i = 0; i < m_dtbIssueProcess.Rows.Count; i++)
                                //{
                                //    if (m_dtbIssueProcess.Rows[i]["main_lot_id"].ToString() == txtMainLotID.Text)
                                //    {
                                //        Global.Message("Main Lot ID already added to the Issue list!");
                                //        txtMainLotID.Text = "";
                                //        txtMainLotID.Focus();
                                //        return;
                                //    }
                                //}
                            }
                        }
                        if (m_dtbIssueProcess.Rows.Count > 0)
                        {
                            DataTable DTabTemp = new DataTable();

                            DTabTemp = Global.GetRoughJangedMainStockWise(Val.ToInt(lueKapan.EditValue), Val.ToInt64(txtMainLotID.Text), Val.ToString("JANGED_ISSUE"));

                            if (DTabTemp.Rows.Count > 0)
                            {
                                txtMainLotID.Text = "";
                                txtMainLotID.Focus();
                            }
                            m_dtbIssueProcess.Merge(DTabTemp);
                        }
                        else
                        {

                            m_dtbIssueProcess = Global.GetRoughJangedMainStockWise(Val.ToInt(lueKapan.EditValue), Val.ToInt64(txtMainLotID.Text), Val.ToString("JANGED_ISSUE"));

                            if (m_dtbIssueProcess.Rows.Count > 0)
                            {
                                txtMainLotID.Text = "";
                                txtMainLotID.Focus();
                            }
                        }
                    }
                    else
                    {
                        m_dtbIssueProcess = Global.GetRoughJangedMainStockWise(Val.ToInt(lueKapan.EditValue), Val.ToInt64(txtMainLotID.Text), Val.ToString("JANGED_ISSUE"));

                        if (m_dtbIssueProcess.Rows.Count > 0)
                        {
                        }
                        else
                        {
                            Global.Message("Lot ID Not Issue in Janged");
                            txtLotID.Text = "";
                            txtLotID.Focus();
                            return;
                        }
                    }
                    grdJangedIssueLotting.DataSource = m_dtbIssueProcess;
                    grdJangedIssueLotting.RefreshDataSource();
                    dgvJangedList.BestFitColumns();
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
    }
}

