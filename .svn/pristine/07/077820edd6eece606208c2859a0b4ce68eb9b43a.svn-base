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
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DREP.Master;
using DREP.Master.MFG;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Transaction.MFG
{
    public partial class FrmMFGProcessReceiveJanged : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        MFGProcessReceive objProcessReceive;
        MFGJangedReceive objMFGJangedReceive;
        MfgRoughSieve objRoughSieve;
        MfgQualityMaster objQuality;
        MfgRoughClarityMaster objRoughClarity;

        Control _NextEnteredControl;
        private List<Control> _tabControls;
        DataTable DtControlSettings = new DataTable();

        DataTable dtTemp;
        DataTable m_dtbParam;
        DataTable m_dtCut;
        DataTable m_dtbType;
        DataTable m_dtOutstanding;
        DataTable m_dtbKapan;
        DataTable m_dtbSubProcess;
        //DataTable DTab_KapanWiseData = new DataTable();

        int m_kapan_id;
        int m_JangedId;
        int Loss_Count;
        Int64 m_numForm_id;
        Int64 IntRes;
        Int64 Receive_IntRes;
        decimal m_OScarat;
        bool m_blnflag;
        decimal m_balcarat;
        int outside = 0;
        Int64 from_company_id = 0;
        Int64 from_branch_id = 0;
        Int64 from_location_id = 0;
        Int64 from_department_id = 0;
        Int64 JangedNo_IntRes;
        Int64 Dept_IntRes;
        Int64 Janged_IntRes;
        Int64 JangedSrNo_IntRes;
        int m_IsLot;
        #endregion

        #region Constructor
        public FrmMFGProcessReceiveJanged()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            _NextEnteredControl = new Control();
            _tabControls = new List<Control>();

            objProcessReceive = new MFGProcessReceive();
            objMFGJangedReceive = new MFGJangedReceive();
            objRoughSieve = new MfgRoughSieve();
            objQuality = new MfgQualityMaster();
            objRoughClarity = new MfgRoughClarityMaster();
            dtTemp = new DataTable();
            m_dtbParam = new DataTable();
            m_dtCut = new DataTable();
            m_dtbKapan = new DataTable();
            m_dtbSubProcess = new DataTable();

            Loss_Count = 0;
            m_kapan_id = 0;
            m_JangedId = 0;
            m_numForm_id = 0;
            m_blnflag = new bool();
            m_IsLot = 0;
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
                if ((Control)sender is CheckedComboBoxEdit)
                {
                    ((CheckedComboBoxEdit)(Control)sender).ShowPopup();
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
                if ((Control)sender is CheckedComboBoxEdit)
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
                            if (!(item2 is TextEdit))
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
        private void lueSubProcess_EditValueChanged(object sender, EventArgs e)
        {
            if (lueSubProcess.EditValue != null)
            {
                MFGProcessReceive objProcessRecieve = new MFGProcessReceive();
                DataTable DTab_Process = objProcessRecieve.Carat_OutStanding_Janged_GetData(Val.ToInt64(txtLotId.Text), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue), 0, "R", Val.ToInt64(txtJangedNo.Text));
                if (DTab_Process.Rows.Count > 0)
                {
                    lblOsPcs.Text = Val.ToString(DTab_Process.Rows[0]["pcs"]);
                    lblOsCarat.Text = Val.ToString(DTab_Process.Rows[0]["carat"]);
                }
                else
                {
                    lblOsPcs.Text = "";
                    lblOsCarat.Text = "";
                }

            }
        }
        private void FrmMFGProcessReceiveJanged_Load(object sender, EventArgs e)
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

                m_dtbSubProcess = (((DataTable)lueSubProcess.Properties.DataSource).Copy());

                m_dtbKapan = Global.GetKapanAll();

                lueKapan.Properties.DataSource = m_dtbKapan;
                lueKapan.Properties.ValueMember = "kapan_id";
                lueKapan.Properties.DisplayMember = "kapan_no";

                m_dtCut = Global.GetRoughCutAll();
                lueCutNo.Properties.DataSource = m_dtCut;
                lueCutNo.Properties.ValueMember = "rough_cut_id";
                lueCutNo.Properties.DisplayMember = "rough_cut_no";

                m_dtbType = new DataTable();
                m_dtbType.Columns.Add("type");
                m_dtbType.Rows.Add("BOTH");
                m_dtbType.Rows.Add("NORMAL");
                m_dtbType.Rows.Add("REJECTION");

                lueType.Properties.DataSource = m_dtbType;
                lueType.Properties.ValueMember = "type";
                lueType.Properties.DisplayMember = "type";
                lueType.EditValue = "BOTH";

                dtpReceiveDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpReceiveDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpReceiveDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpReceiveDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpReceiveDate.EditValue = DateTime.Now;

                DataTable dtbSieve = new DataTable();
                dtbSieve = objRoughSieve.GetData();
                lueSieve.Properties.DataSource = dtbSieve;
                lueSieve.Properties.ValueMember = "rough_sieve_id";
                lueSieve.Properties.DisplayMember = "sieve_name";

                DataTable dtbQuality = new DataTable();
                dtbQuality = objQuality.GetData();
                lueQuality.Properties.DataSource = dtbQuality;
                lueQuality.Properties.ValueMember = "quality_id";
                lueQuality.Properties.DisplayMember = "quality_name";

                m_dtbParam = Global.GetRoughCutAll();

                // Add By Praful On 29072021

                //DTab_KapanWiseData = Global.GetRoughStockWise(Val.ToInt(0), Val.ToInt32(0));

                // End By Praful On 29072021

                lueKapan.Focus();
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
                DataTable dtTemp = new DataTable();
                dtTemp = (DataTable)grdProcessReceiveJanged.DataSource;
                List<ListError> lstError = new List<ListError>();
                if (dtTemp == null)
                {
                    Global.Message("Atleast 1 record must be enter in grid");
                    btnSave.Enabled = true;
                    return;
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
                backgroundWorker_ProcRecJanged.RunWorkerAsync();

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
        private bool ClearDetails()
        {
            bool blnReturn = true;
            try
            {
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

                luePurity.EditValue = System.DBNull.Value;
                lueParty.EditValue = System.DBNull.Value;

                for (int i = 0; i < lueClarity.Properties.Items.Count; i++)
                    lueClarity.Properties.Items[i].CheckState = CheckState.Unchecked;

                for (int i = 0; i < lueSieve.Properties.Items.Count; i++)
                    lueSieve.Properties.Items[i].CheckState = CheckState.Unchecked;

                for (int i = 0; i < lueQuality.Properties.Items.Count; i++)
                    lueQuality.Properties.Items[i].CheckState = CheckState.Unchecked;

                txtBalancePcs.Text = "0";
                txtBalanceCarat.Text = "0";
                lblOsPcs.Text = "0";
                lblOsCarat.Text = "0.00";
                txtLotId.Text = "";
                txtJangedNo.Text = "0";
                grdProcessReceiveJanged.DataSource = null;
                dgvProcessReceiveJanged.Columns.Clear();
                m_kapan_id = 0;
                from_company_id = 0;
                from_branch_id = 0;
                from_location_id = 0;
                from_department_id = 0;
                m_blnflag = false;
                lueKapan.Focus();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateDetails())
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                DataTable dtbDetails_old = (DataTable)grdProcessReceiveJanged.DataSource;

                dgvProcessReceiveJanged.Columns.Clear();
                pivot pt = new pivot(objProcessReceive.Process_Quality_GetData(Val.ToString(lueType.Text), Val.ToString(lueClarity.EditValue), Val.ToString(lueSieve.EditValue), Val.ToString(lueQuality.EditValue)));
                DataTable dtbDetails = pt.PivotDataSuperPlus(new string[] { "quality_id", "quality" }, new string[] { "pcs", "carat", "rate", "amount" }, new AggregateFunction[] { AggregateFunction.Sum, AggregateFunction.Sum, AggregateFunction.Sum, AggregateFunction.Sum }, new string[] { "sieve_name" });
                //m_dtbDetail = objProcessReceive.GetData();

                DataTable DTab_Clarity = objRoughClarity.GetData();
                DataTable DTab_Sieve = objRoughSieve.GetData();
                DataTable Merge_Data = new DataTable();
                Merge_Data.Columns.Add("Clarity_Sieve_Pcs", typeof(string));
                Merge_Data.Columns.Add("Clarity_Sieve_Carat", typeof(string));
                Merge_Data.Columns.Add("Clarity_Sieve_Rate", typeof(string));
                Merge_Data.Columns.Add("Clarity_Sieve_Amount", typeof(string));

                for (int i = 0; i < DTab_Clarity.Rows.Count; i++)
                {
                    for (int j = 0; j < DTab_Sieve.Rows.Count; j++)
                    {
                        //string Merge = DTab_Clarity.Rows[i]["rough_clarity_id"] + "_" + DTab_Sieve.Rows[j]["rough_sieve_id"] + "_" + DTab_Clarity.Rows[i]["rough_clarity_name"] + DTab_Sieve.Rows[j]["sieve_name"] + "_carat";
                        Merge_Data.Rows.Add(DTab_Clarity.Rows[i]["rough_clarity_id"] + "_" + DTab_Sieve.Rows[j]["rough_sieve_id"] + "_" + DTab_Clarity.Rows[i]["rough_clarity_name"] + DTab_Sieve.Rows[j]["sieve_name"] + "_pcs", DTab_Clarity.Rows[i]["rough_clarity_id"] + "_" + DTab_Sieve.Rows[j]["rough_sieve_id"] + "_" + DTab_Clarity.Rows[i]["rough_clarity_name"] + DTab_Sieve.Rows[j]["sieve_name"] + "_carat", DTab_Clarity.Rows[i]["rough_clarity_id"] + "_" + DTab_Sieve.Rows[j]["rough_sieve_id"] + "_" + DTab_Clarity.Rows[i]["rough_clarity_name"] + DTab_Sieve.Rows[j]["sieve_name"] + "_rate", DTab_Clarity.Rows[i]["rough_clarity_id"] + "_" + DTab_Sieve.Rows[j]["rough_sieve_id"] + "_" + DTab_Clarity.Rows[i]["rough_clarity_name"] + DTab_Sieve.Rows[j]["sieve_name"] + "_amount");

                    }
                }
                int pcs_seq = 2;
                int carat_seq = 3;
                int rate_seq = 4;
                int amount_seq = 5;

                for (int i = 0; i < Merge_Data.Rows.Count; i++)
                {
                    for (int j = 0; j < dtbDetails.Columns.Count; j++)
                    {
                        if (dtbDetails.Columns.Contains(Merge_Data.Rows[i][0].ToString()))
                        {
                            dtbDetails.Columns[Merge_Data.Rows[i][0].ToString()].SetOrdinal(pcs_seq);
                            pcs_seq = pcs_seq + 4;
                            break;
                        }
                    }
                }

                for (int i = 0; i < Merge_Data.Rows.Count; i++)
                {
                    for (int j = 0; j < dtbDetails.Columns.Count; j++)
                    {
                        if (dtbDetails.Columns.Contains(Merge_Data.Rows[i][1].ToString()))
                        {
                            dtbDetails.Columns[Merge_Data.Rows[i][1].ToString()].SetOrdinal(carat_seq);
                            carat_seq = carat_seq + 4;
                            break;
                        }
                    }
                }

                for (int i = 0; i < Merge_Data.Rows.Count; i++)
                {
                    for (int j = 0; j < dtbDetails.Columns.Count; j++)
                    {
                        if (dtbDetails.Columns.Contains(Merge_Data.Rows[i][2].ToString()))
                        {
                            dtbDetails.Columns[Merge_Data.Rows[i][2].ToString()].SetOrdinal(rate_seq);
                            rate_seq = rate_seq + 4;
                            break;
                        }
                    }
                }

                for (int i = 0; i < Merge_Data.Rows.Count; i++)
                {
                    for (int j = 0; j < dtbDetails.Columns.Count; j++)
                    {
                        if (dtbDetails.Columns.Contains(Merge_Data.Rows[i][3].ToString()))
                        {
                            dtbDetails.Columns[Merge_Data.Rows[i][3].ToString()].SetOrdinal(amount_seq);
                            amount_seq = amount_seq + 4;
                            break;
                        }
                    }
                }

                DataColumn Total_Pcs = new System.Data.DataColumn("T_Pcs", typeof(System.Int32));
                DataColumn Total = new System.Data.DataColumn("Total", typeof(System.Decimal));
                DataColumn Rate = new System.Data.DataColumn("Rate", typeof(System.Decimal));
                DataColumn Amount = new System.Data.DataColumn("Amount", typeof(System.Decimal));
                Total_Pcs.DefaultValue = "0";
                Total.DefaultValue = "0.000";
                Rate.DefaultValue = "0.000";
                Amount.DefaultValue = "0.000";
                dtbDetails.Columns.Add(Total_Pcs);
                dtbDetails.Columns.Add(Total);
                dtbDetails.Columns.Add(Rate);
                dtbDetails.Columns.Add(Amount);

                DataColumnCollection columns = dtbDetails.Columns;
                if (dtbDetails_old != null)
                {
                    if (dtbDetails_old.Columns.Count > 0)
                    {
                        for (int i = 0; i < dtbDetails_old.Rows.Count; i++)
                        {
                            for (int j = 0; j < dtbDetails_old.Columns.Count; j++)
                            {
                                for (int k = 0; k < dtbDetails.Rows.Count; k++)
                                {
                                    if (dtbDetails.Rows[k]["quality"].ToString().Trim() == dtbDetails_old.Rows[i]["quality"].ToString().Trim())
                                    {
                                        if (columns.Contains(dtbDetails_old.Columns[j].ColumnName.ToString().Trim()))
                                        {
                                            dtbDetails.Rows[k][dtbDetails_old.Columns[j].ColumnName.ToString().Trim()] = dtbDetails_old.Rows[i][dtbDetails_old.Columns[j].ColumnName.ToString().Trim()];
                                        }
                                    }
                                }

                            }
                        }

                    }
                }

                dtTemp = dtbDetails.Copy();
                grdProcessReceiveJanged.DataSource = dtTemp;

                dgvProcessReceiveJanged.Columns["quality_id"].Visible = false;
                dgvProcessReceiveJanged.Columns["quality"].OptionsColumn.ReadOnly = true;
                dgvProcessReceiveJanged.Columns["quality"].OptionsColumn.AllowFocus = false;
                dgvProcessReceiveJanged.Columns["quality"].Fixed = FixedStyle.Left;
                dgvProcessReceiveJanged.Columns["T_Pcs"].OptionsColumn.ReadOnly = true;
                dgvProcessReceiveJanged.Columns["T_Pcs"].OptionsColumn.AllowFocus = false;
                dgvProcessReceiveJanged.Columns["Total"].OptionsColumn.ReadOnly = true;
                dgvProcessReceiveJanged.Columns["Total"].OptionsColumn.AllowFocus = false;
                dgvProcessReceiveJanged.Columns["Rate"].OptionsColumn.ReadOnly = true;
                dgvProcessReceiveJanged.Columns["Rate"].OptionsColumn.AllowFocus = false;
                dgvProcessReceiveJanged.Columns["Amount"].OptionsColumn.ReadOnly = true;
                dgvProcessReceiveJanged.Columns["Amount"].OptionsColumn.AllowFocus = false;

                for (int i = 0; i <= dtTemp.Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= dtTemp.Columns.Count - 1; j++)
                    {
                        if (dtTemp.Columns[j].ToString().Contains("amount"))
                        {
                            dgvProcessReceiveJanged.Columns[j].OptionsColumn.AllowEdit = false;
                        }
                    }
                }

                for (int i = 0; i <= dtTemp.Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= dtTemp.Columns.Count - 1; j++)
                    {
                        if (dtTemp.Columns[j].ToString().Contains("pcs"))
                        {
                            string pcs = dtTemp.Columns[j].ToString();
                            GridColumn column0 = dgvProcessReceiveJanged.Columns[pcs];
                            dgvProcessReceiveJanged.Columns[pcs].SummaryItem.DisplayFormat = "{0:n0}";
                            column0.SummaryItem.SummaryType = SummaryItemType.Sum;
                        }
                        if (dtTemp.Columns[j].ToString().Contains("carat"))
                        {
                            string carat = dtTemp.Columns[j].ToString();
                            GridColumn column1 = dgvProcessReceiveJanged.Columns[carat];
                            dgvProcessReceiveJanged.Columns[carat].SummaryItem.DisplayFormat = "{0:n3}";
                            column1.SummaryItem.SummaryType = SummaryItemType.Sum;
                        }

                        if (dtTemp.Columns[j].ToString().Contains("rate"))
                        {
                            string rate = dtTemp.Columns[j].ToString();
                            GridColumn column2 = dgvProcessReceiveJanged.Columns[rate];
                            dgvProcessReceiveJanged.Columns[rate].SummaryItem.DisplayFormat = "{0:n3}";
                            column2.SummaryItem.SummaryType = SummaryItemType.Custom;
                        }

                        if (dtTemp.Columns[j].ToString().Contains("amount"))
                        {
                            string amount = dtTemp.Columns[j].ToString();
                            GridColumn column3 = dgvProcessReceiveJanged.Columns[amount];
                            dgvProcessReceiveJanged.Columns[amount].SummaryItem.DisplayFormat = "{0:n3}";
                            column3.SummaryItem.SummaryType = SummaryItemType.Sum;
                        }
                        if (dtTemp.Columns[j].ColumnName.Contains("T_Pcs"))
                        {
                            string total_pcs = dtTemp.Columns[j].ToString();
                            GridColumn column7 = dgvProcessReceiveJanged.Columns[total_pcs];
                            dgvProcessReceiveJanged.Columns[total_pcs].SummaryItem.DisplayFormat = "{0:n0}";
                            column7.SummaryItem.SummaryType = SummaryItemType.Sum;
                        }
                        if (dtTemp.Columns[j].ColumnName.Contains("Total"))
                        {
                            string total = dtTemp.Columns[j].ToString();
                            GridColumn column4 = dgvProcessReceiveJanged.Columns[total];
                            dgvProcessReceiveJanged.Columns[total].SummaryItem.DisplayFormat = "{0:n3}";
                            column4.SummaryItem.SummaryType = SummaryItemType.Sum;
                        }
                        if (dtTemp.Columns[j].ColumnName.Contains("Rate"))
                        {
                            string totrate = dtTemp.Columns[j].ToString();
                            GridColumn column5 = dgvProcessReceiveJanged.Columns[totrate];
                            dgvProcessReceiveJanged.Columns[totrate].SummaryItem.DisplayFormat = "{0:n3}";
                            column5.SummaryItem.SummaryType = SummaryItemType.Custom;
                        }
                        if (dtTemp.Columns[j].ColumnName.Contains("Amount"))
                        {
                            string totamount = dtTemp.Columns[j].ToString();
                            GridColumn column6 = dgvProcessReceiveJanged.Columns[totamount];
                            dgvProcessReceiveJanged.Columns[totamount].SummaryItem.DisplayFormat = "{0:n3}";
                            column6.SummaryItem.SummaryType = SummaryItemType.Sum;
                        }
                    }
                    break;
                }

                dgvProcessReceiveJanged.OptionsView.ShowFooter = true;
                dgvProcessReceiveJanged.BestFitColumns();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
            }
        }
        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateDetails())
                {
                    return;
                }

                pivot pt = new pivot(objProcessReceive.GetShowData(Val.ToString(lueType.Text)));
                dtTemp = pt.PivotDataSuperPlus(new string[] { "quality_id", "quality" }, new string[] { "carat", "rate", "amount" }, new AggregateFunction[] { AggregateFunction.Sum, AggregateFunction.Sum, AggregateFunction.Sum }, new string[] { "sieve_name" });

                grdProcessReceiveJanged.DataSource = dtTemp;

                dgvProcessReceiveJanged.Columns["quality_id"].Visible = false;

                for (int i = 0; i <= dtTemp.Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= dtTemp.Columns.Count - 1; j++)
                    {
                        if (dtTemp.Columns[j].ToString().Contains("amount"))
                        {
                            dgvProcessReceiveJanged.Columns[j].OptionsColumn.AllowEdit = false;
                        }
                    }
                }

                for (int i = 0; i <= dtTemp.Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= dtTemp.Columns.Count - 1; j++)
                    {
                        if (dtTemp.Columns[j].ToString().Contains("carat"))
                        {
                            string carat = dtTemp.Columns[j].ToString();
                            GridColumn column1 = dgvProcessReceiveJanged.Columns[carat];
                            dgvProcessReceiveJanged.Columns[carat].SummaryItem.DisplayFormat = "{0:n3}";
                            column1.SummaryItem.SummaryType = SummaryItemType.Sum;
                        }

                        if (dtTemp.Columns[j].ToString().Contains("rate"))
                        {
                            string rate = dtTemp.Columns[j].ToString();
                            GridColumn column2 = dgvProcessReceiveJanged.Columns[rate];
                            dgvProcessReceiveJanged.Columns[rate].SummaryItem.DisplayFormat = " {0:n3}";
                            column2.SummaryItem.SummaryType = SummaryItemType.Custom;
                        }

                        if (dtTemp.Columns[j].ToString().Contains("amount"))
                        {
                            string amount = dtTemp.Columns[j].ToString();
                            GridColumn column3 = dgvProcessReceiveJanged.Columns[amount];
                            dgvProcessReceiveJanged.Columns[amount].SummaryItem.DisplayFormat = "{0:n3}";
                            column3.SummaryItem.SummaryType = SummaryItemType.Sum;
                        }
                    }
                    break;
                }
                dgvProcessReceiveJanged.OptionsView.ShowFooter = true;
                //ShowSummary();
                dgvProcessReceiveJanged.BestFitColumns();
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                //blnReturn = false;
            }
        }
        DataTable dtIssOS = new DataTable();
        private void lueCutNo_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (m_IsLot == 0)
                {
                    if (!m_blnflag)
                    {
                        if (lueCutNo.EditValue != null)
                        {
                            if (m_dtbParam.Rows.Count > 0)
                            {
                                DataRow[] dr = m_dtbParam.Select("rough_cut_id ='" + Val.ToInt64(lueCutNo.EditValue) + "'");
                                txtLotId.Text = Val.ToString(dr[0]["lot_id"]);
                                MFGProcessReceive objProcessRecieve = new MFGProcessReceive();
                                if (txtLotId.Text != string.Empty || Val.ToInt64(txtLotId.Text) != 0)
                                {
                                    dtIssOS = objProcessRecieve.Carat_OutStanding_Janged_GetData(Val.ToInt64(txtLotId.Text), Val.ToInt(0), Val.ToInt(0), 1, "R", Val.ToInt64(txtJangedNo.Text));
                                    if (dtIssOS.Rows.Count > 0)
                                    {
                                        txtBalancePcs.Text = Val.ToString(Val.ToDecimal(dtIssOS.Rows[0]["pcs"]));
                                        txtBalanceCarat.Text = Val.ToString(Val.ToDecimal(dtIssOS.Rows[0]["carat"]));
                                        m_kapan_id = Val.ToInt(lueKapan.EditValue);
                                        m_balcarat = Val.ToDecimal(dtIssOS.Rows[0]["carat"]);
                                    }
                                    else
                                    {
                                        Global.Message("Lot Not Issue");
                                        return;
                                    }

                                    DataTable DTab_Process = dtIssOS.Select("(process_name = 'AKHU BHAR' OR process_name = 'ASSORT')").CopyToDataTable();
                                    DataTable DTab_Janged_No = new DataTable();
                                    if (DTab_Process.Rows.Count > 0)
                                    {
                                        if (DTab_Process.Rows[0]["process_name"].ToString() == "AKHU BHAR")
                                        {
                                            DTab_Janged_No = objProcessRecieve.Janged_No_Receive_GetData(Val.ToInt64(txtLotId.Text), Val.ToInt32(DTab_Process.Rows[0]["process_id"]));
                                        }
                                        else if (DTab_Process.Rows[0]["process_name"].ToString() == "ASSORT")
                                        {
                                            DTab_Janged_No = objProcessRecieve.Janged_No_Receive_GetData(Val.ToInt64(txtLotId.Text), Val.ToInt32(DTab_Process.Rows[0]["process_id"]));
                                        }

                                        if (DTab_Janged_No.Rows.Count > 0)
                                        {
                                            txtJangedNo.Text = Val.ToInt64(DTab_Janged_No.Rows[0]["janged_no"]).ToString();
                                        }
                                    }
                                }
                            }
                            else
                            {

                            }
                        }
                        m_blnflag = false;
                    }
                    else
                    {
                        m_blnflag = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void txtLotId_EditValueChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (txtLotId.EditValue != System.DBNull.Value)
            //    {
            //        if (m_dtCut.Rows.Count > 0)
            //        {
            //            DataRow[] dr = m_dtCut.Select("lot_id ='" + Val.ToString(txtLotId.Text) + "'");
            //            if (dr.Length > 0)
            //            {
            //                lueCutNo.EditValue = Val.ToInt64(dr[0]["rough_cut_id"]);
            //            }
            //            else
            //            {
            //                Global.Message("Cut No Data Not found");
            //                lueCutNo.EditValue = null;
            //                return;
            //            }
            //            //lueCutNo.Tag = Val.ToString(dr[0]["rough_cut_no"]);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Global.Message(ex.ToString());
            //    return;
            //}
        }
        private void lueKapan_EditValueChanged(object sender, EventArgs e)
        {
            if (m_IsLot == 0)
            {
                m_dtbParam = new DataTable();
                if (lueKapan.Text.ToString() != "")
                {
                    m_dtbParam = Global.GetRoughKapanWise_Data(Val.ToInt(lueKapan.EditValue));
                }
                lueCutNo.Properties.DataSource = m_dtbParam;
                lueCutNo.Properties.ValueMember = "rough_cut_id";
                lueCutNo.Properties.DisplayMember = "rough_cut_no";
            }
        }
        private void grdProcessReceiveJanged_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    (grdProcessReceiveJanged.FocusedView as ColumnView).FocusedRowHandle++;
                    e.Handled = true;
                }
            }
            catch
            {
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

        private void lueClarity_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmMfgRoughClarityMaster frmClarity = new FrmMfgRoughClarityMaster();
                frmClarity.ShowDialog();
                Global.LOOKUPRoughClarity(lueClarity);
            }
        }

        private void lueQuality_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmMfgQualityMaster frmRoughQuality = new FrmMfgQualityMaster();
                frmRoughQuality.ShowDialog();
                Global.LOOKUPRoughQuality(lueQuality);
            }
        }
        private void lueType_EditValueChanged(object sender, EventArgs e)
        {
            MfgRoughClarityMaster objClarity = new MfgRoughClarityMaster();
            DataTable dtbClarity = new DataTable();
            DataTable dtbdetails = new DataTable();
            dtbClarity = objClarity.GetData();

            if (lueType.EditValue.ToString() != "")
            {
                if (lueType.EditValue.ToString() != "BOTH")
                {
                    dtbClarity.DefaultView.RowFilter = "type = '" + lueType.EditValue + "'";
                    dtbClarity.DefaultView.ToTable();
                    dtbdetails = dtbClarity.DefaultView.ToTable();
                }
                else
                {
                    dtbdetails = dtbClarity.Copy();
                }

                lueClarity.Properties.DataSource = dtbdetails;
                lueClarity.Properties.ValueMember = "rough_clarity_id";
                lueClarity.Properties.DisplayMember = "rough_clarity_name";
            }
        }

        private void dgvProcessReceiveJanged_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;

                if (view.FocusedColumn.FieldName.Contains("carat"))
                {
                    double carat = 0.000;
                    if (!double.TryParse(e.Value as string, out carat))
                    {
                        e.Valid = false;
                        e.ErrorText = "Input string was not in a correct format.";
                    }
                }
                else if (view.FocusedColumn.FieldName.Contains("rate"))
                {
                    double rate = 0.000;
                    if (!double.TryParse(e.Value as string, out rate))
                    {
                        e.Valid = false;
                        e.ErrorText = "Input string was not in a correct format.";
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
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

        private void lueParty_EditValueChanged(object sender, EventArgs e)
        {
            if (Val.ToString(lueParty.EditValue) != "")
            {
                PartyMaster objParty = new PartyMaster();
                DataTable Party = objParty.GetData(1);
                Party.DefaultView.RowFilter = "party_id = '" + lueParty.EditValue + "'";
                Party.DefaultView.ToTable();
                DataTable dtbParty = Party.DefaultView.ToTable();
                if (dtbParty.Rows.Count > 0)
                {
                    outside = Val.ToBooleanToInt(dtbParty.Rows[0]["is_outside"]);
                }


            }
        }

        private void txtJangedNo_Validated(object sender, EventArgs e)
        {
            if (txtJangedNo.Text != "")
            {
                objMFGJangedReceive = new MFGJangedReceive();
                m_JangedId = objMFGJangedReceive.Janged_ID_GetData(Val.ToInt64(txtJangedNo.Text));
                DataTable DTab_Janged = objMFGJangedReceive.Janged_Bal_GetData(Val.ToInt64(txtJangedNo.Text));
                if (Val.ToString(DTab_Janged.Rows[0]["process_name"]) == "ASSORT" || Val.ToString(DTab_Janged.Rows[0]["process_name"]) == "AKHU BHAR")
                {
                    if (DTab_Janged.Rows.Count > 0)
                    {
                        m_IsLot = 1;
                        from_company_id = Val.ToInt64(DTab_Janged.Rows[0]["company_id"]);
                        from_branch_id = Val.ToInt64(DTab_Janged.Rows[0]["branch_id"]);
                        from_location_id = Val.ToInt64(DTab_Janged.Rows[0]["location_id"]);
                        from_department_id = Val.ToInt64(DTab_Janged.Rows[0]["to_department_id"]);
                        txtBalancePcs.Text = Val.ToInt(DTab_Janged.Rows[0]["pcs"]).ToString();
                        txtBalanceCarat.Text = Val.ToDecimal(DTab_Janged.Rows[0]["carat"]).ToString();
                        lueParty.EditValue = Val.ToInt32(DTab_Janged.Rows[0]["party_id"]);
                        lueKapan.EditValue = Val.ToInt64(DTab_Janged.Rows[0]["kapan_id"]);
                        lueCutNo.EditValue = Val.ToInt64(DTab_Janged.Rows[0]["rough_cut_id"]);
                        txtLotId.Text = Val.ToString(DTab_Janged.Rows[0]["lot_id"]);
                        lueProcess.EditValue = Val.ToInt32(DTab_Janged.Rows[0]["process_id"]);
                        lueSubProcess.EditValue = Val.ToInt32(DTab_Janged.Rows[0]["sub_process_id"]);
                        lueManager.EditValue = Val.ToInt64(DTab_Janged.Rows[0]["manager_id"]);
                        lueEmployee.EditValue = Val.ToInt64(DTab_Janged.Rows[0]["employee_id"]);

                        if (lueCutNo.EditValue != System.DBNull.Value)
                        {
                            if (m_dtbParam.Rows.Count > 0)
                            {
                                DataRow[] dr = m_dtbParam.Select("rough_cut_no ='" + Val.ToString(lueCutNo.Text) + "'");
                                if (dr.Length > 0)
                                {

                                    if (txtLotId.Text != string.Empty || Val.ToInt64(txtLotId.Text) != 0)
                                    {
                                        MFGProcessReceive objProcessRecieve = new MFGProcessReceive();
                                        //GetOsCarat(Val.ToInt64(txtLotId.Text));
                                        DataTable dtIssOS = new DataTable();
                                        dtIssOS = objProcessRecieve.Carat_OutStanding_Janged_GetData(Val.ToInt64(txtLotId.Text), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue), 0, "R", Val.ToInt64(txtJangedNo.Text));

                                        if (dtIssOS.Rows.Count > 0)
                                        {
                                            m_OScarat = Val.ToDecimal(Val.ToDecimal(dtIssOS.Rows[0]["carat"]));
                                            txtBalanceCarat.Text = Val.ToString(m_OScarat);
                                            lblOsPcs.Text = Val.ToString(dtIssOS.Rows[0]["pcs"]);
                                            lblOsCarat.Text = Val.ToString(dtIssOS.Rows[0]["carat"]);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Global.Message("Issue Janged Balance Data Not Found..");
                        txtJangedNo.Text = "";
                        txtBalancePcs.Text = "0";
                        txtBalanceCarat.Text = "0";
                        lueKapan.Focus();
                        return;
                    }
                }
                else
                {
                    Global.Message("Lot Not Issue In Assort / Akhu Bhar Process");
                    return;
                }
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

        private void luePurity_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmClarityMaster frmPurity = new FrmClarityMaster();
                frmPurity.ShowDialog();
                Global.LOOKUPPurity(luePurity);
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

        private void lueSubProcess_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmMfgSubProcessMaster frmSubProcess = new FrmMfgSubProcessMaster();
                frmSubProcess.ShowDialog();
                Global.LOOKUPSubProcess(lueSubProcess);
            }
        }

        private void lueProcess_EditValueChanged(object sender, EventArgs e)
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

                        lueSubProcess.Properties.DataSource = dtbdetail;
                        lueSubProcess.Properties.ValueMember = "sub_process_id";
                        lueSubProcess.Properties.DisplayMember = "sub_process_name";
                        lueSubProcess.EditValue = System.DBNull.Value;
                    }
                    //if (lueProcess.Text == "CHIPIYO FINAL")
                    //{
                    //    txtRate.Text = Val.ToString(Math.Round(m_chipyo_rate, 2));
                    //}
                }
                //if (lueProcess.EditValue != System.DBNull.Value && lueSubProcess.EditValue != System.DBNull.Value && Val.ToInt64(txtLotId.Text) != 0)
                //{
                //    DataTable dtIss = new DataTable();
                //    dtIss = objProcessRecieve.GetIssueID(Val.ToInt64(txtLotId.Text), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue));
                //    if (dtIss.Rows.Count > 0)
                //    {
                //        Global.Message("Lot is already issue in this process.");
                //    }

                //}
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void dgvProcessReceiveJanged_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                DataTable dtAmount = new DataTable();
                dtAmount = (DataTable)grdProcessReceiveJanged.DataSource;
                string[] col = e.Column.FieldName.Split('_');
                string currcol = "";
                if (col.Length > 4 && e.Column.FieldName.Contains("_") && col != null)
                {
                    currcol = col[0] + "_" + col[1] + "_" + col[2] + "_" + col[3];
                }
                Int32 pcs = 0;
                decimal rate = 0;
                decimal carat = 0;
                Int32 total_pcs = 0;
                decimal total = 0;
                decimal totRate = 0;
                decimal totAmount = 0;
                //if (col.Length > 3)
                //{
                for (int i = 0; i <= dtAmount.Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= dtAmount.Columns.Count - 1; j++)
                    {
                        if (dtAmount.Columns[j].ToString().Contains("pcs") && dtAmount.Columns[j].ColumnName.Contains(currcol))
                        {
                            pcs = Val.ToInt32(dtAmount.Rows[i][j]);
                            total_pcs += pcs;
                        }
                        if (dtAmount.Columns[j].ToString().Contains("carat") && dtAmount.Columns[j].ColumnName.Contains(currcol))
                        {
                            carat = Val.ToDecimal(dtAmount.Rows[i][j]);
                            total += carat;
                        }
                        else if (dtAmount.Columns[j].ToString().Contains("rate") && dtAmount.Columns[j].ColumnName.Contains(currcol))
                        {
                            rate = Val.ToDecimal(dtAmount.Rows[i][j]);
                        }
                        else if (dtAmount.Columns[j].ToString().Contains("amount") && dtAmount.Columns[j].ColumnName.Contains(currcol))
                        {
                            dtAmount.Rows[i][j] = (carat * rate).ToString();
                            totAmount += (carat * rate);
                            //break;
                        }
                        if (dtAmount.Columns[j].ColumnName.Contains("T_Pcs"))
                        {
                            dtAmount.Rows[i][j] = Val.ToInt32(total_pcs);

                        }
                        if (dtAmount.Columns[j].ColumnName.Contains("Total"))
                        {
                            dtAmount.Rows[i][j] = Math.Round(total, 3).ToString();

                        }
                        else if (dtAmount.Columns[j].ColumnName.Contains("Rate"))
                        {
                            if (totAmount != 0 && total != 0)
                            {
                                totRate = totAmount / total;
                            }
                            else
                            {
                                totRate = 0;
                            }
                            dtAmount.Rows[i][j] = Math.Round(totRate, 3).ToString();

                        }
                        else if (dtAmount.Columns[j].ColumnName.Contains("Amount"))
                        {
                            decimal amount = Math.Round((total * totRate), 0);
                            dtAmount.Rows[i][j] = amount.ToString();
                            break;
                        }

                    }
                    totAmount = 0;
                    total = 0;
                    total_pcs = 0;
                    totRate = 0;
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }

        private void backgroundWorker_ProcRecJanged_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                MFGJangedReceive MFGJangedReceive = new MFGJangedReceive();
                MFGJangedReceive_Property objMFGJangedReceiveProperty = new MFGJangedReceive_Property();

                Conn = new BeginTranConnection(true, false);

                DataTable m_DTab = new DataTable();
                ArrayList MyArrayList = new ArrayList();

                IntRes = 0;
                Receive_IntRes = 0;
                JangedNo_IntRes = 0;
                Dept_IntRes = 0;
                Janged_IntRes = 0;
                Loss_Count = 0;
                JangedSrNo_IntRes = 0;
                Int64 NewHistory_Union_Id = 0;
                Int64 Lot_SrNo = 0;
                try
                {

                    m_DTab = (DataTable)grdProcessReceiveJanged.DataSource;

                    DataTable dtbDetail = m_DTab.Copy();
                    decimal Carat = 0;
                    decimal Pcs = 0;

                    for (int i = 0; i <= dtTemp.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j <= dtTemp.Columns.Count - 1; j++)
                        {
                            if (dtTemp.Columns[j].ToString().Contains("pcs"))
                            {
                                if (Val.ToDecimal(dtTemp.Rows[i][j]) != 0)
                                {
                                    Pcs = Pcs + Val.ToDecimal(dtTemp.Rows[i][j]);
                                }
                            }
                        }
                    }

                    for (int i = 0; i <= dtTemp.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j <= dtTemp.Columns.Count - 1; j++)
                        {
                            if (dtTemp.Columns[j].ToString().Contains("carat"))
                            {
                                if (Val.ToDecimal(dtTemp.Rows[i][j]) != 0)
                                {
                                    Carat = Carat + Val.ToDecimal(dtTemp.Rows[i][j]);
                                }
                            }
                        }
                    }

                    if (Val.ToDecimal(lblOsCarat.Text) < Carat)
                    {
                        Global.Message("Carat more then Outstanding Carat");
                        btnSave.Enabled = true;
                        IntRes = -1;
                        return;
                    }
                    //if (Val.ToDecimal(lblOsPcs.Text) < Pcs)
                    //{
                    //    Global.Message("Pcs more then Outstanding Pcs");
                    //    btnSave.Enabled = true;
                    //    IntRes = -1;
                    //    return;
                    //}

                    for (int i = dtbDetail.Columns.Count - 4; i >= 2; i--)
                    {
                        string strNew = Val.ToString(dtbDetail.Columns[i]);
                        string str = strNew.Substring(strNew.LastIndexOf("_") + 1);
                        dtbDetail.Columns[Val.ToString(dtbDetail.Columns[i])].ColumnName = strNew.Split('_')[0] + "_" + strNew.Split('_')[1] + "_" + str;
                    }
                    //MFGProcessReceive MFGProcessReceive = new MFGProcessReceive();

                    //DataTable dtIssueDet = MFGProcessReceive.ProcessIssue_GetData(Val.ToInt64(txtLotId.Text), Val.ToString(lueProcess.Text));
                    //objMFGJangedReceiveProperty.Issue_id = Val.ToInt(dtIssueDet.Rows[0]["issue_id"]);
                    //if (Val.ToInt(dtIssueDet.Rows[0]["issue_id"]) == 0)
                    //{
                    //    Global.Message("Issue data not found in this Cut No: " + Val.ToString(lueCutNo.Text));
                    //    return;
                    //}

                    if (dtbDetail.Rows.Count > 0)
                    {
                        for (int i = dtbDetail.Columns.Count - 4; i >= 2; i--)
                        {
                            if (Val.ToString(dtbDetail.Columns[i]) == Val.ToString(dtbDetail.Columns[i]).Split('_')[0] + "_" + Val.ToString(dtbDetail.Columns[i]).Split('_')[1] + "_carat")
                            {
                                foreach (DataRow Drw in dtbDetail.Rows)
                                {
                                    objMFGJangedReceiveProperty.rough_clarity_id = Val.ToInt(Val.ToString(dtbDetail.Columns[i]).Split('_')[1]);
                                    objMFGJangedReceiveProperty.rough_sieve_id = Val.ToInt(Val.ToString(dtbDetail.Columns[i]).Split('_')[0]);
                                    objMFGJangedReceiveProperty.pcs = Val.ToInt32(Drw[Val.ToString(objMFGJangedReceiveProperty.rough_sieve_id) + "_" + Val.ToString(objMFGJangedReceiveProperty.rough_clarity_id) + "_" + "pcs"]);
                                    objMFGJangedReceiveProperty.carat = Val.ToDecimal(Drw[Val.ToString(objMFGJangedReceiveProperty.rough_sieve_id) + "_" + Val.ToString(objMFGJangedReceiveProperty.rough_clarity_id) + "_" + "carat"]);
                                    objMFGJangedReceiveProperty.rate = Val.ToDecimal(Drw[Val.ToString(objMFGJangedReceiveProperty.rough_sieve_id) + "_" + Val.ToString(objMFGJangedReceiveProperty.rough_clarity_id) + "_" + "rate"]);

                                    if (objMFGJangedReceiveProperty.carat > 0)
                                    {
                                        if (objMFGJangedReceiveProperty.rate > 0)
                                        {

                                        }
                                        else
                                        {
                                            Global.Message("Rate is Zero..So pLease Put Rate and Continue..");
                                            btnSave.Enabled = true;
                                            return;
                                        }
                                    }

                                }
                            }
                        }
                    }

                    if (dtbDetail.Rows.Count > 0)
                    {
                        for (int i = dtbDetail.Columns.Count - 4; i >= 2; i--)
                        {
                            if (Val.ToString(dtbDetail.Columns[i]) == Val.ToString(dtbDetail.Columns[i]).Split('_')[0] + "_" + Val.ToString(dtbDetail.Columns[i]).Split('_')[1] + "_carat")
                            {
                                foreach (DataRow Drw in dtbDetail.Rows)
                                {
                                    objMFGJangedReceiveProperty.janged_no = JangedNo_IntRes;
                                    objMFGJangedReceiveProperty.previous_janged_id = Val.ToInt32(m_JangedId);
                                    objMFGJangedReceiveProperty.previous_janged_no = Val.ToInt64(txtJangedNo.Text);
                                    objMFGJangedReceiveProperty.lot_id = Val.ToInt64(txtLotId.Text);
                                    objMFGJangedReceiveProperty.union_id = IntRes;
                                    objMFGJangedReceiveProperty.kapan_id = Val.ToInt64(lueKapan.EditValue);
                                    objMFGJangedReceiveProperty.janged_date = Val.DBDate(dtpReceiveDate.Text);
                                    objMFGJangedReceiveProperty.from_company_id = from_company_id;
                                    objMFGJangedReceiveProperty.from_branch_id = from_branch_id;
                                    objMFGJangedReceiveProperty.from_location_id = from_location_id;
                                    objMFGJangedReceiveProperty.from_department_id = from_department_id;
                                    objMFGJangedReceiveProperty.manager_id = Val.ToInt64(lueManager.EditValue);
                                    objMFGJangedReceiveProperty.to_manager_id = Val.ToInt(lueManager.EditValue);
                                    objMFGJangedReceiveProperty.employee_id = Val.ToInt64(lueEmployee.EditValue);
                                    objMFGJangedReceiveProperty.process_id = Val.ToInt64(lueProcess.EditValue);
                                    objMFGJangedReceiveProperty.sub_process_id = Val.ToInt64(lueSubProcess.EditValue);
                                    objMFGJangedReceiveProperty.purity_id = Val.ToInt64(luePurity.EditValue);

                                    objMFGJangedReceiveProperty.party_id = Val.ToInt64(lueParty.EditValue);
                                    objMFGJangedReceiveProperty.form_id = Val.ToInt(m_numForm_id);
                                    objMFGJangedReceiveProperty.is_outside = Val.ToInt(outside);
                                    objMFGJangedReceiveProperty.janged_union_id = Janged_IntRes;
                                    objMFGJangedReceiveProperty.dept_union_id = Dept_IntRes;

                                    objMFGJangedReceiveProperty.rough_quality_id = Val.ToInt(Drw["quality_id"]);
                                    objMFGJangedReceiveProperty.rough_cut_id = Val.ToInt(lueCutNo.EditValue);
                                    objMFGJangedReceiveProperty.rough_clarity_id = Val.ToInt(Val.ToString(dtbDetail.Columns[i]).Split('_')[1]);
                                    objMFGJangedReceiveProperty.rough_sieve_id = Val.ToInt(Val.ToString(dtbDetail.Columns[i]).Split('_')[0]);
                                    objMFGJangedReceiveProperty.pcs = Val.ToInt32(Drw[Val.ToString(objMFGJangedReceiveProperty.rough_sieve_id) + "_" + Val.ToString(objMFGJangedReceiveProperty.rough_clarity_id) + "_" + "pcs"]);
                                    objMFGJangedReceiveProperty.carat = Val.ToDecimal(Drw[Val.ToString(objMFGJangedReceiveProperty.rough_sieve_id) + "_" + Val.ToString(objMFGJangedReceiveProperty.rough_clarity_id) + "_" + "carat"]);
                                    objMFGJangedReceiveProperty.rate = Val.ToDecimal(Drw[Val.ToString(objMFGJangedReceiveProperty.rough_sieve_id) + "_" + Val.ToString(objMFGJangedReceiveProperty.rough_clarity_id) + "_" + "rate"]);
                                    objMFGJangedReceiveProperty.amount = Val.ToDecimal(Drw[Val.ToString(objMFGJangedReceiveProperty.rough_sieve_id) + "_" + Val.ToString(objMFGJangedReceiveProperty.rough_clarity_id) + "_" + "amount"]);
                                    objMFGJangedReceiveProperty.union_id = IntRes;
                                    objMFGJangedReceiveProperty.receive_union_id = Receive_IntRes;
                                    objMFGJangedReceiveProperty.form_id = m_numForm_id;
                                    objMFGJangedReceiveProperty.history_union_id = NewHistory_Union_Id;
                                    if (objMFGJangedReceiveProperty.carat == 0)
                                        continue;
                                    if (Loss_Count == 0)
                                    {
                                        if (txtWeightPlus.Text != "" || txtWeightLoss.Text != "")
                                        {
                                            objMFGJangedReceiveProperty.carat_plus = Val.ToDecimal(txtWeightPlus.Text);
                                            objMFGJangedReceiveProperty.loss_carat = Val.ToDecimal(txtWeightLoss.Text);
                                            objMFGJangedReceiveProperty.loss_count = Val.ToInt(Loss_Count);
                                            Loss_Count = Loss_Count + 1;
                                        }
                                        else
                                        {
                                            objMFGJangedReceiveProperty.carat_plus = Val.ToDecimal(0);
                                            objMFGJangedReceiveProperty.loss_carat = Val.ToDecimal(0);
                                            objMFGJangedReceiveProperty.loss_count = Val.ToInt(1);
                                            Loss_Count = Loss_Count + 1;
                                        }
                                    }
                                    else
                                    {
                                        objMFGJangedReceiveProperty.carat_plus = Val.ToDecimal(0);
                                        objMFGJangedReceiveProperty.loss_carat = Val.ToDecimal(0);
                                        objMFGJangedReceiveProperty.loss_count = Val.ToInt(Loss_Count);
                                    }
                                    objMFGJangedReceiveProperty.janged_srno = JangedSrNo_IntRes;
                                    objMFGJangedReceiveProperty.lot_srno = Lot_SrNo;

                                    objMFGJangedReceiveProperty = MFGJangedReceive.Save(objMFGJangedReceiveProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                                    IntRes = objMFGJangedReceiveProperty.union_id;
                                    Receive_IntRes = objMFGJangedReceiveProperty.receive_union_id;
                                    Janged_IntRes = objMFGJangedReceiveProperty.janged_union_id;
                                    JangedNo_IntRes = objMFGJangedReceiveProperty.janged_no;
                                    Dept_IntRes = objMFGJangedReceiveProperty.dept_union_id;
                                    NewHistory_Union_Id = Val.ToInt64(objMFGJangedReceiveProperty.history_union_id);
                                    JangedSrNo_IntRes = objMFGJangedReceiveProperty.janged_srno;
                                    Lot_SrNo = Val.ToInt64(objMFGJangedReceiveProperty.lot_srno);
                                }
                            }
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

        private void backgroundWorker_ProcRecJanged_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (IntRes > 0)
                {
                    Global.Confirm("Process Receive Janged Save Succesfully");
                    btnClear_Click(null, null);
                    lueType.EditValue = "BOTH";
                }
                else
                {
                    Global.Confirm("Error In Process Receive Janged");
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }
        #region GridEvents
        private void dgvProcessReceiveJanged_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            try
            {
                DataTable dtAmount = new DataTable();
                dtAmount = (DataTable)grdProcessReceiveJanged.DataSource;

                Int32 pcs = 0;
                decimal rate = 0;
                decimal carat = 0;
                decimal amount = 0;
                Int32 totpcs = 0;
                decimal totrate = 0;
                decimal totcarat = 0;
                decimal totamount = 0;

                string column = "";
                for (int j = 0; j <= dtAmount.Columns.Count - 1; j++)
                {
                    if (dtAmount.Columns[j].ToString().Contains("pcs"))
                    {
                        pcs = dtAmount.AsEnumerable().Sum(y => Val.ToInt32(y[dtAmount.Columns[j]]));
                    }
                    if (dtAmount.Columns[j].ToString().Contains("carat"))
                    {
                        carat = dtAmount.AsEnumerable().Sum(x => Val.ToDecimal(x[dtAmount.Columns[j]]));
                    }
                    if (dtAmount.Columns[j].ToString().Contains("amount"))
                    {
                        amount = dtAmount.AsEnumerable().Sum(x => Math.Round(Val.ToDecimal(x[dtAmount.Columns[j]]), 0));
                    }
                    if (dtAmount.Columns[j].ToString().Contains("rate"))
                    {
                        column = dtAmount.Columns[j].ToString();
                        amount = 0;
                    }
                    if (Val.ToDecimal(amount) > 0 && Val.ToDecimal(carat) > 0)
                    {
                        if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == column)
                        {
                            rate = amount / carat;
                            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                                e.TotalValue = rate;
                            column = "";
                            pcs = 0;
                            carat = 0;
                            amount = 0;
                        }
                    }
                    if (dtAmount.Columns[j].ColumnName.Contains("T_Pcs"))
                    {
                        totpcs = dtAmount.AsEnumerable().Sum(y => Val.ToInt32(y[dtAmount.Columns[j]]));
                        //carat = Val.ToDecimal(dtAmount.Rows[i][j]);
                    }
                    if (dtAmount.Columns[j].ColumnName.Contains("Total"))
                    {
                        totcarat = dtAmount.AsEnumerable().Sum(x => Val.ToDecimal(x[dtAmount.Columns[j]]));
                        //carat = Val.ToDecimal(dtAmount.Rows[i][j]);
                    }
                    if (dtAmount.Columns[j].ColumnName.Contains("Amount"))
                    {
                        totamount = dtAmount.AsEnumerable().Sum(x => Math.Round(Val.ToDecimal(x[dtAmount.Columns[j]]), 0));
                    }

                    if (Val.ToDecimal(totamount) > 0 && Val.ToDecimal(totcarat) > 0)
                    {
                        if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == "Rate")
                        {
                            totrate = totamount / totcarat;
                            //((DevExpress.XtraGrid.GridSummaryItem)e.Item).;
                            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                                e.TotalValue = totrate;
                            //column = "";
                            totpcs = 0;
                            totamount = 0;
                            totcarat = 0;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
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
                if (txtJangedNo.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Janged No"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtJangedNo.Focus();
                    }
                }

                if (Val.ToString(lueParty.EditValue) == string.Empty)
                {
                    lstError.Add(new ListError(12, "Party"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueParty.Focus();
                    }
                }
                if (Val.ToString(lueKapan.EditValue) == string.Empty)
                {
                    lstError.Add(new ListError(12, "Kapan"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueKapan.Focus();
                    }
                }
                if (Val.ToString(lueCutNo.EditValue) == string.Empty)
                {
                    lstError.Add(new ListError(12, "Cut"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueCutNo.Focus();
                    }
                }
                if (Val.ToString(lueProcess.EditValue) == string.Empty)
                {
                    lstError.Add(new ListError(12, "Process"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueProcess.Focus();
                    }
                }
                if (Val.ToString(lueSubProcess.EditValue) == string.Empty)
                {
                    lstError.Add(new ListError(12, "Sub Process"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueSubProcess.Focus();
                    }
                }
                if (Val.ToString(luePurity.EditValue) == string.Empty)
                {
                    lstError.Add(new ListError(12, "Purity"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        luePurity.Focus();
                    }
                }
                if (Val.ToString(lueManager.EditValue) == string.Empty)
                {
                    lstError.Add(new ListError(12, "Manager"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueManager.Focus();
                    }
                }
                if (Val.ToString(lueEmployee.EditValue) == string.Empty)
                {
                    lstError.Add(new ListError(12, "Employee"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueEmployee.Focus();
                    }
                }


                if (lueSieve.EditValue.ToString() == string.Empty)
                {
                    lstError.Add(new ListError(12, "Rough Sieve"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueSieve.Focus();
                    }
                }
                if (lueClarity.EditValue.ToString() == string.Empty)
                {
                    lstError.Add(new ListError(12, "Clarity"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueClarity.Focus();
                    }
                }
                if (lueQuality.EditValue.ToString() == string.Empty)
                {
                    lstError.Add(new ListError(12, "Quality"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueQuality.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                lstError.Add(new ListError(ex));
            }
            return (!(BLL.General.ShowErrors(lstError)));
        }
        public void ShowSummary()
        {
            //GridColumn column1 = dgvProcessReceive.Columns["1_(0+3.5)_carat"];
            //dgvProcessReceive.Columns["1_(0+3.5)_carat"].SummaryItem.DisplayFormat = "";
            //column1.SummaryItem.SummaryType = SummaryItemType.Sum;
            //GridColumn column2 = dgvProcessReceive.Columns["1_(0+3.5)_rate"];
            //dgvProcessReceive.Columns["1_(0+3.5)_rate"].SummaryItem.DisplayFormat = "";
            //column2.SummaryItem.SummaryType = SummaryItemType.Custom;
            //GridColumn column3 = dgvProcessReceive.Columns["1_(0+3.5)_amount"];
            //dgvProcessReceive.Columns["1_(0+3.5)_amount"].SummaryItem.DisplayFormat = "";
            //column3.SummaryItem.SummaryType = SummaryItemType.Sum;
        }
        public void GetOsCarat(Int64 lotId)
        {
            try
            {
                if (lotId > 0)
                {
                    m_dtOutstanding = objProcessReceive.GetBalanceCarat(lotId);
                }


                if (m_dtOutstanding.Rows.Count > 0)
                {
                    //m_balcarat = Val.ToDecimal(m_dtOutstanding.Rows[0]["balance_carat"]);
                    //txtBalanceCarat.Text = Val.ToString(m_dtOutstanding.Rows[0]["balance_carat"]);
                    txtBalancePcs.Text = Val.ToString(m_dtOutstanding.Rows[0]["balance_pcs"]);
                    m_kapan_id = Val.ToInt(m_dtOutstanding.Rows[0]["kapan_id"]);
                    m_blnflag = true;
                }
                else
                {
                    BLL.General.ShowErrors("Cut No not Found");
                    txtBalanceCarat.Text = "0";
                    lueCutNo.EditValue = System.DBNull.Value;
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void txtLotId_Validated(object sender, EventArgs e)
        {
            try
            {
                m_IsLot = 1;
                lueKapan.EditValue = null;
                lueCutNo.EditValue = null;
                MFGProcessReceive objProcessRecieve = new MFGProcessReceive();

                if (Val.ToInt64(txtLotId.Text) != 0 && Val.ToInt64(lueKapan.EditValue) == 0 && Val.ToInt64(lueCutNo.EditValue) == 0)
                {
                    //DataTable DTab_Process = objProcessRecieve.Carat_OutStanding_GetData(Val.ToInt64(txtLotId.Text), Val.ToInt32(0), Val.ToInt32(0), 0, "R");
                    m_dtbParam = Global.GetRoughStockWise(Val.ToInt(0), Val.ToInt64(txtLotId.Text));
                    //m_dtbParam = Global.GetRoughKapanWise(Val.ToInt(0), Val.ToInt64(txtLotId.Text));
                    if (m_dtbParam.Rows.Count > 0)
                    {
                        lueKapan.EditValue = Val.ToInt64(m_dtbParam.Rows[0]["kapan_id"]);
                        //m_dtbParam = new DataTable();
                        //if (lueKapan.Text.ToString() != "")
                        //{
                        //    m_dtbParam = Global.GetRoughKapanWise_Data(Val.ToInt(lueKapan.EditValue));
                        //    //if (m_dtbParam.Rows.Count == 0)
                        //    //{
                        //    //    m_dtbParam = DTab_KapanWiseData;
                        //    //}
                        //}

                        int CutId = Val.ToInt(m_dtbParam.Rows[0]["rough_cut_id"]);
                        m_dtbParam = Global.GetRoughKapanWise_Data(Val.ToInt(lueKapan.EditValue));

                        lueCutNo.Properties.DataSource = m_dtbParam;
                        lueCutNo.Properties.ValueMember = "rough_cut_id";
                        lueCutNo.Properties.DisplayMember = "rough_cut_no";
                        lueCutNo.EditValue = Val.ToInt64(CutId);

                        //lueCutNo.Properties.DataSource = m_dtbParam;
                        //lueCutNo.Properties.ValueMember = "rough_cut_id";
                        //lueCutNo.Properties.DisplayMember = "rough_cut_no";
                        //lueCutNo.EditValue = Val.ToInt64(m_dtbParam.Rows[0]["rough_cut_id"]);

                        DataTable DTab_Process = objProcessRecieve.Carat_OutStanding_Janged_GetData(Val.ToInt64(txtLotId.Text), Val.ToInt32(0), Val.ToInt32(0), 0, "R", Val.ToInt64(txtJangedNo.Text));
                        if (DTab_Process.Rows.Count > 0)
                        {
                            lueProcess.Properties.DataSource = DTab_Process;
                            lueProcess.Properties.DisplayMember = "process_name";
                            lueProcess.Properties.ValueMember = "process_id";
                        }
                        if (DTab_Process.Rows.Count == 1)
                        {

                            lueProcess.Text = Val.ToString(DTab_Process.Rows[0]["process_name"]);
                            lueSubProcess.Text = Val.ToString(DTab_Process.Rows[0]["sub_process_name"]);
                            lblOsPcs.Text = Val.ToString(DTab_Process.Rows[0]["pcs"]);
                            lblOsCarat.Text = Val.ToString(DTab_Process.Rows[0]["carat"]);
                            lueProcess.Enabled = false;
                            lueSubProcess.Enabled = false;

                        }
                        else
                        {
                            lueProcess.Enabled = true;
                            lueSubProcess.Enabled = true;
                            lueProcess.EditValue = null;
                            lueSubProcess.EditValue = null;
                            lblOsPcs.Text = Val.ToString("0");
                            lblOsCarat.Text = Val.ToString("0.00");
                        }
                    }
                    else
                    {
                        Global.Message("Lot Not Found");
                        lblOsPcs.Text = Val.ToString("0");
                        lblOsCarat.Text = Val.ToString("0.00");
                        txtLotId.Text = "";
                        m_IsLot = 0;
                        return;
                    }

                }
                m_IsLot = 0;


                //if (lueKapan.Text.ToString() == "" && lueKapan.Text.ToString() == "" && txtLotId.Text.ToString() != "")
                //{
                //    m_IsLot = 1;
                //}
                //else
                //{
                //    m_IsLot = 0;
                //}
                //if (m_dtbParam.Rows.Count > 0)
                //{
                //    if (Val.ToString(txtLotId.Text) != "" && Val.ToInt64(txtLotId.Text) != 0)
                //    {
                //        if (m_dtCut.Select("lot_id ='" + Val.ToString(txtLotId.Text) + "'").Length == 0)
                //        {
                //            Global.Message("Lot Not Found");
                //            return;
                //        }
                //        else
                //        {
                //            m_dtbParam = Global.GetRoughKapanWise(Val.ToInt(0), Val.ToInt64(txtLotId.Text));
                //            if (m_dtbParam.Rows.Count == 0)
                //            {
                //                Global.Message("Lot Not Found");
                //                return;
                //            }
                //        }
                //        m_dtbParam = Global.GetRoughKapanWise(Val.ToInt(0), Val.ToInt64(txtLotId.Text));
                //        lueKapan.Text = Val.ToString(m_dtbParam.Rows[0]["kapan_no"]);
                //        lueCutNo.Properties.DataSource = m_dtbParam;
                //        lueCutNo.Properties.ValueMember = "rough_cut_id";
                //        lueCutNo.Properties.DisplayMember = "rough_cut_no";
                //        lueCutNo.Text = Val.ToString(m_dtbParam.Rows[0]["rough_cut_no"]);
                //        if (txtLotId.Text != string.Empty || Val.ToInt64(txtLotId.Text) != 0)
                //        {
                //            //m_DtProcess = objProcessReceive.GetIssueProcess(Val.ToInt64(txtLotId.Text), "");
                //            //if (m_DtProcess.Rows.Count > 0)
                //            //{
                //            //lueCutNo.Text = Val.ToString(m_DtProcess.Rows[0]["rough_cut_no"]);
                //            m_blnflag = true;
                //            ///txtIssProcess.Text = Val.ToString(m_DtProcess.Rows[0]["process"]);
                //            ////lueCutNo.Text = Val.ToString(m_DtProcess.Rows[0]["rough_cut_no"]);
                //            //GetOsCarat(Val.ToInt64(txtLotId.Text));
                //            GetOsCarat(Val.ToInt64(txtLotId.Text));
                //            DataTable dtIssOS = new DataTable();
                //            objProcessRecieve = new MFGProcessReceive();
                //            dtIssOS = objProcessRecieve.Carat_OutStanding_GetData(Val.ToInt64(txtLotId.Text), Val.ToInt(lueProcess.EditValue), Val.ToInt(lueSubProcess.EditValue), 1, "R");

                //            if (dtIssOS.Rows.Count > 0)
                //            {
                //                txtBalanceCarat.Text = Val.ToString(Val.ToDecimal(dtIssOS.Rows[0]["carat"]));
                //                m_balcarat = Val.ToDecimal(dtIssOS.Rows[0]["carat"]);
                //            }
                //            else if (Val.ToDecimal(m_balcarat) > 0 && dtIssOS.Rows.Count == 0)
                //            {
                //                txtBalanceCarat.Text = Val.ToString(m_balcarat);
                //            }
                //            //}
                //            //else
                //            //{
                //            //    Global.Message("Cut No Data Not found");
                //            //    lueCutNo.EditValue = null;
                //            //    //txtIssProcess.Text = string.Empty;
                //            //    m_blnflag = false;
                //            //    return;
                //            //}
                //            //}
                //            //m_DtProcess = objProcessReceive.GetIssueProcess(Val.ToInt(dr[0]["lot_id"]));
                //            //txtIssProcess.Text = Val.ToString(m_DtProcess.Rows[0]["process"]);

                //        }
                //        else
                //        {
                //            Global.Message("Cut No Data Not found");
                //            lueCutNo.EditValue = null;
                //            //txtIssProcess.Text = string.Empty;
                //            m_blnflag = true;
                //            return;
                //        }
                //    }
                //}
                //else if (m_IsLot == 1 && txtLotId.Text.ToString() != "")
                //{
                //    MFGProcessReceive objProcessRecieve = new MFGProcessReceive();
                //    if (lueKapan.Text.ToString() == "" && lueKapan.Text.ToString() == "" && txtLotId.Text.ToString() != "")
                //    {
                //        m_IsLot = 1;
                //    }
                //    else
                //    {
                //        m_IsLot = 0;
                //    }
                //    m_dtbParam = new DataTable();
                //    if (lueKapan.Text.ToString() == "" && txtLotId.Text.ToString() != "")
                //    {
                //        m_dtbParam = Global.GetRoughKapanWise(Val.ToInt(lueKapan.EditValue), Val.ToInt64(txtLotId.Text));
                //    }
                //    lueKapan.Text = Val.ToString(m_dtbParam.Rows[0]["kapan_no"]);
                //    lueCutNo.Properties.DataSource = m_dtbParam;
                //    lueCutNo.Properties.ValueMember = "rough_cut_id";
                //    lueCutNo.Properties.DisplayMember = "rough_cut_no";
                //    lueCutNo.Text = Val.ToString(m_dtbParam.Rows[0]["rough_cut_no"]);
                //    if (txtLotId.Text != string.Empty || Val.ToInt64(txtLotId.Text) != 0)
                //    {
                //        m_blnflag = true;
                //        GetOsCarat(Val.ToInt64(txtLotId.Text));
                //        DataTable DTab_Process = objProcessRecieve.Carat_OutStanding_GetData(Val.ToInt64(txtLotId.Text), Val.ToInt32(0), Val.ToInt32(0), 0, "R");

                //        if (DTab_Process.Rows.Count > 0)
                //        {
                //            lueProcess.Properties.DataSource = DTab_Process;
                //            lueProcess.Properties.DisplayMember = "process_name";
                //            lueProcess.Properties.ValueMember = "process_id";
                //        }

                //        if (lueProcess.EditValue != System.DBNull.Value && lueSubProcess.EditValue != System.DBNull.Value && Val.ToInt64(txtLotId.Text) != 0)
                //        {
                //            DataTable dtIss = new DataTable();
                //            dtIss = objProcessRecieve.GetIssueID(Val.ToInt64(txtLotId.Text), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue));
                //            if (dtIss.Rows.Count > 0)
                //            {
                //                Global.Message("Lot is already issue in this process.");
                //            }

                //        }
                //    }
                //    m_IsLot = 1;
                //}
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
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
                            dgvProcessReceiveJanged.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvProcessReceiveJanged.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvProcessReceiveJanged.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvProcessReceiveJanged.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvProcessReceiveJanged.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvProcessReceiveJanged.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvProcessReceiveJanged.ExportToCsv(Filepath);
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

        private void btnSearchData_Click(object sender, EventArgs e)
        {
            FrmMFGSearchProcess FrmSearchProcess = new FrmMFGSearchProcess();
            FrmSearchProcess.FrmMFGProcessReceiveJanged = this;
            //FrmSearchProcess.DTab = DtPending;
            FrmSearchProcess.ShowForm(this);
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

        private void txtLotId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
