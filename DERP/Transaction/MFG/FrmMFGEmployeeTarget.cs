using BLL;
using BLL.FunctionClasses.Transaction;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using DERP.Master;
using DERP.Master.MFG;
using DREP.Master;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace DERP.Transaction.MFG
{
    public partial class FrmMFGEmployeeTarget : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        MFGEmployeeTarget objEmployeeTarget;

        DataTable m_dtbDetail;
        DataTable m_dtbParam;

        Int64 m_numForm_id;

        int IntRes;

        #endregion

        #region Constructor
        public FrmMFGEmployeeTarget()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            objEmployeeTarget = new MFGEmployeeTarget();

            m_dtbDetail = new DataTable();
            m_dtbParam = new DataTable();

            m_numForm_id = 0;
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
        private void FrmBrokeragePayable_Load(object sender, EventArgs e)
        {
            try
            {
                Global.LOOKUPDepartment_New(lueDepartment);
                Global.LOOKUPSubProcess(lueSubProcess);
                Global.LOOKUPManager(lueManager);
                dtpFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpFromDate.EditValue = DateTime.Now;

                dtpToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpToDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpToDate.EditValue = DateTime.Now;

                m_dtbParam = Global.GetRoughCutAll();

                FillCheckedCombo(m_dtbParam, "A1", CHKA1);
                FillCheckedCombo(m_dtbParam, "G1", CHKG1);
                FillCheckedCombo(m_dtbParam, "A5", CHKA5);
                FillCheckedCombo(m_dtbParam, "G5", CHKG5);
                FillCheckedCombo(m_dtbParam, "OTHER", CHKOTHER);

                lueDepartment_EditValueChanged(null, null);
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
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
                btnSave.Enabled = false;

                DialogResult result = MessageBox.Show("Do you want to save data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result != DialogResult.Yes)
                {
                    return;
                }
                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                backgroundWorker_EmployeeTarget.RunWorkerAsync();

                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        private void lueManager_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmEmployeeMaster frmManager = new FrmEmployeeMaster();
                frmManager.ShowDialog();
                Global.LOOKUPManager(lueManager);
            }
        }

        private void lueDepartment_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmDepartmentMaster frmDepartment = new FrmDepartmentMaster();
                frmDepartment.ShowDialog();
                Global.LOOKUPDepartment(lueDepartment);
            }
        }

        private void lueSubProcess_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmMfgSubProcessMaster frmSubProcess = new FrmMfgSubProcessMaster();
                frmSubProcess.ShowDialog();
                Global.LOOKUPSubProcess(lueSubProcess);
            }
        }

        private void backgroundWorker_EmployeeTarget_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.Default;
                MFGEmployeeTarget MFGEmployeeTarget = new MFGEmployeeTarget();
                MFGEmployeeTargetProperty objEmployeeTargetProperty = new MFGEmployeeTargetProperty();
                Conn = new BeginTranConnection(true, false);

                DataTable m_DTab = new DataTable();
                ArrayList MyArrayList = new ArrayList();

                IntRes = 0;
                try
                {
                    if (grdEmployeeTarget.DataSource != null)
                    {
                        m_DTab = (DataTable)grdEmployeeTarget.DataSource;

                        if (m_dtbDetail.Rows.Count > 0)
                        {
                            foreach (DataRow Drw in m_dtbDetail.Rows)
                            {
                                objEmployeeTargetProperty.performance_date = Val.ToString(Drw["performance_date"]);
                                objEmployeeTargetProperty.department_id = Val.ToInt(lueDepartment.EditValue);
                                objEmployeeTargetProperty.sub_process_id = Val.ToInt(lueSubProcess.EditValue);
                                objEmployeeTargetProperty.manager_id = Val.ToInt(lueManager.EditValue);
                                objEmployeeTargetProperty.company_id = GlobalDec.gEmployeeProperty.company_id;
                                objEmployeeTargetProperty.branch_id = GlobalDec.gEmployeeProperty.branch_id;
                                objEmployeeTargetProperty.location_id = GlobalDec.gEmployeeProperty.location_id;
                                IntRes = MFGEmployeeTarget.Delete(objEmployeeTargetProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                            }
                        }

                        for (int i = 0; i < m_DTab.Rows.Count; i++)
                        {
                            for (int j = 1; j < m_DTab.Columns.Count; j++)
                            {
                                if (m_DTab.Rows[i][j].ToString().Length > 0)
                                {
                                    string str = m_DTab.Rows[i][j].ToString();
                                    string date = m_DTab.Rows[i][0].ToString();

                                    ArrayList name = new ArrayList(str.Split(','));

                                    foreach (string item in name)
                                    {
                                        objEmployeeTargetProperty.emp_performance_id = i;
                                        objEmployeeTargetProperty.performance_date = Val.DBDate(date);
                                        objEmployeeTargetProperty.rough_cut_id = Val.ToInt(item);
                                        objEmployeeTargetProperty.target_days = 1;
                                        objEmployeeTargetProperty.manager_id = Val.ToInt(lueManager.EditValue);
                                        objEmployeeTargetProperty.sub_process_id = Val.ToInt(lueSubProcess.EditValue);
                                        objEmployeeTargetProperty.form_id = m_numForm_id;
                                        IntRes = MFGEmployeeTarget.Save(objEmployeeTargetProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                                    }
                                }
                            }
                        }
                        Conn.Inter1.Commit();
                    }
                    else
                    {
                        Global.Confirm("No data found");
                    }
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
                    objEmployeeTargetProperty = null;
                }
            }
            catch (Exception ex)
            {
                IntRes = -1;
                Conn.Inter1.Rollback();
                Conn = null;
                Global.Message(ex.ToString());
                if (ex.InnerException != null)
                {
                    Global.Message(ex.InnerException.ToString());
                }
            }
        }
        private void backgroundWorker_EmployeeTarget_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (IntRes > 0)
                {
                    Global.Confirm("Employee Target Save Succesfully");
                    grdEmployeeTarget.DataSource = null;
                }
                else
                {
                    Global.Confirm("Error In Employee Target");
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            grdEmployeeTarget.DataSource = null;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateDetails())
                {
                    return;
                }

                m_dtbDetail = objEmployeeTarget.GetData(Val.DBDate(dtpFromDate.Text), Val.DBDate(dtpToDate.Text), Val.ToInt(lueDepartment.EditValue), Val.ToInt(lueSubProcess.EditValue), Val.ToInt(lueManager.EditValue));
                grdEmployeeTarget.DataSource = m_dtbDetail;
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
            }
        }
        private void lueDepartment_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToInt(lueDepartment.EditValue) > 0 && Val.ToInt(lueManager.EditValue) > 0 && Val.ToInt(lueSubProcess.EditValue) > 0)
                {
                    btnSearch.Enabled = true;
                }
                else
                {
                    btnSearch.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void lueSubProcess_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToInt(lueDepartment.EditValue) > 0 && Val.ToInt(lueManager.EditValue) > 0 && Val.ToInt(lueSubProcess.EditValue) > 0)
                {
                    btnSearch.Enabled = true;
                }
                else
                {
                    btnSearch.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void lueManager_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToInt(lueDepartment.EditValue) > 0 && Val.ToInt(lueManager.EditValue) > 0 && Val.ToInt(lueSubProcess.EditValue) > 0)
                {
                    btnSearch.Enabled = true;
                }
                else
                {
                    btnSearch.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        #endregion

        #region Function
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();

            try
            {
                var result = DateTime.Compare(Convert.ToDateTime(dtpFromDate.Text), Convert.ToDateTime(dtpToDate.Text));
                if (result > 0)
                {
                    lstError.Add(new ListError(5, "From Date Not Be Greater Than To Date"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        dtpFromDate.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                lstError.Add(new ListError(ex));
            }
            return (!(BLL.General.ShowErrors(lstError)));
        }
        private void FillCheckedCombo(DataTable p_dtbTable, String p_strParam, DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit p_cmbParam)
        {
            try
            {
                DataTable dtbTable = p_dtbTable.Copy();
                dtbTable.DefaultView.RowFilter = "rough_cuttype_name IN ('" + p_strParam + "')";
                dtbTable = dtbTable.DefaultView.ToTable();

                if (dtbTable.Rows.Count > 0)
                {
                    p_cmbParam.DataSource = dtbTable;
                    p_cmbParam.ValueMember = "rough_cut_id";
                    p_cmbParam.DisplayMember = "rough_cut_no";
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
            }
        }
        #endregion       
    }
}
