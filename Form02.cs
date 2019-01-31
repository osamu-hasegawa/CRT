using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//---
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections;
using System.IO;

namespace CRTMONITOR
{
	enum ALIGN
	{
		LEFT = 0,
		CENTER,
		RIGHT
	};
	struct CLMDEF
	{
		//public CLT		clt;
		public int		index;
		public string	name;
		public int		width;
		public ALIGN	align;
		public string	format;
		public int		visible;
		public int		clm_idx;
	//	public CLMTYPE	type;
		public CLMDEF(/*CLT clt,*/ int index, string name, int width, ALIGN align, string format/*, CLMTYPE type*/)
		{
			//this.clt   = clt;
			this.index = index;
			this.name = name;
			this.width = width;
			this.align = align;
			this.format = format;
			this.visible = 1;
			this.clm_idx = -1;
			//this.type = type;
		}
	};
	public partial class Form02 : Form
	{
		const
		int C_INTERVAL = 100;//ms
		int C_ROWCNT;
		int C_COLCNT;
		int C_CELCNT;
		int C_CHTCNT;
		//Random r = new Random();
		int		mes_itv;
		int		mes_cnt;
		int		mes_nxt;
		int		mes_wid;
		int		mes_pts_in_wid;
		uint		MEAS_SQNO_BAK;
		PictureBox[] charts = null;
		Bitmap[] canvas = null;
		ChartSub[]
				csubs = null;
		Panel[]
				panels = null;
		StreamWriter m_wr;
		TimeSpan m_ts;

		public Form02()
		{
			InitializeComponent();
		}
#if false
		private bool is_equal(G.SENSOR_TBL[] s1, G.SENSOR_TBL[] s2)
		{
			if (s1 == null || s2 == null) {
				return(false);
			}
			if (s1.Length != s2.Length) {
				return(false);
			}
			for (int i = 0; i < s1.Length; i++) {
				if (s1[i].NAME != s2[i].NAME) {
					return(false);
				}
				if (s1[i].UNIT != s2[i].UNIT) {
					return(false);
				}
			}
			return(true);
		}
#endif
		private void ResetLayout()
		{
			C_ROWCNT = G.SS.GRP_ROW_CNT;
			C_COLCNT = G.SS.GRP_COL_CNT;
			C_CELCNT = (C_ROWCNT * C_COLCNT);
			C_CHTCNT = (C_ROWCNT * C_COLCNT);

			csubs = new ChartSub[C_CHTCNT];
		//	panels = new Panel[C_CELCNT];
		}
		private void ResetDeviceTable()
		{
			int	j;
			G.DT = new G.DEVICE_TBL[22];
			for (j = 0; j < G.DT.Length; j++) {
				G.DT[j] = new G.DEVICE_TBL();
			}
			//-------------------------------------
			//G.DT[0].BID = 0x10;
			//G.DT[0].DID = 0x29;
			G.DT[0].SID = 0;
			G.DT[0].GID = 0;
			G.DT[0].NAME = "カラー:赤";
			G.DT[0].UNIT = "[cnt]";
			//G.DT[0].PREC = 0;
			//-------------------------------------
			//G.DT[1].BID = 0x10;
			//G.DT[1].DID = 0x29;
			G.DT[1].SID = 1;
			G.DT[1].GID = 0;
			G.DT[1].NAME = "カラー:緑";
			G.DT[1].UNIT = "[cnt]";
			//G.DT[1].PREC = 0;
			//-------------------------------------
			//G.DT[2].BID = 0x10;
			//G.DT[2].DID = 0x29;
			G.DT[2].SID = 2;
			G.DT[2].GID = 0;
			G.DT[2].NAME = "カラー:青";
			G.DT[2].UNIT = "[cnt]";
			//G.DT[2].PREC = 0;
			//-------------------------------------
			//G.DT[3].BID = 0x10;
			//G.DT[3].DID = 0x29;
			G.DT[3].SID = 3;
			G.DT[3].GID = 0;
			G.DT[3].NAME = "カラー:白";
			G.DT[3].UNIT = "[cnt]";
			//G.DT[3].PREC = 0;
			//--------------------------------------
			//G.DT[4].BID = 0x10;
			//G.DT[4].DID = 0x29;
			G.DT[4].SID = 4;
			G.DT[4].GID = 0;
			G.DT[4].NAME = "温度";
			G.DT[4].UNIT = "[℃]";
			//G.DT[4].PREC = 0;
			//-------------------------------------
			//G.DT[5].BID = 0x10;
			//G.DT[5].DID = 0x29;
			G.DT[5].SID = 5;
			G.DT[5].GID = 0;
			G.DT[5].NAME = "湿度";
			G.DT[5].UNIT = "[%]";
			//G.DT[5].PREC = 0;
			//-------------------------------------
			//G.DT[6].BID = 0x10;
			//G.DT[6].DID = 0x29;
			G.DT[6].SID = 6;
			G.DT[6].GID = 0;
			G.DT[6].NAME = "気圧";
			G.DT[6].UNIT = "[hPa]";
			//G.DT[6].PREC = 0;
			//-------------------------------------
			//G.DT[7].BID = 0x10;
			//G.DT[7].DID = 0x29;
			G.DT[7].SID = 7;
			G.DT[7].GID = 0;
			G.DT[7].NAME = "加速度:X";
			G.DT[7].UNIT = "[g]";
			//G.DT[7].PREC = 0;
			//-------------------------------------
			//G.DT[8].BID = 0x10;
			//G.DT[8].DID = 0x29;
			G.DT[8].SID = 8;
			G.DT[8].GID = 0;
			G.DT[8].NAME = "加速度:Y";
			G.DT[8].UNIT = "[g]";
			//G.DT[8].PREC = 0;
			//-------------------------------------
			//G.DT[9].BID = 0x10;
			//G.DT[9].DID = 0x29;
			G.DT[9].SID = 9;
			G.DT[9].GID = 0;
			G.DT[9].NAME = "加速度:Z";
			G.DT[9].UNIT = "[g]";
			//G.DT[9].PREC = 0;
			//------------------------------------
			//G.DT[10].BID = 0x10;
			//G.DT[10].DID = 0x29;
			G.DT[10].SID = 10;
			G.DT[10].GID = 0;
			G.DT[10].NAME = "ジャイロ:X";
			G.DT[10].UNIT = "[deg/s]";
			//G.DT[10].PREC = 0;
			//---------------------------------------
			//G.DT[11].BID = 0x10;
			//G.DT[11].DID = 0x29;
			G.DT[11].SID = 11;
			G.DT[11].GID = 0;
			G.DT[11].NAME = "ジャイロ:Y";
			G.DT[11].UNIT = "[deg/s]";
			//G.DT[11].PREC = 0;
			//-------------------------------------
			//G.DT[12].BID = 0x10;
			//G.DT[12].DID = 0x29;
			G.DT[12].SID = 12;
			G.DT[12].GID = 0;
			G.DT[12].NAME = "ジャイロ:Z";
			G.DT[12].UNIT = "[deg/s]";
			//G.DT[12].PREC = 0;
			//-------------------------------------
			//G.DT[13].BID = 0x10;
			//G.DT[13].DID = 0x29;
			G.DT[13].SID = 13;
			G.DT[13].GID = 0;
			G.DT[13].NAME = "照度:左";
			G.DT[13].UNIT = "[lx]";
			//G.DT[13].PREC = 0;
			//-------------------------------------
			//G.DT[14].BID = 0x10;
			//G.DT[14].DID = 0x29;
			G.DT[14].SID = 14;
			G.DT[14].GID = 0;
			G.DT[14].NAME = "近接:左";
			G.DT[14].UNIT = "[cnt]";
			//G.DT[14].PREC = 0;
			//-------------------------------------
			//G.DT[15].BID = 0x10;
			//G.DT[15].DID = 0x29;
			G.DT[15].SID = 15;
			G.DT[15].GID = 0;
			G.DT[15].NAME = "照度:右";
			G.DT[15].UNIT = "[lx]";
			//G.DT[15].PREC = 0;
			//-------------------------------------
			//G.DT[16].BID = 0x10;
			//G.DT[16].DID = 0x29;
			G.DT[16].SID = 16;
			G.DT[16].GID = 0;
			G.DT[16].NAME = "近接:右";
			G.DT[16].UNIT = "[cnt]";
			//G.DT[16].PREC = 0;
			//-------------------------------------
			//G.DT[17].BID = 0x10;
			//G.DT[17].DID = 0x29;
			G.DT[17].SID = 17;
			G.DT[17].GID = 0;
			G.DT[17].NAME = "照度:奥";
			G.DT[17].UNIT = "[lx]";
			//G.DT[17].PREC = 0;
			//-------------------------------------
			//G.DT[18].BID = 0x10;
			//G.DT[18].DID = 0x29;
			G.DT[18].SID = 18;
			G.DT[18].GID = 0;
			G.DT[18].NAME = "近接:奥";
			G.DT[18].UNIT = "[cnt]";
			//G.DT[18].PREC = 0;
			//-------------------------------------
			//G.DT[19].BID = 0x10;
			//G.DT[19].DID = 0x29;
			G.DT[19].SID = 19;
			G.DT[19].GID = 0;
			G.DT[19].NAME = "基準電圧";
			G.DT[19].UNIT = "[cnt]";
			//G.DT[19].PREC = 0;
			//-------------------------------------
			//G.DT[20].BID = 0x10;
			//G.DT[20].DID = 0x29;
			G.DT[20].SID = 20;
			G.DT[20].GID = 0;
			G.DT[20].NAME = "圧力";
			G.DT[20].UNIT = "[N]";
			//G.DT[20].PREC = 0;
			//-----------------------------------
			//G.DT[21].BID = 0x10;
			//G.DT[21].DID = 0x29;
			G.DT[21].SID = 21;
			G.DT[21].GID = 0;
			G.DT[21].NAME = "電池";
			G.DT[21].UNIT = "[V]";
			//G.DT[21].PREC = 0;
		}
		private void Form02_Load(object sender, EventArgs e)
		{
			G.FORM02 = this;
			this.button2.Enabled = false;
			this.button3.Enabled = false;
			this.button4.Enabled = false;
			//this.button6.Enabled = false;
			//this.button7.Enabled = false;
			//this.button8.Enabled = false;
			this.button97.Enabled = false;
			//---
			if (true) {
				//C:\Users\araya320\AppData\Roaming\KOP\uSCOPE (<-セットアップにてコピーされる)
				//から
				//C:\Users\araya320\Documents\KOP\uSCOPE
				//へコピーし、元ファイルを削除する
				G.COPY_SETTINGS("settings.xml");
			}
			G.AS.load(ref G.AS);
			G.SS.load(ref G.SS);
			//---
			//G.SS.AUT_BEF_PATH = G.AS.AUT_BEF_PATH;
			G.SS.BEFORE_PATH  = G.AS.BEFORE_PATH;
			//G.SS.PLM_AUT_FOLD = G.AS.PLM_AUT_FOLD;
			//G.SS.MOZ_CND_FOLD = G.AS.MOZ_CND_FOLD;
			//---
		#if false
			//Math.DivRem();
			int s_b = sizeof(bool  );//1B
			int s_l = sizeof(long  );//8B!!!
			int s_i = sizeof(int   );//4B
			int s_f = sizeof(float );//4B
			int s_d = sizeof(double);//8B
		#endif
			ResetLayout();

			this.charts = new PictureBox[C_CHTCNT];
			this.canvas = new Bitmap[C_CHTCNT];
			this.panels = new Panel[] {
				this.panel1, this.panel2,this.panel3,this.panel4,this.panel5,this.panel6,
				this.panel7, this.panel8,this.panel9,this.panel10,this.panel11,this.panel12,
				this.panel13, this.panel14,this.panel15,this.panel16,this.panel17,this.panel18,
				this.panel19, this.panel20,this.panel21,this.panel22,this.panel23,this.panel24,
				this.panel25, this.panel26,this.panel27,this.panel28,this.panel29,this.panel30
			};

			//this.tabControl1.Dock = DockStyle.Fill;

			for (int i = 0; i < C_CHTCNT; i++) {
				PictureBox pb = new PictureBoxEx();
				pb.Tag = i;
				pb.Paint += new PaintEventHandler(pictureBox1_Paint);
				pb.Resize += new EventHandler(pictureBox1_Resize);
				pb.Dock = DockStyle.Fill;
				this.charts[i] = pb;
				this.panels[i].Controls.Add(pb);
			}
			for (int i = C_CHTCNT; i < this.panels.Length; i++) {
				this.panels[i].Visible = false;
			}
			foreach (PictureBox c in this.charts) {
				c.Dock = DockStyle.Fill;
			}
			//this.tabControl1.Dock = DockStyle.Fill;
			//---
			//Random r = new Random();
			//for (int i = 0; i < 50; i++) {
			//    timer1_Tick(null, null);
			//}


			numericUpDown1_ValueChanged(null, null);
			numericUpDown2_ValueChanged(null, null);
			if (true) {
				int i = 0;
				foreach (PictureBox c in this.charts) {
					csubs[i] = new ChartSub();
					//---
//					csubs[i].GDEF_INIT(3, c, new Rectangle(25,13,-13,-20));
					csubs[i].GDEF_INIT(3, c, new Rectangle(25, 13, -13 - 12, -20));
					csubs[i].GDEF_PSET( 0, this.mes_wid, this.mes_wid/2, -1, +1, 1);
					Graphics g = c.CreateGraphics();
					csubs[i].GDEF_GRID(g);
					g.Dispose();
					//---
					i++;
				}
			}
			//G.bDEBUG = true;
			if (G.DT == null || G.DT.Length != 22/*&& G.bDEBUG*/) {
				ResetDeviceTable();
				do_auto_layout();
			}
			if (G.DT.Length == G.SS.SEN_GRH_GID.Length) {
				for (int i = 0; i < G.DT.Length; i++) {
					G.DT[i].GID  = G.SS.SEN_GRH_GID[i];
					G.DT[i].SMAX = G.SS.SEN_SCL_MAX[i];
					G.DT[i].SMIN = G.SS.SEN_SCL_MIN[i];
				}
			}
			set_graph_tbl();
			if (check_graph_layout()) {
				set_graph_layout();
			}
			//---
			//if ((G.LED_PWR_STS & 1) != 0) {
			//    this.button5.Enabled = false;
			//    this.button6.Enabled = true;
			//}
			//else {
			//    this.button5.Enabled = true;
			//    this.button6.Enabled = false;
			//}
			//if ((G.LED_PWR_STS & 2) != 0) {
			//    this.button7.Enabled = false;
			//    this.button8.Enabled = true;
			//}
			//else {
			//    this.button7.Enabled = true;
			//    this.button8.Enabled = false;
			//}
			//this.numericUpDown3.Value = G.SP.PWM_LED_DTY[0];
			//this.numericUpDown4.Value = G.SP.PWM_LED_DTY[1];
			//---
			this.Left   = G.AS.APP_F02_LFT;
			this.Top    = G.AS.APP_F02_TOP;
			this.Width  = G.AS.APP_F02_WID; 
			this.Height = G.AS.APP_F02_HEI;
			//---
			ResetGrid(this.dataGridView1, m_clms_m, null, -1);
			if (true) {
				//プログラム起動時のUIFレベルとして記憶
				G.UIF_LEVL = G.SS.ETC_UIF_LEVL;
			}
			if (G.SS.ETC_UIF_LEVL == 1/*開発者用(一度)*/) {
				G.SS.ETC_UIF_LEVL = 0;//->ユーザ用へ
			}
		}
		private void Form02_FormClosing(object sender, FormClosingEventArgs e)
		{
			//if (this.SENS != null) {
			//    for (int i = 0; i < this.SENS.Length; i++) {
			//        this.SENS[i].OBJE = null;
			//    }
			//    G.SS.SEN_TBL = this.SENS;
			//}
			//---
			if (this.Left <= -32000 || this.Top <= -32000) {
				//最小化時は更新しない
			}
			else {
				G.AS.APP_F02_LFT = this.Left;
				G.AS.APP_F02_TOP = this.Top;
				G.AS.APP_F02_WID = this.Width;
				G.AS.APP_F02_HEI = this.Height;
			}
			if (true) {
				G.AS.BEFORE_PATH  = G.SS.BEFORE_PATH ;
			}
			f_close();
			//G.SP.PWM_LED_DTY[0] = (int)this.numericUpDown3.Value;
			//G.SP.PWM_LED_DTY[1] = (int)this.numericUpDown4.Value;
			//---
			G.AS.save(G.AS);
			G.SS.save(G.SS);
			if (G.AS.DEBUG_MODE != 0) {
				DBGMODE.TERM();
			}
		}

		private void do_auto_layout()
		{
			//自動割当
			if (G.DT == null) {
				return;
			}
			int gid = 1/*, q = 0*/;
			//---
			for (int i = 0; i < G.DT.Length; i++) {
				G.DEVICE_TBL dev = G.DT[i];

				if (gid <= (this.charts.Length)) {
					dev.GID = gid++;
				}
				else {
					dev.GID = 0;//非表示
				}
				string str1="", str2 = "";
				System.Diagnostics.Debug.WriteLine(str1 + str2);
			}
		}
		private void numericUpDown1_ValueChanged(object sender, EventArgs e)
		{
			this.mes_itv = C_INTERVAL;//ms
			//this.timer1.Interval = (int)this.numericUpDown1.Value;
			this.mes_pts_in_wid = this.mes_wid *1000 / this.mes_itv;
		}
		private void numericUpDown2_ValueChanged(object sender, EventArgs e)
		{
//			int i = 0;
			
			this.mes_wid = (int)this.numericUpDown2.Value; //sec
			this.mes_pts_in_wid = this.mes_wid *1000 / this.mes_itv;

			if (this.csubs == null || this.csubs[0] == null) {
				return;
			}
			for (int i = 0; i < csubs.Length; i++) {
				this.csubs[i].GDEF_PSET(0, this.mes_wid, double.NaN,double.NaN,double.NaN,double.NaN);
				this.csubs[i].Update();
			}
		}
		private void do_auto_scale()
		{
			for (int i = 0; i < this.charts.Length; i++ ) {
				this.csubs[i].DoAutoScale();
			}
		}

		private void remove_data()
		{
		}
		private void timer1_Tick(object sender, EventArgs e)
		{
#if true
			if (true) {
				byte[] buf;
				uint	MEAS_SQNO;
				D.GET_SYS_INF(out buf);
				MEAS_SQNO = (uint)D.MAKELONG(buf[15],buf[14],buf[13],buf[12]);
				if (MEAS_SQNO == MEAS_SQNO_BAK) {
					return;
				}
				MEAS_SQNO_BAK = MEAS_SQNO;
			}

#endif
			this.timer1.Enabled = false;
			if (true) {
#if true
				if (mes_cnt > 0 && (mes_cnt % this.mes_pts_in_wid) == 0) {
					for (int i = 0; i < this.csubs.Length; i++) {
						csubs[i].PageFeed();
					}
					//button7_Click(null, null);
				}
#endif
			}
			if (true) {
				S.MES_DAT dat;
				int q = 0;
				S.get_mes_dat(out dat);
				G.MES_VAL[q++] = dat.C_R;
				G.MES_VAL[q++] = dat.C_G;
				G.MES_VAL[q++] = dat.C_B;
				G.MES_VAL[q++] = dat.C_C;
				G.MES_VAL[q++] = dat.S_T;
				G.MES_VAL[q++] = dat.S_H;
				G.MES_VAL[q++] = dat.S_P;
				G.MES_VAL[q++] = dat.A_X;
				G.MES_VAL[q++] = dat.A_Y;
				G.MES_VAL[q++] = dat.A_Z;
				G.MES_VAL[q++] = dat.G_X;
				G.MES_VAL[q++] = dat.G_Y;
				G.MES_VAL[q++] = dat.G_Z;
				G.MES_VAL[q++] = dat.L_L;
				G.MES_VAL[q++] = dat.L_P;
				G.MES_VAL[q++] = dat.R_L;
				G.MES_VAL[q++] = dat.R_P;
				G.MES_VAL[q++] = dat.B_L;
				G.MES_VAL[q++] = dat.B_P;
				G.MES_VAL[q++] = dat.D_C[0];
				G.MES_VAL[q++] = dat.D_F[1];
				G.MES_VAL[q++] = dat.D_F[2];
				disp_panel(dat);
			}

			for (int i = 0; i < G.DT.Length; i++) {
				//G.DT[i].DATA = G.MES_VAL[i];
				if (G.DT[i].GID > 0) {
					int h = G.DT[i].GID-1;
					if (h < 0 || h >= csubs.Length) {
						h = h;
					}
					else {
						csubs[h].AddData(G.MES_VAL[i], true);
						//add_data(i, G.DT[i].SEN[h]);
					}
				}
			}
			f_write(m_ts, G.MES_VAL);
			m_ts += new TimeSpan(0,0,0,0,mes_itv);
			if ((this.mes_cnt%10) == 0) {
				//do_auto_scale();
				remove_data();
			}

			this.mes_nxt += mes_itv;
			this.mes_cnt++;
			this.timer1.Enabled = true;
		}
		private void f_open(string path)
		{
			string buf;
			try {
				/*				rd = new StreamReader(filename, Encoding.GetEncoding("Shift_JIS"));*/
				m_wr = new StreamWriter(path, true, Encoding.Default);
				buf = "TIME";
				for (int i = 0; i < G.DT.Length; i++) {
					buf += string.Format(",{0}{1}", G.DT[i].NAME, G.DT[i].UNIT);
				}
				m_wr.WriteLine(buf);
			}
			catch (Exception) {
			}
		}
		private void f_close()
		{//無圧力時の平均値:0.6784321459

			try {
				if (m_wr != null) {
					m_wr.Close();
					m_wr.Dispose();
				}
			}
			catch (Exception) {
			}
			m_wr = null;
		}
		private void f_write(TimeSpan ts, double[] mbuf)
		{
			string buf;
			int[] prec = {
				0, 0, 0, 0,	//R,G,B,C
				1, 1, 1,	//T,H,P
				3, 3, 3,	//AX,AY,AZ
				3, 3, 3,	//GX,GY,GZ
				1, 1,		//LX1,PS1,
				1, 1,		//LX2,PS2,
				1, 1,		//LX3,PS3,
				3,			//VREF
				3,			//圧力
				3			//VCELL
			};
			try {
				buf = string.Format("{0:F3}", ts.TotalSeconds);
				for (int i = 0; i < mbuf.Length; i++) {
					switch (prec[i]) {
						case  0:buf += string.Format(",{0:F0}", mbuf[i]); break;
						case  1:buf += string.Format(",{0:F1}", mbuf[i]); break;
						case  2:buf += string.Format(",{0:F2}", mbuf[i]); break;
						case  3:buf += string.Format(",{0:F3}", mbuf[i]); break;
						default:buf += string.Format(",{0:F0}", mbuf[i]); break;

					}
				}
				m_wr.WriteLine(buf);
			}
			catch (Exception) {
			}
		}

		void set_graph_tbl()
		{
		}
		private bool check_graph_layout()
		{
			return(true);
		}

		private void set_graph_layout()
		{
			if (true) {
			}
			//---
			if (G.DT == null) {
				return;
			}
			for (int i = 0; i < G.DT.Length; i++) {
				if (G.DT[i].GID == 0) {
					continue;
				}
				int h = (G.DT[i].GID - 1);///2;
				//---
				if (h >= this.csubs.Length) {
					continue;
				}
/*				this.csubs[h].pdef.lblTitle = G.DT[i].NAME;
				this.csubs[h].pdef.lblYAxis = G.DT[i].UNIT;
				this.csubs[h].pdef.ymax     = G.DT[i].SMAX;
				this.csubs[h].pdef.ymin     = G.DT[i].SMIN;
				this.csubs[h].pdef.ytic     =(G.DT[i].SMAX-G.DT[i].SMIN)/2;*/
				this.csubs[h].GDEF_LSET(G.DT[i].NAME, G.DT[i].UNIT);
				this.csubs[h].GDEF_PSET(0, this.mes_wid,double.NaN, G.DT[i].SMIN, G.DT[i].SMAX, double.NaN);
				//---
				//this.SENS[i].OBJE = this.csubs[h];
				//---
			}
			if (false) {
				int MIN_OF_GID = 1;//GID=0が非表示で1～
				int MAX_OF_GID = this.charts.Length;
				for (int gid = MIN_OF_GID; gid <= MAX_OF_GID; gid++) {
					int h = (gid-1);

					this.csubs[h].GDEF_PSET(double.NaN,double.NaN,double.NaN,-1, +1, 1);
					//.pdef.ymax = 1;
					//this.csubs[h].pdef.ymin = -1;
					//this.csubs[h].pdef.ytic = 1;
					//this.csubs[h].pdef.lblYAxis = unit;
				}
			}
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
		}
		private void check_free_folder(string path)
		{
			const
			int MAX_OF_FILES = 20;
			string[]files;
			try {
				if (System.IO.Directory.Exists(path) == false) {
					System.IO.Directory.CreateDirectory(path);
				}
				files = System.IO.Directory.GetFiles(path, "????????-??????.csv");
				Array.Sort(files);
				if (files.Length > (MAX_OF_FILES-1)) {
					int cnt = files.Length-(MAX_OF_FILES-1);
					for (int i = 0; i < cnt; i++) {
						System.IO.File.Delete(files[i]);
					}
				}
			}
			catch (Exception ex) {
			}
		}
		private void button3_Click(object sender, EventArgs e)
		{//FREE.RUN / MEASURE
			if (sender == this.button3) {
				//FREE RUN
				DateTime dt = DateTime.Now;
				string path = G.GET_DOC_PATH(null);
				path += "\\FREERUN";
				check_free_folder(path);
				path += "\\";
				path += string.Format("{0:0000}{1:00}{2:00}-{3:00}{4:00}{5:00}",
											dt.Year, dt.Month, dt.Day,
											dt.Hour, dt.Minute, dt.Second);
				path += ".csv";
				f_open(path);
			}
			else {
				return;
			}
			this.mes_wid = (int)this.numericUpDown2.Value; //sec
			this.mes_pts_in_wid = this.mes_wid *1000 / this.mes_itv;

			this.mes_cnt = 0;
			this.mes_nxt = System.Environment.TickCount;
			this.timer1.Interval = 1;
			this.timer1.Enabled = true;
			this.button4.Enabled = true;
			this.button3.Enabled = false;
			//this.numericUpDown1.Enabled = false;
			this.numericUpDown2.Enabled = false;
			int i = 0;
			foreach (PictureBox c in this.charts) {
				this.csubs[i].GDEF_PSET(0, this.mes_wid, double.NaN, double.NaN, double.NaN, double.NaN);
				this.csubs[i].rbuf.clear();
				this.csubs[i].Update();
				i++;
			}
			//do_auto_scale();
		}

		private void button4_Click(object sender, EventArgs e)
		{//stop
			this.timer1.Enabled = false;
			this.button3.Enabled = true;
			this.button4.Enabled = false;
//			this.numericUpDown1.Enabled = true;
			this.numericUpDown2.Enabled = true;
			f_close();
		}
		private void check_rewrite()
		{
			if (G.eep_put_all()) {
				//if (G.mlog("#qセンサーをリセットしますか?") == System.Windows.Forms.DialogResult.Yes) {
					//G.eep_put_all();
				//}
			}
			if (G.DT.Length != G.SS.SEN_GRH_GID.Length) {
				return;//あり得る？
			}
			for (int i = 0; i < G.DT.Length; i++) {
				bool flag = false;
				if (G.DT[i].GID != G.SS.SEN_GRH_GID[i]) {
					flag = true;
				}
				if (G.DT[i].SMAX != G.SS.SEN_SCL_MAX[i]) {
					flag = true;
				}
				if (G.DT[i].SMIN != G.SS.SEN_SCL_MIN[i]) {
					flag = true;
				}
				if (flag) {
					G.DT[i].GID  = G.SS.SEN_GRH_GID[i];
					G.DT[i].SMAX = G.SS.SEN_SCL_MAX[i];
					G.DT[i].SMIN = G.SS.SEN_SCL_MIN[i];

					int h = G.DT[i].GID-1;
					if (h < 0 || h >= csubs.Length) {
						h = h;
					}
					else {
						csubs[h].GDEF_LSET(G.DT[i].NAME, G.DT[i].UNIT);
						csubs[h].GDEF_PSET(double.NaN, double.NaN, double.NaN, G.DT[i].SMIN, G.DT[i].SMAX, double.NaN);
						csubs[h].Update();
					}
				}
			}
		}
		private void button97_Click(object sender, EventArgs e)
		{//設定
			if (G.UIF_LEVL > 0) {
				frmSettings frm = new frmSettings();
				frm.m_ss = (G.SYSSET)G.SS.Clone();;
				frm.m_sp = (G.SENPAR)G.SP.Clone();;
				if (frm.ShowDialog(this) == System.Windows.Forms.DialogResult.OK) {
					G.SS = (G.SYSSET)frm.m_ss.Clone();
					G.SP = (G.SENPAR)frm.m_sp.Clone();
					check_rewrite();
				}
			}
			else {
				Form20 frm = new Form20();
				frm.m_sp = (G.SENPAR)G.SP.Clone();
				if (frm.ShowDialog(this) == System.Windows.Forms.DialogResult.OK) {
					G.SP = (G.SENPAR)frm.m_sp.Clone();
					check_rewrite();
				}
			}
		}

		private void button7_Click(object sender, EventArgs e)
		{//DRAW
			for (int i = 0; i < this.charts.Length; i++) {
				Graphics g = this.charts[i].CreateGraphics();
				if (i == (this.charts.Length - 1)) {
					i = i;
				}
				csubs[i].GDEF_GRID(g);
				g.Dispose();
			}
			//this.tabControl1.Dock = DockStyle.Fill;
		}

		private void Form02_Resize(object sender, EventArgs e)
		{
			const
			int gap = 3;
#if true
			int wid = this.panel32.Width;
			int hei = this.panel32.Height;
			wid = this.tabPage1.Width;
			hei = this.tabPage1.Height;
#else
			int wid = this.ClientSize.Width;
			int hei = this.ClientSize.Height;
#endif
			int uniw = (wid - (C_COLCNT - 1) * gap) / C_COLCNT;
			int unih = (hei - (C_ROWCNT - 1) * gap) / C_ROWCNT;
			int i = 0, xoff, yoff;
			for (int x = 0; x < C_COLCNT; x++) {
				xoff =  uniw * x;
				xoff+= gap * x;
				for (int y = 0; y < C_ROWCNT; y++) {
					yoff = unih * y;
					yoff+= gap * y;
					this.panels[i].Left = xoff;
					this.panels[i].Top = yoff;
					this.panels[i].Width = uniw;
					this.panels[i].Height = unih;
					i++;
				}
			}
		}

		private void pictureBox1_Paint(object sender, PaintEventArgs e)
		{
			object obj = ((PictureBox)sender).Tag;
			if (obj == null) {
				return;
			}
			int i = (int)obj;
			if (csubs[i] == null) {
				return;
			}
			if (i == 0) {
				i = i;
			}
			if (((PictureBox)sender).Image != null) {
				Graphics g = Graphics.FromImage(((PictureBox)sender).Image);
				g.Clear(Color.Blue);
				csubs[i].GDEF_GRID(g);
				g.Dispose();
			}
			else {
				Graphics g = e.Graphics;// ((PictureBox)sender).CreateGraphics();
				g.Clear(Color.Blue);
				csubs[i].GDEF_GRID(g);
				//			g.Dispose();
			}
		}
		private void pictureBox1_Resize(object sender, EventArgs e)
		{
			object obj = ((PictureBox)sender).Tag;
			if (obj == null) {
				return;
			}
			int i = (int)obj;
			if (csubs[i] == null) {
				return;
			}
			if (i == 0) {
				i = i;
			}
			if (true) {
				PictureBox pb = (PictureBox)sender;
				Image img = pb.Image;
				if (img == null || img.Width != pb.Width || img.Height != pb.Height) {
					if (img != null) {
						img.Dispose();
						img = null;
					}
					//pb.Image = null;// new Bitmap(pb.Width, pb.Height);
				}
			}
			if (true) /*for (int i = 0; i < this.charts.Length; i++)*/ {
				PictureBox c = this.charts[i];
				csubs[i].GDEF_INIT(3, c, new Rectangle(25, 13, -13-12, -20));
				//csubs[i].GDEF_PSET(0, 120, 60, -1, +1, 1);
				c.Invalidate();
			}
		}
#if  false
		private void button5_Click(object sender, EventArgs e)
		{
			if (false) {
			}
			else if (sender == this.button5) {
				//ch0->on
				numericUpDown3_ValueChanged(this.numericUpDown3, null);
				D.SET_PWM_STS(/*CHAN*/0, /*ON*/1);
				this.button6.Enabled = true;
				this.button5.Enabled = false;
			}
			else if (sender == this.button6) {
				//ch0->off
				D.SET_PWM_STS(/*CHAN*/0, /*OFF*/0);
				this.button6.Enabled = false;
				this.button5.Enabled = true;
			}
			else if (sender == this.button7) {
				//ch1->on
				numericUpDown3_ValueChanged(this.numericUpDown4, null);
				D.SET_PWM_STS(/*CHAN*/1, /*ON*/1);
				this.button8.Enabled = true;
				this.button7.Enabled = false;
			}
			else if (sender == this.button8) {
				//ch1->off
				D.SET_PWM_STS(/*CHAN*/1, /*OFF*/0);
				this.button8.Enabled = false;
				this.button7.Enabled = true;
			}
		}
#endif
		//private void numericUpDown3_ValueChanged(object sender, EventArgs e)
		//{
		//    if (sender == this.numericUpDown3) {
		//        D.SET_PWM_DTY(/*CHAN*/0, (int)this.numericUpDown3.Value);
		//    }
		//    else {
		//        D.SET_PWM_DTY(/*CHAN*/1, (int)this.numericUpDown4.Value);
		//    }

		//}
		private string B2S(byte[] b)
		{
			string buf = "";
			for (int i = 0; i < b.Length; i++) {
				if (b[i] < 0x20 || b[i] > 0x7e) {
					break;
				}
				buf += (char)b[i];
			}
			return(buf);
		}
		private void OnClicks(object sender, EventArgs e)
		{
			try {
				if (false) {
				}
				//else if (sender == this.button28) {
				//    frmSettings frm = new frmSettings();
				//    frm.m_ss = (G.SYSSET)G.SS.Clone();;
				//    frm.m_sp = (G.SENPAR)G.SP.Clone();;
				//    if (frm.ShowDialog(this) == System.Windows.Forms.DialogResult.OK) {
				//        G.SS = (G.SYSSET)frm.m_ss.Clone();
				//        G.SP = (G.SENPAR)frm.m_sp.Clone();
				//        if (G.eep_put_all()) {
				//        if (G.mlog("#qセンサーをリセットしますか?") == System.Windows.Forms.DialogResult.Yes) {
				//            //G.eep_put_all();
				//        }
				//        }
				//    }
				//}
				else if (sender == this.button1) {
					//connect
					if (D.INIT()) {
						this.button1.Enabled = false;
						this.button2.Enabled = true;
						this.button3.Enabled = true;
						this.button4.Enabled = false;
						//this.button6.Enabled = true;
						//this.button7.Enabled = true;
						//this.button8.Enabled = true;
						this.button97.Enabled = true;

						if (D.DEV_TYPE == 0) {
							//this.button21.Enabled = false;
							//this.button20.Enabled = false;
						}
						else {
							G.mlog("Internal Error");
						}
						G.eep_get_all();
						//detect();
						this.Text = string.Format("CRT.MONITOR:[{0}]", B2S(G.SP.DEV_ID));
					}
				}
				else if (sender == this.button2) {
					D.TERM();
					this.button1.Enabled = true;
					this.button2.Enabled = false;
					this.button3.Enabled = false;
					this.button4.Enabled = false;
					//this.button6.Enabled = false;
					//this.button7.Enabled = false;
					//this.button8.Enabled = false;
					this.button97.Enabled = false;
					this.Text ="CRT.MONITOR";
				}
				//----------------------------------------------------
			}
			catch (Exception ex) {
				G.mlog(ex.Message);
			}
		}
		void disp_panel(S.MES_DAT dat)
		{
			//0x29://tcs34725
			this.textBox40.Text = string.Format("{0:F0}", dat.C_R);
			this.textBox41.Text = string.Format("{0:F0}", dat.C_G);
			this.textBox42.Text = string.Format("{0:F0}", dat.C_B);
			this.textBox43.Text = string.Format("{0:F0}", dat.C_C);

			//case 0x38://rpr0521rs:左
			this.textBox44.Text = string.Format("{0:F2}", dat.L_L);
			this.textBox45.Text = string.Format("{0:F0}", dat.L_P);
			//0x39://rpr0521rs:右
			this.textBox46.Text = string.Format("{0:F2}", dat.R_L);
			this.textBox47.Text = string.Format("{0:F0}", dat.R_P);
			//:奥
			this.textBox48.Text = string.Format("{0:F2}", dat.B_L);
			this.textBox49.Text = string.Format("{0:F0}", dat.B_P);
			//0x68://mpu9250
			this.textBox17.Text = string.Format("{0:F2}", dat.A_X);
			this.textBox18.Text = string.Format("{0:F2}", dat.A_Y);
			this.textBox19.Text = string.Format("{0:F2}", dat.A_Z);
			this.textBox20.Text = string.Format("{0:F2}", dat.G_X);
			this.textBox21.Text = string.Format("{0:F2}", dat.G_Y);
			this.textBox22.Text = string.Format("{0:F2}", dat.G_Z);
			//0x76://bme280
			this.textBox12.Text = string.Format("{0:F2}", dat.S_T);
			this.textBox13.Text = string.Format("{0:F2}", dat.S_H);
			this.textBox14.Text = string.Format("{0:F2}", dat.S_P);
			//adc
			this.textBox25.Text = string.Format("{0:F1}", dat.D_C[0]);
			this.textBox26.Text = string.Format("{0:F1}", dat.D_C[1]);
			this.textBox27.Text = string.Format("{0:F1}", dat.D_C[2]);
			this.textBox28.Text = string.Format("{0:F3}", dat.D_V[0]);
			this.textBox29.Text = string.Format("{0:F3}", dat.D_V[1]);
			this.textBox30.Text = string.Format("{0:F3}", dat.D_V[2]);
			this.textBox32.Text = string.Format("{0:F3}", dat.D_F[1]);
			this.textBox33.Text = string.Format("{0:F3}", dat.D_F[2]);

		}
		class ROMDAT {
			public int		MES_MIDX;		// 2:TTL  2 
			public int		USE_SENS;		// 1:TTL  3 
			public int		CND_MODE;		// 1:TTL  4 
			public double	CND_R_VL;		// 4:TTL  8  
			public int		CPU_TIME;		// 4:TTL 12, [100ms]
			public int		LCK_TIME;		// 2:TTL 14, [100ms]
			public int		MES_TIME;		// 2:TTL 16, [100ms]
			public double	LCK_PRES;		// 4:TTL 20 
			public double	MES_PRES;		// 4:TTL 24
			public double	SEN_F_SA;		// 4:TTL 28
			public double	SEN_F_SB;		// 4:TTL 32
			public double	SEN_F_SZ;		// 4:TTL 36
			public double	SEN_F_SN;		// 4:TTL 40
			public double	SEN_R_VL;		// 4:TTL 44
		}
		private void get_romdat(byte[] buf, out ROMDAT dat)
		{
			dat = new ROMDAT();
			dat.MES_MIDX =  BitConverter.ToInt16 (buf,  0);
			dat.USE_SENS =  buf[2];
			dat.CND_MODE =  buf[3];
			dat.CND_R_VL =  BitConverter.ToSingle(buf,  4);
			dat.CPU_TIME =  BitConverter.ToInt32 (buf,  8);
			dat.LCK_TIME =  BitConverter.ToInt16 (buf, 12);
			dat.MES_TIME =  BitConverter.ToInt16 (buf, 14);
			dat.LCK_PRES =  BitConverter.ToSingle(buf, 16);
			dat.MES_PRES =  BitConverter.ToSingle(buf, 20);
			dat.SEN_F_SA =  BitConverter.ToSingle(buf, 24);
			dat.SEN_F_SB =  BitConverter.ToSingle(buf, 28);
			dat.SEN_F_SZ =  BitConverter.ToSingle(buf, 32);
			dat.SEN_F_SN =  BitConverter.ToSingle(buf, 36);
			dat.SEN_R_VL =  BitConverter.ToSingle(buf, 40);
		}
		//---
		CLMDEF[] m_clms_m = new CLMDEF[] {
//			new CLMDEF(-1, "NO."           ,  70, ALIGN.CENTER),
			new CLMDEF( 0, "No."           ,  40, ALIGN.CENTER, null),
			new CLMDEF( 0, "センサー値"    ,  80, ALIGN.RIGHT , null),
			new CLMDEF( 0, "測定時間\r[s]" ,  80, ALIGN.RIGHT , "F1"),
			new CLMDEF( 0, "センサー\r種別",  80, ALIGN.CENTER, null),
			new CLMDEF( 0, "測定方法"      ,  80, ALIGN.CENTER, null),
			new CLMDEF( 0, "圧力@LOCK\r[N]",  80, ALIGN.RIGHT , "F2"),
			new CLMDEF( 0, "圧力@測定\r[N]",  80, ALIGN.RIGHT , "F2"),
			new CLMDEF( 0, "時間@LOCK\r[s]",  80, ALIGN.RIGHT , "F1"),
			new CLMDEF( 0, "Sa"            ,  80, ALIGN.RIGHT , "F0"),
			new CLMDEF( 0, "Sb"            ,  80, ALIGN.RIGHT , "F0"),
			new CLMDEF( 0, "Sz"            ,  80, ALIGN.RIGHT , "F0"),
			new CLMDEF( 0, "R:判定値"      ,  80, ALIGN.RIGHT , "F2"),
			new CLMDEF( 0, "R:計算値"      ,  80, ALIGN.RIGHT , "F2"),
			new CLMDEF( 0, "CPU\rTIME"     ,  80, ALIGN.RIGHT , null),
        };
		private void ResetGrid(DataGridView ctl, CLMDEF[] clms, string key, int rh_width)
        {
            int i = 0, q = 0;
			int	width = -1;

            ctl.Columns.Clear();
			if (key != null) {
			//width = CIni.GetProfileInt("APPLICATION", key+"RH", rh_width);
			}
			else {
			//width = rh_width;
			}
			if (width >= 0) {
				ctl.RowHeadersWidth = width;
			}
            foreach (CLMDEF clm in clms) {
#if false
#else
				//if (clm.type == CLMTYPE.TXT) {
					ctl.Columns.Add(q.ToString(), clm.name);
				//}
				//else if (clm.type == CLMTYPE.CHK) {
				//	DataGridViewCheckBoxColumn c = new DataGridViewCheckBoxColumn();
				//	c.HeaderText = clm.name;
				//	c.Name = q.ToString();
				//	ctl.Columns.Add(c);
				//}
#endif
				if (key != null) {
				//width = CIni.GetProfileInt("APPLICATION", key+i.ToString(), clm.width);
				}
				else {
				width = clm.width;
				}
				ctl.Columns[i].Width        = width;
				ctl.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
#if false//2016.01.23
				ctl.Columns[i].SortMode     = DataGridViewColumnSortMode.Automatic;
#else
				ctl.Columns[i].SortMode     = DataGridViewColumnSortMode.NotSortable;
#endif
				if (clm.index >= 0) {
					ctl.Columns[i].Visible = true;
				}
				else  {
					ctl.Columns[i].Visible = false;
				}
                switch (clm.align)
                {
                    case ALIGN.LEFT:
                        ctl.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        break;
                    case ALIGN.RIGHT:
                        ctl.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        break;
                    default:
                        ctl.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;
                }
				if (clm.format != null) {
					ctl.Columns[i].DefaultCellStyle.Format = clm.format;
				}
				i++;
				q++;
            }
			/*
			ctl.KeyDown -= new KeyEventHandler(Grid_KeyDown);
			ctl.KeyDown += new KeyEventHandler(Grid_KeyDown);
			 */
        }
		private string CT2S(int cputime)
		{
			int sec, min, hor;
			sec = cputime %600;
			cputime /= 600;
			min = cputime % 60;
			cputime /= 60;
			hor = cputime;
			return(string.Format("{0:00}:{1:00}:{2:00}", hor, min, sec/10));
		}
		private void button9_Click(object sender, EventArgs e)
		{
			D.SET_MES_PAR(0, /*STOP*/0);
			G.eep_get_cnt();
			int dcnt;
			if (G.SP.ROM_DAT_STA == 0) {
				dcnt = G.SP.ROM_DAT_END;
			}
			else {
				dcnt = 240;
			}
			if (dcnt <= 0) {
				G.mlog("#iEEPROMに保存されたデータはありません.");
				goto skip;
			}
			this.dataGridView1.Rows.Clear();
			string[] SENS = new string[] {"R", "G", "B", "C"};
			string[] MODE = new string[] {"減衰時間", "変化率", "戻り時間"};
			try {
				for (int i = 0; i < dcnt; i++) {
					byte[]	buf;
					ROMDAT	dat;
					int		idx = G.SP.ROM_DAT_STA+i;
					ArrayList ar = new ArrayList();

					if (idx >= 240) {
						idx -= 240;
					}
					D.GET_EEP_DAT(idx, out buf);
					get_romdat(buf, out dat);
					ar.Add(dat.MES_MIDX);
					ar.Add(dat.SEN_F_SN);
					ar.Add(dat.MES_TIME/10.0);
					ar.Add(SENS[dat.USE_SENS]);

					ar.Add(MODE[dat.CND_MODE]);
					ar.Add(dat.LCK_PRES);
					ar.Add(dat.MES_PRES);
					ar.Add(dat.LCK_TIME/10.0);
					ar.Add(dat.SEN_F_SA);
					ar.Add(dat.SEN_F_SB);
					ar.Add(dat.SEN_F_SZ);
					ar.Add(dat.CND_R_VL);
					ar.Add(dat.SEN_R_VL);
					ar.Add(CT2S(dat.CPU_TIME));

					this.dataGridView1.Rows.Add(ar.ToArray());
				}
			}
			catch (Exception ex) {
			}
		skip:
			D.SET_MES_PAR(0, /*RUN*/1);
			if (this.dataGridView1.Rows.Count > 0) {
				button12.Enabled = true;
			}
			else {
				button12.Enabled = false;
			}
		}

		private void button11_Click(object sender, EventArgs e)
		{
			if (G.mlog("#qEEPROMに保存されたデータをクリアしますか?") == System.Windows.Forms.DialogResult.Yes) {
#if true
				G.eep_put_clr();
#else
				D.SET_MES_PAR(0, /*STOP*/0);
				D.SET_EXE_CMD(4 /* set rom_sta, rom_end to 0 */);
				D.SET_MES_PAR(0, /*RUN*/1);
#endif
			}
		}
		private void button12_Click(object sender, EventArgs e)
		{
			SaveFileDialog dlg = new SaveFileDialog();
			string fold;
			
			fold = System.IO.Path.GetDirectoryName(G.SS.BEFORE_PATH);
			if (!string.IsNullOrEmpty(fold)) {
				if (!System.IO.Directory.Exists(fold)) {
					fold = "";
				}
			}
			dlg.Filter = "Data files (*.csv)|*.csv|All files (*.*)|*.*";
			dlg.DefaultExt = "csv";
			dlg.InitialDirectory = fold;
			dlg.FileName = "";

			if (dlg.ShowDialog() != DialogResult.OK) {
				return;
			}
			G.SS.BEFORE_PATH = dlg.FileName;
			//-----
			StreamWriter sw;
			sw = new StreamWriter(dlg.FileName, false, Encoding.GetEncoding("Shift_JIS"));
			sw.WriteLine(T.GRID2CSV(this.dataGridView1));
			sw.Close();
			sw.Dispose();
			return;
		}
        static bool ctrlKeyFlg = false;
        static bool AKeyFlg = false;
        static bool BKeyFlg = false;
        static bool CKeyFlg = false;

		private void Form02_KeyDown(object sender, KeyEventArgs e)
		{
	        bool flag = false;

			if(e.KeyCode == Keys.ControlKey) {
				ctrlKeyFlg = true;
			}
			if(e.KeyCode == Keys.I) {
				AKeyFlg = true;
			}
			if(e.KeyCode == Keys.O) {
				BKeyFlg = true;
			}
			if(e.KeyCode == Keys.P) {
				CKeyFlg = true;
			}
			if(ctrlKeyFlg && AKeyFlg && BKeyFlg && CKeyFlg) {
                flag = true;
                ctrlKeyFlg = false;
                AKeyFlg = false;
                BKeyFlg = false;
                CKeyFlg = false;
            }
            if (flag) {
				//var frm = new frmMessage();
				//frm.ShowDialog(this);
				string msg;
				if (G.SS.ETC_UIF_LEVL == 0/* || G.SS.ETC_UIF_LEVL == 1*/) {
					//G.SS.ETC_UIF_BACK = G.SS.ETC_UIF_LEVL;
					G.SS.ETC_UIF_LEVL = 1;
					msg = "ソフトウェアは次回起動時に開発者モードで起動します。";
				}
				else {
					G.SS.ETC_UIF_LEVL = 0;//G.SS.ETC_UIF_BACK;
					msg = "ソフトウェアは次回起動時にユーザモードで起動します。";
				}

				G.mlog("#i" + msg);
	            flag = false;
            }
		}

		private void Form02_KeyUp(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.ControlKey) {
				ctrlKeyFlg = false;
			}
			if(e.KeyCode == Keys.I) {
				AKeyFlg = false;
			}
			if(e.KeyCode == Keys.O) {
				BKeyFlg = false;
			}
			if(e.KeyCode == Keys.P) {
				CKeyFlg = false;
			}
		}
	}
}
