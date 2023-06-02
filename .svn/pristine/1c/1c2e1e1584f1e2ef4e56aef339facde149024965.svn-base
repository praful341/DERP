using BLL;
using BLL.FunctionClasses.Transaction;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using DERP.Master.MFG;
using DERP.Transaction.MFG;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DREP.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DREP.Transaction
{
    public partial class FrmMFGKapanCreate : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        MFGKapanCreate objMFGKapanCreate;
        MFGPurchase objMfgRoughStk;
        BranchTransfer objBranch;

        DataTable DTab_KapanCreate;
        DataTable m_dtbRoughStock;
        DataTable m_dtbType;

        int m_numForm_id;
        int IntRes;

        string Party_Memo_No;
        string Company_Memo_No;

        decimal m_numTotalCarats;
        decimal m_numTotalAmount;

        double m_Balance_Carat;

        bool m_blnsave;
        #endregion

        #region Constructor
        public FrmMFGKapanCreate()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            objMFGKapanCreate = new MFGKapanCreate();
            objMfgRoughStk = new MFGPurchase();
            objBranch = new BranchTransfer();

            DTab_KapanCreate = new DataTable();
            m_dtbRoughStock = new DataTable();
            m_dtbType = new DataTable();

            m_numForm_id = 0;
            IntRes = 0;

            Party_Memo_No = string.Empty;
            Company_Memo_No = string.Empty;

            m_numTotalCarats = 0;
            m_numTotalAmount = 0;

            m_Balance_Carat = 0;
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
            this.Show();
        }
        private void AttachFormEvents()
        {
            objBOFormEvents.CurForm = this;
            objBOFormEvents.FormKeyPress = true;
            objBOFormEvents.FormKeyDown = true;
            objBOFormEvents.FormResize = true;
            objBOFormEvents.FormClosing = true;
            objBOFormEvents.ObjToDispose.Add(objMFGKapanCreate);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);

        }

        #endregion

        #region Events
        private void FrmMFGKapanCreate_Load(object sender, EventArgs e)
        {
            try
            {
                GetData();
                ClearDetails();
                m_dtbType = new DataTable();
                m_dtbType.Columns.Add("type");
                m_dtbType.Rows.Add("NORMAL");
                m_dtbType.Rows.Add("REJECTION");

                lueType.Properties.DataSource = m_dtbType;
                lueType.Properties.ValueMember = "type";
                lueType.Properties.DisplayMember = "type";
                lueType.EditValue = "NORMAL";
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
                //string Str = "";
                //if (Val.DBDate(BLL.GlobalDec.gStrServerDate) != Val.DBDate(dtpKapanDate.Text))
                //{
                //    Str = GlobalDec.CheckLockIsOpenOrNot(Val.DBDate(dtpKapanDate.Text), Val.DBTime(DateTime.Now.ToShortTimeString())); //Val.GetFullTime12());
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
                //        dtpKapanDate.Enabled = true;
                //        dtpKapanDate.Visible = true;
                //    }
                //}
                btnSave.Enabled = false;
                m_blnsave = true;
                if (!ValidateDetails())
                {
                    m_blnsave = false;
                    btnSave.Enabled = true;
                    btnSave.Focus();
                    return;
                }
                DialogResult result = MessageBox.Show("Do you want to Create Kapan data?", "Confirmation", MessageBoxButtons.YesNo);
                if (result != DialogResult.Yes)
                {
                    btnSave.Enabled = true;
                    return;
                }

                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;

                backgroundWorker_KapanCreate.RunWorkerAsync();

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
        private void btnExport_Click(object sender, EventArgs e)
        {
            Global.Export("xlsx", dgvKapnaCreate);
        }
        private void txtRate_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtAmount.Text = Val.ToString(Math.Round(Val.ToDecimal(txtRate.Text) * (Val.ToDecimal(lblTotalCrt.Text) + Val.ToDecimal(lblkapancarat.Text)), 0));
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void lblTotalCrt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtAmount.Text = Val.ToString(Math.Round(Val.ToDecimal(txtRate.Text) * (Val.ToDecimal(lblTotalCrt.Text) + Val.ToDecimal(lblkapancarat.Text)), 0));
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void txtKapanNo_Validated(object sender, EventArgs e)
        {
            try
            {
                DataTable dtKapanExist = new DataTable();
                dtKapanExist = objMFGKapanCreate.CheckKapan(Val.ToString(txtKapanNo.Text));
                if (dtKapanExist.Rows.Count > 0)
                {
                    Global.Confirm("Kapan No Already Exist");
                    txtKapanNo.Focus();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void lueManager_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmEmployeeMaster frmManager = new FrmEmployeeMaster();
                frmManager.ShowDialog();
                Global.LOOKUPManager(lueManager);
            }
        }
        private void lueEmployee_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmEmployeeMaster frmEmployee = new FrmEmployeeMaster();
                frmEmployee.ShowDialog();
                Global.LOOKUPEmp(lueEmployee);
            }
        }
        private void lueTeam_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmMfgTeamMaster frmTeam = new FrmMfgTeamMaster();
                frmTeam.ShowDialog();
                Global.LOOKUPTeam(lueTeam);
            }
        }
        private void lueGroup_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmMfgGroupMaster frmGroup = new FrmMfgGroupMaster();
                frmGroup.ShowDialog();
                Global.LOOKUPGroup(lueGroup);
            }
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ClearDetails();
        }
        private void backgroundWorker_KapanCreate_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            IntRes = 0;

            string p_numFromStockRate = string.Empty;
            Int64 NewLotid = 0;
            Int64 NewKapanid = 0;
            Int64 NewHistory_Union_Id = 0;
            Int64 Lot_SrNo = 0;
            Conn = new BeginTranConnection(true, false);
            //double mix_Detail_Carat = Val.ToDouble(dgvKapnaCreate.Columns["trf_carat"].SummaryText);
            //double Split_Detail_Carat = Math.Round(Val.ToDouble(dgvSplitDetails.Columns["trf_carat"].SummaryText) + Val.ToDouble(dgvSplitDetails.Columns["weight_loss"].SummaryText) + Val.ToDouble(dgvSplitDetails.Columns["weight_plus"].SummaryText), 2);

            MFGKapanCreateProperty objKapanProperty = new MFGKapanCreateProperty();
            try
            {
                DTab_KapanCreate = (DataTable)grdKapanCreate.DataSource;
                if (DTab_KapanCreate.Select("transfer_carat > 0").Length > 0)
                {
                    DTab_KapanCreate = DTab_KapanCreate.Select("transfer_carat > 0").CopyToDataTable();

                    objKapanProperty.lot_id = Val.ToInt64(txtLotID.Text);
                    //objKapanProperty.lot_id = Val.ToInt64(0);
                    //objKapanProperty.mixsplit_srno = Val.ToInt64(NewMixSplitid);
                    objKapanProperty.kapan_date = Val.DBDate(dtpKapanDate.Text);
                    //objKapanProperty.mixsplit_time = Val.ToString(GlobalDec.gStr_SystemTime);
                    objKapanProperty.company_id = Val.ToInt(GlobalDec.gEmployeeProperty.company_id);
                    objKapanProperty.branch_id = Val.ToInt(GlobalDec.gEmployeeProperty.branch_id);
                    objKapanProperty.location_id = Val.ToInt(GlobalDec.gEmployeeProperty.location_id);
                    objKapanProperty.department_id = Val.ToInt(GlobalDec.gEmployeeProperty.department_id);
                    objKapanProperty.rough_sieve_id = Val.ToInt(DTab_KapanCreate.Rows[0]["rough_sieve_id"]);
                    objKapanProperty.rough_shade_id = Val.ToInt(DTab_KapanCreate.Rows[0]["rough_shade_id"]);
                    objKapanProperty.pcs = Val.ToInt(lblTotalPcs.Text);
                    objKapanProperty.carat = Val.ToDecimal(lblTotalCrt.Text);

                    decimal Rate = Val.ToDecimal(txtRate.Text);
                    decimal Amount = Val.ToDecimal(txtAmount.Text);
                    decimal Carat = Val.ToDecimal(lblTotalCrt.Text) + Val.ToDecimal(lblkapancarat.Text);
                    decimal Rate_Create = Val.ToDecimal(Math.Round(Val.ToDecimal(Val.ToDecimal(Amount) / Val.ToDecimal(Carat)), 3));
                    decimal Amount_Create = Val.ToDecimal(Math.Round(Val.ToDecimal(txtRate.Text) * Val.ToDecimal(Carat), 3));

                    objKapanProperty.rate = Val.ToDecimal(Rate_Create);
                    objKapanProperty.amount = Val.ToDecimal(Amount_Create);
                    objKapanProperty.form_id = m_numForm_id;

                    objKapanProperty = objMFGKapanCreate.Save_MfgStock(objKapanProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                    NewLotid = Val.ToInt64(objKapanProperty.lot_id);

                    //objKapanProperty.kapan_id = Val.ToInt(0);
                    objKapanProperty.kapan_id = Val.ToInt(txtKapanID.Text);
                    objKapanProperty.kapan_no = Val.ToString(txtKapanNo.Text);
                    objKapanProperty.team_id = Val.ToInt(lueTeam.EditValue);
                    objKapanProperty.group_id = Val.ToInt(lueGroup.EditValue);
                    objKapanProperty.manager_id = Val.ToInt(lueManager.EditValue);
                    objKapanProperty.employee_id = Val.ToInt(lueEmployee.EditValue);
                    objKapanProperty.currency_id = Val.ToInt(GlobalDec.gEmployeeProperty.currency_id);
                    objKapanProperty.remarks = Val.ToString(txtEntry.Text);
                    objKapanProperty.special_remarks = Val.ToString(txtJKK.Text);
                    objKapanProperty.client_remarks = Val.ToString(txtSaleRemark.Text);
                    objKapanProperty.payment_remarks = Val.ToString(txtAccountRemark.Text);
                    objKapanProperty.type = Val.ToString(lueType.Text);
                    objKapanProperty.history_union_id = NewHistory_Union_Id;
                    objKapanProperty.lot_srno = Lot_SrNo;
                    objKapanProperty = objMFGKapanCreate.KapanSave(objKapanProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                    NewKapanid = Val.ToInt64(objKapanProperty.kapan_id);
                    NewHistory_Union_Id = Val.ToInt64(objKapanProperty.history_union_id);
                    Lot_SrNo = Val.ToInt64(objKapanProperty.lot_srno);

                    objKapanProperty.to_rate = Val.ToDecimal(Rate_Create);
                    objKapanProperty.to_amount = Math.Round(Val.ToDecimal(txtRate.Text) * Val.ToDecimal(lblTotalCrt.Text), 3);

                    for (int i = 0; i < DTab_KapanCreate.Rows.Count; i++)
                    {
                        objKapanProperty = new MFGKapanCreateProperty();

                        objKapanProperty.to_rate = Val.ToDecimal(Rate_Create);
                        objKapanProperty.to_amount = Math.Round(Val.ToDecimal(txtRate.Text) * Val.ToDecimal(lblTotalCrt.Text), 3);

                        objKapanProperty.kapan_date = Val.DBDate(dtpKapanDate.Text);
                        objKapanProperty.mixsplit_id = Val.ToInt(0);
                        objKapanProperty.mix_type_id = Val.ToInt(1);
                        objKapanProperty.from_lot_id = Val.ToInt(DTab_KapanCreate.Rows[i]["lot_id"]);
                        objKapanProperty.to_lot_id = NewLotid;
                        objKapanProperty.from_kapan_id = Val.ToInt64(DTab_KapanCreate.Rows[i]["kapan_id"]);
                        objKapanProperty.to_kapan_id = NewKapanid;
                        objKapanProperty.transaction_type_id = Val.ToInt(4);
                        objKapanProperty.from_pcs = Val.ToInt(DTab_KapanCreate.Rows[i]["transfer_pcs"]);
                        objKapanProperty.to_pcs = Val.ToInt(lblTotalPcs.Text);
                        objKapanProperty.from_carat = Val.ToDecimal(DTab_KapanCreate.Rows[i]["transfer_carat"]);
                        objKapanProperty.to_carat = Val.ToDecimal(lblTotalCrt.Text);
                        objKapanProperty.company_id = Val.ToInt(GlobalDec.gEmployeeProperty.company_id);
                        objKapanProperty.branch_id = Val.ToInt(GlobalDec.gEmployeeProperty.branch_id);
                        objKapanProperty.location_id = Val.ToInt(GlobalDec.gEmployeeProperty.location_id);
                        objKapanProperty.department_id = Val.ToInt(GlobalDec.gEmployeeProperty.department_id);
                        objKapanProperty.form_id = m_numForm_id;
                        objKapanProperty.lot_srno = Lot_SrNo;

                        objKapanProperty.from_rate = Val.ToDecimal(DTab_KapanCreate.Rows[i]["rate"]);
                        objKapanProperty.from_amount = Math.Round(Val.ToDecimal(DTab_KapanCreate.Rows[i]["transfer_carat"]) * Val.ToDecimal(DTab_KapanCreate.Rows[i]["rate"]), 3);

                        IntRes = objMFGKapanCreate.Save_New(objKapanProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                    }
                }
                else
                {
                    Global.Message("No Any Mixing Data Found in Grid");
                    IntRes = 0;
                    return;
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
            finally
            {
                objKapanProperty = null;
            }
        }
        private void backgroundWorker_KapanCreate_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (IntRes > 0)
                {
                    Global.Message("Data Save Successfully");
                    ClearDetails();
                    GetData();
                }
                else if (IntRes == -1)
                {
                    Global.Confirm("Error In Kapan Save Data");
                    return;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }

        #region "Grid Events" 
        private void dgvBranchDetails_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                m_numTotalCarats = Math.Round(Val.ToDecimal(clmCarats.SummaryItem.SummaryValue), 2, MidpointRounding.AwayFromZero);

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
        private void CalculateGridAmount(int rowindex)
        {
            try
            {
                m_Balance_Carat = Math.Round(Val.ToDouble(dgvKapnaCreate.GetRowCellValue(rowindex, "balance_carat")), 3);
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
            }
        }
        //decimal sumAmount = 0;
        //decimal sumCarat = 0;
        //decimal sumRate = 0;
        //decimal Rate = 0;
        private void dgvKapnaCreate_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            try
            {
                decimal sumAmount = 0;
                decimal sumCarat = 0;

                lblTotalPcs.Text = Convert.ToString(Math.Round(Val.ToDecimal(clmTransferPcs.SummaryItem.SummaryValue), 3));
                lblTotalCrt.Text = Convert.ToString(Math.Round(Val.ToDecimal(clmTransferCarat.SummaryItem.SummaryValue), 3));

                DataTable dtRate = new DataTable();
                dtRate = (DataTable)grdKapanCreate.DataSource;

                if (dtRate.Select("transfer_carat > 0").Length > 0)
                {
                    dtRate = dtRate.Select("transfer_carat > 0").CopyToDataTable();

                    foreach (DataRow dr in dtRate.Rows)
                    {
                        sumAmount += Val.ToDecimal(dr["transfer_carat"]) * Val.ToDecimal(dr["rate"]);
                    }

                    decimal numKapan_Carat = Val.ToDecimal(lblkapancarat.Text);
                    decimal numKapan_KapanAmount = Val.ToDecimal(lblKapanAmount.Text);

                    sumCarat = Val.ToDecimal(dtRate.Compute("Sum(transfer_carat)", string.Empty));
                    txtRate.Text = Val.ToString(Math.Round((sumAmount + numKapan_KapanAmount) / (sumCarat + numKapan_Carat), 3));

                    //dtRate = dtRate.Select("transfer_carat > 0").CopyToDataTable();

                    //numTotalAmount = Val.ToDecimal(dtRate.Compute("Sum(balance_carat) * sum(rate) ", string.Empty));
                    //sumCarat = Val.ToDecimal(dtRate.Compute("Sum(balance_carat)", string.Empty));

                    //sumRate = Math.Round(Val.ToDecimal(numTotalAmount / sumCarat), 3);
                    //txtRate.Text = sumRate.ToString();

                    //foreach (DataRow Dr in dtRate.Rows)
                    //{
                    //    sumCarat = Val.ToDecimal(Dr["balance_carat"]);
                    //    sumRate = Val.ToDecimal(Dr["rate"]);
                    //    sumAmount = sumAmount + Math.Round(sumCarat * sumRate, 3);

                    //    Rate = Rate + sumRate;
                    //    txtRate.Text = (Rate).ToString();
                    //    txtAmount.Text = sumAmount.ToString();
                    //}


                    //sumAmount = Val.ToDecimal(dtRate.Compute("Sum(amount)", string.Empty));
                    //sumRate = Val.ToDecimal(dtRate.Compute("Sum(rate)", string.Empty));
                    //sumCarat = Val.ToDecimal(dtRate.Compute("Sum(balance_carat)", string.Empty));
                    //txtRate.Text = Val.ToString((Val.ToDecimal(sumCarat) * Val.ToDecimal(sumRate)) / sumCarat);


                    //txtRate.Text = Val.ToString(Math.Round(Val.ToDecimal(Val.ToDecimal(sumAmount) / Val.ToDecimal(sumCarat)), 3));
                    //txtRate.Text = Val.ToString(Math.Round(Val.ToDecimal(Val.ToDecimal(sumRate)), 3));
                    //txtAmount.Text = Val.ToString(Math.Round(Val.ToDecimal(Val.ToDecimal(sumRate) * Val.ToDecimal(sumCarat)), 3));
                }
                if (Val.ToDecimal(lblTotalCrt.Text) == 0)
                {
                    txtRate.Text = "0";
                    txtAmount.Text = "0";
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        private void dgvKapnaCreate_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            CalculateGridAmount(dgvKapnaCreate.FocusedRowHandle);
            GridView view = sender as GridView;
            try
            {
                if (view.FocusedColumn.FieldName == "transfer_carat")
                {
                    string brd = e.Value as string;
                    if (Val.ToDouble(brd) > Val.ToDouble(m_Balance_Carat))
                    {
                        e.Valid = false;
                        e.ErrorText = "Transfer Carat not more then Balance Carat.";
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }

        private void dgvKapan_RowClick(object sender, RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvKapan.GetDataRow(e.RowHandle);
                        txtLotID.Text = Val.ToString(Drow["lot_id"]);
                        dtpKapanDate.Text = Val.ToString(Drow["kapan_date"]);
                        txtKapanNo.Text = Val.ToString(Drow["kapan_no"]);
                        txtKapanID.Text = Val.ToString(Drow["kapan_id"]);

                        lblkapanpcs.Text = Val.ToString(Drow["pcs"]);
                        lblkapancarat.Text = Val.ToString(Drow["carat"]);
                        lblKapanRate.Text = Val.ToString(Drow["rate"]);
                        lblKapanAmount.Text = Val.ToString(Drow["amount"]);

                        txtEntry.Text = Val.ToString(Drow["remarks"]);
                        txtJKK.Text = Val.ToString(Drow["special_remarks"]);
                        txtSaleRemark.Text = Val.ToString(Drow["client_remarks"]);
                        txtAccountRemark.Text = Val.ToString(Drow["payment_remarks"]);
                        lueType.Text = Val.ToString(Drow["type"]);
                        lueTeam.EditValue = Val.ToInt64(Drow["team_id"]);
                        lueGroup.EditValue = Val.ToInt64(Drow["group_id"]);
                        lueEmployee.EditValue = Val.ToInt64(Drow["employee_id"]);
                        lueManager.EditValue = Val.ToInt64(Drow["manager_id"]);
                        txtKapanNo.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);

                return;
            }
        }

        private void grdKapanCreate_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    (grdKapanCreate.FocusedView as ColumnView).FocusedRowHandle++;
                    e.Handled = true;
                }
            }
            catch
            {
            }
        }

        #endregion

        #endregion

        #region Functions 
        public void GetData()
        {
            try
            {
                DataTable DTab = objMFGKapanCreate.GetData();
                grdKapan.DataSource = DTab;
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private bool ClearDetails()
        {
            bool blnReturn = true;
            try
            {
                if (!GenerateSaleInvoiceDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }
                dtpKapanDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpKapanDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpKapanDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpKapanDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpKapanDate.EditValue = DateTime.Now;
                txtKapanNo.Enabled = true;
                txtKapanNo.Text = string.Empty;
                lueTeam.EditValue = null;
                lueGroup.EditValue = null;
                lueManager.EditValue = null;
                txtRate.Text = "0";
                txtAmount.Text = "0";
                lblTotalCrt.Text = "0.00";
                lblTotalPcs.Text = "0";
                txtEntry.Text = string.Empty;
                txtJKK.Text = string.Empty;
                txtSaleRemark.Text = string.Empty;
                txtAccountRemark.Text = string.Empty;
                btnDelete.Enabled = true;
                txtPassword.Text = "";
                lueType.EditValue = "NORMAL";
                txtLotID.Text = "0";
                lblkapanpcs.Text = "0";
                lblkapancarat.Text = "0";
                lblKapanRate.Text = "0";
                lblKapanAmount.Text = "0";
                txtKapanID.Text = "0";
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
        }
        private bool GenerateSaleInvoiceDetails()
        {
            bool blnReturn = true;
            try
            {
                Global.LOOKUPGroup(lueGroup);
                Global.LOOKUPTeam(lueTeam);
                Global.LOOKUPManager(lueManager);

                dtpKapanDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpKapanDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpKapanDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpKapanDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpKapanDate.EditValue = DateTime.Now;

                m_dtbRoughStock = objMFGKapanCreate.GetRoughStock();
                grdKapanCreate.DataSource = m_dtbRoughStock;

                DataTable DTab = objMFGKapanCreate.GetData();
                grdKapan.DataSource = DTab;
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());

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
                    if (txtKapanNo.Text == "")
                    {
                        lstError.Add(new ListError(12, "Kapan No"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtKapanNo.Focus();
                        }
                    }
                    if (Val.ToDecimal(lblTotalCrt.Text) == 0 || lblTotalCrt.Text == "")
                    {
                        lstError.Add(new ListError(5, "Please Enter the transfer carat."));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                        }
                    }

                    var result = DateTime.Compare(Convert.ToDateTime(dtpKapanDate.Text), DateTime.Today);
                    if (result > 0)
                    {
                        lstError.Add(new ListError(5, " Invoice Date Not Be Greater Than Today Date"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            dtpKapanDate.Focus();
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
                            dgvKapnaCreate.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvKapnaCreate.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvKapnaCreate.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvKapnaCreate.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvKapnaCreate.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvKapnaCreate.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvKapnaCreate.ExportToCsv(Filepath);
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtKapanID.Text != "0")
            {
                //MFGKapanCreateProperty objKapanProperty = new MFGKapanCreateProperty();
                //objKapanProperty.kapan_id = Val.ToInt64(txtKapanID.Text);
                //objKapanProperty.kapan_date = Val.DBDate(dtpKapanDate.Text);

                FrmMFGKapanUpdateSearch FrmSearchKapan = new FrmMFGKapanUpdateSearch();
                FrmSearchKapan.FrmMFGKapan = this;
                FrmSearchKapan.ShowForm(this, Val.ToInt(txtKapanID.Text));
                //int IntRes = objMFGKapanCreate.Kapan_Date_Update(objKapanProperty);

                //if (IntRes > 0)
                //{
                //    Global.Confirm("Kapan Data Update Successfully");
                //    ClearDetails();
                //    GetData();
                //}
                //else
                //{
                //    Global.Confirm("Error In Update Kapan Data");
                //}
                ClearDetails();
            }
            else
            {
                Global.Message("Kapan ID are not Selected");
                return;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            MFGKapanCreateProperty objKapanProperty = new MFGKapanCreateProperty();
            try
            {
                DialogResult result = MessageBox.Show("Do you want to Delete Kapan data?", "Confirmation", MessageBoxButtons.YesNo);
                if (result != DialogResult.Yes)
                {
                    btnSave.Enabled = true;
                    return;
                }
                btnDelete.Enabled = false;
                Conn = new BeginTranConnection(true, false);


                objKapanProperty.lot_id = Val.ToInt64(txtLotID.Text);
                objKapanProperty.carat = Val.ToDecimal(lblkapancarat.Text);

                objKapanProperty = objMFGKapanCreate.Kapan_Delete(objKapanProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                Conn.Inter1.Commit();
                if (objKapanProperty.remarks != "")
                {
                    Global.Message(objKapanProperty.remarks);
                    btnDelete.Enabled = true;
                }
                else
                {
                    Global.Message("Data Deleted Successfully");
                    ClearDetails();
                    GetData();
                }

            }
            catch (Exception ex)
            {
                IntRes = -1;
                Conn.Inter1.Rollback();
                Conn = null;
                General.ShowErrors(ex.ToString());
                return;
            }
            finally
            {
                objKapanProperty = null;
            }
        }

        private void txtPassword_EditValueChanged(object sender, EventArgs e)
        {
            if (txtPassword.Text != "")
            {
                if (Val.ToString(txtPassword.Text) == "123")
                {
                    btnDelete.Visible = true;
                }
                else
                {
                    btnDelete.Visible = false;
                }
            }
            else
            {
                btnDelete.Visible = false;
            }
        }
    }
}