using BLL;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Rejection;
using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Utility;
using BLL.PropertyClasses.Rejection;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Rejection
{
    public partial class FrmMFGRejectionTransferManual : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        Control _NextEnteredControl;
        private List<Control> _tabControls;
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        MFGRejectionTransferManual objMFGRejectionTransferManual;
        FinancialYearMaster objFinYear;
        UserAuthentication objUserAuthentication;
        RateMaster objRate;
        OpeningStock opstk;

        DataTable DtControlSettings;
        DataTable m_dtbRejTransferManualDetails;
        DataTable m_dtbDetails;
        DataTable m_dtbType;
        DataTable RejPurity;
        MfgRejectionPurityMaster objRejPurity = new MfgRejectionPurityMaster();
        MFGRoughStockEntry objRoughStockEntry = new MFGRoughStockEntry();
        DataTable m_dtbParam;
        int m_transfer_detail_id;
        int m_srno;
        int m_update_srno;
        Int64 IntSrNo;
        int m_numForm_id;
        decimal m_numTotalCarats;
        decimal m_numTotalAmount;
        decimal m_numcarat;
        decimal m_current_rate;
        decimal m_current_amount;
        decimal m_numSummRate;

        bool m_blnadd;
        bool m_blnsave;
        DataTable RoughKapan;

        #endregion

        #region Constructor
        public FrmMFGRejectionTransferManual()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            _NextEnteredControl = new Control();
            _tabControls = new List<Control>();

            objMFGRejectionTransferManual = new MFGRejectionTransferManual();
            objFinYear = new FinancialYearMaster();
            objUserAuthentication = new UserAuthentication();
            objRate = new RateMaster();
            opstk = new OpeningStock();

            DtControlSettings = new DataTable();
            m_dtbRejTransferManualDetails = new DataTable();
            m_dtbDetails = new DataTable();
            m_dtbType = new DataTable();
            RejPurity = new DataTable();
            RoughKapan = new DataTable();
            m_transfer_detail_id = 0;
            m_srno = 0;
            m_update_srno = 0;
            IntSrNo = 0;
            m_numForm_id = 0;
            m_numTotalCarats = 0;
            m_numTotalAmount = 0;
            m_numcarat = 0;
            m_current_rate = 0;
            m_current_amount = 0;

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
            objBOFormEvents.ObjToDispose.Add(objMFGRejectionTransferManual);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);

        }

        #endregion

        #region Events

        private void FrmMFGRejectionTransferManual_Load(object sender, EventArgs e)
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
                    ttlbMFGRejJangedIssue.SelectedTabPage = tblRejJangeddetail;
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
                txtPurityGroup.Text = "";
                txtPcs.Text = string.Empty;
                txtCarat.Text = string.Empty;
                txtRate.Text = string.Empty;
                txtAmount.Text = string.Empty;
                lueRejPurity.Focus();
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
                    if (entry.Key is DevExpress.XtraEditors.LookUpEdit || entry.Key is DevExpress.XtraEditors.DateEdit)
                    {
                        lstError.Add(new ListError(13, entry.Value));
                    }
                    else if (entry.Key is DevExpress.XtraEditors.TextEdit)
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

            m_blnsave = true;
            m_blnadd = false;
            if (!ValidateDetails())
            {
                m_blnsave = false;
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
            panelProgress.Visible = true;
            backgroundWorker_MFGRejectionTransferManual.RunWorkerAsync();

            btnSave.Enabled = true;
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
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (GlobalDec.gEmployeeProperty.user_name != "JAYESH")
            {
                Global.Message("Don't have Permission...So Please Contact to Administrator");
                return;
            }
            ObjPer.SetFormPer();
            if (ObjPer.AllowDelete == false)
            {
                Global.Message(BLL.GlobalDec.gStrPermissionDelMsg);
                return;
            }
            btnDelete.Enabled = false;
            MFGRejectionTransferManualProperty objMFGRejectionTransferManualProperty = new MFGRejectionTransferManualProperty();
            MFGRejectionTransferManual objMFGRejectionTransferManual = new MFGRejectionTransferManual();
            try
            {
                if (Val.ToInt(lblMode.Tag) != 0)
                {
                    DialogResult result = MessageBox.Show("Do you want to Delete data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                    if (result != DialogResult.Yes)
                    {
                        btnDelete.Enabled = true;
                        return;
                    }

                    objMFGRejectionTransferManualProperty.transfer_id = Val.ToInt(lblMode.Tag);

                    DataTable Dtab_UpdateData = (DataTable)grdMFGRejectionTransferManual.DataSource;

                    //foreach (DataRow DR in Dtab_UpdateData.Rows)
                    //{
                    //    objMFGRejectionSaleProperty.rej_purity_id = Val.ToInt64(DR["purity_id"]);
                    //    objMFGRejectionSaleProperty.carat = Val.ToDecimal(DR["old_carat"]);

                    //    int IntRes_Update = objMFGRejectionSale.GetUpdateJanged_ID(objMFGRejectionSaleProperty);
                    //}

                    int IntRes = objMFGRejectionTransferManual.GetDeleteJanged_ID(objMFGRejectionTransferManualProperty);

                    if (IntRes == -1)
                    {
                        Global.Confirm("Error In Rejection Transfer Manual");
                    }
                    else
                    {
                        if (Val.ToInt(lblMode.Tag) == 0)
                        {
                            Global.Confirm("Rejection Transfer Manual Data Delete Successfully");
                            ClearDetails();
                            PopulateDetails();
                        }
                        else
                        {
                            Global.Confirm("Rejection Transfer Manual Data Delete Successfully");
                            ClearDetails();
                            PopulateDetails();
                        }
                    }
                }
                else
                {
                    Global.Message("Rejection Transfer Manual Data not found");
                    return;
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
            }
            finally
            {
                objMFGRejectionTransferManualProperty = null;
            }
            btnDelete.Enabled = true;
        }
        private void panelControl4_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(255, 191, 219, 255), 2);
            e.Graphics.DrawLine(pen, 0, 64, 1500, 64);
        }
        private void txtCarat_EditValueChanged(object sender, EventArgs e)
        {
            txtAmount.Text = string.Format("{0:0.00}", Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text));
            m_current_amount = Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(m_current_rate);
        }
        private void txtRate_EditValueChanged(object sender, EventArgs e)
        {
            txtAmount.Text = string.Format("{0:0.00}", Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text));
        }
        private void lueRejPurity_EditValueChanged(object sender, EventArgs e)
        {
            if (lueRejPurity.Text != "")
            {
                DataTable DTabRejPurity = RejPurity.Select("purity_id =" + Val.ToInt64(lueRejPurity.EditValue)).CopyToDataTable();

                if (DTabRejPurity.Rows.Count > 0)
                {
                    txtPurityGroup.Text = Val.ToString(DTabRejPurity.Rows[0]["group_name"]);
                }
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearDetails();
        }
        private void backgroundWorker_MFGRejectionTransferManual_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.Default;

                MFGRejectionTransferManualProperty objMFGRejectionTransferManualProperty = new MFGRejectionTransferManualProperty();
                MFGRejectionTransferManual objMFGRejectionTransferManual = new MFGRejectionTransferManual();
                IntSrNo = 0;
                try
                {
                    Conn = new BeginTranConnection(true, false);

                    objMFGRejectionTransferManualProperty.transfer_id = Val.ToInt64(lblMode.Tag);
                    objMFGRejectionTransferManualProperty.transfer_date = Val.DBDate(dtpTransferDate.Text);
                    objMFGRejectionTransferManualProperty.type = Val.ToString(lueType.Text);
                    objMFGRejectionTransferManualProperty.kapan_id = Val.ToInt64(lueKapan.EditValue);
                    objMFGRejectionTransferManualProperty.kapan_carat = Val.ToDecimal(txtKapanWt.Text);
                    objMFGRejectionTransferManualProperty.total_pcs = Math.Round(Val.ToDecimal(clmDetPcs.SummaryItem.SummaryValue), 2);
                    objMFGRejectionTransferManualProperty.total_carat = Math.Round(Val.ToDecimal(clmDetCarat.SummaryItem.SummaryValue), 3);
                    objMFGRejectionTransferManualProperty.total_rate = Math.Round(Val.ToDecimal(clmRSrate.SummaryItem.SummaryValue), 3);
                    objMFGRejectionTransferManualProperty.total_amount = Math.Round(Val.ToDecimal(clmRSAmount.SummaryItem.SummaryValue), 3);

                    objMFGRejectionTransferManualProperty = objMFGRejectionTransferManual.Save(objMFGRejectionTransferManualProperty, DLL.GlobalDec.EnumTran.Start, Conn);
                    Int64 NewTransferID_B = Val.ToInt64(objMFGRejectionTransferManualProperty.transfer_id);

                    int IntCounter = 0;
                    int Count = 0;
                    int TotalCount = m_dtbRejTransferManualDetails.Rows.Count;

                    foreach (DataRow drw in m_dtbRejTransferManualDetails.Rows)
                    {
                        objMFGRejectionTransferManualProperty.transfer_date = Val.ToString(dtpTransferDate.Text);
                        objMFGRejectionTransferManualProperty.transfer_id = Val.ToInt64(NewTransferID_B);
                        objMFGRejectionTransferManualProperty.transfer_detail_id = Val.ToInt64(drw["transfer_detail_id"]);
                        objMFGRejectionTransferManualProperty.type = Val.ToString(drw["type"]);

                        Int64 Rej_Purity_ID = Val.ToInt64(objMFGRejectionTransferManual.FindRejPurityID(Val.ToString(drw["purity_name"])));

                        if (Rej_Purity_ID == 0)
                        {
                            Global.Message("Rejection Purity not in Master");
                            Conn.Inter1.Rollback();
                            Conn = null;
                            return;
                        }
                        else
                        {
                            objMFGRejectionTransferManualProperty.purity_id = Val.ToInt64(Rej_Purity_ID);
                        }

                        objMFGRejectionTransferManualProperty.group_name = Val.ToString(drw["group_name"]);
                        objMFGRejectionTransferManualProperty.pcs = Val.ToDecimal(drw["pcs"]);
                        objMFGRejectionTransferManualProperty.carat = Val.ToDecimal(drw["carat"]);
                        objMFGRejectionTransferManualProperty.rate = Val.ToDecimal(drw["rate"]);
                        objMFGRejectionTransferManualProperty.amount = Val.ToDecimal(drw["amount"]);

                        objMFGRejectionTransferManualProperty = objMFGRejectionTransferManual.Save_Detail(objMFGRejectionTransferManualProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                        IntSrNo = Val.ToInt64(objMFGRejectionTransferManualProperty.sr_no);

                        Count++;
                        IntCounter++;
                        SetControlPropertyValue(lblProgressCount, "Text", Count.ToString() + "" + "/" + "" + TotalCount.ToString() + " Completed....");
                    }
                    Conn.Inter1.Commit();
                }
                //Conn.Inter1.Commit();
                catch (Exception ex)
                {
                    IntSrNo = -1;
                    Conn.Inter1.Rollback();

                    Conn = null;
                    General.ShowErrors(ex.ToString());
                    return;
                }
                finally
                {
                    objMFGRejectionTransferManualProperty = null;
                }
            }
            catch (Exception ex)
            {
                Conn.Inter1.Rollback();
                Conn = null;
                General.ShowErrors(ex.ToString());
                if (ex.InnerException != null)
                {
                    Global.Message(ex.InnerException.ToString());
                }
            }
        }
        private void backgroundWorker_MFGRejectionTransferManual_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                panelProgress.Visible = false;
                if (IntSrNo > 0)
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Rejection Transfer Manual Entry Data Save Successfully");
                        ClearDetails();
                        PopulateDetails();
                        objMFGRejectionTransferManual = new MFGRejectionTransferManual();
                    }
                    else
                    {
                        Global.Confirm("Rejection Transfer Manual Entry Data Update Successfully");
                        ClearDetails();
                        PopulateDetails();
                    }
                }
                else
                {
                    Global.Confirm("Error In Rejection Transfer Manual Invoice");
                    dtpTransferDate.Focus();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }
        private void lueKapan_Validated(object sender, EventArgs e)
        {
            m_dtbParam = new DataTable();
            if (lueKapan.Text.ToString() != "")
            {
                m_dtbParam = Global.GetRoughKapanPending_Data(Val.ToInt64(lueKapan.EditValue));

                if (m_dtbParam.Rows.Count > 0)
                {
                    txtKapanWt.Text = m_dtbParam.Rows[0]["kapan_carat"].ToString();
                    txtRejWt.Text = m_dtbParam.Rows[0]["rej_carat"].ToString();
                    txtManualWt.Text = m_dtbParam.Rows[0]["mfg_carat"].ToString();
                    txtPendingWt.Text = m_dtbParam.Rows[0]["pending_carat"].ToString();
                    txtOpeningCarat.Text = m_dtbParam.Rows[0]["op_carat"].ToString();
                    txtTranPlusWt.Text = m_dtbParam.Rows[0]["trf_plus_carat"].ToString();
                    txtTranMinusWt.Text = m_dtbParam.Rows[0]["trf_minus_carat"].ToString();
                    lblDiffCarat.Text = m_dtbParam.Rows[0]["pending_carat"].ToString();
                }
                else
                {
                    txtKapanWt.Text = "0";
                    txtRejWt.Text = "0";
                    txtManualWt.Text = "0";
                    txtPendingWt.Text = "0";
                    txtOpeningCarat.Text = "0";
                    txtTranPlusWt.Text = "0";
                    txtTranMinusWt.Text = "0";
                    lblDiffCarat.Text = "0";
                }
            }
        }
        private void lueKapan_EditValueChanged(object sender, EventArgs e)
        {
            //m_dtbParam = new DataTable();
            //if (lueKapan.Text.ToString() != "")
            //{
            //    m_dtbParam = Global.GetRoughKapanPending_Data(Val.ToInt64(lueKapan.EditValue));

            //    if (m_dtbParam.Rows.Count > 0)
            //    {
            //        txtKapanWt.Text = m_dtbParam.Rows[0]["kapan_carat"].ToString();
            //        txtRejWt.Text = m_dtbParam.Rows[0]["rej_carat"].ToString();
            //        txtManualWt.Text = m_dtbParam.Rows[0]["mfg_carat"].ToString();
            //        txtPendingWt.Text = m_dtbParam.Rows[0]["pending_carat"].ToString();
            //    }
            //    else
            //    {
            //        txtKapanWt.Text = "0";
            //        txtRejWt.Text = "0";
            //        txtManualWt.Text = "0";
            //        txtPendingWt.Text = "0";
            //    }
            //}
        }
        private void txtOpeningCarat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as DevExpress.XtraEditors.TextEdit).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }
        private void txtKapanWt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as DevExpress.XtraEditors.TextEdit).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }
        private void txtRejWt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as DevExpress.XtraEditors.TextEdit).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }
        private void txtManualWt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as DevExpress.XtraEditors.TextEdit).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }
        private void txtPendingWt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as DevExpress.XtraEditors.TextEdit).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }
        private void txtTranPlusWt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as DevExpress.XtraEditors.TextEdit).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }
        private void txtTranMinusWt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as DevExpress.XtraEditors.TextEdit).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }
        private void txtCarat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as DevExpress.XtraEditors.TextEdit).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }
        private void txtRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as DevExpress.XtraEditors.TextEdit).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }
        private void lueKapan_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmMFGRoughStockEntry objRoughStockEntry = new FrmMFGRoughStockEntry();
                objRoughStockEntry.ShowDialog();
                Global.LOOKUPRejKapan(lueKapan);
            }
        }

        #region "Grid Events" 
        private void dgvMFGRejectionTransferManual_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                m_numTotalCarats = Math.Round(Val.ToDecimal(clmDetCarat.SummaryItem.SummaryValue), 2, MidpointRounding.AwayFromZero);

                if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == "rate")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                        m_numTotalAmount = 0;
                    else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                        m_numTotalAmount += (Val.ToDecimal(e.GetValue("carat")) * Val.ToDecimal(e.GetValue("rate")));
                    else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    {
                        if (m_numTotalAmount > 0 && m_numTotalCarats > 0)
                            e.TotalValue = Math.Round((m_numTotalAmount / m_numTotalCarats), 2, MidpointRounding.AwayFromZero);
                        else
                            e.TotalValue = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        private void dgvMFGRejectionTransferManual_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvMFGRejectionTransferManual.GetDataRow(e.RowHandle);
                        btnAdd.Text = "&Update";
                        lueType.Text = Val.ToString(Drow["type"]);
                        txtPurityGroup.Text = Val.ToString(Drow["group_name"]);
                        lueRejPurity.EditValue = Val.ToInt64(Drow["purity_id"]);
                        txtPcs.Text = Val.ToString(Drow["pcs"]);
                        txtCarat.Text = Val.ToString(Drow["carat"]);
                        txtRate.Text = Val.ToString(Drow["rate"]);
                        txtAmount.Text = Val.ToString(Drow["amount"]);
                        m_numcarat = Val.ToDecimal(Drow["carat"]);
                        m_transfer_detail_id = Val.ToInt(Drow["transfer_detail_id"]);
                        m_update_srno = Val.ToInt(Drow["sr_no"]);
                        decimal Diff_Carat = Math.Round(Val.ToDecimal(clmDetCarat.SummaryItem.SummaryValue), 3) + Val.ToDecimal(txtPendingWt.Text);
                        lblDiffCarat.Text = Diff_Carat.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void dgvMFGRejectionTrfManualList_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                objMFGRejectionTransferManual = new MFGRejectionTransferManual();
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvMFGRejectionTrfManualList.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt32(Drow["transfer_id"]);
                        dtpTransferDate.Text = Val.DBDate(Val.ToString(Drow["transfer_date"]));
                        lueKapan.EditValue = Val.ToInt64(Drow["kapan_id"]);

                        m_dtbRejTransferManualDetails = objMFGRejectionTransferManual.GetDataDetails(Val.ToInt64(lblMode.Tag));

                        grdMFGRejectionTransferManual.DataSource = m_dtbRejTransferManualDetails;

                        m_dtbParam = Global.GetRoughKapanPending_Data(Val.ToInt64(lueKapan.EditValue));

                        if (m_dtbParam.Rows.Count > 0)
                        {
                            txtKapanWt.Text = m_dtbParam.Rows[0]["kapan_carat"].ToString();
                            txtRejWt.Text = m_dtbParam.Rows[0]["rej_carat"].ToString();
                            txtManualWt.Text = m_dtbParam.Rows[0]["mfg_carat"].ToString();
                            txtOpeningCarat.Text = m_dtbParam.Rows[0]["op_carat"].ToString();
                            txtTranPlusWt.Text = m_dtbParam.Rows[0]["trf_plus_carat"].ToString();
                            txtTranMinusWt.Text = m_dtbParam.Rows[0]["trf_minus_carat"].ToString();
                            txtPendingWt.Text = Val.ToString(Val.ToDecimal(txtOpeningCarat.Text) + Val.ToDecimal(txtTranPlusWt.Text) + Val.ToDecimal(txtKapanWt.Text) - (Val.ToDecimal(txtRejWt.Text) + Val.ToDecimal(txtManualWt.Text) + Val.ToDecimal(txtTranMinusWt.Text)));
                            decimal Diff_Carat = Math.Round(Val.ToDecimal(clmCarat.SummaryItem.SummaryValue), 3) + Val.ToDecimal(txtPendingWt.Text);
                            lblDiffCarat.Text = Diff_Carat.ToString();
                        }
                        else
                        {
                            txtKapanWt.Text = "0";
                            txtRejWt.Text = "0";
                            txtManualWt.Text = "0";
                            txtPendingWt.Text = "0";
                            txtOpeningCarat.Text = "0";
                            txtTranPlusWt.Text = "0";
                            txtTranMinusWt.Text = "0";
                            lblDiffCarat.Text = "0";
                        }

                        ttlbMFGRejJangedIssue.SelectedTabPage = tblRejJangeddetail;
                        dtpTransferDate.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void dgvMFGRejectionTrfManualList_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                if (Val.ToDecimal(clmLAmount.SummaryItem.SummaryValue) > 0 && Val.ToDecimal(clmLCarat.SummaryItem.SummaryValue) > 0)
                {
                    m_numSummRate = Math.Round((Val.ToDecimal(clmLAmount.SummaryItem.SummaryValue) / Val.ToDecimal(clmLCarat.SummaryItem.SummaryValue)), 2, MidpointRounding.AwayFromZero);
                }
                else
                {
                    m_numSummRate = 0;
                }
                if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == "rate")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                        e.TotalValue = m_numSummRate;
                }

            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
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
                RoughKapan = objRoughStockEntry.Kapan_GetData();
                lueKapan.Properties.DataSource = RoughKapan;
                lueKapan.Properties.ValueMember = "kapan_id";
                lueKapan.Properties.DisplayMember = "kapan_no";

                RejPurity = objRejPurity.GetData(1);
                lueRejPurity.Properties.DataSource = RejPurity;
                lueRejPurity.Properties.ValueMember = "purity_id";
                lueRejPurity.Properties.DisplayMember = "purity_name";

                dtpFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpFromDate.EditValue = DateTime.Now;

                dtpToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpToDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpToDate.EditValue = DateTime.Now;

                dtpTransferDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpTransferDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpTransferDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpTransferDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpTransferDate.EditValue = DateTime.Now;

                m_dtbType = new DataTable();
                m_dtbType.Columns.Add("type");
                m_dtbType.Rows.Add("ROUGH");
                m_dtbType.Rows.Add("REJECTION");

                lueType.Properties.DataSource = m_dtbType;
                lueType.Properties.ValueMember = "type";
                lueType.Properties.DisplayMember = "type";
                lueType.EditValue = "ROUGH";

                lueKapan.Focus();
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
                    DataRow drwNew = m_dtbRejTransferManualDetails.NewRow();
                    decimal numPcs = Val.ToDecimal(txtPcs.Text);
                    decimal numCarat = Val.ToDecimal(txtCarat.Text);
                    decimal numRate = Val.ToDecimal(txtRate.Text);
                    decimal numAmount = Val.ToDecimal(txtAmount.Text);

                    drwNew["transfer_id"] = Val.ToInt(0);
                    drwNew["pcs"] = numPcs;
                    drwNew["carat"] = numCarat;
                    drwNew["rate"] = Val.ToDecimal(txtRate.Text);
                    drwNew["amount"] = Math.Round(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text), 2);
                    m_srno = m_srno + 1;
                    drwNew["sr_no"] = Val.ToInt(m_srno);

                    drwNew["purity_id"] = Val.ToInt64(lueRejPurity.EditValue);
                    drwNew["purity_name"] = Val.ToString(lueRejPurity.Text);
                    drwNew["group_name"] = Val.ToString(txtPurityGroup.Text);
                    drwNew["type"] = Val.ToString(lueType.Text);

                    m_dtbRejTransferManualDetails.Rows.Add(drwNew);
                    dgvMFGRejectionTransferManual.MoveLast();
                }
                else if (btnAdd.Text == "&Update")
                {
                    for (int i = 0; i < m_dtbRejTransferManualDetails.Rows.Count; i++)
                    {
                        if (m_dtbRejTransferManualDetails.Select("transfer_detail_id ='" + m_transfer_detail_id + "' AND sr_no = '" + m_update_srno + "'").Length > 0)
                        {
                            if (m_dtbRejTransferManualDetails.Rows[m_update_srno - 1]["transfer_detail_id"].ToString() == m_transfer_detail_id.ToString())
                            {
                                m_dtbRejTransferManualDetails.Rows[m_update_srno - 1]["pcs"] = Val.ToDecimal(txtPcs.Text).ToString();
                                m_dtbRejTransferManualDetails.Rows[m_update_srno - 1]["carat"] = Val.ToDecimal(txtCarat.Text).ToString();
                                m_dtbRejTransferManualDetails.Rows[m_update_srno - 1]["rate"] = Val.ToDecimal(txtRate.Text);
                                m_dtbRejTransferManualDetails.Rows[m_update_srno - 1]["amount"] = Math.Round(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text), 2);
                                m_dtbRejTransferManualDetails.Rows[m_update_srno - 1]["type"] = Val.ToString(lueType.Text);
                                m_dtbRejTransferManualDetails.Rows[m_update_srno - 1]["group_name"] = Val.ToString(txtPurityGroup.Text);
                                m_dtbRejTransferManualDetails.Rows[m_update_srno - 1]["purity_id"] = Val.ToInt64(lueRejPurity.EditValue);
                                m_dtbRejTransferManualDetails.Rows[m_update_srno - 1]["purity_name"] = Val.ToString(lueRejPurity.Text);
                                break;
                            }
                        }
                    }
                    btnAdd.Text = "&Add";
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
                    if (m_dtbRejTransferManualDetails.Rows.Count == 0)
                    {
                        lstError.Add(new ListError(22, "Record"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                        }
                    }
                    if (dgvMFGRejectionTransferManual == null)
                    {
                        lstError.Add(new ListError(22, "Record"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                        }
                    }
                    var result = DateTime.Compare(Convert.ToDateTime(dtpTransferDate.Text), DateTime.Today);
                    if (result > 0)
                    {
                        lstError.Add(new ListError(5, " Transfer Date Not Be Greater Than Today Date"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            dtpTransferDate.Focus();
                        }
                    }

                    if (lblMode.Text == "Add Mode")
                    {
                        decimal Total_wt = Math.Round(Val.ToDecimal(clmDetCarat.SummaryItem.SummaryValue), 3);

                        if (Val.ToDecimal(txtPendingWt.Text) < Total_wt)
                        {
                            lstError.Add(new ListError(5, " Total Carat Not More Then Rejection Pending Carat"));
                            if (!blnFocus)
                            {
                                blnFocus = true;
                            }
                        }
                    }
                    else
                    {
                        decimal Total_wt = Math.Round(Val.ToDecimal(clmDetCarat.SummaryItem.SummaryValue), 3);

                        string Pending_Weight = Val.ToString(Val.ToDecimal(txtOpeningCarat.Text) + Val.ToDecimal(txtTranPlusWt.Text) + Val.ToDecimal(txtKapanWt.Text) + Val.ToDecimal(lblDiffCarat.Text));

                        if (Val.ToDecimal(Pending_Weight) < Total_wt)
                        {
                            lstError.Add(new ListError(5, " Total Carat Not More Then Rejection Pending Carat"));
                            if (!blnFocus)
                            {
                                blnFocus = true;
                            }
                        }
                    }
                }
                if (m_blnadd)
                {
                    //if (Val.ToDouble(txtPcs.Text) == 0)
                    //{
                    //    lstError.Add(new ListError(12, "Pcs"));
                    //    if (!blnFocus)
                    //    {
                    //        blnFocus = true;
                    //        txtPcs.Focus();
                    //    }
                    //}
                    if (Val.ToDouble(txtCarat.Text) == 0)
                    {
                        lstError.Add(new ListError(12, "Carat"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtCarat.Focus();
                        }
                    }
                    if (Val.ToDouble(txtRate.Text) == 0)
                    {
                        lstError.Add(new ListError(12, "Rate"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtRate.Focus();
                        }
                    }

                    if (Val.ToDouble(txtAmount.Text) == 0)
                    {
                        lstError.Add(new ListError(12, "Amount"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtAmount.Focus();
                        }
                    }

                    if (lueType.Text == "")
                    {
                        lstError.Add(new ListError(13, "Type"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueType.Focus();
                        }
                    }
                    if (lueRejPurity.Text == "")
                    {
                        lstError.Add(new ListError(13, "Rej Purity"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueRejPurity.Focus();
                        }
                    }
                    if (txtPurityGroup.Text == "")
                    {
                        lstError.Add(new ListError(13, "Purity Group"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtPurityGroup.Focus();
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
                if (!GenerateRejectionTransferManualDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }

                lblMode.Tag = 0;
                lblMode.Text = "Add Mode";
                lueRejPurity.EditValue = System.DBNull.Value;
                lueKapan.EditValue = System.DBNull.Value;

                dtpTransferDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpTransferDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpTransferDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpTransferDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpTransferDate.EditValue = DateTime.Now;

                txtPcs.Text = string.Empty;
                txtCarat.Text = string.Empty;
                txtRate.Text = string.Empty;
                txtAmount.Text = string.Empty;

                txtOpeningCarat.Text = string.Empty;
                txtKapanWt.Text = string.Empty;
                txtRejWt.Text = string.Empty;
                txtManualWt.Text = string.Empty;
                txtPendingWt.Text = string.Empty;
                txtTranPlusWt.Text = string.Empty;
                txtTranMinusWt.Text = string.Empty;
                txtPurityGroup.Text = "";
                m_srno = 0;
                lblDiffCarat.Text = "0";
                btnAdd.Text = "&Add";
                dtpTransferDate.Focus();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
        }
        private bool PopulateDetails()
        {
            objMFGRejectionTransferManual = new MFGRejectionTransferManual();
            bool blnReturn = true;
            DateTime datFromDate = DateTime.MinValue;
            DateTime datToDate = DateTime.MinValue;
            try
            {
                m_dtbDetails = objMFGRejectionTransferManual.GetData(Val.DBDate(dtpFromDate.Text), Val.DBDate(dtpToDate.Text));

                if (m_dtbDetails.Rows.Count == 0)
                {
                    Global.Message("Data Not Found");
                    blnReturn = false;
                }
                grdMFGRejectionTrfManualList.DataSource = m_dtbDetails;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            finally
            {
                objMFGRejectionTransferManual = null;
            }
            return blnReturn;
        }
        private bool GenerateRejectionTransferManualDetails()
        {
            bool blnReturn = true;
            try
            {
                if (m_dtbRejTransferManualDetails.Rows.Count > 0)
                    m_dtbRejTransferManualDetails.Rows.Clear();

                m_dtbRejTransferManualDetails = new DataTable();

                m_dtbRejTransferManualDetails.Columns.Add("sr_no", typeof(int));
                m_dtbRejTransferManualDetails.Columns.Add("transfer_detail_id", typeof(int));
                m_dtbRejTransferManualDetails.Columns.Add("transfer_id", typeof(int));
                m_dtbRejTransferManualDetails.Columns.Add("purity_id", typeof(Int64));
                m_dtbRejTransferManualDetails.Columns.Add("purity_name", typeof(string));
                m_dtbRejTransferManualDetails.Columns.Add("pcs", typeof(decimal)).DefaultValue = 0;
                m_dtbRejTransferManualDetails.Columns.Add("carat", typeof(decimal)).DefaultValue = 0;
                m_dtbRejTransferManualDetails.Columns.Add("rate", typeof(decimal)).DefaultValue = 0;
                m_dtbRejTransferManualDetails.Columns.Add("amount", typeof(decimal)).DefaultValue = 0;
                m_dtbRejTransferManualDetails.Columns.Add("group_name", typeof(string));
                m_dtbRejTransferManualDetails.Columns.Add("type", typeof(string));

                grdMFGRejectionTransferManual.DataSource = m_dtbRejTransferManualDetails;
                grdMFGRejectionTransferManual.Refresh();
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
                            dgvMFGRejectionTrfManualList.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvMFGRejectionTrfManualList.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvMFGRejectionTrfManualList.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvMFGRejectionTrfManualList.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvMFGRejectionTrfManualList.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvMFGRejectionTrfManualList.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvMFGRejectionTrfManualList.ExportToCsv(Filepath);
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

        private void RepDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (Global.Confirm("Are you sure delete selected row?", "STOCK", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                MFGRejectionTransferManualProperty objMFGRejectionTransferManualProperty = new MFGRejectionTransferManualProperty();
                MFGRejectionTransferManual objMFGRejectionTransferManual = new MFGRejectionTransferManual();
                int IntRes = 0;
                Int64 transfer_detail_id = Val.ToInt64(dgvMFGRejectionTransferManual.GetFocusedRowCellValue("transfer_detail_id").ToString());
                objMFGRejectionTransferManualProperty.transfer_detail_id = Val.ToInt64(transfer_detail_id);

                if (transfer_detail_id == 0)
                {
                    dgvMFGRejectionTransferManual.DeleteRow(dgvMFGRejectionTransferManual.GetRowHandle(dgvMFGRejectionTransferManual.FocusedRowHandle));
                    m_dtbRejTransferManualDetails.AcceptChanges();
                }
                else
                {
                    IntRes = objMFGRejectionTransferManual.DeleteRejectionTransferManual(objMFGRejectionTransferManualProperty);
                    dgvMFGRejectionTransferManual.DeleteRow(dgvMFGRejectionTransferManual.GetRowHandle(dgvMFGRejectionTransferManual.FocusedRowHandle));
                    m_dtbRejTransferManualDetails.AcceptChanges();
                }

                if (IntRes == -1)
                {
                    Global.Confirm("Error in Detail Deleted Data.");
                    objMFGRejectionTransferManualProperty = null;
                }
                else
                {
                    Global.Confirm("Detail Deleted successfully...");
                    objMFGRejectionTransferManualProperty = null;
                }
            }
        }

        private void txtPcs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}