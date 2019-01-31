using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace CRTMONITOR
{
    class D
    {
#if true
		[DllImport("USBHIDHELPER32.DLL", EntryPoint = "HID_ENUM")]
		static extern int _HID_ENUM(uint vid, uint pid, IntPtr pcnt);

		[DllImport("USBHIDHELPER32.DLL")]
		static extern int HID_OPEN(uint vid, uint pid, uint did);
    
		[DllImport("USBHIDHELPER32.DLL")]
		static extern int HID_CLOSE();
    
		[DllImport("USBHIDHELPER32.DLL")]
		static extern int WRITE_HID(byte[] pbuf, int size);
    
		[DllImport("USBHIDHELPER32.DLL")]
		static extern int READ_HID(byte[] pbuf, int size);
		/*
		BOOL APIENTRY HID_ENUM(DWORD vid, DWORD pid, LPDWORD pcnt);
		BOOL APIENTRY HID_OPEN(DWORD vid, DWORD pid, DWORD did);
		BOOL APIENTRY HID_CLOSE(void);
		BOOL APIENTRY WRITE_HID(LPBYTE pbuf, DWORD size);
		BOOL APIENTRY READ_HID(LPBYTE pbuf, DWORD size);
		*/
#endif
	    /**/
	    public const int CMD_GET_I2C_RED				= 0x40;
	    public const int CMD_GET_I2C_WRT				= 0x41;
	    public const int CMD_SET_I2C_BUS				= 0x42;
	    public const int CMD_GET_I2C_STS				= 0x43;
		//public const int CMD_SET_CUR_STS                = 0x44;
	    public const int CMD_GET_ECHOBAC				= 0x01;
		//public const int CMD_GET_BTN_STS			 	= 0x10;
		//public const int CMD_SET_LED_STS			 	= 0x11;
		//public const int CMD_SET_HUB_PWR			 	= 0x12;
	    public const int CMD_SET_PWM_STS				= 0x20;
	    public const int CMD_SET_PWM_DTY				= 0x13;
	    public const int CMD_SET_PWM_FRQ				= 0x14;
		public const int CMD_SET_BZR_STS				= 0x16;	// AD0 * A + AD1 * B + C => 0-1023?  
		public const int CMD_GET_ADC_VAL				= 0x17;
		//
		public const int CMD_SET_MES_TBL				= 0x30;
		public const int CMD_GET_SEN_RAW				= 0x31;
		public const int CMD_SET_VAR_TES				= 0x32;
		public const int CMD_SET_EEP_VAR				= 0x33;
		public const int CMD_GET_EEP_VAR				= 0x34;
		public const int CMD_SET_MES_PAR		        = 0x35;
		public const int CMD_GET_SYS_INF			    = 0x36;
		public const int CMD_GET_MES_DAT				= 0x37;
		public const int CMD_SET_EXE_CMD				= 0x38;
		public const int CMD_GET_EEP_DAT				= 0x39;
	    //---
	    private static bool m_access = false;
	    private static bool m_bPresetDone = false;
	    //---
		static public int DEV_TYPE;	//0:mini, 1:汎用
	
	    static private int MAKELONG(int w1, int w2) {
	    // TODO 自動生成されたメソッド・スタブ
		    return((0xffff & w1) << 16 | (0xffff & w2));
	    }
		static public int MAKELONG(int b4, int b3, int b2, int b1)
		{
	    // TODO 自動生成されたメソッド・スタブ
		    return ((0xff & b4) << 24 | (0xff & b3) << 16 | (0xff & b2) << 8 | (0xff & b1));
	    }
	    static public int MAKEWORD(int b2, int b1) {
	    // TODO 自動生成されたメソッド・スタブ
		    return ((0xff & b2) << 8 | (0xff & b1));
	    }
	    static private int WH(int l) {
	    // TODO 自動生成されたメソッド・スタブ
		    return ((short)((l & 0xffff0000) >> 16));
	    }
	    /*private int WL(int l) {
	    // TODO 自動生成されたメソッド・スタブ
		    return ((short)((l & 0x0000ffff) >> 0));
	    }*/
	    static public int B4(int l) {
	    // TODO 自動生成されたメソッド・スタブ
		    return ((byte)((l & 0xff000000) >> 24));
	    }
	    static public int B3(int l) {
	    // TODO 自動生成されたメソッド・スタブ
		    return ((byte)((l & 0x00ff0000) >> 16));
	    }
	    static public int B2(int l) {
	    // TODO 自動生成されたメソッド・スタブ
		    return ((byte)((l & 0x0000ff00) >> 8));
	    }
	    static public int B1(int l) {
	    // TODO 自動生成されたメソッド・スタブ
		    return ((byte)((l & 0x000000ff) >> 0));
	    }
	    static public void PRESET_PARAM() {
		    if (m_bPresetDone) {
			    return;
		    }
			//int[]	STG_HSPD = {500, 500, 500};//PULSE/SEC
			//int[]	STG_LSPD = { 50,  50,  50};//PULSE/SEC
			//int[]	STG_JSPD = {100, 100, 100};//PULSE/SEC
			//int[]	STG_ACCL = {100, 100, 100};//ms
			//int[]	STG_LIMT = {950, 950,250};//PULSE
		    //int		HSPD, LSPD, JSPD, ACCL, LIMT;
			//int		ia, ib, ic, it, tr;
		    for (int i = 0; i < 4; i++){
				//HSPD = G.SS.PLM_HSPD[i];
				//LSPD = G.SS.PLM_LSPD[i];
				//JSPD = G.SS.PLM_JSPD[i];
				//ACCL = G.SS.PLM_ACCL[i];
				//LIMT = G.SS.PLM_PLIM[i];
				//if (!CMDOUT(CMD_SET_PLM_PRM, /*AXIS=*/i, /*HISPD*/0, B2(HSPD), B1(HSPD), null)) {
				//    return;
				//}
				//if (!CMDOUT(CMD_SET_PLM_PRM, /*AXIS=*/i, /*LOSPD*/1, B2(LSPD), B1(LSPD), null)) {
				//    return;
				//}
				//if (!CMDOUT(CMD_SET_PLM_PRM, /*AXIS=*/i, /*JGSPD*/2, B2(JSPD), B1(JSPD), null)) {
				//    return;
				//}
				//if (!CMDOUT(CMD_SET_PLM_PRM, /*AXIS=*/i, /*ACCEL*/3, B2(ACCL), B1(ACCL), null)) {
				//    return;
				//}
				//if (!CMDOUT(CMD_SET_PLM_LMT, /*AXIS=*/i, B3(LIMT)  , B2(LIMT), B1(LIMT), null)) {
				//    return;
				//}
			    if (true) {//2012.02.10 m.araya
					////int[]	STG_LMTM = {-950,-950,-5050};	//CCW側ソフトリミット値[PULSE]
					////
					//int LMTM = G.SS.PLM_MLIM[i];
					//if (!CMDOUT(CMD_SET_PLM_LMT, /*AXIS=*/i+4, B3(LMTM)  , B2(LMTM), B1(LMTM), null)) {
					//    return;
					//}
			    }
		    }
			//再計算
			//if (!CMDOUT(CMD_SET_PLM_PRM, -1, 0, 0, 0, null))
			//{
			//    return;
			//}

			//if (true) {//2012.02.10
			//    tr = 0;		// 許容範囲 0とする=>後ほどこの係数はカットします
			//    it = 50;	// 更新インターバル[ms] 40ms以上の値を指定すること
			//    it = 5 ;//2012.02.17　最小1msに設定
			//    CMDOUT(CMD_SET_AFC_C_T, B2(tr), B1(tr), null);
			//    CMDOUT(CMD_SET_AFC_C_I, B2(it), B1(it), null);
			//}
			//if (true) {//2012.02.11
			//    // デフォルトは800Hzでしたが、とりあえず 3200Hz としました.
			//    CMD_SET_PWM_FRQ(32767);	// 245-32767Hzの範囲で指定してください
			//}
			//if (true) {//2012.02.17 m.araya PID係数の設定
			//    SET_PID_COEFF_P(0.10f);
			//    SET_PID_COEFF_I(0.f);
			//    SET_PID_COEFF_D(0.f);
			//}
			//m_bPresetDone = true;
	    }
		static private int HID_ENUM(uint vid, uint pid, out int pcnt)
		{
			if ((G.AS.DEBUG_MODE & 1) != 0) {
				DBGMODE.HID_ENUM(vid, pid, out pcnt);
				return(1);
			}
			int		ret;
			IntPtr	buf = new IntPtr();

			buf = Marshal.AllocHGlobal(4);
			ret = _HID_ENUM(vid, pid, buf); ;
			pcnt = Marshal.ReadInt32(buf);
			Marshal.FreeHGlobal(buf);

			return (ret);
		}

		static public bool INIT() {
			int cnt;
			int ret;
			// 0x04D8, // Vendor ID
			// 0x003F, // Product ID(uSCOPE)
			// 0xEF22, // Product ID(SENSOR.BIG)
			// 0xEED8, // Product ID(SENSOR.MINI)
			if ((ret = HID_ENUM(0x04D8, 0xEED8, out cnt)) == 0) {
				return(false);
			}
			if (cnt <= 0) {
				G.mlog("#sCRTデバイスに接続できません.");
				return(false);
			}
			if ((G.AS.DEBUG_MODE & 1) != 0) {
				if (DBGMODE.HID_OPEN(0x04D8, 0xEED8, 0) == 0) {
					return(false);
				}
				DEV_TYPE = 0;
				m_access = true;
				return (true);
			}
			if (HID_OPEN(0x04D8, 0xEED8, 0) == 0) {
				G.mlog("ERROR @ HID_OPEN");
				return (false);
			}
			DEV_TYPE = 0;
			m_access = true;
			return (true);
		}
		static public void SET_I2C_BUS(int busadr, int busenb, int busspd, int wait01, int wait02) {
			byte[] ibuf = {0,0};

			CMDOUT05(CMD_SET_I2C_BUS, busadr, busenb, busspd, wait01, wait02);
		}
		static public void GET_I2C_STS(int busadr, out int con, out int sts) {

			byte[] buf = new byte[4];
			CMDOUT(CMD_GET_I2C_STS, busadr, buf);

			con = MAKEWORD(buf[0], buf[1]);
			sts = MAKEWORD(buf[2], buf[3]);
			return;
		}
		static public void GET_I2C_WRT(int busadr, int devadr, int regadr, int len, byte[] obuf, out int ret, out int done) {
			byte[] ibuf = {0,0};

			CMDOUT64(CMD_GET_I2C_WRT, busadr, devadr, regadr, len, obuf, ibuf);

			ret = ibuf[0];
			done = ibuf[1];
		}
		static public void GET_I2C_RED(int busadr, int devadr, int regadr, int len, out byte[] ibuf, out int ret, out int done) {
			byte[] buf = new byte[2+len];
			ibuf = new byte[len];

			CMDOUT64(CMD_GET_I2C_RED, busadr, devadr, regadr, len, null, buf);

			ret = buf[0];
			done = buf[1];
			if (len > 0) {
				System.Array.Copy(buf, 2, ibuf, 0, len);
			}
		}
		static public void TERM() {
			if (m_access) {
				if ((G.AS.DEBUG_MODE & 1) != 0) {
					DBGMODE.HID_CLOSE();
					m_access = false;
					return;
				}
				HID_CLOSE();
				m_access = false;
			}
		}
		//static public String GET_DEV_STR() {
		//    return(m_access.m_devstr);    	
		//}
		//static public void SET_ILED_STS(int sts) {
		//    CMDOUT(CMD_SET_LED_STS, sts, null);
		//}
		//static public void SET_HUB_PWR(int sts) {
		//    sts = ~sts;
		//    CMDOUT(CMD_SET_HUB_PWR, sts, null);
		//}
		static public bool isCONNECTED() {
			return(m_access);
		}
		/************************************************************/
		/* 2012.02.11 追加 */
		/************************************************************/
		static public void SET_PWM_FRQ(int freq) {
			if (freq < 245) {
				freq = 245;
			}
			else if (freq > 32767) {
				freq = 32767;
			}
			CMDOUT(CMD_SET_PWM_FRQ, B2(freq), B1(freq), null);
		}
		static public void SET_PWM_DTY(int chan, int duty)
		{
			CMDOUT02(CMD_SET_PWM_DTY, chan, duty);
		}
		static public void SET_PWM_STS(int chan, int sts)
		{
			if (sts != 0) {
				G.LED_PWR_STS |= (1 << chan);
			}
			else {
				G.LED_PWR_STS &=~(1 << chan);
			}
			CMDOUT02(CMD_SET_PWM_STS, chan, sts);
		}
		static public void SET_BZR_STS(int freq, string pat)
		{
			byte[] buf = new byte[63];
			int h = 0;
			buf[0] = (byte)B2(freq);
			buf[1] = (byte)B1(freq);
			for (int i = 2; i < buf.Length; i++) {
				if (h >= pat.Length) {
					break;
				}
				buf[i] = (byte)pat[h++];
			}
			CMDOUT64(CMD_SET_BZR_STS, buf, null);
		}
		static public void GET_ADC_VAL(ushort[] adbuf)
		{
			byte[] buf = new byte[64];
			int	h = 0;
			CMDOUT(CMD_GET_ADC_VAL, 0, buf);
			for (int i = 0; i < buf.Length; i+=2, h++) {
				if (h >= adbuf.Length) {
					break;
				}
				adbuf[h] = (ushort)MAKEWORD(buf[i + 1], buf[i + 0]);
			}
			return;
		}
		static public void SET_MES_TBL(byte[] buf)
		{
			CMDOUT64(CMD_SET_MES_TBL, buf, null);
			return;
		}
		static public void GET_SEN_RAW(int cnt, byte[] buf)
		{
			byte[] obuf = new byte[0];
			CMDOUT64(CMD_GET_SEN_RAW, obuf, buf);
			return;
		}
		static public void SET_VAR_TES(float val)
		{
			byte[] buf = BitConverter.GetBytes(val);
			CMDOUT05(CMD_SET_VAR_TES, 'F', buf[0], buf[1], buf[2], buf[3]);
		}
		static public void SET_VAR_TES(int idx)
		{
			CMDOUT02(CMD_SET_VAR_TES, idx, 0);
		}
		static public void SET_EEP_VAR(int idx, int len, byte[]buf)
		{
			byte[] obuf = new byte[64];
			
			obuf[0] = (byte)idx;
			obuf[1] = (byte)len;
			for (int i = 0; i < len; i++) {
				obuf[2+i] = buf[i];
			}
			CMDOUT64(CMD_SET_EEP_VAR, obuf, null);
		}
		static public void GET_EEP_VAR(int idx, int len, out byte[]buf)
		{
			byte[] obuf = new byte[2];
			byte[] ibuf = new byte[64];
			
			obuf[0] = (byte)idx;
			obuf[1] = (byte)len;
			CMDOUT64(CMD_GET_EEP_VAR, obuf, ibuf);
			buf = new byte[len];
			for (int i = 0; i < len; i++) {
				buf[i] = ibuf[i];
			}
		}
		static public void SET_MES_PAR(int idx, int val)
		{
			CMDOUT05(CMD_SET_MES_PAR, idx, B2(val), B1(val), 0, 0);
		}
		static public void GET_SYS_INF(out byte[]buf)
		{
			buf = new byte[64];
			CMDOUT64(CMD_GET_SYS_INF, null, buf);
		}
		static public void GET_MES_DAT(out byte[] md1, out byte[] md2)
		{
			md1 = new byte[64];
			md2 = new byte[64];
			CMDOUT64(CMD_GET_MES_DAT, 0, 0, 0, 0, null, md1);
			CMDOUT64(CMD_GET_MES_DAT, 1, 0, 0, 0, null, md2);
		}
		static public void SET_EXE_CMD(int idx)
		{
			CMDOUT05(CMD_SET_EXE_CMD, idx, 0, 0, 0, 0);
		}
		
		static public void GET_EEP_DAT(int idx, out byte[] buf)
		{
			buf = new byte[64];
			CMDOUT64(CMD_GET_EEP_DAT, B2(idx), B1(idx), 0, 0, null, buf);
		}
		
		//static public void GET_EEP_MEM(int adr, out ushort val)
		//{
		//    byte[] buf = new byte[64];

		//    CMDOUT(CMD_GET_EEP_MEM, adr, 2, buf);
		//    val = (ushort)MAKEWORD(buf[1], buf[0]);
		//}
		//static public void GET_EEP_MEM(int adr, out float val)
		//{
		//    byte[] buf = new byte[64];

		//    CMDOUT(CMD_GET_EEP_MEM, adr, 4, buf);
		//    val = BitConverter.ToSingle(buf, 0);
		//}
		//static public void SET_EEP_MEM(int adr, ushort val)
		//{
		//    byte[] buf = new byte[64];
		//    buf[0] = (byte)(val >> 8);
		//    buf[1] = (byte)(val & 0xFF);

		//    CMDOUT(CMD_SET_EEP_MEM, adr, 2, buf);
		//}
		//static public void SET_EEP_MEM(int adr, float val)
		//{
		//    byte[] buf = BitConverter.GetBytes(val);
		//    CMDOUT(CMD_SET_EEP_MEM, adr, 4, buf);
		//}
		/************************************************************/
		static public bool CMDOUT(int cmd, int par1, byte[] buf)
		{
			return(CMDOUT(cmd, par1, 0, 0, 0, buf));
		}
		/************************************************************/
		static public bool CMDOUT(int cmd, int par1, int par2, byte[] buf)
		{
			return(CMDOUT(cmd, par1, par2, 0, 0, buf));
		}
		/************************************************************/
		static public bool CMDOUT(int cmd, int par1, int par2, int par3, byte[] buf)
		{
			return(CMDOUT(cmd, par1, par2, par3, 0, buf));
		}
		/************************************************************/
		static public bool CMDOUT(int cmd, int par1, int par2, int par3, int par4, byte[] buf)
		{
			byte[] commandPacket = new byte[16];
			commandPacket[0] = (byte)cmd;
			commandPacket[1] = (byte)par1;
			commandPacket[2] = (byte)par2;
			commandPacket[3] = (byte)par3;
			commandPacket[4] = (byte)par4;

			if ((G.AS.DEBUG_MODE & 1) != 0) {
				DBGMODE.WRITE_HID(commandPacket);
				if (buf != null) {
				DBGMODE.READ_HID(buf);
				}
				return(true);
			}
			if (WRITE_HID(commandPacket, commandPacket.Length) == 0) {
		//			Toast.makeText(TEST24Activity.this, "USB COMMUNICATION ERROR!!!", Toast.LENGTH_SHORT).show();
			//	return(false);
			}
			if (buf != null) {
				if (READ_HID(commandPacket, commandPacket.Length) == 0)
				{
					return(false);
				}
				for (int i = 0; i < commandPacket.Length; i++)
				{
					if (i >= buf.Length)
					{
						break;
					}
					buf[i] = commandPacket[i];
				}
			}
			return(true);
		}
		/************************************************************/
		static public bool CMDOUT05(int cmd, int par1, int par2, int par3, int par4, int par5)
		{
			byte[] commandPacket = new byte[16];
			commandPacket[0] = (byte)cmd;
			commandPacket[1] = (byte)par1;
			commandPacket[2] = (byte)par2;
			commandPacket[3] = (byte)par3;
			commandPacket[4] = (byte)par4;
			commandPacket[5] = (byte)par5;

			if ((G.AS.DEBUG_MODE & 1) != 0) {
				DBGMODE.WRITE_HID(commandPacket);
				return(true);
			}
			if (WRITE_HID(commandPacket, commandPacket.Length) == 0) {
		//			Toast.makeText(TEST24Activity.this, "USB COMMUNICATION ERROR!!!", Toast.LENGTH_SHORT).show();
			//	return(false);
			}
			return(true);
		}
		/************************************************************/
		static public bool CMDOUT02(int cmd, int par1, int par2)
		{
			byte[] commandPacket = new byte[16];
			commandPacket[0] = (byte)cmd;
			commandPacket[1] = (byte)par1;
			commandPacket[2] = (byte)par2;
			commandPacket[3] = 0;
			commandPacket[4] = 0;
			commandPacket[5] = 0;


			if (WRITE_HID(commandPacket, commandPacket.Length) == 0) {
		//			Toast.makeText(TEST24Activity.this, "USB COMMUNICATION ERROR!!!", Toast.LENGTH_SHORT).show();
				return(false);
			}
			return(true);
		}
		/************************************************************/
		static public bool CMDOUT64(int cmd, int par1, int par2, int par3, int par4, byte[] obuf, byte[] ibuf)
		{
			byte[] commandPacket = new byte[64];
			commandPacket[0] = (byte)cmd;
			commandPacket[1] = (byte)par1;
			commandPacket[2] = (byte)par2;
			commandPacket[3] = (byte)par3;
			commandPacket[4] = (byte)par4;
			if (obuf != null) {
				for (int i = 0; i < obuf.Length; i++) {
					commandPacket[5+i] = obuf[i];
				}
			}
			if ((G.AS.DEBUG_MODE & 1) != 0) {
				DBGMODE.WRITE_HID(commandPacket);
				if (ibuf != null) {
				DBGMODE.READ_HID(ibuf);
				}
				return(true);
			}
			if (WRITE_HID(commandPacket, commandPacket.Length) == 0) {
			//	return(false);
			}
			if (ibuf != null) {
				if (READ_HID(commandPacket, commandPacket.Length) == 0) {
					return(false);
				}
				for (int i = 0; i < commandPacket.Length; i++) {
					if (i >= ibuf.Length) {
						break;
					}
					ibuf[i] = commandPacket[i];
				}
			}
			return(true);
		}
		/************************************************************/
		static public bool CMDOUT64(int cmd, byte[] obuf, byte[] ibuf)
		{
			byte[] commandPacket = new byte[64];
			commandPacket[0] = (byte)cmd;
			if (obuf != null) {
				for (int i = 0; i < obuf.Length && i < 63; i++) {
					commandPacket[1 + i] = obuf[i];
				}
			}
			if ((G.AS.DEBUG_MODE & 1) != 0) {
				DBGMODE.WRITE_HID(commandPacket);
				if (ibuf != null) {
				DBGMODE.READ_HID(ibuf);
				}
				return(true);
			}
			if (WRITE_HID(commandPacket, commandPacket.Length) == 0) {
				//	return(false);
			}
			if (ibuf != null) {
				if (READ_HID(commandPacket, commandPacket.Length) == 0) {
					return (false);
				}
				for (int i = 0; i < commandPacket.Length; i++) {
					if (i >= ibuf.Length) {
						break;
					}
					ibuf[i] = commandPacket[i];
				}
			}
			return (true);
		}
		static public void CLEAR_PRESET_FLAG() {
			m_bPresetDone = false;
		}
    }
}
