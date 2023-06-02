using BLL;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Master.MFG;
using BLL.PropertyClasses.Master;
using DERP.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace DERP.Master.MFG
{
    public partial class FrmMFGBenchMarkMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        FormEvents objBOFormEvents = new FormEvents();
        Validation Val;
        BLL.FormPer ObjPer;

        BenchMarkMaster objBenchMarkMaster;
        ProcessMaster objProcess;
        MfgSubProcessMaster objSubProcess;

        DataTable m_dtbProcesstype;
        DataTable m_dtbSubProcess;

        #endregion

        #region Constructor
        public FrmMFGBenchMarkMaster()
        {
            InitializeComponent();

            objBOFormEvents = new FormEvents();
            Val = new Validation();
            ObjPer = new BLL.FormPer();

            objBenchMarkMaster = new BenchMarkMaster();
            objProcess = new ProcessMaster();
            objSubProcess = new MfgSubProcessMaster();

            m_dtbProcesstype = new DataTable();
            m_dtbSubProcess = new DataTable();
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
            objBOFormEvents.ObjToDispose.Add(objBenchMarkMaster);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion

        #region Events
        private void FrmMFGBenchMarkMaster_Load(object sender, EventArgs e)
        {
            try
            {
                GetData();
                Global.LOOKUPCompany_New(lueCompany);
                Global.LOOKUPBranch_New(lueBranch);
                Global.LOOKUPLocation_New(lueLocation);
                Global.LOOKUPDepartment_New(lueDepartment);

                //lueCompany.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.company_id);
                //lueBranch.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.branch_id);
                //lueLocation.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.location_id);
                //lueDepartment.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.department_id);

                //DataTable dtbPro = new DataTable();
                //dtbPro = objProcess.GetData_All();
                //lueProcess.Properties.DataSource = dtbPro;
                //lueProcess.Properties.ValueMember = "process_id";
                //lueProcess.Properties.DisplayMember = "process_name";

                //DataTable dtbSubPro = new DataTable();
                //dtbSubPro = objSubProcess.GetData();
                //lueSubProcess.Properties.DataSource = dtbSubPro;
                //lueSubProcess.Properties.ValueMember = "sub_process_id";
                //lueSubProcess.Properties.DisplayMember = "sub_process_name";

                //m_dtbSubProcess = (((DataTable)lueSubProcess.Properties.DataSource).Copy());

                Global.LOOKUPProcess(lueProcess);
                Global.LOOKUPSubProcess(lueSubProcess);



                btnClear_Click(btnClear, null);
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        public void GetData()
        {
            try
            {
                DataTable DTab = objBenchMarkMaster.GetData();
                grdBenchMarkMaster.DataSource = DTab;
                dgvBenchMarkMaster.BestFitColumns();
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
                lueProcess.EditValue = null;
                lueSubProcess.EditValue = null;
                dtpBenchMarkDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpBenchMarkDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpBenchMarkDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpBenchMarkDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpBenchMarkDate.EditValue = DateTime.Now;
                txtTotalPcs.Text = "0";
                txtTotalCarat.Text = "0";
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

        #endregion

        #region Functions
        private bool SaveDetails()
        {
            bool blnReturn = true;
            BenchMark_MasterProperty BenchMarkMasterProperty = new BenchMark_MasterProperty();
            int pIntRes = 0;
            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }

                BenchMarkMasterProperty.benchmark_id = Val.ToInt64(lblMode.Tag);
                BenchMarkMasterProperty.benchmark_date = Val.DBDate(dtpBenchMarkDate.Text);
                BenchMarkMasterProperty.company_id = Val.ToInt64(lueCompany.EditValue);
                BenchMarkMasterProperty.branch_id = Val.ToInt64(lueBranch.EditValue);
                BenchMarkMasterProperty.location_id = Val.ToInt64(lueLocation.EditValue);
                BenchMarkMasterProperty.department_id = Val.ToInt64(lueDepartment.EditValue);
                BenchMarkMasterProperty.process_id = Val.ToInt64(lueProcess.EditValue);
                BenchMarkMasterProperty.sub_process_id = Val.ToInt64(lueSubProcess.EditValue);
                BenchMarkMasterProperty.total_pcs = Val.ToInt64(txtTotalPcs.Text);
                BenchMarkMasterProperty.total_carat = Val.ToDecimal(txtTotalCarat.Text);

                pIntRes = objBenchMarkMaster.Save(BenchMarkMasterProperty);

                if (pIntRes == -1)
                {
                    Global.Confirm("Error In Save BenchMark Master Details");
                    lueProcess.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("BenchMark Master Data Save Successfully");
                    }
                    else
                    {
                        Global.Confirm("BenchMark Master Data Update Successfully");
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
                BenchMarkMasterProperty = null;
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
                if (lueProcess.Text == string.Empty)
                {
                    lstError.Add(new ListError(13, "Process"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueProcess.Focus();
                    }
                }
                if (lueSubProcess.Text == string.Empty)
                {
                    lstError.Add(new ListError(13, "Sub Process"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueSubProcess.Focus();
                    }
                }

                if (!objBenchMarkMaster.ISExists(Val.ToInt64(lueCompany.EditValue), Val.ToInt64(lueBranch.EditValue), Val.ToInt64(lueLocation.EditValue), Val.ToInt64(lueDepartment.EditValue), Val.ToInt64(lueProcess.EditValue), Val.ToInt64(lueSubProcess.EditValue), Val.ToInt64(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
                {
                    lstError.Add(new ListError(23, "Comp AND Bran. AND Loc. AND Dept. AND Proc. AND Sub Proc."));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueCompany.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                lstError.Add(new ListError(ex));
            }
            return (!(BLL.General.ShowErrors(lstError)));
        }

        #endregion

        private void txtTotalPcs_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtTotalCarat_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as DevExpress.XtraEditors.TextEdit).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void lueProcess_Validated(object sender, EventArgs e)
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
                        lueSubProcess.EditValue = System.DBNull.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }

        private void dgvBenchMarkMaster_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvBenchMarkMaster.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt64(Drow["benchmark_id"]);
                        dtpBenchMarkDate.Text = Val.DBDate(Drow["benchmark_date"].ToString());
                        lueCompany.EditValue = Val.ToInt32(Drow["company_id"]);
                        lueBranch.EditValue = Val.ToInt32(Drow["branch_id"]);
                        lueLocation.EditValue = Val.ToInt32(Drow["location_id"]);
                        lueDepartment.EditValue = Val.ToInt32(Drow["department_id"]);
                        lueProcess.EditValue = Val.ToInt32(Drow["process_id"]);
                        lueSubProcess.EditValue = Val.ToInt32(Drow["sub_process_id"]);
                        txtTotalPcs.Text = Val.ToString(Drow["total_pcs"]);
                        txtTotalCarat.Text = Val.ToString(Drow["total_carat"]);
                        dtpBenchMarkDate.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);

                return;
            }
        }
    }
}
