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
    public partial class FrmStoreSales : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        Control _NextEnteredControl;
        private List<Control> _tabControls;
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        StoreSales objSales;
        FinancialYearMaster objFinYear;
        UserAuthentication objUserAuthentication;
        RateMaster objRate;
        OpeningStock opstk;

        StorePurchase objPurchase;
        DataTable DtControlSettings;
        DataTable m_dtbSalesDetails;
        DataTable m_dtbDetails;
        DataTable m_opDate;
        DataTable m_dtbItem;
        Int64 Int_Sale_No = 0;
        MfgSubItemMaster objSubItem;
        DataTable DTab_Qty;
        DataTable DTab_Rate;

        int m_sales_detail_id;
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

        #endregion

        #region Constructor
        public FrmStoreSales()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            _NextEnteredControl = new Control();
            _tabControls = new List<Control>();

            objPurchase = new StorePurchase();
            objSales = new StoreSales();
            objFinYear = new FinancialYearMaster();
            objUserAuthentication = new UserAuthentication();
            objRate = new RateMaster();
            opstk = new OpeningStock();

            DtControlSettings = new DataTable();
            m_dtbSalesDetails = new DataTable();
            m_dtbDetails = new DataTable();
            m_opDate = new DataTable();
            m_dtbItem = new DataTable();
            objSubItem = new MfgSubItemMaster();
            DTab_Qty = new DataTable();
            DTab_Rate = new DataTable();

            m_sales_detail_id = 0;
            m_srno = 0;
            m_update_srno = 0;
            IntRes = 0;
            m_FinYear = 0;
            m_numForm_id = 0;

            m_numTotalQty = 0;
            m_numTotalAmount = 0;
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
            objBOFormEvents.ObjToDispose.Add(objSales);
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

            //DialogResult result = MessageBox.Show("Do you want to save data?", "Confirmation", MessageBoxButtons.YesNoCancel);
            //if (result != DialogResult.Yes)
            //{
            //    btnSave.Enabled = true;
            //    return;
            //}

            DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
            panelProgress.Visible = true;
            backgroundWorker_MFGPurchase.RunWorkerAsync();

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
            MFG_PurchaseProperty.date = Val.DBDate(dtpSaleDate.Text);
            MFG_PurchaseProperty.invoice_no = Val.ToString(txtSBillNo.Text);
            MFG_PurchaseProperty.purchase_id = Val.ToInt32(lblMode.Tag);

            DataTable dtpur = new DataTable();
            //dtpur = objMFGPurchase.GetPrintData(MFG_PurchaseProperty);

            //FrmReportViewer FrmReportViewer = new FrmReportViewer();
            //FrmReportViewer.DS.Tables.Add(dtpur);
            //FrmReportViewer.GroupBy = "";
            //FrmReportViewer.RepName = "";
            //FrmReportViewer.RepPara = "";
            //this.Cursor = Cursors.Default;
            //FrmReportViewer.AllowSetFormula = true;

            //FrmReportViewer.ShowForm("Sale_Invoice_Sum", 120, FrmReportViewer.ReportFolder.ACCOUNT);
            //MFG_PurchaseProperty = null;
            //dtpur = null;
            //FrmReportViewer.DS.Tables.Clear();
            //FrmReportViewer.DS.Clear();
            //FrmReportViewer = null;
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
        private void lblFormatSample_Click(object sender, EventArgs e)
        {
            Global.CopyFormat(System.Windows.Forms.Application.StartupPath + @"\FORMAT\MFG_Purchase.xlsx", "MFG_Purchase.xlsx", "xlsx");
        }
        private void lueParty_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmStorePartyMaster frmStoreParty = new FrmStorePartyMaster();
                frmStoreParty.ShowDialog();
                Global.LOOKUPStoreParty(lueParty, "SALE");
            }
        }
        private void dtpSalesDate_EditValueChanged(object sender, EventArgs e)
        {
            DataTable dtFinYear = new DataTable();
            dtFinYear = objFinYear.GetData();
            foreach (DataRow drw in dtFinYear.Rows)
            {
                if (dtpSaleDate.Text != "")
                {
                    var result = DateTime.Compare(Convert.ToDateTime(dtpSaleDate.Text), Convert.ToDateTime(drw["start_date"]));
                    var result2 = DateTime.Compare(Convert.ToDateTime(dtpSaleDate.Text), Convert.ToDateTime(drw["end_date"]));
                    if (result > 0 && result2 < 0)
                    {
                        m_FinYear = Val.ToInt(drw["fin_year_id"]);
                    }
                }
            }
        }
        private void txtPartyInvoiceNo_TextChanged(object sender, EventArgs e)
        {
            //if (txtPBillNo.Text != "")
            //{
            //    txtPurchaseNo.Text = txtPBillNo.Text;
            //}
        }
        private void backgroundWorker_MFGSales_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.Default;
                Conn = new BeginTranConnection(true, false);
                //double dueDays;
                //dueDays = (Convert.ToDateTime(dtpDueDate.Text) - Convert.ToDateTime(dtpPurchaseDate.Text)).TotalDays;

                Store_SalesProperty objSalesProperty = new Store_SalesProperty();
                StoreSales objPurchase = new StoreSales();
                try
                {
                    IntRes = 0;

                    objSalesProperty.sales_id = Val.ToInt(lblMode.Tag);

                    if (Int_Sale_No == 0)
                    {
                        objSalesProperty.sales_no = Int_Sale_No;
                    }
                    else
                    {
                        objSalesProperty.sales_no = Val.ToInt64(txtSaleNo.Text);
                    }
                    objSalesProperty.s_bill_no = Val.ToString(txtSBillNo.Text);
                    objSalesProperty.party_id = Val.ToInt64(lueParty.EditValue);
                    objSalesProperty.company_id = Val.ToInt64(GlobalDec.gEmployeeProperty.company_id);
                    objSalesProperty.branch_id = Val.ToInt64(GlobalDec.gEmployeeProperty.branch_id);
                    objSalesProperty.location_id = Val.ToInt64(GlobalDec.gEmployeeProperty.location_id);
                    objSalesProperty.department_id = Val.ToInt64(GlobalDec.gEmployeeProperty.department_id);
                    objSalesProperty.remarks = Val.ToString(txtRemark.Text);
                    objSalesProperty.sales_date = Val.DBDate(dtpSaleDate.Text);
                    objSalesProperty.bill_date = Val.DBDate(dtpBillDate.Text);
                    objSalesProperty.total_qty = Math.Round(Val.ToDecimal(clmQty.SummaryItem.SummaryValue), 2);
                    objSalesProperty.total_rate = Math.Round(Val.ToDecimal(clmRSrate.SummaryItem.SummaryValue), 2);
                    objSalesProperty.total_amount = Math.Round(Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue), 2);
                    objSalesProperty = objPurchase.Save(objSalesProperty, DLL.GlobalDec.EnumTran.Start, Conn);
                    Int64 NewmSalesid = Val.ToInt64(objSalesProperty.sales_id);

                    int IntCounter = 0;
                    int Count = 0;
                    int TotalCount = m_dtbSalesDetails.Rows.Count;

                    foreach (DataRow drw in m_dtbSalesDetails.Rows)
                    {
                        objSalesProperty = new Store_SalesProperty();
                        objSalesProperty.sales_id = Val.ToInt64(NewmSalesid);
                        objSalesProperty.sales_detail_id = Val.ToInt64(drw["sales_detail_id"]);
                        objSalesProperty.item_id = Val.ToInt(drw["item_id"]);
                        objSalesProperty.sub_item_id = Val.ToInt64(drw["sub_item_id"]);
                        objSalesProperty.remarks = Val.ToString(drw["remarks"]);
                        objSalesProperty.qty = Val.ToDecimal(drw["qty"]);
                        objSalesProperty.rate = Val.ToDecimal(drw["rate"]);
                        objSalesProperty.amount = Val.ToDecimal(drw["amount"]);
                        objSalesProperty.item_condition = Val.ToString(drw["item_condition"]);

                        objSalesProperty.party_id = Val.ToInt64(lueParty.EditValue);

                        IntRes = objPurchase.Save_PurchaseDetail(objSalesProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

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
                    objSalesProperty = null;
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
        private void backgroundWorker_MFGPurchase_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                panelProgress.Visible = false;
                if (IntRes > 0)
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Sales Entry Data Save Successfully");
                        ClearDetails();
                        PopulateDetails();
                    }
                    else
                    {
                        Global.Confirm("Sales Entry Data Update Successfully");
                        ClearDetails();
                        PopulateDetails();
                    }
                }
                else
                {
                    Global.Confirm("Error In Sales");
                    txtSaleNo.Focus();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }
        private void RepBtnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //dgvMFGSaleDetails.DeleteRow(dgvMFGSaleDetails.GetRowHandle(dgvMFGSaleDetails.FocusedRowHandle));
            //m_dtbSalesDetails.AcceptChanges();
            if (Global.Confirm("Are you sure delete selected row?", "STORE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                //dgvStock.DeleteRow(dgvStock.GetRowHandle(dgvStock.FocusedRowHandle));

                Store_PurchaseProperty objPurchaseProperty = new Store_PurchaseProperty();
                int IntRes = 0;
                Int64 SalesDetailID = Val.ToInt64(dgvMFGSaleDetails.GetFocusedRowCellValue("sales_detail_id").ToString());
                Int64 SalesID = Val.ToInt64(dgvMFGSaleDetails.GetFocusedRowCellValue("sales_id").ToString());
                objPurchaseProperty.purchase_detail_id = Val.ToInt64(SalesDetailID);
                objPurchaseProperty.purchase_id = Val.ToInt64(SalesID);

                if (SalesDetailID == 0)
                {
                    dgvMFGSaleDetails.DeleteRow(dgvMFGSaleDetails.GetRowHandle(dgvMFGSaleDetails.FocusedRowHandle));
                    m_dtbSalesDetails.AcceptChanges();
                }
                else
                {
                    IntRes = objPurchase.Delete_PurchaseDetail(objPurchaseProperty, "SALE");
                    dgvMFGSaleDetails.DeleteRow(dgvMFGSaleDetails.GetRowHandle(dgvMFGSaleDetails.FocusedRowHandle));
                    m_dtbSalesDetails.AcceptChanges();
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
            //DataTable dtRate = new DataTable();
            //if (lueItem.Text != "")
            //{
            //    dtRate = m_dtbItem.Select("item_id=" + Val.ToInt32(lueItem.EditValue)).CopyToDataTable();
            //    if (dtRate.Rows.Count > 0)
            //    {
            //        txtRate.Text = Val.ToString(dtRate.Rows[0]["rate"]);
            //    }
            //}
            DataTable dtSubItem = new DataTable();
            if (lueItem.Text != "")
            {
                objSubItem = new MfgSubItemMaster();
                dtSubItem = objSubItem.ItemWise_Sub_GetData(Val.ToInt64(lueItem.EditValue));

                lueSubItem.Properties.DataSource = dtSubItem;
                lueSubItem.Properties.ValueMember = "sub_item_id";
                lueSubItem.Properties.DisplayMember = "sub_item_name";
            }

            if (lueItem.Text != "" && lueSubItem.Text != "")
            {
                DTab_Rate = new DataTable();
                objSales = new StoreSales();

                DTab_Rate = objSales.GetRate(Val.ToInt64(lueItem.EditValue), Val.ToInt64(lueSubItem.EditValue));

                if (DTab_Rate.Rows.Count > 0)
                {
                    txtRate.Text = Val.ToDecimal(DTab_Rate.Rows[0]["rate"]).ToString();
                }
                else
                {
                    txtRate.Text = "0";
                }

                DTab_Qty = new DataTable();
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
        private void txtQty_EditValueChanged(object sender, EventArgs e)
        {
            txtAmount.Text = string.Format("{0:0.00}", Val.ToDecimal(txtQty.Text) * Val.ToDecimal(txtRate.Text));
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
        private void dgvMFGSalesDetails_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvMFGSaleDetails.GetDataRow(e.RowHandle);
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
                        m_sales_detail_id = Val.ToInt(Drow["sales_detail_id"]);
                        m_update_srno = Val.ToInt(Drow["sr_no"]);
                        Int_Sale_No = Val.ToInt64(txtSaleNo.Text);

                        if (lblMode.Text == "Edit Mode")
                        {
                            DTab_Qty = new DataTable();
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
        private void dgvMFGSales_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                objSales = new StoreSales();
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvMFGSales.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt32(Drow["sales_id"]);
                        txtSaleNo.Text = Val.ToString(Drow["sales_no"]);
                        txtSBillNo.Text = Val.ToString(Drow["s_bill_no"]);
                        dtpSaleDate.Text = Val.DBDate(Val.ToString(Drow["sales_date"]));
                        dtpBillDate.Text = Val.DBDate(Val.ToString(Drow["bill_date"]));
                        lueParty.EditValue = Val.ToInt64(Drow["party_id"]);

                        m_dtbSalesDetails = objSales.GetDataDetails(Val.ToInt(lblMode.Tag));

                        grdMFGSaleDetails.DataSource = m_dtbSalesDetails;
                        Int_Sale_No = Val.ToInt64(txtSaleNo.Text);

                        ttlbMFGSales.SelectedTabPage = tblSaledetail;
                        txtSaleNo.Focus();

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
        #endregion

        #endregion

        #region Functions
        private bool LoadDefaults()
        {
            bool blnReturn = true;
            try
            {
                Global.LOOKUPItem(lueItem);
                Global.LOOKUPStoreParty(lueParty, "SALE");
                Global.LOOKUPStoreParty(lueListParty, "SALE");
                Global.LOOKUPSubItem(lueSubItem);

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

                dtpSaleDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpSaleDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpSaleDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpSaleDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpSaleDate.EditValue = DateTime.Now;

                dtpBillDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpBillDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpBillDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpBillDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpBillDate.EditValue = DateTime.Now;
                txtSBillNo.Focus();
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

                //dr = m_dtbSalesDetails.Select("item_id = " + Val.ToInt(lueItem.EditValue) + " and sub_item_id =" + Val.ToInt(lueSubItem.EditValue) + " and sr_no <>" + m_update_srno);

                //if (dr.Count() == 1)
                //{
                //    Global.Message("Record already exists in grid", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    lueItem.Focus();
                //    blnReturn = false;
                //    return blnReturn;
                //}
                if (btnAdd.Text == "&Add")
                {
                    //dr = m_dtbSalesDetails.Select("item_id = " + Val.ToInt(lueItem.EditValue) + " and sub_item_id =" + Val.ToInt(lueSubItem.EditValue));

                    //if (dr.Count() == 1)
                    //{
                    //    Global.Message("Record already exists in grid", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    lueItem.Focus();
                    //    blnReturn = false;
                    //    return blnReturn;
                    //}

                    DataRow drwNew = m_dtbSalesDetails.NewRow();
                    //int numUnit = Val.ToInt32(txtUnit.Text);
                    decimal numRate = Val.ToDecimal(txtRate.Text);
                    decimal numAmount = Val.ToDecimal(txtAmount.Text);
                    decimal numQty = Val.ToDecimal(txtQty.Text);

                    //drwNew["purchase_id"] = Val.ToInt(0);
                    drwNew["sales_detail_id"] = Val.ToInt64(0);
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
                    m_dtbSalesDetails.Rows.Add(drwNew);

                    dgvMFGSaleDetails.MoveLast();
                }
                else if (btnAdd.Text == "&Update")
                {
                    if (m_dtbSalesDetails.Select("sr_no = " + Val.ToInt(m_update_srno)).Length > 0)
                    {
                        for (int i = 0; i < m_dtbSalesDetails.Rows.Count; i++)
                        {
                            if (m_dtbSalesDetails.Select("sales_detail_id ='" + m_sales_detail_id + "' AND sr_no = '" + m_update_srno + "'").Length > 0)
                            {
                                //if (m_dtbPurchaseDetails.Rows[m_update_srno - 1]["purchase_detail_id"].ToString() == m_purchase_detail_id.ToString())
                                if (m_dtbSalesDetails.Rows[dgvMFGSaleDetails.FocusedRowHandle]["sales_detail_id"].ToString() == m_sales_detail_id.ToString())
                                {
                                    m_dtbSalesDetails.Rows[dgvMFGSaleDetails.FocusedRowHandle]["item_id"] = Val.ToInt64(lueItem.EditValue).ToString();
                                    m_dtbSalesDetails.Rows[dgvMFGSaleDetails.FocusedRowHandle]["item_name"] = Val.ToString(lueItem.Text).ToString();
                                    m_dtbSalesDetails.Rows[dgvMFGSaleDetails.FocusedRowHandle]["sub_item_id"] = Val.ToInt64(lueSubItem.EditValue).ToString();
                                    m_dtbSalesDetails.Rows[dgvMFGSaleDetails.FocusedRowHandle]["sub_item_name"] = Val.ToString(lueSubItem.Text).ToString();
                                    //m_dtbPurchaseDetails.Rows[dgvMFGPurchaseDetails.FocusedRowHandle]["unit"] = Val.ToDecimal(txtUnit.Text).ToString();
                                    m_dtbSalesDetails.Rows[dgvMFGSaleDetails.FocusedRowHandle]["qty"] = Val.ToDecimal(txtQty.Text);
                                    m_dtbSalesDetails.Rows[dgvMFGSaleDetails.FocusedRowHandle]["rate"] = Val.ToDecimal(txtRate.Text);
                                    m_dtbSalesDetails.Rows[dgvMFGSaleDetails.FocusedRowHandle]["remarks"] = Val.ToString(txtRemark.Text);
                                    m_dtbSalesDetails.Rows[dgvMFGSaleDetails.FocusedRowHandle]["flag"] = 1;
                                    m_dtbSalesDetails.Rows[dgvMFGSaleDetails.FocusedRowHandle]["amount"] = Math.Round(Val.ToDecimal(txtQty.Text) * Val.ToDecimal(txtRate.Text), 2);
                                    m_dtbSalesDetails.Rows[dgvMFGSaleDetails.FocusedRowHandle]["item_condition"] = Val.ToString(CmbItemCondition.Text).ToString();
                                    break;
                                }
                            }
                        }
                        btnAdd.Text = "&Add";
                    }
                    else
                    {
                        for (int i = 0; i < m_dtbSalesDetails.Rows.Count; i++)
                        {
                            if (m_dtbSalesDetails.Select("sales_detail_id ='" + m_sales_detail_id + "' AND sr_no = '" + m_update_srno + "'").Length > 0)
                            {
                                if (m_dtbSalesDetails.Rows[dgvMFGSaleDetails.FocusedRowHandle]["sales_detail_id"].ToString() == m_sales_detail_id.ToString())
                                {
                                    //m_dtbPurchaseDetails.Rows[dgvMFGPurchaseDetails.FocusedRowHandle]["unit"] = Val.ToDecimal(txtUnit.Text).ToString();
                                    m_dtbSalesDetails.Rows[dgvMFGSaleDetails.FocusedRowHandle]["qty"] = Val.ToDecimal(txtQty.Text);
                                    m_dtbSalesDetails.Rows[dgvMFGSaleDetails.FocusedRowHandle]["rate"] = Val.ToDecimal(txtRate.Text);
                                    m_dtbSalesDetails.Rows[dgvMFGSaleDetails.FocusedRowHandle]["flag"] = 1;
                                    m_dtbSalesDetails.Rows[dgvMFGSaleDetails.FocusedRowHandle]["amount"] = Math.Round(Val.ToDecimal(txtQty.Text) * Val.ToDecimal(txtRate.Text), 2);

                                    m_dtbSalesDetails.Rows[dgvMFGSaleDetails.FocusedRowHandle]["item_id"] = Val.ToInt64(lueItem.EditValue);
                                    m_dtbSalesDetails.Rows[dgvMFGSaleDetails.FocusedRowHandle]["item_name"] = Val.ToString(lueItem.Text);
                                    m_dtbSalesDetails.Rows[dgvMFGSaleDetails.FocusedRowHandle]["sub_item_id"] = Val.ToInt64(lueSubItem.EditValue).ToString();
                                    m_dtbSalesDetails.Rows[dgvMFGSaleDetails.FocusedRowHandle]["sub_item_name"] = Val.ToString(lueSubItem.Text).ToString();
                                    m_dtbSalesDetails.Rows[dgvMFGSaleDetails.FocusedRowHandle]["item_condition"] = Val.ToString(CmbItemCondition.Text);
                                }
                            }
                        }
                        btnAdd.Text = "&Add";
                    }
                    dgvMFGSaleDetails.MoveLast();
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
                    if (m_dtbSalesDetails.Rows.Count == 0)
                    {
                        lstError.Add(new ListError(22, "Record"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                        }
                    }
                    if (dgvMFGSaleDetails == null)
                    {
                        lstError.Add(new ListError(22, "Record"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                        }
                    }
                    //var result = DateTime.Compare(Convert.ToDateTime(dtpSaleDate.Text), DateTime.Today);
                    //if (result > 0)
                    //{
                    //    lstError.Add(new ListError(5, " Invoice Date Not Be Greater Than Today Date"));
                    //    if (!blnFocus)
                    //    {
                    //        blnFocus = true;
                    //        dtpSaleDate.Focus();
                    //    }
                    //}
                }
                if (m_blnadd)
                {
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
                lueParty.EditValue = System.DBNull.Value;
                txtSBillNo.Text = string.Empty;
                dtpSaleDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpSaleDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpSaleDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpSaleDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpSaleDate.EditValue = DateTime.Now;

                //m_opDate = Global.GetDate();
                dtpFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
                // dtpFromDate.EditValue = Val.DBDate(m_opDate.Rows[0]["opening_date"].ToString());
                dtpFromDate.EditValue = DateTime.Now;

                dtpSaleDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpSaleDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpSaleDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpSaleDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpSaleDate.EditValue = DateTime.Now;

                dtpBillDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpBillDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpBillDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpBillDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpBillDate.EditValue = DateTime.Now;

                objSales = new StoreSales();
                Int64 Sale_No = Val.ToInt64(objSales.GetMaximumID("Store_Sales"));
                txtSaleNo.Text = Sale_No.ToString();

                lueItem.EditValue = System.DBNull.Value;
                lueSubItem.EditValue = DBNull.Value;
                //txtSaleNo.Text = string.Empty;
                txtQty.Text = string.Empty;
                txtRate.Text = string.Empty;
                txtAmount.Text = string.Empty;
                txtRemark.Text = string.Empty;
                CmbItemCondition.SelectedIndex = 0;
                Int_Sale_No = 0;
                m_srno = 0;
                lblBalQty.Text = "0";
                btnAdd.Text = "&Add";
                lblMode.Text = "Add Mode";
                txtSBillNo.Focus();
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

                    int IntRes = objStorePurchase.Delete(objStorePurchaseProperty, "SALE");

                    if (IntRes == -1)
                    {
                        Global.Confirm("Error In Sale Entry");
                        txtSBillNo.Focus();
                    }
                    else
                    {
                        if (Val.ToInt(lblMode.Tag) == 0)
                        {
                            Global.Confirm("Sale Entry Data Delete Successfully");
                        }
                        else
                        {
                            Global.Confirm("Sale Entry Data Delete Successfully");
                        }
                    }
                }
                else
                {
                    Global.Message("Sale ID not found");
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
            objSales = new StoreSales();
            bool blnReturn = true;
            DateTime datFromDate = DateTime.MinValue;
            DateTime datToDate = DateTime.MinValue;
            try
            {
                m_dtbDetails = objSales.GetData(Val.DBDate(dtpFromDate.Text), Val.DBDate(dtpToDate.Text), Val.ToInt64(lueListParty.EditValue));

                if (m_dtbDetails.Rows.Count == 0)
                {
                    Global.Message("Data Not Found");
                    blnReturn = false;
                }
                grdMFGSales.DataSource = m_dtbDetails;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            finally
            {
                objSales = null;
            }

            return blnReturn;
        }
        private bool GeneratePurchaseDetails()
        {
            bool blnReturn = true;
            try
            {
                if (m_dtbSalesDetails.Rows.Count > 0)
                    m_dtbSalesDetails.Rows.Clear();

                m_dtbSalesDetails = new DataTable();

                m_dtbSalesDetails.Columns.Add("sr_no", typeof(int));
                m_dtbSalesDetails.Columns.Add("sales_detail_id", typeof(Int64));
                m_dtbSalesDetails.Columns.Add("sales_id", typeof(Int64));
                m_dtbSalesDetails.Columns.Add("item_id", typeof(Int64));
                m_dtbSalesDetails.Columns.Add("item_name", typeof(string));
                m_dtbSalesDetails.Columns.Add("sub_item_id", typeof(Int64));
                m_dtbSalesDetails.Columns.Add("sub_item_name", typeof(string));
                m_dtbSalesDetails.Columns.Add("qty", typeof(decimal)).DefaultValue = 0;
                m_dtbSalesDetails.Columns.Add("unit", typeof(int)).DefaultValue = 0;
                m_dtbSalesDetails.Columns.Add("rate", typeof(decimal)).DefaultValue = 0;
                m_dtbSalesDetails.Columns.Add("amount", typeof(decimal)).DefaultValue = 0;
                m_dtbSalesDetails.Columns.Add("remarks", typeof(string));
                m_dtbSalesDetails.Columns.Add("flag", typeof(int)).DefaultValue = 0;
                m_dtbSalesDetails.Columns.Add("item_condition", typeof(string));
                m_dtbSalesDetails.Columns.Add("old_qty", typeof(decimal)).DefaultValue = 0;

                grdMFGSaleDetails.DataSource = m_dtbSalesDetails;
                grdMFGSaleDetails.Refresh();
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
                            dgvMFGSales.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvMFGSales.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvMFGSales.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvMFGSales.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvMFGSales.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvMFGSales.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvMFGSales.ExportToCsv(Filepath);
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
                DTab_Rate = new DataTable();
                objSales = new StoreSales();

                DTab_Rate = objSales.GetRate(Val.ToInt64(lueItem.EditValue), Val.ToInt64(lueSubItem.EditValue));

                if (DTab_Rate.Rows.Count > 0)
                {
                    txtRate.Text = Val.ToDecimal(DTab_Rate.Rows[0]["rate"]).ToString();
                }
                else
                {
                    txtRate.Text = "0";
                }

                DTab_Qty = new DataTable();
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearDetails();
        }

        private void BtnListClear_Click(object sender, EventArgs e)
        {
            lueListParty.EditValue = System.DBNull.Value;
        }
    }
}