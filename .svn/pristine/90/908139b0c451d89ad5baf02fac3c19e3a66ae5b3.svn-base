using BLL;
using BLL.FunctionClasses.Master.HR;
using BLL.FunctionClasses.Transaction;
using BLL.PropertyClasses.Master.HR;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DREP.Master.HR
{
    public partial class FrmHREmployeeMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        Control _NextEnteredControl;
        private List<Control> _tabControls;

        BLL.FormEvents objBOFormEvents = new BLL.FormEvents();
        BLL.Validation Val = new BLL.Validation();
        BLL.FormPer ObjPer = new BLL.FormPer();
        HREmployeeMaster objHREmp = new HREmployeeMaster();
        DataTable DtControlSettings;
        #endregion

        #region Constructor
        public FrmHREmployeeMaster()
        {
            InitializeComponent();

            _NextEnteredControl = new Control();
            _tabControls = new List<Control>();
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
            //AddKeyPressListener(this);
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
            objBOFormEvents.ObjToDispose.Add(objHREmp);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
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

        #endregion

        #region Events

        private void FrmEmployeeMaster_Load(object sender, EventArgs e)
        {
            try
            {
                GetData();
                btnClear_Click(btnClear, null);
                //txtSrNo.Focus();
                Global.LOOKUPHRManager(lueManager);
                Global.LOOKUPHRFactory(lueFactory);
                Global.LOOKUPHRFactoryDept(lueFactDepartment);
                txtSrNo.Focus();
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            ObjPer.FormName = this.Name.ToUpper();
            ObjPer.SetFormPer();
            if (ObjPer.AllowUpdate == false || ObjPer.AllowInsert == false)
            {
                Global.Message(BLL.GlobalDec.gStrPermissionInsUpdMsg);
                return;
            }
            btnSave.Enabled = false;

            if (SaveDetails())
            {
                GetData();
                btnClear_Click(sender, e);
            }

            btnSave.Enabled = true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                lblMode.Tag = 0;
                lblMode.Text = "Add Mode";
                txtEmpName.Text = "";
                txtEmployeeCode.Text = "";
                //lueManager.EditValue = System.DBNull.Value;
                //lueFactory.EditValue = System.DBNull.Value;
                //lueFactDepartment.EditValue = System.DBNull.Value;

                txtRefBy.Text = "";
                txtMobile.Text = "";
                txtAadharNo.Text = "";
                txtRefAdharNo.Text = "";

                dtpDOB.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpDOB.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpDOB.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpDOB.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpDOB.EditValue = DateTime.Now;

                dtpJoiningDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpJoiningDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpJoiningDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpJoiningDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpJoiningDate.EditValue = DateTime.Now;

                dtpLeaveDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpLeaveDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpLeaveDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpLeaveDate.Properties.CharacterCasing = CharacterCasing.Upper;

                dtpDOB.EditValue = null;
                dtpJoiningDate.EditValue = null;
                dtpLeaveDate.EditValue = null;
                chkActive.Checked = true;

                txtSrNo.Text = "0";
                txtSrNo.Focus();
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region GridEvents

        private void dgvEmployeeMaster_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvEmployeeMaster.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt64(Drow["employee_id"]);
                        txtSrNo.Text = Val.ToString(Drow["sr_no"]);
                        txtEmpName.Text = Val.ToString(Drow["employee_name"]);
                        txtEmployeeCode.Text = Val.ToString(Drow["employee_code"]);

                        lueFactory.EditValue = Val.ToInt64(Drow["factory_id"]);
                        lueFactDepartment.EditValue = Val.ToInt64(Drow["fact_department_id"]);
                        lueManager.EditValue = Val.ToInt64(Drow["manager_id"]);
                        txtAadharNo.Text = Val.ToString(Drow["adharcard_no"]);
                        txtMobile.Text = Val.ToString(Drow["mobile_no"]);
                        dtpDOB.EditValue = Val.DBDate(Drow["dob"].ToString());
                        dtpJoiningDate.EditValue = Val.DBDate(Drow["joining_date"].ToString());
                        txtRefBy.Text = Val.ToString(Drow["refrence_emp_name"]);
                        txtRefAdharNo.Text = Val.ToString(Drow["refrence_adharcard_no"]);
                        chkActive.Checked = Val.ToBoolean(Drow["active"]);
                        dtpLeaveDate.EditValue = Val.DBDate(Drow["leave_date"].ToString());
                        txtSrNo.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void dgvEmployeeMaster_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                string StrCategory = Val.ToString(dgvEmployeeMaster.GetRowCellDisplayText(e.RowHandle, dgvEmployeeMaster.Columns["active"]));

                if (StrCategory == "Unchecked")
                {
                    e.Appearance.BackColor = BLL.GlobalDec.ABColor;
                    e.Appearance.BackColor2 = BLL.GlobalDec.ABColor2;
                    //e.Appearance.BackColor2 = Color.White;
                }
                else if (StrCategory == "Checked")
                {
                    e.Appearance.BackColor = Color.Transparent;
                    e.Appearance.BackColor2 = Color.Transparent;
                }
            }
        }
        #endregion

        #endregion

        #region Functions

        private bool SaveDetails()
        {
            bool blnReturn = true;
            HREmployee_MasterProperty HREmpMasterProperty = new HREmployee_MasterProperty();
            HREmployeeMaster objHREmp = new HREmployeeMaster();

            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }

                HREmpMasterProperty.employee_id = Val.ToInt64(lblMode.Tag);
                HREmpMasterProperty.employee_code = Val.ToInt64(txtEmployeeCode.Text);
                HREmpMasterProperty.sr_no = Val.ToInt64(txtSrNo.Text);
                HREmpMasterProperty.employee_name = Val.ToString(txtEmpName.Text).ToUpper();
                HREmpMasterProperty.manager_id = Val.ToInt64(lueManager.EditValue);
                HREmpMasterProperty.factory_id = Val.ToInt64(lueFactory.EditValue);
                HREmpMasterProperty.fact_department_id = Val.ToInt64(lueFactDepartment.EditValue);
                HREmpMasterProperty.adharcard_no = Val.ToString(txtAadharNo.Text);
                HREmpMasterProperty.mobile_no = Val.ToString(txtMobile.Text);
                HREmpMasterProperty.reference_by = Val.ToString(txtRefBy.Text);
                HREmpMasterProperty.reference_adhar_no = Val.ToInt64(txtRefAdharNo.Text);
                HREmpMasterProperty.DOB = Val.DBDate(dtpDOB.Text);
                HREmpMasterProperty.joining_date = Val.DBDate(dtpJoiningDate.Text);
                HREmpMasterProperty.leave_date = Val.DBDate(dtpLeaveDate.Text);
                HREmpMasterProperty.active = Val.ToBoolean(chkActive.Checked);

                int IntRes = objHREmp.Save(HREmpMasterProperty);
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Save HR Employee Details");
                    txtEmpName.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("HR Employee Details Data Save Successfully");
                    }
                    else
                    {
                        Global.Confirm("HR Employee Details Data Update Successfully");
                    }
                }

            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                blnReturn = false;
            }
            finally
            {
                HREmpMasterProperty = null;
            }

            return blnReturn;
        }

        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (txtSrNo.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Sr No"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtSrNo.Focus();
                    }
                }
                if (txtEmpName.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Employee Name"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtEmpName.Focus();
                    }
                }
                //if (!objHREmp.ISExistsCode(txtEmployeeCode.Text, Val.ToInt64(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
                //{
                //    lstError.Add(new ListError(23, "Employee Code"));
                //    if (!blnFocus)
                //    {
                //        blnFocus = true;
                //        txtEmployeeCode.Focus();
                //    }
                //}
                if (lueManager.Text == "")
                {
                    lstError.Add(new ListError(13, "Manager"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueManager.Focus();
                    }
                }
                if (lueFactory.Text == "")
                {
                    lstError.Add(new ListError(13, "Factory"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueFactory.Focus();
                    }
                }
                if (lueFactDepartment.Text == "")
                {
                    lstError.Add(new ListError(13, "Factory Department"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueFactDepartment.Focus();
                    }
                }
                if (txtRefAdharNo.Text != "" && txtRefAdharNo.Text != "0")
                {
                    if (txtRefAdharNo.Text.Length != 12)
                    {
                        lstError.Add(new ListError(5, "Please Enter Only 12 Digit Refrence Adhar Card No"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtRefAdharNo.Focus();
                        }
                    }
                }
                //if (txtAadharNo.Text != "")
                //{
                //    if (txtAadharNo.Text.Length != 12)
                //    {
                //        lstError.Add(new ListError(5, "Please Enter Only 12 Digit Adhar Card No"));
                //        if (!blnFocus)
                //        {
                //            blnFocus = true;
                //            txtAadharNo.Focus();
                //        }
                //    }
                //}
                //if (txtAadharNo.Text == "")
                //{
                //    lstError.Add(new ListError(12, "Adhar Card No"));
                //    if (!blnFocus)
                //    {
                //        blnFocus = true;
                //        txtAadharNo.Focus();
                //    }
                //}
            }
            catch (Exception ex)
            {
                lstError.Add(new ListError(ex));
            }
            return (!(BLL.General.ShowErrors(lstError)));

        }

        public void GetData()
        {
            List<ListError> lstError = new List<ListError>();
            try
            {
                DataTable DTab = objHREmp.GetData();
                grdEmployeeMaster.DataSource = DTab;
                dgvEmployeeMaster.BestFitColumns();
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
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
                            dgvEmployeeMaster.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvEmployeeMaster.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvEmployeeMaster.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvEmployeeMaster.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvEmployeeMaster.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvEmployeeMaster.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvEmployeeMaster.ExportToCsv(Filepath);
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

        private void txtAadharNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtMobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtRefAdharNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            //{
            //    e.Handled = true;
            //}
        }

        private void lueFactory_EditValueChanged(object sender, EventArgs e)
        {
            if (Val.ToString(lblMode.Text) == "Add Mode")
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
        }

        private void lueFactDepartment_EditValueChanged(object sender, EventArgs e)
        {
            if (Val.ToString(lblMode.Text) == "Add Mode")
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
        }

        private void lueFactory_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmHRFactoryMaster frmCnt = new FrmHRFactoryMaster();
                frmCnt.ShowDialog();
                Global.LOOKUPHRFactory(lueFactory);
            }
        }

        private void lueManager_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmHRManagerMaster frmCnt = new FrmHRManagerMaster();
                frmCnt.ShowDialog();

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
                //Global.LOOKUPHRManager(lueManager);
            }
        }
    }
}
