using BLL;
using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.PropertyClasses.Transaction;
using BLL.PropertyClasses.Transaction.MFG;
using DERP.Class;
using DERP.Report.Barcode_Print;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DERP.Transaction.MFG
{
    public partial class FrmMFGRussionSplit : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        FormEvents objBOFormEvents = new FormEvents();
        Validation Val = new Validation();
        BLL.FormPer ObjPer = new BLL.FormPer();
        DataTable m_dtbDetail = new DataTable();
        DataTable m_dtbParam = new DataTable();
        DataTable m_dtbSubProcess = new DataTable();
        BLL.BeginTranConnection Conn;
        DataTable m_dtbLotMixSplit = new DataTable();
        Control _NextEnteredControl = new Control();
        DataTable DtControlSettings = new DataTable();
        private List<Control> _tabControls = new List<Control>();
        MFGFactorySplit objFactorySplit = new MFGFactorySplit();
        DataTable dtIss = new DataTable();
        DataTable m_OldLotDetail = new DataTable();
        DataTable dtBarcodePrint = new DataTable();
        bool m_blnadd = new bool();
        bool m_blnsave = new bool();
        int m_Srno = 1;
        int m_update_srno = 1;
        decimal m_numSummRate = 0;
        int m_numForm_id = 0;
        bool m_blnflag = new bool();
        Int64 IntRes;
        decimal m_updateCarat = 0;
        int m_updatePcs = 0;
        #endregion

        #region Constructor
        public FrmMFGRussionSplit()
        {
            InitializeComponent();
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

        #region Validation
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (m_blnsave)
                {
                    if (m_dtbLotMixSplit.Rows.Count == 0)
                    {
                        lstError.Add(new ListError(5, "Atleast 1 Record must be enter in Split grid"));
                        if (!blnFocus)
                        {
                            blnFocus = true;

                        }
                    }
                    var result = DateTime.Compare(Convert.ToDateTime(dtpReceiveDate.Text), DateTime.Today);
                    if (result > 0)
                    {
                        lstError.Add(new ListError(5, " Recieve Date Not Be Greater Than Today Date"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            dtpReceiveDate.Focus();
                        }
                    }
                    if (Val.ToString(dtpReceiveDate.Text) == string.Empty)
                    {
                        lstError.Add(new ListError(22, "Date"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            dtpReceiveDate.Focus();
                        }
                    }
                    if (Val.ToDecimal(lblTotalCarat.Text) - Val.ToDecimal(0.015) > Val.ToDecimal(clmCarat.SummaryItem.SummaryValue))
                    {
                        lstError.Add(new ListError(5, "Carat is not allow to lesser 0.015 than total carat."));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtCarat.Focus();
                        }
                    }
                    if ((Val.ToInt(lblTotalPcs.Text) - Val.ToInt(1)) > (Val.ToInt(clmPcs.SummaryItem.SummaryValue)))
                    {
                        lstError.Add(new ListError(5, "Pcs is not allow to lesser 1 than total pcs."));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtPcs.Focus();
                        }
                    }

                    //if (Val.ToDecimal(lblTotalCarat.Text) != Val.ToDecimal(clmCarat.SummaryItem.SummaryValue))
                    //{
                    //    lstError.Add(new ListError(5, "Carat not match than balance carat."));
                    //    if (!blnFocus)
                    //    {
                    //        blnFocus = true;
                    //        txtCarat.Focus();
                    //    }
                    //}
                }

                if (m_blnadd)
                {

                    if (Val.ToDecimal(txtCarat.Text) == 0)
                    {
                        lstError.Add(new ListError(12, "Carat"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtCarat.Focus();
                        }
                    }
                    if (btnAdd.Text == "&Add")
                    {
                        if ((Val.ToInt32(lblTotalPcs.Text) + 1) < Val.ToInt32(clmPcs.SummaryItem.SummaryValue) + Val.ToInt32(txtPcs.Text))
                        {

                            lstError.Add(new ListError(5, "Carat not greater than balance pcs."));
                            if (!blnFocus)
                            {
                                blnFocus = true;
                                txtPcs.Focus();
                            }
                        }
                    }
                    if (btnAdd.Text == "&Update" && m_blnflag == true)
                    {
                        if ((Val.ToInt32(lblTotalPcs.Text) + 1) < (Val.ToInt32(clmPcs.SummaryItem.SummaryValue) + Val.ToInt32(txtPcs.Text)) - m_updatePcs)
                        {
                            lstError.Add(new ListError(5, "Carat not greater than balance pcs."));
                            if (!blnFocus)
                            {
                                blnFocus = true;
                                txtPcs.Focus();
                            }
                        }
                    }
                    if (btnAdd.Text == "&Add")
                    {
                        if ((Val.ToDecimal(lblTotalCarat.Text) + Val.ToDecimal(0.015)) < Val.ToDecimal(clmCarat.SummaryItem.SummaryValue) + Val.ToDecimal(txtCarat.Text))
                        {
                            lstError.Add(new ListError(5, "Carat not greater than balance carat."));
                            if (!blnFocus)
                            {
                                blnFocus = true;
                                txtCarat.Focus();
                            }
                        }
                    }
                    if (btnAdd.Text == "&Update" && m_blnflag == true)
                    {
                        if ((Val.ToDecimal(lblTotalCarat.Text) + Val.ToDecimal(0.015)) < (Val.ToDecimal(clmCarat.SummaryItem.SummaryValue) + Val.ToDecimal(txtCarat.Text)) - m_updateCarat)
                        {
                            lstError.Add(new ListError(5, "Carat not greater than balance carat."));
                            if (!blnFocus)
                            {
                                blnFocus = true;
                                txtCarat.Focus();
                            }
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
        #endregion

        #region Events
        private void RepDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            dgvFactorySplit.DeleteRow(dgvFactorySplit.GetRowHandle(dgvFactorySplit.FocusedRowHandle));
            m_dtbLotMixSplit.AcceptChanges();
        }
        private void backgroundWorker_LotSplit_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                MFGFactorySplit objMFGFactorySplit = new MFGFactorySplit();
                MFG_Factory_SplitProperty objMFGFactorySplitProperty = new MFG_Factory_SplitProperty();
                Conn = new BeginTranConnection(true, false);
                DataRow drwNew;
                try
                {
                    IntRes = 0;
                    Int64 NewHistory_Union_Id = 0;
                    dtBarcodePrint = new DataTable();
                    dtBarcodePrint.Columns.Add("kapan_no", typeof(string));
                    dtBarcodePrint.Columns.Add("sr_no", typeof(string));
                    dtBarcodePrint.Columns.Add("lot_id", typeof(int));
                    dtBarcodePrint.Columns.Add("pcs", typeof(int));
                    dtBarcodePrint.Columns.Add("carat", typeof(decimal));
                    dtBarcodePrint.Columns.Add("receive_date", typeof(string));
                    int count = 0;
                    int srno_count = 0;
                    string SrNo = string.Empty;
                    Int64 IntLotNO = 0;
                    string IntSub_LotNO = string.Empty;
                    Int64 Lot_SrNo = 0;

                    IntLotNO = Val.ToInt(objMFGFactorySplit.FindMaxLotNo(Val.ToInt64(txtLotID.Text)));


                    IntSub_LotNO = objMFGFactorySplit.FindMaxSubLotNo(Val.ToInt64(m_dtbLotMixSplit.Rows[0]["rough_cut_id"]));

                    //if(IntSub_LotNO.ToString().Contains("-"))
                    //{
                    //    Sub_Lot_Srno = Val.ToInt64(IntSub_LotNO.Split('-')[1]);
                    //}
                    //else
                    //{
                    //    Sub_Lot_Srno = 0;
                    //}              

                    foreach (DataRow drw in m_dtbLotMixSplit.Rows)
                    {
                        if (Val.ToInt32(IntSub_LotNO) == 0)
                        {
                            srno_count = srno_count + 1;
                            SrNo = Val.ToString(Val.ToInt64(IntLotNO)) + '-' + srno_count;
                            objMFGFactorySplitProperty.sub_lot_no = Val.ToString(SrNo);
                        }
                        else
                        {
                            IntSub_LotNO = Val.ToString(Val.ToInt64(Val.ToInt64(IntSub_LotNO) + 1));
                            SrNo = Val.ToString(Val.ToInt64(IntLotNO)) + '-' + IntSub_LotNO;
                            objMFGFactorySplitProperty.sub_lot_no = Val.ToString(SrNo);
                        }
                        //Sub_Lot_Srno = Sub_Lot_Srno + 1;
                        //SrNo = Val.ToString(Val.ToInt64(IntLotNO)) + '-' + Sub_Lot_Srno;
                        objMFGFactorySplitProperty.lot_no = Val.ToInt64(IntLotNO);
                        //objMFGFactorySplitProperty.sub_lot_no = Val.ToString(SrNo);

                        objMFGFactorySplitProperty.lot_id = Val.ToInt64(txtLotID.Text);
                        objMFGFactorySplitProperty.rough_cut_id = Val.ToInt(drw["rough_cut_id"]);
                        objMFGFactorySplitProperty.from_lot_id = Val.ToInt(drw["lot_id"]);
                        objMFGFactorySplitProperty.mix_split_date = Val.DBDate(dtpReceiveDate.Text);// Val.DBDate(Val.ToString(drw["issue_date"]));
                        objMFGFactorySplitProperty.manager_id = Val.ToInt(drw["manager_id"]); //Val.ToInt(drw["manager_id"]);
                        objMFGFactorySplitProperty.employee_id = Val.ToInt(drw["employee_id"]);//Val.ToInt(drw["employee_id"]);
                        objMFGFactorySplitProperty.process_id = Val.ToInt(drw["process_id"]); //Val.ToInt(drw["process_id"]);
                        objMFGFactorySplitProperty.sub_process_id = Val.ToInt(drw["sub_process_id"]); //Val.ToInt(drw["sub_process_id"]);
                        objMFGFactorySplitProperty.quality_id = Val.ToInt(drw["quality_id"]);
                        objMFGFactorySplitProperty.purity_id = Val.ToInt(drw["purity_id"]);
                        objMFGFactorySplitProperty.from_pcs = Val.ToInt(lblTotalPcs.Text);
                        objMFGFactorySplitProperty.from_carat = Val.ToDecimal(lblTotalCarat.Text);
                        objMFGFactorySplitProperty.to_pcs = Val.ToInt(drw["pcs"]);
                        objMFGFactorySplitProperty.to_carat = Val.ToDecimal(drw["carat"]);
                        objMFGFactorySplitProperty.rate = Val.ToDecimal(drw["rate"]);
                        objMFGFactorySplitProperty.amount = Val.ToDecimal(drw["amount"]);
                        objMFGFactorySplitProperty.prediction_id = Val.ToInt(m_OldLotDetail.Rows[0]["prediction_id"]);
                        objMFGFactorySplitProperty.form_id = Val.ToInt(m_numForm_id);
                        objMFGFactorySplitProperty.union_id = IntRes;
                        objMFGFactorySplitProperty.kapan_id = Val.ToInt(drw["kapan_id"]);
                        objMFGFactorySplitProperty.history_union_id = NewHistory_Union_Id;
                        objMFGFactorySplitProperty.lot_srno = Lot_SrNo;
                        objMFGFactorySplitProperty = objMFGFactorySplit.Russion_Save(objMFGFactorySplitProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                        IntRes = objMFGFactorySplitProperty.union_id;
                        NewHistory_Union_Id = Val.ToInt64(objMFGFactorySplitProperty.history_union_id);
                        Lot_SrNo = Val.ToInt64(objMFGFactorySplitProperty.lot_srno);
                        drwNew = dtBarcodePrint.NewRow();
                        drwNew["lot_id"] = Val.ToInt(objMFGFactorySplitProperty.to_lot_id);
                        drwNew["sr_no"] = Val.ToString(objMFGFactorySplitProperty.sub_lot_no);
                        drwNew["receive_date"] = Val.DBDate(dtpReceiveDate.Text);
                        drwNew["kapan_no"] = Val.ToString(drw["kapan_no"]);
                        drwNew["pcs"] = Val.ToInt(drw["pcs"]);
                        drwNew["carat"] = Val.ToDecimal(drw["carat"]);
                        dtBarcodePrint.Rows.Add(drwNew);
                        count++;

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
        private void backgroundWorker_LotSplit_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (IntRes > 0)
                {
                    Global.Confirm("Lot Split Recieve Data Save Succesfully");
                    btnSave.Enabled = true;
                    ClearDetails();

                    DialogResult result = MessageBox.Show("Do you want to Print Barcode?", "Confirmation", MessageBoxButtons.YesNoCancel);
                    if (result != DialogResult.Yes)
                    {
                        btnSave.Enabled = true;
                        return;
                    }
                    else
                    {
                        PrintBarcode();
                        btnSave.Enabled = true;
                    }
                }
                else
                {
                    Global.Confirm("Error In Lot Split Recieve");
                    btnSave.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }
        private void PrintBarcode()
        {
            BarcodePrint printBarCode = new BarcodePrint();
            //DataTable dtCheckedBarcode = (DataTable)grdBarcodePrint.DataSource;
            //dtCheckedBarcode = dtCheckedBarcode.Select("print = 'True' ").CopyToDataTable();
            for (int i = 0; i < dtBarcodePrint.Rows.Count; i++)
            {
                if (dtBarcodePrint.Rows[i]["lot_id"] != null && dtBarcodePrint.Rows[i]["carat"].ToString() != "")
                {
                    printBarCode.AddPkt(dtBarcodePrint.Rows[i]["kapan_no"].ToString(), dtBarcodePrint.Rows[i]["sr_no"].ToString(), Val.ToString(dtBarcodePrint.Rows[i]["receive_date"]),
                        Val.ToInt(dtBarcodePrint.Rows[i]["lot_id"]), Val.ToInt(dtBarcodePrint.Rows[i]["pcs"]), Math.Round(Val.ToDecimal(dtBarcodePrint.Rows[i]["carat"]), 3), true);
                }
            }
            //printBarCode.PrintDMX();
            printBarCode.PrintTSC();
        }
        private void FrmMFGMixSplit_Load(object sender, EventArgs e)
        {
            try
            {
                dtpReceiveDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpReceiveDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpReceiveDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpReceiveDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpReceiveDate.EditValue = DateTime.Now;

                ClearDetails();
                txtLotID.Focus();
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }

        private void dgvMixSplit_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                DataTable dtAmount = new DataTable();
                dtAmount = (DataTable)grdFactorySplit.DataSource;

                decimal rate = 0;
                decimal carat = 0;
                decimal amount = 0;

                string column = "";
                for (int j = 0; j <= dtAmount.Columns.Count - 1; j++)
                {
                    if (dtAmount.Columns[j].ToString().Contains("carat"))
                    {
                        carat = dtAmount.AsEnumerable().Sum(x => Val.ToDecimal(x[dtAmount.Columns[j]]));
                    }
                    if (dtAmount.Columns[j].ToString().Contains("amount"))
                    {
                        amount = dtAmount.AsEnumerable().Sum(x => Math.Round(Val.ToDecimal(x[dtAmount.Columns[j]]), 0));
                    }
                    if (dtAmount.Columns[j].ToString().Contains("rate"))
                    {
                        column = dtAmount.Columns[j].ToString();
                        amount = 0;
                    }
                    if (Val.ToDecimal(amount) > 0 && Val.ToDecimal(carat) > 0)
                    {
                        if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == column)
                        {
                            rate = Math.Round(amount / carat, 3);
                            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                                e.TotalValue = rate;
                            column = "";
                            carat = 0;
                            amount = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }

        private void dgvMixSplit_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvFactorySplit.GetDataRow(e.RowHandle);
                        btnAdd.Text = "&Update";
                        txtPcs.Text = Val.ToString(Drow["pcs"]);
                        txtCarat.Text = Val.ToString(Drow["carat"]);
                        m_update_srno = Val.ToInt(Drow["sr_no"]);
                        m_updateCarat = Val.ToDecimal(Drow["carat"]);
                        m_updatePcs = Val.ToInt32(Drow["pcs"]);
                        m_blnflag = true;
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
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
                string Str = "";
                if (Val.DBDate(BLL.GlobalDec.gStrServerDate) != Val.DBDate(dtpReceiveDate.Text))
                {
                    Str = GlobalDec.CheckLockIsOpenOrNot(Val.DBDate(dtpReceiveDate.Text), Val.DBTime(DateTime.Now.ToShortTimeString())); //Val.GetFullTime12());
                    if (Str != "YES")
                    {
                        if (Str != "")
                        {
                            Global.Message(Str);
                            return;
                        }
                        else
                        {
                            Global.Message("You Are Not Suppose to Make Entry On Different Date");
                            return;
                        }
                    }
                    else
                    {
                        dtpReceiveDate.Enabled = true;
                        dtpReceiveDate.Visible = true;
                    }
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
                DialogResult result = MessageBox.Show("Do you want to save Split data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result != DialogResult.Yes)
                {
                    btnSave.Enabled = true;
                    return;
                }

                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                backgroundWorker_LotSplit.RunWorkerAsync();

                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearDetails();
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            Global.Export("xlsx", dgvFactorySplit);
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (AddInGrid())
                {
                    if (GlobalDec.gEmployeeProperty.department_name == "GALAXY" && GlobalDec.gEmployeeProperty.branch_name == "KAMALA ESTATE" ||
                        GlobalDec.gEmployeeProperty.user_name == "HARDIK" || GlobalDec.gEmployeeProperty.user_name == "TEJAL" || GlobalDec.gEmployeeProperty.user_name == "PRIYANKA")
                    {
                        txtPcs.Text = "1";
                        txtCarat.Text = string.Empty;
                        txtPcs.Focus();
                    }
                    else
                    {
                        txtPcs.Text = "1";
                        txtCarat.Text = string.Empty;
                        txtCarat.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
            }
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
                    DataRow drwNew = m_dtbLotMixSplit.NewRow();
                    int numPcs = Val.ToInt(txtPcs.Text);
                    decimal numCarat = Val.ToDecimal(txtCarat.Text);
                    decimal numRate = Val.ToDecimal(m_OldLotDetail.Rows[0]["rate"]);
                    decimal numAmount = Val.ToDecimal(numCarat * numRate);

                    drwNew["manager_id"] = Val.ToInt(m_OldLotDetail.Rows[0]["manager_id"]);
                    drwNew["manager_name"] = Val.ToString(m_OldLotDetail.Rows[0]["manager_name"]);
                    drwNew["employee_id"] = Val.ToInt(m_OldLotDetail.Rows[0]["employee_id"]);
                    drwNew["employee_name"] = Val.ToString(m_OldLotDetail.Rows[0]["employee_name"]);
                    drwNew["rough_cut_no"] = Val.ToString(m_OldLotDetail.Rows[0]["rough_cut_no"]);
                    drwNew["rough_cut_id"] = Val.ToInt(m_OldLotDetail.Rows[0]["rough_cut_id"]);
                    drwNew["lot_id"] = Val.ToInt64(txtLotID.Text);
                    drwNew["kapan_id"] = Val.ToInt(m_OldLotDetail.Rows[0]["kapan_id"]);
                    drwNew["kapan_no"] = Val.ToString(m_OldLotDetail.Rows[0]["kapan_no"]);
                    drwNew["rough_sieve_id"] = Val.ToInt(m_OldLotDetail.Rows[0]["rough_sieve_id"]);
                    drwNew["sieve_name"] = Val.ToString(m_OldLotDetail.Rows[0]["sieve_name"]);
                    drwNew["rough_clarity_id"] = Val.ToInt(m_OldLotDetail.Rows[0]["rough_clarity_id"]);
                    drwNew["rough_clarity_name"] = Val.ToString(m_OldLotDetail.Rows[0]["rough_clarity_name"]);
                    drwNew["purity_id"] = Val.ToInt(m_OldLotDetail.Rows[0]["purity_id"]);
                    drwNew["purity_name"] = Val.ToString(m_OldLotDetail.Rows[0]["purity_name"]);
                    drwNew["quality_id"] = Val.ToInt(m_OldLotDetail.Rows[0]["quality_id"]);
                    drwNew["quality_name"] = Val.ToString(m_OldLotDetail.Rows[0]["quality_name"]);
                    drwNew["process_id"] = Val.ToInt(m_OldLotDetail.Rows[0]["prd_process"]);
                    drwNew["process_name"] = Val.ToString(m_OldLotDetail.Rows[0]["process_name"]);
                    drwNew["sub_process_id"] = Val.ToInt(m_OldLotDetail.Rows[0]["prd_sub_process_id"]);
                    drwNew["sub_process_name"] = Val.ToString(m_OldLotDetail.Rows[0]["sub_process_name"]);
                    drwNew["pcs"] = numPcs;
                    drwNew["carat"] = numCarat;
                    drwNew["rate"] = numRate;
                    drwNew["amount"] = numAmount;
                    drwNew["sr_no"] = m_Srno;
                    m_dtbLotMixSplit.Rows.Add(drwNew);
                    m_Srno++;
                }
                else if (btnAdd.Text == "&Update")
                {
                    if (m_dtbLotMixSplit.Select("sr_no ='" + Val.ToInt(m_update_srno) + "'").Length > 0)
                    {
                        for (int i = 0; i < m_dtbLotMixSplit.Rows.Count; i++)
                        {
                            if (m_dtbLotMixSplit.Select("rough_cut_no ='" + m_OldLotDetail.Rows[0]["rough_cut_no"] + "' AND sr_no = '" + m_update_srno + "'").Length > 0)
                            {
                                if (m_dtbLotMixSplit.Rows[dgvFactorySplit.FocusedRowHandle]["sr_no"].ToString() == Val.ToString(m_update_srno))
                                {
                                    m_dtbLotMixSplit.Rows[dgvFactorySplit.FocusedRowHandle]["pcs"] = Val.ToInt(txtPcs.Text).ToString();
                                    m_dtbLotMixSplit.Rows[dgvFactorySplit.FocusedRowHandle]["carat"] = Val.ToDecimal(txtCarat.Text).ToString();
                                    m_dtbLotMixSplit.Rows[dgvFactorySplit.FocusedRowHandle]["rate"] = Val.ToDecimal(m_OldLotDetail.Rows[0]["rate"]);
                                    m_dtbLotMixSplit.Rows[dgvFactorySplit.FocusedRowHandle]["amount"] = Val.ToDecimal(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(m_OldLotDetail.Rows[0]["rate"]));
                                    m_blnflag = false;
                                    break;
                                }
                            }
                        }
                    }
                    btnAdd.Text = "&Add";
                }
                dgvFactorySplit.MoveLast();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
        }
        private void txtLotID_Validated(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToInt64(txtLotID.Text) > 0)
                {
                    DataTable dttab = new DataTable();
                    if (GlobalDec.gEmployeeProperty.branch_name == "KAMLA ESTATE" && GlobalDec.gEmployeeProperty.department_name == "GALAXY")
                    {
                        dttab = objFactorySplit.CheckProcess(Val.ToInt64(txtLotID.Text), "MARKING");
                        if (Val.ToInt32(dttab.Rows[0]["cnt"]) == 0)
                        {
                            Global.Message("Please check marking process not complete.");
                            return;
                        }
                    }
                    m_OldLotDetail = new DataTable();
                    m_OldLotDetail = objFactorySplit.GetLotData(Val.ToInt64(txtLotID.Text));
                    if (m_OldLotDetail.Rows.Count > 0)
                    {
                        lblTotalPcs.Text = Val.ToString(m_OldLotDetail.Rows[0]["balance_pcs"]);
                        lblTotalCarat.Text = Val.ToString(m_OldLotDetail.Rows[0]["balance_carat"]);
                        txtLotID.Enabled = false;
                    }
                    else
                    {
                        lblTotalPcs.Text = Val.ToString("0");
                        lblTotalCarat.Text = Val.ToString("0.00");
                        Global.Message("Lot Not Found");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void dgvDet_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                if (Val.ToDecimal(clmAmount.SummaryItem.SummaryValue) > 0 && Val.ToDecimal(clmCarat.SummaryItem.SummaryValue) > 0)
                {
                    m_numSummRate = Math.Round((Val.ToDecimal(clmAmount.SummaryItem.SummaryValue) / Val.ToDecimal(clmCarat.SummaryItem.SummaryValue)), 2, MidpointRounding.AwayFromZero);
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

        #region Function
        private bool GenerateLotSplitDetails()
        {
            bool blnReturn = true;
            try
            {
                if (m_dtbLotMixSplit.Rows.Count > 0)
                    m_dtbLotMixSplit.Rows.Clear();

                m_dtbLotMixSplit = new DataTable();

                m_dtbLotMixSplit.Columns.Add("lot_id", typeof(int));
                m_dtbLotMixSplit.Columns.Add("rough_cut_no", typeof(string));
                m_dtbLotMixSplit.Columns.Add("rough_cut_id", typeof(int));
                m_dtbLotMixSplit.Columns.Add("kapan_no", typeof(string));
                m_dtbLotMixSplit.Columns.Add("kapan_id", typeof(int));
                m_dtbLotMixSplit.Columns.Add("quality_name", typeof(string));
                m_dtbLotMixSplit.Columns.Add("quality_id", typeof(int));
                m_dtbLotMixSplit.Columns.Add("purity_name", typeof(string));
                m_dtbLotMixSplit.Columns.Add("purity_id", typeof(int));
                m_dtbLotMixSplit.Columns.Add("rough_clarity_name", typeof(string));
                m_dtbLotMixSplit.Columns.Add("rough_clarity_id", typeof(int));
                m_dtbLotMixSplit.Columns.Add("sieve_name", typeof(string));
                m_dtbLotMixSplit.Columns.Add("rough_sieve_id", typeof(int));
                m_dtbLotMixSplit.Columns.Add("manager_name", typeof(string));
                m_dtbLotMixSplit.Columns.Add("manager_id", typeof(int));
                m_dtbLotMixSplit.Columns.Add("employee_name", typeof(string));
                m_dtbLotMixSplit.Columns.Add("employee_id", typeof(int));
                m_dtbLotMixSplit.Columns.Add("process_name", typeof(string));
                m_dtbLotMixSplit.Columns.Add("process_id", typeof(int));
                m_dtbLotMixSplit.Columns.Add("sub_process_name", typeof(string));
                m_dtbLotMixSplit.Columns.Add("sub_process_id", typeof(int));
                m_dtbLotMixSplit.Columns.Add("pcs", typeof(int)).DefaultValue = 0;
                m_dtbLotMixSplit.Columns.Add("carat", typeof(decimal)).DefaultValue = 0;
                m_dtbLotMixSplit.Columns.Add("plus_carat", typeof(decimal)).DefaultValue = 0;
                m_dtbLotMixSplit.Columns.Add("loss_carat", typeof(decimal)).DefaultValue = 0;
                m_dtbLotMixSplit.Columns.Add("sr_no", typeof(decimal)).DefaultValue = 1;
                m_dtbLotMixSplit.Columns.Add("rate", typeof(decimal)).DefaultValue = 0;
                m_dtbLotMixSplit.Columns.Add("amount", typeof(decimal)).DefaultValue = 0;

                grdFactorySplit.DataSource = m_dtbLotMixSplit;
                grdFactorySplit.Refresh();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
        }
        private bool ClearDetails()
        {
            bool blnReturn = true;
            try
            {
                if (!GenerateLotSplitDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }
                dtpReceiveDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpReceiveDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpReceiveDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpReceiveDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpReceiveDate.EditValue = DateTime.Now;

                txtPcs.Text = "1";
                txtCarat.Text = string.Empty;
                lblTotalPcs.Text = "0";
                lblTotalCarat.Text = "0.00";
                txtLotID.Text = string.Empty;
                txtLotID.Enabled = true;
                txtLotID.Focus();
                m_Srno = 1;
                m_update_srno = 0;
                btnAdd.Text = "&Add";
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
        }
        #endregion

        private void txtLotID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPcs_EditValueChanged(object sender, EventArgs e)
        {
            if (BLL.GlobalDec.gEmployeeProperty.branch_id == 38)
            {
                decimal perPieceCarat = Val.ToDecimal(lblTotalCarat.Text) / Val.ToInt(lblTotalPcs.Text);
                decimal Carat = Math.Round(Val.ToInt(txtPcs.Text) * perPieceCarat, 3);
                txtCarat.Text = Carat.ToString();

            }
        }
    }
}
