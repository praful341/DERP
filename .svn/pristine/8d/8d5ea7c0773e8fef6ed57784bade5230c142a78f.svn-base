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
    public partial class FrmMFGPartyLock : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member
        BLL.FormEvents objBOFormEvents = new BLL.FormEvents();
        BLL.FormPer ObjPer = new BLL.FormPer();
        BLL.Validation Val = new BLL.Validation();
        MFGPartyLockMaster ObjPartyLock = new MFGPartyLockMaster();

        #endregion

        #region Constructor
        public FrmMFGPartyLock()
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
                if (lueParty.Text == "")
                {
                    lstError.Add(new ListError(13, "Party"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueParty.Focus();
                    }
                }

                if (txtPcs.Text.Length == 0)
                {
                    lstError.Add(new ListError(5, "Pcs Is Required"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtPcs.Focus();
                    }
                }
                if (!ObjPartyLock.ISExists(Val.ToInt(lueParty.EditValue), Val.ToInt64(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
                {
                    lstError.Add(new ListError(23, "Already Exist"));
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

        #region Events
        private void FrmMFGLock_Load(object sender, EventArgs e)
        {
            Global.LOOKUPCompany(lueCompany);
            Global.LOOKUPBranch(lueBranch);
            Global.LOOKUPLocation(lueLocation);
            Global.LOOKUPDepartment(lueDepartment);
            Global.LOOKUPDepartment(lueSDepartment);
            Global.LOOKUPParty(lueParty);
            Global.LOOKUPParty(lueSParty);

            lueCompany.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.company_id);
            lueBranch.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.branch_id);
            lueLocation.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.location_id);
            lueDepartment.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.department_id);

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

            lueSDepartment.EditValue = DBNull.Value;
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
                        lueParty.EditValue = Val.ToInt32(Drow["party_id"]);
                        DTPFromDate.Text = Val.ToString(Drow["from_date"]);
                        DTPToDate.Text = Val.ToString(Drow["to_date"]);
                        txtPcs.Text = Val.ToString(Drow["total_pcs"]);
                        lblMode.Tag = Val.ToInt32(Drow["lock_id"]);
                        lueParty.Focus();
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
            PartyLock_MasterProperty Property = new PartyLock_MasterProperty();
            Property.lock_id = Val.ToInt(lblMode.Tag);
            Property.company_id = Val.ToInt(lueCompany.EditValue);
            Property.branch_id = Val.ToInt(lueBranch.EditValue);
            Property.location_id = Val.ToInt(lueLocation.EditValue);

            Property.department_id = Val.ToInt(lueDepartment.EditValue);
            Property.from_date = Val.DBDate(DTPFromDate.Text);
            Property.to_date = Val.DBDate(DTPToDate.Text);
            Property.total_pcs = Val.ToInt(txtPcs.Text);
            Property.party_id = Val.ToInt(lueParty.EditValue);
            //Property.active = Val.ToInt(lueDepartment.EditValue);            

            IntRes = ObjPartyLock.Save(Property);

            Property = null;
            this.Cursor = Cursors.Default;
            if (IntRes != 0)
            {
                if (Val.ToInt(lblMode.Tag) == 0)
                {
                    Global.Message("Party Lock Are Successfully Saved");
                }
                else
                {
                    Global.Message("Party Lock Are Updated Successfully");
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

            PartyLock_MasterProperty Property = new PartyLock_MasterProperty();

            Property.lock_id = Val.ToInt(lblMode.Tag);

            IntRes = ObjPartyLock.Delete(Property);
            Property = null;
            if (IntRes != 0)
            {
                Global.Message("Party Lock Are SuccessFully Deleted");
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

            PartyLock_MasterProperty Property = new PartyLock_MasterProperty();
            Property.party_id = Val.ToInt(lueSParty.EditValue);
            Property.department_id = Val.ToInt(lueSDepartment.EditValue);

            DataTable DTab = ObjPartyLock.GetData(Property);
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

                lueSDepartment.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.department_id);
                lueCompany.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.company_id);
                lueBranch.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.branch_id);
                lueLocation.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.location_id);
                lueDepartment.EditValue = Val.ToInt(GlobalDec.gEmployeeProperty.department_id);
                lueCompany.Enabled = false;
                lueBranch.Enabled = false;
                lueLocation.Enabled = false;
                lueDepartment.Enabled = false;
                txtPcs.Text = string.Empty;
                lblMode.Tag = 0;
                lueParty.EditValue = DBNull.Value;
                lueSParty.EditValue = DBNull.Value;

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
