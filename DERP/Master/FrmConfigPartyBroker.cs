using BLL;
using BLL.FunctionClasses.Master;
using BLL.PropertyClasses.Master;
using DERP.Class;
using System;
using System.Collections.Generic;
using System.Data;

namespace DERP.Master
{
    public partial class FrmConfigPartyBroker : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        FormEvents objBOFormEvents;
        Validation Val;
        BLL.FormPer ObjPer;

        ConfigPartyBrokerMaster objConfigPartyBroker;
        BrokerMaster objbroker;

        DataTable m_dtbProcesstype;

        string m_StrGetBroker;

        #endregion

        #region Constructor
        public FrmConfigPartyBroker()
        {
            InitializeComponent();

            objBOFormEvents = new FormEvents();
            Val = new Validation();
            ObjPer = new BLL.FormPer();

            objConfigPartyBroker = new ConfigPartyBrokerMaster();
            objbroker = new BrokerMaster();

            m_dtbProcesstype = new DataTable();

            m_StrGetBroker = "";
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
            chkActive.Checked = true;
        }
        private void AttachFormEvents()
        {
            objBOFormEvents.CurForm = this;
            objBOFormEvents.FormKeyPress = true;
            objBOFormEvents.FormKeyDown = true;
            objBOFormEvents.FormResize = true;
            objBOFormEvents.FormClosing = true;
            objBOFormEvents.ObjToDispose.Add(objConfigPartyBroker);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion

        #region Events
        private void FrmConfigPartyBroker_Load(object sender, EventArgs e)
        {
            try
            {
                Global.LOOKUPParty(lueParty);
                DataTable dtbBro = new DataTable();
                dtbBro = objbroker.GetData();
                lueBroker.Properties.DataSource = dtbBro;
                lueBroker.Properties.ValueMember = "broker_id";
                lueBroker.Properties.DisplayMember = "broker_name";
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
                lueBroker.SetEditValue("");
                lueParty.EditValue = null;
                chkActive.Checked = true;
                lueBroker.Focus();
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
        private void lueParty_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToInt(lueParty.EditValue) == 0)
                {
                    return;
                }
                if (Val.ToInt(lueParty.EditValue) > 0)
                {
                    DataRow DR = (DataRow)objConfigPartyBroker.GetBrokerData(Val.ToInt(lueParty.EditValue));
                    if (DR != null)
                    {
                        lueBroker.SetEditValue(Val.ToString(DR["broker_id"]));
                        lueBroker.Tag = Val.ToString(DR["broker_id"]);
                    }
                }
                else
                {
                    lueBroker.SetEditValue("");
                }
                m_StrGetBroker = lueBroker.Properties.GetCheckedItems().ToString().Replace(" ", "").Replace("  ", "").Trim();
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
            }
        }
        #endregion

        #region Functions
        private bool SaveDetails()
        {
            int pIntRes = 0;
            bool blnReturn = true;
            ConfigPartyBroker_MasterProperty ConfigPartyBrokerMasterProperty = new ConfigPartyBroker_MasterProperty();

            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }

                var StrBroker = lueBroker.Properties.GetCheckedItems().ToString().Replace(" ", "").Replace("  ", "").Trim();


                string[] array = StrBroker.Split(',');
                if (!string.IsNullOrEmpty(StrBroker))
                {
                    if (!m_StrGetBroker.Equals(StrBroker))
                    {
                        ConfigPartyBrokerMasterProperty.type = "Delete";
                        ConfigPartyBrokerMasterProperty.party_id = Val.ToInt32(lueParty.EditValue);
                        pIntRes = objConfigPartyBroker.Delete(ConfigPartyBrokerMasterProperty);
                        foreach (var item in array)
                        {
                            ConfigPartyBrokerMasterProperty.type = "Broker";
                            ConfigPartyBrokerMasterProperty.broker_id = Val.ToInt32(item);
                            ConfigPartyBrokerMasterProperty.party_id = Val.ToInt32(lueParty.EditValue);
                            ConfigPartyBrokerMasterProperty.active = Val.ToBoolean(chkActive.Checked);
                            pIntRes = objConfigPartyBroker.Save(ConfigPartyBrokerMasterProperty);
                        }
                    }

                }

                if (pIntRes == -1)
                {
                    Global.Confirm("Error In Save Config Process Details");
                    lueBroker.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Config Process Details Data Save Successfully");
                    }
                    else
                    {
                        Global.Confirm("Config Process Details Data Update Successfully");
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
                ConfigPartyBrokerMasterProperty = null;
            }

            return blnReturn;
        }
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (lueParty.Text == "")
                {
                    lstError.Add(new ListError(13, "Company"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueParty.Focus();
                    }
                }

                if (lueBroker.EditValue.ToString() == string.Empty)
                {
                    lstError.Add(new ListError(13, "Process"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueBroker.Focus();
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
    }
}
