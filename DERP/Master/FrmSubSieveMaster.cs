using BLL;
using BLL.FunctionClasses.Master;
using BLL.PropertyClasses.Master;
using DERP.Class;
using System;
using System.Collections.Generic;
using System.Data;

namespace DERP.Master
{
    public partial class FrmSubSieveMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.FormEvents objBOFormEvents;
        BLL.Validation Val;
        BLL.FormPer ObjPer;

        SubSieveMaster objSubSieve;

        #endregion

        #region Constructor
        public FrmSubSieveMaster()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            Val = new BLL.Validation();
            ObjPer = new BLL.FormPer();

            objSubSieve = new SubSieveMaster();
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
            objBOFormEvents.ObjToDispose.Add(objSubSieve);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }

        #endregion

        #region Events
        private void FrmSubSieveMaster_Load(object sender, EventArgs e)
        {
            try
            {
                Global.LOOKUPSieve(lueSieve);
                GetData();
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
                txtSubSieveName.Text = "";
                lueSieve.EditValue = null;
                chkActive.Checked = true;
                txtSubSieveName.Focus();
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
        private void MNExportExcel_Click(object sender, EventArgs e)
        {
            Global.Export("xlsx", dgvSubSieveMaster);
        }

        #region GridEvents
        private void dgvSubSieveMaster_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvSubSieveMaster.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt64(Drow["sub_sieve_id"]);
                        txtSubSieveName.Text = Val.ToString(Drow["sub_sieve_name"]);
                        lueSieve.EditValue = Val.ToInt32(Drow["sieve_id"]);
                        chkActive.Checked = Val.ToBoolean(Drow["active"]);
                        txtSubSieveName.Focus();
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
        private bool SaveDetails()
        {
            bool blnReturn = true;
            SubSieve_MasterProperty SSieveMasterProperty = new SubSieve_MasterProperty();

            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }


                SSieveMasterProperty.sub_sieve_id = Val.ToInt64(lblMode.Tag);
                SSieveMasterProperty.sub_sieve_name = txtSubSieveName.Text.ToUpper();
                SSieveMasterProperty.sieve_id = Val.ToInt(lueSieve.EditValue);
                SSieveMasterProperty.active = Val.ToBoolean(chkActive.Checked);

                int IntRes = objSubSieve.Save(SSieveMasterProperty);
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Save Sub Sieve Details");
                    txtSubSieveName.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Sub Sieve Details Data Save Successfully");
                    }
                    else
                    {
                        Global.Confirm("Sub Sieve Details Data Update Successfully");
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
                SSieveMasterProperty = null;
            }

            return blnReturn;
        }
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (txtSubSieveName.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Sub Sieve Name"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtSubSieveName.Focus();
                    }
                }

                if (!objSubSieve.ISExists(txtSubSieveName.Text, Val.ToInt64(lblMode.Tag), Val.ToInt32(lueSieve.EditValue)).ToString().Trim().Equals(string.Empty))
                {
                    lstError.Add(new ListError(23, "Sub Sieve Name"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtSubSieveName.Focus();
                    }

                }

                if (lueSieve.Text == "")
                {
                    lstError.Add(new ListError(13, "Sieve"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueSieve.Focus();
                    }
                }

            }
            catch (Exception ex)
            {
                lstError.Add(new ListError(ex));
            }
            return (!(BLL.General.ShowErrors(lstError)));

        }
        public void GetData()
        {
            try
            {
                DataTable DTab = objSubSieve.GetData();
                grdSubSieveMaster.DataSource = DTab;
                dgvSubSieveMaster.BestFitColumns();
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        #endregion
    }
}
