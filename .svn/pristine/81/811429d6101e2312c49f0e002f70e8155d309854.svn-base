using BLL;
using BLL.FunctionClasses.Transaction;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace DERP.Transaction
{
    public partial class FrmPriceActivation : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member
        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        PriceActivation objPriceActive;

        int m_numForm_id;
        int IntRes;
        #endregion

        #region Constructor
        public FrmPriceActivation()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            objPriceActive = new PriceActivation();

            m_numForm_id = 0;
            IntRes = 0;
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
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion

        #region Events       
        private void FrmPriceActivation_Load(object sender, System.EventArgs e)
        {
            try
            {
                GetData();
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
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
            btnSave.Enabled = false;

            DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
            backgroundWorker_PriceActivation.RunWorkerAsync();
            Cursor.Current = Cursors.Default;
            btnSave.Enabled = true;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void chkActive_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                for (var i = 0; i < dgvPriceActivation.RowCount; i++)
                {
                    if (!dgvPriceActivation.IsRowSelected(i) && dgvPriceActivation.GetRowCellValue(i, "currency").ToString() == dgvPriceActivation.GetRowCellValue(dgvPriceActivation.FocusedRowHandle, "currency").ToString() && dgvPriceActivation.GetRowCellValue(i, "rate_type").ToString() == dgvPriceActivation.GetRowCellValue(dgvPriceActivation.FocusedRowHandle, "rate_type").ToString())
                    {
                        dgvPriceActivation.SetRowCellValue(i, "active", false);
                    }
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void backgroundWorker_PriceActivation_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            IntRes = 0;
            Conn = new BeginTranConnection(true, false);
            Price_ActivationProperty priceActivationProperty = new Price_ActivationProperty();
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)grdPriceActivation.DataSource;
                DataView view = new DataView(dt);
                DataTable distinctValues = view.ToTable(true, "currency", "rate_type");

                for (int i = 0; i < distinctValues.Rows.Count; i++)
                {
                    if (dt.Select("currency = '" + distinctValues.Rows[i]["currency"] + "' AND rate_type = '" + distinctValues.Rows[i]["rate_type"] + "' AND active = '" + "True" + "'").Length > 0)
                    {
                        DataTable DTab_New = dt.Select("currency = '" + distinctValues.Rows[i]["currency"] + "' AND rate_type = '" + distinctValues.Rows[i]["rate_type"] + "'").CopyToDataTable();
                    }
                    else
                    {
                        Global.Message("Please Select Atleast One Currency Name = '" + distinctValues.Rows[i]["currency"] + "' AND rate_type = '" + distinctValues.Rows[i]["rate_type"] + "'");
                        return;
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    try
                    {
                        foreach (DataRow DRow in dt.Rows)
                        {
                            int Active = Val.ToBooleanToInt(DRow["active"]);
                            Int32 Rate_Id = Val.ToInt32(DRow["rate_id"]); 
                            Int32 Currency_Id = Val.ToInt32(DRow["currency_id"]);
                            Int32 Rate_Type_Id = Val.ToInt32(DRow["rate_type_id"]);
                            string Rate_Date = Val.ToString(DRow["rate_date"]);
                            Int64 form_id = m_numForm_id;
                            IntRes = objPriceActive.Save_PriceActivate(Active, Rate_Id, Rate_Date, Rate_Type_Id, Currency_Id, form_id, DLL.GlobalDec.EnumTran.Continue, Conn);
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
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
            finally
            {
                priceActivationProperty = null;
            }
        }
        private void backgroundWorker_PriceActivation_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (IntRes > 0)
                {
                    Global.Message("Price Activation Data Save Successfully");
                    GetData();
                    return;
                }
                else
                {
                    Global.Confirm("Error In Price Activation");
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }
        #endregion

        #region Functions
        private bool SaveDetails()
        {
            IntRes = 0;
            bool blnReturn = true;
            Price_ActivationProperty priceActivationProperty = new Price_ActivationProperty();
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)grdPriceActivation.DataSource;
                DataView view = new DataView(dt);
                DataTable distinctValues = view.ToTable(true, "currency", "rate_type");

                for (int i = 0; i < distinctValues.Rows.Count; i++)
                {
                    if (dt.Select("currency = '" + distinctValues.Rows[i]["currency"] + "' AND rate_type = '" + distinctValues.Rows[i]["rate_type"] + "' AND active = '" + "True" + "'").Length > 0)
                    {
                        DataTable DTab_New = dt.Select("currency = '" + distinctValues.Rows[i]["currency"] + "' AND rate_type = '" + distinctValues.Rows[i]["rate_type"] + "'").CopyToDataTable();
                    }
                    else
                    {
                        Global.Message("Please Select Atleast One Currency Name = '" + distinctValues.Rows[i]["currency"] + "' AND rate_type = '" + distinctValues.Rows[i]["rate_type"] + "'");
                        blnReturn = false;
                        return blnReturn;
                    }
                }

                List<ListError> lstError = new List<ListError>();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow DRow in dt.Rows)
                    {
                        int Active = Val.ToBooleanToInt(DRow["active"]);
                        Int32 Rate_Id = Val.ToInt32(DRow["rate_id"]);
                        Int32 Currency_Id = Val.ToInt32(DRow["currency_id"]);
                        Int32 Rate_Type_Id = Val.ToInt32(DRow["rate_type_id"]);
                        string Rate_Date = Val.ToString(DRow["rate_date"]);
                        Int64 form_id = m_numForm_id;

                        IntRes = objPriceActive.Save_PriceActivate(Active, Rate_Id, Rate_Date, Rate_Type_Id, Currency_Id, form_id);
                    }

                    if (IntRes > 0)
                    {
                        Global.Message("Price Activation Data Save Successfully");
                        return blnReturn;
                    }
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                blnReturn = false;
            }
            finally
            {
                priceActivationProperty = null;
            }

            return blnReturn;
        }
        public void GetData()
        {
            try
            {
                DataTable DTab = objPriceActive.GetData();
                grdPriceActivation.DataSource = DTab;
                dgvPriceActivation.BestFitColumns();
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        #endregion
    }
}
