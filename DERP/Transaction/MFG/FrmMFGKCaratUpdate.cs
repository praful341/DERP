using BLL;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DREP.Master
{
    public partial class FrmMFGKCaratUpdate : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        FormEvents objBOFormEvents;
        Validation Val;
        BLL.FormPer ObjPer;
        BLL.BeginTranConnection Conn;

        Control _NextEnteredControl;
        private List<Control> _tabControls;

        CityMaster objCity;

        DataTable DtControlSettings;
        DataTable m_dtbParam = new DataTable();
        DataTable m_dtbKapan = new DataTable();
        //DataTable DTab_KapanWiseData = new DataTable();

        #endregion

        #region Constructor
        public FrmMFGKCaratUpdate()
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

            //if (Global.HideFormControls(Val.ToInt(ObjPer.form_id), this) != "")
            //{
            //    Global.Message("Select First User Setting...Please Contact to Administrator...");
            //    return;
            //}

            //ControlSettingDT(Val.ToInt(ObjPer.form_id), this);
            //AddGotFocusListener(this);
            //this.KeyPreview = true;

            //TabControlsToList(this.Controls);
            //_tabControls = _tabControls.OrderBy(x => x.TabIndex).ToList();

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
                m_dtbKapan = Global.GetKapanAll();

                lueKapan.Properties.DataSource = m_dtbKapan;
                lueKapan.Properties.ValueMember = "kapan_id";
                lueKapan.Properties.DisplayMember = "kapan_no";

                m_dtbParam = Global.GetRoughCutAll();

                lueCutNo.Properties.DataSource = m_dtbParam;
                lueCutNo.Properties.ValueMember = "rough_cut_id";
                lueCutNo.Properties.DisplayMember = "rough_cut_no";

                Global.LOOKUPDepartment_New(lueDepartment);

                // Add By Praful On 29072021

                //DTab_KapanWiseData = Global.GetRoughStockWise(Val.ToInt(0), Val.ToInt32(0));

                // End By Praful On 29072021

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
            //ObjPer.SetFormPer();
            //if (ObjPer.AllowUpdate == false || ObjPer.AllowInsert == false)
            //{
            //    Global.Message(BLL.GlobalDec.gStrPermissionInsUpdMsg);
            //    return;
            //}

            //List<ListError> lstError = new List<ListError>();
            //Dictionary<Control, string> rtnCtrls = new Dictionary<Control, string>();
            //rtnCtrls = Global.CheckCompulsoryControls(Val.ToInt(ObjPer.form_id), this);
            //if (rtnCtrls.Count > 0)
            //{
            //    foreach (KeyValuePair<Control, string> entry in rtnCtrls)
            //    {
            //        if (entry.Key is DevExpress.XtraEditors.LookUpEdit)
            //        {
            //            lstError.Add(new ListError(13, entry.Value));
            //        }
            //        else
            //        {
            //            lstError.Add(new ListError(12, entry.Value));
            //        }
            //    }
            //    rtnCtrls.First().Key.Focus();
            //    BLL.General.ShowErrors(lstError);
            //    Cursor.Current = Cursors.Arrow;
            //    return;
            //}

            btnSave.Enabled = false;

            if (SaveDetails())
            {
                btnClear_Click(sender, e);
            }

            btnSave.Enabled = true;
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                lueKapan.EditValue = System.DBNull.Value;
                lueCutNo.EditValue = System.DBNull.Value;
                lueDepartment.EditValue = System.DBNull.Value;
                txtPercentage.Text = string.Empty;
                lueKapan.Focus();
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

        #endregion

        #region Functions
        private bool SaveDetails()
        {
            bool blnReturn = true;
            MFGLotSplit objMFGLotSplit = new MFGLotSplit();
            MFGMixSplitProperty objMFGMixSplitProperty = new MFGMixSplitProperty();
            Conn = new BeginTranConnection(true, false);
            int IntRes = 0;
            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }


                objMFGMixSplitProperty.kapan_id = Val.ToInt(lueKapan.EditValue);
                objMFGMixSplitProperty.from_cut_id = Val.ToInt(lueCutNo.EditValue);
                objMFGMixSplitProperty.lotting_department_id = Val.ToInt(lueDepartment.EditValue);
                objMFGMixSplitProperty.percentage = Val.ToDecimal(txtPercentage.Text);

                IntRes = objMFGLotSplit.KCarat_Update(objMFGMixSplitProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                Conn.Inter1.Commit();
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Update K Carat");
                    IntRes = -1;
                    Conn.Inter1.Rollback();
                    Conn = null;
                    lueKapan.Focus();
                }
                else
                {
                    Global.Confirm("K Carat Update Successfully");
                }

            }
            catch (Exception ex)
            {
                IntRes = -1;
                Conn.Inter1.Rollback();
                Conn = null;
                General.ShowErrors(ex.ToString());
                blnReturn = false;
            }
            finally
            {
                objMFGMixSplitProperty = null;
            }

            return blnReturn;
        }
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (Val.ToString(lueKapan.Text) == "")
                {
                    lstError.Add(new ListError(5, "Select Kapan No."));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueKapan.Focus();
                    }
                }
                if (Val.ToString(lueCutNo.Text) == "")
                {
                    lstError.Add(new ListError(5, "Select Cut No."));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueCutNo.Focus();
                    }
                }
                if (Val.ToString(lueDepartment.Text) == "")
                {
                    lstError.Add(new ListError(5, "Select Lotting Department"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueDepartment.Focus();
                    }
                }
                if (Val.ToDecimal(txtPercentage.Text) == 0)
                {
                    lstError.Add(new ListError(5, "Percentage should be not blank."));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtPercentage.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                lstError.Add(new ListError(ex));
            }
            return (!(BLL.General.ShowErrors(lstError)));

        }


        #endregion

        private void lueKapan_EditValueChanged(object sender, EventArgs e)
        {

            if (lueKapan.Text.ToString() != "")
            {
                m_dtbParam = new DataTable();
                m_dtbParam = Global.GetRoughKapanWise_Data(Val.ToInt(lueKapan.EditValue));
                //if (m_dtbParam.Rows.Count == 0)
                //{
                //    m_dtbParam = DTab_KapanWiseData;
                //}
            }
            lueCutNo.Properties.DataSource = m_dtbParam;
            lueCutNo.Properties.ValueMember = "rough_cut_id";
            lueCutNo.Properties.DisplayMember = "rough_cut_no";
        }
    }
}
