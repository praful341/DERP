using BLL;
using BLL.FunctionClasses.Transaction;
using DERP.Class;
using DevExpress.XtraCharts;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace DERP.Transaction
{
    public partial class FrmSalesDashBoard : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member
        
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;
        SaleInvoice objSaleInvoice;
        DataSet m_dtbDetails;
        DataTable m_opDate = new DataTable();

        #endregion

        #region Constructor
        public FrmSalesDashBoard()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();
            objSaleInvoice = new SaleInvoice();
            m_dtbDetails = new DataSet();
        }
        public void ShowForm()
        {
            ObjPer.FormName = this.Name.ToUpper();
            //if (ObjPer.CheckPermission() == false)
            //{
            //    Global.Message(BLL.GlobalDec.gStrPermissionViwMsg);
            //    return;
            //}
            //if (Global.CheckDefault() == 0)
            //{
            //    Global.Message("Please Check User Default Setting");
            //    this.Close();
            //    return;
            //}
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
        private void FrmSalesDashBoard_Load(object sender, EventArgs e)
        {
            try
            {
                m_opDate = Global.GetDate();

                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);

                string From_Date = Val.DBDate(m_opDate.Rows[0]["opening_date"].ToString());
                if (From_Date != "")
                {
                    dtpFromDate.EditValue = Val.DBDate(m_opDate.Rows[0]["opening_date"].ToString());
                }
                else
                {
                    dtpFromDate.EditValue = DateTime.Now;
                }

                dtpFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
                

                dtpToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpToDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpToDate.EditValue = Val.DBDate(Val.ToString(DateTime.Now));

                GetData();
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }

        private bool GetData()
        {
            bool blnReturn = true;
            DateTime datFromDate = DateTime.MinValue;
            DateTime datToDate = DateTime.MinValue;
            objSaleInvoice = new SaleInvoice();
            m_dtbDetails = new DataSet();

            try
            {

                XRChartBTSales.Series.Clear();
                XRChartParty.Series.Clear();
                XRChartPieInOut.Series.Clear();
                //XRChartSeller.Series.Clear();
                //XRChartDemand.Series.Clear();

                m_dtbDetails = objSaleInvoice.DashBord_Data(Val.DBDate(dtpFromDate.Text), Val.DBDate(dtpToDate.Text));

                Series SeriesBT = new Series("Branch Transfer", ViewType.Bar);
                SeriesBT.DataSource = m_dtbDetails.Tables[0];
                SeriesBT.ArgumentDataMember = "description";
                SeriesBT.ValueDataMembers.AddRange(new string[] { "bt_amount" });

                Series SeriesSales = new Series("Sales Invoice", ViewType.Bar);
                SeriesSales.DataSource = m_dtbDetails.Tables[0];
                SeriesSales.ArgumentDataMember = "description";
                SeriesSales.ValueDataMembers.AddRange(new string[] { "netamount" });

                XRChartBTSales.Series.Add(SeriesBT);
                XRChartBTSales.Series.Add(SeriesSales);

                Series SeriesPartySales = new Series("Party Sales", ViewType.Bar);
                SeriesPartySales.DataSource = m_dtbDetails.Tables[1];
                SeriesPartySales.ArgumentDataMember = "party_name";
                SeriesPartySales.ValueDataMembers.AddRange(new string[] { "netamount" });
                SeriesPartySales.Label.TextPattern = "{A} : {VP:P}";
                SeriesPartySales.Label.Font = new Font("Tahoma", 8, FontStyle.Regular);

                XRChartParty.Series.Add(SeriesPartySales);

                Series SeriesInwardOutward = new Series("Inward", ViewType.Pie);
                SeriesInwardOutward.DataSource = m_dtbDetails.Tables[2];
                SeriesInwardOutward.ArgumentDataMember = "description";
                SeriesInwardOutward.ValueDataMembers.AddRange(new string[] { "netamount" });
                SeriesInwardOutward.Label.TextPattern = "{A} : {VP:P}";
                ((PieSeriesLabel)SeriesInwardOutward.Label).Position = PieSeriesLabelPosition.TwoColumns;
                ((PieSeriesLabel)SeriesInwardOutward.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

                XRChartPieInOut.Series.Add(SeriesInwardOutward);

                //Series SeriesSellerWise = new Series("Month Wise", ViewType.Bar);
                //SeriesSellerWise.DataSource = m_dtbDetails.Tables[3];
                //SeriesSellerWise.ArgumentDataMember = "description";
                //SeriesSellerWise.ValueDataMembers.AddRange(new string[] { "netamount" });
                //SeriesSellerWise.Label.TextPattern = "{A} : {VP:P}";
                //SeriesSellerWise.Label.Font = new Font("Tahoma", 8, FontStyle.Regular);

                //XRChartSeller.Series.Add(SeriesSellerWise);


                //Series SeriesDemand = new Series("Demand Noting", ViewType.Pie);
                //SeriesDemand.DataSource = m_dtbDetails.Tables[4];
                //SeriesDemand.ArgumentDataMember = "type";
                //SeriesDemand.ValueDataMembers.AddRange(new string[] { "total_per" });
                //SeriesDemand.Label.TextPattern = "{A} : {VP:P}";
                //SeriesDemand.Label.Font = new Font("Tahoma", 8, FontStyle.Regular);

                //XRChartDemand.Series.Add(SeriesDemand);

                grdDueDays.DataSource = m_dtbDetails.Tables[5];
                dgvDueDays.BestFitColumns();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            finally
            {
                objSaleInvoice = null;
            }

            return blnReturn;
        }

        #endregion

        private void BtnGenerateReport_Click(object sender, EventArgs e)
        {
            GetData();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
