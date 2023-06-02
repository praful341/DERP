using BLL;
using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Utility;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using DERP.Master.MFG;
using DevExpress.XtraEditors;
using DREP.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DREP.Transaction
{
    public partial class FrmMFGCutCreate : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        Control _NextEnteredControl;
        private List<Control> _tabControls;
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        MFGCutCreate objCutCreate;
        UserAuthentication objUserAuthentication;
        OpeningStock opstk;

        DataTable DtControlSettings;
        DataTable m_dtbCutCreate;
        DataTable m_dtbCurrencyType;
        DataTable m_dtbDetails;
        DataTable m_opDate;
        DataTable m_dtbMemoNo;
        DataTable m_dtbDemandNo;
        DataTable m_dtKapan;
        DataTable DTab_CutCreate;
        DataTable m_dtbSievecheck;
        DataTable m_dtbSubSievecheck;
        DataTable m_dtbAssortscheck;
        DataTable m_dtbSievedtl;
        DataTable m_dtbAssortsdtl;
        DataTable m_dtbSubSievedtl;
        DataTable m_dtbMemoData;


        int m_rough_cut_id;
        int m_srno;
        int m_update_srno;
        int flag;
        int IntRes;
        Int64 Dept_union_Id;

        int Add_Kapan;
        int m_numForm_id;

        decimal m_numcarat;
        decimal m_old_carat;
        decimal m_numSummRate;
        decimal m_numSummDetRate;

        bool m_blnadd;
        bool m_blnsave;


        #endregion

        #region Constructor
        public FrmMFGCutCreate()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            _NextEnteredControl = new Control();
            _tabControls = new List<Control>();

            objCutCreate = new MFGCutCreate();
            objUserAuthentication = new UserAuthentication();
            opstk = new OpeningStock();

            DtControlSettings = new DataTable();
            m_dtbCutCreate = new DataTable();
            m_dtbCurrencyType = new DataTable();
            m_dtbDetails = new DataTable();
            m_opDate = new DataTable();
            m_dtbMemoNo = new DataTable();
            m_dtbDemandNo = new DataTable();
            m_dtKapan = new DataTable();
            DTab_CutCreate = new DataTable();
            m_dtbSievecheck = new DataTable();
            m_dtbSubSievecheck = new DataTable();
            m_dtbAssortscheck = new DataTable();
            m_dtbSievedtl = new DataTable();
            m_dtbAssortsdtl = new DataTable();
            m_dtbSubSievedtl = new DataTable();
            m_dtbMemoData = new DataTable();

            m_rough_cut_id = 0;
            m_srno = 0;
            m_update_srno = 0;
            flag = 0;
            IntRes = 0;
            Add_Kapan = 0;
            m_numForm_id = 0;

            m_numcarat = 0;
            m_old_carat = 0;
            m_numSummRate = 0;

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
            if (Global.CheckDefault() == 0)
            {
                Global.Message("Please Check User Default Setting");
                this.Close();
                return;
            }
            Val.frmGenSet(this);
            AttachFormEvents();

            // for Dynamic Setting By Praful On 01022020

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

            // End for Dynamic Setting By Praful On 01022020

            this.Show();
        }

        private void AttachFormEvents()
        {
            objBOFormEvents.CurForm = this;
            objBOFormEvents.FormKeyPress = true;
            objBOFormEvents.FormKeyDown = true;
            objBOFormEvents.FormResize = true;
            objBOFormEvents.FormClosing = true;
            objBOFormEvents.ObjToDispose.Add(objCutCreate);
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

        #endregion

        #region Events

        private void FrmMFGCutCreate_Load(object sender, EventArgs e)
        {
            try
            {
                if (!LoadDefaults())
                {
                    btnAdd.Enabled = false;
                    btnClear.Enabled = false;
                    btnSave.Enabled = false;
                }
                else
                {
                    ClearDetails();
                    ttlbCutList.SelectedTabPage = tblCutdetail;
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
                if (chkIsUpdate.Checked == false)
                {
                    if (Add_Kapan == 0)
                    {
                        Add_Kapan = Add_Kapan + 1;
                        MFGCutCreateProperty oProperty = new MFGCutCreateProperty();
                        oProperty.lock_date = Val.DBDate(dtpCutDate.Text);
                        oProperty.lock_type_id = (int)GlobalDec.LOCKTYPE.CUT;
                        oProperty.lot_id = Val.ToInt64(lueKapanNo.EditValue);
                        DataTable DTab = objCutCreate.LockGetData(oProperty);
                        if (DTab.Rows.Count > 0)
                        {
                            Global.ErrorMessage("There is issue with Locking Kapan!" + DTab.Rows[0]["user_name"] + "," + DTab.Rows[0]["ip_address"] + "Please contact Administrator!");
                            Add_Kapan = 0;
                            return;
                        }
                        else
                        {
                            int IntReso = objCutCreate.LockSave(oProperty);
                        }
                    }
                }

                if (AddInGrid())
                {
                    dtpCutDate.Enabled = false;
                    lueKapanNo.Enabled = false;
                    lueEmployee.Enabled = false;
                    lueRoughCuttype.Focus();
                    txtCutNo.Text = string.Empty;
                    txtPcs.Text = string.Empty;
                    txtCarat.Text = string.Empty;
                    txtAmount.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
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

                List<ListError> lstError = new List<ListError>();
                Dictionary<Control, string> rtnCtrls = new Dictionary<Control, string>();
                rtnCtrls = Global.CheckCompulsoryControls(Val.ToInt(ObjPer.form_id), this);
                if (rtnCtrls.Count > 0)
                {
                    foreach (KeyValuePair<Control, string> entry in rtnCtrls)
                    {
                        if (entry.Key is DevExpress.XtraEditors.LookUpEdit || entry.Key is DevExpress.XtraEditors.DateEdit)
                        {
                            lstError.Add(new ListError(13, entry.Value));
                        }
                        else if (entry.Key is DevExpress.XtraEditors.TextEdit)
                        {
                            lstError.Add(new ListError(12, entry.Value));
                        }
                    }
                    rtnCtrls.First().Key.Focus();
                    BLL.General.ShowErrors(lstError);
                    Cursor.Current = Cursors.Arrow;
                    return;
                }
                //string Str = "";
                //if (Val.DBDate(BLL.GlobalDec.gStrServerDate) != Val.DBDate(dtpCutDate.Text))
                //{
                //    Str = GlobalDec.CheckLockIsOpenOrNot(Val.DBDate(dtpCutDate.Text), Val.DBTime(DateTime.Now.ToShortTimeString())); //Val.GetFullTime12());
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
                //        dtpCutDate.Enabled = true;
                //        dtpCutDate.Visible = true;
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
                DialogResult result = MessageBox.Show("Do you want to save data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result != DialogResult.Yes)
                {
                    btnSave.Enabled = true;
                    return;
                }
                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                panelProgress.Visible = true;
                backgroundWorker_CutCreate.RunWorkerAsync();

                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        private void btnClear_Click_1(object sender, EventArgs e)
        {
            try
            {
                ClearDetails();
                int IntReso_Del = objCutCreate.LockDelete((int)GlobalDec.LOCKTYPE.CUT, Val.ToInt64(lueKapanNo.EditValue), ""); //For Unlock Current Kapan
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
        private void txtCarat_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtAmount.Text = string.Format("{0:0.00}", Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text));
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        private void txtRate_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtAmount.Text = string.Format("{0:0.00}", Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text));
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        private void panelControl4_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(255, 191, 219, 255), 2);
            e.Graphics.DrawLine(pen, 0, 40, 1500, 40);
        }
        private void txtCutNo_Validated(object sender, EventArgs e)
        {
            try
            {
                DataTable dtRoughCut = new DataTable();
                dtRoughCut = objCutCreate.GetRoughCut(Val.ToString(txtCutNo.Text));
                if (dtRoughCut.Rows.Count > 0)
                {
                    Global.Confirm("Cut No Already Exist");
                    txtCutNo.Focus();
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        private void grdCutCreate_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F9)
                {
                    if (Global.Confirm("Are you sure delete selected row?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        dgvCutCreate.DeleteRow(dgvCutCreate.GetRowHandle(dgvCutCreate.FocusedRowHandle));
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        private void FrmMFGCutCreate_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                int IntReso_Del = objCutCreate.LockDelete((int)GlobalDec.LOCKTYPE.CUT, Val.ToInt64(lueKapanNo.EditValue), ""); //For Unlock Current Kapan
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        private void lueKapanNo_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (lueKapanNo.EditValue != System.DBNull.Value)
                {
                    if (m_dtKapan.Rows.Count > 0)
                    {
                        DataRow[] dr = m_dtKapan.Select("kapan_no ='" + Val.ToString(lueKapanNo.Text) + "'");
                        txtTotalPcs.Text = Val.ToString(dr[0]["balance_pcs"]);
                        txtTotalCarat.Text = Val.ToString(dr[0]["balance_carat"]);
                        txtSieveId.Text = Val.ToString(dr[0]["rough_sieve_id"]);
                        txtShadeId.Text = Val.ToString(dr[0]["rough_shade_id"]);
                        lblLotId.Text = Val.ToString(dr[0]["lot_id"]);
                        txtRate.Text = Val.ToDecimal(dr[0]["Rate"]).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        private void lueRoughCuttype_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmMfgRoughCutTypeMaster frmCutType = new FrmMfgRoughCutTypeMaster();
                frmCutType.ShowDialog();
                Global.LOOKUPRoughCutType(lueRoughCuttype);
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
        private void backgroundWorker_CutCreate_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.Default;
                Conn = new BeginTranConnection(true, false);
                MFGCutCreateProperty objMFGCutCreateProperty = new MFGCutCreateProperty();
                MFGCutCreate objMFGCutCreate = new MFGCutCreate();
                try
                {
                    IntRes = 0;
                    Int64 NewLotid = 0;
                    Int64 NewHistory_Union_Id = 0;
                    //Int64 NewKapanid = 0;
                    Int64 Lot_SrNo = 0;

                    if (Val.ToString(lblMode.Text) == "Add Mode")
                    {
                        DTab_CutCreate = (DataTable)grdCutCreate.DataSource;
                        for (int i = 0; i < DTab_CutCreate.Rows.Count; i++)
                        {
                            objMFGCutCreateProperty = new MFGCutCreateProperty();

                            objMFGCutCreateProperty.lot_id = Val.ToInt64(0);
                            objMFGCutCreateProperty.kapan_date = Val.DBDate(dtpCutDate.Text);
                            objMFGCutCreateProperty.company_id = Val.ToInt(GlobalDec.gEmployeeProperty.company_id);
                            objMFGCutCreateProperty.branch_id = Val.ToInt(GlobalDec.gEmployeeProperty.branch_id);
                            objMFGCutCreateProperty.location_id = Val.ToInt(GlobalDec.gEmployeeProperty.location_id);
                            objMFGCutCreateProperty.department_id = Val.ToInt(GlobalDec.gEmployeeProperty.department_id);
                            objMFGCutCreateProperty.rough_sieve_id = Val.ToInt(DTab_CutCreate.Rows[i]["rough_sieve_id"]);
                            objMFGCutCreateProperty.rough_shade_id = Val.ToInt(DTab_CutCreate.Rows[i]["rough_shade_id"]);

                            objMFGCutCreateProperty.pcs = Val.ToInt(DTab_CutCreate.Rows[i]["pcs"]);
                            objMFGCutCreateProperty.carat = Val.ToDecimal(DTab_CutCreate.Rows[i]["carat"]);
                            objMFGCutCreateProperty.rate = Val.ToDecimal(DTab_CutCreate.Rows[i]["rate"]);
                            objMFGCutCreateProperty.amount = Val.ToDecimal(DTab_CutCreate.Rows[i]["amount"]);
                            //objMFGCutCreateProperty = objCutCreate.Save_MfgStock(objMFGCutCreateProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                            //NewLotid = Val.ToInt64(objMFGCutCreateProperty.lot_id);

                            objMFGCutCreateProperty.rough_cut_id = Val.ToInt(0);
                            objMFGCutCreateProperty.rough_cut_date = Val.DBDate(dtpCutDate.Text);
                            objMFGCutCreateProperty.rough_cut_no = Val.ToString(DTab_CutCreate.Rows[i]["rough_cut_no"]);
                            objMFGCutCreateProperty.rough_cuttype_id = Val.ToInt(DTab_CutCreate.Rows[i]["rough_cuttype_id"]);
                            objMFGCutCreateProperty.kapan_id = Val.ToInt(lueKapanNo.EditValue);
                            objMFGCutCreateProperty.manager_id = Val.ToInt(DTab_CutCreate.Rows[i]["manager_id"]);
                            objMFGCutCreateProperty.employee_id = Val.ToInt(DTab_CutCreate.Rows[i]["employee_id"]);
                            objMFGCutCreateProperty.from_lot_id = Val.ToInt(lblLotId.Text);
                            objMFGCutCreateProperty.pcs = Val.ToInt(DTab_CutCreate.Rows[i]["pcs"]);
                            objMFGCutCreateProperty.carat = Val.ToDecimal(DTab_CutCreate.Rows[i]["carat"]);
                            objMFGCutCreateProperty.rate = Val.ToDecimal(DTab_CutCreate.Rows[i]["rate"]);
                            objMFGCutCreateProperty.amount = Val.ToDecimal(DTab_CutCreate.Rows[i]["amount"]);
                            objMFGCutCreateProperty.currency_id = Val.ToInt(GlobalDec.gEmployeeProperty.currency_id);
                            objMFGCutCreateProperty.remarks = Val.ToString(txtEntry.Text);
                            objMFGCutCreateProperty.special_remarks = Val.ToString(txtJKK.Text);
                            objMFGCutCreateProperty.client_remarks = Val.ToString(txtSaleRemark.Text);
                            objMFGCutCreateProperty.payment_remarks = Val.ToString(txtAccountRemark.Text);
                            objMFGCutCreateProperty.form_id = m_numForm_id;
                            objMFGCutCreateProperty.history_union_id = NewHistory_Union_Id;
                            objMFGCutCreateProperty.lot_srno = Lot_SrNo;

                            objMFGCutCreateProperty = objCutCreate.CutSave(objMFGCutCreateProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                            Int64 NewCutid = Val.ToInt64(objMFGCutCreateProperty.rough_cut_id);
                            objMFGCutCreateProperty.rough_cut_id = NewCutid;
                            objMFGCutCreateProperty.rough_lot_id = Val.ToInt(lblLotId.Text);
                            NewHistory_Union_Id = Val.ToInt64(objMFGCutCreateProperty.history_union_id);
                            objMFGCutCreateProperty.department_union_id = Dept_union_Id;
                            Lot_SrNo = Val.ToInt64(objMFGCutCreateProperty.lot_srno);

                            objMFGCutCreateProperty = objCutCreate.Save_RoughCutStock(objMFGCutCreateProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                            NewLotid = Val.ToInt64(objMFGCutCreateProperty.lot_id);
                            Dept_union_Id = Val.ToInt64(objMFGCutCreateProperty.department_union_id);
                            Lot_SrNo = Val.ToInt64(objMFGCutCreateProperty.lot_srno);

                            objMFGCutCreateProperty.rough_cut_date = Val.DBDate(dtpCutDate.Text);
                            objMFGCutCreateProperty.mixsplit_id = Val.ToInt(0);
                            objMFGCutCreateProperty.mix_type_id = Val.ToInt(2);
                            objMFGCutCreateProperty.from_lot_id = Val.ToInt(lblLotId.Text);
                            objMFGCutCreateProperty.to_lot_id = NewLotid;
                            objMFGCutCreateProperty.from_kapan_id = Val.ToInt(lueKapanNo.EditValue);
                            objMFGCutCreateProperty.to_kapan_id = Val.ToInt(lueKapanNo.EditValue);
                            objMFGCutCreateProperty.transaction_type_id = Val.ToInt(4);
                            objMFGCutCreateProperty.from_pcs = Val.ToInt(txtTotalPcs.Text);
                            objMFGCutCreateProperty.to_pcs = Val.ToInt(DTab_CutCreate.Rows[i]["pcs"]);
                            objMFGCutCreateProperty.from_carat = Val.ToDecimal(txtTotalCarat.Text);
                            objMFGCutCreateProperty.to_carat = Val.ToDecimal(DTab_CutCreate.Rows[i]["carat"]);
                            objMFGCutCreateProperty.lot_srno = Lot_SrNo;
                            IntRes = objCutCreate.Save_New(objMFGCutCreateProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                        }
                    }
                    else
                    {
                        //objMFGCutCreateProperty.rough_cut_id = Val.ToInt(lblMode.Tag);
                        //objMFGCutCreateProperty.rough_cut_date = Val.DBDate(dtpCutDate.Text);
                        //IntRes = objCutCreate.Update(objMFGCutCreateProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                        if (chkIsUpdate.Checked == true)
                        {
                            DTab_CutCreate = (DataTable)grdCutCreate.DataSource;
                            for (int i = 0; i < DTab_CutCreate.Rows.Count; i++)
                            {
                                //objMFGCutCreateProperty.rough_cut_id = Val.ToInt64(DTab_CutCreate.Rows[i]["rough_cut_id"]);
                                objMFGCutCreateProperty.rough_cut_id = Val.ToInt(lblMode.Tag);
                                objMFGCutCreateProperty.rough_cut_no = Val.ToString(DTab_CutCreate.Rows[i]["rough_cut_no"]);
                                objMFGCutCreateProperty.rough_cut_date = Val.DBDate(dtpCutDate.Text);
                                objMFGCutCreateProperty.carat = Val.ToDecimal(DTab_CutCreate.Rows[i]["carat"]);
                                objMFGCutCreateProperty.rate = Val.ToDecimal(DTab_CutCreate.Rows[i]["rate"]);
                                objMFGCutCreateProperty.amount = Val.ToDecimal(DTab_CutCreate.Rows[i]["amount"]);
                                objMFGCutCreateProperty.old_carat = m_old_carat;
                                objMFGCutCreateProperty.diff_carat = m_old_carat - Val.ToDecimal(DTab_CutCreate.Rows[i]["carat"]);
                                IntRes = objCutCreate.Update(objMFGCutCreateProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                                if (IntRes == -1)
                                {
                                    Global.Message("Already Issue This Cut No...So Don't Update Carat in this Cut No...");
                                    return;
                                }
                            }
                        }
                        else
                        {
                            Global.Message("Update Statemenent Not Checked...");
                            chkIsUpdate.Focus();
                            return;
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
                finally
                {
                    objMFGCutCreateProperty = null;
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
        private void backgroundWorker_CutCreate_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                panelProgress.Visible = false;
                if (IntRes > 0)
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Cut Create Data Save Successfully");
                        int IntReso_Del = objCutCreate.LockDelete((int)GlobalDec.LOCKTYPE.CUT, Val.ToInt64(lueKapanNo.EditValue), ""); //For Unlock Current Kapan
                        ClearDetails();
                        GetData();
                    }
                    else
                    {
                        Global.Confirm("Cut Create Data Update Successfully");
                        ClearDetails();
                        GetData();
                    }
                }
                else
                {
                    Global.Confirm("Error In Cut Create");
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
            GetData();
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
        private void txtPcs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
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
        private void btnDelete_Click(object sender, EventArgs e)
        {
            MFGCutCreateProperty objMFGCutCreateProperty = new MFGCutCreateProperty();
            MFGCutCreate objMFGCutCreate = new MFGCutCreate();

            try
            {
                if (m_dtbCutCreate.Rows.Count == 0)
                {
                    Global.Message("Please Select Records..");
                    return;
                }
                DialogResult result = MessageBox.Show("Do you want to Delete Cut data?", "Confirmation", MessageBoxButtons.YesNo);
                if (result != DialogResult.Yes)
                {
                    btnSave.Enabled = true;
                    return;
                }
                btnDelete.Enabled = false;
                Conn = new BeginTranConnection(true, false);

                DataTable DTab = (DataTable)grdCutCreate.DataSource;

                foreach (DataRow DRow in DTab.Rows)
                {
                    objMFGCutCreateProperty.rough_cut_id = Val.ToInt64(DRow["rough_cut_id"]);
                    objMFGCutCreateProperty.carat = Val.ToDecimal(DRow["carat"]);

                    objMFGCutCreateProperty = objMFGCutCreate.Cut_Delete(objMFGCutCreateProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                }

                Conn.Inter1.Commit();
                if (objMFGCutCreateProperty.remarks != "")
                {
                    Global.Message(objMFGCutCreateProperty.remarks);
                    btnDelete.Enabled = true;
                }
                else
                {
                    Global.Message("Data Deleted Successfully");
                    ClearDetails();
                    GetData();
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
                objMFGCutCreateProperty = null;
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

        #region "Grid Events" 
        private void dgvCutCreate_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                if (Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) > 0 && Val.ToDecimal(clmDetCarat.SummaryItem.SummaryValue) > 0)
                {
                    m_numSummDetRate = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) / Val.ToDecimal(clmDetCarat.SummaryItem.SummaryValue)), 2, MidpointRounding.AwayFromZero);
                }
                else
                {
                    m_numSummDetRate = 0;
                }
                if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == "rate")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                        e.TotalValue = m_numSummDetRate;
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        private void dgvSaleInvoice_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                objCutCreate = new MFGCutCreate();
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvCutList.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt32(Drow["rough_cut_id"]);

                        dtpCutDate.Text = Val.DBDate(Val.ToString(Drow["cut_date"]));
                        //lueKapanNo.EditValue = Val.ToInt64(Drow["kapan_id"]);
                        //txtEntry.Text = Val.ToString(Drow["remarks"]);
                        //txtJKK.Text = Val.ToString(Drow["special_remarks"]);
                        //txtAccountRemark.Text = Val.ToString(Drow["payment_remarks"]);
                        //txtSaleRemark.Text = Val.ToString(Drow["client_remarks"]);
                        //m_dtbCutCreate = dgvCutList.GetFocusedDataRow(e.RowHandle);
                        m_old_carat = Val.ToDecimal(Drow["carat"]);
                        m_dtbCutCreate = objCutCreate.GetDataDetails(Val.ToInt(lblMode.Tag));

                        grdCutCreate.DataSource = m_dtbCutCreate;
                        ttlbCutList.SelectedTabPage = tblCutdetail;
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void dgvCutCreate_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvCutCreate.GetDataRow(e.RowHandle);
                        btnAdd.Text = "&Update";
                        txtCutNo.Text = Val.ToString(Drow["rough_cut_no"]);
                        lueManager.EditValue = Val.ToInt64(Drow["manager_id"]);
                        lueEmployee.EditValue = Val.ToInt64(Drow["employee_id"]);
                        lueRoughCuttype.EditValue = Val.ToInt64(Drow["rough_cuttype_id"]);
                        txtPcs.Text = Val.ToString(Drow["pcs"]);
                        txtCarat.Text = Val.ToString(Drow["carat"]);
                        txtRate.Text = Val.ToString(Drow["rate"]);
                        txtAmount.Text = Val.ToString(Drow["amount"]);
                        m_numcarat = Val.ToDecimal(Drow["carat"]);
                        m_rough_cut_id = Val.ToInt(Drow["rough_cut_id"]);
                        m_update_srno = Val.ToInt(Drow["sr_no"]);
                        flag = 1;
                        m_old_carat = Val.ToDecimal(Drow["carat"]);
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void dgvSaleInvoice_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
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

            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        #endregion

        #endregion

        #region Functions
        private bool LoadDefaults()
        {
            bool blnReturn = true;
            try
            {
                Global.LOOKUPManager(lueManager);
                Global.LOOKUPEmp(lueEmployee);
                Global.LOOKUPRoughCutType(lueRoughCuttype);
                GetKapan();
                lueKapanNo.Properties.DataSource = m_dtKapan;
                lueKapanNo.Properties.ValueMember = "kapan_id";
                lueKapanNo.Properties.DisplayMember = "kapan_no";

                dtpCutDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpCutDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpCutDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpCutDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpCutDate.EditValue = DateTime.Now;

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
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            finally
            {
            }
            return blnReturn;
        }
        private bool AddInGrid()
        {
            bool blnReturn = true;
            try
            {
                if (chkIsUpdate.Checked == false)
                {
                    m_blnadd = true;
                    m_blnsave = false;
                    if (!ValidateDetails())
                    {
                        m_blnadd = false;
                        blnReturn = false;
                        return blnReturn;
                    }
                    flag = 0;
                    m_old_carat = 0;
                    objCutCreate = new MFGCutCreate();
                    DataTable p_dtbDetail = new DataTable();

                    if (btnAdd.Text == "&Add")
                    {
                        DataRow[] dr = m_dtbCutCreate.Select("rough_cut_no = '" + Val.ToString(txtCutNo.Text) + "'");

                        if (dr.Count() == 1)
                        {
                            Global.Message("Record already exists in grid", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCutNo.Focus();
                            blnReturn = false;
                            return blnReturn;
                        }
                        DataRow drwNew = m_dtbCutCreate.NewRow();
                        decimal numCarat = Val.ToDecimal(txtCarat.Text);
                        decimal numRate = Val.ToDecimal(txtRate.Text);
                        decimal numAmount = Val.ToDecimal(txtAmount.Text);
                        int numPcs = Val.ToInt(txtPcs.Text);
                        drwNew["rough_cut_id"] = Val.ToInt(0);
                        drwNew["rough_cut_no"] = Val.ToString(txtCutNo.Text);
                        drwNew["manager_id"] = Val.ToInt(lueManager.EditValue);
                        drwNew["manager_name"] = Val.ToString(lueManager.Text);
                        drwNew["employee_id"] = Val.ToInt(lueManager.EditValue);
                        drwNew["employee_name"] = Val.ToString(lueManager.Text);
                        drwNew["rough_cuttype_id"] = Val.ToInt(lueRoughCuttype.EditValue);
                        drwNew["rough_cuttype_name"] = Val.ToString(lueRoughCuttype.Text);
                        drwNew["pcs"] = numPcs;
                        drwNew["carat"] = numCarat;
                        drwNew["rate"] = Val.ToDecimal(txtRate.Text);
                        drwNew["amount"] = Math.Round(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text), 3);
                        drwNew["rough_sieve_id"] = Val.ToInt(txtSieveId.Text);
                        drwNew["rough_shade_id"] = Val.ToInt(txtShadeId.Text);
                        m_srno = m_srno + 1;
                        drwNew["sr_no"] = Val.ToInt(m_srno);

                        m_dtbCutCreate.Rows.Add(drwNew);
                        dgvCutCreate.MoveLast();
                    }
                    else if (btnAdd.Text == "&Update")
                    {
                        objCutCreate = new MFGCutCreate();

                        if (m_dtbCutCreate.Select("rough_cut_no ='" + Val.ToString(txtCutNo.Text) + "'").Length > 0)
                        {
                            for (int i = 0; i < m_dtbCutCreate.Rows.Count; i++)
                            {
                                if (m_dtbCutCreate.Select("rough_cut_id ='" + m_rough_cut_id + "' AND sr_no = '" + m_update_srno + "'").Length > 0)
                                {
                                    if (m_dtbCutCreate.Rows[m_update_srno - 1]["rough_cut_id"].ToString() == m_rough_cut_id.ToString())
                                    {
                                        m_dtbCutCreate.Rows[m_update_srno - 1]["rough_cut_no"] = Val.ToString(txtCutNo.Text);
                                        m_dtbCutCreate.Rows[m_update_srno - 1]["manager_id"] = Val.ToInt(lueManager.EditValue).ToString();
                                        m_dtbCutCreate.Rows[m_update_srno - 1]["manager_name"] = Val.ToString(lueManager.Text).ToString();
                                        m_dtbCutCreate.Rows[m_update_srno - 1]["employee_id"] = Val.ToInt(lueEmployee.EditValue).ToString();
                                        m_dtbCutCreate.Rows[m_update_srno - 1]["employee_name"] = Val.ToString(lueEmployee.Text).ToString();
                                        m_dtbCutCreate.Rows[m_update_srno - 1]["rough_cuttype_id"] = Val.ToInt(lueRoughCuttype.EditValue).ToString();
                                        m_dtbCutCreate.Rows[m_update_srno - 1]["rough_cuttype_name"] = Val.ToString(lueRoughCuttype.Text).ToString();
                                        m_dtbCutCreate.Rows[m_update_srno - 1]["carat"] = Val.ToDecimal(txtCarat.Text).ToString();
                                        m_dtbCutCreate.Rows[m_update_srno - 1]["pcs"] = Val.ToInt(txtPcs.Text);
                                        m_dtbCutCreate.Rows[m_update_srno - 1]["rate"] = Val.ToDecimal(txtRate.Text);
                                        m_dtbCutCreate.Rows[m_update_srno - 1]["amount"] = Math.Round(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text), 3);
                                        break;
                                    }
                                }
                            }
                            btnAdd.Text = "&Add";
                        }
                        else
                        {
                            for (int i = 0; i < m_dtbCutCreate.Rows.Count; i++)
                            {
                                if (m_dtbCutCreate.Select("rough_cut_id ='" + m_rough_cut_id + "' AND sr_no = '" + m_update_srno + "'").Length > 0)
                                {
                                    if (m_dtbCutCreate.Rows[m_update_srno - 1]["rough_cut_id"].ToString() == m_rough_cut_id.ToString())
                                    {
                                        m_dtbCutCreate.Rows[m_update_srno - 1]["rough_cut_no"] = Val.ToString(txtCutNo.Text);
                                        m_dtbCutCreate.Rows[m_update_srno - 1]["carat"] = Val.ToDecimal(txtCarat.Text).ToString();
                                        m_dtbCutCreate.Rows[m_update_srno - 1]["pcs"] = Val.ToInt(txtPcs.Text);
                                        m_dtbCutCreate.Rows[m_update_srno - 1]["rate"] = Val.ToDecimal(txtRate.Text);

                                        m_dtbCutCreate.Rows[m_update_srno - 1]["amount"] = Math.Round(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text), 3);
                                    }
                                }
                            }
                            btnAdd.Text = "&Add";
                        }
                        dgvCutCreate.MoveLast();
                    }
                }
                else
                {
                    if (btnAdd.Text == "&Update")
                    {
                        objCutCreate = new MFGCutCreate();

                        if (m_dtbCutCreate.Select("rough_cut_no ='" + Val.ToString(txtCutNo.Text) + "'").Length > 0)
                        {
                            for (int i = 0; i < m_dtbCutCreate.Rows.Count; i++)
                            {
                                if (m_dtbCutCreate.Select("rough_cut_id ='" + m_rough_cut_id + "' AND sr_no = '" + m_update_srno + "'").Length > 0)
                                {
                                    if (m_dtbCutCreate.Rows[m_update_srno - 1]["rough_cut_id"].ToString() == m_rough_cut_id.ToString())
                                    {
                                        m_dtbCutCreate.Rows[m_update_srno - 1]["rough_cut_no"] = Val.ToString(txtCutNo.Text);
                                        m_dtbCutCreate.Rows[m_update_srno - 1]["manager_id"] = Val.ToInt(lueManager.EditValue).ToString();
                                        m_dtbCutCreate.Rows[m_update_srno - 1]["manager_name"] = Val.ToString(lueManager.Text).ToString();
                                        m_dtbCutCreate.Rows[m_update_srno - 1]["employee_id"] = Val.ToInt(lueEmployee.EditValue).ToString();
                                        m_dtbCutCreate.Rows[m_update_srno - 1]["employee_name"] = Val.ToString(lueEmployee.Text).ToString();
                                        m_dtbCutCreate.Rows[m_update_srno - 1]["rough_cuttype_id"] = Val.ToInt(lueRoughCuttype.EditValue).ToString();
                                        m_dtbCutCreate.Rows[m_update_srno - 1]["rough_cuttype_name"] = Val.ToString(lueRoughCuttype.Text).ToString();
                                        m_dtbCutCreate.Rows[m_update_srno - 1]["carat"] = Val.ToDecimal(txtCarat.Text).ToString();
                                        m_dtbCutCreate.Rows[m_update_srno - 1]["pcs"] = Val.ToInt(txtPcs.Text);
                                        m_dtbCutCreate.Rows[m_update_srno - 1]["rate"] = Val.ToDecimal(txtRate.Text);
                                        m_dtbCutCreate.Rows[m_update_srno - 1]["amount"] = Math.Round(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text), 3);
                                        break;
                                    }
                                }
                            }
                            btnAdd.Text = "&Add";
                        }
                        else
                        {
                            for (int i = 0; i < m_dtbCutCreate.Rows.Count; i++)
                            {
                                if (m_dtbCutCreate.Select("rough_cut_id ='" + m_rough_cut_id + "' AND sr_no = '" + m_update_srno + "'").Length > 0)
                                {
                                    if (m_dtbCutCreate.Rows[m_update_srno - 1]["rough_cut_id"].ToString() == m_rough_cut_id.ToString())
                                    {
                                        m_dtbCutCreate.Rows[m_update_srno - 1]["rough_cut_no"] = Val.ToString(txtCutNo.Text);
                                        m_dtbCutCreate.Rows[m_update_srno - 1]["carat"] = Val.ToDecimal(txtCarat.Text).ToString();
                                        m_dtbCutCreate.Rows[m_update_srno - 1]["pcs"] = Val.ToInt(txtPcs.Text);
                                        m_dtbCutCreate.Rows[m_update_srno - 1]["rate"] = Val.ToDecimal(txtRate.Text);

                                        m_dtbCutCreate.Rows[m_update_srno - 1]["amount"] = Math.Round(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text), 3);
                                    }
                                }
                            }
                            btnAdd.Text = "&Add";
                        }
                        dgvCutCreate.MoveLast();
                    }
                }
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
                    if (m_dtbCutCreate.Rows.Count == 0)
                    {
                        lstError.Add(new ListError(22, "Record"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                        }
                    }
                    if (dgvCutCreate == null)
                    {
                        lstError.Add(new ListError(22, "Record"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                        }
                    }
                    var result = DateTime.Compare(Convert.ToDateTime(dtpCutDate.Text), DateTime.Today);
                    if (result > 0)
                    {
                        lstError.Add(new ListError(5, " Invoice Date Not Be Greater Than Today Date"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            dtpCutDate.Focus();
                        }
                    }
                }

                if (m_blnadd)
                {
                    if (Val.ToDouble(txtCarat.Text) == 0)
                    {
                        lstError.Add(new ListError(12, "Carat"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtCarat.Focus();
                        }
                    }
                    if (Val.ToString(txtCutNo.Text) == string.Empty)
                    {
                        lstError.Add(new ListError(12, "Cut No"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtCutNo.Focus();
                        }
                    }
                    if (!objCutCreate.ISExists(txtCutNo.Text, Val.ToInt64(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
                    {
                        lstError.Add(new ListError(5, "Cut No Already Exists in Master"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtCutNo.Focus();
                        }

                    }
                    if (Val.ToDouble(txtRate.Text) == 0)
                    {
                        lstError.Add(new ListError(12, "Rate"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtRate.Focus();
                        }
                    }

                    if (Val.ToDouble(txtAmount.Text) == 0)
                    {
                        lstError.Add(new ListError(12, "Amount"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtAmount.Focus();
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
                    if (lueRoughCuttype.Text == "")
                    {
                        lstError.Add(new ListError(13, "Rough Cut Type"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueRoughCuttype.Focus();
                        }
                    }
                    if (Val.ToDecimal(txtTotalCarat.Text) < Val.ToDecimal(txtCarat.Text))
                    {
                        lstError.Add(new ListError(5, "Carat not greater than total carat"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtCarat.Focus();
                        }
                    }
                    if (Val.ToDecimal(txtTotalCarat.Text) < (Val.ToDecimal(dgvCutCreate.Columns["carat"].SummaryText) + Val.ToDecimal(txtCarat.Text)) && flag == 0)
                    {
                        lstError.Add(new ListError(5, "Entry Carat not greater than total carat"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtCarat.Focus();
                        }
                    }
                    if (Val.ToDecimal(txtTotalCarat.Text) < (Val.ToDecimal(dgvCutCreate.Columns["carat"].SummaryText) + Val.ToDecimal(txtCarat.Text) - Val.ToDecimal(m_old_carat)) && flag == 1)
                    {
                        lstError.Add(new ListError(5, "Entry Carat not greater than total carat"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtCarat.Focus();
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
                if (!GenerateCutCreateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }
                lblMode.Tag = 0;
                lblMode.Text = "Add Mode";
                dtpCutDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpCutDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpCutDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpCutDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpCutDate.EditValue = DateTime.Now;

                lueManager.EditValue = System.DBNull.Value;
                lueEmployee.EditValue = System.DBNull.Value;
                lueRoughCuttype.EditValue = System.DBNull.Value;

                txtCutNo.Text = "";
                txtTotalPcs.Text = "0";
                txtTotalCarat.Text = "0";
                lueKapan.EditValue = System.DBNull.Value;
                lueKapanNo.EditValue = System.DBNull.Value;
                txtShadeId.Text = string.Empty;
                txtSieveId.Text = string.Empty;
                txtPcs.Text = string.Empty;
                txtCarat.Text = string.Empty;
                txtRate.Text = string.Empty;
                txtAmount.Text = string.Empty;
                txtEntry.Text = string.Empty;
                txtJKK.Text = string.Empty;
                txtAccountRemark.Text = string.Empty;
                txtSaleRemark.Text = string.Empty;
                m_srno = 0;
                chkIsUpdate.Checked = false;
                GetKapan();
                lueKapanNo.Enabled = true;
                dtpCutDate.Enabled = true;
                lueEmployee.Enabled = true;
                txtPassword.Text = "";
                btnDelete.Enabled = true;
                btnDelete.Visible = false;
                Add_Kapan = 0;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
        }
        private void GetKapan()
        {
            try
            {
                m_dtKapan = objCutCreate.GetKapan();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
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
                            dgvCutCreate.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvCutCreate.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvCutCreate.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvCutCreate.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvCutCreate.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvCutCreate.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvCutCreate.ExportToCsv(Filepath);
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
        public void GetData()
        {
            DataTable dtab = new DataTable();
            dtab = objCutCreate.GetSearchData(Val.ToString(dtpFromDate.Text), Val.ToString(dtpToDate.Text), Val.ToInt(lueKapan.EditValue));

            grdCutList.DataSource = (DataTable)dtab;
        }
        private bool GenerateCutCreateDetails()
        {
            bool blnReturn = true;
            try
            {
                if (m_dtbCutCreate.Rows.Count > 0)
                    m_dtbCutCreate.Rows.Clear();

                m_dtbCutCreate = new DataTable();

                m_dtbCutCreate.Columns.Add("sr_no", typeof(int));
                m_dtbCutCreate.Columns.Add("rough_cut_id", typeof(int));
                m_dtbCutCreate.Columns.Add("rough_cut_no", typeof(string));
                m_dtbCutCreate.Columns.Add("manager_id", typeof(int));
                m_dtbCutCreate.Columns.Add("manager_name", typeof(string));
                m_dtbCutCreate.Columns.Add("employee_id", typeof(int));
                m_dtbCutCreate.Columns.Add("employee_name", typeof(string));
                m_dtbCutCreate.Columns.Add("rough_cuttype_id", typeof(int));
                m_dtbCutCreate.Columns.Add("rough_cuttype_name", typeof(string));
                m_dtbCutCreate.Columns.Add("pcs", typeof(int)).DefaultValue = 0;
                m_dtbCutCreate.Columns.Add("carat", typeof(decimal)).DefaultValue = 0;
                m_dtbCutCreate.Columns.Add("rate", typeof(decimal)).DefaultValue = 0;
                m_dtbCutCreate.Columns.Add("amount", typeof(decimal)).DefaultValue = 0;
                m_dtbCutCreate.Columns.Add("rough_sieve_id", typeof(int));
                m_dtbCutCreate.Columns.Add("rough_shade_id", typeof(int));
                m_dtbCutCreate.Columns.Add("old_carat", typeof(decimal)).DefaultValue = 0;

                grdCutCreate.DataSource = m_dtbCutCreate;
                grdCutCreate.Refresh();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
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
    }
}