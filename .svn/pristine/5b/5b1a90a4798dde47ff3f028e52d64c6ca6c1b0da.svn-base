using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.PropertyClasses.Transaction;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Global = DERP.Class.Global;

namespace DERP.Transaction
{
    public partial class FrmMFGEmployeeHistoryView : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        Control _NextEnteredControl;
        private List<Control> _tabControls;

        MFGEmployeeHistoryView objEmployeeHistory;

        DataTable DtControlSettings;
        DataTable DTab;
        public string GblLockBarcode { get; set; }

        #endregion

        #region Constructor
        public FrmMFGEmployeeHistoryView()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            _NextEnteredControl = new Control();
            _tabControls = new List<Control>();

            objEmployeeHistory = new MFGEmployeeHistoryView();

            DtControlSettings = new DataTable();
            DTab = new DataTable();
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

            // for Dynamic Setting By Praful On 01022020

            //Global.HideFormControls(Val.ToInt(ObjPer.form_id), this);

            ControlSettingDT(Val.ToInt(ObjPer.form_id), this);
            AddGotFocusListener(this);
            this.KeyPreview = true;

            TabControlsToList(this.Controls);
            _tabControls = _tabControls.OrderBy(x => x.TabIndex).ToList();

            // End for Dynamic Setting By Praful On 01022020

            this.Show();
        }
        private void AttachFormEvents()
        {
            objBOFormEvents.CurForm = this;
            objBOFormEvents.FormKeyPress = true;
            objBOFormEvents.FormResize = true;
            objBOFormEvents.FormClosing = true;
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion

        #region Dynamic Tab Setting
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
                if ((Control)sender is CheckedComboBoxEdit)
                {
                    ((CheckedComboBoxEdit)(Control)sender).ShowPopup();
                }
                if ((Control)sender is ComboBoxEdit)
                {
                    ((ComboBoxEdit)(Control)sender).ShowPopup();
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
            //DataTable DtColSetting = ObjSingleSettings.GetData(Property);

            //DataTable DtFilterColSetting = (from DataRow dr in DtColSetting.Rows
            //                                where Val.ToBooleanToInt(dr["is_control"]) == 1
            //                                && dr["column_type"].ToString() != "LABEL"
            //                                select dr).CopyToDataTable();
            //DevExpress.XtraLayout.LayoutControl l = new DevExpress.XtraLayout.LayoutControl();
            //l.OptionsFocus.EnableAutoTabOrder = false;

            //if (DtFilterColSetting.Rows.Count > 0)
            //{
            //    DtControlSettings = DtFilterColSetting;
            //    foreach (Control item1 in pForm.Controls)
            //    {
            //        ControllSettings(item1, DtFilterColSetting);
            //    }
            //}
        }

        private static void ControllSettings(Control item2, DataTable DTab)
        {
            BLL.Validation Val = new BLL.Validation();

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
        private void FrmEmployeeHistoryView_Shown(object sender, EventArgs e)
        {
            dtpFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            dtpFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            dtpFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            dtpFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
            dtpFromDate.EditValue = DateTime.Now;
            tmFromTime.EditValue = DateTime.Now;
            dtpToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            dtpToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            dtpToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            dtpToDate.Properties.CharacterCasing = CharacterCasing.Upper;
            dtpToDate.EditValue = DateTime.Now;
            tmToTime.EditValue = DateTime.Now;
        }
        private void Btn_Show_Click(object sender, EventArgs e)
        {
            try
            {
                EmployeeData(Val.DBDate(dtpFromDate.Text), Val.DBDate(dtpToDate.Text), Val.ToString(tmFromTime.Text), Val.ToString(tmToTime.Text));
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
                            dgvEmployeeHistory.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvEmployeeHistory.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvEmployeeHistory.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvEmployeeHistory.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvEmployeeHistory.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvEmployeeHistory.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvEmployeeHistory.ExportToCsv(Filepath);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString(), "Error in Export");
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            grdEmployeeHistory.DataSource = null;
            dtpFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            dtpFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            dtpFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            dtpFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
            dtpFromDate.EditValue = DateTime.Now;
            tmFromTime.EditValue = DateTime.Now;
            dtpToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            dtpToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            dtpToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            dtpToDate.Properties.CharacterCasing = CharacterCasing.Upper;
            dtpToDate.EditValue = DateTime.Now;
            tmToTime.EditValue = DateTime.Now;
        }
        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Functions
        public void EmployeeData(string fromDate, string toDate, string fromTime, string toTime)
        {
            try
            {
                DateTime fdate = DateTime.Parse(fromTime, System.Globalization.CultureInfo.CurrentCulture);
                string fTime = fdate.ToString("HH:mm:ss tt");
                DateTime tdate = DateTime.Parse(toTime, System.Globalization.CultureInfo.CurrentCulture);
                string tTime = tdate.ToString("HH:mm:ss tt");
                DTab = objEmployeeHistory.EmployeeData(fromDate, toDate, fTime, tTime);
                if (DTab.Rows.Count > 0)
                {
                    grdEmployeeHistory.DataSource = DTab;
                }
                else
                {
                    grdEmployeeHistory.DataSource = DTab;
                    Global.Message("No Data Found");
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }

        #endregion

        #region Export Grid
        private void MNExportToHTML_Click(object sender, EventArgs e)
        {
            Export("html", "Export to HTML", "Html files (*.html)|*.html|Htm files (*.htm)|*.htm");
        }
        private void MNExportToRTF_Click(object sender, EventArgs e)
        {
            Export("rtf", "Export to RTF", "Word (*.doc) |*.doc;*.rtf|(*.txt) |*.txt|(*.*) |*.*");
        }
        private void MNExportToPDF_Click(object sender, EventArgs e)
        {
            Export("pdf", "Export Report to PDF", "PDF (*.PDF)|*.PDF");
        }
        private void MNExportCSV_Click(object sender, EventArgs e)
        {
            Export("csv", "Export Report to CSVB", "csv (*.csv)|*.csv");
        }
        #endregion
    }
}