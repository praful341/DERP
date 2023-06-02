using BLL;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Report;
using BLL.FunctionClasses.Transaction;
using BLL.PropertyClasses.Master;
using BLL.PropertyClasses.Report;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Transaction
{
    public partial class FrmPriceImport : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member
        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        Control _NextEnteredControl;
        private List<Control> _tabControls;
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);
        RateTypeMaster objRateType = new RateTypeMaster();
        DataTable RateType = new DataTable();
        PriceImport ObjPrcImp;
        ReportParams_Property ReportParams_Property = new BLL.PropertyClasses.Report.ReportParams_Property();
        ReportParams ObjReportParams = new ReportParams();
        RateTypeMaster ObjRateType = new RateTypeMaster();
        DataTable DtControlSettings;
        DataTable DtSieve;
        DataTable DtAssort;
        DataTable DTabFile;
        DataTable DTab_Data;
        DataTable DAssort;
        DataTable DSieve;
        DataTable m_dtbRateDetails;
        DataTable m_dtbPriceImport;
        //DataTable m_dtbColor = new DataTable();
        int m_numForm_id;
        int i;
        int IntRes;
        int m_RateId;
        int flag_check;
        #endregion

        #region Constructor
        public FrmPriceImport()
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
            DtAssort = new AssortMaster().GetData();
            DTabFile = new DataTable();
            DTab_Data = new DataTable();
            DAssort = new DataTable();
            DSieve = new DataTable();
            m_dtbRateDetails = new DataTable();
            m_dtbPriceImport = new DataTable();
            flag_check = 0;

            m_numForm_id = 0;
            i = 0;
            IntRes = 0;
            m_RateId = 0;
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

                RepLueAssort.DataSource = DtAssort;
                RepLueAssort.ValueMember = "assort_id";
                RepLueAssort.DisplayMember = "assort_name";

                RepLueSieve.DataSource = DtSieve;
                RepLueSieve.ValueMember = "sieve_id";
                RepLueSieve.DisplayMember = "sieve_name";

                dtpEntryDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpEntryDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpEntryDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpEntryDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpEntryDate.EditValue = DateTime.Now;

                //Global.LOOKUPRoughShade(lueColor);

                //m_dtbColor = (((DataTable)lueColor.Properties.DataSource).Copy());
                //DataTable DTab_Color = new DataTable();
                //DTab_Color.Columns.Add("color_id", typeof(Int64));
                //DTab_Color.Columns.Add("color_name", typeof(string));

                //foreach (DataRow DR in m_dtbColor.Rows)
                //{
                //    if (DR["color_name"].ToString() == "WHITE" || DR["color_name"].ToString() == "NATTS" || DR["color_name"].ToString() == "ATLB" || DR["color_name"].ToString() == "ATLC"
                //        //|| DR["color_name"].ToString() == "MEL" || DR["color_name"].ToString() == "CVD" || DR["color_name"].ToString() == "-70"
                //        //|| DR["color_name"].ToString() == "BROKEN" || DR["color_name"].ToString() == "+6.5"
                //        //|| DR["color_name"].ToString() == "AT"
                //        //|| DR["color_name"].ToString() == "NOLB"
                //        //|| DR["color_name"].ToString() == "ATLB"
                //        //|| DR["color_name"].ToString() == "NWLB"
                //        //|| DR["color_name"].ToString() == "G"
                //        //|| DR["color_name"].ToString() == "N.O"
                //        //|| DR["color_name"].ToString() == "ATLC"
                //        //|| DR["color_name"].ToString() == "NWLC"
                //        //|| DR["color_name"].ToString() == "W"
                //        //|| DR["color_name"].ToString() == "GAA"
                //        //|| DR["color_name"].ToString() == "LCH"
                //        //|| DR["color_name"].ToString() == "LCI"
                //        //|| DR["color_name"].ToString() == "LBGH"
                //        )
                //    {
                //        DataRow dr = DTab_Color.NewRow();
                //        dr[0] = DR["color_id"];
                //        dr[1] = DR["color_name"];
                //        DTab_Color.Rows.Add(dr);
                //    }
                //    else
                //    {

                //    }
                //}

                //lueColor.Properties.DataSource = DTab_Color;
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void FrmPriceImport_Shown(object sender, System.EventArgs e)
        {
            //try
            //{
            //    dtpRateDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            //    dtpRateDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            //    dtpRateDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            //    dtpRateDate.Properties.CharacterCasing = CharacterCasing.Upper;

            //    dtpRateDate.EditValue = DateTime.Now;
            //}
            //catch (Exception ex)
            //{
            //    Global.Message(ex.ToString());
            //    return;
            //}
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (GlobalDec.gEmployeeProperty.user_name != "RIKITA" && GlobalDec.gEmployeeProperty.user_name != "MAYANK" && GlobalDec.gEmployeeProperty.user_name != "PRAFUL" && GlobalDec.gEmployeeProperty.user_name != "DEEPAK" && GlobalDec.gEmployeeProperty.user_name != "MILAN" && GlobalDec.gEmployeeProperty.user_name != "CHINTAN" && GlobalDec.gEmployeeProperty.user_name != "HASTI")
                {
                    Global.Message("Don't have permission...Please Contact to Administrator...");
                    return;
                }
                else
                {
                    if (Val.ToString(CmbColorName.Text) != "MINUS 2 MUMBAI" && GlobalDec.gEmployeeProperty.user_name == "MAYANK")
                    {
                        Global.Message("Don't have permission...Please Contact to Administrator...");
                        return;
                    }
                }
                if (flag_check == 0)
                {
                    //var result = DateTime.Compare(Convert.ToDateTime(dtpRateDate.Text), DateTime.Today);
                    //if (result > 0)
                    //{
                    //    Global.Message("Date Not Be Greater Than Today Date");
                    //    dtpRateDate.Focus();
                    //    return;
                    //}
                }
                else
                {
                    //var result = DateTime.Compare(Convert.ToDateTime(dtpEntryDate.Text), DateTime.Today);
                    //if (result > 0)
                    //{
                    //    Global.Message("Date Not Be Greater Than Today Date");
                    //    dtpEntryDate.Focus();
                    //    return;
                    //}
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
                DTab_Data.AcceptChanges();
                DTab_Data = new DataTable();
                DTab_Data = (DataTable)grdPriceImport.DataSource;
                if (m_RateId != 0)
                {
                    if (DTab_Data.Rows.Count > 0)
                    {
                        foreach (DataRow DRow in DTab_Data.Rows)
                        {
                            if (DRow["assort_id"] != null)
                            {
                                if (Val.ToString(DRow["assort_id"]) != "")
                                {
                                    if (DtAssort.Select("assort_id =" + Val.ToInt(DRow["assort_id"])).Length > 0)
                                    {
                                        DAssort = DtAssort.Select("assort_id =" + Val.ToString(DRow["assort_id"])).CopyToDataTable();
                                    }
                                    else
                                    {
                                        Global.Message("Assort Not found in Master" + Val.ToString(DRow["assort"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                            //DataRow drwNew = m_dtbPriceImport.NewRow();
                            if (DAssort.Rows.Count > 0)
                            {
                                DRow["assort"] = Val.ToString(DAssort.Rows[0]["assort_name"]);
                            }
                            if (DSieve.Rows.Count > 0)
                            {
                                DRow["sieve"] = Val.ToString(DSieve.Rows[0]["sieve_name"]);
                            }
                        }
                    }
                }
                // DataTable dt = new DataTable();
                DataTable dtDataCheck = new DataTable();
                if (flag_check == 0)
                {
                    dtDataCheck = ObjPrcImp.GetData(Val.ToString(dtpRateDate.Text), Val.ToInt(lueRateType.EditValue), Val.ToInt(lueCurrency.EditValue));
                }
                else
                {
                    dtDataCheck = ObjPrcImp.GetData(Val.ToString(dtpEntryDate.Text), Val.ToInt(lueRateType.EditValue), Val.ToInt(lueCurrency.EditValue));
                }

                //DataTable DtoldData = new DataTable();
                //if (dtDataCheck.Rows.Count == 0)
                //{
                //    DtoldData = ObjPrcImp.GetOldData(Val.ToInt(lueRateType.EditValue), Val.ToInt(lueCurrency.EditValue));
                //    foreach (DataRow DRow in DtoldData.Rows)
                //    {
                //        if (DTab_Data.Select("assort ='" + Val.ToString(DRow["assort_name"]) + "' AND sieve ='" + Val.ToString(DRow["sieve_name"]) + "'").Length == 0)
                //        {
                //            Global.Message("Please check old assort is compulsary in file. Assort " + Val.ToString(DRow["assort_name"]) + " and sieve " + Val.ToString(DRow["sieve_name"]) + " price not exist");
                //            //break;
                //            this.Cursor = Cursors.Default;
                //            return;
                //        }
                //    }
                //}

                int dataCount = 0;

                if (dtDataCheck.Rows.Count > 0)
                {
                    m_RateId = Val.ToInt(dtDataCheck.Rows[0]["rate_id"]);
                }
                else
                {
                    m_RateId = 0;
                }

                if (dataCount == 0)
                {
                    int rowNo = 1;

                    DTab_Data.AcceptChanges();
                    DataTable distinct = DTab_Data.DefaultView.ToTable(true, "assort", "sieve");
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
                            if (DRow["assort"] != null)
                            {
                                if (Val.ToString(DRow["assort"]) != "")
                                {
                                    if (DtAssort.Select("assort_name ='" + Val.ToString(DRow["assort"]) + "'").Length > 0)
                                    {
                                        DAssort = DtAssort.Select("assort_name ='" + Val.ToString(DRow["assort"]) + "'").CopyToDataTable();
                                    }
                                    else
                                    {
                                        Global.Message("Assort Not found in Master" + Val.ToString(DRow["assort"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                }
                                else
                                {
                                    Global.Message("Assort Name Blank at Row No :" + rowNo);
                                    this.Cursor = Cursors.Default;
                                    return;
                                }
                            }
                            else
                            {
                                Global.Message("Assort Name are not found :" + Val.ToString(DRow["assort"]));
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
                    backgroundWorker_PriceImport.RunWorkerAsync();

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
                if (Val.ToString(dtpEntryDate.Text) != null && Val.ToInt(lueRateType.EditValue) != 0 && Val.ToInt(lueCurrency.EditValue) != 0)
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

                    DataTable dtDataCheckNew = new DataTable();

                    dtDataCheckNew = ObjPrcImp.Price_GetData(Val.ToString(dtpEntryDate.Text), Val.ToInt(lueRateType.EditValue), Val.ToInt(lueCurrency.EditValue));

                    if (dtDataCheckNew.Rows.Count > 0)
                    {
                        Global.Message("Price Date Already Exists in Record...Please Check And Confirm...");
                        grdPriceImport.DataSource = null;
                        m_dtbPriceImport.AcceptChanges();
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
                    grdPriceImport.DataSource = null;

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
                    m_dtbPriceImport.Columns.Add("assort_id", typeof(int)).DefaultValue = 0;
                    m_dtbPriceImport.Columns.Add("assort", typeof(string));
                    m_dtbPriceImport.Columns.Add("rate", typeof(decimal)).DefaultValue = 0;
                    m_dtbPriceImport.Columns.Add("flag", typeof(int)).DefaultValue = 0;
                    m_dtbPriceImport.Columns.Add("rate_detail_id", typeof(Int64)).DefaultValue = 0;
                    m_dtbPriceImport.Columns.Add("old_rate", typeof(decimal)).DefaultValue = 0;
                    if (DTabFile.Rows.Count > 0)
                    {
                        foreach (DataRow DRow in DTabFile.Rows)
                        {
                            if (DRow["assort"] != null)
                            {
                                if (Val.ToString(DRow["assort"]) != "")
                                {
                                    if (DtAssort.Select("assort_name ='" + Val.ToString(DRow["assort"]) + "'").Length > 0)
                                    {
                                        DAssort = DtAssort.Select("assort_name ='" + Val.ToString(DRow["assort"]) + "'").CopyToDataTable();
                                    }
                                    else
                                    {
                                        Global.Message("Assort Not found in Master" + Val.ToString(DRow["assort"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        this.Cursor = Cursors.Default;
                                        return;
                                    }
                                }
                            }
                            //if (DRow["sieve"] != null)
                            //{
                            //    if (Val.ToString(DRow["sieve"]) != "")
                            //    {
                            //        if (DtSieve.Select("sieve_name ='" + Val.ToString(DRow["sieve"]) + "'").Length > 0)
                            //        {
                            //            DSieve = DtSieve.Select("sieve_name ='" + Val.ToString(DRow["sieve"]) + "'").CopyToDataTable();
                            //        }
                            //        else
                            //        {
                            //            Global.Message("Sieve Not found in Master" + Val.ToString(DRow["sieve"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //            this.Cursor = Cursors.Default;
                            //            return;
                            //        }
                            //    }
                            //}
                            DataRow drwNew = m_dtbPriceImport.NewRow();
                            if (DRow["-2"] != null)
                            {
                                if (Val.ToString(DRow["-2"]) != "")
                                {
                                    if (DtSieve.Select("sieve_name ='" + "-2" + "'").Length > 0)
                                    {
                                        DSieve = DtSieve.Select("sieve_name ='" + "-2" + "'").CopyToDataTable();
                                    }
                                    else
                                    {
                                        Global.Message("Sieve Not found in Master" + Val.ToString(DRow["sieve"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        this.Cursor = Cursors.Default;
                                        return;
                                    }

                                    if (DAssort.Rows.Count > 0)
                                    {
                                        drwNew["assort_id"] = Val.ToInt(DAssort.Rows[0]["assort_id"]);
                                        drwNew["assort"] = Val.ToString(DAssort.Rows[0]["assort_name"]);
                                    }
                                    if (DSieve.Rows.Count > 0)
                                    {
                                        drwNew["sieve_id"] = Val.ToInt(DSieve.Rows[0]["sieve_id"]);
                                        drwNew["sieve"] = Val.ToString(DSieve.Rows[0]["sieve_name"]);
                                    }
                                    if (Val.ToDecimal(DRow["-2"]) > 0)
                                    {
                                        drwNew["rate"] = Val.ToDecimal(DRow["-2"]);
                                        drwNew["old_rate"] = Val.ToDecimal(DRow["-2"]);
                                    }
                                    drwNew["rate_detail_id"] = Val.ToInt(0);

                                    m_dtbPriceImport.Rows.Add(drwNew);
                                }
                            }

                            if (DRow["+2"] != null)
                            {
                                if (Val.ToString(DRow["+2"]) != "")
                                {
                                    if (DtSieve.Select("sieve_name ='" + "+2" + "'").Length > 0)
                                    {
                                        DSieve = DtSieve.Select("sieve_name ='" + "+2" + "'").CopyToDataTable();
                                    }
                                    else
                                    {
                                        Global.Message("Sieve Not found in Master" + Val.ToString(DRow["sieve"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        this.Cursor = Cursors.Default;
                                        return;
                                    }

                                    drwNew = m_dtbPriceImport.NewRow();
                                    if (DAssort.Rows.Count > 0)
                                    {
                                        drwNew["assort_id"] = Val.ToInt(DAssort.Rows[0]["assort_id"]);
                                        drwNew["assort"] = Val.ToString(DAssort.Rows[0]["assort_name"]);
                                    }
                                    if (DSieve.Rows.Count > 0)
                                    {
                                        drwNew["sieve_id"] = Val.ToInt(DSieve.Rows[0]["sieve_id"]);
                                        drwNew["sieve"] = Val.ToString(DSieve.Rows[0]["sieve_name"]);
                                    }
                                    if (Val.ToDecimal(DRow["+2"]) > 0)
                                    {
                                        drwNew["rate"] = Val.ToDecimal(DRow["+2"]);
                                        drwNew["old_rate"] = Val.ToDecimal(DRow["+2"]);
                                    }
                                    drwNew["rate_detail_id"] = Val.ToInt(0);

                                    m_dtbPriceImport.Rows.Add(drwNew);
                                }
                            }

                            if (DRow["-00"] != null)
                            {
                                if (Val.ToString(DRow["-00"]) != "")
                                {
                                    if (DtSieve.Select("sieve_name ='" + "-00" + "'").Length > 0)
                                    {
                                        DSieve = DtSieve.Select("sieve_name ='" + "-00" + "'").CopyToDataTable();
                                    }
                                    else
                                    {
                                        Global.Message("Sieve Not found in Master" + Val.ToString(DRow["sieve"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        this.Cursor = Cursors.Default;
                                        return;
                                    }

                                    drwNew = m_dtbPriceImport.NewRow();
                                    if (DAssort.Rows.Count > 0)
                                    {
                                        drwNew["assort_id"] = Val.ToInt(DAssort.Rows[0]["assort_id"]);
                                        drwNew["assort"] = Val.ToString(DAssort.Rows[0]["assort_name"]);
                                    }
                                    if (DSieve.Rows.Count > 0)
                                    {
                                        drwNew["sieve_id"] = Val.ToInt(DSieve.Rows[0]["sieve_id"]);
                                        drwNew["sieve"] = Val.ToString(DSieve.Rows[0]["sieve_name"]);
                                    }
                                    if (Val.ToDecimal(DRow["-00"]) > 0)
                                    {
                                        drwNew["rate"] = Val.ToDecimal(DRow["-00"]);
                                        drwNew["old_rate"] = Val.ToDecimal(DRow["-00"]);
                                    }
                                    drwNew["rate_detail_id"] = Val.ToInt(0);

                                    m_dtbPriceImport.Rows.Add(drwNew);
                                }
                            }

                            dgvPriceImport.MoveLast();
                        }
                    }
                    grdPriceImport.DataSource = m_dtbPriceImport;
                    flag_check = 1;

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
                //dtpRateDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                //dtpRateDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                //dtpRateDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                //dtpRateDate.Properties.CharacterCasing = CharacterCasing.Upper;

                //dtpRateDate.EditValue = DateTime.Now;
                //lueCurrency.EditValue = null;
                //lueRateType.EditValue = null;
                m_dtbPriceImport.Rows.Clear();
                DTabFile.Rows.Clear();
                grdPriceImport.DataSource = null;
                CmbColorName.Text = "";

                dtpEntryDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpEntryDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpEntryDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpEntryDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpEntryDate.EditValue = DateTime.Now;
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void lblFormatSample_Click(object sender, EventArgs e)
        {
            Global.CopyFormat(System.Windows.Forms.Application.StartupPath + @"\FORMAT\PriceImport.xlsx", "PriceImportSurat.xlsx", "xlsx");
        }
        private void backgroundWorker_PriceImport_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
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
                    if (m_RateId == 0)
                    {
                        if (flag_check == 0)
                        {
                            objPrcImpProperty.rate_date = Val.DBDate(dtpRateDate.Text);
                        }
                        else
                        {
                            objPrcImpProperty.rate_date = Val.DBDate(dtpEntryDate.Text);
                        }
                        objPrcImpProperty.rate_type_id = Val.ToInt32(lueRateType.EditValue);
                        objPrcImpProperty.currency_id = Val.ToInt32(lueCurrency.EditValue);

                        objPrcImpProperty.form_id = m_numForm_id;

                        //RateType = objRateType.GetData(1);

                        //DataTable Dtab_RateType = RateType.Select("ratetype_id = " + Val.ToInt32(lueRateType.EditValue)).CopyToDataTable();                  

                        //if (Dtab_RateType.Rows.Count > 0)
                        //{
                        //    ReportParams_Property.From_Date = Val.DBDate(dtpDate.Text);
                        //    ReportParams_Property.To_Date = Val.DBDate(dtpDate.Text);
                        //    ReportParams_Property.company_id = Val.ToString(GlobalDec.gEmployeeProperty.company_id);
                        //    ReportParams_Property.branch_id = Val.ToString(GlobalDec.gEmployeeProperty.branch_id);
                        //    ReportParams_Property.location_id = Val.ToString(Dtab_RateType.Rows[0]["location_id"]);

                        //    DataTable DTab_Stock = ObjReportParams.GetLiveStock(ReportParams_Property, "RPT_StockLedger_InOut_New");

                        //    DataTable DTab_Old_Price = ObjReportParams.Get_Old_PriceAmount(objPrcImpProperty);


                        //    for (int i = 0; i < DTab_Stock.Rows.Count; i++)
                        //    {
                        //        for (int j = 0; j < DTab_Old_Price.Rows.Count; j++)
                        //        {
                        //            if (DTab_Old_Price.Select("assort_id = " + DTab_Stock.Rows[i]["assort_seq"] + " AND sieve_id =" + DTab_Stock.Rows[i]["sieve_seq"]).Length > 0)
                        //            {
                        //                DataTable Dtab_Asort = DTab_Stock.Select("assort_seq = " + DTab_Old_Price.Rows[i]["assort_id"] + " AND sieve_seq =" + DTab_Old_Price.Rows[i]["sieve_id"]).CopyToDataTable();

                        //                DTab_Old_Price.Rows[j]["amount"] = Dtab_Asort.Rows[0]["closing_amount"];
                        //            }

                        //        }

                        //    }

                        //    decimal Sum = Val.ToDecimal(DTab_Stock.Compute("Sum(closing_carat)", ""));

                        //    decimal New_Diff_Amount = Val.ToDecimal(Sum * Val.ToDecimal(DTab_Data.Rows[i]["rate"]));

                        //    decimal Old_Diff_Amount = Val.ToDecimal(Sum * Val.ToDecimal(DTab_Data.Rows[i]["rate"]));

                        //}

                        if (m_RateId == 0)
                        {
                            IntResMst = ObjPrcImp.MasterSave(objPrcImpProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                        }
                        else
                        {
                            IntResMst = 1;
                        }


                        Rate_id = objPrcImpProperty.rate_id;
                    }

                    if (IntResMst > 0 || m_RateId != 0)
                    {
                        //DataTable dt = new DataTable();
                        //dt = ObjPrcImp.GetPriceData(Val.ToString(dtpDate.Text), Val.ToInt(lueRateType.EditValue), Val.ToInt(lueCurrency.EditValue));
                        //int dataCount = 0;
                        //if (dt.Rows.Count > 0)
                        //{
                        //    foreach (DataRow DRow in dt.Rows)
                        //    {
                        //        objPrcImpProperty = new PriceImportProperty();
                        //        dataCount = DTab_Data.Select("assort ='" + Val.ToString(DRow["assort_name"]) + "' AND sieve ='" + Val.ToString(DRow["sieve_name"]) + "'").Length;
                        //        if (m_RateId > 0)
                        //        {
                        //            objPrcImpProperty.rate_id = Val.ToInt(m_RateId);
                        //        }
                        //        else
                        //        {
                        //            objPrcImpProperty.rate_id = Val.ToInt(Rate_id);
                        //        }
                        //        objPrcImpProperty.assort_id = Val.ToInt(DRow["assort_id"]);
                        //        objPrcImpProperty.sieve_id = Val.ToInt(DRow["sieve_id"]);
                        //        objPrcImpProperty.rate = Val.ToDecimal(DRow["rate"]);

                        //        objPrcImpProperty.form_id = m_numForm_id;
                        //        i++;
                        //        objPrcImpProperty.sequence_no = i;
                        //        if (dataCount != 0)
                        //        {
                        //            objPrcImpProperty.count = 1;
                        //        }
                        //        else
                        //        {
                        //            objPrcImpProperty.count = 0;
                        //        }
                        //        IntRes = ObjPrcImp.DetailSave(objPrcImpProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                        //        Count++;
                        //        IntCounter++;
                        //        IntRes++;
                        //        SetControlPropertyValue(lblProgressCount, "Text", Count.ToString() + "" + "/" + "" + TotalCount.ToString() + " Completed....");


                        //    }
                        //}
                        //else
                        //{

                        foreach (DataRow DRow in DTab_Data.Rows)
                        {

                            //if (DRow["assort"] != null)
                            //{
                            //    if (Val.ToString(DRow["assort"]) != "")
                            //    {
                            //        if (DtAssort.Select("assort_name ='" + Val.ToString(DRow["assort"]) + "'").Length > 0)
                            //        {
                            //            DAssort = DtAssort.Select("assort_name ='" + Val.ToString(DRow["assort"]) + "'").CopyToDataTable();
                            //        }
                            //    }
                            //}

                            //if (DRow["sieve"] != null)
                            //{
                            //    if (Val.ToString(DRow["sieve"]) != "")
                            //    {
                            //        if (DtSieve.Select("sieve_name ='" + Val.ToString(DRow["sieve"]) + "'").Length > 0)
                            //        {
                            //            DSieve = DtSieve.Select("sieve_name ='" + Val.ToString(DRow["sieve"]) + "'").CopyToDataTable();
                            //        }
                            //    }
                            //}

                            objPrcImpProperty = new PriceImportProperty();
                            if (flag_check == 0)
                            {
                                objPrcImpProperty.rate_date = Val.DBDate(dtpRateDate.Text);
                            }
                            else
                            {
                                objPrcImpProperty.rate_date = Val.DBDate(dtpEntryDate.Text);
                            }
                            objPrcImpProperty.rate_type_id = Val.ToInt32(lueRateType.EditValue);
                            objPrcImpProperty.currency_id = Val.ToInt32(lueCurrency.EditValue);
                            if (m_RateId > 0)
                            {
                                objPrcImpProperty.rate_id = Val.ToInt(m_RateId);
                            }
                            else
                            {
                                objPrcImpProperty.rate_id = Val.ToInt(Rate_id);
                            }
                            //objPrcImpProperty.rate_detail_id = Val.ToInt64(DRow["rate_detail_id"]);
                            objPrcImpProperty.flag = Val.ToInt(DRow["flag"]);
                            objPrcImpProperty.assort_id = Val.ToInt(DRow["assort_id"]);
                            objPrcImpProperty.sieve_id = Val.ToInt(DRow["sieve_id"]);
                            objPrcImpProperty.rate = Val.ToDecimal(DRow["rate"]);
                            objPrcImpProperty.form_id = m_numForm_id;
                            objPrcImpProperty.count = 0;
                            i++;
                            objPrcImpProperty.sequence_no = i;
                            IntRes = ObjPrcImp.DetailSave(objPrcImpProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                            Count++;
                            IntCounter++;
                            IntRes++;
                            SetControlPropertyValue(lblProgressCount, "Text", Count.ToString() + "" + "/" + "" + TotalCount.ToString() + " Completed....");
                        }
                        //}

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
        private void backgroundWorker_PriceImport_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
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
                    dtpRateDate.Focus();
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
                            dgvPriceImport.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvPriceImport.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvPriceImport.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvPriceImport.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvPriceImport.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvPriceImport.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvPriceImport.ExportToCsv(Filepath);
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

        private void dtpDate_EditValueChanged(object sender, EventArgs e)
        {
            m_RateId = 0;
            if (Val.ToString(dtpRateDate.Text) != null && Val.ToInt(lueRateType.EditValue) != 0 && Val.ToInt(lueCurrency.EditValue) != 0)
            {
                DataTable dtRate = new DataTable();
                dtRate = objRateType.GetRateData(Val.ToString(dtpRateDate.Text), Val.ToInt(lueRateType.EditValue), Val.ToInt(lueCurrency.EditValue));
                if (dtRate.Rows.Count > 0)
                {
                    m_dtbPriceImport = new DataTable();
                    m_dtbPriceImport.Columns.Add("sieve_id", typeof(int)).DefaultValue = 0;
                    m_dtbPriceImport.Columns.Add("sieve", typeof(string));
                    m_dtbPriceImport.Columns.Add("assort_id", typeof(int)).DefaultValue = 0;
                    m_dtbPriceImport.Columns.Add("assort", typeof(string));
                    m_dtbPriceImport.Columns.Add("rate", typeof(decimal)).DefaultValue = 0;
                    grdPriceImport.DataSource = m_dtbPriceImport;
                    m_RateId = Val.ToInt(dtRate.Rows[0]["rate_id"]);
                    //dgvPriceImport.Columns["assort"].OptionsColumn.ReadOnly = true;
                    //dgvPriceImport.Columns["assort"].OptionsColumn.AllowFocus = false;
                    //dgvPriceImport.Columns["sieve"].OptionsColumn.ReadOnly = true;
                    //dgvPriceImport.Columns["sieve"].OptionsColumn.AllowFocus = false;
                }
                else
                {
                    grdPriceImport.DataSource = null;
                    m_RateId = 0;
                }
            }
        }

        private void lueRateType_EditValueChanged(object sender, EventArgs e)
        {
            m_RateId = 0;

            if (Val.ToString(dtpRateDate.Text) != null && Val.ToInt(lueRateType.EditValue) != 0 && Val.ToInt(lueCurrency.EditValue) != 0)
            {
                //DataTable dtRate = new DataTable();
                //dtRate = objRateType.GetRateData("", Val.ToInt(lueRateType.EditValue), Val.ToInt(lueCurrency.EditValue));

                //if (dtRate.Rows.Count > 0)
                //{
                //    m_dtbPriceImport = new DataTable();
                //    m_dtbPriceImport.Columns.Add("sieve_id", typeof(int)).DefaultValue = 0;
                //    m_dtbPriceImport.Columns.Add("sieve", typeof(string));
                //    m_dtbPriceImport.Columns.Add("assort_id", typeof(int)).DefaultValue = 0;
                //    m_dtbPriceImport.Columns.Add("assort", typeof(string));
                //    m_dtbPriceImport.Columns.Add("rate", typeof(decimal)).DefaultValue = 0;
                //    grdPriceImport.DataSource = m_dtbPriceImport;

                //    m_RateId = Val.ToInt(dtRate.Rows[0]["rate_id"]);

                //    dtpRateDate.Text = Val.DBDate(dtRate.Rows[0]["rate_date"].ToString());
                //    dgvPriceImport.Columns["assort"].OptionsColumn.ReadOnly = true;
                //    dgvPriceImport.Columns["assort"].OptionsColumn.AllowFocus = false;
                //    dgvPriceImport.Columns["sieve"].OptionsColumn.ReadOnly = true;
                //    dgvPriceImport.Columns["sieve"].OptionsColumn.AllowFocus = false;
                //}
                //else
                //{
                //    grdPriceImport.DataSource = null;
                //    m_RateId = 0;
                //}



                dtpRateDate.Properties.Items.Clear();
                RateType_MasterProperty RateTypeProperty = new RateType_MasterProperty();
                RateTypeProperty.ratetype_id = Val.ToInt(lueRateType.EditValue);
                DataTable DTab = ObjRateType.GetDate(RateTypeProperty);

                if (DTab.Rows.Count > 0)
                {
                    m_dtbPriceImport = new DataTable();
                    m_dtbPriceImport.Columns.Add("sieve_id", typeof(int)).DefaultValue = 0;
                    m_dtbPriceImport.Columns.Add("sieve", typeof(string));
                    m_dtbPriceImport.Columns.Add("assort_id", typeof(int)).DefaultValue = 0;
                    m_dtbPriceImport.Columns.Add("assort", typeof(string));
                    m_dtbPriceImport.Columns.Add("rate", typeof(decimal)).DefaultValue = 0;
                    grdPriceImport.DataSource = m_dtbPriceImport;


                    if (DTab.Rows.Count > 0)
                    {
                        foreach (DataRow DRow in DTab.Rows)
                        {
                            dtpRateDate.Properties.Items.Add(Convert.ToDateTime(DRow[0]).ToString("dd/MMM/yyyy"));
                        }
                        if (dtpRateDate.Properties.Items.Count >= 1)
                        {
                            dtpRateDate.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        dtpRateDate.Properties.Items.Clear();
                        dtpRateDate.EditValue = null;
                    }
                    RateTypeProperty = null;
                }
                else
                {
                    grdPriceImport.DataSource = null;
                    m_RateId = 0;
                }
            }
        }

        private void lueCurrency_EditValueChanged(object sender, EventArgs e)
        {
            m_RateId = 0;
            if (Val.ToString(dtpRateDate.Text) != null && Val.ToInt(lueRateType.EditValue) != 0 && Val.ToInt(lueCurrency.EditValue) != 0)
            {
                //DataTable dtRate = new DataTable();
                //dtRate = objRateType.GetRateData("", Val.ToInt(lueRateType.EditValue), Val.ToInt(lueCurrency.EditValue));

                dtpRateDate.Properties.Items.Clear();

                RateType_MasterProperty RateTypeProperty = new RateType_MasterProperty();
                RateTypeProperty.ratetype_id = Val.ToInt(lueRateType.EditValue);
                DataTable DTab = ObjRateType.GetDate(RateTypeProperty);

                if (DTab.Rows.Count > 0)
                {
                    m_dtbPriceImport = new DataTable();
                    m_dtbPriceImport.Columns.Add("sieve_id", typeof(int)).DefaultValue = 0;
                    m_dtbPriceImport.Columns.Add("sieve", typeof(string));
                    m_dtbPriceImport.Columns.Add("assort_id", typeof(int)).DefaultValue = 0;
                    m_dtbPriceImport.Columns.Add("assort", typeof(string));
                    m_dtbPriceImport.Columns.Add("rate", typeof(decimal)).DefaultValue = 0;
                    grdPriceImport.DataSource = m_dtbPriceImport;

                    //m_RateId = Val.ToInt(dtRate.Rows[0]["rate_id"]);
                    //dtpRateDate.Text = Val.DBDate(dtRate.Rows[0]["rate_date"].ToString());

                    if (DTab.Rows.Count > 0)
                    {
                        foreach (DataRow DRow in DTab.Rows)
                        {
                            dtpRateDate.Properties.Items.Add(Convert.ToDateTime(DRow[0]).ToString("dd/MMM/yyyy"));
                        }
                        if (dtpRateDate.Properties.Items.Count >= 1)
                        {
                            dtpRateDate.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        dtpRateDate.Properties.Items.Clear();
                        dtpRateDate.EditValue = null;
                    }
                    RateTypeProperty = null;


                }
                else
                {
                    grdPriceImport.DataSource = null;
                    m_RateId = 0;
                }
            }
        }

        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (lueCurrency.Text == "")
                {
                    lstError.Add(new ListError(13, "Currency"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueCurrency.Focus();
                    }
                }
                if (lueRateType.Text == "")
                {
                    lstError.Add(new ListError(13, "Rate Type"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueRateType.Focus();
                    }
                }
                if (dtpRateDate.Text == "")
                {
                    lstError.Add(new ListError(13, "Date"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        dtpRateDate.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                lstError.Add(new ListError(ex));
            }
            return (!(BLL.General.ShowErrors(lstError)));
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (!ValidateDetails())
            {
                return;
            }
            flag_check = 0;
            DataTable dtRate = new DataTable();
            dtRate = objRateType.GetRateDateWiseData(Val.DBDate(dtpRateDate.Text), Val.ToInt(lueRateType.EditValue), Val.ToInt(lueCurrency.EditValue), Val.ToString(CmbColorName.Text));
            if (dtRate.Rows.Count > 0)
            {
                grdPriceImport.DataSource = dtRate;
                //dgvPriceImport.BestFitColumns();
            }
            else
            {
                grdPriceImport.DataSource = null;
            }
        }
        private void CalculateGridAmount(int rowindex)
        {
            try
            {
                decimal rate = Val.ToDecimal(dgvPriceImport.GetRowCellValue(rowindex - 1, "rate").ToString());
                decimal old_rate = Val.ToDecimal(dgvPriceImport.GetRowCellValue(rowindex - 1, "old_rate").ToString());

                if (rate != old_rate)
                {
                    dgvPriceImport.SetRowCellValue(rowindex - 1, "flag", 1);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void dgvPriceImport_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            CalculateGridAmount(dgvPriceImport.FocusedRowHandle);
        }

        private void dgvPriceImport_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            CalculateGridAmount(dgvPriceImport.FocusedRowHandle);
        }

        private void dgvPriceImport_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            CalculateGridAmount(dgvPriceImport.FocusedRowHandle);
        }

        private void RepDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (Global.Confirm("Are you sure delete selected row?", "Price", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                PriceImportProperty PriceImportProperty = new PriceImportProperty();
                int IntRes = 0;
                Int64 Rate_Detail_ID = Val.ToInt64(dgvPriceImport.GetFocusedRowCellValue("rate_detail_id").ToString());
                PriceImportProperty.rate_detail_id = Val.ToInt64(Rate_Detail_ID);

                if (Rate_Detail_ID == 0)
                {
                    dgvPriceImport.DeleteRow(dgvPriceImport.GetRowHandle(dgvPriceImport.FocusedRowHandle));
                }
                else
                {
                    IntRes = ObjPrcImp.DeletePriceImportDetail(PriceImportProperty);
                    dgvPriceImport.DeleteRow(dgvPriceImport.GetRowHandle(dgvPriceImport.FocusedRowHandle));
                }

                if (IntRes == -1)
                {
                    Global.Confirm("Error in Detail Deleted Data.");
                    PriceImportProperty = null;
                }
                else
                {
                    Global.Confirm("Detail Deleted successfully...");
                    PriceImportProperty = null;
                }
            }
        }

        private void dgvPriceImport_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter && dgvPriceImport.FocusedColumn.Caption == "Rate")
            //{
            //    //dgvPriceImport.CloseEditor();
            //    // dgvPriceImport.UpdateCurrentRow();
            //    dgvPriceImport.FocusedRowHandle = 4;
            //    //dgvPriceImport.ShowEditor();
            //}
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

                PrinterSettingsUsing pst = new PrinterSettingsUsing();

                PrintSystem.PageSettings.AssignDefaultPrinterSettings(pst);

                PrintableComponentLink link = new PrintableComponentLink(PrintSystem);

                link.Component = grdPriceImport;

                //foreach (System.Drawing.Printing.PaperKind foo in Enum.GetValues(typeof(System.Drawing.Printing.PaperKind)))
                //{
                //    if (Val.ToString(CmbPageKind.SelectedItem) == foo.ToString())
                //    {
                //        link.PaperKind = foo;
                //        link.PaperName = foo.ToString();
                //    }
                //}

                //if (Val.ToString(cmbOrientation.SelectedItem) == "Landscape")
                //{
                //    link.Landscape = true;
                //}
                //if (Val.ToString(cmbExpand.SelectedItem) == "Yes")
                //{
                //    GridView1.OptionsPrint.ExpandAllGroups = true;
                //}
                //else
                //{
                //    GridView1.OptionsPrint.ExpandAllGroups = false;
                //}

                dgvPriceImport.OptionsPrint.AutoWidth = true;

                link.Margins.Left = 40;
                link.Margins.Right = 40;
                link.Margins.Bottom = 40;
                link.Margins.Top = 130;
                link.CreateMarginalHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);
                link.CreateMarginalFooterArea += new CreateAreaEventHandler(Link_CreateMarginalFooterArea);
                link.CreateDocument();
                link.ShowPreview();
                link.PrintDlg();
            }
            catch (Exception EX)
            {
                Global.Message(EX.Message);
            }
        }
        public void Link_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {
            // ' For Report Title
            TextBrick BrickTitle = e.Graph.DrawString("Price Import", System.Drawing.Color.Navy, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 20), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitle.Font = new Font("Tahoma", 12, FontStyle.Bold);
            BrickTitle.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitle.VertAlignment = DevExpress.Utils.VertAlignment.Center;

            // ' For Group 
            //TextBrick BrickTitleseller = e.Graph.DrawString("Group :- " + lblGroupBy.Text, System.Drawing.Color.Navy, new RectangleF(0, 25, e.Graph.ClientPageSize.Width, 20), DevExpress.XtraPrinting.BorderSide.None);
            //BrickTitleseller.Font = new Font("Tahoma", 8, FontStyle.Bold);
            //BrickTitleseller.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            //BrickTitleseller.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            //BrickTitleseller.ForeColor = Color.Black;

            //// ' For Filter 
            //TextBrick BrickTitlesParam = e.Graph.DrawString("Parameters :- " + lblFilter.Text, System.Drawing.Color.Navy, new RectangleF(0, 40, e.Graph.ClientPageSize.Width, 60), DevExpress.XtraPrinting.BorderSide.None);
            //BrickTitlesParam.Font = new Font("Tahoma", 8, FontStyle.Bold);
            //BrickTitlesParam.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            //BrickTitlesParam.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            //BrickTitlesParam.ForeColor = Color.Black;

            //int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 400, 0));
            //TextBrick BrickTitledate = e.Graph.DrawString("Print Date :- " + lblDateTime.Text, System.Drawing.Color.Navy, new RectangleF(IntX, 25, 400, 18), DevExpress.XtraPrinting.BorderSide.None);
            //BrickTitledate.Font = new Font("Tahoma", 8, FontStyle.Bold);
            //BrickTitledate.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            //BrickTitledate.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            //BrickTitledate.ForeColor = Color.Black;
        }
        public void Link_CreateMarginalFooterArea(object sender, CreateAreaEventArgs e)
        {
            int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 100, 0));

            PageInfoBrick BrickPageNo = e.Graph.DrawPageInfo(PageInfo.NumberOfTotal, "Page {0} of {1}", System.Drawing.Color.Navy, new RectangleF(IntX, 0, 100, 15), DevExpress.XtraPrinting.BorderSide.None);
            BrickPageNo.LineAlignment = BrickAlignment.Far;
            BrickPageNo.Alignment = BrickAlignment.Far;
            BrickPageNo.Font = new Font("Tahoma", 8, FontStyle.Bold); ;
            BrickPageNo.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            BrickPageNo.VertAlignment = DevExpress.Utils.VertAlignment.Center;
        }

        private void lblFormatSampleMumbai_Click(object sender, EventArgs e)
        {
            Global.CopyFormat(System.Windows.Forms.Application.StartupPath + @"\FORMAT\PriceImportMumbai.xlsx", "PriceImportMumbai.xlsx", "xlsx");
        }
    }
}
