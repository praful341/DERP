using BLL;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.PropertyClasses.Transaction.MFG;
using DERP.Class;
using System;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Transaction
{
    public partial class FrmMfgDepartmentTransferConfirm : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        //BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        MfgDepartmentTransferConfirm objDepartmentCnf;
        DataTable m_dtbDetails;
        int m_numForm_id;
        int IntRes;
        int IntRes1;
        int m_numSelectedCount;
        int filterFlag;
        #endregion

        #region Constructor
        public FrmMfgDepartmentTransferConfirm()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            objDepartmentCnf = new MfgDepartmentTransferConfirm();
            m_dtbDetails = new DataTable();

            m_numForm_id = 0;
            IntRes = 0;
            IntRes1 = 0;
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
            this.Show();
        }
        private void AttachFormEvents()
        {
            objBOFormEvents.CurForm = this;
            objBOFormEvents.FormKeyPress = true;
            objBOFormEvents.FormKeyDown = true;
            objBOFormEvents.FormResize = true;
            objBOFormEvents.FormClosing = true;
            objBOFormEvents.ObjToDispose.Add(objDepartmentCnf);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion   

        #region Events     
        private void FrmDepartmentTransferConfirm_Load(object sender, EventArgs e)
        {
            dtpConfirmDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            dtpConfirmDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            dtpConfirmDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            dtpConfirmDate.Properties.CharacterCasing = CharacterCasing.Upper;
            dtpConfirmDate.EditValue = DateTime.Now;

            //Task.Run(() => Global.LOOKUPWagesType(lueWagesType));
            //Task.Run(() => Global.LOOKUPActiveManagerName(lueManager));
            //Task.Run(() => Global.LOOKUPRoughSieveWages(lueWagesSieve));
            //Task.Run(() => GetData());

            Global.LOOKUPTypeOfWages(RepWagesType);
            Global.LOOKUPWagesType(lueWagesType);
            Global.LOOKUPManagerRep(RepManager);
            Global.LOOKUPRoughSieveWagesRep(RepSieve);
            Global.LOOKUPActiveManagerName(lueManager);
            Global.LOOKUPRoughSieveWages(lueWagesSieve);
            GetData();
        }
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                ObjPer.SetFormPer();
                if (ObjPer.AllowUpdate == false || ObjPer.AllowInsert == false)
                {
                    Global.Message(BLL.GlobalDec.gStrPermissionInsUpdMsg);
                    return;
                }
                btnConfirm.Enabled = false;
                m_dtbDetails.AcceptChanges();
                if (m_dtbDetails.Rows.Count > 0 && m_dtbDetails.Select("SEL = true").Length > 0)
                {
                    DataTable CheckDT = new DataTable();
                    CheckDT = m_dtbDetails.Select("SEL <> False").CopyToDataTable();
                    if (GlobalDec.gEmployeeProperty.role_name.ToUpper() == "SURAT SARIN" || GlobalDec.gEmployeeProperty.role_name.ToUpper() == "SURAT SARIN ENTRY" || GlobalDec.gEmployeeProperty.role_name.ToUpper() == "SURAT KATARGAM" || GlobalDec.gEmployeeProperty.role_name.ToUpper() == "SURAT KAMALA")
                    {
                        foreach (DataRow DRow in CheckDT.Rows)
                        {
                            if (Val.ToInt64(DRow["packet_type_id"]) == 0)
                            {
                                Global.Message("Please Check Wages Type not be blank");
                                btnConfirm.Enabled = true;
                                return;
                            }
                            if (Val.ToInt64(DRow["manager_id"]) == 0)
                            {
                                Global.Message("Please Check Manager Name not be blank");
                                btnConfirm.Enabled = true;
                                return;
                            }
                            if (Val.ToInt64(DRow["factory_wages_sieve_id"]) == 0)
                            {
                                Global.Message("Please Check Wages Sieve not be blank");
                                btnConfirm.Enabled = true;
                                return;
                            }
                        }
                    }
                    else if (GlobalDec.gEmployeeProperty.role_name.ToUpper() == "SURAT 4P" || GlobalDec.gEmployeeProperty.role_name.ToUpper() == "SURAT KATARGAM")
                    {
                        foreach (DataRow DRow in CheckDT.Rows)
                        {
                            if (Val.ToInt64(DRow["packet_type_id"]) == 0)
                            {
                                Global.Message("Please Check Wages Type not be blank");
                                btnConfirm.Enabled = true;
                                return;
                            }
                            if (Val.ToInt64(DRow["manager_id"]) == 0)
                            {
                                Global.Message("Please Check Manager Name not be blank");
                                btnConfirm.Enabled = true;
                                return;
                            }
                        }
                    }
                    else if (GlobalDec.gEmployeeProperty.role_name.ToUpper() == "SURAT RUSSIAN" || GlobalDec.gEmployeeProperty.user_name.ToUpper() == "VISHAL4P")
                    {
                        foreach (DataRow DRow in CheckDT.Rows)
                        {
                            if (Val.ToInt64(DRow["manager_id"]) == 0)
                            {
                                Global.Message("Please Check Manager Name not be blank");
                                btnConfirm.Enabled = true;
                                return;
                            }
                        }
                    }

                    DialogResult result = MessageBox.Show("Do you want to Confirm data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                    if (result != DialogResult.Yes)
                    {
                        btnConfirm.Enabled = true;
                        return;
                    }

                    DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;

                    panelProgress.Visible = true;
                    backgroundWorker_DeptConfirm.RunWorkerAsync();
                }
                else
                {
                    General.ShowErrors("Atleast 1 Lot must be select in grid.");
                }
                btnConfirm.Enabled = true;
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
        private void txtLotID_Validated(object sender, EventArgs e)
        {
            if (txtLotID.Text.Length == 0)
            {
                return;
            }

            if (GlobalDec.gEmployeeProperty.role_name.ToUpper() == "SURAT SARIN" || GlobalDec.gEmployeeProperty.role_name.ToUpper() == "SURAT SARIN ENTRY" || GlobalDec.gEmployeeProperty.role_name.ToUpper() == "SURAT 4P" || GlobalDec.gEmployeeProperty.role_name.ToUpper() == "SURAT KATARGAM" || GlobalDec.gEmployeeProperty.role_name.ToUpper() == "SURAT KAMALA")
            {
                if (lueWagesType.EditValue == null)
                {
                    Global.Message("Please Select Wages type.");
                    lueWagesType.Focus();
                    return;
                }
                else if (lueManager.EditValue == null)
                {
                    Global.Message("Please Select Manager Name.");
                    lueManager.Focus();
                    return;
                }
            }

            foreach (DataRow DRow in m_dtbDetails.Rows)
            {
                if (Val.ToString(DRow["lot_id"]) == txtLotID.Text)
                {
                    DRow["SEL"] = true;
                    if (lueWagesType.EditValue != null)
                    {
                        DRow["packet_type_id"] = Val.ToInt64(lueWagesType.EditValue);
                    }

                    if (lueManager.EditValue != null)
                    {
                        DRow["manager_id"] = Val.ToInt64(lueManager.EditValue);
                    }
                    if (lueWagesSieve.EditValue != null)
                    {
                        DRow["factory_wages_sieve_id"] = Val.ToInt64(lueWagesSieve.EditValue);
                    }
                }
            }
            GetSummary();
            txtLotID.Text = "";
            txtLotID.Focus();
        }
        private void dgvDepartmentTransferConfirm_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                Int64 Janged_No = Val.ToInt64(dgvDepartmentTransferConfirm.GetRowCellDisplayText(e.RowHandle, dgvDepartmentTransferConfirm.Columns["janged_no"]));

                if (Janged_No != 0)
                {
                    e.Appearance.BackColor = Color.Gray;
                }
                else
                {
                    e.Appearance.BackColor = Color.Transparent;
                }
            }
        }
        private void repChkSel_CheckedChanged(object sender, EventArgs e)
        {
            GetSummary();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (GlobalDec.gEmployeeProperty.role_name.ToUpper() == "SURAT SARIN" || GlobalDec.gEmployeeProperty.role_name.ToUpper() == "SURAT SARIN ENTRY" || GlobalDec.gEmployeeProperty.role_name.ToUpper() == "SURAT KATARGAM" || GlobalDec.gEmployeeProperty.role_name.ToUpper() == "SURAT KAMALA" || GlobalDec.gEmployeeProperty.role_name.ToUpper() == "GALAXY DW" || GlobalDec.gEmployeeProperty.role_name.ToUpper() == "KAMALA ADMIN")
            {
                m_dtbDetails.AcceptChanges();
                if (lueWagesType.EditValue != null)
                {
                    if (m_dtbDetails.Select("SEL = true").Length > 0)
                    {
                        for (int i = 0; i < m_dtbDetails.Rows.Count; i++)
                        {
                            if (m_dtbDetails.Rows[i]["SEL"].ToString() == "True")
                            {
                                m_dtbDetails.Rows[i]["packet_type_id"] = Val.ToInt64(lueWagesType.EditValue);
                            }
                        }
                    }
                    else
                    {
                        Global.Message("Atleast One Lot Select");
                        return;
                    }
                }
                else
                {
                    Global.Message("Please Select Wages type.");
                    return;
                }

                if (lueManager.EditValue != null)
                {
                    m_dtbDetails.AcceptChanges();
                    if (m_dtbDetails.Select("SEL = true").Length > 0)
                    {
                        for (int i = 0; i < m_dtbDetails.Rows.Count; i++)
                        {
                            if (m_dtbDetails.Rows[i]["SEL"].ToString() == "True")
                            {
                                m_dtbDetails.Rows[i]["manager_id"] = Val.ToInt64(lueManager.EditValue);
                            }
                        }
                    }
                    else
                    {
                        Global.Message("Atleast One Lot Select");
                        return;
                    }
                }
                else
                {
                    Global.Message("Please Select Manager Name.");
                    return;
                }
                if (lueWagesSieve.EditValue != null)
                {
                    m_dtbDetails.AcceptChanges();
                    if (m_dtbDetails.Select("SEL = true").Length > 0)
                    {
                        for (int i = 0; i < m_dtbDetails.Rows.Count; i++)
                        {
                            if (m_dtbDetails.Rows[i]["SEL"].ToString() == "True")
                            {
                                m_dtbDetails.Rows[i]["factory_wages_sieve_id"] = Val.ToInt64(lueWagesSieve.EditValue);
                            }
                        }
                    }
                    else
                    {
                        Global.Message("Atleast One Lot Select");
                        return;
                    }
                }
                else
                {
                    Global.Message("Please Select Wages Sieve.");
                    return;
                }
            }
            else if (GlobalDec.gEmployeeProperty.role_name.ToUpper() == "SURAT 4P" || GlobalDec.gEmployeeProperty.role_name.ToUpper() == "SURAT KATARGAM" || GlobalDec.gEmployeeProperty.role_name.ToUpper() == "SURAT KAMALA")
            {
                if (lueWagesType.EditValue != null)
                {
                    m_dtbDetails.AcceptChanges();
                    if (m_dtbDetails.Select("SEL = true").Length > 0)
                    {
                        for (int i = 0; i < m_dtbDetails.Rows.Count; i++)
                        {
                            if (m_dtbDetails.Rows[i]["SEL"].ToString() == "True")
                            {
                                m_dtbDetails.Rows[i]["packet_type_id"] = Val.ToInt64(lueWagesType.EditValue);
                            }
                        }
                    }
                    else
                    {
                        Global.Message("Atleast One Lot Select");
                        return;
                    }
                }
                else
                {
                    Global.Message("Please Select Wages type.");
                    return;
                }

                if (lueManager.EditValue != null)
                {
                    m_dtbDetails.AcceptChanges();
                    if (m_dtbDetails.Select("SEL = true").Length > 0)
                    {
                        for (int i = 0; i < m_dtbDetails.Rows.Count; i++)
                        {
                            if (m_dtbDetails.Rows[i]["SEL"].ToString() == "True")
                            {
                                m_dtbDetails.Rows[i]["manager_id"] = Val.ToInt64(lueManager.EditValue);
                            }
                        }
                    }
                    else
                    {
                        Global.Message("Atleast One Lot Select");
                        return;
                    }
                }
                else
                {
                    Global.Message("Please Select Manager Name.");
                    return;
                }
            }
            else if (GlobalDec.gEmployeeProperty.role_name.ToUpper() == "SURAT RUSSIAN" || GlobalDec.gEmployeeProperty.role_name.ToUpper() == "SURAT KAMALA")
            {
                m_dtbDetails.AcceptChanges();
                if (lueManager.EditValue != null)
                {
                    if (m_dtbDetails.Select("SEL = true").Length > 0)
                    {
                        for (int i = 0; i < m_dtbDetails.Rows.Count; i++)
                        {
                            if (m_dtbDetails.Rows[i]["SEL"].ToString() == "True")
                            {
                                m_dtbDetails.Rows[i]["manager_id"] = Val.ToInt64(lueManager.EditValue);
                            }
                        }
                    }
                    else
                    {
                        Global.Message("Atleast One Lot Select");
                        return;
                    }
                }
                else
                {
                    Global.Message("Please Select Manager Name.");
                    return;
                }
            }
            if (GlobalDec.gEmployeeProperty.role_name.ToUpper() == "SURAT POLISH REPARING")
            {
                m_dtbDetails.AcceptChanges();

                if (lueManager.EditValue != null)
                {
                    m_dtbDetails.AcceptChanges();
                    if (m_dtbDetails.Select("SEL = true").Length > 0)
                    {
                        for (int i = 0; i < m_dtbDetails.Rows.Count; i++)
                        {
                            if (m_dtbDetails.Rows[i]["SEL"].ToString() == "True")
                            {
                                m_dtbDetails.Rows[i]["manager_id"] = Val.ToInt64(lueManager.EditValue);
                            }
                        }
                    }
                    else
                    {
                        Global.Message("Atleast One Lot Select");
                        return;
                    }
                }
                else
                {
                    Global.Message("Please Select Manager Name.");
                    return;
                }
            }
            if (GlobalDec.gEmployeeProperty.user_name.ToUpper() == "VISHAL4P" || GlobalDec.gEmployeeProperty.user_name.ToUpper() == "PRINCE")
            {
                m_dtbDetails.AcceptChanges();

                if (lueManager.EditValue != null)
                {
                    m_dtbDetails.AcceptChanges();
                    if (m_dtbDetails.Select("SEL = true").Length > 0)
                    {
                        for (int i = 0; i < m_dtbDetails.Rows.Count; i++)
                        {
                            if (m_dtbDetails.Rows[i]["SEL"].ToString() == "True")
                            {
                                m_dtbDetails.Rows[i]["manager_id"] = Val.ToInt64(lueManager.EditValue);
                            }
                        }
                    }
                    else
                    {
                        Global.Message("Atleast One Lot Select");
                        return;
                    }
                }
                else
                {
                    Global.Message("Please Select Manager Name.");
                    return;
                }
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtLotID.Text = "";
            lueManager.EditValue = null;
            lueWagesSieve.EditValue = null;
            lueWagesType.EditValue = null;

            dtpConfirmDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            dtpConfirmDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            dtpConfirmDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            dtpConfirmDate.Properties.CharacterCasing = CharacterCasing.Upper;
            dtpConfirmDate.EditValue = DateTime.Now;
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
                //if(col)
                if (filterFlag == 0)
                {
                    for (int i = 0; i < dgvDepartmentTransferConfirm.RowCount; i++)
                    {
                        //if (dgvDepartmentTransferConfirm.IsFilterRow(i))
                        dgvDepartmentTransferConfirm.SetRowCellValue(i, "SEL", chkAll.Checked);
                    }
                    //foreach (Dow DRow in m_dtbDetails.Rows)
                    //{
                    //    dgvDepartmentTransferConfirm.IsFilterRow(DRo)
                    //    DRow["SEL"] = chkAll.Checked;
                    //}
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
        private void backgroundWorker_BTConfirm_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.Default;
                //Conn = new BeginTranConnection(true, false);
                MFGDepartmentTransferProperty DepartmentTransferProperty = new MFGDepartmentTransferProperty();

                try
                {
                    IntRes = 0;
                    IntRes1 = 0;

                    //List<ListError> lstError = new List<ListError>();

                    int IntCounter = 0;
                    int Count = 0;

                    if (m_dtbDetails.Select("SEL = true").Length > 0)
                    {
                        m_dtbDetails = m_dtbDetails.Select("SEL = true").CopyToDataTable();
                        int TotalCount = m_dtbDetails.Rows.Count;

                        foreach (DataRow DRow in m_dtbDetails.Rows)
                        {
                            DepartmentTransferProperty.transfer_id = Val.ToInt(DRow["transfer_id"]);
                            DepartmentTransferProperty.union_id = Val.ToInt(DRow["union_id"]);
                            DepartmentTransferProperty.janged_no = Val.ToInt64(DRow["janged_no"]);
                            DepartmentTransferProperty.packet_type_wages_id = Val.ToInt64(DRow["packet_type_id"]);
                            DepartmentTransferProperty.lot_id = Val.ToInt64(DRow["lot_id"]);
                            DepartmentTransferProperty.wages_sieve_id = Val.ToInt32(DRow["factory_wages_sieve_id"]);

                            if (GlobalDec.gEmployeeProperty.role_name.ToUpper() == "SURAT POLISH REPARING")
                            {
                                DepartmentTransferProperty.manager_id = Val.ToInt64(34538);
                            }
                            else
                            {
                                DepartmentTransferProperty.manager_id = Val.ToInt64(DRow["manager_id"]);
                            }
                            DepartmentTransferProperty.receive_date = Val.DBDate(dtpConfirmDate.Text);

                            IntRes = objDepartmentCnf.Save(DepartmentTransferProperty);

                            Count++;
                            IntCounter++;
                            IntRes++;
                            SetControlPropertyValue(lblProgressCount, "Text", Count.ToString() + "" + "/" + "" + TotalCount.ToString() + " Completed....");
                        }
                    }
                    if (IntRes == -1)
                    {
                        Global.Confirm("Error In Pending Confirm");
                    }
                    else
                    {
                        //Count++;
                        //IntCounter++;
                        //IntRes1++;
                        //SetControlPropertyValue(lblProgressCount, "Text", Count.ToString() + "" + "/" + "" + TotalCount.ToString() + " Completed....");
                    }
                    //Conn.Inter1.Commit();
                }
                catch (Exception ex)
                {
                    IntRes = -1;
                    IntRes1 = -1;
                    //Conn.Inter1.Rollback();
                    //Conn = null;
                    General.ShowErrors(ex.ToString());
                    return;
                }
                finally
                {
                    DepartmentTransferProperty = null;
                }
            }
            catch (Exception ex)
            {
                IntRes = -1;
                IntRes1 = -1;
                //Conn.Inter1.Rollback();
                //Conn = null;
                General.ShowErrors(ex.ToString());
                if (ex.InnerException != null)
                {
                    Global.Message(ex.InnerException.ToString());
                }
            }
        }
        private void backgroundWorker_BTConfirm_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                panelProgress.Visible = false;
                if (IntRes1 > 0 || IntRes > 0)
                {
                    Global.Confirm("Pending Confirm Successfully");
                    m_dtbDetails = null;
                    GetData();
                    CalculateTotal();
                    txtLotID.Text = "";
                    lueWagesType.EditValue = null;
                    lueManager.EditValue = null;
                    lueWagesSieve.EditValue = null;
                    lueWagesType.Focus();
                    this.Cursor = Cursors.Default;
                }
                else
                {
                    Global.Confirm("Error In Pending Confirm");
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            GetData();
        }

        #region GridEvents
        private void dgvDepartmentTransferConfirm_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "SEL")
            {
                if (Val.ToBoolean(dgvDepartmentTransferConfirm.GetRowCellValue(e.RowHandle, "SEL")) == true)
                {
                    dgvDepartmentTransferConfirm.SetRowCellValue(e.RowHandle, "SEL", false);
                }
                else
                {
                    dgvDepartmentTransferConfirm.SetRowCellValue(e.RowHandle, "SEL", true);
                }
            }
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
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        private void dgvDepartmentTransferConfirm_ColumnFilterChanged(object sender, EventArgs e)
        {
            if (dgvDepartmentTransferConfirm.ActiveFilterString != "")
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
        }
        #endregion

        #endregion

        #region Functions
        public void GetData()
        {
            try
            {
                m_dtbDetails = objDepartmentCnf.GetData();
                if (m_dtbDetails.Columns.Contains("SEL") == false)
                {
                    if (m_dtbDetails.Columns.Contains("SEL") == false)
                    {
                        DataColumn Col = new DataColumn();
                        Col.ColumnName = "SEL";
                        Col.DataType = typeof(bool);
                        Col.DefaultValue = false;
                        m_dtbDetails.Columns.Add(Col);
                    }
                }
                m_dtbDetails.Columns["SEL"].SetOrdinal(0);
                grdDepartmentTransferConfirm.DataSource = m_dtbDetails;

                grdDepartmentTransferConfirm.InvokeEx(t =>
                {
                    t.DataSource = m_dtbDetails;
                    dgvDepartmentTransferConfirm.BestFitColumns();
                });

                dgvDepartmentTransferConfirm.BestFitColumns();
                GetSummary();
                txtSelLot.EditValue = "";
                txtSelPcs.EditValue = "";
                txtSelCarat.EditValue = "";
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void GetSummary()
        {
            try
            {
                double IntSelPcs = 0; double IntSelLot = 0; double DouSelCarat = 0;
                System.Data.DataTable DtTransfer = (System.Data.DataTable)grdDepartmentTransferConfirm.DataSource;
                dgvDepartmentTransferConfirm.PostEditor();
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
                                IntSelPcs = IntSelPcs + Val.Val(DRow["pcs"]) + Val.Val(DRow["rr_pcs"]);
                                DouSelCarat = DouSelCarat + Val.Val(DRow["carat"]) + Val.Val(DRow["rr_carat"]);
                            }
                        }
                    }
                }
                txtSelLot.Text = IntSelLot.ToString();
                txtSelPcs.Text = IntSelPcs.ToString();
                txtSelCarat.Text = DouSelCarat.ToString();
            }
            catch
            {
            }
        }
        private void CalculateTotal()
        {
            try
            {
                m_numSelectedCount = 0;
                dgvDepartmentTransferConfirm.UpdateCurrentRow();
                m_dtbDetails.AcceptChanges();
                foreach (DataRow drw in m_dtbDetails.Rows)
                {
                    if (Val.ToBoolean(drw["SEL"]) == true)
                    {
                        m_numSelectedCount += 1;
                    }
                }
                dgvDepartmentTransferConfirm.UpdateTotalSummary();
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
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
                            dgvDepartmentTransferConfirm.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvDepartmentTransferConfirm.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvDepartmentTransferConfirm.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvDepartmentTransferConfirm.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvDepartmentTransferConfirm.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvDepartmentTransferConfirm.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvDepartmentTransferConfirm.ExportToCsv(Filepath);
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

        private void txtLotID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
