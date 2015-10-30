using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace scissors
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			int x = 2, y = 1, tmp;
			for (int i = 0; i < 40; i++) {
				tmp = x;
				x = x + y;
				y = tmp;
			}
			System.Diagnostics.Debug.WriteLine(string.Format("x={0},y={1}", x, y));
		}
	}
}
