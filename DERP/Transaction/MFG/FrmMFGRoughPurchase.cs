using BLL;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Utility;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using DERP.Master;
using DERP.Master.MFG;
using DERP.Report;
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
    public partial class FrmMFGRoughPurchase : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        Control _NextEnteredControl;
        private List<Control> _tabControls;
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        MFGPurchase objMFGPurchase;
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
        DataTable m_dtbSievecheck;
        DataTable m_dtbSubSievecheck;
        DataTable m_dtbAssortscheck;
        DataTable m_dtbSievedtl;
        DataTable m_dtbAssortsdtl;
        DataTable m_dtbSubSievedtl;

        int m_purchase_detail_id;
        int m_srno;
        int m_update_srno;
        int IntRes;
        int m_FinYear;
        int m_numForm_id;

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
        public FrmMFGRoughPurchase()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            _NextEnteredControl = new Control();
            _tabControls = new List<Control>();

            objMFGPurchase = new MFGPurchase();
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
            m_dtbSievecheck = new DataTable();
            m_dtbSubSievecheck = new DataTable();
            m_dtbAssortscheck = new DataTable();
            m_dtbSievedtl = new DataTable();
            m_dtbAssortsdtl = new DataTable();
            m_dtbSubSievedtl = new DataTable();

            m_purchase_detail_id = 0;
            m_srno = 0;
            m_update_srno = 0;
            IntRes = 0;
            m_FinYear = 0;
            m_numForm_id = 0;

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
            objBOFormEvents.ObjToDispose.Add(objMFGPurchase);
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
                lueRoughSieve.Focus();
                lueRoughSieve.ShowPopup();
                lueRoughSieve.EditValue = DBNull.Value;
                txtCarat.Text = string.Empty;
                txtRate.Text = string.Empty;
                txtAmount.Text = string.Empty;
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
            //string Str = "";
            //if (Val.DBDate(BLL.GlobalDec.gStrServerDate) != Val.DBDate(dtpPurchaseDate.Text))
            //{
            //    Str = GlobalDec.CheckLockIsOpenOrNot(Val.DBDate(dtpPurchaseDate.Text), Val.DBTime(DateTime.Now.ToShortTimeString())); //Val.GetFullTime12());
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
            //        dtpPurchaseDate.Enabled = true;
            //        dtpPurchaseDate.Visible = true;
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
            MFG_PurchaseProperty.date = Val.DBDate(dtpPurchaseDate.Text);
            MFG_PurchaseProperty.purchase_id = Val.ToInt32(lblMode.Tag);

            DataTable dtpur = new DataTable();
            dtpur = objMFGPurchase.GetPrintData(MFG_PurchaseProperty);

            FrmReportViewer FrmReportViewer = new FrmReportViewer();
            FrmReportViewer.DS.Tables.Add(dtpur);
            FrmReportViewer.GroupBy = "";
            FrmReportViewer.RepName = "";
            FrmReportViewer.RepPara = "";
            this.Cursor = Cursors.Default;
            FrmReportViewer.AllowSetFormula = true;

            FrmReportViewer.ShowForm("Sale_Invoice_Sum", 120, FrmReportViewer.ReportFolder.ACCOUNT);
            MFG_PurchaseProperty = null;
            dtpur = null;
            FrmReportViewer.DS.Tables.Clear();
            FrmReportViewer.DS.Clear();
            FrmReportViewer = null;
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
            }
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
                grdMFGPurchaseDetails.DataSource = null;


                if (txtFileName.Text.Length != 0)
                {
                    using (var pck = new ExcelPackage(new FileInfo(txtFileName.Text)))
                    {
                        ExcelWorksheet ws = pck.Workbook.Worksheets[1];
                        m_dtbPurchaseDetails = WorksheetToDataTable(ws, true);
                    }
                }

                m_dtbSievecheck = new SieveMaster().GetData();
                m_dtbAssortscheck = new AssortMaster().GetData();
                m_dtbSubSievecheck = new SubSieveMaster().GetData();

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
                            if (m_dtbAssortscheck.Select("sieve_name ='" + Val.ToString(DRow["sieve_name"]) + "'").Length > 0)
                            {
                                m_dtbAssortsdtl = m_dtbAssortscheck.Select("sieve_name ='" + Val.ToString(DRow["sieve_name"]) + "'").CopyToDataTable();
                            }
                            else
                            {
                                Global.Message("Sieve Name Not found in Master : " + Val.ToString(DRow["sieve_name"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
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
                            if (m_dtbSievecheck.Select("color_name ='" + Val.ToString(DRow["color_name"]) + "'").Length > 0)
                            {
                                m_dtbSievedtl = m_dtbSievecheck.Select("color_name ='" + Val.ToString(DRow["color_name"]) + "'").CopyToDataTable();
                            }
                            else
                            {
                                Global.Message("Color Not found in Master : " + Val.ToString(DRow["color_name"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Cursor = Cursors.Default;
                                return;
                            }
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


                    DRow["rough_sieve_id"] = Val.ToInt(m_dtbAssortsdtl.Rows[0]["rough_sieve_id"]);
                    DRow["color_id"] = Val.ToInt(m_dtbSievedtl.Rows[0]["color_id"]);

                    //string p_numStockRate = string.Empty;
                    //p_numStockRate = objBranch.GetLetestPrice(Val.ToInt(m_dtbAssortsdtl.Rows[0]["assort_id"]), Val.ToInt(m_dtbSievedtl.Rows[0]["sieve_id"]));
                    //m_current_rate = Val.ToDecimal(p_numStockRate);
                    //DRow["current_rate"] = m_current_rate;
                    //DRow["current_amount"] = Val.ToDecimal(m_current_rate) * Val.ToDecimal(DRow["carat"]);
                    m_srno = m_srno + 1;
                    DRow["sr_no"] = Val.ToInt(m_srno);
                }

                grdMFGPurchaseDetails.DataSource = m_dtbPurchaseDetails;
                dgvMFGPurchaseDetails.MoveLast();
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
                if (dtpPurchaseDate.Text.Length <= 0 || dtpPurchaseDate.Text == "")
                {
                    txtTermDays.Text = "";
                    dtpDueDate.EditValue = null;
                }
                else
                {
                    Double Days = Val.ToDouble(txtTermDays.Text);
                    DateTime Date = Convert.ToDateTime(dtpPurchaseDate.EditValue).AddDays(Val.ToDouble(Days));
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
                    decimal Net_Amount = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue)) + Val.ToDecimal(txtBrokerageAmt.Text) + Val.ToDecimal(txtPremiumAmt.Text) + Val.ToDecimal(txtOtherExpense.Text), 0);
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

            DataTable dtFinYear = new DataTable();
            dtFinYear = objFinYear.GetData();
            foreach (DataRow drw in dtFinYear.Rows)
            {
                var result = DateTime.Compare(Convert.ToDateTime(dtpPurchaseDate.Text), Convert.ToDateTime(drw["start_date"]));
                var result2 = DateTime.Compare(Convert.ToDateTime(dtpPurchaseDate.Text), Convert.ToDateTime(drw["end_date"]));
                if (result > 0 && result2 < 0)
                {
                    m_FinYear = Val.ToInt(drw["fin_year_id"]);
                }

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
      
        private void lueRoughType_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmMfgRoughTypeMaster frmRoughType = new FrmMfgRoughTypeMaster();
                frmRoughType.ShowDialog();
                Global.LOOKUPRoughType(lueRoughType);
            }
        }
        private void txtPremiumPer_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!m_blncheckevents)
                {
                    decimal Premium_amt = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue)) * Val.ToDecimal(txtPremiumPer.Text) / 100, 0);
                    txtPremiumAmt.Text = Premium_amt.ToString();
                    decimal Net_Amount = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue)) + Val.ToDecimal(txtBrokerageAmt.Text) + Val.ToDecimal(txtPremiumAmt.Text) + Val.ToDecimal(txtOtherExpense.Text), 0);
                    txtNetAmount.Text = Net_Amount.ToString();
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
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

                MFG_PurchaseProperty objMFGPurchaseProperty = new MFG_PurchaseProperty();
                MFGPurchase objMFGPurchase = new MFGPurchase();
                try
                {
                    IntRes = 0;

                    objMFGPurchaseProperty.purchase_id = Val.ToInt(lblMode.Tag);
                    //objMFGPurchaseProperty.party_invoice_no = Val.ToString(txtPartyInvoiceNo.Text);
                    objMFGPurchaseProperty.fin_year_id = m_FinYear;
                    //objMFGPurchaseProperty.invoice_no = Val.ToString(txtInvoiceNo.Text);
                    objMFGPurchaseProperty.party_id = Val.ToInt(lueParty.EditValue);
                    objMFGPurchaseProperty.broker_id = Val.ToInt(lueBroker.EditValue);
                    //objMFGPurchaseProperty.sight_type_id = Val.ToInt(lueSightType.EditValue);
                    //objMFGPurchaseProperty.source_id = Val.ToInt(lueSource.EditValue);
                    //objMFGPurchaseProperty.article_id = Val.ToInt(lueSightType.EditValue);
                    //objMFGPurchaseProperty.group_id = Val.ToInt(lueGroup.EditValue);
                    //objMFGPurchaseProperty.team_id = Val.ToInt(lueTeam.EditValue);
                    //objMFGPurchaseProperty.janged_sieve_id = Val.ToInt(lueJangedSieve.EditValue);
                    objMFGPurchaseProperty.rough_type_id = Val.ToInt(lueRoughType.EditValue);
                    objMFGPurchaseProperty.company_id = Val.ToInt(GlobalDec.gEmployeeProperty.company_id);
                    objMFGPurchaseProperty.branch_id = Val.ToInt(GlobalDec.gEmployeeProperty.branch_id);
                    objMFGPurchaseProperty.location_id = Val.ToInt(GlobalDec.gEmployeeProperty.location_id);
                    objMFGPurchaseProperty.department_id = Val.ToInt(GlobalDec.gEmployeeProperty.department_id);
                    objMFGPurchaseProperty.currency_id = Val.ToInt(GlobalDec.gEmployeeProperty.currency_id);
                    objMFGPurchaseProperty.exchange_rate = Val.ToDecimal(txtExchangeRate.Text);
                    objMFGPurchaseProperty.remarks = Val.ToString(txtEntry.Text);
                    objMFGPurchaseProperty.special_remarks = Val.ToString(txtJKK.Text);
                    objMFGPurchaseProperty.client_remarks = Val.ToString(txtSaleRemark.Text);
                    objMFGPurchaseProperty.payment_remarks = Val.ToString(txtAccountRemark.Text);
                    objMFGPurchaseProperty.due_days = Val.ToInt(txtTermDays.Text);
                    objMFGPurchaseProperty.brokrage_per = Val.ToDecimal(txtBrokeragePer.Text);
                    objMFGPurchaseProperty.brokrage_amount = Val.ToDecimal(txtBrokerageAmt.Text);
                    objMFGPurchaseProperty.premium_per = Val.ToDecimal(txtPremiumPer.Text);
                    objMFGPurchaseProperty.premium_amount = Val.ToDecimal(txtPremiumAmt.Text);
                    objMFGPurchaseProperty.date = Val.DBDate(dtpPurchaseDate.Text);
                    objMFGPurchaseProperty.other_expence = Val.ToDecimal(txtOtherExpense.Text);
                    objMFGPurchaseProperty.net_amount = Val.ToDecimal(txtNetAmount.Text);
                    objMFGPurchaseProperty.form_id = Val.ToInt(m_numForm_id);
                    //objMFGPurchaseProperty.total_pcs = Math.Round(Val.ToDecimal(clmPcs.SummaryItem.SummaryValue), 2);
                    //objMFGPurchaseProperty.total_carat = Math.Round(Val.ToDecimal(clmDetCarat.SummaryItem.SummaryValue), 3);
                    //objMFGPurchaseProperty.Gross_Amount = Math.Round(Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue), 2);
                    objMFGPurchaseProperty = objMFGPurchase.Save(objMFGPurchaseProperty, DLL.GlobalDec.EnumTran.Start, Conn);
                    Int64 NewmPurchaseid = Val.ToInt64(objMFGPurchaseProperty.purchase_id);

                    int IntCounter = 0;
                    int Count = 0;
                    int TotalCount = m_dtbPurchaseDetails.Rows.Count;

                    foreach (DataRow drw in m_dtbPurchaseDetails.Rows)
                    {
                        objMFGPurchaseProperty = new MFG_PurchaseProperty();
                        //objMFGPurchaseProperty.lot_id = Val.ToInt32(NewmInvoiceid);
                        objMFGPurchaseProperty.lot_id = Val.ToInt(drw["lot_id"]);
                        objMFGPurchaseProperty.rough_sieve_id = Val.ToInt(drw["rough_sieve_id"]);
                        objMFGPurchaseProperty.rough_shade_id = Val.ToInt(drw["rough_shade_id"]);
                        objMFGPurchaseProperty.pcs = Val.ToInt(drw["pcs"]);
                        objMFGPurchaseProperty.carat = Val.ToDecimal(drw["carat"]);
                        objMFGPurchaseProperty.rate = Val.ToDecimal(drw["rate"]);
                        objMFGPurchaseProperty.amount = Val.ToDecimal(drw["amount"]);
                        objMFGPurchaseProperty.company_id = Val.ToInt(GlobalDec.gEmployeeProperty.company_id);
                        objMFGPurchaseProperty.branch_id = Val.ToInt(GlobalDec.gEmployeeProperty.branch_id);
                        objMFGPurchaseProperty.location_id = Val.ToInt(GlobalDec.gEmployeeProperty.location_id);
                        objMFGPurchaseProperty.department_id = Val.ToInt(GlobalDec.gEmployeeProperty.department_id);
                        objMFGPurchaseProperty.form_id = Val.ToInt(m_numForm_id);
                        objMFGPurchaseProperty = objMFGPurchase.Save_MfgStock_Purchase(objMFGPurchaseProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                        Int64 NewmLotid = Val.ToInt64(objMFGPurchaseProperty.lot_id);

                        objMFGPurchaseProperty.purchase_id = Val.ToInt32(NewmPurchaseid);
                        objMFGPurchaseProperty.lot_id = Val.ToInt32(NewmLotid);
                        objMFGPurchaseProperty.purchase_detail_id = Val.ToInt(drw["purchase_detail_id"]);

                        objMFGPurchaseProperty.rough_type_id = Val.ToInt(drw["rough_type_id"]);

                        IntRes = objMFGPurchase.Save_Detail(objMFGPurchaseProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

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
                    objMFGPurchaseProperty = null;
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
                        Global.Confirm("Purchase Entry Data Save Successfully");
                        ClearDetails();
                        PopulateDetails();
                    }
                    else
                    {
                        Global.Confirm("Purchase Entry Data Update Successfully");
                        ClearDetails();
                        PopulateDetails();
                    }
                }
                else
                {
                    Global.Confirm("Error In Purchase Invoice");
                    //txtPartyInvoiceNo.Focus();
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
                m_numTotalCarats = Math.Round(Val.ToDecimal(clmCarats.SummaryItem.SummaryValue), 2, MidpointRounding.AwayFromZero);

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
                        DataRow Drow = dgvMFGPurchaseDetails.GetDataRow(e.RowHandle);
                        btnAdd.Text = "&Update";
                        //lueRoughShade.Text = Val.ToString(Drow["color_name"]);
                        //lueRoughShade.EditValue = Val.ToInt(Drow["rough_shade_id"]);
                        //lueRoughSieve.Text = Val.ToString(Drow["sieve_name"]);
                        lueRoughSieve.EditValue = Val.ToInt(Drow["rough_sieve_id"]);
                        lueRoughType.EditValue = Val.ToInt(Drow["rough_type_id"]);
                        //txtPcs.Text = Val.ToString(Drow["pcs"]);
                        txtCarat.Text = Val.ToString(Drow["carat"]);
                        txtRate.Text = Val.ToString(Drow["rate"]);
                        txtAmount.Text = Val.ToString(Drow["amount"]);
                        m_numcarat = Val.ToDecimal(Drow["carat"]);
                        m_purchase_detail_id = Val.ToInt(Drow["purchase_detail_id"]);
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
                objMFGPurchase = new MFGPurchase();
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        m_blncheckevents = true;

                        DataRow Drow = dgvMFGPurchase.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt32(Drow["purchase_id"]);
                        //txtInvoiceNo.Text = Val.ToString(Drow["invoice_no"]);
                        //txtPartyInvoiceNo.Text = Val.ToString(Drow["party_invoice_no"]);
                        dtpPurchaseDate.Text = Val.DBDate(Val.ToString(Drow["purchase_date"]));
                        lueParty.EditValue = Val.ToInt(Drow["party_id"]);
                        lueBroker.EditValue = Val.ToInt(Drow["broker_id"]);
                        //lueSightType.EditValue = Val.ToInt(Drow["sight_type_id"]);
                        //lueSource.EditValue = Val.ToInt(Drow["source_id"]);
                        //lueTeam.EditValue = Val.ToInt(Drow["team_id"]);
                        //lueArticle.EditValue = Val.ToInt(Drow["article_id"]);
                        //lueGroup.EditValue = Val.ToInt(Drow["group_id"]);
                        //lueJangedSieve.EditValue = Val.ToInt(Drow["janged_sieve_id"]);
                        lueRoughType.EditValue = Val.ToInt(Drow["rough_type_id"]);
                        txtExchangeRate.Text = Val.ToString(Drow["exchange_rate"]);
                        txtTermDays.Text = Val.ToString(Drow["due_days"]);
                        txtEntry.Text = Val.ToString(Drow["remarks"]);
                        txtJKK.Text = Val.ToString(Drow["special_remarks"]);
                        txtAccountRemark.Text = Val.ToString(Drow["payment_remarks"]);
                        txtSaleRemark.Text = Val.ToString(Drow["client_remarks"]);
                        txtBrokeragePer.Text = Val.ToString(Drow["brokrage_per"]);
                        txtBrokerageAmt.Text = Val.ToString(Drow["brokrage_amount"]);
                        txtPremiumPer.Text = Val.ToString(Drow["premium_per"]);
                        txtPremiumAmt.Text = Val.ToString(Drow["premium_amount"]);
                        txtOtherExpense.Text = Val.ToString(Drow["other_expence"]);
                        txtNetAmount.Text = Val.ToString(Drow["net_amount"]);
                        m_dtbPurchaseDetails = objMFGPurchase.GetDataDetails(Val.ToInt(lblMode.Tag));

                        grdMFGPurchaseDetails.DataSource = m_dtbPurchaseDetails;

                        ttlbMFGPurchase.SelectedTabPage = tblBranchdetail;
                        //txtPartyInvoiceNo.Focus();
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
                if (Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) > 0 && Val.ToDecimal(clmDetCarat.SummaryItem.SummaryValue) > 0)
                {
                    m_numSummRate = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) / Val.ToDecimal(clmDetCarat.SummaryItem.SummaryValue)), 2, MidpointRounding.AwayFromZero);

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
                Global.LOOKUPRoughType(lueRoughType);
                Global.LOOKUPRoughSieve(lueRoughSieve);

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

                dtpPurchaseDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpPurchaseDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpPurchaseDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpPurchaseDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpPurchaseDate.EditValue = DateTime.Now;
                //txtPartyInvoiceNo.Focus();
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
                //if (Val.ToInt(lblMode.Tag) > 0)
                //{
                //    DataTable dtCheckKapan = new DataTable();
                //    dtCheckKapan = objMFGPurchase.CheckPurchaseToKapan(Val.ToInt(lblMode.Tag));
                //    if (dtCheckKapan.Rows.Count > 0)
                //    {
                //        Global.Message("Purchase not Update because Kapan is Created");
                //        blnReturn = false;
                //        return blnReturn;
                //    }
                //}
                if (!ValidateDetails())
                {
                    m_blnadd = false;
                    blnReturn = false;
                    return blnReturn;
                }

                if (btnAdd.Text == "&Add")
                {
                    DataRow drwNew = m_dtbPurchaseDetails.NewRow();
                    decimal numCarat = Val.ToDecimal(txtCarat.Text);
                    decimal numRate = Val.ToDecimal(txtRate.Text);
                    decimal numAmount = Val.ToDecimal(txtAmount.Text);
                    

                    drwNew["invoice_id"] = Val.ToInt(0);
                    drwNew["purchase_detail_id"] = Val.ToInt(0);
                    drwNew["lot_id"] = Val.ToInt(0);
                    drwNew["rough_sieve_id"] = Val.ToInt(lueRoughSieve.EditValue);
                    drwNew["sieve_name"] = Val.ToString(lueRoughSieve.Text);


                    drwNew["carat"] = numCarat;
                    drwNew["rate"] = Val.ToDecimal(txtRate.Text);
                    drwNew["amount"] = Math.Round(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text), 2);
                    drwNew["flag"] = Val.ToInt(0);
                    m_srno = m_srno + 1;
                    drwNew["sr_no"] = Val.ToInt(m_srno);

                   
                    m_dtbPurchaseDetails.Rows.Add(drwNew);

                    dgvMFGPurchaseDetails.MoveLast();


                    decimal Expence_Amt = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) + Val.ToDecimal(txtBrokerageAmt.Text)) + Val.ToDecimal(txtPremiumAmt.Text) + Val.ToDecimal(txtOtherExpense.Text), 0);
                    txtNetAmount.Text = Expence_Amt.ToString();
                }
                //else if (btnAdd.Text == "&Update")
                //{
                //    if (m_dtbPurchaseDetails.Select("rough_shade_id = " + Val.ToInt(lueRoughShade.EditValue) + " AND rough_sieve_id = " + Val.ToInt(lueRoughSieve.EditValue)).Length > 0)
                //    {
                //        for (int i = 0; i < m_dtbPurchaseDetails.Rows.Count; i++)
                //        {
                //            if (m_dtbPurchaseDetails.Select("purchase_detail_id ='" + m_purchase_detail_id + "' AND sr_no = '" + m_update_srno + "'").Length > 0)
                //            {
                //                //if (m_dtbPurchaseDetails.Rows[m_update_srno - 1]["purchase_detail_id"].ToString() == m_purchase_detail_id.ToString())
                //                if (m_dtbPurchaseDetails.Rows[dgvMFGPurchaseDetails.FocusedRowHandle]["purchase_detail_id"].ToString() == m_purchase_detail_id.ToString())
                //                {
                //                    m_dtbPurchaseDetails.Rows[dgvMFGPurchaseDetails.FocusedRowHandle]["carat"] = Val.ToDecimal(txtCarat.Text).ToString();
                //                    m_dtbPurchaseDetails.Rows[dgvMFGPurchaseDetails.FocusedRowHandle]["pcs"] = Val.ToInt(txtPcs.Text);
                //                    m_dtbPurchaseDetails.Rows[dgvMFGPurchaseDetails.FocusedRowHandle]["rate"] = Val.ToDecimal(txtRate.Text);
                //                    m_dtbPurchaseDetails.Rows[dgvMFGPurchaseDetails.FocusedRowHandle]["flag"] = 1;
                //                    m_dtbPurchaseDetails.Rows[dgvMFGPurchaseDetails.FocusedRowHandle]["amount"] = Math.Round(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text), 2);
                //                    m_dtbPurchaseDetails.Rows[dgvMFGPurchaseDetails.FocusedRowHandle]["rough_type_id"] = Val.ToInt(lueRoughType.EditValue);
                //                    m_dtbPurchaseDetails.Rows[dgvMFGPurchaseDetails.FocusedRowHandle]["rough_type"] = Val.ToString(lueRoughType.Text);
                //                    //m_dtbPurchaseDetails.Rows[m_update_srno - 1]["carat"] = Val.ToDecimal(txtCarat.Text).ToString();
                //                    //m_dtbPurchaseDetails.Rows[m_update_srno - 1]["pcs"] = Val.ToInt(txtPcs.Text);
                //                    //m_dtbPurchaseDetails.Rows[m_update_srno - 1]["rate"] = Val.ToDecimal(txtRate.Text);
                //                    //m_dtbPurchaseDetails.Rows[m_update_srno - 1]["flag"] = 1;
                //                    //m_dtbPurchaseDetails.Rows[m_update_srno - 1]["amount"] = Math.Round(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text), 2);
                //                    decimal Expence_Amt = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) + Val.ToDecimal(txtBrokerageAmt.Text)) + Val.ToDecimal(txtPremiumAmt.Text) + Val.ToDecimal(txtOtherExpense.Text), 0);
                //                    txtNetAmount.Text = Expence_Amt.ToString();
                //                    break;
                //                }
                //            }
                //        }
                //        btnAdd.Text = "&Add";
                //    }
                //    else
                //    {
                //        for (int i = 0; i < m_dtbPurchaseDetails.Rows.Count; i++)
                //        {
                //            if (m_dtbPurchaseDetails.Select("purchase_detail_id ='" + m_purchase_detail_id + "' AND sr_no = '" + m_update_srno + "'").Length > 0)
                //            {
                //                if (m_dtbPurchaseDetails.Rows[dgvMFGPurchaseDetails.FocusedRowHandle]["purchase_detail_id"].ToString() == m_purchase_detail_id.ToString())
                //                {
                //                    m_dtbPurchaseDetails.Rows[dgvMFGPurchaseDetails.FocusedRowHandle]["carat"] = Val.ToDecimal(txtCarat.Text).ToString();
                //                    //m_dtbPurchaseDetails.Rows[dgvMFGPurchaseDetails.FocusedRowHandle]["pcs"] = Val.ToInt(txtPcs.Text);
                //                    m_dtbPurchaseDetails.Rows[dgvMFGPurchaseDetails.FocusedRowHandle]["rate"] = Val.ToDecimal(txtRate.Text);
                //                    m_dtbPurchaseDetails.Rows[dgvMFGPurchaseDetails.FocusedRowHandle]["flag"] = 1;
                //                    m_dtbPurchaseDetails.Rows[dgvMFGPurchaseDetails.FocusedRowHandle]["amount"] = Math.Round(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text), 2);

                //                    m_dtbPurchaseDetails.Rows[dgvMFGPurchaseDetails.FocusedRowHandle]["rough_sieve_id"] = Val.ToInt(lueRoughSieve.EditValue);
                //                    //m_dtbPurchaseDetails.Rows[dgvMFGPurchaseDetails.FocusedRowHandle]["rough_shade_id"] = Val.ToInt(lueRoughShade.EditValue);
                //                    m_dtbPurchaseDetails.Rows[dgvMFGPurchaseDetails.FocusedRowHandle]["sieve_name"] = Val.ToString(lueRoughSieve.Text);
                //                    //m_dtbPurchaseDetails.Rows[dgvMFGPurchaseDetails.FocusedRowHandle]["color_name"] = Val.ToString(lueRoughShade.Text);
                //                    m_dtbPurchaseDetails.Rows[dgvMFGPurchaseDetails.FocusedRowHandle]["rough_type_id"] = Val.ToInt(lueRoughType.EditValue);
                //                    m_dtbPurchaseDetails.Rows[dgvMFGPurchaseDetails.FocusedRowHandle]["rough_type"] = Val.ToString(lueRoughType.Text);
                //                    //m_dtbPurchaseDetails.Rows[m_update_srno - 1]["carat"] = Val.ToDecimal(txtCarat.Text).ToString();
                //                    //m_dtbPurchaseDetails.Rows[m_update_srno - 1]["pcs"] = Val.ToInt(txtPcs.Text);
                //                    //m_dtbPurchaseDetails.Rows[m_update_srno - 1]["rate"] = Val.ToDecimal(txtRate.Text);
                //                    //m_dtbPurchaseDetails.Rows[m_update_srno - 1]["flag"] = 1;
                //                    //m_dtbPurchaseDetails.Rows[m_update_srno - 1]["rough_sieve_id"] = Val.ToInt(lueRoughSieve.EditValue);
                //                    //m_dtbPurchaseDetails.Rows[m_update_srno - 1]["rough_shade_id"] = Val.ToInt(lueRoughShade.EditValue);
                //                    //m_dtbPurchaseDetails.Rows[m_update_srno - 1]["sieve_name"] = Val.ToString(lueRoughSieve.Text);
                //                    //m_dtbPurchaseDetails.Rows[m_update_srno - 1]["color_name"] = Val.ToString(lueRoughShade.Text);

                //                    //m_dtbPurchaseDetails.Rows[m_update_srno - 1]["amount"] = Math.Round(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text), 2);
                //                    decimal Expence_Amt = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) + Val.ToDecimal(txtBrokerageAmt.Text)) + Val.ToDecimal(txtPremiumAmt.Text) + Val.ToDecimal(txtOtherExpense.Text), 0);
                //                    txtNetAmount.Text = Expence_Amt.ToString();
                //                }
                //            }
                //        }
                //        btnAdd.Text = "&Add";
                //    }
                //    dgvMFGPurchaseDetails.MoveLast();
                //}
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
                    if (dgvMFGPurchaseDetails == null)
                    {
                        lstError.Add(new ListError(22, "Record"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                        }
                    }
                    var result = DateTime.Compare(Convert.ToDateTime(dtpPurchaseDate.Text), DateTime.Today);
                    if (result > 0)
                    {
                        lstError.Add(new ListError(5, " Invoice Date Not Be Greater Than Today Date"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            dtpPurchaseDate.Focus();
                        }
                    }
                }

                if (m_blnadd)
                {
                    if (lueRoughSieve.Text == "")
                    {
                        lstError.Add(new ListError(13, "Assort"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueRoughSieve.Focus();
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
                //txtPartyInvoiceNo.Text = string.Empty;
                txtTermDays.Text = string.Empty;
                txtExchangeRate.Text = string.Empty;

                txtSearchInvoice.Text = string.Empty;
                lueBillToParty.EditValue = System.DBNull.Value;
                dtpPurchaseDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpPurchaseDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpPurchaseDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpPurchaseDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpPurchaseDate.EditValue = DateTime.Now;

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

                lueRoughSieve.EditValue = System.DBNull.Value;
                lueRoughType.EditValue = System.DBNull.Value;
                
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
                btnBrowse.Enabled = true;
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
            MFG_PurchaseProperty objMFGPurchaseProperty = new MFG_PurchaseProperty();
            MFGPurchase objMFGPurchase = new MFGPurchase();
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

                    objMFGPurchaseProperty.purchase_id = Val.ToInt(lblMode.Tag);

                    int IntRes = objMFGPurchase.Delete(objMFGPurchaseProperty, m_dtbPurchaseDetails);

                    if (IntRes == -1)
                    {
                        Global.Confirm("Error In Purchase Invoice");
                        //txtPartyInvoiceNo.Focus();
                    }
                    else
                    {
                        if (Val.ToInt(lblMode.Tag) == 0)
                        {
                            Global.Confirm("Purchase Entry Data Delete Successfully");
                        }
                        else
                        {
                            Global.Confirm("Purchase Entry Data Delete Successfully");
                        }

                    }
                }
                else
                {
                    Global.Message("Invoice ID not found");
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
                objMFGPurchaseProperty = null;
            }

            return blnReturn;
        }
        private bool PopulateDetails()
        {
            objMFGPurchase = new MFGPurchase();
            bool blnReturn = true;
            DateTime datFromDate = DateTime.MinValue;
            DateTime datToDate = DateTime.MinValue;
            try
            {
                m_dtbDetails = objMFGPurchase.GetData(Val.DBDate(dtpFromDate.Text), Val.DBDate(dtpToDate.Text), Val.ToString(txtSearchInvoice.Text), Val.ToInt32(lueBillToParty.EditValue));

                if (m_dtbDetails.Rows.Count == 0)
                {
                    Global.Message("Data Not Found");
                    blnReturn = false;
                }
                grdMFGPurchase.DataSource = m_dtbDetails;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            finally
            {
                objMFGPurchase = null;
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
                m_dtbPurchaseDetails.Columns.Add("purchase_detail_id", typeof(int));
                m_dtbPurchaseDetails.Columns.Add("invoice_id", typeof(int));
                m_dtbPurchaseDetails.Columns.Add("lot_id", typeof(int));
                m_dtbPurchaseDetails.Columns.Add("rough_sieve_id", typeof(int));
                m_dtbPurchaseDetails.Columns.Add("sieve_name", typeof(string));
                m_dtbPurchaseDetails.Columns.Add("rough_shade_id", typeof(int));
                m_dtbPurchaseDetails.Columns.Add("color_name", typeof(string));
                m_dtbPurchaseDetails.Columns.Add("pcs", typeof(int)).DefaultValue = 0;
                m_dtbPurchaseDetails.Columns.Add("carat", typeof(decimal)).DefaultValue = 0;
                m_dtbPurchaseDetails.Columns.Add("rate", typeof(decimal)).DefaultValue = 0;
                m_dtbPurchaseDetails.Columns.Add("amount", typeof(decimal)).DefaultValue = 0;
                m_dtbPurchaseDetails.Columns.Add("discount", typeof(decimal)).DefaultValue = 0;
                m_dtbPurchaseDetails.Columns.Add("remarks", typeof(string));
                m_dtbPurchaseDetails.Columns.Add("old_pcs", typeof(int)).DefaultValue = 0;
                m_dtbPurchaseDetails.Columns.Add("old_carat", typeof(decimal));
                m_dtbPurchaseDetails.Columns.Add("flag", typeof(int)).DefaultValue = 0;
                //m_dtbPurchaseDetails.Columns.Add("old_assort_id", typeof(int));
                //m_dtbPurchaseDetails.Columns.Add("old_sieve_id", typeof(int));
                //m_dtbPurchaseDetails.Columns.Add("old_sub_sieve_id", typeof(int));
                //m_dtbPurchaseDetails.Columns.Add("old_assort_name", typeof(string));
                //m_dtbPurchaseDetails.Columns.Add("old_sieve_name", typeof(string));
                //m_dtbPurchaseDetails.Columns.Add("old_sub_sieve_name", typeof(string));
                m_dtbPurchaseDetails.Columns.Add("current_rate", typeof(decimal)).DefaultValue = 0;
                m_dtbPurchaseDetails.Columns.Add("current_amount", typeof(decimal)).DefaultValue = 0;

                m_dtbPurchaseDetails.Columns.Add("rough_type_id", typeof(int));
                m_dtbPurchaseDetails.Columns.Add("rough_type", typeof(string));

                grdMFGPurchaseDetails.DataSource = m_dtbPurchaseDetails;
                grdMFGPurchaseDetails.Refresh();
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
                            dgvMFGPurchase.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvMFGPurchase.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvMFGPurchase.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvMFGPurchase.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvMFGPurchase.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvMFGPurchase.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvMFGPurchase.ExportToCsv(Filepath);
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

        private void RepBtnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            dgvMFGPurchaseDetails.DeleteRow(dgvMFGPurchaseDetails.GetRowHandle(dgvMFGPurchaseDetails.FocusedRowHandle));
            m_dtbPurchaseDetails.AcceptChanges();
        }
    }
}