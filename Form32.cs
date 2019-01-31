using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CRTMONITOR
{
	public partial class Form32 : Form
	{
		public G.SENPAR	m_sp;
		public G.SYSSET	m_ss;
		//
		public Form32()
		{
			InitializeComponent();
		}

		private void Form32_Load(object sender, EventArgs e)
		{
			//
			//
			DDX(true);
		}

		private bool DDX(bool bUpdate)
        {
            bool rc=false;
			//
			try {
				DDV.DDX(bUpdate, this.checkBox1     , ref m_sp.LED_LGT_CHK   );
//				DDV.DDX(bUpdate, new RadioButton[] {this.radioButton1, this.radioButton2}
//													, ref m_sp.LED_LGT_OPT   );
				DDV.DDX(bUpdate, this.comboBox20    , ref m_sp.LED_LGT_OPT   );
				//---
				DDV.DDX(bUpdate, this.comboBox1     , ref m_sp.LED_LGT_SEN[0]);
				DDV.DDX(bUpdate, this.comboBox3     , ref m_sp.LED_LGT_SEN[1]);
				DDV.DDX(bUpdate, this.comboBox5     , ref m_sp.LED_LGT_SEN[2]);
				//---
				DDV.DDX(bUpdate, this.textBox1      , ref m_sp.LED_LGT_VAL[0]);
				DDV.DDX(bUpdate, this.textBox2      , ref m_sp.LED_LGT_VAL[1]);
				DDV.DDX(bUpdate, this.textBox3      , ref m_sp.LED_LGT_VAL[2]);
				//---
				DDV.DDX(bUpdate, this.comboBox2     , ref m_sp.LED_LGT_CND[0]);
				DDV.DDX(bUpdate, this.comboBox4     , ref m_sp.LED_LGT_CND[1]);
				DDV.DDX(bUpdate, this.comboBox6     , ref m_sp.LED_LGT_CND[2]);
				//---
				DDV.DDX(bUpdate, this.checkBox2     , ref m_sp.LED_OFF_CHK   );
//				DDV.DDX(bUpdate, new RadioButton[] {this.radioButton3, this.radioButton4}
//													, ref m_sp.LED_OFF_OPT   );
				DDV.DDX(bUpdate, this.comboBox21    , ref m_sp.LED_OFF_OPT   );
				//---
				DDV.DDX(bUpdate, this.comboBox7     , ref m_sp.LED_OFF_SEN[0]);
				DDV.DDX(bUpdate, this.comboBox9     , ref m_sp.LED_OFF_SEN[1]);
				DDV.DDX(bUpdate, this.comboBox11    , ref m_sp.LED_OFF_SEN[2]);
				//---
				DDV.DDX(bUpdate, this.textBox4      , ref m_sp.LED_OFF_VAL[0]);
				DDV.DDX(bUpdate, this.textBox5      , ref m_sp.LED_OFF_VAL[1]);
				DDV.DDX(bUpdate, this.textBox6      , ref m_sp.LED_OFF_VAL[2]);
				//---
				DDV.DDX(bUpdate, this.comboBox8     , ref m_sp.LED_OFF_CND[0]);
				DDV.DDX(bUpdate, this.comboBox10    , ref m_sp.LED_OFF_CND[1]);
				DDV.DDX(bUpdate, this.comboBox12    , ref m_sp.LED_OFF_CND[2]);
				//---
				DDV.DDX(bUpdate, this.comboBox13    , ref m_sp.LCD_FRE_TOP);
				DDV.DDX(bUpdate, this.comboBox14    , ref m_sp.LCD_FRE_BTM);
				DDV.DDX(bUpdate, this.textBox7      , ref m_sp.LCD_FRE_STP, 8);
				DDV.DDX(bUpdate, this.textBox8      , ref m_sp.LCD_FRE_SBT, 8);
				DDV.DDX(bUpdate, this.comboBox15    , ref m_sp.LCD_LCK_TOP);
				DDV.DDX(bUpdate, this.comboBox16    , ref m_sp.LCD_LCK_BTM);
				DDV.DDX(bUpdate, this.textBox9      , ref m_sp.LCD_LCK_STP, 8);
				DDV.DDX(bUpdate, this.textBox10     , ref m_sp.LCD_LCK_SBT, 8);
				DDV.DDX(bUpdate, this.comboBox17    , ref m_sp.LCD_MES_TOP);
				DDV.DDX(bUpdate, this.comboBox18    , ref m_sp.LCD_MES_BTM);
				DDV.DDX(bUpdate, this.textBox11     , ref m_sp.LCD_MES_STP, 8);
				DDV.DDX(bUpdate, this.textBox12     , ref m_sp.LCD_MES_SBT, 8);
				//---
				DDV.DDX(bUpdate, this.comboBox19    , ref m_sp.MES_USE_SEN);
				DDV.DDX(bUpdate, this.textBox13     , ref m_sp.MES_FRE_PRS);				//---
				DDV.DDX(bUpdate, this.textBox14     , ref m_sp.MES_LCK_PRS);				//---
				DDV.DDX(bUpdate, this.textBox15     , ref m_sp.MES_MES_PRS);				//---
				DDV.DDX(bUpdate, this.textBox16     , ref m_sp.MES_LCK_TOT);
	
				//---
				DDV.DDX(bUpdate, new RadioButton[] {this.radioButton5, this.radioButton6, this.radioButton7}
													, ref m_sp.MES_MES_CND   );
				DDV.DDX(bUpdate, this.textBox17     , ref m_sp.MES_MES_VRD);				//---
				DDV.DDX(bUpdate, this.textBox18     , ref m_sp.MES_MES_VRC);				//---
				DDV.DDX(bUpdate, this.textBox19     , ref m_sp.MES_MES_VRB);				//---
				DDV.DDX(bUpdate, this.textBox20     , ref m_sp.MES_MES_WAI);				//---
				DDV.DDX(bUpdate, this.textBox21     , ref m_sp.MES_MES_TOT);				//---
				//---
				DDV.DDX(bUpdate, this.textBox22     , ref m_sp.BAT_WRN_VOL);				//---
				DDV.DDX(bUpdate, this.textBox23     , ref m_sp.BAT_HLT_VOL);				//---
				//---
				DDV.DDX(bUpdate, this.comboBox22    , ref m_ss.ETC_UIF_LEVL);
				//---
				if (bUpdate) {
				DDV.DDX(bUpdate, this.textBox24     , ref m_sp.ROM_DAT_STA);				//---
				DDV.DDX(bUpdate, this.textBox25     , ref m_sp.ROM_DAT_END);				//---
				}
				//---
                rc = true;
            }
            catch (Exception e)
            {
                G.mlog(e.Message);
                rc = false;
            }
            return (rc);
		}

		private void Form32_Validating(object sender, CancelEventArgs e)
		{
			if (DDX(false) == false) {
				e.Cancel = true;
			}
		}

	}
}
