using BLL;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Report;
using BLL.FunctionClasses.Transaction;
using BLL.PropertyClasses.Report;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using DevExpress.XtraEditors;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Transaction.MFG
{
    public partial class FrmPriceImportPurityWise : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member
        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        Control _NextEnteredControl;
        private List<Control> _tabControls;
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);
        //RateTypeMaster objRateType = new RateTypeMaster();
        //DataTable RateType = new DataTable();
        PriceImport ObjPrcImp;
        ReportParams_Property ReportParams_Property = new BLL.PropertyClasses.Report.ReportParams_Property();
        ReportParams ObjReportParams = new ReportParams();

        DataTable DtControlSettings;
        DataTable DtSieve;
        DataTable DtPurity;
        DataTable DTabFile;
        DataTable DTab_Data;
        DataTable DPurity;
        DataTable DSieve;
        //DataTable m_dtbRateDetails;
        DataTable m_dtbPriceImport;
        int m_numForm_id;
        int i;
        int IntRes;
        //int m_RateId;
        #endregion

        #region Constructor
        public FrmPriceImportPurityWise()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            _NextEnteredControl = new Control();
            _tabControls = new List<Control>();

            ObjPrcImp = new PriceImport();

            DtControlSettings = new DataTable();
            DtSieve = new SieveMaster().GetData();
            DtPurity = new ClarityMaster().GetData();
            DTabFile = new DataTable();
            DTab_Data = new DataTable();
            DPurity = new DataTable();
            DSieve = new DataTable();
            m_dtbPriceImport = new DataTable();

            m_numForm_id = 0;
            i = 0;
            IntRes = 0;
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
            if (Global.CheckDefault() == 0)
            {
                Global.Message("Please Check User Default Setting");
                this.Close();
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
            //objBOFormEvents.ObjToDispose.Add(objCountry);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion

        #region "Events" 
        private void FrmPriceImport_Load(object sender, EventArgs e)
        {
            try
            {
                Global.LOOKUPRate(lueRateType);
                Global.LOOKUPCurrency(lueCurrency);

                RepLuePurity.DataSource = DtPurity;
                RepLuePurity.ValueMember = "purity_id";
                RepLuePurity.DisplayMember = "purity_name";

                RepLueSieve.DataSource = DtSieve;
                RepLueSieve.ValueMember = "sieve_id";
                RepLueSieve.DisplayMember = "sieve_name";

            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void FrmPriceImport_Shown(object sender, System.EventArgs e)
        {
            try
            {

                dtpDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpDate.Properties.CharacterCasing = CharacterCasing.Upper;

                dtpDate.EditValue = DateTime.Now;
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                var result = DateTime.Compare(Convert.ToDateTime(dtpDate.Text), DateTime.Today);
                if (result > 0)
                {
                    Global.Message("Date Not Be Greater Than Today Date");
                    dtpDate.Focus();
                    return;

                }
                ObjPer.SetFormPer();
                if (ObjPer.AllowUpdate == false || ObjPer.AllowInsert == false)
                {
                    Global.Message(BLL.GlobalDec.gStrPermissionInsUpdMsg);
                    return;
                }


                List<ListError> lstError = new List<ListError>();
                Dictionary<Control, string> rtnCtrls = new Dictionary<Control, string>();
                rtnCtrls = Global.CheckCompulsoryControls(Val.ToInt(ObjPer.form_id), this);
                if (rtnCtrls.Count > 0)
                {
                    foreach (KeyValuePair<Control, string> entry in rtnCtrls)
                    {
                        if (entry.Key is DevExpress.XtraEditors.LookUpEdit || entry.Key is DevExpress.XtraEditors.DateEdit || entry.Key is DevExpress.XtraEditors.TextEdit)
                        {
                            lstError.Add(new ListError(13, entry.Value));
                        }
                    }
                    rtnCtrls.First().Key.Focus();
                    BLL.General.ShowErrors(lstError);
                    Cursor.Current = Cursors.Arrow;
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                DTab_Data = new DataTable();
                DTab_Data = (DataTable)grdPriceImportPurityWise.DataSource;

                if (DTab_Data.Rows.Count > 0)
                {
                    foreach (DataRow DRow in DTab_Data.Rows)
                    {
                        if (DRow["purity_id"] != null)
                        {
                            if (Val.ToString(DRow["purity_id"]) != "")
                            {
                                if (DtPurity.Select("purity_id =" + Val.ToInt(DRow["purity_id"])).Length > 0)
                                {
                                    DPurity = DtPurity.Select("purity_id =" + Val.ToString(DRow["purity_id"])).CopyToDataTable();
                                }
                                else
                                {
                                    Global.Message("Purity Not found in Master" + Val.ToString(DRow["purity"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.Cursor = Cursors.Default;
                                    return;
                                }
                            }
                        }
                        if (DRow["sieve_id"] != null)
                        {
                            if (Val.ToString(DRow["sieve_id"]) != "")
                            {
                                if (DtSieve.Select("sieve_id ='" + Val.ToString(DRow["sieve_id"]) + "'").Length > 0)
                                {
                                    DSieve = DtSieve.Select("sieve_id ='" + Val.ToString(DRow["sieve_id"]) + "'").CopyToDataTable();
                                }
                                else
                                {
                                    Global.Message("Sieve Not found in Master" + Val.ToString(DRow["sieve"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.Cursor = Cursors.Default;
                                    return;
                                }
                            }
                        }
                        if (DPurity.Rows.Count > 0)
                        {
                            DRow["purity"] = Val.ToString(DPurity.Rows[0]["purity_name"]);
                        }
                        if (DSieve.Rows.Count > 0)
                        {
                            DRow["sieve"] = Val.ToString(DSieve.Rows[0]["sieve_name"]);
                        }

                    }
                }
                DataTable dt = new DataTable();

                int dataCount = 0;
                foreach (DataRow DRow in dt.Rows)
                {
                    dataCount = DTab_Data.Select("purity ='" + Val.ToString(DRow["purity_name"]) + "' AND sieve ='" + Val.ToString(DRow["sieve_name"]) + "'").Length;
                    if (dataCount != 0)
                    {
                        Global.Message("Price already exists " + Val.ToString(DRow["purity_name"]) + " and " + Val.ToString(DRow["sieve_name"]));
                        this.Cursor = Cursors.Default;
                        return;
                    }
                }

                if (dataCount == 0)
                {
                    int rowNo = 1;

                    DataTable distinct = DTab_Data.DefaultView.ToTable(true, "purity", "sieve");
                    if (distinct.Rows.Count != DTab_Data.Rows.Count)
                    {
                        Global.Message("Please Check File Duplicate Data Found!!");
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    if (DTab_Data != null)
                    {
                        foreach (DataRow DRow in DTab_Data.Rows)
                        {
                            if (DRow["purity"] != null)
                            {
                                if (Val.ToString(DRow["purity"]) != "")
                                {
                                    if (DtPurity.Select("purity_name ='" + Val.ToString(DRow["purity"]) + "'").Length > 0)
                                    {
                                        DPurity = DtPurity.Select("purity_name ='" + Val.ToString(DRow["purity"]) + "'").CopyToDataTable();
                                    }
                                    else
                                    {
                                        Global.Message("Purity Not found in Master" + Val.ToString(DRow["purity"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                }
                                else
                                {
                                    Global.Message("Purity Name Blank at Row No :" + rowNo);
                                    this.Cursor = Cursors.Default;
                                    return;
                                }
                            }
                            else
                            {
                                Global.Message("Purity Name are not found :" + Val.ToString(DRow["purity"]));
                                this.Cursor = Cursors.Default;
                                return;
                            }
                            if (DRow["sieve"] != null)
                            {
                                if (Val.ToString(DRow["sieve"]) != "")
                                {
                                    if (DtSieve.Select("sieve_name ='" + Val.ToString(DRow["sieve"]) + "'").Length > 0)
                                    {
                                        DSieve = DtSieve.Select("sieve_name ='" + Val.ToString(DRow["sieve"]) + "'").CopyToDataTable();
                                    }
                                    else
                                    {
                                        Global.Message("Sieve Not found in Master" + Val.ToString(DRow["sieve"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                }
                                else
                                {
                                    Global.Message("Sieve Name Blank at Row No :" + rowNo);
                                    this.Cursor = Cursors.Default;
                                    return;
                                }
                            }
                            else
                            {
                                Global.Message("Sieve Name are not found :" + Val.ToString(DRow["sieve"]));
                                this.Cursor = Cursors.Default;
                                return;
                            }
                            rowNo++;
                        }
                    }
                    else
                    {
                        Global.Message("Data not found in Grid Please check data");
                        this.Cursor = Cursors.Default;
                        return;
                    }

                    DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;

                    panelProgress.Visible = true;
                    backgroundWorker_PriceImportPurityWise.RunWorkerAsync();

                    Cursor.Current = Cursors.Default;
                }
                else
                {
                    Global.Message("Price already exists.");
                    return;
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                this.Cursor = Cursors.Default;
                return;
            }
        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            List<ListError> lstError = new List<ListError>();
            Dictionary<Control, string> rtnCtrls = new Dictionary<Control, string>();
            rtnCtrls = Global.CheckCompulsoryControls(Val.ToInt(ObjPer.form_id), this);
            try
            {
                if (Val.ToString(dtpDate.Text) != null && Val.ToInt(lueRateType.EditValue) != 0 && Val.ToInt(lueCurrency.EditValue) != 0)
                {
                    if (rtnCtrls.Count > 0)
                    {
                        foreach (KeyValuePair<Control, string> entry in rtnCtrls)
                        {
                            if (entry.Key is DevExpress.XtraEditors.LookUpEdit || entry.Key is DevExpress.XtraEditors.DateEdit || entry.Key is DevExpress.XtraEditors.TextEdit)
                            {
                                lstError.Add(new ListError(13, entry.Value));
                            }
                        }
                        rtnCtrls.First().Key.Focus();
                        BLL.General.ShowErrors(lstError);
                        Cursor.Current = Cursors.Arrow;
                        return;
                    }

                    OpenFileDialog OpenDialog = new OpenFileDialog();
                    if (OpenDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                    {
                        return;
                    }
                    txtFileName.Text = OpenDialog.FileName;
                    OpenDialog.Dispose();
                    OpenDialog = null;
                    DTabFile.Rows.Clear();
                    try
                    {
                        Stream s = File.Open(txtFileName.Text, FileMode.Open, FileAccess.Read, FileShare.None);

                        s.Close();


                    }
                    catch (Exception ex)
                    {
                        Global.Message("File Is open please close the excel file.");
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    if (File.Exists(txtFileName.Text) == false)
                    {
                        Global.Message("File Is Not Exists To The Path");
                        return;
                    }

                    this.Cursor = Cursors.WaitCursor;
                    grdPriceImportPurityWise.DataSource = null;

                    if (txtFileName.Text.Length != 0)
                    {
                        using (var pck = new ExcelPackage(new FileInfo(txtFileName.Text)))
                        {
                            ExcelWorksheet ws = pck.Workbook.Worksheets[1];
                            DTabFile = WorksheetToDataTable(ws, true);
                        }
                    }

                    m_dtbPriceImport = new DataTable();
                    m_dtbPriceImport.Columns.Add("sieve_id", typeof(int)).DefaultValue = 0;
                    m_dtbPriceImport.Columns.Add("sieve", typeof(string));
                    m_dtbPriceImport.Columns.Add("purity_id", typeof(int)).DefaultValue = 0;
                    m_dtbPriceImport.Columns.Add("purity", typeof(string));
                    m_dtbPriceImport.Columns.Add("per_pcs", typeof(decimal)).DefaultValue = 0;
                    m_dtbPriceImport.Columns.Add("per_carat", typeof(decimal)).DefaultValue = 0;
                    m_dtbPriceImport.Columns.Add("janged_per_carat", typeof(decimal)).DefaultValue = 0;
                    if (DTabFile.Rows.Count > 0)
                    {
                        foreach (DataRow DRow in DTabFile.Rows)
                        {
                            if (DRow["purity"] != null)
                            {
                                if (Val.ToString(DRow["purity"]) != "")
                                {
                                    if (DtPurity.Select("purity_name ='" + Val.ToString(DRow["purity"]) + "'").Length > 0)
                                    {
                                        DPurity = DtPurity.Select("purity_name ='" + Val.ToString(DRow["purity"]) + "'").CopyToDataTable();
                                    }
                                    else
                                    {
                                        Global.Message("Purity Not found in Master" + Val.ToString(DRow["purity"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        this.Cursor = Cursors.Default;
                                        return;
                                    }
                                }
                            }
                            if (DRow["sieve"] != null)
                            {
                                if (Val.ToString(DRow["sieve"]) != "")
                                {
                                    if (DtSieve.Select("sieve_name ='" + Val.ToString(DRow["sieve"]) + "'").Length > 0)
                                    {
                                        DSieve = DtSieve.Select("sieve_name ='" + Val.ToString(DRow["sieve"]) + "'").CopyToDataTable();
                                    }
                                    else
                                    {
                                        Global.Message("Sieve Not found in Master" + Val.ToString(DRow["sieve"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        this.Cursor = Cursors.Default;
                                        return;
                                    }
                                }
                            }
                            DataRow drwNew = m_dtbPriceImport.NewRow();
                            if (DPurity.Rows.Count > 0)
                            {
                                drwNew["purity_id"] = Val.ToInt(DPurity.Rows[0]["purity_id"]);
                                drwNew["purity"] = Val.ToString(DPurity.Rows[0]["purity_name"]);
                            }
                            if (DSieve.Rows.Count > 0)
                            {
                                drwNew["sieve_id"] = Val.ToInt(DSieve.Rows[0]["sieve_id"]);
                                drwNew["sieve"] = Val.ToString(DSieve.Rows[0]["sieve_name"]);
                            }
                            if (Val.ToDecimal(DRow["per_pcs"]) > 0)
                            {
                                drwNew["per_pcs"] = Val.ToDecimal(DRow["per_pcs"]);
                            }
                            if (Val.ToDecimal(DRow["per_carat"]) > 0)
                            {
                                drwNew["per_carat"] = Val.ToDecimal(DRow["per_carat"]);
                            }
                            if (Val.ToDecimal(DRow["janged_per_carat"]) > 0)
                            {
                                drwNew["janged_per_carat"] = Val.ToDecimal(DRow["janged_per_carat"]);
                            }
                            m_dtbPriceImport.Rows.Add(drwNew);

                            dgvPriceImportPurityWise.MoveLast();
                        }
                    }
                    grdPriceImportPurityWise.DataSource = m_dtbPriceImport;

                    this.Cursor = Cursors.Default;
                }
                else
                {
                    Global.Message("Date/Rate Type/ Currency should not be blank");
                    return;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                this.Cursor = Cursors.Default;
                return;
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtFileName.Text = "";
                dtpDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpDate.Properties.CharacterCasing = CharacterCasing.Upper;

                dtpDate.EditValue = DateTime.Now;
                lueCurrency.EditValue = null;
                lueRateType.EditValue = null;
                m_dtbPriceImport.Rows.Clear();
                DTabFile.Rows.Clear();
                grdPriceImportPurityWise.DataSource = null;
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void lblFormatSample_Click(object sender, EventArgs e)
        {
            Global.CopyFormat(System.Windows.Forms.Application.StartupPath + @"\FORMAT\PriceImportPurityWise.xlsx", "PriceImportPurityWise.xlsx", "xlsx");
        }
        private void backgroundWorker_PriceImportPurityWise_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                Conn = new BeginTranConnection(true, false);
                PriceImportProperty objPrcImpProperty = new PriceImportProperty();
                try
                {
                    IntRes = 0;
                    int IntResMst = 0;
                    int IntCounter = 0;
                    int Count = 0;
                    int Rate_id = 0;
                    int TotalCount = DTab_Data.Rows.Count;
                    //if (m_RateId == 0)
                    //{
                    objPrcImpProperty.rate_date = Val.DBDate(dtpDate.Text);
                    objPrcImpProperty.rate_type_id = Val.ToInt32(lueRateType.EditValue);
                    objPrcImpProperty.currency_id = Val.ToInt32(lueCurrency.EditValue);

                    objPrcImpProperty.form_id = m_numForm_id;

                    //    if (m_RateId == 0)
                    //    {
                    IntResMst = ObjPrcImp.Purity_Rate_MasterSave(objPrcImpProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                    //    }
                    //    else
                    //    {
                    //        IntResMst = 1;
                    //    }
                    Rate_id = objPrcImpProperty.rate_id;
                    //}

                    if (IntResMst > 0)
                    {
                        foreach (DataRow DRow in DTab_Data.Rows)
                        {
                            objPrcImpProperty = new PriceImportProperty();
                            //if (m_RateId > 0)
                            //{
                            //    objPrcImpProperty.rate_id = Val.ToInt(m_RateId);
                            //}
                            //else
                            //{
                            objPrcImpProperty.rate_id = Val.ToInt(Rate_id);
                            //}
                            objPrcImpProperty.purity_id = Val.ToInt(DRow["purity_id"]);
                            objPrcImpProperty.sieve_id = Val.ToInt(DRow["sieve_id"]);
                            objPrcImpProperty.per_pcs = Val.ToDecimal(DRow["per_pcs"]);
                            objPrcImpProperty.per_carat = Val.ToDecimal(DRow["per_carat"]);
                            objPrcImpProperty.janged_per_carat = Val.ToDecimal(DRow["janged_per_carat"]);
                            objPrcImpProperty.form_id = m_numForm_id;
                            i++;
                            objPrcImpProperty.sequence_no = i;
                            IntRes = ObjPrcImp.PurityPrice_DetailSave(objPrcImpProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                            Count++;
                            IntCounter++;
                            IntRes++;
                            SetControlPropertyValue(lblProgressCount, "Text", Count.ToString() + "" + "/" + "" + TotalCount.ToString() + " Completed....");
                        }
                        Conn.Inter1.Commit();
                    }
                }
                catch (Exception ex)
                {
                    IntRes = -1;
                    Conn.Inter1.Rollback();
                    Conn = null;
                    General.ShowErrors(ex.ToString());
                    return;
                }
                finally
                {
                    objPrcImpProperty = null;
                }
            }
            catch (Exception ex)
            {
                IntRes = -1;
                Conn.Inter1.Rollback();
                Conn = null;
                Global.Message(ex.ToString());
                if (ex.InnerException != null)
                {
                    Global.Message(ex.InnerException.ToString());
                }
            }
        }

        private void backgroundWorker_PriceImportPurityWise_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                panelProgress.Visible = false;
                if (IntRes > 0)
                {
                    Global.Confirm("Price Import Data Save Successfully");
                    i = 0;
                    btnClear_Click(null, null);
                    this.Cursor = Cursors.Default;
                }
                else
                {
                    Global.Confirm("Error In Price Import");
                    this.Cursor = Cursors.Default;
                    dtpDate.Focus();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }
        #endregion

        #region "Function"
        public System.Data.DataTable WorksheetToDataTable(ExcelWorksheet ws, bool hasHeader = true)
        {
            System.Data.DataTable dt = new System.Data.DataTable(ws.Name);
            int totalCols = ws.Dimension.End.Column;
            int totalRows = ws.Dimension.End.Row;
            int startRow = hasHeader ? 2 : 1;
            ExcelRange wsRow;
            DataRow dr;
            foreach (var firstRowCell in ws.Cells[1, 1, 1, totalCols])
            {
                dt.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
            }

            for (int rowNum = startRow; rowNum <= totalRows; rowNum++)
            {
                wsRow = ws.Cells[rowNum, 1, rowNum, totalCols];
                dr = dt.NewRow();
                foreach (var cell in wsRow)
                {
                    dr[cell.Start.Column - 1] = cell.Text;
                }

                dt.Rows.Add(dr);
            }

            return dt;
        }
        public void SetControlPropertyValue(Control oControl, string propName, object propValue)
        {
            if (oControl.InvokeRequired)
            {
                SetControlValueCallback d = new SetControlValueCallback(SetControlPropertyValue);
                oControl.Invoke(d, new object[]
                        {
                            oControl,
                            propName,
                            propValue
                        });
            }
            else
            {
                Type t = oControl.GetType();
                PropertyInfo[] props = t.GetProperties();
                foreach (PropertyInfo p in props)
                {
                    if ((p.Name.ToUpper() == propName.ToUpper()))
                    {
                        p.SetValue(oControl, propValue, null);
                    }
                }
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
                            dgvPriceImportPurityWise.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvPriceImportPurityWise.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvPriceImportPurityWise.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvPriceImportPurityWise.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvPriceImportPurityWise.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvPriceImportPurityWise.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvPriceImportPurityWise.ExportToCsv(Filepath);
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
    }
}
