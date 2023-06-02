using BLL;
using BLL.FunctionClasses.Master;
using BLL.PropertyClasses.Master;
using DERP.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Master
{
    public partial class FrmLedgerMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.FormEvents objBOFormEvents;
        BLL.Validation Val;
        BLL.FormPer ObjPer;

        LedgerMaster objLedger;
        #endregion

        #region Constructor
        public FrmLedgerMaster()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            Val = new BLL.Validation();
            ObjPer = new BLL.FormPer();

            objLedger = new LedgerMaster();
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
            objBOFormEvents.ObjToDispose.Add(objLedger);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion

        #region Events 
        private void FrmLedgerMaster_Load(object sender, EventArgs e)
        {
            try
            {
                GetData();
                btnClear_Click(btnClear, null);
                txtLedgerName.Focus();
                Global.LOOKUPParty(lueParty);
                Global.LOOKUPBroker(lueBroker);
                Global.LOOKUPLocation(lueLocation);
                Global.LOOKUPCountry(lueOffCountry);
                Global.LOOKUPState(lueOffState);
                Global.LOOKUPCity(lueOffCity);
                Global.LOOKUPCountry(lueFacCountry);
                Global.LOOKUPState(lueFacState);
                Global.LOOKUPCity(lueFacCity);
                Global.LOOKUPAccountType(lueAccountType);
                Global.LOOKUPDesignation(lueDesignation);

                dtpBankBirthDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpBankBirthDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpBankBirthDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpBankBirthDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpBankBirthDate.EditValue = DateTime.Now;

                dtpBirthDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpBirthDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpBirthDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpBirthDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpBirthDate.EditValue = DateTime.Now;

                dtpGstInDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpGstInDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpGstInDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpGstInDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpGstInDate.EditValue = DateTime.Now;

                dtpOpeningDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpOpeningDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpOpeningDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpOpeningDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpOpeningDate.EditValue = DateTime.Now;

                dtpTinCstDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpTinCstDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpTinCstDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpTinCstDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpTinCstDate.EditValue = DateTime.Now;

                dtpTinVatDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpTinVatDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpTinVatDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpTinVatDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpTinVatDate.EditValue = DateTime.Now;

            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            ObjPer.FormName = this.Name.ToUpper();

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
                txtLedgerName.Text = string.Empty;
                txtShortName.Text = string.Empty;
                lueParty.EditValue = null;
                lueBroker.EditValue = null;
                lueAccountType.EditValue = null;
                lueLocation.EditValue = null;
                cmbLedgerType.Text = "";
                txtCrts.Text = "0.00";
                txtAdatPer.Text = "0.00";
                txtBrokerPer.Text = "0.00";
                txtAddress.Text = string.Empty;
                txtReferenceBy.Text = string.Empty;
                txtRemark.Text = string.Empty;
                chkIsInspection.Checked = false;
                chkIsAdat.Checked = false;
                chkIsBroker.Checked = false;
                chkFactory.Checked = false;
                ChkISOutSide.Checked = false;
                ChkISExpense.Checked = false;
                ChkISPattyCash.Checked = false;
                chkIsImpExpParty.Checked = false;
                ChkIsChecked.Checked = false;
                ChkISPartyLockOpen.Checked = false;
                txtOffAddress.Text = string.Empty;
                txtOffZipCode.Text = string.Empty;
                txtOffPhone.Text = string.Empty;
                txtOffEmailID.Text = string.Empty;
                txtOffFaxNo.Text = string.Empty;
                lueOffState.EditValue = null;
                lueOffCountry.EditValue = null;
                lueOffCity.EditValue = null;
                txtFacAddress.Text = string.Empty;
                txtFacZipcode.Text = string.Empty;
                txtFacEmailId.Text = string.Empty;
                txtFacFaxNo.Text = string.Empty;
                txtFacPhoneNo.Text = string.Empty;
                lueFacState.EditValue = null;
                lueFacCountry.EditValue = null;
                lueFacCity.EditValue = null;
                txtContactName.Text = string.Empty;
                txtAddress.Text = string.Empty;
                txtPhoneNo.Text = string.Empty;
                lueDesignation.EditValue = null;
                txtContactEmail.Text = string.Empty;
                dtpBirthDate.Text = string.Empty;
                txtMobileNo.Text = string.Empty;
                txtAccountNo.Text = string.Empty;
                txtBankEmailId.Text = string.Empty;
                dtpBankBirthDate.Text = string.Empty;
                txtBankBranch.Text = string.Empty;
                txtBankTransactionLimit.Text = string.Empty;
                txtPanNo.Text = string.Empty;
                txtTinVatNo.Text = string.Empty;
                txtTinCstNo.Text = string.Empty;
                txtGstIn.Text = string.Empty;
                txtTanNo.Text = string.Empty;
                txtMobileNo.Text = string.Empty;
                dtpTinVatDate.Text = string.Empty;
                dtpTinCstDate.Text = string.Empty;
                dtpGstInDate.Text = string.Empty;
                dtpOpeningDate.Text = string.Empty;
                txtSequenceNo.Text = string.Empty;
                txtOpeningBalance.Text = "0.00";
                lblActualOpeningBalance.Text = "0.00";
                txtActualOpeningBalance.Text = "0.00";
                txtOutStandingLimit.Text = string.Empty;
                chkActive.Checked = true;
                txtLedgerName.Focus();
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
        private void lblAddressCopy_Click(object sender, EventArgs e)
        {
            try
            {
                txtFacAddress.Text = txtOffAddress.Text;
                lueFacCountry.Text = lueOffCountry.Text;
                lueFacState.Text = lueOffState.Text;
                lueFacCity.Text = lueOffCity.Text;
                txtFacZipcode.Text = txtOffZipCode.Text;
                txtFacPhoneNo.Text = txtOffPhone.Text;
                txtFacEmailId.Text = txtOffEmailID.Text;
                txtFacFaxNo.Text = txtOffFaxNo.Text;
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }

        #region GridEvents
        private void dgvLedgerMaster_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvLedgerMaster.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt64(Drow["ledger_id"]);
                        txtLedgerName.Text = Val.ToString(Drow["ledger_name"]);
                        txtShortName.Text = Val.ToString(Drow["ledger_shortname"]);
                        lueBroker.EditValue = Val.ToInt32(Drow["broker_id"]);
                        lueParty.EditValue = Val.ToInt32(Drow["party_id"]);
                        lueAccountType.EditValue = Val.ToInt32(Drow["account_type_id"]);
                        lueLocation.EditValue = Val.ToInt32(Drow["location_id"]);
                        txtCrts.Text = Val.ToString(Drow["crts"]);
                        txtAdatPer.Text = Val.ToString(Drow["adat_per"]);
                        txtBrokerPer.Text = Val.ToString(Drow["broker_per"]);
                        dtpOpeningDate.Text = Val.ToString(Drow["opening_date"]);
                        txtOpeningBalance.Text = Val.ToString(Drow["opening_balance"]);
                        //lblActualOpeningBalance.Text = Val.ToString(Drow["actual_opening_balance"]);
                        txtActualOpeningBalance.Text = Val.ToString(Drow["actual_opening_balance"]);
                        txtReferenceBy.Text = Val.ToString(Drow["reference_by"]);
                        txtRemark.Text = Val.ToString(Drow["remarks"]);
                        chkIsInspection.Checked = Val.ToBoolean(Drow["is_inspection"]);
                        chkIsAdat.Checked = Val.ToBoolean(Drow["is_adat"]);
                        chkIsBroker.Checked = Val.ToBoolean(Drow["is_broker"]);
                        chkIsImpExpParty.Checked = Val.ToBoolean(Drow["is_imp_exp_party"]);
                        chkFactory.Checked = Val.ToBoolean(Drow["factory"]);
                        ChkISOutSide.Checked = Val.ToBoolean(Drow["is_outside"]);
                        ChkISExpense.Checked = Val.ToBoolean(Drow["is_expense"]);
                        ChkISPattyCash.Checked = Val.ToBoolean(Drow["is_pattycash"]);
                        txtOffAddress.Text = Val.ToString(Drow["office_address"]);
                        lueOffCountry.EditValue = Val.ToInt32(Drow["office_country_id"]);
                        lueOffState.EditValue = Val.ToInt32(Drow["office_state_id"]);
                        lueOffCity.EditValue = Val.ToInt32(Drow["office_city_id"]);
                        txtOffZipCode.Text = Val.ToString(Drow["office_pincode"]);
                        txtOffPhone.Text = Val.ToString(Drow["office_phone_no"]);
                        txtOffEmailID.Text = Val.ToString(Drow["office_email_id"]);
                        txtOffFaxNo.Text = Val.ToString(Drow["office_fax"]);
                        txtFacAddress.Text = Val.ToString(Drow["factory_address"]);
                        lueFacCountry.EditValue = Val.ToInt32(Drow["factory_country_id"]);
                        lueFacState.EditValue = Val.ToInt32(Drow["factory_state_id"]);
                        lueFacCity.EditValue = Val.ToInt32(Drow["factory_city_id"]);
                        txtFacZipcode.Text = Val.ToString(Drow["factory_pincode"]);
                        txtFacPhoneNo.Text = Val.ToString(Drow["factory_phone_no"]);
                        txtFacEmailId.Text = Val.ToString(Drow["factory_email_id"]);
                        txtFacFaxNo.Text = Val.ToString(Drow["factory_fax"]);
                        txtContactName.Text = Val.ToString(Drow["contact_name"]);
                        txtAddress.Text = Val.ToString(Drow["address"]);
                        lueDesignation.EditValue = Val.ToInt32(Drow["designation_id"]);
                        txtContactEmail.Text = Val.ToString(Drow["email_id"]);
                        dtpBirthDate.Text = Val.ToString(Drow["birth_date"]);
                        txtMobileNo.Text = Val.ToString(Drow["mobile_no"]);
                        txtPhoneNo.Text = Val.ToString(Drow["phone_no"]);
                        txtAccountNo.Text = Val.ToString(Drow["account_no"]);
                        txtBankEmailId.Text = Val.ToString(Drow["bank_email_id"]);
                        dtpBankBirthDate.Text = Val.ToString(Drow["bank_birth_date"]);
                        txtBankBranch.Text = Val.ToString(Drow["bank_branch_name"]);
                        txtBankTransactionLimit.Text = Val.ToString(Drow["bank_transaction_limit"]);
                        txtPanNo.Text = Val.ToString(Drow["pan_no"]);
                        txtTinVatNo.Text = Val.ToString(Drow["tin_vat_no"]);
                        txtTinCstNo.Text = Val.ToString(Drow["tin_cst_no"]);
                        txtGstIn.Text = Val.ToString(Drow["gstin"]);
                        txtTanNo.Text = Val.ToString(Drow["tan_no"]);
                        dtpTinVatDate.Text = Val.ToString(Drow["tin_vat_effective_date"]);
                        dtpTinCstDate.Text = Val.ToString(Drow["tin_cst_effective_date"]);
                        dtpGstInDate.Text = Val.ToString(Drow["gstin_effective_date"]);
                        txtSequenceNo.Text = Val.ToString(Drow["sequence_no"]);
                        chkActive.Checked = Val.ToBoolean(Drow["active"]);
                        cmbLedgerType.Text = Val.ToString(Drow["ledger_type"]);
                        ChkIsChecked.Checked = Val.ToBoolean(Drow["is_checked"]);
                        ChkISPartyLockOpen.Checked = Val.ToBoolean(Drow["is_party_lock_open"]);
                        txtOutStandingLimit.Text = Val.ToString(Drow["outstanding_limit"]);
                        txtLedgerName.Focus();
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
            Ledger_MasterProperty LedgerMasterProperty = new Ledger_MasterProperty();
            LedgerMaster objAssort = new LedgerMaster();
            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }
                List<ListError> lstError = new List<ListError>();


                LedgerMasterProperty.ledger_id = Val.ToInt32(lblMode.Tag);
                LedgerMasterProperty.ledger_name = Val.ToString(txtLedgerName.Text).ToUpper();
                LedgerMasterProperty.ledger_shortname = Val.ToString(txtShortName.Text).ToUpper();
                LedgerMasterProperty.party_id = Val.ToInt32(lueParty.EditValue);
                LedgerMasterProperty.broker_id = Val.ToInt32(lueBroker.EditValue);
                LedgerMasterProperty.active = Val.ToBoolean(chkActive.Checked);
                LedgerMasterProperty.remarks = Val.ToString(txtRemark.Text).ToUpper();
                LedgerMasterProperty.account_type_id = Val.ToInt32(lueAccountType.EditValue);
                LedgerMasterProperty.location_id = Val.ToInt32(GlobalDec.gEmployeeProperty.location_id);
                LedgerMasterProperty.crts = Val.ToDecimal(txtCrts.Text);
                LedgerMasterProperty.adat_per = Val.ToDecimal(txtAdatPer.Text);
                LedgerMasterProperty.broker_per = Val.ToDecimal(txtBrokerPer.Text);
                LedgerMasterProperty.opening_date = Val.DBDate(dtpOpeningDate.Text);
                LedgerMasterProperty.opening_balance = Val.ToDecimal(txtOpeningBalance.Text);
                //LedgerMasterProperty.actual_opening_balance = Val.ToDecimal(txtOpeningBalance.Text);
                LedgerMasterProperty.actual_opening_balance = Val.ToDecimal(txtActualOpeningBalance.Text);
                LedgerMasterProperty.reference_by = Val.ToString(txtReferenceBy.Text);
                LedgerMasterProperty.remarks = Val.ToString(txtRemark.Text);
                LedgerMasterProperty.is_inspection = Val.ToBoolean(chkIsInspection.Checked);
                LedgerMasterProperty.is_adat = Val.ToBoolean(chkIsAdat.Checked);
                LedgerMasterProperty.is_broker = Val.ToBoolean(chkIsBroker.Checked);
                LedgerMasterProperty.is_imp_exp_party = Val.ToBoolean(chkIsImpExpParty.Checked);
                LedgerMasterProperty.factory = Val.ToBoolean(chkFactory.Checked);
                LedgerMasterProperty.is_outside = Val.ToBoolean(ChkISOutSide.Checked);
                LedgerMasterProperty.is_expense = Val.ToBoolean(ChkISExpense.Checked);
                LedgerMasterProperty.is_pattycash = Val.ToBoolean(ChkISPattyCash.Checked);
                LedgerMasterProperty.office_address = Val.ToString(txtOffAddress.Text);
                LedgerMasterProperty.office_country_id = Val.ToInt32(lueOffCountry.EditValue);
                LedgerMasterProperty.office_state_id = Val.ToInt32(lueOffState.EditValue);
                LedgerMasterProperty.office_city_id = Val.ToInt32(lueOffCity.EditValue);
                LedgerMasterProperty.office_pincode = Val.ToString(txtOffZipCode.Text);
                LedgerMasterProperty.office_email_id = Val.ToString(txtOffEmailID.Text);
                LedgerMasterProperty.office_phone_no = Val.ToString(txtOffPhone.Text);
                LedgerMasterProperty.office_fax = Val.ToString(txtOffFaxNo.Text);
                LedgerMasterProperty.factory_address = Val.ToString(txtFacAddress.Text);
                LedgerMasterProperty.factory_country_id = Val.ToInt32(lueFacCountry.EditValue);
                LedgerMasterProperty.factory_state_id = Val.ToInt32(lueFacState.EditValue);
                LedgerMasterProperty.factory_city_id = Val.ToInt32(lueFacCity.EditValue);
                LedgerMasterProperty.factory_pincode = Val.ToString(txtFacZipcode.Text);
                LedgerMasterProperty.factory_email_id = Val.ToString(txtFacEmailId.Text);
                LedgerMasterProperty.factory_phone_no = Val.ToString(txtFacPhoneNo.Text);
                LedgerMasterProperty.factory_fax = Val.ToString(txtFacFaxNo.Text);
                LedgerMasterProperty.contact_name = Val.ToString(txtOffZipCode.Text);
                LedgerMasterProperty.address = Val.ToString(txtOffZipCode.Text);
                LedgerMasterProperty.designation_id = Val.ToInt32(lueDesignation.EditValue);
                LedgerMasterProperty.email_id = Val.ToString(txtContactEmail.Text);
                LedgerMasterProperty.birth_date = Val.DBDate(dtpBirthDate.Text);
                LedgerMasterProperty.mobile_no = Val.ToString(txtMobileNo.Text);
                LedgerMasterProperty.phone_no = Val.ToString(txtPhoneNo.Text);
                LedgerMasterProperty.account_no = Val.ToString(txtAccountNo.Text);
                LedgerMasterProperty.bank_email_id = Val.ToString(txtBankEmailId.Text);
                LedgerMasterProperty.bank_birth_date = Val.DBDate(dtpBankBirthDate.Text);
                LedgerMasterProperty.bank_branch_name = Val.ToString(txtOffZipCode.Text);
                LedgerMasterProperty.bank_transaction_limit = Val.ToDecimal(txtBankTransactionLimit.Text);
                LedgerMasterProperty.pan_no = Val.ToString(txtPanNo.Text);
                LedgerMasterProperty.tin_vat_no = Val.ToString(txtTinVatNo.Text);
                LedgerMasterProperty.tin_cst_no = Val.ToString(txtTinCstNo.Text);
                LedgerMasterProperty.gstin = Val.ToString(txtGstIn.Text);
                LedgerMasterProperty.tan_no = Val.ToString(txtTanNo.Text);
                LedgerMasterProperty.tin_vat_effective_date = Val.DBDate(dtpTinVatDate.Text);
                LedgerMasterProperty.tin_cst_effective_date = Val.DBDate(dtpTinCstDate.Text);
                LedgerMasterProperty.gstin_effective_date = Val.DBDate(dtpGstInDate.Text);
                LedgerMasterProperty.sequence_no = Val.ToInt32(txtSequenceNo.Text);
                LedgerMasterProperty.ledger_type = Val.ToString(cmbLedgerType.Text);
                LedgerMasterProperty.is_checked = Val.ToBoolean(ChkIsChecked.Checked);
                LedgerMasterProperty.is_party_lock_open = Val.ToBoolean(ChkISPartyLockOpen.Checked);
                LedgerMasterProperty.outstanding_limit = Val.ToInt64(txtOutStandingLimit.Text);

                int IntRes = objLedger.Save(LedgerMasterProperty);
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Save Ledger Details");
                    txtLedgerName.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Ledger Details Data Save Successfully");
                        GetData();
                    }
                    else
                    {
                        Global.Confirm("Ledger Details Data Update Successfully");
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
                LedgerMasterProperty = null;
            }

            return blnReturn;
        }
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (txtLedgerName.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Ledger Name"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtLedgerName.Focus();
                    }
                }

                if (txtShortName.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Short Name"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtShortName.Focus();
                    }
                }

                if (!objLedger.ISExists(txtShortName.Text, txtLedgerName.Text, Val.ToInt64(lblMode.Tag), Val.ToInt(GlobalDec.gEmployeeProperty.location_id)).ToString().Trim().Equals(string.Empty))
                {
                    lstError.Add(new ListError(23, "Ledger Name"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtShortName.Focus();
                    }

                }
                if (txtShortName.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Short Name"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtShortName.Focus();
                    }
                }
                //if (lueBroker.ItemIndex < 0)
                //{
                //    lstError.Add(new ListError(13, "Broker"));
                //    if (!blnFocus)
                //    {
                //        blnFocus = true;
                //        lueBroker.Focus();
                //    }
                //}
                //if (lueParty.ItemIndex < 0)
                //{
                //    lstError.Add(new ListError(13, "Party"));
                //    if (!blnFocus)
                //    {
                //        blnFocus = true;
                //        lueParty.Focus();
                //    }
                //}
                if (lueAccountType.Text == "")
                {
                    lstError.Add(new ListError(13, "Account Type"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueOffCountry.Focus();
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
                DataTable DTab = objLedger.GetData();
                grdLedgerMaster.DataSource = DTab;

                if (GlobalDec.gEmployeeProperty.user_name == "MILAN" || GlobalDec.gEmployeeProperty.user_name == "MAYUR")
                {
                    txtOutStandingLimit.Visible = true;
                    lblOutStandingLimit.Visible = true;
                    dgvLedgerMaster.Columns["outstanding_limit"].Visible = true;
                    ChkISPartyLockOpen.Visible = true;
                }
                else
                {
                    txtOutStandingLimit.Visible = false;
                    lblOutStandingLimit.Visible = false;
                    ChkISPartyLockOpen.Visible = false;
                    dgvLedgerMaster.Columns["outstanding_limit"].Visible = false;
                }
                //dgvLedgerMaster.BestFitColumns();
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void Export(string format, string dlgHeader, string dlgFilter)
        {
            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = format;
                svDialog.Title = dlgHeader;
                svDialog.FileName = "Report";
                svDialog.Filter = dlgFilter;
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    string Filepath = svDialog.FileName;

                    switch (format)
                    {
                        case "pdf":
                            dgvLedgerMaster.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvLedgerMaster.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvLedgerMaster.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvLedgerMaster.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvLedgerMaster.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvLedgerMaster.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvLedgerMaster.ExportToCsv(Filepath);
                            break;
                    }

                    if (format.Equals(Exports.xlsx.ToString()))
                    {
                        if (Global.Confirm("Export Done\n\nYou Want To Open Excel File ?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(Filepath);
                        }
                    }
                    else if (format.Equals(Exports.pdf.ToString()))
                    {
                        if (Global.Confirm("Export Done\n\nYou Want To Open PDF File ?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(Filepath);
                        }
                    }
                    else
                    {
                        if (Global.Confirm("Export Done\n\nYou Want To Open File ?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(Filepath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString(), "Error in Export");
            }
        }

        #endregion

        #region Export Grid
        private void MNExportExcel_Click(object sender, EventArgs e)
        {
            //Global.Export("xlsx", dgvRoughClarityMaster);
            Export("xlsx", "Export to Excel", "Excel files 97-2003 (Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*");
        }
        private void MNExportPDF_Click(object sender, EventArgs e)
        {
            // Global.Export("pdf", dgvRoughClarityMaster);
            Export("pdf", "Export Report to PDF", "PDF (*.PDF)|*.PDF");
        }
        private void MNExportTEXT_Click(object sender, EventArgs e)
        {
            Export("txt", "Export to Text", "Text files (*.txt)|*.txt|All files (*.*)|*.*");
        }

        private void MNExportHTML_Click(object sender, EventArgs e)
        {
            Export("html", "Export to HTML", "Html files (*.html)|*.html|Htm files (*.htm)|*.htm");
        }

        private void MNExportRTF_Click(object sender, EventArgs e)
        {
            Export("rtf", "Export to RTF", "Word (*.doc) |*.doc;*.rtf|(*.txt) |*.txt|(*.*) |*.*");
        }

        private void MNExportCSV_Click(object sender, EventArgs e)
        {
            Export("csv", "Export Report to CSVB", "csv (*.csv)|*.csv");
        }
        #endregion

        private void txtOutStandingLimit_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}
