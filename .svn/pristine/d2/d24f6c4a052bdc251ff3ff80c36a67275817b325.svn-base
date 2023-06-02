using BLL;
using BLL.FunctionClasses.Transaction;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

namespace DERP.Transaction
{
    public partial class FrmBranchTransferConfirm : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        BranchTransferConfirm objBranchCnf;

        DataTable DTab_Data;

        int m_numForm_id;
        int IntRes;
        int IntRes1;

        #endregion

        #region Constructor
        public FrmBranchTransferConfirm()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            objBranchCnf = new BranchTransferConfirm();

            DTab_Data = new DataTable();

            m_numForm_id = 0;
            IntRes = 0;
            IntRes1 = 0;
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
            this.Show();
        }
        private void AttachFormEvents()
        {
            objBOFormEvents.CurForm = this;
            objBOFormEvents.FormKeyPress = true;
            objBOFormEvents.FormKeyDown = true;
            objBOFormEvents.FormResize = true;
            objBOFormEvents.FormClosing = true;
            objBOFormEvents.ObjToDispose.Add(objBranchCnf);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion   

        #region Events     
        private void FrmBranchTransferConfirm_Load(object sender, EventArgs e)
        {
            GetData();
        }
        private void btnCnf_Click(object sender, EventArgs e)
        {
            try
            {
                ObjPer.SetFormPer();
                if (ObjPer.AllowUpdate == false || ObjPer.AllowInsert == false)
                {
                    Global.Message(BLL.GlobalDec.gStrPermissionInsUpdMsg);
                    return;
                }
                btnConfirm.Enabled = false;

                if (Val.ToInt(btnConfirm.Tag) == 0)
                {
                    return;
                }
                DialogResult result = MessageBox.Show("Do you want to save data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result != DialogResult.Yes)
                {
                    btnConfirm.Enabled = true;
                    return;
                }

                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;

                panelProgress.Visible = true;
                backgroundWorker_BTConfirm.RunWorkerAsync();

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
        private void backgroundWorker_BTConfirm_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.Default;
                if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                {
                    Conn = new BeginTranConnection(true, false);
                }
                else
                {
                    Conn = new BeginTranConnection(false, true);
                }
                BranchTransfer_ConfirmProperty BranchCnfProperty = new BranchTransfer_ConfirmProperty();

                try
                {
                    IntRes = 0;
                    IntRes1 = 0;

                    List<ListError> lstError = new List<ListError>();
                    BranchCnfProperty.bt_id = Val.ToInt32(btnConfirm.Tag);
                    DTab_Data = (DataTable)grdBTDetConfirm.DataSource;

                    int IntCounter = 0;
                    int Count = 0;
                    int TotalCount = DTab_Data.Rows.Count;

                    foreach (DataRow DRow in DTab_Data.Rows)
                    {
                        BranchCnfProperty.bt_id = Val.ToInt(DRow["bt_id"]);
                        BranchCnfProperty.bt_detail_id = Val.ToInt(DRow["bt_detail_id"]);
                        BranchCnfProperty.from_company_id = Val.ToInt(DRow["from_company_id"]);
                        BranchCnfProperty.from_branch_id = Val.ToInt(DRow["from_branch_id"]);
                        BranchCnfProperty.from_location_id = Val.ToInt(DRow["from_location_id"]);
                        BranchCnfProperty.from_department_id = Val.ToInt(DRow["from_department_id"]);
                        BranchCnfProperty.to_company_id = Val.ToInt(DRow["to_company_id"]);
                        BranchCnfProperty.to_branch_id = Val.ToInt(DRow["to_branch_id"]);
                        BranchCnfProperty.to_location_id = Val.ToInt(DRow["to_location_id"]);
                        BranchCnfProperty.to_department_id = Val.ToInt(DRow["to_department_id"]);
                        BranchCnfProperty.assort_id = Val.ToInt(DRow["assort_id"]);
                        BranchCnfProperty.sieve_id = Val.ToInt(DRow["sieve_id"]);
                        BranchCnfProperty.pcs = Val.ToInt(DRow["pcs"]);
                        BranchCnfProperty.carat = Val.ToDecimal(DRow["carat"]);
                        BranchCnfProperty.amount = Val.ToDecimal(DRow["amount"]);
                        BranchCnfProperty.rate = Val.ToDecimal(DRow["rate"]);
                        BranchCnfProperty.discount = Val.ToDecimal(DRow["discount"]);
                        BranchCnfProperty.currency_id = Val.ToInt(DRow["currency_id"]);
                        BranchCnfProperty.rate_type_id = Val.ToInt(DRow["rate_type_id"]);
                        BranchCnfProperty.type = "DETAIL";
                        BranchCnfProperty.current_rate = Val.ToDecimal(DRow["current_rate"]);
                        BranchCnfProperty.current_amount = Val.ToDecimal(DRow["current_rate"]) * Val.ToDecimal(DRow["carat"]);

                        IntRes = objBranchCnf.Update(BranchCnfProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                    }

                    BranchCnfProperty.type = "Receive";

                    if (IntRes == -1)
                    {
                        Global.Confirm("Error In Confirm Branch Details");
                    }
                    else
                    {
                        BranchCnfProperty.bt_id = Val.ToInt(btnConfirm.Tag);
                        BranchCnfProperty.type = "MASTER";
                        IntRes1 = objBranchCnf.Update(BranchCnfProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                        Count++;
                        IntCounter++;
                        IntRes1++;
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
                    IntRes1 = -1;
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
                    BranchCnfProperty = null;
                }
            }
            catch (Exception ex)
            {
                IntRes = -1;
                IntRes1 = -1;
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
        private void backgroundWorker_BTConfirm_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                panelProgress.Visible = false;
                if (IntRes1 > 0 || IntRes > 0)
                {
                    btnConfirm.Tag = "";
                    if (Val.ToInt(btnConfirm.Tag) == 0)
                    {
                        Global.Confirm("Branch Details Data Confirm Successfully");
                        GetData();
                        grdBTDetConfirm.DataSource = null;
                        this.Cursor = Cursors.Default;
                    }
                }
                else
                {
                    Global.Confirm("Error In Confirm Branch Details");
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }

        #region GridEvents
        private void dgvBTConfirm_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvBTConfirm.GetDataRow(e.RowHandle);
                        btnConfirm.Tag = Val.ToInt32(Drow["bt_id"]);
                        BtGetData();
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
        public void GetData()
        {
            try
            {
                DataTable DTab = objBranchCnf.GetData();
                grdBTConfirm.DataSource = DTab;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void BtGetData()
        {
            try
            {
                string type = "Detail";
                DataTable DTab = objBranchCnf.BTGetData(Val.ToInt(btnConfirm.Tag), type);
                grdBTDetConfirm.DataSource = DTab;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
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

        #endregion
    }
}
