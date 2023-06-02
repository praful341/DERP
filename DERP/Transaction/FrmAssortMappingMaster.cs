using BLL;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Utility;
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

namespace DREP.Transaction
{
    public partial class FrmAssortMappingMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member
        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        Control _NextEnteredControl;
        private List<Control> _tabControls;
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        AssortMappingMaster objAssortMapping;
        UserAuthentication objUserAuthentication;
        AssortMaster objAssort;
        SieveMaster objSieve;
        RateMaster objRate;
        SaleInvoice objSaleInvoice;
        DataTable DtControlSettings;
        DataTable m_dtbAssorts;
        DataTable m_dtbSieve;
        DataTable m_dtbAssortMappingDetail;
        DataTable m_dtbCurrency;
        DataTable m_dtbDetails;
        DataTable m_opDate;
        DataTable m_dtbStockCarat;
        DataSet m_dtbDS;
        DataTable DTabFile;
        DataTable FAssort;
        DataTable TAssort;
        DataTable FSieve;
        DataTable TSieve;
        DataTable OldMapping;
        int m_numForm_id;
        int m_assort_mapping_id;
        int m_srno;
        int m_update_srno;
        int IntRes;
        Int64 IntRes_Union_ID;

        decimal m_numPer;
        bool m_blnadd = new bool();
        bool m_blnsave = new bool();
        #endregion

        #region Constructor
        public FrmAssortMappingMaster()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            _NextEnteredControl = new Control();
            _tabControls = new List<Control>();

            objAssortMapping = new AssortMappingMaster();
            objUserAuthentication = new UserAuthentication();
            objAssort = new AssortMaster();
            objSieve = new SieveMaster();
            objRate = new RateMaster();
            objSaleInvoice = new SaleInvoice();
            DtControlSettings = new DataTable();
            m_dtbAssorts = new DataTable();
            m_dtbSieve = new DataTable();
            m_dtbAssortMappingDetail = new DataTable();
            m_dtbCurrency = new DataTable();
            m_dtbDetails = new DataTable();
            m_opDate = new DataTable();
            m_dtbStockCarat = new DataTable();
            m_dtbDS = new DataSet();
            DTabFile = new DataTable();
            OldMapping = new DataTable();
            m_numForm_id = 0;
            m_assort_mapping_id = 0;
            m_srno = 0;
            m_update_srno = 0;
            IntRes = 0;
            m_numPer = 0;
            m_blnadd = new bool();
            m_blnsave = new bool();
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
            AddKeyPressListener(this);
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
            objBOFormEvents.FormKeyDown = true;
            objBOFormEvents.FormResize = true;
            objBOFormEvents.FormClosing = true;
            objBOFormEvents.ObjToDispose.Add(objAssortMapping);
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

        #endregion

        #region Events
        private void FrmAssortMappingMaster_Load(object sender, EventArgs e)
        {
            try
            {
                if (!LoadDefaults())
                {
                    btnAdd.Enabled = false;
                    btnClear.Enabled = false;
                    btnSave.Enabled = false;
                }
                else
                {
                    ClearDetails();
                    ttlbAssortMapping.SelectedTabPage = tblAssortMappingdetail;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (AddInGrid())
            {
                lueFromAssortName.EditValue = DBNull.Value;
                lueToAssortName.EditValue = DBNull.Value;
                lueFromSieveName.EditValue = DBNull.Value;
                lueToSieveName.EditValue = DBNull.Value;
                txtPer.Text = string.Empty;
                lueFromAssortName.Focus();
                lueFromAssortName.ShowPopup();
            }
        }
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

                btnSave.Enabled = false;

                m_blnsave = true;
                m_blnadd = false;
                if (!ValidateDetails())
                {
                    m_blnsave = false;
                    btnSave.Enabled = true;
                    return;
                }

                DialogResult result = MessageBox.Show("Do you want to save Assort Mapping?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result != DialogResult.Yes)
                {
                    btnSave.Enabled = true;
                    return;
                }

                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;

                panelProgress.Visible = true;
                backgroundWorker_AssortMapping.RunWorkerAsync();

                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                this.Cursor = Cursors.Default;
                return;
            }
        }
        private void btnClear_Click_1(object sender, EventArgs e)
        {
            ClearDetails();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!PopulateDetails())
                return;
        }

        private void backgroundWorker_AssortMapping_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                {
                    Conn = new BeginTranConnection(true, false);
                }
                else
                {
                    Conn = new BeginTranConnection(false, true);
                }
                AssortMappingMasterProperty objAssortMappingMasterProperty = new AssortMappingMasterProperty();
                AssortMappingMaster objAssortMappingMaster = new AssortMappingMaster();
                try
                {
                    IntRes = 0;
                    int IntCounter = 0;
                    int Count = 0;
                    IntRes_Union_ID = 0;
                    int TotalCount = m_dtbAssortMappingDetail.Rows.Count;

                    foreach (DataRow drw in m_dtbAssortMappingDetail.Rows)
                    {
                        objAssortMappingMasterProperty.assort_mapping_id = Val.ToInt64(drw["assort_mapping_id"]);

                        if (Val.ToInt64(drw["assort_union_id"]) == 0)
                        {
                            objAssortMappingMasterProperty.assort_union_id = IntRes_Union_ID;
                        }
                        else
                        {
                            objAssortMappingMasterProperty.assort_union_id = Val.ToInt64(drw["assort_union_id"]);
                            IntRes_Union_ID = Val.ToInt64(drw["assort_union_id"]);
                        }

                        objAssortMappingMasterProperty.company_id = Val.ToInt(GlobalDec.gEmployeeProperty.company_id);
                        objAssortMappingMasterProperty.branch_id = Val.ToInt(GlobalDec.gEmployeeProperty.branch_id);
                        objAssortMappingMasterProperty.location_id = Val.ToInt(GlobalDec.gEmployeeProperty.location_id);
                        objAssortMappingMasterProperty.assort_mapping_date = Val.DBDate(dtpMappingDate.Text);
                        objAssortMappingMasterProperty.form_id = m_numForm_id;
                        objAssortMappingMasterProperty.from_assort_id = Val.ToInt(drw["from_assort_id"]);
                        objAssortMappingMasterProperty.to_assort_id = Val.ToInt(drw["to_assort_id"]);
                        objAssortMappingMasterProperty.from_sieve_id = Val.ToInt(drw["from_sieve_id"]);
                        objAssortMappingMasterProperty.to_sieve_id = Val.ToInt(drw["to_sieve_id"]);
                        objAssortMappingMasterProperty.percentage = Val.ToDecimal(drw["percentage"]);

                        objAssortMappingMasterProperty = objAssortMappingMaster.Save(objAssortMappingMasterProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                        IntRes_Union_ID = Val.ToInt64(objAssortMappingMasterProperty.assort_union_id);
                        Count++;
                        IntCounter++;
                        IntRes++;
                        SetControlPropertyValue(lblProgressCount, "Text", Count.ToString() + "" + "/" + "" + TotalCount.ToString() + " Completed....");
                    }
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
                    objAssortMappingMasterProperty = null;
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
                Global.Message(ex.ToString());
                if (ex.InnerException != null)
                {
                    Global.Message(ex.InnerException.ToString());
                }
            }
        }

        private void backgroundWorker_AssortMapping_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                panelProgress.Visible = false;

                if (IntRes > 0)
                {
                    Global.Confirm("Assort Mapping Data Save Succesfully");
                    ClearDetails();
                    PopulateDetails();
                }
                else
                {
                    Global.Confirm("Error In Assort Mapping");
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }

        #region "Grid Events" 

        private void dgvAssortMappingView_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                objAssortMapping = new AssortMappingMaster();
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvAssortMappingView.GetDataRow(e.RowHandle);
                        m_dtbAssortMappingDetail = objAssortMapping.GetDataDetails(Val.ToInt(Drow["assort_union_id"]));
                        grdAssortMapping.DataSource = m_dtbAssortMappingDetail;

                        ttlbAssortMapping.SelectedTabPage = tblAssortMappingdetail;
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void dgvAssortMapping_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                objAssortMapping = new AssortMappingMaster();
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvAssortMapping.GetDataRow(e.RowHandle);
                        btnAdd.Text = "&Update";
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt32(Drow["assort_mapping_id"]);
                        lueFromSieveName.Text = Val.ToString(Drow["from_sieve_name"]);
                        lueFromSieveName.Tag = Val.ToInt64(Drow["from_sieve_id"]);
                        lueFromAssortName.Text = Val.ToString(Drow["from_assort_name"]);
                        lueFromAssortName.Tag = Val.ToInt64(Drow["from_assort_id"]);
                        lueToAssortName.Text = Val.ToString(Drow["to_assort_name"]);
                        lueToAssortName.Tag = Val.ToInt64(Drow["to_assort_id"]);
                        lueToSieveName.Text = Val.ToString(Drow["to_sieve_name"]);
                        lueToSieveName.Tag = Val.ToInt64(Drow["to_sieve_name"]);
                        txtPer.Text = Val.ToString(Drow["percentage"]);
                        m_numPer = Val.ToDecimal(Drow["percentage"]);
                        m_assort_mapping_id = Val.ToInt(Drow["assort_mapping_id"]);
                        m_update_srno = Val.ToInt(Drow["sr_no"]);
                        lueFromAssortName.Focus();
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
        private bool LoadDefaults()
        {
            bool blnReturn = true;
            try
            {
                m_dtbAssorts = objAssort.GetData(1);
                lueFromAssortName.Properties.DataSource = m_dtbAssorts;
                lueFromAssortName.Properties.ValueMember = "assort_id";
                lueFromAssortName.Properties.DisplayMember = "assort_name";

                lueToAssortName.Properties.DataSource = m_dtbAssorts;
                lueToAssortName.Properties.ValueMember = "assort_id";
                lueToAssortName.Properties.DisplayMember = "assort_name";

                m_dtbSieve = objSieve.GetData(1);
                lueFromSieveName.Properties.DataSource = m_dtbSieve;
                lueFromSieveName.Properties.ValueMember = "sieve_id";
                lueFromSieveName.Properties.DisplayMember = "sieve_name";

                lueToSieveName.Properties.DataSource = m_dtbSieve;
                lueToSieveName.Properties.ValueMember = "sieve_id";
                lueToSieveName.Properties.DisplayMember = "sieve_name";

                m_opDate = Global.GetDate();
                dtpFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpFromDate.EditValue = Val.DBDate(m_opDate.Rows[0]["opening_date"].ToString());

                dtpToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpToDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpToDate.EditValue = DateTime.Now;

                dtpMappingDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpMappingDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpMappingDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpMappingDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpMappingDate.EditValue = DateTime.Now;

                lueFromAssortName.Focus();
                lueFromAssortName.ShowPopup();

                GetOldData();
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
        private bool AddInGrid()
        {
            bool blnReturn = true;

            try
            {
                m_blnadd = true;
                m_blnsave = false;
                if (!ValidateDetails())
                {
                    m_blnadd = false;
                    blnReturn = false;
                    return blnReturn;
                }

                if (btnAdd.Text == "&Add")
                {
                    //if (m_dtbStockCarat.Rows.Count > 0)
                    //{
                    //    numStockCarat = Val.ToDecimal(m_dtbStockCarat.Rows[0]["stock_carat"]);
                    //}

                    //if (numStockCarat < (Val.ToDecimal(txtPer.Text)))
                    //{
                    //    Global.Message("Please check enter carat more then stock carat", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    txtPer.Focus();
                    //    blnReturn = false;
                    //    return blnReturn;
                    //}
                    m_srno = dgvAssortMapping.RowCount;
                    DataRow[] dr = m_dtbAssortMappingDetail.Select("from_sieve_id = " + Val.ToInt(lueFromSieveName.EditValue) + " AND from_assort_id = " + Val.ToInt(lueFromAssortName.EditValue) + " AND to_sieve_id = " + Val.ToInt(lueToSieveName.EditValue) + " AND to_assort_id = " + Val.ToInt(lueToAssortName.EditValue));
                    if (dr.Count() == 1)
                    {
                        Global.Message("Record already exists in grid", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lueFromAssortName.Focus();
                        blnReturn = false;
                        return blnReturn;
                    }

                    DataRow drwNew = m_dtbAssortMappingDetail.NewRow();
                    decimal numCarat = Val.ToDecimal(txtPer.Text);

                    drwNew["assort_mapping_id"] = Val.ToInt64(0);
                    drwNew["assort_union_id"] = Val.ToInt64(0);
                    drwNew["assort_mapping_date"] = Val.ToString(dtpMappingDate.Text);
                    drwNew["from_assort_id"] = Val.ToInt(lueFromAssortName.EditValue);
                    drwNew["from_assort_name"] = Val.ToString(lueFromAssortName.Text);

                    drwNew["to_assort_id"] = Val.ToInt(lueToAssortName.EditValue);
                    drwNew["to_assort_name"] = Val.ToString(lueToAssortName.Text);

                    drwNew["from_sieve_id"] = Val.ToInt(lueFromSieveName.EditValue);
                    drwNew["from_sieve_name"] = Val.ToString(lueFromSieveName.Text);

                    drwNew["to_sieve_id"] = Val.ToInt(lueToSieveName.EditValue);
                    drwNew["to_sieve_name"] = Val.ToString(lueToSieveName.Text);

                    drwNew["percentage"] = numCarat;

                    m_srno = m_srno + 1;

                    drwNew["sr_no"] = m_srno;
                    m_dtbAssortMappingDetail.Rows.Add(drwNew);

                    dgvAssortMapping.MoveLast();
                }
                else if (btnAdd.Text == "&Update")
                {
                    if (m_dtbAssortMappingDetail.Rows.Count > 1)
                    {
                        DataRow[] dr = m_dtbAssortMappingDetail.Select("from_sieve_id = " + Val.ToInt(lueFromSieveName.EditValue) + " AND from_assort_id = " + Val.ToInt(lueFromAssortName.EditValue) + " AND to_sieve_id = " + Val.ToInt(lueToSieveName.EditValue) + " AND to_assort_id = " + Val.ToInt(lueToAssortName.EditValue) + " AND sr_no <> " + m_update_srno);
                        if (dr.Count() > 0)
                        {
                            Global.Message("Record already exists in grid", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            lueFromAssortName.Focus();
                            blnReturn = false;
                            return blnReturn;
                        }
                    }
                    if (m_dtbAssortMappingDetail.Select("from_assort_id ='" + Val.ToInt(lueFromAssortName.EditValue) + "' AND from_sieve_id ='" + Val.ToInt(lueFromSieveName.EditValue) + "' AND to_assort_id ='" + Val.ToInt(lueToAssortName.EditValue) + "' AND to_sieve_id ='" + Val.ToInt(lueToSieveName.EditValue) + "'").Length > 0)
                    {
                        for (int i = 0; i < m_dtbAssortMappingDetail.Rows.Count; i++)
                        {
                            if (m_dtbAssortMappingDetail.Select("assort_mapping_id ='" + m_assort_mapping_id + "'AND sr_no = '" + m_update_srno + "'").Length > 0)
                            {
                                if (m_dtbAssortMappingDetail.Rows[m_update_srno - 1]["assort_mapping_id"].ToString() == m_assort_mapping_id.ToString())
                                {
                                    m_dtbAssortMappingDetail.Rows[m_update_srno - 1]["percentage"] = Val.ToDecimal(txtPer.Text).ToString();

                                    //m_dtbAssortMappingDetail.Rows[m_update_srno - 1]["flag"] = 1;
                                    // Add By Praful On 13082020
                                    m_dtbAssortMappingDetail.Rows[m_update_srno - 1]["from_assort_id"] = Val.ToInt(lueFromAssortName.EditValue);
                                    m_dtbAssortMappingDetail.Rows[m_update_srno - 1]["from_assort_name"] = Val.ToString(lueFromAssortName.Text);
                                    m_dtbAssortMappingDetail.Rows[m_update_srno - 1]["from_sieve_id"] = Val.ToInt(lueFromSieveName.EditValue);
                                    m_dtbAssortMappingDetail.Rows[m_update_srno - 1]["from_sieve_name"] = Val.ToString(lueFromSieveName.Text);

                                    m_dtbAssortMappingDetail.Rows[m_update_srno - 1]["to_assort_id"] = Val.ToInt(lueToAssortName.EditValue);
                                    m_dtbAssortMappingDetail.Rows[m_update_srno - 1]["to_assort_name"] = Val.ToString(lueToAssortName.Text);
                                    m_dtbAssortMappingDetail.Rows[m_update_srno - 1]["to_sieve_id"] = Val.ToInt(lueToSieveName.EditValue);
                                    m_dtbAssortMappingDetail.Rows[m_update_srno - 1]["to_sieve_name"] = Val.ToString(lueToSieveName.Text);
                                    break;
                                }
                            }
                        }
                        btnAdd.Text = "&Add";
                    }
                    else
                    {
                        for (int i = 0; i < m_dtbAssortMappingDetail.Rows.Count; i++)
                        {
                            if (m_dtbAssortMappingDetail.Select("assort_mapping_id ='" + m_assort_mapping_id + "' AND sr_no = '" + m_update_srno + "'").Length > 0)
                            {
                                if (m_dtbAssortMappingDetail.Rows[m_update_srno - 1]["assort_mapping_id"].ToString() == m_assort_mapping_id.ToString())
                                {

                                    m_dtbAssortMappingDetail.Rows[m_update_srno - 1]["percentage"] = Val.ToDecimal(txtPer.Text).ToString();
                                    //m_dtbAssortMappingDetail.Rows[m_update_srno - 1]["flag"] = 1;
                                    m_dtbAssortMappingDetail.Rows[m_update_srno - 1]["from_assort_id"] = Val.ToInt(lueFromAssortName.EditValue);
                                    m_dtbAssortMappingDetail.Rows[m_update_srno - 1]["from_assort_name"] = Val.ToString(lueFromAssortName.Text);
                                    m_dtbAssortMappingDetail.Rows[m_update_srno - 1]["from_sieve_id"] = Val.ToInt(lueFromSieveName.EditValue);
                                    m_dtbAssortMappingDetail.Rows[m_update_srno - 1]["from_sieve_name"] = Val.ToString(lueFromSieveName.Text);
                                    m_dtbAssortMappingDetail.Rows[m_update_srno - 1]["to_assort_id"] = Val.ToInt(lueToAssortName.EditValue);
                                    m_dtbAssortMappingDetail.Rows[m_update_srno - 1]["to_assort_name"] = Val.ToString(lueToAssortName.Text);
                                    m_dtbAssortMappingDetail.Rows[m_update_srno - 1]["to_sieve_id"] = Val.ToInt(lueToSieveName.EditValue);
                                    m_dtbAssortMappingDetail.Rows[m_update_srno - 1]["to_sieve_name"] = Val.ToString(lueToSieveName.Text);
                                }
                            }
                        }
                        btnAdd.Text = "&Add";
                    }
                    dgvAssortMapping.MoveLast();
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
        }
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();

            try
            {
                if (m_blnsave)
                {
                    if (m_dtbAssortMappingDetail.Rows.Count == 0)
                    {
                        lstError.Add(new ListError(22, "Record"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                        }
                    }
                    var result = DateTime.Compare(Convert.ToDateTime(dtpMappingDate.Text), DateTime.Today);
                    if (result > 0)
                    {
                        lstError.Add(new ListError(5, "Mapping Date Not Be Greater Than Today Date"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            dtpMappingDate.Focus();
                        }
                    }
                }

                if (m_blnadd)
                {
                    if (lueFromAssortName.Text == "")
                    {
                        lstError.Add(new ListError(13, "From Assort"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueFromAssortName.Focus();
                        }
                    }
                    if (lueToAssortName.Text == "")
                    {
                        lstError.Add(new ListError(13, "To Assort"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueToAssortName.Focus();
                        }
                    }
                    if (lueFromSieveName.Text == "")
                    {
                        lstError.Add(new ListError(13, "From Sieve"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueFromSieveName.Focus();
                        }
                    }
                    if (lueToSieveName.Text == "")
                    {
                        lstError.Add(new ListError(13, "To Sieve"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueToSieveName.Focus();
                        }
                    }

                    if (Val.ToDouble(txtPer.Text) == 0)
                    {
                        lstError.Add(new ListError(12, "Percentage"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtPer.Focus();
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                lstError.Add(new ListError(ex));
            }
            return (!(BLL.General.ShowErrors(lstError)));
        }
        private bool ClearDetails()
        {
            bool blnReturn = true;
            try
            {
                if (!GenerateAssortMappingDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }

                lblMode.Tag = null;

                dtpFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpFromDate.EditValue = Val.DBDate(m_opDate.Rows[0]["opening_date"].ToString());

                dtpMappingDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpMappingDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpMappingDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpMappingDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpMappingDate.EditValue = DateTime.Now;

                lueFromAssortName.EditValue = System.DBNull.Value;
                lueFromSieveName.EditValue = System.DBNull.Value;
                lueToAssortName.EditValue = System.DBNull.Value;
                lueToSieveName.EditValue = System.DBNull.Value;

                txtPer.Text = string.Empty;
                lueFromAssortName.Focus();
                m_srno = 0;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
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
        private bool GenerateAssortMappingDetails()
        {
            bool blnReturn = true;
            try
            {
                if (m_dtbAssortMappingDetail.Rows.Count > 0)
                    m_dtbAssortMappingDetail.Rows.Clear();

                m_dtbAssortMappingDetail = new DataTable();
                m_dtbAssortMappingDetail.Columns.Add("sr_no", typeof(int));
                m_dtbAssortMappingDetail.Columns.Add("assort_mapping_id", typeof(Int64));
                m_dtbAssortMappingDetail.Columns.Add("assort_mapping_date", typeof(string));
                m_dtbAssortMappingDetail.Columns.Add("from_assort_id", typeof(int));
                m_dtbAssortMappingDetail.Columns.Add("from_assort_name", typeof(string));
                m_dtbAssortMappingDetail.Columns.Add("to_assort_id", typeof(int));
                m_dtbAssortMappingDetail.Columns.Add("to_assort_name", typeof(string));
                m_dtbAssortMappingDetail.Columns.Add("from_sieve_id", typeof(int));
                m_dtbAssortMappingDetail.Columns.Add("from_sieve_name", typeof(string));
                m_dtbAssortMappingDetail.Columns.Add("to_sieve_id", typeof(int));
                m_dtbAssortMappingDetail.Columns.Add("to_sieve_name", typeof(string));
                m_dtbAssortMappingDetail.Columns.Add("percentage", typeof(decimal)).DefaultValue = 0;
                m_dtbAssortMappingDetail.Columns.Add("flag", typeof(int)).DefaultValue = 0;
                m_dtbAssortMappingDetail.Columns.Add("assort_union_id", typeof(Int64));
                grdAssortMapping.DataSource = m_dtbAssortMappingDetail;
                grdAssortMapping.Refresh();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
        }
        private void GetOldData()
        {
            OldMapping = new DataTable();
            OldMapping = objAssortMapping.GetData("", "");

        }
        private bool PopulateDetails()
        {
            objAssortMapping = new AssortMappingMaster();
            bool blnReturn = true;
            DateTime datFromDate = DateTime.MinValue;
            DateTime datToDate = DateTime.MinValue;
            try
            {
                m_dtbDetails = objAssortMapping.GetData(Val.DBDate(dtpFromDate.Text), Val.DBDate(dtpToDate.Text));

                if (m_dtbDetails.Rows.Count == 0)
                {
                    Global.Message("Data Not Found");
                    blnReturn = false;
                }

                grdAssortMappingView.DataSource = m_dtbDetails;
                //dgvMemoView.BestFitColumns();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            finally
            {
                objAssortMapping = null;
            }

            return blnReturn;
        }
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
                            dgvAssortMappingView.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvAssortMappingView.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvAssortMappingView.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvAssortMappingView.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvAssortMappingView.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvAssortMappingView.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvAssortMappingView.ExportToCsv(Filepath);
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

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            List<ListError> lstError = new List<ListError>();
            Dictionary<Control, string> rtnCtrls = new Dictionary<Control, string>();
            rtnCtrls = Global.CheckCompulsoryControls(Val.ToInt(ObjPer.form_id), this);
            try
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
                grdAssortMapping.DataSource = null;

                if (txtFileName.Text.Length != 0)
                {
                    using (var pck = new ExcelPackage(new FileInfo(txtFileName.Text)))
                    {
                        ExcelWorksheet ws = pck.Workbook.Worksheets[1];
                        DTabFile = WorksheetToDataTable(ws, true);
                    }
                }
                if (DTabFile.Rows.Count > 0)
                {
                    DataTable distinct = DTabFile.DefaultView.ToTable(true, "from_assort", "to_assort", "from_sieve", "to_sieve");
                    if (distinct.Rows.Count != DTabFile.Rows.Count)
                    {
                        Global.Message("Please Check File Duplicate Data Found!!");
                        this.Cursor = Cursors.Default;
                        return;
                    }
                }
                GetOldData();
                if (DTabFile.Rows.Count > 0)
                {
                    m_srno = 1;
                    GenerateAssortMappingDetails();

                    foreach (DataRow DRow in DTabFile.Rows)
                    {
                        if (DRow["from_assort"] != null)
                        {
                            if (Val.ToString(DRow["from_assort"]) != "")
                            {
                                if (m_dtbAssorts.Select("assort_name ='" + Val.ToString(DRow["from_assort"]) + "'").Length > 0)
                                {
                                    FAssort = m_dtbAssorts.Select("assort_name ='" + Val.ToString(DRow["from_assort"]) + "'").CopyToDataTable();
                                }
                                else
                                {
                                    Global.Message("Assort Not found in Master" + Val.ToString(DRow["from_assort"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.Cursor = Cursors.Default;
                                    return;
                                }
                            }
                        }
                        if (DRow["to_assort"] != null)
                        {
                            if (Val.ToString(DRow["to_assort"]) != "")
                            {
                                if (m_dtbAssorts.Select("assort_name ='" + Val.ToString(DRow["to_assort"]) + "'").Length > 0)
                                {
                                    TAssort = m_dtbAssorts.Select("assort_name ='" + Val.ToString(DRow["to_assort"]) + "'").CopyToDataTable();
                                }
                                else
                                {
                                    Global.Message("Assort Not found in Master" + Val.ToString(DRow["to_assort"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.Cursor = Cursors.Default;
                                    return;
                                }
                            }
                        }
                        if (DRow["from_sieve"] != null)
                        {
                            if (Val.ToString(DRow["from_sieve"]) != "")
                            {
                                if (m_dtbSieve.Select("sieve_name ='" + Val.ToString(DRow["from_sieve"]) + "'").Length > 0)
                                {
                                    FSieve = m_dtbSieve.Select("sieve_name ='" + Val.ToString(DRow["from_sieve"]) + "'").CopyToDataTable();
                                }
                                else
                                {
                                    Global.Message("Sieve Not found in Master" + Val.ToString(DRow["from_sieve"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.Cursor = Cursors.Default;
                                    return;
                                }
                            }
                        }
                        if (DRow["to_sieve"] != null)
                        {
                            if (Val.ToString(DRow["to_sieve"]) != "")
                            {
                                if (m_dtbSieve.Select("sieve_name ='" + Val.ToString(DRow["to_sieve"]) + "'").Length > 0)
                                {
                                    TSieve = m_dtbSieve.Select("sieve_name ='" + Val.ToString(DRow["to_sieve"]) + "'").CopyToDataTable();
                                }
                                else
                                {
                                    Global.Message("Sieve Not found in Master" + Val.ToString(DRow["to_sieve"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.Cursor = Cursors.Default;
                                    return;
                                }
                            }
                        }
                        if (OldMapping.Select("from_assort_id =" + Val.ToInt(FAssort.Rows[0]["assort_id"]) + " and to_assort_id = " + Val.ToInt(TAssort.Rows[0]["assort_id"]) + " and from_sieve_id = " + Val.ToInt(FSieve.Rows[0]["sieve_id"]) + " and to_sieve_id = " + Val.ToInt(TSieve.Rows[0]["sieve_id"]) + "").Length > 0)
                        {
                            Global.Message("Already Exist Mapping of Assort From " + Val.ToString(FAssort.Rows[0]["assort_name"]) + " To " + Val.ToString(TAssort.Rows[0]["assort_name"]) + " And Sieve is " + Val.ToString(FSieve.Rows[0]["sieve_name"]));
                            this.Cursor = Cursors.Default;
                            return;
                        }
                        DataRow drwNew = m_dtbAssortMappingDetail.NewRow();
                        if (FAssort.Rows.Count > 0)
                        {
                            drwNew["from_assort_id"] = Val.ToInt(FAssort.Rows[0]["assort_id"]);
                            drwNew["from_assort_name"] = Val.ToString(FAssort.Rows[0]["assort_name"]);
                        }
                        if (TAssort.Rows.Count > 0)
                        {
                            drwNew["to_assort_id"] = Val.ToInt(TAssort.Rows[0]["assort_id"]);
                            drwNew["to_assort_name"] = Val.ToString(TAssort.Rows[0]["assort_name"]);
                        }
                        if (FSieve.Rows.Count > 0)
                        {
                            drwNew["from_sieve_id"] = Val.ToInt(FSieve.Rows[0]["sieve_id"]);
                            drwNew["from_sieve_name"] = Val.ToString(FSieve.Rows[0]["sieve_name"]);
                        }
                        if (TSieve.Rows.Count > 0)
                        {
                            drwNew["to_sieve_id"] = Val.ToInt(TSieve.Rows[0]["sieve_id"]);
                            drwNew["to_sieve_name"] = Val.ToString(TSieve.Rows[0]["sieve_name"]);
                        }
                        if (Val.ToDecimal(DRow["percentage"]) > 0)
                        {
                            drwNew["percentage"] = Val.ToDecimal(DRow["percentage"]);
                        }
                        drwNew["assort_mapping_id"] = Val.ToInt64(0);
                        drwNew["assort_union_id"] = Val.ToInt64(0);
                        drwNew["assort_mapping_date"] = Val.ToString(dtpMappingDate.Text);
                        drwNew["sr_no"] = m_srno;

                        m_dtbAssortMappingDetail.Rows.Add(drwNew);

                        dgvAssortMapping.MoveLast();
                        m_srno++;
                    }
                }
                grdAssortMapping.DataSource = m_dtbAssortMappingDetail;

                this.Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                this.Cursor = Cursors.Default;
                return;
            }
        }
    }
}