using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Q2
{
	class Program
	{
		const int BoxCount = 20;
		const int BoxXsize = 2;
		const int BoxYsize = 3;
		const int BoardArea = BoxXsize * BoxYsize * BoxCount;
		static readonly int BoxMin = Math.Min(BoxXsize, BoxYsize);
		public enum BoxDirection { Horizontal, Vertical }
		const char BlankChar = '○';
		const char WallChar = '×';

		static StringBuilder sb = new StringBuilder();

		[STAThreadAttribute]
		static void Main(string[] args)
		{
			System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
			sw.Start();

			int count = 0;
			for (int x = BoxMin; x <= BoardArea / BoxMin; x++) {
				int y = BoardArea / x;
				if (x * y == BoardArea && y >= BoxMin && x <= y) {
					// 盤面の準備
					ConsoleOutput(string.Format("({0},{1})\r\n", x, y));
					char[,] Board = new char[y + 2, x + 2];
					for (int i = 0; i < Board.GetLength(0); i++) {
						for (int j = 0; j < Board.GetLength(1); j++) {
							if (i == 0 || i > y || j == 0 || j > x) {
								Board[i, j] = WallChar;
							} else {
								Board[i, j] = BlankChar;
							}
						}
					}
					// 深さ優先探索
					int c = Saiki(1, new Point(1, 1), Board, 0);
					// 長方形の時は、縦横で２倍にする
					if (x != y) {
						count += c * 2;
						ConsoleOutput(string.Format("{0}×2＝{1}通り\r\n\r\n", c, c * 2));
					} else {
						count += c;
						ConsoleOutput(string.Format("{0}通り\r\n\r\n", c));
					}
				}
			}
			ConsoleOutput(string.Format("合計{0}通り\r\n\r\n", count));

			sw.Stop();
			ConsoleOutput(string.Format("計算時間：{0}\r\n\r\n", sw.Elapsed));

			System.Diagnostics.Debug.Write(sb.ToString());
			Console.Write(sb.ToString());
			Console.Write("Hit Any Key");
			Console.Read();

			Clipboard.SetDataObject(sb.ToString(), true);
		}

		static void ConsoleOutput(string str)
		{
			sb.Append(str);
			//System.Diagnostics.Debug.Write(str);
			//Console.Write(str);
		}

		static HashSet<string> set = new HashSet<string>();

		static int Saiki(int num, Point pos, char[,] Board, int count)
		{
			foreach (BoxDirection direction in new BoxDirection[] { BoxDirection.Horizontal, BoxDirection.Vertical }) {
				if (BlankCheck(pos, direction, Board)) {
					SetBox(num, pos, direction, Board);
					// debug: ConsoleOutput(string.Format("{0}:{1},{2},{3}\r\n", direction == BoxDirection.Vertical ? "縦" : "横", num, pos.X, pos.Y));
					if (num == BoxCount) {
						count++;
						// 解答表示
						if (count > 1) {
							ConsoleOutput("\r\n");
						}
						ConsoleOutput(PrintBoard(Board));
						// 重複チェック
						string str = PrintBoard(Board);
						if (set.Contains(str)) {
							throw new NotImplementedException();
						}
					} else {
						// 次の Box を置く場所を探す
						bool f = false;
						for (int top = 1; top < Board.GetLength(0); top++) {
							for (int left = 1; left < Board.GetLength(1); left++) {
								if (Board[top, left] == BlankChar) {
									count = Saiki(num + 1, new Point(left, top), Board, count);
									f = true;
									break;
								}
							}
							if (f) {
								break;
							}
						}
					}
					RemoveBox(pos, direction, Board);
				}
			}
			return count;
		}

		static bool BlankCheck(Point pos, BoxDirection direction, char[,] Board)
		{
			int sizeX, sizeY;
			if (direction == BoxDirection.Vertical) {
				sizeX = BoxXsize;
				sizeY = BoxYsize;
			} else {
				sizeX = BoxYsize;
				sizeY = BoxXsize;
			}
			for (int top = 0; top < sizeY; top++) {
				for (int left = 0; left < sizeX; left++) {
					if (Board[pos.Y + top, pos.X + left] != BlankChar) {
						return false;
					}
				}
			}
			return true;
		}

		static void SetBox(int num, Point pos, BoxDirection direction, char[,] Board)
		{
			int sizeX, sizeY;
			if (direction == BoxDirection.Vertical) {
				sizeX = BoxXsize;
				sizeY = BoxYsize;
			} else {
				sizeX = BoxYsize;
				sizeY = BoxXsize;
			}
			for (int top = 0; top < sizeY; top++) {
				for (int left = 0; left < sizeX; left++) {
					char c = BlankChar;
					if (num != 0) {
						c = '　';
						if (top == 0 && left == 0) {
							c = '┌';
						} else if (top == 0 && left == sizeX - 1) {
							c = '┐';
						} else if (top == sizeY - 1 && left == 0) {
							c = '└';
						} else if (top == sizeY - 1 && left == sizeX - 1) {
							c = '┘';
						} else if (top == 0 || top == sizeY - 1) {
							c = '─';
						} else if (left == 0 || left == sizeX - 1) {
							c = '│';
						}
					}
					Board[pos.Y + top, pos.X + left] = c;
				}
			}
		}

		static void RemoveBox(Point pos, BoxDirection direction, char[,] Board)
		{
			SetBox(0, pos, direction, Board);
		}

		static string PrintBoard(char[,] Board)
		{
			StringBuilder result = new StringBuilder();
			for (int top = 0; top < Board.GetLength(0); top++) {
				for (int left = 0; left < Board.GetLength(1); left++) {
					result.Append(Board[top, left]);
				}
				result.AppendLine();
			}
			return result.ToString();
		}
	}
}
