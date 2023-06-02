using BLL;
using BLL.FunctionClasses.Utility;
using BLL.PropertyClasses.Utility;
using DERP.Class;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DERP.Utility
{
    public partial class FrmChangePassword : DevExpress.XtraEditors.XtraForm
    {
        #region Declaration

        BLL.FormEvents objBOFormEvents = new BLL.FormEvents();
        BLL.Validation Val = new BLL.Validation();
        BLL.FormPer ObjPer = new BLL.FormPer();
        UserAuthenticationProperty UserAuthenticationProperty = new UserAuthenticationProperty();
        string User_Name = string.Empty;

        #endregion

        #region Constructor

        public FrmChangePassword()
        {
            InitializeComponent();
        }

        public void AttachFormEvents()
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

        #region Form Events

        public void ShowForm()
        {
            ObjPer.FormName = this.Name.ToUpper();
            if (ObjPer.CheckPermission() == false)
            {
                Global.Message(BLL.GlobalDec.gStrPermissionViwMsg);
                this.Dispose();
                return;
            }
            Val.frmGenSet(this);
            AttachFormEvents();
            this.Show();
        }

        private void FrmChangePassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
                this.Close();
            }
        }

        #endregion

        #region Val

        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (lueEmployee.Text.Equals(string.Empty))
                {
                    lstError.Add(new ListError(5, "Employee Name Is Required."));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueEmployee.Focus();
                    }
                }

                if (txtOldPassword.Text.Equals(string.Empty))
                {
                    lstError.Add(new ListError(5, "Old Password Is Required."));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtOldPassword.Focus();
                    }
                }

                if (txtNewPassword.Text.Equals(string.Empty))
                {
                    lstError.Add(new ListError(5, "New Password Is Required."));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtNewPassword.Focus();
                    }
                }

                if (txtConfirmPassword.Text.Equals(string.Empty))
                {
                    lstError.Add(new ListError(5, "Confirm Password Is Required."));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtConfirmPassword.Focus();
                    }
                }
                if (!txtNewPassword.Text.Equals(txtConfirmPassword.Text))
                {
                    lstError.Add(new ListError(5, "New Password And Confirm Password Not Match."));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtConfirmPassword.Focus();
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

        #region Button Event

        private void BtnChangePassword_Click(object sender, EventArgs e)
        {
            try
            {
                if (ObjPer.AllowUpdate == false || ObjPer.AllowInsert == false)
                {
                    Global.Message(BLL.GlobalDec.gStrPermissionInsUpdMsg);
                    return;
                }

                if (!ValidateDetails())
                {
                    return;
                }

                DialogResult result = MessageBox.Show("Are You Sure To Change Password?", "Confirmation", MessageBoxButtons.YesNo);
                if (result != DialogResult.Yes)
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                Login objLogin = new Login();


                UserAuthenticationProperty = objLogin.ChangePassword(UserAuthenticationProperty, GlobalDec.Encrypt(txtOldPassword.Text, true), GlobalDec.Encrypt(txtNewPassword.Text, true), Val.ToInt64(lueEmployee.EditValue));

                this.Cursor = Cursors.Default;
                if (UserAuthenticationProperty.Message != "")
                {
                    Global.Message(UserAuthenticationProperty.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    lueEmployee.Focus();
                    return;
                }
                else
                {
                    Global.Message("Change Password Successfully.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lueEmployee.EditValue = null;
                    txtOldPassword.Text = "";
                    txtNewPassword.Text = "";
                    txtConfirmPassword.Text = "";

                    //FrmLogin FrmLogin = new FrmLogin();
                    //this.Hide();

                    //this.Close();
                    //this.Dispose();
                    //this.Cursor = Cursors.WaitCursor;
                    //foreach (System.Windows.Forms.Form Frm in this.MdiChildren)
                    //{
                    //    if (Frm.Name.ToUpper() != "FrmMainHome".ToUpper())
                    //    {
                    //        Frm.Focus();
                    //        Frm.Hide();
                    //        Frm.Close();
                    //        Frm.Dispose();
                    //    }
                    //}
                    //FrmLogin.ShowDialog();

                    //this.Cursor = Cursors.Default;
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Dispose();
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString());
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }
        #endregion

        private void FrmChangePassword_Shown(object sender, EventArgs e)
        {
            lueEmployee.Focus();
            Global.LOOKUPUser_GetData(lueEmployee);
        }
    }
}
