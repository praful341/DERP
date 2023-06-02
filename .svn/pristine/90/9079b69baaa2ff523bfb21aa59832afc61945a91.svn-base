using BLL;
using BLL.FunctionClasses.Master.MFG;
using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using DERP.Master.MFG;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DREP.Master.MFG;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Transaction.MFG
{
    public partial class FrmMFGLSAssortFinal : DevExpress.XtraEditors.XtraForm
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
        MFGLotSplit objLotSplitReceive = new MFGLotSplit();
        DataTable m_dtOutstanding = new DataTable();
        DataTable m_dtbKapan = new DataTable();
        DataTable DTab_StockData = new DataTable();
        MFGMixSplit objMFGMixSplit = new MFGMixSplit();
        MfgRoughClarityMaster objRoughClarity = new MfgRoughClarityMaster();
        MFGProcessReceive objProcessReceive = new MFGProcessReceive();
        MfgRoughSieve objRoughSieve = new MfgRoughSieve();
        DataTable dtTemp = new DataTable();

        private List<Control> _tabControls = new List<Control>();
        MFGProcessReceive objProcessRecieve = new MFGProcessReceive();
        DataTable dtIss = new DataTable();
        IDataObject PasteclipData = Clipboard.GetDataObject();
        String PasteData = "";
        DataTable DtPending = new DataTable();
        DataTable dtBarcodePrint = new DataTable();
        DataTable DTabTemp = new DataTable();
        int Process_Id = 0;
        int Sub_Process_Id = 0;
        DataTable m_dtbType = new DataTable();
        //DataTable DTab_KapanWiseData = new DataTable();

        bool m_blnadd = new bool();
        bool m_blnsave = new bool();
        decimal m_numSummRate = 0;
        int m_numForm_id = 0;
        int IntTotalLot = 0;
        double DblTotalCarat = 0.00;
        int IntTotalPcs = 0;
        Int64 IntRes;
        Int64 IntRes_MixSplit;
        Int64 Receive_IntRes;
        Int64 Issue_IntRes;
        Int64 New_Lot_ID;
        Int64 MixSplit_IntRes;
        int Count_Mix = 0;
        #endregion

        #region Constructor
        public FrmMFGLSAssortFinal()
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
                else if ((Control)sender is CheckedComboBoxEdit)
                {
                    ((CheckedComboBoxEdit)(Control)sender).ShowPopup();
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

        #region Validation
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();

            try
            {
                if (m_blnsave)
                {
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

                    if (DTab_StockData.Rows.Count == 0)
                    {
                        lstError.Add(new ListError(5, "Atleast 1 Record must be enter in Split grid"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                        }
                    }

                    decimal Carat = 0;

                    for (int i = 0; i <= dtTemp.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j <= dtTemp.Columns.Count - 1; j++)
                        {
                            if (dtTemp.Columns[j].ToString().Contains("carat"))
                            {
                                if (Val.ToDecimal(dtTemp.Rows[i][j]) != 0)
                                {
                                    Carat = Carat + Val.ToDecimal(dtTemp.Rows[i][j]);
                                }
                            }
                        }
                    }

                    if (Val.ToDecimal(txtCarat.Text) != Carat)
                    {
                        lstError.Add(new ListError(5, "Carat not match Please Check.."));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                        }
                    }

                    //decimal Mix_Carat = Val.ToDecimal(clmSplitCarat.SummaryItem.SummaryValue) + Val.ToDecimal(clmLossCarat.SummaryItem.SummaryValue) - Val.ToDecimal(clmCaratPlus.SummaryItem.SummaryValue);

                    //if (Val.ToDecimal(clmCarat.SummaryItem.SummaryValue) != Mix_Carat)
                    //{
                    //    lstError.Add(new ListError(5, "Carat not match Please Check.."));
                    //    if (!blnFocus)
                    //    {
                    //        blnFocus = true;
                    //    }
                    //}
                }

                if (m_blnadd)
                {
                    if (lueCutNo.Text == "")
                    {
                        lstError.Add(new ListError(13, "Cut No"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueCutNo.Focus();
                        }
                    }
                    if (lueSieve.EditValue.ToString() == string.Empty)
                    {
                        lstError.Add(new ListError(12, "Rough Sieve"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueSieve.Focus();
                        }
                    }
                    if (lueClarity.EditValue.ToString() == string.Empty)
                    {
                        lstError.Add(new ListError(12, "Clarity"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueClarity.Focus();
                        }
                    }
                    if (lueQuality.EditValue.ToString() == string.Empty)
                    {
                        lstError.Add(new ListError(12, "Quality"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueQuality.Focus();
                        }
                    }
                    if (Val.ToDecimal(txtCarat.Text) == 0)
                    {
                        lstError.Add(new ListError(12, "Carat"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtCarat.Focus();
                        }
                    }

                    if (Val.ToDecimal(txtRate.Text) == 0)
                    {
                        lstError.Add(new ListError(12, "Rate"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtRate.Focus();
                        }
                    }
                    if (Val.ToDecimal(txtAmount.Text) == 0)
                    {
                        lstError.Add(new ListError(12, "Amount"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtAmount.Focus();
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateDetails())
                {
                    return;
                }

                DataTable dtbDetails_old = (DataTable)grdLSAssortFinalSplit.DataSource;

                this.Cursor = Cursors.WaitCursor;

                dgvLSAssortFinalSplit.Columns.Clear();
                pivot pt = new pivot(objProcessReceive.Process_Quality_LS_AssortFinal_GetData(Val.ToString(lueType.Text), Val.ToString(lueClarity.EditValue), Val.ToString(lueSieve.EditValue), Val.ToString(lueQuality.EditValue)));
                //DataTable dtbDetails = pt.PivotDataSuperPlusAssortment(new string[] { "quality_id", "quality" }, new string[] { "carat" }, new AggregateFunction[] { AggregateFunction.Sum, AggregateFunction.Sum, AggregateFunction.Sum }, new string[] { "sieve_name" });
                DataTable dtbDetails = pt.PivotDataSuperPlus(new string[] { "quality_id", "quality" }, new string[] { "pcs", "carat" }, new AggregateFunction[] { AggregateFunction.Sum, AggregateFunction.Sum, AggregateFunction.Sum, AggregateFunction.Sum }, new string[] { "sieve_name" });

                DataTable DTab_Clarity = objRoughClarity.GetData();
                DataTable DTab_Sieve = objRoughSieve.GetData();
                DataTable Merge_Data = new DataTable();
                Merge_Data.Columns.Add("Clarity_Sieve_Pcs", typeof(string));
                Merge_Data.Columns.Add("Clarity_Sieve_Carat", typeof(string));
                //Merge_Data.Columns.Add("Clarity_Sieve_Rate", typeof(string));
                //Merge_Data.Columns.Add("Clarity_Sieve_Amount", typeof(string));

                for (int i = 0; i < DTab_Clarity.Rows.Count; i++)
                {
                    for (int j = 0; j < DTab_Sieve.Rows.Count; j++)
                    {
                        Merge_Data.Rows.Add(DTab_Clarity.Rows[i]["rough_clarity_id"] + "_" + DTab_Sieve.Rows[j]["rough_sieve_id"] + "_" + DTab_Clarity.Rows[i]["rough_clarity_name"] + DTab_Sieve.Rows[j]["sieve_name"] + "_pcs", DTab_Clarity.Rows[i]["rough_clarity_id"] + "_" + DTab_Sieve.Rows[j]["rough_sieve_id"] + "_" + DTab_Clarity.Rows[i]["rough_clarity_name"] + DTab_Sieve.Rows[j]["sieve_name"] + "_carat");
                    }
                }
                //int carat_seq = 2;
                //int rate_seq = 3;
                //int amount_seq = 4;

                //for (int i = 0; i < Merge_Data.Rows.Count; i++)
                //{
                //    for (int j = 0; j < dtbDetails.Columns.Count; j++)
                //    {
                //        if (dtbDetails.Columns.Contains(Merge_Data.Rows[i][0].ToString()))
                //        {
                //            dtbDetails.Columns[Merge_Data.Rows[i][0].ToString()].SetOrdinal(carat_seq);
                //            carat_seq = carat_seq;
                //            break;
                //        }
                //    }
                //}

                //for (int i = 0; i < Merge_Data.Rows.Count; i++)
                //{
                //    for (int j = 0; j < dtbDetails.Columns.Count; j++)
                //    {
                //        if (dtbDetails.Columns.Contains(Merge_Data.Rows[i][1].ToString()))
                //        {
                //            dtbDetails.Columns[Merge_Data.Rows[i][1].ToString()].SetOrdinal(rate_seq);
                //            rate_seq = rate_seq + 3;
                //            break;
                //        }
                //    }
                //}

                //for (int i = 0; i < Merge_Data.Rows.Count; i++)
                //{
                //    for (int j = 0; j < dtbDetails.Columns.Count; j++)
                //    {
                //        if (dtbDetails.Columns.Contains(Merge_Data.Rows[i][2].ToString()))
                //        {
                //            dtbDetails.Columns[Merge_Data.Rows[i][2].ToString()].SetOrdinal(amount_seq);
                //            amount_seq = amount_seq + 3;
                //            break;
                //        }
                //    }
                //}
                DataColumn Total_Pcs = new System.Data.DataColumn("T_Pcs", typeof(System.Int32));
                DataColumn Total = new System.Data.DataColumn("Total", typeof(System.Decimal));
                //DataColumn Rate = new System.Data.DataColumn("Rate", typeof(System.Decimal));
                //DataColumn Amount = new System.Data.DataColumn("Amount", typeof(System.Decimal));
                Total_Pcs.DefaultValue = "0";
                Total.DefaultValue = "0.000";
                //Rate.DefaultValue = "0.000";
                //Amount.DefaultValue = "0.000";
                dtbDetails.Columns.Add(Total_Pcs);
                dtbDetails.Columns.Add(Total);
                //dtbDetails.Columns.Add(Rate);
                //dtbDetails.Columns.Add(Amount);

                DataColumnCollection columns = dtbDetails.Columns;
                if (dtbDetails_old != null)
                {
                    if (dtbDetails_old.Columns.Count > 0)
                    {
                        for (int i = 0; i < dtbDetails_old.Rows.Count; i++)
                        {
                            for (int j = 0; j < dtbDetails_old.Columns.Count; j++)
                            {
                                for (int k = 0; k < dtbDetails.Rows.Count; k++)
                                {
                                    if (dtbDetails.Rows[k]["quality"].ToString().Trim() == dtbDetails_old.Rows[i]["quality"].ToString().Trim())
                                    {
                                        if (columns.Contains(dtbDetails_old.Columns[j].ColumnName.ToString().Trim()))
                                        {
                                            dtbDetails.Rows[k][dtbDetails_old.Columns[j].ColumnName.ToString().Trim()] = dtbDetails_old.Rows[i][dtbDetails_old.Columns[j].ColumnName.ToString().Trim()];
                                        }
                                    }
                                }

                            }
                        }
                    }
                }

                dtTemp = dtbDetails.Copy();
                grdLSAssortFinalSplit.DataSource = dtTemp;

                dgvLSAssortFinalSplit.Columns["quality_id"].Visible = false;
                dgvLSAssortFinalSplit.Columns["quality"].OptionsColumn.ReadOnly = true;
                dgvLSAssortFinalSplit.Columns["quality"].OptionsColumn.AllowFocus = false;
                dgvLSAssortFinalSplit.Columns["quality"].Fixed = FixedStyle.Left;
                dgvLSAssortFinalSplit.Columns["T_Pcs"].OptionsColumn.ReadOnly = true;
                dgvLSAssortFinalSplit.Columns["T_Pcs"].OptionsColumn.AllowFocus = false;
                dgvLSAssortFinalSplit.Columns["Total"].OptionsColumn.ReadOnly = true;
                dgvLSAssortFinalSplit.Columns["Total"].OptionsColumn.AllowFocus = false;
                //dgvLSAssortFinalSplit.Columns["Rate"].OptionsColumn.ReadOnly = true;
                //dgvLSAssortFinalSplit.Columns["Rate"].OptionsColumn.AllowFocus = false;
                //dgvLSAssortFinalSplit.Columns["Amount"].OptionsColumn.ReadOnly = true;
                //dgvLSAssortFinalSplit.Columns["Amount"].OptionsColumn.AllowFocus = false;

                //for (int i = 0; i <= dtTemp.Rows.Count - 1; i++)
                //{
                //    for (int j = 0; j <= dtTemp.Columns.Count - 1; j++)
                //    {
                //        if (dtTemp.Columns[j].ToString().Contains("amount"))
                //        {
                //            dgvLSAssortFinalSplit.Columns[j].OptionsColumn.AllowEdit = false;
                //        }
                //    }
                //}

                for (int i = 0; i <= dtTemp.Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= dtTemp.Columns.Count - 1; j++)
                    {
                        if (dtTemp.Columns[j].ToString().Contains("pcs"))
                        {
                            string pcs = dtTemp.Columns[j].ToString();
                            GridColumn column0 = dgvLSAssortFinalSplit.Columns[pcs];
                            dgvLSAssortFinalSplit.Columns[pcs].SummaryItem.DisplayFormat = "{0:n0}";
                            column0.SummaryItem.SummaryType = SummaryItemType.Sum;
                        }
                        if (dtTemp.Columns[j].ToString().Contains("carat"))
                        {
                            string carat = dtTemp.Columns[j].ToString();
                            GridColumn column1 = dgvLSAssortFinalSplit.Columns[carat];
                            dgvLSAssortFinalSplit.Columns[carat].SummaryItem.DisplayFormat = "{0:n3}";
                            column1.SummaryItem.SummaryType = SummaryItemType.Sum;
                        }

                        //if (dtTemp.Columns[j].ToString().Contains("rate"))
                        //{
                        //    string rate = dtTemp.Columns[j].ToString();
                        //    GridColumn column2 = dgvLSAssortFinalSplit.Columns[rate];
                        //    dgvLSAssortFinalSplit.Columns[rate].SummaryItem.DisplayFormat = "{0:n3}";
                        //    column2.SummaryItem.SummaryType = SummaryItemType.Custom;
                        //}

                        //if (dtTemp.Columns[j].ToString().Contains("amount"))
                        //{
                        //    string amount = dtTemp.Columns[j].ToString();
                        //    GridColumn column3 = dgvLSAssortFinalSplit.Columns[amount];
                        //    dgvLSAssortFinalSplit.Columns[amount].SummaryItem.DisplayFormat = "{0:n3}";
                        //    column3.SummaryItem.SummaryType = SummaryItemType.Sum;
                        //}
                        if (dtTemp.Columns[j].ColumnName.Contains("T_Pcs"))
                        {
                            string total_pcs = dtTemp.Columns[j].ToString();
                            GridColumn column2 = dgvLSAssortFinalSplit.Columns[total_pcs];
                            dgvLSAssortFinalSplit.Columns[total_pcs].SummaryItem.DisplayFormat = "{0:n0}";
                            column2.SummaryItem.SummaryType = SummaryItemType.Sum;
                        }
                        if (dtTemp.Columns[j].ColumnName.Contains("Total"))
                        {
                            string total = dtTemp.Columns[j].ToString();
                            GridColumn column4 = dgvLSAssortFinalSplit.Columns[total];
                            dgvLSAssortFinalSplit.Columns[total].SummaryItem.DisplayFormat = "{0:n3}";
                            column4.SummaryItem.SummaryType = SummaryItemType.Sum;
                        }
                        //if (dtTemp.Columns[j].ColumnName.Contains("Rate"))
                        //{
                        //    string totrate = dtTemp.Columns[j].ToString();
                        //    GridColumn column5 = dgvLSAssortFinalSplit.Columns[totrate];
                        //    dgvLSAssortFinalSplit.Columns[totrate].SummaryItem.DisplayFormat = "{0:n3}";
                        //    column5.SummaryItem.SummaryType = SummaryItemType.Custom;
                        //}
                        //if (dtTemp.Columns[j].ColumnName.Contains("Amount"))
                        //{
                        //    string totamount = dtTemp.Columns[j].ToString();
                        //    GridColumn column6 = dgvLSAssortFinalSplit.Columns[totamount];
                        //    dgvLSAssortFinalSplit.Columns[totamount].SummaryItem.DisplayFormat = "{0:n3}";
                        //    column6.SummaryItem.SummaryType = SummaryItemType.Sum;
                        //}
                    }
                    break;
                }

                dgvLSAssortFinalSplit.OptionsView.ShowFooter = true;
                dgvLSAssortFinalSplit.BestFitColumns();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
            }
        }

        private void lueType_EditValueChanged(object sender, EventArgs e)
        {
            MfgRoughClarityMaster objClarity = new MfgRoughClarityMaster();
            DataTable dtbClarity = new DataTable();
            DataTable dtbdetails = new DataTable();
            dtbClarity = objClarity.GetData();

            if (lueType.EditValue.ToString() != "")
            {
                if (lueType.EditValue.ToString() != "BOTH")
                {
                    dtbClarity.DefaultView.RowFilter = "type = '" + lueType.EditValue + "'";
                    dtbClarity.DefaultView.ToTable();
                    dtbdetails = dtbClarity.DefaultView.ToTable();
                }
                else
                {
                    dtbdetails = dtbClarity.Copy();
                }
                lueClarity.Properties.DataSource = dtbdetails;
                lueClarity.Properties.ValueMember = "rough_clarity_id";
                lueClarity.Properties.DisplayMember = "rough_clarity_name";
            }
        }

        private void lueMixCutNo_EditValueChanged(object sender, EventArgs e)
        {
            if (Val.ToInt(lueMixCutNo.EditValue) > 0)
            {
                btnPopUpStock.Enabled = true;
            }
            else
            {
                btnPopUpStock.Enabled = false;
            }
        }

        private void lueMixKapan_EditValueChanged(object sender, EventArgs e)
        {
            m_dtbParam = new DataTable();
            if (lueMixKapan.Text.ToString() != "")
            {
                m_dtbParam = Global.GetRoughKapanWise_Data(Val.ToInt(lueMixKapan.EditValue));
                //if (m_dtbParam.Rows.Count == 0)
                //{
                //    m_dtbParam = DTab_KapanWiseData;
                //}
            }
            lueMixCutNo.Properties.DataSource = m_dtbParam;
            lueMixCutNo.Properties.ValueMember = "rough_cut_id";
            lueMixCutNo.Properties.DisplayMember = "rough_cut_no";
        }

        private void backgroundWorker_LSAssortFinal_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {

                MFGLotSplit objMFGLotSplit = new MFGLotSplit();
                MFGMixSplitProperty objMFGMixSplitProperty = new MFGMixSplitProperty();
                MFGProcessReceiveProperty objMFGProcessReceiveProperty = new MFGProcessReceiveProperty();
                Conn = new BeginTranConnection(true, false);
                //dtBarcodePrint = new DataTable();
                //dtBarcodePrint.Columns.Add("kapan_no", typeof(string));
                //dtBarcodePrint.Columns.Add("sr_no", typeof(int));
                //dtBarcodePrint.Columns.Add("lot_id", typeof(int));
                //dtBarcodePrint.Columns.Add("pcs", typeof(int));
                //dtBarcodePrint.Columns.Add("carat", typeof(decimal));
                //dtBarcodePrint.Columns.Add("receive_date", typeof(string));
                try
                {
                    IntRes = 0;
                    Count_Mix = 0;
                    New_Lot_ID = 0;
                    MixSplit_IntRes = 0;
                    Int64 NewHistory_Union_Id = 0;
                    Int64 Lot_SrNo = 0;

                    //decimal Plus_carat = Val.ToDecimal(clmCaratPlus.SummaryItem.SummaryValue);
                    //decimal Loss_Carat = Val.ToDecimal(clmLossCarat.SummaryItem.SummaryValue);
                    DataTable Mix_Data = (DataTable)grdDet.DataSource;

                    foreach (DataRow drw in Mix_Data.Rows)
                    {
                        if (Count_Mix == 0)
                        {
                            objMFGMixSplitProperty.pcs = Val.ToInt(clmPcs.SummaryItem.SummaryValue);
                            objMFGMixSplitProperty.carat = Val.ToDecimal(clmCarat.SummaryItem.SummaryValue);
                            m_numSummRate = Math.Round((Val.ToDecimal(clmAmount.SummaryItem.SummaryValue) / Val.ToDecimal(clmCarat.SummaryItem.SummaryValue)), 2, MidpointRounding.AwayFromZero);
                            objMFGMixSplitProperty.rate = Val.ToDecimal(m_numSummRate);
                            objMFGMixSplitProperty.amount = Val.ToDecimal(clmAmount.SummaryItem.SummaryValue);

                            objMFGMixSplitProperty.from_lot_id = Val.ToInt64(drw["lot_id"]);
                            objMFGMixSplitProperty.prediction_id = Val.ToInt64(drw["prediction_id"]);
                            objMFGMixSplitProperty.new_lot_id = New_Lot_ID;
                            objMFGMixSplitProperty.receive_date = Val.DBDate(dtpReceiveDate.Text);
                            objMFGMixSplitProperty.from_pcs = Val.ToInt(drw["pcs"]);
                            objMFGMixSplitProperty.from_carat = Val.ToDecimal(drw["carat"]);
                            objMFGMixSplitProperty.quality_id = Val.ToInt64(drw["quality_id"]);
                            objMFGMixSplitProperty.count = Count_Mix;
                            objMFGMixSplitProperty.mix_union_id = MixSplit_IntRes;
                            objMFGMixSplitProperty.form_id = Val.ToInt(m_numForm_id);
                            objMFGMixSplitProperty.rough_cut_id = Val.ToInt64(lueCutNo.EditValue);
                            objMFGMixSplitProperty.kapan_id = Val.ToInt64(lueKapan.EditValue);
                            //objMFGMixSplitProperty.loss_carat = Val.ToDecimal(Loss_Carat);
                            //objMFGMixSplitProperty.plus_carat = Val.ToDecimal(Plus_carat);

                            objMFGMixSplitProperty.manager_id = Val.ToInt(drw["manager_id"]);
                            objMFGMixSplitProperty.employee_id = Val.ToInt(drw["employee_id"]);
                            objMFGMixSplitProperty.process_id = Val.ToInt(drw["process_id"]);
                            objMFGMixSplitProperty.sub_process_id = Val.ToInt(drw["sub_process_id"]);
                            objMFGMixSplitProperty.rough_clarity_id = Val.ToInt(drw["rough_clarity_id"]);
                            objMFGMixSplitProperty.rough_sieve_id = Val.ToInt(drw["rough_sieve_id"]);
                            objMFGMixSplitProperty.purity_id = Val.ToInt(drw["purity_id"]);

                            objMFGMixSplitProperty.union_id = IntRes;
                            objMFGMixSplitProperty.receive_union_id = Receive_IntRes;
                            objMFGMixSplitProperty.issue_union_id = Issue_IntRes;
                            objMFGMixSplitProperty.history_union_id = NewHistory_Union_Id;
                            objMFGMixSplitProperty.lot_srno = Lot_SrNo;
                            objMFGMixSplitProperty.transaction_type = "MANY-TO-MANY";

                            objMFGMixSplitProperty = objMFGMixSplit.Save_MFGStockMixData(objMFGMixSplitProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                            New_Lot_ID = objMFGMixSplitProperty.new_lot_id;
                            MixSplit_IntRes = objMFGMixSplitProperty.mix_union_id;
                            IntRes = objMFGMixSplitProperty.union_id;
                            Receive_IntRes = objMFGMixSplitProperty.receive_union_id;
                            Issue_IntRes = objMFGMixSplitProperty.issue_union_id;
                            NewHistory_Union_Id = Val.ToInt64(objMFGMixSplitProperty.history_union_id);
                            Lot_SrNo = Val.ToInt64(objMFGMixSplitProperty.lot_srno);
                            IntRes_MixSplit = objMFGMixSplit.Update_Balance_Carat(Val.ToString(drw["lot_id"]), DLL.GlobalDec.EnumTran.Continue, Conn);
                            Count_Mix = Count_Mix + 1;
                        }
                        else
                        {
                            objMFGMixSplitProperty.pcs = Val.ToInt(clmPcs.SummaryItem.SummaryValue);
                            objMFGMixSplitProperty.carat = Val.ToDecimal(clmCarat.SummaryItem.SummaryValue);
                            m_numSummRate = Math.Round((Val.ToDecimal(clmAmount.SummaryItem.SummaryValue) / Val.ToDecimal(clmCarat.SummaryItem.SummaryValue)), 2, MidpointRounding.AwayFromZero);
                            objMFGMixSplitProperty.rate = Val.ToDecimal(m_numSummRate);
                            objMFGMixSplitProperty.amount = Val.ToDecimal(clmAmount.SummaryItem.SummaryValue);

                            objMFGMixSplitProperty.from_lot_id = Val.ToInt64(drw["lot_id"]);
                            objMFGMixSplitProperty.new_lot_id = New_Lot_ID;
                            objMFGMixSplitProperty.receive_date = Val.DBDate(dtpReceiveDate.Text);
                            objMFGMixSplitProperty.from_pcs = Val.ToInt(drw["pcs"]);
                            objMFGMixSplitProperty.from_carat = Val.ToDecimal(drw["carat"]);
                            objMFGMixSplitProperty.quality_id = Val.ToInt64(drw["quality_id"]);
                            objMFGMixSplitProperty.count = Count_Mix;
                            objMFGMixSplitProperty.mix_union_id = MixSplit_IntRes;
                            objMFGMixSplitProperty.form_id = Val.ToInt(m_numForm_id);
                            objMFGMixSplitProperty.rough_cut_id = Val.ToInt64(lueCutNo.EditValue);
                            objMFGMixSplitProperty.kapan_id = Val.ToInt64(lueKapan.EditValue);
                            objMFGMixSplitProperty.loss_carat = Val.ToDecimal(0);
                            objMFGMixSplitProperty.plus_carat = Val.ToDecimal(0);

                            objMFGMixSplitProperty.manager_id = Val.ToInt(drw["manager_id"]);
                            objMFGMixSplitProperty.employee_id = Val.ToInt(drw["employee_id"]);
                            objMFGMixSplitProperty.process_id = Val.ToInt(drw["process_id"]);
                            objMFGMixSplitProperty.sub_process_id = Val.ToInt(drw["sub_process_id"]);
                            objMFGMixSplitProperty.rough_clarity_id = Val.ToInt(drw["rough_clarity_id"]);
                            objMFGMixSplitProperty.rough_sieve_id = Val.ToInt(drw["rough_sieve_id"]);
                            objMFGMixSplitProperty.purity_id = Val.ToInt(drw["purity_id"]);

                            objMFGMixSplitProperty.union_id = IntRes;
                            objMFGMixSplitProperty.receive_union_id = Receive_IntRes;
                            objMFGMixSplitProperty.issue_union_id = Issue_IntRes;
                            objMFGMixSplitProperty.history_union_id = NewHistory_Union_Id;
                            objMFGMixSplitProperty.lot_srno = Lot_SrNo;
                            objMFGMixSplitProperty.transaction_type = "MANY-TO-MANY";

                            objMFGMixSplitProperty = objMFGMixSplit.Save_MFGStockMixData(objMFGMixSplitProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                            New_Lot_ID = objMFGMixSplitProperty.new_lot_id;
                            MixSplit_IntRes = objMFGMixSplitProperty.mix_union_id;
                            IntRes = objMFGMixSplitProperty.union_id;
                            Receive_IntRes = objMFGMixSplitProperty.receive_union_id;
                            Issue_IntRes = objMFGMixSplitProperty.issue_union_id;
                            NewHistory_Union_Id = Val.ToInt64(objMFGMixSplitProperty.history_union_id);
                            Lot_SrNo = Val.ToInt64(objMFGMixSplitProperty.lot_srno);
                            IntRes_MixSplit = objMFGMixSplit.Update_Balance_Carat(Val.ToString(drw["lot_id"]), DLL.GlobalDec.EnumTran.Continue, Conn);
                            Count_Mix = Count_Mix + 1;
                        }
                    }
                    DataTable m_DTab = new DataTable();
                    m_DTab = (DataTable)grdLSAssortFinalSplit.DataSource;
                    int SrNo = 0;
                    DataTable dtbDetail = m_DTab.Copy();

                    for (int i = dtbDetail.Columns.Count - 2; i >= 2; i--)
                    {
                        string strNew = Val.ToString(dtbDetail.Columns[i]);
                        string str = strNew.Substring(strNew.LastIndexOf("_") + 1);
                        dtbDetail.Columns[Val.ToString(dtbDetail.Columns[i])].ColumnName = strNew.Split('_')[0] + "_" + strNew.Split('_')[1] + "_" + str;
                    }

                    if (dtbDetail.Rows.Count > 0)
                    {
                        for (int i = dtbDetail.Columns.Count - 2; i >= 2; i--)
                        {
                            if (Val.ToString(dtbDetail.Columns[i]) == Val.ToString(dtbDetail.Columns[i]).Split('_')[0] + "_" + Val.ToString(dtbDetail.Columns[i]).Split('_')[1] + "_carat")
                            {
                                foreach (DataRow Drw in dtbDetail.Rows)
                                {
                                    SrNo = objMFGMixSplit.GetSrNo(Val.ToInt(lueCutNo.EditValue), DLL.GlobalDec.EnumTran.Continue, Conn);
                                    objMFGMixSplitProperty.sr_no = Val.ToInt(SrNo + 1);
                                    objMFGMixSplitProperty.receive_date = Val.DBDate(dtpReceiveDate.Text);
                                    objMFGMixSplitProperty.from_lot_id = New_Lot_ID;
                                    objMFGMixSplitProperty.rough_cut_id = Val.ToInt64(lueCutNo.EditValue);
                                    objMFGMixSplitProperty.rough_sieve_id = Val.ToInt64(Val.ToString(dtbDetail.Columns[i]).Split('_')[1]);
                                    objMFGMixSplitProperty.quality_id = Val.ToInt64(Drw["quality_id"]);
                                    objMFGMixSplitProperty.kapan_id = Val.ToInt64(lueKapan.EditValue);
                                    objMFGMixSplitProperty.form_id = Val.ToInt(m_numForm_id);
                                    objMFGMixSplitProperty.manager_id = Val.ToInt(Mix_Data.Rows[0]["manager_id"]);
                                    objMFGMixSplitProperty.employee_id = Val.ToInt(Mix_Data.Rows[0]["employee_id"]);
                                    objMFGMixSplitProperty.process_id = Val.ToInt(Mix_Data.Rows[0]["process_id"]);
                                    objMFGMixSplitProperty.sub_process_id = Val.ToInt(Mix_Data.Rows[0]["sub_process_id"]);
                                    objMFGMixSplitProperty.rough_clarity_id = Val.ToInt64(Val.ToString(dtbDetail.Columns[i]).Split('_')[0]);
                                    objMFGMixSplitProperty.purity_id = Val.ToInt(0);
                                    objMFGMixSplitProperty.from_pcs = Val.ToInt(clmPcs.SummaryItem.SummaryValue);
                                    objMFGMixSplitProperty.from_carat = Val.ToDecimal(clmCarat.SummaryItem.SummaryValue);
                                    objMFGMixSplitProperty.pcs = Val.ToInt32(Drw[Val.ToString(objMFGMixSplitProperty.rough_clarity_id) + "_" + Val.ToString(objMFGMixSplitProperty.rough_sieve_id) + "_" + "pcs"]);
                                    objMFGMixSplitProperty.carat = Val.ToDecimal(Drw[Val.ToString(objMFGMixSplitProperty.rough_clarity_id) + "_" + Val.ToString(objMFGMixSplitProperty.rough_sieve_id) + "_" + "carat"]);
                                    objMFGMixSplitProperty.loss_carat = Val.ToDecimal(0);
                                    objMFGMixSplitProperty.plus_carat = Val.ToDecimal(0);
                                    objMFGMixSplitProperty.rate = Val.ToDecimal(txtRate.Text);
                                    objMFGMixSplitProperty.amount = Val.ToDecimal(objMFGMixSplitProperty.carat * objMFGMixSplitProperty.rate);
                                    objMFGMixSplitProperty.union_id = IntRes;
                                    objMFGMixSplitProperty.mix_union_id = MixSplit_IntRes;
                                    objMFGMixSplitProperty.receive_union_id = Receive_IntRes;
                                    objMFGMixSplitProperty.issue_union_id = Issue_IntRes;
                                    objMFGMixSplitProperty.history_union_id = NewHistory_Union_Id;
                                    objMFGMixSplitProperty.lot_srno = Lot_SrNo;
                                    objMFGMixSplitProperty.transaction_type = "MANY-TO-MANY";

                                    objMFGMixSplitProperty = objMFGMixSplit.Save_MFGMixSplit(objMFGMixSplitProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                                    IntRes = objMFGMixSplitProperty.union_id;
                                    Receive_IntRes = objMFGMixSplitProperty.receive_union_id;
                                    Issue_IntRes = objMFGMixSplitProperty.issue_union_id;
                                    NewHistory_Union_Id = Val.ToInt64(objMFGMixSplitProperty.history_union_id);
                                    Lot_SrNo = Val.ToInt64(objMFGMixSplitProperty.lot_srno);

                                    //objMFGProcessReceiveProperty.lot_id = Val.ToInt64(txtLotId.Text);
                                    //objMFGProcessReceiveProperty.balance_pcs = Val.ToInt(txtBalancePcs.Text);
                                    //objMFGProcessReceiveProperty.balance_carat = Val.ToDecimal(txtBalanceCarat.Text);
                                    //objMFGProcessReceiveProperty.receive_date = Val.DBDate(dtpReceiveDate.Text);
                                    //objMFGProcessReceiveProperty.rough_quality_id = Val.ToInt(Drw["quality_id"]);
                                    //objMFGProcessReceiveProperty.rough_cut_id = Val.ToInt(lueCutNo.EditValue);
                                    //objMFGProcessReceiveProperty.rough_clarity_id = Val.ToInt(Val.ToString(dtbDetail.Columns[i]).Split('_')[0]);
                                    //objMFGProcessReceiveProperty.rough_sieve_id = Val.ToInt(Val.ToString(dtbDetail.Columns[i]).Split('_')[1]);
                                    //objMFGProcessReceiveProperty.carat = Val.ToDecimal(Drw[Val.ToString(objMFGProcessReceiveProperty.rough_clarity_id) + "_" + Val.ToString(objMFGProcessReceiveProperty.rough_sieve_id) + "_" + "carat"]);
                                    //objMFGProcessReceiveProperty.rate = Val.ToDecimal(Drw[Val.ToString(objMFGProcessReceiveProperty.rough_clarity_id) + "_" + Val.ToString(objMFGProcessReceiveProperty.rough_sieve_id) + "_" + "rate"]);
                                    //objMFGProcessReceiveProperty.amount = Val.ToDecimal(Drw[Val.ToString(objMFGProcessReceiveProperty.rough_clarity_id) + "_" + Val.ToString(objMFGProcessReceiveProperty.rough_sieve_id) + "_" + "amount"]);
                                    //objMFGProcessReceiveProperty.union_id = IntRes;
                                    //objMFGProcessReceiveProperty.receive_union_id = Receive_IntRes;
                                    //objMFGProcessReceiveProperty.issue_union_id = Issue_IntRes;
                                    //objMFGProcessReceiveProperty.mix_union_id = MixSplit_IntRes;
                                    //objMFGProcessReceiveProperty.kapan_id = Val.ToInt(lueKapan.EditValue);
                                    //objMFGProcessReceiveProperty.form_id = m_numForm_id;
                                    //objMFGProcessReceiveProperty.history_union_id = NewHistory_Union_Id;

                                    //if (objMFGProcessReceiveProperty.carat == 0)
                                    //    continue;

                                    //objMFGProcessReceiveProperty = MFGProcessReceive.Save_Process_Receive_Split(objMFGProcessReceiveProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                                    //IntRes = objMFGProcessReceiveProperty.union_id;
                                    //Receive_IntRes = objMFGProcessReceiveProperty.receive_union_id;
                                    //Issue_IntRes = objMFGProcessReceiveProperty.issue_union_id;
                                    //MixSplit_IntRes = objMFGProcessReceiveProperty.mix_union_id;
                                    //NewHistory_Union_Id = Val.ToInt64(objMFGProcessReceiveProperty.history_union_id);
                                }
                            }
                        }
                    }
                    //int SrNo = 0;
                    //foreach (DataRow drw in m_dtbLotMixSplit.Rows)
                    //{
                    //    SrNo = objMFGMixSplit.GetSrNo(Val.ToInt(drw["rough_cut_id"]), DLL.GlobalDec.EnumTran.Continue, Conn);
                    //    objMFGMixSplitProperty.sr_no = Val.ToInt(SrNo + 1);
                    //    objMFGMixSplitProperty.receive_date = Val.DBDate(dtpReceiveDate.Text);
                    //    objMFGMixSplitProperty.from_lot_id = New_Lot_ID;
                    //    objMFGMixSplitProperty.rough_cut_id = Val.ToInt(drw["rough_cut_id"]);
                    //    objMFGMixSplitProperty.rough_sieve_id = Val.ToInt64(drw["rough_sieve_id"]);
                    //    objMFGMixSplitProperty.quality_id = Val.ToInt64(drw["quality_id"]);
                    //    objMFGMixSplitProperty.kapan_id = Val.ToInt64(drw["kapan_id"]);
                    //    objMFGMixSplitProperty.form_id = Val.ToInt(m_numForm_id);
                    //    objMFGMixSplitProperty.manager_id = Val.ToInt(Mix_Data.Rows[0]["manager_id"]);
                    //    objMFGMixSplitProperty.employee_id = Val.ToInt(Mix_Data.Rows[0]["employee_id"]);
                    //    objMFGMixSplitProperty.process_id = Val.ToInt(Mix_Data.Rows[0]["process_id"]);
                    //    objMFGMixSplitProperty.sub_process_id = Val.ToInt(Mix_Data.Rows[0]["sub_process_id"]);
                    //    objMFGMixSplitProperty.rough_clarity_id = Val.ToInt64(drw["rough_clarity_id"]);
                    //    objMFGMixSplitProperty.rough_sieve_id = Val.ToInt(drw["rough_sieve_id"]);
                    //    objMFGMixSplitProperty.purity_id = Val.ToInt(0);
                    //    objMFGMixSplitProperty.from_pcs = Val.ToInt(clmPcs.SummaryItem.SummaryValue);
                    //    objMFGMixSplitProperty.from_carat = Val.ToDecimal(clmCarat.SummaryItem.SummaryValue);
                    //    objMFGMixSplitProperty.pcs = Val.ToInt(drw["balance_pcs"]);
                    //    objMFGMixSplitProperty.carat = Val.ToDecimal(drw["balance_carat"]);
                    //    objMFGMixSplitProperty.loss_carat = Val.ToDecimal(0);
                    //    objMFGMixSplitProperty.plus_carat = Val.ToDecimal(0);
                    //    objMFGMixSplitProperty.rate = Val.ToDecimal(drw["rate"]);
                    //    objMFGMixSplitProperty.amount = Val.ToDecimal(drw["amount"]);
                    //    objMFGMixSplitProperty.union_id = IntRes;
                    //    objMFGMixSplitProperty.mix_union_id = MixSplit_IntRes;
                    //    objMFGMixSplitProperty.receive_union_id = Receive_IntRes;
                    //    objMFGMixSplitProperty.issue_union_id = Issue_IntRes;
                    //    objMFGMixSplitProperty.history_union_id = NewHistory_Union_Id;

                    //    objMFGMixSplitProperty = objMFGMixSplit.Save_MFGMixSplit(objMFGMixSplitProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                    //    IntRes = objMFGMixSplitProperty.union_id;
                    //    Receive_IntRes = objMFGMixSplitProperty.receive_union_id;
                    //    Issue_IntRes = objMFGMixSplitProperty.issue_union_id;
                    //    NewHistory_Union_Id = Val.ToInt64(objMFGMixSplitProperty.history_union_id);
                    //    drwNew = dtBarcodePrint.NewRow();
                    //    drwNew["lot_id"] = Val.ToInt(objMFGMixSplitProperty.lot_id);
                    //    drwNew["sr_no"] = Val.ToInt(SrNo + 1);
                    //    drwNew["receive_date"] = Val.DBDate(dtpReceiveDate.Text);
                    //    drwNew["kapan_no"] = Val.ToString(drw["kapan_no"]);
                    //    drwNew["pcs"] = Val.ToInt(drw["balance_pcs"]);
                    //    drwNew["carat"] = Val.ToDecimal(drw["balance_carat"]);
                    //    dtBarcodePrint.Rows.Add(drwNew);
                    //}
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

        private void backgroundWorker_LSAssortFinal_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (IntRes > 0)
                {
                    Global.Confirm("Lot Split Recieve Data Save Succesfully");
                    ClearDetails();
                    lueType.EditValue = "BOTH";
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

        private void grdDet_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                if (Global.Confirm("Are you sure delete selected row?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    dgvDet.DeleteRow(dgvDet.GetRowHandle(dgvDet.FocusedRowHandle));

                    txtPcs.Text = Val.ToInt(clmPcs.SummaryItem.SummaryValue).ToString();
                    txtCarat.Text = Val.Val(clmCarat.SummaryItem.SummaryValue).ToString();
                    txtAmount.Text = Val.Val(clmAmount.SummaryItem.SummaryValue).ToString();
                    decimal Carat = Val.ToDecimal(clmCarat.SummaryItem.SummaryValue);
                    if (Carat > 0)
                    {
                        m_numSummRate = Math.Round((Val.ToDecimal(clmAmount.SummaryItem.SummaryValue) / Val.ToDecimal(clmCarat.SummaryItem.SummaryValue)), 2, MidpointRounding.AwayFromZero);
                        txtRate.Text = m_numSummRate.ToString();
                    }
                    else
                    {
                        txtRate.Text = m_numSummRate.ToString();
                    }
                }
            }
        }

        private void FrmMFGLSAssortFinal_Load(object sender, EventArgs e)
        {
            try
            {
                dtpReceiveDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpReceiveDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpReceiveDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpReceiveDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpReceiveDate.EditValue = DateTime.Now;

                m_dtbKapan = Global.GetKapanAll();

                lueKapan.Properties.DataSource = m_dtbKapan;
                lueKapan.Properties.ValueMember = "kapan_id";
                lueKapan.Properties.DisplayMember = "kapan_no";

                lueMixKapan.Properties.DataSource = m_dtbKapan;
                lueMixKapan.Properties.ValueMember = "kapan_id";
                lueMixKapan.Properties.DisplayMember = "kapan_no";

                m_dtbParam = Global.GetRoughCutAll();

                lueCutNo.Properties.DataSource = m_dtbParam;
                lueCutNo.Properties.ValueMember = "rough_cut_id";
                lueCutNo.Properties.DisplayMember = "rough_cut_no";

                lueMixCutNo.Properties.DataSource = m_dtbParam;
                lueMixCutNo.Properties.ValueMember = "rough_cut_id";
                lueMixCutNo.Properties.DisplayMember = "rough_cut_no";

                m_dtbType = new DataTable();
                m_dtbType.Columns.Add("type");
                m_dtbType.Rows.Add("BOTH");
                m_dtbType.Rows.Add("NORMAL");
                m_dtbType.Rows.Add("REJECTION");

                lueType.Properties.DataSource = m_dtbType;
                lueType.Properties.ValueMember = "type";
                lueType.Properties.DisplayMember = "type";
                lueType.EditValue = "BOTH";

                Global.LOOKUPRoughSieve(lueSieve);
                Global.LOOKUPRoughQuality(lueQuality);

                // Add By Praful On 29072021

                //DTab_KapanWiseData = Global.GetRoughStockWise(Val.ToInt(0), Val.ToInt32(0));

                // End By Praful On 29072021

                ClearDetails();
                txtLotID.Focus();
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }

        private void lueSieve_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmMfgRoughSieve frmRoughSieve = new FrmMfgRoughSieve();
                frmRoughSieve.ShowDialog();
                Global.LOOKUPRoughSieve(lueSieve);
            }
        }

        private void lueClarity_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmMfgRoughClarityMaster frmClarity = new FrmMfgRoughClarityMaster();
                frmClarity.ShowDialog();
                Global.LOOKUPRoughClarity(lueClarity);
            }
        }

        private void lueQuality_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmMfgQualityMaster frmRoughQuality = new FrmMfgQualityMaster();
                frmRoughQuality.ShowDialog();
                Global.LOOKUPRoughQuality(lueQuality);
            }
        }

        private void txtRate_EditValueChanged(object sender, EventArgs e)
        {
            if (lueQuality.Text == "LOSS")
            {
                txtAmount.Text = (Val.ToString(Math.Round((Val.ToDecimal(txtCarat.Text)) * Val.ToDecimal(txtRate.Text), 3)));
            }
            else
            {
                txtAmount.Text = (Val.ToString(Math.Round((Val.ToDecimal(txtCarat.Text)) * Val.ToDecimal(txtRate.Text), 0)));
            }
        }

        private void txtWeightPlus_EditValueChanged(object sender, EventArgs e)
        {
            if (lueQuality.Text == "LOSS")
            {
                txtAmount.Text = (Val.ToString(Math.Round((Val.ToDecimal(txtCarat.Text)) * Val.ToDecimal(txtRate.Text), 3)));
            }
            else
            {
                txtAmount.Text = (Val.ToString(Math.Round((Val.ToDecimal(txtCarat.Text)) * Val.ToDecimal(txtRate.Text), 0)));
            }
        }

        private void dgvMixSplit_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                DataTable dtAmount = new DataTable();
                //dtAmount = (DataTable)grdMixSplit.DataSource;

                Int32 pcs = 0;
                decimal rate = 0;
                decimal carat = 0;
                decimal amount = 0;

                string column = "";
                for (int j = 0; j <= dtAmount.Columns.Count - 1; j++)
                {
                    if (dtAmount.Columns[j].ToString().Contains("pcs"))
                    {
                        pcs = dtAmount.AsEnumerable().Sum(x => Val.ToInt32(x[dtAmount.Columns[j]]));
                    }
                    if (dtAmount.Columns[j].ToString().Contains("balance_carat"))
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

        private void txtLotID_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
            {
                IDataObject clipData = Clipboard.GetDataObject();
                String Data = Val.ToString(clipData.GetData(System.Windows.Forms.DataFormats.Text));
                String str1 = Data.Replace("\r\n", ",");                   //data.Replace(\n, ",");
                str1 = str1.Trim();
                str1 = str1.TrimEnd();
                str1 = str1.TrimStart();
                str1 = str1.TrimEnd(',');
                str1 = str1.TrimStart(',');
                txtLotID.Text = str1;
            }
        }

        private void txtLotID_MouseDown(object sender, MouseEventArgs e)
        {
            if (txtLotID.Focus())
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    PasteData = Val.ToString(PasteclipData.GetData(System.Windows.Forms.DataFormats.Text));
                }
            }
        }
        private void btnPopUpStock_Click(object sender, EventArgs e)
        {
            GetPendingStock();
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

                DataTable dtTemp = new DataTable();
                dtTemp = (DataTable)grdLSAssortFinalSplit.DataSource;
                List<ListError> lstError = new List<ListError>();
                if (dtTemp == null)
                {
                    Global.Message("Atleast 1 record must be enter in grid");
                    btnSave.Enabled = true;
                    return;
                }

                m_blnsave = true;
                m_blnadd = false;
                if (!ValidateDetails())
                {
                    m_blnsave = false;
                    btnSave.Enabled = true;
                    return;
                }
                DialogResult result = MessageBox.Show("Do you want to save LS Assort Final data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result != DialogResult.Yes)
                {
                    btnSave.Enabled = true;
                    return;
                }
                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                backgroundWorker_LSAssortFinal.RunWorkerAsync();
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
            Global.Export("xlsx", dgvDet);
        }

        private void lueKapan_EditValueChanged(object sender, EventArgs e)
        {
            m_dtbParam = new DataTable();
            if (lueKapan.Text.ToString() != "")
            {
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

        private void txtLotID_Validated(object sender, EventArgs e)
        {
            if (Save_Validate())
            {
                try
                {
                    DTab_StockData.AcceptChanges();
                    if (DTab_StockData != null)
                    {
                        if (DTab_StockData.Rows.Count > 0)
                        {
                            DataRow[] dr = DTab_StockData.Select("lot_id = " + Val.ToInt64(txtLotID.Text));

                            if (dr.Length > 0)
                            {
                                Global.Message("Lot ID already added to the Issue list!");
                                txtLotID.Text = "";
                                txtLotID.Focus();
                                return;
                            }

                            //for (int i = 0; i < DTab_StockData.Rows.Count; i++)
                            //{
                            //    if (DTab_StockData.Rows[i]["lot_id"].ToString() == txtLotID.Text)
                            //    {
                            //        Global.Message("Lot ID already added to the Issue list!");
                            //        txtLotID.Text = "";
                            //        txtLotID.Focus();
                            //        return;
                            //    }
                            //}
                        }
                    }

                    if (txtLotID.Text.Length == 0)
                    {
                        return;
                    }
                    MFGProcessIssue objMFGProcessIssue = new MFGProcessIssue();
                    MFGProcessIssueProperty objMFGProcessIssueProperty = new MFGProcessIssueProperty();
                    objMFGProcessIssueProperty.rough_cut_id = Val.ToInt(lueMixCutNo.EditValue);
                    objMFGProcessIssueProperty.kapan_id = Val.ToInt(lueMixKapan.EditValue);

                    if (DTab_StockData.Rows.Count > 0)
                    {
                        DataTable DTabTemp = new DataTable();

                        DataTable DTab_ValidateBarcode = objMFGProcessIssue.GetPendingIssueStock(objMFGProcessIssueProperty);

                        if (DTab_ValidateBarcode.Rows.Count > 0)
                        {
                            for (int i = 0; i < DTab_ValidateBarcode.Rows.Count; i++)
                            {
                                if (Process_Id != Val.ToInt(DTab_ValidateBarcode.Rows[i]["process_id"]))
                                {
                                    Global.Message("Difference Process Name in this lot ID =" + Val.ToInt(DTab_ValidateBarcode.Rows[i]["lot_id"]));
                                    txtLotID.Text = "";
                                    txtLotID.Focus();
                                    return;
                                }
                                else
                                {
                                    int Process_Id_New = Val.ToInt(DTab_ValidateBarcode.Rows[i]["process_id"]);
                                    Sub_Process_Id = Val.ToInt(DTab_ValidateBarcode.Rows[i]["sub_process_id"]);
                                    dtIss = objProcessRecieve.GetIssueData(Val.ToString(DTab_ValidateBarcode.Rows[i]["lot_id"]), Val.ToInt32(Process_Id_New), Val.ToInt32(Sub_Process_Id));
                                    if (dtIss.Rows.Count > 0)
                                    {
                                    }
                                    else
                                    {
                                        Global.Message("Process Not Issue in this Lot ID = " + Val.ToString(DTab_ValidateBarcode.Rows[i]["lot_id"]));
                                        txtLotID.Text = "";
                                        txtLotID.Focus();
                                        return;
                                    }
                                }
                            }
                        }
                        else
                        {
                            Global.Message("Lot ID Not found");
                            txtLotID.Text = "";
                            txtLotID.Focus();
                            return;
                        }

                        DTabTemp = objMFGProcessIssue.GetPendingIssueStock(objMFGProcessIssueProperty);

                        if (DTabTemp.Rows.Count > 0)
                        {
                            txtLotID.Text = "";
                            txtLotID.Focus();
                        }

                        DTab_StockData.Merge(DTabTemp);
                    }
                    else
                    {
                        DataTable DTab_ValidateBarcode = objMFGProcessIssue.GetPendingIssueStock(objMFGProcessIssueProperty);

                        if (DTab_ValidateBarcode.Rows.Count > 0)
                        {
                            for (int i = 0; i < DTab_ValidateBarcode.Rows.Count; i++)
                            {
                                Process_Id = Val.ToInt(DTab_ValidateBarcode.Rows[i]["process_id"]);
                                Sub_Process_Id = Val.ToInt(DTab_ValidateBarcode.Rows[i]["sub_process_id"]);

                                dtIss = objProcessRecieve.GetIssueData(Val.ToString(DTab_ValidateBarcode.Rows[i]["lot_id"]), Val.ToInt32(Process_Id), Val.ToInt32(Sub_Process_Id));
                                if (dtIss.Rows.Count > 0)
                                {
                                }
                                else
                                {
                                    Global.Message("Process Not Issue in this Lot ID = " + Val.ToString(DTab_ValidateBarcode.Rows[i]["lot_id"]));
                                    txtLotID.Text = "";
                                    txtLotID.Focus();
                                    return;
                                }
                            }
                        }
                        else
                        {
                            Global.Message("Lot ID Not found");
                            txtLotID.Text = "";
                            txtLotID.Focus();
                            return;
                        }

                        DTab_StockData = objMFGProcessIssue.GetPendingIssueStock(objMFGProcessIssueProperty);

                        if (DTab_StockData.Rows.Count > 0)
                        {
                            lueKapan.EditValue = Val.ToInt64(DTab_StockData.Rows[0]["kapan_id"]);
                            lueCutNo.EditValue = Val.ToInt64(DTab_StockData.Rows[0]["rough_cut_id"]);

                            txtLotID.Text = "";
                            txtLotID.Focus();
                        }
                    }

                    grdDet.DataSource = DTab_StockData;
                    grdDet.RefreshDataSource();
                    dgvDet.BestFitColumns();
                    CalculateSummary();
                }
                catch (Exception ex)
                {
                    BLL.General.ShowErrors(ex);
                    return;
                }
            }
        }

        private void dgvLSAssortFinalSplit_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                DataTable dtAmount = new DataTable();
                dtAmount = (DataTable)grdLSAssortFinalSplit.DataSource;
                string[] col = e.Column.FieldName.Split('_');
                string currcol = "";
                if (col.Length > 4 && e.Column.FieldName.Contains("_") && col != null)
                {
                    currcol = col[0] + "_" + col[1] + "_" + col[2] + "_" + col[3];
                }
                Int32 pcs = 0;
                decimal carat = 0;
                decimal total_pcs = 0;
                decimal total = 0;
                for (int i = 0; i <= dtAmount.Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= dtAmount.Columns.Count - 1; j++)
                    {
                        if (dtAmount.Columns[j].ToString().Contains("pcs") && dtAmount.Columns[j].ColumnName.Contains(currcol))
                        {
                            pcs = Val.ToInt32(dtAmount.Rows[i][j]);
                            total_pcs += pcs;
                        }
                        if (dtAmount.Columns[j].ToString().Contains("carat") && dtAmount.Columns[j].ColumnName.Contains(currcol))
                        {
                            carat = Val.ToDecimal(dtAmount.Rows[i][j]);
                            total += carat;
                        }
                        if (dtAmount.Columns[j].ColumnName.Contains("T_Pcs"))
                        {
                            dtAmount.Rows[i][j] = Val.ToInt32(total_pcs);
                        }
                        if (dtAmount.Columns[j].ColumnName.Contains("Total"))
                        {
                            dtAmount.Rows[i][j] = Math.Round(total, 3).ToString();
                        }
                    }
                    total_pcs = 0;
                    total = 0;
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }

        private void dgvLSAssortFinalSplit_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            try
            {
                DataTable dtAmount = new DataTable();
                dtAmount = (DataTable)grdLSAssortFinalSplit.DataSource;

                Int32 pcs = 0;
                decimal carat = 0;
                Int32 totpcs = 0;
                decimal totcarat = 0;

                for (int j = 0; j <= dtAmount.Columns.Count - 1; j++)
                {
                    if (dtAmount.Columns[j].ToString().Contains("pcs"))
                    {
                        pcs = dtAmount.AsEnumerable().Sum(x => Val.ToInt32(x[dtAmount.Columns[j]]));
                    }
                    if (dtAmount.Columns[j].ToString().Contains("carat"))
                    {
                        carat = dtAmount.AsEnumerable().Sum(x => Val.ToDecimal(x[dtAmount.Columns[j]]));
                    }
                    if (dtAmount.Columns[j].ColumnName.Contains("T_Pcs"))
                    {
                        totpcs = dtAmount.AsEnumerable().Sum(x => Val.ToInt32(x[dtAmount.Columns[j]]));
                    }
                    if (dtAmount.Columns[j].ColumnName.Contains("Total"))
                    {
                        totcarat = dtAmount.AsEnumerable().Sum(x => Val.ToDecimal(x[dtAmount.Columns[j]]));
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }

        private void dgvLSAssortFinalSplit_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;

                if (view.FocusedColumn.FieldName.Contains("carat"))
                {
                    double carat = 0.000;
                    if (!double.TryParse(e.Value as string, out carat))
                    {
                        e.Valid = false;
                        e.ErrorText = "Input string was not in a correct format.";
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }

        private void grdLSAssortFinalSplit_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    (grdLSAssortFinalSplit.FocusedView as ColumnView).FocusedRowHandle++;
                    e.Handled = true;
                }
            }
            catch
            {
            }
        }
        private void dgvDet_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                if (Val.ToDecimal(clmAmount.SummaryItem.SummaryValue) > 0 && Val.ToDecimal(clmCarat.SummaryItem.SummaryValue) > 0)
                {
                    m_numSummRate = Math.Round((Val.ToDecimal(clmAmount.SummaryItem.SummaryValue) / Val.ToDecimal(clmCarat.SummaryItem.SummaryValue)), 2, MidpointRounding.AwayFromZero);
                    txtRate.Text = m_numSummRate.ToString();
                    txtAmount.Text = Val.Val(clmAmount.SummaryItem.SummaryValue).ToString();
                    txtCarat.Text = Val.Val(clmCarat.SummaryItem.SummaryValue).ToString();
                    txtPcs.Text = Val.ToInt(clmPcs.SummaryItem.SummaryValue).ToString();
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

        public void GetStockData(DataTable Stock_Data)
        {
            try
            {
                DTabTemp = Stock_Data.Copy();
                DTab_StockData.AcceptChanges();
                if (DTab_StockData != null)
                {
                    if (DTab_StockData.Rows.Count > 0)
                    {
                        for (int i = 0; i < DTab_StockData.Rows.Count; i++)
                        {
                            for (int j = 0; j < DTabTemp.Rows.Count; j++)
                            {
                                if (DTab_StockData.Rows[i]["lot_id"].ToString() == DTabTemp.Rows[j]["lot_id"].ToString())
                                {
                                    Global.Message(DTab_StockData.Rows[i]["lot_id"].ToString() + " = Lot ID already added to the Issue list!");
                                    txtLotID.Text = "";
                                    txtLotID.Focus();
                                    return;
                                }
                            }
                        }
                    }
                }

                //DTab_StockData = Stock_Data.Copy();

                if (DTab_StockData.Rows.Count > 0)
                {
                    if (DTabTemp.Rows.Count > 0)
                    {
                        Process_Id = Val.ToInt(DTabTemp.Rows[0]["process_id"]);
                        for (int i = 0; i < DTabTemp.Rows.Count; i++)
                        {
                            if (Process_Id != Val.ToInt(DTabTemp.Rows[i]["process_id"]))
                            {
                                Global.Message("Difference Process Name in this lot ID =" + DTabTemp.Rows[i]["lot_id"].ToString());
                                txtLotID.Text = "";
                                txtLotID.Focus();
                                return;
                            }
                            else
                            {
                                int Process_Id_New = Val.ToInt(DTabTemp.Rows[i]["process_id"]);
                                Sub_Process_Id = Val.ToInt(DTabTemp.Rows[i]["sub_process_id"]);
                                dtIss = objProcessRecieve.GetIssueData(Val.ToString(DTabTemp.Rows[i]["lot_id"]), Val.ToInt32(Process_Id_New), Val.ToInt32(Sub_Process_Id));
                                if (dtIss.Rows.Count > 0)
                                {
                                    //Global.Message("Lot is already issue in this process.");
                                }
                                else
                                {
                                    Global.Message("Lot is not issue in this process.");
                                    txtLotID.Text = "";
                                    txtLotID.Focus();
                                    return;
                                }
                            }
                        }

                        lueKapan.EditValue = Val.ToInt64(DTabTemp.Rows[0]["kapan_id"]);
                        lueCutNo.EditValue = Val.ToInt64(DTabTemp.Rows[0]["rough_cut_id"]);
                    }
                    DTab_StockData.Merge(DTabTemp);
                }
                else
                {
                    DataTable DTab_ValidateBarcode = new DataTable();
                    if (txtLotID.Text == "")
                    {
                        DTab_ValidateBarcode = DTabTemp.Copy();

                        for (int i = 0; i < DTab_ValidateBarcode.Rows.Count; i++)
                        {
                            Process_Id = Val.ToInt(DTab_ValidateBarcode.Rows[0]["process_id"]);
                            if (Process_Id != Val.ToInt(DTab_ValidateBarcode.Rows[i]["process_id"]))
                            {
                                Global.Message("Difference Process Name in this lot ID =" + DTab_ValidateBarcode.Rows[i]["lot_id"].ToString());
                                txtLotID.Text = "";
                                txtLotID.Focus();
                                return;
                            }
                            else
                            {
                                int Process_Id_New = Val.ToInt(DTab_ValidateBarcode.Rows[i]["process_id"]);
                                Sub_Process_Id = Val.ToInt(DTab_ValidateBarcode.Rows[i]["sub_process_id"]);
                                dtIss = objProcessRecieve.GetIssueData(Val.ToString(DTab_ValidateBarcode.Rows[i]["lot_id"]), Val.ToInt32(Process_Id_New), Val.ToInt32(Sub_Process_Id));
                                if (dtIss.Rows.Count > 0)
                                {
                                    //Global.Message("Lot is already issue in this process.");
                                }
                                else
                                {
                                    Global.Message("Lot is not issue in this process.");
                                    txtLotID.Text = "";
                                    txtLotID.Focus();
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        DTab_ValidateBarcode = objMFGMixSplit.Live_Stock_GetData(Val.Trim(txtLotID.Text));

                        for (int i = 0; i < DTab_ValidateBarcode.Rows.Count; i++)
                        {
                            Process_Id = Val.ToInt(DTab_ValidateBarcode.Rows[0]["process_id"]);
                            if (Process_Id != Val.ToInt(DTab_ValidateBarcode.Rows[i]["process_id"]))
                            {
                                Global.Message("Difference Process Name in this lot ID =" + DTab_ValidateBarcode.Rows[i]["lot_id"].ToString());
                                txtLotID.Text = "";
                                txtLotID.Focus();
                                return;
                            }
                            else
                            {
                                int Process_Id_New = Val.ToInt(DTab_ValidateBarcode.Rows[i]["process_id"]);
                                Sub_Process_Id = Val.ToInt(DTab_ValidateBarcode.Rows[i]["sub_process_id"]);
                                dtIss = objProcessRecieve.GetIssueData(Val.ToString(DTab_ValidateBarcode.Rows[i]["lot_id"]), Val.ToInt32(Process_Id_New), Val.ToInt32(Sub_Process_Id));
                                if (dtIss.Rows.Count > 0)
                                {
                                    //Global.Message("Lot is already issue in this process.");
                                }
                                else
                                {
                                    Global.Message("Lot is not issue in this process.");
                                    txtLotID.Text = "";
                                    txtLotID.Focus();
                                    return;
                                }
                            }
                        }
                    }

                    if (DTab_ValidateBarcode.Rows.Count > 0)
                    {
                    }
                    else
                    {
                        Global.Message("Lot ID Not found");
                        txtLotID.Text = "";
                        txtLotID.Focus();
                        return;
                    }
                    if (txtLotID.Text == "")
                    {
                        DTab_StockData = DTabTemp.Copy();
                    }
                    else
                    {
                        DTab_StockData = objMFGMixSplit.Live_Stock_GetData(Val.Trim(txtLotID.Text));
                    }


                    if (DTab_StockData.Rows.Count > 0)
                    {
                        lueKapan.EditValue = Val.ToInt64(DTab_StockData.Rows[0]["kapan_id"]);
                        lueCutNo.EditValue = Val.ToInt64(DTab_StockData.Rows[0]["rough_cut_id"]);

                        txtLotID.Text = "";
                        txtLotID.Focus();
                    }
                }

                grdDet.DataSource = DTab_StockData;
                grdDet.RefreshDataSource();
                dgvDet.BestFitColumns();
                CalculateSummary();
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
            }
        }

        private bool Save_Validate()
        {
            bool blnReturn = true;

            try
            {
                m_blnadd = true;
                m_blnsave = false;
                if (!Validate_PopUp())
                {
                    m_blnadd = false;
                    blnReturn = false;
                    return blnReturn;
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
        }

        private bool Validate_PopUp()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (lueMixKapan.Text == "")
                {
                    lstError.Add(new ListError(13, "Kapan No"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueMixKapan.Focus();
                    }
                }
                if (lueMixCutNo.Text == "")
                {
                    lstError.Add(new ListError(13, "Cut No"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueMixCutNo.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                lstError.Add(new ListError(ex));
            }
            return (!(BLL.General.ShowErrors(lstError)));
        }

        public void GetPendingStock()
        {
            try
            {
                if (Save_Validate())
                {
                    MFGProcessIssue objMFGProcessIssue = new MFGProcessIssue();
                    MFGProcessIssueProperty objMFGProcessIssueProperty = new MFGProcessIssueProperty();
                    objMFGProcessIssueProperty.rough_cut_id = Val.ToInt(lueMixCutNo.EditValue);
                    objMFGProcessIssueProperty.kapan_id = Val.ToInt(lueMixKapan.EditValue);

                    DtPending = objMFGProcessIssue.GetPendingIssueStock(objMFGProcessIssueProperty);

                    FrmMFGStockConfirm FrmStockConfirm = new FrmMFGStockConfirm();
                    FrmStockConfirm.FrmMFGLSAssortFinal = this;
                    FrmStockConfirm.DTab = DtPending;
                    FrmStockConfirm.ShowForm(this);
                }
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
                dtpReceiveDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpReceiveDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpReceiveDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpReceiveDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpReceiveDate.EditValue = DateTime.Now;
                lueKapan.EditValue = System.DBNull.Value;
                lueCutNo.EditValue = System.DBNull.Value;
                lueMixKapan.EditValue = System.DBNull.Value;
                lueMixCutNo.EditValue = System.DBNull.Value;

                for (int i = 0; i < lueClarity.Properties.Items.Count; i++)
                    lueClarity.Properties.Items[i].CheckState = CheckState.Unchecked;

                for (int i = 0; i < lueSieve.Properties.Items.Count; i++)
                    lueSieve.Properties.Items[i].CheckState = CheckState.Unchecked;

                for (int i = 0; i < lueQuality.Properties.Items.Count; i++)
                    lueQuality.Properties.Items[i].CheckState = CheckState.Unchecked;

                txtPcs.Text = string.Empty;
                txtCarat.Text = string.Empty;
                txtRate.Text = string.Empty;
                txtAmount.Text = string.Empty;
                btnPopUpStock.Enabled = false;
                DTab_StockData.Rows.Clear();
                DTab_StockData.AcceptChanges();
                txtLotID.Text = string.Empty;
                grdDet.DataSource = null;
                grdDet.RefreshDataSource();
                grdLSAssortFinalSplit.DataSource = null;
                dgvLSAssortFinalSplit.Columns.Clear();

                lueMixKapan.Focus();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
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
                            dgvLSAssortFinalSplit.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvLSAssortFinalSplit.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvLSAssortFinalSplit.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvLSAssortFinalSplit.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvLSAssortFinalSplit.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvLSAssortFinalSplit.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvLSAssortFinalSplit.ExportToCsv(Filepath);
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
        public void FillGrid(int LotSrno)
        {
            try
            {
                if (Val.ToInt32(LotSrno) <= 0)
                {
                    return;
                }
                btnDelete.Tag = Val.ToInt32(LotSrno);
                DataTable dtbDetails_old = (DataTable)grdLSAssortFinalSplit.DataSource;

                this.Cursor = Cursors.WaitCursor;

                dgvLSAssortFinalSplit.Columns.Clear();
                pivot pt = new pivot(objProcessReceive.Process_LS_AssortFinal_GetData(LotSrno));
                //DataTable dtbDetails = pt.PivotDataSuperPlusAssortment(new string[] { "quality_id", "quality" }, new string[] { "carat" }, new AggregateFunction[] { AggregateFunction.Sum, AggregateFunction.Sum, AggregateFunction.Sum }, new string[] { "sieve_name" });
                DataTable dtbDetails = pt.PivotDataSuperPlus(new string[] { "quality_id", "quality" }, new string[] { "carat" }, new AggregateFunction[] { AggregateFunction.Sum, AggregateFunction.Sum, AggregateFunction.Sum }, new string[] { "sieve_name" });

                DataTable DTab_Clarity = objRoughClarity.GetData();
                DataTable DTab_Sieve = objRoughSieve.GetData();
                DataTable Merge_Data = new DataTable();
                Merge_Data.Columns.Add("Clarity_Sieve_Carat", typeof(string));
                //Merge_Data.Columns.Add("Clarity_Sieve_Rate", typeof(string));
                //Merge_Data.Columns.Add("Clarity_Sieve_Amount", typeof(string));

                for (int i = 0; i < DTab_Clarity.Rows.Count; i++)
                {
                    for (int j = 0; j < DTab_Sieve.Rows.Count; j++)
                    {
                        Merge_Data.Rows.Add(DTab_Clarity.Rows[i]["rough_clarity_id"] + "_" + DTab_Sieve.Rows[j]["rough_sieve_id"] + "_" + DTab_Clarity.Rows[i]["rough_clarity_name"] + DTab_Sieve.Rows[j]["sieve_name"] + "_carat");
                    }
                }


                DataColumn Total = new System.Data.DataColumn("Total", typeof(System.Decimal));
                //DataColumn Rate = new System.Data.DataColumn("Rate", typeof(System.Decimal));
                //DataColumn Amount = new System.Data.DataColumn("Amount", typeof(System.Decimal));
                Total.DefaultValue = "0.000";
                //Rate.DefaultValue = "0.000";
                //Amount.DefaultValue = "0.000";
                dtbDetails.Columns.Add(Total);
                //dtbDetails.Columns.Add(Rate);
                //dtbDetails.Columns.Add(Amount);

                DataColumnCollection columns = dtbDetails.Columns;
                if (dtbDetails_old != null)
                {
                    if (dtbDetails_old.Columns.Count > 0)
                    {
                        for (int i = 0; i < dtbDetails_old.Rows.Count; i++)
                        {
                            for (int j = 0; j < dtbDetails_old.Columns.Count; j++)
                            {
                                for (int k = 0; k < dtbDetails.Rows.Count; k++)
                                {
                                    if (dtbDetails.Rows[k]["quality"].ToString().Trim() == dtbDetails_old.Rows[i]["quality"].ToString().Trim())
                                    {
                                        if (columns.Contains(dtbDetails_old.Columns[j].ColumnName.ToString().Trim()))
                                        {
                                            dtbDetails.Rows[k][dtbDetails_old.Columns[j].ColumnName.ToString().Trim()] = dtbDetails_old.Rows[i][dtbDetails_old.Columns[j].ColumnName.ToString().Trim()];
                                        }
                                    }
                                }

                            }
                        }
                    }
                }

                dtTemp = dtbDetails.Copy();
                grdLSAssortFinalSplit.DataSource = dtTemp;

                dgvLSAssortFinalSplit.Columns["quality_id"].Visible = false;
                dgvLSAssortFinalSplit.Columns["quality"].OptionsColumn.ReadOnly = true;
                dgvLSAssortFinalSplit.Columns["quality"].OptionsColumn.AllowFocus = false;
                dgvLSAssortFinalSplit.Columns["quality"].Fixed = FixedStyle.Left;
                dgvLSAssortFinalSplit.Columns["Total"].OptionsColumn.ReadOnly = true;
                dgvLSAssortFinalSplit.Columns["Total"].OptionsColumn.AllowFocus = false;

                for (int i = 0; i <= dtTemp.Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= dtTemp.Columns.Count - 1; j++)
                    {
                        if (dtTemp.Columns[j].ToString().Contains("carat"))
                        {
                            string carat = dtTemp.Columns[j].ToString();
                            GridColumn column1 = dgvLSAssortFinalSplit.Columns[carat];
                            dgvLSAssortFinalSplit.Columns[carat].SummaryItem.DisplayFormat = "{0:n3}";
                            column1.SummaryItem.SummaryType = SummaryItemType.Sum;
                        }


                        if (dtTemp.Columns[j].ColumnName.Contains("Total"))
                        {
                            string total = dtTemp.Columns[j].ToString();
                            GridColumn column4 = dgvLSAssortFinalSplit.Columns[total];
                            dgvLSAssortFinalSplit.Columns[total].SummaryItem.DisplayFormat = "{0:n3}";
                            column4.SummaryItem.SummaryType = SummaryItemType.Sum;
                        }

                    }
                    break;
                }

                dgvLSAssortFinalSplit.OptionsView.ShowFooter = true;
                dgvLSAssortFinalSplit.BestFitColumns();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
            }
        }
        private void CalculateSummary()
        {
            dgvDet.PostEditor();
            dgvDet.RefreshData();
            DTab_StockData.AcceptChanges();

            foreach (DataRow DRow in DTab_StockData.Rows)
            {
                // Total Summary Details
                IntTotalLot++;
                // Comment By Praful On 11102017

                IntTotalPcs += Val.ToInt(DRow["pcs"]);
                DblTotalCarat += Val.Val(DRow["carat"]);
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

        private void btnSearchData_Click(object sender, EventArgs e)
        {
            FrmMFGSearchProcess FrmSearchProcess = new FrmMFGSearchProcess();
            FrmSearchProcess.FrmMFGLSAssortFinal = this;
            //FrmSearchProcess.DTab = DtPending;
            FrmSearchProcess.ShowForm(this);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to Delete data?", "Confirmation", MessageBoxButtons.YesNoCancel);
            if (result != DialogResult.Yes)
            {
                return;
            }

            try
            {
                if (Val.ToInt32(btnDelete.Tag) > 0)
                {
                    objMFGMixSplit = new MFGMixSplit();
                    ObjPer.SetFormPer();
                    if (ObjPer.AllowDelete == false)
                    {
                        Global.Message(BLL.GlobalDec.gStrPermissionDelMsg);
                        return;
                    }
                    btnDelete.Enabled = false;
                    int count = 0;

                    count = objMFGMixSplit.CheckLsAssort(Val.ToInt32(btnDelete.Tag));
                    if (count == 0)
                    {



                        if (DeleteDetail())
                        {
                            ClearDetails();
                        }
                    }
                    else
                    {
                        Global.Message("This Process is not last Process");
                        btnDelete.Enabled = true;
                        return;
                    }
                    btnDelete.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        private bool DeleteDetail()
        {
            bool blnReturn = true;
            MFGLotSplit objMFGLotSplit = new MFGLotSplit();
            MFGMixSplitProperty objMFGMixSplitProperty = new MFGMixSplitProperty();

            try
            {
                //if (Val.ToString(lblMode.Tag) != "")
                //{

                Conn = new BeginTranConnection(true, false);

                objMFGMixSplitProperty.lot_srno = Val.ToInt32(btnDelete.Tag);
                DataTable DeleteData = (DataTable)grdDet.DataSource;
                int IntRes = objMFGMixSplit.Delete_LsAssort(objMFGMixSplitProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                Conn.Inter1.Commit();

                if (IntRes == -1)
                {
                    Global.Confirm("Error In Deleted Data");
                    IntRes = -1;
                    Conn.Inter1.Rollback();
                    Conn = null;
                    return blnReturn;
                    //txtPartyInvoiceNo.Focus();
                }
                else
                {
                    Global.Confirm("Deleted Data Successfully");
                    ClearDetails();

                }
                //}
                //else
                //{
                //    Global.Message("Invoice ID not found");
                //    blnReturn = false;
                //}
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
                btnDelete.Enabled = true;
            }

            return blnReturn;
        }

        private void txtLotID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
