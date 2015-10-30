using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Text;

namespace Maze
{
	public partial class Form1 : Form
	{
		string[,] maze;
		int[,] mazeNum;
		int xMax;
		int yMax;
		Location start;
		Location goal;

		Queue<Location> searchQueue;

		public Form1()
		{
			InitializeComponent();
			searchQueue = new Queue<Location>();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			// 読み込み
			string[] lines = File.ReadAllLines("Maze.txt");
			for (int i = 0; i < lines.Length; i++) {
				xMax = xMax < lines[i].Length ? lines[i].Length : xMax;
			}
			yMax = lines.Length;
			maze = new string[xMax, yMax];
			mazeNum = new int[xMax, yMax];
			for (int y = 0; y < lines.Length; y++) {
				for (int x = 0; x < lines[y].Length; x++) {
					maze[x, y] = lines[y][x].ToString();
					if (maze[x, y] == "S") {
						start = new Location(x, y);
						mazeNum[x, y] = 1;
						searchQueue.Enqueue(start);
					} else if (maze[x, y] == "G") {
						goal = new Location(x, y);
						mazeNum[x, y] = -1;
						searchQueue.Enqueue(goal);
					}
				}
			}
			Print();
			textBox1.Text += "\r\n";
		}

		private void button2_Click(object sender, EventArgs e)
		{
			DateTime startTime = DateTime.Now;
			// 開始
			int[,] offset = { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } };
			bool endFlag = false;
			Location item, r1, r2;
			r1 = null;
			r2 = null;
			int x, y, no;
			while (!endFlag) {
				if (searchQueue.Count == 0) {
					textBox1.Text += "回答無し";
					return;
				}
				item = searchQueue.Dequeue();
				no = mazeNum[item.x, item.y];
				no += no > 0 ? 1 : -1;
				for (int i = 0; i < offset.GetLength(0); i++) {
					x = item.x + offset[i, 0];
					y = item.y + offset[i, 1];
					if (SearchCheck(x, y, no)) {
						r1 = item;
						r2 = new Location(x, y);
						endFlag = true;
						break;
					}
				}
			}
			while (maze[r1.x, r1.y] == " ") {
				maze[r1.x, r1.y] = "$";
				RouteCheck(r1);
			}
			while (maze[r2.x, r2.y] == " ") {
				maze[r2.x, r2.y] = "$";
				RouteCheck(r2);
			}
			DateTime goalTime = DateTime.Now;

			Print();
			textBox1.Text += string.Format("{0:ss.fffffff} {1}", goalTime - startTime, (goalTime - startTime).Ticks);
		}

		private void RouteCheck(Location loc)
		{
			int x, y, no;
			no = mazeNum[loc.x, loc.y];
			no += no > 0 ? -1 : 1;

			int[,] offset = { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } };
			for (int i = 0; i < offset.GetLength(0); i++) {
				x = loc.x + offset[i, 0];
				y = loc.y + offset[i, 1];
				if (0 <= x && x < xMax && 0 <= y && y < yMax) {
					if (mazeNum[x, y] == no) {
						loc.x = x;
						loc.y = y;
						break;
					}
				}
			}
		}

		private bool SearchCheck(int x, int y, int no)
		{
			if (0 <= x && x < xMax && 0 <= y && y < yMax) {
				if ((mazeNum[x, y] > 0 && no < 0) || (mazeNum[x, y] < 0 && no > 0)) {
					// 発見
					return true;
				} else if (mazeNum[x, y] == 0 && maze[x, y] == " ") {
					mazeNum[x, y] = no;
					searchQueue.Enqueue(new Location(x, y));
				}
			}
			return false;
		}

		private void Print()
		{
			StringBuilder sb = new StringBuilder();
			for (int y = 0; y < yMax; y++) {
				for (int x = 0; x < xMax; x++) {
					sb.Append(maze[x, y]);
				}
				sb.AppendLine();
			}
			textBox1.Text += sb.ToString();
		}

	}

	public class Location
	{
		public int x;
		public int y;
		public Location(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
	}

}
