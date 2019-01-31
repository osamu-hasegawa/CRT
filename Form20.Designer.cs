namespace CRTMONITOR
{
	partial class Form20
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.label27 = new System.Windows.Forms.Label();
			this.radioButton7 = new System.Windows.Forms.RadioButton();
			this.radioButton6 = new System.Windows.Forms.RadioButton();
			this.label55 = new System.Windows.Forms.Label();
			this.label53 = new System.Windows.Forms.Label();
			this.label49 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label57 = new System.Windows.Forms.Label();
			this.label52 = new System.Windows.Forms.Label();
			this.label50 = new System.Windows.Forms.Label();
			this.label56 = new System.Windows.Forms.Label();
			this.label54 = new System.Windows.Forms.Label();
			this.label46 = new System.Windows.Forms.Label();
			this.radioButton5 = new System.Windows.Forms.RadioButton();
			this.label1 = new System.Windows.Forms.Label();
			this.label48 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label47 = new System.Windows.Forms.Label();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
			this.groupBox4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
			this.SuspendLayout();
			// 
			// button2
			// 
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.Location = new System.Drawing.Point(161, 476);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 26);
			this.button2.TabIndex = 1;
			this.button2.Tag = "1";
			this.button2.Text = "キャンセル";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// button1
			// 
			this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.button1.Location = new System.Drawing.Point(66, 476);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 26);
			this.button1.TabIndex = 0;
			this.button1.Tag = "0";
			this.button1.Text = "OK";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.numericUpDown3);
			this.groupBox4.Controls.Add(this.numericUpDown2);
			this.groupBox4.Controls.Add(this.numericUpDown1);
			this.groupBox4.Controls.Add(this.label27);
			this.groupBox4.Controls.Add(this.radioButton7);
			this.groupBox4.Controls.Add(this.radioButton6);
			this.groupBox4.Controls.Add(this.label55);
			this.groupBox4.Controls.Add(this.label53);
			this.groupBox4.Controls.Add(this.label47);
			this.groupBox4.Controls.Add(this.label49);
			this.groupBox4.Controls.Add(this.label48);
			this.groupBox4.Controls.Add(this.label3);
			this.groupBox4.Controls.Add(this.label1);
			this.groupBox4.Controls.Add(this.label12);
			this.groupBox4.Controls.Add(this.label57);
			this.groupBox4.Controls.Add(this.label52);
			this.groupBox4.Controls.Add(this.label50);
			this.groupBox4.Controls.Add(this.label56);
			this.groupBox4.Controls.Add(this.label54);
			this.groupBox4.Controls.Add(this.label46);
			this.groupBox4.Controls.Add(this.radioButton5);
			this.groupBox4.Location = new System.Drawing.Point(12, 12);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(273, 450);
			this.groupBox4.TabIndex = 44;
			this.groupBox4.TabStop = false;
			// 
			// label27
			// 
			this.label27.AutoSize = true;
			this.label27.Location = new System.Drawing.Point(6, 16);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(65, 12);
			this.label27.TabIndex = 1;
			this.label27.Text = "測定時条件";
			// 
			// radioButton7
			// 
			this.radioButton7.Location = new System.Drawing.Point(16, 253);
			this.radioButton7.Name = "radioButton7";
			this.radioButton7.Size = new System.Drawing.Size(244, 32);
			this.radioButton7.TabIndex = 22;
			this.radioButton7.TabStop = true;
			this.radioButton7.Text = "無圧力時のセンサー値に戻るまでの時間";
			this.radioButton7.UseVisualStyleBackColor = true;
			// 
			// radioButton6
			// 
			this.radioButton6.Location = new System.Drawing.Point(16, 148);
			this.radioButton6.Name = "radioButton6";
			this.radioButton6.Size = new System.Drawing.Size(244, 32);
			this.radioButton6.TabIndex = 22;
			this.radioButton6.TabStop = true;
			this.radioButton6.Text = "センサー値の変化率が指定された値になるまでの時間を測定";
			this.radioButton6.UseVisualStyleBackColor = true;
			// 
			// label55
			// 
			this.label55.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label55.Location = new System.Drawing.Point(59, 324);
			this.label55.Name = "label55";
			this.label55.Size = new System.Drawing.Size(160, 26);
			this.label55.TabIndex = 1;
			this.label55.Text = "Rb=(Sn/Sa-1)*100 [%]";
			this.label55.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label53
			// 
			this.label53.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label53.Location = new System.Drawing.Point(59, 214);
			this.label53.Name = "label53";
			this.label53.Size = new System.Drawing.Size(160, 26);
			this.label53.TabIndex = 1;
			this.label53.Text = "Rc=|Sn-Sz|/(Sb-Sa)*100 [%]";
			this.label53.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label49
			// 
			this.label49.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label49.Location = new System.Drawing.Point(59, 108);
			this.label49.Name = "label49";
			this.label49.Size = new System.Drawing.Size(160, 26);
			this.label49.TabIndex = 1;
			this.label49.Text = "Rd=(Sn-Sa)/(Sb-Sa)*100 [%]";
			this.label49.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label12
			// 
			this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label12.Location = new System.Drawing.Point(8, 32);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(255, 2);
			this.label12.TabIndex = 24;
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label57
			// 
			this.label57.AutoSize = true;
			this.label57.Location = new System.Drawing.Point(61, 292);
			this.label57.Name = "label57";
			this.label57.Size = new System.Drawing.Size(21, 12);
			this.label57.TabIndex = 1;
			this.label57.Text = "Rb:";
			// 
			// label52
			// 
			this.label52.AutoSize = true;
			this.label52.Location = new System.Drawing.Point(61, 186);
			this.label52.Name = "label52";
			this.label52.Size = new System.Drawing.Size(21, 12);
			this.label52.TabIndex = 1;
			this.label52.Text = "Rc:";
			// 
			// label50
			// 
			this.label50.AutoSize = true;
			this.label50.Location = new System.Drawing.Point(61, 82);
			this.label50.Name = "label50";
			this.label50.Size = new System.Drawing.Size(21, 12);
			this.label50.TabIndex = 1;
			this.label50.Text = "Rd:";
			// 
			// label56
			// 
			this.label56.AutoSize = true;
			this.label56.Location = new System.Drawing.Point(137, 292);
			this.label56.Name = "label56";
			this.label56.Size = new System.Drawing.Size(124, 12);
			this.label56.TabIndex = 1;
			this.label56.Text = "[%]以下になるまでの時間";
			// 
			// label54
			// 
			this.label54.AutoSize = true;
			this.label54.Location = new System.Drawing.Point(137, 186);
			this.label54.Name = "label54";
			this.label54.Size = new System.Drawing.Size(124, 12);
			this.label54.TabIndex = 1;
			this.label54.Text = "[%]以下になるまでの時間";
			// 
			// label46
			// 
			this.label46.AutoSize = true;
			this.label46.Location = new System.Drawing.Point(137, 83);
			this.label46.Name = "label46";
			this.label46.Size = new System.Drawing.Size(124, 12);
			this.label46.TabIndex = 1;
			this.label46.Text = "[%]以下になるまでの時間";
			// 
			// radioButton5
			// 
			this.radioButton5.Location = new System.Drawing.Point(16, 38);
			this.radioButton5.Name = "radioButton5";
			this.radioButton5.Size = new System.Drawing.Size(244, 47);
			this.radioButton5.TabIndex = 22;
			this.radioButton5.TabStop = true;
			this.radioButton5.Text = "センサー値がロック時のピーク値から指定された割合に減衰するまでの時間を測定";
			this.radioButton5.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label1.Location = new System.Drawing.Point(8, 364);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(255, 2);
			this.label1.TabIndex = 24;
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label48
			// 
			this.label48.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label48.Location = new System.Drawing.Point(8, 375);
			this.label48.Name = "label48";
			this.label48.Size = new System.Drawing.Size(121, 29);
			this.label48.TabIndex = 1;
			this.label48.Text = "Sn:現在のセンサー値\r\nSz:ひとつ前のセンサー値\r\n";
			// 
			// label3
			// 
			this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label3.Location = new System.Drawing.Point(139, 375);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(121, 29);
			this.label3.TabIndex = 1;
			this.label3.Text = "Sa:無圧力時の直近のセンサー値";
			// 
			// label47
			// 
			this.label47.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label47.Location = new System.Drawing.Point(8, 411);
			this.label47.Name = "label47";
			this.label47.Size = new System.Drawing.Size(121, 29);
			this.label47.TabIndex = 1;
			this.label47.Text = "Sb:ロック時のセンサー値の最大値";
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Location = new System.Drawing.Point(86, 79);
			this.numericUpDown1.Maximum = new decimal(new int[] {
            95,
            0,
            0,
            0});
			this.numericUpDown1.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(48, 19);
			this.numericUpDown1.TabIndex = 25;
			this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numericUpDown1.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			// 
			// numericUpDown2
			// 
			this.numericUpDown2.Location = new System.Drawing.Point(86, 183);
			this.numericUpDown2.Maximum = new decimal(new int[] {
            95,
            0,
            0,
            0});
			this.numericUpDown2.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.numericUpDown2.Name = "numericUpDown2";
			this.numericUpDown2.Size = new System.Drawing.Size(48, 19);
			this.numericUpDown2.TabIndex = 25;
			this.numericUpDown2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numericUpDown2.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			// 
			// numericUpDown3
			// 
			this.numericUpDown3.Location = new System.Drawing.Point(86, 289);
			this.numericUpDown3.Maximum = new decimal(new int[] {
            95,
            0,
            0,
            0});
			this.numericUpDown3.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.numericUpDown3.Name = "numericUpDown3";
			this.numericUpDown3.Size = new System.Drawing.Size(48, 19);
			this.numericUpDown3.TabIndex = 25;
			this.numericUpDown3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numericUpDown3.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			// 
			// Form20
			// 
			this.AcceptButton = this.button1;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.LightSteelBlue;
			this.CancelButton = this.button2;
			this.ClientSize = new System.Drawing.Size(299, 511);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Form20";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "設定";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form20_FormClosing);
			this.Load += new System.EventHandler(this.Form20_Load);
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Label label27;
		private System.Windows.Forms.RadioButton radioButton7;
		private System.Windows.Forms.RadioButton radioButton6;
		private System.Windows.Forms.Label label55;
		private System.Windows.Forms.Label label53;
		private System.Windows.Forms.Label label49;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label57;
		private System.Windows.Forms.Label label52;
		private System.Windows.Forms.Label label50;
		private System.Windows.Forms.Label label56;
		private System.Windows.Forms.Label label54;
		private System.Windows.Forms.Label label46;
		private System.Windows.Forms.RadioButton radioButton5;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label48;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label47;
		private System.Windows.Forms.NumericUpDown numericUpDown3;
		private System.Windows.Forms.NumericUpDown numericUpDown2;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
	}
}