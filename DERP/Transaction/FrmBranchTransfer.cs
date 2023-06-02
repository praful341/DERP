using BLL;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Utility;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using DevExpress.XtraEditors;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace DREP.Transaction
{
    public partial class FrmBranchTransfer : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        Control _NextEnteredControl;
        private List<Control> _tabControls;
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        BranchTransfer objBranchTransfer;
        UserAuthentication objUserAuthentication;
        AssortMaster objAssort;
        SieveMaster objSieve;
        RateMaster objRate;

        DataTable DtControlSettings;
        DataTable m_dtbAssorts;
        DataTable m_dtbSieve;
        DataTable m_dtbBranchDetails;
        DataTable m_dtbCurrency;
        DataTable m_dtbDetails;
        DataTable m_dtbSievecheck;
        DataTable m_dtbAssortscheck;
        DataTable m_dtbSievedtl;
        DataTable m_dtbAssortsdtl;
        DataTable m_opDate;

        int m_numForm_id;
        int m_srno;
        int m_update_srno;
        int IntRes;
        int m_branch_detail_id;

        decimal m_numTotalCarats;
        decimal m_numTotalAmount;
        decimal m_current_rate;
        decimal m_current_amount;
        decimal m_numcarat;

        bool m_blnadd;
        bool m_blnsave;
        string NewBranchNo;

        #endregion

        #region Constructor
        public FrmBranchTransfer()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            _NextEnteredControl = new Control();
            _tabControls = new List<Control>();

            objBranchTransfer = new BranchTransfer();
            objUserAuthentication = new UserAuthentication();
            objAssort = new AssortMaster();
            objSieve = new SieveMaster();
            objRate = new RateMaster();

            DtControlSettings = new DataTable();
            m_dtbAssorts = new DataTable();
            m_dtbSieve = new DataTable();
            m_dtbBranchDetails = new DataTable();
            m_dtbCurrency = new DataTable();
            m_dtbDetails = new DataTable();
            m_dtbSievecheck = new DataTable();
            m_dtbAssortscheck = new DataTable();
            m_dtbSievedtl = new DataTable();
            m_dtbAssortsdtl = new DataTable();
            m_opDate = new DataTable();

            m_numForm_id = 0;
            m_srno = 0;
            m_update_srno = 0;
            IntRes = 0;
            m_branch_detail_id = 0;

            m_numTotalCarats = 0;
            m_numTotalAmount = 0;
            m_current_rate = 0;
            m_current_amount = 0;
            m_numcarat = 0;
            NewBranchNo = "";

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
            objBOFormEvents.ObjToDispose.Add(objBranchTransfer);
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
        private void FrmBranchTransfer_Load(object sender, EventArgs e)
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
                    ttlbBranchTransfer.SelectedTabPage = tblBranchList;
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
                    lueAssortName.EditValue = DBNull.Value;
                    lueSieveName.EditValue = DBNull.Value;
                    txtPcs.Text = string.Empty;
                    txtCarat.Text = string.Empty;
                    txtRate.Text = string.Empty;
                    txtAmount.Text = string.Empty;
                    lueAssortName.Focus();
                    lueAssortName.ShowPopup();
                    lueAssortName.ItemIndex = 0;
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
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
                    if (entry.Key is DevExpress.XtraEditors.LookUpEdit || entry.Key is DevExpress.XtraEditors.DateEdit || entry.Key is DevExpress.XtraEditors.TextEdit)
                    {
                        lstError.Add(new ListError(13, entry.Value));
                    }
                }
                rtnCtrls.First().Key.Focus();
                BLL.General.ShowErrors(lstError);
                Cursor.Current = Cursors.Arrow;
                return;
            }
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
            backgroundWorker_BTTransfer.RunWorkerAsync();

            btnSave.Enabled = true;
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
        private void btnBrowse_Click(object sender, EventArgs e)
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
            grdBranchDetails.DataSource = null;
            try
            {
                if (txtFileName.Text.Length != 0)
                {
                    using (var pck = new ExcelPackage(new FileInfo(txtFileName.Text)))
                    {
                        ExcelWorksheet ws = pck.Workbook.Worksheets[1];
                        m_dtbBranchDetails = WorksheetToDataTable(ws, true);
                    }
                }

                m_dtbSievecheck = new SieveMaster().GetData();
                m_dtbAssortscheck = new AssortMaster().GetData();

                m_dtbBranchDetails.Columns.Add("bt_detail_id", typeof(int));
                m_dtbBranchDetails.Columns.Add("bt_id", typeof(int));
                m_dtbBranchDetails.Columns.Add("assort_id", typeof(int));
                m_dtbBranchDetails.Columns.Add("sieve_id", typeof(int));
                m_dtbBranchDetails.Columns.Add("current_rate", typeof(decimal)).DefaultValue = 0;
                m_dtbBranchDetails.Columns.Add("current_amount", typeof(decimal)).DefaultValue = 0;
                m_dtbBranchDetails.Columns.Add("discount", typeof(decimal)).DefaultValue = 0;

                m_dtbBranchDetails.Columns.Add("old_pcs", typeof(int)).DefaultValue = 0;
                m_dtbBranchDetails.Columns.Add("old_carat", typeof(decimal));
                m_dtbBranchDetails.Columns.Add("flag", typeof(int)).DefaultValue = 0;
                m_dtbBranchDetails.Columns.Add("old_assort_id", typeof(int));
                m_dtbBranchDetails.Columns.Add("old_sieve_id", typeof(int));
                m_dtbBranchDetails.Columns.Add("old_sub_sieve_id", typeof(int));
                m_srno = 0;
                foreach (DataRow DRow in m_dtbBranchDetails.Rows)
                {
                    BranchTransfer objBranch = new BranchTransfer();

                    if (m_dtbBranchDetails.Select("sieve_name ='" + Val.ToString(DRow["sieve_name"]) + "' And assort_name = '" + Val.ToString(DRow["assort_name"]) + "'").Length > 1)
                    {
                        Global.Message("Duplicate Value found in Sieve : " + Val.ToString(DRow["sieve_name"]) + " AND Assort: " + Val.ToString(DRow["assort_name"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (DRow["assort_name"] != null)
                    {
                        if (Val.ToString(DRow["assort_name"]) != "")
                        {
                            if (m_dtbAssortscheck.Select("assort_name ='" + Val.ToString(DRow["assort_name"]) + "'").Length > 0)
                            {
                                m_dtbAssortsdtl = m_dtbAssortscheck.Select("assort_name ='" + Val.ToString(DRow["assort_name"]) + "'").CopyToDataTable();
                            }
                            else
                            {
                                Global.Message("Assort Not found in Master : " + Val.ToString(DRow["assort_name"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                    }
                    else
                    {
                        Global.Message("Assort Name are not found :" + Val.ToString(DRow["assort_name"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Cursor = Cursors.Default;
                        return;
                    }

                    if (DRow["sieve_name"] != null)
                    {
                        if (Val.ToString(DRow["sieve_name"]) != "")
                        {
                            if (m_dtbSievecheck.Select("sieve_name ='" + Val.ToString(DRow["sieve_name"]) + "'").Length > 0)
                            {
                                m_dtbSievedtl = m_dtbSievecheck.Select("sieve_name ='" + Val.ToString(DRow["sieve_name"]) + "'").CopyToDataTable();
                            }
                            else
                            {
                                Global.Message("Sieve Not found in Master : " + Val.ToString(DRow["sieve_name"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Cursor = Cursors.Default;
                                return;
                            }
                        }
                    }
                    else
                    {
                        Global.Message("Sieve Name are not found : " + Val.ToString(DRow["sieve_name"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Cursor = Cursors.Default;
                        return;
                    }

                    DRow["assort_id"] = Val.ToInt(m_dtbAssortsdtl.Rows[0]["assort_id"]);
                    DRow["sieve_id"] = Val.ToInt(m_dtbSievedtl.Rows[0]["sieve_id"]);
                    DRow["amount"] = Val.ToDecimal(DRow["rate"]) * Val.ToDecimal(DRow["carat"]);
                    string p_numStockRate = string.Empty;
                    p_numStockRate = objBranch.GetLetestPrice(Val.ToInt(m_dtbAssortsdtl.Rows[0]["assort_id"]), Val.ToInt(m_dtbSievedtl.Rows[0]["sieve_id"]));
                    m_current_rate = Val.ToDecimal(p_numStockRate);
                    DRow["current_rate"] = m_current_rate;
                    DRow["current_amount"] = Val.ToDecimal(m_current_rate) * Val.ToDecimal(DRow["carat"]);
                    m_srno = m_srno + 1;
                    DRow["sr_no"] = Val.ToInt(m_srno);
                }

                grdBranchDetails.DataSource = m_dtbBranchDetails;

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void txtCarat_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtAmount.Text = string.Format("{0:0.00}", Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text));
                m_current_amount = Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(m_current_rate);
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
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
                return;
            }
        }
        private void lueAssortName_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (lueAssortName.ItemIndex != -1 && lueSieveName.ItemIndex != -1)
                {
                    BranchTransfer objBranch = new BranchTransfer();
                    string p_numStockRate = string.Empty;
                    p_numStockRate = objBranch.GetLetestPrice(Val.ToInt(lueAssortName.EditValue), Val.ToInt(lueSieveName.EditValue));

                    txtRate.Text = Val.ToString(p_numStockRate);
                    m_current_rate = Val.ToDecimal(p_numStockRate);
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void lueSieveName_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (lueAssortName.ItemIndex != -1 && lueSieveName.ItemIndex != -1)
                {
                    BranchTransfer objBranch = new BranchTransfer();
                    string p_numStockRate = string.Empty;
                    p_numStockRate = objBranch.GetLetestPrice(Val.ToInt(lueAssortName.EditValue), Val.ToInt(lueSieveName.EditValue));

                    txtRate.Text = Val.ToString(p_numStockRate);
                    m_current_rate = Val.ToDecimal(p_numStockRate);
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
            e.Graphics.DrawLine(pen, 0, 59, 1500, 59);
        }
        private void lblFormatSample_Click(object sender, EventArgs e)
        {
            Global.CopyFormat(System.Windows.Forms.Application.StartupPath + @"\FORMAT\Branch_Transfer.xlsx", "Branch_Transfer.xlsx", "xlsx");
        }
        private void backgroundWorker_BTTransfer_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {

                if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                {
                    Conn = new BeginTranConnection(true, false);
                }
                else
                {
                    Conn = new BeginTranConnection(false, true);
                }
                Branch_TransferProperty objBranchProperty = new Branch_TransferProperty();
                BranchTransfer objBranch = new BranchTransfer();
                try
                {
                    IntRes = 0;
                    objBranchProperty.bt_id = Val.ToInt(lblMode.Tag);
                    objBranchProperty.from_company_id = Val.ToInt(GlobalDec.gEmployeeProperty.company_id);
                    objBranchProperty.from_branch_id = Val.ToInt(GlobalDec.gEmployeeProperty.branch_id);
                    objBranchProperty.from_location_id = Val.ToInt(GlobalDec.gEmployeeProperty.location_id);
                    objBranchProperty.from_department_id = Val.ToInt(GlobalDec.gEmployeeProperty.department_id);

                    objBranchProperty.to_company_id = Val.ToInt(lueToCompany.EditValue);
                    objBranchProperty.to_branch_id = Val.ToInt(lueToBranch.EditValue);
                    objBranchProperty.to_location_id = Val.ToInt(lueToLocation.EditValue);
                    objBranchProperty.to_department_id = Val.ToInt(lueToDepartment.EditValue);
                    objBranchProperty.bt_issue_date = Val.DBDate(dtpIssueDate.Text);
                    objBranchProperty.particulars = Val.ToString(txtParticuler.Text);
                    objBranchProperty.hsn = Val.ToInt(txthsn.Text);
                    objBranchProperty.delivery_type_id = Val.ToInt(lueDeliveryType.EditValue);
                    objBranchProperty.remarks = Val.ToString(txtRemark.Text);
                    objBranchProperty.netamount = Math.Round(Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue), 3);
                    objBranchProperty.total_carat = Math.Round(Val.ToDecimal(clmDetCarat.SummaryItem.SummaryValue), 3);
                    objBranchProperty.form_id = m_numForm_id;

                    objBranchProperty = objBranch.Save(objBranchProperty, DLL.GlobalDec.EnumTran.Start, Conn);

                    Int64 NewmBTid = Val.ToInt64(objBranchProperty.bt_id);

                    int IntCounter = 0;
                    int Count = 0;
                    int TotalCount = m_dtbBranchDetails.Rows.Count;

                    foreach (DataRow drw in m_dtbBranchDetails.Rows)
                    {

                        objBranchProperty.bt_detail_id = Val.ToInt(drw["bt_detail_id"]);
                        objBranchProperty.bt_id = Val.ToInt32(NewmBTid);
                        objBranchProperty.assort_id = Val.ToInt(drw["assort_id"]);
                        objBranchProperty.sieve_id = Val.ToInt(drw["sieve_id"]);
                        objBranchProperty.pcs = Val.ToInt(drw["pcs"]);
                        objBranchProperty.carat = Val.ToDecimal(drw["carat"]);
                        objBranchProperty.rate = Val.ToDecimal(drw["rate"]);
                        objBranchProperty.amount = Val.ToDecimal(drw["amount"]);
                        objBranchProperty.discount = Val.ToDecimal(drw["discount"]);
                        objBranchProperty.current_rate = Val.ToDecimal(drw["current_rate"]);
                        objBranchProperty.current_amount = Val.ToDecimal(drw["current_amount"]);

                        objBranchProperty.old_carat = Val.ToDecimal(drw["old_carat"]);
                        objBranchProperty.old_pcs = Val.ToInt(drw["old_pcs"]);
                        objBranchProperty.flag = Val.ToInt(drw["flag"]);
                        objBranchProperty.old_assort_id = Val.ToInt(drw["old_assort_id"]);
                        objBranchProperty.old_sieve_id = Val.ToInt(drw["old_sieve_id"]);

                        IntRes = objBranch.Save_Detail(objBranchProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                        Count++;
                        IntCounter++;
                        IntRes++;
                        SetControlPropertyValue(lblProgressCount, "Text", Count.ToString() + "" + "/" + "" + TotalCount.ToString() + " Completed....");
                    }
                    if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                    {
                        Conn.Inter1.Commit();
                    }
                    else
                    {
                        Conn.Inter2.Commit();
                    }
                }
                catch (Exception ex)
                {
                    IntRes = -1;
                    if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                    {
                        Conn.Inter1.Rollback();
                    }
                    else
                    {
                        Conn.Inter2.Rollback();
                    }
                    Conn = null;
                    General.ShowErrors(ex.ToString());
                    return;
                }
                finally
                {
                    objBranchProperty = null;
                }
            }
            catch (Exception ex)
            {
                IntRes = -1;
                if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                {
                    Conn.Inter1.Rollback();
                }
                else
                {
                    Conn.Inter2.Rollback();
                }
                Conn = null;
                General.ShowErrors(ex.ToString());
                if (ex.InnerException != null)
                {
                    Global.Message(ex.InnerException.ToString());
                }
            }
        }
        private void backgroundWorker_BTTransfer_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                panelProgress.Visible = false;
                if (IntRes > 0)
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Branch Transfer Data Save Successfully");
                        ClearDetails();
                    }
                    else
                    {
                        Global.Confirm("Branch Transfer Data Update Successfully");
                        ClearDetails();
                    }
                }
                else
                {
                    Global.Confirm("Error In Branch Transfer");
                    lueToCompany.Focus();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }

        #region "Grid Events" 
        private void dgvBTMaster_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        BranchTransfer objBranchTransfer = new BranchTransfer();

                        DataRow Drow = dgvBranchTransfer.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt32(Drow["bt_id"]);

                        lueToCompany.EditValue = Val.ToInt(Drow["to_company_id"]);
                        lueToBranch.EditValue = Val.ToInt(Drow["to_branch_id"]);
                        lueToLocation.EditValue = Val.ToInt(Drow["to_location_id"]);
                        lueToDepartment.EditValue = Val.ToInt(Drow["to_department_id"]);
                        dtpIssueDate.Text = Val.DBDate(Val.ToString(Drow["bt_issue_date"]));
                        txtParticuler.Text = Val.ToString(Drow["particulars"]);
                        txthsn.Text = Val.ToString(Drow["hsn"]);
                        lueDeliveryType.EditValue = Val.ToInt(Drow["delivery_type_id"]);

                        m_dtbBranchDetails = objBranchTransfer.GetDataDetails(Val.ToInt(lblMode.Tag));

                        grdBranchDetails.DataSource = m_dtbBranchDetails;

                        ttlbBranchTransfer.SelectedTabPage = tblBranchdetail;
                        lueToCompany.Focus();
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
        private void dgvBranchDetails_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                m_numTotalCarats = Math.Round(Val.ToDecimal(clmCarats.SummaryItem.SummaryValue), 3, MidpointRounding.AwayFromZero);

                if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == "rate")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                        m_numTotalAmount = 0;
                    else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                        m_numTotalAmount += (Val.ToDecimal(e.GetValue("carat")) * Val.ToDecimal(e.GetValue("rate")));
                    else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    {
                        if (m_numTotalAmount > 0 && m_numTotalCarats > 0)
                            e.TotalValue = Math.Round((m_numTotalAmount / m_numTotalCarats), 3, MidpointRounding.AwayFromZero);
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
        private void dgvBranchDetails_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvBranchDetails.GetDataRow(e.RowHandle);
                        btnAdd.Text = "&Update";
                        lueSieveName.Text = Val.ToString(Drow["sieve_name"]);
                        lueSieveName.Tag = Val.ToInt64(Drow["sieve_id"]);
                        lueAssortName.Text = Val.ToString(Drow["assort_name"]);
                        lueAssortName.Tag = Val.ToInt64(Drow["assort_id"]);
                        txtPcs.Text = Val.ToString(Drow["pcs"]);
                        txtCarat.Text = Val.ToString(Drow["carat"]);
                        txtRate.Text = Val.ToString(Drow["rate"]);
                        txtAmount.Text = Val.ToString(Drow["amount"]);
                        m_numcarat = Val.ToDecimal(Drow["carat"]);
                        m_branch_detail_id = Val.ToInt(Drow["bt_detail_id"]);
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
        #endregion

        #endregion

        #region Functions
        private bool LoadDefaults()
        {
            bool blnReturn = true;
            try
            {
                Global.LOOKUPCompany(lueToCompany);
                Global.LOOKUPBranch(lueToBranch);
                Global.LOOKUPLocation(lueToLocation);
                Global.LOOKUPDepartment(lueToDepartment);
                Global.LOOKUPDeliveryType(lueDeliveryType);

                m_dtbAssorts = objAssort.GetData(1);
                lueAssortName.Properties.DataSource = m_dtbAssorts;
                lueAssortName.Properties.ValueMember = "assort_id";
                lueAssortName.Properties.DisplayMember = "assort_name";

                m_dtbSieve = objSieve.GetData(1);
                lueSieveName.Properties.DataSource = m_dtbSieve;
                lueSieveName.Properties.ValueMember = "sieve_id";
                lueSieveName.Properties.DisplayMember = "sieve_name";

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

                dtpIssueDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpIssueDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpIssueDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpIssueDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpIssueDate.EditValue = DateTime.Now;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            finally
            {
                objAssort = null;
                objSieve = null;
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
                    DataRow[] dr = m_dtbBranchDetails.Select("sieve_id = " + Val.ToInt(lueSieveName.EditValue) + " AND assort_id = " + Val.ToInt(lueAssortName.EditValue));

                    if (dr.Count() == 1)
                    {
                        Global.Message("Record already exists in grid", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lueAssortName.Focus();
                        blnReturn = false;
                        return blnReturn;
                    }

                    DataRow drwNew = m_dtbBranchDetails.NewRow();
                    decimal numCarat = Val.ToDecimal(txtCarat.Text);
                    decimal numRate = Val.ToDecimal(txtRate.Text);
                    decimal numAmount = Val.ToDecimal(txtAmount.Text);
                    int numPcs = Val.ToInt(txtPcs.Text);

                    drwNew["bt_id"] = Val.ToInt(0);
                    drwNew["bt_detail_id"] = Val.ToInt(0);

                    drwNew["assort_id"] = Val.ToInt(lueAssortName.EditValue);
                    drwNew["assort_name"] = Val.ToString(lueAssortName.Text);

                    drwNew["sieve_id"] = Val.ToInt(lueSieveName.EditValue);
                    drwNew["sieve_name"] = Val.ToString(lueSieveName.Text);

                    drwNew["pcs"] = numPcs;
                    drwNew["carat"] = numCarat;
                    drwNew["rate"] = Val.ToDecimal(txtRate.Text);
                    drwNew["amount"] = Math.Round(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text), 3);
                    drwNew["old_carat"] = Val.ToDecimal(0);
                    drwNew["old_pcs"] = Val.ToDecimal(0);
                    drwNew["flag"] = Val.ToInt(0);
                    drwNew["current_rate"] = m_current_rate;
                    drwNew["current_amount"] = Val.Val(m_current_rate * numCarat);

                    m_srno = m_srno + 1;
                    drwNew["sr_no"] = Val.ToInt(m_srno);

                    m_dtbBranchDetails.Rows.Add(drwNew);
                    dgvBranchDetails.MoveLast();
                }
                else if (btnAdd.Text == "&Update")
                {
                    if (m_dtbBranchDetails.Select("assort_id ='" + Val.ToInt(lueAssortName.EditValue) + "' AND sieve_id ='" + Val.ToInt(lueSieveName.EditValue) + "'").Length > 0)
                    {
                        for (int i = 0; i < m_dtbBranchDetails.Rows.Count; i++)
                        {
                            if (m_dtbBranchDetails.Select("bt_detail_id ='" + m_branch_detail_id + "' AND sr_no = '" + m_update_srno + "'").Length > 0)
                            {
                                if (m_dtbBranchDetails.Rows[m_update_srno - 1]["bt_detail_id"].ToString() == m_branch_detail_id.ToString())
                                {
                                    m_dtbBranchDetails.Rows[m_update_srno - 1]["carat"] = Val.ToDecimal(txtCarat.Text).ToString();
                                    m_dtbBranchDetails.Rows[m_update_srno - 1]["pcs"] = Val.ToInt(txtPcs.Text);
                                    m_dtbBranchDetails.Rows[m_update_srno - 1]["rate"] = Val.ToDecimal(txtRate.Text);
                                    m_dtbBranchDetails.Rows[m_update_srno - 1]["flag"] = 1;
                                    m_dtbBranchDetails.Rows[m_update_srno - 1]["amount"] = Math.Round(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text), 3);
                                    m_dtbBranchDetails.Rows[m_update_srno - 1]["current_rate"] = m_current_rate;
                                    m_dtbBranchDetails.Rows[m_update_srno - 1]["current_amount"] = m_current_amount;
                                    break;
                                }
                            }
                        }
                        btnAdd.Text = "&Add";
                    }
                    else
                    {
                        for (int i = 0; i < m_dtbBranchDetails.Rows.Count; i++)
                        {
                            if (m_dtbBranchDetails.Select("bt_detail_id ='" + m_branch_detail_id + "' AND sr_no = '" + m_update_srno + "'").Length > 0)
                            {
                                if (m_dtbBranchDetails.Rows[m_update_srno - 1]["bt_detail_id"].ToString() == m_branch_detail_id.ToString())
                                {
                                    m_dtbBranchDetails.Rows[m_update_srno - 1]["carat"] = Val.ToDecimal(txtCarat.Text).ToString();
                                    m_dtbBranchDetails.Rows[m_update_srno - 1]["pcs"] = Val.ToInt(txtPcs.Text);
                                    m_dtbBranchDetails.Rows[m_update_srno - 1]["rate"] = Val.ToDecimal(txtRate.Text);
                                    m_dtbBranchDetails.Rows[m_update_srno - 1]["flag"] = 1;
                                    m_dtbBranchDetails.Rows[m_update_srno - 1]["assort_id"] = Val.ToInt(lueAssortName.EditValue);
                                    m_dtbBranchDetails.Rows[m_update_srno - 1]["sieve_id"] = Val.ToInt(lueSieveName.EditValue);
                                    m_dtbBranchDetails.Rows[m_update_srno - 1]["assort_name"] = Val.ToString(lueAssortName.Text);
                                    m_dtbBranchDetails.Rows[m_update_srno - 1]["sieve_name"] = Val.ToString(lueSieveName.Text);
                                    m_dtbBranchDetails.Rows[m_update_srno - 1]["amount"] = Math.Round(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text), 3);
                                    m_dtbBranchDetails.Rows[m_update_srno - 1]["current_rate"] = m_current_rate;
                                    m_dtbBranchDetails.Rows[m_update_srno - 1]["current_amount"] = m_current_amount;
                                }
                            }
                        }
                        btnAdd.Text = "&Add";
                    }
                    dgvBranchDetails.MoveLast();
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
                    if (m_dtbBranchDetails.Rows.Count == 0)
                    {
                        lstError.Add(new ListError(22, "Record"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                        }
                    }
                    if (lueToCompany.EditValue.Equals(GlobalDec.gEmployeeProperty.company_id) && lueToBranch.EditValue.Equals(GlobalDec.gEmployeeProperty.branch_id) && lueToLocation.EditValue.Equals(GlobalDec.gEmployeeProperty.location_id) && lueToDepartment.EditValue.Equals(GlobalDec.gEmployeeProperty.department_id))
                    {
                        lstError.Add(new ListError(31, "Same"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                        }
                    }
                    var result = DateTime.Compare(Convert.ToDateTime(dtpIssueDate.Text), DateTime.Today);
                    if (result > 0)
                    {
                        lstError.Add(new ListError(5, "Issue Date Not Be Greater Than Today Date"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                        }
                    }
                }

                if (m_blnadd)
                {
                    if (lueAssortName.Text == "")
                    {
                        lstError.Add(new ListError(13, "Assort"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueAssortName.Focus();
                        }
                    }
                    if (lueSieveName.Text == "")
                    {
                        lstError.Add(new ListError(13, "Sieve"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueSieveName.Focus();
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
                if (!GenerateBranchTransferDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }

                lblMode.Tag = null;
                lblMode.Text = "Add Mode";
                lueToCompany.EditValue = System.DBNull.Value;
                lueToBranch.EditValue = System.DBNull.Value;
                lueToLocation.EditValue = System.DBNull.Value;
                lueToDepartment.EditValue = System.DBNull.Value;
                lueDeliveryType.EditValue = System.DBNull.Value;
                lueDeliveryType.EditValue = Val.ToInt32(GlobalDec.gEmployeeProperty.delivery_type_id);
                txtParticuler.Text = string.Empty;
                txthsn.Text = string.Empty;
                txtRemark.Text = "";
                lueToCompany.Focus();

                dtpIssueDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpIssueDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpIssueDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpIssueDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpIssueDate.EditValue = DateTime.Now;

                m_opDate = Global.GetDate();
                dtpFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpFromDate.EditValue = Val.DBDate(m_opDate.Rows[0]["opening_date"].ToString());

                lueAssortName.EditValue = System.DBNull.Value;
                lueSieveName.EditValue = System.DBNull.Value;
                txtPcs.Text = string.Empty;
                txtCarat.Text = string.Empty;
                txtRate.Text = string.Empty;
                txtAmount.Text = string.Empty;
                btnAdd.Text = "&Add";
                m_srno = 0;
                btnBrowse.Enabled = true;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
        }
        private bool GenerateBranchTransferDetails()
        {
            bool blnReturn = true;
            try
            {
                if (m_dtbBranchDetails.Rows.Count > 0)
                    m_dtbBranchDetails.Rows.Clear();

                m_dtbBranchDetails = new DataTable();
                m_dtbBranchDetails.Columns.Add("sr_no", typeof(int));
                m_dtbBranchDetails.Columns.Add("bt_detail_id", typeof(int));
                m_dtbBranchDetails.Columns.Add("bt_id", typeof(int));
                m_dtbBranchDetails.Columns.Add("assort_id", typeof(int));
                m_dtbBranchDetails.Columns.Add("assort_name", typeof(string));
                m_dtbBranchDetails.Columns.Add("sieve_id", typeof(int));
                m_dtbBranchDetails.Columns.Add("sieve_name", typeof(string));
                m_dtbBranchDetails.Columns.Add("pcs", typeof(int)).DefaultValue = 0;
                m_dtbBranchDetails.Columns.Add("carat", typeof(decimal)).DefaultValue = 0;
                m_dtbBranchDetails.Columns.Add("rate", typeof(decimal)).DefaultValue = 0;
                m_dtbBranchDetails.Columns.Add("amount", typeof(decimal)).DefaultValue = 0;
                m_dtbBranchDetails.Columns.Add("discount", typeof(decimal)).DefaultValue = 0;
                m_dtbBranchDetails.Columns.Add("remarks", typeof(string));
                m_dtbBranchDetails.Columns.Add("old_pcs", typeof(int)).DefaultValue = 0;
                m_dtbBranchDetails.Columns.Add("old_carat", typeof(decimal));
                m_dtbBranchDetails.Columns.Add("flag", typeof(int)).DefaultValue = 0;
                m_dtbBranchDetails.Columns.Add("old_assort_id", typeof(int));
                m_dtbBranchDetails.Columns.Add("old_sieve_id", typeof(int));
                m_dtbBranchDetails.Columns.Add("old_sub_sieve_id", typeof(int));
                m_dtbBranchDetails.Columns.Add("old_assort_name", typeof(string));
                m_dtbBranchDetails.Columns.Add("old_sieve_name", typeof(string));
                m_dtbBranchDetails.Columns.Add("old_sub_sieve_name", typeof(string));
                m_dtbBranchDetails.Columns.Add("current_rate", typeof(decimal)).DefaultValue = 0;
                m_dtbBranchDetails.Columns.Add("current_amount", typeof(decimal)).DefaultValue = 0;

                grdBranchDetails.DataSource = m_dtbBranchDetails;
                grdBranchDetails.Refresh();
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
            BranchTransfer objBranchTransfer = new BranchTransfer();
            DateTime datFromDate = DateTime.MinValue;
            DateTime datToDate = DateTime.MinValue;
            try
            {
                m_dtbDetails = objBranchTransfer.GetData(Val.DBDate(dtpFromDate.Text).ToString(), Val.DBDate(dtpToDate.Text).ToString());

                if (m_dtbDetails.Rows.Count == 0)
                {
                    Global.Message("Data Not Found");
                    blnReturn = false;
                }

                grdBranchTransfer.DataSource = m_dtbDetails;
                dgvBranchTransfer.BestFitColumns();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            finally
            {
                objBranchTransfer = null;
            }
            return blnReturn;
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lblMode.Text == "Edit Mode")
            {
                DialogResult result = MessageBox.Show("Do you want to Delete data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result != DialogResult.Yes)
                {
                    btnDelete.Enabled = true;
                    return;
                }
                Branch_TransferProperty objBranchProperty = new Branch_TransferProperty();
                BranchTransfer objBranch = new BranchTransfer();

                objBranchProperty.bt_id = Val.ToInt(lblMode.Tag);

                objBranchProperty = objBranch.Branch_Transfer_Delete(objBranchProperty);

                NewBranchNo = Val.ToString(objBranchProperty.bt_id);

                if (NewBranchNo == "0")
                {
                    Global.Message("Already Confirm in This Branch Transfer...So Don't have Delete in this Branch Transfer Data.");
                    return;
                }
                else
                {
                    Global.Message("Branch Transfer Deleted Successfully");
                    ClearDetails();
                }
            }
            else
            {
                Global.Message("Not Selected Any Data in Branch Transfer");
                return;
            }
        }
    }
}