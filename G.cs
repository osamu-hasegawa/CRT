using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//-----------------------
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Drawing;
//-----------------------
using System.Runtime.InteropServices;

namespace CRTMONITOR
{
	public class G
	{
		public delegate void DLG_VOID_VOID();
		public delegate void DLG_VOID_BOOL(bool b);
		public delegate void DLG_VOID_INT(int n);
		public delegate bool DLG_BOOL_OBJECTS(out object[] obj);
		public delegate void DLG_VOID_OBJECTS(out object[] obj);

		public class APPSET:System.ICloneable
		{
			public int TRACE_LEVEL = 0;
			public int DEBUG_MODE = 0;
			public int APP_F02_LFT =  10;
			public int APP_F02_TOP =   5;
			public int APP_F02_WID = 600;
			public int APP_F02_HEI = 800;
			//public string AUT_BEF_PATH = "";
			public string BEFORE_PATH = "";
			//---
			//public string PLM_AUT_FOLD = "";
			//public string MOZ_CND_FOLD = "";

			public Object Clone()
			{
				APPSET cln = (APPSET)this.MemberwiseClone();
				return (cln);
			}
			public bool load(ref APPSET ss)
			{
				string path = GET_DOC_PATH("CRTMONITOR.xml");
				bool ret = false;
				try {
					XmlSerializer sz = new XmlSerializer(typeof(APPSET));
					System.IO.StreamReader fs = new System.IO.StreamReader(path, System.Text.Encoding.Default);
					APPSET obj;
					obj = (APPSET)sz.Deserialize(fs);
					fs.Close();
					obj = (APPSET)obj.Clone();
					ss = obj;
					ret = true;
				}
				catch (Exception /*ex*/) {
				}
				return(ret);
			}
			//
			public bool save(APPSET ss)
			{
				string path = GET_DOC_PATH("CRTMONITOR.xml");
				bool ret = false;
				try {
					XmlSerializer sz = new XmlSerializer(typeof(APPSET));
					System.IO.StreamWriter fs = new System.IO.StreamWriter(path, false, System.Text.Encoding.Default);
					sz.Serialize(fs, ss);
					fs.Close();
					ret = true;
				}
				catch (Exception /*ex*/) {
				}
				return (ret);
			}
		}
		public class SYSSET : System.ICloneable
		{
			//public int		TRACE_LEVEL = 0;
			//public int	APP_F01_LFT = 700;
			//public int	APP_F01_TOP =   5;
			//public int		APP_F02_LFT =  10;
			//public int		APP_F02_TOP =   5;
			//public int		APP_F02_WID = 600;
			//public int		APP_F02_HEI = 800;
			//public string	AUT_BEF_PATH = null;
			[XmlIgnoreAttribute]
			public string	BEFORE_PATH = null;
			//---
/*
0:ユーザ用(暫定)
1:ユーザ用(最終):0
2:開発者用(一度):1
3:開発者用(常に):2
 */
			public int		ETC_UIF_LEVL = 0;
//			public int		ETC_UIF_BACK = 0;
			//---
			public int		GRP_ROW_CNT = 4;
			public int		GRP_COL_CNT = 5;
			//---(22ケ:adc基準を含む)
			public int[] SEN_GRH_GID = { 1, 2, 3, 4, 8,12,16, 5, 6,7,9,10,11,13,17,14,18,15,19, 0,20, 0 };
			//---(22ケ:adc基準を含む)
			//---
			//---(22ケ:adc基準を含む)
			public double[] SEN_SCL_MAX = { 9000, 9000, 9000, 11000, 40, 100, 1100, 5, 5, 5, 300, 300, 300, 500, 1500, 500, 1500, 500, 1500, 1, 5, 1 };
			public double[] SEN_SCL_MIN = {    0,    0,    0,     0,  0,   0,  900,-5,-5,-5,-300,-300,-300,   0,    0,   0,    0,   0,    0, 0, 0, 0 };
#if false
#endif
			//---
			public Object Clone()
			{
				SYSSET cln = (SYSSET)this.MemberwiseClone();
				if (this.SEN_GRH_GID != null) {
				    cln.SEN_GRH_GID = (int[])this.SEN_GRH_GID.Clone();
				}
				else {
				    cln.SEN_GRH_GID = null;
				}
				if (this.SEN_SCL_MAX != null) {
				    cln.SEN_SCL_MAX = (double[])this.SEN_SCL_MAX.Clone();
				}
				else {
				    cln.SEN_SCL_MAX = null;
				}
				if (this.SEN_SCL_MIN != null) {
				    cln.SEN_SCL_MIN = (double[])this.SEN_SCL_MIN.Clone();
				}
				else {
				    cln.SEN_SCL_MIN = null;
				}
				return (cln);
			}
			//
			public bool load(ref SYSSET ss)
			{
				string path = GET_DOC_PATH("settings.xml");
				bool ret = false;
				try {
					XmlSerializer sz = new XmlSerializer(typeof(SYSSET));
					System.IO.StreamReader fs = new System.IO.StreamReader(path, System.Text.Encoding.Default);
					SYSSET obj;
					obj = (SYSSET)sz.Deserialize(fs);
					fs.Close();
					obj = (SYSSET)obj.Clone();
					ss = obj;
					ret = true;
				}
				catch (Exception ex) {
					System.Diagnostics.Debug.WriteLine(ex.ToString());
				}
				return(ret);
			}
			//
			public bool save(SYSSET ss)
			{
				string path = GET_DOC_PATH("settings.xml");
				bool ret = false;
				try {
					XmlSerializer sz = new XmlSerializer(typeof(SYSSET));
					System.IO.StreamWriter fs = new System.IO.StreamWriter(path, false, System.Text.Encoding.Default);
					sz.Serialize(fs, ss);
					fs.Close();
					ret = true;
				}
				catch (Exception ex) {
					System.Diagnostics.Debug.WriteLine(ex.ToString());
				}
				return (ret);
			}
		};
		public class SENPAR : System.ICloneable
		{
			//---
			public int		RESET_FLAG;
			public byte[]	DEV_ID = {0,0,0,0,0,0};
			public int[]	PWM_LED_DTY = { 10, 10 };
			public double[]	ADC_COF_GRD = {1,1,1};
			public double[]	ADC_COF_OFS = { 0,-0.68, 0 };
			//--- TEMP/HUMI/PRESS
			public int		D76_F20_HUMI = 1;		//0:Skipped(output:0x80000), 1:oversampling x1, 2:Humidity oversampling x 1
			public int		D76_F40_MODE = 3;		//0:Sleep, 1,2:Forced, 3:Normal
			public int		D76_F42_PRES = 1;		//Pressure oversampling x 1
			public int		D76_F45_TEMP = 1;		//Temperature oversampling x 1
			public int		D76_F52_FILT = 2;		//Filter(0:off, 1:2, 2,4, 3:8, 4:16)
			public int		D76_F55_STBY = 2;		//Tstandby(0:0.5, 1:62.5, 2:125, 3:250, 4:500, 5:1000ms)
			//--- R/G/B/C
			public int		D29_010_ATIM = (256 - 10);//Integ.Time(255:2.4ms, 254:4.8ms, 246:24ms,... 1:612ms)
			public int		D29_0F0_GAIN = 1;		//Gain(0:x1, 1:x4, 2:x16, 3:x60)
			//---
			public int		D68_190_SMRT = 4;		//Smp.Rate(4:200Hz, 9:100Hz,...) -> (1KHz/(1+SMRT))
			public int		D68_1A0_GLPF = 3;		//LPF.G(0:250, 1:184, 2:92, 3:41, 4:20, ...6:5[Hz])
			public int		D68_1B3_GSCL = 1;		//Gyro(0:+-250, 1:500, 2:1000, 3:2000)
			public int		D68_1C3_ASCL = 1;		//Accl(0:+-2g, 1:+-4g, , 2:+-8g, 3:+-16g)
			public int		D68_1D0_ALPF = 3;		//LPF.A(0:218, 1:218, 2:99, 3:45, 4:21, ...6:5[Hz])
			//---
			public int[]	D38_410_MSTM = {6,6,6};		//Mes.Time(0:ALS/PS=STB/STB,...,6:100ms/100ms, 7:100,400ms,...,12:50/50ms)
			public int[]	D38_424_ALG1 = {1,1,1};		//Gain(0:x1, 1:x2, 2:x64, 3:x128)
			public int[]	D38_422_ALG2 = {1,1,1};		//Gain(0:x1, 1:x2, 2:x64, 3:x128)
			public int[]	D38_420_LEDC = {0,0,0};		//LED.Current(0:25, 1:50, 2:100, 3:200mA)
			public int[]	D38_434_PSG0 = { 1, 1, 1 };		//Gain(0:x1, 1:x2, 2:x4)
			//---(22ケ:adc基準を含む)
			public double[] SEN_COF_GRD = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3.04,1 };
			public double[] SEN_COF_OFS = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0 };
			//---
			public int		LED_LGT_CHK;
			public int		LED_LGT_OPT;
			public int[]	LED_LGT_SEN = {0,0,0};
			public int[]	LED_LGT_VAL = {0,0,0};
			public int[]	LED_LGT_CND = {0,0,0};
			public int		LED_OFF_CHK;
			public int		LED_OFF_OPT;
			public int[]	LED_OFF_SEN = {0,0,0};
			public int[]	LED_OFF_VAL = {0,0,0};
			public int[]	LED_OFF_CND = {0,0,0};
			//---
			public int		LCD_FRE_TOP;
			public int		LCD_FRE_BTM;
			public byte[]	LCD_FRE_STP = {0,0,0,0,0,0,0,0,0,0};
			public byte[]	LCD_FRE_SBT = {0,0,0,0,0,0,0,0,0,0};
			public int		LCD_LCK_TOP;
			public int		LCD_LCK_BTM;
			public byte[]	LCD_LCK_STP = {0,0,0,0,0,0,0,0,0,0};
			public byte[]	LCD_LCK_SBT = {0,0,0,0,0,0,0,0,0,0};
			public int		LCD_MES_TOP;
			public int		LCD_MES_BTM;
			public byte[]	LCD_MES_STP = {0,0,0,0,0,0,0,0,0,0};
			public byte[]	LCD_MES_SBT = {0,0,0,0,0,0,0,0,0,0};
			//---
			public double	BAT_WRN_VOL;
			public double	BAT_HLT_VOL;
			//---
			public int		MES_USE_SEN;
			public double	MES_FRE_PRS;
			public double	MES_LCK_PRS;
			public double	MES_MES_PRS;
			public int		MES_LCK_TOT;
			public int		MES_MES_CND;
			public double	MES_MES_VRD;
			public double	MES_MES_VRC;
			public double	MES_MES_VRB;
			public int		MES_MES_TOT;
			public int		MES_MES_WAI;
			//---
			public int		ROM_DAT_STA;
			public int		ROM_DAT_END;
#if false
			//---
			public int[]	MES_CHK ={0,0,0,0,0,0,0,0,0,0};
			public double[]	MES_VAL ={0,0,0,0,0,0,0,0,0,0};
#endif
			//---
			public Object Clone()
			{
				SENPAR cln = (SENPAR)this.MemberwiseClone();
				if (this.DEV_ID       != null) {cln.DEV_ID       = (byte  [])this.DEV_ID.Clone();}
				if (this.PWM_LED_DTY  != null) {cln.PWM_LED_DTY  = (int   [])this.PWM_LED_DTY.Clone();}
				if (this.ADC_COF_GRD  != null) {cln.ADC_COF_GRD  = (double[])this.ADC_COF_GRD.Clone();}
				if (this.ADC_COF_OFS  != null) {cln.ADC_COF_OFS  = (double[])this.ADC_COF_OFS.Clone();}
				if (this.DEV_ID       != null) {cln.D38_410_MSTM = (int   [])this.D38_410_MSTM.Clone();}
				if (this.D38_424_ALG1 != null) {cln.D38_424_ALG1 = (int   [])this.D38_424_ALG1.Clone();}
				if (this.D38_422_ALG2 != null) {cln.D38_422_ALG2 = (int   [])this.D38_422_ALG2.Clone();}
				if (this.D38_420_LEDC != null) {cln.D38_420_LEDC = (int   [])this.D38_420_LEDC.Clone();}
				if (this.D38_434_PSG0 != null) {cln.D38_434_PSG0 = (int   [])this.D38_434_PSG0.Clone();}
				if (this.SEN_COF_GRD  != null) {cln.SEN_COF_GRD  = (double[])this.SEN_COF_GRD.Clone();}
				if (this.SEN_COF_OFS  != null) {cln.SEN_COF_OFS  = (double[])this.SEN_COF_OFS.Clone();}
				if (this.LED_LGT_SEN  != null) {cln.LED_LGT_SEN  = (int   [])this.LED_LGT_SEN.Clone();}
				if (this.LED_LGT_VAL  != null) {cln.LED_LGT_VAL  = (int   [])this.LED_LGT_VAL.Clone();}
				if (this.LED_LGT_CND  != null) {cln.LED_LGT_CND  = (int   [])this.LED_LGT_CND.Clone();}
				if (this.LED_OFF_SEN  != null) {cln.LED_OFF_SEN  = (int   [])this.LED_OFF_SEN.Clone();}
				if (this.LED_OFF_VAL  != null) {cln.LED_OFF_VAL  = (int   [])this.LED_OFF_VAL.Clone();}
				if (this.LED_OFF_CND  != null) {cln.LED_OFF_CND  = (int   [])this.LED_OFF_CND.Clone();}
				if (this.LCD_FRE_STP  != null) {cln.LCD_FRE_STP  = (byte  [])this.LCD_FRE_STP.Clone();}
				if (this.LCD_FRE_SBT  != null) {cln.LCD_FRE_SBT  = (byte  [])this.LCD_FRE_SBT.Clone();}
				if (this.LCD_LCK_STP  != null) {cln.LCD_LCK_STP  = (byte  [])this.LCD_LCK_STP.Clone();}
				if (this.LCD_LCK_SBT  != null) {cln.LCD_LCK_SBT  = (byte  [])this.LCD_LCK_SBT.Clone();}
				if (this.LCD_MES_STP  != null) {cln.LCD_MES_STP  = (byte  [])this.LCD_MES_STP.Clone();}
				if (this.LCD_MES_SBT  != null) {cln.LCD_MES_SBT  = (byte  [])this.LCD_MES_SBT.Clone();}
#if false
				//---
				if (this.MES_CHK != null) {
					cln.MES_CHK = (int[])this.MES_CHK.Clone();
				}
				//---
				if (this.MES_VAL != null) {
					cln.MES_VAL = (double[])this.MES_VAL.Clone();
				}
#endif
				//---
				return (cln);
			}
		};
		public class DEVICE_TBL/*:System.ICloneable*/
		{
			//public int	BID;	//バスID(アドレス: 0x00,0x01,,0x10)
			//public int	DID;	//デバイスID(アドレス: 0x36,0x68,0x76,...)
			//public int	CNT_OF_SENS;
			//public SENSOR_TBL[]
			//            SEN;
			//public G.DLG_BOOL_OBJECTS
			//            FUN;
			public int SID;	//sensor id
			public int GID;		//graph id
			public string NAME;	//ACCEL.X, GYRO.X,...TEMP.,HUMIDITY
			public string UNIT;	//[g/s],[deg/s],...,[degC],[%]
			//public int PREC;	//小数点以下桁数
			//public double DATA;	//センサーデータ
			public double SMAX;
			public double SMIN;
		}
#if false
		public class SENSOR_TBL:System.ICloneable
		{
			//public bool HIST_RECT;
			//public int[]	BUS;
			//public int[]	DEV;
			[System.Xml.Serialization.XmlIgnore]
			public DEVICE_TBL
							_DEV;	//センサーデバイス
			public string	NAME;	//ACCEL.X, GYRO.X,...TEMP.,HUMIDITY
			public string	UNIT;	//[g/s],[deg/s],...,[degC],[%]
			public int		G_ID;	//0:NONE, 1:CHART1-LEFT, 2:CHART2:RIGHT,...
			//---
			//---
			public int		PREC;	//小数点以下桁数
			public double	DATA;	//センサーデータ
			//---
			[System.Xml.Serialization.XmlIgnore]
			public object	OBJE;	//チャート・シリーズを格納
			//---
			public Object Clone()
			{
				SYSSET cln = (SYSSET)this.MemberwiseClone();
				return (cln);
			}
			public void clear()
			{
				//for (int i = 0; i < this.HISTVALY.Length; i++) {
				//    this.HISTVALY[i] = 0;
				//}
				//this.HIST_MIN = double.NaN;
				//this.HIST_MAX = double.NaN;
				//this.HIST_AVG = double.NaN;
				//this.CONTRAST = double.NaN;
				//---
				//this.CIR_CNT = 0;
				//this.CIR_S = double.NaN;
				//this.CIR_L = double.NaN;
				//this.CIR_C = double.NaN;
				//this.CIR_P = double.NaN;
				//this.CIR_U = double.NaN;
				//this.CIR_RT = new Rectangle(0, 0, 0, 0);
				////---
				//this.DIA_CNT = 0;
				////---
				//this.EDG_CNT = 0;
			}
		}
#endif
#if false
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct MES_BUF {
			//---
			public ushort DAT_NUMB;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
			public byte[] RESERVED;//
			//---
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
			public byte[] D76_BUFF;//BME280
			//---
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
			public byte[] D29_BUFF;//TCS34725
			//---
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 18)]
			public byte[] D38_BUFF;//RPR0521RS(6*3)
			//---
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
			public byte[] D68_BUFF;//MPU9250
			//---
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
			public byte[] ADC_BUFF;//ADC10bit x 3ch
			//---
		};
#endif
		//---
		static public APPSET AS = new APPSET();
		static public SYSSET SS = new SYSSET();
		static public SENPAR SP = new SENPAR();
		static public SENPAR SB = new SENPAR();
		static public int UIF_LEVL;
		//static public IP_RESULT	IR = new IP_RESULT();
		static public DEVICE_TBL[] DT = null;
		//static public MES_BUF MBUF = new MES_BUF()
		//{
		//    DAT_NUMB = 0xffff,
		//    RESERVED = new byte[10],
		//    D76_BUFF = new byte[8],//BME280
		//    D29_BUFF = new byte[8],//TCS34725
		//    D38_BUFF = new byte[18],//TCS34725
		//    D68_BUFF = new byte[12],//TCS34725
		//    ADC_BUFF = new byte[6]//TCS34725
		//};
		//---	
		static public bool		bDEBUG=false;

		//static public bool bFAST = true;
		static public bool		bCANCEL=false;
		//static public Form01	FORM01 = null;
		static public Form02	FORM02 = null;
		static public int STAT_OF_MEAS = 0;
		static public int INTV_OF_MEAS = 100;//100ms
		static public int YOKO_OF_MEAS = 60;//60s
		static public double[] MES_VAL = new double[22];
		//static public Form03	FORM03 = null;
		//static public Form10	FORM10 = null;
		//static public Form11	FORM11 = null;
		//static public Form12	FORM12 = null;
		//static public int PLM_STS = 0;
		//static public int[] PLM_POS = { 0, 0, 0, 0 };
		//static public byte[] PLM_STS_BIT = new byte[16];
		//static public int CAM_STS;
		//static public CAM_STS CAM_PRC = CAM_STS.STS_NONE;
		//static public int AUT_STS;
		//static public int MOK_STS;
		//static public int CAM_WID;
		//static public int CAM_HEI;
		static public int LED_PWR_STS;
		//static public int CAM_GAI_STS=2;//0:固定, 1:自動, 2:不定
		//static public int CAM_EXP_STS=2;//0:固定, 1:自動, 2:不定
		//static public int CAM_WBL_STS=2;//0:固定, 1:自動, 2:不定
		//static public bool bJITAN=false;
		//static public int CNT_MOD;
		//-----------------------
		static public DialogResult mlog(string str)
		{
			return(mlog(str, Form.ActiveForm));
		}
		//-----------------------
		static public DialogResult mlog(string str, Form frm)
        {
			MessageBoxIcon	icons = MessageBoxIcon.Exclamation;
			MessageBoxButtons
							butns = MessageBoxButtons.OK;
			DialogResult	rc;
			/**/
			 
			if (frm == null) {
				/*if (G.FORM01 != null && G.FORM01.Visible) {
					frm = G.FORM01;
				}
				else*/ if (G.FORM02 != null && G.FORM02.Visible) {
					frm = G.FORM02;
				}
			}
			/**/
			if (str.Length > 0 && str[0]== '#') {
				switch (char.ToLower(str[1])) {
				case 's':
					icons = MessageBoxIcon.Stop;
				break;
				case 'q':
					icons = MessageBoxIcon.Question;
					if (str[1] == 'q') {// small q
						butns = MessageBoxButtons.YesNo;
					}
					else {				// large Q
						butns = MessageBoxButtons.OKCancel;
					}
				break;
				case 'c':
					icons = MessageBoxIcon.Question;
					butns = MessageBoxButtons.YesNoCancel;
				break;
				case 'i':
					icons = MessageBoxIcon.Information;
				break;
				case 'e':
					icons = MessageBoxIcon.Exclamation;
				break;
				default:
				break;
				}
				str = str.Substring(2);
			}
			using (new CenterWinDialog(frm)) {
				rc = MessageBox.Show(G.FORM02, str, Application.ProductName, butns, icons);
			}
			return(rc);
        }
		static public void lerr(string str)
		{
			mlog("internal error %s %d");//(, __FILE__, __LINE__);
		}
#if false
		static public void eep_read(int adr, int len, ref byte[] buf)
		{
			const int busadr = 0x00;
			const int devadr = 0x50;
			int ret, done;
			byte[] outbuf = { 0, 0 };
			outbuf[0] = (byte)(adr >> 8);
			outbuf[1] = (byte)(adr & 0xff);
			D.GET_I2C_WRT(busadr, devadr, 0, 0x80 | 2, outbuf, out ret, out done);

			D.GET_I2C_RED(busadr, devadr, 0, 0x80 | len, out buf, out ret, out done);
		}
		static public void eep_write(int adr, int len, byte[] buf)
		{
			const int busadr = 0x00;
			const int devadr = 0x50;
			int ret, done;
			byte[] outbuf = new byte[2+len];
			outbuf[0] = (byte)(adr >> 8);
			outbuf[1] = (byte)(adr & 0xff);
			for (int i = 0; i < len; i++) {
				outbuf[2 + i] = buf[i];
			}
			D.GET_I2C_WRT(busadr, devadr, 0, 0x80 | (2+len), outbuf, out ret, out done);
			System.Threading.Thread.Sleep(5);
		}
#endif
		static public void eep_get(int idx, int len, out int val)
		{
			byte[] buf;

			D.GET_EEP_VAR(idx, len, out buf);
			switch (len) {
				case 1:val = buf[0]; break;
				case 2:val = D.MAKEWORD(buf[1], buf[0]); break;
				case 4:val = D.MAKELONG(buf[3], buf[2], buf[1], buf[0]); break;
				default: throw new Exception("Internal Error");
			}
		}
		static public void eep_get(int idx, int len, out double val)
		{
			byte[] buf;
			double f;
			D.GET_EEP_VAR(idx, len, out buf);
			f = BitConverter.ToSingle(buf, 0);
			if (f != 0 && f != 1) {
				f = f;
			}
			if (f < 0) {
			f = (int)(f*1000-0.5);
			}
			else {
			f = (int)(f*1000+0.5);
			}
			f/=1000;
			val = f;
		}
		static public void eep_get(int idx, int len, out byte[] val)
		{
			byte[] buf;

			D.GET_EEP_VAR(idx, len, out buf);
			val = buf;
		}

		static public void eep_put(int idx, int len, int val)
		{
			//byte[] buf = new byte[64];
			//buf[0] = (byte)((val & 0xff000000) >> 24);
			//buf[1] = (byte)((val & 0x00ff0000) >> 16);
			//buf[2] = (byte)((val & 0x0000ff00) >> 8);
			//buf[3] = (byte)((val & 0x000000ff) >> 0);

			//eep_write(adr, 4, buf);
			byte[] buf;

			switch (len) {
				case 1:buf = new byte[1] {(byte)val}; break;
				case 2:buf = new byte[2] {(byte)D.B1(val), (byte)D.B2(val)}; break;
				case 4:buf = new byte[4] {(byte)D.B1(val), (byte)D.B2(val),(byte)D.B3(val), (byte)D.B4(val)}; break;
				default: throw new Exception("Internal Error");
			}
			D.SET_EEP_VAR(idx, len, buf);
		}
		static public void eep_put(int idx, int len, double val)
		{
			byte[] buf = BitConverter.GetBytes((float)val);
			D.SET_EEP_VAR(idx, len, buf);
		}
		static public void eep_put(int idx, int len, byte[] val)
		{
			byte[] buf = val;
			D.SET_EEP_VAR(idx, len, buf);
		}
		static public void eep_get_cnt()
		{
			eep_get(129, 2, out G.SP.ROM_DAT_STA);
			eep_get(130, 2, out G.SP.ROM_DAT_END);
			G.SB.ROM_DAT_STA = G.SP.ROM_DAT_STA;
			G.SB.ROM_DAT_END = G.SP.ROM_DAT_END;
		}
		static public void eep_put_clr()
		{
			D.SET_MES_PAR(0, /*STOP*/0);
			G.SP.ROM_DAT_STA = 0;
			G.SP.ROM_DAT_END = 0;
			eep_put(129, 2, G.SP.ROM_DAT_STA);
			eep_put(130, 2, G.SP.ROM_DAT_END);
			G.SB.ROM_DAT_STA = G.SP.ROM_DAT_STA;
			G.SB.ROM_DAT_END = G.SP.ROM_DAT_END;
			D.SET_MES_PAR(0, /*RUN*/1);
		}
		static public void eep_get_all()
		{
			//if ((G.SS.DEVID & 0xffff) == 0xffff) {eep_put_all();}
			eep_get(  0, 1, out G.SP.RESET_FLAG);
			eep_get(  1, 6, out G.SP.DEV_ID    );
			eep_get(  2, 2, out G.SP.PWM_LED_DTY[0]);
			eep_get(  3, 2, out G.SP.PWM_LED_DTY[1]);
			eep_get(  4, 4, out G.SP.ADC_COF_GRD[0]);
			eep_get(  5, 4, out G.SP.ADC_COF_GRD[1]);
			eep_get(  6, 4, out G.SP.ADC_COF_GRD[2]);
			eep_get(  7, 4, out G.SP.ADC_COF_OFS[0]);
			eep_get(  8, 4, out G.SP.ADC_COF_OFS[1]);
			eep_get(  9, 4, out G.SP.ADC_COF_OFS[2]);
			eep_get( 10, 2, out G.SP.D76_F20_HUMI);
			eep_get( 11, 2, out G.SP.D76_F40_MODE);
			eep_get( 12, 2, out G.SP.D76_F42_PRES);
			eep_get( 13, 2, out G.SP.D76_F45_TEMP);
			eep_get( 14, 2, out G.SP.D76_F52_FILT);
			eep_get( 15, 2, out G.SP.D76_F55_STBY);
			eep_get( 16, 2, out G.SP.D29_010_ATIM);
			eep_get( 17, 2, out G.SP.D29_0F0_GAIN);
			eep_get( 18, 2, out G.SP.D68_190_SMRT);
			eep_get( 19, 2, out G.SP.D68_1A0_GLPF);
			eep_get( 20, 2, out G.SP.D68_1B3_GSCL);
			eep_get( 21, 2, out G.SP.D68_1C3_ASCL);
			eep_get( 22, 2, out G.SP.D68_1D0_ALPF);
			eep_get( 23, 2, out G.SP.D38_410_MSTM[0]);
			eep_get( 24, 2, out G.SP.D38_410_MSTM[1]);
			eep_get( 25, 2, out G.SP.D38_410_MSTM[2]);
			eep_get( 26, 2, out G.SP.D38_424_ALG1[0]);
			eep_get( 27, 2, out G.SP.D38_424_ALG1[1]);
			eep_get( 28, 2, out G.SP.D38_424_ALG1[2]);
			eep_get( 29, 2, out G.SP.D38_422_ALG2[0]);
			eep_get( 30, 2, out G.SP.D38_422_ALG2[1]);
			eep_get( 31, 2, out G.SP.D38_422_ALG2[2]);
			eep_get( 32, 2, out G.SP.D38_420_LEDC[0]);
			eep_get( 33, 2, out G.SP.D38_420_LEDC[1]);
			eep_get( 34, 2, out G.SP.D38_420_LEDC[2]);
			eep_get( 35, 2, out G.SP.D38_434_PSG0[0]);
			eep_get( 36, 2, out G.SP.D38_434_PSG0[1]);
			eep_get( 37, 2, out G.SP.D38_434_PSG0[2]);
			eep_get( 38, 4, out G.SP.SEN_COF_GRD[0]);
			eep_get( 39, 4, out G.SP.SEN_COF_GRD[1]);
			eep_get( 40, 4, out G.SP.SEN_COF_GRD[2]);
			eep_get( 41, 4, out G.SP.SEN_COF_GRD[3]);
			eep_get( 42, 4, out G.SP.SEN_COF_GRD[4]);
			eep_get( 43, 4, out G.SP.SEN_COF_GRD[5]);
			eep_get( 44, 4, out G.SP.SEN_COF_GRD[6]);
			eep_get( 45, 4, out G.SP.SEN_COF_GRD[7]);
			eep_get( 46, 4, out G.SP.SEN_COF_GRD[8]);
			eep_get( 47, 4, out G.SP.SEN_COF_GRD[9]);
			eep_get( 48, 4, out G.SP.SEN_COF_GRD[10]);
			eep_get( 49, 4, out G.SP.SEN_COF_GRD[11]);
			eep_get( 50, 4, out G.SP.SEN_COF_GRD[12]);
			eep_get( 51, 4, out G.SP.SEN_COF_GRD[13]);
			eep_get( 52, 4, out G.SP.SEN_COF_GRD[14]);
			eep_get( 53, 4, out G.SP.SEN_COF_GRD[15]);
			eep_get( 54, 4, out G.SP.SEN_COF_GRD[16]);
			eep_get( 55, 4, out G.SP.SEN_COF_GRD[17]);
			eep_get( 56, 4, out G.SP.SEN_COF_GRD[18]);
			eep_get( 57, 4, out G.SP.SEN_COF_GRD[19]);//ADC基準
			eep_get( 58, 4, out G.SP.SEN_COF_GRD[20]);
			eep_get( 59, 4, out G.SP.SEN_COF_GRD[21]);
			eep_get( 60, 4, out G.SP.SEN_COF_OFS[0]);
			eep_get( 61, 4, out G.SP.SEN_COF_OFS[1]);
			eep_get( 62, 4, out G.SP.SEN_COF_OFS[2]);
			eep_get( 63, 4, out G.SP.SEN_COF_OFS[3]);
			eep_get( 64, 4, out G.SP.SEN_COF_OFS[4]);
			eep_get( 65, 4, out G.SP.SEN_COF_OFS[5]);
			eep_get( 66, 4, out G.SP.SEN_COF_OFS[6]);
			eep_get( 67, 4, out G.SP.SEN_COF_OFS[7]);
			eep_get( 68, 4, out G.SP.SEN_COF_OFS[8]);
			eep_get( 69, 4, out G.SP.SEN_COF_OFS[9]);
			eep_get( 70, 4, out G.SP.SEN_COF_OFS[10]);
			eep_get( 71, 4, out G.SP.SEN_COF_OFS[11]);
			eep_get( 72, 4, out G.SP.SEN_COF_OFS[12]);
			eep_get( 73, 4, out G.SP.SEN_COF_OFS[13]);
			eep_get( 74, 4, out G.SP.SEN_COF_OFS[14]);
			eep_get( 75, 4, out G.SP.SEN_COF_OFS[15]);
			eep_get( 76, 4, out G.SP.SEN_COF_OFS[16]);
			eep_get( 77, 4, out G.SP.SEN_COF_OFS[17]);
			eep_get( 78, 4, out G.SP.SEN_COF_OFS[18]);
			eep_get( 79, 4, out G.SP.SEN_COF_OFS[19]);//ADC基準
			eep_get( 80, 4, out G.SP.SEN_COF_OFS[20]);
			eep_get( 81, 4, out G.SP.SEN_COF_OFS[21]);
			//---
			eep_get( 82, 2, out G.SP.LED_LGT_CHK    );
			eep_get( 83, 2, out G.SP.LED_LGT_OPT    );
			eep_get( 84, 2, out G.SP.LED_LGT_SEN[0] );
			eep_get( 85, 2, out G.SP.LED_LGT_SEN[1] );
			eep_get( 86, 2, out G.SP.LED_LGT_SEN[2] );
			eep_get( 87, 2, out G.SP.LED_LGT_VAL[0] );
			eep_get( 88, 2, out G.SP.LED_LGT_VAL[1] );
			eep_get( 89, 2, out G.SP.LED_LGT_VAL[2] );
			eep_get( 90, 2, out G.SP.LED_LGT_CND[0] );
			eep_get( 91, 2, out G.SP.LED_LGT_CND[1] );
			eep_get( 92, 2, out G.SP.LED_LGT_CND[2] );
			eep_get( 93, 2, out G.SP.LED_OFF_CHK    );
			eep_get( 94, 2, out G.SP.LED_OFF_OPT    );
			eep_get( 95, 2, out G.SP.LED_OFF_SEN[0] );
			eep_get( 96, 2, out G.SP.LED_OFF_SEN[1] );
			eep_get( 97, 2, out G.SP.LED_OFF_SEN[2] );
			eep_get( 98, 2, out G.SP.LED_OFF_VAL[0] );
			eep_get( 99, 2, out G.SP.LED_OFF_VAL[1] );
			eep_get(100, 2, out G.SP.LED_OFF_VAL[2] );
			eep_get(101, 2, out G.SP.LED_OFF_CND[0] );
			eep_get(102, 2, out G.SP.LED_OFF_CND[1] );
			eep_get(103, 2, out G.SP.LED_OFF_CND[2] );
			//---
			eep_get(104, 2, out G.SP.LCD_FRE_TOP    );
			eep_get(105, 2, out G.SP.LCD_FRE_BTM    );
			eep_get(106,10, out G.SP.LCD_FRE_STP    );
			eep_get(107,10, out G.SP.LCD_FRE_SBT    );
			eep_get(108, 2, out G.SP.LCD_LCK_TOP    );
			eep_get(109, 2, out G.SP.LCD_LCK_BTM    );
			eep_get(110,10, out G.SP.LCD_LCK_STP    );
			eep_get(111,10, out G.SP.LCD_LCK_SBT    );
			eep_get(112, 2, out G.SP.LCD_MES_TOP    );
			eep_get(113, 2, out G.SP.LCD_MES_BTM    );
			eep_get(114,10, out G.SP.LCD_MES_STP    );
			eep_get(115,10, out G.SP.LCD_MES_SBT    );
			//---
			eep_get(116, 4, out G.SP.BAT_WRN_VOL    );
			eep_get(117, 4, out G.SP.BAT_HLT_VOL    );
			//---
			eep_get(118, 2, out G.SP.MES_USE_SEN    );
			eep_get(119, 4, out G.SP.MES_FRE_PRS    );
			eep_get(120, 4, out G.SP.MES_LCK_PRS    );
			eep_get(121, 4, out G.SP.MES_MES_PRS    );
			eep_get(122, 2, out G.SP.MES_LCK_TOT    );
			eep_get(123, 2, out G.SP.MES_MES_CND    );
			eep_get(124, 4, out G.SP.MES_MES_VRD    );
			eep_get(125, 4, out G.SP.MES_MES_VRC    );
			eep_get(126, 4, out G.SP.MES_MES_VRB    );
			eep_get(127, 2, out G.SP.MES_MES_TOT    );
			eep_get(128, 2, out G.SP.MES_MES_WAI    );
			eep_get(129, 2, out G.SP.ROM_DAT_STA    );
			eep_get(130, 2, out G.SP.ROM_DAT_END    );
			//---
/*			eep_get(122, 2, out G.SP.MES_CHK[8]     );
			eep_get(123, 2, out G.SP.MES_CHK[8]     );
			eep_get(124, 2, out G.SP.MES_CHK[8]     );
			eep_get(125, 2, out G.SP.MES_CHK[8]     );
			eep_get(126, 2, out G.SP.MES_CHK[8]     );
			eep_get(127, 2, out G.SP.MES_CHK[8]     );
			eep_get(128, 2, out G.SP.MES_CHK[8]     );
			eep_get(129, 2, out G.SP.MES_CHK[8]     );
			eep_get(130, 2, out G.SP.MES_CHK[8]     );
			eep_get(131, 2, out G.SP.MES_CHK[9]     );
			eep_get(132, 4, out G.SP.MES_VAL[0]     );
			eep_get(133, 4, out G.SP.MES_VAL[1]     );
			eep_get(134, 4, out G.SP.MES_VAL[2]     );
			eep_get(135, 4, out G.SP.MES_VAL[3]     );
			eep_get(136, 4, out G.SP.MES_VAL[4]     );
			eep_get(137, 4, out G.SP.MES_VAL[5]     );
			eep_get(138, 4, out G.SP.MES_VAL[6]     );
			eep_get(139, 4, out G.SP.MES_VAL[7]     );
			eep_get(140, 4, out G.SP.MES_VAL[8]     );
			eep_get(141, 4, out G.SP.MES_VAL[9]     );*/
			//---
			D.SET_MES_PAR(0, /*RUN*/1);
			G.SB = (SENPAR)G.SP.Clone();
		}
		static public bool eep_put_all(bool bOnlyChanged=true)
		{
			List<int> l_flg = new List<int>();
			if (!G.SP.DEV_ID.SequenceEqual(G.SB.DEV_ID)     ) {l_flg.Add( 1);}
			if (G.SP.PWM_LED_DTY[0]  != G.SB.PWM_LED_DTY[0] ) {l_flg.Add( 2);}
			if (G.SP.PWM_LED_DTY[1]  != G.SB.PWM_LED_DTY[1] ) {l_flg.Add( 3);}
			if (G.SP.ADC_COF_GRD[0]  != G.SB.ADC_COF_GRD[0] ) {l_flg.Add( 4);}
			if (G.SP.ADC_COF_GRD[1]  != G.SB.ADC_COF_GRD[1] ) {l_flg.Add( 5);}
			if (G.SP.ADC_COF_GRD[2]  != G.SB.ADC_COF_GRD[2] ) {l_flg.Add( 6);}
			if (G.SP.ADC_COF_OFS[0]  != G.SB.ADC_COF_OFS[0] ) {l_flg.Add( 7);}
			if (G.SP.ADC_COF_OFS[1]  != G.SB.ADC_COF_OFS[1] ) {l_flg.Add( 8);}
			if (G.SP.ADC_COF_OFS[2]  != G.SB.ADC_COF_OFS[2] ) {l_flg.Add( 9);}
			if (G.SP.D76_F20_HUMI    != G.SB.D76_F20_HUMI   ) {l_flg.Add(10);}
			if (G.SP.D76_F40_MODE    != G.SB.D76_F40_MODE   ) {l_flg.Add(11);}
			if (G.SP.D76_F42_PRES    != G.SB.D76_F42_PRES   ) {l_flg.Add(12);}
			if (G.SP.D76_F45_TEMP    != G.SB.D76_F45_TEMP   ) {l_flg.Add(13);}
			if (G.SP.D76_F52_FILT    != G.SB.D76_F52_FILT   ) {l_flg.Add(14);}
			if (G.SP.D76_F55_STBY    != G.SB.D76_F55_STBY   ) {l_flg.Add(15);}
			if (G.SP.D29_010_ATIM    != G.SB.D29_010_ATIM   ) {l_flg.Add(16);}
			if (G.SP.D29_0F0_GAIN    != G.SB.D29_0F0_GAIN   ) {l_flg.Add(17);}
			if (G.SP.D68_190_SMRT    != G.SB.D68_190_SMRT   ) {l_flg.Add(18);}
			if (G.SP.D68_1A0_GLPF    != G.SB.D68_1A0_GLPF   ) {l_flg.Add(19);}
			if (G.SP.D68_1B3_GSCL    != G.SB.D68_1B3_GSCL   ) {l_flg.Add(20);}
			if (G.SP.D68_1C3_ASCL    != G.SB.D68_1C3_ASCL   ) {l_flg.Add(21);}
			if (G.SP.D68_1D0_ALPF    != G.SB.D68_1D0_ALPF   ) {l_flg.Add(22);}
			if (G.SP.D38_410_MSTM[0] != G.SB.D38_410_MSTM[0]) {l_flg.Add(23);}
			if (G.SP.D38_410_MSTM[1] != G.SB.D38_410_MSTM[1]) {l_flg.Add(24);}
			if (G.SP.D38_410_MSTM[2] != G.SB.D38_410_MSTM[2]) {l_flg.Add(25);}
			if (G.SP.D38_424_ALG1[0] != G.SB.D38_424_ALG1[0]) {l_flg.Add(26);}
			if (G.SP.D38_424_ALG1[1] != G.SB.D38_424_ALG1[1]) {l_flg.Add(27);}
			if (G.SP.D38_424_ALG1[2] != G.SB.D38_424_ALG1[2]) {l_flg.Add(28);}
			if (G.SP.D38_422_ALG2[0] != G.SB.D38_422_ALG2[0]) {l_flg.Add(29);}
			if (G.SP.D38_422_ALG2[1] != G.SB.D38_422_ALG2[1]) {l_flg.Add(30);}
			if (G.SP.D38_422_ALG2[2] != G.SB.D38_422_ALG2[2]) {l_flg.Add(31);}
			if (G.SP.D38_420_LEDC[0] != G.SB.D38_420_LEDC[0]) {l_flg.Add(32);}
			if (G.SP.D38_420_LEDC[1] != G.SB.D38_420_LEDC[1]) {l_flg.Add(33);}
			if (G.SP.D38_420_LEDC[2] != G.SB.D38_420_LEDC[2]) {l_flg.Add(34);}
			if (G.SP.D38_434_PSG0[0] != G.SB.D38_434_PSG0[0]) {l_flg.Add(35);}
			if (G.SP.D38_434_PSG0[1] != G.SB.D38_434_PSG0[1]) {l_flg.Add(36);}
			if (G.SP.D38_434_PSG0[2] != G.SB.D38_434_PSG0[2]) {l_flg.Add(37);}
			if (G.SP.SEN_COF_GRD[0]  != G.SB.SEN_COF_GRD[0] ) {l_flg.Add(38);}
			if (G.SP.SEN_COF_GRD[1]  != G.SB.SEN_COF_GRD[1] ) {l_flg.Add(39);}
			if (G.SP.SEN_COF_GRD[2]  != G.SB.SEN_COF_GRD[2] ) {l_flg.Add(40);}
			if (G.SP.SEN_COF_GRD[3]  != G.SB.SEN_COF_GRD[3] ) {l_flg.Add(41);}
			if (G.SP.SEN_COF_GRD[4]  != G.SB.SEN_COF_GRD[4] ) {l_flg.Add(42);}
			if (G.SP.SEN_COF_GRD[5]  != G.SB.SEN_COF_GRD[5] ) {l_flg.Add(43);}
			if (G.SP.SEN_COF_GRD[6]  != G.SB.SEN_COF_GRD[6] ) {l_flg.Add(44);}
			if (G.SP.SEN_COF_GRD[7]  != G.SB.SEN_COF_GRD[7] ) {l_flg.Add(45);}
			if (G.SP.SEN_COF_GRD[8]  != G.SB.SEN_COF_GRD[8] ) {l_flg.Add(46);}
			if (G.SP.SEN_COF_GRD[9]  != G.SB.SEN_COF_GRD[9] ) {l_flg.Add(47);}
			if (G.SP.SEN_COF_GRD[10] != G.SB.SEN_COF_GRD[10]) {l_flg.Add(48);}
			if (G.SP.SEN_COF_GRD[11] != G.SB.SEN_COF_GRD[11]) {l_flg.Add(49);}
			if (G.SP.SEN_COF_GRD[12] != G.SB.SEN_COF_GRD[12]) {l_flg.Add(50);}
			if (G.SP.SEN_COF_GRD[13] != G.SB.SEN_COF_GRD[13]) {l_flg.Add(51);}
			if (G.SP.SEN_COF_GRD[14] != G.SB.SEN_COF_GRD[14]) {l_flg.Add(52);}
			if (G.SP.SEN_COF_GRD[15] != G.SB.SEN_COF_GRD[15]) {l_flg.Add(53);}
			if (G.SP.SEN_COF_GRD[16] != G.SB.SEN_COF_GRD[16]) {l_flg.Add(54);}
			if (G.SP.SEN_COF_GRD[17] != G.SB.SEN_COF_GRD[17]) {l_flg.Add(55);}
			if (G.SP.SEN_COF_GRD[18] != G.SB.SEN_COF_GRD[18]) {l_flg.Add(56);}
			if (G.SP.SEN_COF_GRD[19] != G.SB.SEN_COF_GRD[19]) {l_flg.Add(57);}//ADC基準
			if (G.SP.SEN_COF_GRD[20] != G.SB.SEN_COF_GRD[20]) {l_flg.Add(58);}
			if (G.SP.SEN_COF_GRD[21] != G.SB.SEN_COF_GRD[21]) {l_flg.Add(59);}
			if (G.SP.SEN_COF_OFS[0]  != G.SB.SEN_COF_OFS[0] ) {l_flg.Add(60);}
			if (G.SP.SEN_COF_OFS[1]  != G.SB.SEN_COF_OFS[1] ) {l_flg.Add(61);}
			if (G.SP.SEN_COF_OFS[2]  != G.SB.SEN_COF_OFS[2] ) {l_flg.Add(62);}
			if (G.SP.SEN_COF_OFS[3]  != G.SB.SEN_COF_OFS[3] ) {l_flg.Add(63);}
			if (G.SP.SEN_COF_OFS[4]  != G.SB.SEN_COF_OFS[4] ) {l_flg.Add(64);}
			if (G.SP.SEN_COF_OFS[5]  != G.SB.SEN_COF_OFS[5] ) {l_flg.Add(65);}
			if (G.SP.SEN_COF_OFS[6]  != G.SB.SEN_COF_OFS[6] ) {l_flg.Add(66);}
			if (G.SP.SEN_COF_OFS[7]  != G.SB.SEN_COF_OFS[7] ) {l_flg.Add(67);}
			if (G.SP.SEN_COF_OFS[8]  != G.SB.SEN_COF_OFS[8] ) {l_flg.Add(68);}
			if (G.SP.SEN_COF_OFS[9]  != G.SB.SEN_COF_OFS[9] ) {l_flg.Add(69);}
			if (G.SP.SEN_COF_OFS[10] != G.SB.SEN_COF_OFS[10]) {l_flg.Add(70);}
			if (G.SP.SEN_COF_OFS[11] != G.SB.SEN_COF_OFS[11]) {l_flg.Add(71);}
			if (G.SP.SEN_COF_OFS[12] != G.SB.SEN_COF_OFS[12]) {l_flg.Add(72);}
			if (G.SP.SEN_COF_OFS[13] != G.SB.SEN_COF_OFS[13]) {l_flg.Add(73);}
			if (G.SP.SEN_COF_OFS[14] != G.SB.SEN_COF_OFS[14]) {l_flg.Add(74);}
			if (G.SP.SEN_COF_OFS[15] != G.SB.SEN_COF_OFS[15]) {l_flg.Add(75);}
			if (G.SP.SEN_COF_OFS[16] != G.SB.SEN_COF_OFS[16]) {l_flg.Add(76);}
			if (G.SP.SEN_COF_OFS[17] != G.SB.SEN_COF_OFS[17]) {l_flg.Add(77);}
			if (G.SP.SEN_COF_OFS[18] != G.SB.SEN_COF_OFS[18]) {l_flg.Add(78);}
			if (G.SP.SEN_COF_OFS[19] != G.SB.SEN_COF_OFS[19]) {l_flg.Add(79);}//ADC基準
			if (G.SP.SEN_COF_OFS[20] != G.SB.SEN_COF_OFS[20]) {l_flg.Add(80);}
			if (G.SP.SEN_COF_OFS[21] != G.SB.SEN_COF_OFS[21]) {l_flg.Add(81);}
			//---
			if (G.SP.LED_LGT_CHK     != G.SB.LED_LGT_CHK    ) {l_flg.Add( 82);}
			if (G.SP.LED_LGT_OPT     != G.SB.LED_LGT_OPT    ) {l_flg.Add( 83);}
			if (G.SP.LED_LGT_SEN[0]  != G.SB.LED_LGT_SEN[0] ) {l_flg.Add( 84);}
			if (G.SP.LED_LGT_SEN[1]  != G.SB.LED_LGT_SEN[1] ) {l_flg.Add( 85);}
			if (G.SP.LED_LGT_SEN[2]  != G.SB.LED_LGT_SEN[2] ) {l_flg.Add( 86);}
			if (G.SP.LED_LGT_VAL[0]  != G.SB.LED_LGT_VAL[0] ) {l_flg.Add( 87);}
			if (G.SP.LED_LGT_VAL[1]  != G.SB.LED_LGT_VAL[1] ) {l_flg.Add( 88);}
			if (G.SP.LED_LGT_VAL[2]  != G.SB.LED_LGT_VAL[2] ) {l_flg.Add( 89);}
			if (G.SP.LED_LGT_CND[0]  != G.SB.LED_LGT_CND[0] ) {l_flg.Add( 90);}
			if (G.SP.LED_LGT_CND[1]  != G.SB.LED_LGT_CND[1] ) {l_flg.Add( 91);}
			if (G.SP.LED_LGT_CND[2]  != G.SB.LED_LGT_CND[2] ) {l_flg.Add( 92);}
			if (G.SP.LED_OFF_CHK     != G.SB.LED_OFF_CHK    ) {l_flg.Add( 93);}
			if (G.SP.LED_OFF_OPT     != G.SB.LED_OFF_OPT    ) {l_flg.Add( 94);}
			if (G.SP.LED_OFF_SEN[0]  != G.SB.LED_OFF_SEN[0] ) {l_flg.Add( 95);}
			if (G.SP.LED_OFF_SEN[1]  != G.SB.LED_OFF_SEN[1] ) {l_flg.Add( 96);}
			if (G.SP.LED_OFF_SEN[2]  != G.SB.LED_OFF_SEN[2] ) {l_flg.Add( 97);}
			if (G.SP.LED_OFF_VAL[0]  != G.SB.LED_OFF_VAL[0] ) {l_flg.Add( 98);}
			if (G.SP.LED_OFF_VAL[1]  != G.SB.LED_OFF_VAL[1] ) {l_flg.Add( 99);}
			if (G.SP.LED_OFF_VAL[2]  != G.SB.LED_OFF_VAL[2] ) {l_flg.Add(100);}
			if (G.SP.LED_OFF_CND[0]  != G.SB.LED_OFF_CND[0] ) {l_flg.Add(101);}
			if (G.SP.LED_OFF_CND[1]  != G.SB.LED_OFF_CND[1] ) {l_flg.Add(102);}
			if (G.SP.LED_OFF_CND[2]  != G.SB.LED_OFF_CND[2] ) {l_flg.Add(103);}
			if (G.SP.LCD_FRE_TOP     != G.SB.LCD_FRE_TOP    ) {l_flg.Add(104);}
			if (G.SP.LCD_FRE_BTM     != G.SB.LCD_FRE_BTM    ) {l_flg.Add(105);}
			if (!G.SP.LCD_FRE_STP.SequenceEqual(G.SB.LCD_FRE_STP)) {l_flg.Add(106);}
			if (!G.SP.LCD_FRE_SBT.SequenceEqual(G.SB.LCD_FRE_SBT)) {l_flg.Add(107);}
			if (G.SP.LCD_LCK_TOP     != G.SB.LCD_LCK_TOP    ) {l_flg.Add(108);}
			if (G.SP.LCD_LCK_BTM     != G.SB.LCD_LCK_BTM    ) {l_flg.Add(109);}
			if (!G.SP.LCD_LCK_STP.SequenceEqual(G.SB.LCD_LCK_STP)) {l_flg.Add(110);}
			if (!G.SP.LCD_LCK_SBT.SequenceEqual(G.SB.LCD_LCK_SBT)) {l_flg.Add(111);}
			if (G.SP.LCD_MES_TOP     != G.SB.LCD_MES_TOP    ) {l_flg.Add(112);}
			if (G.SP.LCD_MES_BTM     != G.SB.LCD_MES_BTM    ) {l_flg.Add(113);}
			if (!G.SP.LCD_MES_STP.SequenceEqual(G.SB.LCD_MES_STP)) {l_flg.Add(114);}
			if (!G.SP.LCD_MES_SBT.SequenceEqual(G.SB.LCD_MES_SBT)) {l_flg.Add(115);}
			if (G.SP.BAT_WRN_VOL     != G.SB.BAT_WRN_VOL    ) {l_flg.Add(116);}
			if (G.SP.BAT_HLT_VOL     != G.SB.BAT_HLT_VOL    ) {l_flg.Add(117);}
			if (G.SP.MES_USE_SEN     != G.SB.MES_USE_SEN    ) {l_flg.Add(118);}
			if (G.SP.MES_FRE_PRS     != G.SB.MES_FRE_PRS    ) {l_flg.Add(119);}
			if (G.SP.MES_LCK_PRS     != G.SB.MES_LCK_PRS    ) {l_flg.Add(120);}
			if (G.SP.MES_MES_PRS     != G.SB.MES_MES_PRS    ) {l_flg.Add(121);}
			if (G.SP.MES_LCK_TOT     != G.SB.MES_LCK_TOT    ) {l_flg.Add(122);}
			if (G.SP.MES_MES_CND     != G.SB.MES_MES_CND    ) {l_flg.Add(123);}
			if (G.SP.MES_MES_VRD     != G.SB.MES_MES_VRD    ) {l_flg.Add(124);}
			if (G.SP.MES_MES_VRC     != G.SB.MES_MES_VRC    ) {l_flg.Add(125);}
			if (G.SP.MES_MES_VRB     != G.SB.MES_MES_VRB    ) {l_flg.Add(126);}
			if (G.SP.MES_MES_TOT     != G.SB.MES_MES_TOT    ) {l_flg.Add(127);}
			if (G.SP.MES_MES_WAI     != G.SB.MES_MES_WAI    ) {l_flg.Add(128);}
			if (G.SP.ROM_DAT_STA     != G.SB.ROM_DAT_STA    ) {l_flg.Add(129);}
			if (G.SP.ROM_DAT_END     != G.SB.ROM_DAT_END    ) {l_flg.Add(130);}
#if false
			if (G.SP.MES_CHK[0]      != G.SB.MES_CHK[0]     ) {l_flg.Add(84);}
			if (G.SP.MES_CHK[1]      != G.SB.MES_CHK[1]     ) {l_flg.Add(85);}
			if (G.SP.MES_CHK[2]      != G.SB.MES_CHK[2]     ) {l_flg.Add(86);}
			if (G.SP.MES_CHK[3]      != G.SB.MES_CHK[3]     ) {l_flg.Add(87);}
			if (G.SP.MES_CHK[4]      != G.SB.MES_CHK[4]     ) {l_flg.Add(88);}
			if (G.SP.MES_CHK[5]      != G.SB.MES_CHK[5]     ) {l_flg.Add(89);}
			if (G.SP.MES_CHK[6]      != G.SB.MES_CHK[6]     ) {l_flg.Add(90);}
			if (G.SP.MES_CHK[7]      != G.SB.MES_CHK[7]     ) {l_flg.Add(91);}
			if (G.SP.MES_CHK[8]      != G.SB.MES_CHK[8]     ) {l_flg.Add(92);}
			if (G.SP.MES_CHK[9]      != G.SB.MES_CHK[9]     ) {l_flg.Add(93);}
			if (G.SP.MES_VAL[0]      != G.SB.MES_VAL[0]     ) {l_flg.Add(94);}
			if (G.SP.MES_VAL[1]      != G.SB.MES_VAL[1]     ) {l_flg.Add(95);}
			if (G.SP.MES_VAL[2]      != G.SB.MES_VAL[2]     ) {l_flg.Add(96);}
			if (G.SP.MES_VAL[3]      != G.SB.MES_VAL[3]     ) {l_flg.Add(97);}
			if (G.SP.MES_VAL[4]      != G.SB.MES_VAL[4]     ) {l_flg.Add(98);}
			if (G.SP.MES_VAL[5]      != G.SB.MES_VAL[5]     ) {l_flg.Add(99);}
			if (G.SP.MES_VAL[6]      != G.SB.MES_VAL[6]     ) {l_flg.Add(100);}
			if (G.SP.MES_VAL[7]      != G.SB.MES_VAL[7]     ) {l_flg.Add(101);}
			if (G.SP.MES_VAL[8]      != G.SB.MES_VAL[8]     ) {l_flg.Add(102);}
			if (G.SP.MES_VAL[9]      != G.SB.MES_VAL[9]     ) {l_flg.Add(103);}
#endif
			//---
			if (l_flg.Count <= 0) {
				return(false);
			}
			D.SET_MES_PAR(0, /*STOP*/0);
			//goto skip;
			//---
			for (int i = 0; i < l_flg.Count; i++) {
				int idx = l_flg[i];
				switch (idx) {
				case   1: eep_put(idx, 6, G.SP.DEV_ID         ); break;
				case   2: eep_put(idx, 2, G.SP.PWM_LED_DTY[0] ); break;
				case   3: eep_put(idx, 2, G.SP.PWM_LED_DTY[1] ); break;
				case   4: eep_put(idx, 4, G.SP.ADC_COF_GRD[0] ); break;
				case   5: eep_put(idx, 4, G.SP.ADC_COF_GRD[1] ); break;
				case   6: eep_put(idx, 4, G.SP.ADC_COF_GRD[2] ); break;
				case   7: eep_put(idx, 4, G.SP.ADC_COF_OFS[0] ); break;
				case   8: eep_put(idx, 4, G.SP.ADC_COF_OFS[1] ); break;
				case   9: eep_put(idx, 4, G.SP.ADC_COF_OFS[2] ); break;
				case  10: eep_put(idx, 2, G.SP.D76_F20_HUMI   ); break;
				case  11: eep_put(idx, 2, G.SP.D76_F40_MODE   ); break;
				case  12: eep_put(idx, 2, G.SP.D76_F42_PRES   ); break;
				case  13: eep_put(idx, 2, G.SP.D76_F45_TEMP   ); break;
				case  14: eep_put(idx, 2, G.SP.D76_F52_FILT   ); break;
				case  15: eep_put(idx, 2, G.SP.D76_F55_STBY   ); break;
				case  16: eep_put(idx, 2, G.SP.D29_010_ATIM   ); break;
				case  17: eep_put(idx, 2, G.SP.D29_0F0_GAIN   ); break;
				case  18: eep_put(idx, 2, G.SP.D68_190_SMRT   ); break;
				case  19: eep_put(idx, 2, G.SP.D68_1A0_GLPF   ); break;
				case  20: eep_put(idx, 2, G.SP.D68_1B3_GSCL   ); break;
				case  21: eep_put(idx, 2, G.SP.D68_1C3_ASCL   ); break;
				case  22: eep_put(idx, 2, G.SP.D68_1D0_ALPF   ); break;
				case  23: eep_put(idx, 2, G.SP.D38_410_MSTM[0]); break;
				case  24: eep_put(idx, 2, G.SP.D38_410_MSTM[1]); break;
				case  25: eep_put(idx, 2, G.SP.D38_410_MSTM[2]); break;
				case  26: eep_put(idx, 2, G.SP.D38_424_ALG1[0]); break;
				case  27: eep_put(idx, 2, G.SP.D38_424_ALG1[1]); break;
				case  28: eep_put(idx, 2, G.SP.D38_424_ALG1[2]); break;
				case  29: eep_put(idx, 2, G.SP.D38_422_ALG2[0]); break;
				case  30: eep_put(idx, 2, G.SP.D38_422_ALG2[1]); break;
				case  31: eep_put(idx, 2, G.SP.D38_422_ALG2[2]); break;
				case  32: eep_put(idx, 2, G.SP.D38_420_LEDC[0]); break;
				case  33: eep_put(idx, 2, G.SP.D38_420_LEDC[1]); break;
				case  34: eep_put(idx, 2, G.SP.D38_420_LEDC[2]); break;
				case  35: eep_put(idx, 2, G.SP.D38_434_PSG0[0]); break;
				case  36: eep_put(idx, 2, G.SP.D38_434_PSG0[1]); break;
				case  37: eep_put(idx, 2, G.SP.D38_434_PSG0[2]); break;
				case  38: eep_put(idx, 4, G.SP.SEN_COF_GRD[0] ); break;
				case  39: eep_put(idx, 4, G.SP.SEN_COF_GRD[1] ); break;
				case  40: eep_put(idx, 4, G.SP.SEN_COF_GRD[2] ); break;
				case  41: eep_put(idx, 4, G.SP.SEN_COF_GRD[3] ); break;
				case  42: eep_put(idx, 4, G.SP.SEN_COF_GRD[4] ); break;
				case  43: eep_put(idx, 4, G.SP.SEN_COF_GRD[5] ); break;
				case  44: eep_put(idx, 4, G.SP.SEN_COF_GRD[6] ); break;
				case  45: eep_put(idx, 4, G.SP.SEN_COF_GRD[7] ); break;
				case  46: eep_put(idx, 4, G.SP.SEN_COF_GRD[8] ); break;
				case  47: eep_put(idx, 4, G.SP.SEN_COF_GRD[9] ); break;
				case  48: eep_put(idx, 4, G.SP.SEN_COF_GRD[10]); break;
				case  49: eep_put(idx, 4, G.SP.SEN_COF_GRD[11]); break;
				case  50: eep_put(idx, 4, G.SP.SEN_COF_GRD[12]); break;
				case  51: eep_put(idx, 4, G.SP.SEN_COF_GRD[13]); break;
				case  52: eep_put(idx, 4, G.SP.SEN_COF_GRD[14]); break;
				case  53: eep_put(idx, 4, G.SP.SEN_COF_GRD[15]); break;
				case  54: eep_put(idx, 4, G.SP.SEN_COF_GRD[16]); break;
				case  55: eep_put(idx, 4, G.SP.SEN_COF_GRD[17]); break;
				case  56: eep_put(idx, 4, G.SP.SEN_COF_GRD[18]); break;
				case  57: eep_put(idx, 4, G.SP.SEN_COF_GRD[19]); break;//ADC基準
				case  58: eep_put(idx, 4, G.SP.SEN_COF_GRD[20]); break;
				case  59: eep_put(idx, 4, G.SP.SEN_COF_GRD[21]); break;
				case  60: eep_put(idx, 4, G.SP.SEN_COF_OFS[0] ); break;
				case  61: eep_put(idx, 4, G.SP.SEN_COF_OFS[1] ); break;
				case  62: eep_put(idx, 4, G.SP.SEN_COF_OFS[2] ); break;
				case  63: eep_put(idx, 4, G.SP.SEN_COF_OFS[3] ); break;
				case  64: eep_put(idx, 4, G.SP.SEN_COF_OFS[4] ); break;
				case  65: eep_put(idx, 4, G.SP.SEN_COF_OFS[5] ); break;
				case  66: eep_put(idx, 4, G.SP.SEN_COF_OFS[6] ); break;
				case  67: eep_put(idx, 4, G.SP.SEN_COF_OFS[7] ); break;
				case  68: eep_put(idx, 4, G.SP.SEN_COF_OFS[8] ); break;
				case  69: eep_put(idx, 4, G.SP.SEN_COF_OFS[9] ); break;
				case  70: eep_put(idx, 4, G.SP.SEN_COF_OFS[10]); break;
				case  71: eep_put(idx, 4, G.SP.SEN_COF_OFS[11]); break;
				case  72: eep_put(idx, 4, G.SP.SEN_COF_OFS[12]); break;
				case  73: eep_put(idx, 4, G.SP.SEN_COF_OFS[13]); break;
				case  74: eep_put(idx, 4, G.SP.SEN_COF_OFS[14]); break;
				case  75: eep_put(idx, 4, G.SP.SEN_COF_OFS[15]); break;
				case  76: eep_put(idx, 4, G.SP.SEN_COF_OFS[16]); break;
				case  77: eep_put(idx, 4, G.SP.SEN_COF_OFS[17]); break;
				case  78: eep_put(idx, 4, G.SP.SEN_COF_OFS[18]); break;
				case  79: eep_put(idx, 4, G.SP.SEN_COF_OFS[19]); break;//ADC基準
				case  80: eep_put(idx, 4, G.SP.SEN_COF_OFS[20]); break;
				case  81: eep_put(idx, 4, G.SP.SEN_COF_OFS[21]); break;
				//---
				case  82: eep_put(idx, 2, G.SP.LED_LGT_CHK    ); break;
				case  83: eep_put(idx, 2, G.SP.LED_LGT_OPT    ); break;
				case  84: eep_put(idx, 2, G.SP.LED_LGT_SEN[0] ); break;
				case  85: eep_put(idx, 2, G.SP.LED_LGT_SEN[1] ); break;
				case  86: eep_put(idx, 2, G.SP.LED_LGT_SEN[2] ); break;
				case  87: eep_put(idx, 2, G.SP.LED_LGT_VAL[0] ); break;
				case  88: eep_put(idx, 2, G.SP.LED_LGT_VAL[1] ); break;
				case  89: eep_put(idx, 2, G.SP.LED_LGT_VAL[2] ); break;
				case  90: eep_put(idx, 2, G.SP.LED_LGT_CND[0] ); break;
				case  91: eep_put(idx, 2, G.SP.LED_LGT_CND[1] ); break;
				case  92: eep_put(idx, 2, G.SP.LED_LGT_CND[2] ); break;
				case  93: eep_put(idx, 2, G.SP.LED_OFF_CHK    ); break;
				case  94: eep_put(idx, 2, G.SP.LED_OFF_OPT    ); break;
				case  95: eep_put(idx, 2, G.SP.LED_OFF_SEN[0] ); break;
				case  96: eep_put(idx, 2, G.SP.LED_OFF_SEN[1] ); break;
				case  97: eep_put(idx, 2, G.SP.LED_OFF_SEN[2] ); break;
				case  98: eep_put(idx, 2, G.SP.LED_OFF_VAL[0] ); break;
				case  99: eep_put(idx, 2, G.SP.LED_OFF_VAL[1] ); break;
				case 100: eep_put(idx, 2, G.SP.LED_OFF_VAL[2] ); break;
				case 101: eep_put(idx, 2, G.SP.LED_OFF_CND[0] ); break;
				case 102: eep_put(idx, 2, G.SP.LED_OFF_CND[1] ); break;
				case 103: eep_put(idx, 2, G.SP.LED_OFF_CND[2] ); break;
				//---
				case 104: eep_put(idx, 2, G.SP.LCD_FRE_TOP    ); break;
				case 105: eep_put(idx, 2, G.SP.LCD_FRE_BTM    ); break;
				case 106: eep_put(idx,10, G.SP.LCD_FRE_STP    ); break;
				case 107: eep_put(idx,10, G.SP.LCD_FRE_SBT    ); break;
				case 108: eep_put(idx, 2, G.SP.LCD_LCK_TOP    ); break;
				case 109: eep_put(idx, 2, G.SP.LCD_LCK_BTM    ); break;
				case 110: eep_put(idx,10, G.SP.LCD_LCK_STP    ); break;
				case 111: eep_put(idx,10, G.SP.LCD_LCK_SBT    ); break;
				case 112: eep_put(idx, 2, G.SP.LCD_MES_TOP    ); break;
				case 113: eep_put(idx, 2, G.SP.LCD_MES_BTM    ); break;
				case 114: eep_put(idx,10, G.SP.LCD_MES_STP    ); break;
				case 115: eep_put(idx,10, G.SP.LCD_MES_SBT    ); break;
				//---
				case 116: eep_put(idx, 4, G.SP.BAT_WRN_VOL    ); break;
				case 117: eep_put(idx, 4, G.SP.BAT_HLT_VOL    ); break;
				//---
				case 118: eep_put(idx, 2, G.SP.MES_USE_SEN    ); break;
				case 119: eep_put(idx, 4, G.SP.MES_FRE_PRS    ); break;
				case 120: eep_put(idx, 4, G.SP.MES_LCK_PRS    ); break;
				case 121: eep_put(idx, 4, G.SP.MES_MES_PRS    ); break;
				case 122: eep_put(idx, 2, G.SP.MES_LCK_TOT    ); break;
				case 123: eep_put(idx, 2, G.SP.MES_MES_CND    ); break;
				case 124: eep_put(idx, 4, G.SP.MES_MES_VRD    ); break;
				case 125: eep_put(idx, 4, G.SP.MES_MES_VRC    ); break;
				case 126: eep_put(idx, 4, G.SP.MES_MES_VRB    ); break;
				case 127: eep_put(idx, 2, G.SP.MES_MES_TOT    ); break;
				case 128: eep_put(idx, 2, G.SP.MES_MES_WAI    ); break;
				case 129: eep_put(idx, 2, G.SP.ROM_DAT_STA    ); break;
				case 130: eep_put(idx, 2, G.SP.ROM_DAT_END    ); break;
				//---
#if false
				case  84:eep_put(84, 2, G.SP.MES_CHK[0]      ); break;
				case  85:eep_put(85, 2, G.SP.MES_CHK[1]      ); break;
				case  86:eep_put(86, 2, G.SP.MES_CHK[2]      ); break;
				case  87:eep_put(87, 2, G.SP.MES_CHK[3]      ); break;
				case  88:eep_put(88, 2, G.SP.MES_CHK[4]      ); break;
				case  89:eep_put(89, 2, G.SP.MES_CHK[5]      ); break;
				case  90:eep_put(90, 2, G.SP.MES_CHK[6]      ); break;
				case  91:eep_put(91, 2, G.SP.MES_CHK[7]      ); break;
				case  92:eep_put(92, 2, G.SP.MES_CHK[8]      ); break;
				case  93:eep_put(93, 2, G.SP.MES_CHK[9]      ); break;
				case  94:eep_put(94, 4, G.SP.MES_VAL[0]      ); break;
				case  95:eep_put(95, 4, G.SP.MES_VAL[1]      ); break;
				case  96:eep_put(96, 4, G.SP.MES_VAL[2]      ); break;
				case  97:eep_put(97, 4, G.SP.MES_VAL[3]      ); break;
				case  98:eep_put(98, 4, G.SP.MES_VAL[4]      ); break;
				case  99:eep_put(99, 4, G.SP.MES_VAL[5]      ); break;
				case 100:eep_put(100,4, G.SP.MES_VAL[6]      ); break;
				case 101:eep_put(101,4, G.SP.MES_VAL[7]      ); break;
				case 102:eep_put(102,4, G.SP.MES_VAL[8]      ); break;
				case 103:eep_put(103,4, G.SP.MES_VAL[9]      ); break;
#endif
				}
			}
		skip:
			D.SET_MES_PAR(0, /*RUN*/1);
			G.SB = (SENPAR)G.SP.Clone();
			return(true);
		}
		static public string GET_DOC_PATH(string file)
		{
			string path;
			path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			path += @"\KOP";
			if (!System.IO.Directory.Exists(path)) {
				System.IO.Directory.CreateDirectory(path);
			}
			path += @"\" + Application.ProductName;
			if (!System.IO.Directory.Exists(path)) {
				System.IO.Directory.CreateDirectory(path);
			}
			if (!string.IsNullOrEmpty(file)) {
				if (file[0] != '\\') {
					path += "\\";
				}
				path += file;
			}

			return (path);
		}
		/*static private string GET_SET_PATH()
		{
			string path;
			path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			path += @"\KOP";
			if (!System.IO.Directory.Exists(path)) {
				System.IO.Directory.CreateDirectory(path);
			}
			path += @"\" + Application.ProductName;
			if (!System.IO.Directory.Exists(path)) {
				System.IO.Directory.CreateDirectory(path);
			}
			path += @"\settings.xml";

			return (path);
		}*/
		// filename:setting.xml
		static public bool COPY_SETTINGS(string filename)
		{
			string path;
			// path:C:\\Users\\araya320\\AppData\\Roaming
			path = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			path += @"\KOP";
			if (!System.IO.Directory.Exists(path)) {
				return(false);
			}
			path += @"\" + Application.ProductName;
			if (!System.IO.Directory.Exists(path)) {
				return(false);
			}
			path += @"\";
			path += filename;
			if (!System.IO.File.Exists(path)) {
				return(false);
			}
			try {
				string path_dst = GET_DOC_PATH(filename);
				if (System.IO.File.Exists(path_dst)) {
					//backupを作成
					DateTime dt = System.IO.File.GetLastWriteTime(path_dst);
					string file_base = System.IO.Path.GetFileNameWithoutExtension(filename);
					string file_ext = System.IO.Path.GetExtension(filename);
					string path_bak = file_base;
					path_bak += string.Format("-{0:D04}{1:D02}{2:D02}", dt.Year, dt.Month, dt.Day);
					path_bak += string.Format("-{0:D02}h{1:D02}m{2:D02}s", dt.Hour, dt.Minute, dt.Second);
					path_bak += file_ext;
					path_bak = GET_DOC_PATH(path_bak);
					System.IO.File.Copy(path_dst, path_bak, true);
				}
				System.IO.File.Copy(path, path_dst, true);
				System.IO.File.Delete(path);
			}
			catch (Exception ex) {
				return(false);
			}
			return(true);
		}
	}
}
