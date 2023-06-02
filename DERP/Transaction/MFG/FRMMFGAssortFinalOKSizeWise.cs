using BLL;
using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.PropertyClasses.Report;
using BLL.PropertyClasses.Transaction;
using BLL.PropertyClasses.Transaction.MFG;
using DERP.Class;
using DERP.Master;
using DERP.Master.MFG;
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
    public partial class FRMMFGAssortFinalOKSizeWise : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;
        DataTable m_dtbSubProcess = new DataTable();
        MFGAssortFinalOkSizeWise objAssortFinalOK;

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

        Int64 m_numForm_id;
        Int64 IntRes;
        int Count = 0;
        int m_IsLot;
        Int64 Lot_SrNo = 0;

        #endregion

        #region Constructor
        public FRMMFGAssortFinalOKSizeWise()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            objAssortFinalOK = new MFGAssortFinalOkSizeWise();
            ObjReportDetailProperty = new New_Report_DetailProperty();
            DtControlSettings = new DataTable();
            dtTemp = new DataTable();
            m_dtbParam = new DataTable();
            m_dtCut = new DataTable();
            m_DtProcess = new DataTable();
            m_dtOutstanding = new DataTable();
            m_dtbKapan = new DataTable();
            //DTab_KapanWiseData = new DataTable();

            m_numForm_id = 0;
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

            //if (Global.HideFormControls(Val.ToInt(ObjPer.form_id), this) != "")
            //{
            //    Global.Message("Select First User Setting...Please Contact to Administrator...");
            //    return;
            //}

            //ControlSettingDT(Val.ToInt(ObjPer.form_id), this);
            //AddGotFocusListener(this);
            //AddKeyPressListener(this);
            //this.KeyPreview = true;

            //TabControlsToList(this.Controls);
            //_tabControls = _tabControls.OrderBy(x => x.TabIndex).ToList();

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
                    Global.Message("Carat not match Please Check..");
                    return;

                }
                int IsExist = objAssortFinalOK.CheckGetData(Val.ToInt(lueKapan.EditValue), Val.ToInt(lueCutNo.EditValue), Val.ToInt32(lblLotSRNo.Text));
                if (IsExist == 1)
                {
                    Global.Message("This Kapan and Rough Cut is Already Exist!!!");
                    return;
                }

                if (!ValidateDetails())
                {
                    return;
                }

                DialogResult result = MessageBox.Show("Do you want to save Assort Final OK data?", "Confirmation", MessageBoxButtons.YesNoCancel);
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

                txtCarat.Text = "0";
                grdAssortFinal.DataSource = null;
                dgvAssortFinal.Columns.Clear();
                btnSave.Enabled = true;
                lblLotSRNo.Text = "0";
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
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateDetails())
                {
                    return;
                }
                int IsExist = objAssortFinalOK.CheckGetData(Val.ToInt(lueKapan.EditValue), Val.ToInt(lueCutNo.EditValue), Val.ToInt32(lblLotSRNo.Text));
                if (IsExist == 1)
                {
                    Global.Message("This Kapan and Rough Cut is Already Exist!!!");
                    return;
                }
                dtTemp = new DataTable();
                DataTable dtnew = new DataTable();
                dtnew.AcceptChanges();
                dgvAssortFinal.Columns.Clear();
                lblLotSRNo.Text = "0";

                dtnew = objAssortFinalOK.AssortFinalSizeGetData(Val.ToString(lueAssort.EditValue), Val.ToString(lueSieve.EditValue));

                if (dtnew.Rows.Count > 0)
                {
                    pivot pt = new pivot(dtnew);
                    dtTemp = pt.PivotDataSuperPlusAssortment(new string[] { "assort_id", "assort" }, new string[] { "carat", "per(%)" }, new AggregateFunction[] { AggregateFunction.Sum, AggregateFunction.Sum }, new string[] { "sieve" });

                    grdAssortFinal.DataSource = dtTemp;
                    //dgvAssortFinal.Columns["type"].Visible = false;
                    dgvAssortFinal.Columns["assort_id"].Visible = false;
                    dgvAssortFinal.Columns["assort"].OptionsColumn.ReadOnly = true;
                    dgvAssortFinal.Columns["assort"].OptionsColumn.AllowFocus = false;

                    dgvAssortFinal.Columns["assort"].Caption = "#";
                    dgvAssortFinal.Columns["assort"].Fixed = FixedStyle.Left;
                    dgvAssortFinal.Columns["assort"].Caption = "#";
                    decimal Total_Tot;
                    //decimal Total_Amt;
                    for (int i = 0; i <= dtTemp.Rows.Count - 1; i++)
                    {
                        Total_Tot = 0;
                        //Total_Amt = 0;
                        for (int j = 0; j <= dtTemp.Columns.Count - 1; j++)
                        {

                            if (dtTemp.Columns[j].ToString().Contains("carat"))
                            {
                                Total_Tot = Total_Tot + Val.ToDecimal(dtTemp.Rows[i][j].ToString());
                                string[] col = dtTemp.Columns[j].ToString().Split('_');
                                string currcol = "";
                                //currcol = col[1] + "_" + col[2];
                                currcol = col[1];
                                dgvAssortFinal.Columns[j].Caption = currcol;
                                //dgvAssortFinal.Columns[dtTemp.Columns[j].ToString()].Width = 150;
                            }
                            if (dtTemp.Columns[j].ToString().Contains("per(%)"))
                            {
                                string[] col = dtTemp.Columns[j].ToString().Split('_');
                                string currcol = "";
                                //currcol = col[1] + "_" + col[2];
                                currcol = col[2];
                                dgvAssortFinal.Columns[j].Caption = currcol;
                                dgvAssortFinal.Columns[j].OptionsColumn.AllowEdit = false;
                                //dgvAssortFinal.Columns[dtTemp.Columns[j].ToString()].Width = 150;
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


                            if (dtTemp.Columns[j].ToString().Contains("per(%)"))
                            {
                                string Per = dtTemp.Columns[j].ToString();
                                GridColumn column4 = dgvAssortFinal.Columns[Per];
                                dgvAssortFinal.Columns[Per].SummaryItem.DisplayFormat = " {0:n2}";
                                column4.SummaryItem.SummaryType = SummaryItemType.Custom;
                            }

                        }
                        break;
                    }
                }

                if (Val.ToDecimal(dtnew.Rows[0]["carat"]) > 0)
                {
                }
                else
                {
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
            //        }
            //        else
            //        {
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
                    lueCutNo.Properties.DataSource = m_dtbParam;
                    lueCutNo.Properties.ValueMember = "rough_cut_id";
                    lueCutNo.Properties.DisplayMember = "rough_cut_no";
                }
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

                Global.LOOKUPSieve(lueSieve);
                Global.LOOKUPProcess(lueProcess);
                lueProcess.Text = "ASSORTMENT";

                Global.LOOKUPSubProcess(lueSubProcess);
                Global.LOOKUPAssort(lueAssort);

                lueSieve.SetEditValue("13,16,25,26,27,28,29,30,31,32");
                lueAssort.SetEditValue("300,425,333,369,1665,1664");
                lueProcess.EditValue = Val.ToInt(3012);
                m_dtbSubProcess = (((DataTable)lueSubProcess.Properties.DataSource).Copy());
                lueProcess_EditValueChanged(null, null);

                // Add By Praful On 29072021

                //DTab_KapanWiseData = Global.GetRoughStockWise(Val.ToInt(0), Val.ToInt32(0));

                // End By Praful On 29072021

                xtabAssortDetail.SelectedTabPage = xtblEntry;

                lueSubProcess.Text = "FINAL OK";
                lueSubProcess.EditValue = Val.ToInt(3074);
                m_dtbParam = Global.GetRoughCutAll();
                lueSieve.Enabled = false;
                lueAssort.Enabled = false;
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
                MFGAssortFinalOkSizeWise MFGAssortFinalOK = new MFGAssortFinalOkSizeWise();
                MFGAssortFirst MFGAssortReceive = new MFGAssortFirst();
                MFGAssortFinal_OKProperty objMFGAssortFinalOKProperty = new MFGAssortFinal_OKProperty();

                Conn = new BeginTranConnection(true, false);

                DataTable m_DTab = new DataTable();
                ArrayList MyArrayList = new ArrayList();

                IntRes = 0;
                Lot_SrNo = 0;
                try
                {
                    objMFGAssortFinalOKProperty.process_id = Val.ToInt(lueProcess.EditValue);
                    objMFGAssortFinalOKProperty.sub_process_id = Val.ToInt(lueSubProcess.EditValue);
                    objMFGAssortFinalOKProperty.kapan_id = Val.ToInt(lueKapan.EditValue);
                    //objMFGAssortFinalOKProperty.lot_id = Val.ToInt(0);
                    objMFGAssortFinalOKProperty.rough_cut_id = Val.ToInt(lueCutNo.EditValue);
                    //objMFGAssortFinalOKProperty.temp_sieve_name = lueSieve.Text.ToString();
                    if (Val.ToInt64(lblLotSRNo.Text) > 0)
                    {
                        objMFGAssortFinalOKProperty.flag = Val.ToInt(2);
                        Lot_SrNo = Val.ToInt64(lblLotSRNo.Text);
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
                                objMFGAssortFinalOKProperty.rough_cut_id = Val.ToInt(lueCutNo.EditValue);
                                //objMFGAssortFinalOKProperty.receive_date = Val.DBDate(dtpReceiveDate.Text);
                                objMFGAssortFinalOKProperty.assort_id = Val.ToInt(Drw["assort_id"]);
                                objMFGAssortFinalOKProperty.sieve_id = Val.ToInt(Val.ToString(dtbDetail.Columns[i]).Split('_')[0]);
                                //if (Count == 0)
                                //{
                                //    objMFGAssortFinalOKProperty.carat = Val.ToDecimal(txtCarat.Text);
                                //}
                                //else
                                //{
                                //    objMFGAssortFinalOKProperty.carat = Val.ToDecimal(0);
                                //}
                                objMFGAssortFinalOKProperty.to_carat = Val.ToDecimal(Drw[Val.ToString(objMFGAssortFinalOKProperty.sieve_id) + "_" + "carat"]);
                                objMFGAssortFinalOKProperty.percentage = Val.ToDecimal(Drw[Val.ToString(objMFGAssortFinalOKProperty.sieve_id) + "_" + "per(%)"]);
                                objMFGAssortFinalOKProperty.form_id = m_numForm_id;
                                objMFGAssortFinalOKProperty.count = Count;
                                //objMFGAssortFinalOKProperty.mix_union_id = Mix_Union_Id;
                                objMFGAssortFinalOKProperty.lot_srno = Lot_SrNo;
                                //objMFGAssortFinalOKProperty.temp_sieve_name = lueSieve.Text.ToString();

                                objMFGAssortFinalOKProperty = MFGAssortFinalOK.Save(objMFGAssortFinalOKProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                                Lot_SrNo = Val.ToInt64(objMFGAssortFinalOKProperty.lot_srno);
                                IntRes = Val.ToInt(Lot_SrNo);
                            }
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
        private void backgroundWorker_AssortFinalReceive_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (IntRes > 0)
                {
                    Global.Confirm("Assortment Final OK Size Data Save Succesfully");

                    string Date = Val.DBDate(dtpReceiveDate.Text);
                    btnClear_Click(null, null);
                    xtabAssortDetail.SelectedTabPage = xtblEntry;
                    lblLotSRNo.Text = Lot_SrNo.ToString();
                    lblLotSRNo.Text = "0";

                }
                else
                {
                    Global.Confirm("Error In Assortment Final OK Size");
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
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
                //if (RBtnLocationType.EditValue.ToString() == "2")
                //{
                //    if (RBtnType.SelectedIndex == 0)
                //    {
                //        DataSet DTab_IssueJanged = objAssortFinalOK.Print_Assort_Final_Mumbai(Val.ToInt32(lueKapan.EditValue), Val.ToInt32(lueCutNo.EditValue), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue), Val.ToInt64(lblLotSRNo.Text), Val.ToInt64(RBtnLocationType.EditValue), Val.DBDate(dtpReceiveDate.Text));


                //        FrmReportViewer FrmReportViewer = new FrmReportViewer();
                //        foreach (DataTable DTab in DTab_IssueJanged.Tables)
                //            FrmReportViewer.DS.Tables.Add(DTab.Copy());
                //        FrmReportViewer.GroupBy = "";
                //        FrmReportViewer.RepName = "";
                //        FrmReportViewer.RepPara = "";
                //        this.Cursor = Cursors.Default;
                //        FrmReportViewer.AllowSetFormula = true;

                //        FrmReportViewer.ShowForm_SubReport("CrtPolishGrading_Final_Main_Mumbai", 120, FrmReportViewer.ReportFolder.ACCOUNT);

                //        DTab_IssueJanged = null;
                //        FrmReportViewer.DS.Tables.Clear();
                //        FrmReportViewer.DS.Clear();
                //        FrmReportViewer = null;
                //    }
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
                objAssortFinalOK = new MFGAssortFinalOkSizeWise();
                MFGProcessIssueProperty objMFGProcessIssueProperty = new MFGProcessIssueProperty();

                DtAssortment = objAssortFinalOK.GetData(Val.ToString(dtpFromDate.Text), Val.ToString(dtpToDate.Text));
                grdMFGFinalOkSizeList.DataSource = DtAssortment;
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
                DialogResult result = MessageBox.Show("Do you want to Delete Final data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result != DialogResult.Yes)
                {
                    btnSave.Enabled = true;
                    return;
                }
                IntRes = 0;
                MFGAssortFinalOkSizeWise MFGAssortFinalSize = new MFGAssortFinalOkSizeWise();
                MFGAssortFinal_LottingProperty objMFGAssortFinalLottingProperty = new MFGAssortFinal_LottingProperty();

                objMFGAssortFinalLottingProperty.Del_lot_srno = Val.ToInt64(lblLotSRNo.Text);

                IntRes = MFGAssortFinalSize.Delete(objMFGAssortFinalLottingProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                if (IntRes > 0)
                {
                    Global.Confirm("Final OK Size Data Deleted Succesfully");
                    ClearDetails();
                    btnSave.Enabled = true;
                }
                else
                {
                    Global.Confirm("Error In Final OK Size Data");
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
                            //rate = Val.ToDecimal(dtAmount.Rows[i][prefix[0] + "_" + prefix[1] + "_rate"]);
                            //totAmount += Val.ToDecimal(carat * rate);
                            //dtAmount.Rows[i][prefix[0] + "_" + prefix[1] + "_amount"] = Math.Round((carat * rate), 0).ToString();
                            Percent = (perTotal * 100) / Val.ToDecimal(txtCarat.Text);
                            dtAmount.Rows[i][prefix[0] + "_" + prefix[1] + "_per(%)"] = Math.Round(Percent, 2).ToString();
                            //break;
                            // dtAmount.Rows[i][prefix[0] + "_" + prefix[1] + "_carat"] = Math.Round(total, 2).ToString();
                        }

                    }
                    total = 0;
                }
                dtAmount.AcceptChanges();
                CalculateMain();
                dgvAssortFinal.BestFitColumns();
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
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void dgvMFGAssortFinalOkSizeList_RowClick(object sender, RowClickEventArgs e)
        {
            try
            {
                objAssortFinalOK = new MFGAssortFinalOkSizeWise();
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        //m_blncheckevents = true;
                        DataRow Drow = dgvMFGAssortFinalOkSizeList.GetDataRow(e.RowHandle);
                        lblLotSRNo.Text = Val.ToString(Drow["lot_srno"]);
                        dtpReceiveDate.Text = Val.DBDate(Val.ToString(Drow["assort_date"]));
                        m_dtCut = Global.GetRoughCutAll();
                        //m_IsLot = 1;

                        lueKapan.EditValue = Val.ToInt64(Drow["kapan_id"]);
                        lueCutNo.EditValue = Val.ToInt64(Drow["rough_cut_id"]);
                        txtCarat.Text = Val.ToString(Drow["carat"]);
                        DtAssortment = objAssortFinalOK.GetListData(Val.ToInt(lblLotSRNo.Text));

                        //grdMFGFinalOkSizeList.DataSource = DtAssortment;
                        xtabAssortDetail.SelectedTabPage = xtblEntry;
                        FillGrid(DtAssortment);

                        dtpReceiveDate.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
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
            //m_TotalSumm = 0;
            //if (dgvAssortFinal.DataSource != null)
            //{
            //    GridColumn column4 = dgvAssortFinal.Columns["Total"];
            //    m_TotalSumm = Val.ToDecimal(column4.SummaryText);
            //}
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
        public void FillGrid(DataTable DTFill)
        {
            if (DTFill.Rows.Count > 0)
            {
                pivot pt = new pivot(DTFill);
                dtTemp = pt.PivotDataSuperPlusAssortment(new string[] { "assort_id", "assort" }, new string[] { "carat", "per(%)" }, new AggregateFunction[] { AggregateFunction.Sum, AggregateFunction.Sum }, new string[] { "sieve" });

                grdAssortFinal.DataSource = dtTemp;
                //dgvAssortFinal.Columns["type"].Visible = false;
                dgvAssortFinal.Columns["assort_id"].Visible = false;
                dgvAssortFinal.Columns["assort"].OptionsColumn.ReadOnly = true;
                dgvAssortFinal.Columns["assort"].OptionsColumn.AllowFocus = false;

                dgvAssortFinal.Columns["assort"].Caption = "#";
                dgvAssortFinal.Columns["assort"].Fixed = FixedStyle.Left;
                dgvAssortFinal.Columns["assort"].Caption = "#";
                decimal Total_Tot;
                //decimal Total_Amt;
                for (int i = 0; i <= dtTemp.Rows.Count - 1; i++)
                {
                    Total_Tot = 0;
                    //Total_Amt = 0;
                    for (int j = 0; j <= dtTemp.Columns.Count - 1; j++)
                    {

                        if (dtTemp.Columns[j].ToString().Contains("carat"))
                        {
                            Total_Tot = Total_Tot + Val.ToDecimal(dtTemp.Rows[i][j].ToString());
                            string[] col = dtTemp.Columns[j].ToString().Split('_');
                            string currcol = "";
                            currcol = col[1];
                            dgvAssortFinal.Columns[j].Caption = currcol;
                            //dgvAssortFinal.Columns[dtTemp.Columns[j].ToString()].Width = 150;
                        }
                        if (dtTemp.Columns[j].ToString().Contains("per(%)"))
                        {
                            string[] col = dtTemp.Columns[j].ToString().Split('_');
                            string currcol = "";
                            currcol = col[2];
                            dgvAssortFinal.Columns[j].Caption = currcol;
                            //dgvAssortFinal.Columns[dtTemp.Columns[j].ToString()].Width = 150;
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


                        if (dtTemp.Columns[j].ToString().Contains("per(%)"))
                        {
                            string Per = dtTemp.Columns[j].ToString();
                            GridColumn column4 = dgvAssortFinal.Columns[Per];
                            dgvAssortFinal.Columns[Per].SummaryItem.DisplayFormat = " {0:n2}";
                            column4.SummaryItem.SummaryType = SummaryItemType.Custom;
                        }

                    }
                    break;
                }
            }

            dgvAssortFinal.OptionsView.ShowFooter = true;
            dgvAssortFinal.BestFitColumns();
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
