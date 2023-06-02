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
    public partial class FrmMFGInsuranceRate : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member
        BLL.FormEvents objBOFormEvents = new BLL.FormEvents();
        BLL.FormPer ObjPer = new BLL.FormPer();
        BLL.Validation Val = new BLL.Validation();
        MFGInsuranceMaster ObjInsuranceRate = new MFGInsuranceMaster();

        #endregion

        #region Constructor
        public FrmMFGInsuranceRate()
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
                if (!ObjInsuranceRate.ISExists(Val.ToInt(lueCompany.EditValue), Val.ToInt(lueBranch.EditValue), Val.ToInt(lueLocation.EditValue), Val.ToInt(lueDepartment.EditValue), Val.ToInt(lueQuality.EditValue), Val.ToInt(lueRoughSieve.EditValue), Val.ToInt64(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
                {
                    lstError.Add(new ListError(23, "Already Exist"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueDepartment.Focus();
                    }

                }
                //if (lueCompany.Text == "")
                //{
                //    lstError.Add(new ListError(13, "Company"));
                //    if (!blnFocus)
                //    {
                //        blnFocus = true;
                //        lueCompany.Focus();
                //    }
                //}
                //if (lueBranch.Text == "")
                //{
                //    lstError.Add(new ListError(13, "Branch"));
                //    if (!blnFocus)
                //    {
                //        blnFocus = true;
                //        lueBranch.Focus();
                //    }
                //}
                //if (lueLocation.Text == "")
                //{
                //    lstError.Add(new ListError(13, "Location"));
                //    if (!blnFocus)
                //    {
                //        blnFocus = true;
                //        lueLocation.Focus();
                //    }
                //}
                //if (lueDepartment.Text == "")
                //{
                //    lstError.Add(new ListError(13, "Department"));
                //    if (!blnFocus)
                //    {
                //        blnFocus = true;
                //        lueDepartment.Focus();
                //    }
                //}
                if (lueQuality.Text == "")
                {
                    lstError.Add(new ListError(13, "Quality"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueQuality.Focus();
                    }
                }
                if (lueRoughSieve.Text == "")
                {
                    lstError.Add(new ListError(13, "Sieve"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueRoughSieve.Focus();
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
            Global.LOOKUPRoughSieve(lueRoughSieve);
            Global.LOOKUPQuality(lueQuality);
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
                        DataRow Drow = dgvMFGInsuranceRate.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lueCompany.EditValue = Val.ToInt32(Drow["company_id"]);
                        lueBranch.EditValue = Val.ToInt32(Drow["branch_id"]);
                        lueLocation.EditValue = Val.ToInt32(Drow["location_id"]);
                        lueDepartment.EditValue = Val.ToInt32(Drow["department_id"]);
                        lueQuality.EditValue = Val.ToInt(Drow["quality_id"]);
                        lueQuality.Text = Val.ToString(Drow["quality_name"]);
                        lueRoughSieve.EditValue = Val.ToInt32(Drow["rough_sieve_id"]);
                        DTPRateDate.Text = Val.ToString(Drow["rate_date"]);
                        txtRate.Text = Val.ToString(Drow["rate"]);
                        lblMode.Tag = Val.ToInt32(Drow["insurance_rate_id"]);
                        lueQuality.Focus();
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
            InsuranceRate_MasterProperty Property = new InsuranceRate_MasterProperty();
            Property.insurance_rate_id = Val.ToInt(lblMode.Tag);
            //Property.company_id = Val.ToInt(lueCompany.EditValue);
            //Property.branch_id = Val.ToInt(lueBranch.EditValue);
            //Property.location_id = Val.ToInt(lueLocation.EditValue);
            //Property.department_id = Val.ToInt(lueDepartment.EditValue);
            Property.rate_date = Val.DBDate(DTPRateDate.Text);
            Property.rate = Val.ToDecimal(txtRate.Text);
            Property.quality_id = Val.ToInt(lueQuality.EditValue);
            Property.rough_sieve_id = Val.ToInt(lueRoughSieve.EditValue);
            //Property.active = Val.ToInt(lueDepartment.EditValue);            


            IntRes = ObjInsuranceRate.Save(Property);

            Property = null;
            this.Cursor = Cursors.Default;
            if (IntRes != 0)
            {
                if (Val.ToInt(lblMode.Tag) == 0)
                {
                    Global.Message("MFG Insurance Rate Successfully Saved");
                }
                else
                {
                    Global.Message("MFG Insurance Rate Update Successfully");
                }
                lueRoughSieve.EditValue = DBNull.Value;
                txtRate.Text = "";
                lblMode.Tag = 0;
                lueRoughSieve.Focus();
                GetData();

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

            InsuranceRate_MasterProperty Property = new InsuranceRate_MasterProperty();

            Property.insurance_rate_id = Val.ToInt(lblMode.Tag);

            IntRes += ObjInsuranceRate.Delete(Property);
            Property = null;
            if (IntRes != 0)
            {
                Global.Message("Insurance Rate Are SuccessFully Deleted");
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

            DataTable DTab = ObjInsuranceRate.GetData();
            grdMFGInsuranceRate.DataSource = DTab;
            dgvMFGInsuranceRate.BestFitColumns();
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
                lueQuality.EditValue = DBNull.Value;
                lueRoughSieve.EditValue = DBNull.Value;
                txtRate.Text = string.Empty;
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

    }
}
