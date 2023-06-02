using BLL;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.PropertyClasses.Transaction.MFG;
using DERP.Class;
using System;
using System.Data;
using System.Windows.Forms;

namespace DERP.Transaction
{
    public partial class FrmMfgRussianJobworkManagerUpdate : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        MfgDepartmentTransferConfirm objDepartmentCnf;
        DataTable m_dtbDetails;
        int IntRes;

        #endregion

        #region Constructor
        public FrmMfgRussianJobworkManagerUpdate()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            objDepartmentCnf = new MfgDepartmentTransferConfirm();
            m_dtbDetails = new DataTable();

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
            if (Global.CheckDefault() == 0)
            {
                Global.Message("Please Check User Default Setting");
                this.Close();
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
            objBOFormEvents.ObjToDispose.Add(objDepartmentCnf);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion   

        #region Events     
        private void FrmMfgRussianJobworkManagerUpdate_Load(object sender, EventArgs e)
        {
            Global.LOOKUPManagerName(lueFromManager);
            Global.LOOKUPManagerName(lueToManager);

            if (GlobalDec.gEmployeeProperty.role_name.ToUpper() == "SURAT RUSSIAN" || GlobalDec.gEmployeeProperty.role_name.ToUpper() == "SURAT ADMIN")
            {
                if (GlobalDec.gEmployeeProperty.user_name.ToUpper() == "PRAFUL" || GlobalDec.gEmployeeProperty.user_name.ToUpper() == "SUBHASH" || GlobalDec.gEmployeeProperty.user_name.ToUpper() == "KUNAL")
                {
                    btnConfirm.Enabled = true;
                }
                else
                {
                    btnConfirm.Enabled = false;
                }
            }
            else
            {
                btnConfirm.Enabled = false;
            }

            lueFromManager.Focus();
        }
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                ObjPer.SetFormPer();
                if (ObjPer.AllowUpdate == false || ObjPer.AllowInsert == false)
                {
                    Global.Message(BLL.GlobalDec.gStrPermissionInsUpdMsg);
                    return;
                }
                if (Val.ToString(lueFromManager.Text) == "")
                {
                    Global.Message("Please Select From Manager");
                    lueFromManager.Focus();
                    return;
                }
                if (Val.ToString(lueToManager.Text) == "")
                {
                    Global.Message("Please Select To Manager");
                    lueToManager.Focus();
                    return;
                }
                btnConfirm.Enabled = false;
                m_dtbDetails.AcceptChanges();
                if (m_dtbDetails.Rows.Count > 0)
                {
                    DialogResult result = MessageBox.Show("Do you want to Update Manager Data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                    if (result != DialogResult.Yes)
                    {
                        btnConfirm.Enabled = true;
                        return;
                    }

                    DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                    backgroundWorker_RussianJobworkUpdate.RunWorkerAsync();
                }
                else
                {
                    General.ShowErrors("Atleast 1 Lot must be select in grid.");
                    btnConfirm.Enabled = true;
                    return;
                }
                btnConfirm.Enabled = true;
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                this.Cursor = Cursors.Default;
                return;
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txtLotID_Validated(object sender, EventArgs e)
        {
            //if (Val.ToString(lueFromManager.Text) == "")
            //{
            //    Global.Message("Please Select From Manager");
            //    lueFromManager.Focus();
            //    return;
            //}
            //if (Val.ToString(lueToManager.Text) == "")
            //{
            //    Global.Message("Please Select To Manager");
            //    lueToManager.Focus();
            //    return;
            //}

            if (txtLotID.Text.Length == 0 && Val.ToInt64(txtLotID.Text) == 0)
            {
                return;
            }

            m_dtbDetails.AcceptChanges();

            if (txtLotID.Text.Length == 0)
            {
                return;
            }

            if (m_dtbDetails.Rows.Count > 0)
            {
                DataTable DTabTemp = new DataTable();
                if (m_dtbDetails.Select("lot_id=" + Val.ToInt64(txtLotID.Text)).Length > 0)
                {
                    Global.Message("Lot ID Already Added in a grid");
                    txtLotID.Text = "";
                    txtLotID.Focus();
                    return;
                }
                DTabTemp = objDepartmentCnf.JobWork_Manager_GetData(Val.ToInt64(txtLotID.Text), Val.ToInt64(lueFromManager.EditValue));

                if (DTabTemp.Rows.Count > 0)
                {
                    txtLotID.Text = "";
                    txtLotID.Focus();
                }
                else
                {
                    Global.Message("Manager Not Found in this Lot_ID");
                    return;
                }
                m_dtbDetails.Merge(DTabTemp);
            }
            else
            {
                //DataTable DTab_ValidateLotID = objDepartmentCnf.JobWork_Manager_GetData(Val.ToInt64(txtLotID.Text), Val.ToInt64(lueFromManager.EditValue));

                //if (DTab_ValidateLotID.Rows.Count > 0)
                //{
                //}
                //else
                //{
                //    Global.Message("Lot ID Not Issue in Janged");
                //    txtLotID.Text = "";
                //    txtLotID.Focus();
                //    return;
                //}

                m_dtbDetails = objDepartmentCnf.JobWork_Manager_GetData(Val.ToInt64(txtLotID.Text), Val.ToInt64(lueFromManager.EditValue));

                if (m_dtbDetails.Rows.Count > 0)
                {
                    if (Val.ToInt64(txtLotID.Text) != 0)
                    {
                        DataTable DtConfirm = Global.CheckConfirmJangedLot(Val.ToInt64(txtLotID.Text));
                        if (DtConfirm.Rows.Count == 0)
                        {
                            Global.Message("Please Confirm Lot First!!!");
                            return;
                        }
                    }
                    txtLotID.Text = "";
                    txtLotID.Focus();
                }
                else
                {
                    Global.Message("Manager Not Found in this Lot_ID");
                    return;
                }
            }
            grdRussainJobWorkManagerUpdate.DataSource = m_dtbDetails;
            grdRussainJobWorkManagerUpdate.RefreshDataSource();
            dgvRussainJobWorkManagerUpdate.BestFitColumns();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtLotID.Text = "";
            lueFromManager.EditValue = null;
            lueToManager.EditValue = null;
            m_dtbDetails = new DataTable();
            grdRussainJobWorkManagerUpdate.DataSource = null;
        }
        #endregion       

        private void backgroundWorker_RussianJobworkUpdate_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.Default;
                MFGDepartmentTransferProperty DepartmentTransferProperty = new MFGDepartmentTransferProperty();
                try
                {
                    IntRes = 0;

                    if (m_dtbDetails.Rows.Count > 0)
                    {
                        foreach (DataRow DRow in m_dtbDetails.Rows)
                        {
                            DepartmentTransferProperty.lot_srno = Val.ToInt64(DRow["lot_srno"]);
                            DepartmentTransferProperty.lot_id = Val.ToInt64(DRow["lot_id"]);
                            DepartmentTransferProperty.to_manager_id = Val.ToInt64(lueToManager.EditValue);
                            DepartmentTransferProperty.manager_id = Val.ToInt64(DRow["manager_id"]);

                            IntRes = objDepartmentCnf.ManagerRussainJobWork_Update(DepartmentTransferProperty);
                        }
                    }
                    if (IntRes == -1)
                    {
                        Global.Confirm("Error In Update Manager");
                    }
                }
                catch (Exception ex)
                {
                    IntRes = -1;
                    General.ShowErrors(ex.ToString());
                    return;
                }
                finally
                {
                    DepartmentTransferProperty = null;
                }
            }
            catch (Exception ex)
            {
                IntRes = -1;
                General.ShowErrors(ex.ToString());
                if (ex.InnerException != null)
                {
                    Global.Message(ex.InnerException.ToString());
                }
            }
        }
        private void backgroundWorker_RussianJobworkUpdate_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (IntRes > 0)
                {
                    Global.Confirm("Update Manager Date Successfully");
                    m_dtbDetails = null;
                    btnClear_Click(null, null);
                    this.Cursor = Cursors.Default;
                }
                else
                {
                    Global.Confirm("Error In Update Manager Date");
                    this.Cursor = Cursors.Default;
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
            if (Global.Confirm("Are you sure delete selected row?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                dgvRussainJobWorkManagerUpdate.DeleteRow(dgvRussainJobWorkManagerUpdate.GetRowHandle(dgvRussainJobWorkManagerUpdate.FocusedRowHandle));
                m_dtbDetails.AcceptChanges();
            }
        }
    }
}
