using BLL;
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
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Transaction.MFG
{
    public partial class FrmMFGBoilIssue_New : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        Control _NextEnteredControl;
        private List<Control> _tabControls;
        
        MFGBoilIssue objBoilIssue;

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
        string m_invoice_no = string.Empty;
        string m_article_name = string.Empty;
        //DataTable DTab_KapanWiseData;       
        Int64 m_numForm_id;
        Int64 IntRes;
        Int64 Issue_IntRes;
        Int64 m_purchase_id;
        Int64 m_purchasedetail_id;
        int m_Srno;
        int m_update_srno;
        int m_issue_id;
        int m_OsPcs = 0;
        decimal m_OsCarat = 0;
        decimal m_old_carat = 0;
        bool m_blnadd;
        bool m_blnsave;

        #endregion

        #region Constructor
        public FrmMFGBoilIssue_New()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            _NextEnteredControl = new Control();
            _tabControls = new List<Control>();

            objBoilIssue = new MFGBoilIssue();

            DtControlSettings = new DataTable();
            m_dtbDetail = new DataTable();
            m_dtbParam = new DataTable();
            m_dtbSubProcess = new DataTable();
            m_dtbIssueProcess = new DataTable();
            m_dtOutstanding = new DataTable();
            m_dtbKapan = new DataTable();
            // DTab_KapanWiseData = new DataTable();           
            m_numForm_id = 0;
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

        private void RepBtnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (Global.Confirm("Are you sure delete selected row?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                grvProcessIssue.DeleteRow(grvProcessIssue.GetRowHandle(grvProcessIssue.FocusedRowHandle));
                m_dtbIssueProcess.AcceptChanges();
            }
        }
        private void FrmMFGBoilIssue_Load(object sender, EventArgs e)
        {
            try
            {
                Global.LOOKUPEmp(lueEmployee);
                Global.LOOKUPManager(lueManager);
                Global.LOOKUPProcess(lueProcess);
                Global.LOOKUPSubProcess(lueSubProcess);
                Global.LOOKUPRoughSieve(lueSieve);

                m_dtbSubProcess = (((DataTable)lueSubProcess.Properties.DataSource).Copy());

                dtpIssueDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpIssueDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpIssueDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpIssueDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpIssueDate.EditValue = DateTime.Now;

                ClearDetails();

                DataTable dtbProcess = (((DataTable)lueProcess.Properties.DataSource).Copy());
                if (dtbProcess.Rows.Count > 0 && dtbProcess.Select("is_default = 'True'").Count() > 0)
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

                DialogResult result = MessageBox.Show("Do you want to save " + Val.ToString(lueProcess.Text) + " issue data?", "Confirmation", MessageBoxButtons.YesNoCancel);
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
                if (lueProcess.EditValue != null && lueSubProcess.EditValue != System.DBNull.Value)
                {
                    DataTable dtIss = new DataTable();
                    dtIss = objBoilIssue.GetBoilIssuePending(Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue));
                    if (dtIss.Rows.Count > 0)
                    {

                        lueInvoiceNo.Properties.DataSource = dtIss;
                        lueInvoiceNo.Properties.ValueMember = "purchase_detail_id";
                        lueInvoiceNo.Properties.DisplayMember = "invoice_no";

                    }
                    else
                    {
                        Global.Message("No Any Invoice Available Pending For Issue.");
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
                    btnPopUpStock.Enabled = true;
                }
                else
                {
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
                //if (GlobalDec.gEmployeeProperty.role_name == "SURAT KAMALA")
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
                //            if (GlobalDec.gEmployeeProperty.user_name == "4POK")
                //            {
                //                lueSubProcess.EditValue = Val.ToInt32(2024);
                //                lueSubProcess.Enabled = false;
                //            }
                //            else
                //            {
                //                lueSubProcess.EditValue = System.DBNull.Value;
                //                lueSubProcess.Enabled = true;
                //            }

                //        }
                //    }
                //}

            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void lueCutNo_EditValueChanged_1(object sender, EventArgs e)
        {

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
        private void backgroundWorker_ProcessIssue_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                MFGBoilIssue objMFGBoilIssue = new MFGBoilIssue();
                MFGBoilIssueProperty objMFGBoilIssueProperty = new MFGBoilIssueProperty();
                Conn = new BeginTranConnection(true, false);

                IntRes = 0;
                Issue_IntRes = 0;
                Int64 NewHistory_Union_Id = 0;
                Int64 Lot_SrNo = 0;

                int IntCounter = 0;
                int Count = 0;

                try
                {
                    //m_dtbIssueProcess = (DataTable)grdProcessIssue.DataSource;

                    int TotalCount = m_dtbIssueProcess.Rows.Count;

                    foreach (DataRow drw in m_dtbIssueProcess.Rows)
                    {
                        objMFGBoilIssueProperty.Issue_id = Val.ToInt(drw["issue_id"]);
                        objMFGBoilIssueProperty.prev_id = Val.ToInt(0);
                        objMFGBoilIssueProperty.rough_purchase_id = Val.ToInt(drw["rough_purchase_id"]);
                        objMFGBoilIssueProperty.purchase_detail_id = Val.ToInt(drw["purchase_detail_id"]);
                        objMFGBoilIssueProperty.issue_date = Val.DBDate(dtpIssueDate.Text);// Val.DBDate(Val.ToString(drw["issue_date"]));

                        objMFGBoilIssueProperty.manager_id = Val.ToInt(lueManager.EditValue);
                        objMFGBoilIssueProperty.employee_id = Val.ToInt(lueEmployee.EditValue);
                        objMFGBoilIssueProperty.process_id = Val.ToInt(lueProcess.EditValue); //Val.ToInt(drw["process_id"]);
                        objMFGBoilIssueProperty.sub_process_id = Val.ToInt(lueSubProcess.EditValue); //Val.ToInt(drw["sub_process_id"]);
                        objMFGBoilIssueProperty.rough_sieve_id = Val.ToInt(lueSieve.EditValue);
                        objMFGBoilIssueProperty.pcs = Val.ToInt(drw["pcs"]);
                        objMFGBoilIssueProperty.carat = Val.ToDecimal(drw["carat"]);
                        objMFGBoilIssueProperty.rate = Val.ToDecimal(drw["rate"]);
                        objMFGBoilIssueProperty.amount = Val.ToDecimal(drw["amount"]);
                        objMFGBoilIssueProperty.form_id = Val.ToInt(m_numForm_id);
                        objMFGBoilIssueProperty.union_id = IntRes;
                        objMFGBoilIssueProperty.issue_union_id = Issue_IntRes;
                        //m_old_carat = Val.ToDecimal(drw["carat"]);
                        objMFGBoilIssueProperty.history_union_id = NewHistory_Union_Id;
                        objMFGBoilIssueProperty.lot_srno = Lot_SrNo;
                        objMFGBoilIssueProperty = objMFGBoilIssue.Save(objMFGBoilIssueProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                        IntRes = objMFGBoilIssueProperty.union_id;
                        Issue_IntRes = objMFGBoilIssueProperty.issue_union_id;
                        Lot_SrNo = Val.ToInt64(objMFGBoilIssueProperty.lot_srno);
                        NewHistory_Union_Id = Val.ToInt64(objMFGBoilIssueProperty.history_union_id);

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
                    DialogResult result = MessageBox.Show("Process Issue Data Save Succesfully and Union ID is : " + Issue_IntRes + " Are you sure print this janged?", "Confirmation", MessageBoxButtons.YesNoCancel);
                    if (result != DialogResult.Yes)
                    {
                        btnSave.Enabled = true;
                        ClearDetails();
                        return;
                    }

                    MFGJangedIssue objMFGJangedIssue = new MFGJangedIssue();
                    DataTable DTab_IssueJanged = objMFGJangedIssue.GetProcessBoil_DataDetails(Val.ToInt64(Issue_IntRes));

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
                    //Global.Confirm(Val.ToString(lueProcess.Text) + " Issue Data Save Succesfully");
                    //ClearDetails();
                    //btnSave.Enabled = true;
                }
                else
                {
                    Global.Confirm("Error In " + Val.ToString(lueProcess.Text) + " Issue");
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
                        lueInvoiceNo.EditValue = Val.ToInt64(Drow["purchase_detail_id"]);
                        txtPcs.Text = Val.ToString(Drow["pcs"]);
                        txtCarat.Text = Val.ToString(Drow["carat"]);
                        m_issue_id = Val.ToInt32(Drow["issue_id"]);
                        m_invoice_no = Val.ToString(Drow["invoice_no"]);
                        m_update_srno = Val.ToInt(Drow["sr_no"]);
                        m_purchasedetail_id = Val.ToInt32(Drow["purchase_detail_id"]);
                        m_purchase_id = Val.ToInt32(Drow["rough_purchase_id"]);
                        m_old_carat = Val.ToDecimal(Drow["carat"]);
                        //m_OsPcs = Val.ToInt(txtReturnPcs.Text) + Val.ToInt(Drow["loss_pcs"]);
                        //lblOsCarat.Text = OS_Carat.ToString();
                        //lblOsPcs.Text = OS_Pcs.ToString();
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
            //FrmMFGSearchProcess FrmSearchProcess = new FrmMFGSearchProcess();
            //FrmSearchProcess.FrmMFGProcessIssue = this;
            ////FrmSearchProcess.DTab = DtPending;
            //FrmSearchProcess.ShowForm(this);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            FrmMFGSearchBoilIssRec_New FrmSearchProcess = new FrmMFGSearchBoilIssRec_New();
            FrmSearchProcess.FrmMFGBoilIssue = this;
            //FrmSearchProcess.DTab = DtPending;
            FrmSearchProcess.ShowForm(this);
        }

        #endregion

        #region Function

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
                        lstError.Add(new ListError(13, "Manager"));
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
                    if (Val.ToDecimal(txtCarat.Text) > Val.ToDecimal(lblOsCarat.Text))
                    {
                        lstError.Add(new ListError(5, "Issue Carat not more than Os Carat."));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtCarat.Focus();
                        }
                    }
                    if (Val.ToDecimal(txtCarat.Text) == 0)
                    {
                        lstError.Add(new ListError(5, "Issue Carat not be zero."));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtCarat.Focus();
                        }
                    }
                    if (Val.ToInt(txtCarat.Text) > 0 && txtCarat.Text != string.Empty)
                    {
                        if (Val.ToDecimal(lblOsCarat.Text) < (Val.ToDecimal(grvProcessIssue.Columns["carat"].SummaryText) + Val.ToDecimal(txtCarat.Text) - m_old_carat))
                        {
                            lstError.Add(new ListError(5, "Entry Carat not greater than total Carat"));
                            if (!blnFocus)
                            {
                                blnFocus = true;
                                txtCarat.Focus();
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

                lueManager.EditValue = System.DBNull.Value;
                lueEmployee.EditValue = System.DBNull.Value;
                lueSieve.EditValue = System.DBNull.Value;
                lueProcess.Text = "BOILING";
                lueSubProcess.Text = "BOILING";
                lueProcess.Enabled = true;
                lueSubProcess.Enabled = true;
                lueEmployee.Enabled = true;
                lueSieve.Enabled = true;
                btnPopUpStock.Enabled = false;
                btnClear.Enabled = true;
                btnExit.Enabled = true;
                //m_flag = 0;
                //m_Srno = 1;
                //m_update_srno = 0;
                //m_numcarat = 0;
                //m_old_carat = 0;
                //m_old_rate = 0;
                //m_old_amount = 0;
                //m_kapan_id = 0;
                //m_IsLot = 0;
                //m_blnflag = false;
                //Lot_SrNo_Print = 0;
                btnSave.Enabled = true;
                txtPassword.Text = "";
                btnDelete.Enabled = true;
                btnDelete.Visible = false;
                lblUnionId.Tag = "0";
                txtPassword.Text = "";
                lblOsCarat.Text = "0";
                lblOsPcs.Text = "0";
                dtpIssueDate.Focus();

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

                m_dtbIssueProcess.Columns.Add("issue_id", typeof(int)).DefaultValue = 0;
                m_dtbIssueProcess.Columns.Add("rough_purchase_id", typeof(int));
                m_dtbIssueProcess.Columns.Add("purchase_detail_id", typeof(int));
                m_dtbIssueProcess.Columns.Add("article_name", typeof(string));
                m_dtbIssueProcess.Columns.Add("rough_sieve_id", typeof(int));
                m_dtbIssueProcess.Columns.Add("issue_date", typeof(DateTime));
                m_dtbIssueProcess.Columns.Add("invoice_no", typeof(string));
                m_dtbIssueProcess.Columns.Add("pcs", typeof(int)).DefaultValue = 0;
                m_dtbIssueProcess.Columns.Add("carat", typeof(decimal)).DefaultValue = 0;
                m_dtbIssueProcess.Columns.Add("rate", typeof(decimal)).DefaultValue = 0;
                m_dtbIssueProcess.Columns.Add("amount", typeof(decimal)).DefaultValue = 0;
                m_dtbIssueProcess.Columns.Add("sr_no", typeof(int)).DefaultValue = 0;
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
                    lstError.Add(new ListError(13, "Manager"));
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
                if (lueSieve.Text == "")
                {
                    lstError.Add(new ListError(13, "Sieve"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueSieve.Focus();
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
                //if (Save_Validate())
                //{
                //    MFGBoilIssue objMFGBoilIssue = new MFGBoilIssue();
                //    MFGBoilIssueProperty objMFGBoilIssueProperty = new MFGBoilIssueProperty();

                //    objMFGBoilIssueProperty.process_id = Val.ToInt64(lueProcess.EditValue);
                //    objMFGBoilIssueProperty.sub_process_id = Val.ToInt64(lueSubProcess.EditValue);

                //    DtPending = objMFGBoilIssue.GetBoilIssuePending(objMFGBoilIssueProperty);

                //    FrmMFGBoilIssuePending_New FrmBoilIssuePending = new FrmMFGBoilIssuePending_New();
                //    FrmBoilIssuePending.FrmMFGBoilIssue = this;
                //    FrmBoilIssuePending.DTab = DtPending;
                //    FrmBoilIssuePending.ShowForm(this);
                //}
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

                m_dtbIssueProcess.AcceptChanges();
                DTabTemp = Stock_Data.Copy();

                if (m_dtbIssueProcess != null)
                {
                    if (m_dtbIssueProcess.Rows.Count > 0)
                    {
                        for (int i = 0; i < m_dtbIssueProcess.Rows.Count; i++)
                        {
                            for (int j = 0; j < DTabTemp.Rows.Count; j++)
                            {
                                if (m_dtbIssueProcess.Rows[i]["rough_purchase_id"].ToString() == DTabTemp.Rows[j]["rough_purchase_id"].ToString())
                                {
                                    Global.Message(m_dtbIssueProcess.Rows[i]["invoice_no"].ToString() + " = Invoice No already added to the Grid list!");
                                    return;
                                }
                            }
                        }
                    }
                }


                if (m_dtbIssueProcess.Rows.Count > 0)
                {
                    DTabTemp = Stock_Data.Copy();
                    m_dtbIssueProcess.Merge(DTabTemp);
                }
                else
                {
                    m_dtbIssueProcess = Stock_Data.Copy();
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
        public void FillGrid(int Lot_SRNo)
        {
            MFGBoilIssue objMFGBoilIssue = new MFGBoilIssue();

            DataTable DTab_DeptBoilIssue = objMFGBoilIssue.GetIssRecDetails(Lot_SRNo);

            btnSave.Enabled = false;

            if (DTab_DeptBoilIssue.Rows.Count > 0)
            {
                dtpIssueDate.Text = Val.DBDate(DTab_DeptBoilIssue.Rows[0]["issue_date"].ToString());
                lblUnionId.Tag = Val.ToInt64(DTab_DeptBoilIssue.Rows[0]["union_id"]).ToString();
                lueProcess.EditValue = Val.ToInt32(DTab_DeptBoilIssue.Rows[0]["process_id"]);
                lueSubProcess.EditValue = Val.ToInt32(DTab_DeptBoilIssue.Rows[0]["sub_process_id"]);
                lueEmployee.EditValue = Val.ToInt64(DTab_DeptBoilIssue.Rows[0]["employee_id"]);
                lueManager.EditValue = Val.ToInt64(DTab_DeptBoilIssue.Rows[0]["manager_id"]);
                lueSieve.EditValue = Val.ToInt32(DTab_DeptBoilIssue.Rows[0]["rough_sieve_id"]);
            }
            else
            {
                lblUnionId.Tag = "0";
            }
            grdProcessIssue.DataSource = DTab_DeptBoilIssue;
            grdProcessIssue.RefreshDataSource();
            grvProcessIssue.BestFitColumns();


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
                    DataRow[] dr;
                    //if (m_dtbIssueProcess.Rows.Count > 0)
                    //{
                    //    if (m_dtbIssueProcess.Rows[0]["invoice_no"] != Val.ToString(lueInvoiceNo.Text) && Val.ToString(lueSubProcess.Text) == "CHARNI")
                    //    {
                    //        Global.Message("Single Invoice Recieve at a Time.");
                    //        lueInvoiceNo.EditValue = null;
                    //        blnReturn = false;
                    //        return blnReturn;
                    //    }
                    //}
                    //if (Val.ToInt32(lueProcess.EditValue) == 1002)
                    //{
                    //    dr = m_dtbIssueProcess.Select("invoice_no = '" + Val.ToString(lueInvoiceNo.Text) + "' and sieve_name = '" + Val.ToString(lueSieve.Text) + "'");
                    //}
                    //else
                    //{
                    //    dr = m_dtbIssueProcess.Select("invoice_no = '" + Val.ToString(lueInvoiceNo.Text) + "'");
                    //}
                    dr = m_dtbIssueProcess.Select("purchase_detail_id = '" + Val.ToString(lueInvoiceNo.EditValue) + "'");
                    if (dr.Count() == 1)
                    {
                        Global.Message("Record already exists in grid", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lueInvoiceNo.EditValue = null;
                        blnReturn = false;
                        return blnReturn;
                    }

                    DataRow drwNew = m_dtbIssueProcess.NewRow();
                    int numPcs = Val.ToInt(txtPcs.Text);
                    decimal numCarat = Val.ToDecimal(txtCarat.Text);
                    decimal numRate = Val.ToDecimal(txtRate.Text);
                    decimal numAmount = Val.ToDecimal(txtAmount.Text);
                    drwNew["issue_id"] = Val.ToInt(0);
                    drwNew["invoice_no"] = Val.ToString(lueInvoiceNo.Text);
                    drwNew["rough_purchase_id"] = Val.ToString(m_purchase_id);
                    drwNew["purchase_detail_id"] = Val.ToString(m_purchasedetail_id);
                    //drwNew["manager_id"] = Val.ToInt(lueManager.EditValue);
                    drwNew["article_name"] = Val.ToString(m_article_name);
                    //drwNew["employee_id"] = Val.ToInt(lueEmployee.EditValue);
                    //drwNew["employee"] = Val.ToString(lueEmployee.Text);
                    drwNew["rough_sieve_id"] = Val.ToInt(lueSieve.EditValue);
                    //drwNew["sieve_name"] = Val.ToString(lueSieve.Text);
                    drwNew["pcs"] = numPcs;
                    drwNew["carat"] = numCarat;
                    drwNew["rate"] = numRate;
                    drwNew["amount"] = numAmount;
                    drwNew["sr_no"] = m_Srno;
                    drwNew["issue_id"] = m_issue_id;
                    m_dtbIssueProcess.Rows.Add(drwNew);
                    m_Srno++;
                }
                else if (btnAdd.Text == "&Update")
                {

                    if (m_dtbIssueProcess.Select("sr_no ='" + Val.ToInt(m_update_srno) + "'").Length > 0)
                    {
                        for (int i = 0; i < m_dtbIssueProcess.Rows.Count; i++)
                        {
                            if (m_dtbIssueProcess.Select("invoice_no ='" + m_invoice_no + "' AND sr_no = '" + m_update_srno + "'").Length > 0)
                            {
                                if (m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["sr_no"].ToString() == m_update_srno.ToString())
                                {
                                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["pcs"] = Val.ToInt(txtPcs.Text).ToString();
                                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["carat"] = Val.ToString(txtCarat.Text).ToString();
                                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["rate"] = Val.ToDecimal(txtRate.Text);
                                    m_dtbIssueProcess.Rows[grvProcessIssue.FocusedRowHandle]["amount"] = Val.ToDecimal(txtAmount.Text);
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
            DataTable DTab_IssueJanged = objMFGJangedIssue.GetProcessBoil_DataDetails(Val.ToInt64(lblUnionId.Tag));

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

        private void btnDelete_Click(object sender, EventArgs e)
        {

            MFGBoilIssueProperty objMFGBoilIssueProperty = new MFGBoilIssueProperty();
            MFGBoilIssue objMFGBoilIssue = new MFGBoilIssue();

            try
            {
                m_dtbIssueProcess = (DataTable)grdProcessIssue.DataSource;
                if (m_dtbIssueProcess.Rows.Count == 0)
                {
                    Global.Message("Please Select Records..");
                    return;
                }
                DialogResult result = MessageBox.Show("Do you want to Delete " + Val.ToString(lueProcess.Text) + " Issue data?", "Confirmation", MessageBoxButtons.YesNo);
                if (result != DialogResult.Yes)
                {
                    btnSave.Enabled = true;
                    return;
                }
                btnDelete.Enabled = false;

                DataTable DTab = (DataTable)grdProcessIssue.DataSource;

                objMFGBoilIssueProperty.union_id = Val.ToInt64(lblUnionId.Tag);
                objMFGBoilIssueProperty.process_id = Val.ToInt64(lueProcess.EditValue);
                objMFGBoilIssueProperty.sub_process_id = Val.ToInt64(lueSubProcess.EditValue);
                objMFGBoilIssueProperty = objMFGBoilIssue.Boil_Delete(objMFGBoilIssueProperty);

                if (objMFGBoilIssueProperty.remarks != "")
                {
                    Global.Message(objMFGBoilIssueProperty.remarks);
                    btnDelete.Enabled = true;
                }
                else
                {
                    Global.Message("Data Deleted Successfully");
                    ClearDetails();
                }
            }
            catch (Exception ex)
            {
                IntRes = -1;
                General.ShowErrors(ex.ToString());
                return;
            }
            finally
            {
                objMFGBoilIssueProperty = null;
            }

        }

        private void txtPassword_EditValueChanged(object sender, EventArgs e)
        {
            if (txtPassword.Text != "")
            {
                if (Val.ToString(txtPassword.Text) == "123")
                {
                    btnDelete.Visible = true;
                }
                else
                {
                    btnDelete.Visible = false;
                }
            }
            else
            {
                btnDelete.Visible = false;
            }
        }

        private void lblUnionId_TextChanged(object sender, EventArgs e)
        {
            if (Val.ToInt64(lblUnionId.Tag) > 0)
            {
                txtPassword.Enabled = true;
            }
            else
            {
                txtPassword.Enabled = false;
            }
        }

        private void lueInvoiceNo_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (lueInvoiceNo.EditValue != null && lueInvoiceNo.EditValue != System.DBNull.Value)
                {
                    DataTable dtIss = new DataTable();
                    string strFilter = string.Empty;
                    dtIss = objBoilIssue.GetBoilIssuePending(Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue));
                    if (Val.ToInt32(lueInvoiceNo.EditValue) > 0)
                        strFilter = "purchase_detail_id = " + Val.ToInt32(lueInvoiceNo.EditValue);

                    dtIss.DefaultView.RowFilter = strFilter;
                    dtIss.DefaultView.ToTable();

                    DataTable dtb = dtIss.DefaultView.ToTable();
                    if (dtb.Rows.Count > 0)
                    {

                        m_purchase_id = Val.ToInt(dtb.Rows[0]["rough_purchase_id"]);
                        m_purchasedetail_id = Val.ToInt(dtb.Rows[0]["purchase_detail_id"]);
                        m_invoice_no = Val.ToString(dtb.Rows[0]["invoice_no"]);
                        m_article_name = Val.ToString(dtb.Rows[0]["article_name"]);
                        DataTable dtbOs = new DataTable();
                        dtbOs = objBoilIssue.GetBoilIssRecOS(Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue), m_purchase_id, m_purchasedetail_id);
                        if(dtbOs.Rows.Count > 0)
                        {
                            txtPcs.Text = Val.ToString(0);
                            txtCarat.Text = Val.ToString(dtbOs.Rows[0]["os_carat"]);
                            m_OsPcs = Val.ToInt(0);
                            m_OsCarat = Val.ToDecimal(dtbOs.Rows[0]["os_carat"]);
                            lblOsCarat.Text = Val.ToString(m_OsCarat);
                            lblOsPcs.Text = Val.ToString(m_OsPcs);
                        }
                        
                        txtRate.Text = Val.ToString(dtb.Rows[0]["rate"]);
                        txtAmount.Text = Val.ToString(m_OsCarat * Val.ToDecimal(dtb.Rows[0]["rate"]));

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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (AddInGrid())
                {
                    //lueManager.EditValue = System.DBNull.Value;
                    //lueEmployee.EditValue = System.DBNull.Value;
                    //lueSieve.EditValue = System.DBNull.Value;
                    lueInvoiceNo.EditValue = System.DBNull.Value;
                    txtPcs.Text = "0";
                    txtCarat.Text = "0";
                    lblOsCarat.Text = "0";
                    lblOsPcs.Text = "0";
                    txtRate.Text = "0";
                    txtAmount.Text = "0";
                    m_article_name = "";
                    //chkWithoutLoss.Checked = false;
                    lueInvoiceNo.Focus();
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
            }
        }

        private void txtCarat_EditValueChanged(object sender, EventArgs e)
        {
            if (Val.ToDecimal(txtCarat.Text) != 0 && Val.ToDecimal(txtRate.Text) != 0 && Val.ToDecimal(lblOsCarat.Text) > 0)
            {               
                decimal amount = Math.Round(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text), 2);
                txtAmount.Text = Val.ToString(amount);
            }
            else
            {
                txtAmount.Text = Val.ToString("0");
            }
        }
    }
}
