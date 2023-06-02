using BLL;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.PropertyClasses.Master;
using BLL.PropertyClasses.Transaction;
using BLL.PropertyClasses.Transaction.MFG;
using DERP.Class;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using static DERP.Class.Global;

namespace DREP.Master
{
    public partial class FrmCityMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        FormEvents objBOFormEvents;
        Validation Val;
        BLL.FormPer ObjPer;

        Control _NextEnteredControl;
        private List<Control> _tabControls;

        CityMaster objCity;

        DataTable DtControlSettings;

        #endregion

        #region Constructor
        public FrmCityMaster()
        {
            InitializeComponent();

            objBOFormEvents = new FormEvents();
            Val = new Validation();
            ObjPer = new BLL.FormPer();

            _NextEnteredControl = new Control();
            _tabControls = new List<Control>();

            objCity = new CityMaster();

            DtControlSettings = new DataTable();
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

            DataTable DtFilterColSetting = new DataTable();
            if (DtColSetting.Rows.Count > 0)
            {
                DtFilterColSetting = (from DataRow dr in DtColSetting.Rows
                                      where Val.ToBooleanToInt(dr["is_control"]) == 1
                                      && dr["column_type"].ToString() != "LABEL"
                                      select dr).CopyToDataTable();
            }
            else
            {
                Global.Message("Please Select User Setting");
                return;
            }
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
            this.KeyPreview = true;
            objBOFormEvents.CurForm = this;
            objBOFormEvents.FormKeyPress = true;
            objBOFormEvents.FormKeyDown = true;
            objBOFormEvents.FormResize = true;
            objBOFormEvents.FormClosing = true;
            objBOFormEvents.ObjToDispose.Add(objCity);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }

        #endregion

        #region Events
        private void FrmCityMaster_Load(object sender, EventArgs e)
        {
            try
            {
                Global.LOOKUPCountry(lueCountry);
                Global.LOOKUPState(lueState);
                GetData();
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

            List<ListError> lstError = new List<ListError>();
            Dictionary<Control, string> rtnCtrls = new Dictionary<Control, string>();
            rtnCtrls = Global.CheckCompulsoryControls(Val.ToInt(ObjPer.form_id), this);
            if (rtnCtrls.Count > 0)
            {
                foreach (KeyValuePair<Control, string> entry in rtnCtrls)
                {
                    if (entry.Key is DevExpress.XtraEditors.LookUpEdit)
                    {
                        lstError.Add(new ListError(13, entry.Value));
                    }
                    else
                    {
                        lstError.Add(new ListError(12, entry.Value));
                    }
                }
                rtnCtrls.First().Key.Focus();
                BLL.General.ShowErrors(lstError);
                Cursor.Current = Cursors.Arrow;
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
                txtCityName.Text = "";
                txtRemark.Text = "";
                lueState.EditValue = null;
                lueCountry.EditValue = null;
                txtSequenceNo.Text = "";
                chkActive.Checked = true;
                txtCityName.Focus();
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
        private void LookupState_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                lueCountry.EditValue = lueState.GetColumnValue("country_id");
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }

        #region GridEvents
        private void dgvCityMaster_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvCityMaster.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt64(Drow["city_id"]);
                        lueState.EditValue = Val.ToInt32(Drow["state_id"]);
                        lueCountry.EditValue = Val.ToInt32(Drow["country_id"]);
                        txtCityName.Text = Val.ToString(Drow["city_name"]);
                        txtRemark.Text = Val.ToString(Drow["remarks"]);
                        txtSequenceNo.Text = Val.ToString(Drow["sequence_no"]);
                        chkActive.Checked = Val.ToBoolean(Drow["active"]);
                        txtCityName.Focus();
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
            City_MasterProperty CityMasterProperty = new City_MasterProperty();
            CityMaster ConfigFormMaster = new CityMaster();

            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }


                CityMasterProperty.city_id = Val.ToInt64(lblMode.Tag);
                CityMasterProperty.city_name = txtCityName.Text.ToUpper();
                CityMasterProperty.state_id = Val.ToInt(lueState.EditValue);
                CityMasterProperty.country_id = Val.ToInt(lueCountry.EditValue);
                CityMasterProperty.active = Val.ToBoolean(chkActive.Checked);
                CityMasterProperty.remarks = txtRemark.Text.ToUpper();
                CityMasterProperty.sequence_no = Val.ToInt(txtSequenceNo.Text);

                int IntRes = objCity.Save(CityMasterProperty);
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Save City Details");
                    txtCityName.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("City Details Data Save Successfully");
                    }
                    else
                    {
                        Global.Confirm("City Details Data Update Successfully");
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
                CityMasterProperty = null;
            }

            return blnReturn;
        }
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (!objCity.ISExists(txtCityName.Text, Val.ToInt64(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
                {
                    lstError.Add(new ListError(23, "City Name"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtCityName.Focus();
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
            //MFGDepartmentCosting objMFGDepartmentCosting = new MFGDepartmentCosting();
            //MFGDepartmentCostingProperty objMFGDepartmentCostingProperty = new MFGDepartmentCostingProperty();

            //DataTable DTab_List = new DataTable();
            try
            {
                DataTable DTab = objCity.GetData();
                grdCityMaster.DataSource = DTab;
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }


            //DTab_List = objMFGDepartmentCosting.GetDepartmentCostingData(objMFGDepartmentCostingProperty);

            //XmlDocument doc = new XmlDocument();
            ////create the root element
            //XmlElement root = doc.DocumentElement;
            ////doc.InsertBefore(xmlDeclaration, root);
            ////string.Empty makes cleaner code
            //XmlElement element1 = doc.CreateElement(string.Empty, "Galaxy", string.Empty);
            //doc.AppendChild(element1);
            //XmlElement element6 = doc.CreateElement(string.Empty, "StoneCollection", string.Empty);
            ////Console.WriteLine(element6 + "version=" + "1.0.0");
            //XmlElement element2 = doc.CreateElement(string.Empty, "stones", string.Empty);
            ////XmlElement element3 = doc.CreateElement(string.Empty, "stone", string.Empty);
            //element1.AppendChild(element6);
            //element6.AppendChild(element2);

            //foreach (DataRow DR in DTab_List.Rows)
            //{
            //    string Packeg_Name = "''" + Val.ToString(DR["package_name"]) + "''";
            //    string Stone_Name = "''" + Val.ToString(DR["stone_name"]) + "''";
            //    string Process_Type = "''" + Val.ToString(DR["process_type"]) + "''";
            //    string Weight = "''" + Val.ToString(DR["weight"]) + "''";

            //    XmlElement element3 = doc.CreateElement(string.Empty, "stone", string.Empty);
            //    XmlText txt = doc.CreateTextNode("package_name=" + Packeg_Name + " stone_name=" + Stone_Name + " process_type=" + Process_Type + " weight=" + Weight);
            //    element2.AppendChild(element3);
            //    element3.AppendChild(txt);
            //}

            ////XmlText text1 = doc.CreateTextNode("Demo Text");
            ////element3.AppendChild(text1);
            //doc.Save(@"D:\MyDataset.xml");
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
                            dgvCityMaster.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvCityMaster.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvCityMaster.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvCityMaster.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvCityMaster.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvCityMaster.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvCityMaster.ExportToCsv(Filepath);
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
