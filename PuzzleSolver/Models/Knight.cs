using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PuzzleSolver.Models
{
	internal class Knight
	{
		static int height;
		static int width;
		static int[,] board;
		static (int row, int col) start;
		static int goalStep;

		internal static string Solver(ref string problem, CancellationToken token)
		{
			if (string.IsNullOrWhiteSpace(problem)) {
				problem = "*******\r\n*K    *\r\n*     *\r\n*     *\r\n*     *\r\n*     *\r\n*     *\r\n*******\r\n";
			}

			// 問題文
			try {
				string[] lines = problem.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
				height = lines.Length;
				width = lines.Select(p => p.Length).Max();
				board = new int[height, width];
				start = default;
				goalStep = 1;
				for (int i = 0; i < height; i++) {
					for (int j = 0; j < width; j++) {
						board[i, j] = -1;
					}
				}
				foreach (var (s, i) in lines.Select((p, i) => (p, i))) {
					foreach (var (c, j) in s.Select((p, i) => (p, i))) {
						if (c.IsPiece()) {
							if (start == default) {
								board[i, j] = 1;
								start = (i, j);
							} else {
								return "Error";
							}
						} else if (c == ' ') {
							board[i, j] = 0;
							goalStep++;
						}
					}
				}
				if (start == default) {
					return "Error";
				}
			} catch {
				return "Error";
			}

			// 探索
			if (Saiki(start.row, start.col, 1, token)) {
				return PrintBoard(board);
			} else {
				return "No";
			}
		}

		static readonly (int, int)[] pos = new (int, int)[] { (-2, 1), (-1, 2), (1, 2), (2, 1), (2, -1), (1, -2), (-1, -2), (-2, -1) };
		private static bool Saiki(int row, int col, int cnt, CancellationToken token)
		{
			token.ThrowIfCancellationRequested();
			if (cnt == goalStep) {
				return true;
			}
			// 次
			foreach (var (i, j) in pos) {
				int r = row + i;
				int c = col + j;
				if (0 <= r && r < height && 0 <= c && c < width && board[r, c] == 0) {
					board[r, c] = cnt + 1;
					if (Saiki(r, c, cnt + 1, token)) {
						return true;
					}
					board[r, c] = 0;
				}
			}
			return false;
		}

		private static readonly StringBuilder sb = new();
		private static string PrintBoard(int[,] data)
		{
			sb.Clear();
			int l = 0;
			for (int i = 0; i < data.GetLength(0); i++) {
				for (int j = 0; j < data.GetLength(1); j++) {
					if (data[i, j] > 0) {
						l = Math.Max(l, data[i, j].ToString().Length);
					}
				}
			}
			for (int i = 0; i < data.GetLength(0); i++) {
				sb.Append("");
				for (int j = 0; j < data.GetLength(1); j++) {
					if (l > 1 && j > 0) {
						sb.Append(",");
					}
					if (data[i, j] < 0) {
						sb.Append("*".PadLeft(l));
					} else {
						sb.Append($"{data[i, j]}".PadLeft(l));
					}
				}
				sb.AppendLine();
			}
			return sb.ToString();
		}

	}
}
