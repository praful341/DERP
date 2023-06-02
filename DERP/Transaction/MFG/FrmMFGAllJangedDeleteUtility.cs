using BLL;
using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.PropertyClasses.Transaction;
using BLL.PropertyClasses.Transaction.MFG;
using DERP.Class;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Transaction.MFG
{
    public partial class FrmMFGAllJangedDeleteUtility : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;
        MFGLiveStock objLiveStock = new MFGLiveStock();
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        Control _NextEnteredControl;
        private List<Control> _tabControls;

        MFGDepartmentTransfer objDepartmentTransfer;
        MFGSawableRecieve objSawableRecieve;
        MFGJangedReturn objJangedReturn = new MFGJangedReturn();
        MFGProcessIssue objMFGProcessIssue = new MFGProcessIssue();
        MFGProcessIssueProperty objMFGProcessIssueProperty = new MFGProcessIssueProperty();

        DataTable DtControlSettings;
        DataTable m_dtbDetail;
        DataTable m_dtbParam;
        DataTable m_dtbSubProcess;
        DataTable m_dtbDepartmentTransfer;
        DataTable m_dtOutstanding;
        DataTable DTab_LotId = new DataTable();
        DataTable m_dtbKapan;
        DataTable DtPending = new DataTable();
        DataTable DTabTemp = new DataTable();
        //DataTable DTab_KapanWiseData = new DataTable();

        int m_Srno;
        Int64 m_numForm_id;
        Int64 IntRes;

        #endregion

        #region Constructor
        public FrmMFGAllJangedDeleteUtility()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            _NextEnteredControl = new Control();
            _tabControls = new List<Control>();

            objDepartmentTransfer = new MFGDepartmentTransfer();
            objSawableRecieve = new MFGSawableRecieve();

            DtControlSettings = new DataTable();
            m_dtbDetail = new DataTable();
            m_dtbParam = new DataTable();
            m_dtbSubProcess = new DataTable();
            m_dtbDepartmentTransfer = new DataTable();
            m_dtOutstanding = new DataTable();
            m_dtbKapan = new DataTable();

            m_Srno = 1;
            m_numForm_id = 0;
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
            //Val.frmGenSet(this);
            AttachFormEvents();
            //if (Global.HideFormControls(Val.ToInt(ObjPer.form_id), this) != "")
            //{
            //    Global.Message("Select First User Setting...Please Contact to Administrator...");
            //    return;
            //}

            //ControlSettingDT(Val.ToInt(ObjPer.form_id), this);
            //AddGotFocusListener(this);
            //AddKeyPressListener(this);
            //this.KeyPreview = true;

            //TabControlsToList(this.Controls);
            //_tabControls = _tabControls.OrderBy(x => x.TabIndex).ToList();
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

        #region Events
        private void RepDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (Global.Confirm("Are you sure delete selected row?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                dgvAllJangedDeleteUtility.DeleteRow(dgvAllJangedDeleteUtility.GetRowHandle(dgvAllJangedDeleteUtility.FocusedRowHandle));
                m_dtbDepartmentTransfer.AcceptChanges();
            }
        }
        private void FrmMFGProcessIssue_Load(object sender, EventArgs e)
        {
            try
            {
                //Global.LOOKUPDepartment(lueToDepartment);         

                m_dtbKapan = Global.GetKapanAll();

                lueKapan.Properties.DataSource = m_dtbKapan;
                lueKapan.Properties.ValueMember = "kapan_id";
                lueKapan.Properties.DisplayMember = "kapan_no";

                m_dtbParam = Global.GetRoughCutAll();

                lueCutNo.Properties.DataSource = m_dtbParam;
                lueCutNo.Properties.ValueMember = "rough_cut_id";
                lueCutNo.Properties.DisplayMember = "rough_cut_no";


                ClearDetails();
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
                //if (AddInGrid())
                //{
                //    txtLotID.Text = string.Empty;
                //    //lueCutNo.EditValue = System.DBNull.Value;
                //    //lueLotId.EditValue = System.DBNull.Value;
                //    //txtPcs.Text = string.Empty;
                //    prdId = 0;
                //    m_balcarat = 0;
                //    //txtCarat.Text = string.Empty;
                //    //lueLotId.Focus();
                //}
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearDetails();
            //grdProcessWeightLossRecieve.DataSource = null;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            Global.Export("xlsx", dgvAllJangedDeleteUtility);
        }
        private void txtLotID_Validated(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToInt64(txtLotID.Text) > 0)
                {
                    //DTab_LotId = DtPending.Select("lot_id in(" + Val.ToInt64(txtLotID.Text) + ")").CopyToDataTable();

                    //kapan_id = Val.ToInt(DTab_LotId.Rows[0]["kapan_no"]);

                    DataRow[] dr = DtPending.Select("lot_id = " + Val.ToInt64(txtLotID.Text));

                    if (dr.Length > 0)
                    {

                        if (AddInGrid())
                        {
                            txtLotID.Text = string.Empty;
                            lueCutNo.EditValue = System.DBNull.Value;
                        }
                        txtLotID.Focus();
                    }
                    else
                    {
                        Global.Message("Lot Not Found.");
                    }



                }

            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
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
        private void lueCutNo_Validated(object sender, EventArgs e)
        {

        }
        private void lueCutNo_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    if (Val.ToInt(lueCutNo.EditValue) > 0)
                    {
                        //DTab_LotId = objDepartmentTransfer.GetDataLotID(Val.ToInt(lueCutNo.EditValue));

                        MFGProcessIssue objMFGProcessIssue = new MFGProcessIssue();
                        MFGProcessIssueProperty objMFGProcessIssueProperty = new MFGProcessIssueProperty();
                        objMFGProcessIssueProperty.rough_cut_id = Val.ToInt(lueCutNo.EditValue);
                        objMFGProcessIssueProperty.kapan_id = Val.ToInt(lueKapan.EditValue);

                        DtPending = objMFGProcessIssue.GetPendingStock(objMFGProcessIssueProperty);

                    }
                    if (DtPending.Rows.Count > 0)
                    {
                        //lueLotId.Properties.DataSource = DtPending;
                        //lueLotId.Properties.DisplayMember = "lot_id";
                        //lueLotId.Properties.ValueMember = "lot_id";
                    }
                    //else
                    //{
                    //    Global.Message("Lot ID Not Found in this Cut No =" + lueCutNo.Text);
                    //    lueLotId.EditValue = null;
                    //    return;
                    //}

                    //if (Val.ToInt(lueToDepartment.EditValue) > 0 && Val.ToInt(lueManager.EditValue) > 0 && Val.ToInt(lueProcess.EditValue) > 0)
                    //{
                    //    btnAdd.Enabled = true;
                    //    btnPopUpStock.Enabled = true;
                    //}
                    //else
                    //{
                    //    btnAdd.Enabled = false;
                    //    btnPopUpStock.Enabled = false;
                    //}
                }
                catch (Exception ex)
                {
                    Global.Message(ex.ToString());
                    return;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void lueLotId_EditValueChanged(object sender, EventArgs e)
        {
            //if (Val.ToInt(lueLotId.EditValue) > 0)
            //{
            //    DTab_LotId = DtPending.Select("lot_id in(" + Val.ToInt(lueLotId.EditValue) + ")").CopyToDataTable();

            //    kapan_id = Val.ToInt(DTab_LotId.Rows[0]["kapan_id"]);


            //    DataRow[] dr = DTab_LotId.Select("lot_id = " + Val.ToInt(lueLotId.EditValue));

            //    if (dr.Length > 0)
            //    {
            //        txtPcs.Text = Val.ToInt(dr[0]["pcs"]).ToString();
            //        txtCarat.Text = Val.ToDecimal(dr[0]["carat"]).ToString();
            //    }
            //}
        }
        private void grdDepartmentTransfer_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.F9)
            //{
            //    if (Global.Confirm("Are you sure delete selected row?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            //    {
            //        dgvDepartmentTransfer.DeleteRow(dgvDepartmentTransfer.GetRowHandle(dgvDepartmentTransfer.FocusedRowHandle));
            //    }
            //}
        }

        private void lueToDepartment_EditValueChanged(object sender, EventArgs e)
        {
            //if (Val.ToInt(lueToDepartment.EditValue) > 0 && Val.ToInt(lueManager.EditValue) > 0 && Val.ToInt(lueProcess.EditValue) > 0)
            //{
            //    btnAdd.Enabled = true;
            //    btnPopUpStock.Enabled = true;
            //}
            //else
            //{
            //    btnAdd.Enabled = false;
            //    btnPopUpStock.Enabled = false;
            //}
        }

        private void backgroundWorker_DeptTransfer_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                //List<ListError> lstError = new List<ListError>();

                m_dtbDepartmentTransfer.AcceptChanges();
                m_dtbDepartmentTransfer = (DataTable)grdAllJangedDeleteUtility.DataSource;
                MFGDepartmentTransfer objMFGDepartmentTransfer = new MFGDepartmentTransfer();
                MFGDepartmentTransferProperty objMFGDepartmentTransferProperty = new MFGDepartmentTransferProperty();
                Conn = new BeginTranConnection(true, false);
                IntRes = 0;

                int IntCounter = 0;
                int Count = 0;
                int TotalCount = m_dtbDepartmentTransfer.Rows.Count;
                try
                {
                    foreach (DataRow drw in m_dtbDepartmentTransfer.Rows)
                    {

                        objMFGDepartmentTransferProperty.lot_id = Val.ToInt(drw["lot_id"]);
                        objMFGDepartmentTransferProperty.cut_id = Val.ToInt(drw["rough_cut_id"]);

                        IntRes = objMFGDepartmentTransfer.Delete(objMFGDepartmentTransferProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                        //IntRes = objMFGDepartmentTransferProperty.union_id;
                        //NewHistory_Union_Id = Val.ToInt64(objMFGDepartmentTransferProperty.history_union_id);
                        //Lot_SrNo = Val.ToInt64(objMFGDepartmentTransferProperty.lot_srno);

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

        private void backgroundWorker_DeptTransfer_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                panelProgress.Visible = false;
                if (IntRes > 0)
                {
                    Global.Confirm("Deleted Succesfully");
                    grdAllJangedDeleteUtility.DataSource = null;
                    btnDelete.Enabled = true;
                    lueCutNo.EditValue = System.DBNull.Value;
                    ClearDetails();
                }
                else
                {
                    Global.Confirm("Error In Deleted");
                    btnDelete.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }
        #endregion

        #region Function
        private bool AddInGrid()
        {
            bool blnReturn = true;

            try
            {


                DataRow[] dr = null;
                if (Val.ToInt64(txtLotID.Text) > 0)
                {
                    dr = m_dtbDepartmentTransfer.Select("lot_id = " + Val.ToInt64(txtLotID.Text));
                }
                else
                {
                    dr = m_dtbDepartmentTransfer.Select("rough_cut_id = " + Val.ToInt(lueCutNo.EditValue) + " AND lot_id = " + (Val.ToInt64(txtLotID.Text)));
                }
                if (dr.Count() == 1)
                {
                    Global.Message("Record already exists in grid", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //lueLotId.Focus();
                    blnReturn = false;
                    return blnReturn;
                }
                DataRow drwNew = m_dtbDepartmentTransfer.NewRow();
                //int numPcs = Val.ToInt(txtPcs.Text);
                //decimal numCarat = Val.ToDecimal(txtCarat.Text);

                //drwNew["transfer_date"] = Val.DBDate(dtpDate.Text);
                drwNew["rough_cut_id"] = Val.ToInt64(DtPending.Rows[0]["rough_cut_id"]);
                drwNew["rough_cut_no"] = Val.ToString(DtPending.Rows[0]["rough_cut_no"]);
                drwNew["lot_id"] = Val.ToInt(txtLotID.EditValue) == 0 ? Val.ToInt64(txtLotID.Text) : Val.ToInt(txtLotID.EditValue);

                drwNew["kapan_no"] = Val.ToString(DtPending.Rows[0]["kapan_no"]);
                drwNew["kapan_id"] = Val.ToInt64(DtPending.Rows[0]["kapan_id"]);

                drwNew["pcs"] = Val.ToInt32(DtPending.Rows[0]["balance_pcs"]);
                drwNew["carat"] = Val.ToDecimal(DtPending.Rows[0]["balance_carat"]);
                drwNew["sr_no"] = m_Srno;
                m_dtbDepartmentTransfer.Rows.Add(drwNew);
                m_Srno++;


                dgvAllJangedDeleteUtility.MoveLast();
                dgvAllJangedDeleteUtility.BestFitColumns();
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
                if (!GenerateDepartmentDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }


                lueKapan.EditValue = System.DBNull.Value;
                lueCutNo.EditValue = System.DBNull.Value;
                lueKapan.Enabled = false;
                lueCutNo.Enabled = false;
                txtLotID.Text = string.Empty;
                DtPending = objLiveStock.GetData();
                txtLotID.Focus();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
        }
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
                        //((LookUpEdit)(Control)sender).ClosePopup();
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
        private bool GenerateDepartmentDetails()
        {
            bool blnReturn = true;
            try
            {
                if (m_dtbDepartmentTransfer.Rows.Count > 0)
                    m_dtbDepartmentTransfer.Rows.Clear();

                m_dtbDepartmentTransfer = new DataTable();

                m_dtbDepartmentTransfer.Columns.Add("transfer_id", typeof(int));
                //m_dtbDepartmentTransfer.Columns.Add("recieve_date", typeof(DateTime));
                m_dtbDepartmentTransfer.Columns.Add("lot_id", typeof(int));
                m_dtbDepartmentTransfer.Columns.Add("rough_cut_id", typeof(int));
                m_dtbDepartmentTransfer.Columns.Add("rough_cut_no", typeof(string));
                // m_dtbDepartmentTransfer.Columns.Add("purity_group", typeof(string));
                m_dtbDepartmentTransfer.Columns.Add("from_department_id", typeof(int));

                //m_dtbDepartmentTransfer.Columns.Add("purity_name", typeof(string));
                m_dtbDepartmentTransfer.Columns.Add("from_manager_id", typeof(int));
                m_dtbDepartmentTransfer.Columns.Add("prediction_id", typeof(int));

                m_dtbDepartmentTransfer.Columns.Add("kapan_no", typeof(string));
                m_dtbDepartmentTransfer.Columns.Add("kapan_id", typeof(Int64));
                m_dtbDepartmentTransfer.Columns.Add("employee_name", typeof(string));
                m_dtbDepartmentTransfer.Columns.Add("employee_id", typeof(Int64));
                m_dtbDepartmentTransfer.Columns.Add("manager_name", typeof(string));
                m_dtbDepartmentTransfer.Columns.Add("manager_id", typeof(Int64));
                m_dtbDepartmentTransfer.Columns.Add("process_name", typeof(string));
                m_dtbDepartmentTransfer.Columns.Add("process_id", typeof(Int64));
                m_dtbDepartmentTransfer.Columns.Add("sub_process_name", typeof(string));
                m_dtbDepartmentTransfer.Columns.Add("sub_process_id", typeof(Int64));
                m_dtbDepartmentTransfer.Columns.Add("quality_name", typeof(string));
                m_dtbDepartmentTransfer.Columns.Add("quality_id", typeof(Int64));
                m_dtbDepartmentTransfer.Columns.Add("sieve_name", typeof(string));
                m_dtbDepartmentTransfer.Columns.Add("rough_sieve_id", typeof(Int64));
                m_dtbDepartmentTransfer.Columns.Add("rough_clarity_name", typeof(string));
                m_dtbDepartmentTransfer.Columns.Add("rough_clarity_id", typeof(Int64));
                m_dtbDepartmentTransfer.Columns.Add("purity_name", typeof(string));
                m_dtbDepartmentTransfer.Columns.Add("purity_id", typeof(Int64));

                m_dtbDepartmentTransfer.Columns.Add("pcs", typeof(int)).DefaultValue = 0;
                m_dtbDepartmentTransfer.Columns.Add("carat", typeof(decimal)).DefaultValue = 0;
                m_dtbDepartmentTransfer.Columns.Add("sr_no", typeof(int)).DefaultValue = 1;
                m_dtbDepartmentTransfer.Columns.Add("rr_pcs", typeof(int));
                m_dtbDepartmentTransfer.Columns.Add("rr_carat", typeof(decimal));

                grdAllJangedDeleteUtility.DataSource = m_dtbDepartmentTransfer;
                grdAllJangedDeleteUtility.Refresh();
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
                            dgvAllJangedDeleteUtility.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvAllJangedDeleteUtility.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvAllJangedDeleteUtility.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvAllJangedDeleteUtility.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvAllJangedDeleteUtility.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvAllJangedDeleteUtility.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvAllJangedDeleteUtility.ExportToCsv(Filepath);
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


        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ObjPer.SetFormPer();
                if (ObjPer.AllowUpdate == false || ObjPer.AllowInsert == false)
                {
                    Global.Message(BLL.GlobalDec.gStrPermissionInsUpdMsg);
                    return;
                }
                btnDelete.Enabled = false;
                m_dtbDepartmentTransfer = (DataTable)grdAllJangedDeleteUtility.DataSource;
                if (m_dtbDepartmentTransfer.Rows.Count == 0)
                {
                    Global.Message("Atleast one row in Grid!!!");
                    btnDelete.Enabled = true;
                    return;
                }

                DialogResult result = MessageBox.Show("Do you want to Delete data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result != DialogResult.Yes)
                {
                    btnDelete.Enabled = true;
                    return;
                }

                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;

                panelProgress.Visible = true;
                backgroundWorker_DeptTransfer.RunWorkerAsync();

                btnDelete.Enabled = true;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
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
