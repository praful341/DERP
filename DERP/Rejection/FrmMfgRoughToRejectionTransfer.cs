using BLL;
using BLL.FunctionClasses.Rejection;
using BLL.PropertyClasses.Rejection;
using DERP.Class;
using DERP.Rejection;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Transaction
{
    public partial class FrmMfgRoughToRejectionTransfer : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;
        DataTable m_dtbParam;
        DataTable m_dtCut;
        DataTable m_DtProcess;
        DataTable m_dtbType;
        DataTable m_dtbKapan;
        DataTable m_dtbRejectionLot;
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        MfgRejectionTransfer objRejectionTrf;
        DataTable DTabSummary = new DataTable();
        //DataTable DTab_KapanWiseData;
        int m_numForm_id;
        int IntRes;
        int m_numSelectedCount;
        int filterFlag;

        Int64 Lot_SrNo;
        decimal m_numSummRate = 0;
        decimal m_numDetSummRate = 0;
        #endregion

        #region Constructor
        public FrmMfgRoughToRejectionTransfer()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            objRejectionTrf = new MfgRejectionTransfer();
            m_dtbParam = new DataTable();
            m_dtCut = new DataTable();
            m_DtProcess = new DataTable();
            m_dtbKapan = new DataTable();
            m_dtbRejectionLot = new DataTable();
            //DTab_KapanWiseData = new DataTable();
            m_numForm_id = 0;
            IntRes = 0;
            m_numSelectedCount = 0;
            filterFlag = 0;
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
            DTabSummary.Columns.Add(new DataColumn("purity_name", typeof(string)));
            DTabSummary.Columns.Add(new DataColumn("carat", typeof(string)));
            this.Show();
        }
        private void AttachFormEvents()
        {
            objBOFormEvents.CurForm = this;
            objBOFormEvents.FormKeyPress = true;
            objBOFormEvents.FormKeyDown = true;
            objBOFormEvents.FormResize = true;
            objBOFormEvents.FormClosing = true;
            objBOFormEvents.ObjToDispose.Add(objRejectionTrf);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion

        #region Events     
        private void FrmRejectionTransferConfirm_Load(object sender, EventArgs e)
        {
            try
            {
                dtpConfirmDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpConfirmDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpConfirmDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpConfirmDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpConfirmDate.EditValue = DateTime.Now;

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

                m_dtbKapan = Global.GetKapanAll();

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

                Global.LOOKUPProcess(lueProcess);
                Global.LOOKUPRejPurityRep(repLueRejectionPurity);
                lueType.Properties.DataSource = m_dtbType;
                lueType.Properties.ValueMember = "type";
                lueType.Properties.DisplayMember = "type";
                lueType.EditValue = "BOTH";

                DataTable dtbProcess = (((DataTable)lueProcess.Properties.DataSource).Copy());

                if (dtbProcess.Select("process_id in(1003,3,1002,2002,3034,3036,2004,3010,3032,3024,1,3016)").Length > 0)
                {
                    dtbProcess = dtbProcess.Select("process_id in(1003,3,1002,2002,3034,3036,2004,3010,3032,3024,1,3016)").CopyToDataTable();

                    if (dtbProcess.Rows.Count > 0)
                    {
                        lueProcess.Properties.DataSource = dtbProcess;
                        lueProcess.Properties.ValueMember = "process_id";
                        lueProcess.Properties.DisplayMember = "process_name";
                    }
                }
                else
                {
                    lueProcess.Properties.DataSource = null;
                }

                // Add By Praful On 29072021

                //DTab_KapanWiseData = Global.GetRoughStockWise(Val.ToInt(0), Val.ToInt32(0));

                // End By Praful On 29072021

                dtpConfirmDate.Focus();
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
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
                btnSave.Enabled = false;
                m_dtbRejectionLot.AcceptChanges();
                if (Val.ToString(lblMode.Text) == "Add Mode")
                {
                    if (m_dtbRejectionLot.Rows.Count > 0 && m_dtbRejectionLot.Select("SEL = true").Length > 0)
                    {
                        DataTable CheckDT = new DataTable();
                        CheckDT = m_dtbRejectionLot.Select("SEL <> False").CopyToDataTable();

                        foreach (DataRow DRow in CheckDT.Rows)
                        {
                            if (Val.ToInt64(DRow["purity_id"]) == 0)
                            {
                                Global.Message("Please Check Rejection Purity not be blank");
                                btnSave.Enabled = true;
                                return;
                            }
                        }

                        DialogResult result = MessageBox.Show("Do you want to Save data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                        if (result != DialogResult.Yes)
                        {
                            btnSave.Enabled = true;
                            return;
                        }
                        DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;

                        panelProgress.Visible = true;
                        backgroundWorker_RejectionTransfer.RunWorkerAsync();
                    }
                    else
                    {
                        General.ShowErrors("Atleast 1 Lot must be select in grid.");
                    }
                }
                else
                {
                    DialogResult result = MessageBox.Show("Do you want to Update data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                    if (result != DialogResult.Yes)
                    {
                        btnSave.Enabled = true;
                        return;
                    }
                    DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;

                    panelProgress.Visible = true;
                    backgroundWorker_RejectionTransfer.RunWorkerAsync();
                }
                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                this.Cursor = Cursors.Default;
                return;
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void repChkSel_CheckedChanged(object sender, EventArgs e)
        {
            GetSummary();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            lueKapan.EditValue = null;
            //lueCutNo.EditValue = null;
            for (int i = 0; i < lueProcess.Properties.Items.Count; i++)
                lueProcess.Properties.Items[i].CheckState = CheckState.Unchecked;
            for (int i = 0; i < lueCutNo.Properties.Items.Count; i++)
                lueCutNo.Properties.Items[i].CheckState = CheckState.Unchecked;
            //m_dtbRejectionLot = null;
            m_dtbRejectionLot.Rows.Clear();
            dtpConfirmDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            dtpConfirmDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            dtpConfirmDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            dtpConfirmDate.Properties.CharacterCasing = CharacterCasing.Upper;
            dtpConfirmDate.EditValue = DateTime.Now;
            lueType.EditValue = "BOTH";
            txtSelCarat.Text = "0";
            txtSelLot.Text = "0";
            lblMode.Text = "Add Mode";
            lblMode.Tag = 0;
            grdRejectionTransfer.DataSource = m_dtbRejectionLot;
            //grdRejectionTransfer.DataSource = null;
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
        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (filterFlag == 0)
                {
                    for (int i = 0; i < dgvRejectionTransfer.RowCount; i++)
                    {
                        dgvRejectionTransfer.SetRowCellValue(i, "SEL", chkAll.Checked);
                    }
                    GetSummary();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void repChkSel_QueryValueByCheckState(object sender, DevExpress.XtraEditors.Controls.QueryValueByCheckStateEventArgs e)
        {
            GetSummary();
        }
        private void repChkSel_MouseUp(object sender, MouseEventArgs e)
        {
            GetSummary();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (GlobalDec.gEmployeeProperty.user_name != "JAYESH")
            {
                Global.Message("Don't have Permission...So Please Contact to Administrator");
                return;
            }
            if (Val.ToString(lblMode.Text) != "Edit Mode")
            {
                return;
            }
            btnDelete.Enabled = false;
            DialogResult result = MessageBox.Show("Do you want to Delete data?", "Confirmation", MessageBoxButtons.YesNoCancel);
            if (result != DialogResult.Yes)
            {
                btnDelete.Enabled = true;
                return;
            }
            MFGRejection_TransferProperty RejTransferProperty = new MFGRejection_TransferProperty();
            try
            {
                Conn = new BeginTranConnection(true, false);
                foreach (DataRow DRow in m_dtbRejectionLot.Rows)
                {
                    RejTransferProperty.transfer_id = Val.ToInt(DRow["transfer_id"]);
                    RejTransferProperty.kapan_id = Val.ToInt(lueKapan.EditValue);
                    RejTransferProperty.cut_id = Val.ToInt(DRow["cut_id"]);
                    RejTransferProperty.process_id = Val.ToInt(DRow["process_id"]);
                    RejTransferProperty.from_clarity_id = Val.ToInt(DRow["rough_clarity_id"]);
                    RejTransferProperty.from_purity_id = Val.ToInt(DRow["quality_id"]);
                    RejTransferProperty.to_purity_id = Val.ToInt(DRow["purity_id"]);
                    RejTransferProperty.from_carat = Val.ToDecimal(DRow["carat"]);
                    RejTransferProperty.from_rate = Val.ToDecimal(DRow["rate"]);
                    RejTransferProperty.from_amount = Val.ToDecimal(DRow["amount"]);
                    RejTransferProperty.to_carat = Val.ToDecimal(DRow["carat"]);
                    RejTransferProperty.to_rate = Val.ToDecimal(DRow["rate"]);
                    RejTransferProperty.to_amount = Val.ToDecimal(DRow["amount"]);
                    RejTransferProperty.lot_srno = Val.ToInt64(Lot_SrNo);
                    IntRes = objRejectionTrf.Delete(RejTransferProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                    IntRes++;
                }
                Conn.Inter1.Commit();

                if (IntRes > 0)
                {
                    Global.Confirm("Rough To Rej Transfer Data Deleted Succesfully");
                    btnDelete.Enabled = true;
                    btnClear_Click(null, null);
                    PopulateDetails();
                }
                else
                {
                    Global.Confirm("Error In Rough To Rej Transfer Data");
                    btnDelete.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                IntRes = -1;
                Conn.Inter1.Rollback();
                Conn = null;
                General.ShowErrors(ex.ToString());
                btnDelete.Enabled = true;
                return;
            }
            finally
            {
                RejTransferProperty = null;
                btnDelete.Enabled = true;
            }
        }
        private void backgroundWorker_RejectionTransfer_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.Default;
                MFGRejection_TransferProperty RejTransferProperty = new MFGRejection_TransferProperty();
                try
                {
                    IntRes = 0;
                    Lot_SrNo = 0;
                    Conn = new BeginTranConnection(true, false);
                    int IntCounter = 0;
                    int Count = 0;
                    m_dtbRejectionLot.AcceptChanges();
                    //m_dtbRejectionLot.Rows.Clear();
                    m_dtbRejectionLot = (DataTable)grdRejectionTransfer.DataSource;
                    if (m_dtbRejectionLot.Select("SEL = true").Length > 0 && Val.ToInt32(lblMode.Tag) == 0)
                    {
                        m_dtbRejectionLot = m_dtbRejectionLot.Select("SEL = true").CopyToDataTable();
                        int TotalCount = m_dtbRejectionLot.Rows.Count;

                        foreach (DataRow DRow in m_dtbRejectionLot.Rows)
                        {
                            decimal Rate = Val.ToDecimal(DRow["rate"]);
                            decimal Amount = Val.ToDecimal(DRow["amount"]);

                            if (Rate <= 0)
                            {
                                Global.Message("Rate Is Zero in this Rough Clarity = " + Val.ToString(DRow["rough_clarity_name"]) + " And Purity Name = " + Val.ToString(DRow["quality_name"]));
                                return;
                            }
                            else if (Amount <= 0)
                            {
                                Global.Message("Amount Is Zero in this Rough Clarity = " + Val.ToString(DRow["rough_clarity_name"]) + " And Purity Name = " + Val.ToString(DRow["quality_name"]));
                                return;
                            }
                        }

                        foreach (DataRow DRow in m_dtbRejectionLot.Rows)
                        {
                            RejTransferProperty.transfer_id = Val.ToInt(0);
                            RejTransferProperty.transfer_date = Val.DBDate(dtpConfirmDate.Text);
                            RejTransferProperty.kapan_id = Val.ToInt(lueKapan.EditValue);
                            RejTransferProperty.cut_id = Val.ToInt(DRow["rough_cut_id"]);
                            RejTransferProperty.process_id = Val.ToInt(DRow["process_id"]);
                            RejTransferProperty.from_clarity_id = Val.ToInt(DRow["rough_clarity_id"]);
                            RejTransferProperty.from_purity_id = Val.ToInt(DRow["quality_id"]);
                            RejTransferProperty.to_purity_id = Val.ToInt(DRow["purity_id"]);
                            RejTransferProperty.old_to_purity_id = Val.ToInt(0);
                            RejTransferProperty.from_carat = Val.ToDecimal(DRow["carat"]);
                            RejTransferProperty.from_rate = Val.ToDecimal(DRow["rate"]);
                            RejTransferProperty.from_amount = Val.ToDecimal(DRow["amount"]);
                            RejTransferProperty.to_carat = Val.ToDecimal(DRow["carat"]);
                            RejTransferProperty.to_rate = Val.ToDecimal(DRow["rate"]);
                            RejTransferProperty.to_amount = Val.ToDecimal(DRow["amount"]);
                            RejTransferProperty.lot_srno = Val.ToInt64(Lot_SrNo);
                            //if (Val.ToInt(DRow["process_id"]) == 3032)
                            //{
                            RejTransferProperty.prediction_id = Val.ToInt64(DRow["prediction_id"]);
                            //}
                            RejTransferProperty.dept_transfer_id = Val.ToInt64(DRow["dept_transfer_id"]);
                            RejTransferProperty = objRejectionTrf.Save(RejTransferProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                            Lot_SrNo = Val.ToInt64(RejTransferProperty.lot_srno);
                            Count++;
                            IntCounter++;
                            IntRes++;
                            SetControlPropertyValue(lblProgressCount, "Text", Count.ToString() + "" + "/" + "" + TotalCount.ToString() + " Completed....");
                        }
                    }
                    else if (Val.ToInt32(lblMode.Tag) != 0)
                    {
                        foreach (DataRow DRow in m_dtbRejectionLot.Rows)
                        {
                            Lot_SrNo = Val.ToInt32(lblMode.Tag);
                            RejTransferProperty.transfer_id = Val.ToInt(DRow["transfer_id"]);
                            RejTransferProperty.transfer_date = Val.DBDate(dtpConfirmDate.Text);
                            RejTransferProperty.kapan_id = Val.ToInt(lueKapan.EditValue);
                            RejTransferProperty.cut_id = Val.ToInt(DRow["cut_id"]);
                            RejTransferProperty.process_id = Val.ToInt(DRow["process_id"]);
                            RejTransferProperty.from_clarity_id = Val.ToInt(0);
                            RejTransferProperty.from_purity_id = Val.ToInt(0);
                            RejTransferProperty.to_purity_id = Val.ToInt(DRow["purity_id"]);
                            RejTransferProperty.old_to_purity_id = Val.ToInt(DRow["old_purity_id"]);
                            RejTransferProperty.from_carat = Val.ToDecimal(DRow["carat"]);
                            RejTransferProperty.from_rate = Val.ToDecimal(DRow["rate"]);
                            RejTransferProperty.from_amount = Val.ToDecimal(DRow["amount"]);
                            RejTransferProperty.to_carat = Val.ToDecimal(0);
                            RejTransferProperty.to_rate = Val.ToDecimal(0);
                            RejTransferProperty.to_amount = Val.ToDecimal(0);
                            RejTransferProperty.lot_srno = Val.ToInt64(Lot_SrNo);
                            //if (Val.ToInt(DRow["process_id"]) == 3032)
                            //{
                            RejTransferProperty.prediction_id = Val.ToInt64(DRow["prediction_id"]);
                            //}
                            RejTransferProperty.dept_transfer_id = Val.ToInt64(DRow["dept_transfer_id"]);
                            RejTransferProperty = objRejectionTrf.Save(RejTransferProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                            Lot_SrNo = Val.ToInt64(RejTransferProperty.lot_srno);
                        }
                    }
                    if (Lot_SrNo == 0)
                    {
                        Global.Confirm("Error In Rejection Transfer");
                    }
                    else
                    {
                    }
                    Conn.Inter1.Commit();
                }
                catch (Exception ex)
                {
                    IntRes = -1;
                    //Conn.Inter1.Rollback();
                    //Conn = null;
                    General.ShowErrors(ex.ToString());
                    return;
                }
                finally
                {
                    RejTransferProperty = null;
                }
            }
            catch (Exception ex)
            {
                IntRes = -1;
                //Conn.Inter1.Rollback();
                //Conn = null;
                General.ShowErrors(ex.ToString());
                if (ex.InnerException != null)
                {
                    Global.Message(ex.InnerException.ToString());
                }
            }
        }
        private void backgroundWorker_RejectionTransfer_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                panelProgress.Visible = false;
                if (Lot_SrNo > 0)
                {
                    Global.Confirm("Rejection Transfer Successfully");
                    //m_dtbRejectionLot = null;
                    btnClear_Click(null, null);
                    PopulateDetails();
                    //GetData();
                    //CalculateTotal();

                    lueKapan.Focus();
                    this.Cursor = Cursors.Default;
                }
                else
                {
                    Global.Confirm("Error In Rejection Transfer");
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }
        private void lueKapan_EditValueChanged(object sender, EventArgs e)
        {
            m_dtbParam = new DataTable();
            if (lueKapan.Text.ToString() != "")
            {
                m_dtbParam = Global.GetRoughKapanWise_Data(Val.ToInt(lueKapan.EditValue));
            }
            lueCutNo.Properties.DataSource = m_dtbParam;
            lueCutNo.Properties.ValueMember = "rough_cut_id";
            lueCutNo.Properties.DisplayMember = "rough_cut_no";
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!ValidateDetails())
            {
                return;
            }
            objRejectionTrf = new MfgRejectionTransfer();
            m_dtbRejectionLot.Rows.Clear();
            m_dtbRejectionLot.Columns.Clear();
            m_dtbRejectionLot.AcceptChanges();

            string[] process = lueProcess.EditValue.ToString().Split(',');

            for (int i = 0; i < process.Length; i++)
            {
                DataTable DTab_Merge = new DataTable();

                if (Val.ToString(process[i]).Trim() == "3016" && Val.ToString(lueCutNo.Text) == string.Empty)
                {
                    DTab_Merge = objRejectionTrf.GetData(Val.ToInt32(lueKapan.EditValue), "", Val.ToString(process[i]).Trim(), Val.ToString(lueType.Text));

                    if (DTab_Merge.Rows.Count > 0)
                    {
                        if (DTab_Merge.Rows.Count > 0)
                            m_dtbRejectionLot.Merge(DTab_Merge);
                    }
                    else
                    {
                        Global.Message("Data not Found...");
                        //return;
                    }
                }
                else
                {
                    DTab_Merge = objRejectionTrf.GetData(Val.ToInt32(lueKapan.EditValue), Val.ToString(Val.Trim(lueCutNo.Properties.GetCheckedItems())), Val.ToString(process[i]).Trim(), Val.ToString(lueType.Text));
                    // Console.Write(process[i]);
                    if (DTab_Merge.Rows.Count > 0)
                    {
                        if (DTab_Merge.Rows.Count > 0)
                            m_dtbRejectionLot.Merge(DTab_Merge);
                    }
                    else
                    {
                        Global.Message("Data not Found...");
                        //return;
                    }
                }
            }

            //m_dtbRejectionLot = objRejectionTrf.GetData(Val.ToInt32(lueKapan.EditValue), Val.ToInt32(lueCutNo.EditValue), Val.ToString(lueProcess.EditValue), Val.ToString(lueType.Text));
            if (m_dtbRejectionLot.Columns.Contains("SEL") == false)
            {
                if (m_dtbRejectionLot.Columns.Contains("SEL") == false)
                {
                    DataColumn Col = new DataColumn();
                    Col.ColumnName = "SEL";
                    Col.DataType = typeof(bool);
                    Col.DefaultValue = false;
                    m_dtbRejectionLot.Columns.Add(Col);
                }
            }
            m_dtbRejectionLot.Columns["SEL"].SetOrdinal(0);
            grdRejectionTransfer.DataSource = m_dtbRejectionLot;
            dgvRejectionTransfer.BestFitColumns();
        }
        private void btnLSearch_Click(object sender, EventArgs e)
        {
            if (!PopulateDetails())
                return;
        }
        private void BtnPending_Click(object sender, EventArgs e)
        {
            FrmMFGSearchRejectionTransfer FrmMFGSearchRejectionTransfer = new FrmMFGSearchRejectionTransfer();
            FrmMFGSearchRejectionTransfer.FrmMfgRoughToRejectionTransfer = this;
            FrmMFGSearchRejectionTransfer.ShowForm(this);
        }
        private void repLueRejectionPurity_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit type = sender as LookUpEdit;
            dgvRejectionTransfer.SetRowCellValue(dgvRejectionTransfer.FocusedRowHandle, "purity_name", type.GetColumnValue("purity_name"));
        }
        private void dgvRejectionTransfer_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            FillSummaryGrid();
        }
        private void dgvRejectionTransfer_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            FillSummaryGrid();
        }

        #region GridEvents
        private void dgvDepartmentTransferConfirm_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "SEL")
            {
                if (Val.ToBoolean(dgvRejectionTransfer.GetRowCellValue(e.RowHandle, "SEL")) == true)
                {
                    dgvRejectionTransfer.SetRowCellValue(e.RowHandle, "SEL", false);
                }
                else
                {
                    dgvRejectionTransfer.SetRowCellValue(e.RowHandle, "SEL", true);
                }
            }
            FillSummaryGrid();
        }
        private void dgvDepartmentTransferConfirm_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == "SEL")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                        e.TotalValue = m_numSelectedCount;
                }
                if (Val.ToDecimal(clmAmount.SummaryItem.SummaryValue) > 0 && Val.ToDecimal(clmCarat.SummaryItem.SummaryValue) > 0)
                {
                    m_numDetSummRate = Math.Round((Val.ToDecimal(clmAmount.SummaryItem.SummaryValue) / Val.ToDecimal(clmCarat.SummaryItem.SummaryValue)), 3, MidpointRounding.AwayFromZero);

                }
                else
                {
                    m_numDetSummRate = 0;
                }
                if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == "rate")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                        e.TotalValue = m_numDetSummRate;
                }

            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        private void dgvDepartmentTransferConfirm_ColumnFilterChanged(object sender, EventArgs e)
        {
            if (dgvRejectionTransfer.ActiveFilterString != "")
            {
                filterFlag = 1;
                chkAll.Checked = false;
                filterFlag = 0;
            }
            else
            {
                filterFlag = 1;
                chkAll.Checked = false;
                filterFlag = 0;
            }
            FillSummaryGrid();
        }
        private void dgvJangedList_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvRejectionList.GetDataRow(e.RowHandle);
                        objRejectionTrf = new MfgRejectionTransfer();
                        m_dtbRejectionLot = new DataTable();
                        lueKapan.Text = Val.ToString(Drow["kapan_no"]);
                        lueKapan.EditValue = Val.ToInt(Drow["kapan_id"]);
                        lueCutNo.EditValue = Val.ToInt(Drow["cut_id"]);
                        lueCutNo.Text = Val.ToString(Drow["rough_cut_no"]);

                        lueProcess.EditValue = Val.ToInt(Drow["process_id"]);
                        dtpConfirmDate.EditValue = Val.ToString(Drow["transfer_date"]);


                        m_dtbRejectionLot = objRejectionTrf.FillListData(Val.ToInt(Drow["lot_srno"]), Val.ToInt(Drow["kapan_id"]), Val.ToInt(Drow["cut_id"]), Val.ToInt(Drow["process_id"]));
                        Lot_SrNo = Val.ToInt64(Drow["lot_srno"]);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt(Lot_SrNo);
                        if (m_dtbRejectionLot.Columns.Contains("SEL") == false)
                        {
                            if (m_dtbRejectionLot.Columns.Contains("SEL") == false)
                            {
                                DataColumn Col = new DataColumn();
                                Col.ColumnName = "SEL";
                                Col.DataType = typeof(bool);
                                Col.DefaultValue = false;
                                m_dtbRejectionLot.Columns.Add(Col);
                            }
                        }
                        m_dtbRejectionLot.Columns["SEL"].SetOrdinal(0);

                        grdRejectionTransfer.DataSource = m_dtbRejectionLot;
                        ttlbRejectionTransfer.SelectedTabPage = xtbpgEntry;
                        btnDelete.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void dgvRejectionList_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            if (Val.ToDecimal(clmLAmount.SummaryItem.SummaryValue) > 0 && Val.ToDecimal(clmLCarat.SummaryItem.SummaryValue) > 0)
            {
                m_numSummRate = Math.Round((Val.ToDecimal(clmLAmount.SummaryItem.SummaryValue) / Val.ToDecimal(clmLCarat.SummaryItem.SummaryValue)), 3, MidpointRounding.AwayFromZero);

            }
            else
            {
                m_numSummRate = 0;
            }
            if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == "to_rate")
            {
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    e.TotalValue = m_numSummRate;
            }
        }
        public void FillSummaryGrid()
        {
            DTabSummary.Rows.Clear();
            DataTable DtTransfer = new DataTable();

            DtTransfer = (DataTable)grdRejectionTransfer.DataSource;

            //MessageBox.Show("1");
            //DtTransfer.AcceptChanges();
            //MessageBox.Show("2");

            if (DtTransfer != null)
            {
                DtTransfer.AcceptChanges();

                if (DtTransfer.Select("SEL = True").Length > 0)
                {
                    //MessageBox.Show("3");
                    DtTransfer = DtTransfer.Select("SEL = True").CopyToDataTable();

                    string[] grpArray = new string[1] { "purity_name" };
                    DataTable temp = new DataTable();
                    for (int i = 0; i < grpArray.Length; i++)
                    {
                        if (grpArray[i] == "purity_name")
                        {
                            temp = DtTransfer;
                        }

                        var query = temp.AsEnumerable()
                                               .GroupBy(w =>
                                               new
                                               {
                                                   purity_name = w.Field<string>(grpArray[i])
                                               })
                                                        .Select(x => new
                                                        {
                                                            purity_name = x.Key.purity_name,
                                                            carat = x.Sum(w => Val.Val(w["carat"]))
                                                        });
                        DataTable DTProcessWise = LINQToDataTable(query);
                        foreach (DataRow DRow in DTProcessWise.Rows)
                        {
                            if (Val.ToString(DRow["purity_name"]).Trim().Equals(string.Empty))
                                continue;
                            DataRow DRNew = DTabSummary.NewRow();
                            DRNew["purity_name"] = DRow["purity_name"];
                            DRNew["carat"] = Math.Round(Val.Val(DRow["carat"]), 3);
                            DTabSummary.Rows.Add(DRNew);
                        }
                    }
                    //MessageBox.Show("4");
                    grdRejPuritySummary.DataSource = DTabSummary;
                    dgvRejPuritySummary.BestFitColumns();
                    //dgvRejPuritySummary.Columns["type"].Group();
                    dgvRejPuritySummary.ExpandAllGroups();
                }
            }
            else
            {
                grdRejPuritySummary.DataSource = null;
                dgvRejPuritySummary.BestFitColumns();
                dgvRejPuritySummary.ExpandAllGroups();
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
                if (lueProcess.Text.Length == 0)
                {
                    lstError.Add(new ListError(12, "Process"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueProcess.Focus();
                    }
                }
                if (lueType.Text.Length == 0)
                {
                    lstError.Add(new ListError(12, "Type"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueType.Focus();
                    }
                }
            }

            catch (Exception ex)
            {
                lstError.Add(new ListError(ex));
            }
            return (!(BLL.General.ShowErrors(lstError)));
        }
        private bool PopulateDetails()
        {
            objRejectionTrf = new MfgRejectionTransfer();
            bool blnReturn = true;
            DateTime datFromDate = DateTime.MinValue;
            DateTime datToDate = DateTime.MinValue;
            try
            {
                m_dtbRejectionLot = objRejectionTrf.GetListData(Val.DBDate(dtpFromDate.Text), Val.DBDate(dtpToDate.Text));

                if (m_dtbRejectionLot.Rows.Count == 0)
                {
                    Global.Message("Data Not Found");
                    blnReturn = false;
                }
                grdRejectionList.DataSource = m_dtbRejectionLot;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            finally
            {
                objRejectionTrf = null;
            }

            return blnReturn;
        }
        private void GetSummary()
        {
            try
            {
                double IntSelLot = 0; double DouSelCarat = 0;
                System.Data.DataTable DtTransfer = (System.Data.DataTable)grdRejectionTransfer.DataSource;
                dgvRejectionTransfer.PostEditor();
                Global.DtTransfer.AcceptChanges();

                if (DtTransfer != null)
                {
                    if (DtTransfer.Rows.Count > 0)
                    {
                        foreach (DataRow DRow in DtTransfer.Rows)
                        {
                            if (Val.ToString(DRow["SEL"]) == "True")
                            {
                                IntSelLot = IntSelLot + 1;
                                //IntSelPcs = IntSelPcs + Val.Val(DRow["pcs"]);
                                DouSelCarat = DouSelCarat + Val.Val(DRow["carat"]);
                            }
                        }
                    }
                }
                txtSelLot.Text = IntSelLot.ToString();
                //txtSelPcs.Text = IntSelPcs.ToString();
                txtSelCarat.Text = DouSelCarat.ToString();
            }
            catch
            {
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
                            dgvRejectionTransfer.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvRejectionTransfer.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvRejectionTransfer.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvRejectionTransfer.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvRejectionTransfer.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvRejectionTransfer.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvRejectionTransfer.ExportToCsv(Filepath);
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
        private void Export_List(string format, string dlgHeader, string dlgFilter)
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
                            dgvRejectionTransfer.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvRejectionTransfer.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvRejectionTransfer.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvRejectionTransfer.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvRejectionTransfer.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvRejectionTransfer.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvRejectionTransfer.ExportToCsv(Filepath);
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

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Export_List("xlsx", "Export to Excel", "Excel files 97-2003 (Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*");
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Export_List("pdf", "Export Report to PDF", "PDF (*.PDF)|*.PDF");
        }
    }
}
