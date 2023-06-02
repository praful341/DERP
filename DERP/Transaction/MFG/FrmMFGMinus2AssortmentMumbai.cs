using BLL;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.PropertyClasses.Master;
using BLL.PropertyClasses.Report;
using BLL.PropertyClasses.Transaction;
using BLL.PropertyClasses.Transaction.MFG;
using DERP.Class;
using DERP.Master;
using DERP.Master.MFG;
using DERP.Report;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Transaction.MFG
{
    public partial class FrmMFGMinus2AssortmentMumbai : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;
        DataTable m_dtbSubProcess = new DataTable();
        MFGAssortFinalLotting objAssortFinal;

        public New_Report_DetailProperty ObjReportDetailProperty;
        private List<Control> _tabControls = new List<Control>();
        Control _NextEnteredControl = new Control();

        DataTable DtControlSettings;
        DataTable DtAssortment = new DataTable();
        DataTable dtTemp;
        DataTable m_dtbParam;
        DataTable m_dtCut;
        DataTable m_DtProcess;
        DataTable m_dtbType;
        DataTable m_dtbKapan;
        DataTable m_dtOutstanding;
        DataTable DTabSummary = new DataTable();
        RateTypeMaster ObjRateType = new RateTypeMaster();

        Int64 m_numForm_id;
        Int64 IntRes;
        Int64 Receive_IntRes;
        int Count = 0;
        int m_IsLot;
        decimal m_TotalSumm;
        Int64 Lot_SrNo = 0;

        #endregion

        #region Constructor
        public FrmMFGMinus2AssortmentMumbai()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            objAssortFinal = new MFGAssortFinalLotting();
            ObjReportDetailProperty = new New_Report_DetailProperty();
            DtControlSettings = new DataTable();
            dtTemp = new DataTable();
            m_dtbParam = new DataTable();
            m_dtCut = new DataTable();
            m_DtProcess = new DataTable();
            m_dtOutstanding = new DataTable();
            m_dtbKapan = new DataTable();
            m_numForm_id = 0;
            m_TotalSumm = 0;
            m_IsLot = 0;
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
            AddKeyPressListener(this);
            this.KeyPreview = true;

            TabControlsToList(this.Controls);
            _tabControls = _tabControls.OrderBy(x => x.TabIndex).ToList();

            DTabSummary.Columns.Add(new DataColumn("type", typeof(string)));
            DTabSummary.Columns.Add(new DataColumn("13_-2_carat", typeof(decimal)));
            DTabSummary.Columns.Add(new DataColumn("16_+2_carat", typeof(decimal)));
            DTabSummary.Columns.Add(new DataColumn("17_-00_carat", typeof(decimal)));

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
                //string Str = "";
                //if (Val.DBDate(BLL.GlobalDec.gStrServerDate) != Val.DBDate(dtpReceiveDate.Text))
                //{
                //    Str = GlobalDec.CheckLockIsOpenOrNot(Val.DBDate(dtpReceiveDate.Text), Val.DBTime(DateTime.Now.ToShortTimeString())); //Val.GetFullTime12());
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
                //        dtpReceiveDate.Enabled = true;
                //        dtpReceiveDate.Visible = true;
                //    }
                //}

                //btnSave.Enabled = false;
                DataTable dtbDetail = (DataTable)grdAssortFinal.DataSource;

                //if (dtbDetail.Rows.Count > 0)
                //{
                //    for (int i = dtbDetail.Columns.Count - 1; i >= 2; i--)
                //    {
                //        if (Val.ToString(dtbDetail.Columns[i]) == Val.ToString(dtbDetail.Columns[i]).Split('_')[0] + "_carat")
                //        {
                //            foreach (DataRow Drw in dtbDetail.Rows)
                //            {
                //                if (Val.ToDecimal(Drw[Val.ToString(Val.ToInt(Val.ToString(dtbDetail.Columns[i]).Split('_')[0])) + "_" + "carat"]) > 0)
                //                {
                //                    if (Val.ToDecimal(Drw[Val.ToString(Val.ToInt(Val.ToString(dtbDetail.Columns[i]).Split('_')[0])) + "_" + "amount"]) == 0)
                //                    {
                //                        Global.Confirm("Amount Cannot Be Blank Please Check!!");
                //                        return;
                //                    }
                //                }
                //            }
                //        }
                //    }
                //}
                if (!ValidateDetails())
                {
                    return;
                }

                if (Val.ToDecimal(txtCarat.Text) != Val.ToDecimal(txtMain.Text))
                {
                    Global.Message("Entry Carat Not match O/s Carat");
                    return;
                }
                DialogResult result = MessageBox.Show("Do you want to save Assort Mumbai -2 data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result != DialogResult.Yes)
                {
                    btnSave.Enabled = true;
                    return;
                }

                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                backgroundWorker_AssortFirstReceive.RunWorkerAsync();

                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            //grdProcessReceive.DataSource = null;
            //btnSearch_Click(null, null);
            ClearDetails();
            panelControl1.Enabled = true;
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

                //lueKapan.EditValue = System.DBNull.Value;
                //lueCutNo.EditValue = System.DBNull.Value;
                //lueProcess.EditValue = System.DBNull.Value;
                //lueSubProcess.EditValue = System.DBNull.Value;

                //for (int i = 0; i < lueAssort.Properties.Items.Count; i++)
                //    lueAssort.Properties.Items[i].CheckState = CheckState.Unchecked;

                //for (int i = 0; i < lueSieve.Properties.Items.Count; i++)
                //    lueSieve.Properties.Items[i].CheckState = CheckState.Unchecked;

                txtRGhat.Text = "0";
                txtRBaki.Text = Val.ToString(0);
                txtBGhat.Text = Val.ToString(0);
                txtLotId.Text = "0";
                txtMain.Text = "0";
                txtCarat.Text = "0";
                txtMainGhat.Text = "0";
                txtAssortGhat.Text = "0";
                grdAssortFinal.DataSource = null;
                dgvAssortFinal.Columns.Clear();
                grdAssortGroupTotal.DataSource = null;
                //dgvAssortGroupTotal.Columns.Clear();
                btnSave.Enabled = true;
                //count = 0;
                lblLotSRNo.Text = "0";
                //RBtnLocationType.SelectedIndex = 0;
                RBtnLocationType.Enabled = true;
                lueKapan.Focus();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void FillSummaryGrid()
        {
            DTabSummary.Rows.Clear();
            DataTable DtTransfer = new DataTable();

            DtTransfer = (DataTable)grdAssortFinal.DataSource;

            DtTransfer.AcceptChanges();


            string[] grpArray = new string[1] { "type" };
            DataTable temp = new DataTable();
            for (int i = 0; i < grpArray.Length; i++)
            {
                if (grpArray[i] == "type")
                {
                    temp = DtTransfer;
                }

                var query = temp.AsEnumerable()
                                       .GroupBy(w => new
                                       {
                                           type = w.Field<string>(grpArray[i])
                                       })
                                                .Select(x => new
                                                {
                                                    type = x.Key.type,
                                                    minus2_carat = x.Sum(w => Val.Val(w["13_-2_carat"])),
                                                    plus2_carat = x.Sum(w => Val.Val(w["16_+2_carat"])),
                                                    minus00_carat = x.Sum(w => Val.Val(w["17_-00_carat"]))
                                                });
                DataTable DTProcessWise = LINQToDataTable(query);
                foreach (DataRow DRow in DTProcessWise.Rows)
                {
                    if (Val.Val(DRow["minus2_carat"]) == 0 && Val.Val(DRow["plus2_carat"]) == 0 && Val.Val(DRow["minus00_carat"]) == 0)
                        continue;
                    DataRow DRNew = DTabSummary.NewRow();
                    DRNew["type"] = DRow["type"];
                    DRNew["13_-2_carat"] = Math.Round(Val.Val(DRow["minus2_carat"]), 3);
                    DRNew["16_+2_carat"] = Math.Round(Val.Val(DRow["plus2_carat"]), 3);
                    DRNew["17_-00_carat"] = Math.Round(Val.Val(DRow["minus00_carat"]), 3);
                    DTabSummary.Rows.Add(DRNew);
                }
            }
            grdAssortGroupTotal.DataSource = DTabSummary;
            dgvAssortGroupTotal.BestFitColumns();
            //dgvRejPuritySummary.Columns["type"].Group();
            dgvAssortGroupTotal.ExpandAllGroups();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateDetails())
                {
                    return;
                }
                RBtnLocationType.Enabled = false;
                dtTemp = new DataTable();
                DataTable dtnew = new DataTable();
                dtnew.AcceptChanges();
                dgvAssortFinal.Columns.Clear();
                lblLotSRNo.Text = "0";

                dtnew = objAssortFinal.Minus2Mumbai_Assort_GetData(Val.ToString(lueAssort.EditValue), Val.ToString(lueSieve.EditValue), Val.ToInt32(lueKapan.EditValue), Val.ToInt32(lueCutNo.EditValue), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue), Val.ToString(lueSieve.Text), Val.ToInt64(lblLotSRNo.Text), Val.ToInt64(RBtnLocationType.EditValue), Val.DBDate(dtpRateDate.Text), Val.ToInt(2));

                txtAssortGhat.Text = Val.ToString("0");
                txtBGhat.Text = Val.ToString("0");
                txtRGhat.Text = Val.ToString("0");
                txtMainGhat.Text = Val.ToString("0");

                if (dtnew.Rows.Count > 0)
                {
                    pivot pt = new pivot(dtnew);
                    dtTemp = pt.PivotDataSuperPlusAssortment(new string[] { "type", "assort_id", "assort" }, new string[] { "carat", "per(%)", "rate", "amount", }, new AggregateFunction[] { AggregateFunction.Sum, AggregateFunction.Sum, AggregateFunction.Sum, AggregateFunction.Sum }, new string[] { "sieve" });
                    DataColumn Total = new System.Data.DataColumn("Total", typeof(System.Decimal));
                    DataColumn Percentage = new System.Data.DataColumn("Total%", typeof(System.Decimal));
                    DataColumn Ttlamount = new System.Data.DataColumn("Total_AMT", typeof(System.Decimal));
                    Total.DefaultValue = "0.000";
                    Percentage.DefaultValue = "0.000";
                    Ttlamount.DefaultValue = "0";
                    dtTemp.Columns.Add(Total);
                    dtTemp.Columns.Add(Percentage);
                    dtTemp.Columns.Add(Ttlamount);

                    int index = 3;

                    for (int j = 2; j <= dtTemp.Columns.Count - 1; j++)
                    {
                        if (dtTemp.Columns[j].ToString().Contains("carat"))
                        {
                            dtTemp.Columns[j].SetOrdinal(index);
                            index++;

                        }
                    }
                    for (int j = 0; j <= dtTemp.Columns.Count - 1; j++)
                    {
                        if (dtTemp.Columns[j].ToString().Contains("rate"))
                        {
                            dtTemp.Columns[j].SetOrdinal(index);
                            index++;
                        }
                    }
                    for (int j = 0; j <= dtTemp.Columns.Count - 1; j++)
                    {
                        if (dtTemp.Columns[j].ToString().Contains("amount"))
                        {
                            dtTemp.Columns[j].SetOrdinal(index);
                            index++;
                        }
                    }
                    index = 0;

                    grdAssortFinal.DataSource = dtTemp;
                    //dgvAssortFinal.Columns["type"].Visible = false;
                    dgvAssortFinal.Columns["assort_id"].Visible = false;
                    dgvAssortFinal.Columns["assort"].OptionsColumn.ReadOnly = true;
                    dgvAssortFinal.Columns["assort"].OptionsColumn.AllowFocus = false;
                    dgvAssortFinal.Columns["Total"].OptionsColumn.ReadOnly = true;
                    dgvAssortFinal.Columns["Total"].OptionsColumn.AllowFocus = false;
                    dgvAssortFinal.Columns["Total%"].OptionsColumn.ReadOnly = true;
                    dgvAssortFinal.Columns["Total%"].OptionsColumn.AllowFocus = false;
                    dgvAssortFinal.Columns["Total%"].Visible = false;
                    dgvAssortFinal.Columns["assort"].Caption = "#";
                    dgvAssortFinal.Columns["assort"].Fixed = FixedStyle.Left;
                    dgvAssortFinal.Columns["assort"].Caption = "#";
                    decimal Total_Tot;
                    decimal Total_Amt;
                    for (int i = 0; i <= dtTemp.Rows.Count - 1; i++)
                    {
                        Total_Tot = 0;
                        Total_Amt = 0;
                        for (int j = 0; j <= dtTemp.Columns.Count - 1; j++)
                        {
                            if (dtTemp.Columns[j].ToString().Contains("per(%)"))
                            {
                                dgvAssortFinal.Columns[j].OptionsColumn.AllowEdit = false;
                                dgvAssortFinal.Columns[j].Visible = false;
                            }
                            if (dtTemp.Columns[j].ToString().Contains("carat"))
                            {
                                Total_Tot = Total_Tot + Val.ToDecimal(dtTemp.Rows[i][j].ToString());
                                string[] col = dtTemp.Columns[j].ToString().Split('_');
                                string currcol = "";
                                currcol = col[1] + "_" + col[2];
                                dgvAssortFinal.Columns[j].Caption = currcol;
                                //dgvAssortFinal.Columns[dtTemp.Columns[j].ToString()].Width = 150;
                            }
                            if (dtTemp.Columns[j].ToString().Contains("rate"))
                            {
                                string[] col = dtTemp.Columns[j].ToString().Split('_');
                                string currcol = "";
                                currcol = col[1] + "_" + col[2];
                                dgvAssortFinal.Columns[j].Caption = currcol;
                                // dgvAssortFinal.Columns[dtTemp.Columns[j].ToString()].Width = 150;
                            }
                            if (dtTemp.Columns[j].ToString().Contains("amount"))
                            {
                                Total_Amt = Total_Amt + Val.ToDecimal(dtTemp.Rows[i][j].ToString());
                                string[] col = dtTemp.Columns[j].ToString().Split('_');
                                string currcol = "";
                                currcol = col[1] + "_" + col[2];
                                dgvAssortFinal.Columns[j].Caption = currcol;
                                // dgvAssortFinal.Columns[dtTemp.Columns[j].ToString()].Width = 150;
                            }
                            if (dtTemp.Columns[j].ColumnName.Contains("Total"))
                            {
                                decimal total = Total_Tot;
                                dtTemp.Rows[i][j] = total;
                            }
                            if (dtTemp.Columns[j].ColumnName.Contains("Total_AMT"))
                            {
                                dgvAssortFinal.Columns[j].OptionsColumn.AllowEdit = false;
                                decimal total_Amt = Total_Amt;
                                dtTemp.Rows[i][j] = total_Amt;
                            }
                        }
                        for (int j = 0; j <= dtTemp.Columns.Count - 1; j++)
                        {
                            if (dtTemp.Columns[j].ToString().Contains("rate"))
                            {
                                dgvAssortFinal.Columns[j].OptionsColumn.AllowEdit = false;
                                dgvAssortFinal.Columns[j].Visible = true;
                                //dgvAssortFinal.Columns[dtTemp.Columns[j].ToString()].VisibleIndex = 3;
                            }
                        }
                        for (int j = 0; j <= dtTemp.Columns.Count - 1; j++)
                        {
                            if (dtTemp.Columns[j].ToString().Contains("amount"))
                            {
                                dgvAssortFinal.Columns[j].OptionsColumn.AllowEdit = false;
                                dgvAssortFinal.Columns[j].Visible = true;
                                //dgvAssortFinal.Columns[dtTemp.Columns[j].ToString()].VisibleIndex = 5;
                            }
                        }
                    }
                    for (int i = 0; i <= dtTemp.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j <= dtTemp.Columns.Count - 1; j++)
                        {
                            if (dtTemp.Columns[j].ToString().Contains("carat"))
                            {
                                string carat = dtTemp.Columns[j].ToString();
                                GridColumn column1 = dgvAssortFinal.Columns[carat];
                                dgvAssortFinal.Columns[carat].SummaryItem.DisplayFormat = "{0:n3}";
                                column1.SummaryItem.SummaryType = SummaryItemType.Sum;
                            }

                            if (dtTemp.Columns[j].ToString().Contains("rate"))
                            {
                                string rate = dtTemp.Columns[j].ToString();
                                GridColumn column2 = dgvAssortFinal.Columns[rate];
                                dgvAssortFinal.Columns[rate].SummaryItem.DisplayFormat = " {0:n2}";
                                column2.SummaryItem.SummaryType = SummaryItemType.Custom;
                            }
                            if (dtTemp.Columns[j].ToString().Contains("amount"))
                            {
                                string amt = dtTemp.Columns[j].ToString();
                                GridColumn column3 = dgvAssortFinal.Columns[amt];
                                dgvAssortFinal.Columns[amt].SummaryItem.DisplayFormat = " {0:n0}";
                                column3.SummaryItem.SummaryType = SummaryItemType.Sum;
                            }
                            if (dtTemp.Columns[j].ToString().Contains("per(%)"))
                            {
                                string Per = dtTemp.Columns[j].ToString();
                                GridColumn column4 = dgvAssortFinal.Columns[Per];
                                dgvAssortFinal.Columns[Per].SummaryItem.DisplayFormat = " {0:n2}";
                                column4.SummaryItem.SummaryType = SummaryItemType.Custom;
                            }
                            if (dtTemp.Columns[j].ColumnName.Contains("Total"))
                            {
                                string total = dtTemp.Columns[j].ToString();
                                GridColumn column5 = dgvAssortFinal.Columns[total];
                                dgvAssortFinal.Columns[total].SummaryItem.DisplayFormat = "{0:n3}";
                                column5.SummaryItem.SummaryType = SummaryItemType.Sum;
                            }
                            if (dtTemp.Columns[j].ColumnName.Contains("Total_AMT"))
                            {
                                string totalamt = dtTemp.Columns[j].ToString();
                                GridColumn column6 = dgvAssortFinal.Columns[totalamt];
                                dgvAssortFinal.Columns[totalamt].SummaryItem.DisplayFormat = "{0:n3}";
                                column6.SummaryItem.SummaryType = SummaryItemType.Sum;
                            }
                        }
                        break;
                    }
                }

                if (Val.ToDecimal(dtnew.Rows[0]["carat"]) > 0)
                {
                    //decimal Total_Amount = Val.ToDecimal(dtnew.Compute("Sum(carat)", string.Empty)) + Val.ToDecimal(dtnew.Rows[0]["bghat"]) + Val.ToDecimal(dtnew.Rows[0]["rbaki"]) + Val.ToDecimal(dtnew.Rows[0]["assortghat"]);
                    //lblOsCarat.Text = Total_Amount.ToString(); //Val.ToString(Val.ToDecimal(dtnew.Rows[0]["carat"])).Sum();
                    //txtBGhat.Text = Val.ToString(Val.ToDecimal(dtnew.Rows[0]["bghat"]));
                    //txtAssortGhat.Text = Val.ToString(Val.ToDecimal(dtnew.Rows[0]["assortghat"]));
                    //txtRBaki.Text = Val.ToString(Val.ToDecimal(dtnew.Rows[0]["rbaki"]));
                    //txtMainGhat.Text = Val.ToString(Val.ToDecimal(dtnew.Rows[0]["mainghat"]));
                }
                else
                {
                    txtBGhat.Text = "0";
                    txtAssortGhat.Text = "0";
                    txtRBaki.Text = "0";
                    txtMainGhat.Text = "0";
                    btnSave.Enabled = true;
                }

                dgvAssortFinal.OptionsView.ShowFooter = true;
                dgvAssortFinal.BestFitColumns();
                //panelControl1.Enabled = false;
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());

            }
        }
        private void lueCutNo_EditValueChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (count == 0)
            //    {
            //        MFGProcessReceive objProcessRecieve = new MFGProcessReceive();

            //        m_dtOutstanding = Global.GetStockCutwise(Val.ToInt(lueKapan.EditValue), Val.ToInt(lueCutNo.EditValue));

            //        if (m_dtOutstanding.Rows.Count > 0)
            //        {
            //            m_kapan_id = Val.ToInt(m_dtOutstanding.Rows[0]["kapan_id"]);
            //            //lblOsPcs.Text = Val.ToString(Val.ToDecimal(m_dtOutstanding.Rows[0]["balance_pcs"]));
            //            //lblOsCarat.Text = Val.ToString(Val.ToDecimal(m_dtOutstanding.Rows[0]["balance_carat"]));
            //            //m_balcarat = Val.ToDecimal(m_dtOutstanding.Rows[0]["balance_carat"]);
            //            //txtLotId.Text = Val.ToString(Val.ToInt(m_dtOutstanding.Rows[0]["lot_id"]));
            //        }
            //        else
            //        {
            //            //lblOsPcs.Text = Val.ToString("0");
            //            //lblOsCarat.Text = Val.ToString("0.00");
            //            return;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    BLL.General.ShowErrors(ex);
            //    return;
            //}
        }
        private void lueKapan_EditValueChanged(object sender, EventArgs e)
        {
            if (m_IsLot == 0)
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
        }
        private void grdProcessReceive_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    (grdAssortFinal.FocusedView as ColumnView).FocusedRowHandle++;
                    e.Handled = true;
                }
            }
            catch { }
        }
        private void dgvAssortFinal_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                DataTable dtAmount = new DataTable();
                dtAmount = (DataTable)grdAssortFinal.DataSource;
                string[] col = e.Column.FieldName.Split('_');
                string currcol = "";
                if (col.Length > 3 && e.Column.FieldName.Contains("_") && col != null)
                {
                    currcol = col[0] + "_" + col[1];
                }
                decimal carat = 0;
                decimal total = 0;
                decimal rate = 0;
                decimal totAmount = 0;
                decimal perTotal = 0;
                string colname = "";
                decimal Percent = 0;
                for (int i = 0; i <= dtAmount.Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= dtAmount.Columns.Count - 1; j++)
                    {
                        string[] prefix = dtAmount.Columns[j].ToString().Split('_');
                        if (e.RowHandle != i)
                            continue;
                        if (dtAmount.Columns[j].ToString().Contains("carat") && dtAmount.Columns[j].ColumnName.Contains(currcol))
                        {
                            perTotal = 0;
                            carat = Val.ToDecimal(dtAmount.Rows[i][j]);
                            total += carat;
                            perTotal = carat;
                            colname = currcol;
                            rate = Val.ToDecimal(dtAmount.Rows[i][prefix[0] + "_" + prefix[1] + "_rate"]);
                            totAmount += Val.ToDecimal(carat * rate);
                            dtAmount.Rows[i][prefix[0] + "_" + prefix[1] + "_amount"] = Math.Round((carat * rate), 0).ToString();
                            Percent = (perTotal * 100) / Val.ToDecimal(txtCarat.Text);
                            dtAmount.Rows[i][prefix[0] + "_" + prefix[1] + "_per(%)"] = Math.Round(Percent, 2).ToString();
                        }


                        if (dtAmount.Columns[j].ColumnName.ToString() == "Total")
                        {
                            dtAmount.Rows[i][j] = Math.Round(total, 3).ToString();
                            perTotal = carat;
                            dtAmount.Rows[i]["Total_AMT"] = Math.Round(totAmount, 0);
                        }

                        if (dtAmount.Columns[j].ColumnName.Contains("Total%"))
                        {
                            if (Val.ToDecimal(txtCarat.Text) > 0)
                            {
                                Percent = (total * 100) / Val.ToDecimal(txtCarat.Text);
                                dtAmount.Rows[i][j] = Math.Round(Percent, 2).ToString();
                            }
                            else
                            {
                                dtAmount.Rows[i][j] = 0;
                            }
                            //break;
                        }

                        if (dtAmount.Columns[j].ColumnName.ToString() == "Flag")
                        {
                            dtAmount.Rows[i][j] = 1;
                            break;
                        }
                    }

                    total = 0;
                    //dtAmount.AcceptChanges();
                    //CalculateMain();
                    //dgvAssortFinal.BestFitColumns();
                }
                dtAmount.AcceptChanges();
                CalculateMain();
                dgvAssortFinal.BestFitColumns();
                FillSummaryGrid();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        private void dgvAssortFinal_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
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
                FillSummaryGrid();
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void lueProcess_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmProcessMaster frmProcess = new FrmProcessMaster();
                frmProcess.ShowDialog();
                Global.LOOKUPProcess(lueProcess);
            }
        }
        private void lueSubProcess_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmMfgSubProcessMaster frmSubProcess = new FrmMfgSubProcessMaster();
                frmSubProcess.ShowDialog();
                Global.LOOKUPSubProcess(lueSubProcess);
                m_dtbSubProcess = (((DataTable)lueSubProcess.Properties.DataSource).Copy());
            }
        }
        private void FrmMFGAssortFinal_Load(object sender, EventArgs e)
        {
            try
            {
                m_dtbKapan = Global.GetKapanAll_Assort();

                lueKapan.Properties.DataSource = m_dtbKapan;
                lueKapan.Properties.ValueMember = "kapan_id";
                lueKapan.Properties.DisplayMember = "kapan_no";

                m_dtCut = Global.GetRoughCutAll();
                lueCutNo.Properties.DataSource = m_dtCut;
                lueCutNo.Properties.ValueMember = "rough_cut_id";
                lueCutNo.Properties.DisplayMember = "rough_cut_no";

                m_dtbType = new DataTable();
                m_dtbType.Columns.Add("type");
                m_dtbType.Rows.Add("BOTH");
                m_dtbType.Rows.Add("NORMAL");
                m_dtbType.Rows.Add("REJECTION");

                dtpReceiveDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpReceiveDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpReceiveDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpReceiveDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpReceiveDate.EditValue = DateTime.Now;

                Global.LOOKUPSieve(lueSieve);
                Global.LOOKUPProcess(lueProcess);
                lueProcess.Text = "ASSORTMENT";

                Global.LOOKUPSubProcess(lueSubProcess);
                Global.LOOKUPAssort(lueAssort);

                lueSieve.SetEditValue("13,16,17");
                lueAssort.SetEditValue("514,515,516,517,518,519,520,521,522,523,524,1671,1672,1673,1674,1675,1676,1677,398,400,401,402,403,404,1678,1679,1810,1811,1812,1813,1814,1815,1816");
                m_dtbSubProcess = (((DataTable)lueSubProcess.Properties.DataSource).Copy());
                lueProcess_EditValueChanged(null, null);

                lueSubProcess.Text = "-2 MUMBAI ASSORT";
                m_dtbParam = Global.GetRoughCutAll();


                dtpRateDate.Properties.Items.Clear();

                RateType_MasterProperty RateTypeProperty = new RateType_MasterProperty();
                RateTypeProperty.ratetype_id = Val.ToInt(2);
                DataTable DTab = ObjRateType.GetDate(RateTypeProperty);

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


                // Add By Praful On 29072021

                //DTab_KapanWiseData = Global.GetRoughStockWise(Val.ToInt(0), Val.ToInt32(0));

                // End By Praful On 29072021
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void backgroundWorker_AssortFinalReceive_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                MFGAssortFinalLotting MFGAssortFinal = new MFGAssortFinalLotting();
                MFGAssortFirst MFGAssortReceive = new MFGAssortFirst();
                MFGAssortFinal_LottingProperty objMFGAssortFinalLottingProperty = new MFGAssortFinal_LottingProperty();

                Conn = new BeginTranConnection(true, false);
                DataTable m_DTab = new DataTable();
                ArrayList MyArrayList = new ArrayList();

                IntRes = 0;
                Receive_IntRes = 0;
                int Mix_Union_Id = 0;
                int NewIssue_Union_Id = 0;
                Int64 NewHistory_Union_Id = 0;
                Lot_SrNo = 0;
                try
                {
                    if (lblLotSRNo.Text.ToString() == "0")
                    {
                        objMFGAssortFinalLottingProperty.lot_id = Val.ToInt64(txtLotId.Text);
                        objMFGAssortFinalLottingProperty.flag_update = Val.ToInt(2);
                        int Lot_Flag = MFGAssortFinal.GetUpdateLot_ID_Flag(objMFGAssortFinalLottingProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                    }

                    objMFGAssortFinalLottingProperty.manager_id = Val.ToInt(0);
                    objMFGAssortFinalLottingProperty.employee_id = Val.ToInt(0);
                    objMFGAssortFinalLottingProperty.process_id = Val.ToInt(lueProcess.EditValue);
                    objMFGAssortFinalLottingProperty.sub_process_id = Val.ToInt(lueSubProcess.EditValue);
                    objMFGAssortFinalLottingProperty.kapan_id = Val.ToInt(lueKapan.EditValue);
                    objMFGAssortFinalLottingProperty.lot_id = Val.ToInt64(txtLotId.Text);
                    objMFGAssortFinalLottingProperty.rough_cut_id = Val.ToInt(lueCutNo.EditValue);
                    objMFGAssortFinalLottingProperty.temp_sieve_name = lueSieve.Text.ToString();
                    objMFGAssortFinalLottingProperty.flag = Val.ToInt(2);

                    if (Val.ToInt64(lblLotSRNo.Text) > 0)
                    {
                        Lot_SrNo = Val.ToInt64(lblLotSRNo.Text);
                        objMFGAssortFinalLottingProperty.Del_lot_srno = Val.ToInt64(lblLotSRNo.Text);
                        int Lot_Delete = MFGAssortFinal.GetDeleteLot_ID(objMFGAssortFinalLottingProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                    }

                    m_DTab = (DataTable)grdAssortFinal.DataSource;
                    DataTable dtbDetail = m_DTab.Copy();
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
                    for (int i = dtbDetail.Columns.Count - 1; i >= 2; i--)
                    {
                        string strNew = Val.ToString(dtbDetail.Columns[i]);
                        string str = strNew.Substring(strNew.LastIndexOf("_") + 1);

                        dtbDetail.Columns[Val.ToString(dtbDetail.Columns[i])].ColumnName = strNew.Split('_')[0] + "_" + str;
                    }
                    for (int i = dtbDetail.Columns.Count - 1; i >= 2; i--)
                    {
                        if (Val.ToString(dtbDetail.Columns[i]) == Val.ToString(dtbDetail.Columns[i]).Split('_')[0] + "_carat")
                        {
                            foreach (DataRow Drw in dtbDetail.Rows)
                            {
                                objMFGAssortFinalLottingProperty.lot_id = Val.ToInt64(txtLotId.Text);
                                objMFGAssortFinalLottingProperty.rough_cut_id = Val.ToInt(lueCutNo.EditValue);
                                objMFGAssortFinalLottingProperty.receive_date = Val.DBDate(dtpReceiveDate.Text);
                                objMFGAssortFinalLottingProperty.assort_id = Val.ToInt(Drw["assort_id"]);
                                objMFGAssortFinalLottingProperty.sieve_id = Val.ToInt(Val.ToString(dtbDetail.Columns[i]).Split('_')[0]);
                                if (Count == 0)
                                {
                                    objMFGAssortFinalLottingProperty.carat = Val.ToDecimal(txtCarat.Text);
                                }
                                else
                                {
                                    objMFGAssortFinalLottingProperty.carat = Val.ToDecimal(0);
                                }
                                objMFGAssortFinalLottingProperty.to_carat = Val.ToDecimal(Drw[Val.ToString(objMFGAssortFinalLottingProperty.sieve_id) + "_" + "carat"]);
                                objMFGAssortFinalLottingProperty.percentage = Val.ToDecimal(Drw[Val.ToString(objMFGAssortFinalLottingProperty.sieve_id) + "_" + "per(%)"]);
                                objMFGAssortFinalLottingProperty.rate = Val.ToDecimal(Drw[Val.ToString(objMFGAssortFinalLottingProperty.sieve_id) + "_" + "rate"]);
                                objMFGAssortFinalLottingProperty.amount = Val.ToDecimal(Drw[Val.ToString(objMFGAssortFinalLottingProperty.sieve_id) + "_" + "amount"]);

                                objMFGAssortFinalLottingProperty.union_id = IntRes;
                                objMFGAssortFinalLottingProperty.receive_union_id = Receive_IntRes;
                                objMFGAssortFinalLottingProperty.form_id = m_numForm_id;
                                objMFGAssortFinalLottingProperty.history_union_id = NewHistory_Union_Id;
                                objMFGAssortFinalLottingProperty.issue_union_id = NewIssue_Union_Id;
                                objMFGAssortFinalLottingProperty.count = Count;
                                objMFGAssortFinalLottingProperty.mix_union_id = Mix_Union_Id;

                                objMFGAssortFinalLottingProperty.lot_srno = Lot_SrNo;
                                objMFGAssortFinalLottingProperty.temp_sieve_name = lueSieve.Text.ToString();

                                if (RBtnLocationType.EditValue.ToString() == "1")
                                {
                                    objMFGAssortFinalLottingProperty.location_id = Val.ToInt32(1);
                                }
                                else
                                {
                                    objMFGAssortFinalLottingProperty.location_id = Val.ToInt32(2);
                                }
                                objMFGAssortFinalLottingProperty = MFGAssortFinal.Save(objMFGAssortFinalLottingProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                                IntRes = objMFGAssortFinalLottingProperty.union_id;
                                Receive_IntRes = Val.ToInt64(objMFGAssortFinalLottingProperty.receive_union_id);
                                NewHistory_Union_Id = Val.ToInt64(objMFGAssortFinalLottingProperty.history_union_id);
                                Lot_SrNo = Val.ToInt64(objMFGAssortFinalLottingProperty.lot_srno);
                            }
                        }
                    }

                    int AssortId = 0;
                    if (Val.ToDecimal(txtMainGhat.Text) != 0)
                    {
                        AssortId = MFGAssortFinal.GetAssortId("MAIN.GHAT");
                        objMFGAssortFinalLottingProperty.assort_id = Val.ToInt(AssortId);
                        objMFGAssortFinalLottingProperty.sieve_id = Val.ToInt(0);
                        objMFGAssortFinalLottingProperty.percentage = Val.ToDecimal(txtMainGhatPer.Text);
                        objMFGAssortFinalLottingProperty.to_carat = Val.ToDecimal(txtMainGhat.Text);

                        objMFGAssortFinalLottingProperty = MFGAssortFinal.Save(objMFGAssortFinalLottingProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                        IntRes = objMFGAssortFinalLottingProperty.union_id;

                        Receive_IntRes = objMFGAssortFinalLottingProperty.receive_union_id;
                        NewHistory_Union_Id = Val.ToInt64(objMFGAssortFinalLottingProperty.history_union_id);
                        NewIssue_Union_Id = Val.ToInt(objMFGAssortFinalLottingProperty.issue_union_id);
                        Mix_Union_Id = Val.ToInt(objMFGAssortFinalLottingProperty.mix_union_id);
                    }
                    if (Val.ToDecimal(txtRBaki.Text) != 0)
                    {
                        AssortId = MFGAssortFinal.GetAssortId("R.BAAKI");
                        objMFGAssortFinalLottingProperty.assort_id = Val.ToInt(AssortId);
                        objMFGAssortFinalLottingProperty.sieve_id = Val.ToInt(0);
                        objMFGAssortFinalLottingProperty.percentage = Val.ToDecimal(txtRBakiPer.Text);
                        objMFGAssortFinalLottingProperty.to_carat = Val.ToDecimal(txtRBaki.Text);
                        objMFGAssortFinalLottingProperty.lot_srno = Lot_SrNo;
                        objMFGAssortFinalLottingProperty.temp_sieve_name = lueSieve.Text.ToString();

                        objMFGAssortFinalLottingProperty = MFGAssortFinal.GhatSave(objMFGAssortFinalLottingProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                        IntRes = objMFGAssortFinalLottingProperty.union_id;

                        Receive_IntRes = objMFGAssortFinalLottingProperty.receive_union_id;
                        NewHistory_Union_Id = Val.ToInt64(objMFGAssortFinalLottingProperty.history_union_id);
                        NewIssue_Union_Id = Val.ToInt(objMFGAssortFinalLottingProperty.issue_union_id);
                        Mix_Union_Id = Val.ToInt(objMFGAssortFinalLottingProperty.mix_union_id);
                        Lot_SrNo = Val.ToInt64(objMFGAssortFinalLottingProperty.lot_srno);
                    }
                    if (Val.ToDecimal(txtRGhat.Text) != 0)
                    {
                        AssortId = MFGAssortFinal.GetAssortId("R.GHAT");
                        objMFGAssortFinalLottingProperty.assort_id = Val.ToInt(AssortId);
                        objMFGAssortFinalLottingProperty.sieve_id = Val.ToInt(0);
                        objMFGAssortFinalLottingProperty.percentage = Val.ToDecimal(txtRGhatPer.Text);
                        objMFGAssortFinalLottingProperty.to_carat = Val.ToDecimal(txtRGhat.Text);
                        objMFGAssortFinalLottingProperty.lot_srno = Lot_SrNo;
                        objMFGAssortFinalLottingProperty.temp_sieve_name = lueSieve.Text.ToString();

                        objMFGAssortFinalLottingProperty = MFGAssortFinal.GhatSave(objMFGAssortFinalLottingProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                        IntRes = objMFGAssortFinalLottingProperty.union_id;

                        Receive_IntRes = objMFGAssortFinalLottingProperty.receive_union_id;
                        NewHistory_Union_Id = Val.ToInt64(objMFGAssortFinalLottingProperty.history_union_id);
                        NewIssue_Union_Id = Val.ToInt(objMFGAssortFinalLottingProperty.issue_union_id);
                        Mix_Union_Id = Val.ToInt(objMFGAssortFinalLottingProperty.mix_union_id);
                        Lot_SrNo = Val.ToInt64(objMFGAssortFinalLottingProperty.lot_srno);
                    }
                    if (Val.ToDecimal(txtBGhat.Text) != 0)
                    {
                        AssortId = MFGAssortFinal.GetAssortId("B.GHAT");
                        objMFGAssortFinalLottingProperty.assort_id = Val.ToInt(AssortId);
                        objMFGAssortFinalLottingProperty.sieve_id = Val.ToInt(0);
                        objMFGAssortFinalLottingProperty.percentage = Val.ToDecimal(txtBGhatPer.Text);
                        objMFGAssortFinalLottingProperty.to_carat = Val.ToDecimal(txtBGhat.Text);
                        objMFGAssortFinalLottingProperty.lot_srno = Lot_SrNo;
                        objMFGAssortFinalLottingProperty.temp_sieve_name = lueSieve.Text.ToString();

                        objMFGAssortFinalLottingProperty = MFGAssortFinal.GhatSave(objMFGAssortFinalLottingProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                        IntRes = objMFGAssortFinalLottingProperty.union_id;

                        Receive_IntRes = objMFGAssortFinalLottingProperty.receive_union_id;
                        NewHistory_Union_Id = Val.ToInt64(objMFGAssortFinalLottingProperty.history_union_id);
                        NewIssue_Union_Id = Val.ToInt(objMFGAssortFinalLottingProperty.issue_union_id);
                        Mix_Union_Id = Val.ToInt(objMFGAssortFinalLottingProperty.mix_union_id);
                        Lot_SrNo = Val.ToInt64(objMFGAssortFinalLottingProperty.lot_srno);
                    }
                    if (Val.ToDecimal(txtAssortGhat.Text) != 0)
                    {
                        AssortId = MFGAssortFinal.GetAssortId("ASSORT.GHAT");
                        objMFGAssortFinalLottingProperty.assort_id = Val.ToInt(AssortId);
                        objMFGAssortFinalLottingProperty.sieve_id = Val.ToInt(0);
                        objMFGAssortFinalLottingProperty.percentage = Val.ToDecimal(txtAssortGhatPer.Text);
                        objMFGAssortFinalLottingProperty.to_carat = Val.ToDecimal(txtAssortGhat.Text);
                        objMFGAssortFinalLottingProperty.lot_srno = Lot_SrNo;
                        objMFGAssortFinalLottingProperty.temp_sieve_name = lueSieve.Text.ToString();

                        objMFGAssortFinalLottingProperty = MFGAssortFinal.GhatSave(objMFGAssortFinalLottingProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                        IntRes = objMFGAssortFinalLottingProperty.union_id;

                        Receive_IntRes = objMFGAssortFinalLottingProperty.receive_union_id;
                        NewHistory_Union_Id = Val.ToInt64(objMFGAssortFinalLottingProperty.history_union_id);
                        NewIssue_Union_Id = Val.ToInt(objMFGAssortFinalLottingProperty.issue_union_id);
                        Mix_Union_Id = Val.ToInt(objMFGAssortFinalLottingProperty.mix_union_id);
                        Lot_SrNo = Val.ToInt64(objMFGAssortFinalLottingProperty.lot_srno);
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
        private void backgroundWorker_AssortFinalReceive_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (IntRes > 0)
                {
                    if (Global.Confirm("Assortment Mumbai-2 Data Save Succesfully.... " + "\n Are You Sure To Print ?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        try
                        {
                            string Date = Val.DBDate(dtpReceiveDate.Text);
                            btnClear_Click(null, null);
                            lblLotSRNo.Text = Lot_SrNo.ToString();
                            //btn_Print_Click(null, null);

                            //if (RBtnLocationType.EditValue.ToString() == "1")
                            //{
                            //    if (RBtnType.SelectedIndex == 0)
                            //    {
                            //        DataSet DTab_IssueJanged = objAssortFinal.Print_Assort_Final(Val.ToInt32(lueKapan.EditValue), Val.ToInt32(lueCutNo.EditValue), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue), Val.ToInt64(lblLotSRNo.Text), Val.ToInt64(RBtnLocationType.EditValue), Date, Val.DBDate(dtpRateDate.Text));

                            //        FrmReportViewer FrmReportViewer = new FrmReportViewer();
                            //        foreach (DataTable DTab in DTab_IssueJanged.Tables)
                            //            FrmReportViewer.DS.Tables.Add(DTab.Copy());
                            //        FrmReportViewer.GroupBy = "";
                            //        FrmReportViewer.RepName = "";
                            //        FrmReportViewer.RepPara = "";
                            //        this.Cursor = Cursors.Default;
                            //        FrmReportViewer.AllowSetFormula = true;

                            //        //FrmReportViewer.ShowForm_SubReport("CrtPolishGrading_Final_Main", 120, FrmReportViewer.ReportFolder.ACCOUNT);
                            //        FrmReportViewer.ShowForm_SubReport("CrtPolishGrading_Final_Main", 120, FrmReportViewer.ReportFolder.FINAL_MAIN);

                            //        DTab_IssueJanged = null;
                            //        FrmReportViewer.DS.Tables.Clear();
                            //        FrmReportViewer.DS.Clear();
                            //        FrmReportViewer = null;

                            //        DataSet DTab_IssueJanged_Print2 = objAssortFinal.Print_Assort_Final_Print2(Val.ToInt32(lueKapan.EditValue), Val.ToInt32(lueCutNo.EditValue), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue), Val.ToInt64(lblLotSRNo.Text), Val.ToInt64(RBtnLocationType.EditValue), Date, Val.DBDate(dtpRateDate.Text));


                            //        FrmReportViewer = new FrmReportViewer();
                            //        foreach (DataTable DTab in DTab_IssueJanged_Print2.Tables)
                            //            FrmReportViewer.DS.Tables.Add(DTab.Copy());
                            //        FrmReportViewer.GroupBy = "";
                            //        FrmReportViewer.RepName = "";
                            //        FrmReportViewer.RepPara = "";
                            //        this.Cursor = Cursors.Default;
                            //        FrmReportViewer.AllowSetFormula = true;

                            //        //FrmReportViewer.ShowForm_SubReport("CrtPolishGrading_Final_Main_Print2", 120, FrmReportViewer.ReportFolder.ACCOUNT);
                            //        FrmReportViewer.ShowForm_SubReport("CrtPolishGrading_Final_OK_Main_Print2", 120, FrmReportViewer.ReportFolder.FINAL_OK_SUB);

                            //        DTab_IssueJanged = null;
                            //        FrmReportViewer.DS.Tables.Clear();
                            //        FrmReportViewer.DS.Clear();
                            //        FrmReportViewer = null;
                            //    }
                            //}
                            //else if (RBtnLocationType.EditValue.ToString() == "2")
                            //{
                            if (RBtnType.SelectedIndex == 0)
                            {
                                DataSet DTab_IssueJanged = objAssortFinal.Print_Assort_Minus2_Mumbai(Val.ToInt32(lueKapan.EditValue), Val.ToInt32(lueCutNo.EditValue), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue), Val.ToInt64(lblLotSRNo.Text), Val.ToInt64(RBtnLocationType.EditValue), Val.DBDate(dtpReceiveDate.Text), Val.DBDate(dtpRateDate.Text), Val.ToInt32(2));


                                FrmReportViewer FrmReportViewer = new FrmReportViewer();
                                foreach (DataTable DTab in DTab_IssueJanged.Tables)
                                    FrmReportViewer.DS.Tables.Add(DTab.Copy());
                                FrmReportViewer.GroupBy = "";
                                FrmReportViewer.RepName = "";
                                FrmReportViewer.RepPara = "";
                                this.Cursor = Cursors.Default;
                                FrmReportViewer.AllowSetFormula = true;

                                //FrmReportViewer.ShowForm_SubReport("CrtPolishGrading_Final_Main_Mumbai", 120, FrmReportViewer.ReportFolder.ACCOUNT);
                                FrmReportViewer.ShowForm_SubReport("CrtPolishGrading_Minus2_Mumbai", 120, FrmReportViewer.ReportFolder.MINUS2_MUMBAI_ASSORTMENT);

                                DTab_IssueJanged = null;
                                FrmReportViewer.DS.Tables.Clear();
                                FrmReportViewer.DS.Clear();
                                FrmReportViewer = null;
                            }
                            //}
                            lblLotSRNo.Text = "0";
                        }
                        catch (Exception ex)
                        {
                            Global.Message(ex.ToString());
                            return;
                        }
                    }
                    else
                    {
                        btnClear_Click(null, null);
                    }

                }
                else
                {
                    Global.Confirm("Error In Assortment Final");
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }
        private void txtRGhat_EditValueChanged(object sender, EventArgs e)
        {
            if (Val.ToDecimal(txtCarat.Text) > 0 && Val.ToDecimal(txtRGhat.Text) > 0)
            {
                txtRGhatPer.Text = Val.ToString(Math.Round((Val.ToDecimal(txtRGhat.Text) * 100) / Val.ToDecimal(txtCarat.Text), 2));
                txtAssortGhat.Text = Val.ToString(Math.Round(Val.ToDecimal(txtMain.Text) - (m_TotalSumm + (Val.ToDecimal(txtRGhat.Text) + Val.ToDecimal(txtBGhat.Text) + Val.ToDecimal(txtRBaki.Text))), 3));

            }
            else
            {
                txtRGhatPer.Text = "0";
            }
            CalculateMain();
        }
        private void txtBGhat_EditValueChanged(object sender, EventArgs e)
        {
            CalculateMain();
            if (Val.ToDecimal(txtCarat.Text) > 0 && Val.ToDecimal(txtBGhat.Text) > 0)
            {
                txtBGhatPer.Text = Val.ToString(Math.Round((Val.ToDecimal(txtBGhat.Text) * 100) / Val.ToDecimal(txtCarat.Text), 2));
                txtAssortGhat.Text = Val.ToString(Math.Round(Val.ToDecimal(txtMain.Text) - (m_TotalSumm + (Val.ToDecimal(txtRGhat.Text) + Val.ToDecimal(txtBGhat.Text) + Val.ToDecimal(txtRBaki.Text))), 3));
                txtMainGhat.Text = Val.ToString(Math.Round((Val.ToDecimal(txtAssortGhat.Text) + Val.ToDecimal(txtBGhat.Text)), 3));
            }
            else
            {
                txtBGhatPer.Text = "0";
            }

        }
        private void txtRBakiEditValueChanged(object sender, EventArgs e)
        {
            CalculateMain();
            if (Val.ToDecimal(txtCarat.Text) > 0 && Val.ToDecimal(txtRBaki.Text) > 0)
            {
                txtRBakiPer.Text = Val.ToString(Math.Round((Val.ToDecimal(txtRBaki.Text) * 100) / Val.ToDecimal(txtCarat.Text), 2));
                txtAssortGhat.Text = Val.ToString(Math.Round(Val.ToDecimal(txtCarat.Text) - (m_TotalSumm + (Val.ToDecimal(txtRGhat.Text) + Val.ToDecimal(txtBGhat.Text) + Val.ToDecimal(txtRBaki.Text))), 3));
            }
            else
            {
                txtRBakiPer.Text = "0";
            }
            decimal TotalSumm = 0;
            if (dgvAssortFinal.DataSource != null)
            {
                GridColumn column4 = dgvAssortFinal.Columns["Total"];
                TotalSumm = Val.ToDecimal(column4.SummaryText);
            }

        }
        private void txtMainEditValueChanged(object sender, EventArgs e)
        {
            if (Val.ToDecimal(txtCarat.Text) > 0 && Val.ToDecimal(txtMain.Text) > 0)
            {
                txtMainPer.Text = Val.ToString(Math.Round((Val.ToDecimal(txtMain.Text) * 100) / Val.ToDecimal(txtCarat.Text), 2));
            }
            else
            {
                txtMainPer.Text = "0";
            }
        }
        private void txtAssortGhatEditValueChanged(object sender, EventArgs e)
        {
            if (Val.ToDecimal(txtCarat.Text) > 0 && Val.ToDecimal(txtAssortGhat.Text) > 0)
            {
                txtAssortGhatPer.Text = Val.ToString(Math.Round((Val.ToDecimal(txtAssortGhat.Text) * 100) / Val.ToDecimal(txtCarat.Text), 2));
                txtMainGhat.Text = Val.ToString(Math.Round((Val.ToDecimal(txtAssortGhat.Text) + Val.ToDecimal(txtBGhat.Text)), 3));
            }
            else
            {
                txtAssortGhatPer.Text = "0";
            }
            decimal TotalSumm = 0;
            if (dgvAssortFinal.DataSource != null)
            {
                GridColumn column4 = dgvAssortFinal.Columns["Total"];
                TotalSumm = Val.ToDecimal(column4.SummaryText);
            }
            CalculateMain();
        }
        private void txtMainGhatEditValueChanged(object sender, EventArgs e)
        {
            if (Val.ToDecimal(txtCarat.Text) > 0 && Val.ToDecimal(txtMainGhat.Text) > 0)
            {
                txtMainGhatPer.Text = Val.ToString(Math.Round((Val.ToDecimal(txtMainGhat.Text) * 100) / Val.ToDecimal(txtCarat.Text), 2));
            }
            else
            {
                txtMainGhatPer.Text = "0";
            }
            decimal TotalSumm = 0;
            if (dgvAssortFinal.DataSource != null)
            {
                GridColumn column4 = dgvAssortFinal.Columns["Total"];
                TotalSumm = Val.ToDecimal(column4.SummaryText);
            }
            CalculateMain();
        }
        private void lueProcess_EditValueChanged(object sender, EventArgs e)
        {
            if (lueProcess.EditValue != System.DBNull.Value)
            {
                if (m_dtbSubProcess.Rows.Count > 0)
                {
                    DataTable dtbdetail = m_dtbSubProcess;

                    string strFilter = string.Empty;

                    if (lueProcess.Text != "")
                        strFilter = "process_id = " + lueProcess.EditValue;


                    dtbdetail.DefaultView.RowFilter = strFilter;
                    dtbdetail.DefaultView.ToTable();

                    DataTable dtb = dtbdetail.DefaultView.ToTable();

                    lueSubProcess.Properties.DataSource = dtb;
                    lueSubProcess.Properties.ValueMember = "sub_process_id";
                    lueSubProcess.Properties.DisplayMember = "sub_process_name";
                    lueSubProcess.EditValue = System.DBNull.Value;
                }
            }
        }
        private void btn_Print_Click(object sender, EventArgs e)
        {
            try
            {
                //if (RBtnLocationType.EditValue.ToString() == "1")
                //{
                //    if (RBtnType.SelectedIndex == 0)
                //    {
                //        DataSet DTab_IssueJanged = objAssortFinal.Print_Assort_Final(Val.ToInt32(lueKapan.EditValue), Val.ToInt32(lueCutNo.EditValue), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue), Val.ToInt64(lblLotSRNo.Text), Val.ToInt64(RBtnLocationType.EditValue), Val.DBDate(dtpReceiveDate.Text), Val.DBDate(dtpRateDate.Text));

                //        FrmReportViewer FrmReportViewer = new FrmReportViewer();
                //        foreach (DataTable DTab in DTab_IssueJanged.Tables)
                //            FrmReportViewer.DS.Tables.Add(DTab.Copy());
                //        FrmReportViewer.GroupBy = "";
                //        FrmReportViewer.RepName = "";
                //        FrmReportViewer.RepPara = "";
                //        this.Cursor = Cursors.Default;
                //        FrmReportViewer.AllowSetFormula = true;

                //        //FrmReportViewer.ShowForm_SubReport("CrtPolishGrading_Final_Main", 120, FrmReportViewer.ReportFolder.ACCOUNT);
                //        FrmReportViewer.ShowForm_SubReport("CrtPolishGrading_Final_Main", 120, FrmReportViewer.ReportFolder.FINAL_MAIN);

                //        DTab_IssueJanged = null;
                //        FrmReportViewer.DS.Tables.Clear();
                //        FrmReportViewer.DS.Clear();
                //        FrmReportViewer = null;
                //    }
                //    else if (RBtnType.SelectedIndex == 1)
                //    {
                //        DataSet DTab_IssueJanged = objAssortFinal.Print_Assort_Final_Print2(Val.ToInt32(lueKapan.EditValue), Val.ToInt32(lueCutNo.EditValue), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue), Val.ToInt64(lblLotSRNo.Text), Val.ToInt64(RBtnLocationType.EditValue), Val.DBDate(dtpReceiveDate.Text), Val.DBDate(dtpRateDate.Text));

                //        FrmReportViewer FrmReportViewer = new FrmReportViewer();
                //        foreach (DataTable DTab in DTab_IssueJanged.Tables)
                //            FrmReportViewer.DS.Tables.Add(DTab.Copy());
                //        FrmReportViewer.GroupBy = "";
                //        FrmReportViewer.RepName = "";
                //        FrmReportViewer.RepPara = "";
                //        this.Cursor = Cursors.Default;
                //        FrmReportViewer.AllowSetFormula = true;

                //        //FrmReportViewer.ShowForm_SubReport("CrtPolishGrading_Final_Main_Print2", 120, FrmReportViewer.ReportFolder.ACCOUNT);
                //        FrmReportViewer.ShowForm_SubReport("CrtPolishGrading_Final_OK_Main_Print2", 120, FrmReportViewer.ReportFolder.FINAL_OK_SUB);

                //        DTab_IssueJanged = null;
                //        FrmReportViewer.DS.Tables.Clear();
                //        FrmReportViewer.DS.Clear();
                //        FrmReportViewer = null;
                //    }
                //    else if (RBtnType.SelectedIndex == 2)
                //    {
                //        DataSet DTab_IssueJanged = objAssortFinal.Print_Assort_Final_Print2(Val.ToInt32(lueKapan.EditValue), Val.ToInt32(lueCutNo.EditValue), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue), Val.ToInt64(lblLotSRNo.Text), Val.ToInt64(RBtnLocationType.EditValue), Val.DBDate(dtpReceiveDate.Text), Val.DBDate(dtpRateDate.Text));

                //        FrmReportViewer FrmReportViewer = new FrmReportViewer();
                //        foreach (DataTable DTab in DTab_IssueJanged.Tables)
                //            FrmReportViewer.DS.Tables.Add(DTab.Copy());
                //        FrmReportViewer.GroupBy = "";
                //        FrmReportViewer.RepName = "";
                //        FrmReportViewer.RepPara = "";
                //        this.Cursor = Cursors.Default;
                //        FrmReportViewer.AllowSetFormula = true;

                //        //FrmReportViewer.ShowForm_SubReport("CrtPolishGrading_Final_Main_Print2", 120, FrmReportViewer.ReportFolder.ACCOUNT);
                //        FrmReportViewer.ShowForm_SubReport("CrtPolishGrading_Final_OK_Main_Print3", 120, FrmReportViewer.ReportFolder.FINAL_OK_SUB);

                //        DTab_IssueJanged = null;
                //        FrmReportViewer.DS.Tables.Clear();
                //        FrmReportViewer.DS.Clear();
                //        FrmReportViewer = null;
                //    }
                //}
                //else if (RBtnLocationType.EditValue.ToString() == "2")
                //{
                if (RBtnType.SelectedIndex == 0)
                {
                    DataSet DTab_IssueJanged = objAssortFinal.Print_Assort_Minus2_Mumbai(Val.ToInt32(lueKapan.EditValue), Val.ToInt32(lueCutNo.EditValue), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue), Val.ToInt64(lblLotSRNo.Text), Val.ToInt64(RBtnLocationType.EditValue), Val.DBDate(dtpReceiveDate.Text), Val.DBDate(dtpRateDate.Text), Val.ToInt(2));


                    FrmReportViewer FrmReportViewer = new FrmReportViewer();
                    foreach (DataTable DTab in DTab_IssueJanged.Tables)
                        FrmReportViewer.DS.Tables.Add(DTab.Copy());
                    FrmReportViewer.GroupBy = "";
                    FrmReportViewer.RepName = "";
                    FrmReportViewer.RepPara = "";
                    this.Cursor = Cursors.Default;
                    FrmReportViewer.AllowSetFormula = true;

                    //FrmReportViewer.ShowForm_SubReport("CrtPolishGrading_Final_Main_Mumbai", 120, FrmReportViewer.ReportFolder.ACCOUNT);
                    FrmReportViewer.ShowForm_SubReport("CrtPolishGrading_Minus2_Mumbai", 120, FrmReportViewer.ReportFolder.MINUS2_MUMBAI_ASSORTMENT);

                    DTab_IssueJanged = null;
                    FrmReportViewer.DS.Tables.Clear();
                    FrmReportViewer.DS.Clear();
                    FrmReportViewer = null;
                }
                //if (RBtnType.SelectedIndex == 1)
                //{
                //    DataSet DTab_IssueJanged = objAssortFinal.Print_Assort_Final_Mumbai(Val.ToInt32(lueKapan.EditValue), Val.ToInt32(lueCutNo.EditValue), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue), Val.ToInt64(lblLotSRNo.Text), Val.ToInt64(RBtnLocationType.EditValue), Val.DBDate(dtpReceiveDate.Text), Val.DBDate(dtpRateDate.Text));


                //    FrmReportViewer FrmReportViewer = new FrmReportViewer();
                //    foreach (DataTable DTab in DTab_IssueJanged.Tables)
                //        FrmReportViewer.DS.Tables.Add(DTab.Copy());
                //    FrmReportViewer.GroupBy = "";
                //    FrmReportViewer.RepName = "";
                //    FrmReportViewer.RepPara = "";
                //    this.Cursor = Cursors.Default;
                //    FrmReportViewer.AllowSetFormula = true;

                //    //FrmReportViewer.ShowForm_SubReport("CrtPolishGrading_Final_Main_Mumbai", 120, FrmReportViewer.ReportFolder.ACCOUNT);
                //    FrmReportViewer.ShowForm_SubReport("CrtPolishGrading_Final_Main_Mumbai_Print2", 120, FrmReportViewer.ReportFolder.FINAL_MAIN_MUMBAI);

                //    DTab_IssueJanged = null;
                //    FrmReportViewer.DS.Tables.Clear();
                //    FrmReportViewer.DS.Clear();
                //    FrmReportViewer = null;
                //}
                //}
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                dgvAssortFinal.Columns.Clear();

                //MFGJangedIsuRecAssortment objMFGJangedIsuRec = new MFGJangedIsuRecAssortment();
                //MFGProcessIssueProperty objMFGProcessIssueProperty = new MFGProcessIssueProperty();
                ////objMFGProcessIssueProperty.rough_cut_id = Val.ToInt(lueCutNo.EditValue);
                ////objMFGProcessIssueProperty.kapan_id = Val.ToInt(lueKapan.EditValue);
                //objMFGProcessIssueProperty.rough_cut_id = Val.ToInt(0);
                //objMFGProcessIssueProperty.kapan_id = Val.ToInt(0);
                //objMFGProcessIssueProperty.flag = Val.ToInt(3);
                //objMFGProcessIssueProperty.process_id = Val.ToInt(lueProcess.EditValue);
                //objMFGProcessIssueProperty.sub_process_id = Val.ToInt(lueSubProcess.EditValue);
                //objMFGProcessIssueProperty.temp_sieve_name = Val.ToString("");
                //objMFGProcessIssueProperty.temp_purity_name = Val.ToString("");
                //objMFGProcessIssueProperty.from_date = Val.DBDate(dtpReceiveDate.Text);
                //objMFGProcessIssueProperty.to_date = Val.DBDate(dtpReceiveDate.Text);
                ////objMFGProcessIssueProperty.location_id = Val.ToInt32(GlobalDec.gEmployeeProperty.location_id);

                //if (RBtnLocationType.EditValue.ToString() == "1")
                //{
                //    objMFGProcessIssueProperty.location_id = Val.ToInt32(1);
                //}
                //else
                //{
                //    objMFGProcessIssueProperty.location_id = Val.ToInt32(2);
                //}

                //DtAssortment = objMFGJangedIsuRec.GetPendingStock(objMFGProcessIssueProperty);

                FrmMFGAssortmentFinalStock FrmMFGAssortmentFinalStock = new FrmMFGAssortmentFinalStock();
                FrmMFGAssortmentFinalStock.FrmMFGMinus2AssortmentMumbai = this;
                FrmMFGAssortmentFinalStock.DTab = DtAssortment;
                FrmMFGAssortmentFinalStock.ShowForm(this, Val.ToInt32(RBtnLocationType.EditValue));
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
            }
        }
        private void RBtnLocationType_EditValueChanged(object sender, EventArgs e)
        {
            lueSieve.SetEditValue("13,16,17");
            lueAssort.SetEditValue("514,515,516,517,518,519,520,521,522,523,524,1671,1672,1673,1674,1675,1676,1677,398,400,401,402,403,404,1678,1679,1810,1811,1812,1813,1814,1815,1816");
        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (GlobalDec.gEmployeeProperty.user_name != "RIKITA")
            {
                Global.Message("Don't have permission...Please Contact to Administrator...");
                return;
            }
            if (Val.ToInt(lblLotSRNo.Text) != 0)
            {
                DialogResult result = MessageBox.Show("Do you want to Delete Assortment -2 Mumbai data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result != DialogResult.Yes)
                {
                    btnSave.Enabled = true;
                    return;
                }
                IntRes = 0;
                MFGAssortFinalLotting MFGAssortFinal = new MFGAssortFinalLotting();
                MFGAssortFirst MFGAssortReceive = new MFGAssortFirst();
                MFGAssortFinal_LottingProperty objMFGAssortFinalLottingProperty = new MFGAssortFinal_LottingProperty();

                objMFGAssortFinalLottingProperty.manager_id = Val.ToInt(0);
                objMFGAssortFinalLottingProperty.employee_id = Val.ToInt(0);
                objMFGAssortFinalLottingProperty.process_id = Val.ToInt(lueProcess.EditValue);
                objMFGAssortFinalLottingProperty.sub_process_id = Val.ToInt(lueSubProcess.EditValue);
                objMFGAssortFinalLottingProperty.kapan_id = Val.ToInt(lueKapan.EditValue);
                objMFGAssortFinalLottingProperty.lot_id = Val.ToInt64(txtLotId.Text);
                objMFGAssortFinalLottingProperty.rough_cut_id = Val.ToInt(lueCutNo.EditValue);
                objMFGAssortFinalLottingProperty.temp_sieve_name = lueSieve.Text.ToString();
                objMFGAssortFinalLottingProperty.flag = Val.ToInt(4);
                objMFGAssortFinalLottingProperty.Del_lot_srno = Val.ToInt64(lblLotSRNo.Text);
                objMFGAssortFinalLottingProperty.assort_total_carat = Val.ToDecimal(txtCarat.Text);
                objMFGAssortFinalLottingProperty.form_id = m_numForm_id;

                IntRes = MFGAssortFinal.GetDeleteFinalLot_ID(objMFGAssortFinalLottingProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                if (IntRes > 0)
                {
                    Global.Confirm("Assortment -2 Mumbai Data Deleted Succesfully");
                    ClearDetails();
                    btnSave.Enabled = true;
                }
                else
                {
                    Global.Confirm("Error In Assortment -2 Mumbai Data");
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

        #region GridEvents
        private void dgvAssortFinal_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            try
            {
                DataTable dtAmount = new DataTable();
                dtAmount = (DataTable)grdAssortFinal.DataSource;
                decimal percentage = 0;
                decimal totcarat = 0;
                string column = "";
                for (int j = 0; j <= dtAmount.Columns.Count - 1; j++)
                {
                    if (dtAmount.Columns[j].ToString().Contains("per(%)"))
                    {
                        column = dtAmount.Columns[j].ToString();
                    }
                    if (dtAmount.Columns[j].ColumnName.Contains("carat"))
                    {
                        totcarat = dtAmount.AsEnumerable().Sum(x => Val.ToDecimal(x[dtAmount.Columns[j]]));
                    }
                    //if (dtAmount.Columns[j].ToString().Contains("amount"))
                    //{
                    //    amount = dtAmount.AsEnumerable().Sum(x => Math.Round(Val.ToDecimal(x[dtAmount.Columns[j]]), 0));
                    //}
                    //if (dtAmount.Columns[j].ToString().Contains("rate"))
                    //{
                    //    column1 = dtAmount.Columns[j].ToString();
                    //    amount = 0;
                    //}
                    //if (Val.ToDecimal(amount) > 0 && Val.ToDecimal(totcarat) > 0)
                    //{
                    //    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == column1)
                    //    {
                    //        rate = Math.Round(amount / totcarat, 3);
                    //        if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    //            e.TotalValue = rate;
                    //        column = "";
                    //        carat = 0;
                    //        amount = 0;
                    //    }
                    //}
                    if (totcarat > 0 && Val.ToDecimal(txtCarat.Text) > 0)
                    {
                        if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == column)
                        {
                            percentage = Math.Round(totcarat * 100 / Val.ToDecimal(txtCarat.Text), 3);
                            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                                e.TotalValue = percentage;
                            column = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        private void dgvAssortFinal_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (Val.ToString(dgvAssortFinal.GetRowCellValue(e.RowHandle, "type")) == "A")
            {
                e.Appearance.BeginUpdate();
                e.Appearance.BackColor = Color.FromArgb(210, 170, 162);
            }
            if (Val.ToString(dgvAssortFinal.GetRowCellValue(e.RowHandle, "type")) == "B")
            {
                e.Appearance.BeginUpdate();
                e.Appearance.BackColor = Color.FromArgb(215, 215, 173);
            }
            if (Val.ToString(dgvAssortFinal.GetRowCellValue(e.RowHandle, "type")) == "C")
            {
                e.Appearance.BeginUpdate();
                e.Appearance.BackColor = Color.FromArgb(184, 205, 187);
            }
            if (Val.ToString(dgvAssortFinal.GetRowCellValue(e.RowHandle, "type")) == "D")
            {
                e.Appearance.BeginUpdate();
                e.Appearance.BackColor = Color.FromArgb(225, 249, 229);
            }
            if (Val.ToString(dgvAssortFinal.GetRowCellValue(e.RowHandle, "type")) == "E")
            {
                e.Appearance.BeginUpdate();
                e.Appearance.BackColor = Color.FromArgb(225, 227, 251);
            }
            if (Val.ToString(dgvAssortFinal.GetRowCellValue(e.RowHandle, "type")) == "F")
            {
                e.Appearance.BeginUpdate();
                e.Appearance.BackColor = Color.FromArgb(220, 220, 221);
            }
            if (Val.ToString(dgvAssortFinal.GetRowCellValue(e.RowHandle, "type")) == "G")
            {
                e.Appearance.BeginUpdate();
                e.Appearance.BackColor = Color.FromArgb(229, 219, 234);
            }
            if (Val.ToString(dgvAssortFinal.GetRowCellValue(e.RowHandle, "type")) == "H")
            {
                e.Appearance.BeginUpdate();
                e.Appearance.BackColor = Color.FromArgb(166, 120, 111);
            }
            if (Val.ToString(dgvAssortFinal.GetRowCellValue(e.RowHandle, "type")) == "I")
            {
                e.Appearance.BeginUpdate();
                e.Appearance.BackColor = Color.FromArgb(191, 177, 198);
            }
            if (Val.ToString(dgvAssortFinal.GetRowCellValue(e.RowHandle, "type")) == "J")
            {
                e.Appearance.BeginUpdate();
                e.Appearance.BackColor = Color.FromArgb(173, 131, 169);
            }
            if (Val.ToString(dgvAssortFinal.GetRowCellValue(e.RowHandle, "type")) == "K")
            {
                e.Appearance.BeginUpdate();
                e.Appearance.BackColor = Color.FromArgb(123, 165, 172);
            }
            if (Val.ToString(dgvAssortFinal.GetRowCellValue(e.RowHandle, "type")) == "L")
            {
                e.Appearance.BeginUpdate();
                e.Appearance.BackColor = Color.FromArgb(180, 180, 142);
            }
            if (Val.ToString(dgvAssortFinal.GetRowCellValue(e.RowHandle, "type")) == "M")
            {
                e.Appearance.BeginUpdate();
                e.Appearance.BackColor = Color.FromArgb(214, 244, 233);
            }
            if (Val.ToString(dgvAssortFinal.GetRowCellValue(e.RowHandle, "type")) == "N")
            {
                e.Appearance.BeginUpdate();
                e.Appearance.BackColor = Color.FromArgb(197, 151, 192);
            }
            if (Val.ToString(dgvAssortFinal.GetRowCellValue(e.RowHandle, "type")) == "O")
            {
                e.Appearance.BeginUpdate();
                e.Appearance.BackColor = Color.FromArgb(190, 105, 115);
            }
            if (Val.ToString(dgvAssortFinal.GetRowCellValue(e.RowHandle, "type")) == "P")
            {
                e.Appearance.BeginUpdate();
                e.Appearance.BackColor = Color.FromArgb(176, 155, 189);
            }
            if (Val.ToString(dgvAssortFinal.GetRowCellValue(e.RowHandle, "type")) == "Q")
            {
                e.Appearance.BeginUpdate();
                e.Appearance.BackColor = Color.FromArgb(172, 132, 142);
            }
            if (Val.ToString(dgvAssortFinal.GetRowCellValue(e.RowHandle, "type")) == "R")
            {
                e.Appearance.BeginUpdate();
                e.Appearance.BackColor = Color.FromArgb(249, 185, 200);
            }
            if (Val.ToString(dgvAssortFinal.GetRowCellValue(e.RowHandle, "type")) == "S")
            {
                e.Appearance.BeginUpdate();
                e.Appearance.BackColor = Color.FromArgb(210, 176, 233);
            }
            if (Val.ToString(dgvAssortFinal.GetRowCellValue(e.RowHandle, "type")) == "T")
            {
                e.Appearance.BeginUpdate();
                e.Appearance.BackColor = Color.FromArgb(173, 155, 186);
            }
            if (Val.ToString(dgvAssortFinal.GetRowCellValue(e.RowHandle, "type")) == "U")
            {
                e.Appearance.BeginUpdate();
                e.Appearance.BackColor = Color.FromArgb(216, 176, 185);
            }
        }
        private void dgvAssortGroupTotal_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (Val.ToString(dgvAssortGroupTotal.GetRowCellValue(e.RowHandle, "type")) == "A")
            {
                e.Appearance.BeginUpdate();
                e.Appearance.BackColor = Color.FromArgb(210, 170, 162);
            }
            if (Val.ToString(dgvAssortGroupTotal.GetRowCellValue(e.RowHandle, "type")) == "B")
            {
                e.Appearance.BeginUpdate();
                e.Appearance.BackColor = Color.FromArgb(215, 215, 173);
            }
            if (Val.ToString(dgvAssortGroupTotal.GetRowCellValue(e.RowHandle, "type")) == "C")
            {
                e.Appearance.BeginUpdate();
                e.Appearance.BackColor = Color.FromArgb(184, 205, 187);
            }
            if (Val.ToString(dgvAssortGroupTotal.GetRowCellValue(e.RowHandle, "type")) == "D")
            {
                e.Appearance.BeginUpdate();
                e.Appearance.BackColor = Color.FromArgb(225, 249, 229);
            }
            if (Val.ToString(dgvAssortGroupTotal.GetRowCellValue(e.RowHandle, "type")) == "E")
            {
                e.Appearance.BeginUpdate();
                e.Appearance.BackColor = Color.FromArgb(225, 227, 251);
            }
            if (Val.ToString(dgvAssortGroupTotal.GetRowCellValue(e.RowHandle, "type")) == "F")
            {
                e.Appearance.BeginUpdate();
                e.Appearance.BackColor = Color.FromArgb(220, 220, 221);
            }
            if (Val.ToString(dgvAssortGroupTotal.GetRowCellValue(e.RowHandle, "type")) == "G")
            {
                e.Appearance.BeginUpdate();
                e.Appearance.BackColor = Color.FromArgb(229, 219, 234);
            }
            if (Val.ToString(dgvAssortGroupTotal.GetRowCellValue(e.RowHandle, "type")) == "H")
            {
                e.Appearance.BeginUpdate();
                e.Appearance.BackColor = Color.FromArgb(166, 120, 111);
            }
            if (Val.ToString(dgvAssortGroupTotal.GetRowCellValue(e.RowHandle, "type")) == "I")
            {
                e.Appearance.BeginUpdate();
                e.Appearance.BackColor = Color.FromArgb(191, 177, 198);
            }
            if (Val.ToString(dgvAssortGroupTotal.GetRowCellValue(e.RowHandle, "type")) == "J")
            {
                e.Appearance.BeginUpdate();
                e.Appearance.BackColor = Color.FromArgb(173, 131, 169);
            }
            if (Val.ToString(dgvAssortGroupTotal.GetRowCellValue(e.RowHandle, "type")) == "K")
            {
                e.Appearance.BeginUpdate();
                e.Appearance.BackColor = Color.FromArgb(123, 165, 172);
            }
            if (Val.ToString(dgvAssortGroupTotal.GetRowCellValue(e.RowHandle, "type")) == "L")
            {
                e.Appearance.BeginUpdate();
                e.Appearance.BackColor = Color.FromArgb(180, 180, 142);
            }
            if (Val.ToString(dgvAssortGroupTotal.GetRowCellValue(e.RowHandle, "type")) == "M")
            {
                e.Appearance.BeginUpdate();
                e.Appearance.BackColor = Color.FromArgb(214, 244, 233);
            }
            if (Val.ToString(dgvAssortGroupTotal.GetRowCellValue(e.RowHandle, "type")) == "N")
            {
                e.Appearance.BeginUpdate();
                e.Appearance.BackColor = Color.FromArgb(197, 151, 192);
            }
            if (Val.ToString(dgvAssortGroupTotal.GetRowCellValue(e.RowHandle, "type")) == "O")
            {
                e.Appearance.BeginUpdate();
                e.Appearance.BackColor = Color.FromArgb(190, 105, 115);
            }
            if (Val.ToString(dgvAssortGroupTotal.GetRowCellValue(e.RowHandle, "type")) == "P")
            {
                e.Appearance.BeginUpdate();
                e.Appearance.BackColor = Color.FromArgb(176, 155, 189);
            }
            if (Val.ToString(dgvAssortGroupTotal.GetRowCellValue(e.RowHandle, "type")) == "Q")
            {
                e.Appearance.BeginUpdate();
                e.Appearance.BackColor = Color.FromArgb(172, 132, 142);
            }
            if (Val.ToString(dgvAssortGroupTotal.GetRowCellValue(e.RowHandle, "type")) == "R")
            {
                e.Appearance.BeginUpdate();
                e.Appearance.BackColor = Color.FromArgb(249, 185, 200);
            }
            if (Val.ToString(dgvAssortGroupTotal.GetRowCellValue(e.RowHandle, "type")) == "S")
            {
                e.Appearance.BeginUpdate();
                e.Appearance.BackColor = Color.FromArgb(210, 176, 233);
            }
            if (Val.ToString(dgvAssortGroupTotal.GetRowCellValue(e.RowHandle, "type")) == "T")
            {
                e.Appearance.BeginUpdate();
                e.Appearance.BackColor = Color.FromArgb(173, 155, 186);
            }
            if (Val.ToString(dgvAssortGroupTotal.GetRowCellValue(e.RowHandle, "type")) == "U")
            {
                e.Appearance.BeginUpdate();
                e.Appearance.BackColor = Color.FromArgb(216, 176, 185);
            }
        }

        #endregion

        #endregion

        #region Functions
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (txtCarat.Text.ToString() == "" || txtCarat.Text.ToString() == "0")
                {
                    lstError.Add(new ListError(12, "Carat"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtCarat.Focus();
                    }
                }
                if (lueCutNo.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Cut No"));
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

                if (lueProcess.Text == "")
                {
                    lstError.Add(new ListError(13, "Process"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueProcess.Focus();
                    }
                }
                if (lueSubProcess.Text == "")
                {
                    lstError.Add(new ListError(13, "Sub Process"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueSubProcess.Focus();
                    }
                }
                if (RBtnLocationType.SelectedIndex == 0)
                {
                    if (GlobalDec.gEmployeeProperty.department_name != "MUMBAI ASSORTMENT")
                    {
                        lstError.Add(new ListError(5, "Department Not In Surat Location..Please Select Correct Department"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                        }
                    }
                }
                // Add By Praful On 17072020
                //DataTable DTab_Data = (DataTable)grdAssortFirst.DataSource;                
            }
            catch (Exception ex)
            {
                lstError.Add(new ListError(ex));
            }
            return (!(BLL.General.ShowErrors(lstError)));
        }
        private void CalculateMain()
        {
            m_TotalSumm = 0;
            if (dgvAssortFinal.DataSource != null)
            {
                GridColumn column4 = dgvAssortFinal.Columns["Total"];
                m_TotalSumm = Val.ToDecimal(column4.SummaryText);
            }
            txtAssortGhat.Text = Val.ToString(Math.Round(Val.ToDecimal(txtCarat.Text) - (m_TotalSumm + (Val.ToDecimal(txtRGhat.Text) + Val.ToDecimal(txtBGhat.Text) + Val.ToDecimal(txtRBaki.Text))), 3));
            //txtMainGhat.Text = Val.ToString(Math.Round((Val.ToDecimal(txtAssortGhat.Text) + Val.ToDecimal(txtBGhat.Text)), 3));
            //txtMainGhat.Text = Val.ToString(Math.Round((Val.ToDecimal(txtAssortGhat.Text) + Val.ToDecimal(txtBGhat.Text)), 3));
            txtMain.Text = Val.ToString(Math.Round((Val.ToDecimal(txtAssortGhat.Text) + Val.ToDecimal(txtBGhat.Text) + Val.ToDecimal(txtRBaki.Text) + Val.ToDecimal(txtRGhat.Text) + m_TotalSumm), 3));

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
                            dgvAssortFinal.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvAssortFinal.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvAssortFinal.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvAssortFinal.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvAssortFinal.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvAssortFinal.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvAssortFinal.ExportToCsv(Filepath);
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
        public void GetStockPendingData(DataTable Stock_Data)
        {
            try
            {
                DataTable DTab_Stk = Stock_Data.Copy();

                lueKapan.EditValue = Val.ToInt64(DTab_Stk.Rows[0]["kapan_id"]);
                lueCutNo.EditValue = Val.ToInt64(DTab_Stk.Rows[0]["rough_cut_id"]);
                txtCarat.Text = Val.ToDecimal(DTab_Stk.Rows[0]["carat"]).ToString();
                txtLotId.Text = Val.ToInt64(DTab_Stk.Rows[0]["lot_id"]).ToString();
                //string quality_name = Val.ToString(DTab_Stk.Rows[0]["quality_name"]);
                //string purity_name = string.Empty;
                //DataTable DeTemp = new DataTable();
                //var temp1 = quality_name;
                //string StrTransactionType = "";
                //string[] array = temp1.Split(',');
                //if (!string.IsNullOrEmpty(temp1))
                //{
                //    foreach (var item in array)
                //    {
                //        StrTransactionType += "'" + item + "',";
                //    }
                //    StrTransactionType = StrTransactionType.Remove(StrTransactionType.Length - 1);
                //}

                //if (StrTransactionType.Length > 0)
                //    DeTemp = DTabQuality.Select("quality_name in (" + StrTransactionType + ")").CopyToDataTable();

                //if (DeTemp.Rows.Count > 0)
                //{
                //    foreach (DataRow Dr in DeTemp.Rows)
                //    {
                //        purity_name += "" + Dr["quality_id"] + ",";
                //    }
                //}
                //ListQuality.SetEditValue(purity_name);
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
            }
        }
        public void GetStockData(DataTable Stock_Data)
        {
            try
            {
                DataTable DTab_Stk = Stock_Data.Copy();

                //decimal carat = Val.ToInt64(DTab_Stk.Compute("Sum(carat)", string.Empty));

                //lblOsCarat.Text = "0";
                //decimal Total_carat = 0;
                lueAssort.SetEditValue(0);
                lueSieve.SetEditValue(0);
                lblLotSRNo.Text = Val.ToString(Val.ToDecimal(DTab_Stk.Rows[0]["lot_srno"]));
                lueKapan.EditValue = Val.ToInt64(DTab_Stk.Rows[0]["kapan_id"]);
                lueCutNo.EditValue = Val.ToInt64(DTab_Stk.Rows[0]["rough_cut_id"]);
                txtCarat.Text = Val.ToDecimal(DTab_Stk.Rows[0]["assort_total_carat"]).ToString();
                dtpReceiveDate.Text = Val.DBDate(DTab_Stk.Rows[0]["receive_date"].ToString());
                txtLotId.Text = Val.ToInt64(DTab_Stk.Rows[0]["lot_id"]).ToString();

                RBtnLocationType.SelectedIndex = 1;
                lueSieve.SetEditValue("13,16,17");
                lueAssort.SetEditValue("514,515,516,517,518,519,520,521,522,523,524,1671,1672,1673,1674,1675,1676,1677,398,400,401,402,403,404,1678,1679,1810,1811,1812,1813,1814,1815,1816");

                dtTemp = new DataTable();
                dgvAssortFinal.Columns.Clear();

                DataTable dtnew = objAssortFinal.Minus2Mumbai_Assort_GetData(Val.ToString(lueAssort.EditValue), Val.ToString(lueSieve.EditValue), Val.ToInt32(lueKapan.EditValue), Val.ToInt32(lueCutNo.EditValue), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue), Val.ToString(lueSieve.Text), Val.ToInt64(lblLotSRNo.Text), Val.ToInt64(RBtnLocationType.EditValue), Val.DBDate(dtpRateDate.Text), Val.ToInt(2));

                if (dtnew.Rows.Count > 0)
                {
                    pivot pt = new pivot(dtnew);

                    dtTemp = pt.PivotDataSuperPlusAssortment(new string[] { "type", "assort_id", "assort" }, new string[] { "carat", "per(%)", "rate", "amount", }, new AggregateFunction[] { AggregateFunction.Sum, AggregateFunction.Sum, AggregateFunction.Sum, AggregateFunction.Sum }, new string[] { "sieve" });
                    DataColumn Total = new System.Data.DataColumn("Total", typeof(System.Decimal));
                    DataColumn Percentage = new System.Data.DataColumn("Total%", typeof(System.Decimal));
                    DataColumn Ttlamount = new System.Data.DataColumn("Total_AMT", typeof(System.Decimal));
                    DataColumn TtlFlag = new System.Data.DataColumn("Flag", typeof(System.Int32));
                    Total.DefaultValue = "0.000";
                    Percentage.DefaultValue = "0.000";
                    Ttlamount.DefaultValue = "0";
                    dtTemp.Columns.Add(Total);
                    dtTemp.Columns.Add(Percentage);
                    dtTemp.Columns.Add(Ttlamount);
                    dtTemp.Columns.Add(TtlFlag);

                    int index = 3;

                    for (int j = 2; j <= dtTemp.Columns.Count - 1; j++)
                    {
                        if (dtTemp.Columns[j].ToString().Contains("carat"))
                        {
                            dtTemp.Columns[j].SetOrdinal(index);
                            index++;

                        }
                    }
                    for (int j = 0; j <= dtTemp.Columns.Count - 1; j++)
                    {
                        if (dtTemp.Columns[j].ToString().Contains("rate"))
                        {
                            dtTemp.Columns[j].SetOrdinal(index);
                            index++;
                        }
                    }
                    for (int j = 0; j <= dtTemp.Columns.Count - 1; j++)
                    {
                        if (dtTemp.Columns[j].ToString().Contains("amount"))
                        {
                            dtTemp.Columns[j].SetOrdinal(index);
                            index++;
                        }
                    }
                    index = 0;

                    grdAssortFinal.DataSource = dtTemp;
                    //dgvAssortFinal.Columns["type"].Visible = false;
                    dgvAssortFinal.Columns["type"].OptionsColumn.ReadOnly = true;
                    dgvAssortFinal.Columns["type"].OptionsColumn.AllowFocus = false;
                    dgvAssortFinal.Columns["assort_id"].Visible = false;
                    dgvAssortFinal.Columns["assort"].OptionsColumn.ReadOnly = true;
                    dgvAssortFinal.Columns["assort"].OptionsColumn.AllowFocus = false;
                    dgvAssortFinal.Columns["Total"].OptionsColumn.ReadOnly = true;
                    dgvAssortFinal.Columns["Total"].OptionsColumn.AllowFocus = false;
                    dgvAssortFinal.Columns["Total%"].OptionsColumn.ReadOnly = true;
                    dgvAssortFinal.Columns["Total%"].OptionsColumn.AllowFocus = false;
                    dgvAssortFinal.Columns["Total%"].Visible = false;
                    dgvAssortFinal.Columns["assort"].Caption = "#";
                    dgvAssortFinal.Columns["assort"].Fixed = FixedStyle.Left;
                    dgvAssortFinal.Columns["assort"].Caption = "#";
                    decimal Total_Tot;
                    decimal Total_Amt;

                    for (int i = 0; i <= dtTemp.Rows.Count - 1; i++)
                    {
                        Total_Tot = 0;
                        Total_Amt = 0;
                        for (int j = 0; j <= dtTemp.Columns.Count - 1; j++)
                        {
                            if (dtTemp.Columns[j].ToString().Contains("per(%)"))
                            {
                                dgvAssortFinal.Columns[j].OptionsColumn.AllowEdit = false;
                                dgvAssortFinal.Columns[j].Visible = false;

                            }
                            if (dtTemp.Columns[j].ToString().Contains("carat"))
                            {
                                Total_Tot = Total_Tot + Val.ToDecimal(dtTemp.Rows[i][j].ToString());
                                string[] col = dtTemp.Columns[j].ToString().Split('_');
                                string currcol = "";
                                currcol = col[1] + "_" + col[2];
                                dgvAssortFinal.Columns[j].Caption = currcol;
                                //dgvAssortFinal.Columns[dtTemp.Columns[j].ToString()].Width = 150;
                            }
                            if (dtTemp.Columns[j].ToString().Contains("rate"))
                            {
                                string[] col = dtTemp.Columns[j].ToString().Split('_');
                                string currcol = "";
                                currcol = col[1] + "_" + col[2];
                                dgvAssortFinal.Columns[j].Caption = currcol;
                                // dgvAssortFinal.Columns[dtTemp.Columns[j].ToString()].Width = 150;
                            }
                            if (dtTemp.Columns[j].ToString().Contains("amount"))
                            {
                                Total_Amt = Total_Amt + Val.ToDecimal(dtTemp.Rows[i][j].ToString());
                                string[] col = dtTemp.Columns[j].ToString().Split('_');
                                string currcol = "";
                                currcol = col[1] + "_" + col[2];
                                dgvAssortFinal.Columns[j].Caption = currcol;
                                // dgvAssortFinal.Columns[dtTemp.Columns[j].ToString()].Width = 150;
                            }
                            if (dtTemp.Columns[j].ColumnName.Contains("Total"))
                            {
                                decimal total = Total_Tot;
                                dtTemp.Rows[i][j] = total;
                            }
                            if (dtTemp.Columns[j].ColumnName.Contains("Total_AMT"))
                            {
                                dgvAssortFinal.Columns[j].OptionsColumn.AllowEdit = false;
                                decimal total_Amt = Total_Amt;
                                dtTemp.Rows[i][j] = total_Amt;
                            }
                            if (dtTemp.Columns[j].ColumnName.Contains("Flag"))
                            {
                                dgvAssortFinal.Columns[j].OptionsColumn.AllowEdit = false;
                                dgvAssortFinal.Columns[j].Visible = false;
                            }
                        }
                        for (int j = 0; j <= dtTemp.Columns.Count - 1; j++)
                        {
                            if (dtTemp.Columns[j].ToString().Contains("rate"))
                            {
                                dgvAssortFinal.Columns[j].OptionsColumn.AllowEdit = false;
                                dgvAssortFinal.Columns[j].Visible = true;
                                //dgvAssortFinal.Columns[dtTemp.Columns[j].ToString()].VisibleIndex = 3;
                            }
                        }
                        for (int j = 0; j <= dtTemp.Columns.Count - 1; j++)
                        {
                            if (dtTemp.Columns[j].ToString().Contains("amount"))
                            {
                                dgvAssortFinal.Columns[j].OptionsColumn.AllowEdit = false;
                                dgvAssortFinal.Columns[j].Visible = true;
                                //dgvAssortFinal.Columns[dtTemp.Columns[j].ToString()].VisibleIndex = 5;
                            }
                        }
                    }
                    for (int i = 0; i <= dtTemp.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j <= dtTemp.Columns.Count - 1; j++)
                        {
                            if (dtTemp.Columns[j].ToString().Contains("carat"))
                            {
                                string carat = dtTemp.Columns[j].ToString();
                                GridColumn column1 = dgvAssortFinal.Columns[carat];
                                dgvAssortFinal.Columns[carat].SummaryItem.DisplayFormat = "{0:n3}";
                                column1.SummaryItem.SummaryType = SummaryItemType.Sum;
                            }

                            if (dtTemp.Columns[j].ToString().Contains("rate"))
                            {
                                string rate = dtTemp.Columns[j].ToString();
                                GridColumn column2 = dgvAssortFinal.Columns[rate];
                                dgvAssortFinal.Columns[rate].SummaryItem.DisplayFormat = " {0:n2}";
                                column2.SummaryItem.SummaryType = SummaryItemType.Custom;
                            }
                            if (dtTemp.Columns[j].ToString().Contains("amount"))
                            {
                                string amt = dtTemp.Columns[j].ToString();
                                GridColumn column3 = dgvAssortFinal.Columns[amt];
                                dgvAssortFinal.Columns[amt].SummaryItem.DisplayFormat = " {0:n0}";
                                column3.SummaryItem.SummaryType = SummaryItemType.Sum;
                            }
                            if (dtTemp.Columns[j].ToString().Contains("per(%)"))
                            {
                                string Per = dtTemp.Columns[j].ToString();
                                GridColumn column4 = dgvAssortFinal.Columns[Per];
                                dgvAssortFinal.Columns[Per].SummaryItem.DisplayFormat = " {0:n2}";
                                column4.SummaryItem.SummaryType = SummaryItemType.Custom;
                            }
                            if (dtTemp.Columns[j].ColumnName.Contains("Total"))
                            {
                                string total = dtTemp.Columns[j].ToString();
                                GridColumn column5 = dgvAssortFinal.Columns[total];
                                dgvAssortFinal.Columns[total].SummaryItem.DisplayFormat = "{0:n3}";
                                column5.SummaryItem.SummaryType = SummaryItemType.Sum;
                            }
                            if (dtTemp.Columns[j].ColumnName.Contains("Total_AMT"))
                            {
                                string totalamt = dtTemp.Columns[j].ToString();
                                GridColumn column6 = dgvAssortFinal.Columns[totalamt];
                                dgvAssortFinal.Columns[totalamt].SummaryItem.DisplayFormat = "{0:n3}";
                                column6.SummaryItem.SummaryType = SummaryItemType.Sum;
                            }
                        }
                        break;
                    }
                }
                if (Val.ToDecimal(txtCarat.Text) > 0)
                {
                    //decimal Total_Amount = Val.ToDecimal(dtnew.Compute("Sum(carat)", string.Empty)) + Val.ToDecimal(dtnew.Rows[0]["rghat"]) + Val.ToDecimal(dtnew.Rows[0]["bghat"]) + Val.ToDecimal(dtnew.Rows[0]["rbaki"]) + Val.ToDecimal(dtnew.Rows[0]["assortghat"]);
                    //txtCarat.Text = Total_Amount.ToString(); //Val.ToString(Val.ToDecimal(dtnew.Rows[0]["carat"])).Sum();
                    txtBGhat.Text = Val.ToString(Val.ToDecimal(dtnew.Rows[0]["bghat"]));
                    txtAssortGhat.Text = Val.ToString(Val.ToDecimal(dtnew.Rows[0]["assortghat"]));
                    txtRBaki.Text = Val.ToString(Val.ToDecimal(dtnew.Rows[0]["rbaki"]));
                    txtMainGhat.Text = Val.ToString(Val.ToDecimal(dtnew.Rows[0]["mainghat"]));
                    //txtLotId.Text = Val.ToString(Val.ToDecimal(dtnew.Rows[0]["from_lot_id"]));
                    txtRGhat.Text = Val.ToString(Val.ToDecimal(dtnew.Rows[0]["rghat"]));
                    //btnSave.Enabled = false;
                }
                //count = 0;
                dgvAssortFinal.OptionsView.ShowFooter = true;
                dgvAssortFinal.BestFitColumns();
                panelControl1.Enabled = true;
                FillSummaryGrid();
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
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

        #region Key Press Event

        private void txtRGhat_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txtRGhatPer_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txtBGhat_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txtBGhatPer_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txtMainGhat_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txtMainGhatPer_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txtAssortGhat_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txtAssortGhatPer_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txtRBaki_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txtRBakiPer_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txtMain_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txtMainPer_KeyPress(object sender, KeyPressEventArgs e)
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
        #endregion
        private bool Validate_PopUp()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();

            try
            {
                if (lueKapan.Text == "")
                {
                    lstError.Add(new ListError(13, "Kapan No"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueKapan.Focus();
                    }
                }
                if (lueCutNo.Text == "")
                {
                    lstError.Add(new ListError(13, "Cut No"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueCutNo.Focus();
                    }
                }
            }

            catch (Exception ex)
            {
                lstError.Add(new ListError(ex));
            }
            return (!(BLL.General.ShowErrors(lstError)));
        }
        private void btnPopUpStock_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (Validate_PopUp())
            //    {
            //        DataTable DTab_Stock = Global.GetStockCutwise(Val.ToInt(lueKapan.EditValue), Val.ToInt(lueCutNo.EditValue), Val.ToInt32(2));

            //        FrmMFGAssortmentConfirm FrmMFGAssortmentConfirm = new FrmMFGAssortmentConfirm();
            //        FrmMFGAssortmentConfirm.FrmMFGAssortFinalLotting = this;
            //        FrmMFGAssortmentConfirm.DTab = DTab_Stock;
            //        FrmMFGAssortmentConfirm.ShowForm(this);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Global.Message(ex.ToString());
            //}
        }

        private void txtLotId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
