using BLL;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.PropertyClasses.Account;
using DERP.Class;
using DERP.Master;
using DevExpress.XtraGrid.Views.Grid;
using DREP.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DREP.Transaction
{
    public partial class FrmMFGRoughSaleStatus : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        FormEvents objBOFormEvents = new FormEvents();
        BLL.BeginTranConnection Conn;
        Validation Val = new Validation();
        BLL.FormPer ObjPer = new BLL.FormPer();
        MFGRoughSaleStatus objRoughSaleStatus = new MFGRoughSaleStatus();
        CurrencyMaster objCurr = new CurrencyMaster();
        DataTable m_DTab;

        int termDays = 0;
        int IntRes = 0;
        int m_numForm_id = 0;
        decimal Purchase_Carat = 0;
        decimal Tot_Carat = 0;
        decimal m_numNetRate;
        decimal m_numshpRate;
        decimal m_numRate;

        #endregion

        #region Constructor
        public FrmMFGRoughSaleStatus()
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
            ObjPer.SetFormPer();
            if (ObjPer.AllowUpdate == false || ObjPer.AllowInsert == false)
            {
                Global.Message(BLL.GlobalDec.gStrPermissionInsUpdMsg);
                return;
            }

            btnSave.Enabled = false;

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
            backgroundWorker_RoughSaleStatus.RunWorkerAsync();

            btnSave.Enabled = true;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            grdRoughSaleStatus.DataSource = null;
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            Global.Export("xlsx", dgvRoughSaleStatus);
        }
        private void dgvRoughSaleStatus_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;
            if (view == null) return;
            if (e.Column.Caption == "Sale Party" || e.Column.Caption == "MFG Carat" || e.Column.Caption == "M Pending" || e.Column.Caption == "M Working" || e.Column.Caption == "Assort Sale" || e.Column.Caption == "A Pending" ||
                e.Column.Caption == "A Working" || e.Column.Caption == "Sale Carat" || e.Column.Caption == "Rate" || e.Column.Caption == "Terms" || e.Column.Caption == "Due Date" || e.Column.Caption == "Delivery Date" || e.Column.Caption == "Sale Date" || e.Column.Caption == "Is Final Dispatch")
            {
                view.SetRowCellValue(e.RowHandle, view.Columns["flag"], 1);
            }
            //if(e.Column.Caption == "Sale Party")
            else
            {
                return;
            }
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            GetData();
        }
        private void backgroundWorker_RoughSaleStatus_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.Default;
                if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                {
                    Conn = new BeginTranConnection(true, false);
                }
                else
                {
                    Conn = new BeginTranConnection(false, true);
                }
                MFGRoughSaleStatus_Property MFGRoughSaleStatusProperty = new MFGRoughSaleStatus_Property();
                IntRes = 0;
                try
                {
                    DataRow[] dr2 = m_DTab.Select("flag=1");

                    if (dr2.Count() > 0)
                    {
                        m_DTab = dr2.CopyToDataTable();
                    }

                    foreach (DataRow drw in m_DTab.Rows)
                    {
                        //if (Val.DBDate(drw["invoice_date"].ToString()) != "")
                        //{
                        MFGRoughSaleStatusProperty.rough_purchase_id = Val.ToInt64(drw["rough_purchase_id"]);

                        if (Val.DBDate(drw["invoice_date"].ToString()) != "")
                        {
                            MFGRoughSaleStatusProperty.invoice_date = Val.DBDate(drw["invoice_date"].ToString());
                        }
                        else
                        {
                            MFGRoughSaleStatusProperty.invoice_date = null;
                        }

                        MFGRoughSaleStatusProperty.rough_party_id = Val.ToInt64(drw["rough_party_id"]);
                        MFGRoughSaleStatusProperty.rough_broker_id = Val.ToInt64(drw["rough_broker_id"]);

                        MFGRoughSaleStatusProperty.mfg_carat = Val.ToDecimal(drw["mfg_carat"]);
                        MFGRoughSaleStatusProperty.mfg_pending = Val.ToDecimal(drw["mfg_pending"]);
                        MFGRoughSaleStatusProperty.mfg_working = Val.ToDecimal(drw["mfg_working"]);
                        MFGRoughSaleStatusProperty.assort_sale = Val.ToDecimal(drw["assort_sale"]);
                        MFGRoughSaleStatusProperty.assort_pending = Val.ToDecimal(drw["assort_pending"]);
                        MFGRoughSaleStatusProperty.assort_working = Val.ToDecimal(drw["assort_working"]);
                        MFGRoughSaleStatusProperty.sale_carat = Val.ToDecimal(drw["sale_carat"]);
                        MFGRoughSaleStatusProperty.cl_carat = Val.ToDecimal(drw["cl_carat"]);
                        MFGRoughSaleStatusProperty.sale_rate = Val.ToDecimal(drw["sale_rate"]);
                        MFGRoughSaleStatusProperty.net_rate = Val.ToDecimal(drw["sale_net_rate"]);
                        MFGRoughSaleStatusProperty.net_amount = Val.ToDecimal(drw["sale_net_amount"]);

                        MFGRoughSaleStatusProperty.term_days = Val.ToInt32(drw["sale_term_days"]);

                        if (Val.DBDate(drw["sale_due_date"].ToString()) != "")
                        {
                            MFGRoughSaleStatusProperty.due_date = Val.DBDate(drw["sale_due_date"].ToString());
                        }
                        else
                        {
                            MFGRoughSaleStatusProperty.due_date = null;
                        }

                        if (Val.DBDate(drw["delivery_date"].ToString()) != "")
                        {
                            MFGRoughSaleStatusProperty.delivery_date = Val.DBDate(drw["delivery_date"].ToString());
                        }
                        else
                        {
                            MFGRoughSaleStatusProperty.delivery_date = null;
                        }

                        MFGRoughSaleStatusProperty.is_delivery = Val.ToInt32(drw["is_delivery"]);
                        MFGRoughSaleStatusProperty.is_final_dispatch = Val.ToBooleanToInt(drw["SEL"]);

                        MFGRoughSaleStatusProperty.exchange_rate = Val.ToDecimal(drw["sale_exchange_rate"]);
                        MFGRoughSaleStatusProperty.inr_amount = Val.ToDecimal(drw["sale_inr_amount"]);
                        MFGRoughSaleStatusProperty.closing_amount = Val.ToDecimal(drw["closing_amount"]);

                        MFGRoughSaleStatusProperty.form_id = m_numForm_id;
                        IntRes = objRoughSaleStatus.Save(MFGRoughSaleStatusProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                        // }
                    }
                    if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                    {
                        Conn.Inter1.Commit();
                    }
                    else
                    {
                        Conn.Inter2.Commit();
                    }
                }
                catch (Exception ex)
                {
                    IntRes = -1;
                    if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                    {
                        Conn.Inter1.Rollback();
                    }
                    else
                    {
                        Conn.Inter2.Rollback();
                    }
                    Conn = null;
                    General.ShowErrors(ex.ToString());
                    return;
                }
                finally
                {
                    MFGRoughSaleStatusProperty = null;
                }
            }
            catch (Exception ex)
            {
                IntRes = -1;
                if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                {
                    Conn.Inter1.Rollback();
                }
                else
                {
                    Conn.Inter2.Rollback();
                }
                Conn = null;
                Global.Message(ex.ToString());
                if (ex.InnerException != null)
                {
                    Global.Message(ex.InnerException.ToString());
                }
            }
        }
        private void backgroundWorker_RoughSaleStatus_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (IntRes > 0)
                {
                    Global.Confirm("Rough Sale Status Save Succesfully");
                    grdRoughSaleStatus.DataSource = null;
                    GetData();
                }
                else
                {
                    Global.Confirm("Error In Save Rough Sale Status");
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }
        private void FrmMFGRoughSaleStatus_Load(object sender, EventArgs e)
        {
            Global.LOOKUPParty_RoughRep(RepSalePartyName);
            Global.LOOKUPBroker_RoughRep(RepSaleBrokerName);
            GetData();
        }
        private void RepSalePartyName_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmRoughPartyMaster frmCnt = new FrmRoughPartyMaster();
                frmCnt.ShowDialog();
                Global.LOOKUPParty_RoughRep(RepSalePartyName);
            }
        }
        private void RepSaleBrokerName_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmRoughBrokerMaster frmCnt = new FrmRoughBrokerMaster();
                frmCnt.ShowDialog();
                Global.LOOKUPBroker_RoughRep(RepSaleBrokerName);
            }
        }
        private void dgvRoughSaleStatus_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            CalculateGridAmount(e.PrevFocusedRowHandle);
        }
        private void dgvRoughSaleStatus_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            CalculateGridAmount(dgvRoughSaleStatus.FocusedRowHandle);
            //GridView View = sender as GridView;
            //if (View == null) return;
            //m_DTab.AcceptChanges();
            //string brd = e.Value as string;
            //decimal Tot = Val.ToDecimal(Tot_Carat) + Val.ToDecimal(brd);
            //if (Val.ToDecimal(Tot) > Val.ToDecimal(Purchase_Carat))
            //{
            //    e.Valid = false;
            //    e.ErrorText = "Purchase Carat not more then Sale Carat.";
            //}
        }
        private void dgvRoughSaleStatus_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;

            bool mar = Val.ToBoolean(dgvRoughSaleStatus.GetRowCellValue(e.RowHandle, "is_inward"));

            if (e.Column.FieldName == "carat" && mar == true)
            {
                e.Appearance.BackColor = mar ? Color.LightGreen : Color.LightGreen;
                return;
            }
        }

        #region Grid Event
        private void dgvRoughSaleStatus_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            CalculateGridAmount(dgvRoughSaleStatus.FocusedRowHandle);
        }
        #endregion

        #endregion

        #region Function

        private void GetData()
        {
            try
            {
                m_DTab = objRoughSaleStatus.GetData();

                if (m_DTab.Columns.Contains("SEL") == false)
                {
                    DataColumn Col = new DataColumn();
                    Col.ColumnName = "SEL";
                    Col.DataType = typeof(bool);
                    Col.DefaultValue = false;
                    m_DTab.Columns.Add(Col);
                }
                // m_DTab.Columns["SEL"].SetOrdinal(0);

                grdRoughSaleStatus.DataSource = m_DTab;
                // dgvRoughSaleStatus.BestFitColumns();
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
            }
        }
        private bool ValidateDetails()
        {
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (grdRoughSaleStatus.DataSource != null)
                {
                    m_DTab = (DataTable)grdRoughSaleStatus.DataSource;

                    foreach (DataRow drw in m_DTab.Rows)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                lstError.Add(new ListError(ex));
            }
            return (!(BLL.General.ShowErrors(lstError)));
        }
        private void CalculateGridAmount(int rowindex)
        {
            try
            {
                dgvRoughSaleStatus.RefreshData();
                termDays = Val.ToInt(dgvRoughSaleStatus.GetRowCellValue(rowindex, "sale_term_days"));

                //if (termDays == 0)
                //{
                //    dgvRoughSaleStatus.SetRowCellValue(rowindex, "sale_net_rate", Math.Round((Val.ToDouble(dgvRoughSaleStatus.GetRowCellValue(rowindex, "sale_rate"))), 3) + (Math.Round(Val.ToDouble(dgvRoughSaleStatus.GetRowCellValue(rowindex, "sale_rate")), 3) / 100 * 4));

                double Total_Carat = Math.Round(Val.ToDouble(dgvRoughSaleStatus.GetRowCellValue(rowindex, "assort_sale")) + Val.ToDouble(dgvRoughSaleStatus.GetRowCellValue(rowindex, "mfg_carat")) + Val.ToDouble(dgvRoughSaleStatus.GetRowCellValue(rowindex, "sale_carat")), 3);

                dgvRoughSaleStatus.SetRowCellValue(rowindex, "sale_net_amount", Math.Round(Val.ToDouble(dgvRoughSaleStatus.GetRowCellValue(rowindex, "sale_net_rate")) * Total_Carat, 3));
                //}
                //else if (termDays == 30)
                //{
                //    dgvRoughSaleStatus.SetRowCellValue(rowindex, "sale_net_rate", Math.Round((Val.ToDouble(dgvRoughSaleStatus.GetRowCellValue(rowindex, "sale_rate"))), 3) + (Math.Round(Val.ToDouble(dgvRoughSaleStatus.GetRowCellValue(rowindex, "sale_rate")), 3) / 100 * 3));
                //    dgvRoughSaleStatus.SetRowCellValue(rowindex, "sale_net_amount", Math.Round(Val.ToDouble(dgvRoughSaleStatus.GetRowCellValue(rowindex, "sale_net_rate")) * Val.ToDouble(dgvRoughSaleStatus.GetRowCellValue(rowindex, "sale_carat")), 3));
                //}
                //else if (termDays == 60)
                //{
                //    dgvRoughSaleStatus.SetRowCellValue(rowindex, "sale_net_rate", Math.Round((Val.ToDouble(dgvRoughSaleStatus.GetRowCellValue(rowindex, "sale_rate"))), 3) + (Math.Round(Val.ToDouble(dgvRoughSaleStatus.GetRowCellValue(rowindex, "sale_rate")), 3) / 100 * 2));
                //    dgvRoughSaleStatus.SetRowCellValue(rowindex, "sale_net_amount", Math.Round(Val.ToDouble(dgvRoughSaleStatus.GetRowCellValue(rowindex, "sale_net_rate")) * Val.ToDouble(dgvRoughSaleStatus.GetRowCellValue(rowindex, "sale_carat")), 3));
                //}
                //else if (termDays == 90)
                //{
                //    dgvRoughSaleStatus.SetRowCellValue(rowindex, "sale_net_rate", Math.Round((Val.ToDouble(dgvRoughSaleStatus.GetRowCellValue(rowindex, "sale_rate"))), 3) + (Math.Round(Val.ToDouble(dgvRoughSaleStatus.GetRowCellValue(rowindex, "sale_rate")), 3) / 100 * 1));
                //    dgvRoughSaleStatus.SetRowCellValue(rowindex, "sale_net_amount", Math.Round(Val.ToDouble(dgvRoughSaleStatus.GetRowCellValue(rowindex, "sale_net_rate")) * Val.ToDouble(dgvRoughSaleStatus.GetRowCellValue(rowindex, "sale_carat")), 3));
                //}
                //else if (termDays == 120)
                //{
                //    dgvRoughSaleStatus.SetRowCellValue(rowindex, "sale_net_rate", Math.Round((Val.ToDouble(dgvRoughSaleStatus.GetRowCellValue(rowindex, "sale_rate"))), 3) + (Math.Round(Val.ToDouble(dgvRoughSaleStatus.GetRowCellValue(rowindex, "sale_rate")), 3) / 100 * 0));
                //    dgvRoughSaleStatus.SetRowCellValue(rowindex, "sale_net_amount", Math.Round(Val.ToDouble(dgvRoughSaleStatus.GetRowCellValue(rowindex, "sale_net_rate")) * Val.ToDouble(dgvRoughSaleStatus.GetRowCellValue(rowindex, "sale_carat")), 3));
                //}
                //else
                //{
                //    dgvRoughSaleStatus.SetRowCellValue(rowindex, "sale_net_rate", 0);
                //    dgvRoughSaleStatus.SetRowCellValue(rowindex, "sale_net_amount", 0);
                //}
                decimal carat = Val.ToDecimal(dgvRoughSaleStatus.GetRowCellValue(dgvRoughSaleStatus.FocusedRowHandle, "mfg_carat")) + Val.ToDecimal(dgvRoughSaleStatus.GetRowCellValue(dgvRoughSaleStatus.FocusedRowHandle, "mfg_pending")) + Val.ToDecimal(dgvRoughSaleStatus.GetRowCellValue(dgvRoughSaleStatus.FocusedRowHandle, "mfg_working")) +
                                   Val.ToDecimal(dgvRoughSaleStatus.GetRowCellValue(dgvRoughSaleStatus.FocusedRowHandle, "assort_sale")) + Val.ToDecimal(dgvRoughSaleStatus.GetRowCellValue(dgvRoughSaleStatus.FocusedRowHandle, "assort_pending")) + Val.ToDecimal(dgvRoughSaleStatus.GetRowCellValue(dgvRoughSaleStatus.FocusedRowHandle, "assort_working")) +
                                   Val.ToDecimal(dgvRoughSaleStatus.GetRowCellValue(dgvRoughSaleStatus.FocusedRowHandle, "sale_carat"));
                dgvRoughSaleStatus.SetRowCellValue(dgvRoughSaleStatus.FocusedRowHandle, "cl_carat", Val.ToDecimal(dgvRoughSaleStatus.GetRowCellValue(dgvRoughSaleStatus.FocusedRowHandle, "carat")) - Val.ToDecimal(carat));

                //DateTime Date1 = Convert.ToDateTime(dgvRoughSaleStatus.GetRowCellValue(rowindex, "invoice_date"));

                string Invoice_Date = Val.ToString(dgvRoughSaleStatus.GetRowCellValue(rowindex, "invoice_date"));
                if (Invoice_Date != "")
                {
                    DateTime Date = Convert.ToDateTime(dgvRoughSaleStatus.GetRowCellValue(rowindex, "invoice_date")).AddDays(Val.ToInt(termDays));
                    dgvRoughSaleStatus.SetRowCellValue(rowindex, "sale_due_date", Val.DBDate(Date.ToString()));
                }

                Purchase_Carat = Val.ToDecimal(dgvRoughSaleStatus.GetRowCellValue(rowindex, "carat"));
                Tot_Carat = Val.ToDecimal(dgvRoughSaleStatus.GetRowCellValue(rowindex, "mfg_carat")) + Val.ToDecimal(dgvRoughSaleStatus.GetRowCellValue(rowindex, "mfg_pending")) + Val.ToDecimal(dgvRoughSaleStatus.GetRowCellValue(rowindex, "mfg_working")) + Val.ToDecimal(dgvRoughSaleStatus.GetRowCellValue(rowindex, "assort_sale")) + Val.ToDecimal(dgvRoughSaleStatus.GetRowCellValue(rowindex, "assort_pending")) + Val.ToDecimal(dgvRoughSaleStatus.GetRowCellValue(rowindex, "assort_working")) + Val.ToDecimal(dgvRoughSaleStatus.GetRowCellValue(rowindex, "sale_carat"));

                dgvRoughSaleStatus.SetRowCellValue(dgvRoughSaleStatus.FocusedRowHandle, "sale_inr_amount", Math.Round(Val.ToDecimal(dgvRoughSaleStatus.GetRowCellValue(dgvRoughSaleStatus.FocusedRowHandle, "sale_net_amount")) * Val.ToDecimal(dgvRoughSaleStatus.GetRowCellValue(dgvRoughSaleStatus.FocusedRowHandle, "sale_exchange_rate")), 2));
                dgvRoughSaleStatus.SetRowCellValue(dgvRoughSaleStatus.FocusedRowHandle, "closing_amount", Math.Round(Val.ToDecimal(dgvRoughSaleStatus.GetRowCellValue(dgvRoughSaleStatus.FocusedRowHandle, "sale_inr_amount")) - Val.ToDecimal(dgvRoughSaleStatus.GetRowCellValue(dgvRoughSaleStatus.FocusedRowHandle, "inr_amount")), 0));
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
            Export("xlsx", "Export to Excel", "Excel files 97-2003 (Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*");
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
                            dgvRoughSaleStatus.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvRoughSaleStatus.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvRoughSaleStatus.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvRoughSaleStatus.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvRoughSaleStatus.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvRoughSaleStatus.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvRoughSaleStatus.ExportToCsv(Filepath);
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

        private void dgvRoughSaleStatus_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            if (m_DTab.Rows.Count > 0 && Val.ToDecimal(ClmCarat.SummaryItem.SummaryValue) > 0)
            {
                decimal amount = 0;
                amount = Convert.ToDecimal(m_DTab.Compute("SUM(amount)", string.Empty));
                m_numRate = Math.Round((Val.ToDecimal(amount) / Val.ToDecimal(ClmCarat.SummaryItem.SummaryValue)), 2, MidpointRounding.AwayFromZero);

            }
            else
            {
                m_numRate = 0;
            }
            if (m_DTab.Rows.Count > 0 && Val.ToDecimal(ClmCarat.SummaryItem.SummaryValue) > 0)
            {
                decimal shp_amount = 0;
                shp_amount = Convert.ToDecimal(m_DTab.Compute("SUM(shiping_amount)", string.Empty));
                m_numshpRate = Math.Round((Val.ToDecimal(shp_amount) / Val.ToDecimal(ClmCarat.SummaryItem.SummaryValue)), 2, MidpointRounding.AwayFromZero);
            }
            else
            {
                m_numshpRate = 0;
            }
            if (Val.ToDecimal(ClmNetAmount.SummaryItem.SummaryValue) > 0 && Val.ToDecimal(ClmCarat.SummaryItem.SummaryValue) > 0)
            {
                m_numNetRate = Math.Round((Val.ToDecimal(ClmNetAmount.SummaryItem.SummaryValue) / Val.ToDecimal(ClmCarat.SummaryItem.SummaryValue)), 2, MidpointRounding.AwayFromZero);
                //net_rate,
            }
            else
            {
                m_numNetRate = 0;
            }
            if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == "rate")
            {
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    e.TotalValue = m_numRate;
            }
            else if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == "shiping_rate")
            {
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    e.TotalValue = m_numshpRate;
            }
            else if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == "net_rate")
            {
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    e.TotalValue = m_numNetRate;
            }
        }
    }
}
