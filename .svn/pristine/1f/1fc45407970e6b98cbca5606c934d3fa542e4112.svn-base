using BLL;
using BLL.FunctionClasses.Transaction;
using BLL.PropertyClasses.Transaction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Global = DERP.Class.Global;

namespace DREP.Transaction
{
    public partial class FrmMFGLock : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member
        BLL.FormEvents objBOFormEvents = new BLL.FormEvents();
        BLL.FormPer ObjPer = new BLL.FormPer();
        BLL.Validation Val = new BLL.Validation();
        MFGTranscationLock ObjTranscationLock = new MFGTranscationLock();
        string mStrTranscationType = "";

        #endregion

        #region Constructor
        public FrmMFGLock()
        {
            InitializeComponent();
        }
        public void ShowForm(string pStrFormType)
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

            if (pStrFormType == "OPEN-LOCK")
            {
                lblTitle.Text = "MFG [OPEN LOCK]";
                this.Text = "MFG [OPEN LOCK]";
                mStrTranscationType = "OPEN-LOCK";
            }
            else if (pStrFormType == "LOCK")
            {
                lblTitle.Text = "MFG [LOCK]";
                this.Text = "MFG [LOCK]";
                mStrTranscationType = "LOCK";
            }
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
                if (mStrTranscationType == "OPEN-LOCK")
                {
                    if (txtMinutes.Text.Length == 0)
                    {
                        lstError.Add(new ListError(5, "Minutes Is Required"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtMinutes.Focus();
                        }
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
            Global.LOOKUPDepartment(lueSDepartment);

            lueCompany.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.company_id);
            lueBranch.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.branch_id);
            lueLocation.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.location_id);

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

            DTPSFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPSFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPSFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPSFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
            DTPSFromDate.EditValue = DateTime.Now;

            DTPSToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPSToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPSToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPSToDate.Properties.CharacterCasing = CharacterCasing.Upper;
            DTPSToDate.EditValue = DateTime.Now;

            lueSDepartment.EditValue = DBNull.Value;

            if (GlobalDec.gEmployeeProperty.role_name.ToUpper() == "SURAT 4P" || GlobalDec.gEmployeeProperty.role_name.ToUpper() == "SURAT MAKABLE" ||
             GlobalDec.gEmployeeProperty.role_name.ToUpper() == "SURAT ADMIN" || GlobalDec.gEmployeeProperty.role_name.ToUpper() == "MASTER ADMIN" || GlobalDec.gEmployeeProperty.role_name.ToUpper() == "SURAT ROUGH" || GlobalDec.gEmployeeProperty.role_name.ToUpper() == "SURAT KAMALA" || GlobalDec.gEmployeeProperty.role_name.ToUpper() == "KAMALA ADMIN" || GlobalDec.gEmployeeProperty.role_name.ToUpper() == "AMBIKA ADMIN")
            {
                if (GlobalDec.gEmployeeProperty.user_name.ToUpper() == "PRAFUL" || GlobalDec.gEmployeeProperty.user_name.ToUpper() == "KETAN" ||
                        GlobalDec.gEmployeeProperty.user_name.ToUpper() == "LALU" || GlobalDec.gEmployeeProperty.user_name.ToUpper() == "PRAGNESH" || GlobalDec.gEmployeeProperty.user_name.ToUpper() == "KAILASH" || GlobalDec.gEmployeeProperty.user_name.ToUpper() == "RAHUL" || GlobalDec.gEmployeeProperty.user_name.ToUpper() == "JAYESH" || GlobalDec.gEmployeeProperty.user_name.ToUpper() == "YTHUMMAR")
                {
                    btnSave.Enabled = true;
                    btnDelete.Enabled = true;
                }
                else
                {
                    btnSave.Enabled = false;
                    btnDelete.Enabled = false;
                }
            }
            else
            {
                btnSave.Enabled = false;
                btnDelete.Enabled = false;
            }

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
                        DTPFromDate.Text = Val.ToString(Drow["from_date"]);
                        DTPToDate.Text = Val.ToString(Drow["to_date"]);
                        txtMinutes.Text = Val.ToString(Drow["minutes"]);
                        txtRemark.Text = Val.ToString(Drow["remarks"]);
                        chkActive.Checked = Val.ToBoolean(Drow["active"]);
                        lueCompany.Focus();
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

            DialogResult result = MessageBox.Show("Do you want to save Lock Open data?", "Confirmation", MessageBoxButtons.YesNoCancel);
            if (result != DialogResult.Yes)
            {
                btnSave.Enabled = true;
                return;
            }

            int IntRes = 0;
            this.Cursor = Cursors.WaitCursor;
            MFGTransactionLockProperty Property = new MFGTransactionLockProperty();

            Property.company_id = Val.ToInt(lueCompany.EditValue);
            Property.branch_id = Val.ToInt(lueBranch.EditValue);
            Property.location_id = Val.ToInt(lueLocation.EditValue);

            Property.department_id = Val.ToInt(lueDepartment.EditValue);
            Property.from_date = Val.DBDate(DTPFromDate.Text);
            Property.to_date = Val.DBDate(DTPToDate.Text);
            Property.minutes = Val.ToInt(txtMinutes.Text);
            Property.active = Val.ToBoolean(chkActive.Checked);
            Property.remark = Val.ToString(txtRemark.Text);

            if (mStrTranscationType == "OPEN-LOCK")
            {
                Property.transction_type = "OPEN-LOCK";
            }
            else if (mStrTranscationType == "LOCK")
            {
                Property.transction_type = "LOCK";
            }

            IntRes += ObjTranscationLock.Save(Property);

            Property = null;
            this.Cursor = Cursors.Default;
            if (IntRes != 0)
            {
                Global.Message("MFG Lock Are Successfully Saved");
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

            MFGTransactionLockProperty Property = new MFGTransactionLockProperty();

            Property.company_id = Val.ToInt(lueCompany.EditValue);
            Property.branch_id = Val.ToInt(lueBranch.EditValue);
            Property.location_id = Val.ToInt(lueLocation.EditValue);
            Property.department_id = Val.ToInt(lueDepartment.EditValue);
            Property.from_date = Val.DBDate(DTPFromDate.Text);
            Property.to_date = Val.DBDate(DTPToDate.Text);

            IntRes += ObjTranscationLock.Delete(Property);
            Property = null;
            if (IntRes != 0)
            {
                Global.Message("MFG Lock Are SuccessFully Deleted");
                GetData();
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
            if (lueSDepartment.Text == "")
            {
                lueSDepartment.Tag = 0;
            }

            MFGTransactionLockProperty Property = new MFGTransactionLockProperty();
            Property.department_id = Val.ToInt(lueDepartment.Tag);

            if (DTPSFromDate.Text != "")
            {
                Property.sfrom_date = Val.DBDate(DTPSFromDate.Text);
            }
            if (DTPSToDate.Text != "")
            {
                Property.sto_date = Val.DBDate(DTPSToDate.Text);
            }
            DataTable DTab = ObjTranscationLock.GetData(Property);
            grdMFGLock.DataSource = DTab;
            dgvMFGLock.BestFitColumns();
        }
        private bool ClearDetails()
        {
            bool blnReturn = true;
            try
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

                DTPSFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                DTPSFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                DTPSFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                DTPSFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
                DTPSFromDate.EditValue = DateTime.Now;

                DTPSToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                DTPSToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                DTPSToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                DTPSToDate.Properties.CharacterCasing = CharacterCasing.Upper;
                DTPSToDate.EditValue = DateTime.Now;

                chkActive.Checked = true;

                lueDepartment.EditValue = DBNull.Value;
                lueSDepartment.EditValue = DBNull.Value;

                lueCompany.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.company_id);
                lueBranch.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.branch_id);
                lueLocation.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.location_id);

                txtMinutes.Text = string.Empty;
                txtRemark.Text = string.Empty;
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
