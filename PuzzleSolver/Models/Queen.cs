using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace PuzzleSolver.Models
{
	internal class Queen
	{
		const char QueenChar = 'Q';
		const char WallChar = '#';
		const char BlankChar = ' ';

		static int count;
		static char[,] board;
		static Stack<(int row, int col)> position;
		internal static string Solver(ref string problem, CancellationToken token)
		{
			if (string.IsNullOrWhiteSpace(problem)) {
				problem = "8\r\n**********\r\n*        *\r\n*   Q    *\r\n*        *\r\n*        *\r\n*        *\r\n*        *\r\n*        *\r\n*        *\r\n**********\r\n";
			}

			// 問題文
			try {
				string[] lines = problem.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
				count = int.Parse(lines[0]);
				position = new();
				if (lines.Length == 1) {
					board = new char[count + 2, count + 2];
					for (int i = 0; i < count + 2; i++) {
						for (int j = 0; j < count + 2; j++) {
							board[i, j] = (i == 0 || i == count + 1 || j == 0 || j == count + 1) ? WallChar : BlankChar;
						}
					}
				} else {
					int h = lines.Length - 1;
					int w = lines.Skip(1).Select(p => p.Length).Max();
					board = new char[h, w];
					SetBoard(lines.Skip(1));
				}
			} catch {
				return "Error";
			}

			// 探索
			int r = 0, c = 0, st = position.Count;
			while (position.Count < count) {
				token.ThrowIfCancellationRequested();
				if (CheckPosition(r, c)) {
					//System.Diagnostics.Debug.WriteLine($"({r},{c}),{position.Count}:{string.Join(" ", position)}");
					position.Push((r, c));
				}
				if (!Increment(ref r, ref c)) {
					int i, j;
					do {
						if (position.Count > st) {
							(i, j) = position.Pop();
						} else {
							return "No";
						}
					} while (!Increment(ref i, ref j));
					r = i;
					c = j;
				}
			}

			// 結果
			foreach (var (i, j) in position) {
				board[i, j] = QueenChar;
			}
			return PrintBoard(board);
		}

		// 文字列を読み込む
		private static void SetBoard(IEnumerable<string> collection)
		{
			for (int i = 0; i < board.GetLength(0); i++) {
				for (int j = 0; j < board.GetLength(1); j++) {
					board[i, j] = WallChar;
				}
			}
			foreach (var (s, i) in collection.Select((p, i) => (p, i))) {
				foreach (var (c, j) in s.Select((p, i) => (p, i))) {
					if (c == BlankChar) {
						board[i, j] = BlankChar;
					} else if (c.IsPiece()) {
						board[i, j] = QueenChar;
						position.Push((i, j));
					} else {
						board[i, j] = WallChar;
					}
				}
			}
		}

		private static bool CheckPosition(int row, int col)
		{
			if (board[row, col] == WallChar) {
				return false;
			}
			foreach (var (r, c) in position) {
				if (row == r && col == c) {
					return false;
				} else if (row == r) {
					bool f = true;
					int x = Math.Sign(col.CompareTo(c));
					for (int i = c + x; i != col; i += x) {
						if (board[r, i] == WallChar) {
							f = false;
							break;
						}
					}
					if (f) {
						return false;
					}
				} else if (col == c) {
					bool f = true;
					int y = Math.Sign(row.CompareTo(r));
					for (int i = r + y; i != row; i += y) {
						if (board[i, c] == WallChar) {
							f = false;
							break;
						}
					}
					if (f) {
						return false;
					}
				} else if (Math.Abs(row - r) == Math.Abs(col - c)) {
					bool f = true;
					int x = Math.Sign(col.CompareTo(c));
					int y = Math.Sign(row.CompareTo(r));
					for (int i = r + y, j = c + x; i != row; i += y, j += x) {
						if (board[i, j] == WallChar) {
							f = false;
							break;
						}
					}
					if (f) {
						return false;
					}
				}
			}
			return true;
		}

		private static bool Increment(ref int row, ref int col)
		{
			int i = board.GetLength(1) * row + col + 1;
			row = i / board.GetLength(1);
			col = i % board.GetLength(0);
			return row < board.GetLength(0);
		}

		private static readonly StringBuilder sb = new();
		private static string PrintBoard(char[,] data)
		{
			sb.Clear();
			for (int i = 0; i < data.GetLength(0); i++) {
				for (int j = 0; j < data.GetLength(1); j++) {
					if (data[i, j] == BlankChar) {
						if ((i + (j % 2)) % 2 == 0) {
							sb.Append(' ');
						} else {
							sb.Append('.');
						}
					} else {
						sb.Append(data[i, j]);
					}
				}
				sb.AppendLine();
			}
			return sb.ToString();
		}

	}
}
