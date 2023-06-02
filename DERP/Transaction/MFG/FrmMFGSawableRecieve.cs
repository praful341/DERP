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
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Transaction.MFG
{
    public partial class FrmMFGSawableRecieve : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;
        Control _NextEnteredControl;
        private List<Control> _tabControls;
        MFGSawableRecieve objSawableRecieve;
        MFGProcessReceive objProcessReceive;

        DataTable DtControlSettings;
        DataTable m_dtbDetail;
        DataTable m_dtbParam;
        DataTable m_dtbSubProcess;
        DataTable m_dtbSawableProcess;
        DataTable m_dtOutstanding;
        DataTable m_dtbKapan;
        //DataTable DTab_KapanWiseData = new DataTable();

        int m_Srno;
        int m_update_srno;
        int m_flag;
        int m_numForm_id;
        int m_kapan_id;
        string m_cut_no;
        int m_manager_id;
        int m_emp_id;
        string m_process = "";
        decimal m_OsCarat;
        int m_issue_id;
        decimal m_numcarat;
        decimal m_old_carat;
        decimal m_numSummRate;
        decimal m_balcarat;
        decimal m_old_rrcarat;

        bool m_blnadd;
        bool m_blnsave;
        Int64 IntRes;
        Int64 Receive_IntRes;
        Int64 Issue_IntRes;
        Int64 MixSplit_IntRes;
        #endregion

        #region Constructor
        public FrmMFGSawableRecieve()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            _NextEnteredControl = new Control();
            _tabControls = new List<Control>();
            objSawableRecieve = new MFGSawableRecieve();
            objProcessReceive = new MFGProcessReceive();

            DtControlSettings = new DataTable();
            m_dtbDetail = new DataTable();
            m_dtbParam = new DataTable();
            m_dtbSubProcess = new DataTable();
            m_dtbSawableProcess = new DataTable();
            m_dtOutstanding = new DataTable();
            m_dtbKapan = new DataTable();

            m_Srno = 1;
            m_update_srno = 1;
            m_flag = 0;
            m_numForm_id = 0;
            m_kapan_id = 0;
            m_cut_no = "";
            m_numcarat = 0;
            m_old_carat = 0;
            m_numSummRate = 0;
            m_balcarat = 0;
            m_old_rrcarat = 0;
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
        private void lueCutNo_Validated(object sender, EventArgs e)
        {
            try
            {
                if (lueCutNo.EditValue != System.DBNull.Value)
                {
                    if (m_dtbParam.Rows.Count > 0)
                    {
                        DataRow[] dr = m_dtbParam.Select("rough_cut_no ='" + Val.ToString(lueCutNo.Text) + "'");
                        txtLotID.Text = Val.ToString(dr[0]["lot_id"]);
                        MFGProcessReceive objProcessRecieve = new MFGProcessReceive();
                        if (txtLotID.Text != string.Empty || Val.ToInt64(txtLotID.Text) != 0)
                        {
                            GetOsCarat(Val.ToInt64(txtLotID.Text));
                            dtIssOS = objProcessRecieve.Carat_OutStanding_GetData(Val.ToInt64(txtLotID.Text), Val.ToInt(0), Val.ToInt(0), 1, "R");
                            if (dtIssOS.Rows.Count > 0)
                            {
                                DataRow[] drProcess = dtIssOS.Select("(process_name ='" + Val.ToString("SOYEBLE") + "' OR process_name ='" + Val.ToString("REJ TO MFG") + "')");
                                if (drProcess.Length > 0)
                                {
                                    lblOsPcs.Text = Val.ToString(Val.ToDecimal(drProcess[0]["pcs"]));
                                    lblOsCarat.Text = Val.ToString(Val.ToDecimal(drProcess[0]["carat"]));
                                    m_process = Val.ToString(drProcess[0]["process_name"]);
                                }
                                else
                                {
                                    Global.Message("Lot not issue in Soyeble Process");
                                    return;
                                }
                            }
                            else
                            {
                                Global.Message("Lot Not Issue");
                                return;
                            }
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
        private void txtLotID_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (lueCutNo.EditValue != System.DBNull.Value)
                {
                    if (m_dtbParam.Rows.Count > 0)
                    {
                        DataRow[] dr = m_dtbParam.Select("rough_cut_no ='" + Val.ToString(lueCutNo.Text) + "'");
                        txtLotID.Text = Val.ToString(dr[0]["lot_id"]);
                        MFGProcessReceive objProcessRecieve = new MFGProcessReceive();
                        if (txtLotID.Text != string.Empty || Val.ToInt64(txtLotID.Text) != 0)
                        {
                            GetOsCarat(Val.ToInt64(txtLotID.Text));
                            dtIssOS = objProcessRecieve.Carat_OutStanding_GetData(Val.ToInt64(txtLotID.Text), Val.ToInt(0), Val.ToInt(0), 1, "R");
                            if (dtIssOS.Rows.Count > 0)
                            {
                                DataRow[] drProcess = dtIssOS.Select("(process_name ='" + Val.ToString("SOYEBLE") + "' OR process_name ='" + Val.ToString("REJ TO MFG") + "')");
                                if (drProcess.Length > 0)
                                {
                                    lblOsPcs.Text = Val.ToString(Val.ToDecimal(drProcess[0]["pcs"]));
                                    lblOsCarat.Text = Val.ToString(Val.ToDecimal(drProcess[0]["carat"]));
                                    m_process = Val.ToString(drProcess[0]["process_name"]);
                                }
                                else
                                {
                                    Global.Message("Lot not issue in Soyeble Process");
                                    return;
                                }
                            }
                            else
                            {
                                Global.Message("Lot Not Issue");
                                return;
                            }
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
        private void RepDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (Global.Confirm("Are you sure delete selected row?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                dgvSawableRecieve.DeleteRow(dgvSawableRecieve.GetRowHandle(dgvSawableRecieve.FocusedRowHandle));
                m_dtbSawableProcess.AcceptChanges();
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (AddInGrid())
                {
                    luePurity.EditValue = System.DBNull.Value;
                    txtPcs.Text = string.Empty;
                    txtCarat.Text = string.Empty;
                    txtRate.Text = string.Empty;
                    txtRRPcs.Text = string.Empty;
                    txtRRCarat.Text = string.Empty;
                    txtAmount.Text = string.Empty;
                    txtWeightPlus.Text = string.Empty;
                    txtWeightLoss.Text = string.Empty;
                    luePurity.Focus();
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

                // Add By Praful On 04082020

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

                // End By Praful On 04082020

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
                backgroundWorker_SoyebleReceive.RunWorkerAsync();

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
            Global.Export("xlsx", dgvSawableRecieve);
        }
        private void luePurityGroup_EditValueChanged(object sender, EventArgs e)
        {
            if (luePurityGroup.EditValue != System.DBNull.Value)
            {
                Global.LOOKUPPurityGroupWisePurity(luePurity, Val.ToInt(luePurityGroup.EditValue));
            }
        }
        DataTable dtIssOS = new DataTable();

        private void txtLotID_Validated(object sender, EventArgs e)
        {
            try
            {
                if (txtLotID.Text != string.Empty || Val.ToInt64(txtLotID.Text) != 0)
                {
                    MFGProcessReceive objProcessRecieve = new MFGProcessReceive();
                    DataTable DTab_Process = objProcessRecieve.Carat_OutStanding_GetData(Val.ToInt64(txtLotID.Text), Val.ToInt32(0), Val.ToInt32(0), 0, "R");
                    if (DTab_Process.Rows.Count > 0)
                    {
                        DataRow[] drProcess = DTab_Process.Select("(process_name ='" + Val.ToString("SOYEBLE") + "' OR process_name ='" + Val.ToString("REJ TO MFG") + "')");
                        if (drProcess.Length > 0)
                        {
                            lueProcess.EditValue = null;
                            lueSubProcess.EditValue = null;
                            lueProcess.EditValue = Val.ToInt(drProcess[0]["process_id"]);
                            lueSubProcess.EditValue = Val.ToInt(drProcess[0]["sub_process_id"]);
                            lblOsPcs.Text = Val.ToString(drProcess[0]["pcs"]);
                            lblOsCarat.Text = Val.ToString(drProcess[0]["carat"]);
                            m_balcarat = Val.ToDecimal(drProcess[0]["carat"]);
                            lueProcess.Enabled = false;
                            lueSubProcess.Enabled = false;
                            m_process = Val.ToString(drProcess[0]["process_name"]);
                        }
                        else
                        {
                            Global.Message("Lot not issue in Soyeble Process");
                            lueProcess.Enabled = true;
                            lueSubProcess.Enabled = true;
                            lueProcess.EditValue = null;
                            lueSubProcess.EditValue = null;
                            lblOsPcs.Text = Val.ToString("0");
                            lblOsCarat.Text = Val.ToString("0.00");
                            return;
                        }
                    }
                }
                else
                {
                    Global.Message("Lot no should not be blank");
                    lueProcess.Enabled = true;
                    lueSubProcess.Enabled = true;
                    lueProcess.EditValue = null;
                    lueSubProcess.EditValue = null;
                    lblOsPcs.Text = Val.ToString("0");
                    lblOsCarat.Text = Val.ToString("0.00");
                    return;
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                lueProcess.Enabled = true;
                lueSubProcess.Enabled = true;
                lueProcess.EditValue = null;
                lueSubProcess.EditValue = null;
                lblOsPcs.Text = Val.ToString("0");
                lblOsCarat.Text = Val.ToString("0.00");
                return;
            }
        }
        private void txtCarat_EditValueChanged(object sender, EventArgs e)
        {
            txtAmount.Text = Val.ToString(Math.Round(((Val.ToDecimal(txtCarat.Text) + Val.ToDecimal(txtWeightPlus.Text)) * Val.ToDecimal(txtRate.Text)), 2));
        }
        private void txtRate_EditValueChanged(object sender, EventArgs e)
        {
            txtAmount.Text = Val.ToString(Math.Round(((Val.ToDecimal(txtCarat.Text) + Val.ToDecimal(txtWeightPlus.Text)) * Val.ToDecimal(txtRate.Text)), 2));
        }
        private void txtWeightPlus_EditValueChanged(object sender, EventArgs e)
        {
            txtAmount.Text = Val.ToString(Math.Round(((Val.ToDecimal(txtCarat.Text) + Val.ToDecimal(txtWeightPlus.Text)) * Val.ToDecimal(txtRate.Text)), 2));
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
        private void luePurityGroup_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmMfgPurityGroupMaster objPurityGroup = new FrmMfgPurityGroupMaster();
                objPurityGroup.ShowDialog();
                Global.LOOKUPPurityGroup(luePurityGroup);
            }
        }
        private void luePurity_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmClarityMaster objPurity = new FrmClarityMaster();
                objPurity.ShowDialog();
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

        private void lueProcess_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (lueProcess.EditValue != null)
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
                        lueSubProcess.EditValue = null;
                    }
                }
                if (lueProcess.EditValue != null && Val.ToString(lueProcess.Text) != "" && lueSubProcess.EditValue != null && Val.ToString(lueSubProcess.Text) != "" && Val.ToInt64(txtLotID.Text) != 0)
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
                            txtBalanceCarat.Text = Val.ToString(m_OsCarat);
                            m_balcarat = Val.ToDecimal(dtIssOS.Rows[0]["carat"]);
                            txtCarat.Text = Val.ToDecimal(dtIssOS.Rows[0]["carat"]).ToString();
                        }
                        else
                        {
                            txtBalanceCarat.Text = "0";
                            m_balcarat = 0;
                            txtCarat.Text = "0";
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

        private void lueSubProcess_EditValueChanged(object sender, EventArgs e)
        {
            if (lueProcess.EditValue != null && Val.ToString(lueProcess.Text) != "" && lueSubProcess.EditValue != null && Val.ToString(lueSubProcess.Text) != "" && Val.ToInt64(txtLotID.Text) != 0)
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
                        txtBalanceCarat.Text = Val.ToString(m_dtOutstanding.Rows[0]["carat"]);
                        m_OsCarat = Val.ToDecimal(txtBalanceCarat.Text);
                        m_balcarat = Val.ToDecimal(m_dtOutstanding.Rows[0]["carat"]);
                        txtCarat.Text = Val.ToDecimal(m_dtOutstanding.Rows[0]["carat"]).ToString();
                    }
                    else
                    {
                        txtBalanceCarat.Text = "0";
                        txtCarat.Text = "0";
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

        private void FrmMFGSawableRecieve_Load(object sender, EventArgs e)
        {
            try
            {
                Global.LOOKUPPurityGroup(luePurityGroup);
                Global.LOOKUPPurity(luePurity);
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
                dtpReceiveDate.Focus();
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void backgroundWorker_SoyebleReceive_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                MFGSawableRecieve objMFGSawableReceive = new MFGSawableRecieve();
                MFGSawableReceiveProperty objMFGSawableReceiveProperty = new MFGSawableReceiveProperty();
                Conn = new BeginTranConnection(true, false);

                IntRes = 0;
                Receive_IntRes = 0;
                Issue_IntRes = 0;
                MixSplit_IntRes = 0;

                try
                {
                    //DataTable dtIssueDet = objMFGSawableReceive.ProcessIssue_GetData(Val.ToInt64(txtLotID.Text), "SOYEBLE");
                    //objMFGSawableReceiveProperty.manager_id = Val.ToInt(dtIssueDet.Rows[0]["manager_id"]);
                    //objMFGSawableReceiveProperty.employee_id = Val.ToInt(dtIssueDet.Rows[0]["employee_id"]);
                    //objMFGSawableReceiveProperty.process_id = Val.ToInt(dtIssueDet.Rows[0]["process_id"]);
                    //objMFGSawableReceiveProperty.sub_process_id = Val.ToInt(dtIssueDet.Rows[0]["sub_process_id"]);
                    //objMFGSawableReceiveProperty.issue_id = Val.ToInt(dtIssueDet.Rows[0]["issue_id"]);
                    //objMFGSawableReceiveProperty.kapan_id = Val.ToInt(lueKapan.EditValue);

                    //int count = m_dtbSawableProcess.Rows.Count;
                    decimal TotCarat = 0;
                    int New_Count = 1;
                    Int64 NewHistory_Union_Id = 0;
                    Int64 Lot_SrNo = 0;
                    DataTable DTab = new DataTable();
                    foreach (DataRow drw in m_dtbSawableProcess.Rows)
                    {
                        DataTable dtIssueDet = objMFGSawableReceive.ProcessIssue_GetData(Val.ToInt(drw["lot_id"]), "SOYEBLE", DLL.GlobalDec.EnumTran.Continue, Conn);
                        objMFGSawableReceiveProperty.manager_id = Val.ToInt(dtIssueDet.Rows[0]["manager_id"]);
                        objMFGSawableReceiveProperty.employee_id = Val.ToInt(dtIssueDet.Rows[0]["employee_id"]);
                        objMFGSawableReceiveProperty.process_id = Val.ToInt(lueProcess.EditValue);
                        objMFGSawableReceiveProperty.sub_process_id = Val.ToInt(lueSubProcess.EditValue);
                        objMFGSawableReceiveProperty.issue_id = Val.ToInt(dtIssueDet.Rows[0]["issue_id"]);
                        objMFGSawableReceiveProperty.kapan_id = Val.ToInt(drw["kapan_id"]);

                        DTab = m_dtbSawableProcess.Select("lot_id =" + Val.ToInt(drw["lot_id"])).CopyToDataTable();
                        int count = DTab.Rows.Count;

                        if (count == New_Count)
                        {
                            foreach (DataRow DRow in DTab.Rows)
                            {
                                TotCarat += Val.ToDecimal(DRow["carat"]);
                            }
                            objMFGSawableReceiveProperty.flag = 1;
                            objMFGSawableReceiveProperty.recieve_carat = Val.ToDecimal(drw["os_carat"]) - (TotCarat + Val.ToDecimal(dgvSawableRecieve.Columns["loss_carat"].SummaryText));
                            objMFGSawableReceiveProperty.total_loss_carat = Val.ToDecimal(dgvSawableRecieve.Columns["loss_carat"].SummaryText);
                            objMFGSawableReceiveProperty.total_plus_carat = Val.ToDecimal(dgvSawableRecieve.Columns["plus_carat"].SummaryText);
                            New_Count = 0;
                            TotCarat = 0;
                        }
                        else
                        {
                            objMFGSawableReceiveProperty.flag = 0;
                        }

                        objMFGSawableReceiveProperty.receive_date = Val.DBDate(dtpReceiveDate.Text);
                        objMFGSawableReceiveProperty.balance_pcs = Val.ToInt(drw["os_pcs"]);
                        objMFGSawableReceiveProperty.balance_carat = Val.ToDecimal(drw["os_carat"]);
                        objMFGSawableReceiveProperty.rough_lot_id = Val.ToInt(drw["lot_id"]);
                        objMFGSawableReceiveProperty.rough_cut_id = Val.ToInt(drw["rough_cut_id"]);
                        objMFGSawableReceiveProperty.pcs = Val.ToInt(drw["pcs"]);
                        objMFGSawableReceiveProperty.carat = Val.ToDecimal(drw["carat"]);
                        objMFGSawableReceiveProperty.loss_carat = Val.ToDecimal(drw["loss_carat"]);
                        objMFGSawableReceiveProperty.plus_carat = Val.ToDecimal(drw["plus_carat"]);
                        objMFGSawableReceiveProperty.rr_pcs = Val.ToInt(drw["rr_pcs"]);
                        objMFGSawableReceiveProperty.rr_carat = Val.ToDecimal(drw["rr_carat"]);
                        objMFGSawableReceiveProperty.rate = Val.ToDecimal(drw["rate"]);
                        objMFGSawableReceiveProperty.amount = Val.ToDecimal(drw["amount"]);
                        objMFGSawableReceiveProperty.purity_id = Val.ToInt64(drw["purity_id"]);
                        objMFGSawableReceiveProperty.union_id = IntRes;
                        objMFGSawableReceiveProperty.receive_union_id = Receive_IntRes;
                        objMFGSawableReceiveProperty.issue_union_id = Issue_IntRes;
                        objMFGSawableReceiveProperty.mix_union_id = MixSplit_IntRes;
                        objMFGSawableReceiveProperty.form_id = Val.ToInt(m_numForm_id);
                        objMFGSawableReceiveProperty.history_union_id = NewHistory_Union_Id;
                        objMFGSawableReceiveProperty.lot_srno = Lot_SrNo;

                        objMFGSawableReceiveProperty = objMFGSawableReceive.Save(objMFGSawableReceiveProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                        IntRes = objMFGSawableReceiveProperty.union_id;
                        Receive_IntRes = objMFGSawableReceiveProperty.receive_union_id;
                        Issue_IntRes = objMFGSawableReceiveProperty.issue_union_id;
                        MixSplit_IntRes = objMFGSawableReceiveProperty.mix_union_id;
                        NewHistory_Union_Id = Val.ToInt64(objMFGSawableReceiveProperty.history_union_id);
                        Lot_SrNo = Val.ToInt64(objMFGSawableReceiveProperty.lot_srno);
                        New_Count = New_Count + 1;

                        objMFGSawableReceiveProperty.is_outstanding = Val.ToBoolean(0);
                        objMFGSawableReceiveProperty.rough_lot_id = Val.ToInt(drw["lot_id"]);
                        objMFGSawableReceive.Update(objMFGSawableReceiveProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                    }
                    //if (m_balcarat == (Val.ToDecimal(dgvSawableRecieve.Columns["carat"].SummaryText) + Val.ToDecimal(dgvSawableRecieve.Columns["rr_carat"].SummaryText)))
                    //{
                    //    objMFGSawableReceiveProperty.is_outstanding = Val.ToBoolean(0);
                    //    objMFGSawableReceiveProperty.rough_lot_id = Val.ToInt(drw["lot_id"]);
                    //    objMFGSawableReceive.Update(objMFGSawableReceiveProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                    //}
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
        private void backgroundWorker_SoyebleReceive_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (IntRes > 0)
                {
                    Global.Confirm("Process Recieve Data Save Succesfully");
                    grdSawableRecieve.DataSource = null;
                    btnSave.Enabled = true;
                    ClearDetails();
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
        private void btnSearch_Click(object sender, EventArgs e)
        {
            FrmMFGSearchProcess FrmSearchProcess = new FrmMFGSearchProcess();
            FrmSearchProcess.FrmMFGSawableRecieve = this;
            //FrmSearchProcess.DTab = DtPending;
            FrmSearchProcess.ShowForm(this);
        }
        private void txtPcs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as DevExpress.XtraEditors.TextEdit).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }
        private void txtCarat_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txtWeightLoss_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txtWeightPlus_KeyPress(object sender, KeyPressEventArgs e)
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
        private void dgvSawableRecieve_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvSawableRecieve.GetDataRow(e.RowHandle);
                        btnAdd.Text = "&Update";
                        lueCutNo.Text = Val.ToString(Drow["cut_no"]);
                        txtLotID.Text = Val.ToString(Drow["lot_id"]);
                        luePurityGroup.Text = Val.ToString(Drow["purity_group"]);
                        luePurity.Text = Val.ToString(Drow["purity_name"]);
                        txtPcs.Text = Val.ToString(Drow["pcs"]);
                        txtCarat.Text = Val.ToString(Drow["carat"]);
                        txtRRPcs.Text = Val.ToString(Drow["rr_pcs"]);
                        txtRRCarat.Text = Val.ToString(Drow["rr_carat"]);
                        txtRate.Text = Val.ToString(Drow["rate"]);
                        m_numcarat = Val.ToDecimal(Drow["carat"]);
                        m_cut_no = Val.ToString(Drow["cut_no"]);
                        m_update_srno = Val.ToInt(Drow["sr_no"]);
                        m_old_carat = Val.ToDecimal(Drow["carat"]);
                        m_old_rrcarat = Val.ToDecimal(Drow["rr_carat"]);
                        m_flag = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void dgvSawableRecieve_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
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
                    DataRow drwNew = m_dtbSawableProcess.NewRow();
                    int numPcs = Val.ToInt(txtPcs.Text);
                    decimal numCarat = Val.ToDecimal(txtCarat.Text);
                    int numRRPcs = Val.ToInt(txtRRPcs.Text);
                    decimal numRRCarat = Val.ToDecimal(txtRRCarat.Text);
                    decimal numRate = Val.ToDecimal(txtRate.Text);
                    decimal numAmount = Val.ToDecimal(txtAmount.Text);
                    decimal numLossCarat = Val.ToDecimal(txtWeightLoss.Text);
                    decimal numPlusCarat = Val.ToDecimal(txtWeightPlus.Text);

                    drwNew["recieve_id"] = Val.ToInt(0);
                    drwNew["recieve_date"] = Val.DBDate(dtpReceiveDate.Text);
                    drwNew["cut_no"] = Val.ToString(lueCutNo.Text);
                    drwNew["lot_id"] = Val.ToInt64(txtLotID.Text);
                    drwNew["purity_group_id"] = Val.ToInt(luePurityGroup.EditValue);
                    drwNew["purity_group"] = Val.ToString(luePurityGroup.Text);
                    drwNew["purity_id"] = Val.ToInt(luePurity.EditValue);
                    drwNew["purity_name"] = Val.ToString(luePurity.Text);
                    drwNew["pcs"] = numPcs;
                    drwNew["carat"] = numCarat;
                    drwNew["loss_carat"] = numLossCarat;
                    drwNew["plus_carat"] = numPlusCarat;
                    drwNew["rr_pcs"] = numRRPcs;
                    drwNew["rr_carat"] = numRRCarat;
                    drwNew["rate"] = numRate;
                    drwNew["amount"] = numAmount;
                    drwNew["sr_no"] = m_Srno;

                    drwNew["rough_cut_id"] = Val.ToString(lueCutNo.EditValue);
                    drwNew["kapan_id"] = Val.ToString(lueKapan.EditValue);
                    drwNew["os_pcs"] = Val.ToDecimal(lblOsPcs.Text);
                    drwNew["os_carat"] = Val.ToDecimal(lblOsCarat.Text);

                    m_dtbSawableProcess.Rows.Add(drwNew);
                    m_Srno++;
                }
                else if (btnAdd.Text == "&Update")
                {

                    if (m_dtbSawableProcess.Select("sr_no ='" + Val.ToInt(m_update_srno) + "'").Length > 0)
                    {
                        for (int i = 0; i < m_dtbSawableProcess.Rows.Count; i++)
                        {
                            if (m_dtbSawableProcess.Select("cut_no ='" + m_cut_no + "' AND sr_no = '" + m_update_srno + "'").Length > 0)
                            {
                                if (m_dtbSawableProcess.Rows[dgvSawableRecieve.FocusedRowHandle]["cut_no"].ToString() == m_cut_no.ToString())
                                {
                                    m_dtbSawableProcess.Rows[dgvSawableRecieve.FocusedRowHandle]["lot_id"] = Val.ToString(txtLotID.Text);
                                    m_dtbSawableProcess.Rows[dgvSawableRecieve.FocusedRowHandle]["purity_group"] = Val.ToString(luePurityGroup.Text);
                                    m_dtbSawableProcess.Rows[dgvSawableRecieve.FocusedRowHandle]["purity_name"] = Val.ToString(luePurity.Text);
                                    m_dtbSawableProcess.Rows[dgvSawableRecieve.FocusedRowHandle]["purity_group_id"] = Val.ToInt(luePurityGroup.EditValue);
                                    m_dtbSawableProcess.Rows[dgvSawableRecieve.FocusedRowHandle]["purity_id"] = Val.ToInt(luePurity.EditValue);
                                    m_dtbSawableProcess.Rows[dgvSawableRecieve.FocusedRowHandle]["pcs"] = Val.ToInt(txtPcs.Text).ToString();
                                    m_dtbSawableProcess.Rows[dgvSawableRecieve.FocusedRowHandle]["carat"] = Val.ToDecimal(txtCarat.Text).ToString();
                                    m_dtbSawableProcess.Rows[dgvSawableRecieve.FocusedRowHandle]["plus_carat"] = Val.ToDecimal(txtWeightPlus.Text).ToString();
                                    m_dtbSawableProcess.Rows[dgvSawableRecieve.FocusedRowHandle]["loss_carat"] = Val.ToDecimal(txtWeightLoss.Text).ToString();
                                    m_dtbSawableProcess.Rows[dgvSawableRecieve.FocusedRowHandle]["rr_pcs"] = Val.ToInt(txtRRPcs.Text).ToString();
                                    m_dtbSawableProcess.Rows[dgvSawableRecieve.FocusedRowHandle]["rr_carat"] = Val.ToDecimal(txtRRCarat.Text).ToString();
                                    m_dtbSawableProcess.Rows[dgvSawableRecieve.FocusedRowHandle]["rate"] = Val.ToDecimal(txtRate.Text);
                                    m_dtbSawableProcess.Rows[dgvSawableRecieve.FocusedRowHandle]["amount"] = Val.ToDecimal(txtAmount.Text);
                                    m_dtbSawableProcess.Rows[dgvSawableRecieve.FocusedRowHandle]["rough_cut_id"] = Val.ToInt32(lueCutNo.EditValue);
                                    m_dtbSawableProcess.Rows[dgvSawableRecieve.FocusedRowHandle]["kapan_id"] = Val.ToInt32(lueKapan.EditValue);
                                    m_dtbSawableProcess.Rows[dgvSawableRecieve.FocusedRowHandle]["os_pcs"] = Val.ToDecimal(lblOsPcs.Text);
                                    m_dtbSawableProcess.Rows[dgvSawableRecieve.FocusedRowHandle]["os_carat"] = Val.ToDecimal(lblOsCarat.Text);

                                    m_flag = 0;
                                    break;
                                }
                            }
                        }
                    }
                    btnAdd.Text = "&Add";
                }
                dgvSawableRecieve.MoveLast();
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
                    if (m_dtbSawableProcess.Rows.Count == 0)
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
                }

                if (m_blnadd)
                {
                    if (Val.ToString(m_process) != "SOYEBLE" && Val.ToString(m_process) != "REJ TO MFG")
                    {
                        lstError.Add(new ListError(5, "Lot not issue in SOYEBLE Process OR REJ TO MFG Process"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtLotID.Focus();
                        }
                    }
                    if (Val.ToInt64(txtLotID.Text) == 0)
                    {
                        lstError.Add(new ListError(12, "Lot No."));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtLotID.Focus();
                        }
                    }
                    if (Val.ToString(lueCutNo.Text) == "")
                    {
                        lstError.Add(new ListError(13, "Cut No"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueCutNo.Focus();
                        }
                    }
                    if (Val.ToString(luePurity.Text) == "")
                    {
                        lstError.Add(new ListError(13, "Purity"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            luePurity.Focus();
                        }
                    }
                    if (Val.ToString(luePurityGroup.Text) == "")
                    {
                        lstError.Add(new ListError(13, "Purity Group"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            luePurityGroup.Focus();
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
                    if (Val.ToDecimal(txtCarat.Text) == 0)
                    {
                        lstError.Add(new ListError(12, "Carat"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtCarat.Focus();
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
                    if (m_flag == 1)
                    {
                        //if ((m_balcarat + Val.ToDecimal(dgvSawableRecieve.Columns["plus_carat"].SummaryText) - m_oldpluscarat) < (Val.ToDecimal(txtCarat.Text) + Val.ToDecimal(txtRRCarat.Text) + (Val.ToDecimal(dgvSawableRecieve.Columns["carat"].SummaryText) - Val.ToDecimal(m_old_carat)) + (Val.ToDecimal(dgvSawableRecieve.Columns["rr_carat"].SummaryText) - Val.ToDecimal(m_old_rrcarat)) + (Val.ToDecimal(dgvSawableRecieve.Columns["plus_carat"].SummaryText) - m_oldpluscarat) + (Val.ToDecimal(dgvSawableRecieve.Columns["loss_carat"].SummaryText) - m_oldlosscarat)))
                        //{
                        //    lstError.Add(new ListError(5, "Issue carat more than balance carat"));
                        //    if (!blnFocus)
                        //    {
                        //        blnFocus = true;
                        //        txtCarat.Focus();
                        //    }
                        //}
                    }
                    if (m_flag == 0)
                    {
                        //if ((m_balcarat + Val.ToDecimal(dgvSawableRecieve.Columns["plus_carat"].SummaryText)) < (Val.ToDecimal(txtCarat.Text) + Val.ToDecimal(txtRRCarat.Text) + Val.ToDecimal(dgvSawableRecieve.Columns["carat"].SummaryText) + Val.ToDecimal(dgvSawableRecieve.Columns["rr_carat"].SummaryText) + Val.ToDecimal(dgvSawableRecieve.Columns["plus_carat"].SummaryText) + Val.ToDecimal(dgvSawableRecieve.Columns["loss_carat"].SummaryText)))
                        //{
                        //    lstError.Add(new ListError(5, "Issue carat more than balance carat"));
                        //    if (!blnFocus)
                        //    {
                        //        blnFocus = true;
                        //        txtCarat.Focus();
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
                if (!GenerateSawableDetails())
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
                luePurityGroup.EditValue = System.DBNull.Value;
                luePurity.EditValue = System.DBNull.Value;
                lueProcess.EditValue = System.DBNull.Value;
                lueSubProcess.EditValue = System.DBNull.Value;
                txtBalanceCarat.Text = string.Empty;
                txtBalancePcs.Text = string.Empty;
                lblOsCarat.Text = string.Empty;
                lblOsPcs.Text = string.Empty;
                txtPcs.Text = string.Empty;
                txtCarat.Text = string.Empty;
                txtRRPcs.Text = string.Empty;
                txtRRCarat.Text = string.Empty;
                txtLotID.Text = string.Empty;
                txtRate.Text = "0";
                txtAmount.Text = "0";
                m_Srno = 1;
                m_update_srno = 0;
                btnAdd.Text = "&Add";
                txtIssProcess.Text = "";
                lueProcess.Enabled = true;
                lueSubProcess.Enabled = true;
                dtpReceiveDate.Focus();
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
                    txtBalancePcs.Text = Val.ToString(m_dtOutstanding.Rows[0]["balance_pcs"]);
                    txtPcs.Text = Val.ToString(m_dtOutstanding.Rows[0]["balance_pcs"]);
                    lueCutNo.EditValue = Val.ToInt(m_dtOutstanding.Rows[0]["rough_cut_id"]);
                    txtRate.Text = Val.ToDecimal(m_dtOutstanding.Rows[0]["rate"]).ToString();
                    m_kapan_id = Val.ToInt(m_dtOutstanding.Rows[0]["kapan_id"]);
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private bool GenerateSawableDetails()
        {
            bool blnReturn = true;
            try
            {
                if (m_dtbSawableProcess.Rows.Count > 0)
                    m_dtbSawableProcess.Rows.Clear();
                m_dtbSawableProcess = new DataTable();

                m_dtbSawableProcess.Columns.Add("recieve_id", typeof(int));
                m_dtbSawableProcess.Columns.Add("recieve_date", typeof(DateTime));
                m_dtbSawableProcess.Columns.Add("lot_id", typeof(int));
                m_dtbSawableProcess.Columns.Add("cut_no", typeof(string));
                m_dtbSawableProcess.Columns.Add("purity_group", typeof(string));
                m_dtbSawableProcess.Columns.Add("purity_group_id", typeof(int));
                m_dtbSawableProcess.Columns.Add("purity_name", typeof(string));
                m_dtbSawableProcess.Columns.Add("purity_id", typeof(int));
                m_dtbSawableProcess.Columns.Add("pcs", typeof(int)).DefaultValue = 0;
                m_dtbSawableProcess.Columns.Add("carat", typeof(decimal)).DefaultValue = 0;
                m_dtbSawableProcess.Columns.Add("plus_carat", typeof(decimal)).DefaultValue = 0;
                m_dtbSawableProcess.Columns.Add("loss_carat", typeof(decimal)).DefaultValue = 0;
                m_dtbSawableProcess.Columns.Add("rr_pcs", typeof(int)).DefaultValue = 0;
                m_dtbSawableProcess.Columns.Add("rr_carat", typeof(decimal)).DefaultValue = 0;
                m_dtbSawableProcess.Columns.Add("sr_no", typeof(decimal)).DefaultValue = 1;
                m_dtbSawableProcess.Columns.Add("rate", typeof(decimal)).DefaultValue = 0;
                m_dtbSawableProcess.Columns.Add("amount", typeof(decimal)).DefaultValue = 0;

                m_dtbSawableProcess.Columns.Add("rough_cut_id", typeof(int));
                m_dtbSawableProcess.Columns.Add("kapan_id", typeof(int));
                m_dtbSawableProcess.Columns.Add("os_pcs", typeof(decimal));
                m_dtbSawableProcess.Columns.Add("os_carat", typeof(decimal));

                grdSawableRecieve.DataSource = m_dtbSawableProcess;
                grdSawableRecieve.Refresh();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
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
                            dgvSawableRecieve.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvSawableRecieve.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvSawableRecieve.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvSawableRecieve.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvSawableRecieve.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvSawableRecieve.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvSawableRecieve.ExportToCsv(Filepath);
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
            //Global.Export("xlsx", dgvRoughClarityMaster);
            Export("xlsx", "Export to Excel", "Excel files 97-2003 (Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*");
        }
        private void MNExportPDF_Click(object sender, EventArgs e)
        {
            // Global.Export("pdf", dgvRoughClarityMaster);
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
