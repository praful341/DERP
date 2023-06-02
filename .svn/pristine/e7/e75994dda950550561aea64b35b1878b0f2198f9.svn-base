using BLL;
using BLL.FunctionClasses.Report;
using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.PropertyClasses.Report;
using BLL.PropertyClasses.Transaction;
using BLL.PropertyClasses.Transaction.MFG;
using DERP.Class;
using DERP.Report;
using DevExpress.Diagram.Core;
using DevExpress.XtraEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using static DERP.Class.Global;
using static DERP.Master.FrmPartyMaster;

namespace DERP.Transaction.MFG
{
    public partial class FrmMFGDepartmentCosting : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;
        MFGDepartmentCosting objDepartmentCosting;
        Control _NextEnteredControl;
        private List<Control> _tabControls;

        FillCombo ObjFillCombo = new FillCombo();
        DataTable DtControlSettings;
        DataTable dtTemp;
        DataTable m_dtbParam;
        DataTable m_dtCut;
        DataTable m_DtProcess;
        DataTable m_dtbKapan;
        DataTable m_dtOutstanding;
        DataTable DtCostingDepartmentEntry = new DataTable();
        DataTable DtDepartmentCosting = new DataTable();
        ReportParams ObjReportParams = new ReportParams();
        ReportParams_Property ReportParams_Property = new BLL.PropertyClasses.Report.ReportParams_Property();
        Int64 m_numForm_id;
        Int64 IntRes;
        Int64 Lot_SrNo = 0;

        string StrListTempPurity = string.Empty;

        #endregion

        #region Constructor
        public FrmMFGDepartmentCosting()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            _NextEnteredControl = new Control();
            _tabControls = new List<Control>();

            objDepartmentCosting = new MFGDepartmentCosting();
            DtControlSettings = new DataTable();
            dtTemp = new DataTable();
            m_dtbParam = new DataTable();
            m_dtCut = new DataTable();
            m_DtProcess = new DataTable();
            m_dtOutstanding = new DataTable();
            m_dtbKapan = new DataTable();
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
            objBOFormEvents.ObjToDispose.Add("");
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion

        #region Events
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ObjPer.SetFormPer();
                if (ObjPer.AllowUpdate == false || ObjPer.AllowInsert == false)
                {
                    Global.Message(BLL.GlobalDec.gStrPermissionInsUpdMsg);
                    return;
                }

                //string Str = "";
                //if (Val.DBDate(BLL.GlobalDec.gStrServerDate) != Val.DBDate(dtpCostingDate.Text))
                //{
                //    Str = GlobalDec.CheckLockIsOpenOrNot(Val.DBDate(dtpCostingDate.Text), Val.DBTime(DateTime.Now.ToShortTimeString())); //Val.GetFullTime12());
                //    if (Str != "YES")
                //    {
                //        if (Str != "")
                //        {
                //            Global.Message(Str);
                //            return;
                //        }
                //        else
                //        {
                //            Global.Message("You Are Not Suppose to Make Entry On Different Date");
                //            return;
                //        }
                //    }
                //    else
                //    {
                //        dtpCostingDate.Enabled = true;
                //        dtpCostingDate.Visible = true;
                //    }
                //}
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

                if (!ValidateDetails())
                {
                    return;
                }
                DialogResult result = MessageBox.Show("Do you want to save Department Costing data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result != DialogResult.Yes)
                {
                    btnSave.Enabled = true;
                    return;
                }

                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;

                panelProgress.Visible = true;
                backgroundWorker_DepartmentCosting.RunWorkerAsync();

                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearDetails();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable DTab_List = new DataTable();
                MFGDepartmentCosting objMFGDepartmentCosting = new MFGDepartmentCosting();
                MFGDepartmentCostingProperty objMFGDepartmentCostingProperty = new MFGDepartmentCostingProperty();

                objMFGDepartmentCostingProperty.department_id = Val.ToInt64(0);

                DTab_List = objMFGDepartmentCosting.GetDepartmentCostingData(objMFGDepartmentCostingProperty);

                //XmlTextWriter xmlWriter = new XmlTextWriter(@"C:\MyDataset.xml", Encoding.UTF8);
                //xmlWriter.WriteStartDocument(true);
                //xmlWriter.WriteStartElement("Galaxy"); //Root Element
                //xmlWriter.WriteStartElement("StoneCollection"); //Department Element

                //xmlWriter.WriteStartAttribute("version"); //Attribute "Name"
                //xmlWriter.WriteString("1.0.0"); //Attribute Value 
                //xmlWriter.WriteEndAttribute();


                //xmlWriter.WriteStartElement("stones"); //Started Employees Element
                //foreach (DataRow DR in DTab_List.Rows)
                //{
                //    xmlWriter.WriteStartElement("stone"); //Started Employee Element

                //    xmlWriter.WriteStartAttribute("package_name"); //Attribute "Name"
                //    xmlWriter.WriteString(Val.ToString(DR["package_name"])); //Attribute Value 
                //    xmlWriter.WriteEndAttribute();

                //    xmlWriter.WriteStartAttribute("stone_name");//Attribute "Age"
                //    xmlWriter.WriteString(Val.ToString(DR["stone_name"]));//Attribute Value
                //    xmlWriter.WriteEndAttribute();

                //    xmlWriter.WriteStartAttribute("process_type");//Attribute "Age"
                //    xmlWriter.WriteString(Val.ToString(DR["process_type"]));//Attribute Value
                //    xmlWriter.WriteEndAttribute();

                //    xmlWriter.WriteStartAttribute("weight");//Attribute "Age"
                //    xmlWriter.WriteString(Val.ToString(DR["weight"]));//Attribute Value
                //    xmlWriter.WriteEndAttribute();

                //    xmlWriter.WriteEndElement(); //End of Employee Element                    
                //}

                //xmlWriter.WriteEndElement(); //End of Employees Element
                //xmlWriter.WriteEndElement(); //End of Department Element
                //xmlWriter.WriteEndElement(); //End of Root Element

                //xmlWriter.WriteEndDocument();
                //xmlWriter.Flush();
                //xmlWriter.Close();

                FrmMFGDepartmentCostingStock FrmMFGDepartmentCostingStock = new FrmMFGDepartmentCostingStock();
                FrmMFGDepartmentCostingStock.FrmMFGDepartmentCosting = this;
                FrmMFGDepartmentCostingStock.DTab = DTab_List;
                FrmMFGDepartmentCostingStock.ShowForm(this, DTab_List);
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
            }
        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (Val.ToInt(lblLotSRNo.Text) != 0)
            {
                DialogResult result = MessageBox.Show("Do you want to Delete Department Costing Data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result != DialogResult.Yes)
                {
                    btnSave.Enabled = true;
                    return;
                }

                IntRes = 0;

                MFGDepartmentCosting MFGDepartmentCosting = new MFGDepartmentCosting();
                MFGDepartmentCostingProperty objMFGDepartmentCostingProperty = new MFGDepartmentCostingProperty();

                objMFGDepartmentCostingProperty.lot_srno = Val.ToInt64(lblLotSRNo.Text);

                IntRes = MFGDepartmentCosting.GetDeleteDepartmentCostingEntry(objMFGDepartmentCostingProperty);

                if (IntRes > 0)
                {
                    Global.Confirm("Department Costing Entry Data Deleted Succesfully");
                    ClearDetails();
                    btnSave.Enabled = true;
                }
                else
                {
                    Global.Confirm("Error In Department Costing Entry Data");
                    btnSave.Enabled = true;
                }
            }
            else
            {
                Global.Confirm("Not Selected Any Data are Deleted..");
                btnSave.Enabled = true;
                return;
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateDetails())
                {
                    return;
                }

                MFGDepartmentCosting objMFGDepartmentCosting = new MFGDepartmentCosting();
                MFGDepartmentCostingProperty objMFGDepartmentCostingProperty = new MFGDepartmentCostingProperty();
                objMFGDepartmentCostingProperty.department_id = Val.ToInt64(lueDepartment.EditValue);
                objMFGDepartmentCostingProperty.lot_srno = Val.ToInt64(lblLotSRNo.Text);

                DtDepartmentCosting = objMFGDepartmentCosting.MFGDepartmentCostingGetData(objMFGDepartmentCostingProperty);
                lblLotSRNo.Text = "0";

                grdDepartmentCosting.DataSource = DtDepartmentCosting;

                dgvDepartmentCosting.OptionsView.ShowFooter = true;
                //dgvDepartmentCosting.BestFitColumns();
                dgvDepartmentCosting.FocusedColumn = dgvDepartmentCosting.Columns["amount"];
                dgvDepartmentCosting.ShowEditor();
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
            }
        }
        private void grdProcessReceive_ProcessGridKey(object sender, KeyEventArgs e)
        {
            //try
            //{
            //    if (e.KeyCode == Keys.Enter)
            //    {
            //        (grdDepartmentCosting.FocusedView as ColumnView).FocusedRowHandle++;
            //        e.Handled = true;
            //    }
            //}
            //catch { }
        }
        private void FrmMFGDepartmentCosting_Load(object sender, EventArgs e)
        {
            try
            {
                Global.LOOKUPDepartment_Costing(lueDepartment);

                dtpCostingDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpCostingDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpCostingDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpCostingDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpCostingDate.EditValue = DateTime.Now;

                DTPFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                DTPFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                DTPFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                DTPFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
                DTPFromDate.EditValue = DateTime.Now;

                DTPToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                DTPToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                DTPToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                DTPToDate.Properties.CharacterCasing = CharacterCasing.Upper;
                DTPToDate.EditValue = DateTime.Now;

                dtpCostingDate.Focus();
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }


        #endregion

        #region Functions
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (lueDepartment.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Department"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueDepartment.Focus();
                    }
                }
                if (txtYear.Text == "" || txtYear.Text == "0")
                {
                    lstError.Add(new ListError(13, "Year"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtYear.Focus();
                    }
                }
                if (txtMonth.Text == "" || txtMonth.Text == "0")
                {
                    lstError.Add(new ListError(13, "Month"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtMonth.Focus();
                    }
                }
                if (txtInPcs.Text.ToString() == "" || txtInPcs.Text.ToString() == "0")
                {
                    lstError.Add(new ListError(12, "Total Pcs"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtInPcs.Focus();
                    }
                }
                if (!objDepartmentCosting.ISExists(Val.ToInt64(lueDepartment.EditValue), Val.ToInt32(txtYear.Text), Val.ToInt32(txtMonth.Text), Val.ToInt64(lblLotSRNo.Text)).ToString().Trim().Equals(string.Empty))
                {
                    lstError.Add(new ListError(5, "This Year - " + Val.ToInt32(txtYear.Text) + " AND Month - " + Val.ToInt32(txtMonth.Text) + "  Data Already Exist.."));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtYear.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                lstError.Add(new ListError(ex));
            }
            return (!(BLL.General.ShowErrors(lstError)));
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
                    }
                }
                if ((Control)sender is CheckedComboBoxEdit)
                {
                    if (e.KeyChar == Convert.ToChar(Keys.Enter))
                    {
                        SendKeys.Send("{TAB}");
                    }
                }
                if ((Control)sender is ButtonEdit)
                {
                    if (e.KeyChar == Convert.ToChar(Keys.Enter))
                    {
                        SendKeys.Send("{TAB}");
                    }
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
                else if ((Control)sender is CheckedComboBoxEdit)
                {
                    ((CheckedComboBoxEdit)(Control)sender).ShowPopup();
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
                            dgvDepartmentCosting.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvDepartmentCosting.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvDepartmentCosting.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvDepartmentCosting.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvDepartmentCosting.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvDepartmentCosting.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvDepartmentCosting.ExportToCsv(Filepath);
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
        private bool Validate_PopUp()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();

            try
            {
                if (lueDepartment.Text == "")
                {
                    lstError.Add(new ListError(13, "Kapan No"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueDepartment.Focus();
                    }
                }
            }

            catch (Exception ex)
            {
                lstError.Add(new ListError(ex));
            }
            return (!(BLL.General.ShowErrors(lstError)));
        }
        public void GetStockData(DataTable Stock_Data)
        {
            try
            {
                DataTable DTab_PataLot = Stock_Data.Copy();

                lblLotSRNo.Text = Val.ToString(Val.ToDecimal(DTab_PataLot.Rows[0]["lot_srno"]));
                lueDepartment.EditValue = Val.ToInt32(DTab_PataLot.Rows[0]["department_id"]);
                txtTarget.Text = Val.ToDecimal(DTab_PataLot.Rows[0]["target"]).ToString();
                txtOpeningPcs.Text = Val.ToInt64(DTab_PataLot.Rows[0]["opening_pcs"]).ToString();
                txtInPcs.Text = Val.ToInt64(DTab_PataLot.Rows[0]["in_pcs"]).ToString();
                txtOutPcs.Text = Val.ToInt64(DTab_PataLot.Rows[0]["out_pcs"]).ToString();
                txtClosingPcs.Text = Val.ToInt64(DTab_PataLot.Rows[0]["closing_pcs"]).ToString();
                dtpCostingDate.Text = Val.DBDate(DTab_PataLot.Rows[0]["costing_date"].ToString());
                txtYear.Text = Val.ToInt32(DTab_PataLot.Rows[0]["year"]).ToString();
                txtMonth.Text = Val.ToInt32(DTab_PataLot.Rows[0]["month"]).ToString();

                MFGDepartmentCosting objMFGDepartmentCosting = new MFGDepartmentCosting();
                MFGDepartmentCostingProperty objMFGDepartmentCostingProperty = new MFGDepartmentCostingProperty();
                objMFGDepartmentCostingProperty.department_id = Val.ToInt64(lueDepartment.EditValue);
                objMFGDepartmentCostingProperty.year = Val.ToInt32(txtYear.EditValue);
                objMFGDepartmentCostingProperty.month = Val.ToInt32(txtMonth.EditValue);
                objMFGDepartmentCostingProperty.lot_srno = Val.ToInt64(lblLotSRNo.Text);

                DtDepartmentCosting = objMFGDepartmentCosting.MFGPataLotEntryGetDataList(objMFGDepartmentCostingProperty);

                grdDepartmentCosting.DataSource = DtDepartmentCosting;
                dgvDepartmentCosting.OptionsView.ShowFooter = true;
                dgvDepartmentCosting.BestFitColumns();
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
            }
        }
        private bool ClearDetails()
        {
            bool blnReturn = true;
            try
            {
                grdDepartmentCosting.DataSource = null;
                lueDepartment.EditValue = null;
                //txtYear.Text = "0";
                //txtMonth.Text = "0";
                txtOpeningPcs.Text = "0";
                txtInPcs.Text = "0";
                txtOutPcs.Text = "0";
                txtClosingPcs.Text = "0";
                txtTarget.Text = "0";
                lblLotSRNo.Text = "0";
                btnSearch.Enabled = true;
                lueDepartment.Focus();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
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

        #region Key Press Event

        #endregion

        private void backgroundWorker_DepartmentCosting_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                MFGDepartmentCosting MFGDepartmentCosting = new MFGDepartmentCosting();
                MFGDepartmentCostingProperty objMFGDepartmentCostingProperty = new MFGDepartmentCostingProperty();

                Conn = new BeginTranConnection(true, false);

                DataTable m_DTab = new DataTable();
                ArrayList MyArrayList = new ArrayList();

                IntRes = 0;
                Lot_SrNo = 0;
                try
                {
                    m_DTab = (DataTable)grdDepartmentCosting.DataSource;

                    objMFGDepartmentCostingProperty.department_id = Val.ToInt64(lueDepartment.EditValue);
                    objMFGDepartmentCostingProperty.costing_date = Val.DBDate(dtpCostingDate.Text);

                    // objMFGPataLotEntryProperty.carat = Val.ToDecimal(txtTotalCarat.Text);
                    objMFGDepartmentCostingProperty.year = Val.ToInt(txtYear.Text);
                    objMFGDepartmentCostingProperty.month = Val.ToInt(txtMonth.Text.ToString());
                    objMFGDepartmentCostingProperty.opening_pcs = Val.ToInt64(txtOpeningPcs.Text.ToString());
                    objMFGDepartmentCostingProperty.in_pcs = Val.ToInt64(txtInPcs.Text.ToString());
                    objMFGDepartmentCostingProperty.out_pcs = Val.ToInt64(txtOutPcs.Text.ToString());
                    objMFGDepartmentCostingProperty.closing_pcs = Val.ToInt64(txtClosingPcs.Text.ToString());
                    objMFGDepartmentCostingProperty.target = Val.ToDecimal(txtTarget.Text);

                    int month = Val.ToInt(txtMonth.Text.ToString());

                    if (month > 9)
                    {
                        objMFGDepartmentCostingProperty.yearmonth = Val.ToInt(Val.ToString(txtYear.Text) + Val.ToString(month));
                    }
                    else
                    {
                        int Middle = Val.ToInt(0);
                        objMFGDepartmentCostingProperty.yearmonth = Val.ToInt(Val.ToString(txtYear.Text) + Val.ToString("0") + Val.ToString(month));
                    }

                    if (lblLotSRNo.Text.ToString() != "0")
                    {
                        Lot_SrNo = Val.ToInt64(lblLotSRNo.Text);
                    }
                    else
                    {
                        Lot_SrNo = 0;
                    }

                    if (m_DTab.Rows.Count > 0)
                    {
                        int IntCounter = 0;
                        int Count = 0;
                        int TotalCount = m_DTab.Rows.Count;
                        for (int i = 0; i <= m_DTab.Rows.Count - 1; i++)
                        {
                            objMFGDepartmentCostingProperty.lot_srno = Lot_SrNo;

                            objMFGDepartmentCostingProperty.department_costing_id = Val.ToInt64(m_DTab.Rows[i]["department_costing_id"]);
                            objMFGDepartmentCostingProperty.ledger_id = Val.ToInt64(m_DTab.Rows[i]["ledger_id"]);

                            objMFGDepartmentCostingProperty.amount = Val.ToDecimal(m_DTab.Rows[i]["amount"]);

                            objMFGDepartmentCostingProperty = MFGDepartmentCosting.Save(objMFGDepartmentCostingProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                            Lot_SrNo = Val.ToInt64(objMFGDepartmentCostingProperty.lot_srno);

                            Count++;
                            IntCounter++;
                            IntRes++;
                            SetControlPropertyValue(lblProgressCount, "Text", Count.ToString() + "" + "/" + "" + TotalCount.ToString() + " Completed....");
                        }
                    }
                    Conn.Inter1.Commit();
                }
                catch (Exception ex)
                {
                    IntRes = -1;
                    Conn.Inter1.Rollback();
                    Conn = null;
                    General.ShowErrors(ex.ToString());
                    return;
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
        private void backgroundWorker_DepartmentCosting_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                panelProgress.Visible = false;

                if (IntRes > 0)
                {
                    DialogResult result = MessageBox.Show("Department Costing Data Save Succesfully");
                    ClearDetails();
                    btnSave.Enabled = true;
                }
                else
                {
                    Global.Confirm("Error in Department Costing Data");
                    btnSave.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
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
        private void lueDepartment_EditValueChanged(object sender, EventArgs e)
        {
            MFGDepartmentCosting objMFGDepartmentCosting = new MFGDepartmentCosting();
            MFGDepartmentCostingProperty objMFGDepartmentCostingProperty = new MFGDepartmentCostingProperty();
            objMFGDepartmentCostingProperty.department_id = Val.ToInt64(lueDepartment.EditValue);

            DtCostingDepartmentEntry = objDepartmentCosting.MFGDepartmentWiseCostingData(objMFGDepartmentCostingProperty);

            if (DtCostingDepartmentEntry.Rows.Count > 0)
            {
                txtTarget.Text = Val.ToDecimal(DtCostingDepartmentEntry.Rows[0]["target"]).ToString();
            }
            else
            {
                txtTarget.Text = "0";
            }
        }
        //private bool ValidatePrintDetails()
        //{
        //    bool blnFocus = false;
        //    List<ListError> lstError = new List<ListError>();
        //    try
        //    {
        //        //if (txtYear.Text == "")
        //        //{
        //        //    lstError.Add(new ListError(13, "Year"));
        //        //    if (!blnFocus)
        //        //    {
        //        //        blnFocus = true;
        //        //        txtYear.Focus();
        //        //    }
        //        //}
        //        //if (txtMonth.Text == "")
        //        //{
        //        //    lstError.Add(new ListError(13, "Month"));
        //        //    if (!blnFocus)
        //        //    {
        //        //        blnFocus = true;
        //        //        txtMonth.Focus();
        //        //    }
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        lstError.Add(new ListError(ex));
        //    }
        //    return (!(BLL.General.ShowErrors(lstError)));
        //}
        private void btnPrint_Click(object sender, EventArgs e)
        {
            //if (!ValidatePrintDetails())
            //{
            //    return;
            //}

            DataSet DtPataLotPrintData = new DataSet();
            MFGDepartmentCosting objMFGDepartmentCosting = new MFGDepartmentCosting();
            MFGDepartmentCostingProperty objMFGDepartmentCostingProperty = new MFGDepartmentCostingProperty();

            objMFGDepartmentCostingProperty.year = Val.ToInt(txtYear.Text);
            objMFGDepartmentCostingProperty.month = Val.ToInt(txtMonth.Text);
            objMFGDepartmentCostingProperty.from_date = Val.DBDate(DTPFromDate.Text);
            objMFGDepartmentCostingProperty.to_date = Val.DBDate(DTPToDate.Text);

            DtPataLotPrintData = objMFGDepartmentCosting.MFGDepartmentCostingPrintGetData(objMFGDepartmentCostingProperty);

            //////////////// Start Calculation ///////////////////////

            if (DtPataLotPrintData.Tables[0].Rows.Count > 0)
            {
                DtPataLotPrintData.Tables[0].Rows[0]["sales_remarks"] = "BY SALES MAKABLE";

                object sumObject;
                sumObject = DtPataLotPrintData.Tables[0].Compute("Sum(total_pcs)", string.Empty);
                DtPataLotPrintData.Tables[0].Rows[0]["sales"] = Val.ToDecimal(sumObject) * Val.ToDecimal(DtPataLotPrintData.Tables[0].Rows[1]["target"]);

                decimal Sales = Val.ToDecimal(DtPataLotPrintData.Tables[0].Rows[0]["sales"]);
                //decimal Sales = Val.ToDecimal(sumObject);
                object decTotal = DtPataLotPrintData.Tables[0].Compute("Sum(amount)", "").ToString();
                decimal Amount = Sales - Val.ToDecimal(decTotal);

                if (Amount > 0)
                {
                    decimal Profit_Amount = Sales - Val.ToDecimal(decTotal);
                    DtPataLotPrintData.Tables[0].Rows[1]["profit_amount"] = Profit_Amount;
                }
                else
                {
                    DtPataLotPrintData.Tables[0].Rows[1]["profit_amount"] = 0;
                }

                if (Amount < 0)
                {
                    decimal Diff_Amount = Val.ToDecimal(decTotal) - Sales;
                    DtPataLotPrintData.Tables[0].Rows[1]["sales_remarks"] = "BY LOSS";
                    DtPataLotPrintData.Tables[0].Rows[1]["sales"] = Diff_Amount;
                }
                else
                {
                    DtPataLotPrintData.Tables[0].Rows[1]["sales"] = 0;
                }
            }
            if (DtPataLotPrintData.Tables[1].Rows.Count > 0)
            {
                DtPataLotPrintData.Tables[1].Rows[0]["sales_remarks"] = "BY SALES MAKABLE";

                object sumObject;
                sumObject = DtPataLotPrintData.Tables[1].Compute("Sum(total_pcs)", string.Empty);
                DtPataLotPrintData.Tables[1].Rows[0]["sales"] = Val.ToDecimal(sumObject) * Val.ToDecimal(DtPataLotPrintData.Tables[1].Rows[1]["target"]);

                decimal Sales = Val.ToDecimal(DtPataLotPrintData.Tables[1].Rows[0]["sales"]);
                object decTotal = DtPataLotPrintData.Tables[1].Compute("Sum(amount)", "").ToString();
                decimal Amount = Sales - Val.ToDecimal(decTotal);

                if (Amount > 0)
                {
                    decimal Profit_Amount = Sales - Val.ToDecimal(decTotal);
                    DtPataLotPrintData.Tables[1].Rows[1]["profit_amount"] = Profit_Amount;
                }
                else
                {
                    DtPataLotPrintData.Tables[1].Rows[1]["profit_amount"] = 0;
                }

                if (Amount < 0)
                {
                    decimal Diff_Amount = Val.ToDecimal(decTotal) - Sales;
                    DtPataLotPrintData.Tables[1].Rows[1]["sales_remarks"] = "BY LOSS";
                    DtPataLotPrintData.Tables[1].Rows[1]["sales"] = Diff_Amount;
                }
                else
                {
                    DtPataLotPrintData.Tables[1].Rows[1]["sales"] = 0;
                }
            }
            if (DtPataLotPrintData.Tables[2].Rows.Count > 0)
            {
                DtPataLotPrintData.Tables[2].Rows[0]["sales_remarks"] = "BY SALES";

                object sumObject;
                sumObject = DtPataLotPrintData.Tables[2].Compute("Sum(total_pcs)", string.Empty);
                DtPataLotPrintData.Tables[2].Rows[0]["sales"] = Val.ToDecimal(sumObject) * Val.ToDecimal(DtPataLotPrintData.Tables[2].Rows[1]["target"]);

                decimal Sales = Val.ToDecimal(DtPataLotPrintData.Tables[2].Rows[0]["sales"]);
                object decTotal = DtPataLotPrintData.Tables[2].Compute("Sum(amount)", "").ToString();
                decimal Amount = Sales - Val.ToDecimal(decTotal);

                if (Amount > 0)
                {
                    decimal Profit_Amount = Sales - Val.ToDecimal(decTotal);
                    DtPataLotPrintData.Tables[2].Rows[1]["profit_amount"] = Profit_Amount;
                }
                else
                {
                    DtPataLotPrintData.Tables[2].Rows[1]["profit_amount"] = 0;
                }

                if (Amount < 0)
                {
                    decimal Diff_Amount = Val.ToDecimal(decTotal) - Sales;
                    DtPataLotPrintData.Tables[2].Rows[1]["sales_remarks"] = "BY LOSS";
                    DtPataLotPrintData.Tables[2].Rows[1]["sales"] = Diff_Amount;
                }
                else
                {
                    DtPataLotPrintData.Tables[2].Rows[1]["sales"] = 0;
                }
            }
            if (DtPataLotPrintData.Tables[3].Rows.Count > 0)
            {
                DtPataLotPrintData.Tables[3].Rows[0]["sales_remarks"] = "BY SALES";

                object sumObject;
                sumObject = DtPataLotPrintData.Tables[3].Compute("Sum(total_pcs)", string.Empty);
                DtPataLotPrintData.Tables[3].Rows[0]["sales"] = Val.ToDecimal(sumObject) * Val.ToDecimal(DtPataLotPrintData.Tables[3].Rows[1]["target"]);

                decimal Sales = Val.ToDecimal(DtPataLotPrintData.Tables[3].Rows[0]["sales"]);
                object decTotal = DtPataLotPrintData.Tables[3].Compute("Sum(amount)", "").ToString();
                decimal Amount = Sales - Val.ToDecimal(decTotal);

                if (Amount > 0)
                {
                    decimal Profit_Amount = Sales - Val.ToDecimal(decTotal);
                    DtPataLotPrintData.Tables[3].Rows[1]["profit_amount"] = Profit_Amount;
                }
                else
                {
                    DtPataLotPrintData.Tables[3].Rows[1]["profit_amount"] = 0;
                }

                if (Amount < 0)
                {
                    decimal Diff_Amount = Val.ToDecimal(decTotal) - Sales;
                    DtPataLotPrintData.Tables[3].Rows[1]["sales_remarks"] = "BY LOSS";
                    DtPataLotPrintData.Tables[3].Rows[1]["sales"] = Diff_Amount;
                }
                else
                {
                    DtPataLotPrintData.Tables[3].Rows[1]["sales"] = 0;
                }
            }
            if (DtPataLotPrintData.Tables[4].Rows.Count > 0)
            {
                DtPataLotPrintData.Tables[4].Rows[0]["sales_remarks"] = "BY SALES";

                object sumObject;
                sumObject = DtPataLotPrintData.Tables[4].Compute("Sum(total_pcs)", string.Empty);
                DtPataLotPrintData.Tables[4].Rows[0]["sales"] = Val.ToDecimal(sumObject) * Val.ToDecimal(DtPataLotPrintData.Tables[4].Rows[1]["target"]);

                decimal Sales = Val.ToDecimal(DtPataLotPrintData.Tables[4].Rows[0]["sales"]);
                object decTotal = DtPataLotPrintData.Tables[4].Compute("Sum(amount)", "").ToString();
                decimal Amount = Sales - Val.ToDecimal(decTotal);

                if (Amount > 0)
                {
                    decimal Profit_Amount = Sales - Val.ToDecimal(decTotal);
                    DtPataLotPrintData.Tables[4].Rows[1]["profit_amount"] = Profit_Amount;
                }
                else
                {
                    DtPataLotPrintData.Tables[4].Rows[1]["profit_amount"] = 0;
                }

                if (Amount < 0)
                {
                    decimal Diff_Amount = Val.ToDecimal(decTotal) - Sales;
                    DtPataLotPrintData.Tables[4].Rows[1]["sales_remarks"] = "BY LOSS";
                    DtPataLotPrintData.Tables[4].Rows[1]["sales"] = Diff_Amount;
                }
                else
                {
                    DtPataLotPrintData.Tables[4].Rows[1]["sales"] = 0;
                }
            }
            if (DtPataLotPrintData.Tables[5].Rows.Count > 0)
            {
                DtPataLotPrintData.Tables[5].Rows[0]["sales_remarks"] = "BY SALES MAKABLE";

                object sumObject;
                sumObject = DtPataLotPrintData.Tables[5].Compute("Sum(total_pcs)", string.Empty);
                DtPataLotPrintData.Tables[5].Rows[0]["sales"] = Val.ToDecimal(sumObject) * Val.ToDecimal(DtPataLotPrintData.Tables[5].Rows[1]["target"]);

                decimal Sales = Val.ToDecimal(DtPataLotPrintData.Tables[5].Rows[0]["sales"]);
                object decTotal = DtPataLotPrintData.Tables[5].Compute("Sum(amount)", "").ToString();
                decimal Amount = Sales - Val.ToDecimal(decTotal);

                if (Amount > 0)
                {
                    decimal Profit_Amount = Sales - Val.ToDecimal(decTotal);
                    DtPataLotPrintData.Tables[5].Rows[1]["profit_amount"] = Profit_Amount;
                }
                else
                {
                    DtPataLotPrintData.Tables[5].Rows[1]["profit_amount"] = 0;
                }

                if (Amount < 0)
                {
                    decimal Diff_Amount = Val.ToDecimal(decTotal) - Sales;
                    DtPataLotPrintData.Tables[5].Rows[1]["sales_remarks"] = "BY LOSS";
                    DtPataLotPrintData.Tables[5].Rows[1]["sales"] = Diff_Amount;
                }
                else
                {
                    DtPataLotPrintData.Tables[5].Rows[1]["sales"] = 0;
                }
            }
            if (DtPataLotPrintData.Tables[6].Rows.Count > 0)
            {
                DtPataLotPrintData.Tables[6].Rows[0]["sales_remarks"] = "BY SALES ASSORT";

                object sumObject;
                sumObject = DtPataLotPrintData.Tables[6].Compute("Sum(total_pcs)", string.Empty);
                DtPataLotPrintData.Tables[6].Rows[0]["sales"] = Val.ToDecimal(sumObject) * Val.ToDecimal(DtPataLotPrintData.Tables[6].Rows[1]["target"]);

                decimal Sales = Val.ToDecimal(DtPataLotPrintData.Tables[6].Rows[0]["sales"]);
                object decTotal = DtPataLotPrintData.Tables[6].Compute("Sum(amount)", "").ToString();
                decimal Amount = Sales - Val.ToDecimal(decTotal);

                if (Amount > 0)
                {
                    decimal Profit_Amount = Sales - Val.ToDecimal(decTotal);
                    DtPataLotPrintData.Tables[6].Rows[1]["profit_amount"] = Profit_Amount;
                }
                else
                {
                    DtPataLotPrintData.Tables[6].Rows[1]["profit_amount"] = 0;
                }

                if (Amount < 0)
                {
                    decimal Diff_Amount = Val.ToDecimal(decTotal) - Sales;
                    DtPataLotPrintData.Tables[6].Rows[1]["sales_remarks"] = "BY LOSS";
                    DtPataLotPrintData.Tables[6].Rows[1]["sales"] = Diff_Amount;
                }
                else
                {
                    DtPataLotPrintData.Tables[6].Rows[1]["sales"] = 0;
                }
            }

            //////////////// End Calculation ///////////////////////

            FrmReportViewer FrmReportViewer = new FrmReportViewer();
            //FrmReportViewer.DS.Tables.Add(DtPataLotPrintData);
            foreach (DataTable DTab in DtPataLotPrintData.Tables)
                FrmReportViewer.DS.Tables.Add(DTab.Copy());
            FrmReportViewer.GroupBy = "";
            FrmReportViewer.RepName = "";
            FrmReportViewer.RepPara = "";
            this.Cursor = Cursors.Default;
            FrmReportViewer.AllowSetFormula = true;

            //FrmReportViewer.ShowForm("Main_Report", 120, FrmReportViewer.ReportFolder.DEPARTMENT_COSTING);

            FrmReportViewer.ShowForm_SubReport("Main_Report", 120, FrmReportViewer.ReportFolder.DEPARTMENT_COSTING);

            DtPataLotPrintData = null;
            FrmReportViewer.DS.Tables.Clear();
            FrmReportViewer.DS.Clear();
            FrmReportViewer = null;
        }
        private void dgvDepartmentCosting_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dgvDepartmentCosting.FocusedColumn.Caption == "Amount")
            {
                if (dgvDepartmentCosting.IsLastRow)
                {
                    btnSave_Click(null, null);
                }
            }
        }
        private void RepAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S)
            {
                btnSave_Click(null, null);
            }
        }

        private void txtOpeningPcs_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtClosingPcs.Text = string.Format("{0:0}", Val.ToInt64(txtOpeningPcs.Text) + Val.ToInt64(txtInPcs.Text) - Val.ToInt64(txtOutPcs.Text));
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }

        private void txtInPcs_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtClosingPcs.Text = string.Format("{0:0}", Val.ToInt64(txtOpeningPcs.Text) + Val.ToInt64(txtInPcs.Text) - Val.ToInt64(txtOutPcs.Text));
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }

        private void txtOutPcs_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtClosingPcs.Text = string.Format("{0:0}", Val.ToInt64(txtOpeningPcs.Text) + Val.ToInt64(txtInPcs.Text) - Val.ToInt64(txtOutPcs.Text));
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }

        private void txtOpeningPcs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtInPcs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtOutPcs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnPopUpStock_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            var startDate = new DateTime();
            var endDate = new DateTime();
            if (Val.ToInt(txtYear.Text) != 0 && Val.ToInt(txtMonth.Text) != 0)
            {
                DateTime now = DateTime.Now;
                startDate = new DateTime(Val.ToInt(txtYear.Text), Val.ToInt(txtMonth.Text), 1);
                endDate = startDate.AddMonths(1).AddDays(-1);
            }
            else
            {
                Global.Message("Select Year And Month");
                return;
            }
            ReportParams_Property.Department_Type = Val.ToString(lueDepartment.Text);
            ReportParams_Property.From_Date = Val.DBDate(startDate.ToString());
            ReportParams_Property.To_Date = Val.DBDate(endDate.ToString());
            DataTable DTab = ObjReportParams.GetMFGLiveStockASOnDate(ReportParams_Property, "MFG_TRN_Stock_BackDate_Costing");

            if (DTab.Rows.Count > 0)
            {
                this.Cursor = Cursors.Default;
                txtOpeningPcs.Text = Val.ToInt64(DTab.Rows[0]["op_pcs"]).ToString();
                txtInPcs.Text = Val.ToInt64(DTab.Rows[0]["in_pcs"]).ToString();
                txtOutPcs.Text = Val.ToInt64(DTab.Rows[0]["out_pcs"]).ToString();
                txtClosingPcs.Text = Val.ToInt64(DTab.Rows[0]["cl_pcs"]).ToString();
                txtOpeningPcs.Focus();
            }
            this.Cursor = Cursors.Default;
        }
    }
}
