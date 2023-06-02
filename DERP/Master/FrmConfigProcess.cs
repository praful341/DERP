using BLL;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Master.MFG;
using BLL.PropertyClasses.Master;
using DERP.Class;
using System;
using System.Collections.Generic;
using System.Data;

namespace DERP.Master
{
    public partial class FrmConfigProcess : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        FormEvents objBOFormEvents = new FormEvents();
        Validation Val;
        BLL.FormPer ObjPer;

        ConfigProcessMaster objConfigProcess;
        ProcessMaster objProcess;
        MfgSubProcessMaster objSubProcess;

        DataTable m_dtbProcesstype;
        DataTable m_dtbSubProcess;

        string m_StrGetSubProcess;

        #endregion

        #region Constructor
        public FrmConfigProcess()
        {
            InitializeComponent();

            objBOFormEvents = new FormEvents();
            Val = new Validation();
            ObjPer = new BLL.FormPer();

            objConfigProcess = new ConfigProcessMaster();
            objProcess = new ProcessMaster();
            objSubProcess = new MfgSubProcessMaster();

            m_dtbProcesstype = new DataTable();
            m_dtbSubProcess = new DataTable();

            m_StrGetSubProcess = "";
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
            chkActive.Checked = true;
        }
        private void AttachFormEvents()
        {
            objBOFormEvents.CurForm = this;
            objBOFormEvents.FormKeyPress = true;
            objBOFormEvents.FormKeyDown = true;
            objBOFormEvents.FormResize = true;
            objBOFormEvents.FormClosing = true;
            objBOFormEvents.ObjToDispose.Add(objConfigProcess);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion

        #region Events
        private void FrmConfigProcess_Load(object sender, EventArgs e)
        {
            try
            {
                Global.LOOKUPCompany_New(lueCompany);
                Global.LOOKUPBranch_New(lueBranch);
                Global.LOOKUPLocation_New(lueLocation);
                Global.LOOKUPDepartment_New(lueDepartment);

                lueCompany.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.company_id);
                lueBranch.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.branch_id);
                lueLocation.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.location_id);
                lueDepartment.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.department_id);

                DataTable dtbPro = new DataTable();
                dtbPro = objProcess.GetData_All();
                lueProcess.Properties.DataSource = dtbPro;
                lueProcess.Properties.ValueMember = "process_id";
                lueProcess.Properties.DisplayMember = "process_name";

                DataTable dtbSubPro = new DataTable();
                dtbSubPro = objSubProcess.GetData();
                lueSubProcess.Properties.DataSource = dtbSubPro;
                lueSubProcess.Properties.ValueMember = "sub_process_id";
                lueSubProcess.Properties.DisplayMember = "sub_process_name";

                m_dtbSubProcess = (((DataTable)lueSubProcess.Properties.DataSource).Copy());

                btnClear_Click(btnClear, null);
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
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
            btnSave.Enabled = false;

            if (SaveDetails())
            {
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
                lueProcess.EditValue = null;
                lueCompany.EditValue = null;
                lueBranch.EditValue = null;
                lueLocation.EditValue = null;
                lueDepartment.EditValue = null;
                lueEmployee.EditValue = null;
                lueSubProcess.SetEditValue("");
                lueCompany.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.company_id);
                lueBranch.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.branch_id);
                lueLocation.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.location_id);
                lueDepartment.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.department_id);
                chkActive.Checked = true;
                lueProcess.Focus();
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
        private void lueProcess_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (lueProcess.EditValue != System.DBNull.Value)
                {
                    if (m_dtbSubProcess.Rows.Count > 0)
                    {
                        DataTable dtbdetail = m_dtbSubProcess;

                        string strFilter = string.Empty;

                        if (lueProcess.Text != "")
                            strFilter = "process_id = " + lueProcess.EditValue;


                        dtbdetail.DefaultView.RowFilter = strFilter;
                        dtbdetail.DefaultView.ToTable();

                        DataTable dtb = dtbdetail.DefaultView.ToTable();

                        lueSubProcess.Properties.DataSource = dtb;
                        lueSubProcess.Properties.ValueMember = "sub_process_id";
                        lueSubProcess.Properties.DisplayMember = "sub_process_name";

                        lueSubProcess.SetEditValue("");
                    }
                    if (Val.ToInt(lueProcess.EditValue) == 0 || Val.ToInt(lueEmployee.EditValue) == 0)
                    {
                        return;
                    }
                    if (Val.ToInt(lueEmployee.EditValue) > 0)
                    {
                        DataRow DR = (DataRow)objConfigProcess.GetProcessData(Val.ToInt(lueEmployee.EditValue), Val.ToInt(lueProcess.EditValue));
                        if (DR != null)
                        {
                            lueSubProcess.SetEditValue(Val.ToString(DR["sub_process_id"]));
                            lueSubProcess.Tag = Val.ToString(DR["sub_process_id"]);
                        }
                    }
                    else
                    {
                        //lueProcess.EditValue =
                        lueSubProcess.SetEditValue("");
                    }
                    m_StrGetSubProcess = lueSubProcess.Properties.GetCheckedItems().ToString().Replace(" ", "").Replace("  ", "").Trim();

                }

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
                return;
            }
        }
        private void lueDepartment_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtbEmp = new DataTable();
                dtbEmp = objConfigProcess.GetData(Val.ToInt(lueCompany.EditValue), Val.ToInt(lueBranch.EditValue), Val.ToInt(lueLocation.EditValue), Val.ToInt(lueDepartment.EditValue));
                lueEmployee.Properties.DataSource = dtbEmp;
                lueEmployee.Properties.ValueMember = "employee_id";
                lueEmployee.Properties.DisplayMember = "employee_name";
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void lueCompany_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtbEmp = new DataTable();
                dtbEmp = objConfigProcess.GetData(Val.ToInt(lueCompany.EditValue), Val.ToInt(lueBranch.EditValue), Val.ToInt(lueLocation.EditValue), Val.ToInt(lueDepartment.EditValue));
                lueEmployee.Properties.DataSource = dtbEmp;
                lueEmployee.Properties.ValueMember = "employee_id";
                lueEmployee.Properties.DisplayMember = "employee_name";
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void lueBranch_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtbEmp = new DataTable();
                dtbEmp = objConfigProcess.GetData(Val.ToInt(lueCompany.EditValue), Val.ToInt(lueBranch.EditValue), Val.ToInt(lueLocation.EditValue), Val.ToInt(lueDepartment.EditValue));
                lueEmployee.Properties.DataSource = dtbEmp;
                lueEmployee.Properties.ValueMember = "employee_id";
                lueEmployee.Properties.DisplayMember = "employee_name";
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void lueLocation_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtbEmp = new DataTable();
                dtbEmp = objConfigProcess.GetData(Val.ToInt(lueCompany.EditValue), Val.ToInt(lueBranch.EditValue), Val.ToInt(lueLocation.EditValue), Val.ToInt(lueDepartment.EditValue));
                lueEmployee.Properties.DataSource = dtbEmp;
                lueEmployee.Properties.ValueMember = "employee_id";
                lueEmployee.Properties.DisplayMember = "employee_name";
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void lueEmployee_EditValueChanged(object sender, EventArgs e)
        {
            try
            {

                if (Val.ToInt(lueProcess.EditValue) == 0 || Val.ToInt(lueEmployee.EditValue) == 0)
                {
                    return;
                }
                if (Val.ToInt(lueEmployee.EditValue) > 0 && Val.ToInt(lueProcess.EditValue) > 0)
                {
                    DataRow DR = (DataRow)objConfigProcess.GetProcessData(Val.ToInt(lueEmployee.EditValue), Val.ToInt(lueProcess.EditValue));
                    if (DR != null)
                    {
                        lueSubProcess.SetEditValue(Val.ToString(DR["sub_process_id"]));
                        lueSubProcess.Tag = Val.ToString(DR["sub_process_id"]);
                    }
                }
                else
                {
                    //lueProcess.EditValue =
                    lueSubProcess.SetEditValue("");
                }
                m_StrGetSubProcess = lueSubProcess.Properties.GetCheckedItems().ToString().Replace(" ", "").Replace("  ", "").Trim();



            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
                return;
            }
        }
        #endregion

        #region Functions
        private bool SaveDetails()
        {
            bool blnReturn = true;
            ConfigProcess_MasterProperty ConfigProcessMasterProperty = new ConfigProcess_MasterProperty();
            int pIntRes = 0;
            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }
                var StrSubProcess = lueSubProcess.Properties.GetCheckedItems().ToString().Replace(" ", "").Replace("  ", "").Trim();
                string[] array = StrSubProcess.Split(',');
                if (!string.IsNullOrEmpty(StrSubProcess))
                {
                    if (!m_StrGetSubProcess.Equals(StrSubProcess))
                    {
                        ConfigProcessMasterProperty.type = "Delete";
                        ConfigProcessMasterProperty.employee_id = Val.ToInt32(lueEmployee.EditValue);
                        ConfigProcessMasterProperty.process_id = Val.ToInt32(lueProcess.EditValue);
                        pIntRes = objConfigProcess.Delete(ConfigProcessMasterProperty);
                        foreach (var item in array)
                        {
                            ConfigProcessMasterProperty.type = "Process";
                            ConfigProcessMasterProperty.sub_process_id = Val.ToInt32(item);
                            ConfigProcessMasterProperty.employee_id = Val.ToInt32(lueEmployee.EditValue);
                            ConfigProcessMasterProperty.company_id = Val.ToInt32(lueCompany.EditValue);
                            ConfigProcessMasterProperty.branch_id = Val.ToInt32(lueBranch.EditValue);
                            ConfigProcessMasterProperty.location_id = Val.ToInt32(lueLocation.EditValue);
                            ConfigProcessMasterProperty.department_id = Val.ToInt32(lueDepartment.EditValue);

                            ConfigProcessMasterProperty.active = Val.ToBoolean(chkActive.Checked);
                            pIntRes = objConfigProcess.Save(ConfigProcessMasterProperty);
                        }
                    }

                }
                if (pIntRes == -1)
                {
                    Global.Confirm("Error In Save Config Process Details");
                    lueProcess.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Config Process Details Data Save Successfully");
                    }
                    else
                    {
                        Global.Confirm("Config Process Details Data Update Successfully");
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
                ConfigProcessMasterProperty = null;
            }

            return blnReturn;
        }
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (lueCompany.Text == "")
                {
                    lstError.Add(new ListError(13, "Company"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueCompany.Focus();
                    }
                }
                if (lueBranch.Text == "")
                {
                    lstError.Add(new ListError(13, "Branch"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueBranch.Focus();
                    }
                }
                if (lueLocation.Text == "")
                {
                    lstError.Add(new ListError(13, "Location"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueLocation.Focus();
                    }
                }
                if (lueDepartment.Text == "")
                {
                    lstError.Add(new ListError(13, "Department"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueDepartment.Focus();
                    }
                }
                if (lueEmployee.Text == "")
                {
                    lstError.Add(new ListError(13, "Employee"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueEmployee.Focus();
                    }
                }
                if (lueProcess.Text == string.Empty)
                {
                    lstError.Add(new ListError(13, "Process"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueProcess.Focus();
                    }
                }
                if (lueSubProcess.EditValue.ToString() == string.Empty)
                {
                    lstError.Add(new ListError(13, "Sub Process"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueSubProcess.Focus();
                    }
                }
                //if (!objProcess.ISExists(txtProcessName.Text, Val.ToInt(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
                //{
                //    lstError.Add(new ListError(23, "Process Name"));
                //    if (!blnFocus)
                //    {
                //        blnFocus = true;
                //        txtProcessName.Focus();
                //    }

                //}


            }
            catch (Exception ex)
            {
                lstError.Add(new ListError(ex));
            }
            return (!(BLL.General.ShowErrors(lstError)));
        }

        #endregion       
    }
}
