using BLL;
using BLL.FunctionClasses.Master.MFG;
using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.PropertyClasses.Transaction.MFG;
using DERP.Class;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Transaction.MFG
{
    public partial class FrmMFGBoilingNo : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        MFGProcessReceive objProcessReceive;
        MFGJangedReceive objMFGJangedReceive;
        MfgRoughSieve objRoughSieve;
        MfgQualityMaster objQuality;
        MfgRoughClarityMaster objRoughClarity;
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);
        MultiEmployeeReturn objMFGMultiEmp = new MultiEmployeeReturn();
        DataTable dtTemp;
        DataTable m_dtbParam;
        DataTable m_dtCut;
        DataTable m_dtbKapan;
        DataTable m_dtbSubProcess;
        MFGJangedReturn objJangedReturn = new MFGJangedReturn();
        DataTable dtIss = new DataTable();
        DataTable DTab_StockData;
        DataTable DTabTemp = new DataTable();

        Int64 m_numForm_id;
        Int64 Boil_IntRes;
        #endregion

        #region Constructor
        public FrmMFGBoilingNo()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            objProcessReceive = new MFGProcessReceive();
            objMFGJangedReceive = new MFGJangedReceive();
            objRoughSieve = new MfgRoughSieve();
            objQuality = new MfgQualityMaster();
            objRoughClarity = new MfgRoughClarityMaster();
            DTab_StockData = new DataTable();
            dtTemp = new DataTable();
            m_dtbParam = new DataTable();
            m_dtCut = new DataTable();
            m_dtbKapan = new DataTable();
            m_dtbSubProcess = new DataTable();
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
        private void txtLotId_Validated(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateDetails())
                {
                    return;
                }
                m_dtbSubProcess.AcceptChanges();
                if (m_dtbSubProcess != null)
                {
                    if (m_dtbSubProcess.Rows.Count > 0)
                    {
                        DataRow[] dr = m_dtbSubProcess.Select("lot_id = " + Val.ToInt64(txtLotId.Text));

                        if (dr.Length > 0)
                        {
                            Global.Message(txtLotId + " = Lot ID already added to the Issue list!");
                            txtLotId.Text = "";
                            txtLotId.Focus();
                            return;
                        }

                        //for (int i = 0; i < m_dtbSubProcess.Rows.Count; i++)
                        //{
                        //    if (m_dtbSubProcess.Rows[i]["lot_id"].ToString() == txtLotId.Text)
                        //    {
                        //        Global.Message("Lot ID already added to the Issue list!");
                        //        txtLotId.Text = "";
                        //        txtLotId.Focus();
                        //        return;
                        //    }
                        //}
                    }
                }

                if (txtLotId.Text.Length == 0)
                {
                    return;
                }
                if (Val.ToString(GlobalDec.gEmployeeProperty.department_name) == "POLISH REPARING")
                {
                    MFGJangedReturn MFGJangedReturn = new MFGJangedReturn();
                    MFGJangedReturn_Property objMFGJangedReturnProperty = new MFGJangedReturn_Property();

                    objMFGJangedReturnProperty.lot_id = Val.ToInt64(txtLotId.Text);

                    DataTable DTab_ProcessIDCount = objJangedReturn.Process_CountData(objMFGJangedReturnProperty);

                    if (DTab_ProcessIDCount.Rows[0]["CNT"].ToString() == "0")
                    {
                        Global.Message("Polish Repairing Process not Completed in this Lot ID =" + Val.ToString(txtLotId.Text));
                        return;
                    }
                }

                if (m_dtbSubProcess.Rows.Count > 0)
                {
                    DataTable DTabTemp = new DataTable();
                    //DataTable DTab_ValidateLotID = new DataTable();
                    if (GlobalDec.gEmployeeProperty.role_name == "SURAT ASSORT")
                    {
                        DTabTemp = objJangedReturn.Stock_GetData(Val.ToInt64(txtLotId.Text), 0, Val.ToInt32(txtBoilingNo.Text));
                    }
                    else
                    {
                        DTabTemp = objJangedReturn.Boiling_Stock_GetData(Val.ToInt64(txtLotId.Text), 0, Val.ToInt32(txtBoilingNo.Text), Val.ToInt64(lueKapan.EditValue), Val.ToInt64(lueCutNo.EditValue));
                    }

                    //DataTable DTab_ValidateLotID = objJangedReturn.Boiling_Stock_GetData(Val.ToInt64(txtLotId.Text), 0, Val.ToInt32(txtBoilingNo.Text), Val.ToInt64(lueKapan.EditValue), Val.ToInt64(lueCutNo.EditValue)); //objJangedReturn.Stock_GetData(Val.ToInt64(txtLotId.Text), 0, Val.ToInt32(txtBoilingNo.Text));

                    if (DTabTemp.Rows.Count > 0)
                    {
                    }
                    else
                    {
                        Global.Message("Lot ID Not Issue in Janged");
                        txtLotId.Text = "";
                        txtLotId.Focus();
                        return;
                    }

                    //DTabTemp = objJangedReturn.Stock_GetData(Val.ToInt64(txtLotId.Text), 0, Val.ToInt32(txtBoilingNo.Text));
                    //if (GlobalDec.gEmployeeProperty.role_name == "SURAT ASSORT")
                    //{
                    //    DTabTemp = objJangedReturn.Stock_GetData(Val.ToInt64(txtLotId.Text), 0, Val.ToInt32(txtBoilingNo.Text));
                    //}
                    //else
                    //{
                    //    DTabTemp = objJangedReturn.Boiling_Stock_GetData(Val.ToInt64(txtLotId.Text), 0, Val.ToInt32(txtBoilingNo.Text), Val.ToInt64(lueKapan.EditValue), Val.ToInt64(lueCutNo.EditValue));
                    //}

                    if (DTabTemp.Rows.Count > 0)
                    {
                        txtLotId.Text = "";
                        txtLotId.Focus();
                    }
                    m_dtbSubProcess.Merge(DTabTemp);
                }
                else
                {
                    //DataTable DTab_ValidateLotID = objJangedReturn.Stock_GetData(Val.ToInt64(txtLotId.Text), 0, Val.ToInt32(txtBoilingNo.Text));

                    //DataTable DTab_ValidateLotID = new DataTable();
                    if (GlobalDec.gEmployeeProperty.role_name == "SURAT ASSORT")
                    {
                        m_dtbSubProcess = objJangedReturn.Stock_GetData(Val.ToInt64(txtLotId.Text), 0, Val.ToInt32(txtBoilingNo.Text));
                    }
                    else
                    {
                        m_dtbSubProcess = objJangedReturn.Boiling_Stock_GetData(Val.ToInt64(txtLotId.Text), 0, Val.ToInt32(txtBoilingNo.Text), Val.ToInt64(lueKapan.EditValue), Val.ToInt64(lueCutNo.EditValue));
                    }

                    if (m_dtbSubProcess.Rows.Count > 0)
                    {
                    }
                    else
                    {
                        Global.Message("Lot ID Not Issue in Janged");
                        txtLotId.Text = "";
                        txtLotId.Focus();
                        return;
                    }

                    //m_dtbSubProcess = objJangedReturn.Stock_GetData(Val.ToInt64(txtLotId.Text), 0, Val.ToInt32(txtBoilingNo.Text));

                    //if (GlobalDec.gEmployeeProperty.role_name == "SURAT ASSORT")
                    //{
                    //    m_dtbSubProcess = objJangedReturn.Stock_GetData(Val.ToInt64(txtLotId.Text), 0, Val.ToInt32(txtBoilingNo.Text));
                    //}
                    //else
                    //{
                    //    m_dtbSubProcess = objJangedReturn.Boiling_Stock_GetData(Val.ToInt64(txtLotId.Text), 0, Val.ToInt32(txtBoilingNo.Text), Val.ToInt64(lueKapan.EditValue), Val.ToInt64(lueCutNo.EditValue));
                    //}

                    if (m_dtbSubProcess.Rows.Count > 0)
                    {
                        txtLotId.Text = "";
                        txtLotId.Focus();
                    }
                }
                grdBoilingNo.DataSource = m_dtbSubProcess;
                grdBoilingNo.RefreshDataSource();
                dgvBoilingNo.BestFitColumns();
                (grdBoilingNo.FocusedView as GridView).MoveLast();
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void RepDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (Global.Confirm("Are you sure delete selected row?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                dgvBoilingNo.DeleteRow(dgvBoilingNo.GetRowHandle(dgvBoilingNo.FocusedRowHandle));
                DTab_StockData.AcceptChanges();
            }
        }
        private void FrmMFGJangedReturn_Load(object sender, EventArgs e)
        {
            try
            {
                m_dtbKapan = Global.GetKapanAll();

                lueKapan.Properties.DataSource = m_dtbKapan;
                lueKapan.Properties.ValueMember = "kapan_id";
                lueKapan.Properties.DisplayMember = "kapan_no";

                m_dtCut = Global.GetRoughCutAll();

                lueCutNo.Properties.DataSource = m_dtCut;
                lueCutNo.Properties.ValueMember = "rough_cut_id";
                lueCutNo.Properties.DisplayMember = "rough_cut_no";

                dtpReturnDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpReturnDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpReturnDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpReturnDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpReturnDate.EditValue = DateTime.Now;
                txtBoilingNo.Text = Val.ToInt64(objMFGMultiEmp.FindMaxBoilingLotID()).ToString();

                if (GlobalDec.gEmployeeProperty.role_name == "SURAT ASSORT")
                {
                    btnPopUpStock.Visible = false;
                    lblKapanNo.Visible = false;
                    lblCutNo.Visible = false;
                    lueKapan.Visible = false;
                    lueCutNo.Visible = false;
                }
                else
                {
                    btnPopUpStock.Visible = true;
                    lblKapanNo.Visible = true;
                    lblCutNo.Visible = true;
                    lueKapan.Visible = true;
                    lueCutNo.Visible = true;
                }
                txtLotId.Focus();
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
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
                btnSave.Enabled = false;
                DataTable dtTemp = new DataTable();
                dtTemp = (DataTable)grdBoilingNo.DataSource;
                List<ListError> lstError = new List<ListError>();
                if (dtTemp == null)
                {
                    Global.Message("Atleast 1 record must be enter in grid");
                    btnSave.Enabled = true;
                    return;
                }

                if (!ValidateDetails())
                {
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
                PanelLoading.Visible = true;
                backgroundWorker_BoilingNo.RunWorkerAsync();

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
        private void backgroundWorker_BoilingNo_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                MFGBoilingNo MFGBoilingNo = new MFGBoilingNo();
                MFG_Boiling_NoProperty objMFGBoilingNoProperty = new MFG_Boiling_NoProperty();

                Conn = new BeginTranConnection(true, false);
                Boil_IntRes = 0;
                try
                {
                    DataTable Boiling_Data = (DataTable)grdBoilingNo.DataSource;

                    Boiling_Data.AcceptChanges();

                    int IntCounter = 0;
                    int Count = 0;
                    int TotalCount = Boiling_Data.Rows.Count;

                    foreach (DataRow drw in Boiling_Data.Rows)
                    {
                        objMFGBoilingNoProperty.lot_id = Val.ToInt32(drw["lot_id"]);
                        objMFGBoilingNoProperty.boiling_no = Val.ToInt32(txtBoilingNo.Text);
                        objMFGBoilingNoProperty.boiling_date = Val.DBDate(dtpReturnDate.Text);
                        objMFGBoilingNoProperty.kapan_id = Val.ToInt32(drw["kapan_id"]);
                        objMFGBoilingNoProperty.rough_cut_id = Val.ToInt32(drw["rough_cut_id"]);
                        //objMFGBoilingNoProperty.history_union_id = NewHistory_Union_Id;

                        objMFGBoilingNoProperty = MFGBoilingNo.Save(objMFGBoilingNoProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                        Boil_IntRes = objMFGBoilingNoProperty.boiling_no;
                        //NewHistory_Union_Id = Val.ToInt64(objMFGBoilingNoProperty.history_union_id);

                        Count++;
                        IntCounter++;
                        SetControlPropertyValue(lblProgressCount, "Text", Count.ToString() + "" + "/" + "" + TotalCount.ToString() + " Completed....");
                    }
                    Conn.Inter1.Commit();
                }
                catch (Exception ex)
                {
                    PanelLoading.Visible = false;
                    Boil_IntRes = -1;
                    Conn.Inter1.Rollback();
                    Conn = null;
                    General.ShowErrors(ex.ToString());
                    return;
                }
            }
            catch (Exception ex)
            {
                PanelLoading.Visible = false;
                Boil_IntRes = -1;
                Conn.Inter1.Rollback();
                Conn = null;
                Global.Message(ex.ToString());
                if (ex.InnerException != null)
                {
                    Global.Message(ex.InnerException.ToString());
                }
            }
        }
        private void backgroundWorker_BoilingNo_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                PanelLoading.Visible = false;
                if (Boil_IntRes > 0)
                {
                    MessageBox.Show("Boiling No. Succesfully Saved");
                    btnSave.Enabled = true;
                    ClearDetails();
                    this.Cursor = Cursors.Default;

                    btnSave.Enabled = true;

                }
                else
                {
                    Global.Confirm("Error In Boiling No.");
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }
        private void btnPopUpStock_Click(object sender, EventArgs e)
        {
            GetStock();
        }

        #region Grid Event

        private void grdJangedReturn_ViewRegistered(object sender, DevExpress.XtraGrid.ViewOperationEventArgs e)
        {
            (e.View as GridView).OptionsNavigation.AutoFocusNewRow = true;
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
                //if (lueKapan.Text == "")
                //{
                //    lstError.Add(new ListError(5, "Kapan No is Required.."));
                //    if (!blnFocus)
                //    {
                //        blnFocus = true;
                //        lueKapan.Focus();
                //    }
                //}
                //if (lueCutNo.Text == "")
                //{
                //    lstError.Add(new ListError(5, "Rough Cut No is Required.."));
                //    if (!blnFocus)
                //    {
                //        blnFocus = true;
                //        lueCutNo.Focus();
                //    }
                //}
                var result = DateTime.Compare(Convert.ToDateTime(dtpReturnDate.Text), DateTime.Today);
                if (result > 0)
                {
                    lstError.Add(new ListError(5, " Return Date Not Be Greater Than Today Date"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        dtpReturnDate.Focus();
                    }
                }
                if (Val.ToString(dtpReturnDate.Text) == string.Empty)
                {
                    lstError.Add(new ListError(22, "Date"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        dtpReturnDate.Focus();
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
                dtpReturnDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpReturnDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpReturnDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpReturnDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpReturnDate.EditValue = DateTime.Now;
                txtLotId.Text = "0";
                grdBoilingNo.DataSource = null;
                DTab_StockData.Rows.Clear();
                DTab_StockData.Columns.Clear();
                lueKapan.EditValue = null;
                lueCutNo.EditValue = null;
                m_dtbSubProcess.Rows.Clear();
                m_dtbSubProcess.AcceptChanges();
                txtBoilingNo.Text = Val.ToInt64(objMFGMultiEmp.FindMaxBoilingLotID()).ToString();

                if (GlobalDec.gEmployeeProperty.role_name == "SURAT ASSORT")
                {
                    btnPopUpStock.Visible = false;
                    lblKapanNo.Visible = false;
                    lblCutNo.Visible = false;
                    lueKapan.Visible = false;
                    lueCutNo.Visible = false;
                }
                else
                {
                    btnPopUpStock.Visible = true;
                    lblKapanNo.Visible = true;
                    lblCutNo.Visible = true;
                    lueKapan.Visible = true;
                    lueCutNo.Visible = true;
                }

                lueKapan.Focus();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
        }
        public void GetStockData(DataTable Stock_Data)
        {
            try
            {
                m_dtbSubProcess.AcceptChanges();
                DTabTemp = Stock_Data.Copy();

                if (m_dtbSubProcess != null)
                {
                    if (m_dtbSubProcess.Rows.Count > 0)
                    {
                        for (int i = 0; i < m_dtbSubProcess.Rows.Count; i++)
                        {
                            for (int j = 0; j < DTabTemp.Rows.Count; j++)
                            {
                                if (m_dtbSubProcess.Rows[i]["lot_id"].ToString() == DTabTemp.Rows[j]["lot_id"].ToString())
                                {
                                    Global.Message(m_dtbSubProcess.Rows[i]["lot_id"].ToString() + " = Lot ID already added to the Grid list!");
                                    return;
                                }
                            }
                        }
                    }
                }


                if (m_dtbSubProcess.Rows.Count > 0)
                {
                    DTabTemp = Stock_Data.Copy();
                    m_dtbSubProcess.Merge(DTabTemp);
                }
                else
                {
                    m_dtbSubProcess = Stock_Data.Copy();
                }
                grdBoilingNo.DataSource = m_dtbSubProcess;
                grdBoilingNo.RefreshDataSource();
                dgvBoilingNo.BestFitColumns();
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
            }
        }
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
        private bool Save_Validate()
        {
            bool blnReturn = true;
            try
            {
                if (!Validate_PopUp())
                {
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
        public void GetStock()
        {
            try
            {
                if (Save_Validate())
                {
                    DTab_StockData = objJangedReturn.Boiling_Stock_GetData(Val.ToInt64(0), 0, Val.ToInt32(txtBoilingNo.Text), Val.ToInt64(lueKapan.EditValue), Val.ToInt64(lueCutNo.EditValue)); // objJangedReturn.ReturnStock_GetData();
                }

                if (DTab_StockData.Rows.Count > 0)
                {
                    FrmMFGStockConfirm FrmStockConfirm = new FrmMFGStockConfirm();
                    FrmStockConfirm.FrmMFGBoilingNo = this;
                    FrmStockConfirm.DTab = DTab_StockData;
                    FrmStockConfirm.ShowForm(this);
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
            }
        }

        #endregion

        #region Export Grid

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
                            dgvBoilingNo.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvBoilingNo.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvBoilingNo.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvBoilingNo.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvBoilingNo.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvBoilingNo.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvBoilingNo.ExportToCsv(Filepath);
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

        private void txtLotId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
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
    }
}
