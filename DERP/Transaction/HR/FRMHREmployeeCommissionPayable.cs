using BLL;
using BLL.FunctionClasses.Master.HR;
using BLL.PropertyClasses.Master.HR;
using DERP.Class;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DREP.Transaction
{
    public partial class FRMHREmployeeCommissionPayable : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        FormEvents objBOFormEvents = new FormEvents();
        Validation Val = new Validation();
        BLL.FormPer ObjPer = new BLL.FormPer();
        HREmployeeCommissionPayable objHREmployeeCommissionPayable = new HREmployeeCommissionPayable();
        DataTable m_DTab;
        int IntRes = 0;
        DataTable DTab_Month = new DataTable();
        #endregion

        #region Constructor
        public FRMHREmployeeCommissionPayable()
        {
            InitializeComponent();
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
            ObjPer.SetFormPer();
            if (ObjPer.AllowUpdate == false || ObjPer.AllowInsert == false)
            {
                Global.Message(BLL.GlobalDec.gStrPermissionInsUpdMsg);
                return;
            }

            btnSave.Enabled = false;

            //if (!ValidateDetails())
            //{
            //    btnSave.Enabled = true;
            //    return;
            //}

            DialogResult result = MessageBox.Show("Do you want to save data?", "Confirmation", MessageBoxButtons.YesNoCancel);
            if (result != DialogResult.Yes)
            {
                btnSave.Enabled = true;
                return;
            }

            DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
            backgroundWorkerHREmployeeCommissionPayable.RunWorkerAsync();

            btnSave.Enabled = true;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            grdHREmployeeCommissionPayable.DataSource = null;
            CmbPaidUnpaid.SelectedIndex = 0;
            ChkPaidUnpaid.Checked = false;
            //CmbType.SelectedIndex = 0;
            txtDays.Text = "0";
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            Global.Export("xlsx", dgvHREmployeeCommissionPayable);
        }
        private void backgroundWorkerHREmployeeCommissionPayable_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.Default;

                //Conn = new BeginTranConnection(true, false);

                HREmployeeCommissionPayableProperty HREmployeeCommissionPayableProperty = new HREmployeeCommissionPayableProperty();
                IntRes = 0;
                try
                {
                    m_DTab = (DataTable)grdHREmployeeCommissionPayable.DataSource;
                    m_DTab.AcceptChanges();
                    DataRow[] resut = m_DTab.Select("status in('PAID','UNPAID')");

                    if (resut.Length > 0)
                    {
                        DataTable DTab_Emp = m_DTab.Select("status in('PAID','UNPAID')").CopyToDataTable();

                        foreach (DataRow drw in DTab_Emp.Rows)
                        {
                            HREmployeeCommissionPayableProperty.factory_id = Val.ToInt64(drw["factory_id"]);
                            HREmployeeCommissionPayableProperty.fact_department_id = Val.ToInt64(drw["fact_department_id"]);
                            HREmployeeCommissionPayableProperty.manager_id = Val.ToInt64(drw["manager_id"]);
                            HREmployeeCommissionPayableProperty.employee_id = Val.ToInt64(drw["employee_id"]);
                            HREmployeeCommissionPayableProperty.new_employee_amount = Val.ToDecimal(drw["new_employee_amount"]);
                            HREmployeeCommissionPayableProperty.refrence_employee_amount = Val.ToDecimal(drw["ref_employee_amount"]);
                            HREmployeeCommissionPayableProperty.status = Val.ToString(drw["status"]);
                            HREmployeeCommissionPayableProperty.paid_date = Val.DBDate(drw["paid_date"].ToString());
                            HREmployeeCommissionPayableProperty.remarks = Val.ToString(drw["remarks"]);

                            IntRes = objHREmployeeCommissionPayable.Save(HREmployeeCommissionPayableProperty);
                        }
                    }
                    else
                    {
                        Global.Message("Employee Data not any Paid Selection");
                        return;
                    }
                    //Conn.Inter1.Commit();
                }
                catch (Exception ex)
                {
                    IntRes = -1;
                    //Conn.Inter1.Rollback();
                    //Conn = null;
                    General.ShowErrors(ex.ToString());
                    return;
                }
                finally
                {
                    HREmployeeCommissionPayableProperty = null;
                }
            }
            catch (Exception ex)
            {
                IntRes = -1;
                //Conn.Inter1.Rollback();
                //Conn = null;
                Global.Message(ex.ToString());
                if (ex.InnerException != null)
                {
                    Global.Message(ex.InnerException.ToString());
                }
            }
        }
        private void backgroundWorkerHREmployeeCommissionPayable_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (IntRes > 0)
                {
                    Global.Confirm("HR Employee Payable Commission Save Succesfully");
                    btnClear_Click(null, null);
                    BtnGenerateReport_Click(null, null);
                    //GetData();
                }
                else
                {
                    Global.Confirm("Error In Save HR Employee Payable Commission");
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }
        private void FRMHREmployeeCommissionPayable_Load(object sender, EventArgs e)
        {
            Global.LOOKUPHRFactory(lueFactory);
            Global.LOOKUPHRFactoryDept(lueFactDepartment);
            Global.LOOKUPStatusRep(RepStatus);

            DTPFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
            DTPFromDate.EditValue = DateTime.Now;

            DTPToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPToDate.Properties.CharacterCasing = CharacterCasing.Upper;
            DTPToDate.EditValue = DateTime.Now;

            DTab_Month = new DataTable();
            DTab_Month.Columns.Add("MONTH", typeof(string));
            DataRow Dr = DTab_Month.NewRow();
            Dr["MONTH"] = "JAN";
            DTab_Month.Rows.Add(Dr);
            Dr = DTab_Month.NewRow();
            Dr["MONTH"] = "FEB";
            DTab_Month.Rows.Add(Dr);
            Dr = DTab_Month.NewRow();
            Dr["MONTH"] = "MAR";
            DTab_Month.Rows.Add(Dr);
            Dr = DTab_Month.NewRow();
            Dr["MONTH"] = "APR";
            DTab_Month.Rows.Add(Dr);
            Dr = DTab_Month.NewRow();
            Dr["MONTH"] = "MAY";
            DTab_Month.Rows.Add(Dr);
            Dr = DTab_Month.NewRow();
            Dr["MONTH"] = "JUN";
            DTab_Month.Rows.Add(Dr);
            Dr = DTab_Month.NewRow();
            Dr["MONTH"] = "JUL";
            DTab_Month.Rows.Add(Dr);
            Dr = DTab_Month.NewRow();
            Dr["MONTH"] = "AUG";
            DTab_Month.Rows.Add(Dr);
            Dr = DTab_Month.NewRow();
            Dr["MONTH"] = "SEP";
            DTab_Month.Rows.Add(Dr);
            Dr = DTab_Month.NewRow();
            Dr["MONTH"] = "OCT";
            DTab_Month.Rows.Add(Dr);
            Dr = DTab_Month.NewRow();
            Dr["MONTH"] = "NOV";
            DTab_Month.Rows.Add(Dr);
            Dr = DTab_Month.NewRow();
            Dr["MONTH"] = "DEC";
            DTab_Month.Rows.Add(Dr);
            DTPFromDate.Focus();
        }
        private void BtnReset_Click(object sender, EventArgs e)
        {
            DTPFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
            DTPFromDate.EditValue = DateTime.Now;

            DTPToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPToDate.Properties.CharacterCasing = CharacterCasing.Upper;
            DTPToDate.EditValue = DateTime.Now;

            DTPFromDate.Focus();
        }
        private void lueFactory_EditValueChanged(object sender, EventArgs e)
        {
            if (Val.ToString(lueFactory.Text) != "" && Val.ToString(lueFactDepartment.Text) != "")
            {
                HREmployee_MasterProperty HREmpMasterProperty = new HREmployee_MasterProperty();
                HREmployeeMaster objHREmp = new HREmployeeMaster();
                HREmpMasterProperty.fact_department_id = Val.ToInt64(lueFactDepartment.EditValue);
                HREmpMasterProperty.factory_id = Val.ToInt64(lueFactory.EditValue);

                DataTable Dtab_Fact_Dept = objHREmp.Fact_Dept_Wise_ManagerGetData(HREmpMasterProperty);

                if (Dtab_Fact_Dept.Rows.Count > 0)
                {
                    lueManager.EditValue = null;
                    lueManager.Properties.DataSource = Dtab_Fact_Dept;
                    lueManager.Properties.ValueMember = "manager_id";
                    lueManager.Properties.DisplayMember = "manager_name";
                }
                else
                {
                    lueManager.EditValue = null;
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

                DataTable Dtab_Fact_Dept = objHREmp.Fact_Dept_Wise_ManagerGetData(HREmpMasterProperty);

                if (Dtab_Fact_Dept.Rows.Count > 0)
                {
                    lueManager.EditValue = null;
                    lueManager.Properties.DataSource = Dtab_Fact_Dept;
                    lueManager.Properties.ValueMember = "manager_id";
                    lueManager.Properties.DisplayMember = "manager_name";
                }
                else
                {
                    lueManager.EditValue = null;
                }
            }
        }
        private void BtnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!ValidateDetails())
                //{
                //    return;
                //}

                var result = DateTime.Compare(Convert.ToDateTime(DTPFromDate.Text), Convert.ToDateTime(DTPToDate.Text.ToString()));
                if (result > 0)
                {
                    Global.Message(" Transaction From Date Not Be Greater Than Transaction To Date");
                    return;
                }

                if (Val.ToString(CmbType.Text) == "")
                {
                    Global.Message("Type Are Required...");
                    CmbType.Focus();
                    return;
                }

                PanelLoading.Visible = true;
                HREmployeeCommissionPayableProperty HREmployeeCommissionPayableProperty = new HREmployeeCommissionPayableProperty();

                HREmployeeCommissionPayableProperty.manager_id = Val.ToInt64(lueManager.EditValue);
                HREmployeeCommissionPayableProperty.factory_id = Val.ToInt64(lueFactory.EditValue);
                HREmployeeCommissionPayableProperty.fact_department_id = Val.ToInt64(lueFactDepartment.EditValue);
                HREmployeeCommissionPayableProperty.from_date = Val.DBDate(DTPFromDate.Text);
                HREmployeeCommissionPayableProperty.to_date = Val.DBDate(DTPToDate.Text);
                HREmployeeCommissionPayableProperty.days = Val.ToInt(txtDays.Text);
                HREmployeeCommissionPayableProperty.employee_type = Val.ToString(CmbType.Text);

                DataTable FillHREmpData = objHREmployeeCommissionPayable.HR_Emp_CommissionPayableGetData(HREmployeeCommissionPayableProperty);

                if (Val.DBDate(DTPFromDate.Text) != "" && Val.DBDate(DTPToDate.Text) != "")
                {
                    String sFromDate = Val.DBDate(DTPFromDate.Text);
                    DateTime Fromdatevalue = (Convert.ToDateTime(sFromDate.ToString()));
                    int Fromdy = Fromdatevalue.Day;
                    int Frommn = Fromdatevalue.Month;
                    int Fromyy = Fromdatevalue.Year;

                    String sToDate = Val.DBDate(DTPToDate.Text);
                    DateTime Todatevalue = (Convert.ToDateTime(sToDate.ToString()));
                    int Tody = Todatevalue.Day;
                    int Tomn = Todatevalue.Month;
                    int Toyy = Todatevalue.Year;

                    var start = new DateTime(Fromyy, Frommn, Fromdy);
                    var end = new DateTime(Toyy, Tomn, Tody);

                    // set end-date to end of month
                    end = new DateTime(end.Year, end.Month, DateTime.DaysInMonth(end.Year, end.Month));

                    var diff = Enumerable.Range(0, Int32.MaxValue)
                                         .Select(x => start.AddMonths(x))
                                         .TakeWhile(x => x <= end)
                                         .Select(x => x.ToString("MMM").ToUpper()).Distinct();

                    DataTable DTab_Diff = new DataTable();
                    DTab_Diff.Columns.Add("MONTH", typeof(string));

                    foreach (string Word in diff)
                    {
                        DTab_Diff.Rows.Add(Word);
                    }
                    grdHREmployeeCommissionPayable.DataSource = null;
                    grdHREmployeeCommissionPayable.RefreshDataSource();
                    grdHREmployeeCommissionPayable.Refresh();

                    DataTable dt3 = DTab_Month.Clone();
                    //find index numbers in dt1 not present in dt2
                    var idsExceptDt2 = DTab_Month.AsEnumerable().Select(r => (string)r.ItemArray[0])
                        .Except(DTab_Diff.AsEnumerable().Select(r => (string)r.ItemArray[0])).ToArray();

                    foreach (DataRow r in DTab_Month.Rows)
                    {
                        //does the row index number match any of the non-duplicate index numbers?
                        bool isMatched = idsExceptDt2.Any(id => (string)r.ItemArray[0] == id);
                        if (isMatched)
                        {
                            dt3.Rows.Add(r.ItemArray);
                        }
                        dgvHREmployeeCommissionPayable.Columns[(string)r.ItemArray[0]].Visible = true;
                        dgvHREmployeeCommissionPayable.Columns[(string)r.ItemArray[0] + 1].Visible = true;
                    }

                    if (dt3.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt3.Rows.Count; i++)
                        {
                            dgvHREmployeeCommissionPayable.Columns[dt3.Rows[i]["MONTH"].ToString()].Visible = false;
                            dgvHREmployeeCommissionPayable.Columns[dt3.Rows[i]["MONTH"].ToString() + 1].Visible = false;
                        }
                    }
                }

                grdHREmployeeCommissionPayable.DataSource = FillHREmpData;
                dgvHREmployeeCommissionPayable.OptionsView.ColumnAutoWidth = false;
                dgvHREmployeeCommissionPayable.OptionsView.ShowFooter = true;
                PanelLoading.Visible = false;
            }
            catch (Exception ex)
            {
                PanelLoading.Visible = false;
                General.ShowErrors(ex.ToString());
            }
        }

        #region Grid Event

        #endregion

        #endregion

        #region Function

        #endregion

        #region Export Grid

        private void MNExportExcel_Click(object sender, EventArgs e)
        {
            Export("xlsx", "Export to Excel", "Excel files 97-2003 (Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*");
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
                            dgvHREmployeeCommissionPayable.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvHREmployeeCommissionPayable.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvHREmployeeCommissionPayable.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvHREmployeeCommissionPayable.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvHREmployeeCommissionPayable.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvHREmployeeCommissionPayable.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvHREmployeeCommissionPayable.ExportToCsv(Filepath);
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

        private void ChkPaidUnpaid_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkPaidUnpaid.Checked == true)
            {
                if (CmbPaidUnpaid.SelectedIndex == 0)
                {
                    for (int i = 0; i < dgvHREmployeeCommissionPayable.RowCount; i++)
                    {
                        dgvHREmployeeCommissionPayable.SetRowCellValue(i, "status", "");
                    }
                }
                else if (CmbPaidUnpaid.SelectedIndex == 1)
                {
                    for (int i = 0; i < dgvHREmployeeCommissionPayable.RowCount; i++)
                    {
                        dgvHREmployeeCommissionPayable.SetRowCellValue(i, "status", "PAID");
                    }
                }
                else if (CmbPaidUnpaid.SelectedIndex == 2)
                {
                    for (int i = 0; i < dgvHREmployeeCommissionPayable.RowCount; i++)
                    {
                        dgvHREmployeeCommissionPayable.SetRowCellValue(i, "status", "UNPAID");
                    }
                }
            }
        }

        private void CmbReport_EditValueChanged(object sender, EventArgs e)
        {
            ChkPaidUnpaid.Checked = false;
        }
    }
}
