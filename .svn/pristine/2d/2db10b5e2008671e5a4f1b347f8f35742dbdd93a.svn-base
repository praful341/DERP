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

namespace DREP.Master.MFG
{
    public partial class FrmMfgRejectionBrokerMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.FormEvents objBOFormEvents = new BLL.FormEvents();
        BLL.Validation Val = new BLL.Validation();
        BLL.FormPer ObjPer = new BLL.FormPer();
        RejectionBrokerMaster objRejectionBroker = new RejectionBrokerMaster();
        string m_OldAadharPath = null;
        string m_OldPanPath = null;

        #endregion

        #region Constructor
        public FrmMfgRejectionBrokerMaster()
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
        }
        private void AttachFormEvents()
        {
            objBOFormEvents.CurForm = this;
            objBOFormEvents.FormKeyPress = true;
            objBOFormEvents.FormKeyDown = true;
            objBOFormEvents.FormResize = true;
            objBOFormEvents.FormClosing = true;
            objBOFormEvents.ObjToDispose.Add(objRejectionBroker);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }

        #endregion

        #region Events 
        private void FrmBrokerMaster_Load(object sender, EventArgs e)
        {
            try
            {
                GetData();
                btnClear_Click(btnClear, null);
                txtBrokerType.Focus();

                Global.LOOKUPCountry(lueCountry);
                Global.LOOKUPState(lueState);
                Global.LOOKUPCity(lueCity);
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
                txtBrokerType.Text = string.Empty;
                txtBrokerName.Text = string.Empty;
                txtBrokerage.Text = string.Empty;
                txtAddress.Text = string.Empty;
                txtPinCode.Text = string.Empty;
                txtRemark.Text = string.Empty;
                lueState.EditValue = null;
                lueCountry.EditValue = null;
                lueCity.EditValue = null;
                txtPhone.Text = string.Empty;
                txtOfficeExtNo.Text = string.Empty;
                txtFax.Text = string.Empty;
                txtEmail.Text = string.Empty;
                chkActive.Checked = true;
                txtAadharNo.Text = string.Empty;
                txtPanCardNo.Text = string.Empty;
                lblAadharPath.Text = string.Empty;
                lblPanPath.Text = string.Empty;
                txtBrokerType.Focus();
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }

        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
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
                Global.Message(ex.ToString());
                return;
            }

        }
        private void btnAadhar_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenDialog = new OpenFileDialog();
            try
            {
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
                Global.Message(ex.ToString());
                return;
            }

        }
        private void btnPan_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenDialog = new OpenFileDialog();
            try
            {
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
                Global.Message(ex.ToString());
                return;
            }

        }
        private void btnAadharDownload_Click(object sender, EventArgs e)
        {
            btnAadharDownload.Tag = lblPanPath.Text;
            try
            {
                if (lblPanPath.Text != string.Empty)
                {
                    Global.CopyFormat(Convert.ToString(btnAadharDownload.Tag), txtBrokerName.Text + "_AADHAR", Path.GetExtension(lblAadharPath.Text));
                }
                else
                {
                    Global.Message("File Is Not Exists To The Path");
                    return;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }

        }
        private void btnPanDownload_Click(object sender, EventArgs e)
        {
            btnPanDownload.Tag = lblPanPath.Text;
            try
            {
                if (lblPanPath.Text != string.Empty)
                {
                    Global.CopyFormat(Convert.ToString(btnPanDownload.Tag), txtBrokerName.Text + "_PANCARD", Path.GetExtension(lblPanPath.Text));
                }
                else
                {
                    Global.Message("File Is Not Exists To The Path");
                    return;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }

        }

        #region GridEvents

        #endregion

        #endregion

        #region Functions
        private bool SaveDetails()
        {
            //string gStrPath = System.Configuration.ConfigurationManager.AppSettings["ServerPath"].ToString(); //iniCl.IniReadValue("LOGIN", "ServerPath");
            //string pathString = System.IO.Path.Combine(gStrPath, "Broker Document");
            //System.IO.Directory.CreateDirectory(pathString);

            string srcpathAadhar = lblAadharPath.Text;
            string srcpathPan = lblPanPath.Text;

            bool blnReturn = true;
            RejectionBroker_MasterProperty RejectionBrokerMasterProperty = new RejectionBroker_MasterProperty();
            RejectionBrokerMaster objRejectionBroker = new RejectionBrokerMaster();
            int IntRes = 0;
            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }

                RejectionBrokerMasterProperty.rejection_broker_id = Val.ToInt32(lblMode.Tag);
                RejectionBrokerMasterProperty.rejection_broker_type = txtBrokerType.Text.ToUpper();
                RejectionBrokerMasterProperty.rejection_broker_name = Val.ToString(txtBrokerName.Text).ToUpper();
                RejectionBrokerMasterProperty.brokerage = Val.ToInt32(txtBrokerage.Text);
                RejectionBrokerMasterProperty.address = Val.ToString(txtAddress.Text).ToUpper();
                RejectionBrokerMasterProperty.pincode = Val.ToString(txtPinCode.Text);
                RejectionBrokerMasterProperty.remarks = Val.ToString(txtRemark.Text).ToUpper();
                RejectionBrokerMasterProperty.city_id = Val.ToInt32(lueCity.EditValue);
                RejectionBrokerMasterProperty.state_id = Val.ToInt32(lueState.EditValue);
                RejectionBrokerMasterProperty.country_id = Val.ToInt32(lueCountry.EditValue);
                RejectionBrokerMasterProperty.phone = Val.ToString(txtPhone.Text).ToUpper();
                RejectionBrokerMasterProperty.mobile = Val.ToString(txtOfficeExtNo.Text);
                RejectionBrokerMasterProperty.fax = Val.ToString(txtFax.Text).ToUpper();
                RejectionBrokerMasterProperty.email = Val.ToString(txtEmail.Text).ToUpper();
                RejectionBrokerMasterProperty.aadhar_no = Val.ToString(txtAadharNo.Text).ToUpper();
                RejectionBrokerMasterProperty.pan_no = Val.ToString(txtPanCardNo.Text).ToUpper();
                RejectionBrokerMasterProperty.aadhar_path = Val.ToString(lblAadharPath.Text).ToUpper();
                RejectionBrokerMasterProperty.pan_path = Val.ToString(lblPanPath.Text).ToUpper();
                RejectionBrokerMasterProperty.active = Val.ToBoolean(chkActive.Checked);
                RejectionBrokerMasterProperty.remarks = Val.ToString(txtRemark.Text).ToUpper();

                DataTable p_dtbBrokerId = new DataTable();
                p_dtbBrokerId = objRejectionBroker.Save(RejectionBrokerMasterProperty);

                if (IntRes == -1)
                {
                    Global.Confirm("Error In Save Rejection Broker Master Details");
                    txtBrokerType.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Rejection Broker Details Data Save Successfully");
                    }
                    else
                    {
                        Global.Confirm("Rejection Broker Details Data Update Successfully");
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
                RejectionBrokerMasterProperty = null;
            }
            return blnReturn;
        }
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                //if (txtBrokerType.Text == string.Empty)
                //{
                //    lstError.Add(new ListError(12, "Broker Type"));
                //    if (!blnFocus)
                //    {
                //        blnFocus = true;
                //        txtBrokerType.Focus();
                //    }
                //}

                if (txtBrokerName.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Broker Name"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtBrokerName.Focus();
                    }
                }

                if (!objRejectionBroker.ISExists(txtBrokerName.Text, Val.ToInt64(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
                {
                    lstError.Add(new ListError(23, "Rejection Broker Name"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtBrokerName.Focus();
                    }
                }

                //if (txtBrokerage.Text == string.Empty)
                //{
                //    lstError.Add(new ListError(12, "Brokerage"));
                //    if (!blnFocus)
                //    {
                //        blnFocus = true;
                //        txtBrokerage.Focus();
                //    }
                //}
                //if (txtAadharNo.Text == string.Empty && txtPanCardNo.Text == string.Empty)
                //{
                //    lstError.Add(new ListError(12, "Aadhar No / Pancard No"));
                //    if (!blnFocus)
                //    {
                //        blnFocus = true;
                //        txtAadharNo.Focus();
                //    }
                //}

                //if (txtAadharNo.Text != string.Empty)
                //{
                //    if (txtAadharNo.Text.Length < 12 || txtAadharNo.Text.Length > 12)
                //    {
                //        lstError.Add(new ListError(5, "Aadhar No Must Be Enter 12 Digit"));
                //        if (!blnFocus)
                //        {
                //            blnFocus = true;
                //            txtAadharNo.Focus();
                //        }
                //    }
                //}

                //if (txtPanCardNo.Text != string.Empty)
                //{
                //    if (txtPanCardNo.Text.Length < 10 || txtPanCardNo.Text.Length > 10)
                //    {
                //        lstError.Add(new ListError(5, "Pan No Must Be Enter 10 Digit"));
                //        if (!blnFocus)
                //        {
                //            blnFocus = true;
                //            txtPanCardNo.Focus();
                //        }
                //    }
                //}
                //if (lueCountry.Text == "")
                //{
                //    lstError.Add(new ListError(13, "Country"));
                //    if (!blnFocus)
                //    {
                //        blnFocus = true;
                //        lueCountry.Focus();
                //    }
                //}
                //if (lueState.Text == "")
                //{
                //    lstError.Add(new ListError(13, "State"));
                //    if (!blnFocus)
                //    {
                //        blnFocus = true;
                //        lueState.Focus();
                //    }
                //}
                //if (lueCity.Text == "")
                //{
                //    lstError.Add(new ListError(13, "City"));
                //    if (!blnFocus)
                //    {
                //        blnFocus = true;
                //        lueCity.Focus();
                //    }
                //}
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
                DataTable DTab = objRejectionBroker.GetData();
                grdRejectionBrokerMaster.DataSource = DTab;
                dgvRejectionBrokerMaster.BestFitColumns();
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
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
                            dgvRejectionBrokerMaster.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvRejectionBrokerMaster.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvRejectionBrokerMaster.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvRejectionBrokerMaster.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvRejectionBrokerMaster.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvRejectionBrokerMaster.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvRejectionBrokerMaster.ExportToCsv(Filepath);
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
            Export("xlsx", "Export to Excel", "Excel files 97-2003 (Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*");
        }
        private void MNExportPDF_Click(object sender, EventArgs e)
        {
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

        private void dgvRejectionBrokerMaster_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvRejectionBrokerMaster.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt64(Drow["rejection_broker_id"]);
                        txtBrokerType.Text = Val.ToString(Drow["rejection_broker_type"]);
                        txtBrokerName.Text = Val.ToString(Drow["rejection_broker_name"]);
                        txtBrokerage.Text = Val.ToString(Val.ToDecimal(Drow["brokerage"]));
                        lueCountry.EditValue = Val.ToInt32(Drow["country_id"]);
                        lueState.EditValue = Val.ToInt32(Drow["state_id"]);
                        lueCity.EditValue = Val.ToInt32(Drow["city_id"]);
                        txtAddress.Text = Val.ToString(Drow["address"]);
                        txtPinCode.Text = Val.ToString(Drow["pincode"]);
                        txtRemark.Text = Val.ToString(Drow["remarks"]);
                        txtPhone.Text = Val.ToString(Drow["phone"]);
                        txtOfficeExtNo.Text = Val.ToString(Drow["mobile"]);
                        txtFax.Text = Val.ToString(Drow["fax"]);
                        txtEmail.Text = Val.ToString(Drow["email"]);
                        chkActive.Checked = Val.ToBoolean(Drow["active"]);
                        txtAadharNo.Text = Val.ToString(Drow["aadhar_no"]);
                        txtPanCardNo.Text = Val.ToString(Drow["pancard_no"]);
                        lblAadharPath.Text = Val.ToString(Drow["aadhar_path"]);
                        lblPanPath.Text = Val.ToString(Drow["pancard_path"]);
                        m_OldAadharPath = Val.ToString(Drow["aadhar_path"]);
                        m_OldPanPath = Val.ToString(Drow["pancard_path"]);
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
