using System;
using System.Windows.Forms;

namespace Frog
{
	public partial class Form1 : Form
	{
		Marsh marsh;
		Frog frog;

		public Form1()
		{
			InitializeComponent();
			marsh = new Marsh();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			button1.Enabled = false;

			// 読み込み
			marsh.LoadFile();
			// 行き先設定
			marsh.SetNextList();
			// 
			frog = new Frog(marsh);

			textBox1.Text += marsh.GetLotusString();

			button2.Enabled = true;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			button2.Enabled = false;

			DateTime startTime = DateTime.Now;

			textBox1.Text += string.Format("探索開始:{0}\r\n", startTime.ToString("hh:mm:ss.ff"));

			int result;
			do {
				result = frog.Move();
			} while (result == 0);

			DateTime endTime = DateTime.Now;

			if (result == 1) {
				textBox1.Text += string.Format("探索完了:{0}\r\n経過時間:{1}\r\n\r\n", endTime.ToString("hh:mm:ss.ff"), (endTime - startTime).ToString());
				textBox1.Text += frog.GetStackString();
			} else if (result == -1) {
				textBox1.Text += string.Format("探索失敗:{0}\r\n経過時間:{1}\r\n", endTime.ToString("hh:mm:ss.ff"), (endTime - startTime).ToString());

			}

		}
	}
}
