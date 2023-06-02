using BLL;
using BLL.FunctionClasses.Master;
using BLL.PropertyClasses.Master;
using DERP.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Master
{
    public partial class FrmRoughPartyMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.FormEvents objBOFormEvents;
        BLL.Validation Val;
        BLL.FormPer ObjPer;

        RoughPartyMaster objRoughParty;

        DataTable m_dtbBussinesstype;
        DataTable m_dtbInitName;
        DataTable m_dtbPartyType;
        DataTable m_dtbRegSource;
        FillCombo ObjFillCombo = new FillCombo();

        string m_OldAadharPath;
        string m_OldPanPath;
        #endregion

        #region Constructor
        public FrmRoughPartyMaster()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            Val = new BLL.Validation();
            ObjPer = new BLL.FormPer();

            objRoughParty = new RoughPartyMaster();

            m_dtbBussinesstype = new DataTable();
            m_dtbInitName = new DataTable();
            m_dtbPartyType = new DataTable();
            m_dtbRegSource = new DataTable();

            m_OldAadharPath = null;
            m_OldPanPath = null;
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
            TabRegisterDetail.SelectedTabPageIndex = 0;
            txtPartyName.Focus();
            this.Show();
        }
        private void AttachFormEvents()
        {
            objBOFormEvents.CurForm = this;
            objBOFormEvents.FormKeyPress = true;
            objBOFormEvents.FormKeyDown = true;
            objBOFormEvents.FormResize = true;
            objBOFormEvents.FormClosing = true;
            objBOFormEvents.ObjToDispose.Add(objRoughParty);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }

        #endregion

        #region Events
        private void FrmPartyMaster_Load(object sender, EventArgs e)
        {
            try
            {
                m_dtbBussinesstype = new DataTable();
                m_dtbInitName = new DataTable();
                m_dtbPartyType = new DataTable();
                m_dtbRegSource = new DataTable();

                GetData();
                btnClear_Click(btnClear, null);

                Global.LOOKUPCountry(lueCountry);
                Global.LOOKUPState(lueState);
                Global.LOOKUPCity(lueCity);
                Global.LOOKUPCountry(luePhnCountry);
                Global.LOOKUPCountry(lueMobCountry);
                Global.LOOKUPCity(luePhnCity);
                Global.LOOKUPCategory(lueCategory);
                Global.LOOKUPIDProof(lueIDProof);

                DataTable DTabManager = ObjFillCombo.FillCmb(FillCombo.TABLE.Manager_Master);
                DTabManager.DefaultView.Sort = "employee_name";
                DTabManager = DTabManager.DefaultView.ToTable();

                lueManager.Properties.DataSource = DTabManager;
                lueManager.Properties.ValueMember = "employee_id";
                lueManager.Properties.DisplayMember = "employee_name";
                lueManager.ClosePopup();

                //Global.LOOKUPManager(lueManager);

                m_dtbBussinesstype.Columns.Add("business_type");
                m_dtbBussinesstype.Rows.Add("MFG");
                m_dtbBussinesstype.Rows.Add("BUYER");
                m_dtbBussinesstype.Rows.Add("SELLER");

                lueBusinessType.Properties.DataSource = m_dtbBussinesstype;
                lueBusinessType.Properties.ValueMember = "business_type";
                lueBusinessType.Properties.DisplayMember = "business_type";

                lueBusinessType.EditValue = Val.ToString(m_dtbBussinesstype.Rows[1]["business_type"].ToString());

                m_dtbInitName.Columns.Add("init_name");
                m_dtbInitName.Rows.Add("Mr.");
                m_dtbInitName.Rows.Add("Mrs.");

                lueInitName.Properties.DataSource = m_dtbInitName;
                lueInitName.Properties.ValueMember = "init_name";
                lueInitName.Properties.DisplayMember = "init_name";

                m_dtbPartyType.Columns.Add("party_type");
                m_dtbPartyType.Rows.Add("Inhouse");
                m_dtbPartyType.Rows.Add("Outside");

                luePartyType.Properties.DataSource = m_dtbPartyType;
                luePartyType.Properties.ValueMember = "party_type";
                luePartyType.Properties.DisplayMember = "party_type";

                m_dtbRegSource.Columns.Add("registration_source");
                m_dtbRegSource.Rows.Add("App");
                m_dtbRegSource.Rows.Add("Software");
                m_dtbRegSource.Rows.Add("Web");
                m_dtbRegSource.Rows.Add("IPad");

                lueRegSource.Properties.DataSource = m_dtbRegSource;
                lueRegSource.Properties.ValueMember = "registration_source";
                lueRegSource.Properties.DisplayMember = "registration_source";
                chkActive.Checked = true;
                txtPartyName.Focus();
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
                txtPartyName.Text = string.Empty;
                txtShortName.Text = string.Empty;
                txtFName.Text = string.Empty;
                txtLName.Text = string.Empty;
                lueInitName.EditValue = null;
                lueCategory.EditValue = null;
                //lueBusinessType.EditValue = null;
                lueBusinessType.EditValue = Val.ToString("BUYER");
                lueCity.EditValue = null;
                lueState.EditValue = null;
                lueCountry.EditValue = null;
                luePartyType.EditValue = null;
                lueManager.EditValue = null;
                txtEmailID.Text = string.Empty;
                txtZipCode.Text = string.Empty;
                txtRemark.Text = string.Empty;
                txtAddress.Text = string.Empty;
                txtPhone1.Text = string.Empty;
                txtMobileNo.Text = string.Empty;
                txtCSTNo.Text = string.Empty;
                luePhnCity.EditValue = null;
                lueMobCountry.EditValue = null;
                luePhnCountry.EditValue = null;
                txtFaxNo.Text = string.Empty;
                txtWebsite.Text = string.Empty;
                txtSecEmail.Text = string.Empty;
                txtDiscount.Text = string.Empty;
                txtAadharNo.Text = string.Empty;
                txtPanCardNo.Text = string.Empty;
                txtTdsCircle.Text = string.Empty;
                txtSequenceNo.Text = string.Empty;
                txtVatNo.Text = string.Empty;
                txtGSTNo.Text = string.Empty;
                txtTANNo.Text = string.Empty;
                txtCSTNo.Text = string.Empty;
                txtSerTax.Text = string.Empty;
                dtpVat.Text = string.Empty;
                dtpGSTDate.Text = string.Empty;
                dtpTANDate.Text = string.Empty;
                dtpCSTDate.Text = string.Empty;
                dtpSerTaxDate.Text = string.Empty;
                lblAadharPath.Text = string.Empty;
                lblPanPath.Text = string.Empty;
                lueRegSource.EditValue = null;
                chkActive.Checked = true;
                txtQbc.Text = string.Empty;
                chkFactory.Checked = false;
                ChkISOutSide.Checked = false;
                ChkisAutoConfirm.Checked = false;
                lueIDProof.EditValue = null;
                txtIDProofNo.Text = string.Empty;

                TabRegisterDetail.SelectedTabPageIndex = 0;

                txtPartyName.Focus();
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
        private void txtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnSave.Focus();
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void btnAadhar_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog OpenDialog = new OpenFileDialog();
                if (OpenDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }
                lblAadharPath.Text = OpenDialog.FileName;
                OpenDialog.Dispose();
                OpenDialog = null;

                if (File.Exists(lblAadharPath.Text) == false)
                {
                    Global.Message("File Is Not Exists To The Path");
                    return;
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void btnPan_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog OpenDialog = new OpenFileDialog();
                if (OpenDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }
                lblPanPath.Text = OpenDialog.FileName;
                OpenDialog.Dispose();
                OpenDialog = null;

                if (File.Exists(lblPanPath.Text) == false)
                {
                    Global.Message("File Is Not Exists To The Path");
                    return;
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void lblAadharPath_Click(object sender, EventArgs e)
        {
            //Global.CopyFormat(lblAadharPath.Text, txtPartyName.Text + "_AADHAR", Path.GetExtension(lblAadharPath.Text));
        }
        private void lblPanPath_Click(object sender, EventArgs e)
        {
            //Global.CopyFormat(lblPanPath.Text, txtPartyName.Text + "_PANCARD", Path.GetExtension(lblPanPath.Text));
        }
        private void btnAadharDownload_Click(object sender, EventArgs e)
        {
            try
            {
                btnAadharDownload.Tag = lblPanPath.Text;
                if (lblPanPath.Text != string.Empty)
                {
                    Global.CopyFormat(Convert.ToString(btnAadharDownload.Tag), txtPartyName.Text + "_AADHAR", Path.GetExtension(lblAadharPath.Text));
                }
                else
                {
                    Global.Message("File Is Not Exists To The Path");
                    return;
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void btnPanDownload_Click(object sender, EventArgs e)
        {
            try
            {
                btnPanDownload.Tag = lblPanPath.Text;
                if (lblPanPath.Text != string.Empty)
                {
                    Global.CopyFormat(Convert.ToString(btnPanDownload.Tag), txtPartyName.Text + "_PANCARD", Path.GetExtension(lblPanPath.Text));
                }
                else
                {
                    Global.Message("File Is Not Exists To The Path");
                    return;
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }

        #region GridEvents
        private void dgvRoughPartyMaster_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvRoughPartyMaster.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt64(Drow["rough_party_id"]);
                        txtPartyName.Text = Val.ToString(Drow["rough_party_name"]);
                        txtShortName.Text = Val.ToString(Drow["rough_party_shortname"]);
                        txtFName.Text = Val.ToString(Drow["first_name"]);
                        txtLName.Text = Val.ToString(Drow["last_name"]);
                        lueInitName.Text = Val.ToString(Drow["init_name"]);
                        txtEmailID.Text = Val.ToString(Drow["primary_email"]);
                        lueBusinessType.Text = Val.ToString(Drow["business_type"]);
                        luePartyType.Text = Val.ToString(Drow["party_type"]);
                        lueManager.EditValue = Val.ToInt32(Drow["manager_id"]);
                        lueCountry.EditValue = Val.ToInt32(Drow["country_id"]);
                        lueState.EditValue = Val.ToInt32(Drow["state_id"]);
                        lueCity.EditValue = Val.ToInt32(Drow["city_id"]);
                        txtRemark.Text = Val.ToString(Drow["remarks"]);
                        chkActive.Checked = Val.ToBoolean(Drow["active"]);
                        txtZipCode.Text = Val.ToString(Drow["pincode"]);
                        txtAddress.Text = Val.ToString(Drow["address"]);
                        txtPhone1.Text = Val.ToString(Drow["phone1"]);
                        txtFaxNo.Text = Val.ToString(Drow["fax"]);
                        txtMobileNo.Text = Val.ToString(Drow["mobile1"]);
                        lueMobCountry.EditValue = Val.ToInt32(Drow["mobcountry_id"]);
                        txtWebsite.Text = Val.ToString(Drow["website"]);
                        luePhnCountry.EditValue = Val.ToInt32(Drow["phncountry_id"]);
                        luePhnCity.EditValue = Val.ToInt32(Drow["phncity_id"]);
                        txtSecEmail.Text = Val.ToString(Drow["secondary_email"]);
                        txtDiscount.Text = Val.ToString(Drow["discount"]);
                        lueCategory.EditValue = Val.ToInt32(Drow["category_id"]);
                        txtAadharNo.Text = Val.ToString(Drow["aadhar_no"]);
                        txtPanCardNo.Text = Val.ToString(Drow["pancard_no"]);
                        txtTdsCircle.Text = Val.ToString(Drow["tds_circle"]);
                        lueRegSource.Text = Val.ToString(Drow["registration_source"]);
                        txtSequenceNo.Text = Val.ToString(Drow["sequence_no"]);
                        txtSerTax.Text = Val.ToString(Drow["service_tax_no"]);
                        dtpSerTaxDate.Text = Val.DBDate(Val.ToString(Drow["service_tax_date"]));
                        txtVatNo.Text = Val.ToString(Drow["vat_no"]);
                        dtpVat.Text = Val.DBDate(Val.ToString(Drow["vat_date"]));
                        txtTANNo.Text = Val.ToString(Drow["tan_no"]);
                        dtpTANDate.Text = Val.DBDate(Val.ToString(Drow["tan_date"]));
                        txtGSTNo.Text = Val.ToString(Drow["gst_no"]);
                        dtpGSTDate.Text = Val.DBDate(Val.ToString(Drow["gst_date"]));
                        txtCSTNo.Text = Val.ToString(Drow["cst_no"]);
                        dtpCSTDate.Text = Val.DBDate(Val.ToString(Drow["cst_date"]));
                        lblAadharPath.Text = Val.ToString(Drow["aadhar_path"]);
                        lblPanPath.Text = Val.ToString(Drow["pancard_path"]);
                        m_OldAadharPath = Val.ToString(Drow["aadhar_path"]);
                        m_OldPanPath = Val.ToString(Drow["pancard_path"]);
                        txtQbc.Text = Val.ToString(Drow["qbc"]);
                        chkFactory.Checked = Val.ToBoolean(Drow["factory"]);
                        ChkISOutSide.Checked = Val.ToBoolean(Drow["is_outside"]);
                        ChkisAutoConfirm.Checked = Val.ToBoolean(Drow["is_autoconfirm"]);
                        lueIDProof.EditValue = Val.ToInt(Drow["idproof_id"]);
                        txtIDProofNo.Text = Val.ToString(Drow["idproof_no"]);

                        txtPartyName.Focus();
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

        #region Functions
        private bool SaveDetails()
        {
            string gStrPath = string.Empty;
            string pathString = string.Empty;
            string srcpathAadhar = lblAadharPath.Text;
            string srcpathPan = lblPanPath.Text;

            bool blnReturn = true;
            RoughParty_MasterProperty RoughPartyMasterProperty = new RoughParty_MasterProperty();
            RoughPartyMaster objRoughParty = new RoughPartyMaster();
            int IntRes = 0;
            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }

                if (lueIDProof.Text == "" || txtIDProofNo.Text == string.Empty)
                {
                    DialogResult result = MessageBox.Show("ID Proof Not Found Do you want to save Party?", "Confirmation", MessageBoxButtons.YesNoCancel);
                    if (result != DialogResult.Yes)
                    {
                        btnSave.Enabled = true;
                        return false;
                    }
                }

                RoughPartyMasterProperty.rough_party_id = Val.ToInt32(lblMode.Tag);
                RoughPartyMasterProperty.rough_party_name = Val.ToString(txtPartyName.Text).ToUpper();
                RoughPartyMasterProperty.rough_party_shortname = Val.ToString(txtShortName.Text).ToUpper();
                RoughPartyMasterProperty.init_name = Val.ToString(lueInitName.EditValue).ToUpper();
                RoughPartyMasterProperty.first_name = Val.ToString(txtFName.Text).ToUpper();
                RoughPartyMasterProperty.last_name = Val.ToString(txtLName.Text).ToUpper();
                RoughPartyMasterProperty.primary_email = Val.ToString(txtEmailID.Text).ToUpper();
                RoughPartyMasterProperty.business_type = Val.ToString(lueBusinessType.EditValue).ToUpper();
                RoughPartyMasterProperty.party_type = Val.ToString(luePartyType.EditValue).ToUpper();
                RoughPartyMasterProperty.country_id = Val.ToInt32(lueCountry.EditValue);
                RoughPartyMasterProperty.city_id = Val.ToInt32(lueCity.EditValue);
                RoughPartyMasterProperty.state_id = Val.ToInt32(lueState.EditValue);
                RoughPartyMasterProperty.pincode = Val.ToString(txtZipCode.Text).ToUpper();
                RoughPartyMasterProperty.remarks = Val.ToString(txtRemark.Text).ToUpper();
                RoughPartyMasterProperty.active = Val.ToBoolean(chkActive.Checked);
                RoughPartyMasterProperty.address = Val.ToString(txtAddress.Text).ToUpper();
                RoughPartyMasterProperty.phone1 = Val.ToString(txtPhone1.Text).ToUpper();
                RoughPartyMasterProperty.fax = Val.ToString(txtFaxNo.Text).ToUpper();
                RoughPartyMasterProperty.mobile1 = Val.ToString(txtMobileNo.Text).ToUpper();
                RoughPartyMasterProperty.mobile1country = Val.ToString(lueMobCountry.EditValue).ToUpper();
                RoughPartyMasterProperty.website = Val.ToString(txtWebsite.Text).ToUpper();
                RoughPartyMasterProperty.phone1country = Val.ToString(luePhnCountry.EditValue).ToUpper();
                RoughPartyMasterProperty.phone1city = Val.ToString(luePhnCity.EditValue).ToUpper();
                RoughPartyMasterProperty.secondary_email = Val.ToString(txtSecEmail.Text).ToUpper();
                RoughPartyMasterProperty.discount = Val.ToInt32(txtDiscount.Text);
                RoughPartyMasterProperty.category_id = Val.ToInt32(lueCategory.EditValue);
                RoughPartyMasterProperty.aadhar_no = Val.ToString(txtAadharNo.Text).ToUpper();
                RoughPartyMasterProperty.pancard_no = Val.ToString(txtPanCardNo.Text).ToUpper();
                RoughPartyMasterProperty.tds_circle = Val.ToString(txtTdsCircle.Text).ToUpper();
                RoughPartyMasterProperty.registration_source = Val.ToString(lueRegSource.EditValue).ToUpper();
                RoughPartyMasterProperty.sequence_no = Val.ToInt32(txtSequenceNo.Text);
                RoughPartyMasterProperty.service_tax_no = Val.ToString(txtSerTax.Text).ToUpper();
                RoughPartyMasterProperty.service_tax_date = Val.DBDate(dtpSerTaxDate.Text).ToUpper();
                RoughPartyMasterProperty.vat_no = Val.ToString(txtVatNo.Text).ToUpper();
                RoughPartyMasterProperty.vat_date = Val.DBDate(dtpVat.Text).ToUpper();
                RoughPartyMasterProperty.gst_no = Val.ToString(txtGSTNo.Text).ToUpper();
                RoughPartyMasterProperty.gst_date = Val.DBDate(dtpGSTDate.Text).ToUpper();
                RoughPartyMasterProperty.cst_no = Val.ToString(txtCSTNo.Text).ToUpper();
                RoughPartyMasterProperty.cst_date = Val.DBDate(dtpCSTDate.Text).ToUpper();
                RoughPartyMasterProperty.tan_no = Val.ToString(txtTANNo.Text).ToUpper();
                RoughPartyMasterProperty.tan_date = Val.DBDate(dtpTANDate.Text).ToUpper();
                RoughPartyMasterProperty.aadhar_path = Val.ToString(lblAadharPath.Text).ToUpper();
                RoughPartyMasterProperty.pan_path = Val.ToString(lblPanPath.Text).ToUpper();
                RoughPartyMasterProperty.qbc = Val.ToString(txtQbc.Text);
                RoughPartyMasterProperty.factory = Val.ToBoolean(chkFactory.Checked);
                RoughPartyMasterProperty.is_outside = Val.ToBoolean(ChkISOutSide.Checked);
                RoughPartyMasterProperty.is_autoconfirm = Val.ToBoolean(ChkisAutoConfirm.Checked);
                RoughPartyMasterProperty.manager_id = Val.ToInt32(lueManager.EditValue);

                RoughPartyMasterProperty.IDProof_ID = Val.ToInt32(lueIDProof.EditValue);
                RoughPartyMasterProperty.IDProof_No = Val.ToString(txtIDProofNo.Text).ToUpper();


                DataTable p_dtbPartyId = new DataTable();
                p_dtbPartyId = objRoughParty.Save(RoughPartyMasterProperty);
                if (p_dtbPartyId.Rows.Count > 0 && (Convert.ToString(srcpathAadhar) != string.Empty || Convert.ToString(srcpathPan) != string.Empty))
                {
                    RoughPartyMasterProperty.rough_party_id = Convert.ToInt32(p_dtbPartyId.Rows[0][0]);
                    if (Convert.ToString(srcpathAadhar) != string.Empty)
                    {
                        RoughPartyMasterProperty.aadhar_path = pathString + "\\" + RoughPartyMasterProperty.rough_party_id + "_AADHAR" + Path.GetExtension(srcpathAadhar);
                    }
                    if (Convert.ToString(srcpathPan) != string.Empty)
                    {
                        RoughPartyMasterProperty.pan_path = pathString + "\\" + RoughPartyMasterProperty.rough_party_id + "_PANCARD" + Path.GetExtension(srcpathPan);
                    }
                    IntRes = objRoughParty.Update(RoughPartyMasterProperty);
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) > 0)
                    {

                        if (string.Equals(m_OldAadharPath, Val.ToString(lblAadharPath.Text)) == false)
                        {
                            RoughPartyMasterProperty.aadhar_path = null;

                            File.Delete(pathString + "\\" + RoughPartyMasterProperty.rough_party_id + "_AADHAR" + Path.GetExtension(m_OldAadharPath));
                            RoughPartyMasterProperty.rough_party_id = Convert.ToInt32(Val.ToInt(lblMode.Tag));
                            if (Convert.ToString(srcpathAadhar) != string.Empty)
                            {
                                RoughPartyMasterProperty.aadhar_path = pathString + "\\" + RoughPartyMasterProperty.rough_party_id + "_AADHAR" + Path.GetExtension(srcpathAadhar);
                            }


                        }
                        if (string.Equals(m_OldPanPath, Val.ToString(lblPanPath.Text)) == false)
                        {
                            RoughPartyMasterProperty.pan_path = null;
                            File.Delete(pathString + "\\" + RoughPartyMasterProperty.rough_party_id + "_PANCARD" + Path.GetExtension(m_OldPanPath));
                            if (Convert.ToString(srcpathPan) != string.Empty)
                            {
                                RoughPartyMasterProperty.pan_path = pathString + "\\" + RoughPartyMasterProperty.rough_party_id + "_PANCARD" + Path.GetExtension(srcpathPan);
                            }
                        }
                        IntRes = objRoughParty.Update(RoughPartyMasterProperty);
                        //if (string.Equals(m_OldPanPath, Val.ToString(txtPanPath.Text)) == false)
                        //{
                        //    File.Delete(pathString + "\\" + BrokerMasterProperty.broker_id + "_PANCARD" + Path.GetExtension(srcpathPan));
                        //}
                    }
                }
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Save Rough Party Details");
                    TabRegisterDetail.SelectedTabPageIndex = 0;
                    txtPartyName.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        if (srcpathAadhar != string.Empty)
                        {
                            File.Copy(srcpathAadhar, pathString + "\\" + RoughPartyMasterProperty.rough_party_id + "_AADHAR" + Path.GetExtension(srcpathAadhar));
                        }
                        if (srcpathPan != string.Empty)
                        {
                            File.Copy(srcpathPan, pathString + "\\" + RoughPartyMasterProperty.rough_party_id + "_PANCARD" + Path.GetExtension(srcpathPan));
                        }
                        Global.Confirm("Rough Party Details Data Save Successfully");
                        TabRegisterDetail.SelectedTabPageIndex = 0;
                        txtPartyName.Focus();
                    }
                    else
                    {
                        if (string.Equals(m_OldAadharPath, Val.ToString(lblAadharPath.Text)) == false)
                        {
                            if (srcpathAadhar != string.Empty)
                            {
                                File.Copy(srcpathAadhar, pathString + "\\" + RoughPartyMasterProperty.rough_party_id + "_AADHAR" + Path.GetExtension(srcpathAadhar));
                            }

                        }
                        if (string.Equals(m_OldPanPath, Val.ToString(lblPanPath.Text)) == false)
                        {
                            if (srcpathPan != string.Empty)
                            {
                                File.Copy(srcpathPan, pathString + "\\" + RoughPartyMasterProperty.rough_party_id + "_PANCARD" + Path.GetExtension(srcpathPan));
                            }
                        }
                        Global.Confirm("Rough Party Details Data Update Successfully");
                        TabRegisterDetail.SelectedTabPageIndex = 0;
                        txtPartyName.Focus();
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
                RoughPartyMasterProperty = null;
            }

            return blnReturn;
        }
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (txtPartyName.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Rough Party Name"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtPartyName.Focus();
                    }
                }

                if (txtFName.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "First Name"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtFName.Focus();
                    }
                }

                if (lueInitName.Text == "")
                {
                    lstError.Add(new ListError(13, "Intial Name"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueInitName.Focus();
                    }
                }
                if (lueBusinessType.Text == "")
                {
                    lstError.Add(new ListError(13, "Bussiness Type"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueBusinessType.Focus();
                    }
                }
                if (luePartyType.Text == "")
                {
                    lstError.Add(new ListError(13, "Party Type"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        luePartyType.Focus();
                    }
                }
                if (lueCountry.Text == "")
                {
                    lstError.Add(new ListError(13, "Country"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueCountry.Focus();
                    }
                }
                if (lueState.Text == "")
                {
                    lstError.Add(new ListError(13, "State"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueState.Focus();
                    }
                }
                if (lueCity.Text == "")
                {
                    lstError.Add(new ListError(13, "City"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueCity.Focus();
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
                DataTable DTab = objRoughParty.GetData();
                grdRoughPartyMaster.DataSource = DTab;
                dgvRoughPartyMaster.BestFitColumns();
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
                            dgvRoughPartyMaster.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvRoughPartyMaster.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvRoughPartyMaster.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvRoughPartyMaster.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvRoughPartyMaster.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvRoughPartyMaster.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvRoughPartyMaster.ExportToCsv(Filepath);
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
    }
}
