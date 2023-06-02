using BLL;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Transaction;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using DevExpress.XtraEditors;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;


namespace DREP.Transaction
{
    public partial class FrmCompanyMemo_Issue : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member
        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        Control _NextEnteredControl;
        private List<Control> _tabControls;
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        CompanyMemoIssueReceipt objCompanyMamoIssueReceipt;
        AssortMaster objAssort;
        SieveMaster objSieve;

        DataTable m_dtbAssort;
        DataTable m_dtbSieve;
        DataTable m_dtbStockCarat;
        DataTable m_dtbMemoIssueDetail;

        DataTable DtControlSettings;
        DataTable m_dtbCompanyMemoIssueDetail;

        int m_numForm_id;
        int IntRes;
        int m_srno = 0;

        decimal m_current_rate;
        decimal m_current_amount;
        decimal m_numSummRate;

        bool m_blnadd;
        bool m_blnsave;
        #endregion

        #region Constructor
        public FrmCompanyMemo_Issue()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            _NextEnteredControl = new Control();
            _tabControls = new List<Control>();

            objCompanyMamoIssueReceipt = new CompanyMemoIssueReceipt();
            objAssort = new AssortMaster();
            objSieve = new SieveMaster();
            m_dtbStockCarat = new DataTable();

            m_dtbAssort = new DataTable();
            m_dtbSieve = new DataTable();
            DtControlSettings = new DataTable();
            m_dtbCompanyMemoIssueDetail = new DataTable();
            m_dtbMemoIssueDetail = new DataTable();
            m_numForm_id = 0;
            IntRes = 0;

            m_current_rate = 0;
            m_current_amount = 0;

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
            objBOFormEvents.ObjToDispose.Add(objCompanyMamoIssueReceipt);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);

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
        private void btnClear_Click(object sender, EventArgs e)
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
            //OpenFileDialog OpenDialog = new OpenFileDialog();
            //if (OpenDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            //{
            //    return;
            //}
            //txtFileName.Text = OpenDialog.FileName;
            //OpenDialog.Dispose();
            //OpenDialog = null;

            //if (File.Exists(txtFileName.Text) == false)
            //{
            //    Global.Message("File Is Not Exists To The Path");
            //    return;
            //}

            //this.Cursor = Cursors.WaitCursor;
            //grdBranchDetails.DataSource = null;

            ////System.Data.DataTable DTabFile = new System.Data.DataTable();

            //try
            //{


            //    if (txtFileName.Text.Length != 0)
            //    {
            //        using (var pck = new ExcelPackage(new FileInfo(txtFileName.Text)))
            //        {
            //            ExcelWorksheet ws = pck.Workbook.Worksheets[1];
            //            m_dtbBranchDetails = WorksheetToDataTable(ws, true);
            //        }
            //    }

            //    m_dtbSievecheck = new SieveMaster().GetData();
            //    m_dtbAssortscheck = new AssortMaster().GetData();

            //    m_dtbBranchDetails.Columns.Add("bt_detail_id", typeof(int));
            //    m_dtbBranchDetails.Columns.Add("bt_id", typeof(int));
            //    m_dtbBranchDetails.Columns.Add("assort_id", typeof(int));
            //    m_dtbBranchDetails.Columns.Add("sieve_id", typeof(int));
            //    m_dtbBranchDetails.Columns.Add("current_rate", typeof(decimal)).DefaultValue = 0;
            //    m_dtbBranchDetails.Columns.Add("current_amount", typeof(decimal)).DefaultValue = 0;
            //    m_dtbBranchDetails.Columns.Add("discount", typeof(decimal)).DefaultValue = 0;


            //    foreach (DataRow DRow in m_dtbBranchDetails.Rows)
            //    {
            //        BranchTransfer objBranch = new BranchTransfer();

            //        if (m_dtbBranchDetails.Select("sieve_name ='" + Val.ToString(DRow["sieve_name"]) + "' And assort_name = '" + Val.ToString(DRow["assort_name"]) + "'").Length > 1)
            //        {
            //            Global.Message("Duplicate Value found in Sieve : " + Val.ToString(DRow["sieve_name"]) + " AND Assort: " + Val.ToString(DRow["assort_name"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            return;
            //        }

            //        if (DRow["assort_name"] != null)
            //        {
            //            if (Val.ToString(DRow["assort_name"]) != "")
            //            {
            //                if (m_dtbAssortscheck.Select("assort_name ='" + Val.ToString(DRow["assort_name"]) + "'").Length > 0)
            //                {
            //                    m_dtbAssortsdtl = m_dtbAssortscheck.Select("assort_name ='" + Val.ToString(DRow["assort_name"]) + "'").CopyToDataTable();
            //                }
            //                else
            //                {
            //                    Global.Message("Assort Not found in Master : " + Val.ToString(DRow["assort_name"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                    return;
            //                }
            //            }
            //        }
            //        else
            //        {
            //            Global.Message("Assort Name are not found :" + Val.ToString(DRow["assort_name"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            this.Cursor = Cursors.Default;
            //            return;
            //        }

            //        if (DRow["sieve_name"] != null)
            //        {
            //            if (Val.ToString(DRow["sieve_name"]) != "")
            //            {
            //                if (m_dtbSievecheck.Select("sieve_name ='" + Val.ToString(DRow["sieve_name"]) + "'").Length > 0)
            //                {
            //                    m_dtbSievedtl = m_dtbSievecheck.Select("sieve_name ='" + Val.ToString(DRow["sieve_name"]) + "'").CopyToDataTable();
            //                }
            //                else
            //                {
            //                    Global.Message("Sieve Not found in Master : " + Val.ToString(DRow["sieve_name"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                    this.Cursor = Cursors.Default;
            //                    return;
            //                }
            //            }
            //        }
            //        else
            //        {
            //            Global.Message("Sieve Name are not found : " + Val.ToString(DRow["sieve_name"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            this.Cursor = Cursors.Default;
            //            return;
            //        }

            //        decimal numStockCarat = 0;


            //        DataTable p_dtbStockCarat = new DataTable();
            //        p_dtbStockCarat = objBranchTransfer.GetStockCarat(GlobalDec.gEmployeeProperty.company_id, GlobalDec.gEmployeeProperty.branch_id, GlobalDec.gEmployeeProperty.location_id, GlobalDec.gEmployeeProperty.department_id, Val.ToInt(lueAssortName.EditValue), Val.ToInt(lueSieveName.EditValue));

            //        if (p_dtbStockCarat.Rows.Count > 0)
            //        {
            //            numStockCarat = Val.ToDecimal(p_dtbStockCarat.Rows[0]["stock_carat"]);
            //        }

            //        if (numStockCarat < Val.ToDecimal(DRow["carat"]))
            //        {
            //            Global.Message("Please check enter carat more then stock carat  (Assorts : " + Val.ToString(DRow["assort_name"]) + ") (Sieve : " + Val.ToString(DRow["sieve_name"]) + " ) ", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            txtCarat.Focus();
            //            this.Cursor = Cursors.Default;
            //            return;
            //        }

            //        DRow["assort_id"] = Val.ToInt(m_dtbAssortsdtl.Rows[0]["assort_id"]);
            //        DRow["sieve_id"] = Val.ToInt(m_dtbSievedtl.Rows[0]["sieve_id"]);

            //        string p_numStockRate = string.Empty;
            //        p_numStockRate = objBranch.GetLetestPrice(Val.ToInt(m_dtbAssortsdtl.Rows[0]["assort_id"]), Val.ToInt(m_dtbSievedtl.Rows[0]["sieve_id"]));
            //        m_current_rate = Val.ToDecimal(p_numStockRate);
            //        DRow["current_rate"] = m_current_rate;
            //        DRow["current_amount"] = Val.ToDecimal(m_current_rate) * Val.ToDecimal(DRow["carat"]);
            //    }

            //    grdBranchDetails.DataSource = m_dtbBranchDetails;

            //    this.Cursor = Cursors.Default;
            //}
            //catch (Exception ex)
            //{
            //    General.ShowErrors(ex.ToString());
            //    return;
            //}
        }
        private void txtCarat_EditValueChanged(object sender, EventArgs e)
        {
            txtAmount.Text = string.Format("{0:0.00}", Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text));
            m_current_amount = Val.ToDecimal(txtAmount.Text);
        }
        private void txtRate_EditValueChanged(object sender, EventArgs e)
        {
            txtAmount.Text = string.Format("{0:0.00}", Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text));
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
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void lueDeliveryType_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    lueAssortName.Focus();
            //    lueAssortName.ShowPopup();
            //}
        }
        private void lueSieveName_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    txtPcs.Focus();
            //}
        }
        private void lueAssortName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lueSieveName.Focus();
                lueSieveName.ShowPopup();
            }
        }
        private void lueAssortName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lueSieveName.Focus();
                lueSieveName.ShowPopup();
            }
        }
        private void lueDeliveryType_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lueAssortName.Focus();
                lueAssortName.ShowPopup();
            }
        }
        private void RBtnType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RBtnType.SelectedIndex == 0)
            {
                lueToCompany.EditValue = null;
                lueToBranch.EditValue = null;
                lueToLocation.EditValue = null;
                lueToDepartment.EditValue = null;
                dtpIssueDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpIssueDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpIssueDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpIssueDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpIssueDate.EditValue = DateTime.Now;
                txtPartyMemoNo.Text = string.Empty;
                txtCompanyMamoNo.Text = string.Empty;
                lueToCompany.Enabled = true;
                lueToBranch.Enabled = true;
                lueToLocation.Enabled = true;
                lueToDepartment.Enabled = true;
                dtpIssueDate.Enabled = true;
                txtPartyMemoNo.Enabled = true;
                lueToCompany.Focus();
            }
            else if (RBtnType.SelectedIndex == 1)
            {
                lueToCompany.EditValue = null;
                lueToBranch.EditValue = null;
                lueToLocation.EditValue = null;
                lueToDepartment.EditValue = null;
                dtpIssueDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpIssueDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpIssueDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpIssueDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpIssueDate.EditValue = DateTime.Now;
                txtPartyMemoNo.Text = string.Empty;
                txtCompanyMamoNo.Text = string.Empty;
                lueToCompany.Enabled = false;
                lueToBranch.Enabled = false;
                lueToLocation.Enabled = false;
                lueToDepartment.Enabled = false;
                dtpIssueDate.Enabled = false;
                txtPartyMemoNo.Enabled = false;
                txtCompanyMamoNo.Focus();
            }
        }
        private void txtPartyMemoNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                GetData();
            }
        }
        private void txtCompanyMamoNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GetData_Receive();
            }
        }
        private void panelControl4_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(255, 191, 219, 255), 2);
            e.Graphics.DrawLine(pen, 0, 57, 1500, 57);
        }
        private void backgroundWorker_BTTransfer_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Company_MemoIssueReceiptProperty objCompany_MemoIssueReceiptProperty = new Company_MemoIssueReceiptProperty();
            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                Conn = new BeginTranConnection(true, false);
            }
            else
            {
                Conn = new BeginTranConnection(false, true);
            }
            try
            {
                if (RBtnType.SelectedIndex == 0)
                {
                    try
                    {
                        IntRes = 0;
                        int IntCounter = 0;
                        int Count = 0;
                        int TotalCount = m_dtbCompanyMemoIssueDetail.Rows.Count;

                        foreach (DataRow drw in m_dtbCompanyMemoIssueDetail.Rows)
                        {
                            objCompany_MemoIssueReceiptProperty = new Company_MemoIssueReceiptProperty();

                            objCompany_MemoIssueReceiptProperty.from_company_id = Val.ToInt(GlobalDec.gEmployeeProperty.company_id);
                            objCompany_MemoIssueReceiptProperty.from_branch_id = Val.ToInt(GlobalDec.gEmployeeProperty.branch_id);
                            objCompany_MemoIssueReceiptProperty.from_location_id = Val.ToInt(GlobalDec.gEmployeeProperty.location_id);
                            objCompany_MemoIssueReceiptProperty.from_department_id = Val.ToInt(GlobalDec.gEmployeeProperty.department_id);

                            objCompany_MemoIssueReceiptProperty.to_company_id = Val.ToInt(lueToCompany.EditValue);
                            objCompany_MemoIssueReceiptProperty.to_branch_id = Val.ToInt(lueToBranch.EditValue);
                            objCompany_MemoIssueReceiptProperty.to_location_id = Val.ToInt(lueToLocation.EditValue);
                            objCompany_MemoIssueReceiptProperty.to_department_id = Val.ToInt(lueToDepartment.EditValue);
                            objCompany_MemoIssueReceiptProperty.company_memo_date = Val.DBDate(dtpIssueDate.Text);
                            objCompany_MemoIssueReceiptProperty.Party_Memo_No = Val.ToString(txtPartyMemoNo.Text);
                            objCompany_MemoIssueReceiptProperty.Company_Memo_No = Val.ToString(txtCompanyMamoNo.Text);
                            objCompany_MemoIssueReceiptProperty.form_id = m_numForm_id;

                            objCompany_MemoIssueReceiptProperty.remarks = Val.ToString(txtRemark.Text);

                            if (RBtnType.SelectedIndex == 0)
                            {
                                objCompany_MemoIssueReceiptProperty.issue_type_id = Val.ToInt(1);
                            }
                            else
                            {
                                objCompany_MemoIssueReceiptProperty.issue_type_id = Val.ToInt(2);
                            }


                            objCompany_MemoIssueReceiptProperty.assort_id = Val.ToInt(drw["assort_id"]);
                            objCompany_MemoIssueReceiptProperty.sieve_id = Val.ToInt(drw["sieve_id"]);
                            objCompany_MemoIssueReceiptProperty.sub_sieve_id = Val.ToInt(drw["sub_sieve_id"]);
                            objCompany_MemoIssueReceiptProperty.rej_pcs = Val.ToInt(drw["pcs"]);
                            objCompany_MemoIssueReceiptProperty.rej_carat = Val.ToDecimal(drw["carat"]);
                            objCompany_MemoIssueReceiptProperty.rate = Val.ToDecimal(drw["rate"]);
                            objCompany_MemoIssueReceiptProperty.amount = Val.ToDecimal(drw["amount"]);

                            IntRes = objCompanyMamoIssueReceipt.Save(objCompany_MemoIssueReceiptProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

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
                        objCompany_MemoIssueReceiptProperty = null;
                    }
                }
                if (RBtnType.SelectedIndex == 1)
                {
                    try
                    {
                        IntRes = 0;

                        int IntCounter = 0;
                        int Count = 0;
                        int TotalCount = m_dtbCompanyMemoIssueDetail.Rows.Count;

                        foreach (DataRow drw in m_dtbCompanyMemoIssueDetail.Rows)
                        {
                            objCompany_MemoIssueReceiptProperty = new Company_MemoIssueReceiptProperty();

                            objCompany_MemoIssueReceiptProperty.from_company_id = Val.ToInt(lueToCompany.EditValue);
                            objCompany_MemoIssueReceiptProperty.from_branch_id = Val.ToInt(lueToBranch.EditValue);
                            objCompany_MemoIssueReceiptProperty.from_location_id = Val.ToInt(lueToLocation.EditValue);
                            objCompany_MemoIssueReceiptProperty.from_department_id = Val.ToInt(lueToDepartment.EditValue);

                            objCompany_MemoIssueReceiptProperty.to_company_id = Val.ToInt(GlobalDec.gEmployeeProperty.company_id);
                            objCompany_MemoIssueReceiptProperty.to_branch_id = Val.ToInt(GlobalDec.gEmployeeProperty.branch_id);
                            objCompany_MemoIssueReceiptProperty.to_location_id = Val.ToInt(GlobalDec.gEmployeeProperty.location_id);
                            objCompany_MemoIssueReceiptProperty.to_department_id = Val.ToInt(GlobalDec.gEmployeeProperty.department_id);


                            objCompany_MemoIssueReceiptProperty.company_memo_date = Val.DBDate(dtpIssueDate.Text);
                            objCompany_MemoIssueReceiptProperty.Party_Memo_No = Val.ToString(txtPartyMemoNo.Text);
                            objCompany_MemoIssueReceiptProperty.Company_Memo_No = Val.ToString(txtCompanyMamoNo.Text);
                            objCompany_MemoIssueReceiptProperty.form_id = m_numForm_id;

                            objCompany_MemoIssueReceiptProperty.remarks = Val.ToString(txtRemark.Text);

                            if (RBtnType.SelectedIndex == 0)
                            {
                                objCompany_MemoIssueReceiptProperty.issue_type_id = Val.ToInt(1);
                            }
                            else
                            {
                                objCompany_MemoIssueReceiptProperty.issue_type_id = Val.ToInt(2);
                            }


                            objCompany_MemoIssueReceiptProperty.assort_id = Val.ToInt(drw["assort_id"]);
                            objCompany_MemoIssueReceiptProperty.sieve_id = Val.ToInt(drw["sieve_id"]);
                            objCompany_MemoIssueReceiptProperty.sub_sieve_id = Val.ToInt(drw["sub_sieve_id"]);
                            objCompany_MemoIssueReceiptProperty.rej_pcs = Val.ToInt(drw["pcs"]);
                            objCompany_MemoIssueReceiptProperty.rej_carat = Val.ToDecimal(drw["carat"]);
                            objCompany_MemoIssueReceiptProperty.rate = Val.ToDecimal(drw["rate"]);
                            objCompany_MemoIssueReceiptProperty.amount = Val.ToDecimal(drw["amount"]);

                            IntRes = objCompanyMamoIssueReceipt.Save(objCompany_MemoIssueReceiptProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

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
                        objCompany_MemoIssueReceiptProperty = null;
                    }
                }
            }
            catch (Exception ex)
            {
                IntRes = -1;
                Conn.Inter1.Rollback();
                Conn.Inter2.Rollback();
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
                    Global.Confirm("Company Memo Issue Receipt Data Save Successfully");
                    ClearDetails();
                }
                else
                {
                    Global.Confirm("Error In Company Memo Issue Receipt Data");
                    lueToCompany.Focus();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }
        #region GridEvents
        private void dgvCompanyMamoIssueReceipt_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                if (Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) > 0 && Val.ToDecimal(clmDetCarat.SummaryItem.SummaryValue) > 0)
                {
                    m_numSummRate = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) / Val.ToDecimal(clmDetCarat.SummaryItem.SummaryValue)), 3, MidpointRounding.AwayFromZero);

                }
                //if (Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) > 0 && Val.ToDecimal(clmDetCarat.SummaryItem.SummaryValue) > 0)
                //{
                //    m_numSummCurrRate = Math.Round((Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue) / Val.ToDecimal(clmDetCarat.SummaryItem.SummaryValue)), 2, MidpointRounding.AwayFromZero);

                //}
                else
                {
                    m_numSummRate = 0;
                    //m_numSummCurrRate = 0;
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
                Global.LOOKUPCompany(lueToCompany);
                Global.LOOKUPBranch(lueToBranch);
                Global.LOOKUPLocation(lueToLocation);
                Global.LOOKUPDepartment(lueToDepartment);
                // Global.LOOKUPDeliveryType(lueDeliveryType);

                m_dtbAssort = objAssort.GetData(1);
                lueAssortName.Properties.DataSource = m_dtbAssort;
                lueAssortName.Properties.ValueMember = "assort_id";
                lueAssortName.Properties.DisplayMember = "assort_name";

                m_dtbSieve = objSieve.GetData(1);
                lueSieveName.Properties.DataSource = m_dtbSieve;
                lueSieveName.Properties.ValueMember = "sieve_id";
                lueSieveName.Properties.DisplayMember = "sieve_name";

                dtpIssueDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpIssueDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpIssueDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpIssueDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpIssueDate.EditValue = DateTime.Now;

                //m_dtbCurrency = objRate.GetCurrencyData(1);
                //if(m_dtbCurrency.Rows.Count>0)
                //{
                //    lblcurrency.Text = Val.ToString(m_dtbCurrency.Rows[0]["currency_rate"]);
                //}
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            finally
            {
                //objAssort = null;
                //objSieve = null;
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
                    if (m_dtbCompanyMemoIssueDetail.Rows.Count == 0)
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
                    //if (m_dtbStockCarat.Rows.Count > 0)
                    //{
                    //    numStockCarat = Val.ToDecimal(m_dtbStockCarat.Rows[0]["stock_carat"]);
                    //}

                    //if (numStockCarat < (Val.ToDecimal(txtCarat.Text)))
                    //{
                    //    Global.Message("Please check enter carat more then stock carat", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    txtCarat.Focus();
                    //    blnReturn = false;
                    //    return blnReturn;
                    //}
                    m_srno = dgvCompanyMamoIssueReceipt.RowCount;
                    DataRow[] dr = m_dtbMemoIssueDetail.Select("sieve_id = " + Val.ToInt(lueSieveName.EditValue) + " AND assort_id = " + Val.ToInt(lueAssortName.EditValue));
                    if (dr.Count() == 1)
                    {
                        Global.Message("Record already exists in grid", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lueAssortName.Focus();
                        blnReturn = false;
                        return blnReturn;
                    }

                    DataRow drwNew = m_dtbMemoIssueDetail.NewRow();
                    decimal numCarat = Val.ToDecimal(txtCarat.Text);
                    decimal numRate = Val.ToDecimal(txtRate.Text);
                    decimal numAmount = Val.ToDecimal(txtAmount.Text);
                    int numPcs = Val.ToInt(txtPcs.Text);

                    //drwNew["memo_no"] = Val.ToString(txtMemoNo.Text);
                    drwNew["memo_id"] = Val.ToInt(0);
                    //drwNew["memo_date"] = Val.ToString(dtpMemoDate.Text);
                    drwNew["assort_id"] = Val.ToInt(lueAssortName.EditValue);
                    drwNew["assort_name"] = Val.ToString(lueAssortName.Text);

                    drwNew["sieve_id"] = Val.ToInt(lueSieveName.EditValue);
                    drwNew["sieve_name"] = Val.ToString(lueSieveName.Text);

                    drwNew["pcs"] = numPcs;
                    drwNew["carat"] = numCarat;
                    drwNew["rate"] = Val.ToDecimal(txtRate.Text);
                    drwNew["amount"] = Math.Round(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text), 3);
                    m_srno = m_srno + 1;

                    drwNew["sr_no"] = m_srno;
                    m_dtbMemoIssueDetail.Rows.Add(drwNew);

                    dgvCompanyMamoIssueReceipt.MoveLast();

                    //DataView dv = m_dtbMemoIssueDetail.DefaultView;
                    //dv.Sort = "sr_no desc";
                    //DataTable sortedDT = dv.ToTable();
                }
                else if (btnAdd.Text == "&Update")
                {
                    //DataRow[] dr = m_dtbMemoIssueDetail.Select("sieve_id = " + Val.ToInt(lueSieveName.EditValue) + " AND assort_id = " + Val.ToInt(lueAssortName.EditValue));
                    //if (dr.Count() == 1)
                    //{
                    //    Global.Message("Record already exists in grid", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    lueAssortName.Focus();
                    //    blnReturn = false;
                    //    return blnReturn;
                    //}

                    if (m_dtbMemoIssueDetail.Select("assort_id ='" + Val.ToInt(lueAssortName.EditValue) + "' AND sieve_id ='" + Val.ToInt(lueSieveName.EditValue) + "'").Length > 0)
                    {
                        for (int i = 0; i < m_dtbMemoIssueDetail.Rows.Count; i++)
                        {
                            //if (m_dtbMemoIssueDetail.Select("memo_id ='" + m_memo_id + "'AND sr_no = '" + m_update_srno + "'").Length > 0)
                            //{
                            //    if (m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["memo_id"].ToString() == m_memo_id.ToString())
                            //    {
                            //        m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["diff_carat"] = m_dtbMemoIssueDetail.Rows[i]["carat"];
                            //        m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["diff_pcs"] = m_dtbMemoIssueDetail.Rows[i]["pcs"];
                            //        m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["carat"] = Val.ToDecimal(txtCarat.Text).ToString();
                            //        m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["pcs"] = Val.ToInt(txtPcs.Text);
                            //        m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["rate"] = Val.ToDecimal(txtRate.Text);
                            //        m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["flag"] = 1;
                            //        // Add By Praful On 13082020
                            //        m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["sieve_id"] = Val.ToInt(lueSieveName.EditValue);
                            //        m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["sub_sieve_id"] = Val.ToInt(lueSubSieveName.EditValue);
                            //        m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["sieve_name"] = Val.ToString(lueSieveName.Text);
                            //        m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["sub_sieve_name"] = Val.ToString(lueSubSieveName.Text);
                            //        // End
                            //        m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["discount_per"] = Val.ToDecimal(txtDiscPer.Text);
                            //        m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["discount_amt"] = Val.ToDecimal(txtDiscAmt.Text);
                            //        m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["net_amount"] = Val.ToDecimal(txtNetAmount.Text);
                            //        m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["amount"] = Math.Round(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text), 3);
                            //        break;


                            //    }
                            //}
                        }
                        btnAdd.Text = "&Add";
                    }
                    else
                    {
                        for (int i = 0; i < m_dtbMemoIssueDetail.Rows.Count; i++)
                        {
                            //if (m_dtbMemoIssueDetail.Select("memo_id ='" + m_memo_id + "' AND sr_no = '" + m_update_srno + "'").Length > 0)
                            //{
                            //    if (m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["memo_id"].ToString() == m_memo_id.ToString())
                            //    {
                            //        m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["diff_carat"] = m_dtbMemoIssueDetail.Rows[i]["carat"];
                            //        m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["diff_pcs"] = m_dtbMemoIssueDetail.Rows[i]["pcs"];
                            //        m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["carat"] = Val.ToDecimal(txtCarat.Text).ToString();
                            //        m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["pcs"] = Val.ToInt(txtPcs.Text);
                            //        m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["rate"] = Val.ToDecimal(txtRate.Text);
                            //        m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["flag"] = 1;
                            //        m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["assort_id"] = Val.ToInt(lueAssortName.EditValue);
                            //        m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["sieve_id"] = Val.ToInt(lueSieveName.EditValue);
                            //        m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["sub_sieve_id"] = Val.ToInt(lueSubSieveName.EditValue);
                            //        m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["assort_name"] = Val.ToString(lueAssortName.Text);
                            //        m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["sieve_name"] = Val.ToString(lueSieveName.Text);
                            //        m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["sub_sieve_name"] = Val.ToString(lueSubSieveName.Text);

                            //        m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["discount_per"] = Val.ToDecimal(txtDiscPer.Text);
                            //        m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["discount_amt"] = Val.ToDecimal(txtDiscAmt.Text);
                            //        m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["net_amount"] = Val.ToDecimal(txtNetAmount.Text);

                            //        m_dtbMemoIssueDetail.Rows[m_update_srno - 1]["amount"] = Math.Round(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text), 3);

                            //    }
                            //}
                        }
                        btnAdd.Text = "&Add";
                    }
                    dgvCompanyMamoIssueReceipt.MoveLast();
                }
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
                //if (!GenerateBranchTransferDetails())
                //{
                //    blnReturn = false;
                //    return blnReturn;
                //}

                //lblMode.Tag = null;
                lueToCompany.EditValue = System.DBNull.Value;
                lueToBranch.EditValue = System.DBNull.Value;
                lueToLocation.EditValue = System.DBNull.Value;
                lueToDepartment.EditValue = System.DBNull.Value;
                txtPartyMemoNo.Text = "";
                txtCompanyMamoNo.Text = "";
                //txtParticuler.Text = string.Empty;
                //txthsn.Text = string.Empty;
                txtRemark.Text = "";
                lueToCompany.Focus();
                RBtnType.SelectedIndex = 0;

                dtpIssueDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpIssueDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpIssueDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpIssueDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpIssueDate.EditValue = DateTime.Now;
                lueAssortName.EditValue = System.DBNull.Value;
                lueSieveName.EditValue = System.DBNull.Value;
                txtPcs.Text = string.Empty;
                txtCarat.Text = string.Empty;
                txtRate.Text = string.Empty;
                txtAmount.Text = string.Empty;

                grdCompanyMamoIssueReceipt.DataSource = null;
                dgvCompanyMamoIssueReceipt.BestFitColumns();
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
                //m_dtbDetails = objBranchTransfer.GetData(Convert.ToDateTime(dtpFromDate.Text), Convert.ToDateTime(dtpToDate.Text));

                //if (m_dtbDetails.Rows.Count == 0)
                //{
                //    Global.Message("Data Not Found");
                //    blnReturn = false;
                //}

                //grdBranchTransfer.DataSource = m_dtbDetails;
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
        public void GetData()
        {
            try
            {
                if (RBtnType.SelectedIndex == 0)
                {
                    m_dtbCompanyMemoIssueDetail = objCompanyMamoIssueReceipt.GetCompanyMemoCarat(Val.ToString(txtPartyMemoNo.Text), Val.ToString(txtCompanyMamoNo.Text));

                    if (m_dtbCompanyMemoIssueDetail.Rows.Count == 0)
                    {
                        m_dtbCompanyMemoIssueDetail = objCompanyMamoIssueReceipt.GetCompanyMemoData(GlobalDec.gEmployeeProperty.company_id, GlobalDec.gEmployeeProperty.branch_id, GlobalDec.gEmployeeProperty.location_id, GlobalDec.gEmployeeProperty.department_id, Val.ToString(txtPartyMemoNo.Text));

                        if (m_dtbCompanyMemoIssueDetail.Rows.Count > 0)
                        {
                            grdCompanyMamoIssueReceipt.DataSource = m_dtbCompanyMemoIssueDetail;
                            txtRemark.Text = Val.ToString(m_dtbCompanyMemoIssueDetail.Rows[0]["remarks"]);
                            dgvCompanyMamoIssueReceipt.BestFitColumns();
                        }
                        else
                        {
                            Global.Message("Data Not Exist in Party Memo Number");
                            txtPartyMemoNo.Focus();
                            grdCompanyMamoIssueReceipt.DataSource = null;
                            dgvCompanyMamoIssueReceipt.BestFitColumns();
                            return;
                        }
                    }
                    else
                    {
                        if (Val.ToDecimal(m_dtbCompanyMemoIssueDetail.Rows[0]["outstanding_carat"]) == 0)
                        {
                            m_dtbCompanyMemoIssueDetail = objCompanyMamoIssueReceipt.GetCompanyMemoData(GlobalDec.gEmployeeProperty.company_id, GlobalDec.gEmployeeProperty.branch_id, GlobalDec.gEmployeeProperty.location_id, GlobalDec.gEmployeeProperty.department_id, Val.ToString(txtPartyMemoNo.Text));

                            if (m_dtbCompanyMemoIssueDetail.Rows.Count > 0)
                            {
                                grdCompanyMamoIssueReceipt.DataSource = m_dtbCompanyMemoIssueDetail;
                                txtRemark.Text = Val.ToString(m_dtbCompanyMemoIssueDetail.Rows[0]["remarks"]);
                                dgvCompanyMamoIssueReceipt.BestFitColumns();
                            }
                            else
                            {
                                Global.Message("Data Not Exist in Party Mamo Number");
                                grdCompanyMamoIssueReceipt.DataSource = null;
                                dgvCompanyMamoIssueReceipt.BestFitColumns();
                                txtPartyMemoNo.Focus();
                                return;
                            }
                        }
                        else
                        {
                            Global.Message("This Party Memo Number already Issue : " + Val.ToString(txtPartyMemoNo.Text));
                            grdCompanyMamoIssueReceipt.DataSource = null;
                            dgvCompanyMamoIssueReceipt.BestFitColumns();
                            txtPartyMemoNo.Focus();
                            return;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        public void GetData_Receive()
        {
            try
            {
                if (RBtnType.SelectedIndex == 1)
                {
                    m_dtbCompanyMemoIssueDetail = objCompanyMamoIssueReceipt.GetCompanyMemoCarat(Val.ToString(txtPartyMemoNo.Text), Val.ToString(txtCompanyMamoNo.Text));

                    if (Val.ToDecimal(m_dtbCompanyMemoIssueDetail.Rows[0]["outstanding_carat"]) > 0)
                    {
                        m_dtbCompanyMemoIssueDetail = objCompanyMamoIssueReceipt.GetCompanyMemoReceiveData(GlobalDec.gEmployeeProperty.company_id, GlobalDec.gEmployeeProperty.branch_id, GlobalDec.gEmployeeProperty.location_id, GlobalDec.gEmployeeProperty.department_id, Val.ToString(txtCompanyMamoNo.Text));

                        if (m_dtbCompanyMemoIssueDetail.Rows.Count > 0)
                        {
                            lueToCompany.EditValue = Val.ToInt32(m_dtbCompanyMemoIssueDetail.Rows[0]["to_company_id"]);
                            lueToBranch.EditValue = Val.ToInt32(m_dtbCompanyMemoIssueDetail.Rows[0]["to_branch_id"]);
                            lueToLocation.EditValue = Val.ToInt32(m_dtbCompanyMemoIssueDetail.Rows[0]["to_location_id"]);
                            lueToDepartment.EditValue = Val.ToInt32(m_dtbCompanyMemoIssueDetail.Rows[0]["to_department_id"]);
                            txtPartyMemoNo.Text = Val.ToString(m_dtbCompanyMemoIssueDetail.Rows[0]["party_memo_no"]);
                            dtpIssueDate.Text = Val.DBDate(m_dtbCompanyMemoIssueDetail.Rows[0]["Company_memo_date"].ToString());

                            grdCompanyMamoIssueReceipt.DataSource = m_dtbCompanyMemoIssueDetail;
                            txtRemark.Text = Val.ToString(m_dtbCompanyMemoIssueDetail.Rows[0]["remarks"]);
                            dgvCompanyMamoIssueReceipt.BestFitColumns();
                        }
                        else
                        {
                            Global.Message("Data Not Exist in Party Memo Number");
                            txtPartyMemoNo.Focus();
                            grdCompanyMamoIssueReceipt.DataSource = null;
                            dgvCompanyMamoIssueReceipt.BestFitColumns();
                            return;
                        }
                    }
                    else
                    {
                        Global.Message("This Party Memo Number already Receive : " + Val.ToString(txtPartyMemoNo.Text));
                        txtPartyMemoNo.Focus();
                        return;
                    }
                }

            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
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


        #endregion       
    }
}