using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Diagnostics;
using System.Linq;

namespace CRTMONITOR
{
    class S
    {
#if false
		static public bool WRITE_ONE(int bid, int did, int adr, int dat)
		{
			byte[] buf = {(byte)dat};
			int	ret, done;

			D.GET_I2C_WRT(bid, did, adr, /*len*/1, buf, out ret, out done);
			if (ret != 1 || done != 1) {
				//G.mlog("ret != 1 || done != 1");
			//	throw new Exception(string.Format("ERROR @ WRITE_ONE / BUS/DEV={0:X2}h/{1:X2}h, adr/ret/done={2:X2}h/{3}/{4}", BUSADR, DEVADR, adr, ret, done));
				return(false);
			}
			return(true);
		}
		static public bool READ_ONE(int bid, int did, int adr, out int dat)
		{
			byte[] buf = {0};
			int	ret, done;
			dat = 0;
			D.GET_I2C_RED(bid, did, adr, /*len*/1, out buf, out ret, out done);
			if (ret != 1 || done != 1) {
			//	throw new Exception(string.Format("ERROR @ READ_ONE / BUS/DEV={0:X2}h/{1:X2}h, adr/ret/done={2:X2}h/{3}/{4}", BUSADR, DEVADR, adr, ret, done));
				return(false);
			}
			dat = buf[0];
			return(true);
		}
		static public bool READ_ONE(int bid, int did, int adr, out byte dat)
		{
			byte[] buf = {0};
			int	ret, done;
			dat = 0;
			D.GET_I2C_RED(bid, did, adr, /*len*/1, out buf, out ret, out done);
			if (ret != 1 || done != 1) {
			//	throw new Exception(string.Format("ERROR @ READ_ONE / BUS/DEV={0:X2}h/{1:X2}h, adr/ret/done={2:X2}h/{3}/{4}", BUSADR, DEVADR, adr, ret, done));
				return(false);
			}
			dat = buf[0];
			return(true);
		}
		static int m_chk0;
		static public bool INIT_38(int bid, int did, int id)
		{
			bool rc = false;
			int reg;
		//	int index;
		//	byte[] als_gain_table = {1, 2, 64, 128};
		//	ushort[] als_meas_time_table = {0,0,0,0,0,100,100,100,100,100,400,400,50,0,0,0};
/*			int id;
			if (bid == 0x00 && did == 0x38) {
				id = 0;
			}
			else if (did == 0x39) {
				id = 1;
			}
			else {
				id = 2;
			}*/
			if (!READ_ONE(bid, did, 0x40/*REG_SYSTEM_CONTROL*/, out reg)) {
				goto skip;
			}
			if ((reg&0x3F) != 0x0A/*PART_ID_VAL*/) {
				//Serial.println(F("Can't find RPR0521RS"));
				goto skip;
			}

			if (!READ_ONE(bid, did, 0x92/*REG_MANUFACT_ID*/, out reg)) {
				goto skip;
			}
			if (reg != 0xE0/*MANUFACT_ID_VAL*/) {
				//Serial.println(F("Can't find RPR0521RS"));
				goto skip;
			}
			if (true) {
				reg = 0;
				reg |= G.SP.D38_424_ALG1[id] << 4;
				reg |= G.SP.D38_422_ALG2[id] << 2;
				reg |= G.SP.D38_420_LEDC[id] << 0;
				WRITE_ONE(bid, did, 0x42/*REG_ALS_PS_CONTROL*/, reg);
			}
			if (!READ_ONE(bid, did, 0x43/*REG_PS_CONTROL*/, out reg)) {
				goto skip;
			}
			if (m_chk0 == 0) {
				m_chk0++;
				G.mlog("maskしてクリアする必要？？");
			}
			reg |= G.SP.D38_434_PSG0[id] << 4;
			if (!WRITE_ONE(bid, did, 0x43/*REG_PS_CONTROL*/, reg)) {
				goto skip;
			}
			reg = 0;
			reg |= G.SP.D38_410_MSTM[id] << 0;
		//	reg |= MODE_CONTROL_PS_EN ;//(1 << 6)
		//	reg |= MODE_CONTROL_ALS_EN;//(1 << 7)
			reg |= ((1<<6)|(1<<7));
			if (!WRITE_ONE(bid, did, 0x41/*REG_MODE_CONTROL*/, reg)) {
				goto skip;
			}
			/*
			reg = ALS_PS_CONTROL_VAL;
			index = (reg >> 4) & 0x03;
			if (true) {
				index = G.SP.D38_424_ALG1[id];
			}
			_als_data0_gain = als_gain_table[index];
			index = (reg >> 2) & 0x03;
			if (true) {
				index = G.SP.D38_422_ALG2[id];
			}
			_als_data1_gain = als_gain_table[index];

			index = MODE_CONTROL_VAL & 0x0F;
			if (true) {
				index = G.SP.D38_410_MSTM[id];
			}
			_als_measure_time = als_meas_time_table[index];*/
		skip:
			rc = true;
			return(rc);
		}
		static public bool INIT_29(int bid, int did, int id)
		{
			const int _REGISTER_ENABLE   = (0x80|0x00);
			const int _REGISTER_ATIME    = (0x80|0x01);
			const int _REGISTER_CONTROL  = (0x80|0x0f);
			const int _REGISTER_SENSORID = (0x80|0x12);

			const int _ENABLE_AEN = 0x02;
			const int _ENABLE_PON = 0x01;
			bool rc = false;
			int reg;
			if (!READ_ONE(bid, did, _REGISTER_SENSORID, out reg)) {		//reg -> 0x44
				goto skip;
			}
			if (!(reg == 0x44 || reg == 0x10)) {
				goto skip;
			}
			if (true) {
				//POWER->OFF
				if (!READ_ONE(bid, did, _REGISTER_ENABLE, out reg)) {	//reg -> 0x14
					goto skip;
				}
				reg &= ~(_ENABLE_PON|_ENABLE_AEN);
				if (!WRITE_ONE(bid, did, _REGISTER_ENABLE, reg)) {		//reg -> 0x14
					goto skip;
				}
			}
			if (true) {
				if (!WRITE_ONE(bid, did, _REGISTER_ATIME, G.SP.D29_010_ATIM)) {
					goto skip;
				}
				G.SS.D29_XXX_ATMS = (256 - G.SP.D29_010_ATIM) * 2.4;
			}
			if (true) {
				if (!WRITE_ONE(bid, did, _REGISTER_CONTROL, G.SP.D29_0F0_GAIN)) {
					goto skip;
				}
			}
			if (true) {
				if (!READ_ONE(bid, did, _REGISTER_ENABLE, out reg)) {	//reg -> 0x14
					goto skip;
				}
				reg |= _ENABLE_PON;
				if (!WRITE_ONE(bid, did, _REGISTER_ENABLE, reg)) {		//reg -> 0x15
					goto skip;
				}
				System.Threading.Thread.Sleep(3);
				reg |= (_ENABLE_PON|_ENABLE_AEN);
				if(!WRITE_ONE(bid, did, _REGISTER_ENABLE, reg)) {		//reg -> 0x17
					goto skip;
				}
			}
			rc = true;
		skip:
			return(rc);
		}
		static public bool INIT_76(int bid, int did, int id)
		{
#if false
			const int _REGISTER_ENABLE   = (0x80|0x00);
			const int _REGISTER_ATIME    = (0x80|0x01);
			const int _REGISTER_CONTROL  = (0x80|0x0f);
			const int _REGISTER_SENSORID = (0x80|0x12);

			const int _ENABLE_AEN = 0x02;
			const int _ENABLE_PON = 0x01;
			bool rc = false;
			int reg;


			int osrs_t = 1;			//#Temperature oversampling x 1
			int osrs_p = 1;			//#Pressure oversampling x 1
			int osrs_h = 1;			//#Humidity oversampling x 1
			int mode   = 3;			//#Normal mode
//			int t_sb   = 5;			//#Tstandby 1000ms
			int t_sb   = 2;			//#Tstandby (1:62.5, 2:125, 3:250, 4:500, 5:1000ms)
//			int filter = 0;			//#Filter off
			int filter = 2;			//#Filter(0:off, 1:2, 2,4, 3:8, 4:16)
			int spi3w_en = 0;		//#3-wire SPI Disable

			int ctrl_meas_reg = (osrs_t << 5) | (osrs_p << 2) | mode;
			int config_reg    = (t_sb << 5) | (filter << 2) | spi3w_en;
			int ctrl_hum_reg  = osrs_h;
			if (true) {
				int r1 = ctrl_hum_reg,
					r2 = ctrl_meas_reg,
					r3 = config_reg;

				ctrl_hum_reg  = G.SP.D76_F20_HUMI;
				//---
				ctrl_meas_reg = G.SP.D76_F40_MODE;
				ctrl_meas_reg|= G.SP.D76_F42_PRES<<2;
				ctrl_meas_reg|= G.SP.D76_F45_TEMP<<5;
				//---
				config_reg    = 0;
				config_reg   |= G.SP.D76_F52_FILT<<2;
				config_reg   |= G.SP.D76_F55_STBY<<5;
			}
#endif
			bool rc = false;
			byte[] calib = new byte[64];
			int h = 0;	

			if (!WRITE_ONE(bid, did, 0xF2, (G.SP.D76_F20_HUMI))) {
				goto skip;
			}
			if (!WRITE_ONE(bid, did, 0xF4, (G.SP.D76_F45_TEMP<<5 | G.SP.D76_F42_PRES<<2 | G.SP.D76_F40_MODE<<0))) {
				goto skip;
			}
			if (!WRITE_ONE(bid, did, 0xF5, (G.SP.D76_F55_STBY<<5 | G.SP.D76_F52_FILT<<2))) {
				goto skip;
			}

			for (int i = 0; i < 24; i++) {
				if (!READ_ONE(bid, did, 0x88+i, out calib[h++])) {
					goto skip;
				}
			}
			if (true) {
				if (!READ_ONE(bid, did, 0xA1+0, out calib[h++])) {
					goto skip;
				}
			}
			for (int i = 0; i < 7; i++) {
				if (!READ_ONE(bid, did, 0xE1+i, out calib[h++])) {
					goto skip;
				}
			}

	
			G.SS.D76_CAL_DIGT[0] = (int  )((calib[1] << 8) | calib[0]);
			G.SS.D76_CAL_DIGT[1] = (short)((calib[3] << 8) | calib[2]);
			G.SS.D76_CAL_DIGT[2] = (int  )((calib[5] << 8) | calib[4]);
			//---
			G.SS.D76_CAL_DIGP[0] = (int  )((calib[7] << 8) | calib[6]);
			G.SS.D76_CAL_DIGP[1] = (short)((calib[9] << 8) | calib[8]);
			G.SS.D76_CAL_DIGP[2] = (short)((calib[11]<< 8) | calib[10]);
			G.SS.D76_CAL_DIGP[3] = (short)((calib[13]<< 8) | calib[12]);
			G.SS.D76_CAL_DIGP[4] = (short)((calib[15]<< 8) | calib[14]);
			G.SS.D76_CAL_DIGP[5] = (short)((calib[17]<< 8) | calib[16]);
			G.SS.D76_CAL_DIGP[6] = (short)((calib[19]<< 8) | calib[18]);
			G.SS.D76_CAL_DIGP[7] = (short)((calib[21]<< 8) | calib[20]);
			G.SS.D76_CAL_DIGP[8] = (int  )((calib[23]<< 8) | calib[22]);
			//---
			G.SS.D76_CAL_DIGH[0] = (short)( calib[24] );
			G.SS.D76_CAL_DIGH[1] = (short)((calib[26]<< 8) | calib[25]);
			G.SS.D76_CAL_DIGH[2] = (short)( calib[27] );
			G.SS.D76_CAL_DIGH[3] = (short)((calib[28]<< 4) | (0x0F & calib[29]));
			G.SS.D76_CAL_DIGH[4] = (short)((calib[30]<< 4) | ((calib[29] >> 4) & 0x0F));
			G.SS.D76_CAL_DIGH[5] = (short)( calib[31] );
			//---
			rc = true;
		skip:
			return(rc);
		}
		static void delay(int ms)
		{
			System.Threading.Thread.Sleep(ms);
		}
		static public bool INIT_68(int bid, int did, int id)
		{
			const int REG_SMPLRT_DIV    =0x19;
			const int REG_CONFIG        =0x1A;
			const int REG_GYRO_CONFIG   =0x1B;
			const int REG_ACCEL_CONFIG  =0x1C;
			const int REG_ACCEL_CONFIG2	=0x1D;
			const int REG_INT_PIN_CFG   =0x37;
			const int REG_INT_ENABLE    =0x38;
			const int REG_PWR_MGMT_1	=0x6B; // Device defaults to the SLEEP mode
//			const int REG_PWR_MGMT_2	=0x6C;

			//const int _ENABLE_AEN = 0x02;
			//const int _ENABLE_PON = 0x01;
			bool rc = false;
			int c;
//			int reg;


			// wake up device
			// Clear sleep mode bit (6), enable all sensors
			if (!WRITE_ONE(bid, did, REG_PWR_MGMT_1, 0x00)) {
				goto skip;
			}
			delay(100); // Wait for all registers to reset

			// Get stable time source
			// Auto select clock source to be PLL gyroscope reference if ready else
			if (!WRITE_ONE(bid, did, REG_PWR_MGMT_1, 0x01)) {
				goto skip;
			}
			delay(200);

			// Configure Gyro and Thermometer
			// Disable FSYNC and set thermometer and gyro bandwidth to 41 and 42 Hz,
			// respectively;
			// minimum delay time for this setting is 5.9 ms, which means sensor fusion
			// update rates cannot be higher than 1 / 0.0059 = 170 Hz
			// DLPF_CFG = bits 2:0 = 011; this limits the sample rate to 1000 Hz for both
			// With the MPU9250, it is possible to get gyro sample rates of 32 kHz (!),
			// 8 kHz, or 1 kHz
			if (!WRITE_ONE(bid, did, /*1Ah*/REG_CONFIG, /*0x03*/G.SP.D68_1A0_GLPF)) {
				goto skip;
			}

			// Set sample rate = gyroscope output rate/(1 + SMPLRT_DIV)
			// Use a 200 Hz rate; a rate consistent with the filter update rate
			// determined inset in CONFIG above.
			if (!WRITE_ONE(bid, did, /*19h*/REG_SMPLRT_DIV, /*0x04*/G.SP.D68_190_SMRT)) {
				goto skip;
			}

			// Set gyroscope full scale range
			// Range selects FS_SEL and AFS_SEL are 0 - 3, so 2-bit values are
			// left-shifted into positions 4:3

			// get current GYRO_CONFIG register value
			if (!READ_ONE(bid, did, /*1Bh*/REG_GYRO_CONFIG, out c)) {
				goto skip;
			}
			// c = c & ~0xE0; // Clear self-test bits [7:5]
			c = c & ~0x02; // Clear Fchoice bits [1:0]
			c = c & ~0x18; // Clear AFS bits [4:3]
	//@@@	c = c | Gscale << 3; // Set full scale range for the gyro
			// Set Fchoice for the gyro to 11 by writing its inverse to bits 1:0 of
			// GYRO_CONFIG
			// c =| 0x00;
			if (true) {
				c &= ~(0x03 << 3);			//3<<3:[0001-1000b]
				c |= G.SP.D68_1B3_GSCL<<3;
	//@@@		Gscale = (byte)G.SP.D68_1B3_GSCL;
			}
			// Write new GYRO_CONFIG value to register
			if (!WRITE_ONE(bid, did, /*1Bh*/REG_GYRO_CONFIG, c)) {
				goto skip;
			}

			// Set accelerometer full-scale range configuration
			// Get current ACCEL_CONFIG register value
			if (!READ_ONE(bid, did, /*1Ch*/REG_ACCEL_CONFIG, out c)) {
				goto skip;
			}
			// c = c & ~0xE0; // Clear self-test bits [7:5]
			c = c & ~0x18;  // Clear AFS bits [4:3]
	//@@@	c = c | Ascale << 3; // Set full scale range for the accelerometer
			if (true) {
				c &= ~(0x03 << 3);
				c |= (G.SP.D68_1C3_ASCL<<3);
	//@@@		Ascale = (byte)G.SP.D68_1C3_ASCL;
			}
			// Write new ACCEL_CONFIG register value
			if (!WRITE_ONE(bid, did, /*1Ch*/REG_ACCEL_CONFIG, c)) {
				goto skip;
			}

			// Set accelerometer sample rate configuration
			// It is possible to get a 4 kHz sample rate from the accelerometer by
			// choosing 1 for accel_fchoice_b bit [3]; in this case the bandwidth is
			// 1.13 kHz
			// Get current ACCEL_CONFIG2 register value
			if (!READ_ONE(bid, did, /*1Dh*/REG_ACCEL_CONFIG2, out c)) {
				goto skip;
			}
			c = c & ~0x0F; // Clear accel_fchoice_b (bit 3) and A_DLPFG (bits [2:0])
	//@@@	c = c | 0x03;  // Set accelerometer rate to 1 kHz and bandwidth to 41 Hz
			if (true) {
				c &= ~(0x03 << 0);
				c |= (G.SP.D68_1D0_ALPF << 0);
			}
			// Write new ACCEL_CONFIG2 register value
			if (!WRITE_ONE(bid, did, /*1Dh*/REG_ACCEL_CONFIG2, c)) {
				goto skip;
			}
			// The accelerometer, gyro, and thermometer are set to 1 kHz sample rates,
			// but all these rates are further reduced by a factor of 5 to 200 Hz because
			// of the SMPLRT_DIV setting

			// Configure Interrupts and Bypass Enable
			// Set interrupt pin active high, push-pull, hold interrupt pin level HIGH
			// until interrupt cleared, clear on read of INT_STATUS, and enable
			// I2C_BYPASS_EN so additional chips can join the I2C bus and all can be
			// controlled by the Arduino as master.
#if true
			//磁気センサ(AK8963)はアクセスしない
			if (!WRITE_ONE(bid, did, REG_INT_PIN_CFG, 0x20)) {
				goto skip;
			}
#else
			WRITE_ONE(INT_PIN_CFG, 0x22);
#endif

			// Enable data ready (bit 0) interrupt
			if (!WRITE_ONE(bid, did, REG_INT_ENABLE, 0x01)) {
				goto skip;
			}
			delay(100);
			rc = true;
		skip:
			return(rc);
		}
		static public bool INIT_DEVS()
		{
			bool rc = false;

			if (!INIT_38(0x00, 0x38, 0)) {//照度近接:左(P,L)
				goto skip;
			}
			if (!INIT_38(0x00, 0x39, 1)) {//照度近接:右(P,L)
				goto skip;
			}
			if (!INIT_38(0x10, 0x38, 2)) {//照度近接:奥(P,L)
				goto skip;
			}
			if (!INIT_29(0x10, 0x29, 0)) {//カラーセンサー(C,R,G,B)
				goto skip;
			}
			if (!INIT_76(0x00, 0x76, 0)) {//温湿度(P,T,H)
				goto skip;
			}
			if (!INIT_68(0x10, 0x68, 0)) {//加速度・ジャイロ(Axyz,T,Gxyz)
				goto skip;
			}
			rc = true;
		skip:
			if (!rc) {
				G.mlog("センサー初期化エラー発生！！！");
			}
			return(rc);
		}

		static public void set_mes_table()
		{
#if true
			G.mlog("書き換え必要!!");
#else
			//
			byte[] tmp00 = new byte[] {0x00, 0x38, 0x44, 6};//照度近接:左(P,L)
			byte[] tmp01 = new byte[] {0x00, 0x39, 0x44, 6};//照度近接:右(P,L)
			byte[] tmp02 = new byte[] {0x10, 0x38, 0x44, 6};//照度近接:奥(P,L)
			byte[] tmp03 = new byte[] {0x10, 0x29, 0x14|0x80, 8};//カラーセンサー(C,R,G,B)
			byte[] tmp04 = new byte[] {0x00, 0x76, 0xF7, 8};//温湿度(P,T,H)
			byte[] tmp05 = new byte[] {0x10, 0x68, 0x3B,14};//加速度・ジャイロ(Axyz,T,Gxyz)
			byte[] tmp06 = new byte[] {0x00, 0xFF, 0x00, 6};//ADC
			byte[] tmp07 = new byte[] {0x00, 0x00, 0x00, 0};//EOT
			List<byte> obuf = new List<byte>();
			obuf.AddRange(tmp00);
			obuf.AddRange(tmp01);
			obuf.AddRange(tmp02);
			obuf.AddRange(tmp03);
			obuf.AddRange(tmp04);
			obuf.AddRange(tmp05);
			obuf.AddRange(tmp06);
			obuf.AddRange(tmp07);
			D.SET_MES_TBL(obuf.ToArray());
#endif
		}
#endif
		public class MES_DAT {
			public double L_L, L_P;
			public double R_L, R_P;
			public double B_L, B_P;
			public double C_R, C_G, C_B, C_C;
			public double S_P, S_T, S_H;
			public double A_X, A_Y, A_Z;
			public double G_X, G_Y, G_Z;
		//	public double D_Z, D_P, D_B;
			public double[] D_C, D_V, D_F;
			public double TES;
			public uint	IDX;
			public MES_DAT() {
				L_L= L_P = double.NaN;
				R_L= R_P = double.NaN;
				B_L= B_P = double.NaN;
				C_R= C_G= C_B= C_C = double.NaN;
				S_P= S_T= S_H = double.NaN;
				A_X= A_Y= A_Z = double.NaN;
				G_X= G_Y= G_Z = double.NaN;
				//D_Z= D_P= D_B = double.NaN;
				D_C = new double[]{double.NaN,double.NaN,double.NaN};
				D_V = new double[]{double.NaN,double.NaN,double.NaN};
				D_F = new double[]{double.NaN,double.NaN,double.NaN};
				TES = double.NaN;
				IDX = 0;
			}
		};
		//static public void get_sen_raw(out MES_DAT dat)
		//{
		//    byte[] ibuf = new byte[64];
		//    List<byte> lbuf;
		//    D.GET_SEN_RAW(1, ibuf);

		//    lbuf = new List<byte>(ibuf);
		//    uint meas_sqno, meas_time, ret, done;
		//    meas_sqno = (UInt32)D.MAKELONG(ibuf[3],ibuf[2],ibuf[1],ibuf[0]);
		//    meas_time = (UInt32)D.MAKEWORD(ibuf[5],ibuf[4]);
		//    ret = ibuf[25];
		//    done = ibuf[43];
		//    dat = new MES_DAT();
		//    GET_38(lbuf.GetRange( 6, 6).ToArray(), 13, ref dat.L_L, ref dat.L_P);
		//    GET_38(lbuf.GetRange(12, 6).ToArray(), 15, ref dat.R_L, ref dat.R_P);
		//    GET_38(lbuf.GetRange(18, 6).ToArray(), 17, ref dat.B_L, ref dat.B_P);
		//    GET_29(lbuf.GetRange(26, 8).ToArray(),  0, ref dat.C_R, ref dat.C_G, ref dat.C_B, ref dat.C_C);
		//    GET_76(lbuf.GetRange(34, 8).ToArray(),  4, ref dat.S_P, ref dat.S_T, ref dat.S_H);
		//    GET_68(lbuf.GetRange(44,14).ToArray(),  7, ref dat.A_X, ref dat.A_Y, ref dat.A_Z, ref dat.G_X, ref dat.G_Y, ref dat.G_Z);
		//    GET_FF(lbuf.GetRange(58, 6).ToArray(), 20, ref dat.D_C, ref dat.D_V, ref dat.D_F);
		//    dat.TES = meas_time;
		//    dat.IDX = meas_sqno;
		//}
		static public void get_mes_dat(out MES_DAT dat)
		{
			byte[] md1, md2;
			List<byte> l1, l2;
			D.GET_MES_DAT(out md1, out md2);

			l1 = new List<byte>(md1);
			l2 = new List<byte>(md2);
			uint meas_sqno, meas_time, conv_time/*, ret, done*/;
			meas_sqno = (UInt32)D.MAKELONG(md1[3],md1[2],md1[1],md1[0]);
			meas_time = (UInt32)D.MAKEWORD(md1[5],md1[4]);
			conv_time = (UInt32)D.MAKEWORD(md1[7],md1[6]);
			dat = new MES_DAT();
			//---
			dat.L_L = BitConverter.ToSingle(md1,  8);
			dat.L_P = BitConverter.ToSingle(md1, 12);
			//---
			dat.R_L = BitConverter.ToSingle(md1, 16);
			dat.R_P = BitConverter.ToSingle(md1, 20);
			//---
			dat.B_L = BitConverter.ToSingle(md1, 24);
			dat.B_P = BitConverter.ToSingle(md1, 28);
			//---
			dat.C_R = BitConverter.ToSingle(md1, 32);
			dat.C_G = BitConverter.ToSingle(md1, 36);
			dat.C_B = BitConverter.ToSingle(md1, 40);
			dat.C_C = BitConverter.ToSingle(md1, 44);
			//---
			dat.S_P = BitConverter.ToSingle(md1, 48);
			dat.S_T = BitConverter.ToSingle(md1, 52);
			dat.S_H = BitConverter.ToSingle(md1, 56);
			//---
			dat.A_X = BitConverter.ToSingle(md2,  8);
			dat.A_Y = BitConverter.ToSingle(md2, 12);
			dat.A_Z = BitConverter.ToSingle(md2, 16);
			dat.G_X = BitConverter.ToSingle(md2, 20);
			dat.G_Y = BitConverter.ToSingle(md2, 24);
			dat.G_Z = BitConverter.ToSingle(md2, 28);
			dat.D_C[0] = D.MAKEWORD(md2[33], md2[32])/10.0;
			dat.D_C[1] = D.MAKEWORD(md2[35], md2[34])/10.0;
			dat.D_C[2] = D.MAKEWORD(md2[37], md2[36])/10.0;
			//reserved:2byte
			dat.D_V[0] = BitConverter.ToSingle(md2, 40);
			dat.D_V[1] = BitConverter.ToSingle(md2, 44);
			dat.D_V[2] = BitConverter.ToSingle(md2, 48);
			dat.D_F[0] = BitConverter.ToSingle(md2, 52);
			dat.D_F[1] = BitConverter.ToSingle(md2, 56);
			dat.D_F[2] = BitConverter.ToSingle(md2, 60);
			//for (int i = 38; i < 60; i++) {
			//    dat.D_V[0] = BitConverter.ToSingle(md2, i);
			//}
			dat.TES = meas_time;
			dat.IDX = meas_sqno;
		}
#if false
		static public void GET_TM(byte[]buf, int c_idx, ref double T)
		{
			uint t1, t2, ts;
			t1 = (UInt32)D.MAKELONG(buf[3],buf[2],buf[1],buf[0]);
			t2 = (UInt32)D.MAKELONG(buf[7],buf[6],buf[5],buf[4]);
			ts = (t2-t1);
			T = ts/20.0;
		}

		static public void GET_38(byte[]buf, int c_idx, ref double L, ref double P)
		{
			ushort	rawps;
			ushort[] rawals = {0,0};
			byte[] als_gain_table = {1, 2, 64, 128};
			ushort[] als_meas_time_table = {0,0,0,0,0,100,100,100,100,100,400,400,50,0,0,0};
			int id;
			rawps     = (ushort)(((ushort)buf[1] << 8) | buf[0]);
			rawals[0] = (ushort)(((ushort)buf[3] << 8) | buf[2]);
			rawals[1] = (ushort)(((ushort)buf[5] << 8) | buf[4]);
			//---
			switch (c_idx) {
				case 13:id = /*左*/0; break;
				case 15:id = /*右*/1; break;
				default:id = /*奥*/2; break;
			}
			int index, gain0, gain1,mestm;
			//---
			index = G.SP.D38_424_ALG1[id];
			gain0 =  als_gain_table[index];
			//---
			index = G.SP.D38_422_ALG2[id];
			gain1 =  als_gain_table[index];
			//---
			index = G.SP.D38_410_MSTM[id];
			mestm = als_meas_time_table[index];
			//---
			P = rawps;
			L = S.D38_CONVERT_LX(rawals, gain0, gain1, mestm);
			L = L * G.SP.SEN_COF_GRD[c_idx + 0] + G.SP.SEN_COF_OFS[c_idx+0];
			P = P * G.SP.SEN_COF_GRD[c_idx + 1] + G.SP.SEN_COF_OFS[c_idx+1];
		}
		static public void GET_29(byte[]buf, int c_idx, ref double CR, ref double CG, ref double CB, ref double CC)
		{
			CC = D.MAKEWORD(buf[1], buf[0]);
			CR = D.MAKEWORD(buf[3], buf[2]);
			CG = D.MAKEWORD(buf[5], buf[4]);
			CB = D.MAKEWORD(buf[7], buf[6]);
			//---
			CR = CR * G.SP.SEN_COF_GRD[c_idx+0] + G.SP.SEN_COF_OFS[c_idx+0];
			CG = CG * G.SP.SEN_COF_GRD[c_idx+1] + G.SP.SEN_COF_OFS[c_idx+1];
			CB = CB * G.SP.SEN_COF_GRD[c_idx+2] + G.SP.SEN_COF_OFS[c_idx+2];
			CC = CC * G.SP.SEN_COF_GRD[c_idx+3] + G.SP.SEN_COF_OFS[c_idx+3];
		}
		static public void GET_76(byte[]buf, int c_idx, ref double P, ref double T, ref double H)
		{
			int	temp_r, pres_r, humi_r;
			pres_r = (buf[0] << 12) | (buf[1] << 4) | (buf[2] >> 4);
			temp_r = (buf[3] << 12) | (buf[4] << 4) | (buf[5] >> 4);
			humi_r = (buf[6] << 8)  |  buf[7];
			//---
			float t_fine;
			T = S.D76_COMPENSATE_T(temp_r, out t_fine);
			P = S.D76_COMPENSATE_P(pres_r, t_fine);
			H = S.D76_COMPENSATE_H(humi_r, t_fine);
			//---
			T = T * G.SP.SEN_COF_GRD[c_idx+0] + G.SP.SEN_COF_OFS[c_idx+0];
			H = H * G.SP.SEN_COF_GRD[c_idx+1] + G.SP.SEN_COF_OFS[c_idx+1];
			P = P * G.SP.SEN_COF_GRD[c_idx+2] + G.SP.SEN_COF_OFS[c_idx+2];
		}
		static public void GET_68(byte[]buf, int c_idx, ref double AX, ref double AY, ref double AZ, ref double GX, ref double GY, ref double GZ)
		{
			double ares, gres;
			ares = S.D68_GET_ARES(G.SP.D68_1C3_ASCL);
			gres = S.D68_GET_GRES(G.SP.D68_1B3_GSCL);

			AX = (Int16)D.MAKEWORD(buf[ 0], buf[ 1]);
			AY = (Int16)D.MAKEWORD(buf[ 2], buf[ 3]);
			AZ = (Int16)D.MAKEWORD(buf[ 4], buf[ 5]);
			//---
			GX = (Int16)D.MAKEWORD(buf[ 8], buf[ 9]);
			GY = (Int16)D.MAKEWORD(buf[10], buf[11]);
			GZ = (Int16)D.MAKEWORD(buf[12], buf[13]);
			//---
			AX *= ares;
			AY *= ares;
			AZ *= ares;
			GX *= gres;
			GY *= gres;
			GZ *= gres;
			//---
			AX = AX * G.SP.SEN_COF_GRD[c_idx+0] + G.SP.SEN_COF_OFS[c_idx+0];
			AY = AY * G.SP.SEN_COF_GRD[c_idx+1] + G.SP.SEN_COF_OFS[c_idx+1];
			AZ = AZ * G.SP.SEN_COF_GRD[c_idx+2] + G.SP.SEN_COF_OFS[c_idx+2];
			GX = GX * G.SP.SEN_COF_GRD[c_idx+3] + G.SP.SEN_COF_OFS[c_idx+3];
			GY = GY * G.SP.SEN_COF_GRD[c_idx+4] + G.SP.SEN_COF_OFS[c_idx+4];
			GZ = GZ * G.SP.SEN_COF_GRD[c_idx+5] + G.SP.SEN_COF_OFS[c_idx+5];
		}
		static public void GET_FF(byte[]buf, int c_idx, ref double[] CNTS, ref double[] VOLS, ref double[] VALS)
		{
//			const
//			int MAX_OF_CH = 3;
			//double[] cnts = new double[MAX_OF_CH];
			//double[] vols = new double[MAX_OF_CH];
			//double[] vals = new double[MAX_OF_CH];
			for (int i = 0; i < 3; i++) {
				int j = i*2;
				CNTS[i] = D.MAKEWORD(buf[j+1], buf[j+0]);
			}
			for (int i = 0; i < 3; i++) {
				CNTS[i] /= 10.0;
				VOLS[i] = CNTS[i] / (double)CNTS[0] * 2.5;
			}
			for (int i = 1; i < 3; i++) {
				VOLS[i] = VOLS[i] * G.SP.ADC_COF_GRD[i] + G.SP.ADC_COF_OFS[i];
			}
			for (int i = 1; i < 3; i++) {
				VALS[0] = VOLS[0];
				VALS[1] = VOLS[1] * G.SP.SEN_COF_GRD[20] + G.SP.SEN_COF_OFS[20];
				VALS[2] = VOLS[2] * G.SP.SEN_COF_GRD[21] + G.SP.SEN_COF_OFS[21];
			}
			//---
			//Z = cnts[0];
			//P = vals[1];
			//B = vals[2];
		}
		static public float D38_CONVERT_LX(ushort[] data, int data0_gain, int data1_gain, int meas_time)
		{
			const int RPR0521RS_ERROR =(-1);
			float lx;
			float d0, d1, d1_d0;

			if (data0_gain == 0) {
				return (RPR0521RS_ERROR);
			}
			if (data1_gain == 0) {
				return (RPR0521RS_ERROR);
			}

			if (meas_time == 0) {
				return (RPR0521RS_ERROR);
			}
			else if (meas_time == 50) {
				if ((data[0] & 0x8000) == 0x8000) {
					data[0] = 0x7FFF;
				}
				if ((data[1] & 0x8000) == 0x8000) {
					data[1] = 0x7FFF;
				}
			}

			d0 = (float)data[0] * (100 / meas_time) / data0_gain;
			d1 = (float)data[1] * (100 / meas_time) / data1_gain;

			if (d0 == 0) {
				lx = 0;
				return (lx);
			}

			d1_d0 = d1 / d0;

			if (d1_d0 < 0.595f) {
				lx = (1.682f * d0 - 1.877f * d1);
			}
			else if (d1_d0 < 1.015f) {
				lx = (0.644f * d0 - 0.132f * d1);
			}
			else if (d1_d0 < 1.352f) {
				lx = (0.756f * d0 - 0.243f * d1);
			}
			else if (d1_d0 < 3.053f) {
				lx = (0.766f * d0 - 0.250f * d1);
			}
			else {
				lx = 0;
			}
			return (lx);
		}
		static public float D68_GET_GRES(int/*GSCALE*/ g_s)
		{
			float res = float.NaN;
			switch ((D68_MPU9250.GSCALE)g_s)
			{
			// Possible gyro scales (and their register bit settings) are:
			// 250 DPS (00), 500 DPS (01), 1000 DPS (10), and 2000 DPS (11).
			// Here's a bit of an algorith to calculate DPS/(ADC tick) based on that
			// 2-bit value:
			case D68_MPU9250.GSCALE.GFS_250DPS:
				res = 250.0f / 32768.0f;
			break;
			case D68_MPU9250.GSCALE.GFS_500DPS:
				res = 500.0f / 32768.0f;
			break;
			case D68_MPU9250.GSCALE.GFS_1000DPS:
				res = 1000.0f / 32768.0f;
			break;
			case D68_MPU9250.GSCALE.GFS_2000DPS:
				res = 2000.0f / 32768.0f;
			break;
			}
			return(res);
		}
		static public float D68_GET_ARES(int/*ASCALE*/ a_s)
		{
			float res = float.NaN;
			switch ((D68_MPU9250.ASCALE)a_s)
			{
			// Possible accelerometer scales (and their register bit settings) are:
			// 2 Gs (00), 4 Gs (01), 8 Gs (10), and 16 Gs  (11).
			// Here's a bit of an algorith to calculate DPS/(ADC tick) based on that
			// 2-bit value:
			case D68_MPU9250.ASCALE.AFS_2G:
				res = 2.0f / 32768.0f;
				break;
			case D68_MPU9250.ASCALE.AFS_4G:
				res = 4.0f / 32768.0f;
				break;
			case D68_MPU9250.ASCALE.AFS_8G:
				res = 8.0f / 32768.0f;
				break;
			case D68_MPU9250.ASCALE.AFS_16G:
				res = 16.0f / 32768.0f;
				break;
			}
			return(res);
		}
		static public float D76_COMPENSATE_T(int adc_T, out float t_fine)
		{
			float v1, v2, temperature;

			v1 = (adc_T / 16384.0f - G.SS.D76_CAL_DIGT[0] / 1024.0f) * G.SS.D76_CAL_DIGT[1];
			v2 = (adc_T / 131072.0f - G.SS.D76_CAL_DIGT[0] / 8192.0f) * (adc_T / 131072.0f - G.SS.D76_CAL_DIGT[0] / 8192.0f) * G.SS.D76_CAL_DIGT[2];
			t_fine = v1 + v2;
			temperature = t_fine / 5120.0f;
	
			//print "temp : %-6.2f ℃" % (temperature) 

			return(temperature);
		}
		static public float D76_COMPENSATE_H(int adc_H, float t_fine)
		{
			float var_h;

			var_h = t_fine - 76800.0f;
			if (var_h != 0) {
				var_h = (adc_H - (G.SS.D76_CAL_DIGH[3] * 64.0f + G.SS.D76_CAL_DIGH[4]/16384.0f * var_h))
					  * (G.SS.D76_CAL_DIGH[1] / 65536.0f
					  * (1.0f + G.SS.D76_CAL_DIGH[5] / 67108864.0f * var_h * (1.0f + G.SS.D76_CAL_DIGH[2] / 67108864.0f * var_h)));
			}
			else {
				return 0;
			}
			var_h = var_h * (1.0f - G.SS.D76_CAL_DIGH[0] * var_h / 524288.0f);
			if (var_h > 100.0f) {
				var_h = 100.0f;
			}
			else if (var_h < 0.0f) {
				var_h = 0.0f;
			}
			//print "hum : %6.2f ％" % (var_h)

			return(var_h);
		}
		static public float D76_COMPENSATE_P(int adc_P, float t_fine)
		{
			float pressure, v1, v2;

			pressure = 0.0f;
	
			v1 = (t_fine / 2.0f) - 64000.0f;
			v2 = (((v1 / 4.0f) * (v1 / 4.0f)) / 2048) * G.SS.D76_CAL_DIGP[5];
			v2 = v2 + ((v1 * G.SS.D76_CAL_DIGP[4]) * 2.0f);
			v2 = (v2 / 4.0f) + (G.SS.D76_CAL_DIGP[3] * 65536.0f);
			v1 = (((G.SS.D76_CAL_DIGP[2] * (((v1 / 4.0f) * (v1 / 4.0f)) / 8192)) / 8)  + ((G.SS.D76_CAL_DIGP[1] * v1) / 2.0f)) / 262144;
			v1 = ((32768f + v1) * G.SS.D76_CAL_DIGP[0]) / 32768f;
	
			if (v1 == 0) {
				return 0;
			}
			pressure = ((1048576 - adc_P) - (v2 / 4096)) * 3125;
			if (pressure < 0x80000000) {
				pressure = (pressure * 2.0f) / v1;
			}
			else {
				pressure = (pressure / v1) * 2;
			}
			v1 = (G.SS.D76_CAL_DIGP[8] * (((pressure / 8.0f) * (pressure / 8.0f)) / 8192.0f)) / 4096;
			v2 = ((pressure / 4.0f) * G.SS.D76_CAL_DIGP[7]) / 8192.0f;
			pressure = pressure + ((v1 + v2 + G.SS.D76_CAL_DIGP[6]) / 16.0f);

//			print "pressure : %7.2f hPa" % (pressure/100)
			return(pressure/100);
		}
#endif
	}
}
#if false
--------
CRT V0.0
-MONITOR
--------

FREE.RUN MODE (update rate:1/100ms)
--------  --------  --------  --------  --------
F  [cnt]  F  [d/s]  F  [deg]  F  [cnt]  F    [N]
C.R-0.99  GX_-2.17  TMP 24.1  P.L 9999  PRS 3.33
--------  --------  --------  --------  --------
↓
PS検知でLED点灯
↓
--------
LIGHT
C.R-0.99
--------
↓
圧力検知で測定準備
↓
--------
PRESS
C.R-0.99
--------
↓
圧力解放で測定スタート
↓
--------
TIME 1.1
C.R-0.99
--------

typedef struct {
	int SEQNO_OF_MEASURE;
	int	SEQNO_OF_CPUTIME;
	int	CTIME_OF_MEASURE;
	int	CTIME_OF_CONVERT;
	int	CTIME_OF_XXX;
	int	STAT_OF_APP;
} PROG_STAT;

#endif
