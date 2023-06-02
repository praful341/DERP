using BLL.FunctionClasses.Utility;
using DERP.Class;
using DevExpress.XtraBars;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DERP.MDI
{
    public partial class FrmHomePage : DevExpress.XtraEditors.XtraForm
    {
        DataTable dtResult = new DataTable();
        UserAuthentication objUser = new UserAuthentication();
        Boolean Flag = false;
        public FrmHomePage()
        {
            InitializeComponent();
        }

        public void SetToolTip(System.Windows.Forms.Control cntrol, string tooltipTitle, string tooltip)
        {
            ToolTip tool = new ToolTip();
            System.Drawing.Color myBackColor = System.Drawing.ColorTranslator.FromHtml("#7897B7");
            tool.BackColor = myBackColor;
            tool.ToolTipIcon = ToolTipIcon.Info;
            tool.ToolTipTitle = tooltipTitle;

            tool.SetToolTip(cntrol, tooltip);
        }

        private void btnMfg_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            this.Dispose();
        }
        public PictureBox picBox;
        public Image image;
        public PictureBox picBox1;
        public Image image1;
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                dtResult = objUser.GetStartMenuData();

                if (dtResult.Rows.Count == 1)
                {
                    BarButtonItem btn = new BarButtonItem(barManagerStart, dtResult.Rows[0]["caption"].ToString());
                    btn.Tag = dtResult.Rows[0]["menu_id"].ToString();
                    if (btn.Tag.ToString() == "1")
                    {
                        MDIMain MainForm = new MDIMain();
                        Global.gMainFormRef = MainForm;
                        this.Hide();
                        MainForm.ShowDialog();
                    }
                    else if (btn.Tag.ToString() == "2")
                    {
                        MDIMFG MDIMFG = new MDIMFG();
                        Global.gMainFormRef = MDIMFG;
                        this.Hide();
                        MDIMFG.ShowDialog();
                    }
                }
                else
                {
                    if (dtResult.Rows.Count == 2)
                    {
                        if (dtResult.Rows[0]["menu_id"].ToString() == "1" && dtResult.Rows[1]["menu_id"].ToString() == "2")
                        {
                            image = global::DERP.Properties.Resources.man1;
                            picBox = new PictureBox();
                            picBox.Height = 150;
                            picBox.Width = 150;
                            picBox.Location = new Point(525, 375);
                            picBox.Image = image;
                            Controls.Add(picBox);
                            picBox.Show();
                            picBox.Tag = dtResult.Rows[0]["menu_id"].ToString();

                            picBox.Click += this.PictureClick;

                            image1 = Image.FromFile("C:\\Users\\PRAFUL\\Desktop\\RM_MANUFACTURING_opt.png");
                            picBox1 = new PictureBox();
                            picBox1.Height = 150;
                            picBox1.Width = 150;
                            picBox1.Location = new Point(725, 375);
                            picBox1.Image = image1;
                            Controls.Add(picBox1);

                            picBox1.Show();
                            picBox1.Tag = dtResult.Rows[1]["menu_id"].ToString();

                            picBox1.Click += this.PictureClick1;
                        }
                        else if (dtResult.Rows[0]["menu_id"].ToString() == "1" && dtResult.Rows[1]["menu_id"].ToString() == "3")
                        {
                            image = global::DERP.Properties.Resources.man1;
                            picBox = new PictureBox();
                            picBox.Height = 150;
                            picBox.Width = 150;
                            picBox.Location = new Point(525, 375);
                            picBox.Image = image;
                            Controls.Add(picBox);
                            picBox.Show();
                            picBox.Tag = dtResult.Rows[0]["menu_id"].ToString();

                            picBox.Click += this.PictureClick;

                            image1 = Image.FromFile("C:\\Users\\PRAFUL\\Desktop\\Mixing.png");
                            picBox1 = new PictureBox();
                            picBox1.Height = 150;
                            picBox1.Width = 150;
                            picBox1.Location = new Point(725, 375);
                            picBox1.Image = image1;
                            Controls.Add(picBox1);

                            picBox1.Show();
                            picBox1.Tag = dtResult.Rows[1]["menu_id"].ToString();

                            picBox1.Click += this.PictureClick1;
                        }
                        if (dtResult.Rows[0]["menu_id"].ToString() == "2" && dtResult.Rows[1]["menu_id"].ToString() == "3")
                        {
                            image = global::DERP.Properties.Resources.man1;
                            picBox = new PictureBox();
                            picBox.Height = 150;
                            picBox.Width = 150;
                            picBox.Location = new Point(525, 375);
                            picBox.Image = image;
                            Controls.Add(picBox);
                            picBox.Show();
                            picBox.Tag = dtResult.Rows[0]["menu_id"].ToString();

                            picBox.Click += this.PictureClick;

                            image1 = Image.FromFile("C:\\Users\\PRAFUL\\Desktop\\Mixing.png");
                            picBox1 = new PictureBox();
                            picBox1.Height = 150;
                            picBox1.Width = 150;
                            picBox1.Location = new Point(725, 375);
                            picBox1.Image = image1;
                            Controls.Add(picBox1);

                            picBox1.Show();
                            picBox1.Tag = dtResult.Rows[1]["menu_id"].ToString();

                            picBox1.Click += this.PictureClick1;
                        }
                    }

                    int width = this.Width;
                    int height = this.Height;

                    close_lable.Location = new Point((width / 4) * 3, height - Convert.ToInt32(height * 0.14));
                }
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.ToString());
            }
        }

        private void PictureClick(object sender, EventArgs e)
        {
            if (picBox.Tag.ToString() == "1")
            {
                MDIMain MainForm = new MDIMain();
                Global.gMainFormRef = MainForm;
                this.Hide();
                MainForm.ShowDialog();
            }
            else if (picBox.Tag.ToString() == "2")
            {
                MDIMFG MDIMFG = new MDIMFG();
                Global.gMainFormRef = MDIMFG;
                this.Hide();
                MDIMFG.ShowDialog();
            }
            else if (picBox.Tag.ToString() == "3")
            {
                MDIMFG MDIMFG = new MDIMFG();
                Global.gMainFormRef = MDIMFG;
                this.Hide();
                MDIMFG.ShowDialog();
            }
        }

        private void PictureClick1(object sender, EventArgs e)
        {
            if (picBox1.Tag.ToString() == "1")
            {
                MDIMain MainForm = new MDIMain();
                Global.gMainFormRef = MainForm;
                this.Hide();
                MainForm.ShowDialog();
            }
            else if (picBox1.Tag.ToString() == "2")
            {
                MDIMFG MDIMFG = new MDIMFG();
                Global.gMainFormRef = MDIMFG;
                this.Hide();
                MDIMFG.ShowDialog();
            }
            else if (picBox1.Tag.ToString() == "3")
            {
                MDIMFG MDIMFG = new MDIMFG();
                Global.gMainFormRef = MDIMFG;
                this.Hide();
                MDIMFG.ShowDialog();
            }
        }

        void barManager_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                BarButtonItem btn = e.Item as BarButtonItem;
                DataRow dr = dtResult.AsEnumerable().Where(m => m.Field<int?>("Menu_id").ToString() == e.Item.Tag.ToString()).First();

                switch (Convert.ToInt32(btn.Tag))           //Start Menu ID
                {
                    case 1:
                        MDIMain MainForm = new MDIMain();
                        Global.gMainFormRef = MainForm;
                        Flag = true;
                        radialMenuStart.HidePopup();
                        radialMenuStart.Collapse(false, true);
                        this.Hide();
                        MainForm.ShowDialog();
                        break;
                    case 2:
                        {
                            MDIMFG MDIMFG = new MDIMFG();
                            Global.gMainFormRef = MDIMFG;
                            Flag = true;
                            radialMenuStart.HidePopup();
                            radialMenuStart.Collapse(false, true);
                            this.Hide();
                            MDIMFG.ShowDialog();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void FrmHomePage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.E && Control.ModifierKeys == Keys.Control)
            {
                Application.Exit();
            }
        }

        private void radialMenuStart_CloseUp(object sender, EventArgs e)
        {
            try
            {
                if (!Flag)
                {
                    Point p = new Point(Convert.ToInt32(700), 900 / 2);
                    radialMenuStart.ShowPopup(p);
                    radialMenuStart.Expand();
                }
                else
                {
                    radialMenuStart.HidePopup();
                    radialMenuStart.Collapse(false, true);
                }
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.ToString());
            }
        }
    }
}
