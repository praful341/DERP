using BLL;
using BLL.FunctionClasses.Master.HR;
using BLL.FunctionClasses.Transaction;
using BLL.PropertyClasses.Master.HR;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DREP.Master.HR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Transaction.MFG
{
    public partial class FrmHRTransactionEntry : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member
        
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;
        HRTransactionEntry objHRTransactionEntry;
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);
        private List<Control> _tabControls = new List<Control>();
        Control _NextEnteredControl = new Control();
        FillCombo ObjFillCombo = new FillCombo();
        DataTable DtControlSettings;
        DataTable dtTemp;
        Int64 New_Union_Id;
        int IntRes;
        Int32 counter = 0;

        #endregion

        #region Constructor
        public FrmHRTransactionEntry()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            objHRTransactionEntry = new HRTransactionEntry();
            DtControlSettings = new DataTable();
            New_Union_Id = 0;
            IntRes = 0;
        }
        public void ShowForm()
        {
            ObjPer.FormName = this.Name.ToUpper();
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
            // AddKeyPressListener(this);
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

        #region Events
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
                btnSave.Enabled = false;
                if (!ValidateDetails())
                {
                    btnSave.Enabled = true;
                    return;
                }

                if (txtBookNo.Text.ToString() == "")
                {
                    Global.Message("Please Generate Book No..");
                    return;
                }

                DialogResult result = MessageBox.Show("Do you want to save HR Transaction data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result != DialogResult.Yes)
                {
                    btnSave.Enabled = true;
                    return;
                }

                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                PanelLoading.Visible = true;
                backgroundWorker_HRTransactionEntry.RunWorkerAsync();

                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            grdHRTransactionHistory.DataSource = null;
            ClearDetails();
            panelControl1.Enabled = true;
            panelControl5.Enabled = true;
            lueFactory.Enabled = true;
            lueFactDepartment.Enabled = true;
            lueManager.Enabled = true;
            txtYear.Enabled = true;
            txtMonth.Enabled = true;
            txtBookNo.Enabled = true;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void lueFactory_EditValueChanged(object sender, EventArgs e)
        {
            if (Val.ToString(lueFactory.Text) != "" && Val.ToString(lueFactDepartment.Text) != "")
            {
                HREmployee_MasterProperty HREmpMasterProperty = new HREmployee_MasterProperty();
                HREmployeeMaster objHREmp = new HREmployeeMaster();
                HREmpMasterProperty.fact_department_id = Val.ToInt64(lueFactDepartment.EditValue);
                HREmpMasterProperty.factory_id = Val.ToInt64(lueFactory.EditValue);
                HREmpMasterProperty.active_new = Val.ToInt32(1);

                DataTable Dtab_Fact_Dept = objHREmp.Fact_Dept_Wise_ManagerGetData(HREmpMasterProperty);

                if (Dtab_Fact_Dept.Rows.Count > 0)
                {
                    lueManager.Properties.DataSource = null;
                    lueManager.Properties.DataSource = Dtab_Fact_Dept;
                    lueManager.Properties.ValueMember = "manager_id";
                    lueManager.Properties.DisplayMember = "manager_name";
                }
                else
                {
                    lueManager.Properties.DataSource = null;
                }
            }
        }
        private void lueFactDepartment_EditValueChanged(object sender, EventArgs e)
        {
            if (Val.ToString(lueFactory.Text) != "" && Val.ToString(lueFactDepartment.Text) != "")
            {
                HREmployee_MasterProperty HREmpMasterProperty = new HREmployee_MasterProperty();
                HREmployeeMaster objHREmp = new HREmployeeMaster();
                HREmpMasterProperty.fact_department_id = Val.ToInt64(lueFactDepartment.EditValue);
                HREmpMasterProperty.factory_id = Val.ToInt64(lueFactory.EditValue);
                HREmpMasterProperty.active_new = Val.ToInt32(1);

                DataTable Dtab_Fact_Dept = objHREmp.Fact_Dept_Wise_ManagerGetData(HREmpMasterProperty);

                if (Dtab_Fact_Dept.Rows.Count > 0)
                {
                    lueManager.Properties.DataSource = null;
                    lueManager.Properties.DataSource = Dtab_Fact_Dept;
                    lueManager.Properties.ValueMember = "manager_id";
                    lueManager.Properties.DisplayMember = "manager_name";
                }
                else
                {
                    lueManager.Properties.DataSource = null;
                }
            }
        }
        private void txtYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtMonth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void BtnGenerateBookNo_Click(object sender, EventArgs e)
        {
            if (txtYear.Text == "")
            {
                Global.Message("Year can't not be Blank");
                txtYear.Focus();
                return;
            }
            if (txtMonth.Text == "")
            {
                Global.Message("Month can't not be Blank");
                txtMonth.Focus();
                return;
            }

            txtBookNo.Text = objHRTransactionEntry.FindNewID(Val.ToInt(txtYear.Text), Val.ToInt(txtMonth.Text)).ToString();

            if (txtBookNo.Text == "1")
            {
                txtBookNo.Text = txtYear.Text + txtMonth.Text + "01";
            }

            btnSearch_Click(null, null);
        }
        private void txtMonth_TextChanged(object sender, EventArgs e)
        {
            //if (txtYear.Text == "")
            //{
            //    Global.Message("Year can't not be Blank");
            //    txtYear.Focus();
            //    return;
            //}
            //if (txtMonth.Text == "")
            //{
            //    Global.Message("Month can't not be Blank");
            //    txtMonth.Focus();
            //    return;
            //}

            //if (txtYear.Text != "" && txtMonth.Text != "")
            //{
            //    txtBookNo.Text = objHRTransactionEntry.FindNewID(Val.ToInt(txtYear.Text), Val.ToInt(txtMonth.Text)).ToString();

            //    if (txtBookNo.Text == "1")
            //    {
            //        txtBookNo.Text = txtYear.Text + txtMonth.Text + "01";
            //    }
            //}

            if (txtYear.Text != "" && txtMonth.Text != "")
            {
                txtBookNo.Text = objHRTransactionEntry.FindNewID_Search(Val.ToInt(txtYear.Text), Val.ToInt(txtMonth.Text)).ToString();

                if (txtBookNo.Text == "0")
                {
                    txtBookNo.Text = txtYear.Text + txtMonth.Text + "01";
                }
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateDetails())
                {
                    return;
                }
                PanelLoading.Visible = true;
                dgvHRTransactionHistory.Columns.Clear();
                HRTransactionEntryProperty HRTransactionEntryProperty = new HRTransactionEntryProperty();

                HRTransactionEntryProperty.manager_id = Val.ToInt64(lueManager.EditValue);
                HRTransactionEntryProperty.factory_id = Val.ToInt64(lueFactory.EditValue);
                HRTransactionEntryProperty.fact_department_id = Val.ToInt64(lueFactDepartment.EditValue);
                HRTransactionEntryProperty.year = Val.ToInt(txtYear.Text);
                HRTransactionEntryProperty.month = Val.ToInt(txtMonth.Text);
                HRTransactionEntryProperty.book_no = Val.ToInt64(txtBookNo.Text);

                DataTable FillEmpData = objHRTransactionEntry.Emp_GetData(HRTransactionEntryProperty);

                if (FillEmpData.Rows.Count > 0)
                {
                    if (FillEmpData.Rows[0]["flag"].ToString() == "0")
                    {
                        int y = Val.ToInt(txtYear.Text);
                        int m = Val.ToInt(txtMonth.Text);
                        int res = DateTime.DaysInMonth(y, m);

                        for (int i = 1; i <= res; i++)
                        {
                            FillEmpData.Columns.Add(i.ToString(), typeof(string));
                        }

                        FillEmpData.Columns.Remove("flag");
                        FillEmpData.AcceptChanges();

                        lblUnionID.Text = "0";

                        DataColumn Total = new System.Data.DataColumn("Total", typeof(System.Decimal));
                        Total.DefaultValue = "0";
                        FillEmpData.Columns.Add(Total);

                        Int64 total = 0;
                        Int64 Total_Qty = 0;

                        for (int i = 0; i <= FillEmpData.Rows.Count - 1; i++)
                        {
                            for (int j = 0; j <= FillEmpData.Columns.Count - 1; j++)
                            {
                                if (FillEmpData.Columns[j].ToString().Contains("1") || FillEmpData.Columns[j].ToString().Contains("2") || FillEmpData.Columns[j].ToString().Contains("3") || FillEmpData.Columns[j].ToString().Contains("4") || FillEmpData.Columns[j].ToString().Contains("5") || FillEmpData.Columns[j].ToString().Contains("6") || FillEmpData.Columns[j].ToString().Contains("7") || FillEmpData.Columns[j].ToString().Contains("8") || FillEmpData.Columns[j].ToString().Contains("9"))
                                {
                                    Total_Qty = Val.ToInt64(FillEmpData.Rows[i][j]);
                                    total += Total_Qty;
                                    FillEmpData.Rows[i][j] = "";
                                }
                                if (FillEmpData.Columns[j].ColumnName.Contains("Total"))
                                {
                                    FillEmpData.Rows[i][j] = total;
                                }
                            }
                            total = 0;
                        }

                        grdHRTransactionHistory.DataSource = FillEmpData;
                        dgvHRTransactionHistory.Columns["employee_id"].Visible = false;
                        dgvHRTransactionHistory.Columns["employee_name"].OptionsColumn.AllowEdit = false;
                        dgvHRTransactionHistory.Columns["Total"].OptionsColumn.ReadOnly = true;
                        dgvHRTransactionHistory.Columns["Total"].OptionsColumn.AllowFocus = false;
                        dgvHRTransactionHistory.Columns["employee_name"].Caption = "Emp Name";
                        dgvHRTransactionHistory.Columns["sr_no"].Caption = "Sr No";

                        dgvHRTransactionHistory.Columns["employee_name"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                        dgvHRTransactionHistory.Columns["sr_no"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

                        //txtBookNo.Text = objHRTransactionEntry.FindNewID_Search(Val.ToInt(txtYear.Text), Val.ToInt(txtMonth.Text)).ToString();

                        //if (txtBookNo.Text == "0")
                        //{
                        //    txtBookNo.Text = txtYear.Text + txtMonth.Text + "01";
                        //}

                        for (int i = 0; i <= FillEmpData.Rows.Count - 1; i++)
                        {
                            for (int j = 0; j <= FillEmpData.Columns.Count - 1; j++)
                            {
                                if (FillEmpData.Columns[j].ToString().Contains("1") || FillEmpData.Columns[j].ToString().Contains("2") || FillEmpData.Columns[j].ToString().Contains("3") || FillEmpData.Columns[j].ToString().Contains("4") || FillEmpData.Columns[j].ToString().Contains("5") || FillEmpData.Columns[j].ToString().Contains("6") || FillEmpData.Columns[j].ToString().Contains("7") || FillEmpData.Columns[j].ToString().Contains("8") || FillEmpData.Columns[j].ToString().Contains("9"))
                                {
                                    dgvHRTransactionHistory.Columns[FillEmpData.Columns[j].ToString()].Summary.Add(SummaryItemType.Sum, FillEmpData.Columns[j].ToString(), "{0:N0}");
                                    dgvHRTransactionHistory.GroupSummary.Add(SummaryItemType.Sum, FillEmpData.Columns[j].ToString(), dgvHRTransactionHistory.Columns[FillEmpData.Columns[j].ToString()], "{0:N0}");
                                    dgvHRTransactionHistory.OptionsBehavior.SummariesIgnoreNullValues = true;
                                    dgvHRTransactionHistory.Columns[FillEmpData.Columns[j].ToString()].Summary.Add(SummaryItemType.Custom, FillEmpData.Columns[j].ToString(), "{0:N0}");
                                    dgvHRTransactionHistory.GroupSummary.Add(SummaryItemType.Custom, FillEmpData.Columns[j].ToString(), dgvHRTransactionHistory.Columns[FillEmpData.Columns[j].ToString()], "{0:N0}");
                                    dgvHRTransactionHistory.Columns[j].Width = 40;
                                }
                                if (FillEmpData.Columns[j].ToString().Contains("Total"))
                                {
                                    dgvHRTransactionHistory.Columns[FillEmpData.Columns[j].ToString()].Summary.Add(SummaryItemType.Sum, FillEmpData.Columns[j].ToString(), "{0:N0}");
                                    dgvHRTransactionHistory.GroupSummary.Add(SummaryItemType.Sum, FillEmpData.Columns[j].ToString(), dgvHRTransactionHistory.Columns[FillEmpData.Columns[j].ToString()], "{0:N0}");
                                    dgvHRTransactionHistory.Columns[FillEmpData.Columns[j].ToString()].Summary.Add(SummaryItemType.Count, FillEmpData.Columns[j].ToString(), "{0:N0}");
                                    dgvHRTransactionHistory.GroupSummary.Add(SummaryItemType.Count, FillEmpData.Columns[j].ToString(), dgvHRTransactionHistory.Columns[FillEmpData.Columns[j].ToString()], "{0:N0}");
                                    dgvHRTransactionHistory.Columns["Total"].Width = 60;
                                }
                                if (FillEmpData.Columns[j].ToString().Contains("employee_name"))
                                {
                                    dgvHRTransactionHistory.Columns["employee_name"].Width = 200;
                                }
                                if (FillEmpData.Columns[j].ToString().Contains("sr_no"))
                                {
                                    dgvHRTransactionHistory.Columns[FillEmpData.Columns[j].ToString()].Summary.Add(SummaryItemType.Count, FillEmpData.Columns[j].ToString(), "{0:N0}");
                                    dgvHRTransactionHistory.GroupSummary.Add(SummaryItemType.Count, FillEmpData.Columns[j].ToString(), dgvHRTransactionHistory.Columns[FillEmpData.Columns[j].ToString()], "{0:N0}");
                                    dgvHRTransactionHistory.OptionsBehavior.SummariesIgnoreNullValues = true;
                                    dgvHRTransactionHistory.Columns[FillEmpData.Columns[j].ToString()].Summary.Add(SummaryItemType.Custom, FillEmpData.Columns[j].ToString(), "{0:N0}");
                                    dgvHRTransactionHistory.GroupSummary.Add(SummaryItemType.Custom, FillEmpData.Columns[j].ToString(), dgvHRTransactionHistory.Columns[FillEmpData.Columns[j].ToString()], "{0:N0}");
                                    dgvHRTransactionHistory.Columns["sr_no"].Width = 45;
                                }
                                if (FillEmpData.Columns[j].ToString() == "1" || FillEmpData.Columns[j].ToString() == "2" || FillEmpData.Columns[j].ToString() == "3" || FillEmpData.Columns[j].ToString() == "4" || FillEmpData.Columns[j].ToString() == "5" || FillEmpData.Columns[j].ToString() == "6" || FillEmpData.Columns[j].ToString() == "7" ||
                                    FillEmpData.Columns[j].ToString() == "8" || FillEmpData.Columns[j].ToString() == "9" || FillEmpData.Columns[j].ToString() == "10" || FillEmpData.Columns[j].ToString() == "11" || FillEmpData.Columns[j].ToString() == "12" || FillEmpData.Columns[j].ToString() == "13" || FillEmpData.Columns[j].ToString() == "14" ||
                                    FillEmpData.Columns[j].ToString() == "15" || FillEmpData.Columns[j].ToString() == "16" || FillEmpData.Columns[j].ToString() == "17" || FillEmpData.Columns[j].ToString() == "18" || FillEmpData.Columns[j].ToString() == "19" || FillEmpData.Columns[j].ToString() == "20" || FillEmpData.Columns[j].ToString() == "21" ||
                                     FillEmpData.Columns[j].ToString() == "22" || FillEmpData.Columns[j].ToString() == "23" || FillEmpData.Columns[j].ToString() == "24" || FillEmpData.Columns[j].ToString() == "25" || FillEmpData.Columns[j].ToString() == "26" || FillEmpData.Columns[j].ToString() == "27" || FillEmpData.Columns[j].ToString() == "28")
                                {
                                    dgvHRTransactionHistory.Columns["1"].Width = 50;
                                    dgvHRTransactionHistory.Columns["2"].Width = 50;
                                    dgvHRTransactionHistory.Columns["3"].Width = 50;
                                    dgvHRTransactionHistory.Columns["4"].Width = 50;
                                    dgvHRTransactionHistory.Columns["5"].Width = 50;
                                    dgvHRTransactionHistory.Columns["6"].Width = 50;
                                    dgvHRTransactionHistory.Columns["7"].Width = 50;
                                    dgvHRTransactionHistory.Columns["8"].Width = 50;
                                    dgvHRTransactionHistory.Columns["9"].Width = 50;
                                    dgvHRTransactionHistory.Columns["10"].Width = 50;
                                    dgvHRTransactionHistory.Columns["11"].Width = 50;
                                    dgvHRTransactionHistory.Columns["12"].Width = 50;
                                    dgvHRTransactionHistory.Columns["13"].Width = 50;
                                    dgvHRTransactionHistory.Columns["14"].Width = 50;
                                    dgvHRTransactionHistory.Columns["15"].Width = 50;
                                    dgvHRTransactionHistory.Columns["16"].Width = 50;
                                    dgvHRTransactionHistory.Columns["17"].Width = 50;
                                    dgvHRTransactionHistory.Columns["18"].Width = 50;
                                    dgvHRTransactionHistory.Columns["19"].Width = 50;
                                    dgvHRTransactionHistory.Columns["20"].Width = 50;
                                    dgvHRTransactionHistory.Columns["21"].Width = 50;
                                    dgvHRTransactionHistory.Columns["22"].Width = 50;
                                    dgvHRTransactionHistory.Columns["23"].Width = 50;
                                    dgvHRTransactionHistory.Columns["24"].Width = 50;
                                    dgvHRTransactionHistory.Columns["25"].Width = 50;
                                    dgvHRTransactionHistory.Columns["26"].Width = 50;
                                    dgvHRTransactionHistory.Columns["27"].Width = 50;
                                    dgvHRTransactionHistory.Columns["28"].Width = 50;
                                }
                                if (FillEmpData.Columns[j].ToString() == "29")
                                {
                                    dgvHRTransactionHistory.Columns["29"].Width = 50;
                                }
                                if (FillEmpData.Columns[j].ToString() == "30")
                                {
                                    dgvHRTransactionHistory.Columns["30"].Width = 50;
                                }
                                if (FillEmpData.Columns[j].ToString() == "31")
                                {
                                    dgvHRTransactionHistory.Columns["31"].Width = 50;
                                }
                            }
                            break;
                        }
                    }
                    else
                    {
                        lblUnionID.Text = Val.ToInt64(FillEmpData.Rows[0]["union_id"]).ToString();
                        //txtBookNo.Text = Val.ToInt64(FillEmpData.Rows[0]["book_no"]).ToString();

                        FillEmpData.Columns.Remove("union_id");
                        FillEmpData.Columns.Remove("book_no");
                        FillEmpData.Columns.Remove("flag");
                        FillEmpData.AcceptChanges();
                        if (FillEmpData.Rows.Count > 0)
                        {
                            pivot pt = new pivot(FillEmpData);
                            dtTemp = pt.PivotDataSuperPlusAssortment(new string[] { "sr_no", "employee_id", "employee_name" }, new string[] { "total_qty" }, new AggregateFunction[] { AggregateFunction.Sum, AggregateFunction.Sum }, new string[] { "transaction_date" });
                        }

                        DataColumn Total = new System.Data.DataColumn("Total", typeof(System.Decimal));
                        Total.DefaultValue = "0";
                        dtTemp.Columns.Add(Total);

                        for (int i = dtTemp.Columns.Count - 1; i >= 3; i--)
                        {
                            string strNew = Val.ToString(dtTemp.Columns[i]);
                            string abc = strNew.ToString();
                            string[] Split = abc.Split(new Char[] { '_' });
                            if (Split.Length == 3)
                            {
                                dtTemp.Columns[Val.ToString(dtTemp.Columns[i])].ColumnName = strNew.Split('_')[0];
                            }
                        }

                        Int64 total = 0;
                        Int64 Total_Qty = 0;

                        for (int i = 0; i <= dtTemp.Rows.Count - 1; i++)
                        {
                            for (int j = 0; j <= dtTemp.Columns.Count - 1; j++)
                            {
                                if (dtTemp.Columns[j].ToString().Contains("1") || dtTemp.Columns[j].ToString().Contains("2") || dtTemp.Columns[j].ToString().Contains("3") || dtTemp.Columns[j].ToString().Contains("4") || dtTemp.Columns[j].ToString().Contains("5") || dtTemp.Columns[j].ToString().Contains("6") || dtTemp.Columns[j].ToString().Contains("7") || dtTemp.Columns[j].ToString().Contains("8") || dtTemp.Columns[j].ToString().Contains("9"))
                                {
                                    Total_Qty = Val.ToInt64(dtTemp.Rows[i][j]);
                                    total += Total_Qty;
                                }
                                if (dtTemp.Columns[j].ColumnName.Contains("Total"))
                                {
                                    dtTemp.Rows[i][j] = total;
                                }
                                else
                                {
                                    if (!dtTemp.Columns[j].ColumnName.Contains("sr_no"))
                                    {
                                        if (dtTemp.Rows[i][j].ToString() == "0")
                                        {
                                            dtTemp.Rows[i][j] = "";
                                        }
                                    }
                                }
                            }
                            total = 0;
                        }

                        grdHRTransactionHistory.DataSource = dtTemp;
                        dgvHRTransactionHistory.Columns["employee_id"].Visible = false;
                        dgvHRTransactionHistory.Columns["employee_name"].OptionsColumn.AllowEdit = false;
                        dgvHRTransactionHistory.Columns["Total"].OptionsColumn.ReadOnly = true;
                        dgvHRTransactionHistory.Columns["Total"].OptionsColumn.AllowFocus = false;
                        dgvHRTransactionHistory.Columns["employee_name"].Caption = "Emp Name";
                        dgvHRTransactionHistory.Columns["sr_no"].Caption = "Sr No";
                        dgvHRTransactionHistory.Columns["employee_name"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                        dgvHRTransactionHistory.Columns["sr_no"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

                        for (int i = 0; i <= dtTemp.Rows.Count - 1; i++)
                        {
                            for (int j = 0; j <= dtTemp.Columns.Count - 1; j++)
                            {
                                if (dtTemp.Columns[j].ToString().Contains("1") || dtTemp.Columns[j].ToString().Contains("2") || dtTemp.Columns[j].ToString().Contains("3") || dtTemp.Columns[j].ToString().Contains("4") || dtTemp.Columns[j].ToString().Contains("5") || dtTemp.Columns[j].ToString().Contains("6") || dtTemp.Columns[j].ToString().Contains("7") || dtTemp.Columns[j].ToString().Contains("8") || dtTemp.Columns[j].ToString().Contains("9"))
                                {
                                    dgvHRTransactionHistory.Columns[dtTemp.Columns[j].ToString()].Summary.Add(SummaryItemType.Sum, dtTemp.Columns[j].ToString(), "{0:N0}");
                                    dgvHRTransactionHistory.GroupSummary.Add(SummaryItemType.Sum, dtTemp.Columns[j].ToString(), dgvHRTransactionHistory.Columns[dtTemp.Columns[j].ToString()], "{0:N0}");
                                    dgvHRTransactionHistory.OptionsBehavior.SummariesIgnoreNullValues = true;
                                    dgvHRTransactionHistory.Columns[dtTemp.Columns[j].ToString()].Summary.Add(SummaryItemType.Custom, dtTemp.Columns[j].ToString(), "{0:N0}");
                                    dgvHRTransactionHistory.GroupSummary.Add(SummaryItemType.Custom, dtTemp.Columns[j].ToString(), dgvHRTransactionHistory.Columns[dtTemp.Columns[j].ToString()], "{0:N0}");

                                    dgvHRTransactionHistory.Columns[j].Width = 40;
                                }
                                if (dtTemp.Columns[j].ToString().Contains("Total"))
                                {
                                    dgvHRTransactionHistory.Columns[dtTemp.Columns[j].ToString()].Summary.Add(SummaryItemType.Sum, dtTemp.Columns[j].ToString(), "{0:N0}");
                                    dgvHRTransactionHistory.GroupSummary.Add(SummaryItemType.Sum, dtTemp.Columns[j].ToString(), dgvHRTransactionHistory.Columns[dtTemp.Columns[j].ToString()], "{0:N0}");
                                    dgvHRTransactionHistory.Columns[dtTemp.Columns[j].ToString()].Summary.Add(SummaryItemType.Count, dtTemp.Columns[j].ToString(), "{0:N0}");
                                    dgvHRTransactionHistory.GroupSummary.Add(SummaryItemType.Count, dtTemp.Columns[j].ToString(), dgvHRTransactionHistory.Columns[dtTemp.Columns[j].ToString()], "{0:N0}");
                                    dgvHRTransactionHistory.Columns["Total"].Width = 60;
                                }
                                if (dtTemp.Columns[j].ToString().Contains("employee_name"))
                                {
                                    dgvHRTransactionHistory.Columns["employee_name"].Width = 200;
                                }
                                if (dtTemp.Columns[j].ToString().Contains("sr_no"))
                                {
                                    dgvHRTransactionHistory.Columns[dtTemp.Columns[j].ToString()].Summary.Add(SummaryItemType.Count, dtTemp.Columns[j].ToString(), "{0:N0}");
                                    dgvHRTransactionHistory.GroupSummary.Add(SummaryItemType.Count, dtTemp.Columns[j].ToString(), dgvHRTransactionHistory.Columns[dtTemp.Columns[j].ToString()], "{0:N0}");
                                    dgvHRTransactionHistory.OptionsBehavior.SummariesIgnoreNullValues = true;
                                    dgvHRTransactionHistory.Columns[dtTemp.Columns[j].ToString()].Summary.Add(SummaryItemType.Custom, dtTemp.Columns[j].ToString(), "{0:N0}");
                                    dgvHRTransactionHistory.GroupSummary.Add(SummaryItemType.Custom, dtTemp.Columns[j].ToString(), dgvHRTransactionHistory.Columns[dtTemp.Columns[j].ToString()], "{0:N0}");
                                    dgvHRTransactionHistory.Columns["sr_no"].Width = 45;
                                }
                                if (dtTemp.Columns[j].ToString() == "1" || dtTemp.Columns[j].ToString() == "2" || dtTemp.Columns[j].ToString() == "3" || dtTemp.Columns[j].ToString() == "4" || dtTemp.Columns[j].ToString() == "5" || dtTemp.Columns[j].ToString() == "6" || dtTemp.Columns[j].ToString() == "7" ||
                                    dtTemp.Columns[j].ToString() == "8" || dtTemp.Columns[j].ToString() == "9" || dtTemp.Columns[j].ToString() == "10" || dtTemp.Columns[j].ToString() == "11" || dtTemp.Columns[j].ToString() == "12" || dtTemp.Columns[j].ToString() == "13" || dtTemp.Columns[j].ToString() == "14" ||
                                    dtTemp.Columns[j].ToString() == "15" || dtTemp.Columns[j].ToString() == "16" || dtTemp.Columns[j].ToString() == "17" || dtTemp.Columns[j].ToString() == "18" || dtTemp.Columns[j].ToString() == "19" || dtTemp.Columns[j].ToString() == "20" || dtTemp.Columns[j].ToString() == "21" ||
                                     dtTemp.Columns[j].ToString() == "22" || dtTemp.Columns[j].ToString() == "23" || dtTemp.Columns[j].ToString() == "24" || dtTemp.Columns[j].ToString() == "25" || dtTemp.Columns[j].ToString() == "26" || dtTemp.Columns[j].ToString() == "27" || dtTemp.Columns[j].ToString() == "28")
                                {
                                    dgvHRTransactionHistory.Columns["1"].Width = 50;
                                    dgvHRTransactionHistory.Columns["2"].Width = 50;
                                    dgvHRTransactionHistory.Columns["3"].Width = 50;
                                    dgvHRTransactionHistory.Columns["4"].Width = 50;
                                    dgvHRTransactionHistory.Columns["5"].Width = 50;
                                    dgvHRTransactionHistory.Columns["6"].Width = 50;
                                    dgvHRTransactionHistory.Columns["7"].Width = 50;
                                    dgvHRTransactionHistory.Columns["8"].Width = 50;
                                    dgvHRTransactionHistory.Columns["9"].Width = 50;
                                    dgvHRTransactionHistory.Columns["10"].Width = 50;
                                    dgvHRTransactionHistory.Columns["11"].Width = 50;
                                    dgvHRTransactionHistory.Columns["12"].Width = 50;
                                    dgvHRTransactionHistory.Columns["13"].Width = 50;
                                    dgvHRTransactionHistory.Columns["14"].Width = 50;
                                    dgvHRTransactionHistory.Columns["15"].Width = 50;
                                    dgvHRTransactionHistory.Columns["16"].Width = 50;
                                    dgvHRTransactionHistory.Columns["17"].Width = 50;
                                    dgvHRTransactionHistory.Columns["18"].Width = 50;
                                    dgvHRTransactionHistory.Columns["19"].Width = 50;
                                    dgvHRTransactionHistory.Columns["20"].Width = 50;
                                    dgvHRTransactionHistory.Columns["21"].Width = 50;
                                    dgvHRTransactionHistory.Columns["22"].Width = 50;
                                    dgvHRTransactionHistory.Columns["23"].Width = 50;
                                    dgvHRTransactionHistory.Columns["24"].Width = 50;
                                    dgvHRTransactionHistory.Columns["25"].Width = 50;
                                    dgvHRTransactionHistory.Columns["26"].Width = 50;
                                    dgvHRTransactionHistory.Columns["27"].Width = 50;
                                    dgvHRTransactionHistory.Columns["28"].Width = 50;
                                }
                                if (dtTemp.Columns[j].ToString() == "29")
                                {
                                    dgvHRTransactionHistory.Columns["29"].Width = 50;
                                }
                                if (dtTemp.Columns[j].ToString() == "30")
                                {
                                    dgvHRTransactionHistory.Columns["30"].Width = 50;
                                }
                                if (dtTemp.Columns[j].ToString() == "31")
                                {
                                    dgvHRTransactionHistory.Columns["31"].Width = 50;
                                }

                            }
                            break;
                        }
                    }
                }
                else
                {
                    //txtBookNo.Text = "0";
                    lblUnionID.Text = "0";
                    grdHRTransactionHistory.DataSource = null;
                    dgvHRTransactionHistory.Columns.Clear();
                    btnSearch.Enabled = true;
                    PanelLoading.Visible = false;
                    Global.Message("Manager Name not found in Employee Master");
                    return;
                }

                dgvHRTransactionHistory.OptionsView.ColumnAutoWidth = false;
                dgvHRTransactionHistory.OptionsView.ShowFooter = true;
                PanelLoading.Visible = false;
                //panelControl5.Enabled = false;
                lueFactory.Enabled = false;
                lueFactDepartment.Enabled = false;
                lueManager.Enabled = false;
                txtYear.Enabled = true;
                txtMonth.Enabled = true;
                txtBookNo.Enabled = false;
            }
            catch (Exception ex)
            {
                PanelLoading.Visible = false;
                General.ShowErrors(ex.ToString());
            }
        }
        private void FrmHRTransactionEntry_Load(object sender, EventArgs e)
        {
            try
            {
                Global.LOOKUPHRActiveFactory(lueFactory);
                Global.LOOKUPHRFactoryActiveDept(lueFactDepartment);

                DTPCLDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                DTPCLDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                DTPCLDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                DTPCLDate.Properties.CharacterCasing = CharacterCasing.Upper;
                DTPCLDate.EditValue = DateTime.Now;
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void backgroundWorker_HRTransactionEntry_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                HRTransactionEntry HRTransactionEntry = new HRTransactionEntry();
                HRTransactionEntryProperty objHRTransactionEntryProperty = new HRTransactionEntryProperty();

                //Conn = new BeginTranConnection(true, false);

                DataTable m_DTab = new DataTable();
                New_Union_Id = 0;
                try
                {
                    m_DTab = (DataTable)grdHRTransactionHistory.DataSource;

                    //m_DTab.Columns.Remove("Total");
                    m_DTab.AcceptChanges();

                    int IntCounter = 0;
                    int Count = 0;
                    int TotalCount = m_DTab.Rows.Count;

                    for (int i = 0; i <= m_DTab.Rows.Count - 1; i++)
                    {
                        for (int j = 3; j <= m_DTab.Columns.Count - 2; j++)
                        {
                            //Decimal TotalPrice = Convert.ToDecimal(m_DTab.Compute("SUM(1)", string.Empty));
                            //    for (int j = m_DTab.Columns.Count - 1; j >= 3; j--)
                            //{
                            objHRTransactionEntryProperty.employee_id = Val.ToInt64(m_DTab.Rows[i]["employee_id"]);
                            objHRTransactionEntryProperty.sr_no = Val.ToInt64(m_DTab.Rows[i]["sr_no"]);
                            objHRTransactionEntryProperty.factory_id = Val.ToInt64(lueFactory.EditValue);
                            objHRTransactionEntryProperty.fact_department_id = Val.ToInt64(lueFactDepartment.EditValue);
                            objHRTransactionEntryProperty.manager_id = Val.ToInt64(lueManager.EditValue);
                            objHRTransactionEntryProperty.year = Val.ToInt(txtYear.Text);
                            objHRTransactionEntryProperty.month = Val.ToInt(txtMonth.Text);
                            objHRTransactionEntryProperty.book_no = Val.ToInt64(txtBookNo.Text);
                            objHRTransactionEntryProperty.total_qty = Val.ToInt32(m_DTab.Rows[i][j]);

                            DateTime date = new DateTime(objHRTransactionEntryProperty.year, objHRTransactionEntryProperty.month, Val.ToInt32(m_DTab.Columns[j]));

                            objHRTransactionEntryProperty.transaction_date = Val.DBDate(date.ToString());
                            objHRTransactionEntryProperty.transaction_time = Val.ToString(GlobalDec.gStr_SystemTime);
                            if (Val.ToInt64(lblUnionID.Text) > 0)
                            {
                                New_Union_Id = Val.ToInt64(lblUnionID.Text);
                                objHRTransactionEntryProperty.union_id = Val.ToInt64(New_Union_Id);
                            }
                            else
                            {
                                objHRTransactionEntryProperty.union_id = Val.ToInt64(New_Union_Id);
                            }
                            objHRTransactionEntryProperty.current_rate = Val.ToDecimal(0);
                            objHRTransactionEntryProperty.cl_date = Val.DBDate(DTPCLDate.Text);


                            objHRTransactionEntryProperty = HRTransactionEntry.Save(objHRTransactionEntryProperty);
                            //objHRTransactionEntryProperty = HRTransactionEntry.Save(objHRTransactionEntryProperty);

                            New_Union_Id = objHRTransactionEntryProperty.union_id;
                        }
                        //Thread.Sleep(100);
                        Count++;
                        IntCounter++;
                        SetControlPropertyValue(lblProgressCount, "Text", Count.ToString() + "" + "/" + "" + TotalCount.ToString() + " Completed....");
                    }
                    //Conn.Inter1.Commit();
                }
                catch (Exception ex)
                {
                    //m_DTab.Columns.Add("Total");
                    //m_DTab.AcceptChanges();
                    New_Union_Id = -1;
                    //Conn.Inter1.Rollback();
                    //Conn = null;
                    General.ShowErrors(ex.ToString());
                    return;
                }
            }
            catch (Exception ex)
            {
                //m_DTab.Columns.Add("Total");
                New_Union_Id = -1;
                //Conn.Inter1.Rollback();
                //Conn = null;
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
        private void backgroundWorker_HRTransactionEntry_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                PanelLoading.Visible = false;
                if (New_Union_Id > 0)
                {
                    Global.Confirm("HR Transaction Data Save Succesfully");
                    //if (txtYear.Text != "" && txtMonth.Text != "")
                    //{
                    //    txtBookNo.Text = objHRTransactionEntry.FindNewID_Search(Val.ToInt(txtYear.Text), Val.ToInt(txtMonth.Text)).ToString();

                    //    if (txtBookNo.Text == "0")
                    //    {
                    //        txtBookNo.Text = txtYear.Text + txtMonth.Text + "01";
                    //    }
                    //}
                    lblUnionID.Text = "0";
                    grdHRTransactionHistory.DataSource = null;
                    dgvHRTransactionHistory.Columns.Clear();
                    btnSearch.Enabled = true;

                    DTPCLDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                    DTPCLDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                    DTPCLDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                    DTPCLDate.Properties.CharacterCasing = CharacterCasing.Upper;
                    DTPCLDate.EditValue = DateTime.Now;
                    panelControl1.Enabled = true;
                    lueFactory.Enabled = true;
                    lueFactDepartment.Enabled = true;
                    lueManager.Enabled = true;
                    txtYear.Enabled = true;
                    txtMonth.Enabled = true;
                    txtBookNo.Enabled = false;
                }
                else
                {
                    Global.Confirm("Error In HR Transaction Data");
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }
        private void txtYear_TextChanged(object sender, EventArgs e)
        {
            //if (txtYear.Text != "" && txtMonth.Text != "")
            //{
            //    txtBookNo.Text = objHRTransactionEntry.FindNewID(Val.ToInt(txtYear.Text), Val.ToInt(txtMonth.Text)).ToString();

            //    if (txtBookNo.Text == "1")
            //    {
            //        txtBookNo.Text = txtYear.Text + txtMonth.Text + "01";
            //    }
            //}
            if (txtYear.Text != "" && txtMonth.Text != "")
            {
                txtBookNo.Text = objHRTransactionEntry.FindNewID_Search(Val.ToInt(txtYear.Text), Val.ToInt(txtMonth.Text)).ToString();

                if (txtBookNo.Text == "0")
                {
                    txtBookNo.Text = txtYear.Text + txtMonth.Text + "01";
                }
            }
        }

        #region GridEvents

        private void grdHRTransactionHistory_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    (grdHRTransactionHistory.FocusedView as ColumnView).FocusedRowHandle++;
                    e.Handled = true;
                }
            }
            catch { }
        }
        private void dgvHRTransactionHistory_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            GridView view = sender as GridView;

            if (view.FocusedColumn.FieldName == "1" || view.FocusedColumn.FieldName == "2" || view.FocusedColumn.FieldName == "3" || view.FocusedColumn.FieldName == "4" || view.FocusedColumn.FieldName == "5" || view.FocusedColumn.FieldName == "6" || view.FocusedColumn.FieldName == "7" || view.FocusedColumn.FieldName == "8" || view.FocusedColumn.FieldName == "9"
                || view.FocusedColumn.FieldName == "10" || view.FocusedColumn.FieldName == "11" || view.FocusedColumn.FieldName == "12" || view.FocusedColumn.FieldName == "13" || view.FocusedColumn.FieldName == "14" || view.FocusedColumn.FieldName == "15" || view.FocusedColumn.FieldName == "16" || view.FocusedColumn.FieldName == "17" || view.FocusedColumn.FieldName == "18"
                || view.FocusedColumn.FieldName == "19" || view.FocusedColumn.FieldName == "20" || view.FocusedColumn.FieldName == "21" || view.FocusedColumn.FieldName == "22" || view.FocusedColumn.FieldName == "23" || view.FocusedColumn.FieldName == "24" || view.FocusedColumn.FieldName == "25" || view.FocusedColumn.FieldName == "26" || view.FocusedColumn.FieldName == "27"
                || view.FocusedColumn.FieldName == "28" || view.FocusedColumn.FieldName == "29" || view.FocusedColumn.FieldName == "30" || view.FocusedColumn.FieldName == "31")
            {
                string brd = e.Value as string;
                if (Val.ToDecimal(brd).ToString().Contains(".") || (!(Val.ToDecimal(brd).ToString().Contains("0")) && !(Val.ToDecimal(brd).ToString().Contains("1")) && !(Val.ToDecimal(brd).ToString().Contains("2")) && !(Val.ToDecimal(brd).ToString().Contains("3")) && !(Val.ToDecimal(brd).ToString().Contains("4")) && !(Val.ToDecimal(brd).ToString().Contains("5")) && !(Val.ToDecimal(brd).ToString().Contains("6")) && !(Val.ToDecimal(brd).ToString().Contains("7")) && !(Val.ToDecimal(brd).ToString().Contains("8")) && !(Val.ToDecimal(brd).ToString().Contains("9"))))
                {
                    e.Valid = false;
                    e.ErrorText = "Input string was not in a correct format.";
                }
            }
        }
        private void dgvHRTransactionHistory_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                DataTable dtAmount = new DataTable();
                dtAmount = (DataTable)grdHRTransactionHistory.DataSource;

                Int64 total = 0;
                Int64 Total_Qty = 0;
                for (int i = 0; i <= dtAmount.Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= dtAmount.Columns.Count - 1; j++)
                    {
                        if (dtAmount.Columns[j].ToString().Contains("1") || dtAmount.Columns[j].ToString().Contains("2") || dtAmount.Columns[j].ToString().Contains("3") || dtAmount.Columns[j].ToString().Contains("4") || dtAmount.Columns[j].ToString().Contains("5") || dtAmount.Columns[j].ToString().Contains("6") || dtAmount.Columns[j].ToString().Contains("7") || dtAmount.Columns[j].ToString().Contains("8") || dtAmount.Columns[j].ToString().Contains("9"))
                        {
                            Total_Qty = Val.ToInt64(dtAmount.Rows[i][j]);
                            total += Total_Qty;
                        }
                        if (dtAmount.Columns[j].ColumnName.Contains("Total"))
                        {
                            dtAmount.Rows[i][j] = total;
                        }
                    }
                    total = 0;
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }

        #endregion

        #endregion

        #region Dynamic Control
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
                if ((Control)sender is CheckedComboBoxEdit)
                {
                    if (e.KeyChar == Convert.ToChar(Keys.Enter))
                    {
                        SendKeys.Send("{TAB}");
                    }
                }
                if ((Control)sender is ButtonEdit)
                {
                    if (e.KeyChar == Convert.ToChar(Keys.Enter))
                    {
                        SendKeys.Send("{TAB}");
                    }
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
                else if ((Control)sender is CheckedComboBoxEdit)
                {
                    ((CheckedComboBoxEdit)(Control)sender).ShowPopup();
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

        #region Functions
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (lueFactory.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Factory"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueFactory.Focus();
                    }
                }
                if (lueFactDepartment.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Department"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueFactDepartment.Focus();
                    }
                }
                if (lueManager.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Manager"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueManager.Focus();
                    }
                }
                if (txtYear.Text == "")
                {
                    lstError.Add(new ListError(13, "Year"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtYear.Focus();
                    }
                }
                if (txtMonth.Text == "")
                {
                    lstError.Add(new ListError(13, "Month"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtMonth.Focus();
                    }
                }
                if (txtBookNo.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Book No"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtBookNo.Focus();
                    }
                }
                if (txtBookNo.Text == "0")
                {
                    lstError.Add(new ListError(15, "Book No"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtBookNo.Focus();
                    }
                }


            }
            catch (Exception ex)
            {
                lstError.Add(new ListError(ex));
            }
            return (!(BLL.General.ShowErrors(lstError)));
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
                            dgvHRTransactionHistory.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvHRTransactionHistory.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvHRTransactionHistory.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvHRTransactionHistory.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvHRTransactionHistory.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvHRTransactionHistory.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvHRTransactionHistory.ExportToCsv(Filepath);
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
        private bool ClearDetails()
        {
            bool blnReturn = true;
            try
            {
                txtYear.Text = "0";
                txtMonth.Text = "0";
                txtBookNo.Text = "0";

                if ((txtYear.Text != "" && txtMonth.Text != "") && (txtYear.Text != "0" && txtMonth.Text != "0"))
                {
                    txtBookNo.Text = objHRTransactionEntry.FindNewID_Search(Val.ToInt(txtYear.Text), Val.ToInt(txtMonth.Text)).ToString();

                    if (txtBookNo.Text == "0")
                    {
                        txtBookNo.Text = txtYear.Text + txtMonth.Text + "01";
                    }
                }
                lblUnionID.Text = "0";
                grdHRTransactionHistory.DataSource = null;
                dgvHRTransactionHistory.Columns.Clear();
                btnSearch.Enabled = true;

                lueFactory.Properties.DataSource = null;
                lueFactDepartment.Properties.DataSource = null;
                lueManager.Properties.DataSource = null;
                lueFactory.EditValue = null;
                lueFactDepartment.EditValue = null;
                lueManager.EditValue = null;
                Global.LOOKUPHRFactory(lueFactory);
                Global.LOOKUPHRFactoryDept(lueFactDepartment);

                DTPCLDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                DTPCLDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                DTPCLDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                DTPCLDate.Properties.CharacterCasing = CharacterCasing.Upper;
                DTPCLDate.EditValue = DateTime.Now;
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

        private void LueEmpName_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmHREmployeeMaster frmCnt = new FrmHREmployeeMaster();
                frmCnt.ShowDialog();
            }
        }
        private void dgvHRTransactionHistory_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {

            if (e.SummaryProcess == CustomSummaryProcess.Start)
            {
                counter = 0;
            }
            else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
            {
                //return;

                if (((e.FieldValue.ToString() != "" && e.FieldValue.ToString() != null) && e.FieldValue.ToString() != "0"))
                    counter = counter + 1;
            }
            else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
            {
                if (((GridSummaryItem)e.Item).FieldName.CompareTo("1") == 0 || ((GridSummaryItem)e.Item).FieldName.CompareTo("2") == 0 || ((GridSummaryItem)e.Item).FieldName.CompareTo("3") == 0 || ((GridSummaryItem)e.Item).FieldName.CompareTo("4") == 0 || ((GridSummaryItem)e.Item).FieldName.CompareTo("5") == 0 || ((GridSummaryItem)e.Item).FieldName.CompareTo("6") == 0 ||
                    ((GridSummaryItem)e.Item).FieldName.CompareTo("7") == 0 || ((GridSummaryItem)e.Item).FieldName.CompareTo("8") == 0 || ((GridSummaryItem)e.Item).FieldName.CompareTo("9") == 0 || ((GridSummaryItem)e.Item).FieldName.CompareTo("10") == 0 || ((GridSummaryItem)e.Item).FieldName.CompareTo("11") == 0 || ((GridSummaryItem)e.Item).FieldName.CompareTo("12") == 0 ||
                    ((GridSummaryItem)e.Item).FieldName.CompareTo("13") == 0 || ((GridSummaryItem)e.Item).FieldName.CompareTo("14") == 0 || ((GridSummaryItem)e.Item).FieldName.CompareTo("15") == 0 || ((GridSummaryItem)e.Item).FieldName.CompareTo("16") == 0 || ((GridSummaryItem)e.Item).FieldName.CompareTo("17") == 0 || ((GridSummaryItem)e.Item).FieldName.CompareTo("18") == 0 ||
                    ((GridSummaryItem)e.Item).FieldName.CompareTo("19") == 0 || ((GridSummaryItem)e.Item).FieldName.CompareTo("20") == 0 || ((GridSummaryItem)e.Item).FieldName.CompareTo("21") == 0 || ((GridSummaryItem)e.Item).FieldName.CompareTo("22") == 0 || ((GridSummaryItem)e.Item).FieldName.CompareTo("23") == 0 || ((GridSummaryItem)e.Item).FieldName.CompareTo("24") == 0 ||
                    ((GridSummaryItem)e.Item).FieldName.CompareTo("25") == 0 || ((GridSummaryItem)e.Item).FieldName.CompareTo("26") == 0 || ((GridSummaryItem)e.Item).FieldName.CompareTo("27") == 0 || ((GridSummaryItem)e.Item).FieldName.CompareTo("28") == 0 || ((GridSummaryItem)e.Item).FieldName.CompareTo("29") == 0 || ((GridSummaryItem)e.Item).FieldName.CompareTo("30") == 0 ||
                    ((GridSummaryItem)e.Item).FieldName.CompareTo("31") == 0 || (((GridSummaryItem)e.Item).FieldName.CompareTo("sr_no") == 0 || ((GridSummaryItem)e.Item).FieldName.CompareTo("sr_no").ToString() == ""))
                {
                    if (counter > 0)
                    {
                        e.TotalValue = String.Format("{0}", counter);
                    }
                    else
                    {
                        e.TotalValue = 0;
                    }
                }
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            grdHRTransactionHistory.DataSource = null;
            txtBookNo.Enabled = true;
        }

        private void backgroundWorker_HRTransactionEntryUpdate_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

            HRTransactionEntry HRTransactionEntry = new HRTransactionEntry();
            HRTransactionEntryProperty objHRTransactionEntryProperty = new HRTransactionEntryProperty();

            DataTable m_DTab = new DataTable();
            try
            {
                m_DTab = (DataTable)grdHRTransactionHistory.DataSource;

                m_DTab.AcceptChanges();
                int IntCounter = 0;
                int Count = 0;
                int TotalCount = m_DTab.Rows.Count;

                for (int i = 0; i <= m_DTab.Rows.Count - 1; i++)
                {
                    for (int j = 3; j <= m_DTab.Columns.Count - 2; j++)
                    {
                        objHRTransactionEntryProperty.employee_id = Val.ToInt64(m_DTab.Rows[i]["employee_id"]);
                        objHRTransactionEntryProperty.sr_no = Val.ToInt64(m_DTab.Rows[i]["sr_no"]);
                        objHRTransactionEntryProperty.factory_id = Val.ToInt64(lueFactory.EditValue);
                        objHRTransactionEntryProperty.fact_department_id = Val.ToInt64(lueFactDepartment.EditValue);
                        objHRTransactionEntryProperty.manager_id = Val.ToInt64(lueManager.EditValue);
                        objHRTransactionEntryProperty.year = Val.ToInt(txtYear.Text);
                        objHRTransactionEntryProperty.month = Val.ToInt(txtMonth.Text);
                        objHRTransactionEntryProperty.book_no = Val.ToInt64(txtBookNo.Text);
                        objHRTransactionEntryProperty.total_qty = Val.ToInt32(m_DTab.Rows[i][j]);

                        DateTime date = new DateTime(objHRTransactionEntryProperty.year, objHRTransactionEntryProperty.month, Val.ToInt32(m_DTab.Columns[j]));

                        objHRTransactionEntryProperty.transaction_date = Val.DBDate(date.ToString());


                        IntRes = HRTransactionEntry.Update(objHRTransactionEntryProperty);

                        if (IntRes == 0)
                        {
                            Global.Message("New Employee Found...Please Save Data Then After Update");
                            return;
                        }
                    }
                    // Thread.Sleep(100);
                    Count++;
                    IntCounter++;
                    SetControlPropertyValue(lblProgressCount, "Text", Count.ToString() + "" + "/" + "" + TotalCount.ToString() + " Completed....");
                }
            }
            catch (Exception ex)
            {
                IntRes = -1;
                General.ShowErrors(ex.ToString());
                return;
            }
        }

        private void backgroundWorker_HRTransactionEntryUpdate_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                PanelLoading.Visible = false;
                if (IntRes > 0)
                {
                    Global.Confirm("HR Transaction Data Updates Succesfully");

                    lblUnionID.Text = "0";
                    grdHRTransactionHistory.DataSource = null;
                    dgvHRTransactionHistory.Columns.Clear();
                    btnSearch.Enabled = true;

                    DTPCLDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                    DTPCLDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                    DTPCLDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                    DTPCLDate.Properties.CharacterCasing = CharacterCasing.Upper;
                    DTPCLDate.EditValue = DateTime.Now;
                    panelControl1.Enabled = true;
                    lueFactory.Enabled = true;
                    lueFactDepartment.Enabled = true;
                    lueManager.Enabled = true;
                    txtYear.Enabled = true;
                    txtMonth.Enabled = true;
                    txtBookNo.Enabled = false;
                }
                else
                {
                    Global.Confirm("Error In HR Transaction Data");
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
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
                btnUpdate.Enabled = false;
                if (!ValidateDetails())
                {
                    btnUpdate.Enabled = true;
                    return;
                }

                if (txtBookNo.Text.ToString() == "")
                {
                    Global.Message("Please Generate Book No..");
                    return;
                }

                DialogResult result = MessageBox.Show("Do you want to Update HR Transaction data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result != DialogResult.Yes)
                {
                    btnUpdate.Enabled = true;
                    return;
                }

                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                PanelLoading.Visible = true;
                backgroundWorker_HRTransactionEntryUpdate.RunWorkerAsync();

                btnUpdate.Enabled = true;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
    }
}
