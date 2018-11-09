using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Frog
{
	class Marsh
	{
		public List<Leaf> lotus;
		public Leaf start;
		public Leaf goal;
		public int xSize;
		public int ySize;
		public int leafCount;

		public Marsh()
		{
			lotus = new List<Leaf>();
		}

		public void LoadFile()
		{
			string path = "doc\\NAZO.txt";

			string[] lines = File.ReadAllLines(path);
			int num = 1, x = 0, y = 0;
			bool first = true;
			foreach (string str in lines) {
				string[] cells = str.Split(new char[] { ',' });
				if (first) {
					xSize = cells.Length;
					ySize = lines.Length;
					first = false;
				}
				foreach (string cell in cells) {
					if (!string.IsNullOrEmpty(cell)) {
						Leaf leaf = new Leaf(x, y);
						if (cell == "S") {
							leaf.Num = "S";
							leaf.Range = 1;
							leaf.Cross = false;
							leafCount++;
							start = leaf;
						} else if (cell == "G") {
							leaf.Num = "G";
							leaf.Range = 0;
							leaf.Cross = true;
							leafCount++;
							goal = leaf;
						} else if (cell == "x") {
							leaf.Num = "x";
							leaf.Range = 0;
							leaf.Cross = false;
						} else {
							leaf.Num = num.ToString();
							leaf.Range = int.Parse(cell);
							leaf.Cross = true;
							num++;
							leafCount++;
						}
						lotus.Add(leaf);
						x++;
					}
				}
				x = 0;
				y++;
			}
			//DebugPrint();
		}

		public void SetNextList()
		{
			foreach (Leaf leaf in lotus) {
				if (leaf.Range > 0) {
					// 上
					if (leaf.Y - leaf.Range >= 0) {
						int index = (leaf.Y - leaf.Range) * xSize + leaf.X;
						if (lotus[index].Cross) {
							leaf.NextLeaf.Add(lotus[index]);
						}
					}
					// 下
					if (leaf.Y + leaf.Range < ySize) {
						int index = (leaf.Y + leaf.Range) * xSize + leaf.X;
						if (lotus[index].Cross) {
							leaf.NextLeaf.Add(lotus[index]);
						}
					}
					// 左
					if (leaf.X - leaf.Range >= 0) {
						int index = leaf.Y * xSize + (leaf.X - leaf.Range);
						if (lotus[index].Cross) {
							leaf.NextLeaf.Add(lotus[index]);
						}
					}
					// 右
					if (leaf.X + leaf.Range < xSize) {
						int index = leaf.Y * xSize + (leaf.X + leaf.Range);
						if (lotus[index].Cross) {
							leaf.NextLeaf.Add(lotus[index]);
						}
					}
				}
			}
			//DebugPrint2();
		}

		public bool isComplete()
		{
			foreach (Leaf leaf in lotus) {
				if (leaf.Cross) {
					return false;
				}
			}
			return true;
		}

		public string GetLotusString()
		{
			StringBuilder sb = new StringBuilder();

			for (int i = 0; i < lotus.Count; i++) {
				if (start == lotus[i]) {
					sb.AppendFormat("S{0}", lotus[i].Range);
				} else if (goal == lotus[i]) {
					sb.AppendFormat(" {0}G", lotus[i].Range);
				} else {
					sb.AppendFormat(" {0}", lotus[i].Range);
				}
				if ((i + 1) % xSize == 0) {
					sb.AppendLine();
				}
			}
			sb.AppendLine();
			return sb.ToString();
		}

		public string GetLotusString2()
		{
			StringBuilder sb = new StringBuilder();

			for (int i = 0; i < lotus.Count; i++) {
				sb.AppendFormat(" {0,2}", lotus[i].Num);
				if ((i + 1) % xSize == 0) {
					sb.AppendLine();
				}
			}
			sb.AppendLine();
			return sb.ToString();
		}

		public void DebugPrint()
		{
			System.Diagnostics.Debug.WriteLine(string.Format("Size = {0} X {1} ({2})", xSize, ySize, lotus.Count));
			for (int i = 0; i < lotus.Count; i++) {
				if (start == lotus[i]) {
					System.Diagnostics.Debug.Write(string.Format("S{0}", lotus[i].Range));
				} else if (goal == lotus[i]) {
					System.Diagnostics.Debug.Write(string.Format(" {0}G", lotus[i].Range));
				} else {
					System.Diagnostics.Debug.Write(string.Format(" {0}", lotus[i].Range));
				}
				if ((i + 1) % xSize == 0) {
					System.Diagnostics.Debug.WriteLine("");
				}
			}
			System.Diagnostics.Debug.WriteLine("");
		}

		private void DebugPrint2()
		{
			foreach (Leaf leaf in lotus) {
				if (leaf.Cross) {
					System.Diagnostics.Debug.Write(string.Format("{0} -> ", leaf.Name));
					foreach (Leaf next in leaf.NextLeaf) {
						System.Diagnostics.Debug.Write(string.Format("{0}, ", next.Name));
					}
					System.Diagnostics.Debug.WriteLine("");
				}
			}
			System.Diagnostics.Debug.WriteLine("");
		}

	}
}
