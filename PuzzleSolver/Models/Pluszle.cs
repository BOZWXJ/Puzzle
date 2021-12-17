using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver.Models
{
	internal class Pluszle
	{
		static int[,] board;
		static int[] rowSum;
		static int[] colSum;
		static StringBuilder sb = new StringBuilder();
		public static string Solver(ref string problem)
		{
			if (string.IsNullOrWhiteSpace(problem)) {
				problem = "6 8 3 6 1 4 7 8 13\n9 7 6 1 9 2 4 9 22\n1 3 8 3 2 5 5 9 18\n2 8 8 4 8 3 5 7 37\n9 3 8 3 1 8 2 3 17\n9 1 7 2 8 8 6 5 16\n9 8 5 4 4 3 2 7 9\n6 8 8 5 6 5 2 6 25\n30 20 16 11 7 15 14 44\n";
			}

			int row;
			int col;
			// 問題文
			try {
				string[] lines = problem.Split("\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
				string[][] cells = new string[lines.Length][];
				List<string> list = new List<string>();
				foreach (var (str, i) in lines.Select((str, i) => (str, i))) {
					list.Clear();
					string s = "";
					foreach (var c in str) {
						if ('0' <= c && c <= '9') {
							s += c.ToString();
						} else {
							list.Add(s);
							s = "";
						}
					}
					if (!string.IsNullOrEmpty(s)) {
						list.Add(s);
					}
					cells[i] = list.ToArray();
				}
				row = cells.Length - 1;
				col = cells[0].Length - 1;
				if (row < 1 || col < 1) {
					return "Error";
				}
				board = new int[row, col];
				rowSum = new int[row];
				colSum = new int[col];
				for (int i = 0; i <= row; i++) {
					for (int j = 0; j <= col; j++) {
						if (i < row) {
							if (j < col) {
								board[i, j] = int.Parse(cells[i][j]);
							} else {
								rowSum[i] = int.Parse(cells[i][j]);
							}
						} else {
							if (j < col) {
								colSum[j] = int.Parse(cells[i][j]);
							}
						}
					}
				}
			} catch {
				return "Error";
			}

			// 回答
			bool[,] flg = new bool[row, col];
			int[] rSum = new int[row];
			int[] cSum = new int[col];
			if (Saiki(0, 0, flg, rSum, cSum)) {
				int w = Math.Max(rowSum.Max(), colSum.Max()).ToString().Length;
				sb.Clear();
				for (int i = 0; i < row; i++) {
					for (int j = 0; j < col; j++) {
						if (j > 0) {
							sb.Append(",");
						}
						if (flg[i, j]) {
							sb.Append(board[i, j].ToString().PadLeft(w));
						} else {
							sb.Append(new string(' ', w));
						}
					}
					sb.AppendLine($":{rowSum[i].ToString().PadLeft(w)}");
				}
				sb.AppendLine(string.Join(" ", colSum.Select(p => p.ToString().PadLeft(w))));
				return sb.ToString();
			}
			return "No";
		}

		private static bool Saiki(int r, int c, bool[,] flg, int[] rSum, int[] cSum)
		{
			// 終了判定
			if (r >= board.GetLength(0)) {
				return true;
			}
			// 次
			int nextR = r, nextC = c + 1;
			if (nextC >= board.GetLength(1)) {
				nextR++;
				nextC = 0;
			}
			// 選択しない
			if (Check(r, c, rSum[r], cSum[c])) {
				if (Saiki(nextR, nextC, flg, rSum, cSum)) { return true; }
			}
			// 選択する
			flg[r, c] = true;
			rSum[r] += board[r, c];
			cSum[c] += board[r, c];
			if (Check(r, c, rSum[r], cSum[c])) {
				if (Saiki(nextR, nextC, flg, rSum, cSum)) { return true; }
			}
			flg[r, c] = false;
			rSum[r] -= board[r, c];
			cSum[c] -= board[r, c];
			return false;
		}

		private static bool Check(int r, int c, int rs, int cs)
		{
			if (c == board.GetLength(1) - 1) {
				if (rowSum[r] != rs) {
					return false;
				}
			} else {
				if (rowSum[r] < rs) {
					return false;
				}
			}
			if (r == board.GetLength(0) - 1) {
				if (colSum[c] != cs) {
					return false;
				}
			} else {
				if (colSum[c] < cs) {
					return false;
				}
			}
			return true;
		}

	}
}
