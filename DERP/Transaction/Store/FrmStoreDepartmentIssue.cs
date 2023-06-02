using BLL;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Master.Store;
using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Transaction.Store;
using BLL.FunctionClasses.Utility;
using BLL.PropertyClasses.Transaction;
using BLL.PropertyClasses.Transaction.Store;
using DERP.Class;
using DERP.Master.Store;
using DevExpress.XtraEditors;
using OfficeOpenXml;
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
    public partial class FrmStoreDepartmentIssue : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        Control _NextEnteredControl;
        private List<Control> _tabControls;
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        StorePurchase objPurchase;
        StoreDepartmentIssue objDepartmentIssue;
        FinancialYearMaster objFinYear;
        UserAuthentication objUserAuthentication;
        RateMaster objRate;
        OpeningStock opstk;
        MfgSubItemMaster objSubItem;

        DataTable DtControlSettings;
        DataTable m_dtbDeptIssueDetails;
        DataTable m_dtbDetails;
        DataTable m_opDate;
        DataTable m_dtbItem;
        Int64 Int_Dept_Issue_No = 0;

        Int64 m_Dept_Issue_detail_id;
        int m_srno;
        int m_update_srno;
        int IntRes;
        int m_FinYear;
        int m_numForm_id;
        decimal m_numTotalQty;
        decimal m_numTotalAmount;
        decimal m_numSummRate;
        bool m_blnadd;
        bool m_blnsave;
        StoreSales objSales;

        #endregion

        #region Constructor
        public FrmStoreDepartmentIssue()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            _NextEnteredControl = new Control();
            _tabControls = new List<Control>();

            objPurchase = new StorePurchase();
            objDepartmentIssue = new StoreDepartmentIssue();
            objFinYear = new FinancialYearMaster();
            objUserAuthentication = new UserAuthentication();
            objRate = new RateMaster();
            opstk = new OpeningStock();

            DtControlSettings = new DataTable();
            m_dtbDeptIssueDetails = new DataTable();
            m_dtbDetails = new DataTable();
            m_opDate = new DataTable();
            m_dtbItem = new DataTable();
            objSubItem = new MfgSubItemMaster();

            m_Dept_Issue_detail_id = 0;
            m_srno = 0;
            m_update_srno = 0;
            IntRes = 0;
            m_FinYear = 0;
            m_numForm_id = 0;
            m_numTotalQty = 0;
            m_numTotalAmount = 0;
            m_blnadd = new bool();
            m_blnsave = new bool();
            objSales = new StoreSales();
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
        private void AttachFormEvents()
        {
            objBOFormEvents.CurForm = this;
            objBOFormEvents.FormKeyPress = true;
            objBOFormEvents.FormKeyDown = true;
            objBOFormEvents.FormResize = true;
            objBOFormEvents.FormClosing = true;
            objBOFormEvents.ObjToDispose.Add(objDepartmentIssue);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);

        }

        #endregion

        #region Events
        private void FrmMFGSales_Load(object sender, EventArgs e)
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
                    ttlbMFGSales.SelectedTabPage = tblSaledetail;
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
            if (AddInGrid())
            {
                lueItem.EditValue = DBNull.Value;
                lueSubItem.EditValue = DBNull.Value;
                txtQty.Text = string.Empty;
                //txtUnit.Text = string.Empty;
                txtRate.Text = string.Empty;
                txtAmount.Text = string.Empty;
                txtRemark.Text = string.Empty;
                CmbItemCondition.SelectedIndex = 0;
                //lueAssortName_KeyUp(null, null);

                //DialogResult result = MessageBox.Show("Add More Entry?", "Confirmation", MessageBoxButtons.YesNo);
                //if (result != DialogResult.Yes)
                //{
                //    btnSave_Click(null, null);
                //}
                //else
                //{
                lueItem.Focus();
                lueItem.ShowPopup();
                //}
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
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

            btnSave.Enabled = false;

            m_blnsave = true;
            m_blnadd = false;
            if (!ValidateDetails())
            {
                m_blnsave = false;
                btnSave.Enabled = true;
                return;
            }

            DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
            panelProgress.Visible = true;
            backgroundWorker_StoreDepartmentIssue.RunWorkerAsync();

            btnSave.Enabled = true;
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            ObjPer.SetFormPer();
            if (ObjPer.AllowPrint == false)
            {
                Global.Message(BLL.GlobalDec.gStrPermissionPrintMsg);
                return;
            }
            MFG_PurchaseProperty MFG_PurchaseProperty = new MFG_PurchaseProperty();
            MFG_PurchaseProperty.date = Val.DBDate(dtpIssueDate.Text);
            //MFG_PurchaseProperty.invoice_no = Val.ToString(txtSBillNo.Text);
            MFG_PurchaseProperty.purchase_id = Val.ToInt32(lblMode.Tag);

            DataTable dtpur = new DataTable();
        }
        private void btnClear_Click_1(object sender, EventArgs e)
        {
            ClearDetails();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!PopulateDetails())
                return;
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            ObjPer.SetFormPer();
            if (ObjPer.AllowDelete == false)
            {
                Global.Message(BLL.GlobalDec.gStrPermissionDelMsg);
                return;
            }
            btnDelete.Enabled = false;
            if (DeleteDetail())
            {
                ClearDetails();
                PopulateDetails();
            }
            btnDelete.Enabled = true;
        }
        private void panelControl4_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(255, 191, 219, 255), 2);
            e.Graphics.DrawLine(pen, 0, 80, 1500, 80);
        }
        private void txtRate_EditValueChanged(object sender, EventArgs e)
        {
            txtAmount.Text = string.Format("{0:0.00}", Val.ToDecimal(txtQty.Text) * Val.ToDecimal(txtRate.Text));
        }
        private void dtpSalesDate_EditValueChanged(object sender, EventArgs e)
        {
            DataTable dtFinYear = new DataTable();
            dtFinYear = objFinYear.GetData();
            foreach (DataRow drw in dtFinYear.Rows)
            {
                if (dtpIssueDate.Text != "")
                {
                    var result = DateTime.Compare(Convert.ToDateTime(dtpIssueDate.Text), Convert.ToDateTime(drw["start_date"]));
                    var result2 = DateTime.Compare(Convert.ToDateTime(dtpIssueDate.Text), Convert.ToDateTime(drw["end_date"]));
                    if (result > 0 && result2 < 0)
                    {
                        m_FinYear = Val.ToInt(drw["fin_year_id"]);
                    }
                }
            }
        }
        private void RepBtnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (Global.Confirm("Are you sure delete selected row?", "STORE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                Store_PurchaseProperty objPurchaseProperty = new Store_PurchaseProperty();
                int IntRes = 0;
                Int64 DeptIssueDetailID = Val.ToInt64(dgvDeptIssueDetails.GetFocusedRowCellValue("dept_issuedetail_id").ToString());
                Int64 DeptIssueID = Val.ToInt64(dgvDeptIssueDetails.GetFocusedRowCellValue("dept_issue_id").ToString());
                objPurchaseProperty.purchase_detail_id = Val.ToInt64(DeptIssueDetailID);
                objPurchaseProperty.purchase_id = Val.ToInt64(DeptIssueID);

                if (DeptIssueDetailID == 0)
                {
                    dgvDeptIssueDetails.DeleteRow(dgvDeptIssueDetails.GetRowHandle(dgvDeptIssueDetails.FocusedRowHandle));
                    m_dtbDeptIssueDetails.AcceptChanges();
                }
                else
                {
                    IntRes = objPurchase.Delete_PurchaseDetail(objPurchaseProperty, "DEPARTMENT ISSUE");
                    dgvDeptIssueDetails.DeleteRow(dgvDeptIssueDetails.GetRowHandle(dgvDeptIssueDetails.FocusedRowHandle));
                    m_dtbDeptIssueDetails.AcceptChanges();
                }

                if (IntRes == -1)
                {
                    Global.Confirm("Error in Detail Deleted Data.");
                    objPurchaseProperty = null;
                }
                else
                {
                    Global.Confirm("Detail Deleted successfully...");
                    objPurchaseProperty = null;
                }
            }
        }
        private void lueItem_EditValueChanged(object sender, EventArgs e)
        {
            DataTable dtSubItem = new DataTable();
            if (lueItem.Text != "")
            {
                objSubItem = new MfgSubItemMaster();
                dtSubItem = objSubItem.ItemWise_Sub_GetData(Val.ToInt64(lueItem.EditValue));

                lueSubItem.Properties.DataSource = dtSubItem;
                lueSubItem.Properties.ValueMember = "sub_item_id";
                lueSubItem.Properties.DisplayMember = "sub_item_name";
            }
        }
        private void txtQty_EditValueChanged(object sender, EventArgs e)
        {
            txtAmount.Text = string.Format("{0:0.00}", Val.ToDecimal(txtQty.Text) * Val.ToDecimal(txtRate.Text));
        }
        private void backgroundWorker_StoreDepartmentIssue_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.Default;
                Conn = new BeginTranConnection(true, false);

                Store_DepartmentIssueProperty objDeptIssueProperty = new Store_DepartmentIssueProperty();
                StoreDepartmentIssue objDeptIssue = new StoreDepartmentIssue();
                try
                {
                    IntRes = 0;

                    objDeptIssueProperty.dept_issue_id = Val.ToInt(lblMode.Tag);

                    if (Int_Dept_Issue_No == 0)
                    {
                        objDeptIssueProperty.dept_issue_no = Int_Dept_Issue_No;
                    }
                    else
                    {
                        objDeptIssueProperty.dept_issue_no = Val.ToInt64(txtDeptIssueNo.Text);
                    }
                    objDeptIssueProperty.manager_id = Val.ToInt64(lueToManager.EditValue);
                    //objDeptIssueProperty.from_company_id = Val.ToInt64(GlobalDec.gEmployeeProperty.company_id);
                    //objDeptIssueProperty.from_branch_id = Val.ToInt64(GlobalDec.gEmployeeProperty.branch_id);
                    //objDeptIssueProperty.from_location_id = Val.ToInt64(GlobalDec.gEmployeeProperty.location_id);
                    //objDeptIssueProperty.from_department_id = Val.ToInt64(GlobalDec.gEmployeeProperty.department_id);
                    objDeptIssueProperty.to_company_id = Val.ToInt64(GlobalDec.gEmployeeProperty.company_id);
                    objDeptIssueProperty.to_branch_id = Val.ToInt64(lueToBranch.EditValue);
                    objDeptIssueProperty.to_location_id = Val.ToInt64(GlobalDec.gEmployeeProperty.location_id);
                    objDeptIssueProperty.to_department_id = Val.ToInt64(lueToDepartent.EditValue);
                    objDeptIssueProperty.to_division_id = Val.ToInt64(LueToDivision.EditValue);
                    objDeptIssueProperty.issue_date = Val.DBDate(dtpIssueDate.Text);
                    objDeptIssueProperty.bill_date = Val.DBDate(dtpBillDate.Text);
                    objDeptIssueProperty.total_qty = Math.Round(Val.ToDecimal(clmQty.SummaryItem.SummaryValue), 2);
                    objDeptIssueProperty.total_rate = Math.Round(Val.ToDecimal(clmRSrate.SummaryItem.SummaryValue), 2);
                    objDeptIssueProperty.total_amount = Math.Round(Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue), 2);
                    objDeptIssueProperty = objDeptIssue.Save(objDeptIssueProperty, DLL.GlobalDec.EnumTran.Start, Conn);
                    Int64 NewDeptIssueid = Val.ToInt64(objDeptIssueProperty.dept_issue_id);

                    int IntCounter = 0;
                    int Count = 0;
                    int TotalCount = m_dtbDeptIssueDetails.Rows.Count;

                    foreach (DataRow drw in m_dtbDeptIssueDetails.Rows)
                    {
                        objDeptIssueProperty = new Store_DepartmentIssueProperty();
                        objDeptIssueProperty.dept_issue_id = Val.ToInt64(NewDeptIssueid);
                        objDeptIssueProperty.dept_issuedetail_id = Val.ToInt64(drw["dept_issuedetail_id"]);
                        objDeptIssueProperty.item_id = Val.ToInt64(drw["item_id"]);
                        objDeptIssueProperty.sub_item_id = Val.ToInt64(drw["sub_item_id"]);
                        objDeptIssueProperty.remarks = Val.ToString(drw["remarks"]);
                        objDeptIssueProperty.qty = Val.ToDecimal(drw["qty"]);
                        objDeptIssueProperty.rate = Val.ToDecimal(drw["rate"]);
                        objDeptIssueProperty.amount = Val.ToDecimal(drw["amount"]);
                        objDeptIssueProperty.item_condition = Val.ToString(drw["item_condition"]);

                        objDeptIssueProperty.from_department_id = Val.ToInt64(GlobalDec.gEmployeeProperty.department_id);
                        objDeptIssueProperty.from_department_id = Val.ToInt64(GlobalDec.gEmployeeProperty.department_id);
                        objDeptIssueProperty.to_department_id = Val.ToInt64(lueToDepartent.EditValue);
                        objDeptIssueProperty.manager_id = Val.ToInt64(lueToManager.EditValue);

                        IntRes = objDeptIssue.Save_PurchaseDetail(objDeptIssueProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

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
                finally
                {
                    objDeptIssueProperty = null;
                }
            }
            catch (Exception ex)
            {
                IntRes = -1;
                Conn.Inter1.Rollback();
                Conn = null;
                General.ShowErrors(ex.ToString());
                if (ex.InnerException != null)
                {
                    Global.Message(ex.InnerException.ToString());
                }
            }
        }
        private void backgroundWorker_StoreDepartmentIssue_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                panelProgress.Visible = false;
                if (IntRes > 0)
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Department Issue Entry Data Save Successfully");
                        ClearDetails();
                        PopulateDetails();
                    }
                    else
                    {
                        Global.Confirm("Department Issue Entry Data Update Successfully");
                        ClearDetails();
                        PopulateDetails();
                    }
                }
                else
                {
                    Global.Confirm("Error In Department Issue");
                    txtDeptIssueNo.Focus();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }
        private void lueToManager_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //if (e.Button.Index == 1)
            //{
            //    FrmStoreManagerMaster frmManager = new FrmStoreManagerMaster();
            //    frmManager.ShowDialog();
            //    Global.LOOKUPStoreManager(lueToManager);
            //}
        }
        private void lueToDepartent_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmStoreDepartmentMaster frmStoreDepartment = new FrmStoreDepartmentMaster();
                frmStoreDepartment.ShowDialog();
                Global.LOOKUPStoreDepartment(lueToDepartent);
            }
        }
        private void lueItem_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmMfgItemMaster frmMfgItem = new FrmMfgItemMaster();
                frmMfgItem.ShowDialog();
                Global.LOOKUPItem(lueItem);
            }
        }
        private void lueSubItem_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmMfgSubItemMaster frmMfgSubItem = new FrmMfgSubItemMaster();
                frmMfgSubItem.ShowDialog();
                Global.LOOKUPSubItem(lueSubItem);
            }
        }
        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
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

        #region "Grid Events" 
        private void dgvMFGSalesDetails_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                m_numTotalQty = Val.ToDecimal(clmQty.SummaryItem.SummaryValue);

                if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == "rate")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                        m_numTotalAmount = 0;
                    else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                        m_numTotalAmount += (Val.ToDecimal(e.GetValue("qty")) * Val.ToDecimal(e.GetValue("rate")));
                    else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    {
                        if (m_numTotalAmount > 0 && m_numTotalQty > 0)
                            e.TotalValue = Math.Round((m_numTotalAmount / m_numTotalQty), 2, MidpointRounding.AwayFromZero);
                        else
                            e.TotalValue = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        private void dgvMFGSales_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                objDepartmentIssue = new StoreDepartmentIssue();
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvDepartmentIssue.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt32(Drow["dept_issue_id"]);
                        txtDeptIssueNo.Text = Val.ToString(Drow["dept_issue_no"]);
                        //txtSBillNo.Text = Val.ToString(Drow["s_bill_no"]);
                        dtpIssueDate.Text = Val.DBDate(Val.ToString(Drow["issue_date"]));
                        dtpBillDate.Text = Val.DBDate(Val.ToString(Drow["bill_date"]));
                        lueToBranch.EditValue = Val.ToInt32(Drow["branch_id"]);
                        lueToDepartent.EditValue = Val.ToInt64(Drow["department_id"]);
                        LueToDivision.EditValue = Val.ToInt64(Drow["division_id"]);
                        lueToManager.EditValue = Val.ToInt64(Drow["manager_id"]);

                        m_dtbDeptIssueDetails = objDepartmentIssue.GetDataDetails(Val.ToInt64(lblMode.Tag));

                        grdDeptIssueDetails.DataSource = m_dtbDeptIssueDetails;

                        Int_Dept_Issue_No = Val.ToInt64(txtDeptIssueNo.Text);

                        ttlbMFGSales.SelectedTabPage = tblSaledetail;
                        txtDeptIssueNo.Focus();

                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void dgvMFGSales_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                if (Val.ToDecimal(clmLAmount.SummaryItem.SummaryValue) > 0 && Val.ToDecimal(clmLQty.SummaryItem.SummaryValue) > 0)
                {
                    m_numSummRate = Math.Round((Val.ToDecimal(clmLAmount.SummaryItem.SummaryValue) / Val.ToDecimal(clmLQty.SummaryItem.SummaryValue)), 2, MidpointRounding.AwayFromZero);

                }
                else
                {
                    m_numSummRate = 0;
                }
                if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == "total_rate")
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
        private void dgvDeptIssueDetails_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvDeptIssueDetails.GetDataRow(e.RowHandle);
                        btnAdd.Text = "&Update";
                        //lueRoughShade.Text = Val.ToString(Drow["color_name"]);
                        //lueRoughSieve.Text = Val.ToString(Drow["sieve_name"]);
                        lueItem.EditValue = Val.ToInt64(Drow["item_id"]);
                        lueSubItem.EditValue = Val.ToInt64(Drow["sub_item_id"]);
                        CmbItemCondition.Text = Val.ToString(Drow["item_condition"]);
                        txtQty.Text = Val.ToString(Drow["qty"]);
                        txtRemark.Text = Val.ToString(Drow["remarks"]);
                        txtRate.Text = Val.ToString(Drow["rate"]);
                        txtAmount.Text = Val.ToString(Drow["amount"]);
                        //m_numUnit = Val.ToDecimal(Drow["qty"]);
                        m_Dept_Issue_detail_id = Val.ToInt64(Drow["dept_issuedetail_id"]);
                        m_update_srno = Val.ToInt(Drow["sr_no"]);
                        Int_Dept_Issue_No = Val.ToInt64(txtDeptIssueNo.Text);

                        if (lblMode.Text == "Edit Mode")
                        {
                            DataTable DTab_Qty = new DataTable();
                            DTab_Qty = objSales.GetQty(Val.ToInt64(lueItem.EditValue), Val.ToInt64(lueSubItem.EditValue));

                            if (DTab_Qty.Rows.Count > 0)
                            {
                                lblBalQty.Text = Val.ToDecimal(DTab_Qty.Rows[0]["balance_qty"]).ToString();
                            }
                            else
                            {
                                lblBalQty.Text = "0";
                            }
                            decimal QTY = Val.ToDecimal(Drow["old_qty"]);
                            decimal lbl_Qty = Val.ToDecimal(lblBalQty.Text);
                            lblBalQty.Text = Val.ToDecimal(QTY + lbl_Qty).ToString();
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
        #endregion

        #endregion

        #region Functions
        private bool LoadDefaults()
        {
            bool blnReturn = true;
            try
            {
                Global.LOOKUPBranch_New(lueToBranch);
                Global.LOOKUPItem(lueItem);
                Global.LOOKUPSubItem(lueSubItem);
                //Global.LOOKUPStoreManager(lueToManager);
                Global.LOOKUPStoreDepartment(lueToDepartent);
                //Global.LOOKUPStoreDivision(LueToDivision);

                m_opDate = Global.GetDate();
                dtpFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpFromDate.EditValue = DateTime.Now;
                m_dtbItem = (((DataTable)lueItem.Properties.DataSource).Copy());
                //dtpFromDate.EditValue = Val.DBDate(m_opDate.Rows[0]["opening_date"].ToString());

                dtpToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpToDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpToDate.EditValue = DateTime.Now;

                dtpIssueDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpIssueDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpIssueDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpIssueDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpIssueDate.EditValue = DateTime.Now;

                dtpBillDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpBillDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpBillDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpBillDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpBillDate.EditValue = DateTime.Now;
                dtpBillDate.Focus();
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
                m_blnadd = true;
                m_blnsave = false;

                if (!ValidateDetails())
                {
                    m_blnadd = false;
                    blnReturn = false;
                    return blnReturn;
                }
                //DataRow[] dr = null;

                //dr = m_dtbDeptIssueDetails.Select("item_id = " + Val.ToInt(lueItem.EditValue) + " and sr_no <>" + m_update_srno);

                //if (dr.Count() == 1)
                //{
                //    Global.Message("Record already exists in grid", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    lueItem.Focus();
                //    blnReturn = false;
                //    return blnReturn;
                //}
                if (btnAdd.Text == "&Add")
                {
                    DataRow drwNew = m_dtbDeptIssueDetails.NewRow();
                    //int numUnit = Val.ToInt32(txtUnit.Text);
                    decimal numRate = Val.ToDecimal(txtRate.Text);
                    decimal numAmount = Val.ToDecimal(txtAmount.Text);
                    decimal numQty = Val.ToDecimal(txtQty.Text);

                    //drwNew["purchase_id"] = Val.ToInt(0);
                    drwNew["dept_issuedetail_id"] = Val.ToInt64(0);
                    drwNew["item_id"] = Val.ToInt64(lueItem.EditValue);
                    drwNew["item_name"] = Val.ToString(lueItem.Text);

                    drwNew["sub_item_id"] = Val.ToInt64(lueSubItem.EditValue);
                    drwNew["sub_item_name"] = Val.ToString(lueSubItem.Text);

                    drwNew["qty"] = numQty;
                    drwNew["old_qty"] = numQty;
                    //drwNew["unit"] = numUnit;
                    drwNew["rate"] = Val.ToDecimal(txtRate.Text);
                    drwNew["amount"] = Math.Round(Val.ToDecimal(txtQty.Text) * Val.ToDecimal(txtRate.Text), 2);
                    drwNew["remarks"] = Val.ToString(txtRemark.Text);
                    drwNew["flag"] = Val.ToInt(0);
                    m_srno = m_srno + 1;
                    drwNew["sr_no"] = Val.ToInt(m_srno);
                    drwNew["item_condition"] = Val.ToString(CmbItemCondition.Text);
                    m_dtbDeptIssueDetails.Rows.Add(drwNew);

                    dgvDeptIssueDetails.MoveLast();
                }
                else if (btnAdd.Text == "&Update")
                {
                    if (m_dtbDeptIssueDetails.Select("sr_no = " + Val.ToInt(m_update_srno)).Length > 0)
                    {
                        for (int i = 0; i < m_dtbDeptIssueDetails.Rows.Count; i++)
                        {
                            if (m_dtbDeptIssueDetails.Select("dept_issuedetail_id ='" + m_Dept_Issue_detail_id + "' AND sr_no = '" + m_update_srno + "'").Length > 0)
                            {
                                //if (m_dtbPurchaseDetails.Rows[m_update_srno - 1]["purchase_detail_id"].ToString() == m_purchase_detail_id.ToString())
                                if (m_dtbDeptIssueDetails.Rows[dgvDeptIssueDetails.FocusedRowHandle]["dept_issuedetail_id"].ToString() == m_Dept_Issue_detail_id.ToString())
                                {
                                    m_dtbDeptIssueDetails.Rows[dgvDeptIssueDetails.FocusedRowHandle]["item_id"] = Val.ToInt32(lueItem.EditValue).ToString();
                                    m_dtbDeptIssueDetails.Rows[dgvDeptIssueDetails.FocusedRowHandle]["item_name"] = Val.ToString(lueItem.Text).ToString();
                                    m_dtbDeptIssueDetails.Rows[dgvDeptIssueDetails.FocusedRowHandle]["sub_item_id"] = Val.ToInt64(lueSubItem.EditValue).ToString();
                                    m_dtbDeptIssueDetails.Rows[dgvDeptIssueDetails.FocusedRowHandle]["sub_item_name"] = Val.ToString(lueSubItem.Text).ToString();
                                    //m_dtbPurchaseDetails.Rows[dgvMFGPurchaseDetails.FocusedRowHandle]["unit"] = Val.ToDecimal(txtUnit.Text).ToString();
                                    m_dtbDeptIssueDetails.Rows[dgvDeptIssueDetails.FocusedRowHandle]["qty"] = Val.ToDecimal(txtQty.Text);
                                    m_dtbDeptIssueDetails.Rows[dgvDeptIssueDetails.FocusedRowHandle]["rate"] = Val.ToDecimal(txtRate.Text);
                                    m_dtbDeptIssueDetails.Rows[dgvDeptIssueDetails.FocusedRowHandle]["remarks"] = Val.ToString(txtRemark.Text);
                                    m_dtbDeptIssueDetails.Rows[dgvDeptIssueDetails.FocusedRowHandle]["flag"] = 1;
                                    m_dtbDeptIssueDetails.Rows[dgvDeptIssueDetails.FocusedRowHandle]["amount"] = Math.Round(Val.ToDecimal(txtQty.Text) * Val.ToDecimal(txtRate.Text), 2);
                                    m_dtbDeptIssueDetails.Rows[dgvDeptIssueDetails.FocusedRowHandle]["item_condition"] = Val.ToString(CmbItemCondition.Text).ToString();
                                    break;
                                }
                            }
                        }
                        btnAdd.Text = "&Add";
                    }
                    else
                    {
                        for (int i = 0; i < m_dtbDeptIssueDetails.Rows.Count; i++)
                        {
                            if (m_dtbDeptIssueDetails.Select("dept_issuedetail_id ='" + m_Dept_Issue_detail_id + "' AND sr_no = '" + m_update_srno + "'").Length > 0)
                            {
                                if (m_dtbDeptIssueDetails.Rows[dgvDeptIssueDetails.FocusedRowHandle]["dept_issuedetail_id"].ToString() == m_Dept_Issue_detail_id.ToString())
                                {
                                    //m_dtbPurchaseDetails.Rows[dgvMFGPurchaseDetails.FocusedRowHandle]["unit"] = Val.ToDecimal(txtUnit.Text).ToString();
                                    m_dtbDeptIssueDetails.Rows[dgvDeptIssueDetails.FocusedRowHandle]["qty"] = Val.ToDecimal(txtQty.Text);
                                    m_dtbDeptIssueDetails.Rows[dgvDeptIssueDetails.FocusedRowHandle]["rate"] = Val.ToDecimal(txtRate.Text);
                                    m_dtbDeptIssueDetails.Rows[dgvDeptIssueDetails.FocusedRowHandle]["flag"] = 1;
                                    m_dtbDeptIssueDetails.Rows[dgvDeptIssueDetails.FocusedRowHandle]["amount"] = Math.Round(Val.ToDecimal(txtQty.Text) * Val.ToDecimal(txtRate.Text), 2);

                                    m_dtbDeptIssueDetails.Rows[dgvDeptIssueDetails.FocusedRowHandle]["item_id"] = Val.ToInt(lueItem.EditValue);
                                    m_dtbDeptIssueDetails.Rows[dgvDeptIssueDetails.FocusedRowHandle]["item_name"] = Val.ToString(lueItem.Text);
                                    m_dtbDeptIssueDetails.Rows[dgvDeptIssueDetails.FocusedRowHandle]["sub_item_id"] = Val.ToInt64(lueSubItem.EditValue).ToString();
                                    m_dtbDeptIssueDetails.Rows[dgvDeptIssueDetails.FocusedRowHandle]["sub_item_name"] = Val.ToString(lueSubItem.Text).ToString();
                                    m_dtbDeptIssueDetails.Rows[dgvDeptIssueDetails.FocusedRowHandle]["item_condition"] = Val.ToString(CmbItemCondition.Text);
                                }
                            }
                        }
                        btnAdd.Text = "&Add";
                    }
                    dgvDeptIssueDetails.MoveLast();
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
                    if (m_dtbDeptIssueDetails.Rows.Count == 0)
                    {
                        lstError.Add(new ListError(22, "Record"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                        }
                    }
                    if (dgvDeptIssueDetails == null)
                    {
                        lstError.Add(new ListError(22, "Record"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                        }
                    }
                    //var result = DateTime.Compare(Convert.ToDateTime(dtpIssueDate.Text), DateTime.Today);
                    //if (result > 0)
                    //{
                    //    lstError.Add(new ListError(5, " Invoice Date Not Be Greater Than Today Date"));
                    //    if (!blnFocus)
                    //    {
                    //        blnFocus = true;
                    //        dtpIssueDate.Focus();
                    //    }
                    //}
                }
                if (m_blnadd)
                {
                    if (lueToBranch.EditValue.ToString() == "")
                    {
                        lstError.Add(new ListError(13, "To Branch"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueToBranch.Focus();
                        }
                    }
                    if (lueToDepartent.EditValue.ToString() == "")
                    {
                        lstError.Add(new ListError(13, "To Department"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueToDepartent.Focus();
                        }
                    }
                    if (LueToDivision.EditValue.ToString() == "")
                    {
                        lstError.Add(new ListError(13, "To Division"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            LueToDivision.Focus();
                        }
                    }
                    //if (lueToManager.EditValue.ToString() != "")
                    //{
                    //    lstError.Add(new ListError(13, "To Manager"));
                    //    if (!blnFocus)
                    //    {
                    //        blnFocus = true;
                    //        lueToManager.Focus();
                    //    }
                    //}
                    if (lueItem.Text == "")
                    {
                        lstError.Add(new ListError(13, "Item"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueItem.Focus();
                        }
                    }
                    if (lueSubItem.Text == "")
                    {
                        lstError.Add(new ListError(13, "Sub Item"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueSubItem.Focus();
                        }
                    }
                    if (Val.ToDouble(txtQty.Text) == 0)
                    {
                        lstError.Add(new ListError(12, "Qty"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtQty.Focus();
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
                    if (Val.ToDecimal(lblBalQty.Text) < Val.ToDecimal(txtQty.Text))
                    {
                        lstError.Add(new ListError(14, "Qty Not More Than Stock"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtQty.Focus();
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
                if (!GeneratePurchaseDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }

                lblMode.Tag = null;
                //lueToManager.EditValue = System.DBNull.Value;
                //lueToBranch.EditValue = System.DBNull.Value;
                //lueToDepartent.EditValue = System.DBNull.Value;
                //LueToDivision.EditValue = System.DBNull.Value;
                //txtSBillNo.Text = string.Empty;
                dtpIssueDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpIssueDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpIssueDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpIssueDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpIssueDate.EditValue = DateTime.Now;

                m_opDate = Global.GetDate();
                //dtpFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                //dtpFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                //dtpFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                //dtpFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
                //// dtpFromDate.EditValue = Val.DBDate(m_opDate.Rows[0]["opening_date"].ToString());
                //dtpFromDate.EditValue = DateTime.Now;

                //dtpIssueDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                //dtpIssueDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                //dtpIssueDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                //dtpIssueDate.Properties.CharacterCasing = CharacterCasing.Upper;
                //dtpIssueDate.EditValue = DateTime.Now;

                dtpBillDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpBillDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpBillDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpBillDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpBillDate.EditValue = DateTime.Now;

                objDepartmentIssue = new StoreDepartmentIssue();
                Int64 Dept_Issue_No = Val.ToInt64(objDepartmentIssue.GetMaximumID("Store_Department_Issue"));
                txtDeptIssueNo.Text = Dept_Issue_No.ToString();

                lueItem.EditValue = System.DBNull.Value;
                lueSubItem.EditValue = DBNull.Value;
                //txtSaleNo.Text = string.Empty;
                txtQty.Text = string.Empty;
                txtRate.Text = string.Empty;
                txtAmount.Text = string.Empty;
                txtRemark.Text = string.Empty;
                CmbItemCondition.SelectedIndex = 0;
                lblBalQty.Text = "0";
                Int_Dept_Issue_No = 0;
                m_srno = 0;
                btnAdd.Text = "&Add";
                lblMode.Text = "Add Mode";
                dtpBillDate.Focus();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
        }
        private bool DeleteDetail()
        {
            bool blnReturn = true;
            Store_PurchaseProperty objStorePurchaseProperty = new Store_PurchaseProperty();
            StorePurchase objStorePurchase = new StorePurchase();
            try
            {
                if (Val.ToInt(lblMode.Tag) != 0)
                {
                    DialogResult result = MessageBox.Show("Do you want to Delete data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                    if (result != DialogResult.Yes)
                    {
                        blnReturn = false;
                        return blnReturn;
                    }

                    objStorePurchaseProperty.purchase_id = Val.ToInt(lblMode.Tag);

                    int IntRes = objStorePurchase.Delete(objStorePurchaseProperty, "DEPARTMENT ISSUE");

                    if (IntRes == -1)
                    {
                        Global.Confirm("Error In Department Issue Entry");
                        txtDeptIssueNo.Focus();
                    }
                    else
                    {
                        if (Val.ToInt(lblMode.Tag) == 0)
                        {
                            Global.Confirm("Department Issue Entry Data Delete Successfully");
                        }
                        else
                        {
                            Global.Confirm("Department Issue Entry Data Delete Successfully");
                        }

                    }
                }
                else
                {
                    Global.Message("Department Issue ID not found");
                    blnReturn = false;
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                blnReturn = false;
            }
            finally
            {
                objStorePurchaseProperty = null;
            }
            return blnReturn;
        }
        private bool PopulateDetails()
        {
            objDepartmentIssue = new StoreDepartmentIssue();
            bool blnReturn = true;
            DateTime datFromDate = DateTime.MinValue;
            DateTime datToDate = DateTime.MinValue;
            try
            {
                m_dtbDetails = objDepartmentIssue.GetData(Val.DBDate(dtpFromDate.Text), Val.DBDate(dtpToDate.Text));

                if (m_dtbDetails.Rows.Count == 0)
                {
                    Global.Message("Data Not Found");
                    blnReturn = false;
                }
                grdDepartmentIssue.DataSource = m_dtbDetails;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            finally
            {
                objDepartmentIssue = null;
            }

            return blnReturn;
        }
        private bool GeneratePurchaseDetails()
        {
            bool blnReturn = true;
            try
            {
                if (m_dtbDeptIssueDetails.Rows.Count > 0)
                    m_dtbDeptIssueDetails.Rows.Clear();

                m_dtbDeptIssueDetails = new DataTable();

                m_dtbDeptIssueDetails.Columns.Add("sr_no", typeof(int));
                m_dtbDeptIssueDetails.Columns.Add("dept_issuedetail_id", typeof(Int64));
                m_dtbDeptIssueDetails.Columns.Add("dept_issue_id", typeof(Int64));
                m_dtbDeptIssueDetails.Columns.Add("item_id", typeof(Int64));
                m_dtbDeptIssueDetails.Columns.Add("item_name", typeof(string));
                m_dtbDeptIssueDetails.Columns.Add("sub_item_id", typeof(Int64));
                m_dtbDeptIssueDetails.Columns.Add("sub_item_name", typeof(string));
                m_dtbDeptIssueDetails.Columns.Add("qty", typeof(decimal)).DefaultValue = 0;
                m_dtbDeptIssueDetails.Columns.Add("unit", typeof(int)).DefaultValue = 0;
                m_dtbDeptIssueDetails.Columns.Add("rate", typeof(decimal)).DefaultValue = 0;
                m_dtbDeptIssueDetails.Columns.Add("amount", typeof(decimal)).DefaultValue = 0;
                m_dtbDeptIssueDetails.Columns.Add("remarks", typeof(string));
                m_dtbDeptIssueDetails.Columns.Add("flag", typeof(int)).DefaultValue = 0;
                m_dtbDeptIssueDetails.Columns.Add("item_condition", typeof(string));
                m_dtbDeptIssueDetails.Columns.Add("old_qty", typeof(decimal)).DefaultValue = 0;

                grdDeptIssueDetails.DataSource = m_dtbDeptIssueDetails;
                grdDeptIssueDetails.Refresh();
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
        public System.Data.DataTable WorksheetToDataTable(ExcelWorksheet ws, bool hasHeader = true)
        {
            System.Data.DataTable dt = new System.Data.DataTable(ws.Name);
            int totalCols = ws.Dimension.End.Column;
            int totalRows = ws.Dimension.End.Row;
            int startRow = hasHeader ? 2 : 1;
            ExcelRange wsRow;
            DataRow dr;
            foreach (var firstRowCell in ws.Cells[1, 1, 1, totalCols])
            {
                dt.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
            }

            for (int rowNum = startRow; rowNum <= totalRows; rowNum++)
            {
                wsRow = ws.Cells[rowNum, 1, rowNum, totalCols];
                dr = dt.NewRow();
                foreach (var cell in wsRow)
                {
                    dr[cell.Start.Column - 1] = cell.Text;
                }
                dt.Rows.Add(dr);
            }
            return dt;
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
                            dgvDepartmentIssue.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvDepartmentIssue.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvDepartmentIssue.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvDepartmentIssue.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvDepartmentIssue.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvDepartmentIssue.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvDepartmentIssue.ExportToCsv(Filepath);
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

        private void lueSubItem_EditValueChanged(object sender, EventArgs e)
        {
            if (lueItem.Text != "" && lueSubItem.Text != "")
            {
                DataTable DTab_Qty = new DataTable();
                DTab_Qty = objSales.GetQty(Val.ToInt64(lueItem.EditValue), Val.ToInt64(lueSubItem.EditValue));

                if (DTab_Qty.Rows.Count > 0)
                {
                    lblBalQty.Text = Val.ToDecimal(DTab_Qty.Rows[0]["balance_qty"]).ToString();
                }
                else
                {
                    lblBalQty.Text = "0";
                }
            }
        }

        private void lueToBranch_EditValueChanged(object sender, EventArgs e)
        {
            if (lueToBranch.EditValue != null && lueToDepartent.EditValue != null)
            {
                Store_DepartmentIssueProperty objDeptIssueProperty = new Store_DepartmentIssueProperty();
                StoreDepartmentIssue objDeptIssue = new StoreDepartmentIssue();

                objDeptIssueProperty.to_branch_id = Val.ToInt64(lueToBranch.EditValue);
                objDeptIssueProperty.to_department_id = Val.ToInt64(lueToDepartent.EditValue);

                DataTable DTab_Division = objDeptIssue.Division_GetData(objDeptIssueProperty);

                LueToDivision.Properties.DataSource = DTab_Division;
                LueToDivision.Properties.ValueMember = "division_id";
                LueToDivision.Properties.DisplayMember = "division_name";
                LueToDivision.ClosePopup();
            }
            else
            {
                LueToDivision.EditValue = null;
            }
        }
        private void lueToDepartent_EditValueChanged_1(object sender, EventArgs e)
        {
            if (lueToBranch.EditValue != null && lueToDepartent.EditValue != null)
            {
                Store_DepartmentIssueProperty objDeptIssueProperty = new Store_DepartmentIssueProperty();
                StoreDepartmentIssue objDeptIssue = new StoreDepartmentIssue();

                objDeptIssueProperty.to_branch_id = Val.ToInt64(lueToBranch.EditValue);
                objDeptIssueProperty.to_department_id = Val.ToInt64(lueToDepartent.EditValue);

                DataTable DTab_Division = objDeptIssue.Division_GetData(objDeptIssueProperty);

                LueToDivision.Properties.DataSource = DTab_Division;
                LueToDivision.Properties.ValueMember = "division_id";
                LueToDivision.Properties.DisplayMember = "division_name";
                LueToDivision.ClosePopup();
            }
            else
            {
                LueToDivision.EditValue = null;
            }
        }
    }
}