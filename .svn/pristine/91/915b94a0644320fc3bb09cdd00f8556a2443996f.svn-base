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
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DERP.Transaction.MFG
{
    public partial class FRMMFGEmployeeWagesEntry : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        FormEvents objBOFormEvents = new FormEvents();
        Validation Val = new Validation();
        BLL.FormPer ObjPer = new BLL.FormPer();
        DataTable m_dtbDetail = new DataTable();
        DataTable m_dtbParam = new DataTable();
        DataTable m_dtbSubProcess = new DataTable();
        BLL.BeginTranConnection Conn;
        DataTable m_dtbLotSplitProcess = new DataTable();
        Control _NextEnteredControl = new Control();
        DataTable DtControlSettings = new DataTable();
        MFGLotSplit objLotSplitReceive = new MFGLotSplit();
        DataTable m_dtOutstanding = new DataTable();
        DataTable m_dtbKapan = new DataTable();
        DataTable dtOsRate = new DataTable();
        //DataTable DTab_KapanWiseData = new DataTable();
        MFGProcessReceive objProcessReceive = new MFGProcessReceive();
        private List<Control> _tabControls = new List<Control>();
        bool m_blnadd = new bool();
        bool m_blnsave = new bool();
        int m_Srno = 1;
        int m_update_srno = 1;
        int m_kapan_id = 0;
        int m_IsLot = 0;
        decimal m_numcarat = 0;
        string m_cut_no = "";
        decimal m_old_carat = 0;
        decimal m_numSummRate = 0;
        decimal m_balcarat = 0;
        decimal m_old_rrcarat = 0;
        decimal m_os_rate;
        int m_issue_id;
        int m_manager_id;
        int m_emp_id;
        string m_process = "";
        decimal m_OsCarat;
        int m_flag = 0;
        int m_numForm_id = 0;
        Int64 IntRes;
        Int64 Receive_IntRes;
        Int64 Issue_IntRes;
        Int64 MixSplit_IntRes;

        #endregion

        #region Constructor
        public FRMMFGEmployeeWagesEntry()
        {
            InitializeComponent();
            m_os_rate = 0;
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

        #region Validation
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();

            try
            {
                if (m_blnsave)
                {
                    if (m_dtbLotSplitProcess.Rows.Count == 0)
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


                    //if (chkIsSplit.Checked == false)
                    //{
                    DataView view = new DataView(m_dtbLotSplitProcess);
                    DataTable distinctValues = new DataTable();
                    distinctValues = view.ToTable(true, "lot_id");

                    foreach (DataRow Dr in distinctValues.Rows)
                    {
                        //DataTable DTab = m_dtbLotSplitProcess.Select("sum(carat),sum(plus_carat),sum(loss_carat),max(os_carat) WHERE lot_id =" + Dr["lot_id"]).CopyToDataTable();

                        Decimal carat = Val.ToDecimal(m_dtbLotSplitProcess.Compute("SUM(carat)", "lot_id =" + Dr["lot_id"]));
                        //Decimal plus_carat = Val.ToDecimal(m_dtbLotSplitProcess.Compute("SUM(plus_carat)", "lot_id =" + Dr["lot_id"]));
                        //Decimal loss_carat = Val.ToDecimal(m_dtbLotSplitProcess.Compute("SUM(loss_carat)", "lot_id =" + Dr["lot_id"]));
                        Decimal os_carat = Val.ToDecimal(m_dtbLotSplitProcess.Compute("MAX(os_carat)", "lot_id =" + Dr["lot_id"]));


                        if ((Val.ToDecimal(os_carat)) != (Val.ToDecimal(carat)))
                        {
                            lstError.Add(new ListError(5, "Issue carat not equal to balance carat in this Lot = " + Dr["lot_id"]));
                            if (!blnFocus)
                            {
                                blnFocus = true;
                                txtCarat.Focus();
                            }
                        }
                    }
                    //}                   
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
                    if (lueRoughSieve.Text == "")
                    {
                        lstError.Add(new ListError(13, "Sieve"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueRoughSieve.Focus();
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
                        //if ((m_balcarat + (Val.ToDecimal(txtWeightPlus.Text) + Val.ToDecimal(dgvLotSplit.Columns["plus_carat"].SummaryText) - m_oldpluscarat)) < (Val.ToDecimal(txtCarat.Text) + (Val.ToDecimal(dgvLotSplit.Columns["carat"].SummaryText) - Val.ToDecimal(m_old_carat)) + (Val.ToDecimal(dgvLotSplit.Columns["rr_carat"].SummaryText) - Val.ToDecimal(m_old_rrcarat)) + ((Val.ToDecimal(txtWeightLoss.Text) + Val.ToDecimal(dgvLotSplit.Columns["loss_carat"].SummaryText)) - m_oldlosscarat) + ((Val.ToDecimal(txtWeightPlus.Text) + Val.ToDecimal(dgvLotSplit.Columns["plus_carat"].SummaryText)) - m_oldpluscarat)))
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
                        //if ((m_balcarat + Val.ToDecimal(txtWeightPlus.Text) + Val.ToDecimal(dgvLotSplit.Columns["plus_carat"].SummaryText)) < (Val.ToDecimal(txtCarat.Text) + Val.ToDecimal(txtWeightPlus.Text) + Val.ToDecimal(dgvLotSplit.Columns["plus_carat"].SummaryText) + Val.ToDecimal(dgvLotSplit.Columns["carat"].SummaryText) + Val.ToDecimal(dgvLotSplit.Columns["rr_carat"].SummaryText) + (Val.ToDecimal(txtWeightLoss.Text) + Val.ToDecimal(dgvLotSplit.Columns["loss_carat"].SummaryText))))
                        //{
                        //    lstError.Add(new ListError(5, "Issue carat more than balance carat"));
                        //    if (!blnFocus)
                        //    {
                        //        blnFocus = true;
                        //        txtCarat.Focus();
                        //    }
                        //}
                    }
                    if (Val.ToString(m_process) != "CHARNI" && Val.ToString(m_process) != "SOYEBAL CHIPIYO" && Val.ToString(m_process) != "CHARNI FINAL")
                    {
                        if (lueProcess.Text == "CHARNI")
                        {
                            lstError.Add(new ListError(5, "Lot Not Issue in Charni process"));
                        }
                        else if (lueProcess.Text == "SOYEBAL CHIPIYO")
                        {
                            lstError.Add(new ListError(5, "Lot Not Issue in Soyeble Chipiyo process"));
                        }
                        else if (lueProcess.Text == "CHARNI FINAL")
                        {
                            lstError.Add(new ListError(5, "Lot Not Issue in Soyeble Chipiyo process"));
                        }
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtLotID.Focus();
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
        #endregion

        #region Events
        private void dgvLotSplit_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvEmployeeWages.GetDataRow(e.RowHandle);
                        btnAdd.Text = "&Update";
                        lueCutNo.Text = Val.ToString(Drow["cut_no"]);
                        txtLotID.Text = Val.ToString(Drow["lot_id"]);
                        lueRoughSieve.Text = Val.ToString(Drow["sieve_name"]);
                        txtPcs.Text = Val.ToString(Drow["pcs"]);
                        txtCarat.Text = Val.ToString(Drow["carat"]);
                        txtRate.Text = Val.ToString(Drow["rate"]);
                        m_numcarat = Val.ToDecimal(Drow["carat"]);
                        //m_oldlosscarat = Val.ToDecimal(Drow["loss_carat"]);
                        //m_oldpluscarat = Val.ToDecimal(Drow["plus_carat"]);
                        //txtWeightPlus.Text = Val.ToString(m_oldpluscarat);
                        //txtWeightLoss.Text = Val.ToString(m_oldlosscarat);
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
        private void lueRoughSieve_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmMfgRoughSieve objRoughSieve = new FrmMfgRoughSieve();
                objRoughSieve.ShowDialog();
                Global.LOOKUPRoughSieve(lueRoughSieve);
            }
        }
        private void backgroundWorker_LotSplit_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                MFGLotSplit objMFGLotSplit = new MFGLotSplit();
                MFGLotSplitProperty objMFGLotSplitProperty = new MFGLotSplitProperty();
                Conn = new BeginTranConnection(true, false);

                try
                {
                    IntRes = 0;
                    Receive_IntRes = 0;
                    Issue_IntRes = 0;
                    MixSplit_IntRes = 0;
                    Int64 NewHistory_Union_Id = 0;
                    Int64 Lot_SrNo = 0;
                    //DataTable dtIssueDet = objMFGLotSplit.ProcessIssue_GetData(Val.ToInt64(txtLotID.Text), "CHARNI");
                    //objMFGLotSplitProperty.manager_id = Val.ToInt(dtIssueDet.Rows[0]["manager_id"]);
                    //objMFGLotSplitProperty.employee_id = Val.ToInt(dtIssueDet.Rows[0]["employee_id"]);
                    //objMFGLotSplitProperty.process_id = Val.ToInt(lueProcess.EditValue);// Val.ToInt(dtIssueDet.Rows[0]["process_id"]);
                    //objMFGLotSplitProperty.sub_process_id = Val.ToInt(lueSubProcess.EditValue);// Val.ToInt(dtIssueDet.Rows[0]["sub_process_id"]);
                    //objMFGLotSplitProperty.issue_id = Val.ToInt(dtIssueDet.Rows[0]["issue_id"]);
                    //objMFGLotSplitProperty.kapan_id = Val.ToInt(lueKapan.EditValue);
                    //objMFGLotSplitProperty.rough_cut_id = Val.ToInt(lueCutNo.EditValue);
                    //objMFGLotSplitProperty.is_split = Val.ToBooleanToInt(chkIsSplit.Checked);

                    //if (Val.ToInt(dtIssueDet.Rows[0]["issue_id"]) == 0)
                    //{
                    //    Global.Message("Issue data not found in this Cut No: " + Val.ToString(lueCutNo.Text));
                    //    return;
                    //}
                    // int count = m_dtbLotSplitProcess.Rows.Count;
                    decimal TotCarat = 0;
                    int New_Count = 1;
                    DataTable DTab = new DataTable();
                    foreach (DataRow drw in m_dtbLotSplitProcess.Rows)
                    {
                        DataTable dtIssueDet = new DataTable();
                        if (lueProcess.Text == "CHARNI")
                        {
                            dtIssueDet = objMFGLotSplit.ProcessIssue_GetData(Val.ToInt(drw["lot_id"]), "CHARNI", DLL.GlobalDec.EnumTran.Continue, Conn);
                        }
                        else if (lueProcess.Text == "SOYEBAL CHIPIYO")
                        {
                            dtIssueDet = objMFGLotSplit.ProcessIssue_GetData(Val.ToInt(drw["lot_id"]), "SOYEBAL CHIPIYO", DLL.GlobalDec.EnumTran.Continue, Conn);
                        }
                        else if (lueProcess.Text == "CHARNI FINAL")
                        {
                            dtIssueDet = objMFGLotSplit.ProcessIssue_GetData(Val.ToInt(drw["lot_id"]), "CHARNI FINAL", DLL.GlobalDec.EnumTran.Continue, Conn);
                        }

                        objMFGLotSplitProperty.manager_id = Val.ToInt(dtIssueDet.Rows[0]["manager_id"]);
                        objMFGLotSplitProperty.employee_id = Val.ToInt(dtIssueDet.Rows[0]["employee_id"]);
                        objMFGLotSplitProperty.process_id = Val.ToInt(lueProcess.EditValue);// Val.ToInt(dtIssueDet.Rows[0]["process_id"]);
                        objMFGLotSplitProperty.sub_process_id = Val.ToInt(lueSubProcess.EditValue);// Val.ToInt(dtIssueDet.Rows[0]["sub_process_id"]);
                        objMFGLotSplitProperty.issue_id = Val.ToInt(dtIssueDet.Rows[0]["issue_id"]);
                        objMFGLotSplitProperty.kapan_id = Val.ToInt(drw["kapan_id"]);
                        //objMFGLotSplitProperty.rough_cut_id = Val.ToInt(drw["rough_cut_id"]);
                        //objMFGLotSplitProperty.is_split = Val.ToBooleanToInt(chkIsSplit.Checked);

                        if (Val.ToInt(dtIssueDet.Rows[0]["issue_id"]) == 0)
                        {
                            Global.Message("Issue data not found in this Cut No: " + Val.ToString(lueCutNo.Text));
                            return;
                        }

                        DTab = m_dtbLotSplitProcess.Select("lot_id =" + Val.ToInt(drw["lot_id"])).CopyToDataTable();
                        int count = DTab.Rows.Count;

                        //TotCarat = Val.ToDecimal(DTab.Rows[0]["carat"]);

                        if (count == New_Count)
                        {
                            foreach (DataRow DRow in DTab.Rows)
                            {
                                TotCarat += Val.ToDecimal(DRow["carat"]);
                            }
                            objMFGLotSplitProperty.flag = 1;
                            objMFGLotSplitProperty.recieve_carat = Val.ToDecimal(drw["os_carat"]) - (TotCarat);
                            //objMFGLotSplitProperty.total_loss_carat = Val.ToDecimal(dgvEmployeeWages.Columns["loss_carat"].SummaryText);
                            //objMFGLotSplitProperty.total_plus_carat = Val.ToDecimal(dgvEmployeeWages.Columns["plus_carat"].SummaryText);
                            New_Count = 0;
                            TotCarat = 0;
                        }
                        else
                        {
                            objMFGLotSplitProperty.flag = 0;
                        }

                        objMFGLotSplitProperty.receive_date = Val.DBDate(dtpReceiveDate.Text);
                        objMFGLotSplitProperty.balance_pcs = Val.ToInt(drw["os_pcs"]);//Val.ToInt(lblOsPcs.Text);
                        objMFGLotSplitProperty.balance_carat = Val.ToDecimal(drw["os_carat"]);  //Val.ToDecimal(lblOsCarat.Text);
                        objMFGLotSplitProperty.rough_lot_id = Val.ToInt(drw["lot_id"]);//Val.ToInt64(txtLotID.Text);
                        objMFGLotSplitProperty.rough_cut_id = Val.ToInt(drw["rough_cut_id"]); //Val.ToInt(lueCutNo.EditValue);
                        objMFGLotSplitProperty.rough_sieve_id = Val.ToInt64(drw["rough_sieve_id"]);
                        objMFGLotSplitProperty.form_id = Val.ToInt(m_numForm_id);

                        objMFGLotSplitProperty.pcs = Val.ToInt(drw["pcs"]);
                        objMFGLotSplitProperty.carat = Val.ToDecimal(drw["carat"]);
                        //objMFGLotSplitProperty.loss_carat = Val.ToDecimal(drw["loss_carat"]);
                        //objMFGLotSplitProperty.plus_carat = Val.ToDecimal(drw["plus_carat"]);
                        objMFGLotSplitProperty.rate = Val.ToDecimal(drw["rate"]);
                        objMFGLotSplitProperty.amount = Val.ToDecimal(drw["amount"]);
                        objMFGLotSplitProperty.union_id = IntRes;
                        objMFGLotSplitProperty.receive_union_id = Receive_IntRes;
                        objMFGLotSplitProperty.issue_union_id = Issue_IntRes;
                        objMFGLotSplitProperty.mix_union_id = MixSplit_IntRes;
                        objMFGLotSplitProperty.history_union_id = NewHistory_Union_Id;
                        objMFGLotSplitProperty.lot_srno = Lot_SrNo;

                        objMFGLotSplitProperty = objMFGLotSplit.Save(objMFGLotSplitProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                        IntRes = objMFGLotSplitProperty.union_id;
                        Receive_IntRes = objMFGLotSplitProperty.receive_union_id;
                        Issue_IntRes = objMFGLotSplitProperty.issue_union_id;
                        MixSplit_IntRes = objMFGLotSplitProperty.mix_union_id;
                        NewHistory_Union_Id = Val.ToInt64(objMFGLotSplitProperty.history_union_id);
                        Lot_SrNo = Val.ToInt64(objMFGLotSplitProperty.lot_srno);
                        //count--;
                        New_Count = New_Count + 1;
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
        private void backgroundWorker_LotSplit_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (IntRes > 0)
                {
                    Global.Confirm("Lot Split Recieve Data Save Succesfully");
                    btnSave.Enabled = true;
                    ClearDetails();
                }
                else
                {
                    Global.Confirm("Error In Lot Split Recieve");
                    btnSave.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
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
                    dtIss = objProcessReceive.GetIssueID(Val.ToInt64(txtLotID.Text), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue));
                    if (dtIss.Rows.Count > 0)
                    {
                        m_manager_id = Val.ToInt(dtIss.Rows[0]["manager_id"]);
                        m_emp_id = Val.ToInt(dtIss.Rows[0]["employee_id"]);
                        m_dtOutstanding = objProcessReceive.Carat_OutStanding_GetData(Val.ToInt64(txtLotID.Text), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue), 1, "R");
                        if (m_dtOutstanding.Rows.Count > 0)
                        {
                            m_OsCarat = Val.ToInt32(m_dtOutstanding.Rows[0]["carat"]);
                            //txtBalanceCarat.Text = Val.ToString(m_OsCarat);
                            lblOsCarat.Text = Val.ToString(m_OsCarat);
                            m_balcarat = Val.ToDecimal(dtIssOS.Rows[0]["carat"]);
                            txtCarat.Text = Val.ToDecimal(dtIssOS.Rows[0]["carat"]).ToString();
                        }
                        else
                        {
                            //txtBalanceCarat.Text = "0";
                            lblOsCarat.Text = "0";
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

        private void FrmMFGLotSplit_Load(object sender, EventArgs e)
        {
            try
            {
                Global.LOOKUPEmp(lueEmployee);
                Global.LOOKUPRoughSieve(lueRoughSieve);
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
                lueCutNo.Focus();
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }

        private void lueSubProcess_EditValueChanged(object sender, EventArgs e)
        {
            if (lueProcess.EditValue != System.DBNull.Value && lueSubProcess.EditValue != System.DBNull.Value && Val.ToInt64(txtLotID.Text) != 0)
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
                        //txtBalanceCarat.Text = Val.ToString(m_dtOutstanding.Rows[0]["carat"]);
                        m_OsCarat = Val.ToDecimal(m_dtOutstanding.Rows[0]["carat"]);
                        m_balcarat = Val.ToDecimal(m_dtOutstanding.Rows[0]["carat"]);
                        txtCarat.Text = Val.ToDecimal(m_dtOutstanding.Rows[0]["carat"]).ToString();
                    }
                    else
                    {
                        //txtBalanceCarat.Text = "0";
                        m_balcarat = 0;
                        m_OsCarat = 0;
                        txtCarat.Text = "0";
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
                }
                else
                {
                    Global.Message("Lot not issue in this process.");
                }
            }
        }

        private void lueCutNo_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
            MFGProcessReceive objProcessRecieve = new MFGProcessReceive();
            if (txtLotID.Text != string.Empty || Val.ToInt64(txtLotID.Text) != 0)
            {
                GetOsCarat(Val.ToInt64(txtLotID.Text));
                dtIssOS = objProcessRecieve.Carat_OutStanding_GetData(Val.ToInt64(txtLotID.Text), Val.ToInt(0), Val.ToInt(0), 1, "R");
                if (dtIssOS.Rows.Count > 0)
                {
                    DataRow[] drProcess = dtIssOS.Select("process_name in('" + Val.ToString("CHARNI") + "', '" + Val.ToString("SOYEBAL CHIPIYO") + "', '" + Val.ToString("CHARNI FINAL") + "')");
                    if (drProcess.Length > 0)
                    {
                        lueProcess.EditValue = Val.ToInt(drProcess[0]["process_id"]);
                        lueSubProcess.Text = Val.ToString(drProcess[0]["sub_process_name"]);
                        lblOsPcs.Text = Val.ToString(Val.ToDecimal(drProcess[0]["pcs"]));
                        lblOsCarat.Text = Val.ToString(Val.ToDecimal(drProcess[0]["carat"]));
                        m_process = Val.ToString(drProcess[0]["process_name"]);
                        lueProcess.Enabled = false;
                        lueSubProcess.Enabled = false;
                    }
                    else
                    {
                        if (lueProcess.Text == "CHARNI")
                        {
                            Global.Message("Lot not issue in Charni Process");
                        }
                        else
                        {
                            Global.Message("Lot not issue in Soyable Chipiyo Process");
                        }
                        lueProcess.Enabled = true;
                        lueSubProcess.Enabled = true;
                        lblOsPcs.Text = "0";
                        lblOsCarat.Text = "0.00";
                        return;
                    }
                }
                else
                {
                    Global.Message("Lot Not Issue");
                    lueProcess.EditValue = null;
                    lueSubProcess.EditValue = null;
                    lueProcess.Enabled = true;
                    lueSubProcess.Enabled = true;
                    lblOsPcs.Text = "0";
                    lblOsCarat.Text = "0.00";
                    m_IsLot = 0;
                    return;
                }
            }
        }

        private void RepDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (Global.Confirm("Are you sure delete selected row?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                dgvEmployeeWages.DeleteRow(dgvEmployeeWages.GetRowHandle(dgvEmployeeWages.FocusedRowHandle));
                m_dtbLotSplitProcess.AcceptChanges();
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
                backgroundWorker_LotSplit.RunWorkerAsync();

                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearDetails();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            Global.Export("xlsx", dgvEmployeeWages);
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (AddInGrid())
                {
                    //lueRoughSieve.EditValue = System.DBNull.Value;
                    txtPcs.Text = string.Empty;
                    txtCarat.Text = string.Empty;
                    txtAmount.Text = string.Empty;
                    //txtRate.Text = "0";

                    lueRoughSieve.Focus();
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
            }
        }
        DataTable dtIssOS = new DataTable();
        private void lueCutNo_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (m_IsLot == 0)
                {
                    if (lueCutNo.EditValue != System.DBNull.Value)
                    {
                        if (m_dtbParam.Rows.Count > 0)
                        {
                            DataRow[] dr = m_dtbParam.Select("rough_cut_no ='" + Val.ToString(lueCutNo.Text) + "'");
                            txtLotID.Text = Val.ToString(dr[0]["lot_id"]);
                        }
                        GetOsCarat(Val.ToInt64(txtLotID.Text));
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
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
        private void txtLotID_Validated(object sender, EventArgs e)
        {
            try
            {
                MFGProcessReceive objProcessRecieve = new MFGProcessReceive();

                if (Val.ToInt64(txtLotID.Text) != 0 && Val.ToInt64(lueKapan.EditValue) != 0 && Val.ToInt64(lueCutNo.EditValue) != 0)
                {
                    m_IsLot = 1;
                    lueKapan.EditValue = null;
                    lueCutNo.EditValue = null;
                    if (Val.ToInt64(txtLotID.Text) != 0)
                    {
                        m_dtbParam = Global.GetRoughStockWise(Val.ToInt(0), Val.ToInt64(txtLotID.Text));
                        if (m_dtbParam.Rows.Count > 0)
                        {
                            Int64 Cut_id = Val.ToInt64(m_dtbParam.Rows[0]["rough_cut_id"]);
                            lueKapan.EditValue = Val.ToInt64(m_dtbParam.Rows[0]["kapan_id"]);
                            //m_dtbParam = Global.GetRoughKapanWise(Val.ToInt(lueKapan.EditValue));
                            m_dtbParam = Global.GetRoughKapanWise_Data(Val.ToInt(lueKapan.EditValue));
                            //if (m_dtbParam.Rows.Count == 0)
                            //{
                            //    m_dtbParam = DTab_KapanWiseData;
                            //}
                            lueCutNo.Properties.DataSource = m_dtbParam;
                            lueCutNo.Properties.ValueMember = "rough_cut_id";
                            lueCutNo.Properties.DisplayMember = "rough_cut_no";
                            lueCutNo.EditValue = Val.ToInt64(Cut_id);

                            DataTable DTab_Process = objProcessRecieve.Carat_OutStanding_GetData(Val.ToInt64(txtLotID.Text), Val.ToInt32(0), Val.ToInt32(0), 0, "R");

                            if (DTab_Process.Rows.Count > 0)
                            {
                                lueProcess.Properties.DataSource = DTab_Process;
                                lueProcess.Properties.DisplayMember = "process_name";
                                lueProcess.Properties.ValueMember = "process_id";

                                DataRow[] drProcess = DTab_Process.Select("process_name in('" + Val.ToString("CHARNI") + "', '" + Val.ToString("SOYEBAL CHIPIYO") + "', '" + Val.ToString("CHARNI FINAL") + "')");
                                if (drProcess.Length > 0 && Val.ToDecimal(drProcess[0]["carat"]) > 0)
                                {
                                    lueProcess.EditValue = Val.ToInt(drProcess[0]["process_id"]);
                                    lueSubProcess.Text = Val.ToString(drProcess[0]["sub_process_name"]);
                                    lblOsPcs.Text = Val.ToString(Val.ToDecimal(drProcess[0]["pcs"]));
                                    lblOsCarat.Text = Val.ToString(Val.ToDecimal(drProcess[0]["carat"]));
                                    m_process = Val.ToString(drProcess[0]["process_name"]);
                                    lueProcess.Enabled = false;
                                    lueSubProcess.Enabled = false;
                                }
                                else
                                {
                                    if (lueProcess.Text == "CHARNI")
                                    {
                                        Global.Message("Lot not issue in Charni Process");
                                    }
                                    else
                                    {
                                        Global.Message("Lot not issue in Soyable Chipiyo Process");
                                    }

                                    lueProcess.Enabled = true;
                                    lueSubProcess.Enabled = true;
                                    lueProcess.EditValue = null;
                                    lueSubProcess.EditValue = null;
                                    lblOsPcs.Text = "0";
                                    lblOsCarat.Text = "0.00";
                                    return;
                                }
                            }
                            else
                            {
                                lueProcess.Enabled = true;
                                lueSubProcess.Enabled = true;
                                lueProcess.EditValue = null;
                                lueSubProcess.EditValue = null;
                                lblOsPcs.Text = Val.ToString("0");
                                lblOsCarat.Text = Val.ToString("0.00");
                                txtLotID.Focus();
                            }
                            m_IsLot = 0;
                        }
                        else
                        {
                            Global.Message("Lot Not Found");
                            lblOsPcs.Text = Val.ToString("0");
                            lblOsCarat.Text = Val.ToString("0.00");
                            txtLotID.Text = "";
                            m_IsLot = 0;
                            return;
                        }
                    }
                    else
                    {
                        m_IsLot = 0;
                        return;
                    }
                }
                else if (Val.ToInt64(txtLotID.Text) != 0 && Val.ToInt64(lueKapan.EditValue) == 0 && Val.ToInt64(lueCutNo.EditValue) == 0)
                {
                    m_IsLot = 1;
                    lueKapan.EditValue = null;
                    lueCutNo.EditValue = null;
                    if (Val.ToInt64(txtLotID.Text) != 0)
                    {
                        m_dtbParam = Global.GetRoughStockWise(Val.ToInt(0), Val.ToInt64(txtLotID.Text));
                        if (m_dtbParam.Rows.Count > 0)
                        {
                            lueKapan.EditValue = Val.ToInt64(m_dtbParam.Rows[0]["kapan_id"]);

                            lueCutNo.Properties.DataSource = m_dtbParam;
                            lueCutNo.Properties.ValueMember = "rough_cut_id";
                            lueCutNo.Properties.DisplayMember = "rough_cut_no";
                            lueCutNo.EditValue = Val.ToInt64(m_dtbParam.Rows[0]["rough_cut_id"]);

                            DataTable DTab_Process = objProcessRecieve.Carat_OutStanding_GetData(Val.ToInt64(txtLotID.Text), Val.ToInt32(0), Val.ToInt32(0), 0, "R");

                            if (DTab_Process.Rows.Count > 0)
                            {
                                lueProcess.Properties.DataSource = DTab_Process;
                                lueProcess.Properties.DisplayMember = "process_name";
                                lueProcess.Properties.ValueMember = "process_id";

                                DataRow[] drProcess = DTab_Process.Select("process_name in('" + Val.ToString("CHARNI") + "', '" + Val.ToString("SOYEBAL CHIPIYO") + "', '" + Val.ToString("CHARNI FINAL") + "')");
                                if (drProcess.Length > 0 && Val.ToDecimal(drProcess[0]["carat"]) > 0)
                                {
                                    lueProcess.EditValue = Val.ToInt(drProcess[0]["process_id"]);
                                    lueSubProcess.Text = Val.ToString(drProcess[0]["sub_process_name"]);
                                    lblOsPcs.Text = Val.ToString(Val.ToDecimal(drProcess[0]["pcs"]));
                                    lblOsCarat.Text = Val.ToString(Val.ToDecimal(drProcess[0]["carat"]));
                                    m_process = Val.ToString(drProcess[0]["process_name"]);
                                    lueProcess.Enabled = false;
                                    lueSubProcess.Enabled = false;
                                }
                                else
                                {
                                    if (lueProcess.Text == "CHARNI")
                                    {
                                        Global.Message("Lot not issue in Charni Process");
                                    }
                                    else
                                    {
                                        Global.Message("Lot not issue in Soyable Chipiyo Process");
                                    }
                                    lueProcess.Enabled = true;
                                    lueSubProcess.Enabled = true;
                                    lueProcess.EditValue = null;
                                    lueSubProcess.EditValue = null;
                                    lblOsPcs.Text = "0";
                                    lblOsCarat.Text = "0.00";
                                    return;
                                }
                            }
                            else
                            {
                                lueProcess.Enabled = true;
                                lueSubProcess.Enabled = true;
                                lueProcess.EditValue = null;
                                lueSubProcess.EditValue = null;
                                lblOsPcs.Text = Val.ToString("0");
                                lblOsCarat.Text = Val.ToString("0.00");
                                txtLotID.Focus();
                            }
                            m_IsLot = 0;
                        }
                        else
                        {
                            Global.Message("Lot Not Found");
                            lblOsPcs.Text = Val.ToString("0");
                            lblOsCarat.Text = Val.ToString("0.00");
                            txtLotID.Text = "";
                            m_IsLot = 0;
                            return;
                        }
                    }
                    else
                    {
                        m_IsLot = 0;
                        return;
                    }
                }
                else if (Val.ToInt64(txtLotID.Text) == 0 && Val.ToInt64(lueKapan.EditValue) != 0 && Val.ToInt64(lueCutNo.EditValue) != 0)
                {
                    lueProcess.EditValue = null;
                    lueSubProcess.EditValue = null;
                    lueKapan.EditValue = null;
                    lueCutNo.EditValue = null;
                    lblOsPcs.Text = "0";
                    lblOsCarat.Text = "0.00";
                    m_IsLot = 0;
                    txtLotID.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void txtCarat_EditValueChanged(object sender, EventArgs e)
        {
            txtAmount.Text = Val.ToString(Math.Round((Val.ToDecimal(txtCarat.Text)) * Val.ToDecimal(txtRate.Text), 0));
        }

        private void txtRate_EditValueChanged(object sender, EventArgs e)
        {
            txtAmount.Text = Val.ToString(Math.Round((Val.ToDecimal(txtCarat.Text)) * Val.ToDecimal(txtRate.Text), 0));
        }
        private void txtWeightPlus_EditValueChanged(object sender, EventArgs e)
        {
            txtAmount.Text = Val.ToString(Math.Round((Val.ToDecimal(txtCarat.Text)) * Val.ToDecimal(txtRate.Text), 0));
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

        #region Function

        private bool GenerateLotSplitDetails()
        {
            bool blnReturn = true;
            try
            {
                if (m_dtbLotSplitProcess.Rows.Count > 0)
                    m_dtbLotSplitProcess.Rows.Clear();

                m_dtbLotSplitProcess = new DataTable();

                m_dtbLotSplitProcess.Columns.Add("recieve_id", typeof(int));
                m_dtbLotSplitProcess.Columns.Add("recieve_date", typeof(DateTime));
                m_dtbLotSplitProcess.Columns.Add("lot_id", typeof(int));
                m_dtbLotSplitProcess.Columns.Add("cut_no", typeof(string));
                m_dtbLotSplitProcess.Columns.Add("sieve_name", typeof(string));
                m_dtbLotSplitProcess.Columns.Add("rough_sieve_id", typeof(int));
                m_dtbLotSplitProcess.Columns.Add("pcs", typeof(int)).DefaultValue = 0;
                m_dtbLotSplitProcess.Columns.Add("carat", typeof(decimal)).DefaultValue = 0;
                //m_dtbLotSplitProcess.Columns.Add("plus_carat", typeof(decimal)).DefaultValue = 0;
                //m_dtbLotSplitProcess.Columns.Add("loss_carat", typeof(decimal)).DefaultValue = 0;
                //m_dtbLotSplitProcess.Columns.Add("rr_pcs", typeof(int)).DefaultValue = 0;
                //m_dtbLotSplitProcess.Columns.Add("rr_carat", typeof(decimal)).DefaultValue = 0;
                m_dtbLotSplitProcess.Columns.Add("sr_no", typeof(decimal)).DefaultValue = 1;
                m_dtbLotSplitProcess.Columns.Add("rate", typeof(decimal)).DefaultValue = 0;
                m_dtbLotSplitProcess.Columns.Add("amount", typeof(decimal)).DefaultValue = 0;

                m_dtbLotSplitProcess.Columns.Add("rough_cut_id", typeof(int));
                m_dtbLotSplitProcess.Columns.Add("kapan_id", typeof(int));
                m_dtbLotSplitProcess.Columns.Add("os_pcs", typeof(decimal));
                m_dtbLotSplitProcess.Columns.Add("os_carat", typeof(decimal));

                grdEmployeeWages.DataSource = m_dtbLotSplitProcess;
                grdEmployeeWages.Refresh();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
        }
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
                    DataRow drwNew = m_dtbLotSplitProcess.NewRow();
                    int numPcs = Val.ToInt(txtPcs.Text);
                    decimal numCarat = Val.ToDecimal(txtCarat.Text);
                    //decimal numLossCarat = Val.ToDecimal(txtWeightLoss.Text);
                    //decimal numPlusCarat = Val.ToDecimal(txtWeightPlus.Text);
                    decimal numRate = Val.ToDecimal(txtRate.Text);
                    decimal numAmount = Val.ToDecimal(txtAmount.Text);

                    drwNew["recieve_id"] = Val.ToInt(0);
                    drwNew["recieve_date"] = Val.DBDate(dtpReceiveDate.Text);
                    drwNew["cut_no"] = Val.ToString(lueCutNo.Text);
                    drwNew["lot_id"] = Val.ToInt64(txtLotID.Text);
                    drwNew["rough_sieve_id"] = Val.ToInt(lueRoughSieve.EditValue);
                    drwNew["sieve_name"] = Val.ToString(lueRoughSieve.Text);
                    drwNew["pcs"] = numPcs;
                    drwNew["carat"] = numCarat;
                    //drwNew["loss_carat"] = numLossCarat;
                    //drwNew["plus_carat"] = numPlusCarat;
                    drwNew["rate"] = numRate;
                    drwNew["amount"] = numAmount;
                    drwNew["sr_no"] = m_Srno;

                    drwNew["rough_cut_id"] = Val.ToString(lueCutNo.EditValue);
                    drwNew["kapan_id"] = Val.ToString(lueKapan.EditValue);
                    drwNew["os_pcs"] = Val.ToDecimal(lblOsPcs.Text);
                    drwNew["os_carat"] = Val.ToDecimal(lblOsCarat.Text);

                    m_dtbLotSplitProcess.Rows.Add(drwNew);
                    m_Srno++;
                }
                else if (btnAdd.Text == "&Update")
                {
                    if (m_dtbLotSplitProcess.Select("sr_no ='" + Val.ToInt(m_update_srno) + "'").Length > 0)
                    {
                        for (int i = 0; i < m_dtbLotSplitProcess.Rows.Count; i++)
                        {
                            if (m_dtbLotSplitProcess.Select("cut_no ='" + m_cut_no + "' AND sr_no = '" + m_update_srno + "'").Length > 0)
                            {
                                if (m_dtbLotSplitProcess.Rows[dgvEmployeeWages.FocusedRowHandle]["cut_no"].ToString() == m_cut_no.ToString())
                                {
                                    m_dtbLotSplitProcess.Rows[dgvEmployeeWages.FocusedRowHandle]["lot_id"] = Val.ToString(txtLotID.Text);
                                    m_dtbLotSplitProcess.Rows[dgvEmployeeWages.FocusedRowHandle]["sieve_name"] = Val.ToString(lueRoughSieve.Text);
                                    m_dtbLotSplitProcess.Rows[dgvEmployeeWages.FocusedRowHandle]["rough_sieve_id"] = Val.ToInt(lueRoughSieve.EditValue);
                                    m_dtbLotSplitProcess.Rows[dgvEmployeeWages.FocusedRowHandle]["pcs"] = Val.ToInt(txtPcs.Text).ToString();
                                    m_dtbLotSplitProcess.Rows[dgvEmployeeWages.FocusedRowHandle]["carat"] = Val.ToDecimal(txtCarat.Text).ToString();
                                    //m_dtbLotSplitProcess.Rows[dgvEmployeeWages.FocusedRowHandle]["loss_carat"] = Val.ToDecimal(txtWeightLoss.Text).ToString();
                                    //m_dtbLotSplitProcess.Rows[dgvEmployeeWages.FocusedRowHandle]["plus_carat"] = Val.ToDecimal(txtWeightPlus.Text).ToString();
                                    m_dtbLotSplitProcess.Rows[dgvEmployeeWages.FocusedRowHandle]["rate"] = Val.ToDecimal(txtRate.Text);
                                    m_dtbLotSplitProcess.Rows[dgvEmployeeWages.FocusedRowHandle]["amount"] = Val.ToDecimal(txtAmount.Text);
                                    m_dtbLotSplitProcess.Rows[dgvEmployeeWages.FocusedRowHandle]["rough_cut_id"] = Val.ToInt32(lueCutNo.EditValue);
                                    m_dtbLotSplitProcess.Rows[dgvEmployeeWages.FocusedRowHandle]["kapan_id"] = Val.ToInt32(lueKapan.EditValue);
                                    m_dtbLotSplitProcess.Rows[dgvEmployeeWages.FocusedRowHandle]["os_pcs"] = Val.ToDecimal(lblOsPcs.Text);
                                    m_dtbLotSplitProcess.Rows[dgvEmployeeWages.FocusedRowHandle]["os_carat"] = Val.ToDecimal(lblOsCarat.Text);
                                    m_flag = 0;
                                    break;
                                }
                            }
                        }
                    }
                    btnAdd.Text = "&Add";
                }
                dgvEmployeeWages.MoveLast();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
        }
        private bool ClearDetails()
        {
            bool blnReturn = true;
            try
            {
                if (!GenerateLotSplitDetails())
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
                lueRoughSieve.EditValue = System.DBNull.Value;
                lueProcess.EditValue = System.DBNull.Value;
                lueSubProcess.EditValue = System.DBNull.Value;
                lueProcess.Enabled = true;
                lueSubProcess.Enabled = true;
                //txtBalanceCarat.Text = string.Empty;
                //txtBalancePcs.Text = string.Empty;
                txtPcs.Text = string.Empty;
                txtCarat.Text = string.Empty;
                txtLotID.Text = string.Empty;
                //chkIsSplit.Checked = false;
                txtRate.Text = "0";
                txtAmount.Text = "0";
                //txtWeightLoss.Text = "0";
                //txtWeightPlus.Text = "0";
                lblOsCarat.Text = "0";
                lblOsPcs.Text = "0";
                m_Srno = 1;
                m_update_srno = 0;
                btnAdd.Text = "&Add";
                lueCutNo.Focus();
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
                    m_dtOutstanding.Rows.Clear();
                    m_dtOutstanding = objLotSplitReceive.GetBalanceCarat(lotId);
                }
                if (m_dtOutstanding.Rows.Count > 0)
                {
                    //txtBalancePcs.Text = Val.ToString(m_dtOutstanding.Rows[0]["balance_pcs"]);
                    txtPcs.Text = Val.ToString(m_dtOutstanding.Rows[0]["balance_pcs"]);
                    lueCutNo.EditValue = Val.ToInt64(m_dtOutstanding.Rows[0]["rough_cut_id"]);
                    m_kapan_id = Val.ToInt(m_dtOutstanding.Rows[0]["kapan_id"]);
                    // txtRate.Text = Val.ToDecimal(m_dtOutstanding.Rows[0]["rate"]).ToString();
                }
                else
                {
                    BLL.General.ShowErrors("Cut No not Found");
                    txtPcs.Text = "0";
                    //txtBalanceCarat.Text = "0";
                    lueCutNo.EditValue = System.DBNull.Value;
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        #endregion

        private void btnSearchData_Click(object sender, EventArgs e)
        {
            //FrmMFGSearchProcess FrmSearchProcess = new FrmMFGSearchProcess();
            //FrmSearchProcess.FrmMFGLotSplit = this;
            ////FrmSearchProcess.DTab = DtPending;
            //FrmSearchProcess.ShowForm(this);
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

        private void txtLotID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
