using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//---
using System.Windows.Forms;

namespace CRTMONITOR
{
	class PictureBoxEx:PictureBox
	{
		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
			base.OnPaintBackground(pevent);
			//何もしない
		}
	}
}
