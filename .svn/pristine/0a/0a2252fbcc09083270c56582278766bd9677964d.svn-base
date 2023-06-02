using BLL;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Master.HR;
using BLL.FunctionClasses.Transaction;
using BLL.PropertyClasses.Master.HR;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DREP.Master.HR
{
    public partial class FrmHRRateMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        Control _NextEnteredControl;
        private List<Control> _tabControls;

        BLL.FormEvents objBOFormEvents = new BLL.FormEvents();
        BLL.Validation Val = new BLL.Validation();
        BLL.FormPer ObjPer = new BLL.FormPer();
        HRRateMaster objHRRate = new HRRateMaster();
        RateTypeMaster ObjRateType = new RateTypeMaster();
        DataTable DtControlSettings;
        int IntRes;

        #endregion

        #region Constructor
        public FrmHRRateMaster()
        {
            InitializeComponent();

            _NextEnteredControl = new Control();
            _tabControls = new List<Control>();
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

            if (Global.HideFormControls(Val.ToInt(ObjPer.form_id), this) != "")
            {
                Global.Message("Select First User Setting...Please Contact to Administrator...");
                return;
            }

            ControlSettingDT(Val.ToInt(ObjPer.form_id), this);
            AddGotFocusListener(this);
            //AddKeyPressListener(this);
            this.KeyPreview = true;

            TabControlsToList(this.Controls);
            _tabControls = _tabControls.OrderBy(x => x.TabIndex).ToList();

            this.Show();
        }
        private void AttachFormEvents()
        {
            objBOFormEvents.CurForm = this;
            objBOFormEvents.FormKeyPress = true;
            objBOFormEvents.FormKeyDown = true;
            objBOFormEvents.FormResize = true;
            objBOFormEvents.FormClosing = true;
            objBOFormEvents.ObjToDispose.Add(objHRRate);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
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
        private void AddKeyPressListener(Control ctrl)
        {
            foreach (Control c in ctrl.Controls)
            {
                c.KeyPress += new KeyPressEventHandler(Control_KeyPress);
                if (c.Controls.Count > 0)
                {
                    AddKeyPressListener(c);
                }
            }
        }

        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((Control)sender).Name.ToString().Trim().Equals(string.Empty))
            {
                _NextEnteredControl = (Control)sender;
                if ((Control)sender is LookUpEdit)
                {
                    if (e.KeyChar == Convert.ToChar(Keys.Enter))
                    {
                        SendKeys.Send("{TAB}");
                        //((LookUpEdit)(Control)sender).ClosePopup();
                    }

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

        #endregion

        #region Events
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
                txtRate.Text = "";
                lueFactory.EditValue = System.DBNull.Value;
                lueFactDepartment.EditValue = System.DBNull.Value;
                dtpRateDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpRateDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpRateDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpRateDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpRateDate.EditValue = DateTime.Now;

                dtpInsertDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpInsertDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpInsertDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpInsertDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpInsertDate.EditValue = DateTime.Now;

                dtpDate.Properties.Items.Clear();
                dtpDate.EditValue = null;

                DataTable DTab = ObjRateType.HR_GetRateDate();

                if (DTab.Rows.Count > 0)
                {
                    foreach (DataRow DRow in DTab.Rows)
                    {
                        dtpDate.Properties.Items.Add(Convert.ToDateTime(DRow[0]).ToString("dd/MMM/yyyy"));
                    }
                    if (dtpDate.Properties.Items.Count >= 1)
                    {
                        dtpDate.SelectedIndex = 0;
                    }
                }
                else
                {
                    dtpDate.Properties.Items.Clear();
                    dtpDate.EditValue = null;
                }

                dtpRateDate.Focus();
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

        #region GridEvents

        private void dgvRateMaster_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvRateMaster.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt64(Drow["rate_id"]);
                        txtRate.Text = Val.ToString(Drow["rate"]);
                        dtpRateDate.EditValue = Val.DBDate(Drow["rate_date"].ToString());
                        lueFactory.EditValue = Val.ToInt64(Drow["factory_id"]);
                        lueFactDepartment.EditValue = Val.ToInt64(Drow["fact_department_id"]);
                        dtpRateDate.Focus();
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
            HRRate_MasterProperty HRRateMasterProperty = new HRRate_MasterProperty();
            HRRateMaster objHRMgr = new HRRateMaster();

            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }

                HRRateMasterProperty.rate_id = Val.ToInt64(lblMode.Tag);
                HRRateMasterProperty.rate = Val.ToDecimal(txtRate.Text);
                HRRateMasterProperty.factory_id = Val.ToInt64(lueFactory.EditValue);
                HRRateMasterProperty.fact_department_id = Val.ToInt64(lueFactDepartment.EditValue);
                HRRateMasterProperty.rate_date = Val.DBDate(dtpRateDate.Text);
                int IntRes = objHRMgr.Save(HRRateMasterProperty);
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Save HR Rate Details");
                    txtRate.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("HR Rate Details Data Save Successfully");
                    }
                    else
                    {
                        Global.Confirm("HR Rate Details Data Update Successfully");
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
                HRRateMasterProperty = null;
            }

            return blnReturn;
        }

        private bool InsertDetails()
        {
            bool blnReturn = true;
            HRRate_MasterProperty HRRateMasterProperty = new HRRate_MasterProperty();
            HRRateMaster objHRMgr = new HRRateMaster();

            try
            {
                if (!Validate_InsertDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }

                HRRateMasterProperty.rate_date = Val.DBDate(dtpDate.Text);
                HRRateMasterProperty.insert_date = Val.DBDate(dtpInsertDate.Text);
                int IntRes = objHRMgr.Insert_Save(HRRateMasterProperty);
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Save HR Rate Details");
                    txtRate.Focus();
                }
                else
                {
                    Global.Confirm("HR Rate Details Data Save Successfully");
                }

            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                blnReturn = false;
            }
            finally
            {
                HRRateMasterProperty = null;
            }

            return blnReturn;
        }

        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (lueFactory.Text == "")
                {
                    lstError.Add(new ListError(13, "Factory"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueFactory.Focus();
                    }
                }
                if (lueFactDepartment.Text == "")
                {
                    lstError.Add(new ListError(13, "Factory Department"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueFactDepartment.Focus();
                    }
                }
                if (txtRate.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Rate"));
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

        private bool Validate_InsertDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (Val.DBDate(dtpDate.Text) == Val.DBDate(dtpInsertDate.Text))
                {
                    lstError.Add(new ListError(5, "Rate Date And Insert Date Not Same Inserted"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        dtpInsertDate.Focus();
                    }
                }

                if (!objHRRate.ISExists(Val.ToString(dtpInsertDate.Text), Val.DBDate(dtpInsertDate.Text)).ToString().Trim().Equals(string.Empty))
                {
                    lstError.Add(new ListError(23, "Insert Date"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        dtpInsertDate.Focus();
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
                DataTable DTab = objHRRate.GetData();
                grdRateMaster.DataSource = DTab;
                //dgvRateMaster.BestFitColumns();
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
                            dgvRateMaster.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvRateMaster.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvRateMaster.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvRateMaster.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvRateMaster.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvRateMaster.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvRateMaster.ExportToCsv(Filepath);
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

        private void FrmHRRateMaster_Load(object sender, EventArgs e)
        {
            try
            {
                GetData();
                btnClear_Click(btnClear, null);
                //txtSrNo.Focus();
                Global.LOOKUPHRFactory(lueFactory);
                Global.LOOKUPHRFactoryDept(lueFactDepartment);

                txtRate.Focus();
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }

        private void lueFactory_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmHRFactoryMaster frmCnt = new FrmHRFactoryMaster();
                frmCnt.ShowDialog();
                Global.LOOKUPHRFactory(lueFactory);
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (InsertDetails())
            {
                GetData();
                btnClear_Click(sender, e);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ObjPer.SetFormPer();
            if (ObjPer.AllowDelete == false)
            {
                Global.Message(BLL.GlobalDec.gStrPermissionDelMsg);
                return;
            }

            if (GlobalDec.gEmployeeProperty.role_name == "SURAT HR" && GlobalDec.gEmployeeProperty.user_name != "RIYA")
            {
                Global.Message("This Transaction Can't Deleted.\n Please Contact Administrator.\n");
                return;
            }
            btnDelete.Enabled = false;


            DialogResult result = MessageBox.Show("Do you want to Delete data?", "Confirmation", MessageBoxButtons.YesNoCancel);
            if (result != DialogResult.Yes)
            {
                btnDelete.Enabled = true;
                return;
            }

            DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
            backgroundWorker_HRRateDelete.RunWorkerAsync();

            btnDelete.Enabled = true;
        }

        private void backgroundWorker_HRRateDelete_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            HRRate_MasterProperty HRRateMasterProperty = new HRRate_MasterProperty();
            HRRateMaster objHRMgr = new HRRateMaster();
            try
            {
                if (Val.ToInt(lblMode.Tag) != 0)
                {
                    IntRes = 0;
                    HRRateMasterProperty.rate_id = Val.ToInt(lblMode.Tag);

                    IntRes = objHRMgr.HR_Rate_Delete(HRRateMasterProperty);
                }
                else
                {
                    Global.Message("HR Rate ID not found");
                    return;
                }
            }
            catch (Exception ex)
            {
                IntRes = -1;
                General.ShowErrors(ex.ToString());
                if (ex.InnerException != null)
                {
                    Global.Message(ex.InnerException.ToString());
                }
            }
            finally
            {
                HRRateMasterProperty = null;
            }
        }

        private void backgroundWorker_HRRateDelete_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (IntRes > 0)
                {
                    Global.Confirm("HR Rate Data Deleted Successfully");
                    btnClear_Click(btnClear, null);
                    GetData();
                }
                else
                {
                    Global.Confirm("Error In HR Rate");
                    dtpRateDate.Focus();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }
    }
}
