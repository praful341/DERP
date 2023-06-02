using BLL;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Transaction;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DREP.Master
{
    public partial class FrmWeightLoss : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member
        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        Control _NextEnteredControl;
        private List<Control> _tabControls;

        WeightLoss objWeightLoss;
        Weight_LossProperty WeightLossProperty;
        AssortMaster objAssort;
        SieveMaster objSieve;

        DataTable DtControlSettings = new DataTable();
        DataTable m_dtbAssorts = new DataTable();
        DataTable m_dtbSieve = new DataTable();

        int IntRes = 0;
        int m_numForm_id = 0;

        #endregion

        #region Constructor

        public FrmWeightLoss()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            _NextEnteredControl = new Control();
            _tabControls = new List<Control>();

            objWeightLoss = new WeightLoss();
            WeightLossProperty = new Weight_LossProperty();
            objAssort = new AssortMaster();
            objSieve = new SieveMaster();

            DtControlSettings = new DataTable();
            m_dtbAssorts = new DataTable();
            m_dtbSieve = new DataTable();

            IntRes = 0;
            m_numForm_id = 0;
        }
        public void ShowForm()
        {
            ObjPer.FormName = this.Name.ToUpper();
            m_numForm_id = ObjPer.form_id;
            if (ObjPer.CheckPermission() == false)
            {
                Global.Message(BLL.GlobalDec.gStrPermissionViwMsg);
                return;
            }
            Val.frmGenSet(this);
            AttachFormEvents();

            // for Dynamic Setting By Praful On 01022020

            if (Global.HideFormControls(Val.ToInt(ObjPer.form_id), this) != "")
            {
                Global.Message("Select First User Setting...Please Contact to Administrator...");
                return;
            }

            ControlSettingDT(Val.ToInt(ObjPer.form_id), this);
            AddGotFocusListener(this);
            this.KeyPreview = true;

            TabControlsToList(this.Controls);
            _tabControls = _tabControls.OrderBy(x => x.TabIndex).ToList();

            // End for Dynamic Setting By Praful On 01022020

            this.Show();
        }

        private void AddGotFocusListener(Control ctrl)
        {
            foreach (Control c in ctrl.Controls)
            {
                c.GotFocus += new EventHandler(Control_GotFocus);
                if (c.Controls.Count > 0)
                {
                    AddGotFocusListener(c);
                }
            }
        }
        private void Control_GotFocus(object sender, EventArgs e)
        {
            if (!((Control)sender).Name.ToString().Trim().Equals(string.Empty))
            {
                _NextEnteredControl = (Control)sender;
                if ((Control)sender is LookUpEdit)
                {
                    ((LookUpEdit)(Control)sender).ShowPopup();
                }
            }
        }

        private void TabControlsToList(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control.TabStop)
                    _tabControls.Add(control);
                if (control.HasChildren)
                    TabControlsToList(control.Controls);
            }
        }
        private void ControlSettingDT(int FormCode, Form pForm)
        {
            BLL.Validation Val = new BLL.Validation();
            Single_Setting ObjSingleSettings = new Single_Setting();
            Single_SettingProperty Property = new Single_SettingProperty();

            Property.role_id = Val.ToInt(BLL.GlobalDec.gEmployeeProperty.role_id);
            Property.form_id = Val.ToInt(FormCode);
            DataTable DtColSetting = ObjSingleSettings.GetData(Property);

            DataTable DtFilterColSetting = (from DataRow dr in DtColSetting.Rows
                                            where Val.ToBooleanToInt(dr["is_control"]) == 1
                                            && dr["column_type"].ToString() != "LABEL"
                                            select dr).CopyToDataTable();
            DevExpress.XtraLayout.LayoutControl l = new DevExpress.XtraLayout.LayoutControl();
            l.OptionsFocus.EnableAutoTabOrder = false;

            if (DtFilterColSetting.Rows.Count > 0)
            {
                DtControlSettings = DtFilterColSetting;
                foreach (Control item1 in pForm.Controls)
                {
                    ControllSettings(item1, DtFilterColSetting);
                }
            }
        }

        private static void ControllSettings(Control item2, DataTable DTab)
        {
            BLL.Validation Val = new BLL.Validation();

            //else
            {
                var VarControlSetting = (from DataRow dr in DTab.Rows
                                         where dr["column_name"].ToString() == item2.Name.ToString()
                                         select dr);

                if (VarControlSetting.Count() > 0)
                {
                    DataRow DRow = VarControlSetting.CopyToDataTable().Rows[0];
                    if (item2.Name.ToString() == Val.ToString(DRow["column_name"]))
                    {
                        if (!(item2 is TextEdit))
                        {
                            if (!(item2 is DateTimePicker))
                            {
                                if (!(item2 is DevExpress.XtraEditors.TextEdit))
                                {
                                    item2.Text = (Val.ToBooleanToInt(DRow["is_compulsory"]).Equals(0) ? Val.ToString(DRow["caption"]) : "* " + Val.ToString(DRow["caption"]));
                                }
                            }
                        }
                        if (Val.ToInt(DRow["tab_index"]) >= 0)
                        {
                            if (item2.CanSelect)
                                item2.TabStop = true;
                        }
                        else
                            item2.TabStop = false;
                        if (Val.ToBooleanToInt(DRow["is_visible"]).Equals(1))
                        {
                            item2.Visible = true;
                            item2.TabStop = true;
                        }
                        else
                        {
                            item2.Visible = false;
                            item2.TabStop = false;
                        }

                        item2.TabIndex = Val.ToInt(DRow["tab_index"]);
                        if (item2.TabIndex == 1)
                        {
                            item2.Select();
                            item2.Focus();
                        }
                        if (Val.ToBooleanToInt(DRow["is_editable"]).Equals(1))
                        {
                            item2.Enabled = true;
                        }
                        else
                        {
                            item2.Enabled = false;
                        }
                    }
                }
                else
                {
                    item2.TabStop = false;
                }
            }
            if (item2.Controls.Count > 0)
            {
                foreach (Control item1 in item2.Controls)
                {
                    ControllSettings(item1, DTab);
                }
            }
        }

        private void AttachFormEvents()
        {
            objBOFormEvents.CurForm = this;
            objBOFormEvents.FormKeyPress = true;
            objBOFormEvents.FormKeyDown = true;
            objBOFormEvents.FormResize = true;
            objBOFormEvents.FormClosing = true;
            objBOFormEvents.ObjToDispose.Add(objWeightLoss);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }

        #endregion

        #region Events       
        private void FrmUserMaster_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDefaults();
                GetData();
                btnClear_Click(btnClear, null);
                dtpLossDate.Focus();

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

            if (!ValidateDetails())
            {
                btnSave.Enabled = true;
                return;
            }

            DialogResult result = MessageBox.Show("Do you want to save data?", "Confirmation", MessageBoxButtons.YesNoCancel);
            if (result != DialogResult.Yes)
            {
                btnSave.Enabled = true;
                return;
            }

            DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
            backgroundWorker_WeightLoss.RunWorkerAsync();
            btnSave.Enabled = true;
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearDetails();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txtLossCarat_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtLossAmount.Text = string.Format("{0:0.00}", Val.ToDecimal(txtLossCarat.Text) * Val.ToDecimal(txtLossRate.Text));
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void txtLossRate_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtLossAmount.Text = string.Format("{0:0.00}", Val.ToDecimal(txtLossCarat.Text) * Val.ToDecimal(txtLossRate.Text));
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void txtCaratPluse_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtAmountPluese.Text = string.Format("{0:0.00}", Val.ToDecimal(txtCaratPluse.Text) * Val.ToDecimal(txtRatePluse.Text));
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void txtRatePluse_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtAmountPluese.Text = string.Format("{0:0.00}", Val.ToDecimal(txtCaratPluse.Text) * Val.ToDecimal(txtRatePluse.Text));
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void txtLostCarat_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtLostAmount.Text = string.Format("{0:0.00}", Val.ToDecimal(txtLostCarat.Text) * Val.ToDecimal(txtLostRate.Text));
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void txtLostRate_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtLostAmount.Text = string.Format("{0:0.00}", Val.ToDecimal(txtLostCarat.Text) * Val.ToDecimal(txtLostRate.Text));
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void backgroundWorker_WeightLoss_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                IntRes = 0;
                if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                {
                    Conn = new BeginTranConnection(true, false);
                }
                else
                {
                    Conn = new BeginTranConnection(false, true);
                }
                WeightLossProperty = new Weight_LossProperty();

                WeightLossProperty.loss_id = Val.ToInt32(lblMode.Tag);
                WeightLossProperty.assort_id = Val.ToInt32(lueAssortName.EditValue);
                WeightLossProperty.sieve_id = Val.ToInt32(lueSieveName.EditValue);

                WeightLossProperty.company_id = Val.ToInt(GlobalDec.gEmployeeProperty.company_id);
                WeightLossProperty.branch_id = Val.ToInt(GlobalDec.gEmployeeProperty.branch_id);
                WeightLossProperty.location_id = Val.ToInt(GlobalDec.gEmployeeProperty.location_id);
                WeightLossProperty.department_id = Val.ToInt(GlobalDec.gEmployeeProperty.department_id);

                WeightLossProperty.loss_date = Val.ToString(dtpLossDate.Text);
                WeightLossProperty.loss_carat = Val.ToDecimal(txtLossCarat.Text);
                WeightLossProperty.loss_rate = Val.ToDecimal(txtLossRate.Text);
                WeightLossProperty.loss_amount = Val.ToDecimal(txtLossAmount.Text);
                WeightLossProperty.lost_carat = Val.ToDecimal(txtLostCarat.Text);
                WeightLossProperty.lost_rate = Val.ToDecimal(txtLostRate.Text);
                WeightLossProperty.lost_amount = Val.ToDecimal(txtLostAmount.Text);
                WeightLossProperty.pluse_carat = Val.ToDecimal(txtCaratPluse.Text);
                WeightLossProperty.pluse_rate = Val.ToDecimal(txtRatePluse.Text);
                WeightLossProperty.pluse_amount = Val.ToDecimal(txtAmountPluese.Text);
                WeightLossProperty.remarks = Val.ToString(txtRemark.Text);
                WeightLossProperty.special_remarks = Val.ToString(txtSpecialRemark.Text);
                WeightLossProperty.form_id = m_numForm_id;

                IntRes = objWeightLoss.Save(WeightLossProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                {
                    Conn.Inter1.Commit();
                }
                else
                {
                    Conn.Inter2.Commit();
                }
            }
            catch (Exception ex)
            {
                IntRes = -1;
                if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                {
                    Conn.Inter1.Rollback();
                }
                else
                {
                    Conn.Inter2.Rollback();
                }
                Conn = null;
                General.ShowErrors(ex.ToString());
                return;
            }
            finally
            {
                WeightLossProperty = null;
            }
        }
        private void backgroundWorker_WeightLoss_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (IntRes > 0)
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Weight Loss Data Save Successfully");
                    }
                    else
                    {
                        Global.Confirm("Weight Loss Data Update Successfully");
                    }
                    GetData();
                    ClearDetails();
                }
                else
                {
                    Global.Confirm("Error In Save Weight Loss");
                    txtLossCarat.Focus();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }
        #region GridEvents
        private void dgvWeightLoss_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvWeightLoss.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt32(Drow["weight_loss_id"]);
                        dtpLossDate.Text = Val.DBDate(Drow["weight_loss_date"].ToString());
                        lueAssortName.EditValue = Val.ToInt(Drow["assort_id"]);
                        lueSieveName.EditValue = Val.ToInt(Drow["sieve_id"]);

                        txtLossCarat.Text = Val.ToString(Drow["weight_loss_carat"]);
                        txtLossRate.Text = Val.ToString(Drow["weight_loss_rate"]);
                        txtLossAmount.Text = Val.ToString(Drow["weight_loss_amount"]);

                        txtLostCarat.Text = Val.ToString(Drow["lost_carat"]);
                        txtLostRate.Text = Val.ToString(Drow["lost_rate"]);
                        txtLostAmount.Text = Val.ToString(Drow["lost_amount"]);

                        txtCaratPluse.Text = Val.ToString(Drow["weight_plus_carat"]);
                        txtRatePluse.Text = Val.ToString(Drow["weight_plus_rate"]);
                        txtAmountPluese.Text = Val.ToString(Drow["weight_plus_amount"]);

                        txtRemark.Text = Val.ToString(Drow["remarks"]);
                        txtSpecialRemark.Text = Val.ToString(Drow["special_remarks"]);
                        btnSave.Enabled = false;
                        dtpLossDate.Focus();
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
        private bool LoadDefaults()
        {
            bool blnReturn = true;
            try
            {
                m_dtbAssorts = objAssort.GetData(1);
                lueAssortName.Properties.DataSource = m_dtbAssorts;
                lueAssortName.Properties.ValueMember = "assort_id";
                lueAssortName.Properties.DisplayMember = "assort_name";

                m_dtbSieve = objSieve.GetData(1);
                lueSieveName.Properties.DataSource = m_dtbSieve;
                lueSieveName.Properties.ValueMember = "sieve_id";
                lueSieveName.Properties.DisplayMember = "sieve_name";

                dtpLossDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpLossDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpLossDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpLossDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpLossDate.EditValue = DateTime.Now;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            finally
            {
                objAssort = null;
                objSieve = null;
            }

            return blnReturn;
        }
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (dtpLossDate.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Loss Date"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        dtpLossDate.Focus();
                    }
                }

                if (lueAssortName.Text == "")
                {
                    lstError.Add(new ListError(13, "Assort"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueAssortName.Focus();
                    }
                }
                if (lueSieveName.Text == "")
                {
                    lstError.Add(new ListError(13, "Sieve"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueSieveName.Focus();
                    }
                }
                var result = DateTime.Compare(Convert.ToDateTime(dtpLossDate.Text), DateTime.Today);
                if (result > 0)
                {
                    lstError.Add(new ListError(5, "Loss Date Not Be Greater Than Today Date"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        dtpLossDate.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                lstError.Add(new ListError(ex));
            }
            return (!(BLL.General.ShowErrors(lstError)));

        }
        private bool SaveDetails()
        {
            bool blnReturn = true;

            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }



            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                blnReturn = false;
            }
            finally
            {
                WeightLossProperty = null;
            }

            return blnReturn;
        }
        private bool ClearDetails()
        {
            bool blnReturn = true;
            try
            {
                lblMode.Tag = 0;
                lblMode.Text = "Add Mode";

                txtLossCarat.Text = string.Empty;
                txtLossRate.Text = string.Empty;
                txtLossAmount.Text = string.Empty;

                txtLostCarat.Text = string.Empty;
                txtLostRate.Text = string.Empty;
                txtLostAmount.Text = string.Empty;

                txtCaratPluse.Text = string.Empty;
                txtRatePluse.Text = string.Empty;
                txtAmountPluese.Text = string.Empty;

                txtRemark.Text = string.Empty;
                txtSpecialRemark.Text = string.Empty;
                btnSave.Enabled = true;

                lueAssortName.EditValue = System.DBNull.Value;
                lueSieveName.EditValue = System.DBNull.Value;

                dtpLossDate.Focus();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            finally
            {

            }

            return blnReturn;
        }
        public void GetData()
        {
            try
            {
                DataTable DTab = objWeightLoss.GetData();
                grdWeightLoss.DataSource = DTab;
                dgvWeightLoss.BestFitColumns();
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
                            dgvWeightLoss.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvWeightLoss.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvWeightLoss.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvWeightLoss.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvWeightLoss.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvWeightLoss.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvWeightLoss.ExportToCsv(Filepath);
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
