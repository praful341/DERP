using BLL;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.FunctionClasses.Utility;
using BLL.PropertyClasses.Transaction;
using BLL.PropertyClasses.Transaction.MFG;
using DERP.Class;
using DERP.Master;
using DevExpress.XtraEditors;
using DREP.Master;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DREP.Transaction
{
    public partial class FrmMFGRoughSale : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        Control _NextEnteredControl;
        private List<Control> _tabControls;
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        MFGRoughSale objRoughSale;
        FinancialYearMaster objFinYear;
        UserAuthentication objUserAuthentication;
        RateMaster objRate;
        OpeningStock opstk;

        DataTable DtControlSettings;
        DataTable m_dtbPurchaseDetails;
        DataTable m_dtbDetails;
        DataTable m_opDate;
        DataTable m_dtbMemoNo;
        DataTable m_dtbDemandNo;
        DataTable m_dtbKapanNo;

        int m_invoice_id;
        int m_srno;
        int m_update_srno;
        int IntRes;
        int m_numForm_id;
        int m_kapansale_flag;

        decimal m_numTotalCarats;
        decimal m_numTotalAmount;
        decimal m_numcarat;
        decimal m_current_rate;
        decimal m_current_amount;
        decimal m_numSummRate;

        bool m_blnadd;
        bool m_blnsave;
        bool m_blncheckevents;

        #endregion

        #region Constructor
        public FrmMFGRoughSale()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            _NextEnteredControl = new Control();
            _tabControls = new List<Control>();

            objRoughSale = new MFGRoughSale();
            objFinYear = new FinancialYearMaster();
            objUserAuthentication = new UserAuthentication();
            objRate = new RateMaster();
            opstk = new OpeningStock();

            DtControlSettings = new DataTable();
            m_dtbPurchaseDetails = new DataTable();
            m_dtbDetails = new DataTable();
            m_opDate = new DataTable();
            m_dtbMemoNo = new DataTable();
            m_dtbDemandNo = new DataTable();
            m_dtbKapanNo = new DataTable();

            m_invoice_id = 0;
            m_srno = 0;
            m_update_srno = 0;
            IntRes = 0;
            m_numForm_id = 0;
            m_kapansale_flag = 0;
            m_numTotalCarats = 0;
            m_numTotalAmount = 0;
            m_numcarat = 0;
            m_current_rate = 0;
            m_current_amount = 0;

            m_blnadd = new bool();
            m_blnsave = new bool();
            m_blncheckevents = new bool();
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
            objBOFormEvents.ObjToDispose.Add(objRoughSale);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);

        }

        #endregion

        #region Events
        private void FrmMFGPurchase_Load(object sender, EventArgs e)
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
                    ttlbMFGPurchase.SelectedTabPage = tblBranchdetail;
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
                lueKapan.EditValue = System.DBNull.Value;
                txtCarat.Text = string.Empty;
                txtRate.Text = string.Empty;
                txtAmount.Text = string.Empty;
                lueKapan.Focus();
                //lueAssortName_KeyUp(null, null);
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
            string Str = "";
            if (Val.DBDate(BLL.GlobalDec.gStrServerDate) != Val.DBDate(dtpInvoiceDate.Text))
            {
                Str = GlobalDec.CheckLockIsOpenOrNot(Val.DBDate(dtpInvoiceDate.Text), Val.DBTime(DateTime.Now.ToShortTimeString())); //Val.GetFullTime12());
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
                    dtpInvoiceDate.Enabled = true;
                    dtpInvoiceDate.Visible = true;
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
            MFG_PurchaseProperty.date = Val.DBDate(dtpInvoiceDate.Text);
            MFG_PurchaseProperty.invoice_no = Val.ToString(txtInvoiceNo.Text);
            MFG_PurchaseProperty.purchase_id = Val.ToInt32(lblMode.Tag);

            //DataTable dtpur = new DataTable();
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
            //if (DeleteDetail())
            //{
            //    ClearDetails();
            //}
            btnDelete.Enabled = true;
        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog OpenDialog = new OpenFileDialog();
                if (OpenDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }
                txtFileName.Text = OpenDialog.FileName;
                OpenDialog.Dispose();
                OpenDialog = null;

                if (File.Exists(txtFileName.Text) == false)
                {
                    Global.Message("File Is Not Exists To The Path");
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                grdMFGRoughSale.DataSource = null;


                if (txtFileName.Text.Length != 0)
                {
                    using (var pck = new ExcelPackage(new FileInfo(txtFileName.Text)))
                    {
                        ExcelWorksheet ws = pck.Workbook.Worksheets[1];
                        m_dtbPurchaseDetails = WorksheetToDataTable(ws, true);
                    }
                }


                m_dtbPurchaseDetails.Columns.Add("purchase_detail_id", typeof(int));
                m_dtbPurchaseDetails.Columns.Add("invoice_id", typeof(int));
                m_dtbPurchaseDetails.Columns.Add("rough_sieve_id", typeof(int));
                m_dtbPurchaseDetails.Columns.Add("color_id", typeof(int));
                m_dtbPurchaseDetails.Columns.Add("discount", typeof(decimal)).DefaultValue = 0;
                m_dtbPurchaseDetails.Columns.Add("current_rate", typeof(decimal)).DefaultValue = 0;
                m_dtbPurchaseDetails.Columns.Add("current_amount", typeof(decimal)).DefaultValue = 0;
                m_dtbPurchaseDetails.Columns.Add("old_carat", typeof(decimal)).DefaultValue = 0;
                m_dtbPurchaseDetails.Columns.Add("old_pcs", typeof(int));
                m_dtbPurchaseDetails.Columns.Add("flag", typeof(int));

                //m_dtbPurchaseDetails.Columns.Add("old_assort_id", typeof(int));
                //m_dtbPurchaseDetails.Columns.Add("old_sieve_id", typeof(int));
                //m_dtbPurchaseDetails.Columns.Add("old_sub_sieve_id", typeof(int));
                m_dtbPurchaseDetails.Columns.Add("sr_no", typeof(int));
                m_srno = 0;

                foreach (DataRow DRow in m_dtbPurchaseDetails.Rows)
                {
                    BranchTransfer objBranch = new BranchTransfer();

                    if (m_dtbPurchaseDetails.Select("sieve_name ='" + Val.ToString(DRow["sieve_name"]) + "' And color_name = '" + Val.ToString(DRow["color_name"]) + "'").Length > 1)
                    {
                        Global.Message("Duplicate Value found in Rough Sieve : " + Val.ToString(DRow["sieve_name"]) + " AND Color: " + Val.ToString(DRow["color_name"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (DRow["sieve_name"] != null)
                    {
                        if (Val.ToString(DRow["sieve_name"]) != "")
                        {
                            //if (m_dtbAssortscheck.Select("sieve_name ='" + Val.ToString(DRow["sieve_name"]) + "'").Length > 0)
                            //{
                            //    m_dtbAssortsdtl = m_dtbAssortscheck.Select("sieve_name ='" + Val.ToString(DRow["sieve_name"]) + "'").CopyToDataTable();
                            //}
                            //else
                            //{
                            //    Global.Message("Sieve Name Not found in Master : " + Val.ToString(DRow["sieve_name"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    return;
                            //}
                        }
                    }
                    else
                    {
                        Global.Message("Sieve Name are not found :" + Val.ToString(DRow["sieve_name"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Cursor = Cursors.Default;
                        return;
                    }


                    if (DRow["color_name"] != null)
                    {
                        if (Val.ToString(DRow["color_name"]) != "")
                        {
                            //if (m_dtbSievecheck.Select("color_name ='" + Val.ToString(DRow["color_name"]) + "'").Length > 0)
                            //{
                            //    m_dtbSievedtl = m_dtbSievecheck.Select("color_name ='" + Val.ToString(DRow["color_name"]) + "'").CopyToDataTable();
                            //}
                            //else
                            //{
                            //    Global.Message("Color Not found in Master : " + Val.ToString(DRow["color_name"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    this.Cursor = Cursors.Default;
                            //    return;
                            //}
                        }
                    }
                    else
                    {
                        Global.Message("Color Name are not found : " + Val.ToString(DRow["color_name"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Cursor = Cursors.Default;
                        return;
                    }

                    //decimal numStockCarat = 0;

                    //DataTable p_dtbStockCarat = new DataTable();
                    //p_dtbStockCarat = objSaleInvoice.GetStockCarat(GlobalDec.gEmployeeProperty.company_id, GlobalDec.gEmployeeProperty.branch_id, GlobalDec.gEmployeeProperty.location_id, GlobalDec.gEmployeeProperty.department_id, Val.ToInt(lueRoughSieve.EditValue), Val.ToInt(lueRoughShade.EditValue));

                    //if (p_dtbStockCarat.Rows.Count > 0)
                    //{
                    //    numStockCarat = Val.ToDecimal(p_dtbStockCarat.Rows[0]["stock_carat"]);
                    //}

                    //if (numStockCarat < Val.ToDecimal(DRow["carat"]))
                    //{
                    //    Global.Message("Please check enter carat more then stock carat  (Assorts : " + Val.ToString(DRow["assort_name"]) + ") (Sieve : " + Val.ToString(DRow["sieve_name"]) + " ) ", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    txtCarat.Focus();
                    //    this.Cursor = Cursors.Default;
                    //    return;
                    //}


                    //DRow["rough_sieve_id"] = Val.ToInt(m_dtbAssortsdtl.Rows[0]["rough_sieve_id"]);
                    //DRow["color_id"] = Val.ToInt(m_dtbSievedtl.Rows[0]["color_id"]);

                    //string p_numStockRate = string.Empty;
                    //p_numStockRate = objBranch.GetLetestPrice(Val.ToInt(m_dtbAssortsdtl.Rows[0]["assort_id"]), Val.ToInt(m_dtbSievedtl.Rows[0]["sieve_id"]));
                    //m_current_rate = Val.ToDecimal(p_numStockRate);
                    //DRow["current_rate"] = m_current_rate;
                    //DRow["current_amount"] = Val.ToDecimal(m_current_rate) * Val.ToDecimal(DRow["carat"]);
                    m_srno = m_srno + 1;
                    DRow["sr_no"] = Val.ToInt(m_srno);
                }

                grdMFGRoughSale.DataSource = m_dtbPurchaseDetails;
                dgvMFGRoughSale.MoveLast();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                this.Cursor = Cursors.Default;
                return;
            }
        }
        private void txtOtherExpense_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!m_blncheckevents)
                {
                    decimal Other_Expense = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) + Val.ToDecimal(txtBrokerageAmt.Text) + Val.ToDecimal(txtPremiumAmt.Text) + Val.ToDecimal(txtOtherExpense.Text)), 0);
                    txtNetAmount.Text = Other_Expense.ToString();
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void panelControl4_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(255, 191, 219, 255), 2);
            e.Graphics.DrawLine(pen, 0, 80, 1500, 80);
        }
        private void txtCarat_EditValueChanged(object sender, EventArgs e)
        {
            txtAmount.Text = string.Format("{0:0.00}", Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text));
            m_current_amount = Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(m_current_rate);
        }
        private void txtRate_EditValueChanged(object sender, EventArgs e)
        {
            txtAmount.Text = string.Format("{0:0.00}", Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text));
        }
        private void txtTermDays_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtpInvoiceDate.Text.Length <= 0 || dtpInvoiceDate.Text == "")
                {
                    txtTermDays.Text = "";
                    dtpDueDate.EditValue = null;
                }
                else
                {
                    Double Days = Val.ToDouble(txtTermDays.Text);
                    DateTime Date = Convert.ToDateTime(dtpInvoiceDate.EditValue).AddDays(Val.ToDouble(Days));
                    dtpDueDate.EditValue = Val.DBDate(Date.ToShortDateString());
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void txtBrokeragePer_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!m_blncheckevents)
                {
                    decimal Brokerage_amt = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue)) * Val.ToDecimal(txtBrokeragePer.Text) / 100, 0);
                    txtBrokerageAmt.Text = Brokerage_amt.ToString();
                    decimal Net_Amount = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue)) + Val.ToDecimal(txtBrokerageAmt.Text) + Val.ToDecimal(txtPremiumAmt.Text) - Val.ToDecimal(txtDiscountAmt.Text), 0);
                    txtNetAmount.Text = Net_Amount.ToString();
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void lblFormatSample_Click(object sender, EventArgs e)
        {
            Global.CopyFormat(System.Windows.Forms.Application.StartupPath + @"\FORMAT\MFG_Purchase.xlsx", "MFG_Purchase.xlsx", "xlsx");
        }
        private void lueBroker_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmBrokerMaster frmBroker = new FrmBrokerMaster();
                frmBroker.ShowDialog();
                Global.LOOKUPBroker(lueBroker);
            }
        }
        private void lueParty_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmPartyMaster frmParty = new FrmPartyMaster();
                frmParty.ShowDialog();
                Global.LOOKUPParty(lueParty);
            }
        }
        private void dtpPurchaseDate_EditValueChanged(object sender, EventArgs e)
        {
            //DataTable dtFinYear = new DataTable();
            //dtFinYear = objFinYear.GetData();
            //foreach (DataRow drw in dtFinYear.Rows)
            //{
            //    var result = DateTime.Compare(Convert.ToDateTime(dtpInvoiceDate.Text), Convert.ToDateTime(drw["start_date"]));
            //    var result2 = DateTime.Compare(Convert.ToDateTime(dtpInvoiceDate.Text), Convert.ToDateTime(drw["end_date"]));
            //    if (result > 0 && result2 < 0)
            //    {
            //        m_FinYear = Val.ToInt(drw["fin_year_id"]);
            //    }
            //}
        }
        private void txtPremiumPer_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!m_blncheckevents)
                {
                    decimal Premium_amt = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue)) * Val.ToDecimal(txtPremiumPer.Text) / 100, 0);
                    txtPremiumAmt.Text = Premium_amt.ToString();
                    decimal Net_Amount = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue)) + Val.ToDecimal(txtBrokerageAmt.Text) + Val.ToDecimal(txtPremiumAmt.Text) - Val.ToDecimal(txtDiscountAmt.Text), 0);
                    txtNetAmount.Text = Net_Amount.ToString();
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void txtDiscountPer_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!m_blncheckevents)
                {
                    decimal discount_amt = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue)) * Val.ToDecimal(txtDiscountPer.Text) / 100, 0);
                    txtDiscountAmt.Text = discount_amt.ToString();
                    decimal Net_Amount = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue)) + Val.ToDecimal(txtBrokerageAmt.Text) - Val.ToDecimal(txtDiscountAmt.Text), 0);
                    txtNetAmount.Text = Net_Amount.ToString();
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void lueKapan_EditValueChanged(object sender, EventArgs e)
        {
            if (lueKapan.Text != "")
            {
                //DataRow pKapanRow = m_dtbKapanNo.Select("kapan_id =" + Val.ToInt(lueKapan.EditValue));
                foreach (DataRow p_KapanRow in m_dtbKapanNo.Select("kapan_id =" + Val.ToInt(lueKapan.EditValue)))
                {
                    txtCarat.Text = Val.ToString(p_KapanRow["carat"]);
                    txtRate.Text = Val.ToString(p_KapanRow["rate"]);
                    txtAmount.Text = Val.ToString(p_KapanRow["amount"]);
                }
            }
        }
        private void backgroundWorker_MFGPurchase_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.Default;
                Conn = new BeginTranConnection(true, false);
                //double dueDays;
                //dueDays = (Convert.ToDateTime(dtpDueDate.Text) - Convert.ToDateTime(dtpPurchaseDate.Text)).TotalDays;

                MFGRough_SaleProperty objMFRoughSaleProperty = new MFGRough_SaleProperty();
                MFGRoughSale objMFGRoughSale = new MFGRoughSale();
                try
                {
                    IntRes = 0;
                    int IntCounter = 0;
                    int Count = 0;
                    int TotalCount = m_dtbPurchaseDetails.Rows.Count;
                    //Int64 NewHistory_Union_Id = 0;
                    foreach (DataRow drw in m_dtbPurchaseDetails.Rows)
                    {
                        objMFRoughSaleProperty.invoice_date = Val.ToString(dtpInvoiceDate.Text);
                        objMFRoughSaleProperty.sr_no = Val.ToInt(txtSrNo.Text);
                        objMFRoughSaleProperty.invoice_id = Val.ToInt(drw["invoice_id"]);
                        objMFRoughSaleProperty.invoice_no = Val.ToInt(IntRes);
                        objMFRoughSaleProperty.party_id = Val.ToInt(lueParty.EditValue);
                        objMFRoughSaleProperty.broker_id = Val.ToInt(lueBroker.EditValue);
                        objMFRoughSaleProperty.company_id = Val.ToInt(GlobalDec.gEmployeeProperty.company_id);
                        objMFRoughSaleProperty.branch_id = Val.ToInt(GlobalDec.gEmployeeProperty.branch_id);
                        objMFRoughSaleProperty.location_id = Val.ToInt(GlobalDec.gEmployeeProperty.location_id);
                        objMFRoughSaleProperty.department_id = Val.ToInt(GlobalDec.gEmployeeProperty.department_id);
                        objMFRoughSaleProperty.currency_id = Val.ToInt(GlobalDec.gEmployeeProperty.currency_id);
                        objMFRoughSaleProperty.exchange_rate = Val.ToDecimal(txtExchangeRate.Text);
                        objMFRoughSaleProperty.remarks = Val.ToString(txtEntry.Text);
                        objMFRoughSaleProperty.special_remarks = Val.ToString(txtJKK.Text);
                        objMFRoughSaleProperty.client_remarks = Val.ToString(txtSaleRemark.Text);
                        objMFRoughSaleProperty.payment_remarks = Val.ToString(txtAccountRemark.Text);
                        objMFRoughSaleProperty.due_days = Val.ToInt(txtTermDays.Text);
                        objMFRoughSaleProperty.brokerage_per = Val.ToDecimal(txtBrokeragePer.Text);
                        objMFRoughSaleProperty.brokerage_amount = Val.ToDecimal(txtBrokerageAmt.Text);
                        objMFRoughSaleProperty.discount_per = Val.ToDecimal(txtDiscountPer.Text);
                        objMFRoughSaleProperty.discount_amount = Val.ToDecimal(txtDiscountAmt.Text);
                        objMFRoughSaleProperty.premium_per = Val.ToDecimal(txtPremiumPer.Text);
                        objMFRoughSaleProperty.premium_amount = Val.ToDecimal(txtPremiumAmt.Text);
                        objMFRoughSaleProperty.net_amount = Val.ToDecimal(txtNetAmount.Text);
                        //objMFGPurchaseProperty.total_pcs = Math.Round(Val.ToDecimal(clmPcs.SummaryItem.SummaryValue), 2);
                        //objMFGPurchaseProperty.total_carat = Math.Round(Val.ToDecimal(clmDetCarat.SummaryItem.SummaryValue), 3);
                        //objMFGPurchaseProperty.Gross_Amount = Math.Round(Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue), 2);
                        //IntRes = objMFGRoughSale.Save(objMFRoughSaleProperty, DLL.GlobalDec.EnumTran.Start, Conn);
                        //Int64 NewmPurchaseid = Val.ToInt64(objMFRoughSaleProperty.purchase_id);

                        objMFRoughSaleProperty.kapan_id = Val.ToInt(drw["kapan_id"]);
                        objMFRoughSaleProperty.total_carat = Val.ToDecimal(drw["carat"]);
                        objMFRoughSaleProperty.total_rate = Val.ToDecimal(drw["rate"]);
                        objMFRoughSaleProperty.total_amount = Val.ToDecimal(drw["amount"]);
                        //objMFRoughSaleProperty.history_union_id = NewHistory_Union_Id;

                        //objMFRoughSaleProperty.form_id = Val.ToInt(m_numForm_id);
                        objMFRoughSaleProperty = objRoughSale.Save(objMFRoughSaleProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                        IntRes = objMFRoughSaleProperty.invoice_no;
                        //NewHistory_Union_Id = Val.ToInt64(objMFRoughSaleProperty.history_union_id);
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
                finally
                {
                    objMFRoughSaleProperty = null;
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
                        Global.Confirm("Rough Sale Entry Data Save Successfully");
                        ClearDetails();
                        PopulateDetails();
                        objRoughSale = new MFGRoughSale();
                    }
                    else
                    {
                        Global.Confirm("Rough Sale Entry Data Update Successfully");
                        ClearDetails();
                        PopulateDetails();
                    }
                }
                else
                {
                    Global.Confirm("Error In Rough Sale Invoice");
                    dtpInvoiceDate.Focus();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }

        #region "Grid Events" 
        private void dgvMFGPurchaseDetails_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                m_numTotalCarats = Math.Round(Val.ToDecimal(clmDetCarat.SummaryItem.SummaryValue), 2, MidpointRounding.AwayFromZero);

                if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == "rate")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                        m_numTotalAmount = 0;
                    else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                        m_numTotalAmount += (Val.ToDecimal(e.GetValue("carat")) * Val.ToDecimal(e.GetValue("rate")));
                    else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    {
                        if (m_numTotalAmount > 0 && m_numTotalCarats > 0)
                            e.TotalValue = Math.Round((m_numTotalAmount / m_numTotalCarats), 2, MidpointRounding.AwayFromZero);
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
        private void dgvMFGPurchaseDetails_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvMFGRoughSale.GetDataRow(e.RowHandle);
                        btnAdd.Text = "&Update";
                        //lueRoughShade.Text = Val.ToString(Drow["color_name"]);                       
                        txtCarat.Text = Val.ToString(Drow["carat"]);
                        txtRate.Text = Val.ToString(Drow["rate"]);
                        txtAmount.Text = Val.ToString(Drow["amount"]);
                        lueKapan.Text = Val.ToString(Drow["kapan_no"]);
                        m_numcarat = Val.ToDecimal(Drow["carat"]);
                        m_invoice_id = Val.ToInt(Drow["invoice_id"]);
                        m_update_srno = Val.ToInt(Drow["sr_no"]);
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void dgvMFGPurchase_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                objRoughSale = new MFGRoughSale();
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        m_blncheckevents = true;

                        DataRow Drow = dgvMFGRoughSaleList.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt32(Drow["invoice_id"]);
                        txtInvoiceNo.Text = Val.ToString(Drow["invoice_no"]);
                        dtpInvoiceDate.Text = Val.DBDate(Val.ToString(Drow["invoice_date"]));
                        lueParty.EditValue = Val.ToInt(Drow["party_id"]);
                        lueBroker.EditValue = Val.ToInt(Drow["broker_id"]);
                        txtExchangeRate.Text = Val.ToString(Drow["exchange_rate"]);
                        txtTermDays.Text = Val.ToString(Drow["due_days"]);
                        txtEntry.Text = Val.ToString(Drow["remarks"]);
                        txtJKK.Text = Val.ToString(Drow["special_remarks"]);
                        txtAccountRemark.Text = Val.ToString(Drow["payment_remarks"]);
                        txtSaleRemark.Text = Val.ToString(Drow["client_remarks"]);
                        txtBrokeragePer.Text = Val.ToString(Drow["brokerage_per"]);
                        txtBrokerageAmt.Text = Val.ToString(Drow["brokerage_amount"]);
                        txtPremiumPer.Text = Val.ToString(Drow["premium_per"]);
                        txtPremiumAmt.Text = Val.ToString(Drow["premium_amount"]);
                        //txtOtherExpense.Text = Val.ToString(Drow["other_expence"]);
                        txtNetAmount.Text = Val.ToString(Drow["net_amount"]);
                        m_dtbPurchaseDetails = objRoughSale.GetDataDetails(Val.ToInt(txtInvoiceNo.Text));
                        m_kapansale_flag = 1;
                        m_dtbKapanNo = objRoughSale.GetKapan(m_kapansale_flag);
                        lueKapan.Properties.DataSource = m_dtbKapanNo;
                        lueKapan.Properties.ValueMember = "kapan_id";
                        lueKapan.Properties.DisplayMember = "kapan_no";

                        grdMFGRoughSale.DataSource = m_dtbPurchaseDetails;

                        ttlbMFGPurchase.SelectedTabPage = tblBranchdetail;
                        dtpInvoiceDate.Focus();
                        btnBrowse.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void dgvMFGPurchase_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                if (Val.ToDecimal(clmLAmount.SummaryItem.SummaryValue) > 0 && Val.ToDecimal(clmLCarat.SummaryItem.SummaryValue) > 0)
                {
                    m_numSummRate = Math.Round((Val.ToDecimal(clmLAmount.SummaryItem.SummaryValue) / Val.ToDecimal(clmLCarat.SummaryItem.SummaryValue)), 2, MidpointRounding.AwayFromZero);

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

        #region Functions
        private bool LoadDefaults()
        {
            bool blnReturn = true;
            try
            {
                Global.LOOKUPParty(lueParty);
                Global.LOOKUPBroker(lueBroker);
                Global.LOOKUPParty(lueBillToParty);

                m_dtbKapanNo = objRoughSale.GetKapan(m_kapansale_flag);
                lueKapan.Properties.DataSource = m_dtbKapanNo;
                lueKapan.Properties.ValueMember = "kapan_id";
                lueKapan.Properties.DisplayMember = "kapan_no";

                m_opDate = Global.GetDate();
                dtpFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpFromDate.EditValue = Val.DBDate(m_opDate.Rows[0]["opening_date"].ToString());

                dtpToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpToDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpToDate.EditValue = DateTime.Now;

                dtpInvoiceDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpInvoiceDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpInvoiceDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpInvoiceDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpInvoiceDate.EditValue = DateTime.Now;
                dtpInvoiceDate.Focus();
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
                //objMFGPurchase = new SaleInvoice();
                //DataTable p_dtbDetail = new DataTable();

                //p_dtbDetail = objSaleInvoice.GetCheckPriceList(m_numCurrency_id, Val.ToInt(GlobalDec.gEmployeeProperty.rate_type_id));

                //if (p_dtbDetail.Rows.Count <= 0)
                //{
                //    Global.Message("Selected currency type price not found in master please check", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    blnReturn = false;
                //    return blnReturn;
                //}

                //decimal numStockCarat = 0;
                if (btnAdd.Text == "&Add")
                {
                    //DataTable p_dtbStockCarat = new DataTable();
                    //objSaleInvoice = new SaleInvoice();
                    //p_dtbStockCarat = objSaleInvoice.GetStockCarat(GlobalDec.gEmployeeProperty.company_id, GlobalDec.gEmployeeProperty.branch_id, GlobalDec.gEmployeeProperty.location_id, GlobalDec.gEmployeeProperty.department_id, Val.ToInt(lueRoughSieve.EditValue), Val.ToInt(lueRoughShade.EditValue));

                    //if (p_dtbStockCarat.Rows.Count > 0)
                    //{
                    //    numStockCarat = Val.ToDecimal(p_dtbStockCarat.Rows[0]["stock_carat"]);
                    //}

                    //if (numStockCarat < Val.ToDecimal(txtCarat.Text))
                    //{
                    //    Global.Message("Please check enter carat more then stock carat", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    txtCarat.Focus();
                    //    blnReturn = false;
                    //    return blnReturn;
                    //}

                    DataRow[] dr = m_dtbPurchaseDetails.Select("kapan_id = " + Val.ToInt(lueKapan.EditValue));

                    if (dr.Count() == 1)
                    {
                        Global.Message("Record already exists in grid", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lueKapan.Focus();
                        blnReturn = false;
                        return blnReturn;
                    }
                    DataRow drwNew = m_dtbPurchaseDetails.NewRow();
                    decimal numCarat = Val.ToDecimal(txtCarat.Text);
                    decimal numRate = Val.ToDecimal(txtRate.Text);
                    decimal numAmount = Val.ToDecimal(txtAmount.Text);

                    drwNew["invoice_id"] = Val.ToInt(0);
                    drwNew["kapan_id"] = Val.ToInt(lueKapan.EditValue);
                    drwNew["kapan_no"] = Val.ToString(lueKapan.Text);
                    drwNew["carat"] = numCarat;
                    drwNew["rate"] = Val.ToDecimal(txtRate.Text);
                    drwNew["amount"] = Math.Round(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text), 2);
                    drwNew["old_carat"] = Val.ToDecimal(0);
                    drwNew["flag"] = Val.ToInt(0);
                    m_srno = m_srno + 1;
                    drwNew["sr_no"] = Val.ToInt(m_srno);

                    //drwNew["current_rate"] = m_current_rate;
                    //drwNew["current_amount"] = m_current_amount;

                    m_dtbPurchaseDetails.Rows.Add(drwNew);

                    dgvMFGRoughSale.MoveLast();


                    decimal Expence_Amt = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) + Val.ToDecimal(txtBrokerageAmt.Text)) + Val.ToDecimal(txtPremiumAmt.Text) - Val.ToDecimal(txtDiscountAmt.Text), 0);
                    txtNetAmount.Text = Expence_Amt.ToString();
                }
                else if (btnAdd.Text == "&Update")
                {
                    //objSaleInvoice = new SaleInvoice();
                    //if (Val.ToDecimal(txtCarat.Text) > m_numcarat)
                    //{
                    //    if (m_invoice_detail_id == 0)
                    //    {
                    //        DataTable p_dtbStockCarat = new DataTable();
                    //        p_dtbStockCarat = objSaleInvoice.GetStockCarat(GlobalDec.gEmployeeProperty.company_id, GlobalDec.gEmployeeProperty.branch_id, GlobalDec.gEmployeeProperty.location_id, GlobalDec.gEmployeeProperty.department_id, Val.ToInt(lueRoughSieve.EditValue), Val.ToInt(lueRoughShade.EditValue));

                    //        if (p_dtbStockCarat.Rows.Count > 0)
                    //        {
                    //            numStockCarat = Val.ToDecimal(p_dtbStockCarat.Rows[0]["stock_carat"]);
                    //        }

                    //        if (numStockCarat < Val.ToDecimal(txtCarat.Text))
                    //        {
                    //            Global.Message("Please check enter carat more then stock carat", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //            txtCarat.Focus();
                    //            blnReturn = false;
                    //            return blnReturn;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        DataTable p_dtbStockCarat = new DataTable();
                    //        p_dtbStockCarat = objSaleInvoice.GetStockCarat(GlobalDec.gEmployeeProperty.company_id, GlobalDec.gEmployeeProperty.branch_id, GlobalDec.gEmployeeProperty.location_id, GlobalDec.gEmployeeProperty.department_id, Val.ToInt(lueRoughSieve.EditValue), Val.ToInt(lueRoughShade.EditValue));

                    //        decimal p_numdiff_Carat = Val.ToDecimal(txtCarat.Text) - m_numcarat;

                    //        if (p_dtbStockCarat.Rows.Count > 0)
                    //        {
                    //            numStockCarat = Val.ToDecimal(p_dtbStockCarat.Rows[0]["stock_carat"]);
                    //        }

                    //        if (numStockCarat < Val.ToDecimal(p_numdiff_Carat))
                    //        {
                    //            Global.Message("Please check enter carat more then stock carat", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //            txtCarat.Focus();
                    //            blnReturn = false;
                    //            return blnReturn;
                    //        }
                    //    }

                    //}

                    if (m_dtbPurchaseDetails.Select("kapan_id = " + Val.ToInt(lueKapan.EditValue)).Length > 0)
                    {
                        for (int i = 0; i < m_dtbPurchaseDetails.Rows.Count; i++)
                        {
                            if (m_dtbPurchaseDetails.Select("invoice_id ='" + m_invoice_id + "' AND sr_no = '" + m_update_srno + "'").Length > 0)
                            {
                                if (m_dtbPurchaseDetails.Rows[m_update_srno - 1]["invoice_id"].ToString() == m_invoice_id.ToString())
                                {
                                    m_dtbPurchaseDetails.Rows[m_update_srno - 1]["carat"] = Val.ToDecimal(txtCarat.Text).ToString();
                                    m_dtbPurchaseDetails.Rows[m_update_srno - 1]["rate"] = Val.ToDecimal(txtRate.Text);
                                    m_dtbPurchaseDetails.Rows[m_update_srno - 1]["flag"] = 1;
                                    m_dtbPurchaseDetails.Rows[m_update_srno - 1]["amount"] = Math.Round(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text), 2);
                                    //m_dtbPurchaseDetails.Rows[m_update_srno - 1]["current_rate"] = m_current_rate;
                                    //m_dtbPurchaseDetails.Rows[m_update_srno - 1]["current_amount"] = m_current_amount;
                                    decimal Expence_Amt = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) + Val.ToDecimal(txtBrokerageAmt.Text)) + Val.ToDecimal(txtPremiumAmt.Text) + Val.ToDecimal(txtOtherExpense.Text), 0);
                                    txtNetAmount.Text = Expence_Amt.ToString();
                                    break;
                                }
                            }
                        }
                        btnAdd.Text = "&Add";
                    }
                    else
                    {
                        for (int i = 0; i < m_dtbPurchaseDetails.Rows.Count; i++)
                        {
                            if (m_dtbPurchaseDetails.Select("invoice_id ='" + m_invoice_id + "' AND sr_no = '" + m_update_srno + "'").Length > 0)
                            {
                                if (m_dtbPurchaseDetails.Rows[m_update_srno - 1]["invoice_id"].ToString() == m_invoice_id.ToString())
                                {
                                    m_dtbPurchaseDetails.Rows[m_update_srno - 1]["carat"] = Val.ToDecimal(txtCarat.Text).ToString();
                                    m_dtbPurchaseDetails.Rows[m_update_srno - 1]["rate"] = Val.ToDecimal(txtRate.Text);
                                    m_dtbPurchaseDetails.Rows[m_update_srno - 1]["flag"] = 1;

                                    m_dtbPurchaseDetails.Rows[m_update_srno - 1]["amount"] = Math.Round(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text), 2);
                                    //m_dtbPurchaseDetails.Rows[m_update_srno - 1]["current_rate"] = m_current_rate;
                                    //m_dtbPurchaseDetails.Rows[m_update_srno - 1]["current_amount"] = m_current_amount;
                                    decimal Expence_Amt = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) + Val.ToDecimal(txtBrokerageAmt.Text)) + Val.ToDecimal(txtPremiumAmt.Text) + Val.ToDecimal(txtOtherExpense.Text), 0);
                                    txtNetAmount.Text = Expence_Amt.ToString();
                                }
                            }
                        }
                        btnAdd.Text = "&Add";
                    }
                    dgvMFGRoughSale.MoveLast();
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
                    if (m_dtbPurchaseDetails.Rows.Count == 0)
                    {
                        lstError.Add(new ListError(22, "Record"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                        }
                    }
                    if (dgvMFGRoughSale == null)
                    {
                        lstError.Add(new ListError(22, "Record"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                        }
                    }
                    var result = DateTime.Compare(Convert.ToDateTime(dtpInvoiceDate.Text), DateTime.Today);
                    if (result > 0)
                    {
                        lstError.Add(new ListError(5, " Invoice Date Not Be Greater Than Today Date"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            dtpInvoiceDate.Focus();
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

                }
                if (m_blnadd)
                {

                    if (lueKapan.Text == "")
                    {
                        lstError.Add(new ListError(13, "Kapan"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueKapan.Focus();
                        }
                    }

                    if (Val.ToDouble(txtCarat.Text) == 0)
                    {
                        lstError.Add(new ListError(12, "Carat"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtCarat.Focus();
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
                lueBroker.EditValue = System.DBNull.Value;
                lueKapan.EditValue = System.DBNull.Value;
                txtTermDays.Text = string.Empty;
                txtExchangeRate.Text = string.Empty;

                txtSearchInvoice.Text = string.Empty;
                lueBillToParty.EditValue = System.DBNull.Value;
                dtpInvoiceDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpInvoiceDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpInvoiceDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpInvoiceDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpInvoiceDate.EditValue = DateTime.Now;

                m_opDate = Global.GetDate();
                dtpFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpFromDate.EditValue = Val.DBDate(m_opDate.Rows[0]["opening_date"].ToString());

                dtpDueDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpDueDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpDueDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpDueDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpDueDate.EditValue = DateTime.Now;

                m_kapansale_flag = 0;
                m_dtbKapanNo = objRoughSale.GetKapan(m_kapansale_flag);
                lueKapan.Properties.DataSource = m_dtbKapanNo;
                lueKapan.Properties.ValueMember = "kapan_id";
                lueKapan.Properties.DisplayMember = "kapan_no";

                txtInvoiceNo.Text = string.Empty;
                txtCarat.Text = string.Empty;
                txtRate.Text = string.Empty;
                txtAmount.Text = string.Empty;

                txtEntry.Text = string.Empty;
                txtJKK.Text = string.Empty;
                txtAccountRemark.Text = string.Empty;
                txtSaleRemark.Text = string.Empty;

                txtBrokeragePer.Text = string.Empty;
                txtBrokerageAmt.Text = string.Empty;
                txtPremiumPer.Text = string.Empty;
                txtPremiumAmt.Text = string.Empty;
                txtOtherExpense.Text = string.Empty;
                txtNetAmount.Text = string.Empty;
                m_srno = 0;
                btnAdd.Text = "&Add";
                dtpInvoiceDate.Focus();
                btnBrowse.Enabled = true;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
        }
        //private bool DeleteDetail()
        //{
        //    bool blnReturn = true;
        //    MFGRough_SaleProperty objMFGRoughSaleProperty = new MFGRough_SaleProperty();
        //   MFGRoughSale objMFGRoughSale = new MFGRoughSale();
        //    try
        //    {
        //        if (Val.ToInt(lblMode.Tag) != 0)
        //        {
        //            DialogResult result = MessageBox.Show("Do you want to Delete data?", "Confirmation", MessageBoxButtons.YesNoCancel);
        //            if (result != DialogResult.Yes)
        //            {
        //                blnReturn = false;
        //                return blnReturn;
        //            }

        //            objMFGRoughSaleProperty.invoice_id = Val.ToInt(lblMode.Tag);

        //            int IntRes = objMFGPurchase.Delete(objMFGRoughSaleProperty, m_dtbPurchaseDetails);

        //            if (IntRes == -1)
        //            {
        //                Global.Confirm("Error In Rough Sale Invoice");
        //                lueKapan.Focus();
        //            }
        //            else
        //            {
        //                if (Val.ToInt(lblMode.Tag) == 0)
        //                {
        //                    Global.Confirm("Rough Sale Data Delete Successfully");
        //                }
        //                else
        //                {
        //                    Global.Confirm("Rough Sale Data Delete Successfully");
        //                }

        //            }
        //        }
        //        else
        //        {
        //            Global.Message("Invoice ID not found");
        //            blnReturn = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        General.ShowErrors(ex.ToString());
        //        blnReturn = false;
        //    }
        //    finally
        //    {
        //        objMFGPurchaseProperty = null;
        //    }

        //    return blnReturn;
        //}
        private bool PopulateDetails()
        {
            objRoughSale = new MFGRoughSale();
            bool blnReturn = true;
            DateTime datFromDate = DateTime.MinValue;
            DateTime datToDate = DateTime.MinValue;
            try
            {
                m_dtbDetails = objRoughSale.GetData(Val.DBDate(dtpFromDate.Text), Val.DBDate(dtpToDate.Text), Val.ToString(txtSearchInvoice.Text), Val.ToInt32(lueBillToParty.EditValue));

                if (m_dtbDetails.Rows.Count == 0)
                {
                    Global.Message("Data Not Found");
                    blnReturn = false;
                }
                grdMFGRoughSaleList.DataSource = m_dtbDetails;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            finally
            {
                objRoughSale = null;
            }

            return blnReturn;
        }
        private bool GeneratePurchaseDetails()
        {
            bool blnReturn = true;
            try
            {
                if (m_dtbPurchaseDetails.Rows.Count > 0)
                    m_dtbPurchaseDetails.Rows.Clear();

                m_dtbPurchaseDetails = new DataTable();

                m_dtbPurchaseDetails.Columns.Add("sr_no", typeof(int));
                m_dtbPurchaseDetails.Columns.Add("invoice_id", typeof(int));
                m_dtbPurchaseDetails.Columns.Add("kapan_id", typeof(int));
                m_dtbPurchaseDetails.Columns.Add("kapan_no", typeof(string));
                m_dtbPurchaseDetails.Columns.Add("carat", typeof(decimal)).DefaultValue = 0;
                m_dtbPurchaseDetails.Columns.Add("rate", typeof(decimal)).DefaultValue = 0;
                m_dtbPurchaseDetails.Columns.Add("amount", typeof(decimal)).DefaultValue = 0;
                m_dtbPurchaseDetails.Columns.Add("old_carat", typeof(decimal));
                m_dtbPurchaseDetails.Columns.Add("flag", typeof(int)).DefaultValue = 0;
                m_dtbPurchaseDetails.Columns.Add("current_rate", typeof(decimal)).DefaultValue = 0;
                m_dtbPurchaseDetails.Columns.Add("current_amount", typeof(decimal)).DefaultValue = 0;

                grdMFGRoughSale.DataSource = m_dtbPurchaseDetails;
                grdMFGRoughSale.Refresh();
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
                            dgvMFGRoughSaleList.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvMFGRoughSaleList.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvMFGRoughSaleList.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvMFGRoughSaleList.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvMFGRoughSaleList.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvMFGRoughSaleList.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvMFGRoughSaleList.ExportToCsv(Filepath);
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