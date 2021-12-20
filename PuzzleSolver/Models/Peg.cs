using Microsoft.VisualBasic.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PuzzleSolver.Models
{
	internal class Peg
	{
		const char PegChar = 'o';
		const char WallChar = '#';
		const char BlankChar = ' ';

		static int height;
		static int width;
		static char[,] start;
		static readonly Dictionary<int, (int parent, char[,] board, string msg)> stateDict = new();
		static readonly HashSet<string> stateHash = new();

		internal static string Solver(ref string problem, CancellationToken token)
		{
			if (string.IsNullOrWhiteSpace(problem)) {
				problem = "**   **\r\n**   **\r\n       \r\n  o o  \r\n  ooo  \r\n**ooo**\r\n**ooo**\r\n";
				problem = "**ooo**\r\n**ooo**\r\nooooooo\r\nooo ooo\r\nooooooo\r\n**ooo**\r\n**ooo**\r\n";
			}

			// 問題文
			try {
				string[] lines = problem.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
				height = lines.Length;
				width = lines.Select(p => p.Length).Max();
				start = SetBoard(lines);
			} catch {
				return "Error";
			}

			// 探索
			stateDict.Clear();
			stateHash.Clear();
			stateDict.Add(0, (0, start, ""));
			stateHash.Add(MakeHashString(start));
			int ans = Saiki(0, 1, token);
			if (ans < 0) {
				return "No";
			}
			List<int> ansList = new();
			ansList.Add(ans);
			while (true) {
				if (0 != stateDict[ans].parent) {
					ansList.Add(stateDict[ans].parent);
					ans = stateDict[ans].parent;
				} else {
					break;
				}
			}
			ansList.Reverse();
			string answer = $"{ansList.Count}\n";
			foreach (var i in ansList) {
				answer += $"{stateDict[i].msg}\n{PrintBoard(stateDict[i].board)}";
			}
			return answer;
		}

		private static int Saiki(int id, int lv, CancellationToken token)
		{
			token.ThrowIfCancellationRequested();
			if (CheckComplete(stateDict[id].board)) {
				return id;
			}
			// 次
			for (int row = 0; row < height; row++) {
				for (int col = 0; col < width; col++) {
					if (stateDict[id].board[row, col] == PegChar) {
						foreach (Direction d in Enum.GetValues(typeof(Direction))) {
							var next = MakeNextState(stateDict[id].board, row, col, d);
							if (next != null) {
								string nextHash = MakeHashString(next);
								if (!stateHash.Contains(nextHash)) {
									int nextId = stateDict.Count;
									stateDict.Add(nextId, (id, next, $"{lv}:({row},{col}) {d}"));
									stateHash.Add(nextHash);
									//System.Diagnostics.Debug.WriteLine($"{id}->{nextId} [label=\"{lv}:({row},{col}) {d}\"]");
									int result = Saiki(nextId, lv + 1, token);
									if (result >= 0) {
										return result;
									}
								}
							}
						}
					}
				}
			}
			return -1;
		}

		private static char[,] MakeNextState(char[,] board, int row, int col, Direction d)
		{
			char[,] result = new char[height, width];
			for (int i = 0; i < height; i++) {
				for (int j = 0; j < width; j++) {
					result[i, j] = board[i, j];
				}
			}
			int r1 = row + (d == Direction.Up ? -1 : d == Direction.Down ? 1 : 0);
			int r2 = row + (d == Direction.Up ? -2 : d == Direction.Down ? 2 : 0);
			int c1 = col + (d == Direction.Left ? -1 : d == Direction.Right ? 1 : 0);
			int c2 = col + (d == Direction.Left ? -2 : d == Direction.Right ? 2 : 0);
			if (0 <= r2 && r2 < height && 0 <= c2 && c2 < width && board[r1, c1] == PegChar && board[r2, c2] == BlankChar) {
				result[row, col] = BlankChar;
				result[r1, c1] = BlankChar;
				result[r2, c2] = PegChar;
				return result;
			} else {
				return null;
			}
		}

		// 文字列を読み込む
		private static char[,] SetBoard(IEnumerable<string> collection)
		{
			char[,] result = new char[height, width];
			for (int i = 0; i < height; i++) {
				for (int j = 0; j < width; j++) {
					result[i, j] = WallChar;
				}
			}
			foreach (var (s, i) in collection.Select((p, i) => (p, i))) {
				foreach (var (c, j) in s.Select((p, i) => (p, i))) {
					if (c == BlankChar) {
						result[i, j] = BlankChar;
					} else if (c.IsPiece()) {
						result[i, j] = PegChar;
					} else {
						result[i, j] = WallChar;
					}
				}
			}
			return result;
		}

		private static readonly StringBuilder sb = new();
		private static string MakeHashString(char[,] state)
		{
			sb.Clear();
			for (int i = 0; i < height; i++) {
				for (int j = 0; j < width; j++) {
					sb.Append(state[i, j]);
				}
			}
			return sb.ToString();
		}

		// 完了
		private static bool CheckComplete(char[,] state)
		{
			int cnt = 0;
			for (int i = 0; i < height; i++) {
				for (int j = 0; j < width; j++) {
					if (state[i, j] == PegChar) {
						cnt++;
					}
				}
			}
			return cnt == 1;
		}

		private static string PrintBoard(char[,] data)
		{
			sb.Clear();
			for (int i = 0; i < data.GetLength(0); i++) {
				sb.Append("  ");
				for (int j = 0; j < data.GetLength(1); j++) {
					sb.Append(data[i, j]);
				}
				sb.AppendLine();
			}
			return sb.ToString();
		}

	}
}
