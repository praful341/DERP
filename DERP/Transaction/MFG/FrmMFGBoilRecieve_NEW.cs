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
using System.Reflection;
using System.Windows.Forms;

namespace DERP.Transaction.MFG
{
    public partial class FrmMFGBoilRecieve_NEW : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        Control _NextEnteredControl;
        private List<Control> _tabControls;

        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        MFGBoilRecieve objBoilRecieve;

        DataTable DtControlSettings;
        DataTable m_dtbDetail;
        DataTable m_dtbParam;
        DataTable m_dtbSubProcess;
        DataTable m_dtbBoilReceive;
        DataTable m_dtOutstanding;
        DataTable m_dtbDetails;
        DataTable m_dtbKapan;
        DataTable DtPending = new DataTable();
        DataTable DTabTemp = new DataTable();
        DataTable DTab_StockData = new DataTable();
        DataTable dtIss = new DataTable();
        DataTable m_dtbType = new DataTable();

        MFGMixSplit objMFGMixSplit = new MFGMixSplit();
        int m_Srno;
        int m_update_srno;
        int m_issue_id;
        int m_manager_id;
        int m_emp_id;
        int m_OsPcs;
        int m_articleId;
        Int64 m_numForm_id;
        Int64 IntRes;
        Int64 m_invoice_id;
        string m_invoice_no;
        string m_article;

        decimal m_numlosscarat;
        decimal m_old_losscarat;
        decimal m_OsCarat;
        decimal m_returnCarat;
        bool m_blnadd;
        bool m_blnsave;

        #endregion

        #region Constructor
        public FrmMFGBoilRecieve_NEW()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            _NextEnteredControl = new Control();
            _tabControls = new List<Control>();

            objBoilRecieve = new MFGBoilRecieve();

            DtControlSettings = new DataTable();
            m_dtbDetail = new DataTable();
            m_dtbParam = new DataTable();
            m_dtbSubProcess = new DataTable();
            m_dtbBoilReceive = new DataTable();
            m_dtOutstanding = new DataTable();
            m_dtbDetails = new DataTable();
            m_dtbKapan = new DataTable();
            //DTab_KapanWiseData = new DataTable();

            m_Srno = 1;
            m_update_srno = 1;
            m_issue_id = 0;
            m_manager_id = 0;
            m_emp_id = 0;
            m_numForm_id = 0;
            m_invoice_id = 0;
            m_OsPcs = 0;
            m_articleId = 0;
            m_invoice_no = "";
            m_article = "";
            m_numlosscarat = 0;
            m_old_losscarat = 0;
            m_OsCarat = 0;
            m_returnCarat = 0;
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
                else if ((Control)sender is CheckedComboBoxEdit)
                {
                    if (e.KeyChar == Convert.ToChar(Keys.Enter))
                    {
                        SendKeys.Send("{TAB}");
                    }
                }
                else if ((Control)sender is SimpleButton)
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
        private void btnPopUpStock_Click(object sender, EventArgs e)
        {
            //GetPendingStock();
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
        private void backgroundWorker_BoilRecieve_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                MFGBoilRecieve objMFGBoilRecieve = new MFGBoilRecieve();
                MFGBoilIssueProperty objMFGBoilIssueProperty = new MFGBoilIssueProperty();
                Conn = new BeginTranConnection(true, false);

                IntRes = 0;
                Int64 NewHistory_Union_Id = 0;
                Int64 Lot_SrNo = 0;

                int IntCounter = 0;
                int Count = 0;
                int TotalCount = m_dtbBoilReceive.Rows.Count;
                try
                {
                    foreach (DataRow drw in m_dtbBoilReceive.Rows)
                    {
                        objMFGBoilIssueProperty.Issue_id = Val.ToInt(drw["recieve_id"]);
                        objMFGBoilIssueProperty.prev_id = Val.ToInt(drw["boil_issue_id"]);
                        objMFGBoilIssueProperty.rough_purchase_id = Val.ToInt(drw["purchase_id"]);
                        objMFGBoilIssueProperty.purchase_detail_id = Val.ToInt(drw["purchase_detail_id"]);
                        objMFGBoilIssueProperty.issue_date = Val.DBDate(Val.ToString(dtpReceiveDate.EditValue));
                        objMFGBoilIssueProperty.rough_sieve_id = Val.ToInt(drw["rough_sieve_id"]);
                        objMFGBoilIssueProperty.manager_id = Val.ToInt(drw["manager_id"]);
                        objMFGBoilIssueProperty.employee_id = Val.ToInt(drw["employee_id"]);
                        objMFGBoilIssueProperty.process_id = Val.ToInt(lueProcess.EditValue);//Val.ToInt(lueProcess.EditValue); //Val.ToInt(drw["process_id"]);
                        objMFGBoilIssueProperty.sub_process_id = Val.ToInt(lueSubProcess.EditValue);  //Val.ToInt(lueSubProcess.EditValue); //Val.ToInt(drw["sub_process_id"]);
                        objMFGBoilIssueProperty.pcs = Val.ToInt(drw["return_pcs"]);
                        objMFGBoilIssueProperty.carat = Val.ToDecimal(drw["return_carat"]);
                        //objMFGBoilIssueProperty.loss_pcs = Val.ToInt(drw["loss_pcs"]);
                        objMFGBoilIssueProperty.loss_carat = Val.ToDecimal(drw["loss_carat"]);
                        objMFGBoilIssueProperty.rate = Val.ToDecimal(drw["rate"]);
                        objMFGBoilIssueProperty.amount = Val.ToDecimal(drw["amount"]);
                        objMFGBoilIssueProperty.return_per = Val.ToDecimal(drw["return_per"]);
                        objMFGBoilIssueProperty.loss_per = Val.ToDecimal(drw["loss_per"]);
                        objMFGBoilIssueProperty.form_id = m_numForm_id;
                        objMFGBoilIssueProperty.issue_union_id = IntRes;
                        objMFGBoilIssueProperty.union_id = IntRes;
                        objMFGBoilIssueProperty.history_union_id = NewHistory_Union_Id;
                        objMFGBoilIssueProperty.lot_srno = Lot_SrNo;
                        objMFGBoilIssueProperty.rough_type = Val.ToString(drw["type"]);


                        objMFGBoilIssueProperty = objMFGBoilRecieve.Save(objMFGBoilIssueProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                        IntRes = objMFGBoilIssueProperty.issue_union_id;
                        NewHistory_Union_Id = Val.ToInt64(objMFGBoilIssueProperty.history_union_id);
                        Lot_SrNo = Val.ToInt64(objMFGBoilIssueProperty.lot_srno);

                        Count++;
                        IntCounter++;
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
        private void backgroundWorker_BoilRecieve_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                panelProgress.Visible = false;
                if (IntRes > 0)
                {
                    Global.Confirm(Val.ToString(lueProcess.Text) + " Recieve Data Save Succesfully");
                    grdBoilRecieve.DataSource = null;
                    ClearDetails();
                }
                else
                {
                    Global.Confirm("Error In " + Val.ToString(lueProcess.Text) + " Recieve");
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (AddInGrid())
                {

                    lueSieve.EditValue = System.DBNull.Value;
                    //if (Val.ToString(lueSubProcess.Text) != "CHARNI")
                    //{
                    //    lueManager.EditValue = System.DBNull.Value;
                    //    lueEmployee.EditValue = System.DBNull.Value;
                    //    lueInvoiceNo.EditValue = System.DBNull.Value;
                    //    lblOsCarat.Text = "0";
                    //    lblOsPcs.Text = "0";
                    //    txtRate.Text = "0";
                    //    txtAmount.Text = "0";
                    //    m_manager_id = 0;
                    //    m_emp_id = 0;
                    //    m_BalCarat = 0;
                    //}
                    lblOsCarat.Text = Val.ToString((Val.ToDecimal(lblOsCarat.Text)) - (Val.ToDecimal(txtReturnCarat.Text) + Val.ToDecimal(txtLossCarat.Text)));
                    txtReturnPcs.Text = "0";
                    txtReturnCarat.Text = "0";
                    txtLossPcs.Text = "0";
                    txtLossCarat.Text = "0";
                    m_returnCarat = 0;
                    m_numlosscarat = 0;
                    lblLossCtP.Text = "0";
                    lblRetCtP.Text = "0";

                    //m_BalCarat = 0;
                    //chkWithoutLoss.Checked = false;
                    lueSieve.Focus();
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            MFGBoilIssueProperty objMFGBoilIssueProperty = new MFGBoilIssueProperty();
            MFGBoilRecieve objMFGBoilRecieve = new MFGBoilRecieve();

            try
            {
                m_dtbBoilReceive = (DataTable)grdBoilRecieve.DataSource;
                if (m_dtbBoilReceive.Rows.Count == 0)
                {
                    Global.Message("Please Select Records..");
                    return;
                }
                DialogResult result = MessageBox.Show("Do you want to Delete " + Val.ToString(lueProcess.Text) + " Recieve data?", "Confirmation", MessageBoxButtons.YesNo);
                if (result != DialogResult.Yes)
                {
                    btnSave.Enabled = true;
                    return;
                }
                btnDelete.Enabled = false;
                Conn = new BeginTranConnection(true, false);

                DataTable DTab = (DataTable)grdBoilRecieve.DataSource;

                objMFGBoilIssueProperty.union_id = Val.ToInt64(lblUnionId.Tag);
                objMFGBoilIssueProperty.process_id = Val.ToInt64(lueProcess.EditValue);
                objMFGBoilIssueProperty.sub_process_id = Val.ToInt64(lueSubProcess.EditValue);
                objMFGBoilIssueProperty = objMFGBoilRecieve.Boil_Delete(objMFGBoilIssueProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                Conn.Inter1.Commit();
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
                Conn.Inter1.Rollback();
                Conn = null;
                General.ShowErrors(ex.ToString());
                return;
            }
            finally
            {
                objMFGBoilIssueProperty = null;
            }
        }
        private void RepDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (Global.Confirm("Are you sure delete selected row?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                dgvBoilRecieve.DeleteRow(dgvBoilRecieve.GetRowHandle(dgvBoilRecieve.FocusedRowHandle));
                m_dtbBoilReceive.AcceptChanges();
            }
        }
        private void FrmMFGBoilRecieve_Load(object sender, EventArgs e)
        {
            Global.LOOKUPEmp(lueEmployee);
            Global.LOOKUPManager(lueManager);
            Global.LOOKUPRoughSieve(lueSieve);
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

            m_dtbType = new DataTable();
            m_dtbType.Columns.Add("type");
            m_dtbType.Rows.Add("REJECTION");
            m_dtbType.Rows.Add("ROUGH");

            lueType.Properties.DataSource = m_dtbType;
            lueType.Properties.ValueMember = "type";
            lueType.Properties.DisplayMember = "type";
            lueType.EditValue = "ROUGH";

            //m_dtbParam = Global.GetRoughCutAll();
            //if (GlobalDec.gEmployeeProperty.role_name == "SURAT ROUGH" || GlobalDec.gEmployeeProperty.role_name == "SURAT KAMALA")
            //{
            //    chkWithoutLoss.Visible = true;
            //    chkWithoutLoss.Checked = false;
            //}
            //else
            //{
            //    chkWithoutLoss.Visible = false;
            //    chkWithoutLoss.Checked = false;
            //}

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

                panelProgress.Visible = true;
                backgroundWorker_BoilRecieve.RunWorkerAsync();

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
            Global.Export("xlsx", dgvBoilRecieve);
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
                if (lueProcess.Text != "CHARNI")
                {
                    label15.Visible = false;
                    lueType.Visible = false;
                }
                else
                {
                    label15.Visible = true;
                    lueType.Visible = true;
                }
                if (lueProcess.Text == "CHARNI" && lueType.Text == "REJECTION")
                {
                    label5.Visible = false;
                    txtLossPcs.Visible = false;
                    label16.Visible = false;
                    txtLossCarat.Visible = false;
                }
                else
                {
                    label5.Visible = true;
                    txtLossPcs.Visible = true;
                    label16.Visible = true;
                    txtLossCarat.Visible = true;
                }
            }

            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void lueInvoiceNo_EditValueChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (lueInvoiceNo.EditValue != null && lueInvoiceNo.EditValue != System.DBNull.Value)
                {
                    DataTable dtIss = new DataTable();
                    string strFilter = string.Empty;
                    dtIss = objBoilRecieve.GetBoilRecievePending(Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue), Val.ToInt32(lueInvoiceNo.EditValue));
                    if (Val.ToInt32(lueInvoiceNo.EditValue) > 0)
                        strFilter = "purchase_detail_id = " + Val.ToInt32(lueInvoiceNo.EditValue);

                    dtIss.DefaultView.RowFilter = strFilter;
                    dtIss.DefaultView.ToTable();

                    DataTable dtb = dtIss.DefaultView.ToTable();
                    if (dtb.Rows.Count > 0)
                    {

                        m_manager_id = Val.ToInt(dtb.Rows[0]["manager_id"]);
                        m_emp_id = Val.ToInt(dtb.Rows[0]["employee_id"]);
                        m_issue_id = Val.ToInt(dtb.Rows[0]["issue_id"]);
                        m_invoice_id = Val.ToInt(dtb.Rows[0]["purchase_id"]);
                        m_invoice_no = Val.ToString(dtb.Rows[0]["invoice_no"]);
                        m_articleId = Val.ToInt(dtb.Rows[0]["article_id"]);
                        m_article = Val.ToString(dtb.Rows[0]["article_name"]);
                        lueManager.EditValue = Val.ToInt64(dtb.Rows[0]["manager_id"]);
                        lueEmployee.EditValue = Val.ToInt64(dtb.Rows[0]["employee_id"]);
                        lueSieve.EditValue = Val.ToInt(dtb.Rows[0]["rough_sieve_id"]);
                        txtReturnCarat.Text = Val.ToString(dtb.Rows[0]["carat"]);
                        txtReturnPcs.Text = Val.ToString(dtb.Rows[0]["pcs"]);
                        txtLossCarat.Text = Val.ToString(0);
                        txtLossPcs.Text = Val.ToString(0);
                        m_OsPcs = Val.ToInt(dtb.Rows[0]["pcs"]);
                        m_OsCarat = Val.ToDecimal(dtb.Rows[0]["carat"]);
                        //m_returnCarat = Val.ToDecimal(txtReturnCarat.Text);
                        lblOsCarat.Text = Val.ToString((Val.ToDecimal(dtb.Rows[0]["carat"])) - (Val.ToDecimal(clmReturnCarat.SummaryItem.SummaryValue) + Val.ToDecimal(clmLossCarat.SummaryItem.SummaryValue)));
                        lblOsPcs.Text = Val.ToString(dtb.Rows[0]["pcs"]);
                        txtRate.Text = Val.ToString(dtb.Rows[0]["rate"]);
                        txtAmount.Text = Val.ToString(dtb.Rows[0]["amount"]);

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
            if (lueProcess.EditValue != null && lueSubProcess.EditValue != System.DBNull.Value)
            {
                DataTable dtIss = new DataTable();
                dtIss = objBoilRecieve.GetBoilRecievePending(Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue));
                if (dtIss.Rows.Count > 0)
                {

                    lueInvoiceNo.Properties.DataSource = dtIss;
                    lueInvoiceNo.Properties.ValueMember = "purchase_detail_id";
                    lueInvoiceNo.Properties.DisplayMember = "invoice_no";

                }
                else
                {
                    Global.Message("Lot not issue in this process.");
                }

            }
        }
        private void txtLossCarat_Validated(object sender, EventArgs e)
        {
            if (Val.ToString(lueProcess.Text) != "CHARNI")
            {
                txtReturnCarat.Text = Val.ToString(m_OsCarat - Val.ToDecimal(txtLossCarat.Text));
            }
            if (Val.ToDecimal(m_OsCarat) < (Val.ToDecimal(txtLossCarat.Text) + Val.ToDecimal(txtReturnCarat.Text)))
            {
                Global.Message("Carat not greater than Oustanding Carat.");
            }

        }

        private void txtReturnCarat_Validated(object sender, EventArgs e)
        {
            if ((Val.ToDecimal(lblOsCarat.Text) < (Val.ToDecimal(txtLossCarat.Text) + Val.ToDecimal(txtReturnCarat.Text))) && GlobalDec.gEmployeeProperty.department_name != "ROUGH")
            {
                Global.Message("Carat not greater than Oustanding Carat.");
            }
            else if ((Val.ToDecimal(lblOsCarat.Text) > Val.ToDecimal(txtReturnCarat.Text)) && GlobalDec.gEmployeeProperty.department_name == "ROUGH" && (lueProcess.Text != "CHARNI" || lueType.Text != "REJECTION"))
            {
                txtLossCarat.Text = Val.ToString(Val.ToDecimal(lblOsCarat.Text) - Val.ToDecimal(txtReturnCarat.Text));

            }
            if (Val.ToDecimal(txtReturnCarat.Text) > 0 && (lueProcess.Text != "CHARNI" || lueType.Text != "REJECTION"))
            {
                lblRetCtP.Text = (Val.ToString(Math.Round((Val.ToDecimal(txtReturnCarat.Text) * 100) / (Val.ToDecimal(lblOsCarat.Text) + (Val.ToDecimal(clmReturnCarat.SummaryItem.SummaryValue) + Val.ToDecimal(clmLossCarat.SummaryItem.SummaryValue))), 2)));
                lblLossCtP.Text = Val.ToString(Math.Round((Val.ToDecimal(txtLossCarat.Text) * 100) / (Val.ToDecimal(lblOsCarat.Text) + (Val.ToDecimal(clmReturnCarat.SummaryItem.SummaryValue) + Val.ToDecimal(clmLossCarat.SummaryItem.SummaryValue))), 2));

            }
            else if (lueProcess.Text == "CHARNI" && lueType.Text == "REJECTION")
            {
                lblRetCtP.Text = (Val.ToString(Math.Round((Val.ToDecimal(txtReturnCarat.Text) * 100) / Val.ToDecimal(lblOsCarat.Text), 2)));
            }
            else
            {
                lblRetCtP.Text = Val.ToString((Val.ToDecimal(0)));
                lblLossCtP.Text = Val.ToString((Val.ToDecimal(0)));
            }
        }
        private void txtLossPcs_Validated(object sender, EventArgs e)
        {
            txtReturnPcs.Text = Val.ToString(m_OsPcs - Val.ToInt(txtLossPcs.Text));
            if (Val.ToInt(m_OsPcs) < (Val.ToInt(txtLossPcs.Text) + Val.ToInt(txtReturnPcs.Text)))
            {
                Global.Message("Pcs not greater than Oustanding Pcs.");
            }
        }
        private void txtReturnPcs_Validated(object sender, EventArgs e)
        {
            if ((Val.ToInt(m_OsPcs) < (Val.ToInt(txtLossPcs.Text) + Val.ToInt(txtReturnPcs.Text))) && GlobalDec.gEmployeeProperty.department_name != "ROUGH")
            {
                Global.Message("Pcs not greater than Oustanding Pcs.");
            }
            else if ((Val.ToInt(m_OsPcs) > Val.ToInt(txtReturnPcs.Text)) && GlobalDec.gEmployeeProperty.department_name == "ROUGH")
            {
                txtLossPcs.Text = Val.ToString(Val.ToInt(m_OsPcs) - Val.ToInt(txtReturnPcs.Text));
            }
        }

        private void txtReturnCarat_EditValueChanged(object sender, EventArgs e)
        {
            if (Val.ToString(lueProcess.Text) != "CHARNI")
            {
                if (Val.ToDecimal(txtReturnCarat.Text) != 0 && Val.ToDecimal(txtRate.Text) != 0 && Val.ToDecimal(lblOsCarat.Text) > 0)
                {
                    txtLossCarat.Text = Val.ToString(Val.ToDecimal(lblOsCarat.Text) - Val.ToDecimal(txtReturnCarat.Text));
                    decimal amount = Math.Round(Val.ToDecimal(txtReturnCarat.Text) * Val.ToDecimal(txtRate.Text), 2);
                    txtAmount.Text = Val.ToString(amount);
                }
                else
                {
                    txtAmount.Text = Val.ToString("0");
                }
            }
        }
        private void btnSearchRec_Click(object sender, EventArgs e)
        {
            FrmMFGSearchBoilIssRec_New FrmSearchProcess = new FrmMFGSearchBoilIssRec_New();
            FrmSearchProcess.FrmMFGBoilRecieve = this;
            //FrmSearchProcess.DTab = DtPending;
            FrmSearchProcess.ShowForm(this);
        }
        private void txtReturnPcs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtReturnCarat_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txtLossPcs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtLossCarat_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txtLostPcs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtLostCarat_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txtRejPcs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtRejCarat_KeyPress(object sender, KeyPressEventArgs e)
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

        #region Grid Event
        private void dgvBoilRecieve_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvBoilRecieve.GetDataRow(e.RowHandle);
                        btnAdd.Text = "&Update";
                        lueInvoiceNo.Text = Val.ToString(Drow["invoice_no"]);
                        lueSieve.Text = Val.ToString(Drow["sieve_name"]);
                        txtReturnPcs.Text = Val.ToString(Drow["return_pcs"]);
                        txtReturnCarat.Text = Val.ToString(Drow["return_carat"]);
                        txtLossPcs.Text = Val.ToString(Drow["loss_pcs"]);
                        txtLossCarat.Text = Val.ToString(Drow["loss_carat"]);
                        m_issue_id = Val.ToInt32(Drow["boil_issue_id"]);
                        m_numlosscarat = Val.ToDecimal(Drow["loss_carat"]);
                        m_invoice_no = Val.ToString(Drow["invoice_no"]);
                        m_update_srno = Val.ToInt(Drow["sr_no"]);
                        m_old_losscarat = Val.ToDecimal(Drow["loss_carat"]);
                        m_OsCarat = Val.ToDecimal(txtReturnCarat.Text);
                        m_OsPcs = Val.ToInt(txtReturnPcs.Text);
                        m_returnCarat = Val.ToDecimal(Drow["return_carat"]);
                        decimal OS_Carat = Val.ToDecimal(txtReturnCarat.Text) + Val.ToDecimal(txtLossCarat.Text);
                        int OS_Pcs = Val.ToInt(txtReturnPcs.Text) + Val.ToInt(txtLossPcs.Text);
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
        private void dgvWeightLossRec_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvWeightLossRec.GetDataRow(e.RowHandle);
                        DataRow drwNew = m_dtbBoilReceive.NewRow();

                        int numLossPcs = Val.ToInt32(Drow["loss_pcs"]);
                        decimal numLossCarat = Val.ToDecimal(Drow["loss_carat"]);
                        int numLostPcs = Val.ToInt32(Drow["lost_pcs"]);
                        decimal numLostCarat = Val.ToDecimal(Drow["lost_carat"]);
                        int numReturnPcs = Val.ToInt32(Drow["pcs"]);
                        decimal numReturnCarat = Val.ToDecimal(Drow["carat"]);
                        dtpReceiveDate.Text = Val.ToString(Drow["receive_date"]);
                        drwNew["recieve_id"] = Val.ToInt32(Drow["receive_id"]);
                        drwNew["invoice_no"] = Val.ToString(Drow["rough_cut_no"]);
                        drwNew["manager_id"] = Val.ToInt32(Drow["manager_id"]);
                        drwNew["employee_id"] = Val.ToInt32(Drow["employee_id"]);
                        drwNew["process_id"] = Val.ToInt32(Drow["process_id"]);
                        drwNew["sub_process_id"] = Val.ToInt32(Drow["sub_process_id"]);
                        drwNew["boil_issue_id"] = Val.ToInt32(Drow["issue_id"]);
                        drwNew["return_pcs"] = numReturnPcs;
                        drwNew["return_carat"] = numReturnCarat;
                        drwNew["loss_pcs"] = numLossPcs;
                        drwNew["loss_carat"] = numLossCarat;
                        drwNew["sr_no"] = m_Srno;
                        m_dtbBoilReceive.Rows.Add(drwNew);
                        grdBoilRecieve.DataSource = m_dtbBoilReceive;

                        ttlbWeightLossRecieve.SelectedTabPage = tblWeightLossdetail;
                        lueInvoiceNo.Focus();
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
                    DataRow[] dr;
                    if (m_dtbBoilReceive.Rows.Count > 0)
                    {
                        if (m_dtbBoilReceive.Rows[0]["invoice_no"].ToString() != Val.ToString(lueInvoiceNo.Text) && Val.ToString(lueSubProcess.Text) == "CHARNI")
                        {
                            Global.Message("Single Invoice Recieve at a Time.");
                            //lueInvoiceNo.EditValue = null;
                            blnReturn = false;
                            return blnReturn;
                        }
                    }
                    if (Val.ToInt32(lueProcess.EditValue) == 1002)
                    {
                        dr = m_dtbBoilReceive.Select("invoice_no = '" + Val.ToString(lueInvoiceNo.Text) + "' and sieve_name = '" + Val.ToString(lueSieve.Text) + "'");
                    }
                    else
                    {
                        dr = m_dtbBoilReceive.Select("invoice_no = '" + Val.ToString(lueInvoiceNo.Text) + "'");
                    }

                    if (dr.Count() == 1)
                    {
                        Global.Message("Record already exists in grid", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //lueInvoiceNo.EditValue = null;
                        blnReturn = false;
                        return blnReturn;
                    }

                    DataRow drwNew = m_dtbBoilReceive.NewRow();
                    int numLossPcs = Val.ToInt(txtLossPcs.Text);
                    decimal numLossCarat = Val.ToDecimal(txtLossCarat.Text);
                    int numReturnPcs = Val.ToInt(txtReturnPcs.Text);
                    decimal numReturnCarat = Val.ToDecimal(txtReturnCarat.Text);
                    decimal numRate = Val.ToDecimal(txtRate.Text);
                    decimal numAmount = Val.ToDecimal(txtAmount.Text);
                    drwNew["recieve_id"] = Val.ToInt(0);
                    drwNew["invoice_no"] = Val.ToString(lueInvoiceNo.Text);
                    drwNew["purchase_id"] = Val.ToString(m_invoice_id);
                    drwNew["purchase_detail_id"] = Val.ToString(lueInvoiceNo.EditValue);
                    drwNew["manager_id"] = Val.ToInt(lueManager.EditValue);
                    drwNew["manager"] = Val.ToString(lueManager.Text);
                    drwNew["employee_id"] = Val.ToInt(lueEmployee.EditValue);
                    drwNew["employee"] = Val.ToString(lueEmployee.Text);
                    drwNew["rough_sieve_id"] = Val.ToInt(lueSieve.EditValue);
                    drwNew["sieve_name"] = Val.ToString(lueSieve.Text);
                    drwNew["article_id"] = Val.ToInt(m_articleId);
                    drwNew["article_name"] = Val.ToString(m_article);
                    drwNew["return_pcs"] = numReturnPcs;
                    drwNew["return_carat"] = numReturnCarat;
                    drwNew["loss_pcs"] = numLossPcs;
                    drwNew["loss_carat"] = numLossCarat;
                    drwNew["rate"] = numRate;
                    drwNew["amount"] = numAmount;
                    drwNew["sr_no"] = m_Srno;
                    drwNew["boil_issue_id"] = m_issue_id;
                    drwNew["return_per"] = Val.ToDecimal(lblRetCtP.Text);
                    drwNew["loss_per"] = Val.ToDecimal(lblLossCtP.Text);
                    drwNew["type"] = Val.ToString(lueType.Text);

                    m_dtbBoilReceive.Rows.Add(drwNew);
                    m_Srno++;
                }
                else if (btnAdd.Text == "&Update")
                {

                    if (m_dtbBoilReceive.Select("sr_no ='" + Val.ToInt(m_update_srno) + "'").Length > 0)
                    {
                        for (int i = 0; i < m_dtbBoilReceive.Rows.Count; i++)
                        {
                            if (m_dtbBoilReceive.Select("invoice_no ='" + m_invoice_no + "' AND sr_no = '" + m_update_srno + "'").Length > 0)
                            {
                                if (m_dtbBoilReceive.Rows[dgvBoilRecieve.FocusedRowHandle]["invoice_no"].ToString() == m_invoice_no.ToString())
                                {
                                    m_dtbBoilReceive.Rows[dgvBoilRecieve.FocusedRowHandle]["return_pcs"] = Val.ToInt(txtReturnPcs.Text).ToString();
                                    m_dtbBoilReceive.Rows[dgvBoilRecieve.FocusedRowHandle]["return_carat"] = Val.ToString(txtReturnCarat.Text).ToString();
                                    m_dtbBoilReceive.Rows[dgvBoilRecieve.FocusedRowHandle]["loss_pcs"] = Val.ToInt(txtLossPcs.Text).ToString();
                                    m_dtbBoilReceive.Rows[dgvBoilRecieve.FocusedRowHandle]["loss_carat"] = Val.ToDecimal(txtLossCarat.Text);
                                    m_dtbBoilReceive.Rows[dgvBoilRecieve.FocusedRowHandle]["rate"] = Val.ToDecimal(txtRate.Text);
                                    m_dtbBoilReceive.Rows[dgvBoilRecieve.FocusedRowHandle]["amount"] = Val.ToDecimal(txtAmount.Text);
                                    m_dtbBoilReceive.Rows[dgvBoilRecieve.FocusedRowHandle]["return_per"] = Val.ToDecimal(lblRetCtP.Text);
                                    m_dtbBoilReceive.Rows[dgvBoilRecieve.FocusedRowHandle]["loss_per"] = Val.ToDecimal(lblLossCtP.Text);
                                    m_dtbBoilReceive.Rows[dgvBoilRecieve.FocusedRowHandle]["type"] = Val.ToString(lueType.Text);
                                    break;
                                }
                            }
                        }
                    }
                    btnAdd.Text = "&Add";
                }
                dgvBoilRecieve.MoveLast();
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
                    if (m_dtbBoilReceive.Rows.Count == 0)
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

                    if (Val.ToDecimal(lblOsCarat.Text) < 0)
                    {
                        lstError.Add(new ListError(5, "Os Carat is less then zero"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtReturnCarat.Focus();
                        }
                    }

                    //if (Val.ToDecimal(lblOsCarat.Text) != (Val.ToDecimal(dgvBoilRecieve.Columns["return_carat"].SummaryText) + (Val.ToDecimal(dgvBoilRecieve.Columns["loss_carat"].SummaryText))) && Val.ToString(lueProcess.Text) != "CHARNI")
                    //{
                    //    lstError.Add(new ListError(5, "Recieve carat not match with os Carat"));
                    //    if (!blnFocus)
                    //    {
                    //        blnFocus = true;
                    //        txtReturnCarat.Focus();
                    //    }
                    //}

                }
                if (m_blnadd)
                {
                    if (lueInvoiceNo.Text == "")
                    {
                        lstError.Add(new ListError(13, "Invoice No"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueInvoiceNo.Focus();
                        }
                    }
                    if (Val.ToString(lueSieve.Text) == "" && Val.ToString(lueProcess.Text) == "CHARNI")
                    {
                        lstError.Add(new ListError(13, "Rough Sieve"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueSieve.Focus();
                        }
                    }
                    //if ((txtLossCarat.Text == string.Empty || txtLossCarat.Text == "0"))
                    //{
                    //    lstError.Add(new ListError(5, "Loss Carat can not be blank!!!"));                            
                    //    if (!blnFocus)
                    //    {
                    //        blnFocus = true;
                    //        txtLossCarat.Focus();
                    //    }
                    //}                                                          
                    if (Val.ToInt(txtLossPcs.Text) > 0 && txtLossPcs.Text != string.Empty)
                    {
                        if (Val.ToInt(lblOsPcs.Text) < (Val.ToInt(txtLossPcs.Text) + Val.ToInt(txtReturnPcs.Text)))
                        {
                            lstError.Add(new ListError(5, "Entry Pcs not greater than total pcs"));
                            if (!blnFocus)
                            {
                                blnFocus = true;
                                txtLossPcs.Focus();
                            }
                        }
                    }
                    if (Val.ToDecimal(txtLossCarat.Text) > 0 && txtLossCarat.Text != string.Empty)
                    {
                        if (Val.ToDecimal(lblOsCarat.Text) < (Val.ToDecimal(txtLossCarat.Text) + Val.ToDecimal(txtReturnCarat.Text)))
                        {
                            lstError.Add(new ListError(5, "Entry Carat not greater than total carat"));
                            if (!blnFocus)
                            {
                                blnFocus = true;
                                txtLossCarat.Focus();
                            }
                        }
                    }
                    if (Val.ToDecimal(txtLossCarat.Text) < 0)
                    {

                        lstError.Add(new ListError(5, "Entry Carat not greater than total carat"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtLossCarat.Focus();
                        }

                    }
                    if (Val.ToDecimal(txtReturnCarat.Text) == 0 || txtReturnCarat.Text == string.Empty)
                    {
                        lstError.Add(new ListError(5, "Return Carat not Blank"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtReturnCarat.Focus();
                        }

                    }
                    if ((Val.ToInt(txtLossPcs.Text) > 0 && txtLossPcs.Text != string.Empty))
                    {
                        if (Val.ToInt(lblOsPcs.Text) < (Val.ToInt(txtReturnPcs.Text) + Val.ToInt(txtLossPcs.Text)))
                        {
                            lstError.Add(new ListError(5, "Entry Pcs not greater than total pcs"));
                            if (!blnFocus)
                            {
                                blnFocus = true;
                                txtLossPcs.Focus();
                            }
                        }
                    }
                    if (Val.ToDecimal(txtReturnCarat.Text) > 0 && txtReturnCarat.Text != string.Empty)
                    {
                        if (Val.ToDecimal(lblOsCarat.Text) < (Val.ToDecimal(txtReturnCarat.Text) + Val.ToDecimal(dgvBoilRecieve.Columns["loss_carat"].SummaryText) + Val.ToDecimal(txtLossCarat.Text) - m_numlosscarat - m_returnCarat))
                        {
                            lstError.Add(new ListError(5, "Return Carat not greater than total Carat"));
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

                lueInvoiceNo.EditValue = System.DBNull.Value;
                lueManager.EditValue = System.DBNull.Value;
                lueEmployee.EditValue = System.DBNull.Value;
                lueSieve.EditValue = System.DBNull.Value;
                lueProcess.EditValue = System.DBNull.Value;
                lueSubProcess.EditValue = System.DBNull.Value;
                lueProcess.Text = "BOILING";
                lueSubProcess.Text = "BOILING";
                lueSubProcess.EditValue = 1003;
                lueProcess.Enabled = true;
                lueSubProcess.Enabled = true;
                txtReturnPcs.Text = string.Empty;
                txtReturnCarat.Text = string.Empty;
                txtLossPcs.Text = string.Empty;
                txtLossCarat.Text = string.Empty;
                lblOsCarat.Text = "0.00";
                lblOsPcs.Text = "0";
                m_manager_id = 0;
                m_emp_id = 0;
                m_Srno = 1;
                m_update_srno = 1;
                btnAdd.Text = "&Add";
                lblUnionId.Tag = "0";
                txtPassword.Text = "";
                m_invoice_id = 0;
                m_invoice_no = "";
                btnSave.Enabled = true;
                dtpReceiveDate.Focus();
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
                if (m_dtbBoilReceive.Rows.Count > 0)
                    m_dtbBoilReceive.Rows.Clear();

                m_dtbBoilReceive = new DataTable();

                m_dtbBoilReceive.Columns.Add("recieve_id", typeof(int));
                m_dtbBoilReceive.Columns.Add("boil_issue_id", typeof(int));
                m_dtbBoilReceive.Columns.Add("rough_sieve_id", typeof(string));
                m_dtbBoilReceive.Columns.Add("sieve_name", typeof(string));
                m_dtbBoilReceive.Columns.Add("manager_id", typeof(int));
                m_dtbBoilReceive.Columns.Add("manager", typeof(string));
                m_dtbBoilReceive.Columns.Add("employee_id", typeof(int));
                m_dtbBoilReceive.Columns.Add("employee", typeof(string));
                m_dtbBoilReceive.Columns.Add("return_pcs", typeof(int)).DefaultValue = 0;
                m_dtbBoilReceive.Columns.Add("return_carat", typeof(decimal));
                m_dtbBoilReceive.Columns.Add("loss_pcs", typeof(int)).DefaultValue = 0;
                m_dtbBoilReceive.Columns.Add("loss_carat", typeof(decimal));
                m_dtbBoilReceive.Columns.Add("rate", typeof(decimal));
                m_dtbBoilReceive.Columns.Add("amount", typeof(decimal));
                m_dtbBoilReceive.Columns.Add("sr_no", typeof(int)).DefaultValue = 1;
                m_dtbBoilReceive.Columns.Add("invoice_no", typeof(string));
                m_dtbBoilReceive.Columns.Add("purchase_id", typeof(int)).DefaultValue = 0;
                m_dtbBoilReceive.Columns.Add("purchase_detail_id", typeof(int)).DefaultValue = 0;
                m_dtbBoilReceive.Columns.Add("article_id", typeof(int));
                m_dtbBoilReceive.Columns.Add("article_name", typeof(string));
                m_dtbBoilReceive.Columns.Add("return_per", typeof(decimal));
                m_dtbBoilReceive.Columns.Add("loss_per", typeof(decimal));
                m_dtbBoilReceive.Columns.Add("type", typeof(string));
                grdBoilRecieve.DataSource = m_dtbBoilReceive;
                grdBoilRecieve.Refresh();
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
            //MFGProcessWeightLossRecieve objWeightLossRec = new MFGProcessWeightLossRecieve();
            //DateTime datFromDate = DateTime.MinValue;
            //DateTime datToDate = DateTime.MinValue;
            //try
            //{
            //    m_dtbDetails = objWeightLossRec.GetData(Convert.ToDateTime(dtpFromDate.Text), Convert.ToDateTime(dtpToDate.Text));

            //    if (m_dtbDetails.Rows.Count == 0)
            //    {
            //        Global.Message("Data Not Found");
            //        blnReturn = false;
            //    }

            //    grdWeightLossRec.DataSource = m_dtbDetails;
            //}
            //catch (Exception ex)
            //{
            //    BLL.General.ShowErrors(ex);
            //    blnReturn = false;
            //}
            //finally
            //{
            //    objWeightLossRec = null;
            //}

            return blnReturn;
        }
        public void FillGrid(int Lot_SRNo)
        {
            MFGBoilRecieve objMFGBoilRecieve = new MFGBoilRecieve();

            DataTable DTab_DeptBoilRecieve = objMFGBoilRecieve.GetIssRecDetails(Lot_SRNo);

            btnSave.Enabled = false;

            if (DTab_DeptBoilRecieve.Rows.Count > 0)
            {
                dtpReceiveDate.Text = Val.DBDate(DTab_DeptBoilRecieve.Rows[0]["issue_date"].ToString());
                lblUnionId.Tag = Val.ToInt64(DTab_DeptBoilRecieve.Rows[0]["union_id"]).ToString();
                lueProcess.EditValue = Val.ToInt32(DTab_DeptBoilRecieve.Rows[0]["process_id"]);
                lueSubProcess.EditValue = Val.ToInt32(DTab_DeptBoilRecieve.Rows[0]["sub_process_id"]);
                //lueEmployee.EditValue = Val.ToInt64(DTab_DeptBoilRecieve.Rows[0]["employee_id"]);
                //lueManager.EditValue = Val.ToInt64(DTab_DeptBoilRecieve.Rows[0]["manager_id"]);
                //lueSieve.EditValue = Val.ToInt32(DTab_DeptBoilRecieve.Rows[0]["rough_sieve_id"]);
            }
            else
            {
                lblUnionId.Tag = "0";
            }
            grdBoilRecieve.DataSource = DTab_DeptBoilRecieve;
            grdBoilRecieve.RefreshDataSource();
            dgvBoilRecieve.BestFitColumns();


        }

        #endregion

        private void txtLotID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtRate_EditValueChanged(object sender, EventArgs e)
        {
            if (Val.ToDecimal(txtReturnCarat.Text) != 0 && Val.ToDecimal(txtRate.Text) != 0)
            {
                decimal amount = Math.Round(Val.ToDecimal(txtReturnCarat.Text) * Val.ToDecimal(txtRate.Text), 2);
                txtAmount.Text = Val.ToString(amount);
            }
            else
            {
                txtAmount.Text = Val.ToString("0");
            }
        }

        private void txtLossCarat_EditValueChanged(object sender, EventArgs e)
        {
            if (Val.ToString(lueProcess.Text) != "CHARNI")
            {
                if (Val.ToDecimal(txtReturnCarat.Text) != 0 && Val.ToDecimal(txtRate.Text) != 0 && Val.ToDecimal(lblOsCarat.Text) > 0)
                {
                    txtLossCarat.Text = Val.ToString(Val.ToDecimal(lblOsCarat.Text) - Val.ToDecimal(txtReturnCarat.Text));
                    decimal amount = Math.Round(Val.ToDecimal(txtReturnCarat.Text) * Val.ToDecimal(txtRate.Text), 2);
                    txtAmount.Text = Val.ToString(amount);
                }
                else
                {
                    txtAmount.Text = Val.ToString("0");
                }
            }
        }

        private void lueEmployee_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

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

        private void grdBoilRecieve_Click(object sender, EventArgs e)
        {

        }

        private void lueType_Validated(object sender, EventArgs e)
        {
            if (lueProcess.Text == "CHARNI" && lueType.Text == "REJECTION")
            {
                label5.Visible = false;
                txtLossPcs.Visible = false;
                label16.Visible = false;
                txtLossCarat.Visible = false;
                txtLossCarat.Text = "0";
                txtLossPcs.Text = "0";
            }
            else
            {
                label5.Visible = true;
                txtLossPcs.Visible = true;
                label16.Visible = true;
                txtLossCarat.Visible = true;
            }
        }

        private void lueType_EditValueChanged(object sender, EventArgs e)
        {
            if (lueProcess.Text == "CHARNI" && lueType.Text == "REJECTION")
            {
                label5.Visible = false;
                txtLossPcs.Visible = false;
                label16.Visible = false;
                txtLossCarat.Visible = false;
                txtLossCarat.Text = "0";
                txtLossPcs.Text = "0";
            }
            else
            {
                label5.Visible = true;
                txtLossPcs.Visible = true;
                label16.Visible = true;
                txtLossCarat.Visible = true;
            }
        }
    }
}
