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
	public partial class Form20 : Form
	{
		public G.SENPAR	m_sp;

		public Form20()
		{
			InitializeComponent();
		}

		private void Form20_Load(object sender, EventArgs e)
		{
			//
			//
			DDX(true);
		}

		private void Form20_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.DialogResult != DialogResult.OK) {
				return;
			}
			if (DDX(false) == false) {
				e.Cancel = true;
			}
			if (true) {
				//if (this.comboBox3.Text == "-") {
				//    G.mlog("有効な測定モードを選択してください.");
				//    this.comboBox3.Focus();
				//    e.Cancel = true;
				//    return;
				//}
				//G.SS = (G.SYSSET)m_ss.Clone();
			}
		}
		private bool DDX(bool bUpdate)
        {
            bool rc;
			try {
				//---
				DDV.DDX(bUpdate, new RadioButton[] {this.radioButton5, this.radioButton6, this.radioButton7}
													, ref m_sp.MES_MES_CND   );
				DDV.DDX(bUpdate, this.numericUpDown1, ref m_sp.MES_MES_VRD);				//---
				DDV.DDX(bUpdate, this.numericUpDown2, ref m_sp.MES_MES_VRC);				//---
				DDV.DDX(bUpdate, this.numericUpDown3, ref m_sp.MES_MES_VRB);				//---

                rc = true;
            }
            catch (Exception e) {
                G.mlog(e.Message);
                rc = false;
            }
            return (rc);
		}
	}
}
