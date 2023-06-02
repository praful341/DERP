using BLL;
using BLL.FunctionClasses.Master.MFG;
using BLL.PropertyClasses.Master.MFG;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Global = DERP.Class.Global;

namespace DREP.Transaction
{
    public partial class FrmMFGProcessRate : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member
        BLL.FormEvents objBOFormEvents = new BLL.FormEvents();
        BLL.FormPer ObjPer = new BLL.FormPer();
        BLL.Validation Val = new BLL.Validation();
        MFGProcessRateMaster ObjProcessRate = new MFGProcessRateMaster();
        DataTable m_dtbSubProcess = new DataTable();
        #endregion

        #region Constructor
        public FrmMFGProcessRate()
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
            BtnClear_Click(null, null);
            lueCompany.Focus();
        }

        private void AttachFormEvents()
        {
            objBOFormEvents.CurForm = this;
            objBOFormEvents.FormKeyDown = true;
            objBOFormEvents.FormKeyPress = true;
            objBOFormEvents.FormResize = true;
            objBOFormEvents.FormClosing = true;
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }

        #endregion

        #region Validation

        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();

            try
            {
                if (!ObjProcessRate.ISExists(Val.ToInt(lueCompany.EditValue), Val.ToInt(lueBranch.EditValue), Val.ToInt(lueLocation.EditValue), Val.ToInt(lueDepartment.EditValue), Val.ToInt(lueProcess.EditValue), Val.ToInt(lueSubProcess.EditValue), Val.ToInt(lueType.EditValue), Val.ToInt64(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
                {
                    lstError.Add(new ListError(23, "Already Exist"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueDepartment.Focus();
                    }

                }
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
                if (lueProcess.Text == "")
                {
                    lstError.Add(new ListError(13, "Process"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueProcess.Focus();
                    }
                }
                if (lueSubProcess.Text == "")
                {
                    lstError.Add(new ListError(13, "Sub Process"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueSubProcess.Focus();
                    }
                }
                if (lueType.Text == "")
                {
                    lstError.Add(new ListError(13, "Packet Type"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueType.Focus();
                    }
                }
                if (txtRate.Text.Length == 0)
                {
                    lstError.Add(new ListError(5, "Rate Is Required"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtRate.Focus();
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

        #region Events
        private void FrmMFGLock_Load(object sender, EventArgs e)
        {
            Global.LOOKUPCompany(lueCompany);
            Global.LOOKUPBranch(lueBranch);
            Global.LOOKUPLocation(lueLocation);
            Global.LOOKUPDepartment(lueDepartment);
            Global.LOOKUPProcess(lueProcess);
            Global.LOOKUPSubProcess(lueSubProcess);
            Global.LOOKUPMfgPacketTypeWages(lueType);

            m_dtbSubProcess = (((DataTable)lueSubProcess.Properties.DataSource).Copy());
            lueCompany.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.company_id);
            lueBranch.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.branch_id);
            lueLocation.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.location_id);
            lueDepartment.EditValue = Val.ToInt64(GlobalDec.gEmployeeProperty.department_id);

            DTPRateDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPRateDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPRateDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPRateDate.Properties.CharacterCasing = CharacterCasing.Upper;
            DTPRateDate.EditValue = DateTime.Now;


            ClearDetails();
        }
        private void dgvMFGLock_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvMFGLock.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lueCompany.EditValue = Val.ToInt32(Drow["company_id"]);
                        lueBranch.EditValue = Val.ToInt32(Drow["branch_id"]);
                        lueLocation.EditValue = Val.ToInt32(Drow["location_id"]);
                        lueDepartment.EditValue = Val.ToInt32(Drow["department_id"]);
                        lueProcess.EditValue = Val.ToInt32(Drow["process_id"]);
                        lueSubProcess.EditValue = Val.ToInt32(Drow["sub_process_id"]);
                        lueType.EditValue = Val.ToInt32(Drow["packet_type_id"]);
                        DTPRateDate.Text = Val.ToString(Drow["rate_date"]);
                        txtRate.Text = Val.ToString(Drow["rate"]);
                        lblMode.Tag = Val.ToInt32(Drow["process_rate_id"]);
                        lueProcess.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            lblMode.Text = "Add Mode";

            ClearDetails();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (ObjPer.AllowUpdate == false || ObjPer.AllowInsert == false)
            {
                Global.Message(BLL.GlobalDec.gStrPermissionInsUpdMsg);
                return;
            }

            if (!ValidateDetails())
            {
                btnSave.Enabled = true;
                return;
            }
            int IntRes = 0;
            this.Cursor = Cursors.WaitCursor;
            ProcessRate_MasterProperty Property = new ProcessRate_MasterProperty();
            Property.process_rate_id = Val.ToInt(lblMode.Tag);
            Property.company_id = Val.ToInt(lueCompany.EditValue);
            Property.branch_id = Val.ToInt(lueBranch.EditValue);
            Property.location_id = Val.ToInt(lueLocation.EditValue);
            Property.department_id = Val.ToInt(lueDepartment.EditValue);
            Property.rate_date = Val.DBDate(DTPRateDate.Text);
            Property.rate = Val.ToDecimal(txtRate.Text);
            Property.packet_type_id = Val.ToInt(lueType.EditValue);
            Property.process_id = Val.ToInt(lueProcess.EditValue);
            Property.sub_process_id = Val.ToInt(lueSubProcess.EditValue);
            //Property.active = Val.ToInt(lueDepartment.EditValue);            


            IntRes = ObjProcessRate.Save(Property);

            Property = null;
            this.Cursor = Cursors.Default;
            if (IntRes != 0)
            {
                if (Val.ToInt(lblMode.Tag) == 0)
                {
                    Global.Message("MFG Process Rate Successfully Saved");
                }
                else
                {
                    Global.Message("MFG Process Rate Update Successfully");
                }
                GetData();
                ClearDetails();
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (ObjPer.AllowDelete == false)
            {
                Global.Message(BLL.GlobalDec.gStrPermissionDelMsg);
                return;
            }

            int IntRes = 0;

            ProcessRate_MasterProperty Property = new ProcessRate_MasterProperty();

            Property.process_rate_id = Val.ToInt(lblMode.Tag);

            IntRes += ObjProcessRate.Delete(Property);
            Property = null;
            if (IntRes != 0)
            {
                Global.Message("Process Rate Are SuccessFully Deleted");
                GetData();
                ClearDetails();
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            GetData();
            this.Cursor = Cursors.Default;
        }

        #endregion

        #region Other Function

        private void GetData()
        {

            ProcessRate_MasterProperty Property = new ProcessRate_MasterProperty();
            DataTable DTab = ObjProcessRate.GetData();
            grdMFGLock.DataSource = DTab;
            dgvMFGLock.BestFitColumns();
        }
        private bool ClearDetails()
        {
            bool blnReturn = true;
            try
            {
                DTPRateDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                DTPRateDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                DTPRateDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                DTPRateDate.Properties.CharacterCasing = CharacterCasing.Upper;
                DTPRateDate.EditValue = DateTime.Now;

                //lueDepartment.EditValue = DBNull.Value;

                lueCompany.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.company_id);
                lueBranch.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.branch_id);
                lueLocation.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.location_id);
                lueDepartment.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.department_id);

                lueCompany.Enabled = false;
                lueBranch.Enabled = false;
                lueLocation.Enabled = false;
                lueDepartment.Enabled = false;
                lueProcess.EditValue = DBNull.Value;
                lueSubProcess.EditValue = DBNull.Value;
                lueType.EditValue = DBNull.Value;
                txtRate.Text = string.Empty;
                lueProcess.EditValue = DBNull.Value;
                lblMode.Tag = 0;
                GetData();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
        }

        #endregion

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
    }
}
